Imports System.Globalization
Imports DevExpress.DataAccess
Imports System.Threading

Public Class ReportIncomeAndExpensesOfMonth

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        LoadFilterMount()

     

    End Sub

    Public Sub LoadFilterMount()
        Me.MonthID.Description = "เดือน"
        Dim MonthSetting As New DevExpress.XtraReports.Parameters.StaticListLookUpSettings
        For indexMonth As Integer = 1 To 12
            MonthSetting.LookUpValues.Add(New DevExpress.XtraReports.Parameters.LookUpValue(indexMonth.ToString, MonthName(indexMonth, False).ToString(New System.Globalization.CultureInfo("th-TH"))))
        Next
        Me.MonthID.LookUpSettings = MonthSetting

        'Dim param1 As New Parameter()
        'param1.Name = "CatID"

        '' Specify other parameter properties.
        'param1.Type = GetType(System.Int32)
        'param1.Value = 1


        'Dim rpt As New XtraReport1()
        'rpt.Parameters.Add(param1)

    End Sub



End Class