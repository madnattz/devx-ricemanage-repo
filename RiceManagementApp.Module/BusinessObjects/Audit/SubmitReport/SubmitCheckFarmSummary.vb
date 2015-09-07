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
<XafDisplayName("ส่งรายงาน ผลการตรวจตัดสินคุณภาพแปลงขยายพันธุ์")> _
<DefaultClassOptions()> _
<ConditionalAppearance.Appearance("SubmitCheckFarmSummaryReportDisableAllItems", criteria:="Status!='Draft'", Enabled:=False, TargetItems:="*", Context:="DetailView")> _
Public Class SubmitCheckFarmSummary
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
        'SubmitCheckFarmSummaryDetails.Sorting.Add(New SortProperty("PlanYear,MonthNo", DevExpress.Xpo.DB.SortingDirection.Ascending))
        Dim sortItem(2) As SortProperty
        sortItem(0) = New SortProperty("PlanYear", DB.SortingDirection.Ascending)
        sortItem(1) = New SortProperty("MonthNo", DB.SortingDirection.Ascending)
        SubmitCheckFarmSummaryDetails.Sorting.AddRange(sortItem)
    End Sub

    <Association("SubmitCheckFarmSummary-SubmitCheckFarmSummaryDetails", GetType(SubmitCheckFarmSummaryDetail))> _
    <DevExpress.Xpo.Aggregated> _
    <XafDisplayName("ผลการตรวจสอบคุณภาพแปลงขยายพันธุ์")> _
    Public ReadOnly Property SubmitCheckFarmSummaryDetails() As XPCollection(Of SubmitCheckFarmSummaryDetail)
        Get
            Return GetCollection(Of SubmitCheckFarmSummaryDetail)("SubmitCheckFarmSummaryDetails")
        End Get
    End Property

    Private Sub GetAuditCheckFarmPlant(listAuditCheckFarmPlant As XPCollection(Of AuditCheckFarmPlanDetail))
        For Each item As AuditCheckFarmPlanDetail In listAuditCheckFarmPlant
            Dim objItem As SubmitCheckFarmSummaryDetail = Session.FindObject(Of SubmitCheckFarmSummaryDetail)(CriteriaOperator.Parse("MainPlan=? and MainPlan.PlanStatus='Pending' and SubmitCheckFarmSummary=? ", item.AuditActivityPlan.MainPlan, Me))
            If objItem Is Nothing Then
                objItem = New SubmitCheckFarmSummaryDetail(Session)
                objItem.SubmitCheckFarmSummary = Me
                objItem.MainPlan = item.AuditActivityPlan.MainPlan
                objItem.MonthNo = item.MonthNo
                objItem.PlanYear = item.PlanYear
                objItem.AreaSize = item.AreaSize
                objItem.ActualAreaSize = item.ActualAreaSize
                objItem.ActualPassArea = item.ActualPassArea
                objItem.FullDamageArea = item.FullDamageArea
                objItem.FailReason = item.FailReason
                objItem.Save()
            End If
        Next
    End Sub

    Dim fSeedYear As String
    <Index(1)>
    <XafDisplayName("ปีงบประมาณ")> _
    <ImmediatePostData()> _
    <RuleRequiredField(DefaultContexts.Save)> _
    <ModelDefault("PropertyEditorType", "RiceManagementApp.Module.Win.CustomYearDropdown")> _
    Public Property SeedYear() As String
        Get
            Return fSeedYear
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SeedYear", fSeedYear, value)
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

    Public Sub ImportData()
        Dim fAuditCheckFarmPlanDetails = New XPCollection(Of AuditCheckFarmPlanDetail)(Session, CriteriaOperator.Parse("AuditActivityPlan.MainPlan.SeedYear=? and AuditActivityPlan.MainPlan.PlanStatus='Pending' ", Me.SeedYear))
        If fAuditCheckFarmPlanDetails.Count > 0 Then
            GetAuditCheckFarmPlant(fAuditCheckFarmPlanDetails)
            OnChanged("SubmitCheckFarmSummaryDetails")
        End If
    End Sub

    Public Function MarkAsComplete() As Boolean
        If Not IsDeleted Then
            If Not SubmitCheckFarmSummaryDetails.Count > 0 Then
                MsgBox("ไม่พบข้อมูล กรุณาตรวจสอบข้อมูลอีกครั้ง", MsgBoxStyle.Information, "")
                Exit Function
            End If
            Try

                Dim objService As New RiceService.RiceService

                Dim objData As New List(Of RiceService.CheckFarmSummaryHeader)

                Dim objSiteInfo As SiteSetting = GetSiteInfo()

                For Each item As SubmitCheckFarmSummaryDetail In SubmitCheckFarmSummaryDetails
                    Dim objHeader As New RiceService.CheckFarmSummaryHeader
                    objHeader.SiteNo = objSiteInfo.SiteNo
                    objHeader.SiteName = objSiteInfo.SiteName
                    objHeader.PlantName = item.MainPlan.Plant.PlantName
                    objHeader.SeedTypeName = item.MainPlan.SeedType.SeedName
                    objHeader.SeedClassName = item.MainPlan.SeedClass.ClassName
                    objHeader.SeasonName = item.MainPlan.Season.SeasonName
                    objHeader.SeedYear = item.MainPlan.SeedYear
                    objHeader.SeedLot = item.MainPlan.Lot
                    objHeader.Remark = ""
                    objHeader.SubmitBy = CType(SecuritySystem.CurrentUser, User).DisplayName
                    objHeader.SubmitDate = Date.Now

                    Dim listOfDetail As New List(Of RiceService.CheckFarmSummaryDetail)
                    Dim objDetail As New RiceService.CheckFarmSummaryDetail
                    For i As Integer = 1 To 12
                        For Each row As SubmitCheckFarmSummaryDetail In SubmitCheckFarmSummaryDetails
                            Select Case row.MonthNo
                                Case PublicEnum.EnumMonth.JAN
                                    objDetail.JanPlanSize = row.AreaSize
                                    objDetail.JanCheckSize = row.ActualAreaSize
                                    objDetail.JanPassSize = row.ActualPassArea
                                    objDetail.JanFullDamageSize = row.FullDamageArea
                                    objDetail.JanFailReason = row.FailReason
                                Case PublicEnum.EnumMonth.FEB
                                    objDetail.FebPlanSize = row.AreaSize
                                    objDetail.FebCheckSize = row.ActualAreaSize
                                    objDetail.FebPassSize = row.ActualPassArea
                                    objDetail.FebFullDamageSize = row.FullDamageArea
                                    objDetail.FebFailReason = row.FailReason
                                Case PublicEnum.EnumMonth.MAR
                                    objDetail.MarPlanSize = row.AreaSize
                                    objDetail.MarCheckSize = row.ActualAreaSize
                                    objDetail.MarPassSize = row.ActualPassArea
                                    objDetail.MarFullDamageSize = row.FullDamageArea
                                    objDetail.MarFailReason = row.FailReason
                                Case PublicEnum.EnumMonth.APR
                                    objDetail.AprPlanSize = row.AreaSize
                                    objDetail.AprCheckSize = row.ActualAreaSize
                                    objDetail.AprPassSize = row.ActualPassArea
                                    objDetail.AprFullDamageSize = row.FullDamageArea
                                    objDetail.AprFailReason = row.FailReason
                                Case PublicEnum.EnumMonth.MAY
                                    objDetail.MayPlanSize = row.AreaSize
                                    objDetail.MayCheckSize = row.ActualAreaSize
                                    objDetail.MayPassSize = row.ActualPassArea
                                    objDetail.MayFullDamageSize = row.FullDamageArea
                                    objDetail.MayFailReason = row.FailReason
                                Case PublicEnum.EnumMonth.JUN
                                    objDetail.JunPlanSize = row.AreaSize
                                    objDetail.JunCheckSize = row.ActualAreaSize
                                    objDetail.JunPassSize = row.ActualPassArea
                                    objDetail.JunFullDamageSize = row.FullDamageArea
                                    objDetail.JunFailReason = row.FailReason
                                Case PublicEnum.EnumMonth.JUL
                                    objDetail.JulPlanSize = row.AreaSize
                                    objDetail.JulCheckSize = row.ActualAreaSize
                                    objDetail.JulPassSize = row.ActualPassArea
                                    objDetail.JulFullDamageSize = row.FullDamageArea
                                    objDetail.JulFailReason = row.FailReason
                                Case PublicEnum.EnumMonth.AUG
                                    objDetail.AugPlanSize = row.AreaSize
                                    objDetail.AugCheckSize = row.ActualAreaSize
                                    objDetail.AugPassSize = row.ActualPassArea
                                    objDetail.AugFullDamageSize = row.FullDamageArea
                                    objDetail.AugFailReason = row.FailReason
                                Case PublicEnum.EnumMonth.SEP
                                    objDetail.SepPlanSize = row.AreaSize
                                    objDetail.SepCheckSize = row.ActualAreaSize
                                    objDetail.SepPassSize = row.ActualPassArea
                                    objDetail.SepFullDamageSize = row.FullDamageArea
                                    objDetail.SepFailReason = row.FailReason
                                Case PublicEnum.EnumMonth.OCT
                                    objDetail.OctPlanSize = row.AreaSize
                                    objDetail.OctCheckSize = row.ActualAreaSize
                                    objDetail.OctPassSize = row.ActualPassArea
                                    objDetail.OctFullDamageSize = row.FullDamageArea
                                    objDetail.OctFailReason = row.FailReason
                                Case PublicEnum.EnumMonth.NOV
                                    objDetail.NovPlanSize = row.AreaSize
                                    objDetail.NovCheckSize = row.ActualAreaSize
                                    objDetail.NovPassSize = row.ActualPassArea
                                    objDetail.NovFullDamageSize = row.FullDamageArea
                                    objDetail.NovFailReason = row.FailReason
                                Case PublicEnum.EnumMonth.DEC
                                    objDetail.DecPlanSize = row.AreaSize
                                    objDetail.DecCheckSize = row.ActualAreaSize
                                    objDetail.DecPassSize = row.ActualPassArea
                                    objDetail.DecFullDamageSize = row.FullDamageArea
                                    objDetail.DecFailReason = row.FailReason


                            End Select
                        Next

                    Next
                    listOfDetail.Add(objDetail)

                    objHeader.CheckFarmSummaryDetails = listOfDetail.ToArray

                    objData.Add(objHeader)

                Next

                '//ส่งข้อมูลผ่าน Webservice
                Dim ret As String = objService.SubmitDataCheckFarmSummaryReport("NTiSecureCode", objData.ToArray)

                '//เป็นสถานะเป็น ส่งข้อมูลแล้ว (Sent)
                If ret = "Success" Then
                    Me.Status = PublicEnum.SubmitReportStatus.Sent
                    Me.SubmitDate = Date.Now
                    Me.SubmitBy = CType(SecuritySystem.CurrentUser, User).DisplayName
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
        Dim sortItem(2) As SortProperty
        sortItem(0) = New SortProperty("PlanYear", DB.SortingDirection.Ascending)
        sortItem(1) = New SortProperty("MonthNo", DB.SortingDirection.Ascending)
        SubmitCheckFarmSummaryDetails.Sorting.AddRange(sortItem)
    End Sub

    Public Function DoSubmitReport() As Boolean Implements ISubmitReportAble.DoSubmitReport
        Return MarkAsComplete()
    End Function
End Class
