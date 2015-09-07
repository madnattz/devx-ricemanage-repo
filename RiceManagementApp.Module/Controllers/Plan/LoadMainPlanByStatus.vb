Imports Microsoft.VisualBasic
Imports System
Imports System.Linq
Imports System.Text
Imports DevExpress.ExpressApp
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports DevExpress.Persistent.Base
Imports DevExpress.ExpressApp.Utils
Imports DevExpress.ExpressApp.Layout
Imports DevExpress.ExpressApp.Actions
Imports DevExpress.ExpressApp.Editors
Imports DevExpress.ExpressApp.Templates
Imports DevExpress.Persistent.Validation
Imports DevExpress.ExpressApp.SystemModule
Imports DevExpress.ExpressApp.Model.NodeGenerators

Partial Public Class LoadMainPlanByStatus
    Inherits ViewController

    Public Sub New()
        InitializeComponent()
        TargetObjectType = GetType(MainPlan)
        TargetViewType = ViewType.ListView
    End Sub

    Protected Overrides Sub OnActivated()
        MyBase.OnActivated()

        Dim item1 As ChoiceActionItem = New ChoiceActionItem("อยู่ระหว่างดำเนินการ", 0)
        item1.ImageName = "State_Validation_Valid"

        Dim item2 As ChoiceActionItem = New ChoiceActionItem("เสร็จสิ้นกระบวนการ", 1)
        item2.ImageName = "State_Task_Completed"

        Dim item3 As ChoiceActionItem = New ChoiceActionItem("ทั้งหมด", -1)
        item3.ImageName = "BO_List"

        MainPlanStatusAction.Items.Clear()

        MainPlanStatusAction.Items.Add(item1)
        MainPlanStatusAction.Items.Add(item2)
        MainPlanStatusAction.Items.Add(item3)
        MainPlanStatusAction.SelectedIndex = 0

        Search(MainPlanStatusAction.SelectedItem.Data)

    End Sub
    Protected Overrides Sub OnViewControlsCreated()
        MyBase.OnViewControlsCreated()
    End Sub
    Protected Overrides Sub OnDeactivated()
        MyBase.OnDeactivated()
    End Sub

    Private Sub MainPlanStatusAction_Execute(sender As Object, e As SingleChoiceActionExecuteEventArgs) Handles MainPlanStatusAction.Execute
        Search(e.SelectedChoiceActionItem.Data)
    End Sub

    Public Sub Search(data As String)
        Try
            If data <> -1 Then
                Dim ctr As String = "PlanStatus=?"
                If (TypeOf View Is ListView) And (View.ObjectTypeInfo.Type Is GetType(MainPlan)) Then
                    CType(View, ListView).CollectionSource.Criteria("Filter1") = CriteriaOperator.Parse(ctr, data)
                End If
            Else
                If (TypeOf View Is ListView) And (View.ObjectTypeInfo.Type Is GetType(MainPlan)) Then
                    CType(View, ListView).CollectionSource.Criteria.Clear()
                End If

            End If
        Catch ex As Exception
        End Try
    End Sub

End Class
