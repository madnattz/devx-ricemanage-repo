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
Public Class SettingSubActivity ' Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        ' Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).
    End Sub

    Private _Activity As SettingActivity
    <Association("SettingActivity-SettingSubActivitys")> _
    <Index(0), VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)> _
    <ImmediatePostData()> _
    Public Property Activity() As SettingActivity
        Get
            Return _Activity
        End Get
        Set(ByVal value As SettingActivity)
            SetPropertyValue("Activity", _Activity, value)
        End Set
    End Property

    Private _SubActivityNo As String
    <XafDisplayName("รายการที่")> _
    <Index(0), VisibleInListView(True)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property SubActivityNo() As String
        Get
            Return _SubActivityNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue("SubActivityNo", _SubActivityNo, value)
        End Set
    End Property

    Private _SubActivityName As String
    <XafDisplayName("กิจรรมย่อย")> _
    <Index(0), VisibleInListView(True)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    <Size(200)> _
    Public Property SubActivityName() As String
        Get
            Return _SubActivityName
        End Get
        Set(ByVal value As String)
            SetPropertyValue("SubActivityName", _SubActivityName, value)
        End Set
    End Property

    Private _CodeSubActivities As String
    <XafDisplayName("รหัสกิจกรรมย่อย")> _
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

    '<Action(Caption:="My UI Action", ConfirmationMessage:="Are you sure?", ImageName:="Attention", AutoCommit:=True)> _
    'Public Sub ActionMethod()
    '    ' Trigger a custom business logic for the current record in the UI (http://documentation.devexpress.com/#Xaf/CustomDocument2619).
    '    Me.PersistentProperty = "Paid"
    'End Sub
End Class
