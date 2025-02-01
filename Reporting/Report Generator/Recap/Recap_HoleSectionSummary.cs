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
    class Recap_HoleSectionSummary
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

        decimal drillInterval = 0;
        decimal daysOnSection = 0;
        decimal engCostPerDay = 0;
        decimal eqCostPerDay = 0;

        string casingBottoms = "";
        string casingODs = "";
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
        decimal totalVol = 0;
        decimal totalCost = 0;
        //-------------------------------------------------------
        void PrepareSheetFormat(ExcelWorksheet sheet, int noDFSs)
        {
            this.noDFSs = noDFSs;
            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            EPP_ExcelManager.SetText(sheet, 10, 4, 10, 4, "Drilled Interval (" + selUnitDepth + ")");
            EPP_ExcelManager.SetText(sheet, 10, 12, 10, 12, "CSG/Liner Depth (" + selUnitDepth + ")");
            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            EPP_ExcelManager.SetText(sheet, 17, 4, 17, 4, "Actual Mud Volume (" + selUnitVol + ")");
            EPP_ExcelManager.SetText(sheet, 17, 6, 17, 6, "Actual Actual Mud Cost (" + selCurrency + ")");
            EPP_ExcelManager.SetText(sheet, 17, 8, 17, 8, "Actual Cost/Vol. (" + selCurrency + "/" + selUnitVol + ")");
            EPP_ExcelManager.SetText(sheet, 17, 10, 17, 10, "Actual Cost/Drilled Interval  (" + selCurrency + "/" + selUnitDepth + ")");
            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            EPP_ExcelManager.SetText(sheet, 11, 2, 11, 2, holeSection);
            EPP_ExcelManager.SetText(sheet, 11, 4, 11, 4, drillInterval.ToString("0.###"));
            EPP_ExcelManager.SetText(sheet, 11, 6, 11, 6, ((int)(daysOnSection)).ToString());

            EPP_ExcelManager.SetText(sheet, 11, 8, 11, 8, casingODs);
            EPP_ExcelManager.SetText(sheet, 11, 12, 11, 12, casingBottoms);
            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            if (noDFSs > 0)
            {
                sheet.InsertRow(19, noDFSs - 1, 19);//copy cell format [but not merging]

                EPP_ExcelManager.SetText(sheet, 21 + noDFSs, 8, 21 + noDFSs, 8, selCurrency);
                EPP_ExcelManager.SetText(sheet, 22 + noDFSs, 8, 22 + noDFSs, 8, selCurrency);
                EPP_ExcelManager.SetText(sheet, 23 + noDFSs, 8, 23 + noDFSs, 8, selCurrency);
                EPP_ExcelManager.SetText(sheet, 24 + noDFSs, 8, 24 + noDFSs, 8, selCurrency);

                for (int i = 0; i < noDFSs; i++)
                {
                    EPP_ExcelManager.Merge(sheet, i + 19, 2, i + 19, 3);
                    EPP_ExcelManager.Merge(sheet, i + 19, 4, i + 19, 5);
                    EPP_ExcelManager.Merge(sheet, i + 19, 6, i + 19, 7);
                    EPP_ExcelManager.Merge(sheet, i + 19, 8, i + 19, 9);
                    EPP_ExcelManager.Merge(sheet, i + 19, 10, i + 19, 11);
                }

                EPP_ExcelManager.SetInBorder(sheet, 17, 2, 17 + 1 + noDFSs + 1, 11, Color.Black, OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                EPP_ExcelManager.SetOutBorder(sheet, 17, 2, 17 + 1 + noDFSs + 1, 11, Color.Black, OfficeOpenXml.Style.ExcelBorderStyle.Thin);

                EPP_ExcelManager.SetAlignment(sheet, 17, 2, 17 + 1 + noDFSs + 1, 11, OfficeOpenXml.Style.ExcelHorizontalAlignment.Center, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
            }
            else
            {
                sheet.DeleteRow(19);
            }
        }
        //-------------------------------------------------------
        int NewDFS_Section(ExcelWorksheet sheet, string dfs, Recap_HoleSectionMudMaterialConcentrationAndCostAnalysis mmcc, Recap_HoleSectionVolumeAnalysis va, bool isLastDFS = false)
        {
            try
            {
                int CurRowIndex = 19 + processedDfsCount;
                //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                EPP_ExcelManager.SetText(sheet, CurRowIndex, 2, CurRowIndex, 2, dfs);
                EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, 4, CurRowIndex, 4, mmcc.sumActualVol);
                EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, 6, CurRowIndex, 6, mmcc.sumActualCost);

                if (mmcc.sumActualVol > 0)
                    EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, 8, CurRowIndex, 8, mmcc.sumActualCost / mmcc.sumActualVol);
                else
                    EPP_ExcelManager.SetText(sheet, CurRowIndex, 8, CurRowIndex, 8, "Undefined");

                if (drillInterval > 0)
                    EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, 10, CurRowIndex, 10, mmcc.sumActualCost / drillInterval);
                else
                    EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, 10, CurRowIndex, 10, "Undefined");

                totalVol += mmcc.sumActualVol;
                totalCost += mmcc.sumActualCost;

                if (isLastDFS)
                {
                    CurRowIndex = 20 + processedDfsCount;
                    //~~~~~~~~~~~~~~~~~~~~
                    EPP_ExcelManager.SetText(sheet, CurRowIndex, 4, CurRowIndex, 4, totalVol.ToString("0.###"));
                    EPP_ExcelManager.SetText(sheet, CurRowIndex, 6, CurRowIndex, 6, totalCost.ToString("0.###"));
                    EPP_ExcelManager.SetText(sheet, CurRowIndex, 8, CurRowIndex, 8, (totalVol > 0 ? (totalCost / totalVol).ToString("0.###") : "Undefined"));
                    EPP_ExcelManager.SetText(sheet, CurRowIndex, 10, CurRowIndex, 10, (drillInterval > 0 ? (totalCost / drillInterval).ToString("0.###") : "Undefined"));
                    //~~~~~~~~~~~~~~~~~~~~
                    CurRowIndex = 22 + processedDfsCount;
                    //~~~~~~~~~~~~~~~~~~~~
                    EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, 9, CurRowIndex++, 9, totalCost);
                    EPP_ExcelManager.SetText(sheet, CurRowIndex, 9, CurRowIndex++, 9, (daysOnSection * engCostPerDay));
                    EPP_ExcelManager.SetText(sheet, CurRowIndex, 9, CurRowIndex++, 9, (daysOnSection * eqCostPerDay));
                    //~~~~~~~~~~~~~~~~~~~~


                    //Chart
                    var pieChart = (ExcelPieChart)((ExcelChartsheet)sheet.Workbook.Worksheets[sheet.Index + 1]).Chart;
                    pieChart.Series.Add
                    (
                    ExcelCellBase.GetFullAddress(sheet.Name, ExcelCellBase.GetAddress(19, 6, 19 + processedDfsCount, 6)),//cost
                    ExcelCellBase.GetFullAddress(sheet.Name, ExcelCellBase.GetAddress(19, 2, 19 + processedDfsCount, 2))//dfs name
                    );

                    pieChart.Legend.Remove();
                    pieChart.Title.Text = "Hole section Actual cost (" + projectName + "_" + rigName + "_" + wellName + "_" + holeSection + ")" +
                                           Environment.NewLine + "Total Cost (" + selCurrency + "):" + totalCost.ToString("0.###");

                    pieChart.DataLabel.ShowCategory = true;
                    pieChart.DataLabel.ShowPercent = true;
                    pieChart.DataLabel.ShowValue = true;
                    pieChart.DataLabel.ShowLeaderLines = true;
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
                totalVol = 0;
                totalCost = 0;

                casingBottoms = "";
                casingODs = "";
                //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                if (!CheckParameters(recap))
                    return "Invalid Parameters";
                //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                holeSection = holeSizeLabel;

                holeSizeLabel = holeSizeLabel.Replace("\'", "\'\'");
                //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                // *********************** Header ***********************
                {//Images
                    EPP_ExcelManager.SetImage(sheet, 50, 10, 140, 60, DMR.Images.PDFExcellLogo, "pdf-logo");//Logo

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
                            EPP_ExcelManager.SetImage(sheet, 740, 10, 80, 40, contractorImage, "Contractor Image");

                        if (operatorImage != null)
                            EPP_ExcelManager.SetImage(sheet, 820, 10, 80, 40, operatorImage, "Operator Image");
                    }
                    else if (useContractorImage)
                    {
                        if (contractorImage != null)
                            EPP_ExcelManager.SetImage(sheet, 740, 10, 120, 60, contractorImage, "Contractor Image");
                    }
                    else if (useOperatorImage)
                    {
                        if (operatorImage != null)
                            EPP_ExcelManager.SetImage(sheet, 740, 10, 120, 60, operatorImage, "Operator Image");
                    }
                }
                //~~~~~~~~~~~~~~~~~
                {//others
                    string query = " select * from fn_Reporting_Recap_Header(" + wellID.ToString() + "," + "\'" + holeSizeLabel + "\'" + ")";

                    DataSet ds = ConnectionManager.ExecQuery(query, 1);
                    if (ds != null)
                    {
                        EPP_ExcelManager.SetText(sheet, 5, 4, ds.Tables[0].Rows[0][0].ToString());//client Name
                        EPP_ExcelManager.SetText(sheet, 6, 4, operatorName = ds.Tables[0].Rows[0][1].ToString());//Operator Name
                        EPP_ExcelManager.SetText(sheet, 7, 4, projectName = ds.Tables[0].Rows[0][2].ToString());//Project Name

                        EPP_ExcelManager.SetText(sheet, 5, 8, rigName = ds.Tables[0].Rows[0][3].ToString());//Rig Name
                        EPP_ExcelManager.SetText(sheet, 6, 8, wellName = ds.Tables[0].Rows[0][4].ToString());//Well Name
                        EPP_ExcelManager.SetText(sheet, 7, 8, holeSection);//Hole Section

                        EPP_ExcelManager.SetText(sheet, 5, 9, "Drilled Interval (" + selUnitDepth + "):");//Drilled Interval (SU)
                        EPP_ExcelManager.SetText(sheet, 5, 11, ds.Tables[0].Rows[0][5].ToString());//Drilled Interval
                        drillInterval = Convert.ToDecimal(ds.Tables[0].Rows[0][5]);

                        EPP_ExcelManager.SetText(sheet, 7, 11, ds.Tables[0].Rows[0][6].ToString());//Casing Depth
                        ds.Dispose();
                    }

                    {//Code , Date
                        EPP_ExcelManager.SetText(sheet, 5, 13, code);//Code
                        EPP_ExcelManager.SetText(sheet, 7, 13, reportingDate.ToShortDateString() + " - " + shamsiReportingDate);//Date
                    }
                }
                //~~~~~~~~~~~~~~~~~
                {
                    string query = " select * from  fn_Reporting_Recap_HoleSectionSummary(" + wellID + "," + "\'" + holeSizeLabel + "\') ";

                    DataSet ds = new DataSet();
                    if (ConnectionManager.ExecQuery(query, ref ds, 1))
                    {
                        if (ds.Tables[0].Columns.Count != 5)
                            return "Report Generating Error (Data-1)";

                        daysOnSection = Convert.ToDecimal(ds.Tables[0].Rows[0][0]);
                        engCostPerDay = Convert.ToDecimal(ds.Tables[0].Rows[0][1]);
                        eqCostPerDay = Convert.ToDecimal(ds.Tables[0].Rows[0][2]);

                        casingBottoms = ds.Tables[0].Rows[0][3].ToString();
                        casingODs = ds.Tables[0].Rows[0][4].ToString();

                        ds.Dispose();
                    }
                }
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
