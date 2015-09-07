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
Imports DevExpress.ExpressApp.Security
Imports DevExpress.ExpressApp.Security.Strategy

Partial Public Class HideItemWindowController
    Inherits WindowController
    Private navigationController As ShowNavigationItemController
    Public Sub New()
        InitializeComponent()
        TargetWindowType = WindowType.Main
    End Sub
    Protected Overrides Sub OnActivated()
        MyBase.OnActivated()
    End Sub
    Protected Overrides Sub OnFrameAssigned()
        MyBase.OnFrameAssigned()
        navigationController = Frame.GetController(Of ShowNavigationItemController)()
        If navigationController IsNot Nothing Then
            AddHandler navigationController.ItemsInitialized, AddressOf HideItemWindowController_ItemsInitialized
        End If
    End Sub
    Protected Overrides Sub OnDeactivated()
        If navigationController IsNot Nothing Then
            RemoveHandler navigationController.ItemsInitialized, AddressOf HideItemWindowController_ItemsInitialized
            navigationController = Nothing
        End If
        MyBase.OnDeactivated()
    End Sub
    Private Sub HideItemWindowController_ItemsInitialized(ByVal sender As Object, ByVal e As EventArgs)
        Dim currentUser As User = TryCast(SecuritySystem.CurrentUser, User)
        If currentUser IsNot Nothing Then

            VisibleNavigationItemById(navigationController.ShowNavigationItemAction.Items, "PlanNav", False)
            VisibleNavigationItemById(navigationController.ShowNavigationItemAction.Items, "AuditNav", False)
            VisibleNavigationItemById(navigationController.ShowNavigationItemAction.Items, "BuySeedNav", False)
            VisibleNavigationItemById(navigationController.ShowNavigationItemAction.Items, "AccountNav", False)
            VisibleNavigationItemById(navigationController.ShowNavigationItemAction.Items, "InventoryNav", False)
            VisibleNavigationItemById(navigationController.ShowNavigationItemAction.Items, "FactoryNav", False)
            VisibleNavigationItemById(navigationController.ShowNavigationItemAction.Items, "SaleSeedNav", False)
            VisibleNavigationItemById(navigationController.ShowNavigationItemAction.Items, "AdminNav", False)
            VisibleNavigationItemById(navigationController.ShowNavigationItemAction.Items, "Reports", False)
            VisibleNavigationItemById(navigationController.ShowNavigationItemAction.Items, "Default", False)
            VisibleNavigationItemById(navigationController.ShowNavigationItemAction.Items, " ¡“§¡œ", False)

            For Each role As Role In currentUser.Roles
                Select Case role.Name.Trim.ToLower
                    Case "plan"
                        VisibleNavigationItemById(navigationController.ShowNavigationItemAction.Items, "PlanNav", True)
                        VisibleNavigationItemById(navigationController.ShowNavigationItemAction.Items, "BuySeedNav", True)
                    Case "audit"
                        VisibleNavigationItemById(navigationController.ShowNavigationItemAction.Items, "AuditNav", True)
                    Case "factory"
                        VisibleNavigationItemById(navigationController.ShowNavigationItemAction.Items, "FactoryNav", True)
                    Case "account"
                        VisibleNavigationItemById(navigationController.ShowNavigationItemAction.Items, "AccountNav", True)
                    Case "inventory"
                        VisibleNavigationItemById(navigationController.ShowNavigationItemAction.Items, "InventoryNav", True)
                    Case "sale"
                        VisibleNavigationItemById(navigationController.ShowNavigationItemAction.Items, "SaleSeedNav", True)
                    Case "association"
                        VisibleNavigationItemById(navigationController.ShowNavigationItemAction.Items, " ¡“§¡œ", True)
                    Case "administrator"
                        VisibleNavigationItemById(navigationController.ShowNavigationItemAction.Items, "PlanNav", True)
                        VisibleNavigationItemById(navigationController.ShowNavigationItemAction.Items, "AuditNav", True)
                        VisibleNavigationItemById(navigationController.ShowNavigationItemAction.Items, "BuySeedNav", True)
                        VisibleNavigationItemById(navigationController.ShowNavigationItemAction.Items, "AccountNav", True)
                        VisibleNavigationItemById(navigationController.ShowNavigationItemAction.Items, "InventoryNav", True)
                        VisibleNavigationItemById(navigationController.ShowNavigationItemAction.Items, "FactoryNav", True)
                        VisibleNavigationItemById(navigationController.ShowNavigationItemAction.Items, "SaleSeedNav", True)
                        VisibleNavigationItemById(navigationController.ShowNavigationItemAction.Items, "AdminNav", True)
                        VisibleNavigationItemById(navigationController.ShowNavigationItemAction.Items, "Reports", True)
                        VisibleNavigationItemById(navigationController.ShowNavigationItemAction.Items, "Default", True)
                        VisibleNavigationItemById(navigationController.ShowNavigationItemAction.Items, " ¡“§¡œ", True)
                End Select
            Next role

        End If
    End Sub
    Private Sub VisibleNavigationItemById(ByVal items As ChoiceActionItemCollection, ByVal navigationItemId As String, Visible As Boolean)
        For Each item As ChoiceActionItem In items
            If item.Id = navigationItemId Then
                item.Active("InactiveForUsersRole") = Visible
                Return
            End If
            VisibleNavigationItemById(item.Items, navigationItemId, Visible)
        Next item
    End Sub
End Class
