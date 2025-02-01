namespace DMR.Reporting.Report_Generator
{
    partial class ReportType
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportType));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbtnMD = new System.Windows.Forms.RadioButton();
            this.rbtnTVD = new System.Windows.Forms.RadioButton();
            this.rbtnRecap = new System.Windows.Forms.RadioButton();
            this.rbtnDMR = new System.Windows.Forms.RadioButton();
            this.rbtnWellProgress = new System.Windows.Forms.RadioButton();
            this.rbtnHoleSectionMudMatCost = new System.Windows.Forms.RadioButton();
            this.rbtnHoleSectionMudMatConsumption = new System.Windows.Forms.RadioButton();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.txtReportShamsiDate = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtReportCode = new System.Windows.Forms.TextBox();
            this.btnChooseHoleSize = new System.Windows.Forms.Button();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.lblHoleSize = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.rbtnOperatorImage = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dtpReportTimePicker = new System.Windows.Forms.DateTimePicker();
            this.rbtnOperatorAndContractorImage = new System.Windows.Forms.RadioButton();
            this.rbtnContractorImage = new System.Windows.Forms.RadioButton();
            this.nudReportNumber = new System.Windows.Forms.NumericUpDown();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.btnReportSelectOutDir = new System.Windows.Forms.Button();
            this.txtReportOutDir = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.btnReportGenerate = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudReportNumber)).BeginInit();
            this.groupBox12.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.rbtnRecap);
            this.groupBox1.Controls.Add(this.rbtnDMR);
            this.groupBox1.Controls.Add(this.rbtnWellProgress);
            this.groupBox1.Controls.Add(this.rbtnHoleSectionMudMatCost);
            this.groupBox1.Controls.Add(this.rbtnHoleSectionMudMatConsumption);
            this.groupBox1.Location = new System.Drawing.Point(13, 41);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(427, 70);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Report Type";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.rbtnMD);
            this.panel1.Controls.Add(this.rbtnTVD);
            this.panel1.Location = new System.Drawing.Point(322, 17);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(99, 23);
            this.panel1.TabIndex = 1;
            // 
            // rbtnMD
            // 
            this.rbtnMD.AutoSize = true;
            this.rbtnMD.Location = new System.Drawing.Point(56, 2);
            this.rbtnMD.Name = "rbtnMD";
            this.rbtnMD.Size = new System.Drawing.Size(42, 17);
            this.rbtnMD.TabIndex = 1;
            this.rbtnMD.Text = "MD";
            this.rbtnMD.UseVisualStyleBackColor = true;
            // 
            // rbtnTVD
            // 
            this.rbtnTVD.AutoSize = true;
            this.rbtnTVD.Checked = true;
            this.rbtnTVD.Location = new System.Drawing.Point(3, 2);
            this.rbtnTVD.Name = "rbtnTVD";
            this.rbtnTVD.Size = new System.Drawing.Size(47, 17);
            this.rbtnTVD.TabIndex = 0;
            this.rbtnTVD.TabStop = true;
            this.rbtnTVD.Text = "TVD";
            this.rbtnTVD.UseVisualStyleBackColor = true;
            // 
            // rbtnRecap
            // 
            this.rbtnRecap.AutoSize = true;
            this.rbtnRecap.Location = new System.Drawing.Point(322, 46);
            this.rbtnRecap.Name = "rbtnRecap";
            this.rbtnRecap.Size = new System.Drawing.Size(57, 17);
            this.rbtnRecap.TabIndex = 4;
            this.rbtnRecap.Text = "Recap";
            this.rbtnRecap.UseVisualStyleBackColor = true;
            // 
            // rbtnDMR
            // 
            this.rbtnDMR.AutoSize = true;
            this.rbtnDMR.Checked = true;
            this.rbtnDMR.Location = new System.Drawing.Point(226, 42);
            this.rbtnDMR.Name = "rbtnDMR";
            this.rbtnDMR.Size = new System.Drawing.Size(50, 17);
            this.rbtnDMR.TabIndex = 3;
            this.rbtnDMR.TabStop = true;
            this.rbtnDMR.Text = "DMR";
            this.rbtnDMR.UseVisualStyleBackColor = true;
            // 
            // rbtnWellProgress
            // 
            this.rbtnWellProgress.AutoSize = true;
            this.rbtnWellProgress.Location = new System.Drawing.Point(226, 19);
            this.rbtnWellProgress.Name = "rbtnWellProgress";
            this.rbtnWellProgress.Size = new System.Drawing.Size(90, 17);
            this.rbtnWellProgress.TabIndex = 2;
            this.rbtnWellProgress.Text = "Well Progress";
            this.rbtnWellProgress.UseVisualStyleBackColor = true;
            // 
            // rbtnHoleSectionMudMatCost
            // 
            this.rbtnHoleSectionMudMatCost.AutoSize = true;
            this.rbtnHoleSectionMudMatCost.Location = new System.Drawing.Point(6, 42);
            this.rbtnHoleSectionMudMatCost.Name = "rbtnHoleSectionMudMatCost";
            this.rbtnHoleSectionMudMatCost.Size = new System.Drawing.Size(174, 17);
            this.rbtnHoleSectionMudMatCost.TabIndex = 1;
            this.rbtnHoleSectionMudMatCost.Text = "Hole Section Mud Material Cost";
            this.rbtnHoleSectionMudMatCost.UseVisualStyleBackColor = true;
            // 
            // rbtnHoleSectionMudMatConsumption
            // 
            this.rbtnHoleSectionMudMatConsumption.AutoSize = true;
            this.rbtnHoleSectionMudMatConsumption.Location = new System.Drawing.Point(6, 19);
            this.rbtnHoleSectionMudMatConsumption.Name = "rbtnHoleSectionMudMatConsumption";
            this.rbtnHoleSectionMudMatConsumption.Size = new System.Drawing.Size(214, 17);
            this.rbtnHoleSectionMudMatConsumption.TabIndex = 0;
            this.rbtnHoleSectionMudMatConsumption.Text = "Hole Section Mud Material Consumption";
            this.rbtnHoleSectionMudMatConsumption.UseVisualStyleBackColor = true;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(15, 46);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(70, 13);
            this.label17.TabIndex = 3;
            this.label17.Text = "Shamsi Date:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(15, 22);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(33, 13);
            this.label16.TabIndex = 4;
            this.label16.Text = "Date:";
            // 
            // txtReportShamsiDate
            // 
            this.txtReportShamsiDate.Location = new System.Drawing.Point(103, 43);
            this.txtReportShamsiDate.Name = "txtReportShamsiDate";
            this.txtReportShamsiDate.ReadOnly = true;
            this.txtReportShamsiDate.Size = new System.Drawing.Size(133, 20);
            this.txtReportShamsiDate.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Code:";
            // 
            // txtReportCode
            // 
            this.txtReportCode.Location = new System.Drawing.Point(103, 69);
            this.txtReportCode.Name = "txtReportCode";
            this.txtReportCode.Size = new System.Drawing.Size(133, 20);
            this.txtReportCode.TabIndex = 1;
            // 
            // btnChooseHoleSize
            // 
            this.btnChooseHoleSize.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnChooseHoleSize.ImageKey = "Add";
            this.btnChooseHoleSize.ImageList = this.imageList;
            this.btnChooseHoleSize.Location = new System.Drawing.Point(406, 13);
            this.btnChooseHoleSize.Name = "btnChooseHoleSize";
            this.btnChooseHoleSize.Size = new System.Drawing.Size(22, 20);
            this.btnChooseHoleSize.TabIndex = 1;
            this.btnChooseHoleSize.Tag = "Choose from Hole Size List";
            this.btnChooseHoleSize.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnChooseHoleSize.UseVisualStyleBackColor = true;
            this.btnChooseHoleSize.Click += new System.EventHandler(this.btnChooseHoleSize_Click);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "Project");
            this.imageList.Images.SetKeyName(1, "Rig");
            this.imageList.Images.SetKeyName(2, "Well");
            this.imageList.Images.SetKeyName(3, "RigWell");
            this.imageList.Images.SetKeyName(4, "Onshore");
            this.imageList.Images.SetKeyName(5, "Offshore");
            this.imageList.Images.SetKeyName(6, "Add");
            this.imageList.Images.SetKeyName(7, "add2");
            this.imageList.Images.SetKeyName(8, "Remove");
            this.imageList.Images.SetKeyName(9, "remove2");
            this.imageList.Images.SetKeyName(10, "Edit");
            this.imageList.Images.SetKeyName(11, "Check");
            this.imageList.Images.SetKeyName(12, "warning");
            this.imageList.Images.SetKeyName(13, "Right");
            this.imageList.Images.SetKeyName(14, "Left.png");
            this.imageList.Images.SetKeyName(15, "Up");
            this.imageList.Images.SetKeyName(16, "Down");
            this.imageList.Images.SetKeyName(17, "report");
            this.imageList.Images.SetKeyName(18, "revision");
            this.imageList.Images.SetKeyName(19, "refresh");
            this.imageList.Images.SetKeyName(20, "forbidden.png");
            this.imageList.Images.SetKeyName(21, "point_hand.png");
            this.imageList.Images.SetKeyName(22, "noFilter");
            this.imageList.Images.SetKeyName(23, "applyFilter");
            this.imageList.Images.SetKeyName(24, "bookmark.png");
            this.imageList.Images.SetKeyName(25, "flask.png");
            this.imageList.Images.SetKeyName(26, "eject.png");
            this.imageList.Images.SetKeyName(27, "user_clear.png");
            this.imageList.Images.SetKeyName(28, "open");
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(217, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 84;
            this.label3.Text = "Hole Size (in):";
            // 
            // lblHoleSize
            // 
            this.lblHoleSize.Location = new System.Drawing.Point(295, 15);
            this.lblHoleSize.Name = "lblHoleSize";
            this.lblHoleSize.Size = new System.Drawing.Size(105, 18);
            this.lblHoleSize.TabIndex = 85;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(281, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Logo:";
            // 
            // rbtnOperatorImage
            // 
            this.rbtnOperatorImage.AutoSize = true;
            this.rbtnOperatorImage.Checked = true;
            this.rbtnOperatorImage.Location = new System.Drawing.Point(285, 32);
            this.rbtnOperatorImage.Name = "rbtnOperatorImage";
            this.rbtnOperatorImage.Size = new System.Drawing.Size(66, 17);
            this.rbtnOperatorImage.TabIndex = 2;
            this.rbtnOperatorImage.TabStop = true;
            this.rbtnOperatorImage.Text = "Operator";
            this.rbtnOperatorImage.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.txtReportShamsiDate);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Controls.Add(this.dtpReportTimePicker);
            this.groupBox2.Controls.Add(this.rbtnOperatorAndContractorImage);
            this.groupBox2.Controls.Add(this.rbtnContractorImage);
            this.groupBox2.Controls.Add(this.rbtnOperatorImage);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtReportCode);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(13, 117);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(427, 99);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Common";
            // 
            // dtpReportTimePicker
            // 
            this.dtpReportTimePicker.Location = new System.Drawing.Point(54, 19);
            this.dtpReportTimePicker.Name = "dtpReportTimePicker";
            this.dtpReportTimePicker.Size = new System.Drawing.Size(182, 20);
            this.dtpReportTimePicker.TabIndex = 0;
            this.dtpReportTimePicker.ValueChanged += new System.EventHandler(this.dtpReportTimePicker_ValueChanged);
            // 
            // rbtnOperatorAndContractorImage
            // 
            this.rbtnOperatorAndContractorImage.AutoSize = true;
            this.rbtnOperatorAndContractorImage.Location = new System.Drawing.Point(284, 78);
            this.rbtnOperatorAndContractorImage.Name = "rbtnOperatorAndContractorImage";
            this.rbtnOperatorAndContractorImage.Size = new System.Drawing.Size(47, 17);
            this.rbtnOperatorAndContractorImage.TabIndex = 4;
            this.rbtnOperatorAndContractorImage.Text = "Both";
            this.rbtnOperatorAndContractorImage.UseVisualStyleBackColor = true;
            // 
            // rbtnContractorImage
            // 
            this.rbtnContractorImage.AutoSize = true;
            this.rbtnContractorImage.Location = new System.Drawing.Point(284, 55);
            this.rbtnContractorImage.Name = "rbtnContractorImage";
            this.rbtnContractorImage.Size = new System.Drawing.Size(74, 17);
            this.rbtnContractorImage.TabIndex = 3;
            this.rbtnContractorImage.Text = "Contractor";
            this.rbtnContractorImage.UseVisualStyleBackColor = true;
            // 
            // nudReportNumber
            // 
            this.nudReportNumber.Location = new System.Drawing.Point(74, 15);
            this.nudReportNumber.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudReportNumber.Name = "nudReportNumber";
            this.nudReportNumber.Size = new System.Drawing.Size(59, 20);
            this.nudReportNumber.TabIndex = 0;
            // 
            // groupBox12
            // 
            this.groupBox12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox12.Controls.Add(this.btnReportSelectOutDir);
            this.groupBox12.Controls.Add(this.txtReportOutDir);
            this.groupBox12.Controls.Add(this.label18);
            this.groupBox12.Location = new System.Drawing.Point(13, 225);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(338, 91);
            this.groupBox12.TabIndex = 1;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "Output";
            // 
            // btnReportSelectOutDir
            // 
            this.btnReportSelectOutDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReportSelectOutDir.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnReportSelectOutDir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReportSelectOutDir.ImageKey = "point_hand.png";
            this.btnReportSelectOutDir.Location = new System.Drawing.Point(284, 34);
            this.btnReportSelectOutDir.Name = "btnReportSelectOutDir";
            this.btnReportSelectOutDir.Size = new System.Drawing.Size(48, 23);
            this.btnReportSelectOutDir.TabIndex = 0;
            this.btnReportSelectOutDir.Text = "...";
            this.btnReportSelectOutDir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnReportSelectOutDir.UseVisualStyleBackColor = true;
            this.btnReportSelectOutDir.Click += new System.EventHandler(this.btnReportSelectOutDir_Click);
            // 
            // txtReportOutDir
            // 
            this.txtReportOutDir.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtReportOutDir.Location = new System.Drawing.Point(9, 36);
            this.txtReportOutDir.Multiline = true;
            this.txtReportOutDir.Name = "txtReportOutDir";
            this.txtReportOutDir.ReadOnly = true;
            this.txtReportOutDir.Size = new System.Drawing.Size(270, 48);
            this.txtReportOutDir.TabIndex = 2;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(6, 19);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(39, 13);
            this.label18.TabIndex = 0;
            this.label18.Text = "Folder:";
            // 
            // btnReportGenerate
            // 
            this.btnReportGenerate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReportGenerate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnReportGenerate.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnReportGenerate.ForeColor = System.Drawing.Color.Black;
            this.btnReportGenerate.ImageKey = "List";
            this.btnReportGenerate.Location = new System.Drawing.Point(369, 225);
            this.btnReportGenerate.Name = "btnReportGenerate";
            this.btnReportGenerate.Size = new System.Drawing.Size(70, 91);
            this.btnReportGenerate.TabIndex = 2;
            this.btnReportGenerate.Text = "Generate";
            this.btnReportGenerate.UseVisualStyleBackColor = true;
            this.btnReportGenerate.Click += new System.EventHandler(this.btnReportGenerate_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Report #:";
            // 
            // ReportType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(449, 328);
            this.Controls.Add(this.nudReportNumber);
            this.Controls.Add(this.btnChooseHoleSize);
            this.Controls.Add(this.btnReportGenerate);
            this.Controls.Add(this.groupBox12);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.lblHoleSize);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ReportType";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Report Type";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudReportNumber)).EndInit();
            this.groupBox12.ResumeLayout(false);
            this.groupBox12.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbtnHoleSectionMudMatConsumption;
        private System.Windows.Forms.RadioButton rbtnDMR;
        private System.Windows.Forms.RadioButton rbtnWellProgress;
        private System.Windows.Forms.RadioButton rbtnHoleSectionMudMatCost;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtReportShamsiDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtReportCode;
        private System.Windows.Forms.Button btnChooseHoleSize;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblHoleSize;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rbtnOperatorImage;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbtnOperatorAndContractorImage;
        private System.Windows.Forms.RadioButton rbtnContractorImage;
        private System.Windows.Forms.GroupBox groupBox12;
        private System.Windows.Forms.Button btnReportSelectOutDir;
        private System.Windows.Forms.TextBox txtReportOutDir;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button btnReportGenerate;
        private System.Windows.Forms.RadioButton rbtnTVD;
        private System.Windows.Forms.RadioButton rbtnMD;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbtnRecap;
        private System.Windows.Forms.NumericUpDown nudReportNumber;
        private System.Windows.Forms.DateTimePicker dtpReportTimePicker;
        private System.Windows.Forms.Label label4;
    }
}