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
    public partial class frmGenerateSiteEdition : Form
    {
        public frmGenerateSiteEdition()
        {
            InitializeComponent();
        }

        private void frmGenerateSiteEdition_Load(object sender, EventArgs e)
        {
            txtUserID.Focus();
        }

        private void chbShowPasswords_CheckedChanged(object sender, EventArgs e)
        {
            if(chbShowPasswords.Checked)
            {
                txtPassword.PasswordChar = '\0';
                txtPasswordRepeat.PasswordChar = '\0';
            }
            else
            {
                txtPassword.PasswordChar = '*';
                txtPasswordRepeat.PasswordChar = '*';
            }
        }
    }
}
