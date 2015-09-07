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
Imports DevExpress.ExpressApp.ConditionalAppearance

'<ImageName("BO_Contact")> _
'<DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")> _
'<DefaultListViewOptions(MasterDetailMode.ListViewOnly, False, NewItemRowPosition.None)> _
'<Persistent("DatabaseTableName")> _
<XafDisplayName("ส่งรายงาน แผนการรับ - จ่าย เงินประจำปีงบประมาณ")> _
<ConditionalAppearance.Appearance("SubmitPlanTransferReportDisableAllItems", criteria:="Status!='Draft'", Enabled:=False, TargetItems:="*", Context:="DetailView")> _
<DefaultClassOptions()> _
Public Class SubmitPlanTransferReport ' Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        Me.Status = PublicEnum.SubmitReportStatus.Draft
        MyBase.AfterConstruction()
        ' Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).
    End Sub

    Private _PlanYear As String
    <XafDisplayName("ปีงบประมาณ")> _
<RuleRequiredField(DefaultContexts.Save)> _
<Index(0)> _
    Public Property PlanYear() As String
        Get
            Return _PlanYear
        End Get
        Set(ByVal value As String)
            SetPropertyValue("PlanYear", _PlanYear, value)
        End Set
    End Property

    Dim fSubmitDate As Date = Today
    <Appearance("SubmitDateBudget02", Enabled:=False, Context:="DetailView")> _
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

    <Association("SubmitPlanTransferReport-PlanTransferDataIncome", GetType(PlanTransferDataIncome))> _
<DevExpress.Xpo.Aggregated> _
<XafDisplayName("แผนการได้รับโอนเงิน (รายรับ)")> _
    Public ReadOnly Property PlanTransferDataIncome() As XPCollection(Of PlanTransferDataIncome)
        Get
            Return GetCollection(Of PlanTransferDataIncome)("PlanTransferDataIncome")
        End Get
    End Property

    <Association("SubmitPlanTransferReport-PlanTransferDataExpenses", GetType(PlanTransferDataExpenses))> _
<DevExpress.Xpo.Aggregated> _
<XafDisplayName("แผนการได้รับโอนเงิน (รายจ่าย)")> _
    Public ReadOnly Property PlanTransferDataExpenses() As XPCollection(Of PlanTransferDataExpenses)
        Get
            Return GetCollection(Of PlanTransferDataExpenses)("PlanTransferDataExpenses")
        End Get
    End Property

    <Action(Caption:="ส่งรายงาน", ConfirmationMessage:="ท่านต้องการส่งข้อมูลรายงานนี้ ใช่หรือไม่?", ImageName:="Action_Grant", AutoCommit:=True, TargetObjectsCriteria:="Status='Draft'")> _
    Public Sub MarkAsComplete()
        If Not IsDeleted Then
            If Not PlanTransferDataIncome.Count > 0 Then
                MsgBox("ไม่พบรายการแผนการรับ เงินประจำปีงบประมาณ กรุณาตรวจสอบข้อมูลอีกครั้ง", MsgBoxStyle.Information, "")
                Exit Sub
            End If
            If Not PlanTransferDataExpenses.Count > 0 Then
                MsgBox("ไม่พบรายการแผนการจ่าย เงินประจำปีงบประมาณ กรุณาตรวจสอบข้อมูลอีกครั้ง", MsgBoxStyle.Information, "")
                Exit Sub
            End If

            'Dim accStartDate As Date = Date.MinValue
            'Dim accEndDate As Date = Date.MinValue
            'For Each item As AccountPeriodDetail In AccountPeriodDetails
            '    If item.ItemNo = 1 Then
            '        accStartDate = item.StartDate
            '        'Exit For
            '    End If
            'Next
            '//ประกาศตัวแปล webservice
            Dim objService As New RiceService.RiceService

            '//ประกาศ Object ของรายงาน ขพ.2 (Header)
            Dim objData As New RiceService.PlanTrialBalanceTransfer03
            Try
                '//ข้อมูลตั้งค่าของศูนย์
                Dim objSiteInfo As SiteSetting = GetSiteInfo()

                '//ใส่ค่าให้กับ object รายงาน ขพ.2 (ข้อมูลส่วน Header)
                objData.DataOwner = objSiteInfo.Site.Oid.ToString
                objData.SiteNo = objSiteInfo.SiteNo
                objData.SiteName = objSiteInfo.SiteName
                objData.SentDate = Date.Now
                objData.PlanYear = PlanYear
                objData.SentBy = CType(SecuritySystem.CurrentUser, User).DisplayName

                'Catch ex As Exception

                'End Try


                'BalanceDate, SentDate, [Year], SentBy
                'objData.SubmitDate = Date.Now

                '//ตัวแปลของ ขพ.2 (Detail) (รายชื่อ และที่อยู่ เกษตรกร) ประเภท List
                Dim listOfDetailIncome As New List(Of RiceService.TrialBalanceTransferIncomeData)
                For Each item As PlanTransferDataIncome In PlanTransferDataIncome
                    Dim objDetail As New RiceService.TrialBalanceTransferIncomeData
                    objDetail.SettingIncomeOid = item.SettingIncomeOid
                    objDetail.CatalogueNo = item.CatalogueNo
                    objDetail.listIncome = item.listIncome
                    objDetail.October = item.October
                    objDetail.November = item.November
                    objDetail.December = item.December
                    objDetail.January = item.January
                    objDetail.February = item.February
                    objDetail.March = item.March
                    objDetail.April = item.April
                    objDetail.May = item.May
                    objDetail.June = item.June
                    objDetail.July = item.July
                    objDetail.August = item.August
                    objDetail.September = item.September

                    listOfDetailIncome.Add(objDetail)
                    'listOfMonth.Add(objDetailMonth)
                    'listOfResult.Add(objDetailResult)
                Next

                Dim listOfDetailExpenses As New List(Of RiceService.TrialBalanceTransferExpensesData)
                For Each item As PlanTransferDataExpenses In PlanTransferDataExpenses
                    Dim objDetail As New RiceService.TrialBalanceTransferExpensesData
                    objDetail.SettingExpensesOid = item.SettingExpensesOid
                    objDetail.CatalogueNo = item.CatalogueNo
                    objDetail.listExpenses = item.listExpenses
                    objDetail.October = item.October
                    objDetail.November = item.November
                    objDetail.December = item.December
                    objDetail.January = item.January
                    objDetail.February = item.February
                    objDetail.March = item.March
                    objDetail.April = item.April
                    objDetail.May = item.May
                    objDetail.June = item.June
                    objDetail.July = item.July
                    objDetail.August = item.August
                    objDetail.September = item.September

                    listOfDetailExpenses.Add(objDetail)
                    'listOfMonth.Add(objDetailMonth)
                    'listOfResult.Add(objDetailResult)
                Next

                '//ใส่รายชื่อเกษตรกร (Detail) ในส่วนของ Header
                '//ต้องแปลง List ให้เป็น Array เพราะส่งข้อมูลผ่านเว็บเซอร์วิส ต้องเป็น Array 
                objData.PlanTransferDetailIncome = listOfDetailIncome.ToArray
                objData.PlanTransferDetailExpend = listOfDetailExpenses.ToArray
                'objData.PlanBudgetDetails = listOfMonth.ToArray
                'objData.PlanBudgetDetails = listOfResult.ToArray


                '//ส่งข้อมูลผ่าน Webservice
                Dim ret As String = objService.SubmitDataTrialBalanceTransfer("NTiSecureCode", objData)

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
        Session.Delete(PlanTransferDataIncome)
        Session.Delete(PlanTransferDataExpenses)
        Dim _PlanTransferDataIncome = New XPCollection(Of TrialBalanceTransferDetailIncome03)(Session, CriteriaOperator.Parse("TrialBalanceTransfer03.FiscalYear=?", PlanYear))
        If _PlanTransferDataIncome.Count > 0 Then
            For Each TblEventPlanTransferDetailIncome As TrialBalanceTransferDetailIncome03 In _PlanTransferDataIncome
                Dim InsertPlanTransferDataIncome As New PlanTransferDataIncome(Session)
                With InsertPlanTransferDataIncome
                    .SubmitPlanTransferReport = Me
                    .SettingIncomeOid = TblEventPlanTransferDetailIncome.CatalogueNo.Oid.ToString
                    .CatalogueNo = TblEventPlanTransferDetailIncome.CatalogueNo.listID
                    .listIncome = TblEventPlanTransferDetailIncome.listIncome
                    .October = TblEventPlanTransferDetailIncome.October
                    .November = TblEventPlanTransferDetailIncome.November
                    .December = TblEventPlanTransferDetailIncome.December
                    .January = TblEventPlanTransferDetailIncome.January
                    .February = TblEventPlanTransferDetailIncome.February
                    .March = TblEventPlanTransferDetailIncome.March
                    .April = TblEventPlanTransferDetailIncome.April
                    .May = TblEventPlanTransferDetailIncome.May
                    .June = TblEventPlanTransferDetailIncome.June
                    .July = TblEventPlanTransferDetailIncome.July
                    .August = TblEventPlanTransferDetailIncome.August
                    .September = TblEventPlanTransferDetailIncome.September
                End With
            Next
        End If
        OnChanged("PlanTransferDataIncome")
        'Add data to PlanBudgetMonthData01

        Dim _PlanTransferDataExpenses = New XPCollection(Of TrialBalanceTransferDetailExpenses03)(Session, CriteriaOperator.Parse("TrialBalanceTransfer03.FiscalYear=?", PlanYear))
        If _PlanTransferDataExpenses.Count > 0 Then
            For Each TblEventPlanTransferDetailExpenses As TrialBalanceTransferDetailExpenses03 In _PlanTransferDataExpenses
                Dim InsertPlanTransferDataExpenses As New PlanTransferDataExpenses(Session)
                With InsertPlanTransferDataExpenses
                    .SubmitPlanTransferReport = Me
                    .SettingExpensesOid = TblEventPlanTransferDetailExpenses.CatalogueNo.Oid.ToString
                    .CatalogueNo = TblEventPlanTransferDetailExpenses.CatalogueNo.listID
                    .listExpenses = TblEventPlanTransferDetailExpenses.listExpenses
                    .October = TblEventPlanTransferDetailExpenses.October
                    .November = TblEventPlanTransferDetailExpenses.November
                    .December = TblEventPlanTransferDetailExpenses.December
                    .January = TblEventPlanTransferDetailExpenses.January
                    .February = TblEventPlanTransferDetailExpenses.February
                    .March = TblEventPlanTransferDetailExpenses.March
                    .April = TblEventPlanTransferDetailExpenses.April
                    .May = TblEventPlanTransferDetailExpenses.May
                    .June = TblEventPlanTransferDetailExpenses.June
                    .July = TblEventPlanTransferDetailExpenses.July
                    .August = TblEventPlanTransferDetailExpenses.August
                    .September = TblEventPlanTransferDetailExpenses.September
                End With
            Next
            'Add data to ResultBudgetData01
        End If
        OnChanged("PlanTransferDataExpenses")
        'Else
        '    OnChanged("SubmitCheckFarmReportDetails")
        'End If
        'OnChanged("SubmitCheckFarmReportDetails")

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
