using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;


namespace DBPatch
{
    public partial class frmUpdate : Form
    {
        public frmUpdate()
        {
            InitializeComponent();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            SqlConnection sqlConnction;
            SqlDataAdapter dataAdapter;

            string fileName;

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "MDF|*.mdf";
            ofd.Title = "Select DB (beside patch)";
            ofd.InitialDirectory = Application.StartupPath;

            if (ofd.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;

            fileName = ofd.SafeFileName;//note: DB file must be beside patch.exe

            sqlConnction = new SqlConnection("Data Source=.\\MSSQLSERVER2012;AttachDbFilename=|DataDirectory|" + fileName + ";Integrated Security=True;Connect Timeout=10;User Instance=True");
            //sqlConnction = new SqlConnection("Data Source=.;AttachDbFilename=|DataDirectory|" + fileName + ";Integrated Security=True;Connect Timeout=10;User Instance=True");


            dataAdapter = new SqlDataAdapter("--------", sqlConnction);

            string Query = DBPatch.Properties.Settings.Default.FixQuery;

            try
            {
                DataSet ds = new DataSet();
                dataAdapter.SelectCommand.CommandType = CommandType.Text;
                dataAdapter.SelectCommand.CommandText = Query;
                dataAdapter.Fill(ds);
                ds.Dispose();
                dataAdapter.SelectCommand.Connection.Close();
            }
            catch (Exception)
            {
                dataAdapter.SelectCommand.Connection.Close();
                dataAdapter.Dispose();
                sqlConnction.Dispose();
                MessageBox.Show("Failed to fix DB");
                Application.Exit();
            }

            dataAdapter.Dispose();
            sqlConnction.Dispose();

            MessageBox.Show("Done");
            Application.Exit();
        }
    }
}
