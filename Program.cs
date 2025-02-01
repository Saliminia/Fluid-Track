using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using DMR.Helpers;

#if !NOLOCK
using AxARM;
using ARM;
#endif

namespace DMR
{
    static class Program
    {

#if !NOLOCK
        public static string LockID = string.Empty;
        public static int iClientID = 0;
        private static string _strLog = string.Empty;
        public static bool bOnline = true;
        public static Class1 cls = new Class1();
        public static Random Rand = new Random();
        public static object obj = new object();
        public static byte[] KeyAES = new byte[16];
        public static AxARMClass arm;
        public static int NoLockCount = 0;

        public static string strLog
        {
            get
            {
                return _strLog;
            }
            set
            {
                if (value == string.Empty)
                    _strLog = string.Empty;
                else
                    _strLog = "(Ref432erfd3^" + value;
            }
        }
#endif


        [STAThread]
        static void Main()
        {

#if  SITE_EDITION
            if (Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).Length > 1)
            {
                InformationManager.Set_Info("Application is already running");
                return;
            }
#endif

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


#if !OFFICE_EDITION && !SITE_EDITION
            InformationManager.Set_Info("Software Edition Is Invalid");
#endif

            UnitConverter.InitDirectFactors();

            ConnectionManager.Init();
            LogManager.ClearLog();

            SplashForm s = new SplashForm();
            s.ShowDialog();


            ////======================Dummy==============================
            //if (MessageBox.Show("Only generate report?", "what to do?", MessageBoxButtons.YesNo) == DialogResult.Yes)
            //{
            //    try
            //    {
            //        Reporting.Report_Generator.ReportingBase rep = null;
            //        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            //        frmMain.selectedPrjID = 153;
            //        frmMain.selected_RW_WellID = 1530000000000000;

            //        rep = new Reporting.Report_Generator.Daily.DailyDMR();
            //        rep.parameters.Add("wellID", 1530000000000000);
            //        rep.parameters.Add("prjID", 153);
            //        rep.parameters.Add("code", "test - code");
            //        rep.parameters.Add("reportingDate", new DateTime(2017, 4, 1));
            //        rep.parameters.Add("shamsiReportingDate", "1396/1/12");
            //        rep.parameters.Add("useOperatorImage", true);
            //        rep.parameters.Add("useContractorImage", true);
            //        rep.parameters.Add("selCurrency", "Rial");
            //        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            //        if (rep != null)
            //        {
            //            rep.outDir = @"C:\Users\Zare\Desktop";
            //            string err = rep.GenerateReport();

            //            if (err != "")
            //            {
            //                InformationManager.Set_Info(err);
            //                return;
            //            }
            //        }
            //        else
            //        {
            //            InformationManager.Set_Info("Invalid Report Type");
            //        }
            //    }
            //    catch (Exception)
            //    {
            //        InformationManager.Set_Info("Report Generating Error");
            //        return;
            //    }
            //    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            //    InformationManager.Set_Info("Report generated", "Status:", "Information");
            //    return;
            //}
            ////==========================================================

            //Application.Run(new LoginForm());

#if !NOLOCK
            if (s.bFormStatus == true)
            {
                try
                {
                    Application.Run(new LoginForm());
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error Code 20 in exiting the program.", "DMR Lock", MessageBoxButtons.OK);
                }
            }
            else
            {
                if (s.bSecurityLock == true)
                {
                    Forms.SoftwareActivation frmAF = new Forms.SoftwareActivation();
                    frmAF.ShowDialog();
                }
            }
#else
            Application.Run(new LoginForm());

#endif

            ConnectionManager.Dispose();
        }
    }
}
