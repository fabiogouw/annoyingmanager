using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AnnoyingManager.Core;
using AnnoyingManager.Core.Common;

namespace AnnoyingManager.WindowsTrayAlert
{
    public static class UIUtil
    {
        public static void SetDefaults(this Form form)
        {
            form.Text = Constants.APP_LABEL;
        }
    }
}
