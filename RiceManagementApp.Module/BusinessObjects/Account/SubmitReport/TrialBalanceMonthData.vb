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
Public Class TrialBalanceMonthData ' Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        ' Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).
    End Sub

    Dim fSubmitTrialBalacneMonthReport As SubmitTrialBalacneMonthReport
    <Association("SubmitTrialBalacneMonthReport-TrialBalanceMonthData")> _
    Public Property SubmitTrialBalacneMonthReport() As SubmitTrialBalacneMonthReport
        Get
            Return fSubmitTrialBalacneMonthReport
        End Get
        Set(ByVal value As SubmitTrialBalacneMonthReport)
            SetPropertyValue(Of SubmitTrialBalacneMonthReport)("SubmitTrialBalacneMonthReport", fSubmitTrialBalacneMonthReport, value)
        End Set
    End Property
    Private _AccountID As String
    <XafDisplayName("รหัสบัญชี")> _
    <Size(15)> _
    <Index(1), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    Public Property AccountID() As String
        Get
            Return _AccountID
        End Get
        Set(ByVal value As String)
            SetPropertyValue("AccountID", _AccountID, value)
        End Set
    End Property

    Private _AccountName As String
    <XafDisplayName("ชื่อบัญชี")> _
    <Index(2), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    Public Property AccountName() As String
        Get
            Return _AccountName
        End Get
        Set(ByVal value As String)
            SetPropertyValue("AccountName", _AccountName, value)
        End Set
    End Property

    Private _ForwardDrCr As Double
    <XafDisplayName("ยอดยกมา")> _
    <Index(3), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    Public Property ForwardDrCr() As Double
        Get
            Return _ForwardDrCr
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("ForwardDrCr", _ForwardDrCr, value)
        End Set
    End Property

    Private _CurrentDrCr As Double
    <XafDisplayName("ยอดเคลื่อนไหว")> _
    <Index(4), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    Public Property CurrentDrCr() As Double
        Get
            Return _CurrentDrCr
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("CurrentDrCr", _CurrentDrCr, value)
        End Set
    End Property

    Private _TotalDrCr As Double
    <XafDisplayName("ยอดยกไป")> _
    <Index(4), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    Public Property TotalDrCr() As Double
        Get
            Return _TotalDrCr
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("TotalDrCr", _TotalDrCr, value)
        End Set
    End Property
End Class
