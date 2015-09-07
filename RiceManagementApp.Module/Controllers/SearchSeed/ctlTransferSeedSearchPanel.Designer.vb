Partial Class ctlTransferSeedSearchPanel

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
        Me.ActSearchTransferSeed = New DevExpress.ExpressApp.Actions.PopupWindowShowAction(Me.components)
        '
        'ActSearchTransferSeed
        '
        Me.ActSearchTransferSeed.AcceptButtonCaption = Nothing
        Me.ActSearchTransferSeed.CancelButtonCaption = Nothing
        Me.ActSearchTransferSeed.Caption = "ค้นหาขั้นสูง"
        Me.ActSearchTransferSeed.Category = "Search"
        Me.ActSearchTransferSeed.ConfirmationMessage = Nothing
        Me.ActSearchTransferSeed.Id = "ActSearchTransferSeed"
        Me.ActSearchTransferSeed.ImageName = "SearchIcon"
        Me.ActSearchTransferSeed.TargetViewType = DevExpress.ExpressApp.ViewType.ListView
        Me.ActSearchTransferSeed.ToolTip = Nothing
        Me.ActSearchTransferSeed.TypeOfView = GetType(DevExpress.ExpressApp.ListView)
        '
        'ctlTransferSeedSearchPanel
        '
        Me.Actions.Add(Me.ActSearchTransferSeed)

    End Sub
    Friend WithEvents ActSearchTransferSeed As DevExpress.ExpressApp.Actions.PopupWindowShowAction

End Class
