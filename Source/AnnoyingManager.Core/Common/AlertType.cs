using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnnoyingManager.Core.Common
{
    public enum AlertType : int
    {
        Ok = 1,
        WillNeedAttentionSoon = 2,
        AttentionPlease = 3,
        Resting = 4
    }
}
