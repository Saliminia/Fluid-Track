namespace DMR
{
    partial class MudVolManSelToRecForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MudVolManSelToRecForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnOK = new System.Windows.Forms.Button();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.dgvTransRecord = new System.Windows.Forms.DataGridView();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnCancel = new System.Windows.Forms.Button();
            this.dgV_Filter1 = new DMR.User_DB.DGV_Filter();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTransRecord)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK.ImageKey = "point_hand.png";
            this.btnOK.ImageList = this.imageList;
            this.btnOK.Location = new System.Drawing.Point(440, 277);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(69, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOK.UseVisualStyleBackColor = true;
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
            this.imageList.Images.SetKeyName(6, "Remove");
            this.imageList.Images.SetKeyName(7, "Edit");
            this.imageList.Images.SetKeyName(8, "Check");
            this.imageList.Images.SetKeyName(9, "Right");
            this.imageList.Images.SetKeyName(10, "Up");
            this.imageList.Images.SetKeyName(11, "Down");
            this.imageList.Images.SetKeyName(12, "List");
            this.imageList.Images.SetKeyName(13, "warning");
            this.imageList.Images.SetKeyName(14, "refresh");
            this.imageList.Images.SetKeyName(15, "forbidden.png");
            this.imageList.Images.SetKeyName(16, "point_hand.png");
            this.imageList.Images.SetKeyName(17, "noFilter");
            this.imageList.Images.SetKeyName(18, "applyFilter");
            this.imageList.Images.SetKeyName(19, "page_add.png");
            this.imageList.Images.SetKeyName(20, "bookmark.png");
            this.imageList.Images.SetKeyName(21, "flask.png");
            this.imageList.Images.SetKeyName(22, "eject.png");
            this.imageList.Images.SetKeyName(23, "user_clear.png");
            this.imageList.Images.SetKeyName(24, "open");
            // 
            // dgvTransRecord
            // 
            this.dgvTransRecord.AllowUserToAddRows = false;
            this.dgvTransRecord.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.dgvTransRecord.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvTransRecord.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTransRecord.BackgroundColor = System.Drawing.Color.White;
            this.dgvTransRecord.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTransRecord.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column5,
            this.Column1,
            this.Column2});
            this.dgvTransRecord.EnableHeadersVisualStyles = false;
            this.dgvTransRecord.Location = new System.Drawing.Point(12, 50);
            this.dgvTransRecord.MultiSelect = false;
            this.dgvTransRecord.Name = "dgvTransRecord";
            this.dgvTransRecord.ReadOnly = true;
            this.dgvTransRecord.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTransRecord.Size = new System.Drawing.Size(497, 221);
            this.dgvTransRecord.TabIndex = 1;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "transRecordId";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.Column5.Visible = false;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.HeaderText = "Report";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column2.HeaderText = "Transfer";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.ImageKey = "forbidden.png";
            this.btnCancel.ImageList = this.imageList;
            this.btnCancel.Location = new System.Drawing.Point(12, 277);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(69, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // dgV_Filter1
            // 
            this.dgV_Filter1.DGV_ToFilter = this.dgvTransRecord;
            this.dgV_Filter1.Labels_ToFilter = new string[] {
        "All",
        "Report",
        "Transfer"};
            this.dgV_Filter1.Location = new System.Drawing.Point(12, 1);
            this.dgV_Filter1.Name = "dgV_Filter1";
            this.dgV_Filter1.Size = new System.Drawing.Size(506, 32);
            this.dgV_Filter1.StartColumn_ToFilter = 1;
            this.dgV_Filter1.TabIndex = 0;
            // 
            // MudVolManSelToRecForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(521, 312);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.dgvTransRecord);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.dgV_Filter1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MudVolManSelToRecForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Receive";
            this.Load += new System.EventHandler(this.MudVolManSelToRecForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTransRecord)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.ImageList imageList;
        public System.Windows.Forms.DataGridView dgvTransRecord;
        private System.Windows.Forms.Button btnCancel;
        public User_DB.DGV_Filter dgV_Filter1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
    }
}