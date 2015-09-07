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
<XafDisplayName("ขพ.5")> _
<RuleCriteria("SubmitPlantKP5.NotDelete", DefaultContexts.Delete, "Status='Draft'", CustomMessageTemplate:="ไม่สามารถลบข้อมูลนี้ได้")> _
<ConditionalAppearance.Appearance("SubmitPlanKP5ReportDisableAllItems", criteria:="Status!='Draft'", Enabled:=False, TargetItems:="*", Context:="DetailView")> _
<DefaultClassOptions()> _
Public Class SubmitPlantKP5
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
        Me.SubmitPlantKP5Details.Sorting.Add(New SortProperty("FarmerNo", SortingDirection.Ascending))
    End Sub

    <Association("SubmitPlantKP5-SubmitPlantKP5Details", GetType(SubmitPlantKP5Detail))> _
    <DevExpress.Xpo.Aggregated> _
    <XafDisplayName("เกษตรกรผู้จัดทำแปลง")> _
    Public ReadOnly Property SubmitPlantKP5Details() As XPCollection(Of SubmitPlantKP5Detail)
        Get
            Return GetCollection(Of SubmitPlantKP5Detail)("SubmitPlantKP5Details")
        End Get
    End Property

    Dim fMainPlan As MainPlan
    <XafDisplayName("เป้าการผลิต")> _
    <RuleRequiredField(DefaultContexts.Save)> _
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
            Dim objItem As SubmitPlantKP5Detail = Session.FindObject(Of SubmitPlantKP5Detail)(CriteriaOperator.Parse("FarmerNo=? and SubmitPlantKP5=? ", item.Farmer.MemberID, Me))
            If objItem Is Nothing Then
                objItem = New SubmitPlantKP5Detail(Session)
                objItem.SubmitPlantKP5 = Me
                objItem.FarmerNo = item.Farmer.MemberID
                objItem.FarmerName = item.FullName
                objItem.FarmerGroupNo = item.Farmer.FarmerGroup.GroupCode
                objItem.FarmerGroupName = item.Farmer.FarmerGroup.GroupName
                objItem.AddressNo = item.Farmer.Address.AddressNo
                objItem.Moo = item.Farmer.Address.Moo
                objItem.Tambol = GetAddressRefName(item.Farmer.Address, AddressType.SubDistrict)
                objItem.Amphur = GetAddressRefName(item.Farmer.Address, AddressType.District)
                objItem.Province = GetAddressRefName(item.Farmer.Address, AddressType.Province)
                objItem.AreaSize = item.TotalGrowArea
                objItem.QuantityPerUnit = item.EstimateResultPerArea
                objItem.BuyQuantity = item.MaxBuyQuantity
                objItem.SeedPrice = item.SeedPrice
                objItem.IsMaxBuyFarmer = item.IsMaximumFarmer
                objItem.Save()
            End If
        Next
    End Sub

    '<Action(Caption:="ดึงข้อมูล", ConfirmationMessage:="ท่านต้องการดึงข้อมูล ใช่หรือไม่?", ImageName:="BO_Note", AutoCommit:=True, TargetObjectsCriteria:="Status='Draft'")> _
    Public Sub ImportData()
        'Session.Delete(SubmitPlantKP5Details)
        Dim _Planfarmers = New XPCollection(Of PlanFarmer)(Session, CriteriaOperator.Parse("PlanFarmerGroup.MainPlan=?", Me.MainPlan))
        If _Planfarmers.Count > 0 Then
            GetPlanFarmer(_Planfarmers)
            OnChanged("SubmitPlantKP5Details")
            Session.CommitTransaction()
        End If

    End Sub

    '<Action(Caption:="ส่งรายงาน", ConfirmationMessage:="ท่านต้องการส่งข้อมูลรายงานนี้ ใช่หรือไม่?", ImageName:="sent2", AutoCommit:=True, TargetObjectsCriteria:="Status='Draft'")> _
    Public Function MarkAsComplete() As Boolean
        If Not IsDeleted Then
            If Not SubmitPlantKP5Details.Count > 0 Then
                MsgBox("ไม่พบรายชื่อเกษตรกรผู้จัดทำแปลง กรุณาตรวจสอบข้อมูลอีกครั้ง", MsgBoxStyle.Information, "")
                Exit Function
            End If
            Try
                '//ประกาศตัวแปล webservice
                Dim objService As New RiceService.RiceService

                '//ประกาศ Object ของรายงาน ขพ.5 (Header)
                Dim objData As New RiceService.KP5ReportData

                '//ข้อมูลตั้งค่าของศูนย์
                Dim objSiteInfo As SiteSetting = GetSiteInfo()

                '//ใส่ค่าให้กับ object รายงาน ขพ.5 (ข้อมูลส่วน Header)
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

                Dim listOfDetail As New List(Of RiceService.KP5ReportDetail)
                For Each item As SubmitPlantKP5Detail In SubmitPlantKP5Details
                    '//ประกาศตัวแปล Detail ของรายงาน ขพ.5
                    Dim objDetail As New RiceService.KP5ReportDetail
                    objDetail.FarmerNo = item.FarmerNo
                    objDetail.FarmerName = item.FarmerName
                    objDetail.FarmerGroupNo = item.FarmerGroupNo
                    objDetail.FarmerGroupName = item.FarmerGroupName
                    objDetail.AddressNo = item.AddressNo
                    objDetail.Moo = item.Moo
                    objDetail.Tambol = item.Tambol
                    objDetail.Amphur = item.Amphur
                    objDetail.Province = item.Province
                    objDetail.AreaSize = item.AreaSize
                    objDetail.QuantityPerUnit = item.QuantityPerUnit
                    objDetail.BuyQuantity = item.BuyQuantity
                    objDetail.IsMaxBuyFarmer = item.IsMaxBuyFarmer
                    objDetail.SeedPrice = item.SeedPrice
                    listOfDetail.Add(objDetail)
                Next

                '//ใส่รายชื่อเกษตรกร (Detail) ในส่วนของ Header
                '//ต้องแปลง List ให้เป็น Array เพราะส่งข้อมูลผ่านเว็บเซอร์วิส ต้องเป็น Array 
                objData.KP5ReportDetails = listOfDetail.ToArray

                '//ส่งข้อมูลผ่าน Webservice
                Dim ret As String = objService.SubmitDataKp5Report("NTiSecureCode", objData)

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
                    Return False
                    'MsgBox("เกิดข้อผิดพลาด ไม่สามารถส่งรายงานได้ กรุณาติดต่อผู้ดูแลระบบ", MsgBoxStyle.Critical)
                End If

            Catch ex As Exception
                Return False
                'MsgBox("เกิดข้อผิดพลาด ไม่สามารถส่งรายงานได้ กรุณาติดต่อผู้ดูแลระบบ", MsgBoxStyle.Critical)
            End Try

        End If
    End Function


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
