using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DMR.Helpers;

namespace DMR.Helpers.DGV
{
    public delegate void ButtonClick();
    //-----------------------------------------------------------
    public partial class ListFormSelectionButtons : UserControl
    {
        public ListFormSelectionButtons()
        {
            InitializeComponent();
        }
        //-----------------------------------------------------------
        bool showOKCancelBtns = true;

        [Browsable(true)]
        public bool ShowOKCancelButtons
        {
            get { return showOKCancelBtns; }

            set
            {
                showOKCancelBtns = value;

                if (!showOKCancelBtns)
                {
                    btnOK.Visible = false;
                    btnCancel.Visible = false;
                }
                else
                {
                    btnOK.Visible = true;
                    btnCancel.Visible = true;
                }
            }
        }
        //-----------------------------------------------------------
        bool showSelBtns = true;

        [Browsable(true)]
        public bool ShowSelectionButtons
        {
            get { return showSelBtns; }

            set
            {
                showSelBtns = value;

                if (!showSelBtns)
                {
                    btnSelectAll.Visible = false;
                    btnInvertSelection.Visible = false;
                    btnUnselectAll.Visible = false;
                }
                else
                {
                    btnSelectAll.Visible = true;
                    btnInvertSelection.Visible = true;
                    btnUnselectAll.Visible = true;
                }
            }
        }
        //-----------------------------------------------------------
        DataGridView dgvToSelect = null;

        [Browsable(true)]
        public DataGridView DGV_ToSelect
        {
            get { return dgvToSelect; }
            set { dgvToSelect = value; }
        }
        //-----------------------------------------------------------
        private void btnInvertSelection_Click(object sender, EventArgs e)
        {
            if (dgvToSelect == null || !showSelBtns)
                return;

            DGV_Operations.InvertAllCheckBoxes(dgvToSelect);
        }
        //-----------------------------------------------------------
        private void btnUnselectAll_Click(object sender, EventArgs e)
        {
            if (dgvToSelect == null || !showSelBtns)
                return;

            DGV_Operations.UnselectAllCheckBoxes(dgvToSelect);
        }
        //-----------------------------------------------------------
        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            if (dgvToSelect == null || !showSelBtns)
                return;

            DGV_Operations.SelectAllCheckBoxes(dgvToSelect);
        }
        //-----------------------------------------------------------

    }
}
