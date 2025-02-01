using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace DMR.Reporting.Report_Generator

{
    public partial class frmUnitSelectionForReport : Form
    {
        public Dictionary<string/*group*/, List<string>/*selected units*/> userSelUnitsForReport = null;
        //-------------------------------------------------------------------
        public List<CheckBox> allCheckBoxes = new List<CheckBox>();
        //-------------------------------------------------------------------
        //returns OnError:error description , Else:""
        string ExtractSelectedUnits()
        {
            userSelUnitsForReport = new Dictionary<string, List<string>>();

            foreach (Control con in pnlProjectUnitContainer.Controls)
            {
                if (con.GetType() != typeof(GroupBox))
                    continue;

                GroupBox grb = (GroupBox)con;
                //~~~~~~~~~~~~~
                List<string> units = new List<string>();

                foreach (CheckBox chb in grb.Controls)
                {
                    if (chb.Checked)
                        units.Add(chb.Text);
                }
                //~~~~~~~~~~~~~
                if (units.Count == 0)
                    return "No unit selected for " + grb.Text;
                //~~~~~~~~~~~~~
                userSelUnitsForReport.Add(grb.Text, units);
            }

            return "";
        }
        //-------------------------------------------------------------------
        public frmUnitSelectionForReport()
        {
            InitializeComponent();

            InitAllCheckBoxes();

            LoadProjectUnits();
        }
        //-------------------------------------------------------------------
        void InitAllCheckBoxes()
        {
            allCheckBoxes.Add(rbtnUnitSelectedLiquidMaterialConcentrationGalBBL);
            allCheckBoxes.Add(rbtnUnitSelectedLiquidMaterialConcentrationVolPercent);

            allCheckBoxes.Add(rbtnUnitSelectedPowderMaterialConcentrationKgm3);
            allCheckBoxes.Add(rbtnUnitSelectedPowderMaterialConcentrationPPB);

            allCheckBoxes.Add(rbtnUnitSelectedFilterCakeThicknessMM);
            allCheckBoxes.Add(rbtnUnitSelectedFilterCakeThickness1_32In);

            allCheckBoxes.Add(rbtnUnitSelectedLinerLengthAndDiameterMM);
            allCheckBoxes.Add(rbtnUnitSelectedLinerLengthAndDiameterIn);

            allCheckBoxes.Add(rbtnUnitSelectedWeightKg);
            allCheckBoxes.Add(rbtnUnitSelectedWeightLbm);

            allCheckBoxes.Add(rbtnUnitSelectedNozzleVelocityMSec);
            allCheckBoxes.Add(rbtnUnitSelectedNozzleVelocityFtSec);

            allCheckBoxes.Add(rbtnUnitSelectedVelocityMMin);
            allCheckBoxes.Add(rbtnUnitSelectedVelocityFtMin);
            allCheckBoxes.Add(rbtnUnitSelectedVelocityFtSec);

            allCheckBoxes.Add(rbtnUnitSelectedForceN);
            allCheckBoxes.Add(rbtnUnitSelectedForceLbf);

            allCheckBoxes.Add(rbtnUnitSelectedBulkSystemCapacityTon);
            allCheckBoxes.Add(rbtnUnitSelectedBulkSystemCapacityLb);

            allCheckBoxes.Add(rbtnUnitSelectedYieldPointAndGelStrengthLbf100Ft2);
            allCheckBoxes.Add(rbtnUnitSelectedYieldPointAndGelStrengthPa);

            allCheckBoxes.Add(rbtnUnitSelectedDischargeLossRateM3Hr);
            allCheckBoxes.Add(rbtnUnitSelectedDischargeLossRateBblHr);

            allCheckBoxes.Add(rbtnUnitSelectedNozzleSizeMM);
            allCheckBoxes.Add(rbtnUnitSelectedNozzleSize1_32In);

            allCheckBoxes.Add(rbtnUnitSelectedCasingAndPipeDiameterMM);
            allCheckBoxes.Add(rbtnUnitSelectedCasingAndPipeDiameterIn);

            allCheckBoxes.Add(rbtnUnitSelectedFunnelViscositySecL);
            allCheckBoxes.Add(rbtnUnitSelectedFunnelViscositySecQt);

            allCheckBoxes.Add(rbtnUnitSelectedPlasticViscosityMPaS);
            allCheckBoxes.Add(rbtnUnitSelectedPlasticViscosityCp);

            allCheckBoxes.Add(rbtnUnitSelectedROPMHr);
            allCheckBoxes.Add(rbtnUnitSelectedROPFtHr);

            allCheckBoxes.Add(rbtnUnitSelectedPressureGradientKPaM);
            allCheckBoxes.Add(rbtnUnitSelectedPressureGradientPsiFt);

            allCheckBoxes.Add(rbtnUnitSelectedHoleBitSizeMM);
            allCheckBoxes.Add(rbtnUnitSelectedHoleBitSizeIn);

            allCheckBoxes.Add(rbtnUnitSelectedDepthM);
            allCheckBoxes.Add(rbtnUnitSelectedDepthFt);

            allCheckBoxes.Add(rbtnUnitSelectedMudMBTKgM3);
            allCheckBoxes.Add(rbtnUnitSelectedMudMBTPPb);

            allCheckBoxes.Add(rbtnUnitSelectedPmPfMfCm3);
            allCheckBoxes.Add(rbtnUnitSelectedPmPfMfMl);

            allCheckBoxes.Add(rbtnUnitSelectedLonicMassConcentrationMgL);
            allCheckBoxes.Add(rbtnUnitSelectedLonicMassConcentrationPpm);

            allCheckBoxes.Add(rbtnUnitSelectedLiquidVolumeM3);
            allCheckBoxes.Add(rbtnUnitSelectedLiquidVolumeBbl);

            allCheckBoxes.Add(rbtnUnitSelectedTemperatureC);
            allCheckBoxes.Add(rbtnUnitSelectedTemperatureF);

            allCheckBoxes.Add(rbtnUnitSelectedPressureKPa);
            allCheckBoxes.Add(rbtnUnitSelectedPressureMPa);
            allCheckBoxes.Add(rbtnUnitSelectedPressurePsi);

            allCheckBoxes.Add(rbtnUnitSelectedVolumeStrokeM3Stk);
            allCheckBoxes.Add(rbtnUnitSelectedVolumeStrokeBblStk);
            allCheckBoxes.Add(rbtnUnitSelectedVolumeStrokeGalStk);

            allCheckBoxes.Add(rbtnUnitSelectedFlowRateM3Min);
            allCheckBoxes.Add(rbtnUnitSelectedFlowRateBblMin);
            allCheckBoxes.Add(rbtnUnitSelectedFlowRateGalMin);

            allCheckBoxes.Add(rbtnUnitSelectedTorqueNm);
            allCheckBoxes.Add(rbtnUnitSelectedTorqueLbfFt);
            allCheckBoxes.Add(rbtnUnitSelectedTorqueKlbfFt);

            allCheckBoxes.Add(rbtnUnitSelectedAPIHPHTFluidLossCm3_30Min);
            allCheckBoxes.Add(rbtnUnitSelectedAPIHPHTFluidLossCc_30Min);
            allCheckBoxes.Add(rbtnUnitSelectedAPIHPHTFluidLossMl_30Min);

            allCheckBoxes.Add(rbtnUnitSelectedMudPitsCapacityAndDeadVolM3);
            allCheckBoxes.Add(rbtnUnitSelectedMudPitsCapacityAndDeadVolBbl);

            allCheckBoxes.Add(rbtnUnitSelectedPowerKW);
            allCheckBoxes.Add(rbtnUnitSelectedPowerHp);

            allCheckBoxes.Add(rbtnUnitSelectedMudWeightKgM3);
            allCheckBoxes.Add(rbtnUnitSelectedMudWeightSG);
            allCheckBoxes.Add(rbtnUnitSelectedMudWeightPpg);
            allCheckBoxes.Add(rbtnUnitSelectedMudWeightPcf);

            allCheckBoxes.Add(rbtnUnitSelectedNaClConcentrationKgM3);
            allCheckBoxes.Add(rbtnUnitSelectedNaClConcentrationPPb);
            allCheckBoxes.Add(rbtnUnitSelectedNaClConcentrationWt);
            allCheckBoxes.Add(rbtnUnitSelectedNaClConcentrationMgL);

            allCheckBoxes.Add(rbtnUnitSelectedKClConcentrationKgM3);
            allCheckBoxes.Add(rbtnUnitSelectedKClConcentrationPPb);
            allCheckBoxes.Add(rbtnUnitSelectedKClConcentrationWt);
            allCheckBoxes.Add(rbtnUnitSelectedKClConcentrationMgL);

            allCheckBoxes.Add(rbtnUnitSelectedWeightOnBitDaN);
            allCheckBoxes.Add(rbtnUnitSelectedWeightOnBitLbf);
            allCheckBoxes.Add(rbtnUnitSelectedWeightOnBitKlbf);
        }       
        //-------------------------------------------------------
        private void btnOK_Click(object sender, EventArgs e)
        {
            string res = ExtractSelectedUnits();
            if (res != "")
            {
                userSelUnitsForReport.Clear();
                userSelUnitsForReport = null;
                InformationManager.Set_Info(res);
            }
            else
                Close();
        }
        //-------------------------------------------------------------------
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
        //-------------------------------------------------------------------
        //returns:   true (success) , false(failure)
        bool LoadProjectUnits()
        {
            //string METHOD = "LoadProjectUnits : " + CLASS_NAME;

            try
            {
                string query =

                "SELECT PropName,selectedUnit FROM rt_Prj2Prop  where  PrjAutoID = " + frmMain.selectedPrjID.ToString();

                DataSet ds = ConnectionManager.ExecQuery(query);

                if (ds != null)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        for (int j = 0; j < allCheckBoxes.Count; j++)
                        {
                            CheckBox chb = allCheckBoxes[j];
                            GroupBox gb = (GroupBox)chb.Parent;

                            if (ds.Tables[0].Rows[i][0].ToString() == gb.Text && ds.Tables[0].Rows[i][1].ToString() == chb.Text)
                            {
                                foreach (Control con in gb.Controls)
                                {
                                    if (con.GetType() != typeof(CheckBox))
                                        continue;

                                    ((CheckBox)con).Checked = false;
                                }

                                chb.Checked = true;
                                break;
                            }
                        }
                    }

                    ds.Dispose();
                    return true;
                }
            }
            catch (Exception)
            {
                //ErrorManager.Set_Error(new Errors("Can not load data from data base", ex.Message, METHOD));
                //will be handled outside
            }

            return false;
        }
        //-------------------------------------------------------
    }
}
