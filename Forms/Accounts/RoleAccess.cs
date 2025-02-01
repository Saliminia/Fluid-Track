using System;
using System.Collections.Generic;

using System.Text;

namespace DMR
{
    public class RoleAccess
    {
        public enum AcessTypes
        {
            AT_WriteAndRead,
            AT_ReadOnly,
            AT_None
        }
        //-------------------------------------------------------
        public AcessTypes
            project_TreeView = AcessTypes.AT_None,
            project_GeneralInformation = AcessTypes.AT_None,
            project_MasterProductList = AcessTypes.AT_None,
            project_Units = AcessTypes.AT_None,
            project_ServicePriceList = AcessTypes.AT_None,
            //-----------
            dfp_TreeView = AcessTypes.AT_None,
            dfp_WellDesign = AcessTypes.AT_None,
            dfp_Geo = AcessTypes.AT_None,
            dfp_DFS = AcessTypes.AT_None,
            //-----------
            //DMR = AcessTypes.AT_None,
            //-----------
            rig_TreeView = AcessTypes.AT_None,
            rig_RigInfo = AcessTypes.AT_None,
            rig_MudPumps = AcessTypes.AT_None,
            rig_MudPits_Tanks = AcessTypes.AT_None,
            rig_SolidControlEquipments = AcessTypes.AT_None,
            rig_BulkSystem = AcessTypes.AT_None,
            //-----------
            well_TreeView = AcessTypes.AT_None,
            well_General = AcessTypes.AT_None,
            well_CasingData = AcessTypes.AT_None,
            well_Geo = AcessTypes.AT_None,
            //-----------
            rigwell_TreeView = AcessTypes.AT_None,
            rigwell_Reports = AcessTypes.AT_None,
            //-----------
            geologyInformation = AcessTypes.AT_None,
            //-----------
            solidControlEquipment = AcessTypes.AT_None,
            //-----------
            drillingOperation_HoleGeometry = AcessTypes.AT_None,
            drillingOperation_IADC_Hours = AcessTypes.AT_None,
            drillingOperation_DailyOperatingData = AcessTypes.AT_None,
            drillingOperation_PerformanceDrilling = AcessTypes.AT_None,
            //-----------
            mudLosses_FormationLosses = AcessTypes.AT_None,
            mudLosses_LossesRecord = AcessTypes.AT_None,
            //-----------
            mudVolumeManagement_DFSs = AcessTypes.AT_None,
            mudVolumeManagement_Pit_TankVolume = AcessTypes.AT_None,
            mudVolumeManagement_TotalVolManagement = AcessTypes.AT_None,
            //-----------
            mudProperties_MudProperties = AcessTypes.AT_None,
            mudProperties_Water = AcessTypes.AT_None,
            //-----------
            hydraulic = AcessTypes.AT_None,
            //-----------
            solidAnalysis = AcessTypes.AT_None,
            //-----------
            inventoryManagement_ProductInventory = AcessTypes.AT_None,
            inventoryManagement_RigEquipmentInventory = AcessTypes.AT_None,
            inventoryManagement_ManPower = AcessTypes.AT_None,
            inventoryManagement_LabEquipmentInventory = AcessTypes.AT_None,
            inventoryManagement_HSE_PPE = AcessTypes.AT_None,
            inventoryManagement_GeneralEquipmentInventory = AcessTypes.AT_None,
            //-----------
            remarks = AcessTypes.AT_None,
            //-----------
            generalDBs = AcessTypes.AT_None,//none or write
            preDefValues = AcessTypes.AT_None;
        //=======================================================
        public void FillFromRole(string role)
        {
            if (role == "Admin")
            {
                project_TreeView = AcessTypes.AT_WriteAndRead;
                project_GeneralInformation = AcessTypes.AT_WriteAndRead;
                project_MasterProductList = AcessTypes.AT_WriteAndRead;
                project_Units = AcessTypes.AT_WriteAndRead;
                project_ServicePriceList = AcessTypes.AT_WriteAndRead;
                //-----------
                dfp_TreeView = AcessTypes.AT_WriteAndRead;
                dfp_WellDesign = AcessTypes.AT_WriteAndRead;
                dfp_Geo = AcessTypes.AT_WriteAndRead;
                dfp_DFS = AcessTypes.AT_WriteAndRead;
                //-----------
                //DMR = AcessTypes.AT_WriteAndRead;
                //-----------
                rig_TreeView = AcessTypes.AT_WriteAndRead;
                rig_RigInfo = AcessTypes.AT_WriteAndRead;
                rig_MudPumps = AcessTypes.AT_WriteAndRead;
                rig_MudPits_Tanks = AcessTypes.AT_WriteAndRead;
                rig_SolidControlEquipments = AcessTypes.AT_WriteAndRead;
                rig_BulkSystem = AcessTypes.AT_WriteAndRead;
                //-----------
                well_TreeView = AcessTypes.AT_WriteAndRead;
                well_General = AcessTypes.AT_WriteAndRead;
                well_CasingData = AcessTypes.AT_WriteAndRead;
                well_Geo = AcessTypes.AT_WriteAndRead;
                //-----------
                rigwell_TreeView = AcessTypes.AT_WriteAndRead;
                rigwell_Reports = AcessTypes.AT_WriteAndRead;
                //-----------
                geologyInformation = AcessTypes.AT_WriteAndRead;
                //-----------
                solidControlEquipment = AcessTypes.AT_WriteAndRead;
                //-----------
                drillingOperation_HoleGeometry = AcessTypes.AT_WriteAndRead;
                drillingOperation_IADC_Hours = AcessTypes.AT_WriteAndRead;
                drillingOperation_DailyOperatingData = AcessTypes.AT_WriteAndRead;
                drillingOperation_PerformanceDrilling = AcessTypes.AT_WriteAndRead;
                //-----------
                mudLosses_FormationLosses = AcessTypes.AT_WriteAndRead;
                mudLosses_LossesRecord = AcessTypes.AT_WriteAndRead;
                //-----------
                mudVolumeManagement_DFSs = AcessTypes.AT_WriteAndRead;
                mudVolumeManagement_Pit_TankVolume = AcessTypes.AT_WriteAndRead;
                mudVolumeManagement_TotalVolManagement = AcessTypes.AT_WriteAndRead;
                //-----------
                mudProperties_MudProperties = AcessTypes.AT_WriteAndRead;
                mudProperties_Water = AcessTypes.AT_WriteAndRead;
                //-----------
                hydraulic = AcessTypes.AT_WriteAndRead;
                //-----------
                solidAnalysis = AcessTypes.AT_WriteAndRead;
                //-----------
                inventoryManagement_ProductInventory = AcessTypes.AT_WriteAndRead;
                inventoryManagement_RigEquipmentInventory = AcessTypes.AT_WriteAndRead;
                inventoryManagement_ManPower = AcessTypes.AT_WriteAndRead;
                inventoryManagement_LabEquipmentInventory = AcessTypes.AT_WriteAndRead;
                inventoryManagement_HSE_PPE = AcessTypes.AT_WriteAndRead;
                inventoryManagement_GeneralEquipmentInventory = AcessTypes.AT_WriteAndRead;
                //-----------
                remarks = AcessTypes.AT_WriteAndRead;
                //-----------
                generalDBs = AcessTypes.AT_WriteAndRead;
                preDefValues = AcessTypes.AT_WriteAndRead;
            }
            else if (role == "Planning")
            {
                project_TreeView = AcessTypes.AT_WriteAndRead;
                project_GeneralInformation = AcessTypes.AT_WriteAndRead;
                project_MasterProductList = AcessTypes.AT_WriteAndRead;
                project_Units = AcessTypes.AT_WriteAndRead;
                project_ServicePriceList = AcessTypes.AT_WriteAndRead;
                //-----------
                dfp_TreeView = AcessTypes.AT_WriteAndRead;
                dfp_WellDesign = AcessTypes.AT_WriteAndRead;
                dfp_Geo = AcessTypes.AT_WriteAndRead;
                dfp_DFS = AcessTypes.AT_WriteAndRead;
                //-----------
                //DMR = AcessTypes.AT_WriteAndRead;
                //-----------
                rig_TreeView = AcessTypes.AT_ReadOnly;
                rig_RigInfo = AcessTypes.AT_WriteAndRead;
                rig_MudPumps = AcessTypes.AT_ReadOnly;
                rig_MudPits_Tanks = AcessTypes.AT_ReadOnly;
                rig_SolidControlEquipments = AcessTypes.AT_ReadOnly;
                rig_BulkSystem = AcessTypes.AT_ReadOnly;
                //-----------
                well_TreeView = AcessTypes.AT_WriteAndRead;
                well_General = AcessTypes.AT_ReadOnly;
                well_CasingData = AcessTypes.AT_ReadOnly;
                well_Geo = AcessTypes.AT_ReadOnly;
                //-----------
                rigwell_TreeView = AcessTypes.AT_ReadOnly;
                rigwell_Reports = AcessTypes.AT_ReadOnly;
                //-----------
                geologyInformation = AcessTypes.AT_ReadOnly;
                //-----------
                solidControlEquipment = AcessTypes.AT_ReadOnly;
                //-----------
                drillingOperation_HoleGeometry = AcessTypes.AT_ReadOnly;
                drillingOperation_IADC_Hours = AcessTypes.AT_ReadOnly;
                drillingOperation_DailyOperatingData = AcessTypes.AT_ReadOnly;
                drillingOperation_PerformanceDrilling = AcessTypes.AT_ReadOnly;
                //-----------
                mudLosses_FormationLosses = AcessTypes.AT_ReadOnly;
                mudLosses_LossesRecord = AcessTypes.AT_ReadOnly;
                //-----------
                mudVolumeManagement_DFSs = AcessTypes.AT_ReadOnly;
                mudVolumeManagement_Pit_TankVolume = AcessTypes.AT_ReadOnly;
                mudVolumeManagement_TotalVolManagement = AcessTypes.AT_ReadOnly;
                //-----------
                mudProperties_MudProperties = AcessTypes.AT_ReadOnly;
                mudProperties_Water = AcessTypes.AT_ReadOnly;
                //-----------
                hydraulic = AcessTypes.AT_ReadOnly;
                //-----------
                solidAnalysis = AcessTypes.AT_ReadOnly;
                //-----------
                inventoryManagement_ProductInventory = AcessTypes.AT_ReadOnly;
                inventoryManagement_RigEquipmentInventory = AcessTypes.AT_ReadOnly;
                inventoryManagement_ManPower = AcessTypes.AT_ReadOnly;
                inventoryManagement_LabEquipmentInventory = AcessTypes.AT_ReadOnly;
                inventoryManagement_HSE_PPE = AcessTypes.AT_ReadOnly;
                inventoryManagement_GeneralEquipmentInventory = AcessTypes.AT_ReadOnly;
                //-----------
                remarks = AcessTypes.AT_ReadOnly;
                //-----------
                generalDBs = AcessTypes.AT_WriteAndRead;
                preDefValues = AcessTypes.AT_None;
            }
            else if (role == "Engineering Supervisor")
            {
                project_TreeView = AcessTypes.AT_ReadOnly;
                project_GeneralInformation = AcessTypes.AT_ReadOnly;
                project_MasterProductList = AcessTypes.AT_ReadOnly;
                project_Units = AcessTypes.AT_ReadOnly;
                project_ServicePriceList = AcessTypes.AT_ReadOnly;
                //-----------
                dfp_TreeView = AcessTypes.AT_ReadOnly;
                dfp_WellDesign = AcessTypes.AT_ReadOnly;
                dfp_Geo = AcessTypes.AT_ReadOnly;
                dfp_DFS = AcessTypes.AT_ReadOnly;
                //-----------
                //DMR = AcessTypes.AT_ReadOnly;
                //-----------
                rig_TreeView = AcessTypes.AT_ReadOnly;
                rig_RigInfo = AcessTypes.AT_ReadOnly;
                rig_MudPumps = AcessTypes.AT_ReadOnly;
                rig_MudPits_Tanks = AcessTypes.AT_ReadOnly;
                rig_SolidControlEquipments = AcessTypes.AT_ReadOnly;
                rig_BulkSystem = AcessTypes.AT_ReadOnly;
                //-----------
                well_TreeView = AcessTypes.AT_ReadOnly;
                well_General = AcessTypes.AT_ReadOnly;
                well_CasingData = AcessTypes.AT_ReadOnly;
                well_Geo = AcessTypes.AT_ReadOnly;
                //-----------
                rigwell_TreeView = AcessTypes.AT_ReadOnly;
                rigwell_Reports = AcessTypes.AT_ReadOnly;
                //-----------
                geologyInformation = AcessTypes.AT_ReadOnly;
                //-----------
                solidControlEquipment = AcessTypes.AT_ReadOnly;
                //-----------
                drillingOperation_HoleGeometry = AcessTypes.AT_ReadOnly;
                drillingOperation_IADC_Hours = AcessTypes.AT_ReadOnly;
                drillingOperation_DailyOperatingData = AcessTypes.AT_ReadOnly;
                drillingOperation_PerformanceDrilling = AcessTypes.AT_ReadOnly;
                //-----------
                mudLosses_FormationLosses = AcessTypes.AT_ReadOnly;
                mudLosses_LossesRecord = AcessTypes.AT_ReadOnly;
                //-----------
                mudVolumeManagement_DFSs = AcessTypes.AT_ReadOnly;
                mudVolumeManagement_Pit_TankVolume = AcessTypes.AT_ReadOnly;
                mudVolumeManagement_TotalVolManagement = AcessTypes.AT_ReadOnly;
                //-----------
                mudProperties_MudProperties = AcessTypes.AT_ReadOnly;
                mudProperties_Water = AcessTypes.AT_ReadOnly;
                //-----------
                hydraulic = AcessTypes.AT_ReadOnly;
                //-----------
                solidAnalysis = AcessTypes.AT_ReadOnly;
                //-----------
                inventoryManagement_ProductInventory = AcessTypes.AT_ReadOnly;
                inventoryManagement_RigEquipmentInventory = AcessTypes.AT_ReadOnly;
                inventoryManagement_ManPower = AcessTypes.AT_ReadOnly;
                inventoryManagement_LabEquipmentInventory = AcessTypes.AT_ReadOnly;
                inventoryManagement_HSE_PPE = AcessTypes.AT_ReadOnly;
                inventoryManagement_GeneralEquipmentInventory = AcessTypes.AT_ReadOnly;
                //-----------
                remarks = AcessTypes.AT_ReadOnly;
                //-----------
                generalDBs = AcessTypes.AT_WriteAndRead;
                preDefValues = AcessTypes.AT_None;
            }
            else if (role == "Technical Engineer")
            {
                project_TreeView = AcessTypes.AT_WriteAndRead;
                project_GeneralInformation = AcessTypes.AT_ReadOnly;
                project_MasterProductList = AcessTypes.AT_ReadOnly;
                project_Units = AcessTypes.AT_ReadOnly;
                project_ServicePriceList = AcessTypes.AT_ReadOnly;
                //-----------
                dfp_TreeView = AcessTypes.AT_ReadOnly;
                dfp_WellDesign = AcessTypes.AT_ReadOnly;
                dfp_Geo = AcessTypes.AT_ReadOnly;
                dfp_DFS = AcessTypes.AT_ReadOnly;
                //-----------
                //DMR = AcessTypes.AT_ReadOnly;
                //-----------
                rig_TreeView = AcessTypes.AT_WriteAndRead;
                rig_RigInfo = AcessTypes.AT_ReadOnly;
                rig_MudPumps = AcessTypes.AT_ReadOnly;
                rig_MudPits_Tanks = AcessTypes.AT_ReadOnly;
                rig_SolidControlEquipments = AcessTypes.AT_ReadOnly;
                rig_BulkSystem = AcessTypes.AT_ReadOnly;
                //-----------
                well_TreeView = AcessTypes.AT_WriteAndRead;
                well_General = AcessTypes.AT_ReadOnly;
                well_CasingData = AcessTypes.AT_ReadOnly;
                well_Geo = AcessTypes.AT_ReadOnly;
                //-----------
                rigwell_TreeView = AcessTypes.AT_ReadOnly;
                rigwell_Reports = AcessTypes.AT_ReadOnly;
                //-----------
                geologyInformation = AcessTypes.AT_ReadOnly;
                //-----------
                solidControlEquipment = AcessTypes.AT_ReadOnly;
                //-----------
                drillingOperation_HoleGeometry = AcessTypes.AT_ReadOnly;
                drillingOperation_IADC_Hours = AcessTypes.AT_ReadOnly;
                drillingOperation_DailyOperatingData = AcessTypes.AT_ReadOnly;
                drillingOperation_PerformanceDrilling = AcessTypes.AT_ReadOnly;
                //-----------
                mudLosses_FormationLosses = AcessTypes.AT_ReadOnly;
                mudLosses_LossesRecord = AcessTypes.AT_ReadOnly;
                //-----------
                mudVolumeManagement_DFSs = AcessTypes.AT_ReadOnly;
                mudVolumeManagement_Pit_TankVolume = AcessTypes.AT_ReadOnly;
                mudVolumeManagement_TotalVolManagement = AcessTypes.AT_ReadOnly;
                //-----------
                mudProperties_MudProperties = AcessTypes.AT_ReadOnly;
                mudProperties_Water = AcessTypes.AT_ReadOnly;
                //-----------
                hydraulic = AcessTypes.AT_WriteAndRead;
                //-----------
                solidAnalysis = AcessTypes.AT_ReadOnly;
                //-----------
                inventoryManagement_ProductInventory = AcessTypes.AT_ReadOnly;
                inventoryManagement_RigEquipmentInventory = AcessTypes.AT_ReadOnly;
                inventoryManagement_ManPower = AcessTypes.AT_ReadOnly;
                inventoryManagement_LabEquipmentInventory = AcessTypes.AT_ReadOnly;
                inventoryManagement_HSE_PPE = AcessTypes.AT_ReadOnly;
                inventoryManagement_GeneralEquipmentInventory = AcessTypes.AT_ReadOnly;
                //-----------
                remarks = AcessTypes.AT_ReadOnly;
                //-----------
                generalDBs = AcessTypes.AT_ReadOnly;
                preDefValues = AcessTypes.AT_None;
            }
            else if (role == "Operation Manager")
            {
                project_TreeView = AcessTypes.AT_WriteAndRead;
                project_GeneralInformation = AcessTypes.AT_ReadOnly;
                project_MasterProductList = AcessTypes.AT_ReadOnly;
                project_Units = AcessTypes.AT_ReadOnly;
                project_ServicePriceList = AcessTypes.AT_ReadOnly;
                //-----------
                dfp_TreeView = AcessTypes.AT_ReadOnly;
                dfp_WellDesign = AcessTypes.AT_ReadOnly;
                dfp_Geo = AcessTypes.AT_ReadOnly;
                dfp_DFS = AcessTypes.AT_ReadOnly;
                //-----------
                //DMR = AcessTypes.AT_ReadOnly;
                //-----------
                rig_TreeView = AcessTypes.AT_WriteAndRead;
                rig_RigInfo = AcessTypes.AT_ReadOnly;
                rig_MudPumps = AcessTypes.AT_ReadOnly;
                rig_MudPits_Tanks = AcessTypes.AT_ReadOnly;
                rig_SolidControlEquipments = AcessTypes.AT_ReadOnly;
                rig_BulkSystem = AcessTypes.AT_ReadOnly;
                //-----------
                well_TreeView = AcessTypes.AT_WriteAndRead;
                well_General = AcessTypes.AT_ReadOnly;
                well_CasingData = AcessTypes.AT_ReadOnly;
                well_Geo = AcessTypes.AT_ReadOnly;
                //-----------
                rigwell_TreeView = AcessTypes.AT_ReadOnly;
                rigwell_Reports = AcessTypes.AT_ReadOnly;
                //-----------
                geologyInformation = AcessTypes.AT_ReadOnly;
                //-----------
                solidControlEquipment = AcessTypes.AT_ReadOnly;
                //-----------
                drillingOperation_HoleGeometry = AcessTypes.AT_ReadOnly;
                drillingOperation_IADC_Hours = AcessTypes.AT_ReadOnly;
                drillingOperation_DailyOperatingData = AcessTypes.AT_ReadOnly;
                drillingOperation_PerformanceDrilling = AcessTypes.AT_ReadOnly;
                //-----------
                mudLosses_FormationLosses = AcessTypes.AT_ReadOnly;
                mudLosses_LossesRecord = AcessTypes.AT_ReadOnly;
                //-----------
                mudVolumeManagement_DFSs = AcessTypes.AT_ReadOnly;
                mudVolumeManagement_Pit_TankVolume = AcessTypes.AT_ReadOnly;
                mudVolumeManagement_TotalVolManagement = AcessTypes.AT_ReadOnly;
                //-----------
                mudProperties_MudProperties = AcessTypes.AT_ReadOnly;
                mudProperties_Water = AcessTypes.AT_ReadOnly;
                //-----------
                hydraulic = AcessTypes.AT_ReadOnly;
                //-----------
                solidAnalysis = AcessTypes.AT_ReadOnly;
                //-----------
                inventoryManagement_ProductInventory = AcessTypes.AT_ReadOnly;
                inventoryManagement_RigEquipmentInventory = AcessTypes.AT_ReadOnly;
                inventoryManagement_ManPower = AcessTypes.AT_ReadOnly;
                inventoryManagement_LabEquipmentInventory = AcessTypes.AT_ReadOnly;
                inventoryManagement_HSE_PPE = AcessTypes.AT_ReadOnly;
                inventoryManagement_GeneralEquipmentInventory = AcessTypes.AT_ReadOnly;
                //-----------
                remarks = AcessTypes.AT_ReadOnly;
                //-----------
                generalDBs = AcessTypes.AT_ReadOnly;
                preDefValues = AcessTypes.AT_None;
            }
            else if (role == "Project Engineer")
            {
                project_TreeView = AcessTypes.AT_WriteAndRead;
                project_GeneralInformation = AcessTypes.AT_ReadOnly;
                project_MasterProductList = AcessTypes.AT_ReadOnly;
                project_Units = AcessTypes.AT_ReadOnly;
                project_ServicePriceList = AcessTypes.AT_ReadOnly;
                //-----------
                dfp_TreeView = AcessTypes.AT_ReadOnly;
                dfp_WellDesign = AcessTypes.AT_ReadOnly;
                dfp_Geo = AcessTypes.AT_ReadOnly;
                dfp_DFS = AcessTypes.AT_ReadOnly;
                //-----------
                //DMR = AcessTypes.AT_ReadOnly;
                //-----------
                rig_TreeView = AcessTypes.AT_WriteAndRead;
                rig_RigInfo = AcessTypes.AT_ReadOnly;
                rig_MudPumps = AcessTypes.AT_ReadOnly;
                rig_MudPits_Tanks = AcessTypes.AT_ReadOnly;
                rig_SolidControlEquipments = AcessTypes.AT_ReadOnly;
                rig_BulkSystem = AcessTypes.AT_ReadOnly;
                //-----------
                well_TreeView = AcessTypes.AT_WriteAndRead;
                well_General = AcessTypes.AT_ReadOnly;
                well_CasingData = AcessTypes.AT_ReadOnly;
                well_Geo = AcessTypes.AT_ReadOnly;
                //-----------
                rigwell_TreeView = AcessTypes.AT_ReadOnly;
                rigwell_Reports = AcessTypes.AT_ReadOnly;
                //-----------
                geologyInformation = AcessTypes.AT_ReadOnly;
                //-----------
                solidControlEquipment = AcessTypes.AT_ReadOnly;
                //-----------
                drillingOperation_HoleGeometry = AcessTypes.AT_ReadOnly;
                drillingOperation_IADC_Hours = AcessTypes.AT_ReadOnly;
                drillingOperation_DailyOperatingData = AcessTypes.AT_ReadOnly;
                drillingOperation_PerformanceDrilling = AcessTypes.AT_ReadOnly;
                //-----------
                mudLosses_FormationLosses = AcessTypes.AT_ReadOnly;
                mudLosses_LossesRecord = AcessTypes.AT_ReadOnly;
                //-----------
                mudVolumeManagement_DFSs = AcessTypes.AT_ReadOnly;
                mudVolumeManagement_Pit_TankVolume = AcessTypes.AT_ReadOnly;
                mudVolumeManagement_TotalVolManagement = AcessTypes.AT_ReadOnly;
                //-----------
                mudProperties_MudProperties = AcessTypes.AT_ReadOnly;
                mudProperties_Water = AcessTypes.AT_ReadOnly;
                //-----------
                hydraulic = AcessTypes.AT_ReadOnly;
                //-----------
                solidAnalysis = AcessTypes.AT_ReadOnly;
                //-----------
                inventoryManagement_ProductInventory = AcessTypes.AT_ReadOnly;
                inventoryManagement_RigEquipmentInventory = AcessTypes.AT_ReadOnly;
                inventoryManagement_ManPower = AcessTypes.AT_ReadOnly;
                inventoryManagement_LabEquipmentInventory = AcessTypes.AT_ReadOnly;
                inventoryManagement_HSE_PPE = AcessTypes.AT_ReadOnly;
                inventoryManagement_GeneralEquipmentInventory = AcessTypes.AT_ReadOnly;
                //-----------
                remarks = AcessTypes.AT_ReadOnly;
                //-----------
                generalDBs = AcessTypes.AT_ReadOnly;
                preDefValues = AcessTypes.AT_None;
            }
            else if (role == "Site/Mud Engineer")
            {
                project_TreeView = AcessTypes.AT_WriteAndRead;
                project_GeneralInformation = AcessTypes.AT_ReadOnly;
                project_MasterProductList = AcessTypes.AT_ReadOnly;
                project_Units = AcessTypes.AT_ReadOnly;
                project_ServicePriceList = AcessTypes.AT_None;
                //-----------
                dfp_TreeView = AcessTypes.AT_ReadOnly;
                dfp_WellDesign = AcessTypes.AT_ReadOnly;
                dfp_Geo = AcessTypes.AT_ReadOnly;
                dfp_DFS = AcessTypes.AT_ReadOnly;
                //-----------
                //DMR = AcessTypes.AT_ReadOnly;
                //-----------
                rig_TreeView = AcessTypes.AT_WriteAndRead;
                rig_RigInfo = AcessTypes.AT_WriteAndRead;
                rig_MudPumps = AcessTypes.AT_WriteAndRead;
                rig_MudPits_Tanks = AcessTypes.AT_WriteAndRead;
                rig_SolidControlEquipments = AcessTypes.AT_WriteAndRead;
                rig_BulkSystem = AcessTypes.AT_WriteAndRead;
                //-----------
                well_TreeView = AcessTypes.AT_WriteAndRead;
                well_General = AcessTypes.AT_WriteAndRead;
                well_CasingData = AcessTypes.AT_WriteAndRead;
                well_Geo = AcessTypes.AT_WriteAndRead;
                //-----------
                rigwell_TreeView = AcessTypes.AT_WriteAndRead;
                rigwell_Reports = AcessTypes.AT_WriteAndRead;
                //-----------
                geologyInformation = AcessTypes.AT_WriteAndRead;
                //-----------
                solidControlEquipment = AcessTypes.AT_WriteAndRead;
                //-----------
                drillingOperation_HoleGeometry = AcessTypes.AT_WriteAndRead;
                drillingOperation_IADC_Hours = AcessTypes.AT_WriteAndRead;
                drillingOperation_DailyOperatingData = AcessTypes.AT_WriteAndRead;
                drillingOperation_PerformanceDrilling = AcessTypes.AT_WriteAndRead;
                //-----------
                mudLosses_FormationLosses = AcessTypes.AT_WriteAndRead;
                mudLosses_LossesRecord = AcessTypes.AT_WriteAndRead;
                //-----------
                mudVolumeManagement_DFSs = AcessTypes.AT_WriteAndRead;
                mudVolumeManagement_Pit_TankVolume = AcessTypes.AT_WriteAndRead;
                mudVolumeManagement_TotalVolManagement = AcessTypes.AT_WriteAndRead;
                //-----------
                mudProperties_MudProperties = AcessTypes.AT_WriteAndRead;
                mudProperties_Water = AcessTypes.AT_WriteAndRead;
                //-----------
                hydraulic = AcessTypes.AT_WriteAndRead;
                //-----------
                solidAnalysis = AcessTypes.AT_WriteAndRead;
                //-----------
                inventoryManagement_ProductInventory = AcessTypes.AT_WriteAndRead;
                inventoryManagement_RigEquipmentInventory = AcessTypes.AT_WriteAndRead;
                inventoryManagement_ManPower = AcessTypes.AT_WriteAndRead;
                inventoryManagement_LabEquipmentInventory = AcessTypes.AT_WriteAndRead;
                inventoryManagement_HSE_PPE = AcessTypes.AT_WriteAndRead;
                inventoryManagement_GeneralEquipmentInventory = AcessTypes.AT_WriteAndRead;
                //-----------
                remarks = AcessTypes.AT_WriteAndRead;
                //-----------
                generalDBs = AcessTypes.AT_ReadOnly;
                preDefValues = AcessTypes.AT_ReadOnly;
            }
            else if (role == "Read Only User")
            {
                project_TreeView = AcessTypes.AT_ReadOnly;
                project_GeneralInformation = AcessTypes.AT_ReadOnly;
                project_MasterProductList = AcessTypes.AT_ReadOnly;
                project_Units = AcessTypes.AT_ReadOnly;
                project_ServicePriceList = AcessTypes.AT_None;
                //-----------
                dfp_TreeView = AcessTypes.AT_ReadOnly;
                dfp_WellDesign = AcessTypes.AT_ReadOnly;
                dfp_Geo = AcessTypes.AT_ReadOnly;
                dfp_DFS = AcessTypes.AT_ReadOnly;
                //-----------
                //DMR = AcessTypes.AT_ReadOnly;
                //-----------
                rig_TreeView = AcessTypes.AT_ReadOnly;
                rig_RigInfo = AcessTypes.AT_ReadOnly;
                rig_MudPumps = AcessTypes.AT_ReadOnly;
                rig_MudPits_Tanks = AcessTypes.AT_ReadOnly;
                rig_SolidControlEquipments = AcessTypes.AT_ReadOnly;
                rig_BulkSystem = AcessTypes.AT_ReadOnly;
                //-----------
                well_TreeView = AcessTypes.AT_ReadOnly;
                well_General = AcessTypes.AT_ReadOnly;
                well_CasingData = AcessTypes.AT_ReadOnly;
                well_Geo = AcessTypes.AT_ReadOnly;
                //-----------
                rigwell_TreeView = AcessTypes.AT_ReadOnly;
                rigwell_Reports = AcessTypes.AT_ReadOnly;
                //-----------
                geologyInformation = AcessTypes.AT_ReadOnly;
                //-----------
                solidControlEquipment = AcessTypes.AT_ReadOnly;
                //-----------
                drillingOperation_HoleGeometry = AcessTypes.AT_ReadOnly;
                drillingOperation_IADC_Hours = AcessTypes.AT_ReadOnly;
                drillingOperation_DailyOperatingData = AcessTypes.AT_ReadOnly;
                drillingOperation_PerformanceDrilling = AcessTypes.AT_ReadOnly;
                //-----------
                mudLosses_FormationLosses = AcessTypes.AT_ReadOnly;
                mudLosses_LossesRecord = AcessTypes.AT_ReadOnly;
                //-----------
                mudVolumeManagement_DFSs = AcessTypes.AT_ReadOnly;
                mudVolumeManagement_Pit_TankVolume = AcessTypes.AT_ReadOnly;
                mudVolumeManagement_TotalVolManagement = AcessTypes.AT_ReadOnly;
                //-----------
                mudProperties_MudProperties = AcessTypes.AT_ReadOnly;
                mudProperties_Water = AcessTypes.AT_ReadOnly;
                //-----------
                hydraulic = AcessTypes.AT_ReadOnly;
                //-----------
                solidAnalysis = AcessTypes.AT_ReadOnly;
                //-----------
                inventoryManagement_ProductInventory = AcessTypes.AT_ReadOnly;
                inventoryManagement_RigEquipmentInventory = AcessTypes.AT_ReadOnly;
                inventoryManagement_ManPower = AcessTypes.AT_ReadOnly;
                inventoryManagement_LabEquipmentInventory = AcessTypes.AT_ReadOnly;
                inventoryManagement_HSE_PPE = AcessTypes.AT_ReadOnly;
                inventoryManagement_GeneralEquipmentInventory = AcessTypes.AT_ReadOnly;
                //-----------
                remarks = AcessTypes.AT_ReadOnly;
                //-----------
                generalDBs = AcessTypes.AT_ReadOnly;
                preDefValues = AcessTypes.AT_None;
            }

#if !OFFICE_EDITION
            if (project_TreeView != AcessTypes.AT_None) project_TreeView = AcessTypes.AT_ReadOnly;
            if (project_GeneralInformation != AcessTypes.AT_None) project_GeneralInformation = AcessTypes.AT_ReadOnly;
            if (project_MasterProductList != AcessTypes.AT_None) project_MasterProductList = AcessTypes.AT_ReadOnly;
            if (project_Units != AcessTypes.AT_None) project_Units = AcessTypes.AT_ReadOnly;
            if (project_ServicePriceList != AcessTypes.AT_None) project_ServicePriceList = AcessTypes.AT_ReadOnly;
            //-----------
            //no write access to dfp
            if (dfp_TreeView != AcessTypes.AT_None) dfp_TreeView = AcessTypes.AT_ReadOnly;
            if (dfp_WellDesign != AcessTypes.AT_None) dfp_WellDesign = AcessTypes.AT_ReadOnly;
            if (dfp_Geo != AcessTypes.AT_None) dfp_Geo = AcessTypes.AT_ReadOnly;
            if (dfp_DFS != AcessTypes.AT_None) dfp_DFS = AcessTypes.AT_ReadOnly;
            //-----------
            if (generalDBs != AcessTypes.AT_None) generalDBs = AcessTypes.AT_ReadOnly;
            if (preDefValues != AcessTypes.AT_None) preDefValues = AcessTypes.AT_ReadOnly;
#endif

        }
        //-------------------------------------------------------


    }
}
