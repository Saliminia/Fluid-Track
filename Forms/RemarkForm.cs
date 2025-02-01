using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;

using System.Windows.Forms;

namespace DMR
{
    public partial class RemarkForm : Form, IMyForm
    {
        const string CLASS_NAME = "RemarkForm";
        RoleAccess access = null;
        frmMain mainForm = null;
        //=======================================================
        void IMyForm.OnCurrentProjectChanged()
        {
            ClearTextBoxes();
            btnApply.ImageKey = "check";
            btnApply.BackColor = frmMain.checkColor;
        }
        //-------------------------------------------------------
        void IMyForm.OnCurrentRigWellChanged()
        {
            ClearTextBoxes();
            btnApply.ImageKey = "check";
            btnApply.BackColor = frmMain.checkColor;
        }
        //-------------------------------------------------------
        void IMyForm.OnCurrentReportChanged()
        {
            string METHOD = "OnCurrentReportChanged : " + CLASS_NAME;

            if (frmMain.selectedRepID != -1)
            {
                if (!LoadRemarks())
                {
                    InformationManager.Set_Info(new Errors("Can not load data from data base", "", METHOD));
                    //?????logout????
                    return;
                }
            }

            btnApply.ImageKey = "check";
            btnApply.BackColor = frmMain.checkColor;
        }
        //-------------------------------------------------------
        void IMyForm.OnInit()
        {
            //~~~~~~~~~~~~~~~~~ AT_None = Hide ~~~~~~~~~~~~~~~~~~~~
            if (access.remarks == RoleAccess.AcessTypes.AT_None)
            {
                tbLayout.Visible = false;
            }
            else if (access.remarks == RoleAccess.AcessTypes.AT_ReadOnly)
            {
                tbLayout.Enabled = false;
                btnApply.Enabled = false;
            }
            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            ClearTextBoxes();
            btnApply.ImageKey = "check";
            btnApply.BackColor = frmMain.checkColor;
        }
        //-------------------------------------------------------
        void IMyForm.OnCurrentUnitsChanged()
        {
            UpdateComputationsByPressRefereshButtons();
        }
        //-------------------------------------------------------
        void UpdateComputationsByPressRefereshButtons()
        {
        }
        //-------------------------------------------------------
        void ClearTextBoxes()
        {
            txtRemarkDrill.Text = "";
            txtRemarkDrillFluid.Text = "";
            txtRemarkSolidControl.Text = "";
            txtRemarkRigEq.Text = "";
            txtRemarkMaterial.Text = "";
            txtRemarkLesson.Text = "";
        }
        //-------------------------------------------------------
        public RemarkForm(RoleAccess access, frmMain mainForm)
        {
            this.access = access;
            this.mainForm = mainForm;

            InitializeComponent();
        }
        //-------------------------------------------------------
        private void RemarkForm_Load(object sender, EventArgs e)
        {
#if !NOLOCK
            //Security String (Fetch string value from HLock)
            object obj1 = new object();
            uint RProg = (uint)Program.Rand.Next(2147483647);
            obj1 = Program.arm.GetARMData(ARM.IWhichData.STRING_VAL1, RProg);
            string strVal1 = Program.cls.DecodData(obj1, ARM.IWhichData.STRING_VAL1, RProg);
            if (strVal1.Trim().Substring(0, 35) != "rre75revgdsur4edcg234583sdg1pdwer67")
            {
                MessageBox.Show("Connecting to Security Key Lock failed. Please plug security lock.", "PDF DMR System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.LockID = string.Empty;
                this.Close();
            }
#endif
        }
        //-------------------------------------------------------
        //returns:   true (success) , false(failure)
        bool LoadRemarks()
        {
            //string METHOD = "LoadRemarks : " + CLASS_NAME;

            try
            {
                string query =
                          " select	Drill_Remarks,  "
                        + " DrillFluidTreat_Remarks, "
                        + " SolidControl_Remarks, "
                        + " Rig_Remarks, "
                        + " Material_Remarks, "
                        + " Lesson_Remarks  "
                        + " from at_Report "
                        + " where ID = " + frmMain.selectedRepID.ToString();


                DataSet ds = ConnectionManager.ExecQuery(query, 1);

                if (ds != null)
                {
                    txtRemarkDrill.Text = ds.Tables[0].Rows[0][0].ToString();
                    txtRemarkDrillFluid.Text = ds.Tables[0].Rows[0][1].ToString();
                    txtRemarkSolidControl.Text = ds.Tables[0].Rows[0][2].ToString();
                    txtRemarkRigEq.Text = ds.Tables[0].Rows[0][3].ToString();
                    txtRemarkMaterial.Text = ds.Tables[0].Rows[0][4].ToString();
                    txtRemarkLesson.Text = ds.Tables[0].Rows[0][5].ToString();

                    ds.Dispose();
                    return true;
                }
            }
            catch (Exception)
            {
                //ErrorManager.Set_Error(new Errors("Can not load data from data base", ex.Message, METHOD));
                //will be handled outside
            }

            return false;
        }
        //-------------------------------------------------------
        private void btnApply_Click(object sender, EventArgs e)
        {
            if (frmMain.selectedRepID == -1)
                return;

            if (access.remarks != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;

            List<ProcedureParam> prms = new List<ProcedureParam>();
            prms.Add(new ProcedureParam("@ReportID", frmMain.selectedRepID.ToString(), ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Report"));
            prms.Add(new ProcedureParam("@Drill_Remarks", txtRemarkDrill.Text, 500));
            prms.Add(new ProcedureParam("@DrillFluidTreat_Remarks", txtRemarkDrillFluid.Text, 500));
            prms.Add(new ProcedureParam("@SolidControl_Remarks", txtRemarkSolidControl.Text, 500));
            prms.Add(new ProcedureParam("@Rig_Remarks", txtRemarkRigEq.Text, 500));
            prms.Add(new ProcedureParam("@Material_Remarks", txtRemarkMaterial.Text, 500));
            prms.Add(new ProcedureParam("@Lesson_Remarks", txtRemarkLesson.Text, 500));

            string simpErr;
            Errors critErr;
            Int64 resultStat;

            int ret = Procedures.CallProcedure("prc_at_Report_update_Remark", prms, out simpErr, out critErr, out resultStat);

            if (ret != 0)
            {
                if (ret == 1)
                    InformationManager.Set_Info(simpErr);
                else
                    InformationManager.Set_Info(critErr);

                return;
            }

            btnApply.ImageKey = "Check";
            btnApply.BackColor = frmMain.checkColor;
        }
        //-------------------------------------------------------
        private void txts_TextChanged(object sender, EventArgs e)
        {
            if (frmMain.selectedRepID == -1)
                return;

            if (access.remarks != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;

            btnApply.ImageKey = "warning";
            btnApply.BackColor = frmMain.warningColor;
        }
        //-------------------------------------------------------
    }
}
