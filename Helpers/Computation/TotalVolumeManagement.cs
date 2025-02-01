using System;
using System.Collections.Generic;
using System.Data;

using System.Text;

namespace DMR.Helpers.Computation
{
    class TotalVolumeManagement
    {
        //=================================================
        public string in_selUnitVol;
        public string in_selUnitDepth;
        public string in_selUnitFlowRate;
        public string in_selUnitDischarge;
        public string in_selUnitVolPerStroke;
        ////=================================================
        public string out_hvm_StringVol;
        public string out_hvm_AnnVol;
        public string out_hvm_BelVol;
        public string out_hvm_TotalVol;
        public string out_hvm_DrillVol;
        public string out_svm_PitVol;
        public string out_bvm_EndVol;
        public string out_svm_SurVol;
        public string out_svm_TreVol;
        public string out_bvm_RecVol;
        public string out_bvm_TransVol;
        public string out_bvm_Gain;
        public string out_bvm_AddVol;
        public string out_bvm_BuildVol;
        public string out_bvm_StartVol;
        public string out_bvm_RetVol;
        public string out_bvm_DispVol;
        public string out_bvm_LossVol;
        public string out_cd_BblToBit;
        public string out_cd_BblBottomUp;
        public string out_cd_BblTotalCir;
        public string out_cd_MinsToBit;
        public string out_cd_MinsBottomUp;
        public string out_cd_MinsTotalCir;
        //=================================================
        //returns true on success, else false
        public bool Compute(Int64 repID, Int64 wellID)
        {
            try
            {
                string repNumStr = "-1";
                if (!FetchTableData.GetReportNum(repID.ToString(), out repNumStr))
                    return false;

                string queryCasing =
                      " select wc.CasingGroup, c.ODia, c.IDia, wc.CasingTop, wc.CasingBottom "
                    + " from rt_Well2Casing wc join lt_CasingData c on wc.CasingAutoID = c.AutoID "
                    + " where wc.WellID = " + wellID.ToString()
                    + " and wc.RepNum <= " + repNumStr   //upto current report number
                    + " order by wc.CasingBottom  ";

                DataSet dsCasing = ConnectionManager.ExecQuery(queryCasing);
                //~~~~~~~~~~~~~~
                string queryDrillString =
                      " select o, colListIDia, l "
                    + " from "
                    + " ( "
                    + " 	select d.ODia as o, d.IDia as colListIDia, dh.Length as l"
                    + " 	from rt_Rep2DrlOpHole dh  join lt_Drill_PCH d on dh.DrillPCH_AutoID = d.AutoID "
                    + " 	where dh.ReportID = " + repID.ToString()

                    + " 	union all "  // including duplicates

                    + " 	select ODia as o, IDia as colListIDia, Length as l"
                    + " 	from rt_Rep2DrlOpHole_User "
                    + " 	where ReportID = " + repID.ToString()
                    + " ) as DrillStrings ";

                DataSet dsDrillString = ConnectionManager.ExecQuery(queryDrillString);
                //~~~~~~~~~~~~~~
                string queryReport =
                      " select NightMD_DrlOp_HG, PrevMD_DrlOp_HG, HoleSize_AutoID  from at_Report where ID = " + repID.ToString();

                DataSet dsReport = ConnectionManager.ExecQuery(queryReport, 1);
                //~~~~~~~~~~~~~~
                if (dsCasing != null && dsDrillString != null && dsReport != null && dsReport.Tables[0].Rows[0][2] != DBNull.Value)
                {
                    //-----------
                    decimal nightMDM = Convert.ToDecimal(UnitConverter.Convert("Depth", in_selUnitDepth, "m", dsReport.Tables[0].Rows[0][0].ToString()));
                    decimal prevMDM = Convert.ToDecimal(UnitConverter.Convert("Depth", in_selUnitDepth, "m", dsReport.Tables[0].Rows[0][1].ToString()));
                    decimal holeSize = 0;

                    string hDia;
                    string hLabel;
                    if (FetchTableData.GetHoleSizeAndLabel(Convert.ToInt32(dsReport.Tables[0].Rows[0][2]), out hDia, out hLabel))
                    {
                        AdvancedConvertor.ToDecimal(hDia, ref holeSize);
                    }
                    //-----------
                    decimal stringInnerVolumeBbl = 0;
                    decimal stringOuterVolumeBbl = 0;
                    decimal stringBodyVolumeBbl = 0;
                    decimal stringTotalLengthM = 0;
                    //-----------
                    decimal belowBitVolBbl = 0;
                    //-----------
                    decimal openHoleDepthM = 0;
                    decimal openHoleVolBbl = 0;
                    //-----------
                    decimal emptyWellVolBbl = 0;
                    decimal TotalVolBbl = 0;// = emptyWellVolBbl - stringBodyVolumeBbl
                    decimal AnnVolBbl = 0;// = TotalVolBbl - stringInnerVolumeBbl - belowBitVolBbl

                    //-----------
                    for (int i = 0; i < dsDrillString.Tables[0].Rows.Count; i++)
                    {
                        decimal od = Convert.ToDecimal(dsDrillString.Tables[0].Rows[i][0]);
                        decimal id = Convert.ToDecimal(dsDrillString.Tables[0].Rows[i][1]);
                        decimal len = Convert.ToDecimal(dsDrillString.Tables[0].Rows[i][2]);

                        //convert len -->  len(m)
                        decimal lenM = 0;
                        string lenMStr = UnitConverter.Convert("Depth", in_selUnitDepth, "m", len.ToString());
                        AdvancedConvertor.ToDecimal(lenMStr, ref lenM);

                        decimal inVol = id * id * lenM;
                        decimal outVol = od * od * lenM;

                        stringInnerVolumeBbl += inVol;
                        stringOuterVolumeBbl += outVol;

                        stringBodyVolumeBbl += outVol - inVol;

                        stringTotalLengthM += lenM;
                    }

                    stringInnerVolumeBbl /= 313.76m;//bbl
                    stringOuterVolumeBbl /= 313.76m;//bbl
                    stringBodyVolumeBbl /= 313.76m;//bbl

                    out_hvm_StringVol = EasyConvert.LiquidVolume_FromBBL(in_selUnitVol, stringInnerVolumeBbl).ToString();//bbl->sel unit
                    //----------

                    openHoleDepthM = nightMDM - prevMDM;// lastLinerCasingBottomM;
                    openHoleVolBbl = openHoleDepthM * holeSize * holeSize / 313.76m;
                    out_hvm_DrillVol = EasyConvert.LiquidVolume_FromBBL(in_selUnitVol, openHoleVolBbl).ToString(); ;//bbl->sel unit
                    //----------

                    List<KeyValuePair<decimal/*ind (M)*/, decimal/*dia (in)*/>> ewList = new List<KeyValuePair<decimal, decimal>>();//for empty well

                    if (dsCasing.Tables[0].Rows.Count == 0)
                    {
                        ewList.Add(new KeyValuePair<decimal, decimal>(0, holeSize));
                        ewList.Add(new KeyValuePair<decimal, decimal>(nightMDM, 0/*useless*/));
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

                        if (lastCasingIndex == -1)
                        {
                            out_hvm_AnnVol = "No Casing";
                            out_hvm_BelVol = "No Casing";
                            out_hvm_TotalVol = "No Casing";
                        }
                        else
                        {
                            decimal lastCasingDia = Convert.ToDecimal(dsCasing.Tables[0].Rows[lastCasingIndex][2]);
                            ewList.Add(new KeyValuePair<decimal, decimal>(0, lastCasingDia));

                            for (int i = lastCasingIndex + 1; i < dsCasing.Tables[0].Rows.Count; i++)//liners
                            {
                                decimal top = Convert.ToDecimal(dsCasing.Tables[0].Rows[i][3]);
                                decimal dia = Convert.ToDecimal(dsCasing.Tables[0].Rows[i][2]);
                                ewList.Add(new KeyValuePair<decimal, decimal>(top, dia));
                            }

                            decimal bottom = Convert.ToDecimal(dsCasing.Tables[0].Rows[dsCasing.Tables[0].Rows.Count - 1][4]);
                            ewList.Add(new KeyValuePair<decimal, decimal>(bottom, holeSize));

                            ewList.Add(new KeyValuePair<decimal, decimal>(nightMDM, 0/*useless*/));
                        }
                    }


                    if (ewList.Count >= 2)
                    {
                        emptyWellVolBbl = ComputeVolumeByIndicatorAndDiameter(ewList);
                        //---------------
                        TotalVolBbl = emptyWellVolBbl - stringBodyVolumeBbl;

                        out_hvm_TotalVol = EasyConvert.LiquidVolume_FromBBL(in_selUnitVol, TotalVolBbl).ToString();//bbl->sel unit
                        //---------------

                        int clIDEndString = -1;// casing/Liner At The End Of String

                        for (int k = 0; k < ewList.Count - 1; k++)
                        {
                            if (ewList[k + 1].Key >= stringTotalLengthM)
                            {
                                clIDEndString = k;
                                break;
                            }
                        }

                        if (clIDEndString == -1)
                        {
                            belowBitVolBbl = 0;
                        }
                        else
                        {
                            //use ewList for below bit
                            decimal firstInd = stringTotalLengthM;
                            decimal firstDia = ewList[clIDEndString].Value;

                            ewList.RemoveRange(0, clIDEndString + 1);
                            ewList.Insert(0, new KeyValuePair<decimal, decimal>(firstInd, firstDia));

                            belowBitVolBbl = ComputeVolumeByIndicatorAndDiameter(ewList);

                            out_hvm_BelVol = EasyConvert.LiquidVolume_FromBBL(in_selUnitVol, belowBitVolBbl).ToString();//bbl->sel unit
                        }
                        //---------------
                        AnnVolBbl = TotalVolBbl - stringInnerVolumeBbl - belowBitVolBbl;
                        out_hvm_AnnVol = EasyConvert.LiquidVolume_FromBBL(in_selUnitVol, AnnVolBbl).ToString();//bbl->sel unit
                        //---------------
                    }

                    //-----------
                    dsCasing.Dispose();
                    dsDrillString.Dispose();
                    dsReport.Dispose();
                }
                //#########################################################################################
                //All vol units are selected vol ==> no need to convert units

                //~~~~~~~~~~~~~~~~Pits Volume~~~~~~~~~~~~~~~~~~~~~~  
                {
                    string query =
                    " select sum(CurMudVol) from rt_Rep2MudVolMan_PitTankVol where ReportID = " + repID.ToString();

                    DataSet ds = ConnectionManager.ExecQuery(query, 1);

                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows[0][0] != DBNull.Value)
                            out_svm_PitVol = ds.Tables[0].Rows[0][0].ToString();
                        ds.Dispose();
                    }
                }

                //~~~~~~~~~~~~~~~~End Volume~~~~~~~~~~~~~~~~~~~~~~
                decimal afterMergeSum = 0;
                {
                    string query = " select sum( dbo.fn_Get_AfterMergeVol_ByWellID_UptoRepNum ("
                                        + frmMain.selected_RW_WellID.ToString() + ","
                                        + frmMain.selectedRepNum.ToString() + ", Dfs_AutoID ) )"
                                        + " from rt_Rep2MudVolManDFS"
                                        + " where ReportID = " + repID.ToString();

                    DataSet ds = new DataSet();

                    if (ConnectionManager.ExecQuery(query, ref ds, 1))
                    {
                        if (ds.Tables[0].Rows[0][0] != DBNull.Value)
                            afterMergeSum = Convert.ToDecimal(ds.Tables[0].Rows[0][0]);

                        ds.Dispose();
                    }
                }

                out_bvm_EndVol = (Convert.ToDecimal(out_svm_PitVol) + afterMergeSum).ToString();


                //~~~~~~~~~~~~~~~~ Surface Circulation Volume ~~~~~~~~~~~~~~~~~~~~~~
                {
                    string query =
                    " select sum(CurMudVol) from rt_Rep2MudVolMan_PitTankVol where  CurrentStatus = \'A\' and ReportID = " + repID.ToString();

                    DataSet ds = ConnectionManager.ExecQuery(query, 1);

                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows[0][0] != DBNull.Value)
                            out_svm_SurVol = ds.Tables[0].Rows[0][0].ToString();
                        ds.Dispose();
                    }
                }

                //~~~~~~~~~~~~~~~~~~ Treated ~~~~~~~~~~~~~~~~~~~~
                {
                    string query =
                          "  select sum(vol) "
                        + "  from "
                        + "  ( "
                        + "  	select  "
                        + "  	( "
                        + "  		sum  "
                        + "  		(  "
                        + "  		CASE   "
                        + "  		WHEN (prd.UnitSize = '55 GAL/DRUM') THEN 1.3095 * (tp.Used_PDF + tp.Used_OtherCompany) "
                        + "  		WHEN (prd.UnitSize = '5 GAL/CAN') THEN 0.119 * (tp.Used_PDF + tp.Used_OtherCompany)	  "
                        + "  		WHEN (prd.UnitSize = '25 KG/SXS') THEN 0.1573 * ((tp.Used_PDF + tp.Used_OtherCompany) / prd.SpecificGravity) "
                        + "  		WHEN (prd.UnitSize = '50 KG/SXS') THEN 0.31461 * ((tp.Used_PDF + tp.Used_OtherCompany) / prd.SpecificGravity) "
                        + "  		WHEN (prd.UnitSize = '25 KG/PAIL') THEN 0.1573 * ((tp.Used_PDF + tp.Used_OtherCompany) / prd.SpecificGravity) "
                        + "  		WHEN (prd.UnitSize = '12.5 KG/SXS') THEN 0.07865 * ((tp.Used_PDF + tp.Used_OtherCompany) / prd.SpecificGravity) "
                        + "  		WHEN (prd.UnitSize = '1 MT/B.B') THEN 6.292 * ((tp.Used_PDF + tp.Used_OtherCompany) / prd.SpecificGravity)  "
                        + "  		WHEN (prd.UnitSize = '1.5 MT/B.B') THEN 9.438 * ((tp.Used_PDF + tp.Used_OtherCompany) / prd.SpecificGravity) "
                        + "  		WHEN (prd.UnitSize = '1 MT/BULK') THEN 6.292 * ((tp.Used_PDF + tp.Used_OtherCompany) / prd.SpecificGravity) "
                        + "  		ELSE 0 END 	  "
                        + "  		) "//chemical volume
                        + "  	+ "
                        + "  		sum(t.VolTreat + t.AddOil + t.AddSeaWater + t.AddDrillWater + t.AddDeWater + t.AddLocalWater) / count(*)   " // once per group  ;)
                        + "  	) as vol "
                        + "    "
                        + "  	from rt_Rep2MudVolManDFS d  "
                        + "  	join rt_Rep2MudVolManDFS_TreatedVol t on d.ID = t.MudVolManDFS_ID  "
                        + "  	join rt_Rep2MudVolManDFS_TreatedVol_Prd tp on t.ID = tp.TreatedVol_ID "
                        + "  	join lt_Product prd on tp.Prd_AutoID = prd.AutoID   "
                        + "  	where d.ReportID =  " + repID.ToString()
                        + "  	group by t.ID "

                        + "  ) as FinalTreatVolumes ";

                    DataSet ds = ConnectionManager.ExecQuery(query, 1);


                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows[0][0] != DBNull.Value)
                            out_svm_TreVol = ds.Tables[0].Rows[0][0].ToString();

                        ds.Dispose();
                    }
                }


                //#########################################################################################
                //All units are selected vol ==> no need to convert units

                //~~~~~~~~~~~~~~~~Rec~~~~~~~~~~~~~~~~~~~~~~
                {
                    string query =
                      " select sum(r.Vol) "
                    + " from rt_Rep2MudVolManDFS d join rt_Rep2MudVolManDFS_RecTrans r on d.ID = r.MudVolManDFS_ID  "
                    + " where r.RecTrans_Flag = 0 and d.ReportID = " + repID.ToString();

                    DataSet ds = ConnectionManager.ExecQuery(query, 1);

                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows[0][0] != DBNull.Value)
                            out_bvm_RecVol = ds.Tables[0].Rows[0][0].ToString();

                        ds.Dispose();
                    }
                }
                //~~~~~~~~~~~~~~~~~~Trans~~~~~~~~~~~~~~~~~~~~
                {
                    string query =
                      " select sum(r.Vol) "
                    + " from rt_Rep2MudVolManDFS d join rt_Rep2MudVolManDFS_RecTrans r on d.ID = r.MudVolManDFS_ID  "
                    + " where r.RecTrans_Flag = 0 and d.ReportID = " + repID.ToString();

                    DataSet ds = ConnectionManager.ExecQuery(query, 1);

                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows[0][0] != DBNull.Value)
                            out_bvm_TransVol = ds.Tables[0].Rows[0][0].ToString();
                        ds.Dispose();
                    }
                }
                //~~~~~~~~~~~~~~~~~~Gain, Start, Rain~~~~~~~~~~~~~~~~~~~~
                {
                    string query =
                      " select	sum(BuiltGainVol), sum(BuiltStartVol), sum(BuiltRainVol)  "
                    + " from rt_Rep2MudVolManDFS "
                    + " where ReportID = " + repID.ToString();

                    DataSet ds = ConnectionManager.ExecQuery(query, 1);

                    string gain = "0", start = "0", rain = "0";

                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows[0][0] != DBNull.Value)
                            gain = ds.Tables[0].Rows[0][0].ToString();

                        if (ds.Tables[0].Rows[0][1] != DBNull.Value)
                            start = ds.Tables[0].Rows[0][1].ToString();

                        if (ds.Tables[0].Rows[0][2] != DBNull.Value)
                            rain = ds.Tables[0].Rows[0][2].ToString();

                        ds.Dispose();
                    }

                    out_bvm_Gain = gain;
                    out_bvm_StartVol = start;
                }
                //~~~~~~~~~~~~~~~~~~Treat Added~~~~~~~~~~~~~~~~~~~~
                {
                    //???????? +chemical vol
                    string query =
                      " select	sum(t.AddOil + t.AddSeaWater + t.AddDrillWater + t.AddDeWater + t.AddLocalWater + dbo.fn_Get_TreatedPrd_ChemicalVolume(t.ID))  "
                    + " from rt_Rep2MudVolManDFS d join rt_Rep2MudVolManDFS_TreatedVol t on d.ID = t.MudVolManDFS_ID  "
                    + " where d.ReportID = " + repID.ToString();

                    DataSet ds = ConnectionManager.ExecQuery(query, 1);

                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows[0][0] != DBNull.Value)
                            out_bvm_AddVol = ds.Tables[0].Rows[0][0].ToString();

                        ds.Dispose();
                    }
                }
                //~~~~~~~~~~~~~~~~~~Built~~~~~~~~~~~~~~~~~~~~
                {
                    string query =
                          "  select sum(vol) "
                        + "  from "
                        + "  ( "
                        + "  	select  "
                        + "  	( "
                        + "  		sum  "
                        + "  		(  "
                        + "  		CASE   "
                        + "  		WHEN (prd.UnitSize = '55 GAL/DRUM') THEN 1.3095 * (bp.Used_PDF + bp.Used_OtherCompany) "
                        + "  		WHEN (prd.UnitSize = '5 GAL/CAN') THEN 0.119 * (bp.Used_PDF + bp.Used_OtherCompany)	  "
                        + "  		WHEN (prd.UnitSize = '25 KG/SXS') THEN 0.1573 * ((bp.Used_PDF + bp.Used_OtherCompany) / prd.SpecificGravity) "
                        + "  		WHEN (prd.UnitSize = '50 KG/SXS') THEN 0.31461 * ((bp.Used_PDF + bp.Used_OtherCompany) / prd.SpecificGravity) "
                        + "  		WHEN (prd.UnitSize = '25 KG/PAIL') THEN 0.1573 * ((bp.Used_PDF + bp.Used_OtherCompany) / prd.SpecificGravity) "
                        + "  		WHEN (prd.UnitSize = '12.5 KG/SXS') THEN 0.07865 * ((bp.Used_PDF + bp.Used_OtherCompany) / prd.SpecificGravity) "
                        + "  		WHEN (prd.UnitSize = '1 MT/B.B') THEN 6.292 * ((bp.Used_PDF + bp.Used_OtherCompany) / prd.SpecificGravity)  "
                        + "  		WHEN (prd.UnitSize = '1.5 MT/B.B') THEN 9.438 * ((bp.Used_PDF + bp.Used_OtherCompany) / prd.SpecificGravity) "
                        + "  		WHEN (prd.UnitSize = '1 MT/BULK') THEN 6.292 * ((bp.Used_PDF + bp.Used_OtherCompany) / prd.SpecificGravity) "
                        + "  		ELSE 0 END 	  "
                        + "  		) "//chemical volume
                        + "  	+ "
                        + "  		sum(b.AddOil + b.AddSeaWater + b.AddDrillWater + b.AddDeWater + b.AddLocalWater) / count(*)   " // once per group  ;)
                        + "  	) as vol "
                        + "    "
                        + "  	from rt_Rep2MudVolManDFS d  "
                        + "  	join rt_Rep2MudVolManDFS_BuiltVol b on d.ID = b.MudVolManDFS_ID  "
                        + "  	join rt_Rep2MudVolManDFS_BuiltVol_Prd bp on b.ID = bp.BuiltVol_ID "
                        + "  	join lt_Product prd on bp.Prd_AutoID = prd.AutoID   "
                        + "  	where d.ReportID =  " + repID.ToString()
                        + "  	group by b.ID "

                        + "  ) as BuiltVolumes ";

                    DataSet ds = ConnectionManager.ExecQuery(query, 1);

                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows[0][0] != DBNull.Value)
                            out_bvm_BuildVol = ds.Tables[0].Rows[0][0].ToString();

                        ds.Dispose();
                    }
                }
                //~~~~~~~~~~~~~~~~~~return, disp.~~~~~~~~~~~~~~~~~~~~
                {
                    string query =
                      " select sum(AtEndMudRetPitVol + AtEndMudRetWasteVol + AtEndMudRetOverVol), sum(AtEndMudDispVol) "
                    + " from rt_Rep2MudVolManDFS "
                    + " where ReportID = " + repID.ToString();

                    DataSet ds = ConnectionManager.ExecQuery(query, 1);

                    string ret = "0.###", disp = "0.###";

                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows[0][0] != DBNull.Value)
                            ret = ds.Tables[0].Rows[0][0].ToString();

                        if (ds.Tables[0].Rows[0][1] != DBNull.Value)
                            disp = ds.Tables[0].Rows[0][1].ToString();

                        ds.Dispose();
                    }

                    out_bvm_RetVol = ret;
                    out_bvm_DispVol = disp;
                }
                //~~~~~~~~~~~~~~~~~~Loss~~~~~~~~~~~~~~~~~~~~
                {
                    string lossRec = "0", formLos = "0", actVolLost = "0", wasteOverboard = "0";

                    {
                        string query = " select sum(Vol) from rt_Rep2MudLossRecord where ReportID = " + repID.ToString();
                        DataSet ds = ConnectionManager.ExecQuery(query, 1);

                        if (ds != null)
                        {
                            if (ds.Tables[0].Rows[0][0] != DBNull.Value)
                                lossRec = ds.Tables[0].Rows[0][0].ToString();

                            ds.Dispose();
                        }
                    }

                    {
                        string query = " select sum(Vol) from rt_Rep2MudLossFormation where ReportID = " + repID.ToString();
                        DataSet ds = ConnectionManager.ExecQuery(query, 1);

                        if (ds != null)
                        {
                            if (ds.Tables[0].Rows[0][0] != DBNull.Value)
                                formLos = ds.Tables[0].Rows[0][0].ToString();

                            ds.Dispose();
                        }
                    }


                    {
                        string query = "  select sum(u.Used*u.Discharge) from rt_Rep2SolidControlUsed u join rt_Rep2SolidControl s on u.R2SC_ID = s.ID where s.ReportID = " + repID.ToString();
                        DataSet ds = ConnectionManager.ExecQuery(query, 1);

                        if (ds != null)
                        {
                            if (ds.Tables[0].Rows[0][0] != DBNull.Value)
                                actVolLost = ds.Tables[0].Rows[0][0].ToString();

                            ds.Dispose();
                        }
                    }



                    {
                        string query = "  select sum(DailyMudRetWasteVol+DailyMudRetOverVol+AtEndMudRetWasteVol+AtEndMudRetOverVol) from rt_Rep2MudVolManDFS where ReportID = " + repID.ToString();
                        DataSet ds = ConnectionManager.ExecQuery(query, 1);

                        if (ds != null)
                        {
                            if (ds.Tables[0].Rows[0][0] != DBNull.Value)
                                wasteOverboard = ds.Tables[0].Rows[0][0].ToString();

                            ds.Dispose();
                        }
                    }


                    decimal actLosFactor = 1;
                    if (in_selUnitDischarge == "bbl/hr")
                    {
                        if (in_selUnitVol == "m³")
                            actLosFactor = 0.1589873m;//m³ -> bbl
                    }
                    else// m³/hr
                    {
                        if (in_selUnitVol == "bbl")
                            actLosFactor = 6.2898m;//bbl -> m³ 
                    }

                    out_bvm_LossVol = (Convert.ToDecimal(lossRec) + Convert.ToDecimal(formLos) + Convert.ToDecimal(actVolLost) * actLosFactor + Convert.ToDecimal(wasteOverboard)).ToString();
                }
                //#########################################################################################
                out_cd_BblToBit = UnitConverter.Convert("Liquid Volume", in_selUnitVol, "bbl", out_hvm_StringVol);

                out_cd_BblBottomUp = UnitConverter.Convert("Liquid Volume", in_selUnitVol, "bbl", out_hvm_AnnVol);

                decimal totalCir = Convert.ToDecimal(out_hvm_TotalVol) + Convert.ToDecimal(out_svm_SurVol) - Convert.ToDecimal(out_hvm_BelVol);
                out_cd_BblTotalCir = UnitConverter.Convert("Liquid Volume", in_selUnitVol, "bbl", totalCir.ToString());
                //~~~~~~~~~~~~~~
                decimal sumCirRate = 0;
                {
                    string query =
                              " select dbo.fn_Get_VolPerStkBbl(pm.ID), rmp.StkRate "
                            + " from rt_Rig2MudPump pm "
                            + " join rt_Rig2Well rw on rw.RigID = pm.RigID "
                            + " join at_Report r on rw.ID = r.RigWellID "
                            + " join rt_Rep2MudPump rmp on rmp.ReportID = r.ID  and rmp.PumpID = pm.ID " // it is important: 'and rmp.PumpID = pm.ID' ==> preventing dupliates
                            + " where rmp.ReportID =  " + repID.ToString()
                            + " order by pm.ID ";

                    DataSet ds = ConnectionManager.ExecQuery(query);

                    if (ds != null)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            decimal volStkBbl = Convert.ToDecimal(ds.Tables[0].Rows[i][0]);
                            decimal stkRate = Convert.ToDecimal(ds.Tables[0].Rows[i][1]);
                            sumCirRate += (stkRate * volStkBbl);
                        }

                        ds.Dispose();
                    }
                }
                //~~~~~~~~~~~~~~
                out_cd_MinsToBit = (Convert.ToDecimal(out_cd_BblToBit) / sumCirRate).ToString();
                out_cd_MinsBottomUp = (Convert.ToDecimal(out_cd_BblBottomUp) / sumCirRate).ToString();
                out_cd_MinsTotalCir = (Convert.ToDecimal(out_cd_BblTotalCir) / sumCirRate).ToString();
                //#########################################################################################
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
        //-------------------------------------------------------
        decimal/*bbl*/ ComputeVolumeByIndicatorAndDiameter(List<KeyValuePair<decimal/*ind (M)*/, decimal/*dia (in)*/>> list)
        {
            decimal result = 0;

            for (int i = 0; i < list.Count - 1; i++)
            {
                decimal len = list[i + 1].Key - list[i].Key;
                decimal dia = list[i].Value;
                result += dia * dia * len;
            }

            result /= 313.76m;
            return result;
        }
        //-------------------------------------------------------
    }
}
