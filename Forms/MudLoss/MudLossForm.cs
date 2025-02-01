using DMR.User_DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DMR
{
    public partial class MudLossForm : Form, IMyForm
    {
        const string CLASS_NAME = "MudLossForm";
        RoleAccess access = null;
        frmMain mainForm = null;

        bool skipCellValueChanged_dgvFL = false;
        bool skipCellValueChanged_dgvLR = false;
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
            string METHOD = "OnCurrentReportChanged : " + CLASS_NAME;

            if (frmMain.selectedRepID != -1)
            {
                if (!LoadFormationLoss())
                {
                    InformationManager.Set_Info(new Errors("Can not load data from data base", "", METHOD));
                    //?????logout????
                    return;
                }

                if (!LoadLossesRecord())
                {
                    InformationManager.Set_Info(new Errors("Can not load data from data base", "", METHOD));
                    //?????logout????
                    return;
                };
            }

            ResetApplyBtns();
        }
        //-------------------------------------------------------
        void IMyForm.OnInit()
        {
            //~~~~~~~~~~~~~~~~~ AT_None = Hide ~~~~~~~~~~~~~~~~~~~~
            if (access.mudLosses_FormationLosses == RoleAccess.AcessTypes.AT_None)
            {
                tcMudLoss.TabPages.Remove(tpFormationLoss);
            }
            else if (access.mudLosses_FormationLosses == RoleAccess.AcessTypes.AT_ReadOnly)
            {
                dgvFormationLoss.ReadOnly = true;
                btnFLAdd.Enabled = false;
                btnFLApply.Enabled = false;
                btnFLRemove.Enabled = false;
            }

            DGV_Operations.ColorizeNestedColumnHeader(dgvFormationLoss, 3, 5, 14);
            DGV_Operations.HandleKeyDown(dgvFormationLoss, dgvFormationLoss_CellMouseClick);

            if (access.mudLosses_LossesRecord == RoleAccess.AcessTypes.AT_None)
            {
                tcMudLoss.TabPages.Remove(tpLossesRecord);
            }
            else if (access.mudLosses_LossesRecord == RoleAccess.AcessTypes.AT_ReadOnly)
            {
                dgvLossesRecord.ReadOnly = true;
                btnLRAdd.Enabled = false;
                btnLRApply.Enabled = false;
                btnLRRemove.Enabled = false;
            }

            DGV_Operations.ColorizeNestedColumnHeader(dgvLossesRecord, 2, 5);
            DGV_Operations.HandleKeyDown(dgvLossesRecord, dgvLossesRecord_CellMouseClick);

            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

            CleanVolatileParts();
            ResetApplyBtns();
            //~~~~~~~~~~~~~~~~~~~~
        }
        //-------------------------------------------------------
        void IMyForm.OnCurrentUnitsChanged()
        {
            //~~~~~~~~~~~~~~
            ProjectForm prjForm = (ProjectForm)mainForm.subForms[mainForm.PorjectFormID];
            string selUnitDepth = prjForm.lblUnitSelectedDepth.Tag.ToString();
            string selUnitVol = prjForm.lblUnitSelectedLiquidVolume.Tag.ToString();
            string selUnitMW = prjForm.lblUnitSelectedMudWeight.Tag.ToString();
            string selUnitDischarge = prjForm.lblUnitSelectedDischargeLossRate.Tag.ToString();
            string selUnitFlowRate = prjForm.lblUnitSelectedFlowRate.Tag.ToString();
            string selUnitPressure = prjForm.lblUnitSelectedPressure.Tag.ToString();
            //~~~~~~~~~~~~~~

            //Formation Loss ~~~~~~~~~~~~~~
            UnitString.WriteUnit(dgvFormationLoss.Columns[4], selUnitMW);
            UnitString.WriteUnit(dgvFormationLoss.Columns[7], selUnitDepth);
            UnitString.WriteUnit(dgvFormationLoss.Columns[8], selUnitVol);
            UnitString.WriteUnit(dgvFormationLoss.Columns[9], selUnitDischarge);
            UnitString.WriteUnit(dgvFormationLoss.Columns[10], selUnitDischarge);
            UnitString.WriteUnit(dgvFormationLoss.Columns[11], selUnitPressure);
            UnitString.WriteUnit(dgvFormationLoss.Columns[12], selUnitFlowRate);

            //Losses Record ~~~~~~~~~~~~~~
            UnitString.WriteUnit(dgvLossesRecord.Columns[6], selUnitVol);

            UpdateComputationsByPressRefereshButtons();
        }
        //-------------------------------------------------------
        void UpdateComputationsByPressRefereshButtons()
        {
            //btnFLRefresh_Click(null, null);
            //btnLRRefresh_Click(null, null);
        }
        //-------------------------------------------------------
        public MudLossForm(RoleAccess access, frmMain mainForm)
        {
            this.access = access;
            this.mainForm = mainForm;

            InitializeComponent();
        }
        //-------------------------------------------------------
        private void MudLossForm_Load(object sender, EventArgs e)
        {
#if !NOLOCK
            //Security Query (Request to HLock and Analyse Answer)
            AlxSSB.AlxSSBClass myClass = new AlxSSB.AlxSSBClass();
            int RndValue = Program.Rand.Next(1000);
            string Answer = "";
            object obj1 = new object();
            Program.obj = Program.cls.EncodeQueryRequest(myClass.ArrRequest[RndValue], Program.KeyAES);
            obj1 = Program.arm.GetARMQuery(Program.obj);
            Answer = Program.cls.DecodeQueryResponse(obj1, Program.KeyAES);

            if (Answer.Trim() != myClass.ArrResponse[RndValue].Trim())
            {
                MessageBox.Show("Connecting to Security Key Lock failed. Please plug security lock.", "PDF DMR System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.LockID = string.Empty;
                this.Close();
            }
#endif
        }
        //-------------------------------------------------------
        void CleanVolatileParts()
        {
            dgvFormationLoss.Rows.Clear();
            dgvLossesRecord.Rows.Clear();
        }
        //-------------------------------------------------------
        void ResetApplyBtns()
        {
            btnFLApply.ImageKey = "check";
            btnFLApply.BackColor = frmMain.checkColor;
            btnLRApply.ImageKey = "check";
            btnLRApply.BackColor = frmMain.checkColor;
        }
        //-------------------------------------------------------
        //returns:   true (success) , false(failure)
        bool LoadFormationLoss()
        {
            //string METHOD = "LoadFormationLoss : " + CLASS_NAME;

            skipCellValueChanged_dgvFL = true;

            dgvFormationLoss.Rows.Clear();

            try
            {
                string query =
                     " select ID, TimeInterval, Dfs_AutoID, d.DrillingFluidSystem, "
                    + " MW, i.DailyActivity, i.AutoID, Depth, Vol, LossRateMax, "
                    + " LossRateMin, SPP, FlowRate, Porosity, \'\', RemedialAction "
                    + " from rt_Rep2MudLossFormation left join lt_DrillingFluidSystem d on Dfs_AutoID = d.AutoID"
                    + " left join lt_IADC_Hours i on IADC_AutoID = i.AutoID"
                    + " where ReportID = " + frmMain.selectedRepID.ToString();

                DataSet ds = ConnectionManager.ExecQuery(query);

                if (ds != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        dgvFormationLoss.Rows.Add(ds.Tables[0].Rows[i].ItemArray);
                        dgvFormationLoss.Rows[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dgvFormationLoss.Rows[i].HeaderCell.Value = (i + 1).ToString();

                        dgvFormationLoss.Rows[i].Cells[14].Value = CombineAdditives(Convert.ToInt64(ds.Tables[0].Rows[i][0]));
                        dgvFormationLoss.Rows[i].Height = 50;
                    }

                    ds.Dispose();
                    skipCellValueChanged_dgvFL = false;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                //ErrorManager.Set_Error(new Errors("Can not load data from data base", ex.Message, METHOD));
                //will be handled outside
            }

            skipCellValueChanged_dgvFL = false;
            return false;
        }
        //-------------------------------------------------------
        //returns:   true (success) , false(failure)
        bool LoadLossesRecord()
        {
            //string METHOD = "LoadLossesRecord : " + CLASS_NAME;

            skipCellValueChanged_dgvLR = true;

            dgvLossesRecord.Rows.Clear();

            try
            {
                string query =
                  " select rr.ID, rr.TimeInterval, p.Value, rr.Losses_PredefAutoID, d.AutoID, d.DrillingFluidSystem, rr.Vol, rr.RemedialAction "
                + " from rt_Rep2MudLossRecord rr left join lt_PredefValues p on rr.Losses_PredefAutoID = p.AutoID "
                + " left join lt_DrillingFluidSystem d on Dfs_AutoID = d.AutoID  "
                + " where ReportID = " + frmMain.selectedRepID.ToString();

                DataSet ds = ConnectionManager.ExecQuery(query);

                if (ds != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        dgvLossesRecord.Rows.Add(ds.Tables[0].Rows[i].ItemArray);
                        dgvLossesRecord.Rows[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dgvLossesRecord.Rows[i].HeaderCell.Value = (i + 1).ToString();
                    }

                    ds.Dispose();
                    skipCellValueChanged_dgvLR = false;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                //ErrorManager.Set_Error(new Errors("Can not load data from data base", ex.Message, METHOD));
                //will be handled outside
            }

            skipCellValueChanged_dgvLR = false;
            return false;
        }
        //-------------------------------------------------------
        private void btnFLAdd_Click(object sender, EventArgs e)
        {
            if (frmMain.selectedRepID == -1)
                return;

            if (access.mudLosses_FormationLosses != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;

            {
                List<ProcedureParam> prms = new List<ProcedureParam>();
                prms.Add(new ProcedureParam("@ReportID", frmMain.selectedRepID.ToString(), ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Report"));
                prms.Add(new ProcedureParam("@ProjAutoID", frmMain.selectedPrjID.ToString(), ProcedureParam.ParamType.PT_Int, true, "1", false, "", "Invalid Project"));

                string simpErr;
                Errors critErr;
                Int64 resultStat;

                int ret = Procedures.CallProcedure("prc_rt_Rep2MudLossFormation_insert", prms, out simpErr, out critErr, out resultStat);

                if (ret != 0)
                {
                    if (ret == 1)
                        InformationManager.Set_Info(simpErr);
                    else
                        InformationManager.Set_Info(critErr);

                    return;
                }
            }

            if (!LoadFormationLoss())
            {
                //ErrorManager.Set_Error(new Errors("Can not load data from data base", "", METHOD));
                ////?????logout????
            }

            btnFLApply.ImageKey = "Check";
            btnFLApply.BackColor = frmMain.checkColor;
        }
        //-------------------------------------------------------
        private void btnFLApply_Click(object sender, EventArgs e)
        {
            if (frmMain.selectedRepID == -1)
                return;

            if (access.mudLosses_FormationLosses != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;

            List<Errors> errs = new List<Errors>();

            for (int i = 0; i < dgvFormationLoss.Rows.Count; i++)
            {
                //DA
                {
                    string idStr = DGV_Operations.CellValueAsString(dgvFormationLoss.Rows[i].Cells[0]);

                    List<ProcedureParam> prms = new List<ProcedureParam>();
                    prms.Add(new ProcedureParam("@ID", idStr, ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Record"));

                    string daStr = DGV_Operations.CellValueAsString(dgvFormationLoss.Rows[i].Cells[6]);

                    if (daStr == "")
                        prms.Add(new ProcedureParam("@IADC_AutoID", ProcedureParam.ParamType.PT_String));
                    else
                        prms.Add(new ProcedureParam("@IADC_AutoID", daStr, ProcedureParam.ParamType.PT_Int, true, "1", false, "", "Invalid Daily Activity"));

                    string simpErr;
                    Errors critErr;
                    Int64 resultStat;

                    int ret = Procedures.CallProcedure("prc_rt_Rep2MudLossFormation_Update_DA", prms, out simpErr, out critErr, out resultStat);

                    if (ret != 0)
                    {
                        if (ret == 1)
                        {
                            critErr = new Errors(simpErr);
                        }

                        errs.Add(critErr);
                    }
                }

                //DFS
                {
                    string idStr = DGV_Operations.CellValueAsString(dgvFormationLoss.Rows[i].Cells[0]);

                    List<ProcedureParam> prms = new List<ProcedureParam>();
                    prms.Add(new ProcedureParam("@ID", idStr, ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Record"));

                    string dfsStr = DGV_Operations.CellValueAsString(dgvFormationLoss.Rows[i].Cells[3]);
                    string dfsIDStr = DGV_Operations.CellValueAsString(dgvFormationLoss.Rows[i].Cells[2]);

                    if (dfsStr == "")
                        prms.Add(new ProcedureParam("@Dfs_AutoID", ProcedureParam.ParamType.PT_Int));
                    else
                        prms.Add(new ProcedureParam("@Dfs_AutoID", dfsIDStr, ProcedureParam.ParamType.PT_Int, true, "1", false, "", "Invalid DFS"));

                    string simpErr;
                    Errors critErr;
                    Int64 resultStat;

                    int ret = Procedures.CallProcedure("prc_rt_Rep2MudLossFormation_Update_DFS", prms, out simpErr, out critErr, out resultStat);

                    if (ret != 0)
                    {
                        if (ret == 1)
                        {
                            critErr = new Errors(simpErr);
                        }

                        errs.Add(critErr);
                    }
                }

                //Others
                {
                    string idStr = DGV_Operations.CellValueAsString(dgvFormationLoss.Rows[i].Cells[0]);

                    List<ProcedureParam> prms = new List<ProcedureParam>();
                    prms.Add(new ProcedureParam("@ID", idStr, ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Record"));
                    prms.Add(new ProcedureParam("@TimeInterval", DGV_Operations.CellValueAsString(dgvFormationLoss.Rows[i].Cells[1]), 10));
                    prms.Add(new ProcedureParam("@MW", DGV_Operations.CellValueAsString(dgvFormationLoss.Rows[i].Cells[4]), ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid MW"));
                    prms.Add(new ProcedureParam("@Depth", DGV_Operations.CellValueAsString(dgvFormationLoss.Rows[i].Cells[7]), ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Depth"));
                    prms.Add(new ProcedureParam("@Vol", DGV_Operations.CellValueAsString(dgvFormationLoss.Rows[i].Cells[8]), ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Vol"));
                    prms.Add(new ProcedureParam("@LossRateMax", DGV_Operations.CellValueAsString(dgvFormationLoss.Rows[i].Cells[9]), ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Loss Rate Max"));
                    prms.Add(new ProcedureParam("@LossRateMin", DGV_Operations.CellValueAsString(dgvFormationLoss.Rows[i].Cells[10]), ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Loss Rate Min"));
                    prms.Add(new ProcedureParam("@SPP", DGV_Operations.CellValueAsString(dgvFormationLoss.Rows[i].Cells[11]), ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid SPP"));
                    prms.Add(new ProcedureParam("@FlowRate", DGV_Operations.CellValueAsString(dgvFormationLoss.Rows[i].Cells[12]), ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Flow Rate"));
                    prms.Add(new ProcedureParam("@Porosity", DGV_Operations.CellValueAsString(dgvFormationLoss.Rows[i].Cells[13]), ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Porosity"));
                    prms.Add(new ProcedureParam("@RemedialAction", DGV_Operations.CellValueAsString(dgvFormationLoss.Rows[i].Cells[15]), 100));

                    string simpErr;
                    Errors critErr;
                    Int64 resultStat;

                    int ret = Procedures.CallProcedure("prc_rt_Rep2MudLossFormation_Update_Others", prms, out simpErr, out critErr, out resultStat);

                    Color normalBkColor = (i % 2 == 0) ? dgvFormationLoss.BackgroundColor : dgvFormationLoss.AlternatingRowsDefaultCellStyle.BackColor;
                    dgvFormationLoss.Rows[i].Cells[4].Style.BackColor = normalBkColor;
                    dgvFormationLoss.Rows[i].Cells[7].Style.BackColor = normalBkColor;
                    dgvFormationLoss.Rows[i].Cells[8].Style.BackColor = normalBkColor;
                    dgvFormationLoss.Rows[i].Cells[9].Style.BackColor = normalBkColor;
                    dgvFormationLoss.Rows[i].Cells[10].Style.BackColor = normalBkColor;
                    dgvFormationLoss.Rows[i].Cells[11].Style.BackColor = normalBkColor;
                    dgvFormationLoss.Rows[i].Cells[12].Style.BackColor = normalBkColor;
                    dgvFormationLoss.Rows[i].Cells[13].Style.BackColor = normalBkColor;

                    if (ret != 0)
                    {
                        if (ret == 1)
                        {
                            if (simpErr.StartsWith("Invalid MW"))
                                dgvFormationLoss.Rows[i].Cells[4].Style.BackColor = Color.Red;
                            else if (simpErr.StartsWith("Invalid Depth"))
                                dgvFormationLoss.Rows[i].Cells[7].Style.BackColor = Color.Red;
                            else if (simpErr.StartsWith("Invalid Vol"))
                                dgvFormationLoss.Rows[i].Cells[8].Style.BackColor = Color.Red;
                            else if (simpErr.StartsWith("Invalid Loss Rate Max"))
                                dgvFormationLoss.Rows[i].Cells[9].Style.BackColor = Color.Red;
                            else if (simpErr.StartsWith("Invalid Loss Rate Min"))
                                dgvFormationLoss.Rows[i].Cells[10].Style.BackColor = Color.Red;
                            else if (simpErr.StartsWith("Invalid SPP"))
                                dgvFormationLoss.Rows[i].Cells[11].Style.BackColor = Color.Red;
                            else if (simpErr.StartsWith("Invalid Flow Rate"))
                                dgvFormationLoss.Rows[i].Cells[12].Style.BackColor = Color.Red;
                            else if (simpErr.StartsWith("Invalid Porosity"))
                                dgvFormationLoss.Rows[i].Cells[13].Style.BackColor = Color.Red;

                            critErr = new Errors(simpErr);
                        }

                        errs.Add(critErr);
                    }
                }

            }

            InformationManager.Set_Info(errs.ToArray());

            if (errs.Count == 0)
            {
                btnFLApply.ImageKey = "Check";
                btnFLApply.BackColor = frmMain.checkColor;
            }
        }
        //-------------------------------------------------------
        private void btnFLRemove_Click(object sender, EventArgs e)
        {
            if (frmMain.selectedRepID == -1)
                return;

            if (access.mudLosses_FormationLosses != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;

            if (dgvFormationLoss.SelectedRows.Count == 0)
            {
                InformationManager.Set_Info("Select One Row to be Deleted");
                return;
            }

            string idStr = DGV_Operations.CellValueAsString(dgvFormationLoss.Rows[dgvFormationLoss.SelectedRows[0].Index].Cells[0]);

            List<ProcedureParam> prms = new List<ProcedureParam>();
            prms.Add(new ProcedureParam("@ID", idStr, ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Record"));

            string simpErr;
            Errors critErr;
            Int64 resultStat;

            int ret = 0;

            ret = Procedures.CallProcedure("prc_rt_Rep2MudLossFormation_delete", prms, out simpErr, out critErr, out resultStat);

            if (ret != 0)
            {
                if (ret == 1)
                    InformationManager.Set_Info(simpErr);
                else
                    InformationManager.Set_Info(critErr);

                return;
            }

            if (!LoadFormationLoss())
            {
                //ErrorManager.Set_Error(new Errors("Can not load data from data base", "", METHOD));
                ////?????logout????
            }
        }
        //-------------------------------------------------------
        private void btnFLRefresh_Click(object sender, EventArgs e)
        {
            string METHOD = "btnFLRefresh_Click : " + CLASS_NAME;

            if (frmMain.selectedRepID == -1)
                return;

            if (access.mudLosses_FormationLosses == RoleAccess.AcessTypes.AT_None)
                return;

            if (!LoadFormationLoss())
            {
                InformationManager.Set_Info(new Errors("Can not load data from data base", "", METHOD));
                //?????logout????
            }

            btnFLApply.ImageKey = "check";
            btnFLApply.BackColor = frmMain.checkColor;
        }
        //-------------------------------------------------------
        string CombineAdditives(Int64 mlossID)
        {
            //~~~~~~~~~~~~~~
            ProjectForm prjForm = (ProjectForm)mainForm.subForms[mainForm.PorjectFormID];
            string selUnitVol = prjForm.lblUnitSelectedLiquidVolume.Tag.ToString();
            //~~~~~~~~~~~~~~

            string result = "";

            try
            {
                string query =
                     " select p.Name, p.UnitSize, rp.Used, rp.PillVol "
                    + " from rt_Rep2MudLossFormation_Additive rp join lt_Product p on rp.Prd_AutoID = p.AutoID "
                    + " where rp.MudLF_ID = " + mlossID.ToString();

                DataSet ds = ConnectionManager.ExecQuery(query);

                if (ds != null)
                {
                    //copy row by row to preserve header of table
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        string Name = ds.Tables[0].Rows[i][0].ToString();
                        string UnitSize = ds.Tables[0].Rows[i][1].ToString();
                        string prdUsed = Convert.ToDecimal(ds.Tables[0].Rows[i][2]).ToString("0.###");//show fractures if exist
                        string pillVol = Convert.ToDecimal(ds.Tables[0].Rows[i][3]).ToString("0.###");

                        result += Name + "(" + UnitSize + ") : " + prdUsed + " , " + pillVol + " " + selUnitVol + "\n";
                    }

                    ds.Dispose();
                }
            }
            catch (Exception)
            {
                return "";
            }

            return result;
        }
        //-------------------------------------------------------
        private void dgvFormationLoss_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (access.mudLosses_FormationLosses != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;

            if (frmMain.selectedRepID == -1)
                return;

            if (e.ColumnIndex > 0 && e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 3)//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                {
                    string query = "select 0, AutoID, MudType,DrillingFluidSystem  from lt_DrillingFluidSystem ";
                    DrillingFluidSystemFormDB_Selection frmDFSel = new DrillingFluidSystemFormDB_Selection(query);

                    frmDFSel.dgvFluidSys.Columns[0].Visible = false;//single selection

                    System.Windows.Forms.DialogResult dgRes = frmDFSel.ShowDialog();

                    if (dgRes == System.Windows.Forms.DialogResult.Cancel)
                        return;

                    if (dgRes == System.Windows.Forms.DialogResult.Ignore)
                    {
                        dgvFormationLoss.Rows[e.RowIndex].Cells[2].Value = "";
                        dgvFormationLoss.Rows[e.RowIndex].Cells[3].Value = "";
                        return;
                    }

                    if (frmDFSel.dgvFluidSys.SelectedRows.Count == 0)
                        return;

                    string drillFS = DGV_Operations.CellValueAsString(frmDFSel.dgvFluidSys.Rows[frmDFSel.dgvFluidSys.SelectedRows[0].Index].Cells[3]);
                    string mudType = DGV_Operations.CellValueAsString(frmDFSel.dgvFluidSys.Rows[frmDFSel.dgvFluidSys.SelectedRows[0].Index].Cells[2]);
                    string dfsAutoID = DGV_Operations.CellValueAsString(frmDFSel.dgvFluidSys.Rows[frmDFSel.dgvFluidSys.SelectedRows[0].Index].Cells[1]);

                    dgvFormationLoss.Rows[e.RowIndex].Cells[2].Value = dfsAutoID;
                    dgvFormationLoss.Rows[e.RowIndex].Cells[3].Value = drillFS;
                }
                else if (e.ColumnIndex == 5)//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                {
                    string query = " select 0, AutoID, DailyActivity from lt_IADC_Hours ";

                    IADCFormDB_Selection iadcSel = new IADCFormDB_Selection(query);
                    iadcSel.dgvIADC.Columns[0].Visible = false;
                    iadcSel.selBtns.ShowSelectionButtons = false;

                    if (iadcSel.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                        return;

                    if (iadcSel.dgvIADC.SelectedRows.Count == 0)
                        return;

                    dgvFormationLoss.Rows[e.RowIndex].Cells[5].Value = DGV_Operations.CellValueAsString(iadcSel.dgvIADC.Rows[iadcSel.dgvIADC.SelectedRows[0].Index].Cells[2]);
                    dgvFormationLoss.Rows[e.RowIndex].Cells[6].Value = DGV_Operations.CellValueAsString(iadcSel.dgvIADC.Rows[iadcSel.dgvIADC.SelectedRows[0].Index].Cells[1]);
                }
                else if (e.ColumnIndex == 14)//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                {
                    //~~~~~~~~~~~~~~
                    ProjectForm prjForm = (ProjectForm)mainForm.subForms[mainForm.PorjectFormID];
                    string selUnitVol = prjForm.lblUnitSelectedLiquidVolume.Tag.ToString();
                    //~~~~~~~~~~~~~~

                    Int64 mlossID = Convert.ToInt64(dgvFormationLoss.Rows[e.RowIndex].Cells[0].Value);
                    MudLossFLAdditivesForm mfl = new MudLossFLAdditivesForm(mlossID, selUnitVol);
                    mfl.ShowDialog();

                    dgvFormationLoss.Rows[e.RowIndex].Cells[14].Value = CombineAdditives(mlossID);
                }
            }
        }
        //-------------------------------------------------------
        private void dgvFormationLoss_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex == -1)
                return;

            if (skipCellValueChanged_dgvFL)
                return;

            btnFLApply.ImageKey = "warning";
            btnFLApply.BackColor = frmMain.warningColor;
        }
        //-------------------------------------------------------
        private void btnLRAdd_Click(object sender, EventArgs e)
        {
            if (frmMain.selectedRepID == -1)
                return;

            if (access.mudLosses_LossesRecord != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;

            {
                List<ProcedureParam> prms = new List<ProcedureParam>();
                prms.Add(new ProcedureParam("@ReportID", frmMain.selectedRepID.ToString(), ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Report"));
                prms.Add(new ProcedureParam("@ProjAutoID", frmMain.selectedPrjID.ToString(), ProcedureParam.ParamType.PT_Int, true, "1", false, "", "Invalid Project"));

                string simpErr;
                Errors critErr;
                Int64 resultStat;

                int ret = Procedures.CallProcedure("prc_rt_Rep2MudLossRecord_insert", prms, out simpErr, out critErr, out resultStat);

                if (ret != 0)
                {
                    if (ret == 1)
                        InformationManager.Set_Info(simpErr);
                    else
                        InformationManager.Set_Info(critErr);

                    return;
                }
            }

            if (!LoadLossesRecord())
            {
                //ErrorManager.Set_Error(new Errors("Can not load data from data base", "", METHOD));
                ////?????logout????
            }

            btnLRApply.ImageKey = "Check";
            btnLRApply.BackColor = frmMain.checkColor;
        }
        //-------------------------------------------------------
        private void btnLRApply_Click(object sender, EventArgs e)
        {
            if (frmMain.selectedRepID == -1)
                return;

            if (access.mudLosses_LossesRecord != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;

            List<Errors> errs = new List<Errors>();

            for (int i = 0; i < dgvLossesRecord.Rows.Count; i++)
            {
                string idStr = DGV_Operations.CellValueAsString(dgvLossesRecord.Rows[i].Cells[0]);

                //DFS
                {
                    List<ProcedureParam> prms = new List<ProcedureParam>();
                    prms.Add(new ProcedureParam("@ID", idStr, ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Record"));

                    string dfsStr = DGV_Operations.CellValueAsString(dgvLossesRecord.Rows[i].Cells[5]);
                    string dfsIDStr = DGV_Operations.CellValueAsString(dgvLossesRecord.Rows[i].Cells[4]);

                    if (dfsStr == "")
                        prms.Add(new ProcedureParam("@Dfs_AutoID", ProcedureParam.ParamType.PT_Int));
                    else
                        prms.Add(new ProcedureParam("@Dfs_AutoID", dfsIDStr, ProcedureParam.ParamType.PT_Int, true, "1", false, "", "Invalid DFS"));


                    string simpErr;
                    Errors critErr;
                    Int64 resultStat;

                    int ret = Procedures.CallProcedure("prc_rt_Rep2MudLossRecord_Update_DFS", prms, out simpErr, out critErr, out resultStat);

                    if (ret != 0)
                    {
                        if (ret == 1)
                        {
                            critErr = new Errors(simpErr);
                        }

                        errs.Add(critErr);
                    }
                }

                {//Losses
                    List<ProcedureParam> prms = new List<ProcedureParam>();
                    prms.Add(new ProcedureParam("@ID", idStr, ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Record"));

                    string predefID = DGV_Operations.CellValueAsString(dgvLossesRecord.Rows[i].Cells[3]);

                    if (predefID == "")
                        prms.Add(new ProcedureParam("@Losses_PredefAutoID", ProcedureParam.ParamType.PT_Int));
                    else
                        prms.Add(new ProcedureParam("@Losses_PredefAutoID", predefID, ProcedureParam.ParamType.PT_Int, true, "1", false, "", "Invalid Losses Record"));

                    string simpErr;
                    Errors critErr;
                    Int64 resultStat;

                    int ret = Procedures.CallProcedure("prc_rt_Rep2MudLossRecord_Update_Losses", prms, out simpErr, out critErr, out resultStat);

                    if (ret != 0)
                    {
                        if (ret == 1)
                        {
                            critErr = new Errors(simpErr);
                        }

                        errs.Add(critErr);
                    }
                }

                //Others
                {
                    List<ProcedureParam> prms = new List<ProcedureParam>();
                    prms.Add(new ProcedureParam("@ID", idStr, ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Record"));
                    prms.Add(new ProcedureParam("@TimeInterval", DGV_Operations.CellValueAsString(dgvLossesRecord.Rows[i].Cells[1]), 10));
                    prms.Add(new ProcedureParam("@Vol", DGV_Operations.CellValueAsString(dgvLossesRecord.Rows[i].Cells[6]), ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Vol"));
                    prms.Add(new ProcedureParam("@RemedialAction", DGV_Operations.CellValueAsString(dgvLossesRecord.Rows[i].Cells[7]), 100));

                    string simpErr;
                    Errors critErr;
                    Int64 resultStat;

                    int ret = Procedures.CallProcedure("prc_rt_Rep2MudLossRecord_Update_Others", prms, out simpErr, out critErr, out resultStat);

                    Color normalBkColor = (i % 2 == 0) ? dgvLossesRecord.BackgroundColor : dgvLossesRecord.AlternatingRowsDefaultCellStyle.BackColor;
                    dgvLossesRecord.Rows[i].Cells[5].Style.BackColor = normalBkColor;

                    if (ret != 0)
                    {
                        if (ret == 1)
                        {
                            if (simpErr.StartsWith("Invalid Vol"))
                                dgvLossesRecord.Rows[i].Cells[5].Style.BackColor = Color.Red;

                            critErr = new Errors(simpErr);
                        }

                        errs.Add(critErr);
                    }
                }

            }

            InformationManager.Set_Info(errs.ToArray());

            if (errs.Count == 0)
            {
                btnLRApply.ImageKey = "Check";
                btnLRApply.BackColor = frmMain.checkColor;
            }
        }
        //-------------------------------------------------------
        private void btnLRRemove_Click(object sender, EventArgs e)
        {
            if (frmMain.selectedRepID == -1)
                return;

            if (access.mudLosses_LossesRecord != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;

            if (dgvLossesRecord.SelectedRows.Count == 0)
            {
                InformationManager.Set_Info("Select One Row to be Deleted");
                return;
            }

            string idStr = DGV_Operations.CellValueAsString(dgvLossesRecord.Rows[dgvLossesRecord.SelectedRows[0].Index].Cells[0]);

            List<ProcedureParam> prms = new List<ProcedureParam>();
            prms.Add(new ProcedureParam("@ID", idStr, ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Record"));

            string simpErr;
            Errors critErr;
            Int64 resultStat;

            int ret = 0;

            ret = Procedures.CallProcedure("prc_rt_Rep2MudLossRecord_delete", prms, out simpErr, out critErr, out resultStat);

            if (ret != 0)
            {
                if (ret == 1)
                    InformationManager.Set_Info(simpErr);
                else
                    InformationManager.Set_Info(critErr);

                return;
            }

            if (!LoadLossesRecord())
            {
                //ErrorManager.Set_Error(new Errors("Can not load data from data base", "", METHOD));
                ////?????logout????
            }
        }
        //-------------------------------------------------------
        private void btnLRRefresh_Click(object sender, EventArgs e)
        {
            string METHOD = "btnLRRefresh_Click : " + CLASS_NAME;

            if (frmMain.selectedRepID == -1)
                return;

            if (access.mudLosses_LossesRecord == RoleAccess.AcessTypes.AT_None)
                return;

            if (!LoadLossesRecord())
            {
                InformationManager.Set_Info(new Errors("Can not load data from data base", "", METHOD));
                //?????logout????
            }

            btnLRApply.ImageKey = "check";
            btnLRApply.BackColor = frmMain.checkColor;
        }
        //-------------------------------------------------------
        private void dgvLossesRecord_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex == -1)
                return;

            if (skipCellValueChanged_dgvLR)
                return;

            btnLRApply.ImageKey = "warning";
            btnLRApply.BackColor = frmMain.warningColor;
        }
        //-------------------------------------------------------
        private void dgvLossesRecord_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (access.mudLosses_LossesRecord != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;

            if (frmMain.selectedRepID == -1)
                return;

            if (e.ColumnIndex > 0 && e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 5)//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                {
                    string query = "select 0, AutoID, MudType,DrillingFluidSystem  from lt_DrillingFluidSystem ";
                    DrillingFluidSystemFormDB_Selection frmDFSel = new DrillingFluidSystemFormDB_Selection(query);

                    frmDFSel.dgvFluidSys.Columns[0].Visible = false;//single selection

                    System.Windows.Forms.DialogResult dgRes = frmDFSel.ShowDialog();

                    if (dgRes == System.Windows.Forms.DialogResult.Cancel)
                        return;

                    if (dgRes == System.Windows.Forms.DialogResult.Ignore)
                    {
                        dgvLossesRecord.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = DBNull.Value;
                        return;
                    }

                    if (frmDFSel.dgvFluidSys.SelectedRows.Count == 0)
                        return;

                    string drillFS = DGV_Operations.CellValueAsString(frmDFSel.dgvFluidSys.Rows[frmDFSel.dgvFluidSys.SelectedRows[0].Index].Cells[3]);
                    string mudType = DGV_Operations.CellValueAsString(frmDFSel.dgvFluidSys.Rows[frmDFSel.dgvFluidSys.SelectedRows[0].Index].Cells[2]);
                    string dfsAutoID = DGV_Operations.CellValueAsString(frmDFSel.dgvFluidSys.Rows[frmDFSel.dgvFluidSys.SelectedRows[0].Index].Cells[1]);

                    dgvLossesRecord.Rows[e.RowIndex].Cells[4].Value = dfsAutoID;
                    dgvLossesRecord.Rows[e.RowIndex].Cells[5].Value = drillFS;
                }
                else if (e.ColumnIndex == 2)//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                {
                    PredefinedValuesFormDB_Selection pdSel = new PredefinedValuesFormDB_Selection("Losses");

                    if (pdSel.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                        return;

                    if (pdSel.dgvValue.SelectedRows.Count == 0)
                        return;

                    string Val = DGV_Operations.CellValueAsString(pdSel.dgvValue.Rows[pdSel.dgvValue.SelectedRows[0].Index].Cells[1]);
                    string id = DGV_Operations.CellValueAsString(pdSel.dgvValue.Rows[pdSel.dgvValue.SelectedRows[0].Index].Cells[0]);

                    dgvLossesRecord.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = Val;
                    dgvLossesRecord.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value = id;
                }
            }
        }
        //-------------------------------------------------------
    }
}
