using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace DMR
{
    public partial class SolidAnaForm : Form, IMyForm
    {
        const string CLASS_NAME = "SolidAnaForm";
        RoleAccess access = null;
        frmMain mainForm = null;

        bool skipComputations = false;

        Int64 selectedMudPropColID = -1;
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
            CleanVolatileParts();
            //~~~~~~~~~~~~~~~~~~~~
            if (frmMain.selectedRepID != -1)
            {
                LoadMudPropColumns();
                selectedMudPropColID = -1;
                OnSelectedMudPropColumnChanged();
                ChangeVisibility();
            }

            ResetApplyBtns();
        }
        //-------------------------------------------------------
        void IMyForm.OnInit()
        {
            //~~~~~~~~~~~~~~~~~ AT_None = Hide ~~~~~~~~~~~~~~~~~~~~
            if (access.solidAnalysis == RoleAccess.AcessTypes.AT_None)
            {
                pnlTotal.Visible = false;
            }
            else if (access.solidAnalysis == RoleAccess.AcessTypes.AT_ReadOnly)
            {
                grbAll.Enabled = false;
                btnApply.Enabled = false;
            }
            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            CleanVolatileParts();
            ResetApplyBtns();
        }
        //-------------------------------------------------------
        void IMyForm.OnCurrentUnitsChanged()
        {
            //~~~~~~~~~~~~~~
            ProjectForm prjForm = (ProjectForm)mainForm.subForms[mainForm.PorjectFormID];
            string selUnitIonic = prjForm.lblUnitSelectedIonicMassConcentration.Tag.ToString();
            string selUnitKClCon = prjForm.lblUnitSelectedKClConcentration.Tag.ToString();
            string selUnitNaClCon = prjForm.lblUnitSelectedNaClConcentration.Tag.ToString();
            string selUnitMBT = prjForm.lblUnitSelectedMudMBT.Tag.ToString();
            string selUnitPowderMatCon = prjForm.lblUnitSelectedPowderMaterialConcentration.Tag.ToString();
            //~~~~~~~~~~~~~~

            UnitString.WriteUnit(lblHemConc, selUnitPowderMatCon);
            UnitString.WriteUnit(lblBarConc, selUnitPowderMatCon);
            UnitString.WriteUnit(lblCalCarConc, selUnitPowderMatCon);

            UnitString.WriteUnit(label17, selUnitMBT);
            UnitString.WriteUnit(label16, selUnitMBT);

            UnitString.WriteUnit(label4, selUnitIonic);
            UnitString.WriteUnit(label10, selUnitIonic);
            UnitString.WriteUnit(label100, selUnitIonic);

            UnitString.WriteUnit(label8, selUnitKClCon);

            UnitString.WriteUnit(label12, selUnitNaClCon);

            UpdateComputationsByPressRefereshButtons();
        }
        //-------------------------------------------------------
        void UpdateComputationsByPressRefereshButtons()
        {
            //btnRefresh_Click(null, null);
        }
        //-------------------------------------------------------
        void CleanVolatileParts()
        {
            trvDFSofMudProp.Nodes.Clear();

            selectedMudPropColID = -1;
            OnSelectedMudPropColumnChanged();

            txtMW.Text = "";
            txtSandContent.Text = "";
            txtWater.Text = "";
            txtOilLubricant.Text = "";
            txtGlycol.Text = "";
            txtTotalCh.Text = "";
            txtKCL.Text = "";
            txtKCLCh.Text = "";
            txtNaCl.Text = "";
            txtNaClCh.Text = "";
            txtDSCEC.Text = "";
            txtActMudMBT.Text = "";
            txtOutNaClVol.Text = "";
            txtOutKClVol.Text = "";
            txtOutDisSolVol.Text = "";
            txtOutDisSol_wt.Text = "";
            txtOutWeiMatVol.Text = "";
            txtOutWeiMat_BBL.Text = "";
            txtOutLGSVol.Text = "";
            txtOutLGS_BBL.Text = "";
            txtOutCorSolVol.Text = "";
            txtOutCorSol_BBL.Text = "";
            txtOutBentVol.Text = "";
            txtOutBent_BBL.Text = "";
            txtOutDrillSolVol.Text = "";
            txtOutDrillSol_BBL.Text = "";
            txtTotalSolid.Text = "";
            txtOutNaClwt.Text = "";
            txtOutKClwt.Text = "";
            txtOutBariteVol.Text = "";
            txtOutBarite_BBL.Text = "";
            txtOutHematitVol.Text = "";
            txtOutHematit_BBL.Text = "";
            txtOutCalCarVol.Text = "";
            txtOutCalCar_BBL.Text = "";
        }
        //-------------------------------------------------------
        void ResetApplyBtns()
        {
            btnApply.ImageKey = "check";
            btnApply.BackColor = frmMain.checkColor;
        }
        //-------------------------------------------------------
        public SolidAnaForm(RoleAccess access, frmMain mainForm)
        {
            this.access = access;
            this.mainForm = mainForm;

            InitializeComponent();
        }
        //-------------------------------------------------------
        private void SolidAnaForm_Load(object sender, EventArgs e)
        {
#if !NOLOCK
            //Security Integer (Fetch Integer memory value from HLock)
            object obj1 = new object();
            uint RProg = (uint)Program.Rand.Next(2147483647);
            obj1 = Program.arm.GetARMData(ARM.IWhichData.INT_VAL1, RProg);
            string strInt1 = Program.cls.DecodData(obj1, ARM.IWhichData.INT_VAL1, RProg);

            if (strInt1.Trim() != "5567")
            {
                MessageBox.Show("Connecting to Security Key Lock failed. Please plug security lock.", "PDF DMR System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.LockID = string.Empty;
                this.Close();
            }
#endif
        }
        //-------------------------------------------------------        
        void LoadMudPropColumns()
        {
            trvDFSofMudProp.Nodes.Clear();
            lblSelectedFluidSystem.Text = "";

            string METHOD = "LoadMudPropColumns : " + CLASS_NAME;

            try
            {
                string query =
                        " select rmc.ID, d.DrillingFluidSystem, samplePit_ID, rmc.CheckTime, rmc.UserOrder "
                    + " from rt_Rep2MudProp_MudPropCol rmc join rt_Rep2MudPropPeriod rm on rmc.MudPropPeriod_ID = rm.ID "
                    + " left join lt_DrillingFluidSystem d on rmc.Dfs_AutoID = d.AutoID "
                    + " where rm.ReportID = " + frmMain.selectedRepID.ToString()
                    + " order by rmc.CheckTime, rmc.UserOrder ";




                DataSet ds = new DataSet();

                if (ConnectionManager.ExecQuery(query, ref ds))
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        Int64 colID = Convert.ToInt64(dr.ItemArray[0]);

                        string dfs = dr.ItemArray[1].ToString();
                        string chktime = dr.ItemArray[3].ToString().Trim();

                        string pitNumAndName = "Flow Line";
                        if (dr.ItemArray[2] != DBNull.Value)
                        {
                            Int64 sampPitID = Convert.ToInt64(dr.ItemArray[2]);

                            int sampPitNum = 0;
                            string pitName;
                            FetchTableData.GetPitNameAndNumber(sampPitID, out pitName, out sampPitNum);

                            pitNumAndName = pitName + " #" + sampPitNum.ToString();
                        }

                        TreeNode dfsNode = new TreeNode(dfs + "(" + pitNumAndName + ")" + "[" + chktime + "]");
                        dfsNode.Name = colID.ToString();
                        trvDFSofMudProp.Nodes.Add(dfsNode);
                    }

                    ds.Dispose();
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                InformationManager.Set_Info(new Errors("Can not load data from data base", ex.Message, METHOD));
                return;
            }
        }
        //-------------------------------------------------------
        private void btnRefreshSys_Click(object sender, EventArgs e)
        {
            //string METHOD = "btnRefreshSys_Click : " + CLASS_NAME;

            if (frmMain.selectedRepID == -1)
                return;

            if (access.solidAnalysis == RoleAccess.AcessTypes.AT_None)
                return;

            LoadMudPropColumns();

            bool lastSelDFSExists = false;

            for (int i = 0; i < trvDFSofMudProp.Nodes.Count; i++)
                if (Convert.ToInt64(trvDFSofMudProp.Nodes[i].Name) == selectedMudPropColID)
                {
                    lastSelDFSExists = true;
                    trvDFSofMudProp.SelectedNode = trvDFSofMudProp.Nodes[i];
                    break;
                }

            if (!lastSelDFSExists)
            {
                selectedMudPropColID = -1;
                OnSelectedMudPropColumnChanged();                
            }
            else
            {
                lblSelectedFluidSystem.Text = trvDFSofMudProp.SelectedNode.Text;
            }
        }
        //-------------------------------------------------------
        private void trvDFSofMudProp_MouseDown(object sender, MouseEventArgs e)
        {
            trvDFSofMudProp.ContextMenuStrip = null;
        }
        //-------------------------------------------------------
        private void trvDFSofMudProp_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (access.solidAnalysis == RoleAccess.AcessTypes.AT_None)
                return;

            if (e.Node == null)
                return;

            if (access.solidAnalysis == RoleAccess.AcessTypes.AT_ReadOnly)
            {
                //nothing
            }

            trvDFSofMudProp.SelectedNode = e.Node;

            if (e.Button == MouseButtons.Right)
            {
                trvDFSofMudProp.ContextMenuStrip = ctxDFS;
            }
        }
        //-------------------------------------------------------
        void OnSelectedMudPropColumnChanged()
        {
            if (selectedMudPropColID == -1)
            {
                rbtnWeighted.Checked = true;

                chbWAHem.Checked = false;
                chbWABar.Checked = false;
                chbWACaCarb.Checked = false;

                txtHemConc.Text = "";
                txtBarConc.Text = "";
                txtCalCarConc.Text = "";

                txtHemDens.Text = "";
                txtBarDens.Text = "";
                txtCalCarDens.Text = "";

                txtFreshMudMBT.Text = "";
                txtBentCEC.Text = "";
            }
            else
            {
                string query =
                " select Weighted, WA_Hematit, WA_Barite, WA_CalciumCarbonate, HematitDensity, BariteDensity, CalCarbDensity, HematitConcentration,   "
                + " 		BariteConcentration, CalCarbConcentration, FreshMudMBT, BentoniteMBT  "
                + " from rt_Rep2SolidAnaly  "
                + " where MudPropColumn_ID =  " + selectedMudPropColID.ToString();

                DataSet ds = new DataSet();
                skipComputations = true;

                if (ConnectionManager.ExecQuery(query, ref ds, 1))
                {
                    if (Convert.ToBoolean(ds.Tables[0].Rows[0][0]))
                        rbtnWeighted.Checked = true;
                    else
                        rbtnUnweighted.Checked = true;

                    chbWAHem.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0][1]);
                    chbWABar.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0][2]);
                    chbWACaCarb.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0][3]);

                    txtHemDens.Text = ds.Tables[0].Rows[0][4].ToString();
                    txtBarDens.Text = ds.Tables[0].Rows[0][5].ToString();
                    txtCalCarDens.Text = ds.Tables[0].Rows[0][6].ToString();

                    txtHemConc.Text = ds.Tables[0].Rows[0][7].ToString();
                    txtBarConc.Text = ds.Tables[0].Rows[0][8].ToString();
                    txtCalCarConc.Text = ds.Tables[0].Rows[0][9].ToString();

                    txtFreshMudMBT.Text = ds.Tables[0].Rows[0][10].ToString();
                    txtBentCEC.Text = ds.Tables[0].Rows[0][11].ToString();

                    ds.Dispose();
                }
            }

            skipComputations = false;

            btnApply.ImageKey = "check";
            btnApply.BackColor = frmMain.checkColor;
        }
        //-------------------------------------------------------
        private void ctxDFSSetAsCurrent_Click(object sender, EventArgs e)
        {
            if (access.solidAnalysis == RoleAccess.AcessTypes.AT_None)
                return;

            try
            {
                if (trvDFSofMudProp.SelectedNode == null)
                    return;

                selectedMudPropColID = Convert.ToInt64(trvDFSofMudProp.SelectedNode.Name);

                OnSelectedMudPropColumnChanged();
                ChangeVisibility();
                ComputeValues();

                lblSelectedFluidSystem.Text = trvDFSofMudProp.SelectedNode.Text;
            }
            catch (Exception ex)
            {
                InformationManager.Set_Info(ex.Message);
            }
        }
        //-------------------------------------------------------
        void ComputeValues()
        {
            try
            {
                if (selectedMudPropColID == -1)
                    return;
                //~~~~~~~~~~~~~~
                Helpers.Computation.SolidAnalysis solAComp = new Helpers.Computation.SolidAnalysis();
                //~~~~~~~~~~~~~~
                ProjectForm prjForm = (ProjectForm)mainForm.subForms[mainForm.PorjectFormID];
                solAComp.in_selUnitVol = prjForm.lblUnitSelectedLiquidVolume.Tag.ToString();
                solAComp.in_selUnitPlasticVis = prjForm.lblUnitSelectedPlasticViscosity.Tag.ToString();
                solAComp.in_selUnitYieldPoint = prjForm.lblUnitSelectedYieldPointAndGelStrength.Tag.ToString();
                solAComp.in_selUnitIonic = prjForm.lblUnitSelectedIonicMassConcentration.Tag.ToString();
                solAComp.in_selUnitKClCon = prjForm.lblUnitSelectedKClConcentration.Tag.ToString();
                solAComp.in_selUnitNaClCon = prjForm.lblUnitSelectedNaClConcentration.Tag.ToString();
                solAComp.in_selUnitPmPfMf = prjForm.lblUnitSelectedPmPfMf.Tag.ToString();
                solAComp.in_selUnitPowderMatConc = prjForm.lblUnitSelectedPowderMaterialConcentration.Tag.ToString();
                solAComp.in_selUnitMW = prjForm.lblUnitSelectedMudWeight.Tag.ToString();
                solAComp.in_selUnitMBT = prjForm.lblUnitSelectedMudMBT.Tag.ToString();
                solAComp.in_selUnitPowderMatCon = prjForm.lblUnitSelectedPowderMaterialConcentration.Tag.ToString();
                //~~~~~~~~~~~~~~
                solAComp.in_FreshMudMBT = txtFreshMudMBT.Text;
                solAComp.in_BentCEC = txtBentCEC.Text;
                solAComp.in_BarDens = txtBarDens.Text;
                solAComp.in_HemDens = txtHemDens.Text;
                solAComp.in_CalCarDens = txtCalCarDens.Text;
                solAComp.in_BarConc = txtBarConc.Text;
                solAComp.in_HemConc = txtHemConc.Text;
                solAComp.in_CalCarConc = txtCalCarConc.Text;
                //~~~~~~~~~~~~~~
                txtActMudMBT.Text = ""; txtDSCEC.Text = ""; txtWater.Text = ""; txtOilLubricant.Text = "";
                txtGlycol.Text = ""; txtTotalSolid.Text = ""; txtMW.Text = ""; txtSandContent.Text = "";
                txtTotalCh.Text = ""; txtKCL.Text = ""; txtKCLCh.Text = ""; txtNaCl.Text = "";
                txtNaClCh.Text = ""; txtOutDisSolVol.Text = ""; txtOutDisSol_wt.Text = "";
                txtOutNaClVol.Text = ""; txtOutNaClwt.Text = ""; txtOutKClVol.Text = ""; txtOutKClwt.Text = "";
                txtOutCorSolVol.Text = ""; txtOutCorSol_BBL.Text = ""; txtOutWeiMatVol.Text = ""; txtOutWeiMat_BBL.Text = "";
                txtOutBariteVol.Text = ""; txtOutBarite_BBL.Text = ""; txtOutHematitVol.Text = ""; txtOutHematit_BBL.Text = "";
                txtOutCalCarVol.Text = ""; txtOutCalCar_BBL.Text = ""; txtOutLGSVol.Text = ""; txtOutLGS_BBL.Text = "";
                txtOutBentVol.Text = ""; txtOutBent_BBL.Text = ""; txtOutDrillSolVol.Text = ""; txtOutDrillSol_BBL.Text = "";
                //~~~~~~~~~~~~~~
                //do not return on error => show any computed value yet
                solAComp.Compute(selectedMudPropColID, chbWABar.Checked, chbWAHem.Checked, chbWACaCarb.Checked, rbtnWeighted.Checked);
                //~~~~~~~~~~~~~~
                try { SetComputedValueAndCheck(txtMW, Convert.ToDecimal(solAComp.out_MW)); } catch (Exception) { }
                try { SetComputedValueAndCheck(txtSandContent, Convert.ToDecimal(solAComp.out_SandContent)); } catch (Exception) { }
                try { SetComputedValueAndCheck(txtWater, Convert.ToDecimal(solAComp.out_Water)); } catch (Exception) { }
                try { SetComputedValueAndCheck(txtOilLubricant, Convert.ToDecimal(solAComp.out_OilLubricant)); } catch (Exception) { }
                try { SetComputedValueAndCheck(txtGlycol, Convert.ToDecimal(solAComp.out_Glycol)); } catch (Exception) { }
                try { SetComputedValueAndCheck(txtTotalCh, Convert.ToDecimal(solAComp.out_TotalCh)); } catch (Exception) { }
                try { SetComputedValueAndCheck(txtKCL, Convert.ToDecimal(solAComp.out_KCL)); } catch (Exception) { }
                try { SetComputedValueAndCheck(txtKCLCh, Convert.ToDecimal(solAComp.out_KCLCh)); } catch (Exception) { }
                try { SetComputedValueAndCheck(txtNaCl, Convert.ToDecimal(solAComp.out_NaCl)); } catch (Exception) { }
                try { SetComputedValueAndCheck(txtNaClCh, Convert.ToDecimal(solAComp.out_NaClCh)); } catch (Exception) { }
                try { SetComputedValueAndCheck(txtDSCEC, Convert.ToDecimal(solAComp.out_DSCEC)); } catch (Exception) { }
                try { SetComputedValueAndCheck(txtActMudMBT, Convert.ToDecimal(solAComp.out_ActMudMBT)); } catch (Exception) { }
                try { SetComputedValueAndCheck(txtOutNaClVol, Convert.ToDecimal(solAComp.out_NaClVol)); } catch (Exception) { }
                try { SetComputedValueAndCheck(txtOutKClVol, Convert.ToDecimal(solAComp.out_KClVol)); } catch (Exception) { }
                try { SetComputedValueAndCheck(txtOutDisSolVol, Convert.ToDecimal(solAComp.out_DisSolVol)); } catch (Exception) { }
                try { SetComputedValueAndCheck(txtOutDisSol_wt, Convert.ToDecimal(solAComp.out_DisSol_wt)); } catch (Exception) { }
                try { SetComputedValueAndCheck(txtOutWeiMatVol, Convert.ToDecimal(solAComp.out_WeiMatVol)); } catch (Exception) { }
                try { SetComputedValueAndCheck(txtOutWeiMat_BBL, Convert.ToDecimal(solAComp.out_WeiMat_BBL)); } catch (Exception) { }
                try { SetComputedValueAndCheck(txtOutLGSVol, Convert.ToDecimal(solAComp.out_LGSVol)); } catch (Exception) { }
                try { SetComputedValueAndCheck(txtOutLGS_BBL, Convert.ToDecimal(solAComp.out_LGS_BBL)); } catch (Exception) { }
                try { SetComputedValueAndCheck(txtOutCorSolVol, Convert.ToDecimal(solAComp.out_CorSolVol)); } catch (Exception) { }
                try { SetComputedValueAndCheck(txtOutCorSol_BBL, Convert.ToDecimal(solAComp.out_CorSol_BBL)); } catch (Exception) { }
                try { SetComputedValueAndCheck(txtOutBentVol, Convert.ToDecimal(solAComp.out_BentVol)); } catch (Exception) { }
                try { SetComputedValueAndCheck(txtOutBent_BBL, Convert.ToDecimal(solAComp.out_Bent_BBL)); } catch (Exception) { }
                try { SetComputedValueAndCheck(txtOutDrillSolVol, Convert.ToDecimal(solAComp.out_DrillSolVol)); } catch (Exception) { }
                try { SetComputedValueAndCheck(txtOutDrillSol_BBL, Convert.ToDecimal(solAComp.out_DrillSol_BBL)); } catch (Exception) { }
                try { SetComputedValueAndCheck(txtTotalSolid, Convert.ToDecimal(solAComp.out_TotalSolid)); } catch (Exception) { }
                try { SetComputedValueAndCheck(txtOutNaClwt, Convert.ToDecimal(solAComp.out_NaClwt)); } catch (Exception) { }
                try { SetComputedValueAndCheck(txtOutKClwt, Convert.ToDecimal(solAComp.out_KCL_wt)); } catch (Exception) { }
                try { SetComputedValueAndCheck(txtOutBariteVol, Convert.ToDecimal(solAComp.out_BariteVol)); } catch (Exception) { }
                try { SetComputedValueAndCheck(txtOutBarite_BBL, Convert.ToDecimal(solAComp.out_Barite_BBL)); } catch (Exception) { }
                try { SetComputedValueAndCheck(txtOutHematitVol, Convert.ToDecimal(solAComp.out_HematitVol)); } catch (Exception) { }
                try { SetComputedValueAndCheck(txtOutHematit_BBL, Convert.ToDecimal(solAComp.out_Hematit_BBL)); } catch (Exception) { }
                try { SetComputedValueAndCheck(txtOutCalCarVol, Convert.ToDecimal(solAComp.out_CalCarVol)); } catch (Exception) { }
                try { SetComputedValueAndCheck(txtOutCalCar_BBL, Convert.ToDecimal(solAComp.out_CalCar_BBL)); } catch (Exception) { }
                //~~~~~~~~~~~~~~
            }
            catch (Exception)
            {
            }
        }
        //-------------------------------------------------------
        void SetComputedValueAndCheck(TextBox t, decimal v)
        {
            if (v < 0) t.BackColor = Color.Red; else t.BackColor = SystemColors.Control;
            t.Text = v.ToString("0.###");
        }
        //-------------------------------------------------------
        private void txt_TextChanged(object sender, EventArgs e)
        {
            if (skipComputations)
                return;

            if (frmMain.selectedRepID == -1 || selectedMudPropColID == -1)
                return;

            if (access.solidAnalysis != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;

            //ComputeValues();

            btnApply.ImageKey = "warning";
            btnApply.BackColor = frmMain.warningColor;
        }
        //-------------------------------------------------------
        private void chbWA_CheckedChanged(object sender, EventArgs e)
        {
            if (skipComputations)
                return;

            if (frmMain.selectedRepID == -1 || selectedMudPropColID == -1)
                return;

            if (access.solidAnalysis != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;

            ChangeVisibility();
            //ComputeValues();

            btnApply.ImageKey = "warning";
            btnApply.BackColor = frmMain.warningColor;
        }
        //-------------------------------------------------------
        private void rbtnWeighted_CheckedChanged(object sender, EventArgs e)
        {
            if (skipComputations)
                return;

            if (frmMain.selectedRepID == -1 || selectedMudPropColID == -1)
                return;

            if (access.solidAnalysis != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;

            if (!rbtnWeighted.Checked)
                return;

            ChangeVisibility();
            //ComputeValues();

            btnApply.ImageKey = "warning";
            btnApply.BackColor = frmMain.warningColor;
        }
        //-------------------------------------------------------
        private void rbtnUnweighted_CheckedChanged(object sender, EventArgs e)
        {
            if (skipComputations)
                return;

            if (frmMain.selectedRepID == -1 || selectedMudPropColID == -1)
                return;

            if (access.solidAnalysis != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;

            if (!rbtnUnweighted.Checked)
                return;

            ChangeVisibility();
            //ComputeValues();

            btnApply.ImageKey = "warning";
            btnApply.BackColor = frmMain.warningColor;
        }
        //-------------------------------------------------------
        void ChangeVisibility()
        {
            grbWAConcentration.Visible = true;

            lblBarConc.Visible = true;
            txtBarConc.Visible = true;
            lblHemConc.Visible = true;
            txtHemConc.Visible = true;
            lblCalCarConc.Visible = true;
            txtCalCarConc.Visible = true;

            lblBarDens.Visible = true;
            txtBarDens.Visible = true;
            lblHemDens.Visible = true;
            txtHemDens.Visible = true;
            lblCalCarDens.Visible = true;
            txtCalCarDens.Visible = true;


            if (rbtnUnweighted.Checked)//Unweighted
            {
                pnlWeighted.Visible = false;
                return;
            }
            else//Weighted
            {
                pnlWeighted.Visible = true;

                bool b = chbWABar.Checked;
                bool h = chbWAHem.Checked;
                bool c = chbWACaCarb.Checked;

                if (!b && !h && !c)
                {
                    /*nothing*/
                }
                else if (!b && !h && c)
                {
                    lblBarDens.Visible = false;
                    txtBarDens.Visible = false;
                    lblHemDens.Visible = false;
                    txtHemDens.Visible = false;
                    lblBarConc.Visible = false;
                    txtBarConc.Visible = false;
                    lblHemConc.Visible = false;
                    txtHemConc.Visible = false;
                }
                else if (!b && h && !c)
                {
                    grbWAConcentration.Visible = false;
                    lblBarDens.Visible = false;
                    txtBarDens.Visible = false;
                    lblCalCarDens.Visible = false;
                    txtCalCarDens.Visible = false;
                }
                else if (!b && h && c)
                {
                    lblBarConc.Visible = false;
                    txtBarConc.Visible = false;
                    lblBarDens.Visible = false;
                    txtBarDens.Visible = false;
                }
                else if (b && !h && !c)
                {
                    grbWAConcentration.Visible = false;
                    lblHemDens.Visible = false;
                    txtHemDens.Visible = false;
                    lblCalCarDens.Visible = false;
                    txtCalCarDens.Visible = false;
                }
                else if (b && !h && c)
                {
                    lblHemConc.Visible = false;
                    txtHemConc.Visible = false;
                    lblHemDens.Visible = false;
                    txtHemDens.Visible = false;
                }
                else if (b && h && !c)
                {
                    lblCalCarConc.Visible = false;
                    txtCalCarConc.Visible = false;
                    lblCalCarDens.Visible = false;
                    txtCalCarDens.Visible = false;
                }
                else if (b && h && c)
                {
                    /*nothing*/
                }
            }
        }
        //-------------------------------------------------------
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (frmMain.selectedRepID == -1)
                return;

            OnSelectedMudPropColumnChanged();
            ChangeVisibility();
            ComputeValues();
            btnApply.ImageKey = "check";
            btnApply.BackColor = frmMain.checkColor;
        }
        //-------------------------------------------------------
        private void btnApply_Click(object sender, EventArgs e)
        {
            if (frmMain.selectedRepID == -1 || selectedMudPropColID == -1)
                return;

            if (access.solidAnalysis != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;

            List<Errors> errs = new List<Errors>();

            List<ProcedureParam> prms = new List<ProcedureParam>();

            prms.Add(new ProcedureParam("@MudPropColumn_ID", selectedMudPropColID.ToString(), ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Mud Prop. Column"));
            prms.Add(new ProcedureParam("@Weighted", rbtnWeighted.Checked ? "1" : "0", ProcedureParam.ParamType.PT_Bit, false, "0", false, "", ""));
            prms.Add(new ProcedureParam("@WA_Hematit", chbWAHem.Checked ? "1" : "0", ProcedureParam.ParamType.PT_Bit, false, "0", false, "", ""));
            prms.Add(new ProcedureParam("@WA_Barite", chbWABar.Checked ? "1" : "0", ProcedureParam.ParamType.PT_Bit, false, "0", false, "", ""));
            prms.Add(new ProcedureParam("@WA_CalciumCarbonate", chbWACaCarb.Checked ? "1" : "0", ProcedureParam.ParamType.PT_Bit, false, "0", false, "", ""));
            prms.Add(new ProcedureParam("@HematitDensity", txtHemDens.Text, ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Hematit Density"));
            prms.Add(new ProcedureParam("@BariteDensity", txtBarDens.Text, ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Barite Density"));
            prms.Add(new ProcedureParam("@CalCarbDensity", txtCalCarDens.Text, ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Calcium Carbonate Density"));
            prms.Add(new ProcedureParam("@HematitConcentration", txtHemConc.Text, ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Hematit Concentration"));
            prms.Add(new ProcedureParam("@BariteConcentration", txtBarConc.Text, ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Barite Concentration"));
            prms.Add(new ProcedureParam("@CalCarbConcentration", txtCalCarConc.Text, ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Calcium Carbonate Concentration"));
            prms.Add(new ProcedureParam("@FreshMudMBT", txtFreshMudMBT.Text, ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Fresh Mud MBT"));
            prms.Add(new ProcedureParam("@BentoniteMBT", txtBentCEC.Text, ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Bentonite MBT"));

            string simpErr;
            Errors critErr;
            Int64 resultStat;

            int ret = Procedures.CallProcedure("prc_rt_Rep2SolidAnaly_Update", prms, out simpErr, out critErr, out resultStat);

            if (ret != 0)
            {
                if (ret == 1)
                    InformationManager.Set_Info(simpErr);
                else
                    InformationManager.Set_Info(critErr);

                return;
            }

            btnApply.ImageKey = "Check";
            btnApply.BackColor = frmMain.checkColor;
            ComputeValues();
        }
        //-------------------------------------------------------
        private void btnOutExport_Click(object sender, EventArgs e)
        {
            if (frmMain.selectedRepID == -1 || selectedMudPropColID == -1)
                return;

            if (access.solidAnalysis == RoleAccess.AcessTypes.AT_None)
                return;

            string dfs = "", smp = "", ct = "", cc = "", gt = "";
            if (!FetchTableData.Get_AllValues_OfMudPropColumn(selectedMudPropColID, out dfs, out smp, out ct, out cc, out gt))
                return;

            string fileName = EPP_ExcelManager.SaveFileDialog();

            if (fileName.Trim() == "")
                return;

            SaveSolidAnalysisHierarchy(fileName, dfs + " (vol%)");
        }
        //-------------------------------------------------------
        void SaveSolidAnalysisHierarchy(string fileName, string dfsName)
        {
            FileInfo newFile = new FileInfo(fileName);
            if (newFile.Exists)
            {
                newFile.Delete();  // ensures we create a new workbook
                newFile = new FileInfo(fileName);
            }

            using (ExcelPackage package = new ExcelPackage(newFile))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Solid Analysis Output");
                //~~~~~~~~~~~~~~~~~~~~~~~~
                Point[] conLinePos = new Point[] //P1:Left-Top  ,  P2:Left-Top  
                {
                    new Point(85 ,116), new Point(640 ,116),//H. below DFS 
                    new Point(510 ,80), new Point(510 ,116),//V. below DFS
                    new Point(85 ,116),new Point(85 ,140), //V. above Water 
                    new Point(220 ,116),new Point(220 ,140),//V. above Oil
                    new Point(348 ,116),new Point(348 ,140), //V. above Glycol
                    new Point(640 ,116),new Point(640 ,140), //V. above Solids

                    new Point(480 ,180), new Point(800 ,180),//H. below Solids
                    new Point(640 ,160), new Point(640 ,180),//V. below Solids
                    new Point(480 ,180), new Point(480 ,210),//V. above diss. Sol.
                    new Point(800 ,180),new Point(800 ,210),//V. above corre. sol.

                    new Point(430 ,238), new Point(527 ,238),//H. below  diss. Sol.
                    new Point(480 ,225), new Point(480 ,238),//V. below  diss. Sol.
                    new Point(430 ,238), new Point(430 ,260),//V. above nacl
                    new Point(527 ,238),new Point(527 ,260),//V. above kcl

                    new Point(700 ,238),new Point(1070 ,238),//H. below corre. sol.
                    new Point(810 ,220),new Point(810 ,238),//V. below corre. sol.
                    new Point(700 ,238),new Point(700 ,260),//V. above wei. mat.
                    new Point(1070 ,238),new Point(1070 ,260),//V. above lgs

                    new Point(645 ,300),new Point(865 ,300),//H. below wei. mat.
                    new Point(700 ,280),new Point(700 ,300),//V. below wei. mat.
                    new Point(645 ,300),new Point(645 ,325),//V. above barite
                    new Point(745 ,300),new Point(745 ,325),//V. above hematit
                    new Point(865 ,300),new Point(865 ,325),//V. above cal. car.

                    new Point(1015 ,300),new Point(1115 ,300),//H. below lgs
                    new Point(1070 ,290),new Point(1070 ,300),//V. below lgs
                    new Point(1015 ,300),new Point(1015 ,320),//V. above bent.
                    new Point(1115 ,300),new Point(1115 ,320)//V. above dril. sol.
                };
                //~~~~~~~~~~~~~~~~~~~~~~~~
                for (int i = 0; i < conLinePos.Length / 2; i++)
                {
                    int t = conLinePos[2 * i].Y;
                    int l = conLinePos[2 * i].X;

                    int w = conLinePos[2 * i + 1].X - conLinePos[2 * i].X;
                    int h = conLinePos[2 * i + 1].Y - conLinePos[2 * i].Y;

                    var shp = worksheet.Drawings.AddShape("Line" + i.ToString(), eShapeStyle.Line);
                    shp.SetPosition(t, l);//Top-Left
                    shp.SetSize(w, h);//Width-Height
                }
                //~~~~~~~~~~~~~~~~~~~~~~~~
                string[] subShapeText = new string[15]
                {
                    "Water\n" + txtWater.Text,
                    "Oil/Lubricant\n" + txtOilLubricant.Text,
                    "Glycol\n" + txtGlycol.Text,
                    "Solids\n" + txtTotalSolid.Text,
                    "Dissolved Solids\n" + txtOutDisSolVol.Text,
                    "Corrected Solids\n"+ txtOutCorSolVol.Text,
                    "NaCl\n" + txtOutNaClVol.Text,
                    "KCl\n" + txtOutKClVol.Text,
                    "Weighting Material\n" + txtOutWeiMatVol.Text,
                    "LGS\n" + txtOutLGSVol.Text,
                    "Barite\n" + txtOutBariteVol.Text,
                    "Hematit\n" + txtOutHematitVol.Text,
                    "Calcium Carbonate\n" + txtOutCalCarVol.Text,
                    "Bentonite\n" + txtOutBentVol.Text,
                    "Drilled Solids\n" + txtOutDrillSolVol.Text
                };
                //~~~~~~~~~~~~~~~~~~~~~~~~
                string[] subShapeName = new string[15]
                                {
                    "Water",
                    "Oil/Lubricant",
                    "Glycol",
                    "Solids",
                    "Dissolved Solids",
                    "Corrected Solids",
                    "NaCl",
                    "KCl",
                    "Weighting Material",
                    "LGS",
                    "Barite",
                    "Hematit",
                    "Calcium Carbonate",
                    "Bentonite",
                    "Drilled Solids"
                };
                //~~~~~~~~~~~~~~~~~~~~~~~~
                Point[] subShapePos = new Point[15] //Left-Top
                {
                    new Point( 43 ,130),
                    new Point(164 ,130),
                    new Point(305 ,130),
                    new Point(539 ,130),
                    new Point(412 ,187),
                    new Point(710 ,187),
                    new Point(385 ,245),
                    new Point(483 ,245),
                    new Point(630 ,245),
                    new Point(1026,245),
                    new Point(595 ,315),
                    new Point(697 ,315),
                    new Point(797 ,315),
                    new Point(973 ,315),
                    new Point(1075,315)
                };
                //~~~~~~~~~~~~~~~~~~~~~~~~
                Size[] subShapeSize = new Size[15] //Width-Height
                                {
                    new Size(82, 44),
                    new Size(108,44),
                    new Size(84,44),
                    new Size(206,44),
                    new Size(135,44),
                    new Size(205,44),
                    new Size(88,44),
                    new Size(88,44),
                    new Size(145,44),
                    new Size(90,44),
                    new Size(97,44),
                    new Size(97,44),
                    new Size(140,44),
                    new Size(99,44),
                    new Size(97,44)
                };
                //~~~~~~~~~~~~~~~~~~~~~~~~

                worksheet.View.ShowGridLines = false;
                //~~~~~~~~~~~~~~~~~~~~~~~~
                {//DFS
                    //~~~~~~~
                    var shp = worksheet.Drawings.AddShape("DFSName", eShapeStyle.Rect);
                    shp.SetPosition(38, 336);//Top-Left
                    shp.SetSize(375, 54);//Width-Height
                    //~~~~~~~
                    shp.Text = dfsName;
                    shp.TextAnchoring = OfficeOpenXml.Drawing.eTextAnchoringType.Center;
                    shp.TextVertical = OfficeOpenXml.Drawing.eTextVerticalType.Horizontal;
                    shp.TextAnchoringControl = true;
                    shp.TextAlignment = eTextAlignment.Center;
                    //~~~~~~~
                    shp.Font.Size = 14;
                    shp.Font.Bold = true;
                    shp.Font.Color = Color.Black;
                    //~~~~~~~
                    shp.Fill.Style = eFillStyle.SolidFill;
                    shp.Fill.Color = Color.FromArgb(149, 179, 215);
                    //~~~~~~~
                    shp.Border.Fill.Style = eFillStyle.SolidFill;
                    shp.Border.LineStyle = OfficeOpenXml.Drawing.eLineStyle.Solid;
                    shp.Border.Width = 1;
                    shp.Border.Fill.Color = Color.Black;
                    //~~~~~~~
                }
                //~~~~~~~~~~~~~~~~~~~~~~~~
                for (int i = 0; i < subShapeName.Length; i++)
                {
                    var shp = worksheet.Drawings.AddShape(subShapeName[i], eShapeStyle.Rect);
                    shp.SetPosition(subShapePos[i].Y, subShapePos[i].X);//Top-Left
                    shp.SetSize(subShapeSize[i].Width, subShapeSize[i].Height);//Width-Height
                    //~~~~~~~
                    shp.Text = subShapeText[i];
                    shp.TextAnchoring = OfficeOpenXml.Drawing.eTextAnchoringType.Center;
                    shp.TextVertical = OfficeOpenXml.Drawing.eTextVerticalType.Horizontal;
                    shp.TextAnchoringControl = true;
                    shp.TextAlignment = eTextAlignment.Center;
                    //~~~~~~~
                    shp.Font.Size = 10;
                    shp.Font.Bold = true;
                    shp.Font.Color = Color.Black;
                    //~~~~~~~
                    shp.Fill.Style = eFillStyle.SolidFill;
                    shp.Fill.Color = Color.FromArgb(217, 217, 217);
                    //~~~~~~~
                    shp.Border.Fill.Style = eFillStyle.SolidFill;
                    shp.Border.LineStyle = OfficeOpenXml.Drawing.eLineStyle.Solid;
                    shp.Border.Width = 1;
                    shp.Border.Fill.Color = Color.Black;
                    //~~~~~~~
                }
                //~~~~~~~~~~~~~~~~~~~~~~~~

                package.Save();
            }
        }

        private void pnlTotal_Paint(object sender, PaintEventArgs e)
        {

        }
        //-------------------------------------------------------


    }
}
