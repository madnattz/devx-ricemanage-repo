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
<XafDisplayName("ข้อมูลรหัสสมุดรายวัน")> _
<DefaultClassOptions()> _
Public Class AccountBookID ' Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        ' Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).
    End Sub

    Private _AccountBookNo As String
    <XafDisplayName("รหัสสมุดรายวัน")> _
    <Index(0), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    Public Property AccountBookNo() As String
        Get
            Return _AccountBookNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue("AccountBookNo", _AccountBookNo, value)
        End Set
    End Property

    Private _AccountBookName As String
    <XafDisplayName("ชื่อรหัสสมุดรายวัน")> _
    <Index(1), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    Public Property AccountBookName() As String
        Get
            Return _AccountBookName
        End Get
        Set(ByVal value As String)
            SetPropertyValue("AccountBookName", _AccountBookName, value)
        End Set
    End Property

    Private _AccountBookNameVGA As String
    <XafDisplayName("ชื่อย่อรหัสสมุดรายวัน")> _
    <Index(2), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    Public Property AccountBookNameVGA() As String
        Get
            Return _AccountBookNameVGA
        End Get
        Set(ByVal value As String)
            SetPropertyValue("AccountBookNameVGA", _AccountBookNameVGA, value)
        End Set
    End Property

    '<Action(Caption:="My UI Action", ConfirmationMessage:="Are you sure?", ImageName:="Attention", AutoCommit:=True)> _
    'Public Sub ActionMethod()
    '    ' Trigger a custom business logic for the current record in the UI (http://documentation.devexpress.com/#Xaf/CustomDocument2619).
    '    Me.PersistentProperty = "Paid"
    'End Sub
End Class
