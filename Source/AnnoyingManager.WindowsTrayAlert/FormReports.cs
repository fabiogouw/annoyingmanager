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

namespace AnnoyingManager.WindowsTrayAlert
{
    public partial class FormReports : Form
    {
        public FormReports()
        {
            InitializeComponent();
        }

        private void FormReports_Load(object sender, EventArgs e)
        {
            this.SetDefaults();
            GenerateReport();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            GenerateReport();
        }

        private void GenerateReport()
        {
            this.reportViewer1.LocalReport.DataSources.Clear();
            ITaskRepository repository = new TxtRepository();
            var tasks = repository.SearchTasks(dtpFrom.Value, dtpTo.Value);
            var tasksForReport = TaskForReport.ConvertFromEntity(tasks);
            tasksForReport = ApplyFilter(tasksForReport);
            this.reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("TaskDataSet", tasksForReport));
            this.reportViewer1.RefreshReport();
        }

        private List<TaskForReport> ApplyFilter(List<TaskForReport> tasksForReport)
        {
            tasksForReport = FilterBlankCategory(tasksForReport);
            tasksForReport = GroupTasks(tasksForReport);
            tasksForReport = SetTimeDescription(tasksForReport);
            return tasksForReport;
        }

        private List<TaskForReport> FilterBlankCategory(List<TaskForReport> tasks)
        {
            if (chkIgnoreBlank.Checked)
            {
                tasks = tasks.Where(t => !string.IsNullOrEmpty(t.Category)).ToList();
            }
            return tasks;
        }

        private List<TaskForReport> GroupTasks(List<TaskForReport> tasksForReport)
        {
            if (chkGroupSimilar.Checked)
            {
                tasksForReport = (from r in tasksForReport
                                  group r by new { r.TaskDate, r.Category, r.ReferenceID, r.Description } into g
                                  orderby g.Key.TaskDate ascending
                                  select new TaskForReport()
                                  {
                                      TaskDate = g.Key.TaskDate,
                                      Category = g.Key.Category,
                                      ReferenceID = g.Key.ReferenceID,
                                      Description = g.Key.Description,
                                      TimeElapsed = TimeSpan.FromMinutes(g.Sum(t => t.TimeElapsed.TotalMinutes)),
                                      HasBeenGrouped = true
                                  }).ToList();
            }
            return tasksForReport;
        }

        private List<TaskForReport> SetTimeDescription(List<TaskForReport> tasksForReport)
        {
            foreach (var task in tasksForReport)
            {
                if (task.HasBeenGrouped)
                    task.TimeDescription = task.TimeElapsed.ToString(@"hh\:mm");
                else
                {
                    var timeElapsed = task.EndDate.Subtract(task.StartDate);
                    task.TimeDescription = string.Format("{0:hh\\:mm} ({1:hh\\:mm} - {2:hh\\:mm})", timeElapsed, task.StartDate, task.EndDate);
                }
            }
            return tasksForReport;
        }
    }
}
