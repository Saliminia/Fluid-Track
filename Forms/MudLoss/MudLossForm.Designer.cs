namespace DMR
{
    partial class MudLossForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MudLossForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tcMudLoss = new System.Windows.Forms.TabControl();
            this.tpFormationLoss = new System.Windows.Forms.TabPage();
            this.btnFLRefresh = new System.Windows.Forms.Button();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.dgvFormationLoss = new System.Windows.Forms.DataGridView();
            this.formationID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column18 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnFLAdd = new System.Windows.Forms.Button();
            this.btnFLRemove = new System.Windows.Forms.Button();
            this.btnFLApply = new System.Windows.Forms.Button();
            this.tpLossesRecord = new System.Windows.Forms.TabPage();
            this.dgvLossesRecord = new System.Windows.Forms.DataGridView();
            this.lossID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Losses_PredefAutoID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column17 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewComboBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnLRRefresh = new System.Windows.Forms.Button();
            this.btnLRAdd = new System.Windows.Forms.Button();
            this.btnLRApply = new System.Windows.Forms.Button();
            this.btnLRRemove = new System.Windows.Forms.Button();
            this.tcMudLoss.SuspendLayout();
            this.tpFormationLoss.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFormationLoss)).BeginInit();
            this.tpLossesRecord.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLossesRecord)).BeginInit();
            this.SuspendLayout();
            // 
            // tcMudLoss
            // 
            this.tcMudLoss.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tcMudLoss.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tcMudLoss.Controls.Add(this.tpFormationLoss);
            this.tcMudLoss.Controls.Add(this.tpLossesRecord);
            this.tcMudLoss.Location = new System.Drawing.Point(12, 12);
            this.tcMudLoss.Name = "tcMudLoss";
            this.tcMudLoss.SelectedIndex = 0;
            this.tcMudLoss.Size = new System.Drawing.Size(657, 414);
            this.tcMudLoss.TabIndex = 0;
            // 
            // tpFormationLoss
            // 
            this.tpFormationLoss.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tpFormationLoss.Controls.Add(this.btnFLRefresh);
            this.tpFormationLoss.Controls.Add(this.dgvFormationLoss);
            this.tpFormationLoss.Controls.Add(this.btnFLAdd);
            this.tpFormationLoss.Controls.Add(this.btnFLRemove);
            this.tpFormationLoss.Controls.Add(this.btnFLApply);
            this.tpFormationLoss.Location = new System.Drawing.Point(4, 25);
            this.tpFormationLoss.Name = "tpFormationLoss";
            this.tpFormationLoss.Padding = new System.Windows.Forms.Padding(3);
            this.tpFormationLoss.Size = new System.Drawing.Size(649, 385);
            this.tpFormationLoss.TabIndex = 1;
            this.tpFormationLoss.Text = "Formation Loss";
            this.tpFormationLoss.UseVisualStyleBackColor = true;
            // 
            // btnFLRefresh
            // 
            this.btnFLRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnFLRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnFLRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFLRefresh.ImageKey = "refresh";
            this.btnFLRefresh.ImageList = this.imageList;
            this.btnFLRefresh.Location = new System.Drawing.Point(248, 354);
            this.btnFLRefresh.Name = "btnFLRefresh";
            this.btnFLRefresh.Size = new System.Drawing.Size(71, 23);
            this.btnFLRefresh.TabIndex = 4;
            this.btnFLRefresh.Text = "Refresh";
            this.btnFLRefresh.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnFLRefresh.UseVisualStyleBackColor = true;
            this.btnFLRefresh.Click += new System.EventHandler(this.btnFLRefresh_Click);
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
            this.imageList.Images.SetKeyName(13, "Left");
            this.imageList.Images.SetKeyName(14, "Up");
            this.imageList.Images.SetKeyName(15, "Down");
            this.imageList.Images.SetKeyName(16, "report");
            this.imageList.Images.SetKeyName(17, "revision");
            this.imageList.Images.SetKeyName(18, "refresh");
            this.imageList.Images.SetKeyName(19, "forbidden.png");
            this.imageList.Images.SetKeyName(20, "point_hand.png");
            this.imageList.Images.SetKeyName(21, "noFilter");
            this.imageList.Images.SetKeyName(22, "applyFilter");
            this.imageList.Images.SetKeyName(23, "bookmark.png");
            this.imageList.Images.SetKeyName(24, "flask.png");
            this.imageList.Images.SetKeyName(25, "eject.png");
            this.imageList.Images.SetKeyName(26, "user_clear.png");
            this.imageList.Images.SetKeyName(27, "open");
            // 
            // dgvFormationLoss
            // 
            this.dgvFormationLoss.AllowUserToAddRows = false;
            this.dgvFormationLoss.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.dgvFormationLoss.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvFormationLoss.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvFormationLoss.BackgroundColor = System.Drawing.Color.White;
            this.dgvFormationLoss.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFormationLoss.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.formationID,
            this.Column14,
            this.Column16,
            this.Column5,
            this.Column8,
            this.Column9,
            this.Column18,
            this.Column3,
            this.Column1,
            this.Column10,
            this.Column11,
            this.Column12,
            this.Column4,
            this.Column6,
            this.Column13,
            this.Column7});
            this.dgvFormationLoss.EnableHeadersVisualStyles = false;
            this.dgvFormationLoss.Location = new System.Drawing.Point(2, 2);
            this.dgvFormationLoss.MultiSelect = false;
            this.dgvFormationLoss.Name = "dgvFormationLoss";
            this.dgvFormationLoss.Size = new System.Drawing.Size(639, 346);
            this.dgvFormationLoss.TabIndex = 0;
            this.dgvFormationLoss.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvFormationLoss_CellMouseClick);
            this.dgvFormationLoss.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFormationLoss_CellValueChanged);
            // 
            // formationID
            // 
            this.formationID.HeaderText = "";
            this.formationID.Name = "formationID";
            this.formationID.ReadOnly = true;
            this.formationID.Visible = false;
            // 
            // Column14
            // 
            this.Column14.HeaderText = "Time Interval";
            this.Column14.Name = "Column14";
            this.Column14.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // Column16
            // 
            this.Column16.HeaderText = "dfsAutoID_Formation";
            this.Column16.Name = "Column16";
            this.Column16.ReadOnly = true;
            this.Column16.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column16.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.Column16.Visible = false;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Drilling Fluid System";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "MW (?)";
            this.Column8.Name = "Column8";
            this.Column8.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // Column9
            // 
            this.Column9.HeaderText = "Daily Activity";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            this.Column9.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column9.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // Column18
            // 
            this.Column18.HeaderText = "iadcAutoID";
            this.Column18.Name = "Column18";
            this.Column18.ReadOnly = true;
            this.Column18.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.Column18.Visible = false;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Depth (m?)";
            this.Column3.Name = "Column3";
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Vol. (bbl?)";
            this.Column1.Name = "Column1";
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // Column10
            // 
            this.Column10.HeaderText = "Loss Rate max(bbl/hr?)";
            this.Column10.Name = "Column10";
            this.Column10.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // Column11
            // 
            this.Column11.HeaderText = "Loss Rate min(bbl/hr?)";
            this.Column11.Name = "Column11";
            this.Column11.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // Column12
            // 
            this.Column12.HeaderText = "SPP (psi?)";
            this.Column12.Name = "Column12";
            this.Column12.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Flow Rate (gpm?)";
            this.Column4.Name = "Column4";
            this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Porosity (%)";
            this.Column6.Name = "Column6";
            this.Column6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // Column13
            // 
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Column13.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column13.HeaderText = "Additives";
            this.Column13.Name = "Column13";
            this.Column13.ReadOnly = true;
            this.Column13.Width = 150;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "Remedial Action";
            this.Column7.Name = "Column7";
            this.Column7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.Column7.Width = 250;
            // 
            // btnFLAdd
            // 
            this.btnFLAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnFLAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnFLAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFLAdd.ImageKey = "Add";
            this.btnFLAdd.ImageList = this.imageList;
            this.btnFLAdd.Location = new System.Drawing.Point(6, 354);
            this.btnFLAdd.Name = "btnFLAdd";
            this.btnFLAdd.Size = new System.Drawing.Size(49, 23);
            this.btnFLAdd.TabIndex = 1;
            this.btnFLAdd.Tag = "From Drill Pipe/Heavy Weight/Drill Color";
            this.btnFLAdd.Text = "Add";
            this.btnFLAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnFLAdd.UseVisualStyleBackColor = true;
            this.btnFLAdd.Click += new System.EventHandler(this.btnFLAdd_Click);
            // 
            // btnFLRemove
            // 
            this.btnFLRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnFLRemove.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnFLRemove.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFLRemove.ImageKey = "Remove";
            this.btnFLRemove.ImageList = this.imageList;
            this.btnFLRemove.Location = new System.Drawing.Point(171, 354);
            this.btnFLRemove.Name = "btnFLRemove";
            this.btnFLRemove.Size = new System.Drawing.Size(71, 23);
            this.btnFLRemove.TabIndex = 3;
            this.btnFLRemove.Text = "Remove";
            this.btnFLRemove.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnFLRemove.UseVisualStyleBackColor = true;
            this.btnFLRemove.Click += new System.EventHandler(this.btnFLRemove_Click);
            // 
            // btnFLApply
            // 
            this.btnFLApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnFLApply.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnFLApply.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFLApply.ImageKey = "Check";
            this.btnFLApply.ImageList = this.imageList;
            this.btnFLApply.Location = new System.Drawing.Point(61, 354);
            this.btnFLApply.Name = "btnFLApply";
            this.btnFLApply.Size = new System.Drawing.Size(104, 23);
            this.btnFLApply.TabIndex = 2;
            this.btnFLApply.Text = "Apply Changes";
            this.btnFLApply.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnFLApply.UseVisualStyleBackColor = true;
            this.btnFLApply.Click += new System.EventHandler(this.btnFLApply_Click);
            // 
            // tpLossesRecord
            // 
            this.tpLossesRecord.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tpLossesRecord.Controls.Add(this.dgvLossesRecord);
            this.tpLossesRecord.Controls.Add(this.btnLRRefresh);
            this.tpLossesRecord.Controls.Add(this.btnLRAdd);
            this.tpLossesRecord.Controls.Add(this.btnLRApply);
            this.tpLossesRecord.Controls.Add(this.btnLRRemove);
            this.tpLossesRecord.Location = new System.Drawing.Point(4, 25);
            this.tpLossesRecord.Name = "tpLossesRecord";
            this.tpLossesRecord.Size = new System.Drawing.Size(649, 385);
            this.tpLossesRecord.TabIndex = 2;
            this.tpLossesRecord.Text = "Losses Record";
            this.tpLossesRecord.UseVisualStyleBackColor = true;
            // 
            // dgvLossesRecord
            // 
            this.dgvLossesRecord.AllowUserToAddRows = false;
            this.dgvLossesRecord.AllowUserToDeleteRows = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.dgvLossesRecord.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvLossesRecord.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvLossesRecord.BackgroundColor = System.Drawing.Color.White;
            this.dgvLossesRecord.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLossesRecord.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.lossID,
            this.Column15,
            this.Column2,
            this.Losses_PredefAutoID,
            this.Column17,
            this.dataGridViewComboBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn6});
            this.dgvLossesRecord.EnableHeadersVisualStyles = false;
            this.dgvLossesRecord.Location = new System.Drawing.Point(3, 3);
            this.dgvLossesRecord.MultiSelect = false;
            this.dgvLossesRecord.Name = "dgvLossesRecord";
            this.dgvLossesRecord.Size = new System.Drawing.Size(641, 348);
            this.dgvLossesRecord.TabIndex = 0;
            this.dgvLossesRecord.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvLossesRecord_CellMouseClick);
            this.dgvLossesRecord.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLossesRecord_CellValueChanged);
            // 
            // lossID
            // 
            this.lossID.HeaderText = "";
            this.lossID.Name = "lossID";
            this.lossID.ReadOnly = true;
            this.lossID.Visible = false;
            // 
            // Column15
            // 
            this.Column15.HeaderText = "Time Interval";
            this.Column15.Name = "Column15";
            this.Column15.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Losses";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.Column2.Width = 150;
            // 
            // Losses_PredefAutoID
            // 
            this.Losses_PredefAutoID.HeaderText = "";
            this.Losses_PredefAutoID.Name = "Losses_PredefAutoID";
            this.Losses_PredefAutoID.ReadOnly = true;
            this.Losses_PredefAutoID.Visible = false;
            // 
            // Column17
            // 
            this.Column17.HeaderText = "dfsAutoID_Loss";
            this.Column17.Name = "Column17";
            this.Column17.ReadOnly = true;
            this.Column17.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.Column17.Visible = false;
            // 
            // dataGridViewComboBoxColumn2
            // 
            this.dataGridViewComboBoxColumn2.HeaderText = "Drilling Fluid System";
            this.dataGridViewComboBoxColumn2.Name = "dataGridViewComboBoxColumn2";
            this.dataGridViewComboBoxColumn2.ReadOnly = true;
            this.dataGridViewComboBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewComboBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.dataGridViewComboBoxColumn2.Width = 150;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Vol. (bbl?)";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn6.HeaderText = "Remedial Action";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // btnLRRefresh
            // 
            this.btnLRRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLRRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnLRRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLRRefresh.ImageKey = "refresh";
            this.btnLRRefresh.ImageList = this.imageList;
            this.btnLRRefresh.Location = new System.Drawing.Point(245, 357);
            this.btnLRRefresh.Name = "btnLRRefresh";
            this.btnLRRefresh.Size = new System.Drawing.Size(71, 23);
            this.btnLRRefresh.TabIndex = 4;
            this.btnLRRefresh.Text = "Refresh";
            this.btnLRRefresh.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLRRefresh.UseVisualStyleBackColor = true;
            this.btnLRRefresh.Click += new System.EventHandler(this.btnLRRefresh_Click);
            // 
            // btnLRAdd
            // 
            this.btnLRAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLRAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnLRAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLRAdd.ImageKey = "Add";
            this.btnLRAdd.ImageList = this.imageList;
            this.btnLRAdd.Location = new System.Drawing.Point(3, 357);
            this.btnLRAdd.Name = "btnLRAdd";
            this.btnLRAdd.Size = new System.Drawing.Size(49, 23);
            this.btnLRAdd.TabIndex = 1;
            this.btnLRAdd.Tag = "From Drill Pipe/Heavy Weight/Drill Color";
            this.btnLRAdd.Text = "Add";
            this.btnLRAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLRAdd.UseVisualStyleBackColor = true;
            this.btnLRAdd.Click += new System.EventHandler(this.btnLRAdd_Click);
            // 
            // btnLRApply
            // 
            this.btnLRApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLRApply.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnLRApply.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLRApply.ImageKey = "Check";
            this.btnLRApply.ImageList = this.imageList;
            this.btnLRApply.Location = new System.Drawing.Point(58, 357);
            this.btnLRApply.Name = "btnLRApply";
            this.btnLRApply.Size = new System.Drawing.Size(104, 23);
            this.btnLRApply.TabIndex = 2;
            this.btnLRApply.Text = "Apply Changes";
            this.btnLRApply.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLRApply.UseVisualStyleBackColor = true;
            this.btnLRApply.Click += new System.EventHandler(this.btnLRApply_Click);
            // 
            // btnLRRemove
            // 
            this.btnLRRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLRRemove.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnLRRemove.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLRRemove.ImageKey = "Remove";
            this.btnLRRemove.ImageList = this.imageList;
            this.btnLRRemove.Location = new System.Drawing.Point(168, 357);
            this.btnLRRemove.Name = "btnLRRemove";
            this.btnLRRemove.Size = new System.Drawing.Size(71, 23);
            this.btnLRRemove.TabIndex = 3;
            this.btnLRRemove.Text = "Remove";
            this.btnLRRemove.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLRRemove.UseVisualStyleBackColor = true;
            this.btnLRRemove.Click += new System.EventHandler(this.btnLRRemove_Click);
            // 
            // MudLossForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(681, 438);
            this.Controls.Add(this.tcMudLoss);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimumSize = new System.Drawing.Size(681, 438);
            this.Name = "MudLossForm";
            this.Text = "MudLossForm";
            this.Load += new System.EventHandler(this.MudLossForm_Load);
            this.tcMudLoss.ResumeLayout(false);
            this.tpFormationLoss.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFormationLoss)).EndInit();
            this.tpLossesRecord.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLossesRecord)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcMudLoss;
        private System.Windows.Forms.TabPage tpFormationLoss;
        private System.Windows.Forms.DataGridView dgvFormationLoss;
        private System.Windows.Forms.TabPage tpLossesRecord;
        private System.Windows.Forms.DataGridView dgvLossesRecord;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.Button btnFLRefresh;
        private System.Windows.Forms.Button btnFLRemove;
        private System.Windows.Forms.Button btnFLAdd;
        private System.Windows.Forms.Button btnFLApply;
        private System.Windows.Forms.Button btnLRRefresh;
        private System.Windows.Forms.Button btnLRRemove;
        private System.Windows.Forms.Button btnLRAdd;
        private System.Windows.Forms.Button btnLRApply;
        private System.Windows.Forms.DataGridViewTextBoxColumn lossID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column15;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Losses_PredefAutoID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column17;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewComboBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn formationID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column14;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column16;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column18;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column13;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;

    }
}