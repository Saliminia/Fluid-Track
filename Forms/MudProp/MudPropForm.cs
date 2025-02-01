using DMR.User_DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace DMR
{
    public partial class MudPropForm : Form, IMyForm
    {
        const string CLASS_NAME = "MudPropForm";
        RoleAccess access = null;
        frmMain mainForm = null;

        bool skipCellValueChanged_dgvMud = false;
        bool skipCellValueChanged_dgvWater = false;
        bool skipPainting_dgvWater = false;

        List<string> waterTypesList = new List<string>();

        string curMudPropPeriod = "00-06";
        List<bool[]> mudPropChangedCells = new List<bool[]>();//column by column

        public static decimal INVALID_VALUE = -999999999999;
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
                if (!LoadMudProperties(curMudPropPeriod))
                {
                    InformationManager.Set_Info(new Errors("Can not load data from data base", "", METHOD));
                    //?????logout????
                    return;
                }

                if (!LoadWater())
                {
                    InformationManager.Set_Info(new Errors("Can not load data from data base", "", METHOD));
                    //?????logout????
                    return;
                }
            }

            ResetApplyBtns();
        }
        //-------------------------------------------------------
        void AddMudPropertyRow(string header)
        {
            AddMudPropertyRow(header, true);
        }
        //-------------------------------------------------------
        void AddMudPropertyRow(string header, bool isInMudPropertiesTable)
        {
            dgvMudProp.Rows.Add();
            dgvMudProp.Rows[dgvMudProp.Rows.Count - 1].HeaderCell.Value = header;

            if (isInMudPropertiesTable)
                dgvMudProp.Rows[dgvMudProp.Rows.Count - 1].Tag = "y";
            else
                dgvMudProp.Rows[dgvMudProp.Rows.Count - 1].Tag = "n";

        }
        //-------------------------------------------------------
        void AddMudPropertyRowAsReadOnly(string header)
        {
            AddMudPropertyRow(header);
            dgvMudProp.Rows[dgvMudProp.Rows.Count - 1].ReadOnly = true;
            dgvMudProp.Rows[dgvMudProp.Rows.Count - 1].Tag = "n";
        }
        //-------------------------------------------------------
        void AddWaterRow(string header)
        {
            dgvWater.Rows.Add();
            dgvWater.Rows[dgvWater.Rows.Count - 1].HeaderCell.Value = header;
        }
        //-------------------------------------------------------
        void AddWaterRowAsReadOnly(string header)
        {
            AddWaterRow(header);
            dgvWater.Rows[dgvWater.Rows.Count - 1].ReadOnly = true;
        }
        //-------------------------------------------------------
        void IMyForm.OnInit()
        {
            //~~~~~~~~~~~~~~~~~ AT_None = Hide ~~~~~~~~~~~~~~~~~~~~
            if (access.mudProperties_MudProperties == RoleAccess.AcessTypes.AT_None)
            {
                tcMudProp.TabPages.Remove(tpMudProp);
            }
            else if (access.mudProperties_MudProperties == RoleAccess.AcessTypes.AT_ReadOnly)
            {
                dgvMudProp.ReadOnly = true;
                rbtnPeriod00_06.Enabled = false;
                rbtnPeriod06_12.Enabled = false;
                rbtnPeriod12_18.Enabled = false;
                rbtnPeriod18_24.Enabled = false;
                btnMudPropAdd.Enabled = false;
                btnMudPropApply.Enabled = false;
                btnMudPropRemove.Enabled = false;
            }

            DGV_Operations.ColorizeFixedColumnHeader(dgvMudProp, 1);
            DGV_Operations.HandleKeyDown(dgvMudProp, dgvMudProp_CellMouseClick);


            if (access.mudProperties_Water == RoleAccess.AcessTypes.AT_None)
            {
                tcMudProp.TabPages.Remove(tpWater);
            }
            else if (access.mudProperties_Water == RoleAccess.AcessTypes.AT_ReadOnly)
            {
                dgvWater.ReadOnly = true;

                btnWaterAdd.Enabled = false;
                btnWaterApply.Enabled = false;
                btnWaterRemove.Enabled = false;
            }
            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

            CleanVolatileParts();
            ResetApplyBtns();
            //~~~~~~~~~~~~~~~~~~~~
            AddMudPropertyRow("Check Time", false);//#0#
            AddMudPropertyRowAsReadOnly("Sample From"); // Project Pits +  "Flow Line"
            AddMudPropertyRowAsReadOnly("Drilling Fluid System");
            AddMudPropertyRow("Depth / MD");
            AddMudPropertyRow("Depth / TVD");
            AddMudPropertyRow("Bit Depth");//#5#
            AddMudPropertyRow("Flow line Temp.");
            AddMudPropertyRow("Rheology Temp.");
            AddMudPropertyRow("Density");
            AddMudPropertyRow("Funnel Vis.");
            AddMudPropertyRow("R600");//#10#
            AddMudPropertyRow("R300");
            AddMudPropertyRow("R200");
            AddMudPropertyRow("R100");
            AddMudPropertyRow("R6");
            AddMudPropertyRow("R3");//#15#
            AddMudPropertyRow("Gel 10\'\'");
            AddMudPropertyRow("Gel 10\'");
            AddMudPropertyRow("Gel 30\'");
            AddMudPropertyRowAsReadOnly("PV");
            AddMudPropertyRowAsReadOnly("YP");//#20#
            AddMudPropertyRow("API FL");
            AddMudPropertyRow("HPHT FL");
            AddMudPropertyRow("Filter Cake Thickness");
            AddMudPropertyRow("Cracking Cake", false);
            AddMudPropertyRow("Total Chlorides");//#25#
            AddMudPropertyRow("KCl");//input (fixed unit:wt%)          
            AddMudPropertyRowAsReadOnly("KCl");//computation (selectable unit)
            AddMudPropertyRowAsReadOnly("KCl Chloride");
            AddMudPropertyRowAsReadOnly("NaCl");
            AddMudPropertyRowAsReadOnly("NaCl Chloride");//#30#
            AddMudPropertyRow("Total Hardness");
            AddMudPropertyRow("Ca++");
            AddMudPropertyRowAsReadOnly("Mg++");
            AddMudPropertyRow("Drill Solids CEC");
            AddMudPropertyRow("Mud MBT");//#35#
            AddMudPropertyRow("pH");
            AddMudPropertyRow("Alkal Mud (Pm)");
            AddMudPropertyRow("Pf");
            AddMudPropertyRow("Mf");
            AddMudPropertyRowAsReadOnly("Bicarbonate (HCO3)");//#40#
            AddMudPropertyRowAsReadOnly("Carbonate (CO3)");
            AddMudPropertyRowAsReadOnly("Hydroxyl (OH)");
            AddMudPropertyRowAsReadOnly("Lime");
            AddMudPropertyRow("Lubricity Factor(kf)");
            AddMudPropertyRow("Gas Type", false);//#45#
            AddMudPropertyRow("Gas");
            AddMudPropertyRow("Water");
            AddMudPropertyRow("Oil/Lubricant");
            AddMudPropertyRow("Glycol");
            AddMudPropertyRow("Sand Content");//#50#
            AddMudPropertyRow("Es (Electrical Stability)");
            AddMudPropertyRow("Oil Water Ratio");
            AddMudPropertyRow("Aniline Point");

            dgvMudProp.TopLeftHeaderCell.Value = "System";
            dgvMudProp.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);

            //~~~~~~~~~~~~~~~~~~~~
            AddWaterRow("Vol (bbl)");
            AddWaterRow("Cl (mg/lit)");
            AddWaterRow("Total Hardness (mg/lit)");
            AddWaterRow("Ca++ (mg/lit)");
            AddWaterRowAsReadOnly("Mg++ (mg/lit)");
            AddWaterRow("pH");

            dgvWater.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
        }
        //-------------------------------------------------------
        void IMyForm.OnCurrentUnitsChanged()
        {
            //~~~~~~~~~~~~~~
            ProjectForm prjForm = (ProjectForm)mainForm.subForms[mainForm.PorjectFormID];
            //string selUnitDepth= prjForm.lblUnitSelectedDepth.Tag.ToString();
            string selUnitVol = prjForm.lblUnitSelectedLiquidVolume.Tag.ToString();
            string selUnitPlasticVis = prjForm.lblUnitSelectedPlasticViscosity.Tag.ToString();
            string selUnitIonic = prjForm.lblUnitSelectedIonicMassConcentration.Tag.ToString();
            string selUnitKClCon = prjForm.lblUnitSelectedKClConcentration.Tag.ToString();
            string selUnitNaClCon = prjForm.lblUnitSelectedNaClConcentration.Tag.ToString();
            string selUnitPmPfMf = prjForm.lblUnitSelectedPmPfMf.Tag.ToString();
            //string selUnitMW = prjForm.lblUnitSelectedMudWeight.Tag.ToString();
            //string selUnitWeight = prjForm.lblUnitSelectedWeight.Tag.ToString();
            //string selUnitDischarge = prjForm.lblUnitSelectedDischarge.Tag.ToString();
            //string selUnitVolPerStroke = prjForm.lblUnitSelectedVolumeStroke.Tag.ToString();
            //string selUnitFlowRate = prjForm.lblUnitSelectedCirculationRate.Tag.ToString();
            //string selUnitFlowRate = prjForm.lblUnitSelectedFlowRate.Tag.ToString();
            //string selUnitRop = prjForm.lblUnitSelectedROP.Tag.ToString();
            //string selUnitTorque = prjForm.lblUnitSelectedTorque.Tag.ToString();
            string selUnitYieldPointAndGel = prjForm.lblUnitSelectedYieldPointAndGelStrength.Tag.ToString();
            string selUnitMudMBT = prjForm.lblUnitSelectedMudMBT.Tag.ToString();
            string selUnitPowderMatConc = prjForm.lblUnitSelectedPowderMaterialConcentration.Tag.ToString();
            //~~~~~~~~~~~~~~

            //Mud Properties ~~~~~~~~~~~~~~
            if (frmMain.selectedPrjID == -1)
            {
                for (int i = 2; i < dgvMudProp.Rows.Count; i++)
                    dgvMudProp[1, i].Value = "";
            }
            else
            {
                for (int i = 3; i < dgvMudProp.Rows.Count; i++)
                    dgvMudProp[1, i].Value = FetchTableData.GetSelectedUnitForMudProperty(frmMain.selectedPrjID, dgvMudProp.Rows[i].HeaderCell.Value.ToString());

                //set correct unit names for rows where mud property name is not same as DB or ...
                dgvMudProp[1, 19].Value = selUnitPlasticVis;//PV
                dgvMudProp[1, 20].Value = selUnitYieldPointAndGel;//YP

                dgvMudProp[1, 24].Value = "";//Cracking Cake
                dgvMudProp[1, 26].Value = "wt%";//KCl (fixed unit:wt%) 
                dgvMudProp[1, 27].Value = selUnitKClCon;//KCl 
                dgvMudProp[1, 28].Value = selUnitIonic;//KCl Chloride
                dgvMudProp[1, 29].Value = selUnitNaClCon;//NaCl
                dgvMudProp[1, 30].Value = selUnitIonic;//NaCl Chloride
                dgvMudProp[1, 33].Value = selUnitIonic;//Mg++
                dgvMudProp[1, 34].Value = "meq/100 gr";//Drill Solids CEC
                dgvMudProp[1, 35].Value = selUnitMudMBT;//Mud MBT
                dgvMudProp[1, 36].Value = "";//pH
                dgvMudProp[1, 40].Value = selUnitIonic;//Bicarbonate (HCO3)
                dgvMudProp[1, 41].Value = selUnitIonic;//Carbonate (CO3)
                dgvMudProp[1, 42].Value = selUnitIonic;//Hydroxyl (OH)
                dgvMudProp[1, 43].Value = selUnitPowderMatConc;//Lime
                dgvMudProp[1, 44].Value = "kf";//Lubricity Factor(kf)
                dgvMudProp[1, 45].Value = "";//Gas Type
                dgvMudProp[1, 46].Value = "vol%";//Gas

                dgvMudProp[1, 47].Value = "vol%";//Water
                dgvMudProp[1, 48].Value = "vol%";//Oil/Lubricant
                dgvMudProp[1, 49].Value = "vol%";//Glycol
                dgvMudProp[1, 50].Value = "vol%";//Sand Content

                dgvMudProp[1, 51].Value = "volt.";//Es (Electrical Stability)
                dgvMudProp[1, 52].Value = "";//Oil Water Ratio
            }

            //Water ~~~~~~~~~~~~~~
            UnitString.WriteUnit(dgvWater.Rows[0], selUnitVol);
            UnitString.WriteUnit(dgvWater.Rows[1], selUnitIonic);
            UnitString.WriteUnit(dgvWater.Rows[2], selUnitIonic);
            UnitString.WriteUnit(dgvWater.Rows[3], selUnitIonic);
            UnitString.WriteUnit(dgvWater.Rows[4], selUnitIonic);

            UpdateComputationsByPressRefereshButtons();
        }
        //-------------------------------------------------------
        void UpdateComputationsByPressRefereshButtons()
        {
            //btnMudPropRefresh_Click(null, null);
            //btnWaterRefresh_Click(null, null);
        }
        //-------------------------------------------------------
        public MudPropForm(RoleAccess access, frmMain mainForm)
        {
            this.access = access;
            this.mainForm = mainForm;

            InitializeComponent();
        }
        //-------------------------------------------------------
        private void MudPropForm_Load(object sender, EventArgs e)
        {
            this.dgvWater.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dgvWater.ColumnHeadersHeight = this.dgvWater.ColumnHeadersHeight * 2;
            this.dgvWater.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;

            this.dgvWater.CellPainting += new DataGridViewCellPaintingEventHandler(dgvWater_CellPainting);
            this.dgvWater.Paint += new PaintEventHandler(dgvWater_Paint);
            this.dgvWater.Scroll += new ScrollEventHandler(dgvWater_Scroll);
            this.dgvWater.ColumnWidthChanged += new DataGridViewColumnEventHandler(dgvWater_ColumnWidthChanged);
        }
        //-------------------------------------------------------
        private void dgvWater_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            Rectangle rtHeader = this.dgvWater.DisplayRectangle;
            rtHeader.Height = this.dgvWater.ColumnHeadersHeight / 2;
            this.dgvWater.Invalidate(rtHeader);
        }
        //-------------------------------------------------------
        private void dgvWater_Scroll(object sender, ScrollEventArgs e)
        {
            Rectangle rtHeader = this.dgvWater.DisplayRectangle;
            rtHeader.Height = this.dgvWater.ColumnHeadersHeight / 2;
            this.dgvWater.Invalidate(rtHeader);
        }
        //-------------------------------------------------------
        private void dgvWater_Paint(object sender, PaintEventArgs e)
        {
            if (skipPainting_dgvWater)
                return;

            for (int j = 0; j < waterTypesList.Count; j++)
            {
                Rectangle r1 = this.dgvWater.GetCellDisplayRectangle(j * 2 + 1, -1, true);
                int w2 = this.dgvWater.GetCellDisplayRectangle(j * 2 + 2, -1, true).Width;
                r1.X += 1;
                r1.Y += 1;
                r1.Width = r1.Width + w2 - 2;
                r1.Height = r1.Height / 2 - 2;
                e.Graphics.FillRectangle(new SolidBrush(this.dgvWater.ColumnHeadersDefaultCellStyle.BackColor), r1);

                StringFormat format = new StringFormat();
                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Center;
                e.Graphics.DrawString(waterTypesList[j],

                this.dgvWater.ColumnHeadersDefaultCellStyle.Font,
                new SolidBrush(this.dgvWater.ColumnHeadersDefaultCellStyle.ForeColor),
                r1, format);
            }
        }
        //-------------------------------------------------------
        private void dgvWater_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex > -1)
            {
                Rectangle r2 = e.CellBounds;
                r2.Y += e.CellBounds.Height / 2;
                r2.Height = e.CellBounds.Height / 2;
                e.PaintBackground(r2, true);
                e.PaintContent(r2);
                e.Handled = true;
            }
        }
        //-------------------------------------------------------
        void CleanVolatileParts()
        {
            while (dgvMudProp.Columns.Count > 2)//remove all columns but first two ones (Indices 0, 1)
                dgvMudProp.Columns.RemoveAt(2);

            skipPainting_dgvWater = true;

            while (dgvWater.Columns.Count > 1)//remove all columns but first
                dgvWater.Columns.RemoveAt(1);

            skipPainting_dgvWater = false;
        }
        //-------------------------------------------------------
        void ResetApplyBtns()
        {
            btnMudPropApply.ImageKey = "check";
            btnMudPropApply.BackColor = frmMain.checkColor;
            btnWaterApply.ImageKey = "check";
            btnWaterApply.BackColor = frmMain.checkColor;
        }
        //-------------------------------------------------------
        void ComputeMudPropertiesNumbers(int gridColumnID)
        {
            try
            {
                Helpers.Computation.MudProperties mudComp = new Helpers.Computation.MudProperties();
                //~~~~~~~~~~~~~~
                ProjectForm prjForm = (ProjectForm)mainForm.subForms[mainForm.PorjectFormID];
                mudComp.in_selUnitVol = prjForm.lblUnitSelectedLiquidVolume.Tag.ToString();
                mudComp.in_selUnitPlasticVis = prjForm.lblUnitSelectedPlasticViscosity.Tag.ToString();
                mudComp.in_selUnitYieldPoint = prjForm.lblUnitSelectedYieldPointAndGelStrength.Tag.ToString();
                mudComp.in_selUnitIonic = prjForm.lblUnitSelectedIonicMassConcentration.Tag.ToString();
                mudComp.in_selUnitKClCon = prjForm.lblUnitSelectedKClConcentration.Tag.ToString();
                mudComp.in_selUnitNaClCon = prjForm.lblUnitSelectedNaClConcentration.Tag.ToString();
                mudComp.in_selUnitPmPfMf = prjForm.lblUnitSelectedPmPfMf.Tag.ToString();
                mudComp.in_selUnitPowderMatConc = prjForm.lblUnitSelectedPowderMaterialConcentration.Tag.ToString();
                //~~~~~~~~~~~~~~
                AdvancedConvertor.ToDecimal(DGV_Operations.CellValueAsString(dgvMudProp[gridColumnID, 10]), ref mudComp.in_R600);
                AdvancedConvertor.ToDecimal(DGV_Operations.CellValueAsString(dgvMudProp[gridColumnID, 11]), ref mudComp.in_R300);
                AdvancedConvertor.ToDecimal(DGV_Operations.CellValueAsString(dgvMudProp[gridColumnID, 26]), ref mudComp.in_KClwt);
                AdvancedConvertor.ToDecimal(DGV_Operations.CellValueAsString(dgvMudProp[gridColumnID, 38]), ref mudComp.in_Pf);
                AdvancedConvertor.ToDecimal(DGV_Operations.CellValueAsString(dgvMudProp[gridColumnID, 39]), ref mudComp.in_Mf);
                AdvancedConvertor.ToDecimal(DGV_Operations.CellValueAsString(dgvMudProp[gridColumnID, 25]), ref mudComp.in_TotalCh);
                AdvancedConvertor.ToDecimal(DGV_Operations.CellValueAsString(dgvMudProp[gridColumnID, 31]), ref mudComp.in_TotalH);
                AdvancedConvertor.ToDecimal(DGV_Operations.CellValueAsString(dgvMudProp[gridColumnID, 32]), ref mudComp.in_Capp);
                AdvancedConvertor.ToDecimal(DGV_Operations.CellValueAsString(dgvMudProp[gridColumnID, 37]), ref mudComp.in_Pm);
                AdvancedConvertor.ToDecimal(DGV_Operations.CellValueAsString(dgvMudProp[gridColumnID, 47]), ref mudComp.in_water);
                //~~~~~~~~~~~~~~
                dgvMudProp[gridColumnID, 19].Tag = null; dgvMudProp[gridColumnID, 19].Value = "!!!";
                dgvMudProp[gridColumnID, 20].Tag = null; dgvMudProp[gridColumnID, 20].Value = "!!!";
                dgvMudProp[gridColumnID, 27].Tag = null; dgvMudProp[gridColumnID, 27].Value = "!!!";
                dgvMudProp[gridColumnID, 28].Tag = null; dgvMudProp[gridColumnID, 28].Value = "!!!";
                dgvMudProp[gridColumnID, 29].Tag = null; dgvMudProp[gridColumnID, 29].Value = "!!!";
                dgvMudProp[gridColumnID, 30].Tag = null; dgvMudProp[gridColumnID, 30].Value = "!!!";
                dgvMudProp[gridColumnID, 33].Tag = null; dgvMudProp[gridColumnID, 33].Value = "!!!";
                dgvMudProp[gridColumnID, 40].Tag = null; dgvMudProp[gridColumnID, 40].Value = "!!!";
                dgvMudProp[gridColumnID, 41].Tag = null; dgvMudProp[gridColumnID, 41].Value = "!!!";
                dgvMudProp[gridColumnID, 42].Tag = null; dgvMudProp[gridColumnID, 42].Value = "!!!";
                dgvMudProp[gridColumnID, 43].Tag = null; dgvMudProp[gridColumnID, 43].Value = "!!!";
                //~~~~~~~~~~~~~~
                //do not return on error => show any computed value till now
                mudComp.Compute();
                //~~~~~~~~~~~~~~
                dgvMudProp[gridColumnID, 19].Tag = mudComp.out_PV; dgvMudProp[gridColumnID, 19].Value = Convert.ToDecimal(mudComp.out_PV).ToString("0.###");
                dgvMudProp[gridColumnID, 20].Tag = mudComp.out_YP; dgvMudProp[gridColumnID, 20].Value = Convert.ToDecimal(mudComp.out_YP).ToString("0.###");
                dgvMudProp[gridColumnID, 27].Tag = mudComp.out_KCl; dgvMudProp[gridColumnID, 27].Value = Convert.ToDecimal(mudComp.out_KCl).ToString("0.###");
                dgvMudProp[gridColumnID, 28].Tag = mudComp.out_KClCh; dgvMudProp[gridColumnID, 28].Value = Convert.ToDecimal(mudComp.out_KClCh).ToString("0.###");
                dgvMudProp[gridColumnID, 30].Tag = mudComp.out_NaClCh; dgvMudProp[gridColumnID, 30].Value = Convert.ToDecimal(mudComp.out_NaClCh).ToString("0.###");
                dgvMudProp[gridColumnID, 29].Tag = mudComp.out_NaCl; dgvMudProp[gridColumnID, 29].Value = Convert.ToDecimal(mudComp.out_NaCl).ToString("0.###");
                dgvMudProp[gridColumnID, 33].Tag = mudComp.out_Mgpp; dgvMudProp[gridColumnID, 33].Value = Convert.ToDecimal(mudComp.out_Mgpp).ToString("0.###");
                dgvMudProp[gridColumnID, 40].Tag = mudComp.out_Bicabonate; dgvMudProp[gridColumnID, 40].Value = Convert.ToDecimal(mudComp.out_Bicabonate).ToString("0.###");
                dgvMudProp[gridColumnID, 41].Tag = mudComp.out_Carbonate; dgvMudProp[gridColumnID, 41].Value = Convert.ToDecimal(mudComp.out_Carbonate).ToString("0.###");
                dgvMudProp[gridColumnID, 42].Tag = mudComp.out_Hydroxyl; dgvMudProp[gridColumnID, 42].Value = Convert.ToDecimal(mudComp.out_Hydroxyl).ToString("0.###");
                dgvMudProp[gridColumnID, 43].Tag = mudComp.out_LimeLBL; dgvMudProp[gridColumnID, 43].Value = Convert.ToDecimal(mudComp.out_LimeLBL).ToString("0.###");
            }
            catch (Exception)
            {
            }
        }
        //-------------------------------------------------------
        //returns:   true (success) , false(failure)
        bool LoadMudProperties(string timePeriod)
        {
            //string METHOD = "LoadMudProperties : " + CLASS_NAME;

            skipCellValueChanged_dgvMud = true;

            while (dgvMudProp.Columns.Count > 2)//remove all columns but first two ones (Indices 0, 1)
                dgvMudProp.Columns.RemoveAt(2);

            mudPropChangedCells.Clear();

            try
            {
                //-----------------------------------
                int colCount = 0;
                const int mudPropCount = 38;
                //-----------------------------------
                {
                    string query =
                          " select rt_Rep2MudProp_MudPropCol.ID, CheckTime, samplePit_ID, Dfs_AutoID, d.DrillingFluidSystem, CrackingCake, GasType "
                        + " from "
                        + " rt_Rep2MudProp_MudPropCol join rt_Rep2MudPropPeriod on rt_Rep2MudPropPeriod.ID = rt_Rep2MudProp_MudPropCol.MudPropPeriod_ID "
                        + " left join lt_DrillingFluidSystem d on rt_Rep2MudProp_MudPropCol.Dfs_AutoID = d.AutoID "
                        + " where  "
                        + " rt_Rep2MudPropPeriod.TimesPeriod = \'" + timePeriod.Replace("\'", "\'\'") + "\'"
                        + " and rt_Rep2MudPropPeriod.ReportID = " + frmMain.selectedRepID.ToString()
                        + " order by rt_Rep2MudProp_MudPropCol.UserOrder ";

                    DataSet ds = ConnectionManager.ExecQuery(query);

                    if (ds != null)
                    {
                        colCount = ds.Tables[0].Rows.Count;

                        for (int c = 0; c < colCount + 2; c++)
                        {
                            bool[] entries = new bool[mudPropCount + 16];// entry for all rows (for simplicity)

                            for (int e = 0; e < entries.Length; e++)
                                entries[e] = false;

                            mudPropChangedCells.Add(entries);
                        }

                        for (int i = 0; i < colCount; i++)
                        {
                            DataGridViewTextBoxColumn newCol = new DataGridViewTextBoxColumn();
                            newCol.SortMode = DataGridViewColumnSortMode.Programmatic;
                            newCol.Name = "mudPropCol" + i.ToString();
                            newCol.HeaderText = (i + 1).ToString();
                            newCol.Tag = ds.Tables[0].Rows[i][0];
                            dgvMudProp.Columns.Add(newCol);

                            //--------
                            if (ds.Tables[0].Rows[i][3] != DBNull.Value)
                            {
                                dgvMudProp.Rows[2].Cells[i + 2].Value = ds.Tables[0].Rows[i][4].ToString();
                                dgvMudProp.Rows[2].Cells[i + 2].Tag = ds.Tables[0].Rows[i][3].ToString();
                            }
                            else
                            {
                                dgvMudProp.Rows[2].Cells[i + 2].Value = "";
                                dgvMudProp.Rows[2].Cells[i + 2].Tag = null;
                            }
                            //--------
                            if (ds.Tables[0].Rows[i][2] == DBNull.Value)
                            {
                                dgvMudProp.Rows[1].Cells[i + 2].Value = "Flow Line";
                                dgvMudProp.Rows[1].Cells[i + 2].Tag = null;
                            }
                            else
                            {
                                Int64 sampPitID = Convert.ToInt64(ds.Tables[0].Rows[i][2]);

                                int sampPitNum = 0;
                                string pitName;
                                FetchTableData.GetPitNameAndNumber(sampPitID, out pitName, out sampPitNum);

                                string pitNumAndName = pitName + " #" + sampPitNum.ToString();

                                dgvMudProp.Rows[1].Cells[i + 2].Value = pitNumAndName;
                                dgvMudProp.Rows[1].Cells[i + 2].Tag = sampPitID;
                            }


                            //--------
                            dgvMudProp.Rows[0].Cells[i + 2].Value = ds.Tables[0].Rows[i][1].ToString();//CheckTime
                            dgvMudProp.Rows[24].Cells[i + 2].Value = ds.Tables[0].Rows[i][5].ToString();//CrackingCake
                            dgvMudProp.Rows[45].Cells[i + 2].Value = ds.Tables[0].Rows[i][6].ToString();//GasType
                            //--------
                        }

                        ds.Dispose();
                    }
                    else
                    {
                        skipCellValueChanged_dgvMud = false;
                        return false;
                    }
                }
                //-----------------------------------
                {
                    string query =
                          " select rt_Rep2MudProp_MudPropRow.ID, rt_Rep2MudProp_MudPropRow.Value "
                        + " from "
                        + " ft_MudProperties join rt_Rep2MudProp_MudPropRow on ft_MudProperties.MudPropName = rt_Rep2MudProp_MudPropRow.MudPropName "
                        + " join rt_Rep2MudProp_MudPropCol on rt_Rep2MudProp_MudPropCol.ID = rt_Rep2MudProp_MudPropRow.Column_ID "
                        + " join rt_Rep2MudPropPeriod on rt_Rep2MudPropPeriod.ID =  rt_Rep2MudProp_MudPropCol.MudPropPeriod_ID "
                        + " where  "
                        + " rt_Rep2MudPropPeriod.TimesPeriod = \'" + timePeriod.Replace("\'", "\'\'") + "\'"
                        + " and rt_Rep2MudPropPeriod.ReportID = " + frmMain.selectedRepID.ToString()
                        + " order by ft_MudProperties.UserOrder, rt_Rep2MudProp_MudPropCol.UserOrder ";

                    DataSet ds = ConnectionManager.ExecQuery(query);

                    if (ds != null)
                    {
                        int id = 0;
                        for (int j = 0; j < dgvMudProp.Rows.Count - 3; j++)
                        {
                            if (dgvMudProp.Rows[j + 3].Tag.ToString() == "y")
                            {
                                for (int i = 0; i < colCount; i++, id++)
                                {
                                    decimal value = Convert.ToDecimal(ds.Tables[0].Rows[id][1]);

                                    dgvMudProp.Rows[j + 3].Cells[i + 2].Tag = ds.Tables[0].Rows[id][0].ToString(); //id
                                    dgvMudProp.Rows[j + 3].Cells[i + 2].Value = (value == INVALID_VALUE) ? "" : value.ToString(); //value
                                }
                            }
                        }

                        for (int i = 0; i < colCount; i++)
                            ComputeMudPropertiesNumbers(i + 2);

                        ds.Dispose();
                    }
                    else
                    {
                        skipCellValueChanged_dgvMud = false;
                        return false;
                    }
                }

                skipCellValueChanged_dgvMud = false;
                return true;

            }
            catch (Exception)
            {
                //ErrorManager.Set_Error(new Errors("Can not load data from data base", ex.Message, METHOD));
                //will be handled outside
            }

            skipCellValueChanged_dgvMud = false;
            return false;
        }
        //-------------------------------------------------------
        void ComputeWaterNumbers(int columnID)
        {
            try
            {
                //~~~~~~~~~~~~~~
                ProjectForm prjForm = (ProjectForm)mainForm.subForms[mainForm.PorjectFormID];
                string selUnitIonic = prjForm.lblUnitSelectedIonicMassConcentration.Tag.ToString();
                //~~~~~~~~~~~~~~

                decimal TotalH = 0, Capp = 0;
                if (AdvancedConvertor.ToDecimal(DGV_Operations.CellValueAsString(dgvWater[columnID, 2]), ref TotalH) &&
                    AdvancedConvertor.ToDecimal(DGV_Operations.CellValueAsString(dgvWater[columnID, 3]), ref Capp))
                {
                    AdvancedConvertor.ToDecimal(UnitConverter.Convert("Ionic Mass Concentration", selUnitIonic, "mg/l", TotalH.ToString()), ref TotalH);
                    AdvancedConvertor.ToDecimal(UnitConverter.Convert("Ionic Mass Concentration", selUnitIonic, "mg/l", Capp.ToString()), ref Capp);

                    decimal Mgpp = (TotalH - Capp) * (243.0m / 400);//  in mg/l
                    dgvWater[columnID, 4].Tag = UnitConverter.Convert("Ionic Mass Concentration", "mg/l", selUnitIonic, Mgpp.ToString());
                    dgvWater[columnID, 4].Value = Convert.ToDecimal(dgvWater[columnID, 4].Tag).ToString("0.###");
                }
                else
                {
                    dgvWater[columnID, 4].Tag = null;
                    dgvWater[columnID, 4].Value = "Error";
                }
            }
            catch (Exception)
            {
            }
        }
        //-------------------------------------------------------
        //returns:   true (success) , false(failure)
        bool LoadWater()
        {
            //string METHOD = "LoadWater : " + CLASS_NAME;

            skipCellValueChanged_dgvWater = true;
            skipPainting_dgvWater = true;

            while (dgvWater.Columns.Count > 1)//remove all columns but first
                dgvWater.Columns.RemoveAt(1);

            try
            {
                string query =
                       " select w.ID, Vol, CL_B, TotalH_B, Ca_B, pH_B, CL_A, TotalH_A, Ca_A, pH_A, p.Value "
                     + " from rt_Rep2MudPropWater w left join lt_PredefValues p on w.WaterType_PredefAutoID = p.AutoID where "
                     + " ReportID = " + frmMain.selectedRepID.ToString()
                     + " order by  w.ID ";

                DataSet ds = ConnectionManager.ExecQuery(query);

                if (ds != null)
                {
                    waterTypesList.Clear();

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {

                        DataGridViewTextBoxColumn newColBefore = new DataGridViewTextBoxColumn();
                        newColBefore.SortMode = DataGridViewColumnSortMode.Programmatic;
                        newColBefore.Name = "mudWaterColBefore" + i.ToString();
                        newColBefore.HeaderText = "Before";
                        newColBefore.Tag = ds.Tables[0].Rows[i][0];
                        dgvWater.Columns.Add(newColBefore);

                        DataGridViewTextBoxColumn newColAfter = new DataGridViewTextBoxColumn();
                        newColAfter.SortMode = DataGridViewColumnSortMode.Programmatic;
                        newColAfter.Name = "mudWaterColAfter" + i.ToString();
                        newColAfter.HeaderText = "After";
                        newColAfter.Tag = ds.Tables[0].Rows[i][0];
                        dgvWater.Columns.Add(newColAfter);

                        dgvWater[i * 2 + 1, 0].Value = ds.Tables[0].Rows[i][1].ToString();//Vol
                        dgvWater[i * 2 + 1, 1].Value = ds.Tables[0].Rows[i][2].ToString();//CL_B
                        dgvWater[i * 2 + 1, 2].Value = ds.Tables[0].Rows[i][3].ToString();//TotalH_B
                        dgvWater[i * 2 + 1, 3].Value = ds.Tables[0].Rows[i][4].ToString();//Ca_B
                        dgvWater[i * 2 + 1, 5].Value = ds.Tables[0].Rows[i][5].ToString();//pH_B

                        dgvWater[i * 2 + 2, 0].Value = "";
                        dgvWater[i * 2 + 2, 0].ReadOnly = true;
                        dgvWater[i * 2 + 2, 0].Style.BackColor = Color.Gray;

                        dgvWater[i * 2 + 2, 1].Value = ds.Tables[0].Rows[i][6].ToString();//CL_A
                        dgvWater[i * 2 + 2, 2].Value = ds.Tables[0].Rows[i][7].ToString();//TotalH_A
                        dgvWater[i * 2 + 2, 3].Value = ds.Tables[0].Rows[i][8].ToString();//Ca_A
                        dgvWater[i * 2 + 2, 5].Value = ds.Tables[0].Rows[i][9].ToString();//pH_A

                        waterTypesList.Add(ds.Tables[0].Rows[i][10].ToString());

                        //Mg++
                        ComputeWaterNumbers(i * 2 + 1);
                        ComputeWaterNumbers(i * 2 + 2);
                    }

                    ds.Dispose();

                    skipCellValueChanged_dgvWater = false;
                    skipPainting_dgvWater = false;
                    return true;
                }
                else
                {
                    skipCellValueChanged_dgvWater = false;
                    skipPainting_dgvWater = false;
                    return false;
                }
            }
            catch (Exception)
            {
                //ErrorManager.Set_Error(new Errors("Can not load data from data base", ex.Message, METHOD));
                //will be handled outside
            }

            skipPainting_dgvWater = false;
            skipCellValueChanged_dgvWater = false;
            return false;
        }
        //-------------------------------------------------------
        private void rbtnPeriod_CheckedChanged(object sender, EventArgs e)
        {
            if (frmMain.selectedRepID == -1)
                return;

            if (access.mudProperties_MudProperties != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;

            if (rbtnPeriod00_06.Checked)
                curMudPropPeriod = "00-06";
            else if (rbtnPeriod06_12.Checked)
                curMudPropPeriod = "06-12";
            else if (rbtnPeriod12_18.Checked)
                curMudPropPeriod = "12-18";
            else if (rbtnPeriod18_24.Checked)
                curMudPropPeriod = "18-24";

            if (!LoadMudProperties(curMudPropPeriod))
            {
                //?????logout????
            }

            btnMudPropApply.ImageKey = "check";
            btnMudPropApply.BackColor = frmMain.checkColor;
        }
        //-------------------------------------------------------
        private void btnMudPropRefresh_Click(object sender, EventArgs e)
        {
            string METHOD = "btnMudPropRefresh_Click : " + CLASS_NAME;

            if (frmMain.selectedRepID == -1)
                return;

            if (access.mudProperties_MudProperties == RoleAccess.AcessTypes.AT_None)
                return;

            if (!LoadMudProperties(curMudPropPeriod))
            {
                InformationManager.Set_Info(new Errors("Can not load data from data base", "", METHOD));
                //?????logout????
            }

            btnMudPropApply.ImageKey = "check";
            btnMudPropApply.BackColor = frmMain.checkColor;
        }
        //-------------------------------------------------------
        private void btnMudPropApply_Click(object sender, EventArgs e)
        {
            if (frmMain.selectedRepID == -1)
                return;

            if (access.mudProperties_MudProperties != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;

            try
            {
                List<Errors> errs = new List<Errors>();

                {//Properties Values

                    for (int j = 0; j < dgvMudProp.Rows.Count - 3; j++)
                        if (dgvMudProp.Rows[j + 3].Tag.ToString() == "y")
                        {
                            for (int i = 0; i < dgvMudProp.Columns.Count - 2; i++)
                            {
                                if (mudPropChangedCells[i + 2][j + 3])
                                {
                                    List<ProcedureParam> prms = new List<ProcedureParam>();
                                    prms.Add(new ProcedureParam("@ID", dgvMudProp.Rows[j + 3].Cells[i + 2].Tag.ToString(), ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Record"));

                                    string valueStr = DGV_Operations.CellValueAsString(dgvMudProp.Rows[j + 3].Cells[i + 2]);
                                    if (valueStr.Trim() == "")
                                        prms.Add(new ProcedureParam("@Value", INVALID_VALUE.ToString(), ProcedureParam.ParamType.PT_Decimal, false, "", false, "", ""));
                                    else
                                        prms.Add(new ProcedureParam("@Value", valueStr, ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Value" + " on Row,Col = " + (j + 3 + 1).ToString() + "," + (i + 2).ToString()));

                                    string simpErr;
                                    Errors critErr;
                                    Int64 resultStat;

                                    int ret = Procedures.CallProcedure("prc_rt_Rep2MudProp_MudPropRow_update_Value", prms, out simpErr, out critErr, out resultStat);

                                    Color normalBkColor = ((j + 3) % 2 == 0) ? dgvMudProp.BackgroundColor : dgvMudProp.AlternatingRowsDefaultCellStyle.BackColor;

                                    dgvMudProp.Rows[j + 3].Cells[i + 2].Style.BackColor = normalBkColor;

                                    if (ret != 0)
                                    {
                                        if (ret == 1)
                                        {

                                            if (simpErr.StartsWith("Invalid Value"))
                                                dgvMudProp.Rows[j + 3].Cells[i + 2].Style.BackColor = Color.Red;

                                            critErr = new Errors(simpErr);
                                        }

                                        errs.Add(critErr);
                                    }
                                    else
                                    {
                                        mudPropChangedCells[i + 2][j + 3] = false;
                                    }
                                }
                            }
                        }
                }

                {//DrillingFluidSystem, samplePit, Others
                    for (int i = 0; i < dgvMudProp.Columns.Count - 2; i++)
                    {
                        {//DrillingFluidSystem
                            List<ProcedureParam> prms = new List<ProcedureParam>();
                            prms.Add(new ProcedureParam("@ID", dgvMudProp.Columns[i + 2].Tag.ToString(), ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Record"));

                            string dfs = DGV_Operations.CellValueAsString(dgvMudProp.Rows[2].Cells[i + 2]);

                            if (dfs == "")
                                prms.Add(new ProcedureParam("@Dfs_AutoID", ProcedureParam.ParamType.PT_Int));
                            else
                                prms.Add(new ProcedureParam("@Dfs_AutoID", dgvMudProp.Rows[2].Cells[i + 2].Tag.ToString(), ProcedureParam.ParamType.PT_Int, true, "1", false, "", "Invalid DFS"));

                            string simpErr;
                            Errors critErr;
                            Int64 resultStat;

                            int ret = Procedures.CallProcedure("prc_rt_Rep2MudProp_MudPropCol_update_DFS", prms, out simpErr, out critErr, out resultStat);

                            if (ret != 0)
                            {
                                if (ret == 1)
                                    critErr = new Errors(simpErr);

                                errs.Add(critErr);
                            }
                        }

                        {//samplePit 
                            List<ProcedureParam> prms = new List<ProcedureParam>();
                            prms.Add(new ProcedureParam("@ID", dgvMudProp.Columns[i + 2].Tag.ToString(), ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Record"));

                            if (dgvMudProp.Rows[1].Cells[i + 2].Tag == null)
                            {
                                prms.Add(new ProcedureParam("@samplePitID", ProcedureParam.ParamType.PT_BigInt));
                            }
                            else
                            {
                                Int64 pitID = Convert.ToInt64(dgvMudProp.Rows[1].Cells[i + 2].Tag);
                                prms.Add(new ProcedureParam("@samplePitID", pitID.ToString(), ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Pit"));
                            }

                            string simpErr;
                            Errors critErr;
                            Int64 resultStat;

                            int ret = Procedures.CallProcedure("prc_rt_Rep2MudProp_MudPropCol_update_SamplePit", prms, out simpErr, out critErr, out resultStat);

                            if (ret != 0)
                            {
                                if (ret == 1)
                                    critErr = new Errors(simpErr);

                                errs.Add(critErr);
                            }
                        }



                        {//Others

                            //check CheckTime ----------------------------------
                            Regex regExr = new Regex(@"^(?:(?:[01][0-9]|2[0-3]):[0-5][0-9]|24:00)$");
                            string checkTime = DGV_Operations.CellValueAsString(dgvMudProp[i + 2, 0]).Trim();

                            if (checkTime != "")
                            {
                                if (checkTime.Length != 5 || !regExr.IsMatch(checkTime))
                                {
                                    InformationManager.Set_Info("Check time [" + checkTime + "] format is invalid. Use something like ab:cd in selected interval. Also do not use upper bound of interval.");
                                    checkTime = "";
                                }

                                int minHour = 0, maxHour = 0;

                                if (curMudPropPeriod == "00-06") { minHour = 0; maxHour = 5; }
                                else if (curMudPropPeriod == "06-12") { minHour = 6; maxHour = 11; }
                                else if (curMudPropPeriod == "12-18") { minHour = 12; maxHour = 17; }
                                else if (curMudPropPeriod == "18-24") { minHour = 18; maxHour = 24; }

                                int hour = Convert.ToInt32(checkTime.Substring(0, 2));

                                if (hour < minHour || hour > maxHour)
                                {
                                    InformationManager.Set_Info("Check time [" + checkTime + "] is out of range.");
                                    checkTime = "??:??";
                                    dgvMudProp[i + 2, 0].Value = checkTime;
                                }
                            }
                            // ----------------------------------

                            List<ProcedureParam> prms = new List<ProcedureParam>();
                            prms.Add(new ProcedureParam("@ID", dgvMudProp.Columns[i + 2].Tag.ToString(), ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Record"));
                            prms.Add(new ProcedureParam("@CheckTime", checkTime, 15));
                            prms.Add(new ProcedureParam("@CrackingCake", DGV_Operations.CellValueAsString(dgvMudProp[i + 2, 24]), 50));
                            prms.Add(new ProcedureParam("@GasType", DGV_Operations.CellValueAsString(dgvMudProp[i + 2, 45]), 50));

                            string simpErr;
                            Errors critErr;
                            Int64 resultStat;

                            int ret = Procedures.CallProcedure("prc_rt_Rep2MudProp_MudPropCol_update_Others", prms, out simpErr, out critErr, out resultStat);

                            if (ret != 0)
                            {
                                if (ret == 1)
                                    critErr = new Errors(simpErr);

                                errs.Add(critErr);
                            }
                        }
                    }
                }

                InformationManager.Set_Info(errs.ToArray());

                if (errs.Count == 0)
                {
                    btnMudPropApply.ImageKey = "Check";
                    btnMudPropApply.BackColor = frmMain.checkColor;
                }
            }
            catch (Exception)
            {
            }
        }
        //-------------------------------------------------------
        private void btnMudPropAdd_Click(object sender, EventArgs e)
        {
            string METHOD = "btnMudPropAdd_Click : " + CLASS_NAME;

            if (frmMain.selectedRepID == -1)
                return;

            if (access.mudProperties_MudProperties != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;

            Int64 mudPropID = -1;
            if (!FetchTableData.GetMudPropertiesAutoID(frmMain.selectedRepID, curMudPropPeriod, out mudPropID))
                return;

            try
            {
                Int64 generatedColID = -1;

                {
                    List<ProcedureParam> prms = new List<ProcedureParam>();
                    prms.Add(new ProcedureParam("@MudPropPeriod_ID", mudPropID.ToString(), ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Mud Properties Period"));
                    prms.Add(new ProcedureParam("@ProjAutoID", frmMain.selectedPrjID.ToString(), ProcedureParam.ParamType.PT_Int, true, "1", false, "", "Invalid Project"));

                    string simpErr;
                    Errors critErr;

                    int ret = Procedures.CallProcedure("prc_rt_Rep2MudProp_MudPropCol_insert", prms, out simpErr, out critErr, out generatedColID);

                    if (ret != 0)
                    {
                        if (ret == 1)
                            InformationManager.Set_Info(simpErr);
                        else
                            InformationManager.Set_Info(critErr);

                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                InformationManager.Set_Info(ex.Message);
            }


            if (!LoadMudProperties(curMudPropPeriod))
            {
                InformationManager.Set_Info(new Errors("Can not load data from data base", "", METHOD));
                //?????logout????
            }

            btnMudPropApply.ImageKey = "Check";
            btnMudPropApply.BackColor = frmMain.checkColor;
        }
        //-------------------------------------------------------
        private void btnMudPropRemove_Click(object sender, EventArgs e)
        {
            if (frmMain.selectedRepID == -1)
                return;

            if (access.mudProperties_MudProperties != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;

            if (dgvMudProp.SelectedColumns.Count == 0 || dgvMudProp.SelectedColumns[0].Index < 2)
            {
                InformationManager.Set_Info("Select One Column to Delete");
                return;
            }

            int colID = dgvMudProp.SelectedColumns[0].Index;

            mudPropChangedCells.RemoveAt(colID);

            string idStr = dgvMudProp.Columns[colID].Tag.ToString();

            List<ProcedureParam> prms = new List<ProcedureParam>();
            prms.Add(new ProcedureParam("@ID", idStr, ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Record"));

            string simpErr;
            Errors critErr;
            Int64 resultStat;

            int ret = Procedures.CallProcedure("prc_rt_Rep2MudProp_MudPropCol_delete", prms, out simpErr, out critErr, out resultStat);

            if (ret != 0)
            {
                if (ret == 1)
                    InformationManager.Set_Info(simpErr);
                else
                    InformationManager.Set_Info(critErr);

                return;
            }

            if (!LoadMudProperties(curMudPropPeriod))
            {
                //ErrorManager.Set_Error(new Errors("Can not load data from data base", "", METHOD));
                ////?????logout????
            }

            btnMudPropApply.ImageKey = "check";
            btnMudPropApply.BackColor = frmMain.checkColor;
        }
        //-------------------------------------------------------
        private void txtExactTime_TextChanged(object sender, EventArgs e)
        {
            if (frmMain.selectedRepID == -1)
                return;

            if (access.mudProperties_MudProperties != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;

            btnMudPropApply.ImageKey = "warning";
            btnMudPropApply.BackColor = frmMain.warningColor;
        }
        //-------------------------------------------------------
        private void dgvMudProp_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex == -1)
                return;

            if (skipCellValueChanged_dgvMud)
                return;

            if (e.RowIndex >= 0 && e.ColumnIndex >= 2)
            {

                btnMudPropApply.ImageKey = "warning";
                btnMudPropApply.BackColor = frmMain.warningColor;

                if (dgvMudProp.Rows[e.RowIndex].Tag.ToString() == "y")
                {
                    skipCellValueChanged_dgvMud = true;
                    ComputeMudPropertiesNumbers(e.ColumnIndex);
                    skipCellValueChanged_dgvMud = false;

                    mudPropChangedCells[e.ColumnIndex][e.RowIndex] = true;
                }
            }
        }
        //-------------------------------------------------------
        private void dgvMudProp_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (frmMain.selectedRepID == -1)
                return;

            if (access.mudProperties_MudProperties != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;

            if (e.ColumnIndex > 1 && e.RowIndex >= 0 && e.RowIndex <= 2)
            {
                if (e.RowIndex == 2)//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                {
                    string query =
                        " select 0, DFS_AutoID, MudType, DrillingFluidSystem "
                        + " from lt_DrillingFluidSystem join rt_Rep2MudVolManDFS "
                        + " on lt_DrillingFluidSystem.AutoID = rt_Rep2MudVolManDFS.DFS_AutoID "
                        + " and ReportID = " + frmMain.selectedRepID.ToString();

                    DrillingFluidSystemFormDB_Selection frmDFSel = new DrillingFluidSystemFormDB_Selection(query);
                    frmDFSel.Text = "Selection from Drilling Fluid Systems in Mud Volume Management";

                    frmDFSel.dgvFluidSys.Columns[0].Visible = false;//single selection

                    System.Windows.Forms.DialogResult dgRes = frmDFSel.ShowDialog();

                    if (dgRes == System.Windows.Forms.DialogResult.Cancel)
                        return;

                    if (dgRes == System.Windows.Forms.DialogResult.Ignore)
                    {
                        dgvMudProp.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = DBNull.Value;
                        dgvMudProp.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag = null;
                        btnMudPropApply.ImageKey = "warning";
                        btnMudPropApply.BackColor = frmMain.warningColor;
                        return;
                    }

                    if (frmDFSel.dgvFluidSys.SelectedRows.Count == 0)
                        return;

                    string drillFS = DGV_Operations.CellValueAsString(frmDFSel.dgvFluidSys.Rows[frmDFSel.dgvFluidSys.SelectedRows[0].Index].Cells[3]);
                    string mudType = DGV_Operations.CellValueAsString(frmDFSel.dgvFluidSys.Rows[frmDFSel.dgvFluidSys.SelectedRows[0].Index].Cells[2]);
                    string dfsAutoID = DGV_Operations.CellValueAsString(frmDFSel.dgvFluidSys.Rows[frmDFSel.dgvFluidSys.SelectedRows[0].Index].Cells[1]);

                    dgvMudProp.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = drillFS;
                    dgvMudProp.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag = dfsAutoID;
                    //~~~~~~~~~~~~~~~~~~~~~~
                    btnMudPropApply.ImageKey = "warning";
                    btnMudPropApply.BackColor = frmMain.warningColor;
                }
                else if (e.RowIndex == 1)//%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                {
                    frmMultiInput fm = new frmMultiInput();

                    fm.lblTitle.Text = "Sampling Source:";

                    fm.dgvInput.Columns.Add("ID", "");
                    fm.dgvInput.Columns[0].SortMode = DataGridViewColumnSortMode.Programmatic;
                    fm.dgvInput.Columns[0].Visible = false;

                    fm.dgvInput.Columns.Add("Source", "Pit Number / Flow Line");
                    fm.dgvInput.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    fm.dgvInput.Columns[1].SortMode = DataGridViewColumnSortMode.Programmatic;

                    fm.dgvInput.ReadOnly = true;
                    fm.dgvInput.AllowUserToAddRows = false;
                    fm.dgvInput.AllowUserToDeleteRows = false;
                    fm.dgvInput.RowHeadersVisible = false;
                    fm.dgvInput.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    fm.dgvInput.MultiSelect = false;

                    string query = " select rt_Rig2PitTank.ID, rt_Rig2PitTank.PitNum, p.Value from rt_Rig2PitTank join lt_PredefValues p on PitName_PredefAutoID = p.AutoID "
                        + " where RigID = " + frmMain.selected_RW_RigID.ToString();

                    DataSet ds = ConnectionManager.ExecQuery(query);

                    if (ds != null)
                    {
                        fm.dgvInput.Rows.Add(-1, "Flow Line");

                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            string pitNumAndName = ds.Tables[0].Rows[i][2].ToString() + " #" + ds.Tables[0].Rows[i][1].ToString();
                            fm.dgvInput.Rows.Add(ds.Tables[0].Rows[i][0], pitNumAndName);
                        }

                        ds.Dispose();
                    }
                    else
                        return;

                    if (fm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        if (fm.dgvInput.SelectedRows.Count > 0)
                        {
                            int index = fm.dgvInput.SelectedRows[0].Index;

                            if (index == 0)
                            {
                                dgvMudProp.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "Flow Line";
                                dgvMudProp.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag = null;
                            }
                            else
                            {
                                dgvMudProp.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = fm.dgvInput.SelectedRows[0].Cells[1].Value;
                                dgvMudProp.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag = fm.dgvInput.SelectedRows[0].Cells[0].Value;
                            }
                        }
                    }
                    else
                        return;
                    //~~~~~~~~~~~~~~~~~~~~~~
                    btnMudPropApply.ImageKey = "warning";
                    btnMudPropApply.BackColor = frmMain.warningColor;
                }
            }
        }
        //-------------------------------------------------------
        private void btnWaterAdd_Click(object sender, EventArgs e)
        {
            string METHOD = "btnWaterAdd_Click : " + CLASS_NAME;

            if (frmMain.selectedRepID == -1)
                return;

            if (access.mudProperties_Water != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;

            //-----------
            PredefinedValuesFormDB_Selection pdSel = new PredefinedValuesFormDB_Selection("Water Type");

            if (pdSel.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;

            if (pdSel.dgvValue.SelectedRows.Count == 0)
                return;

            string wID = pdSel.dgvValue.Rows[pdSel.dgvValue.SelectedRows[0].Index].Cells[0].Value.ToString();
            //-----------

            try
            {
                List<ProcedureParam> prms = new List<ProcedureParam>();
                prms.Add(new ProcedureParam("@ReportID", frmMain.selectedRepID.ToString(), ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Report"));
                prms.Add(new ProcedureParam("@WaterType_PredefAutoID", wID, ProcedureParam.ParamType.PT_Int, true, "1", false, "", "Invalid Water Type"));
                prms.Add(new ProcedureParam("@ProjAutoID", frmMain.selectedPrjID.ToString(), ProcedureParam.ParamType.PT_Int, true, "1", false, "", "Invalid Project"));

                Int64 retStat = -1;
                string simpErr;
                Errors critErr;

                int ret = Procedures.CallProcedure("prc_rt_Rep2MudPropWater_insert", prms, out simpErr, out critErr, out retStat);

                if (ret != 0)
                {
                    if (ret == 1)
                        InformationManager.Set_Info(simpErr);
                    else
                        InformationManager.Set_Info(critErr);

                    return;
                }
            }
            catch (Exception ex)
            {
                InformationManager.Set_Info(ex.Message);
            }


            if (!LoadWater())
            {
                InformationManager.Set_Info(new Errors("Can not load data from data base", "", METHOD));
                //?????logout????
            }

            btnWaterApply.ImageKey = "Check";
            btnWaterApply.BackColor = frmMain.checkColor;
        }
        //-------------------------------------------------------
        private void btnWaterApply_Click(object sender, EventArgs e)
        {
            if (frmMain.selectedRepID == -1)
                return;

            if (access.mudProperties_Water != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;

            try
            {
                List<Errors> errs = new List<Errors>();

                for (int i = 1; i < dgvWater.Columns.Count; i += 2)
                {
                    string id = dgvWater.Columns[i].Tag.ToString();

                    List<ProcedureParam> prms = new List<ProcedureParam>();
                    prms.Add(new ProcedureParam("@ID", id, ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Record"));
                    prms.Add(new ProcedureParam("@Vol", DGV_Operations.CellValueAsString(dgvWater[i, 0]), ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Volume"));
                    prms.Add(new ProcedureParam("@CL_B", DGV_Operations.CellValueAsString(dgvWater[i, 1]), ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Cl (Before)"));
                    prms.Add(new ProcedureParam("@TotalH_B", DGV_Operations.CellValueAsString(dgvWater[i, 2]), ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Total Hardness (Before)"));
                    prms.Add(new ProcedureParam("@Ca_B", DGV_Operations.CellValueAsString(dgvWater[i, 3]), ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Ca++ (Before)"));
                    prms.Add(new ProcedureParam("@pH_B", DGV_Operations.CellValueAsString(dgvWater[i, 5]), ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid pH (Before)"));

                    prms.Add(new ProcedureParam("@CL_A", DGV_Operations.CellValueAsString(dgvWater[i + 1, 1]), ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Cl (After)"));
                    prms.Add(new ProcedureParam("@TotalH_A", DGV_Operations.CellValueAsString(dgvWater[i + 1, 2]), ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Total Hardness (After)"));
                    prms.Add(new ProcedureParam("@Ca_A", DGV_Operations.CellValueAsString(dgvWater[i + 1, 3]), ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid Ca++ (After)"));
                    prms.Add(new ProcedureParam("@pH_A", DGV_Operations.CellValueAsString(dgvWater[i + 1, 5]), ProcedureParam.ParamType.PT_Decimal, true, "0", false, "", "Invalid pH (After)"));

                    string simpErr;
                    Errors critErr;
                    Int64 resultStat;

                    int ret = Procedures.CallProcedure("prc_rt_Rep2MudPropWater_update", prms, out simpErr, out critErr, out resultStat);

                    for (int j = 0; j < dgvWater.Rows.Count; j++)
                    {
                        Color normalBkColor = (j % 2 == 0) ? dgvWater.BackgroundColor : dgvWater.AlternatingRowsDefaultCellStyle.BackColor;
                        dgvWater.Rows[j].Cells[i].Style.BackColor = normalBkColor;
                        dgvWater.Rows[j].Cells[i + 1].Style.BackColor = normalBkColor;
                    }

                    dgvWater.Rows[0].Cells[i + 1].Style.BackColor = Color.Gray;

                    if (ret != 0)
                    {
                        if (ret == 1)
                        {
                            if (simpErr.StartsWith("Invalid Volume"))
                                dgvWater.Rows[0].Cells[i].Style.BackColor = Color.Red;

                            else if (simpErr.StartsWith("Invalid Cl (Before)"))
                                dgvWater.Rows[1].Cells[i].Style.BackColor = Color.Red;
                            else if (simpErr.StartsWith("Invalid Total Hardness (Before)"))
                                dgvWater.Rows[2].Cells[i].Style.BackColor = Color.Red;
                            else if (simpErr.StartsWith("Invalid Ca++ (Before)"))
                                dgvWater.Rows[3].Cells[i].Style.BackColor = Color.Red;
                            else if (simpErr.StartsWith("Invalid pH (Before)"))
                                dgvWater.Rows[5].Cells[i].Style.BackColor = Color.Red;


                            else if (simpErr.StartsWith("Invalid Cl (After)"))
                                dgvWater.Rows[1].Cells[i + 1].Style.BackColor = Color.Red;
                            else if (simpErr.StartsWith("Invalid Total Hardness (After)"))
                                dgvWater.Rows[2].Cells[i + 1].Style.BackColor = Color.Red;
                            else if (simpErr.StartsWith("Invalid Ca++ (After)"))
                                dgvWater.Rows[3].Cells[i + 1].Style.BackColor = Color.Red;
                            else if (simpErr.StartsWith("Invalid pH (After)"))
                                dgvWater.Rows[5].Cells[i + 1].Style.BackColor = Color.Red;

                            critErr = new Errors(simpErr);
                        }

                        errs.Add(critErr);
                    }
                }

                InformationManager.Set_Info(errs.ToArray());

                if (errs.Count == 0)
                {
                    btnWaterApply.ImageKey = "Check";
                    btnWaterApply.BackColor = frmMain.checkColor;
                }
            }
            catch (Exception)
            {
            }
        }
        //-------------------------------------------------------
        private void btnWaterRemove_Click(object sender, EventArgs e)
        {
            if (frmMain.selectedRepID == -1)
                return;

            if (access.mudProperties_MudProperties != RoleAccess.AcessTypes.AT_WriteAndRead)
                return;

            if (dgvWater.SelectedColumns.Count == 0)
            {
                InformationManager.Set_Info("Select One Column to Delete");
                return;
            }

            string idStr = dgvWater.Columns[dgvWater.SelectedColumns[0].Index].Tag.ToString();

            List<ProcedureParam> prms = new List<ProcedureParam>();
            prms.Add(new ProcedureParam("@ID", idStr, ProcedureParam.ParamType.PT_BigInt, true, "0", false, "", "Invalid Record"));

            string simpErr;
            Errors critErr;
            Int64 resultStat;

            int ret = Procedures.CallProcedure("prc_rt_Rep2MudPropWater_delete", prms, out simpErr, out critErr, out resultStat);

            if (ret != 0)
            {
                if (ret == 1)
                    InformationManager.Set_Info(simpErr);
                else
                    InformationManager.Set_Info(critErr);
            }
            else
            {
                skipPainting_dgvWater = true;

                int waterTypeID = ((dgvWater.SelectedColumns[0].Index - 1) / 2);
                int id = (2 * waterTypeID) + 1;

                dgvWater.Columns.RemoveAt(id);
                dgvWater.Columns.RemoveAt(id);//do not forget column shift
                waterTypesList.RemoveAt(waterTypeID);

                skipPainting_dgvWater = false;
            }
        }
        //-------------------------------------------------------
        private void btnWaterRefresh_Click(object sender, EventArgs e)
        {
            string METHOD = "btnWaterRefresh_Click : " + CLASS_NAME;

            if (frmMain.selectedRepID == -1)
                return;

            if (access.mudProperties_Water == RoleAccess.AcessTypes.AT_None)
                return;

            if (!LoadWater())
            {
                InformationManager.Set_Info(new Errors("Can not load data from data base", "", METHOD));
                //?????logout????
            }

            btnWaterApply.ImageKey = "check";
            btnWaterApply.BackColor = frmMain.checkColor;
        }
        //-------------------------------------------------------
        private void dgvWater_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex == -1)
                return;

            if (skipCellValueChanged_dgvWater)
                return;

            if (e.RowIndex >= 0 && e.ColumnIndex >= 1)
            {
                btnWaterApply.ImageKey = "warning";
                btnWaterApply.BackColor = frmMain.warningColor;

                if (!dgvWater[e.ColumnIndex, e.RowIndex].ReadOnly)
                {
                    skipCellValueChanged_dgvWater = true;
                    ComputeWaterNumbers(e.ColumnIndex);
                    skipCellValueChanged_dgvWater = false;
                }
            }
        }
        //-------------------------------------------------------
    }
}
