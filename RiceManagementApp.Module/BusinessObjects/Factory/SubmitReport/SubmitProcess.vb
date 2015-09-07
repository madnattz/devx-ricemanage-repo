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
<XafDisplayName("ส่งรายงาน การปรับปรุงสภาพเมล็ดพันธุ์รายวัน")> _
<ConditionalAppearance.Appearance("SubmitPlanKP3DisableAllItems", criteria:="Status!='Draft'", Enabled:=False, TargetItems:="*", Context:="DetailView")> _
<DefaultClassOptions()> _
Public Class SubmitProcess
    Inherits BaseObject
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        Me.Status = PublicEnum.SubmitReportStatus.Draft
    End Sub

    <Association("SubmitProcess-SubmitProcessDetails", GetType(SubmitProcessDetail))> _
  <DevExpress.Xpo.Aggregated> _
    <XafDisplayName("เมล็ดพันธุ์คงคลังโรงงาน")> _
    Public ReadOnly Property SubmitProcessDetails() As XPCollection(Of SubmitProcessDetail)
        Get
            Return GetCollection(Of SubmitProcessDetail)("SubmitProcessDetails")
        End Get
    End Property

    Dim fBiweekly As PublicEnum.EnumBiweekly
    <XafDisplayName("ปักษ์")> _
    <ImmediatePostData()> _
    <Index(1)>
    <RuleValueComparison(DefaultContexts.Save, ValueComparisonType.NotEquals, "None")> _
    Public Property Biweekly() As PublicEnum.EnumBiweekly
        Get
            Return fBiweekly
        End Get
        Set(ByVal value As PublicEnum.EnumBiweekly)
            SetPropertyValue(Of PublicEnum.EnumBiweekly)("Biweekly", fBiweekly, value)
        End Set
    End Property

    Dim fProcessMonth As PublicEnum.EnumMonth
    <XafDisplayName("เดือน")> _
    <Index(2)>
    <ImmediatePostData()> _
    <RuleValueComparison(DefaultContexts.Save, ValueComparisonType.NotEquals, "None")> _
    Public Property ProcessMonth() As PublicEnum.EnumMonth
        Get
            Return fProcessMonth
        End Get
        Set(ByVal value As PublicEnum.EnumMonth)
            SetPropertyValue(Of PublicEnum.EnumMonth)("ProcessMonth", fProcessMonth, value)
        End Set
    End Property

    Dim fProcessYear As String
    <XafDisplayName("ปี")> _
    <Index(3)>
    <RuleRequiredField(DefaultContexts.Save)> _
    <ModelDefault("PropertyEditorType", "RiceManagementApp.Module.Win.CustomYearDropdown")> _
    Public Property ProcessYear() As String
        Get
            Return fProcessYear
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ProcessYear", fProcessYear, value)
        End Set
    End Property

    'Dim fLot As String
    '<XafDisplayName("รุ่นที่")> _
    '<RuleRequiredField(DefaultContexts.Save)> _
    '<ModelDefault("PropertyEditorType", "RiceManagementApp.Module.Win.CustomLotDropdown")> _
    'Public Property Lot() As String
    '    Get
    '        Return fLot
    '    End Get
    '    Set(ByVal value As String)
    '        SetPropertyValue(Of String)("Lot", fLot, value)
    '    End Set
    'End Property

    Dim fSubmitDate As Date
    <XafDisplayName("วันที่ส่งรายงาน")> _
    <Index(4)>
    Public Property SubmitDate() As Date
        Get
            Return fSubmitDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue(Of Date)("SubmitDate", fSubmitDate, value)
        End Set
    End Property

    Dim fSubmitBy As String
    <Index(5)>
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
    <Index(6)>
    Public Property Status() As PublicEnum.SubmitReportStatus
        Get
            Return fStatus
        End Get
        Set(ByVal value As PublicEnum.SubmitReportStatus)
            SetPropertyValue(Of PublicEnum.SubmitReportStatus)("Status", fStatus, value)
        End Set
    End Property

    Private Sub GetFactorySeedProcessToday(listFactorySeedProcessToday As XPCollection(Of FactorySeedProcessToday))
        For Each item As FactorySeedProcessToday In listFactorySeedProcessToday
            Dim objItem As SubmitProcessDetail = Session.FindObject(Of SubmitProcessDetail)(CriteriaOperator.Parse("ProcessDate=? and SubmitProcess=?", item.ProcessDate, Me))
            If objItem Is Nothing Then
                objItem = New SubmitProcessDetail(Session)
                objItem.SubmitProcess = Me
                objItem.ProcessDate = item.ProcessDate
                objItem.PlantName = item.FactorySeedProcess.Plant.PlantName
                objItem.SeedName = item.FactorySeedProcess.SeedType.SeedName
                objItem.ClassName = item.FactorySeedProcess.SeedClass.ClassName
                objItem.SeasonName = item.FactorySeedProcess.Season.SeasonName
                objItem.SeedYear = item.FactorySeedProcess.SeedYear
                objItem.OutputQuantity = item.SeedProcessAmount
                objItem.NormalTime = item.NormalTime
                objItem.OverTime = item.OverTime
                objItem.ShiftTime = item.ShiftTime
                objItem.StartTime = item.StartTime
                objItem.CompleteTime = item.EndTime
                objItem.ImproveComplete = item.ImproveComplete
                objItem.FactoryName = item.FactorySeedProcess.FactoryNo
                objItem.Lot = item.FactorySeedProcess.Lot
                objItem.Save()
            End If
        Next
    End Sub

    <Action(Caption:="ดึงข้อมูล", ConfirmationMessage:="ท่านต้องการดึงข้อมูล ใช่หรือไม่?", ImageName:="BO_Note", AutoCommit:=True, TargetObjectsCriteria:="Status='Draft'")> _
    Public Sub ImportData()
        If Me.Biweekly <> PublicEnum.EnumBiweekly.None And Me.ProcessMonth <> PublicEnum.EnumMonth.None And Me.ProcessYear <> "" Then

            'Session.Delete(SubmitProcessDetails)
            Dim sDay As Integer = 1
            Dim eDay As Integer = 30

            If Biweekly = PublicEnum.EnumBiweekly.WeekOne Then
                sDay = 1
                eDay = 15
            ElseIf Biweekly = PublicEnum.EnumBiweekly.WeekTwo Then
                sDay = 16
                eDay = Date.DaysInMonth(CInt(ProcessYear) - 543, ProcessMonth)
            End If

            Dim sDate As New Date(CInt(ProcessYear) - 543, ProcessMonth, sDay)
            Dim eDate As New Date(CInt(ProcessYear) - 543, ProcessMonth, eDay)
            Dim fFactoryProcessSeedDetails = New XPCollection(Of FactorySeedProcessToday)(Session, CriteriaOperator.Parse("ProcessDate >= ? and ProcessDate <= ?", sDate, eDate))
            If fFactoryProcessSeedDetails.Count > 0 Then
                GetFactorySeedProcessToday(fFactoryProcessSeedDetails)
                OnChanged("SubmitPlantKP3Details")
                Session.CommitTransaction()
            End If
        End If
    End Sub

    <Action(Caption:="ส่งรายงาน", ConfirmationMessage:="ท่านต้องการส่งข้อมูลรายงานนี้ ใช่หรือไม่?", ImageName:="sent2", AutoCommit:=True, TargetObjectsCriteria:="Status='Draft'")> _
    Public Sub MarkAsComplete()
        If Not IsDeleted Then
            If Not SubmitProcessDetails.Count > 0 Then
                MsgBox("ไม่พบรายชื่อเมล็ดพันธุ์ปรับปรุงสภาพรายวัน กรุณาตรวจสอบข้อมูลอีกครั้ง", MsgBoxStyle.Information, "")
                Exit Sub
            End If
            Try
                '//ประกาศตัวแปล webservice
                Dim objService As New RiceService.RiceService

                '//ประกาศ Object ของรายงาน แบบแผนสรุปผลการปรับปรุงสภาพเมล็ดพันธุ์ข้าวรายวัน (Header)
                Dim objData As New RiceService.DailyProcessHeader

                '//ข้อมูลตั้งค่าของศูนย์
                Dim objSiteInfo As SiteSetting = GetSiteInfo()

                '//ใส่ค่าให้กับ object รายงาน แบบแผนสรุปผลการปรับปรุงสภาพเมล็ดพันธุ์ข้าวรายวัน(ข้อมูลส่วน Header)
                objData.SiteNo = objSiteInfo.SiteNo
                objData.SiteName = objSiteInfo.SiteName
                objData.ProcessWeek = Biweekly
                objData.ProcessMonth = ProcessMonth
                objData.ProcessYear = ProcessYear
                objData.SubmitBy = CType(SecuritySystem.CurrentUser, User).DisplayName
                objData.SubmitDate = Date.Now

                Dim sDay As Integer = 1
                Dim eDay As Integer = 30

                If Biweekly = PublicEnum.EnumBiweekly.WeekOne Then
                    sDay = 1
                    eDay = 15
                ElseIf Biweekly = PublicEnum.EnumBiweekly.WeekTwo Then
                    sDay = 16
                    eDay = Date.DaysInMonth(CInt(ProcessYear) - 543, ProcessMonth)
                End If

                Dim sDate As New Date(CInt(ProcessYear) - 543, ProcessMonth, sDay)
                Dim eDate As New Date(CInt(ProcessYear) - 543, ProcessMonth, eDay)

                objData.ProcessDateFullName = "วันที่ " & sDate.ToString("d MMMM yyyy", New System.Globalization.CultureInfo("th-TH")) & _
                                                               "   ถึงวันที่ " & eDate.ToString("d MMMM yyyy", New System.Globalization.CultureInfo("th-TH"))

                '//ตัวแปลของ แบบแผนสรุปผลการปรับปรุงสภาพเมล็ดพันธุ์ข้าวรายวัน(Detail) (โรงงานและข้อมูลการปรับปรุง) ประเภท List
                Dim listOfDetail As New List(Of RiceService.DailyProcessDetail)
                For Each item As SubmitProcessDetail In SubmitProcessDetails
                    Dim objDetail As New RiceService.DailyProcessDetail
                    objDetail.ProcessDate = item.ProcessDate
                    objDetail.PlantName = item.PlantName
                    objDetail.SeedName = item.SeedName
                    objDetail.ClassName = item.ClassName
                    objDetail.SeasonName = item.SeasonName
                    objDetail.SeedYear = item.SeedYear
                    objDetail.OutputQuantity = item.OutputQuantity
                    objDetail.NormalTime = item.NormalTime
                    objDetail.OverTime = item.OverTime
                    objDetail.ShiftTime = item.ShiftTime
                    objDetail.ImproveComplete = item.ImproveComplete
                    objDetail.FactoryName = item.FactoryName
                    listOfDetail.Add(objDetail)
                Next

                '  //ใส่รายชื่อเกษตรกร (Detail) ในส่วนของ Header
                '  //ต้องแปลง List ให้เป็น Array เพราะส่งข้อมูลผ่านเว็บเซอร์วิส ต้องเป็น Array 
                objData.DailyProcessDetails = listOfDetail.ToArray

                ' //ส่งข้อมูลผ่าน Webservice
                Dim ret As String = objService.SubmitDataDailyProcessReport("NTiSecureCode", objData)

                '//เป็นสถานะเป็น ส่งข้อมูลแล้ว (Sent)
                If ret = "Success" Then
                    Me.Status = PublicEnum.SubmitReportStatus.Sent
                    Me.SubmitDate = Date.Now
                    Me.SubmitBy = CType(SecuritySystem.CurrentUser, User).DisplayName

                    ' //บันทึกข้อมูล
                    MsgBox("ส่งข้อมูลรายงาน เรียบร้อยแล้ว", MsgBoxStyle.Information)
                    Me.Save()
                    Session.CommitTransaction()
                Else
                    MsgBox("เกิดข้อผิดพลาด ไม่สามารถส่งรายงานได้ กรุณาติดต่อผู้ดูแลระบบ", MsgBoxStyle.Critical)
                End If

            Catch ex As Exception
                MsgBox("เกิดข้อผิดพลาด ไม่สามารถส่งรายงานได้ กรุณาติดต่อผู้ดูแลระบบ", MsgBoxStyle.Critical)
            End Try

        End If
    End Sub

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

End Class
