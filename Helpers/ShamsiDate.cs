using System;
using System.Collections.Generic;
using System.Globalization;

using System.Text;

namespace DMR
{
    public class ShamsiDate
    {
        //-------------------------------------------------------
        public static string ShamsiEquivalent(DateTime dt)
        {
            PersianCalendar pc = new PersianCalendar();

            return  pc.GetYear(dt).ToString() + "/"
                    + pc.GetMonth(dt).ToString() + "/"
                    + pc.GetDayOfMonth(dt).ToString();
        }
        //-------------------------------------------------------
    }
}
