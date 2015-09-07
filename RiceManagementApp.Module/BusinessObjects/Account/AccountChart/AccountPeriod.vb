Imports Microsoft.VisualBasic
Imports System
Imports System.Linq
Imports System.Text
Imports DevExpress.Xpo
Imports DevExpress.ExpressApp
Imports System.ComponentModel
Imports DevExpress.ExpressApp.DC
Imports DevExpress.Data.Filtering
Imports DevExpress.Persistent.Base
Imports System.Collections.Generic
Imports DevExpress.ExpressApp.Model
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Persistent.Validation
Imports DevExpress.Xpo.DB

'<ImageName("BO_Contact")> _
'<DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")> _
'<DefaultListViewOptions(MasterDetailMode.ListViewOnly, False, NewItemRowPosition.None)> _
'<Persistent("DatabaseTableName")> _
<NavigationItem("กำหนดผังบัญชี")> _
<XafDisplayName("กำหนดงวดบัญชี")> _
<DeferredDeletion(False)> _
<DefaultProperty("AccountYear")> _
<DefaultClassOptions()> _
Public Class AccountPeriod ' Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        DataOwner = GetCurrentSite()
        MyBase.AfterConstruction()
        ' Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).
    End Sub

    Protected Overrides Sub OnSaving()
        MyBase.OnSaving()
        If AccountPeriodDetails.Count > 0 Then
            Dim sDate = From a In AccountPeriodDetails Where a.ItemNo = 1 Select a
            _StartDate = sDate(0).StartDate

            Dim eDate = From a In AccountPeriodDetails Where a.ItemNo = 12 Select a
            _EndDate = eDate(0).EndDate
        Else

        End If
        If Me.DataOwner Is Nothing Then
            DataOwner = GetCurrentSite()
        End If
    End Sub

    Dim fDataOwner As Site
    <Browsable(False)> _
    Public Property DataOwner() As Site
        Get
            Return fDataOwner
        End Get
        Set(ByVal value As Site)
            SetPropertyValue(Of Site)("DataOwner", fDataOwner, value)
        End Set
    End Property

    Public Function GetCurrentSite() As Site
        Dim siteSetting As SiteSetting = Session.FindObject(Of SiteSetting)(Nothing)
        If siteSetting IsNot Nothing Then
            If siteSetting.Site IsNot Nothing Then
                Return siteSetting.Site
            Else
                Return Nothing
            End If
            Return siteSetting.Site
        Else
            Return Nothing
        End If
    End Function

    Private _AccountYear As String
    <XafDisplayName("ปี")> _
    Public Property AccountYear() As String
        Get
            Return _AccountYear
        End Get
        Set(ByVal value As String)
            SetPropertyValue("AccountYear", _AccountYear, value)
        End Set
    End Property

    Private _StartMonth As AccMonth
    <XafDisplayName("เดือนเริ่มต้นงวดบัญชี")> _
    Public Property StartMonth() As AccMonth
        Get
            Return _StartMonth
        End Get
        Set(ByVal value As AccMonth)
            SetPropertyValue("StartMonth", _StartMonth, value)
        End Set
    End Property

    Private _StartDate As DateTime
    <XafDisplayName("วันเริ่มต้นงวดบัญชี")> _
    <VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    Public Property StartDate() As DateTime
        Get
            Return _StartDate
        End Get
        Set(value As DateTime)
            SetPropertyValue("StartDate", _StartDate, value)
        End Set
    End Property

    Private _EndDate As DateTime
    <XafDisplayName("วันสิ้นสุดงวดบัญชี")> _
    <VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    Public Property EndDate() As DateTime
        Get
            Return _EndDate
        End Get
        Set(value As DateTime)
            SetPropertyValue("EndDate", _EndDate, value)
        End Set
    End Property

    Private _Status As AccountStatus
    <XafDisplayName("สถานะ")> _
    Public Property Status() As AccountStatus
        Get
            Return _Status
        End Get
        Set(ByVal value As AccountStatus)
            SetPropertyValue("Status", _Status, value)
        End Set
    End Property
    <XafDisplayName("รายการงวดบัญชี")> _
    <Association("AccountPeriod-AccountPeriodDetails", GetType(AccountPeriodDetail))> _
    <DevExpress.Xpo.Aggregated()> _
    Public ReadOnly Property AccountPeriodDetails() As XPCollection(Of AccountPeriodDetail)
        Get
            Return GetCollection(Of AccountPeriodDetail)("AccountPeriodDetails")
        End Get
    End Property

    'Public ReadOnly Property AccountSummary As AccountSummary
    '    Get
    '        Try
    '            Dim sumBringForword As Double = 0
    '            Dim sumDr As Double = 0
    '            Dim sumCr As Double = 0
    '            Dim objAccountSummary As New AccountSummary(Session)
    '            Dim data As SelectedData = Session.ExecuteSproc("SP_GetSummaryBalance", New OperandValue(Date.MinValue), New OperandValue(Date.Now))
    '            For Each row As SelectStatementResultRow In data.ResultSet(0).Rows
    '                If row.Values(0) = "30200101.000" Then
    '                    sumBringForword += row.Values(2)
    '                End If
    '                sumDr += row.Values(6)
    '                sumCr += row.Values(7)

    '            Next

    '            objAccountSummary.BringForword = sumBringForword
    '            objAccountSummary.CurrentSummary = sumDr - sumCr
    '            objAccountSummary.CumulativeSummary = objAccountSummary.CurrentSummary + objAccountSummary.BringForword

    '            Return objAccountSummary
    '        Catch ex As Exception
    '            Return Nothing
    '        End Try
    '        'Return Nothing
    '    End Get
    'End Property

    <Action(Caption:="กำหนดงวดบัญชี", ConfirmationMessage:="ต้องการบันทึกข้อมูล ใช่หรือไม่?", ImageName:="Action_Reload", AutoCommit:=True)> _
    Public Sub ActionMethod()

        '//ลบของเก่าออก ตามปีบัญชี [AccountYear]
        Dim colDelete As New XPCollection(Of AccountPeriodDetail)(Session, CriteriaOperator.Parse("AccountPeriod=?", Me))
        If colDelete.Count > 0 Then
            Session.Delete(colDelete)
            Session.Save(colDelete)
        End If
        ' Objects for deletion.

        Dim d As New Date(Convert.ToInt32(Me._AccountYear - 544), Me._StartMonth, 1)

        For i As Integer = 0 To 11
            Dim objDetail As New AccountPeriodDetail(Session)
            objDetail.AccountPeriod = Me
            objDetail.ItemNo = i + 1
            objDetail.StartDate = d.AddMonths(i)
            objDetail.EndDate = objDetail.StartDate.AddMonths(1).AddDays(-1)

            If i = 0 Then
                StartDate = objDetail.StartDate
                OnChanged("StartDate")
            End If
            If i = 11 Then
                EndDate = objDetail.EndDate
                OnChanged("EndDate")
            End If
            objDetail.Save()
        Next
        Session.CommitTransaction()
        ' Trigger a custom business logic for the current record in the UI (http://documentation.devexpress.com/#Xaf/CustomDocument2619).
    End Sub

    <Action(Caption:="ปิดบัญชี", ConfirmationMessage:="ต้องการบันทึกข้อมูล ใช่หรือไม่?", ImageName:="Action_ResetPassword", AutoCommit:=True)> _
    Public Sub CloseAccountPeriod()
        '1. เปลี่ยน status = ปิด
        '2. สร้างงวดบัญชี Auto
        '3. สร้างข้อมูลใบผ่านรายวัน

        '1. สร้างใบผ่านรายวัน งวดสุดท้าย ปิดบัญชี
        '2. เปลี่ยนสถานะปีปัจจุบัน = ปิด
        '3. สร้างงวดบัญชีปีถัดไป
        '4. สร้างรายวัน BL คล้ายกับ Post จาก (ยอดยกมา) BringForward ลง GL

        'Dim colDelete As New XPCollection(Of AccountPeriodDetail)(Session, CriteriaOperator.Parse("AccountPeriod=?", Me))
        'If colDelete.Count > 0 Then
        '    Session.Delete(colDelete)
        '    Session.Save(colDelete)
        'End If

        Try
            'Dim objAccJV As New AccountBookJV(Session) ' Add Master
            'Dim objAccid As AccountID

            Dim accStartDate As Date = Date.MinValue
            Dim accEndDate As Date = Date.MinValue
            For Each item As AccountPeriodDetail In AccountPeriodDetails
                If item.ItemNo = 1 Then
                    accStartDate = item.StartDate
                    'Exit For
                End If
                If item.ItemNo = 12 Then
                    accEndDate = item.EndDate
                End If
            Next

            '2.
            Me.Status = AccountStatus.DeActivate

            '3.
            Dim objAccPr As New AccountPeriod(Session)
            objAccPr.StartMonth = AccMonth.October
            objAccPr.AccountYear = CInt(Me.AccountYear) + 1

            Dim d As New Date(Convert.ToInt32(objAccPr.AccountYear - 544), objAccPr.StartMonth, 1)

            For i As Integer = 0 To 11
                Dim objDetail As New AccountPeriodDetail(Session)
                objDetail.AccountPeriod = objAccPr
                objDetail.ItemNo = i + 1
                objDetail.StartDate = d.AddMonths(i)
                objDetail.EndDate = objDetail.StartDate.AddMonths(1).AddDays(-1)

                objDetail.Save()
            Next

            objAccPr.Save()

            '4. 
            Dim newData As SelectedData = Session.ExecuteSproc("SP_GetTrialBalanceV8", New OperandValue(accStartDate), New OperandValue(accEndDate))
            Dim newAccStartDate As Date = Date.MinValue
            For Each item As AccountPeriodDetail In objAccPr.AccountPeriodDetails
                If item.ItemNo = 1 Then
                    newAccStartDate = item.StartDate
                    Exit For
                End If
            Next

            Dim bookRefNo As String = ""
            Dim prefix As String = "BL" & Date.Now.ToString("yyyyMMdd")
            bookRefNo = String.Format("{0}{1:D5}", prefix, DistributedIdGeneratorHelper.Generate(Me.Session.DataLayer, Me.GetType().FullName, prefix))

            For Each row As SelectStatementResultRow In newData.ResultSet(0).Rows
                If row IsNot Nothing Then
                    If row.Values(0).ToString.StartsWith("1") Or row.Values(0).ToString.StartsWith("2") Then
                        'If row.Values(0).ToString <> "30200101.000" Then
                        Dim ADDGL As GL = New GL(Session)
                        ADDGL.BookRefNo = bookRefNo
                        ADDGL.DocuNo = ""
                        ADDGL.DocuDate = newAccStartDate
                        ADDGL.AccountBook = "BL"
                        ADDGL.ListNo = "9999" 'ListNo

                        ADDGL.AccID = row.Values(0)
                        ADDGL.AccountName = row.Values(1)
                        ADDGL.AccDetail = "ยอดยกมา"

                        ADDGL.DrAmnt = row.Values(6)
                        ADDGL.CrAmnt = row.Values(7)
                        ADDGL.Save()
                        'End If
                    End If
                End If
                'objAccJV.Save()

            Next

            Dim data As SelectedData = Session.ExecuteSproc("SP_GetSummaryBalance", New OperandValue(accStartDate), New OperandValue(accEndDate))
            Dim objAccDetail As Account = Session.FindObject(Of Account)(CriteriaOperator.Parse("AccountID='30200101.00000'"))
            Dim newAccStartDateSB As Date = Date.MinValue
            For Each item As AccountPeriodDetail In objAccPr.AccountPeriodDetails
                If item.ItemNo = 1 Then
                    newAccStartDateSB = item.StartDate
                    Exit For
                End If
            Next
            Dim bookRefNoBL As String = ""
            Dim prefixBL As String = "BL" & Date.Now.ToString("yyyyMMdd")
            bookRefNoBL = String.Format("{0}{1:D5}", prefixBL, DistributedIdGeneratorHelper.Generate(Me.Session.DataLayer, Me.GetType().FullName, prefixBL))
            For Each row As SelectStatementResultRow In data.ResultSet(0).Rows
                If row IsNot Nothing Then

                    Dim ADDGL As GL = New GL(Session)
                    ADDGL.BookRefNo = bookRefNoBL
                    ADDGL.DocuNo = ""
                    ADDGL.DocuDate = newAccStartDateSB
                    ADDGL.AccountBook = "BL"
                    ADDGL.ListNo = "9999" 'ListNo

                    ADDGL.AccID = objAccDetail.AccountID
                    ADDGL.AccountName = objAccDetail.AccountName
                    ADDGL.AccDetail = "ยอดยกมา"

                    If CDbl(row.Values(0)) > CDbl(row.Values(1)) Then
                        ADDGL.DrAmnt = CDbl(row.Values(0)) - CDbl(row.Values(1))
                        ADDGL.CrAmnt = 0
                    ElseIf CDbl(row.Values(0)) < CDbl(row.Values(1)) Then
                        ADDGL.DrAmnt = 0
                        ADDGL.CrAmnt = CDbl(row.Values(1)) - CDbl(row.Values(0))
                    Else
                        ADDGL.DrAmnt = 0
                        ADDGL.CrAmnt = 0
                    End If
                    ADDGL.Save()
                End If
            Next

            '4. 
            'Dim objAccPr As AccountPeriod
            Dim newDataSP As SelectedData = Session.ExecuteSproc("SP_GetTrialBalanceV9", New OperandValue(accStartDate), New OperandValue(accEndDate))
            Dim newAccStartDateSP As Date = Date.MinValue
            Dim newAccDate As Date = Today
            For Each item As AccountPeriodDetail In objAccPr.AccountPeriodDetails
                If item.ItemNo = 1 Then
                    newAccStartDateSP = item.StartDate
                    Exit For
                End If
            Next

            Dim bookRefNoSP As String = ""
            Dim prefixSP As String = "BL" & Date.Now.ToString("yyyyMMdd")
            bookRefNoSP = String.Format("{0}{1:D5}", prefixSP, DistributedIdGeneratorHelper.Generate(Me.Session.DataLayer, Me.GetType().FullName, prefixSP))

            For Each row As SelectStatementResultRow In newDataSP.ResultSet(0).Rows
                If row IsNot Nothing Then
                    If row.Values(0).ToString.StartsWith("4") Or row.Values(0).ToString.StartsWith("5") Then
                        'If row.Values(0).ToString = "30200101.00000" Then
                        Dim ADDGL As GL = New GL(Session)
                        ADDGL.BookRefNo = bookRefNo
                        ADDGL.DocuNo = ""
                        ADDGL.DocuDate = newAccStartDateSP.AddDays(-1)
                        ADDGL.AccountBook = "BL"
                        ADDGL.ListNo = "9999" 'ListNo

                        ADDGL.AccID = row.Values(0)
                        ADDGL.AccountName = row.Values(1)
                        ADDGL.AccDetail = "ประมวลผลปิดบัญชี"

                        ADDGL.DrAmnt = row.Values(7)
                        ADDGL.CrAmnt = row.Values(6)
                        ADDGL.Save()
                    End If
                End If
                'End If
                'objAccJV.Save()

            Next

            '1.

            'Dim data As SelectedData = Session.ExecuteSproc("SP_GetTrialBalanceV9", New OperandValue(accStartDate), New OperandValue(accEndDate))
            Dim dataGB As SelectedData = Session.ExecuteSproc("SP_GetSummaryBalance", New OperandValue(accStartDate), New OperandValue(accEndDate))
            Dim objAccDetailGB As Account = Session.FindObject(Of Account)(CriteriaOperator.Parse("AccountID='30200101.00000'"))
            For Each item As AccountPeriodDetail In objAccPr.AccountPeriodDetails
                If item.ItemNo = 1 Then
                    newAccStartDateSB = item.StartDate
                    Exit For
                End If
            Next
            Dim bookRefNoGB As String = ""
            Dim prefixGB As String = "BL" & Date.Now.ToString("yyyyMMdd")
            bookRefNoGB = String.Format("{0}{1:D5}", prefixGB, DistributedIdGeneratorHelper.Generate(Me.Session.DataLayer, Me.GetType().FullName, prefixGB))
            For Each row As SelectStatementResultRow In data.ResultSet(0).Rows
                If row IsNot Nothing Then

                    Dim ADDGL As GL = New GL(Session)
                    ADDGL.BookRefNo = bookRefNoGB
                    ADDGL.DocuNo = ""
                    ADDGL.DocuDate = newAccStartDateSB.AddDays(-1)
                    ADDGL.AccountBook = "BL"
                    ADDGL.ListNo = "9999" 'ListNo

                    ADDGL.AccID = objAccDetail.AccountID
                    ADDGL.AccountName = objAccDetail.AccountName
                    ADDGL.AccDetail = "ประมวลผลปิดบัญชี"

                    If CDbl(row.Values(0)) > CDbl(row.Values(1)) Then
                        ADDGL.DrAmnt = CDbl(row.Values(0)) - CDbl(row.Values(1))
                        ADDGL.CrAmnt = 0
                    ElseIf CDbl(row.Values(0)) < CDbl(row.Values(1)) Then
                        ADDGL.DrAmnt = 0
                        ADDGL.CrAmnt = CDbl(row.Values(1)) - CDbl(row.Values(0))
                    Else
                        ADDGL.DrAmnt = 0
                        ADDGL.CrAmnt = 0
                    End If

                    ADDGL.Save()
                End If
            Next

            Session.CommitTransaction()

        Catch ex As Exception
            Session.RollbackTransaction()
        End Try
    End Sub

    '<Action(Caption:="ปิดบัญชี", ConfirmationMessage:="ต้องการบันทึกข้อมูล ใช่หรือไม่?", ImageName:="Action_ResetPassword", AutoCommit:=True)> _
    'Public Sub CloseAccountPeriod()
    '    '1. เปลี่ยน status = ปิด
    '    '2. สร้างงวดบัญชี Auto
    '    '3. สร้างข้อมูลใบผ่านรายวัน

    '    '1. สร้างใบผ่านรายวัน งวดสุดท้าย ปิดบัญชี
    '    '2. เปลี่ยนสถานะปีปัจจุบัน = ปิด
    '    '3. สร้างงวดบัญชีปีถัดไป
    '    '4. สร้างรายวัน BL คล้ายกับ Post จาก (ยอดยกมา) BringForward ลง GL

    '    Try

    '        Dim accStartDate As Date = Date.MinValue
    '        Dim accEndDate As Date = Date.MinValue
    '        For Each item As AccountPeriodDetail In AccountPeriodDetails
    '            If item.ItemNo = 1 Then
    '                accStartDate = item.StartDate
    '                'Exit For
    '            End If
    '            If item.ItemNo = 12 Then
    '                accEndDate = item.EndDate
    '            End If
    '        Next

    '        '2.
    '        Me.Status = AccountStatus.DeActivate

    '        '3
    '        Dim objAccJV As New AccountBookJV(Session) ' Add Master
    '        Dim objAccid As AccountID
    '        objAccJV.AccountBookJV = Session.FindObject(Of AccountBookID)(CriteriaOperator.Parse("AccountBookNo=3"))
    '        objAccJV.DetailJV = "รายได้สูงกว่ารายจ่ายสะสม"
    '        objAccJV.ListNoJV = "0000"
    '        objAccJV.AmountJV = objAccJV.TotalDebitJV
    '        objAccJV.Save()
    '        ' End With
    '        '-------------

    '        '4. 
    '        'Dim objAccPr As AccountPeriod
    '        Dim newData As SelectedData = Session.ExecuteSproc("SP_GetTrialBalanceV9", New OperandValue(accStartDate), New OperandValue(accEndDate))
    '        Dim newAccStartDate As Date = Date.MinValue
    '        For Each item As AccountPeriodDetail In AccountPeriodDetails 'objAccPr.AccountPeriodDetails
    '            If item.ItemNo = 1 Then
    '                newAccStartDate = item.StartDate
    '                Exit For
    '            End If
    '        Next

    '        'Dim bookRefNo As String = ""
    '        'Dim prefix As String = "BL" & Date.Now.ToString("yyyyMMdd")
    '        'bookRefNo = String.Format("{0}{1:D5}", prefix, DistributedIdGeneratorHelper.Generate(Me.Session.DataLayer, Me.GetType().FullName, prefix))
    '        'Dim objAccount As Account = Session.FindObject(Of Account)(CriteriaOperator.Parse(""))
    '        For Each row As SelectStatementResultRow In newData.ResultSet(0).Rows
    '            If row IsNot Nothing Then
    '                Dim objAccount As Account = Session.FindObject(Of Account)(CriteriaOperator.Parse("AccountID=?", row.Values(0)))
    '                Dim ADDAccID = New AccountID(Session)
    '                ADDAccID.Account = objAccount
    '                ADDAccID.AccountName = row.Values(1)
    '                ADDAccID.Debit = row.Values(7)
    '                ADDAccID.Credit = row.Values(6)
    '                ADDAccID.Save()
    '                objAccount.Save()
    '            End If
    '        Next
    '        Dim data As SelectedData = Session.ExecuteSproc("SP_GetSummaryBalance", New OperandValue(accStartDate), New OperandValue(accEndDate))
    '        Dim objAccDetail As Account = Session.FindObject(Of Account)(CriteriaOperator.Parse("AccountID='30200101.00000'"))
    '        'Dim bookRefNoBL As String = ""
    '        'Dim prefixBL As String = "BL" & Date.Now.ToString("yyyyMMdd")
    '        'bookRefNoBL = String.Format("{0}{1:D5}", prefixBL, DistributedIdGeneratorHelper.Generate(Me.Session.DataLayer, Me.GetType().FullName, prefixBL))
    '        For Each row As SelectStatementResultRow In data.ResultSet(0).Rows
    '            If row IsNot Nothing Then
    '                Dim ADDGL = New AccountID(Session)
    '                ADDGL.Account = objAccDetail
    '                ADDGL.AccountName = objAccDetail.AccountName
    '                ADDGL.Debit = row.Values(1)
    '                ADDGL.Credit = row.Values(0)
    '                ADDGL.Save()
    '                objAccDetail.Save()
    '            End If
    '        Next

    '        Session.CommitTransaction()

    '    Catch ex As Exception
    '        Session.RollbackTransaction()
    '    End Try

    'End Sub

    Public Enum AccMonth
        <XafDisplayName("มกราคม")> _
        January = 1
        <XafDisplayName("กุมภาพันธ์")> _
        February = 2
        <XafDisplayName("มีนาคม")> _
        March = 3
        <XafDisplayName("เมษายน")> _
        April = 4
        <XafDisplayName("พฤษภาคม")> _
        May = 5
        <XafDisplayName("มิถุนายน")> _
        June = 6
        <XafDisplayName("กรกฎาคม")> _
        July = 7
        <XafDisplayName("สิงหาคม")> _
        August = 8
        <XafDisplayName("กันยายน")> _
        September = 9
        <XafDisplayName("ตุลาคม")> _
        October = 10
        <XafDisplayName("พฤศจิกายน")> _
        November = 11
        <XafDisplayName("ธันวาคม")> _
        December = 12
    End Enum
End Class
