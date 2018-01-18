using AnnoyingManager.Core.Common;
using AnnoyingManager.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnnoyingManager.Core.StateMachine
{
    /// <summary>
    /// State when the program knows it should not ask for new tasks because
    /// the user isn't in his working time.
    /// </summary>
    public class ManagerStateRestingTime : IManagerState
    {
        private bool _hasBeenWarned = false;

        public StateType StateType
        {
            get { return StateType.RestingTime; }
        }

        public StateContext Handle(StateContext context)
        {
            var currentTime = context.CurrentDateTime;
            if (currentTime.TimeOfDay >= context.Config.StartupTime && currentTime.TimeOfDay <= context.Config.EndTime)
            {
                context.NewState = StateType.WithoutTask;
            }
            if (!_hasBeenWarned)
            {
                context.TaskSupplier.UpdateStatus(new Alert() { AlertType = AlertType.Resting });
                _hasBeenWarned = true;
            }
            return context;
        }
    }
}
