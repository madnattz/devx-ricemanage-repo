Partial Class LoadHarvestPlanByMainPlanStatus

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
        Me.HarvestPlanStatusAction = New DevExpress.ExpressApp.Actions.SingleChoiceAction(Me.components)
        '
        'HarvestPlanStatusAction
        '
        Me.HarvestPlanStatusAction.Caption = "ʶҹ�"
        Me.HarvestPlanStatusAction.Category = "Search"
        Me.HarvestPlanStatusAction.ConfirmationMessage = Nothing
        Me.HarvestPlanStatusAction.Id = "HarvestPlanStatusAction"
        Me.HarvestPlanStatusAction.TargetObjectType = GetType(RiceManagementApp.[Module].HarvestPlan)
        Me.HarvestPlanStatusAction.TargetViewType = DevExpress.ExpressApp.ViewType.ListView
        Me.HarvestPlanStatusAction.ToolTip = Nothing
        Me.HarvestPlanStatusAction.TypeOfView = GetType(DevExpress.ExpressApp.ListView)
        '
        'LoadHarvestPlanByMainPlanStatus
        '
        Me.Actions.Add(Me.HarvestPlanStatusAction)

    End Sub
    Friend WithEvents HarvestPlanStatusAction As DevExpress.ExpressApp.Actions.SingleChoiceAction

End Class
