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
<NavigationItem("ทดสอบ")> _
<DefaultClassOptions(), ImageName("BO_Task"), XafDisplayName("งบประมาณ สงป 02")> _
Public Class DemoPlanBudget02 ' Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
    Public Sub New(ByVal session As Session)
        MyBase.New(session)

    End Sub
    Public Overrides Sub AfterConstruction()
        DataOwner = GetCurrentSite()
        MyBase.AfterConstruction()
        ' Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).
        '===เช็คปี งบประมาณ ===
        Dim Month As Integer = Now.Month
        If Month <= 10 Then
            Year = Today.Year + 543
        Else
            Year = Today.Year + 544
        End If
    End Sub

    Protected Overrides Sub OnSaving()
        MyBase.OnSaving()
        If Me.DataOwner Is Nothing Then
            DataOwner = GetCurrentSite()
        End If
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

    Private _Year As String
    <XafDisplayName("ปีงบประมาณ")> _
<Appearance("DisYear", Enabled:=False, Criteria:="[PlaningStatus] <> 'status0'", Context:="DetailView")> _
    Public Property Year() As String
        Get
            Return _Year
        End Get
        Set(ByVal value As String)
            SetPropertyValue("Year", _Year, value)
        End Set
    End Property

    Private _PlanBudget As Double
    <XafDisplayName("งบประมาณ")> _
    <VisibleInListView(True), VisibleInDetailView(True)> _
    <Appearance("DisBudget", Enabled:=False, Criteria:="[PlaningStatus] <> 'status0'", Context:="DetailView")> _
    <ImmediatePostData> _
    Public Property PlanBudget() As Double
        Get
            Return _PlanBudget
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("PlanBudget", _PlanBudget, value)
        End Set
    End Property

    Private _Subductionyears As Double
    <XafDisplayName("เงินงบประมาณที่กันไว้เบิกเหลื่อมปีที่ผ่านมา")> _
    <VisibleInListView(False), VisibleInDetailView(True)> _
    <ImmediatePostData> _
    Public Property Subductionyears() As Double
        Get
            Return _Subductionyears
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("Subductionyears", _Subductionyears, value)
        End Set
    End Property

    Private _NonBudget As Double
    <XafDisplayName("เงินนอกงบประมาณ")> _
    <VisibleInListView(False), VisibleInDetailView(True)> _
    <ImmediatePostData> _
    Public Property NonBudget() As Double
        Get
            Return _NonBudget
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("NonBudget", _NonBudget, value)
        End Set
    End Property

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

    Private _ActionRequest As ActRequest
    <XafDisplayName("สถานะขอเข้าร่วมโครงการ")> _
    <VisibleInDetailView(True), VisibleInListView(True)> _
    Public Property ActionRequest() As ActRequest
        Get
            Return _ActionRequest
        End Get
        Set(ByVal value As ActRequest)
            SetPropertyValue("ActionRequest", _ActionRequest, value)
        End Set
    End Property

#Region "==========Association============"
    <Association("PlanBudget02-PlanBudgetDetail02", GetType(DemoPlanBudgetDetail02))> _
    <XafDisplayName("รายการค่าใช้จ่าย")> _
    Public ReadOnly Property PlanBudget02Details() As XPCollection(Of DemoPlanBudgetDetail02)
        Get
            Return GetCollection(Of DemoPlanBudgetDetail02)("PlanBudget02Details")
        End Get
    End Property
#End Region

    <Action(Caption:="แสดงรายการค่าใช้จ่าย", ConfirmationMessage:="ท่านต้องการแสดงรายการค่าใช้จ่ายใช่หรือไม่?", ImageName:="BO_List", AutoCommit:=True)> _
    Sub LoadExpenseCat()
        If PlaningStatus = StausAppove.status0 Then
            Dim EXPs As New XPCollection(Of ExpenseCategories)(Session, CriteriaOperator.Parse("PublicStatus==?", 0))
            For Each row As ExpenseCategories In EXPs

                Dim Target As DemoPlanBudgetDetail02 = New DemoPlanBudgetDetail02(Session)
                Target.ExpenseID = row.ExpenseID
                Target.ExpenseName = row.ExpenseName
                Target.PlanBudget02 = Me
            Next
            PlaningStatus = StausAppove.Status1
        End If
    End Sub

    <Action(Caption:="อนุมัติ", ConfirmationMessage:="ยืนยันการอนุมัติใช่หรือไม่?", ImageName:="Action_Validation_Validate", AutoCommit:=True, TargetObjectsCriteria:="")> _
    Public Sub AppoveProject()
        If PlaningStatus = StausAppove.Status1 Then
            PlaningStatus = StausAppove.Status2
        End If
    End Sub

    <Action(Caption:="เข้าร่วม", ConfirmationMessage:="ยืนยันการเข้าร่วมใช่หรือไม่?", ImageName:="Action_Workflow_Activate", AutoCommit:=True, TargetObjectsCriteria:="")> _
    Public Sub DoneProject()
        If ActionRequest = ActRequest.Request Then
            ActionRequest = ActRequest.Done
        End If
    End Sub

    <Action(Caption:="ไม่เข้าร่วม", ConfirmationMessage:="ยืนยันการไม่เข้าร่วมใช่หรือไม่?", ImageName:="Action_Workflow_DeActivate", AutoCommit:=True, TargetObjectsCriteria:="")> _
    Public Sub ResponceProject()
        If ActionRequest = ActRequest.Request Or ActionRequest = ActRequest.Done Then
            ActionRequest = ActRequest.Responce
        End If
    End Sub
End Class
