namespace DMR
{
    partial class MudPropForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MudPropForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tcMudProp = new System.Windows.Forms.TabControl();
            this.tpMudProp = new System.Windows.Forms.TabPage();
            this.btnMudPropRemove = new System.Windows.Forms.Button();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.btnMudPropAdd = new System.Windows.Forms.Button();
            this.btnMudPropRefresh = new System.Windows.Forms.Button();
            this.rbtnPeriod18_24 = new System.Windows.Forms.RadioButton();
            this.rbtnPeriod12_18 = new System.Windows.Forms.RadioButton();
            this.rbtnPeriod06_12 = new System.Windows.Forms.RadioButton();
            this.rbtnPeriod00_06 = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.btnMudPropApply = new System.Windows.Forms.Button();
            this.dgvMudProp = new System.Windows.Forms.DataGridView();
            this.mudPlaceHolder = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tpWater = new System.Windows.Forms.TabPage();
            this.btnWaterRemove = new System.Windows.Forms.Button();
            this.btnWaterRefresh = new System.Windows.Forms.Button();
            this.btnWaterAdd = new System.Windows.Forms.Button();
            this.btnWaterApply = new System.Windows.Forms.Button();
            this.dgvWater = new System.Windows.Forms.DataGridView();
            this.waterPlaceHolder = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tcMudProp.SuspendLayout();
            this.tpMudProp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMudProp)).BeginInit();
            this.tpWater.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWater)).BeginInit();
            this.SuspendLayout();
            // 
            // tcMudProp
            // 
            this.tcMudProp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tcMudProp.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tcMudProp.Controls.Add(this.tpMudProp);
            this.tcMudProp.Controls.Add(this.tpWater);
            this.tcMudProp.Location = new System.Drawing.Point(12, 12);
            this.tcMudProp.Name = "tcMudProp";
            this.tcMudProp.SelectedIndex = 0;
            this.tcMudProp.Size = new System.Drawing.Size(667, 378);
            this.tcMudProp.TabIndex = 0;
            // 
            // tpMudProp
            // 
            this.tpMudProp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tpMudProp.Controls.Add(this.btnMudPropRemove);
            this.tpMudProp.Controls.Add(this.btnMudPropAdd);
            this.tpMudProp.Controls.Add(this.btnMudPropRefresh);
            this.tpMudProp.Controls.Add(this.rbtnPeriod18_24);
            this.tpMudProp.Controls.Add(this.rbtnPeriod12_18);
            this.tpMudProp.Controls.Add(this.rbtnPeriod06_12);
            this.tpMudProp.Controls.Add(this.rbtnPeriod00_06);
            this.tpMudProp.Controls.Add(this.label1);
            this.tpMudProp.Controls.Add(this.btnMudPropApply);
            this.tpMudProp.Controls.Add(this.dgvMudProp);
            this.tpMudProp.Location = new System.Drawing.Point(4, 25);
            this.tpMudProp.Name = "tpMudProp";
            this.tpMudProp.Padding = new System.Windows.Forms.Padding(3);
            this.tpMudProp.Size = new System.Drawing.Size(659, 349);
            this.tpMudProp.TabIndex = 0;
            this.tpMudProp.Text = "Mud Properties";
            this.tpMudProp.UseVisualStyleBackColor = true;
            // 
            // btnMudPropRemove
            // 
            this.btnMudPropRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnMudPropRemove.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnMudPropRemove.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMudPropRemove.ImageKey = "Remove";
            this.btnMudPropRemove.ImageList = this.imageList;
            this.btnMudPropRemove.Location = new System.Drawing.Point(176, 318);
            this.btnMudPropRemove.Name = "btnMudPropRemove";
            this.btnMudPropRemove.Size = new System.Drawing.Size(71, 23);
            this.btnMudPropRemove.TabIndex = 7;
            this.btnMudPropRemove.Text = "Remove";
            this.btnMudPropRemove.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnMudPropRemove.UseVisualStyleBackColor = true;
            this.btnMudPropRemove.Click += new System.EventHandler(this.btnMudPropRemove_Click);
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
            // btnMudPropAdd
            // 
            this.btnMudPropAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnMudPropAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnMudPropAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMudPropAdd.ImageKey = "add2";
            this.btnMudPropAdd.ImageList = this.imageList;
            this.btnMudPropAdd.Location = new System.Drawing.Point(6, 318);
            this.btnMudPropAdd.Name = "btnMudPropAdd";
            this.btnMudPropAdd.Size = new System.Drawing.Size(54, 23);
            this.btnMudPropAdd.TabIndex = 5;
            this.btnMudPropAdd.Text = "Add";
            this.btnMudPropAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnMudPropAdd.UseVisualStyleBackColor = true;
            this.btnMudPropAdd.Click += new System.EventHandler(this.btnMudPropAdd_Click);
            // 
            // btnMudPropRefresh
            // 
            this.btnMudPropRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnMudPropRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnMudPropRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMudPropRefresh.ImageKey = "refresh";
            this.btnMudPropRefresh.ImageList = this.imageList;
            this.btnMudPropRefresh.Location = new System.Drawing.Point(253, 318);
            this.btnMudPropRefresh.Name = "btnMudPropRefresh";
            this.btnMudPropRefresh.Size = new System.Drawing.Size(71, 23);
            this.btnMudPropRefresh.TabIndex = 8;
            this.btnMudPropRefresh.Text = "Refresh";
            this.btnMudPropRefresh.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnMudPropRefresh.UseVisualStyleBackColor = true;
            this.btnMudPropRefresh.Click += new System.EventHandler(this.btnMudPropRefresh_Click);
            // 
            // rbtnPeriod18_24
            // 
            this.rbtnPeriod18_24.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbtnPeriod18_24.AutoSize = true;
            this.rbtnPeriod18_24.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.rbtnPeriod18_24.Location = new System.Drawing.Point(224, 7);
            this.rbtnPeriod18_24.Name = "rbtnPeriod18_24";
            this.rbtnPeriod18_24.Size = new System.Drawing.Size(44, 23);
            this.rbtnPeriod18_24.TabIndex = 3;
            this.rbtnPeriod18_24.Text = "18-24";
            this.rbtnPeriod18_24.UseVisualStyleBackColor = true;
            this.rbtnPeriod18_24.CheckedChanged += new System.EventHandler(this.rbtnPeriod_CheckedChanged);
            // 
            // rbtnPeriod12_18
            // 
            this.rbtnPeriod12_18.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbtnPeriod12_18.AutoSize = true;
            this.rbtnPeriod12_18.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.rbtnPeriod12_18.Location = new System.Drawing.Point(166, 7);
            this.rbtnPeriod12_18.Name = "rbtnPeriod12_18";
            this.rbtnPeriod12_18.Size = new System.Drawing.Size(44, 23);
            this.rbtnPeriod12_18.TabIndex = 2;
            this.rbtnPeriod12_18.Text = "12-18";
            this.rbtnPeriod12_18.UseVisualStyleBackColor = true;
            this.rbtnPeriod12_18.CheckedChanged += new System.EventHandler(this.rbtnPeriod_CheckedChanged);
            // 
            // rbtnPeriod06_12
            // 
            this.rbtnPeriod06_12.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbtnPeriod06_12.AutoSize = true;
            this.rbtnPeriod06_12.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.rbtnPeriod06_12.Location = new System.Drawing.Point(108, 7);
            this.rbtnPeriod06_12.Name = "rbtnPeriod06_12";
            this.rbtnPeriod06_12.Size = new System.Drawing.Size(44, 23);
            this.rbtnPeriod06_12.TabIndex = 1;
            this.rbtnPeriod06_12.Text = "06-12";
            this.rbtnPeriod06_12.UseVisualStyleBackColor = true;
            this.rbtnPeriod06_12.CheckedChanged += new System.EventHandler(this.rbtnPeriod_CheckedChanged);
            // 
            // rbtnPeriod00_06
            // 
            this.rbtnPeriod00_06.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbtnPeriod00_06.AutoSize = true;
            this.rbtnPeriod00_06.Checked = true;
            this.rbtnPeriod00_06.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.rbtnPeriod00_06.Location = new System.Drawing.Point(50, 7);
            this.rbtnPeriod00_06.Name = "rbtnPeriod00_06";
            this.rbtnPeriod00_06.Size = new System.Drawing.Size(44, 23);
            this.rbtnPeriod00_06.TabIndex = 0;
            this.rbtnPeriod00_06.TabStop = true;
            this.rbtnPeriod00_06.Text = "00-06";
            this.rbtnPeriod00_06.UseVisualStyleBackColor = true;
            this.rbtnPeriod00_06.CheckedChanged += new System.EventHandler(this.rbtnPeriod_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 164;
            this.label1.Text = "Times:";
            // 
            // btnMudPropApply
            // 
            this.btnMudPropApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnMudPropApply.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnMudPropApply.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMudPropApply.ImageKey = "Check";
            this.btnMudPropApply.ImageList = this.imageList;
            this.btnMudPropApply.Location = new System.Drawing.Point(66, 318);
            this.btnMudPropApply.Name = "btnMudPropApply";
            this.btnMudPropApply.Size = new System.Drawing.Size(104, 23);
            this.btnMudPropApply.TabIndex = 6;
            this.btnMudPropApply.Text = "Apply Changes";
            this.btnMudPropApply.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnMudPropApply.UseVisualStyleBackColor = true;
            this.btnMudPropApply.Click += new System.EventHandler(this.btnMudPropApply_Click);
            // 
            // dgvMudProp
            // 
            this.dgvMudProp.AllowUserToAddRows = false;
            this.dgvMudProp.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.dgvMudProp.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvMudProp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvMudProp.BackgroundColor = System.Drawing.Color.White;
            this.dgvMudProp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMudProp.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.mudPlaceHolder,
            this.Column1});
            this.dgvMudProp.EnableHeadersVisualStyles = false;
            this.dgvMudProp.Location = new System.Drawing.Point(6, 36);
            this.dgvMudProp.MultiSelect = false;
            this.dgvMudProp.Name = "dgvMudProp";
            this.dgvMudProp.RowHeadersWidth = 160;
            this.dgvMudProp.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.ColumnHeaderSelect;
            this.dgvMudProp.Size = new System.Drawing.Size(645, 276);
            this.dgvMudProp.TabIndex = 4;
            this.dgvMudProp.Tag = "spread sheet\'s rows = all properties from project ";
            this.dgvMudProp.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvMudProp_CellMouseClick);
            this.dgvMudProp.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMudProp_CellValueChanged);
            // 
            // mudPlaceHolder
            // 
            this.mudPlaceHolder.HeaderText = "";
            this.mudPlaceHolder.Name = "mudPlaceHolder";
            this.mudPlaceHolder.ReadOnly = true;
            this.mudPlaceHolder.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.mudPlaceHolder.Visible = false;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Unit";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.Column1.Width = 70;
            // 
            // tpWater
            // 
            this.tpWater.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tpWater.Controls.Add(this.btnWaterRemove);
            this.tpWater.Controls.Add(this.btnWaterRefresh);
            this.tpWater.Controls.Add(this.btnWaterAdd);
            this.tpWater.Controls.Add(this.btnWaterApply);
            this.tpWater.Controls.Add(this.dgvWater);
            this.tpWater.Location = new System.Drawing.Point(4, 25);
            this.tpWater.Name = "tpWater";
            this.tpWater.Padding = new System.Windows.Forms.Padding(3);
            this.tpWater.Size = new System.Drawing.Size(659, 349);
            this.tpWater.TabIndex = 1;
            this.tpWater.Text = "Water";
            this.tpWater.UseVisualStyleBackColor = true;
            // 
            // btnWaterRemove
            // 
            this.btnWaterRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnWaterRemove.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnWaterRemove.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnWaterRemove.ImageKey = "Remove";
            this.btnWaterRemove.ImageList = this.imageList;
            this.btnWaterRemove.Location = new System.Drawing.Point(176, 318);
            this.btnWaterRemove.Name = "btnWaterRemove";
            this.btnWaterRemove.Size = new System.Drawing.Size(71, 23);
            this.btnWaterRemove.TabIndex = 3;
            this.btnWaterRemove.Text = "Remove";
            this.btnWaterRemove.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnWaterRemove.UseVisualStyleBackColor = true;
            this.btnWaterRemove.Click += new System.EventHandler(this.btnWaterRemove_Click);
            // 
            // btnWaterRefresh
            // 
            this.btnWaterRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnWaterRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnWaterRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnWaterRefresh.ImageKey = "refresh";
            this.btnWaterRefresh.ImageList = this.imageList;
            this.btnWaterRefresh.Location = new System.Drawing.Point(253, 318);
            this.btnWaterRefresh.Name = "btnWaterRefresh";
            this.btnWaterRefresh.Size = new System.Drawing.Size(71, 23);
            this.btnWaterRefresh.TabIndex = 4;
            this.btnWaterRefresh.Text = "Refresh";
            this.btnWaterRefresh.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnWaterRefresh.UseVisualStyleBackColor = true;
            this.btnWaterRefresh.Click += new System.EventHandler(this.btnWaterRefresh_Click);
            // 
            // btnWaterAdd
            // 
            this.btnWaterAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnWaterAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnWaterAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnWaterAdd.ImageKey = "add2";
            this.btnWaterAdd.ImageList = this.imageList;
            this.btnWaterAdd.Location = new System.Drawing.Point(6, 318);
            this.btnWaterAdd.Name = "btnWaterAdd";
            this.btnWaterAdd.Size = new System.Drawing.Size(54, 23);
            this.btnWaterAdd.TabIndex = 1;
            this.btnWaterAdd.Text = "Add";
            this.btnWaterAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnWaterAdd.UseVisualStyleBackColor = true;
            this.btnWaterAdd.Click += new System.EventHandler(this.btnWaterAdd_Click);
            // 
            // btnWaterApply
            // 
            this.btnWaterApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnWaterApply.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnWaterApply.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnWaterApply.ImageKey = "Check";
            this.btnWaterApply.ImageList = this.imageList;
            this.btnWaterApply.Location = new System.Drawing.Point(66, 318);
            this.btnWaterApply.Name = "btnWaterApply";
            this.btnWaterApply.Size = new System.Drawing.Size(104, 23);
            this.btnWaterApply.TabIndex = 2;
            this.btnWaterApply.Text = "Apply Changes";
            this.btnWaterApply.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnWaterApply.UseVisualStyleBackColor = true;
            this.btnWaterApply.Click += new System.EventHandler(this.btnWaterApply_Click);
            // 
            // dgvWater
            // 
            this.dgvWater.AllowUserToAddRows = false;
            this.dgvWater.AllowUserToDeleteRows = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.dgvWater.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvWater.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvWater.BackgroundColor = System.Drawing.Color.White;
            this.dgvWater.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvWater.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.waterPlaceHolder});
            this.dgvWater.EnableHeadersVisualStyles = false;
            this.dgvWater.Location = new System.Drawing.Point(6, 6);
            this.dgvWater.MultiSelect = false;
            this.dgvWater.Name = "dgvWater";
            this.dgvWater.RowHeadersWidth = 100;
            this.dgvWater.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.ColumnHeaderSelect;
            this.dgvWater.Size = new System.Drawing.Size(645, 306);
            this.dgvWater.TabIndex = 0;
            this.dgvWater.Tag = "spread sheet - 5 rows ";
            this.dgvWater.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvWater_CellValueChanged);
            // 
            // waterPlaceHolder
            // 
            this.waterPlaceHolder.HeaderText = "";
            this.waterPlaceHolder.Name = "waterPlaceHolder";
            this.waterPlaceHolder.ReadOnly = true;
            this.waterPlaceHolder.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.waterPlaceHolder.Visible = false;
            // 
            // MudPropForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(691, 402);
            this.Controls.Add(this.tcMudProp);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimumSize = new System.Drawing.Size(691, 402);
            this.Name = "MudPropForm";
            this.Text = "MudPropForm";
            this.Load += new System.EventHandler(this.MudPropForm_Load);
            this.tcMudProp.ResumeLayout(false);
            this.tpMudProp.ResumeLayout(false);
            this.tpMudProp.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMudProp)).EndInit();
            this.tpWater.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvWater)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcMudProp;
        private System.Windows.Forms.TabPage tpMudProp;
        private System.Windows.Forms.Button btnMudPropApply;
        private System.Windows.Forms.TabPage tpWater;
        private System.Windows.Forms.Button btnWaterApply;
        private System.Windows.Forms.DataGridView dgvWater;
        private System.Windows.Forms.Button btnWaterAdd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rbtnPeriod18_24;
        private System.Windows.Forms.RadioButton rbtnPeriod12_18;
        private System.Windows.Forms.RadioButton rbtnPeriod06_12;
        private System.Windows.Forms.RadioButton rbtnPeriod00_06;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.Button btnMudPropRefresh;
        private System.Windows.Forms.Button btnWaterRefresh;
        private System.Windows.Forms.Button btnMudPropAdd;
        private System.Windows.Forms.Button btnMudPropRemove;
        private System.Windows.Forms.DataGridViewTextBoxColumn waterPlaceHolder;
        private System.Windows.Forms.Button btnWaterRemove;
        private System.Windows.Forms.DataGridViewTextBoxColumn mudPlaceHolder;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        public System.Windows.Forms.DataGridView dgvMudProp;

    }
}