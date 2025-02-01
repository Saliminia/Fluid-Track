namespace DMR
{
    partial class UserProjectSelection
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserProjectSelection));
            this.dgvProjects = new System.Windows.Forms.DataGridView();
            this.selExists = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.casingID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.selButtons = new DMR.Helpers.DGV.ListFormSelectionButtons();
            this.dgv_Filter = new DMR.User_DB.DGV_Filter();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProjects)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvProjects
            // 
            this.dgvProjects.AllowUserToAddRows = false;
            this.dgvProjects.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Wheat;
            this.dgvProjects.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvProjects.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvProjects.BackgroundColor = System.Drawing.Color.White;
            this.dgvProjects.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProjects.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.selExists,
            this.casingID,
            this.Column1,
            this.Column5});
            this.dgvProjects.EnableHeadersVisualStyles = false;
            this.dgvProjects.Location = new System.Drawing.Point(13, 46);
            this.dgvProjects.MultiSelect = false;
            this.dgvProjects.Name = "dgvProjects";
            this.dgvProjects.RowHeadersVisible = false;
            this.dgvProjects.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProjects.Size = new System.Drawing.Size(494, 270);
            this.dgvProjects.TabIndex = 1;
            // 
            // selExists
            // 
            this.selExists.HeaderText = "";
            this.selExists.Name = "selExists";
            this.selExists.Width = 50;
            // 
            // casingID
            // 
            this.casingID.HeaderText = "";
            this.casingID.Name = "casingID";
            this.casingID.ReadOnly = true;
            this.casingID.Visible = false;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.HeaderText = "Name";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column5.HeaderText = "Type";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // selButtons
            // 
            this.selButtons.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.selButtons.DGV_ToSelect = this.dgvProjects;
            this.selButtons.Location = new System.Drawing.Point(12, 322);
            this.selButtons.Name = "selButtons";
            this.selButtons.ShowOKCancelButtons = true;
            this.selButtons.ShowSelectionButtons = true;
            this.selButtons.Size = new System.Drawing.Size(494, 29);
            this.selButtons.TabIndex = 2;
            // 
            // dgv_Filter
            // 
            this.dgv_Filter.DGV_ToFilter = this.dgvProjects;
            this.dgv_Filter.Labels_ToFilter = new string[] {
        "All",
        "Name",
        "Type"};
            this.dgv_Filter.Location = new System.Drawing.Point(13, 12);
            this.dgv_Filter.Name = "dgv_Filter";
            this.dgv_Filter.Size = new System.Drawing.Size(494, 28);
            this.dgv_Filter.StartColumn_ToFilter = 2;
            this.dgv_Filter.TabIndex = 0;
            // 
            // UserProjectSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(519, 356);
            this.Controls.Add(this.selButtons);
            this.Controls.Add(this.dgv_Filter);
            this.Controls.Add(this.dgvProjects);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(535, 394);
            this.Name = "UserProjectSelection";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Project Selection";
            this.Load += new System.EventHandler(this.UserProjectSelection_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProjects)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Helpers.DGV.ListFormSelectionButtons selButtons;
        private User_DB.DGV_Filter dgv_Filter;
        public System.Windows.Forms.DataGridView dgvProjects;
        private System.Windows.Forms.DataGridViewCheckBoxColumn sel;
        private System.Windows.Forms.DataGridViewTextBoxColumn casingID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewCheckBoxColumn selExists;
    }
}