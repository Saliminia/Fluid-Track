using System;
using System.Collections.Generic;

using System.Text;
using System.Windows.Forms;

namespace DMR
{
    public class InformationManager
    {
        private static MultiItem_Info_Form Multi_Info_Frm = new MultiItem_Info_Form();
        private static SingleItem_Info_Form Single_Info_Frm = new SingleItem_Info_Form();
        private static MultiButton_Info_Form Multi_Btn_Info_Frm = new MultiButton_Info_Form();
        //=======================================================
        public static DialogResult Set_Multi_Btn_Info_YesNo(string context, string label, string title)
        {
            return Multi_Btn_Info_Frm.Set_Info_YesNo(context, label, title);
        }
        //-------------------------------------------------------
        public static void Set_Info(string context, string label, string title)
        {
            Single_Info_Frm.Set_Info(context, label, title);
        }
        //-------------------------------------------------------
        public static void Set_Info(string context)
        {
            Single_Info_Frm.Set_Info(context, "Error:", "Error/Warning");
        }
        //-------------------------------------------------------
        public static void Set_Info(params Errors[] err)
        {
            Multi_Info_Frm.Set_Error(err);
        }
        //-------------------------------------------------------
        public static void Set_MultiInfo_Mode(int mode)
        {
            Multi_Info_Frm.SetMode(mode);
        }
        //-----------------------------------------------------

    }
}
