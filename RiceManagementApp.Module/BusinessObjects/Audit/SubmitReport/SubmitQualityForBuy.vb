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
<XafDisplayName("ส่งรายงาน ผลการทดสอบคุณภาพเมล็ดพันธุ์เพื่อจัดซื้อ")> _
<DefaultClassOptions()> _
<ConditionalAppearance.Appearance("SubmitQualityForBuyReportDisableAllItems", criteria:="Status!='Draft'", Enabled:=False, TargetItems:="*", Context:="DetailView")> _
Public Class SubmitQualityForBuy
    Inherits BaseObject
    Implements ISubmitReportAble

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        Me.Status = PublicEnum.SubmitReportStatus.Draft
    End Sub

    <Association("SubmitQualityForBuyReport -SubmitQualityForBuyReportDetails", GetType(SubmitQualityForBuyDetail))> _
<DevExpress.Xpo.Aggregated> _
<XafDisplayName("ข้อมูลการตรวจสอบคุณภาพ")> _
    Public ReadOnly Property SubmitQualityForBuyReportDetails() As XPCollection(Of SubmitQualityForBuyDetail)
        Get
            Return GetCollection(Of SubmitQualityForBuyDetail)("SubmitQualityForBuyReportDetails")
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

    Dim fStartDay As Date
    <XafDisplayName("วันที่ตรวจสอบ")> _
    <RuleRequiredField(DefaultContexts.Save)> _
    <ImmediatePostData()> _
    Public Property StartDay() As Date
        Get
            Return fStartDay
        End Get
        Set(ByVal value As Date)
            SetPropertyValue(Of Date)("StartDay", fStartDay, value)
        End Set
    End Property

    Dim fEndDate As Date
    <XafDisplayName("ถึงวันที่")> _
    <RuleRequiredField(DefaultContexts.Save)> _
    <ImmediatePostData()> _
    Public Property EndDate() As Date
        Get
            Return fEndDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue(Of Date)("EndDate", fEndDate, value)
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

    Private Sub GetQualityAudit(listQualityAudit As XPCollection(Of QualityAudit))
        For Each item As QualityAudit In listQualityAudit
            Dim objItem As SubmitQualityForBuyDetail = Session.FindObject(Of SubmitQualityForBuyDetail)(CriteriaOperator.Parse("SampleNo=? and SubmitQualityForBuyReport=?", item.SampleNo, Me))

            If objItem Is Nothing Then
                objItem = New SubmitQualityForBuyDetail(Session)
                objItem.SubmitQualityForBuyReport = Me
                objItem.ItemNo = item.ItemNo
                objItem.SampleNo = item.SampleNo
                objItem.PlanFarmerNo = item.PlanFarmer.Farmer.MemberID
                objItem.FullName = item.PlanFarmer.FullName
                objItem.Plant = item.Plant.PlantName
                objItem.SeedType = item.SeedType.SeedName
                objItem.SeedClass = item.SeedClass.ClassName
                objItem.SeedSeason = item.Season.SeasonName
                objItem.SeedYear = item.SeedYear
                objItem.LabDate = item.LabDate
                objItem.Wet = item.Wet
                objItem.PureSeed = item.PureSeed
                objItem.Compound = item.Compound
                objItem.OtherRiceSeed = item.OtherRiceSeed
                objItem.RedSeed = item.RedSeed
                objItem.Grow = item.Grow
                objItem.CheckResults = item.CheckResults
                objItem.SeedWeight = item.SeedWeight

            End If
        Next
    End Sub

    '<Action(Caption:="ดึงข้อมูล", ConfirmationMessage:="ท่านต้องการดึงข้อมูล ใช่หรือไม่?", ImageName:="BO_Note", AutoCommit:=True, TargetObjectsCriteria:="Status='Draft'")> _
    Public Sub ImportData()
        Session.Delete(SubmitQualityForBuyReportDetails)
        Dim fQualityAudits = New XPCollection(Of QualityAudit)(Session, CriteriaOperator.Parse("Contains(QualityAuditStep.StepName, 'จัดซื้อ') And PlanFarmer.PlanFarmerGroup.MainPlan = ? And LabDate Between(?, ?)", Me.MainPlan, Me.StartDay, Me.EndDate))
        If fQualityAudits.Count > 0 Then
            GetQualityAudit(fQualityAudits)
        Else
            OnChanged("SubmitQualityForBuyReportDetails")
        End If
        OnChanged("SubmitQualityForBuyReportDetails")
    End Sub


    ' <Action(Caption:="ส่งรายงาน", ConfirmationMessage:="ท่านต้องการส่งข้อมูลรายงานนี้ ใช่หรือไม่?", ImageName:="sent2", AutoCommit:=True, TargetObjectsCriteria:="Status='Draft'")> _
    Public Function MarkAsComplete() As Boolean
        If Not IsDeleted Then
            If Not SubmitQualityForBuyReportDetails.Count > 0 Then
                MsgBox("ไม่พบรายชื่อเกษตรกรผู้จัดทำแปลง กรุณาตรวจสอบข้อมูลอีกครั้ง", MsgBoxStyle.Information, "")
                Exit Function
            End If
            Try

                Dim objService As New RiceService.RiceService

                Dim objData As New RiceService.QualityForBuyHeader

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

                Dim listOfDetail As New List(Of RiceService.QualityForBuyDetail)
                For Each item As SubmitQualityForBuyDetail In SubmitQualityForBuyReportDetails
                    Dim objDetail As New RiceService.QualityForBuyDetail

                    objDetail.SampleNo = item.SampleNo
                    objDetail.PlanFarmerNo = item.PlanFarmerNo
                    objDetail.SampleNo = item.ItemNo
                    'objDetail.FullName = item.FullName
                    'objDetail.Plant = item.Plant
                    'objDetail.SeedType = item.SeedType
                    'objDetail.SeedClass = item.SeedClass
                    'objDetail.SeedSeason = item.SeedSeason
                    'objDetail.SeedYear = item.SeedYear
                    objDetail.Quantity = item.SeedWeight
                    objDetail.WetSeed = item.Wet
                    objDetail.PureSeed = item.PureSeed
                    objDetail.CompoundSeed = item.Compound
                    objDetail.OtherRiceSeed = item.OtherRiceSeed
                    objDetail.RedSeed = item.RedSeed
                    objDetail.GrowSeed = item.Grow
                    objDetail.OtherSeed = item.OtherSeed
                    objDetail.CheckDate = item.LabDate
                    objDetail.CheckResult = item.CheckResults

                    listOfDetail.Add(objDetail)
                Next

                objData.QualityForBuyDetails = listOfDetail.ToArray

                Dim ret As String = objService.SubmitDataQualityForBuyReport("NTiSecureCode", objData)

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
        ImportData()
    End Sub

    Public Function DoSubmitReport() As Boolean Implements ISubmitReportAble.DoSubmitReport
        Return MarkAsComplete()
    End Function
End Class
