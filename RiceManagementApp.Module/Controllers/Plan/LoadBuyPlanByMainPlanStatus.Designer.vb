Partial Class LoadBuyPlanByMainPlanStatus

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
        Me.BuyPlanStatusAction = New DevExpress.ExpressApp.Actions.SingleChoiceAction(Me.components)
        '
        'BuyPlanStatusAction
        '
        Me.BuyPlanStatusAction.Caption = "ʶҹ�"
        Me.BuyPlanStatusAction.Category = "Search"
        Me.BuyPlanStatusAction.ConfirmationMessage = Nothing
        Me.BuyPlanStatusAction.Id = "BuyPlanStatusAction"
        Me.BuyPlanStatusAction.TargetObjectType = GetType(RiceManagementApp.[Module].BuyPlan)
        Me.BuyPlanStatusAction.TargetViewType = DevExpress.ExpressApp.ViewType.ListView
        Me.BuyPlanStatusAction.ToolTip = Nothing
        Me.BuyPlanStatusAction.TypeOfView = GetType(DevExpress.ExpressApp.ListView)
        '
        'LoadBuyPlanByMainPlanStatus
        '
        Me.Actions.Add(Me.BuyPlanStatusAction)

    End Sub
    Friend WithEvents BuyPlanStatusAction As DevExpress.ExpressApp.Actions.SingleChoiceAction

End Class
