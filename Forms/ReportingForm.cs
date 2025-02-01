using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;

using System.Windows.Forms;

namespace DMR
{
    public partial class ReportsForm : Form, IMyForm
    {
        //=======================================================
        void IMyForm.OnCurrentProjectChanged() { }  ///////////////////////////
        //-------------------------------------------------------
        void IMyForm.OnCurrentRigWellChanged() { }  ///////////////////////////
        //-------------------------------------------------------
        void IMyForm.OnCurrentReportChanged() {  }/////////////////
        //-------------------------------------------------------
        void IMyForm.OnInit()
        {
            /////////////////
        }
        //-------------------------------------------------------
        void IMyForm.OnCurrentUnitsChanged()
        {
            //??????????????????????????
            UpdateComputationsByPressRefereshButtons();
        }
        //-------------------------------------------------------
        void UpdateComputationsByPressRefereshButtons()
        {
        }
        //-------------------------------------------------------
        public ReportsForm()
        {
            InitializeComponent();
        }
    }
}
