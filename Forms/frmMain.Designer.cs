namespace DMR
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnUnitConv = new System.Windows.Forms.Button();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.btnCalc = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnLogOut = new System.Windows.Forms.Button();
            this.btnAccount = new System.Windows.Forms.Button();
            this.lblUserID = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.lblCurDateShamsi = new System.Windows.Forms.Label();
            this.lblCurDate = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.lblCurTime = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.lblCurRep = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblCurRigWell = new System.Windows.Forms.Label();
            this.lblCurProject = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.pboxStartup = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rbtnRemarkForm = new System.Windows.Forms.RadioButton();
            this.rbtnMudVolManagementForm = new System.Windows.Forms.RadioButton();
            this.rbtnInvManagementForm = new System.Windows.Forms.RadioButton();
            this.rbtnRigForm = new System.Windows.Forms.RadioButton();
            this.rbtnReportingForm = new System.Windows.Forms.RadioButton();
            this.rbtnRigWellReportForm = new System.Windows.Forms.RadioButton();
            this.rbtnWellInfoForm = new System.Windows.Forms.RadioButton();
            this.rbtnStartupForm = new System.Windows.Forms.RadioButton();
            this.rbtnSolidAnaForm = new System.Windows.Forms.RadioButton();
            this.rbtnMudPropForm = new System.Windows.Forms.RadioButton();
            this.rbtnSolidConEqForm = new System.Windows.Forms.RadioButton();
            this.rbtnDrilliOpForm = new System.Windows.Forms.RadioButton();
            this.rbtnMudLossForm = new System.Windows.Forms.RadioButton();
            this.rbtnDrillingFluidsProgramForm = new System.Windows.Forms.RadioButton();
            this.rbtnPorjectForm = new System.Windows.Forms.RadioButton();
            this.rbtnHydraulicForm = new System.Windows.Forms.RadioButton();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.tmrUpdate = new System.Windows.Forms.Timer(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pboxStartup)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox6);
            this.panel1.Controls.Add(this.groupBox5);
            this.panel1.Controls.Add(this.pboxStartup);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1011, 96);
            this.panel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnUnitConv);
            this.groupBox1.Controls.Add(this.btnCalc);
            this.groupBox1.Location = new System.Drawing.Point(476, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(121, 82);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Helper Tools";
            // 
            // btnUnitConv
            // 
            this.btnUnitConv.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnUnitConv.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUnitConv.ImageIndex = 19;
            this.btnUnitConv.ImageList = this.imageList;
            this.btnUnitConv.Location = new System.Drawing.Point(6, 19);
            this.btnUnitConv.Name = "btnUnitConv";
            this.btnUnitConv.Size = new System.Drawing.Size(104, 25);
            this.btnUnitConv.TabIndex = 0;
            this.btnUnitConv.Text = "Unit Conversion";
            this.btnUnitConv.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnUnitConv.UseVisualStyleBackColor = true;
            this.btnUnitConv.Click += new System.EventHandler(this.btnUnitConv_Click);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "Project");
            this.imageList.Images.SetKeyName(1, "Rig");
            this.imageList.Images.SetKeyName(2, "Well");
            this.imageList.Images.SetKeyName(3, "Onshore");
            this.imageList.Images.SetKeyName(4, "Offshore");
            this.imageList.Images.SetKeyName(5, "Add");
            this.imageList.Images.SetKeyName(6, "add2");
            this.imageList.Images.SetKeyName(7, "Remove");
            this.imageList.Images.SetKeyName(8, "remove2");
            this.imageList.Images.SetKeyName(9, "Edit");
            this.imageList.Images.SetKeyName(10, "Check");
            this.imageList.Images.SetKeyName(11, "warning");
            this.imageList.Images.SetKeyName(12, "Right");
            this.imageList.Images.SetKeyName(13, "Up");
            this.imageList.Images.SetKeyName(14, "Down");
            this.imageList.Images.SetKeyName(15, "report");
            this.imageList.Images.SetKeyName(16, "revision");
            this.imageList.Images.SetKeyName(17, "refresh");
            this.imageList.Images.SetKeyName(18, "forbidden.png");
            this.imageList.Images.SetKeyName(19, "point_hand.png");
            this.imageList.Images.SetKeyName(20, "noFilter");
            this.imageList.Images.SetKeyName(21, "applyFilter");
            this.imageList.Images.SetKeyName(22, "bookmark.png");
            this.imageList.Images.SetKeyName(23, "flask.png");
            this.imageList.Images.SetKeyName(24, "eject.png");
            this.imageList.Images.SetKeyName(25, "user_clear.png");
            this.imageList.Images.SetKeyName(26, "open");
            // 
            // btnCalc
            // 
            this.btnCalc.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCalc.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCalc.ImageIndex = 19;
            this.btnCalc.ImageList = this.imageList;
            this.btnCalc.Location = new System.Drawing.Point(6, 50);
            this.btnCalc.Name = "btnCalc";
            this.btnCalc.Size = new System.Drawing.Size(104, 25);
            this.btnCalc.TabIndex = 1;
            this.btnCalc.Text = "Calculator";
            this.btnCalc.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCalc.UseVisualStyleBackColor = true;
            this.btnCalc.Click += new System.EventHandler(this.btnCalc_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnLogOut);
            this.groupBox2.Controls.Add(this.btnAccount);
            this.groupBox2.Controls.Add(this.lblUserID);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Location = new System.Drawing.Point(603, 7);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(160, 82);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "User";
            // 
            // btnLogOut
            // 
            this.btnLogOut.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnLogOut.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLogOut.ImageKey = "forbidden.png";
            this.btnLogOut.ImageList = this.imageList;
            this.btnLogOut.Location = new System.Drawing.Point(9, 50);
            this.btnLogOut.Name = "btnLogOut";
            this.btnLogOut.Size = new System.Drawing.Size(68, 25);
            this.btnLogOut.TabIndex = 0;
            this.btnLogOut.Text = "Log Out";
            this.btnLogOut.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLogOut.UseVisualStyleBackColor = true;
            this.btnLogOut.Click += new System.EventHandler(this.btnLogOut_Click);
            // 
            // btnAccount
            // 
            this.btnAccount.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAccount.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAccount.ImageKey = "user_clear.png";
            this.btnAccount.ImageList = this.imageList;
            this.btnAccount.Location = new System.Drawing.Point(86, 50);
            this.btnAccount.Name = "btnAccount";
            this.btnAccount.Size = new System.Drawing.Size(68, 25);
            this.btnAccount.TabIndex = 1;
            this.btnAccount.Text = "Account";
            this.btnAccount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAccount.UseVisualStyleBackColor = true;
            this.btnAccount.Click += new System.EventHandler(this.btnAccount_Click);
            // 
            // lblUserID
            // 
            this.lblUserID.AutoSize = true;
            this.lblUserID.Location = new System.Drawing.Point(33, 16);
            this.lblUserID.Name = "lblUserID";
            this.lblUserID.Size = new System.Drawing.Size(57, 13);
            this.lblUserID.TabIndex = 24;
            this.lblUserID.Text = "Something";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 16);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(21, 13);
            this.label8.TabIndex = 26;
            this.label8.Text = "ID:";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.lblCurDateShamsi);
            this.groupBox6.Controls.Add(this.lblCurDate);
            this.groupBox6.Controls.Add(this.label23);
            this.groupBox6.Controls.Add(this.lblCurTime);
            this.groupBox6.Controls.Add(this.label31);
            this.groupBox6.Location = new System.Drawing.Point(338, 7);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(132, 82);
            this.groupBox6.TabIndex = 1;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Today";
            // 
            // lblCurDateShamsi
            // 
            this.lblCurDateShamsi.AutoSize = true;
            this.lblCurDateShamsi.Location = new System.Drawing.Point(45, 56);
            this.lblCurDateShamsi.Name = "lblCurDateShamsi";
            this.lblCurDateShamsi.Size = new System.Drawing.Size(57, 13);
            this.lblCurDateShamsi.TabIndex = 23;
            this.lblCurDateShamsi.Text = "Something";
            // 
            // lblCurDate
            // 
            this.lblCurDate.AutoSize = true;
            this.lblCurDate.Location = new System.Drawing.Point(45, 36);
            this.lblCurDate.Name = "lblCurDate";
            this.lblCurDate.Size = new System.Drawing.Size(57, 13);
            this.lblCurDate.TabIndex = 23;
            this.lblCurDate.Text = "Something";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(6, 36);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(33, 13);
            this.label23.TabIndex = 25;
            this.label23.Text = "Date:";
            // 
            // lblCurTime
            // 
            this.lblCurTime.AutoSize = true;
            this.lblCurTime.Location = new System.Drawing.Point(45, 16);
            this.lblCurTime.Name = "lblCurTime";
            this.lblCurTime.Size = new System.Drawing.Size(57, 13);
            this.lblCurTime.TabIndex = 24;
            this.lblCurTime.Text = "Something";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(6, 16);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(33, 13);
            this.label31.TabIndex = 26;
            this.label31.Text = "Time:";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.lblCurRep);
            this.groupBox5.Controls.Add(this.label3);
            this.groupBox5.Controls.Add(this.lblCurRigWell);
            this.groupBox5.Controls.Add(this.lblCurProject);
            this.groupBox5.Controls.Add(this.label1);
            this.groupBox5.Controls.Add(this.label19);
            this.groupBox5.Location = new System.Drawing.Point(137, 7);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(186, 82);
            this.groupBox5.TabIndex = 0;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Current";
            // 
            // lblCurRep
            // 
            this.lblCurRep.AutoEllipsis = true;
            this.lblCurRep.AutoSize = true;
            this.lblCurRep.Location = new System.Drawing.Point(55, 62);
            this.lblCurRep.Name = "lblCurRep";
            this.lblCurRep.Size = new System.Drawing.Size(57, 13);
            this.lblCurRep.TabIndex = 21;
            this.lblCurRep.Text = "Something";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 22;
            this.label3.Text = "Report:";
            // 
            // lblCurRigWell
            // 
            this.lblCurRigWell.AutoEllipsis = true;
            this.lblCurRigWell.AutoSize = true;
            this.lblCurRigWell.Location = new System.Drawing.Point(55, 36);
            this.lblCurRigWell.Name = "lblCurRigWell";
            this.lblCurRigWell.Size = new System.Drawing.Size(110, 13);
            this.lblCurRigWell.TabIndex = 21;
            this.lblCurRigWell.Text = "Something-Something";
            // 
            // lblCurProject
            // 
            this.lblCurProject.AutoEllipsis = true;
            this.lblCurProject.Location = new System.Drawing.Point(55, 16);
            this.lblCurProject.Name = "lblCurProject";
            this.lblCurProject.Size = new System.Drawing.Size(77, 17);
            this.lblCurProject.TabIndex = 21;
            this.lblCurProject.Text = "Something";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "Rig-Well:";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(6, 16);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(43, 13);
            this.label19.TabIndex = 22;
            this.label19.Text = "Project:";
            // 
            // pboxStartup
            // 
            this.pboxStartup.BackColor = System.Drawing.Color.White;
            this.pboxStartup.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pboxStartup.BackgroundImage")));
            this.pboxStartup.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pboxStartup.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pboxStartup.Location = new System.Drawing.Point(6, 7);
            this.pboxStartup.Name = "pboxStartup";
            this.pboxStartup.Size = new System.Drawing.Size(125, 82);
            this.pboxStartup.TabIndex = 1;
            this.pboxStartup.TabStop = false;
            this.pboxStartup.Click += new System.EventHandler(this.pboxStartup_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.rbtnRemarkForm);
            this.panel2.Controls.Add(this.rbtnMudVolManagementForm);
            this.panel2.Controls.Add(this.rbtnInvManagementForm);
            this.panel2.Controls.Add(this.rbtnRigForm);
            this.panel2.Controls.Add(this.rbtnReportingForm);
            this.panel2.Controls.Add(this.rbtnRigWellReportForm);
            this.panel2.Controls.Add(this.rbtnWellInfoForm);
            this.panel2.Controls.Add(this.rbtnStartupForm);
            this.panel2.Controls.Add(this.rbtnSolidAnaForm);
            this.panel2.Controls.Add(this.rbtnMudPropForm);
            this.panel2.Controls.Add(this.rbtnSolidConEqForm);
            this.panel2.Controls.Add(this.rbtnDrilliOpForm);
            this.panel2.Controls.Add(this.rbtnMudLossForm);
            this.panel2.Controls.Add(this.rbtnDrillingFluidsProgramForm);
            this.panel2.Controls.Add(this.rbtnPorjectForm);
            this.panel2.Controls.Add(this.rbtnHydraulicForm);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 96);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(175, 466);
            this.panel2.TabIndex = 1;
            // 
            // rbtnRemarkForm
            // 
            this.rbtnRemarkForm.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbtnRemarkForm.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.rbtnRemarkForm.FlatAppearance.CheckedBackColor = System.Drawing.Color.Silver;
            this.rbtnRemarkForm.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.rbtnRemarkForm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbtnRemarkForm.Image = ((System.Drawing.Image)(resources.GetObject("rbtnRemarkForm.Image")));
            this.rbtnRemarkForm.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.rbtnRemarkForm.Location = new System.Drawing.Point(6, 381);
            this.rbtnRemarkForm.Margin = new System.Windows.Forms.Padding(0);
            this.rbtnRemarkForm.Name = "rbtnRemarkForm";
            this.rbtnRemarkForm.Size = new System.Drawing.Size(160, 25);
            this.rbtnRemarkForm.TabIndex = 13;
            this.rbtnRemarkForm.Text = "Remarks";
            this.rbtnRemarkForm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbtnRemarkForm.UseVisualStyleBackColor = true;
            this.rbtnRemarkForm.CheckedChanged += new System.EventHandler(this.rbtnForm_CheckedChanged);
            // 
            // rbtnMudVolManagementForm
            // 
            this.rbtnMudVolManagementForm.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbtnMudVolManagementForm.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.rbtnMudVolManagementForm.FlatAppearance.CheckedBackColor = System.Drawing.Color.Silver;
            this.rbtnMudVolManagementForm.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.rbtnMudVolManagementForm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbtnMudVolManagementForm.Image = ((System.Drawing.Image)(resources.GetObject("rbtnMudVolManagementForm.Image")));
            this.rbtnMudVolManagementForm.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.rbtnMudVolManagementForm.Location = new System.Drawing.Point(6, 241);
            this.rbtnMudVolManagementForm.Margin = new System.Windows.Forms.Padding(0);
            this.rbtnMudVolManagementForm.Name = "rbtnMudVolManagementForm";
            this.rbtnMudVolManagementForm.Size = new System.Drawing.Size(160, 25);
            this.rbtnMudVolManagementForm.TabIndex = 9;
            this.rbtnMudVolManagementForm.Text = "Mud Volume Management";
            this.rbtnMudVolManagementForm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbtnMudVolManagementForm.UseVisualStyleBackColor = true;
            this.rbtnMudVolManagementForm.CheckedChanged += new System.EventHandler(this.rbtnForm_CheckedChanged);
            // 
            // rbtnInvManagementForm
            // 
            this.rbtnInvManagementForm.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbtnInvManagementForm.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.rbtnInvManagementForm.FlatAppearance.CheckedBackColor = System.Drawing.Color.Silver;
            this.rbtnInvManagementForm.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.rbtnInvManagementForm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbtnInvManagementForm.Image = ((System.Drawing.Image)(resources.GetObject("rbtnInvManagementForm.Image")));
            this.rbtnInvManagementForm.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.rbtnInvManagementForm.Location = new System.Drawing.Point(6, 353);
            this.rbtnInvManagementForm.Margin = new System.Windows.Forms.Padding(0);
            this.rbtnInvManagementForm.Name = "rbtnInvManagementForm";
            this.rbtnInvManagementForm.Size = new System.Drawing.Size(160, 25);
            this.rbtnInvManagementForm.TabIndex = 12;
            this.rbtnInvManagementForm.Text = "Inventory Management";
            this.rbtnInvManagementForm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbtnInvManagementForm.UseVisualStyleBackColor = true;
            this.rbtnInvManagementForm.CheckedChanged += new System.EventHandler(this.rbtnForm_CheckedChanged);
            // 
            // rbtnRigForm
            // 
            this.rbtnRigForm.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbtnRigForm.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.rbtnRigForm.FlatAppearance.CheckedBackColor = System.Drawing.Color.Silver;
            this.rbtnRigForm.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.rbtnRigForm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbtnRigForm.Image = ((System.Drawing.Image)(resources.GetObject("rbtnRigForm.Image")));
            this.rbtnRigForm.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.rbtnRigForm.Location = new System.Drawing.Point(6, 87);
            this.rbtnRigForm.Margin = new System.Windows.Forms.Padding(0);
            this.rbtnRigForm.Name = "rbtnRigForm";
            this.rbtnRigForm.Size = new System.Drawing.Size(76, 25);
            this.rbtnRigForm.TabIndex = 3;
            this.rbtnRigForm.Text = "Rig";
            this.rbtnRigForm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbtnRigForm.UseVisualStyleBackColor = true;
            this.rbtnRigForm.CheckedChanged += new System.EventHandler(this.rbtnForm_CheckedChanged);
            // 
            // rbtnReportingForm
            // 
            this.rbtnReportingForm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rbtnReportingForm.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbtnReportingForm.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.rbtnReportingForm.FlatAppearance.CheckedBackColor = System.Drawing.Color.Silver;
            this.rbtnReportingForm.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.rbtnReportingForm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbtnReportingForm.Image = ((System.Drawing.Image)(resources.GetObject("rbtnReportingForm.Image")));
            this.rbtnReportingForm.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.rbtnReportingForm.Location = new System.Drawing.Point(6, 431);
            this.rbtnReportingForm.Margin = new System.Windows.Forms.Padding(0);
            this.rbtnReportingForm.Name = "rbtnReportingForm";
            this.rbtnReportingForm.Size = new System.Drawing.Size(160, 25);
            this.rbtnReportingForm.TabIndex = 14;
            this.rbtnReportingForm.Text = "Reporting";
            this.rbtnReportingForm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbtnReportingForm.UseVisualStyleBackColor = true;
            this.rbtnReportingForm.CheckedChanged += new System.EventHandler(this.rbtnReportingForm_CheckedChanged);
            // 
            // rbtnRigWellReportForm
            // 
            this.rbtnRigWellReportForm.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbtnRigWellReportForm.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.rbtnRigWellReportForm.FlatAppearance.CheckedBackColor = System.Drawing.Color.Silver;
            this.rbtnRigWellReportForm.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.rbtnRigWellReportForm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbtnRigWellReportForm.Image = ((System.Drawing.Image)(resources.GetObject("rbtnRigWellReportForm.Image")));
            this.rbtnRigWellReportForm.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.rbtnRigWellReportForm.Location = new System.Drawing.Point(6, 115);
            this.rbtnRigWellReportForm.Margin = new System.Windows.Forms.Padding(0);
            this.rbtnRigWellReportForm.Name = "rbtnRigWellReportForm";
            this.rbtnRigWellReportForm.Size = new System.Drawing.Size(160, 25);
            this.rbtnRigWellReportForm.TabIndex = 5;
            this.rbtnRigWellReportForm.Text = "Rig-Well : Report";
            this.rbtnRigWellReportForm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbtnRigWellReportForm.UseVisualStyleBackColor = true;
            this.rbtnRigWellReportForm.CheckedChanged += new System.EventHandler(this.rbtnForm_CheckedChanged);
            // 
            // rbtnWellInfoForm
            // 
            this.rbtnWellInfoForm.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbtnWellInfoForm.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.rbtnWellInfoForm.FlatAppearance.CheckedBackColor = System.Drawing.Color.Silver;
            this.rbtnWellInfoForm.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.rbtnWellInfoForm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbtnWellInfoForm.Image = ((System.Drawing.Image)(resources.GetObject("rbtnWellInfoForm.Image")));
            this.rbtnWellInfoForm.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.rbtnWellInfoForm.Location = new System.Drawing.Point(90, 87);
            this.rbtnWellInfoForm.Margin = new System.Windows.Forms.Padding(0);
            this.rbtnWellInfoForm.Name = "rbtnWellInfoForm";
            this.rbtnWellInfoForm.Size = new System.Drawing.Size(76, 25);
            this.rbtnWellInfoForm.TabIndex = 4;
            this.rbtnWellInfoForm.Text = "Well";
            this.rbtnWellInfoForm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbtnWellInfoForm.UseVisualStyleBackColor = true;
            this.rbtnWellInfoForm.CheckedChanged += new System.EventHandler(this.rbtnForm_CheckedChanged);
            // 
            // rbtnStartupForm
            // 
            this.rbtnStartupForm.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbtnStartupForm.BackColor = System.Drawing.SystemColors.Control;
            this.rbtnStartupForm.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.rbtnStartupForm.FlatAppearance.CheckedBackColor = System.Drawing.Color.Silver;
            this.rbtnStartupForm.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.rbtnStartupForm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbtnStartupForm.Image = ((System.Drawing.Image)(resources.GetObject("rbtnStartupForm.Image")));
            this.rbtnStartupForm.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.rbtnStartupForm.Location = new System.Drawing.Point(6, 2);
            this.rbtnStartupForm.Margin = new System.Windows.Forms.Padding(0);
            this.rbtnStartupForm.Name = "rbtnStartupForm";
            this.rbtnStartupForm.Size = new System.Drawing.Size(160, 25);
            this.rbtnStartupForm.TabIndex = 0;
            this.rbtnStartupForm.Text = "Startup";
            this.rbtnStartupForm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbtnStartupForm.UseVisualStyleBackColor = false;
            this.rbtnStartupForm.CheckedChanged += new System.EventHandler(this.rbtnForm_CheckedChanged);
            // 
            // rbtnSolidAnaForm
            // 
            this.rbtnSolidAnaForm.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbtnSolidAnaForm.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.rbtnSolidAnaForm.FlatAppearance.CheckedBackColor = System.Drawing.Color.Silver;
            this.rbtnSolidAnaForm.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.rbtnSolidAnaForm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbtnSolidAnaForm.Image = ((System.Drawing.Image)(resources.GetObject("rbtnSolidAnaForm.Image")));
            this.rbtnSolidAnaForm.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.rbtnSolidAnaForm.Location = new System.Drawing.Point(6, 325);
            this.rbtnSolidAnaForm.Margin = new System.Windows.Forms.Padding(0);
            this.rbtnSolidAnaForm.Name = "rbtnSolidAnaForm";
            this.rbtnSolidAnaForm.Size = new System.Drawing.Size(160, 25);
            this.rbtnSolidAnaForm.TabIndex = 11;
            this.rbtnSolidAnaForm.Text = "Solid Analysis";
            this.rbtnSolidAnaForm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbtnSolidAnaForm.UseVisualStyleBackColor = true;
            this.rbtnSolidAnaForm.CheckedChanged += new System.EventHandler(this.rbtnForm_CheckedChanged);
            // 
            // rbtnMudPropForm
            // 
            this.rbtnMudPropForm.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbtnMudPropForm.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.rbtnMudPropForm.FlatAppearance.CheckedBackColor = System.Drawing.Color.Silver;
            this.rbtnMudPropForm.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.rbtnMudPropForm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbtnMudPropForm.Image = ((System.Drawing.Image)(resources.GetObject("rbtnMudPropForm.Image")));
            this.rbtnMudPropForm.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.rbtnMudPropForm.Location = new System.Drawing.Point(6, 269);
            this.rbtnMudPropForm.Margin = new System.Windows.Forms.Padding(0);
            this.rbtnMudPropForm.Name = "rbtnMudPropForm";
            this.rbtnMudPropForm.Size = new System.Drawing.Size(160, 25);
            this.rbtnMudPropForm.TabIndex = 10;
            this.rbtnMudPropForm.Text = "Mud Properties";
            this.rbtnMudPropForm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbtnMudPropForm.UseVisualStyleBackColor = true;
            this.rbtnMudPropForm.CheckedChanged += new System.EventHandler(this.rbtnForm_CheckedChanged);
            // 
            // rbtnSolidConEqForm
            // 
            this.rbtnSolidConEqForm.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbtnSolidConEqForm.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.rbtnSolidConEqForm.FlatAppearance.CheckedBackColor = System.Drawing.Color.Silver;
            this.rbtnSolidConEqForm.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.rbtnSolidConEqForm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbtnSolidConEqForm.Image = ((System.Drawing.Image)(resources.GetObject("rbtnSolidConEqForm.Image")));
            this.rbtnSolidConEqForm.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.rbtnSolidConEqForm.Location = new System.Drawing.Point(6, 143);
            this.rbtnSolidConEqForm.Margin = new System.Windows.Forms.Padding(0);
            this.rbtnSolidConEqForm.Name = "rbtnSolidConEqForm";
            this.rbtnSolidConEqForm.Size = new System.Drawing.Size(160, 39);
            this.rbtnSolidConEqForm.TabIndex = 6;
            this.rbtnSolidConEqForm.Text = "Solid Control Equipment\r\n/MudPump";
            this.rbtnSolidConEqForm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbtnSolidConEqForm.UseVisualStyleBackColor = true;
            this.rbtnSolidConEqForm.CheckedChanged += new System.EventHandler(this.rbtnForm_CheckedChanged);
            // 
            // rbtnDrilliOpForm
            // 
            this.rbtnDrilliOpForm.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbtnDrilliOpForm.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.rbtnDrilliOpForm.FlatAppearance.CheckedBackColor = System.Drawing.Color.Silver;
            this.rbtnDrilliOpForm.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.rbtnDrilliOpForm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbtnDrilliOpForm.Image = ((System.Drawing.Image)(resources.GetObject("rbtnDrilliOpForm.Image")));
            this.rbtnDrilliOpForm.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.rbtnDrilliOpForm.Location = new System.Drawing.Point(6, 185);
            this.rbtnDrilliOpForm.Margin = new System.Windows.Forms.Padding(0);
            this.rbtnDrilliOpForm.Name = "rbtnDrilliOpForm";
            this.rbtnDrilliOpForm.Size = new System.Drawing.Size(160, 25);
            this.rbtnDrilliOpForm.TabIndex = 7;
            this.rbtnDrilliOpForm.Text = "Drilling Operation";
            this.rbtnDrilliOpForm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbtnDrilliOpForm.UseVisualStyleBackColor = true;
            this.rbtnDrilliOpForm.CheckedChanged += new System.EventHandler(this.rbtnForm_CheckedChanged);
            // 
            // rbtnMudLossForm
            // 
            this.rbtnMudLossForm.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbtnMudLossForm.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.rbtnMudLossForm.FlatAppearance.CheckedBackColor = System.Drawing.Color.Silver;
            this.rbtnMudLossForm.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.rbtnMudLossForm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbtnMudLossForm.Image = ((System.Drawing.Image)(resources.GetObject("rbtnMudLossForm.Image")));
            this.rbtnMudLossForm.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.rbtnMudLossForm.Location = new System.Drawing.Point(6, 213);
            this.rbtnMudLossForm.Margin = new System.Windows.Forms.Padding(0);
            this.rbtnMudLossForm.Name = "rbtnMudLossForm";
            this.rbtnMudLossForm.Size = new System.Drawing.Size(160, 25);
            this.rbtnMudLossForm.TabIndex = 8;
            this.rbtnMudLossForm.Text = "Mud Losses";
            this.rbtnMudLossForm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbtnMudLossForm.UseVisualStyleBackColor = true;
            this.rbtnMudLossForm.CheckedChanged += new System.EventHandler(this.rbtnForm_CheckedChanged);
            // 
            // rbtnDrillingFluidsProgramForm
            // 
            this.rbtnDrillingFluidsProgramForm.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbtnDrillingFluidsProgramForm.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.rbtnDrillingFluidsProgramForm.FlatAppearance.CheckedBackColor = System.Drawing.Color.Silver;
            this.rbtnDrillingFluidsProgramForm.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.rbtnDrillingFluidsProgramForm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbtnDrillingFluidsProgramForm.Image = ((System.Drawing.Image)(resources.GetObject("rbtnDrillingFluidsProgramForm.Image")));
            this.rbtnDrillingFluidsProgramForm.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.rbtnDrillingFluidsProgramForm.Location = new System.Drawing.Point(6, 59);
            this.rbtnDrillingFluidsProgramForm.Margin = new System.Windows.Forms.Padding(0);
            this.rbtnDrillingFluidsProgramForm.Name = "rbtnDrillingFluidsProgramForm";
            this.rbtnDrillingFluidsProgramForm.Size = new System.Drawing.Size(160, 25);
            this.rbtnDrillingFluidsProgramForm.TabIndex = 2;
            this.rbtnDrillingFluidsProgramForm.Text = "Drilling Fluids Program";
            this.rbtnDrillingFluidsProgramForm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbtnDrillingFluidsProgramForm.UseVisualStyleBackColor = true;
            this.rbtnDrillingFluidsProgramForm.CheckedChanged += new System.EventHandler(this.rbtnForm_CheckedChanged);
            // 
            // rbtnPorjectForm
            // 
            this.rbtnPorjectForm.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbtnPorjectForm.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.rbtnPorjectForm.FlatAppearance.CheckedBackColor = System.Drawing.Color.Silver;
            this.rbtnPorjectForm.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.rbtnPorjectForm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbtnPorjectForm.Image = ((System.Drawing.Image)(resources.GetObject("rbtnPorjectForm.Image")));
            this.rbtnPorjectForm.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.rbtnPorjectForm.Location = new System.Drawing.Point(6, 31);
            this.rbtnPorjectForm.Margin = new System.Windows.Forms.Padding(0);
            this.rbtnPorjectForm.Name = "rbtnPorjectForm";
            this.rbtnPorjectForm.Size = new System.Drawing.Size(160, 25);
            this.rbtnPorjectForm.TabIndex = 1;
            this.rbtnPorjectForm.Tag = "add click event in addition to checked changed";
            this.rbtnPorjectForm.Text = "Project";
            this.rbtnPorjectForm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbtnPorjectForm.UseVisualStyleBackColor = true;
            this.rbtnPorjectForm.CheckedChanged += new System.EventHandler(this.rbtnForm_CheckedChanged);
            // 
            // rbtnHydraulicForm
            // 
            this.rbtnHydraulicForm.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbtnHydraulicForm.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.rbtnHydraulicForm.FlatAppearance.CheckedBackColor = System.Drawing.Color.Silver;
            this.rbtnHydraulicForm.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.rbtnHydraulicForm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbtnHydraulicForm.Image = ((System.Drawing.Image)(resources.GetObject("rbtnHydraulicForm.Image")));
            this.rbtnHydraulicForm.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.rbtnHydraulicForm.Location = new System.Drawing.Point(6, 297);
            this.rbtnHydraulicForm.Margin = new System.Windows.Forms.Padding(0);
            this.rbtnHydraulicForm.Name = "rbtnHydraulicForm";
            this.rbtnHydraulicForm.Size = new System.Drawing.Size(160, 25);
            this.rbtnHydraulicForm.TabIndex = 11;
            this.rbtnHydraulicForm.Text = "Hydraulic";
            this.rbtnHydraulicForm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbtnHydraulicForm.UseVisualStyleBackColor = true;
            this.rbtnHydraulicForm.CheckedChanged += new System.EventHandler(this.rbtnForm_CheckedChanged);
            // 
            // pnlMain
            // 
            this.pnlMain.AutoScroll = true;
            this.pnlMain.BackColor = System.Drawing.SystemColors.Control;
            this.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(175, 96);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(836, 466);
            this.pnlMain.TabIndex = 2;
            this.pnlMain.Resize += new System.EventHandler(this.pnlMain_Resize);
            // 
            // tmrUpdate
            // 
            this.tmrUpdate.Enabled = true;
            this.tmrUpdate.Interval = 1000;
            this.tmrUpdate.Tick += new System.EventHandler(this.tmrUpdate_Tick);
            // 
            // timer1
            // 
            this.timer1.Interval = 30000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(1011, 562);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(700, 600);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PDF Management";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pboxStartup)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label lblCurDate;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label lblCurTime;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.PictureBox pboxStartup;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton rbtnPorjectForm;
        private System.Windows.Forms.Button btnUnitConv;
        private System.Windows.Forms.Button btnCalc;
        private System.Windows.Forms.RadioButton rbtnMudVolManagementForm;
        private System.Windows.Forms.RadioButton rbtnWellInfoForm;
        private System.Windows.Forms.RadioButton rbtnReportingForm;
        private System.Windows.Forms.RadioButton rbtnRemarkForm;
        private System.Windows.Forms.RadioButton rbtnSolidAnaForm;
        private System.Windows.Forms.RadioButton rbtnInvManagementForm;
        private System.Windows.Forms.RadioButton rbtnMudPropForm;
        private System.Windows.Forms.RadioButton rbtnSolidConEqForm;
        private System.Windows.Forms.RadioButton rbtnDrilliOpForm;
        private System.Windows.Forms.RadioButton rbtnMudLossForm;
        private System.Windows.Forms.RadioButton rbtnRigForm;
        private System.Windows.Forms.RadioButton rbtnDrillingFluidsProgramForm;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnAccount;
        private System.Windows.Forms.Button btnLogOut;
        public System.Windows.Forms.Label lblUserID;
        private System.Windows.Forms.RadioButton rbtnStartupForm;
        public System.Windows.Forms.Label lblCurProject;
        public System.Windows.Forms.Label lblCurRep;
        public System.Windows.Forms.Label lblCurRigWell;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.RadioButton rbtnRigWellReportForm;
        private System.Windows.Forms.Timer tmrUpdate;
        private System.Windows.Forms.Label lblCurDateShamsi;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.RadioButton rbtnHydraulicForm;
    }
}

