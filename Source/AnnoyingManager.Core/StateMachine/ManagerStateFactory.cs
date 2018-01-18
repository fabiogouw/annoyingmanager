using AnnoyingManager.Core.Common;
using AnnoyingManager.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnnoyingManager.Core.StateMachine
{
    public class ManagerStateFactory : IManagerStateFactory
    {
        public IManagerState GetState(StateType stateType)
        {
            switch (stateType)
            {
                case StateType.AskingTask:
                    return new ManagerStateAskingTask();
                case StateType.RestingTime:
                    return new ManagerStateRestingTime();
                case StateType.Siesta:
                    // TODO: verify this later
                    throw new NotImplementedException();
                    //return new ManagerStateSiesta(new XmlConfigRepository());
                case StateType.Waiting:
                    return new ManagerStateWaiting(new XmlConfigRepository());
                case StateType.WithoutTask:
                    return new ManagerStateWithoutTask();
            }
            throw new ArgumentException(string.Format("Creation of {0} is not available.", stateType));
        }
    }
}
