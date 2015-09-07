Partial Class ctlSaleSeedSearchPanel

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
        Me.ActSearchSaleSeed = New DevExpress.ExpressApp.Actions.PopupWindowShowAction(Me.components)
        '
        'ActSearchSaleSeed
        '
        Me.ActSearchSaleSeed.AcceptButtonCaption = Nothing
        Me.ActSearchSaleSeed.CancelButtonCaption = Nothing
        Me.ActSearchSaleSeed.Caption = "ค้นหาขั้นสูง"
        Me.ActSearchSaleSeed.Category = "Search"
        Me.ActSearchSaleSeed.ConfirmationMessage = Nothing
        Me.ActSearchSaleSeed.Id = "ActSearchSaleSeed"
        Me.ActSearchSaleSeed.ImageName = "SearchIcon"
        Me.ActSearchSaleSeed.TargetViewType = DevExpress.ExpressApp.ViewType.ListView
        Me.ActSearchSaleSeed.ToolTip = Nothing
        Me.ActSearchSaleSeed.TypeOfView = GetType(DevExpress.ExpressApp.ListView)
        '
        'ctlSaleSeedSearchPanel
        '
        Me.Actions.Add(Me.ActSearchSaleSeed)

    End Sub
    Friend WithEvents ActSearchSaleSeed As DevExpress.ExpressApp.Actions.PopupWindowShowAction

End Class
