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
    public partial class App : Application
    {
        private void App_Startup(object sender, StartupEventArgs e)
        {
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                if (!Directory.Exists(Global.NotesDir))
                {
                    Debug.WriteLine("А где");
                    Debug.WriteLine(Global.NotesDir);
                    Directory.CreateDirectory(Global.NotesDir);
                    string f = "Welcome";
                    File.Copy(Path.Combine("Resources", f), Path.Combine(Global.NotesDir, f));
                    Debug.WriteLine(Path.Combine(Global.NotesDir, f));
                    Settings settings = Settings.GetSettings();

                    settings.OpenNote = f;
                    settings.Save();
                    
                }
            }
        }
    }
}
