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
<XafDisplayName("ข้อมูลหมวดบัญชี")> _
<DefaultClassOptions()> _
Public Class AccountGroup ' Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        ' Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).
    End Sub

    Private _AccountGroupID As String
    <XafDisplayName("รหัสหมวดบัญชี")> _
    <Index(0), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    Public Property AccountGroupID() As String
        Get
            Return _AccountGroupID
        End Get
        Set(ByVal value As String)
            SetPropertyValue("AccountGroupID", _AccountGroupID, value)
        End Set
    End Property

    Private _AccountGroupName As String
    <XafDisplayName("ชื่อหมวดบัญชี")> _
    <Index(1), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    Public Property AccountGroupName() As String
        Get
            Return _AccountGroupName
        End Get
        Set(ByVal value As String)
            SetPropertyValue("AccountGroupName", _AccountGroupName, value)
        End Set
    End Property

    Private _Nature As NatureType
    '<XafDisplayName("ชื่อหมวดบัญชี")> _
    <Index(1), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    Public Property Nature() As NatureType
        Get
            Return _Nature
        End Get
        Set(ByVal value As NatureType)
            SetPropertyValue("Nature", _Nature, value)
        End Set
    End Property

    Public Enum NatureType
        Dr
        Cr
    End Enum

End Class
