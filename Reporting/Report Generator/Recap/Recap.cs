using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OfficeOpenXml;
using OfficeOpenXml.Drawing.Chart;
using System.Data;
using System.IO;
using System.Drawing;

namespace DMR.Reporting.Report_Generator.Recap
{
    class Recap : ReportingBase
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
        string selUnitPowderMatConc = "";
        string selUnitLiquidMatConc = "";
        string selUnitDischarge = "";
        string selCurrency = "";
        //-------------------------------------------------------
        //others
        string operatorName = "";
        string projectName = "";
        string wellName = "";

        string holeSection = "";
        //-------------------------------------------------------
       // ExcelPackage package = null;
        //-------------------------------------------------------
        public class DfsProperties
        {
            public int DFSAutoID = -1;
            public string DFS = "";
            public string MudType = "";
        }
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
                selUnitVol = GetParam<string>("selUnitVol");
                selUnitPowderMatConc = GetParam<string>("selUnitPowderMatConc");
                selUnitLiquidMatConc = GetParam<string>("selUnitLiquidMatConc");
                selUnitDischarge = GetParam<string>("selUnitDischarge");
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
                Stream templateFileName = new MemoryStream(DMR.Reporting.Report_Generator.Recap.RecapReportXlsx.Recap);

                ExcelPackage package = new ExcelPackage(templateFileName);
                if (package == null) { return "Excel Package error"; }
                //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                List<DfsProperties> DFSs = new List<DfsProperties>();
                //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                string query = " select * from  fn_Reporting_Recap_DFSs(" + wellID + "," + "\'" + holeSizeLabel + "\'" + ") ";

                DataSet ds = new DataSet();
                if (ConnectionManager.ExecQuery(query, ref ds))
                {
                    if (ds.Tables[0].Columns.Count != 3)
                    {
                        return "Report Generating Error (Data-1)";
                    }

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DfsProperties dfsProp = new DfsProperties();
                        dfsProp.DFSAutoID = Convert.ToInt32(ds.Tables[0].Rows[i][0]);
                        dfsProp.DFS = ds.Tables[0].Rows[i][1].ToString();
                        dfsProp.MudType = ds.Tables[0].Rows[i][2].ToString();

                        DFSs.Add(dfsProp);
                    }

                    ds.Dispose();
                }
                //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                Recap_HoleSectionMudMaterialConcentrationAndCostAnalysis mmcc = new Recap_HoleSectionMudMaterialConcentrationAndCostAnalysis();
                Recap_HoleSectionVolumeAnalysis va = new Recap_HoleSectionVolumeAnalysis();
                Recap_Hole_SectionVolumeAndCostAnalysis vca = new Recap_Hole_SectionVolumeAndCostAnalysis();
                Recap_HoleSectionSummary s = new Recap_HoleSectionSummary();

                string ret = "";

                string baseSheetName1 = package.Workbook.Worksheets[1].Name;
                string baseSheetName2 = package.Workbook.Worksheets[2].Name;
                string baseSheetName3 = package.Workbook.Worksheets[3].Name;
                string baseSheetName4 = package.Workbook.Worksheets[4].Name;

                //Add Chart Sheets ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                //int chartSheetCount1 = DFSs.Count * 2;//H.S Actual Conc.&Cost-DFS
                {
                    for (int i = DFSs.Count - 1; i >= 0; i--)
                    {
                        string sheetnName = "H.S Act. Cost-" + DFSs[i].DFS; 
                        if (sheetnName.Length > 31) sheetnName = sheetnName.Substring(0, 31);
                        package.Workbook.Worksheets.AddChart(sheetnName, eChartType.Pie3D);
                        package.Workbook.Worksheets.MoveAfter(sheetnName,baseSheetName1);

                        sheetnName = "H.S Act. Conc.-" + DFSs[i].DFS;
                        if (sheetnName.Length > 31) sheetnName = sheetnName.Substring(0, 31);
                        package.Workbook.Worksheets.AddChart(sheetnName, eChartType.Pie3D);
                        package.Workbook.Worksheets.MoveAfter(sheetnName, baseSheetName1);
                    }
                }
                //~~~
                //int chartSheetCount2 = DFSs.Count * 2 + 2/*total*/;//H.S Vol. Analysis for DFS  ,  H.S Vol. Analysis-GR
                {
                    string sheetnName = "H.S Vol. Analysis-GR";
                    if (sheetnName.Length > 31) sheetnName = sheetnName.Substring(0, 31);
                    package.Workbook.Worksheets.AddChart(sheetnName, eChartType.Pie3D);
                    package.Workbook.Worksheets.MoveAfter(sheetnName, baseSheetName2);

                    sheetnName = "H.S Mud Losses Analysis-GR";
                    if (sheetnName.Length > 31) sheetnName = sheetnName.Substring(0, 31);
                    package.Workbook.Worksheets.AddChart(sheetnName, eChartType.Pie3D);
                    package.Workbook.Worksheets.MoveAfter(sheetnName, baseSheetName2);

                    for (int i = DFSs.Count - 1; i >= 0; i--)
                    {
                        sheetnName = "H.S Vol.-" + DFSs[i].DFS;
                        if (sheetnName.Length > 31) sheetnName = sheetnName.Substring(0, 31);
                        package.Workbook.Worksheets.AddChart(sheetnName, eChartType.Pie3D);
                        package.Workbook.Worksheets.MoveAfter(sheetnName, baseSheetName2);

                        sheetnName = "H.S Mud Losses-" + DFSs[i].DFS;
                        if (sheetnName.Length > 31) sheetnName = sheetnName.Substring(0, 31);
                        package.Workbook.Worksheets.AddChart(sheetnName, eChartType.Pie3D);
                        package.Workbook.Worksheets.MoveAfter(sheetnName, baseSheetName2);
                    }
                }
                //~~~
                //int chartSheetCount3 = 1; //H.S Cost Analysis-GR
                {
                    string sheetnName = "H.S Cost Analysis-GR";
                    if (sheetnName.Length > 31) sheetnName = sheetnName.Substring(0, 31);
                    package.Workbook.Worksheets.AddChart(sheetnName, eChartType.ColumnClustered3D);
                    package.Workbook.Worksheets.MoveAfter(sheetnName, baseSheetName3);
                }
                //~~~
                //int chartSheetCount4 = 1; //Hole Section Cost Summary-GR
                {
                    string sheetnName = "Hole Section Cost Summary-GR";
                    if (sheetnName.Length > 31) sheetnName = sheetnName.Substring(0, 31);
                    package.Workbook.Worksheets.AddChart(sheetnName, eChartType.Pie3D);
                    package.Workbook.Worksheets.MoveAfter(sheetnName, baseSheetName4);
                }
                //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

                //Prepare
                ret = mmcc.PrepareSheet(this, package.Workbook.Worksheets[baseSheetName1], DFSs.Count); if (ret != "") throw new Exception();
                ret = va.PrepareSheet(this, package.Workbook.Worksheets[baseSheetName2], DFSs.Count); if (ret != "") throw new Exception();
                ret = vca.PrepareSheet(this, package.Workbook.Worksheets[baseSheetName3], DFSs.Count); if (ret != "") throw new Exception();
                ret = s.PrepareSheet(this, package.Workbook.Worksheets[baseSheetName4], DFSs.Count); if (ret != "") throw new Exception();

                //Generate
                for (int i = 0; i < DFSs.Count; i++)
                {
                    ret = mmcc.GenerateReportForNextDFS(this, package.Workbook.Worksheets[baseSheetName1], DFSs[i].DFSAutoID, DFSs[i].DFS); if (ret != "") throw new Exception();
                    ret = va.GenerateReportForNextDFS(this, package.Workbook.Worksheets[baseSheetName2], DFSs[i].DFSAutoID, DFSs[i].DFS); if (ret != "") throw new Exception();
                    ret = vca.GenerateReportForNextDFS(this, package.Workbook.Worksheets[baseSheetName3], DFSs[i].DFSAutoID, DFSs[i].DFS, mmcc, va); if (ret != "") throw new Exception();
                    ret = s.GenerateReportForNextDFS(this, package.Workbook.Worksheets[baseSheetName4], DFSs[i].DFSAutoID, DFSs[i].DFS, mmcc, va); if (ret != "") throw new Exception();
                }
                //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                {
                    query = " select * from fn_Reporting_Recap_Header(" + wellID.ToString() + "," + "\'" + holeSizeLabel + "\'" + ")";

                    ds = ConnectionManager.ExecQuery(query, 1);
                    if (ds != null)
                    {
                        operatorName = ds.Tables[0].Rows[0][1].ToString();
                        projectName = ds.Tables[0].Rows[0][2].ToString();
                        wellName = ds.Tables[0].Rows[0][4].ToString();
                        ds.Dispose();
                    }
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
            return StringManipulation.ReplaceBadNameChars("PDF-" + operatorName + "-" + projectName + "-" + wellName + "-" + holeSizeLabel + "-Recap-HoleSectionMudMaterialConcentrationAndCostAnalysis" + ".xlsx");
        }
        //-------------------------------------------------------
    }
}
