namespace YKWriter
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            btnRun = new Button();
            lbSelect = new Label();
            cbDrives = new ComboBox();
            gbLog = new GroupBox();
            rtbLog = new RichTextBox();
            label2 = new Label();
            pbStatus = new ProgressBar();
            btnRefresh = new Button();
            gbLog.SuspendLayout();
            SuspendLayout();
            // 
            // btnRun
            // 
            btnRun.Location = new Point(245, 5);
            btnRun.Margin = new Padding(2);
            btnRun.Name = "btnRun";
            btnRun.Size = new Size(185, 22);
            btnRun.TabIndex = 0;
            btnRun.Text = "Run";
            btnRun.UseVisualStyleBackColor = true;
            btnRun.Click += btnRun_Click;
            // 
            // lbSelect
            // 
            lbSelect.AutoSize = true;
            lbSelect.Font = new Font("Verdana", 7F);
            lbSelect.Location = new Point(11, 10);
            lbSelect.Margin = new Padding(2, 0, 2, 0);
            lbSelect.Name = "lbSelect";
            lbSelect.Size = new Size(137, 12);
            lbSelect.TabIndex = 1;
            lbSelect.Text = "Select removable drive:";
            // 
            // cbDrives
            // 
            cbDrives.FormattingEnabled = true;
            cbDrives.Location = new Point(156, 7);
            cbDrives.Margin = new Padding(2);
            cbDrives.Name = "cbDrives";
            cbDrives.Size = new Size(60, 20);
            cbDrives.TabIndex = 2;
            cbDrives.SelectedIndexChanged += cbDrives_SelectedIndexChanged;
            // 
            // gbLog
            // 
            gbLog.Controls.Add(rtbLog);
            gbLog.Location = new Point(11, 31);
            gbLog.Margin = new Padding(2);
            gbLog.Name = "gbLog";
            gbLog.Padding = new Padding(2);
            gbLog.Size = new Size(419, 138);
            gbLog.TabIndex = 3;
            gbLog.TabStop = false;
            gbLog.Text = "Log";
            // 
            // rtbLog
            // 
            rtbLog.BorderStyle = BorderStyle.None;
            rtbLog.Location = new Point(4, 16);
            rtbLog.Margin = new Padding(2);
            rtbLog.Name = "rtbLog";
            rtbLog.ReadOnly = true;
            rtbLog.ScrollBars = RichTextBoxScrollBars.Vertical;
            rtbLog.Size = new Size(411, 118);
            rtbLog.TabIndex = 0;
            rtbLog.Text = "";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(261, 194);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(169, 12);
            label2.TabIndex = 5;
            label2.Text = "Brought to you by Equilibrium";
            // 
            // pbStatus
            // 
            pbStatus.Location = new Point(11, 173);
            pbStatus.Margin = new Padding(2);
            pbStatus.Name = "pbStatus";
            pbStatus.Size = new Size(419, 19);
            pbStatus.TabIndex = 6;
            // 
            // btnRefresh
            // 
            btnRefresh.Image = (Image)resources.GetObject("btnRefresh.Image");
            btnRefresh.Location = new Point(220, 6);
            btnRefresh.Margin = new Padding(2);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(21, 20);
            btnRefresh.TabIndex = 7;
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(6F, 12F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(441, 212);
            Controls.Add(btnRefresh);
            Controls.Add(pbStatus);
            Controls.Add(label2);
            Controls.Add(gbLog);
            Controls.Add(cbDrives);
            Controls.Add(lbSelect);
            Controls.Add(btnRun);
            Font = new Font("Verdana", 7F);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            HelpButton = true;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(2);
            MaximizeBox = false;
            Name = "MainForm";
            Text = "YKWriter (CVE-2026-45585)";
            Load += Form1_Load;
            gbLog.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnRun;
        private Label lbSelect;
        private ComboBox cbDrives;
        private GroupBox gbLog;
        private RichTextBox rtbLog;
        private Label label2;
        private ProgressBar pbStatus;
        private Button button1;
        private Button btnRefresh;
    }
}
