Partial Class SimsApproveController

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
        Me.SimsApproveAction = New DevExpress.ExpressApp.Actions.SimpleAction(Me.components)
        Me.SimsCancelAction = New DevExpress.ExpressApp.Actions.SimpleAction(Me.components)
        '
        'SimsApproveAction
        '
        Me.SimsApproveAction.Caption = "อนุมัติ"
        Me.SimsApproveAction.Category = "RecordEdit"
        Me.SimsApproveAction.ConfirmationMessage = "ท่านต้องการบันทึกข้อมูล ใช่หรือไม่?"
        Me.SimsApproveAction.Id = "SimsApproveAction"
        Me.SimsApproveAction.ImageName = "Action_Grant"
        Me.SimsApproveAction.TargetObjectsCriteria = "Status='Pending'"
        Me.SimsApproveAction.ToolTip = Nothing
        '
        'SimsCancelAction
        '
        Me.SimsCancelAction.Caption = "ยกเลิก"
        Me.SimsCancelAction.Category = "RecordEdit"
        Me.SimsCancelAction.ConfirmationMessage = "ท่านต้องการบันทึกข้อมูล ใช่หรือไม่?"
        Me.SimsCancelAction.Id = "SimsCancelAction"
        Me.SimsCancelAction.ImageName = "Action_Deny"
        Me.SimsCancelAction.TargetObjectsCriteria = "Status='Approve'"
        Me.SimsCancelAction.ToolTip = Nothing
        '
        'SimsApproveController
        '
        Me.Actions.Add(Me.SimsApproveAction)
        Me.Actions.Add(Me.SimsCancelAction)

    End Sub
    Friend WithEvents SimsApproveAction As DevExpress.ExpressApp.Actions.SimpleAction
    Friend WithEvents SimsCancelAction As DevExpress.ExpressApp.Actions.SimpleAction

End Class
