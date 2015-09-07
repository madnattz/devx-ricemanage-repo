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

' For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppViewControllertopic.
Partial Public Class SimsApproveController
    Inherits ViewController
    Public Sub New()
        InitializeComponent()
        TargetObjectType = GetType(IApproveAble)
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

    Private Sub ApproveAction_Execute(sender As Object, e As SimpleActionExecuteEventArgs) Handles SimsApproveAction.Execute
        If TypeOf View.CurrentObject Is IApproveAble Then
            Dim objToSubmit As IApproveAble = CType(View.CurrentObject, IApproveAble)
            If objToSubmit.DoApprove Then
                View.ObjectSpace.CommitChanges()
                View.ObjectSpace.Refresh()

                'Dim os1 As IObjectSpace = Application.CreateObjectSpace
                'Application.MainWindow.SetView(Application.CreateListView(os1, objToSubmit.GetType, True))

            End If
        End If
    End Sub

    Private Sub CancelAction_Execute(sender As Object, e As SimpleActionExecuteEventArgs) Handles SimsCancelAction.Execute
        If TypeOf View.CurrentObject Is IApproveAble Then
            Dim objToSubmit As IApproveAble = CType(View.CurrentObject, IApproveAble)
            If objToSubmit.DoCancel Then
                View.ObjectSpace.CommitChanges()
                View.ObjectSpace.Refresh()

                'Dim os1 As IObjectSpace = Application.CreateObjectSpace
                'Application.MainWindow.SetView(Application.CreateListView(os1, objToSubmit.GetType, True))

            End If
        End If
    End Sub

End Class
