using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using AnnoyingManager.Core.Repository;
using AnnoyingManager.Core;
using AnnoyingManager.Core.Entities;
using AnnoyingManager.Core.DTO;
using AnnoyingManager.WindowsTrayAlert.Reports;
using Ninject;
using System.Reflection;
using AnnoyingManager.Core.Common;

namespace AnnoyingManager.WindowsTrayAlert
{
    public partial class FormReports : Form
    {
        private ITaskRepository _repository;
        private ReportControl _currentReport;

        public FormReports(ITaskRepository repository, ReportControl[] reportControls)
        {
            _repository = repository;
            InitializeComponent();
            cboReports.DisplayMember = "DisplayName";
            cboReports.DataSource = reportControls;
        }

        public void Execute()
        {
            if (CheckIfSystemHasReportingServicesInstalled())
                ShowDialog();
            else
                MessageBox.Show(Properties.Resources.ReportingServices10NotInstalled, Constants.APP_LABEL, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private bool CheckIfSystemHasReportingServicesInstalled()
        {
            try
            {
                Assembly.Load("Microsoft.ReportViewer.WinForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91");
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void FormReports_Load(object sender, EventArgs e)
        {
            this.SetDefaults();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            GenerateReport();
        }

        private void GenerateReport()
        {
            if (_currentReport != null)
            {
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1 = _currentReport.ApplyReport(reportViewer1);
                reportViewer1.RefreshReport();
            }
        }

        private void cboReports_SelectedIndexChanged(object sender, EventArgs e)
        {
            _currentReport = (ReportControl)cboReports.SelectedItem;
            if (_currentReport != null)
            {
                grpParameters.Controls.Add(_currentReport);
                _currentReport.Location = new Point(5, 10);
            }
        }
    }
}
