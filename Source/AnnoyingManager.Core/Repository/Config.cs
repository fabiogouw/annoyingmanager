using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnnoyingManager.Core.Repository
{
    public class Config
    {
        /// <summary>
        /// Time of the day when the count of tasks starts.
        /// </summary>
        public TimeSpan StartupTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int MaxLenghtOfTaskInSeconds { get; set; }
        public bool StartsAtLogon { get; set; }
        public bool ShowTaskForm { get; set; }
    }
}
