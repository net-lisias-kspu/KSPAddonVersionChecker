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

using UnityEngine;

#endregion

namespace KSP_AVC.Toolbar
{
    [KSPAddon(KSPAddon.Startup.Instantly, false)]
    public class ToolbarWindow : MonoBehaviour
    {
        #region Fields

        private string addonList;
        private GUIStyle labelGreen;
        private GUIStyle labelYellow;
        private Rect position = new Rect(0.0f, 0.0f, 400.0f, 0.0f);
        private Vector2 scrollPosition;
        private bool showAddons;
        private bool useScrollView;
        private GUIStyle windowStyle;

        #endregion

        #region Methods: protected

        protected void Awake()
        {
            try
            {
                DontDestroyOnLoad(this);
            }
            catch (Exception ex)
            {
                Log.ex(this,ex);
            }
        }

        protected void OnGUI()
        {
            try
            {
                this.position = GUILayout.Window(this.GetInstanceID(), this.position, this.Window, String.Empty, this.windowStyle);
                this.CheckScrollViewUsage();
            }
            catch (Exception ex)
            {
                Log.ex(this,ex);
            }
        }

        protected void Start()
        {
            try
            {
                if (AssemblyLoader.loadedAssemblies.Any(a => a.name == "DevHelper"))
                {
                    this.position.y = 60.0f;
                }
                this.InitialiseStyles();
            }
            catch (Exception ex)
            {
                Log.ex(this,ex);
            }
        }

        protected void Update()
        {
            try
            {
                if (HighLogic.LoadedScene != GameScenes.LOADING && HighLogic.LoadedScene != GameScenes.MAINMENU)
                {
                    Destroy(this);
                }
            }
            catch (Exception ex)
            {
                Log.ex(this,ex);
            }
        }

        #endregion

        #region Methods: private

        private void CheckScrollViewUsage()
        {
            if (this.position.height < Screen.height * 0.5f || this.useScrollView)
            {
                return;
            }

            this.useScrollView = true;
            this.position.height = Screen.height * 0.5f;
        }

        private void CopyToClipboard()
        {
            if (!GUILayout.Button("Copy to Clipboard"))
            {
                return;
            }

            var kspVersion = AddonInfo.ActualKspVersion.ToString();
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                if (IntPtr.Size == 8)
                {
                    kspVersion += " (Win64)";
                }
                else
                {
                    kspVersion += " (Win32)";
                }
            }
            else
            {
                kspVersion += " (" + Environment.OSVersion.Platform + ")";
            }

            var copyText = "KSP: " + kspVersion +
                           " - Unity: " + Application.unityVersion +
                           " - OS: " + SystemInfo.operatingSystem +
                           this.addonList;

            var textEditor = new TextEditor
            {
                text = copyText
            };
            textEditor.SelectAll();
            textEditor.Copy();
        }

        private void DrawAddonBoxEnd()
        {
            if (this.useScrollView)
            {
                GUILayout.EndScrollView();
            }
            else
            {
                GUILayout.EndVertical();
            }
        }

        private void DrawAddonBoxStart()
        {
            if (this.useScrollView)
            {
                this.scrollPosition = GUILayout.BeginScrollView(this.scrollPosition, GUILayout.Height(Screen.height * 0.5f));
            }
            else
            {
                GUILayout.BeginVertical(GUI.skin.scrollView);
            }
        }

        private void DrawAddonList()
        {
            this.DrawAddonBoxStart();
            this.DrawAddons();
            this.DrawAddonBoxEnd();

            GUILayout.Space(5.0f);
            this.CopyToClipboard();
            GUILayout.Space(5.0f);
        }

        private void DrawAddons()
        {
            this.addonList = String.Empty;
            foreach (var addon in AddonLibrary.Addons)
            {
                var labelStyle = !addon.IsCompatible || addon.IsUpdateAvailable ? this.labelYellow : this.labelGreen;
                this.addonList += Environment.NewLine + addon.Name + " - " + addon.LocalInfo.Version;
                GUILayout.BeginHorizontal();
                GUILayout.Label(addon.Name, labelStyle);
                GUILayout.FlexibleSpace();
                GUILayout.Label(addon.LocalInfo != null ? addon.LocalInfo.Version.ToString() : String.Empty, labelStyle);
                GUILayout.EndHorizontal();
            }
        }

        private void InitialiseStyles()
        {
            this.windowStyle = new GUIStyle
            {
                normal =
                {
                    background = UI.Image.OverlayBackground
                },
                border = new RectOffset(3, 3, 20, 3),
                padding = new RectOffset(10, 10, 23, 5)
            };

            this.labelGreen = new GUIStyle
            {
                normal =
                {
                    textColor = Color.green
                }
            };

            this.labelYellow = new GUIStyle
            {
                normal =
                {
                    textColor = Color.yellow
                }
            };
        }

        private void Window(int windowId)
        {
            try
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label("Installed Add-ons: " + AddonLibrary.TotalCount);
                GUILayout.FlexibleSpace();
                if (GUILayout.Toggle(this.showAddons, "Show Add-ons") != this.showAddons)
                {
                    this.showAddons = !this.showAddons;
                    this.position.height = 0.0f;
                }
                GUILayout.EndHorizontal();
                if (this.showAddons)
                {
                    this.DrawAddonList();
                }
            }
            catch (Exception ex)
            {
                Log.ex(this,ex);
            }
        }

        #endregion
    }
}