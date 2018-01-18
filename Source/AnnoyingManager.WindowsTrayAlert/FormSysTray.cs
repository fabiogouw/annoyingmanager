using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AnnoyingManager.Core;
using System.Runtime.InteropServices;
using System.Reflection;
using AnnoyingManager.Core.Repository;
using AnnoyingManager.Core.Contracts;
using AnnoyingManager.Core.Entities;
using AnnoyingManager.Core.Common;

namespace AnnoyingManager.WindowsTrayAlert
{
    public partial class FormSysTray : Form, ITaskSupplier
    {
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool DestroyIcon(IntPtr hIcon);

        private static object _lock = new object();
        private static int ICON_SIZE = 16;

        private Font _font = new Font("Courier New", 8);
        private Color _attentionColor = Color.Red;

        private FormTask _askingForm;
        private FormReports _formReports;
        private FormConfig _formConfig;
        private FormAbout _formAbout;

        private bool _askingTask = false;
        private Suggestion _lastSuggestion = new Suggestion();
        private Task _currentTask = null;

        private IConfigRepository _configRepository;

        public FormSysTray(IConfigRepository configRepository, 
            FormTask askingForm,
            FormReports formReports, 
            FormConfig formConfig,
            FormAbout formAbout)
        {
            _configRepository = configRepository;
            _askingForm = askingForm;
            _formReports = formReports;
            _formConfig = formConfig;
            _formAbout = formAbout;
            _askingForm.Exited += _askingForm_Exited;
            InitializeComponent();
            notifyIcon1.Text = Constants.APP_LABEL;
        }

        private void ExitApp()
        {
            notifyIcon1.Dispose();  // take the icon off from the taskbar
            Application.Exit();
        }

        private void ShowText(string text, Font font, Color foreColor)
        {
            ShowText(text, font, foreColor, Color.Black);
        }

        private void ShowText(string text, Font font, Color foreColor, Color backColor)
        {
            var foreBrush = new SolidBrush(foreColor);
            var backBrush = new SolidBrush(backColor);
            using (Bitmap bitmap = new Bitmap(ICON_SIZE, ICON_SIZE))
            {
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    graphics.FillRectangle(backBrush, 0, 0, ICON_SIZE, ICON_SIZE);
                    graphics.DrawString(text, _font, foreBrush, 0, 0);
                    ShowAsIcon(bitmap);
                }
            }
        }

        private void ShowIconGauge(double percentage, Color foreColor, Color backColor)
        {
            var foreBrush = new SolidBrush(foreColor);
            var backBrush = new SolidBrush(backColor);
            using (Bitmap bitmap = new Bitmap(ICON_SIZE, ICON_SIZE))
            {
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    graphics.FillRectangle(backBrush, 0, 0, ICON_SIZE, ICON_SIZE);  // draw background
                    graphics.FillRectangle(foreBrush, 0, 0, ICON_SIZE, Convert.ToInt32(ICON_SIZE * (1 - percentage)));  // draw foreground
                    ShowAsIcon(bitmap);
                }
            }
        }

        private void ShowAsIcon(Bitmap bitmap)
        {
            IntPtr hIcon = bitmap.GetHicon();
            Icon icon = (Icon)Icon.FromHandle(hIcon).Clone();
            DestroyIcon(hIcon);
            notifyIcon1.Icon = icon;
        }

        public void Initialize()
        {
            ShowText(":)", _font, Color.DarkRed);
        }

        public void AskForNewTask(Suggestion suggestion)
        {
            _lastSuggestion = suggestion;
            var config = _configRepository.GetConfig();
            if (config.ShowTaskForm)
            {
                AskForTask();
            }
        }

        private void AskForTask()
        {
            lock (_lock)
            {
                if (!_askingTask)   // to make sure that only one window will pop up
                {
                    _askingTask = true;
                    var act = new FormTask.ExecuteFormTask(_askingForm.Execute);
                    act.BeginInvoke(_lastSuggestion, new AsyncCallback(EndAskForNewTask), act);
                }
            }
        }

        private void EndAskForNewTask(IAsyncResult result)
        {
            lock (_lock)
            {
                var act = (FormTask.ExecuteFormTask)result.AsyncState;
                var taskProvided = act.EndInvoke(result);
                if (taskProvided != null)
                    _currentTask = taskProvided;
                _askingTask = false;    // ok, the task has been supplied, so we can let another one come in the future
            }
        }

        public Task GetTask()
        {
            lock (_lock)
            {
                if (_currentTask != null)
                {
                    var task = _currentTask;
                    _currentTask = null;
                    return task;
                }
                return null;
            }
        }

        public void UpdateStatus(Alert alert)
        {
            if (alert.AlertType == AlertType.AttentionPlease)
            {
                _attentionColor = _attentionColor == Color.Red ? Color.Fuchsia : Color.Red;
                ShowText(string.Empty, _font, _attentionColor, _attentionColor);
            }
            else if (alert.AlertType == AlertType.Resting)
            {
                if(alert.RemainingPercentage <= 0)
                {
                    ShowText("Zz", _font, Color.LightGray, Color.DarkBlue);
                }
                else
                {
                    ShowIconGauge(alert.RemainingPercentage, Color.LightBlue, Color.DarkBlue);
                }
            }
            else if (alert.AlertType == AlertType.WillNeedAttentionSoon)
            {
                ShowIconGauge(alert.RemainingTime.TotalSeconds / 60, Color.PeachPuff, Color.DarkOrange);
            }
            else
            {
                ShowIconGauge(alert.RemainingPercentage, Color.DarkOrange, Color.DarkGreen);
            }
        }

        public bool IsANewTaskAvailable()
        {
            lock (_lock)
            {
                return _currentTask != null;
            }
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            ExitApp();
        }

        private void mnuAddTask_Click(object sender, EventArgs e)
        {
            AskForTask();
        }

        private void mnuConfig_Click(object sender, EventArgs e)
        {
            _formConfig.Execute();
        }

        private void munAbout_Click(object sender, EventArgs e)
        {
            _formAbout.ShowDialog();
        }

        private void mnuReports_Click(object sender, EventArgs e)
        {
            _formReports.Execute();
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            AskForTask();
        }

        private void _askingForm_Exited(object sender, EventArgs e)
        {
            ExitApp();
        }
    }
}
