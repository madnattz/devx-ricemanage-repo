Partial Class LoadApproveBuyByMainPlanStatus

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
        Me.ApproveBuyStatusAction = New DevExpress.ExpressApp.Actions.SingleChoiceAction(Me.components)
        '
        'ApproveBuyStatusAction
        '
        Me.ApproveBuyStatusAction.Caption = "ʶҹ�"
        Me.ApproveBuyStatusAction.Category = "Search"
        Me.ApproveBuyStatusAction.ConfirmationMessage = Nothing
        Me.ApproveBuyStatusAction.Id = "ApproveBuyStatusAction"
        Me.ApproveBuyStatusAction.TargetObjectType = GetType(RiceManagementApp.[Module].ApproveBuy)
        Me.ApproveBuyStatusAction.TargetViewType = DevExpress.ExpressApp.ViewType.ListView
        Me.ApproveBuyStatusAction.ToolTip = Nothing
        Me.ApproveBuyStatusAction.TypeOfView = GetType(DevExpress.ExpressApp.ListView)
        '
        'LoadApproveBuyByMainPlanStatus
        '
        Me.Actions.Add(Me.ApproveBuyStatusAction)

    End Sub
    Friend WithEvents ApproveBuyStatusAction As DevExpress.ExpressApp.Actions.SingleChoiceAction

End Class
