using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DMR.Helpers
{
    public delegate void ButtonClick();
    //-----------------------------------------------------------
    public partial class ListFormButtons : UserControl
    {
        public event ButtonClick BtnRefreshClick;
        public event ButtonClick BtnAddClick;
        public event ButtonClick BtnApplyChangesClick;
        public event ButtonClick BtnClearFieldsClick;
        public event ButtonClick BtnExportClick;
        public event ButtonClick BtnImportClick;

        //-----------------------------------------------------------
        public ListFormButtons()
        {
            InitializeComponent();
        }
        //-----------------------------------------------------------
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (BtnRefreshClick != null)
                BtnRefreshClick();
        }
        //-----------------------------------------------------------
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (hideEditingButtons)
                return;

            if (BtnAddClick != null)
                BtnAddClick();
        }
        //-----------------------------------------------------------
        private void btnApplyChanges_Click(object sender, EventArgs e)
        {
            if (hideEditingButtons)
                return;

            if (BtnApplyChangesClick != null)
                BtnApplyChangesClick();
        }
        //-----------------------------------------------------------
        private void btnClearFields_Click(object sender, EventArgs e)
        {
            if (hideEditingButtons)
                return;

            if (BtnClearFieldsClick != null)
                BtnClearFieldsClick();
        }
        //-----------------------------------------------------------
        private void btnExport_Click(object sender, EventArgs e)
        {
            if (hideExportImportButtons)
                return;

            if (BtnExportClick != null)
                BtnExportClick();
        }
        //-----------------------------------------------------------
        private void btnImport_Click(object sender, EventArgs e)
        {
            if (hideEditingButtons)
                return;

            if (hideExportImportButtons)
                return;

            if (BtnImportClick != null)
                BtnImportClick();
        }
        //-----------------------------------------------------------
        bool applyBtnCheck = true;

        [Browsable(true)]
        public bool ApplyButtontnCheck
        {
            get { return applyBtnCheck; }
            set
            {
                applyBtnCheck = value;
                if (applyBtnCheck)
                {
                    btnApplyChanges.ImageKey = "check";
                    btnApplyChanges.BackColor = frmMain.checkColor;
                }
                else
                {
                    btnApplyChanges.ImageKey = "warning";
                    btnApplyChanges.BackColor = frmMain.warningColor;
                }
            }
        }
        //-----------------------------------------------------------
        bool hideEditingButtons = false;

        [Browsable(true)]
        public bool HideEditingButtons
        {
            get { return hideEditingButtons; }
            set
            {
                hideEditingButtons = value;

                if (hideEditingButtons)
                {
                    btnAdd.Visible = false;
                    btnApplyChanges.Visible = false;
                    btnClearFields.Visible = false;
                    btnImport.Visible = false;
                }
                else
                {
                    btnAdd.Visible = true;
                    btnApplyChanges.Visible = true;
                    btnClearFields.Visible = true;
                    btnImport.Visible = true;
                }
            }
        }
        //-----------------------------------------------------------
        bool hideExportImportButtons = false;

        [Browsable(true)]
        public bool HideExportImportButtons
        {
            get { return hideExportImportButtons; }
            set
            {
                hideExportImportButtons = value;

                if (hideExportImportButtons)
                {
                    btnImport.Visible = false;
                    btnExport.Visible = false;
                }
                else
                {
                    btnImport.Visible = true;
                    btnExport.Visible = true;
                }
            }
        }
        //-----------------------------------------------------------
    }
}
