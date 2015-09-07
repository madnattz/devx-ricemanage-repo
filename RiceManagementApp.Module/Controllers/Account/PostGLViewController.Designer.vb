Partial Class PostGLViewController

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
        Me.PostGL = New DevExpress.ExpressApp.Actions.SimpleAction(Me.components)
        '
        'PostGL
        '
        Me.PostGL.Caption = "CVPost GL"
        Me.PostGL.ConfirmationMessage = "คุณต้องการโพสข้อมูลไปยังรายการประเภทบัญชีใช่ หรือไม่"
        Me.PostGL.Id = "CVPostGL"
        Me.PostGL.ImageName = "Action_Printing_Print"
        Me.PostGL.TargetObjectType = GetType(AccountBookRV)
        Me.PostGL.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView
        Me.PostGL.ToolTip = Nothing
        Me.PostGL.TypeOfView = GetType(DevExpress.ExpressApp.DetailView)
        '
        'PostGLViewController
        '
        Me.Actions.Add(Me.PostGL)

    End Sub
    Friend WithEvents PostGL As DevExpress.ExpressApp.Actions.SimpleAction

End Class
