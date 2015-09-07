Imports Microsoft.VisualBasic
Imports System
Imports System.Linq
Imports System.Text
Imports DevExpress.ExpressApp
Imports DevExpress.Data.Filtering
Imports System.Collections.Generic
Imports DevExpress.Persistent.Base
Imports DevExpress.ExpressApp.Utils
Imports DevExpress.ExpressApp.Layout
Imports DevExpress.ExpressApp.Actions
Imports DevExpress.ExpressApp.Editors
Imports DevExpress.ExpressApp.Templates
Imports DevExpress.Persistent.Validation
Imports DevExpress.ExpressApp.SystemModule
Imports DevExpress.ExpressApp.Model.NodeGenerators

' For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppViewControllertopic.
Partial Public Class BringViewController
    Inherits ViewController
    Public Sub New()
        InitializeComponent()
        ' Target required Views (via the TargetXXX properties) and create their Actions.
    End Sub
    Protected Overrides Sub OnActivated()
        MyBase.OnActivated()
        ' Perform various tasks depending on the target View.
    End Sub
    Protected Overrides Sub OnViewControlsCreated()
        MyBase.OnViewControlsCreated()
        ' Access and customize the target View control.
    End Sub
    Protected Overrides Sub OnDeactivated()
        ' Unsubscribe from previously subscribed events and release other references and resources.
        MyBase.OnDeactivated()
    End Sub

    Private Sub Bringforward_Execute(sender As Object, e As SimpleActionExecuteEventArgs) Handles Bringforward.Execute
        Dim os As IObjectSpace = Me.Application.CreateObjectSpace()
        'Try
        '        For Each Bringinfo As EventBringforward In e.SelectedObjects
        '            'For Each AccountID In Bringinfo.AccountID
        '            Dim CheckDO As Account = ObjectSpace.FindObject(Of Account)(CriteriaOperator.Parse("AccountID=? and AccountType=?", Bringinfo., Bringinfo.DateList))
        '            '    If CheckDO Is Nothing Then
        '            '        Dim Transaction As GLID = os.CreateObject(Of GLID)()
        '            '        Transaction.DocuNo = BookRVinfo.DocumentNo
        '            '        Transaction.DocuDate = BookRVinfo.DateList
        '            '        Transaction.AccountBook = BookRVinfo.AccountBook.AccountBookNameVGA
        '            '        Transaction.IVRefNo = BookRVinfo.ReceiptNo
        '            '        Transaction.RefNo = BookRVinfo.ReferenceNoMs
        '            '        Transaction.RefDate = BookRVinfo.DateDate
        '            '        Transaction.RefAmnt = BookRVinfo.Amount
        '            '        Transaction.ListNo = BookRVinfo.ListNo
        '            '        Transaction.RVDesc1 = BookRVinfo.Detail
        '            '        Transaction.ToptotalAmnt = BookRVinfo.Money
        '            '        Transaction.AccID = AccountID.Account.AccountID
        '            '        Transaction.AccDetail = AccountID.AccountName
        '            '        Transaction.DrAmnt = AccountID.Debit
        '            '        Transaction.CrAmnt = AccountID.Credit
        '            '        Transaction.Save()
        '            '    End If
        '            'Next
        '            'Next
        '        os.CommitChanges()
        '    Catch ex As Exception
        '        os.Rollback()
        '    End Try
        'End Sub
    End Sub
End Class
