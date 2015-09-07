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
Public Class TrialBalanceQuarterData ' Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        ' Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).
    End Sub

    Dim fSubmitTrialBalacneQuarterReport As SubmitTrialBalacneQuarterReport
    <Association("SubmitTrialBalacneQuarterReport-TrialBalanceQuarterData")> _
    Public Property SubmitTrialBalacneQuarterReport() As SubmitTrialBalacneQuarterReport
        Get
            Return fSubmitTrialBalacneQuarterReport
        End Get
        Set(ByVal value As SubmitTrialBalacneQuarterReport)
            SetPropertyValue(Of SubmitTrialBalacneQuarterReport)("SubmitTrialBalacneQuarterReport", fSubmitTrialBalacneQuarterReport, value)
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

    Private _TotalDr As Double
    <XafDisplayName("เดบิต")> _
    <Index(3), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    Public Property TotalDr() As Double
        Get
            Return _TotalDr
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("TotalDr", _TotalDr, value)
        End Set
    End Property

    Private _TotalCr As Double
    <XafDisplayName("เครดิต")> _
    <Index(4), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    Public Property TotalCr() As Double
        Get
            Return _TotalCr
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("TotalDr", _TotalCr, value)
        End Set
    End Property
End Class
