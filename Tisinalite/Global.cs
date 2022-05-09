using System;
using System.Text;
using System.Diagnostics;
using System.Windows.Controls;

namespace Tisinalite
{
    public class Global
    {
        public static string TisinaliteDir = Environment.GetEnvironmentVariable("USERPROFILE") + @"\Documents\Tisinalite";
        public static string SettingsFile = "settings.xml";
        public static string NotesDir = TisinaliteDir + @"\Notes";
        public static Frame MainFrame;
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
