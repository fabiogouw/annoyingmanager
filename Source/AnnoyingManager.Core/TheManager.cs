using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using AnnoyingManager.Core.StateMachine;
using AnnoyingManager.Core.Repository;
using AnnoyingManager.Core.Entities;
using AnnoyingManager.Core.Contracts;

namespace AnnoyingManager.Core
{
    /// <summary>
    /// Manages the logic such asking for task information to the user and storing it in the repository.
    /// </summary>
    public class TheManager
    {
        private ITaskRepository _taskRepository;
        private ITaskSupplier _taskSupplier;
        private IConfigRepository _configRepository;
        private IManagerStateFactory _managerStateFactory;

        private IManagerState _state = new ManagerStateWithoutTask();
        private DateTime _currentDate = DateTime.MinValue;
        private DiaryTasksList _tasksOfTheDay = null;
        private Timer _timer = null;
        private volatile bool _processing = false;
        private volatile bool _savingToRepository = false;
        private StateContext _context = new StateContext();

        public TheManager(ITaskRepository taskRepository, 
            ITaskSupplier taskSupplier, 
            IConfigRepository configRepository,
            IManagerStateFactory managerStateFactory)
        {
            _taskRepository = taskRepository;
            _taskSupplier = taskSupplier;
            _configRepository = configRepository;
            _managerStateFactory = managerStateFactory;
        }

        public void Initialize()
        {
            _taskSupplier.Initialize();
            InitializeListOfTasks();
            _timer = new Timer(500);   // executes every 1/2 second
            _timer.Elapsed += new ElapsedEventHandler(_timer_Elapsed);
            _timer.Start();           
        }

        public void Application_ApplicationExit(object sender, EventArgs e)
        {
            if (_tasksOfTheDay != null)
            {
                _tasksOfTheDay.SaveAll();
                SaveTasks(_tasksOfTheDay);
            }
        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (!_processing)
            {
                _processing = true;
                try
                {
                    _context = EnrichContext(_context);
                    _context = _state.Handle(_context);
                    ProcessContext(_context);
                    SaveTasks(_context.TasksOfTheDay);
                }
                finally
                {
                    _processing = false;
                }
            }
        }

        private StateContext EnrichContext(StateContext context)
        {
            var config = _configRepository.GetConfig();
            context.Config = config;
            context.CurrentDateTime = _configRepository.GetCurrentDateTime();
            context.TaskSupplier = _taskSupplier;
            context.TasksOfTheDay = _tasksOfTheDay;
            context.LastTask = _tasksOfTheDay.GetLast();
            return context;
        }

        private void ProcessContext(StateContext context)
        {
            if (_tasksOfTheDay == null || _tasksOfTheDay.Count == 0)
            {
                InitializeListOfTasks();
            }
            if (context.NewTask != null)
            {
                _tasksOfTheDay.Add(context.NewTask);
                context.NewTask = null; // clear the task so it won't be collected again
            }
            if (context.NewState.HasValue)
            {
                _state = _managerStateFactory.GetState(context.NewState.Value);   // the most important piece of code of this method
            }
        }

        private void SaveTasks(IDiaryTasksList taskList)
        {
            // avois saving errors while looping
            if (!_savingToRepository)
            {
                _savingToRepository = true;
                try
                {
                    var tasksToBeSaved = taskList.GetPendingTasks();
                    if (tasksToBeSaved.Count() > 0)
                    {
                        _taskRepository.SaveTasks(tasksToBeSaved);
                        taskList.SetAllSaved();
                    }
                }
                finally
                {
                    _savingToRepository = false;
                }
            }
        }

        private void InitializeListOfTasks()
        {
            var currentDate = _configRepository.GetCurrentDateTime();
            var savedTasksForThisDay = _taskRepository.GetCurrentTasks(currentDate);
            _tasksOfTheDay = DiaryTasksList.Create(savedTasksForThisDay, _configRepository);
        }
    }
}
