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
<DefaultClassOptions(), NavigationItem("ตั้งค่า"), XafDisplayName("ตั้งค่ารายจ่าย"), DefaultProperty("listID")> _
Public Class SettingExpenses ' Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
    Implements ITreeNode
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

    Private _listID As String
    <XafDisplayName("รหัส")> _
    <Index(0), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    Public Property listID() As String
        Get
            Return _listID
        End Get
        Set(ByVal value As String)
            SetPropertyValue("listID", _listID, value)
        End Set
    End Property

    Private _listExpenses As String
    <XafDisplayName("รายการ")> _
    <Index(1), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    <Size(200)> _
    Public Property listExpenses() As String
        Get
            Return _listExpenses
        End Get
        Set(ByVal value As String)
            SetPropertyValue("listExpenses", _listExpenses, value)
        End Set
    End Property

    Private _CodeGF_Oid As CodeGFPV
    <XafDisplayName("รหัสGF")> _
    <Index(2), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    Public Property CodeGF_Oid() As CodeGFPV
        Get
            Return _CodeGF_Oid
        End Get
        Set(ByVal value As CodeGFPV)
            SetPropertyValue("CodeGF_Oid", _CodeGF_Oid, value)
            If CodeGF_Oid IsNot Nothing Then
                CodeGF_ID = CodeGF_Oid.GFID
                CodeGF_Name = CodeGF_Oid.GFName
            End If
        End Set
    End Property

    Private _CodeGF_ID As String
    <XafDisplayName("รหัสGF")> _
    <Index(2), VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)> _
    Public Property CodeGF_ID() As String
        Get
            Return _CodeGF_ID
        End Get
        Set(ByVal value As String)
            SetPropertyValue("CodeGF_ID", _CodeGF_ID, value)
        End Set
    End Property

    Private _CodeGF_Name As String
    <XafDisplayName("หมวดGF")> _
    <Index(2), VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)> _
    Public Property CodeGF_Name() As String
        Get
            Return _CodeGF_Name
        End Get
        Set(ByVal value As String)
            SetPropertyValue("CodeGF_Name", _CodeGF_Name, value)
        End Set
    End Property

    Private _Status As PublicEnum.PublicStatus
    <XafDisplayName("สถานะ")> _
    <Index(4), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property Status() As PublicEnum.PublicStatus
        Get
            Return _Status
        End Get
        Set(ByVal value As PublicEnum.PublicStatus)
            SetPropertyValue("Status", _Status, value)
        End Set
    End Property

    '    <XafDisplayName("รายการ รายจ่าย")> _
    '<Association("SettingExpenses-SettingExpensesDetails", GetType(SettingExpensesDetail))> _
    '<DC.Aggregated()> _
    '    Public ReadOnly Property SettingExpensesDetails() As XPCollection(Of SettingExpensesDetail)
    '        Get
    '            Return GetCollection(Of SettingExpensesDetail)("SettingExpensesDetails")
    '        End Get
    '    End Property

    <VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)> _
    Public ReadOnly Property LevelItem() As Integer
        Get
            Try
                Dim SQL As String = "  with Emp as (select oid,listID, listExpenses,1 as EmpLevel from SettingExpenses as Parent where Parent.ParentSettingExpenses is null " & _
"  union all " & _
"  select Child.oid, Child.listID , Child.listExpenses ,EL.EmpLevel+1  from SettingExpenses as Child  " & _
"  inner join emp as EL " & _
"  on Child.ParentSettingExpenses  =el.oid " & _
"  where Child.ParentSettingExpenses is not null) " & _
"  select EmpLevel from EMP where listID  ='" & listID & "' "

                Return Session.ExecuteScalar(SQL)

            Catch ex As Exception

            End Try

        End Get
    End Property

    Private _parentSettingExpenses As SettingExpenses
    <Browsable(False), Association("SettingExpenses-SettingExpenses")> _
    Public Property parentSettingExpenses() As SettingExpenses
        Get
            Return _parentSettingExpenses
        End Get
        Set(ByVal value As SettingExpenses)
            SetPropertyValue(Of SettingExpenses)("parentSettingExpenses", _parentSettingExpenses, value)
        End Set
    End Property
    <Association("SettingExpenses-SettingExpenses"), DevExpress.Xpo.Aggregated()> _
    Public ReadOnly Property NestedSettingExpenses() As XPCollection(Of SettingExpenses)
        Get
            Return GetCollection(Of SettingExpenses)("NestedSettingExpenses")
        End Get
    End Property

#Region "ITreeNode Members"
    Private ReadOnly Property Children() As IBindingList Implements ITreeNode.Children
        Get
            Return NestedSettingExpenses
        End Get
    End Property
    Private ReadOnly Property Name() As String Implements ITreeNode.Name
        Get
            Return listExpenses
        End Get
    End Property
    Private ReadOnly Property Parent() As ITreeNode Implements ITreeNode.Parent
        Get
            Return parentSettingExpenses
        End Get
    End Property
#End Region

    '<Action(Caption:="My UI Action", ConfirmationMessage:="Are you sure?", ImageName:="Attention", AutoCommit:=True)> _
    'Public Sub ActionMethod()
    '    ' Trigger a custom business logic for the current record in the UI (http://documentation.devexpress.com/#Xaf/CustomDocument2619).
    'End Sub
End Class
