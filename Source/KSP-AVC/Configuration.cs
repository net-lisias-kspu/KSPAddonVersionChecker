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
using System.Xml.Serialization;

#endregion

namespace KSP_AVC
{
    public class Configuration
    {
        #region Fields

        private static readonly string fileName = Path.ChangeExtension(Assembly.GetExecutingAssembly().Location, "xml");

        #endregion

        #region Constructors

        static Configuration()
        {
            Instance = new Configuration
            {
                FirstRun = true,
                Version = Assembly.GetExecutingAssembly().GetName().Version.ToString()
            };
            Load();
        }

        #endregion

        #region Properties

        public static Configuration Instance { get; private set; }

        public bool FirstRun { get; set; }

        public string Version { get; set; }

        #endregion

        #region Methods: public

        public static bool GetFirstRun()
        {
            return Instance.FirstRun;
        }

        public static string GetVersion()
        {
            return Instance.Version;
        }

        public static void Load()
        {
            try
            {
                if (!File.Exists(fileName))
                {
                    return;
                }

                using (var stream = new FileStream(fileName, FileMode.Open))
                {
                    var xml = new XmlSerializer(Instance.GetType());
                    Instance = xml.Deserialize(stream) as Configuration;
                    stream.Close();
                }
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
            }
        }

        public static void Save()
        {
            try
            {
                using (var stream = new FileStream(fileName, FileMode.Create))
                {
                    var xml = new XmlSerializer(Instance.GetType());
                    xml.Serialize(stream, Instance);
                    stream.Close();
                }
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
            }
        }

        public static void SetFirstRun(bool value)
        {
            Instance.FirstRun = value;
            Save();
        }

        public static void SetVersion(string value)
        {
            Instance.Version = value;
            Save();
        }

        #endregion
    }
}