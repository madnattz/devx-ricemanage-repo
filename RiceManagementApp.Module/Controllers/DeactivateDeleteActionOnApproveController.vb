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

Partial Public Class DeactivateDeleteActionOnApproveController
    Inherits ViewController
    Public Sub New()
        InitializeComponent()
        TargetObjectType = GetType(IApproveAble)
    End Sub
    Protected Overrides Sub OnActivated()
        MyBase.OnActivated()
        If TypeOf View.CurrentObject Is IApproveAble Then
            Frame.GetController(Of DevExpress.ExpressApp.SystemModule.DeleteObjectsViewController)().DeleteAction.Active.SetItemValue("ApprovedCantDelete", False)
        End If
        
    End Sub
    Protected Overrides Sub OnViewControlsCreated()
        MyBase.OnViewControlsCreated()
        ' Access and customize the target View control.
    End Sub
    Protected Overrides Sub OnDeactivated()
        ' Unsubscribe from previously subscribed events and release other references and resources.
        MyBase.OnDeactivated()
    End Sub
End Class
