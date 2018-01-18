using AnnoyingManager.Core.Common;
using AnnoyingManager.Core.Contracts;
using AnnoyingManager.Core.Entities;
using AnnoyingManager.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnnoyingManager.Core.StateMachine
{
    /// <summary>
    /// The program has been supplied a task, and it will either check that task is over
    /// or just sleep for a while.
    /// </summary>
    public class ManagerStateWaiting : IManagerState
    {
        private readonly IReadOnlyConfigRepository _config;

        public ManagerStateWaiting(IReadOnlyConfigRepository config)
        {
            _config = config;
        }

        public StateType StateType
        {
            get { return StateType.Waiting; }
        }

        public StateContext Handle(StateContext context)
        {
            if (context.TasksOfTheDay.PastTasksInNeedOfSupply)
            {
                context.NewState = StateType.WithoutTask;   // older time spots need to be filled
                return context;
            }
            /*if (context.Config.SiestaLengthInSeconds > 0 && !context.Rested)
            {
                context.NewState = StateType.Siesta;
                return context;
            }*/
            if (context.TaskSupplier.IsANewTaskAvailable())
            {
                context.NewState = StateType.AskingTask;    // if there's already a new task in the supplier, let's go get it
                return context;
            }
            var task = context.LastTask;
            if (task == null || task.ExpectedEnd <= _config.GetCurrentDateTime())
            {
                context.NewState = StateType.WithoutTask;   // we ask for a new task if we don't have one now
                return context;
            }
            context = ManagerExpiredTask(context, task);
            return context;
        }

        private StateContext ManagerExpiredTask(StateContext context, Task currentTask)
        {
            var currentTime = context.CurrentDateTime.TimeOfDay;
            var expectedDuration = Math.Max(currentTask.ExpectedDurationInSeconds, context.Config.MaxLengthOfTaskInSeconds);
            var diffSeconds = currentTask.AssignedDate.TimeOfDay.TotalSeconds + expectedDuration - currentTime.TotalSeconds;
            if (diffSeconds <= 0)
            {
                // time is over, end task now!
                context.NewState = StateType.WithoutTask;
            }
            else if (diffSeconds < 60)
            {
                // alert for minute countdown
                context.TaskSupplier.UpdateStatus(new Alert()
                {
                    AlertType = AlertType.WillNeedAttentionSoon,
                    RemainingTime = new TimeSpan(0, 0, (int)diffSeconds),
                });
            }
            else
            {
                // update % only
                var percentage = diffSeconds / expectedDuration;
                context.TaskSupplier.UpdateStatus(new Alert()
                {
                    AlertType = AlertType.Ok,
                    RemainingPercentage = percentage
                });
            }
            return context;
        }
    }
}
