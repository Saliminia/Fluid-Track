namespace DMR.Forms
{
    partial class SoftwareActivation
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
#if !NOLOCK
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SoftwareActivation));
            this.rtextAgreement = new System.Windows.Forms.RichTextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.btnLicGenerate = new System.Windows.Forms.Button();
            this.rtextRequestString = new System.Windows.Forms.RichTextBox();
            this.btnSaveToFile = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.btnImportActivationFile = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rtextAgreement
            // 
            this.rtextAgreement.AcceptsTab = true;
            this.rtextAgreement.AutoWordSelection = true;
            this.rtextAgreement.EnableAutoDragDrop = true;
            this.rtextAgreement.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtextAgreement.HideSelection = false;
            this.rtextAgreement.Location = new System.Drawing.Point(12, 12);
            this.rtextAgreement.Name = "rtextAgreement";
            this.rtextAgreement.ReadOnly = true;
            this.rtextAgreement.Size = new System.Drawing.Size(689, 227);
            this.rtextAgreement.TabIndex = 0;
            this.rtextAgreement.Text = resources.GetString("rtextAgreement.Text");
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(12, 245);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(205, 17);
            this.checkBox1.TabIndex = 1;
            this.checkBox1.Text = "I have read and agree to this License.";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // btnLicGenerate
            // 
            this.btnLicGenerate.Enabled = false;
            this.btnLicGenerate.Location = new System.Drawing.Point(13, 278);
            this.btnLicGenerate.Name = "btnLicGenerate";
            this.btnLicGenerate.Size = new System.Drawing.Size(108, 23);
            this.btnLicGenerate.TabIndex = 2;
            this.btnLicGenerate.Text = "License Request";
            this.btnLicGenerate.UseVisualStyleBackColor = true;
            this.btnLicGenerate.Click += new System.EventHandler(this.btnLicGenerate_Click);
            // 
            // rtextRequestString
            // 
            this.rtextRequestString.Location = new System.Drawing.Point(13, 308);
            this.rtextRequestString.Name = "rtextRequestString";
            this.rtextRequestString.ReadOnly = true;
            this.rtextRequestString.Size = new System.Drawing.Size(688, 96);
            this.rtextRequestString.TabIndex = 3;
            this.rtextRequestString.Text = "";
            // 
            // btnSaveToFile
            // 
            this.btnSaveToFile.Enabled = false;
            this.btnSaveToFile.Location = new System.Drawing.Point(608, 279);
            this.btnSaveToFile.Name = "btnSaveToFile";
            this.btnSaveToFile.Size = new System.Drawing.Size(93, 23);
            this.btnSaveToFile.TabIndex = 4;
            this.btnSaveToFile.Text = "Save To File";
            this.btnSaveToFile.UseVisualStyleBackColor = true;
            this.btnSaveToFile.Click += new System.EventHandler(this.btnSaveToFile_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(391, 424);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(127, 279);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(475, 23);
            this.progressBar1.TabIndex = 8;
            this.progressBar1.Value = 1;
            this.progressBar1.Visible = false;
            // 
            // btnImportActivationFile
            // 
            this.btnImportActivationFile.Location = new System.Drawing.Point(263, 424);
            this.btnImportActivationFile.Name = "btnImportActivationFile";
            this.btnImportActivationFile.Size = new System.Drawing.Size(122, 23);
            this.btnImportActivationFile.TabIndex = 5;
            this.btnImportActivationFile.Text = "Import Activation File";
            this.btnImportActivationFile.UseVisualStyleBackColor = true;
            this.btnImportActivationFile.Click += new System.EventHandler(this.btnImportActivationFile_Click);
            // 
            // SoftwareActivation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(713, 465);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnImportActivationFile);
            this.Controls.Add(this.btnSaveToFile);
            this.Controls.Add(this.rtextRequestString);
            this.Controls.Add(this.btnLicGenerate);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.rtextAgreement);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SoftwareActivation";
            this.ShowInTaskbar = false;
            this.Text = "Software Activation";
            this.ResumeLayout(false);
            this.PerformLayout();
#endif
        }

        #endregion


#if !NOLOCK
        private System.Windows.Forms.RichTextBox rtextAgreement;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button btnLicGenerate;
        private System.Windows.Forms.RichTextBox rtextRequestString;
        private System.Windows.Forms.Button btnSaveToFile;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button btnImportActivationFile;
#endif
    }
}