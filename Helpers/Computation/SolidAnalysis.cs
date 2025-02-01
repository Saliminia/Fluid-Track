using System;
using System.Collections.Generic;

using System.Text;

namespace DMR.Helpers.Computation
{
    class SolidAnalysis
    {
        //=================================================
        public string in_selUnitVol;
        public string in_selUnitPlasticVis;
        public string in_selUnitIonic;
        public string in_selUnitKClCon;
        public string in_selUnitNaClCon;
        public string in_selUnitPmPfMf;
        public string in_selUnitMW;
        public string in_selUnitMBT;
        public string in_selUnitPowderMatCon;
        public string in_selUnitYieldPoint;
        public string in_selUnitPowderMatConc;
        ////=================================================
        public string in_FreshMudMBT;
        public string in_BentCEC;
        public string in_BarDens;
        public string in_HemDens;
        public string in_CalCarDens;
        public string in_BarConc;
        public string in_HemConc;
        public string in_CalCarConc;
        //=================================================
        public string out_ActMudMBT;
        public string out_DSCEC;
        public string out_Water;
        public string out_OilLubricant;
        public string out_Glycol;
        public string out_TotalSolid;
        public string out_MW;
        public string out_SandContent;
        public string out_TotalCh;
        public string out_KCL_wt;
        public string out_KCL;
        public string out_KCLCh;
        public string out_NaCl;
        public string out_NaClCh;
        public string out_DisSolVol;
        public string out_DisSol_wt;
        public string out_NaClVol;
        public string out_NaClwt;
        public string out_KClVol;
        public string out_CorSolVol;
        public string out_CorSol_BBL;
        public string out_WeiMatVol;
        public string out_WeiMat_BBL;
        public string out_BariteVol;
        public string out_Barite_BBL;
        public string out_HematitVol;
        public string out_Hematit_BBL;
        public string out_CalCarVol;
        public string out_CalCar_BBL;
        public string out_LGSVol;
        public string out_LGS_BBL;
        public string out_BentVol;
        public string out_Bent_BBL;
        public string out_DrillSolVol;
        public string out_DrillSol_BBL;
        //=================================================
        //returns true on success, else false
        public bool Compute(Int64 mudPropColID/*for db*/,
            bool b/*barit check box*/,
            bool h/*hematit check box*/,
            bool c/*cal. Carb check box*/ ,
            bool w/*weighted check box*/)
        {
            try
            {
                //~~~~~~~~~~~~~~
                out_ActMudMBT = "!!!";
                out_DSCEC = "!!!";
                out_Water = "!!!";
                out_OilLubricant = "!!!";
                out_Glycol = "!!!";
                out_TotalSolid = "!!!";
                out_MW = "!!!";
                out_SandContent = "!!!";
                out_TotalCh = "!!!";
                out_KCL_wt = "!!!";
                out_KCL = "!!!";
                out_KCLCh = "!!!";
                out_NaCl = "!!!";
                out_NaClCh = "!!!";
                out_DisSolVol = "!!!";
                out_DisSol_wt = "!!!";
                out_NaClVol = "!!!";
                out_NaClwt = "!!!";
                out_KClVol = "!!!";
                out_CorSolVol = "!!!";
                out_CorSol_BBL = "!!!";
                out_WeiMatVol = "!!!";
                out_WeiMat_BBL = "!!!";
                out_BariteVol = "!!!";
                out_Barite_BBL = "!!!";
                out_HematitVol = "!!!";
                out_Hematit_BBL = "!!!";
                out_CalCarVol = "!!!";
                out_CalCar_BBL = "!!!";
                out_LGSVol = "!!!";
                out_LGS_BBL = "!!!";
                out_BentVol = "!!!";
                out_Bent_BBL = "!!!";
                out_DrillSolVol = "!!!";
                out_DrillSol_BBL = "!!!";
                //~~~~~~~~~~~~~~
                Dictionary<string, decimal> mudPorp = FetchTableData.GetMudPropertiesOfOneMudPropColumn(mudPropColID);
                if (mudPorp.Count == 0)
                    return false;
                //~~~~~~~~~~~~~~
                decimal density,
                    sandContent,
                    water,
                    oilLubricant,
                    glycol,
                    totalChlorides,
                    drillSolidsCEC,
                    mudMBT;

                if (
                    !mudPorp.TryGetValue("Density", out density) ||
                    !mudPorp.TryGetValue("Sand Content", out sandContent) ||
                    !mudPorp.TryGetValue("Water", out water) ||
                    !mudPorp.TryGetValue("Oil/Lubricant", out oilLubricant) ||
                    !mudPorp.TryGetValue("Glycol", out glycol) ||
                    !mudPorp.TryGetValue("Total Chlorides", out totalChlorides) ||
                    !mudPorp.TryGetValue("Drill Solids CEC", out drillSolidsCEC) ||
                    !mudPorp.TryGetValue("Mud MBT", out mudMBT)
                    )
                    return false;

                if (
                    density == MudPropForm.INVALID_VALUE ||
                    sandContent == MudPropForm.INVALID_VALUE ||
                    water == MudPropForm.INVALID_VALUE ||
                    oilLubricant == MudPropForm.INVALID_VALUE ||
                    glycol == MudPropForm.INVALID_VALUE ||
                    totalChlorides == MudPropForm.INVALID_VALUE ||
                    drillSolidsCEC == MudPropForm.INVALID_VALUE ||
                    mudMBT == MudPropForm.INVALID_VALUE
                    )
                    return false;

                //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
                Helpers.Computation.MudProperties mudComp = new Helpers.Computation.MudProperties();
                //~~~~~~~~~~~~~~
                mudComp.in_selUnitVol = in_selUnitVol;
                mudComp.in_selUnitPlasticVis = in_selUnitPlasticVis;
                mudComp.in_selUnitYieldPoint = in_selUnitYieldPoint;
                mudComp.in_selUnitIonic = in_selUnitIonic;
                mudComp.in_selUnitKClCon = in_selUnitKClCon;
                mudComp.in_selUnitNaClCon = in_selUnitNaClCon;
                mudComp.in_selUnitPmPfMf = in_selUnitPmPfMf;
                mudComp.in_selUnitPowderMatConc = in_selUnitPowderMatConc;
                //~~~~~~~~~~~~~~
                if (
                    !mudPorp.TryGetValue("R600", out mudComp.in_R600) ||
                    !mudPorp.TryGetValue("R300", out mudComp.in_R300) ||
                    !mudPorp.TryGetValue("KCl", out mudComp.in_KClwt) ||
                    !mudPorp.TryGetValue("Pf", out mudComp.in_Pf) ||
                    !mudPorp.TryGetValue("Mf", out mudComp.in_Mf) ||
                    !mudPorp.TryGetValue("Total Chlorides", out mudComp.in_TotalCh) ||
                    !mudPorp.TryGetValue("Total Hardness", out mudComp.in_TotalH) ||
                    !mudPorp.TryGetValue("Ca++", out mudComp.in_Capp) ||
                    !mudPorp.TryGetValue("Alkal Mud (Pm)", out mudComp.in_Pm) ||
                    !mudPorp.TryGetValue("Water", out mudComp.in_water)
                    )
                    return false;

                if (
                    mudComp.in_R600 == MudPropForm.INVALID_VALUE ||
                    mudComp.in_R300 == MudPropForm.INVALID_VALUE ||
                    mudComp.in_KClwt == MudPropForm.INVALID_VALUE ||
                    mudComp.in_Pf == MudPropForm.INVALID_VALUE ||
                    mudComp.in_Mf == MudPropForm.INVALID_VALUE ||
                    mudComp.in_TotalCh == MudPropForm.INVALID_VALUE ||
                    mudComp.in_TotalH == MudPropForm.INVALID_VALUE ||
                    mudComp.in_Capp == MudPropForm.INVALID_VALUE ||
                    mudComp.in_Pm == MudPropForm.INVALID_VALUE ||
                    mudComp.in_water == MudPropForm.INVALID_VALUE
                    )
                    return false;
                //~~~~~~~~~~~~~~
                mudComp.Compute();
                //~~~~~~~~~~~~~~
                out_MW = UnitConverter.Convert("Mud Weight", in_selUnitMW, "ppg", density.ToString());

                decimal mwPPG = 0;
                AdvancedConvertor.ToDecimal(out_MW, ref mwPPG);
                //-----------------
                out_SandContent = sandContent.ToString();
                out_Water = water.ToString();
                out_OilLubricant = oilLubricant.ToString();
                out_Glycol = glycol.ToString();
                out_TotalCh = totalChlorides.ToString();
                out_KCL_wt = mudComp.in_KClwt.ToString();
                out_KCLCh = mudComp.out_KClCh;
                out_KCL = mudComp.out_KCl;

                out_NaCl = mudComp.out_NaCl;
                out_NaClCh = mudComp.out_NaClCh;
                out_DSCEC = drillSolidsCEC.ToString();
                out_ActMudMBT = mudMBT.ToString();

                decimal oilLubricantPercent = 0, waterPercent = 0, glycolPercent = 0;
                AdvancedConvertor.ToDecimal(oilLubricant.ToString(), ref oilLubricantPercent);
                AdvancedConvertor.ToDecimal(water.ToString(), ref waterPercent);
                AdvancedConvertor.ToDecimal(glycol.ToString(), ref glycolPercent);
                //-----------------
                decimal kclMgL = 0, naclMgL = 0;
                AdvancedConvertor.ToDecimal(UnitConverter.Convert("KCl Concentration", in_selUnitKClCon, "mg/l", mudComp.out_KCl), ref kclMgL);
                AdvancedConvertor.ToDecimal(UnitConverter.Convert("NaCl Concentration", in_selUnitNaClCon, "mg/l", mudComp.out_NaCl), ref naclMgL);
                //-----------------
                decimal freshMbtBBL = 0;
                AdvancedConvertor.ToDecimal(UnitConverter.Convert("Mud MBT", in_selUnitMBT, "lb/bbl", in_FreshMudMBT), ref freshMbtBBL);

                decimal activeMbtBBL = 0;
                AdvancedConvertor.ToDecimal(UnitConverter.Convert("Mud MBT", in_selUnitMBT, "lb/bbl", out_ActMudMBT), ref activeMbtBBL);

                decimal bentCEC = 0; AdvancedConvertor.ToDecimal(in_BentCEC, ref bentCEC);
                decimal drillSolCEC = 0; AdvancedConvertor.ToDecimal(out_DSCEC, ref drillSolCEC);
                //-----------------
                decimal kclChMgL = kclMgL * 0.476m;
                decimal kclSG = 1.00056m + (1.22832m * (0.000001m) * kclChMgL);

                decimal totalChMgL = 0;
                AdvancedConvertor.ToDecimal(UnitConverter.Convert("Ionic Mass Concentration", in_selUnitIonic, "mg/l", totalChlorides.ToString()), ref totalChMgL);

                decimal naclChMgL = totalChMgL - kclChMgL;

                decimal kclVolPercent = (2.775m * (0.0000001m) * (decimal)Math.Pow((double)kclChMgL, 1.105)) * waterPercent;
                decimal naclVolPercent = (5.88m * (0.00000001m) * (decimal)Math.Pow((double)naclChMgL, 1.2)) * waterPercent;

                out_NaClVol = naclVolPercent.ToString();
                out_KClVol = kclVolPercent.ToString();

                decimal kclWt = 0, naclWt = 0;
                AdvancedConvertor.ToDecimal(UnitConverter.Convert("KCl Concentration", "mg/l", "wt%", kclMgL.ToString()), ref kclWt);
                AdvancedConvertor.ToDecimal(UnitConverter.Convert("NaCl Concentration", "mg/l", "wt%", naclMgL.ToString()), ref naclWt);

                decimal naclSG = 1 + (1.94m * (0.000001m) * (decimal)Math.Pow((double)naclChMgL, 0.95));
                //-----------------
                decimal dissolvedSolidPercent = (kclVolPercent + naclVolPercent);
                out_DisSolVol = dissolvedSolidPercent.ToString();
                out_DisSol_wt = (kclWt + naclWt).ToString();
                //-----------------
                decimal hematitDensPPG = 0, baritDensPPG = 0, calCarDensPPG = 0;
                AdvancedConvertor.ToDecimal(in_BarDens, ref baritDensPPG);
                AdvancedConvertor.ToDecimal(in_HemDens, ref hematitDensPPG);
                AdvancedConvertor.ToDecimal(in_CalCarDens, ref calCarDensPPG);
                //-----------------
                decimal hematitVolPercent = 0, baritVolPercent = 0, calCarVolPercent = 0;
                decimal hematitBBL = 0, baritBBL = 0, calCarBBL = 0;
                //-----------------
                decimal SaltWaterDensSG = kclSG + naclSG - 1;
                //-----------------

                if (!w || (!b && !h && !c))
                {
                    /*nothing*/
                }
                else if (b && !h && !c)
                {
                    baritVolPercent = ((100m * mwPPG) - ((100m - waterPercent - dissolvedSolidPercent - oilLubricantPercent - glycolPercent) * 21.7m) - ((dissolvedSolidPercent + waterPercent) * (SaltWaterDensSG * 8.34m)) - (5 * oilLubricantPercent) - (1 * glycolPercent)) / (baritDensPPG - 21.7m);
                    baritBBL = baritVolPercent * baritDensPPG * 42m / 100m;
                }
                else if (!b && h && !c)
                {
                    hematitVolPercent = ((100m * mwPPG) - ((100m - waterPercent - dissolvedSolidPercent - oilLubricantPercent - glycolPercent) * 21.7m) - ((dissolvedSolidPercent + waterPercent) * (SaltWaterDensSG * 8.34m)) - (5m * oilLubricantPercent) - (1m * glycolPercent)) / (hematitDensPPG - 21.7m);
                    hematitBBL = hematitVolPercent * hematitDensPPG * 42m / 100m;
                }
                else if (!b && !h && c)
                {
                    AdvancedConvertor.ToDecimal(UnitConverter.Convert("Powder Material Concentration", in_selUnitPowderMatCon, "lb/bbl", in_CalCarConc), ref calCarBBL);
                    calCarVolPercent = calCarBBL * 100m / (42m * calCarDensPPG);
                }
                else if (b && h && !c)
                {
                    AdvancedConvertor.ToDecimal(UnitConverter.Convert("Powder Material Concentration", in_selUnitPowderMatCon, "lb/bbl", in_BarConc), ref baritBBL);
                    baritVolPercent = baritBBL * 100 / (42 * baritDensPPG);

                    AdvancedConvertor.ToDecimal(UnitConverter.Convert("Powder Material Concentration", in_selUnitPowderMatCon, "lb/bbl", in_HemConc), ref hematitBBL);
                    hematitVolPercent = hematitBBL * 100m / (42m * hematitDensPPG);
                }
                else if (b && !h && c)
                {
                    AdvancedConvertor.ToDecimal(UnitConverter.Convert("Powder Material Concentration", in_selUnitPowderMatCon, "lb/bbl", in_BarConc), ref baritBBL);
                    baritVolPercent = baritBBL * 100 / (42 * baritDensPPG);


                    AdvancedConvertor.ToDecimal(UnitConverter.Convert("Powder Material Concentration", in_selUnitPowderMatCon, "lb/bbl", in_CalCarConc), ref calCarBBL);
                    calCarVolPercent = calCarBBL * 100m / (42m * calCarDensPPG);
                }
                else if (!b && h && c)
                {
                    AdvancedConvertor.ToDecimal(UnitConverter.Convert("Powder Material Concentration", in_selUnitPowderMatCon, "lb/bbl", in_HemConc), ref hematitBBL);
                    hematitVolPercent = hematitBBL * 100m / (42m * hematitDensPPG);

                    AdvancedConvertor.ToDecimal(UnitConverter.Convert("Powder Material Concentration", in_selUnitPowderMatCon, "lb/bbl", in_CalCarConc), ref calCarBBL);
                    calCarVolPercent = calCarBBL * 100m / (42m * calCarDensPPG);
                }
                else if (b && h && c)
                {
                    AdvancedConvertor.ToDecimal(UnitConverter.Convert("Powder Material Concentration", in_selUnitPowderMatCon, "lb/bbl", in_BarConc), ref baritBBL);
                    baritVolPercent = baritBBL * 100 / (42 * baritDensPPG);

                    AdvancedConvertor.ToDecimal(UnitConverter.Convert("Powder Material Concentration", in_selUnitPowderMatCon, "lb/bbl", in_HemConc), ref hematitBBL);
                    hematitVolPercent = hematitBBL * 100m / (42m * hematitDensPPG);

                    AdvancedConvertor.ToDecimal(UnitConverter.Convert("Powder Material Concentration", in_selUnitPowderMatCon, "lb/bbl", in_CalCarConc), ref calCarBBL);
                    calCarVolPercent = calCarBBL * 100m / (42m * calCarDensPPG);
                }
                //~~~~~~~~~~~~~~~~
                decimal weiMatVolPercent = baritVolPercent + hematitVolPercent + calCarVolPercent;
                decimal weiMatBBL = baritBBL + hematitBBL + calCarBBL;

                out_WeiMatVol = weiMatVolPercent.ToString();
                out_WeiMat_BBL = weiMatBBL.ToString();
                //~~~~~~~~~~~~~~~~
                decimal lgsVolPercent = 100 - weiMatVolPercent - waterPercent - oilLubricantPercent - glycolPercent - dissolvedSolidPercent;
                decimal lgsBBL = lgsVolPercent * 21.7m * 42m / 100m;

                out_LGSVol = lgsVolPercent.ToString();
                out_LGS_BBL = lgsBBL.ToString();
                //~~~~~~~~~~~~~~~~
                decimal corSolVolPercent = weiMatVolPercent + lgsVolPercent;
                decimal corSolBBL = weiMatBBL + lgsBBL;

                out_CorSolVol = corSolVolPercent.ToString();
                out_CorSol_BBL = corSolBBL.ToString();
                //~~~~~~~~~~~~~~~~
                decimal bentVolPercent = 0;
                try
                {
                    bentVolPercent = ((7.69m * activeMbtBBL) - (lgsVolPercent * drillSolCEC)) / (bentCEC - drillSolCEC);
                    decimal bentBBL = bentVolPercent * 21.7m * 42m / 101m;

                    out_BentVol = bentVolPercent.ToString();
                    out_Bent_BBL = bentBBL.ToString();
                }
                catch (Exception)
                {
                    bentVolPercent = 0;
                }
                //~~~~~~~~~~~~~~~~
                decimal drilSolVolPercent = lgsVolPercent - bentVolPercent;
                decimal drilSolBBL = drilSolVolPercent * 21.7m * 42m / 102m;

                out_DrillSolVol = drilSolVolPercent.ToString();
                out_DrillSol_BBL = drilSolBBL.ToString();

                //-----------------
                out_TotalSolid = (100 - water - oilLubricant - glycol).ToString();
                out_NaClwt = naclWt.ToString();
                out_BariteVol = baritVolPercent.ToString();
                out_Barite_BBL = baritBBL.ToString();
                out_HematitVol = hematitVolPercent.ToString();
                out_Hematit_BBL = hematitBBL.ToString();
                out_CalCarVol = calCarVolPercent.ToString();
                out_CalCar_BBL = calCarBBL.ToString();
                //-----------------
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
        //-----------------------------------------------------------------------------
    }
}
