using System;
using System.Collections.Generic;
using System.Text;
using f0t0vi3w3r.Properties;
using f0t0vi3w3r.ProgramSettings;

namespace f0t0vi3w3r.Tools
{
    internal class ExtensionHandler
    {
        private static string extensions;

        static ExtensionHandler()
        {
            extensions = Settings.Default.Extensions;
            Settings.Default.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(Default_PropertyChanged);
        }

        static void Default_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "Extensions")
                extensions = Settings.Default.Extensions;
        }

        internal static string GetDialogFormat()
        {
            string result = "All Files|*.*";
            if (extensions != null)
            {
                string[] exts = extensions.Split('/');
                string extList = string.Empty;
                foreach (string ext in exts)
                {
                    result = string.Format("{0}S|{1}|{2}", ext.TrimStart('*', '.').ToUpper().Replace(";*.", "S or ") , ext, result);
                    extList += string.Format("{0};", ext);
                }
                result = string.Format("All Images|{0}|{1}", extList, result);
            }
            return result;
        }


        internal static string[] GetSearchOptionFormat()
        {
            List<string> result = new List<string>();
            if (extensions != null)
            {
                string[] exts = extensions.Split('/');
                foreach (string ext in exts)
                { 
                    string[] extx = ext.Split(';');
                    foreach (string e in extx)
                    {
                        result.Add(e);
                    }
                }
            }
            return result.ToArray();
        }

        
    }
}
