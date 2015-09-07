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
<NavigationItem("ʧ�. 301")> _
<DefaultClassOptions(), ImageName("BO_Task"), XafDisplayName("������ҳʧ�. 301")> _
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
    <XafDisplayName("�է�����ҳ")> _
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
    <XafDisplayName("����Ἱ�ҹ")> _
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
    <XafDisplayName("�����ç���")> _
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
    <XafDisplayName("���͡Ԩ������ѡ")> _
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
    <XafDisplayName("�кؤ�͸Ժ�¼š�ô��Թ�ҹ�������͹ (�����)")> _
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
    <XafDisplayName("�ѭ��/�ػ��ä/�Ƿҧ��� : (���������кش���)")> _
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
    <XafDisplayName("��������´�Ԩ�������Ἱ���Թ�ҹ")> _
    Public ReadOnly Property PlanBudgetDetail01s() As XPCollection(Of PlanBudgetDetail01)
        Get
            Return GetCollection(Of PlanBudgetDetail01)("PlanBudgetDetail01s")
        End Get
    End Property
#End Region

    Private _PlaningStatus As StausAppove
    <XafDisplayName("ʶҹ�")> _
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

    <Action(Caption:="�ʴ���¡�áԨ����", ConfirmationMessage:="��ҹ��ͧ����ʴ���¡�áԨ�������������?", ImageName:="BO_List", AutoCommit:=True)> _
    Public Sub LoadSiteNameList()
        If PlanName Is Nothing Then
            MsgBox("��س����͡ ����Ἱ")
        ElseIf ProjectName Is Nothing Then
            MsgBox("��س����͡ �����ç���")
        ElseIf ActivityName Is Nothing Then
            MsgBox("��س����͡ ���͡Ԩ������ѡ")
            'ElseIf PlanBudget = 0 Then
            '    MsgBox("��سҡ�˹� �ʹ������ҳ�������������Ѻ�Ѵ���")
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
                    BudgetName.ActivityName = "������ҳ�Ѵ���" + row.SubActivityName
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
    Status1 = 1 '�������ö��䢪��͡Ԩ����
    Status2 = 2 '������������ ���͡Ԩ���� , Ἱ(��ǹ��ҧ) , ˹���
    status3 = 3
    status4 = 4
End Enum
