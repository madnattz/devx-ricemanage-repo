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
Public Class AccountGFMISDetailRVData ' Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        ' Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).
    End Sub

    Private _GFMIS As SubmitIncomeAndExpensesMonthReport
    <Association("SubmitIncomeAndExpensesMonthReport-AccountGFMISDetailRVDatas")> _
    <Index(0), VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)> _
    <ImmediatePostData()> _
    Public Property GFMIS() As SubmitIncomeAndExpensesMonthReport
        Get
            Return _GFMIS
        End Get
        Set(ByVal value As SubmitIncomeAndExpensesMonthReport)
            SetPropertyValue("GFMIS", _GFMIS, value)
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

    Private _CodeGF As String
    <XafDisplayName("GL")> _
    <Index(3), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    Public Property CodeGF() As String
        Get
            Return _CodeGF
        End Get
        Set(ByVal value As String)
            SetPropertyValue("CodeGF", _CodeGF, value)
        End Set
    End Property

    Private _AmountIncome As Double
    <XafDisplayName("เดือนนี้")> _
    <ModelDefault("DisplayFormat", "{0:N2}")> _
    <ModelDefault("EditMask", "N")> _
    <Index(4), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    <ImmediatePostData()> _
    Public Property AmountIncome() As Double
        Get
            Return _AmountIncome
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("AmountIncome", _AmountIncome, value)
        End Set
    End Property
End Class
