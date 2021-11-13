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
using System.Reflection;

using UnityEngine;

#endregion

namespace KSP_AVC
{
    public class FirstRunGui : MonoBehaviour
    {
        #region Fields

        private readonly VersionInfo version = Assembly.GetExecutingAssembly().GetName().Version;
        private GUIStyle buttonStyle;
        private bool hasCentred;
        private string message;
        private Rect position = new Rect(Screen.width, Screen.height, 0, 0);
        private string title;
        private GUIStyle titleStyle;

        #endregion

        #region Properties

        public bool HasBeenUpdated { get; set; }

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
            Log.trace("FirstRunGui was created.");
        }

        protected void OnDestroy()
        {
            Log.trace("FirstRunGui was destroyed.");
        }

        protected void OnGUI()
        {
            try
            {
                this.position = GUILayout.Window(this.GetInstanceID(), this.position, this.Window, this.title, HighLogic.Skin.window);
                this.CentreWindow();
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
                this.InitialiseStyles();
                this.title = "KSP-AVC Plugin - " + (this.HasBeenUpdated ? "Updated" : "Installed");
                this.message = (this.HasBeenUpdated ? "You have successfully updated KSP-AVC to v" : "You have successfully installed KSP-AVC v") + this.version;
            }
            catch (Exception ex)
            {
                Log.ex(this,ex);
            }
        }

        #endregion

        #region Methods: private

        private void CentreWindow()
        {
            if (this.hasCentred || !(this.position.width > 0) || !(this.position.height > 0))
            {
                return;
            }
            this.position.center = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
            this.hasCentred = true;
        }

        private void InitialiseStyles()
        {
            this.titleStyle = new GUIStyle(HighLogic.Skin.label)
            {
                normal =
                {
                    textColor = Color.white
                },
                fontSize = 13,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleCenter,
                stretchWidth = true
            };

            this.buttonStyle = new GUIStyle(HighLogic.Skin.button)
            {
                normal =
                {
                    textColor = Color.white
                }
            };
        }

        private void Window(int id)
        {
            try
            {
                GUILayout.BeginVertical(HighLogic.Skin.box);
                GUILayout.Label(this.message, this.titleStyle, GUILayout.Width(350.0f));
                GUILayout.EndVertical();
                if (GUILayout.Button("CLOSE", this.buttonStyle))
                {
                    Destroy(this);
                }
                GUI.DragWindow();
            }
            catch (Exception ex)
            {
                Log.ex(this,ex);
            }
        }

        #endregion
    }
}