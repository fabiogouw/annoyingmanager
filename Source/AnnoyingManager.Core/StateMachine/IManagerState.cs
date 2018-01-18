using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnnoyingManager.Core.StateMachine
{
    public interface IManagerState
    {
        void Handle(StateContext context);
    }
}
