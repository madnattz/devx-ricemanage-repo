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
'<DefaultClassOptions()> _
<DeferredDeletion(False)> _
<NavigationItem("สมุดรายวัน")> _
<XafDisplayName("งบทดลอง")> _
<VisibleInReports(True)> _
Public Class GL ' Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        ' Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).
        CreateDate = Now
    End Sub

    Private fAccountYear As String
    '<VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)> _
    <XafDisplayName("ปีบัญชี")> _
    <Size(4)> _
    Public Property AccountYear() As String
        Get
            Return fAccountYear
        End Get
        Set(value As String)
            SetPropertyValue("AccountYear", fAccountYear, value)
        End Set
    End Property

    Private fBookRefNo As String
    Public Property BookRefNo() As String
        Get
            Return fBookRefNo
        End Get
        Set(value As String)
            SetPropertyValue("BookRefNo", fBookRefNo, value)
        End Set
    End Property

    Private _DocuDate As DateTime
    <XafDisplayName("วันที่เอกสาร")> _
    <Index(0), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property DocuDate() As DateTime
        Get
            Return _DocuDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue("DocuDate", _DocuDate, value)
            If Not IsLoading AndAlso Not IsSaving Then
                Dim objPeriod As AccountPeriod = Session.FindObject(Of AccountPeriod)(CriteriaOperator.Parse(""))
            End If
        End Set
    End Property

    Private _CreateDate As Date
    <ModelDefault("DisplayFormat", "{0:dd/MM/yyyy hh:mm:ss:ms}"), ModelDefault("EditMask", "dd/MM/yyyy hh:mm:ss:ms")> _
    <Index(0), VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)> _
    Public Property CreateDate() As Date
        Get
            Return _CreateDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue("CreateDate", _CreateDate, value)
        End Set
    End Property


    Private _AccountBook As String
    <XafDisplayName("สมุดรายวัน")> _
    <Index(1), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    <ImmediatePostData()> _
    Public Property AccountBook() As String
        Get
            Return _AccountBook
        End Get
        Set(ByVal value As String)
            SetPropertyValue("AccountBook", _AccountBook, value)
        End Set
    End Property

    Private _DocuNo As String
    <XafDisplayName("เลขที่เอกสาร")> _
    <Index(1), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property DocuNo() As String
        Get
            Return _DocuNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue("DocuNo", _DocuNo, value)
        End Set
    End Property

    Private _IVRefNo As String
    <XafDisplayName("เลขที่ใบกำกับสินค้า")> _
    <Index(2), VisibleInListView(False), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property IVRefNo() As String
        Get
            Return _IVRefNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue("IVRefNo", _IVRefNo, value)
        End Set
    End Property

    Private _ReceiptNo As String
    <XafDisplayName("เลขที่ใบเสร็จรับเงิน")> _
    <Index(2), VisibleInListView(False), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property ReceiptNo() As String
        Get
            Return _ReceiptNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue("ReceiptNo", _ReceiptNo, value)
        End Set
    End Property

    Private _RefNo As String
    <XafDisplayName("เลขที่เอกสารอ้างอิง")> _
    <Index(3), VisibleInListView(False), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property RefNo() As String
        Get
            Return _RefNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue("RefNo", _RefNo, value)
        End Set
    End Property

    Private _RefDate As Date
    <XafDisplayName("วันที่เอกสารอ้างอิง")> _
    <Index(4), VisibleInListView(False), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property RefDate() As Date
        Get
            Return _RefDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue("RefDate", _RefDate, value)
        End Set
    End Property

    Private _RefAmnt As Double
    <XafDisplayName("จำนวนเงิน")> _
    <Index(5), VisibleInListView(False), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property RefAmnt() As Double
        Get
            Return _RefAmnt
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("RefAmnt", _RefAmnt, value)
        End Set
    End Property

    Private _ListNo As String
    <XafDisplayName("รายการที่")> _
    <Index(6), VisibleInListView(False), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property ListNo() As String
        Get
            Return _ListNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue("ListNo", _ListNo, value)
        End Set
    End Property

    Private _RVDesc1 As String
    <XafDisplayName("รายละเอียด")> _
    <Index(7), VisibleInListView(False), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property RVDesc1() As String
        Get
            Return _RVDesc1
        End Get
        Set(ByVal value As String)
            SetPropertyValue("RVDesc1", _RVDesc1, value)
        End Set
    End Property

    Private _ToptotalAmnt As Double
    <XafDisplayName("จำนวนรวม")> _
    <Index(8), VisibleInListView(False), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property ToptotalAmnt() As Double
        Get
            Return _ToptotalAmnt
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("ToptotalAmnt", _ToptotalAmnt, value)
        End Set
    End Property

    Private _AccID As String
    <XafDisplayName("รหัสบัญชี")> _
    <Index(9), VisibleInListView(False), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property AccID() As String
        Get
            Return _AccID
        End Get
        Set(ByVal value As String)
            SetPropertyValue("AccID", _AccID, value)
        End Set
    End Property

    Private _AccountName As String
    <XafDisplayName("ชื่อบัญชี")> _
    <Index(10), VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)> _
    Public Property AccountName() As String
        Get
            Return _AccountName
        End Get
        Set(ByVal value As String)
            SetPropertyValue("AccountName", _AccountName, value)
        End Set
    End Property

    Private _AccDetail As String
    <XafDisplayName("รายการ")> _
    <Index(10), VisibleInListView(False), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property AccDetail() As String
        Get
            Return _AccDetail
        End Get
        Set(ByVal value As String)
            SetPropertyValue("AccDetail", _AccDetail, value)
        End Set
    End Property

    Private _DrAmnt As Double
    <XafDisplayName("เดบิต")> _
    <Index(11), VisibleInListView(False), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property DrAmnt() As Double
        Get
            Return _DrAmnt
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("DrAmnt", _DrAmnt, value)
        End Set
    End Property

    Private _CrAmnt As Double
    <XafDisplayName("เครดิต")> _
    <Index(12), VisibleInListView(False), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property CrAmnt() As Double
        Get
            Return _CrAmnt
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("CrAmnt", _CrAmnt, value)
        End Set
    End Property

    Private _TotalAmnt As Double
    <XafDisplayName("ยอดเงินคงเหลือ")> _
    <Index(13), VisibleInListView(False), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property TotalAmnt() As Double
        Get
            Return _TotalAmnt
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("TotalAmnt", _TotalAmnt, value)
        End Set
    End Property

    '<Action(Caption:="My UI Action", ConfirmationMessage:="Are you sure?", ImageName:="Attention", AutoCommit:=True)> _
    'Public Sub ActionMethod()
    '    ' Trigger a custom business logic for the current record in the UI (http://documentation.devexpress.com/#Xaf/CustomDocument2619).
    '    Me.PersistentProperty = "Paid"
    'End Sub
End Class
