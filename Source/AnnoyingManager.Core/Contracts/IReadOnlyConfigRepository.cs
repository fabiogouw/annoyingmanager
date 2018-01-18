using AnnoyingManager.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnnoyingManager.Core.Contracts
{
    public interface IReadOnlyConfigRepository
    {
        Config GetConfig();
        DateTime GetCurrentDateTime();
    }
}
