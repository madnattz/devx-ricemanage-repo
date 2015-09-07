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
Public Class PlanBudgetDetail01 ' Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
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
            Dim ResultMonth As PlanBudgetDetailMonth01
            ResultMonth = New PlanBudgetDetailMonth01(Session)
            ResultMonth.PlanBudget01 = Me

            Dim Result As ResultBudget01
            Result = New ResultBudget01(Session)
            Result.PlanBudget01 = Me
            PlaningStatus = StausAppove.Status1
        End If

        If Me.DataOwner Is Nothing Then
            DataOwner = GetCurrentSite()
        End If
    End Sub
    Protected Overrides Sub OnSaved()
        MyBase.OnSaved()
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

#Region "==========Association============"
    Dim _PlanBudget01 As PlanBudget01
    <Association("PlanBudget01-PlanBudgetDetail01s")> _
    <VisibleInDetailView(False), VisibleInListView(False)> _
    <Index(0)> _
    Public Property PlanBudget01() As PlanBudget01
        Get
            Return _PlanBudget01
        End Get
        Set(ByVal value As PlanBudget01)
            SetPropertyValue(Of PlanBudget01)("PlanBudget01", _PlanBudget01, value)
        End Set
    End Property
#End Region

    '================( ส่วนของ ส่วนกลาง) ====================================

    Private _ActivityNameNo As String
    <XafDisplayName("รายการที่")> _
    <VisibleInDetailView(False), VisibleInListView(True)> _
    <Appearance("DISACTNo", Enabled:=False, context:="ListView")> _
    Public Property ActivityNameNo() As String
        Get
            Return _ActivityNameNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue("ActivityNameNo", _ActivityNameNo, value)
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

    '========================== (ส่วนของ ศูนย์ ) =============================

    Private _PlanQuarter1 As Double
    <XafDisplayName("แผนไตรมาส 1 (ต.ค. - ธ.ค.)")> _
    <PersistentAlias("PlanBudgetDetailMonth01s.Sum(PlanQuarter1)")> _
    <Appearance("VisiblePlan1", Visibility:=Editors.ViewItemVisibility.Hide, Criteria:="ActivityName = 'งบประมาณจัดสรร'", Context:="DetailView")> _
          <VisibleInDetailView(False), VisibleInListView(False)> _
    <ImmediatePostData> _
    Public ReadOnly Property PlanQuarter1() As Double
        Get
            Try
                Dim tempObject As Object
                tempObject = EvaluateAlias("PlanQuarter1")
                Return CDbl(tempObject)
            Catch ex As Exception
                Return 0
            End Try
        End Get
    End Property

    Private _BudgetQuarter1 As Double
    <XafDisplayName("งบประมาณจัดสรร (ต.ค. - ธ.ค.)")> _
    <PersistentAlias("PlanBudgetDetailMonth01s.Sum(BudgetQuarter1)")> _
    <Appearance("VisibleBudget1", Visibility:=Editors.ViewItemVisibility.Hide, Criteria:="ActivityName <> 'งบประมาณจัดสรร'", Context:="DetailView")> _
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

    Private _PlanQuarter2 As Double
    <XafDisplayName("แผนไตรมาส 2 (ม.ค. - มี.ค.)")> _
    <PersistentAlias("PlanBudgetDetailMonth01s.Sum(PlanQuarter2)")> _
     <Appearance("VisiblePlan2", Visibility:=Editors.ViewItemVisibility.Hide, Criteria:="ActivityName = 'งบประมาณจัดสรร'", Context:="DetailView")> _
    <VisibleInDetailView(False), VisibleInListView(False)> _
    <ImmediatePostData> _
    Public ReadOnly Property PlanQuarter2() As Double
        Get
            Try
                Dim tempObject As Object
                tempObject = EvaluateAlias("PlanQuarter2")
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
    <Appearance("VisibleBudget2", Visibility:=Editors.ViewItemVisibility.Hide, Criteria:="ActivityName <> 'งบประมาณจัดสรร'", Context:="DetailView")> _
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

    Private _PlanQuarter3 As Double
    <XafDisplayName("แผนไตรมาส 3 (เม.ย. - มิ.ย.)")> _
    <PersistentAlias("PlanBudgetDetailMonth01s.Sum(PlanQuarter3)")> _
    <VisibleInDetailView(False), VisibleInListView(False)> _
    <Appearance("VisiblePlan3", Visibility:=Editors.ViewItemVisibility.Hide, Criteria:="ActivityName = 'งบประมาณจัดสรร'", Context:="DetailView")> _
    <ImmediatePostData> _
    Public ReadOnly Property PlanQuarter3() As Double
        Get
            Try
                Dim tempObject As Object
                tempObject = EvaluateAlias("PlanQuarter3")
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
    <Appearance("VisibleBudget3", Visibility:=Editors.ViewItemVisibility.Hide, Criteria:="ActivityName <> 'งบประมาณจัดสรร'", Context:="DetailView")> _
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

    Private _PlanQuarter4 As Double
    <XafDisplayName("แผนไตรมาส 4 (ก.ค. - ก.ย.)")> _
    <PersistentAlias("PlanBudgetDetailMonth01s.Sum(PlanQuarter4)")> _
    <VisibleInDetailView(False), VisibleInListView(False)> _
    <Appearance("VisiblePlan4", Visibility:=Editors.ViewItemVisibility.Hide, Criteria:="ActivityName = 'งบประมาณจัดสรร'", Context:="DetailView")> _
    <ImmediatePostData> _
    Public ReadOnly Property PlanQuarter4() As Double
        Get
            Try
                Dim tempObject As Object
                tempObject = EvaluateAlias("PlanQuarter4")
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
    <Appearance("VisibleBudget4", Visibility:=Editors.ViewItemVisibility.Hide, Criteria:="ActivityName <> 'งบประมาณจัดสรร'", Context:="DetailView")> _
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
            Return (PlanQuarter1 + PlanQuarter2 + PlanQuarter3 + PlanQuarter4)
        End Get
    End Property

    <XafDisplayName("รวมทั้งสิ้น งบประมาณ"), VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)> _
    Public ReadOnly Property SumBudget As Double
        Get
            Return (BudgetQuarter1 + BudgetQuarter2 + BudgetQuarter3 + BudgetQuarter4)
        End Get
    End Property

    Private _PlaningStatus As StausAppove
    <XafDisplayName("สถานะ")> _
    <Index(10)> _
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
    <Association("PlanBudgetDetail01-PlanBudgetDetailMonth01", GetType(PlanBudgetDetailMonth01))> _
    <XafDisplayName("แผนการดำเนินงานและผลการใช้จ่ายงบประมาณ (ศูนย์)")> _
    <DevExpress.Xpo.Aggregated>
    Public ReadOnly Property PlanBudgetDetailMonth01s() As XPCollection(Of PlanBudgetDetailMonth01)
        Get
            Return GetCollection(Of PlanBudgetDetailMonth01)("PlanBudgetDetailMonth01s")
        End Get
    End Property
#End Region

#Region "==========Association============"
    <Association("PlanBudgetDetail01-ResultBudget01", GetType(ResultBudget01))> _
    <XafDisplayName("ผลการดำเนินงานและผลการใช้จ่ายงบประมาณ (ศูนย์)")> _
    <DevExpress.Xpo.Aggregated>
    Public ReadOnly Property ResultBudget01s() As XPCollection(Of ResultBudget01)
        Get
            Return GetCollection(Of ResultBudget01)("ResultBudget01s")
        End Get
    End Property
#End Region

    '<Action(Caption:="My UI Action", ConfirmationMessage:="Are you sure?", ImageName:="Attention", AutoCommit:=True)> _
    'Public Sub ActionMethod()
    '    ' Trigger a custom business logic for the current record in the UI (http://documentation.devexpress.com/#Xaf/CustomDocument2619).
    '    Me.PersistentProperty = "Paid"
    'End Sub
End Class

