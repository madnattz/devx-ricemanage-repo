Partial Class MainPlanSearchPanel

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
        Me.PlantSearch = New DevExpress.ExpressApp.Actions.SingleChoiceAction(Me.components)
        Me.SeedTypeSearch = New DevExpress.ExpressApp.Actions.SingleChoiceAction(Me.components)
        Me.SeedClassSearch = New DevExpress.ExpressApp.Actions.SingleChoiceAction(Me.components)
        Me.SeasonSearch = New DevExpress.ExpressApp.Actions.SingleChoiceAction(Me.components)
        Me.SeedYearSearch = New DevExpress.ExpressApp.Actions.SingleChoiceAction(Me.components)
        Me.LotSearch = New DevExpress.ExpressApp.Actions.SingleChoiceAction(Me.components)
        Me.MainPlanSearchButton = New DevExpress.ExpressApp.Actions.SimpleAction(Me.components)
        Me.ClearSearch = New DevExpress.ExpressApp.Actions.SimpleAction(Me.components)
        '
        'PlantSearch
        '
        Me.PlantSearch.Caption = "พืช"
        Me.PlantSearch.ConfirmationMessage = Nothing
        Me.PlantSearch.Id = "PlantSearch"
        Me.PlantSearch.ToolTip = "พืช"
        '
        'SeedTypeSearch
        '
        Me.SeedTypeSearch.Caption = "พันธุ์"
        Me.SeedTypeSearch.ConfirmationMessage = Nothing
        Me.SeedTypeSearch.Id = "SeedTypeSearch"
        Me.SeedTypeSearch.ToolTip = Nothing
        '
        'SeedClassSearch
        '
        Me.SeedClassSearch.Caption = "ชั้นพันธุ์"
        Me.SeedClassSearch.ConfirmationMessage = Nothing
        Me.SeedClassSearch.Id = "SeedClassSearch"
        Me.SeedClassSearch.ToolTip = Nothing
        '
        'SeasonSearch
        '
        Me.SeasonSearch.Caption = "ฤดู"
        Me.SeasonSearch.ConfirmationMessage = Nothing
        Me.SeasonSearch.Id = "SeasonSearch"
        Me.SeasonSearch.ToolTip = Nothing
        '
        'SeedYearSearch
        '
        Me.SeedYearSearch.Caption = "ปี"
        Me.SeedYearSearch.ConfirmationMessage = Nothing
        Me.SeedYearSearch.Id = "SeedYearSearch"
        Me.SeedYearSearch.ToolTip = Nothing
        '
        'LotSearch
        '
        Me.LotSearch.Caption = "รุ่นที่"
        Me.LotSearch.ConfirmationMessage = Nothing
        Me.LotSearch.Id = "LotSearch"
        Me.LotSearch.ToolTip = Nothing
        '
        'MainPlanSearchButton
        '
        Me.MainPlanSearchButton.Caption = "Search"
        Me.MainPlanSearchButton.ConfirmationMessage = Nothing
        Me.MainPlanSearchButton.Id = "MainPlanSearchButton"
        Me.MainPlanSearchButton.ImageName = "Action_Search"
        Me.MainPlanSearchButton.ToolTip = Nothing
        '
        'ClearSearch
        '
        Me.ClearSearch.Caption = "Clear"
        Me.ClearSearch.ConfirmationMessage = Nothing
        Me.ClearSearch.Id = "MainPlanClearSearch"
        Me.ClearSearch.ImageName = "Action_Clear"
        Me.ClearSearch.ToolTip = Nothing
        '
        'MainPlanSearchPanel
        '
        Me.Actions.Add(Me.PlantSearch)
        Me.Actions.Add(Me.SeedTypeSearch)
        Me.Actions.Add(Me.SeedClassSearch)
        Me.Actions.Add(Me.SeasonSearch)
        Me.Actions.Add(Me.SeedYearSearch)
        Me.Actions.Add(Me.LotSearch)
        Me.Actions.Add(Me.MainPlanSearchButton)
        Me.Actions.Add(Me.ClearSearch)

    End Sub
    Friend WithEvents PlantSearch As DevExpress.ExpressApp.Actions.SingleChoiceAction
    Friend WithEvents SeedTypeSearch As DevExpress.ExpressApp.Actions.SingleChoiceAction
    Friend WithEvents SeedClassSearch As DevExpress.ExpressApp.Actions.SingleChoiceAction
    Friend WithEvents SeasonSearch As DevExpress.ExpressApp.Actions.SingleChoiceAction
    Friend WithEvents SeedYearSearch As DevExpress.ExpressApp.Actions.SingleChoiceAction
    Friend WithEvents MainPlanSearchButton As DevExpress.ExpressApp.Actions.SimpleAction
    Friend WithEvents ClearSearch As DevExpress.ExpressApp.Actions.SimpleAction
    Friend WithEvents LotSearch As DevExpress.ExpressApp.Actions.SingleChoiceAction

End Class
