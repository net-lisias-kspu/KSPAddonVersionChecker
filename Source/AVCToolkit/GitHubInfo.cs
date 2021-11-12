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

using AVCToolkit.Json;

#endregion

namespace AVCToolkit
{
    public class GitHubInfo
    {
        #region Properties

        [JsonField("ALLOW_PRE_RELEASE", Order = 3)]
        public bool? AllowPreRelease { get; set; }

        [JsonField("REPOSITORY", Order = 2)]
        public string Repository { get; set; }

        [JsonField("USERNAME", Order = 1)]
        public string Username { get; set; }

        #endregion
    }
}