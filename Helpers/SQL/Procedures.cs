using System;
using System.Collections.Generic;

using System.Text;
using System.Data.SqlClient;
using System.Data;


namespace DMR
{
    public class Procedures
    {
        const string CLASS_NAME = "Procedures";
        //=======================================================

        //returns:  0 (success) , 1 (Simple Error, simpErr is valid), 4 (Citical Error, critErr is valid)
        public static int CallProcedure(string prcName, List<ProcedureParam> prcParams, out string simpErr, out Errors critErr, out Int64 resultStatus)
        {
            string METHOD = "CallProcedure : " + CLASS_NAME;

            LogManager.AddLogRecord(METHOD + "  Procedure name = " + prcName);

            simpErr = null;
            critErr = null;
            resultStatus = -1;

            ConnectionManager.BeginCallStoredProcedure(prcName);
            DataSet ds = new DataSet();


            for (int i = 0; i < prcParams.Count; i++)
            {
                SqlParameter sqlParam = prcParams[i].GenerateSqlParameter(out simpErr);

                if (sqlParam == null)
                    return 1;

                ConnectionManager.dataAdapter.SelectCommand.Parameters.Add(sqlParam);
            }


            SqlParameter RESULT_ = new SqlParameter("@RESULT_", null);
            RESULT_.Direction = ParameterDirection.Output;
            RESULT_.SqlDbType = SqlDbType.NVarChar;
            RESULT_.Size = 250;
            SqlParameter Result_Status = new SqlParameter("@Result_Status", SqlDbType.BigInt);
            Result_Status.Direction = ParameterDirection.Output;


            ConnectionManager.dataAdapter.SelectCommand.Parameters.Add(RESULT_);
            ConnectionManager.dataAdapter.SelectCommand.Parameters.Add(Result_Status);


            try
            {
                LogManager.AddLogRecord("dataAdapter.SelectCommand.Connection.Open() : " + METHOD);
                ConnectionManager.dataAdapter.SelectCommand.Connection.Open();

                LogManager.AddLogRecord("dataAdapter.SelectCommand.ExecuteNonQuery() : " + METHOD);
                ConnectionManager.dataAdapter.SelectCommand.ExecuteNonQuery();

                resultStatus = (Int64)Result_Status.Value;


                if (resultStatus < 0)
                {
                    LogManager.AddLogRecord("Stored Procedure Result = " + (string)RESULT_.Value);

                    LogManager.AddLogRecord("EndCallStoredProcedure : " + METHOD);
                    ConnectionManager.EndCallStoredProcedure();

                    simpErr = (string)RESULT_.Value;
                    return 1;
                }
                 
            }
            catch (Exception ex)
            {
                LogManager.AddLogRecord(ex.Message + " : " + METHOD);

                LogManager.AddLogRecord("EndCallStoredProcedure : " + METHOD);
                ConnectionManager.EndCallStoredProcedure();

                LogManager.WriteLogToFile("_l.e", true);
                LogManager.ClearLog();

                critErr = new Errors("Request error", ex.Message, METHOD);
                return 4;
            }



            LogManager.AddLogRecord("EndCallStoredProcedure : " + METHOD);
            ConnectionManager.EndCallStoredProcedure();

            return 0;
        }
        //-------------------------------------------------------
    }
}