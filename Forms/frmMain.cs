using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using DMR.Helpers;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;


#if !NOLOCK
using AxARM;
#endif


//check project ID [to exist], when inserting anything [choosing freeID]


//select report date when adding report 
//also check reports date to be different when adding or updating reports ...

//?????????????? check(limmit) of values

//?????????????? id , ... overflow   (larger than range of ids of project [if you have time] )    ??????????????????????

//??????? we can build a user error log which contains hints to solve problem (e.g. press refresh button) ????

//check deleted/undeleted   project, rig, well  when joining DB Tables , ...

//check user-proj access when restore, delete projects 

//create limits on datagridviews row [column] count  or other paramters to avoid overflow

//update computations when changing units ....

//data grid view: columns : autosize mode : fill  ==> may cause some exception at run time !!!! 
//ex: This operation cannot be performed while an auto-filled column is being resized


//do not show exceptions to user !!!!

//prevent decompiling code ?????
//password on DB ????????????????


//[for later] Refine at_HolesOfProjects  (update names of prj, rig, ...)  and  unify references

//set min size for columns of all DGVs


 
namespace DMR



{
    public interface IMyForm
    {
        //Dependability
        void OnCurrentProjectChanged();
        void OnCurrentRigWellChanged();
        void OnCurrentReportChanged();

        //Init
        void OnInit();

        //Unit
        void OnCurrentUnitsChanged();
    }
    //-------------------------------------------------------
    public partial class frmMain : Form
    {
        public static int selectedPrjID = -1;
        public static string selectedPrjName = "";
        public static Int64 selected_RWID = -1;
        public static Int64 selected_RW_RigID = -1;
        public static string selected_RW_RigName = "";
        public static Int64 selected_RW_WellID = -1;
        public static string selected_RW_WellName = "";
        public static Int64 selectedRepID = -1;
        public static int selectedRepNum = -1;
        public static int selectedRepRev = -1;
        //=======================================================
        public UserRole curUserRole = new UserRole();
#if !NOLOCK
        private AlxSSB.AlxSSBClass myLock = new AlxSSB.AlxSSBClass();
#endif
        //=======================================================
        public List<object> subForms = new List<object>();
        public Dictionary<RadioButton, int> idOfRadioButtons = new Dictionary<RadioButton, int>();
        //=======================================================

        public int StartupFormID = -1;
        public int PorjectFormID = -1;
        public int DrillingFluidsProgramFormID = -1;
        public int RigFormID = -1;
        public int WellInfoFormID = -1;
        public int RigWellReportFormID = -1;
        public int SolidConEqFormID = -1;
        public int DrilliOpFormID = -1;
        public int MudLossFormID = -1;
        public int MudVolManagementFormID = -1;
        public int MudPropFormID = -1;
        public int HydraulicFormID = -1;
        public int SolidAnaFormID = -1;
        public int InvManagementFormID = -1;
        public int RemarkFormID = -1;
        public int ReportsFormID = -1;
        //=======================================================
        public static Color checkColor = Color.LightGreen;
        public static Color warningColor = Color.LightSalmon;
        //=======================================================
        UnitConvForm frmUConv = new UnitConvForm();
        //=======================================================
        public frmMain()
        {
            InitializeComponent();

#if OFFICE_EDITION
            Text = "PDF Management [Office Edition]";
#elif SITE_EDITION
            Text = "PDF Management [Site Edition]";
#else
            Text = "PDF Management [Invalid Edition]";
#endif
        }
        //-------------------------------------------------------
        public bool IsCurUserAdmin()
        {
            return (curUserRole.userID.ToLower() == "admin");
        }
        //-------------------------------------------------------
        int AddSubForm(Form sForm, RadioButton relatedRadioButton)
        {
            subForms.Add(sForm);

            int id = subForms.Count - 1;
            idOfRadioButtons.Add(relatedRadioButton, id);

            return id;
        }
        //-------------------------------------------------------
        void InitForms()
        {
            StartupFormID = AddSubForm(new StartupForm(curUserRole.accessParams, this), rbtnStartupForm);
            PorjectFormID = AddSubForm(new ProjectForm(curUserRole.accessParams, this), rbtnPorjectForm);
            DrillingFluidsProgramFormID = AddSubForm(new DrillingFluidsProgramForm(curUserRole.accessParams, this), rbtnDrillingFluidsProgramForm);
            RigFormID = AddSubForm(new RigForm(curUserRole.accessParams, this), rbtnRigForm);
            WellInfoFormID = AddSubForm(new WellForm(curUserRole.accessParams, this), rbtnWellInfoForm);
            RigWellReportFormID = AddSubForm(new RigWellReportForm(curUserRole.accessParams, this), rbtnRigWellReportForm);
            SolidConEqFormID = AddSubForm(new SolidConEqForm(curUserRole.accessParams, this), rbtnSolidConEqForm);
            DrilliOpFormID = AddSubForm(new DrilliOpForm(curUserRole.accessParams, this), rbtnDrilliOpForm);
            MudLossFormID = AddSubForm(new MudLossForm(curUserRole.accessParams, this), rbtnMudLossForm);
            MudVolManagementFormID = AddSubForm(new MudVolManagementForm(curUserRole.accessParams, this), rbtnMudVolManagementForm);
            MudPropFormID = AddSubForm(new MudPropForm(curUserRole.accessParams, this), rbtnMudPropForm);
            HydraulicFormID = AddSubForm(new HydraulicForm(curUserRole.accessParams, this), rbtnHydraulicForm);
            SolidAnaFormID = AddSubForm(new SolidAnaForm(curUserRole.accessParams, this), rbtnSolidAnaForm);
            InvManagementFormID = AddSubForm(new InvManagementForm(curUserRole.accessParams, this), rbtnInvManagementForm);
            RemarkFormID = AddSubForm(new RemarkForm(curUserRole.accessParams, this), rbtnRemarkForm);
            ReportsFormID = AddSubForm(new ReportsForm(), rbtnReportingForm);

            for (int i = 0; i < subForms.Count; i++)
            {
                ((Form)subForms[i]).TopLevel = false;
                ((Form)subForms[i]).Parent = pnlMain;
                //~~~~~~~~~~~~~~
                ((Form)subForms[i]).Left = 0;
                ((Form)subForms[i]).Top = 0;
                ((Form)subForms[i]).Size = pnlMain.ClientSize;
            }

            //-------------
            //Initialize Unit Label Tags
            ProjectForm prjForm = (ProjectForm)subForms[PorjectFormID];
            prjForm.WriteAllUnitsToTags();
        }
        //-------------------------------------------------------
        private void pnlMain_Resize(object sender, EventArgs e)
        {
            for (int i = 0; i < subForms.Count; i++)
            {
                //NOTE: if you want scroll bars of pnlMain to be shown and usable,  do not use 'Dock=DockStyle.Fill'  and   'Anchor=left,right,top,bottom'  for forms
                //Instead use  Resize event
                ((Form)subForms[i]).Size = pnlMain.ClientSize;
            }
        }
        //-------------------------------------------------------
        private void frmMain_Load(object sender, EventArgs e)
        {
            InitForms();

            rbtnStartupForm.Checked = true;

            OnInit();//init (and maybe clean)
            OnCurrentProjectChanged();//clean [to be sure]
            OnCurrentUnitChanged();//Init units
        }
        //-------------------------------------------------------
        private void rbtnForm_CheckedChanged(object sender, EventArgs e)
        {
            if (sender == null)
                return;

            //Hide All
            for (int i = 0; i < subForms.Count; i++)
                ((Form)subForms[i]).Visible = false;

            RadioButton rbtn = (RadioButton)sender;

            if (rbtn.Checked)
            {
                int id = -1;

                if (!idOfRadioButtons.TryGetValue(rbtn, out id))
                    return;

                ((Form)subForms[id]).Location = new Point(0, 0);
                ((Form)subForms[id]).Visible = true;
            }
        }
        //-------------------------------------------------------
        public void OnInit()
        {
            selectedPrjID = -1;
            selectedPrjName = "";
            selected_RWID = -1;
            selected_RW_RigID = -1;
            selected_RW_RigName = "";
            selected_RW_WellID = -1;
            selected_RW_WellName = "";
            selectedRepID = -1;
            selectedRepNum = -1;
            selectedRepRev = -1;

            lblCurProject.Text = lblCurRigWell.Text = lblCurRep.Text = "";

            for (int i = 0; i < subForms.Count; i++)
                ((IMyForm)subForms[i]).OnInit();
        }
        //-------------------------------------------------------
        public void OnCurrentProjectChanged()
        {
            lblCurRigWell.Text = lblCurRep.Text = "";

            for (int i = 0; i < subForms.Count; i++)
                ((IMyForm)subForms[i]).OnCurrentProjectChanged();
        }
        //-------------------------------------------------------
        public void OnCurrentRigWellChanged()
        {
            lblCurRep.Text = "";

            for (int i = 0; i < subForms.Count; i++)
                ((IMyForm)subForms[i]).OnCurrentRigWellChanged();
        }
        //-------------------------------------------------------
        public void OnCurrentReportChanged()
        {
            for (int i = 0; i < subForms.Count; i++)
                ((IMyForm)subForms[i]).OnCurrentReportChanged();
        }
        //-------------------------------------------------------
        public void OnCurrentUnitChanged()
        {
            for (int i = 0; i < subForms.Count; i++)
                ((IMyForm)subForms[i]).OnCurrentUnitsChanged();
        }
        //-------------------------------------------------------

        private void btnCalc_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process p = System.Diagnostics.Process.Start("calc.exe");
            //p.WaitForInputIdle();
        }
        //-------------------------------------------------------
        private void btnUnitConv_Click(object sender, EventArgs e)
        {
            frmUConv.Visible = false;
            frmUConv.Show(this);
        }
        //-------------------------------------------------------
        private void pboxStartup_Click(object sender, EventArgs e)
        {
            rbtnStartupForm.Checked = true;
        }
        //-------------------------------------------------------
        private void btnLogOut_Click(object sender, EventArgs e)
        {
            Close();
        }
        //-------------------------------------------------------
        private void btnAccount_Click(object sender, EventArgs e)
        {
            if (curUserRole.userID.ToLower() == "admin")
            {
#if OFFICE_EDITION
                frmAdminAccount fac = new frmAdminAccount(this);
                fac.ShowDialog();
#endif
            }
            else
            {
                frmAccount fac = new frmAccount(frmAccount.InteractMode.Edit, curUserRole.userID, this);
                fac.ShowDialog();
            }
        }
        //-------------------------------------------------------
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
#if !NOLOCK
            if (Program.LockID.Trim().Length > 5)
                if (InformationManager.Set_Multi_Btn_Info_YesNo("Are you sure?", "Message:", "Exiting Program") != DialogResult.Yes)
                {
                    e.Cancel = true;
                    return;
                }
                else
                {
                    Program.strLog += SLock.GenDetail(2, "is Logged off by user", "Logoff button pressed", curUserRole.userID.Trim());
                }
            else
                Program.strLog += SLock.GenDetail(2, "is Logged off by System", "Security Lock not found", curUserRole.userID.Trim());

            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                SLock.SendLog(curUserRole.userID);
            }).Start();

#else
            if (InformationManager.Set_Multi_Btn_Info_YesNo("Are you sure?", "Message:", "Exiting Program") != DialogResult.Yes)
            {
                e.Cancel = true;
                return;
            }
#endif
        }
        //-------------------------------------------------------
        private void rbtnReportingForm_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbtnReportingForm.Checked)
                {
                    WellForm wellForm = (WellForm)subForms[WellInfoFormID];
                    wellForm.btnWellReporting_Click(null, null);
                }
            }
            catch (Exception)
            {
            }
        }
        //-------------------------------------------------------
        private void tmrUpdate_Tick(object sender, EventArgs e)
        {
            lblCurTime.Text = DateTime.Now.ToShortTimeString();
            lblCurDate.Text = DateTime.Now.ToShortDateString();
            lblCurDateShamsi.Text = ShamsiDate.ShamsiEquivalent(DateTime.Now);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
#if !NOLOCK
            string CurLockID = string.Empty;
            if (Program.strLog.Trim().Length > 5)
                SLock.SendLog(curUserRole.userID);
            if (Program.LockID.Trim().Length < 5)
            {
                this.Close();
                return;
            }
            DialogResult dr = DialogResult.Retry;

            //Program.LockID = myLock.GetSerialNo();

            uint RProg = (uint)Program.Rand.Next(2147483647);

            RProg = (uint)Program.Rand.Next(2147483647);

            Program.obj = Program.arm.GetARMData(ARM.IWhichData.SERIAL_NUM, RProg);
            CurLockID = Program.cls.DecodData(Program.obj, ARM.IWhichData.SERIAL_NUM, RProg);

            if (CurLockID.Trim() == string.Empty || CurLockID.Trim() == "Lock Not Found!")
            {
                if (Program.NoLockCount > 10)
                    Program.LockID = CurLockID;
                else
                    Program.NoLockCount++;
            }
            else
            {
                Program.NoLockCount = 0;
                Program.LockID = CurLockID.Trim();
            }


            while (Program.LockID == string.Empty || Program.LockID == "Lock Not Found!")
            {
                //this.timer1.Enabled = false;
                dr = MessageBox.Show("Connecting to Security Key Lock failed. Please plug security lock.", "PDF DMR System", MessageBoxButtons.RetryCancel, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                if (dr != DialogResult.Retry)
                {
                    Program.LockID = string.Empty;
                    this.Close();
                    break;
                }
                Program.LockID = myLock.GetSerialNo();
            }
            //this.timer1.Enabled = true;
#endif
        }
        //-------------------------------------------------------


    }
}
