using System;
using System.Text;
using System.Diagnostics;
using System.Windows.Controls;

namespace Tisinalite
{
    // Класс с глобальными переменными и функциями
    public class Global
    {
        // Строки с путями в файловой системе
        public static string TisinaliteDir = Environment.GetEnvironmentVariable("USERPROFILE") + @"\Documents\Tisinalite";
        public static string SettingsFile = "settings.xml";
        public static string NotesDir = TisinaliteDir + @"\Notes";
        // Фрейм, где происходит переключение всех окон
        public static Frame MainFrame { get; set; }
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
