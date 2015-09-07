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

Partial Public Class ctlTransferSeedSearchPanel
    Inherits ViewController
    Public Sub New()
        InitializeComponent()
        TargetObjectType = GetType(TransferSeed)
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
    Dim obj As SearchTransferSeed
    Private Sub ActSearchReceiveSeed_CustomizePopupWindowParams(sender As Object, e As CustomizePopupWindowParamsEventArgs) Handles ActSearchTransferSeed.CustomizePopupWindowParams
        objectSpaceInternal = Application.CreateObjectSpace()
        obj = objectSpaceInternal.CreateObject(Of SearchTransferSeed)()
        Dim dv As DetailView = Application.CreateDetailView(objectSpaceInternal, obj)
        dv.ViewEditMode = DevExpress.ExpressApp.Editors.ViewEditMode.Edit
        e.View = dv

    End Sub

    Private Sub ActSearchReceiveSeed_Execute(sender As Object, e As PopupWindowShowActionExecuteEventArgs) Handles ActSearchTransferSeed.Execute
        Dim crt As String = "[TransferSeedDetails][[Oid] IS NOT NULL"
        '[��¡�á�è������紾ѹ���][[�������紾ѹ���.�ת.���;ת] = '����']

        If obj.Plant IsNot Nothing Then
            crt &= " and [SeedProduct.Plant.PlantName]='" & obj.Plant.PlantName & "'"
        End If
        If obj.SeedStatus IsNot Nothing Then
            crt &= " and [SeedProduct.SeedStatus.SeedStatusName]='" & obj.SeedStatus.SeedStatusName & "'"
        End If
        If obj.SeedType IsNot Nothing Then
            crt &= " and [SeedProduct.SeedType.SeedName]='" & obj.SeedType.SeedName & "'"
        End If
        If obj.SeedClass IsNot Nothing Then
            crt &= " and [SeedProduct.SeedClass.ClassName]='" & obj.SeedClass.ClassName & "'"
        End If
        If obj.Season IsNot Nothing Then
            crt &= " and [SeedProduct.Season.SeasonName]='" & obj.Season.SeasonName & "'"
        End If
        If obj.MoneyType IsNot Nothing Then
            crt &= " and [SeedProduct.MoneyType.MoneyTypeName]='" & obj.MoneyType.MoneyTypeName & "'"
        End If
        If obj.SeedYear <> "" Then
            crt &= " and [SeedProduct.SeedYear]='" & obj.SeedYear & "'"
        End If
        If obj.LotNo <> 0 Then
            crt &= " and [SeedProduct.LotNo]='" & obj.LotNo & "'"
        End If
        crt &= "]"

        If obj.ReceiveNo <> "" Then
            crt &= " and [TransferNo]='" & obj.ReceiveNo & "'"
        End If
        If Not ConvertToDate(obj.StartDate).Equals(Date.MinValue) And _
            ConvertToDate(obj.EndDate).Equals(Date.MinValue) Then
            crt &= " and [TransferDate] >= '" & obj.StartDate.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-US")) & "'"
        End If

        If ConvertToDate(obj.StartDate).Equals(Date.MinValue) And _
           Not ConvertToDate(obj.EndDate).Equals(Date.MinValue) Then
            crt &= " and [TransferDate] <= '" & obj.EndDate.AddDays(1).AddMinutes(-1).ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-US")) & "'"
        End If

        If Not ConvertToDate(obj.StartDate).Equals(Date.MinValue) And _
          Not ConvertToDate(obj.EndDate).Equals(Date.MinValue) Then
            crt &= " and [TransferDate] >= '" & obj.StartDate.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-US")) & "'"
            crt &= " and [TransferDate] <= '" & obj.EndDate.AddDays(1).AddMinutes(-1).ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-US")) & "'"
        End If

        If obj.Status <> PublicEnum.SimsStatusSearch.None Then
            crt &= " and [SendStatus]=" & obj.Status
        End If

        If (TypeOf View Is ListView) And (View.ObjectTypeInfo.Type Is GetType(TransferSeed)) Then
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
