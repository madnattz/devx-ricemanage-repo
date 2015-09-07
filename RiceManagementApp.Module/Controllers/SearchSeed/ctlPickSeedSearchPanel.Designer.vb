Partial Class ctlPickSeedSearchPanel

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
        Me.ActSearchPickSeed = New DevExpress.ExpressApp.Actions.PopupWindowShowAction(Me.components)
        '
        'ActSearchPickSeed
        '
        Me.ActSearchPickSeed.AcceptButtonCaption = Nothing
        Me.ActSearchPickSeed.CancelButtonCaption = Nothing
        Me.ActSearchPickSeed.Caption = "ค้นหาขั้นสูง"
        Me.ActSearchPickSeed.Category = "Search"
        Me.ActSearchPickSeed.ConfirmationMessage = Nothing
        Me.ActSearchPickSeed.Id = "ActIDSearchPickSeed"
        Me.ActSearchPickSeed.ImageName = "SearchIcon"
        Me.ActSearchPickSeed.TargetViewType = DevExpress.ExpressApp.ViewType.ListView
        Me.ActSearchPickSeed.ToolTip = Nothing
        Me.ActSearchPickSeed.TypeOfView = GetType(DevExpress.ExpressApp.ListView)
        '
        'ctlPickSeedSearchPanel
        '
        Me.Actions.Add(Me.ActSearchPickSeed)

    End Sub
    Friend WithEvents ActSearchPickSeed As DevExpress.ExpressApp.Actions.PopupWindowShowAction

End Class
