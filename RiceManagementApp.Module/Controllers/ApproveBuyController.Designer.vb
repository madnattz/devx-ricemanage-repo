Partial Class ApproveBuyController

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
        Me.ApproveBuyApproveAction = New DevExpress.ExpressApp.Actions.SimpleAction(Me.components)
        Me.ApproveBuyCancelAction = New DevExpress.ExpressApp.Actions.SimpleAction(Me.components)
        Me.ApproveBuyFinishAction = New DevExpress.ExpressApp.Actions.SimpleAction(Me.components)
        '
        'ApproveBuyApproveAction
        '
        Me.ApproveBuyApproveAction.Caption = "͹��ѵ�"
        Me.ApproveBuyApproveAction.Category = "RecordEdit"
        Me.ApproveBuyApproveAction.ConfirmationMessage = "��ҹ��ͧ��úѹ�֡������ ���������?"
        Me.ApproveBuyApproveAction.Id = "ApproveBuyApproveAction"
        Me.ApproveBuyApproveAction.ImageName = "Action_Grant"
        Me.ApproveBuyApproveAction.TargetObjectsCriteria = "ApproveStatus='NotApprove'"
        Me.ApproveBuyApproveAction.ToolTip = Nothing
        '
        'ApproveBuyCancelAction
        '
        Me.ApproveBuyCancelAction.Caption = "¡��ԡ"
        Me.ApproveBuyCancelAction.Category = "RecordEdit"
        Me.ApproveBuyCancelAction.ConfirmationMessage = "��ҹ��ͧ��úѹ�֡������ ���������?"
        Me.ApproveBuyCancelAction.Id = "ApproveBuyCancelAction"
        Me.ApproveBuyCancelAction.ImageName = "Action_Deny"
        Me.ApproveBuyCancelAction.TargetObjectsCriteria = "ApproveStatus='Approve'"
        Me.ApproveBuyCancelAction.ToolTip = Nothing
        '
        'ApproveBuyFinishAction
        '
        Me.ApproveBuyFinishAction.Caption = "�������"
        Me.ApproveBuyFinishAction.Category = "RecordEdit"
        Me.ApproveBuyFinishAction.ConfirmationMessage = "��ҹ��ͧ��úѹ�֡������ ���������?"
        Me.ApproveBuyFinishAction.Id = "ApproveBuyFinishAction"
        Me.ApproveBuyFinishAction.ImageName = "Action_Grant_Set"
        Me.ApproveBuyFinishAction.TargetObjectsCriteria = "ApproveStatus='Approve'"
        Me.ApproveBuyFinishAction.ToolTip = Nothing
        '
        'ApproveBuyController
        '
        Me.Actions.Add(Me.ApproveBuyApproveAction)
        Me.Actions.Add(Me.ApproveBuyCancelAction)
        Me.Actions.Add(Me.ApproveBuyFinishAction)
        Me.TargetViewNesting = DevExpress.ExpressApp.Nesting.Root

    End Sub
    Friend WithEvents ApproveBuyApproveAction As DevExpress.ExpressApp.Actions.SimpleAction
    Friend WithEvents ApproveBuyCancelAction As DevExpress.ExpressApp.Actions.SimpleAction
    Friend WithEvents ApproveBuyFinishAction As DevExpress.ExpressApp.Actions.SimpleAction

End Class
