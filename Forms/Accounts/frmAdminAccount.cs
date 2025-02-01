using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;


namespace DMR
{
    public partial class frmAdminAccount : Form
    {
        const string CLASS_NAME = "frmAdminAccount";

        frmMain mainForm = null;
        //=======================================================
        List<string> Rankings = new List<string>();
        List<string> Roles = new List<string>();

        bool skipCellValueChanged_dgvUsers = false;
        //=======================================================
        public frmAdminAccount(frmMain mainForm)
        {
            this.mainForm = mainForm;

            InitializeComponent();
        }
        //-------------------------------------------------------
        void PrepareDgvUser()
        {
#if !OFFICE_EDITION
            return;
#endif

            skipCellValueChanged_dgvUsers = true;

            dgvUsers.Rows.Clear();

            skipCellValueChanged_dgvUsers = false;
        }
        //-------------------------------------------------------
        private void frmAdminAccount_Load(object sender, EventArgs e)
        {
#if !OFFICE_EDITION
            return;
#endif

            string METHOD = "frmAdminAccount_Load : " + CLASS_NAME;

            if (!LoadUsers())
            {
                InformationManager.Set_Info(new Errors("Can not load data from data base", "", METHOD));
                //?????logout????
            }
        }
        //-------------------------------------------------------
        //returns:   true (success) , false(failure)
        bool LoadUsers()
        {
            skipCellValueChanged_dgvUsers = true;

            dgvUsers.Rows.Clear();

            try
            {
                string query = "select ID,Active,RoleName,FirstName,LastName,CurrentStatus  from at_User order by OrderInList ";// where ID != \'Admin\' ";
                DataSet ds = ConnectionManager.ExecQuery(query);

                if (ds != null)
                {
                    //copy row by row to preserve header of table
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        dgvUsers.Rows.Add(ds.Tables[0].Rows[i].ItemArray);

                        if (ds.Tables[0].Rows[i][0].ToString().ToLower() == "admin")
                        {
                            dgvUsers.Rows[i].Cells[1].ReadOnly = true;
                            dgvUsers.Rows[i].Cells[1].Style.BackColor = Color.Gray;
                            dgvUsers.Rows[i].Cells[1].ToolTipText = "Admin";
                        }
                        else if (ds.Tables[0].Rows[i][5].ToString().ToLower() == "logged in")
                        {
                            dgvUsers.Rows[i].Cells[1].ReadOnly = true;
                            dgvUsers.Rows[i].Cells[1].Style.BackColor = Color.Yellow;
                            dgvUsers.Rows[i].Cells[1].ToolTipText = "Logged in";
                        }

                        dgvUsers.Rows[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dgvUsers.Rows[i].HeaderCell.Value = (i + 1).ToString();
                    }

                    ds.Dispose();
                    skipCellValueChanged_dgvUsers = false;
                    return true;
                }
            }
            catch (Exception)
            {
                //ErrorManager.Set_Error(new Errors("Can not load data from data base", ex.Message, " LoadProductList : product list form"));
                //will be handled outside
            }

            skipCellValueChanged_dgvUsers = false;
            return false;
        }
        //-------------------------------------------------------
        private void btnAdd_Click(object sender, EventArgs e)
        {
#if !OFFICE_EDITION
            return;
#endif

            frmAccount fac = new frmAccount(frmAccount.InteractMode.Add, "new user", mainForm);

            if (fac.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                return;

            string METHOD = "btnGeoAdd_Click : " + CLASS_NAME;

            if (!LoadUsers())
            {
                InformationManager.Set_Info(new Errors("Can not load data from data base", "", METHOD));
                //?????logout????
            }
        }
        //-------------------------------------------------------
        private void btnRefresh_Click(object sender, EventArgs e)
        {
#if !OFFICE_EDITION
            return;
#endif

            string METHOD = "btnGeoRefresh_Click : " + CLASS_NAME;

            if (!LoadUsers())
            {
                InformationManager.Set_Info(new Errors("Can not load data from data base", "", METHOD));
                //?????logout????
            }
        }
        //-------------------------------------------------------
        private void btnRemove_Click(object sender, EventArgs e)
        {
#if !OFFICE_EDITION
            return;
#endif

            if (dgvUsers.SelectedRows.Count == 0)
            {
                InformationManager.Set_Info("Select one row !!!");
                return;
            }


            int selRowID = dgvUsers.SelectedRows[0].Index;

            string userID = DGV_Operations.CellValueAsString(dgvUsers.Rows[selRowID].Cells[0]);
            bool isAdmin = (userID.ToLower() == "admin");

            if (isAdmin)
            {
                InformationManager.Set_Info("You can not remove Admin user");
                return;
            }


            if (
                MessageBox.Show("You are about to remove user ID \'" + userID.Replace("\'", "\'\'") + "\' permanently." + "\n Are you sure?",
                "Warning",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question)
                == System.Windows.Forms.DialogResult.No
                )
            {
                return;
            }

            string METHOD = "btnGeoRemove_Click : " + CLASS_NAME;

            List<ProcedureParam> prms = new List<ProcedureParam>();
            prms.Add(new ProcedureParam("@ID", userID, 50));

            string simpErr;
            Errors critErr;
            Int64 resultStat;

            int ret = Procedures.CallProcedure("prc_at_User_delete", prms, out simpErr, out critErr, out resultStat);

            if (ret != 0)
            {
                if (ret == 1)
                    InformationManager.Set_Info(simpErr);
                else
                    InformationManager.Set_Info(critErr);
            }
            else
            {
                if (!LoadUsers())
                {
                    InformationManager.Set_Info(new Errors("Can not load data from data base", "", METHOD));
                    //?????logout????
                }
            }
        }
        //-------------------------------------------------------
        private void btnEdit_Click(object sender, EventArgs e)
        {
#if !OFFICE_EDITION
            return;
#endif

            string METHOD = "btnEdit_Click : " + CLASS_NAME;

            if (dgvUsers.SelectedRows.Count == 0)
            {
                InformationManager.Set_Info("Select one user first.");
                return;
            }

            int selRowID = dgvUsers.SelectedRows[0].Index;
            string userID = DGV_Operations.CellValueAsString(dgvUsers.Rows[selRowID].Cells[0]);

            frmAccount fac = new frmAccount(frmAccount.InteractMode.Edit, userID, mainForm);

            if (fac.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                return;


            if (!LoadUsers())
            {
                InformationManager.Set_Info(new Errors("Can not load data from data base", "", METHOD));
                //?????logout????
            }

            //DGV_Operations.SelectOneRow(dgvUsers, selRowID);
        }
        //-------------------------------------------------------
        private void dgvUsers_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
#if !OFFICE_EDITION
            return;
#endif

            if (e.RowIndex == -1 || e.ColumnIndex == -1)
                return;

            if (skipCellValueChanged_dgvUsers)
                return;

            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                string userID = DGV_Operations.CellValueAsString(dgvUsers.Rows[e.RowIndex].Cells[0]);
                bool isAdmin = (userID.ToLower() == "admin");

                if (e.ColumnIndex == 1)//active
                {
                    DataGridViewCheckBoxCell ckboxCell = (DataGridViewCheckBoxCell)dgvUsers.Rows[e.RowIndex].Cells[1];
                    bool newValue = (Boolean)ckboxCell.Value;

                    if (!isAdmin)
                    {
                        List<ProcedureParam> prms = new List<ProcedureParam>();
                        prms.Add(new ProcedureParam("@ID", userID, 50));
                        prms.Add(new ProcedureParam("@activate", newValue ? "1" : "0", 1));

                        string simpErr;
                        Errors critErr;
                        Int64 resultStat;

                        int ret = Procedures.CallProcedure("prc_at_User_update_activate", prms, out simpErr, out critErr, out resultStat);

                        if (ret != 0)
                        {
                            if (ret == 1)
                                InformationManager.Set_Info(simpErr);
                            else
                                InformationManager.Set_Info(critErr);

                            return;
                        }
                    }
                }
            }
        }
        //-------------------------------------------------------
        private void dgvUsers_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
#if !OFFICE_EDITION
            return;
#endif

            //https://msdn.microsoft.com/en-us/library/system.windows.forms.datagridview.currentcelldirtystatechanged.aspx

            //note: we have check box
            if (dgvUsers.IsCurrentCellDirty)
            {
                dgvUsers.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }
        //-------------------------------------------------------
        private void dgvUsers_SelectionChanged(object sender, EventArgs e)
        {
#if !OFFICE_EDITION
            return;
#endif

            //string METHOD = "dgvUsers_SelectionChanged : " + CLASS_NAME;

            if (dgvUsers.SelectedRows.Count == 0)
                return;

            int selRowID = dgvUsers.SelectedRows[0].Index;
            string userID = DGV_Operations.CellValueAsString(dgvUsers.Rows[selRowID].Cells[0]);

            LoadProjectList(userID);
        }
        //-------------------------------------------------------
        private void btnAsnProjAdd_Click(object sender, EventArgs e)
        {
#if !OFFICE_EDITION
            return;
#endif

            string METHOD = "btnAsnProjAdd_Click : " + CLASS_NAME;

            if (dgvUsers.SelectedRows.Count == 0)
            {
                InformationManager.Set_Info("Select a none Admin user first.");
                return;
            }

            int selRowID = dgvUsers.SelectedRows[0].Index;
            string userID = DGV_Operations.CellValueAsString(dgvUsers.Rows[selRowID].Cells[0]);

            if (userID.ToLower() == "admin")
            {
                InformationManager.Set_Info("Select a none Admin user");
                return;
            }

            string query =
                    " select 0, AutoID, Name, (CASE Type WHEN 'O' THEN 'Onshore' ELSE 'Offshore' END) "
                     + " from at_Project  where AutoID not in "
                     + " (select PrjAutoID from rt_User2Proj where rt_User2Proj.UserID = \'" + userID.Replace("\'", "\'\'") + "\')";

            UserProjectSelection ups = new UserProjectSelection(query);
            ups.Text = "Project Selection for user id : " + userID;

            if (ups.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;

            List<Errors> errs = new List<Errors>();

            for (int i = 0; i < ups.dgvProjects.Rows.Count; i++)
            {
                if (Convert.ToBoolean(ups.dgvProjects.Rows[i].Cells[0].Value) == true)
                {
                    string prjID = DGV_Operations.CellValueAsString(ups.dgvProjects.Rows[i].Cells[1]);
                    string prjName = DGV_Operations.CellValueAsString(ups.dgvProjects.Rows[i].Cells[2]);


                    List<ProcedureParam> prms = new List<ProcedureParam>();
                    prms.Add(new ProcedureParam("@UserID", userID, 50));
                    prms.Add(new ProcedureParam("@PrjAutoID", prjID, ProcedureParam.ParamType.PT_Int, true, "1", false, "", "Invalid Project"));

                    string simpErr;
                    Errors critErr;
                    Int64 resultStat;

                    int ret = Procedures.CallProcedure("prc_rt_User2Proj_insert", prms, out simpErr, out critErr, out resultStat);

                    if (ret != 0)
                    {
                        if (ret == 1) critErr = new Errors(simpErr);

                        errs.Add(critErr);
                    }
                }
            }

            InformationManager.Set_Info(errs.ToArray());

            if (!LoadProjectList(userID))
            {
                InformationManager.Set_Info(new Errors("Can not load data from data base", "", METHOD));
                //?????logout????
            }
        }
        //-------------------------------------------------------
        private void btnAsnProjRemove_Click(object sender, EventArgs e)
        {
#if !OFFICE_EDITION
            return;
#endif

            string METHOD = "btnAsnProjRemove_Click : " + CLASS_NAME;

            if (dgvUsers.SelectedRows.Count == 0)
            {
                InformationManager.Set_Info("Select a none Admin user");
                return;
            }

            int selRowID = dgvUsers.SelectedRows[0].Index;
            string userID = DGV_Operations.CellValueAsString(dgvUsers.Rows[selRowID].Cells[0]);

            if (userID.ToLower() == "admin")
            {
                InformationManager.Set_Info("Select a none Admin user");
                return;
            }

            //select used Products
            string query =
                    " select 0, AutoID, Name, (CASE Type WHEN 'O' THEN 'Onshore' ELSE 'Offshore' END) "
                     + " from at_Project  where AutoID in "
                     + " (select PrjAutoID from rt_User2Proj where rt_User2Proj.UserID = \'" + userID.Replace("\'", "\'\'") + "\')";

            UserProjectSelection ups = new UserProjectSelection(query);
            ups.Text = "Project Selection for user id : " + userID;

            if (ups.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;

            List<Errors> errs = new List<Errors>();

            for (int i = 0; i < ups.dgvProjects.Rows.Count; i++)
            {
                if (Convert.ToBoolean(ups.dgvProjects.Rows[i].Cells[0].Value) == true)
                {
                    string prjID = DGV_Operations.CellValueAsString(ups.dgvProjects.Rows[i].Cells[1]);
                    string prjName = DGV_Operations.CellValueAsString(ups.dgvProjects.Rows[i].Cells[2]);


                    List<ProcedureParam> prms = new List<ProcedureParam>();
                    prms.Add(new ProcedureParam("@UserID", userID, 50));
                    prms.Add(new ProcedureParam("@PrjAutoID", prjID, ProcedureParam.ParamType.PT_Int, true, "1", false, "", "Invalid Project"));

                    string simpErr;
                    Errors critErr;
                    Int64 resultStat;

                    int ret = Procedures.CallProcedure("prc_rt_User2Proj_delete", prms, out simpErr, out critErr, out resultStat);

                    if (ret != 0)
                    {
                        if (ret == 1) critErr = new Errors(simpErr);

                        errs.Add(critErr);
                    }
                }
            }

            InformationManager.Set_Info(errs.ToArray());

            if (!LoadProjectList(userID))
            {
                InformationManager.Set_Info(new Errors("Can not load data from data base", "", METHOD));
                //?????logout????
            }
        }
        //-------------------------------------------------------
        //returns:   true (success) , false(failure)
        bool LoadProjectList(string userID)
        {
            dgvProjectsForUserID.Rows.Clear();

            try
            {
                string query = "";

                if (userID.ToLower() == "admin")
                    query = " select AutoID, Name, (CASE Type WHEN 'O' THEN 'Onshore' ELSE 'Offshore' END) from at_Project";//all projects
                else
                    query = " select at_Project.AutoID, at_Project.Name, (CASE at_Project.Type WHEN 'O' THEN 'Onshore' ELSE 'Offshore' END) "
                    + " from at_Project join rt_User2Proj on at_Project.AutoID = rt_User2Proj.PrjAutoID "
                    + " where rt_User2Proj.UserID = \'" + userID.Replace("\'", "\'\'") + "\'";

                DataSet ds = ConnectionManager.ExecQuery(query);

                if (ds != null)
                {
                    //copy row by row to preserve header of table
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        dgvProjectsForUserID.Rows.Add(ds.Tables[0].Rows[i].ItemArray);
                        dgvProjectsForUserID.Rows[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dgvProjectsForUserID.Rows[i].HeaderCell.Value = (i + 1).ToString();
                    }

                    ds.Dispose();
                    return true;
                }
            }
            catch (Exception)
            {
                //ErrorManager.Set_Error(new Errors("Can not load data from data base", ex.Message, " LoadProductList : product list form"));
                //will be handled outside
            }

            return false;
        }
        //-------------------------------------------------------

    }
}
