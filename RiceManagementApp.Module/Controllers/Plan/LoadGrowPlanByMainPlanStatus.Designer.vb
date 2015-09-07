Partial Class LoadGrowPlanByMainPlanStatus

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
        Me.GrowPlanStatusAction = New DevExpress.ExpressApp.Actions.SingleChoiceAction(Me.components)
        '
        'GrowPlanStatusAction
        '
        Me.GrowPlanStatusAction.Caption = "ʶҹ�"
        Me.GrowPlanStatusAction.Category = "Search"
        Me.GrowPlanStatusAction.ConfirmationMessage = Nothing
        Me.GrowPlanStatusAction.Id = "GrowPlanStatusAction"
        Me.GrowPlanStatusAction.TargetObjectType = GetType(RiceManagementApp.[Module].GrowPlan)
        Me.GrowPlanStatusAction.TargetViewType = DevExpress.ExpressApp.ViewType.ListView
        Me.GrowPlanStatusAction.ToolTip = Nothing
        Me.GrowPlanStatusAction.TypeOfView = GetType(DevExpress.ExpressApp.ListView)
        '
        'LoadGrowPlanByMainPlanStatus
        '
        Me.Actions.Add(Me.GrowPlanStatusAction)

    End Sub
    Friend WithEvents GrowPlanStatusAction As DevExpress.ExpressApp.Actions.SingleChoiceAction

End Class
