using AnnoyingManager.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnnoyingManager.Core.Contracts
{
    /// <summary>
    /// Tells how the information of the tasks will be passed to the tasks manager class.
    /// </summary>
    public interface ITaskSupplier
    {
        void Initialize();
        void AskForNewTask(Suggestion suggestion);
        void UpdateStatus(Alert alert);
        Task GetTask();
        bool IsANewTaskAvailable();
    }
}
