using AnnoyingManager.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnnoyingManager.Core.StateMachine
{
    public interface IManagerStateFactory
    {
        IManagerState GetState(StateType stateType);
    }
}
