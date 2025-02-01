using OfficeOpenXml;
using OfficeOpenXml.Drawing.Chart;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;

namespace DMR.Reporting.Report_Generator.Daily
{
    class DailyDMR : ReportingBase
    {
        //-------------------------------------------------------
        //from parameters
        Int64 wellID = -1;
        int prjID = -1;
        string code = "";
        DateTime reportingDate;
        string shamsiReportingDate;
        int repNum = -1;
        bool useOperatorImage = false;
        bool useContractorImage = false;
        string selCurrency = "";
        //-------------------------------------------------------
        //others
        string operatorName = "";
        string projectName = "";
        string wellName = "";
        string thisReportDate_Shamsi = "";
        //-------------------------------------------------------
        Int64 thisRepID = -1;
        bool reportIsValid = false;
        //-------------------------------------------------------
        public override bool CheckParameters()
        {
            try
            {
                wellID = GetParam<Int64>("wellID");
                prjID = GetParam<int>("prjID");
                code = GetParam<string>("code");
                repNum = GetParam<int>("repNum");
                reportingDate = GetParam<DateTime>("reportingDate");
                shamsiReportingDate = GetParam<string>("shamsiReportingDate");
                useOperatorImage = GetParam<bool>("useOperatorImage");
                useContractorImage = GetParam<bool>("useContractorImage");
                useContractorImage = GetParam<bool>("useContractorImage");
                //~~~~~~~~
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
        public string SelUnits_CommaSeparated(string group, Dictionary<string, List<string>> userSelUnitsForReport)
        {
            List<string> toUnits = null;

            if (!userSelUnitsForReport.TryGetValue(group, out toUnits))
                return "";// Error !!!!!
            //~~~~~~~
            string result = "";

            for (int i = 0; i < toUnits.Count; i++)
            {
                if (i != 0)
                    result += ", ";

                result += toUnits[i];
            }
            //~~~~~~~
            return result;
        }
        //-------------------------------------------------------
        public string ConvertToManyUnits_SlashSeparated(Dictionary<string, decimal> mudPorps,
                                                        string propName, string group,
                                                        Dictionary<string, string> allSelectedUnitsInProject,
                                                        Dictionary<string, List<string>> userSelUnitsForReport)
        {
            decimal val = 0;
            if (!mudPorps.TryGetValue(propName, out val) || val == MudPropForm.INVALID_VALUE)
                return "";// Error !!!!!
            //~~~~~~~
            string fromUnit = "";

            if (!allSelectedUnitsInProject.TryGetValue(group, out fromUnit))
                return "";// Error !!!!!
            //~~~~~~~
            List<string> toUnits = null;

            if (!userSelUnitsForReport.TryGetValue(group, out toUnits))
                return "";// Error !!!!!
            //~~~~~~~
            string result = "";

            for (int i = 0; i < toUnits.Count; i++)
            {
                string conVal = UnitConverter.Convert(group, fromUnit, toUnits[i], val.ToString());

                if (i != 0)
                    result += "/ ";

                result += Convert.ToDecimal(conVal).ToString("0.###");
            }
            //~~~~~~~
            return result;
        }
        //-------------------------------------------------------
        public string ConvertToManyUnits_SlashSeparated(string valStr, string group,
                                                        Dictionary<string, string> allSelectedUnitsInProject,
                                                        Dictionary<string, List<string>> userSelUnitsForReport)
        {
            if (Convert.ToDecimal(valStr) == MudPropForm.INVALID_VALUE)
                return "";// Error !!!!!
            //~~~~~~~
            string fromUnit = "";

            if (!allSelectedUnitsInProject.TryGetValue(group, out fromUnit))
                return "";// Error !!!!!
            //~~~~~~~ 
            List<string> toUnits = null;

            if (!userSelUnitsForReport.TryGetValue(group, out toUnits))
                return "";// Error !!!!!
            //~~~~~~~
            string result = "";

            for (int i = 0; i < toUnits.Count; i++)
            {
                string conVal = UnitConverter.Convert(group, fromUnit, toUnits[i], valStr);

                if (i != 0)
                    result += "/ ";

                result += Convert.ToDecimal(conVal).ToString("0.###");
            }
            //~~~~~~~
            return result;
        }
        //-------------------------------------------------------
        decimal sumProductInvDailyCost = 0, sumProductInvtotalCost = 0;//for main dmr 
        //-------------------------------------------------------
        private string InsertProductInvReport(ExcelWorksheet ews, Image contractorImage, Image operatorImage, string excelImgNamePrefix, Dictionary<string, string> allSelectedUnits)
        {
            try
            {
                // *********************** Header ***********************
                {//Images
                    EPP_ExcelManager.SetImage(ews, 40, 5, 140, 60, DMR.Images.PDFExcellLogo, excelImgNamePrefix + "pdf-logo");//Logo
                    //~~~~~~~~~~~~~~~~~~~~~
                    if (useOperatorImage && useContractorImage)
                    {
                        if (contractorImage != null)
                            EPP_ExcelManager.SetImage(ews, 1300, 10, 80, 40, contractorImage, excelImgNamePrefix + "Contractor Image");

                        if (operatorImage != null)
                            EPP_ExcelManager.SetImage(ews, 1380, 10, 80, 40, operatorImage, excelImgNamePrefix + "Operator Image");
                    }
                    else if (useContractorImage)
                    {
                        if (contractorImage != null)
                            EPP_ExcelManager.SetImage(ews, 1300, 10, 120, 60, contractorImage, excelImgNamePrefix + "Contractor Image");
                    }
                    else if (useOperatorImage)
                    {
                        if (operatorImage != null)
                            EPP_ExcelManager.SetImage(ews, 1300, 10, 120, 60, operatorImage, excelImgNamePrefix + "Operator Image");
                    }
                }
                //**********************
                {//others
                    string query = " select * from fn_Reporting_DMR_Header_ByWellIDAndRepID(" + wellID.ToString() + "," + thisRepID.ToString() + ")";//one report 


                    DataSet ds = ConnectionManager.ExecQuery(query, 1);
                    if (ds != null)
                    {
                        EPP_ExcelManager.SetText(ews, 4, 4, ds.Tables[0].Rows[0][2].ToString());//Client
                        EPP_ExcelManager.SetText(ews, 5, 4, operatorName = ds.Tables[0].Rows[0][3].ToString());//Operator
                        projectName = ds.Tables[0].Rows[0][4].ToString();
                        EPP_ExcelManager.SetText(ews, 6, 4, ds.Tables[0].Rows[0][6].ToString());//Rig Contractor
                        EPP_ExcelManager.SetText(ews, 7, 4, ds.Tables[0].Rows[0][5].ToString());//Rig Name 

                        EPP_ExcelManager.SetText(ews, 4, 16, ds.Tables[0].Rows[0][0].ToString());//Area             	
                        EPP_ExcelManager.SetText(ews, 5, 16, ds.Tables[0].Rows[0][1].ToString());//Field Name				
                        EPP_ExcelManager.SetText(ews, 6, 16, ds.Tables[0].Rows[0][8].ToString());//Well Classification	
                        EPP_ExcelManager.SetText(ews, 7, 16, wellName = ds.Tables[0].Rows[0][7].ToString() + "/ " + ds.Tables[0].Rows[0][9].ToString());//Well Name/ Well Type

                        if (ds.Tables[0].Rows[0][28].ToString().Trim() != "")
                            EPP_ExcelManager.SetText(ews, 4, 24, ds.Tables[0].Rows[0][28].ToString());//CorrExtRepID		
                        else
                            EPP_ExcelManager.SetText(ews, 4, 24, ds.Tables[0].Rows[0][11].ToString() + "/ " + Convert.ToInt32(ds.Tables[0].Rows[0][12]).ToString("00"));//Report Number/ Rev	


                        EPP_ExcelManager.SetText(ews, 6, 24, Convert.ToDateTime(ds.Tables[0].Rows[0][10]).ToShortDateString());//Spud Date of Well		
                        EPP_ExcelManager.SetText(ews, 7, 24, ds.Tables[0].Rows[0][14].ToString());//Drilling Fluid System		

                        ds.Dispose();
                    }

                    {//Code , Date
                        EPP_ExcelManager.SetText(ews, 3, 2, "Code : " + code);//code
                        EPP_ExcelManager.SetText(ews, 5, 24, reportingDate.ToShortDateString() + " - " + shamsiReportingDate);//Report Date
                    }
                }
                // ******************* Body ***********************
                int rowCount = 0;
                {
                    sumProductInvDailyCost = 0;
                    sumProductInvtotalCost = 0;

                    string query = " select * from  fn_Reporting_DMR_ProductInventory(" + wellID.ToString() + "," + repNum.ToString() + ") order by item ";

                    DataSet ds = new DataSet();
                    if (ConnectionManager.ExecQuery(query, ref ds))
                    {
                        rowCount = ds.Tables[0].Rows.Count;

                        if (rowCount > 0)
                            ews.InsertRow(10 + 1, ds.Tables[0].Rows.Count - 1, 10);//copy cell format [but not merging]

                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            int rIndex = 10 + i;//one-based

                            EPP_ExcelManager.SetText(ews, rIndex, 2, rIndex, 1 + ds.Tables[0].Columns.Count, ds.Tables[0].Rows[i].ItemArray);

                            sumProductInvDailyCost += Convert.ToDecimal(ds.Tables[0].Rows[i][22]);
                            sumProductInvtotalCost += Convert.ToDecimal(ds.Tables[0].Rows[i][23]);
                        }

                        if (rowCount > 0)
                            EPP_ExcelManager.SetBackColor_AlternatingRows(ews, 10, 2, 10 + rowCount - 1, 23, Color.White, Color.FromArgb(204, 255, 255), false);

                        ds.Dispose();
                    }
                }
                // ******************* Units, Currency ***********************
                {
                    EPP_ExcelManager.SetText(ews, 9, 5, selCurrency);//Unit Price

                    EPP_ExcelManager.SetText(ews, 8, 24, "Daily Cost" + Environment.NewLine + selCurrency);//Daily Cost
                    EPP_ExcelManager.SetText(ews, 8, 25, "Total Cost  " + Environment.NewLine + selCurrency);//Total Cost  
                }
                // *********************** Footer ***********************
                {
                    if (rowCount > 0)
                    {
                        string query = " select * from  fn_Get_Remarks(" + wellID.ToString() + "," + repNum.ToString() + ")";

                        DataSet ds = ConnectionManager.ExecQuery(query, 1);
                        if (ds != null)
                        {
                            int rIndex = 9 + rowCount + 2;
                            EPP_ExcelManager.SetText(ews, rIndex, 2, ds.Tables[0].Rows[0][4].ToString());//Material Remarks

                            ds.Dispose();
                        }
                    }
                    //~~~~~~~~~~~~~~~~~~~
                    {
                        string query = " select * from  fn_Reporting_DMR_Footer_ByRepID(" + thisRepID.ToString() + "," + wellID.ToString() + ")";

                        DataSet ds = ConnectionManager.ExecQuery(query, 1);
                        if (ds != null)
                        {
                            //~~~~~~~~~~~~~~~~~~~~~~~
                            int rIndex = 0;
                            //~~~~~~~~~~~~~~~~~~~~~~~
                            if (rowCount > 0)
                                rIndex = 9 + rowCount + 6;
                            else
                                rIndex = 16;

                            EPP_ExcelManager.SetText(ews, rIndex + 0, 2, "DAILY MATERIAL COST (" + selCurrency + ")");
                            EPP_ExcelManager.SetText(ews, rIndex + 1, 2, "DAILY ENGINEERING COST (" + selCurrency + ")");
                            EPP_ExcelManager.SetText(ews, rIndex + 2, 2, "DAILY EQUIPMENT COST (" + selCurrency + ")");
                            EPP_ExcelManager.SetText(ews, rIndex + 3, 2, "DAILY SCREENS COST (" + selCurrency + ")");

                            EPP_ExcelManager.SetText(ews, rIndex + 0, 5, sumProductInvDailyCost.ToString("0.###"));
                            EPP_ExcelManager.SetText(ews, rIndex + 1, 5, ds.Tables[0].Rows[0][12].ToString());//DAILY ENGINEERING COST
                            EPP_ExcelManager.SetText(ews, rIndex + 2, 5, ds.Tables[0].Rows[0][13].ToString());//DAILY EQUIPMENT COST
                            EPP_ExcelManager.SetText(ews, rIndex + 3, 5, sumScreensDailyCost.ToString("0.###"));

                            EPP_ExcelManager.SetText(ews, rIndex + 0, 16, "CUMULATIVE MATERIAL COST (" + selCurrency + ")");
                            EPP_ExcelManager.SetText(ews, rIndex + 1, 16, "CUMULATIVE ENGINEERING COST (" + selCurrency + ")");
                            EPP_ExcelManager.SetText(ews, rIndex + 2, 16, "CUMULATIVE EQUIPMENT COST (" + selCurrency + ")");
                            EPP_ExcelManager.SetText(ews, rIndex + 3, 16, "CUMULATIVE SCREENS COST (" + selCurrency + ")");

                            EPP_ExcelManager.SetText(ews, rIndex + 0, 22, sumProductInvtotalCost.ToString("0.###"));
                            EPP_ExcelManager.SetText(ews, rIndex + 1, 22, ds.Tables[0].Rows[0][12].ToString());//DAILY ENGINEERING COST
                            EPP_ExcelManager.SetText(ews, rIndex + 2, 22, ds.Tables[0].Rows[0][13].ToString());//DAILY EQUIPMENT COST
                            EPP_ExcelManager.SetText(ews, rIndex + 3, 22, sumScreensTotalCost.ToString("0.###"));

                            //~~~~~~~~~~~~~~~~~~~~~~~



                            if (rowCount > 0)
                                rIndex = 9 + rowCount + 11;
                            else
                                rIndex = 21;


                            EPP_ExcelManager.SetText(ews, rIndex + 0, 2, ds.Tables[0].Rows[0][0].ToString());//SenMudEng
                            EPP_ExcelManager.SetText(ews, rIndex + 1, 2, ds.Tables[0].Rows[0][1].ToString());//JunMudEng

                            EPP_ExcelManager.SetText(ews, rIndex + 0, 5, ds.Tables[0].Rows[0][3].ToString());//SenCmpRep		
                            EPP_ExcelManager.SetText(ews, rIndex + 1, 5, ds.Tables[0].Rows[0][4].ToString());//JunCmpRep	

                            EPP_ExcelManager.SetText(ews, rIndex + 0, 18, ds.Tables[0].Rows[0][6].ToString());//EngSup			
                            EPP_ExcelManager.SetText(ews, rIndex + 1, 18, ds.Tables[0].Rows[0][7].ToString());//OpSup	

                            EPP_ExcelManager.SetText(ews, rIndex + 0, 22, ds.Tables[0].Rows[0][10].ToString());//WareHouse  
                            EPP_ExcelManager.SetText(ews, rIndex + 1, 22, ds.Tables[0].Rows[0][11].ToString());// WareHousePhone


                            //EPP_ExcelManager.SetText(ews, rIndex + , , ds.Tables[0].Rows[0][2].ToString());//ProjEng		    
                            //EPP_ExcelManager.SetText(ews, rIndex + , , ds.Tables[0].Rows[0][5].ToString());//ClientRep		
                            //EPP_ExcelManager.SetText(ews, rIndex + , , ds.Tables[0].Rows[0][8].ToString());//EngSupPhone	    
                            //EPP_ExcelManager.SetText(ews, rIndex + , , ds.Tables[0].Rows[0][9].ToString());//OpSupPhone		
                            //~~~~~~~~~~~~~~~~~~~~~~~
                            ds.Dispose();
                        }
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
        decimal sumScreensDailyCost = 0, sumScreensTotalCost = 0;//for main dmr 
        //-------------------------------------------------------
        private string InsertShakerScreensInvInvReport(ExcelWorksheet ews, Image contractorImage, Image operatorImage, string excelImgNamePrefix, Dictionary<string, string> allSelectedUnits)
        {
            try
            {
                // *********************** Header ***********************
                {//Images
                    EPP_ExcelManager.SetImage(ews, 70, 10, 140, 60, DMR.Images.PDFExcellLogo, excelImgNamePrefix + "pdf-logo");//Logo
                    //~~~~~~~~~~~~~~~~~~~~~
                    if (useOperatorImage && useContractorImage)
                    {
                        if (contractorImage != null)
                            EPP_ExcelManager.SetImage(ews, 800, 10, 80, 40, contractorImage, excelImgNamePrefix + "Contractor Image");

                        if (operatorImage != null)
                            EPP_ExcelManager.SetImage(ews, 880, 10, 80, 40, operatorImage, excelImgNamePrefix + "Operator Image");
                    }
                    else if (useContractorImage)
                    {
                        if (contractorImage != null)
                            EPP_ExcelManager.SetImage(ews, 800, 10, 120, 60, contractorImage, excelImgNamePrefix + "Contractor Image");
                    }
                    else if (useOperatorImage)
                    {
                        if (operatorImage != null)
                            EPP_ExcelManager.SetImage(ews, 800, 10, 120, 60, operatorImage, excelImgNamePrefix + "Operator Image");
                    }
                }
                //**********************
                {//others
                    string query = " select * from fn_Reporting_DMR_Header_ByWellIDAndRepID(" + wellID.ToString() + "," + thisRepID.ToString() + ")";


                    DataSet ds = ConnectionManager.ExecQuery(query, 1);
                    if (ds != null)
                    {
                        EPP_ExcelManager.SetText(ews, 4, 4, ds.Tables[0].Rows[0][2].ToString());//Client
                        EPP_ExcelManager.SetText(ews, 5, 4, ds.Tables[0].Rows[0][3].ToString());//Operator
                        EPP_ExcelManager.SetText(ews, 6, 4, ds.Tables[0].Rows[0][6].ToString());//Rig Contractor
                        EPP_ExcelManager.SetText(ews, 7, 4, ds.Tables[0].Rows[0][5].ToString());//Rig Name 

                        EPP_ExcelManager.SetText(ews, 4, 8, ds.Tables[0].Rows[0][0].ToString());//Area             	
                        EPP_ExcelManager.SetText(ews, 5, 8, ds.Tables[0].Rows[0][1].ToString());//Field Name				
                        EPP_ExcelManager.SetText(ews, 6, 8, ds.Tables[0].Rows[0][8].ToString());//Well Classification	
                        EPP_ExcelManager.SetText(ews, 7, 8, ds.Tables[0].Rows[0][7].ToString() + "/ " + ds.Tables[0].Rows[0][9].ToString());//Well Name/ Well Type

                        if (ds.Tables[0].Rows[0][28].ToString().Trim() != "")
                            EPP_ExcelManager.SetText(ews, 4, 15, ds.Tables[0].Rows[0][28].ToString());//CorrExtRepID		
                        else
                            EPP_ExcelManager.SetText(ews, 4, 15, ds.Tables[0].Rows[0][11].ToString() + "/ " + Convert.ToInt32(ds.Tables[0].Rows[0][12]).ToString("00"));//Report Number/ Rev	

                        EPP_ExcelManager.SetText(ews, 6, 15, Convert.ToDateTime(ds.Tables[0].Rows[0][10]).ToShortDateString());//Spud Date of Well		
                        EPP_ExcelManager.SetText(ews, 7, 15, ds.Tables[0].Rows[0][14].ToString());//Drilling Fluid System		

                        ds.Dispose();
                    }

                    {//Date
                        EPP_ExcelManager.SetText(ews, 5, 15, reportingDate.ToShortDateString() + " - " + shamsiReportingDate);//Report Date
                    }
                }
                // ******************* Body ***********************
                int rowCount = 0;
                {
                    string query = " select * from  fn_Reporting_DMR_ShakerScreensInventory(" + wellID.ToString() + "," + repNum.ToString() + ")  order by item ";

                    DataSet ds = new DataSet();
                    if (ConnectionManager.ExecQuery(query, ref ds))
                    {
                        rowCount = ds.Tables[0].Rows.Count;

                        if (rowCount > 0)
                            ews.InsertRow(10 + 1, rowCount - 1, 10);//copy cell format [but not merging]

                        for (int i = 0; i < rowCount; i++)
                        {
                            int rIndex = 10 + i;//one-based

                            EPP_ExcelManager.Merge(ews, rIndex, 3, rIndex, 5);
                            EPP_ExcelManager.Merge(ews, rIndex, 15, rIndex, 16);


                            EPP_ExcelManager.SetText(ews, rIndex, 2, ds.Tables[0].Rows[i][0]);
                            EPP_ExcelManager.SetText(ews, rIndex, 3, ds.Tables[0].Rows[i][1]);
                            EPP_ExcelManager.SetDecimalValue(ews, rIndex, 6, ds.Tables[0].Rows[i][2]);
                            EPP_ExcelManager.SetDecimalValue(ews, rIndex, 7, ds.Tables[0].Rows[i][3]);
                            EPP_ExcelManager.SetDecimalValue(ews, rIndex, 8, ds.Tables[0].Rows[i][4]);
                            EPP_ExcelManager.SetDecimalValue(ews, rIndex, 9, ds.Tables[0].Rows[i][5]);
                            EPP_ExcelManager.SetDecimalValue(ews, rIndex, 10, ds.Tables[0].Rows[i][6]);
                            EPP_ExcelManager.SetDecimalValue(ews, rIndex, 11, ds.Tables[0].Rows[i][7]);
                            EPP_ExcelManager.SetDecimalValue(ews, rIndex, 12, ds.Tables[0].Rows[i][8]);
                            EPP_ExcelManager.SetDecimalValue(ews, rIndex, 13, ds.Tables[0].Rows[i][9]);
                            EPP_ExcelManager.SetDecimalValue(ews, rIndex, 14, ds.Tables[0].Rows[i][10]);
                            EPP_ExcelManager.SetDecimalValue(ews, rIndex, 15, ds.Tables[0].Rows[i][11]);

                            sumScreensDailyCost += Convert.ToDecimal(ds.Tables[0].Rows[i][10]);
                            sumScreensTotalCost += Convert.ToDecimal(ds.Tables[0].Rows[i][11]);
                        }

                        if (rowCount > 0)
                            EPP_ExcelManager.SetBackColor_AlternatingRows(ews, 10, 2, 10 + rowCount - 1, 23, Color.White, Color.FromArgb(204, 255, 255), true);

                        ds.Dispose();
                    }
                }
                // ******************* Units, Currency ***********************
                {
                    EPP_ExcelManager.SetText(ews, 8, 6, "Unit Cost" + Environment.NewLine + selCurrency);//Unit Cost

                    EPP_ExcelManager.SetText(ews, 8, 14, "Daily Cost" + Environment.NewLine + selCurrency);//Daily Cost
                    EPP_ExcelManager.SetText(ews, 8, 15, "Total Cost  " + Environment.NewLine + selCurrency);//Total Cost  
                }
                // *********************** Footer ***********************
                {
                    string query = " select * from  fn_Reporting_DMR_Footer_ByRepID(" + thisRepID.ToString() + "," + wellID.ToString() + ")";

                    DataSet ds = ConnectionManager.ExecQuery(query, 1);
                    if (ds != null)
                    {
                        //~~~~~~~~~~~~~~~~~~~~~~~
                        int rIndex = 0;
                        //~~~~~~~~~~~~~~~~~~~~~~~
                        if (rowCount > 0)
                            rIndex = 9 + rowCount + 2;
                        else
                            rIndex = 12;

                        EPP_ExcelManager.SetText(ews, rIndex + 0, 2, ds.Tables[0].Rows[0][0].ToString());//SenMudEng
                        EPP_ExcelManager.SetText(ews, rIndex + 1, 2, ds.Tables[0].Rows[0][1].ToString());//JunMudEng

                        EPP_ExcelManager.SetText(ews, rIndex + 0, 4, ds.Tables[0].Rows[0][3].ToString());//SenCmpRep		
                        EPP_ExcelManager.SetText(ews, rIndex + 1, 4, ds.Tables[0].Rows[0][4].ToString());//JunCmpRep	

                        EPP_ExcelManager.SetText(ews, rIndex + 0, 5, ds.Tables[0].Rows[0][6].ToString());//EngSup			
                        EPP_ExcelManager.SetText(ews, rIndex + 1, 5, ds.Tables[0].Rows[0][7].ToString());//OpSup	

                        EPP_ExcelManager.SetText(ews, rIndex + 0, 7, ds.Tables[0].Rows[0][10].ToString());//WareHouse		
                        EPP_ExcelManager.SetText(ews, rIndex + 1, 7, ds.Tables[0].Rows[0][11].ToString());//WareHousePhone

                        //EPP_ExcelManager.SetText(ews, rIndex + , , ds.Tables[0].Rows[0][2].ToString());//ProjEng		    
                        //EPP_ExcelManager.SetText(ews, rIndex + , , ds.Tables[0].Rows[0][5].ToString());//ClientRep		
                        //EPP_ExcelManager.SetText(ews, rIndex + , , ds.Tables[0].Rows[0][8].ToString());//EngSupPhone	    
                        //EPP_ExcelManager.SetText(ews, rIndex + , , ds.Tables[0].Rows[0][9].ToString());//OpSupPhone	
                        //EPP_ExcelManager.SetText(ews, rIndex + , , ds.Tables[0].Rows[0][12].ToString());//EngCostPerDay		
                        //EPP_ExcelManager.SetText(ews, rIndex + , , ds.Tables[0].Rows[0][13].ToString());//EqCostPerDay

                        EPP_ExcelManager.SetText(ews, rIndex - 1, 10, "DAILY SCREENS COST (" + selCurrency + ")");
                        EPP_ExcelManager.SetText(ews, rIndex, 10, sumScreensDailyCost.ToString("0.###"));

                        EPP_ExcelManager.SetText(ews, rIndex - 1, 13, "CUMULATIVE SCREENS COST (" + selCurrency + ")");
                        EPP_ExcelManager.SetText(ews, rIndex, 13, sumScreensTotalCost.ToString("0.###"));

                        //~~~~~~~~~~~~~~~~~~~~~~~
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
        private string InsertMainDMRReport(ExcelWorksheet ews, Image contractorImage, Image operatorImage, string excelImgNamePrefix,
            Dictionary<string, string> allSelectedUnitsInProject,
            Dictionary<string/*group*/, List<string>/*selected units*/> userSelUnitsForReport)
        {
            try
            {
                //******************************************************
                string selUnitDepth = "";
                string selUnitVol = "";
                string selUnitMW = "";
                string selUnitDischarge = "";
                string selUnitVolPerStroke = "";
                string selUnitFlowRate = "";
                string selUnitLinerLenAndDia = "";
                string selUnitRop = "";
                string selUnitPressure = "";
                string selUnitNozzleSize = "";
                string selUnitWeightOnBit = "";
                string selUnitHoleBitSize = "";
                string selUnitNozzleVelocity = "";
                //************
                if (
                    !allSelectedUnitsInProject.TryGetValue("Liquid Volume", out selUnitVol) ||
                    !allSelectedUnitsInProject.TryGetValue("Depth", out selUnitDepth) ||
                    !allSelectedUnitsInProject.TryGetValue("Mud Weight", out selUnitMW) ||
                    !allSelectedUnitsInProject.TryGetValue("Discharge,Loss Rate", out selUnitDischarge) ||
                    !allSelectedUnitsInProject.TryGetValue("Volume/Stroke", out selUnitVolPerStroke) ||
                    !allSelectedUnitsInProject.TryGetValue("Flow Rate", out selUnitFlowRate) ||
                    !allSelectedUnitsInProject.TryGetValue("Liner Length and Diameter", out selUnitLinerLenAndDia) ||
                    !allSelectedUnitsInProject.TryGetValue("ROP", out selUnitRop) ||
                    !allSelectedUnitsInProject.TryGetValue("Pressure", out selUnitPressure) ||
                    !allSelectedUnitsInProject.TryGetValue("Nozzle Size", out selUnitNozzleSize) ||
                    !allSelectedUnitsInProject.TryGetValue("Weight on Bit (WOB)", out selUnitWeightOnBit) ||
                    !allSelectedUnitsInProject.TryGetValue("Hole,Bit Size", out selUnitHoleBitSize) ||
                    !allSelectedUnitsInProject.TryGetValue("Nozzle Velocity", out selUnitNozzleVelocity)
                    )
                    return "Error fetching selected units";
                //******************************************************
                //common computations for all volume 
                Helpers.Computation.TotalVolumeManagement tvmComp = new Helpers.Computation.TotalVolumeManagement();
                //~~~~ 
                tvmComp.in_selUnitVol = selUnitVol;
                tvmComp.in_selUnitDepth = selUnitDepth;
                tvmComp.in_selUnitDischarge = selUnitDischarge;
                tvmComp.in_selUnitFlowRate = selUnitFlowRate;
                tvmComp.in_selUnitVolPerStroke = selUnitVolPerStroke;
                //~~~~
                tvmComp.Compute(thisRepID, frmMain.selected_RW_WellID);
                //******************************************************
                //******************************************************
                Helpers.Computation.Hydraulic hydComp = new Helpers.Computation.Hydraulic();
                //~~~
                hydComp.in_selUnitPressure = selUnitPressure;
                hydComp.in_selUnitHoleBitSize = selUnitHoleBitSize;
                hydComp.in_selUnitWeightOnBit = selUnitWeightOnBit;
                hydComp.in_selUnitNozzleSize = selUnitNozzleSize;
                hydComp.in_selUnitNozzleVelocity = selUnitNozzleVelocity;

                hydComp.in_selUnitFlowRate = selUnitFlowRate;
                hydComp.in_selUnitVolPerStroke = selUnitVolPerStroke;
                hydComp.in_selUnitLinerLenAndDia = selUnitLinerLenAndDia;
                hydComp.in_selUnitMW = selUnitMW;
                hydComp.in_selUnitDepth = selUnitDepth;
                //~~~
                hydComp.Compute(thisRepID, frmMain.selected_RW_WellID);
                //******************************************************

                // *********************** Header ***********************
                {//Images
                    EPP_ExcelManager.SetImage(ews, 40, 20, 140, 60, DMR.Images.PDFExcellLogo, excelImgNamePrefix + "pdf-logo");//Logo
                    //~~~~~~~~~~~~~~~~~~~~~
                    if (useOperatorImage && useContractorImage)
                    {
                        if (contractorImage != null)
                            EPP_ExcelManager.SetImage(ews, 1020, 30, 80, 40, contractorImage, excelImgNamePrefix + "Contractor Image");

                        if (operatorImage != null)
                            EPP_ExcelManager.SetImage(ews, 1100, 30, 80, 40, operatorImage, excelImgNamePrefix + "Operator Image");
                    }
                    else if (useContractorImage)
                    {
                        if (contractorImage != null)
                            EPP_ExcelManager.SetImage(ews, 1100, 30, 120, 60, contractorImage, excelImgNamePrefix + "Contractor Image");
                    }
                    else if (useOperatorImage)
                    {
                        if (operatorImage != null)
                            EPP_ExcelManager.SetImage(ews, 1100, 30, 120, 60, operatorImage, excelImgNamePrefix + "Operator Image");
                    }
                }
                //**********************
                {//others
                    string query = " select * from fn_Reporting_DMR_Header_ByWellIDAndRepID(" + wellID.ToString() + "," + thisRepID.ToString() + ")";


                    DataSet ds = ConnectionManager.ExecQuery(query, 1);
                    if (ds != null)
                    {
                        EPP_ExcelManager.SetText(ews, 5, 4, ds.Tables[0].Rows[0][2].ToString()); //ClientName  
                        EPP_ExcelManager.SetText(ews, 6, 4, ds.Tables[0].Rows[0][3].ToString()); //OperatorName
                        EPP_ExcelManager.SetText(ews, 7, 4, ds.Tables[0].Rows[0][6].ToString()); //RigContractor
                        EPP_ExcelManager.SetText(ews, 8, 4, ds.Tables[0].Rows[0][5].ToString()); //RigName  
                        EPP_ExcelManager.SetText(ews, 9, 4, ds.Tables[0].Rows[0][15].ToString()); //WellStatus
                        EPP_ExcelManager.SetText(ews, 10, 4, ds.Tables[0].Rows[0][23].ToString() +  //Fm  
                                                      "/ " + ds.Tables[0].Rows[0][24].ToString() +  //Mb  
                                                      "/ " + ds.Tables[0].Rows[0][25].ToString()); //SubMb  
                        EPP_ExcelManager.SetText(ews, 11, 4, ds.Tables[0].Rows[0][26].ToString()); //Lithology


                        EPP_ExcelManager.SetText(ews, 5, 13, ds.Tables[0].Rows[0][0].ToString()); //Area
                        EPP_ExcelManager.SetText(ews, 6, 13, ds.Tables[0].Rows[0][1].ToString()); //FieldName   
                        EPP_ExcelManager.SetText(ews, 7, 13, ds.Tables[0].Rows[0][8].ToString()); //WellClassification
                        EPP_ExcelManager.SetText(ews, 8, 13, ds.Tables[0].Rows[0][7].ToString() + //WellName  
                                                     "/ " + ds.Tables[0].Rows[0][9].ToString() +//WellType 
                                                     "/ " + ds.Tables[0].Rows[0][29].ToString()); //St 

                        EPP_ExcelManager.SetText(ews, 9, 13, ds.Tables[0].Rows[0][13].ToString()); //RepHS  
                        EPP_ExcelManager.SetDecimalValue(ews, 10, 13, ds.Tables[0].Rows[0][16]); //PrevTVD   
                        EPP_ExcelManager.SetDecimalValue(ews, 10, 17, ds.Tables[0].Rows[0][17]); //PrevMD    
                        EPP_ExcelManager.SetDecimalValue(ews, 11, 13, ds.Tables[0].Rows[0][18]); //NightTVD  
                        EPP_ExcelManager.SetDecimalValue(ews, 11, 17, ds.Tables[0].Rows[0][19]); //NightMD 

                        if (ds.Tables[0].Rows[0][28].ToString().Trim() != "")
                            EPP_ExcelManager.SetText(ews, 5, 24, ds.Tables[0].Rows[0][28].ToString());//CorrExtRepID		
                        else
                            EPP_ExcelManager.SetText(ews, 5, 24, ds.Tables[0].Rows[0][11].ToString() + "/ " + Convert.ToInt32(ds.Tables[0].Rows[0][12]).ToString("00"));//Report Number/ Rev	

                        PersianCalendar pc = new PersianCalendar();

                        EPP_ExcelManager.SetText(ews, 7, 24, Convert.ToDateTime(ds.Tables[0].Rows[0][10]).ToShortDateString().ToString() + "-"
                                                             + ShamsiDate.ShamsiEquivalent(Convert.ToDateTime(ds.Tables[0].Rows[0][10]))); //WellSpudDate  
                        EPP_ExcelManager.SetText(ews, 8, 24, ds.Tables[0].Rows[0][14].ToString()); //RepDFS 
                        EPP_ExcelManager.SetDecimalValue(ews, 9, 24, ds.Tables[0].Rows[0][22]); //DailyDrilling
                        EPP_ExcelManager.SetDecimalValue(ews, 10, 24, ds.Tables[0].Rows[0][27]); //AverageROP 
                        EPP_ExcelManager.SetDecimalValue(ews, 11, 24, ds.Tables[0].Rows[0][20]); //BitTVD  
                        EPP_ExcelManager.SetDecimalValue(ews, 11, 26, ds.Tables[0].Rows[0][21]); //BitMD 

                        //EPP_ExcelManager.SetText(ews, , , ds.Tables[0].Rows[0][4].ToString()); //ProjectName 

                        ds.Dispose();
                    }

                    {//Code , Date 
                        EPP_ExcelManager.SetText(ews, 4, 1, "Code : " + code);//code
                        //EPP_ExcelManager.SetText(ews, 6, 24, reportingDate.ToShortDateString() + " - " + shamsiReportingDate);//Report Date

                        EPP_ExcelManager.SetText(ews, 6, 24, Convert.ToDateTime(ds.Tables[0].Rows[0][30]).ToShortDateString().ToString() + "-"
                                                             + ShamsiDate.ShamsiEquivalent(Convert.ToDateTime(ds.Tables[0].Rows[0][30])));//Report Date                      
                    }

                    thisReportDate_Shamsi = ShamsiDate.ShamsiEquivalent(Convert.ToDateTime(ds.Tables[0].Rows[0][30]));
                }
                // ******************* Body ***********************
                {
                    {//Drilling Assembly
                        string query = " select * from fn_Get_DMR_DrillingAssembly_ByRepID(" + thisRepID.ToString() + ")";

                        DataSet ds = new DataSet();
                        if (ConnectionManager.ExecQuery(query, ref ds))
                        {
                            int m = Math.Min(15, ds.Tables[0].Rows.Count);

                            for (int i = 0; i < m; i++)
                            {
                                EPP_ExcelManager.SetText(ews, 15 + i, 1, ds.Tables[0].Rows[i][0].ToString()); // Item
                                EPP_ExcelManager.SetDecimalValue(ews, 15 + i, 3, ds.Tables[0].Rows[i][1]); // OD 
                                EPP_ExcelManager.SetDecimalValue(ews, 15 + i, 4, ds.Tables[0].Rows[i][2]); // ID
                                EPP_ExcelManager.SetDecimalValue(ews, 15 + i, 5, ds.Tables[0].Rows[i][3]); // Weight
                                EPP_ExcelManager.SetDecimalValue(ews, 15 + i, 7, ds.Tables[0].Rows[i][4]); // Length
                            }

                            ds.Dispose();
                        }
                    }
                    //~~~~~~~~~~~~~~~~
                    {//Casing
                        string query = " select * from fn_Get_DMR_Casing_ByWellID(" + wellID.ToString() + ")";

                        DataSet ds = new DataSet();
                        if (ConnectionManager.ExecQuery(query, ref ds))
                        {
                            int m = Math.Min(8, ds.Tables[0].Rows.Count);

                            for (int i = 0; i < m; i++)
                            {
                                EPP_ExcelManager.SetDecimalValue(ews, 15 + i, 8, ds.Tables[0].Rows[i][0]); // OD 
                                EPP_ExcelManager.SetDecimalValue(ews, 15 + i, 10, ds.Tables[0].Rows[i][1]); // ID
                                EPP_ExcelManager.SetDecimalValue(ews, 15 + i, 11, ds.Tables[0].Rows[i][2]); // Start
                                EPP_ExcelManager.SetDecimalValue(ews, 15 + i, 12, ds.Tables[0].Rows[i][3]); // Finish
                            }

                            ds.Dispose();
                        }
                    }
                    //~~~~~~~~~~~~~~~~
                    {//Deviation Parameter
                        string query = " select * from fn_Get_DMR_DeviationParameter_ByRepID(" + thisRepID.ToString() + ")";

                        DataSet ds = ConnectionManager.ExecQuery(query, 1);
                        if (ds != null)
                        {
                            int m = Math.Min(5, ds.Tables[0].Rows.Count);

                            for (int i = 0; i < m; i++)
                            {
                                EPP_ExcelManager.SetDecimalValue(ews, 25 + i, 8, ds.Tables[0].Rows[i][0]); // KOP 
                                EPP_ExcelManager.SetDecimalValue(ews, 25 + i, 11, ds.Tables[0].Rows[i][1]); // INC
                            }

                            ds.Dispose();
                        }
                    }
                    //~~~~~~~~~~~~~~
                    {//Bit Information
                        string query = " select * from fn_Get_DMR_Hydraulic_ByRepID(" + thisRepID.ToString() + ")";

                        DataSet ds = ConnectionManager.ExecQuery(query, 1);
                        if (ds != null)
                        {
                            EPP_ExcelManager.SetDecimalValue(ews, 19, 26, ds.Tables[0].Rows[0][0]);//SPP	     

                            EPP_ExcelManager.SetDecimalValue(ews, 23, 18, ds.Tables[0].Rows[0][1]);//BitSize	 
                            EPP_ExcelManager.SetDecimalValue(ews, 24, 18, ds.Tables[0].Rows[0][3]);//HoursOnBit	     
                            EPP_ExcelManager.SetDecimalValue(ews, 25, 18, ds.Tables[0].Rows[0][4]);//BitNumber	     
                            EPP_ExcelManager.SetDecimalValue(ews, 26, 18, ds.Tables[0].Rows[0][5]);//BHANumber	     
                            EPP_ExcelManager.SetDecimalValue(ews, 27, 18, ds.Tables[0].Rows[0][6]);//BitRPM	         
                            EPP_ExcelManager.SetDecimalValue(ews, 28, 18, ds.Tables[0].Rows[0][7]);//WOB	         	 

                            // EPP_ExcelManager.SetDecimalValue(ews, , , ds.Tables[0].Rows[0][2]);//Bit Type

                            ds.Dispose();
                        }

                        {//Nozzles
                            query = " select dbo.fn_Get_Nozzles_ByReportId (" + thisRepID.ToString() + ")";
                            ds = new DataSet();

                            if (ConnectionManager.ExecQuery(query, ref ds, 1))
                            {
                                EPP_ExcelManager.SetDecimalValue(ews, 29, 18, ds.Tables[0].Rows[0][0]);//Nozzles	         	 
                                ds.Dispose();
                            }
                        }
                    }
                    //~~~~~~~~~~~~~~
                    try
                    {//Bit Hydraulic
                        EPP_ExcelManager.SetDecimalValue(ews, 23, 22, Convert.ToDecimal(hydComp.out_bh_TFA).ToString("0.###")); // TFS		
                        EPP_ExcelManager.SetDecimalValue(ews, 24, 22, Convert.ToDecimal(hydComp.out_bh_NozVel).ToString("0.###")); //Nozzle Vel	

                        EPP_ExcelManager.SetDecimalValue(ews, 25, 22,
                            Convert.ToDecimal(hydComp.out_bh_BitPressLossSu).ToString("0.###") + "/"
                            + Convert.ToDecimal(hydComp.out_bh_BitPressLossPercent).ToString("0.###")); //Bit Press Loss

                        EPP_ExcelManager.SetDecimalValue(ews, 26, 22, Convert.ToDecimal(hydComp.out_bh_HHP).ToString("0.###")); //HHP	
                        EPP_ExcelManager.SetDecimalValue(ews, 27, 22, Convert.ToDecimal(hydComp.out_bh_HSI).ToString("0.###")); //HIS	
                        EPP_ExcelManager.SetDecimalValue(ews, 28, 22, Convert.ToDecimal(hydComp.out_bh_JIF).ToString("0.###")); //JIF /Area	
                    }
                    catch (Exception)
                    {
                        //...
                    }
                    //~~~~~~~~~~~~~~
                    try
                    {//Hydraulic Analysis
                        EPP_ExcelManager.SetDecimalValue(ews, 23, 27,
                              Convert.ToDecimal(hydComp.out_ha_Np).ToString("0.###") + "/" + Convert.ToDecimal(hydComp.out_ha_Na).ToString("0.###")); //np/na	

                        EPP_ExcelManager.SetDecimalValue(ews, 24, 27,
                              Convert.ToDecimal(hydComp.out_ha_Kp).ToString("0.###") + "/" + Convert.ToDecimal(hydComp.out_ha_Ka).ToString("0.###"));  //Kp/Ka	

                        EPP_ExcelManager.SetDecimalValue(ews, 25, 27, Convert.ToDecimal(hydComp.out_ha_ECDBitDepth).ToString("0.###")); //ECD @ Bit Depth	
                        EPP_ExcelManager.SetDecimalValue(ews, 26, 27, Convert.ToDecimal(hydComp.out_ha_ECDCasingShoe).ToString("0.###"));  //ECD @ Casing Shoe
                    }
                    catch (Exception)
                    {
                        //...
                    }
                    //~~~~~~~~~~~~~~
                    try
                    {//Volumes
                        EPP_ExcelManager.SetDecimalValue(ews, 14, 17, Convert.ToDecimal(tvmComp.out_hvm_StringVol).ToString("0.###")); // String		
                        EPP_ExcelManager.SetDecimalValue(ews, 15, 17, Convert.ToDecimal(tvmComp.out_hvm_AnnVol).ToString("0.###")); // Annulus		
                        EPP_ExcelManager.SetDecimalValue(ews, 16, 17, Convert.ToDecimal(tvmComp.out_hvm_BelVol).ToString("0.###")); // Below Bit 	
                        EPP_ExcelManager.SetDecimalValue(ews, 17, 17, Convert.ToDecimal(tvmComp.out_hvm_TotalVol).ToString("0.###")); // Hole		

                        string totalCir = UnitConverter.Convert("Liquid Volume", "bbl", selUnitVol, tvmComp.out_cd_BblTotalCir);
                        EPP_ExcelManager.SetDecimalValue(ews, 19, 17, Convert.ToDecimal(totalCir).ToString("0.###")); // Total Circ.	

                        EPP_ExcelManager.SetDecimalValue(ews, 21, 17, Convert.ToDecimal(tvmComp.out_hvm_DrillVol).ToString("0.###")); // Drilling 	
                    }
                    catch (Exception)
                    {
                        //...
                    }
                    //~~~~~~~~~~~~~~~~
                    {//Pump Information
                        string query = " select * from fn_Get_DMR_PumpInformation_ByRepID(" + thisRepID.ToString() + ") order by PumpNum ";

                        DataSet ds = new DataSet();
                        if (ConnectionManager.ExecQuery(query, ref ds))
                        {
                            int m = Math.Min(3, ds.Tables[0].Rows.Count);

                            for (int i = 0; i < m; i++)
                            {
                                decimal volStkBbl = Convert.ToDecimal(ds.Tables[0].Rows[i][6]);
                                if (selUnitLinerLenAndDia == "mm")
                                    volStkBbl *= (decimal)Math.Pow(1 / 25.4, 3);

                                decimal stkRate = Convert.ToDecimal(ds.Tables[0].Rows[i][7]);

                                string circRateStr = "???";
                                {
                                    circRateStr = (stkRate * volStkBbl).ToString();
                                    circRateStr = UnitConverter.Convert("Flow Rate", "bbl/min", selUnitFlowRate, circRateStr);
                                }

                                string volStkStr = UnitConverter.Convert("Volume/Stroke", "bbl/stk", selUnitVolPerStroke, volStkBbl.ToString());

                                EPP_ExcelManager.SetText(ews, 15 + i, 18, ds.Tables[0].Rows[i][0].ToString()); // Pump #
                                EPP_ExcelManager.SetText(ews, 15 + i, 19, ds.Tables[0].Rows[i][1].ToString()); // Brand
                                EPP_ExcelManager.SetText(ews, 15 + i, 20, ds.Tables[0].Rows[i][2].ToString()); // Model
                                EPP_ExcelManager.SetText(ews, 15 + i, 21, ds.Tables[0].Rows[i][3]); // Liner (liner diameters)
                                EPP_ExcelManager.SetDecimalValue(ews, 15 + i, 22, ds.Tables[0].Rows[i][4]); // Stroke  (LinerLength)
                                EPP_ExcelManager.SetDecimalValue(ews, 15 + i, 23, ds.Tables[0].Rows[i][5]); // Eff (%)
                                EPP_ExcelManager.SetDecimalValue(ews, 15 + i, 24, Convert.ToDecimal(volStkStr)); // Vol/Stk
                                EPP_ExcelManager.SetDecimalValue(ews, 15 + i, 25, ds.Tables[0].Rows[i][7]); // Stk Rate
                                EPP_ExcelManager.SetDecimalValue(ews, 15 + i, 26, Convert.ToDecimal(circRateStr)); // Circ. Rate
                            }

                            ds.Dispose();
                        }
                    }
                    //~~~~~~~~~~~~~~~~
                    try
                    {//Circulation Data
                        string surToBit = UnitConverter.Convert("Liquid Volume", "bbl", selUnitVol, tvmComp.out_cd_BblToBit);
                        string bottomUp = UnitConverter.Convert("Liquid Volume", "bbl", selUnitVol, tvmComp.out_cd_BblBottomUp);
                        string totalCir = UnitConverter.Convert("Liquid Volume", "bbl", selUnitVol, tvmComp.out_cd_BblTotalCir);

                        EPP_ExcelManager.SetDecimalValue(ews, 19, 20, Convert.ToDecimal(surToBit).ToString("0.###")); // Surface To Bit	
                        EPP_ExcelManager.SetDecimalValue(ews, 20, 20, Convert.ToDecimal(bottomUp).ToString("0.###")); // Bottoms Up
                        EPP_ExcelManager.SetDecimalValue(ews, 21, 20, Convert.ToDecimal(totalCir).ToString("0.###")); // Total Circulation

                        EPP_ExcelManager.SetDecimalValue(ews, 19, 22, Convert.ToDecimal(tvmComp.out_cd_MinsToBit).ToString("0.###")); // Surface To Bit	
                        EPP_ExcelManager.SetDecimalValue(ews, 20, 22, Convert.ToDecimal(tvmComp.out_cd_MinsBottomUp).ToString("0.###")); // Bottoms Up
                        EPP_ExcelManager.SetDecimalValue(ews, 21, 22, Convert.ToDecimal(tvmComp.out_cd_MinsTotalCir).ToString("0.###")); // Total Circulation

                    }
                    catch (Exception)
                    {
                        //...
                    }
                    //~~~~~~~~~~~~~~~~
                    {//Formation Loss Rate
                        string query = " select * from fn_Get_DMR_FormationLossRate_ByRepID(" + thisRepID.ToString() + ")";

                        DataSet ds = ConnectionManager.ExecQuery(query, 1);
                        if (ds != null)
                        {
                            EPP_ExcelManager.SetText(ews, 21, 25, Convert.ToDecimal(ds.Tables[0].Rows[0][0].ToString()).ToString("0.###")); // min 
                            EPP_ExcelManager.SetText(ews, 21, 27, Convert.ToDecimal(ds.Tables[0].Rows[0][1].ToString()).ToString("0.###")); // max

                            ds.Dispose();
                        }
                    }
                    //~~~~~~~~~~~~~~~~
                    List<Int64> mudPropColIDs = new List<Int64>();

                    {
                        string query = " select * from fn_Get_MudPropColumns_ByRepID(" + thisRepID.ToString() + ")";

                        DataSet ds = ConnectionManager.ExecQuery(query);
                        if (ds != null)
                        {
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                mudPropColIDs.Add(Convert.ToInt64(ds.Tables[0].Rows[i][0]));

                            ds.Dispose();
                        }
                    }
                    //~~~~~~
                    try
                    {//MUD PROPERTIES

                        int[] excelColIDs = new int[5] { 5, 7, 8, 9, 11 };//1-based

                        int m = Math.Min(mudPropColIDs.Count, 5);

                        for (int colListID = 0; colListID < m; colListID++)
                        {
                            //~~~~~~~~~~~~~
                            Dictionary<string, decimal> mudProp = FetchTableData.GetMudPropertiesOfOneMudPropColumn(mudPropColIDs[colListID]);
                            if (mudProp.Count == 0)
                                continue;// Error !!!!!
                            //~~~~~~~~~~~~~
                            Helpers.Computation.MudProperties mudComp = new Helpers.Computation.MudProperties();
                            //~~~~~~~~~~~~~~
                            if (
                                    !allSelectedUnitsInProject.TryGetValue("Liquid Volume", out mudComp.in_selUnitVol) ||
                                    !allSelectedUnitsInProject.TryGetValue("Plastic Viscosity", out mudComp.in_selUnitPlasticVis) ||
                                    !allSelectedUnitsInProject.TryGetValue("Yield Point and Gel Strength", out mudComp.in_selUnitYieldPoint) ||
                                    !allSelectedUnitsInProject.TryGetValue("Ionic Mass Concentration", out mudComp.in_selUnitIonic) ||
                                    !allSelectedUnitsInProject.TryGetValue("KCl Concentration", out mudComp.in_selUnitKClCon) ||
                                    !allSelectedUnitsInProject.TryGetValue("NaCl Concentration", out mudComp.in_selUnitNaClCon) ||
                                    !allSelectedUnitsInProject.TryGetValue("Pm, Pf, Mf", out mudComp.in_selUnitPmPfMf) ||
                                    !allSelectedUnitsInProject.TryGetValue("Powder Material Concentration", out mudComp.in_selUnitPowderMatConc)
                                )
                                continue;// Error !!!!!
                            //~~~~~~~~~~~~~~
                            if (
                                !mudProp.TryGetValue("R600", out mudComp.in_R600) ||
                                !mudProp.TryGetValue("R300", out mudComp.in_R300) ||
                                !mudProp.TryGetValue("KCl", out mudComp.in_KClwt) ||
                                !mudProp.TryGetValue("Pf", out mudComp.in_Pf) ||
                                !mudProp.TryGetValue("Mf", out mudComp.in_Mf) ||
                                !mudProp.TryGetValue("Total Chlorides", out mudComp.in_TotalCh) ||
                                !mudProp.TryGetValue("Total Hardness", out mudComp.in_TotalH) ||
                                !mudProp.TryGetValue("Ca++", out mudComp.in_Capp) ||
                                !mudProp.TryGetValue("Alkal Mud (Pm)", out mudComp.in_Pm) ||
                                !mudProp.TryGetValue("Water", out mudComp.in_water)
                                )
                                continue;// Error !!!!!
                                         //~~~~~~~~~~~~~~
                            //if (
                            //    mudComp.in_R600 == MudPropForm.INVALID_VALUE ||
                            //    mudComp.in_R300 == MudPropForm.INVALID_VALUE ||
                            //    mudComp.in_KClwt == MudPropForm.INVALID_VALUE ||
                            //    mudComp.in_Pf == MudPropForm.INVALID_VALUE ||
                            //    mudComp.in_Mf == MudPropForm.INVALID_VALUE ||
                            //    mudComp.in_TotalCh == MudPropForm.INVALID_VALUE ||
                            //    mudComp.in_TotalH == MudPropForm.INVALID_VALUE ||
                            //    mudComp.in_Capp == MudPropForm.INVALID_VALUE ||
                            //    mudComp.in_Pm == MudPropForm.INVALID_VALUE ||
                            //    mudComp.in_water == MudPropForm.INVALID_VALUE
                            //    )
                            //    continue;// Error !!!!!
                            //~~~~~~~~~~~~~~
                            mudComp.Compute();
                            //~~~~~~~~~~~~~~
                            string dfs = "", smp = "", ct = "", crCk = "", gType = "";
                            if (!FetchTableData.Get_AllValues_OfMudPropColumn(mudPropColIDs[colListID], out dfs, out smp, out ct, out crCk, out gType))
                                continue;// Error !!!!!
                            //~~~~~~~~~~~~~~
                            EPP_ExcelManager.SetText(ews, 23 + 8, excelColIDs[colListID], dfs); //  Drilling Fluid System
                            EPP_ExcelManager.SetText(ews, 24 + 8, excelColIDs[colListID], smp); //  Sample From
                            EPP_ExcelManager.SetText(ews, 25 + 8, excelColIDs[colListID], ct); //  Check Time
                            EPP_ExcelManager.SetText(ews, 43 + 8, excelColIDs[colListID], crCk); //  Cracking Cake
                            EPP_ExcelManager.SetText(ews, 62 + 8, excelColIDs[colListID], gType); //  Gas Type
                            //~~~~~~~~~~~~~~
                            decimal v1 = 0;
                            //~~~~~~~~~~~~
                            EPP_ExcelManager.SetText(ews, 26 + 8, 4, SelUnits_CommaSeparated("Depth", userSelUnitsForReport));
                            EPP_ExcelManager.SetDecimalValue(ews, 26 + 8, excelColIDs[colListID],
                                ConvertToManyUnits_SlashSeparated(mudProp, "Depth / TVD", "Depth", allSelectedUnitsInProject, userSelUnitsForReport));//Depth / TVD
                            //~~~~~~~~~~~~
                            EPP_ExcelManager.SetText(ews, 27 + 8, 4, SelUnits_CommaSeparated("Depth", userSelUnitsForReport));
                            EPP_ExcelManager.SetDecimalValue(ews, 27 + 8, excelColIDs[colListID],
                                ConvertToManyUnits_SlashSeparated(mudProp, "Depth / MD", "Depth", allSelectedUnitsInProject, userSelUnitsForReport));//Depth / MD	
                            //~~~~~~~~~~~~
                            EPP_ExcelManager.SetText(ews, 29 + 8, 4, SelUnits_CommaSeparated("Depth", userSelUnitsForReport));

                            if (mudProp.TryGetValue("Bit Depth", out v1) && v1 != MudPropForm.INVALID_VALUE)
                            {
                                EPP_ExcelManager.SetDecimalValue(ews, 29 + 8, excelColIDs[colListID],
                                    ConvertToManyUnits_SlashSeparated(mudProp, "Bit Depth", "Depth", allSelectedUnitsInProject, userSelUnitsForReport));//Bit Depth	
                            }
                            //~~~~~~~~~~~~
                            EPP_ExcelManager.SetText(ews, 30 + 8, 4, SelUnits_CommaSeparated("Temperature", userSelUnitsForReport));
                            EPP_ExcelManager.SetDecimalValue(ews, 30 + 8, excelColIDs[colListID],
                                ConvertToManyUnits_SlashSeparated(mudProp, "Flow line Temp.", "Temperature", allSelectedUnitsInProject, userSelUnitsForReport));//Flow line Temp.  
                            //~~~~~~~~~~~~
                            EPP_ExcelManager.SetText(ews, 31 + 8, 4, SelUnits_CommaSeparated("Temperature", userSelUnitsForReport));
                            EPP_ExcelManager.SetDecimalValue(ews, 31 + 8, excelColIDs[colListID],
                                ConvertToManyUnits_SlashSeparated(mudProp, "Rheology Temp.", "Temperature", allSelectedUnitsInProject, userSelUnitsForReport));//Rheology Temp.
                            //~~~~~~~~~~~~
                            EPP_ExcelManager.SetText(ews, 32 + 8, 4, SelUnits_CommaSeparated("Mud Weight", userSelUnitsForReport));
                            EPP_ExcelManager.SetDecimalValue(ews, 32 + 8, excelColIDs[colListID],
                            ConvertToManyUnits_SlashSeparated(mudProp, "Density", "Mud Weight", allSelectedUnitsInProject, userSelUnitsForReport));//Density		
                            //~~~~~~~~~~~~
                            EPP_ExcelManager.SetText(ews, 33 + 8, 4, SelUnits_CommaSeparated("Funnel Viscosity", userSelUnitsForReport));
                            EPP_ExcelManager.SetDecimalValue(ews, 33 + 8, excelColIDs[colListID],
                                ConvertToManyUnits_SlashSeparated(mudProp, "Funnel Vis.", "Funnel Viscosity", allSelectedUnitsInProject, userSelUnitsForReport));//Funnel Vis.  		
                            //~~~~~~~~~~~~
                            {
                                string strVal = "";
                                //~~~~~~~~~~~~
                                //R600  / R300	
                                if (mudProp.TryGetValue("R600", out v1) && v1 != MudPropForm.INVALID_VALUE)
                                    strVal = v1.ToString("0.###");

                                if (mudProp.TryGetValue("R300", out v1) && v1 != MudPropForm.INVALID_VALUE)
                                    strVal = strVal + "/" + v1.ToString("0.###");

                                EPP_ExcelManager.SetText(ews, 34 + 8, excelColIDs[colListID], strVal);
                                //~~~~~~~~~~~~
                                //R200 / R100		
                                if (mudProp.TryGetValue("R200", out v1) && v1 != MudPropForm.INVALID_VALUE)
                                    strVal = v1.ToString("0.###");

                                if (mudProp.TryGetValue("R100", out v1) && v1 != MudPropForm.INVALID_VALUE)
                                    strVal = strVal + "/" + v1.ToString("0.###");

                                EPP_ExcelManager.SetDecimalValue(ews, 35 + 8, excelColIDs[colListID], strVal);
                                //~~~~~~~~~~~~
                                //R6 / R3	
                                if (mudProp.TryGetValue("R6", out v1) && v1 != MudPropForm.INVALID_VALUE)
                                    strVal = v1.ToString("0.###");

                                if (mudProp.TryGetValue("R3", out v1) && v1 != MudPropForm.INVALID_VALUE)
                                    strVal = strVal + "/" + v1.ToString("0.###");

                                EPP_ExcelManager.SetDecimalValue(ews, 36 + 8, excelColIDs[colListID], strVal);
                            }
                            //~~~~~~~~~~~~
                            //Gel 10''/10'/30'
                            string gel = "";
                            {
                                string g1 = ConvertToManyUnits_SlashSeparated(mudProp, "Gel 10\'\'", "Yield Point and Gel Strength", allSelectedUnitsInProject, userSelUnitsForReport);
                                string g2 = ConvertToManyUnits_SlashSeparated(mudProp, "Gel 10\'", "Yield Point and Gel Strength", allSelectedUnitsInProject, userSelUnitsForReport);
                                string g3 = ConvertToManyUnits_SlashSeparated(mudProp, "Gel 30\'", "Yield Point and Gel Strength", allSelectedUnitsInProject, userSelUnitsForReport);

                                List<string> toUnits = null;

                                if (!userSelUnitsForReport.TryGetValue("Yield Point and Gel Strength", out toUnits))
                                    gel = "";

                                if (toUnits.Count > 1)
                                    gel = "(" + g1 + ")/(" + g2 + ")/(" + g3 + ")";
                                else
                                    gel = g1 + "/" + g2 + "/" + g3;
                            }
                            EPP_ExcelManager.SetText(ews, 37 + 8, 4, SelUnits_CommaSeparated("Yield Point and Gel Strength", userSelUnitsForReport));
                            EPP_ExcelManager.SetDecimalValue(ews, 37 + 8, excelColIDs[colListID], gel);
                            //~~~~~~~~~~~~
                            EPP_ExcelManager.SetText(ews, 38 + 8, 4, SelUnits_CommaSeparated("Plastic Viscosity", userSelUnitsForReport));
                            EPP_ExcelManager.SetText(ews, 38 + 8, excelColIDs[colListID],
                                ConvertToManyUnits_SlashSeparated(mudComp.out_PV, "Plastic Viscosity", allSelectedUnitsInProject, userSelUnitsForReport));//PV 
                            //~~~~~~~~~~~~
                            EPP_ExcelManager.SetText(ews, 39 + 8, 4, SelUnits_CommaSeparated("Yield Point and Gel Strength", userSelUnitsForReport));
                            EPP_ExcelManager.SetText(ews, 39 + 8, excelColIDs[colListID],
                                ConvertToManyUnits_SlashSeparated(mudComp.out_YP, "Yield Point and Gel Strength", allSelectedUnitsInProject, userSelUnitsForReport));//YP             		
                            //~~~~~~~~~~~~
                            EPP_ExcelManager.SetText(ews, 40 + 8, 4, SelUnits_CommaSeparated("API,HPHT Fluid Loss", userSelUnitsForReport));
                            EPP_ExcelManager.SetDecimalValue(ews, 40 + 8, excelColIDs[colListID],
                                ConvertToManyUnits_SlashSeparated(mudProp, "API FL", "API,HPHT Fluid Loss", allSelectedUnitsInProject, userSelUnitsForReport));//API FL  
                            //~~~~~~~~~~~~
                            EPP_ExcelManager.SetText(ews, 41 + 8, 4, SelUnits_CommaSeparated("API,HPHT Fluid Loss", userSelUnitsForReport));
                            EPP_ExcelManager.SetDecimalValue(ews, 41 + 8, excelColIDs[colListID],
                                   ConvertToManyUnits_SlashSeparated(mudProp, "HPHT FL", "API,HPHT Fluid Loss", allSelectedUnitsInProject, userSelUnitsForReport));//HPHT FL	
                            //~~~~~~~~~~~~
                            EPP_ExcelManager.SetText(ews, 42 + 8, 4, SelUnits_CommaSeparated("Filter Cake Thickness", userSelUnitsForReport));
                            EPP_ExcelManager.SetDecimalValue(ews, 42 + 8, excelColIDs[colListID],
                                ConvertToManyUnits_SlashSeparated(mudProp, "Filter Cake Thickness", "Filter Cake Thickness", allSelectedUnitsInProject, userSelUnitsForReport));//Filter Cake Thickness		
                            //~~~~~~~~~~~~
                            EPP_ExcelManager.SetText(ews, 44 + 8, 4, SelUnits_CommaSeparated("Ionic Mass Concentration", userSelUnitsForReport));
                            EPP_ExcelManager.SetDecimalValue(ews, 44 + 8, excelColIDs[colListID],
                                      ConvertToManyUnits_SlashSeparated(mudProp, "Total Chlorides", "Ionic Mass Concentration", allSelectedUnitsInProject, userSelUnitsForReport));//Total Chlorides    		
                            //~~~~~~~~~~~~
                            EPP_ExcelManager.SetText(ews, 45 + 8, 4, SelUnits_CommaSeparated("KCl Concentration", userSelUnitsForReport));
                            EPP_ExcelManager.SetText(ews, 45 + 8, excelColIDs[colListID],
                                ConvertToManyUnits_SlashSeparated(mudComp.out_KCl, "KCl Concentration", allSelectedUnitsInProject, userSelUnitsForReport));//KCl	
                            //~~~~~~~~~~~~
                            EPP_ExcelManager.SetText(ews, 46 + 8, 4, SelUnits_CommaSeparated("NaCl Concentration", userSelUnitsForReport));
                            EPP_ExcelManager.SetText(ews, 46 + 8, excelColIDs[colListID],
                                ConvertToManyUnits_SlashSeparated(mudComp.out_NaCl, "NaCl Concentration", allSelectedUnitsInProject, userSelUnitsForReport));//NaCl		
                            //~~~~~~~~~~~~
                            EPP_ExcelManager.SetText(ews, 47 + 8, 4, SelUnits_CommaSeparated("Ionic Mass Concentration", userSelUnitsForReport));
                            EPP_ExcelManager.SetText(ews, 47 + 8, excelColIDs[colListID],
                                ConvertToManyUnits_SlashSeparated(mudComp.out_KClCh, "Ionic Mass Concentration", allSelectedUnitsInProject, userSelUnitsForReport));//KCl Chloride		
                            //~~~~~~~~~~~~
                            EPP_ExcelManager.SetText(ews, 48 + 8, 4, SelUnits_CommaSeparated("Ionic Mass Concentration", userSelUnitsForReport));
                            EPP_ExcelManager.SetText(ews, 48 + 8, excelColIDs[colListID],
                                ConvertToManyUnits_SlashSeparated(mudComp.out_NaClCh, "Ionic Mass Concentration", allSelectedUnitsInProject, userSelUnitsForReport));//NaCl Chloride		
                            //~~~~~~~~~~~~
                            EPP_ExcelManager.SetText(ews, 49 + 8, 4, SelUnits_CommaSeparated("Ionic Mass Concentration", userSelUnitsForReport));
                            EPP_ExcelManager.SetDecimalValue(ews, 49 + 8, excelColIDs[colListID],
                                  ConvertToManyUnits_SlashSeparated(mudProp, "Total Hardness", "Ionic Mass Concentration", allSelectedUnitsInProject, userSelUnitsForReport));//Total Hardness 		
                            //~~~~~~~~~~~~
                            //Ca++ / Mg++
                            string CaMg = "";
                            {
                                string ca = ConvertToManyUnits_SlashSeparated(mudProp, "Ca++", "Ionic Mass Concentration", allSelectedUnitsInProject, userSelUnitsForReport);
                                string mg = ConvertToManyUnits_SlashSeparated(mudComp.out_Mgpp, "Ionic Mass Concentration", allSelectedUnitsInProject, userSelUnitsForReport);

                                List<string> toUnits = null;

                                if (!userSelUnitsForReport.TryGetValue("Ionic Mass Concentration", out toUnits))
                                    CaMg = "";

                                if (toUnits.Count > 1)
                                    CaMg = "(" + ca + ")/(" + mg + ")";
                                else
                                    CaMg = ca + "/" + mg;
                            }
                            EPP_ExcelManager.SetText(ews, 50 + 8, 4, SelUnits_CommaSeparated("Ionic Mass Concentration", userSelUnitsForReport));
                            EPP_ExcelManager.SetText(ews, 50 + 8, excelColIDs[colListID], CaMg);
                            //~~~~~~~~~~~~
                            EPP_ExcelManager.SetText(ews, 51 + 8, 4, SelUnits_CommaSeparated("Mud MBT", userSelUnitsForReport));
                            EPP_ExcelManager.SetDecimalValue(ews, 51 + 8, excelColIDs[colListID],
                                ConvertToManyUnits_SlashSeparated(mudProp, "Drill Solids CEC", "Mud MBT", allSelectedUnitsInProject, userSelUnitsForReport));//Drilled Solids CEC		
                            //~~~~~~~~~~~~
                            EPP_ExcelManager.SetText(ews, 53 + 8, 4, SelUnits_CommaSeparated("Mud MBT", userSelUnitsForReport));
                            EPP_ExcelManager.SetDecimalValue(ews, 53 + 8, excelColIDs[colListID],
                                ConvertToManyUnits_SlashSeparated(mudProp, "Mud MBT", "Mud MBT", allSelectedUnitsInProject, userSelUnitsForReport));//Mud MBT                 		
                            //~~~~~~~~~~~~
                            if (mudProp.TryGetValue("pH", out v1) && v1 != MudPropForm.INVALID_VALUE)
                                EPP_ExcelManager.SetDecimalValue(ews, 54 + 8, excelColIDs[colListID], v1);//pH		
                            //~~~~~~~~~~~~
                            EPP_ExcelManager.SetText(ews, 55 + 8, 4, SelUnits_CommaSeparated("Pm, Pf, Mf", userSelUnitsForReport));
                            EPP_ExcelManager.SetDecimalValue(ews, 55 + 8, excelColIDs[colListID],
                                ConvertToManyUnits_SlashSeparated(mudProp, "Alkal Mud (Pm)", "Pm, Pf, Mf", allSelectedUnitsInProject, userSelUnitsForReport));//Alkal Mud (Pm)      		
                            //~~~~~~~~~~~~
                            //Pf/Mf		
                            string PfMf = "";
                            {
                                string pf = ConvertToManyUnits_SlashSeparated(mudProp, "Pf", "Pm, Pf, Mf", allSelectedUnitsInProject, userSelUnitsForReport);
                                string mf = ConvertToManyUnits_SlashSeparated(mudProp, "Mf", "Pm, Pf, Mf", allSelectedUnitsInProject, userSelUnitsForReport);

                                List<string> toUnits = null;

                                if (!userSelUnitsForReport.TryGetValue("Pm, Pf, Mf", out toUnits))
                                    PfMf = "";

                                if (toUnits.Count > 1)
                                    PfMf = "(" + pf + ")/(" + mf + ")";
                                else
                                    PfMf = pf + "/" + mf;
                            }
                            EPP_ExcelManager.SetText(ews, 57 + 8, 4, SelUnits_CommaSeparated("Pm, Pf, Mf", userSelUnitsForReport));
                            EPP_ExcelManager.SetDecimalValue(ews, 56 + 8, excelColIDs[colListID], PfMf);
                            //~~~~~~~~~~~~
                            EPP_ExcelManager.SetText(ews, 57 + 8, 4, SelUnits_CommaSeparated("Ionic Mass Concentration", userSelUnitsForReport));
                            EPP_ExcelManager.SetText(ews, 57 + 8, excelColIDs[colListID],
                                ConvertToManyUnits_SlashSeparated(mudComp.out_Bicabonate, "Ionic Mass Concentration", allSelectedUnitsInProject, userSelUnitsForReport));//Bicarbonate (HCO3)		
                            //~~~~~~~~~~~~
                            EPP_ExcelManager.SetText(ews, 58 + 8, 4, SelUnits_CommaSeparated("Ionic Mass Concentration", userSelUnitsForReport));
                            EPP_ExcelManager.SetText(ews, 58 + 8, excelColIDs[colListID],
                                ConvertToManyUnits_SlashSeparated(mudComp.out_Carbonate, "Ionic Mass Concentration", allSelectedUnitsInProject, userSelUnitsForReport));//Carbonate (CO3)		
                            //~~~~~~~~~~~~
                            EPP_ExcelManager.SetText(ews, 59 + 8, 4, SelUnits_CommaSeparated("Ionic Mass Concentration", userSelUnitsForReport));
                            EPP_ExcelManager.SetText(ews, 59 + 8, excelColIDs[colListID],
                                ConvertToManyUnits_SlashSeparated(mudComp.out_Hydroxyl, "Ionic Mass Concentration", allSelectedUnitsInProject, userSelUnitsForReport));//Hydroxyl (OH)		
                            //~~~~~~~~~~~~
                            EPP_ExcelManager.SetText(ews, 60 + 8, 4, SelUnits_CommaSeparated("Powder Material Concentration", userSelUnitsForReport));
                            EPP_ExcelManager.SetText(ews, 60 + 8, excelColIDs[colListID],
                                ConvertToManyUnits_SlashSeparated(mudComp.out_LimeLBL, "Powder Material Concentration", allSelectedUnitsInProject, userSelUnitsForReport));//Lime		
                            //~~~~~~~~~~~~
                            if (mudProp.TryGetValue("Lubricity Factor", out v1) && v1 != MudPropForm.INVALID_VALUE)
                                EPP_ExcelManager.SetDecimalValue(ews, 61 + 8, excelColIDs[colListID], v1);//Lubricity Factor (kf)
                            //~~~~~~~~~~~~
                            if (mudProp.TryGetValue("Gas", out v1) && v1 != MudPropForm.INVALID_VALUE)
                                EPP_ExcelManager.SetDecimalValue(ews, 63 + 8, excelColIDs[colListID], v1);//Gas		
                            //~~~~~~~~~~~~
                            if (mudProp.TryGetValue("Water", out v1) && v1 != MudPropForm.INVALID_VALUE)
                                EPP_ExcelManager.SetDecimalValue(ews, 64 + 8, excelColIDs[colListID], v1);//Water		
                            //~~~~~~~~~~~~
                            if (mudProp.TryGetValue("Oil/Lubricant", out v1) && v1 != MudPropForm.INVALID_VALUE)
                                EPP_ExcelManager.SetDecimalValue(ews, 65 + 8, excelColIDs[colListID], v1);//Oil/Lubricant	
                            //~~~~~~~~~~~~
                            if (mudProp.TryGetValue("Glycol", out v1) && v1 != MudPropForm.INVALID_VALUE)
                                EPP_ExcelManager.SetDecimalValue(ews, 66 + 8, excelColIDs[colListID], v1);//Glycol	
                            //~~~~~~~~~~~~
                            //see 'Solids Analysis' part down there
                            //if (mudPorp.TryGetValue("", out v1) && v1 != MudPropForm.INVALID_VALUE)
                            //    EPP_ExcelManager.SetDecimalValue(ews, 68 + 7, excelColIDs[colListID], v1);//Total Solids	
                            //~~~~~~~~~~~~
                            if (mudProp.TryGetValue("Sand Content", out v1) && v1 != MudPropForm.INVALID_VALUE)
                                EPP_ExcelManager.SetDecimalValue(ews, 68 + 8, excelColIDs[colListID], v1);//Sand Content		
                            //~~~~~~~~~~~~
                        }
                    }
                    catch (Exception)
                    {
                        //...
                    }
                    //~~~~~~~~~~~~~~~~
                    {//Products Used Last 24 hr
                        string query = " select * from fn_Get_DMR_ProductsUsedLast24hr_ByWellIDAndRepID(" + wellID.ToString() + "," + thisRepID.ToString() + ") order by DailyUsed_Pdf ";

                        DataSet ds = new DataSet();
                        if (ConnectionManager.ExecQuery(query, ref ds))
                        {
                            int m = Math.Min(18, ds.Tables[0].Rows.Count);

                            for (int i = 0; i < m; i++)
                            {
                                EPP_ExcelManager.SetText(ews, 24 + 8 + i, 13, ds.Tables[0].Rows[i][0].ToString()); //  Products
                                EPP_ExcelManager.SetText(ews, 24 + 8 + i, 18, ds.Tables[0].Rows[i][1].ToString()); //  Unit Size
                                EPP_ExcelManager.SetText(ews, 24 + 8 + i, 19, Convert.ToDecimal(ds.Tables[0].Rows[i][2]).ToString("0.#")); //  Amount-PDF
                                EPP_ExcelManager.SetText(ews, 24 + 8 + i, 20, Convert.ToDecimal(ds.Tables[0].Rows[i][3]).ToString("0.#")); //  Amount-Other Company
                            }

                            ds.Dispose();
                        }
                    }
                    //~~~~~~~~~~~~~~~~
                    {//Formation Top Data
                        string query = " select * from fn_Get_DMR_FormationTopData_ByWellID(" + wellID.ToString() + ") order by TopMD "; //sql does not guarantee the order of insert ;)

                        DataSet ds = new DataSet();
                        if (ConnectionManager.ExecQuery(query, ref ds))
                        {
                            int m = Math.Min(18, ds.Tables[0].Rows.Count);

                            for (int i = 0; i < m; i++)
                            {
                                EPP_ExcelManager.SetText(ews, 24 + 8 + i, 21, ds.Tables[0].Rows[i][0].ToString()); // Formation  
                                EPP_ExcelManager.SetText(ews, 24 + 8 + i, 23, ds.Tables[0].Rows[i][1].ToString()); // Member
                                EPP_ExcelManager.SetText(ews, 24 + 8 + i, 24, ds.Tables[0].Rows[i][2].ToString()); // Sub mb.
                                EPP_ExcelManager.SetText(ews, 24 + 8 + i, 25, Convert.ToDecimal(ds.Tables[0].Rows[i][3]).ToString("0.#")); // Top TVD
                                EPP_ExcelManager.SetText(ews, 24 + 8 + i, 26, Convert.ToDecimal(ds.Tables[0].Rows[i][4]).ToString("0.#")); // Top MD
                            }

                            ds.Dispose();
                        }
                    }
                    //~~~~~~~~~~~~~~~~
                    try
                    {//Mud Volume Management (BBL) + CMT
                        List<Int64> mudVolManDFSIDs = new List<Int64>();

                        {
                            string query = " select * from fn_Get_MudVolManDFSIDs_ByRepID(" + thisRepID.ToString() + ")";

                            DataSet ds = ConnectionManager.ExecQuery(query);
                            if (ds != null)
                            {
                                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                    mudVolManDFSIDs.Add(Convert.ToInt64(ds.Tables[0].Rows[i][0]));

                                ds.Dispose();
                            }
                        }
                        //~~~~~~~~~
                        int[] excelColIDs = new int[4] { 18, 20, 22, 24 };//1-based
                        int m = Math.Min(mudVolManDFSIDs.Count, 4);
                        //~~~~~~~~~
                        decimal totalRain = 0, totalGain = 0;
                        //~~~~~~~~~
                        for (int colListID = 0; colListID < m; colListID++)
                        {
                            string query = " select * from fn_Get_DMR_MudVolumeManagement_ByWellIDAndMudVolManDFS(" + wellID.ToString() + "," + mudVolManDFSIDs[colListID].ToString() + ")";

                            DataSet ds = ConnectionManager.ExecQuery(query, 1);
                            if (ds != null)
                            {
                                EPP_ExcelManager.SetDecimalValue(ews, 44 + 8, excelColIDs[colListID], ds.Tables[0].Rows[0][0]);//DFS	

                                EPP_ExcelManager.SetDecimalValue(ews, 46 + 8, excelColIDs[colListID], ds.Tables[0].Rows[0][1]);//Received Volume				
                                EPP_ExcelManager.SetDecimalValue(ews, 47 + 8, excelColIDs[colListID], ds.Tables[0].Rows[0][2]);//Start Volume

                                totalGain += Convert.ToDecimal(ds.Tables[0].Rows[0][3]);
                                totalRain += Convert.ToDecimal(ds.Tables[0].Rows[0][4]);
                                EPP_ExcelManager.SetDecimalValue(ews, 48 + 8, excelColIDs[colListID], ds.Tables[0].Rows[0][3] + "/ " + ds.Tables[0].Rows[0][4]);//Gain/ Rain Volume				

                                EPP_ExcelManager.SetDecimalValue(ews, 49 + 8, excelColIDs[colListID], ds.Tables[0].Rows[0][5]);//Build Vol.	:Chemical Vol.		
                                EPP_ExcelManager.SetDecimalValue(ews, 50 + 8, excelColIDs[colListID], ds.Tables[0].Rows[0][6]);//Build Vol.	:Added Water	
                                EPP_ExcelManager.SetDecimalValue(ews, 51 + 8, excelColIDs[colListID], ds.Tables[0].Rows[0][7]);//Build Vol.	:Added Oil		
                                EPP_ExcelManager.SetDecimalValue(ews, 52 + 8, excelColIDs[colListID], ds.Tables[0].Rows[0][8]);//Build Vol.	:Total	

                                EPP_ExcelManager.SetDecimalValue(ews, 53 + 8, excelColIDs[colListID], ds.Tables[0].Rows[0][9]);//Treated Vol.	: Vol For Treat			
                                EPP_ExcelManager.SetDecimalValue(ews, 54 + 8, excelColIDs[colListID], ds.Tables[0].Rows[0][10]);//Treated Vol. :Chemical Vol.	
                                EPP_ExcelManager.SetDecimalValue(ews, 55 + 8, excelColIDs[colListID], ds.Tables[0].Rows[0][11]);//Treated Vol. :Added Water		
                                EPP_ExcelManager.SetDecimalValue(ews, 56 + 8, excelColIDs[colListID], ds.Tables[0].Rows[0][12]);//Treated Vol. :Added Oil		
                                EPP_ExcelManager.SetDecimalValue(ews, 57 + 8, excelColIDs[colListID], ds.Tables[0].Rows[0][13]);//Treated Vol. :Total			
                                EPP_ExcelManager.SetDecimalValue(ews, 58 + 8, excelColIDs[colListID], ds.Tables[0].Rows[0][14]);//Transferred Volume			
                                EPP_ExcelManager.SetDecimalValue(ews, 59 + 8, excelColIDs[colListID], ds.Tables[0].Rows[0][15]);//Using other DFS					
                                EPP_ExcelManager.SetDecimalValue(ews, 60 + 8, excelColIDs[colListID], ds.Tables[0].Rows[0][16]);//Used for Other DFS				
                                EPP_ExcelManager.SetDecimalValue(ews, 61 + 8, excelColIDs[colListID], ds.Tables[0].Rows[0][17]);//Diplaced Vol to Hole				
                                EPP_ExcelManager.SetDecimalValue(ews, 62 + 8, excelColIDs[colListID], ds.Tables[0].Rows[0][18]);//Return Vol from Hole			
                                EPP_ExcelManager.SetDecimalValue(ews, 63 + 8, excelColIDs[colListID], ds.Tables[0].Rows[0][19]);//Left in Pits				
                                EPP_ExcelManager.SetDecimalValue(ews, 64 + 8, excelColIDs[colListID], ds.Tables[0].Rows[0][20]);//Left in Hole   -> after merge  + Left in Pits		

                                ds.Dispose();
                            }
                        }

                        EPP_ExcelManager.SetText(ews, 48, 26, totalGain.ToString("0.###") + "/ " + totalRain.ToString("0.###"));//Total Gain/ Rain Volume

                        //~~~~~~~~~
                        {
                            string query = " select SpacerVol_MVMPitVol, CementSlurryLeadVol_MVMPitVol, CementSlurryTailVol_MVMPitVol from at_Report where  ID = " + thisRepID.ToString();

                            DataSet ds = new DataSet();

                            if (ConnectionManager.ExecQuery(query, ref ds, 1))
                            {
                                EPP_ExcelManager.SetDecimalValue(ews, 67 + 8, 13, ds.Tables[0].Rows[0][0]);//Spacer
                                EPP_ExcelManager.SetDecimalValue(ews, 67 + 8, 19, ds.Tables[0].Rows[0][1]);//Cement Slurry Lead
                                EPP_ExcelManager.SetDecimalValue(ews, 67 + 8, 23, ds.Tables[0].Rows[0][2]);//Cement Slurry Tail

                                ds.Dispose();
                            }
                        }
                    }
                    catch (Exception)
                    {
                        //...
                    }
                    //~~~~~~~~~~~~~~~~
                    try
                    {//Solids Analysis      
                        int[] excelColIDs = new int[4] { 5, 7, 9, 11 };//1-based
                        //~~~~~~~~~~~~~~
                        Helpers.Computation.SolidAnalysis solAComp = new Helpers.Computation.SolidAnalysis();
                        //~~~~~~~~~~~~~~
                        if (
                            !allSelectedUnitsInProject.TryGetValue("Liquid Volume", out solAComp.in_selUnitVol) ||
                            !allSelectedUnitsInProject.TryGetValue("Plastic Viscosity", out solAComp.in_selUnitPlasticVis) ||
                            !allSelectedUnitsInProject.TryGetValue("Yield Point and Gel Strength", out solAComp.in_selUnitYieldPoint) ||
                            !allSelectedUnitsInProject.TryGetValue("Ionic Mass Concentration", out solAComp.in_selUnitIonic) ||
                            !allSelectedUnitsInProject.TryGetValue("KCl Concentration", out solAComp.in_selUnitKClCon) ||
                            !allSelectedUnitsInProject.TryGetValue("NaCl Concentration", out solAComp.in_selUnitNaClCon) ||
                            !allSelectedUnitsInProject.TryGetValue("Pm, Pf, Mf", out solAComp.in_selUnitPmPfMf) ||
                            !allSelectedUnitsInProject.TryGetValue("Powder Material Concentration", out solAComp.in_selUnitPowderMatConc) ||
                            !allSelectedUnitsInProject.TryGetValue("Mud Weight", out solAComp.in_selUnitMW) ||
                            !allSelectedUnitsInProject.TryGetValue("Mud MBT", out solAComp.in_selUnitMBT) ||
                            !allSelectedUnitsInProject.TryGetValue("Powder Material Concentration", out solAComp.in_selUnitPowderMatCon)
                            )
                            return "Error fetching slected units";
                        //~~~~~~~~~~~~~~

                        for (int colListID = 0; colListID < mudPropColIDs.Count; colListID++)
                        {
                            string query = " select FreshMudMBT, BentoniteMBT, BariteDensity, HematitDensity, CalCarbDensity, "
                                         + " BariteConcentration, HematitConcentration, CalCarbConcentration, WA_Barite, WA_Hematit, WA_CalciumCarbonate, Weighted "
                                         + " from rt_Rep2SolidAnaly  "
                                         + " where MudPropColumn_ID =  " + mudPropColIDs[colListID].ToString();

                            DataSet ds = new DataSet();

                            if (ConnectionManager.ExecQuery(query, ref ds, 1))
                            {
                                solAComp.in_FreshMudMBT = ds.Tables[0].Rows[0][0].ToString();
                                solAComp.in_BentCEC = ds.Tables[0].Rows[0][1].ToString();
                                solAComp.in_BarDens = ds.Tables[0].Rows[0][2].ToString();
                                solAComp.in_HemDens = ds.Tables[0].Rows[0][3].ToString();
                                solAComp.in_CalCarDens = ds.Tables[0].Rows[0][4].ToString();
                                solAComp.in_BarConc = ds.Tables[0].Rows[0][5].ToString();
                                solAComp.in_HemConc = ds.Tables[0].Rows[0][6].ToString();
                                solAComp.in_CalCarConc = ds.Tables[0].Rows[0][7].ToString();

                                //~~~~~~~~~~~~~~
                                //do not return on error => show any computed value yet
                                solAComp.Compute(mudPropColIDs[colListID],
                                     Convert.ToBoolean(ds.Tables[0].Rows[0][8]),
                                     Convert.ToBoolean(ds.Tables[0].Rows[0][9]),
                                     Convert.ToBoolean(ds.Tables[0].Rows[0][10]),
                                     Convert.ToBoolean(ds.Tables[0].Rows[0][11])
                                    );
                                //~~~~~~~~~~~~~~
                                ds.Dispose();
                            }

                            EPP_ExcelManager.SetText(ews, 73 + 8, excelColIDs[colListID], Convert.ToDecimal(solAComp.out_DisSolVol).ToString("0.###") + "/ " + Convert.ToDecimal(solAComp.out_DisSol_wt).ToString("0.###")); // Dissolved Solids		
                            EPP_ExcelManager.SetText(ews, 74 + 8, excelColIDs[colListID], Convert.ToDecimal(solAComp.out_KClVol).ToString("0.###") + "/ " + Convert.ToDecimal(solAComp.out_KCL_wt).ToString("0.###")); // KCl		
                            EPP_ExcelManager.SetText(ews, 75 + 8, excelColIDs[colListID], Convert.ToDecimal(solAComp.out_NaClVol).ToString("0.###") + "/ " + Convert.ToDecimal(solAComp.out_NaClwt).ToString("0.###")); // NaCl		
                            EPP_ExcelManager.SetText(ews, 76 + 8, excelColIDs[colListID], Convert.ToDecimal(solAComp.out_CorSolVol).ToString("0.###") + "/ " + Convert.ToDecimal(solAComp.out_CorSol_BBL).ToString("0.###")); // Corrected Solids		
                            EPP_ExcelManager.SetText(ews, 77 + 8, excelColIDs[colListID], Convert.ToDecimal(solAComp.out_WeiMatVol).ToString("0.###") + "/ " + Convert.ToDecimal(solAComp.out_WeiMat_BBL).ToString("0.###")); // Weighting Material		
                            EPP_ExcelManager.SetText(ews, 78 + 8, excelColIDs[colListID], Convert.ToDecimal(solAComp.out_BariteVol).ToString("0.###") + "/ " + Convert.ToDecimal(solAComp.out_Barite_BBL).ToString("0.###")); // Barite		
                            EPP_ExcelManager.SetText(ews, 79 + 8, excelColIDs[colListID], Convert.ToDecimal(solAComp.out_HematitVol).ToString("0.###") + "/ " + Convert.ToDecimal(solAComp.out_Hematit_BBL).ToString("0.###")); // Hematit		
                            EPP_ExcelManager.SetText(ews, 80 + 8, excelColIDs[colListID], Convert.ToDecimal(solAComp.out_CalCarVol).ToString("0.###") + "/ " + Convert.ToDecimal(solAComp.out_CalCar_BBL).ToString("0.###")); // Calcium Carbonate		
                            EPP_ExcelManager.SetText(ews, 81 + 8, excelColIDs[colListID], Convert.ToDecimal(solAComp.out_LGSVol).ToString("0.###") + "/ " + Convert.ToDecimal(solAComp.out_LGS_BBL).ToString("0.###")); // LGS		
                            EPP_ExcelManager.SetText(ews, 82 + 8, excelColIDs[colListID], Convert.ToDecimal(solAComp.out_BentVol).ToString("0.###") + "/ " + Convert.ToDecimal(solAComp.out_Bent_BBL).ToString("0.###")); // Bentonite 		
                            EPP_ExcelManager.SetText(ews, 83 + 8, excelColIDs[colListID], Convert.ToDecimal(solAComp.out_DrillSolVol).ToString("0.###") + "/ " + Convert.ToDecimal(solAComp.out_DrillSol_BBL).ToString("0.###")); // Drilled Solid		


                            EPP_ExcelManager.SetDecimalValue(ews, 68 + 7, excelColIDs[colListID], Convert.ToDecimal(solAComp.out_TotalSolid));//Total Solids
                        }
                    }
                    catch (Exception)
                    {
                        //...
                    }
                    //~~~~~~~~~~~~~~~~
                    {//Surface Volume Information
                        string query = " select * from fn_Get_DMR_SurfaceVolumeInformation_ByRepID(" + thisRepID.ToString() + ")";

                        DataSet ds = new DataSet();
                        if (ConnectionManager.ExecQuery(query, ref ds))
                        {
                            int m = Math.Min(11, ds.Tables[0].Rows.Count);


                            for (int i = 0; i < m; i++)
                            {
                                EPP_ExcelManager.SetText(ews, 71 + 8 + i, 13, ds.Tables[0].Rows[i][0].ToString()); // Pit/Tank
                                EPP_ExcelManager.SetDecimalValue(ews, 71 + 8 + i, 16, ds.Tables[0].Rows[i][1]); // Capacity
                                EPP_ExcelManager.SetText(ews, 71 + 8 + i, 18, ds.Tables[0].Rows[i][2].ToString()); // Status
                                EPP_ExcelManager.SetText(ews, 71 + 8 + i, 19, ds.Tables[0].Rows[i][3].ToString()); // Drilling Fluid System
                                EPP_ExcelManager.SetDecimalValue(ews, 71 + 8 + i, 22, ds.Tables[0].Rows[i][4]); // Volume
                                EPP_ExcelManager.SetDecimalValue(ews, 71 + 8 + i, 23, ds.Tables[0].Rows[i][5]); // MW
                            }

                            ds.Dispose();
                        }
                    }
                    //~~~~~~~~~~~~~~~~
                    {//Loss Breakdown
                        {//SCE

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

                            string query = " select dbo.fn_Get_DMR_LossBreakdown_SCE_ByRepID(" + thisRepID.ToString() + "," + actLossFactor.ToString() + ")";

                            DataSet ds = ConnectionManager.ExecQuery(query, 1);
                            if (ds != null)
                            {
                                EPP_ExcelManager.SetText(ews, 69 + 8, 26, ds.Tables[0].Rows[0][0].ToString());//sum used

                                ds.Dispose();
                            }
                        }
                        //~~~~~~~~~~~~~~~~
                        {//Formation
                            string query = " select dbo.fn_Get_DMR_LossBreakdown_Formation_ByRepID(" + thisRepID.ToString() + ")";

                            DataSet ds = ConnectionManager.ExecQuery(query, 1);
                            if (ds != null)
                            {
                                EPP_ExcelManager.SetText(ews, 70 + 8, 26, ds.Tables[0].Rows[0][0].ToString());

                                ds.Dispose();
                            }
                        }
                        //~~~~~~~~~~~~~~~~
                        {//Waste Pit & Over
                            string query = "  select sum(DailyMudRetWasteVol+AtEndMudRetWasteVol) ,sum(DailyMudRetOverVol+AtEndMudRetOverVol) from rt_Rep2MudVolManDFS where ReportID = " + thisRepID.ToString();
                            DataSet ds = ConnectionManager.ExecQuery(query, 1);

                            if (ds != null)
                            {
                                if (ds.Tables[0].Rows[0][0] != DBNull.Value)
                                {
                                    EPP_ExcelManager.SetText(ews, 71 + 8, 26, ds.Tables[0].Rows[0][0].ToString());
                                    EPP_ExcelManager.SetText(ews, 72 + 8, 26, ds.Tables[0].Rows[0][1].ToString());
                                }

                                ds.Dispose();
                            }
                        }


                        //~~~~~~~~~~~~~~~~
                        {//Loss Record
                            string query = " select * from fn_Get_DMR_LossBreakdown_LossRecord_ByRepID(" + thisRepID.ToString() + ")";

                            DataSet ds = new DataSet();
                            if (ConnectionManager.ExecQuery(query, ref ds))
                            {
                                int m = Math.Min(10, ds.Tables[0].Rows.Count);

                                for (int i = 0; i < m; i++)
                                {
                                    EPP_ExcelManager.SetText(ews, 73 + 8 + i, 24, ds.Tables[0].Rows[i][0].ToString()); // Name
                                    EPP_ExcelManager.SetText(ews, 73 + 8 + i, 26, ds.Tables[0].Rows[i][1].ToString()); // Volume
                                }

                                ds.Dispose();
                            }
                        }
                    }
                    //~~~~~~~~~~~~~~~~
                    {//IADC Hours
                        string query = " select * from fn_Get_DMR_IADCHours_ByRepID(" + thisRepID.ToString() + ")";

                        DataSet ds = new DataSet();
                        if (ConnectionManager.ExecQuery(query, ref ds))
                        {
                            int m = Math.Min(10, ds.Tables[0].Rows.Count);

                            for (int i = 0; i < m; i++)
                            {
                                EPP_ExcelManager.SetText(ews, 82 + 8 + i, 24, ds.Tables[0].Rows[i][0].ToString()); // IADC
                                EPP_ExcelManager.SetText(ews, 82 + 8 + i, 27, ds.Tables[0].Rows[i][1].ToString()); // Time
                            }

                            ds.Dispose();
                        }
                    }
                    //~~~~~~~~~~~~~~~~
                    {//Solids Control Equipment Last 24 hr

                        decimal actLosFactor = 1;
                        if (selUnitDischarge == "bbl/hr")
                        {
                            if (selUnitVol == "m³")
                                actLosFactor = 0.1589873m;//m³ -> bbl
                        }
                        else// m³/hr
                        {
                            if (selUnitVol == "bbl")
                                actLosFactor = 6.2898m;//bbl -> m³ 
                        }

                        {//Upper part
                            string query = " select * from fn_Get_DMR_SolidsControlEquipmentLast24hr_ByRepID(" + thisRepID.ToString() + "," + actLosFactor.ToString() + ")"; //check order !!

                            DataSet ds = new DataSet();
                            if (ConnectionManager.ExecQuery(query, ref ds))
                            {
                                int m = Math.Min(9, ds.Tables[0].Rows.Count);

                                for (int i = 0; i < m; i++)
                                {
                                    EPP_ExcelManager.SetText(ews, 95 + 8 + i, 1, ds.Tables[0].Rows[i][0].ToString()); //  Name
                                    EPP_ExcelManager.SetText(ews, 95 + 8 + i, 4, ds.Tables[0].Rows[i][1].ToString()); //  #
                                    EPP_ExcelManager.SetText(ews, 95 + 8 + i, 5, ds.Tables[0].Rows[i][2].ToString()); //  Brand
                                    EPP_ExcelManager.SetText(ews, 95 + 8 + i, 7, ds.Tables[0].Rows[i][3].ToString()); //  Model
                                    EPP_ExcelManager.SetText(ews, 95 + 8 + i, 9, ds.Tables[0].Rows[i][4].ToString()); //  Specification
                                    EPP_ExcelManager.SetText(ews, 95 + 8 + i, 12, ds.Tables[0].Rows[i][5].ToString()); //  Screen Size	
                                    EPP_ExcelManager.SetText(ews, 95 + 8 + i, 18, ds.Tables[0].Rows[i][6].ToString()); //  Drilling Fluid System
                                    EPP_ExcelManager.SetText(ews, 95 + 8 + i, 20, ds.Tables[0].Rows[i][7].ToString()); //  Used
                                    EPP_ExcelManager.SetText(ews, 95 + 8 + i, 22, ds.Tables[0].Rows[i][8].ToString()); //  Discharge
                                    EPP_ExcelManager.SetText(ews, 95 + 8 + i, 24, ds.Tables[0].Rows[i][9].ToString()); //  Act Vol Lost
                                    EPP_ExcelManager.SetText(ews, 95 + 8 + i, 26, ds.Tables[0].Rows[i][10].ToString()); //  Comment
                                }

                                ds.Dispose();
                            }
                        }

                        {//Total
                            string query = " select * from fn_Get_DMR_SolidsControlEquipmentLast24hr_Total_ByRepID(" + thisRepID.ToString() + "," + actLosFactor.ToString() + ")";

                            DataSet ds = ConnectionManager.ExecQuery(query, 1);
                            if (ds != null)
                            {
                                EPP_ExcelManager.SetText(ews, 104 + 8, 20, ds.Tables[0].Rows[0][0].ToString()); // Used
                                EPP_ExcelManager.SetText(ews, 104 + 8, 22, ds.Tables[0].Rows[0][1].ToString()); // Discharge
                                EPP_ExcelManager.SetText(ews, 104 + 8, 24, ds.Tables[0].Rows[0][2].ToString()); // Act Vol Lost

                                ds.Dispose();
                            }
                        }
                    }
                    //~~~~~~~~~~~~~~~~
                    {//Remarks
                        if (reportIsValid)
                        {
                            string query = " select * from fn_Get_Remarks(" + wellID.ToString() + "," + repNum.ToString() + ")";

                            DataSet ds = ConnectionManager.ExecQuery(query, 1);
                            if (ds != null)
                            {
                                EPP_ExcelManager.SetText(ews, 85 + 8, 1, ds.Tables[0].Rows[0][0].ToString());//Drilling Remarks
                                EPP_ExcelManager.SetText(ews, 85 + 8, 14, ds.Tables[0].Rows[0][1].ToString());// Drilling Fluids Remarks and Treatment 

                                ds.Dispose();
                            }
                        }
                    }
                }
                // ******************* Units, Currency ***********************
                {
                    //header ~~~~~~~~~~~~~~~~
                    EPP_ExcelManager.SetText(ews, 10, 9, "Previous TVD /MD (" + selUnitDepth + ")");//Previous TVD /MD (unit)
                    EPP_ExcelManager.SetText(ews, 11, 9, "Night TVD /MD (" + selUnitDepth + ")");//Night TVD /MD (unit)
                    EPP_ExcelManager.SetText(ews, 9, 20, "Daily Drilling (" + selUnitDepth + ")");//Daily Drilling
                    EPP_ExcelManager.SetText(ews, 10, 20, "Average ROP (" + selUnitRop + ")");//Average ROP
                    EPP_ExcelManager.SetText(ews, 11, 20, "Bit Depth (" + selUnitDepth + ")");//Bit Depth
                    //footer ~~~~~~~~~~~~~~~~
                    EPP_ExcelManager.SetText(ews, 105 + 8, 19, "DAILY MATERIAL COST (" + selCurrency + ")");//DAILY MATERIAL COST
                    EPP_ExcelManager.SetText(ews, 105 + 8, 22, "CUMULATIVE MATERIAL COST  (" + selCurrency + ")");//CUMULATIVE MATERIAL COST 
                    //Drilling Assembly ~~~~~~~~~~~~~~~~
                    EPP_ExcelManager.SetText(ews, 13, 7, "Length (" + selUnitDepth + ")");//Length
                    //Casing ~~~~~~~~~~~~~~~~
                    EPP_ExcelManager.SetText(ews, 13, 11, "Start (" + selUnitDepth + ")");//Start
                    EPP_ExcelManager.SetText(ews, 13, 12, "Finish (" + selUnitDepth + ")");//Finish
                    //Deviation Parameter ~~~~~~~~~~~~~~~~
                    EPP_ExcelManager.SetText(ews, 24, 8, "KOP (" + selUnitDepth + ")");//KOP
                    //Pump Information ~~~~~~~~~~~~~~~~
                    EPP_ExcelManager.SetText(ews, 13, 21, "Liner (" + selUnitLinerLenAndDia + ")");//Liner
                    EPP_ExcelManager.SetText(ews, 13, 22, "Stroke (" + selUnitLinerLenAndDia + ")");//Stroke
                    EPP_ExcelManager.SetText(ews, 13, 24, "Vol/Stk (" + selUnitVolPerStroke + ")");//Vol/Stk
                    EPP_ExcelManager.SetText(ews, 13, 26, "Circ. Rate (" + selUnitFlowRate + ")");//Circ. Rate
                    EPP_ExcelManager.SetText(ews, 18, 24, "Flow Rate (" + selUnitFlowRate + ")");//Circ. Rate
                    EPP_ExcelManager.SetText(ews, 19, 24, "SPP (" + selUnitPressure + ")");//Circ. Rate
                    //Circulation Data ~~~~~~~~~~~~~~~~
                    EPP_ExcelManager.SetText(ews, 19, 21, selUnitVol);
                    EPP_ExcelManager.SetText(ews, 20, 21, selUnitVol);
                    EPP_ExcelManager.SetText(ews, 21, 21, selUnitVol);
                    //Formation Loss Rate ~~~~~~~~~~~~~~~~
                    EPP_ExcelManager.SetText(ews, 20, 24, "Formation Loss Rate (" + selUnitDischarge + ")");//Formation Loss Rate
                    //Surface Volume Information ~~~~~~~~~~~~~~~~
                    EPP_ExcelManager.SetText(ews, 69 + 8, 16, "Capacity (" + selUnitVol + ")");//Capacity
                    EPP_ExcelManager.SetText(ews, 69 + 8, 22, "Volume (" + selUnitVol + ")");//Volume
                    EPP_ExcelManager.SetText(ews, 69 + 8, 23, "MW (" + selUnitMW + ")");//MW
                    //Formation Top Data ~~~~~~~~~~~~~~~~
                    EPP_ExcelManager.SetText(ews, 23 + 8, 25, "Top TVD (" + selUnitDepth + ")");//Top TVD
                    EPP_ExcelManager.SetText(ews, 23 + 8, 26, "Top MD (" + selUnitDepth + ")");//Top MD
                    //Solids Control Equipment Last 24 hr ~~~~~~~~~~~~~~~~
                    string actVolSelUnitVol = selUnitVol;
                    ////     Note: actVolSelUnitVol is related to selUnitDischarge
                    //if (selUnitDischarge == "m³/hr")
                    //    actVolSelUnitVol = "m³";
                    //else if (selUnitDischarge == "bbl/hr")
                    //    actVolSelUnitVol = "bbl";

                    EPP_ExcelManager.SetText(ews, 94 + 8, 22, "(" + selUnitDischarge + ")");//Discharge 
                    EPP_ExcelManager.SetText(ews, 94 + 8, 24, "(" + actVolSelUnitVol + ")");//Act Vol Lost
                    //Volumes ~~~~~~~~~~~~~~~~
                    EPP_ExcelManager.SetText(ews, 13, 14, selUnitVol);//vol
                    //Mud Volume Management ~~~~~~~~~~~~~~~~
                    EPP_ExcelManager.SetText(ews, 42 + 8, 13, "Mud Volume Management (" + selUnitVol + ")");
                    //hydraulic ~~~~~~~~~~~~~~~~
                    EPP_ExcelManager.SetText(ews, 19, 24, "SPP (" + selUnitPressure + ")");

                    EPP_ExcelManager.SetText(ews, 23, 17, selUnitHoleBitSize);//Bit Size
                    EPP_ExcelManager.SetText(ews, 28, 17, selUnitWeightOnBit);//WOB
                    EPP_ExcelManager.SetText(ews, 29, 17, selUnitNozzleSize);//Nozzels
                    EPP_ExcelManager.SetText(ews, 24, 21, selUnitNozzleVelocity);//Nozzels Vel.
                    EPP_ExcelManager.SetText(ews, 25, 21, selUnitPressure + "/%");//Bit Press . Loss
                    //~~~~~~~~~~~~~~~~
                    //~~~~~~~~~~~~~~~~
                }
                // *********************** Footer ***********************
                {
                    //~~~~~~~~~~~~~~~~~~~
                    if (reportIsValid)
                    {
                        string query = " select * from  fn_Reporting_DMR_Footer_ByRepID(" + thisRepID.ToString() + "," + wellID.ToString() + ")";

                        DataSet ds = ConnectionManager.ExecQuery(query, 1);
                        if (ds != null)
                        {
                            EPP_ExcelManager.SetText(ews, 114, 1, ds.Tables[0].Rows[0][0].ToString());//SenMudEng
                            EPP_ExcelManager.SetText(ews, 115, 1, ds.Tables[0].Rows[0][1].ToString());//JunMudEng

                            EPP_ExcelManager.SetText(ews, 114, 5, ds.Tables[0].Rows[0][3].ToString());//SenCmpRep		
                            EPP_ExcelManager.SetText(ews, 115, 5, ds.Tables[0].Rows[0][4].ToString());//JunCmpRep	

                            EPP_ExcelManager.SetText(ews, 114, 9, ds.Tables[0].Rows[0][6].ToString());//EngSup			
                            EPP_ExcelManager.SetText(ews, 115, 9, ds.Tables[0].Rows[0][7].ToString());//OpSup	

                            EPP_ExcelManager.SetText(ews, 114, 14, ds.Tables[0].Rows[0][10].ToString());//WareHouse 
                            EPP_ExcelManager.SetText(ews, 115, 14, ds.Tables[0].Rows[0][11].ToString());//WareHousePhone

                            EPP_ExcelManager.SetText(ews, 115, 19, sumProductInvDailyCost.ToString("0.###"));//DAILY MATERIAL COST 
                            EPP_ExcelManager.SetText(ews, 115, 22, sumProductInvtotalCost.ToString("0.###"));//CUMULATIVE MATERIAL COST

                            //EPP_ExcelManager.SetText(ews, rIndex + , , ds.Tables[0].Rows[0][2].ToString());//ProjEng		    
                            //EPP_ExcelManager.SetText(ews, rIndex + , , ds.Tables[0].Rows[0][5].ToString());//ClientRep		
                            //EPP_ExcelManager.SetText(ews, rIndex + , , ds.Tables[0].Rows[0][8].ToString());//EngSupPhone	    
                            //EPP_ExcelManager.SetText(ews, rIndex + , , ds.Tables[0].Rows[0][9].ToString());//OpSupPhone		
                            //EPP_ExcelManager.SetText(ews, rIndex + , , ds.Tables[0].Rows[0][12].ToString());//EngCostPerDay		
                            //EPP_ExcelManager.SetText(ews, rIndex + , , ds.Tables[0].Rows[0][13].ToString());//EqCostPerDay

                            ds.Dispose();
                        }
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
        public override string GenerateReport()
        {
            try
            {
                if (!CheckParameters())
                    return "Invalid Parameters";
                //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                DMR.Reporting.Report_Generator.frmUnitSelectionForReport frmUnitSel = new Reporting.Report_Generator.frmUnitSelectionForReport();
                frmUnitSel.ShowDialog();

                if (frmUnitSel.userSelUnitsForReport == null)
                    return "Canceled";

                Dictionary<string/*group*/, List<string>/*selected units*/> userSelUnitsForReport = frmUnitSel.userSelUnitsForReport;
                //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                {
                    string query = " select dbo.fn_Get_ReportIdOfLastRev_ByWellIDAndRepNum(" + wellID.ToString() + "," + repNum.ToString() + ")";


                    DataSet ds = ConnectionManager.ExecQuery(query, 1);
                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows[0][0] != DBNull.Value)
                            thisRepID = Convert.ToInt64(ds.Tables[0].Rows[0][0]);
                        ds.Dispose();
                    }

                    if (thisRepID < 0)
                        return "No valid report exists for well and date";

                    reportIsValid = true;
                }
                //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                Stream templateFileName = new MemoryStream(DMR.Reporting.Report_Generator.Daily.DailyReportsXlsx.Daily_DMR);

                ExcelPackage package = new ExcelPackage(templateFileName);
                if (package == null) { return "Excel Package error"; }
                //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
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
                    string query = " select * from fn_Get_ContractorImage_ByWellIDAndRepNum(" + wellID.ToString() + "," + repNum + ")";

                    DataSet ds = ConnectionManager.ExecQuery(query, 1);
                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows[0][0] != DBNull.Value)
                            contractorImage = SqlImageHelper.ByteArrayToImage((byte[])ds.Tables[0].Rows[0][0]);

                        ds.Dispose();
                    }
                }
                //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                Dictionary<string, string> allSelectedUnitsInProject = FetchTableData.GetAllSelectedUnits(frmMain.selectedPrjID);
                //******************************************************
                string ret = "";

                ret = InsertShakerScreensInvInvReport(package.Workbook.Worksheets[3], contractorImage, operatorImage, "ShaleInv", allSelectedUnitsInProject);
                if (ret != "")
                    return ret;

                ret = InsertProductInvReport(package.Workbook.Worksheets[2], contractorImage, operatorImage, "PrdInv", allSelectedUnitsInProject);
                if (ret != "")
                    return ret;

                ret = InsertMainDMRReport(package.Workbook.Worksheets[1], contractorImage, operatorImage, "DMR", allSelectedUnitsInProject, userSelUnitsForReport);
                if (ret != "")
                    return ret;

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
            return StringManipulation.ReplaceBadNameChars("PDF-" + operatorName + "-" + projectName + "-" + wellName + "-DMR " + repNum.ToString("00") + "-" + thisReportDate_Shamsi + ".xlsx");
        }
        //-------------------------------------------------------
    }
}
