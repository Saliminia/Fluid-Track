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
    public partial class SolidConEqUsedForm : Form
    {
        string r2scIDStr = "";
        const string CLASS_NAME = "SolidConEqUsedForm";
        bool skipCellValueChanged_dgvUsed = false;

        string selUnitDischarge = "";
        string selUnitVol = "";
        string actVolSelUnitVol = "";
        //=======================================================
        public SolidConEqUsedForm(string r2scIDStr, ProjectForm prjForm)
        {
            InitializeComponent();

            this.r2scIDStr = r2scIDStr;
            //~~~~~~~~~~~~~~
            string selUnitMW = prjForm.lblUnitSelectedMudWeight.Tag.ToString();
            UnitString.WriteUnit(dgvUsed.Columns[4], selUnitMW);
            UnitString.WriteUnit(dgvUsed.Columns[5], selUnitMW);
            UnitString.WriteUnit(dgvUsed.Columns[6], selUnitMW);

            selUnitDischarge = prjForm.lblUnitSelectedDischargeLossRate.Tag.ToString();
            selUnitVol = prjForm.lblUnitSelectedLiquidVolume.Tag.ToString();
            UnitString.WriteUnit(dgvUsed.Columns[7], selUnitDischarge);

            actVolSelUnitVol = selUnitVol;

            UnitString.WriteUnit(dgvUsed.Columns[8], actVolSelUnitVol);

            DGV_Operations.ColorizeNestedColumnHeader(dgvUsed, 3);
            DGV_Operations.ColorizeComputedColumnHeader(dgvUsed, 8);
            DGV_Operations.HandleKeyDown(dgvUsed, dgvUsed_CellMouseClick);
        }
        //-------------------------------------------------------
        private void SolidConEqUsedForm_Load(object sender, EventArgs e)
        {
            if (!LoadRecords())
            {
                //ErrorManager.Set_Error(new Errors("Can not load data from data base", "", METHOD));
                //?????logout????
            }

            btnApply.ImageKey = "check";
            btnApply.BackColor = frmMain.checkColor;
        }
        //-------------------------------------------------------
        bool LoadRecords()
        {
            skipCellValueChanged_dgvUsed = true;

            dgvUsed.Rows.Clear();

            try
            {
                string query =
                      " select "
                    + " ID, Used, Dfs_AutoID, d.DrillingFluidSystem, FeedFlowSpec, OverFlow,  "
                    + " UnderFlow, Discharge, 0, Comment "
                    + " from rt_Rep2SolidControlUsed left join lt_DrillingFluidSystem d on Dfs_AutoID = d.AutoID "
                    + " where R2SC_ID = " + r2scIDStr;

                DataSet ds = ConnectionManager.ExecQuery(query);

                if (ds != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        dgvUsed.Rows.Add(ds.Tables[0].Rows[i].ItemArray);
                        dgvUsed.Rows[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dgvUsed.Rows[i].HeaderCell.Value = (i + 1).ToString();

                        ComputeActualVolLost(i);
                    }

                    ds.Dispose();
                }
                else
                {
                    skipCellValueChanged_dgvUsed = false;
                    return false;
                }
            }
            catch (Exception)
            {
                //ErrorManager.Set_Error(new Errors("Can not load data from data base", ex.Message, METHOD));
                //will be handled outside
            }

            skipCellValueChanged_dgvUsed = false;
            return true;
        }
        //-------------------------------------------------------
        void ComputeActualVolLost(int rowID)
        {
            skipCellValueChanged_dgvUsed = true;

            try
            {
                dgvUsed.Rows[rowID].Cells[8].Tag = null;
                dgvUsed.Rows[rowID].Cells[8].Value = "Error";

                decimal used = Convert.ToDecimal(DGV_Operations.CellValueAsString(dgvUsed.Rows[rowID].Cells[1]));
                decimal dis = Convert.ToDecimal(DGV_Operations.CellValueAsString(dgvUsed.Rows[rowID].Cells[7]));

                decimal actLossFactor = 1;
                if (selUnitDischarge == "bbl/hr")
                {
                    if (selUnitVol == "m³")
                        actLossFactor = 0.1589876m;//m³ -> bbl                          
                }
                else// m³/hr
                {
                    if (selUnitVol == "bbl")
                        actLossFactor = 6.2898m;//bbl -> m³                       
                }

                decimal actVolLost = dis * used * actLossFactor;

                dgvUsed.Rows[rowID].Cells[8].Tag = actVolLost.ToString();
                dgvUsed.Rows[rowID].Cells[8].Value = actVolLost.ToString("0.###");
            }
            catch (Exception)
            {
            }

            skipCellValueChanged_dgvUsed = false;
        }
        //-------------------------------------------------------
        private void btnAdd_Click(object sender, EventArgs e)
        {
            Int64 generatedID = -1;

            List<ProcedureParam> prms = new List<ProcedureParam>();
            prms.Add(new ProcedureParam("@R2SC_ID", r2scIDStr, ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Record"));
            prms.Add(new ProcedureParam("@ProjAutoID", frmMain.selectedPrjID.ToString(), ProcedureParam.ParamType.PT_Int, true, "1", false, "", "Invalid Project"));

            string simpErr;
            Errors critErr;

            int ret = Procedures.CallProcedure("prc_rt_Rep2SolidControlUsed_insert", prms, out simpErr, out critErr, out generatedID);

            if (ret != 0)
            {
                if (ret == 1)
                    InformationManager.Set_Info(simpErr);
                else
                    InformationManager.Set_Info(critErr);

                return;
            }


            LoadRecords();

            btnApply.ImageKey = "Check";
            btnApply.BackColor = frmMain.checkColor;
        }
        //-------------------------------------------------------
        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (dgvUsed.SelectedRows.Count == 0)
            {
                InformationManager.Set_Info("Select Row to Delete");
                return;
            }

            string id = DGV_Operations.CellValueAsString(dgvUsed.Rows[dgvUsed.SelectedRows[0].Index].Cells[0]);

            List<ProcedureParam> prms = new List<ProcedureParam>();
            prms.Add(new ProcedureParam("@ID", id, ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Record"));

            string simpErr;
            Errors critErr;
            Int64 resultStat;

            int ret = Procedures.CallProcedure("prc_rt_Rep2SolidControlUsed_delete ", prms, out simpErr, out critErr, out resultStat);

            if (ret != 0)
            {
                if (ret == 1)
                    InformationManager.Set_Info(simpErr);
                else
                    InformationManager.Set_Info(critErr);

                return;
            }

            LoadRecords();

            btnApply.ImageKey = "Check";
            btnApply.BackColor = frmMain.checkColor;
        }
        //-------------------------------------------------------
        private void btnApply_Click(object sender, EventArgs e)
        {
            List<Errors> errs = new List<Errors>();

            for (int i = 0; i < dgvUsed.Rows.Count; i++)
            {
                List<ProcedureParam> prms = new List<ProcedureParam>();
                prms.Add(new ProcedureParam("@ID", DGV_Operations.CellValueAsString(dgvUsed.Rows[i].Cells[0]), ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Record"));
                prms.Add(new ProcedureParam("@Used", DGV_Operations.CellValueAsString(dgvUsed.Rows[i].Cells[1]), ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Used"));
                prms.Add(new ProcedureParam("@FeedFlowSpec", DGV_Operations.CellValueAsString(dgvUsed.Rows[i].Cells[4]), ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Feed Flow Spec."));
                prms.Add(new ProcedureParam("@OverFlow", DGV_Operations.CellValueAsString(dgvUsed.Rows[i].Cells[5]), ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Overflow"));
                prms.Add(new ProcedureParam("@UnderFlow", DGV_Operations.CellValueAsString(dgvUsed.Rows[i].Cells[6]), ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Underflow"));
                prms.Add(new ProcedureParam("@Discharge", DGV_Operations.CellValueAsString(dgvUsed.Rows[i].Cells[7]), ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Discharge"));
                prms.Add(new ProcedureParam("@Comment", DGV_Operations.CellValueAsString(dgvUsed.Rows[i].Cells[9]), 200));

                string dfs = DGV_Operations.CellValueAsString(dgvUsed.Rows[i].Cells[3]);

                if (dfs == "")
                    prms.Add(new ProcedureParam("@Dfs_AutoID", ProcedureParam.ParamType.PT_Int));
                else
                    prms.Add(new ProcedureParam("@Dfs_AutoID", DGV_Operations.CellValueAsString(dgvUsed.Rows[i].Cells[2]), ProcedureParam.ParamType.PT_Int, true, "1", false, "", "Invalid DFS"));

                string simpErr;
                Errors critErr;
                Int64 resultStat;

                int ret = Procedures.CallProcedure("prc_rt_Rep2SolidControlUsed_update", prms, out simpErr, out critErr, out resultStat);

                Color normalBkColor = (i % 2 == 0) ? dgvUsed.BackgroundColor : dgvUsed.AlternatingRowsDefaultCellStyle.BackColor;
                for (int j = 1; j <= 8; j++)
                    dgvUsed.Rows[i].Cells[j].Style.BackColor = normalBkColor;

                if (ret != 0)
                {
                    if (ret == 1)
                    {
                        if (simpErr.StartsWith("Invalid Used"))
                            dgvUsed.Rows[i].Cells[1].Style.BackColor = Color.Red;
                        else if (simpErr.StartsWith("Invalid Feed Flow Spec."))
                            dgvUsed.Rows[i].Cells[4].Style.BackColor = Color.Red;
                        else if (simpErr.StartsWith("Invalid Overflow"))
                            dgvUsed.Rows[i].Cells[5].Style.BackColor = Color.Red;
                        else if (simpErr.StartsWith("Invalid Underflow"))
                            dgvUsed.Rows[i].Cells[6].Style.BackColor = Color.Red;
                        else if (simpErr.StartsWith("Invalid Discharge"))
                            dgvUsed.Rows[i].Cells[7].Style.BackColor = Color.Red;

                        critErr = new Errors(simpErr);
                    }

                    errs.Add(critErr);
                }
            }

            InformationManager.Set_Info(errs.ToArray());

            if (errs.Count == 0)
            {
                btnApply.ImageKey = "Check";
                btnApply.BackColor = frmMain.checkColor;
            }
        }
        //-------------------------------------------------------
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (!LoadRecords())
            {
                //ErrorManager.Set_Error(new Errors("Can not load data from data base", "", METHOD));
                //?????logout????
            }

            btnApply.ImageKey = "check";
            btnApply.BackColor = frmMain.checkColor;
        }
        //-------------------------------------------------------
        private void dgvUsed_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex == -1)
                return;

            if (skipCellValueChanged_dgvUsed)
                return;

            ComputeActualVolLost(e.RowIndex);

            btnApply.ImageKey = "warning";
            btnApply.BackColor = frmMain.warningColor;
        }
        //-------------------------------------------------------
        private void dgvUsed_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //string METHOD = "dgvUsed_CellMouseClick : " + CLASS_NAME;

            if (e.ColumnIndex > 0 && e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 3)//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                {
                    string query = " select 0, AutoID, MudType, DrillingFluidSystem from lt_DrillingFluidSystem ";

                    DrillingFluidSystemFormDB_Selection frmDFSel = new DrillingFluidSystemFormDB_Selection(query);
                    frmDFSel.Text = "Drilling Fluid Systems";

                    frmDFSel.dgvFluidSys.Columns[0].Visible = false;//single selection

                    System.Windows.Forms.DialogResult dgRes = frmDFSel.ShowDialog();

                    if (dgRes == System.Windows.Forms.DialogResult.Cancel)
                        return;

                    if (dgRes == System.Windows.Forms.DialogResult.Ignore)
                    {
                        dgvUsed.Rows[e.RowIndex].Cells[3].Value = DBNull.Value;
                        return;
                    }

                    if (frmDFSel.dgvFluidSys.SelectedRows.Count == 0)
                        return;

                    string drillFS = DGV_Operations.CellValueAsString(frmDFSel.dgvFluidSys.Rows[frmDFSel.dgvFluidSys.SelectedRows[0].Index].Cells[3]);
                    string mudType = DGV_Operations.CellValueAsString(frmDFSel.dgvFluidSys.Rows[frmDFSel.dgvFluidSys.SelectedRows[0].Index].Cells[2]);
                    string dfsAutoID = DGV_Operations.CellValueAsString(frmDFSel.dgvFluidSys.Rows[frmDFSel.dgvFluidSys.SelectedRows[0].Index].Cells[1]);

                    dgvUsed.Rows[e.RowIndex].Cells[2].Value = dfsAutoID;
                    dgvUsed.Rows[e.RowIndex].Cells[3].Value = drillFS;
                }
            }
        }
        //-------------------------------------------------------

    }
}
