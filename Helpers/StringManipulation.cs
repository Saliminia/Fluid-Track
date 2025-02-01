using System;
using System.Collections.Generic;

using System.Text;

namespace DMR
{
    public class StringManipulation
    {
        //-------------------------------------------------------
        public static string ReplaceBadNameChars(string input)
        {
            input = input.Replace('\'', '_');//for sql


            input = input.Replace('/', '_');//for window file name 
            input = input.Replace('\\', '_');//for window file name 
            input = input.Replace(':', '_');//for window file name 
            input = input.Replace('*', '_');//for window file name  
            input = input.Replace('?', '_');//for window file name  
            input = input.Replace('\"', '_');//for window file name  
            input = input.Replace('<', '_');//for window file name  
            input = input.Replace('>', '_');//for window file name  
            input = input.Replace('|', '_');//for window file name 

            return input;
        }
    }
}
