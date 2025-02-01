using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DMR.Helpers;
using System.Runtime.InteropServices;

#if !NOLOCK
using AxARM;
using ARM;
#endif


namespace DMR
{
    public partial class SplashForm : Form
    {
#if !NOLOCK
        public AxARM.AxARMClass arm;
#endif

        public Boolean bFormStatus
        {
            get;
            set;
        }
        public Boolean bSecurityLock
        {
            get;
            set;
        }

        public SplashForm()
        {
            InitializeComponent();
            //Visible = false;


#if !NOLOCK

            try
            {
                //DateTime iAuth = DateTime.Now.AddSeconds(2);
                bFormStatus = true;
                bSecurityLock = true;
                ModifyProgressBarColor.SetState(progressBar1, 2);
                //DialogResult dr = DialogResult.Retry;

                //do
                //{
                //    try
                //    {
                //        if (Program.LockID == string.Empty || Program.LockID == "Lock Not Found!")
                //        {
                //            AlxSSB.AlxSSBClass myLock = new AlxSSB.AlxSSBClass();
                //            Program.LockID = myLock.GetSerialNo();
                //        }
                //    }
                //    catch
                //    {
                //        Program.LockID = string.Empty;
                //    }

                //    if ((Program.LockID == string.Empty || Program.LockID == "Lock Not Found!"))
                //    {
                //        dr = MessageBox.Show("Connecting to Security Key Lock failed. Please plug security lock.", "PDF DMR System", MessageBoxButtons.RetryCancel, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                //        if (dr != DialogResult.Retry)
                //        {
                //            bFormStatus = false;
                //            bSecurityLock = false;
                //        }
                //    }
                //    else
                //        dr = DialogResult.OK;
                //} while (dr == DialogResult.Retry && bSecurityLock == true);

                //if (bSecurityLock)
                //{
                //    DateTime CalTime = SLock.CheckSpecification();
                //    if (CalTime < DateTime.Now)
                //    {
                //        MessageBox.Show("Software Security Failed. Please Activate this program.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //        bFormStatus = false;
                //        SLock.ClearLic();
                //    }
                //    else
                //    {
                //        timer1.Enabled = true;
                //    }
                //}
            }
            catch { };
#endif

        }


        int counter = 0;

        //private void timer1_Tick(object sender, EventArgs e)
        //{

        //    counter++;
        //    if (counter > 1)
        //        Close();

        //    lblLoading.Text = "Loading ";

        //    for (int i = 0; i < counter % 5; i++)
        //        lblLoading.Text += ".";

        //    this.TopLevel = true;
        //    this.TopMost = true;
        //}


        private void timer1_Tick(object sender, EventArgs e)
        {

            progressBar1.ForeColor = Color.Black;
            counter += 10;

#if !NOLOCK
            //if (Program.LockID == string.Empty || Program.LockID == "Lock Not Found!")
            //    this.Close();
            if (counter >= 100)
            {
                progressBar1.Maximum = progressBar1.Value = 100;
                if (counter > 200)
                    Close();
            }
            else
                progressBar1.Value = counter;
#endif
            lblLoading.Text = "Loading ";

            for (int i = 0; i < counter % 5; i++)
                lblLoading.Text += ".";

            this.TopLevel = true;
            this.TopMost = true;
        }

        private void SplashForm_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;

#if NOLOCK
            Close();
#endif

        }
    }

    public class NewProgressBar : ProgressBar
    {
        public NewProgressBar()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle rec = e.ClipRectangle;

            rec.Width = (int)(rec.Width * ((double)Value / Maximum)) - 4;
            if (ProgressBarRenderer.IsSupported)
                ProgressBarRenderer.DrawHorizontalBar(e.Graphics, e.ClipRectangle);
            rec.Height = rec.Height - 4;
            e.Graphics.FillRectangle(Brushes.Black, 2, 2, rec.Width, rec.Height);
            base.OnPaint(e);
        }
    }

    public static class ModifyProgressBarColor
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr w, IntPtr l);
        public static void SetState(this ProgressBar pBar, int state)
        {
            SendMessage(pBar.Handle, 1040, (IntPtr)state, IntPtr.Zero);
        }
    }
}
