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
    public partial class MudVolManTotalWellMergeForm : Form
    {
        bool skipCellValueChanged_dgvMerge = false;

        Int64 mudDfsID = 0;
        string selVolUnit = "";
        //=======================================================
        public MudVolManTotalWellMergeForm(Int64 mudDfsID, string selVolUnit)
        {
            InitializeComponent();

            this.mudDfsID = mudDfsID;
            this.selVolUnit = selVolUnit;

            dgvMerge.Columns[4].HeaderText = "Volume(" + selVolUnit + ")";
        }
        //-------------------------------------------------------
        private void MudVolManTotalWellMergeForm_Load(object sender, EventArgs e)
        {
            if (!LoadMerging())
            {
                //ErrorManager.Set_Error(new Errors("Can not load data from data base", "", METHOD));
                //?????logout????
            }

            btnApply.ImageKey = "check";
            btnApply.BackColor = frmMain.checkColor;
        }
        //-------------------------------------------------------
        bool LoadMerging()
        {
            skipCellValueChanged_dgvMerge = true;

            dgvMerge.Rows.Clear();

            try
            {
                string query =
                    " select mdfsBase.ID, dBase.DrillingFluidSystem, mdfsMerged.ID, dSub.DrillingFluidSystem, tvm.Vol "
                    + " from rt_Rep2MudVolManDFS_TotalWellVolMerge tvm join rt_Rep2MudVolManDFS mdfsBase on tvm.MudVolManDFS_ID = mdfsBase.ID "
                    + " join rt_Rep2MudVolManDFS mdfsMerged on tvm.MudVolManDFS_ToBeMerged_ID = mdfsMerged.ID "
                    + " join lt_DrillingFluidSystem dBase on mdfsBase.Dfs_AutoID = dBase.AutoID "
                    + " join lt_DrillingFluidSystem dSub on mdfsMerged.Dfs_AutoID = dSub.AutoID "
                    + " where tvm.MudVolManDFS_ID = " + mudDfsID.ToString();

                DataSet ds = ConnectionManager.ExecQuery(query);

                if (ds != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        dgvMerge.Rows.Add(ds.Tables[0].Rows[i].ItemArray);
                        dgvMerge.Rows[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dgvMerge.Rows[i].HeaderCell.Value = (i + 1).ToString();
                    }

                    ds.Dispose();
                }
                else
                {
                    skipCellValueChanged_dgvMerge = false;
                    return false;
                }
            }
            catch (Exception)
            {
                //ErrorManager.Set_Error(new Errors("Can not load data from data base", ex.Message, METHOD));
                //will be handled outside
            }

            skipCellValueChanged_dgvMerge = false;
            return true;
        }
        //-------------------------------------------------------
        private void dgvMerge_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex == -1)
                return;

            if (skipCellValueChanged_dgvMerge)
                return;

            if (e.ColumnIndex == 4)
            {
                btnApply.ImageKey = "warning";
                btnApply.BackColor = frmMain.warningColor;
            }
        }
        //-------------------------------------------------------
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (frmMain.selectedRepID == -1)
                return;

            if (!LoadMerging())
            {
                //ErrorManager.Set_Error(new Errors("Can not load data from data base", "", METHOD));
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

            List<Errors> errs = new List<Errors>();

            for (int i = 0; i < dgvMerge.Rows.Count; i++)
            {
                List<ProcedureParam> prms = new List<ProcedureParam>();
                prms.Add(new ProcedureParam("@MudVolManDFS_ID", DGV_Operations.CellValueAsString(dgvMerge.Rows[i].Cells[0]), ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Base Record"));
                prms.Add(new ProcedureParam("@MudVolManDFS_ToBeMerged_ID", DGV_Operations.CellValueAsString(dgvMerge.Rows[i].Cells[2]), ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Sub Record"));
                prms.Add(new ProcedureParam("@Vol", DGV_Operations.CellValueAsString(dgvMerge.Rows[i].Cells[4]), ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Volume"));

                string simpErr;
                Errors critErr;
                Int64 resultStat;

                int ret = Procedures.CallProcedure("prc_rt_Rep2MudVolManDFS_TotalWellVolMerge_update", prms, out simpErr, out critErr, out resultStat);

                Color normalBkColor = (i % 2 == 0) ? dgvMerge.BackgroundColor : dgvMerge.AlternatingRowsDefaultCellStyle.BackColor;
                dgvMerge.Rows[i].Cells[4].Style.BackColor = normalBkColor;

                if (ret != 0)
                {
                    if (ret == 1)
                    {
                        if (simpErr.StartsWith("Invalid Volume"))
                            dgvMerge.Rows[i].Cells[4].Style.BackColor = Color.Red;

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
        private void btnAdd_Click(object sender, EventArgs e)
        {
            MudVolManTotalWellSelForMergeForm frmDFSel = new MudVolManTotalWellSelForMergeForm(mudDfsID);

            frmDFSel.Text = "Select Drilling Fluid System";

            if (frmDFSel.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;

            if (frmDFSel.dgvDFS.SelectedRows.Count == 0)
                return;

            string subDfsIDStr = DGV_Operations.CellValueAsString(frmDFSel.dgvDFS.Rows[frmDFSel.dgvDFS.SelectedRows[0].Index].Cells[0]);

            Int64 generatedID = -1;


            List<ProcedureParam> prms = new List<ProcedureParam>();
            prms.Add(new ProcedureParam("@MudVolManDFS_ID", mudDfsID.ToString(), ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Base Record"));
            prms.Add(new ProcedureParam("@MudVolManDFS_ToBeMerged_ID", subDfsIDStr, ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Sub Record"));
            prms.Add(new ProcedureParam("@ProjAutoID", frmMain.selectedPrjID.ToString(), ProcedureParam.ParamType.PT_Int, true, "1", false, "", "Invalid Project"));


            string simpErr;
            Errors critErr;

            int ret = Procedures.CallProcedure("prc_rt_Rep2MudVolManDFS_TotalWellVolMerge_insert", prms, out simpErr, out critErr, out generatedID);

            if (ret != 0)
            {
                if (ret == 1)
                    InformationManager.Set_Info(simpErr);
                else
                    InformationManager.Set_Info(critErr);

                return;
            }


            LoadMerging();

            btnApply.ImageKey = "Check";
            btnApply.BackColor = frmMain.checkColor;
        }
        //-------------------------------------------------------
        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (dgvMerge.SelectedRows.Count == 0)
            {
                InformationManager.Set_Info("Select Row to Delete");
                return;
            }

            string subDfsIDStr = DGV_Operations.CellValueAsString(dgvMerge.Rows[dgvMerge.SelectedRows[0].Index].Cells[2]);

            List<ProcedureParam> prms = new List<ProcedureParam>();
            prms.Add(new ProcedureParam("@MudVolManDFS_ID", mudDfsID.ToString(), ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Base Record"));
            prms.Add(new ProcedureParam("@MudVolManDFS_ToBeMerged_ID", subDfsIDStr, ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Sub Record"));

            string simpErr;
            Errors critErr;
            Int64 resultStat;

            int ret = Procedures.CallProcedure("prc_rt_Rep2MudVolManDFS_TotalWellVolMerge_delete ", prms, out simpErr, out critErr, out resultStat);

            if (ret != 0)
            {
                if (ret == 1)
                    InformationManager.Set_Info(simpErr);
                else
                    InformationManager.Set_Info(critErr);

                return;
            }

            LoadMerging();

            btnApply.ImageKey = "Check";
            btnApply.BackColor = frmMain.checkColor;
        }
        //-------------------------------------------------------
    }
}
