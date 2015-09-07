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
<DefaultClassOptions(), NavigationItem("ตั้งค่า"), XafDisplayName("InvestmentDetail")> _
Public Class InvestmentDetail ' Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        ' Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).
    End Sub

    Private _MonthTrialBalance As PublicEnum.EnumMonth
    <XafDisplayName("เดือน")> _
    <Index(0), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property MonthTrialBalance() As PublicEnum.EnumMonth
        Get
            Return _MonthTrialBalance
        End Get
        Set(ByVal value As PublicEnum.EnumMonth)
            SetPropertyValue("MonthTrialBalance", _MonthTrialBalance, value)
        End Set
    End Property

    Private _YearID As String
    <XafDisplayName("ปี")> _
    <Index(1), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    Public Property YearID() As String
        Get
            Return _YearID
        End Get
        Set(ByVal value As String)
            SetPropertyValue("YearID", _YearID, value)
        End Set
    End Property

    Private _listGF As String
    <XafDisplayName("รายการGF")> _
    <Index(2), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property listGF() As String
        Get
            Return _listGF
        End Get
        Set(ByVal value As String)
            SetPropertyValue("listGF", _listGF, value)
        End Set
    End Property

    Private _CodeGL As String
    <XafDisplayName("GL")> _
    <Index(3), VisibleInListView(False), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property CodeGL() As String
        Get
            Return _CodeGL
        End Get
        Set(ByVal value As String)
            SetPropertyValue("CodeGL", _CodeGL, value)
        End Set
    End Property

    Private _AmountExpend As Decimal
    <XafDisplayName("เดือนนี้")> _
    <ModelDefault("DisplayFormat", "{0:N2}")> _
    <ModelDefault("EditMask", "N")> _
    <Index(4), VisibleInListView(False), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    <ImmediatePostData()> _
    Public Property AmountExpend() As Decimal
        Get
            Return _AmountExpend
        End Get
        Set(ByVal value As Decimal)
            SetPropertyValue("AmountExpend", _AmountExpend, value)
        End Set
    End Property

    '<Action(Caption:="My UI Action", ConfirmationMessage:="Are you sure?", ImageName:="Attention", AutoCommit:=True)> _
    'Public Sub ActionMethod()
    '    ' Trigger a custom business logic for the current record in the UI (http://documentation.devexpress.com/#Xaf/CustomDocument2619).
    '    Me.PersistentProperty = "Paid"
    'End Sub
End Class
