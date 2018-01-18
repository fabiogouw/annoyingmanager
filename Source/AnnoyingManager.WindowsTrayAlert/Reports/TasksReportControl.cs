using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AnnoyingManager.Core.DTO;
using Microsoft.Reporting.WinForms;
using AnnoyingManager.Core.Repository;

namespace AnnoyingManager.WindowsTrayAlert.Reports
{
    public partial class TasksReportControl : ReportControl
    {
        private ITaskRepository _repository;
        
        public TasksReportControl(ITaskRepository repository)
        {
            _repository = repository;
            InitializeComponent();
        }

        public override ReportViewer ApplyReport(ReportViewer viewer)
        {
            var tasks = _repository.SearchTasks(dtpFrom.Value, dtpTo.Value);
            var tasksForReport = TaskForReport.ConvertFromEntity(tasks);
            tasksForReport = ApplyFilter(tasksForReport);
            viewer.LocalReport.ReportEmbeddedResource = "AnnoyingManager.WindowsTrayAlert.Reports.Tasks.rdlc";
            viewer.LocalReport.DataSources.Add(new ReportDataSource("TaskDataSet", tasksForReport));
            return viewer;
        }

        public override string DisplayName
        {
            get { return "Tasks"; }
        }

        private List<TaskForReport> ApplyFilter(List<TaskForReport> tasksForReport)
        {
            tasksForReport = FilterBlankGroup(tasksForReport);
            tasksForReport = GroupTasks(tasksForReport);
            tasksForReport = SetTimeDescription(tasksForReport);
            return tasksForReport;
        }

        private List<TaskForReport> FilterBlankGroup(List<TaskForReport> tasks)
        {
            if (chkIgnoreBlank.Checked)
            {
                tasks = tasks.Where(t => !string.IsNullOrEmpty(t.Group)).ToList();
            }
            return tasks;
        }

        private List<TaskForReport> GroupTasks(List<TaskForReport> tasksForReport)
        {
            if (chkGroupSimilar.Checked)
            {
                tasksForReport = (from r in tasksForReport
                                  group r by new { r.TaskDate, r.Group, r.Category, r.ReferenceID, r.Description } into g
                                  orderby g.Key.TaskDate ascending
                                  select new TaskForReport()
                                  {
                                      TaskDate = g.Key.TaskDate,
                                      Category = g.Key.Category,
                                      Group = g.Key.Group,
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
