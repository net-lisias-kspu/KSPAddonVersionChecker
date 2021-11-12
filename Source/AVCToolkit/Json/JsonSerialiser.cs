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

using AVCToolkit.Json;

#endregion

namespace AVCToolkit
{
    public static class JsonSerialiser
    {
        #region Methods: public

        public static string Serialise(object obj)
        {
            var jsonObject = new JsonObject(obj);
            var json = "{";

            var first = true;
            foreach (var field in jsonObject.Fields)
            {
                var jsonField = SerialiseField(field);
                if (jsonField == String.Empty)
                {
                    continue;
                }
                if (!first)
                {
                    json += ",";
                }
                json += jsonField;
                first = false;
            }

            return json + "}";
        }

        #endregion

        #region Methods: private

        private static bool IsPrimitive(object value)
        {
            return value is byte ||
                   value is sbyte ||
                   value is short ||
                   value is ushort ||
                   value is int ||
                   value is uint ||
                   value is long ||
                   value is ulong ||
                   value is float ||
                   value is double ||
                   value is decimal ||
                   value is bool;
        }

        private static string SerialiseField(KeyValuePair<string, object> field)
        {
            if (field.Value == null)
            {
                return String.Empty;
            }

            if (IsPrimitive(field.Value))
            {
                return "\"" + field.Key + "\":" + field.Value;
            }

            var jsonObject = new JsonObject(field.Value);
            if (jsonObject.Count > 0)
            {
                if (jsonObject.HasVisibleFields)
                {
                    return "\"" + field.Key + "\":" + Serialise(field.Value);
                }
                return String.Empty;
            }

            return "\"" + field.Key + "\":\"" + field.Value + "\"";
        }

        #endregion
    }
}