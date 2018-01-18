using AnnoyingManager.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnnoyingManager.Core.Entities
{
    [Serializable]
    public class Alert
    {
        public AlertType AlertType { get; set; }
        public TimeSpan RemainingTime { get; set; }
        public double RemainingPercentage { get; set; }
    }
}
