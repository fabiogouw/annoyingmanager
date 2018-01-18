using AnnoyingManager.Core.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using System.Linq;

namespace AnnoyingManager.Core.Repository
{
    public class XmlRepository : ITaskRepository
    {
        private string _directoryPath;
        private XmlSerializer _serializer = new XmlSerializer(typeof(List<Task>));
        private readonly TxtToXmlRepositoryUpgrader _updater;

        public XmlRepository()
        {
            _updater = TxtToXmlRepositoryUpgrader.GetInstance(this);
            _updater.RunInBackground();
        }

        public List<Task> GetCurrentTasks(DateTime currentDate)
        {
            return GetTasksFromPeriod(currentDate, currentDate);
        }

        private void AddTask(Task task)
        {
            string fileName = GetTodaysFilePath();
            var list = GetList(fileName);
            list.Add(task);
            SaveList(list, fileName);
        }

        public List<Task> SearchTasks(DateTime start, DateTime finish)
        {
            return GetTasksFromPeriod(start, finish);
        }

        public List<Task> GetTasksFromPeriod(DateTime start, DateTime finish)
        {
            var tasks = new List<Task>();
            var files = GetFilePathsForAPeriodOfTime(start, finish);
            foreach (var file in files)
            {
                tasks.AddRange(GetList(file));
            }
            return tasks;
        }

        public void Update(Task task)
        {
            string fileName = GetTodaysFilePath();
            var list = GetList(fileName);
            var taskToUpdate = list.SingleOrDefault(t => t.ID == task.ID);
            if (taskToUpdate != null)
            {
                taskToUpdate.EndDate = task.EndDate;
                SaveList(list, fileName);
            }
        }

        private List<Task> GetList(string file)
        {
            if (File.Exists(file))
            {
                using (var fileStream = new FileStream(file, FileMode.Open))
                {
                    var list = (List<Task>)_serializer.Deserialize(fileStream);
                    list.ForEach(t => t.Pending = false);
                    return list;
                }
            }
            return new List<Task>();
        }

        private void SaveList(List<Task> list, string file)
        {
            using (var sw = new StreamWriter(file, false))
            {
                _serializer.Serialize(sw, list);
                sw.Close();
            }
        }

        private string[] GetFilePathsForAPeriodOfTime(DateTime start, DateTime end)
        {
            var files = new List<string>();
            while (start.Date <= end.Date)
            {
                string path = GetFilePathForADay(start);
                if (File.Exists(path))
                    files.Add(path);
                start = start.AddDays(1);
            }
            return files.ToArray();
        }

        private string GetTodaysFilePath()
        {
            return GetFilePathForADay(DateTime.Today);
        }

        private string GetFilePathForADay(DateTime date)
        {
            string directoryPath = GetRepositoryDirectory();
            string fileName = string.Format(@"{0}\Tasks_{1:yyyyMMdd}.xml", directoryPath, date);
            return fileName;
        }

        private string GetRepositoryDirectory()
        {
            if (string.IsNullOrEmpty(_directoryPath))
            {
                _directoryPath = string.Format(@"{0}\repository", AppDomain.CurrentDomain.BaseDirectory);
                if (!Directory.Exists(_directoryPath))
                {
                    Directory.CreateDirectory(_directoryPath);
                }
            }
            return _directoryPath;
        }

        public void SaveTasks(IEnumerable<Task> taskToBeSaved)
        {
            SaveTasksForDay(taskToBeSaved, DateTime.Today);
        }

        public void SaveTasksForDay(IEnumerable<Task> taskToBeSaved, DateTime date)
        {
            string fileName = GetFilePathForADay(date);
            var list = GetList(fileName);
            foreach (var task in taskToBeSaved)
            {
                var existingTask = list.SingleOrDefault(t => t.ID == task.ID);
                if (existingTask == null)
                {
                    list.Add(task);
                }
                else
                {
                    existingTask.EndDate = task.EndDate;
                }
            }
            SaveList(list, fileName);
        }
    }
}
