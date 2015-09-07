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
<XafDisplayName("ส่งรายงาน ผลการทดสอบคุณภาพเมล็ดพันธุ์เพื่อยืนยันคุณภาพ (กคภ2556/02)")> _
<ConditionalAppearance.Appearance("SubmitCheckQualityReportDisableAllItems", criteria:="Status!='Draft'", Enabled:=False, TargetItems:="*", Context:="DetailView")> _
<DefaultClassOptions()> _
Public Class SubmitCheckQuality
    Inherits BaseObject
    Implements ISubmitReportAble

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        Me.Status = PublicEnum.SubmitReportStatus.Draft
    End Sub

    <Association("SubmitCheckQuality-SubmitCheckQualityDetails", GetType(SubmitCheckQualityDetail))> _
    <DevExpress.Xpo.Aggregated> _
    <XafDisplayName("ผลการทดสอบคุณภาพเมล็ดพันธุ์เพื่อยืนยันคุณภาพ")> _
    Public ReadOnly Property SubmitCheckQualityDetails() As XPCollection(Of SubmitCheckQualityDetail)
        Get
            Return GetCollection(Of SubmitCheckQualityDetail)("SubmitCheckQualityDetails")
        End Get
    End Property

    Dim fSeedClass As SeedClass
    <XafDisplayName("ชั้นพันธุ์")> _
    Public Property SeedClass() As SeedClass
        Get
            Return fSeedClass
        End Get
        Set(ByVal value As SeedClass)
            SetPropertyValue(Of SeedClass)("SeedClass", fSeedClass, value)
        End Set
    End Property

    Dim fSeason As Season
    <XafDisplayName("ฤดู")> _
    Public Property Season() As Season
        Get
            Return fSeason
        End Get
        Set(ByVal value As Season)
            SetPropertyValue(Of Season)("Season", fSeason, value)
        End Set
    End Property

    Dim fSeedYear As String
    <XafDisplayName("ปี")> _
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

    Public Sub ImportData()
        Dim fQualityAudits = New XPCollection(Of QualityAudit)(Session, CriteriaOperator.Parse("Contains(QualityAuditStep.StepName, 'ยืนยัน') And SeedClass = ? And Season = ? And SeedYear= ?", Me.SeedClass, Me.Season, Me.SeedYear))
        If fQualityAudits.Count > 0 Then
            GetCheckQuality(fQualityAudits)
            OnChanged("SubmitCheckQualityDetails")
        End If
    End Sub

    Private Sub GetCheckQuality(listCheckQuality As XPCollection(Of QualityAudit))
        Try
            For Each item As QualityAudit In listCheckQuality
                Dim objItem As SubmitCheckQualityDetail = Session.FindObject(Of SubmitCheckQualityDetail)(CriteriaOperator.Parse("SampleNo=? and SubmitCheckQuality=?", item.SampleNo, Me))
                If objItem Is Nothing Then
                    objItem = New SubmitCheckQualityDetail(Session)
                    objItem.SubmitCheckQuality = Me
                    objItem.SampleNo = item.SampleNo
                    objItem.SeedLot = item.LotNo
                    objItem.HarvestDate = item.HarvestDate
                    objItem.SeedSource = item.SampleSource
                    objItem.Bags = item.Bags
                    objItem.Quantity = item.SeedWeight
                    objItem.Wet = item.Wet
                    objItem.Grow = item.Grow
                    objItem.PureSeed = item.PureSeed
                    objItem.OtherSeed = item.OtherSeed
                    objItem.Compound = item.Compound
                    objItem.RedSeed = item.RedSeed
                    objItem.StickySeed = item.StickySeed
                    objItem.Strong = item.Strong
                    objItem.CheckDate = item.LabDate
                    objItem.CheckResult = item.CheckResults
                    objItem.Remark = item.Remark
                    objItem.SeedTypeName = item.SeedType.SeedName
                    objItem.Save()
                End If
            Next
            Session.CommitTransaction()
        Catch ex As Exception
            Session.RollbackTransaction()
        End Try
    End Sub

    Public Function MarkAsComplete() As Boolean
        If Not IsDeleted Then
            If Not SubmitCheckQualityDetails.Count > 0 Then
                MsgBox("ไม่พบผลการทดสอบคุณภาพเมล็ดพันธุ์เพื่อยืนยันคุณภาพ กรุณาตรวจสอบข้อมูลอีกครั้ง", MsgBoxStyle.Information, "")
                Exit Function
            End If
            Try

                Dim objService As New RiceService.RiceService

                Dim objData As New RiceService.CheckQualityHeader

                Dim objSiteInfo As SiteSetting = GetSiteInfo()

                objData.SiteNo = objSiteInfo.SiteNo
                objData.SiteName = objSiteInfo.SiteName
                objData.SeedClass = SeedClass.ClassName
                objData.Season = Season.SeasonName
                objData.SeedYear = SeedYear
                objData.SubmitBy = CType(SecuritySystem.CurrentUser, User).DisplayName
                objData.SubmitDate = Date.Now

                Dim listOfDetail As New List(Of RiceService.CheckQualityDetail)
                For Each item As SubmitCheckQualityDetail In SubmitCheckQualityDetails
                    Dim objDetail As New RiceService.CheckQualityDetail

                    objDetail.SampleNo = item.SampleNo
                    objDetail.LotNo = item.SeedLot
                    objDetail.HarvestDate = item.HarvestDate
                    objDetail.SampleSource = item.SeedSource
                    objDetail.Bags = item.Bags
                    objDetail.SeedWeight = item.Quantity
                    objDetail.Wet = item.Wet
                    objDetail.Grow = item.Grow
                    objDetail.PureSeed = item.PureSeed
                    objDetail.OtherSeed = item.OtherSeed
                    objDetail.Compound = item.Compound
                    objDetail.RedSeed = item.RedSeed
                    objDetail.StickySeed = item.StickySeed
                    objDetail.Strong = item.Strong
                    objDetail.LabDate = item.CheckDate
                    objDetail.CheckResults = item.CheckResult
                    objDetail.Remark = item.Remark
                    objDetail.SeedTypeName = item.SeedTypeName

                    listOfDetail.Add(objDetail)

                Next

                objData.CheckQualityDetails = listOfDetail.ToArray

                Dim ret As String = objService.SubmitCheckQualityReport("NTiSecureCode", objData)

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
