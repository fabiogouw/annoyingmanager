using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnnoyingManager.Core.Entities
{
    [Serializable]
    public class Suggestion
    {
        public Suggestion()
        {
            SuggedtedTasks = new List<Task>();
            Groups = new List<string>();
            LenghtOfTaskInSeconds = 5 * 60;
        }

        /// <summary>
        /// Holds the tasks that manager will give to the task supplier choose.
        /// </summary>
        public List<Task> SuggedtedTasks { get; set; }
        /// <summary>
        /// Groups used in the last tasks.
        /// </summary>
        public List<string> Groups { get; set; }
        public string Message { get; set; }
        public int LenghtOfTaskInSeconds { get; set; }
    }
}
