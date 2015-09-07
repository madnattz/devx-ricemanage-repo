Imports DevExpress.XtraReports.UI

Public Class SummaryBuyTransactionReport
    Private currentLine As Integer = 0

    Private Sub SummaryBuyTransactionReport_BeforePrint(sender As Object, e As Drawing.Printing.PrintEventArgs) Handles Me.BeforePrint
        currentLine = 0
    End Sub

    Private Sub txtItemNo_BeforePrint(sender As Object, e As Drawing.Printing.PrintEventArgs) Handles txtItemNo.BeforePrint
        currentLine += 1
        CType(sender, XRLabel).Text = currentLine.ToString
    End Sub
End Class