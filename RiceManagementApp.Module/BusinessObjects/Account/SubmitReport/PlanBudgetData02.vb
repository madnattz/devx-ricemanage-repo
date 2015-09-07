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
<DefaultClassOptions()> _
Public Class PlanBudgetData02 ' Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        ' Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).
    End Sub

    Dim fSubmitPlanBudget02Report As SubmitPlanBudget02Report
    <Association("SubmitPlanBudget02Report-PlanBudgetData02s")> _
    Public Property SubmitPlanBudget02Report() As SubmitPlanBudget02Report
        Get
            Return fSubmitPlanBudget02Report
        End Get
        Set(ByVal value As SubmitPlanBudget02Report)
            SetPropertyValue(Of SubmitPlanBudget02Report)("SubmitPlanBudget02Report", fSubmitPlanBudget02Report, value)
        End Set
    End Property

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

    Private _PlanBudgetType As String
    <Index(0), VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)> _
    Public Property PlanBudgetType() As String
        Get

            Return _PlanBudgetType

        End Get
        Set(ByVal value As String)
            SetPropertyValue("PlanBudgetType", _PlanBudgetType, value)
        End Set
    End Property

    '    <Appearance("DISACTPB02", Enabled:=False, context:="ListView")> _
    Private _ExpenseID As String
    <XafDisplayName("ลำดับที่")> _
    <VisibleInDetailView(False), VisibleInListView(True)> _
    Public Property ExpenseID() As String
        Get
            Return _ExpenseID
        End Get
        Set(ByVal value As String)
            SetPropertyValue("ExpenseID", _ExpenseID, value)
        End Set
    End Property
    '    <Appearance("DISACTPB02", Enabled:=False, context:="ListView")> _
    Private _ExpenseName As String
    <XafDisplayName("ชื่อรายจ่่าย")> _
    <VisibleInDetailView(False), VisibleInListView(True)> _
    Public Property ExpenseName() As String
        Get
            Return _ExpenseName
        End Get
        Set(ByVal value As String)
            SetPropertyValue("ExpenseName", _ExpenseName, value)
        End Set
    End Property

    <Association("PlanBudgetData02-PlanBudgetMonthData02s", GetType(PlanBudgetMonthData02))> _
<DevExpress.Xpo.Aggregated> _
<XafDisplayName("ข้อมูลแผนรายเดือน")> _
    Public ReadOnly Property PlanBudgetMonthData02s() As XPCollection(Of PlanBudgetMonthData02)
        Get
            Return GetCollection(Of PlanBudgetMonthData02)("PlanBudgetMonthData02s")
        End Get
    End Property

    <Association("PlanBudgetData02-ResultBudgetData02s", GetType(ResultBudgetData02))> _
<DevExpress.Xpo.Aggregated> _
<XafDisplayName("ข้อมูลผลรายเดือน")> _
    Public ReadOnly Property ResultBudgetData02s() As XPCollection(Of ResultBudgetData02)
        Get
            Return GetCollection(Of ResultBudgetData02)("ResultBudgetData02s")
        End Get
    End Property

End Class
