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
using System.Globalization;
using System.IO;

#endregion

namespace AVCToolkit
{
    internal class Program
    {
        #region Fields

        private static readonly AddonInfo addon = new AddonInfo();
        private static string output;

        #endregion

        #region Methods: private

        private static void Main(string[] args)
        {
            for (var i = 0; i < args.Length; i++)
            {
                switch (args[i].ToLower(CultureInfo.InvariantCulture))
                {
                    case "-name":
                        if (i++ < args.Length)
                        {
                            addon.Name = args[i];
                        }
                        break;

                    case "-url":
                        if (i++ < args.Length)
                        {
                            addon.Url = args[i];
                        }
                        break;

                    case "-download":
                        if (i++ < args.Length)
                        {
                            addon.Download = args[i];
                        }
                        break;

                    case "-change_log":
                        if (i++ < args.Length)
                        {
                            addon.ChangeLog = args[i];
                        }
                        break;

                    case "-change_log_url":
                        if (i++ < args.Length)
                        {
                            addon.ChangeLogUrl = args[i];
                        }
                        break;

                    case "-github.username":
                        if (i++ < args.Length)
                        {
                            addon.GitHub.Username = args[i];
                        }
                        break;

                    case "-github.repository":
                        if (i++ < args.Length)
                        {
                            addon.GitHub.Repository = args[i];
                        }
                        break;

                    case "-github.allow_pre_release":
                        if (i++ < args.Length)
                        {
                            addon.GitHub.AllowPreRelease = Boolean.Parse(args[i]);
                        }
                        break;

                    case "-version":
                        if (i++ < args.Length)
                        {
                            addon.Version = new VersionInfo(args[i]);
                        }
                        break;

                    case "-ksp_version":
                        if (i++ < args.Length)
                        {
                            addon.KspVersion = new VersionInfo(args[i]);
                        }
                        break;

                    case "-ksp_version_min":
                        if (i++ < args.Length)
                        {
                            addon.KspVersionMin = new VersionInfo(args[i]);
                        }
                        break;

                    case "-ksp_version_max":
                        if (i++ < args.Length)
                        {
                            addon.KspVersionMax = new VersionInfo(args[i]);
                        }
                        break;

                    case "-output":
                        if (i++ < args.Length)
                        {
                            output = args[i];
                        }
                        break;
                }
            }

            if (!String.IsNullOrEmpty(output))
            {
                using (var stream = new StreamWriter(output))
                {
                    stream.Write(JsonSerialiser.Serialise(addon));
                }
            }

            Console.WriteLine(JsonSerialiser.Serialise(addon));
        }

        #endregion
    }
}