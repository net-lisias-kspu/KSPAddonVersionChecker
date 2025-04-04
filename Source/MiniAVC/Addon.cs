﻿/*
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
using System;
using System.IO;
using System.Threading;
using UnityEngine;

namespace MiniAVC
{
    public class Addon
    {
        private readonly AddonSettings settings;

        public Addon(string path, AddonSettings settings)
        {
            this.settings = settings;
            RunProcessLocalInfo(path);
        }

        public string Base64String
        {
            get
            {
                return LocalInfo.Base64String + RemoteInfo.Base64String;
            }
        }

        public bool HasError { get; private set; }

        public bool IsCompatible
        {
            get { return IsLocalReady && LocalInfo.IsCompatible; }
        }

        public bool IsIgnored
        {
            get
            {
                return settings.IgnoredUpdates.Contains(Base64String);
            }
        }

        public bool IsLocalReady { get; private set; }

        public bool IsProcessingComplete { get; private set; }

        public bool IsRemoteReady { get; private set; }

        public bool IsUpdateAvailable
        {
            get { return IsProcessingComplete && LocalInfo.Version != null && RemoteInfo.Version != null && RemoteInfo.Version > LocalInfo.Version && RemoteInfo.IsCompatibleKspVersion && RemoteInfo.IsCompatibleGitHubVersion; }
        }

        public AddonInfo LocalInfo { get; private set; }

        public string Name
        {
            get { return LocalInfo.Name; }
        }

        public AddonInfo RemoteInfo { get; private set; }

        public AddonSettings Settings
        {
            get { return settings; }
        }

        public void RunProcessLocalInfo(string file)
        {
            ThreadPool.QueueUserWorkItem(ProcessLocalInfo, file);
        }

        public void RunProcessRemoteInfo()
        {
            ThreadPool.QueueUserWorkItem(ProcessRemoteInfo);
        }

        private void FetchLocalInfo(string path)
        {
            using (var stream = new StreamReader(File.OpenRead(path)))
            {
                LocalInfo = new AddonInfo(path, stream.ReadToEnd());
                IsLocalReady = true;

                if (LocalInfo.ParseError)
                {
                    SetHasError();
                }
            }
        }

        private void FetchRemoteInfo()
        {
            using (var www = new WWW(Uri.EscapeUriString(LocalInfo.Url)))
            {
                while (!www.isDone)
                {
                    Thread.Sleep(100);
                }
                if (www.error == null)
                {
                    SetRemoteInfo(www);
                }
                else
                {
                    SetLocalInfoOnly();
                }
            }
        }

        private void ProcessLocalInfo(object state)
        {
            try
            {
                var path = (string)state;
                if (File.Exists(path))
                {
                    FetchLocalInfo(path);
                    RunProcessRemoteInfo();
                }
                else
                {
                    Logger.Log("File Not Found: " + path);
                    SetHasError();
                }
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
                SetHasError();
            }
        }

        private void ProcessRemoteInfo(object state)
        {
            try
            {
                if (settings.FirstRun)
                {
                    return;
                }

                if (!settings.AllowCheck || string.IsNullOrEmpty(LocalInfo.Url))
                {
                    SetLocalInfoOnly();
                    return;
                }

                FetchRemoteInfo();
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
                SetLocalInfoOnly();
            }
        }

        private void SetHasError()
        {
            HasError = true;
            IsProcessingComplete = true;
        }

        private void SetLocalInfoOnly()
        {
            RemoteInfo = LocalInfo;
            IsRemoteReady = true;
            IsProcessingComplete = true;
            Logger.Log(LocalInfo);
            Logger.Blank();
        }

        private void SetRemoteInfo(WWW www)
        {
            RemoteInfo = new AddonInfo(LocalInfo.Url, www.text);
            RemoteInfo.FetchRemoteData();

            if (LocalInfo.Version == RemoteInfo.Version)
            {
                Logger.Log("Identical remote version found: Using remote version information only.");
                Logger.Log(RemoteInfo);
                Logger.Blank();
                LocalInfo = RemoteInfo;
            }
            else
            {
                Logger.Log(LocalInfo);
                Logger.Log(RemoteInfo + "\n\tUpdateAvailable: " + IsUpdateAvailable);
                Logger.Blank();
            }

            IsRemoteReady = true;
            IsProcessingComplete = true;
        }
    }
}