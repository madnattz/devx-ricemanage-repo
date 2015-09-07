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
Imports DevExpress.ExpressApp.ConditionalAppearance

'<ImageName("BO_Contact")> _
'<DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")> _
'<DefaultListViewOptions(MasterDetailMode.ListViewOnly, False, NewItemRowPosition.None)> _
'<Persistent("DatabaseTableName")> _
'<Appearance("FontColorRedAccount", AppearanceItemType:="ViewItem", TargetItems:="*", Context:="ListView", Criteria:="LevelItem < 3", FontColor:="Red")> _
<NavigationItem("กำหนดผังบัญชี")> _
<XafDisplayName("ข้อมูลรหัสบัญชี")> _
<DefaultProperty("AccountID")> _
<DefaultClassOptions()> _
Public Class Account ' Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
    Implements ITreeNode

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        ' Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).
    End Sub
    Protected Overrides Sub OnSaved()
        MyBase.OnSaved()
    End Sub

    Private _AccountID As String
    <XafDisplayName("รหัสบัญชี")> _
    <Size(15)> _
    <Index(1), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    Public Property AccountID() As String
        Get
            Return _AccountID
        End Get
        Set(ByVal value As String)
            SetPropertyValue("AccountID", _AccountID, value)
        End Set
    End Property

    Private _AccountName As String
    <XafDisplayName("ชื่อบัญชี")> _
    <Index(2), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    Public Property AccountName() As String
        Get
            Return _AccountName
        End Get
        Set(ByVal value As String)
            SetPropertyValue("AccountName", _AccountName, value)
        End Set
    End Property

    <VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)> _
    Public ReadOnly Property AccountFullName As String
        Get
            Try
                Return AccountID & " : " & AccountName
            Catch ex As Exception
                Return ""
            End Try
        End Get
    End Property


    ' AccountGroup
    Private _AccountGroup As AccountGroup
    <XafDisplayName("หมวดบัญชี")> _
    <Index(2), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    Public Property AccountGroup() As AccountGroup
        Get
            Return _AccountGroup
        End Get
        Set(ByVal value As AccountGroup)
            SetPropertyValue("AccountGroup", _AccountGroup, value)
        End Set
    End Property

    Private _AccountType As AccountType
    <XafDisplayName("ประเภทรายการบัญชี")> _
    <Index(3), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    Public Property AccountType() As AccountType
        Get
            Return _AccountType
        End Get
        Set(ByVal value As AccountType)
            SetPropertyValue("AccountType", _AccountType, value)
        End Set
    End Property

    'Private _AccountParent As Account
    '<XafDisplayName("เลขที่บัญชีคุม")> _
    '<Index(4), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    'Public Property AccountParent() As Account
    '    Get
    '        Return _AccountParent
    '    End Get
    '    Set(ByVal value As Account)
    '        SetPropertyValue("AccountParent", _AccountParent, value)
    '    End Set
    'End Property

    'Private _LvType As Integer
    '<XafDisplayName("ระดับLevel")> _
    '<Index(4), VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)> _
    'Public Property LvType() As Integer
    '    Get
    '        Return _LvType
    '    End Get
    '    Set(ByVal value As Integer)
    '        SetPropertyValue("LvType", _LvType, value)
    '    End Set
    'End Property

    <VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)> _
    Public ReadOnly Property LevelItem() As Integer
        Get
            Try
                Dim SQL As String = "  with Emp as (select oid,AccountID, Accountname,1 as EmpLevel from Account as Parent where Parent.ParentAccount is null " & _
"  union all " & _
"  select Child.oid, Child.AccountID , Child.AccountName ,EL.EmpLevel+1  from Account as Child  " & _
"  inner join emp as EL " & _
"  on Child.ParentAccount  =el.oid " & _
"  where Child.ParentAccount is not null) " & _
"  select EmpLevel from EMP where AccountID  ='" & AccountID & "' "

                Return Session.ExecuteScalar(SQL)

            Catch ex As Exception

            End Try

        End Get
    End Property

    Private _Remark As String
    <XafDisplayName("หมายเหตุ")> _
    <Index(5), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    Public Property Remark() As String
        Get
            Return _Remark
        End Get
        Set(ByVal value As String)
            SetPropertyValue("Remark", _Remark, value)
        End Set
    End Property

    Private _PublicStatus As PublicStatus
    <XafDisplayName("สถานะ")> _
    <Index(6), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    Public Property PublicStatus() As PublicStatus
        Get
            Return _PublicStatus
        End Get
        Set(ByVal value As PublicStatus)
            SetPropertyValue("PublicStatus", _PublicStatus, value)
        End Set
    End Property

    Private _LocalStatus As PublicStatus
    <XafDisplayName("สำหรับ สมข.")> _
    <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property LocalStatus() As PublicStatus
        Get
            Return _LocalStatus
        End Get
        Set(ByVal value As PublicStatus)
            SetPropertyValue("LocalStatus", _LocalStatus, value)
        End Set
    End Property

    Private _parentAccount As Account
    <Browsable(False), Association("Account-Account")> _
    Public Property ParentAccount() As Account
        Get
            Return _parentAccount
        End Get
        Set(ByVal value As Account)
            SetPropertyValue(Of Account)("ParentAccount", _parentAccount, value)
        End Set
    End Property
    <Association("Account-Account"), DevExpress.Xpo.Aggregated()> _
    Public ReadOnly Property NestedAccounts() As XPCollection(Of Account)
        Get
            Return GetCollection(Of Account)("NestedAccounts")
        End Get
    End Property

#Region "ITreeNode Members"
    Private ReadOnly Property Children() As IBindingList Implements ITreeNode.Children
        Get
            Return NestedAccounts
        End Get
    End Property
    Private ReadOnly Property Name() As String Implements ITreeNode.Name
        Get
            Return AccountName
        End Get
    End Property
    Private ReadOnly Property Parent() As ITreeNode Implements ITreeNode.Parent
        Get
            Return ParentAccount
        End Get
    End Property
#End Region

    <Action(Caption:="เปิดการใช้งาน", ImageName:="Action_Workflow_Activate", AutoCommit:=True)> _
    Public Sub Activate()
        PublicStatus = [Module].PublicStatus.Activate
    End Sub

    <Action(Caption:="ปิดการใช้งาน", ImageName:="Action_Workflow_DeActivate", AutoCommit:=True)> _
    Public Sub DeActivate()
        PublicStatus = [Module].PublicStatus.DeActivate
    End Sub
End Class
Public Enum PublicStatus
    <XafDisplayName("เปิดใช้งาน"), ImageName("Action_Workflow_Activate")> _
    Activate = 0
    <XafDisplayName("ปิดใช้งาน"), ImageName("Action_Workflow_DeActivate")> _
    DeActivate = 1
End Enum

Public Enum AccountStatus
    <XafDisplayName("กำลังดำเนินการ"), ImageName("Action_Workflow_Activate")> _
    Activate = 0
    <XafDisplayName("ปิดบัญชี"), ImageName("Action_Workflow_DeActivate")> _
    DeActivate = 1
End Enum

