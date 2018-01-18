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
        #region Dependencies

        public IConfigRepository ConfigRepository { get; set; }

        #endregion

        public FormConfig()
        {
            InitializeComponent();
            this.SetDefaults();
        }

        public static void Execute()
        {
            using (var form = new FormConfig())
            {
                form.ConfigRepository = new XmlConfigRepository();
                form.ShowDialog();
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            var config = new Config();
            config.StartupTime = TimeSpan.Parse(txtStartupTime.Text);
            config.EndTime = TimeSpan.Parse(txtEndTime.Text);
            config.MaxLenghtOfTaskInSeconds = (int)TimeSpan.Parse(txtMaxTime.Text).TotalSeconds;
            config.StartsAtLogon = chkStartAtLogon.Checked;
            config.ShowTaskForm = chkShowTaskForm.Checked;
            ConfigRepository.SaveConfig(config);
            this.Close();
        }

        private void FormConfig_Load(object sender, EventArgs e)
        {
            var config = ConfigRepository.GetConfig();
            txtStartupTime.Text = config.StartupTime.ToString();
            txtEndTime.Text = config.EndTime.ToString();
            txtMaxTime.Text = TimeSpan.FromSeconds(config.MaxLenghtOfTaskInSeconds).ToString();
            chkStartAtLogon.Checked = config.StartsAtLogon;
            chkShowTaskForm.Checked = config.ShowTaskForm;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
