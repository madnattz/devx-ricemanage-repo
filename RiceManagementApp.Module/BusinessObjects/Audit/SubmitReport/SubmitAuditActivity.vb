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

<ImageName("sent3")> _
<XafDisplayName("ส่งรายงาน สรุปแผนและผลการดำเนินงาน 2")> _
<DefaultClassOptions()> _
<ConditionalAppearance.Appearance("SubmitAuditActivityDisableAllItems", criteria:="Status!='Draft'", Enabled:=False, TargetItems:="*", Context:="DetailView")> _
Public Class SubmitAuditActivity
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
        SubmitAuditActivityDetails.Sorting.Add(New SortProperty("ActivityNo", DevExpress.Xpo.DB.SortingDirection.Ascending))
        MyBase.OnLoaded()
    End Sub

    <Association("SubmitAuditActivity-SubmitAuditActivityDetails", GetType(SubmitAuditActivityDetail))> _
    <DevExpress.Xpo.Aggregated> _
    <XafDisplayName("ข้อมูล แผนและผลการดำเนินงานตามกิจกรรมในกระบวนการผลิตเมล็ดพันธุ์")> _
        Public ReadOnly Property SubmitAuditActivityDetails() As XPCollection(Of SubmitAuditActivityDetail)
        Get
            Return GetCollection(Of SubmitAuditActivityDetail)("SubmitAuditActivityDetails")
        End Get
    End Property

    Dim fMainPlan As MainPlan
    <XafDisplayName("เป้าการผลิต")> _
    <ImmediatePostData()> _
    <RuleRequiredField(DefaultContexts.Save)> _
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
    Public Property Status() As PublicEnum.SubmitReportStatus
        Get
            Return fStatus
        End Get
        Set(ByVal value As PublicEnum.SubmitReportStatus)
            SetPropertyValue(Of PublicEnum.SubmitReportStatus)("Status", fStatus, value)
        End Set
    End Property

    '<Action(Caption:="ดึงข้อมูล", ConfirmationMessage:="ท่านต้องการดึงข้อมูล ใช่หรือไม่?", ImageName:="BO_Note", AutoCommit:=True, TargetObjectsCriteria:="Status='Draft'")> _
    Public Sub ImportData()
        'Session.Delete(SubmitAuditActivityDetails)
        Try
            Dim _AuditActivityPlans As AuditActivityPlan = Session.FindObject(Of AuditActivityPlan)(CriteriaOperator.Parse("MainPlan=?", Me.MainPlan))
            If _AuditActivityPlans IsNot Nothing AndAlso _AuditActivityPlans.ActivityPlanReports.Count > 0 Then

                For Each item As ActivityPlanReport In _AuditActivityPlans.ActivityPlanReports
                    Dim objDetail As SubmitAuditActivityDetail = Session.FindObject(Of SubmitAuditActivityDetail)(CriteriaOperator.Parse("ActivityNo=? and ActivityName=? and SubmitAuditActivity = ?", item.ItemNo, item.ActivityName, Me))
                    If objDetail Is Nothing Then
                        objDetail = New SubmitAuditActivityDetail(Session)
                        objDetail.SubmitAuditActivity = Me
                        objDetail.ActivityNo = item.ItemNo
                        objDetail.ActivityName = item.ActivityName
                        objDetail.ActivityUnit = item.ActivityUnit
                        objDetail.OctValue = item.OCT
                        objDetail.NovValue = item.NOV
                        objDetail.DecValue = item.DEC
                        objDetail.JanValue = item.JAN
                        objDetail.FebValue = item.FEB
                        objDetail.MarValue = item.MAR
                        objDetail.AprValue = item.APR
                        objDetail.MayValue = item.MAY
                        objDetail.JunValue = item.JUN
                        objDetail.JulValue = item.JUL
                        objDetail.AugValue = item.AUG
                        objDetail.SepValue = item.SEP
                        objDetail.Total = item.Total
                        objDetail.Differrence = item.Differrence
                        objDetail.Save()
                    End If
                Next
                Session.CommitTransaction()
            End If
        Catch ex As Exception
            Session.RollbackTransaction()
        End Try
    End Sub

    ' <Action(Caption:="ส่งรายงาน", ConfirmationMessage:="ท่านต้องการส่งข้อมูลรายงานนี้ ใช่หรือไม่?", ImageName:="sent2", AutoCommit:=True, TargetObjectsCriteria:="Status='Draft'")> _
    Public Function MarkAsComplete() As Boolean
        If Not IsDeleted Then
            If Not SubmitAuditActivityDetails.Count > 0 Then
                MsgBox("ไม่พบรายการ กรุณาตรวจสอบข้อมูลอีกครั้ง", MsgBoxStyle.Information, "")
                Exit Function
            End If

            Dim objService As New RiceService.RiceService

            Dim objData As New RiceService.QualityActivityHeader
            Try
                Dim objSiteInfo As SiteSetting = GetSiteInfo()

                objData.SiteNo = objSiteInfo.SiteNo
                objData.SiteName = objSiteInfo.SiteName
                objData.PlantName = MainPlan.Plant.PlantName
                objData.SeedTypeName = MainPlan.SeedType.SeedName
                objData.SeedClassName = MainPlan.SeedClass.ClassName
                objData.SeasonName = MainPlan.Season.SeasonName
                objData.SeedYear = MainPlan.SeedYear
                objData.SeedLot = MainPlan.Lot
                objData.SeedGoal = MainPlan.TotalGoal
                objData.BuyGoal = MainPlan.SumMaxBuyQuantity
                objData.TotalGrowArea = MainPlan.AreaCount
                objData.TotalGroup = MainPlan.GroupCount
                objData.TotalFarmer = MainPlan.FarmerCount
                objData.SubmitBy = CType(SecuritySystem.CurrentUser, User).DisplayName
                objData.SubmitDate = Date.Now

                Dim listOfDetail As New List(Of RiceService.QualityActivityDetail)
                For Each row As SubmitAuditActivityDetail In SubmitAuditActivityDetails
                    If row IsNot Nothing Then
                        Dim objDetail As New RiceService.QualityActivityDetail
                        objDetail.ActivityNo = row.ActivityNo
                        objDetail.PlanName = row.ActivityName
                        objDetail.PlanUnit = row.ActivityUnit
                        objDetail.OctValue = row.OctValue
                        objDetail.NovValue = row.NovValue
                        objDetail.DecValue = row.DecValue
                        objDetail.JanValue = row.JunValue
                        objDetail.FebValue = row.FebValue
                        objDetail.MarValue = row.MarValue
                        objDetail.AprValue = row.AprValue
                        objDetail.MayValue = row.MayValue
                        objDetail.JunValue = row.JunValue
                        objDetail.JulValue = row.JulValue
                        objDetail.AugValue = row.AugValue
                        objDetail.SepValue = row.SepValue
                        objDetail.AllValue = row.Total
                        objDetail.DiffValue = row.Differrence
                        listOfDetail.Add(objDetail)
                    End If
                Next

                objData.QualityActivityDetails = listOfDetail.ToArray

                Dim ret As String = objService.SubmitDataQualityActivityReport("NTiSecureCode", objData)

                If ret = "Success" Then
                    Me.Status = PublicEnum.SubmitReportStatus.Sent
                    Me.SubmitDate = Date.Now
                    Me.SubmitBy = CType(SecuritySystem.CurrentUser, User).DisplayName
                    '//บันทึกข้อมูล
                    'MsgBox("ส่งข้อมูลรายงาน เรียบร้อยแล้ว", MsgBoxStyle.Information)
                    Me.Save()
                    Session.CommitTransaction()
                    Return True
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


    Public Sub DoLoadData() Implements ISubmitReportAble.DoLoadData
        ImportData()
        SubmitAuditActivityDetails.Sorting.Add(New SortProperty("ActivityNo", DevExpress.Xpo.DB.SortingDirection.Ascending))
    End Sub

    Public Function DoSubmitReport() As Boolean Implements ISubmitReportAble.DoSubmitReport
        Return MarkAsComplete()
    End Function
End Class
