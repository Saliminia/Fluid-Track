using System;
using System.Collections.Generic;
using System.Data;

using System.Text;
using System.Windows.Forms;

namespace DMR
{
    public class UserOperations
    {
        //-------------------------------------------------------
        public static bool IsUserLoggedIn(string userID)
        {
            bool result = false;

            string query = "select CurrentStatus from at_User where ID = \'" + userID.Replace("\'", "\'\'") + "\'";
            DataSet ds = new DataSet();
            if (ConnectionManager.ExecQuery(query, ref ds, 1))
            {
                result = (ds.Tables[0].Rows[0][0].ToString().ToLower() == "logged in");
                ds.Dispose();
            }

            return result;
        }
        //-------------------------------------------------------
        public static bool IsUserActive(string userID)
        {
            bool result = false;

            string query = "select Active from at_User where ID = \'" + userID.Replace("\'", "\'\'") + "\'";
            DataSet ds = new DataSet();
            if (ConnectionManager.ExecQuery(query, ref ds, 1))
            {
                result = Convert.ToBoolean(ds.Tables[0].Rows[0][0]);
                ds.Dispose();
            }

            return result;
        }
        //-------------------------------------------------------
        public static void UserLogin(string userID)
        {
            string query = "update at_User set  CurrentStatus =  \'Logged in\'  where   ID = \'" + userID.Replace("\'", "\'\'") + "\'";
            ConnectionManager.ExecQuery_NoResult(query);
        }
        //-------------------------------------------------------
        public static void UserLogout(string userID)
        {
            string query = "update at_User set  CurrentStatus =  \'\'  where   ID = \'" + userID.Replace("\'", "\'\'") + "\'";
            ConnectionManager.ExecQuery_NoResult(query);
        }
        //-------------------------------------------------------
        public static string UserRole(string userID)
        {
            string result = "";

            string query = "select RoleName from at_User where ID = \'" + userID.Replace("\'", "\'\'") + "\'";
            DataSet ds = new DataSet();
            if (ConnectionManager.ExecQuery(query, ref ds, 1))
            {
                result = ds.Tables[0].Rows[0][0].ToString();
                ds.Dispose();
            }
            return result;
        }
        //-------------------------------------------------------
        public static bool PrompForUserPassword(string userID)
        {
            frmInput input = new frmInput();
            input.Text = "Are you sure?";
            input.lblTitle.Text = "Enter Password:";
            input.txtInput.PasswordChar = '*';

            if (input.ShowDialog() != DialogResult.OK)
                return false;

            string password = input.txtInput.Text;

            if (password == "")
            {
                InformationManager.Set_Info("Empty Password");
                return false;
            }

            //Check Password
            {
                string pass = EncoderDecoder.EncodePassword(password/*do not trim*/);

                List<ProcedureParam> prms = new List<ProcedureParam>();
                prms.Add(new ProcedureParam("@ID", userID, 50));
                prms.Add(new ProcedureParam("@Pass", pass, 50));

                string simpErr;
                Errors critErr;
                Int64 resultStat;

                int ret = Procedures.CallProcedure("prc_at_User_check", prms, out simpErr, out critErr, out resultStat);

                if (ret != 0)
                {
                    InformationManager.Set_Info("Invalid password for current user");
                    return false;
                }
            }

            return true;
        }
        //-------------------------------------------------------

    }
}
