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

using System.Reflection;
using System.Runtime.InteropServices;

#endregion

[assembly: AssemblyTitle("KSP-AVC /L Unleashed")]
[assembly: AssemblyDescription("KSP Add-on Version Checker is a standardised system for versioning mods")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany(KSP_AVC.LegalMamboJambo.Company)]
[assembly: AssemblyProduct(KSP_AVC.LegalMamboJambo.Product)]
[assembly: AssemblyCopyright(KSP_AVC.LegalMamboJambo.Copyright)]
[assembly: AssemblyTrademark(KSP_AVC.LegalMamboJambo.Trademark)]

[assembly: ComVisible(false)]
[assembly: Guid("be1fd3ec-c1ba-47ec-9e16-f83da6fe87d5")]

[assembly: AssemblyVersion(KSP_AVC.Version.Number)]
[assembly: AssemblyFileVersion(KSP_AVC.Version.Number)]
[assembly: KSPAssembly("KSP_AVC", KSP_AVC.Version.major, KSP_AVC.Version.minor)]

[assembly: KSPAssemblyDependency("KSPe", 2, 4)]
[assembly: KSPAssemblyDependency("KSPe.UI", 2, 4)]
