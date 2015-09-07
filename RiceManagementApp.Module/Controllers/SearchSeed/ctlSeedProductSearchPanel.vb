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

Partial Public Class ctlSeedProductSearchPanel
    Inherits ViewController
    Public Sub New()
        InitializeComponent()
        TargetObjectType = GetType(SeedProduct)
        TargetViewType = ViewType.ListView
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

    Dim objectSpaceInternal As IObjectSpace
    Dim obj As SearchSeedProduct
    Private Sub ActSearchReceiveSeed_CustomizePopupWindowParams(sender As Object, e As CustomizePopupWindowParamsEventArgs) Handles ActSearchSeedProduct.CustomizePopupWindowParams
        objectSpaceInternal = Application.CreateObjectSpace()
        obj = objectSpaceInternal.CreateObject(Of SearchSeedProduct)()
        Dim dv As DetailView = Application.CreateDetailView(objectSpaceInternal, obj)
        dv.ViewEditMode = DevExpress.ExpressApp.Editors.ViewEditMode.Edit
        e.View = dv

    End Sub

    Private Sub ActSearchReceiveSeed_Execute(sender As Object, e As PopupWindowShowActionExecuteEventArgs) Handles ActSearchSeedProduct.Execute
        Dim crt As String = "[Oid] IS NOT NULL "

        If obj.Plant IsNot Nothing Then
            crt &= " and [Plant.PlantName]='" & obj.Plant.PlantName & "'"
        End If
        If obj.SeedStatus IsNot Nothing Then
            crt &= " and [SeedStatus.SeedStatusName]='" & obj.SeedStatus.SeedStatusName & "'"
        End If
        If obj.SeedType IsNot Nothing Then
            crt &= " and [SeedType.SeedName]='" & obj.SeedType.SeedName & "'"
        End If
        If obj.SeedClass IsNot Nothing Then
            crt &= " and [SeedClass.ClassName]='" & obj.SeedClass.ClassName & "'"
        End If
        If obj.Season IsNot Nothing Then
            crt &= " and [Season.SeasonName]='" & obj.Season.SeasonName & "'"
        End If
        If obj.MoneyType IsNot Nothing Then
            crt &= " and [MoneyType.MoneyTypeName]='" & obj.MoneyType.MoneyTypeName & "'"
        End If
        If obj.SeedYear <> "" Then
            crt &= " and [SeedYear]='" & obj.SeedYear & "'"
        End If
        If obj.LotNo <> 0 Then
            crt &= " and [LotNo]='" & obj.LotNo & "'"
        End If

        If obj.IsSearchBy = True Then
            Dim ctrType As String = ">"
            If obj.SearchCriteria = SearchSeedProduct.SearchSeedProductCriteria.MoreThan Then
                ctrType = ">"
            ElseIf obj.SearchCriteria = SearchSeedProduct.SearchSeedProductCriteria.LessThan Then
                ctrType = "<"
            Else
                ctrType = "="
            End If

            crt &= " and [TotalStockAmount] " & ctrType & " " & obj.Quantity
        End If

        If (TypeOf View Is ListView) And (View.ObjectTypeInfo.Type Is GetType(SeedProduct)) Then
            CType(View, ListView).CollectionSource.Criteria("Filter1") = CriteriaOperator.Parse(crt)
        End If
        objectSpaceInternal.Rollback()
        View.ObjectSpace.Refresh()

    End Sub

    Public Function ConvertToDate(obj As Object) As Date
        Try
            Return Convert.ToDateTime(obj)
        Catch ex As Exception
            Return Date.MinValue
        End Try
    End Function

End Class
