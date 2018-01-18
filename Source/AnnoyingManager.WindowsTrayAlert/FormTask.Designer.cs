namespace AnnoyingManager.WindowsTrayAlert
{
    partial class FormTask
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTask));
            this.btnOk = new System.Windows.Forms.Button();
            this.txtReference = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ddlCategories = new System.Windows.Forms.ComboBox();
            this.btnSuggestedTasks = new System.Windows.Forms.Button();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pnlLastTasks = new System.Windows.Forms.Panel();
            this.tipTasks = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblDuration = new System.Windows.Forms.Label();
            this.trkDuration = new System.Windows.Forms.TrackBar();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trkDuration)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(146, 237);
            this.btnOk.Margin = new System.Windows.Forms.Padding(4);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(100, 28);
            this.btnOk.TabIndex = 7;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // txtReference
            // 
            this.txtReference.Location = new System.Drawing.Point(81, 55);
            this.txtReference.Margin = new System.Windows.Forms.Padding(4);
            this.txtReference.MaxLength = 20;
            this.txtReference.Name = "txtReference";
            this.txtReference.Size = new System.Drawing.Size(165, 22);
            this.txtReference.TabIndex = 3;
            this.txtReference.Enter += new System.EventHandler(this.txt_Enter);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.ddlCategories);
            this.groupBox1.Controls.Add(this.btnSuggestedTasks);
            this.groupBox1.Controls.Add(this.btnOk);
            this.groupBox1.Controls.Add(this.txtDescription);
            this.groupBox1.Controls.Add(this.txtReference);
            this.groupBox1.Location = new System.Drawing.Point(16, 15);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(304, 279);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Put a new task...";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 94);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "Descr.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 59);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Ref #";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 27);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Category";
            // 
            // ddlCategories
            // 
            this.ddlCategories.FormattingEnabled = true;
            this.ddlCategories.Location = new System.Drawing.Point(81, 23);
            this.ddlCategories.Margin = new System.Windows.Forms.Padding(4);
            this.ddlCategories.MaxLength = 20;
            this.ddlCategories.Name = "ddlCategories";
            this.ddlCategories.Size = new System.Drawing.Size(213, 24);
            this.ddlCategories.TabIndex = 1;
            // 
            // btnSuggestedTasks
            // 
            this.btnSuggestedTasks.Location = new System.Drawing.Point(256, 53);
            this.btnSuggestedTasks.Margin = new System.Windows.Forms.Padding(4);
            this.btnSuggestedTasks.Name = "btnSuggestedTasks";
            this.btnSuggestedTasks.Size = new System.Drawing.Size(36, 28);
            this.btnSuggestedTasks.TabIndex = 4;
            this.btnSuggestedTasks.Text = "...";
            this.btnSuggestedTasks.UseVisualStyleBackColor = true;
            this.btnSuggestedTasks.Click += new System.EventHandler(this.btnSuggestedTasks_Click);
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(81, 87);
            this.txtDescription.Margin = new System.Windows.Forms.Padding(4);
            this.txtDescription.MaxLength = 1000;
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(213, 142);
            this.txtDescription.TabIndex = 6;
            this.txtDescription.Enter += new System.EventHandler(this.txt_Enter);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.pnlLastTasks);
            this.groupBox2.Location = new System.Drawing.Point(328, 15);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(304, 279);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "... or choose among the last ones";
            // 
            // pnlLastTasks
            // 
            this.pnlLastTasks.AutoScroll = true;
            this.pnlLastTasks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLastTasks.Location = new System.Drawing.Point(4, 19);
            this.pnlLastTasks.Margin = new System.Windows.Forms.Padding(4);
            this.pnlLastTasks.Name = "pnlLastTasks";
            this.pnlLastTasks.Size = new System.Drawing.Size(296, 256);
            this.pnlLastTasks.TabIndex = 0;
            // 
            // tipTasks
            // 
            this.tipTasks.ShowAlways = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblDuration);
            this.groupBox3.Controls.Add(this.trkDuration);
            this.groupBox3.Location = new System.Drawing.Point(640, 15);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(76, 279);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Duration";
            // 
            // lblDuration
            // 
            this.lblDuration.Location = new System.Drawing.Point(7, 19);
            this.lblDuration.Name = "lblDuration";
            this.lblDuration.Size = new System.Drawing.Size(63, 23);
            this.lblDuration.TabIndex = 1;
            this.lblDuration.Text = "00:00";
            // 
            // trkDuration
            // 
            this.trkDuration.LargeChange = 10;
            this.trkDuration.Location = new System.Drawing.Point(14, 45);
            this.trkDuration.Maximum = 120;
            this.trkDuration.Minimum = 5;
            this.trkDuration.Name = "trkDuration";
            this.trkDuration.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trkDuration.Size = new System.Drawing.Size(56, 221);
            this.trkDuration.TabIndex = 0;
            this.trkDuration.TickFrequency = 10;
            this.trkDuration.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trkDuration.Value = 5;
            this.trkDuration.ValueChanged += new System.EventHandler(this.trkDuration_ValueChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 306);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(734, 25);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(151, 20);
            this.lblStatus.Text = "toolStripStatusLabel1";
            // 
            // FormTask
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 331);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormTask";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Annoying Manager 1.0";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trkDuration)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.TextBox txtReference;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.ComboBox ddlCategories;
        private System.Windows.Forms.Panel pnlLastTasks;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolTip tipTasks;
        private System.Windows.Forms.Button btnSuggestedTasks;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TrackBar trkDuration;
        private System.Windows.Forms.Label lblDuration;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
    }
}