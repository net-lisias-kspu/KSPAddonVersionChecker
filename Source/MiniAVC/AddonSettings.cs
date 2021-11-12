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
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

namespace MiniAVC
{
    public class AddonSettings
    {
        private bool firstRun = true;

        public AddonSettings() { }

        public AddonSettings(string fileName)
        {
            FileName = fileName;
        }

        public bool AllowCheck { get; set; }

        [XmlIgnore]
        public string FileName { get; set; }

        public bool FirstRun
        {
            get { return firstRun; }
            set { firstRun = value; }
        }

        public List<string> IgnoredUpdates { get; set; } = new List<string>();

        public static AddonSettings Load(string rootPath)
        {
            var filePath = Path.Combine(rootPath, "MiniAVC.xml");

            if (!File.Exists(filePath))
            {
                return new AddonSettings(filePath);
            }

            AddonSettings settings;
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                settings = new XmlSerializer(typeof(AddonSettings)).Deserialize(stream) as AddonSettings;
                settings.FileName = filePath;
                stream.Close();
            }
            return settings;
        }

        public void Save()
        {
            using (var stream = new FileStream(FileName, FileMode.Create))
            {
                new XmlSerializer(typeof(AddonSettings)).Serialize(stream, this);
                stream.Close();
            }
        }
    }
}