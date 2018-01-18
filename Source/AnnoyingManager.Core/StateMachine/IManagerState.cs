﻿using AnnoyingManager.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnnoyingManager.Core.StateMachine
{
    public interface IManagerState
    {
        StateType StateType { get; }
        StateContext Handle(StateContext context);
    }
}
