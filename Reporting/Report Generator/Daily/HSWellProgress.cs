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
    class HSWellProgress : ReportingBase
    {
        //-------------------------------------------------------
        //from parameters
        Int64 wellID = -1;
        string holeSizeLabel = "";
        DateTime reportingDate;
        int repNum = -1;
        string shamsiReportingDate;
        string selUnitDepth = "";
        string selCurrency = "";
        string operatorName = "";
        string projectName = "";
        string wellName = "";
        bool TVD_MD = false;//true:TVD   ,   false:  MD
        //-------------------------------------------------------
        //Others
        string holeSection = "";
        //-------------------------------------------------------
        public override bool CheckParameters()
        {
            try
            {
                wellID = GetParam<Int64>("wellID");
                holeSizeLabel = GetParam<string>("holeSizeLabel");
                reportingDate = GetParam<DateTime>("reportingDate");
                repNum = GetParam<int>("repNum");
                shamsiReportingDate = GetParam<string>("shamsiReportingDate");
                selUnitDepth = GetParam<string>("selUnitDepth");
                selCurrency = GetParam<string>("selCurrency");
                operatorName = GetParam<string>("operatorName");
                projectName = GetParam<string>("projectName");
                wellName = GetParam<string>("wellName");
                TVD_MD = GetParam<bool>("TVD_MD");
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
                Stream templateFileName = new MemoryStream(DMR.Reporting.Report_Generator.Daily.DailyReportsXlsx.Daily_WellProgress);

                ExcelPackage package = new ExcelPackage(templateFileName);
                if (package == null) { return "Excel Package error"; }
                //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                string tmStr = TVD_MD ? "TVD" : "MD";
                tmStr += "(" + selUnitDepth + ")";
                //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                // ******************* Body ***********************
                int rowCount = 0;
                {
                    string query;

                    if (TVD_MD)
                        query = " select * from  fn_Reporting_RepNumAgainstNightTVD(" + wellID.ToString() + "," + "\'" + holeSizeLabel + "\'" + "," + repNum.ToString() + ") order by repNum";
                    else
                        query = " select * from  fn_Reporting_RepNumAgainstNightMD(" + wellID.ToString() + "," + "\'" + holeSizeLabel + "\'" + "," + repNum.ToString() + ") order by repNum";

                    DataSet ds = new DataSet();
                    if (ConnectionManager.ExecQuery(query, ref ds))
                    {
                        rowCount = ds.Tables[0].Rows.Count;

                        EPP_ExcelManager.SetText(package.Workbook.Worksheets[1], 1, 2, tmStr);


                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            int rIndex = 2 + i;//one-based

                            EPP_ExcelManager.SetText(package.Workbook.Worksheets[1], rIndex, 1, ds.Tables[0].Rows[i][0]);
                            EPP_ExcelManager.SetDecimalValue(package.Workbook.Worksheets[1], rIndex, 2, ds.Tables[0].Rows[i][1]);
                        }

                        ds.Dispose();
                    }
                }
                // ******************* Chart ***********************
                if (rowCount > 0)
                {
                    string rigNames = "";

                    {//others
                        string query = " select * from  fn_Reporting_RepNumAgainstNightTVDMD_Header(" + wellID.ToString() + "," + "\'" + holeSizeLabel + "\'" + "," + repNum.ToString() + ") order by repNum";

                        DataSet ds = new DataSet();
                        if (ConnectionManager.ExecQuery(query, ref ds))
                        {
                            if (ds.Tables[0].Rows[0][0] != DBNull.Value)
                                rigNames = ds.Tables[0].Rows[0][0].ToString();

                            ds.Dispose();
                        }
                    }

                    ExcelChartsheet csheet = package.Workbook.Worksheets.AddChart("Well Progress", eChartType.LineMarkers);
                    if (csheet == null) { return "Chart sheet error"; }
                    ExcelChart chart = csheet.Chart;

                    chart.Title.Text = projectName + "_" + wellName + "_" + rigNames + "_" + holeSection + "_Well Progress";
                    string dataSheetName = "\'Values\'";
                    chart.Series.Add(dataSheetName + "!" + "B2:B" + (1 + rowCount), dataSheetName + "!" + "A2:A" + (1 + rowCount));
                    chart.XAxis.Title.Text = "Report Number";
                    chart.XAxis.MajorTickMark = eAxisTickMark.None;
                    chart.Legend.Remove();
                    chart.YAxis.Title.Text = tmStr;
                    chart.Series[0].Header = wellName;
                    chart.Style = eChartStyle.Style2;
                    chart.YAxis.Orientation = eAxisOrientation.MaxMin;
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
            return StringManipulation.ReplaceBadNameChars("PDF-" + operatorName + "-" + projectName + "-" + wellName + "-" + holeSection + "Well Progress" + ".xlsx");
        }
        //-------------------------------------------------------
    }
}
