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
'<NavigationItem("รายการต้นทุนการผลิต")> _
'<DefaultClassOptions()> _
Public Class AccCost ' Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        ' Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).
    End Sub

    Private _SeedID As String
    <XafDisplayName("พันธุ์")> _
    <Index(2), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    Public Property SeedID() As String
        Get
            Return _SeedID
        End Get
        Set(ByVal value As String)
            SetPropertyValue("SeedID", _SeedID, value)
        End Set
    End Property

    Private _SeedTypeID As String
    <XafDisplayName("ชั้นพันธุ์")> _
    <Index(3), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    Public Property SeedTypeID() As String
        Get
            Return _SeedTypeID
        End Get
        Set(ByVal value As String)
            SetPropertyValue("SeedTypeID", _SeedTypeID, value)
        End Set
    End Property

    Private _No As String
    <XafDisplayName("รุ่นที่")> _
    <Index(4), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    Public Property No() As String
        Get
            Return _No
        End Get
        Set(ByVal value As String)
            SetPropertyValue("No", _No, value)
        End Set
    End Property

    Private _SeasonID As String
    <XafDisplayName("ฤดู")> _
    <Index(5), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    Public Property SeasonID() As String
        Get
            Return _SeasonID
        End Get
        Set(ByVal value As String)
            SetPropertyValue("SeasonID", _SeasonID, value)
        End Set
    End Property

    Private _YearID As String
    <XafDisplayName("ปี")> _
    <Index(6), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    Public Property YearID() As String
        Get
            Return _YearID
        End Get
        Set(ByVal value As String)
            SetPropertyValue("YearID", _YearID, value)
        End Set
    End Property

    Private _AmountBS As String
    <XafDisplayName("น้ำหนักเมล็ดพันธุ์ซื้อคืน (กก.)")> _
    <Index(6), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    Public Property AmountBS() As String
        Get
            Return _AmountBS
        End Get
        Set(ByVal value As String)
            SetPropertyValue("AmountBS", _AmountBS, value)
        End Set
    End Property

    Private _AmountSeedGood As String
    <XafDisplayName("น้ำหนักเมล็ดพันธุ์ดี (กก.)")> _
    <Index(6), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    Public Property AmountSeedGood() As String
        Get
            Return _AmountSeedGood
        End Get
        Set(ByVal value As String)
            SetPropertyValue("AmountSeedGood", _AmountSeedGood, value)
        End Set
    End Property

    Private _AmountSeedOut As String
    <XafDisplayName("น้ำหนักเมล็ดพันธุ์คัดออก (กก.)")> _
    <Index(6), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    Public Property AmountSeedOut() As String
        Get
            Return _AmountSeedOut
        End Get
        Set(ByVal value As String)
            SetPropertyValue("AmountSeedOut", _AmountSeedOut, value)
        End Set
    End Property

    Private _TotalAmountBS As String
    <XafDisplayName("วงเงินจัดซื้อเมล็ดพันธุ์ซื้อคืนทั้งสิ้น (บาท)")> _
    <Index(6), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    Public Property TotalAmountBS() As String
        Get
            Return _TotalAmountBS
        End Get
        Set(ByVal value As String)
            SetPropertyValue("AmountSeedOut", _TotalAmountBS, value)
        End Set
    End Property

    Private _Material As String
    <XafDisplayName("วัสดุที่ใช้ในการผลิต (บาท)")> _
    <Index(6), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    Public Property Material() As String
        Get
            Return _Material
        End Get
        Set(ByVal value As String)
            SetPropertyValue("Material", _Material, value)
        End Set
    End Property

    Private _Oil As String
    <XafDisplayName("ค่าน้ำมัน (บาท)")> _
    <Index(6), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    Public Property Oil() As String
        Get
            Return _Oil
        End Get
        Set(ByVal value As String)
            SetPropertyValue("Oil", _Oil, value)
        End Set
    End Property

    Private _TotalAmountCP As String
    <XafDisplayName("เคมีที่ใช้ในการผลิต (บาท)")> _
    <Index(6), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    Public Property TotalAmountCP() As String
        Get
            Return _TotalAmountCP
        End Get
        Set(ByVal value As String)
            SetPropertyValue("TotalAmountCP", _TotalAmountCP, value)
        End Set
    End Property

    '<Action(Caption:="My UI Action", ConfirmationMessage:="Are you sure?", ImageName:="Attention", AutoCommit:=True)> _
    'Public Sub ActionMethod()
    '    ' Trigger a custom business logic for the current record in the UI (http://documentation.devexpress.com/#Xaf/CustomDocument2619).
    '    Me.PersistentProperty = "Paid"
    'End Sub
End Class
