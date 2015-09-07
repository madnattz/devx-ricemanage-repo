Partial Class LoadPlanFarerByMainPlanStatus

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
        Me.PlanFarmerStatusAction = New DevExpress.ExpressApp.Actions.SingleChoiceAction(Me.components)
        '
        'PlanFarmerStatusAction
        '
        Me.PlanFarmerStatusAction.Caption = "ʶҹ�"
        Me.PlanFarmerStatusAction.Category = "Search"
        Me.PlanFarmerStatusAction.ConfirmationMessage = Nothing
        Me.PlanFarmerStatusAction.Id = "PlanFarmerStatusAction"
        Me.PlanFarmerStatusAction.TargetObjectType = GetType(RiceManagementApp.[Module].PlanFarmer)
        Me.PlanFarmerStatusAction.TargetViewType = DevExpress.ExpressApp.ViewType.ListView
        Me.PlanFarmerStatusAction.ToolTip = Nothing
        Me.PlanFarmerStatusAction.TypeOfView = GetType(DevExpress.ExpressApp.ListView)
        '
        'LoadGrowPlanByMainPlanStatus
        '
        Me.Actions.Add(Me.PlanFarmerStatusAction)

    End Sub
    Friend WithEvents PlanFarmerStatusAction As DevExpress.ExpressApp.Actions.SingleChoiceAction

End Class
