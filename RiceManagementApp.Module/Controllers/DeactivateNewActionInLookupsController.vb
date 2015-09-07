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

Partial Public Class DeactivateNewActionInLookupsController
    Inherits ViewController
    Public Sub New()
        InitializeComponent()
        TargetViewType = ViewType.ListView
    End Sub
    Protected Overrides Sub OnActivated()
        MyBase.OnActivated()
    End Sub
    Protected Overrides Sub OnViewControlsCreated()
        MyBase.OnViewControlsCreated()
        AddHandler Frame.TemplateChanged, AddressOf Frame_TemplateChanged
        If Frame.Template IsNot Nothing Then
            HideNewAction()
        End If
    End Sub
    Private Sub Frame_TemplateChanged(ByVal sender As Object, ByVal e As EventArgs)
        HideNewAction()
    End Sub
    Private Sub HideNewAction()
        If IsLookupTemplate() Then
            Dim controller As NewObjectViewController = Frame.GetController(Of NewObjectViewController)()
            controller.NewObjectAction.Active.SetItemValue("LookupListView", False)
        End If
    End Sub
    Protected Overrides Sub OnDeactivated()
        MyBase.OnDeactivated()
        RemoveHandler Frame.TemplateChanged, AddressOf Frame_TemplateChanged
    End Sub
    Protected MustOverride Function IsLookupTemplate() As Boolean
End Class

Public Class WinDeactivateNewActionInLookupsController
    Inherits DeactivateNewActionInLookupsController

    Protected Overrides Function IsLookupTemplate() As Boolean
        Return TypeOf Frame.Template Is ILookupPopupFrameTemplate
    End Function
End Class

