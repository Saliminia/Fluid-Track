using OfficeOpenXml;
using OfficeOpenXml.Drawing.Chart;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;

namespace DMR.Reporting.Report_Generator.Recap
{
    class Recap_Hole_SectionVolumeAndCostAnalysis
    {
        //-------------------------------------------------------
        //from parameters
        Int64 wellID = -1;
        int prjID = -1;
        string holeSizeLabel = "";
        string code = "";
        DateTime reportingDate;
        string shamsiReportingDate;
        bool useOperatorImage = false;
        bool useContractorImage = false;
        string selUnitDepth = "";
        string selUnitVol = "";
        string selUnitDischarge = "";
        string selCurrency = "";
        //-------------------------------------------------------
        //others
        string operatorName = "";
        string projectName = "";
        string rigName = "";
        string wellName = "";

        string holeSection = "";
        //-------------------------------------------------------
        int noDFSs = 0;
        //-------------------------------------------------------
        decimal totalRecVol = 0, totalRecCost = 0,
            totalBuiltVol = 0, totalBuiltCost = 0,
            totalTreatVol = 0, totalTreatCost = 0,
            totalUsingVol = 0, totalUsingCost = 0,
            totalUsedForVol = 0, totalUsedForCost = 0,
            totalLossVol = 0, totalLossCost = 0,
            totalTransVol = 0, totalTransCost = 0,
            totalActualVol = 0, totalActualCost = 0;
        //-------------------------------------------------------
        decimal drillInterval = 0;
        //-------------------------------------------------------
        public bool CheckParameters(ReportingBase recap)
        {
            try
            {
                wellID = recap.GetParam<Int64>("wellID");
                prjID = recap.GetParam<int>("prjID");
                holeSizeLabel = recap.GetParam<string>("holeSizeLabel");
                code = recap.GetParam<string>("code");
                reportingDate = recap.GetParam<DateTime>("reportingDate");
                shamsiReportingDate = recap.GetParam<string>("shamsiReportingDate");
                useOperatorImage = recap.GetParam<bool>("useOperatorImage");
                useContractorImage = recap.GetParam<bool>("useContractorImage");
                useContractorImage = recap.GetParam<bool>("useContractorImage");
                selUnitDepth = recap.GetParam<string>("selUnitDepth");
                selUnitVol = recap.GetParam<string>("selUnitVol");
                selUnitDischarge = recap.GetParam<string>("selUnitDischarge");
                selCurrency = recap.GetParam<string>("selCurrency");
                //~~~~~~~~
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        //-------------------------------------------------------
        void PrepareSheetFormat(ExcelWorksheet sheet, int noDFSs)
        {
            this.noDFSs = noDFSs;
            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            for (int i = 0; i < noDFSs + 1; i++)
                EPP_ExcelManager.SetWidth(sheet, 5 + i, 16.0f);

            int totalColumns = 5 + noDFSs;

            EPP_ExcelManager.SetBackColor(sheet, 9, 1, 9, totalColumns, Color.FromArgb(255, 80, 80));
            EPP_ExcelManager.SetFont(sheet, 9, 1, 9, totalColumns, 10, "Times New Roman", true, false);

            string[] titels = new string[] { "Received Mud", "Built Mud", "Treated Mud", 
                                            "Using Other Drilling Fluid System", 
                                            "Used for Other Drilling Fluid System", "Losses Mud",
                                            "Transferred Mud","Actual Mud"};
            int[] titleRows = new int[] { 10, 14, 19, 23, 27, 31, 36, 40 };

            for (int g = 0; g < titels.Length; g++)
            {
                ExcelRange rangeTitle = EPP_ExcelManager.GetRange(sheet, titleRows[g], 1, titleRows[g], totalColumns);
                EPP_ExcelManager.SetBackColor(sheet, rangeTitle, Color.Black);
                EPP_ExcelManager.SetForeColor(sheet, rangeTitle, Color.White);
                EPP_ExcelManager.SetAlignment(sheet, rangeTitle, OfficeOpenXml.Style.ExcelHorizontalAlignment.Center, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
                EPP_ExcelManager.SetFont(sheet, rangeTitle, 12, "Times New Roman", false, false);
                EPP_ExcelManager.MergeAndSet(sheet, rangeTitle, titels[g]);
            }
            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            EPP_ExcelManager.SetText(sheet, 9, totalColumns, 9, totalColumns, "Total");
            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            int CurRowIndex = 11;
            //Rec
            EPP_ExcelManager.SetText(sheet, CurRowIndex, 4, CurRowIndex++, 4, selUnitVol);
            EPP_ExcelManager.SetText(sheet, CurRowIndex, 4, CurRowIndex++, 4, selCurrency);
            EPP_ExcelManager.SetText(sheet, CurRowIndex, 4, CurRowIndex++, 4, selCurrency + "/" + selUnitVol);
            CurRowIndex++;
            //built
            EPP_ExcelManager.SetText(sheet, CurRowIndex, 4, CurRowIndex++, 4, selUnitVol);
            EPP_ExcelManager.SetText(sheet, CurRowIndex, 4, CurRowIndex++, 4, selCurrency);
            EPP_ExcelManager.SetText(sheet, CurRowIndex, 4, CurRowIndex++, 4, selCurrency + "/" + selUnitVol);
            EPP_ExcelManager.SetText(sheet, CurRowIndex, 4, CurRowIndex++, 4, selUnitVol + "/" + selUnitDepth);
            CurRowIndex++;
            //treat
            EPP_ExcelManager.SetText(sheet, CurRowIndex, 4, CurRowIndex++, 4, selUnitVol);
            EPP_ExcelManager.SetText(sheet, CurRowIndex, 4, CurRowIndex++, 4, selCurrency);
            EPP_ExcelManager.SetText(sheet, CurRowIndex, 4, CurRowIndex++, 4, selCurrency + "/" + selUnitVol);
            CurRowIndex++;
            //using other
            EPP_ExcelManager.SetText(sheet, CurRowIndex, 4, CurRowIndex++, 4, selUnitVol);
            EPP_ExcelManager.SetText(sheet, CurRowIndex, 4, CurRowIndex++, 4, selCurrency);
            EPP_ExcelManager.SetText(sheet, CurRowIndex, 4, CurRowIndex++, 4, selCurrency + "/" + selUnitVol);
            CurRowIndex++;
            //using for other
            EPP_ExcelManager.SetText(sheet, CurRowIndex, 4, CurRowIndex++, 4, selUnitVol);
            EPP_ExcelManager.SetText(sheet, CurRowIndex, 4, CurRowIndex++, 4, selCurrency);
            EPP_ExcelManager.SetText(sheet, CurRowIndex, 4, CurRowIndex++, 4, selCurrency + "/" + selUnitVol);
            CurRowIndex++;
            //losses
            EPP_ExcelManager.SetText(sheet, CurRowIndex, 4, CurRowIndex++, 4, selUnitVol);
            EPP_ExcelManager.SetText(sheet, CurRowIndex, 4, CurRowIndex++, 4, selCurrency);
            EPP_ExcelManager.SetText(sheet, CurRowIndex, 4, CurRowIndex++, 4, selCurrency + "/" + selUnitVol);
            EPP_ExcelManager.SetText(sheet, CurRowIndex, 4, CurRowIndex++, 4, selUnitVol + "/" + selUnitDepth);
            CurRowIndex++;
            //transfer
            EPP_ExcelManager.SetText(sheet, CurRowIndex, 4, CurRowIndex++, 4, selUnitVol);
            EPP_ExcelManager.SetText(sheet, CurRowIndex, 4, CurRowIndex++, 4, selCurrency);
            EPP_ExcelManager.SetText(sheet, CurRowIndex, 4, CurRowIndex++, 4, selCurrency + "/" + selUnitVol);
            CurRowIndex++;
            //actual
            EPP_ExcelManager.SetText(sheet, CurRowIndex, 4, CurRowIndex++, 4, selUnitVol);
            EPP_ExcelManager.SetText(sheet, CurRowIndex, 4, CurRowIndex++, 4, selCurrency);
            EPP_ExcelManager.SetText(sheet, CurRowIndex, 4, CurRowIndex++, 4, selCurrency + "/" + selUnitVol);
            EPP_ExcelManager.SetText(sheet, CurRowIndex, 4, CurRowIndex++, 4, selUnitVol + "/" + selUnitDepth);
            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            ExcelRange range = EPP_ExcelManager.GetRange(sheet, 9, 5, 44, totalColumns);
            EPP_ExcelManager.SetInBorder(sheet, range, Color.Black, OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            EPP_ExcelManager.SetOutBorder(sheet, range, Color.Black, OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            EPP_ExcelManager.SetAlignment(sheet, range, OfficeOpenXml.Style.ExcelHorizontalAlignment.Center, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
        }
        //-------------------------------------------------------
        int NewDFS_Section(ExcelWorksheet sheet, string dfs, Recap_HoleSectionMudMaterialConcentrationAndCostAnalysis mmcc, Recap_HoleSectionVolumeAnalysis va, bool isLastDFS = false)
        {
            try
            {
                int CurRowIndex = 9;
                int CurColIndex = 5 + processedDfsCount;
                //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                EPP_ExcelManager.SetText(sheet, CurRowIndex, CurColIndex, CurRowIndex++, CurColIndex, dfs);
                CurRowIndex++;
                //Rec
                EPP_ExcelManager.SetText(sheet, CurRowIndex, CurColIndex, CurRowIndex++, CurColIndex, mmcc.sumRecVol.ToString("0.###")); totalRecVol += mmcc.sumRecVol;
                EPP_ExcelManager.SetText(sheet, CurRowIndex, CurColIndex, CurRowIndex++, CurColIndex, mmcc.sumRecCost.ToString("0.###")); totalRecCost += mmcc.sumRecCost;
                EPP_ExcelManager.SetText(sheet, CurRowIndex, CurColIndex, CurRowIndex++, CurColIndex, (mmcc.sumRecVol > 0 ? (mmcc.sumRecCost / mmcc.sumRecVol).ToString("0.###") : "Undefined"));
                CurRowIndex++;
                //built
                EPP_ExcelManager.SetText(sheet, CurRowIndex, CurColIndex, CurRowIndex++, CurColIndex, mmcc.sumBuiltVol.ToString("0.###")); totalBuiltVol += mmcc.sumBuiltVol;
                EPP_ExcelManager.SetText(sheet, CurRowIndex, CurColIndex, CurRowIndex++, CurColIndex, mmcc.sumBuiltCost.ToString("0.###")); totalBuiltCost += mmcc.sumBuiltCost;
                EPP_ExcelManager.SetText(sheet, CurRowIndex, CurColIndex, CurRowIndex++, CurColIndex, (mmcc.sumBuiltVol > 0 ? (mmcc.sumBuiltCost / mmcc.sumBuiltVol).ToString("0.###") : "Undefined"));
                EPP_ExcelManager.SetText(sheet, CurRowIndex, CurColIndex, CurRowIndex++, CurColIndex, (drillInterval > 0 ? (mmcc.sumBuiltCost / drillInterval).ToString("0.###") : "Undefined"));
                CurRowIndex++;
                //treat
                EPP_ExcelManager.SetText(sheet, CurRowIndex, CurColIndex, CurRowIndex++, CurColIndex, mmcc.sumTreatVol.ToString("0.###")); totalTreatVol += mmcc.sumTreatVol;
                EPP_ExcelManager.SetText(sheet, CurRowIndex, CurColIndex, CurRowIndex++, CurColIndex, mmcc.sumTreatCost.ToString("0.###")); totalTreatCost += mmcc.sumTreatCost;
                EPP_ExcelManager.SetText(sheet, CurRowIndex, CurColIndex, CurRowIndex++, CurColIndex, (mmcc.sumTreatVol > 0 ? (mmcc.sumTreatCost / mmcc.sumTreatVol).ToString("0.###") : "Undefined"));
                CurRowIndex++;
                //using other
                EPP_ExcelManager.SetText(sheet, CurRowIndex, CurColIndex, CurRowIndex++, CurColIndex, mmcc.sumUsingVol.ToString("0.###")); totalUsingVol += mmcc.sumUsingVol;
                EPP_ExcelManager.SetText(sheet, CurRowIndex, CurColIndex, CurRowIndex++, CurColIndex, mmcc.sumUsingCost.ToString("0.###")); totalUsingCost += mmcc.sumUsingCost;
                EPP_ExcelManager.SetText(sheet, CurRowIndex, CurColIndex, CurRowIndex++, CurColIndex, (mmcc.sumUsingVol > 0 ? (mmcc.sumUsingCost / mmcc.sumUsingVol).ToString("0.###") : "Undefined"));
                CurRowIndex++;
                //using for other
                EPP_ExcelManager.SetText(sheet, CurRowIndex, CurColIndex, CurRowIndex++, CurColIndex, mmcc.sumUsedForVol.ToString("0.###")); totalUsedForVol += mmcc.sumUsedForVol;
                EPP_ExcelManager.SetText(sheet, CurRowIndex, CurColIndex, CurRowIndex++, CurColIndex, mmcc.sumUsedForCost.ToString("0.###")); totalUsedForCost += mmcc.sumUsedForCost;
                EPP_ExcelManager.SetText(sheet, CurRowIndex, CurColIndex, CurRowIndex++, CurColIndex, (mmcc.sumUsedForVol > 0 ? (mmcc.sumUsedForCost / mmcc.sumUsedForVol).ToString("0.###") : "Undefined"));
                CurRowIndex++;
                //losses
                EPP_ExcelManager.SetText(sheet, CurRowIndex, CurColIndex, CurRowIndex++, CurColIndex, va.sumLossesVol.ToString("0.###")); totalLossVol += va.sumLossesVol;
                decimal lossCost = mmcc.sumFinalVol > 0 ? (va.sumLossesVol * (mmcc.sumFinalCost / mmcc.sumFinalVol)) : -1;
                EPP_ExcelManager.SetText(sheet, CurRowIndex, CurColIndex, CurRowIndex++, CurColIndex, (lossCost >= 0 ? lossCost.ToString("0.###") : "Undefined")); totalLossCost += (lossCost < 0 ? 0 : lossCost);
                EPP_ExcelManager.SetText(sheet, CurRowIndex, CurColIndex, CurRowIndex++, CurColIndex, (lossCost >= 0 ? (mmcc.sumFinalCost / mmcc.sumFinalVol).ToString("0.###") : "Undefined"));
                EPP_ExcelManager.SetText(sheet, CurRowIndex, CurColIndex, CurRowIndex++, CurColIndex, (lossCost >= 0 && drillInterval > 0 ? (lossCost / drillInterval).ToString("0.###") : "Undefined"));
                CurRowIndex++;
                //transfer
                EPP_ExcelManager.SetText(sheet, CurRowIndex, CurColIndex, CurRowIndex++, CurColIndex, mmcc.sumTransVol.ToString("0.###")); totalTransVol += mmcc.sumTransVol;
                EPP_ExcelManager.SetText(sheet, CurRowIndex, CurColIndex, CurRowIndex++, CurColIndex, mmcc.sumTransCost.ToString("0.###")); totalTransCost += mmcc.sumTransCost;
                EPP_ExcelManager.SetText(sheet, CurRowIndex, CurColIndex, CurRowIndex++, CurColIndex, (mmcc.sumTransVol > 0 ? (mmcc.sumTransCost / mmcc.sumTransVol).ToString("0.###") : "Undefined"));
                CurRowIndex++;
                //actual
                EPP_ExcelManager.SetText(sheet, CurRowIndex, CurColIndex, CurRowIndex++, CurColIndex, mmcc.sumActualVol.ToString("0.###")); totalActualVol += mmcc.sumActualVol;
                EPP_ExcelManager.SetText(sheet, CurRowIndex, CurColIndex, CurRowIndex++, CurColIndex, mmcc.sumActualCost.ToString("0.###")); totalActualCost += mmcc.sumActualCost;
                EPP_ExcelManager.SetText(sheet, CurRowIndex, CurColIndex, CurRowIndex++, CurColIndex, (mmcc.sumActualVol > 0 ? (mmcc.sumActualCost / mmcc.sumActualVol).ToString("0.###") : "Undefined"));
                EPP_ExcelManager.SetText(sheet, CurRowIndex, CurColIndex, CurRowIndex++, CurColIndex, (drillInterval > 0 ? (mmcc.sumActualCost / drillInterval).ToString("0.###") : "Undefined"));


                if (isLastDFS)
                {
                    //Rec
                    EPP_ExcelManager.SetText(sheet, 11, CurColIndex + 1, 11, CurColIndex + 1, totalRecVol.ToString("0.###"));
                    EPP_ExcelManager.SetDecimalValue(sheet, 12, CurColIndex + 1, 12, CurColIndex + 1, totalRecCost);
                    EPP_ExcelManager.SetText(sheet, 13, CurColIndex + 1, 13, CurColIndex + 1, (totalRecVol > 0 ? (totalRecCost / totalRecVol).ToString("0.###") : "Undefined"));
                    //built
                    EPP_ExcelManager.SetText(sheet, 15, CurColIndex + 1, 15, CurColIndex + 1, totalBuiltVol.ToString("0.###"));
                    EPP_ExcelManager.SetDecimalValue(sheet, 16, CurColIndex + 1, 16, CurColIndex + 1, totalBuiltCost);
                    EPP_ExcelManager.SetText(sheet, 17, CurColIndex + 1, 17, CurColIndex + 1, (totalBuiltVol > 0 ? (totalBuiltCost / totalBuiltVol).ToString("0.###") : "Undefined"));
                    EPP_ExcelManager.SetText(sheet, 18, CurColIndex + 1, 18, CurColIndex + 1, (drillInterval > 0 ? (totalBuiltCost / drillInterval).ToString("0.###") : "Undefined"));
                    //treat
                    EPP_ExcelManager.SetText(sheet, 20, CurColIndex + 1, 20, CurColIndex + 1, totalTreatVol.ToString("0.###"));
                    EPP_ExcelManager.SetDecimalValue(sheet, 21, CurColIndex + 1, 21, CurColIndex + 1, totalTreatCost);
                    EPP_ExcelManager.SetText(sheet, 22, CurColIndex + 1, 22, CurColIndex + 1, (totalTreatVol > 0 ? (totalTreatCost / totalTreatVol).ToString("0.###") : "Undefined"));
                    //using other
                    EPP_ExcelManager.SetText(sheet, 24, CurColIndex + 1, 24, CurColIndex + 1, totalUsingVol.ToString("0.###"));
                    EPP_ExcelManager.SetDecimalValue(sheet, 25, CurColIndex + 1, 25, CurColIndex + 1, totalUsingCost);
                    EPP_ExcelManager.SetText(sheet, 26, CurColIndex + 1, 26, CurColIndex + 1, (totalUsingVol > 0 ? (totalUsingCost / totalUsingVol).ToString("0.###") : "Undefined"));
                    //using for other
                    EPP_ExcelManager.SetText(sheet, 28, CurColIndex + 1, 28, CurColIndex + 1, totalUsedForVol.ToString("0.###"));
                    EPP_ExcelManager.SetDecimalValue(sheet, 29, CurColIndex + 1, 29, CurColIndex + 1, totalUsedForCost);
                    EPP_ExcelManager.SetText(sheet, 30, CurColIndex + 1, 30, CurColIndex + 1, (totalUsedForVol > 0 ? (totalUsedForCost / totalUsedForVol).ToString("0.###") : "Undefined"));
                    //losses
                    EPP_ExcelManager.SetText(sheet, 32, CurColIndex + 1, 32, CurColIndex + 1, totalLossVol.ToString("0.###"));
                    EPP_ExcelManager.SetDecimalValue(sheet, 33, CurColIndex + 1, 33, CurColIndex + 1, totalLossCost);
                    EPP_ExcelManager.SetText(sheet, 34, CurColIndex + 1, 34, CurColIndex + 1, (totalLossVol > 0 ? (totalLossCost / totalLossVol).ToString("0.###") : "Undefined"));
                    EPP_ExcelManager.SetText(sheet, 35, CurColIndex + 1, 35, CurColIndex + 1, (drillInterval > 0 ? (totalLossCost / drillInterval).ToString("0.###") : "Undefined"));
                    //transfer
                    EPP_ExcelManager.SetText(sheet, 37, CurColIndex + 1, 37, CurColIndex + 1, totalTransVol.ToString("0.###"));
                    EPP_ExcelManager.SetDecimalValue(sheet, 38, CurColIndex + 1, 38, CurColIndex + 1, totalTransCost);
                    EPP_ExcelManager.SetText(sheet, 39, CurColIndex + 1, 39, CurColIndex + 1, (totalTransVol > 0 ? (totalTransCost / totalTransVol).ToString("0.###") : "Undefined"));
                    //actual
                    EPP_ExcelManager.SetText(sheet, 41, CurColIndex + 1, 41, CurColIndex + 1, totalActualVol.ToString("0.###"));
                    EPP_ExcelManager.SetDecimalValue(sheet, 42, CurColIndex + 1, 42, CurColIndex + 1, totalActualCost);
                    EPP_ExcelManager.SetText(sheet, 43, CurColIndex + 1, 43, CurColIndex + 1, (totalActualVol > 0 ? (totalActualCost / totalActualVol).ToString("0.###") : "Undefined"));
                    EPP_ExcelManager.SetText(sheet, 44, CurColIndex + 1, 44, CurColIndex + 1, (drillInterval > 0 ? (totalActualCost / drillInterval).ToString("0.###") : "Undefined"));


                    //chart
                    {
                        var barChart = (ExcelBarChart)((ExcelChartsheet)sheet.Workbook.Worksheets[sheet.Index + 1]).Chart;

                        string names = ExcelCellBase.GetFullAddress(sheet.Name, ExcelCellBase.GetAddress(12, 1)) + "," +//rec
                                       ExcelCellBase.GetFullAddress(sheet.Name, ExcelCellBase.GetAddress(16, 1)) + "," +//built
                                       ExcelCellBase.GetFullAddress(sheet.Name, ExcelCellBase.GetAddress(21, 1)) + "," +//treat
                                       ExcelCellBase.GetFullAddress(sheet.Name, ExcelCellBase.GetAddress(33, 1)) + "," +//loss
                                       ExcelCellBase.GetFullAddress(sheet.Name, ExcelCellBase.GetAddress(38, 1)) + "," +//trans
                                       ExcelCellBase.GetFullAddress(sheet.Name, ExcelCellBase.GetAddress(42, 1));//actual

                        string totals = ExcelCellBase.GetFullAddress(sheet.Name, ExcelCellBase.GetAddress(12, 4 + noDFSs + 1)) + "," +//rec
                                        ExcelCellBase.GetFullAddress(sheet.Name, ExcelCellBase.GetAddress(16, 4 + noDFSs + 1)) + "," +//built
                                        ExcelCellBase.GetFullAddress(sheet.Name, ExcelCellBase.GetAddress(21, 4 + noDFSs + 1)) + "," +//treat
                                        ExcelCellBase.GetFullAddress(sheet.Name, ExcelCellBase.GetAddress(33, 4 + noDFSs + 1)) + "," +//loss
                                        ExcelCellBase.GetFullAddress(sheet.Name, ExcelCellBase.GetAddress(38, 4 + noDFSs + 1)) + "," +//trans
                                        ExcelCellBase.GetFullAddress(sheet.Name, ExcelCellBase.GetAddress(42, 4 + noDFSs + 1));//actual


                        barChart.Series.Add
                        (
                        ExcelCellBase.GetFullAddress(sheet.Name, totals),//total
                        ExcelCellBase.GetFullAddress(sheet.Name, names)//name
                        );

                        barChart.Legend.Remove();
                        barChart.Title.Text = "COST ANALYSIS " + dfs + Environment.NewLine +
                                              "(" + projectName + "_" + rigName + "_" + wellName + "_" + holeSection + ")";

                        barChart.YAxis.Title.Text = "Cost (" + selCurrency + ")";
                        //barChart.DataLabel.ShowCategory = true;
                        //barChart.DataLabel.ShowPercent = true;
                        //barChart.DataLabel.ShowValue = true;
                        //barChart.DataLabel.ShowLeaderLines = true;
                    }
                }

                return 0;
            }
            catch (Exception)
            {
                return -1024;
            }
        }
        //-------------------------------------------------------
        public string /*error*/ PrepareSheet(ReportingBase recap, ExcelWorksheet sheet, int noDFSs)
        {
            try
            {
                //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                if (!CheckParameters(recap))
                    return "Invalid Parameters";
                //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                holeSection = holeSizeLabel;

                holeSizeLabel = holeSizeLabel.Replace("\'", "\'\'");
                //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                // *********************** Header ***********************
                {//Images
                    EPP_ExcelManager.SetImage(sheet, 20, 10, 140, 60, DMR.Images.PDFExcellLogo, "pdf-logo");//Logo

                    Image contractorImage = null, operatorImage = null;
                    //~~~~~~~~~~~~~~~~~~~~~
                    if (useOperatorImage)
                    {
                        string query = " select * from fn_Get_OperatorImage_ByProjectID(" + prjID.ToString() + ")";

                        DataSet ds = ConnectionManager.ExecQuery(query, 1);
                        if (ds != null)
                        {
                            if (ds.Tables[0].Rows[0][0] != DBNull.Value)
                                operatorImage = SqlImageHelper.ByteArrayToImage((byte[])ds.Tables[0].Rows[0][0]);

                            ds.Dispose();
                        }
                    }
                    //~~~~~~~~~~~~~~~~~~~~~
                    if (useContractorImage)
                    {
                        string query = " select * from fn_Get_ContractorImage_ByWellIDAndHoleSize(" + wellID.ToString() + "," + "\'" + holeSizeLabel + "\'" + ")";

                        DataSet ds = ConnectionManager.ExecQuery(query, 1);
                        if (ds != null)
                        {
                            if (ds.Tables[0].Rows[0][0] != DBNull.Value)
                                contractorImage = SqlImageHelper.ByteArrayToImage((byte[])ds.Tables[0].Rows[0][0]);

                            ds.Dispose();
                        }
                    }
                    //~~~~~~~~~~~~~~~~~~~~~
                    if (useOperatorImage && useContractorImage)
                    {
                        if (contractorImage != null)
                            EPP_ExcelManager.SetImage(sheet, 700, 10, 80, 40, contractorImage, "Contractor Image");

                        if (operatorImage != null)
                            EPP_ExcelManager.SetImage(sheet, 780, 10, 80, 40, operatorImage, "Operator Image");
                    }
                    else if (useContractorImage)
                    {
                        if (contractorImage != null)
                            EPP_ExcelManager.SetImage(sheet, 700, 10, 120, 60, contractorImage, "Contractor Image");
                    }
                    else if (useOperatorImage)
                    {
                        if (operatorImage != null)
                            EPP_ExcelManager.SetImage(sheet, 700, 10, 120, 60, operatorImage, "Operator Image");
                    }
                }
                //~~~~~~~~~~~~~~~~~
                {//others
                    string query = " select * from fn_Reporting_Recap_Header(" + wellID.ToString() + "," + "\'" + holeSizeLabel + "\'" + ")";

                    DataSet ds = ConnectionManager.ExecQuery(query, 1);
                    if (ds != null)
                    {
                        EPP_ExcelManager.SetText(sheet, 5, 2, ds.Tables[0].Rows[0][0].ToString());//client Name
                        EPP_ExcelManager.SetText(sheet, 6, 2, operatorName = ds.Tables[0].Rows[0][1].ToString());//Operator Name
                        EPP_ExcelManager.SetText(sheet, 7, 2, projectName = ds.Tables[0].Rows[0][2].ToString());//Project Name

                        EPP_ExcelManager.SetText(sheet, 5, 4, rigName = ds.Tables[0].Rows[0][3].ToString());//Rig Name
                        EPP_ExcelManager.SetText(sheet, 6, 4, wellName = ds.Tables[0].Rows[0][4].ToString());//Well Name
                        EPP_ExcelManager.SetText(sheet, 7, 4, holeSection);//Hole Section

                        EPP_ExcelManager.SetText(sheet, 5, 5, "Drilled Interval (" + selUnitDepth + "):");//Drilled Interval (SU)
                        EPP_ExcelManager.SetText(sheet, 5, 6, ds.Tables[0].Rows[0][5].ToString());//Drilled Interval
                        drillInterval = Convert.ToDecimal(ds.Tables[0].Rows[0][5]);

                        EPP_ExcelManager.SetText(sheet, 7, 6, ds.Tables[0].Rows[0][6].ToString());//Casing Depth
                        ds.Dispose();
                    }

                    {//Code , Date
                        EPP_ExcelManager.SetText(sheet, 5, 8, code);//Code
                        EPP_ExcelManager.SetText(sheet, 7, 8, reportingDate.ToShortDateString() + " - " + shamsiReportingDate);//Date
                    }
                }
                //~~~~~~~~~~~~~~~~~
                //**********************************************
                PrepareSheetFormat(sheet, noDFSs);
                //**********************************************
                return "";
            }
            catch (Exception)
            {
                return "Report Generating Error";
            }
        }
        //-------------------------------------------------------
        int processedDfsCount = 0;
        //-------------------------------------------------------
        public string GenerateReportForNextDFS(ReportingBase recap, ExcelWorksheet sheet, int DFSAutoID, string DFS,
                                                Recap_HoleSectionMudMaterialConcentrationAndCostAnalysis mmcc,
                                                Recap_HoleSectionVolumeAnalysis va)
        {
            try
            {
                if (NewDFS_Section(sheet, DFS, mmcc, va, processedDfsCount == noDFSs - 1) < 0)
                    return "Report Generating Error (Data-2)";

                processedDfsCount++;
                //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                return "";
            }
            catch (Exception)
            {
                return "Report Generating Error";
            }
        }
        //-------------------------------------------------------
    }
}
