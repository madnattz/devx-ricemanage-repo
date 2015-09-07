Partial Class ctlTransferMaterialSearchPanel

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
        Me.ActSearchTransferMaterial = New DevExpress.ExpressApp.Actions.PopupWindowShowAction(Me.components)
        '
        'ActSearchTransferMaterial
        '
        Me.ActSearchTransferMaterial.AcceptButtonCaption = "ค้นหา"
        Me.ActSearchTransferMaterial.CancelButtonCaption = "ยกเลิก"
        Me.ActSearchTransferMaterial.Caption = "ค้นหาขั้นสูง"
        Me.ActSearchTransferMaterial.Category = "Search"
        Me.ActSearchTransferMaterial.ConfirmationMessage = Nothing
        Me.ActSearchTransferMaterial.Id = "ActSearchTransferMaterial"
        Me.ActSearchTransferMaterial.ImageName = "SearchIcon"
        Me.ActSearchTransferMaterial.TargetViewType = DevExpress.ExpressApp.ViewType.ListView
        Me.ActSearchTransferMaterial.ToolTip = Nothing
        Me.ActSearchTransferMaterial.TypeOfView = GetType(DevExpress.ExpressApp.ListView)
        '
        'ctlTransferMaterialSearchPanel
        '
        Me.Actions.Add(Me.ActSearchTransferMaterial)

    End Sub
    Friend WithEvents ActSearchTransferMaterial As DevExpress.ExpressApp.Actions.PopupWindowShowAction

End Class
