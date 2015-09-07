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

Partial Public Class ctlReserveSeedSearchPanel
    Inherits ViewController
    Public Sub New()
        InitializeComponent()
        TargetObjectType = GetType(ReserveSeed)
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
    Dim obj As SearchReserveSeed
    Private Sub ActSearchReceiveSeed_CustomizePopupWindowParams(sender As Object, e As CustomizePopupWindowParamsEventArgs) Handles ActSearchReserveSeed.CustomizePopupWindowParams
        objectSpaceInternal = Application.CreateObjectSpace()
        obj = objectSpaceInternal.CreateObject(Of SearchReserveSeed)()
        Dim dv As DetailView = Application.CreateDetailView(objectSpaceInternal, obj)
        dv.ViewEditMode = DevExpress.ExpressApp.Editors.ViewEditMode.Edit
        e.View = dv

    End Sub

    Private Sub ActSearchReceiveSeed_Execute(sender As Object, e As PopupWindowShowActionExecuteEventArgs) Handles ActSearchReserveSeed.Execute
        Dim crt As String = "[ReserveSeedDetails][[Oid] IS NOT NULL"

        If obj.Plant IsNot Nothing Then
            crt &= " and [Plant.PlantName]='" & obj.Plant.PlantName & "'"
        End If

        If obj.SeedType IsNot Nothing Then
            crt &= " and [SeedType.SeedName]='" & obj.SeedType.SeedName & "'"
        End If
        If obj.SeedClass IsNot Nothing Then
            crt &= " and [SeedClass.ClassName]='" & obj.SeedClass.ClassName & "'"
        End If
        crt &= "]"

        If obj.ReserveNo <> "" Then
            crt &= " and [ReserveNo]='" & obj.ReserveNo & "'"
        End If

        If obj.MemberName <> "" Then
            crt &= " and [Member.MemberLookupName] like '%" & obj.MemberName.Replace("'", "''") & "%'"
        End If

        If Not ConvertToDate(obj.StartDate).Equals(Date.MinValue) And _
            ConvertToDate(obj.EndDate).Equals(Date.MinValue) Then
            crt &= " and [ReserveDateTime] >= '" & obj.StartDate.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-US")) & "'"
        End If

        If ConvertToDate(obj.StartDate).Equals(Date.MinValue) And _
           Not ConvertToDate(obj.EndDate).Equals(Date.MinValue) Then
            crt &= " and [ReserveDateTime] <= '" & obj.EndDate.AddDays(1).AddMinutes(-1).ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-US")) & "'"
        End If

        If Not ConvertToDate(obj.StartDate).Equals(Date.MinValue) And _
          Not ConvertToDate(obj.EndDate).Equals(Date.MinValue) Then
            crt &= " and [ReserveDateTime] >= '" & obj.StartDate.ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-US")) & "'"
            crt &= " and [ReserveDateTime] <= '" & obj.EndDate.AddDays(1).AddMinutes(-1).ToString("yyyy-MM-dd HH:mm:ss", New System.Globalization.CultureInfo("en-US")) & "'"
        End If

        If obj.Status <> PublicEnum.SimsStatusSearch.None Then
            crt &= " and [Status]=" & obj.Status
        End If

        If (TypeOf View Is ListView) And (View.ObjectTypeInfo.Type Is GetType(ReserveSeed)) Then
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
