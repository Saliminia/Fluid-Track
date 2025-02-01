using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DMR
{
    public partial class MultiButton_Info_Form : Form
    {
        public MultiButton_Info_Form()
        {
            InitializeComponent();
        }
        //-------------------------------------------------------
        public DialogResult Set_Info_YesNo(string text, string label, string title)
        {
            lblTitle.Text = label;
            this.Text = title;
            txtInfo.Text = text;
            txtInfo.SelectionLength = 0;

            btn2.Visible = btn4.Visible = false;
            
            btn1.Visible = btn3.Visible = true;
            btn1.Text = "Yes"; btn1.DialogResult = DialogResult.Yes;
            btn3.Text = "No"; btn3.DialogResult = DialogResult.No;

            AcceptButton = btn1;
            CancelButton = btn3;

            return ShowDialog();
        }
        //-------------------------------------------------------
        private void MultiButton_Info_Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hide();
        }
        //-------------------------------------------------------
    }
}
