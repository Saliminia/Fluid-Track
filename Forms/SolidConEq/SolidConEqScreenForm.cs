using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace DMR
{
    public partial class SolidConEqScreenForm : Form
    {
        const string CLASS_NAME = "SolidConEqScreenForm";

        Int64 r2SC_ID = -1;
        //=======================================================
        public SolidConEqScreenForm(Int64 r2SC_ID)
        {
            InitializeComponent();

            this.r2SC_ID = r2SC_ID;
        }
        //-------------------------------------------------------
        private void SolidConEqScreenForm_Load(object sender, EventArgs e)
        {
            string METHOD = "SolidConEqScreenForm_Load : " + CLASS_NAME;

            if (!LoadScreenSizes())
            {
                InformationManager.Set_Info(new Errors("Can not load data from data base", "", METHOD));
                //?????logout????
            }
        }
        //-------------------------------------------------------
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string METHOD = "btnGeoAdd_Click : " + CLASS_NAME;

            string query =
              " select ss.ID, 0, p.Value  "
            + " from rt_Rep2SolidControl rs join at_Report rep on rep.ID = rs.ReportID "
            + " join rt_Rig2Well rw on rep.RigWellID = rw.ID "
            + " join at_Rig rig on rig.ID = rw.RigID "
            + " join rt_Rig2ShaleShaker ss on rig.ID = ss.RigID "
            + " left join lt_PredefValues p on p.AutoID = ScreenSize_PredefAutoID "
            + " where  rs.ID = " + r2SC_ID.ToString();


            PredefinedValuesFormDB_Selection pdSel = new PredefinedValuesFormDB_Selection(query, false);
            DataGridViewCheckBoxColumn sel = new DataGridViewCheckBoxColumn();
            pdSel.dgvValue.Columns.Insert(1, sel);

            if (pdSel.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;

            List<Errors> errs = new List<Errors>();
            for (int i = 0; i < pdSel.dgvValue.Rows.Count; i++)
            {
                if (Convert.ToBoolean(pdSel.dgvValue.Rows[i].Cells[1].Value) == true)
                {

                    //string ssVal = DGV_Operations.CellValueAsString(pdSel.dgvValue.Rows[pdSel.dgvValue.SelectedRows[0].Index].Cells[1]);
                    string ssId = DGV_Operations.CellValueAsString(pdSel.dgvValue.Rows[i].Cells[0]);

                    List<ProcedureParam> prms = new List<ProcedureParam>();
                    prms.Add(new ProcedureParam("@R2SC_ID", r2SC_ID.ToString(), ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Solid Control Record"));
                    prms.Add(new ProcedureParam("@ShaleShaker_ID", ssId.ToString(), ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Shale Shaker Screen Size"));
                    prms.Add(new ProcedureParam("@ProjAutoID", frmMain.selectedPrjID.ToString(), ProcedureParam.ParamType.PT_Int, true, "1", false, "", "Invalid Project"));

                    string simpErr;
                    Errors critErr;
                    Int64 resultStat;

                    int ret = Procedures.CallProcedure("prc_rt_Rep2SolidControlScreen_insert", prms, out simpErr, out critErr, out resultStat);


                    if (ret != 0)
                    {
                        if (ret == 1) critErr = new Errors(simpErr);

                        errs.Add(critErr);
                    }
                }
            }

            InformationManager.Set_Info(errs.ToArray());

            if (!LoadScreenSizes())
            {
                InformationManager.Set_Info(new Errors("Can not load data from data base", "", METHOD));
                //?????logout????
            }

        }
        //-------------------------------------------------------
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            string METHOD = "btnGeoAdd_Click : " + CLASS_NAME;

            if (!LoadScreenSizes())
            {
                InformationManager.Set_Info(new Errors("Can not load data from data base", "", METHOD));
                //?????logout????
            }
        }
        //-------------------------------------------------------
        private void btnRemove_Click(object sender, EventArgs e)
        {
            string METHOD = "btnGeoRemove_Click : " + CLASS_NAME;

            if (dgvScreenSize.SelectedRows.Count == 0)
            {
                InformationManager.Set_Info("Select One Row to be Deleted");
                return;
            }

            string id = DGV_Operations.CellValueAsString(dgvScreenSize.Rows[dgvScreenSize.SelectedRows[0].Index].Cells[0]);

            List<ProcedureParam> prms = new List<ProcedureParam>();
            prms.Add(new ProcedureParam("@ID", id, ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Solid Control Screen Screen"));

            string simpErr;
            Errors critErr;
            Int64 resultStat;

            int ret = Procedures.CallProcedure("prc_rt_Rep2SolidControlScreen_delete", prms, out simpErr, out critErr, out resultStat);

            if (ret != 0)
            {
                if (ret == 1)
                    InformationManager.Set_Info(simpErr);
                else
                    InformationManager.Set_Info(critErr);

                return;
            }


            if (!LoadScreenSizes())
            {
                InformationManager.Set_Info(new Errors("Can not load data from data base", "", METHOD));
                //?????logout????
            }
        }
        //-------------------------------------------------------
        //returns:   true (success) , false(failure)
        public bool LoadScreenSizes()
        {
            dgvScreenSize.Rows.Clear();

            try
            {
                string query =
                    " select rss.ID, p.Value "
                    + " from rt_Rep2SolidControlScreen rss join rt_Rig2ShaleShaker ss  on rss.ShaleShaker_ID = ss.ID "
                    + " left join lt_PredefValues p on ss.ScreenSize_PredefAutoID = p.AutoID "
                    + " where rss.R2SC_ID = " + r2SC_ID.ToString()
                    + " order by rss.UserOrderInGroup ";

                DataSet ds = ConnectionManager.ExecQuery(query);

                if (ds != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        dgvScreenSize.Rows.Add(ds.Tables[0].Rows[i].ItemArray);

                        dgvScreenSize.Rows[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dgvScreenSize.Rows[i].HeaderCell.Value = (i + 1).ToString();
                    }

                    ds.Dispose();
                    return true;
                }
            }
            catch (Exception)
            {
                //ErrorManager.Set_Error(new Errors("Can not load data from data base", ex.Message, METHOD));
                //will be handled outside
            }

            return false;
        }
        //-------------------------------------------------------
        private void btnUp_Click(object sender, EventArgs e)
        {
            if (dgvScreenSize.SelectedRows.Count != 1)
                return;

            int rowSelIndex = dgvScreenSize.SelectedRows[0].Index;

            if (rowSelIndex == 0)
                return;

            string id = DGV_Operations.CellValueAsString(dgvScreenSize.SelectedRows[0].Cells[0]);

            List<ProcedureParam> prms = new List<ProcedureParam>();
            prms.Add(new ProcedureParam("@ID", id, ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Record"));
            prms.Add(new ProcedureParam("@DecInc", "-1", ProcedureParam.ParamType.PT_Int, false, "", false, "", ""));

            string simpErr;
            Errors critErr;
            Int64 resultStat;

            int ret = Procedures.CallProcedure("prc_rt_Rep2SolidControlScreen_ShiftUserOrderInGroup", prms, out simpErr, out critErr, out resultStat);

            if (ret != 0)
            {
                if (ret == 1)
                    InformationManager.Set_Info(simpErr);
                else
                    InformationManager.Set_Info(critErr);

                return;
            }

            if (!LoadScreenSizes())
            {
                //?????logout????
            }

            DGV_Operations.SelectOneRow(dgvScreenSize, rowSelIndex - 1);
        }
        //-------------------------------------------------------
        private void btnDown_Click(object sender, EventArgs e)
        {
            if (dgvScreenSize.SelectedRows.Count != 1)
                return;

            int rowSelIndex = dgvScreenSize.SelectedRows[0].Index;

            if (rowSelIndex == dgvScreenSize.Rows.Count - 1)
                return;

            string id = DGV_Operations.CellValueAsString(dgvScreenSize.SelectedRows[0].Cells[0]);

            List<ProcedureParam> prms = new List<ProcedureParam>();
            prms.Add(new ProcedureParam("@ID", id, ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Record"));
            prms.Add(new ProcedureParam("@DecInc", "1", ProcedureParam.ParamType.PT_Int, false, "", false, "", ""));

            string simpErr;
            Errors critErr;
            Int64 resultStat;

            int ret = Procedures.CallProcedure("prc_rt_Rep2SolidControlScreen_ShiftUserOrderInGroup", prms, out simpErr, out critErr, out resultStat);

            if (ret != 0)
            {
                if (ret == 1)
                    InformationManager.Set_Info(simpErr);
                else
                    InformationManager.Set_Info(critErr);

                return;
            }

            if (!LoadScreenSizes())
            {
                //?????logout????
            }

            DGV_Operations.SelectOneRow(dgvScreenSize, rowSelIndex + 1);
        }
        //-------------------------------------------------------

    }
}
