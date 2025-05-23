﻿/*
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

#endregion

namespace AVCToolkit.Json
{
    [AttributeUsage(AttributeTargets.Property)]
    public class JsonFieldAttribute : Attribute
    {
        #region Constructors

        public JsonFieldAttribute(string name)
        {
            this.Name = name;
        }

        #endregion

        #region Properties

        public string Name { get; private set; }

        public int Order { get; set; }

        #endregion
    }
}