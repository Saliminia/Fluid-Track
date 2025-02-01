using System;
using System.Collections.Generic;

using System.Text;

namespace DMR
{
    public class Errors
    {
        //-------------------------------------------------------
        public string err = "";
        public string fullExceptionMessage = "";
        public string ErrorLocation = "";
        //-------------------------------------------------------
        public Errors() { }
        //-------------------------------------------------------
        public Errors(string errText) 
        {
            this.err = errText;
        }
        //-------------------------------------------------------
        public Errors(string errText, string exception)
        {
            this.err = errText;
            this.fullExceptionMessage = exception;
        }
        //-------------------------------------------------------
        public Errors(string errText, string exception, string loc)
        {
            this.err = errText;
            this.fullExceptionMessage = exception;
            this.ErrorLocation = loc;
        }
        //-------------------------------------------------------
    }
}
