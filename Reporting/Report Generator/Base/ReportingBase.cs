using System;
using System.Collections.Generic;

using System.Text;

namespace DMR.Reporting.Report_Generator
{
    class ReportingBase
    {
        public Dictionary<string, object> parameters = new Dictionary<string, object>();
        public string outDir = "";//where report file must be saved
        //=======================================================
        //checks parameters
        //returns true if parameters are enough and valid, else returns false
        public virtual bool CheckParameters()
        {
            return true;
        }
        //-------------------------------------------------------
        //generates and returns report file name according to parameters and report type
        public virtual string GetReportFilename()
        {
            return "";
        }
        //-------------------------------------------------------
        public T GetParam<T>(string key)
        {
            object objVal = null;
            if (parameters.TryGetValue(key, out objVal))
                return (T)objVal;

            throw new Exception();// "Parameter Not Found";
        }
        //-------------------------------------------------------
        //generates report
        //returns "" on success, else returns error
        public virtual string GenerateReport()
        {
            return "";
        }
        //-------------------------------------------------------
    }
}
