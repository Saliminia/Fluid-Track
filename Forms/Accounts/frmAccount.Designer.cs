namespace DMR
{
    partial class frmAccount
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAccount));
            this.imgList = new System.Windows.Forms.ImageList(this.components);
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnLoadImage = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.pboxImage = new System.Windows.Forms.PictureBox();
            this.lblUserRole = new System.Windows.Forms.Label();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.txtPassNew = new System.Windows.Forms.TextBox();
            this.txtPassRep = new System.Windows.Forms.TextBox();
            this.lblPassRep = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPassCur = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblPassNew = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cboxRole = new System.Windows.Forms.ComboBox();
            this.btnClearImage = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pboxImage)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // imgList
            // 
            this.imgList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgList.ImageStream")));
            this.imgList.TransparentColor = System.Drawing.Color.Transparent;
            this.imgList.Images.SetKeyName(0, "forbidden.png");
            this.imgList.Images.SetKeyName(1, "refresh");
            this.imgList.Images.SetKeyName(2, "plus_light_alt.png");
            this.imgList.Images.SetKeyName(3, "point_hand.png");
            this.imgList.Images.SetKeyName(4, "noFilter");
            this.imgList.Images.SetKeyName(5, "applyFilter");
            this.imgList.Images.SetKeyName(6, "tick.png");
            this.imgList.Images.SetKeyName(7, "page_add.png");
            this.imgList.Images.SetKeyName(8, "arrow_down.png");
            this.imgList.Images.SetKeyName(9, "arrow_uo.png");
            this.imgList.Images.SetKeyName(10, "folder_clear.png");
            // 
            // btnOK
            // 
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK.ImageKey = "point_hand.png";
            this.btnOK.ImageList = this.imgList;
            this.btnOK.Location = new System.Drawing.Point(453, 167);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(69, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.ImageKey = "forbidden.png";
            this.btnCancel.ImageList = this.imgList;
            this.btnCancel.Location = new System.Drawing.Point(12, 167);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(69, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // txtID
            // 
            this.txtID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtID.Location = new System.Drawing.Point(73, 19);
            this.txtID.MaxLength = 50;
            this.txtID.Name = "txtID";
            this.txtID.ReadOnly = true;
            this.txtID.Size = new System.Drawing.Size(114, 20);
            this.txtID.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "First Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Last Name:";
            // 
            // btnLoadImage
            // 
            this.btnLoadImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLoadImage.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnLoadImage.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLoadImage.ImageKey = "folder_clear.png";
            this.btnLoadImage.ImageList = this.imgList;
            this.btnLoadImage.Location = new System.Drawing.Point(281, 123);
            this.btnLoadImage.Name = "btnLoadImage";
            this.btnLoadImage.Size = new System.Drawing.Size(35, 20);
            this.btnLoadImage.TabIndex = 2;
            this.btnLoadImage.UseVisualStyleBackColor = true;
            this.btnLoadImage.Click += new System.EventHandler(this.btnLoadImage_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(21, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "ID:";
            // 
            // pboxImage
            // 
            this.pboxImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pboxImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pboxImage.Location = new System.Drawing.Point(207, 21);
            this.pboxImage.Name = "pboxImage";
            this.pboxImage.Size = new System.Drawing.Size(109, 122);
            this.pboxImage.TabIndex = 2;
            this.pboxImage.TabStop = false;
            // 
            // lblUserRole
            // 
            this.lblUserRole.AutoSize = true;
            this.lblUserRole.Location = new System.Drawing.Point(7, 120);
            this.lblUserRole.Name = "lblUserRole";
            this.lblUserRole.Size = new System.Drawing.Size(32, 13);
            this.lblUserRole.TabIndex = 0;
            this.lblUserRole.Text = "Role:";
            // 
            // txtFirstName
            // 
            this.txtFirstName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFirstName.Location = new System.Drawing.Point(73, 45);
            this.txtFirstName.MaxLength = 30;
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(114, 20);
            this.txtFirstName.TabIndex = 0;
            // 
            // txtLastName
            // 
            this.txtLastName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLastName.Location = new System.Drawing.Point(73, 71);
            this.txtLastName.MaxLength = 30;
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(114, 20);
            this.txtLastName.TabIndex = 1;
            // 
            // txtPassNew
            // 
            this.txtPassNew.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPassNew.Location = new System.Drawing.Point(58, 92);
            this.txtPassNew.MaxLength = 30;
            this.txtPassNew.Name = "txtPassNew";
            this.txtPassNew.PasswordChar = '•';
            this.txtPassNew.Size = new System.Drawing.Size(114, 20);
            this.txtPassNew.TabIndex = 1;
            // 
            // txtPassRep
            // 
            this.txtPassRep.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPassRep.Location = new System.Drawing.Point(58, 118);
            this.txtPassRep.MaxLength = 30;
            this.txtPassRep.Name = "txtPassRep";
            this.txtPassRep.PasswordChar = '•';
            this.txtPassRep.Size = new System.Drawing.Size(114, 20);
            this.txtPassRep.TabIndex = 2;
            // 
            // lblPassRep
            // 
            this.lblPassRep.AutoSize = true;
            this.lblPassRep.Location = new System.Drawing.Point(7, 120);
            this.lblPassRep.Name = "lblPassRep";
            this.lblPassRep.Size = new System.Drawing.Size(45, 13);
            this.lblPassRep.TabIndex = 0;
            this.lblPassRep.Text = "Repeat:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Current:";
            // 
            // txtPassCur
            // 
            this.txtPassCur.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPassCur.Location = new System.Drawing.Point(58, 19);
            this.txtPassCur.MaxLength = 30;
            this.txtPassCur.Name = "txtPassCur";
            this.txtPassCur.PasswordChar = '•';
            this.txtPassCur.Size = new System.Drawing.Size(114, 20);
            this.txtPassCur.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtPassCur);
            this.groupBox1.Controls.Add(this.lblPassNew);
            this.groupBox1.Controls.Add(this.txtPassNew);
            this.groupBox1.Controls.Add(this.txtPassRep);
            this.groupBox1.Controls.Add(this.lblPassRep);
            this.groupBox1.Location = new System.Drawing.Point(340, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(182, 149);
            this.groupBox1.TabIndex = 268;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Password";
            // 
            // lblPassNew
            // 
            this.lblPassNew.AutoSize = true;
            this.lblPassNew.Location = new System.Drawing.Point(7, 94);
            this.lblPassNew.Name = "lblPassNew";
            this.lblPassNew.Size = new System.Drawing.Size(32, 13);
            this.lblPassNew.TabIndex = 0;
            this.lblPassNew.Text = "New:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cboxRole);
            this.groupBox2.Controls.Add(this.txtID);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.btnClearImage);
            this.groupBox2.Controls.Add(this.btnLoadImage);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.pboxImage);
            this.groupBox2.Controls.Add(this.lblUserRole);
            this.groupBox2.Controls.Add(this.txtFirstName);
            this.groupBox2.Controls.Add(this.txtLastName);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(322, 149);
            this.groupBox2.TabIndex = 269;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "General";
            // 
            // cboxRole
            // 
            this.cboxRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxRole.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cboxRole.FormattingEnabled = true;
            this.cboxRole.Items.AddRange(new object[] {
            "Planning",
            "Engineering Supervisor",
            "Technical Engineer",
            "Operation Manager",
            "Project Engineer",
            "Site/Mud Engineer"});
            this.cboxRole.Location = new System.Drawing.Point(73, 117);
            this.cboxRole.Name = "cboxRole";
            this.cboxRole.Size = new System.Drawing.Size(114, 21);
            this.cboxRole.TabIndex = 5;
            // 
            // btnClearImage
            // 
            this.btnClearImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClearImage.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClearImage.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClearImage.ImageKey = "forbidden.png";
            this.btnClearImage.ImageList = this.imgList;
            this.btnClearImage.Location = new System.Drawing.Point(207, 123);
            this.btnClearImage.Name = "btnClearImage";
            this.btnClearImage.Size = new System.Drawing.Size(35, 20);
            this.btnClearImage.TabIndex = 3;
            this.btnClearImage.UseVisualStyleBackColor = true;
            this.btnClearImage.Click += new System.EventHandler(this.btnClearImage_Click);
            // 
            // frmAccount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(537, 202);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmAccount";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Account";
            this.Load += new System.EventHandler(this.frmAccount_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pboxImage)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imgList;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        public System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.Button btnLoadImage;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.PictureBox pboxImage;
        private System.Windows.Forms.Label lblUserRole;
        public System.Windows.Forms.TextBox txtFirstName;
        public System.Windows.Forms.TextBox txtLastName;
        public System.Windows.Forms.TextBox txtPassNew;
        public System.Windows.Forms.TextBox txtPassRep;
        private System.Windows.Forms.Label lblPassRep;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox txtPassCur;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblPassNew;
        private System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.Button btnClearImage;
        private System.Windows.Forms.ComboBox cboxRole;
    }
}