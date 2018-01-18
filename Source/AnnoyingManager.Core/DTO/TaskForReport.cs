using AnnoyingManager.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnnoyingManager.Core.DTO
{
    [Serializable]
    public class TaskForReport
    {
        public string Category { get; set; }
        public string ReferenceID { get; set; }
        public string Description { get; set; }
        public DateTime TaskDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; } 
        public TimeSpan TimeElapsed { get; set; }
        public string TimeDescription { get; set; }
        public bool HasBeenGrouped { get; set; }

        public static List<TaskForReport> ConvertFromEntity(List<Task> tasks)
        {
            return tasks.Select(t => new TaskForReport()
            {
                Category = t.Category,
                ReferenceID = t.ReferenceID,
                Description = t.Description,
                TaskDate = t.StartDate.Date,
                StartDate = t.StartDate,
                EndDate = t.EndDate,
                TimeElapsed = t.EndDate.Subtract(t.StartDate),
                HasBeenGrouped = false
            }).ToList();
        }
    }
}
