using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AnnoyingManager.Core;
using AnnoyingManager.Core.Common;

namespace AnnoyingManager.WindowsTrayAlert
{
    public partial class FormAbout : Form
    {
        public FormAbout()
        {
            InitializeComponent();
            this.SetDefaults();
        }

        private void FormAbout_Load(object sender, EventArgs e)
        {
            label1.Text = Constants.APP_LABEL;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string targetURL = @"http://annoyingmanager.codeplex.com";
            System.Diagnostics.Process.Start(targetURL);
        }
    }
}
