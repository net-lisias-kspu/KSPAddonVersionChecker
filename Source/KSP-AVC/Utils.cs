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
    public static class Utils
    {
        #region Fields

        private static readonly string textureDirectory = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Textures");

        #endregion

        #region Methods: public

        public static Texture2D GetTexture(string file, int width, int height)
        {
            try
            {
                var texture = new Texture2D(width, height, TextureFormat.ARGB32, false);
                texture.LoadImage(File.ReadAllBytes(Path.Combine(textureDirectory, file)));
                return texture;
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
                return null;
            }
        }

        #endregion
    }
}