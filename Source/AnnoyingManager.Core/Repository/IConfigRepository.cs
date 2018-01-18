using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnnoyingManager.Core.Contracts;

namespace AnnoyingManager.Core.Repository
{
    public interface IConfigRepository : IReadOnlyConfigRepository
    {
        void SaveConfig(Config config);
    }
}
