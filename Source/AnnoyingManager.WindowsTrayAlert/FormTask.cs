using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AnnoyingManager.Core;
using AnnoyingManager.Core.Entities;
using AnnoyingManager.Core.Common;
using AnnoyingManager.Core.Repository;
using AnnoyingManager.Core.Contracts;

namespace AnnoyingManager.WindowsTrayAlert
{
    public partial class FormTask : Form
    {
        private IConfigRepository _configRepository;
        private FormConfig _formConfig;
        private FormReports _formReports;
        private FormAbout _formAbout;

        public FormTask(IConfigRepository configRepository,
            FormConfig formConfig,
            FormReports formReports,
            FormAbout formAbout)
        {
            _configRepository = configRepository;
            _formConfig = formConfig;
            _formReports = formReports;
            _formAbout = formAbout;
            InitializeComponent();
            this.SetDefaults();
        }

        /// <summary>
        /// Holds the task provided by the user, whatever way he chose (new task or repeating another one).
        /// </summary>
        private Task _resultTask = new Task();

        /// <summary>
        /// Holds the last task supplied by the user, so the choices he had made can be restored to the window controls next time.
        /// </summary>
        private Task _lastTaskUsed = null;

        public delegate Task ExecuteFormTask(Suggestion suggestion);

        public event EventHandler Exited;

        protected virtual void OnExited(EventArgs e)
        {
            if (Exited != null)
            {
                Exited(this, e);
            }
        }

        /// <summary>
        /// Shows the form so the user can provide the task she's doing right now.
        /// </summary>
        /// <param name="suggestion"></param>
        /// <returns></returns>
        public Task Execute(Suggestion suggestion)
        {
            _resultTask = null;
            InitializeSuggestion(suggestion);
            TopMost = true;
            ShowDialog();
            return _resultTask;
        }

        private void InitializeSuggestion(Suggestion suggestion)
        {
            SetStatusText(suggestion);
            SetDurationValue(suggestion);
            ChangeDurationLabel(trkDuration.Value);
            InitializeCategoriesControl();
            InitializeGroupControl(suggestion);
            InitializeSuggestionPanel(suggestion);
        }

        private void SetDurationValue(Suggestion suggestion)
        {
            int lenghtOfTaskInMinutes = suggestion.LenghtOfTaskInSeconds / 60;
            trkDuration.Value = Math.Min(trkDuration.Maximum, lenghtOfTaskInMinutes);
        }

        private void SetStatusText(Suggestion suggestion)
        {
            lblStatus.Text = suggestion.Message;
        }

        private void InitializeCategoriesControl()
        {
            var categories = _configRepository.GetConfig().Categories;
            ddlCategories = SetValuesForComboBox(ddlCategories, categories, _lastTaskUsed != null ? _lastTaskUsed.Category : string.Empty);
        }

        private void InitializeGroupControl(Suggestion suggestion)
        {
            ddlGroups = SetValuesForComboBox(ddlGroups, suggestion.Groups, _lastTaskUsed != null ? _lastTaskUsed.Group : string.Empty);
        }

        private ComboBox SetValuesForComboBox(ComboBox comboBox, List<string> values, string lastUsedValue)
        {
            values = values.Where(v => !string.IsNullOrEmpty(v)).ToList();
            comboBox.DataSource = values;
            if (_lastTaskUsed != null)
            {
                comboBox.SelectedIndex = values.IndexOf(lastUsedValue);
            }
            else
            {
                comboBox.SelectedIndex = -1;
            }
            return comboBox;
        }

        private void InitializeSuggestionPanel(Suggestion suggestion)
        {
            int top = 0;
            pnlLastTasks.Controls.Clear();  // removes the controls showed in the last view of this window
            foreach (var task in suggestion.SuggedtedTasks)
            {
                var button = new Button()
                {
                    Tag = task, // very important, holds the information of the suggested task
                    Text = string.Format("{0} {1} - ({2}){3}", task.Category, task.Group, task.ReferenceID, task.Description),
                    AutoSizeMode = AutoSizeMode.GrowAndShrink,
                    AutoSize = true,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Dock = DockStyle.Top
                };
                tipTasks.SetToolTip(button, task.Description);
                button.Click += new EventHandler(btnSuggested_Click);   // bind the control to the delegate used to re-assign the task
                pnlLastTasks.Controls.Add(button);
                top = button.Top + button.Height;   // update the Top value so buttons are put each one above the last one
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            var newTask = new Task() 
            {
                Category = ddlCategories.Text, 
                Group = ddlGroups.Text,
                ReferenceID = txtReference.Text, 
                Description = txtDescription.Text 
            };
            SetCurrentTask(newTask);
            Close();
        }

        private void btnSuggested_Click(object sender, EventArgs e)
        {
            var suggestedTask = (Task)(sender as Button).Tag;
            var newTask = new Task()
            {
                Category = suggestedTask.Category,
                ReferenceID = suggestedTask.ReferenceID,
                Description = suggestedTask.Description
            };
            SetSuggestionAsCurrentFormValues(newTask);
            SetCurrentTask(newTask);
            Close();
        }

        private void SetSuggestionAsCurrentFormValues(Task newTask)
        {
            if (!string.IsNullOrEmpty(newTask.Category))
            {
                ddlCategories.Text = newTask.Category;
            }
            ddlGroups.Text = newTask.Group;
            txtReference.Text = newTask.ReferenceID;
            txtDescription.Text = newTask.Description;
        }

        private void SetCurrentTask(Task task)
        {
            task.ExpectedDurationInSeconds = trkDuration.Value * 60;
            _resultTask = task;
            _lastTaskUsed = _resultTask;
        }

        private void btnSuggestedTasks_Click(object sender, EventArgs e)
        {
            IExternalTaskProvider provider = new FormExternalTaskProvider();
            var task = provider.GetExternalTask();
            SetSuggestionAsCurrentFormValues(task);
        }

        private void txt_Enter(object sender, EventArgs e)
        {
            // select the text when the textbox is focused
            var txt = sender as TextBox;
            if (!string.IsNullOrEmpty(txt.Text))
            {
                txt.SelectionStart = 0;
                txt.SelectionLength = txt.Text.Length;
            }
        }

        private void trkDuration_ValueChanged(object sender, EventArgs e)
        {
            ChangeDurationLabel(trkDuration.Value);
        }

        private void ChangeDurationLabel(int value)
        {
            int minutes = value / 60;
            int seconds = value % 60;
            lblDuration.Text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnExited(e);
        }

        private void configurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _formConfig.Execute();
        }

        private void reportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _formReports.Execute();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _formAbout.ShowDialog();
        }
    }
}
