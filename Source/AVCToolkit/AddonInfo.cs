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
    public class AddonInfo
    {
        #region Fields

        private GitHubInfo gitHub = new GitHubInfo();

        #endregion

        #region Properties

        [JsonField("CHANGE_LOG", Order = 4)]
        public string ChangeLog { get; set; }

        [JsonField("CHANGE_LOG_URL", Order = 5)]
        public string ChangeLogUrl { get; set; }

        [JsonField("DOWNLOAD", Order = 3)]
        public string Download { get; set; }

        [JsonField("GITHUB", Order = 6)]
        public GitHubInfo GitHub
        {
            get { return this.gitHub; }
            set { this.gitHub = value; }
        }

        [JsonField("KSP_VERSION", Order = 8)]
        public VersionInfo KspVersion { get; set; }

        [JsonField("KSP_VERSION_MAX", Order = 10)]
        public VersionInfo KspVersionMax { get; set; }

        [JsonField("KSP_VERSION_MIN", Order = 9)]
        public VersionInfo KspVersionMin { get; set; }

        [JsonField("NAME", Order = 1)]
        public string Name { get; set; }

        [JsonField("URL", Order = 2)]
        public string Url { get; set; }

        [JsonField("VERSION", Order = 7)]
        public VersionInfo Version { get; set; }

        #endregion
    }
}