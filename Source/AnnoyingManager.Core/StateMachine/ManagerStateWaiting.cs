using AnnoyingManager.Core.Common;
using AnnoyingManager.Core.Entities;
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
        public void Handle(StateContext context)
        {
            var task = context.LastTask;
            if (context.TaskSupplier.IsANewTaskAvailable())
            {
                context.NewState = new ManagerStateAskingTask();    // if there's already a new task in the supplier, let's go get it
                return;
            }
            if (task == null)
            {
                context.NewState = new ManagerStateWithoutTask();   // we ask for a new task if we don't have one now
                return;
            }
            if (context.TasksOfTheDay.PastTasksInNeedOfSupply)
            {
                context.NewState = new ManagerStateWithoutTask();   // older time spots need to be filled
                return;
            }
            ManagerExpiredTask(context, task);
        }

        private void ManagerExpiredTask(StateContext context, Task currentTask)
        {
            var currentTime = context.CurrentDateTime.TimeOfDay;
            var expectedDuration = Math.Max(currentTask.ExpectedDurationInSeconds, context.Config.MaxLenghtOfTaskInSeconds);
            var diffSeconds = currentTask.AssignedDate.TimeOfDay.TotalSeconds + expectedDuration - currentTime.TotalSeconds;
            if (diffSeconds <= 0)
            {
                // time is over, end task now!
                context.NewState = new ManagerStateWithoutTask();
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
        }
    }
}
