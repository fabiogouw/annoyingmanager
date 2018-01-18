using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;
using Microsoft.Win32;
using System.Reflection;
using System.Xml.Serialization;

namespace AnnoyingManager.Core.Repository
{
    public class XmlConfigRepository : IConfigRepository 
    {
        private XmlSerializer _serializer = new XmlSerializer(typeof(Config));       
        private string _path = string.Empty;
        private static object _lock = new object();
        private static XDocument _xml = null;

        public XmlConfigRepository()
        {
            string directoryPath = string.Format(@"{0}\config", AppDomain.CurrentDomain.BaseDirectory);
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);
            _path = Path.Combine(directoryPath, "config.xml");
        }

        public Config GetConfig()
        {
            var config = new Config();
            lock (_lock)
            {
                if (_xml == null)
                {
                    if (File.Exists(_path))
                    {
                        using (var fileStream = new FileStream(_path, FileMode.Open))
                        {
                            config = (Config)_serializer.Deserialize(fileStream);
                        }
                    }
                    else
                    {
                        // defaults
                        config.StartupTime = new TimeSpan(8, 0, 0);
                        config.EndTime = new TimeSpan(18, 0, 0);
                        config.MaxLengthOfTaskInSeconds = 1800;
                        config.SiestaLengthInSeconds = 0;
                        config.StartsAtLogon = false;
                        config.ShowTaskForm = false;
                    }
                }
                config.SiestaLengthInSeconds = 0;   // TODO: verify this later
                return config;
            }
        }

        public void SaveConfig(Config config)
        {
            lock (_lock)
            {
                using (var sw = new StreamWriter(_path, false))
                {
                    _serializer.Serialize(sw, config);
                    sw.Close();
                }
                ConfigureToStartAtLogon(config.StartsAtLogon);
            }
        }

        public DateTime GetCurrentDateTime()
        {
            return DateTime.Now;
        }

        private void ConfigureToStartAtLogon(bool enabled)
        {
            using (var key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true))
            {
                if (enabled)
                    key.SetValue("Annoying Manager", Assembly.GetEntryAssembly().Location);
                else
                    if(key.GetValue("Annoying Manager") != null)
                        key.DeleteValue("Annoying Manager");
                key.Close();
            }
        }
    }
}
