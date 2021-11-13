/*
	This file is part of KSP AddOn Version Checker /L Unleashed
		© 2021 Lisias T : http://lisias.net <support@lisias.net>
		© 2014-2016 Cybutek

	KSP AddOn Version Checker /L Unleashed is licensed as follows:
		* GPL 3.0 : https://www.gnu.org/licenses/gpl-3.0.txt

	KSP AddOn Version Checkere /L Unleashed is distributed in the hope that
	it will be useful, but WITHOUT ANY WARRANTY; without even the implied
	warranty of	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.

	You should have received a copy of the GNU General Public License 3.0 along
	with KSP AddOn Version Checker /L Unleashed.
	If not, see <https://www.gnu.org/licenses/>.

*/
#region Using Directives

using System;
using System.IO;
using System.Threading;

using UnityEngine;

#endregion

namespace KSP_AVC
{
    public class Addon
    {
        #region Constructors

        public Addon(string path)
        {
            this.RunProcessLocalInfo(path);
        }

        #endregion

        #region Properties

        public bool HasError { get; private set; }

        public bool IsCompatible
        {
            get { return this.IsLocalReady && this.LocalInfo.IsCompatible; }
        }

        public bool IsLocalReady { get; private set; }

        public bool IsProcessingComplete { get; private set; }

        public bool IsRemoteReady { get; private set; }

        public bool IsUpdateAvailable
        {
            get { return this.IsProcessingComplete && this.LocalInfo.Version != null && this.RemoteInfo.Version != null && this.RemoteInfo.Version > this.LocalInfo.Version && this.RemoteInfo.IsCompatibleKspVersion && this.RemoteInfo.IsCompatibleGitHubVersion; }
        }

        public AddonInfo LocalInfo { get; private set; }

        public string Name
        {
            get { return this.LocalInfo.Name; }
        }

        public AddonInfo RemoteInfo { get; private set; }

        #endregion

        #region Methods: public

        public void RunProcessLocalInfo(string path)
        {
            ThreadPool.QueueUserWorkItem(this.ProcessLocalInfo, path);
        }

        public void RunProcessRemoteInfo()
        {
            ThreadPool.QueueUserWorkItem(this.ProcessRemoteInfo);
        }

        #endregion

        #region Methods: private

        private void FetchLocalInfo(string path)
        {
            using (var stream = new StreamReader(File.OpenRead(path)))
            {
                this.LocalInfo = new AddonInfo(path, stream.ReadToEnd(), AddonInfo.RemoteType.AVC);
                this.IsLocalReady = true;

                if (this.LocalInfo.ParseError)
                {
                    this.SetHasError();
                }
            }
        }

        private void FetchRemoteInfo()
        {
			const float timeoutSeconds = 10.0f;
			float startTime = Time.time;
			float currentTime = startTime;

            if (string.IsNullOrEmpty(this.LocalInfo.Url) == false)
            {
				using (var www = new WWW(Uri.EscapeUriString(this.LocalInfo.Url)))
                {
					while ((!www.isDone) && ((currentTime - startTime) < timeoutSeconds))
                    {
                        Thread.Sleep(100);
						currentTime = Time.time;
                    }
					if ((www.error == null) && ((currentTime - startTime) < timeoutSeconds))
                    {
                        this.SetRemoteAvcInfo(www);
                    }
                    else
                    {
                        this.SetLocalInfoOnly();
                    }
                }
            }
            else
            {
                this.SetLocalInfoOnly();
            }
        }

        private void ProcessLocalInfo(object state)
        {
            try
            {
                var path = (string)state;
                if (File.Exists(path))
                {
                    this.FetchLocalInfo(path);
                    this.RunProcessRemoteInfo();
                }
                else
                {
                    Log.err("File Not Found: {0}", path);
                    this.SetHasError();
                }
            }
            catch (Exception ex)
            {
                Log.ex(this,ex);
                this.SetHasError();
            }
        }

        private void ProcessRemoteInfo(object state)
        {
            try
            {
                if (String.IsNullOrEmpty(this.LocalInfo.Url) && String.IsNullOrEmpty(this.LocalInfo.KerbalStuffUrl))
                {
                    this.SetLocalInfoOnly();
                    return;
                }

                this.FetchRemoteInfo();
            }
            catch (Exception ex)
            {
                Log.ex(this,ex);
                this.SetLocalInfoOnly();
            }
        }

        private void SetHasError()
        {
            this.HasError = true;
            this.IsProcessingComplete = true;
        }

        private void SetLocalInfoOnly()
        {
            this.RemoteInfo = this.LocalInfo;
            this.IsRemoteReady = true;
            this.IsProcessingComplete = true;
            Log.detail("{0}", this.LocalInfo);
        }

        private void SetRemoteAvcInfo(WWW www)
        {
            this.RemoteInfo = new AddonInfo(this.LocalInfo.Url, www.text, AddonInfo.RemoteType.AVC);
            this.RemoteInfo.FetchRemoteData();

            if (this.LocalInfo.Version == this.RemoteInfo.Version)
            {
                Log.info("Identical remote version found: Using remote version information only.");
                Log.info("{0}", this.RemoteInfo);
                this.LocalInfo = this.RemoteInfo;
            }
            else
            {
                Log.info("{0}", this.LocalInfo);
                Log.info("{0}\n\tUpdateAvailable: {1}", this.RemoteInfo, this.IsUpdateAvailable);
            }

            this.IsRemoteReady = true;
            this.IsProcessingComplete = true;
        }

        private void SetRemoteKerbalStuffInfo(WWW www)
        {
            this.RemoteInfo = new AddonInfo(this.LocalInfo.KerbalStuffUrl, www.text, AddonInfo.RemoteType.KerbalStuff);

            if (this.LocalInfo.Version == this.RemoteInfo.Version)
            {
                Log.info("Identical remote version found: Using remote version information only.");
                Log.info("{0}", this.RemoteInfo);
                this.LocalInfo = this.RemoteInfo;
            }
            else
            {
                Log.info("{0}", this.LocalInfo);
                Log.info("{0}\n\tUpdateAvailable: {1}", this.RemoteInfo, this.IsUpdateAvailable);
            }

            this.IsRemoteReady = true;
            this.IsProcessingComplete = true;
        }

        #endregion
    }
}