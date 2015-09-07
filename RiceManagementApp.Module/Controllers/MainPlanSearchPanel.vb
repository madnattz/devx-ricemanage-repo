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
Imports DevExpress.Xpo
Imports DevExpress.Xpo.DB
Imports RiceManagementApp.Module.MainPlanSearchPanel
Imports DevExpress.Xpo.Helpers

Partial Public Class MainPlanSearchPanel
    Inherits ViewController
    Public Sub New()
        InitializeComponent()
        TargetObjectType = GetType(MainPlan)
        TargetViewType = ViewType.ListView
    End Sub
    Protected Overrides Sub OnActivated()
        MyBase.OnActivated()
        GetPlant()
        GetSeedType("")
        GetSeedClass()
        GetSeason()
        GetYear()
        GetLot()
    End Sub
    Protected Overrides Sub OnViewControlsCreated()
        MyBase.OnViewControlsCreated()
    End Sub
    Protected Overrides Sub OnDeactivated()
        MyBase.OnDeactivated()
    End Sub

    Public Sub GetPlant()

        Dim conn As String = Application.ConnectionString
        Dim cmd As String = "SELECT Oid,PlantName From Plant where Status = 0 order by PlantID asc "
        Dim ds As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(conn, CommandType.Text, cmd)

        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
            Dim Caption As String = ds.Tables(0).Rows(i).Item("PlantName")
            Dim item As ChoiceActionItem = New ChoiceActionItem(Caption, Caption)
            '//item.ImageName = "Action_Filter"
            PlantSearch.Items.Add(item)
        Next

    End Sub

    Public Sub GetSeedType(PlantName As String)

        Dim conn As String = Application.ConnectionString
        Dim cmd As String = ""
        If PlantName <> "" Then
            cmd = "SELECT Oid,SeedName From SeedType Where (Status = 0) and (Plant in (select Oid From Plant Where PlantName = '" & PlantName & "')) order by SeedID asc"
        Else
            cmd = "SELECT Oid,SeedName From SeedType order by SeedID asc "
        End If
        Dim ds As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(conn, CommandType.Text, cmd)
        SeedTypeSearch.Items.Clear()
        If ds.Tables(0).Rows.Count > 0 Then
            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                Dim Caption As String = ds.Tables(0).Rows(i).Item("SeedName")
                Dim item As ChoiceActionItem = New ChoiceActionItem(Caption, Caption)
                '//item.ImageName = "Action_Filter"
                SeedTypeSearch.Items.Add(item)
            Next
        Else
            SeedTypeSearch.Items.Add(New ChoiceActionItem("", ""))
        End If

    End Sub

    Public Sub GetSeedClass()

        Dim conn As String = Application.ConnectionString
        Dim cmd As String = "SELECT ClassName From SeedClass Where Status = 0 order by ClassID asc "
        Dim ds As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(conn, CommandType.Text, cmd)

        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
            Dim Caption As String = ds.Tables(0).Rows(i).Item("ClassName")
            Dim item As ChoiceActionItem = New ChoiceActionItem(Caption, Caption)
            '//item.ImageName = "Action_Filter"
            SeedClassSearch.Items.Add(item)
        Next

    End Sub

    Public Sub GetSeason()

        Dim conn As String = Application.ConnectionString
        Dim cmd As String = "SELECT SeasonName From Season Where Status = 0"
        Dim ds As DataSet = Microsoft.ApplicationBlocks.Data.SqlHelper.ExecuteDataset(conn, CommandType.Text, cmd)

        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
            Dim Caption As String = ds.Tables(0).Rows(i).Item("SeasonName")
            Dim item As ChoiceActionItem = New ChoiceActionItem(Caption, Caption)
            '//item.ImageName = "Action_Filter"
            SeasonSearch.Items.Add(item)
        Next
    End Sub

    Public Sub GetYear()
        For i As Integer = 0 To 5
            Dim Caption As String = Date.Now.Year + 543 - i + 1
            Dim item As ChoiceActionItem = New ChoiceActionItem(Caption, Caption)
            '//item.ImageName = "Action_Filter"
            SeedYearSearch.Items.Add(item) '
        Next
    End Sub

    Public Sub GetLot()
        For i As Integer = 1 To 5
            Dim Caption As String = i
            Dim item As ChoiceActionItem = New ChoiceActionItem(Caption, Caption)
            '//item.ImageName = "Action_Filter"
            LotSearch.Items.Add(item) '
        Next

    End Sub

    Private Sub MainPlanSearchButton_Execute(sender As Object, e As SimpleActionExecuteEventArgs) Handles MainPlanSearchButton.Execute
        Search()
    End Sub

    Sub Search()

        Dim ctr As String = " Oid IS NOT NULL"

        If PlantSearch.SelectedItem IsNot Nothing Then
            ctr &= " AND Plant.PlantName='" & PlantSearch.SelectedItem.Caption & "'"
        End If
        If SeedTypeSearch.SelectedItem IsNot Nothing Then
            ctr &= " AND SeedType.SeedName='" & SeedTypeSearch.SelectedItem.Caption & "'"
        End If
        If SeedClassSearch.SelectedItem IsNot Nothing Then
            ctr &= " AND SeedClass.ClassName='" & SeedClassSearch.SelectedItem.Caption & "'"
        End If
        If SeasonSearch.SelectedItem IsNot Nothing Then
            ctr &= " AND Season.SeasonName='" & SeasonSearch.SelectedItem.Caption & "'"
        End If
        If SeedYearSearch.SelectedItem IsNot Nothing Then
            ctr &= " AND SeedYear='" & SeedYearSearch.SelectedItem.Caption & "'"
        End If
        If LotSearch.SelectedItem IsNot Nothing Then
            ctr &= " AND Lot='" & LotSearch.SelectedItem.Caption & "'"
        End If

        Try
            If (TypeOf View Is ListView) And (View.ObjectTypeInfo.Type Is GetType(MainPlan)) Then
                Try
                    CType(View, ListView).CollectionSource.Criteria("Filter1") = CriteriaOperator.Parse(ctr)
                Catch ex As Exception

                End Try
            End If
        Catch ex As Exception
        End Try

    End Sub

    Private Sub SearchByPlanAction_Execute(sender As Object, e As SingleChoiceActionExecuteEventArgs) Handles PlantSearch.Execute
        GetSeedType(e.SelectedChoiceActionItem.Caption)
    End Sub

    Private Sub ClearSearch_Execute(sender As Object, e As SimpleActionExecuteEventArgs) Handles ClearSearch.Execute

        If PlantSearch.SelectedItem IsNot Nothing Then
            PlantSearch.SelectedItem.Items.Clear()
        End If

        If SeedTypeSearch.SelectedItem IsNot Nothing Then
            SeedTypeSearch.SelectedItem.Items.Clear()
        ElseIf SeedTypeSearch.Items(0).Caption = "" Then
            GetSeedType("")
        End If

        If SeedClassSearch.SelectedItem IsNot Nothing Then
            SeedClassSearch.SelectedItem.Items.Clear()
        End If

        If SeasonSearch.SelectedItem IsNot Nothing Then
            SeasonSearch.SelectedItem.Items.Clear()
        End If

        If SeedYearSearch.SelectedItem IsNot Nothing Then
            SeedYearSearch.SelectedItem.Items.Clear()
        End If

        If LotSearch.SelectedItem IsNot Nothing Then
            LotSearch.SelectedItem.Items.Clear()
        End If

        Search()

    End Sub

   
End Class
