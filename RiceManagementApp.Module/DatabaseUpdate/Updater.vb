Imports Microsoft.VisualBasic
Imports System
Imports System.Linq
Imports DevExpress.ExpressApp
Imports DevExpress.Data.Filtering
Imports DevExpress.Persistent.Base
Imports DevExpress.ExpressApp.Updating
Imports DevExpress.ExpressApp.Security
Imports DevExpress.ExpressApp.Security.Strategy
Imports DevExpress.Xpo
Imports DevExpress.ExpressApp.Xpo
Imports DevExpress.Persistent.BaseImpl

Public Class Updater
    Inherits ModuleUpdater
    Public Sub New(ByVal objectSpace As IObjectSpace, ByVal currentDBVersion As Version)
        MyBase.New(objectSpace, currentDBVersion)
    End Sub

    Public Overrides Sub UpdateDatabaseAfterUpdateSchema()
        MyBase.UpdateDatabaseAfterUpdateSchema()
        Dim IsUser As User = ObjectSpace.FindObject(Of User)(CriteriaOperator.Parse("UserName == 'admin'"))

        If IsUser Is Nothing Then
            Dim adminRole As Role = ObjectSpace.FindObject(Of Role)(New BinaryOperator("Name", SecurityStrategy.AdministratorRoleName))
            If adminRole Is Nothing Then
                adminRole = ObjectSpace.CreateObject(Of Role)()
                adminRole.Name = SecurityStrategy.AdministratorRoleName
                adminRole.Description = "เจ้าหน้าที่ดูแลระบบ"
                adminRole.IsAdministrative = True
                adminRole.Save()
            End If

            Dim user1 As User = ObjectSpace.FindObject(Of User)(New BinaryOperator("UserName", "admin"))
            If user1 Is Nothing Then
                user1 = ObjectSpace.CreateObject(Of User)()
                user1.UserName = "admin"
                user1.DisplayName = "เจ้าหน้าที่ดูแลระบบ"
                ' Set a password if the standard authentication type is used 
                user1.SetPassword("")
                user1.Roles.Add(adminRole)
            End If
        End If

        '//สร้าง Role พร้อมตั้งต่า Type Permission
        SetRolePermission_Plan()
        SetRolePermission_Audit()
        SetRolePermission_Factory()
        SetRolePermission_Inventory()
        SetRolePermission_Account()
        SetRolePermission_Sale()
        SetRolePermission_Association()
        '//--------------------------------

        '//ตั้งค่าข้อมูลศูนย์ (SiteSetting) สร้างให้ตั้งแต่แรก เนื่องจาก UI เป็น DetailView บังคับให้มีเพียง 1 รายการ ต่อ ศูนย์
        If ObjectSpace.GetObjectsCount(GetType(SiteSetting), Nothing) = 0 Then
            Dim siteSetting As SiteSetting = ObjectSpace.CreateObject(Of SiteSetting)()
            siteSetting.Save()
        End If

        'If ObjectSpace.GetObjectsCount(GetType(BuyQualitySettings), Nothing) = 0 Then
        '    Dim buyQualitySetting As BuyQualitySettings = ObjectSpace.CreateObject(Of BuyQualitySettings)()
        '    buyQualitySetting.Save()
        'End If

    End Sub

    Public Sub SetRolePermission_Plan()
        Dim RoleName As String = "Plan"
        Dim _Role As Role = ObjectSpace.FindObject(Of Role)(New BinaryOperator("Name", RoleName))

        If _Role Is Nothing Then
            _Role = ObjectSpace.CreateObject(Of Role)()
            _Role.Name = RoleName
            _Role.Description = "กลุ่มพัฒนาการขยายเมล็ดพันธุ์(แผน,แปลง)"
            _Role.IsAdministrative = False

            '_Role.SetTypePermissions(GetType(DevExpress.Persistent.BaseImpl.ReportDataV2), "Read;Navigate;", SecuritySystemModifier.Allow)
            
            _Role.Save()

        End If

        AddTypePermissionToRole(_Role, GetType(DevExpress.Persistent.BaseImpl.ReportDataV2), "Read;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.ApproveBuy), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.ArrangeSeedLotDetail), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.AssociationBusiness), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.AssociationDeal), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.AssociationGroup), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.AssociationMember), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.AssociationPosition), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.AssociationSeed), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.AuditActivityPlan), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.AuditBuyPlanDetail), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.AuditCheckFarmPlanDetail), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.AuditGrowPlanDetail), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.AuditKeepPlanDetail), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.AuditProcessPlanDetail), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.AuditSalePlanDetail), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.BuyFarmer), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.BuyPlan), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.BuyPlanDetail), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.BuyPosition), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.BuyQualitySettings), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.BuySeed), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.BuySeedWeight), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.CheckFarm), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.CustomAddress), "Read;Write;Create;Delete;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.District), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.Employee), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.EstimateByGrowType), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.Farm), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.Farmer), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.FarmerGroup), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.GrowPlan), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.GrowPlanDetail), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.GrowType), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.HarvestPlan), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.HarvestPlanDetail), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.MainPlan), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.OrganizationSection), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.PlanAssignBuyTeam), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.PlanFarmer), "Read;Write;Create;Delete;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.PlanFarmerFarm), "Read;Write;Create;Delete;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.PlanFarmerGroup), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.PlanFarmerGrow), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.PlanFarmerSeedSouce), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.Plant), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.PrefixName), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.Province), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.QualityAudit), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.QualityAuditStep), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.Season), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.SeedClass), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.SeedPrice), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.SeedStatus), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.SeedType), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.Site), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.SubDistrict), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.SubmitActivityPlanReport), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.SubmitPlantKP3), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.SubmitPlantKP3Detail), "Read;Write;Create;Delete;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.SubmitPlantKP5), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.SubmitPlantKP5Detail), "Read;Write;Create;Delete;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.SubmitPlantKP2), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.SubmitPlantKP2Detail), "Read;Write;Create;Delete;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.SummaryBuy), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.SummaryBuyFarmer), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)

    End Sub

    Public Sub SetRolePermission_Audit()
        Dim RoleName As String = "Audit"
        Dim _Role As Role = ObjectSpace.FindObject(Of Role)(New BinaryOperator("Name", RoleName))
        If _Role Is Nothing Then
            _Role = ObjectSpace.CreateObject(Of Role)()
            _Role.Name = RoleName
            _Role.Description = "กลุ่มควบคุมคุณภาพ(ตรวจสอบ)"
            _Role.IsAdministrative = False

            _Role.Save()

        End If

        AddTypePermissionToRole(_Role, GetType(DevExpress.Persistent.BaseImpl.ReportDataV2), "Read;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.AuditActivityPlan), "Read;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.AuditBuyPlanDetail), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.AuditCheckFarmPlanDetail), "Read;Write;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.AuditGrowPlanDetail), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.AuditKeepPlanDetail), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.AuditProcessPlanDetail), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.AuditSalePlanDetail), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.BuyFarmer), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.BuyPlan), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.BuyPosition), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.BuySeedWeight), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.CheckFarm), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.CustomAddress), "Read;Write;Create;Delete;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.Employee), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.EstimateByGrowType), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.Farm), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.FarmerGroup), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.GrowPlan), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.GrowType), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.HarvestPlan), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.MainPlan), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.MoneyType), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.OrganizationSection), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.PlanFarmer), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.PlanFarmerGroup), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.Plant), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.PrefixName), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.Province), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.QualityAudit), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.QualityAuditStep), "Read;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.SalePrice), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.Season), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.SeedClass), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.SeedPrice), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.SeedProduct), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.SeedStatus), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.SeedType), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.SiteSetting), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.SubDistrict), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.SubmitActivityPlanReport), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.SubmitAuditActivity), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.SubmitAuditActivityDetail), "Read;Write;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.SubmitCheckFarmSummary), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.SubmitCheckFarmSummaryDetail), "Read;Write;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.SubmitCheckQualityForMonth), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.SubmitCheckQualityForMonthDetail), "Read;Write;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.SubmitCheckRedSeed), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.SubmitCheckRedSeedDetail), "Read;Write;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.SubmitQualityBetweenStorage), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.SubmitQualityBetweenStorageDetail), "Read;Write;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.SubmitQualityForBuy), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.SubmitQualityForBuyDetail), "Read;Write;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.SubmitQualityHistory), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.SubmitQualityHistoryDetail), "Read;Write;", SecuritySystemModifier.Allow)

        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.SubmitCheckQuality), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.SubmitCheckQualityDetail), "Read;Write;", SecuritySystemModifier.Allow)

    End Sub

    Public Sub SetRolePermission_Factory()
        Dim RoleName As String = "Factory"
        Dim _Role As Role = ObjectSpace.FindObject(Of Role)(New BinaryOperator("Name", RoleName))
        If _Role Is Nothing Then
            _Role = ObjectSpace.CreateObject(Of Role)()
            _Role.Name = RoleName
            _Role.Description = "กลุ่มกรรมพัฒนากรรมวิธีการผลิตและโรงงาน(โรงงาน)"
            _Role.IsAdministrative = False

            _Role.Save()

        End If

        AddTypePermissionToRole(_Role, GetType(DevExpress.Persistent.BaseImpl.ReportDataV2), "Read;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.ArrangeSeedLot), "Read;Write;Create;Delete;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.AuditActivityPlan), "Read;Write;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.AuditBuyPlanDetail), "Read;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.AuditCheckFarmPlanDetail), "Read;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.AuditGrowPlanDetail), "Read;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.AuditKeepPlanDetail), "Read;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.AuditProcessPlanDetail), "Read;Write;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.AuditSalePlanDetail), "Read;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.BuyPlan), "Read;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.CustomAddress), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.District), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.Employee), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.FactoryInventory), "Read;Write;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.FactoryMaterialInventory), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.FactoryMaterialTransaction), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.FactoryPickMaterial), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.FactoryPickMaterialDetail), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.FactoryPickProcess), "Read;Write;Create;Delete;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.FactoryPickProcessDetail), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.FactoryPickSeed), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.FactoryPickSeedDetail), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.FactoryReturnSeed), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.FactoryReturnSeedDetail), "Read;Write;Create;Delete;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.FactorySeedProcess), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.FactorySeedProcessDetail), "Read;Write;Create;Delete;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.FactorySeedProcessToday), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.FactoryTransaction), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.GrowPlan), "Read;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.HarvestPlan), "Read;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.MainPlan), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.Material), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.MaterialProduct), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.MaterialTransaction), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.MoneyType), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.OrganizationSection), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.PickMaterial), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.PickMaterialDetail), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.PickSeed), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.PickType), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.Plant), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.PrefixName), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.Province), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.ReceiveMaterialDetail), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.ReceiveType), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.RestoreMaterialDetail), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.SaleDetail), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.Season), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.SeedClass), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.SeedProduct), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.SeedStatus), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.SeedTransaction), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.SeedType), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.Site), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.SubDistrict), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.SubmitProcess), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.SubmitProcessDetail), "Read;Write;Create;Delete;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.TransferMaterialDetail), "Read;", SecuritySystemModifier.Allow)

    End Sub

    Public Sub SetRolePermission_Inventory()
        Dim RoleName As String = "Inventory"
        Dim _Role As Role = ObjectSpace.FindObject(Of Role)(New BinaryOperator("Name", RoleName))
        If _Role Is Nothing Then
            _Role = ObjectSpace.CreateObject(Of Role)()
            _Role.Name = RoleName
            _Role.Description = "กลุ่มพัฒนาธุรกิจเมล็ดพันธุ์ (ตลาด)"
            _Role.IsAdministrative = False

            _Role.Save()

        End If

        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.ArrangeSeedLot), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.ArrangeSeedLotDetail), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.AssociationSeed), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.AuditActivityPlan), "Read;Write;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.AuditBuyPlanDetail), "Read;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.AuditCheckFarmPlanDetail), "Read;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.AuditGrowPlanDetail), "Read;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.AuditKeepPlanDetail), "Read;Write;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.AuditProcessPlanDetail), "Read;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.AuditSalePlanDetail), "Read;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.BuyFarmer), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.BuyPlan), "Read;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.BuySeed), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.BuySeedWeight), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.CostTypeID), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.CustomAddress), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.District), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.Employee), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.FactoryMaterialTransaction), "Read;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.FactoryPickMaterial), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.FactoryPickMaterialDetail), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.FactoryPickSeed), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.FactoryPickSeedDetail), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.FactoryReturnSeedDetail), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.FactoryTransaction), "Read;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.GrowPlan), "Read;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.HarvestPlan), "Read;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.MainPlan), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.Material), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.MaterialProduct), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.MaterialTransaction), "Read;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.Member), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.MoneyType), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.OrganizationSection), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.PickMaterial), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.PickMaterialDetail), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.PickSeed), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.PickSeedDetail), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.PickType), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.DemoPlanBudget02), "Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.PlanFarmer), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.PlanFarmerGroup), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.PlanFarmerSeedSouce), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.Plant), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.PrefixName), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.Province), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.ReceiveMaterial), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.ReceiveMaterialDetail), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.ReceiveSeed), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.ReceiveSeedDetail), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.ReceiveTransferMaterial), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.ReceiveTransferMaterialDetail), "Read;Write;Create;Delete;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.ReceiveTransferSeed), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.ReceiveTransferSeedDetail), "Read;Write;Create;Delete;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.ReceiveType), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.ReserveSeed), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.ReserveSeedDetail), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.RestoreMaterial), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.RestoreMaterialDetail), "Read;Create;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.Sale), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.SaleDetail), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.SalePrice), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.Season), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.SeedClass), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.SeedPrice), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.SeedProduct), "Read;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.SeedStatus), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.SeedTransaction), "Read;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.SeedType), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.Site), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.SiteSetting), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.SubDistrict), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.SummaryBuyFarmer), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.TransferMaterial), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.TransferMaterialDetail), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.TransferSeed), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.TransferSeedDetail), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)

    End Sub

    Public Sub SetRolePermission_Account()
        Dim RoleName As String = "Account"
        Dim _Role As Role = ObjectSpace.FindObject(Of Role)(New BinaryOperator("Name", RoleName))
        If _Role Is Nothing Then
            _Role = ObjectSpace.CreateObject(Of Role)()
            _Role.Name = RoleName
            _Role.Description = "กลุ่มบริหารทั่วไป(การเงิน,บัญชี)"
            _Role.IsAdministrative = False

            _Role.Save()

        End If

        AddTypePermissionToRole(_Role, GetType(DevExpress.Persistent.BaseImpl.ReportDataV2), "Read;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.AccCost), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.AccCostID), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.Account), "Read;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.AccountBalanceType), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.AccountBID), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.AccountBookID), "Read;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.AccountBookJV), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.AccountBookPV), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.AccountBookRV), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.AccountGFMIS), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.AccountGFMISDetailPV), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.AccountGFMISDetailRV), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.AccountGroup), "Read;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.AccountID), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.AccountPeriod), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.AccountPeriodDetail), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.AccountReportNav), "Read;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.AccountType), "Read;Navigate;", SecuritySystemModifier.Allow)
        'AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.ActivityName), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.Bringforward), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.ConfigAccount), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.CostTypeID), "Read;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.CustomAddress), "Read;Write;Create;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.District), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.EventAccCostID), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.EventAccountBID), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.EventBringforward), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        'AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.EventGLID), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.EventPayDateID), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.ExpenseCategories), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.Forecast), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.ForecastDetail), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        'AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.GFMISTypePV), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        'AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.GFMISTypeRV), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.GL), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.MainPlan), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.Material), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.MaterialProduct), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.MoneyType), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.PayDateDetail), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.DemoPlanBudget02), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.DemoPlanBudgetDetail02), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.Plant), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.PrefixName), "Read;", SecuritySystemModifier.Allow)
        'AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.ProjectName), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.Province), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.DemoResultBudget02), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.SalePrice), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.Season), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.SeedClass), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.SeedPrice), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.SeedProduct), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.SeedStatus), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.SeedType), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.SiteSetting), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.SubDistrict), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.SubmitAccCostReport), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.SubmitPayDateReport), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.SubmitTrialBalacneReport), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.UnitID), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.Vacation), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.VacationDetail), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.VacationSetting), "Read;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.TrialBalanceTransfer03), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.TrialBalanceTransferDetailExpenses03), "Read;Write;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.TrialBalanceTransferDetailIncome03), "Read;Write;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.IncomeAndExpensesOfMonth), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.TrialBalanceExpenses), "Read;Write;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.TrialBalanceIncome), "Read;Write;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.PlanBudget01), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.PlanBudgetDetail01), "Read;Write;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.PlanBudgetDetailMonth01), "Read;Write;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.ResultBudget01), "Read;Write;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.SettingPlan), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.SettingProject), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.SettingActivity), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.SettingSubActivity), "Read;", SecuritySystemModifier.Allow)

    End Sub

    Public Sub SetRolePermission_Sale()
        Dim RoleName As String = "Sale"
        Dim _Role As Role = ObjectSpace.FindObject(Of Role)(New BinaryOperator("Name", RoleName))
        If _Role Is Nothing Then
            _Role = ObjectSpace.CreateObject(Of Role)()
            _Role.Name = RoleName
            _Role.Description = "เจ้าหน้าที่ส่วนงานจำหน่ายเมล็ดพันธุ์"
            _Role.IsAdministrative = False

            _Role.Save()

        End If

        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.CostTypeID), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.CustomAddress), "Read;Write;Create;Delete;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.District), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.Employee), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.FactoryInventory), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.FactoryPickSeedDetail), "Read;Write;Create;Delete;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.Farmer), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.MainPlan), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.Member), "Read;Write;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.MoneyType), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.Plant), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.PrefixName), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.Province), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.ReserveSeed), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.ReserveSeedDetail), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.Sale), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.SaleDetail), "Read;Write;Create;Delete;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.SalePrice), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.Season), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.SeedClass), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.SeedProduct), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.SeedStatus), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.SeedType), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.Site), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.SiteSetting), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.SubDistrict), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.User), "Read;", SecuritySystemModifier.Allow)

    End Sub

    Public Sub SetRolePermission_Association()
        Dim RoleName As String = "Association"
        Dim _Role As Role = ObjectSpace.FindObject(Of Role)(New BinaryOperator("Name", RoleName))
        If _Role Is Nothing Then
            _Role = ObjectSpace.CreateObject(Of Role)()
            _Role.Name = RoleName
            _Role.Description = "เจ้าหน้าที่จัดการรายชื่อสมาชิกชมรมฯ"
            _Role.IsAdministrative = False

            _Role.Save()

        End If

        'AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.AssociationBusiness), "Read;", SecuritySystemModifier.Allow)

        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.AssociationBusiness), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.AssociationDeal), "Read;Write;Create;Delete;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.AssociationGroup), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.AssociationMember), "Read;Write;Create;Delete;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.AssociationPosition), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.AssociationSeed), "Read;Write;Create;Delete;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.BusinessClass), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.CustomAddress), "Read;Write;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.District), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.Plant), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.PrefixName), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.Province), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.Season), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.SeedClass), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.SeedClubs), "Read;Write;Create;Delete;Navigate;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.SeedMembers), "Read;Write;Create;Delete;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.SeedType), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.SiteSetting), "Read;", SecuritySystemModifier.Allow)
        AddTypePermissionToRole(_Role, GetType(RiceManagementApp.Module.SubDistrict), "Read;", SecuritySystemModifier.Allow)

    End Sub

    ''' <summary>
    ''' Natthapong Pongrat
    ''' 2015-07-23
    ''' สำหรับเพิ่มข้อมูลสิทธิ์การเข้าถึงข้อมูลของ Role (Set Type Permission)
    ''' </summary>
    ''' <param name="_Role">Role ที่ต้องการ Set ค่า</param>
    ''' <param name="_Type">System.Type เพื่อระบุ Class ที่ต้องการ Set Permission</param>
    ''' <param name="_Permissions">สิทธุ์การเข้าถึงข้อมูล e.g. "Read;Write;Create;Delete;Navigate;"</param>
    ''' <param name="_Modifier">Modifier as SecuritySystemModifier</param>
    ''' <remarks></remarks>
    Public Sub AddTypePermissionToRole(_Role As Role, _Type As System.Type, _Permissions As String, _Modifier As SecuritySystemModifier)
        If _Role IsNot Nothing Then
            Dim objPermission As SecuritySystemTypePermissionObject = _Role.FindTypePermissionObject(_Type)
            If objPermission Is Nothing Then
                _Role.SetTypePermissions(_Type, _Permissions, _Modifier)
            End If
        End If
    End Sub

    Public Overrides Sub UpdateDatabaseBeforeUpdateSchema()
        MyBase.UpdateDatabaseBeforeUpdateSchema()
        'If (CurrentDBVersion < New Version("1.1.0.0") AndAlso CurrentDBVersion > New Version("0.0.0.0")) Then
        '    RenameColumn("DomainObject1Table", "OldColumnName", "NewColumnName")
        'End If
    End Sub
    Private Function CreateDefaultRole() As SecuritySystemRole
        Dim defaultRole As SecuritySystemRole = ObjectSpace.FindObject(Of SecuritySystemRole)(New BinaryOperator("Name", "Default"))
        If defaultRole Is Nothing Then
            defaultRole = ObjectSpace.CreateObject(Of SecuritySystemRole)()
            defaultRole.Name = "Default"

            defaultRole.AddObjectAccessPermission(Of SecuritySystemUser)("[Oid] = CurrentUserId()", SecurityOperations.ReadOnlyAccess)
            defaultRole.AddMemberAccessPermission(Of SecuritySystemUser)("ChangePasswordOnFirstLogon", SecurityOperations.Write)
            defaultRole.AddMemberAccessPermission(Of SecuritySystemUser)("StoredPassword", SecurityOperations.Write)
            defaultRole.SetTypePermissionsRecursively(Of SecuritySystemRole)(SecurityOperations.Read, SecuritySystemModifier.Allow)
            defaultRole.SetTypePermissionsRecursively(Of ModelDifference)(SecurityOperations.ReadWriteAccess, SecuritySystemModifier.Allow)
            defaultRole.SetTypePermissionsRecursively(Of ModelDifferenceAspect)(SecurityOperations.ReadWriteAccess, SecuritySystemModifier.Allow)
        End If
        Return defaultRole
    End Function
End Class