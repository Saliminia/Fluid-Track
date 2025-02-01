using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace DMR
{
    public partial class SingleItem_Info_Form : Form
    {
        //-------------------------------------------------------
        public SingleItem_Info_Form()
        {
            InitializeComponent();
        }
        //-------------------------------------------------------
        public void Set_Info(string text, string label, string title)
        {
            lblTitle.Text = label;
            this.Text = title;
            txtInfo.Text = text;
            txtInfo.SelectionLength = 0;

            ShowDialog();
        }
        //-------------------------------------------------------
        private void SingleItem_Info_Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            //e.Cancel = true;
            Hide();
        }
        //-------------------------------------------------------
        private void SingleItem_Info_Form_Load(object sender, EventArgs e)
        {
            this.AcceptButton = btnOK;
        }
        //-------------------------------------------------------
    }
}
