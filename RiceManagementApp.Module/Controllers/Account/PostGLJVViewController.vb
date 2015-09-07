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
Imports RiceManagementApp.Module.PublicEnum
Imports System.Threading

' For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppViewControllertopic.
Partial Public Class PostGLJVViewController
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

    Dim AccountTypeEnum As EnumAccountType
    Private Sub PostGL_Execute(sender As Object, e As SimpleActionExecuteEventArgs) Handles PostGLJV.Execute
        Dim os As IObjectSpace = Me.Application.CreateObjectSpace()
        Try
            For Each BookJVinfo As AccountBookJV In e.SelectedObjects
                For i As Integer = 0 To BookJVinfo.AccountID.Count - 1
                    'อันดับแรกเช็คก่อนว่า รหัสบัญชีนี้อยู่หมวดใหน
                    Dim StrLeft As String = Left(BookJVinfo.AccountID(i).Account.AccountID, 2)
                    Dim AccountInfo As Account = ObjectSpace.FindObject(Of Account)(CriteriaOperator.Parse("StartsWith([AccountID], '" & StrLeft & "') and [ParentAccount] Is Null", StrLeft))

                    'ดึงหมวดบัญชีมา
                    Dim LoadBringforward As Bringforward
                    Dim ChkGL As GLID = ObjectSpace.FindObject(Of GLID)(CriteriaOperator.Parse("AccID=?", BookJVinfo.AccountID(i).Account.AccountID))
                    If ChkGL Is Nothing Then
                        'ยังไม่เคย Post ยอดยกมา สำหรับตั้งต้น อันดับแรกให้ไปเพิ่มก่อน
                        LoadBringforward = ObjectSpace.FindObject(Of Bringforward)(CriteriaOperator.Parse("AccountID=?", BookJVinfo.AccountID(i).Account.AccountID))
                        If LoadBringforward IsNot Nothing Then
                            'แปลว่า มึข้อมูลตั้งต้นขอดยอดยกมา
                            Dim Transaction As GLID = os.CreateObject(Of GLID)()
                            Transaction.DocuNo = ""
                            Transaction.AccountBook = BookJVinfo.AccountBookJV.AccountBookNameVGA
                            Transaction.IVRefNo = ""
                            Transaction.RefNo = ""
                            Transaction.RVDesc1 = ""
                            Transaction.AccID = BookJVinfo.AccountID(i).Account.AccountID
                            Transaction.AccDetail = "ยอดยกมา"
                            Transaction.AccountName = BookJVinfo.AccountID(i).Account.AccountName

                            Select Case AccountInfo.AccountName
                                Case "สินทรัพย์" 'เพิ่มอยู่ฝั่ง Debit
                                    AccountTypeEnum = EnumAccountType.Debit
                                    Transaction.DrAmnt = LoadBringforward.Debit
                                    Transaction.TotalAmnt = LoadBringforward.Debit
                                Case "หนิ้สิน" 'เพิ่มอยู่ฝั่ง Credit
                                    AccountTypeEnum = EnumAccountType.Credit
                                    Transaction.DrAmnt = LoadBringforward.Crebit
                                    Transaction.TotalAmnt = LoadBringforward.Crebit
                                Case "ทุน" 'เพิ่มอยู่ฝั่ง Credit
                                    AccountTypeEnum = EnumAccountType.Credit
                                    Transaction.DrAmnt = LoadBringforward.Crebit
                                    Transaction.TotalAmnt = LoadBringforward.Crebit
                                Case "รายได้" 'เพิ่มอยู่ฝั่ง Credit
                                    AccountTypeEnum = EnumAccountType.Credit
                                    Transaction.DrAmnt = LoadBringforward.Crebit
                                    Transaction.TotalAmnt = LoadBringforward.Crebit
                                Case "ค่าใช้จ่าย" 'เพิ่มอยู่ฝั่ง Debit
                                    AccountTypeEnum = EnumAccountType.Debit
                                    Transaction.DrAmnt = LoadBringforward.Debit
                                    Transaction.TotalAmnt = LoadBringforward.Debit
                            End Select

                            Transaction.DrAmnt = LoadBringforward.Debit
                            Transaction.CrAmnt = 0
                            Transaction.Save()
                        End If

                        Thread.Sleep(1000)

                        Dim ADDGL As GLID = os.CreateObject(Of GLID)()
                        ADDGL.DocuNo = BookJVinfo.DocumentNoJV
                        ADDGL.DocuDate = BookJVinfo.DateListJV
                        ADDGL.AccountBook = BookJVinfo.AccountBookJV.AccountBookNameVGA
                        ADDGL.IVRefNo = BookJVinfo.ReferenceNoJV
                        ADDGL.RefNo = BookJVinfo.ReferenceNoJV
                        'ADDGL.RefDate = BookJVinfo.DateDateJV
                        ADDGL.RefAmnt = BookJVinfo.AmountJV
                        ADDGL.ListNo = BookJVinfo.ListNoJV
                        ADDGL.RVDesc1 = BookJVinfo.DetailJV
                        'ADDGL.ToptotalAmnt = BookJVinfo.MoneyJV
                        ADDGL.AccID = BookJVinfo.AccountID(i).Account.AccountID
                        ADDGL.AccountName = BookJVinfo.AccountID(i).Account.AccountName

                        Select Case AccountInfo.AccountName
                            Case "สินทรัพย์" 'เพิ่มอยู่ฝั่ง Debit
                                AccountTypeEnum = EnumAccountType.Debit
                                ADDGL.TotalAmnt = LoadBringforward.Debit + BookJVinfo.AccountID(i).Debit
                            Case "หนิ้สิน" 'เพิ่มอยู่ฝั่ง Credit
                                AccountTypeEnum = EnumAccountType.Credit
                                ADDGL.TotalAmnt = LoadBringforward.Crebit + BookJVinfo.AccountID(i).Credit
                            Case "ทุน" 'เพิ่มอยู่ฝั่ง Credit
                                AccountTypeEnum = EnumAccountType.Credit
                                ADDGL.TotalAmnt = LoadBringforward.Crebit + BookJVinfo.AccountID(i).Credit
                            Case "รายได้" 'เพิ่มอยู่ฝั่ง Credit
                                AccountTypeEnum = EnumAccountType.Credit
                                ADDGL.TotalAmnt = LoadBringforward.Crebit + BookJVinfo.AccountID(i).Credit
                            Case "ค่าใช้จ่าย" 'เพิ่มอยู่ฝั่ง Debit
                                AccountTypeEnum = EnumAccountType.Debit
                                ADDGL.TotalAmnt = LoadBringforward.Debit + BookJVinfo.AccountID(i).Debit
                        End Select

                        ADDGL.AccDetail = BookJVinfo.DetailJV
                        ADDGL.DrAmnt = BookJVinfo.AccountID(i).Debit
                        ADDGL.CrAmnt = BookJVinfo.AccountID(i).Credit
                        ADDGL.Save()

                    Else

                        Thread.Sleep(1000)
                        Dim ADDGL As GLID = os.CreateObject(Of GLID)()
                        ADDGL.DocuNo = BookJVinfo.DocumentNoJV
                        ADDGL.DocuDate = BookJVinfo.DateListJV
                        ADDGL.AccountBook = BookJVinfo.AccountBookJV.AccountBookNameVGA
                        ADDGL.IVRefNo = BookJVinfo.ReferenceNoJV
                        ADDGL.RefNo = BookJVinfo.ReferenceNoJV
                        ADDGL.RefDate = BookJVinfo.DateDateJV
                        ADDGL.RefAmnt = BookJVinfo.AmountJV
                        ADDGL.ListNo = BookJVinfo.ListNoJV
                        ADDGL.RVDesc1 = BookJVinfo.DetailJV
                        ADDGL.ToptotalAmnt = BookJVinfo.MoneyJV
                        ADDGL.AccID = BookJVinfo.AccountID(i).Account.AccountID
                        ADDGL.AccountName = BookJVinfo.AccountID(i).Account.AccountName
                        'Dim AccountInfo As Account = ObjectSpace.FindObject(Of Account)(CriteriaOperator.Parse("StartsWith([AccountID], '10') and [ParentAccount] Is Null", StrLeft))

                        '   Dim ProductNameInfo As XPCollection(Of GLID) = New XPCollection(Of GLID)(ObjectSpace, CriteriaOperator.Parse("AccID=?", ""))
                        'ProductNameInfo.Sorting.Add(New SortProperty("Createdate", DB.SortingDirection.Descending))




                        Dim AccountCollection = ObjectSpace.CreateCollection(GetType(GLID), DevExpress.Data.Filtering.ContainsOperator.Parse("AccID=?", BookJVinfo.AccountID(i).Account.AccountID))

                        Dim list As New ArrayList
                        Dim Index As Integer = 0
                        For Each a As GLID In AccountCollection
                            list.Add(a.TotalAmnt)
                        Next

                        Dim GetAmout As Decimal = list.ToArray.Max

                        Select Case AccountInfo.AccountName
                            Case "สินทรัพย์" 'เพิ่มอยู่ฝั่ง of
                                AccountTypeEnum = EnumAccountType.Debit
                                ADDGL.TotalAmnt = GetAmout + BookJVinfo.AccountID(i).Debit
                            Case "หนิ้สิน" 'เพิ่มอยู่ฝั่ง Credit
                                AccountTypeEnum = EnumAccountType.Credit
                                ADDGL.TotalAmnt = GetAmout + BookJVinfo.AccountID(i).Credit
                            Case "ทุน" 'เพิ่มอยู่ฝั่ง Credit
                                AccountTypeEnum = EnumAccountType.Credit
                                ADDGL.TotalAmnt = GetAmout + BookJVinfo.AccountID(i).Credit
                            Case "รายได้" 'เพิ่มอยู่ฝั่ง Credit
                                AccountTypeEnum = EnumAccountType.Credit
                                ADDGL.TotalAmnt = GetAmout + BookJVinfo.AccountID(i).Credit
                            Case "ค่าใช้จ่าย" 'เพิ่มอยู่ฝั่ง Debit
                                AccountTypeEnum = EnumAccountType.Debit
                                ADDGL.TotalAmnt = GetAmout + BookJVinfo.AccountID(i).Debit 'BookRVinfo.AccountID(i - 1).Debit + BookRVinfo.AccountID(i).Debit
                        End Select

                        ADDGL.AccDetail = BookJVinfo.DetailJV
                        ADDGL.DrAmnt = BookJVinfo.AccountID(i).Debit
                        ADDGL.CrAmnt = BookJVinfo.AccountID(i).Credit
                        ADDGL.Save()

                    End If

                Next




                'Dim LoadBringforward As Bringforward
                'For i As Integer = 0 To BookRVinfo.AccountID.Count - 1

                '    Dim ChkGL As GLID = ObjectSpace.FindObject(Of GLID)(CriteriaOperator.Parse("AccID=?", BookRVinfo.AccountID(i).Account.AccountID))

                '    If ChkGL Is Nothing Then
                '        LoadBringforward = ObjectSpace.FindObject(Of Bringforward)(CriteriaOperator.Parse("AccountID=?", BookRVinfo.AccountID(i).Account.AccountID))
                '        If LoadBringforward IsNot Nothing Then
                '            'แปลว่า มึข้อมูลตั้งต้นขอดยอดยกมา
                '            Dim Transaction As GLID = os.CreateObject(Of GLID)()
                '            Transaction.DocuNo = ""
                '            Transaction.AccountBook = BookRVinfo.AccountBook.AccountBookNameVGA
                '            Transaction.IVRefNo = ""
                '            Transaction.RefNo = ""
                '            Transaction.RVDesc1 = ""
                '            Transaction.AccID = BookRVinfo.AccountID(i).Account.AccountID
                '            Transaction.AccDetail = "ยอดยกมา"
                '            Transaction.AccountName = BookRVinfo.AccountID(i).Account.AccountName
                '            Transaction.DrAmnt = LoadBringforward.Debit
                '            Transaction.CrAmnt = 0
                '            Transaction.Save()
                '        End If
                '    End If
                '    'แปลว่ายังไม่เคยดึงยอดยกมาเลย 




                '    Dim CheckDO As GLID = ObjectSpace.FindObject(Of GLID)(CriteriaOperator.Parse("AccountBook=? and DocuDate=? and DocuNo =? and ListNo =? and AccID=?", BookRVinfo.AccountBook, BookRVinfo.DateList, BookRVinfo.DocumentNo, BookRVinfo.ListNo, BookRVinfo.AccountID(i).Account.AccountID))
                '    If CheckDO Is Nothing Then
                '        Dim Transaction As GLID = os.CreateObject(Of GLID)()
                '        Transaction.DocuNo = BookRVinfo.DocumentNo
                '        Transaction.DocuDate = BookRVinfo.DateList
                '        Transaction.AccountBook = BookRVinfo.AccountBook.AccountBookNameVGA
                '        Transaction.IVRefNo = BookRVinfo.ReceiptNo
                '        Transaction.RefNo = BookRVinfo.ReferenceNoMs
                '        Transaction.RefDate = BookRVinfo.DateDate
                '        Transaction.RefAmnt = BookRVinfo.Amount
                '        Transaction.ListNo = BookRVinfo.ListNo
                '        Transaction.RVDesc1 = BookRVinfo.Detail
                '        Transaction.ToptotalAmnt = BookRVinfo.Money
                '        Transaction.AccID = BookRVinfo.AccountID(i).Account.AccountID
                '        Transaction.AccountName = BookRVinfo.AccountID(i).Account.AccountName

                '        If BookRVinfo.AccountID(i).Debit > 0 Then
                '            'แปลว่าเพิ่ม
                '            Try
                '                Transaction.ToptotalAmnt = BookRVinfo.AccountID(i - 1).Debit + BookRVinfo.AccountID(i).Debit
                '            Catch ex As Exception
                '                Transaction.ToptotalAmnt = LoadBringforward.Debit + BookRVinfo.AccountID(i).Debit
                '            End Try

                '        Else
                '            Try
                '                Transaction.ToptotalAmnt = LoadBringforward.Crebit - BookRVinfo.AccountID(i).Credit
                '            Catch ex As Exception
                '                Transaction.ToptotalAmnt = LoadBringforward.Debit - BookRVinfo.AccountID(i).Credit
                '            End Try

                '        End If

                '        Transaction.AccDetail = BookRVinfo.Detail
                '        Transaction.DrAmnt = BookRVinfo.AccountID(i).Debit
                '        Transaction.CrAmnt = BookRVinfo.AccountID(i).Credit
                '        Transaction.Save()
                '    End If



                'Next


                'For Each AccountID In BookRVinfo.AccountID
                '    'ทำการตรวจสอบในตาราง GL ว่ามีข้อมูลยอดยกมาหรือไม่่
                '    Dim ChkGL As GLID = ObjectSpace.FindObject(Of GLID)(CriteriaOperator.Parse("AccID=?", AccountID.Account.AccountID))

                '    'If ChkGL Is Nothing Then
                '    '    'แปลว่ายังไม่เคยดึงยอดยกมาเลย 
                '    '    Dim LoadBringforward As Bringforward = ObjectSpace.FindObject(Of Bringforward)(CriteriaOperator.Parse("AccountID=?", AccountID.Account.AccountID))
                '    '    If LoadBringforward IsNot Nothing Then
                '    '        'แปลว่า มึข้อมูลตั้งต้นขอดยอดยกมา
                '    '        Dim Transaction As GLID = os.CreateObject(Of GLID)()
                '    '        Transaction.DocuNo = ""
                '    '        Transaction.AccountBook = BookRVinfo.AccountBook.AccountBookNameVGA
                '    '        Transaction.IVRefNo = ""
                '    '        Transaction.RefNo = ""
                '    '        Transaction.RVDesc1 = ""
                '    '        Transaction.AccID = AccountID.Account.AccountID
                '    '        Transaction.AccDetail = "ยอดยกมา"
                '    '        Transaction.AccountName = AccountID.Account.AccountName
                '    '        Transaction.DrAmnt = LoadBringforward.Debit
                '    '        Transaction.CrAmnt = 0
                '    '        Transaction.Save()
                '    '    End If

                '    'End If





                '    'Dim CheckDO As GLID = ObjectSpace.FindObject(Of GLID)(CriteriaOperator.Parse("AccountBook=? and DocuDate=? and DocuNo =? and ListNo =? and AccID=?", BookRVinfo.AccountBook, BookRVinfo.DateList, BookRVinfo.DocumentNo, BookRVinfo.ListNo, AccountID.Account.AccountID))
                '    'If CheckDO Is Nothing Then
                '    '    Dim Transaction As GLID = os.CreateObject(Of GLID)()
                '    '    Transaction.DocuNo = BookRVinfo.DocumentNo
                '    '    Transaction.DocuDate = BookRVinfo.DateList
                '    '    Transaction.AccountBook = BookRVinfo.AccountBook.AccountBookNameVGA
                '    '    Transaction.IVRefNo = BookRVinfo.ReceiptNo
                '    '    Transaction.RefNo = BookRVinfo.ReferenceNoMs
                '    '    Transaction.RefDate = BookRVinfo.DateDate
                '    '    Transaction.RefAmnt = BookRVinfo.Amount
                '    '    Transaction.ListNo = BookRVinfo.ListNo
                '    '    Transaction.RVDesc1 = BookRVinfo.Detail
                '    '    Transaction.ToptotalAmnt = BookRVinfo.Money
                '    '    Transaction.AccID = AccountID.Account.AccountID
                '    '    Transaction.AccountName = AccountID.Account.AccountName
                '    '    Transaction.AccDetail = BookRVinfo.Detail
                '    '    Transaction.DrAmnt = AccountID.Debit
                '    '    Transaction.CrAmnt = AccountID.Credit
                '    '    Transaction.Save()
                '    'End If
                'Next
            Next
            os.CommitChanges()
        Catch ex As Exception
            os.Rollback()
        End Try
    End Sub
End Class
