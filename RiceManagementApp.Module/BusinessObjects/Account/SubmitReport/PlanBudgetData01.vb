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
Imports RiceManagementApp.Module.PublicEnum

'<ImageName("BO_Contact")> _
'<DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")> _
'<DefaultListViewOptions(MasterDetailMode.ListViewOnly, False, NewItemRowPosition.None)> _
'<Persistent("DatabaseTableName")> _
<DefaultClassOptions()> _
Public Class PlanBudgetData01 ' Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        ' Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).
    End Sub

    Dim fSubmitPlanBudget01Report As SubmitPlanBudget01Report
    <Association("SubmitPlanBudget01Report-PlanBudgetData01s")> _
    Public Property SubmitPlanBudget01Report() As SubmitPlanBudget01Report
        Get
            Return fSubmitPlanBudget01Report
        End Get
        Set(ByVal value As SubmitPlanBudget01Report)
            SetPropertyValue(Of SubmitPlanBudget01Report)("SubmitPlanBudget01Report", fSubmitPlanBudget01Report, value)
        End Set
    End Property

    Private _ActivityName As String
    <XafDisplayName("ชื่อกิจกรรม")> _
    <VisibleInDetailView(False), VisibleInListView(True)> _
    <Appearance("DISACT", Enabled:=False, context:="ListView")> _
    Public Property ActivityName() As String
        Get
            Return _ActivityName
        End Get
        Set(ByVal value As String)
            SetPropertyValue("ActivityName", _ActivityName, value)
        End Set
    End Property

    '=======DropDown เลือก หน่วยนับ========
    Private _Classifier As classifier
    <XafDisplayName("หน่วย")> _
   <Appearance("DISPClass", Enabled:=False, context:="ListView", Criteria:="[PlanBudget01.PlaningStatus] == 'Status2'")> _
    <Appearance("DisClass", Enabled:=False, Context:="DetailView")> _
     <VisibleInDetailView(False), VisibleInListView(True)> _
    <ImmediatePostData> _
    Public Property Classifier As classifier
        Get
            Return _Classifier
        End Get
        Set(ByVal value As classifier)
            SetPropertyValue("Classifier", _Classifier, value)
        End Set
    End Property

    <Association("PlanBudgetData01-PlanBudgetMonthData01s", GetType(PlanBudgetMonthData01))> _
<DevExpress.Xpo.Aggregated> _
<XafDisplayName("ข้อมูลแผนรายเดือน")> _
    Public ReadOnly Property PlanBudgetMonthData01s() As XPCollection(Of PlanBudgetMonthData01)
        Get
            Return GetCollection(Of PlanBudgetMonthData01)("PlanBudgetMonthData01s")
        End Get
    End Property

    <Association("PlanBudgetData01-ResultBudgetData01s", GetType(ResultBudgetData01))> _
<DevExpress.Xpo.Aggregated> _
<XafDisplayName("ข้อมูลผลรายเดือน")> _
    Public ReadOnly Property ResultBudgetData01s() As XPCollection(Of ResultBudgetData01)
        Get
            Return GetCollection(Of ResultBudgetData01)("ResultBudgetData01s")
        End Get
    End Property

    '<Action(Caption:="My UI Action", ConfirmationMessage:="Are you sure?", ImageName:="Attention", AutoCommit:=True)> _
    'Public Sub ActionMethod()
    '    ' Trigger a custom business logic for the current record in the UI (http://documentation.devexpress.com/#Xaf/CustomDocument2619).
    '    Me.PersistentProperty = "Paid"
    'End Sub
End Class
