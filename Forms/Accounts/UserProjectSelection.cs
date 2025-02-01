using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace DMR
{
    public partial class UserProjectSelection : Form
    {
        string query = "";
        //=======================================================
        public UserProjectSelection(string query)
        {
            InitializeComponent();

            this.query = query;
        }
        //-------------------------------------------------------

        private void UserProjectSelection_Load(object sender, EventArgs e)
        {
            if (!LoadProductList())
            {
                InformationManager.Set_Info(new Errors("Can not load data from data base", "", "UserProjectSelection_Load : project form selection"));
                //?????logout????
            }
        }
        //-------------------------------------------------------
        //returns:   true (success) , false(failure)
        bool LoadProductList()
        {
            dgvProjects.Rows.Clear();

            try
            {
                DataSet ds = ConnectionManager.ExecQuery(query);

                if (ds != null)
                {
                    //copy row by row to preserve header of table
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        dgvProjects.Rows.Add(ds.Tables[0].Rows[i].ItemArray);
                        dgvProjects.Rows[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dgvProjects.Rows[i].HeaderCell.Value = (i + 1).ToString();
                    }

                    ds.Dispose();
                    return true;
                }
            }
            catch (Exception)
            {
                //will be handled outside
            }

            return false;
        }
        //-------------------------------------------------------

    }
}
