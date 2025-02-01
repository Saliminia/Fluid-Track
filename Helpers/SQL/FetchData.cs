using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;

using System.Text;

namespace DMR
{
    public class FetchTableData
    {
        //^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
        public class ProjectProperties
        {
            public string prjType = "";

            public string name = "";
            public string fieldName = "";
            public string country = "";
            public string province = "";
            public string contractNum = "";

            public string currency = "";
            public decimal currencyToRial = 0;
            public decimal senCostPerDay = 0;
            public decimal junCostPerDay = 0;

            public string clientName = "";
            public Image clientImage = null;
            public string operatorName = "";
            public Image operatorImage = null;
        }
        //^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
        public class WellProperties
        {
            public string name = "";
            public DateTime spudDate = DateTime.Now;
            public DateTime compDate = DateTime.Now;
            public string calssification = "";
            public string type = "";
            public string objective = "";
        }
        //^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^




        //-------------------------------------------------------
        //returns: true:success  ,   false:error
        public static bool GetPitNameAndNumber(Int64 PitID, out string name, out int pitNum)
        {
            name = "";
            pitNum = 0;

            try
            {
                string query =
                      " select p.Value, rt_Rig2PitTank.PitNum from rt_Rig2PitTank join lt_PredefValues p on PitName_PredefAutoID = p.AutoID "
                    + " where rt_Rig2PitTank.ID = " + PitID.ToString();

                DataSet ds = new DataSet();
                if (ConnectionManager.ExecQuery(query, ref ds, 1))
                {
                    if (ds.Tables[0].Rows[0][0] != DBNull.Value)
                        name = ds.Tables[0].Rows[0][0].ToString();

                    pitNum = Convert.ToInt32(ds.Tables[0].Rows[0][1]);

                    ds.Dispose();
                    return true;
                }
            }
            catch (Exception)
            {
            }

            return false;
        }
        //-------------------------------------------------------
        //returns: true:success  ,   false:error
        public static bool GetProjectEngCostPerDay(int prjID, out decimal sen, out decimal jun)
        {
            sen = 0;
            jun = 0;

            try
            {
                string query =
                     " select SeniorEngCostPerDay, JuniorEngCostPerDay  from at_Project where AutoID = " + prjID.ToString();

                DataSet ds = new DataSet();
                if (ConnectionManager.ExecQuery(query, ref ds, 1))
                {
                    sen = Convert.ToDecimal(ds.Tables[0].Rows[0][0]);
                    jun = Convert.ToDecimal(ds.Tables[0].Rows[0][1]);
                    ds.Dispose();
                    return true;
                }
            }
            catch (Exception)
            {
            }

            return false;
        }
        //-------------------------------------------------------
        //returns: true:success  ,   false:error
        public static bool GetWellProperties(int wellID, ref WellProperties properties)
        {
            try
            {
                string query =
                            " select w.Name, w.SpudDate, w.CompDate, aff.Formation," +
                            " isnull(pc.Value, \'\'), isnull(pt.Value, \'\') " +
                            " from at_Well w left join lt_GeologyOfIran_AreaFieldFormation aff on w.Objective_GeoAutoID = aff.AutoID " +
                            " left join lt_PredefValues pc on w.Class_PredefAutoID = pc.AutoID " +
                            " left join lt_PredefValues pt on w.Type_PredefAutoID = pt.AutoID " +
                            " where ID = " + wellID.ToString();

                DataSet ds = new DataSet();
                if (ConnectionManager.ExecQuery(query, ref ds, 1))
                {
                    properties.name = ds.Tables[0].Rows[0][0].ToString();
                    properties.spudDate = Convert.ToDateTime(ds.Tables[0].Rows[0][1]);
                    properties.compDate = Convert.ToDateTime(ds.Tables[0].Rows[0][2]);
                    properties.objective = ds.Tables[0].Rows[0][3].ToString();
                    properties.calssification = ds.Tables[0].Rows[0][4].ToString();
                    properties.type = ds.Tables[0].Rows[0][5].ToString();

                    ds.Dispose();
                    return true;
                }
            }
            catch (Exception)
            {
            }

            return false;
        }
        //-------------------------------------------------------
        //returns: true:success  ,   false:error
        public static bool GetProjectProperties(int prjID, ref ProjectProperties properties)
        {
            try
            {
                string query =
                            " select Type, Name, lt_GeologyOfIran_AreaField.FieldName, Country, Province, " +
                            " ContractNumber, isnull(p.Value, \'\'), CurrencyToRial, " +
                            " SeniorEngCostPerDay, JuniorEngCostPerDay, " +
                            " ClientName, ClientImage, " +
                            " OperatorName, OperatorImage " +
                            " from at_Project left join lt_GeologyOfIran_AreaField on at_Project.Area_Field_Geo_AutoID = lt_GeologyOfIran_AreaField.AutoID " +
                            " left join lt_PredefValues p on Currency_PredefAutoID = p.AutoID " +
                            " where at_Project.AutoID = " + prjID.ToString();

                DataSet ds = new DataSet();
                if (ConnectionManager.ExecQuery(query, ref ds, 1))
                {
                    properties.prjType = ds.Tables[0].Rows[0][0].ToString();
                    properties.name = ds.Tables[0].Rows[0][1].ToString();

                    properties.fieldName = ds.Tables[0].Rows[0][2].ToString();
                    properties.country = ds.Tables[0].Rows[0][3].ToString();
                    properties.province = ds.Tables[0].Rows[0][4].ToString();
                    properties.contractNum = ds.Tables[0].Rows[0][5].ToString();
                    properties.currency = ds.Tables[0].Rows[0][6].ToString();

                    properties.currencyToRial = Convert.ToDecimal(ds.Tables[0].Rows[0][7]);
                    properties.senCostPerDay = Convert.ToDecimal(ds.Tables[0].Rows[0][8]);
                    properties.junCostPerDay = Convert.ToDecimal(ds.Tables[0].Rows[0][9]);

                    properties.clientName = ds.Tables[0].Rows[0][10].ToString();

                    if (ds.Tables[0].Rows[0][11] != DBNull.Value)
                        properties.clientImage = SqlImageHelper.ByteArrayToImage((byte[])ds.Tables[0].Rows[0][11]);


                    properties.operatorName = ds.Tables[0].Rows[0][12].ToString();

                    if (ds.Tables[0].Rows[0][13] != DBNull.Value)
                        properties.operatorImage = SqlImageHelper.ByteArrayToImage((byte[])ds.Tables[0].Rows[0][13]);

                    ds.Dispose();
                    return true;
                }
            }
            catch (Exception)
            {
            }

            return false;
        }
        //-------------------------------------------------------
        //returns: true:success  ,   false:error
        public static bool GetAreaField(int geoID, out string area, out string field)
        {
            area = "";
            field = "";

            try
            {
                string query =
                     " select ga.AreaName, gaf.FieldName "
                     + " from lt_GeologyOfIran_AreaField gaf join  lt_GeologyOfIran_Area ga on gaf.Area_AutoID = ga.AutoID  "
                     + " where gaf.AutoID = " + geoID.ToString();

                DataSet ds = new DataSet();
                if (ConnectionManager.ExecQuery(query, ref ds, 1))
                {
                    area = ds.Tables[0].Rows[0][0].ToString();
                    field = ds.Tables[0].Rows[0][1].ToString();
                    ds.Dispose();
                    return true;
                }
            }
            catch (Exception)
            {
            }

            return false;
        }
        //-------------------------------------------------------
        //returns: true:success  ,   false:error
        public static bool GetAreaFieldFormation(int geoID, out string area, out string field, out string formation)
        {
            area = "";
            field = "";
            formation = "";
            try
            {
                string query =
                     " select ga.AreaName, gaf.FieldName, gaff.Formation   "
                     + " from lt_GeologyOfIran_AreaFieldFormation gaff join lt_GeologyOfIran_AreaField gaf on gaff.AreaFieldID = gaf.AutoID "
                     + " join  lt_GeologyOfIran_Area ga on gaf.Area_AutoID = ga.AutoID  "
                     + " where gaff.AutoID = " + geoID.ToString();

                DataSet ds = new DataSet();
                if (ConnectionManager.ExecQuery(query, ref ds, 1))
                {
                    area = ds.Tables[0].Rows[0][0].ToString();
                    field = ds.Tables[0].Rows[0][1].ToString();
                    formation = ds.Tables[0].Rows[0][2].ToString();
                    ds.Dispose();
                    return true;
                }
            }
            catch (Exception)
            {
            }

            return false;
        }
        //-------------------------------------------------------
        //returns: true:success  ,   false:error
        public static bool GetRecTransVol(string recTransIDStr, out decimal vol)
        {
            vol = 0;

            try
            {
                string query = " SELECT Vol FROM  rt_Rep2MudVolManDFS_RecTrans where ID = " + recTransIDStr;

                DataSet ds = new DataSet();
                if (ConnectionManager.ExecQuery(query, ref ds, 1))
                {
                    vol = Convert.ToDecimal(ds.Tables[0].Rows[0][0].ToString());
                    ds.Dispose();
                    return true;
                }
            }
            catch (Exception)
            {
            }

            return false;
        }
        //-------------------------------------------------------
        //returns: true:success  ,   false:error
        public static bool GetHoleSizeAndLabel(int hID, out string HoleDiameter, out string hLabel)
        {
            HoleDiameter = "";
            hLabel = "";

            try
            {
                string query = " SELECT HoleDiameter, HoleLabel  FROM  lt_HoleSizes " +
                                " where AutoID = " + hID.ToString();

                DataSet ds = new DataSet();
                if (ConnectionManager.ExecQuery(query, ref ds, 1))
                {
                    HoleDiameter = ds.Tables[0].Rows[0][0].ToString();
                    hLabel = ds.Tables[0].Rows[0][1].ToString();
                    ds.Dispose();
                    return true;
                }
            }
            catch (Exception)
            {
            }

            return false;
        }
        //-------------------------------------------------------
        //returns: true:success  ,   false:error
        public static bool GetHoleID(Int64 repID, out string hIDStr)
        {
            hIDStr = "";

            try
            {
                string query = " SELECT HoleSize_AutoID FROM  at_Report where ID = " + repID.ToString();

                DataSet ds = new DataSet();
                if (ConnectionManager.ExecQuery(query, ref ds, 1))
                {
                    hIDStr = ds.Tables[0].Rows[0][0].ToString();
                    ds.Dispose();
                    return true;
                }
            }
            catch (Exception)
            {
            }

            return false;
        }
        //-------------------------------------------------------
        //returns: true:success  ,   false:error
        public static bool GetMudType(string Dfs_AutoIDStr, out string MudType, out string DFS)
        {
            MudType = "";
            DFS = "";

            try
            {
                string query = " SELECT MudType , DrillingFluidSystem FROM  lt_DrillingFluidSystem where  AutoID = " + Dfs_AutoIDStr;

                DataSet ds = new DataSet();
                if (ConnectionManager.ExecQuery(query, ref ds, 1))
                {
                    MudType = ds.Tables[0].Rows[0][0].ToString();
                    DFS = ds.Tables[0].Rows[0][1].ToString();
                    ds.Dispose();
                    return true;
                }
            }
            catch (Exception)
            {
            }

            return false;
        }
        //-------------------------------------------------------
        //returns: true:success  ,   false:error
        public static bool GetDfsAutoID(string Dfs_Name, out string Dfs_AutoIDStr)
        {
            Dfs_Name = Dfs_Name.Replace("\'", "\'\'");

            Dfs_AutoIDStr = "";

            try
            {
                string query = " SELECT AutoID FROM  lt_DrillingFluidSystem where  DrillingFluidSystem = \'" + Dfs_Name + "\'";

                DataSet ds = new DataSet();
                if (ConnectionManager.ExecQuery(query, ref ds, 1))
                {
                    Dfs_AutoIDStr = ds.Tables[0].Rows[0][0].ToString();
                    ds.Dispose();
                    return true;
                }
            }
            catch (Exception)
            {
            }

            return false;
        }
        //-------------------------------------------------------
        //returns: true:success  ,   false:error
        public static bool GetReportNum(string reportID, out string localReportID)
        {
            localReportID = "";

            try
            {
                string query = " SELECT Num  FROM  at_Report " +
                                " where ID = " + reportID;

                DataSet ds = new DataSet();
                if (ConnectionManager.ExecQuery(query, ref ds, 1))
                {
                    localReportID = ds.Tables[0].Rows[0][0].ToString();
                    ds.Dispose();
                    return true;
                }
            }
            catch (Exception)
            {
            }

            return false;
        }
        //-------------------------------------------------------
        //returns: true:success  ,   false:error
        public static bool GetRevisionID(string reportID, out string Rev)
        {
            Rev = "";

            try
            {
                string query = " SELECT Rev  FROM  at_Report " +
                                " where ID = " + reportID;

                DataSet ds = new DataSet();
                if (ConnectionManager.ExecQuery(query, ref ds, 1))
                {
                    Rev = Convert.ToInt32(ds.Tables[0].Rows[0][0]).ToString("00");
                    ds.Dispose();
                    return true;
                }
            }
            catch (Exception)
            {
            }

            return false;
        }
        //-------------------------------------------------------
        //returns: true:success  ,   false:error
        public static bool GetRevisionCount(string reportLocalID, Int64 rigwellID, out int count)
        {
            count = -1;

            try
            {
                string query =
                     " select count(*) from at_Report "
                    + " where RigWellID =  " + rigwellID.ToString()
                    + " and Num = " + reportLocalID;

                DataSet ds = new DataSet();
                if (ConnectionManager.ExecQuery(query, ref ds, 1))
                {
                    count = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
                    ds.Dispose();
                    return true;
                }
            }
            catch (Exception)
            {
            }

            return false;
        }

        //-------------------------------------------------------
        //returns: true:success  ,   false:error
        public static bool GetPersonnelData(int autoID, out int code, out string name, out string phone)
        {
            code = 0;
            name = "";
            phone = "";

            try
            {
                string query = " select Code, Name, PhoneNumber from lt_Personnel " +
                                " where AutoID = " + autoID.ToString();

                DataSet ds = new DataSet();
                if (ConnectionManager.ExecQuery(query, ref ds, 1))
                {
                    code = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
                    name = ds.Tables[0].Rows[0][1].ToString();
                    phone = ds.Tables[0].Rows[0][2].ToString();
                    ds.Dispose();
                    return true;
                }
            }
            catch (Exception)
            {
            }

            return false;
        }
        //-------------------------------------------------------
        public static bool GetReportIDOfLastRevOfPrevReport/*according to local id of report*/(int repNumber, Int64 wellID, out string reportID)
        {
            reportID = "";

            try
            {
                string query =
                  " select top(1) r.ID "
                + " from at_Report r join rt_Rig2Well rw on r.RigWellID = rw.ID "
                + " where rw.WellID = " + wellID.ToString() //same well
                + " and r.Num < " + repNumber.ToString()
                + " order by r.Num desc, r.Rev desc ";

                DataSet ds = new DataSet();
                if (ConnectionManager.ExecQuery(query, ref ds, 1))
                {
                    reportID = ds.Tables[0].Rows[0][0].ToString();
                    ds.Dispose();
                    return true;
                }
            }
            catch (Exception)
            {
            }

            return false;
        }
        //-------------------------------------------------------
        public static object[] GetFieldsOfLastRevOfPrevReport(int repNumber, Int64 wellID, string fields, bool showError = true)
        {
            object[] objs = null;

            try
            {
                string prevReportID;

                if (!GetReportIDOfLastRevOfPrevReport(repNumber, wellID, out prevReportID))
                {
                    if (showError)
                        InformationManager.Set_Info("There is no previous report");
                    return null;
                }

                string query = " SELECT " + fields + " FROM at_Report WHERE ID = " + prevReportID;

                DataSet ds = ConnectionManager.ExecQuery(query, 1);

                if (ds != null)
                {
                    objs = ds.Tables[0].Rows[0].ItemArray;
                    ds.Dispose();
                    return objs;
                }
            }
            catch (Exception)
            {
            }

            return objs;
        }
        //-------------------------------------------------------
        public static bool GetReportIDOfLastRev(Int64 wellID, int repNumber, out string reportID)
        {
            reportID = "";

            try
            {
                string query =
                  " select top(1) r.ID "
                + " from at_Report r join rt_Rig2Well rw on r.RigWellID = rw.ID "
                + " where rw.WellID = " + wellID
                + " and r.Num = " + repNumber
                + " order by r.Num desc, r.Rev desc ";

                DataSet ds = new DataSet();
                if (ConnectionManager.ExecQuery(query, ref ds, 1))
                {
                    reportID = ds.Tables[0].Rows[0][0].ToString();
                    ds.Dispose();
                    return true;
                }
            }
            catch (Exception)
            {
            }

            return false;
        }
        //-------------------------------------------------------
        public static object[] GetFieldsOfLastRevOfReport(Int64 wellID, int repNumber, string fields)
        {
            object[] objs = null;

            try
            {
                string reportID;

                if (!GetReportIDOfLastRev(wellID, repNumber, out reportID))
                {
                    InformationManager.Set_Info("There is no such report");
                    return null;
                }

                string query = " SELECT " + fields + " FROM at_Report WHERE ID = " + reportID;

                DataSet ds = ConnectionManager.ExecQuery(query, 1);

                if (ds != null)
                {
                    objs = ds.Tables[0].Rows[0].ItemArray;
                    ds.Dispose();
                    return objs;
                }
            }
            catch (Exception)
            {
            }

            return objs;
        }
        //-------------------------------------------------------
        public static object[] GetFieldsOfReport(Int64 repID, string fields)
        {
            object[] objs = null;

            try
            {
                string query = " SELECT " + fields + " FROM at_Report WHERE ID = " + repID;

                DataSet ds = ConnectionManager.ExecQuery(query, 1);

                if (ds != null)
                {
                    objs = ds.Tables[0].Rows[0].ItemArray;
                    ds.Dispose();
                    return objs;
                }
            }
            catch (Exception)
            {
            }

            return objs;
        }
        //-------------------------------------------------------
        public static Dictionary<string, decimal> GetMudPropertiesOfOneMudPropColumn(Int64 mudPropColID)
        {
            Dictionary<string, decimal> result = null;

            try
            {
                string query = "select MudPropName, Value from rt_Rep2MudProp_MudPropRow "
                            + " where Column_ID = " + mudPropColID.ToString();

                DataSet ds = ConnectionManager.ExecQuery(query);
                if (ds != null)
                {
                    result = new Dictionary<string, decimal>();

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        result.Add(ds.Tables[0].Rows[i][0].ToString(), Convert.ToDecimal(ds.Tables[0].Rows[i][1])); ;
                    ds.Dispose();

                    return result;
                }
            }
            catch (Exception)
            {
            }

            return null;
        }
        //-------------------------------------------------------
        //returns: true:success  ,   false:error
        public static bool GetMudPropertiesAutoID(Int64 repID, string timePeriod, out Int64 autoID)
        {
            autoID = -1;
            timePeriod = timePeriod.Replace("\'", "\'\'");

            try
            {
                string query =
                    " select  ID from rt_Rep2MudPropPeriod where  "
                    + " TimesPeriod =  \'" + timePeriod + "\'"
                    + " and ReportID = " + repID.ToString();

                DataSet ds = new DataSet();
                if (ConnectionManager.ExecQuery(query, ref ds, 1))
                {
                    autoID = Convert.ToInt64(ds.Tables[0].Rows[0][0]);
                    ds.Dispose();
                    return true;
                }
            }
            catch (Exception)
            {
            }

            return false;
        }
        //-------------------------------------------------------
        //returns: true:success  ,   false:error
        public static bool GetProjectType(int prjAutoID, out string type)
        {
            type = "";

            try
            {
                string query = " select Type from at_Project where AutoID = " + prjAutoID.ToString();

                DataSet ds = new DataSet();
                if (ConnectionManager.ExecQuery(query, ref ds, 1))
                {
                    type = ds.Tables[0].Rows[0][0].ToString();
                    ds.Dispose();
                    return true;
                }
            }
            catch (Exception)
            {
            }

            return false;
        }
        //-------------------------------------------------------
        //returns: true:success  ,   false:error
        public static bool GetProjectType(string prjIDStr, out string type)
        {
            type = "";

            try
            {
                string query = " select Type from at_Project where AutoID = " + prjIDStr;

                DataSet ds = new DataSet();
                if (ConnectionManager.ExecQuery(query, ref ds, 1))
                {
                    type = ds.Tables[0].Rows[0][0].ToString();
                    ds.Dispose();
                    return true;
                }
            }
            catch (Exception)
            {
            }

            return false;
        }
        //-------------------------------------------------------
        //returns: true:success  ,   false:error
        public static bool GetProjectCurrency(int prjAutoID, out string currency)
        {
            currency = "";

            try
            {
                string query = " select p.Value from at_Project join lt_PredefValues p on Currency_PredefAutoID = p.AutoID " +
                               " where at_Project.AutoID = " + prjAutoID.ToString();

                DataSet ds = new DataSet();
                if (ConnectionManager.ExecQuery(query, ref ds, 1))
                {
                    currency = ds.Tables[0].Rows[0][0].ToString();
                    ds.Dispose();
                    return true;
                }
            }
            catch (Exception)
            {
            }

            return false;
        }
        //-------------------------------------------------------
        public static List<string> GetPredefinedValues(string groupName)
        {
            groupName = groupName.Replace("\'", "\'\'");

            List<string> values = new List<string>();

            try
            {
                string query = " select value from lt_PredefValues where GroupName = \'" + groupName + "\'";

                DataSet ds = new DataSet();
                if (ConnectionManager.ExecQuery(query, ref ds))
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        values.Add(ds.Tables[0].Rows[i][0].ToString());

                    ds.Dispose();
                    return values;
                }
            }
            catch (Exception)
            {
            }

            return null;
        }
        //-------------------------------------------------------
        public static string GetPredefinedValue(int autoID)
        {
            string val = "";

            try
            {
                string query = " select value from lt_PredefValues where AutoID = " + autoID.ToString();


                DataSet ds = new DataSet();
                if (ConnectionManager.ExecQuery(query, ref ds, 1))
                {
                    val = ds.Tables[0].Rows[0][0].ToString();

                    ds.Dispose();
                    return val;
                }
            }
            catch (Exception)
            {
            }

            return "";
        }
        //-------------------------------------------------------
        //returns: true:success  ,   false:error
        public static bool GetSolidControlEqScreenSizes(string r2SC_ID, out List<string> screenSizes)
        {
            screenSizes = new List<string>();

            try
            {
                string query =
                    " select p.Value "
                    + " from rt_Rep2SolidControlScreen rss join rt_Rig2ShaleShaker ss  on rss.ShaleShaker_ID = ss.ID "
                    + " left join lt_PredefValues p on ss.ScreenSize_PredefAutoID = p.AutoID "
                    + " where R2SC_ID = " + r2SC_ID
                    + " order by rss.UserOrderInGroup ";

                DataSet ds = new DataSet();
                if (ConnectionManager.ExecQuery(query, ref ds))
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        screenSizes.Add(ds.Tables[0].Rows[i][0].ToString());

                    ds.Dispose();
                    return true;
                }
            }
            catch (Exception)
            {
            }

            return false;
        }
        //-------------------------------------------------------
        public static bool GetSolidControlEqSumUsedDischargedLost(string r2SC_ID, string selUnitDischarge, string selUnitVol, out string usedDischargedLost)
        {
            usedDischargedLost = "???";

            try
            {
                string query = " select sum(Used),sum(Discharge),sum(Used*Discharge)   from rt_Rep2SolidControlUsed where R2SC_ID = " + r2SC_ID;

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

                DataSet ds = new DataSet();
                if (ConnectionManager.ExecQuery(query, ref ds, 1))
                {
                    usedDischargedLost = Convert.ToDecimal(ds.Tables[0].Rows[0][0]).ToString("0.###")   //used
                        //+ "/" + Convert.ToDecimal(ds.Tables[0].Rows[0][1]).ToString("0.###")   //discharged
                                        + "/" + (Convert.ToDecimal(ds.Tables[0].Rows[0][2]) * actLosFactor).ToString("0.###");   //lost

                    ds.Dispose();
                    return true;
                }
            }
            catch (Exception)
            {
            }

            return false;
        }
        //-------------------------------------------------------
        public static bool GetIadcTightSpots(string drlOpIADC_ID, out List<decimal> tightSpots)
        {
            tightSpots = new List<decimal>();

            try
            {
                string query = " select TightSpot   from rt_Rep2DrlOpIADC_TightSpot where DrlOpIADC_ID = " + drlOpIADC_ID;

                DataSet ds = new DataSet();
                if (ConnectionManager.ExecQuery(query, ref ds))
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        tightSpots.Add(Convert.ToDecimal(ds.Tables[0].Rows[i][0]));

                    ds.Dispose();
                    return true;
                }
            }
            catch (Exception)
            {
            }

            return false;
        }
        //-------------------------------------------------------
        public static bool GetIadcWashAndReams(string drlOpIADC_ID, out List<string[]> washAndReams)
        {
            washAndReams = new List<string[]>();

            try
            {
                string query =
                    " select WashStartDepth, WashEndDepth, p.Value "
                    + " from rt_Rep2DrlOpIADC_WashAndReam "
                    + " left join lt_PredefValues p on WashType_PredefAutoID = p.AutoID "
                    + " where DrlOpIADC_ID  = " + drlOpIADC_ID.ToString()
                    + " order by WashStartDepth, WashEndDepth ";

                DataSet ds = new DataSet();
                if (ConnectionManager.ExecQuery(query, ref ds))
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        washAndReams.Add(new string[] { ds.Tables[0].Rows[i][0].ToString(), ds.Tables[0].Rows[i][1].ToString(), ds.Tables[0].Rows[i][2].ToString() });

                    ds.Dispose();
                    return true;
                }
            }
            catch (Exception)
            {
            }

            return false;
        }
        //-------------------------------------------------------
        public static bool GetHydraulicNozzles(Int64 repID, out List<int> nozzleCount, out List<decimal> nozzleSize)
        {
            nozzleCount = new List<int>();
            nozzleSize = new List<decimal>();

            try
            {
                string query =
                    " select NozzleCount, NozzleSize "
                    + " FROM rt_Rep2HydraulicNozzles "
                    + " WHERE ReportID = " + repID.ToString()
                    + " order by NozzleSize  ";


                DataSet ds = new DataSet();
                if (ConnectionManager.ExecQuery(query, ref ds))
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        nozzleCount.Add(Convert.ToInt32(ds.Tables[0].Rows[i][0]));
                        nozzleSize.Add(Convert.ToDecimal(ds.Tables[0].Rows[i][1]));
                    }
                    ds.Dispose();
                    return true;
                }
            }
            catch (Exception)
            {
            }

            return false;
        }
        //-------------------------------------------------------
        public static string GetSelectedUnitForMudProperty(int prjAutoID, string mudProperty)
        {
            try
            {
                mudProperty = mudProperty.Replace("\'", "\'\'");

                string query =
                     " select p2p.selectedUnit "
                    + " from ft_MudProperties mp join ft_Property p on mp.BaseProp = p.PropertyName "
                    + " join rt_Prj2Prop p2p on p.PropertyName = p2p.PropName "
                    + " where mp.MudPropName = \'" + mudProperty + "\' "
                    + " and p2p.PrjAutoID = " + prjAutoID;

                DataSet ds = new DataSet();
                if (ConnectionManager.ExecQuery(query, ref ds, 1))
                {
                    string unit = ds.Tables[0].Rows[0][0].ToString();
                    ds.Dispose();
                    return unit;
                }
                else
                    return "";
            }
            catch (Exception)
            {
                return "";
            }
        }
        //-------------------------------------------------------
        public static Dictionary<string/*prop*/, string/*unit*/> GetAllSelectedUnits(int prjID)
        {
            try
            {
                Dictionary<string/*prop*/, string/*unit*/> result = new Dictionary<string, string>();

                string query = " select PropName, SelectedUnit from rt_Prj2Prop where PrjAutoID = " + prjID.ToString();

                DataSet ds = ConnectionManager.ExecQuery(query);
                if (ds != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        result.Add(ds.Tables[0].Rows[i][0].ToString(), ds.Tables[0].Rows[i][1].ToString());

                    ds.Dispose();
                    return result;
                }
            }
            catch (Exception e)
            {
            }

            return null;
        }
        //-------------------------------------------------------
        public static bool GetPropertiesOfLocation(string locationID, out int prjID, out Int64 rigID, out Int64 wellID, out int holeID)
        {
            prjID = -1;
            rigID = -1;
            wellID = -1;
            holeID = -1;

            try
            {
                string query = " select PrjAutoID, RigID, WellID, HoleSize_AutoID from at_HolesOfProjects where ID = " + locationID;

                DataSet ds = new DataSet();
                if (ConnectionManager.ExecQuery(query, ref ds, 1))
                {
                    prjID = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
                    rigID = Convert.ToInt64(ds.Tables[0].Rows[0][1]);
                    wellID = Convert.ToInt64(ds.Tables[0].Rows[0][2]);
                    holeID = Convert.ToInt32(ds.Tables[0].Rows[0][3]);

                    ds.Dispose();
                    return true;
                }
                else
                    return false;
            }
            catch (Exception)
            {
            }
            return false;
        }
        //-------------------------------------------------------
        public static bool GetWellIDOfReport(Int64 repID, out Int64 wellID)
        {
            wellID = -1;

            try
            {
                string query = " select rw.WellID from at_Report rp join rt_Rig2Well rw on rp.RigWellID = rw.ID "
                             + " where rp.ID = " + repID.ToString();

                DataSet ds = new DataSet();
                if (ConnectionManager.ExecQuery(query, ref ds, 1))
                {
                    wellID = Convert.ToInt64(ds.Tables[0].Rows[0][0]);
                    ds.Dispose();
                    return true;
                }
                else
                    return false;
            }
            catch (Exception)
            {
            }
            return false;
        }
        //-------------------------------------------------------
        public static bool GetRwFromRwID(Int64 rwID, out Int64 wellID, out Int64 rigID, out string wellName, out string rigName)
        {
            wellID = rigID = -1;
            wellName = rigName = "";

            try
            {
                string query = " select rw.WellID, rw.RigID, w.Name, r.Name "
                + " from rt_Rig2Well rw join at_Rig r on rw.RigID = r.ID "
                + " join at_Well w on rw.WellID = w.ID "
                + " where rw.ID = " + rwID.ToString();

                DataSet ds = new DataSet();
                if (ConnectionManager.ExecQuery(query, ref ds, 1))
                {
                    wellID = Convert.ToInt64(ds.Tables[0].Rows[0][0]);
                    rigID = Convert.ToInt64(ds.Tables[0].Rows[0][1]);

                    wellName = ds.Tables[0].Rows[0][2].ToString();
                    rigName = ds.Tables[0].Rows[0][3].ToString();

                    ds.Dispose();
                    return true;
                }
                else
                    return false;
            }
            catch (Exception)
            {
            }
            return false;
        }
        //-------------------------------------------------------
        public static List<string> GetDFPRigNames(int prjID)
        {
            List<string> names = new List<string>();

            try
            {
                string query = "select RigName from at_DFP_Rig where PrjAutoID = " + prjID.ToString();

                DataSet ds = ConnectionManager.ExecQuery(query);

                if (ds != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        names.Add(ds.Tables[0].Rows[i][0].ToString());

                    ds.Dispose();
                    return names;
                }
            }
            catch (Exception)
            {
            }

            return null;
        }
        //-------------------------------------------------------
        public static List<string> GetDFPWellNames(int prjID)
        {
            List<string> names = new List<string>();

            try
            {
                string query =
                      " select WellName from at_DFP_Well join at_DFP_Rig "
                    + " on at_DFP_Well.dfpRigID = at_DFP_Rig.dfpRigID "
                    + " where PrjAutoID =  " + prjID.ToString();

                DataSet ds = ConnectionManager.ExecQuery(query);

                if (ds != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        names.Add(ds.Tables[0].Rows[i][0].ToString());

                    ds.Dispose();
                    return names;
                }
            }
            catch (Exception)
            {
            }

            return null;
        }
        //-------------------------------------------------------
        public static bool Get_AllValues_OfMudPropColumn(Int64 mudPropColID, out string dfs,
                                                         out string sampFrom, out string checkTime,
                                                         out string crackingCake, out string gasType)
        {
            dfs = ""; sampFrom = ""; checkTime = "";
            crackingCake = ""; gasType = "";

            try
            {
                string query = " select d.DrillingFluidSystem, samplePit_ID, CheckTime, CrackingCake, GasType " +
                               " from rt_Rep2MudProp_MudPropCol left join lt_DrillingFluidSystem d on Dfs_AutoID = d.AutoID " +
                                " where ID = " + mudPropColID.ToString();

                DataSet ds = new DataSet();
                if (ConnectionManager.ExecQuery(query, ref ds, 1))
                {
                    if (ds.Tables[0].Rows[0][0] != DBNull.Value)
                    {
                        dfs = ds.Tables[0].Rows[0][0].ToString();
                    }

                    checkTime = ds.Tables[0].Rows[0][2].ToString();
                    crackingCake = ds.Tables[0].Rows[0][3].ToString();
                    gasType = ds.Tables[0].Rows[0][4].ToString();
                    //~~~~~~~~~
                    if (ds.Tables[0].Rows[0][1] == DBNull.Value)
                    {
                        sampFrom = "Flow Line";
                    }
                    else
                    {
                        Int64 sampPitID = Convert.ToInt64(ds.Tables[0].Rows[0][1]);

                        int sampPitNum = 0;
                        string pitName;
                        FetchTableData.GetPitNameAndNumber(sampPitID, out pitName, out sampPitNum);

                        sampFrom = pitName + " #" + sampPitNum.ToString();
                    }
                    //~~~~~~~~~
                    ds.Dispose();
                    return true;
                }
            }
            catch (Exception)
            {
            }

            return false;
        }
        //-------------------------------------------------------        
    }
}
