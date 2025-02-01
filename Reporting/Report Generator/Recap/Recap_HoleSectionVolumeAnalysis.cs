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
    class Recap_HoleSectionVolumeAnalysis
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
        public decimal sumLossesVol = 0;
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
        int CurRowIndex = -1;//1-based [of work sheet]
        int CurColIndex = -1;//1-based [of work sheet]
        //-------------------------------------------------------
        int noDFSs = 0, noRec = 0, noTrans = 0, noLoss = 0;
        decimal[] horTotal = null;
        //-------------------------------------------------------
        int/*next data row index to read*/ PrepareSheetFormat(ExcelWorksheet sheet, DataSet ds)
        {
            int dataIndex = 0;

            if (Convert.ToInt32(ds.Tables[0].Rows[dataIndex][0]) != -40) throw new Exception("Bad Indicator");
            noRec = Convert.ToInt32(ds.Tables[0].Rows[dataIndex++][2]);

            if (Convert.ToInt32(ds.Tables[0].Rows[dataIndex][0]) != -70) throw new Exception("Bad Indicator");
            noTrans = Convert.ToInt32(ds.Tables[0].Rows[dataIndex++][2]);

            if (Convert.ToInt32(ds.Tables[0].Rows[dataIndex][0]) != -90) throw new Exception("Bad Indicator");
            noLoss = Convert.ToInt32(ds.Tables[0].Rows[dataIndex++][2]);

            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            horTotal = new decimal[29 + noRec + noLoss + noTrans + 10/*headers:unused*/];
            for (int i = 0; i < horTotal.Length; i++)
                horTotal[i] = 0;
            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

            //First Row Size
            EPP_ExcelManager.SetHeight(sheet, 9, 34.0f);
            //~~~~~ 
            for (int i = 0; i < noDFSs + 1; i++)
                EPP_ExcelManager.SetWidth(sheet, 5 + i, 16.0f);

            //Merge Columns, Set Fonts, Colors
            int totalColumns = 4 + noDFSs;


            int rowID = 9;
            EPP_ExcelManager.MergeAndSet(sheet, rowID, 2, rowID, 4, "Drilling Fluid Systems:");
            EPP_ExcelManager.MergeAndSet(sheet, rowID, 2 + totalColumns - 1, rowID, 2 + totalColumns - 1, "Total");
            EPP_ExcelManager.SetBackColor(sheet, rowID, 2, rowID, 2 + totalColumns - 1, Color.FromArgb(255, 80, 80));
            EPP_ExcelManager.SetAlignment(sheet, rowID, 2, rowID, 2, OfficeOpenXml.Style.ExcelHorizontalAlignment.Left, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
            EPP_ExcelManager.SetAlignment(sheet, rowID, 3, rowID, 2 + totalColumns - 1, OfficeOpenXml.Style.ExcelHorizontalAlignment.Center, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
            EPP_ExcelManager.SetFont(sheet, rowID, 2, rowID, 2 + totalColumns - 1, 10, "Times New Roman", true, false);

            rowID++;
            //~~~~~~~~~~
            EPP_ExcelManager.SetBackColor_AlternatingRows(sheet, rowID, 2, rowID + 1, 2 + totalColumns - 1, Color.White, Color.FromArgb(255, 204, 204), true);
            EPP_ExcelManager.MergeAndSet(sheet, rowID, 2, rowID, 4, "Rain Volume"); rowID++;
            EPP_ExcelManager.MergeAndSet(sheet, rowID, 2, rowID, 4, "Gain Volume"); rowID++;
            //~~~~~~~~~~
            EPP_ExcelManager.MergeAndSet(sheet, rowID, 2, rowID, 2 + totalColumns - 1, "Received Mud from Other Project/ Rig/ Well/ Section (" + selUnitVol + ")");
            EPP_ExcelManager.SetBackColor(sheet, rowID, 2, rowID, 2 + totalColumns - 1, Color.FromArgb(255, 153, 153));
            EPP_ExcelManager.SetAlignment(sheet, rowID, 2, rowID, 2 + totalColumns - 1, OfficeOpenXml.Style.ExcelHorizontalAlignment.Center, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
            EPP_ExcelManager.SetFont(sheet, rowID, 2, rowID, 2 + totalColumns - 1, 11, "Times New Roman", true, false);
            rowID++;
            EPP_ExcelManager.SetBackColor_AlternatingRows(sheet, rowID, 2, rowID + noRec, 2 + totalColumns - 1, Color.White, Color.FromArgb(255, 204, 204), true);
            for (int i = 0; i < noRec; i++, rowID++)
            {
                if (Convert.ToInt32(ds.Tables[0].Rows[dataIndex][0]) != -200) throw new Exception("Bad Indicator");
                EPP_ExcelManager.MergeAndSet(sheet, rowID, 2, rowID, 4, ds.Tables[0].Rows[dataIndex++][1].ToString());
            }
            EPP_ExcelManager.MergeAndSet(sheet, rowID, 2, rowID++, 4, "Total");
            //~~~~~~~~~~
            EPP_ExcelManager.MergeAndSet(sheet, rowID, 2, rowID, 2 + totalColumns - 1, "Built Mud");
            EPP_ExcelManager.SetBackColor(sheet, rowID, 2, rowID, 2 + totalColumns - 1, Color.FromArgb(255, 153, 153));
            EPP_ExcelManager.SetAlignment(sheet, rowID, 2, rowID, 2 + totalColumns - 1, OfficeOpenXml.Style.ExcelHorizontalAlignment.Center, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
            EPP_ExcelManager.SetFont(sheet, rowID, 2, rowID, 2 + totalColumns - 1, 11, "Times New Roman", true, false);
            rowID++;
            EPP_ExcelManager.SetBackColor_AlternatingRows(sheet, rowID, 2, rowID + 6, 2 + totalColumns - 1, Color.White, Color.FromArgb(255, 204, 204), true);
            EPP_ExcelManager.MergeAndSet(sheet, rowID, 2, rowID++, 4, "Chemical");
            EPP_ExcelManager.MergeAndSet(sheet, rowID, 2, rowID++, 4, "Added Oil");
            EPP_ExcelManager.MergeAndSet(sheet, rowID, 2, rowID++, 4, "Added Sea Water");
            EPP_ExcelManager.MergeAndSet(sheet, rowID, 2, rowID++, 4, "Added Drill/Fresh Water");
            EPP_ExcelManager.MergeAndSet(sheet, rowID, 2, rowID++, 4, "Added DeWater");
            EPP_ExcelManager.MergeAndSet(sheet, rowID, 2, rowID++, 4, "Added Local Water");
            EPP_ExcelManager.MergeAndSet(sheet, rowID, 2, rowID++, 4, "Total");
            //~~~~~~~~~~
            EPP_ExcelManager.MergeAndSet(sheet, rowID, 2, rowID, 2 + totalColumns - 1, "Treated Mud");
            EPP_ExcelManager.SetBackColor(sheet, rowID, 2, rowID, 2 + totalColumns - 1, Color.FromArgb(255, 153, 153));
            EPP_ExcelManager.SetAlignment(sheet, rowID, 2, rowID, 2 + totalColumns - 1, OfficeOpenXml.Style.ExcelHorizontalAlignment.Center, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
            EPP_ExcelManager.SetFont(sheet, rowID, 2, rowID, 2 + totalColumns - 1, 11, "Times New Roman", true, false);
            rowID++;
            EPP_ExcelManager.SetBackColor_AlternatingRows(sheet, rowID, 2, rowID + 8, 2 + totalColumns - 1, Color.White, Color.FromArgb(255, 204, 204), true);
            EPP_ExcelManager.MergeAndSet(sheet, rowID, 2, rowID++, 4, "Chemical");
            EPP_ExcelManager.MergeAndSet(sheet, rowID, 2, rowID++, 4, "Added Oil");
            EPP_ExcelManager.MergeAndSet(sheet, rowID, 2, rowID++, 4, "Added Sea Water");
            EPP_ExcelManager.MergeAndSet(sheet, rowID, 2, rowID++, 4, "Added Drill/Fresh Water");
            EPP_ExcelManager.MergeAndSet(sheet, rowID, 2, rowID++, 4, "Added DeWater");
            EPP_ExcelManager.MergeAndSet(sheet, rowID, 2, rowID++, 4, "Added Local Water");
            EPP_ExcelManager.MergeAndSet(sheet, rowID, 2, rowID++, 4, "Total Added Volume");
            EPP_ExcelManager.MergeAndSet(sheet, rowID, 2, rowID++, 4, "Used Volume for Treatment");
            EPP_ExcelManager.MergeAndSet(sheet, rowID, 2, rowID++, 4, "Final Volume");
            //~~~~~~~~~~
            EPP_ExcelManager.MergeAndSet(sheet, rowID, 2, rowID, 2 + totalColumns - 1, "Using other Drilling Fluid System");
            EPP_ExcelManager.SetBackColor(sheet, rowID, 2, rowID, 2 + totalColumns - 1, Color.FromArgb(255, 153, 153));
            EPP_ExcelManager.SetAlignment(sheet, rowID, 2, rowID, 2 + totalColumns - 1, OfficeOpenXml.Style.ExcelHorizontalAlignment.Center, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
            EPP_ExcelManager.SetFont(sheet, rowID, 2, rowID, 2 + totalColumns - 1, 11, "Times New Roman", true, false);
            rowID++;
            EPP_ExcelManager.SetBackColor(sheet, rowID, 2, rowID, 4, Color.White);
            EPP_ExcelManager.MergeAndSet(sheet, rowID, 2, rowID++, 4, "Total");
            //~~~~~~~~~~
            EPP_ExcelManager.MergeAndSet(sheet, rowID, 2, rowID, 2 + totalColumns - 1, "Used for Other Drilling Fluid Systems");
            EPP_ExcelManager.SetBackColor(sheet, rowID, 2, rowID, 2 + totalColumns - 1, Color.FromArgb(255, 153, 153));
            EPP_ExcelManager.SetAlignment(sheet, rowID, 2, rowID, 2 + totalColumns - 1, OfficeOpenXml.Style.ExcelHorizontalAlignment.Center, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
            EPP_ExcelManager.SetFont(sheet, rowID, 2, rowID, 2 + totalColumns - 1, 11, "Times New Roman", true, false);
            rowID++;
            EPP_ExcelManager.SetBackColor(sheet, rowID, 2, rowID, 4, Color.White);
            EPP_ExcelManager.MergeAndSet(sheet, rowID, 2, rowID++, 4, "Total");
            //~~~~~~~~~~
            EPP_ExcelManager.MergeAndSet(sheet, rowID, 2, rowID, 2 + totalColumns - 1, "Mud Losses");
            EPP_ExcelManager.SetBackColor(sheet, rowID, 2, rowID, 2 + totalColumns - 1, Color.FromArgb(255, 153, 153));
            EPP_ExcelManager.SetAlignment(sheet, rowID, 2, rowID, 2 + totalColumns - 1, OfficeOpenXml.Style.ExcelHorizontalAlignment.Center, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
            EPP_ExcelManager.SetFont(sheet, rowID, 2, rowID, 2 + totalColumns - 1, 11, "Times New Roman", true, false);
            rowID++;
            EPP_ExcelManager.SetBackColor_AlternatingRows(sheet, rowID, 2, rowID + noLoss + 4, 2 + totalColumns - 1, Color.White, Color.FromArgb(255, 204, 204), true);
            EPP_ExcelManager.MergeAndSet(sheet, rowID, 2, rowID++, 4, "Solid Control Losses");
            EPP_ExcelManager.MergeAndSet(sheet, rowID, 2, rowID++, 4, "Formation Loss");
            EPP_ExcelManager.MergeAndSet(sheet, rowID, 2, rowID++, 4, "Waste Pit");
            EPP_ExcelManager.MergeAndSet(sheet, rowID, 2, rowID++, 4, "Overboard");

            for (int i = 0; i < noLoss; i++, rowID++)
            {
                if (Convert.ToInt32(ds.Tables[0].Rows[dataIndex][0]) != -300) throw new Exception("Bad Indicator");
                EPP_ExcelManager.MergeAndSet(sheet, rowID, 2, rowID, 4, ds.Tables[0].Rows[dataIndex++][1].ToString());
            }
            EPP_ExcelManager.MergeAndSet(sheet, rowID, 2, rowID++, 4, "Total");
            //~~~~~~~~~~
            EPP_ExcelManager.MergeAndSet(sheet, rowID, 2, rowID, 2 + totalColumns - 1, "Transferred Mud to Other Project/ Rig/ Well/ Section (" + selUnitVol + ")");
            EPP_ExcelManager.SetBackColor(sheet, rowID, 2, rowID, 2 + totalColumns - 1, Color.FromArgb(255, 153, 153));
            EPP_ExcelManager.SetAlignment(sheet, rowID, 2, rowID, 2 + totalColumns - 1, OfficeOpenXml.Style.ExcelHorizontalAlignment.Center, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
            EPP_ExcelManager.SetFont(sheet, rowID, 2, rowID, 2 + totalColumns - 1, 11, "Times New Roman", true, false);
            rowID++;
            EPP_ExcelManager.SetBackColor_AlternatingRows(sheet, rowID, 2, rowID + noTrans, 2 + totalColumns - 1, Color.White, Color.FromArgb(255, 204, 204), true);
            for (int i = 0; i < noTrans; i++, rowID++)
            {
                if (Convert.ToInt32(ds.Tables[0].Rows[dataIndex][0]) != -400) throw new Exception("Bad Indicator");
                EPP_ExcelManager.MergeAndSet(sheet, rowID, 2, rowID, 4, ds.Tables[0].Rows[dataIndex++][1].ToString());
            }
            EPP_ExcelManager.MergeAndSet(sheet, rowID, 2, rowID++, 4, "Total");
            //~~~~~~~~~~
            EPP_ExcelManager.MergeAndSet(sheet, rowID, 2, rowID, 2 + totalColumns - 1, "Displaced Mud (" + selUnitVol + ")");
            EPP_ExcelManager.SetBackColor(sheet, rowID, 2, rowID, 2 + totalColumns - 1, Color.FromArgb(255, 153, 153));
            EPP_ExcelManager.SetAlignment(sheet, rowID, 2, rowID, 2 + totalColumns - 1, OfficeOpenXml.Style.ExcelHorizontalAlignment.Center, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
            EPP_ExcelManager.SetFont(sheet, rowID, 2, rowID, 2 + totalColumns - 1, 11, "Times New Roman", true, false);
            rowID++;
            EPP_ExcelManager.SetBackColor(sheet, rowID, 2, rowID, 4, Color.White);
            EPP_ExcelManager.MergeAndSet(sheet, rowID, 2, rowID++, 4, "Displaced Mud");
            //~~~~~~~~~~
            EPP_ExcelManager.MergeAndSet(sheet, rowID, 2, rowID, 2 + totalColumns - 1, "Returned Mud (" + selUnitVol + ")");
            EPP_ExcelManager.SetBackColor(sheet, rowID, 2, rowID, 2 + totalColumns - 1, Color.FromArgb(255, 153, 153));
            EPP_ExcelManager.SetAlignment(sheet, rowID, 2, rowID, 2 + totalColumns - 1, OfficeOpenXml.Style.ExcelHorizontalAlignment.Center, OfficeOpenXml.Style.ExcelVerticalAlignment.Center);
            EPP_ExcelManager.SetFont(sheet, rowID, 2, rowID, 2 + totalColumns - 1, 11, "Times New Roman", true, false);
            rowID++;
            EPP_ExcelManager.SetBackColor(sheet, rowID, 2, rowID, 4, Color.White);
            EPP_ExcelManager.MergeAndSet(sheet, rowID, 2, rowID++, 4, "Returned Mud");
            //~~~~~~~~~~

            EPP_ExcelManager.SetInBorder(sheet, 9, 2, 47 + noRec + noTrans + noLoss, 2 + totalColumns - 1, Color.Black, OfficeOpenXml.Style.ExcelBorderStyle.Thin);
            EPP_ExcelManager.SetOutBorder(sheet, 9, 2, 47 + noRec + noTrans + noLoss, 2 + totalColumns - 1, Color.Black, OfficeOpenXml.Style.ExcelBorderStyle.Thin);

            return dataIndex;
        }
        //-------------------------------------------------------
        int/*next data row index to read*/ NewDFS_Section(ExcelWorksheet sheet, DataSet ds, int dataRowIndex/*indicator row index*/, string dfs, bool isLastDFS = false)
        {
            try
            {
                CurColIndex = (CurColIndex < 0) ? 5 : CurColIndex + 1;
                CurRowIndex = 9;
                //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                EPP_ExcelManager.SetText(sheet, CurRowIndex, CurColIndex, CurRowIndex, CurColIndex, dfs);
                CurRowIndex++;
                //~~~~~~~~~~
                if (Convert.ToInt32(ds.Tables[0].Rows[dataRowIndex][0]) != -1000/*Rain*/) throw new Exception("Bad Indicator");
                horTotal[CurRowIndex - 9] += Convert.ToDecimal(ds.Tables[0].Rows[dataRowIndex][2]);
                if (isLastDFS) EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, CurColIndex + 1, CurRowIndex, CurColIndex + 1, horTotal[CurRowIndex - 9]);
                EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, CurColIndex, CurRowIndex, CurColIndex, ds.Tables[0].Rows[dataRowIndex++][2]);
                CurRowIndex++;

                if (Convert.ToInt32(ds.Tables[0].Rows[dataRowIndex][0]) != -2000/*Gain*/) throw new Exception("Bad Indicator");
                horTotal[CurRowIndex - 9] += Convert.ToDecimal(ds.Tables[0].Rows[dataRowIndex][2]);
                if (isLastDFS) EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, CurColIndex + 1, CurRowIndex, CurColIndex + 1, horTotal[CurRowIndex - 9]);
                EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, CurColIndex, CurRowIndex, CurColIndex, ds.Tables[0].Rows[dataRowIndex++][2]);
                CurRowIndex++;
                //~~~~~~~~~~
                CurRowIndex++;

                for (int i = 0; i < noRec; i++, CurRowIndex++)
                {
                    if (Convert.ToInt32(ds.Tables[0].Rows[dataRowIndex][0]) != -3000/*rec vol*/) throw new Exception("Bad Indicator");
                    horTotal[CurRowIndex - 9] += Convert.ToDecimal(ds.Tables[0].Rows[dataRowIndex][2]);
                    if (isLastDFS) EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, CurColIndex + 1, CurRowIndex, CurColIndex + 1, horTotal[CurRowIndex - 9]);
                    EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, CurColIndex, CurRowIndex, CurColIndex, ds.Tables[0].Rows[dataRowIndex++][2]);
                }

                if (Convert.ToInt32(ds.Tables[0].Rows[dataRowIndex][0]) != -4000/*total*/) throw new Exception("Bad Indicator");
                horTotal[CurRowIndex - 9] += Convert.ToDecimal(ds.Tables[0].Rows[dataRowIndex][2]);
                if (isLastDFS) EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, CurColIndex + 1, CurRowIndex, CurColIndex + 1, horTotal[CurRowIndex - 9]);
                EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, CurColIndex, CurRowIndex, CurColIndex, ds.Tables[0].Rows[dataRowIndex++][2]);
                CurRowIndex++;
                //~~~~~~~~~~
                CurRowIndex++;

                if (Convert.ToInt32(ds.Tables[0].Rows[dataRowIndex][0]) != -5000/*Chemical*/) throw new Exception("Bad Indicator");
                horTotal[CurRowIndex - 9] += Convert.ToDecimal(ds.Tables[0].Rows[dataRowIndex][2]);
                if (isLastDFS) EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, CurColIndex + 1, CurRowIndex, CurColIndex + 1, horTotal[CurRowIndex - 9]);
                EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, CurColIndex, CurRowIndex, CurColIndex, ds.Tables[0].Rows[dataRowIndex++][2]);
                CurRowIndex++;

                if (Convert.ToInt32(ds.Tables[0].Rows[dataRowIndex][0]) != -6000/*Added Oil*/) throw new Exception("Bad Indicator");
                horTotal[CurRowIndex - 9] += Convert.ToDecimal(ds.Tables[0].Rows[dataRowIndex][2]);
                if (isLastDFS) EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, CurColIndex + 1, CurRowIndex, CurColIndex + 1, horTotal[CurRowIndex - 9]);
                EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, CurColIndex, CurRowIndex, CurColIndex, ds.Tables[0].Rows[dataRowIndex++][2]);
                CurRowIndex++;

                if (Convert.ToInt32(ds.Tables[0].Rows[dataRowIndex][0]) != -7000/*Added Sea Water*/) throw new Exception("Bad Indicator");
                horTotal[CurRowIndex - 9] += Convert.ToDecimal(ds.Tables[0].Rows[dataRowIndex][2]);
                if (isLastDFS) EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, CurColIndex + 1, CurRowIndex, CurColIndex + 1, horTotal[CurRowIndex - 9]);
                EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, CurColIndex, CurRowIndex, CurColIndex, ds.Tables[0].Rows[dataRowIndex++][2]);
                CurRowIndex++;

                if (Convert.ToInt32(ds.Tables[0].Rows[dataRowIndex][0]) != -8000/*Added Drill/Fresh Water*/) throw new Exception("Bad Indicator");
                horTotal[CurRowIndex - 9] += Convert.ToDecimal(ds.Tables[0].Rows[dataRowIndex][2]);
                if (isLastDFS) EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, CurColIndex + 1, CurRowIndex, CurColIndex + 1, horTotal[CurRowIndex - 9]);
                EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, CurColIndex, CurRowIndex, CurColIndex, ds.Tables[0].Rows[dataRowIndex++][2]);
                CurRowIndex++;

                if (Convert.ToInt32(ds.Tables[0].Rows[dataRowIndex][0]) != -9000/*Added DeWater*/) throw new Exception("Bad Indicator");
                horTotal[CurRowIndex - 9] += Convert.ToDecimal(ds.Tables[0].Rows[dataRowIndex][2]);
                if (isLastDFS) EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, CurColIndex + 1, CurRowIndex, CurColIndex + 1, horTotal[CurRowIndex - 9]);
                EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, CurColIndex, CurRowIndex, CurColIndex, ds.Tables[0].Rows[dataRowIndex++][2]);
                CurRowIndex++;

                if (Convert.ToInt32(ds.Tables[0].Rows[dataRowIndex][0]) != -10000/*Added Local Water*/) throw new Exception("Bad Indicator");
                horTotal[CurRowIndex - 9] += Convert.ToDecimal(ds.Tables[0].Rows[dataRowIndex][2]);
                if (isLastDFS) EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, CurColIndex + 1, CurRowIndex, CurColIndex + 1, horTotal[CurRowIndex - 9]);
                EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, CurColIndex, CurRowIndex, CurColIndex, ds.Tables[0].Rows[dataRowIndex++][2]);
                CurRowIndex++;

                if (Convert.ToInt32(ds.Tables[0].Rows[dataRowIndex][0]) != -11000/*Total*/) throw new Exception("Bad Indicator");
                horTotal[CurRowIndex - 9] += Convert.ToDecimal(ds.Tables[0].Rows[dataRowIndex][2]);
                if (isLastDFS) EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, CurColIndex + 1, CurRowIndex, CurColIndex + 1, horTotal[CurRowIndex - 9]);
                EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, CurColIndex, CurRowIndex, CurColIndex, ds.Tables[0].Rows[dataRowIndex++][2]);
                CurRowIndex++;
                //~~~~~~~~~~
                CurRowIndex++;

                if (Convert.ToInt32(ds.Tables[0].Rows[dataRowIndex][0]) != -20000/*Chemical*/) throw new Exception("Bad Indicator");
                horTotal[CurRowIndex - 9] += Convert.ToDecimal(ds.Tables[0].Rows[dataRowIndex][2]);
                if (isLastDFS) EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, CurColIndex + 1, CurRowIndex, CurColIndex + 1, horTotal[CurRowIndex - 9]);
                EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, CurColIndex, CurRowIndex, CurColIndex, ds.Tables[0].Rows[dataRowIndex++][2]);
                CurRowIndex++;

                if (Convert.ToInt32(ds.Tables[0].Rows[dataRowIndex][0]) != -21000/*Added Oil*/) throw new Exception("Bad Indicator");
                horTotal[CurRowIndex - 9] += Convert.ToDecimal(ds.Tables[0].Rows[dataRowIndex][2]);
                if (isLastDFS) EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, CurColIndex + 1, CurRowIndex, CurColIndex + 1, horTotal[CurRowIndex - 9]);
                EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, CurColIndex, CurRowIndex, CurColIndex, ds.Tables[0].Rows[dataRowIndex++][2]);
                CurRowIndex++;


                if (Convert.ToInt32(ds.Tables[0].Rows[dataRowIndex][0]) != -22000/*Added Sea Water*/) throw new Exception("Bad Indicator");
                horTotal[CurRowIndex - 9] += Convert.ToDecimal(ds.Tables[0].Rows[dataRowIndex][2]);
                if (isLastDFS) EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, CurColIndex + 1, CurRowIndex, CurColIndex + 1, horTotal[CurRowIndex - 9]);
                EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, CurColIndex, CurRowIndex, CurColIndex, ds.Tables[0].Rows[dataRowIndex++][2]);
                CurRowIndex++;

                if (Convert.ToInt32(ds.Tables[0].Rows[dataRowIndex][0]) != -23000/*Added Drill/Fresh Water*/) throw new Exception("Bad Indicator");
                horTotal[CurRowIndex - 9] += Convert.ToDecimal(ds.Tables[0].Rows[dataRowIndex][2]);
                if (isLastDFS) EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, CurColIndex + 1, CurRowIndex, CurColIndex + 1, horTotal[CurRowIndex - 9]);
                EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, CurColIndex, CurRowIndex, CurColIndex, ds.Tables[0].Rows[dataRowIndex++][2]);
                CurRowIndex++;

                if (Convert.ToInt32(ds.Tables[0].Rows[dataRowIndex][0]) != -24000/*Added DeWater*/) throw new Exception("Bad Indicator");
                horTotal[CurRowIndex - 9] += Convert.ToDecimal(ds.Tables[0].Rows[dataRowIndex][2]);
                if (isLastDFS) EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, CurColIndex + 1, CurRowIndex, CurColIndex + 1, horTotal[CurRowIndex - 9]);
                EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, CurColIndex, CurRowIndex, CurColIndex, ds.Tables[0].Rows[dataRowIndex++][2]);
                CurRowIndex++;

                if (Convert.ToInt32(ds.Tables[0].Rows[dataRowIndex][0]) != -25000/*Added Local Water*/) throw new Exception("Bad Indicator");
                horTotal[CurRowIndex - 9] += Convert.ToDecimal(ds.Tables[0].Rows[dataRowIndex][2]);
                if (isLastDFS) EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, CurColIndex + 1, CurRowIndex, CurColIndex + 1, horTotal[CurRowIndex - 9]);
                EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, CurColIndex, CurRowIndex, CurColIndex, ds.Tables[0].Rows[dataRowIndex++][2]);
                CurRowIndex++;

                if (Convert.ToInt32(ds.Tables[0].Rows[dataRowIndex][0]) != -26000/*Total Added Volume*/) throw new Exception("Bad Indicator");
                horTotal[CurRowIndex - 9] += Convert.ToDecimal(ds.Tables[0].Rows[dataRowIndex][2]);
                if (isLastDFS) EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, CurColIndex + 1, CurRowIndex, CurColIndex + 1, horTotal[CurRowIndex - 9]);
                EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, CurColIndex, CurRowIndex, CurColIndex, ds.Tables[0].Rows[dataRowIndex++][2]);
                CurRowIndex++;

                if (Convert.ToInt32(ds.Tables[0].Rows[dataRowIndex][0]) != -27000/*Used Volume for Treatment*/) throw new Exception("Bad Indicator");
                horTotal[CurRowIndex - 9] += Convert.ToDecimal(ds.Tables[0].Rows[dataRowIndex][2]);
                if (isLastDFS) EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, CurColIndex + 1, CurRowIndex, CurColIndex + 1, horTotal[CurRowIndex - 9]);
                EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, CurColIndex, CurRowIndex, CurColIndex, ds.Tables[0].Rows[dataRowIndex++][2]);
                CurRowIndex++;

                if (Convert.ToInt32(ds.Tables[0].Rows[dataRowIndex][0]) != -28000/*Final Volume*/) throw new Exception("Bad Indicator");
                horTotal[CurRowIndex - 9] += Convert.ToDecimal(ds.Tables[0].Rows[dataRowIndex][2]);
                if (isLastDFS) EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, CurColIndex + 1, CurRowIndex, CurColIndex + 1, horTotal[CurRowIndex - 9]);
                EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, CurColIndex, CurRowIndex, CurColIndex, ds.Tables[0].Rows[dataRowIndex++][2]);
                CurRowIndex++;
                //~~~~~~~~~~
                CurRowIndex++;

                if (Convert.ToInt32(ds.Tables[0].Rows[dataRowIndex][0]) != -30000/*Total*/) throw new Exception("Bad Indicator");
                horTotal[CurRowIndex - 9] += Convert.ToDecimal(ds.Tables[0].Rows[dataRowIndex][2]);
                if (isLastDFS) EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, CurColIndex + 1, CurRowIndex, CurColIndex + 1, horTotal[CurRowIndex - 9]);
                EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, CurColIndex, CurRowIndex, CurColIndex, ds.Tables[0].Rows[dataRowIndex++][2]);
                CurRowIndex++;
                //~~~~~~~~~~
                CurRowIndex++;

                if (Convert.ToInt32(ds.Tables[0].Rows[dataRowIndex][0]) != -31000/*Total*/) throw new Exception("Bad Indicator");
                horTotal[CurRowIndex - 9] += Convert.ToDecimal(ds.Tables[0].Rows[dataRowIndex][2]);
                if (isLastDFS) EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, CurColIndex + 1, CurRowIndex, CurColIndex + 1, horTotal[CurRowIndex - 9]);
                EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, CurColIndex, CurRowIndex, CurColIndex, ds.Tables[0].Rows[dataRowIndex++][2]);
                CurRowIndex++;
                //~~~~~~~~~~
                CurRowIndex++;

                if (Convert.ToInt32(ds.Tables[0].Rows[dataRowIndex][0]) != -40000/*Solid Control Losses*/) throw new Exception("Bad Indicator");
                horTotal[CurRowIndex - 9] += Convert.ToDecimal(ds.Tables[0].Rows[dataRowIndex][2]);
                if (isLastDFS) EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, CurColIndex + 1, CurRowIndex, CurColIndex + 1, horTotal[CurRowIndex - 9]);
                EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, CurColIndex, CurRowIndex, CurColIndex, ds.Tables[0].Rows[dataRowIndex++][2]);
                CurRowIndex++;

                if (Convert.ToInt32(ds.Tables[0].Rows[dataRowIndex][0]) != -41000/*Formation Loss*/) throw new Exception("Bad Indicator");
                horTotal[CurRowIndex - 9] += Convert.ToDecimal(ds.Tables[0].Rows[dataRowIndex][2]);
                if (isLastDFS) EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, CurColIndex + 1, CurRowIndex, CurColIndex + 1, horTotal[CurRowIndex - 9]);
                EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, CurColIndex, CurRowIndex, CurColIndex, ds.Tables[0].Rows[dataRowIndex++][2]);
                CurRowIndex++;

                if (Convert.ToInt32(ds.Tables[0].Rows[dataRowIndex][0]) != -42000/*Waste Pit*/) throw new Exception("Bad Indicator");
                horTotal[CurRowIndex - 9] += Convert.ToDecimal(ds.Tables[0].Rows[dataRowIndex][2]);
                if (isLastDFS) EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, CurColIndex + 1, CurRowIndex, CurColIndex + 1, horTotal[CurRowIndex - 9]);
                EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, CurColIndex, CurRowIndex, CurColIndex, ds.Tables[0].Rows[dataRowIndex++][2]);
                CurRowIndex++;

                if (Convert.ToInt32(ds.Tables[0].Rows[dataRowIndex][0]) != -43000/*Overboard*/) throw new Exception("Bad Indicator");
                horTotal[CurRowIndex - 9] += Convert.ToDecimal(ds.Tables[0].Rows[dataRowIndex][2]);
                if (isLastDFS) EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, CurColIndex + 1, CurRowIndex, CurColIndex + 1, horTotal[CurRowIndex - 9]);
                EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, CurColIndex, CurRowIndex, CurColIndex, ds.Tables[0].Rows[dataRowIndex++][2]);
                CurRowIndex++;

                for (int i = 0; i < noLoss; i++, CurRowIndex++)
                {
                    if (Convert.ToInt32(ds.Tables[0].Rows[dataRowIndex][0]) != -44000/*losses vol*/) throw new Exception("Bad Indicator");
                    horTotal[CurRowIndex - 9] += Convert.ToDecimal(ds.Tables[0].Rows[dataRowIndex][2]);
                    if (isLastDFS) EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, CurColIndex + 1, CurRowIndex, CurColIndex + 1, horTotal[CurRowIndex - 9]);
                    EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, CurColIndex, CurRowIndex, CurColIndex, ds.Tables[0].Rows[dataRowIndex++][2]);
                }

                if (Convert.ToInt32(ds.Tables[0].Rows[dataRowIndex][0]) != -45000/*Total*/) throw new Exception("Bad Indicator");
                horTotal[CurRowIndex - 9] += Convert.ToDecimal(ds.Tables[0].Rows[dataRowIndex][2]);
                sumLossesVol = Convert.ToDecimal(ds.Tables[0].Rows[dataRowIndex][2]);
                if (isLastDFS) EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, CurColIndex + 1, CurRowIndex, CurColIndex + 1, horTotal[CurRowIndex - 9]);
                EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, CurColIndex, CurRowIndex, CurColIndex, ds.Tables[0].Rows[dataRowIndex++][2]);
                CurRowIndex++;
                //~~~~~~~~~~
                CurRowIndex++;

                for (int i = 0; i < noTrans; i++, CurRowIndex++)
                {
                    if (Convert.ToInt32(ds.Tables[0].Rows[dataRowIndex][0]) != -50000/*trans vol*/) throw new Exception("Bad Indicator");
                    horTotal[CurRowIndex - 9] += Convert.ToDecimal(ds.Tables[0].Rows[dataRowIndex][2]);
                    if (isLastDFS) EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, CurColIndex + 1, CurRowIndex, CurColIndex + 1, horTotal[CurRowIndex - 9]);
                    EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, CurColIndex, CurRowIndex, CurColIndex, ds.Tables[0].Rows[dataRowIndex++][2]);
                }

                if (Convert.ToInt32(ds.Tables[0].Rows[dataRowIndex][0]) != -51000/*total*/) throw new Exception("Bad Indicator");
                horTotal[CurRowIndex - 9] += Convert.ToDecimal(ds.Tables[0].Rows[dataRowIndex][2]);
                if (isLastDFS) EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, CurColIndex + 1, CurRowIndex, CurColIndex + 1, horTotal[CurRowIndex - 9]);
                EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, CurColIndex, CurRowIndex, CurColIndex, ds.Tables[0].Rows[dataRowIndex++][2]);
                CurRowIndex++;
                //~~~~~~~~~~
                CurRowIndex++;

                if (Convert.ToInt32(ds.Tables[0].Rows[dataRowIndex][0]) != -60000/*Displaced Mud*/) throw new Exception("Bad Indicator");
                horTotal[CurRowIndex - 9] += Convert.ToDecimal(ds.Tables[0].Rows[dataRowIndex][2]);
                if (isLastDFS) EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, CurColIndex + 1, CurRowIndex, CurColIndex + 1, horTotal[CurRowIndex - 9]);
                EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, CurColIndex, CurRowIndex, CurColIndex, ds.Tables[0].Rows[dataRowIndex++][2]);
                CurRowIndex++;
                //~~~~~~~~~~
                CurRowIndex++;

                if (Convert.ToInt32(ds.Tables[0].Rows[dataRowIndex][0]) != -61000/*Returned Mud*/) throw new Exception("Bad Indicator");
                horTotal[CurRowIndex - 9] += Convert.ToDecimal(ds.Tables[0].Rows[dataRowIndex][2]);
                if (isLastDFS) EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, CurColIndex + 1, CurRowIndex, CurColIndex + 1, horTotal[CurRowIndex - 9]);
                EPP_ExcelManager.SetDecimalValue(sheet, CurRowIndex, CurColIndex, CurRowIndex, CurColIndex, ds.Tables[0].Rows[dataRowIndex++][2]);
                CurRowIndex++;
                //~~~~~~~~~~

                //Chart  
                {
                    {
                        int startRow = 37 + noRec;
                        int endRow = startRow + 3 + noLoss;

                        var pieChart = (ExcelPieChart)((ExcelChartsheet)sheet.Workbook.Worksheets[sheet.Index + processedDfsCount * 2 + 1]).Chart;
                        pieChart.Series.Add
                        (
                        ExcelCellBase.GetFullAddress(sheet.Name, ExcelCellBase.GetAddress(startRow, 5 + processedDfsCount, endRow, 5 + processedDfsCount)),//loss
                        ExcelCellBase.GetFullAddress(sheet.Name, ExcelCellBase.GetAddress(startRow, 2, endRow, 2))//name
                        );

                        pieChart.Legend.Remove();
                        pieChart.Title.Text = "Mud Losses Volume Analysis OF " + dfs + Environment.NewLine +
                                              "(" + projectName + "_" + rigName + "_" + wellName + "_" + holeSection + ")";

                        pieChart.DataLabel.ShowCategory = true;
                        pieChart.DataLabel.ShowPercent = true;
                        pieChart.DataLabel.ShowValue = true;
                        pieChart.DataLabel.ShowLeaderLines = true;
                    }

                    {
                        int startRow = 37 + noRec;
                        int endRow = startRow + 3 + noLoss;

                        var pieChart = (ExcelPieChart)((ExcelChartsheet)sheet.Workbook.Worksheets[sheet.Index + processedDfsCount * 2 + 2]).Chart;

                        string names = ExcelCellBase.GetFullAddress(sheet.Name, ExcelCellBase.GetAddress(10, 2)) + "," +//rain
                                       ExcelCellBase.GetFullAddress(sheet.Name, ExcelCellBase.GetAddress(11, 2)) + "," +//gain
                                       ExcelCellBase.GetFullAddress(sheet.Name, ExcelCellBase.GetAddress(12, 2)) + "," +//rec
                                       ExcelCellBase.GetFullAddress(sheet.Name, ExcelCellBase.GetAddress(14 + noRec, 2)) + "," +//built
                                       ExcelCellBase.GetFullAddress(sheet.Name, ExcelCellBase.GetAddress(22 + noRec, 2)) + "," +//treat
                                       ExcelCellBase.GetFullAddress(sheet.Name, ExcelCellBase.GetAddress(32 + noRec, 2)) + "," +//Using
                                       ExcelCellBase.GetFullAddress(sheet.Name, ExcelCellBase.GetAddress(34 + noRec, 2)) + "," +//Used for
                                       ExcelCellBase.GetFullAddress(sheet.Name, ExcelCellBase.GetAddress(36 + noRec, 2)) + "," +//Mud Losses
                                       ExcelCellBase.GetFullAddress(sheet.Name, ExcelCellBase.GetAddress(42 + noRec + noLoss, 2)) + "," +//Transferred 
                                       ExcelCellBase.GetFullAddress(sheet.Name, ExcelCellBase.GetAddress(44 + noRec + noLoss + noTrans, 2)) + "," +//Displaced 
                                       ExcelCellBase.GetFullAddress(sheet.Name, ExcelCellBase.GetAddress(46 + noRec + noLoss + noTrans, 2));//Returned 


                        string totals = ExcelCellBase.GetFullAddress(sheet.Name, ExcelCellBase.GetAddress(10, 5 + processedDfsCount)) + "," +//rain
                                        ExcelCellBase.GetFullAddress(sheet.Name, ExcelCellBase.GetAddress(11, 5 + processedDfsCount)) + "," +//gain
                                        ExcelCellBase.GetFullAddress(sheet.Name, ExcelCellBase.GetAddress(13 + noRec, 5 + processedDfsCount)) + "," +//rec
                                        ExcelCellBase.GetFullAddress(sheet.Name, ExcelCellBase.GetAddress(21 + noRec, 5 + processedDfsCount)) + "," +//built
                                        ExcelCellBase.GetFullAddress(sheet.Name, ExcelCellBase.GetAddress(31 + noRec, 5 + processedDfsCount)) + "," +//treat
                                        ExcelCellBase.GetFullAddress(sheet.Name, ExcelCellBase.GetAddress(33 + noRec, 5 + processedDfsCount)) + "," +//Using
                                        ExcelCellBase.GetFullAddress(sheet.Name, ExcelCellBase.GetAddress(35 + noRec, 5 + processedDfsCount)) + "," +//Used for
                                        ExcelCellBase.GetFullAddress(sheet.Name, ExcelCellBase.GetAddress(41 + noRec + noLoss, 5 + processedDfsCount)) + "," +//Mud Losses
                                        ExcelCellBase.GetFullAddress(sheet.Name, ExcelCellBase.GetAddress(43 + noRec + noLoss + noTrans, 5 + processedDfsCount)) + "," +//Transferred 
                                        ExcelCellBase.GetFullAddress(sheet.Name, ExcelCellBase.GetAddress(45 + noRec + noLoss + noTrans, 5 + processedDfsCount)) + "," +//Displaced 
                                        ExcelCellBase.GetFullAddress(sheet.Name, ExcelCellBase.GetAddress(47 + noRec + noLoss + noTrans, 5 + processedDfsCount));//Returned 

                        pieChart.Series.Add
                        (
                        ExcelCellBase.GetFullAddress(sheet.Name, totals),//total
                        ExcelCellBase.GetFullAddress(sheet.Name, names)//name
                        );

                        pieChart.Legend.Remove();
                        pieChart.Title.Text = "Volume Analysis OF " + dfs + Environment.NewLine +
                                              "(" + projectName + "_" + rigName + "_" + wellName + "_" + holeSection + ")";

                        pieChart.DataLabel.ShowCategory = true;
                        pieChart.DataLabel.ShowPercent = true;
                        pieChart.DataLabel.ShowValue = true;
                        pieChart.DataLabel.ShowLeaderLines = true;
                    }
                }
                //~~~~~~~~~~
                if (isLastDFS)//total column
                {
                    {
                        int startRow = 37 + noRec;
                        int endRow = startRow + 3 + noLoss;

                        var pieChart = (ExcelPieChart)((ExcelChartsheet)sheet.Workbook.Worksheets[sheet.Index + noDFSs * 2 + 1]).Chart;
                        pieChart.Series.Add
                        (
                        ExcelCellBase.GetFullAddress(sheet.Name, ExcelCellBase.GetAddress(startRow, 5 + noDFSs, endRow, 5 + noDFSs)),//loss
                        ExcelCellBase.GetFullAddress(sheet.Name, ExcelCellBase.GetAddress(startRow, 2, endRow, 2))//name
                        );

                        pieChart.Legend.Remove();
                        //pieChart.Title.Text = "Mud Volume Analysis OF " + dfs + Environment.NewLine +
                        pieChart.Title.Text = "Hole Section Mud Losses Analysis OF " + dfs + Environment.NewLine +
                                              "(" + projectName + "_" + rigName + "_" + wellName + "_" + holeSection + ")";

                        pieChart.DataLabel.ShowCategory = true;
                        pieChart.DataLabel.ShowPercent = true;
                        pieChart.DataLabel.ShowValue = true;
                        pieChart.DataLabel.ShowLeaderLines = true;
                    }

                    {
                        int startRow = 37 + noRec;
                        int endRow = startRow + 3 + noLoss;

                        var pieChart = (ExcelPieChart)((ExcelChartsheet)sheet.Workbook.Worksheets[sheet.Index + noDFSs * 2 + 2]).Chart;

                        string names = ExcelCellBase.GetFullAddress(sheet.Name, ExcelCellBase.GetAddress(10, 2)) + "," +//rain
                                       ExcelCellBase.GetFullAddress(sheet.Name, ExcelCellBase.GetAddress(11, 2)) + "," +//gain
                                       ExcelCellBase.GetFullAddress(sheet.Name, ExcelCellBase.GetAddress(12, 2)) + "," +//rec
                                       ExcelCellBase.GetFullAddress(sheet.Name, ExcelCellBase.GetAddress(14 + noRec, 2)) + "," +//built
                                       ExcelCellBase.GetFullAddress(sheet.Name, ExcelCellBase.GetAddress(22 + noRec, 2)) + "," +//treat
                                       ExcelCellBase.GetFullAddress(sheet.Name, ExcelCellBase.GetAddress(32 + noRec, 2)) + "," +//Using
                                       ExcelCellBase.GetFullAddress(sheet.Name, ExcelCellBase.GetAddress(34 + noRec, 2)) + "," +//Used for
                                       ExcelCellBase.GetFullAddress(sheet.Name, ExcelCellBase.GetAddress(36 + noRec, 2)) + "," +//Mud Losses
                                       ExcelCellBase.GetFullAddress(sheet.Name, ExcelCellBase.GetAddress(42 + noRec + noLoss, 2)) + "," +//Transferred 
                                       ExcelCellBase.GetFullAddress(sheet.Name, ExcelCellBase.GetAddress(44 + noRec + noLoss + noTrans, 2)) + "," +//Displaced 
                                       ExcelCellBase.GetFullAddress(sheet.Name, ExcelCellBase.GetAddress(46 + noRec + noLoss + noTrans, 2));//Returned 


                        string totals = ExcelCellBase.GetFullAddress(sheet.Name, ExcelCellBase.GetAddress(10, 5 + noDFSs)) + "," +//rain
                                        ExcelCellBase.GetFullAddress(sheet.Name, ExcelCellBase.GetAddress(11, 5 + noDFSs)) + "," +//gain
                                        ExcelCellBase.GetFullAddress(sheet.Name, ExcelCellBase.GetAddress(13 + noRec, 5 + noDFSs)) + "," +//rec
                                        ExcelCellBase.GetFullAddress(sheet.Name, ExcelCellBase.GetAddress(21 + noRec, 5 + noDFSs)) + "," +//built
                                        ExcelCellBase.GetFullAddress(sheet.Name, ExcelCellBase.GetAddress(31 + noRec, 5 + noDFSs)) + "," +//treat
                                        ExcelCellBase.GetFullAddress(sheet.Name, ExcelCellBase.GetAddress(33 + noRec, 5 + noDFSs)) + "," +//Using
                                        ExcelCellBase.GetFullAddress(sheet.Name, ExcelCellBase.GetAddress(35 + noRec, 5 + noDFSs)) + "," +//Used for
                                        ExcelCellBase.GetFullAddress(sheet.Name, ExcelCellBase.GetAddress(41 + noRec + noLoss, 5 + noDFSs)) + "," +//Mud Losses
                                        ExcelCellBase.GetFullAddress(sheet.Name, ExcelCellBase.GetAddress(43 + noRec + noLoss + noTrans, 5 + noDFSs)) + "," +//Transferred 
                                        ExcelCellBase.GetFullAddress(sheet.Name, ExcelCellBase.GetAddress(45 + noRec + noLoss + noTrans, 5 + noDFSs)) + "," +//Displaced 
                                        ExcelCellBase.GetFullAddress(sheet.Name, ExcelCellBase.GetAddress(47 + noRec + noLoss + noTrans, 5 + noDFSs));//Returned 

                        pieChart.Series.Add
                        (
                        ExcelCellBase.GetFullAddress(sheet.Name, totals),//total
                        ExcelCellBase.GetFullAddress(sheet.Name, names)//name
                        );

                        pieChart.Legend.Remove();
                        //pieChart.Title.Text = "Total Volume Analysis OF " + dfs + Environment.NewLine +
                        pieChart.Title.Text = "Hole Section Total Volume Analysis OF " + dfs + Environment.NewLine +
                                              "(" + projectName + "_" + rigName + "_" + wellName + "_" + holeSection + ")";

                        pieChart.DataLabel.ShowCategory = true;
                        pieChart.DataLabel.ShowPercent = true;
                        pieChart.DataLabel.ShowValue = true;
                        pieChart.DataLabel.ShowLeaderLines = true;
                    }
                }
                //~~~~~~~~~~
                return dataRowIndex;
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
                sumLossesVol = 0;
                //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                if (!CheckParameters(recap))
                    return "Invalid Parameters";
                //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                holeSection = holeSizeLabel;

                holeSizeLabel = holeSizeLabel.Replace("\'", "\'\'");
                //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                // *********************** Header ***********************
                {//Images
                    EPP_ExcelManager.SetImage(sheet, 40, 10, 140, 60, DMR.Images.PDFExcellLogo, "pdf-logo");//Logo

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
                            EPP_ExcelManager.SetImage(sheet, 670, 10, 80, 40, contractorImage, "Contractor Image");

                        if (operatorImage != null)
                            EPP_ExcelManager.SetImage(sheet, 750, 10, 80, 40, operatorImage, "Operator Image");
                    }
                    else if (useContractorImage)
                    {
                        if (contractorImage != null)
                            EPP_ExcelManager.SetImage(sheet, 670, 10, 120, 60, contractorImage, "Contractor Image");
                    }
                    else if (useOperatorImage)
                    {
                        if (operatorImage != null)
                            EPP_ExcelManager.SetImage(sheet, 670, 10, 120, 60, operatorImage, "Operator Image");
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

                        EPP_ExcelManager.SetText(sheet, 5, 6, "Drilled Interval (" + selUnitDepth + "):");//Drilled Interval (SU)
                        EPP_ExcelManager.SetText(sheet, 5, 7, ds.Tables[0].Rows[0][5].ToString());//Drilled Interval
                        EPP_ExcelManager.SetText(sheet, 7, 7, ds.Tables[0].Rows[0][6].ToString());//Casing Depth
                        ds.Dispose();
                    }

                    {//Code , Date
                        EPP_ExcelManager.SetText(sheet, 5, 9, code);//Code
                        EPP_ExcelManager.SetText(sheet, 7, 9, reportingDate.ToShortDateString() + " - " + shamsiReportingDate);//Date
                    }
                }
                //**********************************************
                {
                    string query =
                        " select * from  fn_Reporting_Recap_HoleSectionVolumeAnalysis(" + wellID + "," + "\'" + holeSizeLabel + "\'" + ", 0, 0, \'\', 1)";//generate labels

                    DataSet ds = new DataSet();
                    if (ConnectionManager.ExecQuery(query, ref ds))
                    {
                        if (ds.Tables[0].Columns.Count != 3)
                            return "Report Generating Error (Data-1)";

                        this.noDFSs = noDFSs;

                        PrepareSheetFormat(sheet, ds);

                        ds.Dispose();
                    }
                }
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
        public string GenerateReportForNextDFS(ReportingBase recap, ExcelWorksheet sheet, int DFSAutoID, string DFS)
        {
            try
            {
                // ******************* Body ***********************/
                {
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

                    string query =
                        " select * from  fn_Reporting_Recap_HoleSectionVolumeAnalysis(" + wellID + "," + "\'" + holeSizeLabel + "\'" + "," + actLossFactor + "," +
                                                                                        DFSAutoID.ToString() + ",\'" + DFS + "\', 0) ";//generate values

                    DataSet ds = new DataSet();
                    if (ConnectionManager.ExecQuery(query, ref ds))
                    {
                        if (ds.Tables[0].Columns.Count != 3)
                            return "Report Generating Error (Data-1)";

                        if (NewDFS_Section(sheet, ds, 0, DFS, processedDfsCount == noDFSs - 1) < 0)
                        {
                            return "Report Generating Error (Data-2)";
                        }
                        processedDfsCount++;

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
