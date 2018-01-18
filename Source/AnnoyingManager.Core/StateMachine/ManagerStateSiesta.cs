using AnnoyingManager.Core.Common;
using AnnoyingManager.Core.Contracts;
using AnnoyingManager.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnnoyingManager.Core.StateMachine
{
    // TODO: End this class later

    /// <summary>
    /// A task just has finished and the application is configured to wait for a little time.
    /// The user should use this time to rest, like in pomodoro technique
    /// </summary>
    public class ManagerStateSiesta : IManagerState
    {
        private DateTime _siestaStart;
        private readonly IReadOnlyConfigRepository _config;

        public ManagerStateSiesta(IReadOnlyConfigRepository config)
        {
            _config = config;
            _siestaStart = _config.GetCurrentDateTime();
        }

        public StateType StateType
        {
            get { return StateType.Siesta; }
        }

        public StateContext Handle(StateContext context)
        {
            var currentTime = _config.GetCurrentDateTime();
            var restedTime = currentTime.Subtract(_siestaStart);
            if(restedTime.TotalSeconds >= context.Config.SiestaLengthInSeconds)
            {
                context.Rested = true;
                context.NewState = StateType.Waiting;
            }
            else
            {
                var percentage = restedTime.TotalSeconds / context.Config.SiestaLengthInSeconds;
                context.TaskSupplier.UpdateStatus(new Alert()
                {
                    AlertType = AlertType.Resting,
                    RemainingPercentage = percentage
                });
            }
            return context;
        }
    }
}
