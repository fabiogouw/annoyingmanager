using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace AnnoyingManager.Core.Entities
{
    [Serializable]
    public class Task
    {
        public Task()
        {
            ID = Guid.NewGuid().ToString();
            AssignedDate = DateTime.Now;
            StartDate = DateTime.MinValue;
            EndDate = DateTime.MinValue;
            Pending = true;
        }

        public string ID { get; set; }
        public string Category { get; set; }
        public string Group { get; set; }
        public string ReferenceID { get; set; }
        public string Description { get; set; }
        /// <summary>
        /// Date and time when the user provided the task
        /// </summary>
        public DateTime AssignedDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [XmlIgnore]
        public int ExpectedDurationInSeconds { get; set; }
        [XmlIgnore]
        public DateTime ExpectedEnd
        {
            get { return StartDate.AddSeconds(ExpectedDurationInSeconds); }
        }
        [XmlIgnore]
        public DateTime TimeElapsed
        {
            get { return StartDate.Date.Add(EndDate.Subtract(StartDate)); }
        }
        /// <summary>
        /// Tells if the task has been already saved.
        /// </summary>
        [XmlIgnore]
        public bool Pending { get; set; }
    }
}
