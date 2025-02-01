using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace DMR
{
    public partial class MudVolManCmtSpacerPrdForm : Form
    {
        bool skipCellValueChanged_dgvPrd = false;
        Int64 ReportID = -1;

        int CmtSpcType = 0;//  1:spacer     2: cement lead     3:cement tail
        //=======================================================
        public MudVolManCmtSpacerPrdForm(Int64 ReportID, int CmtSpcType)
        {
            InitializeComponent();

            this.ReportID = ReportID;
            this.CmtSpcType = CmtSpcType;

            DGV_Operations.ColorizeFixedColumnHeader(dgvProduct, 1, 2);
            DGV_Operations.HandleKeyDown(dgvProduct, null);
        }
        //-------------------------------------------------------
        private void MudVolManBuiltPrdForm_Load(object sender, EventArgs e)
        {
            if (!LoadProducts())
            {
                //ErrorManager.Set_Error(new Errors("Can not load data from data base", "", METHOD));
                //?????logout????
            }

            btnApply.ImageKey = "check";
            btnApply.BackColor = frmMain.checkColor;
        }
        //-------------------------------------------------------
        bool LoadProducts()
        {
            skipCellValueChanged_dgvPrd = true;

            dgvProduct.Rows.Clear();

            try
            {
                string query =
                      " select csp.ID, prd.Name, prd.UnitSize, isnull(csp.Used, 0), p2prd.Prd_AutoID "

                    + " from rt_Prj2Prd p2prd join lt_Product prd on p2prd.Prd_AutoID = prd.AutoID "
                    + " left join (select * from rt_Rep2MudVolManDFS_CmtSpc_Prd where ReportID = " + frmMain.selectedRepID.ToString() + " and CmtSpcType = " + CmtSpcType.ToString() + " ) csp "
                    + " on p2prd.Prd_AutoID = csp.Prd_AutoID "
                    + " where p2prd.PrjAutoID = " + frmMain.selectedPrjID.ToString()
                    + " order by csp.Used desc ";

                DataSet ds = ConnectionManager.ExecQuery(query);

                if (ds != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        dgvProduct.Rows.Add(ds.Tables[0].Rows[i].ItemArray);
                        dgvProduct.Rows[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dgvProduct.Rows[i].HeaderCell.Value = (i + 1).ToString();
                    }

                    ds.Dispose();
                }
                else
                {
                    skipCellValueChanged_dgvPrd = false;
                    return false;
                }
            }
            catch (Exception)
            {
                //ErrorManager.Set_Error(new Errors("Can not load data from data base", ex.Message, METHOD));
                //will be handled outside
            }

            skipCellValueChanged_dgvPrd = false;
            return true;
        }
        //-------------------------------------------------------
        private void dgvProduct_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex == -1)
                return;

            if (skipCellValueChanged_dgvPrd)
                return;

            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
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

            if (!LoadProducts())
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

            for (int i = 0; i < dgvProduct.Rows.Count; i++)
            {
                List<ProcedureParam> prms = new List<ProcedureParam>();
                string idStr = DGV_Operations.CellValueAsString(dgvProduct.Rows[i].Cells[0]);

                if (idStr == "")
                    prms.Add(new ProcedureParam("@ID", ProcedureParam.ParamType.PT_BigInt));//try add
                else
                    prms.Add(new ProcedureParam("@ID", idStr, ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Record"));//update
                //~~~~~~~~~~
                prms.Add(new ProcedureParam("@Used", DGV_Operations.CellValueAsString(dgvProduct.Rows[i].Cells[3]), ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Used"));
                //~~~~~~~~~~
                //for adding
                prms.Add(new ProcedureParam("@ReportID", frmMain.selectedRepID.ToString(), ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Report"));
                prms.Add(new ProcedureParam("@ProjAutoID", frmMain.selectedPrjID.ToString(), ProcedureParam.ParamType.PT_Int, true, "1", false, "", "Invalid Project"));
                prms.Add(new ProcedureParam("@Prd_AutoID", DGV_Operations.CellValueAsString(dgvProduct.Rows[i].Cells[4]), ProcedureParam.ParamType.PT_Int, true, "1", false, "", "Invalid Product"));
                prms.Add(new ProcedureParam("@CmtSpcType", CmtSpcType.ToString(), ProcedureParam.ParamType.PT_Int, true, "1", false, "", "Invalid Cmt. Type"));

                string simpErr;
                Errors critErr;
                Int64 resultStat;

                int ret = -1;

                ret = Procedures.CallProcedure("prc_rt_Rep2MudVolManDFS_CmtSpc_Prd_add_or_update", prms, out simpErr, out critErr, out resultStat);

                Color normalBkColor = (i % 2 == 0) ? dgvProduct.BackgroundColor : dgvProduct.AlternatingRowsDefaultCellStyle.BackColor;
                dgvProduct.Rows[i].Cells[3].Style.BackColor = normalBkColor;

                if (ret != 0)
                {
                    if (ret == 1)
                    {
                        if (simpErr.StartsWith("Invalid Used"))
                            dgvProduct.Rows[i].Cells[3].Style.BackColor = Color.Red;

                        critErr = new Errors(simpErr);
                    }

                    errs.Add(critErr);
                }
                else
                {
                    if (resultStat > 0)//new record is just added and updated
                        dgvProduct.Rows[i].Cells[0].Value = resultStat;//to be sure
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
    }
}
