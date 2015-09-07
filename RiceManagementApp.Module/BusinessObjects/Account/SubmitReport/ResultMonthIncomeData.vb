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
Public Class ResultMonthIncomeData ' Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        ' Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).
    End Sub

    Dim fSubmitResultMonthReport As SubmitIncomeAndExpensesMonthReport
    <Association("SubmitIncomeAndExpensesMonthReport-ResultMonthIncomeDatas")> _
    Public Property SubmitResultMonthReport() As SubmitIncomeAndExpensesMonthReport
        Get
            Return fSubmitResultMonthReport
        End Get
        Set(ByVal value As SubmitIncomeAndExpensesMonthReport)
            SetPropertyValue(Of SubmitIncomeAndExpensesMonthReport)("SubmitResultMonthReport", fSubmitResultMonthReport, value)
        End Set
    End Property

    Private _ResultIncomeOid As String
    <Index(0), VisibleInDetailView(False), VisibleInListView(False)> _
    Public Property ResultIncomeOid() As String
        Get
            Return _ResultIncomeOid
        End Get
        Set(ByVal value As String)
            SetPropertyValue("ResultIncomeOid", _ResultIncomeOid, value)
        End Set
    End Property

    Private _listID As String
    <XafDisplayName("รหัส")> _
    <Index(0), VisibleInListView(True), VisibleInDetailView(False), VisibleInLookupListView(True)> _
    Public Property listID() As String
        Get
            Return _listID
        End Get
        Set(ByVal value As String)
            SetPropertyValue("listID", _listID, value)
        End Set
    End Property

    Private _listIncome As String
    <XafDisplayName("รายการ")> _
    <Index(1), VisibleInListView(True), VisibleInDetailView(False), VisibleInLookupListView(True)> _
    Public Property listIncome() As String
        Get
            Return _listIncome
        End Get
        Set(ByVal value As String)
            SetPropertyValue("listIncome", _listIncome, value)
        End Set
    End Property

    Private _IncomeMonth As Double
    <XafDisplayName("ได้รับโอนเงิน เดือนนี้")> _
    <Index(2), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    <ImmediatePostData()> _
    Public Property IncomeMonth() As Double
        Get
            Return _IncomeMonth
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("IncomeMonth", _IncomeMonth, value)
        End Set
    End Property

    Private _ExpendMonth As Double
    <XafDisplayName("ใช้จ่าย เดือนนี้")> _
    <Index(3), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    <ImmediatePostData()> _
    Public Property ExpendMonth() As Double
        Get
            Return _ExpendMonth
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("ExpendMonth", _ExpendMonth, value)
        End Set
    End Property

    Private _Note As String
    <XafDisplayName("หมายเหตุ")> _
    <Index(4), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property Note() As String
        Get
            Return _Note
        End Get
        Set(ByVal value As String)
            SetPropertyValue("Note", _Note, value)
        End Set
    End Property
End Class
