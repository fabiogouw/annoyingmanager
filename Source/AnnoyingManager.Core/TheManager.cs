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
        private ITaskRepository _taskRepository { get; set; }
        private ITaskSupplier _taskSupplier { get; set; }
        private IConfigRepository _configRepository { get; set; }

        private IManagerState _state = new ManagerStateWithoutTask();
        private DateTime _currentDate = DateTime.MinValue;
        private DiaryTasksList _tasksOfTheDay = null;
        private Timer _timer = null;
        private volatile bool _processing = false;

        public TheManager(ITaskRepository taskRepository, ITaskSupplier taskSupplier, IConfigRepository configRepository)
        {
            _taskRepository = taskRepository;
            _taskSupplier = taskSupplier;
            _configRepository = configRepository;
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
                _tasksOfTheDay.SaveAll();
        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (!_processing)
            {
                _processing = true;
                try
                {
                    var context = CreateContext();
                    _state.Handle(context);
                    ProcessContext(context);
                }
                finally
                {
                    _processing = false;
                }
            }
        }

        private StateContext CreateContext()
        {
            var config = _configRepository.GetConfig();
            return new StateContext()
            {
                Config = config,
                CurrentDateTime = _configRepository.GetCurrentDateTime(),
                TaskRepository = _taskRepository,
                TaskSupplier = _taskSupplier,
                TasksOfTheDay = _tasksOfTheDay,
                LastTask = _tasksOfTheDay.GetLast()
            };
        }

        private void ProcessContext(StateContext context)
        {
            if(context.MustInitializeListOfTasks)
                InitializeListOfTasks();
            if (context.NewTask != null)
                _tasksOfTheDay.Add(context.NewTask);
            if (context.NewState != null)
                _state = context.NewState;   // the most important piece of code of this method
        }

        private void InitializeListOfTasks()
        {
            var currentDate = _configRepository.GetCurrentDateTime();
            var savedTasksForThisDay = _taskRepository.GetCurrentTasks(currentDate);
            _tasksOfTheDay = DiaryTasksList.Create(savedTasksForThisDay, _configRepository);
            _tasksOfTheDay.OnTaskEnded += _tasksOfTheDay_OnTaskEnded;
        }

        private void _tasksOfTheDay_OnTaskEnded(Task task)
        {
            _taskRepository.AddTask(task);
        }
    }
}
