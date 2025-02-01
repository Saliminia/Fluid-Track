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
    class HSMaterialConsumption : ReportingBase
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
                selUnitDepth = GetParam<string>("selUnitDepth");
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
                Stream templateFileName = new MemoryStream(DMR.Reporting.Report_Generator.Daily.DailyReportsXlsx.Daily_MaterialConsumption);

                ExcelPackage package = new ExcelPackage(templateFileName);
                if (package == null) { return "Excel Package error"; }
                //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                // *********************** Header ***********************

                {//Images
                    EPP_ExcelManager.SetImage(package.Workbook.Worksheets[1], 10, 30, 140, 60, DMR.Images.PDFExcellLogo, "pdf-logo");//Logo

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
                            EPP_ExcelManager.SetImage(package.Workbook.Worksheets[1], 660, 30, 80, 40, contractorImage, "Contractor Image");

                        if (operatorImage != null)
                            EPP_ExcelManager.SetImage(package.Workbook.Worksheets[1], 740, 30, 80, 40, operatorImage, "Operator Image");
                    }
                    else if (useContractorImage)
                    {
                        if (contractorImage != null)
                            EPP_ExcelManager.SetImage(package.Workbook.Worksheets[1], 660, 30, 120, 60, contractorImage, "Contractor Image");
                    }
                    else if (useOperatorImage)
                    {
                        if (operatorImage != null)
                            EPP_ExcelManager.SetImage(package.Workbook.Worksheets[1], 660, 30, 120, 60, operatorImage, "Operator Image");
                    }
                }
                //~~~~~~~~~~~~~~~~~
                {//others
                    string query = " select * from fn_Reporting_HoleSectionMudMaterialConsumption_Header(" + wellID.ToString() + "," + "\'" + holeSizeLabel + "\'" + ")  ";

                    DataSet ds = ConnectionManager.ExecQuery(query, 1);
                    if (ds != null)
                    {
                        EPP_ExcelManager.SetText(package.Workbook.Worksheets[1], 6, 3, ds.Tables[0].Rows[0][0].ToString());//client Name
                        EPP_ExcelManager.SetText(package.Workbook.Worksheets[1], 7, 3, operatorName = ds.Tables[0].Rows[0][1].ToString());//Operator Name
                        EPP_ExcelManager.SetText(package.Workbook.Worksheets[1], 8, 3, projectName = ds.Tables[0].Rows[0][2].ToString());//Project Name
                        EPP_ExcelManager.SetText(package.Workbook.Worksheets[1], 9, 3, ds.Tables[0].Rows[0][3].ToString());//Rig Name

                        EPP_ExcelManager.SetText(package.Workbook.Worksheets[1], 6, 6, wellName = ds.Tables[0].Rows[0][4].ToString());//Well Name
                        EPP_ExcelManager.SetText(package.Workbook.Worksheets[1], 8, 6, ds.Tables[0].Rows[0][5].ToString());//Drilled Interval
                        EPP_ExcelManager.SetText(package.Workbook.Worksheets[1], 9, 6, ds.Tables[0].Rows[0][6].ToString());//Casing Depth
                        ds.Dispose();
                    }
                    {//Hole section, Code , Date
                        EPP_ExcelManager.SetText(package.Workbook.Worksheets[1], 7, 6, holeSection);//Hole Section
                        EPP_ExcelManager.SetText(package.Workbook.Worksheets[1], 6, 9, code);//Code
                        EPP_ExcelManager.SetText(package.Workbook.Worksheets[1], 8, 9, reportingDate.ToShortDateString() + " - " + shamsiReportingDate);//Date
                    }
                }
                // ******************* Body ***********************
                int nonZeroCount = 0;

                {
                    string query = " select * from  fn_Reporting_HoleSectionMudMaterialConsumption(" + wellID.ToString() + "," + "\'" + holeSizeLabel + "\'" + ") order by item ";

                    DataSet ds = new DataSet();
                    if (ConnectionManager.ExecQuery(query, ref ds))
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            if (ds.Tables[0].Rows[i][4] == DBNull.Value || Convert.ToDecimal(ds.Tables[0].Rows[i][4]) == 0.000m)//used
                                continue;

                            nonZeroCount++;
                        }

                        if (nonZeroCount > 0)
                        {
                            package.Workbook.Worksheets[1].InsertRow(11 + 1, nonZeroCount - 1, 11);//copy cell format [but not merging]
                            nonZeroCount = 0;

                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                if (ds.Tables[0].Rows[i][4] == DBNull.Value || Convert.ToDecimal(ds.Tables[0].Rows[i][4]) == 0.000m)//used
                                    continue;

                                int rIndex = 11 + nonZeroCount;//one-based
                                nonZeroCount++;

                                EPP_ExcelManager.Merge(package.Workbook.Worksheets[1], rIndex, 2, rIndex, 3);
                                EPP_ExcelManager.Merge(package.Workbook.Worksheets[1], rIndex, 4, rIndex, 6);

                                EPP_ExcelManager.SetText(package.Workbook.Worksheets[1], rIndex, 1, ds.Tables[0].Rows[i][0]);
                                EPP_ExcelManager.SetText(package.Workbook.Worksheets[1], rIndex, 2, ds.Tables[0].Rows[i][1]);
                                EPP_ExcelManager.SetText(package.Workbook.Worksheets[1], rIndex, 4, ds.Tables[0].Rows[i][2]);
                                EPP_ExcelManager.SetText(package.Workbook.Worksheets[1], rIndex, 7, ds.Tables[0].Rows[i][3]);
                                EPP_ExcelManager.SetDecimalValue(package.Workbook.Worksheets[1], rIndex, 8, ds.Tables[0].Rows[i][4]);
                                EPP_ExcelManager.SetDecimalValue(package.Workbook.Worksheets[1], rIndex, 9, ds.Tables[0].Rows[i][5]);
                            }

                            EPP_ExcelManager.SetBackColor_AlternatingRows(package.Workbook.Worksheets[1], 11, 1, 11 + nonZeroCount - 1, 9,
                                     Color.White, Color.FromArgb(255, 204, 204), true);
                        }

                        ds.Dispose();
                    }
                }
                // ******************* Units, Currency ***********************
                {
                    EPP_ExcelManager.SetText(package.Workbook.Worksheets[1], 8, 5, "Drilled Interval(" + selUnitDepth + "):");//Drilled Interval
                    EPP_ExcelManager.SetText(package.Workbook.Worksheets[1], 9, 5, "Casing Depth(" + selUnitDepth + "):");//Casing Depth
                }
                // ******************* Chart ***********************
                if (nonZeroCount > 0)
                {
                    ExcelChartsheet csheet = package.Workbook.Worksheets.AddChart("H.S Mud MT Consumption-PDF - Graph", eChartType.Pie);
                    if (csheet == null) { return "Chart sheet error"; }
                    var pieChart = (ExcelPieChart)csheet.Chart;

                    pieChart.Title.Text = projectName + "_" + wellName + "_" + holeSection + "_Mud Material Consumption-PDF";
                    string dataSheetName = "\'H.S Mud MT Consumption- Table \'";
                    pieChart.Series.Add(dataSheetName + "!" + "H11:H" + (10 + nonZeroCount), dataSheetName + "!" + "D11:D" + (10 + nonZeroCount));
                    pieChart.Series[0].Header = "";
                    pieChart.Style = eChartStyle.Style2;

                    pieChart.DataLabel.ShowCategory = true;
                    pieChart.DataLabel.ShowPercent = true;
                    pieChart.DataLabel.ShowValue = true;
                    pieChart.DataLabel.ShowLeaderLines = true;
                }

                if (nonZeroCount > 0)
                {
                    ExcelChartsheet csheet = package.Workbook.Worksheets.AddChart("H.S Mud MT Consumption-Other Company - Graph", eChartType.Pie);
                    if (csheet == null) { return "Chart sheet error"; }
                    ExcelChart chart = csheet.Chart;

                    chart.Title.Text = projectName + "_" + wellName + "_" + holeSection + "_Mud Material Consumption-Other Company";
                    string dataSheetName = "\'H.S Mud MT Consumption- Table \'";
                    chart.Series.Add(dataSheetName + "!" + "I11:I" + (10 + nonZeroCount), dataSheetName + "!" + "D11:D" + (10 + nonZeroCount));
                    chart.Series[0].Header = "";
                    chart.Style = eChartStyle.Style2;
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
            return StringManipulation.ReplaceBadNameChars("PDF-" + operatorName + "-" + projectName + "-" + wellName + "-" + holeSection + "Material Consumption" + ".xlsx");
        }
        //-------------------------------------------------------
    }
}
