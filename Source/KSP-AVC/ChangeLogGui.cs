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

using UnityEngine;

#endregion

namespace KSP_AVC
{
    public class ChangeLogGui : MonoBehaviour
    {
        #region Fields

        private GUIStyle closeStyle;
        private GUIStyle labelStyle;
        private Rect position = new Rect(0, 0, Screen.width, Screen.height);
        private Vector2 scrollPosition;

        #endregion

        #region Properties

        public string Name { get; set; }

        public string Text { get; set; }

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
            Log.trace("ChangeLogGui was created.");
        }

        protected void OnDestroy()
        {
            Log.trace("ChangeLogGui was destroyed.");
        }

        protected void OnGUI()
        {
            try
            {
                GUI.skin = null;
                this.position = GUILayout.Window(this.GetInstanceID(), this.position, this.Window, this.Name + " - Change Log", HighLogic.Skin.window);
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
            }
            catch (Exception ex)
            {
                Log.ex(this,ex);
            }
        }

        #endregion

        #region Methods: private

        private void InitialiseStyles()
        {
            this.closeStyle = new GUIStyle(HighLogic.Skin.button)
            {
                normal =
                {
                    textColor = Color.white
                },
            };

            this.labelStyle = new GUIStyle(HighLogic.Skin.label)
            {
                normal =
                {
                    textColor = Color.white
                },
                fontStyle = FontStyle.Bold,
                stretchWidth = true,
                stretchHeight = true,
                wordWrap = true
            };
        }

        private void Window(int id)
        {
            try
            {
                GUI.skin = HighLogic.Skin;
                this.scrollPosition = GUILayout.BeginScrollView(this.scrollPosition, false, true);
                GUI.skin = null;
                GUILayout.Label(this.Text, this.labelStyle);
                GUILayout.EndScrollView();

                if (GUILayout.Button("CLOSE", this.closeStyle))
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
    }
}