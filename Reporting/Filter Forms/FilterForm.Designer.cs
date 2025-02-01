namespace DMR.Reporting.Filter
{
    partial class FilterForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FilterForm));
            this.btnNext = new System.Windows.Forms.Button();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.radioButton5 = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.radioButton6 = new System.Windows.Forms.RadioButton();
            this.radioButton7 = new System.Windows.Forms.RadioButton();
            this.radioButton8 = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.radioButton9 = new System.Windows.Forms.RadioButton();
            this.radioButton10 = new System.Windows.Forms.RadioButton();
            this.radioButton11 = new System.Windows.Forms.RadioButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.radioButton12 = new System.Windows.Forms.RadioButton();
            this.radioButton13 = new System.Windows.Forms.RadioButton();
            this.radioButton14 = new System.Windows.Forms.RadioButton();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnNext
            // 
            this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnNext.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNext.ImageKey = "Right";
            this.btnNext.ImageList = this.imageList;
            this.btnNext.Location = new System.Drawing.Point(308, 198);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(56, 23);
            this.btnNext.TabIndex = 104;
            this.btnNext.Text = "Next";
            this.btnNext.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNext.UseVisualStyleBackColor = true;
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
            this.imageList.Images.SetKeyName(13, "left");
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
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.radioButton1.Location = new System.Drawing.Point(12, 12);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(62, 17);
            this.radioButton1.TabIndex = 106;
            this.radioButton1.Text = "By Date";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.radioButton2.Location = new System.Drawing.Point(12, 38);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(78, 17);
            this.radioButton2.TabIndex = 106;
            this.radioButton2.Text = "By Report#";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.radioButton3.Location = new System.Drawing.Point(12, 65);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(106, 17);
            this.radioButton3.TabIndex = 106;
            this.radioButton3.Text = "By Drilled Interval";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.radioButton4.Location = new System.Drawing.Point(12, 95);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(85, 17);
            this.radioButton4.TabIndex = 106;
            this.radioButton4.Text = "By Formation";
            this.radioButton4.UseVisualStyleBackColor = true;
            // 
            // radioButton5
            // 
            this.radioButton5.AutoSize = true;
            this.radioButton5.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.radioButton5.Location = new System.Drawing.Point(12, 125);
            this.radioButton5.Name = "radioButton5";
            this.radioButton5.Size = new System.Drawing.Size(100, 17);
            this.radioButton5.TabIndex = 106;
            this.radioButton5.Text = "By Hole Section";
            this.radioButton5.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.radioButton6);
            this.panel1.Controls.Add(this.radioButton7);
            this.panel1.Controls.Add(this.radioButton8);
            this.panel1.Location = new System.Drawing.Point(124, 62);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(239, 24);
            this.panel1.TabIndex = 107;
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.ImageKey = "point_hand.png";
            this.button1.ImageList = this.imageList;
            this.button1.Location = new System.Drawing.Point(336, 158);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(27, 23);
            this.button1.TabIndex = 104;
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Location = new System.Drawing.Point(12, 158);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(318, 23);
            this.label1.TabIndex = 108;
            this.label1.Text = "Selected Well:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // radioButton6
            // 
            this.radioButton6.AutoSize = true;
            this.radioButton6.Checked = true;
            this.radioButton6.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.radioButton6.Location = new System.Drawing.Point(3, 2);
            this.radioButton6.Name = "radioButton6";
            this.radioButton6.Size = new System.Drawing.Size(57, 17);
            this.radioButton6.TabIndex = 106;
            this.radioButton6.Text = "Normal";
            this.radioButton6.UseVisualStyleBackColor = true;
            // 
            // radioButton7
            // 
            this.radioButton7.AutoSize = true;
            this.radioButton7.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.radioButton7.Location = new System.Drawing.Point(66, 2);
            this.radioButton7.Name = "radioButton7";
            this.radioButton7.Size = new System.Drawing.Size(66, 17);
            this.radioButton7.TabIndex = 106;
            this.radioButton7.Text = "vs. Prog.";
            this.radioButton7.UseVisualStyleBackColor = true;
            // 
            // radioButton8
            // 
            this.radioButton8.AutoSize = true;
            this.radioButton8.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.radioButton8.Location = new System.Drawing.Point(138, 2);
            this.radioButton8.Name = "radioButton8";
            this.radioButton8.Size = new System.Drawing.Size(91, 17);
            this.radioButton8.TabIndex = 106;
            this.radioButton8.Text = "vs. Other Well";
            this.radioButton8.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.radioButton9);
            this.panel2.Controls.Add(this.radioButton10);
            this.panel2.Controls.Add(this.radioButton11);
            this.panel2.Location = new System.Drawing.Point(124, 92);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(239, 24);
            this.panel2.TabIndex = 107;
            // 
            // radioButton9
            // 
            this.radioButton9.AutoSize = true;
            this.radioButton9.Checked = true;
            this.radioButton9.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.radioButton9.Location = new System.Drawing.Point(3, 2);
            this.radioButton9.Name = "radioButton9";
            this.radioButton9.Size = new System.Drawing.Size(57, 17);
            this.radioButton9.TabIndex = 106;
            this.radioButton9.Text = "Normal";
            this.radioButton9.UseVisualStyleBackColor = true;
            // 
            // radioButton10
            // 
            this.radioButton10.AutoSize = true;
            this.radioButton10.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.radioButton10.Location = new System.Drawing.Point(66, 2);
            this.radioButton10.Name = "radioButton10";
            this.radioButton10.Size = new System.Drawing.Size(66, 17);
            this.radioButton10.TabIndex = 106;
            this.radioButton10.Text = "vs. Prog.";
            this.radioButton10.UseVisualStyleBackColor = true;
            // 
            // radioButton11
            // 
            this.radioButton11.AutoSize = true;
            this.radioButton11.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.radioButton11.Location = new System.Drawing.Point(138, 2);
            this.radioButton11.Name = "radioButton11";
            this.radioButton11.Size = new System.Drawing.Size(91, 17);
            this.radioButton11.TabIndex = 106;
            this.radioButton11.Text = "vs. Other Well";
            this.radioButton11.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.radioButton12);
            this.panel3.Controls.Add(this.radioButton13);
            this.panel3.Controls.Add(this.radioButton14);
            this.panel3.Location = new System.Drawing.Point(124, 122);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(239, 24);
            this.panel3.TabIndex = 107;
            // 
            // radioButton12
            // 
            this.radioButton12.AutoSize = true;
            this.radioButton12.Checked = true;
            this.radioButton12.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.radioButton12.Location = new System.Drawing.Point(3, 2);
            this.radioButton12.Name = "radioButton12";
            this.radioButton12.Size = new System.Drawing.Size(57, 17);
            this.radioButton12.TabIndex = 106;
            this.radioButton12.Text = "Normal";
            this.radioButton12.UseVisualStyleBackColor = true;
            // 
            // radioButton13
            // 
            this.radioButton13.AutoSize = true;
            this.radioButton13.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.radioButton13.Location = new System.Drawing.Point(66, 2);
            this.radioButton13.Name = "radioButton13";
            this.radioButton13.Size = new System.Drawing.Size(66, 17);
            this.radioButton13.TabIndex = 106;
            this.radioButton13.Text = "vs. Prog.";
            this.radioButton13.UseVisualStyleBackColor = true;
            // 
            // radioButton14
            // 
            this.radioButton14.AutoSize = true;
            this.radioButton14.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.radioButton14.Location = new System.Drawing.Point(138, 2);
            this.radioButton14.Name = "radioButton14";
            this.radioButton14.Size = new System.Drawing.Size(91, 17);
            this.radioButton14.TabIndex = 106;
            this.radioButton14.Text = "vs. Other Well";
            this.radioButton14.UseVisualStyleBackColor = true;
            // 
            // FilterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(376, 233);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.radioButton5);
            this.Controls.Add(this.radioButton4);
            this.Controls.Add(this.radioButton3);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnNext);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FilterForm";
            this.Text = "Filter";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.RadioButton radioButton5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radioButton6;
        private System.Windows.Forms.RadioButton radioButton7;
        private System.Windows.Forms.RadioButton radioButton8;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton radioButton9;
        private System.Windows.Forms.RadioButton radioButton10;
        private System.Windows.Forms.RadioButton radioButton11;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton radioButton12;
        private System.Windows.Forms.RadioButton radioButton13;
        private System.Windows.Forms.RadioButton radioButton14;
    }
}