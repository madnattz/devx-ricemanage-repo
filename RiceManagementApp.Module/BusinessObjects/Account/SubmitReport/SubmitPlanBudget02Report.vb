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

'<ImageName("BO_Contact")> _
'<DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")> _
'<DefaultListViewOptions(MasterDetailMode.ListViewOnly, False, NewItemRowPosition.None)> _
'<Persistent("DatabaseTableName")> _
<XafDisplayName("ส่งรายงาน สงป.302")> _
<ConditionalAppearance.Appearance("SubmitPlanBudget02ReportDisableAllItems", criteria:="Status!='Draft'", Enabled:=False, TargetItems:="*", Context:="DetailView")> _
<DefaultClassOptions()> _
Public Class SubmitPlanBudget02Report ' Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        Me.Status = PublicEnum.SubmitReportStatus.Draft
        MyBase.AfterConstruction()
        ' Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).
    End Sub

    Private _BudgetYear As String
    <XafDisplayName("ปีงบประมาณ")> _
<RuleRequiredField(DefaultContexts.Save)> _
<Index(0)> _
    Public Property BudgetYear() As String
        Get
            Return _BudgetYear
        End Get
        Set(ByVal value As String)
            SetPropertyValue("BudgetYear", _BudgetYear, value)
        End Set
    End Property

    Private _BudgetMonth As PublicEnum.EnumMonth
    <XafDisplayName("ประจำเดือน")> _
<RuleRequiredField(DefaultContexts.Save)> _
<Index(0)> _
    Public Property BudgetMonth() As PublicEnum.EnumMonth
        Get
            Return _BudgetMonth
        End Get
        Set(ByVal value As PublicEnum.EnumMonth)
            SetPropertyValue("BudgetMonth", _BudgetMonth, value)
        End Set
    End Property

    Private _PlanName As SettingPlan
    <XafDisplayName("ชื่อแผนงาน")> _
    <Index(1)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property PlanName() As SettingPlan
        Get
            Return _PlanName
        End Get
        Set(ByVal value As SettingPlan)
            SetPropertyValue("PlanName", _PlanName, value)
        End Set
    End Property

    '<Appearance("DISPro", Enabled:=False, Criteria:="[PlaningStatus] <> 'status0'", Context:="DetailView")> _
    Private _ProjectName As SettingProject
    <DataSourceProperty("PlanName.SettingProjects")> _
    <XafDisplayName("ชื่อโครงการ")> _
    <Index(2)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property ProjectName() As SettingProject
        Get
            Return _ProjectName
        End Get
        Set(ByVal value As SettingProject)
            SetPropertyValue("ProjectName", _ProjectName, value)
        End Set
    End Property

    '<Appearance("DISAct", Enabled:=False, Criteria:="[PlaningStatus] <> 'status0'", Context:="DetailView")> _
    Private _ActivityName As SettingActivity
    <DataSourceProperty("ProjectName.SettingActivitys")> _
    <XafDisplayName("ชื่อกิจกรรมหลัก")> _
    <Index(3)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property ActivityName() As SettingActivity
        Get
            Return _ActivityName
        End Get
        Set(ByVal value As SettingActivity)
            SetPropertyValue("ActivityName", _ActivityName, value)
        End Set
    End Property

    Private _SubActivityName As SettingSubActivity
    <DataSourceProperty("ActivityName.SettingSubActivitys")> _
    <XafDisplayName("ชื่อกิจกรรมย่อยโครงการ")> _
    <Index(4)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property SubActivityName() As SettingSubActivity
        Get
            Return _SubActivityName
        End Get
        Set(ByVal value As SettingSubActivity)
            SetPropertyValue("SubActivityName", _SubActivityName, value)
        End Set
    End Property

    Dim fSubmitDate As Date = Today
    <Appearance("SubmitDateBudget02", Enabled:=False, Context:="DetailView")> _
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

    <Association("SubmitPlanBudget02Report-PlanBudgetData02s", GetType(PlanBudgetData02))> _
<DevExpress.Xpo.Aggregated> _
<XafDisplayName("ข้อมูลรายจ่าย")> _
    Public ReadOnly Property PlanBudgetData02s() As XPCollection(Of PlanBudgetData02)
        Get
            Return GetCollection(Of PlanBudgetData02)("PlanBudgetData02s")
        End Get
    End Property

    <Action(Caption:="ส่งรายงาน", ConfirmationMessage:="ท่านต้องการส่งข้อมูลรายงานนี้ ใช่หรือไม่?", ImageName:="Action_Grant", AutoCommit:=True, TargetObjectsCriteria:="Status='Draft'")> _
    Public Sub MarkAsComplete()
        If Not IsDeleted Then
            If Not PlanBudgetData02s.Count > 0 Then
                MsgBox("ไม่พบรายการสงป.302 กรุณาตรวจสอบข้อมูลอีกครั้ง", MsgBoxStyle.Information, "")
                Exit Sub
            End If

            'Dim accStartDate As Date = Date.MinValue
            'Dim accEndDate As Date = Date.MinValue
            'For Each item As AccountPeriodDetail In AccountPeriodDetails
            '    If item.ItemNo = 1 Then
            '        accStartDate = item.StartDate
            '        'Exit For
            '    End If
            'Next
            '//ประกาศตัวแปล webservice
            Dim objService As New RiceService.RiceService

            '//ประกาศ Object ของรายงาน ขพ.2 (Header)
            Dim objData As New RiceService.PlanBudgetHeader02
            Try
                '//ข้อมูลตั้งค่าของศูนย์
                Dim objSiteInfo As SiteSetting = GetSiteInfo()

                '//ใส่ค่าให้กับ object รายงาน ขพ.2 (ข้อมูลส่วน Header)
                objData.SiteNo = objSiteInfo.SiteNo
                objData.SiteName = objSiteInfo.SiteName
                objData.SentDate = Date.Now
                objData.BudgetYear = BudgetYear
                objData.BudgetMonth = BudgetMonth
                objData.PlanName = PlanName.PlanName
                objData.ProjectName = ProjectName.ProjectName
                objData.ActivityName = ActivityName.ActivityName
                objData.SubActivityName = SubActivityName.SubActivityName
                objData.SentBy = CType(SecuritySystem.CurrentUser, User).DisplayName

                'Catch ex As Exception

                'End Try


                'BalanceDate, SentDate, [Year], SentBy
                'objData.SubmitDate = Date.Now

                '//ตัวแปลของ ขพ.2 (Detail) (รายชื่อ และที่อยู่ เกษตรกร) ประเภท List
                Dim listOfDetail As New List(Of RiceService.PlanBudgetDetail02)
                Dim PlanBudgetDetailMonth As New RiceService.PlanBudgetDetailMonth02
                Dim ResultBudget As New RiceService.ResultBudget02
                For Each item As PlanBudgetData02 In PlanBudgetData02s
                    Dim objDetail As New RiceService.PlanBudgetDetail02
                    objDetail.ProjectSubActivityName = item.ProjectSubActivityName
                    objDetail.PlanBudgetType = item.PlanBudgetType
                    objDetail.ExpenseID = item.ExpenseID
                    objDetail.ExpenseName = item.ExpenseName

                    Dim objMonthData02 As PlanBudgetMonthData02 = Session.FindObject(Of PlanBudgetMonthData02)(CriteriaOperator.Parse("PlanBudgetData02=?", item))
                    Dim objDetailMonth As New RiceService.PlanBudgetDetailMonth02
                    objDetailMonth.October = objMonthData02.October
                    objDetailMonth.November = objMonthData02.November
                    objDetailMonth.December = objMonthData02.December
                    objDetailMonth.January = objMonthData02.January
                    objDetailMonth.February = objMonthData02.February
                    objDetailMonth.March = objMonthData02.March
                    objDetailMonth.April = objMonthData02.April
                    objDetailMonth.May = objMonthData02.May
                    objDetailMonth.June = objMonthData02.June
                    objDetailMonth.July = objMonthData02.July
                    objDetailMonth.August = objMonthData02.August
                    objDetailMonth.September = objMonthData02.September

                    Dim objResultData02 As ResultBudgetData02 = Session.FindObject(Of ResultBudgetData02)(CriteriaOperator.Parse("PlanBudgetData02=?", item))
                    Dim objDetailResult As New RiceService.ResultBudget02
                    objDetailResult.October = objResultData02.October
                    objDetailResult.November = objResultData02.November
                    objDetailResult.December = objResultData02.December
                    objDetailResult.January = objResultData02.January
                    objDetailResult.February = objResultData02.February
                    objDetailResult.March = objResultData02.March
                    objDetailResult.April = objResultData02.April
                    objDetailResult.May = objResultData02.May
                    objDetailResult.June = objResultData02.June
                    objDetailResult.July = objResultData02.July
                    objDetailResult.August = objResultData02.August
                    objDetailResult.September = objResultData02.September

                    objDetail.PlanBudgetDetailMonth = objDetailMonth
                    objDetail.ResultBudget = objDetailResult

                    listOfDetail.Add(objDetail)
                    'listOfMonth.Add(objDetailMonth)
                    'listOfResult.Add(objDetailResult)
                Next

                '//ใส่รายชื่อเกษตรกร (Detail) ในส่วนของ Header
                '//ต้องแปลง List ให้เป็น Array เพราะส่งข้อมูลผ่านเว็บเซอร์วิส ต้องเป็น Array 
                objData.PlanBudgetDetails = listOfDetail.ToArray
                'objData.PlanBudgetDetails = listOfMonth.ToArray
                'objData.PlanBudgetDetails = listOfResult.ToArray


                '//ส่งข้อมูลผ่าน Webservice
                Dim ret As String = objService.SubmitDataPlanBudget02("NTiSecureCode", objData)

                '//เป็นสถานะเป็น ส่งข้อมูลแล้ว (Sent)
                If ret = "Success" Then
                    Me.Status = PublicEnum.SubmitReportStatus.Sent
                    Me.SubmitDate = Date.Now
                    Me.SubmitBy = CType(SecuritySystem.CurrentUser, User).DisplayName
                    '//บันทึกข้อมูล
                    MsgBox("ส่งข้อมูลรายงาน เรียบร้อยแล้ว", MsgBoxStyle.Information)
                    Me.Save()
                Else
                    MsgBox("เกิดข้อผิดพลาด ไม่สามารถส่งรายงานได้ กรุณาติดต่อผู้ดูแลระบบ", MsgBoxStyle.Critical)
                End If

            Catch ex As Exception
                MsgBox("เกิดข้อผิดพลาด ไม่สามารถส่งรายงานได้ กรุณาติดต่อผู้ดูแลระบบ", MsgBoxStyle.Critical)
            End Try

        End If
    End Sub

    <Action(Caption:="ดึงข้อมูล", ConfirmationMessage:="ท่านต้องการดึงข้อมูล ใช่หรือไม่?", ImageName:="BO_Note", AutoCommit:=True, TargetObjectsCriteria:="Status='Draft'")> _
    Public Sub ImportData()
        Session.Delete(PlanBudgetData02s)
        Dim _PlanBudgetData02s = New XPCollection(Of PlanBudgetDetail02)(Session, CriteriaOperator.Parse("PlanBudget02.Year=? AND PlanBudget02.PlanName=? AND PlanBudget02.ProjectName=? AND PlanBudget02.ActivityName=? AND PlanBudget02.SubActivityName=?", BudgetYear, PlanName, ProjectName, ActivityName, SubActivityName))
        If _PlanBudgetData02s.Count > 0 Then
            For Each TblEventPlanBudgetDetail As PlanBudgetDetail02 In _PlanBudgetData02s
                Dim InsertPlanBudgetData02 As New PlanBudgetData02(Session)
                With InsertPlanBudgetData02
                    .SubmitPlanBudget02Report = Me
                    .ProjectSubActivityName = TblEventPlanBudgetDetail.ProjectSubActivityName
                    .PlanBudgetType = TblEventPlanBudgetDetail.PlanBudgetType
                    .ExpenseID = TblEventPlanBudgetDetail.ExpenseID
                    .ExpenseName = TblEventPlanBudgetDetail.ExpenseName
                    'Add data to PlanBudgetMonthData01
                    Dim TempPlanBudgetDetailMonth02 As PlanBudgetDetailMonth02 = Session.FindObject(Of PlanBudgetDetailMonth02)(CriteriaOperator.Parse("PlanBudget02=?", TblEventPlanBudgetDetail))
                    If TempPlanBudgetDetailMonth02 IsNot Nothing Then
                        Dim TempPlanBudgetMonthData02 As PlanBudgetMonthData02 = New PlanBudgetMonthData02(Session)
                        With TempPlanBudgetMonthData02
                            .PlanBudgetData02 = InsertPlanBudgetData02
                            .October = TempPlanBudgetDetailMonth02.October
                            .November = TempPlanBudgetDetailMonth02.November
                            .December = TempPlanBudgetDetailMonth02.December
                            .January = TempPlanBudgetDetailMonth02.January
                            .February = TempPlanBudgetDetailMonth02.February
                            .March = TempPlanBudgetDetailMonth02.March
                            .April = TempPlanBudgetDetailMonth02.April
                            .May = TempPlanBudgetDetailMonth02.May
                            .June = TempPlanBudgetDetailMonth02.June
                            .July = TempPlanBudgetDetailMonth02.July
                            .August = TempPlanBudgetDetailMonth02.August
                            .September = TempPlanBudgetDetailMonth02.September
                            .Save()
                        End With
                    End If
                    'Add data to ResultBudgetData01
                    Dim TempResultBudget02 As ResultBudget02 = Session.FindObject(Of ResultBudget02)(CriteriaOperator.Parse("PlanBudget02=?", TblEventPlanBudgetDetail))
                    If TempResultBudget02 IsNot Nothing Then
                        Dim TempResultBudgetData02 As ResultBudgetData02 = New ResultBudgetData02(Session)
                        With TempResultBudgetData02
                            .PlanBudgetData02 = InsertPlanBudgetData02
                            .October = TempResultBudget02.October
                            .November = TempResultBudget02.November
                            .December = TempResultBudget02.December
                            .January = TempResultBudget02.January
                            .February = TempResultBudget02.February
                            .March = TempResultBudget02.March
                            .April = TempResultBudget02.April
                            .May = TempResultBudget02.May
                            .June = TempResultBudget02.June
                            .July = TempResultBudget02.July
                            .August = TempResultBudget02.August
                            .September = TempResultBudget02.September
                            .Save()
                        End With
                    End If

                    .Save()
                End With
            Next
        End If
        OnChanged("PlanBudgetData02s")
        'Else
        '    OnChanged("SubmitCheckFarmReportDetails")
        'End If
        'OnChanged("SubmitCheckFarmReportDetails")

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
