Imports DevExpress.ExpressApp.ReportsV2
Imports DevExpress.XtraReports.UI

Public Class KP5Report

    'Private Sub XrSubreport1_AfterPrint(sender As Object, e As EventArgs) Handles XrSubreport1.AfterPrint
    '    Dim subreport As XRSubreport = CType(sender, XRSubreport)
    '    subreport.ReportSource.Parameters("Parameter1").Value = "ssss"
    'End Sub

    'Private Sub subreport1_BeforePrint(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles XrSubreport1.AfterPrint
    '    Dim subreport As XRSubreport = CType(sender, XRSubreport)
    '    subreport.ReportSource.Parameters("Parameter1").Value = "xxxx"
    'End Sub

    'Private Sub XrSubreport1_BeforePrint(sender As Object, e As Drawing.Printing.PrintEventArgs) Handles XrSubreport1.BeforePrint
    '    Dim subreport As XRSubreport = CType(sender, XRSubreport)
    '    'Dim val As String = (CType(KP5Report, XafReport)).ObjectSpace.GetKeyValue(GetCurrentRow())
    '    Dim val As String = (CType(KP5Report, XtraReport2)).ObjectSpace.GetKeyValue(GetCurrentRow())
    '    subreport.ReportSource.Parameters("Parameter1").Value = "xxxx"
    'End Sub

    Private Sub XrSubreport1_BeforePrint(sender As Object, e As Drawing.Printing.PrintEventArgs)

    End Sub
End Class