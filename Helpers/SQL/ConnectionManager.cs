using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.IO;

namespace DMR
{
    public class ConnectionManager
    {
        public static SqlConnection sqlConnction;
        public static SqlDataAdapter dataAdapter;
        //=======================================================

        public static void Init()
        {
            //            //------------------------
            //            string serverAddress;
            //            string mdfAddress;
            //            string fileName = "_ads";

            //            if (File.Exists(fileName))
            //            {
            //                try
            //                {
            //                    StreamReader sr = new StreamReader(fileName);
            //                    serverAddress = sr.ReadLine();
            //                    mdfAddress = sr.ReadLine();
            //                    sr.Close();
            //                }
            //                catch (Exception e)
            //                {
            //                    serverAddress = ".\\MSSQLSERVER2012";

            //#if  OFFICE_EDITION
            //                    mdfAddress = "|DataDirectory|PDF_DB_Office_v001.000.mdf";
            //#else
            //                    mdfAddress = "|DataDirectory|PDF_DB_Site_v001.000.mdf";
            //#endif
            //                }
            //            }
            //            else
            //            {
            //                serverAddress = ".\\MSSQLSERVER2012";

            //#if  OFFICE_EDITION
            //                mdfAddress = "|DataDirectory|PDF_DB_Office_v001.000.mdf"; 
            //#else
            //                mdfAddress = "|DataDirectory|PDF_DB_Site_v001.000.mdf";
            //#endif
            //            }
            //            //------------------------ 


            //==========================2012==========================   
#if DEBUG && OFFICE_EDITION     //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
            sqlConnction = new SqlConnection("server=.\\MSSQLSERVER2012; database=PDF_DB_Office_v001.000;Trusted_Connection=Yes");
            //sqlConnction = new SqlConnection("server=.; database=PDF_DB_Office_v001.000;Trusted_Connection=Yes");
#elif DEBUG && !OFFICE_EDITION  //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
            sqlConnction = new SqlConnection("server=.\\MSSQLSERVER2012; database=PDF_DB_Site_v001.000;Trusted_Connection=Yes");
            //sqlConnction = new SqlConnection("server=.; database=ForTest_PDF_DB_Site_v001.000;Trusted_Connection=Yes");


#elif !DEBUG && OFFICE_EDITION  //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
            sqlConnction = new SqlConnection("Data Source=.\\MSSQLSERVER2012;AttachDbFilename=|DataDirectory|PDF_DB_Office_v001.000.mdf;Integrated Security=True;Connect Timeout=10;User Instance=True");
            //sqlConnction = new SqlConnection("Data Source=.;AttachDbFilename=|DataDirectory|PDF_DB_Office_v001.000.mdf;Integrated Security=True;Connect Timeout=10;User Instance=True");
#elif !DEBUG && !OFFICE_EDITION //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
            sqlConnction = new SqlConnection("Data Source=.\\MSSQLSERVER2012;AttachDbFilename=|DataDirectory|PDF_DB_Site_v001.000.mdf;Integrated Security=True;Connect Timeout=10;User Instance=True");
            //sqlConnction = new SqlConnection("Data Source=.;AttachDbFilename=|DataDirectory|PDF_DB_Site_v001.000.mdf;Integrated Security=True;Connect Timeout=10;User Instance=True");
#endif                          //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
            //====================================================


            dataAdapter = new SqlDataAdapter("--------", sqlConnction);

        }
        //-------------------------------------------------------
        public static void Dispose()
        {
            dataAdapter.Dispose();
            sqlConnction.Dispose();
        }
        //-------------------------------------------------------
        public static void BeginCallStoredProcedure(string procName)
        {
            dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            dataAdapter.SelectCommand.CommandText = procName;
            dataAdapter.SelectCommand.Parameters.Clear();
        }
        //-------------------------------------------------------
        public static void EndCallStoredProcedure()
        {
            dataAdapter.SelectCommand.Connection.Close();
            dataAdapter.SelectCommand.CommandType = CommandType.Text;
            dataAdapter.SelectCommand.CommandText = "";
            dataAdapter.SelectCommand.Parameters.Clear();
        }
        //-------------------------------------------------------
        //returns:   true (success) , false(failure)
        public static bool ExecQuery_NoResult(string Query)
        {
            try
            {
                DataSet ds = new DataSet();
                dataAdapter.SelectCommand.CommandType = CommandType.Text;
                dataAdapter.SelectCommand.CommandText = Query;
                dataAdapter.Fill(ds);
                ds.Dispose();
                dataAdapter.SelectCommand.Connection.Close();
            }
            catch (Exception e)
            {
                InformationManager.Set_Info(new Errors("Request Error", e.Message, "Class(ConnectionManager):Func(ExecQuery_NoResult)"));
                dataAdapter.SelectCommand.Connection.Close();
                return false;
            }

            return true;
        }
        //-------------------------------------------------------
        //returns:   true (success) , false(failure)
        public static bool ExecQuery(string Query, ref DataSet ds)
        {
            try
            {
                dataAdapter.SelectCommand.CommandType = CommandType.Text;
                dataAdapter.SelectCommand.CommandText = Query;
                dataAdapter.Fill(ds);
                dataAdapter.SelectCommand.Connection.Close();

                if (ds == null)
                {
                    return false;
                }

                if (ds.Tables == null || ds.Tables.Count == 0 || ds.Tables[0] == null)
                {
                    ds.Dispose();
                    return false;
                }
            }
            catch (Exception e)
            {
                InformationManager.Set_Info(new Errors("Request Error", e.Message, "Class(ConnectionManager):Func(ExecQuery)"));
                dataAdapter.SelectCommand.Connection.Close();
                return false;
            }

            return true;
        }
        //-------------------------------------------------------
        //returns:   true (success) , false(failure or insufficient row count)
        public static bool ExecQuery(string Query, ref DataSet ds, int expectedRowCount)
        {
            try
            {
                dataAdapter.SelectCommand.CommandType = CommandType.Text;
                dataAdapter.SelectCommand.CommandText = Query;
                dataAdapter.Fill(ds);
                dataAdapter.SelectCommand.Connection.Close();

                if (ds == null)
                {
                    return false;
                }

                if (ds.Tables == null || ds.Tables.Count == 0 || ds.Tables[0] == null || ds.Tables[0].Rows.Count != expectedRowCount)
                {
                    ds.Dispose();
                    return false;
                }
            }
            catch (Exception e)
            {
                InformationManager.Set_Info(new Errors("Request Error", e.Message, "Class(ConnectionManager):Func(ExecQuery)"));
                dataAdapter.SelectCommand.Connection.Close();
                return false;
            }

            return true;
        }
        //-------------------------------------------------------
        //returns:   valid DataSet (success) , null(failure)
        public static DataSet ExecQuery(string Query)
        {
            try
            {
                DataSet ds = new DataSet();
                dataAdapter.SelectCommand.CommandType = CommandType.Text;
                dataAdapter.SelectCommand.CommandText = Query;
                dataAdapter.Fill(ds);
                dataAdapter.SelectCommand.Connection.Close();

                if (ds == null)// to be sure  ;)
                {
                    return null;
                }

                if (ds.Tables == null || ds.Tables.Count == 0 || ds.Tables[0] == null)
                {
                    ds.Dispose();
                    return null;
                }

                return ds;
            }
            catch (Exception e)
            {
                InformationManager.Set_Info(new Errors("Request error", e.Message, "Class(ConnectionManager):Func(ExecQuery)"));
                dataAdapter.SelectCommand.Connection.Close();
                return null;
            }
        }
        //-------------------------------------------------------
        //returns:   valid DataSet (success) , null(failure or insufficient row count)
        public static DataSet ExecQuery(string Query, int expectedRowCount)
        {
            try
            {
                DataSet ds = new DataSet();
                dataAdapter.SelectCommand.CommandType = CommandType.Text;
                dataAdapter.SelectCommand.CommandText = Query;
                dataAdapter.Fill(ds);
                dataAdapter.SelectCommand.Connection.Close();

                if (ds == null)//to be sure
                {
                    return null;
                }

                if (ds.Tables == null || ds.Tables.Count == 0 || ds.Tables[0] == null || ds.Tables[0].Rows.Count != expectedRowCount)
                {
                    ds.Dispose();
                    return null;
                }

                return ds;
            }
            catch (Exception e)
            {
                InformationManager.Set_Info(new Errors("Request Error", e.Message, "Class(ConnectionManager):Func(ExecQuery)"));
                dataAdapter.SelectCommand.Connection.Close();
                return null;
            }
        }
        //-------------------------------------------------------
    }
}
