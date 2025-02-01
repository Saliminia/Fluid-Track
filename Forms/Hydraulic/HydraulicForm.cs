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
    public partial class HydraulicForm : Form, IMyForm
    {
        const string CLASS_NAME = "HydraulicForm";
        RoleAccess access = null;
        frmMain mainForm = null;

        bool skipComputations = false;

        string selUnitNozzleVelocity = "";
        //=======================================================
        void IMyForm.OnCurrentProjectChanged()
        {
            CleanVolatileParts();
            ResetApplyBtns();
        }
        //-------------------------------------------------------
        void IMyForm.OnCurrentRigWellChanged()
        {
            CleanVolatileParts();
            ResetApplyBtns();
        }
        //-------------------------------------------------------
        void IMyForm.OnCurrentReportChanged()
        {
            CleanVolatileParts();
            //~~~~~~~~~~~~~~~~~~~~
            if (frmMain.selectedRepID != -1)
            {
                LoadHydraulic();
            }

            ResetApplyBtns();
        }
        //-------------------------------------------------------
        void IMyForm.OnInit()
        {
            //~~~~~~~~~~~~~~~~~ AT_None = Hide ~~~~~~~~~~~~~~~~~~~~
            if (access.hydraulic == RoleAccess.AcessTypes.AT_None)
            {
                pnlTotal.Visible = false;
            }
            else if (access.hydraulic == RoleAccess.AcessTypes.AT_ReadOnly)
            {
                grbInformation.Enabled = false;
                grbBitHyd.Enabled = false;
                grbHydAna.Enabled = false;
                btnApply.Enabled = false;
            }
            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            CleanVolatileParts();
            ResetApplyBtns();
        }
        //-------------------------------------------------------
        void IMyForm.OnCurrentUnitsChanged()
        {
            //~~~~~~~~~~~~~~
            ProjectForm prjForm = (ProjectForm)mainForm.subForms[mainForm.PorjectFormID];
            string selUnitPressure = prjForm.lblUnitSelectedPressure.Tag.ToString();
            string selUnitHoleBitSize = prjForm.lblUnitSelectedHoleBitSize.Tag.ToString();
            string selUnitWeightOnBit = prjForm.lblUnitSelectedWeightOnBit.Tag.ToString();
            string selUnitNozzleSize = prjForm.lblUnitSelectedNozzleSize.Tag.ToString();
            string selUnitNozzleVelocity = prjForm.lblUnitSelectedNozzleVelocity.Tag.ToString();
            //~~~~~~~~~~~~~~
            UnitString.WriteUnit(label17, selUnitPressure);
            UnitString.WriteUnit(label16, selUnitHoleBitSize);
            UnitString.WriteUnit(label9, selUnitWeightOnBit);
            UnitString.WriteUnit(label12, selUnitNozzleVelocity);
            UnitString.WriteUnit(label10, selUnitPressure);

            this.selUnitNozzleVelocity = selUnitNozzleVelocity;

            UpdateComputationsByPressRefereshButtons();
        }
        //-------------------------------------------------------
        void UpdateComputationsByPressRefereshButtons()
        {
            btnRefresh_Click(null, null);
        }
        //-------------------------------------------------------
        void CleanVolatileParts()
        {
            txt_Info_Spp.Text = "";
            txt_Info_BitSize.Text = "";
            txt_Info_Wob.Text = "";
            txt_Info_BHANum.Text = "";
            txt_Info_BitRpm.Text = "";
            txt_Info_BitNum.Text = "";
            txt_Info_BitType.Text = "";
            txt_Info_HoursOnBit.Text = "";
            txt_Info_Nozzles.Text = "";

            txt_MudPropCol_R3.Text = "";
            txt_MudPropCol_R300.Text = "";
            txt_MudPropCol_R600.Text = "";
            txt_MudPropCol_MW.Text = "";
            btnChooseMudPropCol.Tag = null;

            txt_BitHyd_TFA.Text = "";
            txt_BitHyd_NozVel.Text = "";
            txt_BitHyd_BitPressLoss_su.Text = "";
            txt_BitHyd_BitPressLoss_percent.Text = "";
            txt_BitHyd_HHP.Text = "";
            txt_BitHyd_HSI.Text = "";
            txt_BitHyd_JIFArea.Text = "";

            txt_HydAna_NpNa.Text = "";
            txt_HydAna_KpKa.Text = "";
            txt_HydAna_EcdBitDepth.Text = "";
            txt_HydAna_EcdCasingShoe.Text = "";
        }
        //-------------------------------------------------------
        void ResetApplyBtns()
        {
            btnApply.ImageKey = "check";
            btnApply.BackColor = frmMain.checkColor;
        }
        //-------------------------------------------------------
        public HydraulicForm(RoleAccess access, frmMain mainForm)
        {
            this.access = access;
            this.mainForm = mainForm;

            InitializeComponent();
        }
        //-------------------------------------------------------
        private void HydraulicForm_Load(object sender, EventArgs e)
        {
#if !NOLOCK
            //Security Integer (Fetch Integer memory value from HLock)
            object obj1 = new object();
            uint RProg = (uint)Program.Rand.Next(2147483647);
            obj1 = Program.arm.GetARMData(ARM.IWhichData.INT_VAL1, RProg);
            string strInt1 = Program.cls.DecodData(obj1, ARM.IWhichData.INT_VAL1, RProg);

            if (strInt1.Trim() != "5567")
            {
                MessageBox.Show("Connecting to Security Key Lock failed. Please plug security lock.", "PDF DMR System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.LockID = string.Empty;
                this.Close();
            }
#endif
        }
        //-------------------------------------------------------  
        void LoadMudPropColInfo(Int64 mudPropcolID)
        {
            //~~~~~~~~~~~~~~
            ProjectForm prjForm = (ProjectForm)mainForm.subForms[mainForm.PorjectFormID];
            string selUnitMW = prjForm.lblUnitSelectedMudWeight.Tag.ToString();
            //~~~~~~~~~~~~~~

            btnChooseMudPropCol.Tag = mudPropcolID;

            Dictionary<string, decimal> mudProp = FetchTableData.GetMudPropertiesOfOneMudPropColumn(mudPropcolID);

            decimal R3 = 0, R300 = 0, R600 = 0, MW_PPG = 0;
            //~~~~~~~~~~~~~
            if (
                mudProp != null && mudProp.Count != 0 &&
                mudProp.TryGetValue("R600", out R600) && mudProp.TryGetValue("R300", out R300) &&
                mudProp.TryGetValue("R3", out R3) && mudProp.TryGetValue("Density", out MW_PPG))
            {
                if (MW_PPG != MudPropForm.INVALID_VALUE)
                    AdvancedConvertor.ToDecimal(UnitConverter.Convert("Mud Weight", selUnitMW, "ppg", MW_PPG.ToString()), ref MW_PPG);
                
                txt_MudPropCol_R3.Text = (R3 == MudPropForm.INVALID_VALUE) ? "" : R3.ToString();
                txt_MudPropCol_R300.Text = (R300 == MudPropForm.INVALID_VALUE) ? "" : R300.ToString();
                txt_MudPropCol_R600.Text = (R600 == MudPropForm.INVALID_VALUE) ? "" : R600.ToString();
                txt_MudPropCol_MW.Text = (MW_PPG == MudPropForm.INVALID_VALUE) ? "" : MW_PPG.ToString("0.000");
            }
            else
            {
                txt_MudPropCol_R3.Text = "Error";
                txt_MudPropCol_R300.Text = "Error";
                txt_MudPropCol_R600.Text = "Error";
                txt_MudPropCol_MW.Text = "Error";
                btnChooseMudPropCol.Tag = null;
            }

        }
        //-------------------------------------------------------  
        bool LoadHydraulic()
        {
            string query =
            " select SPP_Hydraulic, BitSize_Hydraulic, BitType_Hydraulic, HoursOnBit_Hydraulic,   "
            + " 		BitNumber_Hydraulic, BHANumber_Hydraulic, BitRPM_Hydraulic,  "
            + " 		WOB_Hydraulic, MudPropColumnID_Hydraulic  "
            + " from at_Report  "
            + " where ID =  " + frmMain.selectedRepID.ToString();

            DataSet ds = new DataSet();
            skipComputations = true;

            if (ConnectionManager.ExecQuery(query, ref ds, 1))
            {
                txt_Info_Spp.Text = ds.Tables[0].Rows[0][0].ToString();
                txt_Info_BitSize.Text = ds.Tables[0].Rows[0][1].ToString();
                txt_Info_BitType.Text = ds.Tables[0].Rows[0][2].ToString();

                txt_Info_HoursOnBit.Text = ds.Tables[0].Rows[0][3].ToString();
                txt_Info_BitNum.Text = ds.Tables[0].Rows[0][4].ToString();
                txt_Info_BHANum.Text = ds.Tables[0].Rows[0][5].ToString();

                txt_Info_BitRpm.Text = ds.Tables[0].Rows[0][6].ToString();
                txt_Info_Wob.Text = ds.Tables[0].Rows[0][7].ToString();

                if (ds.Tables[0].Rows[0][8] != DBNull.Value)
                {
                    Int64 mudPropcolID = Convert.ToInt64(ds.Tables[0].Rows[0][8]);
                    LoadMudPropColInfo(mudPropcolID);
                }
                else
                {
                    txt_MudPropCol_R3.Text = "";
                    txt_MudPropCol_R300.Text = "";
                    txt_MudPropCol_R600.Text = "";
                    txt_MudPropCol_MW.Text = "";
                    btnChooseMudPropCol.Tag = null;
                }

                ds.Dispose();

                {//Nozzles
                    query = " select dbo.fn_Get_Nozzles_ByReportId (" + frmMain.selectedRepID.ToString() + ")";
                    ds = new DataSet();

                    if (ConnectionManager.ExecQuery(query, ref ds, 1))
                    {
                        txt_Info_Nozzles.Text = ds.Tables[0].Rows[0][0].ToString();
                        ds.Dispose();
                    }
                }

                skipComputations = false;
                ComputeValues();
                return true;
            }

            skipComputations = false;
            ComputeValues();
            return false;
        }
        //-------------------------------------------------------
        void ComputeValues()
        {
            try
            {
                if (btnChooseMudPropCol.Tag == null)
                    return;

                if (frmMain.selectedRepID == -1)
                    return;

                if (access.hydraulic == RoleAccess.AcessTypes.AT_None)
                    return;
                //~~~~~~~~~~~~~~
                Helpers.Computation.Hydraulic hydComp = new Helpers.Computation.Hydraulic();
                //~~~~~~~~~~~~~~
                ProjectForm prjForm = (ProjectForm)mainForm.subForms[mainForm.PorjectFormID];
                hydComp.in_selUnitPressure = prjForm.lblUnitSelectedPressure.Tag.ToString();
                hydComp.in_selUnitHoleBitSize = prjForm.lblUnitSelectedHoleBitSize.Tag.ToString();
                hydComp.in_selUnitWeightOnBit = prjForm.lblUnitSelectedWeightOnBit.Tag.ToString();
                hydComp.in_selUnitNozzleSize = prjForm.lblUnitSelectedNozzleSize.Tag.ToString();
                hydComp.in_selUnitNozzleVelocity = prjForm.lblUnitSelectedNozzleVelocity.Tag.ToString();

                hydComp.in_selUnitFlowRate = prjForm.lblUnitSelectedFlowRate.Tag.ToString();
                hydComp.in_selUnitVolPerStroke = prjForm.lblUnitSelectedVolumeStroke.Tag.ToString();
                hydComp.in_selUnitLinerLenAndDia = prjForm.lblUnitSelectedLinerLengthAndDiameter.Tag.ToString();
                hydComp.in_selUnitMW = prjForm.lblUnitSelectedMudWeight.Tag.ToString();
                hydComp.in_selUnitDepth = prjForm.lblUnitSelectedDepth.Tag.ToString();
                //~~~~~~~~~~~~~~


                txt_BitHyd_TFA.Text = "";
                txt_BitHyd_NozVel.Text = "";
                txt_BitHyd_BitPressLoss_su.Text = "";
                txt_BitHyd_BitPressLoss_percent.Text = "";
                txt_BitHyd_HHP.Text = "";
                txt_BitHyd_HSI.Text = "";
                txt_BitHyd_JIFArea.Text = "";

                txt_HydAna_NpNa.Text = "";
                txt_HydAna_KpKa.Text = "";
                txt_HydAna_EcdBitDepth.Text = "";
                txt_HydAna_EcdCasingShoe.Text = "";
                //~~~~~~~~~~~~~~
                //do not return on error => show any computed value yet
                hydComp.Compute(frmMain.selectedRepID, frmMain.selected_RW_WellID);
                ////~~~~~~~~~~~~~~
                SetComputedValueAndCheck(txt_BitHyd_TFA, Convert.ToDecimal(hydComp.out_bh_TFA));
                SetComputedValueAndCheck(txt_BitHyd_NozVel, Convert.ToDecimal(hydComp.out_bh_NozVel));
                SetComputedValueAndCheck(txt_BitHyd_BitPressLoss_su, Convert.ToDecimal(hydComp.out_bh_BitPressLossSu));
                SetComputedValueAndCheck(txt_BitHyd_BitPressLoss_percent, Convert.ToDecimal(hydComp.out_bh_BitPressLossPercent));
                SetComputedValueAndCheck(txt_BitHyd_HHP, Convert.ToDecimal(hydComp.out_bh_HHP));
                SetComputedValueAndCheck(txt_BitHyd_HSI, Convert.ToDecimal(hydComp.out_bh_HSI));
                SetComputedValueAndCheck(txt_BitHyd_JIFArea, Convert.ToDecimal(hydComp.out_bh_JIF));
                SetComputedValueAndCheck(txt_HydAna_NpNa, Convert.ToDecimal(hydComp.out_ha_Np), Convert.ToDecimal(hydComp.out_ha_Na));
                SetComputedValueAndCheck(txt_HydAna_KpKa, Convert.ToDecimal(hydComp.out_ha_Kp), Convert.ToDecimal(hydComp.out_ha_Ka));
                SetComputedValueAndCheck(txt_HydAna_EcdBitDepth, Convert.ToDecimal(hydComp.out_ha_ECDBitDepth));
                SetComputedValueAndCheck(txt_HydAna_EcdCasingShoe, Convert.ToDecimal(hydComp.out_ha_ECDCasingShoe));
                //~~~~~~~~~~~~~~
            }
            catch (Exception)
            {
            }
        }
        //-------------------------------------------------------
        void SetComputedValueAndCheck(TextBox t, decimal v)
        {
            if (v < 0) t.BackColor = Color.Red; else t.BackColor = SystemColors.Control;
            t.Text = v.ToString("0.###");
        }
        //-------------------------------------------------------
        void SetComputedValueAndCheck(TextBox t, decimal v1, decimal v2)
        {
            if (v1 < 0) t.BackColor = Color.Red; else t.BackColor = SystemColors.Control;
            if (v2 < 0) t.BackColor = Color.Red; else t.BackColor = SystemColors.Control;

            t.Text = v1.ToString("0.###") + "/" + v2.ToString("0.###");
        }
        //-------------------------------------------------------
        private void txt_TextChanged(object sender, EventArgs e)
        {
            if (skipComputations)
                return;

            if (frmMain.selectedRepID == -1)
                return;

            if (access.hydraulic != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;

            //ComputeValues();

            btnApply.ImageKey = "warning";
            btnApply.BackColor = frmMain.warningColor;
        }
        //-------------------------------------------------------
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (frmMain.selectedRepID == -1)
                return;

            if (access.hydraulic == RoleAccess.AcessTypes.AT_None)
                return;

            string METHOD = "btnRefresh_Click : " + CLASS_NAME;

            if (!LoadHydraulic())
            {
                InformationManager.Set_Info(new Errors("Can not load data from data base", "", METHOD));
                //?????logout????
            }

            btnApply.ImageKey = "check";
            btnApply.BackColor = frmMain.checkColor;
        }
        //-------------------------------------------------------
        private void btnApply_Click(object sender, EventArgs e)
        {
            if (frmMain.selectedRepID == -1)
                return;

            if (access.hydraulic != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;

            List<Errors> errs = new List<Errors>();

            List<ProcedureParam> prms = new List<ProcedureParam>();

            prms.Add(new ProcedureParam("@ReportID", frmMain.selectedRepID.ToString(), ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Report"));
            prms.Add(new ProcedureParam("@SPP_Hydraulic", txt_Info_Spp.Text, ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid SPP"));
            prms.Add(new ProcedureParam("@BitSize_Hydraulic", txt_Info_BitSize.Text, ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Bit Size"));
            prms.Add(new ProcedureParam("@BitType_Hydraulic", txt_Info_BitType.Text, 30));
            prms.Add(new ProcedureParam("@HoursOnBit_Hydraulic", txt_Info_HoursOnBit.Text, ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Hours On Bit"));
            prms.Add(new ProcedureParam("@BitNumber_Hydraulic", txt_Info_BitNum.Text, ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Bit Number"));
            prms.Add(new ProcedureParam("@BHANumber_Hydraulic", txt_Info_BHANum.Text, ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid BHA Number"));
            prms.Add(new ProcedureParam("@BitRPM_Hydraulic", txt_Info_BitRpm.Text, ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Bit RPM"));
            prms.Add(new ProcedureParam("@WOB_Hydraulic", txt_Info_Wob.Text, ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid WOB"));

            if (btnChooseMudPropCol.Tag == null)
                prms.Add(new ProcedureParam("@MudPropColumnID_Hydraulic", ProcedureParam.ParamType.PT_BigInt));
            else
                prms.Add(new ProcedureParam("@MudPropColumnID_Hydraulic", btnChooseMudPropCol.Tag.ToString(), ProcedureParam.ParamType.PT_BigInt, true, "1", false, "", "Invalid Mud Prop. Column"));

            string simpErr;
            Errors critErr;
            Int64 resultStat;

            int ret = Procedures.CallProcedure("prc_at_Report_update_Hydraulic", prms, out simpErr, out critErr, out resultStat);

            if (ret != 0)
            {
                if (ret == 1)
                    InformationManager.Set_Info(simpErr);
                else
                    InformationManager.Set_Info(critErr);

                return;
            }

            ComputeValues();

            btnApply.ImageKey = "Check";
            btnApply.BackColor = frmMain.checkColor;
        }
        //-------------------------------------------------------
        private void btnChooseNozzles_Click(object sender, EventArgs e)
        {
            if (frmMain.selectedRepID == -1)
                return;

            if (access.hydraulic != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;

            //~~~~~~~~~~~~~~
            ProjectForm prjForm = (ProjectForm)mainForm.subForms[mainForm.PorjectFormID];
            string selUnitNozzleSize = prjForm.lblUnitSelectedNozzleSize.Tag.ToString();
            //~~~~~~~~~~~~~~

            HydraulicNozzlesForm hnf = new HydraulicNozzlesForm(selUnitNozzleSize);
            hnf.ShowDialog();

            //Nozzles
            string query = " select dbo.fn_Get_Nozzles_ByReportId (" + frmMain.selectedRepID.ToString() + ")";
            DataSet ds = new DataSet();

            if (ConnectionManager.ExecQuery(query, ref ds, 1))
            {
                txt_Info_Nozzles.Text = ds.Tables[0].Rows[0][0].ToString();
                ds.Dispose();
            }

        }
        //-------------------------------------------------------
        private void btnChooseMudPropCol_Click(object sender, EventArgs e)
        {
            if (frmMain.selectedRepID == -1)
                return;

            if (access.hydraulic != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;

            MudPropCol_Selection mpc = new MudPropCol_Selection();
            if (mpc.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;

            try
            {
                if (mpc.dgvMudPropCol.SelectedRows.Count == 0)
                    return;

                Int64 mudPropcolID = Convert.ToInt64(mpc.dgvMudPropCol.Rows[mpc.dgvMudPropCol.SelectedRows[0].Index].Cells[0].Value);

                LoadMudPropColInfo(mudPropcolID);
            }
            catch (Exception)
            {
            }

            btnApply.ImageKey = "warning";
            btnApply.BackColor = frmMain.warningColor;
        }
        //-------------------------------------------------------

    }
}
