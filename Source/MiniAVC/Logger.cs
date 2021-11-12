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
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

using UnityEngine;

#endregion

namespace MiniAVC
{
    [KSPAddon(KSPAddon.Startup.Instantly, false)]
    public class Logger : MonoBehaviour
    {
        #region Fields

        private static readonly AssemblyName assemblyName;
        private static readonly string fileName;

        private static readonly List<string[]> messages = new List<string[]>();

        #endregion

        #region Constructors

        static Logger()
        {
            assemblyName = Assembly.GetExecutingAssembly().GetName();
            fileName = assemblyName.Name + ".log";
            File.Delete(fileName);

            messages.Add(new[] {"Executing: " + assemblyName.Name + " - " + assemblyName.Version});
            messages.Add(new[] {"Assembly: " + Assembly.GetExecutingAssembly().Location});
            Blank();
        }

        #endregion

        #region Destructors

        ~Logger()
        {
            Flush();
        }

        #endregion

        #region Methods: public

        public static void Blank()
        {
            lock (messages)
            {
                messages.Add(new string[] {});
            }
        }

        public static void Error(string message)
        {
            lock (messages)
            {
                messages.Add(new[] {"Error " + DateTime.Now.TimeOfDay, message});
            }
        }

        public static void Exception(Exception ex)
        {
            lock (messages)
            {
                messages.Add(new[] {"Exception " + DateTime.Now.TimeOfDay, ex.ToString()});
                Blank();
            }
        }

        public static void Exception(Exception ex, string location)
        {
            lock (messages)
            {
                messages.Add(new[] {"Exception " + DateTime.Now.TimeOfDay, location + " // " + ex});
                Blank();
            }
        }

        public static void Flush()
        {
            lock (messages)
            {
                if (messages.Count == 0)
                {
                    return;
                }

                using (var file = File.AppendText(fileName))
                {
                    foreach (var message in messages)
                    {
                        file.WriteLine(message.Length > 0 ? message.Length > 1 ? "[" + message[0] + "]: " + message[1] : message[0] : string.Empty);
                        if (message.Length > 0)
                        {
                            print(message.Length > 1 ? assemblyName.Name + " -> " + message[1] : assemblyName.Name + " -> " + message[0]);
                        }
                    }
                }
                messages.Clear();
            }
        }

        public static void Log(object obj)
        {
            lock (messages)
            {
                try
                {
                    if (obj is IEnumerable)
                    {
                        messages.Add(new[] {"Log " + DateTime.Now.TimeOfDay, obj.ToString()});
                        foreach (var o in obj as IEnumerable)
                        {
                            messages.Add(new[] {"\t", o.ToString()});
                        }
                    }
                    else
                    {
                        messages.Add(new[] {"Log " + DateTime.Now.TimeOfDay, obj.ToString()});
                    }
                }
                catch (Exception ex)
                {
                    Exception(ex);
                }
            }
        }

        public static void Log(string name, object obj)
        {
            lock (messages)
            {
                try
                {
                    if (obj is IEnumerable)
                    {
                        messages.Add(new[] {"Log " + DateTime.Now.TimeOfDay, name});
                        foreach (var o in obj as IEnumerable)
                        {
                            messages.Add(new[] {"\t", o.ToString()});
                        }
                    }
                    else
                    {
                        messages.Add(new[] {"Log " + DateTime.Now.TimeOfDay, obj.ToString()});
                    }
                }
                catch (Exception ex)
                {
                    Exception(ex);
                }
            }
        }

        public static void Log(string message)
        {
            lock (messages)
            {
                messages.Add(new[] {"Log " + DateTime.Now.TimeOfDay, message});
            }
        }

        public static void Warning(string message)
        {
            lock (messages)
            {
                messages.Add(new[] {"Warning " + DateTime.Now.TimeOfDay, message});
            }
        }

        #endregion

        #region Methods: protected

        protected void Awake()
        {
            DontDestroyOnLoad(this);
        }

        protected void LateUpdate()
        {
            Flush();
        }

        protected void OnDestroy()
        {
            Flush();
        }

        #endregion
    }
}