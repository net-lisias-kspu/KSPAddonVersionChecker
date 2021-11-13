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
using System.Reflection;

using UnityEngine;

#endregion

namespace KSP_AVC
{
    public class DropDownList : MonoBehaviour
    {
        #region Fields

        private readonly string textureDirectory = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Textures");

        private Rect listPosition;
        private GUIStyle listStyle;
        private Rect togglePosition;
        private GUIStyle toggleStyle;

        #endregion

        #region Properties

        public Addon Addon { get; set; }

        public Action<DropDownList, Addon> DrawAddonCallback { get; set; }

        public Action<DropDownList> DrawCallback { get; set; }

        public bool ShowList { get; set; }

        public ToolTipGui ToolTip { get; set; }

        #endregion

        #region Methods: public

        public void DrawButton(string label, Rect parent, float width)
        {
            this.ShowList = GUILayout.Toggle(this.ShowList, label, this.toggleStyle, GUILayout.Width(width));
            if (Event.current.type == EventType.repaint)
            {
                this.SetPosition(GUILayoutUtility.GetLastRect(), parent);
            }
        }

        #endregion

        #region Methods: protected

        protected void Awake()
        {
            try
            {
                DontDestroyOnLoad(this);
                this.InitialiseStyles();
                this.ToolTip = this.gameObject.AddComponent<ToolTipGui>();
            }
            catch (Exception ex)
            {
                Log.ex(this,ex);
            }
        }

        protected void OnDestroy()
        {
            try
            {
                if (this.ToolTip != null)
                {
                    Destroy(this.ToolTip);
                }
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
                if (!this.ShowList)
                {
                    if (this.ToolTip != null && !String.IsNullOrEmpty(this.ToolTip.Text))
                    {
                        this.ToolTip.Text = String.Empty;
                    }
                    return;
                }
                this.listPosition = GUILayout.Window(this.GetInstanceID(), this.listPosition, this.Window, String.Empty, this.listStyle);
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
                if (!Input.GetMouseButtonDown(0) && !Input.GetMouseButtonDown(1) && !Input.GetMouseButtonDown(2))
                {
                    return;
                }

                if (!this.listPosition.Contains(new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y)) && !this.togglePosition.Contains(new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y)))
                {
                    this.ShowList = false;
                }
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
            this.listStyle = new GUIStyle
            {
                normal =
                {
                    background = UI.Image.DropDownBackground
                },
                border = new RectOffset(5, 5, 0, 5),
                padding = new RectOffset(3, 3, 3, 3)
            };

            this.toggleStyle = new GUIStyle
            {
                normal =
                {
                    background = UI.Image.DropDownNormal,
                    textColor = Color.white
                },
                hover =
                {
                    background = UI.Image.DropDownHover,
                    textColor = Color.white
                },
                active =
                {
                    background = UI.Image.DropDownActive,
                    textColor = Color.white
                },
                onNormal =
                {
                    background = UI.Image.DropDownOnNormal,
                    textColor = Color.white
                },
                onHover =
                {
                    background = UI.Image.DropDownOnHover,
                    textColor = Color.white
                },
                border = new RectOffset(5, 25, 5, 5),
                margin = new RectOffset(0, 0, 3, 3),
                padding = new RectOffset(5, 30, 5, 5),
                fixedHeight = 25.0f,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleCenter
            };
        }

        private void SetPosition(Rect toggle, Rect parent)
        {
            this.togglePosition = toggle;
            this.togglePosition.x += parent.x;
            this.togglePosition.y += parent.y;
            this.listPosition.x = this.togglePosition.x;
            this.listPosition.y = this.togglePosition.y + this.togglePosition.height;
            this.listPosition.width = this.togglePosition.width;
        }

        private void Window(int windowId)
        {
            try
            {
                GUI.BringWindowToFront(windowId);
                GUI.BringWindowToFront(this.ToolTip.GetInstanceID());

                if (this.DrawCallback != null)
                {
                    this.DrawCallback(this);
                }
                else if (this.DrawAddonCallback != null)
                {
                    this.DrawAddonCallback(this, this.Addon);
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