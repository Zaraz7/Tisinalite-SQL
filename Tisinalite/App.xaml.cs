using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Diagnostics;

namespace Tisinalite
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public string NotesDir;
        private void App_Startup(object sender, StartupEventArgs e)
        {
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                //Debug.WriteLine( "\n\n" + Directory.Exists(Environment.GetEnvironmentVariable("USERPROFILE") + @"\Documents"));
                NotesDir = Environment.GetEnvironmentVariable("USERPROFILE") + @"\Documents\Tisinalite";
                if (!Directory.Exists(NotesDir))
                {
                    Debug.WriteLine("А где");
                    Directory.CreateDirectory(NotesDir);
                    string f = "Welcome.txt";
                    File.Copy(Path.Combine("Resources", f), Path.Combine(NotesDir, f));
                }
            }
        }
    }
}
