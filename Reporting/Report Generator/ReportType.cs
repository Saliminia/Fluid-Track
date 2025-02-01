using DMR.User_DB;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;

using System.Text;
using System.Windows.Forms;
using DMR.Reporting.Report_Generator.Daily;
using DMR.Reporting.Report_Generator.Recap;

namespace DMR.Reporting.Report_Generator
{
    public partial class ReportType : Form
    {
        Int64 wellID = -1;
        int prjID = -1;
        string wellName = "";

        frmMain mainForm = null;
        //=======================================================
        public ReportType(Int64 wellID, int prjID, string wellName, frmMain mainForm)
        {
            InitializeComponent();

            this.wellID = wellID;
            this.prjID = prjID;
            this.wellName = wellName;
            this.mainForm = mainForm;

            DateTime dt = dtpReportTimePicker.Value;
            PersianCalendar pc = new PersianCalendar();

            txtReportShamsiDate.Text = ShamsiDate.ShamsiEquivalent(dt);
        }
        //-------------------------------------------------------
        private void dtpReportTimePicker_ValueChanged(object sender, EventArgs e)
        {
            DateTime dt = dtpReportTimePicker.Value;
            PersianCalendar pc = new PersianCalendar();

            txtReportShamsiDate.Text = ShamsiDate.ShamsiEquivalent(dt);
        }
        //-------------------------------------------------------
        private void btnChooseHoleSize_Click(object sender, EventArgs e)
        {
            HoleSizeFormDB_Selection frmHoleSel = new HoleSizeFormDB_Selection();

            frmHoleSel.dgvHoleSize.Columns[0].Visible = false;//single selection
            frmHoleSel.selBtns.ShowSelectionButtons = false;

            if (frmHoleSel.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;

            if (frmHoleSel.dgvHoleSize.SelectedRows.Count == 0)
                return;

            int holeSizeID = Convert.ToInt32(frmHoleSel.dgvHoleSize.Rows[frmHoleSel.dgvHoleSize.SelectedRows[0].Index].Cells[1].Value);
            string holeLabel = DGV_Operations.CellValueAsString(frmHoleSel.dgvHoleSize.Rows[frmHoleSel.dgvHoleSize.SelectedRows[0].Index].Cells[2]);

            lblHoleSize.Text = holeLabel;
            lblHoleSize.Tag = holeSizeID;
        }
        //-------------------------------------------------------
        private void btnReportSelectOutDir_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() != DialogResult.OK)
                return;

            txtReportOutDir.Text = fbd.SelectedPath;
        }
        //-------------------------------------------------------
        bool IsReportOutDirValid()
        {
            return Directory.Exists(txtReportOutDir.Text);
        }
        //-------------------------------------------------------
        private void btnReportGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsReportOutDirValid())
                {
                    InformationManager.Set_Info("Invalid output directory");
                    return;
                }
                //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                if (!rbtnDMR.Checked && lblHoleSize.Text == "")
                {
                    InformationManager.Set_Info("Invalid hole section");
                    return;
                }
                //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                if (rbtnDMR.Checked || rbtnWellProgress.Checked)
                {
                    string query = " select * from fn_Get_ReportIdOfLastRevs_ByWellIDAndRepNum(" + wellID.ToString() + "," + ((int)nudReportNumber.Value).ToString() + ")";

                    DataSet ds = ConnectionManager.ExecQuery(query);
                    if (ds != null)
                    {
                        int repCount = ds.Tables[0].Rows.Count;
                        ds.Dispose();

                        if (repCount == 0)
                        {
                            InformationManager.Set_Info("No such report number exists");
                            return;
                        }
                        //else if (repCount > 1)
                        //{
                        //}
                    }
                    else
                    {
                        InformationManager.Set_Info("Report Generating Error [Data]");
                        return;
                    }
                }
                //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                ProjectForm prjForm = (ProjectForm)mainForm.subForms[mainForm.PorjectFormID];
                ReportingBase rep = null;
                //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                string currency = "";
                if (!FetchTableData.GetProjectCurrency(frmMain.selectedPrjID, out currency))
                    currency = "currency";
                //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                if (rbtnHoleSectionMudMatConsumption.Checked)
                {
                    rep = new HSMaterialConsumption();
                    rep.parameters.Add("wellID", wellID);
                    rep.parameters.Add("prjID", prjID);
                    rep.parameters.Add("holeSizeLabel", lblHoleSize.Text);
                    rep.parameters.Add("code", txtReportCode.Text);
                    rep.parameters.Add("reportingDate", dtpReportTimePicker.Value);
                    rep.parameters.Add("shamsiReportingDate", txtReportShamsiDate.Text);
                    rep.parameters.Add("useOperatorImage", rbtnOperatorAndContractorImage.Checked || rbtnOperatorImage.Checked);
                    rep.parameters.Add("useContractorImage", rbtnOperatorAndContractorImage.Checked || rbtnContractorImage.Checked);
                    rep.parameters.Add("selUnitDepth", prjForm.lblUnitSelectedDepth.Tag.ToString());
                }
                else if (rbtnHoleSectionMudMatCost.Checked)
                {
                    rep = new HSMaterialCost();
                    rep.parameters.Add("wellID", wellID);
                    rep.parameters.Add("prjID", prjID);
                    rep.parameters.Add("holeSizeLabel", lblHoleSize.Text);
                    rep.parameters.Add("code", txtReportCode.Text);
                    rep.parameters.Add("reportingDate", dtpReportTimePicker.Value);
                    rep.parameters.Add("shamsiReportingDate", txtReportShamsiDate.Text);
                    rep.parameters.Add("useOperatorImage", rbtnOperatorAndContractorImage.Checked || rbtnOperatorImage.Checked);
                    rep.parameters.Add("useContractorImage", rbtnOperatorAndContractorImage.Checked || rbtnContractorImage.Checked);
                    rep.parameters.Add("selUnitDepth", prjForm.lblUnitSelectedDepth.Tag.ToString());
                    rep.parameters.Add("selCurrency", currency);
                }
                else if (rbtnWellProgress.Checked)
                {
                    rep = new HSWellProgress();
                    rep.parameters.Add("wellID", wellID);
                    rep.parameters.Add("holeSizeLabel", lblHoleSize.Text);
                    rep.parameters.Add("reportingDate", dtpReportTimePicker.Value);
                    rep.parameters.Add("shamsiReportingDate", txtReportShamsiDate.Text);
                    rep.parameters.Add("repNum", ((int)nudReportNumber.Value));
                    rep.parameters.Add("selUnitDepth", prjForm.lblUnitSelectedDepth.Tag.ToString());
                    rep.parameters.Add("selCurrency", currency);

                    FetchTableData.ProjectProperties prjProp = new FetchTableData.ProjectProperties();
                    if (!FetchTableData.GetProjectProperties(frmMain.selectedPrjID, ref prjProp))
                        return;

                    rep.parameters.Add("operatorName", prjProp.operatorName);
                    rep.parameters.Add("projectName", prjProp.name);
                    rep.parameters.Add("wellName", wellName);

                    if (rbtnTVD.Checked)
                        rep.parameters.Add("TVD_MD", true);
                    else
                        rep.parameters.Add("TVD_MD", false);

                }
                else if (rbtnDMR.Checked)
                {
                    rep = new DailyDMR();
                    rep.parameters.Add("wellID", wellID);
                    rep.parameters.Add("prjID", prjID);
                    //rep.parameters.Add("holeSizeLabel", lblHoleSize.Text);
                    rep.parameters.Add("code", txtReportCode.Text);
                    rep.parameters.Add("reportingDate", dtpReportTimePicker.Value);
                    rep.parameters.Add("shamsiReportingDate", txtReportShamsiDate.Text);
                    rep.parameters.Add("repNum", ((int)nudReportNumber.Value));
                    rep.parameters.Add("useOperatorImage", rbtnOperatorAndContractorImage.Checked || rbtnOperatorImage.Checked);
                    rep.parameters.Add("useContractorImage", rbtnOperatorAndContractorImage.Checked || rbtnContractorImage.Checked);
                    rep.parameters.Add("selCurrency", currency);
                }
                else if (rbtnRecap.Checked)
                {
                    rep = new DMR.Reporting.Report_Generator.Recap.Recap();
                    rep.parameters.Add("wellID", wellID);
                    rep.parameters.Add("prjID", prjID);
                    rep.parameters.Add("holeSizeLabel", lblHoleSize.Text);
                    rep.parameters.Add("code", txtReportCode.Text);
                    rep.parameters.Add("reportingDate", dtpReportTimePicker.Value);
                    rep.parameters.Add("shamsiReportingDate", txtReportShamsiDate.Text);
                    rep.parameters.Add("useOperatorImage", rbtnOperatorAndContractorImage.Checked || rbtnOperatorImage.Checked);
                    rep.parameters.Add("useContractorImage", rbtnOperatorAndContractorImage.Checked || rbtnContractorImage.Checked);

                    rep.parameters.Add("selUnitDepth", prjForm.lblUnitSelectedDepth.Tag.ToString());
                    rep.parameters.Add("selUnitVol", prjForm.lblUnitSelectedLiquidVolume.Tag.ToString());
                    rep.parameters.Add("selUnitPowderMatConc", prjForm.lblUnitSelectedPowderMaterialConcentration.Tag.ToString());
                    rep.parameters.Add("selUnitLiquidMatConc", prjForm.lblUnitSelectedLiquidMaterialConcentration.Tag.ToString());
                    rep.parameters.Add("selUnitDischarge", prjForm.lblUnitSelectedDischargeLossRate.Tag.ToString());
                    //~~~~~~~~

                    rep.parameters.Add("selCurrency", currency);
                }
                else
                    return;
                //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

                if (rep != null)
                {
                    rep.outDir = txtReportOutDir.Text;
                    string err = rep.GenerateReport();

                    if (err != "")
                    {
                        InformationManager.Set_Info(err);
                        return;
                    }
                }
                else
                {
                    InformationManager.Set_Info("Invalid Report Type");
                }
            }
            catch (Exception)
            {
                InformationManager.Set_Info("Report Generating Error");
                return;
            }
            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            InformationManager.Set_Info("Report generated", "Status:", "Information");
        }
        //-------------------------------------------------------
    }
}
