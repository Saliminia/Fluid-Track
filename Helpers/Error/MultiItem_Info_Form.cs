using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace DMR
{
    public partial class MultiItem_Info_Form : Form
    {
        //-------------------------------------------------------
        int rowHeight = 50;
        //-------------------------------------------------------
        public MultiItem_Info_Form()
        {
            InitializeComponent();
        }
        //-------------------------------------------------------
        public void SetMode(int mode)//32000: admin  ,  else: non-admin
        {
            if (mode == 32000)
            {
                chbLocation.Visible = true;
                chbException.Visible = true;
                dgvInfo.Columns["colException"].Visible = true;
                dgvInfo.Columns["colLocation"].Visible = true;
            }
            else
            {
                chbLocation.Visible = false;
                chbException.Visible = false;
                dgvInfo.Columns["colException"].Visible = false;
                dgvInfo.Columns["colLocation"].Visible = false;
            }
        }
        //-------------------------------------------------------
        public void Set_Error(params Errors[] errs)
        {
            try
            {
                if (errs.Length == 0)
                    return;

                dgvInfo.Rows.Clear();

                for (int i = 0; i < errs.Length; i++)
                {
                    dgvInfo.Rows.Add(i + 1, errs[i].err, errs[i].fullExceptionMessage, errs[i].ErrorLocation);

                    dgvInfo.Rows[i].Height = rowHeight;
                }

                ShowDialog();
            }
            catch (Exception)
            {
            }
        }
        //-------------------------------------------------------
        private void MultiItem_Info_Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                //e.Cancel = true;
                Hide();
            }
            catch (Exception)
            {
            }
        }
        //-------------------------------------------------------
        private void MultiItem_Info_Form_Load(object sender, EventArgs e)
        {
            try
            {
                this.AcceptButton = btnOK;

                this.Text = "Error/Warning";
            }
            catch (Exception)
            {
            }
        }
        //-------------------------------------------------------
        private void chbErrorParts_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chbItem.Visible) dgvInfo.Columns[0].Visible = chbItem.Checked;
                if (chbErrText.Visible) dgvInfo.Columns[1].Visible = chbErrText.Checked;
                if (chbException.Visible) dgvInfo.Columns[2].Visible = chbException.Checked;
                if (chbLocation.Visible) dgvInfo.Columns[3].Visible = chbLocation.Checked;
            }
            catch (Exception)
            {
            }
        }
        //-------------------------------------------------------
    }
}
