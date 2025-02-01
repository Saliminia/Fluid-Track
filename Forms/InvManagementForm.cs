using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using DMR.Helpers;
using System.Text;

using System.Windows.Forms;
using DMR.User_DB;

namespace DMR
{
    public partial class InvManagementForm : Form, IMyForm
    {
        const string CLASS_NAME = "InvManagementForm";
        RoleAccess access = null;
        frmMain mainForm = null;
        //=======================================================
        bool skipCellValueChanged_dgvPrdInv = false;
        bool skipCellValueChanged_dgvRigEqShale = false;
        bool skipCellValueChanged_dgvRigEqBulk = false;
        bool skipCellValueChanged_dgvRigEqPit = false;
        bool skipCellValueChanged_dgvLabEqInv = false;
        bool skipCellValueChanged_dgvHSE = false;
        bool skipCellValueChanged_dgvGeneralEqInv = false;
        //=======================================================
        void IMyForm.OnCurrentProjectChanged()
        {
            CleanVolatileParts();
            ResetApplyBtns();
        }
        //-------------------------------------------------------
        void IMyForm.OnCurrentRigWellChanged()
        {
            CleanVolatileParts();
            ResetApplyBtns();
        }
        //-------------------------------------------------------
        void IMyForm.OnCurrentReportChanged()
        {
            string METHOD = "OnCurrentReportChanged : " + CLASS_NAME;

            if (frmMain.selectedRepID != -1)
            {
                if (!LoadProductInv())
                {
                    InformationManager.Set_Info(new Errors("Can not load data from data base", "", METHOD));
                    //?????logout????
                    return;
                }

                if (!LoadRigEquipmentsShale())
                {
                    InformationManager.Set_Info(new Errors("Can not load data from data base", "", METHOD));
                    //?????logout????
                    return;
                }

                if (!LoadRigEquipmentsBulk())
                {
                    InformationManager.Set_Info(new Errors("Can not load data from data base", "", METHOD));
                    //?????logout????
                    return;
                }

                if (!LoadRigEquipmentsPit())
                {
                    InformationManager.Set_Info(new Errors("Can not load data from data base", "", METHOD));
                    //?????logout????
                    return;
                }

                LoadManPowerTab();

                if (!LoadLabEquipment())
                {
                    InformationManager.Set_Info(new Errors("Can not load data from data base", "", METHOD));
                    //?????logout????
                    return;
                }

                if (!LoadHSEPPE())
                {
                    InformationManager.Set_Info(new Errors("Can not load data from data base", "", METHOD));
                    //?????logout????
                    return;
                }

                if (!LoadGeneralEq())
                {
                    InformationManager.Set_Info(new Errors("Can not load data from data base", "", METHOD));
                    //?????logout????
                    return;
                }
            }

            ResetApplyBtns();
        }
        //-------------------------------------------------------
        void IMyForm.OnInit()
        {
            //~~~~~~~~~~~~~~~~~ AT_None = Hide ~~~~~~~~~~~~~~~~~~~~
            if (access.inventoryManagement_ProductInventory == RoleAccess.AcessTypes.AT_None)
            {
                tcInvMan.TabPages.Remove(tpProductInv);
            }
            else if (access.inventoryManagement_ProductInventory == RoleAccess.AcessTypes.AT_ReadOnly)
            {
                dgvProductInv.ReadOnly = true;
                btnPrdInvApply.Enabled = false;
            }

            DGV_Operations.ColorizeFixedColumnHeader(dgvProductInv, 1, 2);
            DGV_Operations.ColorizeNestedColumnHeader(dgvProductInv, 3, 10, 16, 24);
            DGV_Operations.ColorizeComputedColumnHeader(dgvProductInv, 32);
            DGV_Operations.HandleKeyDown(dgvProductInv, dgvProductInv_CellMouseClick);


            if (access.inventoryManagement_RigEquipmentInventory == RoleAccess.AcessTypes.AT_None)
            {
                tcInvMan.TabPages.Remove(tpRigEquipment);
            }
            else if (access.inventoryManagement_RigEquipmentInventory == RoleAccess.AcessTypes.AT_ReadOnly)
            {
                dgvRigEquipmentShale.ReadOnly = true;
                btnRigEqShaleApply.Enabled = false;

                dgvRigEquipmentBulk.ReadOnly = true;
                btnRigEqBulkApply.Enabled = false;

                dgvRigEquipmentPit.ReadOnly = true;
                btnRigEqPitAdd.Enabled = false;
                btnRigEqPitApply.Enabled = false;
                btnRigEqPitRemove.Enabled = false;
            }

            DGV_Operations.ColorizeFixedColumnHeader(dgvRigEquipmentShale, 1);
            DGV_Operations.HandleKeyDown(dgvRigEquipmentShale, null);

            DGV_Operations.ColorizeNestedColumnHeader(dgvRigEquipmentBulk, 2);
            DGV_Operations.ColorizeFixedColumnHeader(dgvRigEquipmentBulk, 1, 4);
            DGV_Operations.HandleKeyDown(dgvRigEquipmentBulk, dgvRigEquipmentBulk_CellMouseClick);


            DGV_Operations.ColorizeNestedColumnHeader(dgvRigEquipmentPit, 2);
            DGV_Operations.ColorizeFixedColumnHeader(dgvRigEquipmentPit, 1, 4);
            DGV_Operations.HandleKeyDown(dgvRigEquipmentPit, dgvRigEquipmentPit_CellMouseClick);

            if (access.inventoryManagement_ManPower == RoleAccess.AcessTypes.AT_None)
            {
                tcInvMan.TabPages.Remove(tpManPow);
            }
            else if (access.inventoryManagement_ManPower == RoleAccess.AcessTypes.AT_ReadOnly)
            {
                grbManPowPersonelInfo.Enabled = false;
                grbManPowProjSup.Enabled = false;
                grbManPowSupBase.Enabled = false;
                btnManPowApply.Enabled = false;
            }

            if (access.inventoryManagement_LabEquipmentInventory == RoleAccess.AcessTypes.AT_None)
            {
                tcInvMan.TabPages.Remove(tpLabEqInv);
            }
            else if (access.inventoryManagement_LabEquipmentInventory == RoleAccess.AcessTypes.AT_ReadOnly)
            {
                dgvLabEqInv.ReadOnly = true;
                btnLabEqInvAdd.Enabled = false;
                btnLabEqInvApply.Enabled = false;
                btnLabEqInvRemove.Enabled = false;
            }

            DGV_Operations.ColorizeNestedColumnHeader(dgvLabEqInv, 3, 10, 17, 26);
            DGV_Operations.ColorizeFixedColumnHeader(dgvLabEqInv, 1, 2);
            DGV_Operations.ColorizeComputedColumnHeader(dgvLabEqInv, 35);
            DGV_Operations.HandleKeyDown(dgvLabEqInv, dgvLabEqInv_CellMouseClick);

            if (access.inventoryManagement_HSE_PPE == RoleAccess.AcessTypes.AT_None)
            {
                tcInvMan.TabPages.Remove(tpHSE);
            }
            else if (access.inventoryManagement_HSE_PPE == RoleAccess.AcessTypes.AT_ReadOnly)
            {
                dgvHSE.ReadOnly = true;
                btnHSEAdd.Enabled = false;
                btnHSEApply.Enabled = false;
                btnHSERemove.Enabled = false;
            }

            DGV_Operations.ColorizeNestedColumnHeader(dgvHSE, 3, 10, 16, 24);
            DGV_Operations.ColorizeFixedColumnHeader(dgvHSE, 1, 2);
            DGV_Operations.ColorizeComputedColumnHeader(dgvHSE, 32);
            DGV_Operations.HandleKeyDown(dgvHSE, dgvHSE_CellMouseClick);

            if (access.inventoryManagement_GeneralEquipmentInventory == RoleAccess.AcessTypes.AT_None)
            {
                tcInvMan.TabPages.Remove(tpGeneralEqInv);
            }
            else if (access.inventoryManagement_GeneralEquipmentInventory == RoleAccess.AcessTypes.AT_ReadOnly)
            {
                dgvGeneralEqInv.ReadOnly = true;
                btnGeneralEqInvAdd.Enabled = false;
                btnGeneralEqInvApply.Enabled = false;
                btnGeneralEqInvRemove.Enabled = false;
            }

            DGV_Operations.ColorizeNestedColumnHeader(dgvGeneralEqInv, 3, 10, 16, 24);
            DGV_Operations.ColorizeFixedColumnHeader(dgvGeneralEqInv, 1, 2);
            DGV_Operations.ColorizeComputedColumnHeader(dgvGeneralEqInv, 32);
            DGV_Operations.HandleKeyDown(dgvGeneralEqInv, dgvGeneralEqInv_CellMouseClick);

            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            CleanVolatileParts();
            ResetApplyBtns();
            //~~~~~~~~~~~~~~~~~~~~
        }
        //-------------------------------------------------------
        void IMyForm.OnCurrentUnitsChanged()
        {
            //~~~~~~~~~~~~~~
            ProjectForm prjForm = (ProjectForm)mainForm.subForms[mainForm.PorjectFormID];
            string selUnitBulkVol = prjForm.lblUnitSelectedBulkSystemCapacity.Tag.ToString();
            string selUnitVol = prjForm.lblUnitSelectedLiquidVolume.Tag.ToString();
            //~~~~~~~~~~~~~~

            //Rig Eq ~~~~~~~~~~~~~~
            UnitString.WriteUnit(dgvRigEquipmentBulk.Columns[4], selUnitBulkVol);

            UnitString.WriteUnit(dgvRigEquipmentPit.Columns[4], selUnitVol);
            UnitString.WriteUnit(dgvRigEquipmentPit.Columns[5], selUnitVol);

            UpdateComputationsByPressRefereshButtons();
        }
        //-------------------------------------------------------
        void UpdateComputationsByPressRefereshButtons()
        {
            //btnPrdInvRefresh_Click(null, null);
            //btnRigEqShaleRefresh_Click(null, null);
            //btnRigEqBulkRefresh_Click(null, null);
            //btnRigEqPitRefresh_Click(null, null);
            //btnManPowRefresh_Click(null, null);
            //btnLabEqInvRefresh_Click(null, null);
            //btnHSERefresh_Click(null, null);
            //btnGeneralEqInvRefresh_Click(null, null);
        }
        //-------------------------------------------------------
        public InvManagementForm(RoleAccess access, frmMain mainForm)
        {
            this.access = access;
            this.mainForm = mainForm;

            InitializeComponent();
        }
        //-------------------------------------------------------
        private void InvManagementForm_Load(object sender, EventArgs e)
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
        void CleanVolatileParts()
        {
            //~~~~~~~~~~~~~~~~~~~~
            dgvProductInv.Rows.Clear();
            //~~~~~~~~~~~~~~~~~~~~
            dgvRigEquipmentShale.Rows.Clear();
            dgvRigEquipmentBulk.Rows.Clear();
            dgvRigEquipmentPit.Rows.Clear();
            //~~~~~~~~~~~~~~~~~~~~

            lblSenMudEng.Text = lblJunMudEng.Text = "";
            lblSenMudEng.Tag = lblJunMudEng.Tag = null;

            txtProjEng.Text = "";

            txtSenCompRep.Text = txtJunCompRep.Text =
            txtClientRep.Text =
            txtEngSup.Text = txtOpSup.Text =
            txtEngSupPhoneNumber.Text =
            txtOpSupPhoneNumber.Text =
            txtWarehousePhonenumber.Text = "";

            cboxWarehouse.SelectedIndex = -1;
            //~~~~~~~~~~~~~~~~~~~~
            dgvLabEqInv.Rows.Clear();
            //~~~~~~~~~~~~~~~~~~~~
            dgvHSE.Rows.Clear();
            //~~~~~~~~~~~~~~~~~~~~
            dgvGeneralEqInv.Rows.Clear();
        }
        //-------------------------------------------------------
        void ResetApplyBtns()
        {
            btnPrdInvApply.ImageKey = "check";
            btnPrdInvApply.BackColor = frmMain.checkColor;
            btnRigEqShaleApply.ImageKey = "check";
            btnRigEqShaleApply.BackColor = frmMain.checkColor;
            btnRigEqBulkApply.ImageKey = "check";
            btnRigEqBulkApply.BackColor = frmMain.checkColor;
            btnRigEqPitApply.ImageKey = "check";
            btnRigEqPitApply.BackColor = frmMain.checkColor;
            btnManPowApply.ImageKey = "check";
            btnManPowApply.BackColor = frmMain.checkColor;
            btnLabEqInvApply.ImageKey = "check";
            btnLabEqInvApply.BackColor = frmMain.checkColor;
            btnHSEApply.ImageKey = "check";
            btnHSEApply.BackColor = frmMain.checkColor;
            btnGeneralEqInvApply.ImageKey = "check";
            btnGeneralEqInvApply.BackColor = frmMain.checkColor;
        }
        //-------------------------------------------------------
        void ComputePrdInvOutstanding(int rowID)
        {
            string query = " select dbo.fn_Get_InvMan_Prd_OutStanding_ByWellIdAndRepNumAndRepID ( "
                + frmMain.selected_RW_WellID.ToString() + ","
                + frmMain.selectedRepNum.ToString() + ","
                + frmMain.selectedRepID.ToString() + ","
                + DGV_Operations.CellValueAsString(dgvProductInv.Rows[rowID].Cells[34])
                + " ) ";

            DataSet ds = new DataSet();
            if (ConnectionManager.ExecQuery(query, ref ds, 1))
            {
                dgvProductInv.Rows[rowID].Cells[32].Value = ds.Tables[0].Rows[0][0].ToString();
                ds.Dispose();
            }
            else
            {
                dgvProductInv.Rows[rowID].Cells[32].Value = "???";
            }
        }
        //-------------------------------------------------------
        //returns:   true (success) , false(failure)
        public bool LoadProductInv()
        {
            //string METHOD = "LoadProductInv : " + CLASS_NAME;

            skipCellValueChanged_dgvPrdInv = true;

            dgvProductInv.Rows.Clear();

            try
            {
                string query =
                  " select  "
                + " rimp.ID, prd.Name, prd.UnitSize, "
                + " isnull(rimp.ReqQuantity, 0),        isnull(rimp.ReqQuantity, 0), rimp.ReqOrdNum, rimp.ReqOrdTime, rimp.ReqDelivType, rimp.ReqMSC, rimp.ReqComment, "
                + " isnull(rimp.RecQuantity, 0),        isnull(rimp.RecQuantity, 0), rimp.RecBatchNum, rimp.RecSrc, rimp.RecSN, rimp.RecComment, "
                + " isnull(rimp.BkLoadQuantity, 0),     isnull(rimp.BkLoadQuantity, 0), rimp.BkLoadBatchNum, rimp.BkLoadReqNum, rimp.BkLoadTime, rimp.BkLoadDest, rimp.BkLoadSN, rimp.BkLoadComment, "
                + " isnull(rimp.MovQuantity, 0),        isnull(rimp.MovQuantity, 0), rimp.MovBatchNum, rimp.MovReqNum, rimp.MovTime, rimp.MovDest, rimp.MovSN, rimp.MovComment, "
                + " 0,                                  isnull(rimp.StockOnTruck_Boat, 0), p2prd.Prd_AutoID "

                + " from rt_Prj2Prd p2prd join lt_Product prd on p2prd.Prd_AutoID = prd.AutoID "
                + " left join (select * from rt_Rep2InvManPrd where ReportID = " + frmMain.selectedRepID.ToString() + " ) rimp "
                + " on p2prd.Prd_AutoID = rimp.Prd_AutoID "
                + " where p2prd.PrjAutoID = " + frmMain.selectedPrjID.ToString();

                DataSet ds = ConnectionManager.ExecQuery(query);

                if (ds != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        dgvProductInv.Rows.Add(ds.Tables[0].Rows[i].ItemArray);
                        dgvProductInv.Rows[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dgvProductInv.Rows[i].HeaderCell.Value = (i + 1).ToString();

                        ComputePrdInvOutstanding(i);
                    }

                    ds.Dispose();
                    skipCellValueChanged_dgvPrdInv = false;
                    return true;
                }
            }
            catch (Exception)
            {
                //ErrorManager.Set_Error(new Errors("Can not load data from data base", ex.Message, METHOD));
                //will be handled outside
            }

            skipCellValueChanged_dgvPrdInv = false;
            return false;
        }
        //-------------------------------------------------------
        //returns:   true (success) , false(failure)
        public bool LoadRigEquipmentsShale()
        {
            //string METHOD = "LoadRigEquipmentsShale : " + CLASS_NAME;

            skipCellValueChanged_dgvRigEqShale = true;

            dgvRigEquipmentShale.Rows.Clear();

            try
            {
                string query =
                 " select re.ID, (\'Shale Shaker Screen \' + isnull(p2.Value, \'\') + \' \' + isnull(p3.Value, \'\') + \' \' + isnull(p1.Value, \'\') ),   "
                + " 	  isnull(re.Used, 0), isnull(re.Received, 0), isnull(re.Returned, 0), s.ID  "

                + " from rt_Rig2ShaleShaker s "
                + " left join lt_PredefValues p1 on s.ScreenSize_PredefAutoID = p1.AutoID   "
                + " left join lt_PredefValues p2 on s.ScreenBrand_PredefAutoID = p2.AutoID   "
                + " left join lt_PredefValues p3 on s.ScreenModel_PredefAutoID = p3.AutoID   "
                + " left join (select * from rt_Rep2InvManRigEqShale where ReportID = " + frmMain.selectedRepID.ToString() + " ) re "
                + " on re.ShaleShaker_ID = s.ID "
                + " where s.RigID = " + frmMain.selected_RW_RigID.ToString()
                + " order by (\'Shale Shaker Screen \' + isnull(p2.Value, \'\') + \' \' + isnull(p3.Value, \'\') + \' \' + isnull(p1.Value, \'\') )  desc";

                DataSet ds = ConnectionManager.ExecQuery(query);

                if (ds != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        dgvRigEquipmentShale.Rows.Add(ds.Tables[0].Rows[i].ItemArray);
                        dgvRigEquipmentShale.Rows[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dgvRigEquipmentShale.Rows[i].HeaderCell.Value = (i + 1).ToString();
                    }

                    ds.Dispose();
                    skipCellValueChanged_dgvRigEqShale = false;
                    return true;
                }
            }
            catch (Exception)
            {
                //ErrorManager.Set_Error(new Errors("Can not load data from data base", ex.Message, METHOD));
                //will be handled outside
            }

            skipCellValueChanged_dgvRigEqShale = false;
            return false;
        }
        //-------------------------------------------------------
        //returns:   true (success) , false(failure)
        public bool LoadRigEquipmentsBulk()
        {
            //string METHOD = "LoadRigEquipmentsBulk : " + CLASS_NAME;

            skipCellValueChanged_dgvRigEqBulk = true;

            dgvRigEquipmentBulk.Rows.Clear();

            try
            {
                string query =
                      " select rb.ID,  pre.Value, p.Name, p.AutoID, b.Capacity, isnull(rb.Used, 0), b.ID "

                    + " from rt_Rig2BulkSystem b "
                    + " left join lt_PredefValues pre on b.EqDesc_PredefAutoID = pre.AutoID "
                    + " left join (select * from rt_Rep2InvManRigEqBulk where ReportID = " + frmMain.selectedRepID.ToString() + " ) rb "
                    + " on rb.BulkSys_ID = b.ID "
                    + " left join lt_Product p on rb.Prd_AutoID = p.AutoID "
                    + " where b.RigID = " + frmMain.selected_RW_RigID.ToString()
                    + " order by b.UserOrder  ";

                DataSet ds = ConnectionManager.ExecQuery(query);

                if (ds != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        dgvRigEquipmentBulk.Rows.Add(ds.Tables[0].Rows[i].ItemArray);
                        dgvRigEquipmentBulk.Rows[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dgvRigEquipmentBulk.Rows[i].HeaderCell.Value = (i + 1).ToString();
                    }

                    ds.Dispose();
                    skipCellValueChanged_dgvRigEqBulk = false;
                    return true;
                }
            }
            catch (Exception)
            {
                //ErrorManager.Set_Error(new Errors("Can not load data from data base", ex.Message, METHOD));
                //will be handled outside
            }

            skipCellValueChanged_dgvRigEqBulk = false;
            return false;
        }
        //-------------------------------------------------------
        //returns:   true (success) , false(failure)
        public bool LoadRigEquipmentsPit()
        {
            //string METHOD = "LoadRigEquipmentsPit : " + CLASS_NAME;

            skipCellValueChanged_dgvRigEqPit = true;

            dgvRigEquipmentPit.Rows.Clear();

            try
            {
                string query =
                      " select rp.ID, p1.Value, p2.Value, p2.AutoID, pt.PitCapacity, rp.Filled "
                    + " from rt_Rep2InvManRigEqPit rp join rt_Rig2PitTank pt on rp.Pit_ID = pt.ID "
                    + " join lt_PredefValues p1 on pt.PitName_PredefAutoID = p1.AutoID "
                    + " left join lt_PredefValues p2 on rp.WaterType_PredefAutoID = p2.AutoID "
                    + " where rp.ReportID = " + frmMain.selectedRepID.ToString()
                    + " order by pt.UserOrder ";

                DataSet ds = ConnectionManager.ExecQuery(query);

                if (ds != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        dgvRigEquipmentPit.Rows.Add(ds.Tables[0].Rows[i].ItemArray);
                        dgvRigEquipmentPit.Rows[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dgvRigEquipmentPit.Rows[i].HeaderCell.Value = (i + 1).ToString();
                    }

                    ds.Dispose();
                    skipCellValueChanged_dgvRigEqPit = false;
                    return true;
                }
            }
            catch (Exception)
            {
                //ErrorManager.Set_Error(new Errors("Can not load data from data base", ex.Message, METHOD));
                //will be handled outside
            }

            skipCellValueChanged_dgvRigEqPit = false;
            return false;
        }
        //-------------------------------------------------------
        void LoadManPowerTab()
        {
            //string METHOD = "LoadManPowerTab : " + CLASS_NAME;

            try
            {
                string query =
                      " select  "
                    + " SenMudEngAutoID_InvMngManPow,  "
                    + " JunMudEngAutoID_InvMngManPow,  "
                    + " ProjEng_InvMngManPow, "
                    + " SenCmpRep_InvMngManPow,  "
                    + " JunCmpRep_InvMngManPow,  "
                    + " ClientRep_InvMngManPow,  "
                    + " EngSup_InvMngManPow,  "
                    + " OpSup_InvMngManPow,  "
                    + " EngSupPhone_InvMngManPow,  "
                    + " OpSupPhone_InvMngManPow,  "
                    + " WareHouse_InvMngManPow,  "
                    + " WareHousePhone_InvMngManPow  "
                    + " from at_Report where ID = " + frmMain.selectedRepID.ToString();

                DataSet ds = ConnectionManager.ExecQuery(query, 1);

                if (ds != null)
                {
                    //~~~~~~~~~~~~
                    lblSenMudEng.Text = null;
                    lblSenMudEng.Tag = null;

                    if (ds.Tables[0].Rows[0][0] != DBNull.Value)
                    {
                        int code = 0;
                        string name = "";
                        string phone = "";

                        int senAutoID = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
                        if (FetchTableData.GetPersonnelData(senAutoID, out code, out name, out phone))
                        {
                            lblSenMudEng.Text = name;
                            lblSenMudEng.Tag = senAutoID;
                        }
                    }
                    //~~~~~~~~~~~~
                    lblJunMudEng.Text = null;
                    lblJunMudEng.Tag = null;

                    if (ds.Tables[0].Rows[0][1] != DBNull.Value)
                    {
                        int code = 0;
                        string name = "";
                        string phone = "";

                        int junAutoID = Convert.ToInt32(ds.Tables[0].Rows[0][1]);
                        if (FetchTableData.GetPersonnelData(junAutoID, out code, out name, out phone))
                        {
                            lblJunMudEng.Text = name;
                            lblJunMudEng.Tag = junAutoID;
                        }
                    }
                    //~~~~~~~~~~~~
                    txtProjEng.Text = ds.Tables[0].Rows[0][2].ToString();
                    txtSenCompRep.Text = ds.Tables[0].Rows[0][3].ToString();
                    txtJunCompRep.Text = ds.Tables[0].Rows[0][4].ToString();
                    txtClientRep.Text = ds.Tables[0].Rows[0][5].ToString();
                    //~~~~~~~~~~~~
                    txtEngSup.Text = ds.Tables[0].Rows[0][6].ToString();
                    txtOpSup.Text = ds.Tables[0].Rows[0][7].ToString();
                    txtEngSupPhoneNumber.Text = ds.Tables[0].Rows[0][8].ToString();
                    txtOpSupPhoneNumber.Text = ds.Tables[0].Rows[0][9].ToString();
                    //~~~~~~~~~~~~
                    cboxWarehouse.Text = ds.Tables[0].Rows[0][10].ToString();
                    txtWarehousePhonenumber.Text = ds.Tables[0].Rows[0][11].ToString();

                    ds.Dispose();
                }
            }
            catch (Exception)
            {
                //ErrorManager.Set_Error(new Errors("Can not load data from data base", ex.Message, METHOD));
                //will be handled outside
            }
        }
        //-------------------------------------------------------
        void ComputeLabEquipmentOutstanding(int rowID)
        {
            string query = " select dbo.fn_Get_InvMan_LabEq_OutStanding_ByWellIdAndRepNumAndRepID ( "
                + frmMain.selected_RW_WellID.ToString() + ","
                + frmMain.selectedRepNum.ToString() + ","
                + frmMain.selectedRepID.ToString() + ","
                + DGV_Operations.CellValueAsString(dgvLabEqInv.Rows[rowID].Cells[37])
                + " ) ";

            DataSet ds = new DataSet();
            if (ConnectionManager.ExecQuery(query, ref ds, 1))
            {
                dgvLabEqInv.Rows[rowID].Cells[35].Value = ds.Tables[0].Rows[0][0].ToString();
                ds.Dispose();
            }
            else
            {
                dgvLabEqInv.Rows[rowID].Cells[35].Value = "???";
            }
        }
        //-------------------------------------------------------
        //returns:   true (success) , false(failure)
        bool LoadLabEquipment()
        {
            //string METHOD = "LoadLabEquipment : " + CLASS_NAME;

            skipCellValueChanged_dgvLabEqInv = true;

            dgvLabEqInv.Rows.Clear();

            try
            {
                string query =
                      " select rt_Rep2InvManLabEq.ID, lt_LabEquipments.Code, lt_LabEquipments.Name,  "

                    + " ReqQuantity ,         ReqQuantity ,ReqOrdNum ,ReqOrdTime ,ReqDelivType ,ReqMSC ,ReqComment,  "
                    + " RecQuantity,          RecQuantity ,RecPropNum ,RecSN ,RecSrc ,RecCalibrationStatus ,RecComment,  "
                    + " BkLoadQuantity,       BkLoadQuantity ,BkLoadPropNum ,BkLoadSN ,BkLoadReqNum ,BkLoadTime ,BkLoadDest ,BkLoadCalibrationStatus ,BkLoadComment,  "
                    + " MovQuantity,          MovQuantity ,MovPropNum ,MovSN ,MovReqNum ,MovTime ,MovDest ,MovCalibrationStatus, MovComment,  "
                    + " 0,                    StockOnTruck_Boat,   lt_LabEquipments.AutoID	    "

                    + " from rt_Rep2InvManLabEq join lt_LabEquipments "
                    + " on rt_Rep2InvManLabEq.Eq_AutoID = lt_LabEquipments.AutoID "
                    + " where rt_Rep2InvManLabEq.ReportID = " + frmMain.selectedRepID.ToString();

                DataSet ds = ConnectionManager.ExecQuery(query);

                if (ds != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        dgvLabEqInv.Rows.Add(ds.Tables[0].Rows[i].ItemArray);
                        dgvLabEqInv.Rows[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dgvLabEqInv.Rows[i].HeaderCell.Value = (i + 1).ToString();

                        ComputeLabEquipmentOutstanding(i);
                    }

                    ds.Dispose();
                    skipCellValueChanged_dgvLabEqInv = false;
                    return true;
                }
            }
            catch (Exception)
            {
                //ErrorManager.Set_Error(new Errors("Can not load data from data base", ex.Message, METHOD));
                //will be handled outside
            }

            skipCellValueChanged_dgvLabEqInv = false;
            return false;
        }
        //-------------------------------------------------------
        void ComputeHSEPPEOutstanding(int rowID)
        {
            string query = " select dbo.fn_Get_InvMan_HSEPPE_OutStanding_ByWellIdAndRepNumAndRepID ( "
                + frmMain.selected_RW_WellID.ToString() + ","
                + frmMain.selectedRepNum.ToString() + ","
                + frmMain.selectedRepID.ToString() + ","
                + DGV_Operations.CellValueAsString(dgvHSE.Rows[rowID].Cells[34])
                + " ) ";

            DataSet ds = new DataSet();
            if (ConnectionManager.ExecQuery(query, ref ds, 1))
            {
                dgvHSE.Rows[rowID].Cells[32].Value = ds.Tables[0].Rows[0][0].ToString();
                ds.Dispose();
            }
            else
            {
                dgvHSE.Rows[rowID].Cells[32].Value = "???";
            }
        }
        //-------------------------------------------------------
        //returns:   true (success) , false(failure)
        bool LoadHSEPPE()
        {
            //string METHOD = "LoadHSEPPE : " + CLASS_NAME;

            skipCellValueChanged_dgvHSE = true;

            dgvHSE.Rows.Clear();

            try
            {
                string query =
                     " select rt_Rep2InvManHSE.ID, lt_HSE_PPE.HSECode, lt_HSE_PPE.Name, "

                    + " ReqQuantity ,         ReqQuantity ,ReqOrdNum ,ReqOrdTime ,ReqDelivType ,ReqMSC ,ReqComment,  "
                    + " RecQuantity,          RecQuantity ,RecPropNum ,RecSN ,RecSrc ,RecComment,  "
                    + " BkLoadQuantity,       BkLoadQuantity ,BkLoadPropNum ,BkLoadSN ,BkLoadReqNum ,BkLoadTime ,BkLoadDest ,BkLoadComment,  "
                    + " MovQuantity,          MovQuantity ,MovPropNum ,MovSN ,MovReqNum ,MovTime ,MovDest ,MovComment,  "
                    + " 0,                    StockOnTruck_Boat,   lt_HSE_PPE.AutoID	    "

                    + " from rt_Rep2InvManHSE join lt_HSE_PPE  "

                    + " on rt_Rep2InvManHSE.HSE_AutoID = lt_HSE_PPE.AutoID  "
                    + " where rt_Rep2InvManHSE.ReportID = " + frmMain.selectedRepID.ToString();

                DataSet ds = ConnectionManager.ExecQuery(query);

                if (ds != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        dgvHSE.Rows.Add(ds.Tables[0].Rows[i].ItemArray);
                        dgvHSE.Rows[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dgvHSE.Rows[i].HeaderCell.Value = (i + 1).ToString();

                        ComputeHSEPPEOutstanding(i);
                    }

                    ds.Dispose();
                    skipCellValueChanged_dgvHSE = false;
                    return true;
                }
            }
            catch (Exception)
            {
                //ErrorManager.Set_Error(new Errors("Can not load data from data base", ex.Message, METHOD));
                //will be handled outside
            }

            skipCellValueChanged_dgvHSE = false;
            return false;
        }
        //-------------------------------------------------------
        void ComputeGeneralEqOutstanding(int rowID)
        {
            string query = " select dbo.fn_Get_InvMan_GeneralEq_OutStanding_ByWellIdAndRepNumAndRepID ( "
                + frmMain.selected_RW_WellID.ToString() + ","
                + frmMain.selectedRepNum.ToString() + ","
                + frmMain.selectedRepID.ToString() + ","
                + DGV_Operations.CellValueAsString(dgvGeneralEqInv.Rows[rowID].Cells[34]) 
                + " ) ";

            DataSet ds = new DataSet();
            if (ConnectionManager.ExecQuery(query, ref ds, 1))
            {
                dgvGeneralEqInv.Rows[rowID].Cells[32].Value = ds.Tables[0].Rows[0][0].ToString();
                ds.Dispose();
            }
            else
            {
                dgvGeneralEqInv.Rows[rowID].Cells[32].Value = "???";
            }
        }
        //-------------------------------------------------------
        //returns:   true (success) , false(failure)
        bool LoadGeneralEq()
        {
            //string METHOD = "LoadGeneralEq : " + CLASS_NAME;

            skipCellValueChanged_dgvGeneralEqInv = true;

            dgvGeneralEqInv.Rows.Clear();

            try
            {
                string query =
                      " select rt_Rep2InvManGeneralEq.ID, lt_GeneralEquipment.Code, lt_GeneralEquipment.Name, "

                    + " ReqQuantity ,         ReqQuantity ,ReqOrdNum ,ReqOrdTime ,ReqDelivType ,ReqMSC ,ReqComment,  "
                    + " RecQuantity,          RecQuantity ,RecPropNum ,RecSN ,RecSrc ,RecComment,  "
                    + " BkLoadQuantity,       BkLoadQuantity ,BkLoadPropNum ,BkLoadSN ,BkLoadReqNum ,BkLoadTime ,BkLoadDest ,BkLoadComment,  "
                    + " MovQuantity,          MovQuantity ,MovPropNum ,MovSN ,MovReqNum ,MovTime ,MovDest ,MovComment,  "
                    + " 0,                    StockOnTruck_Boat	 ,  lt_GeneralEquipment.AutoID    "

                    + " from rt_Rep2InvManGeneralEq join lt_GeneralEquipment "
                    + " on rt_Rep2InvManGeneralEq.GenEq_AutoID = lt_GeneralEquipment.AutoID  "
                    + " where rt_Rep2InvManGeneralEq.ReportID = " + frmMain.selectedRepID.ToString();

                DataSet ds = ConnectionManager.ExecQuery(query);

                if (ds != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        dgvGeneralEqInv.Rows.Add(ds.Tables[0].Rows[i].ItemArray);
                        dgvGeneralEqInv.Rows[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dgvGeneralEqInv.Rows[i].HeaderCell.Value = (i + 1).ToString();

                        ComputeGeneralEqOutstanding(i);
                    }

                    ds.Dispose();
                    skipCellValueChanged_dgvGeneralEqInv = false;
                    return true;
                }
            }
            catch (Exception)
            {
                //ErrorManager.Set_Error(new Errors("Can not load data from data base", ex.Message, METHOD));
                //will be handled outside
            }

            skipCellValueChanged_dgvGeneralEqInv = false;
            return false;
        }
        //-------------------------------------------------------
        private void btnPrdInvRefresh_Click(object sender, EventArgs e)
        {
            string METHOD = "btnPrdRefresh_Click : " + CLASS_NAME;

            if (frmMain.selectedRepID == -1)
                return;

            if (access.inventoryManagement_ProductInventory == RoleAccess.AcessTypes.AT_None)
                return;

            if (!LoadProductInv())
            {
                InformationManager.Set_Info(new Errors("Can not load data from data base", "", METHOD));
                //?????logout????
            }

            btnPrdInvApply.ImageKey = "check";
            btnPrdInvApply.BackColor = frmMain.checkColor;
        }
        //-------------------------------------------------------
        private void btnPrdApply_Click(object sender, EventArgs e)
        {
            if (frmMain.selectedRepID == -1)
                return;

            if (access.inventoryManagement_ProductInventory != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;

            try
            {
                List<Errors> errs = new List<Errors>();

                for (int i = 0; i < dgvProductInv.Rows.Count; i++)
                {
                    List<ProcedureParam> prms = new List<ProcedureParam>();
                    string idStr = DGV_Operations.CellValueAsString(dgvProductInv.Rows[i].Cells[0]);

                    if (idStr == "")
                        prms.Add(new ProcedureParam("@ID", ProcedureParam.ParamType.PT_BigInt));//try add
                    else
                        prms.Add(new ProcedureParam("@ID", idStr, ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Record"));//update
                    //~~~~~~~~~~	
                    int colID = 4;
                    prms.Add(new ProcedureParam("@ReqQuantity", DGV_Operations.CellValueAsString(dgvProductInv.Rows[i].Cells[colID++]), ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Request Quantity"));
                    prms.Add(new ProcedureParam("@ReqOrdNum", DGV_Operations.CellValueAsString(dgvProductInv.Rows[i].Cells[colID++]), 30));
                    prms.Add(new ProcedureParam("@ReqOrdTime", DGV_Operations.CellValueAsString(dgvProductInv.Rows[i].Cells[colID++]), 30));
                    prms.Add(new ProcedureParam("@ReqDelivType", DGV_Operations.CellValueAsString(dgvProductInv.Rows[i].Cells[colID++]), 50));
                    prms.Add(new ProcedureParam("@ReqMSC", DGV_Operations.CellValueAsString(dgvProductInv.Rows[i].Cells[colID++]), 50));
                    prms.Add(new ProcedureParam("@ReqComment", DGV_Operations.CellValueAsString(dgvProductInv.Rows[i].Cells[colID++]), 150));
                    //~~~~~~~~~~
                    colID = 11;
                    prms.Add(new ProcedureParam("@RecQuantity", DGV_Operations.CellValueAsString(dgvProductInv.Rows[i].Cells[colID++]), ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Receive Quantity"));
                    prms.Add(new ProcedureParam("@RecBatchNum", DGV_Operations.CellValueAsString(dgvProductInv.Rows[i].Cells[colID++]), 30));
                    prms.Add(new ProcedureParam("@RecSrc", DGV_Operations.CellValueAsString(dgvProductInv.Rows[i].Cells[colID++]), 50));
                    prms.Add(new ProcedureParam("@RecSN", DGV_Operations.CellValueAsString(dgvProductInv.Rows[i].Cells[colID++]), 50));
                    prms.Add(new ProcedureParam("@RecComment", DGV_Operations.CellValueAsString(dgvProductInv.Rows[i].Cells[colID++]), 150));
                    //~~~~~~~~~~
                    colID = 17;
                    prms.Add(new ProcedureParam("@BkLoadQuantity", DGV_Operations.CellValueAsString(dgvProductInv.Rows[i].Cells[colID++]), ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Backload Quantity"));
                    prms.Add(new ProcedureParam("@BkLoadBatchNum", DGV_Operations.CellValueAsString(dgvProductInv.Rows[i].Cells[colID++]), 30));
                    prms.Add(new ProcedureParam("@BkLoadReqNum", DGV_Operations.CellValueAsString(dgvProductInv.Rows[i].Cells[colID++]), 30));
                    prms.Add(new ProcedureParam("@BkLoadTime", DGV_Operations.CellValueAsString(dgvProductInv.Rows[i].Cells[colID++]), 30));
                    prms.Add(new ProcedureParam("@BkLoadDest", DGV_Operations.CellValueAsString(dgvProductInv.Rows[i].Cells[colID++]), 50));
                    prms.Add(new ProcedureParam("@BkLoadSN", DGV_Operations.CellValueAsString(dgvProductInv.Rows[i].Cells[colID++]), 50));
                    prms.Add(new ProcedureParam("@BkLoadComment", DGV_Operations.CellValueAsString(dgvProductInv.Rows[i].Cells[colID++]), 150));
                    //~~~~~~~~~~
                    colID = 25;
                    prms.Add(new ProcedureParam("@MovQuantity", DGV_Operations.CellValueAsString(dgvProductInv.Rows[i].Cells[colID++]), ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Movement Quantity"));
                    prms.Add(new ProcedureParam("@MovBatchNum", DGV_Operations.CellValueAsString(dgvProductInv.Rows[i].Cells[colID++]), 30));
                    prms.Add(new ProcedureParam("@MovReqNum", DGV_Operations.CellValueAsString(dgvProductInv.Rows[i].Cells[colID++]), 30));
                    prms.Add(new ProcedureParam("@MovTime", DGV_Operations.CellValueAsString(dgvProductInv.Rows[i].Cells[colID++]), 30));
                    prms.Add(new ProcedureParam("@MovDest", DGV_Operations.CellValueAsString(dgvProductInv.Rows[i].Cells[colID++]), 50));
                    prms.Add(new ProcedureParam("@MovSN", DGV_Operations.CellValueAsString(dgvProductInv.Rows[i].Cells[colID++]), 50));
                    prms.Add(new ProcedureParam("@MovComment", DGV_Operations.CellValueAsString(dgvProductInv.Rows[i].Cells[colID++]), 150));
                    //~~~~~~~~~~
                    colID = 33;
                    prms.Add(new ProcedureParam("@StockOnTruck_Boat", DGV_Operations.CellValueAsString(dgvProductInv.Rows[i].Cells[colID++]), ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Stock on Truck/Boat"));
                    //~~~~~~~~~~
                    //for adding
                    prms.Add(new ProcedureParam("@ReportID", frmMain.selectedRepID.ToString(), ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Report"));
                    prms.Add(new ProcedureParam("@ProjAutoID", frmMain.selectedPrjID.ToString(), ProcedureParam.ParamType.PT_Int, true, "1", false, "", "Invalid Project"));
                    prms.Add(new ProcedureParam("@Prd_AutoID", DGV_Operations.CellValueAsString(dgvProductInv.Rows[i].Cells[colID++]), ProcedureParam.ParamType.PT_Int, true, "1", false, "", "Invalid Product"));


                    string simpErr;
                    Errors critErr;
                    Int64 resultStat;

                    int ret = Procedures.CallProcedure("prc_rt_Rep2InvManPrd_add_or_update", prms, out simpErr, out critErr, out resultStat);

                    Color normalBkColor = (i % 2 == 0) ? dgvProductInv.BackgroundColor : dgvProductInv.AlternatingRowsDefaultCellStyle.BackColor;

                    dgvProductInv.Rows[i].Cells[3].Style.BackColor = normalBkColor;
                    dgvProductInv.Rows[i].Cells[10].Style.BackColor = normalBkColor;
                    dgvProductInv.Rows[i].Cells[16].Style.BackColor = normalBkColor;
                    dgvProductInv.Rows[i].Cells[24].Style.BackColor = normalBkColor;
                    dgvProductInv.Rows[i].Cells[33].Style.BackColor = normalBkColor;

                    if (ret != 0)
                    {
                        if (ret == 1)
                        {
                            if (simpErr.StartsWith("Invalid Request Quantity"))
                                dgvProductInv.Rows[i].Cells[3].Style.BackColor = Color.Red;
                            else if (simpErr.StartsWith("Invalid Receive Quantity"))
                                dgvProductInv.Rows[i].Cells[10].Style.BackColor = Color.Red;
                            else if (simpErr.StartsWith("Invalid Backload Quantity"))
                                dgvProductInv.Rows[i].Cells[16].Style.BackColor = Color.Red;
                            else if (simpErr.StartsWith("Invalid Movement Quantity"))
                                dgvProductInv.Rows[i].Cells[24].Style.BackColor = Color.Red;
                            else if (simpErr.StartsWith("Invalid Stock on Truck/Boat"))
                                dgvProductInv.Rows[i].Cells[33].Style.BackColor = Color.Red;

                            critErr = new Errors(simpErr);
                        }

                        errs.Add(critErr);
                    }
                    else
                    {
                        if (resultStat > 0)//new record is just added and updated
                            dgvProductInv.Rows[i].Cells[0].Value = resultStat;//to be sure
                    }
                }

                InformationManager.Set_Info(errs.ToArray());

                if (errs.Count == 0)
                {
                    for (int i = 0; i < dgvProductInv.Rows.Count; i++)
                        ComputePrdInvOutstanding(i);

                    btnPrdInvApply.ImageKey = "Check";
                    btnPrdInvApply.BackColor = frmMain.checkColor;
                }
            }
            catch (Exception)
            {
            }
        }
        //-------------------------------------------------------
        private void dgvProductInv_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex == -1)
                return;

            if (skipCellValueChanged_dgvPrdInv)
                return;

            if (e.ColumnIndex < 0 || e.RowIndex < 0)
                return;

            //if (e.ColumnIndex == 3 || e.ColumnIndex == 10 || e.ColumnIndex == 16 || e.ColumnIndex == 24)
            //    ComputePrdInvOutstanding(e.RowIndex);

            btnPrdInvApply.ImageKey = "warning";
            btnPrdInvApply.BackColor = frmMain.warningColor;
        }
        //-------------------------------------------------------
        private void dgvProductInv_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (frmMain.selectedRepID == -1)
                return;

            if (access.inventoryManagement_ProductInventory != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;

            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                frmMultiInput fm = new frmMultiInput();
                fm.Height = 200;

                if (e.ColumnIndex == 3)
                {
                    fm.Width = 600;
                    fm.lblTitle.Text = "Request:";

                    fm.dgvInput.Columns.Add("1", "Quantity");
                    fm.dgvInput.Columns.Add("2", "Order #");
                    fm.dgvInput.Columns.Add("3", "Order Time");
                    fm.dgvInput.Columns.Add("4", "Delivery Type");
                    fm.dgvInput.Columns.Add("5", "Material Supplier Code (M.S.C)");
                    fm.dgvInput.Columns.Add("6", "Comment");
                }
                else if (e.ColumnIndex == 10)
                {
                    fm.Width = 600;
                    fm.lblTitle.Text = "Received:";

                    fm.dgvInput.Columns.Add("1", "Quantity");
                    fm.dgvInput.Columns.Add("2", "Batch #");
                    fm.dgvInput.Columns.Add("3", "Source");
                    fm.dgvInput.Columns.Add("4", "S/N");
                    fm.dgvInput.Columns.Add("5", "Comment");
                }
                else if (e.ColumnIndex == 16)
                {
                    fm.Width = 650;
                    fm.lblTitle.Text = "Backload:";

                    fm.dgvInput.Columns.Add("1", "Quantity");
                    fm.dgvInput.Columns.Add("2", "Batch #");
                    fm.dgvInput.Columns.Add("3", "Req. #");
                    fm.dgvInput.Columns.Add("4", "Time");
                    fm.dgvInput.Columns.Add("5", "Destination");
                    fm.dgvInput.Columns.Add("6", "S/N");
                    fm.dgvInput.Columns.Add("7", "Comment");
                }
                else if (e.ColumnIndex == 24)
                {
                    fm.Width = 650;
                    fm.lblTitle.Text = "Movement:";

                    fm.dgvInput.Columns.Add("1", "Quantity");
                    fm.dgvInput.Columns.Add("2", "Batch #");
                    fm.dgvInput.Columns.Add("3", "Req. #");
                    fm.dgvInput.Columns.Add("4", "Time");
                    fm.dgvInput.Columns.Add("5", "Destination");
                    fm.dgvInput.Columns.Add("6", "S/N");
                    fm.dgvInput.Columns.Add("7", "Comment");
                }
                else
                {
                    return;
                }


                int startColID = e.ColumnIndex + 1;

                for (int i = 0; i < fm.dgvInput.Columns.Count; i++)
                {
                    fm.dgvInput.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    fm.dgvInput.Columns[i].SortMode = DataGridViewColumnSortMode.Programmatic;
                }

                fm.dgvInput.AllowUserToAddRows = false;
                fm.dgvInput.AllowUserToDeleteRows = false;
                fm.dgvInput.RowHeadersVisible = false;
                //~~~~~~~~~~~~~~~~~~~~~~
                fm.dgvInput.Rows.Add();
                //~~~~~~~~~~~~~~~~~~~~~~
                for (int i = 0; i < fm.dgvInput.Columns.Count; i++)
                    fm.dgvInput[i, 0].Value = dgvProductInv[startColID + i, e.RowIndex].Value;
                //~~~~~~~~~~~~~~~~~~~~~~
                fm.dgvInput.ReadOnly = false;
                DialogResult dlgRes = fm.ShowDialog();

                if (dlgRes != System.Windows.Forms.DialogResult.OK)
                    return;
                //~~~~~~~~~~~~~~~~~~~~~~
                for (int i = 0; i < fm.dgvInput.Columns.Count; i++)
                    dgvProductInv[startColID + i, e.RowIndex].Value = fm.dgvInput[i, 0].Value;

                dgvProductInv[startColID - 1, e.RowIndex].Value = dgvProductInv[startColID, e.RowIndex].Value;

                //ComputePrdInvOutstanding(e.RowIndex);
            }
        }
        //-------------------------------------------------------
        private void btnRigEqShaleRefresh_Click(object sender, EventArgs e)
        {
            string METHOD = "btnRigEqShaleRefresh_Click : " + CLASS_NAME;

            if (frmMain.selectedRepID == -1)
                return;

            if (access.inventoryManagement_RigEquipmentInventory == RoleAccess.AcessTypes.AT_None)
                return;

            if (!LoadRigEquipmentsShale())
            {
                InformationManager.Set_Info(new Errors("Can not load data from data base", "", METHOD));
                //?????logout????
            }

            btnRigEqShaleApply.ImageKey = "check";
            btnRigEqShaleApply.BackColor = frmMain.checkColor;
        }
        //-------------------------------------------------------
        private void btnRigEqShaleApply_Click(object sender, EventArgs e)
        {
            if (frmMain.selectedRepID == -1)
                return;

            if (access.inventoryManagement_RigEquipmentInventory != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;

            try
            {
                List<Errors> errs = new List<Errors>();

                for (int i = 0; i < dgvRigEquipmentShale.Rows.Count; i++)
                {
                    List<ProcedureParam> prms = new List<ProcedureParam>();
                    string idStr = DGV_Operations.CellValueAsString(dgvRigEquipmentShale.Rows[i].Cells[0]);

                    if (idStr == "")
                        prms.Add(new ProcedureParam("@ID", ProcedureParam.ParamType.PT_BigInt));//try add
                    else
                        prms.Add(new ProcedureParam("@ID", idStr, ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Record"));//update
                    //~~~~~~~~~~
                    prms.Add(new ProcedureParam("@Used", DGV_Operations.CellValueAsString(dgvRigEquipmentShale.Rows[i].Cells[2]), ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Used"));
                    prms.Add(new ProcedureParam("@Received", DGV_Operations.CellValueAsString(dgvRigEquipmentShale.Rows[i].Cells[3]), ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Received"));
                    prms.Add(new ProcedureParam("@Returned", DGV_Operations.CellValueAsString(dgvRigEquipmentShale.Rows[i].Cells[4]), ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Returned"));
                    //~~~~~~~~~~
                    //for adding
                    prms.Add(new ProcedureParam("@ReportID", frmMain.selectedRepID.ToString(), ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Report"));
                    prms.Add(new ProcedureParam("@ProjAutoID", frmMain.selectedPrjID.ToString(), ProcedureParam.ParamType.PT_Int, true, "1", false, "", "Invalid Project"));
                    prms.Add(new ProcedureParam("@ShaleShaker_ID", DGV_Operations.CellValueAsString(dgvRigEquipmentShale.Rows[i].Cells[5]), ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Shale"));

                    string simpErr;
                    Errors critErr;
                    Int64 resultStat;

                    int ret = Procedures.CallProcedure("prc_rt_Rep2InvManRigEqShale_add_or_update", prms, out simpErr, out critErr, out resultStat);

                    Color normalBkColor = (i % 2 == 0) ? dgvRigEquipmentShale.BackgroundColor : dgvRigEquipmentShale.AlternatingRowsDefaultCellStyle.BackColor;

                    dgvRigEquipmentShale.Rows[i].Cells[2].Style.BackColor = normalBkColor;
                    dgvRigEquipmentShale.Rows[i].Cells[3].Style.BackColor = normalBkColor;
                    dgvRigEquipmentShale.Rows[i].Cells[4].Style.BackColor = normalBkColor;

                    if (ret != 0)
                    {
                        if (ret == 1)
                        {
                            if (simpErr.StartsWith("Invalid Used"))
                                dgvRigEquipmentShale.Rows[i].Cells[2].Style.BackColor = Color.Red;
                            else if (simpErr.StartsWith("Invalid Received"))
                                dgvRigEquipmentShale.Rows[i].Cells[3].Style.BackColor = Color.Red;
                            else if (simpErr.StartsWith("Invalid Returned"))
                                dgvRigEquipmentShale.Rows[i].Cells[4].Style.BackColor = Color.Red;
                            critErr = new Errors(simpErr);
                        }

                        errs.Add(critErr);
                    }
                }

                InformationManager.Set_Info(errs.ToArray());

                if (errs.Count == 0)
                {
                    btnRigEqShaleApply.ImageKey = "Check";
                    btnRigEqShaleApply.BackColor = frmMain.checkColor;

                    //if (!LoadRigEquipmentsShale())
                    //{
                    //    //ErrorManager.Set_Error(new Errors("Can not load data from data base", "", METHOD));
                    //    ////?????logout????
                    //}
                }
            }
            catch (Exception)
            {
            }
        }
        //-------------------------------------------------------
        private void dgvRigEquipmentShale_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex == -1)
                return;

            if (skipCellValueChanged_dgvRigEqShale)
                return;

            btnRigEqShaleApply.ImageKey = "warning";
            btnRigEqShaleApply.BackColor = frmMain.warningColor;
        }
        //-------------------------------------------------------
        private void btnChooseSeniorMudEngFromPersonnelList_Click(object sender, EventArgs e)
        {
            if (frmMain.selectedRepID == -1)
                return;

            if (access.inventoryManagement_ManPower != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;

            PersonnelListDataFormDB_Selection frmPerSel = new PersonnelListDataFormDB_Selection();

            frmPerSel.dgvPerson.Columns[0].Visible = false;//single selection
            frmPerSel.selBtns.ShowSelectionButtons = false;
            frmPerSel.dgvPerson.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            if (frmPerSel.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;

            if (frmPerSel.dgvPerson.SelectedRows.Count == 0)
                return;

            int pID = Convert.ToInt32(frmPerSel.dgvPerson.Rows[frmPerSel.dgvPerson.SelectedRows[0].Index].Cells[1].Value);
            string pName = DGV_Operations.CellValueAsString(frmPerSel.dgvPerson.Rows[frmPerSel.dgvPerson.SelectedRows[0].Index].Cells[3]);

            lblSenMudEng.Text = pName;
            lblSenMudEng.Tag = pID;

            btnManPowApply.ImageKey = "warning";
            btnManPowApply.BackColor = frmMain.warningColor;
        }
        //-------------------------------------------------------
        private void btnChooseJuniorMudEngFromPersonnelLis_Click(object sender, EventArgs e)
        {
            if (frmMain.selectedRepID == -1)
                return;

            if (access.inventoryManagement_ManPower != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;

            PersonnelListDataFormDB_Selection frmPerSel = new PersonnelListDataFormDB_Selection();

            frmPerSel.dgvPerson.Columns[0].Visible = false;//single selection
            frmPerSel.selBtns.ShowSelectionButtons = false;
            frmPerSel.dgvPerson.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            if (frmPerSel.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;

            if (frmPerSel.dgvPerson.SelectedRows.Count == 0)
                return;

            int pID = Convert.ToInt32(frmPerSel.dgvPerson.Rows[frmPerSel.dgvPerson.SelectedRows[0].Index].Cells[1].Value);
            string pName = DGV_Operations.CellValueAsString(frmPerSel.dgvPerson.Rows[frmPerSel.dgvPerson.SelectedRows[0].Index].Cells[3]);

            lblJunMudEng.Text = pName;
            lblJunMudEng.Tag = pID;

            btnManPowApply.ImageKey = "warning";
            btnManPowApply.BackColor = frmMain.warningColor;
        }
        //-------------------------------------------------------
        private void btnPrevSenJunProjMudEng_Click(object sender, EventArgs e)
        {
            if (frmMain.selectedRepID == -1)
                return;

            if (access.inventoryManagement_ManPower != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;
            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            object[] dtRow = FetchTableData.GetFieldsOfLastRevOfPrevReport(frmMain.selectedRepNum, frmMain.selected_RW_WellID, " SenMudEngAutoID_InvMngManPow, JunMudEngAutoID_InvMngManPow, ProjEng_InvMngManPow ");

            if (dtRow == null || dtRow.Length != 3)
                return;
            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            lblSenMudEng.Text = "";
            lblSenMudEng.Tag = null;

            lblJunMudEng.Text = "";
            lblJunMudEng.Tag = null;

            txtProjEng.Text = "";
            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            if (dtRow[0] != DBNull.Value)
            {
                int code = 0;
                string name = "";
                string phone = "";

                int senAutoID = Convert.ToInt32(dtRow[0]);
                if (FetchTableData.GetPersonnelData(senAutoID, out code, out name, out phone))
                {
                    lblSenMudEng.Text = name;
                    lblSenMudEng.Tag = senAutoID;
                }
            }
            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            if (dtRow[1] != DBNull.Value)
            {
                int code = 0;
                string name = "";
                string phone = "";

                int senAutoID = Convert.ToInt32(dtRow[1]);
                if (FetchTableData.GetPersonnelData(senAutoID, out code, out name, out phone))
                {
                    lblJunMudEng.Text = name;
                    lblJunMudEng.Tag = senAutoID;
                }
            }
            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            txtProjEng.Text = dtRow[2].ToString();
            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            btnManPowApply.ImageKey = "warning";
            btnManPowApply.BackColor = frmMain.warningColor;
        }
        //-------------------------------------------------------
        private void btnPrevSenJunCompRepAndClientRep_Click(object sender, EventArgs e)
        {
            if (frmMain.selectedRepID == -1)
                return;

            if (access.inventoryManagement_ManPower != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;

            object[] dtRow = FetchTableData.GetFieldsOfLastRevOfPrevReport(frmMain.selectedRepNum, frmMain.selected_RW_WellID, " SenCmpRep_InvMngManPow, JunCmpRep_InvMngManPow,  ClientRep_InvMngManPow ");

            if (dtRow == null || dtRow.Length != 3)
                return;

            txtSenCompRep.Text = dtRow[0].ToString();
            txtJunCompRep.Text = dtRow[1].ToString();
            txtClientRep.Text = dtRow[2].ToString();
        }
        //-------------------------------------------------------
        private void btnPrevEngOpSupAndPhoneNumbers_Click(object sender, EventArgs e)
        {
            if (frmMain.selectedRepID == -1)
                return;

            if (access.inventoryManagement_ManPower != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;

            object[] dtRow = FetchTableData.GetFieldsOfLastRevOfPrevReport(frmMain.selectedRepNum, frmMain.selected_RW_WellID, " EngSup_InvMngManPow, OpSup_InvMngManPow, EngSupPhone_InvMngManPow, OpSupPhone_InvMngManPow ");

            if (dtRow == null || dtRow.Length != 4)
                return;

            txtEngSup.Text = dtRow[0].ToString();
            txtOpSup.Text = dtRow[1].ToString();
            txtEngSupPhoneNumber.Text = dtRow[2].ToString();
            txtOpSupPhoneNumber.Text = dtRow[3].ToString();
        }
        //-------------------------------------------------------
        private void txt_TextChanged(object sender, EventArgs e)
        {
            if (frmMain.selectedRepID == -1)
                return;

            if (access.inventoryManagement_ManPower != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;

            btnManPowApply.ImageKey = "warning";
            btnManPowApply.BackColor = frmMain.warningColor;
        }
        //-------------------------------------------------------
        private void cboxWarehouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (frmMain.selectedRepID == -1)
                return;

            if (access.inventoryManagement_ManPower != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;

            btnManPowApply.ImageKey = "warning";
            btnManPowApply.BackColor = frmMain.warningColor;
        }
        //-------------------------------------------------------
        private void btnManPowRefresh_Click(object sender, EventArgs e)
        {
            //string METHOD = "btnManPowRefresh_Click : " + CLASS_NAME;

            if (frmMain.selectedRepID == -1)
                return;

            if (access.inventoryManagement_ManPower == RoleAccess.AcessTypes.AT_None)
                return;

            LoadManPowerTab();

            btnManPowApply.ImageKey = "check";
            btnManPowApply.BackColor = frmMain.checkColor;
        }
        //-------------------------------------------------------
        private void btnManPowApply_Click(object sender, EventArgs e)
        {
            if (frmMain.selected_RW_WellID == -1)
                return;

            if (access.inventoryManagement_ManPower != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;

            string warehouse = cboxWarehouse.SelectedIndex != -1 ? cboxWarehouse.SelectedItem.ToString() : "";

            List<ProcedureParam> prms = new List<ProcedureParam>();
            prms.Add(new ProcedureParam("@ReportID", frmMain.selectedRepID.ToString(), ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Report"));

            if (lblSenMudEng.Tag == null)
                prms.Add(new ProcedureParam("@SenMudEngAutoID_InvMngManPow", ProcedureParam.ParamType.PT_Int));
            else
                prms.Add(new ProcedureParam("@SenMudEngAutoID_InvMngManPow", lblSenMudEng.Tag.ToString(), ProcedureParam.ParamType.PT_Int, true, "1", false, "", "Invalid Sen. Mud Eng."));

            if (lblJunMudEng.Tag == null)
                prms.Add(new ProcedureParam("@JunMudEngAutoID_InvMngManPow", ProcedureParam.ParamType.PT_Int));
            else
                prms.Add(new ProcedureParam("@JunMudEngAutoID_InvMngManPow", lblJunMudEng.Tag.ToString(), ProcedureParam.ParamType.PT_Int, true, "1", false, "", "Invalid Jun. Mud Eng."));


            prms.Add(new ProcedureParam("@ProjEng_InvMngManPow", txtProjEng.Text, 30));
            prms.Add(new ProcedureParam("@SenCmpRep_InvMngManPow", txtSenCompRep.Text, 30));
            prms.Add(new ProcedureParam("@JunCmpRep_InvMngManPow", txtJunCompRep.Text, 30));
            prms.Add(new ProcedureParam("@ClientRep_InvMngManPow", txtClientRep.Text, 30));
            prms.Add(new ProcedureParam("@EngSup_InvMngManPow", txtEngSup.Text, 30));
            prms.Add(new ProcedureParam("@OpSup_InvMngManPow", txtOpSup.Text, 30));
            prms.Add(new ProcedureParam("@EngSupPhone_InvMngManPow", txtEngSupPhoneNumber.Text, 30));
            prms.Add(new ProcedureParam("@OpSupPhone_InvMngManPow", txtOpSupPhoneNumber.Text, 30));
            prms.Add(new ProcedureParam("@WareHouse_InvMngManPow", warehouse, 30));
            prms.Add(new ProcedureParam("@WareHousePhone_InvMngManPow", txtWarehousePhonenumber.Text, 30));

            string simpErr;
            Errors critErr;
            Int64 resultStat;

            int ret = Procedures.CallProcedure("prc_at_Report_update_InvMan_ManPow", prms, out simpErr, out critErr, out resultStat);

            if (ret != 0)
            {
                if (ret == 1)
                    InformationManager.Set_Info(simpErr);
                else
                    InformationManager.Set_Info(critErr);

                return;
            }

            btnManPowApply.ImageKey = "Check";
            btnManPowApply.BackColor = frmMain.checkColor;
        }
        //-------------------------------------------------------
        private void btnLabEqInvRefresh_Click(object sender, EventArgs e)
        {
            string METHOD = "btnLabEqInvRefresh_Click : " + CLASS_NAME;

            if (frmMain.selectedRepID == -1)
                return;

            if (access.inventoryManagement_LabEquipmentInventory == RoleAccess.AcessTypes.AT_None)
                return;

            if (!LoadLabEquipment())
            {
                InformationManager.Set_Info(new Errors("Can not load data from data base", "", METHOD));
                //?????logout????
            }

            btnLabEqInvApply.ImageKey = "check";
            btnLabEqInvApply.BackColor = frmMain.checkColor;
        }
        //-------------------------------------------------------
        private void btnLabEqInvRemove_Click(object sender, EventArgs e)
        {
            if (frmMain.selectedRepID == -1)
                return;

            if (access.inventoryManagement_LabEquipmentInventory != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;

            if (dgvLabEqInv.SelectedRows.Count == 0)
            {
                InformationManager.Set_Info("Select One Row to be Deleted");
                return;
            }

            string idStr = DGV_Operations.CellValueAsString(dgvLabEqInv.Rows[dgvLabEqInv.SelectedRows[0].Index].Cells[0]);

            List<ProcedureParam> prms = new List<ProcedureParam>();
            prms.Add(new ProcedureParam("@ID", idStr, ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Record"));


            string simpErr;
            Errors critErr;
            Int64 resultStat;

            int ret = Procedures.CallProcedure("prc_rt_Rep2InvManLabEq_delete", prms, out simpErr, out critErr, out resultStat);

            if (ret != 0)
            {
                if (ret == 1)
                    InformationManager.Set_Info(simpErr);
                else
                    InformationManager.Set_Info(critErr);

                return;
            }

            if (!LoadLabEquipment())
            {
                //ErrorManager.Set_Error(new Errors("Can not load data from data base", "", METHOD));
                ////?????logout????
            }
        }
        //-------------------------------------------------------
        private void btnLabEqInvApply_Click(object sender, EventArgs e)
        {
            if (frmMain.selectedRepID == -1)
                return;

            if (access.inventoryManagement_LabEquipmentInventory != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;

            List<Errors> errs = new List<Errors>();

            for (int i = 0; i < dgvLabEqInv.Rows.Count; i++)
            {
                string idStr = DGV_Operations.CellValueAsString(dgvLabEqInv.Rows[i].Cells[0]);

                List<ProcedureParam> prms = new List<ProcedureParam>();
                prms.Add(new ProcedureParam("@ID", idStr, ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Record"));
                //~~~~~~~~~~	
                int colID = 4;
                prms.Add(new ProcedureParam("@ReqQuantity", DGV_Operations.CellValueAsString(dgvLabEqInv.Rows[i].Cells[colID++]), ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Request Quantity"));
                prms.Add(new ProcedureParam("@ReqOrdNum", DGV_Operations.CellValueAsString(dgvLabEqInv.Rows[i].Cells[colID++]), 30));
                prms.Add(new ProcedureParam("@ReqOrdTime", DGV_Operations.CellValueAsString(dgvLabEqInv.Rows[i].Cells[colID++]), 30));
                prms.Add(new ProcedureParam("@ReqDelivType", DGV_Operations.CellValueAsString(dgvLabEqInv.Rows[i].Cells[colID++]), 50));
                prms.Add(new ProcedureParam("@ReqMSC", DGV_Operations.CellValueAsString(dgvLabEqInv.Rows[i].Cells[colID++]), 50));
                prms.Add(new ProcedureParam("@ReqComment", DGV_Operations.CellValueAsString(dgvLabEqInv.Rows[i].Cells[colID++]), 150));
                //~~~~~~~~~~
                colID = 11;
                prms.Add(new ProcedureParam("@RecQuantity", DGV_Operations.CellValueAsString(dgvLabEqInv.Rows[i].Cells[colID++]), ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Receive Quantity"));
                prms.Add(new ProcedureParam("@RecPropNum", DGV_Operations.CellValueAsString(dgvLabEqInv.Rows[i].Cells[colID++]), 30));
                prms.Add(new ProcedureParam("@RecSN", DGV_Operations.CellValueAsString(dgvLabEqInv.Rows[i].Cells[colID++]), 50));
                prms.Add(new ProcedureParam("@RecSrc", DGV_Operations.CellValueAsString(dgvLabEqInv.Rows[i].Cells[colID++]), 50));
                prms.Add(new ProcedureParam("@RecCalibrationStatus", DGV_Operations.CellValueAsString(dgvLabEqInv.Rows[i].Cells[colID++]), 50));
                prms.Add(new ProcedureParam("@RecComment", DGV_Operations.CellValueAsString(dgvLabEqInv.Rows[i].Cells[colID++]), 150));
                //~~~~~~~~~~
                colID = 18;
                prms.Add(new ProcedureParam("@BkLoadQuantity", DGV_Operations.CellValueAsString(dgvLabEqInv.Rows[i].Cells[colID++]), ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Backload Quantity"));
                prms.Add(new ProcedureParam("@BkLoadPropNum", DGV_Operations.CellValueAsString(dgvLabEqInv.Rows[i].Cells[colID++]), 30));
                prms.Add(new ProcedureParam("@BkLoadSN", DGV_Operations.CellValueAsString(dgvLabEqInv.Rows[i].Cells[colID++]), 50));
                prms.Add(new ProcedureParam("@BkLoadReqNum", DGV_Operations.CellValueAsString(dgvLabEqInv.Rows[i].Cells[colID++]), 30));
                prms.Add(new ProcedureParam("@BkLoadTime", DGV_Operations.CellValueAsString(dgvLabEqInv.Rows[i].Cells[colID++]), 30));
                prms.Add(new ProcedureParam("@BkLoadDest", DGV_Operations.CellValueAsString(dgvLabEqInv.Rows[i].Cells[colID++]), 50));
                prms.Add(new ProcedureParam("@BkLoadCalibrationStatus", DGV_Operations.CellValueAsString(dgvLabEqInv.Rows[i].Cells[colID++]), 50));
                prms.Add(new ProcedureParam("@BkLoadComment", DGV_Operations.CellValueAsString(dgvLabEqInv.Rows[i].Cells[colID++]), 150));
                //~~~~~~~~~~
                colID = 27;
                prms.Add(new ProcedureParam("@MovQuantity", DGV_Operations.CellValueAsString(dgvLabEqInv.Rows[i].Cells[colID++]), ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Movement Quantity"));
                prms.Add(new ProcedureParam("@MovPropNum", DGV_Operations.CellValueAsString(dgvLabEqInv.Rows[i].Cells[colID++]), 30));
                prms.Add(new ProcedureParam("@MovSN", DGV_Operations.CellValueAsString(dgvLabEqInv.Rows[i].Cells[colID++]), 50));
                prms.Add(new ProcedureParam("@MovReqNum", DGV_Operations.CellValueAsString(dgvLabEqInv.Rows[i].Cells[colID++]), 30));
                prms.Add(new ProcedureParam("@MovTime", DGV_Operations.CellValueAsString(dgvLabEqInv.Rows[i].Cells[colID++]), 30));
                prms.Add(new ProcedureParam("@MovDest", DGV_Operations.CellValueAsString(dgvLabEqInv.Rows[i].Cells[colID++]), 50));
                prms.Add(new ProcedureParam("@MovCalibrationStatus", DGV_Operations.CellValueAsString(dgvLabEqInv.Rows[i].Cells[colID++]), 50));
                prms.Add(new ProcedureParam("@MovComment", DGV_Operations.CellValueAsString(dgvLabEqInv.Rows[i].Cells[colID++]), 150));
                //~~~~~~~~~~
                colID = 36;
                prms.Add(new ProcedureParam("@StockOnTruck_Boat", DGV_Operations.CellValueAsString(dgvLabEqInv.Rows[i].Cells[colID++]), ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Stock on Truck/Boat"));

                string simpErr;
                Errors critErr;
                Int64 resultStat;

                int ret = Procedures.CallProcedure("prc_rt_Rep2InvManLabEq_update", prms, out simpErr, out critErr, out resultStat);

                Color normalBkColor = (i % 2 == 0) ? dgvLabEqInv.BackgroundColor : dgvLabEqInv.AlternatingRowsDefaultCellStyle.BackColor;

                dgvLabEqInv.Rows[i].Cells[2].Style.BackColor = normalBkColor;
                dgvLabEqInv.Rows[i].Cells[9].Style.BackColor = normalBkColor;
                dgvLabEqInv.Rows[i].Cells[16].Style.BackColor = normalBkColor;
                dgvLabEqInv.Rows[i].Cells[25].Style.BackColor = normalBkColor;
                dgvLabEqInv.Rows[i].Cells[35].Style.BackColor = normalBkColor;

                if (ret != 0)
                {
                    if (ret == 1)
                    {
                        if (simpErr.StartsWith("Invalid Request Quantity"))
                            dgvLabEqInv.Rows[i].Cells[2].Style.BackColor = Color.Red;
                        else if (simpErr.StartsWith("Invalid Receive Quantity"))
                            dgvLabEqInv.Rows[i].Cells[9].Style.BackColor = Color.Red;
                        else if (simpErr.StartsWith("Invalid Backload Quantity"))
                            dgvLabEqInv.Rows[i].Cells[16].Style.BackColor = Color.Red;
                        else if (simpErr.StartsWith("Invalid Movement Quantity"))
                            dgvLabEqInv.Rows[i].Cells[25].Style.BackColor = Color.Red;
                        else if (simpErr.StartsWith("Invalid Stock on Truck/Boat"))
                            dgvLabEqInv.Rows[i].Cells[35].Style.BackColor = Color.Red;

                        critErr = new Errors(simpErr);
                    }

                    errs.Add(critErr);
                }
            }

            InformationManager.Set_Info(errs.ToArray());

            if (errs.Count == 0)
            {
                for (int i = 0; i < dgvLabEqInv.Rows.Count; i++)
                    ComputeLabEquipmentOutstanding(i);

                btnLabEqInvApply.ImageKey = "Check";
                btnLabEqInvApply.BackColor = frmMain.checkColor;
            }
        }
        //-------------------------------------------------------
        private void btnLabEqInvAdd_Click(object sender, EventArgs e)
        {
            string METHOD = "btnLabEqInvAdd_Click : " + CLASS_NAME;

            if (frmMain.selectedRepID == -1)
                return;

            if (access.inventoryManagement_LabEquipmentInventory != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;


            string query =
                        "  select 0, AutoID, Code, Name   "
                        + "  from  lt_LabEquipments  where AutoID not in     "
                        + "  (select Eq_AutoID from rt_Rep2InvManLabEq where ReportID = " + frmMain.selectedRepID.ToString() + " ) ";

            LabEquipmentListFormDB_Selection selLabEq = new LabEquipmentListFormDB_Selection(query);
            if (selLabEq.ShowDialog() != DialogResult.OK)
                return;

            List<Errors> errs = new List<Errors>();

            for (int i = 0; i < selLabEq.dgvLabEq.Rows.Count; i++)
            {
                if (Convert.ToBoolean(selLabEq.dgvLabEq.Rows[i].Cells[0].Value) == true)
                {

                    List<ProcedureParam> prms = new List<ProcedureParam>();
                    prms.Add(new ProcedureParam("@Eq_AutoID", DGV_Operations.CellValueAsString(selLabEq.dgvLabEq.Rows[i].Cells[1]), ProcedureParam.ParamType.PT_Int, true, "1", false, "", "Invalid Eq. Record"));
                    prms.Add(new ProcedureParam("@ReportID", frmMain.selectedRepID.ToString(), ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Report"));
                    prms.Add(new ProcedureParam("@ProjAutoID", frmMain.selectedPrjID.ToString(), ProcedureParam.ParamType.PT_Int, true, "1", false, "", "Invalid Project"));

                    string simpErr;
                    Errors critErr;
                    Int64 resultStat;

                    int ret = Procedures.CallProcedure("prc_rt_Rep2InvManLabEq_insert", prms, out simpErr, out critErr, out resultStat);

                    if (ret != 0)
                    {
                        if (ret == 1) critErr = new Errors(simpErr);

                        errs.Add(critErr);
                    }
                }
            }

            InformationManager.Set_Info(errs.ToArray());

            if (!LoadLabEquipment())
            {
                InformationManager.Set_Info(new Errors("Can not load data from data base", "", METHOD));
                //?????logout????
            }

            btnLabEqInvApply.ImageKey = "Check";
            btnLabEqInvApply.BackColor = frmMain.checkColor;
        }
        //-------------------------------------------------------
        private void dgvLabEqInv_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex == -1)
                return;

            if (skipCellValueChanged_dgvLabEqInv)
                return;

            if (e.ColumnIndex < 0 || e.RowIndex < 0)
                return;

            //if (e.ColumnIndex == 2 || e.ColumnIndex == 9 || e.ColumnIndex == 16 || e.ColumnIndex == 25)
            //    ComputeLabEquipmentOutstanding(e.RowIndex);

            btnLabEqInvApply.ImageKey = "warning";
            btnLabEqInvApply.BackColor = frmMain.warningColor;
        }
        //-------------------------------------------------------
        private void dgvLabEqInv_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (frmMain.selectedRepID == -1)
                return;

            if (access.inventoryManagement_ProductInventory != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;

            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                frmMultiInput fm = new frmMultiInput();
                fm.Height = 200;

                if (e.ColumnIndex == 3)
                {
                    fm.Width = 600;
                    fm.lblTitle.Text = "Request:";

                    fm.dgvInput.Columns.Add("1", "Quantity");
                    fm.dgvInput.Columns.Add("2", "Order #");
                    fm.dgvInput.Columns.Add("3", "Order Time");
                    fm.dgvInput.Columns.Add("4", "Delivery Type");
                    fm.dgvInput.Columns.Add("5", "Material Supplier Code (M.S.C)");
                    fm.dgvInput.Columns.Add("6", "Comment");
                }
                else if (e.ColumnIndex == 10)
                {
                    fm.Width = 600;
                    fm.lblTitle.Text = "Received:";

                    fm.dgvInput.Columns.Add("1", "Quantity");
                    fm.dgvInput.Columns.Add("2", "Prop #");
                    fm.dgvInput.Columns.Add("3", "S/N");
                    fm.dgvInput.Columns.Add("4", "Source");
                    fm.dgvInput.Columns.Add("5", "Calibration Status");
                    fm.dgvInput.Columns.Add("6", "Comment");
                }
                else if (e.ColumnIndex == 17)
                {
                    fm.Width = 650;
                    fm.lblTitle.Text = "Backload:";

                    fm.dgvInput.Columns.Add("1", "Quantity");
                    fm.dgvInput.Columns.Add("2", "Prop #");
                    fm.dgvInput.Columns.Add("3", "S/N");
                    fm.dgvInput.Columns.Add("4", "Req. #");
                    fm.dgvInput.Columns.Add("5", "Time");
                    fm.dgvInput.Columns.Add("6", "Destination");
                    fm.dgvInput.Columns.Add("7", "Calibration Status");
                    fm.dgvInput.Columns.Add("8", "Comment");
                }
                else if (e.ColumnIndex == 26)
                {
                    fm.Width = 650;
                    fm.lblTitle.Text = "Movement:";

                    fm.dgvInput.Columns.Add("1", "Quantity");
                    fm.dgvInput.Columns.Add("2", "Prop #");
                    fm.dgvInput.Columns.Add("3", "S/N");
                    fm.dgvInput.Columns.Add("4", "Req. #");
                    fm.dgvInput.Columns.Add("5", "Time");
                    fm.dgvInput.Columns.Add("6", "Destination");
                    fm.dgvInput.Columns.Add("7", "Calibration Status");
                    fm.dgvInput.Columns.Add("8", "Comment");
                }
                else
                {
                    return;
                }


                int startColID = e.ColumnIndex + 1;

                for (int i = 0; i < fm.dgvInput.Columns.Count; i++)
                {
                    fm.dgvInput.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    fm.dgvInput.Columns[i].SortMode = DataGridViewColumnSortMode.Programmatic;
                }

                fm.dgvInput.AllowUserToAddRows = false;
                fm.dgvInput.AllowUserToDeleteRows = false;
                fm.dgvInput.RowHeadersVisible = false;
                //~~~~~~~~~~~~~~~~~~~~~~
                fm.dgvInput.Rows.Add();
                //~~~~~~~~~~~~~~~~~~~~~~
                for (int i = 0; i < fm.dgvInput.Columns.Count; i++)
                    fm.dgvInput[i, 0].Value = dgvLabEqInv[startColID + i, e.RowIndex].Value;
                //~~~~~~~~~~~~~~~~~~~~~~
                DialogResult dlgRes = fm.ShowDialog();

                if (dlgRes != System.Windows.Forms.DialogResult.OK)
                    return;
                //~~~~~~~~~~~~~~~~~~~~~~
                for (int i = 0; i < fm.dgvInput.Columns.Count; i++)
                    dgvLabEqInv[startColID + i, e.RowIndex].Value = fm.dgvInput[i, 0].Value;

                dgvLabEqInv[startColID - 1, e.RowIndex].Value = dgvLabEqInv[startColID, e.RowIndex].Value;

                //ComputeLabEquipmentOutstanding(e.RowIndex);
            }
        }
        //-------------------------------------------------------
        private void btnHSERefresh_Click(object sender, EventArgs e)
        {
            string METHOD = "btnHSERefresh_Click : " + CLASS_NAME;

            if (frmMain.selectedRepID == -1)
                return;

            if (access.inventoryManagement_HSE_PPE == RoleAccess.AcessTypes.AT_None)
                return;

            if (!LoadHSEPPE())
            {
                InformationManager.Set_Info(new Errors("Can not load data from data base", "", METHOD));
                //?????logout????
            }

            btnHSEApply.ImageKey = "check";
            btnHSEApply.BackColor = frmMain.checkColor;
        }
        //-------------------------------------------------------
        private void btnHSERemove_Click(object sender, EventArgs e)
        {
            if (frmMain.selectedRepID == -1)
                return;

            if (access.inventoryManagement_HSE_PPE != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;

            if (dgvHSE.SelectedRows.Count == 0)
            {
                InformationManager.Set_Info("Select One Row to be Deleted");
                return;
            }

            string idStr = DGV_Operations.CellValueAsString(dgvHSE.Rows[dgvHSE.SelectedRows[0].Index].Cells[0]);

            List<ProcedureParam> prms = new List<ProcedureParam>();
            prms.Add(new ProcedureParam("@ID", idStr, ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Record"));


            string simpErr;
            Errors critErr;
            Int64 resultStat;

            int ret = Procedures.CallProcedure("prc_rt_Rep2InvManHSE_delete", prms, out simpErr, out critErr, out resultStat);

            if (ret != 0)
            {
                if (ret == 1)
                    InformationManager.Set_Info(simpErr);
                else
                    InformationManager.Set_Info(critErr);

                return;
            }

            if (!LoadHSEPPE())
            {
                //ErrorManager.Set_Error(new Errors("Can not load data from data base", "", METHOD));
                ////?????logout????
            }
        }
        //-------------------------------------------------------
        private void btnHSEApply_Click(object sender, EventArgs e)
        {
            if (frmMain.selectedRepID == -1)
                return;

            if (access.inventoryManagement_HSE_PPE != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;

            List<Errors> errs = new List<Errors>();

            for (int i = 0; i < dgvHSE.Rows.Count; i++)
            {
                string idStr = DGV_Operations.CellValueAsString(dgvHSE.Rows[i].Cells[0]);

                List<ProcedureParam> prms = new List<ProcedureParam>();
                prms.Add(new ProcedureParam("@ID", idStr, ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Record"));
                //~~~~~~~~~~	
                int colID = 4;
                prms.Add(new ProcedureParam("@ReqQuantity", DGV_Operations.CellValueAsString(dgvHSE.Rows[i].Cells[colID++]), ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Request Quantity"));
                prms.Add(new ProcedureParam("@ReqOrdNum", DGV_Operations.CellValueAsString(dgvHSE.Rows[i].Cells[colID++]), 30));
                prms.Add(new ProcedureParam("@ReqOrdTime", DGV_Operations.CellValueAsString(dgvHSE.Rows[i].Cells[colID++]), 30));
                prms.Add(new ProcedureParam("@ReqDelivType", DGV_Operations.CellValueAsString(dgvHSE.Rows[i].Cells[colID++]), 50));
                prms.Add(new ProcedureParam("@ReqMSC", DGV_Operations.CellValueAsString(dgvHSE.Rows[i].Cells[colID++]), 50));
                prms.Add(new ProcedureParam("@ReqComment", DGV_Operations.CellValueAsString(dgvHSE.Rows[i].Cells[colID++]), 150));
                //~~~~~~~~~~
                colID = 11;
                prms.Add(new ProcedureParam("@RecQuantity", DGV_Operations.CellValueAsString(dgvHSE.Rows[i].Cells[colID++]), ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Receive Quantity"));
                prms.Add(new ProcedureParam("@RecPropNum", DGV_Operations.CellValueAsString(dgvHSE.Rows[i].Cells[colID++]), 30));
                prms.Add(new ProcedureParam("@RecSN", DGV_Operations.CellValueAsString(dgvHSE.Rows[i].Cells[colID++]), 50));
                prms.Add(new ProcedureParam("@RecSrc", DGV_Operations.CellValueAsString(dgvHSE.Rows[i].Cells[colID++]), 50));
                prms.Add(new ProcedureParam("@RecComment", DGV_Operations.CellValueAsString(dgvHSE.Rows[i].Cells[colID++]), 150));
                //~~~~~~~~~~
                colID = 17;
                prms.Add(new ProcedureParam("@BkLoadQuantity", DGV_Operations.CellValueAsString(dgvHSE.Rows[i].Cells[colID++]), ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Backload Quantity"));
                prms.Add(new ProcedureParam("@BkLoadPropNum", DGV_Operations.CellValueAsString(dgvHSE.Rows[i].Cells[colID++]), 30));
                prms.Add(new ProcedureParam("@BkLoadSN", DGV_Operations.CellValueAsString(dgvHSE.Rows[i].Cells[colID++]), 50));
                prms.Add(new ProcedureParam("@BkLoadReqNum", DGV_Operations.CellValueAsString(dgvHSE.Rows[i].Cells[colID++]), 30));
                prms.Add(new ProcedureParam("@BkLoadTime", DGV_Operations.CellValueAsString(dgvHSE.Rows[i].Cells[colID++]), 30));
                prms.Add(new ProcedureParam("@BkLoadDest", DGV_Operations.CellValueAsString(dgvHSE.Rows[i].Cells[colID++]), 30));
                prms.Add(new ProcedureParam("@BkLoadComment", DGV_Operations.CellValueAsString(dgvHSE.Rows[i].Cells[colID++]), 150));
                //~~~~~~~~~~
                colID = 25;
                prms.Add(new ProcedureParam("@MovQuantity", DGV_Operations.CellValueAsString(dgvHSE.Rows[i].Cells[colID++]), ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Movement Quantity"));
                prms.Add(new ProcedureParam("@MovPropNum", DGV_Operations.CellValueAsString(dgvHSE.Rows[i].Cells[colID++]), 30));
                prms.Add(new ProcedureParam("@MovSN", DGV_Operations.CellValueAsString(dgvHSE.Rows[i].Cells[colID++]), 50));
                prms.Add(new ProcedureParam("@MovReqNum", DGV_Operations.CellValueAsString(dgvHSE.Rows[i].Cells[colID++]), 30));
                prms.Add(new ProcedureParam("@MovTime", DGV_Operations.CellValueAsString(dgvHSE.Rows[i].Cells[colID++]), 30));
                prms.Add(new ProcedureParam("@MovDest", DGV_Operations.CellValueAsString(dgvHSE.Rows[i].Cells[colID++]), 50));
                prms.Add(new ProcedureParam("@MovComment", DGV_Operations.CellValueAsString(dgvHSE.Rows[i].Cells[colID++]), 150));
                //~~~~~~~~~~
                colID = 33;
                prms.Add(new ProcedureParam("@StockOnTruck_Boat", DGV_Operations.CellValueAsString(dgvHSE.Rows[i].Cells[colID++]), ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Stock on Truck/Boat"));


                string simpErr;
                Errors critErr;
                Int64 resultStat;

                int ret = Procedures.CallProcedure("prc_rt_Rep2InvManHSE_update", prms, out simpErr, out critErr, out resultStat);

                Color normalBkColor = (i % 2 == 0) ? dgvHSE.BackgroundColor : dgvHSE.AlternatingRowsDefaultCellStyle.BackColor;

                dgvHSE.Rows[i].Cells[2].Style.BackColor = normalBkColor;
                dgvHSE.Rows[i].Cells[9].Style.BackColor = normalBkColor;
                dgvHSE.Rows[i].Cells[15].Style.BackColor = normalBkColor;
                dgvHSE.Rows[i].Cells[23].Style.BackColor = normalBkColor;
                dgvHSE.Rows[i].Cells[32].Style.BackColor = normalBkColor;

                if (ret != 0)
                {
                    if (ret == 1)
                    {
                        if (simpErr.StartsWith("Invalid Request Quantity"))
                            dgvHSE.Rows[i].Cells[2].Style.BackColor = Color.Red;
                        else if (simpErr.StartsWith("Invalid Receive Quantity"))
                            dgvHSE.Rows[i].Cells[9].Style.BackColor = Color.Red;
                        else if (simpErr.StartsWith("Invalid Backload Quantity"))
                            dgvHSE.Rows[i].Cells[15].Style.BackColor = Color.Red;
                        else if (simpErr.StartsWith("Invalid Movement Quantity"))
                            dgvHSE.Rows[i].Cells[23].Style.BackColor = Color.Red;
                        else if (simpErr.StartsWith("Invalid Stock on Truck/Boat"))
                            dgvHSE.Rows[i].Cells[32].Style.BackColor = Color.Red;

                        critErr = new Errors(simpErr);
                    }

                    errs.Add(critErr);
                }
            }

            InformationManager.Set_Info(errs.ToArray());

            if (errs.Count == 0)
            {
                for (int i = 0; i < dgvHSE.Rows.Count; i++)
                    ComputeHSEPPEOutstanding(i);
                
                btnHSEApply.ImageKey = "Check";
                btnHSEApply.BackColor = frmMain.checkColor;
            }
        }
        //-------------------------------------------------------
        private void btnHSEAdd_Click(object sender, EventArgs e)
        {
            string METHOD = "btnHSEAdd_Click : " + CLASS_NAME;

            if (frmMain.selectedRepID == -1)
                return;

            if (access.inventoryManagement_HSE_PPE != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;


            string query =
                        "  select 0, AutoID, HSECode, Name   "
                        + "  from  lt_HSE_PPE  where AutoID not in     "
                        + "  (select HSE_AutoID from rt_Rep2InvManHSE where ReportID = " + frmMain.selectedRepID.ToString() + " ) ";

            HSE_PPE_DataFormDB_Selection selHSE = new HSE_PPE_DataFormDB_Selection(query);
            if (selHSE.ShowDialog() != DialogResult.OK)
                return;

            List<Errors> errs = new List<Errors>();

            for (int i = 0; i < selHSE.dgvHSE.Rows.Count; i++)
            {
                if (Convert.ToBoolean(selHSE.dgvHSE.Rows[i].Cells[0].Value) == true)
                {

                    List<ProcedureParam> prms = new List<ProcedureParam>();
                    prms.Add(new ProcedureParam("@HSE_AutoID", DGV_Operations.CellValueAsString(selHSE.dgvHSE.Rows[i].Cells[1]), ProcedureParam.ParamType.PT_Int, true, "1", false, "", "Invalid HSE Code"));
                    prms.Add(new ProcedureParam("@ReportID", frmMain.selectedRepID.ToString(), ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Report"));
                    prms.Add(new ProcedureParam("@ProjAutoID", frmMain.selectedPrjID.ToString(), ProcedureParam.ParamType.PT_Int, true, "1", false, "", "Invalid Project"));

                    string simpErr;
                    Errors critErr;
                    Int64 resultStat;

                    int ret = Procedures.CallProcedure("prc_rt_Rep2InvManHSE_insert", prms, out simpErr, out critErr, out resultStat);

                    if (ret != 0)
                    {
                        if (ret == 1) critErr = new Errors(simpErr);

                        errs.Add(critErr);
                    }
                }
            }

            InformationManager.Set_Info(errs.ToArray());

            if (!LoadHSEPPE())
            {
                InformationManager.Set_Info(new Errors("Can not load data from data base", "", METHOD));
                //?????logout????
            }

            btnHSEApply.ImageKey = "Check";
            btnHSEApply.BackColor = frmMain.checkColor;
        }
        //-------------------------------------------------------
        private void dgvHSE_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex == -1)
                return;

            if (skipCellValueChanged_dgvHSE)
                return;

            if (e.ColumnIndex < 0 || e.RowIndex < 0)
                return;

            //if (e.ColumnIndex == 2 || e.ColumnIndex == 9 || e.ColumnIndex == 15 || e.ColumnIndex == 23)
            //    ComputeHSEPPEOutstanding(e.RowIndex);

            btnHSEApply.ImageKey = "warning";
            btnHSEApply.BackColor = frmMain.warningColor;
        }
        //-------------------------------------------------------
        private void dgvHSE_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (frmMain.selectedRepID == -1)
                return;

            if (access.inventoryManagement_ProductInventory != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;

            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                frmMultiInput fm = new frmMultiInput();
                fm.Height = 200;

                if (e.ColumnIndex == 3)
                {
                    fm.Width = 600;
                    fm.lblTitle.Text = "Request:";

                    fm.dgvInput.Columns.Add("1", "Quantity");
                    fm.dgvInput.Columns.Add("2", "Order #");
                    fm.dgvInput.Columns.Add("3", "Order Time");
                    fm.dgvInput.Columns.Add("4", "Delivery Type");
                    fm.dgvInput.Columns.Add("5", "Material Supplier Code (M.S.C)");
                    fm.dgvInput.Columns.Add("6", "Comment");
                }
                else if (e.ColumnIndex == 10)
                {
                    fm.Width = 600;
                    fm.lblTitle.Text = "Received:";

                    fm.dgvInput.Columns.Add("1", "Quantity");
                    fm.dgvInput.Columns.Add("2", "Prop #");
                    fm.dgvInput.Columns.Add("3", "S/N");
                    fm.dgvInput.Columns.Add("4", "Source");
                    fm.dgvInput.Columns.Add("5", "Comment");
                }
                else if (e.ColumnIndex == 16)
                {
                    fm.Width = 650;
                    fm.lblTitle.Text = "Backload:";

                    fm.dgvInput.Columns.Add("1", "Quantity");
                    fm.dgvInput.Columns.Add("2", "Prop #");
                    fm.dgvInput.Columns.Add("3", "S/N");
                    fm.dgvInput.Columns.Add("4", "Req. #");
                    fm.dgvInput.Columns.Add("5", "Time");
                    fm.dgvInput.Columns.Add("6", "Destination");
                    fm.dgvInput.Columns.Add("7", "Comment");
                }
                else if (e.ColumnIndex == 24)
                {
                    fm.Width = 650;
                    fm.lblTitle.Text = "Movement:";

                    fm.dgvInput.Columns.Add("1", "Quantity");
                    fm.dgvInput.Columns.Add("2", "Prop #");
                    fm.dgvInput.Columns.Add("3", "S/N");
                    fm.dgvInput.Columns.Add("4", "Req. #");
                    fm.dgvInput.Columns.Add("5", "Time");
                    fm.dgvInput.Columns.Add("6", "Destination");
                    fm.dgvInput.Columns.Add("7", "Comment");
                }
                else
                {
                    return;
                }


                int startColID = e.ColumnIndex + 1;

                for (int i = 0; i < fm.dgvInput.Columns.Count; i++)
                {
                    fm.dgvInput.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    fm.dgvInput.Columns[i].SortMode = DataGridViewColumnSortMode.Programmatic;
                }

                fm.dgvInput.AllowUserToAddRows = false;
                fm.dgvInput.AllowUserToDeleteRows = false;
                fm.dgvInput.RowHeadersVisible = false;
                //~~~~~~~~~~~~~~~~~~~~~~
                fm.dgvInput.Rows.Add();
                //~~~~~~~~~~~~~~~~~~~~~~
                for (int i = 0; i < fm.dgvInput.Columns.Count; i++)
                    fm.dgvInput[i, 0].Value = dgvHSE[startColID + i, e.RowIndex].Value;
                //~~~~~~~~~~~~~~~~~~~~~~
                DialogResult dlgRes = fm.ShowDialog();

                if (dlgRes != System.Windows.Forms.DialogResult.OK)
                    return;
                //~~~~~~~~~~~~~~~~~~~~~~
                for (int i = 0; i < fm.dgvInput.Columns.Count; i++)
                    dgvHSE[startColID + i, e.RowIndex].Value = fm.dgvInput[i, 0].Value;

                dgvHSE[startColID - 1, e.RowIndex].Value = dgvHSE[startColID, e.RowIndex].Value;

                //ComputeHSEPPEOutstanding(e.RowIndex);
            }
        }
        //-------------------------------------------------------
        private void btnGeneralEqInvRefresh_Click(object sender, EventArgs e)
        {
            string METHOD = "btnGeneralEqInvRefresh_Click : " + CLASS_NAME;

            if (frmMain.selectedRepID == -1)
                return;

            if (access.inventoryManagement_GeneralEquipmentInventory == RoleAccess.AcessTypes.AT_None)
                return;

            if (!LoadGeneralEq())
            {
                InformationManager.Set_Info(new Errors("Can not load data from data base", "", METHOD));
                //?????logout????
            }

            btnGeneralEqInvApply.ImageKey = "check";
            btnGeneralEqInvApply.BackColor = frmMain.checkColor;
        }
        //-------------------------------------------------------
        private void btnGeneralEqInvRemove_Click(object sender, EventArgs e)
        {
            if (frmMain.selectedRepID == -1)
                return;

            if (access.inventoryManagement_GeneralEquipmentInventory != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;

            if (dgvGeneralEqInv.SelectedRows.Count == 0)
            {
                InformationManager.Set_Info("Select One Row to be Deleted");
                return;
            }

            string idStr = DGV_Operations.CellValueAsString(dgvGeneralEqInv.Rows[dgvGeneralEqInv.SelectedRows[0].Index].Cells[0]);

            List<ProcedureParam> prms = new List<ProcedureParam>();
            prms.Add(new ProcedureParam("@ID", idStr, ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Record"));


            string simpErr;
            Errors critErr;
            Int64 resultStat;

            int ret = Procedures.CallProcedure("prc_rt_Rep2InvManGeneralEq_delete", prms, out simpErr, out critErr, out resultStat);

            if (ret != 0)
            {
                if (ret == 1)
                    InformationManager.Set_Info(simpErr);
                else
                    InformationManager.Set_Info(critErr);

                return;
            }

            if (!LoadGeneralEq())
            {
                //ErrorManager.Set_Error(new Errors("Can not load data from data base", "", METHOD));
                ////?????logout????
            }
        }
        //-------------------------------------------------------

        private void btnGeneralEqInvApply_Click(object sender, EventArgs e)
        {
            if (frmMain.selectedRepID == -1)
                return;

            if (access.inventoryManagement_GeneralEquipmentInventory != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;

            List<Errors> errs = new List<Errors>();

            for (int i = 0; i < dgvGeneralEqInv.Rows.Count; i++)
            {
                string idStr = DGV_Operations.CellValueAsString(dgvGeneralEqInv.Rows[i].Cells[0]);

                List<ProcedureParam> prms = new List<ProcedureParam>();
                prms.Add(new ProcedureParam("@ID", idStr, ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Record"));
                //~~~~~~~~~~	
                int colID = 4;
                prms.Add(new ProcedureParam("@ReqQuantity", DGV_Operations.CellValueAsString(dgvGeneralEqInv.Rows[i].Cells[colID++]), ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Request Quantity"));
                prms.Add(new ProcedureParam("@ReqOrdNum", DGV_Operations.CellValueAsString(dgvGeneralEqInv.Rows[i].Cells[colID++]), 30));
                prms.Add(new ProcedureParam("@ReqOrdTime", DGV_Operations.CellValueAsString(dgvGeneralEqInv.Rows[i].Cells[colID++]), 30));
                prms.Add(new ProcedureParam("@ReqDelivType", DGV_Operations.CellValueAsString(dgvGeneralEqInv.Rows[i].Cells[colID++]), 50));
                prms.Add(new ProcedureParam("@ReqMSC", DGV_Operations.CellValueAsString(dgvGeneralEqInv.Rows[i].Cells[colID++]), 50));
                prms.Add(new ProcedureParam("@ReqComment", DGV_Operations.CellValueAsString(dgvGeneralEqInv.Rows[i].Cells[colID++]), 150));
                //~~~~~~~~~~
                colID = 11;
                prms.Add(new ProcedureParam("@RecQuantity", DGV_Operations.CellValueAsString(dgvGeneralEqInv.Rows[i].Cells[colID++]), ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Receive Quantity"));
                prms.Add(new ProcedureParam("@RecPropNum", DGV_Operations.CellValueAsString(dgvGeneralEqInv.Rows[i].Cells[colID++]), 30));
                prms.Add(new ProcedureParam("@RecSN", DGV_Operations.CellValueAsString(dgvGeneralEqInv.Rows[i].Cells[colID++]), 50));
                prms.Add(new ProcedureParam("@RecSrc", DGV_Operations.CellValueAsString(dgvGeneralEqInv.Rows[i].Cells[colID++]), 50));
                prms.Add(new ProcedureParam("@RecComment", DGV_Operations.CellValueAsString(dgvGeneralEqInv.Rows[i].Cells[colID++]), 150));
                //~~~~~~~~~~
                colID = 17;
                prms.Add(new ProcedureParam("@BkLoadQuantity", DGV_Operations.CellValueAsString(dgvGeneralEqInv.Rows[i].Cells[colID++]), ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Backload Quantity"));
                prms.Add(new ProcedureParam("@BkLoadPropNum", DGV_Operations.CellValueAsString(dgvGeneralEqInv.Rows[i].Cells[colID++]), 30));
                prms.Add(new ProcedureParam("@BkLoadSN", DGV_Operations.CellValueAsString(dgvGeneralEqInv.Rows[i].Cells[colID++]), 50));
                prms.Add(new ProcedureParam("@BkLoadReqNum", DGV_Operations.CellValueAsString(dgvGeneralEqInv.Rows[i].Cells[colID++]), 30));
                prms.Add(new ProcedureParam("@BkLoadTime", DGV_Operations.CellValueAsString(dgvGeneralEqInv.Rows[i].Cells[colID++]), 30));
                prms.Add(new ProcedureParam("@BkLoadDest", DGV_Operations.CellValueAsString(dgvGeneralEqInv.Rows[i].Cells[colID++]), 50));
                prms.Add(new ProcedureParam("@BkLoadComment", DGV_Operations.CellValueAsString(dgvGeneralEqInv.Rows[i].Cells[colID++]), 150));
                //~~~~~~~~~~
                colID = 25;
                prms.Add(new ProcedureParam("@MovQuantity", DGV_Operations.CellValueAsString(dgvGeneralEqInv.Rows[i].Cells[colID++]), ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Movement Quantity"));
                prms.Add(new ProcedureParam("@MovPropNum", DGV_Operations.CellValueAsString(dgvGeneralEqInv.Rows[i].Cells[colID++]), 30));
                prms.Add(new ProcedureParam("@MovSN", DGV_Operations.CellValueAsString(dgvGeneralEqInv.Rows[i].Cells[colID++]), 50));
                prms.Add(new ProcedureParam("@MovReqNum", DGV_Operations.CellValueAsString(dgvGeneralEqInv.Rows[i].Cells[colID++]), 30));
                prms.Add(new ProcedureParam("@MovTime", DGV_Operations.CellValueAsString(dgvGeneralEqInv.Rows[i].Cells[colID++]), 30));
                prms.Add(new ProcedureParam("@MovDest", DGV_Operations.CellValueAsString(dgvGeneralEqInv.Rows[i].Cells[colID++]), 50));
                prms.Add(new ProcedureParam("@MovComment", DGV_Operations.CellValueAsString(dgvGeneralEqInv.Rows[i].Cells[colID++]), 150));
                //~~~~~~~~~~
                colID = 33;
                prms.Add(new ProcedureParam("@StockOnTruck_Boat", DGV_Operations.CellValueAsString(dgvGeneralEqInv.Rows[i].Cells[colID++]), ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Stock on Truck/Boat"));


                string simpErr;
                Errors critErr;
                Int64 resultStat;

                int ret = Procedures.CallProcedure("prc_rt_Rep2InvManGeneralEq_update", prms, out simpErr, out critErr, out resultStat);

                Color normalBkColor = (i % 2 == 0) ? dgvGeneralEqInv.BackgroundColor : dgvGeneralEqInv.AlternatingRowsDefaultCellStyle.BackColor;

                dgvGeneralEqInv.Rows[i].Cells[2].Style.BackColor = normalBkColor;
                dgvGeneralEqInv.Rows[i].Cells[9].Style.BackColor = normalBkColor;
                dgvGeneralEqInv.Rows[i].Cells[15].Style.BackColor = normalBkColor;
                dgvGeneralEqInv.Rows[i].Cells[23].Style.BackColor = normalBkColor;
                dgvGeneralEqInv.Rows[i].Cells[32].Style.BackColor = normalBkColor;

                if (ret != 0)
                {
                    if (ret == 1)
                    {
                        if (simpErr.StartsWith("Invalid Request Quantity"))
                            dgvGeneralEqInv.Rows[i].Cells[2].Style.BackColor = Color.Red;
                        else if (simpErr.StartsWith("Invalid Receive Quantity"))
                            dgvGeneralEqInv.Rows[i].Cells[9].Style.BackColor = Color.Red;
                        else if (simpErr.StartsWith("Invalid Backload Quantity"))
                            dgvGeneralEqInv.Rows[i].Cells[15].Style.BackColor = Color.Red;
                        else if (simpErr.StartsWith("Invalid Movement Quantity"))
                            dgvGeneralEqInv.Rows[i].Cells[23].Style.BackColor = Color.Red;
                        else if (simpErr.StartsWith("Invalid Stock on Truck/Boat"))
                            dgvGeneralEqInv.Rows[i].Cells[32].Style.BackColor = Color.Red;

                        critErr = new Errors(simpErr);
                    }

                    errs.Add(critErr);
                }
            }

            InformationManager.Set_Info(errs.ToArray());

            if (errs.Count == 0)
            {
                for (int i = 0; i < dgvGeneralEqInv.Rows.Count; i++)
                    ComputeGeneralEqOutstanding(i);

                btnGeneralEqInvApply.ImageKey = "Check";
                btnGeneralEqInvApply.BackColor = frmMain.checkColor;
            }
        }
        //-------------------------------------------------------
        private void btnGeneralEqInvAdd_Click(object sender, EventArgs e)
        {
            string METHOD = "btnGeneralEqInvAdd_Click : " + CLASS_NAME;

            if (frmMain.selectedRepID == -1)
                return;

            if (access.inventoryManagement_GeneralEquipmentInventory != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;


            string query =
                        "  select 0, AutoID, Code, Name   "
                        + "  from  lt_GeneralEquipment  where AutoID not in     "
                        + "  (select GenEq_AutoID from rt_Rep2InvManGeneralEq where ReportID = " + frmMain.selectedRepID.ToString() + " ) ";

            LabEquipmentListFormDB_Selection selGenLabEq = new LabEquipmentListFormDB_Selection(query);
            if (selGenLabEq.ShowDialog() != DialogResult.OK)
                return;

            List<Errors> errs = new List<Errors>();

            for (int i = 0; i < selGenLabEq.dgvLabEq.Rows.Count; i++)
            {
                if (Convert.ToBoolean(selGenLabEq.dgvLabEq.Rows[i].Cells[0].Value) == true)
                {

                    List<ProcedureParam> prms = new List<ProcedureParam>();
                    prms.Add(new ProcedureParam("@GenEq_AutoID", DGV_Operations.CellValueAsString(selGenLabEq.dgvLabEq.Rows[i].Cells[1]), ProcedureParam.ParamType.PT_Int, true, "1", false, "", "Invalid General Eq. Code"));
                    prms.Add(new ProcedureParam("@ReportID", frmMain.selectedRepID.ToString(), ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Report"));
                    prms.Add(new ProcedureParam("@ProjAutoID", frmMain.selectedPrjID.ToString(), ProcedureParam.ParamType.PT_Int, true, "1", false, "", "Invalid Project"));

                    string simpErr;
                    Errors critErr;
                    Int64 resultStat;

                    int ret = Procedures.CallProcedure("prc_rt_Rep2InvManGeneralEq_insert", prms, out simpErr, out critErr, out resultStat);

                    if (ret != 0)
                    {
                        if (ret == 1) critErr = new Errors(simpErr);

                        errs.Add(critErr);
                    }
                }
            }

            InformationManager.Set_Info(errs.ToArray());

            if (!LoadGeneralEq())
            {
                InformationManager.Set_Info(new Errors("Can not load data from data base", "", METHOD));
                //?????logout????
            }

            btnGeneralEqInvApply.ImageKey = "Check";
            btnGeneralEqInvApply.BackColor = frmMain.checkColor;
        }
        //-------------------------------------------------------
        private void dgvGeneralEqInv_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex == -1)
                return;

            if (skipCellValueChanged_dgvGeneralEqInv)
                return;

            if (e.ColumnIndex < 0 || e.RowIndex < 0)
                return;

            //if (e.ColumnIndex == 2 || e.ColumnIndex == 9 || e.ColumnIndex == 15 || e.ColumnIndex == 23)
            //    ComputeGeneralEqOutstanding(e.RowIndex);

            btnGeneralEqInvApply.ImageKey = "warning";
            btnGeneralEqInvApply.BackColor = frmMain.warningColor;
        }
        //-------------------------------------------------------
        private void dgvGeneralEqInv_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (frmMain.selectedRepID == -1)
                return;

            if (access.inventoryManagement_ProductInventory != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;

            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                frmMultiInput fm = new frmMultiInput();
                fm.Height = 200;

                if (e.ColumnIndex == 3)
                {
                    fm.Width = 600;
                    fm.lblTitle.Text = "Request:";

                    fm.dgvInput.Columns.Add("1", "Quantity");
                    fm.dgvInput.Columns.Add("2", "Order #");
                    fm.dgvInput.Columns.Add("3", "Order Time");
                    fm.dgvInput.Columns.Add("4", "Delivery Type");
                    fm.dgvInput.Columns.Add("5", "Material Supplier Code (M.S.C)");
                    fm.dgvInput.Columns.Add("6", "Comment");
                }
                else if (e.ColumnIndex == 10)
                {
                    fm.Width = 600;
                    fm.lblTitle.Text = "Received:";

                    fm.dgvInput.Columns.Add("1", "Quantity");
                    fm.dgvInput.Columns.Add("2", "Prop #");
                    fm.dgvInput.Columns.Add("3", "S/N");
                    fm.dgvInput.Columns.Add("4", "Source");
                    fm.dgvInput.Columns.Add("5", "Comment");
                }
                else if (e.ColumnIndex == 16)
                {
                    fm.Width = 650;
                    fm.lblTitle.Text = "Backload:";

                    fm.dgvInput.Columns.Add("1", "Quantity");
                    fm.dgvInput.Columns.Add("2", "Prop #");
                    fm.dgvInput.Columns.Add("3", "S/N");
                    fm.dgvInput.Columns.Add("4", "Req. #");
                    fm.dgvInput.Columns.Add("5", "Time");
                    fm.dgvInput.Columns.Add("6", "Destination");
                    fm.dgvInput.Columns.Add("7", "Comment");
                }
                else if (e.ColumnIndex == 24)
                {
                    fm.Width = 650;
                    fm.lblTitle.Text = "Movement:";

                    fm.dgvInput.Columns.Add("1", "Quantity");
                    fm.dgvInput.Columns.Add("2", "Prop #");
                    fm.dgvInput.Columns.Add("3", "S/N");
                    fm.dgvInput.Columns.Add("4", "Req. #");
                    fm.dgvInput.Columns.Add("5", "Time");
                    fm.dgvInput.Columns.Add("6", "Destination");
                    fm.dgvInput.Columns.Add("7", "Comment");
                }
                else
                {
                    return;
                }


                int startColID = e.ColumnIndex + 1;

                for (int i = 0; i < fm.dgvInput.Columns.Count; i++)
                {
                    fm.dgvInput.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    fm.dgvInput.Columns[i].SortMode = DataGridViewColumnSortMode.Programmatic;
                }

                fm.dgvInput.AllowUserToAddRows = false;
                fm.dgvInput.AllowUserToDeleteRows = false;
                fm.dgvInput.RowHeadersVisible = false;
                //~~~~~~~~~~~~~~~~~~~~~~
                fm.dgvInput.Rows.Add();
                //~~~~~~~~~~~~~~~~~~~~~~
                for (int i = 0; i < fm.dgvInput.Columns.Count; i++)
                    fm.dgvInput[i, 0].Value = dgvGeneralEqInv[startColID + i, e.RowIndex].Value;
                //~~~~~~~~~~~~~~~~~~~~~~
                DialogResult dlgRes = fm.ShowDialog();

                if (dlgRes != System.Windows.Forms.DialogResult.OK)
                    return;
                //~~~~~~~~~~~~~~~~~~~~~~
                for (int i = 0; i < fm.dgvInput.Columns.Count; i++)
                    dgvGeneralEqInv[startColID + i, e.RowIndex].Value = fm.dgvInput[i, 0].Value;

                dgvGeneralEqInv[startColID - 1, e.RowIndex].Value = dgvGeneralEqInv[startColID, e.RowIndex].Value;

                //ComputeGeneralEqOutstanding(e.RowIndex);
            }
        }
        //-------------------------------------------------------
        private void btnRigEqBulkRefresh_Click(object sender, EventArgs e)
        {
            string METHOD = "btnRigEqBulkRefresh_Click : " + CLASS_NAME;

            if (frmMain.selectedRepID == -1)
                return;

            if (access.inventoryManagement_RigEquipmentInventory == RoleAccess.AcessTypes.AT_None)
                return;

            if (!LoadRigEquipmentsBulk())
            {
                InformationManager.Set_Info(new Errors("Can not load data from data base", "", METHOD));
                //?????logout????
            }

            btnRigEqBulkApply.ImageKey = "check";
            btnRigEqBulkApply.BackColor = frmMain.checkColor;
        }
        //-------------------------------------------------------
        private void btnRigEqBulkApply_Click(object sender, EventArgs e)
        {
            if (frmMain.selectedRepID == -1)
                return;

            if (access.inventoryManagement_RigEquipmentInventory != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;

            try
            {
                List<Errors> errs = new List<Errors>();

                for (int i = 0; i < dgvRigEquipmentBulk.Rows.Count; i++)
                {
                    List<ProcedureParam> prms = new List<ProcedureParam>();
                    string idStr = DGV_Operations.CellValueAsString(dgvRigEquipmentBulk.Rows[i].Cells[0]);

                    if (idStr == "")
                        prms.Add(new ProcedureParam("@ID", ProcedureParam.ParamType.PT_BigInt));//try add
                    else
                        prms.Add(new ProcedureParam("@ID", idStr, ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Record"));//update
                    //~~~~~~~~~~
                    prms.Add(new ProcedureParam("@Used", DGV_Operations.CellValueAsString(dgvRigEquipmentBulk.Rows[i].Cells[5]), ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Used"));

                    //~~~~~~~~~~~~~
                    string prdAutoID = DGV_Operations.CellValueAsString(dgvRigEquipmentBulk.Rows[i].Cells[3]);

                    if (prdAutoID == "")
                        prms.Add(new ProcedureParam("@Prd_AutoID", ProcedureParam.ParamType.PT_Int));
                    else
                        prms.Add(new ProcedureParam("@Prd_AutoID", prdAutoID, ProcedureParam.ParamType.PT_Int, true, "1", false, "", "Invalid Product"));
                    //~~~~~~~~~~
                    //for adding
                    prms.Add(new ProcedureParam("@ReportID", frmMain.selectedRepID.ToString(), ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Report"));
                    prms.Add(new ProcedureParam("@ProjAutoID", frmMain.selectedPrjID.ToString(), ProcedureParam.ParamType.PT_Int, true, "1", false, "", "Invalid Project"));
                    prms.Add(new ProcedureParam("@BulkSys_ID", DGV_Operations.CellValueAsString(dgvRigEquipmentBulk.Rows[i].Cells[6]), ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Bulk System"));

                    string simpErr;
                    Errors critErr;
                    Int64 resultStat;

                    int ret = Procedures.CallProcedure("prc_rt_Rep2InvManRigEqBulk_add_or_update", prms, out simpErr, out critErr, out resultStat);

                    Color normalBkColor = (i % 2 == 0) ? dgvRigEquipmentBulk.BackgroundColor : dgvRigEquipmentBulk.AlternatingRowsDefaultCellStyle.BackColor;

                    dgvRigEquipmentBulk.Rows[i].Cells[5].Style.BackColor = normalBkColor;

                    if (ret != 0)
                    {
                        if (ret == 1)
                        {
                            if (simpErr.StartsWith("Invalid Used"))
                                dgvRigEquipmentBulk.Rows[i].Cells[5].Style.BackColor = Color.Red;
                            critErr = new Errors(simpErr);
                        }

                        errs.Add(critErr);
                    }
                }

                InformationManager.Set_Info(errs.ToArray());

                if (errs.Count == 0)
                {
                    btnRigEqBulkApply.ImageKey = "Check";
                    btnRigEqBulkApply.BackColor = frmMain.checkColor;

                    //if (!LoadRigEquipmentsBulk())
                    //{
                    //    //ErrorManager.Set_Error(new Errors("Can not load data from data base", "", METHOD));
                    //    ////?????logout????
                    //}
                }
            }
            catch (Exception)
            {
            }
        }
        //-------------------------------------------------------
        private void dgvRigEquipmentBulk_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (frmMain.selectedRepID == -1)
                return;

            if (access.inventoryManagement_RigEquipmentInventory != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;

            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 2)
                {
                    //select unused Products
                    string query =
                    " select 0, * "
                    + " from lt_product "
                    + " where AutoID in "
                    + " (select Prd_AutoID from rt_Prj2Prd where PrjAutoID = " + frmMain.selectedPrjID.ToString() + " ) ";

                    ProductListFormDB_Selection selPrd = new ProductListFormDB_Selection(query);
                    selPrd.dgvProducts.MultiSelect = false;
                    selPrd.dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    selPrd.dgvProducts.Columns[0].Visible = false;
                    selPrd.selBtns.ShowSelectionButtons = false;


                    if (selPrd.ShowDialog() != DialogResult.OK)
                        return;

                    if (selPrd.dgvProducts.SelectedRows.Count == 0)
                        return;


                    string Name = DGV_Operations.CellValueAsString(selPrd.dgvProducts.Rows[selPrd.dgvProducts.SelectedRows[0].Index].Cells[3]);
                    string prdAutoID = DGV_Operations.CellValueAsString(selPrd.dgvProducts.Rows[selPrd.dgvProducts.SelectedRows[0].Index].Cells[1]);

                    dgvRigEquipmentBulk.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = Name;
                    dgvRigEquipmentBulk.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value = prdAutoID;

                    btnRigEqBulkApply.ImageKey = "warning";
                    btnRigEqBulkApply.BackColor = frmMain.warningColor;
                }
            }
        }
        //-------------------------------------------------------
        private void dgvRigEquipmentBulk_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex == -1)
                return;

            if (skipCellValueChanged_dgvRigEqBulk)
                return;

            btnRigEqBulkApply.ImageKey = "warning";
            btnRigEqBulkApply.BackColor = frmMain.warningColor;
        }
        //-------------------------------------------------------
        private void btnRigEqPitRefresh_Click(object sender, EventArgs e)
        {
            string METHOD = "btnRigEqPitRefresh_Click : " + CLASS_NAME;

            if (frmMain.selectedRepID == -1)
                return;

            if (access.inventoryManagement_RigEquipmentInventory == RoleAccess.AcessTypes.AT_None)
                return;

            if (!LoadRigEquipmentsPit())
            {
                InformationManager.Set_Info(new Errors("Can not load data from data base", "", METHOD));
                //?????logout????
            }

            btnRigEqPitApply.ImageKey = "check";
            btnRigEqPitApply.BackColor = frmMain.checkColor;
        }
        //-------------------------------------------------------
        private void btnRigEqPitRemove_Click(object sender, EventArgs e)
        {
            if (frmMain.selectedRepID == -1)
                return;

            if (access.inventoryManagement_RigEquipmentInventory != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;

            if (dgvRigEquipmentPit.SelectedRows.Count == 0)
            {
                InformationManager.Set_Info("Select One Row to be Deleted");
                return;
            }

            string idStr = DGV_Operations.CellValueAsString(dgvRigEquipmentPit.Rows[dgvRigEquipmentPit.SelectedRows[0].Index].Cells[0]);

            List<ProcedureParam> prms = new List<ProcedureParam>();
            prms.Add(new ProcedureParam("@ID", idStr, ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Record"));

            string simpErr;
            Errors critErr;
            Int64 resultStat;

            int ret = Procedures.CallProcedure("prc_rt_Rep2InvManRigEqPit_delete", prms, out simpErr, out critErr, out resultStat);

            if (ret != 0)
            {
                if (ret == 1)
                    InformationManager.Set_Info(simpErr);
                else
                    InformationManager.Set_Info(critErr);

                return;
            }

            if (!LoadRigEquipmentsPit())
            {
                //ErrorManager.Set_Error(new Errors("Can not load data from data base", "", METHOD));
                ////?????logout????
            }
        }
        //-------------------------------------------------------
        private void btnRigEqPitApply_Click(object sender, EventArgs e)
        {
            if (frmMain.selectedRepID == -1)
                return;

            if (access.inventoryManagement_RigEquipmentInventory != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;

            try
            {
                List<Errors> errs = new List<Errors>();

                for (int i = 0; i < dgvRigEquipmentPit.Rows.Count; i++)
                {
                    List<ProcedureParam> prms = new List<ProcedureParam>();
                    prms.Add(new ProcedureParam("@ID", DGV_Operations.CellValueAsString(dgvRigEquipmentPit.Rows[i].Cells[0]), ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Record"));
                    prms.Add(new ProcedureParam("@Filled", DGV_Operations.CellValueAsString(dgvRigEquipmentPit.Rows[i].Cells[5]), ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Filled"));

                    //~~~~~~~~~~~~~
                    string wCode = DGV_Operations.CellValueAsString(dgvRigEquipmentPit.Rows[i].Cells[3]);

                    if (wCode == "")
                        prms.Add(new ProcedureParam("@WaterType_PredefAutoID", ProcedureParam.ParamType.PT_Int));
                    else
                        prms.Add(new ProcedureParam("@WaterType_PredefAutoID", wCode, ProcedureParam.ParamType.PT_Int, true, "1", false, "", "Invalid Water Type"));
                    //~~~~~~~~~~~~~

                    string simpErr;
                    Errors critErr;
                    Int64 resultStat;

                    int ret = Procedures.CallProcedure("prc_rt_Rep2InvManRigEqPit_update", prms, out simpErr, out critErr, out resultStat);

                    Color normalBkColor = (i % 2 == 0) ? dgvRigEquipmentPit.BackgroundColor : dgvRigEquipmentPit.AlternatingRowsDefaultCellStyle.BackColor;

                    dgvRigEquipmentPit.Rows[i].Cells[5].Style.BackColor = normalBkColor;

                    if (ret != 0)
                    {
                        if (ret == 1)
                        {
                            if (simpErr.StartsWith("Invalid Filled"))
                                dgvRigEquipmentPit.Rows[i].Cells[5].Style.BackColor = Color.Red;
                            critErr = new Errors(simpErr);
                        }

                        errs.Add(critErr);
                    }
                }

                InformationManager.Set_Info(errs.ToArray());

                if (errs.Count == 0)
                {
                    btnRigEqPitApply.ImageKey = "Check";
                    btnRigEqPitApply.BackColor = frmMain.checkColor;

                    //if (!LoadRigEquipmentsPit())
                    //{
                    //    //ErrorManager.Set_Error(new Errors("Can not load data from data base", "", METHOD));
                    //    ////?????logout????
                    //}
                }
            }
            catch (Exception)
            {
            }
        }
        //-------------------------------------------------------
        private void btnRigEqPitAdd_Click(object sender, EventArgs e)
        {
            if (frmMain.selectedRepID == -1)
                return;

            if (access.inventoryManagement_RigEquipmentInventory != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;

            //~~~~~~~~~~~~~~~~~~~~~~~~~~

            //select unused
            string query =

                  " select p.AutoID, 0, p.Value, pt.ID, pt.PitNum "
                + " from rt_Rig2PitTank pt join  lt_PredefValues p on pt.PitName_PredefAutoID = p.AutoID "
                + " where   "
                + " pt.RigID = " + frmMain.selected_RW_RigID.ToString()
                + " and pt.ID not in  "
                + " ( "
                + " 	select Pit_ID from rt_Rep2InvManRigEqPit where ReportID = " + frmMain.selectedRepID.ToString()
                + " )  "
                + " order by p.UserOrderInGroup  ";

            PredefinedValuesFormDB_Selection pdSel = new PredefinedValuesFormDB_Selection(query, true);
            //Note: Initial columns of pdSel are (predef ID, Item)

            //-----------
            DataGridViewCheckBoxColumn dgvch = new DataGridViewCheckBoxColumn();
            dgvch.HeaderText = "";
            dgvch.Width = 50;

            pdSel.dgvValue.Columns.Insert(1, dgvch);
            //-----------
            pdSel.dgvValue.Columns[2].HeaderText = "Pit/Tank Name";//Item --> Pit/Tank Name
            //-----------
            DataGridViewTextBoxColumn ptIDCol = new DataGridViewTextBoxColumn();
            ptIDCol.ReadOnly = true;
            ptIDCol.Visible = false;

            pdSel.dgvValue.Columns.Insert(3, ptIDCol);
            //-----------
            DataGridViewTextBoxColumn ptNumCol = new DataGridViewTextBoxColumn();
            ptNumCol.HeaderText = "Pit Number";
            ptNumCol.Width = 100;
            ptNumCol.ReadOnly = true;

            pdSel.dgvValue.Columns.Insert(4, ptNumCol);
            //-----------
            //Note: Now, columns of pdSel are (predef ID, checkbox, Pit/Tank Name, Pit ID, Pit Num)
            //-----------
            if (pdSel.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;

            List<Errors> errs = new List<Errors>();

            for (int i = 0; i < pdSel.dgvValue.Rows.Count; i++)
            {
                if (Convert.ToBoolean(pdSel.dgvValue.Rows[i].Cells[1].Value) == true)
                {
                    string ptIDStr = DGV_Operations.CellValueAsString(pdSel.dgvValue.Rows[i].Cells[3]);

                    List<ProcedureParam> prms = new List<ProcedureParam>();

                    prms.Add(new ProcedureParam("@PitID", ptIDStr, ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Pit"));
                    prms.Add(new ProcedureParam("@ReportID", frmMain.selectedRepID.ToString(), ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Report"));
                    prms.Add(new ProcedureParam("@ProjAutoID", frmMain.selectedPrjID.ToString(), ProcedureParam.ParamType.PT_Int, true, "1", false, "", "Invalid Project"));

                    string simpErr;
                    Errors critErr;
                    Int64 resultStat;

                    int ret = Procedures.CallProcedure("prc_rt_Rep2InvManRigEqPit_insert", prms, out simpErr, out critErr, out resultStat);

                    if (ret != 0)
                    {
                        if (ret == 1) critErr = new Errors(simpErr);

                        errs.Add(critErr);
                    }
                }
            }

            InformationManager.Set_Info(errs.ToArray());

            if (!LoadRigEquipmentsPit())
            {
                //ErrorManager.Set_Error(new Errors("Can not load data from data base", "", METHOD));
                //?????logout????
            }
        }
        //-------------------------------------------------------
        private void dgvRigEquipmentPit_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (frmMain.selectedRepID == -1)
                return;

            if (access.inventoryManagement_RigEquipmentInventory != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;

            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 2)
                {
                    string groupName = "Water Type";
                    //~~~~~~~~~~~~~~~~~~~~~~~~~~
                    PredefinedValuesFormDB_Selection pdSel = new PredefinedValuesFormDB_Selection(groupName);

                    if (pdSel.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                        return;

                    if (pdSel.dgvValue.SelectedRows.Count == 0)
                        return;

                    string val = DGV_Operations.CellValueAsString(pdSel.dgvValue.Rows[pdSel.dgvValue.SelectedRows[0].Index].Cells[1]);
                    string id = DGV_Operations.CellValueAsString(pdSel.dgvValue.Rows[pdSel.dgvValue.SelectedRows[0].Index].Cells[0]);

                    dgvRigEquipmentPit.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = val;
                    dgvRigEquipmentPit.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value = id;

                    btnRigEqPitApply.ImageKey = "warning";
                    btnRigEqPitApply.BackColor = frmMain.warningColor;
                }
            }
        }
        //-------------------------------------------------------
        private void dgvRigEquipmentPit_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex == -1)
                return;

            if (skipCellValueChanged_dgvRigEqPit)
                return;

            btnRigEqPitApply.ImageKey = "warning";
            btnRigEqPitApply.BackColor = frmMain.warningColor;
        }
        //-------------------------------------------------------



    }
}
