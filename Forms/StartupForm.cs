using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DMR.User_DB;

namespace DMR
{
    public partial class StartupForm : Form, IMyForm
    {
        RoleAccess access = null;
        frmMain mainForm = null;

        //=======================================================
        void IMyForm.OnCurrentProjectChanged() {/*Nothing*/}
        //-------------------------------------------------------
        void IMyForm.OnCurrentRigWellChanged() {/*Nothing*/}
        //-------------------------------------------------------
        void IMyForm.OnCurrentReportChanged() {/*Nothing*/}
        //-------------------------------------------------------
        void IMyForm.OnInit()
        {
            //~~~~~~~~~~~~~~~~~ AT_None = Hide ~~~~~~~~~~~~~~~~~~~~
            if (access.generalDBs == RoleAccess.AcessTypes.AT_None &&
                access.preDefValues == RoleAccess.AcessTypes.AT_None)
            {
                pnlButtons.Visible = false;
            }
            else
            {
                if (access.generalDBs == RoleAccess.AcessTypes.AT_None)
                {
                    btnProductList.Visible = false;
                    btnDrillPipeData.Visible = false;
                    btnDrillCollarData.Visible = false;
                    btnHeavyWeightPipe.Visible = false;
                    btnCasingData.Visible = false;
                    btnLabEquipmentList.Visible = false;
                    btnHoleSize.Visible = false;
                    btnGeologyOfIran.Visible = false;
                    btnIADC.Visible = false;
                    btnDrillingFluidSystem.Visible = false;
                    btnPersonnelList.Visible = false;
                    btnHSEPPE.Visible = false;
                    btnGeneralEquipment.Visible = false;
                }

                if (access.preDefValues == RoleAccess.AcessTypes.AT_None)
                    btnPredefValues.Visible = false;
            }
            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        }
        //-------------------------------------------------------
        void IMyForm.OnCurrentUnitsChanged()
        {
            //??????????????????????????

            UpdateComputationsByPressRefereshButtons();
        }
        //-------------------------------------------------------
        void UpdateComputationsByPressRefereshButtons()
        {
        }
        //-------------------------------------------------------
        public StartupForm(RoleAccess access, frmMain mainForm)
        {
            this.access = access;
            this.mainForm = mainForm;

            InitializeComponent();
        }
        //-------------------------------------------------------
        private void btnProductList_Click(object sender, EventArgs e)
        {
            RoleAccess.AcessTypes accessType = access.generalDBs;

            if (accessType == RoleAccess.AcessTypes.AT_None)
                return;

            productListForm gdb = new productListForm();

            if (accessType == RoleAccess.AcessTypes.AT_ReadOnly)
                gdb.controlBtns.HideEditingButtons = true;

            gdb.ShowDialog(this);
        }
        //-------------------------------------------------------
        private void btnDrillPipeData_Click(object sender, EventArgs e)
        {
            RoleAccess.AcessTypes accessType = access.generalDBs;

            if (accessType == RoleAccess.AcessTypes.AT_None)
                return;

            DrillPipeDataForm gdb = new DrillPipeDataForm();

            if (accessType == RoleAccess.AcessTypes.AT_ReadOnly)
                gdb.controlBtns.HideEditingButtons = true;

            gdb.ShowDialog(this);
        }
        //-------------------------------------------------------
        private void btnDrillCollarData_Click(object sender, EventArgs e)
        {
            RoleAccess.AcessTypes accessType = access.generalDBs;

            if (accessType == RoleAccess.AcessTypes.AT_None)
                return;

            DrillCollarDataForm gdb = new DrillCollarDataForm();

            if (accessType == RoleAccess.AcessTypes.AT_ReadOnly)
                gdb.controlBtns.HideEditingButtons = true;

            gdb.ShowDialog(this);
        }
        //-------------------------------------------------------
        private void btnHeavyWeightPipe_Click(object sender, EventArgs e)
        {
            RoleAccess.AcessTypes accessType = access.generalDBs;

            if (accessType == RoleAccess.AcessTypes.AT_None)
                return;

            HeavyWeightFormDB gdb = new HeavyWeightFormDB();

            if (accessType == RoleAccess.AcessTypes.AT_ReadOnly)
                gdb.controlBtns.HideEditingButtons = true;

            gdb.ShowDialog(this);
        }
        //-------------------------------------------------------
        private void btnCasingData_Click(object sender, EventArgs e)
        {
            RoleAccess.AcessTypes accessType = access.generalDBs;

            if (accessType == RoleAccess.AcessTypes.AT_None)
                return;

            CasingDataForm gdb = new CasingDataForm();

            if (accessType == RoleAccess.AcessTypes.AT_ReadOnly)
                gdb.controlBtns.HideEditingButtons = true;

            gdb.ShowDialog(this);
        }
        //-------------------------------------------------------
        private void btnLabEquipmentList_Click(object sender, EventArgs e)
        {
            RoleAccess.AcessTypes accessType = access.generalDBs;

            if (accessType == RoleAccess.AcessTypes.AT_None)
                return;

            LabEquipmentListFormDB gdb = new LabEquipmentListFormDB();

            if (accessType == RoleAccess.AcessTypes.AT_ReadOnly)
                gdb.controlBtns.HideEditingButtons = true;

            gdb.ShowDialog(this);
        }
        //-------------------------------------------------------
        private void btnHoleSize_Click(object sender, EventArgs e)
        {
            RoleAccess.AcessTypes accessType = access.generalDBs;

            if (accessType == RoleAccess.AcessTypes.AT_None)
                return;

            HoleSizeFormDB gdb = new HoleSizeFormDB();

            if (accessType == RoleAccess.AcessTypes.AT_ReadOnly)
                gdb.controlBtns.HideEditingButtons = true;

            gdb.ShowDialog(this);
        }
        //-------------------------------------------------------
        private void btnGeologyOfIran_Click(object sender, EventArgs e)
        {
            RoleAccess.AcessTypes accessType = access.generalDBs;

            if (accessType == RoleAccess.AcessTypes.AT_None)
                return;

            GeologyOfIranFormDB gdb = new GeologyOfIranFormDB
                (
                accessType == RoleAccess.AcessTypes.AT_WriteAndRead //to be sure
                 && mainForm.curUserRole.userID.ToLower() == "admin"
                );

            if (accessType == RoleAccess.AcessTypes.AT_ReadOnly)
            {
                gdb.controlBtns.HideEditingButtons = true;

                gdb.lblFieldName.Visible = false;
                gdb.txtFieldName.Visible = false;
                gdb.btnAddField.Visible = false;
                gdb.btnRanameField.Visible = false;
            }

            gdb.ShowDialog(this);
        }
        //-------------------------------------------------------
        private void btnIADC_Click(object sender, EventArgs e)
        {
            RoleAccess.AcessTypes accessType = access.generalDBs;

            if (accessType == RoleAccess.AcessTypes.AT_None)
                return;

            IADCFormDB gdb = new IADCFormDB();

            if (accessType == RoleAccess.AcessTypes.AT_ReadOnly)
                gdb.controlBtns.HideEditingButtons = true;

            gdb.ShowDialog(this);
        }
        //-------------------------------------------------------
        private void btnDrillingFluidSystem_Click(object sender, EventArgs e)
        {
            RoleAccess.AcessTypes accessType = access.generalDBs;

            if (accessType == RoleAccess.AcessTypes.AT_None)
                return;

            DrillingFluidSystemFormDB gdb = new DrillingFluidSystemFormDB();

            if (accessType == RoleAccess.AcessTypes.AT_ReadOnly)
                gdb.controlBtns.HideEditingButtons = true;

            gdb.ShowDialog(this);
        }
        //-------------------------------------------------------
        private void btnPersonnelList_Click(object sender, EventArgs e)
        {
            RoleAccess.AcessTypes accessType = access.generalDBs;

            if (accessType == RoleAccess.AcessTypes.AT_None)
                return;

            PersonnelListDataFormDB gdb = new PersonnelListDataFormDB();

            if (accessType == RoleAccess.AcessTypes.AT_ReadOnly)
                gdb.controlBtns.HideEditingButtons = true;

            gdb.ShowDialog(this);
        }
        //-------------------------------------------------------
        private void btnHSEPPE_Click(object sender, EventArgs e)
        {
            RoleAccess.AcessTypes accessType = access.generalDBs;

            if (accessType == RoleAccess.AcessTypes.AT_None)
                return;

            HSE_PPE_DataFormDB gdb = new HSE_PPE_DataFormDB(); ;

            if (accessType == RoleAccess.AcessTypes.AT_ReadOnly)
                gdb.controlBtns.HideEditingButtons = true;

            gdb.ShowDialog(this);
        }
        //-------------------------------------------------------
        private void btnGeneralEquipment_Click(object sender, EventArgs e)
        {
            RoleAccess.AcessTypes accessType = access.generalDBs;

            if (accessType == RoleAccess.AcessTypes.AT_None)
                return;

            GeneralEquipmentInventoryDataForm gdb = new GeneralEquipmentInventoryDataForm();

            if (accessType == RoleAccess.AcessTypes.AT_ReadOnly)
                gdb.controlBtns.HideEditingButtons = true;

            gdb.ShowDialog(this);
        }
        //-------------------------------------------------------
        private void btnPredefValues_Click(object sender, EventArgs e)
        {
            RoleAccess.AcessTypes accessType = access.preDefValues;

            if (accessType == RoleAccess.AcessTypes.AT_None)
                return;

            PredefinedValuesFormDB pdv = new PredefinedValuesFormDB();

            if (accessType == RoleAccess.AcessTypes.AT_ReadOnly)
                pdv.controlBtns.HideEditingButtons = true;

            pdv.ShowDialog(this);
        }

        private void StartupForm_Load(object sender, EventArgs e)
        {
#if !NOLOCK
            //Security String (Fetch string value from HLock)
            object obj1 = new object();
            uint RProg = (uint)Program.Rand.Next(2147483647);
            obj1 = Program.arm.GetARMData(ARM.IWhichData.STRING_VAL1, RProg);
            string strVal1 = Program.cls.DecodData(obj1, ARM.IWhichData.STRING_VAL1, RProg);
            if (strVal1.Trim().Substring(0, 35) != "rre75revgdsur4edcg234583sdg1pdwer67")
            {
                MessageBox.Show("Connecting to Security Key Lock failed. Please plug security lock.", "PDF DMR System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.LockID = string.Empty;
                this.Close();
            }
#endif
        }
        //-------------------------------------------------------

    }
}
