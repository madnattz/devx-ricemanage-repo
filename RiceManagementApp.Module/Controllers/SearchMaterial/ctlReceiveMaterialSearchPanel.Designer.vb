Partial Class ctlReceiveMaterialSearchPanel

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
        Me.ActSearchReceiveMaterial = New DevExpress.ExpressApp.Actions.PopupWindowShowAction(Me.components)
        '
        'ActSearchReceiveMaterial
        '
        Me.ActSearchReceiveMaterial.AcceptButtonCaption = "ค้นหา"
        Me.ActSearchReceiveMaterial.CancelButtonCaption = "ยกเลิก"
        Me.ActSearchReceiveMaterial.Caption = "ค้นหาขั้นสูง"
        Me.ActSearchReceiveMaterial.Category = "Search"
        Me.ActSearchReceiveMaterial.ConfirmationMessage = Nothing
        Me.ActSearchReceiveMaterial.Id = "ActSearchReceiveMaterial"
        Me.ActSearchReceiveMaterial.ImageName = "SearchIcon"
        Me.ActSearchReceiveMaterial.TargetViewType = DevExpress.ExpressApp.ViewType.ListView
        Me.ActSearchReceiveMaterial.ToolTip = Nothing
        Me.ActSearchReceiveMaterial.TypeOfView = GetType(DevExpress.ExpressApp.ListView)
        '
        'ctlReceiveMaterialSearchPanel
        '
        Me.Actions.Add(Me.ActSearchReceiveMaterial)

    End Sub
    Friend WithEvents ActSearchReceiveMaterial As DevExpress.ExpressApp.Actions.PopupWindowShowAction

End Class
