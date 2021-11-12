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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

#endregion

namespace MiniAVC
{
    public static class AddonLibrary
    {
        #region Fields

        private static List<Addon> addons;

        #endregion

        #region Constructors

        static AddonLibrary()
        {
            ThreadPool.QueueUserWorkItem(ProcessAddonPopulation);
        }

        #endregion

        #region Properties

        public static List<Addon> Addons
        {
            get { return (addons != null) ? addons.Where(a => !a.HasError).ToList() : null; }
        }

        public static List<Addon> AddonsProcessed
        {
            get { return (addons != null) ? addons.Where(a => !a.HasError && a.IsProcessingComplete).ToList() : null; }
        }

        public static bool Populated { get; private set; }

        public static List<AddonSettings> Settings { get; private set; }

        public static int TotalCount
        {
            get { return (addons != null) ? addons.Count : 0; }
        }

        #endregion

        #region Methods: public

        public static void Remove(Addon addon)
        {
            if (addons == null)
            {
                return;
            }

            addons.Remove(addon);
        }

        #endregion

        #region Methods: private

        private static void ProcessAddonPopulation(object state)
        {
            try
            {
                var threadAddons = new List<Addon>();
                var threadSettings = new List<AddonSettings>();
                foreach (var rootPath in AssemblyLoader.loadedAssemblies.Where(a => a.name == "MiniAVC").Select(a => Path.GetDirectoryName(a.path)))
                {
                    var settings = AddonSettings.Load(rootPath);
                    threadSettings.Add(settings);
                    threadAddons.AddRange(Directory.GetFiles(rootPath, "*.version", SearchOption.AllDirectories).Select(p => new Addon(p, settings)).ToList());
                }
                addons = threadAddons;
                Settings = threadSettings;
                Populated = true;
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
            }
        }

        #endregion
    }
}