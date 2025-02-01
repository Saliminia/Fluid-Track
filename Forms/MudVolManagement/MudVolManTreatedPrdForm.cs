using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace DMR
{
    public partial class MudVolManTreatedPrdForm : Form
    {
        bool skipCellValueChanged_dgvPrd = false;
        Int64 TreatedVol_ID = -1;
        //=======================================================
        public MudVolManTreatedPrdForm(Int64 TreatedVol_ID)
        {
            InitializeComponent();

            this.TreatedVol_ID = TreatedVol_ID;

            DGV_Operations.ColorizeFixedColumnHeader(dgvProduct, 1, 2);
            DGV_Operations.HandleKeyDown(dgvProduct, null);
        }
        //-------------------------------------------------------
        private void MudVolManTreatedPrdForm_Load(object sender, EventArgs e)
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
                  " select tp.ID, prd.Name, prd.UnitSize, isnull(tp.Used_PDF, 0), isnull(tp.Used_OtherCompany, 0), tp.Comment, p2prd.Prd_AutoID "

                + " from rt_Prj2Prd p2prd join lt_Product prd on p2prd.Prd_AutoID = prd.AutoID "
                + " left join (select * from rt_Rep2MudVolManDFS_TreatedVol_Prd where TreatedVol_ID = " + TreatedVol_ID.ToString() + " ) tp "
                + " on p2prd.Prd_AutoID = tp.Prd_AutoID "
                + " where p2prd.PrjAutoID = " + frmMain.selectedPrjID.ToString()
                + " order by  tp.Used_PDF desc ";

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

            btnApply.ImageKey = "warning";
            btnApply.BackColor = frmMain.warningColor;
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
                prms.Add(new ProcedureParam("@Used_PDF", DGV_Operations.CellValueAsString(dgvProduct.Rows[i].Cells[3]), ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Used-PDF"));
                prms.Add(new ProcedureParam("@Used_OtherCompany", DGV_Operations.CellValueAsString(dgvProduct.Rows[i].Cells[4]), ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Used-Other Company"));
                prms.Add(new ProcedureParam("@Comment", DGV_Operations.CellValueAsString(dgvProduct.Rows[i].Cells[5]), 200));
                //~~~~~~~~~~
                //for adding
                prms.Add(new ProcedureParam("@TreatedVol_ID", TreatedVol_ID.ToString(), ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Treat Vol. Record"));
                prms.Add(new ProcedureParam("@ProjAutoID", frmMain.selectedPrjID.ToString(), ProcedureParam.ParamType.PT_Int, true, "1", false, "", "Invalid Project"));
                prms.Add(new ProcedureParam("@Prd_AutoID", DGV_Operations.CellValueAsString(dgvProduct.Rows[i].Cells[6]), ProcedureParam.ParamType.PT_Int, true, "1", false, "", "Invalid Product"));


                string simpErr;
                Errors critErr;
                Int64 resultStat;

                int ret = Procedures.CallProcedure("prc_rt_Rep2MudVolManDFS_TreatedVol_Prd_add_or_update", prms, out simpErr, out critErr, out resultStat);

                Color normalBkColor = (i % 2 == 0) ? dgvProduct.BackgroundColor : dgvProduct.AlternatingRowsDefaultCellStyle.BackColor;
                dgvProduct.Rows[i].Cells[3].Style.BackColor = normalBkColor;
                dgvProduct.Rows[i].Cells[4].Style.BackColor = normalBkColor;

                if (ret != 0)
                {
                    if (ret == 1)
                    {
                        if (simpErr.StartsWith("Invalid Used-PDF"))
                            dgvProduct.Rows[i].Cells[3].Style.BackColor = Color.Red;
                        else if (simpErr.StartsWith("Invalid Used-Other Company"))
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
