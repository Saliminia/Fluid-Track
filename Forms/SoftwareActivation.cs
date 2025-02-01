using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using DMR.Helpers;

#if !NOLOCK
using SysEnhanced;
#endif

namespace DMR.Forms
{

    public partial class SoftwareActivation : Form
    {
#if !NOLOCK
        public static int i = 0;
        public SoftwareActivation()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Enabled == true)
            {
                btnLicGenerate.Enabled = true;
                btnCancel.Enabled = true;
                checkBox1.Enabled = false;
            }
            else
            {
                btnLicGenerate.Enabled = false;
                btnCancel.Enabled = false;
                checkBox1.Enabled = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLicGenerate_Click(object sender, EventArgs e)
        {
            progressBar1.Visible = true;
            progressBar1.Value = 1;
            i = 1;

            string HWinfo = string.Empty;
            Thread HWThread = new Thread(() => { HWinfo = HardwareInfo.GetHWInfoWithCode(); });
            HWThread.Start();
            //HWThread.Join(1000);

            while (HWinfo.Trim() == "")
            {
                if (i < 96)
                {
                    progressBar1.Value = i++;
                    System.Threading.Thread.Sleep(50);
                }
            }

            string DID = "#43420=" + Program.LockID;
            HWinfo += DID.Trim();

            //HWinfo = HardwareInfo.GetHWInfo();
            McDESEnhanced McDESKey = new McDESEnhanced("pas5pr@se3Hwdmpo00hj1", "@1B2c3D3e7F6g7H8");
            string CText = McDESKey.Encrypt("1B2c3D3e7F6g7H8");

            CText = McDESKey.Encrypt(HWinfo);

            rtextRequestString.Text = CText;
            btnSaveToFile.Enabled = true;
            btnImportActivationFile.Enabled = true;
            progressBar1.Value = 100;

        }

        private void btnSaveToFile_Click(object sender, EventArgs e)
        {
            Stream LicStream;
            SaveFileDialog LicFile = new SaveFileDialog();
            LicFile.Filter = "License Request files (*.req)|*.req";
            LicFile.FilterIndex = 1;
            LicFile.RestoreDirectory = true;

            if (LicFile.ShowDialog() == DialogResult.OK)
            {
                if ((LicStream = LicFile.OpenFile()) != null)
                {
                    byte[] LicStringBytes = Encoding.ASCII.GetBytes(rtextRequestString.Text.Trim());
                    LicStream.Write(LicStringBytes, 0, rtextRequestString.Text.Trim().Length);
                    LicStream.Close();
                }
            }
        }

        private void btnImportActivationFile_Click(object sender, EventArgs e)
        {
            StreamReader MyLicense;
            byte[] LicByte;
            string strLicense = string.Empty;
            OpenFileDialog ImpLicFile = new OpenFileDialog();
            ImpLicFile.Filter = "License files (.lic)|*.lic";
            ImpLicFile.FilterIndex = 1;
            ImpLicFile.RestoreDirectory = true;

            if (ImpLicFile.ShowDialog() == DialogResult.OK)
            {
                StreamReader sr = new StreamReader(ImpLicFile.FileName);
                strLicense = sr.ReadToEnd();
            }
            SLock.ApplyLic(strLicense);

            DateTime CalTime = SLock.CheckSpecification();
            if (CalTime < DateTime.Now)
            {
                //SLock.ClearLic();
                MessageBox.Show("Software Activation Failed because your license not valid. Please Contact PDF Support Team.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Activation Successfully. Please Rerun DMR Program.", "Activated", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            this.Close();

        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBox1.Enabled == true)
            {
                btnLicGenerate.Enabled = true;
                btnCancel.Enabled = true;
                checkBox1.Enabled = false;
            }
            else
            {
                btnLicGenerate.Enabled = false;
                btnCancel.Enabled = false;
                checkBox1.Enabled = true;
            }
        }

#endif
    }
}
