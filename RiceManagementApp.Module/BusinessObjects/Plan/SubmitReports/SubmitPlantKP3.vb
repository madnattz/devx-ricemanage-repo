Imports Microsoft.VisualBasic
Imports System
Imports System.Linq
Imports System.Text
Imports DevExpress.Xpo
Imports DevExpress.ExpressApp
Imports System.ComponentModel
Imports DevExpress.ExpressApp.DC
Imports DevExpress.Data.Filtering
Imports DevExpress.Persistent.Base
Imports System.Collections.Generic
Imports DevExpress.ExpressApp.Model
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Persistent.Validation
Imports DevExpress.ExpressApp.ConditionalAppearance
Imports DevExpress.Xpo.DB

<ImageName("sent3")> _
<XafDisplayName("ขพ.3")> _
<DefaultClassOptions()> _
<RuleCriteria("SubmitPlantKP3.NotDelete", DefaultContexts.Delete, "Status='Draft'", CustomMessageTemplate:="ไม่สามารถลบข้อมูลนี้ได้")> _
<ConditionalAppearance.Appearance("SubmitPlanKP3DisableAllItems", criteria:="Status!='Draft'", Enabled:=False, TargetItems:="*", Context:="DetailView")> _
Public Class SubmitPlantKP3
    Inherits BaseObject
    Implements ISubmitReportAble

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        Me.Status = PublicEnum.SubmitReportStatus.Draft
    End Sub

    Protected Overrides Sub OnLoaded()
        MyBase.OnLoaded()
        Me.SubmitPlantKP3Details.Sorting.Add(New SortProperty("FarmerNo", SortingDirection.Ascending))
    End Sub

    <Association("SubmitPlantKP3-SubmitPlantKP3Details", GetType(SubmitPlantKP3Detail))> _
    <DevExpress.Xpo.Aggregated> _
    <XafDisplayName("เกษตรกรผู้จัดทำแปลง")> _
    Public ReadOnly Property SubmitPlantKP3Details() As XPCollection(Of SubmitPlantKP3Detail)
        Get
            Return GetCollection(Of SubmitPlantKP3Detail)("SubmitPlantKP3Details")
        End Get
    End Property

    Dim fMainPlan As MainPlan
    <Index(1)>
    <RuleRequiredField(DefaultContexts.Save)> _
    <XafDisplayName("เป้าการผลิต")> _
    <ImmediatePostData()> _
    Public Property MainPlan() As MainPlan
        Get
            Return fMainPlan
        End Get
        Set(ByVal value As MainPlan)
            SetPropertyValue(Of MainPlan)("MainPlan", fMainPlan, value)
        End Set
    End Property

    Dim fSubmitDate As Date
    <Index(2)>
    <XafDisplayName("วันที่ส่งรายงาน")> _
     <Appearance("SubmitDate", Enabled:=False)> _
    Public Property SubmitDate() As Date
        Get
            Return fSubmitDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue(Of Date)("SubmitDate", fSubmitDate, value)
        End Set
    End Property

    Dim fSubmitBy As String
    <Index(3)>
    <XafDisplayName("ส่งรายงานโดย")> _
     <Appearance("SubmitBy", Enabled:=False)> _
    Public Property SubmitBy() As String
        Get
            Return fSubmitBy
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SubmitBy", fSubmitBy, value)
        End Set
    End Property

    Dim fStatus As PublicEnum.SubmitReportStatus
    <Index(4)>
    <XafDisplayName("สถานะ")> _
     <Appearance("Status", Enabled:=False)> _
    Public Property Status() As PublicEnum.SubmitReportStatus
        Get
            Return fStatus
        End Get
        Set(ByVal value As PublicEnum.SubmitReportStatus)
            SetPropertyValue(Of PublicEnum.SubmitReportStatus)("Status", fStatus, value)
        End Set
    End Property

    Private Sub GetPlanFarmer(listPlanFarmer As XPCollection(Of PlanFarmer))
        For Each item As PlanFarmer In listPlanFarmer
            Dim objItem As SubmitPlantKP3Detail = Session.FindObject(Of SubmitPlantKP3Detail)(CriteriaOperator.Parse("FarmerNo=? and SubmitPlantKP3=? ", item.Farmer.MemberID, Me))
            If objItem Is Nothing Then
                objItem = New SubmitPlantKP3Detail(Session)
                objItem.SubmitPlantKP3 = Me
                objItem.FarmerNo = item.Farmer.MemberID
                objItem.FullName = item.FullName
                objItem.GroupCode = item.Farmer.FarmerGroup.GroupCode
                objItem.GroupName = item.Farmer.FarmerGroup.GroupName
                objItem.AddressNo = item.Farmer.Address.AddressNo
                objItem.Moo = item.Farmer.Address.Moo
                objItem.SubDistrictName = GetAddressRefName(item.Farmer.Address, AddressType.SubDistrict)
                objItem.DistrictName = GetAddressRefName(item.Farmer.Address, AddressType.District)
                objItem.ProvinceName = GetAddressRefName(item.Farmer.Address, AddressType.Province)
                objItem.GroupAddressNo = item.Farmer.FarmerGroup.Address.AddressNo
                objItem.GroupMoo = item.Farmer.FarmerGroup.Address.Moo
                objItem.GroupSubDistrictName = GetAddressRefName(item.Farmer.FarmerGroup.Address, AddressType.SubDistrict)
                objItem.GroupDistrictName = GetAddressRefName(item.Farmer.FarmerGroup.Address, AddressType.District)
                objItem.GroupProvinceName = GetAddressRefName(item.Farmer.FarmerGroup.Address, AddressType.Province)
                objItem.TotalGrowArea = item.TotalGrowArea
                objItem.TotalSeedReceive = item.TotalSeedReceive
                objItem.TotalSeedUse = item.TotalSeedUse
                objItem.MaxBuyQuantity = item.MaxBuyQuantity
                objItem.GrowStartDate = item.PlanFarmerGroup.GrowStartDate
                objItem.GrowEndDate = item.PlanFarmerGroup.GrowEndDate
                objItem.CheckFarmStartDate = item.PlanFarmerGroup.CheckFarmStartDate
                objItem.CheckFarmEndDate = item.PlanFarmerGroup.CheckFarmEndDate
                objItem.HarvestStartDate = item.PlanFarmerGroup.HarvestStartDate
                objItem.HarvestEndDate = item.PlanFarmerGroup.HarvestEndDate
                objItem.BuyStartDate = item.PlanFarmerGroup.BuyStartDate
                objItem.BuyEndDate = item.PlanFarmerGroup.BuyEndDate
                objItem.ProcessStartDate = item.PlanFarmerGroup.ProcessStartDate
                objItem.ProcessEndDate = item.PlanFarmerGroup.ProcessEndDate
                objItem.MarketStartDate = item.PlanFarmerGroup.MarketStartDate
                objItem.MarketEndDate = item.PlanFarmerGroup.MarketEndDate
                objItem.IsMaximumFarmer = item.IsMaximumFarmer
                objItem.Save()
            End If
        Next
    End Sub

    '<Action(Caption:="ดึงข้อมูล", ConfirmationMessage:="ท่านต้องการดึงข้อมูล ใช่หรือไม่?", ImageName:="BO_Note", AutoCommit:=True, TargetObjectsCriteria:="Status='Draft'")> _
    Public Sub ImportData()
        'Session.Delete(SubmitPlantKP3Details)
        Dim _Planfarmers = New XPCollection(Of PlanFarmer)(Session, CriteriaOperator.Parse("PlanFarmerGroup.MainPlan=?", Me.MainPlan))
        If _Planfarmers.Count > 0 Then
            GetPlanFarmer(_Planfarmers)
            OnChanged("SubmitPlantKP3Details")
            Session.CommitTransaction()
        End If
    End Sub

    Function GetAddressRefName(obj As CustomAddress, _Type As AddressType) As String
        Try
            Select Case _Type
                Case AddressType.SubDistrict
                    Return obj.SubDistrict.SubDistrictName
                Case AddressType.District
                    Return obj.District.DistrictName
                Case AddressType.Province
                    Return obj.Province.ProvinceName
            End Select
        Catch ex As Exception
            Return ""
        End Try
    End Function

    '<Action(Caption:="ส่งรายงาน", ConfirmationMessage:="ท่านต้องการส่งข้อมูลรายงานนี้ ใช่หรือไม่?", ImageName:="sent2", AutoCommit:=True, TargetObjectsCriteria:="Status='Draft'")> _
    Public Function MarkAsComplete() As Boolean
        If Not IsDeleted Then
            If Not SubmitPlantKP3Details.Count > 0 Then
                MsgBox("ไม่พบรายชื่อเกษตรกรผู้จัดทำแปลง กรุณาตรวจสอบข้อมูลอีกครั้ง", MsgBoxStyle.Information, "")
                Exit Function
            End If
            Try
                '//ประกาศตัวแปล webservice
                Dim objService As New RiceService.RiceService

                '//ประกาศ Object ของรายงาน ขพ.3 (Header)
                Dim objData As New RiceService.KP3ReportData

                '//ข้อมูลตั้งค่าของศูนย์
                Dim objSiteInfo As SiteSetting = GetSiteInfo()

                '//ใส่ค่าให้กับ object รายงาน ขพ.3 (ข้อมูลส่วน Header)
                objData.SiteNo = objSiteInfo.SiteNo
                objData.SiteName = objSiteInfo.SiteName
                objData.PlantName = MainPlan.Plant.PlantName
                objData.SeedTypeName = MainPlan.SeedType.SeedName
                objData.SeedClassName = MainPlan.SeedClass.ClassName
                objData.SeasonName = MainPlan.Season.SeasonName
                objData.SeedYear = MainPlan.SeedYear
                objData.SeedLot = MainPlan.Lot
                objData.SubmitBy = CType(SecuritySystem.CurrentUser, User).DisplayName
                objData.SubmitDate = Date.Now

                Dim listOfDetail As New List(Of RiceService.KP3ReportDetail)
                For Each item As SubmitPlantKP3Detail In SubmitPlantKP3Details
                    '//ประกาศตัวแปล Detail ของรายงาน ขพ.3
                    Dim objDetail As New RiceService.KP3ReportDetail
                    objDetail.FarmerNo = item.FarmerNo
                    objDetail.FarmerName = item.FullName
                    objDetail.FarmerGroupNo = item.GroupCode
                    objDetail.FarmerGroupName = item.GroupName
                    objDetail.AddressNo = item.AddressNo
                    objDetail.Moo = item.Moo
                    objDetail.Tambol = item.DistrictName
                    objDetail.Amphur = item.SubDistrictName
                    objDetail.Province = item.ProvinceName
                    objDetail.GroupAddressNo = item.GroupAddressNo
                    objDetail.GroupMoo = item.GroupMoo
                    objDetail.GroupTambol = item.GroupSubDistrictName
                    objDetail.GroupAmphur = item.GroupDistrictName
                    objDetail.GroupProvince = item.GroupProvinceName
                    objDetail.AreaSize = item.TotalGrowArea
                    objDetail.SeedRecieveQuantity = item.TotalSeedReceive
                    objDetail.SeedUseQuantity = item.TotalSeedUse
                    objDetail.SeedGoalQuantity = item.MaxBuyQuantity
                    objDetail.GrowStartDate = item.GrowStartDate
                    objDetail.GrowEndDate = item.GrowEndDate
                    objDetail.CheckFarmStartDate = item.CheckFarmStartDate
                    objDetail.CheckFarmEndDate = item.CheckFarmEndDate
                    objDetail.HarvestStartDate = item.HarvestStartDate
                    objDetail.HarvestEndDate = item.HarvestEndDate
                    objDetail.BuyStartDate = item.BuyStartDate
                    objDetail.BuyEndDate = item.BuyEndDate
                    objDetail.ProcessStartDate = item.ProcessStartDate
                    objDetail.ProcessEndDate = item.ProcessEndDate
                    objDetail.MarketStartDate = item.MarketStartDate
                    objDetail.MarketEndDate = item.MarketEndDate
                    objDetail.IsMaxBuyFarmer = item.IsMaximumFarmer
                    listOfDetail.Add(objDetail)
                Next

                '//ใส่รายชื่อเกษตรกร (Detail) ในส่วนของ Header
                '//ต้องแปลง List ให้เป็น Array เพราะส่งข้อมูลผ่านเว็บเซอร์วิส ต้องเป็น Array 
                objData.KP3ReportDetails = listOfDetail.ToArray

                '//ส่งข้อมูลผ่าน Webservice
                Dim ret As String = objService.SubmitDataKp3Report("NTiSecureCode", objData)

                '//เป็นสถานะเป็น ส่งข้อมูลแล้ว (Sent)
                If ret = "Success" Then
                    Me.Status = PublicEnum.SubmitReportStatus.Sent
                    Me.SubmitDate = Date.Now
                    Me.SubmitBy = CType(SecuritySystem.CurrentUser, User).DisplayName
                    '//บันทึกข้อมูล
                    ' MsgBox("ส่งข้อมูลรายงาน เรียบร้อยแล้ว", MsgBoxStyle.Information)
                    Me.Save()
                    Return True
                    'Session.CommitTransaction()
                Else
                    'MsgBox("เกิดข้อผิดพลาด ไม่สามารถส่งรายงานได้ กรุณาติดต่อผู้ดูแลระบบ", MsgBoxStyle.Critical)
                    Return False
                End If

            Catch ex As Exception
                'MsgBox("เกิดข้อผิดพลาด ไม่สามารถส่งรายงานได้ กรุณาติดต่อผู้ดูแลระบบ", MsgBoxStyle.Critical)
                Return False
            End Try

        End If
    End Function

    Enum AddressType
        SubDistrict
        District
        Province
    End Enum

    Public Function ConvertToInteger(obj As Object) As Integer
        Try
            Return Convert.ToInt32(obj)
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Protected Function GetSiteInfo() As SiteSetting
        Dim objSite As New XPCollection(Of SiteSetting)(Session)
        Return objSite(0)
    End Function

    Public Sub DoLoadData() Implements ISubmitReportAble.DoLoadData
        ImportData()
    End Sub

    Public Function DoSubmitReport() As Boolean Implements ISubmitReportAble.DoSubmitReport
        Return MarkAsComplete()
    End Function
End Class
