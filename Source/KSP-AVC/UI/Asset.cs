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
using UnityEngine;
using Asset = KSPe.IO.Asset<KSP_AVC.Startup>;

namespace KSP_AVC.UI
{
	internal static class Image
	{
		private static Texture2D _OverlayBackground = null;
		internal static Texture2D OverlayBackground = _OverlayBackground ?? (_OverlayBackground = Asset.Texture2D.LoadFromFile(400, 100, "OverlayBackground.png"));

		private static Texture2D _DropDownNormal = null;
		internal static Texture2D DropDownNormal = _DropDownNormal ?? (_DropDownNormal = Asset.Texture2D.LoadFromFile(35, 25, "DropDownNormal.png"));

		private static Texture2D _DropDownHover = null;
		internal static Texture2D DropDownHover = _DropDownHover ?? (_DropDownHover = Asset.Texture2D.LoadFromFile(35, 25, "DropDownHover.png"));

		private static Texture2D _DropDownActive = null;
		internal static Texture2D DropDownActive = _DropDownActive ?? (_DropDownActive = Asset.Texture2D.LoadFromFile(35, 25, "DropDownActive.png"));

		private static Texture2D _DropDownOnNormal = null;
		internal static Texture2D DropDownOnNormal = _DropDownOnNormal ?? (_DropDownOnNormal = Asset.Texture2D.LoadFromFile(35, 25, "DropDownOnNormal.png"));

		private static Texture2D _DropDownOnHover = null;
		internal static Texture2D DropDownOnHover = _DropDownOnHover ?? (_DropDownOnHover = Asset.Texture2D.LoadFromFile(35, 25, "DropDownOnHover.png"));

		private static Texture2D _DropDownBackground = null;
		internal static Texture2D DropDownBackground = _DropDownBackground ?? (_DropDownBackground = Asset.Texture2D.LoadFromFile(35, 25, "DropDownBackground.png"));
	}
}
