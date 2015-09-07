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
Imports DevExpress.ExpressApp.ConditionalAppearance

'<ImageName("BO_Contact")> _
'<DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")> _
'<DefaultListViewOptions(MasterDetailMode.ListViewOnly, False, NewItemRowPosition.None)> _
'<Persistent("DatabaseTableName")> _
<NavigationItem("ทดสอบ")> _
<DefaultClassOptions(), ImageName("BO_Task")> _
Public Class DemoPlanBudgetDetail02 ' Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        DataOwner = GetCurrentSite()
        MyBase.AfterConstruction()
        ' Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).
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
    Private _ExpenseID As String
    <XafDisplayName("ลำดับที่")> _
    <Size(15)> _
    <Index(1), VisibleInListView(True), VisibleInDetailView(False), VisibleInLookupListView(True)> _
        <Appearance("ExpenseID", Enabled:=False, Context:="ListView")> _
    Public Property ExpenseID() As String
        Get

            Return _ExpenseID

        End Get
        Set(ByVal value As String)
            SetPropertyValue("ExpenseID", _ExpenseID, value)
        End Set
    End Property

    Private _ExpenseName As String
    <XafDisplayName("รายการ")> _
    <Index(2), VisibleInListView(True), VisibleInDetailView(False), VisibleInLookupListView(True)> _
    <Appearance("ExpenseName", Enabled:=False, Context:="ListView")> _
    Public Property ExpenseName() As String
        Get

            Return _ExpenseName

        End Get
        Set(ByVal value As String)
            SetPropertyValue("ExpenseName", _ExpenseName, value)
        End Set
    End Property



    Private _BudgetQuarter1 As Double
    <XafDisplayName("งบประมาณจัดสรร (ต.ค. - ธ.ค.)")> _
    <VisibleInListView(False)> _
   <Appearance("DISQ1", Visibility:=Editors.ViewItemVisibility.Hide, context:="DetailView", Criteria:="ExpenseID = '1.'OR ExpenseID = '2.'OR ExpenseID = '3.'OR ExpenseID = '4.'OR ExpenseID = '4.1.'OR ExpenseID = '4.2.'OR ExpenseID = '5.'")> _
    <ImmediatePostData> _
    Public Property BudgetQuarter1() As Double
        Get
            Return _BudgetQuarter1
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("BudgetQuarter1", _BudgetQuarter1, value)
        End Set
    End Property

    Private _BudgetQuarter2 As Double
    <XafDisplayName("งบประมาณจัดสรร (ม.ค. - มี.ค.)")> _
    <VisibleInListView(False)> _
   <Appearance("DISQ2", Visibility:=Editors.ViewItemVisibility.Hide, context:="DetailView", Criteria:="ExpenseID = '1.'OR ExpenseID = '2.'OR ExpenseID = '3.'OR ExpenseID = '4.'OR ExpenseID = '4.1.'OR ExpenseID = '4.2.'OR ExpenseID = '5.'")> _
    <ImmediatePostData> _
    Public Property BudgetQuarter2() As Double
        Get
            Return _BudgetQuarter2
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("BudgetQuarter2", _BudgetQuarter2, value)
        End Set
    End Property

    Private _BudgetQuarter3 As Double
    <XafDisplayName("งบประมาณจัดสรร (เม.ย. - มิ.ย.)")> _
    <VisibleInListView(False)> _
   <Appearance("DISQ3", Visibility:=Editors.ViewItemVisibility.Hide, context:="DetailView", Criteria:="ExpenseID = '1.'OR ExpenseID = '2.'OR ExpenseID = '3.'OR ExpenseID = '4.'OR ExpenseID = '4.1.'OR ExpenseID = '4.2.'OR ExpenseID = '5.'")> _
    <ImmediatePostData> _
    Public Property BudgetQuarter3() As Double
        Get
            Return _BudgetQuarter3
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("BudgetQuarter3", _BudgetQuarter3, value)
        End Set
    End Property

    Private _BudgetQuarter4 As Double
    <XafDisplayName("งบประมาณจัดสรร (ก.ค. - ก.ย.)")> _
    <VisibleInListView(False)> _
   <Appearance("DISQ4", Visibility:=Editors.ViewItemVisibility.Hide, context:="DetailView", Criteria:="ExpenseID = '1.'OR ExpenseID = '2.'OR ExpenseID = '3.'OR ExpenseID = '4.'OR ExpenseID = '4.1.'OR ExpenseID = '4.2.'OR ExpenseID = '5.'")> _
    <ImmediatePostData> _
    Public Property BudgetQuarter4() As Double
        Get
            Return _BudgetQuarter4
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("BudgetQuarter4", _BudgetQuarter4, value)
        End Set
    End Property
    <XafDisplayName("รวมทั้งสิ้น แผน"), VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)> _
    Public ReadOnly Property SumPlan As Double
        Get
            Return (BudgetQuarter1 + BudgetQuarter2 + BudgetQuarter3 + BudgetQuarter4)
        End Get
    End Property
    Protected Overrides Sub OnSaving()
        MyBase.OnSaving()
        If PlaningStatus = StausAppove.status0 Then
            Dim Result As DemoResultBudget02
            Result = New DemoResultBudget02(Session)
            Result.PlanBudget02 = Me
            PlaningStatus = StausAppove.Status1
        End If

    End Sub
    Protected Overrides Sub OnSaved()
        MyBase.OnSaved()
        If Me.DataOwner Is Nothing Then
            DataOwner = GetCurrentSite()
        End If
        Session.CommitTransaction()
    End Sub


#Region "==========Association============"
    Dim _PlanBudget02 As DemoPlanBudget02
    <Association("PlanBudget02-PlanBudgetDetail02")> _
    <XafDisplayName("ปีงบประมาณ"), VisibleInDetailView(False), VisibleInListView(False)> _
    <Index(13)> _
    Public Property PlanBudget02() As DemoPlanBudget02
        Get
            Return _PlanBudget02
        End Get
        Set(ByVal value As DemoPlanBudget02)
            SetPropertyValue(Of DemoPlanBudget02)("PlanBudget02", _PlanBudget02, value)
        End Set
    End Property
#End Region

    Private _PlaningStatus As StausAppove
    <XafDisplayName("สถานะ")> _
    <Index(4)> _
    <RuleRequiredField(DefaultContexts.Save), VisibleInDetailView(False), VisibleInListView(False)> _
    Public Property PlaningStatus() As StausAppove
        Get
            Return _PlaningStatus
        End Get
        Set(ByVal value As StausAppove)
            SetPropertyValue("PlaningStatus", _PlaningStatus, value)
        End Set
    End Property

#Region "==========Association============"
    <Association("PlanBudgetDetail02-ResultBudget02", GetType(DemoResultBudget02))> _
    <DevExpress.Xpo.Aggregated>
   <Appearance("DISList", Visibility:=Editors.ViewItemVisibility.Hide, context:="DetailView", Criteria:="ExpenseID = '1.'OR ExpenseID = '2.'OR ExpenseID = '3.'OR ExpenseID = '4.'OR ExpenseID = '4.1.'OR ExpenseID = '4.2.'OR ExpenseID = '5.'")> _
    <XafDisplayName("ผลการใช้จ่ายงบประมาณประจำเดือน")> _
    Public ReadOnly Property ResultBudget02s() As XPCollection(Of DemoResultBudget02)
        Get
            Return GetCollection(Of DemoResultBudget02)("ResultBudget02s")
        End Get
    End Property
#End Region
End Class
