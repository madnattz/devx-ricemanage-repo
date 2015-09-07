Imports DevExpress.XtraReports.UI
Public Class DailyProcess

    Private Sub chkNormalTime_BeforePrint(sender As Object, e As Drawing.Printing.PrintEventArgs) Handles chkNormalTime.BeforePrint
        Dim CellItem As XRTableCell = CType(sender, XRTableCell)
        If CellItem.Text = "True" Then
            CType(sender, XRTableCell).Text = Char.ConvertFromUtf32(10003)
        Else
            CType(sender, XRTableCell).Text = ""
        End If
    End Sub

    Private Sub chkOverTime_BeforePrint(sender As Object, e As Drawing.Printing.PrintEventArgs) Handles chkOverTime.BeforePrint
        Dim CellItem As XRTableCell = CType(sender, XRTableCell)
        If CellItem.Text = "True" Then
            CType(sender, XRTableCell).Text = Char.ConvertFromUtf32(10003)
        Else
            CType(sender, XRTableCell).Text = ""
        End If
    End Sub

    Private Sub chkShiftTime_BeforePrint(sender As Object, e As Drawing.Printing.PrintEventArgs) Handles chkShiftTime.BeforePrint
        Dim CellItem As XRTableCell = CType(sender, XRTableCell)
        If CellItem.Text = "True" Then
            CType(sender, XRTableCell).Text = Char.ConvertFromUtf32(10003)
        Else
            CType(sender, XRTableCell).Text = ""
        End If
    End Sub

    Private Sub XrTableCell5_BeforePrint(sender As Object, e As Drawing.Printing.PrintEventArgs) Handles XrTableCell5.BeforePrint
        Dim CellItem As XRTableCell = CType(sender, XRTableCell)
        If CellItem.Text = "True" Then
            CType(sender, XRTableCell).Text = Char.ConvertFromUtf32(10003)
        Else
            CType(sender, XRTableCell).Text = ""
        End If
    End Sub
End Class