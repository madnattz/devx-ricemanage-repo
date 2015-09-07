Partial Class BringViewController

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
        Me.Bringforward = New DevExpress.ExpressApp.Actions.SimpleAction(Me.components)
        '
        'Bringforward
        '
        Me.Bringforward.Caption = "Bringforward"
        Me.Bringforward.ConfirmationMessage = "คุณต้องการดึงข้อมูลใช่ หรือ ไม่"
        Me.Bringforward.Id = "Bringforward"
        Me.Bringforward.ImageName = "ดึงข้อมูล"
        Me.Bringforward.TargetObjectType = GetType(EventBringforward)
        Me.Bringforward.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView
        Me.Bringforward.ToolTip = Nothing
        Me.Bringforward.TypeOfView = GetType(DevExpress.ExpressApp.DetailView)
        '
        'BringViewController
        '
        Me.Actions.Add(Me.Bringforward)

    End Sub
    Friend WithEvents Bringforward As DevExpress.ExpressApp.Actions.SimpleAction

End Class
