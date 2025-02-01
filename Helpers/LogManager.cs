using System;
using System.Collections.Generic;

using System.Text;
using System.IO;

namespace DMR
{
    class LogManager
    {

        private static string log;

        //=======================================================
        public static string GetLog()
        {
            return log;
        }
        //-------------------------------------------------------
        public static void WriteLogToFile(string fileName, bool append)
        {
            try
            {
                StreamWriter sw = new StreamWriter(fileName, append);

                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine("*****Writing Log to File , Time : " + DateTime.Now.ToString() + "*****");
                sw.WriteLine();

                sw.Write(log);

                sw.Close();
            }
            catch (Exception e)
            {
                InformationManager.Set_Info(new Errors("Logging error (save)", e.Message, "Class(LogManager):Func(WriteLogToFile)"));
            }
        }
        //-------------------------------------------------------
        public static void ClearLog()
        {
            log = "========Begin , Time:" + DateTime.Now.ToString() + "=========\n\n";
        }
        //-------------------------------------------------------
        public static void AddLogRecord(string record)
        {
            try
            {
                log += "\n" + DateTime.Now.ToString() + " , " + record;
            }
            catch (Exception e)
            {
                InformationManager.Set_Info(new Errors("Logging error", e.Message, "Class(LogManager):Func(AddLogRecord)"));
            }
        }
        //-------------------------------------------------------

    }
}
