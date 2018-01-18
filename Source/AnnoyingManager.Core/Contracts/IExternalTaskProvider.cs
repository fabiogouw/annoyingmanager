using AnnoyingManager.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnnoyingManager.Core.Contracts
{
    public interface IExternalTaskProvider
    {
        Task GetExternalTask();
    }
}
