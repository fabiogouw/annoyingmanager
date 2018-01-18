using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnnoyingManager.Core.Entities
{
    public interface IDiaryTasksList : IList<Task>
    {
        Task GetLast();
        void SaveAll();
        bool PastTasksInNeedOfSupply { get; }
        IEnumerable<Task> GetPendingTasks();
        void SetAllSaved();
    }
}
