Imports DevExpress.XtraReports.UI
Public Class SummaryBuyReport
    Private currentLineNo As Integer
    Private Sub SummaryBuyReport_BeforePrint(sender As Object, e As Drawing.Printing.PrintEventArgs) Handles Me.BeforePrint
        currentLineNo = 0
    End Sub

    Private Sub txtItemNo_BeforePrint(sender As Object, e As Drawing.Printing.PrintEventArgs) Handles txtItemNo.BeforePrint
        currentLineNo += 1
        CType(sender, XRLabel).Text = currentLineNo
    End Sub
End Class