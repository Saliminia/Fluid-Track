using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace DMR
{
    public partial class MudVolManRecTransPrdForm : Form
    {
        bool skipCellValueChanged_dgvPrd = false;
        Int64 RecTrans_ID = -1;

        string selUnitVol = "";
        string selUnitPowderMatConc = "";
        string selUnitLiquidMatConc = "";

        decimal vol = 0;

        //=======================================================
        public MudVolManRecTransPrdForm(Int64 RecTrans_ID, string selUnitVol, string selUnitPowderMatConc, string selUnitLiquidMatConc)
        {
            InitializeComponent();

            this.RecTrans_ID = RecTrans_ID;

            this.selUnitVol = selUnitVol;
            this.selUnitPowderMatConc = selUnitPowderMatConc;
            this.selUnitLiquidMatConc = selUnitLiquidMatConc;

            UnitString.WriteUnit(dgvProduct.Columns[3], selUnitPowderMatConc + " , " + selUnitLiquidMatConc);
            UnitString.WriteUnit(dgvProduct.Columns[4], selUnitPowderMatConc + " , " + selUnitLiquidMatConc);
            UnitString.WriteUnit(dgvProduct.Columns[5], selUnitPowderMatConc + " , " + selUnitLiquidMatConc);

            DGV_Operations.ColorizeFixedColumnHeader(dgvProduct, 1, 2);
            DGV_Operations.ColorizeComputedColumnHeader(dgvProduct, 5, 6, 7, 8);
            DGV_Operations.HandleKeyDown(dgvProduct, null);
        }
        //-------------------------------------------------------
        private void MudVolManRecTransPrdForm_Load(object sender, EventArgs e)
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
        void ComputeOtherValues(int rowID)
        {
            try
            {
                decimal Concentration_PDF = Convert.ToDecimal(DGV_Operations.CellValueAsString(dgvProduct.Rows[rowID].Cells[3]));
                decimal Concentration_OtherCompany = Convert.ToDecimal(DGV_Operations.CellValueAsString(dgvProduct.Rows[rowID].Cells[4]));
                decimal Concentration_Total = Concentration_PDF + Concentration_OtherCompany;
                //~~~~~~~~~~~~~~~~~~~~~~

                decimal prdAutoID = Convert.ToDecimal(DGV_Operations.CellValueAsString(dgvProduct.Rows[rowID].Cells[9]));
                decimal VolFactor_SelUnit2BBL = (selUnitVol == "m³") ? 6.2898m : 1;
                decimal PowderConsFactor_SelUnit2LBL = (selUnitPowderMatConc == "kg/m³") ? 0.3505m : 1;
                decimal LiquidConcFactor_ToComputeUsed_SelUnit2GalPerBbl = (selUnitLiquidMatConc == "vol%") ? 2.380952381m : 1;
                //~~~~~~~~~~~~~~~~~~~~~~
                decimal Used_PDF = -1;
                decimal Used_OtherCompany = -1;
                //~~~~~~~~~~~~~~~~~~~~~~
                {
                    string query = " select  dbo.fn_Get_PrdUsed_ByVolAndConcentration(" + prdAutoID + "," + vol + "," + Concentration_PDF + "," +
                                                                    +VolFactor_SelUnit2BBL + "," + PowderConsFactor_SelUnit2LBL + ","
                                                                    + LiquidConcFactor_ToComputeUsed_SelUnit2GalPerBbl + ")";
                    DataSet ds = ConnectionManager.ExecQuery(query, 1);
                    if (ds != null) { Used_PDF = Convert.ToDecimal(ds.Tables[0].Rows[0][0]); ds.Dispose(); }
                }
                //~~~~~~~~~~~~~~~~~~~~~~
                {
                    string query = " select  dbo.fn_Get_PrdUsed_ByVolAndConcentration(" + prdAutoID + "," + vol + "," + Concentration_OtherCompany + "," +
                                                                    +VolFactor_SelUnit2BBL + "," + PowderConsFactor_SelUnit2LBL + ","
                                                                    + LiquidConcFactor_ToComputeUsed_SelUnit2GalPerBbl + ")";
                    DataSet ds = ConnectionManager.ExecQuery(query, 1);
                    if (ds != null) { Used_OtherCompany = Convert.ToDecimal(ds.Tables[0].Rows[0][0]); ds.Dispose(); }
                }
                //~~~~~~~~~~~~~~~~~~~~~~
                decimal Used_Total = Used_PDF + Used_OtherCompany;
                //~~~~~~~~~~~~~~~~~~~~~~
                dgvProduct.Rows[rowID].Cells[5].Value = Concentration_Total.ToString("0.000");
                dgvProduct.Rows[rowID].Cells[6].Value = Used_PDF.ToString("0.000");
                dgvProduct.Rows[rowID].Cells[7].Value = Used_OtherCompany.ToString("0.000");
                dgvProduct.Rows[rowID].Cells[8].Value = Used_Total.ToString("0.000");
            }
            catch (Exception)
            {
            }
        }
        //-------------------------------------------------------
        bool LoadProducts()
        {
            if (!FetchTableData.GetRecTransVol(RecTrans_ID.ToString(), out vol))
                return false;

            lblVolume.Text = vol.ToString("0.000") + " (" + selUnitVol + ")";
            //~~~~~~~~~~~~~~~~
            skipCellValueChanged_dgvPrd = true;

            dgvProduct.Rows.Clear();

            try
            {
                string query = "";

                query =
                  " select rtp.ID, prd.Name, prd.UnitSize, isnull(Concentration_PDF, 0), isnull(Concentration_OtherCompany, 0), "
                + " 0, 0, 0, 0, p2prd.Prd_AutoID "
                + " from rt_Prj2Prd p2prd join lt_Product prd on p2prd.Prd_AutoID = prd.AutoID "
                + " left join (select * from rt_Rep2MudVolManDFS_RecTrans_Prd where RecTrans_ID = " + RecTrans_ID.ToString() + " ) rtp "
                + " on p2prd.Prd_AutoID = rtp.Prd_AutoID "
                + " where p2prd.PrjAutoID = " + frmMain.selectedPrjID.ToString()
                + " order by  rtp.Concentration_PDF desc ";

                DataSet ds = ConnectionManager.ExecQuery(query);

                if (ds != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        dgvProduct.Rows.Add(ds.Tables[0].Rows[i].ItemArray);
                        dgvProduct.Rows[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dgvProduct.Rows[i].HeaderCell.Value = (i + 1).ToString();

                        ComputeOtherValues(i);
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

            if (e.ColumnIndex != 3 && e.ColumnIndex != 4)
                return;

            ComputeOtherValues(e.RowIndex);

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
                prms.Add(new ProcedureParam("@Concentration_PDF", DGV_Operations.CellValueAsString(dgvProduct.Rows[i].Cells[3]), ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Concentration PDF"));
                prms.Add(new ProcedureParam("@Concentration_OtherCompany", DGV_Operations.CellValueAsString(dgvProduct.Rows[i].Cells[4]), ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Concentration Other Company"));
                //~~~~~~~~~~
                //for adding
                prms.Add(new ProcedureParam("@RecTrans_ID", RecTrans_ID.ToString(), ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Rec/Trans. Vol. Record"));

                prms.Add(new ProcedureParam("@ProjAutoID", frmMain.selectedPrjID.ToString(), ProcedureParam.ParamType.PT_Int, true, "1", false, "", "Invalid Project"));
                prms.Add(new ProcedureParam("@Prd_AutoID", DGV_Operations.CellValueAsString(dgvProduct.Rows[i].Cells[9]), ProcedureParam.ParamType.PT_Int, true, "1", false, "", "Invalid Product"));

                string simpErr;
                Errors critErr;
                Int64 resultStat;

                int ret = -1;

                ret = Procedures.CallProcedure("prc_rt_Rep2MudVolManDFS_RecTrans_Prd_add_or_update", prms, out simpErr, out critErr, out resultStat);

                Color normalBkColor = (i % 2 == 0) ? dgvProduct.BackgroundColor : dgvProduct.AlternatingRowsDefaultCellStyle.BackColor;
                dgvProduct.Rows[i].Cells[3].Style.BackColor = normalBkColor;
                dgvProduct.Rows[i].Cells[4].Style.BackColor = normalBkColor;

                if (ret != 0)
                {
                    if (ret == 1)
                    {
                        if (simpErr.StartsWith("Invalid Concentration PDF"))
                            dgvProduct.Rows[i].Cells[3].Style.BackColor = Color.Red;
                        else if (simpErr.StartsWith("Invalid Concentration Other Company"))
                            dgvProduct.Rows[i].Cells[4].Style.BackColor = Color.Red;

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
        private void btnExport_Click(object sender, EventArgs e)
        {
            if (frmMain.selectedRepID == -1)
                return;

            string fileName = EPP_ExcelManager.SaveFileDialog();

            if (fileName.Trim() == "")
                return;

            EPP_ExcelManager.Save(dgvProduct, fileName, "Concentration", true, 1, 8, false);
        }
        //-------------------------------------------------------
        private void btnImport_Click(object sender, EventArgs e)
        {
            if (frmMain.selectedRepID == -1)
                return;

            string fileName = EPP_ExcelManager.OpenFileDialog("Open");

            if (fileName.Trim() == "")
                return;

            List<List<string>> data = new List<List<string>>();
            int columnCount = -1;
            if (!EPP_ExcelManager.Open(ref data, out columnCount, fileName, true))
                return;

            if (columnCount != 8)
            {
                InformationManager.Set_Info("Number of Data Columns Must Be Equal to 8");
                return;
            }
            //~~~~~~~~~~
            if (data.Count != dgvProduct.Rows.Count)
            {
                InformationManager.Set_Info("Mismatched Products");
                return;
            }
            //~~~~~~~~~~
            for (int i = 0; i < data.Count; i++)
            {
                bool found = false;

                for (int j = 0; j < dgvProduct.Rows.Count; j++)
                {
                    if (data[i][0] == dgvProduct.Rows[j].Cells[1].Value.ToString())//match product names
                    {
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    InformationManager.Set_Info("Mismatched Products");
                    return;
                }
            }
            //~~~~~~~~~~
            for (int i = 0; i < data.Count; i++)
            {
                for (int j = 0; j < dgvProduct.Rows.Count; j++)
                {
                    if (data[i][0] == dgvProduct.Rows[j].Cells[1].Value.ToString())
                    {
                        dgvProduct.Rows[j].Cells[3].Value = data[i][2];
                        dgvProduct.Rows[j].Cells[4].Value = data[i][3];
                        break;
                    }
                }
            }
            //~~~~~~~~~~
            InformationManager.Set_Info("Press Apply Button If Values Are OK");
        }
        //-------------------------------------------------------
    }
}
