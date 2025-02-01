namespace DMR.User_DB
{
    partial class DGV_Filter
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DGV_Filter));
            this.chbDoSearch = new System.Windows.Forms.CheckBox();
            this.imgList = new System.Windows.Forms.ImageList(this.components);
            this.cboxSearchBy = new System.Windows.Forms.ComboBox();
            this.txtSearchDesc = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.mainToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // chbDoSearch
            // 
            this.chbDoSearch.Appearance = System.Windows.Forms.Appearance.Button;
            this.chbDoSearch.FlatAppearance.BorderSize = 2;
            this.chbDoSearch.FlatAppearance.CheckedBackColor = System.Drawing.Color.Black;
            this.chbDoSearch.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.chbDoSearch.ImageKey = "noFilter";
            this.chbDoSearch.ImageList = this.imgList;
            this.chbDoSearch.Location = new System.Drawing.Point(458, 7);
            this.chbDoSearch.Name = "chbDoSearch";
            this.chbDoSearch.Size = new System.Drawing.Size(25, 21);
            this.chbDoSearch.TabIndex = 2;
            this.chbDoSearch.UseVisualStyleBackColor = true;
            this.chbDoSearch.CheckedChanged += new System.EventHandler(this.chbDoSearch_CheckedChanged);
            // 
            // imgList
            // 
            this.imgList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgList.ImageStream")));
            this.imgList.TransparentColor = System.Drawing.Color.Transparent;
            this.imgList.Images.SetKeyName(0, "noFilter");
            this.imgList.Images.SetKeyName(1, "applyFilter");
            // 
            // cboxSearchBy
            // 
            this.cboxSearchBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxSearchBy.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cboxSearchBy.FormattingEnabled = true;
            this.cboxSearchBy.Location = new System.Drawing.Point(70, 7);
            this.cboxSearchBy.Name = "cboxSearchBy";
            this.cboxSearchBy.Size = new System.Drawing.Size(143, 21);
            this.cboxSearchBy.TabIndex = 0;
            this.mainToolTip.SetToolTip(this.cboxSearchBy, "Search Filter");
            this.cboxSearchBy.SelectedIndexChanged += new System.EventHandler(this.cboxSearchBy_SelectedIndexChanged);
            // 
            // txtSearchDesc
            // 
            this.txtSearchDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearchDesc.Location = new System.Drawing.Point(312, 8);
            this.txtSearchDesc.Name = "txtSearchDesc";
            this.txtSearchDesc.Size = new System.Drawing.Size(140, 20);
            this.txtSearchDesc.TabIndex = 1;
            this.mainToolTip.SetToolTip(this.txtSearchDesc, "Text to Search");
            this.txtSearchDesc.TextChanged += new System.EventHandler(this.txtSearchDesc_TextChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(243, 10);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(63, 13);
            this.label11.TabIndex = 160;
            this.label11.Text = "Description:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 10);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(59, 13);
            this.label10.TabIndex = 161;
            this.label10.Text = "Search By:";
            // 
            // DGV_Filter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chbDoSearch);
            this.Controls.Add(this.cboxSearchBy);
            this.Controls.Add(this.txtSearchDesc);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Name = "DGV_Filter";
            this.Size = new System.Drawing.Size(491, 32);
            this.Load += new System.EventHandler(this.DGV_Filter_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chbDoSearch;
        private System.Windows.Forms.ImageList imgList;
        private System.Windows.Forms.ComboBox cboxSearchBy;
        private System.Windows.Forms.TextBox txtSearchDesc;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ToolTip mainToolTip;
    }
}
