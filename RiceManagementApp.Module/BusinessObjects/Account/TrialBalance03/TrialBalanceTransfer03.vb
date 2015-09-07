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
<DefaultClassOptions(), XafDisplayName("แผนการรับ - จ่าย เงินประจำปีงบประมาณ")> _
Public Class TrialBalanceTransfer03 ' Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        DataOwner = GetCurrentSite()
        CreateTrialBalanceIncome()
        CreateTrialBalanceExpenses()
        MyBase.AfterConstruction()
        ' Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).
    End Sub

    Protected Overrides Sub OnSaving()
        MyBase.OnSaving()
        If Me.DataOwner Is Nothing Then
            DataOwner = GetCurrentSite()
        End If
    End Sub


    Public Sub CreateTrialBalanceIncome()
        Dim coTrialBalanceIncome As XPCollection(Of SettingIncome) = New XPCollection(Of SettingIncome)(Session)
        coTrialBalanceIncome.Sorting.Add(New SortProperty("listID", DB.SortingDirection.Ascending))
        For Each item As SettingIncome In coTrialBalanceIncome
            Dim objDetail As New TrialBalanceTransferDetailIncome03(Session)
            objDetail.CatalogueNo = item
            objDetail.TrialBalanceTransfer03 = Me
            objDetail.Save()

        Next
    End Sub

    Public Sub CreateTrialBalanceExpenses()
        Dim coTrialBalanceExpenses As XPCollection(Of SettingExpenses) = New XPCollection(Of SettingExpenses)(Session)
        coTrialBalanceExpenses.Sorting.Add(New SortProperty("listID", DB.SortingDirection.Ascending))
        For Each item As SettingExpenses In coTrialBalanceExpenses
            Try
                Dim objDetail As New TrialBalanceTransferDetailExpenses03(Session)
                'Dim objDetail As TrialBalanceTransferDetailExpenses03
                objDetail.CatalogueNo = item
                objDetail.CodeGF_Oid = item.CodeGF_Oid
                objDetail.TrialBalanceTransfer03 = Me
                objDetail.Save()

            Catch ex As Exception

            End Try
        Next
        'fRefTransaction = New XPCollection(Of FactoryTransaction)(Session, CriteriaOperator.Parse("SeedProduct.Oid=? And FactoryNo=? ", Me.fSeedProduct.Oid, Me.fFactoryNo))
    End Sub

    Dim fDataOwner As Site
    <Browsable(False)> _
    Public Property DataOwner() As Site
        Get
            Return fDataOwner
        End Get
        Set(ByVal value As Site)
            SetPropertyValue(Of Site)("DataOwner", fDataOwner, value)
        End Set
    End Property

    Public Function GetCurrentSite() As Site
        Dim siteSetting As SiteSetting = Session.FindObject(Of SiteSetting)(Nothing)
        If siteSetting IsNot Nothing Then
            If siteSetting.Site IsNot Nothing Then
                Return siteSetting.Site
            Else
                Return Nothing
            End If
            Return siteSetting.Site
        Else
            Return Nothing
        End If
    End Function

    Private _FiscalYear As String
    <XafDisplayName("ปีงบประมาณ")> _
    <Index(0), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property FiscalYear() As String
        Get
            Return _FiscalYear
        End Get
        Set(ByVal value As String)
            SetPropertyValue("FiscalYear", _FiscalYear, value)
        End Set
    End Property

    <XafDisplayName("แผนการได้รับโอนเงิน (รายรับ)")> _
<Association("TrialBalanceTransfer03-TrialBalanceTransferDetailIncome03s", GetType(TrialBalanceTransferDetailIncome03))> _
<DevExpress.Xpo.Aggregated()> _
    Public ReadOnly Property TrialBalanceTransferDetailIncome03s() As XPCollection(Of TrialBalanceTransferDetailIncome03)
        Get
            Return GetCollection(Of TrialBalanceTransferDetailIncome03)("TrialBalanceTransferDetailIncome03s")
        End Get
    End Property

    <XafDisplayName("แผนการได้รับโอนเงิน (รายจ่าย)")> _
<Association("TrialBalanceTransfer03-TrialBalanceTransferDetailExpenses03s", GetType(TrialBalanceTransferDetailExpenses03))> _
<DevExpress.Xpo.Aggregated()> _
    Public ReadOnly Property TrialBalanceTransferDetailExpenses03s() As XPCollection(Of TrialBalanceTransferDetailExpenses03)
        Get
            Return GetCollection(Of TrialBalanceTransferDetailExpenses03)("TrialBalanceTransferDetailExpenses03s")
        End Get
    End Property

    Private _PlanQuarter1 As Double
    <XafDisplayName("แผนไตรมาส 1 (ต.ค. - ธ.ค.)")> _
    <PersistentAlias("TrialBalanceTransferDetailExpenses03s.Sum(PlanQuarter1)")> _
    <VisibleInDetailView(False), VisibleInListView(False)> _
    <ImmediatePostData> _
    Public Property PlanQuarter1() As Double
        Get
            Try
                Dim tempObject As Object
                tempObject = EvaluateAlias("PlanQuarter1")
                Return CDbl(tempObject)
            Catch ex As Exception
                Return 0
            End Try
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("PlanQuarter1", _PlanQuarter1, value)
        End Set
    End Property

    Private _PlanQuarter2 As Double
    <XafDisplayName("แผนไตรมาส 2 (ม.ค. - มี.ค.)")> _
    <PersistentAlias("TrialBalanceTransferDetailExpenses03s.Sum(PlanQuarter2)")> _
    <VisibleInDetailView(False), VisibleInListView(False)> _
    <ImmediatePostData> _
    Public Property PlanQuarter2() As Double
        Get
            Try
                Dim tempObject As Object
                tempObject = EvaluateAlias("PlanQuarter2")
                Return CDbl(tempObject)
            Catch ex As Exception
                Return 0
            End Try
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("PlanQuarter2", _PlanQuarter2, value)
        End Set
    End Property

    Private _PlanQuarter3 As Double
    <XafDisplayName("แผนไตรมาส 3 (เม.ย. - มิ.ย.)")> _
    <PersistentAlias("TrialBalanceTransferDetailExpenses03s.Sum(PlanQuarter3)")> _
    <VisibleInDetailView(False), VisibleInListView(False)> _
    <ImmediatePostData> _
    Public Property PlanQuarter3() As Double
        Get
            Try
                Dim tempObject As Object
                tempObject = EvaluateAlias("PlanQuarter3")
                Return CDbl(tempObject)
            Catch ex As Exception
                Return 0
            End Try
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("PlanQuarter3", _PlanQuarter3, value)
        End Set
    End Property

    Private _PlanQuarter4 As Double
    <XafDisplayName("แผนไตรมาส 4 (ก.ค. - ก.ย.)")> _
    <PersistentAlias("TrialBalanceTransferDetailExpenses03s.Sum(PlanQuarter4)")> _
    <VisibleInDetailView(False), VisibleInListView(False)> _
    <ImmediatePostData> _
    Public Property PlanQuarter4() As Double
        Get
            Try
                Dim tempObject As Object
                tempObject = EvaluateAlias("PlanQuarter4")
                Return CDbl(tempObject)
            Catch ex As Exception
                Return 0
            End Try
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("PlanQuarter4", _PlanQuarter4, value)
        End Set
    End Property

    <XafDisplayName("รวมทั้งสิ้น แผน"), VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)> _
    Public ReadOnly Property SumPlan As Double
        Get
            Return (PlanQuarter1 + PlanQuarter2 + PlanQuarter3 + PlanQuarter4)
        End Get
    End Property

    '<Action(Caption:="My UI Action", ConfirmationMessage:="Are you sure?", ImageName:="Attention", AutoCommit:=True)> _
    'Public Sub ActionMethod()
    '    ' Trigger a custom business logic for the current record in the UI (http://documentation.devexpress.com/#Xaf/CustomDocument2619).
    '    Me.PersistentProperty = "Paid"
    'End Sub
End Class
