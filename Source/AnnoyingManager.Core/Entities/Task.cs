using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        }

        public string ID { get; set; }
        public string Category { get; set; }
        public string ReferenceID { get; set; }
        public string Description { get; set; }
        /// <summary>
        /// Date and time when the user provided the task
        /// </summary>
        public DateTime AssignedDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ExpectedDurationInSeconds { get; set; }
        public DateTime TimeElapsed
        {
            get { return StartDate.Date.Add(EndDate.Subtract(StartDate)); }
        }
    }
}
