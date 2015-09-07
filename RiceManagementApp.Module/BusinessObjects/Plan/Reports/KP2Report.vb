Imports DevExpress.XtraReports.UI
Public Class KP2Report
    Private currentLineNo As Integer

    Private Sub GroupHeader2_BeforePrint(sender As Object, e As Drawing.Printing.PrintEventArgs) Handles GroupHeader2.BeforePrint
        currentLineNo = 0
    End Sub

    Private Sub lblItemNo_BeforePrint(sender As Object, e As Drawing.Printing.PrintEventArgs) Handles lblItemNo.BeforePrint
        currentLineNo += 1
        CType(sender, XRLabel).Text = currentLineNo
    End Sub
End Class