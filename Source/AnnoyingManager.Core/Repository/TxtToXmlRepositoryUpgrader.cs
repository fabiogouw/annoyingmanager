using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace AnnoyingManager.Core.Repository
{
    internal class TxtToXmlRepositoryUpgrader
    {
        private static DateTime INITIAL_DATE = DateTime.Parse("2012-02-03");
        private TxtRepository _oldRepository = new TxtRepository();
        private XmlRepository _newRepository;
        private readonly string _directoryPath;
        private readonly string _upgradedFilesDirectoryPath;
        private static TxtToXmlRepositoryUpgrader _instance;
        private static object _lock = new object();
        private bool _executed = false;

        private TxtToXmlRepositoryUpgrader(XmlRepository newRepository)
        {
            _newRepository = newRepository;
            _directoryPath = string.Format(@"{0}\repository", AppDomain.CurrentDomain.BaseDirectory);
            _upgradedFilesDirectoryPath = string.Format(@"{0}\upgraded", _directoryPath);
        }

        public static TxtToXmlRepositoryUpgrader GetInstance(XmlRepository newRepository)
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new TxtToXmlRepositoryUpgrader(newRepository);
                }
                return _instance;
            }
        }

        public void RunInBackground()
        {
            lock (_lock)
            {
                if (!_executed)
                {
                    _executed = true;
                    ThreadPool.QueueUserWorkItem(Execute);
                }
            }
        }

        private void Execute(object state)
        {
            Thread.Sleep(5000);    // wait a little before starting the update process
            try
            {
                if (CheckExistenceOfFilesToBeUpgraded())
                {
                    CreateUpgradedDirectory();
                    DateTime currentDate = INITIAL_DATE;
                    while (currentDate <= DateTime.Today)
                    {
                        var tasks = _oldRepository.SearchTasks(currentDate, currentDate);
                        if(tasks.Count > 0)
                        {
                            UpgradeFile(currentDate, tasks);
                        }
                        currentDate = currentDate.AddDays(1);
                    }
                }
            }
            catch
            {
            }
        }

        private void CreateUpgradedDirectory()
        {
            if(!Directory.Exists(_upgradedFilesDirectoryPath))
            {
                Directory.CreateDirectory(_upgradedFilesDirectoryPath);
            }
        }

        private void UpgradeFile(DateTime date, List<Entities.Task> tasks)
        {
            _newRepository.SaveTasksForDay(tasks, date);
            string oldFilePath = string.Format(@"{0}\Tasks_{1:yyyyMMdd}.txt", _directoryPath, date);
            string newFilePath = string.Format(@"{0}\Tasks_{1:yyyyMMdd}.txt", _upgradedFilesDirectoryPath, date);
            File.Move(oldFilePath, newFilePath);
        }

        private bool CheckExistenceOfFilesToBeUpgraded()
        {
            var files = Directory.GetFiles(_directoryPath, "*.txt", SearchOption.TopDirectoryOnly);
            return files.Count() > 0;
        }
    }
}
