Imports DevExpress.XtraReports.UI

Public Class PickProcess

    Private Sub IsMixCell_BeforePrint(sender As Object, e As Drawing.Printing.PrintEventArgs) Handles IsMixCell.BeforePrint
        Dim CellItem As XRTableCell = CType(sender, XRTableCell)
        If CellItem.Text = "True" Then
            CType(sender, XRTableCell).Text = Char.ConvertFromUtf32(10003)
        Else
            CType(sender, XRTableCell).Text = ""
        End If
    End Sub

    Private Sub IsFinishCell_BeforePrint(sender As Object, e As Drawing.Printing.PrintEventArgs) Handles IsFinishCell.BeforePrint
        Dim CellItem As XRTableCell = CType(sender, XRTableCell)
        If CellItem.Text = "True" Then
            CType(sender, XRTableCell).Text = Char.ConvertFromUtf32(10003)
        Else
            CType(sender, XRTableCell).Text = ""
        End If
    End Sub

End Class