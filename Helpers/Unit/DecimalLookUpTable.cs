using System;
using System.Collections.Generic;

using System.Text;

namespace DMR
{
    public class DecimalLookUpTable
    {
        static decimal epsilon = 0.001m;

        decimal[,] arr = null;
        //=======================================================
        public DecimalLookUpTable(decimal[,] input)
        {
            arr = input;
        }
        //-------------------------------------------------------
        public bool Contains(int searchCol, decimal valToSearch, int corrCol, ref decimal corrVal)
        {
            corrVal = 0;

            if (searchCol >= arr.GetLength(1) || corrCol >= arr.GetLength(1))
                return false;//out of range

            int rowCount = arr.GetLength(0);

            for (int i = 0; i < rowCount; i++)
                if (Math.Abs(arr[i, searchCol] - valToSearch) < epsilon)
                {
                    corrVal = arr[i, corrCol];
                    return true;
                }

            return false;
        }
        //-------------------------------------------------------       
    }
}
