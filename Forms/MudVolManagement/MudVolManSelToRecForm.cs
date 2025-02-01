using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace DMR
{
    public partial class MudVolManSelToRecForm : Form
    {
        string query = "";
        //=======================================================
        public MudVolManSelToRecForm(string query)
        {
            InitializeComponent();

            this.query = query;
        }
        //-------------------------------------------------------
        private void MudVolManSelToRecForm_Load(object sender, EventArgs e)
        {
            if (!LoadTransRecord())
            {
                InformationManager.Set_Info(new Errors("Can not load data from data base", "", "MudVolManSelToRecForm_Load"));
                //?????logout????
            }
        }
        //-------------------------------------------------------
        //returns:   true (success) , false(failure)
        bool LoadTransRecord()
        {
            dgvTransRecord.Rows.Clear();

            try
            {
                DataSet ds = ConnectionManager.ExecQuery(query);

                if (ds != null)
                {
                    //copy row by row to preserve header of table
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        dgvTransRecord.Rows.Add(ds.Tables[0].Rows[i].ItemArray);
                        dgvTransRecord.Rows[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dgvTransRecord.Rows[i].HeaderCell.Value = (i + 1).ToString();
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
