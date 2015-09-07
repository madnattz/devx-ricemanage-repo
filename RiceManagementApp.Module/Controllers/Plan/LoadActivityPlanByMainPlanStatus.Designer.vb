Partial Class LoadActivityPlanByMainPlanStatus

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
        Me.ActivityPlanStatusAction = New DevExpress.ExpressApp.Actions.SingleChoiceAction(Me.components)
        '
        'ActivityPlanStatusAction
        '
        Me.ActivityPlanStatusAction.Caption = "ʶҹ�"
        Me.ActivityPlanStatusAction.Category = "Search"
        Me.ActivityPlanStatusAction.ConfirmationMessage = Nothing
        Me.ActivityPlanStatusAction.Id = "ActivityPlanStatusAction"
        Me.ActivityPlanStatusAction.TargetObjectType = GetType(RiceManagementApp.[Module].AuditActivityPlan)
        Me.ActivityPlanStatusAction.TargetViewType = DevExpress.ExpressApp.ViewType.ListView
        Me.ActivityPlanStatusAction.ToolTip = Nothing
        Me.ActivityPlanStatusAction.TypeOfView = GetType(DevExpress.ExpressApp.ListView)
        '
        'LoadActivityPlanByMainPlanStatus
        '
        Me.Actions.Add(Me.ActivityPlanStatusAction)

    End Sub
    Friend WithEvents ActivityPlanStatusAction As DevExpress.ExpressApp.Actions.SingleChoiceAction

End Class
