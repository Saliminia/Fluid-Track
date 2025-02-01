using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DMR.Helpers.Computation
{
    class Hydraulic
    {
        //=================================================
        public string in_selUnitPressure;
        public string in_selUnitHoleBitSize;
        public string in_selUnitWeightOnBit;
        public string in_selUnitNozzleSize;
        public string in_selUnitNozzleVelocity;

        public string in_selUnitFlowRate;
        public string in_selUnitVolPerStroke;
        public string in_selUnitLinerLenAndDia;
        public string in_selUnitMW;
        public string in_selUnitDepth;
        ////=================================================
        public string out_bh_TFA;
        public string out_bh_NozVel;
        public string out_bh_BitPressLossSu;
        public string out_bh_BitPressLossPercent;
        public string out_bh_HHP;
        public string out_bh_HSI;
        public string out_bh_JIF;

        public string out_ha_Na;
        public string out_ha_Np;
        public string out_ha_Ka;
        public string out_ha_Kp;
        public string out_ha_ECDBitDepth;
        public string out_ha_ECDCasingShoe;
        //=================================================
        class D1D2Len
        {
            public decimal d1 = 0/*string OD*/, d2 = 0/*casing/line ID*/, len = 0;
            //---------------
            public D1D2Len(decimal d1, decimal len, decimal d2)
            {
                this.d1 = d1;
                this.d2 = d2;
                this.len = len;
            }
        };

        //returns true on success, else false
        public bool Compute(Int64 repID, Int64 wellID)
        {
            try
            {
                //~~~~~~~~~~~~~~
                out_bh_TFA = "!!!";
                out_bh_NozVel = "!!!";
                out_bh_BitPressLossSu = "!!!";
                out_bh_BitPressLossPercent = "!!!";
                out_bh_HHP = "!!!";
                out_bh_HSI = "!!!";
                out_bh_JIF = "!!!";

                out_ha_Na = "!!!";
                out_ha_Np = "!!!";
                out_ha_Ka = "!!!";
                out_ha_Ka = "!!!";
                out_ha_ECDBitDepth = "!!!";
                out_ha_ECDCasingShoe = "!!!";
                //~~~~~~~~~~~~~~
                decimal sumJp2 = 0;
                {
                    List<int> nozzleCount;
                    List<decimal> nozzleSize;
                    FetchTableData.GetHydraulicNozzles(frmMain.selectedRepID, out nozzleCount, out nozzleSize);
                    //~~~~~~~~~~~~~~~~~~~~
                    for (int i = 0; i < nozzleSize.Count; i++)
                    {
                        decimal noz1_32 = Convert.ToDecimal(UnitConverter.Convert("Nozzle Size", in_selUnitNozzleSize, "1/32 in", nozzleSize[i].ToString()));
                        sumJp2 += nozzleCount[i] * noz1_32 * noz1_32;
                    }
                }
                decimal tfa_In2 = sumJp2 / 1303.8m;
                out_bh_TFA = tfa_In2.ToString();
                //~~~~~~~~~~~~~~~~~~~~
                decimal Q_GalMin = 0;
                {
                    string query =
                                  " select dbo.fn_Get_VolPerStkBbl(pm.ID), isnull(rmp.StkRate, 0) "
                                + " from rt_Rig2MudPump pm "
                                + " left join (select * from rt_Rep2MudPump where ReportID = " + frmMain.selectedRepID.ToString() + " ) rmp "
                                + " on rmp.PumpID = pm.ID "
                                + " where pm.RigID =  " + frmMain.selected_RW_RigID.ToString();

                    DataSet ds = ConnectionManager.ExecQuery(query);

                    if (ds != null)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            decimal volStkBbl = Convert.ToDecimal(ds.Tables[0].Rows[i][0]);
                            decimal stkRate = Convert.ToDecimal(ds.Tables[0].Rows[i][1]);

                            if (in_selUnitLinerLenAndDia == "mm")
                                volStkBbl *= (decimal)Math.Pow(1 / 25.4, 3);

                            decimal circRateBBlMin = stkRate * volStkBbl;
                            Q_GalMin += Convert.ToDecimal(UnitConverter.Convert("Flow Rate", "bbl/min", "gal/min", circRateBBlMin.ToString()));
                        }

                        ds.Dispose();
                    }
                }
                //~~~~~~~~~~~~~~~~~~~~
                decimal nozzleVel_FtPerSec = 417.2m * Q_GalMin / sumJp2;
                out_bh_NozVel = UnitConverter.Convert("Nozzle Velocity", "ft/sec", in_selUnitNozzleVelocity, nozzleVel_FtPerSec.ToString());
                //~~~~~~~~~~~~~~~~~~~~
                Int64 mudPropcolID = -1;

                {
                    object[] fields = FetchTableData.GetFieldsOfReport(repID, " MudPropColumnID_Hydraulic ");

                    if (fields == null || fields.Length != 1)
                        return false;

                    mudPropcolID = Convert.ToInt64(fields[0]);
                }
                //~~~~~~~~~~~~~~~~~~~~
                Dictionary<string, decimal> mudProp = FetchTableData.GetMudPropertiesOfOneMudPropColumn(mudPropcolID);
                decimal R3 = 0, R300 = 0, R600 = 0, MW_PPG = 0;

                if (
                    mudProp == null || mudProp.Count == 0 ||
                    !mudProp.TryGetValue("R600", out R600) || !mudProp.TryGetValue("R300", out R300) ||
                    !mudProp.TryGetValue("R3", out R3) || !mudProp.TryGetValue("Density", out MW_PPG))
                {
                    return false;
                }

                if (MW_PPG != MudPropForm.INVALID_VALUE && !AdvancedConvertor.ToDecimal(UnitConverter.Convert("Mud Weight", in_selUnitMW, "ppg", MW_PPG.ToString()), ref MW_PPG))
                    return false;

                //~~~~~~~~~~~~~~~~~~~~
                decimal bitPressLossPsi = 0;
                if (MW_PPG == MudPropForm.INVALID_VALUE)
                    bitPressLossPsi = MudPropForm.INVALID_VALUE;
                else
                    bitPressLossPsi = 156.5m * (Q_GalMin * Q_GalMin) * MW_PPG / (sumJp2 * sumJp2);

                if (bitPressLossPsi == MudPropForm.INVALID_VALUE)
                    out_bh_BitPressLossSu = "!!!";
                else
                    out_bh_BitPressLossSu = UnitConverter.Convert("Pressure", "psi", in_selUnitPressure, bitPressLossPsi.ToString());
                //~~~~~~~~~~~~~~~~~~~~
                decimal sPP = 0;
                decimal bitSize = 0;
                string bitType = "";
                decimal hoursOnBit = 0;
                decimal bitNumber = 0;
                decimal bHANumber = 0;
                decimal bitRPM = 0;
                decimal wOB = 0;

                {
                    string fieldNames =
                       "  SPP_Hydraulic,  BitSize_Hydraulic, BitType_Hydraulic, "
                       + "  HoursOnBit_Hydraulic, BitNumber_Hydraulic,  BHANumber_Hydraulic, "
                       + "  BitRPM_Hydraulic, WOB_Hydraulic ";

                    object[] fields = FetchTableData.GetFieldsOfReport(repID, fieldNames);

                    if (fields == null || fields.Length != 8)
                        return false;

                    sPP = Convert.ToDecimal(fields[0]);
                    bitSize = Convert.ToDecimal(fields[1]);
                    bitType = fields[2].ToString();
                    hoursOnBit = Convert.ToDecimal(fields[3]);
                    bitNumber = Convert.ToDecimal(fields[4]);
                    bHANumber = Convert.ToDecimal(fields[5]);
                    bitRPM = Convert.ToDecimal(fields[6]);
                    wOB = Convert.ToDecimal(fields[7]);
                }
                //~~~~~~~~~~~~~
                decimal bitSize_In = Convert.ToDecimal(UnitConverter.Convert("Hole,Bit Size", in_selUnitHoleBitSize, "in", bitSize.ToString()));
                //~~~~~~~~~~~~~
                if (bitPressLossPsi == MudPropForm.INVALID_VALUE)
                {
                    out_bh_BitPressLossPercent = "!!!";
                    out_bh_HHP = "!!!";
                    out_bh_HSI = "!!!";
                }
                else
                {
                    decimal sppPsi = Convert.ToDecimal(UnitConverter.Convert("Pressure", in_selUnitPressure, "psi", sPP.ToString()));
                    decimal bitPressLossPercent = bitPressLossPsi * 100 / sppPsi;
                    out_bh_BitPressLossPercent = bitPressLossPercent.ToString();
                    //~~~~~~~~~~~~~
                    decimal HPP_HP = Q_GalMin * bitPressLossPsi / 1714;
                    out_bh_HHP = HPP_HP.ToString();
                    //~~~~~~~~~~~~~
                    decimal HSI_HpIn2 = HPP_HP * 1.27m / (bitSize_In * bitSize_In);
                    out_bh_HSI = HSI_HpIn2.ToString();
                }

                //~~~~~~~~~~~~~
                if (MW_PPG == MudPropForm.INVALID_VALUE)
                    out_bh_JIF = "!!!";
                else
                {
                    decimal jif = nozzleVel_FtPerSec * Q_GalMin * MW_PPG * 1.27m / 1930 / (bitSize_In * bitSize_In);
                    out_bh_JIF = jif.ToString();
                }
                //~~~~~~~~~~~~~
                decimal na = 0;

                if (R3 == MudPropForm.INVALID_VALUE || R300 == MudPropForm.INVALID_VALUE)
                    na = MudPropForm.INVALID_VALUE;
                else
                    na = 0.5m * (decimal)Math.Log10((double)(R300 / R3));

                out_ha_Na = (na == MudPropForm.INVALID_VALUE) ? "!!!" : na.ToString();
                //~~~~~~~~~~~~~
                decimal np = 0;
                if (R600 == MudPropForm.INVALID_VALUE || R300 == MudPropForm.INVALID_VALUE)
                    np = MudPropForm.INVALID_VALUE;
                else
                    np = 3.32m * (decimal)Math.Log10((double)(R600 / R300));

                out_ha_Np = (np == MudPropForm.INVALID_VALUE) ? "!!!" : np.ToString();
                //~~~~~~~~~~~~~
                decimal ka = 0;
                if (R300 == MudPropForm.INVALID_VALUE)
                    ka = MudPropForm.INVALID_VALUE;
                else
                    ka = 5.11m * R300 / (decimal)(Math.Pow(511, (double)na));

                out_ha_Ka = (ka == MudPropForm.INVALID_VALUE) ? "!!!" : ka.ToString();
                //~~~~~~~~~~~~~
                decimal kp = 0;
                if (R600 == MudPropForm.INVALID_VALUE)
                    kp = MudPropForm.INVALID_VALUE;
                else
                    kp = 5.11m * R600 / (decimal)(Math.Pow(1022, (double)np));

                out_ha_Kp = (kp == MudPropForm.INVALID_VALUE) ? "!!!" : kp.ToString();
                //~~~~~~~~~~~~~
                decimal bitTVDFt = 0;

                string repNumStr = "-1";
                if (!FetchTableData.GetReportNum(repID.ToString(), out repNumStr))
                    return false;

                string queryCasing =
                      " select wc.CasingGroup, c.IDia, wc.CasingTop, wc.CasingBottom, wc.CasingBottomTVD "
                    + " from rt_Well2Casing wc join lt_CasingData c on wc.CasingAutoID = c.AutoID "
                    + " where wc.WellID = " + wellID.ToString()
                    + " and wc.RepNum <= " + repNumStr   //upto current report number
                    + " order by wc.CasingBottom  ";

                DataSet dsCasing = ConnectionManager.ExecQuery(queryCasing);
                //~~~~~~~~~~~~~~
                string queryDrillString =
                      " select o, l "
                    + " from "
                    + " ( "
                    + " 	select d.ODia as o, dh.Length as l, dh.ShowOrder as s"
                    + " 	from rt_Rep2DrlOpHole dh  join lt_Drill_PCH d on dh.DrillPCH_AutoID = d.AutoID "
                    + " 	where dh.ReportID = " + repID.ToString()

                    + " 	union all "  // including duplicates

                    + " 	select ODia as o, Length as l, ShowOrder as s"
                    + " 	from rt_Rep2DrlOpHole_User "
                    + " 	where ReportID = " + repID.ToString()
                    + " ) as DrillStrings "
                    + " order by s desc "; //drill strings are entered from bottom up

                DataSet dsDrillString = ConnectionManager.ExecQuery(queryDrillString);
                //~~~~~~~~~~~~~~
                string queryReport =
                      " select NightMD_DrlOp_HG, PrevMD_DrlOp_HG, HoleSize_AutoID, BitTVD_DrlOp_HG  from at_Report where ID = " + repID.ToString();

                DataSet dsReport = ConnectionManager.ExecQuery(queryReport, 1);
                //~~~~~~~~~~~~~~
                if (dsCasing != null && dsDrillString != null && dsReport != null && dsReport.Tables[0].Rows[0][2] != DBNull.Value)
                {
                    //-----------
                    //decimal nightMdFt = EasyConvert.Depth_ToFoot(in_selUnitDepth, dsReport.Tables[0].Rows[0][0].ToString());
                    //decimal prevMdFt = EasyConvert.Depth_ToFoot(in_selUnitDepth, dsReport.Tables[0].Rows[0][1].ToString());
                    decimal holeSize = 0;

                    string hDia;
                    string hLabel;
                    if (FetchTableData.GetHoleSizeAndLabel(Convert.ToInt32(dsReport.Tables[0].Rows[0][2]), out hDia, out hLabel))
                    {
                        AdvancedConvertor.ToDecimal(hDia, ref holeSize);
                    }

                    bitTVDFt = EasyConvert.Depth_ToFoot(in_selUnitDepth, dsReport.Tables[0].Rows[0][3].ToString());
                    //-----------
                    if (na == MudPropForm.INVALID_VALUE || ka == MudPropForm.INVALID_VALUE || MW_PPG == MudPropForm.INVALID_VALUE)
                    {

                    }
                    else
                    {
                        if (!ComputeEDCBitDepth(dsCasing, dsDrillString, holeSize, bitTVDFt, Q_GalMin, na, ka, MW_PPG))
                            out_ha_ECDBitDepth = "No Casing";

                        if (!ComputeEDCCasingShoe(dsCasing, dsDrillString, holeSize, bitTVDFt, Q_GalMin, na, ka, MW_PPG))
                            out_ha_ECDCasingShoe = "No Casing";
                    }
                    //-----------
                    dsCasing.Dispose();
                    dsDrillString.Dispose();
                    dsReport.Dispose();
                }
                //#########################################################################################
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
        //-------------------------------------------------------
        bool ComputeEDCCasingShoe(DataSet dsCasing, DataSet dsDrillString, decimal holeSize, decimal bitTVDFt, decimal Q_GalMin, decimal na, decimal ka, decimal MW_PPG)
        {
            try
            {
                decimal paT = -666666666666;
                decimal casingBottomTVDFt = 0;//last casing

                List<D1D2Len> casingLinerList = new List<D1D2Len>();//D1 is useless, len is used for Tops

                if (dsCasing.Tables[0].Rows.Count == 0)
                {
                    out_ha_ECDCasingShoe = "No Casing and Liner";
                    return true;
                }
                else
                {
                    int lastCasingIndex = -1;

                    for (int i = dsCasing.Tables[0].Rows.Count - 1; i >= 0; i--)
                    {
                        if (dsCasing.Tables[0].Rows[i][0].ToString() == "Casing")
                        {
                            lastCasingIndex = i;
                            break;
                        }
                    }

                    casingBottomTVDFt = EasyConvert.Depth_ToFoot(in_selUnitDepth, dsCasing.Tables[0].Rows[dsCasing.Tables[0].Rows.Count - 1][4].ToString());


                    if (lastCasingIndex == -1)
                    {
                        out_ha_ECDCasingShoe = "No Casing";
                        return true;
                    }
                    else
                    {
                        decimal lastCasingDia = Convert.ToDecimal(dsCasing.Tables[0].Rows[lastCasingIndex][1]);
                        casingLinerList.Add(new D1D2Len(0, 0, lastCasingDia));

                        for (int i = lastCasingIndex + 1; i < dsCasing.Tables[0].Rows.Count; i++)//liners
                        {
                            decimal topFt = EasyConvert.Depth_ToFoot(in_selUnitDepth, dsCasing.Tables[0].Rows[i][2].ToString());
                            decimal dia = Convert.ToDecimal(dsCasing.Tables[0].Rows[i][1]);
                            casingLinerList.Add(new D1D2Len(0, topFt, dia));
                        }

                        if (dsCasing.Tables[0].Rows.Count - (lastCasingIndex + 1) > 0)//last liner
                        {
                            decimal diaLast = Convert.ToDecimal(dsCasing.Tables[0].Rows[dsCasing.Tables[0].Rows.Count - 1][1]);
                            decimal bottomFt = EasyConvert.Depth_ToFoot(in_selUnitDepth, dsCasing.Tables[0].Rows[dsCasing.Tables[0].Rows.Count - 1][4].ToString());
                            casingLinerList.Add(new D1D2Len(0, bottomFt, diaLast));
                        }
                        else//only casing
                        {
                            casingLinerList.Add(new D1D2Len(0, casingBottomTVDFt, lastCasingDia));
                        }
                    }
                }
                //-----------
                if (casingLinerList.Count >= 2)
                {
                    List<D1D2Len> casingLinerStringsList = new List<D1D2Len>();

                    if (dsDrillString.Tables[0].Rows.Count == 0)
                    {
                        for (int casingIndicator = 0; casingIndicator < casingLinerList.Count - 1; casingIndicator++)
                        {
                            decimal casingLenFt = casingLinerList[casingIndicator + 1].len - casingLinerList[casingIndicator].len;
                            decimal casingIdial = casingLinerList[casingIndicator].d2;
                            casingLinerStringsList.Add(new D1D2Len(0, casingLenFt, casingIdial));
                        }
                    }
                    else
                    {
                        decimal stringLenFt_Reminder = EasyConvert.Depth_ToFoot(in_selUnitDepth, dsDrillString.Tables[0].Rows[0][1].ToString());
                        // decimal stringIteratedDepthFt = 0;

                        decimal casingLenFt_Reminder = casingLinerList[1].len - casingLinerList[0].len;
                        // decimal casingIteratedDepthFt = 0;

                        int casingIndicator = 0;
                        int stringIndicator = 0;

                        decimal stringOdia = 0;
                        decimal casingIdial = 0;
                        decimal lenFt = 0;

                        while (casingIndicator < casingLinerList.Count - 1 && stringIndicator < dsDrillString.Tables[0].Rows.Count)
                        {
                            stringOdia = Convert.ToDecimal(dsDrillString.Tables[0].Rows[stringIndicator][0]);
                            casingIdial = casingLinerList[casingIndicator].d2;
                            lenFt = 0;

                            if (stringLenFt_Reminder < casingLenFt_Reminder)
                            {
                                lenFt = stringLenFt_Reminder;
                                casingLinerStringsList.Add(new D1D2Len(stringOdia, lenFt, casingIdial));

                                casingLenFt_Reminder -= lenFt;

                                stringIndicator++;
                                if (stringIndicator < dsDrillString.Tables[0].Rows.Count)
                                    stringLenFt_Reminder = EasyConvert.Depth_ToFoot(in_selUnitDepth, dsDrillString.Tables[0].Rows[stringIndicator][1].ToString());
                            }
                            else if (stringLenFt_Reminder > casingLenFt_Reminder)
                            {
                                lenFt = casingLenFt_Reminder;
                                casingLinerStringsList.Add(new D1D2Len(stringOdia, lenFt, casingIdial));

                                stringLenFt_Reminder -= lenFt;

                                casingIndicator++;
                                if (casingIndicator < casingLinerList.Count - 1)
                                    casingLenFt_Reminder = casingLinerList[casingIndicator + 1].len - casingLinerList[casingIndicator].len;
                            }
                            else
                            {
                                lenFt = stringLenFt_Reminder;
                                casingLinerStringsList.Add(new D1D2Len(stringOdia, lenFt, casingIdial));

                                stringIndicator++;
                                if (stringIndicator < dsDrillString.Tables[0].Rows.Count)
                                    stringLenFt_Reminder = EasyConvert.Depth_ToFoot(in_selUnitDepth, dsDrillString.Tables[0].Rows[stringIndicator][1].ToString());

                                casingIndicator++;
                                if (casingIndicator < casingLinerList.Count - 1)
                                    casingLenFt_Reminder = casingLinerList[casingIndicator + 1].len - casingLinerList[casingIndicator].len;
                            }
                        }
                        //~~~~~~~~~~~~~~~~~~~~~

                        if (casingIndicator < casingLinerList.Count - 1)
                        {
                            casingLinerStringsList.Add(new D1D2Len(0, casingLenFt_Reminder, casingIdial));
                            casingIndicator++;

                            while (casingIndicator < casingLinerList.Count - 1)
                            {
                                casingLenFt_Reminder = casingLinerList[casingIndicator + 1].len - casingLinerList[casingIndicator].len;
                                casingIdial = casingLinerList[casingIndicator].d2;
                                casingLinerStringsList.Add(new D1D2Len(0, casingLenFt_Reminder, casingIdial));
                                casingIndicator++;
                            }
                        }


                        //do not check more strings
                        ////if (stringIndicator < dsDrillString.Tables[0].Rows.Count)
                        ////{
                        ////    casingLinerStringsList.Add(new D1D2Len(0, stringLenFt_Reminder, stringOdia));
                        ////    stringIndicator++;

                        ////    while (stringIndicator < dsDrillString.Tables[0].Rows.Count)
                        ////    {
                        ////        stringLenFt_Reminder = EasyConvert.Depth_ToFoot(in_selUnitDepth, dsDrillString.Tables[0].Rows[stringIndicator][1].ToString());
                        ////        stringOdia = Convert.ToDecimal(dsDrillString.Tables[0].Rows[stringIndicator][0]);
                        ////        casingLinerStringsList.Add(new D1D2Len(stringOdia, stringLenFt_Reminder, holeSize));
                        ////        stringIndicator++;
                        ////    }
                        ////}
                    }
                    //----------

                    paT = ComputePaT(casingLinerStringsList, Q_GalMin, na, ka, MW_PPG);
                }
                else
                {
                    out_ha_ECDCasingShoe = "No Casing";
                }
                //#########################################################################################

                if (paT != -666666666666)
                {
                    decimal ecdCasingShoePPG = paT / (0.052m * casingBottomTVDFt) + MW_PPG;
                    out_ha_ECDCasingShoe = ecdCasingShoePPG.ToString();
                    //~~~~~~~~~~~~~
                }
                //#########################################################################################
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
        //-------------------------------------------------------
        bool ComputeEDCBitDepth(DataSet dsCasing, DataSet dsDrillString, decimal holeSize, decimal bitTVDFt, decimal Q_GalMin, decimal na, decimal ka, decimal MW_PPG)
        {
            try
            {
                decimal paT = -666666666666;
                decimal casingBottomTVDFt = 0;//last casing

                List<D1D2Len> casingLinerList = new List<D1D2Len>();//D1 is useless, len is used for Tops

                if (dsCasing.Tables[0].Rows.Count == 0)
                {
                    casingLinerList.Add(new D1D2Len(0, 0, holeSize));
                    casingLinerList.Add(new D1D2Len(0, bitTVDFt, holeSize));
                }
                else
                {
                    int lastCasingIndex = -1;

                    for (int i = dsCasing.Tables[0].Rows.Count - 1; i >= 0; i--)
                    {
                        if (dsCasing.Tables[0].Rows[i][0].ToString() == "Casing")
                        {
                            lastCasingIndex = i;
                            casingBottomTVDFt = EasyConvert.Depth_ToFoot(in_selUnitDepth, dsCasing.Tables[0].Rows[i][4].ToString());
                            break;
                        }
                    }

                    if (lastCasingIndex == -1)
                    {
                        out_ha_ECDBitDepth = "No Casing";
                    }
                    else
                    {
                        decimal lastCasingDia = Convert.ToDecimal(dsCasing.Tables[0].Rows[lastCasingIndex][1]);
                        casingLinerList.Add(new D1D2Len(0, 0, lastCasingDia));

                        for (int i = lastCasingIndex + 1; i < dsCasing.Tables[0].Rows.Count; i++)//liners
                        {
                            decimal topFt = EasyConvert.Depth_ToFoot(in_selUnitDepth, dsCasing.Tables[0].Rows[i][2].ToString());
                            decimal dia = Convert.ToDecimal(dsCasing.Tables[0].Rows[i][1]);
                            casingLinerList.Add(new D1D2Len(0, topFt, dia));
                        }

                        if (dsCasing.Tables[0].Rows.Count - (lastCasingIndex + 1) > 0)//last liner
                        {
                            decimal diaLast = Convert.ToDecimal(dsCasing.Tables[0].Rows[dsCasing.Tables[0].Rows.Count - 1][1]);
                            decimal bottomFt = EasyConvert.Depth_ToFoot(in_selUnitDepth, dsCasing.Tables[0].Rows[dsCasing.Tables[0].Rows.Count - 1][4].ToString());
                            casingLinerList.Add(new D1D2Len(0, bottomFt, holeSize));
                        }
                        else//only casing
                        {
                            casingLinerList.Add(new D1D2Len(0, casingBottomTVDFt, lastCasingDia));
                        }


                        casingLinerList.Add(new D1D2Len(0, bitTVDFt, holeSize));
                    }
                }
                //-----------
                if (casingLinerList.Count >= 2)
                {
                    List<D1D2Len> casingLinerStringsList = new List<D1D2Len>();

                    if (dsDrillString.Tables[0].Rows.Count == 0)
                    {
                        for (int casingIndicator = 0; casingIndicator < casingLinerList.Count - 1; casingIndicator++)
                        {
                            decimal casingLenFt = casingLinerList[casingIndicator + 1].len - casingLinerList[casingIndicator].len;
                            decimal casingIdial = casingLinerList[casingIndicator].d2;
                            casingLinerStringsList.Add(new D1D2Len(0, casingLenFt, casingIdial));
                        }
                    }
                    else
                    {
                        decimal stringLenFt_Reminder = EasyConvert.Depth_ToFoot(in_selUnitDepth, dsDrillString.Tables[0].Rows[0][1].ToString());
                        // decimal stringIteratedDepthFt = 0;

                        decimal casingLenFt_Reminder = casingLinerList[1].len - casingLinerList[0].len;
                        // decimal casingIteratedDepthFt = 0;

                        int casingIndicator = 0;
                        int stringIndicator = 0;

                        decimal stringOdia = 0;
                        decimal casingIdial = 0;
                        decimal lenFt = 0;

                        while (casingIndicator < casingLinerList.Count - 1 && stringIndicator < dsDrillString.Tables[0].Rows.Count)
                        {
                            stringOdia = Convert.ToDecimal(dsDrillString.Tables[0].Rows[stringIndicator][0]);
                            casingIdial = casingLinerList[casingIndicator].d2;
                            lenFt = 0;

                            if (stringLenFt_Reminder < casingLenFt_Reminder)
                            {
                                lenFt = stringLenFt_Reminder;
                                casingLinerStringsList.Add(new D1D2Len(stringOdia, lenFt, casingIdial));

                                casingLenFt_Reminder -= lenFt;

                                stringIndicator++;
                                if (stringIndicator < dsDrillString.Tables[0].Rows.Count)
                                    stringLenFt_Reminder = EasyConvert.Depth_ToFoot(in_selUnitDepth, dsDrillString.Tables[0].Rows[stringIndicator][1].ToString());
                            }
                            else if (stringLenFt_Reminder > casingLenFt_Reminder)
                            {
                                lenFt = casingLenFt_Reminder;
                                casingLinerStringsList.Add(new D1D2Len(stringOdia, lenFt, casingIdial));

                                stringLenFt_Reminder -= lenFt;

                                casingIndicator++;
                                if (casingIndicator < casingLinerList.Count - 1)
                                    casingLenFt_Reminder = casingLinerList[casingIndicator + 1].len - casingLinerList[casingIndicator].len;
                            }
                            else
                            {
                                lenFt = stringLenFt_Reminder;
                                casingLinerStringsList.Add(new D1D2Len(stringOdia, lenFt, casingIdial));

                                stringIndicator++;
                                if (stringIndicator < dsDrillString.Tables[0].Rows.Count)
                                    stringLenFt_Reminder = EasyConvert.Depth_ToFoot(in_selUnitDepth, dsDrillString.Tables[0].Rows[stringIndicator][1].ToString());

                                casingIndicator++;
                                if (casingIndicator < casingLinerList.Count - 1)
                                    casingLenFt_Reminder = casingLinerList[casingIndicator + 1].len - casingLinerList[casingIndicator].len;
                            }
                        }
                        //~~~~~~~~~~~~~~~~~~~~~

                        if (casingIndicator < casingLinerList.Count - 1)
                        {
                            casingLinerStringsList.Add(new D1D2Len(0, casingLenFt_Reminder, casingIdial));
                            casingIndicator++;

                            while (casingIndicator < casingLinerList.Count - 1)
                            {
                                casingLenFt_Reminder = casingLinerList[casingIndicator + 1].len - casingLinerList[casingIndicator].len;
                                casingIdial = casingLinerList[casingIndicator].d2;
                                casingLinerStringsList.Add(new D1D2Len(0, casingLenFt_Reminder, casingIdial));
                                casingIndicator++;
                            }
                        }


                        if (stringIndicator < dsDrillString.Tables[0].Rows.Count)
                        {
                            casingLinerStringsList.Add(new D1D2Len(0, stringLenFt_Reminder, stringOdia));
                            stringIndicator++;

                            while (stringIndicator < dsDrillString.Tables[0].Rows.Count)
                            {
                                stringLenFt_Reminder = EasyConvert.Depth_ToFoot(in_selUnitDepth, dsDrillString.Tables[0].Rows[stringIndicator][1].ToString());
                                stringOdia = Convert.ToDecimal(dsDrillString.Tables[0].Rows[stringIndicator][0]);
                                casingLinerStringsList.Add(new D1D2Len(stringOdia, stringLenFt_Reminder, holeSize));
                                stringIndicator++;
                            }
                        }
                    }
                    //----------

                    paT = ComputePaT(casingLinerStringsList, Q_GalMin, na, ka, MW_PPG);
                }
                else
                {
                    out_ha_ECDBitDepth = "No Casing";
                }
                //#########################################################################################

                if (paT != -666666666666)
                {
                    //~~~~~~~~~~~~~
                    decimal ecdDepthPPG = paT / (0.052m * bitTVDFt) + MW_PPG;
                    out_ha_ECDBitDepth = ecdDepthPPG.ToString();
                    //~~~~~~~~~~~~~
                }
                //#########################################################################################
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
        //-------------------------------------------------------
        decimal Pow(decimal a, decimal b)
        {
            return (decimal)Math.Pow((double)a, (double)b);
        }
        //-------------------------------------------------------
        decimal Log10(decimal a)
        {
            return (decimal)Math.Log10((double)a);
        }
        //-------------------------------------------------------
        decimal/*psi*/ ComputePaT(List<D1D2Len> list, decimal Q/*gal/min*/, decimal na, decimal ka, decimal mw/*ppg*/)
        {
            decimal result = 0;

            for (int i = 0; i < list.Count; i++)
            {
                decimal d1 = list[i].d1;
                decimal d2 = list[i].d2;
                decimal len = list[i].len;

                decimal deltaD = d2 - d1;
                decimal deltaD2 = d2 * d2 - d1 * d1;

                if (deltaD == 0)
                    continue;

                decimal vaFtSec = (0.408m * Q) / (deltaD2);
                decimal mhoEA = 100 * ka * Pow(144 * vaFtSec / (deltaD), na - 1);
                decimal Rea = 928 * vaFtSec * (deltaD) * mw / (mhoEA * Pow((2 * na + 1) / (3 * na), na));

                decimal ReL = 3470 - (1370 * na);
                decimal ReT = 4270 - (1370 * na);

                decimal fa = 0;

                if (Rea < ReL)
                    fa = 24 / Rea;
                else if (Rea > ReT)
                    fa = ((Log10(na) + 3.93m) / 50) / (Pow(Rea, (1.75m - Log10(na)) / 7));
                else
                    fa = ((Rea - ReL) / 800) * ((((Log10(na) + 3.93m) / (50)) / (Pow(ReT, (1.75m - Log10(na)) / (7)))) - (24 / ReL)) + (24 / ReL);


                decimal paPsi = (fa * vaFtSec * vaFtSec * mw) * len / (25.81m * deltaD);

                result += paPsi;
            }

            return result;
        }
        //-------------------------------------------------------
    }
}

