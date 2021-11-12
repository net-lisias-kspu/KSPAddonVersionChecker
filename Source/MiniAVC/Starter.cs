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
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

#endregion Using Directives

namespace MiniAVC
{
    [KSPAddon(KSPAddon.Startup.Instantly, false)]
    public class Starter : MonoBehaviour
    {
        #region Fields

        private static bool alreadyRunning;
        private FirstRunGui shownFirstRunGui;
        private IssueGui shownIssueGui;

        #endregion Fields

        #region Methods: protected

        protected void Awake()
        {
            try
            {
                // Only allow one instance to run.
                if (alreadyRunning)
                {
                    Destroy(this);
                    return;
                }
                alreadyRunning = true;

                // Unload if KSP-AVC is detected.
                if (AssemblyLoader.loadedAssemblies.Any(a => a.name == "KSP-AVC"))
                {
                    Logger.Log("KSP-AVC was detected...  Unloading MiniAVC!");
                    Destroy(this);
                    return;
                }
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
            }
            Logger.Log("Starter was created.");
        }

        protected void OnDestroy()
        {
            Logger.Log("Starter was destroyed.");
        }

        protected void Update()
        {
            try
            {
                // Stop updating if there is already a gui being shown or the addon library has not
                // been populated.
                if (!AddonLibrary.Populated || this.shownFirstRunGui != null || this.shownIssueGui != null)
                {
                    return;
                }

                // Do not show first start if no add-ons were found, or just destroy if all add-ons
                // have been processed.
                if (AddonLibrary.TotalCount == 0)
                {
                    Destroy(this);
                    return;
                }

                // Create and show first run gui if required.
                if (this.CreateFirstRunGui())
                {
                    return;
                }

                // Create and show issue gui if required.
                this.CreateIssueGui();
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
            }
        }

        #endregion Methods: protected

        #region Methods: private

        private bool CreateFirstRunGui()
        {
            foreach (var settings in AddonLibrary.Settings.Where(s => s.FirstRun))
            {
                this.shownFirstRunGui = this.gameObject.AddComponent<FirstRunGui>();
                this.shownFirstRunGui.Settings = settings;
                this.shownFirstRunGui.Addons = AddonLibrary.Addons.Where(a => a.Settings == settings).ToList();
                return true;
            }
            return false;
        }

        private void CreateIssueGui()
        {
            foreach (var addon in AddonLibrary.AddonsProcessed)
            {
                if (!addon.HasError && (addon.IsUpdateAvailable || !addon.IsCompatible) && !addon.IsIgnored)
                {
                    this.shownIssueGui = this.gameObject.AddComponent<IssueGui>();
                    this.shownIssueGui.Addon = addon;
                    this.shownIssueGui.enabled = true;
                    AddonLibrary.Remove(addon);
                    break;
                }
                AddonLibrary.Remove(addon);
            }
        }

        #endregion Methods: private
    }
}