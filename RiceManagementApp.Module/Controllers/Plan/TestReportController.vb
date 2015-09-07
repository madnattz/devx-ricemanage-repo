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
Imports RiceManagementApp.Module.ServiceSubmitReportData

Partial Public Class TestReportController
    Inherits ViewController

    Public Sub New()
        InitializeComponent()
        TargetObjectType = GetType(SubmitPlanReport)
        TargetViewType = ViewType.DetailView
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

    Private Sub SimpleAction1_Execute(sender As Object, e As SimpleActionExecuteEventArgs) Handles SimpleAction1.Execute
        Dim objService As New ServiceSubmitReportData.RiceServiceSoapClient
    End Sub
End Class
