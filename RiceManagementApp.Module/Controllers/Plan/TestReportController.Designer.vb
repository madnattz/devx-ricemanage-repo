Partial Class TestReportController

    <System.Diagnostics.DebuggerNonUserCode()> _
    Public Sub New(ByVal Container As System.ComponentModel.IContainer)
        MyClass.New()

        'Required for Windows.Forms Class Composition Designer support
        Container.Add(Me)

    End Sub

    'Component overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Component Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Component Designer
    'It can be modified using the Component Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.SimpleAction1 = New DevExpress.ExpressApp.Actions.SimpleAction(Me.components)
        '
        'SimpleAction1
        '
        Me.SimpleAction1.Caption = "Test Actions"
        Me.SimpleAction1.ConfirmationMessage = Nothing
        Me.SimpleAction1.Id = "TestActions"
        Me.SimpleAction1.ImageName = "BO_Address"
        Me.SimpleAction1.TargetObjectType = GetType(RiceManagementApp.[Module].SubmitPlanReport)
        Me.SimpleAction1.ToolTip = Nothing
        Me.SimpleAction1.TypeOfView = GetType(DevExpress.ExpressApp.View)
        '
        'TestReportController
        '
        Me.Actions.Add(Me.SimpleAction1)
        Me.TargetObjectType = GetType(RiceManagementApp.[Module].SubmitPlanReport)

    End Sub
    Friend WithEvents SimpleAction1 As DevExpress.ExpressApp.Actions.SimpleAction

End Class
