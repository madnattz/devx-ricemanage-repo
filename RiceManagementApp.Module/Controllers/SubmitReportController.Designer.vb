Partial Class SubmitReportController

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
        Me.LoadData = New DevExpress.ExpressApp.Actions.SimpleAction(Me.components)
        Me.SubmitReportAction = New DevExpress.ExpressApp.Actions.SimpleAction(Me.components)
        '
        'LoadData
        '
        Me.LoadData.Caption = "แสดงข้อมูล"
        Me.LoadData.Category = "RecordEdit"
        Me.LoadData.ConfirmationMessage = Nothing
        Me.LoadData.Id = "LoadData"
        Me.LoadData.ImageName = "BO_Note"
        Me.LoadData.TargetObjectsCriteria = "Status='Draft'"
        Me.LoadData.ToolTip = Nothing
        '
        'SubmitReportAction
        '
        Me.SubmitReportAction.Caption = "ส่งรายงาน"
        Me.SubmitReportAction.Category = "RecordEdit"
        Me.SubmitReportAction.ConfirmationMessage = "ท่านต้องการส่งข้อมูลรายงานนี้ ใช่หรือไม่?"
        Me.SubmitReportAction.Id = "SubmitReportAction"
        Me.SubmitReportAction.ImageName = "sent2"
        Me.SubmitReportAction.TargetObjectsCriteria = "Status='Draft'"
        Me.SubmitReportAction.ToolTip = Nothing
        '
        'SubmitReportController
        '
        Me.Actions.Add(Me.LoadData)
        Me.Actions.Add(Me.SubmitReportAction)

    End Sub

    Friend WithEvents LoadData As DevExpress.ExpressApp.Actions.SimpleAction
    Friend WithEvents SubmitReportAction As DevExpress.ExpressApp.Actions.SimpleAction

End Class
