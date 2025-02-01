using System;
using System.Collections.Generic;

using System.Text;

namespace DMR.Helpers.Computation
{
    class MudProperties
    {
        //=================================================
        public string in_selUnitVol;
        public string in_selUnitPlasticVis;
        public string in_selUnitYieldPoint;
        public string in_selUnitIonic;
        public string in_selUnitKClCon;
        public string in_selUnitNaClCon;
        public string in_selUnitPmPfMf;
        public string in_selUnitPowderMatConc;
        //=================================================
        public decimal in_R600 = 0;
        public decimal in_R300 = 0;
        public decimal in_KClwt = 0;
        public decimal in_TotalCh = 0;
        public decimal in_TotalH = 0;
        public decimal in_Capp = 0;
        public decimal in_Pf = 0;
        public decimal in_Mf = 0;
        public decimal in_Pm = 0;
        public decimal in_water = 0;
        //=================================================
        public string out_PV;
        public string out_YP;
        public string out_KCl;
        public string out_KClCh;
        public string out_NaClCh;
        public string out_NaCl;
        public string out_Mgpp;
        public string out_Bicabonate;
        public string out_Carbonate;
        public string out_Hydroxyl;
        public string out_LimeLBL;
        //=================================================
        //-----------------------------------------------------------------------------
        //returns true on success, else false
        public bool Compute()
        {
            try
            {
                //~~~~~~~~~~~~~~
                out_PV = "!!!";
                out_YP = "!!!";
                out_KCl = "!!!";
                out_KClCh = "!!!";
                out_NaClCh = "!!!";
                out_NaCl = "!!!";
                out_Mgpp = "!!!";
                out_Bicabonate = "!!!";
                out_Carbonate = "!!!";
                out_Hydroxyl = "!!!";
                out_LimeLBL = "!!!";
                //~~~~~~~~~~~~~~
                //PV 
                out_PV = UnitConverter.Convert("Plastic Viscosity", "cP", in_selUnitPlasticVis, (in_R600 - in_R300).ToString());

                //YP
                out_YP = UnitConverter.Convert("Yield Point and Gel Strength", "lb/100ft²", in_selUnitYieldPoint, (2 * in_R300 - in_R600).ToString());
                //~~~~~~~~~~~~~~
                decimal KCl = 0, KClCh = 0, NaCl = 0, NaClCh = 0;//  all in mg/l
                AdvancedConvertor.ToDecimal(UnitConverter.Convert("KCl Concentration", "wt%", "mg/l", in_KClwt.ToString()), ref KCl);

                out_KCl = UnitConverter.Convert("KCl Concentration", "mg/l", in_selUnitKClCon, KCl.ToString());

                KClCh = KCl * 0.476m;
                out_KClCh = UnitConverter.Convert("Ionic Mass Concentration", "mg/l", in_selUnitIonic, KClCh.ToString());

                decimal TotalCh = 0;
                AdvancedConvertor.ToDecimal(UnitConverter.Convert("Ionic Mass Concentration", in_selUnitIonic, "mg/l", in_TotalCh.ToString()), ref TotalCh);

                NaClCh = TotalCh - KClCh;
                out_NaClCh = UnitConverter.Convert("Ionic Mass Concentration", "mg/l", in_selUnitIonic, NaClCh.ToString());

                NaCl = NaClCh * 1.65m;
                out_NaCl = UnitConverter.Convert("NaCl Concentration", "mg/l", in_selUnitNaClCon, NaCl.ToString());
                //~~~~~~~~~~~~~~
                decimal TotalH = 0, Capp = 0;
                AdvancedConvertor.ToDecimal(UnitConverter.Convert("Ionic Mass Concentration", in_selUnitIonic, "mg/l", in_TotalH.ToString()), ref TotalH);
                AdvancedConvertor.ToDecimal(UnitConverter.Convert("Ionic Mass Concentration", in_selUnitIonic, "mg/l", in_Capp.ToString()), ref Capp);


                decimal Mgpp = (TotalH - Capp) * (243.0m / 400);//  in mg/l
                out_Mgpp = UnitConverter.Convert("Ionic Mass Concentration", "mg/l", in_selUnitIonic, Mgpp.ToString());
                //~~~~~~~~~~
                decimal Pf = 0, Mf = 0;
                AdvancedConvertor.ToDecimal(UnitConverter.Convert("Pm, Pf, Mf", in_selUnitPmPfMf, "ml", in_Pf.ToString()), ref Pf);
                AdvancedConvertor.ToDecimal(UnitConverter.Convert("Pm, Pf, Mf", in_selUnitPmPfMf, "ml", in_Mf.ToString()), ref Mf);

                decimal epsilon = 0.0001m;
                //-------------
                decimal Bicabonate = -1;
                if (Math.Abs(Pf) < epsilon)
                    Bicabonate = 1220 * Mf;
                else if (Math.Abs(Pf - Mf) < epsilon || Math.Abs(2 * Pf - Mf) < epsilon || 2 * Pf > Mf)
                    Bicabonate = 0;
                else if (2 * Pf < Mf)
                    Bicabonate = 1200 * (Mf - (2 * Pf));

                out_Bicabonate = UnitConverter.Convert("Ionic Mass Concentration", "mg/l", in_selUnitIonic, Bicabonate.ToString());
                //-------------
                decimal Carbonate = -1;
                if (Math.Abs(Pf) < epsilon || Math.Abs(Pf - Mf) < epsilon)
                    Carbonate = 0;
                else if (Math.Abs(2 * Pf - Mf) < epsilon)
                    Carbonate = 1200 * Pf;
                else if (2 * Pf > Mf)
                    Carbonate = 1200 * (Mf - Pf);
                else if (2 * Pf < Mf)
                    Carbonate = 1200 * Pf;

                out_Carbonate = UnitConverter.Convert("Ionic Mass Concentration", "mg/l", in_selUnitIonic, Carbonate.ToString());
                //-------------
                decimal Hydroxyl = -1;
                if (Math.Abs(Pf) < epsilon || Math.Abs(2 * Pf - Mf) < epsilon || 2 * Pf < Mf)
                    Hydroxyl = 0;
                else if (Math.Abs(Pf - Mf) < epsilon)
                    Hydroxyl = 340 * Mf;
                else if (2 * Pf > Mf)
                    Hydroxyl = 340 * (2 * Pf - Mf);

                out_Hydroxyl = UnitConverter.Convert("Ionic Mass Concentration", "mg/l", in_selUnitIonic, Hydroxyl.ToString());
                //-------------
                decimal Pm = 0;
                AdvancedConvertor.ToDecimal(UnitConverter.Convert("Pm, Pf, Mf", in_selUnitPmPfMf, "ml", in_Pm.ToString()), ref Pm);

                decimal Fw = in_water / 100;
                decimal LimeLBL = 0.26m * (Pm - (Fw * Pf));

                out_LimeLBL = UnitConverter.Convert("Powder Material Concentration", "lb/bbl", in_selUnitPowderMatConc, LimeLBL.ToString());
                //~~~~~~~~~~
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
