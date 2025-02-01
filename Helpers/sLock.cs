using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.IO;
using System.Net.NetworkInformation;

#if !NOLOCK
using SysEnhanced;
#endif

namespace DMR.Helpers
{
    public static class SLock
    {
#if !NOLOCK
        public static DateTime CheckSpecification()
        {
            DateTime nResult = DateTime.Today.AddMinutes(-1);
            try
            {
                String HWinfo = HardwareInfo.GetMinHWInfo() + "#Dangle Serial No=" + Program.LockID + "\n#ExpireDate=";
                McDESEnhanced RijndaelKey = new McDESEnhanced("pas5pr@se3Hwdmpo00hj1", "@1B2c3D3e7F6g7H8");
                string CText = RijndaelKey.Encrypt("1B2c3D3e7F6g7H8");
                //RegistryKey RegKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\DeviceClasses\\{e251ab43-5952-6872-4387-65194a7c28f2}\\Properties");
                RegistryKey RegKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\DMR\\Properties");
                RegistryKey RegKeyCurrent = Registry.CurrentUser.OpenSubKey("SOFTWARE\\DMR\\Properties");


                if (RegKey != null)
                {
                    object HInfo = RegKey.GetValue("HInfo");
                    object EInfo = RegKey.GetValue("EInfo");
                    object CInfo = RegKey.GetValue("CInfo");
                    object CIInfo = RegKey.GetValue("CIInfo");
                    if (RegKeyCurrent == null)
                    {
                        try
                        {
                            RegistryKey keycurrent = Registry.CurrentUser.CreateSubKey("SOFTWARE\\DMR\\Properties");
                            keycurrent.Close();
                            RegKeyCurrent = Registry.CurrentUser.OpenSubKey("SOFTWARE\\DMR\\Properties", true);
                            RegKeyCurrent.SetValue("HInfo", HInfo.ToString(), RegistryValueKind.String);
                            RegKeyCurrent.SetValue("EInfo", EInfo.ToString(), RegistryValueKind.String);
                            RegKeyCurrent.SetValue("CInfo", CInfo.ToString(), RegistryValueKind.String);
                            RegKeyCurrent.SetValue("CIInfo", CIInfo.ToString(), RegistryValueKind.String);
                            RegKeyCurrent.Close();
                        }
                        catch { }
                    }
                    string sHInfo = HInfo.ToString().Trim();
                    string sEInfo = RijndaelKey.Decrypt(EInfo.ToString().Trim());
                    string sCInfo = RijndaelKey.Decrypt(CInfo.ToString().Trim());
                    DateTime ExpDate = Convert.ToDateTime(sEInfo);
                    Program.iClientID = Convert.ToInt32(sCInfo);
                    HWinfo += sEInfo.Trim() + "\n";
                    String strHInfo = RijndaelKey.Decrypt(sHInfo);
                    if (String.Compare(HWinfo, strHInfo) == 0 && ExpDate > DateTime.Today && String.Compare(CInfo.ToString().Trim(), CIInfo.ToString().Trim()) == 0)
                        return DateTime.Now.AddSeconds(2);
                }
                else
                    if (RegKeyCurrent != null)
                {
                    object HInfo = RegKeyCurrent.GetValue("HInfo");
                    object EInfo = RegKeyCurrent.GetValue("EInfo");
                    object CInfo = RegKeyCurrent.GetValue("CInfo");
                    object CIInfo = RegKeyCurrent.GetValue("CIInfo");
                    string sHInfo = HInfo.ToString().Trim();
                    string sEInfo = RijndaelKey.Decrypt(EInfo.ToString().Trim());
                    string sCInfo = RijndaelKey.Decrypt(CInfo.ToString().Trim());
                    DateTime ExpDate = Convert.ToDateTime(sEInfo);
                    Program.iClientID = Convert.ToInt32(sCInfo);
                    HWinfo += sEInfo.Trim() + "\n";
                    try
                    {
                        RegistryKey key = Registry.LocalMachine.CreateSubKey("SOFTWARE\\DMR\\Properties");
                        key.Close();
                        RegKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\DMR\\Properties", true);
                        RegKey.SetValue("HInfo", HInfo.ToString(), RegistryValueKind.String);
                        RegKey.SetValue("EInfo", EInfo.ToString(), RegistryValueKind.String);
                        RegKey.SetValue("CInfo", CInfo.ToString(), RegistryValueKind.String);
                        RegKey.SetValue("CIInfo", CIInfo.ToString(), RegistryValueKind.String);
                        RegKey.Close();
                    }
                    catch { }

                    String strHInfo = RijndaelKey.Decrypt(sHInfo);
                    if (String.Compare(HWinfo, strHInfo) == 0 && ExpDate > DateTime.Today && String.Compare(CInfo.ToString().Trim(), CIInfo.ToString().Trim()) == 0)
                        return DateTime.Now.AddSeconds(2);
                }
                return nResult;
            }
            catch
            {
            }

            return nResult;
        }

        public static void ApplyLic(string Lic)
        {
            string[] strSep = new string[] { "AsdrumvChoTvQrthcWRyuIOIBGzcSDxzsswfdCXZSDftreddcfghgjikoC#rfere^Zfdddflde2345" };
            string[] strLic = Lic.Split(strSep, StringSplitOptions.None);
            try
            {
                try
                {
                    RegistryKey key, keycurrent;
                    //key = Registry.LocalMachine.CreateSubKey("SYSTEM\\CurrentControlSet\\Control\\DeviceClasses\\{e251ab43-5952-6872-4387-65194a7c28f2}\\Properties");
                    key = Registry.LocalMachine.CreateSubKey("SOFTWARE\\DMR\\Properties");
                    keycurrent = Registry.CurrentUser.CreateSubKey("SOFTWARE\\DMR\\Properties");
                }
                catch
                {
                }

                //RegistryKey RegKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\DeviceClasses\\{e251ab43-5952-6872-4387-65194a7c28f2}\\Properties", true);
                RegistryKey RegKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\DMR\\Properties", true);
                RegistryKey RegKeyCurrent = Registry.CurrentUser.OpenSubKey("SOFTWARE\\DMR\\Properties", true);

                if (strLic.Length > 2)
                {
                    RegKey.SetValue("HInfo", strLic[0], RegistryValueKind.String);
                    RegKey.SetValue("EInfo", strLic[1], RegistryValueKind.String);
                    RegKey.SetValue("CInfo", strLic[2], RegistryValueKind.String);

                    RegKeyCurrent.SetValue("HInfo", strLic[0], RegistryValueKind.String);
                    RegKeyCurrent.SetValue("EInfo", strLic[1], RegistryValueKind.String);
                    RegKeyCurrent.SetValue("CInfo", strLic[2], RegistryValueKind.String);
                    try
                    {
                        object CIInfo = RegKey.GetValue("CIInfo");

                        if (CIInfo.ToString().ToString().Trim() != "HOuG0SFzy9skpR")
                        {
                            RegKey.SetValue("CIInfo", strLic[2], RegistryValueKind.String);
                            RegKeyCurrent.SetValue("CIInfo", strLic[2], RegistryValueKind.String);
                        }

                    }
                    catch
                    {
                        RegKey.SetValue("CIInfo", strLic[2], RegistryValueKind.String);
                        RegKeyCurrent.SetValue("CIInfo", strLic[2], RegistryValueKind.String);
                    }
                }
            }
            catch { }


        }

        public static void ClearLic()
        {
            try
            {
                //RegistryKey RegKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\DeviceClasses\\{e251ab43-5952-6872-4387-65194a7c28f2}\\Properties", true);
                RegistryKey RegKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\DMR\\Properties", true);

                RegKey.SetValue("HInfo", "Not Valid", RegistryValueKind.String);
                RegKey.SetValue("EInfo", "Not Valid", RegistryValueKind.String);
            }
            catch
            {
            }
        }

        public static string GenDetail(int EventID, string EventStatus, string Desc, string Usrname)
        {
            string strDetail = string.Empty;
            McDESEnhanced DesKey = new McDESEnhanced("pas5pr@se3Hwdmpo00hj1", "@1B2c3D3e7F6g7H8");

            strDetail = "#" + "50221" + "=" + Program.iClientID.ToString().Trim() + "\n";
            strDetail += "#" + "50222" + "=" + DateTime.Now.ToString().Trim() + "\n";
            strDetail += "#" + "50223" + "=" + EventID.ToString().Trim() + "\n";
            strDetail += "#" + "50224" + "=" + EventStatus + "\n";
            strDetail += "#" + "50225" + "=" + HardwareInfo.GetIPAddr().Trim() + "\n";
            strDetail += "#" + "50226" + "=" + HardwareInfo.GetComputerName().Trim() + "\n";
            strDetail += "#" + "50227" + "=" + HardwareInfo.GetAccountName().Trim() + "\n";
            strDetail += "#" + "50228" + "=" + Desc + "\n";
            strDetail += "#" + "50229" + "=" + Usrname + "\n";

            strDetail = DesKey.Encrypt(strDetail);

            return strDetail;
        }

        public static void SaveToFile(string filename, string strFileContent)
        {
            try
            {
                if (!File.Exists(filename))
                {
                    File.WriteAllText(filename, strFileContent);
                }
                else
                {
                    File.AppendAllText(filename, strFileContent);
                }
                Program.strLog = string.Empty;
            }
            catch
            { }
        }

        public static int ApplyServerResult(string strResult)
        {
            int iStatus = 0;
            McDESEnhanced DesKey = new McDESEnhanced("pas5pr@se3Hwdmpo00hj1", "@1B2c3D3e7F6g7H8");
            string sResult = DesKey.Decrypt(strResult);

            if (sResult.Trim() == "Valid")
                iStatus = 100;
            //else
            //    ClearLic();

            return iStatus;
        }

        public static void SendLog(string strUserID)
        {
            if (File.Exists("DLAct.bin"))
            {
                try
                {
                    StreamReader sr = new StreamReader("DLAct.bin");
                    string strLastLogs = sr.ReadToEnd();
                    if (strLastLogs.Length < 614400)
                        if (Program.strLog.Length > 1)
                            Program.strLog = strLastLogs.Substring(13) + Program.strLog;
                        else
                            Program.strLog = strLastLogs;
                    sr.Close();
                    File.Delete("DLAct.bin");
                }
                catch { }
            }

            if (Program.strLog != string.Empty)
            {
                string strwResult = string.Empty;
                Ping ping = new Ping();

                try
                {
                    PingReply pingReply = ping.Send("www.parsdrill.com");
                    if (pingReply.Status == IPStatus.Success)
                    {
                        if (Program.bOnline == false)
                        {
                            Program.bOnline = true;
                            Program.strLog = SLock.GenDetail(3, "Switch to Online", "Online", strUserID) + Program.strLog;

                        }
                        DMRWebService.ClientService wService = new DMRWebService.ClientService();

                        try
                        {
                            strwResult = wService.InputLog(Program.strLog);
                            Program.strLog = string.Empty;
                            if (SLock.ApplyServerResult(strwResult) == 0)
                            {
                                Program.LockID = string.Empty;
                                try
                                {
                                    //RegistryKey RegKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\DeviceClasses\\{e251ab43-5952-6872-4387-65194a7c28f2}\\Properties", true);
                                    RegistryKey RegKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\DMR\\Properties", true);
                                    RegKey.SetValue("CIInfo", "HOuG0SFzy9skpR", RegistryValueKind.String);
                                }
                                catch
                                {
                                }

                            }
                        }
                        catch (Exception ex)
                        {
                            SLock.SaveToFile("DLAct.bin", Program.strLog);
                        }
                    }
                    else
                        Program.bOnline = false;
                }
                catch
                {
                    Program.bOnline = false;
                    SLock.SaveToFile("DLAct.bin", Program.strLog);
                }

            }
            return;
        }
#endif
    }
}
