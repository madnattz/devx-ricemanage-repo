Partial Class ctlReceiceTransferSeedSearchPanel

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
        Me.ActSearchReceiveTransferSeed = New DevExpress.ExpressApp.Actions.PopupWindowShowAction(Me.components)
        '
        'ActSearchReceiveTransferSeed
        '
        Me.ActSearchReceiveTransferSeed.AcceptButtonCaption = "ค้นหา"
        Me.ActSearchReceiveTransferSeed.CancelButtonCaption = "ยกเลิก"
        Me.ActSearchReceiveTransferSeed.Caption = "ค้นหาขั้นสูง"
        Me.ActSearchReceiveTransferSeed.Category = "Search"
        Me.ActSearchReceiveTransferSeed.ConfirmationMessage = Nothing
        Me.ActSearchReceiveTransferSeed.Id = "ActSearchReceiveTransferSeed"
        Me.ActSearchReceiveTransferSeed.ImageName = "SearchIcon"
        Me.ActSearchReceiveTransferSeed.TargetViewType = DevExpress.ExpressApp.ViewType.ListView
        Me.ActSearchReceiveTransferSeed.ToolTip = Nothing
        Me.ActSearchReceiveTransferSeed.TypeOfView = GetType(DevExpress.ExpressApp.ListView)
        '
        'ctlReceiceTransferSeedSearchPanel
        '
        Me.Actions.Add(Me.ActSearchReceiveTransferSeed)

    End Sub
    Friend WithEvents ActSearchReceiveTransferSeed As DevExpress.ExpressApp.Actions.PopupWindowShowAction

End Class
