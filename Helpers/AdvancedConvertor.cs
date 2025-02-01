using System;
using System.Collections.Generic;

using System.Text;

namespace DMR
{
    public class AdvancedConvertor
    {
        //returns:   true (success, result is converted value) , false(failure)
        public static bool ToInt(string val, ref int result)
        {
            try
            {
                result = int.Parse(val);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        //-------------------------------------------------------
        //returns:   true (success, result is converted value) , false(failure)
        public static bool ToBigint(string val, ref Int64 result)
        {
            try
            {
                result = Int64.Parse(val);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        //-------------------------------------------------------
        //returns:   true (success, result is converted value) , false(failure, shows error form)
        public static bool ToInt(string val, ref int result, string errText)
        {
            try
            {
                result = int.Parse(val);
                return true;
            }
            catch (Exception)
            {
                InformationManager.Set_Info(errText);
                return false;
            }
        }
        //-------------------------------------------------------
        //returns:   true (success, result is converted value) , false(failure)
        public static bool ToDecimal(string val, ref decimal result)
        {
            try
            {
                result = decimal.Parse(val);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        //-------------------------------------------------------
        //returns:   true (success, result is converted value) , false(failure, shows error form)
        public static bool ToDecimal(string val, ref decimal result, string errText)
        {
            try
            {
                result = decimal.Parse(val);
                return true;
            }
            catch (Exception)
            {
                InformationManager.Set_Info(errText);
                return false;
            }
        }
    }
}
