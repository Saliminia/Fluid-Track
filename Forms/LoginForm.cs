using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.IO;
using OfficeOpenXml;
using System.Reflection;
using System.Threading;
using DMR.DMRWebService;
using DMR.Helpers;

namespace DMR
{
    public partial class LoginForm : Form
    {
        const string CLASS_NAME = "LoginForm";
        //=======================================================
        public LoginForm()
        {
            InitializeComponent();
#if NOLOCK
            //MessageBox.Show("This is a test version with no lock");
#endif

#if DEBUG
            txtUserName.Text = "ADMIN";
            txtPasswrd.Text = "123456";
#endif

#if OFFICE_EDITION
            Text = "Login [Office Edition]";
#elif SITE_EDITION
            Text = "Login [Site Edition]";
#else
            Text = "Login [Invalid Edition]";
#endif
        }

        //-------------------------------------------------------
        //Class1 cls = new Class1();
        //Random Rand = new Random();
        //object obj = new object();
        //byte[] KeyAES = new byte[16];

        //-------------------------------------------------------
        private bool IsUserValid()
        {
#if !NOLOCK
            string strwResult = string.Empty, LogString = string.Empty;
            ClientService wService = new ClientService();
#endif
            string userID = txtUserName.Text.Trim();
            string pass = EncoderDecoder.EncodePassword(txtPasswrd.Text/*do not trim*/);

#if !NOLOCK
            if (Program.LockID == string.Empty)
            {
                this.Close();
                return false;
            }
#endif

#if !OFFICE_EDITION

#if !DEBUG
            if (userID.ToLower() == "admin")//there is no admin user for 'Site Edition'
            {
                InformationManager.Set_Info("Invalid ID and/or password");
                return false;
            }
#endif

#endif

            //prc_at_User_check
            {
                List<ProcedureParam> prms = new List<ProcedureParam>();
                prms.Add(new ProcedureParam("@ID", userID, 50));
                prms.Add(new ProcedureParam("@Pass", pass, 50));

                string simpErr;
                Errors critErr;
                Int64 resultStat;

                int ret = Procedures.CallProcedure("prc_at_User_check", prms, out simpErr, out critErr, out resultStat);

                if (ret != 0)
                {
#if !NOLOCK
                    Program.strLog += SLock.GenDetail(1, "Login Failed", "Entered Password: " + txtPasswrd.Text, txtUserName.Text.Trim());
#endif
                    if (ret == 1)
                        InformationManager.Set_Info(simpErr);
                    else
                        InformationManager.Set_Info(critErr);

#if !NOLOCK
                    new Thread(() =>
                    {
                        Thread.CurrentThread.IsBackground = true;
                        SLock.SendLog(userID);
                    }).Start();
#endif
                    return false;
                }

#if !NOLOCK
                Program.strLog += SLock.GenDetail(1, "Login Successfully", "", txtUserName.Text.Trim());
                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    SLock.SendLog(userID);
                }).Start();
#endif
            }

            return true;
        }
        //-------------------------------------------------------
        const int MAX_USER_CHECK = 3;
        int chechUserValidity = 0;
        //-------------------------------------------------------
        private void btnLogin_Click(object sender, EventArgs e)
        {
#if !NOLOCK
            uint Rand1;
            byte bteRes = 0;
            object obj = new object();
            byte[] bte1 = new byte[16];
            byte bb = 0;
            byte[] bteRand = new byte[16];

            for (int i = 0; i < 16; i++)
                bteRand[i] = (byte)Program.Rand.Next(255);

            while (bteRes != 1 && bteRes != 101 && bteRes != 106 && bteRes != 113)
            {
                Rand1 = (uint)Program.Rand.Next(2147483647);
                obj = axARMClass2.Authenticate(Rand1);
                bteRes = Program.cls.CheckAuthenticate(obj, Rand1);
            }

            if (bteRes == 1)
            {
                Program.KeyAES = Encoding.Default.GetBytes("newGeneration200");
                bte1 = Program.cls.CreateUserKey("BA72E8207F63EA3DA8C7377D222E5163", Program.KeyAES);
                //axARMClass1.FindFirstARM(bte1, "A4D31D9495552C46A4071DC702744B545C0DD4CD4D8EE37F5C40D4017D3CC68EF73B70FBE8BC7FADF76E702F19D4B991", "6078412E2EB01C8154FD7E9C8B8B2DF457501034FA3B6F7A990DDBDC7FC7CA6629561C1A53598B73C556E658902B089D");
                axARMClass2.FindFirstARM(bte1, "5E31D6F14FB2E5A35E64D6255FADBB1F12338AF303B499A512668A27C1DDD69338D5B0962957BF483809B0C9F3912E92", "B516B51AEF511A7D89C9495261A08A46E0C451A0B40B3C90CCA6ACCE621AF57052C00DA2F80D409680A4F0CCFC6A61CE");
                obj = axARMClass2.GetARMErrorCode(bteRand);
                bb = Program.cls.GenerateErrorCode(obj, Program.KeyAES, bteRand);
                if (bb == 100)
                {
                    uint RProg = (uint)Program.Rand.Next(2147483647);

                    RProg = (uint)Program.Rand.Next(2147483647);
                    obj = axARMClass2.GetARMData(ARM.IWhichData.SERIAL_NUM, RProg);
                    Program.LockID = Program.cls.DecodData(obj, ARM.IWhichData.SERIAL_NUM, RProg);
                    Program.arm = axARMClass2;

                }
            }
            else
            {
                MessageBox.Show("Connecting to Security Key Lock failed. Please plug security lock.", "PDF DMR System", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                return;
            }
#endif

            InformationManager.Set_MultiInfo_Mode(0);//reset

            if (!IsUserValid())
            {
                chechUserValidity++;
                if (chechUserValidity == MAX_USER_CHECK)
                {
                    Thread.Sleep(15000);
                    chechUserValidity--;
                }
                return;
            }

            string userID = txtUserName.Text.Trim();

            if (UserOperations.IsUserLoggedIn(userID))
            {
                InformationManager.Set_Info("This user has logged in previously but not logged out!!!");
                UserOperations.UserLogout(userID);
            }

            frmMain mainForm = new frmMain();
            mainForm.curUserRole.LoadAccessFromDB(userID);

#if DEBUG
            InformationManager.Set_MultiInfo_Mode(32000);
#else
            InformationManager.Set_MultiInfo_Mode((userID.ToLower() == "admin") ? 32000 : 0);
#endif

            mainForm.lblUserID.Text = userID;

            UserOperations.UserLogin(userID);

            Hide();

#if DEBUG
            //nothing yet
#else
            txtPasswrd.Text = "";
#endif

            mainForm.ShowDialog();

            UserOperations.UserLogout(userID);

            Show();
        }
        //-------------------------------------------------------
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
        //-------------------------------------------------------
        private void btnDataTransfer_Click(object sender, EventArgs e)
        {
            if (!IsUserValid())
            {
                chechUserValidity++;
                if (chechUserValidity == MAX_USER_CHECK)
                {
                    Thread.Sleep(15000);
                    chechUserValidity--;
                }
                return;
            }

            string userID = txtUserName.Text.Trim();

            string role = UserOperations.UserRole(userID);

            if (role.ToLower() != "admin" && role.ToLower() != "site/mud engineer")
            {
                InformationManager.Set_Info("Access denied for your role.");
                return;
            }

            Hide();

#if DEBUG
            //nothing yet
#else
            txtPasswrd.Text = "";
#endif

#if OFFICE_EDITION
            frmDataImport_Office office = new frmDataImport_Office();
            office.ShowDialog();
#else
            frmDataImport_Site site = new frmDataImport_Site();
            site.ShowDialog();
#endif

            Show();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
#if !NOLOCK
            try
            {
                //DateTime iAuth = DateTime.Now.AddSeconds(2);
                bool bFormStatus = true;
                bool bSecurityLock = true;
                DialogResult dr = DialogResult.Retry;

                do
                {
                    try
                    {
                        if (Program.LockID == string.Empty || Program.LockID == "Lock Not Found!")
                        {
                            AlxSSB.AlxSSBClass myLock = new AlxSSB.AlxSSBClass();
                            Program.LockID = myLock.GetSerialNo();
                        }
                    }
                    catch
                    {
                        Program.LockID = string.Empty;
                    }

                    if ((Program.LockID == string.Empty || Program.LockID == "Lock Not Found!"))
                    {
                        dr = MessageBox.Show("Connecting to Security Key Lock failed. Please plug security lock.", "PDF DMR System", MessageBoxButtons.RetryCancel, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                        if (dr != DialogResult.Retry)
                        {
                            bFormStatus = false;
                            bSecurityLock = false;
                        }
                    }
                    else
                        dr = DialogResult.OK;
                } while (dr == DialogResult.Retry && bSecurityLock == true);

                if (bSecurityLock)
                {
                    DateTime CalTime = SLock.CheckSpecification();
                    if (CalTime < DateTime.Now)
                    {
                        //MessageBox.Show("Software Security Failed. Please Activate this program.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //bFormStatus = false;
                        //SLock.ClearLic();
                        //Forms.SoftwareActivation frmAF = new Forms.SoftwareActivation();
                        //frmAF.ShowDialog();
                    }
                }
                if (bFormStatus == false || bSecurityLock == false)
                {
                    //this.Close();
                    //Application.Exit();
                }
            }
            catch { };
#endif
        }
        //-------------------------------------------------------

    }
}
