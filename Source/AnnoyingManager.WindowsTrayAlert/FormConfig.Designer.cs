﻿namespace AnnoyingManager.WindowsTrayAlert
{
    partial class FormConfig
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormConfig));
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkShowTaskForm = new System.Windows.Forms.CheckBox();
            this.chkStartAtLogon = new System.Windows.Forms.CheckBox();
            this.txtStartupTime = new System.Windows.Forms.MaskedTextBox();
            this.txtMaxTime = new System.Windows.Forms.MaskedTextBox();
            this.txtEndTime = new System.Windows.Forms.MaskedTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(123, 244);
            this.btnOk.Margin = new System.Windows.Forms.Padding(4);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(100, 28);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(245, 244);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 28);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkShowTaskForm);
            this.groupBox1.Controls.Add(this.chkStartAtLogon);
            this.groupBox1.Controls.Add(this.txtStartupTime);
            this.groupBox1.Controls.Add(this.txtMaxTime);
            this.groupBox1.Controls.Add(this.txtEndTime);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(16, 15);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(436, 206);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Configuration";
            // 
            // chkShowTaskForm
            // 
            this.chkShowTaskForm.AutoSize = true;
            this.chkShowTaskForm.Location = new System.Drawing.Point(31, 170);
            this.chkShowTaskForm.Margin = new System.Windows.Forms.Padding(4);
            this.chkShowTaskForm.Name = "chkShowTaskForm";
            this.chkShowTaskForm.Size = new System.Drawing.Size(236, 21);
            this.chkShowTaskForm.TabIndex = 3;
            this.chkShowTaskForm.Text = "Show task form (really annoying)";
            this.chkShowTaskForm.UseVisualStyleBackColor = true;
            // 
            // chkStartAtLogon
            // 
            this.chkStartAtLogon.AutoSize = true;
            this.chkStartAtLogon.Location = new System.Drawing.Point(31, 142);
            this.chkStartAtLogon.Margin = new System.Windows.Forms.Padding(4);
            this.chkStartAtLogon.Name = "chkStartAtLogon";
            this.chkStartAtLogon.Size = new System.Drawing.Size(238, 21);
            this.chkStartAtLogon.TabIndex = 3;
            this.chkStartAtLogon.Text = "Start Annoying Manager at logon";
            this.chkStartAtLogon.UseVisualStyleBackColor = true;
            // 
            // txtStartupTime
            // 
            this.txtStartupTime.Location = new System.Drawing.Point(279, 31);
            this.txtStartupTime.Margin = new System.Windows.Forms.Padding(4);
            this.txtStartupTime.Mask = "90:00:00";
            this.txtStartupTime.Name = "txtStartupTime";
            this.txtStartupTime.Size = new System.Drawing.Size(132, 22);
            this.txtStartupTime.TabIndex = 2;
            // 
            // txtMaxTime
            // 
            this.txtMaxTime.Location = new System.Drawing.Point(279, 102);
            this.txtMaxTime.Margin = new System.Windows.Forms.Padding(4);
            this.txtMaxTime.Mask = "90:00:00";
            this.txtMaxTime.Name = "txtMaxTime";
            this.txtMaxTime.Size = new System.Drawing.Size(132, 22);
            this.txtMaxTime.TabIndex = 2;
            // 
            // txtEndTime
            // 
            this.txtEndTime.Location = new System.Drawing.Point(279, 66);
            this.txtEndTime.Margin = new System.Windows.Forms.Padding(4);
            this.txtEndTime.Mask = "90:00:00";
            this.txtEndTime.Name = "txtEndTime";
            this.txtEndTime.Size = new System.Drawing.Size(132, 22);
            this.txtEndTime.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 106);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(147, 17);
            this.label3.TabIndex = 1;
            this.label3.Text = "Maximum time for task";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 70);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(123, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "End workday time:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 34);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(144, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Startup workday time:";
            // 
            // FormConfig
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(468, 287);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Annoying Manager 1.0";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FormConfig_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MaskedTextBox txtStartupTime;
        private System.Windows.Forms.MaskedTextBox txtEndTime;
        private System.Windows.Forms.CheckBox chkStartAtLogon;
        private System.Windows.Forms.MaskedTextBox txtMaxTime;
        private System.Windows.Forms.CheckBox chkShowTaskForm;
    }
}