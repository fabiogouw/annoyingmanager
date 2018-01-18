using AnnoyingManager.Core.Common;
using AnnoyingManager.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnnoyingManager.Core.StateMachine
{
    public class ManagerStateAskingTask : IManagerState
    {
        public void Handle(StateContext context)
        {
            var task = context.TaskSupplier.GetTask();  // try to get the task from the supplier
            if (task != null)
            {
                context.NewTask = task;
                context.NewState = new ManagerStateWaiting();
            }
            else
            {
                Suggestion suggestion = new Suggestion()
                {
                    LenghtOfTaskInSeconds = context.Config.MaxLenghtOfTaskInSeconds,
                    Categories = (from t in context.TasksOfTheDay
                                  orderby t.Category ascending
                                  select t.Category).Distinct().ToList(),
                    SuggedtedTasks = (from t in context.TasksOfTheDay
                                      group t by new { t.Category, t.ReferenceID, t.Description } into g
                                      orderby g.Count() descending
                                      select new Task()
                                      {
                                          Category = g.Key.Category,
                                          ReferenceID = g.Key.ReferenceID,
                                          Description = g.Key.Description
                                      }).ToList()
                };
                var lastTask = context.TasksOfTheDay.GetLast();
                if (lastTask != null)
                {
                    int expectedDuration = Math.Max(lastTask.ExpectedDurationInSeconds, context.Config.MaxLenghtOfTaskInSeconds);
                    var expectedEnd = lastTask.StartDate.AddSeconds(expectedDuration);
                    suggestion.Message = string.Format("Last task expired at {0:HH:mm}", expectedEnd);
                }
                context.TaskSupplier.AskForNewTask(suggestion);
            }
            context.TaskSupplier.UpdateStatus(new Alert() { AlertType = AlertType.AttentionPlease });
        }
    }
}
