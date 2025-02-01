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
    public partial class HydraulicNozzlesForm : Form
    {
        const string CLASS_NAME = "HydraulicNozzlesForm";

        string selUnitNozzleSize = "";
        bool skipCellValueChanged_dgvNozzles = false;
        //=======================================================
        public HydraulicNozzlesForm(string selUnitNozzleSize)
        {
            InitializeComponent();
            UnitString.WriteUnit(dgvNozzles.Columns[2], selUnitNozzleSize);
            this.selUnitNozzleSize = selUnitNozzleSize;
        }
        //-------------------------------------------------------
        private void HydraulicNozzlesForm_Load(object sender, EventArgs e)
        {
            string METHOD = "HydraulicNozzlesForm_Load : " + CLASS_NAME;

            if (!LoadNozzles())
            {
                InformationManager.Set_Info(new Errors("Can not load data from data base", "", METHOD));
                //?????logout????
            }

            btnApply.ImageKey = "Check";
            btnApply.BackColor = frmMain.checkColor;
        }
        //-------------------------------------------------------
        //returns:   true (success) , false(failure)
        bool LoadNozzles()
        {
            skipCellValueChanged_dgvNozzles = true;

            dgvNozzles.Rows.Clear();

            try
            {
                string query =
                    " select ID, NozzleCount, NozzleSize "
                    + " from rt_Rep2HydraulicNozzles "
                    + " where ReportID  = " + frmMain.selectedRepID.ToString()
                    + " order by NozzleSize ";

                DataSet ds = ConnectionManager.ExecQuery(query);

                if (ds != null)
                {
                    //copy row by row to preserve header of table
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        dgvNozzles.Rows.Add(ds.Tables[0].Rows[i].ItemArray);
                        dgvNozzles.Rows[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dgvNozzles.Rows[i].HeaderCell.Value = (i + 1).ToString();
                    }

                    ds.Dispose();
                    skipCellValueChanged_dgvNozzles = false;
                    return true;
                }
            }
            catch (Exception)
            {
                //ErrorManager.Set_Error(new Errors("Can not load data from data base", ex.Message, " LoadNozzles"));
                //will be handled outside
            }

            skipCellValueChanged_dgvNozzles = false;
            return false;
        }
        //-------------------------------------------------------
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string METHOD = "btnAdd_Click : " + CLASS_NAME;

            if (frmMain.selectedRepID == -1)
                return;

            frmMultiInput fm = new frmMultiInput();

            fm.lblTitle.Text = "Nozzle:";
            fm.dgvInput.Columns.Add("count", "Count");
            fm.dgvInput.Columns.Add("size", "Size (" + selUnitNozzleSize + ")");

            fm.dgvInput.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            fm.dgvInput.Columns[0].SortMode = DataGridViewColumnSortMode.Programmatic;
            fm.dgvInput.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            fm.dgvInput.Columns[1].SortMode = DataGridViewColumnSortMode.Programmatic;

            fm.dgvInput.AllowUserToAddRows = false;
            fm.dgvInput.AllowUserToDeleteRows = false;
            fm.dgvInput.RowHeadersVisible = false;

            fm.dgvInput.Rows.Add();

            if (fm.ShowDialog() != DialogResult.OK)
                return;

            List<ProcedureParam> prms = new List<ProcedureParam>();
            prms.Add(new ProcedureParam("@ReportID", frmMain.selectedRepID.ToString(), ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Report"));
            prms.Add(new ProcedureParam("@ProjAutoID", frmMain.selectedPrjID.ToString(), ProcedureParam.ParamType.PT_Int, true, "1", false, "", "Invalid Project"));
            prms.Add(new ProcedureParam("@NozzleCount", DGV_Operations.CellValueAsString(fm.dgvInput.Rows[0].Cells[0]), ProcedureParam.ParamType.PT_Int, true, "0", true, "200", "Invalid Nozzle Count"));
            prms.Add(new ProcedureParam("@NozzleSize", DGV_Operations.CellValueAsString(fm.dgvInput.Rows[0].Cells[1]), ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Nozzle Size"));
 
            string simpErr;
            Errors critErr;
            Int64 resultStat;

            int ret = Procedures.CallProcedure("prc_rt_Rep2HydraulicNozzles_insert", prms, out simpErr, out critErr, out resultStat);

            if (ret != 0)
            {
                if (ret == 1)
                    InformationManager.Set_Info(simpErr);
                else
                    InformationManager.Set_Info(critErr);

                return;
            }

            if (!LoadNozzles())
            {
                InformationManager.Set_Info(new Errors("Can not load data from data base", "", METHOD));
                //?????logout????
            }
        }
        //-------------------------------------------------------
        private void btnRemove_Click(object sender, EventArgs e)
        {
            string METHOD = "btnRemove_Click : " + CLASS_NAME;

            if (frmMain.selectedRepID == -1)
                return;

            if (dgvNozzles.SelectedRows.Count == 0)
            {
                InformationManager.Set_Info("Select Row to Delete");
                return;
            }

            string idStr = DGV_Operations.CellValueAsString(dgvNozzles.Rows[dgvNozzles.SelectedRows[0].Index].Cells[0]);

            List<ProcedureParam> prms = new List<ProcedureParam>();
            prms.Add(new ProcedureParam("@ID", idStr, ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Nozzle Record"));
            string simpErr;
            Errors critErr;
            Int64 resultStat;

            int ret = Procedures.CallProcedure("prc_rt_Rep2HydraulicNozzles_delete", prms, out simpErr, out critErr, out resultStat);

            if (ret != 0)
            {
                if (ret == 1)
                    InformationManager.Set_Info(simpErr);
                else
                    InformationManager.Set_Info(critErr);

                return;
            }

            if (!LoadNozzles())
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

            for (int i = 0; i < dgvNozzles.Rows.Count; i++)
            {
                List<ProcedureParam> prms = new List<ProcedureParam>();

                string idStr = DGV_Operations.CellValueAsString(dgvNozzles.Rows[i].Cells[0]);

                prms.Add(new ProcedureParam("@ID", idStr, ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Nozzle Record"));
                prms.Add(new ProcedureParam("@NozzleCount", DGV_Operations.CellValueAsString(dgvNozzles.Rows[i].Cells[1]), ProcedureParam.ParamType.PT_Int, true, "0", true, "200", "Invalid Nozzle Count"));
                prms.Add(new ProcedureParam("@NozzleSize", DGV_Operations.CellValueAsString(dgvNozzles.Rows[i].Cells[2]), ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Nozzle Size"));
 
                string simpErr;
                Errors critErr;
                Int64 resultStat;

                int ret = Procedures.CallProcedure("prc_rt_Rep2HydraulicNozzles_Update", prms, out simpErr, out critErr, out resultStat);

                Color normalBkColor = (i % 2 == 0) ? dgvNozzles.BackgroundColor : dgvNozzles.AlternatingRowsDefaultCellStyle.BackColor;
                dgvNozzles.Rows[i].Cells[1].Style.BackColor = normalBkColor;
                dgvNozzles.Rows[i].Cells[2].Style.BackColor = normalBkColor;

                if (ret != 0)
                {
                    if (ret == 1)
                    {
                        if (simpErr.StartsWith("Invalid Nozzle Count"))
                            dgvNozzles.Rows[i].Cells[1].Style.BackColor = Color.Red;
                        else if (simpErr.StartsWith("Invalid Nozzle Size"))
                            dgvNozzles.Rows[i].Cells[2].Style.BackColor = Color.Red;


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
            string METHOD = "btnRefresh_Click : " + CLASS_NAME;

            if (frmMain.selectedRepID == -1)
                return;

            if (!LoadNozzles())
            {
                InformationManager.Set_Info(new Errors("Can not load data from data base", "", METHOD));
                //?????logout????
            }

            btnApply.ImageKey = "Check";
            btnApply.BackColor = frmMain.checkColor;
        }
        //-------------------------------------------------------
        private void dgvNozzles_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex == -1)
                return;

            if (skipCellValueChanged_dgvNozzles)
                return;

            btnApply.ImageKey = "warning";
            btnApply.BackColor = frmMain.warningColor;
        }
        //-------------------------------------------------------
    }
}
