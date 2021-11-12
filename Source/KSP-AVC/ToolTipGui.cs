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
    public class ToolTipGui : MonoBehaviour
    {
        #region Fields

        private GUIContent content;
        private GUIStyle labelStyle;
        private Rect position;

        #endregion

        #region Properties

        public string Text
        {
            get { return (this.content ?? GUIContent.none).text; }
            set { this.content = new GUIContent(value); }
        }

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
                Logger.Exception(ex);
            }
            Logger.Log("ToolTipGui was created.");
        }

        protected void OnDestroy()
        {
            Logger.Log("ToolTipGui was destroyed.");
        }

        protected void OnGUI()
        {
            try
            {
                if (this.content == null || String.IsNullOrEmpty(this.content.text))
                {
                    return;
                }

                GUILayout.Window(this.GetInstanceID(), this.position, this.Window, String.Empty, GUIStyle.none);
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
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
                Logger.Exception(ex);
            }
        }

        protected void Update()
        {
            this.position.size = this.labelStyle.CalcSize(this.content ?? GUIContent.none);
            this.position.x = Mathf.Clamp(Input.mousePosition.x + 20.0f, 0, Screen.width - this.position.width);
            this.position.y = Screen.height - Input.mousePosition.y + (this.position.x < Input.mousePosition.x + 20.0f ? 20.0f : 0);
        }

        #endregion

        #region Methods: private

        private static Texture2D GetBackgroundTexture()
        {
            var texture = new Texture2D(1, 1, TextureFormat.ARGB32, false);
            texture.SetPixel(1, 1, new Color(1.0f, 1.0f, 1.0f, 1.0f));
            texture.Apply();
            return texture;
        }

        private void InitialiseStyles()
        {
            this.labelStyle = new GUIStyle
            {
                padding = new RectOffset(4, 4, 2, 2),
                normal =
                {
                    textColor = Color.black,
                    background = GetBackgroundTexture()
                },
                fontSize = 11
            };
        }

        private void Window(int windowId)
        {
            try
            {
                GUI.BringWindowToFront(windowId);
                GUILayout.Label(this.content ?? GUIContent.none, this.labelStyle);
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
            }
        }

        #endregion
    }
}