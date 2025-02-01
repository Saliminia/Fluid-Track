using DMR.User_DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DMR
{
    public partial class MudVolManagementForm : Form, IMyForm
    {
        const string CLASS_NAME = "MudVolManagementForm";
        RoleAccess access = null;
        frmMain mainForm = null;

        string selectedMudVolDfsName = "";
        Int64 selectedMudVolDfsID = -1;

        bool skipCellValueChanged_dgvRec = false;
        bool skipCellValueChanged_dgvTrans = false;
        bool skipCellValueChanged_dgvBuiltVol = false;
        bool skipCellValueChanged_dgvBuiltUsing = false;
        bool skipCellValueChanged_dgvTreat = false;

        bool skipCellValueChanged_dgvPitTankPit = false;
        //bool skipCellValueChanged_dgvTotalWell = false; 
        bool skipCellValueChanged_dgvPitTankCMT = false;
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
                LoadDrillingFluidSystemNames();
                selectedMudVolDfsName = "";
                selectedMudVolDfsID = -1;

                OnSelectedDfsChanged();

                LoadPitTankVol();
                LoadCMT_Spacer();
                LoadTotalWellVol();

                UpdateTotalVolumeManagement();
            }

            ResetApplyBtns();
        }
        //-------------------------------------------------------
        void IMyForm.OnInit()
        {
            //~~~~~~~~~~~~~~~~~ AT_None = Hide ~~~~~~~~~~~~~~~~~~~~
            if (access.mudVolumeManagement_DFSs == RoleAccess.AcessTypes.AT_None)
            {
                tcMudVolMan.TabPages.Remove(tpDFSs);
            }
            else if (access.mudVolumeManagement_DFSs == RoleAccess.AcessTypes.AT_ReadOnly)
            {
                dgvRec.ReadOnly = true;
                btnRecAdd.Enabled = false;
                btnRecRemove.Enabled = false;
                btnRecApply.Enabled = false;
                btnRecAutoCompute.Enabled = false;

                dgvTrans.ReadOnly = true;
                btnTransAdd.Enabled = false;
                btnTransRemove.Enabled = false;
                btnTransApply.Enabled = false;
                btnTransAutoCompute.Enabled = false;

                dgvBuiltVol.ReadOnly = true;
                grbStartBuiltRainGain.Enabled = false;
                btnBuiltVolAdd.Enabled = false;
                btnBuiltVolRemove.Enabled = false;
                btnBuiltVolApply.Enabled = false;

                dgvBuiltUsing.ReadOnly = true;
                btnBuiltUsingAdd.Enabled = false;
                btnBuiltUsingRemove.Enabled = false;
                btnBuiltUsingApply.Enabled = false;
                btnBuiltUsingOthersAutoCompute.Enabled = false;

                dgvTreat.ReadOnly = true;
                btnTreatAdd.Enabled = false;
                btnTreatRemove.Enabled = false;
                btnTreatApply.Enabled = false;

                grbDipRetDaily.Enabled = false;
                grbDipRetAtTheEnd.Enabled = false;
                btnDispRetApply.Enabled = false;
            }

            DGV_Operations.ColorizeNestedColumnHeader(dgvRec, 2, 5);
            DGV_Operations.HandleKeyDown(dgvRec, dgvRec_CellMouseClick);

            DGV_Operations.ColorizeNestedColumnHeader(dgvTrans, 2, 5);
            DGV_Operations.HandleKeyDown(dgvTrans, dgvTrans_CellMouseClick);

            DGV_Operations.ColorizeNestedColumnHeader(dgvBuiltVol, 2, 3);
            DGV_Operations.ColorizeComputedColumnHeader(dgvBuiltVol, 10);
            DGV_Operations.HandleKeyDown(dgvBuiltVol, dgvBuiltVol_CellMouseClick);

            DGV_Operations.ColorizeNestedColumnHeader(dgvBuiltUsing, 3, 5);
            DGV_Operations.HandleKeyDown(dgvBuiltUsing, dgvBuiltUsing_CellMouseClick);

            DGV_Operations.ColorizeNestedColumnHeader(dgvTreat, 3, 4);
            DGV_Operations.ColorizeComputedColumnHeader(dgvTreat, 11, 12);
            DGV_Operations.HandleKeyDown(dgvTreat, dgvTreat_CellMouseClick);

            if (access.mudVolumeManagement_Pit_TankVolume == RoleAccess.AcessTypes.AT_None)
            {
                tcMudVolMan.TabPages.Remove(tpPit);
            }
            else if (access.mudVolumeManagement_Pit_TankVolume == RoleAccess.AcessTypes.AT_ReadOnly)
            {
                dgvPitTank.ReadOnly = true;
                btnPitTankApply.Enabled = false;

                dgvPitTankCMT_Spacer.ReadOnly = true;
                btnCMTApply.Enabled = false;
            }

            DGV_Operations.ColorizeNestedColumnHeader(dgvPitTank, 4);
            DGV_Operations.ColorizeFixedColumnHeader(dgvPitTank, 1);
            DGV_Operations.HandleKeyDown(dgvPitTank, dgvPitTank_CellMouseClick);

            if (access.mudVolumeManagement_TotalVolManagement == RoleAccess.AcessTypes.AT_None)
            {
                tcMudVolMan.TabPages.Remove(tpTotalVolMan);
            }//nothing to write to

            DGV_Operations.ColorizeNestedColumnHeader(dgvTotalWellVol, 5);
            DGV_Operations.ColorizeFixedColumnHeader(dgvTotalWellVol, 2);
            DGV_Operations.ColorizeComputedColumnHeader(dgvTotalWellVol, 3, 4);
            DGV_Operations.HandleKeyDown(dgvTotalWellVol, dgvTotalWellVol_CellMouseClick);

            DGV_Operations.ColorizeNestedColumnHeader(dgvPitTankCMT_Spacer, 2);
            DGV_Operations.HandleKeyDown(dgvPitTankCMT_Spacer, dgvPitTankCMT_Spacer_CellMouseClick);

            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            CleanVolatileParts();
            ResetApplyBtns();
            //~~~~~~~~~~~~~~~~~~~~
            //Current Status
            {
                DataGridViewComboBoxColumn cmb = new DataGridViewComboBoxColumn();
                cmb.HeaderText = "Current Status";
                cmb.Items.Add("Active");
                cmb.Items.Add("Reserve");
                cmb.Width = 100;
                cmb.FlatStyle = FlatStyle.Popup;
                cmb.SortMode = DataGridViewColumnSortMode.Programmatic;
                dgvPitTank.Columns.Insert(2, cmb);
            }

            dgvPitTankCMT_Spacer.Rows.Add();
            dgvPitTankCMT_Spacer.Rows.Add();
            dgvPitTankCMT_Spacer.Rows.Add();

            dgvPitTankCMT_Spacer.Rows[0].HeaderCell.Value = "Spacer";
            dgvPitTankCMT_Spacer.Rows[0].Height = 50;
            dgvPitTankCMT_Spacer.Rows[1].HeaderCell.Value = "Cement Slurry Lead";
            dgvPitTankCMT_Spacer.Rows[1].Height = 50;
            dgvPitTankCMT_Spacer.Rows[2].HeaderCell.Value = "Cement Slurry Tail";
            dgvPitTankCMT_Spacer.Rows[2].Height = 50;
        }
        //-------------------------------------------------------
        void IMyForm.OnCurrentUnitsChanged()
        {
            //~~~~~~~~~~~~~~
            ProjectForm prjForm = (ProjectForm)mainForm.subForms[mainForm.PorjectFormID];
            string selUnitVol = prjForm.lblUnitSelectedLiquidVolume.Tag.ToString();
            string selUnitMW = prjForm.lblUnitSelectedMudWeight.Tag.ToString();
            string selUnitWeight = prjForm.lblUnitSelectedWeight.Tag.ToString();
            string selUnitDepth = prjForm.lblUnitSelectedDepth.Tag.ToString();
            string selUnitFlowRate = prjForm.lblUnitSelectedFlowRate.Tag.ToString();
            //~~~~~~~~~~~~~~


            //Rec./Trans. ~~~~~~~~~~~~~~~~~~~~
            UnitString.WriteUnit(label13, selUnitVol);
            UnitString.WriteUnit(label3, selUnitVol);

            UnitString.WriteUnit(dgvRec.Columns[4], selUnitVol);
            UnitString.WriteUnit(dgvRec.Columns[6], selUnitMW);

            UnitString.WriteUnit(dgvTrans.Columns[4], selUnitVol);
            UnitString.WriteUnit(dgvTrans.Columns[6], selUnitMW);


            //Built Mud ~~~~~~~~~~~~~~~~~~~~
            UnitString.WriteUnit(label6, selUnitVol);
            UnitString.WriteUnit(label8, selUnitVol);
            UnitString.WriteUnit(label10, selUnitVol);

            UnitString.WriteUnit(dgvBuiltVol.Columns[3], selUnitVol);
            UnitString.WriteUnit(dgvBuiltVol.Columns[8], selUnitVol);
            UnitString.WriteUnit(dgvBuiltVol.Columns[10], selUnitVol);
            UnitString.WriteUnit(dgvBuiltVol.Columns[9], selUnitMW);

            UnitString.WriteUnit(label4, selUnitVol);

            UnitString.WriteUnit(dgvBuiltUsing.Columns[4], selUnitVol);

            UnitString.WriteUnit(dgvBuiltUsing.Columns[6], selUnitMW);

            UnitString.WriteUnit(label11, selUnitVol);

            //Treated Mud ~~~~~~~~~~~~~~~~~~~~
            UnitString.WriteUnit(dgvTreat.Columns[2], selUnitVol);
            UnitString.WriteUnit(dgvTreat.Columns[4], selUnitVol);
            UnitString.WriteUnit(dgvTreat.Columns[9], selUnitVol);
            UnitString.WriteUnit(dgvTreat.Columns[11], selUnitVol);
            UnitString.WriteUnit(dgvTreat.Columns[12], selUnitVol);
            UnitString.WriteUnit(dgvTreat.Columns[10], selUnitMW);

            UnitString.WriteUnit(label12, selUnitVol);

            //Disp./Ret. ~~~~~~~~~~~~~~~~~~~~
            UnitString.WriteUnit(label29, selUnitVol);
            UnitString.WriteUnit(label16, selUnitMW);

            UnitString.WriteUnit(label35, selUnitVol);
            UnitString.WriteUnit(label34, selUnitMW);

            UnitString.WriteUnit(label23, selUnitDepth);
            UnitString.WriteUnit(label22, selUnitFlowRate);
            UnitString.WriteUnit(label21, selUnitFlowRate);
            UnitString.WriteUnit(label20, selUnitVol);
            UnitString.WriteUnit(label18, selUnitMW);

            //Pit/Tank Vol. ~~~~~~~~~~~~~~~~~~~~
            UnitString.WriteUnit(dgvPitTank.Columns[3], selUnitVol);
            UnitString.WriteUnit(dgvPitTank.Columns[6], selUnitMW);

            UnitString.WriteUnit(dgvPitTankCMT_Spacer.Columns[0], selUnitVol);
            UnitString.WriteUnit(dgvPitTankCMT_Spacer.Columns[1], selUnitMW);

            UnitString.WriteUnit(dgvTotalWellVol.Columns[3], selUnitVol);
            UnitString.WriteUnit(dgvTotalWellVol.Columns[4], selUnitVol);


            //Total Vol Management ~~~~~~~~~~~~~~~~~~~~
            UnitString.WriteUnit(label71, selUnitVol);
            UnitString.WriteUnit(label600, selUnitVol);
            UnitString.WriteUnit(label700, selUnitVol);

            UnitString.WriteUnit(lblTVM_HVM_StringVol, selUnitVol);
            UnitString.WriteUnit(label66, selUnitVol);
            UnitString.WriteUnit(label69, selUnitVol);
            UnitString.WriteUnit(label68, selUnitVol);
            UnitString.WriteUnit(label67, selUnitVol);

            UnitString.WriteUnit(label55, selUnitVol);
            UnitString.WriteUnit(label500, selUnitVol);
            UnitString.WriteUnit(label56, selUnitVol);
            UnitString.WriteUnit(label57, selUnitVol);
            UnitString.WriteUnit(label59, selUnitVol);
            UnitString.WriteUnit(label60, selUnitVol);
            UnitString.WriteUnit(label61, selUnitVol);
            UnitString.WriteUnit(label64, selUnitVol);
            UnitString.WriteUnit(label62, selUnitVol);
            UnitString.WriteUnit(label58, selUnitVol);

            UpdateComputationsByPressRefereshButtons();
        }
        //-------------------------------------------------------
        void UpdateComputationsByPressRefereshButtons()
        {
            //btnRecRefresh_Click(null, null);
            //btnTransRefresh_Click(null, null);
            //btnBuiltVolRefresh_Click(null, null);
            //btnBuiltUsingRefresh_Click(null, null);
            //btnTreatRefresh_Click(null, null);
            //btnPitTankRefresh_Click(null, null);
            //btnCMTRefresh_Click(null, null);
            //btnTotalWellVolRefresh_Click(null, null);

            //btnTotalVolManRefresh_Click(null, null);
            UpdateTotalVolumeManagement();
        }
        //-------------------------------------------------------
        void CleanVolatileParts()
        {
            trvDFSs.Nodes.Clear();
            //~~~~~~~~~~~~~~~~~~~~
            dgvRec.Rows.Clear();
            txtRecTotalVol.Text = "";

            dgvTrans.Rows.Clear();
            txtTransTotalVol.Text = "";
            //~~~~~~~~~~~~~~~~~~~~
            txtStartVol.Text = "";
            txtRainVol.Text = "";
            txtGainVol.Text = "";

            dgvBuiltVol.Rows.Clear();
            txtBuiltVolTotalVol.Text = "";

            dgvBuiltUsing.Rows.Clear();
            txtBuiltUsingTotalVol.Text = "";
            //~~~~~~~~~~~~~~~~~~~~
            dgvTreat.Rows.Clear();
            txtTreatTotalVol.Text = "";
            //~~~~~~~~~~~~~~~~~~~~
            txtDailyOver.Text = "";
            txtDailyPitMW.Text = "";
            txtDailyPitVol.Text = "";
            txtDailyStringDisp.Text = "";
            txtDailyWaste.Text = "";

            txtAtEndDepth.Text = "";
            txtAtEndMaxFR.Text = "";
            txtAtEndMinFR.Text = "";
            txtAtEndMW.Text = "";
            txtAtEndOver.Text = "";
            txtAtEndPitMW.Text = "";
            txtAtEndPitVol.Text = "";
            txtAtEndVol.Text = "";
            txtAtEndWaste.Text = "";
            //~~~~~~~~~~~~~~~~~~~~
            dgvPitTank.Rows.Clear();
            dgvTotalWellVol.Rows.Clear();

            for (int i = 0; i < dgvPitTankCMT_Spacer.Rows.Count; i++)
                for (int j = 0; j < dgvPitTankCMT_Spacer.Columns.Count; j++)
                    dgvPitTankCMT_Spacer[j, i].Value = "";
            //~~~~~~~~~~~~~~~~~~~~
            txtTVM_SVM_TreVol.Text = "";
            txtTVM_SVM_SurVol.Text = "";
            txtTVM_SVM_PitVol.Text = "";

            txtTVM_HVM_StringVol.Text = "";
            txtTVM_HVM_AnnVol.Text = "";
            txtTVM_HVM_BelVol.Text = "";
            txtTVM_HVM_TotalVol.Text = "";
            txtTVM_HVM_DrillVol.Text = "";

            txtTVM_BVM_RecVol.Text = "";
            txtTVM_BVM_TransVol.Text = "";
            txtTVM_BVM_Gain.Text = "";
            txtTVM_BVM_AddVol.Text = "";
            txtTVM_SVM_TreVol.Text = "";
            txtTVM_BVM_BuildVol.Text = "";
            txtTVM_BVM_StartVol.Text = "";
            txtTVM_BVM_RetVol.Text = "";
            txtTVM_BVM_DispVol.Text = "";
            txtTVM_BVM_LossVol.Text = "";
            txtTVM_BVM_EndVol.Text = "";

            txtTVM_CD_BblToBit.Text = "";
            txtTVM_CD_BblBottomUp.Text = "";
            txtTVM_CD_BblTotalCir.Text = "";

            txtTVM_CD_MinsToBit.Text = "";
            txtTVM_CD_MinsBottomUp.Text = "";
            txtTVM_CD_MinsTotalCir.Text = "";
            //~~~~~~~~~~~~~~~~~~~~
        }
        //-------------------------------------------------------
        void ResetApplyBtns()
        {
            btnRecApply.ImageKey = "check";
            btnRecApply.BackColor = frmMain.checkColor;
            btnTransApply.ImageKey = "check";
            btnTransApply.BackColor = frmMain.checkColor;
            btnGeneralApply.ImageKey = "check";
            btnGeneralApply.BackColor = frmMain.checkColor;
            btnBuiltVolApply.ImageKey = "check";
            btnBuiltVolApply.BackColor = frmMain.checkColor;
            btnBuiltUsingApply.ImageKey = "check";
            btnBuiltUsingApply.BackColor = frmMain.checkColor;
            btnTreatApply.ImageKey = "check";
            btnTreatApply.BackColor = frmMain.checkColor;
            btnDispRetApply.ImageKey = "check";
            btnDispRetApply.BackColor = frmMain.checkColor;

            btnPitTankApply.ImageKey = "check";
            btnPitTankApply.BackColor = frmMain.checkColor;
            btnCMTApply.ImageKey = "check";
            btnCMTApply.BackColor = frmMain.checkColor;
        }
        //-------------------------------------------------------
        public MudVolManagementForm(RoleAccess access, frmMain mainForm)
        {
            this.access = access;
            this.mainForm = mainForm;

            InitializeComponent();
        }
        //-------------------------------------------------------
        private void MudVolForm_Load(object sender, EventArgs e)
        {
#if !NOLOCK
            object obj1 = new object();
            uint RProg = (uint)Program.Rand.Next(2147483647);

            RProg = (uint)Program.Rand.Next(2147483647);
            obj1 = Program.arm.GetARMData(ARM.IWhichData.PRODUCT_NAME, RProg);
            string str = Program.cls.DecodData(obj1, ARM.IWhichData.PRODUCT_NAME, RProg);

            if (str.Substring(0, 7).Trim() != "PDF DMR")
            {
                MessageBox.Show("Connecting to Security Key Lock failed. Please plug security lock.", "PDF DMR System", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Program.LockID = string.Empty;
                this.Close();
            }
#endif
        }
        //-------------------------------------------------------
        void LoadDrillingFluidSystemNames()
        {
            trvDFSs.Nodes.Clear();

            string METHOD = "LoadDrillingFluidSystemNames : " + CLASS_NAME;

            try
            {
                TreeNode parentNode = new TreeNode("Drilling Fluid System");
                parentNode.Tag = "parent";

                trvDFSs.Nodes.Add(parentNode);

                string query = "select ID, d.DrillingFluidSystem from rt_Rep2MudVolManDFS join lt_DrillingFluidSystem d on Dfs_AutoID = d.AutoID  " +
                        " where ReportID = " + frmMain.selectedRepID.ToString() +
                        " order by DrillingFluidSystem asc";

                DataSet ds = new DataSet();

                if (ConnectionManager.ExecQuery(query, ref ds))
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        TreeNode sysNode = new TreeNode(dr.ItemArray[1].ToString());//DFS Name
                        sysNode.Name = dr.ItemArray[0].ToString();//AutoID
                        sysNode.Tag = "dfs";

                        parentNode.Nodes.Add(sysNode);
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
        void LoadPitTankVol()
        {
            skipCellValueChanged_dgvPitTankPit = true;

            dgvPitTank.Rows.Clear();

            try
            {
                string query =
                  " select rpt.ID, pre.Value + \' # \' + rtrim(ltrim(str(p.PitNum))), "
                + " case when rpt.CurrentStatus = \'A\' then \'Active\' else \'Reserve\' end, "
                + " ltrim(rtrim(str(isnull(rpt.CurMudVol, 0), 15, 1))), CurDfs_AutoID, d.DrillingFluidSystem, isnull(rpt.MW, 0), p.ID "

                + " from "
                + " rt_Rig2PitTank p join lt_PredefValues pre on p.PitName_PredefAutoID = pre.AutoID "
                + " left join (select * from rt_Rep2MudVolMan_PitTankVol where ReportID = " + frmMain.selectedRepID.ToString() + " ) rpt "
                + " on rpt.PitID = p.ID "
                + " left join lt_DrillingFluidSystem d on rpt.CurDfs_AutoID = d.AutoID "
                + " where p.RigID = " + frmMain.selected_RW_RigID.ToString()
                + " order by p.UserOrder ";

                DataSet ds = ConnectionManager.ExecQuery(query);

                if (ds != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        dgvPitTank.Rows.Add(ds.Tables[0].Rows[i].ItemArray);
                        dgvPitTank.Rows[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dgvPitTank.Rows[i].HeaderCell.Value = (i + 1).ToString();
                    }

                    ds.Dispose();
                }
                else
                {
                    skipCellValueChanged_dgvPitTankPit = false;
                    return;
                }
            }
            catch (Exception)
            {
                //ErrorManager.Set_Error(new Errors("Can not load data from data base", ex.Message, METHOD));
                //will be handled outside
            }

            skipCellValueChanged_dgvPitTankPit = false;
        }
        //-------------------------------------------------------
        void LoadTotalWellVol()
        {
            //skipCellValueChanged_dgvTotalWell = true;

            dgvTotalWellVol.Rows.Clear();

            try
            {
                string query = "select ID, Dfs_AutoID, d.DrillingFluidSystem from rt_Rep2MudVolManDFS join lt_DrillingFluidSystem d on Dfs_AutoID = d.AutoID  " +
                        " where ReportID = " + frmMain.selectedRepID.ToString() +
                        " order by d.DrillingFluidSystem asc";

                DataSet ds = ConnectionManager.ExecQuery(query);

                if (ds != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        dgvTotalWellVol.Rows.Add(ds.Tables[0].Rows[i].ItemArray);
                        dgvTotalWellVol.Rows[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dgvTotalWellVol.Rows[i].HeaderCell.Value = (i + 1).ToString();

                        ComputeTotalWellVolBeforeMerge(i);
                        ComputeTotalWellVolAfterMerge(i);
                    }

                    ds.Dispose();
                }
                else
                {
                    //skipCellValueChanged_dgvTotalWell = false;
                    return;
                }
            }
            catch (Exception)
            {
                //ErrorManager.Set_Error(new Errors("Can not load data from data base", ex.Message, METHOD));
                //will be handled outside
            }

            //skipCellValueChanged_dgvTotalWell = false;
        }
        //-------------------------------------------------------
        void ComputeTotalWellVolBeforeMerge(int rowID)
        {
            try
            {
                string mudDfsIDStr = DGV_Operations.CellValueAsString(dgvTotalWellVol.Rows[rowID].Cells[0]);
                //string dfs = DGV_Operations.CellValueAsString(dgvTotalWellVol.Rows[rowID].Cells[2]);
                string dfsAutoID = DGV_Operations.CellValueAsString(dgvTotalWellVol.Rows[rowID].Cells[1]);
                //~~~~~~~~~~~~~~~~~~~~
                decimal prevAfterMerge = 0;

                object[] dtRow = FetchTableData.GetFieldsOfLastRevOfPrevReport(frmMain.selectedRepNum, frmMain.selected_RW_WellID, " Num ", false);
                if (dtRow != null && dtRow.Length == 1)
                {
                    string query = " select dbo.fn_Get_AfterMergeVol_ByWellID_UptoRepNum ("
                                        + frmMain.selected_RW_WellID.ToString() + ","
                                        + dtRow[0].ToString() + ","
                                        + dfsAutoID
                                    + ")";

                    DataSet ds = new DataSet();

                    if (ConnectionManager.ExecQuery(query, ref ds, 1))
                    {
                        if (ds.Tables[0].Rows[0][0] != DBNull.Value)
                            prevAfterMerge = Convert.ToDecimal(ds.Tables[0].Rows[0][0]);

                        ds.Dispose();
                    }
                }
                //~~~~~~~~~~~~~~~~~~~~
                decimal others = 0;
                {
                    string query =
                        " select DailyStringDisp + AtEndMudDispVol "
                        + " 		- (DailyMudRetPitVol + DailyMudRetWasteVol + DailyMudRetOverVol) "
                        + " 		- (AtEndMudRetPitVol + AtEndMudRetWasteVol + AtEndMudRetOverVol) "
                        + " from rt_Rep2MudVolManDFS "
                        + " where ID = " + mudDfsIDStr;

                    DataSet ds = new DataSet();

                    if (ConnectionManager.ExecQuery(query, ref ds, 1))
                    {
                        others = Convert.ToDecimal(ds.Tables[0].Rows[0][0]);
                        ds.Dispose();
                    }
                }

                dgvTotalWellVol.Rows[rowID].Cells[3].Value = (prevAfterMerge + others).ToString("0.###");//before
                return;
            }
            catch (Exception)
            {
            }
            //~~~~~~~~~~~~~~~~~~~~
            dgvTotalWellVol.Rows[rowID].Cells[3].Value = "Error";
        }
        //-------------------------------------------------------
        void ComputeTotalWellVolAfterMerge(int rowID)
        {
            try
            {
                //string dfs = DGV_Operations.CellValueAsString(dgvTotalWellVol.Rows[rowID].Cells[2]);
                string dfsAutoID = DGV_Operations.CellValueAsString(dgvTotalWellVol.Rows[rowID].Cells[1]);

                decimal after = 0;

                string query = " select dbo.fn_Get_AfterMergeVol_ByWellID_UptoRepNum ("
                                    + frmMain.selected_RW_WellID.ToString() + ","
                                    + frmMain.selectedRepNum.ToString() + ","
                                    + dfsAutoID
                                + ")";

                DataSet ds = new DataSet();

                if (ConnectionManager.ExecQuery(query, ref ds, 1))
                {
                    if (ds.Tables[0].Rows[0][0] != DBNull.Value)
                        after = Convert.ToDecimal(ds.Tables[0].Rows[0][0]);

                    ds.Dispose();
                }

                dgvTotalWellVol.Rows[rowID].Cells[4].Value = after.ToString("0.###");//after
                return;
            }
            catch (Exception)
            {
            }

            dgvTotalWellVol.Rows[rowID].Cells[4].Value = "Error";
        }
        //-------------------------------------------------------
        void LoadCMT_Spacer()
        {
            skipCellValueChanged_dgvPitTankCMT = true;

            try
            {
                string query =
                " select SpacerVol_MVMPitVol, SpacerWeight_MVMPitVol, CementSlurryLeadVol_MVMPitVol, "
                + " CementSlurryLeadWeight_MVMPitVol, CementSlurryTailVol_MVMPitVol, CementSlurryTailWeight_MVMPitVol "
                + " from at_Report where ID = " + frmMain.selectedRepID.ToString();

                DataSet ds = new DataSet();

                if (ConnectionManager.ExecQuery(query, ref ds, 1))
                {
                    dgvPitTankCMT_Spacer[0, 0].Value = ds.Tables[0].Rows[0][0].ToString();
                    dgvPitTankCMT_Spacer[1, 0].Value = ds.Tables[0].Rows[0][1].ToString();
                    dgvPitTankCMT_Spacer[2, 0].Value = CmtSpacerProducts(1);

                    dgvPitTankCMT_Spacer[0, 1].Value = ds.Tables[0].Rows[0][2].ToString();
                    dgvPitTankCMT_Spacer[1, 1].Value = ds.Tables[0].Rows[0][3].ToString();
                    dgvPitTankCMT_Spacer[2, 1].Value = CmtSpacerProducts(2);

                    dgvPitTankCMT_Spacer[0, 2].Value = ds.Tables[0].Rows[0][4].ToString();
                    dgvPitTankCMT_Spacer[1, 2].Value = ds.Tables[0].Rows[0][5].ToString();
                    dgvPitTankCMT_Spacer[2, 2].Value = CmtSpacerProducts(3);

                    ds.Dispose();
                }
                else
                {
                    skipCellValueChanged_dgvPitTankCMT = false;
                    return;
                }
            }
            catch (Exception)
            {
                //ErrorManager.Set_Error(new Errors("Can not load data from data base", ex.Message, METHOD));
                //will be handled outside
            }

            skipCellValueChanged_dgvPitTankCMT = false;
        }
        //-------------------------------------------------------
        string CmtSpacerProducts(int cmtSpcType)
        {
            string result = "";

            try
            {
                string query =
                    " select p.Name, p.UnitSize, csp.Used "
                    + " from rt_Rep2MudVolManDFS_CmtSpc_Prd csp join lt_Product p on csp.Prd_AutoID = p.AutoID "
                    + " where csp.CmtSpcType = " + cmtSpcType.ToString()
                    + " and ReportID = " + frmMain.selectedRepID.ToString()
                    + " and csp.Used > 0 ";

                DataSet ds = ConnectionManager.ExecQuery(query);

                if (ds != null)
                {
                    //copy row by row to preserve header of table
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        string Name = ds.Tables[0].Rows[i][0].ToString();
                        string UnitSize = ds.Tables[0].Rows[i][1].ToString();
                        string prdUsed = ds.Tables[0].Rows[i][2].ToString();

                        result += Name + "(" + UnitSize + ") : " + prdUsed + "\n";
                    }

                    ds.Dispose();
                }
            }
            catch (Exception)
            {
                return "";
            }

            return result;
        }
        //------------------------------------------------------
        void LoadDFSGeneral()
        {
            string query =
                " select "
                + " BuiltStartVol, BuiltRainVol, BuiltGainVol "
                + " ,DailyStringDisp, DailyMudRetPitVol, DailyMudRetWasteVol "
                + " ,DailyMudRetOverVol, DailyMudRetPitMW, AtEndMudDispDepth, AtEndMudDispMinFR	 "
                + " ,AtEndMudDispMaxFR, AtEndMudDispVol, AtEndMudDispMW, AtEndMudRetPitVol "
                + " ,AtEndMudRetWasteVol, AtEndMudRetOverVol, AtEndMudRetPitMW "
                + " from rt_Rep2MudVolManDFS where  "
                + " ID = " + selectedMudVolDfsID.ToString();

            DataSet ds = new DataSet();

            if (ConnectionManager.ExecQuery(query, ref ds, 1))
            {
                int index = 0;

                txtStartVol.Text = ds.Tables[0].Rows[0][index++].ToString();
                txtRainVol.Text = ds.Tables[0].Rows[0][index++].ToString();
                txtGainVol.Text = ds.Tables[0].Rows[0][index++].ToString();

                txtDailyStringDisp.Text = ds.Tables[0].Rows[0][index++].ToString();
                txtDailyPitVol.Text = ds.Tables[0].Rows[0][index++].ToString();
                txtDailyWaste.Text = ds.Tables[0].Rows[0][index++].ToString();
                txtDailyOver.Text = ds.Tables[0].Rows[0][index++].ToString();
                txtDailyPitMW.Text = ds.Tables[0].Rows[0][index++].ToString();

                txtAtEndDepth.Text = ds.Tables[0].Rows[0][index++].ToString();
                txtAtEndMinFR.Text = ds.Tables[0].Rows[0][index++].ToString();
                txtAtEndMaxFR.Text = ds.Tables[0].Rows[0][index++].ToString();
                txtAtEndVol.Text = ds.Tables[0].Rows[0][index++].ToString();
                txtAtEndMW.Text = ds.Tables[0].Rows[0][index++].ToString();
                txtAtEndPitVol.Text = ds.Tables[0].Rows[0][index++].ToString();
                txtAtEndWaste.Text = ds.Tables[0].Rows[0][index++].ToString();
                txtAtEndOver.Text = ds.Tables[0].Rows[0][index++].ToString();
                txtAtEndPitMW.Text = ds.Tables[0].Rows[0][index++].ToString();

                ds.Dispose();
            }
        }
        //-------------------------------------------------------
        void LoadRec()
        {
            skipCellValueChanged_dgvRec = true;
            dgvRec.Rows.Clear();

            try
            {
                string query =
                    " select rr.ID, rr.seq, "
                    + "      IIF(rr.RecTransLocation_ID is null, rr.OtherRecTransLocation, hp.PrjName+'/'+hp.RigName+'/'+hp.WellName+'/'+hp.HoleLabel), "
                    + "      hp.ID, rr.Vol, \'\', rr.MW  "
                    + " from rt_Rep2MudVolManDFS_RecTrans rr left join at_HolesOfProjects hp on rr.RecTransLocation_ID = hp.ID "
                    + " where  RecTrans_Flag = 0 and "
                    + " MudVolManDFS_ID = " + selectedMudVolDfsID.ToString()
                    + " order by rr.seq ";

                DataSet ds = ConnectionManager.ExecQuery(query);

                if (ds != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        dgvRec.Rows.Add(ds.Tables[0].Rows[i].ItemArray);
                        dgvRec.Rows[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dgvRec.Rows[i].HeaderCell.Value = (i + 1).ToString();

                        ComputeRecVolValues(i);
                    }

                    ds.Dispose();
                }
                else
                {
                    skipCellValueChanged_dgvRec = false;
                    return;
                }
            }
            catch (Exception)
            {
                //ErrorManager.Set_Error(new Errors("Can not load data from data base", ex.Message, METHOD));
                //will be handled outside
            }

            skipCellValueChanged_dgvRec = false;
            txtRecTotalVol.Text = DGV_Operations.SummationOnColumn(dgvRec, 4);
        }
        //-------------------------------------------------------
        void ComputeRecVolValues(int rowID)
        {
            skipCellValueChanged_dgvRec = true;

            try
            {
                //~~~~~~~~~~~~~~
                ProjectForm prjForm = (ProjectForm)mainForm.subForms[mainForm.PorjectFormID];
                string selUnitVol = prjForm.lblUnitSelectedLiquidVolume.Tag.ToString();
                //~~~~~~~~~~~~~~
                Int64 recID = Convert.ToInt64(DGV_Operations.CellValueAsString(dgvRec.Rows[rowID].Cells[0]));
                decimal vol = Convert.ToDecimal(DGV_Operations.CellValueAsString(dgvRec.Rows[rowID].Cells[4]));

                //Chemicals Consumption ~~~~~~~~~~~~~~~~~~~~~~
                string chemStr = ComputeChemicalVolumes_RecTrans(recID, vol);
                chemStr = UnitConverter.Convert("Liquid Volume", "bbl", selUnitVol, chemStr);
                dgvRec.Rows[rowID].Cells[5].Tag = chemStr;
                dgvRec.Rows[rowID].Cells[5].Value = Convert.ToDecimal(chemStr).ToString("0.###");
            }
            catch (Exception)
            {
            }

            skipCellValueChanged_dgvRec = false;
        }
        //-------------------------------------------------------
        void LoadTrans()
        {
            skipCellValueChanged_dgvTrans = true;
            dgvTrans.Rows.Clear();

            try
            {
                string query =
                   " select rr.ID, rr.seq, "
                    + "      IIF(rr.RecTransLocation_ID is null, rr.OtherRecTransLocation, hp.PrjName+'/'+hp.RigName+'/'+hp.WellName+'/'+hp.HoleLabel), "
                    + "      hp.ID, rr.Vol, \'\', rr.MW  "
                + " from rt_Rep2MudVolManDFS_RecTrans rr left join at_HolesOfProjects hp on rr.RecTransLocation_ID = hp.ID "
                + " where  RecTrans_Flag = 1 and "
                + " MudVolManDFS_ID = " + selectedMudVolDfsID.ToString()
                + " order by rr.seq ";

                DataSet ds = ConnectionManager.ExecQuery(query);

                if (ds != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        dgvTrans.Rows.Add(ds.Tables[0].Rows[i].ItemArray);
                        dgvTrans.Rows[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dgvTrans.Rows[i].HeaderCell.Value = (i + 1).ToString();

                        ComputeTransVolValues(i);
                    }

                    ds.Dispose();
                }
                else
                {
                    skipCellValueChanged_dgvTrans = false;
                    return;
                }
            }
            catch (Exception)
            {
                //ErrorManager.Set_Error(new Errors("Can not load data from data base", ex.Message, METHOD));
                //will be handled outside
            }

            skipCellValueChanged_dgvTrans = false;
            txtTransTotalVol.Text = DGV_Operations.SummationOnColumn(dgvTrans, 4);
        }
        //-------------------------------------------------------
        void ComputeTransVolValues(int rowID)
        {
            skipCellValueChanged_dgvTrans = true;

            try
            {
                //~~~~~~~~~~~~~~
                ProjectForm prjForm = (ProjectForm)mainForm.subForms[mainForm.PorjectFormID];
                string selUnitVol = prjForm.lblUnitSelectedLiquidVolume.Tag.ToString();
                //~~~~~~~~~~~~~~
                Int64 transID = Convert.ToInt64(DGV_Operations.CellValueAsString(dgvTrans.Rows[rowID].Cells[0]));
                decimal vol = Convert.ToDecimal(DGV_Operations.CellValueAsString(dgvTrans.Rows[rowID].Cells[4]));

                //Chemicals Consumption & Vol. ~~~~~~~~~~~~~~~~~~~~~~
                string chemStr = ComputeChemicalVolumes_RecTrans(transID, vol);
                chemStr = UnitConverter.Convert("Liquid Volume", "bbl", selUnitVol, chemStr);
                dgvTrans.Rows[rowID].Cells[5].Tag = chemStr;
                dgvTrans.Rows[rowID].Cells[5].Value = Convert.ToDecimal(chemStr).ToString("0.###");
            }
            catch (Exception)
            {
            }

            skipCellValueChanged_dgvTrans = false;
        }
        //-------------------------------------------------------
        void LoadBuiltVol()
        {
            skipCellValueChanged_dgvBuiltVol = true;
            dgvBuiltVol.Rows.Clear();

            try
            {
                string query =
                  " select ID, seq, \'\', AddSeaWater + AddDrillWater + AddDeWater + AddLocalWater, AddSeaWater, AddDrillWater, AddDeWater, AddLocalWater, AddOil, MW, \'\' "
                + " from rt_Rep2MudVolManDFS_BuiltVol "
                + " where MudVolManDFS_ID =  " + selectedMudVolDfsID
                + " order by seq ";

                DataSet ds = ConnectionManager.ExecQuery(query);

                if (ds != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        dgvBuiltVol.Rows.Add(ds.Tables[0].Rows[i].ItemArray);

                        dgvBuiltVol.Rows[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dgvBuiltVol.Rows[i].HeaderCell.Value = (i + 1).ToString();

                        ComputeBuildVolValues(i);
                    }

                    ds.Dispose();
                }
                else
                {
                    skipCellValueChanged_dgvBuiltVol = false;
                    return;
                }
            }
            catch (Exception)
            {
                //ErrorManager.Set_Error(new Errors("Can not load data from data base", ex.Message, METHOD));
                //will be handled outside
            }

            skipCellValueChanged_dgvBuiltVol = false;

            txtBuiltVolTotalVol.Text = DGV_Operations.SummationOnColumn(dgvBuiltVol, 10);
        }
        //-------------------------------------------------------
        void ComputeBuildVolValues(int rowID)
        {
            skipCellValueChanged_dgvBuiltVol = true;

            try
            {
                //~~~~~~~~~~~~~~
                ProjectForm prjForm = (ProjectForm)mainForm.subForms[mainForm.PorjectFormID];
                string selUnitVol = prjForm.lblUnitSelectedLiquidVolume.Tag.ToString();
                //~~~~~~~~~~~~~~

                Int64 bvID = Convert.ToInt64(DGV_Operations.CellValueAsString(dgvBuiltVol.Rows[rowID].Cells[0]));
                //Chemicals Consumption & Vol. ~~~~~~~~~~~~~~~~~~~~~~
                string chemStr = ComputeChemicalVolumes("rt_Rep2MudVolManDFS_BuiltVol_Prd", "BuiltVol_ID", bvID);
                chemStr = UnitConverter.Convert("Liquid Volume", "bbl", selUnitVol, chemStr);
                dgvBuiltVol.Rows[rowID].Cells[2].Tag = chemStr;
                dgvBuiltVol.Rows[rowID].Cells[2].Value = Convert.ToDecimal(chemStr).ToString("0.###");

                //Added Water ~~~~~~~~~~~~~~~~~~~~~~
                decimal seaW = 0, drillW = 0, DeW = 0, LocalW = 0;
                if (AdvancedConvertor.ToDecimal(DGV_Operations.CellValueAsString(dgvBuiltVol.Rows[rowID].Cells[4]), ref seaW) &&
                    AdvancedConvertor.ToDecimal(DGV_Operations.CellValueAsString(dgvBuiltVol.Rows[rowID].Cells[5]), ref drillW) &&
                    AdvancedConvertor.ToDecimal(DGV_Operations.CellValueAsString(dgvBuiltVol.Rows[rowID].Cells[6]), ref DeW) &&
                    AdvancedConvertor.ToDecimal(DGV_Operations.CellValueAsString(dgvBuiltVol.Rows[rowID].Cells[7]), ref LocalW))
                {
                    dgvBuiltVol.Rows[rowID].Cells[3].Value = (seaW + drillW + DeW + LocalW).ToString("0.###");
                }
                else
                {
                    dgvBuiltVol.Rows[rowID].Cells[3].Value = "Error";
                }
                //Volume ~~~~~~~~~~~~~~~~~~~~~~
                decimal addedOil = 0, addedW = 0, chemVal = 0;
                if (AdvancedConvertor.ToDecimal(DGV_Operations.CellValueAsString(dgvBuiltVol.Rows[rowID].Cells[8]), ref addedOil) &&
                    AdvancedConvertor.ToDecimal(DGV_Operations.CellValueAsString(dgvBuiltVol.Rows[rowID].Cells[3]), ref addedW) &&
                    AdvancedConvertor.ToDecimal(chemStr, ref chemVal))
                {
                    dgvBuiltVol.Rows[rowID].Cells[10].Tag = (addedOil + addedW + chemVal).ToString();
                    dgvBuiltVol.Rows[rowID].Cells[10].Value = (addedOil + addedW + chemVal).ToString("0.###");
                }
                else
                {
                    dgvBuiltVol.Rows[rowID].Cells[10].Tag = null;
                    dgvBuiltVol.Rows[rowID].Cells[10].Value = "Error";
                }
                //~~~~~~~~~~~~~~~~~~~~~~
            }
            catch (Exception)
            {
            }

            skipCellValueChanged_dgvBuiltVol = false;
        }
        //-------------------------------------------------------
        void LoadBuiltUsing()
        {
            skipCellValueChanged_dgvBuiltUsing = true;
            dgvBuiltUsing.Rows.Clear();

            try
            {
                string query =
                  " select ID, seq, Dfs_AutoID, d.DrillingFluidSystem, UsedVol, \'\', MW"
                + " from rt_Rep2MudVolManDFS_BuiltUsingOthers left join lt_DrillingFluidSystem d on Dfs_AutoID = d.AutoID  "
                + " where MudVolManDFS_ID =  " + selectedMudVolDfsID
                + " order by seq ";

                DataSet ds = ConnectionManager.ExecQuery(query);

                if (ds != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        dgvBuiltUsing.Rows.Add(ds.Tables[0].Rows[i].ItemArray);

                        dgvBuiltUsing.Rows[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dgvBuiltUsing.Rows[i].HeaderCell.Value = (i + 1).ToString();

                        ComputeBuildUsingValues(i);
                    }

                    ds.Dispose();
                }
                else
                {
                    skipCellValueChanged_dgvBuiltUsing = false;
                    return;
                }
            }
            catch (Exception)
            {
                //ErrorManager.Set_Error(new Errors("Can not load data from data base", ex.Message, METHOD));
                //will be handled outside
            }

            skipCellValueChanged_dgvBuiltUsing = false;

            txtBuiltUsingTotalVol.Text = DGV_Operations.SummationOnColumn(dgvBuiltUsing, 4);
        }
        //-------------------------------------------------------
        void ComputeBuildUsingValues(int rowID)
        {
            skipCellValueChanged_dgvBuiltUsing = true;

            try
            {
                //~~~~~~~~~~~~~~
                ProjectForm prjForm = (ProjectForm)mainForm.subForms[mainForm.PorjectFormID];
                string selUnitVol = prjForm.lblUnitSelectedLiquidVolume.Tag.ToString();
                //~~~~~~~~~~~~~~

                Int64 buID = Convert.ToInt64(DGV_Operations.CellValueAsString(dgvBuiltUsing.Rows[rowID].Cells[0]));
                //Chemicals Consumption and Vol. ~~~~~~~~~~~~~~~~~~~~~~
                string chemStr = ComputeChemicalVolumes("rt_Rep2MudVolManDFS_BuiltUsingOthers_Prd", "BuiltUsingOthers_ID", buID);
                chemStr = UnitConverter.Convert("Liquid Volume", "bbl", selUnitVol, chemStr);
                dgvBuiltUsing.Rows[rowID].Cells[5].Tag = chemStr;
                dgvBuiltUsing.Rows[rowID].Cells[5].Value = Convert.ToDecimal(chemStr).ToString("0.###");

                txtBuiltUsingTotalVol.Text = DGV_Operations.SummationOnColumn(dgvBuiltUsing, 4);
            }
            catch (Exception)
            {
            }

            skipCellValueChanged_dgvBuiltUsing = false;
        }
        //-------------------------------------------------------
        void LoadTreated()
        {
            skipCellValueChanged_dgvTreat = true;
            dgvTreat.Rows.Clear();

            try
            {
                string query =
                  " select ID, seq, VolTreat, \'\', AddSeaWater	+ AddDrillWater +AddDeWater + AddLocalWater, AddSeaWater, AddDrillWater, AddDeWater, AddLocalWater, AddOil, MW, \'\', \'\', Reason "
                + " from rt_Rep2MudVolManDFS_TreatedVol "
                + " where MudVolManDFS_ID =  " + selectedMudVolDfsID
                + " order by seq ";

                DataSet ds = ConnectionManager.ExecQuery(query);

                if (ds != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        dgvTreat.Rows.Add(ds.Tables[0].Rows[i].ItemArray);

                        dgvTreat.Rows[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dgvTreat.Rows[i].HeaderCell.Value = (i + 1).ToString();

                        ComputeTreatedValues(i);
                    }

                    ds.Dispose();
                }
                else
                {
                    skipCellValueChanged_dgvTreat = false;
                    return;
                }
            }
            catch (Exception)
            {
                //ErrorManager.Set_Error(new Errors("Can not load data from data base", ex.Message, METHOD));
                //will be handled outside
            }

            skipCellValueChanged_dgvTreat = false;

            txtTreatTotalVol.Text = DGV_Operations.SummationOnColumn(dgvTreat, 12);
        }
        //-------------------------------------------------------
        void ComputeTreatedValues(int rowID)
        {
            skipCellValueChanged_dgvTreat = true;

            try
            {
                //~~~~~~~~~~~~~~
                ProjectForm prjForm = (ProjectForm)mainForm.subForms[mainForm.PorjectFormID];
                string selUnitVol = prjForm.lblUnitSelectedLiquidVolume.Tag.ToString();
                //~~~~~~~~~~~~~~

                Int64 tID = Convert.ToInt64(DGV_Operations.CellValueAsString(dgvTreat.Rows[rowID].Cells[0]));
                //Chemicals Consumption and Vol. ~~~~~~~~~~~~~~~~~~~~~~
                string chemStr = ComputeChemicalVolumes("rt_Rep2MudVolManDFS_TreatedVol_Prd", "TreatedVol_ID", tID);
                chemStr = UnitConverter.Convert("Liquid Volume", "bbl", selUnitVol, chemStr);
                dgvTreat.Rows[rowID].Cells[3].Tag = chemStr;
                dgvTreat.Rows[rowID].Cells[3].Value = Convert.ToDecimal(chemStr).ToString("0.###");

                //Added Water ~~~~~~~~~~~~~~~~~~~~~~
                decimal seaW = 0, drillW = 0, DeW = 0, LocalW = 0;
                if (AdvancedConvertor.ToDecimal(DGV_Operations.CellValueAsString(dgvTreat.Rows[rowID].Cells[5]), ref seaW) &&
                    AdvancedConvertor.ToDecimal(DGV_Operations.CellValueAsString(dgvTreat.Rows[rowID].Cells[6]), ref drillW) &&
                    AdvancedConvertor.ToDecimal(DGV_Operations.CellValueAsString(dgvTreat.Rows[rowID].Cells[7]), ref DeW) &&
                    AdvancedConvertor.ToDecimal(DGV_Operations.CellValueAsString(dgvTreat.Rows[rowID].Cells[8]), ref LocalW))
                {
                    dgvTreat.Rows[rowID].Cells[4].Value = (seaW + drillW + DeW + LocalW).ToString("0.###");
                }
                else
                {
                    dgvTreat.Rows[rowID].Cells[4].Value = "Error";
                }
                //Added Volume ~~~~~~~~~~~~~~~~~~~~~~
                decimal addedOil = 0, addedW = 0, chemVal = 0;
                if (AdvancedConvertor.ToDecimal(DGV_Operations.CellValueAsString(dgvTreat.Rows[rowID].Cells[9]), ref addedOil) &&
                    AdvancedConvertor.ToDecimal(DGV_Operations.CellValueAsString(dgvTreat.Rows[rowID].Cells[4]), ref addedW) &&
                    AdvancedConvertor.ToDecimal(chemStr, ref chemVal))
                {
                    dgvTreat.Rows[rowID].Cells[11].Tag = (addedOil + addedW + chemVal).ToString();
                    dgvTreat.Rows[rowID].Cells[11].Value = (addedOil + addedW + chemVal).ToString("0.###");
                }
                else
                {
                    dgvTreat.Rows[rowID].Cells[11].Tag = null;
                    dgvTreat.Rows[rowID].Cells[11].Value = "Error";
                }
                //Final Volume ~~~~~~~~~~~~~~~~~~~~~~
                decimal addedVol = 0, volForTreat = 0;
                if (AdvancedConvertor.ToDecimal(DGV_Operations.CellValueAsString(dgvTreat.Rows[rowID].Cells[11]), ref addedVol) &&
                    AdvancedConvertor.ToDecimal(DGV_Operations.CellValueAsString(dgvTreat.Rows[rowID].Cells[2]), ref volForTreat))
                {
                    dgvTreat.Rows[rowID].Cells[12].Value = (addedVol + volForTreat).ToString("0.###");
                }
                else
                {
                    dgvTreat.Rows[rowID].Cells[12].Value = "Error";
                }
                //~~~~~~~~~~~~~~~~~~~~~~

                txtTreatTotalVol.Text = DGV_Operations.SummationOnColumn(dgvTreat, 12);
            }
            catch (Exception)
            {
            }

            skipCellValueChanged_dgvTreat = false;
        }
        //-------------------------------------------------------
        void OnSelectedDfsChanged()
        {
            if (selectedMudVolDfsName == "")
            {
                lblSelectedDFS.Text = "";
                //~~~~~~~~~~~~~~~~~~~~
                dgvRec.Rows.Clear();
                txtRecTotalVol.Text = "";

                dgvTrans.Rows.Clear();
                txtTransTotalVol.Text = "";
                //~~~~~~~~~~~~~~~~~~~~
                txtStartVol.Text = "";
                txtRainVol.Text = "";
                txtGainVol.Text = "";

                dgvBuiltVol.Rows.Clear();
                txtBuiltVolTotalVol.Text = "";

                dgvBuiltUsing.Rows.Clear();
                txtBuiltUsingTotalVol.Text = "";
                //~~~~~~~~~~~~~~~~~~~~
                dgvTreat.Rows.Clear();
                txtTreatTotalVol.Text = "";
                //~~~~~~~~~~~~~~~~~~~~
                txtDailyOver.Text = "";
                txtDailyPitMW.Text = "";
                txtDailyPitVol.Text = "";
                txtDailyStringDisp.Text = "";
                txtDailyWaste.Text = "";

                txtAtEndDepth.Text = "";
                txtAtEndMaxFR.Text = "";
                txtAtEndMinFR.Text = "";
                txtAtEndMW.Text = "";
                txtAtEndOver.Text = "";
                txtAtEndPitMW.Text = "";
                txtAtEndPitVol.Text = "";
                txtAtEndVol.Text = "";
                txtAtEndWaste.Text = "";
                //~~~~~~~~~~~~~~~~~~~~


                tcDfs.Enabled = false;
            }
            else
            {
                tcDfs.Enabled = true;

                LoadDFSGeneral();
                LoadRec();
                LoadTrans();
                LoadBuiltVol();
                LoadBuiltUsing();
                LoadTreated();

                //~~~~~~~~~~~~~~~~~~~~
                lblSelectedDFS.Text = selectedMudVolDfsName;
            }

            btnRecApply.ImageKey = "check";
            btnRecApply.BackColor = frmMain.checkColor;
            btnTransApply.ImageKey = "check";
            btnTransApply.BackColor = frmMain.checkColor;
            btnGeneralApply.ImageKey = "check";
            btnGeneralApply.BackColor = frmMain.checkColor;
            btnBuiltVolApply.ImageKey = "check";
            btnBuiltVolApply.BackColor = frmMain.checkColor;
            btnBuiltUsingApply.ImageKey = "check";
            btnBuiltUsingApply.BackColor = frmMain.checkColor;
            btnTreatApply.ImageKey = "check";
            btnTreatApply.BackColor = frmMain.checkColor;
            btnDispRetApply.ImageKey = "check";
            btnDispRetApply.BackColor = frmMain.checkColor;
        }
        //-------------------------------------------------------
        private void trvDFSs_MouseDown(object sender, MouseEventArgs e)
        {
            trvDFSs.ContextMenuStrip = null;
        }
        //-------------------------------------------------------
        private void trvDFSs_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (access.mudVolumeManagement_DFSs == RoleAccess.AcessTypes.AT_None)
                return;

            if (e.Node == null || e.Node.Tag == null)
                return;

            if (access.mudVolumeManagement_DFSs == RoleAccess.AcessTypes.AT_ReadOnly)
            {
                ctxDFSMnu_ParentAdd.Enabled = false;
                ctxDFSMnu_ParentAdd.Visible = false;

                ctxDFSMnuRemove.Enabled = false;
                ctxDFSMnuRemove.Visible = false;

                toolStripSeparator3.Enabled = false;
                toolStripSeparator3.Visible = false;
            }

            trvDFSs.SelectedNode = e.Node;

            string tag = trvDFSs.SelectedNode.Tag.ToString();

            if (e.Button == MouseButtons.Right)
            {
                if (tag.StartsWith("parent"))
                    trvDFSs.ContextMenuStrip = ctxDFSMnu_Parent;
                else if (tag.StartsWith("dfs"))
                    trvDFSs.ContextMenuStrip = ctxDFSMnu;
            }
        }
        //-------------------------------------------------------
        private void ctxDFSMnu_ParentAdd_Click(object sender, EventArgs e)
        {
            //string METHOD = "ctxDFSMnu_ParentAdd_Click : " + CLASS_NAME;

            try
            {
                if (access.mudVolumeManagement_DFSs != RoleAccess.AcessTypes.AT_WriteAndRead)
                    return;

                if (frmMain.selectedRepID == -1)
                    return;

                if (trvDFSs.SelectedNode == null || trvDFSs.SelectedNode.Tag == null || trvDFSs.SelectedNode.Tag.ToString() != "parent")
                    return;

                string query =
                    " select 0, AutoID, MudType, DrillingFluidSystem from lt_DrillingFluidSystem  "
                    + " where AutoID not in "
                    + " ( select DFS_AutoID from rt_Rep2MudVolManDFS where  ReportID = " + frmMain.selectedRepID.ToString() + " ) ";

                DrillingFluidSystemFormDB_Selection frmDFSel = new DrillingFluidSystemFormDB_Selection(query);
                frmDFSel.Text = "Select Drilling Fluid System";

                frmDFSel.dgvFluidSys.Columns[0].Visible = false;//single selection

                if (frmDFSel.ShowDialog() != System.Windows.Forms.DialogResult.OK)//skip other cases
                    return;

                if (frmDFSel.dgvFluidSys.SelectedRows.Count == 0)
                    return;

                string drillFS = DGV_Operations.CellValueAsString(frmDFSel.dgvFluidSys.Rows[frmDFSel.dgvFluidSys.SelectedRows[0].Index].Cells[3]);
                string mudType = DGV_Operations.CellValueAsString(frmDFSel.dgvFluidSys.Rows[frmDFSel.dgvFluidSys.SelectedRows[0].Index].Cells[2]);
                string dfsAutoID = DGV_Operations.CellValueAsString(frmDFSel.dgvFluidSys.Rows[frmDFSel.dgvFluidSys.SelectedRows[0].Index].Cells[1]);

                Int64 generatedID = -1;

                {
                    List<ProcedureParam> prms = new List<ProcedureParam>();
                    prms.Add(new ProcedureParam("@Dfs_AutoID", dfsAutoID, ProcedureParam.ParamType.PT_Int, true, "1", false, "", "Invalid DFS"));
                    prms.Add(new ProcedureParam("@ReportID", frmMain.selectedRepID.ToString(), ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Report"));
                    prms.Add(new ProcedureParam("@ProjAutoID", frmMain.selectedPrjID.ToString(), ProcedureParam.ParamType.PT_Int, true, "1", false, "", "Invalid Project"));


                    string simpErr;
                    Errors critErr;

                    int ret = Procedures.CallProcedure("prc_rt_Rep2MudVolManDFS_insert", prms, out simpErr, out critErr, out generatedID);

                    if (ret != 0)
                    {
                        if (ret == 1)
                            InformationManager.Set_Info(simpErr);
                        else
                            InformationManager.Set_Info(critErr);

                        return;
                    }
                }

                TreeNode dfsNode = trvDFSs.Nodes[0].Nodes.Add(drillFS);
                dfsNode.Name = generatedID.ToString();
                dfsNode.Tag = "dfs";

                trvDFSs.SelectedNode = dfsNode;
            }
            catch (Exception ex)
            {
                InformationManager.Set_Info(ex.Message);
            }

        }
        //-------------------------------------------------------
        private void ctxDFSMnuRemove_Click(object sender, EventArgs e)
        {
            try
            {
                if (access.mudVolumeManagement_DFSs != RoleAccess.AcessTypes.AT_WriteAndRead)
                    return;

                if (frmMain.selectedRepID == -1)
                    return;

                if (trvDFSs.SelectedNode == null || trvDFSs.SelectedNode.Tag == null || trvDFSs.SelectedNode.Tag.ToString() != "dfs")
                    return;

                if (!UserOperations.PrompForUserPassword(mainForm.curUserRole.userID))
                    return;

                string dfs = trvDFSs.SelectedNode.Text;
                string idStr = trvDFSs.SelectedNode.Name;

                List<ProcedureParam> prms = new List<ProcedureParam>();
                prms.Add(new ProcedureParam("@ID", idStr, ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Record"));

                string simpErr;
                Errors critErr;
                Int64 resultStat = -1;

                int ret = Procedures.CallProcedure("prc_rt_Rep2MudVolManDFS_delete", prms, out simpErr, out critErr, out resultStat);

                if (ret != 0)
                {
                    if (ret == 1)
                        InformationManager.Set_Info(simpErr);
                    else
                        InformationManager.Set_Info(critErr);

                    return;
                }

                trvDFSs.SelectedNode.Remove();

                if (selectedMudVolDfsName == dfs)
                {
                    selectedMudVolDfsName = "";
                    selectedMudVolDfsID = -1;

                    OnSelectedDfsChanged();
                }

                trvDFSs.SelectedNode = null;
            }
            catch (Exception ex)
            {
                InformationManager.Set_Info(ex.Message);
            }
        }
        //-------------------------------------------------------
        private void ctxDFSMnuSetAsCurrent_Click(object sender, EventArgs e)
        {
            //string METHOD = "ctxDFSMnuSetAsCurrent_Click : " + CLASS_NAME;

            try
            {
                if (access.mudVolumeManagement_DFSs == RoleAccess.AcessTypes.AT_None)
                    return;

                if (frmMain.selectedRepID == -1)
                    return;

                if (trvDFSs.SelectedNode == null || trvDFSs.SelectedNode.Tag == null || trvDFSs.SelectedNode.Tag.ToString() != "dfs")
                    return;

                selectedMudVolDfsName = trvDFSs.SelectedNode.Text;
                selectedMudVolDfsID = Convert.ToInt64(trvDFSs.SelectedNode.Name);

                OnSelectedDfsChanged();
            }
            catch (Exception ex)
            {
                InformationManager.Set_Info(ex.Message);
            }
        }
        //-------------------------------------------------------
        private void btnDfsCheckDFSs_Click(object sender, EventArgs e)
        {
            try
            {
                if (access.mudVolumeManagement_DFSs == RoleAccess.AcessTypes.AT_None)
                    return;

                if (frmMain.selectedRepID == -1)
                    return;

                string query = " select dbo.fn_CheckMudVolDFSs_CoveringAllRequiredDFSs ( " + frmMain.selectedRepID.ToString() + " ) ";

                DataSet ds = new DataSet();
                if (ConnectionManager.ExecQuery(query, ref ds, 1))
                {
                    string result = ds.Tables[0].Rows[0][0].ToString();

                    ds.Dispose();

                    if (result != "")
                        InformationManager.Set_Info("Following DFSs used in project but not exist in this list" + Environment.NewLine + Environment.NewLine + result);
                    else
                        InformationManager.Set_Info("All DFSs used in project are in this list");
                }
            }
            catch (Exception)
            {
                InformationManager.Set_Info("Error checking");
            }
        }
        //-------------------------------------------------------
        string ComputeChemicalVolumes_RecTrans(Int64 RecTrans_ID, decimal RecTrans_Volume)
        {
            try
            {
                //~~~~~~~~~~~~~~
                ProjectForm prjForm = (ProjectForm)mainForm.subForms[mainForm.PorjectFormID];
                string selUnitVol = prjForm.lblUnitSelectedLiquidVolume.Tag.ToString();
                //~~~~~~~~~~~~~~
                string selUnitPowderMatConc = prjForm.lblUnitSelectedPowderMaterialConcentration.Tag.ToString();
                string selUnitLiquidMatConc = prjForm.lblUnitSelectedLiquidMaterialConcentration.Tag.ToString();
                //~~~~~~~~~~~~~~
                decimal VolFactor_SelUnit2BBL = (selUnitVol == "m³") ? 6.2898m : 1;
                decimal PowderConsFactor_SelUnit2LBL = (selUnitPowderMatConc == "kg/m³") ? 0.3505m : 1;

                decimal LiquidConcFactor_ToComputeUsed_SelUnit2GalPerBbl = (selUnitLiquidMatConc == "vol%") ? 2.380952381m : 1;
                //~~~~~~~~~~~~~~
                decimal result = 0;

                string query = " select dbo.fn_Get_RecTransPrd_ChemicalVolume ( " + RecTrans_ID + "," + RecTrans_Volume + "," + VolFactor_SelUnit2BBL + "," + PowderConsFactor_SelUnit2LBL + "," + LiquidConcFactor_ToComputeUsed_SelUnit2GalPerBbl + " ) ";

                DataSet ds = new DataSet();
                if (ConnectionManager.ExecQuery(query, ref ds, 1))
                {
                    if (ds.Tables[0].Rows[0][0] != DBNull.Value)
                    {
                        result = Convert.ToDecimal(ds.Tables[0].Rows[0][0]);
                    }

                    ds.Dispose();
                    return result.ToString();
                }
            }
            catch (Exception)
            {
            }

            return "Error";
        }
        //-------------------------------------------------------
        string ComputeChemicalVolumes(string tableName, string parentAutoIDName, Int64 parentAutoID)
        {
            try
            {
                decimal result = 0;

                string query =
                    " select sum	"
                    + " (	"
                    + " CASE 	"
                    + " WHEN (prd.UnitSize = '55 GAL/DRUM') THEN 1.3095 * (tbl.Used_PDF + tbl.Used_OtherCompany)	"
                    + " WHEN (prd.UnitSize = '5 GAL/CAN') THEN 0.119 * (tbl.Used_PDF + tbl.Used_OtherCompany)	"
                    + " WHEN (prd.UnitSize = '25 KG/SXS') THEN 0.1573 * ((tbl.Used_PDF + tbl.Used_OtherCompany) / prd.SpecificGravity)	"
                    + " WHEN (prd.UnitSize = '50 KG/SXS') THEN 0.31461 * ((tbl.Used_PDF + tbl.Used_OtherCompany) / prd.SpecificGravity)	"
                    + " WHEN (prd.UnitSize = '25 KG/PAIL') THEN 0.1573 * ((tbl.Used_PDF + tbl.Used_OtherCompany) / prd.SpecificGravity)	"
                    + " WHEN (prd.UnitSize = '12.5 KG/SXS') THEN 0.07865 * ((tbl.Used_PDF + tbl.Used_OtherCompany) / prd.SpecificGravity)	"
                    + " WHEN (prd.UnitSize = '1 MT/B.B') THEN 6.292 * ((tbl.Used_PDF + tbl.Used_OtherCompany) / prd.SpecificGravity)	"
                    + " WHEN (prd.UnitSize = '1.5 MT/B.B') THEN 9.438 * ((tbl.Used_PDF + tbl.Used_OtherCompany) / prd.SpecificGravity)	"
                    + " WHEN (prd.UnitSize = '1 MT/BULK') THEN 6.292 * ((tbl.Used_PDF + tbl.Used_OtherCompany) / prd.SpecificGravity)	"
                    + " ELSE 0 END 	"
                    + " ) "
                    + " from " + tableName + " tbl join lt_Product prd on tbl.Prd_AutoID = prd.AutoID  	"
                    + " where tbl." + parentAutoIDName + " = " + parentAutoID.ToString();

                DataSet ds = new DataSet();
                if (ConnectionManager.ExecQuery(query, ref ds, 1))
                {
                    if (ds.Tables[0].Rows[0][0] != DBNull.Value)
                    {
                        result = Convert.ToDecimal(ds.Tables[0].Rows[0][0]);
                    }

                    ds.Dispose();
                    return result.ToString();
                }
            }
            catch (Exception)
            {
            }

            return "Error";
        }
        //-------------------------------------------------------
        private void txtBuiltGeneral_TextChanged(object sender, EventArgs e)
        {
            if (frmMain.selectedRepID == -1 || selectedMudVolDfsName == "")
                return;

            if (access.mudVolumeManagement_DFSs != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;

            btnGeneralApply.ImageKey = "warning";
            btnGeneralApply.BackColor = frmMain.warningColor;
        }
        //-------------------------------------------------------
        private void txtDispRet_TextChanged(object sender, EventArgs e)
        {
            if (frmMain.selectedRepID == -1 || selectedMudVolDfsName == "")
                return;

            if (access.mudVolumeManagement_DFSs != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;

            btnDispRetApply.ImageKey = "warning";
            btnDispRetApply.BackColor = frmMain.warningColor;
        }
        //-------------------------------------------------------
        private void btnPitTankApply_Click(object sender, EventArgs e)
        {
            if (frmMain.selectedRepID == -1)
                return;

            if (access.mudVolumeManagement_Pit_TankVolume != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;

            List<Errors> errs = new List<Errors>();

            for (int i = 0; i < dgvPitTank.Rows.Count; i++)
            {
                List<ProcedureParam> prms = new List<ProcedureParam>();
                string idStr = DGV_Operations.CellValueAsString(dgvPitTank.Rows[i].Cells[0]);

                if (idStr == "")
                    prms.Add(new ProcedureParam("@ID", ProcedureParam.ParamType.PT_BigInt));//try add
                else
                    prms.Add(new ProcedureParam("@ID", idStr, ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Record"));//update
                //~~~~~~~~~~
                prms.Add(new ProcedureParam("@CurrentStatus", DGV_Operations.CellValueAsString(dgvPitTank.Rows[i].Cells[2]) == "Active" ? "A" : "R", 1));
                prms.Add(new ProcedureParam("@CurMudVol", DGV_Operations.CellValueAsString(dgvPitTank.Rows[i].Cells[3]), ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Current Mud Vol."));
                prms.Add(new ProcedureParam("@MW", DGV_Operations.CellValueAsString(dgvPitTank.Rows[i].Cells[6]), ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid MW"));
                //~~~~~~~~~~
                string dfs = DGV_Operations.CellValueAsString(dgvPitTank.Rows[i].Cells[5]);
                string dfsAutoID = DGV_Operations.CellValueAsString(dgvPitTank.Rows[i].Cells[4]);

                if (dfs == "")
                    prms.Add(new ProcedureParam("@CurDfs_AutoID", ProcedureParam.ParamType.PT_String));
                else
                    prms.Add(new ProcedureParam("@CurDfs_AutoID", dfsAutoID, ProcedureParam.ParamType.PT_Int, true, "1", false, "", "Invalid DFS"));
                //~~~~~~~~~~~~~~
                ProjectForm prjForm = (ProjectForm)mainForm.subForms[mainForm.PorjectFormID];
                string selUnitVol = prjForm.lblUnitSelectedLiquidVolume.Tag.ToString();
                string selUnitMudPitCap_Dead = prjForm.lblUnitSelectedMudPitsCapacityAndDeadVol.Tag.ToString();
                //~~~~~  
                decimal PitCap_VolFactor = 1;
                if (selUnitMudPitCap_Dead == "m³" && selUnitVol == "bbl") PitCap_VolFactor = 6.2898m;
                else if (selUnitMudPitCap_Dead == "bbl" && selUnitVol == "m³") PitCap_VolFactor = 0.1589873m;
                prms.Add(new ProcedureParam("@PitCap_VolFactor", PitCap_VolFactor.ToString(), ProcedureParam.ParamType.PT_Decimal, true, "0.001", false, "", "Invalid factor 1"));
                //~~~~~~~~~~~~~~
                //for adding
                prms.Add(new ProcedureParam("@ReportID", frmMain.selectedRepID.ToString(), ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Report"));
                prms.Add(new ProcedureParam("@ProjAutoID", frmMain.selectedPrjID.ToString(), ProcedureParam.ParamType.PT_Int, true, "1", false, "", "Invalid Project"));
                prms.Add(new ProcedureParam("@PitID", DGV_Operations.CellValueAsString(dgvPitTank.Rows[i].Cells[7]), ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Pit"));
                //~~~~~~~~~~
                string simpErr;
                Errors critErr;
                Int64 resultStat;

                int ret = Procedures.CallProcedure("prc_rt_Rep2MudVolMan_PitTankVol_add_or_update", prms, out simpErr, out critErr, out resultStat);

                Color normalBkColor = (i % 2 == 0) ? dgvPitTank.BackgroundColor : dgvPitTank.AlternatingRowsDefaultCellStyle.BackColor;

                dgvPitTank.Rows[i].Cells[3].Style.BackColor = normalBkColor;
                dgvPitTank.Rows[i].Cells[6].Style.BackColor = normalBkColor;

                if (ret != 0)
                {
                    if (ret == 1)
                    {
                        if (simpErr.StartsWith("Invalid Current Mud Vol."))
                            dgvPitTank.Rows[i].Cells[3].Style.BackColor = Color.Red;
                        else if (simpErr.StartsWith("Invalid MW"))
                            dgvPitTank.Rows[i].Cells[6].Style.BackColor = Color.Red;

                        critErr = new Errors(simpErr);
                    }

                    errs.Add(critErr);
                }
                else
                {
                    if (resultStat > 0)//new record is just added and updated
                        dgvPitTank.Rows[i].Cells[0].Value = resultStat;//to be sure
                }
            }

            InformationManager.Set_Info(errs.ToArray());

            if (errs.Count == 0)
            {
                btnPitTankApply.ImageKey = "Check";
                btnPitTankApply.BackColor = frmMain.checkColor;
            }
        }
        //-------------------------------------------------------
        private void btnPitTankRefresh_Click(object sender, EventArgs e)
        {
            //string METHOD = "btnPitTankRefresh_Click : " + CLASS_NAME;

            if (frmMain.selectedRepID == -1)
                return;

            if (access.mudVolumeManagement_Pit_TankVolume == RoleAccess.AcessTypes.AT_None)
                return;

            LoadPitTankVol();

            btnPitTankApply.ImageKey = "check";
            btnPitTankApply.BackColor = frmMain.checkColor;
        }
        //-------------------------------------------------------
        private void dgvPitTank_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex == -1)
                return;

            if (skipCellValueChanged_dgvPitTankPit)
                return;

            if (e.ColumnIndex >= 2 && e.ColumnIndex <= 6)
            {
                btnPitTankApply.ImageKey = "warning";
                btnPitTankApply.BackColor = frmMain.warningColor;
            }
        }
        //-------------------------------------------------------
        private void dgvPitTank_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //string METHOD = "dgvPitTank_CellMouseClick : " + CLASS_NAME;

            if (frmMain.selectedRepID == -1)
                return;

            if (access.mudVolumeManagement_Pit_TankVolume != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;

            if (e.ColumnIndex > 0 && e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 5)//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                {
                    string query =
                        " select 0, AutoID, MudType, DrillingFluidSystem from lt_DrillingFluidSystem  "
                        + " where AutoID in "
                        + " ( "
                        + "     select rt_Rep2MudVolManDFS.DFS_AutoID "
                        + "     from rt_Rep2MudVolManDFS join at_Report On rt_Rep2MudVolManDFS.ReportID = at_Report.ID "
                        + "     where at_Report.ID =  " + frmMain.selectedRepID.ToString()
                        + " ) ";

                    DrillingFluidSystemFormDB_Selection frmDFSel = new DrillingFluidSystemFormDB_Selection(query);
                    frmDFSel.Text = "Drilling Fluid Systems";

                    frmDFSel.dgvFluidSys.Columns[0].Visible = false;//single selection

                    System.Windows.Forms.DialogResult dgRes = frmDFSel.ShowDialog();

                    if (dgRes == System.Windows.Forms.DialogResult.Cancel)
                        return;

                    if (dgRes == System.Windows.Forms.DialogResult.Ignore)
                    {
                        dgvPitTank.Rows[e.RowIndex].Cells[4].Value = "";
                        dgvPitTank.Rows[e.RowIndex].Cells[5].Value = DBNull.Value;
                        return;
                    }

                    if (frmDFSel.dgvFluidSys.SelectedRows.Count == 0)
                        return;

                    string drillFS = DGV_Operations.CellValueAsString(frmDFSel.dgvFluidSys.Rows[frmDFSel.dgvFluidSys.SelectedRows[0].Index].Cells[3]);
                    string mudType = DGV_Operations.CellValueAsString(frmDFSel.dgvFluidSys.Rows[frmDFSel.dgvFluidSys.SelectedRows[0].Index].Cells[2]);
                    string dfsAutoID = DGV_Operations.CellValueAsString(frmDFSel.dgvFluidSys.Rows[frmDFSel.dgvFluidSys.SelectedRows[0].Index].Cells[1]);

                    dgvPitTank.Rows[e.RowIndex].Cells[4].Value = dfsAutoID;
                    dgvPitTank.Rows[e.RowIndex].Cells[5].Value = drillFS;
                }
            }
        }
        //-------------------------------------------------------
        private void btnPitTankCheckVolumes_Click(object sender, EventArgs e)
        {
            if (frmMain.selectedRepID == -1)
                return;

            if (access.mudVolumeManagement_Pit_TankVolume == RoleAccess.AcessTypes.AT_None)
                return;

            //~~~~~~~~~~~~~~
            ProjectForm prjForm = (ProjectForm)mainForm.subForms[mainForm.PorjectFormID];
            string selUnitVol = prjForm.lblUnitSelectedLiquidVolume.Tag.ToString();
            string selUnitDischarge = prjForm.lblUnitSelectedDischargeLossRate.Tag.ToString();
            //~~~~~~~~~~~~~~
            decimal actLossFactor = 1;
            if (selUnitDischarge == "bbl/hr")
            {
                if (selUnitVol == "m³")
                    actLossFactor = 0.1589876m;//m³ -> bbl                          
            }
            else// m³/hr
            {
                if (selUnitVol == "bbl")
                    actLossFactor = 6.2898m;//bbl -> m³                       
            }
            //~~~~~~~~~~
            List<ProcedureParam> prms = new List<ProcedureParam>();
            //~~~~~~~~~~
            prms.Add(new ProcedureParam("@ActLossFactor", actLossFactor.ToString(), ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Loss Factor"));
            prms.Add(new ProcedureParam("@curRepID", frmMain.selectedRepID.ToString(), ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Report"));
            //~~~~~~~~~~
            string simpErr;
            Errors critErr;
            Int64 resultStat;

            int ret = Procedures.CallProcedure("prc_rt_Rep2MudVolMan_PitTankVol_checkVolumes", prms, out simpErr, out critErr, out resultStat);

            if (ret != 0)
            {
                if (ret == 1)
                    InformationManager.Set_Info(simpErr);
                else
                    InformationManager.Set_Info(critErr);

                return;
            }
            else
            {
                InformationManager.Set_Info("Volumes are OK (upto 1 precision digit)");
            }
        }
        //-------------------------------------------------------
        private void dgvPitTankCMT_Spacer_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex == -1)
                return;

            if (skipCellValueChanged_dgvPitTankCMT)
                return;

            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                btnCMTApply.ImageKey = "warning";
                btnCMTApply.BackColor = frmMain.warningColor;
            }
        }
        //-------------------------------------------------------
        private void btnTotalVolManRefresh_Click(object sender, EventArgs e)
        {
            if (frmMain.selectedRepID == -1)
                return;

            if (access.mudVolumeManagement_TotalVolManagement == RoleAccess.AcessTypes.AT_None)
                return;

            btnTotalWellVolRefresh_Click(null, null);//update before merges
            UpdateTotalVolumeManagement();

            try
            {
                decimal mudVolInHole = Convert.ToDecimal(DGV_Operations.SummationOnColumn(dgvTotalWellVol, 3));//Sum of before merge
                decimal totalWellVol = Math.Round(Convert.ToDecimal(txtTVM_HVM_TotalVol.Tag.ToString()), 1);

                decimal diff = totalWellVol - mudVolInHole;
                if (Math.Abs(diff) > 0.100m)
                {
                    string err = "Total Well Volume - Mud Volume in Hole = " + diff.ToString()
                                + Environment.NewLine
                                + Environment.NewLine
                                + "The difference could be due to the Presence of Cement in the Well or miscalculation, check it";

                    InformationManager.Set_Info(err);
                }
            }
            catch (Exception)
            {
                InformationManager.Set_Info("Can not check difference of Total Well Volume and Mud Volume in Hole");
            }
        }
        //-------------------------------------------------------
        public void UpdateTotalVolumeManagement()
        {
            try
            {
                if (frmMain.selectedRepID == -1)
                    return;

                if (access.mudVolumeManagement_TotalVolManagement == RoleAccess.AcessTypes.AT_None)
                    return;
                //~~~~~~~~~~~~~~
                Helpers.Computation.TotalVolumeManagement tvmComp = new Helpers.Computation.TotalVolumeManagement();
                //~~~~~~~~~~~~~~
                ProjectForm prjForm = (ProjectForm)mainForm.subForms[mainForm.PorjectFormID];
                tvmComp.in_selUnitVol = prjForm.lblUnitSelectedLiquidVolume.Tag.ToString();
                tvmComp.in_selUnitDepth = prjForm.lblUnitSelectedDepth.Tag.ToString();
                tvmComp.in_selUnitDischarge = prjForm.lblUnitSelectedDischargeLossRate.Tag.ToString();
                tvmComp.in_selUnitFlowRate = prjForm.lblUnitSelectedFlowRate.Tag.ToString();
                tvmComp.in_selUnitVolPerStroke = prjForm.lblUnitSelectedVolumeStroke.Tag.ToString();
                //~~~~~~~~~~~~~~
                txtTVM_SVM_PitVol.Tag = null; txtTVM_SVM_PitVol.Text = "";
                txtTVM_BVM_EndVol.Tag = null; txtTVM_BVM_EndVol.Text = "";
                txtTVM_SVM_SurVol.Tag = null; txtTVM_SVM_SurVol.Text = "";
                txtTVM_SVM_TreVol.Tag = null; txtTVM_SVM_TreVol.Text = "";
                txtTVM_HVM_StringVol.Tag = null; txtTVM_HVM_StringVol.Text = "";
                txtTVM_HVM_AnnVol.Tag = null; txtTVM_HVM_AnnVol.Text = "";
                txtTVM_HVM_BelVol.Tag = null; txtTVM_HVM_BelVol.Text = "";
                txtTVM_HVM_TotalVol.Tag = null; txtTVM_HVM_TotalVol.Text = "";
                txtTVM_HVM_DrillVol.Tag = null; txtTVM_HVM_DrillVol.Text = "";
                txtTVM_BVM_RecVol.Tag = null; txtTVM_BVM_RecVol.Text = "";
                txtTVM_BVM_TransVol.Tag = null; txtTVM_BVM_TransVol.Text = "";
                txtTVM_BVM_Gain.Tag = null; txtTVM_BVM_Gain.Text = "";
                txtTVM_BVM_AddVol.Tag = null; txtTVM_BVM_AddVol.Text = "";
                txtTVM_BVM_BuildVol.Tag = null; txtTVM_BVM_BuildVol.Text = "";
                txtTVM_BVM_StartVol.Tag = null; txtTVM_BVM_StartVol.Text = "";
                txtTVM_BVM_RetVol.Tag = null; txtTVM_BVM_RetVol.Text = "";
                txtTVM_BVM_DispVol.Tag = null; txtTVM_BVM_DispVol.Text = "";
                txtTVM_BVM_LossVol.Tag = null; txtTVM_BVM_LossVol.Text = "";
                txtTVM_CD_BblToBit.Tag = null; txtTVM_CD_BblToBit.Text = "";
                txtTVM_CD_BblBottomUp.Tag = null; txtTVM_CD_BblBottomUp.Text = "";
                txtTVM_CD_BblTotalCir.Tag = null; txtTVM_CD_BblTotalCir.Text = "";
                txtTVM_CD_MinsToBit.Tag = null; txtTVM_CD_MinsToBit.Text = "";
                txtTVM_CD_MinsBottomUp.Tag = null; txtTVM_CD_MinsBottomUp.Text = "";
                txtTVM_CD_MinsTotalCir.Tag = null; txtTVM_CD_MinsTotalCir.Text = "";
                //~~~~~~~~~~~~~~
                //do not return on error => show any computed values yet
                tvmComp.Compute(frmMain.selectedRepID, frmMain.selected_RW_WellID);
                //~~~~~~~~~~~~~~
                try { txtTVM_SVM_PitVol.Tag = tvmComp.out_svm_PitVol; txtTVM_SVM_PitVol.Text = Convert.ToDecimal(tvmComp.out_svm_PitVol).ToString("0.###"); } catch (Exception) { }
                try { txtTVM_BVM_EndVol.Tag = tvmComp.out_bvm_EndVol; txtTVM_BVM_EndVol.Text = Convert.ToDecimal(tvmComp.out_bvm_EndVol).ToString("0.###"); } catch (Exception) { }
                try { txtTVM_SVM_SurVol.Tag = tvmComp.out_svm_SurVol; txtTVM_SVM_SurVol.Text = Convert.ToDecimal(tvmComp.out_svm_SurVol).ToString("0.###"); } catch (Exception) { }
                try { txtTVM_SVM_TreVol.Tag = tvmComp.out_svm_TreVol; txtTVM_SVM_TreVol.Text = Convert.ToDecimal(tvmComp.out_svm_TreVol).ToString("0.###"); } catch (Exception) { }
                try { txtTVM_HVM_StringVol.Tag = tvmComp.out_hvm_StringVol; txtTVM_HVM_StringVol.Text = Convert.ToDecimal(tvmComp.out_hvm_StringVol).ToString("0.###"); } catch (Exception) { }
                try { txtTVM_HVM_AnnVol.Tag = tvmComp.out_hvm_AnnVol; txtTVM_HVM_AnnVol.Text = Convert.ToDecimal(tvmComp.out_hvm_AnnVol).ToString("0.###"); } catch (Exception) { }
                try { txtTVM_HVM_BelVol.Tag = tvmComp.out_hvm_BelVol; txtTVM_HVM_BelVol.Text = Convert.ToDecimal(tvmComp.out_hvm_BelVol).ToString("0.###"); } catch (Exception) { }
                try { txtTVM_HVM_TotalVol.Tag = tvmComp.out_hvm_TotalVol; txtTVM_HVM_TotalVol.Text = Convert.ToDecimal(tvmComp.out_hvm_TotalVol).ToString("0.###"); } catch (Exception) { }
                try { txtTVM_HVM_DrillVol.Tag = tvmComp.out_hvm_DrillVol; txtTVM_HVM_DrillVol.Text = Convert.ToDecimal(tvmComp.out_hvm_DrillVol).ToString("0.###"); } catch (Exception) { }
                try { txtTVM_BVM_RecVol.Tag = tvmComp.out_bvm_RecVol; txtTVM_BVM_RecVol.Text = Convert.ToDecimal(tvmComp.out_bvm_RecVol).ToString("0.###"); } catch (Exception) { }
                try { txtTVM_BVM_TransVol.Tag = tvmComp.out_bvm_TransVol; txtTVM_BVM_TransVol.Text = Convert.ToDecimal(tvmComp.out_bvm_TransVol).ToString("0.###"); } catch (Exception) { }
                try { txtTVM_BVM_Gain.Tag = tvmComp.out_bvm_Gain; txtTVM_BVM_Gain.Text = Convert.ToDecimal(tvmComp.out_bvm_Gain).ToString("0.###"); } catch (Exception) { }
                try { txtTVM_BVM_AddVol.Tag = tvmComp.out_bvm_AddVol; txtTVM_BVM_AddVol.Text = Convert.ToDecimal(tvmComp.out_bvm_AddVol).ToString("0.###"); } catch (Exception) { }
                try { txtTVM_BVM_BuildVol.Tag = tvmComp.out_bvm_BuildVol; txtTVM_BVM_BuildVol.Text = Convert.ToDecimal(tvmComp.out_bvm_BuildVol).ToString("0.###"); } catch (Exception) { }
                try { txtTVM_BVM_StartVol.Tag = tvmComp.out_bvm_StartVol; txtTVM_BVM_StartVol.Text = Convert.ToDecimal(tvmComp.out_bvm_StartVol).ToString("0.###"); } catch (Exception) { }
                try { txtTVM_BVM_RetVol.Tag = tvmComp.out_bvm_RetVol; txtTVM_BVM_RetVol.Text = Convert.ToDecimal(tvmComp.out_bvm_RetVol).ToString("0.###"); } catch (Exception) { }
                try { txtTVM_BVM_DispVol.Tag = tvmComp.out_bvm_DispVol; txtTVM_BVM_DispVol.Text = Convert.ToDecimal(tvmComp.out_bvm_DispVol).ToString("0.###"); } catch (Exception) { }
                try { txtTVM_BVM_LossVol.Tag = tvmComp.out_bvm_LossVol; txtTVM_BVM_LossVol.Text = Convert.ToDecimal(tvmComp.out_bvm_LossVol).ToString("0.###"); } catch (Exception) { }
                try { txtTVM_CD_BblToBit.Tag = tvmComp.out_cd_BblToBit; txtTVM_CD_BblToBit.Text = Convert.ToDecimal(tvmComp.out_cd_BblToBit).ToString("0.###"); } catch (Exception) { }
                try { txtTVM_CD_BblBottomUp.Tag = tvmComp.out_cd_BblBottomUp; txtTVM_CD_BblBottomUp.Text = Convert.ToDecimal(tvmComp.out_cd_BblBottomUp).ToString("0.###"); } catch (Exception) { }
                try { txtTVM_CD_BblTotalCir.Tag = tvmComp.out_cd_BblTotalCir; txtTVM_CD_BblTotalCir.Text = Convert.ToDecimal(tvmComp.out_cd_BblTotalCir).ToString("0.###"); } catch (Exception) { }
                try { txtTVM_CD_MinsToBit.Tag = tvmComp.out_cd_MinsToBit; txtTVM_CD_MinsToBit.Text = Convert.ToDecimal(tvmComp.out_cd_MinsToBit).ToString("0.###"); } catch (Exception) { }
                try { txtTVM_CD_MinsBottomUp.Tag = tvmComp.out_cd_MinsBottomUp; txtTVM_CD_MinsBottomUp.Text = Convert.ToDecimal(tvmComp.out_cd_MinsBottomUp).ToString("0.###"); } catch (Exception) { }
                try { txtTVM_CD_MinsTotalCir.Tag = tvmComp.out_cd_MinsTotalCir; txtTVM_CD_MinsTotalCir.Text = Convert.ToDecimal(tvmComp.out_cd_MinsTotalCir).ToString("0.###"); } catch (Exception) { }
            }
            catch (Exception)
            {
            }
        }
        //-------------------------------------------------------
        private void btnCMTRefresh_Click(object sender, EventArgs e)
        {
            //string METHOD = "btnCMTRefresh_Click : " + CLASS_NAME;

            if (frmMain.selectedRepID == -1)
                return;

            if (access.mudVolumeManagement_Pit_TankVolume == RoleAccess.AcessTypes.AT_None)
                return;

            LoadCMT_Spacer();

            btnCMTApply.ImageKey = "check";
            btnCMTApply.BackColor = frmMain.checkColor;
        }
        //-------------------------------------------------------
        private void btnCMTApply_Click(object sender, EventArgs e)
        {
            if (frmMain.selectedRepID == -1)
                return;

            if (access.mudVolumeManagement_Pit_TankVolume != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;

            List<ProcedureParam> prms = new List<ProcedureParam>();
            prms.Add(new ProcedureParam("@ReportID", frmMain.selectedRepID.ToString(), ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Report"));
            prms.Add(new ProcedureParam("@SpacerVol_MVMPitVol", DGV_Operations.CellValueAsString(dgvPitTankCMT_Spacer[0, 0]), ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Spacer Vol."));
            prms.Add(new ProcedureParam("@SpacerWeight_MVMPitVol", DGV_Operations.CellValueAsString(dgvPitTankCMT_Spacer[1, 0]), ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Spacer Weight"));
            prms.Add(new ProcedureParam("@CementSlurryLeadVol_MVMPitVol", DGV_Operations.CellValueAsString(dgvPitTankCMT_Spacer[0, 1]), ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Cement Slurry Lead Vol."));
            prms.Add(new ProcedureParam("@CementSlurryLeadWeight_MVMPitVol", DGV_Operations.CellValueAsString(dgvPitTankCMT_Spacer[1, 1]), ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Cement Slurry Lead Weight"));
            prms.Add(new ProcedureParam("@CementSlurryTailVol_MVMPitVol", DGV_Operations.CellValueAsString(dgvPitTankCMT_Spacer[0, 2]), ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Cement Slurry Tail Vol."));
            prms.Add(new ProcedureParam("@CementSlurryTailWeight_MVMPitVol", DGV_Operations.CellValueAsString(dgvPitTankCMT_Spacer[1, 2]), ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Cement Slurry Tail Weight"));

            string simpErr;
            Errors critErr;
            Int64 resultStat;

            int ret = Procedures.CallProcedure("prc_at_Report_update_CMT_And_Spacer", prms, out simpErr, out critErr, out resultStat);

            if (ret != 0)
            {
                if (ret == 1)
                    InformationManager.Set_Info(simpErr);
                else
                    InformationManager.Set_Info(critErr);

                return;
            }

            btnCMTApply.ImageKey = "Check";
            btnCMTApply.BackColor = frmMain.checkColor;
        }
        //-------------------------------------------------------
        private void dgvPitTankCMT_Spacer_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //string METHOD = "dgvPitTankCMT_Spacer_CellMouseClick : " + CLASS_NAME;

            if (frmMain.selectedRepID == -1)
                return;

            if (access.mudVolumeManagement_Pit_TankVolume != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;

            if (e.ColumnIndex > 0 && e.RowIndex >= 0 && e.RowIndex <= 2)
            {
                if (e.ColumnIndex == 2)//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                {
                    MudVolManCmtSpacerPrdForm frmUsed = new MudVolManCmtSpacerPrdForm(frmMain.selectedRepID, e.RowIndex + 1);

                    frmUsed.ShowDialog();
                    dgvPitTankCMT_Spacer[2, e.RowIndex].Value = CmtSpacerProducts(e.RowIndex + 1);
                }
            }
        }
        //-------------------------------------------------------
        private void btnRecAdd_Click(object sender, EventArgs e)
        {
            //string METHOD = "btnRecAdd_Click : " + CLASS_NAME;

            if (frmMain.selectedRepID == -1 || selectedMudVolDfsName == "")
                return;

            if (access.mudVolumeManagement_DFSs != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;

            List<ProcedureParam> prms = new List<ProcedureParam>();
            prms.Add(new ProcedureParam("@MudVolManDFS_ID", selectedMudVolDfsID.ToString(), ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Record"));
            prms.Add(new ProcedureParam("@RecTrans_Flag", "0", ProcedureParam.ParamType.PT_Bit, false, "", false, "", "Invalid Flag"));
            prms.Add(new ProcedureParam("@ProjAutoID", frmMain.selectedPrjID.ToString(), ProcedureParam.ParamType.PT_Int, true, "1", false, "", "Invalid Project"));

            string simpErr;
            Errors critErr;
            Int64 resultStat;

            int ret = Procedures.CallProcedure("prc_rt_Rep2MudVolManDFS_RecTrans_insert", prms, out simpErr, out critErr, out resultStat);

            if (ret != 0)
            {
                if (ret == 1)
                    InformationManager.Set_Info(simpErr);
                else
                    InformationManager.Set_Info(critErr);

                return;
            }

            LoadRec();

            btnRecApply.ImageKey = "Check";
            btnRecApply.BackColor = frmMain.checkColor;
        }
        //-------------------------------------------------------
        private void btnRecRemove_Click(object sender, EventArgs e)
        {
            //string METHOD = "btnRecRemove_Click : " + CLASS_NAME;

            if (frmMain.selectedRepID == -1 || selectedMudVolDfsName == "")
                return;

            if (access.mudVolumeManagement_DFSs != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;

            if (dgvRec.SelectedRows.Count == 0)
            {
                InformationManager.Set_Info("Select Row to Delete");
                return;
            }

            string idStr = DGV_Operations.CellValueAsString(dgvRec.Rows[dgvRec.SelectedRows[0].Index].Cells[0]);

            List<ProcedureParam> prms = new List<ProcedureParam>();
            prms.Add(new ProcedureParam("@ID", idStr, ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Record"));

            string simpErr;
            Errors critErr;
            Int64 resultStat;

            int ret = Procedures.CallProcedure("prc_rt_Rep2MudVolManDFS_RecTrans_delete", prms, out simpErr, out critErr, out resultStat);

            if (ret != 0)
            {
                if (ret == 1)
                    InformationManager.Set_Info(simpErr);
                else
                    InformationManager.Set_Info(critErr);

                return;
            }

            LoadRec();

            btnRecApply.ImageKey = "Check";
            btnRecApply.BackColor = frmMain.checkColor;
        }
        //-------------------------------------------------------
        private void btnRecAutoCompute_Click(object sender, EventArgs e)
        {
            //string METHOD = "btnRecRemove_Click : " + CLASS_NAME;

            if (frmMain.selectedRepID == -1 || selectedMudVolDfsName == "")
                return;

            if (access.mudVolumeManagement_DFSs != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;

            btnRecApply_Click(null, null);//do apply
            if (btnRecApply.ImageKey != "Check")//on error
                return;

            if (dgvRec.SelectedRows.Count == 0)
            {
                InformationManager.Set_Info("Select Row");
                return;
            }

            string locatioIdStr = DGV_Operations.CellValueAsString(dgvRec.Rows[dgvRec.SelectedRows[0].Index].Cells[3]);
            string dstRecIdStr = DGV_Operations.CellValueAsString(dgvRec.Rows[dgvRec.SelectedRows[0].Index].Cells[0]);

            if (locatioIdStr == "")
            {
                InformationManager.Set_Info("Select location");
                return;
            }

            int srcPrjAutoID = -1;
            Int64 srcRigID = -1,
                    srcWellID = -1;
            int srcHoleID = -1;


            if (!FetchTableData.GetPropertiesOfLocation(locatioIdStr, out srcPrjAutoID, out srcRigID, out srcWellID, out srcHoleID))
            {
                InformationManager.Set_Info("Can not find project");
                return;
            }

            Dictionary<string/*prop*/, string/*unit*/> selUnits = FetchTableData.GetAllSelectedUnits(srcPrjAutoID);

            if (selUnits == null ||
                !selUnits.ContainsKey("Powder Material Concentration") ||
                !selUnits.ContainsKey("Liquid Material Concentration"))
            {
                InformationManager.Set_Info("Can not load project units");
                return;
            }

            string src_powderMatConcUnit;
            string src_liquidMatConcUnit;
            selUnits.TryGetValue("Powder Material Concentration", out src_powderMatConcUnit);
            selUnits.TryGetValue("Liquid Material Concentration", out src_liquidMatConcUnit);

            //~~~~~~~~~~~~~~
            ProjectForm prjForm = (ProjectForm)mainForm.subForms[mainForm.PorjectFormID];
            string selUnitVol_Dest = prjForm.lblUnitSelectedLiquidVolume.Tag.ToString();
            //~~~~~~~~~~~~~~
            string selUnitPowderMatConc = prjForm.lblUnitSelectedPowderMaterialConcentration.Tag.ToString();
            string selUnitLiquidMatConc = prjForm.lblUnitSelectedLiquidMaterialConcentration.Tag.ToString();
            //~~~~~~~~~~~~~~
            decimal pMC_Factor = Convert.ToDecimal(UnitConverter.Convert("Powder Material Concentration", src_powderMatConcUnit, selUnitPowderMatConc, "1"));
            decimal lMC_Factor = Convert.ToDecimal(UnitConverter.Convert("Liquid Material Concentration", src_liquidMatConcUnit, selUnitLiquidMatConc, "1"));

            string curHoleIdStr = "";

            if (!FetchTableData.GetHoleID(frmMain.selectedRepID, out curHoleIdStr))
            {
                InformationManager.Set_Info("Invalid Hole Size");
                return;
            }

            //----------------
            string query =
            " select tr.ID, "
            + " 	ltrim(rtrim(str(rep.Num))) + \' : \' + ltrim(rtrim(str(rep.Rev))), "
            + " 	\'Trans. Seq. = \' + ltrim(rtrim(str(tr.Seq))) + \' , Vol. = \' + ltrim(rtrim(str(tr.Vol))) "
            + " from  "
            + " at_Rig rg join rt_Rig2Well rw on rg.ID = rw.RigID "
            + " join at_Report rep on rw.ID = rep.RigWellID "
            + " join rt_Rep2MudVolManDFS mdfs on rep.ID = mdfs.ReportID "
            + " join rt_Rep2MudVolManDFS_RecTrans tr on mdfs.ID = tr.MudVolManDFS_ID "
            + " join at_HolesOfProjects hp on hp.ID = tr.RecTransLocation_ID "
            + " join lt_DrillingFluidSystem ldfs on ldfs.AutoID = mdfs.Dfs_AutoID "
            + " where "
            + " tr.RecTrans_Flag = 1 "
            + " and ldfs.DrillingFluidSystem = \'" + selectedMudVolDfsName.Replace("\'", "\'\'") + "\' "
            + " and hp.PrjAutoID = " + frmMain.selectedPrjID + " and hp.RigID = " + frmMain.selected_RW_RigID + " and hp.WellID = " + frmMain.selected_RW_WellID + " and hp.HoleSize_AutoID = " + curHoleIdStr
            + " and rg.PrjAutoID = " + srcPrjAutoID + " and rg.ID = " + srcRigID + " and rw.WellID = " + srcWellID + " and rep.HoleSize_AutoID = " + srcHoleID;
            //----------------
            MudVolManSelToRecForm selDlg = new MudVolManSelToRecForm(query);

            if (selDlg.ShowDialog() != System.Windows.Forms.DialogResult.OK || selDlg.dgvTransRecord.SelectedRows.Count == 0)
                return;

            string srcTransIdStr = DGV_Operations.CellValueAsString(selDlg.dgvTransRecord.Rows[selDlg.dgvTransRecord.SelectedRows[0].Index].Cells[0]);
            //----------------

            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            Dictionary<string, string> allSelectedUnitsInSrcProject = FetchTableData.GetAllSelectedUnits(srcPrjAutoID);
            string selUnitVol_Src = "";
            if (!allSelectedUnitsInSrcProject.TryGetValue("Liquid Volume", out selUnitVol_Src))
            {
                InformationManager.Set_Info("Error fetching selected units");
                return;
            }
            //******************************************************
            decimal Src2DestVolUnitFactor = 1;

            if (selUnitVol_Src == "bbl" && selUnitVol_Dest == "m³")
                Src2DestVolUnitFactor = 0.1589873m;
            else if (selUnitVol_Src == "m³" && selUnitVol_Dest == "bbl")
                Src2DestVolUnitFactor = 6.2898m;
            //******************************************************

            //----------------
            List<ProcedureParam> prms = new List<ProcedureParam>();
            prms.Add(new ProcedureParam("@RecTrans_ID_Src", srcTransIdStr, ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Source Record"));
            prms.Add(new ProcedureParam("@RecTrans_ID_Dest", dstRecIdStr, ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Destination Record"));
            prms.Add(new ProcedureParam("@Src2DestVolUnitFactor", Src2DestVolUnitFactor.ToString(), ProcedureParam.ParamType.PT_Decimal, true, "0.001", false, "", "Invalid Src. to Dest. Vol. Unit Factor"));
            prms.Add(new ProcedureParam("@PowderConsFactor", pMC_Factor.ToString(), ProcedureParam.ParamType.PT_Decimal, true, "0.001", false, "", "Invalid Powder Concentration Factor"));
            prms.Add(new ProcedureParam("@LiquidConsFactor", lMC_Factor.ToString(), ProcedureParam.ParamType.PT_Decimal, true, "0.001", false, "", "Invalid Liquid Concentration Factor"));
            prms.Add(new ProcedureParam("@ProjAutoID", frmMain.selectedPrjID.ToString(), ProcedureParam.ParamType.PT_Int, true, "1", false, "", "Invalid Project"));


            string simpErr;
            Errors critErr;
            Int64 resultStat;

            int ret = Procedures.CallProcedure("prc_rt_Rep2MudVolManDFS_RecTrans_Prd_AutoAddRec", prms, out simpErr, out critErr, out resultStat);

            if (ret != 0)
            {
                if (ret == 1)
                    InformationManager.Set_Info(simpErr);
                else
                    InformationManager.Set_Info(critErr);

                return;
            }

            ComputeRecVolValues(dgvTrans.SelectedRows[0].Index);
            InformationManager.Set_Info("Completed Successfully", "Status:", "Information");
        }
        //-------------------------------------------------------
        private void btnRecApply_Click(object sender, EventArgs e)
        {
            //string METHOD = "btnRecApply_Click : " + CLASS_NAME;

            if (frmMain.selectedRepID == -1 || selectedMudVolDfsName == "")
                return;

            if (access.mudVolumeManagement_DFSs != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;

            List<Errors> errs = new List<Errors>();

            for (int i = 0; i < dgvRec.Rows.Count; i++)
            {

                List<ProcedureParam> prms = new List<ProcedureParam>();
                prms.Add(new ProcedureParam("@ID", DGV_Operations.CellValueAsString(dgvRec.Rows[i].Cells[0]), ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Record"));
                prms.Add(new ProcedureParam("@Seq", DGV_Operations.CellValueAsString(dgvRec.Rows[i].Cells[1]), ProcedureParam.ParamType.PT_Int, true, "1", true, "2000", "Invalid Seq."));
                prms.Add(new ProcedureParam("@Vol", DGV_Operations.CellValueAsString(dgvRec.Rows[i].Cells[4]), ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Vol."));
                prms.Add(new ProcedureParam("@MW", DGV_Operations.CellValueAsString(dgvRec.Rows[i].Cells[6]), ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid MW"));

                string locID = DGV_Operations.CellValueAsString(dgvRec.Rows[i].Cells[3]);
                if (locID == "")
                {
                    prms.Add(new ProcedureParam("@RecTransLocation_ID", ProcedureParam.ParamType.PT_BigInt));
                    prms.Add(new ProcedureParam("@OtherRecTransLocation", DGV_Operations.CellValueAsString(dgvRec.Rows[i].Cells[2]), 120));
                }
                else
                {
                    prms.Add(new ProcedureParam("@RecTransLocation_ID", locID, ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Location"));
                    prms.Add(new ProcedureParam("@OtherRecTransLocation", "", 120));
                }

                string simpErr;
                Errors critErr;
                Int64 resultStat;

                int ret = Procedures.CallProcedure("prc_rt_Rep2MudVolManDFS_RecTrans_update", prms, out simpErr, out critErr, out resultStat);

                Color normalBkColor = (i % 2 == 0) ? dgvRec.BackgroundColor : dgvRec.AlternatingRowsDefaultCellStyle.BackColor;
                dgvRec.Rows[i].Cells[1].Style.BackColor = normalBkColor;
                dgvRec.Rows[i].Cells[4].Style.BackColor = normalBkColor;
                dgvRec.Rows[i].Cells[6].Style.BackColor = normalBkColor;


                if (ret != 0)
                {
                    if (ret == 1)
                    {
                        if (simpErr.StartsWith("Invalid Seq."))
                            dgvRec.Rows[i].Cells[1].Style.BackColor = Color.Red;
                        else if (simpErr.StartsWith("Invalid Vol."))
                            dgvRec.Rows[i].Cells[4].Style.BackColor = Color.Red;
                        else if (simpErr.StartsWith("Invalid MW"))
                            dgvRec.Rows[i].Cells[6].Style.BackColor = Color.Red;
                        critErr = new Errors(simpErr);
                    }

                    errs.Add(critErr);
                }
            }

            InformationManager.Set_Info(errs.ToArray());

            if (errs.Count == 0)
            {
                btnRecApply.ImageKey = "Check";
                btnRecApply.BackColor = frmMain.checkColor;
            }
        }
        //-------------------------------------------------------
        private void btnRecRefresh_Click(object sender, EventArgs e)
        {
            //string METHOD = "btnRecRefresh_Click : " + CLASS_NAME;

            if (frmMain.selectedRepID == -1 || selectedMudVolDfsName == "")
                return;

            if (access.mudVolumeManagement_DFSs == RoleAccess.AcessTypes.AT_None)
                return;

            LoadRec();

            btnRecApply.ImageKey = "Check";
            btnRecApply.BackColor = frmMain.checkColor;
        }
        //-------------------------------------------------------
        private void dgvRec_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex == -1)
                return;

            if (skipCellValueChanged_dgvRec)
                return;

            if (e.ColumnIndex >= 1 && e.ColumnIndex <= 6)
            {
                btnRecApply.ImageKey = "warning";
                btnRecApply.BackColor = frmMain.warningColor;
            }

            if (e.ColumnIndex == 4)
            {
                ComputeRecVolValues(e.RowIndex);
                txtRecTotalVol.Text = DGV_Operations.SummationOnColumn(dgvRec, 4);
            }
        }
        //-------------------------------------------------------
        private void dgvRec_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //string METHOD = "dgvRec_CellMouseClick : " + CLASS_NAME;

            if (frmMain.selectedRepID == -1 || selectedMudVolDfsName == "")
                return;

            if (access.mudVolumeManagement_DFSs != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;

            if (e.ColumnIndex > 0 && e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 2)//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                {
                    if (e.Button == System.Windows.Forms.MouseButtons.Left)
                    {
                        string otherLocation = (DGV_Operations.CellValueAsString(dgvRec.Rows[e.RowIndex].Cells[3]) == "")
                                                ? DGV_Operations.CellValueAsString(dgvRec.Rows[e.RowIndex].Cells[2]) : "";

                        HolesOfProjects_Selection prwhSel = new HolesOfProjects_Selection(otherLocation);

                        if (prwhSel.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                            return;

                        if (prwhSel.chbOtherLocation.Checked || prwhSel.dgvPRWH.SelectedRows.Count == 0)
                        {
                            dgvRec.Rows[e.RowIndex].Cells[2].Value = prwhSel.txtOtherLocation.Text;
                            dgvRec.Rows[e.RowIndex].Cells[3].Value = null;
                        }
                        else
                        {
                            string prjName = DGV_Operations.CellValueAsString(prwhSel.dgvPRWH.Rows[prwhSel.dgvPRWH.SelectedRows[0].Index].Cells[1]);
                            string rigName = DGV_Operations.CellValueAsString(prwhSel.dgvPRWH.Rows[prwhSel.dgvPRWH.SelectedRows[0].Index].Cells[2]);
                            string wellName = DGV_Operations.CellValueAsString(prwhSel.dgvPRWH.Rows[prwhSel.dgvPRWH.SelectedRows[0].Index].Cells[3]);
                            string holeLabel = DGV_Operations.CellValueAsString(prwhSel.dgvPRWH.Rows[prwhSel.dgvPRWH.SelectedRows[0].Index].Cells[4]);

                            dgvRec.Rows[e.RowIndex].Cells[2].Value = prjName + "/" + rigName + "/" + wellName + "/" + holeLabel;
                            dgvRec.Rows[e.RowIndex].Cells[3].Value = prwhSel.dgvPRWH.Rows[prwhSel.dgvPRWH.SelectedRows[0].Index].Cells[0].Value;
                        }

                        btnRecApply.ImageKey = "warning";
                        btnRecApply.BackColor = frmMain.warningColor;
                    }
                    else if (e.Button == System.Windows.Forms.MouseButtons.Middle)
                    {
                        dgvRec.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = frmMain.selectedPrjName + "/" + frmMain.selected_RW_RigName + "/" + frmMain.selected_RW_WellName;

                        btnRecApply.ImageKey = "warning";
                        btnRecApply.BackColor = frmMain.warningColor;
                    }
                }
                else if (e.ColumnIndex == 5)//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                {
                    //~~~~~~~~~~~~~~
                    ProjectForm prjForm = (ProjectForm)mainForm.subForms[mainForm.PorjectFormID];
                    string selUnitVol = prjForm.lblUnitSelectedLiquidVolume.Tag.ToString();
                    //~~~~~~~~~~~~~~
                    string selUnitPowderMatConc = prjForm.lblUnitSelectedPowderMaterialConcentration.Tag.ToString();
                    string selUnitLiquidMatConc = prjForm.lblUnitSelectedLiquidMaterialConcentration.Tag.ToString();

                    Int64 recTransID = Convert.ToInt64(DGV_Operations.CellValueAsString(dgvRec.Rows[e.RowIndex].Cells[0]));

                    MudVolManRecTransPrdForm rtPrdForm = new MudVolManRecTransPrdForm(recTransID, selUnitVol, selUnitPowderMatConc, selUnitLiquidMatConc);
                    rtPrdForm.ShowDialog();

                    ComputeRecVolValues(e.RowIndex);
                }
            }
        }
        //-------------------------------------------------------
        private void btnTransAdd_Click(object sender, EventArgs e)
        {
            //string METHOD = "btnTransAdd_Click : " + CLASS_NAME;

            if (frmMain.selectedRepID == -1 || selectedMudVolDfsName == "")
                return;

            if (access.mudVolumeManagement_DFSs != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;

            List<ProcedureParam> prms = new List<ProcedureParam>();
            prms.Add(new ProcedureParam("@MudVolManDFS_ID", selectedMudVolDfsID.ToString(), ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Record"));
            prms.Add(new ProcedureParam("@RecTrans_Flag", "1", ProcedureParam.ParamType.PT_Bit, false, "", false, "", "Invalid Flag"));
            prms.Add(new ProcedureParam("@ProjAutoID", frmMain.selectedPrjID.ToString(), ProcedureParam.ParamType.PT_Int, true, "1", false, "", "Invalid Project"));

            string simpErr;
            Errors critErr;
            Int64 resultStat;

            int ret = Procedures.CallProcedure("prc_rt_Rep2MudVolManDFS_RecTrans_insert", prms, out simpErr, out critErr, out resultStat);

            if (ret != 0)
            {
                if (ret == 1)
                    InformationManager.Set_Info(simpErr);
                else
                    InformationManager.Set_Info(critErr);

                return;
            }

            LoadTrans();

            btnTransApply.ImageKey = "Check";
            btnTransApply.BackColor = frmMain.checkColor;
        }
        //-------------------------------------------------------
        private void btnTransRemove_Click(object sender, EventArgs e)
        {
            //string METHOD = "btnTransRemove_Click : " + CLASS_NAME;

            if (frmMain.selectedRepID == -1 || selectedMudVolDfsName == "")
                return;

            if (access.mudVolumeManagement_DFSs != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;

            if (dgvTrans.SelectedRows.Count == 0)
            {
                InformationManager.Set_Info("Select Row to Delete");
                return;
            }

            string idStr = DGV_Operations.CellValueAsString(dgvTrans.Rows[dgvTrans.SelectedRows[0].Index].Cells[0]);

            List<ProcedureParam> prms = new List<ProcedureParam>();
            prms.Add(new ProcedureParam("@ID", idStr, ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Record"));

            string simpErr;
            Errors critErr;
            Int64 resultStat;

            int ret = Procedures.CallProcedure("prc_rt_Rep2MudVolManDFS_RecTrans_delete", prms, out simpErr, out critErr, out resultStat);

            if (ret != 0)
            {
                if (ret == 1)
                    InformationManager.Set_Info(simpErr);
                else
                    InformationManager.Set_Info(critErr);

                return;
            }

            LoadTrans();

            btnTransApply.ImageKey = "Check";
            btnTransApply.BackColor = frmMain.checkColor;
        }
        //-------------------------------------------------------
        private void btnTransApply_Click(object sender, EventArgs e)
        {
            //string METHOD = "btnTransApply_Click : " + CLASS_NAME;

            if (frmMain.selectedRepID == -1 || selectedMudVolDfsName == "")
                return;

            if (access.mudVolumeManagement_DFSs != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;

            List<Errors> errs = new List<Errors>();

            for (int i = 0; i < dgvTrans.Rows.Count; i++)
            {

                List<ProcedureParam> prms = new List<ProcedureParam>();
                prms.Add(new ProcedureParam("@ID", DGV_Operations.CellValueAsString(dgvTrans.Rows[i].Cells[0]), ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Record"));
                prms.Add(new ProcedureParam("@Seq", DGV_Operations.CellValueAsString(dgvTrans.Rows[i].Cells[1]), ProcedureParam.ParamType.PT_Int, true, "1", true, "2000", "Invalid Seq."));
                prms.Add(new ProcedureParam("@Vol", DGV_Operations.CellValueAsString(dgvTrans.Rows[i].Cells[4]), ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Vol."));
                prms.Add(new ProcedureParam("@MW", DGV_Operations.CellValueAsString(dgvTrans.Rows[i].Cells[6]), ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid MW"));

                string locID = DGV_Operations.CellValueAsString(dgvTrans.Rows[i].Cells[3]);
                if (locID == "")
                {
                    prms.Add(new ProcedureParam("@RecTransLocation_ID", ProcedureParam.ParamType.PT_BigInt));
                    prms.Add(new ProcedureParam("@OtherRecTransLocation", DGV_Operations.CellValueAsString(dgvTrans.Rows[i].Cells[2]), 120));
                }
                else
                {
                    prms.Add(new ProcedureParam("@RecTransLocation_ID", locID, ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Location"));
                    prms.Add(new ProcedureParam("@OtherRecTransLocation", "", 120));
                }

                string simpErr;
                Errors critErr;
                Int64 resultStat;

                int ret = Procedures.CallProcedure("prc_rt_Rep2MudVolManDFS_RecTrans_update", prms, out simpErr, out critErr, out resultStat);

                Color normalBkColor = (i % 2 == 0) ? dgvTrans.BackgroundColor : dgvTrans.AlternatingRowsDefaultCellStyle.BackColor;
                dgvTrans.Rows[i].Cells[1].Style.BackColor = normalBkColor;
                dgvTrans.Rows[i].Cells[4].Style.BackColor = normalBkColor;
                dgvTrans.Rows[i].Cells[6].Style.BackColor = normalBkColor;


                if (ret != 0)
                {
                    if (ret == 1)
                    {
                        if (simpErr.StartsWith("Invalid Seq."))
                            dgvTrans.Rows[i].Cells[1].Style.BackColor = Color.Red;
                        else if (simpErr.StartsWith("Invalid Vol."))
                            dgvTrans.Rows[i].Cells[4].Style.BackColor = Color.Red;
                        else if (simpErr.StartsWith("Invalid MW"))
                            dgvTrans.Rows[i].Cells[6].Style.BackColor = Color.Red;
                        critErr = new Errors(simpErr);
                    }

                    errs.Add(critErr);
                }
            }

            InformationManager.Set_Info(errs.ToArray());

            if (errs.Count == 0)
            {
                btnTransApply.ImageKey = "Check";
                btnTransApply.BackColor = frmMain.checkColor;
            }
        }
        //-------------------------------------------------------
        private void btnTransRefresh_Click(object sender, EventArgs e)
        {
            //string METHOD = "btnTransRefresh_Click : " + CLASS_NAME;

            if (frmMain.selectedRepID == -1 || selectedMudVolDfsName == "")
                return;

            if (access.mudVolumeManagement_DFSs == RoleAccess.AcessTypes.AT_None)
                return;

            LoadTrans();

            btnTransApply.ImageKey = "Check";
            btnTransApply.BackColor = frmMain.checkColor;
        }
        //-------------------------------------------------------
        private void dgvTrans_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex == -1)
                return;

            if (skipCellValueChanged_dgvTrans)
                return;

            if (e.ColumnIndex >= 1 && e.ColumnIndex <= 6)
            {
                btnTransApply.ImageKey = "warning";
                btnTransApply.BackColor = frmMain.warningColor;
            }

            if (e.ColumnIndex == 4)
            {
                ComputeTransVolValues(e.RowIndex);
                txtTransTotalVol.Text = DGV_Operations.SummationOnColumn(dgvTrans, 4);
            }
        }
        //-------------------------------------------------------
        private void dgvTrans_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //string METHOD = "dgvTrans_CellMouseClick : " + CLASS_NAME;

            if (frmMain.selectedRepID == -1 || selectedMudVolDfsName == "")
                return;

            if (access.mudVolumeManagement_DFSs != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;

            if (e.ColumnIndex > 0 && e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 2)//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                {
                    if (e.Button == System.Windows.Forms.MouseButtons.Left)
                    {
                        string otherLocation = (DGV_Operations.CellValueAsString(dgvTrans.Rows[e.RowIndex].Cells[3]) == "")
                                ? DGV_Operations.CellValueAsString(dgvTrans.Rows[e.RowIndex].Cells[2]) : "";

                        HolesOfProjects_Selection prwhSel = new HolesOfProjects_Selection(otherLocation);

                        if (prwhSel.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                            return;

                        if (prwhSel.chbOtherLocation.Checked || prwhSel.dgvPRWH.SelectedRows.Count == 0)
                        {
                            dgvTrans.Rows[e.RowIndex].Cells[2].Value = prwhSel.txtOtherLocation.Text;
                            dgvTrans.Rows[e.RowIndex].Cells[3].Value = null;
                        }
                        else
                        {
                            string prjName = DGV_Operations.CellValueAsString(prwhSel.dgvPRWH.Rows[prwhSel.dgvPRWH.SelectedRows[0].Index].Cells[1]);
                            string rigName = DGV_Operations.CellValueAsString(prwhSel.dgvPRWH.Rows[prwhSel.dgvPRWH.SelectedRows[0].Index].Cells[2]);
                            string wellName = DGV_Operations.CellValueAsString(prwhSel.dgvPRWH.Rows[prwhSel.dgvPRWH.SelectedRows[0].Index].Cells[3]);
                            string holeLabel = DGV_Operations.CellValueAsString(prwhSel.dgvPRWH.Rows[prwhSel.dgvPRWH.SelectedRows[0].Index].Cells[4]);

                            dgvTrans.Rows[e.RowIndex].Cells[2].Value = prjName + "/" + rigName + "/" + wellName + "/" + holeLabel;
                            dgvTrans.Rows[e.RowIndex].Cells[3].Value = prwhSel.dgvPRWH.Rows[prwhSel.dgvPRWH.SelectedRows[0].Index].Cells[0].Value;
                        }

                        btnTransApply.ImageKey = "warning";
                        btnTransApply.BackColor = frmMain.warningColor;
                    }
                    else if (e.Button == System.Windows.Forms.MouseButtons.Middle)
                    {
                        dgvTrans.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = frmMain.selectedPrjName + "/" + frmMain.selected_RW_RigName + "/" + frmMain.selected_RW_WellName;

                        btnTransApply.ImageKey = "warning";
                        btnTransApply.BackColor = frmMain.warningColor;
                    }
                }
                else if (e.ColumnIndex == 5)//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                {
                    //~~~~~~~~~~~~~~
                    ProjectForm prjForm = (ProjectForm)mainForm.subForms[mainForm.PorjectFormID];
                    string selUnitVol = prjForm.lblUnitSelectedLiquidVolume.Tag.ToString();
                    //~~~~~~~~~~~~~~
                    string selUnitPowderMatConc = prjForm.lblUnitSelectedPowderMaterialConcentration.Tag.ToString();
                    string selUnitLiquidMatConc = prjForm.lblUnitSelectedLiquidMaterialConcentration.Tag.ToString();

                    Int64 recTransID = Convert.ToInt64(DGV_Operations.CellValueAsString(dgvTrans.Rows[e.RowIndex].Cells[0]));

                    MudVolManRecTransPrdForm rtPrdForm = new MudVolManRecTransPrdForm(recTransID, selUnitVol, selUnitPowderMatConc, selUnitLiquidMatConc);
                    rtPrdForm.ShowDialog();

                    ComputeTransVolValues(e.RowIndex);
                }
            }
        }
        //-------------------------------------------------------
        private void btnTransAutoCompute_Click(object sender, EventArgs e)
        {
            //string METHOD = "btnTransAutoCompute_Click : " + CLASS_NAME;

            if (frmMain.selectedRepID == -1 || selectedMudVolDfsName == "")
                return;

            if (access.mudVolumeManagement_DFSs != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;

            btnTransApply_Click(null, null);//do apply
            if (btnTransApply.ImageKey != "Check")//on error
                return;

            if (dgvTrans.SelectedRows.Count == 0)
            {
                InformationManager.Set_Info("Select Row");
                return;
            }

            string idStr = DGV_Operations.CellValueAsString(dgvTrans.Rows[dgvTrans.SelectedRows[0].Index].Cells[0]);

            List<ProcedureParam> prms = new List<ProcedureParam>();
            prms.Add(new ProcedureParam("@RecTrans_ID", idStr, ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Record"));
            prms.Add(new ProcedureParam("@ProjAutoID", frmMain.selectedPrjID.ToString(), ProcedureParam.ParamType.PT_Int, true, "1", false, "", "Invalid Project"));
            //~~~~~~~~~~~~~~
            ProjectForm prjForm = (ProjectForm)mainForm.subForms[mainForm.PorjectFormID];
            string selUnitVol = prjForm.lblUnitSelectedLiquidVolume.Tag.ToString();
            string selUnitPowderMatConc = prjForm.lblUnitSelectedPowderMaterialConcentration.Tag.ToString();
            string selUnitLiquidMatConc = prjForm.lblUnitSelectedLiquidMaterialConcentration.Tag.ToString();
            //~~~~~~~~~~~~~~
            decimal VolFactor_SelUnit2BBL = (selUnitVol == "m³") ? 6.2898m : 1;
            prms.Add(new ProcedureParam("@VolFactor_SelUnit2BBL", VolFactor_SelUnit2BBL.ToString(), ProcedureParam.ParamType.PT_Decimal, true, "0.001", false, "", "Invalid factor 1"));
            //~~~~~~~~~~~~~~
            decimal PowderConsFactor_SelUnit2LBL = (selUnitPowderMatConc == "kg/m³") ? 0.3505m : 1;
            prms.Add(new ProcedureParam("@PowderConcFactor_SelUnit2LBL", PowderConsFactor_SelUnit2LBL.ToString(), ProcedureParam.ParamType.PT_Decimal, true, "0.001", false, "", "Invalid factor 2"));
            //~~~~~~~~~~~~~~
            decimal LiquidConcFactor_ToComputeConc_GalPerBbl2SelUnit = (selUnitLiquidMatConc == "vol%") ? 0.42m : 1;
            prms.Add(new ProcedureParam("@LiquidConcFactor_ToComputeConc_GalPerBbl2SelUnit", LiquidConcFactor_ToComputeConc_GalPerBbl2SelUnit.ToString(), ProcedureParam.ParamType.PT_Decimal, true, "0.001", false, "", "Invalid factor 3"));
            //~~~~~~~~~~~~~~
            decimal LiquidConcFactor_ToComputeUsed_SelUnit2GalPerBbl = (selUnitLiquidMatConc == "vol%") ? 2.380952381m : 1;
            prms.Add(new ProcedureParam("@LiquidConcFactor_ToComputeUsed_SelUnit2GalPerBbl", LiquidConcFactor_ToComputeUsed_SelUnit2GalPerBbl.ToString(), ProcedureParam.ParamType.PT_Decimal, true, "0.001", false, "", "Invalid factor 3"));
            //~~~~~~~~~~~~~~


            string simpErr;
            Errors critErr;
            Int64 resultStat;

            int ret = Procedures.CallProcedure("prc_rt_Rep2MudVolManDFS_RecTrans_Prd_AutoAddTrans", prms, out simpErr, out critErr, out resultStat);

            if (ret != 0)
            {
                if (ret == 1)
                    InformationManager.Set_Info(simpErr);
                else
                    InformationManager.Set_Info(critErr);

                return;
            }

            ComputeTransVolValues(dgvTrans.SelectedRows[0].Index);
            InformationManager.Set_Info("Computed Successfully", "Computation Status:", "Information");
        }
        //-------------------------------------------------------
        private void btnGeneralStartVol_Click(object sender, EventArgs e)
        {
            if (access.mudVolumeManagement_DFSs != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;

            if (selectedMudVolDfsID == -1)
                return;

            try
            {
                string prevReportID;

                if (!FetchTableData.GetReportIDOfLastRevOfPrevReport(frmMain.selectedRepNum, frmMain.selected_RW_WellID, out prevReportID))
                {
                    InformationManager.Set_Info("There is no previous report");
                    return;
                }
                //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

                string Dfs_AutoIDStr;
                if (!FetchTableData.GetDfsAutoID(selectedMudVolDfsName, out Dfs_AutoIDStr))
                {
                    InformationManager.Set_Info("Invalid Drilling Fluid System");
                    return;
                }


                decimal pitTankVol = 0;
                {
                    string query =
                          " select sum(CurMudVol) "
                        + " from rt_Rep2MudVolMan_PitTankVol join lt_DrillingFluidSystem  on CurDfs_AutoID = AutoID "
                        + " where ReportID = " + prevReportID
                        + " and DrillingFluidSystem = \'" + selectedMudVolDfsName.Replace("\'", "\'\'") + "\'";

                    DataSet ds = new DataSet();
                    if (ConnectionManager.ExecQuery(query, ref ds, 1))
                    {
                        if (ds.Tables[0].Rows[0][0] != DBNull.Value)
                            pitTankVol = Convert.ToDecimal(ds.Tables[0].Rows[0][0]);

                        ds.Dispose();
                    }
                }
                //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                decimal prevAfterMerge = 0;

                object[] dtRow = FetchTableData.GetFieldsOfLastRevOfPrevReport(frmMain.selectedRepNum, frmMain.selected_RW_WellID, " Num ", false);
                if (dtRow != null && dtRow.Length == 1)
                {
                    string query = " select dbo.fn_Get_AfterMergeVol_ByWellID_UptoRepNum ("
                                        + frmMain.selected_RW_WellID.ToString() + ","
                                        + dtRow[0].ToString() + ","
                                        + Dfs_AutoIDStr
                                    + ")";

                    DataSet ds = new DataSet();

                    if (ConnectionManager.ExecQuery(query, ref ds, 1))
                    {
                        if (ds.Tables[0].Rows[0][0] != DBNull.Value)
                            prevAfterMerge = Convert.ToDecimal(ds.Tables[0].Rows[0][0]);

                        ds.Dispose();
                    }
                }
                //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                txtStartVol.Text = (pitTankVol + prevAfterMerge).ToString();
            }
            catch (Exception)
            {
            }
        }
        //-------------------------------------------------------
        private void btnGeneralApply_Click(object sender, EventArgs e)
        {
            if (access.mudVolumeManagement_DFSs != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;

            if (selectedMudVolDfsID == -1)
                return;

            List<ProcedureParam> prms = new List<ProcedureParam>();
            prms.Add(new ProcedureParam("@ID", selectedMudVolDfsID.ToString(), ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Record"));
            prms.Add(new ProcedureParam("@BuiltStartVol", txtStartVol.Text, ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Start Vol."));
            prms.Add(new ProcedureParam("@BuiltRainVol", txtRainVol.Text, ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Rain Vol."));
            prms.Add(new ProcedureParam("@BuiltGainVol", txtGainVol.Text, ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Gain Vol."));

            string simpErr;
            Errors critErr;
            Int64 resultStat;

            int ret = Procedures.CallProcedure("prc_rt_Rep2MudVolManDFS_update_Built", prms, out simpErr, out critErr, out resultStat);

            if (ret != 0)
            {
                if (ret == 1)
                    InformationManager.Set_Info(simpErr);
                else
                    InformationManager.Set_Info(critErr);

                return;
            }

            btnGeneralApply.ImageKey = "Check";
            btnGeneralApply.BackColor = frmMain.checkColor;
        }
        //-------------------------------------------------------
        private void btnBuiltVolAdd_Click(object sender, EventArgs e)
        {
            //string METHOD = "btnBuiltVolAdd_Click : " + CLASS_NAME;

            if (frmMain.selectedRepID == -1 || selectedMudVolDfsName == "")
                return;

            if (access.mudVolumeManagement_DFSs != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;

            List<ProcedureParam> prms = new List<ProcedureParam>();
            prms.Add(new ProcedureParam("@MudVolManDFS_ID", selectedMudVolDfsID.ToString(), ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Record"));
            prms.Add(new ProcedureParam("@ProjAutoID", frmMain.selectedPrjID.ToString(), ProcedureParam.ParamType.PT_Int, true, "1", false, "", "Invalid Project"));

            string simpErr;
            Errors critErr;
            Int64 resultStat;

            int ret = Procedures.CallProcedure("prc_rt_Rep2MudVolManDFS_BuiltVol_insert", prms, out simpErr, out critErr, out resultStat);

            if (ret != 0)
            {
                if (ret == 1)
                    InformationManager.Set_Info(simpErr);
                else
                    InformationManager.Set_Info(critErr);

                return;
            }

            LoadBuiltVol();

            btnBuiltVolApply.ImageKey = "Check";
            btnBuiltVolApply.BackColor = frmMain.checkColor;
        }
        //-------------------------------------------------------
        private void btnBuiltVolRemove_Click(object sender, EventArgs e)
        {
            //string METHOD = "btnBuiltVolRemove_Click : " + CLASS_NAME;

            if (frmMain.selectedRepID == -1 || selectedMudVolDfsName == "")
                return;

            if (access.mudVolumeManagement_DFSs != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;

            if (dgvBuiltVol.SelectedRows.Count == 0)
            {
                InformationManager.Set_Info("Select Row to Delete");
                return;
            }

            string idStr = DGV_Operations.CellValueAsString(dgvBuiltVol.Rows[dgvBuiltVol.SelectedRows[0].Index].Cells[0]);

            List<ProcedureParam> prms = new List<ProcedureParam>();
            prms.Add(new ProcedureParam("@ID", idStr, ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Record"));

            string simpErr;
            Errors critErr;
            Int64 resultStat;

            int ret = Procedures.CallProcedure("prc_rt_Rep2MudVolManDFS_BuiltVol_delete", prms, out simpErr, out critErr, out resultStat);

            if (ret != 0)
            {
                if (ret == 1)
                    InformationManager.Set_Info(simpErr);
                else
                    InformationManager.Set_Info(critErr);

                return;
            }

            LoadBuiltVol();

            btnBuiltVolApply.ImageKey = "Check";
            btnBuiltVolApply.BackColor = frmMain.checkColor;
        }
        //-------------------------------------------------------
        private void btnBuiltVolApply_Click(object sender, EventArgs e)
        {
            //string METHOD = "btnBuiltVolApply_Click : " + CLASS_NAME;

            if (frmMain.selectedRepID == -1 || selectedMudVolDfsName == "")
                return;

            if (access.mudVolumeManagement_DFSs != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;

            List<Errors> errs = new List<Errors>();

            for (int i = 0; i < dgvBuiltVol.Rows.Count; i++)
            {
                List<ProcedureParam> prms = new List<ProcedureParam>();
                prms.Add(new ProcedureParam("@ID", DGV_Operations.CellValueAsString(dgvBuiltVol.Rows[i].Cells[0]), ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Record"));
                prms.Add(new ProcedureParam("@Seq", DGV_Operations.CellValueAsString(dgvBuiltVol.Rows[i].Cells[1]), ProcedureParam.ParamType.PT_Int, true, "1", true, "2000", "Invalid Seq."));
                prms.Add(new ProcedureParam("@AddOil", DGV_Operations.CellValueAsString(dgvBuiltVol.Rows[i].Cells[8]), ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Added Oil"));
                prms.Add(new ProcedureParam("@AddSeaWater", DGV_Operations.CellValueAsString(dgvBuiltVol.Rows[i].Cells[4]), ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Added Sea Water"));
                prms.Add(new ProcedureParam("@AddDrillWater", DGV_Operations.CellValueAsString(dgvBuiltVol.Rows[i].Cells[5]), ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Added Drill Water"));
                prms.Add(new ProcedureParam("@AddDeWater", DGV_Operations.CellValueAsString(dgvBuiltVol.Rows[i].Cells[6]), ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Added DeWater"));
                prms.Add(new ProcedureParam("@AddLocalWater", DGV_Operations.CellValueAsString(dgvBuiltVol.Rows[i].Cells[7]), ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Added Local Water"));
                prms.Add(new ProcedureParam("@MW", DGV_Operations.CellValueAsString(dgvBuiltVol.Rows[i].Cells[9]), ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid MW"));

                string simpErr;
                Errors critErr;
                Int64 resultStat;

                int ret = Procedures.CallProcedure("prc_rt_Rep2MudVolManDFS_BuiltVol_update", prms, out simpErr, out critErr, out resultStat);

                Color normalBkColor = (i % 2 == 0) ? dgvBuiltVol.BackgroundColor : dgvBuiltVol.AlternatingRowsDefaultCellStyle.BackColor;

                for (int c = 3; c <= 8; c++)
                    dgvBuiltVol.Rows[i].Cells[c].Style.BackColor = normalBkColor;

                dgvBuiltVol.Rows[i].Cells[1].Style.BackColor = normalBkColor;


                if (ret != 0)
                {
                    if (ret == 1)
                    {
                        if (simpErr.StartsWith("Invalid Seq."))
                            dgvBuiltVol.Rows[i].Cells[1].Style.BackColor = Color.Red;
                        else if (simpErr.StartsWith("Invalid Added Oil"))
                            dgvBuiltVol.Rows[i].Cells[8].Style.BackColor = Color.Red;
                        else if (simpErr.StartsWith("Invalid Added Sea Water"))
                            dgvBuiltVol.Rows[i].Cells[4].Style.BackColor = Color.Red;
                        else if (simpErr.StartsWith("Invalid Added Drill Water"))
                            dgvBuiltVol.Rows[i].Cells[5].Style.BackColor = Color.Red;
                        else if (simpErr.StartsWith("Invalid Added DeWater"))
                            dgvBuiltVol.Rows[i].Cells[6].Style.BackColor = Color.Red;
                        else if (simpErr.StartsWith("Invalid Added Local Water"))
                            dgvBuiltVol.Rows[i].Cells[7].Style.BackColor = Color.Red;
                        else if (simpErr.StartsWith("Invalid MW"))
                            dgvBuiltVol.Rows[i].Cells[9].Style.BackColor = Color.Red;
                        critErr = new Errors(simpErr);
                    }

                    errs.Add(critErr);
                }
            }

            InformationManager.Set_Info(errs.ToArray());

            if (errs.Count == 0)
            {
                btnBuiltVolApply.ImageKey = "Check";
                btnBuiltVolApply.BackColor = frmMain.checkColor;
            }
        }
        //-------------------------------------------------------
        private void btnBuiltVolRefresh_Click(object sender, EventArgs e)
        {
            //string METHOD = "btnBuiltVolRefresh_Click : " + CLASS_NAME;

            if (frmMain.selectedRepID == -1 || selectedMudVolDfsName == "")
                return;

            if (access.mudVolumeManagement_DFSs == RoleAccess.AcessTypes.AT_None)
                return;

            LoadBuiltVol();

            btnBuiltVolApply.ImageKey = "Check";
            btnBuiltVolApply.BackColor = frmMain.checkColor;
        }
        //-------------------------------------------------------
        private void dgvBuiltVol_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex == -1)
                return;

            if (skipCellValueChanged_dgvBuiltVol)
                return;

            if (e.ColumnIndex == 1 || (e.ColumnIndex >= 3 && e.ColumnIndex <= 9))
            {
                btnBuiltVolApply.ImageKey = "warning";
                btnBuiltVolApply.BackColor = frmMain.warningColor;
            }

            ComputeBuildVolValues(e.RowIndex);

            txtBuiltVolTotalVol.Text = DGV_Operations.SummationOnColumn(dgvBuiltVol, 10);
        }
        //-------------------------------------------------------
        private void dgvBuiltVol_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //string METHOD = "dgvBuiltVol_CellMouseClick : " + CLASS_NAME;

            if (frmMain.selectedRepID == -1 || selectedMudVolDfsName == "")
                return;

            if (access.mudVolumeManagement_DFSs != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;

            if (e.ColumnIndex > 0 && e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 2)//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                {
                    Int64 builtID = Convert.ToInt64(DGV_Operations.CellValueAsString(dgvBuiltVol.Rows[e.RowIndex].Cells[0]));

                    MudVolManBuiltPrdForm mprd = new MudVolManBuiltPrdForm(builtID, true);

                    mprd.ShowDialog();

                    ComputeBuildVolValues(e.RowIndex);
                    txtBuiltVolTotalVol.Text = DGV_Operations.SummationOnColumn(dgvBuiltVol, 10);
                }
                else if (e.ColumnIndex == 3)//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                {
                    MudVolManAddedWaterForm frmAW = new MudVolManAddedWaterForm();
                    frmAW.dgvAdded.Rows.Add(dgvBuiltVol.Rows[e.RowIndex].Cells[4].Value,
                                            dgvBuiltVol.Rows[e.RowIndex].Cells[5].Value,
                                            dgvBuiltVol.Rows[e.RowIndex].Cells[6].Value,
                                            dgvBuiltVol.Rows[e.RowIndex].Cells[7].Value);

                    DialogResult dlgRes = System.Windows.Forms.DialogResult.Cancel;

                    while ((dlgRes = frmAW.ShowDialog()) == System.Windows.Forms.DialogResult.OK)
                    {
                        int c = 0;
                        for (c = 0; c < frmAW.dgvAdded.Columns.Count; c++)
                        {
                            decimal val = 0;
                            string valStr = DGV_Operations.CellValueAsString(frmAW.dgvAdded[c, 0]);

                            if (!AdvancedConvertor.ToDecimal(valStr, ref val))
                            {
                                InformationManager.Set_Info("Invalid Value: " + valStr);
                                break;
                            }
                        }

                        if (c == frmAW.dgvAdded.Columns.Count)
                            break;
                    }

                    if (dlgRes != System.Windows.Forms.DialogResult.OK)
                        return;

                    dgvBuiltVol.Rows[e.RowIndex].Cells[4].Value = DGV_Operations.CellValueAsString(frmAW.dgvAdded[0, 0]);
                    dgvBuiltVol.Rows[e.RowIndex].Cells[5].Value = DGV_Operations.CellValueAsString(frmAW.dgvAdded[1, 0]);
                    dgvBuiltVol.Rows[e.RowIndex].Cells[6].Value = DGV_Operations.CellValueAsString(frmAW.dgvAdded[2, 0]);
                    dgvBuiltVol.Rows[e.RowIndex].Cells[7].Value = DGV_Operations.CellValueAsString(frmAW.dgvAdded[3, 0]);

                    ComputeBuildVolValues(e.RowIndex);
                    txtBuiltVolTotalVol.Text = DGV_Operations.SummationOnColumn(dgvBuiltVol, 10);
                }
            }
        }
        //-------------------------------------------------------
        private void btnBuiltUsingAdd_Click(object sender, EventArgs e)
        {
            //string METHOD = "btnBuiltUsingAdd_Click : " + CLASS_NAME;

            if (frmMain.selectedRepID == -1 || selectedMudVolDfsName == "")
                return;

            if (access.mudVolumeManagement_DFSs != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;

            List<ProcedureParam> prms = new List<ProcedureParam>();
            prms.Add(new ProcedureParam("@MudVolManDFS_ID", selectedMudVolDfsID.ToString(), ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Record"));
            prms.Add(new ProcedureParam("@ProjAutoID", frmMain.selectedPrjID.ToString(), ProcedureParam.ParamType.PT_Int, true, "1", false, "", "Invalid Project"));

            string simpErr;
            Errors critErr;
            Int64 resultStat;

            int ret = Procedures.CallProcedure("prc_rt_Rep2MudVolManDFS_BuiltUsingOthers_insert", prms, out simpErr, out critErr, out resultStat);

            if (ret != 0)
            {
                if (ret == 1)
                    InformationManager.Set_Info(simpErr);
                else
                    InformationManager.Set_Info(critErr);

                return;
            }

            LoadBuiltUsing();

            btnBuiltUsingApply.ImageKey = "Check";
            btnBuiltUsingApply.BackColor = frmMain.checkColor;
        }
        //-------------------------------------------------------
        private void btnBuiltUsingRemove_Click(object sender, EventArgs e)
        {
            //string METHOD = "btnBuiltUsingRemove_Click : " + CLASS_NAME;

            if (frmMain.selectedRepID == -1 || selectedMudVolDfsName == "")
                return;

            if (access.mudVolumeManagement_DFSs != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;

            if (dgvBuiltUsing.SelectedRows.Count == 0)
            {
                InformationManager.Set_Info("Select Row to Delete");
                return;
            }

            string idStr = DGV_Operations.CellValueAsString(dgvBuiltUsing.Rows[dgvBuiltUsing.SelectedRows[0].Index].Cells[0]);

            List<ProcedureParam> prms = new List<ProcedureParam>();
            prms.Add(new ProcedureParam("@ID", idStr, ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Record"));

            string simpErr;
            Errors critErr;
            Int64 resultStat;

            int ret = Procedures.CallProcedure("prc_rt_Rep2MudVolManDFS_BuiltUsingOthers_delete", prms, out simpErr, out critErr, out resultStat);

            if (ret != 0)
            {
                if (ret == 1)
                    InformationManager.Set_Info(simpErr);
                else
                    InformationManager.Set_Info(critErr);

                return;
            }

            LoadBuiltUsing();

            btnBuiltUsingApply.ImageKey = "Check";
            btnBuiltUsingApply.BackColor = frmMain.checkColor;
        }
        //-------------------------------------------------------
        private void btnBuiltUsingApply_Click(object sender, EventArgs e)
        {
            //string METHOD = "btnBuiltUsingApply_Click : " + CLASS_NAME;

            if (frmMain.selectedRepID == -1 || selectedMudVolDfsName == "")
                return;

            if (access.mudVolumeManagement_DFSs != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;

            List<Errors> errs = new List<Errors>();

            for (int i = 0; i < dgvBuiltUsing.Rows.Count; i++)
            {
                List<ProcedureParam> prms = new List<ProcedureParam>();
                prms.Add(new ProcedureParam("@ID", DGV_Operations.CellValueAsString(dgvBuiltUsing.Rows[i].Cells[0]), ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Record"));
                prms.Add(new ProcedureParam("@Seq", DGV_Operations.CellValueAsString(dgvBuiltUsing.Rows[i].Cells[1]), ProcedureParam.ParamType.PT_Int, true, "1", true, "2000", "Invalid Seq."));
                prms.Add(new ProcedureParam("@UsedVol", DGV_Operations.CellValueAsString(dgvBuiltUsing.Rows[i].Cells[4]), ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Used Vol."));
                prms.Add(new ProcedureParam("@MW", DGV_Operations.CellValueAsString(dgvBuiltUsing.Rows[i].Cells[6]), ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid MW"));


                string dfs = DGV_Operations.CellValueAsString(dgvBuiltUsing.Rows[i].Cells[3]);
                string dfsAutoID = DGV_Operations.CellValueAsString(dgvBuiltUsing.Rows[i].Cells[2]);

                if (dfs == "")
                    prms.Add(new ProcedureParam("@Dfs_AutoID", ProcedureParam.ParamType.PT_Int));
                else
                    prms.Add(new ProcedureParam("@Dfs_AutoID", dfsAutoID, ProcedureParam.ParamType.PT_Int, true, "1", false, "", "Invalid DFS"));


                string simpErr;
                Errors critErr;
                Int64 resultStat;

                int ret = Procedures.CallProcedure("prc_rt_Rep2MudVolManDFS_BuiltUsingOthers_update", prms, out simpErr, out critErr, out resultStat);

                Color normalBkColor = (i % 2 == 0) ? dgvBuiltUsing.BackgroundColor : dgvBuiltUsing.AlternatingRowsDefaultCellStyle.BackColor;

                for (int c = 0; c <= 6; c++)
                    dgvBuiltUsing.Rows[i].Cells[c].Style.BackColor = normalBkColor;

                dgvBuiltUsing.Rows[i].Cells[1].Style.BackColor = normalBkColor;


                if (ret != 0)
                {
                    if (ret == 1)
                    {
                        if (simpErr.StartsWith("Invalid Seq."))
                            dgvBuiltUsing.Rows[i].Cells[1].Style.BackColor = Color.Red;
                        else if (simpErr.StartsWith("Invalid Used Vol."))
                            dgvBuiltUsing.Rows[i].Cells[4].Style.BackColor = Color.Red;
                        else if (simpErr.StartsWith("Invalid MW"))
                            dgvBuiltUsing.Rows[i].Cells[6].Style.BackColor = Color.Red;
                        critErr = new Errors(simpErr);
                    }

                    errs.Add(critErr);
                }
            }

            InformationManager.Set_Info(errs.ToArray());

            if (errs.Count == 0)
            {
                btnBuiltUsingApply.ImageKey = "Check";
                btnBuiltUsingApply.BackColor = frmMain.checkColor;
            }
        }
        //-------------------------------------------------------
        private void btnBuiltUsingRefresh_Click(object sender, EventArgs e)
        {
            //string METHOD = "btnBuiltUsingRefresh_Click : " + CLASS_NAME;

            if (frmMain.selectedRepID == -1 || selectedMudVolDfsName == "")
                return;

            if (access.mudVolumeManagement_DFSs == RoleAccess.AcessTypes.AT_None)
                return;

            LoadBuiltUsing();

            btnBuiltUsingApply.ImageKey = "Check";
            btnBuiltUsingApply.BackColor = frmMain.checkColor;
        }
        //-------------------------------------------------------
        private void dgvBuiltUsing_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex == -1)
                return;

            if (skipCellValueChanged_dgvBuiltUsing)
                return;

            if (e.ColumnIndex >= 1 && e.ColumnIndex <= 6)
            {
                btnBuiltUsingApply.ImageKey = "warning";
                btnBuiltUsingApply.BackColor = frmMain.warningColor;
            }

            ComputeBuildUsingValues(e.RowIndex);

            //txtBuiltUsingTotalVol.Text = DGV_Operations.SummationOnColumn(dgvBuiltUsing, 4);
        }
        //-------------------------------------------------------
        private void dgvBuiltUsing_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //string METHOD = "dgvBuiltUsing_CellMouseClick : " + CLASS_NAME;

            if (frmMain.selectedRepID == -1 || selectedMudVolDfsName == "")
                return;

            if (access.mudVolumeManagement_DFSs != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;

            if (e.ColumnIndex > 0 && e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 3)//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                {
                    string query =
                        " select 0, AutoID, MudType, DrillingFluidSystem from lt_DrillingFluidSystem  "
                        + " where AutoID in "
                        + " ( "
                        + "     select rt_Rep2MudVolManDFS.DFS_AutoID "
                        + "     from rt_Rep2MudVolManDFS join at_Report On rt_Rep2MudVolManDFS.ReportID = at_Report.ID "
                        + "     where at_Report.ID =  " + frmMain.selectedRepID.ToString()
                        + "     and rt_Rep2MudVolManDFS.ID <> " + selectedMudVolDfsID.ToString()
                        + " ) ";

                    DrillingFluidSystemFormDB_Selection frmDFSel = new DrillingFluidSystemFormDB_Selection(query);
                    frmDFSel.Text = "Drilling Fluid Systems";

                    frmDFSel.dgvFluidSys.Columns[0].Visible = false;//single selection

                    System.Windows.Forms.DialogResult dgRes = frmDFSel.ShowDialog();

                    if (dgRes == System.Windows.Forms.DialogResult.Cancel)
                        return;

                    if (dgRes == System.Windows.Forms.DialogResult.Ignore)
                    {
                        dgvBuiltUsing.Rows[e.RowIndex].Cells[2].Value = DBNull.Value;
                        dgvBuiltUsing.Rows[e.RowIndex].Cells[3].Value = "";
                        return;
                    }

                    if (frmDFSel.dgvFluidSys.SelectedRows.Count == 0)
                        return;

                    string drillFS = DGV_Operations.CellValueAsString(frmDFSel.dgvFluidSys.Rows[frmDFSel.dgvFluidSys.SelectedRows[0].Index].Cells[3]);
                    string mudType = DGV_Operations.CellValueAsString(frmDFSel.dgvFluidSys.Rows[frmDFSel.dgvFluidSys.SelectedRows[0].Index].Cells[2]);
                    string dfsAutoID = DGV_Operations.CellValueAsString(frmDFSel.dgvFluidSys.Rows[frmDFSel.dgvFluidSys.SelectedRows[0].Index].Cells[1]);

                    dgvBuiltUsing.Rows[e.RowIndex].Cells[3].Value = drillFS;
                    dgvBuiltUsing.Rows[e.RowIndex].Cells[2].Value = dfsAutoID;
                }
                else if (e.ColumnIndex == 5)//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                {
                    Int64 builtID = Convert.ToInt64(DGV_Operations.CellValueAsString(dgvBuiltUsing.Rows[e.RowIndex].Cells[0]));

                    MudVolManBuiltPrdForm mprd = new MudVolManBuiltPrdForm(builtID, false);

                    mprd.ShowDialog();

                    ComputeBuildUsingValues(e.RowIndex);
                    //txtBuiltUsingTotalVol.Text = DGV_Operations.SummationOnColumn(dgvBuiltUsing, 4);
                }
            }
        }
        //-------------------------------------------------------
        private void btnBuiltUsingOthersAutoCompute_Click(object sender, EventArgs e)
        {
            //string METHOD = "btnUsingOthersAutoCompute_Click : " + CLASS_NAME;

            if (frmMain.selectedRepID == -1 || selectedMudVolDfsName == "")
                return;

            if (access.mudVolumeManagement_DFSs != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;

            btnBuiltUsingApply_Click(null, null);//do apply
            if (btnBuiltUsingApply.ImageKey != "Check")//on error
                return;

            if (dgvBuiltUsing.SelectedRows.Count == 0)
            {
                InformationManager.Set_Info("Select Row");
                return;
            }

            string idStr = DGV_Operations.CellValueAsString(dgvBuiltUsing.Rows[dgvBuiltUsing.SelectedRows[0].Index].Cells[0]);

            List<ProcedureParam> prms = new List<ProcedureParam>();
            prms.Add(new ProcedureParam("@BuiltUsingOthers_ID", idStr, ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Record"));
            prms.Add(new ProcedureParam("@ProjAutoID", frmMain.selectedPrjID.ToString(), ProcedureParam.ParamType.PT_Int, true, "1", false, "", "Invalid Project"));
            //~~~~~~~~~~~~~~
            prms.Add(new ProcedureParam("@UsingOthersVol", DGV_Operations.CellValueAsString(dgvBuiltUsing.Rows[dgvBuiltUsing.SelectedRows[0].Index].Cells[4]), ProcedureParam.ParamType.PT_Decimal, true, "0.001", false, "", "Invalid Vol"));
            //~~~~~~~~~~~~~~
            ProjectForm prjForm = (ProjectForm)mainForm.subForms[mainForm.PorjectFormID];
            string selUnitVol = prjForm.lblUnitSelectedLiquidVolume.Tag.ToString();
            //~~~~~~~~~~~~~~
            string selUnitPowderMatConc = prjForm.lblUnitSelectedPowderMaterialConcentration.Tag.ToString();
            string selUnitLiquidMatConc = prjForm.lblUnitSelectedLiquidMaterialConcentration.Tag.ToString();
            //~~~~~~~~~~~~~~
            decimal VolFactor_SelUnit2BBL = (selUnitVol == "m³") ? 6.2898m : 1;
            prms.Add(new ProcedureParam("@VolFactor_SelUnit2BBL", VolFactor_SelUnit2BBL.ToString(), ProcedureParam.ParamType.PT_Decimal, true, "0.001", false, "", "Invalid factor 1"));
            //~~~~~~~~~~~~~~
            decimal PowderConsFactor_LBL2SelUnit = (selUnitPowderMatConc == "kg/m³") ? 2.853m : 1;
            prms.Add(new ProcedureParam("@PowderConcFactor_SelUnit2LBL", PowderConsFactor_LBL2SelUnit.ToString(), ProcedureParam.ParamType.PT_Decimal, true, "0.001", false, "", "Invalid factor 2"));
            //~~~~~~~~~~~~~~
            decimal LiquidConcFactor_ToComputeUsed_SelUnit2GalPerBbl = (selUnitLiquidMatConc == "vol%") ? 2.380952381m : 1;
            prms.Add(new ProcedureParam("@LiquidConcFactor_ToComputeUsed_SelUnit2GalPerBbl", LiquidConcFactor_ToComputeUsed_SelUnit2GalPerBbl.ToString(), ProcedureParam.ParamType.PT_Decimal, true, "0.001", false, "", "Invalid factor 3"));
            //~~~~~~~~~~~~~~

            string simpErr;
            Errors critErr;
            Int64 resultStat;

            int ret = Procedures.CallProcedure("prc_rt_Rep2MudVolManDFS_BuiltUsingOthers_Prd_AutoAddBuildUsingOthers", prms, out simpErr, out critErr, out resultStat);

            if (ret != 0)
            {
                if (ret == 1)
                    InformationManager.Set_Info(simpErr);
                else
                    InformationManager.Set_Info(critErr);

                return;
            }


            ComputeBuildUsingValues(dgvBuiltUsing.SelectedRows[0].Index);
            InformationManager.Set_Info("Computed Successfully", "Computation Status:", "Information");

            btnBuiltUsingRefresh_Click(null, null);
        }
        //-------------------------------------------------------
        private void btnTreatAdd_Click(object sender, EventArgs e)
        {
            //string METHOD = "btnTreatAdd_Click : " + CLASS_NAME;

            if (frmMain.selectedRepID == -1 || selectedMudVolDfsName == "")
                return;

            if (access.mudVolumeManagement_DFSs != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;

            List<ProcedureParam> prms = new List<ProcedureParam>();
            prms.Add(new ProcedureParam("@MudVolManDFS_ID", selectedMudVolDfsID.ToString(), ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Record"));
            prms.Add(new ProcedureParam("@ProjAutoID", frmMain.selectedPrjID.ToString(), ProcedureParam.ParamType.PT_Int, true, "1", false, "", "Invalid Project"));

            string simpErr;
            Errors critErr;
            Int64 resultStat;

            int ret = Procedures.CallProcedure("prc_rt_Rep2MudVolManDFS_TreatedVol_insert", prms, out simpErr, out critErr, out resultStat);

            if (ret != 0)
            {
                if (ret == 1)
                    InformationManager.Set_Info(simpErr);
                else
                    InformationManager.Set_Info(critErr);

                return;
            }

            LoadTreated();

            btnTreatApply.ImageKey = "Check";
            btnTreatApply.BackColor = frmMain.checkColor;
        }
        //-------------------------------------------------------
        private void btnTreatRemove_Click(object sender, EventArgs e)
        {
            //string METHOD = "btnTreatRemove_Click : " + CLASS_NAME;

            if (frmMain.selectedRepID == -1 || selectedMudVolDfsName == "")
                return;

            if (access.mudVolumeManagement_DFSs != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;

            if (dgvTreat.SelectedRows.Count == 0)
            {
                InformationManager.Set_Info("Select Row to Delete");
                return;
            }

            string idStr = DGV_Operations.CellValueAsString(dgvTreat.Rows[dgvTreat.SelectedRows[0].Index].Cells[0]);

            List<ProcedureParam> prms = new List<ProcedureParam>();
            prms.Add(new ProcedureParam("@ID", idStr, ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Record"));

            string simpErr;
            Errors critErr;
            Int64 resultStat;

            int ret = Procedures.CallProcedure("prc_rt_Rep2MudVolManDFS_TreatedVol_delete", prms, out simpErr, out critErr, out resultStat);

            if (ret != 0)
            {
                if (ret == 1)
                    InformationManager.Set_Info(simpErr);
                else
                    InformationManager.Set_Info(critErr);

                return;
            }

            LoadTreated();

            btnTreatApply.ImageKey = "Check";
            btnTreatApply.BackColor = frmMain.checkColor;
        }
        //-------------------------------------------------------
        private void btnTreatApply_Click(object sender, EventArgs e)
        {
            //string METHOD = "btnTreatApply_Click : " + CLASS_NAME;

            if (frmMain.selectedRepID == -1 || selectedMudVolDfsName == "")
                return;

            if (access.mudVolumeManagement_DFSs != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;

            List<Errors> errs = new List<Errors>();

            for (int i = 0; i < dgvTreat.Rows.Count; i++)
            {
                List<ProcedureParam> prms = new List<ProcedureParam>();
                prms.Add(new ProcedureParam("@ID", DGV_Operations.CellValueAsString(dgvTreat.Rows[i].Cells[0]), ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Record"));
                prms.Add(new ProcedureParam("@Seq", DGV_Operations.CellValueAsString(dgvTreat.Rows[i].Cells[1]), ProcedureParam.ParamType.PT_Int, true, "1", true, "2000", "Invalid Seq."));
                prms.Add(new ProcedureParam("@VolTreat", DGV_Operations.CellValueAsString(dgvTreat.Rows[i].Cells[2]), ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Vol. for Treatment"));
                prms.Add(new ProcedureParam("@AddOil", DGV_Operations.CellValueAsString(dgvTreat.Rows[i].Cells[9]), ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Added Oil"));
                prms.Add(new ProcedureParam("@AddSeaWater", DGV_Operations.CellValueAsString(dgvTreat.Rows[i].Cells[5]), ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Added Sea Water"));
                prms.Add(new ProcedureParam("@AddDrillWater", DGV_Operations.CellValueAsString(dgvTreat.Rows[i].Cells[6]), ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Added Drill Water"));
                prms.Add(new ProcedureParam("@AddDeWater", DGV_Operations.CellValueAsString(dgvTreat.Rows[i].Cells[7]), ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Added DeWater"));
                prms.Add(new ProcedureParam("@AddLocalWater", DGV_Operations.CellValueAsString(dgvTreat.Rows[i].Cells[8]), ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Added Local Water"));
                prms.Add(new ProcedureParam("@MW", DGV_Operations.CellValueAsString(dgvTreat.Rows[i].Cells[10]), ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid MW"));
                prms.Add(new ProcedureParam("@Reason", DGV_Operations.CellValueAsString(dgvTreat.Rows[i].Cells[13]), 150));

                string simpErr;
                Errors critErr;
                Int64 resultStat;

                int ret = Procedures.CallProcedure("prc_rt_Rep2MudVolManDFS_TreatedVol_update", prms, out simpErr, out critErr, out resultStat);

                Color normalBkColor = (i % 2 == 0) ? dgvTreat.BackgroundColor : dgvTreat.AlternatingRowsDefaultCellStyle.BackColor;

                for (int c = 1; c <= 10; c++)
                    dgvTreat.Rows[i].Cells[c].Style.BackColor = normalBkColor;


                if (ret != 0)
                {
                    if (ret == 1)
                    {
                        if (simpErr.StartsWith("Invalid Seq."))
                            dgvTreat.Rows[i].Cells[1].Style.BackColor = Color.Red;
                        else if (simpErr.StartsWith("Invalid Vol. for Treatment"))
                            dgvTreat.Rows[i].Cells[2].Style.BackColor = Color.Red;
                        else if (simpErr.StartsWith("Invalid Added Oil"))
                            dgvTreat.Rows[i].Cells[9].Style.BackColor = Color.Red;
                        else if (simpErr.StartsWith("Invalid Added Sea Water"))
                            dgvTreat.Rows[i].Cells[5].Style.BackColor = Color.Red;
                        else if (simpErr.StartsWith("Invalid Added Drill Water"))
                            dgvTreat.Rows[i].Cells[6].Style.BackColor = Color.Red;
                        else if (simpErr.StartsWith("Invalid Added DeWater"))
                            dgvTreat.Rows[i].Cells[7].Style.BackColor = Color.Red;
                        else if (simpErr.StartsWith("Invalid Added Local Water"))
                            dgvTreat.Rows[i].Cells[8].Style.BackColor = Color.Red;
                        else if (simpErr.StartsWith("Invalid MW"))
                            dgvTreat.Rows[i].Cells[10].Style.BackColor = Color.Red;
                        critErr = new Errors(simpErr);
                    }

                    errs.Add(critErr);
                }
            }

            InformationManager.Set_Info(errs.ToArray());

            if (errs.Count == 0)
            {
                btnTreatApply.ImageKey = "Check";
                btnTreatApply.BackColor = frmMain.checkColor;
            }
        }
        //-------------------------------------------------------
        private void btnTreatRefresh_Click(object sender, EventArgs e)
        {
            //string METHOD = "btnTreatRefresh_Click : " + CLASS_NAME;

            if (frmMain.selectedRepID == -1 || selectedMudVolDfsName == "")
                return;

            if (access.mudVolumeManagement_DFSs == RoleAccess.AcessTypes.AT_None)
                return;

            LoadTreated();

            btnTreatApply.ImageKey = "Check";
            btnTreatApply.BackColor = frmMain.checkColor;
        }
        //-------------------------------------------------------
        private void dgvTreat_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex == -1)
                return;

            if (skipCellValueChanged_dgvTreat)
                return;

            if (e.ColumnIndex >= 1 && e.ColumnIndex <= 13)
            {
                btnTreatApply.ImageKey = "warning";
                btnTreatApply.BackColor = frmMain.warningColor;
            }

            ComputeTreatedValues(e.RowIndex);
            txtTreatTotalVol.Text = DGV_Operations.SummationOnColumn(dgvTreat, 12);
        }
        //-------------------------------------------------------
        private void dgvTreat_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //string METHOD = "dgvTreat_CellMouseClick : " + CLASS_NAME;

            if (frmMain.selectedRepID == -1 || selectedMudVolDfsName == "")
                return;

            if (access.mudVolumeManagement_DFSs != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;

            if (e.ColumnIndex > 0 && e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 3)//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                {
                    Int64 treatID = Convert.ToInt64(DGV_Operations.CellValueAsString(dgvTreat.Rows[e.RowIndex].Cells[0]));

                    MudVolManTreatedPrdForm mprd = new MudVolManTreatedPrdForm(treatID);

                    mprd.ShowDialog();

                    ComputeTreatedValues(e.RowIndex);
                    txtTreatTotalVol.Text = DGV_Operations.SummationOnColumn(dgvTreat, 12);
                }
                else if (e.ColumnIndex == 4)//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                {
                    MudVolManAddedWaterForm frmAW = new MudVolManAddedWaterForm();
                    frmAW.dgvAdded.Rows.Add(dgvTreat.Rows[e.RowIndex].Cells[5].Value,
                                            dgvTreat.Rows[e.RowIndex].Cells[6].Value,
                                            dgvTreat.Rows[e.RowIndex].Cells[7].Value,
                                            dgvTreat.Rows[e.RowIndex].Cells[8].Value);

                    DialogResult dlgRes = System.Windows.Forms.DialogResult.Cancel;

                    while ((dlgRes = frmAW.ShowDialog()) == System.Windows.Forms.DialogResult.OK)
                    {
                        int c = 0;
                        for (c = 0; c < frmAW.dgvAdded.Columns.Count; c++)
                        {
                            decimal val = 0;
                            string valStr = DGV_Operations.CellValueAsString(frmAW.dgvAdded[c, 0]);

                            if (!AdvancedConvertor.ToDecimal(valStr, ref val))
                            {
                                InformationManager.Set_Info("Invalid Value: " + valStr);
                                break;
                            }
                        }

                        if (c == frmAW.dgvAdded.Columns.Count)
                            break;
                    }

                    if (dlgRes != System.Windows.Forms.DialogResult.OK)
                        return;

                    dgvTreat.Rows[e.RowIndex].Cells[5].Value = DGV_Operations.CellValueAsString(frmAW.dgvAdded[0, 0]);
                    dgvTreat.Rows[e.RowIndex].Cells[6].Value = DGV_Operations.CellValueAsString(frmAW.dgvAdded[1, 0]);
                    dgvTreat.Rows[e.RowIndex].Cells[7].Value = DGV_Operations.CellValueAsString(frmAW.dgvAdded[2, 0]);
                    dgvTreat.Rows[e.RowIndex].Cells[8].Value = DGV_Operations.CellValueAsString(frmAW.dgvAdded[3, 0]);

                    ComputeTreatedValues(e.RowIndex);
                    txtTreatTotalVol.Text = DGV_Operations.SummationOnColumn(dgvTreat, 12);
                }
            }
        }
        //-------------------------------------------------------
        private void btnDispRetApply_Click(object sender, EventArgs e)
        {
            if (access.mudVolumeManagement_DFSs != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;

            if (selectedMudVolDfsID == -1)
                return;

            List<ProcedureParam> prms = new List<ProcedureParam>();
            prms.Add(new ProcedureParam("@ID", selectedMudVolDfsID.ToString(), ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Record"));
            prms.Add(new ProcedureParam("@DailyStringDisp", txtDailyStringDisp.Text, ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid String Disp."));
            prms.Add(new ProcedureParam("@DailyMudRetPitVol", txtDailyPitVol.Text, ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Mud Ret. Pit Vol."));
            prms.Add(new ProcedureParam("@DailyMudRetWasteVol", txtDailyWaste.Text, ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Mud Ret. Waste Vol."));
            prms.Add(new ProcedureParam("@DailyMudRetOverVol", txtDailyOver.Text, ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Mud Ret. Overboard Vol."));
            prms.Add(new ProcedureParam("@DailyMudRetPitMW", txtDailyPitMW.Text, ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Mud Ret. Pit MW"));
            prms.Add(new ProcedureParam("@AtEndMudDispDepth", txtAtEndDepth.Text, ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Mud Disp. Depth"));
            prms.Add(new ProcedureParam("@AtEndMudDispMinFR", txtAtEndMinFR.Text, ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Mud Disp. Min Flow Rate"));
            prms.Add(new ProcedureParam("@AtEndMudDispMaxFR", txtAtEndMaxFR.Text, ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Mud Disp. Max Flow Rate"));
            prms.Add(new ProcedureParam("@AtEndMudDispVol", txtAtEndVol.Text, ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Mud Disp. Vol."));
            prms.Add(new ProcedureParam("@AtEndMudDispMW", txtAtEndMW.Text, ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Mud Disp. MW"));
            prms.Add(new ProcedureParam("@AtEndMudRetPitVol", txtAtEndPitVol.Text, ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Mud Ret. Pit Vol."));
            prms.Add(new ProcedureParam("@AtEndMudRetWasteVol", txtAtEndWaste.Text, ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Mud Ret. Waste Vol."));
            prms.Add(new ProcedureParam("@AtEndMudRetOverVol", txtAtEndOver.Text, ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Mud Ret. Overboard Vol."));
            prms.Add(new ProcedureParam("@AtEndMudRetPitMW", txtAtEndPitMW.Text, ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Mud Ret. Pit MW"));

            string simpErr;
            Errors critErr;
            Int64 resultStat;

            int ret = Procedures.CallProcedure("prc_rt_Rep2MudVolManDFS_update_DispRet", prms, out simpErr, out critErr, out resultStat);

            if (ret != 0)
            {
                if (ret == 1)
                    InformationManager.Set_Info(simpErr);
                else
                    InformationManager.Set_Info(critErr);

                return;
            }

            btnDispRetApply.ImageKey = "Check";
            btnDispRetApply.BackColor = frmMain.checkColor;
        }
        //-------------------------------------------------------
        private void btnTotalWellVolRefresh_Click(object sender, EventArgs e)
        {
            //string METHOD = "btnTotalWellVolRefresh_Click : " + CLASS_NAME;

            if (frmMain.selectedRepID == -1)
                return;

            if (access.mudVolumeManagement_Pit_TankVolume == RoleAccess.AcessTypes.AT_None)
                return;

            LoadTotalWellVol();
        }
        //-------------------------------------------------------
        private void dgvTotalWellVol_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //string METHOD = "dgvTotalWellVol_CellMouseClick : " + CLASS_NAME;

            if (frmMain.selectedRepID == -1)
                return;

            if (access.mudVolumeManagement_Pit_TankVolume != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;

            if (e.ColumnIndex > 0 && e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 5)//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                {
                    //~~~~~~~~~~~~~~
                    ProjectForm prjForm = (ProjectForm)mainForm.subForms[mainForm.PorjectFormID];
                    string selUnitVol = prjForm.lblUnitSelectedLiquidVolume.Tag.ToString();
                    //~~~~~~~~~~~~~~

                    Int64 baseDfsID = Convert.ToInt64(DGV_Operations.CellValueAsString(dgvTotalWellVol.Rows[e.RowIndex].Cells[0]));

                    MudVolManTotalWellMergeForm frmMerge = new MudVolManTotalWellMergeForm(baseDfsID, selUnitVol);

                    frmMerge.ShowDialog();

                    //refresh
                    LoadTotalWellVol();
                }
            }
        }










        //-------------------------------------------------------

    }
}
