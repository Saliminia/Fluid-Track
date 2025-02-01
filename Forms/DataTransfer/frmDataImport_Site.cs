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
    public partial class frmDataImport_Site : Form
    {
        public frmDataImport_Site()
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
        private void btnImportPrjDfp_Click(object sender, EventArgs e)
        {
            try
            {
                string fileName = ChooseDataFile("Office Project/DFP/Common Data File [version 1.0]|*.opdc_v001_000", ".opdc_v001_000");

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
                     "@tblProject", "@tblPrj2EqCost", "@tblPrj2Prd", "@tblPrj2Prop"
	                ,"@tblDFP_HoleSize", "@tblDFP_Rig", "@tblDFP_Well", "@tblDFP_Hole2DFS" 
	                ,"@tblDFP_Hole2DFSCol", "@tblDFP_Hole2DFSConsumption", "@tblDFP_Hole2DFSMatConcenRow", "@tblDFP_Hole2DFSPropRow"
	                ,"@tblDFP_Hole2Geo", "@tblDFP_Hole2WellDesign", "@tblCasingData", "@tblDrill_PCH"
	                ,"@tblDrillingFluidSystem", "@tblGeneralEquipment", "@tblGeologyOfIran_Area" 
	                ,"@tblGeologyOfIran_AreaField", "@tblGeologyOfIran_AreaFieldFormation"	, "@tblHoleSizes", "@tblHSE_PPE"
	                ,"@tblIADC_Hours", "@tblLabEquipments", "@tblPersonnel", "@tblPredefValues", "@tblProduct"   						 
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
                int ret = Procedures.CallProcedure("_prc_DataImport_FormOffice_V001_000__ProjectAndDFPAndCommon", prms, out simpErr, out critErr, out resultStat);

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
        //----------------------------------------------------------------
    }
}
