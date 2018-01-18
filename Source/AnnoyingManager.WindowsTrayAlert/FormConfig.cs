using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AnnoyingManager.Core.Repository;

namespace AnnoyingManager.WindowsTrayAlert
{
    public partial class FormConfig : Form
    {
        private IConfigRepository _configRepository;

        public FormConfig(IConfigRepository configRepository)
        {
            _configRepository = configRepository;
            InitializeComponent();
            this.SetDefaults();
        }

        public void Execute()
        {
            ShowDialog();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            var config = new Config();
            config.StartupTime = TimeSpan.Parse(txtStartupTime.Text);
            config.EndTime = TimeSpan.Parse(txtEndTime.Text);
            config.MaxLengthOfTaskInSeconds = (int)TimeSpan.Parse(txtMaxTime.Text).TotalSeconds;
            config.SiestaLengthInSeconds = (int)TimeSpan.Parse(txtSiestaTime.Text).TotalSeconds;
            config.StartsAtLogon = chkStartAtLogon.Checked;
            config.ShowTaskForm = chkShowTaskForm.Checked;
            config.Categories.AddRange(txtCategories.Lines
                .Where(l => !string.IsNullOrEmpty(l))
                .Select(l => l.Trim())
                .Distinct());
            bool isValid = Validate(config);
            if (isValid)
            {
                _configRepository.SaveConfig(config);
                Close();
            }
        }

        private bool Validate(Config config)
        {
            errorProvider1.Clear();
            if (config.MaxLengthOfTaskInSeconds < (1 * 60) || config.MaxLengthOfTaskInSeconds > (120 * 60))
            {
                errorProvider1.SetError(txtMaxTime, "Invalid value for task length, it should be between 1 and 120 minutes.");
                return false;
            }
            return true;
        }

        private void FormConfig_Load(object sender, EventArgs e)
        {
            var config = _configRepository.GetConfig();
            txtStartupTime.Text = config.StartupTime.ToString();
            txtEndTime.Text = config.EndTime.ToString();
            txtMaxTime.Text = TimeSpan.FromSeconds(config.MaxLengthOfTaskInSeconds).ToString();
            txtSiestaTime.Text = TimeSpan.FromSeconds(config.SiestaLengthInSeconds).ToString();
            chkStartAtLogon.Checked = config.StartsAtLogon;
            chkShowTaskForm.Checked = config.ShowTaskForm;
            txtCategories.Lines = config.Categories.ToArray();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
