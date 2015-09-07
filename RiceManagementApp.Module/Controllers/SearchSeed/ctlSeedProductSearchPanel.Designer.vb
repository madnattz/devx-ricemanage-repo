Partial Class ctlSeedProductSearchPanel

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
        Me.ActSearchSeedProduct = New DevExpress.ExpressApp.Actions.PopupWindowShowAction(Me.components)
        '
        'ActSearchSeedProduct
        '
        Me.ActSearchSeedProduct.AcceptButtonCaption = Nothing
        Me.ActSearchSeedProduct.CancelButtonCaption = Nothing
        Me.ActSearchSeedProduct.Caption = "ค้นหาขั้นสูง"
        Me.ActSearchSeedProduct.Category = "Search"
        Me.ActSearchSeedProduct.ConfirmationMessage = Nothing
        Me.ActSearchSeedProduct.Id = "ActSearchSeedProduct"
        Me.ActSearchSeedProduct.ImageName = "SearchIcon"
        Me.ActSearchSeedProduct.TargetViewType = DevExpress.ExpressApp.ViewType.ListView
        Me.ActSearchSeedProduct.ToolTip = Nothing
        Me.ActSearchSeedProduct.TypeOfView = GetType(DevExpress.ExpressApp.ListView)
        '
        'ctlSeedProductSearchPanel
        '
        Me.Actions.Add(Me.ActSearchSeedProduct)

    End Sub
    Friend WithEvents ActSearchSeedProduct As DevExpress.ExpressApp.Actions.PopupWindowShowAction

End Class
