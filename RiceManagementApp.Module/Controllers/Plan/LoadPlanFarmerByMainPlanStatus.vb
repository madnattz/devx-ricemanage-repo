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

Partial Public Class LoadPlanFarerByMainPlanStatus
    Inherits ViewController
    Public Sub New()
        InitializeComponent()
        TargetObjectType = GetType(PlanFarmer)
        TargetViewType = ViewType.ListView
        TargetViewId = "PlanFarmer_ListView"
        ' Target required Views (via the TargetXXX properties) and create their Actions.
    End Sub
    Protected Overrides Sub OnActivated()
        MyBase.OnActivated()

        Dim item1 As ChoiceActionItem = New ChoiceActionItem("อยู่ระหว่างดำเนินการ", 0)
        item1.ImageName = "State_Validation_Valid"

        Dim item2 As ChoiceActionItem = New ChoiceActionItem("เสร็จสิ้นกระบวนการ", 1)
        item2.ImageName = "State_Task_Completed"

        Dim item3 As ChoiceActionItem = New ChoiceActionItem("ทั้งหมด", -1)
        item3.ImageName = "BO_List"

        PlanFarmerStatusAction.Items.Clear()

        PlanFarmerStatusAction.Items.Add(item1)
        PlanFarmerStatusAction.Items.Add(item2)
        PlanFarmerStatusAction.Items.Add(item3)
        PlanFarmerStatusAction.SelectedIndex = 0

        Search(PlanFarmerStatusAction.SelectedItem.Data)

    End Sub
    Protected Overrides Sub OnViewControlsCreated()
        MyBase.OnViewControlsCreated()
        ' Access and customize the target View control.
    End Sub
    Protected Overrides Sub OnDeactivated()
        ' Unsubscribe from previously subscribed events and release other references and resources.
        MyBase.OnDeactivated()
    End Sub

    Private Sub GrowPlanStatusAction_Execute(sender As Object, e As SingleChoiceActionExecuteEventArgs) Handles PlanFarmerStatusAction.Execute, PlanFarmerStatusAction.Execute
        Search(e.SelectedChoiceActionItem.Data)
    End Sub

    Public Sub Search(data As String)
        Try
            If data <> -1 Then
                Dim ctr As String = "PlanFarmerGroup.MainPlan.PlanStatus=?"
                If (TypeOf View Is ListView) And (View.ObjectTypeInfo.Type Is GetType(PlanFarmer)) Then
                    CType(View, ListView).CollectionSource.Criteria("GrowPlanFilter1") = CriteriaOperator.Parse(ctr, data)
                End If
            Else
                If (TypeOf View Is ListView) And (View.ObjectTypeInfo.Type Is GetType(PlanFarmer)) Then
                    CType(View, ListView).CollectionSource.Criteria.Clear()
                End If

            End If
        Catch ex As Exception
        End Try
    End Sub

End Class
