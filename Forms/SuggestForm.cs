using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace DMR
{
    public partial class SuggestForm : Form
    {
        string query = "";
        //=======================================================
        public SuggestForm(string query)
        {
            InitializeComponent();

            this.query = query;
        }
        //-------------------------------------------------------
        private void SuggestForm_Load(object sender, EventArgs e)
        {
            if (!LoadValues())
            {
                InformationManager.Set_Info(new Errors("Can not load data from data base", "", "SuggestForm_Load"));
                //?????logout????
            }
        }
        //-------------------------------------------------------
        //returns:   true (success) , false(failure)
        bool LoadValues()
        {
            dgvValue.Rows.Clear();

            try
            {
                DataSet ds = ConnectionManager.ExecQuery(query);

                if (ds != null)
                {
                    //copy row by row to preserve header of table
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        dgvValue.Rows.Add(ds.Tables[0].Rows[i].ItemArray);
                        dgvValue.Rows[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dgvValue.Rows[i].HeaderCell.Value = (i + 1).ToString();
                    }

                    ds.Dispose();
                    return true;
                }
            }
            catch (Exception)
            {
                //ErrorManager.Set_Error(new Errors("Can not load data from data base", ex.Message, " LoadValues"));
                //will be handled outside
            }

            return false;
        }
        //-------------------------------------------------------

    }
}
