using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace DMR
{
    public partial class frmInput : Form
    {
        public frmInput()
        {
            InitializeComponent();

            this.AcceptButton = btnOK;
            this.CancelButton = btnCancel;
        }
        //-------------------------------------------------------
        private void frmInput_Load(object sender, EventArgs e)
        {
            txtInput.Focus();
        }
        //-------------------------------------------------------
    }
}
