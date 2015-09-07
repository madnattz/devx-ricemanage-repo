Partial Class ctlReceiveSeedSearchPanel

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
        Me.ActSearchReceiveSeed = New DevExpress.ExpressApp.Actions.PopupWindowShowAction(Me.components)
        '
        'ActSearchReceiveSeed
        '
        Me.ActSearchReceiveSeed.AcceptButtonCaption = "ค้นหา"
        Me.ActSearchReceiveSeed.CancelButtonCaption = "ยกเลิก"
        Me.ActSearchReceiveSeed.Caption = "ค้นหาขั้นสูง"
        Me.ActSearchReceiveSeed.Category = "Search"
        Me.ActSearchReceiveSeed.ConfirmationMessage = Nothing
        Me.ActSearchReceiveSeed.Id = "ActIDSearchReceiveSeed"
        Me.ActSearchReceiveSeed.ImageName = "SearchIcon"
        Me.ActSearchReceiveSeed.TargetViewType = DevExpress.ExpressApp.ViewType.ListView
        Me.ActSearchReceiveSeed.ToolTip = Nothing
        Me.ActSearchReceiveSeed.TypeOfView = GetType(DevExpress.ExpressApp.ListView)
        '
        'ctlReceiveSeedSearchPanel
        '
        Me.Actions.Add(Me.ActSearchReceiveSeed)

    End Sub
    Friend WithEvents ActSearchReceiveSeed As DevExpress.ExpressApp.Actions.PopupWindowShowAction

End Class
