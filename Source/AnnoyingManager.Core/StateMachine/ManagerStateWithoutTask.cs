using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnnoyingManager.Core.StateMachine
{
    /// <summary>
    /// State that the program knows it doesn't have a current task and needs to
    /// decide if it should ask for a new one or if should sleep.
    /// </summary>
    public class ManagerStateWithoutTask : IManagerState 
    {
        public void Handle(StateContext context)
        {
            var currentTime = context.CurrentDateTime;
            if (currentTime.TimeOfDay < context.Config.StartupTime || currentTime.TimeOfDay > context.Config.EndTime)
                context.NewState = new ManagerStateRestingTime();
            else
                context.NewState = new ManagerStateAskingTask();
        }
    }
}
