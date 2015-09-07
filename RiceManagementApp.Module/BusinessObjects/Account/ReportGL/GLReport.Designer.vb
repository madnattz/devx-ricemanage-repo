<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Public Class GLReport
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
        Dim StoredProcQuery1 As DevExpress.DataAccess.Sql.StoredProcQuery = New DevExpress.DataAccess.Sql.StoredProcQuery()
        Dim QueryParameter1 As DevExpress.DataAccess.Sql.QueryParameter = New DevExpress.DataAccess.Sql.QueryParameter()
        Dim QueryParameter2 As DevExpress.DataAccess.Sql.QueryParameter = New DevExpress.DataAccess.Sql.QueryParameter()
        Dim QueryParameter3 As DevExpress.DataAccess.Sql.QueryParameter = New DevExpress.DataAccess.Sql.QueryParameter()
        Dim QueryParameter4 As DevExpress.DataAccess.Sql.QueryParameter = New DevExpress.DataAccess.Sql.QueryParameter()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(GLReport))
        Me.PrmDatasource = New DevExpress.Persistent.Base.ReportsV2.CollectionDataSource()
        Me.pageInfo1 = New DevExpress.XtraReports.UI.XRPageInfo()
        Me.tableCell2 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.FieldCaption = New DevExpress.XtraReports.UI.XRControlStyle()
        Me.label2 = New DevExpress.XtraReports.UI.XRLabel()
        Me.label1 = New DevExpress.XtraReports.UI.XRLabel()
        Me.line3 = New DevExpress.XtraReports.UI.XRLine()
        Me.tableCell9 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell10 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.StartAccID = New DevExpress.XtraReports.Parameters.Parameter()
        Me.tableRow1 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.XrTableCell7 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell5 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell1 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell7 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell4 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.GroupFooter1 = New DevExpress.XtraReports.UI.GroupFooterBand()
        Me.XrLine1 = New DevExpress.XtraReports.UI.XRLine()
        Me.XrLabel4 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrTable2 = New DevExpress.XtraReports.UI.XRTable()
        Me.XrTableRow2 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.XrTableCell4 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell5 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.XrTableCell6 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.line1 = New DevExpress.XtraReports.UI.XRLine()
        Me.table1 = New DevExpress.XtraReports.UI.XRTable()
        Me.detailBand1 = New DevExpress.XtraReports.UI.DetailBand()
        Me.PageInfo = New DevExpress.XtraReports.UI.XRControlStyle()
        Me.topMarginBand1 = New DevExpress.XtraReports.UI.TopMarginBand()
        Me.Title = New DevExpress.XtraReports.UI.XRControlStyle()
        Me.pageInfo2 = New DevExpress.XtraReports.UI.XRPageInfo()
        Me.pageFooterBand1 = New DevExpress.XtraReports.UI.PageFooterBand()
        Me.DataField = New DevExpress.XtraReports.UI.XRControlStyle()
        Me.label33 = New DevExpress.XtraReports.UI.XRLabel()
        Me.SiteSettingDs = New DevExpress.Persistent.Base.ReportsV2.CollectionDataSource()
        Me.reportHeaderBand1 = New DevExpress.XtraReports.UI.ReportHeaderBand()
        Me.XrLabel1 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel2 = New DevExpress.XtraReports.UI.XRLabel()
        Me.XrLabel3 = New DevExpress.XtraReports.UI.XRLabel()
        Me.StartDate = New DevExpress.XtraReports.Parameters.Parameter()
        Me.XrLabel6 = New DevExpress.XtraReports.UI.XRLabel()
        Me.EndDate = New DevExpress.XtraReports.Parameters.Parameter()
        Me.table3 = New DevExpress.XtraReports.UI.XRTable()
        Me.tableRow3 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.tableCell14 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell11 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell12 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell16 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell17 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell13 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.bottomMarginBand1 = New DevExpress.XtraReports.UI.BottomMarginBand()
        Me.CalculatedField1 = New DevExpress.XtraReports.UI.CalculatedField()
        Me.CalculatedField2 = New DevExpress.XtraReports.UI.CalculatedField()
        Me.CalculatedField3 = New DevExpress.XtraReports.UI.CalculatedField()
        Me.CalculatedField4 = New DevExpress.XtraReports.UI.CalculatedField()
        Me.TotalDrAmount = New DevExpress.XtraReports.UI.CalculatedField()
        Me.TotalCrAmount = New DevExpress.XtraReports.UI.CalculatedField()
        Me.EndAccID = New DevExpress.XtraReports.Parameters.Parameter()
        Me.GroupHeader1 = New DevExpress.XtraReports.UI.GroupHeaderBand()
        Me.PageHeader = New DevExpress.XtraReports.UI.PageHeaderBand()
        Me.XrLabel5 = New DevExpress.XtraReports.UI.XRLabel()
        Me.table4 = New DevExpress.XtraReports.UI.XRTable()
        Me.tableRow4 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.tableCell18 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.label7 = New DevExpress.XtraReports.UI.XRLabel()
        Me.label6 = New DevExpress.XtraReports.UI.XRLabel()
        Me.label5 = New DevExpress.XtraReports.UI.XRLabel()
        Me.label4 = New DevExpress.XtraReports.UI.XRLabel()
        Me.label3 = New DevExpress.XtraReports.UI.XRLabel()
        Me.table2 = New DevExpress.XtraReports.UI.XRTable()
        Me.tableRow2 = New DevExpress.XtraReports.UI.XRTableRow()
        Me.tableCell3 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell6 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.tableCell8 = New DevExpress.XtraReports.UI.XRTableCell()
        Me.SqlDataSource1 = New DevExpress.DataAccess.Sql.SqlDataSource()
        Me.SUMTotal = New DevExpress.XtraReports.UI.CalculatedField()
        CType(Me.PrmDatasource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XrTable2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.table1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SiteSettingDs, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.table3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.table4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.table2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'PrmDatasource
        '
        Me.PrmDatasource.Name = "PrmDatasource"
        Me.PrmDatasource.ObjectTypeName = "RiceManagementApp.Module.Account"
        Me.PrmDatasource.Sorting.AddRange(New DevExpress.Xpo.SortProperty() {New DevExpress.Xpo.SortProperty("[AccountID]", DevExpress.Xpo.DB.SortingDirection.Ascending)})
        Me.PrmDatasource.TopReturnedRecords = 0
        '
        'pageInfo1
        '
        Me.pageInfo1.Font = New System.Drawing.Font("AngsanaUPC", 10.0!)
        Me.pageInfo1.Format = "พิมพ์วันที่ {0:d MMMM yyyy}"
        Me.pageInfo1.LocationFloat = New DevExpress.Utils.PointFloat(6.000002!, 6.00001!)
        Me.pageInfo1.Name = "pageInfo1"
        Me.pageInfo1.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.pageInfo1.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime
        Me.pageInfo1.SizeF = New System.Drawing.SizeF(290.0!, 23.0!)
        Me.pageInfo1.StyleName = "PageInfo"
        Me.pageInfo1.StylePriority.UseFont = False
        '
        'tableCell2
        '
        Me.tableCell2.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SP_GetGLV2.AccDetail")})
        Me.tableCell2.Name = "tableCell2"
        Me.tableCell2.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 100.0!)
        Me.tableCell2.StylePriority.UsePadding = False
        Me.tableCell2.Weight = 1.1086405867282034R
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
        'label2
        '
        Me.label2.Font = New System.Drawing.Font("AngsanaUPC", 14.0!, System.Drawing.FontStyle.Bold)
        Me.label2.LocationFloat = New DevExpress.Utils.PointFloat(0.0!, 60.08333!)
        Me.label2.Name = "label2"
        Me.label2.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.label2.SizeF = New System.Drawing.SizeF(786.9999!, 26.41666!)
        Me.label2.StylePriority.UseFont = False
        Me.label2.StylePriority.UseTextAlignment = False
        Me.label2.Text = "บัญชีแยกประเภททั่วไป"
        Me.label2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'label1
        '
        Me.label1.Font = New System.Drawing.Font("AngsanaUPC", 14.0!, System.Drawing.FontStyle.Bold)
        Me.label1.LocationFloat = New DevExpress.Utils.PointFloat(0.0!, 32.00001!)
        Me.label1.Name = "label1"
        Me.label1.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.label1.SizeF = New System.Drawing.SizeF(786.9999!, 28.08332!)
        Me.label1.StylePriority.UseFont = False
        Me.label1.StylePriority.UseTextAlignment = False
        Me.label1.Text = "ประเภทบัญชีเงินทุนหมุนเวียนเพื่อผลิต และขยายพันธุ์พืช"
        Me.label1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'line3
        '
        Me.line3.LocationFloat = New DevExpress.Utils.PointFloat(541.0!, 32.33287!)
        Me.line3.Name = "line3"
        Me.line3.SizeF = New System.Drawing.SizeF(245.9999!, 2.166653!)
        '
        'tableCell9
        '
        Me.tableCell9.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SP_GetGLV2.DrAmnt", "{0:n2}")})
        Me.tableCell9.Name = "tableCell9"
        Me.tableCell9.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 2, 0, 0, 100.0!)
        Me.tableCell9.StylePriority.UsePadding = False
        Me.tableCell9.StylePriority.UseTextAlignment = False
        Me.tableCell9.Text = "tableCell9"
        Me.tableCell9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        Me.tableCell9.Weight = 0.31755349823558726R
        '
        'tableCell10
        '
        Me.tableCell10.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SP_GetGLV2.CrAmnt", "{0:n2}")})
        Me.tableCell10.Name = "tableCell10"
        Me.tableCell10.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 2, 0, 0, 100.0!)
        Me.tableCell10.StylePriority.UsePadding = False
        Me.tableCell10.StylePriority.UseTextAlignment = False
        Me.tableCell10.Text = "tableCell10"
        Me.tableCell10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        Me.tableCell10.Weight = 0.31014622181229329R
        '
        'StartAccID
        '
        Me.StartAccID.Description = "จากบัญชี"
        DynamicListLookUpSettings1.DataAdapter = Nothing
        DynamicListLookUpSettings1.DataMember = Nothing
        DynamicListLookUpSettings1.DataSource = Me.PrmDatasource
        DynamicListLookUpSettings1.DisplayMember = "AccountName"
        DynamicListLookUpSettings1.FilterString = "[PublicStatus] = ##Enum#RiceManagementApp.Module.PublicStatus,Activate#"
        DynamicListLookUpSettings1.ValueMember = "AccountID"
        Me.StartAccID.LookUpSettings = DynamicListLookUpSettings1
        Me.StartAccID.Name = "StartAccID"
        '
        'tableRow1
        '
        Me.tableRow1.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell7, Me.tableCell5, Me.tableCell1, Me.tableCell7, Me.tableCell2, Me.tableCell9, Me.tableCell10, Me.tableCell4})
        Me.tableRow1.Name = "tableRow1"
        Me.tableRow1.Weight = 1.0R
        '
        'XrTableCell7
        '
        Me.XrTableCell7.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SP_GetGLV2.AccountBook")})
        Me.XrTableCell7.Name = "XrTableCell7"
        Me.XrTableCell7.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 100.0!)
        Me.XrTableCell7.StylePriority.UsePadding = False
        Me.XrTableCell7.StylePriority.UseTextAlignment = False
        Me.XrTableCell7.Text = "XrTableCell7"
        Me.XrTableCell7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        Me.XrTableCell7.Weight = 0.15817896481333188R
        '
        'tableCell5
        '
        Me.tableCell5.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SP_GetGLV2.DocuDate", "{0:d MMM yy}")})
        Me.tableCell5.Name = "tableCell5"
        Me.tableCell5.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 100.0!)
        Me.tableCell5.StylePriority.UsePadding = False
        Me.tableCell5.StylePriority.UseTextAlignment = False
        Me.tableCell5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        Me.tableCell5.Weight = 0.27237667236763691R
        '
        'tableCell1
        '
        Me.tableCell1.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SP_GetGLV2.DocuNo")})
        Me.tableCell1.Name = "tableCell1"
        Me.tableCell1.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 100.0!)
        Me.tableCell1.StylePriority.UsePadding = False
        Me.tableCell1.StylePriority.UseTextAlignment = False
        Me.tableCell1.Text = "tableCell1"
        Me.tableCell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        Me.tableCell1.Weight = 0.30400639193565576R
        '
        'tableCell7
        '
        Me.tableCell7.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SP_GetGLV2.ListNo")})
        Me.tableCell7.Name = "tableCell7"
        Me.tableCell7.StylePriority.UseTextAlignment = False
        Me.tableCell7.Text = "tableCell7"
        Me.tableCell7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        Me.tableCell7.Weight = 0.21524710790477528R
        '
        'tableCell4
        '
        Me.tableCell4.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SP_GetGLV2.SUMTotal", "{0:n2}")})
        Me.tableCell4.Name = "tableCell4"
        Me.tableCell4.Padding = New DevExpress.XtraPrinting.PaddingInfo(0, 2, 0, 0, 100.0!)
        Me.tableCell4.StylePriority.UsePadding = False
        Me.tableCell4.StylePriority.UseTextAlignment = False
        Me.tableCell4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        Me.tableCell4.Weight = 0.31385072259898733R
        '
        'GroupFooter1
        '
        Me.GroupFooter1.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLine1, Me.XrLabel4, Me.XrTable2, Me.line3, Me.line1})
        Me.GroupFooter1.HeightF = 34.49952!
        Me.GroupFooter1.KeepTogether = True
        Me.GroupFooter1.Name = "GroupFooter1"
        '
        'XrLine1
        '
        Me.XrLine1.LocationFloat = New DevExpress.Utils.PointFloat(539.9995!, 0.0!)
        Me.XrLine1.Name = "XrLine1"
        Me.XrLine1.SizeF = New System.Drawing.SizeF(246.0!, 2.166653!)
        '
        'XrLabel4
        '
        Me.XrLabel4.Font = New System.Drawing.Font("AngsanaUPC", 12.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel4.LocationFloat = New DevExpress.Utils.PointFloat(439.9999!, 4.83284!)
        Me.XrLabel4.Name = "XrLabel4"
        Me.XrLabel4.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel4.SizeF = New System.Drawing.SizeF(76.99997!, 25.0!)
        Me.XrLabel4.StylePriority.UseFont = False
        Me.XrLabel4.Text = "ยอดรวม"
        '
        'XrTable2
        '
        Me.XrTable2.Font = New System.Drawing.Font("AngsanaUPC", 12.0!, System.Drawing.FontStyle.Bold)
        Me.XrTable2.LocationFloat = New DevExpress.Utils.PointFloat(541.0001!, 4.83284!)
        Me.XrTable2.Name = "XrTable2"
        Me.XrTable2.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.XrTableRow2})
        Me.XrTable2.SizeF = New System.Drawing.SizeF(245.9999!, 25.0!)
        Me.XrTable2.StylePriority.UseFont = False
        '
        'XrTableRow2
        '
        Me.XrTableRow2.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.XrTableCell4, Me.XrTableCell5, Me.XrTableCell6})
        Me.XrTableRow2.Name = "XrTableRow2"
        Me.XrTableRow2.Weight = 1.0R
        '
        'XrTableCell4
        '
        Me.XrTableCell4.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SP_GetGLV2.DrAmnt")})
        Me.XrTableCell4.Name = "XrTableCell4"
        Me.XrTableCell4.StylePriority.UseTextAlignment = False
        XrSummary1.FormatString = "{0:n2}"
        XrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.XrTableCell4.Summary = XrSummary1
        Me.XrTableCell4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        Me.XrTableCell4.Weight = 0.89999999999999991R
        '
        'XrTableCell5
        '
        Me.XrTableCell5.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SP_GetGLV2.CrAmnt")})
        Me.XrTableCell5.Name = "XrTableCell5"
        Me.XrTableCell5.StylePriority.UseTextAlignment = False
        XrSummary2.FormatString = "{0:n2}"
        XrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Group
        Me.XrTableCell5.Summary = XrSummary2
        Me.XrTableCell5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        Me.XrTableCell5.Weight = 0.88968567724135439R
        '
        'XrTableCell6
        '
        Me.XrTableCell6.Name = "XrTableCell6"
        Me.XrTableCell6.Weight = 0.90031432275864576R
        '
        'line1
        '
        Me.line1.LocationFloat = New DevExpress.Utils.PointFloat(539.9998!, 29.83284!)
        Me.line1.Name = "line1"
        Me.line1.SizeF = New System.Drawing.SizeF(245.9998!, 2.500023!)
        '
        'table1
        '
        Me.table1.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.table1.Font = New System.Drawing.Font("AngsanaUPC", 12.0!)
        Me.table1.LocationFloat = New DevExpress.Utils.PointFloat(0.0!, 0.0!)
        Me.table1.Name = "table1"
        Me.table1.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.tableRow1})
        Me.table1.SizeF = New System.Drawing.SizeF(787.0001!, 36.45833!)
        Me.table1.StylePriority.UseBorders = False
        Me.table1.StylePriority.UseFont = False
        '
        'detailBand1
        '
        Me.detailBand1.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.table1})
        Me.detailBand1.HeightF = 36.45833!
        Me.detailBand1.KeepTogether = True
        Me.detailBand1.MultiColumn.Layout = DevExpress.XtraPrinting.ColumnLayout.AcrossThenDown
        Me.detailBand1.Name = "detailBand1"
        Me.detailBand1.SortFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("DocuDate", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})
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
        'topMarginBand1
        '
        Me.topMarginBand1.HeightF = 47.0!
        Me.topMarginBand1.Name = "topMarginBand1"
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
        'pageInfo2
        '
        Me.pageInfo2.Font = New System.Drawing.Font("AngsanaUPC", 10.0!)
        Me.pageInfo2.Format = "หน้า {0} / {1}"
        Me.pageInfo2.LocationFloat = New DevExpress.Utils.PointFloat(497.0!, 6.00001!)
        Me.pageInfo2.Name = "pageInfo2"
        Me.pageInfo2.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.pageInfo2.SizeF = New System.Drawing.SizeF(290.0!, 23.0!)
        Me.pageInfo2.StyleName = "PageInfo"
        Me.pageInfo2.StylePriority.UseFont = False
        Me.pageInfo2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight
        '
        'pageFooterBand1
        '
        Me.pageFooterBand1.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.pageInfo1, Me.pageInfo2})
        Me.pageFooterBand1.HeightF = 29.00001!
        Me.pageFooterBand1.Name = "pageFooterBand1"
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
        'label33
        '
        Me.label33.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Me.SiteSettingDs, "SiteName")})
        Me.label33.Font = New System.Drawing.Font("AngsanaUPC", 14.0!, System.Drawing.FontStyle.Bold)
        Me.label33.LocationFloat = New DevExpress.Utils.PointFloat(0.0!, 6.00001!)
        Me.label33.Name = "label33"
        Me.label33.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.label33.SizeF = New System.Drawing.SizeF(786.9999!, 26.0!)
        Me.label33.StyleName = "Title"
        Me.label33.StylePriority.UseFont = False
        Me.label33.StylePriority.UseTextAlignment = False
        Me.label33.Text = "ศูนย์เมล็ดพันธุ์ข้าว จังหวัดนครสวรรค์"
        Me.label33.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'SiteSettingDs
        '
        Me.SiteSettingDs.Name = "SiteSettingDs"
        Me.SiteSettingDs.ObjectTypeName = "RiceManagementApp.Module.SiteSetting"
        Me.SiteSettingDs.TopReturnedRecords = 0
        '
        'reportHeaderBand1
        '
        Me.reportHeaderBand1.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel1, Me.XrLabel2, Me.XrLabel3, Me.XrLabel6, Me.label2, Me.label1, Me.label33})
        Me.reportHeaderBand1.HeightF = 122.9167!
        Me.reportHeaderBand1.Name = "reportHeaderBand1"
        '
        'XrLabel1
        '
        Me.XrLabel1.Font = New System.Drawing.Font("AngsanaUPC", 12.0!)
        Me.XrLabel1.LocationFloat = New DevExpress.Utils.PointFloat(260.8587!, 86.49998!)
        Me.XrLabel1.Name = "XrLabel1"
        Me.XrLabel1.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel1.SizeF = New System.Drawing.SizeF(34.6582!, 22.99999!)
        Me.XrLabel1.StylePriority.UseFont = False
        Me.XrLabel1.StylePriority.UseTextAlignment = False
        Me.XrLabel1.Text = "วันที่"
        Me.XrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrLabel2
        '
        Me.XrLabel2.Font = New System.Drawing.Font("AngsanaUPC", 12.0!)
        Me.XrLabel2.LocationFloat = New DevExpress.Utils.PointFloat(419.4751!, 86.49998!)
        Me.XrLabel2.Name = "XrLabel2"
        Me.XrLabel2.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel2.SizeF = New System.Drawing.SizeF(19.52466!, 22.99999!)
        Me.XrLabel2.StylePriority.UseFont = False
        Me.XrLabel2.StylePriority.UseTextAlignment = False
        Me.XrLabel2.Text = "ถึง"
        Me.XrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'XrLabel3
        '
        Me.XrLabel3.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding(Me.StartDate, "Text", "{0:d MMMM yyyy}")})
        Me.XrLabel3.Font = New System.Drawing.Font("AngsanaUPC", 12.0!)
        Me.XrLabel3.LocationFloat = New DevExpress.Utils.PointFloat(295.5169!, 86.49998!)
        Me.XrLabel3.Name = "XrLabel3"
        Me.XrLabel3.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel3.SizeF = New System.Drawing.SizeF(123.9582!, 22.99999!)
        Me.XrLabel3.StylePriority.UseFont = False
        Me.XrLabel3.StylePriority.UseTextAlignment = False
        Me.XrLabel3.Text = "XrLabel3"
        Me.XrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'StartDate
        '
        Me.StartDate.Description = "วันที่"
        Me.StartDate.Name = "StartDate"
        Me.StartDate.Type = GetType(Date)
        Me.StartDate.Value = Today
        '
        'XrLabel6
        '
        Me.XrLabel6.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding(Me.EndDate, "Text", "{0:d MMMM yyyy}")})
        Me.XrLabel6.Font = New System.Drawing.Font("AngsanaUPC", 12.0!)
        Me.XrLabel6.LocationFloat = New DevExpress.Utils.PointFloat(438.9998!, 86.49998!)
        Me.XrLabel6.Name = "XrLabel6"
        Me.XrLabel6.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel6.SizeF = New System.Drawing.SizeF(123.9582!, 22.99999!)
        Me.XrLabel6.StylePriority.UseFont = False
        Me.XrLabel6.StylePriority.UseTextAlignment = False
        Me.XrLabel6.Text = "XrLabel4"
        Me.XrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'EndDate
        '
        Me.EndDate.Description = "ถึง"
        Me.EndDate.Name = "EndDate"
        Me.EndDate.Type = GetType(Date)
        Me.EndDate.Value = Today
        '
        'table3
        '
        Me.table3.BackColor = System.Drawing.Color.White
        Me.table3.Borders = DevExpress.XtraPrinting.BorderSide.None
        Me.table3.Font = New System.Drawing.Font("AngsanaUPC", 12.0!, System.Drawing.FontStyle.Bold)
        Me.table3.LocationFloat = New DevExpress.Utils.PointFloat(0.0!, 0.0!)
        Me.table3.Name = "table3"
        Me.table3.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.tableRow3})
        Me.table3.SizeF = New System.Drawing.SizeF(785.9996!, 29.58333!)
        Me.table3.StylePriority.UseBackColor = False
        Me.table3.StylePriority.UseBorders = False
        Me.table3.StylePriority.UseFont = False
        Me.table3.StylePriority.UseTextAlignment = False
        Me.table3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'tableRow3
        '
        Me.tableRow3.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.tableCell14, Me.tableCell11, Me.tableCell12, Me.tableCell16, Me.tableCell17, Me.tableCell13})
        Me.tableRow3.Name = "tableRow3"
        Me.tableRow3.Weight = 1.0R
        '
        'tableCell14
        '
        Me.tableCell14.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SP_GetGLV2.AccountID")})
        Me.tableCell14.Name = "tableCell14"
        Me.tableCell14.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 100.0!)
        Me.tableCell14.StylePriority.UsePadding = False
        Me.tableCell14.StylePriority.UseTextAlignment = False
        Me.tableCell14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.tableCell14.Weight = 0.430555652574646R
        '
        'tableCell11
        '
        Me.tableCell11.Name = "tableCell11"
        Me.tableCell11.Weight = 0.51925367506635511R
        '
        'tableCell12
        '
        Me.tableCell12.DataBindings.AddRange(New DevExpress.XtraReports.UI.XRBinding() {New DevExpress.XtraReports.UI.XRBinding("Text", Nothing, "SP_GetGLV2.AccountName")})
        Me.tableCell12.Name = "tableCell12"
        Me.tableCell12.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 0, 0, 0, 100.0!)
        Me.tableCell12.StylePriority.UsePadding = False
        Me.tableCell12.StylePriority.UseTextAlignment = False
        Me.tableCell12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft
        Me.tableCell12.Weight = 1.1086404333335769R
        '
        'tableCell16
        '
        Me.tableCell16.Name = "tableCell16"
        Me.tableCell16.Weight = 0.31384975851727015R
        '
        'tableCell17
        '
        Me.tableCell17.Name = "tableCell17"
        Me.tableCell17.Weight = 0.31384999117988838R
        '
        'tableCell13
        '
        Me.tableCell13.Name = "tableCell13"
        Me.tableCell13.Weight = 0.31003711054254723R
        '
        'bottomMarginBand1
        '
        Me.bottomMarginBand1.HeightF = 12.29159!
        Me.bottomMarginBand1.Name = "bottomMarginBand1"
        '
        'CalculatedField1
        '
        Me.CalculatedField1.DataMember = "SP_GetGL"
        Me.CalculatedField1.Expression = "[].Sum([CrAmnt])"
        Me.CalculatedField1.Name = "CalculatedField1"
        '
        'CalculatedField2
        '
        Me.CalculatedField2.DataMember = "SP_GetGL"
        Me.CalculatedField2.Expression = "[Total] + [CalculatedField1]"
        Me.CalculatedField2.Name = "CalculatedField2"
        '
        'CalculatedField3
        '
        Me.CalculatedField3.DataMember = "SP_GetGL"
        Me.CalculatedField3.Expression = "[].Sum([DrAmnt])"
        Me.CalculatedField3.Name = "CalculatedField3"
        '
        'CalculatedField4
        '
        Me.CalculatedField4.DataMember = "SP_GetGL"
        Me.CalculatedField4.Expression = "[Total] + [CalculatedField3]"
        Me.CalculatedField4.Name = "CalculatedField4"
        '
        'TotalDrAmount
        '
        Me.TotalDrAmount.DataMember = "SP_GetGL"
        Me.TotalDrAmount.Expression = "IIf( [DrAmnt]=0,[CalculatedField4] ,[].Sum([DrAmnt]))"
        Me.TotalDrAmount.Name = "TotalDrAmount"
        '
        'TotalCrAmount
        '
        Me.TotalCrAmount.DataMember = "SP_GetGL"
        Me.TotalCrAmount.Expression = "Iif([CrAmnt]=0,[CalculatedField2],[].Sum([CrAmnt]))"
        Me.TotalCrAmount.Name = "TotalCrAmount"
        '
        'EndAccID
        '
        Me.EndAccID.Description = "ถึงบัญชี"
        DynamicListLookUpSettings2.DataAdapter = Nothing
        DynamicListLookUpSettings2.DataMember = Nothing
        DynamicListLookUpSettings2.DataSource = Me.PrmDatasource
        DynamicListLookUpSettings2.DisplayMember = "AccountName"
        DynamicListLookUpSettings2.FilterString = "[PublicStatus] = ##Enum#RiceManagementApp.Module.PublicStatus,Activate#"
        DynamicListLookUpSettings2.ValueMember = "AccountID"
        Me.EndAccID.LookUpSettings = DynamicListLookUpSettings2
        Me.EndAccID.Name = "EndAccID"
        '
        'GroupHeader1
        '
        Me.GroupHeader1.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.table3})
        Me.GroupHeader1.GroupFields.AddRange(New DevExpress.XtraReports.UI.GroupField() {New DevExpress.XtraReports.UI.GroupField("AccountID", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)})
        Me.GroupHeader1.HeightF = 29.58333!
        Me.GroupHeader1.KeepTogether = True
        Me.GroupHeader1.Name = "GroupHeader1"
        '
        'PageHeader
        '
        Me.PageHeader.Controls.AddRange(New DevExpress.XtraReports.UI.XRControl() {Me.XrLabel5, Me.table4, Me.label7, Me.label6, Me.label5, Me.label4, Me.label3, Me.table2})
        Me.PageHeader.HeightF = 81.2501!
        Me.PageHeader.Name = "PageHeader"
        '
        'XrLabel5
        '
        Me.XrLabel5.Borders = CType((DevExpress.XtraPrinting.BorderSide.Top Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.XrLabel5.Font = New System.Drawing.Font("AngsanaUPC", 13.0!, System.Drawing.FontStyle.Bold)
        Me.XrLabel5.LocationFloat = New DevExpress.Utils.PointFloat(0.0!, 0.0!)
        Me.XrLabel5.Name = "XrLabel5"
        Me.XrLabel5.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.XrLabel5.SizeF = New System.Drawing.SizeF(41.49561!, 81.25!)
        Me.XrLabel5.StylePriority.UseBorders = False
        Me.XrLabel5.StylePriority.UseFont = False
        Me.XrLabel5.StylePriority.UseTextAlignment = False
        Me.XrLabel5.Text = "สมุด"
        Me.XrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'table4
        '
        Me.table4.Borders = CType((DevExpress.XtraPrinting.BorderSide.Left Or DevExpress.XtraPrinting.BorderSide.Top), DevExpress.XtraPrinting.BorderSide)
        Me.table4.Font = New System.Drawing.Font("AngsanaUPC", 13.0!, System.Drawing.FontStyle.Bold)
        Me.table4.LocationFloat = New DevExpress.Utils.PointFloat(249.1666!, 0.0!)
        Me.table4.Name = "table4"
        Me.table4.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.tableRow4})
        Me.table4.SizeF = New System.Drawing.SizeF(290.8331!, 33.37501!)
        Me.table4.StylePriority.UseBorders = False
        Me.table4.StylePriority.UseFont = False
        '
        'tableRow4
        '
        Me.tableRow4.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.tableCell18})
        Me.tableRow4.Name = "tableRow4"
        Me.tableRow4.Weight = 1.0R
        '
        'tableCell18
        '
        Me.tableCell18.Borders = DevExpress.XtraPrinting.BorderSide.Top
        Me.tableCell18.Name = "tableCell18"
        Me.tableCell18.StylePriority.UseBorders = False
        Me.tableCell18.StylePriority.UseTextAlignment = False
        Me.tableCell18.Text = "ชื่อบัญชี"
        Me.tableCell18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        Me.tableCell18.Weight = 1.0R
        '
        'label7
        '
        Me.label7.BackColor = System.Drawing.Color.Transparent
        Me.label7.BorderColor = System.Drawing.Color.Black
        Me.label7.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid
        Me.label7.Borders = DevExpress.XtraPrinting.BorderSide.Top
        Me.label7.BorderWidth = 1.0!
        Me.label7.Font = New System.Drawing.Font("AngsanaUPC", 13.0!, System.Drawing.FontStyle.Bold)
        Me.label7.ForeColor = System.Drawing.Color.Black
        Me.label7.LocationFloat = New DevExpress.Utils.PointFloat(540.0!, 0.0!)
        Me.label7.Name = "label7"
        Me.label7.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.label7.SizeF = New System.Drawing.SizeF(246.9999!, 34.375!)
        Me.label7.StylePriority.UseBackColor = False
        Me.label7.StylePriority.UseBorderColor = False
        Me.label7.StylePriority.UseBorderDashStyle = False
        Me.label7.StylePriority.UseBorders = False
        Me.label7.StylePriority.UseBorderWidth = False
        Me.label7.StylePriority.UseFont = False
        Me.label7.StylePriority.UseForeColor = False
        Me.label7.StylePriority.UsePadding = False
        Me.label7.StylePriority.UseTextAlignment = False
        Me.label7.Text = "จำนวนเงิน"
        Me.label7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'label6
        '
        Me.label6.BackColor = System.Drawing.Color.Transparent
        Me.label6.BorderColor = System.Drawing.Color.Black
        Me.label6.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid
        Me.label6.Borders = DevExpress.XtraPrinting.BorderSide.Bottom
        Me.label6.BorderWidth = 1.0!
        Me.label6.Font = New System.Drawing.Font("AngsanaUPC", 13.0!, System.Drawing.FontStyle.Bold)
        Me.label6.ForeColor = System.Drawing.Color.Black
        Me.label6.LocationFloat = New DevExpress.Utils.PointFloat(249.1666!, 33.3751!)
        Me.label6.Name = "label6"
        Me.label6.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.label6.SizeF = New System.Drawing.SizeF(290.8334!, 47.87498!)
        Me.label6.StylePriority.UseBackColor = False
        Me.label6.StylePriority.UseBorderColor = False
        Me.label6.StylePriority.UseBorderDashStyle = False
        Me.label6.StylePriority.UseBorders = False
        Me.label6.StylePriority.UseBorderWidth = False
        Me.label6.StylePriority.UseFont = False
        Me.label6.StylePriority.UseForeColor = False
        Me.label6.StylePriority.UsePadding = False
        Me.label6.StylePriority.UseTextAlignment = False
        Me.label6.Text = "สมุดรายวัน"
        Me.label6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'label5
        '
        Me.label5.BackColor = System.Drawing.Color.Transparent
        Me.label5.BorderColor = System.Drawing.Color.Black
        Me.label5.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid
        Me.label5.Borders = CType((DevExpress.XtraPrinting.BorderSide.Top Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.label5.BorderWidth = 1.0!
        Me.label5.Font = New System.Drawing.Font("AngsanaUPC", 13.0!, System.Drawing.FontStyle.Bold)
        Me.label5.ForeColor = System.Drawing.Color.Black
        Me.label5.LocationFloat = New DevExpress.Utils.PointFloat(192.7001!, 0.00009536743!)
        Me.label5.Name = "label5"
        Me.label5.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.label5.SizeF = New System.Drawing.SizeF(56.46651!, 81.25!)
        Me.label5.StylePriority.UseBackColor = False
        Me.label5.StylePriority.UseBorderColor = False
        Me.label5.StylePriority.UseBorderDashStyle = False
        Me.label5.StylePriority.UseBorders = False
        Me.label5.StylePriority.UseBorderWidth = False
        Me.label5.StylePriority.UseFont = False
        Me.label5.StylePriority.UseForeColor = False
        Me.label5.StylePriority.UsePadding = False
        Me.label5.StylePriority.UseTextAlignment = False
        Me.label5.Text = "รายการที่"
        Me.label5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'label4
        '
        Me.label4.BackColor = System.Drawing.Color.Transparent
        Me.label4.BorderColor = System.Drawing.Color.Black
        Me.label4.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid
        Me.label4.Borders = CType((DevExpress.XtraPrinting.BorderSide.Top Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.label4.BorderWidth = 1.0!
        Me.label4.Font = New System.Drawing.Font("AngsanaUPC", 13.0!, System.Drawing.FontStyle.Bold)
        Me.label4.ForeColor = System.Drawing.Color.Black
        Me.label4.LocationFloat = New DevExpress.Utils.PointFloat(112.9491!, 0.00009536743!)
        Me.label4.Name = "label4"
        Me.label4.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.label4.SizeF = New System.Drawing.SizeF(79.75101!, 81.25!)
        Me.label4.StylePriority.UseBackColor = False
        Me.label4.StylePriority.UseBorderColor = False
        Me.label4.StylePriority.UseBorderDashStyle = False
        Me.label4.StylePriority.UseBorders = False
        Me.label4.StylePriority.UseBorderWidth = False
        Me.label4.StylePriority.UseFont = False
        Me.label4.StylePriority.UseForeColor = False
        Me.label4.StylePriority.UsePadding = False
        Me.label4.StylePriority.UseTextAlignment = False
        Me.label4.Text = "เลขที่ใบสำคัญ"
        Me.label4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'label3
        '
        Me.label3.BackColor = System.Drawing.Color.Transparent
        Me.label3.BorderColor = System.Drawing.Color.Black
        Me.label3.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid
        Me.label3.Borders = CType((DevExpress.XtraPrinting.BorderSide.Top Or DevExpress.XtraPrinting.BorderSide.Bottom), DevExpress.XtraPrinting.BorderSide)
        Me.label3.BorderWidth = 1.0!
        Me.label3.Font = New System.Drawing.Font("AngsanaUPC", 13.0!, System.Drawing.FontStyle.Bold)
        Me.label3.ForeColor = System.Drawing.Color.Black
        Me.label3.LocationFloat = New DevExpress.Utils.PointFloat(41.49561!, 0.00009536743!)
        Me.label3.Name = "label3"
        Me.label3.Padding = New DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100.0!)
        Me.label3.SizeF = New System.Drawing.SizeF(71.45349!, 81.25!)
        Me.label3.StylePriority.UseBackColor = False
        Me.label3.StylePriority.UseBorderColor = False
        Me.label3.StylePriority.UseBorderDashStyle = False
        Me.label3.StylePriority.UseBorders = False
        Me.label3.StylePriority.UseBorderWidth = False
        Me.label3.StylePriority.UseFont = False
        Me.label3.StylePriority.UseForeColor = False
        Me.label3.StylePriority.UsePadding = False
        Me.label3.StylePriority.UseTextAlignment = False
        Me.label3.Text = "วันที่"
        Me.label3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter
        '
        'table2
        '
        Me.table2.Font = New System.Drawing.Font("AngsanaUPC", 13.0!, System.Drawing.FontStyle.Bold)
        Me.table2.LocationFloat = New DevExpress.Utils.PointFloat(539.9999!, 34.375!)
        Me.table2.Name = "table2"
        Me.table2.Rows.AddRange(New DevExpress.XtraReports.UI.XRTableRow() {Me.tableRow2})
        Me.table2.SizeF = New System.Drawing.SizeF(247.0001!, 46.875!)
        Me.table2.StylePriority.UseBorders = False
        Me.table2.StylePriority.UseFont = False
        Me.table2.StylePriority.UseTextAlignment = False
        Me.table2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter
        '
        'tableRow2
        '
        Me.tableRow2.Cells.AddRange(New DevExpress.XtraReports.UI.XRTableCell() {Me.tableCell3, Me.tableCell6, Me.tableCell8})
        Me.tableRow2.Name = "tableRow2"
        Me.tableRow2.Weight = 1.0R
        '
        'tableCell3
        '
        Me.tableCell3.Borders = DevExpress.XtraPrinting.BorderSide.Bottom
        Me.tableCell3.Name = "tableCell3"
        Me.tableCell3.StylePriority.UseBorders = False
        Me.tableCell3.Text = "เดบิต"
        Me.tableCell3.Weight = 1.0R
        '
        'tableCell6
        '
        Me.tableCell6.Borders = DevExpress.XtraPrinting.BorderSide.Bottom
        Me.tableCell6.Name = "tableCell6"
        Me.tableCell6.StylePriority.UseBorders = False
        Me.tableCell6.Text = "เครดิต"
        Me.tableCell6.Weight = 1.0R
        '
        'tableCell8
        '
        Me.tableCell8.Borders = DevExpress.XtraPrinting.BorderSide.Bottom
        Me.tableCell8.Name = "tableCell8"
        Me.tableCell8.StylePriority.UseBorders = False
        Me.tableCell8.Text = "คงเหลือ"
        Me.tableCell8.Weight = 1.0R
        '
        'SqlDataSource1
        '
        Me.SqlDataSource1.ConnectionName = "ConnectionString"
        Me.SqlDataSource1.Name = "SqlDataSource1"
        StoredProcQuery1.Name = "SP_GetGLV2"
        QueryParameter1.Name = "@StartAccID"
        QueryParameter1.Type = GetType(DevExpress.DataAccess.Expression)
        QueryParameter1.Value = New DevExpress.DataAccess.Expression("[Parameters.StartAccID]", GetType(String))
        QueryParameter2.Name = "@EndAccID"
        QueryParameter2.Type = GetType(DevExpress.DataAccess.Expression)
        QueryParameter2.Value = New DevExpress.DataAccess.Expression("[Parameters.EndAccID]", GetType(String))
        QueryParameter3.Name = "@StartDate"
        QueryParameter3.Type = GetType(DevExpress.DataAccess.Expression)
        QueryParameter3.Value = New DevExpress.DataAccess.Expression("[Parameters.StartDate]", GetType(Date))
        QueryParameter4.Name = "@EndDate"
        QueryParameter4.Type = GetType(DevExpress.DataAccess.Expression)
        QueryParameter4.Value = New DevExpress.DataAccess.Expression("[Parameters.EndDate]", GetType(Date))
        StoredProcQuery1.Parameters.Add(QueryParameter1)
        StoredProcQuery1.Parameters.Add(QueryParameter2)
        StoredProcQuery1.Parameters.Add(QueryParameter3)
        StoredProcQuery1.Parameters.Add(QueryParameter4)
        StoredProcQuery1.StoredProcName = "SP_GetGLV2"
        Me.SqlDataSource1.Queries.AddRange(New DevExpress.DataAccess.Sql.SqlQuery() {StoredProcQuery1})
        Me.SqlDataSource1.ResultSchemaSerializable = resources.GetString("SqlDataSource1.ResultSchemaSerializable")
        '
        'SUMTotal
        '
        Me.SUMTotal.DataMember = "SP_GetGLV2"
        Me.SUMTotal.Expression = "Iif([Total] < 0, -([Total]) ,[Total] )"
        Me.SUMTotal.Name = "SUMTotal"
        '
        'GLReport
        '
        Me.Bands.AddRange(New DevExpress.XtraReports.UI.Band() {Me.detailBand1, Me.pageFooterBand1, Me.reportHeaderBand1, Me.topMarginBand1, Me.bottomMarginBand1, Me.GroupFooter1, Me.GroupHeader1, Me.PageHeader})
        Me.CalculatedFields.AddRange(New DevExpress.XtraReports.UI.CalculatedField() {Me.CalculatedField1, Me.CalculatedField2, Me.CalculatedField3, Me.CalculatedField4, Me.TotalDrAmount, Me.TotalCrAmount, Me.SUMTotal})
        Me.ComponentStorage.Add(Me.PrmDatasource)
        Me.ComponentStorage.Add(Me.SqlDataSource1)
        Me.ComponentStorage.Add(Me.SiteSettingDs)
        Me.DataMember = "SP_GetGLV2"
        Me.DataSource = Me.SqlDataSource1
        Me.DisplayName = "รายงานบัญชีแยกประเภท"
        Me.Extensions.Add("DataSerializationExtension", "XtraReport")
        Me.Extensions.Add("DataEditorExtension", "XtraReport")
        Me.Extensions.Add("ParameterEditorExtension", "XtraReport")
        Me.Font = New System.Drawing.Font("AngsanaUPC", 14.0!)
        Me.Margins = New System.Drawing.Printing.Margins(22, 18, 47, 12)
        Me.PageHeight = 1169
        Me.PageWidth = 827
        Me.PaperKind = System.Drawing.Printing.PaperKind.A4
        Me.Parameters.AddRange(New DevExpress.XtraReports.Parameters.Parameter() {Me.StartAccID, Me.EndAccID, Me.StartDate, Me.EndDate})
        Me.StyleSheet.AddRange(New DevExpress.XtraReports.UI.XRControlStyle() {Me.Title, Me.FieldCaption, Me.PageInfo, Me.DataField})
        Me.Version = "14.2"
        CType(Me.PrmDatasource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XrTable2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.table1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SiteSettingDs, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.table3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.table4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.table2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents pageInfo1 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents tableCell2 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents FieldCaption As DevExpress.XtraReports.UI.XRControlStyle
    Friend WithEvents label2 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents label1 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents line3 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents tableCell9 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell10 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents StartAccID As DevExpress.XtraReports.Parameters.Parameter
    Friend WithEvents tableRow1 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents tableCell4 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents GroupFooter1 As DevExpress.XtraReports.UI.GroupFooterBand
    Friend WithEvents line1 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents table1 As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents detailBand1 As DevExpress.XtraReports.UI.DetailBand
    Friend WithEvents PageInfo As DevExpress.XtraReports.UI.XRControlStyle
    Friend WithEvents topMarginBand1 As DevExpress.XtraReports.UI.TopMarginBand
    Friend WithEvents Title As DevExpress.XtraReports.UI.XRControlStyle
    Friend WithEvents pageInfo2 As DevExpress.XtraReports.UI.XRPageInfo
    Friend WithEvents pageFooterBand1 As DevExpress.XtraReports.UI.PageFooterBand
    Friend WithEvents DataField As DevExpress.XtraReports.UI.XRControlStyle
    Friend WithEvents label33 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents reportHeaderBand1 As DevExpress.XtraReports.UI.ReportHeaderBand
    Friend WithEvents bottomMarginBand1 As DevExpress.XtraReports.UI.BottomMarginBand
    Friend WithEvents tableCell5 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell1 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell7 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents CalculatedField1 As DevExpress.XtraReports.UI.CalculatedField
    Friend WithEvents XrTable2 As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents XrTableRow2 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents XrTableCell4 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell5 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrTableCell6 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents XrLabel4 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLine1 As DevExpress.XtraReports.UI.XRLine
    Friend WithEvents XrTableCell7 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents StartDate As DevExpress.XtraReports.Parameters.Parameter
    Friend WithEvents EndDate As DevExpress.XtraReports.Parameters.Parameter
    Protected WithEvents PrmDatasource As DevExpress.Persistent.Base.ReportsV2.CollectionDataSource
    Friend WithEvents CalculatedField2 As DevExpress.XtraReports.UI.CalculatedField
    Friend WithEvents XrLabel1 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel2 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel3 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents XrLabel6 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents table3 As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents tableRow3 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents tableCell14 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell11 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell12 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell16 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell17 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell13 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents CalculatedField3 As DevExpress.XtraReports.UI.CalculatedField
    Friend WithEvents CalculatedField4 As DevExpress.XtraReports.UI.CalculatedField
    Friend WithEvents TotalDrAmount As DevExpress.XtraReports.UI.CalculatedField
    Friend WithEvents TotalCrAmount As DevExpress.XtraReports.UI.CalculatedField
    Friend WithEvents EndAccID As DevExpress.XtraReports.Parameters.Parameter
    Friend WithEvents GroupHeader1 As DevExpress.XtraReports.UI.GroupHeaderBand
    Friend WithEvents PageHeader As DevExpress.XtraReports.UI.PageHeaderBand
    Friend WithEvents XrLabel5 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents table4 As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents tableRow4 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents tableCell18 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents label7 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents label6 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents label5 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents label4 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents label3 As DevExpress.XtraReports.UI.XRLabel
    Friend WithEvents table2 As DevExpress.XtraReports.UI.XRTable
    Friend WithEvents tableRow2 As DevExpress.XtraReports.UI.XRTableRow
    Friend WithEvents tableCell3 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell6 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents tableCell8 As DevExpress.XtraReports.UI.XRTableCell
    Friend WithEvents SqlDataSource1 As DevExpress.DataAccess.Sql.SqlDataSource
    Protected WithEvents SiteSettingDs As DevExpress.Persistent.Base.ReportsV2.CollectionDataSource
    Friend WithEvents SUMTotal As DevExpress.XtraReports.UI.CalculatedField
End Class
