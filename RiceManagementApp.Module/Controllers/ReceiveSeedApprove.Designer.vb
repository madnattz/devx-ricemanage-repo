Partial Class ReceiveSeedApprove

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
        Me.SetToApprove = New DevExpress.ExpressApp.Actions.SimpleAction(Me.components)
        '
        'SetToApprove
        '
        Me.SetToApprove.Caption = "Set To Approve Action"
        Me.SetToApprove.ConfirmationMessage = "ท่านต้องการบันทึกข้อมูล ใช่หรือไม่?"
        Me.SetToApprove.Id = "SetToApproveAction"
        Me.SetToApprove.ImageName = "Action_Grant"
        Me.SetToApprove.TargetObjectsCriteria = "Status='Pending'"
        Me.SetToApprove.TargetObjectType = GetType(RiceManagementApp.[Module].ReceiveSeed)
        Me.SetToApprove.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView
        Me.SetToApprove.ToolTip = Nothing
        Me.SetToApprove.TypeOfView = GetType(DevExpress.ExpressApp.DetailView)
        '
        'ReceiveSeedApprove
        '
        Me.Actions.Add(Me.SetToApprove)

    End Sub
    Friend WithEvents SetToApprove As DevExpress.ExpressApp.Actions.SimpleAction

End Class
