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
<XafDisplayName("ส่งรายงาน สงป.301")> _
<ConditionalAppearance.Appearance("SubmitPlanBudget01ReportDisableAllItems", criteria:="Status!='Draft'", Enabled:=False, TargetItems:="*", Context:="DetailView")> _
<DefaultClassOptions()> _
Public Class SubmitPlanBudget01Report ' Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
    Implements ISubmitReportAble

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        Me.Status = PublicEnum.SubmitReportStatus.Draft
        MyBase.AfterConstruction()
        ' Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).
    End Sub

    '<Appearance("DisYear", Enabled:=False, Criteria:="[PlaningStatus] <> 'status0'", Context:="DetailView")> _
    Private _PlanYear As String
    <XafDisplayName("ปีงบประมาณ")> _
<RuleRequiredField(DefaultContexts.Save)> _
<Index(0)> _
    Public Property PlanYear() As String
        Get
            Return _PlanYear
        End Get
        Set(ByVal value As String)
            SetPropertyValue("PlanYear", _PlanYear, value)
        End Set
    End Property

    Private _PlanMonth As PublicEnum.EnumMonth
    <XafDisplayName("ประจำเดือน")> _
<RuleRequiredField(DefaultContexts.Save)> _
<Index(0)> _
    Public Property PlanMonth() As PublicEnum.EnumMonth
        Get
            Return _PlanMonth
        End Get
        Set(ByVal value As PublicEnum.EnumMonth)
            SetPropertyValue("PlanMonth", _PlanMonth, value)
        End Set
    End Property

    '<Appearance("DISPlan", Enabled:=False, Criteria:="[PlaningStatus] <> 'status0'", Context:="DetailView")> _
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

    Dim fSubmitDate As Date = Today
    <Appearance("SubmitDateAcc", Enabled:=False, Context:="DetailView")> _
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

    Private _Note As String
    <XafDisplayName("ระบุคำอธิบายผลการดำเนินงานในแต่ละเดือน (ถ้ามี)")> _
    <VisibleInListView(False), VisibleInDetailView(True)> _
    Public Property Note() As String
        Get
            Return _Note
        End Get
        Set(ByVal value As String)
            SetPropertyValue("Note", _Note, value)
        End Set
    End Property


    Private _Problem As String
    <XafDisplayName("ปัญหา/อุปสรรค/แนวทางแก้ไข : (ถ้ามีให้ระบุด้วย)")> _
    <VisibleInListView(False), VisibleInDetailView(True)>
    Public Property Problem() As String
        Get
            Return _Problem
        End Get
        Set(ByVal value As String)
            SetPropertyValue("Problem", _Problem, value)
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

    <Association("SubmitPlanBudget01Report-PlanBudgetData01s", GetType(PlanBudgetData01))> _
<DevExpress.Xpo.Aggregated> _
<XafDisplayName("ข้อมูลรายการกิจกรรม")> _
    Public ReadOnly Property PlanBudgetData01s() As XPCollection(Of PlanBudgetData01)
        Get
            Return GetCollection(Of PlanBudgetData01)("PlanBudgetData01s")
        End Get
    End Property

    '<Action(Caption:="ส่งรายงาน", ConfirmationMessage:="ท่านต้องการส่งข้อมูลรายงานนี้ ใช่หรือไม่?", ImageName:="Action_Grant", AutoCommit:=True, TargetObjectsCriteria:="Status='Draft'")> _
    Public Function MarkAsComplete() As Boolean
        If Not IsDeleted Then
            If Not PlanBudgetData01s.Count > 0 Then
                MsgBox("ไม่พบรายการสงป.301 กรุณาตรวจสอบข้อมูลอีกครั้ง", MsgBoxStyle.Information, "")
                Exit Function
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
            Dim objData As New RiceService.PlanBudgetHeader01
            Try
                '//ข้อมูลตั้งค่าของศูนย์
                Dim objSiteInfo As SiteSetting = GetSiteInfo()

                If objService.CheckCanSubmit(objSiteInfo.SiteNo, "รายงาน สงป.", PlanMonth, PlanYear, 1) = False Then
                    MsgBox("ขณะนี้ได้มีการปิดการรับส่งข้อมูล")
                    Exit Function
                End If

                '//ใส่ค่าให้กับ object รายงาน ขพ.2 (ข้อมูลส่วน Header)
                objData.SiteNo = objSiteInfo.SiteNo
                objData.SiteName = objSiteInfo.SiteName
                objData.SentDate = Date.Now
                objData.PlanYear = PlanYear
                objData.PlanMonth = PlanMonth
                objData.PlanName = PlanName.PlanName
                objData.ProjectName = ProjectName.ProjectName
                objData.ActivityName = ActivityName.ActivityName
                objData.Note = Note
                objData.Problem = Problem
                objData.SentBy = CType(SecuritySystem.CurrentUser, User).DisplayName

                'Catch ex As Exception

                'End Try


                'BalanceDate, SentDate, [Year], SentBy
                'objData.SubmitDate = Date.Now

                '//ตัวแปลของ ขพ.2 (Detail) (รายชื่อ และที่อยู่ เกษตรกร) ประเภท List
                Dim listOfDetail As New List(Of RiceService.PlanBudgetDetail01)
                Dim PlanBudgetDetailMonth As New RiceService.PlanBudgetDetailMonth01
                Dim ResultBudget As New RiceService.ResultBudget01
                For Each item As PlanBudgetData01 In PlanBudgetData01s
                    Dim objDetail As New RiceService.PlanBudgetDetail01
                    objDetail.ActivityName = item.ActivityName
                    objDetail.Classifier = item.Classifier

                    Dim objMonthData01 As PlanBudgetMonthData01 = Session.FindObject(Of PlanBudgetMonthData01)(CriteriaOperator.Parse("PlanBudgetData01=?", item))
                    Dim objDetailMonth As New RiceService.PlanBudgetDetailMonth01
                    objDetailMonth.October = objMonthData01.October
                    objDetailMonth.November = objMonthData01.November
                    objDetailMonth.December = objMonthData01.December
                    objDetailMonth.January = objMonthData01.January
                    objDetailMonth.February = objMonthData01.February
                    objDetailMonth.March = objMonthData01.March
                    objDetailMonth.April = objMonthData01.April
                    objDetailMonth.May = objMonthData01.May
                    objDetailMonth.June = objMonthData01.June
                    objDetailMonth.July = objMonthData01.July
                    objDetailMonth.August = objMonthData01.August
                    objDetailMonth.September = objMonthData01.September

                    Dim objResultData01 As ResultBudgetData01 = Session.FindObject(Of ResultBudgetData01)(CriteriaOperator.Parse("PlanBudgetData01=?", item))
                    Dim objDetailResult As New RiceService.ResultBudget01
                    objDetailResult.October = objResultData01.October
                    objDetailResult.November = objResultData01.November
                    objDetailResult.December = objResultData01.December
                    objDetailResult.January = objResultData01.January
                    objDetailResult.February = objResultData01.February
                    objDetailResult.March = objResultData01.March
                    objDetailResult.April = objResultData01.April
                    objDetailResult.May = objResultData01.May
                    objDetailResult.June = objResultData01.June
                    objDetailResult.July = objResultData01.July
                    objDetailResult.August = objResultData01.August
                    objDetailResult.September = objResultData01.September

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
                Dim ret As String = objService.SubmitDataPlanBudget("NTiSecureCode", objData)

                '//เป็นสถานะเป็น ส่งข้อมูลแล้ว (Sent)
                If ret = "Success" Then
                    Me.Status = PublicEnum.SubmitReportStatus.Sent
                    Me.SubmitDate = Date.Now
                    Me.SubmitBy = CType(SecuritySystem.CurrentUser, User).DisplayName
                    '//บันทึกข้อมูล
                    Me.Save()
                    Return True
                Else
                    Return False
                End If

            Catch ex As Exception
                Return False
            End Try

        End If
    End Function

    '<Action(Caption:="ดึงข้อมูล", ConfirmationMessage:="ท่านต้องการดึงข้อมูล ใช่หรือไม่?", ImageName:="BO_Note", AutoCommit:=True, TargetObjectsCriteria:="Status='Draft'")> _
    Public Sub ImportData()
        Session.Delete(PlanBudgetData01s)
        Dim _PlanBudgetData01s = New XPCollection(Of PlanBudgetDetail01)(Session, CriteriaOperator.Parse("PlanBudget01.Year=?  and PlanBudget01.PlanName=? and PlanBudget01.ProjectName=? and PlanBudget01.ActivityName=?", PlanYear, PlanName, ProjectName, ActivityName))
        If _PlanBudgetData01s.Count > 0 Then
            For Each TblEventPlanBudgetDetail As PlanBudgetDetail01 In _PlanBudgetData01s
                Dim InsertPlanBudgetData01 As New PlanBudgetData01(Session)
                With InsertPlanBudgetData01
                    .SubmitPlanBudget01Report = Me
                    .ActivityName = TblEventPlanBudgetDetail.ActivityName
                    .Classifier = TblEventPlanBudgetDetail.Classifier
                    'Add data to PlanBudgetMonthData01
                    Dim TempPlanBudgetDetailMonth01 As PlanBudgetDetailMonth01 = Session.FindObject(Of PlanBudgetDetailMonth01)(CriteriaOperator.Parse("PlanBudget01=?", TblEventPlanBudgetDetail))
                    If TempPlanBudgetDetailMonth01 IsNot Nothing Then
                        Dim TempPlanBudgetMonthData01 As PlanBudgetMonthData01 = New PlanBudgetMonthData01(Session)
                        With TempPlanBudgetMonthData01
                            .PlanBudgetData01 = InsertPlanBudgetData01
                            .October = TempPlanBudgetDetailMonth01.October
                            .November = TempPlanBudgetDetailMonth01.November
                            .December = TempPlanBudgetDetailMonth01.December
                            .January = TempPlanBudgetDetailMonth01.January
                            .February = TempPlanBudgetDetailMonth01.February
                            .March = TempPlanBudgetDetailMonth01.March
                            .April = TempPlanBudgetDetailMonth01.April
                            .May = TempPlanBudgetDetailMonth01.May
                            .June = TempPlanBudgetDetailMonth01.June
                            .July = TempPlanBudgetDetailMonth01.July
                            .August = TempPlanBudgetDetailMonth01.August
                            .September = TempPlanBudgetDetailMonth01.September
                            .Save()
                        End With
                    End If
                    'Add data to ResultBudgetData01
                    Dim TempResultBudget01 As ResultBudget01 = Session.FindObject(Of ResultBudget01)(CriteriaOperator.Parse("PlanBudget01=?", TblEventPlanBudgetDetail))
                    If TempResultBudget01 IsNot Nothing Then
                        Dim TempResultBudgetData01 As ResultBudgetData01 = New ResultBudgetData01(Session)
                        With TempResultBudgetData01
                            .PlanBudgetData01 = InsertPlanBudgetData01
                            .October = TempResultBudget01.October
                            .November = TempResultBudget01.November
                            .December = TempResultBudget01.December
                            .January = TempResultBudget01.January
                            .February = TempResultBudget01.February
                            .March = TempResultBudget01.March
                            .April = TempResultBudget01.April
                            .May = TempResultBudget01.May
                            .June = TempResultBudget01.June
                            .July = TempResultBudget01.July
                            .August = TempResultBudget01.August
                            .September = TempResultBudget01.September
                            .Save()
                        End With
                    End If

                    .Save()
                End With
            Next
            Dim TempPlanBudget01 As PlanBudget01 = Session.FindObject(Of PlanBudget01)(CriteriaOperator.Parse("Year=? and  PlanName=? and ProjectName=? and ActivityName=?", PlanYear, PlanName, ProjectName, ActivityName))
            If TempPlanBudget01 IsNot Nothing Then
                Note = TempPlanBudget01.Note
                Problem = TempPlanBudget01.Problem
            End If
        End If
        OnChanged("PlanBudgetData01s")
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

    Public Sub DoLoadData() Implements ISubmitReportAble.DoLoadData
        ImportData()
    End Sub

    Public Function DoSubmitReport() As Boolean Implements ISubmitReportAble.DoSubmitReport
        MarkAsComplete()
    End Function
End Class
