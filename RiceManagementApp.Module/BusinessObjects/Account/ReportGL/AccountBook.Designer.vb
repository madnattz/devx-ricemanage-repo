<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class AccountBook
    Inherits DevExpress.XtraReports.UI.XtraReport

    'XtraReport overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Designer
    'It can be modified using the Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim DynamicListLookUpSettings1 As DevExpress.XtraReports.Parameters.DynamicListLookUpSettings = New DevExpress.XtraReports.Parameters.DynamicListLookUpSettings()
        Dim XrSummary1 As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary()
        Dim XrSummary2 As DevExpress.XtraReports.UI.XRSummary = New DevExpress.XtraReports.UI.XRSummary()
        Dim DynamicListLookUpSettings2 As DevExpress.XtraReports.Parameters.DynamicListLookUpSettings = New DevExpress.XtraReports.Parameters.DynamicListLookUpSettings()
        Dim DynamicListLookUpSettings3 As DevExpress.XtraReports.Parameters.DynamicListLookUpSettings = New DevExpress.XtraReports.Parameters.DynamicListLookUpSettings()
        Dim DynamicListLookUpSettings4 As DevExpress.XtraReports.Parameters.DynamicListLookUpSettings = New DevExpress.XtraReports.Parameters.DynamicListLookUpSettings()
        Dim StoredProcQuery2 As DevExpress.DataAccess.Sql.StoredProcQuery = New DevExpress.DataAccess.Sql.StoredProcQuery()
        Dim QueryParameter8 As DevExpress.DataAccess.Sql.QueryParameter = New DevExpress.DataAccess.Sql.QueryParameter()
        Dim QueryParameter9 As DevExpress.DataAccess.Sql.QueryParameter = New DevExpress.DataAccess.Sql.QueryParameter()
        Dim QueryParameter10 As DevExpress.DataAccess.Sql.QueryParameter = New DevExpress.DataAccess.Sql.QueryParameter()
        Dim QueryParameter11 As DevExpress.DataAccess.Sql.QueryParameter = New DevExpress.DataAccess.Sql.QueryParameter()
        Dim QueryParameter12 As DevExpress.DataAccess.Sql.QueryParameter = New DevExpress.DataAccess.Sql.QueryParameter()
        Dim QueryParameter13 As DevExpress.DataAccess.Sql.QueryParameter = New DevExpress.DataAccess.Sql.QueryParameter()
        Dim QueryParameter14 As DevExpress.DataAccess.Sql.QueryParameter = New DevExpress.DataAccess.Sql.QueryParameter()
        Dim StoredProcQuery1 As DevExpress.DataAccess.Sql.StoredProcQuery = New DevExpress.DataAccess.Sql.StoredProcQuery()
        Dim QueryParameter1 As DevExpress.DataAccess.Sql.QueryParameter = New DevExpress.DataAccess.Sql.QueryParameter()
        Dim QueryParameter2 As DevExpress.DataAccess.Sql.QueryParameter = New DevExpress.DataAccess.Sql.QueryParameter()
        Dim QueryParameter3 As DevExpress.DataAccess.Sql.QueryParameter = New DevExpress.DataAccess.Sql.QueryParameter()
        Dim QueryParameter4 As DevExpress.DataAccess.Sql.QueryParameter = New DevExpress.DataAccess.Sql.QueryParameter()
        Dim QueryParameter5 As DevExpress.DataAccess.Sql.QueryParameter = New DevExpress.DataAccess.Sql.QueryParameter()
        Dim QueryParameter6 As DevExpress.DataAccess.Sql.QueryParameter = New DevExpress.DataAccess.Sql.QueryParameter()
        Dim QueryParameter7 As DevExpress.DataAccess.Sql.QueryParameter = New DevExpress.DataAccess.Sql.QueryParameter()
        Me.collectionDataSource1 = New DevExpress.Persistent.Base.ReportsV2.CollectionDataSource()
        Me.collectionDataSource2 = New DevExpress.Persistent.Base.ReportsV2.CollectionDataSource()
        Me.XrTableCell15 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.EndDoc = New DevExpress.XtraReports.Parameters.Parameter()
        Me.DetailReport = New DevExpress.XtraReports.UI.DetailReportBand()
        Me.Detail = New DevExpress.XtraReports.UI.DetailBand()
        Me.GroupFooter1 = New DevExpress.XtraReports.UI.GroupFooterBand()
        Me.table2 = New DevExpress.XtraReports.UI.XRTable()
        Me.tableRow2 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.tableCell14 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell15 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell16 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell17 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell18 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell19 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell20 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell21 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell22 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell23 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell24 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell25 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell26 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell23 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.DataField = New DevExpress.XtraReports.UI.XRControlStyle()
        Me.EndDate = New DevExpress.XtraReports.Parameters.Parameter()
        Me.label22 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrTableRow8 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.XrTableCell21 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell22 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableRow7 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.XrTableCell18 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTable3 = New DevExpress.XtraReports.UI.XRTable()
        Me.XrTableRow5 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.XrTableCell12 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableRow6 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.XrTableCell17 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.Title = New DevExpress.XtraReports.UI.XRControlStyle()
        Me.bottomMarginBand1 = New DevExpress.XtraReports.UI.BottomMarginBand()
        Me.label35 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrTable1 = New DevExpress.XtraReports.UI.XRTable()
        Me.XrTableRow1 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.XrTableCell1 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.collectionDataSource3 = New DevExpress.Persistent.Base.ReportsV2.CollectionDataSource()
        Me.XrTableRow3 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.XrTableCell3 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.label34 = New DevExpress.XtraReports.UI.XRLabel()
        Me.PageInfo = New DevExpress.XtraReports.UI.XRControlStyle()
        Me.StartDate = New DevExpress.XtraReports.Parameters.Parameter()
        Me.pageFooterBand1 = New DevExpress.XtraReports.UI.PageFooterBand()
        Me.pageInfo1 = New DevExpress.XtraReports.UI.XRPageInfo()
        Me.pageInfo2 = New DevExpress.XtraReports.UI.XRPageInfo()
        Me.PageHeader = New DevExpress.XtraReports.UI.PageHeaderBand()
        Me.XrTable4 = New DevExpress.XtraReports.UI.XRTable()
        Me.label28 = New DevExpress.XtraReports.UI.XRLabel()
        Me.label27 = New DevExpress.XtraReports.UI.XRLabel()
        Me.label26 = New DevExpress.XtraReports.UI.XRLabel()
        Me.label25 = New DevExpress.XtraReports.UI.XRLabel()
        Me.label24 = New DevExpress.XtraReports.UI.XRLabel()
        Me.label23 = New DevExpress.XtraReports.UI.XRLabel()
        Me.label21 = New DevExpress.XtraReports.UI.XRLabel()
        Me.StartDoc = New DevExpress.XtraReports.Parameters.Parameter()
        Me.calculatedField1 = New DevExpress.XtraReports.UI.CalculatedField()
        Me.StartList = New DevExpress.XtraReports.Parameters.Parameter()
        Me.label33 = New DevExpress.XtraReports.UI.XRLabel()
        Me.EndList = New DevExpress.XtraReports.Parameters.Parameter()
        Me.topMarginBand1 = New DevExpress.XtraReports.UI.TopMarginBand()
        Me.reportHeaderBand1 = New DevExpress.XtraReports.UI.ReportHeaderBand()
        Me.label36 = New DevExpress.XtraReports.UI.XRLabel()
        Me.detailBand1 = New DevExpress.XtraReports.UI.DetailBand()
        Me.FieldCaption = New DevExpress.XtraReports.UI.XRControlStyle()
        Me.AccBook = New DevExpress.XtraReports.Parameters.Parameter()
        Me.SqlDataSource1 = New DevExpress.DataAccess.Sql.SqlDataSource()
        Me.XrPivotGrid1 = New DevExpress.XtraReports.UI.XRPivotGrid()
        Me.SqlDataSource2 = New DevExpress.DataAccess.Sql.SqlDataSource()
        Me.pivotGridField1 = New DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField()
        CType(Me.collectionDataSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.collectionDataSource2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.table2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XrTable3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XrTable1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.collectionDataSource3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XrTable4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'collectionDataSource1
        '
        Me.collectionDataSource1.Name = "collectionDataSource1"
        Me.collectionDataSource1.ObjectTypeName = "RiceManagementApp.Module.AccountBookRV"
        Me.collectionDataSource1.Sorting.AddRange(New DevExpress.Xpo.SortProperty() {New DevExpress.Xpo.SortProperty("[DocumentNoRV]", DevExpress.Xpo.DB.SortingDirection.Ascending)})
        Me.collectionDataSource1.TopReturnedRecords = 0
        '
        'collectionDataSource2
        '
        Me.collectionDataSource2.Name = "collectionDataSource2"
        Me.collectionDataSource2.ObjectTypeName = "RiceManagementApp.Module.AccountBookRV"
        Me.collectionDataSource2.Sorting.AddRange(New DevExpress.Xpo.SortProperty() {New DevExpress.Xpo.SortProperty("[ListNoRV]", DevExpress.Xpo.DB.SortingDirection.Ascending)})
        Me.collectionDataSource2.TopReturnedRecords = 0
        '
        'XrTableCell15
        '
        Me.XrTableCell15.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.XrTableCell15.Borders = CType((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.XrTableCell15.BorderWidth = 1.0!
        Me.XrTableCell15.Font = New System.Drawing.Font("Angsana New", 12.0!, System.Drawing.FontStyle.Bold)
        Me.XrTableCell15.Name = "XrTableCell15"
        Me.XrTableCell15.StylePriority.UseBorderColor = False
        Me.XrTableCell15.StylePriority.UseBorders = False
        Me.XrTableCell15.StylePriority.UseBorderWidth = False
        Me.XrTableCell15.StylePriority.UseFont = False
        Me.XrTableCell15.Text = "เดบิต"
        Me.XrTableCell15.Weight = 0.58699734017729466R
        '
        'EndDoc
        '
        Me.EndDoc.Description = "ถึง"
        DynamicListLookUpSettings1.DataAdapter = Nothing
        DynamicListLookUpSettings1.DataMember = Nothing
        DynamicListLookUpSettings1.DataSource = Me.collectionDataSource1
        DynamicListLookUpSettings1.DisplayMember = "DocumentNoRV"
        DynamicListLookUpSettings1.FilterString = Nothing
        DynamicListLookUpSettings1.ValueMember = "DocumentNoRV"
        Me.EndDoc.LookUpSettings = DynamicListLookUpSettings1
        Me.EndDoc.Name = "EndDoc"
        '
        'DetailReport
        '
        Me.DetailReport.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.Detail, Me.GroupFooter1})
        Me.DetailReport.DataMember = "AccountID"
        Me.DetailReport.DataSource = Me.collectionDataSource1
        Me.DetailReport.Level = 0
        Me.DetailReport.Name = "DetailReport"
        '
        'Detail
        '
        Me.Detail.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrPivotGrid1})
        Me.Detail.HeightF = 46.875!
        Me.Detail.KeepTogether = True
        Me.Detail.Name = "Detail"
        '
        'GroupFooter1
        '
        Me.GroupFooter1.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.table2})
        Me.GroupFooter1.HeightF = 21.24999!
        Me.GroupFooter1.Name = "GroupFooter1"
        '
        'table2
        '
        Me.table2.Borders = CType(((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) _
            Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.table2.Font = New System.Drawing.Font("Angsana New", 12.0!)
        Me.table2.LocationFloat = New DevExpress.Utils.PointFloat(0.0!, 0.0!)
        Me.table2.Name = "table2"
        Me.table2.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.tableRow2})
        Me.table2.SizeF = New System.Drawing.SizeF(1075.0!, 21.24999!)
        Me.table2.StylePriority.UseBorders = False
        Me.table2.StylePriority.UseFont = False
        '
        'tableRow2
        '
        Me.tableRow2.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.tableCell14, Me.tableCell15, Me.tableCell16, Me.tableCell17, Me.tableCell18, Me.tableCell19, Me.tableCell20, Me.tableCell21, Me.tableCell22, Me.tableCell23, Me.tableCell24, Me.tableCell25, Me.tableCell26})
        Me.tableRow2.Name = "tableRow2"
        Me.tableRow2.Weight = 1.0R
        '
        'tableCell14
        '
        Me.tableCell14.Name = "tableCell14"
        Me.tableCell14.Weight = 0.59584682488744256R
        '
        'tableCell15
        '
        Me.tableCell15.Name = "tableCell15"
        Me.tableCell15.Weight = 0.55417919158935913R
        '
        'tableCell16
        '
        Me.tableCell16.Name = "tableCell16"
        Me.tableCell16.Weight = 0.39937477111816605R
        '
        'tableCell17
        '
        Me.tableCell17.Name = "tableCell17"
        Me.tableCell17.Weight = 0.54158721923827413R
        '
        'tableCell18
        '
        Me.tableCell18.Name = "tableCell18"
        Me.tableCell18.Weight = 0.50399291992187156R
        '
        'tableCell19
        '
        Me.tableCell19.Name = "tableCell19"
        Me.tableCell19.Weight = 0.54608154296874278R
        '
        'tableCell20
        '
        Me.tableCell20.Name = "tableCell20"
        Me.tableCell20.Weight = 0.49148559570313222R
        '
        'tableCell21
        '
        Me.tableCell21.Name = "tableCell21"
        Me.tableCell21.Weight = 0.71974517822263462R
        '
        'tableCell22
        '
        Me.tableCell22.Name = "tableCell22"
        Me.tableCell22.Weight = 1.3868949823524666R
        '
        'tableCell23
        '
        Me.tableCell23.Name = "tableCell23"
        Me.tableCell23.Weight = 1.37588372611813R
        '
        'tableCell24
        '
        Me.tableCell24.Font = New System.Drawing.Font("Angsana New", 12.0!, System.Drawing.FontStyle.Bold)
        Me.tableCell24.Name = "tableCell24"
        Me.tableCell24.StylePriority.UseFont = False
        Me.tableCell24.StylePriority.UseTextAlignment = False
        Me.tableCell24.Text = "ยอดรวม"
        Me.tableCell24.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        Me.tableCell24.Weight = 2.0041637358281266R
        '
        'tableCell25
        '
        Me.tableCell25.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "AccountID.Debit")})
        Me.tableCell25.Name = "tableCell25"
        Me.tableCell25.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 2, 0, 0, 100.0!)
        Me.tableCell25.StylePriority.UsePadding = False
        Me.tableCell25.StylePriority.UseTextAlignment = False
        XrSummary1.FormatString = "{0:n2}"
        XrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.tableCell25.Summary = XrSummary1
        Me.tableCell25.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        Me.tableCell25.Weight = 0.81676874496588558R
        '
        'tableCell26
        '
        Me.tableCell26.Borders = CType((((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) _
            Or DevExpress.XtraPrinting.BorderSide.Right) _
            Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.tableCell26.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "AccountID.Credit")})
        Me.tableCell26.Name = "tableCell26"
        Me.tableCell26.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 2, 0, 0, 100.0!)
        Me.tableCell26.StylePriority.UseBorders = False
        Me.tableCell26.StylePriority.UsePadding = False
        Me.tableCell26.StylePriority.UseTextAlignment = False
        XrSummary2.FormatString = "{0:n2}"
        XrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.tableCell26.Summary = XrSummary2
        Me.tableCell26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight
        Me.tableCell26.Weight = 0.813994086829753R
        '
        'XrTableCell23
        '
        Me.XrTableCell23.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.XrTableCell23.Borders = CType((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.XrTableCell23.BorderWidth = 1.0!
        Me.XrTableCell23.Font = New System.Drawing.Font("Angsana New", 12.0!, System.Drawing.FontStyle.Bold)
        Me.XrTableCell23.Name = "XrTableCell23"
        Me.XrTableCell23.StylePriority.UseBorderColor = False
        Me.XrTableCell23.StylePriority.UseBorders = False
        Me.XrTableCell23.StylePriority.UseBorderWidth = False
        Me.XrTableCell23.StylePriority.UseFont = False
        Me.XrTableCell23.Text = "จำนวน"
        Me.XrTableCell23.Weight = 1.3247799955127839R
        '
        'DataField
        '
        Me.DataField.BackColor = System.Drawing.Color.Transparent
        Me.DataField.BorderColor = System.Drawing.Color.Black
        Me.DataField.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.DataField.BorderWidth = 1.0!
        Me.DataField.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.DataField.ForeColor = System.Drawing.Color.Black
        Me.DataField.Name = "DataField"
        Me.DataField.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        '
        'EndDate
        '
        Me.EndDate.Description = "ถึงวันที่"
        Me.EndDate.Name = "EndDate"
        Me.EndDate.Type = GetType(Date)
        '
        'label22
        '
        Me.label22.BackColor = System.Drawing.Color.Transparent
        Me.label22.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.label22.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid
        Me.label22.Borders = CType(((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) _
            Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.label22.BorderWidth = 1.0!
        Me.label22.Font = New System.Drawing.Font("Angsana New", 12.0!, System.Drawing.FontStyle.Bold)
        Me.label22.ForeColor = System.Drawing.Color.Black
        Me.label22.LocationFloat = New DevExpress.Utils.PointFloat(59.58468!, 0.0!)
        Me.label22.Name = "label22"
        Me.label22.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.label22.SizeF = New System.Drawing.SizeF(55.41793!, 55.12498!)
        Me.label22.StylePriority.UseBackColor = False
        Me.label22.StylePriority.UseBorderColor = False
        Me.label22.StylePriority.UseBorderDashStyle = False
        Me.label22.StylePriority.UseBorders = False
        Me.label22.StylePriority.UseBorderWidth = False
        Me.label22.StylePriority.UseFont = False
        Me.label22.StylePriority.UseForeColor = False
        Me.label22.StylePriority.UsePadding = False
        Me.label22.StylePriority.UseTextAlignment = False
        Me.label22.Text = "เลขที่ใบสำคัญ"
        Me.label22.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrTableRow8
        '
        Me.XrTableRow8.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell21, Me.XrTableCell22, Me.XrTableCell23})
        Me.XrTableRow8.Name = "XrTableRow8"
        Me.XrTableRow8.Weight = 1.0R
        '
        'XrTableCell21
        '
        Me.XrTableCell21.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.XrTableCell21.Borders = CType((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.XrTableCell21.BorderWidth = 1.0!
        Me.XrTableCell21.Font = New System.Drawing.Font("Angsana New", 12.0!, System.Drawing.FontStyle.Bold)
        Me.XrTableCell21.Name = "XrTableCell21"
        Me.XrTableCell21.StylePriority.UseBorderColor = False
        Me.XrTableCell21.StylePriority.UseBorders = False
        Me.XrTableCell21.StylePriority.UseBorderWidth = False
        Me.XrTableCell21.StylePriority.UseFont = False
        Me.XrTableCell21.Text = "วันที่"
        Me.XrTableCell21.Weight = 0.99746180857916766R
        '
        'XrTableCell22
        '
        Me.XrTableCell22.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.XrTableCell22.Borders = CType((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.XrTableCell22.BorderWidth = 1.0!
        Me.XrTableCell22.Font = New System.Drawing.Font("Angsana New", 12.0!, System.Drawing.FontStyle.Bold)
        Me.XrTableCell22.Name = "XrTableCell22"
        Me.XrTableCell22.StylePriority.UseBorderColor = False
        Me.XrTableCell22.StylePriority.UseBorders = False
        Me.XrTableCell22.StylePriority.UseBorderWidth = False
        Me.XrTableCell22.StylePriority.UseFont = False
        Me.XrTableCell22.Text = "เลขที่ "
        Me.XrTableCell22.Weight = 0.90464025365159717R
        '
        'XrTableRow7
        '
        Me.XrTableRow7.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell18})
        Me.XrTableRow7.Name = "XrTableRow7"
        Me.XrTableRow7.Weight = 1.0R
        '
        'XrTableCell18
        '
        Me.XrTableCell18.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.XrTableCell18.Borders = CType(((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) _
            Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.XrTableCell18.BorderWidth = 1.0!
        Me.XrTableCell18.Font = New System.Drawing.Font("Angsana New", 12.0!, System.Drawing.FontStyle.Bold)
        Me.XrTableCell18.Name = "XrTableCell18"
        Me.XrTableCell18.StylePriority.UseBorderColor = False
        Me.XrTableCell18.StylePriority.UseBorders = False
        Me.XrTableCell18.StylePriority.UseBorderWidth = False
        Me.XrTableCell18.StylePriority.UseFont = False
        Me.XrTableCell18.Text = "เอกสารอ้างอิง น.ส.01"
        Me.XrTableCell18.Weight = 3.2268820577435489R
        '
        'XrTable3
        '
        Me.XrTable3.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.XrTable3.Borders = CType((((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) _
            Or DevExpress.XtraPrinting.BorderSide.Right) _
            Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.XrTable3.BorderWidth = 1.0!
        Me.XrTable3.Font = New System.Drawing.Font("Angsana New", 14.0!, System.Drawing.FontStyle.Bold)
        Me.XrTable3.LocationFloat = New DevExpress.Utils.PointFloat(911.9236!, 0.0!)
        Me.XrTable3.Name = "XrTable3"
        Me.XrTable3.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.XrTableRow5, Me.XrTableRow6})
        Me.XrTable3.SizeF = New System.Drawing.SizeF(163.0764!, 54.12498!)
        Me.XrTable3.StylePriority.UseBorderColor = False
        Me.XrTable3.StylePriority.UseBorders = False
        Me.XrTable3.StylePriority.UseBorderWidth = False
        Me.XrTable3.StylePriority.UseFont = False
        Me.XrTable3.StylePriority.UseTextAlignment = False
        Me.XrTable3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrTableRow5
        '
        Me.XrTableRow5.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell12})
        Me.XrTableRow5.Name = "XrTableRow5"
        Me.XrTableRow5.Weight = 1.0R
        '
        'XrTableCell12
        '
        Me.XrTableCell12.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.XrTableCell12.Borders = CType((((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) _
            Or DevExpress.XtraPrinting.BorderSide.Right) _
            Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.XrTableCell12.BorderWidth = 1.0!
        Me.XrTableCell12.Font = New System.Drawing.Font("Angsana New", 12.0!, System.Drawing.FontStyle.Bold)
        Me.XrTableCell12.Name = "XrTableCell12"
        Me.XrTableCell12.StylePriority.UseBorderColor = False
        Me.XrTableCell12.StylePriority.UseBorders = False
        Me.XrTableCell12.StylePriority.UseBorderWidth = False
        Me.XrTableCell12.StylePriority.UseFont = False
        Me.XrTableCell12.Text = "จำนวนเงิน"
        Me.XrTableCell12.Weight = 1.1720019551685319R
        '
        'XrTableRow6
        '
        Me.XrTableRow6.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell15, Me.XrTableCell17})
        Me.XrTableRow6.Name = "XrTableRow6"
        Me.XrTableRow6.Weight = 1.0R
        '
        'XrTableCell17
        '
        Me.XrTableCell17.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.XrTableCell17.Borders = CType(((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Right) _
            Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.XrTableCell17.BorderWidth = 1.0!
        Me.XrTableCell17.Font = New System.Drawing.Font("Angsana New", 12.0!, System.Drawing.FontStyle.Bold)
        Me.XrTableCell17.Name = "XrTableCell17"
        Me.XrTableCell17.StylePriority.UseBorderColor = False
        Me.XrTableCell17.StylePriority.UseBorders = False
        Me.XrTableCell17.StylePriority.UseBorderWidth = False
        Me.XrTableCell17.StylePriority.UseFont = False
        Me.XrTableCell17.Text = "เครดิต"
        Me.XrTableCell17.Weight = 0.585004614991233R
        '
        'Title
        '
        Me.Title.BackColor = System.Drawing.Color.Transparent
        Me.Title.BorderColor = System.Drawing.Color.Black
        Me.Title.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.Title.BorderWidth = 1.0!
        Me.Title.Font = New System.Drawing.Font("Times New Roman", 21.0!)
        Me.Title.ForeColor = System.Drawing.Color.Black
        Me.Title.Name = "Title"
        '
        'bottomMarginBand1
        '
        Me.bottomMarginBand1.HeightF = 13.87507!
        Me.bottomMarginBand1.Name = "bottomMarginBand1"
        '
        'label35
        '
        Me.label35.Font = New System.Drawing.Font("Angsana New", 14.0!)
        Me.label35.LocationFloat = New DevExpress.Utils.PointFloat(391.7486!, 66.66666!)
        Me.label35.Name = "label35"
        Me.label35.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.label35.SizeF = New System.Drawing.SizeF(63.48065!, 23.00001!)
        Me.label35.StylePriority.UseFont = False
        Me.label35.StylePriority.UseTextAlignment = False
        Me.label35.Text = "ตั้งแต่วันที่"
        Me.label35.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrTable1
        '
        Me.XrTable1.Font = New System.Drawing.Font("Angsana New", 14.0!)
        Me.XrTable1.LocationFloat = New DevExpress.Utils.PointFloat(0.0!, 0.0!)
        Me.XrTable1.Name = "XrTable1"
        Me.XrTable1.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.XrTableRow1, Me.XrTableRow3})
        Me.XrTable1.SizeF = New System.Drawing.SizeF(1053.0!, 66.66665!)
        Me.XrTable1.StylePriority.UseFont = False
        Me.XrTable1.StylePriority.UseTextAlignment = False
        Me.XrTable1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrTableRow1
        '
        Me.XrTableRow1.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell1})
        Me.XrTableRow1.Name = "XrTableRow1"
        Me.XrTableRow1.Weight = 1.0R
        '
        'XrTableCell1
        '
        Me.XrTableCell1.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Me.collectionDataSource3, "SiteName")})
        Me.XrTableCell1.Font = New System.Drawing.Font("Angsana New", 14.0!, System.Drawing.FontStyle.Bold)
        Me.XrTableCell1.Name = "XrTableCell1"
        Me.XrTableCell1.StylePriority.UseFont = False
        Me.XrTableCell1.StylePriority.UseTextAlignment = False
        Me.XrTableCell1.Text = "ศูนย์เมล็ดพันธุ์ข้าว จังหวัดนครสวรรค์"
        Me.XrTableCell1.Weight = 1.0R
        '
        'collectionDataSource3
        '
        Me.collectionDataSource3.Name = "collectionDataSource3"
        Me.collectionDataSource3.ObjectTypeName = "RiceManagementApp.Module.SiteSetting"
        Me.collectionDataSource3.TopReturnedRecords = 0
        '
        'XrTableRow3
        '
        Me.XrTableRow3.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell3})
        Me.XrTableRow3.Name = "XrTableRow3"
        Me.XrTableRow3.Weight = 1.0R
        '
        'XrTableCell3
        '
        Me.XrTableCell3.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "AccountBookRV.AccountBookName", "ทะเบียนคุม{0}")})
        Me.XrTableCell3.Font = New System.Drawing.Font("Angsana New", 14.0!)
        Me.XrTableCell3.Name = "XrTableCell3"
        Me.XrTableCell3.StylePriority.UseFont = False
        Me.XrTableCell3.StylePriority.UseTextAlignment = False
        Me.XrTableCell3.Text = "สมุดรายวันรับ"
        Me.XrTableCell3.Weight = 1.0R
        '
        'label34
        '
        Me.label34.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding(Me.EndDate, "Text", "{0:d MMMM yyyy}")})
        Me.label34.Font = New System.Drawing.Font("Angsana New", 14.0!)
        Me.label34.LocationFloat = New DevExpress.Utils.PointFloat(595.7093!, 66.66663!)
        Me.label34.Name = "label34"
        Me.label34.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.label34.SizeF = New System.Drawing.SizeF(106.3813!, 23.00002!)
        Me.label34.StylePriority.UseFont = False
        Me.label34.StylePriority.UseTextAlignment = False
        Me.label34.Text = "label2"
        Me.label34.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'PageInfo
        '
        Me.PageInfo.BackColor = System.Drawing.Color.Transparent
        Me.PageInfo.BorderColor = System.Drawing.Color.Black
        Me.PageInfo.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.PageInfo.BorderWidth = 1.0!
        Me.PageInfo.Font = New System.Drawing.Font("Arial", 8.0!)
        Me.PageInfo.ForeColor = System.Drawing.Color.Black
        Me.PageInfo.Name = "PageInfo"
        '
        'StartDate
        '
        Me.StartDate.Description = "วันที่"
        Me.StartDate.Name = "StartDate"
        Me.StartDate.Type = GetType(Date)
        '
        'pageFooterBand1
        '
        Me.pageFooterBand1.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.pageInfo1, Me.pageInfo2})
        Me.pageFooterBand1.HeightF = 50.0!
        Me.pageFooterBand1.Name = "pageFooterBand1"
        '
        'pageInfo1
        '
        Me.pageInfo1.Font = New System.Drawing.Font("Angsana New", 10.0!)
        Me.pageInfo1.Format = "พิมพ์วันที่ {0:d MMMM yyyy}"
        Me.pageInfo1.LocationFloat = New DevExpress.Utils.PointFloat(6.0!, 6.0!)
        Me.pageInfo1.Name = "pageInfo1"
        Me.pageInfo1.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.pageInfo1.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime
        Me.pageInfo1.SizeF = New System.Drawing.SizeF(438.0!, 23.0!)
        Me.pageInfo1.StyleName = "PageInfo"
        Me.pageInfo1.StylePriority.UseFont = False
        '
        'pageInfo2
        '
        Me.pageInfo2.Font = New System.Drawing.Font("Angsana New", 10.0!)
        Me.pageInfo2.Format = "หน้า {0} / {1}"
        Me.pageInfo2.LocationFloat = New DevExpress.Utils.PointFloat(636.9999!, 6.00001!)
        Me.pageInfo2.Name = "pageInfo2"
        Me.pageInfo2.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.pageInfo2.SizeF = New System.Drawing.SizeF(438.0!, 23.0!)
        Me.pageInfo2.StyleName = "PageInfo"
        Me.pageInfo2.StylePriority.UseFont = False
        Me.pageInfo2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'PageHeader
        '
        Me.PageHeader.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrTable4, Me.label28, Me.label27, Me.label26, Me.label25, Me.label24, Me.label23, Me.label22, Me.XrTable3, Me.label21})
        Me.PageHeader.Font = New System.Drawing.Font("Angsana New", 9.75!)
        Me.PageHeader.HeightF = 55.12498!
        Me.PageHeader.Name = "PageHeader"
        Me.PageHeader.StylePriority.UseFont = False
        '
        'XrTable4
        '
        Me.XrTable4.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.XrTable4.Borders = CType((DevExpress.XtraPrinting.BorderSide.Top Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.XrTable4.BorderWidth = 1.0!
        Me.XrTable4.Font = New System.Drawing.Font("Angsana New", 14.0!, System.Drawing.FontStyle.Bold)
        Me.XrTable4.LocationFloat = New DevExpress.Utils.PointFloat(259.9148!, 0.0!)
        Me.XrTable4.Name = "XrTable4"
        Me.XrTable4.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.XrTableRow7, Me.XrTableRow8})
        Me.XrTable4.SizeF = New System.Drawing.SizeF(175.3145!, 54.12498!)
        Me.XrTable4.StylePriority.UseBorderColor = False
        Me.XrTable4.StylePriority.UseBorders = False
        Me.XrTable4.StylePriority.UseBorderWidth = False
        Me.XrTable4.StylePriority.UseFont = False
        Me.XrTable4.StylePriority.UseTextAlignment = False
        Me.XrTable4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'label28
        '
        Me.label28.BackColor = System.Drawing.Color.Transparent
        Me.label28.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.label28.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid
        Me.label28.Borders = CType(((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) _
            Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.label28.BorderWidth = 1.0!
        Me.label28.Font = New System.Drawing.Font("Angsana New", 12.0!, System.Drawing.FontStyle.Bold)
        Me.label28.ForeColor = System.Drawing.Color.Black
        Me.label28.LocationFloat = New DevExpress.Utils.PointFloat(711.5071!, 0.0!)
        Me.label28.Name = "label28"
        Me.label28.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.label28.SizeF = New System.Drawing.SizeF(200.4164!, 55.12497!)
        Me.label28.StylePriority.UseBackColor = False
        Me.label28.StylePriority.UseBorderColor = False
        Me.label28.StylePriority.UseBorderDashStyle = False
        Me.label28.StylePriority.UseBorders = False
        Me.label28.StylePriority.UseBorderWidth = False
        Me.label28.StylePriority.UseFont = False
        Me.label28.StylePriority.UseForeColor = False
        Me.label28.StylePriority.UsePadding = False
        Me.label28.StylePriority.UseTextAlignment = False
        Me.label28.Text = "ชื่อบัญชี"
        Me.label28.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'label27
        '
        Me.label27.BackColor = System.Drawing.Color.Transparent
        Me.label27.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.label27.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid
        Me.label27.Borders = CType(((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) _
            Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.label27.BorderWidth = 1.0!
        Me.label27.Font = New System.Drawing.Font("Angsana New", 12.0!, System.Drawing.FontStyle.Bold)
        Me.label27.ForeColor = System.Drawing.Color.Black
        Me.label27.LocationFloat = New DevExpress.Utils.PointFloat(573.9188!, 0.0!)
        Me.label27.Name = "label27"
        Me.label27.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.label27.SizeF = New System.Drawing.SizeF(137.5884!, 55.12497!)
        Me.label27.StylePriority.UseBackColor = False
        Me.label27.StylePriority.UseBorderColor = False
        Me.label27.StylePriority.UseBorderDashStyle = False
        Me.label27.StylePriority.UseBorders = False
        Me.label27.StylePriority.UseBorderWidth = False
        Me.label27.StylePriority.UseFont = False
        Me.label27.StylePriority.UseForeColor = False
        Me.label27.StylePriority.UsePadding = False
        Me.label27.StylePriority.UseTextAlignment = False
        Me.label27.Text = "รหัสบัญชี"
        Me.label27.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'label26
        '
        Me.label26.BackColor = System.Drawing.Color.Transparent
        Me.label26.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.label26.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid
        Me.label26.Borders = CType(((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) _
            Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.label26.BorderWidth = 1.0!
        Me.label26.Font = New System.Drawing.Font("Angsana New", 12.0!, System.Drawing.FontStyle.Bold)
        Me.label26.ForeColor = System.Drawing.Color.Black
        Me.label26.LocationFloat = New DevExpress.Utils.PointFloat(435.2292!, 0.0!)
        Me.label26.Name = "label26"
        Me.label26.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.label26.SizeF = New System.Drawing.SizeF(138.6896!, 55.12497!)
        Me.label26.StylePriority.UseBackColor = False
        Me.label26.StylePriority.UseBorderColor = False
        Me.label26.StylePriority.UseBorderDashStyle = False
        Me.label26.StylePriority.UseBorders = False
        Me.label26.StylePriority.UseBorderWidth = False
        Me.label26.StylePriority.UseFont = False
        Me.label26.StylePriority.UseForeColor = False
        Me.label26.StylePriority.UsePadding = False
        Me.label26.StylePriority.UseTextAlignment = False
        Me.label26.Text = "รายการ"
        Me.label26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'label25
        '
        Me.label25.BackColor = System.Drawing.Color.Transparent
        Me.label25.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.label25.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid
        Me.label25.Borders = CType(((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) _
            Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.label25.BorderWidth = 1.0!
        Me.label25.Font = New System.Drawing.Font("Angsana New", 12.0!, System.Drawing.FontStyle.Bold)
        Me.label25.ForeColor = System.Drawing.Color.Black
        Me.label25.LocationFloat = New DevExpress.Utils.PointFloat(209.0988!, 0.0!)
        Me.label25.Name = "label25"
        Me.label25.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.label25.SizeF = New System.Drawing.SizeF(50.39929!, 55.12498!)
        Me.label25.StylePriority.UseBackColor = False
        Me.label25.StylePriority.UseBorderColor = False
        Me.label25.StylePriority.UseBorderDashStyle = False
        Me.label25.StylePriority.UseBorders = False
        Me.label25.StylePriority.UseBorderWidth = False
        Me.label25.StylePriority.UseFont = False
        Me.label25.StylePriority.UseForeColor = False
        Me.label25.StylePriority.UsePadding = False
        Me.label25.StylePriority.UseTextAlignment = False
        Me.label25.Text = "เลขที่ใบเสร็จ"
        Me.label25.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'label24
        '
        Me.label24.BackColor = System.Drawing.Color.Transparent
        Me.label24.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.label24.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid
        Me.label24.Borders = CType(((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) _
            Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.label24.BorderWidth = 1.0!
        Me.label24.Font = New System.Drawing.Font("Angsana New", 12.0!, System.Drawing.FontStyle.Bold)
        Me.label24.ForeColor = System.Drawing.Color.Black
        Me.label24.LocationFloat = New DevExpress.Utils.PointFloat(154.9609!, 0.0!)
        Me.label24.Name = "label24"
        Me.label24.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.label24.SizeF = New System.Drawing.SizeF(54.13788!, 55.12498!)
        Me.label24.StylePriority.UseBackColor = False
        Me.label24.StylePriority.UseBorderColor = False
        Me.label24.StylePriority.UseBorderDashStyle = False
        Me.label24.StylePriority.UseBorders = False
        Me.label24.StylePriority.UseBorderWidth = False
        Me.label24.StylePriority.UseFont = False
        Me.label24.StylePriority.UseForeColor = False
        Me.label24.StylePriority.UsePadding = False
        Me.label24.StylePriority.UseTextAlignment = False
        Me.label24.Text = "เลขที่ใบส่งของ(DOI)"
        Me.label24.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'label23
        '
        Me.label23.BackColor = System.Drawing.Color.Transparent
        Me.label23.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.label23.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid
        Me.label23.Borders = CType(((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) _
            Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.label23.BorderWidth = 1.0!
        Me.label23.Font = New System.Drawing.Font("Angsana New", 12.0!, System.Drawing.FontStyle.Bold)
        Me.label23.ForeColor = System.Drawing.Color.Black
        Me.label23.LocationFloat = New DevExpress.Utils.PointFloat(115.0026!, 0.0!)
        Me.label23.Name = "label23"
        Me.label23.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.label23.SizeF = New System.Drawing.SizeF(39.93748!, 55.12497!)
        Me.label23.StylePriority.UseBackColor = False
        Me.label23.StylePriority.UseBorderColor = False
        Me.label23.StylePriority.UseBorderDashStyle = False
        Me.label23.StylePriority.UseBorders = False
        Me.label23.StylePriority.UseBorderWidth = False
        Me.label23.StylePriority.UseFont = False
        Me.label23.StylePriority.UseForeColor = False
        Me.label23.StylePriority.UsePadding = False
        Me.label23.StylePriority.UseTextAlignment = False
        Me.label23.Text = "รายการที่"
        Me.label23.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'label21
        '
        Me.label21.BackColor = System.Drawing.Color.Transparent
        Me.label21.BorderColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.label21.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid
        Me.label21.Borders = CType(((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top) _
            Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.label21.BorderWidth = 1.0!
        Me.label21.Font = New System.Drawing.Font("Angsana New", 12.0!, System.Drawing.FontStyle.Bold)
        Me.label21.ForeColor = System.Drawing.Color.Black
        Me.label21.LocationFloat = New DevExpress.Utils.PointFloat(0.0!, 0.0!)
        Me.label21.Name = "label21"
        Me.label21.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.label21.SizeF = New System.Drawing.SizeF(59.58468!, 55.12498!)
        Me.label21.StylePriority.UseBackColor = False
        Me.label21.StylePriority.UseBorderColor = False
        Me.label21.StylePriority.UseBorderDashStyle = False
        Me.label21.StylePriority.UseBorders = False
        Me.label21.StylePriority.UseBorderWidth = False
        Me.label21.StylePriority.UseFont = False
        Me.label21.StylePriority.UseForeColor = False
        Me.label21.StylePriority.UsePadding = False
        Me.label21.StylePriority.UseTextAlignment = False
        Me.label21.Text = "วันที่"
        Me.label21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'StartDoc
        '
        Me.StartDoc.Description = "เลขที่ใบสำคัญ"
        DynamicListLookUpSettings2.DataAdapter = Nothing
        DynamicListLookUpSettings2.DataMember = Nothing
        DynamicListLookUpSettings2.DataSource = Me.collectionDataSource1
        DynamicListLookUpSettings2.DisplayMember = "DocumentNoRV"
        DynamicListLookUpSettings2.FilterString = Nothing
        DynamicListLookUpSettings2.ValueMember = "DocumentNoRV"
        Me.StartDoc.LookUpSettings = DynamicListLookUpSettings2
        Me.StartDoc.Name = "StartDoc"
        '
        'calculatedField1
        '
        Me.calculatedField1.Expression = "'ตั้งแต่วันที่' + '[Parameters.StartDate]' + 'ถึงวันที่' + '[Parameters.EndDate]'" & _
    ""
        Me.calculatedField1.Name = "calculatedField1"
        '
        'StartList
        '
        Me.StartList.Description = "รายการที่"
        DynamicListLookUpSettings3.DataAdapter = Nothing
        DynamicListLookUpSettings3.DataMember = Nothing
        DynamicListLookUpSettings3.DataSource = Me.collectionDataSource2
        DynamicListLookUpSettings3.DisplayMember = "ListNoRV"
        DynamicListLookUpSettings3.FilterString = Nothing
        DynamicListLookUpSettings3.ValueMember = "ListNoRV"
        Me.StartList.LookUpSettings = DynamicListLookUpSettings3
        Me.StartList.Name = "StartList"
        '
        'label33
        '
        Me.label33.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding(Me.StartDate, "Text", "{0:d MMMM yyyy}")})
        Me.label33.Font = New System.Drawing.Font("Angsana New", 14.0!)
        Me.label33.LocationFloat = New DevExpress.Utils.PointFloat(455.2292!, 66.66666!)
        Me.label33.Name = "label33"
        Me.label33.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.label33.SizeF = New System.Drawing.SizeF(95.5827!, 23.00001!)
        Me.label33.StylePriority.UseFont = False
        Me.label33.StylePriority.UseTextAlignment = False
        Me.label33.Text = "label1"
        Me.label33.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        '
        'EndList
        '
        Me.EndList.Description = "ถึง"
        DynamicListLookUpSettings4.DataAdapter = Nothing
        DynamicListLookUpSettings4.DataMember = Nothing
        DynamicListLookUpSettings4.DataSource = Me.collectionDataSource2
        DynamicListLookUpSettings4.DisplayMember = "ListNoRV"
        DynamicListLookUpSettings4.FilterString = Nothing
        DynamicListLookUpSettings4.ValueMember = "ListNoRV"
        Me.EndList.LookUpSettings = DynamicListLookUpSettings4
        Me.EndList.Name = "EndList"
        '
        'topMarginBand1
        '
        Me.topMarginBand1.HeightF = 44.00001!
        Me.topMarginBand1.Name = "topMarginBand1"
        '
        'reportHeaderBand1
        '
        Me.reportHeaderBand1.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrTable1, Me.label33, Me.label34, Me.label35, Me.label36})
        Me.reportHeaderBand1.HeightF = 89.66667!
        Me.reportHeaderBand1.Name = "reportHeaderBand1"
        '
        'label36
        '
        Me.label36.Font = New System.Drawing.Font("Angsana New", 14.0!)
        Me.label36.LocationFloat = New DevExpress.Utils.PointFloat(550.8119!, 66.66666!)
        Me.label36.Name = "label36"
        Me.label36.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.label36.SizeF = New System.Drawing.SizeF(44.8974!, 22.99999!)
        Me.label36.StylePriority.UseFont = False
        Me.label36.StylePriority.UseTextAlignment = False
        Me.label36.Text = "ถึงวันที่"
        Me.label36.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'detailBand1
        '
        Me.detailBand1.HeightF = 0.0!
        Me.detailBand1.KeepTogether = True
        Me.detailBand1.KeepTogetherWithDetailReports = True
        Me.detailBand1.Name = "detailBand1"
        Me.detailBand1.SortFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("DateListRV", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending), New DevExpress.XtraReports.UI.GroupField("ListNoRV", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})
        '
        'FieldCaption
        '
        Me.FieldCaption.BackColor = System.Drawing.Color.Transparent
        Me.FieldCaption.BorderColor = System.Drawing.Color.Black
        Me.FieldCaption.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.FieldCaption.BorderWidth = 1.0!
        Me.FieldCaption.Font = New System.Drawing.Font("Times New Roman", 10.0!)
        Me.FieldCaption.ForeColor = System.Drawing.Color.Black
        Me.FieldCaption.Name = "FieldCaption"
        '
        'AccBook
        '
        Me.AccBook.Description = "สมุดบัญชี"
        Me.AccBook.Name = "AccBook"
        '
        'SqlDataSource1
        '
        Me.SqlDataSource1.ConnectionName = "localhost\sql2012_RiceManagementDBConnection"
        Me.SqlDataSource1.Name = "SqlDataSource1"
        StoredProcQuery2.Name = "SP_Report_AccountBook_RV"
        QueryParameter8.Name = "@StartDate"
        QueryParameter8.Type = GetType(DevExpress.DataAccess.Expression)
        QueryParameter8.Value = New DevExpress.DataAccess.Expression("[Parameters.StartDate]", GetType(Date))
        QueryParameter9.Name = "@EandDate"
        QueryParameter9.Type = GetType(DevExpress.DataAccess.Expression)
        QueryParameter9.Value = New DevExpress.DataAccess.Expression("[Parameters.EndDate]", GetType(Date))
        QueryParameter10.Name = "@StartDocuNo"
        QueryParameter10.Type = GetType(DevExpress.DataAccess.Expression)
        QueryParameter10.Value = New DevExpress.DataAccess.Expression("[Parameters.StartDoc]", GetType(String))
        QueryParameter11.Name = "@EndDocuNo"
        QueryParameter11.Type = GetType(DevExpress.DataAccess.Expression)
        QueryParameter11.Value = New DevExpress.DataAccess.Expression("[Parameters.EndDoc]", GetType(String))
        QueryParameter12.Name = "@StartListNo"
        QueryParameter12.Type = GetType(DevExpress.DataAccess.Expression)
        QueryParameter12.Value = New DevExpress.DataAccess.Expression("[Parameters.StartList]", GetType(String))
        QueryParameter13.Name = "@EndListNo"
        QueryParameter13.Type = GetType(DevExpress.DataAccess.Expression)
        QueryParameter13.Value = New DevExpress.DataAccess.Expression("[Parameters.EndList]", GetType(String))
        QueryParameter14.Name = "@AccountBook"
        QueryParameter14.Type = GetType(DevExpress.DataAccess.Expression)
        QueryParameter14.Value = New DevExpress.DataAccess.Expression("[Parameters.AccBook]", GetType(String))
        StoredProcQuery2.Parameters.Add(QueryParameter8)
        StoredProcQuery2.Parameters.Add(QueryParameter9)
        StoredProcQuery2.Parameters.Add(QueryParameter10)
        StoredProcQuery2.Parameters.Add(QueryParameter11)
        StoredProcQuery2.Parameters.Add(QueryParameter12)
        StoredProcQuery2.Parameters.Add(QueryParameter13)
        StoredProcQuery2.Parameters.Add(QueryParameter14)
        StoredProcQuery2.StoredProcName = "SP_Report_AccountBook_RV"
        Me.SqlDataSource1.Queries.AddRange(New DevExpress.DataAccess.Sql.SqlQuery() {StoredProcQuery2})
        Me.SqlDataSource1.ResultSchemaSerializable = "PERhdGFTZXQgTmFtZT0iU3FsRGF0YVNvdXJjZTEiPjxWaWV3IE5hbWU9IlNQX1JlcG9ydF9BY2NvdW50Q" & _
    "m9va19SViIgLz48L0RhdGFTZXQ+"
        '
        'XrPivotGrid1
        '
        Me.XrPivotGrid1.DataMember = "SP_Report_AccountBook_RV"
        Me.XrPivotGrid1.DataSource = Me.SqlDataSource2
        Me.XrPivotGrid1.Fields.AddRange(New DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField() {Me.pivotGridField1})
        Me.XrPivotGrid1.LocationFloat = New DevExpress.Utils.PointFloat(0.0!, 0.0!)
        Me.XrPivotGrid1.Name = "XrPivotGrid1"
        Me.XrPivotGrid1.OLAPConnectionString = ""
        Me.XrPivotGrid1.OptionsPrint.FilterSeparatorBarPadding = 3
        Me.XrPivotGrid1.SizeF = New System.Drawing.SizeF(210.0985!, 46.875!)
        '
        'SqlDataSource2
        '
        Me.SqlDataSource2.ConnectionName = "localhost\sql2012_RiceManagementDBConnection"
        Me.SqlDataSource2.Name = "SqlDataSource2"
        StoredProcQuery1.Name = "SP_Report_AccountBook_RV"
        QueryParameter1.Name = "@StartDate"
        QueryParameter1.Type = GetType(DevExpress.DataAccess.Expression)
        QueryParameter1.Value = New DevExpress.DataAccess.Expression("[Parameters.StartDate]", GetType(Date))
        QueryParameter2.Name = "@EandDate"
        QueryParameter2.Type = GetType(DevExpress.DataAccess.Expression)
        QueryParameter2.Value = New DevExpress.DataAccess.Expression("[Parameters.EndDate]", GetType(Date))
        QueryParameter3.Name = "@StartDocuNo"
        QueryParameter3.Type = GetType(DevExpress.DataAccess.Expression)
        QueryParameter3.Value = New DevExpress.DataAccess.Expression("[Parameters.StartDoc]", GetType(String))
        QueryParameter4.Name = "@EndDocuNo"
        QueryParameter4.Type = GetType(DevExpress.DataAccess.Expression)
        QueryParameter4.Value = New DevExpress.DataAccess.Expression("[Parameters.EndDoc]", GetType(String))
        QueryParameter5.Name = "@StartListNo"
        QueryParameter5.Type = GetType(DevExpress.DataAccess.Expression)
        QueryParameter5.Value = New DevExpress.DataAccess.Expression("[Parameters.StartList]", GetType(String))
        QueryParameter6.Name = "@EndListNo"
        QueryParameter6.Type = GetType(DevExpress.DataAccess.Expression)
        QueryParameter6.Value = New DevExpress.DataAccess.Expression("[Parameters.EndList]", GetType(String))
        QueryParameter7.Name = "@AccountBook"
        QueryParameter7.Type = GetType(DevExpress.DataAccess.Expression)
        QueryParameter7.Value = New DevExpress.DataAccess.Expression("[Parameters.AccBook]", GetType(String))
        StoredProcQuery1.Parameters.Add(QueryParameter1)
        StoredProcQuery1.Parameters.Add(QueryParameter2)
        StoredProcQuery1.Parameters.Add(QueryParameter3)
        StoredProcQuery1.Parameters.Add(QueryParameter4)
        StoredProcQuery1.Parameters.Add(QueryParameter5)
        StoredProcQuery1.Parameters.Add(QueryParameter6)
        StoredProcQuery1.Parameters.Add(QueryParameter7)
        StoredProcQuery1.StoredProcName = "SP_Report_AccountBook_RV"
        Me.SqlDataSource2.Queries.AddRange(New DevExpress.DataAccess.Sql.SqlQuery() {StoredProcQuery1})
        Me.SqlDataSource2.ResultSchemaSerializable = "PERhdGFTZXQgTmFtZT0iU3FsRGF0YVNvdXJjZTIiPjxWaWV3IE5hbWU9IlNQX1JlcG9ydF9BY2NvdW50Q" & _
    "m9va19SViIgLz48L0RhdGFTZXQ+"
        '
        'pivotGridField1
        '
        Me.pivotGridField1.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea
        Me.pivotGridField1.AreaIndex = 0
        Me.pivotGridField1.Name = "pivotGridField1"
        '
        'AccountBook
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.detailBand1, Me.pageFooterBand1, Me.reportHeaderBand1, Me.topMarginBand1, Me.bottomMarginBand1, Me.DetailReport, Me.PageHeader})
        Me.CalculatedFields.AddRange(New DevExpress.XtraReports.UI.CalculatedField() {Me.calculatedField1})
        Me.ComponentStorage.Add(Me.collectionDataSource1)
        Me.ComponentStorage.Add(Me.collectionDataSource2)
        Me.ComponentStorage.Add(Me.collectionDataSource3)
        Me.ComponentStorage.Add(Me.SqlDataSource1)
        Me.ComponentStorage.Add(Me.SqlDataSource2)
        Me.DataSource = Me.collectionDataSource1
        Me.DisplayName = "สมุดรายวันรับ"
        Me.Extensions.Add("DataSerializationExtension", "XtraReport")
        Me.Extensions.Add("DataEditorExtension", "XtraReport")
        Me.Extensions.Add("ParameterEditorExtension", "XtraReport")
        Me.FilterString = Nothing
        Me.Landscape = True
        Me.Margins = New System.Drawing.Printing.Margins(11, 14, 44, 14)
        Me.PageHeight = 850
        Me.PageWidth = 1100
        Me.Parameters.AddRange(New DevExpress.XtraReports.Parameters.Parameter() {Me.StartDate, Me.EndDate, Me.StartDoc, Me.EndDoc, Me.StartList, Me.EndList, Me.AccBook})
        Me.StyleSheet.AddRange(New DevExpress.XtraReports.UI.XRControlStyle() {Me.Title, Me.FieldCaption, Me.PageInfo, Me.DataField})
        Me.Version = "14.2"
        CType(Me.collectionDataSource1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.collectionDataSource2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.table2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XrTable3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XrTable1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.collectionDataSource3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XrTable4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents XrTableCell15 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents EndDoc As DevExpress.XtraReports.Parameters.Parameter
    Friend WithEvents collectionDataSource1 As DevExpress.Persistent.Base.ReportsV2.CollectionDataSource
    Friend WithEvents DetailReport As DevExpress.XtraReports.UI.DetailReportBand
    Friend WithEvents Detail As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents GroupFooter1 As DevExpress.XtraReports.UI.GroupFooterBand
    Friend WithEvents table2 As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents tableRow2 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents tableCell14 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell15 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell16 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell17 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell18 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell19 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell20 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell21 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell22 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell23 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell24 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell25 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell26 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell23 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents DataField As DevExpress.XtraReports.UI.XRControlStyle
    Friend WithEvents EndDate As DevExpress.XtraReports.Parameters.Parameter
    Friend WithEvents label22 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrTableRow8 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell21 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell22 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableRow7 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell18 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTable3 As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents XrTableRow5 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell12 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableRow6 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell17 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents Title As DevExpress.XtraReports.UI.XRControlStyle
    Friend WithEvents bottomMarginBand1 As DevExpress.XtraReports.UI.BottomMarginBand
    Friend WithEvents collectionDataSource2 As DevExpress.Persistent.Base.ReportsV2.CollectionDataSource
    Friend WithEvents label35 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrTable1 As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents XrTableRow1 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell1 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents collectionDataSource3 As DevExpress.Persistent.Base.ReportsV2.CollectionDataSource
    Friend WithEvents XrTableRow3 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell3 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents label34 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents PageInfo As DevExpress.XtraReports.UI.XRControlStyle
    Friend WithEvents StartDate As DevExpress.XtraReports.Parameters.Parameter
    Friend WithEvents pageFooterBand1 As DevExpress.XtraReports.UI.PageFooterBand
    Friend WithEvents pageInfo1 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents pageInfo2 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents PageHeader As DevExpress.XtraReports.UI.PageHeaderBand
    Friend WithEvents XrTable4 As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents label28 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents label27 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents label26 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents label25 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents label24 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents label23 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents label21 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents StartDoc As DevExpress.XtraReports.Parameters.Parameter
    Friend WithEvents calculatedField1 As DevExpress.XtraReports.UI.CalculatedField
    Friend WithEvents StartList As DevExpress.XtraReports.Parameters.Parameter
    Friend WithEvents label33 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents EndList As DevExpress.XtraReports.Parameters.Parameter
    Friend WithEvents topMarginBand1 As DevExpress.XtraReports.UI.TopMarginBand
    Friend WithEvents reportHeaderBand1 As DevExpress.XtraReports.UI.ReportHeaderBand
    Friend WithEvents label36 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents detailBand1 As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents FieldCaption As DevExpress.XtraReports.UI.XRControlStyle
    Friend WithEvents AccBook As DevExpress.XtraReports.Parameters.Parameter
    Friend WithEvents SqlDataSource1 As DevExpress.DataAccess.Sql.SqlDataSource
    Friend WithEvents XrPivotGrid1 As DevExpress.XtraReports.UI.XRPivotGrid
    Friend WithEvents SqlDataSource2 As DevExpress.DataAccess.Sql.SqlDataSource
    Friend WithEvents pivotGridField1 As DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField
End Class
