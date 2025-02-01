using OfficeOpenXml;
using OfficeOpenXml.Drawing.Chart;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;

namespace DMR.Reporting.Report_Generator.Daily
{
    class HSMaterialCost : ReportingBase
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
        string selCurrency = "";
        //-------------------------------------------------------
        //others
        string operatorName = "";
        string projectName = "";
        string wellName = "";

        string holeSection = "";
        //-------------------------------------------------------
        public override bool CheckParameters()
        {
            try
            {
                wellID = GetParam<Int64>("wellID");
                prjID = GetParam<int>("prjID");
                holeSizeLabel = GetParam<string>("holeSizeLabel");
                code = GetParam<string>("code");
                reportingDate = GetParam<DateTime>("reportingDate");
                shamsiReportingDate = GetParam<string>("shamsiReportingDate");
                useOperatorImage = GetParam<bool>("useOperatorImage");
                useContractorImage = GetParam<bool>("useContractorImage");
                useContractorImage = GetParam<bool>("useContractorImage");
                selUnitDepth = GetParam<string>("selUnitDepth");
                selCurrency = GetParam<string>("selCurrency");
                //~~~~~~~~
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        //-------------------------------------------------------
        public override string GenerateReport()
        {
            try
            {
                if (!CheckParameters())
                    return "Invalid Parameters";
                //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                holeSection = holeSizeLabel;

                holeSizeLabel = holeSizeLabel.Replace("\'", "\'\'");
                //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                Stream templateFileName = new MemoryStream(DMR.Reporting.Report_Generator.Daily.DailyReportsXlsx.Daily_MaterialCost);

                ExcelPackage package = new ExcelPackage(templateFileName);
                if (package == null) { return "Excel Package error"; }
                //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                // *********************** Header ***********************
                {//Images
                    EPP_ExcelManager.SetImage(package.Workbook.Worksheets[1], 20, 10, 140, 60, DMR.Images.PDFExcellLogo, "pdf-logo");//Logo

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
                            EPP_ExcelManager.SetImage(package.Workbook.Worksheets[1], 700, 10, 80, 40, contractorImage, "Contractor Image");

                        if (operatorImage != null)
                            EPP_ExcelManager.SetImage(package.Workbook.Worksheets[1], 780, 10, 80, 40, operatorImage, "Operator Image");
                    }
                    else if (useContractorImage)
                    {
                        if (contractorImage != null)
                            EPP_ExcelManager.SetImage(package.Workbook.Worksheets[1], 700, 10, 120, 60, contractorImage, "Contractor Image");
                    }
                    else if (useOperatorImage)
                    {
                        if (operatorImage != null)
                            EPP_ExcelManager.SetImage(package.Workbook.Worksheets[1], 700, 10, 120, 60, operatorImage, "Operator Image");
                    }
                }
                //~~~~~~~~~~~~~~~~~
                {//others
                    string query = " select * from fn_Reporting_HoleSectionMudMaterialCost_Header(" + wellID.ToString() + "," + "\'" + holeSizeLabel + "\'" + ")   ";

                    DataSet ds = ConnectionManager.ExecQuery(query, 1);
                    if (ds != null)
                    {
                        EPP_ExcelManager.SetText(package.Workbook.Worksheets[1], 5, 3, ds.Tables[0].Rows[0][0].ToString());//client Name
                        EPP_ExcelManager.SetText(package.Workbook.Worksheets[1], 6, 3, operatorName = ds.Tables[0].Rows[0][1].ToString());//Operator Name
                        EPP_ExcelManager.SetText(package.Workbook.Worksheets[1], 7, 3, projectName = ds.Tables[0].Rows[0][2].ToString());//Project Name

                        EPP_ExcelManager.SetText(package.Workbook.Worksheets[1], 5, 5, ds.Tables[0].Rows[0][3].ToString());//Rig Name
                        EPP_ExcelManager.SetText(package.Workbook.Worksheets[1], 6, 5, wellName = ds.Tables[0].Rows[0][4].ToString());//Well Name

                        EPP_ExcelManager.SetText(package.Workbook.Worksheets[1], 5, 7, ds.Tables[0].Rows[0][5].ToString());//Drilled Interval
                        EPP_ExcelManager.SetText(package.Workbook.Worksheets[1], 7, 7, ds.Tables[0].Rows[0][6].ToString());//Casing Depth
                        ds.Dispose();
                    }

                    {//Hole section, Code , Date
                        EPP_ExcelManager.SetText(package.Workbook.Worksheets[1], 7, 5, holeSection);//Hole Section
                        EPP_ExcelManager.SetText(package.Workbook.Worksheets[1], 5, 10, code);//Code
                        EPP_ExcelManager.SetText(package.Workbook.Worksheets[1], 7, 10, reportingDate.ToShortDateString() + " - " + shamsiReportingDate);//Date
                    }
                }
                // ******************* Body ***********************
                int nonZeroCount = 0;
                decimal totalCost = 0;

                {
                    string query = " select * from  fn_Reporting_HoleSectionMudMaterialCost(" + wellID.ToString() + "," + "\'" + holeSizeLabel + "\'" + ") order by item  ";

                    DataSet ds = new DataSet();
                    if (ConnectionManager.ExecQuery(query, ref ds))
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            if (ds.Tables[0].Rows[i][5] == DBNull.Value || Convert.ToDecimal(ds.Tables[0].Rows[i][5]) == 0.000m)//used
                                continue;

                            nonZeroCount++;
                        }

                        if (nonZeroCount > 0)
                        {
                            package.Workbook.Worksheets[1].InsertRow(9 + 1, nonZeroCount - 1, 9);//copy cell format [but not merging]
                            nonZeroCount = 0;

                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                if (ds.Tables[0].Rows[i][5] == DBNull.Value || Convert.ToDecimal(ds.Tables[0].Rows[i][5]) == 0.000m)//used
                                    continue;

                                int rIndex = 9 + nonZeroCount;//one-based
                                nonZeroCount++;

                                EPP_ExcelManager.Merge(package.Workbook.Worksheets[1], rIndex, 2, rIndex, 3);
                                EPP_ExcelManager.Merge(package.Workbook.Worksheets[1], rIndex, 4, rIndex, 5);
                                EPP_ExcelManager.Merge(package.Workbook.Worksheets[1], rIndex, 7, rIndex, 8);

                                EPP_ExcelManager.SetText(package.Workbook.Worksheets[1], rIndex, 1, ds.Tables[0].Rows[i][0]);
                                EPP_ExcelManager.SetText(package.Workbook.Worksheets[1], rIndex, 2, ds.Tables[0].Rows[i][1]);
                                EPP_ExcelManager.SetText(package.Workbook.Worksheets[1], rIndex, 4, ds.Tables[0].Rows[i][2]);
                                EPP_ExcelManager.SetText(package.Workbook.Worksheets[1], rIndex, 6, ds.Tables[0].Rows[i][3]);
                                EPP_ExcelManager.SetText(package.Workbook.Worksheets[1], rIndex, 7, ds.Tables[0].Rows[i][4]);
                                EPP_ExcelManager.SetText(package.Workbook.Worksheets[1], rIndex, 9, ds.Tables[0].Rows[i][5]);
                                EPP_ExcelManager.SetText(package.Workbook.Worksheets[1], rIndex, 10, ds.Tables[0].Rows[i][6]);
                                EPP_ExcelManager.SetDecimalValue(package.Workbook.Worksheets[1], rIndex, 11, ds.Tables[0].Rows[i][7]);

                                if (ds.Tables[0].Rows[i][ds.Tables[0].Columns.Count - 1] != DBNull.Value)
                                    totalCost += Convert.ToDecimal(ds.Tables[0].Rows[i][ds.Tables[0].Columns.Count - 1]);
                            }


                            EPP_ExcelManager.SetText(package.Workbook.Worksheets[1], 9 + nonZeroCount, 11, totalCost);

                            EPP_ExcelManager.SetBackColor_AlternatingRows(package.Workbook.Worksheets[1], 9, 1, 9 + nonZeroCount - 1, 10,
                                                                            Color.White, Color.FromArgb(255, 204, 204), true);
                        }
                        else
                        {
                            EPP_ExcelManager.SetText(package.Workbook.Worksheets[1], 10, 11, 10, 11, totalCost);
                        }

                        ds.Dispose();
                    }
                }
                // ******************* Units, Currency ***********************
                {
                    EPP_ExcelManager.SetText(package.Workbook.Worksheets[1], 5, 6, "Drilled Interval(" + selUnitDepth + "):");//Drilled Interval
                    EPP_ExcelManager.SetText(package.Workbook.Worksheets[1], 7, 6, "Casing Depth(" + selUnitDepth + "):");//Casing Depth

                    EPP_ExcelManager.SetText(package.Workbook.Worksheets[1], 8, 7, "Unit Cost" + Environment.NewLine + selCurrency);//Unit Cost
                    EPP_ExcelManager.SetText(package.Workbook.Worksheets[1], 8, 11, "Material Cost" + Environment.NewLine + selCurrency);//Material Cost

                    if (nonZeroCount > 0)
                        EPP_ExcelManager.SetText(package.Workbook.Worksheets[1], 9 + nonZeroCount, 1, "Total Cost(" + selCurrency + "):");//Total Cost
                    else
                        EPP_ExcelManager.SetText(package.Workbook.Worksheets[1], 10 + nonZeroCount, 1, "Total Cost(" + selCurrency + "):");//Total Cost
                }
                // ******************* Chart ***********************
                if (nonZeroCount > 0)
                {
                    ExcelChartsheet csheet = package.Workbook.Worksheets.AddChart("H.S Mud MT Cost - Graph", eChartType.Pie);
                    if (csheet == null) { return "Chart sheet error"; }
                    var pieChart = (ExcelPieChart)csheet.Chart;

                    pieChart.Title.Text = projectName + "_" + wellName + "_" + holeSection + "_Mud Material Cost" + "\nTotal Cost (" + selCurrency + "): " + totalCost.ToString("0.###");
                    string dataSheetName = "\'H.S Mud Material Cost- Table\'";
                    pieChart.Series.Add(dataSheetName + "!" + "K9:K" + (8 + nonZeroCount), dataSheetName + "!" + "D9:D" + (8 + nonZeroCount));
                    pieChart.Series[0].Header = "";
                    pieChart.Style = eChartStyle.Style2;

                    pieChart.DataLabel.ShowCategory = true;
                    pieChart.DataLabel.ShowPercent = true;
                    pieChart.DataLabel.ShowValue = true;
                    pieChart.DataLabel.ShowLeaderLines = true;
                }
                //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                string repFullFileName = outDir + "\\" + GetReportFilename();

                FileInfo newFile = new FileInfo(repFullFileName);
                if (newFile == null) { return "File error"; }

                if (newFile.Exists)
                {
                    newFile.Delete();  // ensures we create a new workbook
                    newFile = new FileInfo(repFullFileName);
                }
                if (newFile == null) { return "File error"; }
                //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                package.SaveAs(newFile);
                return "";
            }
            catch (Exception)
            {
                return "Report Generating Error";
            }
        }
        //-------------------------------------------------------
        public override string GetReportFilename()
        {
            return StringManipulation.ReplaceBadNameChars("PDF-" + operatorName + "-" + projectName + "-" + wellName + "-" + holeSection + "Material Cost" + ".xlsx");
        }
        //-------------------------------------------------------
    }
}
