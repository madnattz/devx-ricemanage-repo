Partial Class PostGLPVViewController

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
        Me.PostGLPV = New DevExpress.ExpressApp.Actions.SimpleAction(Me.components)
        '
        'PostGLPV
        '
        Me.PostGLPV.Caption = "CVPost GLPV"
        Me.PostGLPV.ConfirmationMessage = "คุณต้องการโพสข้อมูลไปยังรายการประเภทบัญชีใช่ หรือไม่"
        Me.PostGLPV.Id = "CVPostGLPV"
        Me.PostGLPV.ImageName = "Action_Printing_Print"
        Me.PostGLPV.TargetObjectType = GetType(AccountBookPV)
        Me.PostGLPV.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView
        Me.PostGLPV.ToolTip = Nothing
        Me.PostGLPV.TypeOfView = GetType(DevExpress.ExpressApp.DetailView)
        '
        'PostGLPVViewController
        '
        Me.Actions.Add(Me.PostGLPV)

    End Sub
    Friend WithEvents PostGLPV As DevExpress.ExpressApp.Actions.SimpleAction

End Class
