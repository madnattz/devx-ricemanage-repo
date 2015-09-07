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

Partial Public Class BuySeedApproveController
    Inherits ViewController
    Public Sub New()
        InitializeComponent()
        TargetObjectType = GetType(IBuySeed)
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

    Private Sub BuyApproveAction_Execute(sender As Object, e As SimpleActionExecuteEventArgs) Handles BuyApproveAction.Execute
        If TypeOf View.CurrentObject Is IBuySeed Then
            Dim objToSubmit As IBuySeed = CType(View.CurrentObject, IBuySeed)
            If objToSubmit.DoApprove Then
                View.ObjectSpace.CommitChanges()
                View.ObjectSpace.Refresh()

            End If
        End If
    End Sub

    Private Sub BuyCancelAction_Execute(sender As Object, e As SimpleActionExecuteEventArgs) Handles BuyCancelAction.Execute
        If TypeOf View.CurrentObject Is IBuySeed Then
            Dim objToSubmit As IBuySeed = CType(View.CurrentObject, IBuySeed)
            If objToSubmit.DoCancel Then
                View.ObjectSpace.CommitChanges()
                View.ObjectSpace.Refresh()

            End If
        End If
    End Sub
End Class
