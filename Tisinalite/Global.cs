using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Tisinalite
{
    public class Global
    {
        public static string TisinaliteDir = Environment.GetEnvironmentVariable("USERPROFILE") + @"\Documents\Tisinalite";
        public static string SettingsFile = "settings.xml";
        public string NotesDir = TisinaliteDir + @"\Notes";
        public static string GetSettingsFile()
        {
            return TisinaliteDir + "\\" + SettingsFile;
        }
        public static string GetNoteFile(string note)
        {
            Debug.WriteLine(NotesDir + "\\" + note);
            return NotesDir + "\\" + note;
        }
    }
}
