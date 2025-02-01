namespace DMR
{
    partial class SolidConEqForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SolidConEqForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvSolidEq = new System.Windows.Forms.DataGridView();
            this.btnSolExport = new System.Windows.Forms.Button();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnSolRefresh = new System.Windows.Forms.Button();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnPumpApply = new System.Windows.Forms.Button();
            this.dgvMudPumps = new System.Windows.Forms.DataGridView();
            this.btnPumpRefresh = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.scerscID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column20 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.scID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pumpTypeVal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pumpBrandVal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pumpModelVal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mpStkRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mpCircRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pumpID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSolidEq)).BeginInit();
            this.tableLayoutPanel.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMudPumps)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvSolidEq
            // 
            this.dgvSolidEq.AllowUserToAddRows = false;
            this.dgvSolidEq.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.dgvSolidEq.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvSolidEq.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSolidEq.BackgroundColor = System.Drawing.Color.White;
            this.dgvSolidEq.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSolidEq.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.scerscID,
            this.Column20,
            this.Column15,
            this.Column14,
            this.Column12,
            this.Column2,
            this.Column3,
            this.Column1,
            this.scID});
            this.dgvSolidEq.EnableHeadersVisualStyles = false;
            this.dgvSolidEq.Location = new System.Drawing.Point(9, 3);
            this.dgvSolidEq.MultiSelect = false;
            this.dgvSolidEq.Name = "dgvSolidEq";
            this.dgvSolidEq.Size = new System.Drawing.Size(593, 182);
            this.dgvSolidEq.TabIndex = 0;
            this.dgvSolidEq.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvSolidEq_CellMouseClick);
            this.dgvSolidEq.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSolidEq_CellValueChanged);
            // 
            // btnSolExport
            // 
            this.btnSolExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSolExport.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSolExport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSolExport.ImageKey = "Up";
            this.btnSolExport.ImageList = this.imageList;
            this.btnSolExport.Location = new System.Drawing.Point(542, 191);
            this.btnSolExport.Name = "btnSolExport";
            this.btnSolExport.Size = new System.Drawing.Size(60, 23);
            this.btnSolExport.TabIndex = 2;
            this.btnSolExport.Text = "Export";
            this.btnSolExport.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSolExport.UseVisualStyleBackColor = true;
            this.btnSolExport.Click += new System.EventHandler(this.btnSolExport_Click);
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
            // btnSolRefresh
            // 
            this.btnSolRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSolRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSolRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSolRefresh.ImageKey = "refresh";
            this.btnSolRefresh.ImageList = this.imageList;
            this.btnSolRefresh.Location = new System.Drawing.Point(9, 191);
            this.btnSolRefresh.Name = "btnSolRefresh";
            this.btnSolRefresh.Size = new System.Drawing.Size(71, 23);
            this.btnSolRefresh.TabIndex = 1;
            this.btnSolRefresh.Text = "Refresh";
            this.btnSolRefresh.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSolRefresh.UseVisualStyleBackColor = true;
            this.btnSolRefresh.Click += new System.EventHandler(this.btnSolRefresh_Click);
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 1;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 2;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(617, 446);
            this.tableLayoutPanel.TabIndex = 188;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnPumpApply);
            this.panel2.Controls.Add(this.dgvMudPumps);
            this.panel2.Controls.Add(this.btnPumpRefresh);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 226);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(611, 217);
            this.panel2.TabIndex = 1;
            // 
            // btnPumpApply
            // 
            this.btnPumpApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPumpApply.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPumpApply.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPumpApply.ImageKey = "Check";
            this.btnPumpApply.ImageList = this.imageList;
            this.btnPumpApply.Location = new System.Drawing.Point(86, 191);
            this.btnPumpApply.Name = "btnPumpApply";
            this.btnPumpApply.Size = new System.Drawing.Size(103, 23);
            this.btnPumpApply.TabIndex = 2;
            this.btnPumpApply.Tag = "";
            this.btnPumpApply.Text = "Apply Changes";
            this.btnPumpApply.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPumpApply.UseVisualStyleBackColor = true;
            this.btnPumpApply.Click += new System.EventHandler(this.btnPumpApply_Click);
            // 
            // dgvMudPumps
            // 
            this.dgvMudPumps.AllowUserToAddRows = false;
            this.dgvMudPumps.AllowUserToDeleteRows = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.dgvMudPumps.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvMudPumps.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvMudPumps.BackgroundColor = System.Drawing.Color.White;
            this.dgvMudPumps.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMudPumps.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.dataGridViewTextBoxColumn1,
            this.pumpTypeVal,
            this.pumpBrandVal,
            this.pumpModelVal,
            this.Column5,
            this.Column6,
            this.mpStkRate,
            this.mpCircRate,
            this.pumpID});
            this.dgvMudPumps.EnableHeadersVisualStyles = false;
            this.dgvMudPumps.Location = new System.Drawing.Point(9, 3);
            this.dgvMudPumps.MultiSelect = false;
            this.dgvMudPumps.Name = "dgvMudPumps";
            this.dgvMudPumps.Size = new System.Drawing.Size(593, 182);
            this.dgvMudPumps.TabIndex = 0;
            this.dgvMudPumps.Tag = "";
            this.dgvMudPumps.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMudPumps_CellValueChanged);
            // 
            // btnPumpRefresh
            // 
            this.btnPumpRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPumpRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPumpRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPumpRefresh.ImageKey = "refresh";
            this.btnPumpRefresh.ImageList = this.imageList;
            this.btnPumpRefresh.Location = new System.Drawing.Point(9, 191);
            this.btnPumpRefresh.Name = "btnPumpRefresh";
            this.btnPumpRefresh.Size = new System.Drawing.Size(71, 23);
            this.btnPumpRefresh.TabIndex = 1;
            this.btnPumpRefresh.Text = "Refresh";
            this.btnPumpRefresh.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPumpRefresh.UseVisualStyleBackColor = true;
            this.btnPumpRefresh.Click += new System.EventHandler(this.btnPumpRefresh_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSolExport);
            this.panel1.Controls.Add(this.dgvSolidEq);
            this.panel1.Controls.Add(this.btnSolRefresh);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(611, 217);
            this.panel1.TabIndex = 0;
            // 
            // scerscID
            // 
            this.scerscID.HeaderText = "";
            this.scerscID.Name = "scerscID";
            this.scerscID.ReadOnly = true;
            this.scerscID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.scerscID.Visible = false;
            // 
            // Column20
            // 
            this.Column20.HeaderText = "Name";
            this.Column20.Name = "Column20";
            this.Column20.ReadOnly = true;
            this.Column20.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.Column20.Width = 150;
            // 
            // Column15
            // 
            this.Column15.HeaderText = "#";
            this.Column15.Name = "Column15";
            this.Column15.ReadOnly = true;
            this.Column15.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.Column15.Width = 40;
            // 
            // Column14
            // 
            this.Column14.HeaderText = "Brand";
            this.Column14.Name = "Column14";
            this.Column14.ReadOnly = true;
            this.Column14.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // Column12
            // 
            this.Column12.HeaderText = "Model";
            this.Column12.Name = "Column12";
            this.Column12.ReadOnly = true;
            this.Column12.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Specification";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // Column3
            // 
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Column3.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column3.HeaderText = "Screen Sizes";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.Column3.Width = 300;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.HeaderText = "Used/Lost";
            this.Column1.MinimumWidth = 150;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // scID
            // 
            this.scID.HeaderText = "";
            this.scID.Name = "scID";
            this.scID.ReadOnly = true;
            this.scID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.scID.Visible = false;
            // 
            // id
            // 
            this.id.HeaderText = "";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.id.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.id.Visible = false;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Pump #";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // pumpTypeVal
            // 
            this.pumpTypeVal.HeaderText = "Pump Type";
            this.pumpTypeVal.Name = "pumpTypeVal";
            this.pumpTypeVal.ReadOnly = true;
            this.pumpTypeVal.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // pumpBrandVal
            // 
            this.pumpBrandVal.HeaderText = "Brand";
            this.pumpBrandVal.Name = "pumpBrandVal";
            this.pumpBrandVal.ReadOnly = true;
            this.pumpBrandVal.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // pumpModelVal
            // 
            this.pumpModelVal.HeaderText = "Model";
            this.pumpModelVal.Name = "pumpModelVal";
            this.pumpModelVal.ReadOnly = true;
            this.pumpModelVal.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Efficiency (%)";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Vol/Stk (bbl/stk?)";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.Column6.Width = 150;
            // 
            // mpStkRate
            // 
            this.mpStkRate.HeaderText = "Stk Rate (SPM)";
            this.mpStkRate.Name = "mpStkRate";
            this.mpStkRate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.mpStkRate.Width = 150;
            // 
            // mpCircRate
            // 
            this.mpCircRate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.mpCircRate.HeaderText = "Circulation Rate (bbl/min?)";
            this.mpCircRate.MinimumWidth = 150;
            this.mpCircRate.Name = "mpCircRate";
            this.mpCircRate.ReadOnly = true;
            this.mpCircRate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // pumpID
            // 
            this.pumpID.HeaderText = "";
            this.pumpID.Name = "pumpID";
            this.pumpID.ReadOnly = true;
            this.pumpID.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.pumpID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.pumpID.Visible = false;
            // 
            // SolidConEqForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(617, 446);
            this.Controls.Add(this.tableLayoutPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(617, 339);
            this.Name = "SolidConEqForm";
            this.Text = "Porject";
            this.Load += new System.EventHandler(this.SolidConEqForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSolidEq)).EndInit();
            this.tableLayoutPanel.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMudPumps)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvSolidEq;
        private System.Windows.Forms.Button btnSolExport;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.Button btnSolRefresh;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnPumpRefresh;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.DataGridView dgvMudPumps;
        private System.Windows.Forms.Button btnPumpApply;
        private System.Windows.Forms.DataGridViewTextBoxColumn scerscID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column20;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column15;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column14;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn scID;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn pumpTypeVal;
        private System.Windows.Forms.DataGridViewTextBoxColumn pumpBrandVal;
        private System.Windows.Forms.DataGridViewTextBoxColumn pumpModelVal;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn mpStkRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn mpCircRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn pumpID;
    }
}