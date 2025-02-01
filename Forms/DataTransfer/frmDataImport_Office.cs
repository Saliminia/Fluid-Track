using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DMR
{
    public partial class frmDataImport_Office : Form
    {
        //--------------------------------------------------------------
        public frmDataImport_Office()
        {
            InitializeComponent();
        }
        //--------------------------------------------------------------
        string ChooseDataFile(string filter, string ext)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = filter;
            ofd.Title = "Select Data File";

            string fileName = "";

            if (ofd.ShowDialog() == DialogResult.OK)
                fileName = ofd.FileName;

            //check if other type of file is selected
            if (!fileName.ToLower().EndsWith(ext))
                fileName = "";

            ofd.Dispose();

            return fileName;
        }
        //--------------------------------------------------------------
        private void btnImportRigWell_Click(object sender, EventArgs e)
        {
            try
            {
                string fileName = ChooseDataFile("Site Rig/Well Data File [version 1.0]|*.srw_v001_000", ".srw_v001_000");

                if (fileName == "")
                    return;
                //~~~~~~~~~~~~~~~~~~~~~~~~~~
                DataSet ds = EncoderDecoder.LoadDataSetFromEncodedFile_V0001_0000(fileName);

                if (ds == null || ds.Tables == null)
                {
                    InformationManager.Set_Info("Can not load data file");
                    return;
                }
                //~~~~~~~~~~~~~~~~~~~~~~~~~~
                string[] tableNames = new string[] 
                {
                    "@tblProject" 
                    ,"@tblHolesOfProjects" ,"@tblRig" ,"@tblRig2BulkSystem" ,"@tblRig2MudPump"
                    ,"@tblRig2PitTank" ,"@tblRig2ShaleShaker" ,"@tblRig2SolidControlEq" ,"@tblWell" ,"@tblWell2Casing"
                    ,"@tblWell2Geo", "@tblRig2Well"								 
                };

                if (ds.Tables.Count != tableNames.Length)
                {
                    InformationManager.Set_Info("Data file is invalid or corrupted");
                    return;
                }
                //~~~~~~~~~~~~~~~~~~~~~~~~~~
                List<ProcedureParam> prms = new List<ProcedureParam>();

                for (int i = 0; i < tableNames.Length; i++)
                    prms.Add(new ProcedureParam(tableNames[i], ds.Tables[i]));

                string simpErr;
                Errors critErr;
                Int64 resultStat;
                //~~~~~~~~~~~~~~~~~~~~~~~~~~
                int ret = Procedures.CallProcedure("_prc_DataImport_FormSite_V001_000__RigAndWell", prms, out simpErr, out critErr, out resultStat);

                if (ret != 0)
                {
                    if (ret == 1)
                        InformationManager.Set_Info(simpErr);
                    else
                        InformationManager.Set_Info(critErr);

                    return;
                }
                //~~~~~~~~~~~~~~~~~~~~~~~~~~
                InformationManager.Set_Info("Done Successfully", "Importing Status:", "Information");
                //~~~~~~~~~~~~~~~~~~~~~~~~~~
            }
            catch (Exception)
            {
                InformationManager.Set_Info("Error importing data");
            }
        }
        //--------------------------------------------------------------
        private void btnImportReport_Click(object sender, EventArgs e)
        {
            try
            {
                string fileName = ChooseDataFile("Site Report Data File [version 1.0]|*.srep_v001_000", ".srep_v001_000");

                if (fileName == "")
                    return;
                //~~~~~~~~~~~~~~~~~~~~~~~~~~
                DataSet ds = EncoderDecoder.LoadDataSetFromEncodedFile_V0001_0000(fileName);

                if (ds == null || ds.Tables == null)
                {
                    InformationManager.Set_Info("Can not load data file");
                    return;
                }
                //~~~~~~~~~~~~~~~~~~~~~~~~~~
                string[] tableNames = new string[] 
                {
                    "@tblProject"
                    ,"@tblReport" ,"@tblRep2DrlOpHole"	,"@tblRep2DrlOpHole_User"	,"@tblRep2DrlOpIADC","@tblRep2DrlOpIADC_TightSpot", "@tblRep2DrlOpIADC_WashAndReam","@tblRep2DrlOpPrf"	,"@tblRep2DrlOpPrf_Additive"
                    ,"@tblRep2DrlOpROPData"	,"@tblRep2DrlOpRotSpeedData"	,"@tblRep2DrlOpSurveyData"	, "@Rep2HydraulicNozzles" ,"@tblRep2InvManGeneralEq"	,"@tblRep2InvManHSE"
                    ,"@tblRep2InvManLabEq" ,"@tblRep2InvManPrd"	,"@tblRep2InvManRigEqBulk"	,"@tblRep2InvManRigEqPit"	,"@tblRep2InvManRigEqShale"
                    ,"@tblRep2MudLossFormation"	,"@tblRep2MudLossFormation_Additive"	,"@tblRep2MudLossRecord"	,"@tblRep2MudProp_MudPropCol"
                    ,"@tblRep2MudProp_MudPropRow"	,"@tblRep2MudPropPeriod"	,"@tblRep2MudPropWater"	,"@tblRep2MudPump"	,"@tblRep2MudVolMan_PitTankVol"
                    ,"@tblRep2MudVolManDFS"	,"@tblRep2MudVolManDFS_BuiltUsingOthers"	,"@tblRep2MudVolManDFS_BuiltUsingOthers_Prd"
                    ,"@tblRep2MudVolManDFS_BuiltVol"	,"@tblRep2MudVolManDFS_BuiltVol_Prd"	,"@tblRep2MudVolManDFS_CmtSpc_Prd"	,"@tblRep2MudVolManDFS_RecTrans"
                    ,"@tblRep2MudVolManDFS_RecTrans_Prd"	,"@tblRep2MudVolManDFS_TotalWellVolMerge"	
                    ,"@tblRep2MudVolManDFS_TreatedVol"	,"@tblRep2MudVolManDFS_TreatedVol_Prd"	,"@tblRep2SolidAnaly"	,"@tblRep2SolidControl"
                    ,"@tblRep2SolidControlScreen"	,"@tblRep2SolidControlUsed"
                };


                if (ds.Tables.Count != tableNames.Length)
                {
                    InformationManager.Set_Info("Data file is invalid or corrupted");
                    return;
                }
                //~~~~~~~~~~~~~~~~~~~~~~~~~~
                List<ProcedureParam> prms = new List<ProcedureParam>();

                for (int i = 0; i < tableNames.Length; i++)
                {
                    prms.Add(new ProcedureParam(tableNames[i], ds.Tables[i]));

                    int g = ds.Tables[i].Columns.Count;
                }

                string simpErr;
                Errors critErr;
                Int64 resultStat;
                //~~~~~~~~~~~~~~~~~~~~~~~~~~
                int ret = Procedures.CallProcedure("_prc_DataImport_FormSite_V001_000__Report", prms, out simpErr, out critErr, out resultStat);

                if (ret != 0)
                {
                    if (ret == 1)
                        InformationManager.Set_Info(simpErr);
                    else
                        InformationManager.Set_Info(critErr);

                    return;
                }
                //~~~~~~~~~~~~~~~~~~~~~~~~~~
                InformationManager.Set_Info("Done Successfully", "Importing Status:", "Information");
                //~~~~~~~~~~~~~~~~~~~~~~~~~~
            }
            catch (Exception)
            {
                InformationManager.Set_Info("Error importing data");
            }
        }
        //--------------------------------------------------------------
    }
}
