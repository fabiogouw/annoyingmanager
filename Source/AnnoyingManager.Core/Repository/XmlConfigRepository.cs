using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;
using Microsoft.Win32;
using System.Reflection;

namespace AnnoyingManager.Core.Repository
{
    public class XmlConfigRepository : IConfigRepository 
    {
        private static readonly string DEFAUL_XML_CONFIG = @"<xml>
                                                                <config>
                                                                    <startupTime>08:00:00</startupTime>
                                                                    <endTime>18:00:00</endTime>
                                                                    <maxLenghtOfTaksInSeconds>1800</maxLenghtOfTaksInSeconds>
                                                                    <startsAtLogon>false</startsAtLogon>
                                                                    <showTaskForm>false</showTaskForm>
                                                                </config>
                                                            </xml>";
        
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
            lock (_lock)
            {
                if (_xml == null)
                {
                    if (File.Exists(_path))
                        _xml = XDocument.Load(_path);   // load the xml config file
                    else
                        _xml = XDocument.Parse(DEFAUL_XML_CONFIG);  // or create new from template
                }
                var config = new Config()
                {
                    StartupTime = TimeSpan.Parse(_xml.Root.Descendants("config").Elements("startupTime").Single().Value),
                    EndTime = TimeSpan.Parse(_xml.Root.Descendants("config").Elements("endTime").Single().Value),
                    MaxLenghtOfTaskInSeconds = int.Parse(_xml.Root.Descendants("config").Elements("maxLenghtOfTaksInSeconds").Single().Value),
                    StartsAtLogon = bool.Parse((_xml.Root.Descendants("config").Elements("startsAtLogon").SingleOrDefault() ?? new XElement("startsAtLogon", "false")).Value),
                    ShowTaskForm = bool.Parse((_xml.Root.Descendants("config").Elements("showTaskForm").SingleOrDefault() ?? new XElement("showTaskForm", "false")).Value)
                };
                return config;
            }
        }

        public void SaveConfig(Config config)
        {
            lock (_lock)
            {
                ConfigureToStartAtLogon(config.StartsAtLogon);
                SetConfigValue("startupTime", config.StartupTime.ToString());
                SetConfigValue("endTime", config.EndTime.ToString());
                SetConfigValue("maxLenghtOfTaksInSeconds", config.MaxLenghtOfTaskInSeconds.ToString());
                SetConfigValue("startsAtLogon", config.StartsAtLogon.ToString());
                SetConfigValue("showTaskForm", config.ShowTaskForm.ToString());
                _xml.Save(_path);
            }
        }

        private void SetConfigValue(string configName, string value)
        {
            var node = _xml.Root.Descendants("config").Elements(configName).SingleOrDefault();
            if (node == null)
            {
                node = new XElement(configName);
                _xml.Root.Element("config").Add(node);
            }
            node.Value = value;
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
