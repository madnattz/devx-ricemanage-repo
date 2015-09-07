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
Imports DevExpress.ExpressApp.Win.SystemModule
''' <summary>
''' View Controller สำหรับซ่อนปุ่ม Open Object (ข้อมูลเชื่อมโยง)
''' </summary>
''' <remarks></remarks>
Partial Public Class OpenObjectCustomController
    Inherits ViewController
    Public Sub New()
        InitializeComponent()
    End Sub
    Protected Overrides Sub OnActivated()
        MyBase.OnActivated()
    End Sub
    Protected Overrides Sub OnViewControlsCreated()
        MyBase.OnViewControlsCreated()
        'Frame.GetController(Of OpenObjectController)().Actions("OpenObject").Active.SetItemValue("HiddenReason", False)
        Frame.GetController(Of OpenObjectController)().OpenObjectAction.Active.SetItemValue("HiddenReason", False)
    End Sub
    Protected Overrides Sub OnDeactivated()
        MyBase.OnDeactivated()
    End Sub
End Class
