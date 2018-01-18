using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AnnoyingManager.WindowsTrayAlert.Reports
{
    public class ReportControl : UserControl
    {
        public virtual ReportViewer ApplyReport(ReportViewer viewer)
        {
            return viewer;
        }

        public virtual string DisplayName
        {
            get { return "N/A"; }
        }
    }
}
