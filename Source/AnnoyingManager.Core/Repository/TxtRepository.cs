using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using AnnoyingManager.Core.Entities;

namespace AnnoyingManager.Core.Repository
{
    public class TxtRepository : ITaskRepository
    {
        private string _directoryPath;

        public List<Task> GetCurrentTasks(DateTime currentDate)
        {
            return GetTasksFromPeriod(currentDate, currentDate);
        }

        public void AddTask(Task task)
        {
            string fileName = GetTodaysFilePath();
            using(var sw = new StreamWriter(fileName, true))
            {
                sw.WriteLine("{0}|{1}|{2}|{3}|{4}|{5}|{6}", task.ID, task.ReferenceID, task.Category, task.Description, task.AssignedDate, task.StartDate, task.EndDate);
                sw.Close();
            }
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
                using (var sr = new StreamReader(file))
                {
                    tasks.AddRange(GetList(sr));
                }
            }
            return tasks;
        }

        private List<Task> GetList(StreamReader sr)
        {
            var list = new List<Task>();
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                if(!string.IsNullOrEmpty(line))
                    list.Add(ReadTask(line));
            }
            return list;
        }

        private Task ReadTask(string line)
        {
            var tokens = line.Split('|');
            var task = new Task()
            {
                ID = tokens[0],
                ReferenceID = tokens[1],
                Category = tokens[2],
                Description = tokens[3],
                AssignedDate = DateTime.Parse(tokens[4]),
                StartDate = DateTime.Parse(tokens[5]),
                EndDate = DateTime.Parse(tokens[6])
            };
            return task;
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
            string fileName = string.Format(@"{0}\Tasks_{1:yyyyMMdd}.txt", directoryPath, date);
            return fileName;
        }

        private string GetRepositoryDirectory()
        {
            if (string.IsNullOrEmpty(_directoryPath))
            {
                _directoryPath = string.Format(@"{0}\repository", AppDomain.CurrentDomain.BaseDirectory);
                if (!Directory.Exists(_directoryPath))
                    Directory.CreateDirectory(_directoryPath);
            }
            return _directoryPath;
        }

        public void SaveTasks(IEnumerable<Task> taskToBeSaved)
        {
            throw new NotImplementedException();
        }
    }
}
