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
    public partial class MudPropCol_Selection : Form
    {
        //=======================================================
        public MudPropCol_Selection()
        {
            InitializeComponent();
        }
        //-------------------------------------------------------
        private void MudPropCol_Selection_Load(object sender, EventArgs e)
        {
            if (!LoadMudPropCol())
            {
                InformationManager.Set_Info(new Errors("Can not load data from data base", "", "MudPropCol_Selection_Load "));
                //?????logout????
            }
        }
        //-------------------------------------------------------
        //returns:   true (success) , false(failure)
        bool LoadMudPropCol()
        {
            dgvMudPropCol.Rows.Clear();

            try
            {
                string query =
                      " select col.ID, CheckTime, d.DrillingFluidSystem, samplePit_ID "
                    + " from rt_Rep2MudProp_MudPropCol col join rt_Rep2MudPropPeriod  per on col.MudPropPeriod_ID = per.ID "
                    + " left join lt_DrillingFluidSystem d on col.Dfs_AutoID = d.AutoID "
                    + " where per.ReportID =  " + frmMain.selectedRepID;

                DataSet ds = ConnectionManager.ExecQuery(query);

                if (ds != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        dgvMudPropCol.Rows.Add(ds.Tables[0].Rows[i].ItemArray);
                        dgvMudPropCol.Rows[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dgvMudPropCol.Rows[i].HeaderCell.Value = (i + 1).ToString();

                        //--------
                        if (ds.Tables[0].Rows[i][3] == DBNull.Value)
                        {
                            dgvMudPropCol.Rows[i].Cells[4].Value = "Flow Line";
                        }
                        else
                        {
                            Int64 sampPitID = Convert.ToInt64(ds.Tables[0].Rows[i][3]);

                            int sampPitNum = 0;
                            string pitName;
                            FetchTableData.GetPitNameAndNumber(sampPitID, out pitName, out sampPitNum);

                            string pitNumAndName = pitName + " #" + sampPitNum.ToString();

                            dgvMudPropCol.Rows[i].Cells[4].Value = pitNumAndName;
                        }
                        //--------
                    }

                    ds.Dispose();
                    return true;
                }
            }
            catch (Exception)
            {
                //ErrorManager.Set_Error(new Errors("Can not load data from data base", ex.Message, " Load"));
                //will be handled outside
            }

            return false;
        }
        //-------------------------------------------------------
    }
}
