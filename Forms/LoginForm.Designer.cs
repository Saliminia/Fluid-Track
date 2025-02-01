namespace DMR
{
    partial class LoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.btnLogin = new System.Windows.Forms.Button();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.txtPasswrd = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnDataImport = new System.Windows.Forms.Button();
#if !NOLOCK
            this.axARMClass2 = new AxARM.AxARMClass();
#endif
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
#if !NOLOCK
            ((System.ComponentModel.ISupportInitialize)(this.axARMClass2)).BeginInit();
#endif
            this.SuspendLayout();
            // 
            // btnLogin
            // 
            this.btnLogin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLogin.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnLogin.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLogin.ImageKey = "point_hand.png";
            this.btnLogin.ImageList = this.imageList;
            this.btnLogin.Location = new System.Drawing.Point(210, 15);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(88, 23);
            this.btnLogin.TabIndex = 2;
            this.btnLogin.Text = "Log In";
            this.btnLogin.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
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
            // txtPasswrd
            // 
            this.txtPasswrd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPasswrd.Location = new System.Drawing.Point(51, 45);
            this.txtPasswrd.MaxLength = 50;
            this.txtPasswrd.Name = "txtPasswrd";
            this.txtPasswrd.PasswordChar = '*';
            this.txtPasswrd.Size = new System.Drawing.Size(153, 20);
            this.txtPasswrd.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(9, 47);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 13);
            this.label5.TabIndex = 70;
            this.label5.Text = "Pass.:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Location = new System.Drawing.Point(9, 18);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(21, 13);
            this.label12.TabIndex = 71;
            this.label12.Text = "ID:";
            // 
            // txtUserName
            // 
            this.txtUserName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUserName.Location = new System.Drawing.Point(51, 16);
            this.txtUserName.MaxLength = 50;
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(153, 20);
            this.txtUserName.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Enabled = false;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.ImageKey = "forbidden.png";
            this.btnCancel.ImageList = this.imageList;
            this.btnCancel.Location = new System.Drawing.Point(94, 71);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(88, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Visible = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.BackgroundImage = global::DMR.Images.PDFExcellLogo;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(318, 151);
            this.pictureBox1.TabIndex = 72;
            this.pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnLogin);
            this.panel1.Controls.Add(this.btnDataImport);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.txtPasswrd);
            this.panel1.Controls.Add(this.txtUserName);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 151);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(318, 89);
            this.panel1.TabIndex = 0;
            // 
            // btnDataImport
            // 
            this.btnDataImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDataImport.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDataImport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDataImport.ImageKey = "add2";
            this.btnDataImport.ImageList = this.imageList;
            this.btnDataImport.Location = new System.Drawing.Point(210, 44);
            this.btnDataImport.Name = "btnDataImport";
            this.btnDataImport.Size = new System.Drawing.Size(88, 23);
            this.btnDataImport.TabIndex = 4;
            this.btnDataImport.Text = "Data Import";
            this.btnDataImport.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDataImport.UseVisualStyleBackColor = true;
            this.btnDataImport.Click += new System.EventHandler(this.btnDataTransfer_Click);
#if !NOLOCK
            // 
            // axARMClass2
            // 
            this.axARMClass2.Location = new System.Drawing.Point(156, 47);
            this.axARMClass2.Name = "axARMClass2";
            this.axARMClass2.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axARMClass2.OcxState")));
            this.axARMClass2.Size = new System.Drawing.Size(75, 23);
            this.axARMClass2.TabIndex = 72;
            this.axARMClass2.Visible = false;
#endif
            // 
            // LoginForm
            // 
            this.AcceptButton = this.btnLogin;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(318, 240);
#if !NOLOCK
            this.Controls.Add(this.axARMClass2);
#endif
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
#if !NOLOCK
            ((System.ComponentModel.ISupportInitialize)(this.axARMClass2)).EndInit();
#endif
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.TextBox txtPasswrd;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.Button btnDataImport;
#if !NOLOCK
        private AxARM.AxARMClass axARMClass1;
        private AxARM.AxARMClass axARMClass2;
#endif
    }
}