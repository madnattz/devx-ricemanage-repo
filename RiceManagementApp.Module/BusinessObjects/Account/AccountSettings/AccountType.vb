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
<XafDisplayName("ข้อมูลประเภทรายการบัญชี")> _
<DefaultClassOptions()> _
Public Class AccountType ' Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        ' Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).
    End Sub

    Private _AccountTypeID As String
    <XafDisplayName("รหัสประเภทรายการบัญชี")> _
    <Index(0), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    Public Property AccountTypeID() As String
        Get
            Return _AccountTypeID
        End Get
        Set(ByVal value As String)
            SetPropertyValue("AccountTypeID", _AccountTypeID, value)
        End Set
    End Property

    Private _AccountTypeName As String
    <XafDisplayName("ชื่อประเภทรายการบัญชี")> _
    <Index(1), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    Public Property AccountTypeName() As String
        Get
            Return _AccountTypeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue("AccountTypeName", _AccountTypeName, value)
        End Set
    End Property

End Class
