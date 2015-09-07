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

<ImageName("sent3")> _
<XafDisplayName("ส่งรายงาน การตรวจตัดสินคุณภาพแปลงขยายพันธุ์ข้าวอย่างเป็นทางการ")> _
<DefaultClassOptions()> _
<ConditionalAppearance.Appearance("SubmitCheckFarmReportDisableAllItems", criteria:="Status!='Draft'", Enabled:=False, TargetItems:="*", Context:="DetailView")> _
Public Class SubmitCheckFarm
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
        SubmitCheckFarmDetails.Sorting.Add(New SortProperty("FarmerNo", DB.SortingDirection.Ascending))
    End Sub

    <Association("SubmitCheckFarm-SubmitCheckFarmDetails", GetType(SubmitCheckFarmDetail))> _
    <DevExpress.Xpo.Aggregated> _
    <XafDisplayName("เกษตรกรผู้จัดทำแปลง")> _
    Public ReadOnly Property SubmitCheckFarmDetails() As XPCollection(Of SubmitCheckFarmDetail)
        Get
            Return GetCollection(Of SubmitCheckFarmDetail)("SubmitCheckFarmDetails")
        End Get
    End Property

    Dim fMainPlan As MainPlan
    <XafDisplayName("เป้าการผลิต")> _
    <ImmediatePostData()> _
    <RuleRequiredField()> _
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

    Private Sub GetCheckFarm(listCheckFarm As XPCollection(Of CheckFarm))
        For Each item As CheckFarm In listCheckFarm
            Dim objItem As SubmitCheckFarmDetail = Session.FindObject(Of SubmitCheckFarmDetail)(CriteriaOperator.Parse("FarmerNo=? and SubmitCheckFarm = ?", item.PlanFarmer.Farmer.MemberID, Me))
            If objItem Is Nothing Then
                objItem = New SubmitCheckFarmDetail(Session)
                objItem.SubmitCheckFarm = Me
                objItem.FarmerNo = item.PlanFarmer.Farmer.MemberID
                objItem.FarmerName = item.PlanFarmer.FullName
                objItem.GrowAreaSize = item.GrowAreaSize
                objItem.EstimateResultPerArea = item.PlanFarmer.EstimateResultPerArea
                objItem.CheckPeriod = item.CheckPeriod
                objItem.GrowTypeString = item.GrowTypeString
                objItem.OtherSeedValue = item.OtherSeedValue
                objItem.RedSeedValue = item.RedSeedValue
                objItem.Disease = item.Disease
                objItem.Bug = item.Bug
                objItem.Weed = item.Weed
                objItem.Broken = item.Broken
                objItem.Distance = item.Distance
                objItem.DamageArea = item.DamageArea
                objItem.CheckDate = item.CheckDate
                objItem.CheckResults = item.CheckResults
                objItem.Remark = item.FailReason
                objItem.Save()
            End If
        Next
    End Sub

    '<Action(Caption:="ดึงข้อมูล", ConfirmationMessage:="ท่านต้องการดึงข้อมูล ใช่หรือไม่?", ImageName:="BO_Note", AutoCommit:=True, TargetObjectsCriteria:="Status='Draft'")> _
    Public Sub ImportData()
        'Session.Delete(SubmitCheckFarmDetails)
        Dim _CheckFarm = New XPCollection(Of CheckFarm)(Session, CriteriaOperator.Parse("MainPlan=? and CheckResults != 'Pending'", Me.MainPlan))
        If _CheckFarm.Count > 0 Then
            GetCheckFarm(_CheckFarm)
            OnChanged("SubmitCheckFarmDetails")
        End If
    End Sub

    ' <Action(Caption:="ส่งรายงาน", ConfirmationMessage:="ท่านต้องการส่งข้อมูลรายงานนี้ ใช่หรือไม่?", ImageName:="sent2", AutoCommit:=True, TargetObjectsCriteria:="Status='Draft'")> _
    Public Function MarkAsComplete() As Boolean
        If Not IsDeleted Then
            If Not SubmitCheckFarmDetails.Count > 0 Then
                MsgBox("ไม่พบรายชื่อเกษตรกรผู้จัดทำแปลง กรุณาตรวจสอบข้อมูลอีกครั้ง", MsgBoxStyle.Information, "")
                Exit Function
            End If
            Try
                Dim objService As New RiceService.RiceService

                Dim objData As New RiceService.CheckFarmHeader

                Dim objSiteInfo As SiteSetting = GetSiteInfo()

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

                Dim listOfDetail As New List(Of RiceService.CheckFarmDetail)
                For Each item As SubmitCheckFarmDetail In SubmitCheckFarmDetails
                    Dim objDetail As New RiceService.CheckFarmDetail

                    objDetail.FarmerNo = item.FarmerNo
                    objDetail.FarmerName = item.FarmerName
                    objDetail.GrowAreaSize = item.GrowAreaSize
                    objDetail.EstimateResultPerArea = item.EstimateResultPerArea
                    objDetail.CheckPeriod = item.CheckPeriod
                    objDetail.GrowTypeString = item.GrowTypeString
                    objDetail.OtherSeedValue = item.OtherSeedValue
                    objDetail.RedSeedValue = item.RedSeedValue
                    objDetail.Disease = item.Disease
                    objDetail.Bug = item.Bug
                    objDetail.Weed = item.Weed
                    objDetail.Broken = item.Broken
                    objDetail.Distance = item.Disease
                    objDetail.DamageArea = item.DamageArea
                    objDetail.CheckDate = item.CheckDate
                    objDetail.CheckResults = item.CheckResults
                    objDetail.Remark = item.Remark
                    listOfDetail.Add(objDetail)
                Next

                objData.CheckFarmDetails = listOfDetail.ToArray

                Dim ret As String = objService.SubmitDataCheckFarmReport("NTiSecureCode", objData)

                If ret = "Success" Then
                    Me.Status = PublicEnum.SubmitReportStatus.Sent
                    Me.SubmitDate = Date.Now
                    Me.SubmitBy = CType(SecuritySystem.CurrentUser, User).DisplayName
                    'MsgBox("ส่งข้อมูลรายงาน เรียบร้อยแล้ว", MsgBoxStyle.Information)
                    Return True
                    Me.Save()
                Else
                    Return False
                End If

            Catch ex As Exception
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

    Public Sub DoLoadData() Implements ISubmitReportAble.DoLoadData
        If Me.MainPlan IsNot Nothing Then
            ImportData()
        End If
    End Sub

    Public Function DoSubmitReport() As Boolean Implements ISubmitReportAble.DoSubmitReport
        Return MarkAsComplete()
    End Function
End Class
