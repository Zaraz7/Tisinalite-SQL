using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace Tisinalite
{
    public class Settings
    {
        public static Settings GetSettings()
        {
            Settings settings = null;
            string filename = Global.GetSettingsFile();
            if (File.Exists(filename))
            {
                using (FileStream fs = new FileStream(filename, FileMode.Open))
                {
                    XmlSerializer xser = new XmlSerializer(typeof(Settings));
                    settings = (Settings)xser.Deserialize(fs);
                    fs.Close();
                }
            }
            else
                settings = new Settings();
            return settings;
        }
        public void Save()
        {
            string filename = Global.GetSettingsFile();
            if (File.Exists(filename))
                File.Delete(filename);
            using (FileStream fs = new FileStream(filename, FileMode.Create)) 
            { 
                XmlSerializer xser = new XmlSerializer(typeof(Settings));
                xser.Serialize(fs, this);
                fs.Close();
            }
        }
        
        public string OpenNote { get; set; }
    }
}
