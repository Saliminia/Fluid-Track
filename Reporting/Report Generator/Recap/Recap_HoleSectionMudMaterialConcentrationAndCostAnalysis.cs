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
    class Recap_HoleSectionMudMaterialConcentrationAndCostAnalysis
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
        string selCurrency = "";
        //-------------------------------------------------------
        //others
        string operatorName = "";
        string projectName = "";
        string rigName = "";
        string wellName = "";

        string holeSection = "";
        //-------------------------------------------------------
        public decimal sumRecVol = 0, sumRecCost = 0;
        public decimal sumTransVol = 0, sumTransCost = 0;
        public decimal sumBuiltVol = 0, sumBuiltCost = 0;
        public decimal sumTreatVol = 0, sumTreatCost = 0;
        public decimal sumUsingVol = 0, sumUsingCost = 0;
        public decimal sumUsedForVol = 0, sumUsedForCost = 0;
        public decimal sumActualVol = 0, sumActualCost = 0;
        public decimal sumFinalVol = 0, sumFinalCost = 0;
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
                selUnitPowderMatConc = recap.GetParam<string>("selUnitPowderMatConc");
                selUnitLiquidMatConc = recap.GetParam<string>("selUnitLiquidMatConc");
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
        int CurRowIndex = -1;//1-based [of work sheet]
        int CurColIndex = -1;//1-based [of work sheet]
        //-------------------------------------------------------
        int/*next data row index to read*/ NewDFS_Section(ExcelWorksheet sheet, string dfs)
        {
            try
            {
                const int colsCountForDFS = 16;

                CurColIndex = (CurColIndex < 0) ? 1 : CurColIndex + colsCountForDFS + 2/*space*/;
                CurRowIndex = 8;
                //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                ExcelRange range = EPP_ExcelManager.GetRange(sheet, CurRowIndex, CurColIndex, CurRowIndex, CurColIndex + colsCountForDFS - 1);
                EPP_ExcelManager.Merge(sheet, range);
                EPP_ExcelManager.SetBackColor(sheet, range, Color.LightGray);
                EPP_ExcelManager.SetAlignment(sheet, range, OfficeOpenXml.Style.ExcelHorizontalAlignment.Center, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
                EPP_ExcelManager.SetFont(sheet, range, 15, "Times New Roman", true, false);
                EPP_ExcelManager.SetText(sheet, range, dfs);

                EPP_ExcelManager.SetOutBorder(sheet, range, Color.Black, OfficeOpenXml.Style.ExcelBorderStyle.Thin);

                CurRowIndex++;
                //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                //prepare header columns width 
                float[] headerColWidth = new float[] { 6.00f, 7.29f, 12.71f, 15.14f, 9.00f, 12.57f, 11.57f, 11.57f, 9.43f, 7.00f, 11.86f, 9.71f, 12.57f, 11.86f, 7.00f, 14.71f, 7/*space*/, 7/*space*/};

                for (int c = 0; c < 15; c++)
                    EPP_ExcelManager.SetWidth(sheet, c + 1, headerColWidth[c]);
                //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                return 0;
            }
            catch (Exception)
            {
                return -1024;
            }
        }
        //-------------------------------------------------------
        int[] GetLogicalHeaderColIndices()
        {
            return new int[] 
                { 
                    CurColIndex+0     ,CurColIndex+1    , CurColIndex+2   ,CurColIndex+4    ,CurColIndex+5
                    ,CurColIndex+6    ,CurColIndex+7    ,CurColIndex+8    ,CurColIndex+9    ,CurColIndex+11
                    ,CurColIndex+13   ,CurColIndex+15
                };
        }
        //-------------------------------------------------------
        void PrepareCommonHeaderOfSections(ExcelWorksheet sheet, Color headerColor)
        {
            int[] logicalHeadID = GetLogicalHeaderColIndices();

            EPP_ExcelManager.SetBackColor(sheet, CurRowIndex, CurColIndex, CurRowIndex, CurColIndex + 15, headerColor);
            EPP_ExcelManager.SetFont(sheet, CurRowIndex, CurColIndex, CurRowIndex, CurColIndex + 15, 10, "Calibri Light", true, false);

            for (int c = 0; c < logicalHeadID.Length - 1; c++)
                EPP_ExcelManager.Merge(sheet, CurRowIndex, logicalHeadID[c], CurRowIndex, logicalHeadID[c + 1] - 1);
            string concSelUnit = selUnitPowderMatConc + " , " + selUnitLiquidMatConc;

            string[] headerText = new string[]
                {
                    "Item", "Code", "Product Name", "Unit Size", "Unit Cost ("+selCurrency+")",	"Used-PDF",	"Used-Other Company", "Used-Total",
                    "Concentration-PDF ("+concSelUnit+")", "Concentration-Other Company ("+concSelUnit+")", "Concentration ("+concSelUnit+")", "Total Cost ("+selCurrency+")"
                };

            ExcelRange rangeHeader = EPP_ExcelManager.GetRange(sheet, CurRowIndex, CurColIndex, CurRowIndex, CurColIndex + 15);
            EPP_ExcelManager.SetTextWrap(sheet, rangeHeader, true);
            EPP_ExcelManager.SetAlignment(sheet, rangeHeader, OfficeOpenXml.Style.ExcelHorizontalAlignment.Center, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
            EPP_ExcelManager.SetHeight(sheet, CurRowIndex, 40);

            for (int c = 0; c < logicalHeadID.Length - 1; c++)
                EPP_ExcelManager.Merge(sheet, CurRowIndex, logicalHeadID[c], CurRowIndex, logicalHeadID[c + 1] - 1);

            for (int c = 0; c < logicalHeadID.Length; c++)
                EPP_ExcelManager.SetText(sheet, CurRowIndex, logicalHeadID[c], headerText[c]);
        }
        //-------------------------------------------------------
        int/*next data row index to read*/ Received_Section(ExcelWorksheet sheet, DataSet ds, int dataRowIndex/*indicator row index*/)
        {
            try
            {
                int sectionStartRowIndex = CurRowIndex;
                const int colsCountForDFS = 16;
                //~~~~~~~~~~~~~~~~~~~~~~~~
                //Title
                ExcelRange rangeTitle = EPP_ExcelManager.GetRange(sheet, CurRowIndex, CurColIndex, CurRowIndex, CurColIndex + colsCountForDFS - 1);
                EPP_ExcelManager.SetBackColor(sheet, rangeTitle, Color.Black);
                EPP_ExcelManager.SetForeColor(sheet, rangeTitle, Color.White);
                EPP_ExcelManager.SetAlignment(sheet, rangeTitle, OfficeOpenXml.Style.ExcelHorizontalAlignment.Center, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
                EPP_ExcelManager.SetFont(sheet, rangeTitle, 12, "Times New Roman", false, false);
                EPP_ExcelManager.MergeAndSet(sheet, rangeTitle, "Received Mud");
                CurRowIndex++;
                //~~~~~~~~~~~~~~~~~~~~~~~~
                //Columns Headers
                PrepareCommonHeaderOfSections(sheet, Color.FromArgb(255, 153, 153));
                CurRowIndex++;

                int bodyStartRowIndex = CurRowIndex;

                int[] logicalHeadID = GetLogicalHeaderColIndices();
                //~~~~~~~~~~~~~~~~~~~~~~~~
                bool endOfSectionFound = false;
                const int receiveVolIndicator = -1100;

                decimal sumTotalCost = 0;
                decimal recVol = 0;

                do
                {
                    dataRowIndex++;
                    int item = Convert.ToInt32(ds.Tables[0].Rows[dataRowIndex][0]);
                    if (item == receiveVolIndicator)
                    {
                        endOfSectionFound = true;
                        recVol = Convert.ToDecimal(ds.Tables[0].Rows[dataRowIndex][4]);
                    }
                    else if (item < 0)
                    {
                        return -8192;
                    }
                    else
                    {
                        for (int c = 0; c < logicalHeadID.Length - 1; c++)
                            EPP_ExcelManager.Merge(sheet, CurRowIndex, logicalHeadID[c], CurRowIndex, logicalHeadID[c + 1] - 1);

                        EPP_ExcelManager.SetText(sheet, CurRowIndex, logicalHeadID[0], item.ToString()); //Item
                        EPP_ExcelManager.SetText(sheet, CurRowIndex, logicalHeadID[1], ds.Tables[0].Rows[dataRowIndex][1].ToString()); //Code
                        EPP_ExcelManager.SetText(sheet, CurRowIndex, logicalHeadID[2], ds.Tables[0].Rows[dataRowIndex][2].ToString()); //Product Name
                        EPP_ExcelManager.SetText(sheet, CurRowIndex, logicalHeadID[3], ds.Tables[0].Rows[dataRowIndex][3].ToString()); //Unit Size

                        decimal UnitCost = Convert.ToDecimal(ds.Tables[0].Rows[dataRowIndex][4]);
                        EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, logicalHeadID[4], UnitCost); //Unit Cost

                        decimal usedPdf = Convert.ToDecimal(ds.Tables[0].Rows[dataRowIndex][5]);
                        decimal usedOtherComp = Convert.ToDecimal(ds.Tables[0].Rows[dataRowIndex][6]);
                        EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, logicalHeadID[5], usedPdf); //Used-PDF
                        EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, logicalHeadID[6], usedOtherComp); //Used-Other Company
                        EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, logicalHeadID[7], usedPdf + usedOtherComp); //Used Total

                        decimal concPdf = Convert.ToDecimal(ds.Tables[0].Rows[dataRowIndex][7]);
                        decimal concOtherComp = Convert.ToDecimal(ds.Tables[0].Rows[dataRowIndex][8]);
                        EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, logicalHeadID[8], concPdf); //Concentration-PDF
                        EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, logicalHeadID[9], concOtherComp); //Concentration-Other Company
                        EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, logicalHeadID[10], concPdf + concOtherComp); //Concentration

                        decimal TotalCost = usedPdf * UnitCost;
                        sumTotalCost += TotalCost;
                        EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, logicalHeadID[11], TotalCost); //Total Cost 

                        CurRowIndex++;
                    }
                }
                while (!endOfSectionFound && dataRowIndex < ds.Tables[0].Rows.Count);

                if (!endOfSectionFound)
                    return -2048;

                int bodyEndRowIndex = CurRowIndex - 1;

                //~~~~~~~~Footer~~~~~~~~~~~
                //
                EPP_ExcelManager.SetBackColor(sheet, CurRowIndex, CurColIndex, CurRowIndex + 1, CurColIndex + colsCountForDFS - 1, Color.FromArgb(255, 153, 153));
                //~~~~~~~~~~~~~~~~~~~
                EPP_ExcelManager.MergeAndSet(sheet, CurRowIndex, CurColIndex, CurRowIndex, CurColIndex + 3, "Recieved Volume (" + selUnitVol + ")   =");
                EPP_ExcelManager.SetAlignment(sheet, CurRowIndex, CurColIndex, CurRowIndex, CurColIndex + 3, OfficeOpenXml.Style.ExcelHorizontalAlignment.Center, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
                EPP_ExcelManager.SetAlignment(sheet, CurRowIndex, CurColIndex + 4, CurRowIndex, CurColIndex + 5, OfficeOpenXml.Style.ExcelHorizontalAlignment.Center, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
                EPP_ExcelManager.MergeAndSet(sheet, CurRowIndex, CurColIndex + 4, CurRowIndex, CurColIndex + 5, recVol);
                sumRecVol = recVol;

                EPP_ExcelManager.MergeAndSet(sheet, CurRowIndex, CurColIndex + 6, CurRowIndex, CurColIndex + 14, "Recieved Mud Cost(" + selCurrency + ")   =");
                EPP_ExcelManager.SetAlignment(sheet, CurRowIndex, CurColIndex + 6, CurRowIndex, CurColIndex + 14, OfficeOpenXml.Style.ExcelHorizontalAlignment.Center, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
                EPP_ExcelManager.SetAlignment(sheet, CurRowIndex, CurColIndex + 15, CurRowIndex, CurColIndex + 15, OfficeOpenXml.Style.ExcelHorizontalAlignment.Center, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
                EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, CurColIndex + 15, sumTotalCost);
                sumRecCost = sumTotalCost;

                CurRowIndex++;
                //~~~~~~~~~~~~~~~~~~~
                EPP_ExcelManager.MergeAndSet(sheet, CurRowIndex, CurColIndex, CurRowIndex, CurColIndex + 3, "Recieved Mud Cost/Vol (" + selCurrency + "/ " + selUnitVol + ")=");
                EPP_ExcelManager.SetAlignment(sheet, CurRowIndex, CurColIndex, CurRowIndex, CurColIndex + 3, OfficeOpenXml.Style.ExcelHorizontalAlignment.Center, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
                EPP_ExcelManager.SetAlignment(sheet, CurRowIndex, CurColIndex + 4, CurRowIndex, CurColIndex + 15, OfficeOpenXml.Style.ExcelHorizontalAlignment.Center, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
                EPP_ExcelManager.MergeAndSet(sheet, CurRowIndex, CurColIndex + 4, CurRowIndex, CurColIndex + 15, (recVol > 0) ? (sumTotalCost / recVol).ToString() : "Undefined");

                CurRowIndex++;
                //~~~~~~~~~~~~~~~~~~~
                int sectionEndRowIndex = CurRowIndex - 1;

                EPP_ExcelManager.SetInBorder(sheet, sectionStartRowIndex, CurColIndex, sectionEndRowIndex, CurColIndex + 15, Color.Black, OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                EPP_ExcelManager.SetOutBorder(sheet, sectionStartRowIndex, CurColIndex, sectionEndRowIndex, CurColIndex + 15, Color.Black, OfficeOpenXml.Style.ExcelBorderStyle.Thin);

                EPP_ExcelManager.SetBackColor_AlternatingRows(sheet, bodyStartRowIndex, CurColIndex, bodyEndRowIndex, CurColIndex + 15, Color.White, Color.FromArgb(255, 204, 204), true);

                if (bodyStartRowIndex <= bodyEndRowIndex)
                    EPP_ExcelManager.SetAlignment(sheet, bodyStartRowIndex, CurColIndex, bodyEndRowIndex, CurColIndex + 15, OfficeOpenXml.Style.ExcelHorizontalAlignment.Center, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);

                return dataRowIndex + 1;
            }
            catch (Exception)
            {
                return -1024;
            }
        }
        //-------------------------------------------------------
        int/*next data row index to read*/ Built_Section(ExcelWorksheet sheet, DataSet ds, int dataRowIndex/*indicator row index*/)
        {
            try
            {
                int sectionStartRowIndex = CurRowIndex;
                const int colsCountForDFS = 16;
                //~~~~~~~~~~~~~~~~~~~~~~~~
                //Title
                ExcelRange rangeTitle = EPP_ExcelManager.GetRange(sheet, CurRowIndex, CurColIndex, CurRowIndex, CurColIndex + colsCountForDFS - 1);
                EPP_ExcelManager.SetBackColor(sheet, rangeTitle, Color.Black);
                EPP_ExcelManager.SetForeColor(sheet, rangeTitle, Color.White);
                EPP_ExcelManager.SetAlignment(sheet, rangeTitle, OfficeOpenXml.Style.ExcelHorizontalAlignment.Center, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
                EPP_ExcelManager.SetFont(sheet, rangeTitle, 12, "Times New Roman", false, false);
                EPP_ExcelManager.MergeAndSet(sheet, rangeTitle, "Built Mud");
                CurRowIndex++;
                //~~~~~~~~~~~~~~~~~~~~~~~~
                //Columns Headers
                PrepareCommonHeaderOfSections(sheet, Color.FromArgb(255, 153, 153));
                CurRowIndex++;

                int bodyStartRowIndex = CurRowIndex;

                int[] logicalHeadID = GetLogicalHeaderColIndices();
                //~~~~~~~~~~~~~~~~~~~~~~~~
                bool endOfSectionFound = false;
                const int builtVolIndicator = -2100;

                decimal sumTotalCost = 0;
                decimal builtVol = 0;

                do
                {
                    dataRowIndex++;
                    int item = Convert.ToInt32(ds.Tables[0].Rows[dataRowIndex][0]);
                    if (item == builtVolIndicator)
                    {
                        endOfSectionFound = true;
                        builtVol = Convert.ToDecimal(ds.Tables[0].Rows[dataRowIndex][4]);
                    }
                    else if (item < 0)
                    {
                        return -8192;
                    }
                    else
                    {
                        for (int c = 0; c < logicalHeadID.Length - 1; c++)
                            EPP_ExcelManager.Merge(sheet, CurRowIndex, logicalHeadID[c], CurRowIndex, logicalHeadID[c + 1] - 1);

                        EPP_ExcelManager.SetText(sheet, CurRowIndex, logicalHeadID[0], item.ToString()); //Item
                        EPP_ExcelManager.SetText(sheet, CurRowIndex, logicalHeadID[1], ds.Tables[0].Rows[dataRowIndex][1].ToString()); //Code
                        EPP_ExcelManager.SetText(sheet, CurRowIndex, logicalHeadID[2], ds.Tables[0].Rows[dataRowIndex][2].ToString()); //Product Name
                        EPP_ExcelManager.SetText(sheet, CurRowIndex, logicalHeadID[3], ds.Tables[0].Rows[dataRowIndex][3].ToString()); //Unit Size

                        decimal UnitCost = Convert.ToDecimal(ds.Tables[0].Rows[dataRowIndex][4]);
                        EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, logicalHeadID[4], UnitCost); //Unit Cost

                        decimal usedPdf = Convert.ToDecimal(ds.Tables[0].Rows[dataRowIndex][5]);
                        decimal usedOtherComp = Convert.ToDecimal(ds.Tables[0].Rows[dataRowIndex][6]);
                        EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, logicalHeadID[5], usedPdf); //Used-PDF
                        EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, logicalHeadID[6], usedOtherComp); //Used-Other Company
                        EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, logicalHeadID[7], usedPdf + usedOtherComp); //Used Total

                        decimal concPdf = Convert.ToDecimal(ds.Tables[0].Rows[dataRowIndex][7]);
                        decimal concOtherComp = Convert.ToDecimal(ds.Tables[0].Rows[dataRowIndex][8]);
                        EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, logicalHeadID[8], concPdf); //Concentration-PDF
                        EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, logicalHeadID[9], concOtherComp); //Concentration-Other Company
                        EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, logicalHeadID[10], concPdf + concOtherComp); //Concentration

                        decimal TotalCost = usedPdf * UnitCost;
                        sumTotalCost += TotalCost;
                        EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, logicalHeadID[11], TotalCost); //Total Cost 

                        CurRowIndex++;
                    }
                }
                while (!endOfSectionFound && dataRowIndex < ds.Tables[0].Rows.Count);

                if (!endOfSectionFound)
                    return -2048;

                int bodyEndRowIndex = CurRowIndex - 1;

                //~~~~~~~~Footer~~~~~~~~~~~
                EPP_ExcelManager.SetBackColor(sheet, CurRowIndex, CurColIndex, CurRowIndex + 1, CurColIndex + colsCountForDFS - 1, Color.FromArgb(255, 153, 153));
                //~~~~~~~~~~~~~~~~~~~
                EPP_ExcelManager.MergeAndSet(sheet, CurRowIndex, CurColIndex, CurRowIndex, CurColIndex + 3, "Built Volume (" + selUnitVol + ")   =");
                EPP_ExcelManager.SetAlignment(sheet, CurRowIndex, CurColIndex, CurRowIndex, CurColIndex + 3, OfficeOpenXml.Style.ExcelHorizontalAlignment.Left, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
                EPP_ExcelManager.SetAlignment(sheet, CurRowIndex, CurColIndex + 4, CurRowIndex, CurColIndex + 5, OfficeOpenXml.Style.ExcelHorizontalAlignment.Center, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
                EPP_ExcelManager.MergeAndSet(sheet, CurRowIndex, CurColIndex + 4, CurRowIndex, CurColIndex + 5, builtVol);
                sumBuiltVol = builtVol;

                EPP_ExcelManager.MergeAndSet(sheet, CurRowIndex, CurColIndex + 6, CurRowIndex, CurColIndex + 14, "Built Mud Cost(" + selCurrency + ")   =");
                EPP_ExcelManager.SetAlignment(sheet, CurRowIndex, CurColIndex + 6, CurRowIndex, CurColIndex + 14, OfficeOpenXml.Style.ExcelHorizontalAlignment.Left, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
                EPP_ExcelManager.SetAlignment(sheet, CurRowIndex, CurColIndex + 15, CurRowIndex, CurColIndex + 15, OfficeOpenXml.Style.ExcelHorizontalAlignment.Center, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
                EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, CurColIndex + 15, sumTotalCost);
                sumBuiltCost = sumTotalCost;

                CurRowIndex++;
                //~~~~~~~~~~~~~~~~~~~
                EPP_ExcelManager.MergeAndSet(sheet, CurRowIndex, CurColIndex, CurRowIndex, CurColIndex + 3, "Built Mud Cost/Vol (" + selCurrency + "/ " + selUnitVol + ")=");
                EPP_ExcelManager.SetAlignment(sheet, CurRowIndex, CurColIndex, CurRowIndex, CurColIndex + 3, OfficeOpenXml.Style.ExcelHorizontalAlignment.Left, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
                EPP_ExcelManager.SetAlignment(sheet, CurRowIndex, CurColIndex + 4, CurRowIndex, CurColIndex + 15, OfficeOpenXml.Style.ExcelHorizontalAlignment.Center, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
                EPP_ExcelManager.MergeAndSet(sheet, CurRowIndex, CurColIndex + 4, CurRowIndex, CurColIndex + 15, (builtVol > 0) ? (sumTotalCost / builtVol).ToString() : "Undefined");

                CurRowIndex++;
                //~~~~~~~~~~~~~~~~~~~
                int sectionEndRowIndex = CurRowIndex - 1;

                EPP_ExcelManager.SetInBorder(sheet, sectionStartRowIndex, CurColIndex, sectionEndRowIndex, CurColIndex + 15, Color.Black, OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                EPP_ExcelManager.SetOutBorder(sheet, sectionStartRowIndex, CurColIndex, sectionEndRowIndex, CurColIndex + 15, Color.Black, OfficeOpenXml.Style.ExcelBorderStyle.Thin);

                EPP_ExcelManager.SetBackColor_AlternatingRows(sheet, bodyStartRowIndex, CurColIndex, bodyEndRowIndex, CurColIndex + 15, Color.White, Color.FromArgb(255, 204, 204), true);

                if (bodyStartRowIndex <= bodyEndRowIndex)
                    EPP_ExcelManager.SetAlignment(sheet, bodyStartRowIndex, CurColIndex, bodyEndRowIndex, CurColIndex + 15, OfficeOpenXml.Style.ExcelHorizontalAlignment.Center, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);

                return dataRowIndex + 1;
            }
            catch (Exception)
            {
                return -1024;
            }
        }
        //-------------------------------------------------------
        int/*next data row index to read*/ Treat_Section(ExcelWorksheet sheet, DataSet ds, int dataRowIndex/*indicator row index*/)
        {
            try
            {
                int sectionStartRowIndex = CurRowIndex;
                const int colsCountForDFS = 16;
                //~~~~~~~~~~~~~~~~~~~~~~~~
                //Title
                ExcelRange rangeTitle = EPP_ExcelManager.GetRange(sheet, CurRowIndex, CurColIndex, CurRowIndex, CurColIndex + colsCountForDFS - 1);
                EPP_ExcelManager.SetBackColor(sheet, rangeTitle, Color.Black);
                EPP_ExcelManager.SetForeColor(sheet, rangeTitle, Color.White);
                EPP_ExcelManager.SetAlignment(sheet, rangeTitle, OfficeOpenXml.Style.ExcelHorizontalAlignment.Center, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
                EPP_ExcelManager.SetFont(sheet, rangeTitle, 12, "Times New Roman", false, false);
                EPP_ExcelManager.MergeAndSet(sheet, rangeTitle, "Treatment");
                CurRowIndex++;
                //~~~~~~~~~~~~~~~~~~~~~~~~
                //Columns Headers
                PrepareCommonHeaderOfSections(sheet, Color.FromArgb(255, 153, 153));
                CurRowIndex++;

                int bodyStartRowIndex = CurRowIndex;

                int[] logicalHeadID = GetLogicalHeaderColIndices();
                //~~~~~~~~~~~~~~~~~~~~~~~~
                bool endOfSectionFound = false;
                const int treatVolIndicator = -3100;

                decimal sumTotalCost = 0;
                decimal treatVol = 0;
                decimal fortreatVol = 0;

                do
                {
                    dataRowIndex++;
                    int item = Convert.ToInt32(ds.Tables[0].Rows[dataRowIndex][0]);
                    if (item == treatVolIndicator)
                    {
                        endOfSectionFound = true;
                        treatVol = Convert.ToDecimal(ds.Tables[0].Rows[dataRowIndex][4]);
                        fortreatVol = Convert.ToDecimal(ds.Tables[0].Rows[dataRowIndex][5]);
                    }
                    else if (item < 0)
                    {
                        return -8192;
                    }
                    else
                    {
                        for (int c = 0; c < logicalHeadID.Length - 1; c++)
                            EPP_ExcelManager.Merge(sheet, CurRowIndex, logicalHeadID[c], CurRowIndex, logicalHeadID[c + 1] - 1);

                        EPP_ExcelManager.SetText(sheet, CurRowIndex, logicalHeadID[0], item.ToString()); //Item
                        EPP_ExcelManager.SetText(sheet, CurRowIndex, logicalHeadID[1], ds.Tables[0].Rows[dataRowIndex][1].ToString()); //Code
                        EPP_ExcelManager.SetText(sheet, CurRowIndex, logicalHeadID[2], ds.Tables[0].Rows[dataRowIndex][2].ToString()); //Product Name
                        EPP_ExcelManager.SetText(sheet, CurRowIndex, logicalHeadID[3], ds.Tables[0].Rows[dataRowIndex][3].ToString()); //Unit Size

                        decimal UnitCost = Convert.ToDecimal(ds.Tables[0].Rows[dataRowIndex][4]);
                        EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, logicalHeadID[4], UnitCost); //Unit Cost

                        decimal usedPdf = Convert.ToDecimal(ds.Tables[0].Rows[dataRowIndex][5]);
                        decimal usedOtherComp = Convert.ToDecimal(ds.Tables[0].Rows[dataRowIndex][6]);
                        EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, logicalHeadID[5], usedPdf); //Used-PDF
                        EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, logicalHeadID[6], usedOtherComp); //Used-Other Company
                        EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, logicalHeadID[7], usedPdf + usedOtherComp); //Used Total

                        decimal concPdf = Convert.ToDecimal(ds.Tables[0].Rows[dataRowIndex][7]);
                        decimal concOtherComp = Convert.ToDecimal(ds.Tables[0].Rows[dataRowIndex][8]);
                        EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, logicalHeadID[8], concPdf); //Concentration-PDF
                        EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, logicalHeadID[9], concOtherComp); //Concentration-Other Company
                        EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, logicalHeadID[10], concPdf + concOtherComp); //Concentration

                        decimal TotalCost = usedPdf * UnitCost;
                        sumTotalCost += TotalCost;
                        EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, logicalHeadID[11], TotalCost); //Total Cost 

                        CurRowIndex++;
                    }
                }
                while (!endOfSectionFound && dataRowIndex < ds.Tables[0].Rows.Count);

                if (!endOfSectionFound)
                    return -2048;

                int bodyEndRowIndex = CurRowIndex - 1;

                //~~~~~~~~Footer~~~~~~~~~~~
                EPP_ExcelManager.SetBackColor(sheet, CurRowIndex, CurColIndex, CurRowIndex + 1, CurColIndex + colsCountForDFS - 1, Color.FromArgb(255, 153, 153));
                //~~~~~~~~~~~~~~~~~~~
                EPP_ExcelManager.MergeAndSet(sheet, CurRowIndex, CurColIndex, CurRowIndex, CurColIndex + 3, "Used Volume for Treatment (" + selUnitVol + ")=");
                EPP_ExcelManager.SetAlignment(sheet, CurRowIndex, CurColIndex, CurRowIndex, CurColIndex + 3, OfficeOpenXml.Style.ExcelHorizontalAlignment.Left, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
                EPP_ExcelManager.SetAlignment(sheet, CurRowIndex, CurColIndex + 4, CurRowIndex, CurColIndex + 6, OfficeOpenXml.Style.ExcelHorizontalAlignment.Center, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
                EPP_ExcelManager.MergeAndSet(sheet, CurRowIndex, CurColIndex + 4, CurRowIndex, CurColIndex + 6, fortreatVol);

                EPP_ExcelManager.MergeAndSet(sheet, CurRowIndex, CurColIndex + 7, CurRowIndex, CurColIndex + 8, "Final Volume (" + selUnitVol + ")=");
                EPP_ExcelManager.SetAlignment(sheet, CurRowIndex, CurColIndex + 7, CurRowIndex, CurColIndex + 8, OfficeOpenXml.Style.ExcelHorizontalAlignment.Left, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
                EPP_ExcelManager.SetAlignment(sheet, CurRowIndex, CurColIndex + 9, CurRowIndex, CurColIndex + 11, OfficeOpenXml.Style.ExcelHorizontalAlignment.Center, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
                EPP_ExcelManager.MergeAndSet(sheet, CurRowIndex, CurColIndex + 9, CurRowIndex, CurColIndex + 11, treatVol);
                sumTreatVol = treatVol;

                EPP_ExcelManager.MergeAndSet(sheet, CurRowIndex, CurColIndex + 12, CurRowIndex, CurColIndex + 14, "Treated Mud Cost(" + selCurrency + ")   =");
                EPP_ExcelManager.SetAlignment(sheet, CurRowIndex, CurColIndex + 12, CurRowIndex, CurColIndex + 14, OfficeOpenXml.Style.ExcelHorizontalAlignment.Left, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
                EPP_ExcelManager.SetAlignment(sheet, CurRowIndex, CurColIndex + 15, CurRowIndex, CurColIndex + 15, OfficeOpenXml.Style.ExcelHorizontalAlignment.Center, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
                EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, CurColIndex + 15, sumTotalCost);
                sumTreatCost = sumTotalCost;

                CurRowIndex++;
                //~~~~~~~~~~~~~~~~~~~
                EPP_ExcelManager.MergeAndSet(sheet, CurRowIndex, CurColIndex, CurRowIndex, CurColIndex + 3, "Treated Mud Cost/Vol (" + selCurrency + "/ " + selUnitVol + ")=");
                EPP_ExcelManager.SetAlignment(sheet, CurRowIndex, CurColIndex, CurRowIndex, CurColIndex + 3, OfficeOpenXml.Style.ExcelHorizontalAlignment.Left, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
                EPP_ExcelManager.SetAlignment(sheet, CurRowIndex, CurColIndex + 4, CurRowIndex, CurColIndex + 15, OfficeOpenXml.Style.ExcelHorizontalAlignment.Center, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
                EPP_ExcelManager.MergeAndSet(sheet, CurRowIndex, CurColIndex + 4, CurRowIndex, CurColIndex + 15, (treatVol > 0) ? (sumTotalCost / treatVol).ToString() : "Undefined");

                CurRowIndex++;
                //~~~~~~~~~~~~~~~~~~~
                int sectionEndRowIndex = CurRowIndex - 1;

                EPP_ExcelManager.SetInBorder(sheet, sectionStartRowIndex, CurColIndex, sectionEndRowIndex, CurColIndex + 15, Color.Black, OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                EPP_ExcelManager.SetOutBorder(sheet, sectionStartRowIndex, CurColIndex, sectionEndRowIndex, CurColIndex + 15, Color.Black, OfficeOpenXml.Style.ExcelBorderStyle.Thin);

                EPP_ExcelManager.SetBackColor_AlternatingRows(sheet, bodyStartRowIndex, CurColIndex, bodyEndRowIndex, CurColIndex + 15, Color.White, Color.FromArgb(255, 204, 204), true);

                if (bodyStartRowIndex <= bodyEndRowIndex)
                    EPP_ExcelManager.SetAlignment(sheet, bodyStartRowIndex, CurColIndex, bodyEndRowIndex, CurColIndex + 15, OfficeOpenXml.Style.ExcelHorizontalAlignment.Center, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);

                return dataRowIndex + 1;
            }
            catch (Exception)
            {
                return -1024;
            }
        }
        //-------------------------------------------------------
        int/*next data row index to read*/ BuiltAndTreat_Section(ExcelWorksheet sheet, DataSet ds, int dataRowIndex/*indicator row index*/)
        {
            try
            {
                int sectionStartRowIndex = CurRowIndex;
                const int colsCountForDFS = 16;
                //~~~~~~~~~~~~~~~~~~~~~~~~
                //Title
                ExcelRange rangeTitle = EPP_ExcelManager.GetRange(sheet, CurRowIndex, CurColIndex, CurRowIndex, CurColIndex + colsCountForDFS - 1);
                EPP_ExcelManager.SetBackColor(sheet, rangeTitle, Color.Black);
                EPP_ExcelManager.SetForeColor(sheet, rangeTitle, Color.White);
                EPP_ExcelManager.SetAlignment(sheet, rangeTitle, OfficeOpenXml.Style.ExcelHorizontalAlignment.Center, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
                EPP_ExcelManager.SetFont(sheet, rangeTitle, 12, "Times New Roman", false, false);
                EPP_ExcelManager.MergeAndSet(sheet, rangeTitle, "Built and Treatment");
                CurRowIndex++;
                //~~~~~~~~~~~~~~~~~~~~~~~~
                //Columns Headers
                PrepareCommonHeaderOfSections(sheet, Color.FromArgb(255, 153, 153));
                CurRowIndex++;

                int bodyStartRowIndex = CurRowIndex;

                int[] logicalHeadID = GetLogicalHeaderColIndices();
                //~~~~~~~~~~~~~~~~~~~~~~~~
                bool endOfSectionFound = false;
                const int builtAndTreatVolIndicator = -4100;

                decimal sumTotalCost = 0;
                decimal builtAndTreatVol = 0;

                do
                {
                    dataRowIndex++;
                    int item = Convert.ToInt32(ds.Tables[0].Rows[dataRowIndex][0]);
                    if (item == builtAndTreatVolIndicator)
                    {
                        endOfSectionFound = true;
                        builtAndTreatVol = Convert.ToDecimal(ds.Tables[0].Rows[dataRowIndex][4]);
                    }
                    else if (item < 0)
                    {
                        return -8192;
                    }
                    else
                    {
                        for (int c = 0; c < logicalHeadID.Length - 1; c++)
                            EPP_ExcelManager.Merge(sheet, CurRowIndex, logicalHeadID[c], CurRowIndex, logicalHeadID[c + 1] - 1);

                        EPP_ExcelManager.SetText(sheet, CurRowIndex, logicalHeadID[0], item.ToString()); //Item
                        EPP_ExcelManager.SetText(sheet, CurRowIndex, logicalHeadID[1], ds.Tables[0].Rows[dataRowIndex][1].ToString()); //Code
                        EPP_ExcelManager.SetText(sheet, CurRowIndex, logicalHeadID[2], ds.Tables[0].Rows[dataRowIndex][2].ToString()); //Product Name
                        EPP_ExcelManager.SetText(sheet, CurRowIndex, logicalHeadID[3], ds.Tables[0].Rows[dataRowIndex][3].ToString()); //Unit Size

                        decimal UnitCost = Convert.ToDecimal(ds.Tables[0].Rows[dataRowIndex][4]);
                        EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, logicalHeadID[4], UnitCost); //Unit Cost

                        decimal usedPdf = Convert.ToDecimal(ds.Tables[0].Rows[dataRowIndex][5]);
                        decimal usedOtherComp = Convert.ToDecimal(ds.Tables[0].Rows[dataRowIndex][6]);
                        EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, logicalHeadID[5], usedPdf); //Used-PDF
                        EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, logicalHeadID[6], usedOtherComp); //Used-Other Company
                        EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, logicalHeadID[7], usedPdf + usedOtherComp); //Used Total

                        decimal concPdf = Convert.ToDecimal(ds.Tables[0].Rows[dataRowIndex][7]);
                        decimal concOtherComp = Convert.ToDecimal(ds.Tables[0].Rows[dataRowIndex][8]);
                        EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, logicalHeadID[8], concPdf); //Concentration-PDF
                        EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, logicalHeadID[9], concOtherComp); //Concentration-Other Company
                        EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, logicalHeadID[10], concPdf + concOtherComp); //Concentration

                        decimal TotalCost = usedPdf * UnitCost;
                        sumTotalCost += TotalCost;
                        EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, logicalHeadID[11], TotalCost); //Total Cost 

                        CurRowIndex++;
                    }
                }
                while (!endOfSectionFound && dataRowIndex < ds.Tables[0].Rows.Count);

                if (!endOfSectionFound)
                    return -2048;

                int bodyEndRowIndex = CurRowIndex - 1;

                //~~~~~~~~Footer~~~~~~~~~~~
                EPP_ExcelManager.SetBackColor(sheet, CurRowIndex, CurColIndex, CurRowIndex + 1, CurColIndex + colsCountForDFS - 1, Color.FromArgb(255, 153, 153));
                //~~~~~~~~~~~~~~~~~~~
                EPP_ExcelManager.MergeAndSet(sheet, CurRowIndex, CurColIndex, CurRowIndex, CurColIndex + 3, "Built Volume + Added Vol inTreatment (" + selUnitVol + ")   =");
                EPP_ExcelManager.SetAlignment(sheet, CurRowIndex, CurColIndex, CurRowIndex, CurColIndex + 3, OfficeOpenXml.Style.ExcelHorizontalAlignment.Left, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
                EPP_ExcelManager.SetAlignment(sheet, CurRowIndex, CurColIndex + 4, CurRowIndex, CurColIndex + 5, OfficeOpenXml.Style.ExcelHorizontalAlignment.Center, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
                EPP_ExcelManager.MergeAndSet(sheet, CurRowIndex, CurColIndex + 4, CurRowIndex, CurColIndex + 5, builtAndTreatVol);

                EPP_ExcelManager.MergeAndSet(sheet, CurRowIndex, CurColIndex + 6, CurRowIndex, CurColIndex + 14, "Mud Cost(" + selCurrency + ")   =");
                EPP_ExcelManager.SetAlignment(sheet, CurRowIndex, CurColIndex + 6, CurRowIndex, CurColIndex + 14, OfficeOpenXml.Style.ExcelHorizontalAlignment.Left, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
                EPP_ExcelManager.SetAlignment(sheet, CurRowIndex, CurColIndex + 15, CurRowIndex, CurColIndex + 15, OfficeOpenXml.Style.ExcelHorizontalAlignment.Center, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
                EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, CurColIndex + 15, sumTotalCost);

                CurRowIndex++;
                //~~~~~~~~~~~~~~~~~~~
                EPP_ExcelManager.MergeAndSet(sheet, CurRowIndex, CurColIndex, CurRowIndex, CurColIndex + 3, "Mud Cost/Vol (" + selCurrency + "/ " + selUnitVol + ")=");
                EPP_ExcelManager.SetAlignment(sheet, CurRowIndex, CurColIndex, CurRowIndex, CurColIndex + 3, OfficeOpenXml.Style.ExcelHorizontalAlignment.Left, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
                EPP_ExcelManager.SetAlignment(sheet, CurRowIndex, CurColIndex + 4, CurRowIndex, CurColIndex + 15, OfficeOpenXml.Style.ExcelHorizontalAlignment.Center, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
                EPP_ExcelManager.MergeAndSet(sheet, CurRowIndex, CurColIndex + 4, CurRowIndex, CurColIndex + 15, (builtAndTreatVol > 0) ? (sumTotalCost / builtAndTreatVol).ToString() : "Undefined");

                CurRowIndex++;
                //~~~~~~~~~~~~~~~~~~~
                int sectionEndRowIndex = CurRowIndex - 1;

                EPP_ExcelManager.SetInBorder(sheet, sectionStartRowIndex, CurColIndex, sectionEndRowIndex, CurColIndex + 15, Color.Black, OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                EPP_ExcelManager.SetOutBorder(sheet, sectionStartRowIndex, CurColIndex, sectionEndRowIndex, CurColIndex + 15, Color.Black, OfficeOpenXml.Style.ExcelBorderStyle.Thin);

                EPP_ExcelManager.SetBackColor_AlternatingRows(sheet, bodyStartRowIndex, CurColIndex, bodyEndRowIndex, CurColIndex + 15, Color.White, Color.FromArgb(255, 204, 204), true);

                if (bodyStartRowIndex <= bodyEndRowIndex)
                    EPP_ExcelManager.SetAlignment(sheet, bodyStartRowIndex, CurColIndex, bodyEndRowIndex, CurColIndex + 15, OfficeOpenXml.Style.ExcelHorizontalAlignment.Center, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);

                return dataRowIndex + 1;
            }
            catch (Exception)
            {
                return -1024;
            }
        }
        //-------------------------------------------------------
        int/*next data row index to read*/ UsedFor_Section(ExcelWorksheet sheet, DataSet ds, int dataRowIndex/*indicator row index*/)
        {
            try
            {
                int sectionStartRowIndex = CurRowIndex;
                const int colsCountForDFS = 16;
                //~~~~~~~~~~~~~~~~~~~~~~~~
                string dfs = ds.Tables[0].Rows[dataRowIndex][2].ToString();

                //Title
                ExcelRange rangeTitle = EPP_ExcelManager.GetRange(sheet, CurRowIndex, CurColIndex, CurRowIndex, CurColIndex + colsCountForDFS - 1);
                EPP_ExcelManager.SetBackColor(sheet, rangeTitle, Color.FromArgb(84, 130, 53));
                EPP_ExcelManager.SetForeColor(sheet, rangeTitle, Color.White);
                EPP_ExcelManager.SetAlignment(sheet, rangeTitle, OfficeOpenXml.Style.ExcelHorizontalAlignment.Center, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
                EPP_ExcelManager.SetFont(sheet, rangeTitle, 12, "Times New Roman", false, false);
                EPP_ExcelManager.MergeAndSet(sheet, rangeTitle, "Used for " + dfs);
                CurRowIndex++;
                //~~~~~~~~~~~~~~~~~~~~~~~~
                //Columns Headers
                PrepareCommonHeaderOfSections(sheet, Color.FromArgb(198, 224, 180));
                CurRowIndex++;

                int bodyStartRowIndex = CurRowIndex;

                int[] logicalHeadID = GetLogicalHeaderColIndices();
                //~~~~~~~~~~~~~~~~~~~~~~~~
                bool endOfSectionFound = false;
                const int usedForVolIndicator = -5100;

                decimal sumTotalCost = 0;
                decimal usedForVol = 0;

                do
                {
                    dataRowIndex++;
                    int item = Convert.ToInt32(ds.Tables[0].Rows[dataRowIndex][0]);
                    if (item == usedForVolIndicator)
                    {
                        endOfSectionFound = true;
                        usedForVol = Convert.ToDecimal(ds.Tables[0].Rows[dataRowIndex][4]);
                    }
                    else if (item < 0)
                    {
                        return -8192;
                    }
                    else
                    {
                        for (int c = 0; c < logicalHeadID.Length - 1; c++)
                            EPP_ExcelManager.Merge(sheet, CurRowIndex, logicalHeadID[c], CurRowIndex, logicalHeadID[c + 1] - 1);

                        EPP_ExcelManager.SetText(sheet, CurRowIndex, logicalHeadID[0], item.ToString()); //Item
                        EPP_ExcelManager.SetText(sheet, CurRowIndex, logicalHeadID[1], ds.Tables[0].Rows[dataRowIndex][1].ToString()); //Code
                        EPP_ExcelManager.SetText(sheet, CurRowIndex, logicalHeadID[2], ds.Tables[0].Rows[dataRowIndex][2].ToString()); //Product Name
                        EPP_ExcelManager.SetText(sheet, CurRowIndex, logicalHeadID[3], ds.Tables[0].Rows[dataRowIndex][3].ToString()); //Unit Size

                        decimal UnitCost = Convert.ToDecimal(ds.Tables[0].Rows[dataRowIndex][4]);
                        EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, logicalHeadID[4], UnitCost); //Unit Cost

                        decimal usedPdf = Convert.ToDecimal(ds.Tables[0].Rows[dataRowIndex][5]);
                        decimal usedOtherComp = Convert.ToDecimal(ds.Tables[0].Rows[dataRowIndex][6]);
                        EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, logicalHeadID[5], usedPdf); //Used-PDF
                        EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, logicalHeadID[6], usedOtherComp); //Used-Other Company
                        EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, logicalHeadID[7], usedPdf + usedOtherComp); //Used Total

                        decimal concPdf = Convert.ToDecimal(ds.Tables[0].Rows[dataRowIndex][7]);
                        decimal concOtherComp = Convert.ToDecimal(ds.Tables[0].Rows[dataRowIndex][8]);
                        EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, logicalHeadID[8], concPdf); //Concentration-PDF
                        EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, logicalHeadID[9], concOtherComp); //Concentration-Other Company
                        EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, logicalHeadID[10], concPdf + concOtherComp); //Concentration

                        decimal TotalCost = usedPdf * UnitCost;
                        sumTotalCost += TotalCost;
                        EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, logicalHeadID[11], TotalCost); //Total Cost 

                        CurRowIndex++;
                    }
                }
                while (!endOfSectionFound && dataRowIndex < ds.Tables[0].Rows.Count);

                if (!endOfSectionFound)
                    return -2048;

                int bodyEndRowIndex = CurRowIndex - 1;

                //~~~~~~~~Footer~~~~~~~~~~~
                EPP_ExcelManager.SetBackColor(sheet, CurRowIndex, CurColIndex, CurRowIndex + 1, CurColIndex + colsCountForDFS - 1, Color.FromArgb(198, 224, 180));
                //~~~~~~~~~~~~~~~~~~~
                EPP_ExcelManager.MergeAndSet(sheet, CurRowIndex, CurColIndex, CurRowIndex, CurColIndex + 3, "Final Volume (" + selUnitVol + ")   =");
                EPP_ExcelManager.SetAlignment(sheet, CurRowIndex, CurColIndex, CurRowIndex, CurColIndex + 3, OfficeOpenXml.Style.ExcelHorizontalAlignment.Left, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
                EPP_ExcelManager.SetAlignment(sheet, CurRowIndex, CurColIndex + 4, CurRowIndex, CurColIndex + 5, OfficeOpenXml.Style.ExcelHorizontalAlignment.Center, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
                EPP_ExcelManager.MergeAndSet(sheet, CurRowIndex, CurColIndex + 4, CurRowIndex, CurColIndex + 5, usedForVol);
                sumUsedForVol += usedForVol;

                EPP_ExcelManager.MergeAndSet(sheet, CurRowIndex, CurColIndex + 6, CurRowIndex, CurColIndex + 14, "Mud Cost(" + selCurrency + ")   =");
                EPP_ExcelManager.SetAlignment(sheet, CurRowIndex, CurColIndex + 6, CurRowIndex, CurColIndex + 14, OfficeOpenXml.Style.ExcelHorizontalAlignment.Left, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
                EPP_ExcelManager.SetAlignment(sheet, CurRowIndex, CurColIndex + 15, CurRowIndex, CurColIndex + 15, OfficeOpenXml.Style.ExcelHorizontalAlignment.Center, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
                EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, CurColIndex + 15, sumTotalCost);
                sumUsedForCost += sumTotalCost;

                CurRowIndex++;
                //~~~~~~~~~~~~~~~~~~~
                EPP_ExcelManager.MergeAndSet(sheet, CurRowIndex, CurColIndex, CurRowIndex, CurColIndex + 3, "Mud Cost/Final Volume (" + selCurrency + "/ " + selUnitVol + ")=");
                EPP_ExcelManager.SetAlignment(sheet, CurRowIndex, CurColIndex, CurRowIndex, CurColIndex + 3, OfficeOpenXml.Style.ExcelHorizontalAlignment.Left, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
                EPP_ExcelManager.SetAlignment(sheet, CurRowIndex, CurColIndex + 4, CurRowIndex, CurColIndex + 15, OfficeOpenXml.Style.ExcelHorizontalAlignment.Center, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
                EPP_ExcelManager.MergeAndSet(sheet, CurRowIndex, CurColIndex + 4, CurRowIndex, CurColIndex + 15, (usedForVol > 0) ? (sumTotalCost / usedForVol).ToString() : "Undefined");

                CurRowIndex++;
                //~~~~~~~~~~~~~~~~~~~
                int sectionEndRowIndex = CurRowIndex - 1;

                EPP_ExcelManager.SetInBorder(sheet, sectionStartRowIndex, CurColIndex, sectionEndRowIndex, CurColIndex + 15, Color.Black, OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                EPP_ExcelManager.SetOutBorder(sheet, sectionStartRowIndex, CurColIndex, sectionEndRowIndex, CurColIndex + 15, Color.Black, OfficeOpenXml.Style.ExcelBorderStyle.Thin);

                EPP_ExcelManager.SetBackColor_AlternatingRows(sheet, bodyStartRowIndex, CurColIndex, bodyEndRowIndex, CurColIndex + 15, Color.White, Color.FromArgb(226, 239, 218), true);

                if (bodyStartRowIndex <= bodyEndRowIndex)
                    EPP_ExcelManager.SetAlignment(sheet, bodyStartRowIndex, CurColIndex, bodyEndRowIndex, CurColIndex + 15, OfficeOpenXml.Style.ExcelHorizontalAlignment.Center, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);

                return dataRowIndex + 1;
            }
            catch (Exception)
            {
                return -1024;
            }
        }
        //-------------------------------------------------------
        int/*next data row index to read*/ Using_Section(ExcelWorksheet sheet, DataSet ds, int dataRowIndex/*indicator row index*/)
        {
            try
            {
                int sectionStartRowIndex = CurRowIndex;
                const int colsCountForDFS = 16;
                //~~~~~~~~~~~~~~~~~~~~~~~~
                string dfs = ds.Tables[0].Rows[dataRowIndex][2].ToString();

                //Title
                ExcelRange rangeTitle = EPP_ExcelManager.GetRange(sheet, CurRowIndex, CurColIndex, CurRowIndex, CurColIndex + colsCountForDFS - 1);
                EPP_ExcelManager.SetBackColor(sheet, rangeTitle, Color.FromArgb(48, 84, 150));
                EPP_ExcelManager.SetForeColor(sheet, rangeTitle, Color.White);
                EPP_ExcelManager.SetAlignment(sheet, rangeTitle, OfficeOpenXml.Style.ExcelHorizontalAlignment.Center, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
                EPP_ExcelManager.SetFont(sheet, rangeTitle, 12, "Times New Roman", false, false);
                EPP_ExcelManager.MergeAndSet(sheet, rangeTitle, "Using " + dfs);
                CurRowIndex++;
                //~~~~~~~~~~~~~~~~~~~~~~~~
                //Columns Headers
                PrepareCommonHeaderOfSections(sheet, Color.FromArgb(180, 198, 231));
                EPP_ExcelManager.MergeAndSet(sheet, CurRowIndex, CurColIndex + 9, CurRowIndex, CurColIndex + 14, "");
                CurRowIndex++;

                int bodyStartRowIndex = CurRowIndex;

                int[] logicalHeadID = GetLogicalHeaderColIndices();
                //~~~~~~~~~~~~~~~~~~~~~~~~
                bool endOfSectionFound = false;
                const int usingVolIndicator = -6100;

                decimal sumTotalCost = 0;
                decimal usingVol = 0;

                do
                {
                    dataRowIndex++;
                    int item = Convert.ToInt32(ds.Tables[0].Rows[dataRowIndex][0]);
                    if (item == usingVolIndicator)
                    {
                        endOfSectionFound = true;
                        usingVol = Convert.ToDecimal(ds.Tables[0].Rows[dataRowIndex][4]);
                    }
                    else if (item < 0)
                    {
                        return -8192;
                    }
                    else
                    {
                        for (int c = 0; c < logicalHeadID.Length - 1; c++)
                            EPP_ExcelManager.Merge(sheet, CurRowIndex, logicalHeadID[c], CurRowIndex, logicalHeadID[c + 1] - 1);

                        EPP_ExcelManager.SetText(sheet, CurRowIndex, logicalHeadID[0], item.ToString()); //Item
                        EPP_ExcelManager.SetText(sheet, CurRowIndex, logicalHeadID[1], ds.Tables[0].Rows[dataRowIndex][1].ToString()); //Code
                        EPP_ExcelManager.SetText(sheet, CurRowIndex, logicalHeadID[2], ds.Tables[0].Rows[dataRowIndex][2].ToString()); //Product Name
                        EPP_ExcelManager.SetText(sheet, CurRowIndex, logicalHeadID[3], ds.Tables[0].Rows[dataRowIndex][3].ToString()); //Unit Size

                        decimal UnitCost = Convert.ToDecimal(ds.Tables[0].Rows[dataRowIndex][4]);
                        EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, logicalHeadID[4], UnitCost); //Unit Cost

                        decimal usedPdf = Convert.ToDecimal(ds.Tables[0].Rows[dataRowIndex][5]);
                        decimal usedOtherComp = Convert.ToDecimal(ds.Tables[0].Rows[dataRowIndex][6]);
                        EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, logicalHeadID[5], usedPdf); //Used-PDF
                        EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, logicalHeadID[6], usedOtherComp); //Used-Other Company
                        EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, logicalHeadID[7], usedPdf + usedOtherComp); //Used Total


                        EPP_ExcelManager.MergeAndSet(sheet, CurRowIndex, CurColIndex + 9, CurRowIndex, CurColIndex + 14, "");
                        //decimal concPdf = Convert.ToDecimal(ds.Tables[0].Rows[dataRowIndex][7]);
                        //decimal concOtherComp = Convert.ToDecimal(ds.Tables[0].Rows[dataRowIndex][8]);
                        //EPP_ExcelManager.SetDecimalValue(sheet, sheet1CurRowIndex, logicalHeadID[8], concPdf); //Concentration-PDF
                        //EPP_ExcelManager.SetDecimalValue(sheet, sheet1CurRowIndex, logicalHeadID[9], concOtherComp); //Concentration-Other Company
                        //EPP_ExcelManager.SetDecimalValue(sheet, sheet1CurRowIndex, logicalHeadID[10], concPdf + concOtherComp); //Concentration

                        decimal TotalCost = usedPdf * UnitCost;
                        sumTotalCost += TotalCost;
                        EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, logicalHeadID[11], TotalCost); //Total Cost 

                        CurRowIndex++;
                    }
                }
                while (!endOfSectionFound && dataRowIndex < ds.Tables[0].Rows.Count);

                if (!endOfSectionFound)
                    return -2048;

                int bodyEndRowIndex = CurRowIndex - 1;

                //~~~~~~~~Footer~~~~~~~~~~~
                EPP_ExcelManager.SetBackColor(sheet, CurRowIndex, CurColIndex, CurRowIndex + 1, CurColIndex + colsCountForDFS - 1, Color.FromArgb(180, 198, 231));
                //~~~~~~~~~~~~~~~~~~~
                EPP_ExcelManager.MergeAndSet(sheet, CurRowIndex, CurColIndex, CurRowIndex, CurColIndex + 3, "Used Volume (" + selUnitVol + ")   =");
                EPP_ExcelManager.SetAlignment(sheet, CurRowIndex, CurColIndex, CurRowIndex, CurColIndex + 3, OfficeOpenXml.Style.ExcelHorizontalAlignment.Left, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
                EPP_ExcelManager.SetAlignment(sheet, CurRowIndex, CurColIndex + 4, CurRowIndex, CurColIndex + 5, OfficeOpenXml.Style.ExcelHorizontalAlignment.Center, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
                EPP_ExcelManager.MergeAndSet(sheet, CurRowIndex, CurColIndex + 4, CurRowIndex, CurColIndex + 5, usingVol);
                sumUsingVol += usingVol;

                EPP_ExcelManager.MergeAndSet(sheet, CurRowIndex, CurColIndex + 6, CurRowIndex, CurColIndex + 14, "Total Cost(" + selCurrency + ")   =");
                EPP_ExcelManager.SetAlignment(sheet, CurRowIndex, CurColIndex + 6, CurRowIndex, CurColIndex + 14, OfficeOpenXml.Style.ExcelHorizontalAlignment.Left, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
                EPP_ExcelManager.SetAlignment(sheet, CurRowIndex, CurColIndex + 15, CurRowIndex, CurColIndex + 15, OfficeOpenXml.Style.ExcelHorizontalAlignment.Center, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
                EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, CurColIndex + 15, sumTotalCost);
                sumUsingCost += sumTotalCost;

                CurRowIndex++;
                //~~~~~~~~~~~~~~~~~~~
                EPP_ExcelManager.MergeAndSet(sheet, CurRowIndex, CurColIndex, CurRowIndex, CurColIndex + 3, "Mud Cost/ Total Vol (" + selCurrency + "/ " + selUnitVol + ")=");
                EPP_ExcelManager.SetAlignment(sheet, CurRowIndex, CurColIndex, CurRowIndex, CurColIndex + 3, OfficeOpenXml.Style.ExcelHorizontalAlignment.Left, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
                EPP_ExcelManager.SetAlignment(sheet, CurRowIndex, CurColIndex + 4, CurRowIndex, CurColIndex + 15, OfficeOpenXml.Style.ExcelHorizontalAlignment.Center, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
                EPP_ExcelManager.MergeAndSet(sheet, CurRowIndex, CurColIndex + 4, CurRowIndex, CurColIndex + 15, (usingVol > 0) ? (sumTotalCost / usingVol).ToString() : "Undefined");

                CurRowIndex++;
                //~~~~~~~~~~~~~~~~~~~
                int sectionEndRowIndex = CurRowIndex - 1;

                EPP_ExcelManager.SetInBorder(sheet, sectionStartRowIndex, CurColIndex, sectionEndRowIndex, CurColIndex + 15, Color.Black, OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                EPP_ExcelManager.SetOutBorder(sheet, sectionStartRowIndex, CurColIndex, sectionEndRowIndex, CurColIndex + 15, Color.Black, OfficeOpenXml.Style.ExcelBorderStyle.Thin);

                EPP_ExcelManager.SetBackColor_AlternatingRows(sheet, bodyStartRowIndex, CurColIndex, bodyEndRowIndex, CurColIndex + 15, Color.White, Color.FromArgb(221, 235, 247), true);

                if (bodyStartRowIndex <= bodyEndRowIndex)
                    EPP_ExcelManager.SetAlignment(sheet, bodyStartRowIndex, CurColIndex, bodyEndRowIndex, CurColIndex + 15, OfficeOpenXml.Style.ExcelHorizontalAlignment.Center, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);

                return dataRowIndex + 1;
            }
            catch (Exception)
            {
                return -1024;
            }
        }
        //-------------------------------------------------------
        int/*next data row index to read*/ Final_Section(ExcelWorksheet sheet, DataSet ds, int dataRowIndex/*indicator row index*/)
        {
            try
            {
                int sectionStartRowIndex = CurRowIndex;
                const int colsCountForDFS = 16;
                //~~~~~~~~~~~~~~~~~~~~~~~~
                //Title
                ExcelRange rangeTitle = EPP_ExcelManager.GetRange(sheet, CurRowIndex, CurColIndex, CurRowIndex, CurColIndex + colsCountForDFS - 1);
                EPP_ExcelManager.SetBackColor(sheet, rangeTitle, Color.Black);
                EPP_ExcelManager.SetForeColor(sheet, rangeTitle, Color.White);
                EPP_ExcelManager.SetAlignment(sheet, rangeTitle, OfficeOpenXml.Style.ExcelHorizontalAlignment.Center, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
                EPP_ExcelManager.SetFont(sheet, rangeTitle, 12, "Times New Roman", false, false);
                EPP_ExcelManager.MergeAndSet(sheet, rangeTitle, "Final Table");
                CurRowIndex++;
                //~~~~~~~~~~~~~~~~~~~~~~~~
                //Columns Headers
                PrepareCommonHeaderOfSections(sheet, Color.FromArgb(255, 153, 153));
                CurRowIndex++;

                int bodyStartRowIndex = CurRowIndex;

                int[] logicalHeadID = GetLogicalHeaderColIndices();
                //~~~~~~~~~~~~~~~~~~~~~~~~
                bool endOfSectionFound = false;
                const int finalVolIndicator = -9100;

                decimal sumTotalCost = 0;
                decimal finalVol = 0;

                do
                {
                    dataRowIndex++;
                    int item = Convert.ToInt32(ds.Tables[0].Rows[dataRowIndex][0]);

                    if (item == finalVolIndicator)
                    {
                        endOfSectionFound = true;
                        finalVol = Convert.ToDecimal(ds.Tables[0].Rows[dataRowIndex][4]);
                    }
                    else if (item < 0)
                    {
                        return -8192;
                    }
                    else
                    {
                        for (int c = 0; c < logicalHeadID.Length - 1; c++)
                            EPP_ExcelManager.Merge(sheet, CurRowIndex, logicalHeadID[c], CurRowIndex, logicalHeadID[c + 1] - 1);

                        EPP_ExcelManager.SetText(sheet, CurRowIndex, logicalHeadID[0], item.ToString()); //Item
                        EPP_ExcelManager.SetText(sheet, CurRowIndex, logicalHeadID[1], ds.Tables[0].Rows[dataRowIndex][1].ToString()); //Code
                        EPP_ExcelManager.SetText(sheet, CurRowIndex, logicalHeadID[2], ds.Tables[0].Rows[dataRowIndex][2].ToString()); //Product Name
                        EPP_ExcelManager.SetText(sheet, CurRowIndex, logicalHeadID[3], ds.Tables[0].Rows[dataRowIndex][3].ToString()); //Unit Size

                        decimal UnitCost = Convert.ToDecimal(ds.Tables[0].Rows[dataRowIndex][4]);
                        EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, logicalHeadID[4], UnitCost); //Unit Cost

                        decimal usedPdf = Convert.ToDecimal(ds.Tables[0].Rows[dataRowIndex][5]);
                        decimal usedOtherComp = Convert.ToDecimal(ds.Tables[0].Rows[dataRowIndex][6]);
                        EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, logicalHeadID[5], usedPdf); //Used-PDF
                        EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, logicalHeadID[6], usedOtherComp); //Used-Other Company
                        EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, logicalHeadID[7], usedPdf + usedOtherComp); //Used Total

                        decimal concPdf = Convert.ToDecimal(ds.Tables[0].Rows[dataRowIndex][7]);
                        decimal concOtherComp = Convert.ToDecimal(ds.Tables[0].Rows[dataRowIndex][8]);
                        EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, logicalHeadID[8], concPdf); //Concentration-PDF
                        EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, logicalHeadID[9], concOtherComp); //Concentration-Other Company
                        EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, logicalHeadID[10], concPdf + concOtherComp); //Concentration

                        decimal TotalCost = usedPdf * UnitCost;
                        sumTotalCost += TotalCost;
                        EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, logicalHeadID[11], TotalCost); //Total Cost 

                        CurRowIndex++;
                    }
                }
                while (!endOfSectionFound && dataRowIndex < ds.Tables[0].Rows.Count);

                if (!endOfSectionFound)
                    return -2048;

                int bodyEndRowIndex = CurRowIndex - 1;

                //~~~~~~~~Footer~~~~~~~~~~~
                EPP_ExcelManager.SetBackColor(sheet, CurRowIndex, CurColIndex, CurRowIndex + 1, CurColIndex + colsCountForDFS - 1, Color.FromArgb(255, 153, 153));
                //~~~~~~~~~~~~~~~~~~~
                EPP_ExcelManager.MergeAndSet(sheet, CurRowIndex, CurColIndex, CurRowIndex, CurColIndex + 3, "Final Volume (" + selUnitVol + ")   =");
                EPP_ExcelManager.SetAlignment(sheet, CurRowIndex, CurColIndex, CurRowIndex, CurColIndex + 3, OfficeOpenXml.Style.ExcelHorizontalAlignment.Left, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
                EPP_ExcelManager.SetAlignment(sheet, CurRowIndex, CurColIndex + 4, CurRowIndex, CurColIndex + 5, OfficeOpenXml.Style.ExcelHorizontalAlignment.Center, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
                EPP_ExcelManager.MergeAndSet(sheet, CurRowIndex, CurColIndex + 4, CurRowIndex, CurColIndex + 5, finalVol);
                sumFinalVol = finalVol;

                EPP_ExcelManager.MergeAndSet(sheet, CurRowIndex, CurColIndex + 6, CurRowIndex, CurColIndex + 14, "Mud Cost(" + selCurrency + ")   =");
                EPP_ExcelManager.SetAlignment(sheet, CurRowIndex, CurColIndex + 6, CurRowIndex, CurColIndex + 14, OfficeOpenXml.Style.ExcelHorizontalAlignment.Left, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
                EPP_ExcelManager.SetAlignment(sheet, CurRowIndex, CurColIndex + 15, CurRowIndex, CurColIndex + 15, OfficeOpenXml.Style.ExcelHorizontalAlignment.Center, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
                EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, CurColIndex + 15, sumTotalCost);
                sumFinalCost = sumTotalCost;

                CurRowIndex++;
                //~~~~~~~~~~~~~~~~~~~
                EPP_ExcelManager.MergeAndSet(sheet, CurRowIndex, CurColIndex, CurRowIndex, CurColIndex + 3, "Mud Cost/Vol (" + selCurrency + "/ " + selUnitVol + ")=");
                EPP_ExcelManager.SetAlignment(sheet, CurRowIndex, CurColIndex, CurRowIndex, CurColIndex + 3, OfficeOpenXml.Style.ExcelHorizontalAlignment.Left, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
                EPP_ExcelManager.SetAlignment(sheet, CurRowIndex, CurColIndex + 4, CurRowIndex, CurColIndex + 15, OfficeOpenXml.Style.ExcelHorizontalAlignment.Center, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
                EPP_ExcelManager.MergeAndSet(sheet, CurRowIndex, CurColIndex + 4, CurRowIndex, CurColIndex + 15, (finalVol > 0) ? (sumTotalCost / finalVol).ToString() : "Undefined");

                CurRowIndex++;
                //~~~~~~~~~~~~~~~~~~~
                int sectionEndRowIndex = CurRowIndex - 1;

                EPP_ExcelManager.SetInBorder(sheet, sectionStartRowIndex, CurColIndex, sectionEndRowIndex, CurColIndex + 15, Color.Black, OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                EPP_ExcelManager.SetOutBorder(sheet, sectionStartRowIndex, CurColIndex, sectionEndRowIndex, CurColIndex + 15, Color.Black, OfficeOpenXml.Style.ExcelBorderStyle.Thin);

                EPP_ExcelManager.SetBackColor_AlternatingRows(sheet, bodyStartRowIndex, CurColIndex, bodyEndRowIndex, CurColIndex + 15, Color.White, Color.FromArgb(255, 204, 204), true);

                if (bodyStartRowIndex <= bodyEndRowIndex)
                    EPP_ExcelManager.SetAlignment(sheet, bodyStartRowIndex, CurColIndex, bodyEndRowIndex, CurColIndex + 15, OfficeOpenXml.Style.ExcelHorizontalAlignment.Center, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);

                return dataRowIndex + 1;
            }
            catch (Exception)
            {
                return -1024;
            }
        }
        //-------------------------------------------------------
        int/*next data row index to read*/ Transfer_Section(ExcelWorksheet sheet, DataSet ds, int dataRowIndex/*indicator row index*/)
        {
            try
            {
                int sectionStartRowIndex = CurRowIndex;
                const int colsCountForDFS = 16;
                //~~~~~~~~~~~~~~~~~~~~~~~~
                //Title
                ExcelRange rangeTitle = EPP_ExcelManager.GetRange(sheet, CurRowIndex, CurColIndex, CurRowIndex, CurColIndex + colsCountForDFS - 1);
                EPP_ExcelManager.SetBackColor(sheet, rangeTitle, Color.Black);
                EPP_ExcelManager.SetForeColor(sheet, rangeTitle, Color.White);
                EPP_ExcelManager.SetAlignment(sheet, rangeTitle, OfficeOpenXml.Style.ExcelHorizontalAlignment.Center, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
                EPP_ExcelManager.SetFont(sheet, rangeTitle, 12, "Times New Roman", false, false);
                EPP_ExcelManager.MergeAndSet(sheet, rangeTitle, "Transferred Mud");
                CurRowIndex++;
                //~~~~~~~~~~~~~~~~~~~~~~~~
                //Columns Headers
                PrepareCommonHeaderOfSections(sheet, Color.FromArgb(255, 153, 153));
                CurRowIndex++;

                int bodyStartRowIndex = CurRowIndex;

                int[] logicalHeadID = GetLogicalHeaderColIndices();
                //~~~~~~~~~~~~~~~~~~~~~~~~
                bool endOfSectionFound = false;
                const int transVolIndicator = -21000;

                decimal sumTotalCost = 0;
                decimal transVol = 0;

                do
                {
                    dataRowIndex++;
                    int item = Convert.ToInt32(ds.Tables[0].Rows[dataRowIndex][0]);
                    if (item == transVolIndicator)
                    {
                        endOfSectionFound = true;
                        transVol = Convert.ToDecimal(ds.Tables[0].Rows[dataRowIndex][4]);
                    }
                    else if (item < 0)
                    {
                        return -8192;
                    }
                    else
                    {
                        for (int c = 0; c < logicalHeadID.Length - 1; c++)
                            EPP_ExcelManager.Merge(sheet, CurRowIndex, logicalHeadID[c], CurRowIndex, logicalHeadID[c + 1] - 1);

                        EPP_ExcelManager.SetText(sheet, CurRowIndex, logicalHeadID[0], item.ToString()); //Item
                        EPP_ExcelManager.SetText(sheet, CurRowIndex, logicalHeadID[1], ds.Tables[0].Rows[dataRowIndex][1].ToString()); //Code
                        EPP_ExcelManager.SetText(sheet, CurRowIndex, logicalHeadID[2], ds.Tables[0].Rows[dataRowIndex][2].ToString()); //Product Name
                        EPP_ExcelManager.SetText(sheet, CurRowIndex, logicalHeadID[3], ds.Tables[0].Rows[dataRowIndex][3].ToString()); //Unit Size

                        decimal UnitCost = Convert.ToDecimal(ds.Tables[0].Rows[dataRowIndex][4]);
                        EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, logicalHeadID[4], UnitCost); //Unit Cost

                        decimal usedPdf = Convert.ToDecimal(ds.Tables[0].Rows[dataRowIndex][5]);
                        decimal usedOtherComp = Convert.ToDecimal(ds.Tables[0].Rows[dataRowIndex][6]);
                        EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, logicalHeadID[5], usedPdf); //Used-PDF
                        EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, logicalHeadID[6], usedOtherComp); //Used-Other Company
                        EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, logicalHeadID[7], usedPdf + usedOtherComp); //Used Total

                        decimal concPdf = Convert.ToDecimal(ds.Tables[0].Rows[dataRowIndex][7]);
                        decimal concOtherComp = Convert.ToDecimal(ds.Tables[0].Rows[dataRowIndex][8]);
                        EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, logicalHeadID[8], concPdf); //Concentration-PDF
                        EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, logicalHeadID[9], concOtherComp); //Concentration-Other Company
                        EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, logicalHeadID[10], concPdf + concOtherComp); //Concentration

                        decimal TotalCost = usedPdf * UnitCost;
                        sumTotalCost += TotalCost;
                        EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, logicalHeadID[11], TotalCost); //Total Cost 

                        CurRowIndex++;
                    }
                }
                while (!endOfSectionFound && dataRowIndex < ds.Tables[0].Rows.Count);

                if (!endOfSectionFound)
                    return -2048;

                int bodyEndRowIndex = CurRowIndex - 1;

                //~~~~~~~~Footer~~~~~~~~~~~
                EPP_ExcelManager.SetBackColor(sheet, CurRowIndex, CurColIndex, CurRowIndex + 1, CurColIndex + colsCountForDFS - 1, Color.FromArgb(255, 153, 153));
                //~~~~~~~~~~~~~~~~~~~
                EPP_ExcelManager.MergeAndSet(sheet, CurRowIndex, CurColIndex, CurRowIndex, CurColIndex + 3, "Transferred Volume (" + selUnitVol + ")   =");
                EPP_ExcelManager.SetAlignment(sheet, CurRowIndex, CurColIndex, CurRowIndex, CurColIndex + 3, OfficeOpenXml.Style.ExcelHorizontalAlignment.Left, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
                EPP_ExcelManager.SetAlignment(sheet, CurRowIndex, CurColIndex + 4, CurRowIndex, CurColIndex + 5, OfficeOpenXml.Style.ExcelHorizontalAlignment.Center, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
                EPP_ExcelManager.MergeAndSet(sheet, CurRowIndex, CurColIndex + 4, CurRowIndex, CurColIndex + 5, transVol);
                sumTransVol = transVol;

                EPP_ExcelManager.MergeAndSet(sheet, CurRowIndex, CurColIndex + 6, CurRowIndex, CurColIndex + 14, "Transferred Mud Cost(" + selCurrency + ")   =");
                EPP_ExcelManager.SetAlignment(sheet, CurRowIndex, CurColIndex + 6, CurRowIndex, CurColIndex + 14, OfficeOpenXml.Style.ExcelHorizontalAlignment.Left, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
                EPP_ExcelManager.SetAlignment(sheet, CurRowIndex, CurColIndex + 15, CurRowIndex, CurColIndex + 15, OfficeOpenXml.Style.ExcelHorizontalAlignment.Center, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
                EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, CurColIndex + 15, sumTotalCost);
                sumTransCost = sumTotalCost;

                CurRowIndex++;
                //~~~~~~~~~~~~~~~~~~~
                EPP_ExcelManager.MergeAndSet(sheet, CurRowIndex, CurColIndex, CurRowIndex, CurColIndex + 3, "Transferred Mud Cost/Vol (" + selCurrency + "/ " + selUnitVol + ")=");
                EPP_ExcelManager.SetAlignment(sheet, CurRowIndex, CurColIndex, CurRowIndex, CurColIndex + 3, OfficeOpenXml.Style.ExcelHorizontalAlignment.Left, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
                EPP_ExcelManager.SetAlignment(sheet, CurRowIndex, CurColIndex + 4, CurRowIndex, CurColIndex + 15, OfficeOpenXml.Style.ExcelHorizontalAlignment.Center, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
                EPP_ExcelManager.MergeAndSet(sheet, CurRowIndex, CurColIndex + 4, CurRowIndex, CurColIndex + 15, (transVol > 0) ? (sumTotalCost / transVol).ToString() : "Undefined");

                CurRowIndex++;
                //~~~~~~~~~~~~~~~~~~~
                int sectionEndRowIndex = CurRowIndex - 1;

                EPP_ExcelManager.SetInBorder(sheet, sectionStartRowIndex, CurColIndex, sectionEndRowIndex, CurColIndex + 15, Color.Black, OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                EPP_ExcelManager.SetOutBorder(sheet, sectionStartRowIndex, CurColIndex, sectionEndRowIndex, CurColIndex + 15, Color.Black, OfficeOpenXml.Style.ExcelBorderStyle.Thin);

                EPP_ExcelManager.SetBackColor_AlternatingRows(sheet, bodyStartRowIndex, CurColIndex, bodyEndRowIndex, CurColIndex + 15, Color.White, Color.FromArgb(255, 204, 204), true);

                if (bodyStartRowIndex <= bodyEndRowIndex)
                    EPP_ExcelManager.SetAlignment(sheet, bodyStartRowIndex, CurColIndex, bodyEndRowIndex, CurColIndex + 15, OfficeOpenXml.Style.ExcelHorizontalAlignment.Center, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);

                return dataRowIndex + 1;
            }
            catch (Exception)
            {
                return -1024;
            }
        }
        //-------------------------------------------------------
        int actualMud_SectionCount = 0;
        int/*next data row index to read*/ ActualMud_Section(ExcelWorksheet sheet, DataSet ds, int dataRowIndex/*indicator row index*/, string dfs)
        {
            try
            {
                int sectionStartRowIndex = CurRowIndex;
                const int colsCountForDFS = 16;
                //~~~~~~~~~~~~~~~~~~~~~~~~
                //Title
                ExcelRange rangeTitle = EPP_ExcelManager.GetRange(sheet, CurRowIndex, CurColIndex, CurRowIndex, CurColIndex + colsCountForDFS - 1);
                EPP_ExcelManager.SetBackColor(sheet, rangeTitle, Color.Black);
                EPP_ExcelManager.SetForeColor(sheet, rangeTitle, Color.White);
                EPP_ExcelManager.SetAlignment(sheet, rangeTitle, OfficeOpenXml.Style.ExcelHorizontalAlignment.Center, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
                EPP_ExcelManager.SetFont(sheet, rangeTitle, 12, "Times New Roman", false, false);
                EPP_ExcelManager.MergeAndSet(sheet, rangeTitle, "Actual Mud");
                CurRowIndex++;
                //~~~~~~~~~~~~~~~~~~~~~~~~
                //Columns Headers
                PrepareCommonHeaderOfSections(sheet, Color.FromArgb(255, 153, 153));
                CurRowIndex++;

                int bodyStartRowIndex = CurRowIndex;

                int[] logicalHeadID = GetLogicalHeaderColIndices();
                //~~~~~~~~~~~~~~~~~~~~~~~~
                bool endOfSectionFound = false;
                const int actualMudVolIndicator = -31000;

                decimal sumTotalCost = 0;
                decimal actualVol = 0;

                do
                {
                    dataRowIndex++;
                    int item = Convert.ToInt32(ds.Tables[0].Rows[dataRowIndex][0]);
                    if (item == actualMudVolIndicator)
                    {
                        endOfSectionFound = true;
                        actualVol = Convert.ToDecimal(ds.Tables[0].Rows[dataRowIndex][4]);
                    }
                    else if (item < 0)
                    {
                        return -8192;
                    }
                    else
                    {
                        for (int c = 0; c < logicalHeadID.Length - 1; c++)
                            EPP_ExcelManager.Merge(sheet, CurRowIndex, logicalHeadID[c], CurRowIndex, logicalHeadID[c + 1] - 1);

                        EPP_ExcelManager.SetText(sheet, CurRowIndex, logicalHeadID[0], item.ToString()); //Item
                        EPP_ExcelManager.SetText(sheet, CurRowIndex, logicalHeadID[1], ds.Tables[0].Rows[dataRowIndex][1].ToString()); //Code
                        EPP_ExcelManager.SetText(sheet, CurRowIndex, logicalHeadID[2], ds.Tables[0].Rows[dataRowIndex][2].ToString()); //Product Name
                        EPP_ExcelManager.SetText(sheet, CurRowIndex, logicalHeadID[3], ds.Tables[0].Rows[dataRowIndex][3].ToString()); //Unit Size

                        decimal UnitCost = Convert.ToDecimal(ds.Tables[0].Rows[dataRowIndex][4]);
                        EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, logicalHeadID[4], UnitCost); //Unit Cost

                        decimal usedPdf = Convert.ToDecimal(ds.Tables[0].Rows[dataRowIndex][5]);
                        decimal usedOtherComp = Convert.ToDecimal(ds.Tables[0].Rows[dataRowIndex][6]);
                        EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, logicalHeadID[5], usedPdf); //Used-PDF
                        EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, logicalHeadID[6], usedOtherComp); //Used-Other Company
                        EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, logicalHeadID[7], usedPdf + usedOtherComp); //Used Total

                        decimal concPdf = Convert.ToDecimal(ds.Tables[0].Rows[dataRowIndex][7]);
                        decimal concOtherComp = Convert.ToDecimal(ds.Tables[0].Rows[dataRowIndex][8]);
                        EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, logicalHeadID[8], concPdf); //Concentration-PDF
                        EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, logicalHeadID[9], concOtherComp); //Concentration-Other Company
                        EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, logicalHeadID[10], concPdf + concOtherComp); //Concentration

                        decimal TotalCost = usedPdf * UnitCost;
                        sumTotalCost += TotalCost;
                        EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, logicalHeadID[11], TotalCost); //Total Cost 

                        CurRowIndex++;
                    }
                }
                while (!endOfSectionFound && dataRowIndex < ds.Tables[0].Rows.Count);

                if (!endOfSectionFound)
                    return -2048;

                int bodyEndRowIndex = CurRowIndex - 1;

                //~~~~~~~~Footer~~~~~~~~~~~
                EPP_ExcelManager.SetBackColor(sheet, CurRowIndex, CurColIndex, CurRowIndex + 1, CurColIndex + colsCountForDFS - 1, Color.FromArgb(255, 153, 153));
                //~~~~~~~~~~~~~~~~~~~
                EPP_ExcelManager.MergeAndSet(sheet, CurRowIndex, CurColIndex, CurRowIndex, CurColIndex + 3, "Actual Volume (" + selUnitVol + ")   =");
                EPP_ExcelManager.SetAlignment(sheet, CurRowIndex, CurColIndex, CurRowIndex, CurColIndex + 3, OfficeOpenXml.Style.ExcelHorizontalAlignment.Left, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
                EPP_ExcelManager.SetAlignment(sheet, CurRowIndex, CurColIndex + 4, CurRowIndex, CurColIndex + 5, OfficeOpenXml.Style.ExcelHorizontalAlignment.Center, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
                EPP_ExcelManager.MergeAndSet(sheet, CurRowIndex, CurColIndex + 4, CurRowIndex, CurColIndex + 5, actualVol);
                sumActualVol = actualVol;

                EPP_ExcelManager.MergeAndSet(sheet, CurRowIndex, CurColIndex + 6, CurRowIndex, CurColIndex + 14, "Actual Mud Cost(" + selCurrency + ")   =");
                EPP_ExcelManager.SetAlignment(sheet, CurRowIndex, CurColIndex + 6, CurRowIndex, CurColIndex + 14, OfficeOpenXml.Style.ExcelHorizontalAlignment.Left, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
                EPP_ExcelManager.SetAlignment(sheet, CurRowIndex, CurColIndex + 15, CurRowIndex, CurColIndex + 15, OfficeOpenXml.Style.ExcelHorizontalAlignment.Center, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
                EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, CurColIndex + 15, sumTotalCost);
                sumActualCost = sumTotalCost;

                CurRowIndex++;
                //~~~~~~~~~~~~~~~~~~~
                EPP_ExcelManager.MergeAndSet(sheet, CurRowIndex, CurColIndex, CurRowIndex, CurColIndex + 3, "Actual Mud Cost/Vol (" + selCurrency + "/ " + selUnitVol + ")=");
                EPP_ExcelManager.SetAlignment(sheet, CurRowIndex, CurColIndex, CurRowIndex, CurColIndex + 3, OfficeOpenXml.Style.ExcelHorizontalAlignment.Left, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
                EPP_ExcelManager.SetAlignment(sheet, CurRowIndex, CurColIndex + 4, CurRowIndex, CurColIndex + 15, OfficeOpenXml.Style.ExcelHorizontalAlignment.Center, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
                EPP_ExcelManager.MergeAndSet(sheet, CurRowIndex, CurColIndex + 4, CurRowIndex, CurColIndex + 15, (actualVol > 0) ? (sumTotalCost / actualVol).ToString() : "Undefined");

                CurRowIndex++;
                //~~~~~~~~~~~~~~~~~~~
                int sectionEndRowIndex = CurRowIndex - 1;

                EPP_ExcelManager.SetInBorder(sheet, sectionStartRowIndex, CurColIndex, sectionEndRowIndex, CurColIndex + 15, Color.Black, OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                EPP_ExcelManager.SetOutBorder(sheet, sectionStartRowIndex, CurColIndex, sectionEndRowIndex, CurColIndex + 15, Color.Black, OfficeOpenXml.Style.ExcelBorderStyle.Thin);

                EPP_ExcelManager.SetBackColor_AlternatingRows(sheet, bodyStartRowIndex, CurColIndex, bodyEndRowIndex, CurColIndex + 15, Color.White, Color.FromArgb(255, 204, 204), true);

                if (bodyStartRowIndex <= bodyEndRowIndex)
                    EPP_ExcelManager.SetAlignment(sheet, bodyStartRowIndex, CurColIndex, bodyEndRowIndex, CurColIndex + 15, OfficeOpenXml.Style.ExcelHorizontalAlignment.Center, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
                //~~~~~~~~~~~~~~~~~~~
                //Chart
                if (bodyStartRowIndex <= bodyEndRowIndex)
                {
                    var pieChart = (ExcelPieChart)((ExcelChartsheet)sheet.Workbook.Worksheets[sheet.Index + actualMud_SectionCount * 2 + 1]).Chart;
                    pieChart.Series.Add
                    (
                    ExcelCellBase.GetFullAddress(sheet.Name, ExcelCellBase.GetAddress(bodyStartRowIndex, CurColIndex + 13, bodyEndRowIndex, CurColIndex + 13)),//Conc.
                    ExcelCellBase.GetFullAddress(sheet.Name, ExcelCellBase.GetAddress(bodyStartRowIndex, CurColIndex + 2, bodyEndRowIndex, CurColIndex + 3))//product name
                    );

                    pieChart.Legend.Remove();
                    pieChart.Title.Text = "Material Concentration FOR ACTUAL Mud OF  " + dfs + Environment.NewLine +
                                          "(" + projectName + "_" + rigName + "_" + wellName + "_" + holeSection + ")";

                    pieChart.DataLabel.ShowCategory = true;
                    pieChart.DataLabel.ShowPercent = true;
                    pieChart.DataLabel.ShowValue = true;
                    pieChart.DataLabel.ShowLeaderLines = true;
                }
                if (bodyStartRowIndex <= bodyEndRowIndex)
                {
                    var pieChart = (ExcelPieChart)((ExcelChartsheet)sheet.Workbook.Worksheets[sheet.Index + actualMud_SectionCount * 2 + 2]).Chart;
                    pieChart.Series.Add
                    (
                    ExcelCellBase.GetFullAddress(sheet.Name, ExcelCellBase.GetAddress(bodyStartRowIndex, CurColIndex + 15, bodyEndRowIndex, CurColIndex + 15)),//Cost
                    ExcelCellBase.GetFullAddress(sheet.Name, ExcelCellBase.GetAddress(bodyStartRowIndex, CurColIndex + 2, bodyEndRowIndex, CurColIndex + 3))//product name
                    );

                    pieChart.Legend.Remove();
                    pieChart.Title.Text = "MATERIAL COST FOR ACTUAL Mud OF " + dfs + Environment.NewLine +
                                          "(" + projectName + "_" + rigName + "_" + wellName + "_" + holeSection + ")" +
                                          Environment.NewLine + "Total Cost (" + selCurrency + "):" + sumTotalCost.ToString("0.###");

                    pieChart.DataLabel.ShowCategory = true;
                    pieChart.DataLabel.ShowPercent = true;
                    pieChart.DataLabel.ShowValue = true;
                    pieChart.DataLabel.ShowLeaderLines = true;
                }


                //~~~~~~~~~~~~~~~~~~~
                actualMud_SectionCount++;
                //~~~~~~~~~~~~~~~~~~~
                return dataRowIndex + 1;
            }
            catch (Exception)
            {
                return -1024;
            }
        }

        //"Hole section Actual cost (" + projectName + "_" + rigName + "_" + wellName + "_" + holeSection + ")" +
        //                     Environment.NewLine + "Total Cost (" + selCurrency + "):" + totalCost.ToString("0.###");


        //-------------------------------------------------------
        public string /*error*/ PrepareSheet(ReportingBase recap, ExcelWorksheet sheet, int noDFSs)
        {
            try
            {
                sumRecVol = 0; sumRecCost = 0;
                sumTransVol = 0; sumTransCost = 0;
                sumBuiltVol = 0; sumBuiltCost = 0;
                sumTreatVol = 0; sumTreatCost = 0;
                sumUsingVol = 0; sumUsingCost = 0;
                sumUsedForVol = 0; sumUsedForCost = 0;
                sumActualVol = 0; sumActualCost = 0;
                sumFinalVol = 0; sumFinalCost = 0;
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
                            EPP_ExcelManager.SetImage(sheet, 1120, 10, 70, 40, contractorImage, "Contractor Image");

                        if (operatorImage != null)
                            EPP_ExcelManager.SetImage(sheet, 1200, 10, 70, 40, operatorImage, "Operator Image");
                    }
                    else if (useContractorImage)
                    {
                        if (contractorImage != null)
                            EPP_ExcelManager.SetImage(sheet, 1120, 10, 120, 60, contractorImage, "Contractor Image");
                    }
                    else if (useOperatorImage)
                    {
                        if (operatorImage != null)
                            EPP_ExcelManager.SetImage(sheet, 1120, 10, 120, 60, operatorImage, "Operator Image");
                    }
                }
                //~~~~~~~~~~~~~~~~~
                {//others
                    string query = " select * from fn_Reporting_Recap_Header(" + wellID.ToString() + "," + "\'" + holeSizeLabel + "\'" + ")";

                    DataSet ds = ConnectionManager.ExecQuery(query, 1);
                    if (ds != null)
                    {
                        EPP_ExcelManager.SetText(sheet, 5, 3, ds.Tables[0].Rows[0][0].ToString());//client Name
                        EPP_ExcelManager.SetText(sheet, 6, 3, operatorName = ds.Tables[0].Rows[0][1].ToString());//Operator Name
                        EPP_ExcelManager.SetText(sheet, 7, 3, projectName = ds.Tables[0].Rows[0][2].ToString());//Project Name

                        EPP_ExcelManager.SetText(sheet, 5, 5, rigName = ds.Tables[0].Rows[0][3].ToString());//Rig Name
                        EPP_ExcelManager.SetText(sheet, 6, 5, wellName = ds.Tables[0].Rows[0][4].ToString());//Well Name
                        EPP_ExcelManager.SetText(sheet, 7, 5, holeSection);//Hole Section

                        EPP_ExcelManager.SetText(sheet, 5, 8, "Drilled Interval (" + selUnitDepth + "):");//Drilled Interval (SU)
                        EPP_ExcelManager.SetText(sheet, 5, 12, ds.Tables[0].Rows[0][5].ToString());//Drilled Interval
                        EPP_ExcelManager.SetText(sheet, 7, 12, ds.Tables[0].Rows[0][6].ToString());//Casing Depth
                        ds.Dispose();
                    }

                    {//Code , Date
                        EPP_ExcelManager.SetText(sheet, 5, 16, code);//Code
                        EPP_ExcelManager.SetText(sheet, 7, 16, reportingDate.ToShortDateString() + " - " + shamsiReportingDate);//Date
                    }
                }
                return "";
            }
            catch (Exception)
            {
                return "Report Generating Error";
            }
        }
        //-------------------------------------------------------//-------------------------------------------------------
        public string GenerateReportForNextDFS(ReportingBase recap, ExcelWorksheet sheet, int DFSAutoID, string DFS)
        {
            try
            {
                // ******************* Body ***********************/
                {
                    //~~~~~~~~~~~~~~
                    decimal VolFactor_SelUnit2BBL = (selUnitVol == "m³") ? 6.2898m : 1;
                    decimal PowderConsFactor_SelUnit2LBL = (selUnitPowderMatConc == "kg/m³") ? 2.853m : 1;
                    decimal LiquidConcFactor_ToComputeConc_GalPerBbl2SelUnit = (selUnitLiquidMatConc == "vol%") ? 0.42m : 1;
                    decimal LiquidConcFactor_ToComputeUsed_SelUnit2GalPerBbl = (selUnitLiquidMatConc == "vol%") ? 2.38095238095238m : 1;
                    //~~~~~~~~~~~~~~

                    string query =
                        " select * from  fn_Reporting_Recap_HoleSectionMudMaterialConcentrationAndCostAnalysis(" + wellID + "," + "\'" + holeSizeLabel + "\'" + ","
                                                                + VolFactor_SelUnit2BBL + "," + PowderConsFactor_SelUnit2LBL + ","
                                                                + LiquidConcFactor_ToComputeConc_GalPerBbl2SelUnit + "," + LiquidConcFactor_ToComputeUsed_SelUnit2GalPerBbl + "," +
                                                                DFSAutoID.ToString() + ",\'" + DFS + "\' )";

                    const int receiveIndicator = -1000;
                    const int builtIndicator = -2000;
                    const int treatIndicator = -3000;
                    const int builtAndTreatIndicator = -4000;
                    const int usedForIndicator = -5000;
                    const int usingIndicator = -6000;
                    const int finalIndicator = -9000;
                    const int transIndicator = -20000;
                    const int actualMudIndicator = -30000;

                    DataSet ds = new DataSet();
                    if (ConnectionManager.ExecQuery(query, ref ds))
                    {
                        if (ds.Tables[0].Columns.Count != 9)
                            return "Report Generating Error (Data-1)";

                        int i = NewDFS_Section(sheet, DFS);

                        if (i < 0)
                            return "Report Generating Error (Data-2)";

                        while (i < ds.Tables[0].Rows.Count)
                        {
                            if (i < 0)
                                return "Report Generating Error (Data-2)";

                            switch (Convert.ToInt32(ds.Tables[0].Rows[i][0]))
                            {
                                case receiveIndicator: i = Received_Section(sheet, ds, i); break;
                                case builtIndicator: i = Built_Section(sheet, ds, i); break;
                                case treatIndicator: i = Treat_Section(sheet, ds, i); break;
                                case builtAndTreatIndicator: i = BuiltAndTreat_Section(sheet, ds, i); break;
                                case usedForIndicator: i = UsedFor_Section(sheet, ds, i); break;
                                case usingIndicator: i = Using_Section(sheet, ds, i); break;
                                case finalIndicator: i = Final_Section(sheet, ds, i); break;
                                case transIndicator: i = Transfer_Section(sheet, ds, i); break;
                                case actualMudIndicator: i = ActualMud_Section(sheet, ds, i, DFS); break;
                                default: i++; break;
                            }
                        }

                        ds.Dispose();
                    }
                }
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
