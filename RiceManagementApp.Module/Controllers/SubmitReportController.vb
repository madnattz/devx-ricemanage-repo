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

Partial Public Class SubmitReportController
    Inherits ViewController
    Public Sub New()
        InitializeComponent()
        TargetViewType = ViewType.DetailView
        TargetObjectType = GetType(ISubmitReportAble)
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

    Private Sub SubmitReportAction_Execute(sender As Object, e As SimpleActionExecuteEventArgs) Handles SubmitReportAction.Execute
        If TypeOf View.CurrentObject Is ISubmitReportAble Then
            Dim objToSubmit As ISubmitReportAble = CType(View.CurrentObject, ISubmitReportAble)
            If objToSubmit.DoSubmitReport() Then
                View.ObjectSpace.CommitChanges()
                View.ObjectSpace.Refresh()
                MsgBox("ส่งข้อมูลรายงาน เรียบร้อยแล้ว", MsgBoxStyle.Information)
            Else
                MsgBox("เกิดข้อผิดพลาด ไม่สามารถส่งรายงานได้ กรุณาติดต่อผู้ดูแลระบบ", MsgBoxStyle.Critical)
            End If
        End If
    End Sub

    Private Sub LoadData_Execute(sender As Object, e As SimpleActionExecuteEventArgs) Handles LoadData.Execute
        If TypeOf View.CurrentObject Is ISubmitReportAble Then
            Dim objToSubmit As ISubmitReportAble = CType(View.CurrentObject, ISubmitReportAble)
            objToSubmit.DoLoadData()
        End If
    End Sub

End Class
