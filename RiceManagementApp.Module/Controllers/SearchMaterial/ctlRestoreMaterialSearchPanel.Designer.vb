Partial Class ctlRestoreMaterialSearchPanel

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
        Me.ActSearchRestoreMaterial = New DevExpress.ExpressApp.Actions.PopupWindowShowAction(Me.components)
        '
        'ActSearchRestoreMaterial
        '
        Me.ActSearchRestoreMaterial.AcceptButtonCaption = "ค้นหา"
        Me.ActSearchRestoreMaterial.CancelButtonCaption = "ยกเลิก"
        Me.ActSearchRestoreMaterial.Caption = "ค้นหาขั้นสูง"
        Me.ActSearchRestoreMaterial.Category = "Search"
        Me.ActSearchRestoreMaterial.ConfirmationMessage = Nothing
        Me.ActSearchRestoreMaterial.Id = "ActSearchRestoreMaterial"
        Me.ActSearchRestoreMaterial.ImageName = "SearchIcon"
        Me.ActSearchRestoreMaterial.TargetViewType = DevExpress.ExpressApp.ViewType.ListView
        Me.ActSearchRestoreMaterial.ToolTip = Nothing
        Me.ActSearchRestoreMaterial.TypeOfView = GetType(DevExpress.ExpressApp.ListView)
        '
        'ctlRestoreMaterialSearchPanel
        '
        Me.Actions.Add(Me.ActSearchRestoreMaterial)

    End Sub
    Friend WithEvents ActSearchRestoreMaterial As DevExpress.ExpressApp.Actions.PopupWindowShowAction

End Class
