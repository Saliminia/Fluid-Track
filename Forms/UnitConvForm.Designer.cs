namespace DMR
{
    partial class UnitConvForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UnitConvForm));
            this.cboxUnit1 = new System.Windows.Forms.ComboBox();
            this.cboxUnit2 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboxGroup = new System.Windows.Forms.ComboBox();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.txtVal1 = new System.Windows.Forms.TextBox();
            this.txtVal2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cboxUnit1
            // 
            this.cboxUnit1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxUnit1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cboxUnit1.FormattingEnabled = true;
            this.cboxUnit1.Items.AddRange(new object[] {
            "Unit A",
            "Unit B",
            "Unit C"});
            this.cboxUnit1.Location = new System.Drawing.Point(60, 85);
            this.cboxUnit1.Name = "cboxUnit1";
            this.cboxUnit1.Size = new System.Drawing.Size(117, 21);
            this.cboxUnit1.TabIndex = 2;
            this.cboxUnit1.SelectedIndexChanged += new System.EventHandler(this.cboxUnit1_SelectedIndexChanged);
            // 
            // cboxUnit2
            // 
            this.cboxUnit2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxUnit2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cboxUnit2.FormattingEnabled = true;
            this.cboxUnit2.Items.AddRange(new object[] {
            "Unit A",
            "Unit B",
            "Unit C"});
            this.cboxUnit2.Location = new System.Drawing.Point(253, 85);
            this.cboxUnit2.Name = "cboxUnit2";
            this.cboxUnit2.Size = new System.Drawing.Size(117, 21);
            this.cboxUnit2.TabIndex = 3;
            this.cboxUnit2.SelectedIndexChanged += new System.EventHandler(this.cboxUnit2_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(79, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Group:";
            // 
            // cboxGroup
            // 
            this.cboxGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxGroup.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cboxGroup.FormattingEnabled = true;
            this.cboxGroup.Items.AddRange(new object[] {
            "Unit A",
            "Unit B",
            "Unit C"});
            this.cboxGroup.Location = new System.Drawing.Point(124, 15);
            this.cboxGroup.Name = "cboxGroup";
            this.cboxGroup.Size = new System.Drawing.Size(182, 21);
            this.cboxGroup.Sorted = true;
            this.cboxGroup.TabIndex = 0;
            this.cboxGroup.SelectedIndexChanged += new System.EventHandler(this.cboxGroup_SelectedIndexChanged);
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
            this.imageList.Images.SetKeyName(10, "warning");
            this.imageList.Images.SetKeyName(11, "Check");
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
            // txtVal1
            // 
            this.txtVal1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtVal1.Location = new System.Drawing.Point(60, 59);
            this.txtVal1.Name = "txtVal1";
            this.txtVal1.Size = new System.Drawing.Size(117, 20);
            this.txtVal1.TabIndex = 1;
            this.txtVal1.TextChanged += new System.EventHandler(this.txtVal1_TextChanged);
            // 
            // txtVal2
            // 
            this.txtVal2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtVal2.Location = new System.Drawing.Point(253, 59);
            this.txtVal2.Name = "txtVal2";
            this.txtVal2.Size = new System.Drawing.Size(117, 20);
            this.txtVal2.TabIndex = 4;
            this.txtVal2.TextChanged += new System.EventHandler(this.txtVal2_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Value:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Unit:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(210, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Value:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(210, 86);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Unit:";
            // 
            // UnitConvForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(392, 121);
            this.Controls.Add(this.txtVal2);
            this.Controls.Add(this.txtVal1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboxUnit2);
            this.Controls.Add(this.cboxGroup);
            this.Controls.Add(this.cboxUnit1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UnitConvForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Unit Conversion";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UnitConvForm_FormClosing);
            this.Load += new System.EventHandler(this.UnitConvForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboxUnit1;
        private System.Windows.Forms.ComboBox cboxUnit2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboxGroup;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.TextBox txtVal1;
        private System.Windows.Forms.TextBox txtVal2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
    }
}