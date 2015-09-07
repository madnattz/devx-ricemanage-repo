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
<XafDisplayName("ส่งรายงาน ขพ.2")> _
<DefaultClassOptions()> _
<RuleCriteria("SubmitPlantKP2.NotDelete", DefaultContexts.Delete, "Status='Draft'", CustomMessageTemplate:="ไม่สามารถลบข้อมูลนี้ได้")> _
<ConditionalAppearance.Appearance("SubmitPlantKP2DisableAllItems", criteria:="Status!='Draft'", Enabled:=False, TargetItems:="*", Context:="DetailView")> _
Public Class SubmitPlantKP2
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
        Me.SubmitPlantKP2Details.Sorting.Add(New SortProperty("PlanFarmerNo", SortingDirection.Ascending))
    End Sub

    <Association("SubmitPlantKP2-SubmitPlantKP2Details", GetType(SubmitPlantKP2Detail))> _
    <DevExpress.Xpo.Aggregated> _
    <XafDisplayName("เกษตรกรผู้จัดทำแปลง")> _
    Public ReadOnly Property SubmitPlantKP2Details() As XPCollection(Of SubmitPlantKP2Detail)
        Get
            Return GetCollection(Of SubmitPlantKP2Detail)("SubmitPlantKP2Details")
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
    <Appearance("SubmitDate", Enabled:=False)> _
    <XafDisplayName("วันที่ส่งรายงาน")> _
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
    <Appearance("SubmitBy", Enabled:=False)> _
    <XafDisplayName("ส่งรายงานโดย")> _
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
    <Appearance("Status", Enabled:=False)> _
    <XafDisplayName("สถานะ")> _
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
            Dim objItem As SubmitPlantKP2Detail = Session.FindObject(Of SubmitPlantKP2Detail)(CriteriaOperator.Parse("PlanFarmerNo=? and SubmitPlantKP2=?", item.Farmer.MemberID, Me))
            If objItem Is Nothing Then
                objItem = New SubmitPlantKP2Detail(Session)
                objItem.SubmitPlantKP2 = Me
                objItem.PlanFarmerNo = item.Farmer.MemberID
                objItem.FullName = item.FullName
                objItem.GroupCode = item.Farmer.FarmerGroup.GroupCode
                objItem.GroupName = item.Farmer.FarmerGroup.GroupName
                objItem.AddressNo = item.Farmer.Address.AddressNo
                objItem.Moo = item.Farmer.Address.Moo
                objItem.SubDistrictName = GetAddressRefName(item.Farmer.Address, AddressType.SubDistrict)
                objItem.DistrictName = GetAddressRefName(item.Farmer.Address, AddressType.District)
                objItem.ProvinceName = GetAddressRefName(item.Farmer.Address, AddressType.Province)
                objItem.TotalGrowArea = item.TotalGrowArea
                objItem.TotalSeedUse = item.TotalSeedUse
                objItem.TotalSeedReceive = item.TotalSeedReceive
                objItem.FullSeedLotName = item.FullSeedLotName
                objItem.MaxBuyQuantity = item.MaxBuyQuantity
                objItem.IsMaximumFarmer = item.IsMaximumFarmer
                objItem.Save()
            End If
        Next
    End Sub

    '<Action(Caption:="ดึงข้อมูล", ConfirmationMessage:="ท่านต้องการดึงข้อมูล ใช่หรือไม่?", ImageName:="BO_Note", AutoCommit:=True, TargetObjectsCriteria:="Status='Draft'")> _
    Public Sub ImportData()
        'Session.Delete(SubmitPlantKP2Details)
        Dim _Planfarmers = New XPCollection(Of PlanFarmer)(Session, CriteriaOperator.Parse("PlanFarmerGroup.MainPlan=?", Me.MainPlan))
        If _Planfarmers.Count > 0 Then
            GetPlanFarmer(_Planfarmers)
            OnChanged("SubmitPlantKP2Details")
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

    Enum AddressType
        SubDistrict
        District
        Province
    End Enum

    '<Action(Caption:="ส่งรายงาน", ConfirmationMessage:="ท่านต้องการส่งข้อมูลรายงานนี้ ใช่หรือไม่?", ImageName:="sent2", AutoCommit:=True, TargetObjectsCriteria:="Status='Draft'")> _
    Public Function MarkAsComplete() As Boolean
        If Not IsDeleted Then
            If Not SubmitPlantKP2Details.Count > 0 Then
                MsgBox("ไม่พบรายชื่อเกษตรกรผู้จัดทำแปลง กรุณาตรวจสอบข้อมูลอีกครั้ง", MsgBoxStyle.Information, "")
                Exit Function
            End If
            Try
                '//ประกาศตัวแปล webservice
                Dim objService As New RiceService.RiceService

                '//ประกาศ Object ของรายงาน ขพ.2 (Header)
                Dim objData As New RiceService.KP2ReportData

                '//ข้อมูลตั้งค่าของศูนย์
                Dim objSiteInfo As SiteSetting = GetSiteInfo()

                '//ใส่ค่าให้กับ object รายงาน ขพ.2 (ข้อมูลส่วน Header)
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

                '//ตัวแปลของรายงาน ขพ.2 (Detail) (รายชื่อ และที่อยู่ เกษตรกร) ประเภท List
                Dim listOfDetail As New List(Of RiceService.KP2ReportDetail)
                For Each item As SubmitPlantKP2Detail In SubmitPlantKP2Details
                    Dim objDetail As New RiceService.KP2ReportDetail
                    objDetail.FarmerNo = item.PlanFarmerNo
                    objDetail.FarmerName = item.FullName
                    objDetail.FarmerGroupNo = item.GroupCode
                    objDetail.FarmerGroupName = item.GroupName
                    objDetail.AddressNo = item.AddressNo
                    objDetail.Moo = item.Moo
                    objDetail.Tambol = item.SubDistrictName
                    objDetail.Amphur = item.DistrictName
                    objDetail.Province = item.ProvinceName
                    objDetail.AreaSize = item.TotalGrowArea
                    objDetail.SeedUseQuantity = item.TotalSeedUse
                    'objDetail.TotalSeedReceive = item.TotalSeedReceive
                    objDetail.SeedSource = item.FullSeedLotName
                    objDetail.SeedGoalQuantity = item.MaxBuyQuantity
                    objDetail.IsMaxBuyFarmer = item.IsMaximumFarmer
                    listOfDetail.Add(objDetail)

                Next

                '//ใส่รายชื่อเกษตรกร (Detail) ในส่วนของ Header
                '//ต้องแปลง List ให้เป็น Array เพราะส่งข้อมูลผ่านเว็บเซอร์วิส ต้องเป็น Array 
                objData.KP2ReportDetails = listOfDetail.ToArray

                '//ส่งข้อมูลผ่าน Webservice
                Dim ret As String = objService.SubmitDataKp2Report("NTiSecureCode", objData)

                '//เป็นสถานะเป็น ส่งข้อมูลแล้ว (Sent)
                If ret = "Success" Then
                    Me.Status = PublicEnum.SubmitReportStatus.Sent
                    Me.SubmitDate = Date.Now
                    Me.SubmitBy = CType(SecuritySystem.CurrentUser, User).DisplayName
                    '//บันทึกข้อมูล
                    'MsgBox("ส่งข้อมูลรายงาน เรียบร้อยแล้ว", MsgBoxStyle.Information)
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

    Public Function DoSubmitReport() As Boolean Implements ISubmitReportAble.DoSubmitReport
        Return MarkAsComplete()
    End Function

    Public Sub DoLoadData() Implements ISubmitReportAble.DoLoadData
        ImportData()
    End Sub

End Class
