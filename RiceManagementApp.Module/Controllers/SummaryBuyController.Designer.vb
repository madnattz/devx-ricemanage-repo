Partial Class SummaryBuyController

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
        Me.SummaryBuyApproveAction = New DevExpress.ExpressApp.Actions.SimpleAction(Me.components)
        Me.SummaryBuyCancelAction = New DevExpress.ExpressApp.Actions.SimpleAction(Me.components)
        '
        'SummaryBuyApproveAction
        '
        Me.SummaryBuyApproveAction.Caption = "อนุมัติ"
        Me.SummaryBuyApproveAction.Category = "RecordEdit"
        Me.SummaryBuyApproveAction.ConfirmationMessage = "ท่านต้องการบันทึกข้อมูล ใช่หรือไม่?"
        Me.SummaryBuyApproveAction.Id = "SummaryBuyApproveAction"
        Me.SummaryBuyApproveAction.ImageName = "Action_Grant"
        Me.SummaryBuyApproveAction.TargetObjectsCriteria = "ApproveStatus='NotApprove'"
        Me.SummaryBuyApproveAction.ToolTip = Nothing
        '
        'SummaryBuyCancelAction
        '
        Me.SummaryBuyCancelAction.Caption = "ยกเลิก"
        Me.SummaryBuyCancelAction.Category = "RecordEdit"
        Me.SummaryBuyCancelAction.ConfirmationMessage = "ท่านต้องการบันทึกข้อมูล ใช่หรือไม่?"
        Me.SummaryBuyCancelAction.Id = "SummaryBuyCancelAction"
        Me.SummaryBuyCancelAction.ImageName = "Action_Deny"
        Me.SummaryBuyCancelAction.TargetObjectsCriteria = "ApproveStatus='Approve'"
        Me.SummaryBuyCancelAction.ToolTip = Nothing
        '
        'SummaryBuyController
        '
        Me.Actions.Add(Me.SummaryBuyApproveAction)
        Me.Actions.Add(Me.SummaryBuyCancelAction)

    End Sub
    Friend WithEvents SummaryBuyApproveAction As DevExpress.ExpressApp.Actions.SimpleAction
    Friend WithEvents SummaryBuyCancelAction As DevExpress.ExpressApp.Actions.SimpleAction

End Class
