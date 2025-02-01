namespace DMR
{
    partial class MultiItem_Info_Form
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MultiItem_Info_Form));
            this.btnOK = new System.Windows.Forms.Button();
            this.dgvInfo = new System.Windows.Forms.DataGridView();
            this.colItemNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colException = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLocation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chbErrText = new System.Windows.Forms.CheckBox();
            this.chbException = new System.Windows.Forms.CheckBox();
            this.chbLocation = new System.Windows.Forms.CheckBox();
            this.chbItem = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnOK.Location = new System.Drawing.Point(431, 177);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // dgvInfo
            // 
            this.dgvInfo.AllowUserToAddRows = false;
            this.dgvInfo.AllowUserToDeleteRows = false;
            this.dgvInfo.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvInfo.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvInfo.BackgroundColor = System.Drawing.Color.White;
            this.dgvInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colItemNumber,
            this.colDescription,
            this.colException,
            this.colLocation});
            this.dgvInfo.Location = new System.Drawing.Point(15, 12);
            this.dgvInfo.Name = "dgvInfo";
            this.dgvInfo.ReadOnly = true;
            this.dgvInfo.RowHeadersVisible = false;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvInfo.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvInfo.Size = new System.Drawing.Size(491, 159);
            this.dgvInfo.TabIndex = 10;
            // 
            // colItemNumber
            // 
            this.colItemNumber.HeaderText = "#";
            this.colItemNumber.Name = "colItemNumber";
            this.colItemNumber.ReadOnly = true;
            this.colItemNumber.Width = 35;
            // 
            // colDescription
            // 
            this.colDescription.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colDescription.HeaderText = "Description";
            this.colDescription.MinimumWidth = 150;
            this.colDescription.Name = "colDescription";
            this.colDescription.ReadOnly = true;
            // 
            // colException
            // 
            this.colException.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colException.HeaderText = "Exception";
            this.colException.MinimumWidth = 150;
            this.colException.Name = "colException";
            this.colException.ReadOnly = true;
            // 
            // colLocation
            // 
            this.colLocation.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colLocation.HeaderText = "Location";
            this.colLocation.MinimumWidth = 150;
            this.colLocation.Name = "colLocation";
            this.colLocation.ReadOnly = true;
            // 
            // chbErrText
            // 
            this.chbErrText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chbErrText.AutoSize = true;
            this.chbErrText.Checked = true;
            this.chbErrText.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbErrText.Location = new System.Drawing.Point(67, 177);
            this.chbErrText.Name = "chbErrText";
            this.chbErrText.Size = new System.Drawing.Size(79, 17);
            this.chbErrText.TabIndex = 11;
            this.chbErrText.Text = "Description";
            this.chbErrText.UseVisualStyleBackColor = true;
            this.chbErrText.CheckedChanged += new System.EventHandler(this.chbErrorParts_CheckedChanged);
            // 
            // chbException
            // 
            this.chbException.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chbException.AutoSize = true;
            this.chbException.Checked = true;
            this.chbException.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbException.Location = new System.Drawing.Point(152, 177);
            this.chbException.Name = "chbException";
            this.chbException.Size = new System.Drawing.Size(73, 17);
            this.chbException.TabIndex = 11;
            this.chbException.Text = "Exception";
            this.chbException.UseVisualStyleBackColor = true;
            this.chbException.CheckedChanged += new System.EventHandler(this.chbErrorParts_CheckedChanged);
            // 
            // chbLocation
            // 
            this.chbLocation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chbLocation.AutoSize = true;
            this.chbLocation.Checked = true;
            this.chbLocation.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbLocation.Location = new System.Drawing.Point(231, 177);
            this.chbLocation.Name = "chbLocation";
            this.chbLocation.Size = new System.Drawing.Size(67, 17);
            this.chbLocation.TabIndex = 11;
            this.chbLocation.Text = "Location";
            this.chbLocation.UseVisualStyleBackColor = true;
            this.chbLocation.CheckedChanged += new System.EventHandler(this.chbErrorParts_CheckedChanged);
            // 
            // chbItem
            // 
            this.chbItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chbItem.AutoSize = true;
            this.chbItem.Checked = true;
            this.chbItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbItem.Location = new System.Drawing.Point(15, 177);
            this.chbItem.Name = "chbItem";
            this.chbItem.Size = new System.Drawing.Size(46, 17);
            this.chbItem.TabIndex = 11;
            this.chbItem.Text = "Item";
            this.chbItem.UseVisualStyleBackColor = true;
            this.chbItem.CheckedChanged += new System.EventHandler(this.chbErrorParts_CheckedChanged);
            // 
            // MultiItem_Info_Form
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(518, 202);
            this.ControlBox = false;
            this.Controls.Add(this.chbLocation);
            this.Controls.Add(this.chbException);
            this.Controls.Add(this.chbItem);
            this.Controls.Add(this.chbErrText);
            this.Controls.Add(this.dgvInfo);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MultiItem_Info_Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Title";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MultiItem_Info_Form_FormClosing);
            this.Load += new System.EventHandler(this.MultiItem_Info_Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInfo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.DataGridView dgvInfo;
        private System.Windows.Forms.CheckBox chbErrText;
        private System.Windows.Forms.CheckBox chbException;
        private System.Windows.Forms.CheckBox chbLocation;
        private System.Windows.Forms.CheckBox chbItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn colItemNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn colException;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLocation;
    }
}