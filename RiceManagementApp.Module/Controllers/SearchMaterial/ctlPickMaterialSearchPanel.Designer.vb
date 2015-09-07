Partial Class ctlPickMaterialSearchPanel

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
        Me.ActSearchPickMaterial = New DevExpress.ExpressApp.Actions.PopupWindowShowAction(Me.components)
        '
        'ActSearchPickMaterial
        '
        Me.ActSearchPickMaterial.AcceptButtonCaption = "ค้นหา"
        Me.ActSearchPickMaterial.CancelButtonCaption = "ยกเลิก"
        Me.ActSearchPickMaterial.Caption = "ค้นหาขั้นสูง"
        Me.ActSearchPickMaterial.Category = "Search"
        Me.ActSearchPickMaterial.ConfirmationMessage = Nothing
        Me.ActSearchPickMaterial.Id = "ActSearchPickMaterial"
        Me.ActSearchPickMaterial.ImageName = "SearchIcon"
        Me.ActSearchPickMaterial.TargetViewType = DevExpress.ExpressApp.ViewType.ListView
        Me.ActSearchPickMaterial.ToolTip = Nothing
        Me.ActSearchPickMaterial.TypeOfView = GetType(DevExpress.ExpressApp.ListView)
        '
        'ctlPickMaterialSearchPanel
        '
        Me.Actions.Add(Me.ActSearchPickMaterial)

    End Sub
    Friend WithEvents ActSearchPickMaterial As DevExpress.ExpressApp.Actions.PopupWindowShowAction

End Class
