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

'<ImageName("BO_Contact")> _
'<DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")> _
'<DefaultListViewOptions(MasterDetailMode.ListViewOnly, False, NewItemRowPosition.None)> _
'<Persistent("DatabaseTableName")> _
<DefaultClassOptions()> _
Public Class SettingActivity ' Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        ' Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).
    End Sub

    Private _Project As SettingProject
    <Association("SettingProject-SettingActivitys")> _
    <Index(0), VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)> _
    <ImmediatePostData()> _
    Public Property Project() As SettingProject
        Get
            Return _Project
        End Get
        Set(ByVal value As SettingProject)
            SetPropertyValue("Project", _Project, value)
        End Set
    End Property

    Private _ActivityNo As String
    <XafDisplayName("รายการที่")> _
    <Index(0), VisibleInListView(True)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property ActivityNo() As String
        Get
            Return _ActivityNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue("ActivityNo", _ActivityNo, value)
        End Set
    End Property

    Private _ActivityName As String
    <XafDisplayName("กิจรรมหลัก")> _
    <Index(0), VisibleInListView(True)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    <Size(200)> _
    Public Property ActivityName() As String
        Get
            Return _ActivityName
        End Get
        Set(ByVal value As String)
            SetPropertyValue("ActivityName", _ActivityName, value)
        End Set
    End Property

    Private _CodeSubActivities As String
    <XafDisplayName("รหัสกิจกรรมหลัก")> _
    <Index(1), VisibleInListView(True)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property CodeSubActivities() As String
        Get
            Return _CodeSubActivities
        End Get
        Set(ByVal value As String)
            SetPropertyValue("CodeSubActivities", _CodeSubActivities, value)
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

    <XafDisplayName("กิจกรรมย่อย")> _
<Association("SettingActivity-SettingSubActivitys", GetType(SettingSubActivity))> _
<DC.Aggregated()> _
    Public ReadOnly Property SettingSubActivitys() As XPCollection(Of SettingSubActivity)
        Get
            Return GetCollection(Of SettingSubActivity)("SettingSubActivitys")
        End Get
    End Property

    '<Action(Caption:="My UI Action", ConfirmationMessage:="Are you sure?", ImageName:="Attention", AutoCommit:=True)> _
    'Public Sub ActionMethod()
    '    ' Trigger a custom business logic for the current record in the UI (http://documentation.devexpress.com/#Xaf/CustomDocument2619).
    '    Me.PersistentProperty = "Paid"
    'End Sub
End Class
