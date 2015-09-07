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
<DefaultClassOptions(), ImageName("BO_Task")> _
Public Class PlanBudgetDetail02 ' Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        DataOwner = GetCurrentSite()
        MyBase.AfterConstruction()
        ' Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).
    End Sub

    Protected Overrides Sub OnSaving()
        MyBase.OnSaving()
        If PlaningStatus = StausAppove.status0 Then
            Dim Result As ResultBudget02
            Result = New ResultBudget02(Session)
            Result.PlanBudget02 = Me
            Dim PlanMonth As PlanBudgetDetailMonth02
            PlanMonth = New PlanBudgetDetailMonth02(Session)
            PlanMonth.PlanBudget02 = Me
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

    Private _ProjectSubActivityName As String
    <XafDisplayName("โครงการ")> _
    <Index(0), VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)> _
    Public Property ProjectSubActivityName() As String
        Get

            Return _ProjectSubActivityName

        End Get
        Set(ByVal value As String)
            SetPropertyValue("ProjectSubActivityName", _ProjectSubActivityName, value)
        End Set
    End Property

    Private _PlanBudgetType As Integer
    <Index(0), VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)> _
    Public Property PlanBudgetType() As Integer
        Get

            Return _PlanBudgetType

        End Get
        Set(ByVal value As Integer)
            SetPropertyValue("PlanBudgetType", _PlanBudgetType, value)
        End Set
    End Property

    Private _ExpenseID As String
    <XafDisplayName("ลำดับที่")> _
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
    <PersistentAlias("PlanBudgetDetailMonth01s.Sum(BudgetQuarter1)")> _
    <VisibleInDetailView(False), VisibleInListView(False)> _
    <ImmediatePostData> _
    Public ReadOnly Property BudgetQuarter1() As Double
        Get
            Try
                Dim tempObject As Object
                tempObject = EvaluateAlias("BudgetQuarter1")
                Return CDbl(tempObject)
            Catch ex As Exception
                Return 0
            End Try
        End Get
    End Property

    Private _BudgetQuarter2 As Double
    <XafDisplayName("งบประมาณจัดสรร (ม.ค. - มี.ค.)")> _
    <PersistentAlias("PlanBudgetDetailMonth01s.Sum(BudgetQuarter2)")> _
    <VisibleInDetailView(False), VisibleInListView(False)> _
    <ImmediatePostData> _
    Public ReadOnly Property BudgetQuarter2() As Double
        Get
            Try
                Dim tempObject As Object
                tempObject = EvaluateAlias("BudgetQuarter2")
                Return CDbl(tempObject)
            Catch ex As Exception
                Return 0
            End Try
        End Get
    End Property

    Private _BudgetQuarter3 As Double
    <XafDisplayName("งบประมาณจัดสรร (เม.ย. - มิ.ย.)")> _
    <PersistentAlias("PlanBudgetDetailMonth01s.Sum(BudgetQuarter3)")> _
    <VisibleInDetailView(False), VisibleInListView(False)> _
    <ImmediatePostData> _
    Public ReadOnly Property BudgetQuarter3() As Double
        Get
            Try
                Dim tempObject As Object
                tempObject = EvaluateAlias("BudgetQuarter3")
                Return CDbl(tempObject)
            Catch ex As Exception
                Return 0
            End Try
        End Get
    End Property

    Private _BudgetQuarter4 As Double
    <XafDisplayName("งบประมาณจัดสรร (ก.ค. - ก.ย.)")> _
    <PersistentAlias("PlanBudgetDetailMonth01s.Sum(BudgetQuarter4)")> _
    <VisibleInDetailView(False), VisibleInListView(False)> _
    <ImmediatePostData> _
    Public ReadOnly Property BudgetQuarter4() As Double
        Get
            Try
                Dim tempObject As Object
                tempObject = EvaluateAlias("BudgetQuarter4")
                Return CDbl(tempObject)
            Catch ex As Exception
                Return 0
            End Try
        End Get
    End Property

    <XafDisplayName("รวมทั้งสิ้น แผน"), VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)> _
    Public ReadOnly Property SumPlan As Double
        Get
            Return (BudgetQuarter1 + BudgetQuarter2 + BudgetQuarter3 + BudgetQuarter4)
        End Get
    End Property

#Region "==========Association============"
    Dim _PlanBudget02 As PlanBudget02
    <Association("PlanBudget02-PlanBudgetDetail02")> _
    <XafDisplayName("ปีงบประมาณ"), VisibleInDetailView(False), VisibleInListView(False)> _
    <Index(13)> _
    Public Property PlanBudget02() As PlanBudget02
        Get
            Return _PlanBudget02
        End Get
        Set(ByVal value As PlanBudget02)
            SetPropertyValue(Of PlanBudget02)("PlanBudget02", _PlanBudget02, value)
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
    '<Appearance("DISList", Visibility:=Editors.ViewItemVisibility.Hide, context:="DetailView", Criteria:="ExpenseID = '1.'OR ExpenseID = '2.'OR ExpenseID = '3.'OR ExpenseID = '4.'OR ExpenseID = '4.1.'OR ExpenseID = '4.2.'OR ExpenseID = '5.'")> _
#Region "==========Association============"
    <Association("PlanBudgetDetail02-PlanBudgetDetailMonth02s", GetType(PlanBudgetDetailMonth02))> _
    <DevExpress.Xpo.Aggregated>
    <Appearance("DISListMonth", Visibility:=Editors.ViewItemVisibility.Hide, context:="DetailView", Criteria:="ExpenseID = '1.'OR ExpenseID = '2.'OR ExpenseID = '3.'OR ExpenseID = '4.'OR ExpenseID = '4.1.'OR ExpenseID = '4.2.'OR ExpenseID = '5.'")> _
       <XafDisplayName("แผนการใช้จ่ายงบประมาณประจำเดือน")> _
    Public ReadOnly Property PlanBudgetDetailMonth02s() As XPCollection(Of PlanBudgetDetailMonth02)
        Get
            Return GetCollection(Of PlanBudgetDetailMonth02)("PlanBudgetDetailMonth02s")
        End Get
    End Property
#End Region
    '<Appearance("DISList", Visibility:=Editors.ViewItemVisibility.Hide, context:="DetailView", Criteria:="ExpenseID = '1.'OR ExpenseID = '2.'OR ExpenseID = '3.'OR ExpenseID = '4.'OR ExpenseID = '4.1.'OR ExpenseID = '4.2.'OR ExpenseID = '5.'")> _
#Region "==========Association============"
    <Association("PlanBudgetDetail02-ResultBudget02", GetType(ResultBudget02))> _
    <DevExpress.Xpo.Aggregated>
    <Appearance("DISListResult", Visibility:=Editors.ViewItemVisibility.Hide, context:="DetailView", Criteria:="ExpenseID = '1.'OR ExpenseID = '2.'OR ExpenseID = '3.'OR ExpenseID = '4.'OR ExpenseID = '4.1.'OR ExpenseID = '4.2.'OR ExpenseID = '5.'")> _
    <XafDisplayName("ผลการใช้จ่ายงบประมาณประจำเดือน")> _
    Public ReadOnly Property ResultBudget02s() As XPCollection(Of ResultBudget02)
        Get
            Return GetCollection(Of ResultBudget02)("ResultBudget02s")
        End Get
    End Property
#End Region

End Class
