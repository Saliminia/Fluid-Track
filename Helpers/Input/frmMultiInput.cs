using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace DMR
{
    public partial class frmMultiInput : Form
    {
        public frmMultiInput()
        {
            InitializeComponent();
        }
        //-------------------------------------------------------
        private void frmMultiInput_Load(object sender, EventArgs e)
        {

        }
        //-------------------------------------------------------
        //private void frmMultiInput_KeyDown(object sender, KeyEventArgs e)
        //{
        //    //if (e.KeyCode == Keys.Escape)
        //    //{
        //    //    Close();
        //    //    DialogResult = DialogResult.Cancel;
        //    //    e.Handled = true;
        //    //}
        //    //else if (e.KeyCode == Keys.Enter)
        //    //{
        //    //    Close();
        //    //    DialogResult = DialogResult.OK;
        //    //    e.Handled = true;
        //    //}

        //    //e.Handled = false;
        //}
        //-------------------------------------------------------
        private void dgvInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
                DialogResult = DialogResult.Cancel;
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Enter)
            {
                Close();
                DialogResult = DialogResult.OK;
                e.Handled = true;
            }

            e.Handled = false;
        }
        //-------------------------------------------------------
    }
}
