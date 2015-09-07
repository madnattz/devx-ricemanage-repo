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
Imports DevExpress.Xpo.DB
Imports DevExpress.ExpressApp.ConditionalAppearance

<ImageName("sent3")> _
<XafDisplayName("ส่งรายงานปริมาณการตรวจสอบคุณภาพเมล็ดพันธุ์ข้าวประจำเดือน")> _
<DefaultClassOptions()> _
<ConditionalAppearance.Appearance("SubmitCheckQualityForMonthDisableAllItems", criteria:="Status!='Draft'", Enabled:=False, TargetItems:="*", Context:="DetailView")> _
Public Class SubmitCheckQualityForMonth
    Inherits BaseObject
    Implements ISubmitReportAble

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        Me.Status = PublicEnum.SubmitReportStatus.Draft
    End Sub

    <Association("SubmitCheckQualityForMonth -SubmitCheckQualityForMonthDetails")> _
   <DevExpress.Xpo.Aggregated> _
   <XafDisplayName("ปริมาณการตรวจสอบคุณภาพเมล็ดพันธุ์ข้าวประจำเดือน")> _
    Public ReadOnly Property SubmitCheckQualityForMonthDetails() As XPCollection(Of SubmitCheckQualityForMonthDetail)
        Get
            Return GetCollection(Of SubmitCheckQualityForMonthDetail)("SubmitCheckQualityForMonthDetails")
        End Get
    End Property

    Dim fMonth As PublicEnum.EnumMonth
    <Index(1)>
    <RuleRequiredField(DefaultContexts.Save)> _
    <XafDisplayName("เดือน")> _
    <ImmediatePostData()> _
    <RuleValueComparison(DefaultContexts.Save, ValueComparisonType.NotEquals, "None")> _
    Public Property Month() As PublicEnum.EnumMonth
        Get
            Return fMonth
        End Get
        Set(ByVal value As PublicEnum.EnumMonth)
            SetPropertyValue(Of PublicEnum.EnumMonth)("Month", fMonth, value)
            OnChanged("ReportDetails")
        End Set
    End Property

    Dim fSeedYear As String
    <Index(2)>
    <RuleRequiredField(DefaultContexts.Save)> _
    <ImmediatePostData()> _
    <XafDisplayName("ปี")> _
    <ModelDefault("PropertyEditorType", "RiceManagementApp.Module.Win.CustomYearDropdown")> _
    Public Property SeedYear() As String
        Get
            Return fSeedYear
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SeedYear", fSeedYear, value)
            OnChanged("ReportDetails")
        End Set
    End Property

    Dim fSubmitDate As Date
    <Index(3)>
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
    <Index(4)>
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
    <Index(5)>
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
        Try
            Dim data As SelectedData = Session.ExecuteSproc("SP_GetCheckQualityForMonth", New OperandValue(CInt(Month)), New OperandValue(SeedYear))
            For Each row As SelectStatementResultRow In data.ResultSet(0).Rows
                If row IsNot Nothing Then
                    Dim obj As SubmitCheckQualityForMonthDetail = Session.FindObject(Of SubmitCheckQualityForMonthDetail)(CriteriaOperator.Parse("SeedTypeName=? and SeedClassName=? and SeasonName=? and SeedYear=? and SubmitCheckQualityForMonth=?", row.Values(0), row.Values(1), row.Values(2), row.Values(3), Me))
                    If obj Is Nothing Then

                        obj = New SubmitCheckQualityForMonthDetail(Session)

                        obj.SubmitCheckQualityForMonth = Me
                        obj.SeedTypeName = row.Values(0)
                        obj.SeedClassName = row.Values(1)
                        obj.SeasonName = row.Values(2)
                        obj.SeedYear = row.Values(3)
                        obj.StepName = row.Values(4)

                        obj.ItemCount = row.Values(5)

                        obj.CheckMonthNo = ConvertToInteger(row.Values(7))
                        obj.CheckMonthName = New Date(Date.Now.Year, row.Values(7), 1).ToString("MMMM", New System.Globalization.CultureInfo("th-TH"))
                        obj.CheckYear = SeedYear

                        obj.PassQuantity = ConvertToInteger(row.Values(6))
                        obj.Wet = ConvertToInteger(row.Values(8))
                        obj.Compound = ConvertToInteger(row.Values(10))
                        obj.Grow = ConvertToInteger(row.Values(13))
                        obj.Strong = ConvertToInteger(row.Values(14))
                        obj.PureSeed = ConvertToInteger(row.Values(9))
                        obj.OtherSeed = ConvertToInteger(row.Values(19))
                        obj.OtherRice = ConvertToInteger(row.Values(11))
                        obj.RedSeed = ConvertToInteger(row.Values(23))
                        obj.GreenSeed = ConvertToInteger(row.Values(20))
                        obj.FloatSeed = ConvertToInteger(row.Values(22))
                        obj.Insect = ConvertToInteger(row.Values(18))
                        obj.DiseaseSeed = ConvertToInteger(row.Values(21))
                        obj.KOH = ConvertToInteger(row.Values(15))
                        obj.Iodine = ConvertToInteger(row.Values(16))
                        obj.StickySeed = ConvertToInteger(row.Values(12))
                        obj.AATest = ConvertToInteger(row.Values(17))
                        obj.AllTestCount = 0

                        obj.Remark = ""

                        obj.Save()
                    End If
                End If
            Next
            Session.CommitTransaction()
        Catch ex As Exception
            Session.RollbackTransaction()
        End Try
    End Sub

    Public Function MarkAsComplete() As Boolean
        If Not IsDeleted Then
            If Not SubmitCheckQualityForMonthDetails.Count > 0 Then
                MsgBox("ไม่พบรายชื่อเกษตรกรผู้จัดทำแปลง กรุณาตรวจสอบข้อมูลอีกครั้ง", MsgBoxStyle.Information, "")
                Exit Function
            End If

            Dim objService As New RiceService.RiceService
            Dim objData As New List(Of RiceService.CheckQualityForMonthData)

            Try
                Dim objSiteInfo As SiteSetting = GetSiteInfo()
                For Each rowDetail As SubmitCheckQualityForMonthDetail In SubmitCheckQualityForMonthDetails
                    If rowDetail IsNot Nothing Then
                        Dim objDetail As New RiceService.CheckQualityForMonthData

                        objDetail.SiteNo = objSiteInfo.SiteNo
                        objDetail.SiteName = objSiteInfo.SiteName
                        objDetail.CheckMonthNo = rowDetail.CheckMonthNo
                        objDetail.CheckMonthName = rowDetail.CheckMonthName
                        objDetail.CheckYear = SeedYear
                        objDetail.SeedTypeName = rowDetail.SeedTypeName
                        objDetail.SeedTypeName = rowDetail.SeedTypeName
                        objDetail.SeedYear = rowDetail.SeedYear
                        objDetail.StepName = rowDetail.StepName
                        objDetail.SampleCount = rowDetail.ItemCount
                        objDetail.Wet = rowDetail.Wet
                        objDetail.Compound = rowDetail.Compound
                        objDetail.Grow = rowDetail.Grow
                        objDetail.Strong = rowDetail.Strong
                        objDetail.PureSeed = rowDetail.PureSeed
                        objDetail.OtherSeed = rowDetail.OtherSeed
                        objDetail.OtherRice = rowDetail.OtherRice
                        objDetail.RedSeed = rowDetail.RedSeed
                        objDetail.GreenSeed = rowDetail.GreenSeed
                        objDetail.FloatSeed = rowDetail.FloatSeed
                        objDetail.Insect = rowDetail.Insect
                        objDetail.KOH = rowDetail.KOH
                        objDetail.Iodine = rowDetail.Iodine
                        objDetail.StickySeed = rowDetail.StickySeed
                        objDetail.AATest = rowDetail.AATest
                        objDetail.AllTestCount = rowDetail.AllTestCount
                        objDetail.PassQuantity = rowDetail.PassQuantity
                        objDetail.SubmitDate = Date.Now
                        objDetail.SubmitBy = CType(SecuritySystem.CurrentUser, User).DisplayName
                        objData.Add(objDetail)
                    End If
                Next

                Dim ret As String = objService.SubmitCheckQualityForMonth("NTiSecureCode", objData.ToArray)


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
    End Sub

    Public Function DoSubmitReport() As Boolean Implements ISubmitReportAble.DoSubmitReport
        Return MarkAsComplete()
    End Function
End Class
