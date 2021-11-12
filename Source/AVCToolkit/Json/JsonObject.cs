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

using System.Collections.Generic;
using System.Linq;

#endregion

namespace AVCToolkit.Json
{
    public class JsonObject
    {
        #region Fields

        private readonly Dictionary<string, object> fields = new Dictionary<string, object>();

        #endregion

        #region Constructors

        public JsonObject(object obj)
        {
            foreach (var property in obj.GetType().GetProperties().Where(p => p.IsDefined(typeof(JsonFieldAttribute), true)).ToList().OrderBy(p => (p.GetCustomAttributes(typeof(JsonFieldAttribute), true).First() as JsonFieldAttribute).Order))
            {
                var name = (property.GetCustomAttributes(typeof(JsonFieldAttribute), true).First() as JsonFieldAttribute).Name;
                var value = property.GetValue(obj, null);

                this.fields.Add(name, value);
            }
        }

        #endregion

        #region Properties

        public int Count
        {
            get { return this.fields.Count; }
        }

        public Dictionary<string, object> Fields
        {
            get { return this.fields; }
        }

        public bool HasVisibleFields
        {
            get { return this.fields.Any(f => f.Value != null); }
        }

        #endregion
    }
}