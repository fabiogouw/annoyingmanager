using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace AnnoyingManager.Core.Repository
{
    public class Config
    {
        public Config()
        {
            Categories = new List<string>();
        }

        /// <summary>
        /// Time of the day when the count of tasks starts.
        /// </summary>
        [XmlIgnore]
        public TimeSpan StartupTime { get; set; }
        [XmlIgnore]
        public TimeSpan EndTime { get; set; }
        public int MaxLengthOfTaskInSeconds { get; set; }
        /// <summary>
        /// After a task ends, the user should user this time to rest before starting another activity.
        /// </summary>
        public int SiestaLengthInSeconds { get; set; }
        public bool StartsAtLogon { get; set; }
        /// <summary>
        /// Defines if the task form should be shown when the current task expires.
        /// </summary>
        public bool ShowTaskForm { get; set; }
        /// <summary>
        /// All categories that should appear for user choice.
        /// </summary>
        public List<string> Categories { get; set; }

        [Browsable(false)]
        [XmlElement(DataType = "duration", ElementName = "StartupTime")]
        public string StartupTimeString
        {
            get { return XmlConvert.ToString(StartupTime); }
            set { StartupTime = string.IsNullOrEmpty(value) ? TimeSpan.Zero : XmlConvert.ToTimeSpan(value); }
        }

        [Browsable(false)]
        [XmlElement(DataType = "duration", ElementName = "EndTime")]
        public string EndTimeString
        {
            get { return XmlConvert.ToString(EndTime); }
            set { EndTime = string.IsNullOrEmpty(value) ? TimeSpan.Zero : XmlConvert.ToTimeSpan(value); }
        }
    }
}
