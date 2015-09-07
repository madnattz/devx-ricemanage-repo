Partial Class BuySeedApproveController

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
        Me.BuyApproveAction = New DevExpress.ExpressApp.Actions.SimpleAction(Me.components)
        Me.BuyCancelAction = New DevExpress.ExpressApp.Actions.SimpleAction(Me.components)
        '
        'BuyApproveAction
        '
        Me.BuyApproveAction.Caption = "อนุมัติ"
        Me.BuyApproveAction.Category = "RecordEdit"
        Me.BuyApproveAction.ConfirmationMessage = "ท่านต้องการบันทึกข้อมูล ใช่หรือไม่?"
        Me.BuyApproveAction.Id = "BuyApproveAction"
        Me.BuyApproveAction.ImageName = "Action_Grant"
        Me.BuyApproveAction.TargetObjectsCriteria = "BuyStatus='Pending'"
        Me.BuyApproveAction.ToolTip = Nothing
        '
        'BuyCancelAction
        '
        Me.BuyCancelAction.Caption = "ยกเลิก"
        Me.BuyCancelAction.Category = "RecordEdit"
        Me.BuyCancelAction.ConfirmationMessage = "ท่านต้องการบันทึกข้อมูล ใช่หรือไม่?"
        Me.BuyCancelAction.Id = "BuyCancelAction"
        Me.BuyCancelAction.ImageName = "Action_Deny"
        Me.BuyCancelAction.TargetObjectsCriteria = "BuyStatus='Approve'"
        Me.BuyCancelAction.ToolTip = Nothing
        '
        'BuySeedApproveController
        '
        Me.Actions.Add(Me.BuyApproveAction)
        Me.Actions.Add(Me.BuyCancelAction)

    End Sub
    Friend WithEvents BuyApproveAction As DevExpress.ExpressApp.Actions.SimpleAction
    Friend WithEvents BuyCancelAction As DevExpress.ExpressApp.Actions.SimpleAction

End Class
