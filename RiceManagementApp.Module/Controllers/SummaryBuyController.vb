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
Partial Public Class SummaryBuyController
    Inherits ViewController
    Public Sub New()
        InitializeComponent()
        TargetObjectType = GetType(SummaryBuy)
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

    Private Sub SummaryBuyApproveAction_Execute(sender As Object, e As SimpleActionExecuteEventArgs) Handles SummaryBuyApproveAction.Execute
        If TypeOf View.CurrentObject Is SummaryBuy Then
            Dim objToSubmit As SummaryBuy = CType(View.CurrentObject, SummaryBuy)
            If objToSubmit.MarkAsComplete Then
                View.ObjectSpace.CommitChanges()
                View.ObjectSpace.Refresh()
            End If
        End If
    End Sub

    Private Sub SummaryBuyCancelAction_Execute(sender As Object, e As SimpleActionExecuteEventArgs) Handles SummaryBuyCancelAction.Execute
        If TypeOf View.CurrentObject Is SummaryBuy Then
            Dim objToSubmit As SummaryBuy = CType(View.CurrentObject, SummaryBuy)
            If objToSubmit.MarkCancle Then
                View.ObjectSpace.CommitChanges()
                View.ObjectSpace.Refresh()
            End If
        End If
    End Sub
End Class
