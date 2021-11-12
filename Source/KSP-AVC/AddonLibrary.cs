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
using System.Reflection;
using System.Threading;

#endregion

namespace KSP_AVC
{
    public static class AddonLibrary
    {
        #region Fields

        private static readonly string rootPath;
        private static List<Addon> addons;

        #endregion

        #region Constructors

        static AddonLibrary()
        {
            rootPath = GetRootPath();
            Logger.Log("Checking Root: " + rootPath);
            ThreadPool.QueueUserWorkItem(ProcessAddonPopulation);
        }

        #endregion

        #region Properties

        public static IEnumerable<Addon> Addons
        {
            get { return (addons != null) ? addons.Where(a => !a.HasError).ToList() : null; }
        }

        public static bool Populated { get; private set; }

        public static int ProcessCount
        {
            get { return (addons != null) ? addons.Count(a => a.IsProcessingComplete) : 0; }
        }

        public static IEnumerable<Addon> Processed
        {
            get { return (addons != null) ? addons.Where(a => !a.HasError && a.IsProcessingComplete) : null; }
        }

        public static bool ProcessingComplete
        {
            get { return addons != null && addons.All(a => a.IsProcessingComplete); }
        }

        public static int TotalCount
        {
            get { return (addons != null) ? addons.Count : 0; }
        }

        #endregion

        #region Methods: private

        private static string GetRootPath()
        {
            var rootPath = Assembly.GetExecutingAssembly().Location;
            var gameDataIndex = rootPath.IndexOf("GameData", StringComparison.CurrentCultureIgnoreCase);
            return Path.Combine(rootPath.Remove(gameDataIndex, rootPath.Length - gameDataIndex), "GameData");
        }

        private static void ProcessAddonPopulation(object state)
        {
            try
            {
                Populated = false;
                addons = Directory.GetFiles(rootPath, "*.version", SearchOption.AllDirectories).Select(path => new Addon(path)).ToList();
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