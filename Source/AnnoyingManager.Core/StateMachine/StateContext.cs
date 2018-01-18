using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnnoyingManager.Core.Repository;
using AnnoyingManager.Core.Entities;
using AnnoyingManager.Core.Contracts;

namespace AnnoyingManager.Core.StateMachine
{
    /// <summary>
    /// Holds the information passed by the Manager class to the state classes. This will hold all
    /// references needed to the state classes do their work.
    /// </summary>
    public class StateContext
    {
        // input ones
        public ITaskRepository TaskRepository { get; set; }
        public ITaskSupplier TaskSupplier { get; set; }
        public DiaryTasksList TasksOfTheDay { get; set; }
        public Config Config { get; set; }
        public DateTime CurrentDateTime { get; set; }
        public Task LastTask { get; set; }
        // output ones
        public IManagerState NewState { get; set; }
        public bool MustInitializeListOfTasks { get; set; }
        public Task NewTask { get; set; }
    }
}
