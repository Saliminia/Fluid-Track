using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DMR.Helpers;

namespace DMR.User_DB
{
    public partial class DGV_Filter : UserControl
    {
        //-----------------------------------------------------------

        public DGV_Filter()
        {
            InitializeComponent();
        }
        //-----------------------------------------------------------

        DataGridView dgvToFilter = null;

        [Browsable(true)]
        public DataGridView DGV_ToFilter
        {
            get { return dgvToFilter; }
            set
            {
                dgvToFilter = value;

                if (dgvToFilter != null)
                {
                    dgvToFilter.RowsAdded += new DataGridViewRowsAddedEventHandler(dgvToFilter_RowsAdded);
                    dgvToFilter.RowsRemoved += new DataGridViewRowsRemovedEventHandler(dgvToFilter_RowsRemoved);
                }
            }
        }
        //-----------------------------------------------------------
        void dgvToFilter_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            FilterRows();
        }
        //-----------------------------------------------------------
        void dgvToFilter_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            FilterRows();
        }
        //-----------------------------------------------------------

        int startColumn = 0;

        [Browsable(true)]
        public int StartColumn_ToFilter
        {
            get { return startColumn; }
            set { startColumn = value; }
        }
        //-----------------------------------------------------------

        string[] FilterLabels = null;

        [Browsable(true)]
        public string[] Labels_ToFilter
        {
            get { return FilterLabels; }
            set
            {
                FilterLabels = value;
                cboxSearchBy.Items.Clear();

                if (FilterLabels != null)
                    cboxSearchBy.Items.AddRange(FilterLabels);
            }
        }
        //-----------------------------------------------------------
        private void cboxSearchBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (chbDoSearch.Checked)
                FilterRows();
        }
        //-----------------------------------------------------------
        private void txtSearchDesc_TextChanged(object sender, EventArgs e)
        {
            if (chbDoSearch.Checked)
                FilterRows();
        }
        //-----------------------------------------------------------
        private void chbDoSearch_CheckedChanged(object sender, EventArgs e)
        {
            if (chbDoSearch.Checked)
                chbDoSearch.ImageKey = "applyFilter";
            else
                chbDoSearch.ImageKey = "noFilter";

            FilterRows();
        }
        //-------------------------------------------------------
        void FilterRows()
        {
            if (DGV_ToFilter != null)
                DGV_Operations.FilterRows_MyOrder(chbDoSearch.Checked, DGV_ToFilter, cboxSearchBy.SelectedIndex, txtSearchDesc.Text, startColumn);
        }
        //-----------------------------------------------------------
        private void DGV_Filter_Load(object sender, EventArgs e)
        {
            if (cboxSearchBy.Items.Count > 0 && cboxSearchBy.SelectedIndex < 0)
                cboxSearchBy.SelectedIndex = 0;
        }
        //-----------------------------------------------------------

    }
}
