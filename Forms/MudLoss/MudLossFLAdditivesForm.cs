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
    public partial class MudLossFLAdditivesForm : Form
    {
        const string CLASS_NAME = "MudLossFLAdditivesForm";
        Int64 MudLF_ID = -1;

        bool skipCellValueChanged_dgvAdditive = false;
        //=======================================================
        public MudLossFLAdditivesForm(Int64 MudLF_ID, string selUnitVol)
        {
            InitializeComponent();

            this.MudLF_ID = MudLF_ID;

            UnitString.WriteUnit(dgvAdditive.Columns[4], selUnitVol);

            DGV_Operations.ColorizeFixedColumnHeader(dgvAdditive, 1, 2);
            DGV_Operations.HandleKeyDown(dgvAdditive, null);
        }
        //-------------------------------------------------------
        private void MudLossFLAdditivesForm_Load(object sender, EventArgs e)
        {
            string METHOD = "MudLossFLAdditivesForm_Load : " + CLASS_NAME;

            if (!LoadAdditives())
            {
                InformationManager.Set_Info(new Errors("Can not load data from data base", "", METHOD));
                //?????logout????
            }

            btnApply.ImageKey = "Check";
            btnApply.BackColor = frmMain.checkColor;
        }
        //-------------------------------------------------------
        //returns:   true (success) , false(failure)
        bool LoadAdditives()
        {

            skipCellValueChanged_dgvAdditive = true;

            dgvAdditive.Rows.Clear();

            try
            {
                string query =
                    " select ID, p.Name, p.UnitSize, rp.Used, rp.PillVol "
                    + " from rt_Rep2MudLossFormation_Additive rp join lt_Product p on rp.Prd_AutoID = p.AutoID "
                    + " where rp.MudLF_ID = " + MudLF_ID.ToString();

                DataSet ds = ConnectionManager.ExecQuery(query);

                if (ds != null)
                {
                    //copy row by row to preserve header of table
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        dgvAdditive.Rows.Add(ds.Tables[0].Rows[i].ItemArray);
                        dgvAdditive.Rows[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dgvAdditive.Rows[i].HeaderCell.Value = (i + 1).ToString();
                    }

                    ds.Dispose();
                    skipCellValueChanged_dgvAdditive = false;
                    return true;
                }
            }
            catch (Exception)
            {
                //ErrorManager.Set_Error(new Errors("Can not load data from data base", ex.Message, " LoadAdditives"));
                //will be handled outside
            }

            skipCellValueChanged_dgvAdditive = false;
            return false;
        }
        //-------------------------------------------------------
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string METHOD = "btnGeoAdd_Click : " + CLASS_NAME;

            if (frmMain.selectedRepID == -1)
                return;

            //select unused Products
            string query =
            " select 0 ,* "
            + " from lt_product "
            + " where AutoID in "
            + " (select Prd_AutoID from rt_Prj2Prd where PrjAutoID = " + frmMain.selectedPrjID.ToString() + " ) "
            + " and AutoID not in "
            + " (select Prd_AutoID from rt_Rep2MudLossFormation_Additive where MudLF_ID = " + MudLF_ID.ToString() + " ) ";

            ProductListFormDB_Selection selPrd = new ProductListFormDB_Selection(query);

            if (selPrd.ShowDialog() != DialogResult.OK)
                return;

            List<Errors> errs = new List<Errors>();

            for (int i = 0; i < selPrd.dgvProducts.Rows.Count; i++)
            {
                if (Convert.ToBoolean(selPrd.dgvProducts.Rows[i].Cells[0].Value) == true)
                {
                    List<ProcedureParam> prms = new List<ProcedureParam>();
                    prms.Add(new ProcedureParam("@MudLF_ID", MudLF_ID.ToString(), ProcedureParam.ParamType.PT_BigInt, true, "1", false, "", "Invalid Mud Loss Formation Record"));
                    prms.Add(new ProcedureParam("@Prd_AutoID", DGV_Operations.CellValueAsString(selPrd.dgvProducts.Rows[i].Cells[1]), ProcedureParam.ParamType.PT_Int, true, "1", false, "", "Invalid Product Code"));
                    prms.Add(new ProcedureParam("@ProjAutoID", frmMain.selectedPrjID.ToString(), ProcedureParam.ParamType.PT_Int, true, "1", false, "", "Invalid Project"));

                    string simpErr;
                    Errors critErr;
                    Int64 resultStat;

                    int ret = Procedures.CallProcedure("prc_rt_Rep2MudLossFormation_Additive_insert", prms, out simpErr, out critErr, out resultStat);

                    if (ret != 0)
                    {
                        if (ret == 1) critErr = new Errors(simpErr);

                        errs.Add(critErr);
                    }
                }
            }

            InformationManager.Set_Info(errs.ToArray());

            if (!LoadAdditives())
            {
                InformationManager.Set_Info(new Errors("Can not load data from data base", "", METHOD));
                //?????logout????
            }
        }
        //-------------------------------------------------------
        private void btnRemove_Click(object sender, EventArgs e)
        {
            string METHOD = "btnGeoRemove_Click : " + CLASS_NAME;

            if (frmMain.selectedRepID == -1)
                return;

            if (dgvAdditive.SelectedRows.Count == 0)
            {
                InformationManager.Set_Info("Select Row to Delete");
                return;
            }

            string idStr = DGV_Operations.CellValueAsString(dgvAdditive.Rows[dgvAdditive.SelectedRows[0].Index].Cells[0]);

            List<ProcedureParam> prms = new List<ProcedureParam>();
            prms.Add(new ProcedureParam("@ID", idStr, ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Record"));

            string simpErr;
            Errors critErr;
            Int64 resultStat;

            int ret = Procedures.CallProcedure("prc_rt_Rep2MudLossFormation_Additive_delete", prms, out simpErr, out critErr, out resultStat);

            if (ret != 0)
            {
                if (ret == 1)
                    InformationManager.Set_Info(simpErr);
                else
                    InformationManager.Set_Info(critErr);

                return;
            }

            if (!LoadAdditives())
            {
                InformationManager.Set_Info(new Errors("Can not load data from data base", "", METHOD));
                //?????logout????
            }
        }
        //-------------------------------------------------------
        private void btnApply_Click(object sender, EventArgs e)
        {
            //string METHOD = "btnApply_Click : " + CLASS_NAME;

            if (frmMain.selectedRepID == -1)
                return;

            List<Errors> errs = new List<Errors>();

            for (int i = 0; i < dgvAdditive.Rows.Count; i++)
            {
                List<ProcedureParam> prms = new List<ProcedureParam>();
                string idStr = DGV_Operations.CellValueAsString(dgvAdditive.Rows[i].Cells[0]);
                prms.Add(new ProcedureParam("@ID", idStr, ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Record"));
                prms.Add(new ProcedureParam("@Used", DGV_Operations.CellValueAsString(dgvAdditive.Rows[i].Cells[3]), ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Used"));
                prms.Add(new ProcedureParam("@PillVol", DGV_Operations.CellValueAsString(dgvAdditive.Rows[i].Cells[4]), ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Pill Volume"));

                string simpErr;
                Errors critErr;
                Int64 resultStat;

                int ret = Procedures.CallProcedure("prc_rt_Rep2MudLossFormation_Additive_update", prms, out simpErr, out critErr, out resultStat);

                Color normalBkColor = (i % 2 == 0) ? dgvAdditive.BackgroundColor : dgvAdditive.AlternatingRowsDefaultCellStyle.BackColor;
                dgvAdditive.Rows[i].Cells[3].Style.BackColor = normalBkColor;
                dgvAdditive.Rows[i].Cells[4].Style.BackColor = normalBkColor;

                if (ret != 0)
                {
                    if (ret == 1)
                    {
                        if (simpErr.StartsWith("Invalid Used"))
                            dgvAdditive.Rows[i].Cells[3].Style.BackColor = Color.Red;
                        else if (simpErr.StartsWith("Invalid Pill Volume"))
                            dgvAdditive.Rows[i].Cells[4].Style.BackColor = Color.Red;

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
            string METHOD = "btnGeoRefresh_Click : " + CLASS_NAME;

            if (frmMain.selectedRepID == -1)
                return;

            if (!LoadAdditives())
            {
                InformationManager.Set_Info(new Errors("Can not load data from data base", "", METHOD));
                //?????logout????
            }

            btnApply.ImageKey = "Check";
            btnApply.BackColor = frmMain.checkColor;
        }
        //-------------------------------------------------------
        private void dgvAdditive_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex == -1)
                return;

            if (skipCellValueChanged_dgvAdditive)
                return;

            btnApply.ImageKey = "warning";
            btnApply.BackColor = frmMain.warningColor;
        }
        //-------------------------------------------------------
    }
}
