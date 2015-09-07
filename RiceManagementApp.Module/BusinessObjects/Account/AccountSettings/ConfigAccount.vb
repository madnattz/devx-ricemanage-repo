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
<NavigationItem("กำหนดผังบัญชี")> _
<DefaultClassOptions()> _
Public Class ConfigAccount ' Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        ' Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).
    End Sub
    Private _AccountID As Account
    <XafDisplayName("รหัสหมวด")> _
    <Size(15)> _
    <Index(1), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    Public Property AccountID() As Account
        Get
            Return _AccountID
        End Get
        Set(ByVal value As Account)
            SetPropertyValue("AccountID", _AccountID, value)
            If AccountID IsNot Nothing Then
                AccountType = AccountID.AccountName
            End If
        End Set
    End Property

    Private _AccountType As String
    <XafDisplayName("หมวดบัญชี")> _
    <Index(3), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    Public Property AccountType() As String
        Get
            Return _AccountType
        End Get
        Set(ByVal value As String)
            SetPropertyValue("AccountType", _AccountType, value)
        End Set
    End Property

    Private _Type As Type
    <XafDisplayName("เพิ่ม")> _
    <Index(4), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    Public Property Type() As Type
        Get
            Return _Type
        End Get
        Set(ByVal value As Type)
            SetPropertyValue("Type", _Type, value)
        End Set
    End Property
End Class
Public Enum Type
    <XafDisplayName("None")> _
    None = 0
    <XafDisplayName("เดบิต")> _
    Debit = 1
    <XafDisplayName("เครดิต")> _
    Crebit = 2
End Enum
