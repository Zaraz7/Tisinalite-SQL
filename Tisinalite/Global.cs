using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tisinalite
{
    public class Global
    {
        public static string NotesDir = Environment.GetEnvironmentVariable("USERPROFILE") + @"\Documents\Tisinalite";
        public static string SettingsFile = "settings.xml";
        public static string GetSettingsFile()
        {
            return NotesDir + "\\" + SettingsFile;
        }
    }
}
