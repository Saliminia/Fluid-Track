using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;

using System.Windows.Forms;

namespace DMR
{
    public partial class SolidConEqForm : Form, IMyForm
    {
        const string CLASS_NAME = "SolidConEqForm/MudPump";
        RoleAccess access = null;
        frmMain mainForm = null;
        //=======================================================
        bool skipCellValueChanged_dgvSolidControlEq = false;
        bool skipCellValueChanged_dgvMudPumps = false;
        //=======================================================
        void IMyForm.OnCurrentProjectChanged()
        {
            dgvSolidEq.Rows.Clear();

            dgvMudPumps.Rows.Clear();
            btnPumpApply.ImageKey = "check";
            btnPumpApply.BackColor = frmMain.checkColor;
        }
        //-------------------------------------------------------
        void IMyForm.OnCurrentRigWellChanged()
        {
            dgvSolidEq.Rows.Clear();

            dgvMudPumps.Rows.Clear();
            btnPumpApply.ImageKey = "check";
            btnPumpApply.BackColor = frmMain.checkColor;
        }
        //-------------------------------------------------------
        void IMyForm.OnCurrentReportChanged()
        {
            string METHOD = "OnCurrentReportChanged : " + CLASS_NAME;

            if (frmMain.selectedRepID != -1)
            {
                if (!LoadSolidControlEq())
                {
                    InformationManager.Set_Info(new Errors("Can not load data from data base", "", METHOD));
                    //?????logout????
                    return;
                }

                if (!LoadMudPump())
                {
                    InformationManager.Set_Info(new Errors("Can not load data from data base", "", METHOD));
                    //?????logout????
                    return;
                }
            }

            btnPumpApply.ImageKey = "check";
            btnPumpApply.BackColor = frmMain.checkColor;
        }
        //-------------------------------------------------------
        void IMyForm.OnInit()
        {
            //~~~~~~~~~~~~~~~~~ AT_None = Hide ~~~~~~~~~~~~~~~~~~~~
            if (access.solidControlEquipment == RoleAccess.AcessTypes.AT_None)
            {
                tableLayoutPanel.Visible = false;
            }
            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            if (access.solidControlEquipment == RoleAccess.AcessTypes.AT_ReadOnly)
            {
                dgvSolidEq.ReadOnly = true;

                dgvMudPumps.ReadOnly = true;
                btnPumpApply.Enabled = false;
            }
            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            dgvSolidEq.Rows.Clear();
            dgvMudPumps.Rows.Clear();

            DGV_Operations.ColorizeFixedColumnHeader(dgvSolidEq, 1, 2, 3, 4, 5);
            DGV_Operations.ColorizeNestedColumnHeader(dgvSolidEq, 6, 7);
            DGV_Operations.HandleKeyDown(dgvSolidEq, dgvSolidEq_CellMouseClick);

            DGV_Operations.ColorizeFixedColumnHeader(dgvMudPumps, 1, 2, 3, 4, 5, 6);
            DGV_Operations.ColorizeComputedColumnHeader(dgvMudPumps, 8);
            DGV_Operations.HandleKeyDown(dgvMudPumps, null);

            btnPumpApply.ImageKey = "check";
            btnPumpApply.BackColor = frmMain.checkColor;
        }
        //-------------------------------------------------------
        void IMyForm.OnCurrentUnitsChanged()
        {
            //~~~~~~~~~~~~~~
            ProjectForm prjForm = (ProjectForm)mainForm.subForms[mainForm.PorjectFormID];
            string selUnitVolPerStroke = prjForm.lblUnitSelectedVolumeStroke.Tag.ToString();
            string selUnitFlowRate = prjForm.lblUnitSelectedFlowRate.Tag.ToString();
            //~~~~~~~~~~~~~~

            //Mud Pumps ~~~~~~~~~~~~~~
            UnitString.WriteUnit(dgvMudPumps.Columns[6], selUnitVolPerStroke);
            UnitString.WriteUnit(dgvMudPumps.Columns[8], selUnitFlowRate);

            UpdateComputationsByPressRefereshButtons();
        }
        //-------------------------------------------------------
        void UpdateComputationsByPressRefereshButtons()
        {
            //btnSolRefresh_Click(null, null);
            //btnPumpRefresh_Click(null, null);
        }
        //-------------------------------------------------------
        public SolidConEqForm(RoleAccess access, frmMain mainForm)
        {
            this.access = access;
            this.mainForm = mainForm;

            InitializeComponent();
        }
        //-------------------------------------------------------
        private void SolidConEqForm_Load(object sender, EventArgs e)
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
        string SCECombineScreenSizes(List<string> screenSizes)
        {
            string result = "";

            if (screenSizes.Count > 0)
            {
                for (int i = 0; i < screenSizes.Count - 1; i++)
                    result += screenSizes[i] + "/";

                result += screenSizes[screenSizes.Count - 1];
            }

            return result;
        }
        //-------------------------------------------------------
        //returns:   true (success) , false(failure)
        public bool LoadSolidControlEq()
        {
            //string METHOD = "LoadSolidControlEq : " + CLASS_NAME;

            skipCellValueChanged_dgvSolidControlEq = true;

            dgvSolidEq.Rows.Clear();

            try
            {
                string query =
                    " select rs.ID, p1.Value, s.Num, p2.Value, p3.Value, s.Specification, \'\', \'\', s.ID "
                    + " from rt_Rig2SolidControlEq s "
                    + " left join lt_PredefValues p1 on s.Name_PredefAutoID = p1.AutoID "
                    + " left join lt_PredefValues p2 on s.Brand_PredefAutoID = p2.AutoID "
                    + " left join lt_PredefValues p3 on s.Model_PredefAutoID = p3.AutoID "
                    + " left join (select * from rt_Rep2SolidControl where ReportID = " + frmMain.selectedRepID.ToString() + " ) rs "
                    + " on rs.SC_ID = s.ID "
                    + " where s.RigID = " + frmMain.selected_RW_RigID.ToString()
                    + " order by s.UserOrder ";

                DataSet ds = ConnectionManager.ExecQuery(query);

                if (ds != null)
                {
                    ProjectForm prjForm = (ProjectForm)mainForm.subForms[mainForm.PorjectFormID];
                    string selUnitDischarge = prjForm.lblUnitSelectedDischargeLossRate.Tag.ToString();
                    string selUnitVol = prjForm.lblUnitSelectedLiquidVolume.Tag.ToString();

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        dgvSolidEq.Rows.Add(ds.Tables[0].Rows[i].ItemArray);

                        string r2scIdStr = ds.Tables[0].Rows[i][0].ToString();

                        if (r2scIdStr.Trim() != "")
                        {
                            List<string> screenSizes;
                            if (FetchTableData.GetSolidControlEqScreenSizes(r2scIdStr, out screenSizes))
                                dgvSolidEq.Rows[i].Cells[6].Value = SCECombineScreenSizes(screenSizes);

                            string used = "";
                            if (FetchTableData.GetSolidControlEqSumUsedDischargedLost(r2scIdStr, selUnitDischarge, selUnitVol, out used))
                                dgvSolidEq.Rows[i].Cells[7].Value = used;
                        }

                        dgvSolidEq.Rows[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dgvSolidEq.Rows[i].HeaderCell.Value = (i + 1).ToString();
                    }

                    ds.Dispose();
                    skipCellValueChanged_dgvSolidControlEq = false;
                    return true;
                }
            }
            catch (Exception)
            {
                //ErrorManager.Set_Error(new Errors("Can not load data from data base", ex.Message, METHOD));
                //will be handled outside
            }

            skipCellValueChanged_dgvSolidControlEq = false;
            return false;
        }
        //-------------------------------------------------------

        private void btnSolRefresh_Click(object sender, EventArgs e)
        {
            string METHOD = "btnGeoRefresh_Click : " + CLASS_NAME;

            if (frmMain.selectedRepID == -1)
                return;

            if (access.solidControlEquipment == RoleAccess.AcessTypes.AT_None)
                return;

            if (!LoadSolidControlEq())
            {
                InformationManager.Set_Info(new Errors("Can not load data from data base", "", METHOD));
                //?????logout????
            }
        }
        //-------------------------------------------------------
        private void btnSolExport_Click(object sender, EventArgs e)
        {
            if (frmMain.selectedRepID == -1)
                return;

            if (access.solidControlEquipment == RoleAccess.AcessTypes.AT_None)
                return;

            string fileName = EPP_ExcelManager.SaveFileDialog();

            if (fileName.Trim() == "")
                return;

            EPP_ExcelManager.Save(dgvSolidEq, fileName, "SCE For Report", true, 1);
        }
        //-------------------------------------------------------
        private void dgvSolidEq_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex == -1)
                return;

            if (skipCellValueChanged_dgvSolidControlEq)
                return;
        }
        //-------------------------------------------------------
        private void dgvSolidEq_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (frmMain.selectedRepID == -1)
                return;

            if (access.solidControlEquipment != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;

            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 6 || e.ColumnIndex == 7)
                {
                    if (!tryAddSolidControl(e.RowIndex))
                        return;
                }

                string r2scIDStr = DGV_Operations.CellValueAsString(dgvSolidEq.Rows[e.RowIndex].Cells[0]);

                if (e.ColumnIndex == 6)
                {
                    SolidConEqScreenForm scs = new SolidConEqScreenForm(Convert.ToInt64(r2scIDStr));
                    scs.ShowDialog();

                    List<string> screenSizes;

                    if (FetchTableData.GetSolidControlEqScreenSizes(r2scIDStr, out screenSizes))
                        dgvSolidEq.Rows[e.RowIndex].Cells[6].Value = SCECombineScreenSizes(screenSizes);
                }
                else if (e.ColumnIndex == 7)
                {
                    ProjectForm prjForm = (ProjectForm)mainForm.subForms[mainForm.PorjectFormID];
                    string selUnitDischarge = prjForm.lblUnitSelectedDischargeLossRate.Tag.ToString();
                    string selUnitVol = prjForm.lblUnitSelectedLiquidVolume.Tag.ToString();

                    SolidConEqUsedForm s = new SolidConEqUsedForm(r2scIDStr, prjForm);
                    s.Text = DGV_Operations.CellValueAsString(dgvSolidEq.Rows[e.RowIndex].Cells[1]) + " " + DGV_Operations.CellValueAsString(dgvSolidEq.Rows[e.RowIndex].Cells[2]);
                    s.ShowDialog();

                    string used = "";
                    if (FetchTableData.GetSolidControlEqSumUsedDischargedLost(r2scIDStr, selUnitDischarge, selUnitVol, out used))
                        dgvSolidEq.Rows[e.RowIndex].Cells[7].Value = used;
                }
            }
        }
        //-------------------------------------------------------
        //returns true on OK
        private bool tryAddSolidControl(int rowID)
        {
            if (frmMain.selectedRepID == -1)
                return false;

            if (access.solidControlEquipment != RoleAccess.AcessTypes.AT_WriteAndRead)
                return false;

            List<ProcedureParam> prms = new List<ProcedureParam>();
            string idStr = DGV_Operations.CellValueAsString(dgvSolidEq.Rows[rowID].Cells[0]);

            if (idStr != "")
                return true;

            prms.Add(new ProcedureParam("@ReportID", frmMain.selectedRepID.ToString(), ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Report"));
            prms.Add(new ProcedureParam("@ProjAutoID", frmMain.selectedPrjID.ToString(), ProcedureParam.ParamType.PT_Int, true, "1", false, "", "Invalid Project"));
            prms.Add(new ProcedureParam("@SC_ID", DGV_Operations.CellValueAsString(dgvSolidEq.Rows[rowID].Cells[8]), ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Solid Control"));

            string simpErr;
            Errors critErr;
            Int64 resultStat;

            int ret = Procedures.CallProcedure("prc_rt_Rep2SolidControl_add", prms, out simpErr, out critErr, out resultStat);

            if (ret == 0 && resultStat > 0)//new record is just added
            {
                dgvSolidEq.Rows[rowID].Cells[0].Value = resultStat;
                return true;
            }

            return false;
        }
        //-------------------------------------------------------
        //returns:   true (success) , false(failure)
        public bool LoadMudPump()
        {
            //string METHOD = "LoadMudPump : " + CLASS_NAME;

            skipCellValueChanged_dgvMudPumps = true;

            dgvMudPumps.Rows.Clear();

            try
            {
                //~~~~~~~~~~~~~~
                ProjectForm prjForm = (ProjectForm)mainForm.subForms[mainForm.PorjectFormID];
                string selUnitVolPerStroke = prjForm.lblUnitSelectedVolumeStroke.Tag.ToString();
                string selUnitFlowRate = prjForm.lblUnitSelectedFlowRate.Tag.ToString();
                string selUnitLinerLenAndDia = prjForm.lblUnitSelectedLinerLengthAndDiameter.Tag.ToString();
                //~~~~~~~~~~~~~~

                string query =
                              " select rmp.ID, pm.Num, p1.Value, p2.Value, p3.Value, pm.Efficiency, dbo.fn_Get_VolPerStkBbl(pm.ID), isnull(rmp.StkRate, 0), 0, pm.ID"
                            + " from rt_Rig2MudPump pm "
                            + " left join lt_PredefValues p1 on pm.Type_PredefAutoID = p1.AutoID "
                            + " left join lt_PredefValues p2 on pm.Brand_PredefAutoID = p2.AutoID "
                            + " left join lt_PredefValues p3 on pm.Model_PredefAutoID = p3.AutoID "
                            + " left join (select * from rt_Rep2MudPump where ReportID = " + frmMain.selectedRepID.ToString() + " ) rmp "
                            + " on rmp.PumpID = pm.ID "
                            + " where pm.RigID =  " + frmMain.selected_RW_RigID.ToString()
                            + " order by pm.Num ";

                DataSet ds = ConnectionManager.ExecQuery(query);

                if (ds != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        string recordID = ds.Tables[0].Rows[i][0].ToString();
                        string pn = ds.Tables[0].Rows[i][1].ToString();
                        string pType = ds.Tables[0].Rows[i][2].ToString();
                        string brand = ds.Tables[0].Rows[i][3].ToString();
                        string model = ds.Tables[0].Rows[i][4].ToString();
                        decimal eff = Convert.ToDecimal(ds.Tables[0].Rows[i][5]);
                        decimal volStkBbl = Convert.ToDecimal(ds.Tables[0].Rows[i][6]);
                        decimal stkRate = Convert.ToDecimal(ds.Tables[0].Rows[i][7]);
                        string pID = ds.Tables[0].Rows[i][9].ToString();

                        if (selUnitLinerLenAndDia == "mm")
                            volStkBbl *= (decimal)Math.Pow(1 / 25.4, 3);

                        string circRateStr = "???";
                        {
                            circRateStr = (stkRate * volStkBbl).ToString();
                            circRateStr = UnitConverter.Convert("Flow Rate", "bbl/min", selUnitFlowRate, circRateStr);
                        }

                        string volStkStr = UnitConverter.Convert("Volume/Stroke", "bbl/stk", selUnitVolPerStroke, volStkBbl.ToString());


                        dgvMudPumps.Rows.Add(recordID, pn, pType, brand, model, eff, Convert.ToDecimal(volStkStr).ToString("0.###"), stkRate, Convert.ToDecimal(circRateStr).ToString("0.###"), pID);
                        dgvMudPumps[8, i].Tag = Convert.ToDecimal(circRateStr);

                        dgvMudPumps.Rows[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dgvMudPumps.Rows[i].HeaderCell.Value = (i + 1).ToString();
                    }

                    ds.Dispose();
                    skipCellValueChanged_dgvMudPumps = false;
                    return true;
                }
            }
            catch (Exception)
            {
                //ErrorManager.Set_Error(new Errors("Can not load data from data base", ex.Message, METHOD));
                //will be handled outside
            }

            skipCellValueChanged_dgvMudPumps = false;
            return false;
        }
        //-------------------------------------------------------
        private void btnPumpRefresh_Click(object sender, EventArgs e)
        {
            string METHOD = "btnPumpRefresh_Click : " + CLASS_NAME;

            if (frmMain.selectedRepID == -1)
                return;

            if (access.solidControlEquipment == RoleAccess.AcessTypes.AT_None)
                return;

            if (!LoadMudPump())
            {
                InformationManager.Set_Info(new Errors("Can not load data from data base", "", METHOD));
                //?????logout????
            }

            btnPumpApply.ImageKey = "check";
            btnPumpApply.BackColor = frmMain.checkColor;
        }
        //-------------------------------------------------------
        private void btnPumpApply_Click(object sender, EventArgs e)
        {
            if (frmMain.selectedRepID == -1)
                return;

            if (access.solidControlEquipment == RoleAccess.AcessTypes.AT_None)
                return;

            List<Errors> errs = new List<Errors>();

            for (int i = 0; i < dgvMudPumps.Rows.Count; i++)
            {
                Color normalBkColor = (i % 2 == 0) ? dgvMudPumps.BackgroundColor : dgvMudPumps.AlternatingRowsDefaultCellStyle.BackColor;

                dgvMudPumps.Rows[i].Cells[7].Style.BackColor = normalBkColor;

                List<ProcedureParam> prms = new List<ProcedureParam>();
                string idStr = DGV_Operations.CellValueAsString(dgvMudPumps.Rows[i].Cells[0]);

                if (idStr == "")
                    prms.Add(new ProcedureParam("@ID", ProcedureParam.ParamType.PT_BigInt));//try add
                else
                    prms.Add(new ProcedureParam("@ID", idStr, ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Record"));//update
                //~~~~~~~~~~	
                prms.Add(new ProcedureParam("@StkRate", DGV_Operations.CellValueAsString(dgvMudPumps.Rows[i].Cells[7]), ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Stk Rate"));
                //~~~~~~~~~~
                //for adding
                prms.Add(new ProcedureParam("@ReportID", frmMain.selectedRepID.ToString(), ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Report"));
                prms.Add(new ProcedureParam("@ProjAutoID", frmMain.selectedPrjID.ToString(), ProcedureParam.ParamType.PT_Int, true, "1", false, "", "Invalid Project"));
                prms.Add(new ProcedureParam("@PumpID", DGV_Operations.CellValueAsString(dgvMudPumps.Rows[i].Cells[9]), ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Pump"));

                string simpErr;
                Errors critErr;
                Int64 resultStat;

                int ret = Procedures.CallProcedure("prc_rt_Rep2MudPump_add_or_update", prms, out simpErr, out critErr, out resultStat);

                if (ret != 0)
                {
                    if (ret == 1)
                    {
                        if (simpErr.StartsWith("Invalid Stk Rate"))
                            dgvMudPumps.Rows[i].Cells[7].Style.BackColor = Color.Red;

                        critErr = new Errors(simpErr);
                    }

                    errs.Add(critErr);
                }
                else
                {
                    if (resultStat > 0)//new record is just added and updated
                        dgvMudPumps.Rows[i].Cells[0].Value = resultStat;//to be sure
                }
            }

            InformationManager.Set_Info(errs.ToArray());

            if (errs.Count == 0)
            {
                btnPumpApply.ImageKey = "Check";
                btnPumpApply.BackColor = frmMain.checkColor;

                if (!LoadMudPump())
                {
                    //ErrorManager.Set_Error(new Errors("Can not load data from data base", "", METHOD));
                    ////?????logout????
                }
            }
        }
        //-------------------------------------------------------
        private void dgvMudPumps_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex == -1)
                return;

            if (access.solidControlEquipment != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;

            if (frmMain.selectedRepID == -1)
                return;

            if (skipCellValueChanged_dgvMudPumps)
                return;

            if (e.ColumnIndex > 0 && e.RowIndex >= 0)
            {
                int rID = e.RowIndex;

                try
                {
                    //~~~~~~~~~~~~~~
                    ProjectForm prjForm = (ProjectForm)mainForm.subForms[mainForm.PorjectFormID];
                    string selUnitVolPerStroke = prjForm.lblUnitSelectedVolumeStroke.Tag.ToString();
                    string selUnitFlowRate = prjForm.lblUnitSelectedFlowRate.Tag.ToString();
                    //~~~~~~~~~~~~~~

                    if (e.ColumnIndex == 6)
                    {
                        dgvMudPumps.Rows[rID].Cells[7].Value = "???";

                        decimal stkRate = 0;

                        if (AdvancedConvertor.ToDecimal(DGV_Operations.CellValueAsString(dgvMudPumps.Rows[rID].Cells[6]), ref stkRate))
                        {
                            string circRateStr = "???";
                            {
                                string volStkBblStr = UnitConverter.Convert("Volume/Stroke", selUnitVolPerStroke, "bbl/stk", DGV_Operations.CellValueAsString(dgvMudPumps.Rows[rID].Cells[5]));
                                decimal volStkBbl = 0;

                                if (AdvancedConvertor.ToDecimal(volStkBblStr, ref volStkBbl))
                                    circRateStr = (stkRate * volStkBbl).ToString();

                                circRateStr = UnitConverter.Convert("Flow Rate", "bbl/min", selUnitFlowRate, circRateStr);
                            }
                            dgvMudPumps.Rows[rID].Cells[7].Value = Convert.ToDecimal(circRateStr).ToString("0.###");
                            dgvMudPumps[7, rID].Tag = Convert.ToDecimal(circRateStr);
                        }
                    }
                }
                catch (Exception)
                {
                }
            }
            //~~~~~~~~~~~~~~~~~~~~~~
            btnPumpApply.ImageKey = "warning";
            btnPumpApply.BackColor = frmMain.warningColor;
        }
        //-------------------------------------------------------
    }
}
