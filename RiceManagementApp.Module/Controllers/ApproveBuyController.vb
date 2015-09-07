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

Partial Public Class ApproveBuyController
    Inherits ViewController
    Public Sub New()
        InitializeComponent()
        TargetObjectType = GetType(IApproveBuy)
        TargetViewNesting = Nesting.Root
    End Sub
    Protected Overrides Sub OnActivated()
        MyBase.OnActivated()
       
    End Sub
    Protected Overrides Sub OnViewControlsCreated()
        MyBase.OnViewControlsCreated()
    End Sub
    Protected Overrides Sub OnDeactivated()
        MyBase.OnDeactivated()
    End Sub

    Private Sub ApproveBuyApproveAction_Execute(sender As Object, e As SimpleActionExecuteEventArgs) Handles ApproveBuyApproveAction.Execute
        If TypeOf View.CurrentObject Is IApproveBuy Then
            Dim objToSubmit As IApproveBuy = CType(View.CurrentObject, IApproveBuy)
            If objToSubmit.DoApprove Then
                View.ObjectSpace.CommitChanges()
                View.ObjectSpace.Refresh()
            End If
        End If
    End Sub

    Private Sub ApproveBuyCancelAction_Execute(sender As Object, e As SimpleActionExecuteEventArgs) Handles ApproveBuyCancelAction.Execute
        If TypeOf View.CurrentObject Is IApproveBuy Then
            Dim objToSubmit As IApproveBuy = CType(View.CurrentObject, IApproveBuy)
            If objToSubmit.DoCancel Then
                View.ObjectSpace.CommitChanges()
                View.ObjectSpace.Refresh()
            End If
        End If
    End Sub

    Private Sub ApproveBuyFinishAction_Execute(sender As Object, e As SimpleActionExecuteEventArgs) Handles ApproveBuyFinishAction.Execute
        If TypeOf View.CurrentObject Is IApproveBuy Then
            Dim objToSubmit As IApproveBuy = CType(View.CurrentObject, IApproveBuy)
            If objToSubmit.DoFinish Then
                View.ObjectSpace.CommitChanges()
                View.ObjectSpace.Refresh()
            End If
        End If
    End Sub
End Class
