using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DMR
{
    public class UnitConverter
    {

        static Dictionary<KeyValuePair<string, string>, decimal> toBaseFactor = new Dictionary<KeyValuePair<string, string>, decimal>();//for direct conversions
        static Dictionary<KeyValuePair<string, string>, decimal> fromBaseFactor = new Dictionary<KeyValuePair<string, string>, decimal>();//for direct conversions
        //=======================================================
        public static DecimalLookUpTable ltNaCl = new DecimalLookUpTable
        (
        new decimal[26, 3]  
            {
                //%﻿Wt 	  NaCl (mg/l)    Chlorides (mg/l) 
                 {1	        ,10070	       ,6108  }
                ,{2	        ,20286	       ,12305 }
                ,{3	        ,30630	       ,18580 }
                ,{4	        ,41144	       ,24957 }
                ,{5	        ,51800	       ,31421 }
                ,{6	        ,62586	       ,37963 }
                ,{7	        ,73500	       ,445584}
                ,{8	        ,84624	       ,51331 }
                ,{9	        ,95850	       ,58141 }
                ,{10	    ,107260	       ,65062 }
                ,{11	    ,118800	       ,72062 }
                ,{12	    ,130512	       ,79166 }
                ,{13	    ,142350	       ,86348 }
                ,{14	    ,154392	       ,93651 }
                ,{15	    ,166650	       ,101087}
                ,{16	    ,178912	       ,108524}
                ,{17	    ,191420	       ,116112}
                ,{18	    ,204102	       ,123804}
                ,{19	    ,216980	       ,131616}
                ,{20	    ,229960	       ,139489}
                ,{21	    ,243180	       ,147508}
                ,{22	    ,256520	       ,155600}
                ,{23	    ,270020	       ,163789}
                ,{24	    ,283800	       ,172147}
                ,{25	    ,297750	       ,180609}
                ,{26	    ,311818	       ,189143}
            }
        );
        //-------------------------------------------------------
        public static DecimalLookUpTable ltKCl = new DecimalLookUpTable
        (
        new decimal[24, 3]  
            {
                //%﻿Wt 	   KCl (mg/l)    Chlorides (mg/l) 
                 {1	        ,10000	       ,4756  }
                ,{2	        ,20200	       ,9606  }
                ,{3	        ,30500	       ,14506 }
                ,{4	        ,41000	       ,19499 }
                ,{5	        ,51500	       ,24493 }
                ,{6	        ,62200	       ,29582 }
                ,{7	        ,73000	       ,34718 }
                ,{8	        ,84000	       ,39950 }
                ,{9	        ,95100	       ,45229 }
                ,{10	    ,106300	       ,50556 }
                ,{11	    ,117700	       ,55977 }
                ,{12	    ,129200	       ,61447 }
                ,{13	    ,140900	       ,67011 }
                ,{14	    ,152700	       ,72623 }
                ,{15	    ,164600	       ,78282 }
                ,{16	    ,176700	       ,84038 }
                ,{17	    ,188900	       ,89840 }
                ,{18	    ,201300	       ,95737 }
                ,{19	    ,213900	       ,101730}
                ,{20	    ,226600	       ,107770}
                ,{21	    ,239500	       ,114000}
                ,{22	    ,252400	       ,120040}
                ,{23	    ,265700	       ,126473}
                ,{24	    ,278900	       ,132643}
            }
        );
        //=======================================================
        public static void InitDirectFactors()
        {
            //================ Depth =================
            //Units: ft, m   
            //Base Unit: m
            toBaseFactor.Add(new KeyValuePair<string, string>("Depth", "ft"), 0.3048m);//note: the m prefix of numbers stands for decimal , nothing related to 'meter' unit
            fromBaseFactor.Add(new KeyValuePair<string, string>("Depth", "ft"), 3.28084m);

            toBaseFactor.Add(new KeyValuePair<string, string>("Depth", "m"), 1m);
            fromBaseFactor.Add(new KeyValuePair<string, string>("Depth", "m"), 1m);

            //================ Funnel Viscosity =================
            //Units: sec/qt, sec/l
            //Base Unit: sec/qt
            toBaseFactor.Add(new KeyValuePair<string, string>("Funnel Viscosity", "sec/l"), 0.946353m);
            fromBaseFactor.Add(new KeyValuePair<string, string>("Funnel Viscosity", "sec/l"), 1.05669m);

            toBaseFactor.Add(new KeyValuePair<string, string>("Funnel Viscosity", "sec/qt"), 1m);
            fromBaseFactor.Add(new KeyValuePair<string, string>("Funnel Viscosity", "sec/qt"), 1m);

            //================ Plastic Viscosity =================
            //Units: cP, mPa.s
            //Base Unit:  cP
            toBaseFactor.Add(new KeyValuePair<string, string>("Plastic Viscosity", "mPa.s"), 1m);
            fromBaseFactor.Add(new KeyValuePair<string, string>("Plastic Viscosity", "mPa.s"), 1m);

            toBaseFactor.Add(new KeyValuePair<string, string>("Plastic Viscosity", "cP"), 1m);
            fromBaseFactor.Add(new KeyValuePair<string, string>("Plastic Viscosity", "cP"), 1m);

            //================ Mud Weight =================
            //Units: SG, ppg, pcf, kg/m³
            //Base Unit:  SG 
            toBaseFactor.Add(new KeyValuePair<string, string>("Mud Weight", "SG"), 1m);
            fromBaseFactor.Add(new KeyValuePair<string, string>("Mud Weight", "SG"), 1m);

            toBaseFactor.Add(new KeyValuePair<string, string>("Mud Weight", "ppg"), 0.119826m);
            fromBaseFactor.Add(new KeyValuePair<string, string>("Mud Weight", "ppg"), 8.345404m);

            toBaseFactor.Add(new KeyValuePair<string, string>("Mud Weight", "pcf"), 0.01601846m);
            fromBaseFactor.Add(new KeyValuePair<string, string>("Mud Weight", "pcf"), 62.427961m);

            toBaseFactor.Add(new KeyValuePair<string, string>("Mud Weight", "kg/m³"), 0.001m);
            fromBaseFactor.Add(new KeyValuePair<string, string>("Mud Weight", "kg/m³"), 1000m);

            //================ Weight =================
            //Units: kg, lbm
            //Base Unit: lbm
            toBaseFactor.Add(new KeyValuePair<string, string>("Weight", "lbm"), 1m);
            fromBaseFactor.Add(new KeyValuePair<string, string>("Weight", "lbm"), 1m);

            toBaseFactor.Add(new KeyValuePair<string, string>("Weight", "kg"), 2.20462m);
            fromBaseFactor.Add(new KeyValuePair<string, string>("Weight", "kg"), 0.453592m);

            //================ Hole,Bit Size =================
            //Units: in, mm
            //Base Unit: in
            toBaseFactor.Add(new KeyValuePair<string, string>("Hole,Bit Size", "in"), 1m);
            fromBaseFactor.Add(new KeyValuePair<string, string>("Hole,Bit Size", "in"), 1m);

            toBaseFactor.Add(new KeyValuePair<string, string>("Hole,Bit Size", "mm"), 0.0393701m);
            fromBaseFactor.Add(new KeyValuePair<string, string>("Hole,Bit Size", "mm"), 25.4m);

            //================ Liquid Volume =================
            //Units: bbl, m³
            //Base Unit: bbl
            toBaseFactor.Add(new KeyValuePair<string, string>("Liquid Volume", "bbl"), 1m);
            fromBaseFactor.Add(new KeyValuePair<string, string>("Liquid Volume", "bbl"), 1m);

            toBaseFactor.Add(new KeyValuePair<string, string>("Liquid Volume", "m³"), 6.2898m);
            fromBaseFactor.Add(new KeyValuePair<string, string>("Liquid Volume", "m³"), 0.1589873m);

            //================ Nozzle Size =================
            //Units: mm, 1/32 in
            //Base Unit: 1/32 in
            toBaseFactor.Add(new KeyValuePair<string, string>("Nozzle Size", "1/32 in"), 1m);
            fromBaseFactor.Add(new KeyValuePair<string, string>("Nozzle Size", "1/32 in"), 1m);

            toBaseFactor.Add(new KeyValuePair<string, string>("Nozzle Size", "mm"), 1.259763m);
            fromBaseFactor.Add(new KeyValuePair<string, string>("Nozzle Size", "mm"), 0.7938m);

            //================ Flow Rate =================
            //Units: m³/min, bbl/min, gal/min
            //Base Unit: gal/min
            toBaseFactor.Add(new KeyValuePair<string, string>("Flow Rate", "m³/min"), 264.172m);
            fromBaseFactor.Add(new KeyValuePair<string, string>("Flow Rate", "m³/min"), 0.00378541m);

            toBaseFactor.Add(new KeyValuePair<string, string>("Flow Rate", "bbl/min"), 42m);
            fromBaseFactor.Add(new KeyValuePair<string, string>("Flow Rate", "bbl/min"), 0.02381m);

            toBaseFactor.Add(new KeyValuePair<string, string>("Flow Rate", "gal/min"), 1m);
            fromBaseFactor.Add(new KeyValuePair<string, string>("Flow Rate", "gal/min"), 1m);

            //================ Nozzle Velocity =================
            //Units: ft/sec, m/sec
            //Base Unit: ft/sec
            toBaseFactor.Add(new KeyValuePair<string, string>("Nozzle Velocity", "m/sec"), 3.28084m);
            fromBaseFactor.Add(new KeyValuePair<string, string>("Nozzle Velocity", "m/sec"), 0.3048m);

            toBaseFactor.Add(new KeyValuePair<string, string>("Nozzle Velocity", "ft/sec"), 1m);
            fromBaseFactor.Add(new KeyValuePair<string, string>("Nozzle Velocity", "ft/sec"), 1m);

            //================ Pressure =================
            //Units: kPa, MPa, psi
            //Base Unit: psi
            toBaseFactor.Add(new KeyValuePair<string, string>("Pressure", "kPa"), 0.145038m);
            fromBaseFactor.Add(new KeyValuePair<string, string>("Pressure", "kPa"), 6.89476m);

            toBaseFactor.Add(new KeyValuePair<string, string>("Pressure", "MPa"), 145.038m);
            fromBaseFactor.Add(new KeyValuePair<string, string>("Pressure", "MPa"), 0.00689476m);

            toBaseFactor.Add(new KeyValuePair<string, string>("Pressure", "psi"), 1m);
            fromBaseFactor.Add(new KeyValuePair<string, string>("Pressure", "psi"), 1m);

            //================ Velocity =================
            //Units: m/min, ft/min
            //Base Unit: ft/min
            toBaseFactor.Add(new KeyValuePair<string, string>("Velocity", "m/min"), 3.28084m);
            fromBaseFactor.Add(new KeyValuePair<string, string>("Velocity", "m/min"), 0.3048m);

            toBaseFactor.Add(new KeyValuePair<string, string>("Velocity", "ft/min"), 1m);
            fromBaseFactor.Add(new KeyValuePair<string, string>("Velocity", "ft/min"), 1m);

            toBaseFactor.Add(new KeyValuePair<string, string>("Velocity", "ft/sec"), 60m);
            fromBaseFactor.Add(new KeyValuePair<string, string>("Velocity", "ft/sec"), 0.0166666667m);


            //================ Weight on Bit (WOB) =================
            //Units: daN, lbf, klbf
            //Base Unit: klbf
            toBaseFactor.Add(new KeyValuePair<string, string>("Weight on Bit (WOB)", "daN"), 0.002248089m);
            fromBaseFactor.Add(new KeyValuePair<string, string>("Weight on Bit (WOB)", "daN"), 444.822m);

            toBaseFactor.Add(new KeyValuePair<string, string>("Weight on Bit (WOB)", "lbf"), 0.001m);
            fromBaseFactor.Add(new KeyValuePair<string, string>("Weight on Bit (WOB)", "lbf"), 1000m);

            toBaseFactor.Add(new KeyValuePair<string, string>("Weight on Bit (WOB)", "klbf"), 1m);
            fromBaseFactor.Add(new KeyValuePair<string, string>("Weight on Bit (WOB)", "klbf"), 1m);

            //================ ROP =================
            //Units: m/hr, ft/hr
            //Base Unit: m/hr
            toBaseFactor.Add(new KeyValuePair<string, string>("ROP", "m/hr"), 1m);
            fromBaseFactor.Add(new KeyValuePair<string, string>("ROP", "m/hr"), 1m);

            toBaseFactor.Add(new KeyValuePair<string, string>("ROP", "ft/hr"), 0.3048m);
            fromBaseFactor.Add(new KeyValuePair<string, string>("ROP", "ft/hr"), 3.28084m);

            //================ Torque =================
            //Units: N.m, lbf.ft, klbf.ft
            //Base Unit: klbf.ft
            toBaseFactor.Add(new KeyValuePair<string, string>("Torque", "N.m"), 0.000737562m);
            fromBaseFactor.Add(new KeyValuePair<string, string>("Torque", "N.m"), 1355.818m);

            toBaseFactor.Add(new KeyValuePair<string, string>("Torque", "lbf.ft"), 0.001m);
            fromBaseFactor.Add(new KeyValuePair<string, string>("Torque", "lbf.ft"), 1000m);

            toBaseFactor.Add(new KeyValuePair<string, string>("Torque", "klbf.ft"), 1m);
            fromBaseFactor.Add(new KeyValuePair<string, string>("Torque", "klbf.ft"), 1m);

            //================ Volume/Stroke =================
            //Units: m³/stk, bbl/stk, gal/stk
            //Base Unit: gal/stk
            toBaseFactor.Add(new KeyValuePair<string, string>("Volume/Stroke", "m³/stk"), 264.172m);
            fromBaseFactor.Add(new KeyValuePair<string, string>("Volume/Stroke", "m³/stk"), 0.00378541m);

            toBaseFactor.Add(new KeyValuePair<string, string>("Volume/Stroke", "bbl/stk"), 42m);
            fromBaseFactor.Add(new KeyValuePair<string, string>("Volume/Stroke", "bbl/stk"), 0.02381m);

            toBaseFactor.Add(new KeyValuePair<string, string>("Volume/Stroke", "gal/stk"), 1m);
            fromBaseFactor.Add(new KeyValuePair<string, string>("Volume/Stroke", "gal/stk"), 1m);

            //================ Yield Point and Gel Strength =================
            //Units: lb/100ft², Pa
            //Base Unit: lb/100ft²
            toBaseFactor.Add(new KeyValuePair<string, string>("Yield Point and Gel Strength", "lb/100ft²"), 1m);
            fromBaseFactor.Add(new KeyValuePair<string, string>("Yield Point and Gel Strength", "lb/100ft²"), 1m);

            toBaseFactor.Add(new KeyValuePair<string, string>("Yield Point and Gel Strength", "Pa"), 2.09205m);
            fromBaseFactor.Add(new KeyValuePair<string, string>("Yield Point and Gel Strength", "Pa"), 0.478m);

            //================ API,HPHT Fluid Loss =================
            //Units: cm³/30 min, cc/30 min, ml/30 min
            //Base Unit: cc/30 min
            toBaseFactor.Add(new KeyValuePair<string, string>("API,HPHT Fluid Loss", "cm³/30 min"), 1m);
            fromBaseFactor.Add(new KeyValuePair<string, string>("API,HPHT Fluid Loss", "cm³/30 min"), 1m);

            toBaseFactor.Add(new KeyValuePair<string, string>("API,HPHT Fluid Loss", "cc/30 min"), 1m);
            fromBaseFactor.Add(new KeyValuePair<string, string>("API,HPHT Fluid Loss", "cc/30 min"), 1m);

            toBaseFactor.Add(new KeyValuePair<string, string>("API,HPHT Fluid Loss", "ml/30 min"), 1m);
            fromBaseFactor.Add(new KeyValuePair<string, string>("API,HPHT Fluid Loss", "ml/30 min"), 1m);

            //================ Discharge,Loss Rate =================
            //Units: m³/hr, bbl/hr
            //Base Unit: bbl/hr
            toBaseFactor.Add(new KeyValuePair<string, string>("Discharge,Loss Rate", "m³/hr"), 6.2898m);
            fromBaseFactor.Add(new KeyValuePair<string, string>("Discharge,Loss Rate", "m³/hr"), 0.1589873m);

            toBaseFactor.Add(new KeyValuePair<string, string>("Discharge,Loss Rate", "bbl/hr"), 1m);
            fromBaseFactor.Add(new KeyValuePair<string, string>("Discharge,Loss Rate", "bbl/hr"), 1m);

            //================ Mud MBT ================
            //Units: lb/bbl, kg/m³
            //Base Unit: lb/bbl
            toBaseFactor.Add(new KeyValuePair<string, string>("Mud MBT", "lb/bbl"), 1m);
            fromBaseFactor.Add(new KeyValuePair<string, string>("Mud MBT", "lb/bbl"), 1m);

            toBaseFactor.Add(new KeyValuePair<string, string>("Mud MBT", "kg/m³"), 0.3505m);
            fromBaseFactor.Add(new KeyValuePair<string, string>("Mud MBT", "kg/m³"), 2.853m);

            //================ Filter Cake Thickness =================
            //Units: mm, 1/32 in
            //Base Unit: 1/32 in
            toBaseFactor.Add(new KeyValuePair<string, string>("Filter Cake Thickness", "mm"), 1.259763m);
            fromBaseFactor.Add(new KeyValuePair<string, string>("Filter Cake Thickness", "mm"), 0.7938m);

            toBaseFactor.Add(new KeyValuePair<string, string>("Filter Cake Thickness", "1/32 in"), 1m);
            fromBaseFactor.Add(new KeyValuePair<string, string>("Filter Cake Thickness", "1/32 in"), 1m);

            //================ Ionic Mass Concentration =================
            //Units: mg/l, ppm
            //Base Unit: mg/l
            toBaseFactor.Add(new KeyValuePair<string, string>("Ionic Mass Concentration", "mg/l"), 1m);
            fromBaseFactor.Add(new KeyValuePair<string, string>("Ionic Mass Concentration", "mg/l"), 1m);

            toBaseFactor.Add(new KeyValuePair<string, string>("Ionic Mass Concentration", "ppm"), 1m);
            fromBaseFactor.Add(new KeyValuePair<string, string>("Ionic Mass Concentration", "ppm"), 1m);

            //================ Powder Material Concentration =================
            //Units: lb/bbl, kg/m³
            //Base Unit: lb/bbl
            toBaseFactor.Add(new KeyValuePair<string, string>("Powder Material Concentration", "lb/bbl"), 1m);
            fromBaseFactor.Add(new KeyValuePair<string, string>("Powder Material Concentration", "lb/bbl"), 1m);

            toBaseFactor.Add(new KeyValuePair<string, string>("Powder Material Concentration", "kg/m³"), 0.3505m);
            fromBaseFactor.Add(new KeyValuePair<string, string>("Powder Material Concentration", "kg/m³"), 2.853m);

            //================ Liquid Material Concentration =================
            //Units: vol%, gal/bbl
            //Base Unit: vol%
            toBaseFactor.Add(new KeyValuePair<string, string>("Liquid Material Concentration", "vol%"), 1m);
            fromBaseFactor.Add(new KeyValuePair<string, string>("Liquid Material Concentration", "vol%"), 1m);

            toBaseFactor.Add(new KeyValuePair<string, string>("Liquid Material Concentration", "gal/bbl"), 0.42m);
            fromBaseFactor.Add(new KeyValuePair<string, string>("Liquid Material Concentration", "gal/bbl"), 2.380952381m);

            //================ Pm, Pf, Mf =================
            //Units: cm³, ml
            //Base Unit: ml
            toBaseFactor.Add(new KeyValuePair<string, string>("Pm, Pf, Mf", "cm³"), 1m);
            fromBaseFactor.Add(new KeyValuePair<string, string>("Pm, Pf, Mf", "cm³"), 1m);

            toBaseFactor.Add(new KeyValuePair<string, string>("Pm, Pf, Mf", "ml"), 1m);
            fromBaseFactor.Add(new KeyValuePair<string, string>("Pm, Pf, Mf", "ml"), 1m);

            //================ Pressure Gradient =================
            //Units: kPa/m, psi/ft
            //Base Unit: psi/ft
            toBaseFactor.Add(new KeyValuePair<string, string>("Pressure Gradient", "kPa/m"), 0.0442075m);
            fromBaseFactor.Add(new KeyValuePair<string, string>("Pressure Gradient", "kPa/m"), 22.6206m);

            toBaseFactor.Add(new KeyValuePair<string, string>("Pressure Gradient", "psi/ft"), 1m);
            fromBaseFactor.Add(new KeyValuePair<string, string>("Pressure Gradient", "psi/ft"), 1m);

            //================ Casing and Pipes Diameter =================
            //Units: in, mm
            //Base Unit: in
            toBaseFactor.Add(new KeyValuePair<string, string>("Casing and Pipes Diameter", "in"), 1m);
            fromBaseFactor.Add(new KeyValuePair<string, string>("Casing and Pipes Diameter", "in"), 1m);

            toBaseFactor.Add(new KeyValuePair<string, string>("Casing and Pipes Diameter", "mm"), 0.0393701m);
            fromBaseFactor.Add(new KeyValuePair<string, string>("Casing and Pipes Diameter", "mm"), 25.4m);

            //================ Liner Length and Diameter =================
            //Units: in, mm
            //Base Unit: in
            toBaseFactor.Add(new KeyValuePair<string, string>("Liner Length and Diameter", "in"), 1m);
            fromBaseFactor.Add(new KeyValuePair<string, string>("Liner Length and Diameter", "in"), 1m);

            toBaseFactor.Add(new KeyValuePair<string, string>("Liner Length and Diameter", "mm"), 0.0393701m);
            fromBaseFactor.Add(new KeyValuePair<string, string>("Liner Length and Diameter", "mm"), 25.4m);

            //================ Mud Pits Capacity and Dead Vol. =================
            //Units: m³, bbl
            //Base Unit: bbl
            toBaseFactor.Add(new KeyValuePair<string, string>("Mud Pits Capacity and Dead Vol.", "m³"), 6.2898m);
            fromBaseFactor.Add(new KeyValuePair<string, string>("Mud Pits Capacity and Dead Vol.", "m³"), 0.1589873m);

            toBaseFactor.Add(new KeyValuePair<string, string>("Mud Pits Capacity and Dead Vol.", "bbl"), 1m);
            fromBaseFactor.Add(new KeyValuePair<string, string>("Mud Pits Capacity and Dead Vol.", "bbl"), 1m);

            //================ Bulk System Capacitty =================
            //Units: lbm, ton
            //Base Unit: ton
            toBaseFactor.Add(new KeyValuePair<string, string>("Bulk System Capacitty", "lbm"), 0.0005m);
            fromBaseFactor.Add(new KeyValuePair<string, string>("Bulk System Capacitty", "lbm"), 2000m);

            toBaseFactor.Add(new KeyValuePair<string, string>("Bulk System Capacitty", "ton"), 1m);
            fromBaseFactor.Add(new KeyValuePair<string, string>("Bulk System Capacitty", "ton"), 1m);

            //================ Power =================
            //Units: kW, hp
            //Base Unit: hp
            toBaseFactor.Add(new KeyValuePair<string, string>("Power", "kW"), 1.3410220888m);
            fromBaseFactor.Add(new KeyValuePair<string, string>("Power", "kW"), 0.7457m);

            toBaseFactor.Add(new KeyValuePair<string, string>("Power", "hp"), 1m);
            fromBaseFactor.Add(new KeyValuePair<string, string>("Power", "hp"), 1m);

            //================ Force =================
            //Units: N, lbf
            //Base Unit: lbf
            toBaseFactor.Add(new KeyValuePair<string, string>("Force", "N"), 0.224809m);
            fromBaseFactor.Add(new KeyValuePair<string, string>("Force", "N"), 4.44822m);

            toBaseFactor.Add(new KeyValuePair<string, string>("Force", "lbf"), 1m);
            fromBaseFactor.Add(new KeyValuePair<string, string>("Force", "lbf"), 1m);

            ////================ KCl Concentration (direct conversion part) =================
            //Units: kg/m³, lb/bbl, mg/l
            //Base Unit: lb/bbl
            toBaseFactor.Add(new KeyValuePair<string, string>("KCl Concentration", "kg/m³"), 0.350508m);
            fromBaseFactor.Add(new KeyValuePair<string, string>("KCl Concentration", "kg/m³"), 2.853m);

            toBaseFactor.Add(new KeyValuePair<string, string>("KCl Concentration", "lb/bbl"), 1m);
            fromBaseFactor.Add(new KeyValuePair<string, string>("KCl Concentration", "lb/bbl"), 1m);

            toBaseFactor.Add(new KeyValuePair<string, string>("KCl Concentration", "mg/l"), 0.000350508m);
            fromBaseFactor.Add(new KeyValuePair<string, string>("KCl Concentration", "mg/l"), 2853m);

            ////================ NaCl Concentration (direct conversion part) =================
            //Units: kg/m³, lb/bbl, mg/l
            //Base Unit: lb/bbl
            toBaseFactor.Add(new KeyValuePair<string, string>("NaCl Concentration", "kg/m³"), 0.350508m);
            fromBaseFactor.Add(new KeyValuePair<string, string>("NaCl Concentration", "kg/m³"), 2.853m);

            toBaseFactor.Add(new KeyValuePair<string, string>("NaCl Concentration", "lb/bbl"), 1m);
            fromBaseFactor.Add(new KeyValuePair<string, string>("NaCl Concentration", "lb/bbl"), 1m);

            toBaseFactor.Add(new KeyValuePair<string, string>("NaCl Concentration", "mg/l"), 0.000350508m);
            fromBaseFactor.Add(new KeyValuePair<string, string>("NaCl Concentration", "mg/l"), 2853m);
        }
        //-------------------------------------------------------
        public static string Convert(string group, string fromUnit, string toUnit, string valStr)
        {
            try
            {
                decimal val = 0;

                if (!AdvancedConvertor.ToDecimal(valStr, ref val))
                    return "Invalid Input";

                if (fromUnit == toUnit)
                    return val.ToString(/*"0.0000000000"*/);

                decimal factorToBase = 0;
                decimal factorFromBase = 0;

                if (toBaseFactor.TryGetValue(new KeyValuePair<string, string>(group, fromUnit), out factorToBase) &&
                    fromBaseFactor.TryGetValue(new KeyValuePair<string, string>(group, toUnit), out factorFromBase))
                    return (val * factorToBase * factorFromBase).ToString(/*"0.0000000000"*/);
                else
                    return NonDirectConvert(group, fromUnit, toUnit, valStr);//try non-direct conversions
            }
            catch (Exception)
            {
                return "Conversion Error";
            }
        }
        //-------------------------------------------------------
        private static string NonDirectConvert(string group, string fromUnit, string toUnit, string valStr)
        {
            toUnit = toUnit.Trim();
            fromUnit = fromUnit.Trim();

            decimal val = 0;

            if (!AdvancedConvertor.ToDecimal(valStr, ref val))
                return "Invalid Input";

            if (fromUnit == toUnit)
                return val.ToString(/*"0.0000000000"*/);

            if (group == "Temperature")//~~~~~~~~~~~~~~~~~~~~~~~~
            {
                if (fromUnit == "°C")
                {
                    if (toUnit == "°F")
                        return ((val * 1.8m) + 32m).ToString(/*"0.0000000000"*/);

                }
                else if (fromUnit == "°F")
                {
                    if (toUnit == "°C")
                        return ((val - 32m) / 1.8m).ToString(/*"0.0000000000"*/);
                }
            }
            else if (group == "KCl Concentration")//~~~~~~~~~~~~~~~~~~~~~~~~
            {
                if (fromUnit == "wt%")
                {
                    //first convert wt% to mg/l
                    decimal mgl = 0;

                    if (!ltKCl.Contains(0, val, 1, ref mgl))//is it contained in lookup table ? if yes, get it
                        mgl = (10005.6m * val) / (1 - (5.85m * (0.001m) * val));//if no, compute it

                    if (toUnit == "mg/l")
                        return mgl.ToString(/*"0.0000000000"*/);
                    else
                        return Convert(group, "mg/l", toUnit, mgl.ToString());
                }


                if (toUnit == "wt%")
                {
                    //first convert to mg/l
                    decimal mgl = 0;
                    string mglStr = Convert(group, fromUnit, "mg/l", valStr);

                    if (AdvancedConvertor.ToDecimal(mglStr, ref mgl))
                    {
                        decimal wt = 0;

                        //Now Convert mgl to wt%
                        if (!ltKCl.Contains(1, mgl, 0, ref wt))//is it contained in lookup table ? if yes, get it
                            wt = mgl / (10005.6m + (mgl * 0.00585m));//if no, compute it

                        return wt.ToString(/*"0.0000000000"*/);
                    }
                }
            }
            else if (group == "NaCl Concentration")//~~~~~~~~~~~~~~~~~~~~~~~~
            {
                if (fromUnit == "wt%")
                {
                    //first convert wt% to mg/l
                    decimal mgl = 0;

                    if (ltNaCl.Contains(0, val, 1, ref mgl))//is it contained in lookup table ? if yes, get it
                    {
                        if (toUnit == "mg/l")
                            return mgl.ToString(/*"0.0000000000"*/);
                        else
                            return Convert(group, "mg/l", toUnit, mgl.ToString());
                    }
                    //if no, we can not compute it; no formula!
                }


                if (toUnit == "wt%")
                {
                    //first convert to mg/l
                    decimal mgl = 0;
                    string mglStr = Convert(group, fromUnit, "mg/l", valStr);

                    if (AdvancedConvertor.ToDecimal(mglStr, ref mgl))
                    {
                        decimal wt = 0;

                        //Now Convert mgl to wt%
                        if (!ltNaCl.Contains(1, mgl, 0, ref wt))//is it contained in lookup table ? if yes, get it
                            wt = (mgl * 0.6066m * 1.65m) / (((1 + (1.94m * (0.000001m) * (decimal)Math.Pow(((double)mgl * 0.6066), 0.95)))) * 10000);//if no, compute it

                        return wt.ToString(/*"0.0000000000"*/);
                    }
                }
            }

            return "Undefined Conversion";
        }
        //-------------------------------------------------------
        public static KeyValuePair<string, string>[] GetDirectGroupsAndUnits()
        {
            return toBaseFactor.Keys.ToArray();
        }
        //-------------------------------------------------------
        public static KeyValuePair<string, string>[] GetNonDirectGroupsAndUnits()
        {
            KeyValuePair<string, string>[] result = new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("Temperature", "°C"),
                new KeyValuePair<string, string>("Temperature", "°F"),
                new KeyValuePair<string, string>("KCl Concentration", "wt%"),
                new KeyValuePair<string, string>("NaCl Concentration", "wt%"),
            };

            return result;
        }
        //-------------------------------------------------------

    }
    //#################################################################
    public class EasyConvert//assuming conversion can be done
    {
        public static decimal LiquidVolume_FromBBL(string toUnit, decimal val)
        {
            return Convert.ToDecimal(UnitConverter.Convert("Liquid Volume", "bbl", toUnit, val.ToString()));
        }
        //-------------------------------------------------------
        public static decimal Depth_ToFoot(string fromUnit, decimal val)
        {
            return Convert.ToDecimal(UnitConverter.Convert("Depth", fromUnit, "ft", val.ToString()));
        }
        //-------------------------------------------------------
        public static decimal Depth_ToFoot(string fromUnit, string val)
        {
            return Convert.ToDecimal(UnitConverter.Convert("Depth", fromUnit, "ft", val));
        }
        //-------------------------------------------------------

    }
    //#################################################################
    public class UnitString
    {
        //-------------------------------------------------------
        public static void WriteUnit(Control ctrl, string unit)
        {
            string text = ctrl.Text;

            int endID = -1, startID = -1;

            startID = text.IndexOf('(');
            endID = text.IndexOf(')');

            if (startID == -1 || endID == -1 || endID - startID == 1)
                return;

            ctrl.Text = text.Substring(0, startID) + "(" + unit + ")" + text.Substring(endID + 1);
        }
        //-------------------------------------------------------
        public static void WriteUnit(DataGridViewColumn col, string unit)
        {
            string text = col.HeaderText;

            int endID = -1, startID = -1;

            startID = text.IndexOf('(');
            endID = text.IndexOf(')');

            if (startID == -1 || endID == -1 || endID - startID == 1)
                return;

            col.HeaderText = text.Substring(0, startID) + "(" + unit + ")" + text.Substring(endID + 1);
        }
        //-------------------------------------------------------
        public static void WriteUnit(DataGridViewRow row, string unit)
        {
            string text = row.HeaderCell.Value.ToString();

            int endID = -1, startID = -1;

            startID = text.IndexOf('(');
            endID = text.IndexOf(')');

            if (startID == -1 || endID == -1 || endID - startID == 1)
                return;

            row.HeaderCell.Value = text.Substring(0, startID) + "(" + unit + ")" + text.Substring(endID + 1);
        }

    }
    //#################################################################
}
