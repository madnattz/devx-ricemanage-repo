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
Imports DevExpress.ExpressApp.ConditionalAppearance

'<ImageName("BO_Contact")> _
'<DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")> _
'<DefaultListViewOptions(MasterDetailMode.ListViewOnly, False, NewItemRowPosition.None)> _
'<Persistent("DatabaseTableName")> _
<XafDisplayName("ส่งรายงาน งบทดลองรายไตรมาส")> _
<ConditionalAppearance.Appearance("SubmitTrialBalacneQuarterReportDisableAllItems", criteria:="Status!='Draft'", Enabled:=False, TargetItems:="*", Context:="DetailView")> _
<DefaultClassOptions()> _
Public Class SubmitTrialBalacneQuarterReport ' Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        ' Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).
        Me.Status = PublicEnum.SubmitReportStatus.Draft
    End Sub

    Dim fBalanceQuarter As PublicEnum.EnumQuarter
    <XafDisplayName("ไตรมาส")> _
    Public Property BalanceQuarter As PublicEnum.EnumQuarter
        Get
            Return fBalanceQuarter
        End Get
        Set(ByVal value As PublicEnum.EnumQuarter)
            SetPropertyValue(Of PublicEnum.EnumQuarter)("BalanceQuarter", fBalanceQuarter, value)
        End Set
    End Property

    'Dim fBalanceMonth As Integer
    '<XafDisplayName("เดือน")> _
    'Public ReadOnly Property BalanceMonth As Integer
    '    Get
    '        Return fBalanceMonth
    '    End Get
    'End Property

    'Dim fBalanceMonth As PublicEnum.EnumMonth
    '<XafDisplayName("เดือน")> _
    'Public Property BalanceMonth As PublicEnum.EnumMonth
    '    Get
    '        Return fBalanceMonth
    '    End Get
    '    Set(ByVal value As PublicEnum.EnumMonth)
    '        SetPropertyValue(Of PublicEnum.EnumMonth)("BalanceMonth", fBalanceMonth, value)
    '        If BalanceMonth <> 0 Then
    '            Dim Quarter As Integer '= Today.Month
    '            Select Case Quarter
    '                Case 10, 11, 12
    '                    BalanceQuarter = 1
    '                Case 1, 2, 3
    '                    BalanceQuarter = 2
    '                Case 4, 5, 6
    '                    BalanceQuarter = 3
    '                Case 7, 8, 9
    '                    BalanceQuarter = 4
    '            End Select
    '        End If
    '    End Set
    'End Property

    Dim fBalanceYear As String
    <XafDisplayName("ปี")> _
    <ModelDefault("PropertyEditorType", "RiceManagementApp.Module.Win.CustomYearDropdown")> _
    Public Property BalanceYear As String
        Get
            Return fBalanceYear
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("BalanceYear", fBalanceYear, value)
        End Set
    End Property

    Dim fSubmitDate As Date = Today
    <Appearance("SubmitDateQuarter", Enabled:=False, Context:="DetailView")> _
    <XafDisplayName("วันที่ส่งรายงาน")> _
    Public Property SubmitDate() As Date
        Get
            Return fSubmitDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue(Of Date)("SubmitDate", fSubmitDate, value)
        End Set
    End Property
    Dim fSubmitBy As String
    <XafDisplayName("ส่งรายงานโดย")> _
    Public Property SubmitBy() As String
        Get
            Return fSubmitBy
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SubmitBy", fSubmitBy, value)
        End Set
    End Property

    Dim fStatus As PublicEnum.SubmitReportStatus
    <XafDisplayName("สถานะ")> _
    Public Property Status() As PublicEnum.SubmitReportStatus
        Get
            Return fStatus
        End Get
        Set(ByVal value As PublicEnum.SubmitReportStatus)
            SetPropertyValue(Of PublicEnum.SubmitReportStatus)("Status", fStatus, value)
        End Set
    End Property

    <Association("SubmitTrialBalacneQuarterReport-TrialBalanceQuarterData", GetType(TrialBalanceQuarterData))> _
<DevExpress.Xpo.Aggregated> _
<XafDisplayName("งบทดลองรายไตมาส")> _
    Public ReadOnly Property TrialBalanceQuarterDatas() As XPCollection(Of TrialBalanceQuarterData)
        Get
            Return GetCollection(Of TrialBalanceQuarterData)("TrialBalanceQuarterDatas")
        End Get
    End Property

    <Action(Caption:="ส่งรายงาน", ConfirmationMessage:="ท่านต้องการส่งข้อมูลรายงานนี้ ใช่หรือไม่?", ImageName:="Action_Grant", AutoCommit:=True, TargetObjectsCriteria:="Status='Draft'")> _
    Public Sub MarkAsComplete()
        If Not IsDeleted Then
            If Not TrialBalanceQuarterDatas.Count > 0 Then
                MsgBox("ไม่พบข้อมูลงบทดลอง กรุณาตรวจสอบข้อมูลอีกครั้ง", MsgBoxStyle.Information, "")
                Exit Sub
            End If
            Try
                '//ประกาศตัวแปล webservice
                Dim objService As New RiceService.RiceService

                '//ประกาศ Object ของรายงาน ขพ.2 (Header)
                Dim objData As New RiceService.TrialBalanceQuarterHeader

                '//ข้อมูลตั้งค่าของศูนย์
                Dim objSiteInfo As SiteSetting = GetSiteInfo()

                If objService.CheckCanSubmit(objSiteInfo.SiteNo, "รายงาน รายไตรมาส (งบดุล งบรายได้ค่าใช้จ่าย)", BalanceQuarter, BalanceYear, 1) = False Then
                    MsgBox("ขณะนี้ได้มีการปิดการรับส่งข้อมูล")
                    Exit Sub
                End If

                '//ใส่ค่าให้กับ object รายงาน ขพ.2 (ข้อมูลส่วน Header)
                objData.SiteNo = objSiteInfo.SiteNo
                objData.SiteName = objSiteInfo.SiteName
                objData.BalanceDate = SubmitDate
                objData.SentDate = Date.Now
                objData.BalanceQuarter = BalanceQuarter
                '///---f-dsfdsfdsfds 
                objData.BalanceYear = BalanceYear
                objData.SentBy = CType(SecuritySystem.CurrentUser, User).DisplayName

                'BalanceDate, SentDate, [Year], SentBy
                'objData.SubmitDate = Date.Now

                '//ตัวแปลของ ขพ.2 (Detail) (รายชื่อ และที่อยู่ เกษตรกร) ประเภท List
                Dim listOfDetail As New List(Of RiceService.TrialBalanceQuarterDetail)

                For Each row As TrialBalanceQuarterData In Me.TrialBalanceQuarterDatas
                    Dim objDetail As New RiceService.TrialBalanceQuarterDetail
                    objDetail.AccountID = row.AccountID
                    objDetail.AccountName = row.AccountName
                    objDetail.TotalDr = row.TotalDr
                    objDetail.TotalCr = row.TotalCr

                    listOfDetail.Add(objDetail)
                Next

                '//ใส่รายชื่อเกษตรกร (Detail) ในส่วนของ Header
                '//ต้องแปลง List ให้เป็น Array เพราะส่งข้อมูลผ่านเว็บเซอร์วิส ต้องเป็น Array 
                objData.TrialBalanceQuarterDetail = listOfDetail.ToArray

                '//ส่งข้อมูลผ่าน Webservice
                Dim ret As String = objService.SubmitDataTrialBalanceQuarter("NTiSecureCode", objData)

                '//เป็นสถานะเป็น ส่งข้อมูลแล้ว (Sent)
                If ret = "Success" Then
                    Me.Status = PublicEnum.SubmitReportStatus.Sent
                    Me.SubmitDate = Date.Now
                    Me.SubmitBy = CType(SecuritySystem.CurrentUser, User).DisplayName
                    '//บันทึกข้อมูล
                    MsgBox("ส่งข้อมูลรายงาน เรียบร้อยแล้ว", MsgBoxStyle.Information)
                    Me.Save()
                Else
                    MsgBox("เกิดข้อผิดพลาด ไม่สามารถส่งรายงานได้ กรุณาติดต่อผู้ดูแลระบบ", MsgBoxStyle.Critical)
                End If

            Catch ex As Exception
                MsgBox("เกิดข้อผิดพลาด ไม่สามารถส่งรายงานได้ กรุณาติดต่อผู้ดูแลระบบ", MsgBoxStyle.Critical)
            End Try

        End If
    End Sub

    <Action(Caption:="ดึงข้อมูล", ConfirmationMessage:="ท่านต้องการดึงข้อมูล ใช่หรือไม่?", ImageName:="BO_Note", AutoCommit:=True, TargetObjectsCriteria:="Status='Draft'")> _
    Public Sub ImportData()
        Session.Delete(TrialBalanceQuarterDatas)
        ' Dim fAccountBalances As New BindingList(Of TrialBalanceMonthData)
        ''fPlanfarmers = New XPCollection(Of PlanFarmer)(Session, CriteriaOperator.Parse("PlanFarmerGroup.MainPlan=?", Me.MainPlan))
        Dim daysOfMonth As Integer = 0
        Dim sDate As New Date
        Dim eDate As New Date
        Dim sMonth As Integer = 0
        Dim eMonth As Integer = 0
        Select Case BalanceQuarter
            Case "1"
                sMonth = 10
                eMonth = 12
                daysOfMonth = DateTime.DaysInMonth(CInt(BalanceYear) - 544, (eMonth))
                sDate = New Date(CInt(BalanceYear) - 544, CUInt(sMonth), 1)
                eDate = New Date(CInt(BalanceYear) - 544, CInt(eMonth), daysOfMonth)
            Case "2"
                sMonth = 1
                eMonth = 3
                daysOfMonth = DateTime.DaysInMonth(CInt(BalanceYear) - 543, (eMonth))
                sDate = New Date(CInt(BalanceYear) - 543, CUInt(sMonth), 1)
                eDate = New Date(CInt(BalanceYear) - 543, CInt(eMonth), daysOfMonth)
            Case "3"
                sMonth = 4
                eMonth = 6
                daysOfMonth = DateTime.DaysInMonth(CInt(BalanceYear) - 543, (eMonth))
                sDate = New Date(CInt(BalanceYear) - 543, CUInt(sMonth), 1)
                eDate = New Date(CInt(BalanceYear) - 543, CInt(eMonth), daysOfMonth)
            Case "4"
                sMonth = 7
                eMonth = 9
                daysOfMonth = DateTime.DaysInMonth(CInt(BalanceYear) - 543, (eMonth))
                sDate = New Date(CInt(BalanceYear) - 543, CUInt(sMonth), 1)
                eDate = New Date(CInt(BalanceYear) - 543, CInt(eMonth), daysOfMonth)
        End Select


        'sDate = New Date(CInt(BalanceYear) - 543, CUInt(sMonth), 1)
        'eDate = New Date(CInt(BalanceYear) - 543, CInt(eMonth), daysOfMonth)
        Dim data As SelectedData = Session.ExecuteSproc("SP_GetTrialBalanceV7", New OperandValue("00000000.00000"), New OperandValue("60000000.00000"), New OperandValue(sDate), New OperandValue(eDate))

        For Each row As SelectStatementResultRow In data.ResultSet(0).Rows
            If row IsNot Nothing Then
                Dim tBalance As New TrialBalanceQuarterData(Session)
                tBalance.AccountID = row.Values(0)
                tBalance.AccountName = row.Values(1)
                If tBalance.AccountID.StartsWith("1") Or tBalance.AccountID.StartsWith("5") Then
                    tBalance.TotalDr = row.Values(6)
                Else
                    tBalance.TotalCr = row.Values(7)
                End If
                TrialBalanceQuarterDatas.Add(tBalance)
            End If
            OnChanged("TrialBalanceQuarterData")
        Next

        ''End If
        ''OnChanged("SubmitCheckFarmReportDetails")

    End Sub

    Public Function ConvertToInteger(obj As Object) As Integer
        Try
            Return Convert.ToInt32(obj)
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Protected Function GetSiteInfo() As SiteSetting
        Dim objSite As New XPCollection(Of SiteSetting)(Session)
        Return objSite(0)
    End Function
End Class
