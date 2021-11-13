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
using System.Linq;
using System.Reflection;

using UnityEngine;

#endregion

namespace KSP_AVC
{
    [KSPAddon(KSPAddon.Startup.Instantly, false)]
    public class Starter : MonoBehaviour
    {
        #region Fields

        private static bool hasAlreadyChecked;
        private CheckerProgressGui checkerProgressGui;
        private FirstRunGui firstRunGui;

        #endregion

        #region Methods: protected

        protected void Awake()
        {
            try
            {
                if (this.HasAlreadyChecked())
                {
                    return;
                }
                DontDestroyOnLoad(this);
            }
            catch (Exception ex)
            {
                Log.ex(this,ex);
            }
            Log.trace("Starter was created.");
        }

        protected void OnDestroy()
        {
            try
            {
                if (this.firstRunGui != null)
                {
                    Destroy(this.firstRunGui);
                }
                if (this.checkerProgressGui != null)
                {
                    Destroy(this.checkerProgressGui);
                }
            }
            catch (Exception ex)
            {
                Log.ex(this,ex);
            }
            Log.trace("Starter was destroyed.");
        }

        protected void Start()
        {
            try
            {
                if (new System.Version(Configuration.GetVersion()) < Assembly.GetExecutingAssembly().GetName().Version)
                {
                    this.ShowUpdatedWindow();
                }
                else if (Configuration.GetFirstRun())
                {
                    this.ShowInstalledWindow();
                }
            }
            catch (Exception ex)
            {
                Log.ex(this,ex);
            }
        }

        protected void Update()
        {
            if (HighLogic.LoadedScene == GameScenes.SPACECENTER)
            {
                Destroy(gameObject);
            }

            try
            {
                if (this.firstRunGui != null)
                {
                    return;
                }
                if (this.ShowIssuesWindow())
                {
                    return;
                }
                if (this.checkerProgressGui == null)
                {
                    this.checkerProgressGui = this.gameObject.AddComponent<CheckerProgressGui>();
                }
            }
            catch (Exception ex)
            {
                Log.ex(this,ex);
            }
        }

        #endregion

        #region Methods: private

        private bool HasAlreadyChecked()
        {
            if (hasAlreadyChecked)
            {
                Destroy(this);
                return true;
            }
            hasAlreadyChecked = true;
            return false;
        }

        private void ShowInstalledWindow()
        {
            Configuration.SetFirstRun(false);
            this.firstRunGui = this.gameObject.AddComponent<FirstRunGui>();
        }

        private bool ShowIssuesWindow()
        {
            if (!AddonLibrary.Populated || !AddonLibrary.ProcessingComplete)
            {
                return false;
            }
            if (AddonLibrary.Addons.Any(a => a.IsUpdateAvailable || !a.IsCompatible))
            {
                this.gameObject.AddComponent<IssueGui>();
            }
            Destroy(this);
            return true;
        }

        private void ShowUpdatedWindow()
        {
            Configuration.SetVersion(Assembly.GetExecutingAssembly().GetName().Version.ToString());
            this.firstRunGui = this.gameObject.AddComponent<FirstRunGui>();
            this.firstRunGui.HasBeenUpdated = true;
        }

        #endregion
    }
}