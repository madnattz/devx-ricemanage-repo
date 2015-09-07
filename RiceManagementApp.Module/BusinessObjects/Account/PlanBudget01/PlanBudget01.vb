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
Imports RiceManagementApp.Module.PublicEnum

'<ImageName("BO_Contact")> _
'<DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")> _
'<DefaultListViewOptions(MasterDetailMode.ListViewOnly, False, NewItemRowPosition.None)> _
'<Persistent("DatabaseTableName")> _
<NavigationItem("สงป. 301")> _
<DefaultClassOptions(), ImageName("BO_Task"), XafDisplayName("งบประมาณสงป. 301")> _
Public Class PlanBudget01 ' Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        DataOwner = GetCurrentSite()
        MyBase.AfterConstruction()
        ' Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).
    End Sub

    Protected Overrides Sub OnSaving()
        MyBase.OnSaving()
        If Me.DataOwner Is Nothing Then
            DataOwner = GetCurrentSite()
        End If
    End Sub

    Dim fDataOwner As Site
    <Browsable(False)> _
    Public Property DataOwner() As Site
        Get
            Return fDataOwner
        End Get
        Set(ByVal value As Site)
            SetPropertyValue(Of Site)("DataOwner", fDataOwner, value)
        End Set
    End Property

    Public Function GetCurrentSite() As Site
        Dim siteSetting As SiteSetting = Session.FindObject(Of SiteSetting)(Nothing)
        If siteSetting IsNot Nothing Then
            If siteSetting.Site IsNot Nothing Then
                Return siteSetting.Site
            Else
                Return Nothing
            End If
            Return siteSetting.Site
        Else
            Return Nothing
        End If
    End Function

    '<Appearance("DisYear", Enabled:=False, Criteria:="[PlaningStatus] <> 'status0'", Context:="DetailView")> _
    Private _Year As String
    <XafDisplayName("ปีงบประมาณ")> _
    <RuleRequiredField(DefaultContexts.Save)> _
    <Index(0)> _
    Public Property Year() As String
        Get
            Return _Year
        End Get
        Set(ByVal value As String)
            SetPropertyValue("Year", _Year, value)
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

    Private _Note As String
    <XafDisplayName("ระบุคำอธิบายผลการดำเนินงานในแต่ละเดือน (ถ้ามี)")> _
    <VisibleInListView(False)> _
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
    <VisibleInListView(False)>
    Public Property Problem() As String
        Get
            Return _Problem
        End Get
        Set(ByVal value As String)
            SetPropertyValue("Problem", _Problem, value)
        End Set
    End Property


#Region "==========Association============"
    <Association("PlanBudget01-PlanBudgetDetail01s", GetType(PlanBudgetDetail01))> _
    <XafDisplayName("รายละเอียดกิจกรรมและแผนดำเนินงาน")> _
    Public ReadOnly Property PlanBudgetDetail01s() As XPCollection(Of PlanBudgetDetail01)
        Get
            Return GetCollection(Of PlanBudgetDetail01)("PlanBudgetDetail01s")
        End Get
    End Property
#End Region

    Private _PlaningStatus As StausAppove
    <XafDisplayName("สถานะ")> _
    <Index(10)> _
    <RuleRequiredField(DefaultContexts.Save), VisibleInDetailView(False), VisibleInListView(False)> _
    Public Property PlaningStatus() As StausAppove
        Get
            Return _PlaningStatus
        End Get
        Set(ByVal value As StausAppove)
            SetPropertyValue("PlaningStatus", _PlaningStatus, value)
        End Set
    End Property

    <Action(Caption:="แสดงรายการกิจกรรม", ConfirmationMessage:="ท่านต้องการแสดงรายการกิจกรรมใช่หรือไม่?", ImageName:="BO_List", AutoCommit:=True)> _
    Public Sub LoadSiteNameList()
        If PlanName Is Nothing Then
            MsgBox("กรุณาเลือก ชื่อแผน")
        ElseIf ProjectName Is Nothing Then
            MsgBox("กรุณาเลือก ชื่อโครงการ")
        ElseIf ActivityName Is Nothing Then
            MsgBox("กรุณาเลือก ชื่อกิจกรรมหลัก")
            'ElseIf PlanBudget = 0 Then
            '    MsgBox("กรุณากำหนด ยอดงบประมาณสะสมตามที่ได้รับจัดสรร")
        Else
            If PlaningStatus = StausAppove.status0 Then
                Dim ACTinfos As New XPCollection(Of SettingSubActivity)(Session, CriteriaOperator.Parse("Activity==?", ActivityName))
                For Each row As SettingSubActivity In ACTinfos

                    Dim Target As PlanBudgetDetail01 = New PlanBudgetDetail01(Session)
                    Target.ActivityNameNo = row.SubActivityNo
                    Target.ActivityName = row.SubActivityName
                    Target.PlanBudget01 = Me
                    Target.Save()
                    Dim BudgetName = New PlanBudgetDetail01(Session)
                    BudgetName.ActivityNameNo = row.SubActivityNo
                    BudgetName.ActivityName = "งบประมาณจัดสรร" + row.SubActivityName
                    'BudgetName.PlanTotal = Me.PlanBudget
                    BudgetName.Classifier = classifier.Bath
                    BudgetName.PlanBudget01 = Me
                    PlaningStatus = StausAppove.Status1
                Next
            End If
        End If
    End Sub
End Class
Public Enum StausAppove
    status0 = 0
    Status1 = 1 'ไม่สามารถแก้ไขชื่อกิจกรรม
    Status2 = 2 'ไม่สามาระแก้ไข ชื่อกิจกรรม , แผน(ส่วนกลาง) , หน่วย
    status3 = 3
    status4 = 4
End Enum
