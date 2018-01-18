using AnnoyingManager.Core.Contracts;
using AnnoyingManager.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnnoyingManager.Core.Entities
{
    public class DiaryTasksList : List<Task>, IDiaryTasksList
    {
        private IReadOnlyConfigRepository _configRepository;

        private DiaryTasksList()
        {
        }

        public static DiaryTasksList Create(List<Task> list, IReadOnlyConfigRepository configRepository)
        {
            var newList = new DiaryTasksList();
            newList.AddRange(list);
            newList._configRepository = configRepository;
            return newList;
        }

        public bool PastTasksInNeedOfSupply
        {
            get
            {
                var lastTask = GetLast();
                if (lastTask == null)
                {
                    return true;
                }
                var config = _configRepository.GetConfig();
                var expectedEndTime = CalculateExpectedEndTime(lastTask, config);
                var now = _configRepository.GetCurrentDateTime().TimeOfDay;
                if (expectedEndTime < now)
                {
                    return true;
                }
                return false;
            }
        }

        private TimeSpan CalculateExpectedEndTime(Task lastTask, Repository.Config config)
        {
            int taskLength = Math.Max(config.MaxLengthOfTaskInSeconds, lastTask.ExpectedDurationInSeconds);
            var expectedEndTime = lastTask.StartDate.TimeOfDay.Add(TimeSpan.FromSeconds(taskLength));
            return expectedEndTime;
        }

        public Task GetLast()
        {
            return this.OrderBy(t => t.StartDate)
                .LastOrDefault();
        }

        public new void Add(Task task)
        {
            var config = _configRepository.GetConfig();
            var currentDate = _configRepository.GetCurrentDateTime();
            if (Count == 0)
                AddFirstTaskOfTheDay(task, config, currentDate);
            else
            {
                var lastTask = GetLast();
                if (lastTask.EndDate == DateTime.MinValue)
                {
                    var expectedEndTime = CalculateExpectedEndTime(lastTask, config);
                    var expectedEnd = lastTask.StartDate.Date.Add(expectedEndTime);
                    task.StartDate = task.AssignedDate < expectedEnd ? task.AssignedDate : expectedEnd;
                    lastTask.EndDate = task.StartDate;
                    lastTask.Pending = true;
                }
                else
                {
                    // the end time of the last task is known when it has been ended by save all (closing app)
                    task.StartDate = lastTask.EndDate;
                }
            }
            base.Add(task);
        }

        private void AddFirstTaskOfTheDay(Task task, Config config, DateTime currentDate)
        {
            task.StartDate = currentDate.Date;
            task.StartDate = task.StartDate.Add(config.StartupTime);
        }

        public void SaveAll()
        {
            var lastTask = GetLast();
            if (lastTask != null)
            {
                if (lastTask.EndDate == DateTime.MinValue)
                {
                    lastTask.EndDate = _configRepository.GetCurrentDateTime();
                    lastTask.Pending = true;
                }
            }
        }

        public IEnumerable<Task> GetPendingTasks()
        {
            return this.Where(t => t.Pending);
        }

        public void SetAllSaved()
        {
            ForEach(t => t.Pending = false);
        }
    }
}
