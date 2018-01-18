using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnnoyingManager.Core.Sources
{
    public interface ITaskSource
    {
        string FriendlyName { get; }
        string InternalId { get; }
        
    }
}
