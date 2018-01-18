using AnnoyingManager.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnnoyingManager.Core.Repository
{
    public interface ITaskRepository
    {
        List<Task> GetCurrentTasks(DateTime currentDate);
        void AddTask(Task task);
        List<Task> SearchTasks(DateTime start, DateTime finish);
    }
}
