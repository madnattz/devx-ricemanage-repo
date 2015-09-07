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
Public Class PayDateData ' Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        ' Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).
    End Sub

    Dim fSubmitPayDateReport As SubmitPayDateReport
    <Association("SubmitPayDateReport-PayDateData")> _
    Public Property SubmitPayDateReport() As SubmitPayDateReport
        Get
            Return fSubmitPayDateReport
        End Get
        Set(ByVal value As SubmitPayDateReport)
            SetPropertyValue(Of SubmitPayDateReport)("SubmitPayDateReport", fSubmitPayDateReport, value)
        End Set
    End Property

    Private _SeedName As SeedType
    <XafDisplayName("พันธุ์")> _
    <Index(0), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    <ImmediatePostData()> _
    Public Property SeedName() As SeedType
        Get
            Return _SeedName
        End Get
        Set(ByVal value As SeedType)
            SetPropertyValue("SeedName", _SeedName, value)
        End Set
    End Property

    Private _SeedClass As SeedClass
    <XafDisplayName("ชั้นพันธุ์")> _
    <Index(1), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    <ImmediatePostData()> _
    Public Property SeedClass() As SeedClass
        Get
            Return _SeedClass
        End Get
        Set(ByVal value As SeedClass)
            SetPropertyValue("SeedClass", _SeedClass, value)
        End Set
    End Property

    Private _BatchNo As String
    <XafDisplayName("รุ่นที่")> _
    <Index(2), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    <ImmediatePostData()> _
    Public Property BatchNo() As String
        Get
            Return _BatchNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue("BatchNo", _BatchNo, value)
        End Set
    End Property

    Private _RefNo As String
    <XafDisplayName("เลขที่เอกสาร ขบ.03")> _
    <Index(3), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    <ImmediatePostData()> _
    Public Property RefNo() As String
        Get
            Return _RefNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue("RefNo", _RefNo, value)
        End Set
    End Property

    Private _WeightDate As Date
    <XafDisplayName("วันที่ตามใบชั่งน้ำหนัก")> _
    <Index(4), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    <ImmediatePostData()> _
    Public Property WeightDate() As Date
        Get
            Return _WeightDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue("WeightDate", _WeightDate, value)
        End Set
    End Property

    Private _RefDate As Date
    <XafDisplayName("วันที่เอกสาร ขบ.03")> _
    <Index(5), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    <ImmediatePostData()> _
    Public Property RefDate() As Date
        Get
            Return _RefDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue("RefDate", _RefDate, value)
        End Set
    End Property

    Private _CenterPayDate As Date
    <XafDisplayName("วันที่คลังโอนเงินเขาบัญชีศูนย์")> _
    <Index(6), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    <ImmediatePostData()> _
    Public Property CenterPayDate() As Date
        Get
            Return _CenterPayDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue("CenterPayDate", _CenterPayDate, value)
        End Set
    End Property

    Private _FamerPayDate As Date
    <XafDisplayName("วันที่โอนเงินเข้าบัญชีเกษตรกร")> _
    <Index(7), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    <ImmediatePostData()> _
    Public Property FamerPayDate() As Date
        Get
            Return _FamerPayDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue("FamerPayDate", _FamerPayDate, value)
        End Set
    End Property

    Private _DateCount As String
    <ImmediatePostData()> _
   <XafDisplayName("ระยะเวลาการโอน")> _
   <Index(8), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    Public Property DateCount As String
        Get
            Return _DateCount
        End Get
        Set(ByVal value As String)
            SetPropertyValue("DateCount", _DateCount, value)
        End Set
    End Property

    Private _TotalFamer As String
    <XafDisplayName("จำนวนเกษตรกร (ราย)")> _
    <Index(9), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    <ImmediatePostData()> _
    Public Property TotalFamer() As String
        Get
            Return _TotalFamer
        End Get
        Set(ByVal value As String)
            SetPropertyValue("TotalFamer", _TotalFamer, value)
        End Set
    End Property

    Private _TotalWeight As Double
    <XafDisplayName("ปริมาณซื้อคืน (กก.)")> _
    <ModelDefault("DisplayFormat", "{0:N2}")> _
    <ModelDefault("EditMask", "N")> _
    <Index(10), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    <ImmediatePostData()> _
    Public Property TotalWeight() As Double
        Get
            Return _TotalWeight
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("TotalWeight", _TotalWeight, value)
        End Set
    End Property

    Private _TotalAmount As Double
    <ModelDefault("DisplayFormat", "{0:N2}")> _
    <ModelDefault("EditMask", "N")> _
    <XafDisplayName("วงเงินที่เบิก (บาท)")> _
    <Index(11), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    <ImmediatePostData()> _
    Public Property TotalAmount() As Double
        Get
            Return _TotalAmount
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("TotalAmount", _TotalAmount, value)
        End Set
    End Property

    'Function CalculateDate(sDate As Date, eDate As Date) As Integer
    '    Dim TotalDayCount As Integer = 0
    '    If sDate <> Nothing And eDate <> Nothing Then
    '        'TotalDayCount = DateDiff(DateInterval.Day, WeightDate, FamerPayDate)
    '        Dim tempStartDate As Date = sDate
    '        While tempStartDate < eDate
    '            If tempStartDate.DayOfWeek <> DayOfWeek.Saturday And tempStartDate.DayOfWeek <> DayOfWeek.Sunday Then
    '                TotalDayCount += 1
    '            End If
    '            tempStartDate = tempStartDate.AddDays(1)
    '        End While

    '        Dim VacationCount As Integer = 0 'Session.Evaluate(GetType(Vacation), CriteriaOperator.Parse("Count()"), CriteriaOperator.Parse("VacationDate >= ? AND VacationDate <= ? ", WeightDate, FamerPayDate))

    '        Dim VacationColl As New XPCollection(Of VacationDetail)(Session, CriteriaOperator.Parse("VacationDate >= ? AND VacationDate <= ?", sDate, eDate))

    '        For Each item As VacationDetail In VacationColl
    '            If Not item.VacationDate.DayOfWeek = DayOfWeek.Saturday And Not item.VacationDate.DayOfWeek = DayOfWeek.Sunday Then
    '                VacationCount += 1
    '            End If
    '        Next

    '        Return TotalDayCount - VacationCount
    '    Else
    '        Return 0
    '    End If
    'End Function
    '<Action(Caption:="My UI Action", ConfirmationMessage:="Are you sure?", ImageName:="Attention", AutoCommit:=True)> _
    'Public Sub ActionMethod()
    '    ' Trigger a custom business logic for the current record in the UI (http://documentation.devexpress.com/#Xaf/CustomDocument2619).
    '    Me.PersistentProperty = "Paid"
    'End Sub
End Class
