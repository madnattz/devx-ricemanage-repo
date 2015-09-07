Partial Class LoadMainPlanByStatus

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
        Me.MainPlanStatusAction = New DevExpress.ExpressApp.Actions.SingleChoiceAction(Me.components)
        '
        'MainPlanStatusAction
        '
        Me.MainPlanStatusAction.Caption = "ʶҹ�"
        Me.MainPlanStatusAction.Category = "Search"
        Me.MainPlanStatusAction.ConfirmationMessage = Nothing
        Me.MainPlanStatusAction.Id = "MainPlanStatusAction"
        Me.MainPlanStatusAction.TargetObjectType = GetType(RiceManagementApp.[Module].MainPlan)
        Me.MainPlanStatusAction.TargetViewType = DevExpress.ExpressApp.ViewType.ListView
        Me.MainPlanStatusAction.ToolTip = Nothing
        Me.MainPlanStatusAction.TypeOfView = GetType(DevExpress.ExpressApp.ListView)
        '
        'LoadMainPlanByStatus
        '
        Me.Actions.Add(Me.MainPlanStatusAction)

    End Sub
    Friend WithEvents MainPlanStatusAction As DevExpress.ExpressApp.Actions.SingleChoiceAction

End Class
