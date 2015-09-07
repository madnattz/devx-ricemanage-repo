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
Imports DevExpress.Persistent.Base.General

'<ImageName("BO_Contact")> _
'<DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")> _
'<DefaultListViewOptions(MasterDetailMode.ListViewOnly, False, NewItemRowPosition.None)> _
'<Persistent("DatabaseTableName")> _
<NavigationItem("สงป. 301")> _
<DefaultClassOptions(), XafDisplayName("ตั้งค่าแผนงบประมาณสงป. 301")> _
Public Class SettingPlan ' Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        ' Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).
    End Sub

    Private _PlanNo As String
    <XafDisplayName("รายการที่")> _
    <Index(0), VisibleInListView(True)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property PlanNo() As String
        Get
            Return _PlanNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue("PlanNo", _PlanNo, value)
        End Set
    End Property

    Private _PlanName As String
    <XafDisplayName("แผนงาน")> _
    <Index(0), VisibleInListView(True)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    <Size(200)> _
    Public Property PlanName() As String
        Get
            Return _PlanName
        End Get
        Set(ByVal value As String)
            SetPropertyValue("PlanName", _PlanName, value)
        End Set
    End Property

    Private _CodePlan As String
    <XafDisplayName("รหัสแผนงาน")> _
    <Index(1), VisibleInListView(True)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property CodePlan() As String
        Get
            Return _CodePlan
        End Get
        Set(ByVal value As String)
            SetPropertyValue("CodePlan", _CodePlan, value)
        End Set
    End Property

    Private _PublicStatus As PublicEnum.PublicStatus
    <XafDisplayName("สถานะ")> _
    <Index(2), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    Public Property PublicStatus() As PublicEnum.PublicStatus
        Get
            Return _PublicStatus
        End Get
        Set(ByVal value As PublicEnum.PublicStatus)
            SetPropertyValue("PublicStatus", _PublicStatus, value)
        End Set
    End Property

    <XafDisplayName("โครงการ")> _
<Association("SettingPlan-SettingProjects", GetType(SettingProject))> _
<DC.Aggregated()> _
    Public ReadOnly Property SettingProjects() As XPCollection(Of SettingProject)
        Get
            Return GetCollection(Of SettingProject)("SettingProjects")
        End Get
    End Property


    '<Action(Caption:="My UI Action", ConfirmationMessage:="Are you sure?", ImageName:="Attention", AutoCommit:=True)> _
    'Public Sub ActionMethod()
    '    ' Trigger a custom business logic for the current record in the UI (http://documentation.devexpress.com/#Xaf/CustomDocument2619).
    '    Me.PersistentProperty = "Paid"
    'End Sub
End Class
