using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace DMR
{
    public partial class MudVolManTotalWellSelForMergeForm : Form
    {
        Int64 mudDfsID = 0;
        //=======================================================
        public MudVolManTotalWellSelForMergeForm(Int64 muddfsID)
        {
            InitializeComponent();

            this.mudDfsID = muddfsID;
        }
        //-------------------------------------------------------
        private void MudVolManTotalWellSelForMergeForm_Load(object sender, EventArgs e)
        {
            if (!LoadDFSs())
            {
                //ErrorManager.Set_Error(new Errors("Can not load data from data base", "", METHOD));
                //?????logout????
            }
        }
        //-------------------------------------------------------
        bool LoadDFSs()
        {
            dgvDFS.Rows.Clear();

            try
            {
                string query =
                   " select ID, Dfs_AutoID, d.DrillingFluidSystem "
                    + " from rt_Rep2MudVolManDFS left join lt_DrillingFluidSystem d on Dfs_AutoID = d.AutoID "
                    + " where ReportID =  " + frmMain.selectedRepID.ToString()
                    + " and  ID <> " + mudDfsID.ToString()
                    + " and  ID not in "
                    + " ( "
                    + "     select MudVolManDFS_ToBeMerged_ID "
                    + "     from rt_Rep2MudVolManDFS_TotalWellVolMerge "
                    + "     where MudVolManDFS_ID = " + mudDfsID.ToString()
                    + " ) ";

                DataSet ds = ConnectionManager.ExecQuery(query);

                if (ds != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        dgvDFS.Rows.Add(ds.Tables[0].Rows[i].ItemArray);
                        dgvDFS.Rows[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dgvDFS.Rows[i].HeaderCell.Value = (i + 1).ToString();
                    }

                    ds.Dispose();
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                //ErrorManager.Set_Error(new Errors("Can not load data from data base", ex.Message, METHOD));
                //will be handled outside
            }

            return true;
        }
        //-------------------------------------------------------
    }
}
