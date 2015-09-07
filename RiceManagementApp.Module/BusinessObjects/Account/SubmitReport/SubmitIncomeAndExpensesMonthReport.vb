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
<XafDisplayName("����§ҹ �š���Ѻ - ���� �Թ��Ш���͹")> _
<ConditionalAppearance.Appearance("SubmitIncomeAndExpensesReportDisableAllItems", criteria:="Status!='Draft'", Enabled:=False, TargetItems:="*", Context:="DetailView")> _
<DefaultClassOptions()> _
Public Class SubmitIncomeAndExpensesMonthReport ' Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        Me.Status = PublicEnum.SubmitReportStatus.Draft
        MyBase.AfterConstruction()
        ' Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).
    End Sub

    Private _ResultYear As String
    <XafDisplayName("��")> _
    <Index(0), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    Public Property ResultYear() As String
        Get
            Return _ResultYear
        End Get
        Set(ByVal value As String)
            SetPropertyValue("ResultYear", _ResultYear, value)
        End Set
    End Property

    Private _ResultMonth As PublicEnum.EnumMonth
    <XafDisplayName("��͹")> _
    <Index(1), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property ResultMonth() As PublicEnum.EnumMonth
        Get
            Return _ResultMonth
        End Get
        Set(ByVal value As PublicEnum.EnumMonth)
            SetPropertyValue("ResultMonth", _ResultMonth, value)
        End Set
    End Property

    Dim fSubmitDate As Date = Today
    <Appearance("SubmitDateBudget02", Enabled:=False, Context:="DetailView")> _
    <XafDisplayName("�ѹ�������§ҹ")> _
    Public Property SubmitDate() As Date
        Get
            Return fSubmitDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue(Of Date)("SubmitDate", fSubmitDate, value)
        End Set
    End Property

    Dim fSubmitBy As String
    <XafDisplayName("����§ҹ��")> _
    Public Property SubmitBy() As String
        Get
            Return fSubmitBy
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SubmitBy", fSubmitBy, value)
        End Set
    End Property

    Dim fStatus As PublicEnum.SubmitReportStatus
    <XafDisplayName("ʶҹ�")> _
    Public Property Status() As PublicEnum.SubmitReportStatus
        Get
            Return fStatus
        End Get
        Set(ByVal value As PublicEnum.SubmitReportStatus)
            SetPropertyValue(Of PublicEnum.SubmitReportStatus)("Status", fStatus, value)
        End Set
    End Property

    <Association("SubmitIncomeAndExpensesMonthReport-ResultMonthIncomeDatas", GetType(ResultMonthIncomeData))> _
<DevExpress.Xpo.Aggregated> _
<XafDisplayName("�š�����Ѻ�͹�Թ (����Ѻ)")> _
    Public ReadOnly Property ResultMonthIncomeDatas() As XPCollection(Of ResultMonthIncomeData)
        Get
            Return GetCollection(Of ResultMonthIncomeData)("ResultMonthIncomeDatas")
        End Get
    End Property

    <Association("SubmitIncomeAndExpensesMonthReport-ResultMonthExpensesDatas", GetType(ResultMonthExpensesData))> _
<DevExpress.Xpo.Aggregated> _
<XafDisplayName("�š�����Ѻ�͹�Թ (��¨���)")> _
    Public ReadOnly Property ResultMonthExpensesDatas() As XPCollection(Of ResultMonthExpensesData)
        Get
            Return GetCollection(Of ResultMonthExpensesData)("ResultMonthExpensesDatas")
        End Get
    End Property

    <XafDisplayName("��§ҹ ����Ѻ")> _
<Association("SubmitIncomeAndExpensesMonthReport-AccountGFMISDetailRVDatas", GetType(AccountGFMISDetailRVData))> _
<DC.Aggregated()> _
    Public ReadOnly Property AccountGFMISDetailRVDatas() As XPCollection(Of AccountGFMISDetailRVData)
        Get
            Return GetCollection(Of AccountGFMISDetailRVData)("AccountGFMISDetailRVDatas")
        End Get
    End Property

    <XafDisplayName("��§ҹ ��¨���")> _
<Association("SubmitIncomeAndExpensesMonthReport-AccountGFMISDetailPVDatas", GetType(AccountGFMISDetailPVData))> _
<DC.Aggregated()> _
    Public ReadOnly Property AccountGFMISDetailPVDatas() As XPCollection(Of AccountGFMISDetailPVData)
        Get
            Return GetCollection(Of AccountGFMISDetailPVData)("AccountGFMISDetailPVDatas")
        End Get
    End Property

    <Action(Caption:="����§ҹ", ConfirmationMessage:="��ҹ��ͧ����觢�������§ҹ��� ���������?", ImageName:="Action_Grant", AutoCommit:=True, TargetObjectsCriteria:="Status='Draft'")> _
    Public Sub MarkAsComplete()
        If Not IsDeleted Then
            If Not ResultMonthIncomeDatas.Count > 0 Then
                MsgBox("��辺��¡�üš���Ѻ �Թ��Ш���͹ ��سҵ�Ǩ�ͺ�������ա����", MsgBoxStyle.Information, "")
                Exit Sub
            End If
            If Not ResultMonthExpensesDatas.Count > 0 Then
                MsgBox("��辺��¡�üš�è��� �Թ��Ш���͹ ��سҵ�Ǩ�ͺ�������ա����", MsgBoxStyle.Information, "")
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
            '//��С�ȵ���� webservice
            Dim objService As New RiceService.RiceService

            '//��С�� Object �ͧ��§ҹ ��.2 (Header)
            Dim objData As New RiceService.ResultIncomeAndExpensesMonth
            Try
                '//�����ŵ�駤�Ңͧ�ٹ��
                Dim objSiteInfo As SiteSetting = GetSiteInfo()

                If objService.CheckCanSubmit(objSiteInfo.SiteNo, "��§ҹ ����Ѻ - ��¨���  (��.03)", ResultMonth, ResultYear, 1) = False Then
                    MsgBox("��й�����ա�ûԴ����Ѻ�觢�����")
                    Exit Sub
                End If

                '//��������Ѻ object ��§ҹ ��.2 (��������ǹ Header)
                objData.DataOwner = objSiteInfo.Site.Oid.ToString
                objData.SiteNo = objSiteInfo.SiteNo
                objData.SiteName = objSiteInfo.SiteName
                objData.SentDate = Date.Now
                objData.ResultYear = ResultYear
                objData.ResultMonth = ResultMonth
                objData.SentBy = CType(SecuritySystem.CurrentUser, User).DisplayName

                'Catch ex As Exception

                'End Try


                'BalanceDate, SentDate, [Year], SentBy
                'objData.SubmitDate = Date.Now

                '//����Ţͧ ��.2 (Detail) (��ª��� ��з������ �ɵá�) ������ List
                Dim listOfDetailIncome As New List(Of RiceService.ResultMonthIncomeData)
                For Each item As ResultMonthIncomeData In ResultMonthIncomeDatas
                    Dim objDetail As New RiceService.ResultMonthIncomeData
                    objDetail.ResultIncomeOid = item.ResultIncomeOid
                    objDetail.listID = item.listID
                    objDetail.listIncome = item.listIncome
                    objDetail.IncomeMonth = item.IncomeMonth
                    objDetail.ExpendMonth = item.ExpendMonth
                    objDetail.Note = item.Note

                    listOfDetailIncome.Add(objDetail)
                    'listOfMonth.Add(objDetailMonth)
                    'listOfResult.Add(objDetailResult)
                Next

                Dim listOfDetailExpenses As New List(Of RiceService.ResultMonthExpensesData)
                For Each item As ResultMonthExpensesData In ResultMonthExpensesDatas
                    Dim objDetail As New RiceService.ResultMonthExpensesData
                    objDetail.ResultExpensesOid = item.ResultExpensesOid
                    objDetail.listID = item.listID
                    objDetail.listExpenses = item.listExpenses
                    objDetail.IncomeMonth = item.IncomeMonth
                    objDetail.ExpendMonth = item.ExpendMonth
                    objDetail.Note = item.Note

                    listOfDetailExpenses.Add(objDetail)
                    'listOfMonth.Add(objDetailMonth)
                    'listOfResult.Add(objDetailResult)
                Next

                Dim listOfDetailRV As New List(Of RiceService.AccountGFMISDetailRVDatas)
                For Each item As AccountGFMISDetailRVData In AccountGFMISDetailRVDatas
                    Dim objDetailRV As New RiceService.AccountGFMISDetailRVDatas
                    objDetailRV.listGF = item.listGF
                    objDetailRV.CodeGF = item.CodeGF
                    objDetailRV.AmountIncome = item.AmountIncome

                    listOfDetailRV.Add(objDetailRV)
                    'listOfMonth.Add(objDetailMonth)
                    'listOfResult.Add(objDetailResult)
                Next

                Dim listOfDetailPV As New List(Of RiceService.AccountGFMISDetailPVDatas)
                For Each item As AccountGFMISDetailPVData In AccountGFMISDetailPVDatas
                    Dim objDetailPV As New RiceService.AccountGFMISDetailPVDatas
                    objDetailPV.listGF = item.listGF
                    objDetailPV.CodeGF = item.CodeGF
                    objDetailPV.AmountExpenses = item.AmountExpend

                    listOfDetailPV.Add(objDetailPV)
                    'listOfMonth.Add(objDetailMonth)
                    'listOfResult.Add(objDetailResult)
                Next

                '//�����ª����ɵá� (Detail) ���ǹ�ͧ Header
                '//��ͧ�ŧ List ����� Array �����觢����ż�ҹ���������� ��ͧ�� Array 
                objData.ResultMonthDetailIncome = listOfDetailIncome.ToArray
                objData.ResultMonthDetailExpenses = listOfDetailExpenses.ToArray
                objData.AccountGFMISDetailRV = listOfDetailRV.ToArray
                objData.AccountGFMISDetailPV = listOfDetailPV.ToArray


                '//�觢����ż�ҹ Webservice
                Dim ret As String = objService.SubmitDataIncomeAndExpensesMonth("NTiSecureCode", objData)

                '//��ʶҹ��� �觢��������� (Sent)
                If ret = "Success" Then
                    Me.Status = PublicEnum.SubmitReportStatus.Sent
                    Me.SubmitDate = Date.Now
                    Me.SubmitBy = CType(SecuritySystem.CurrentUser, User).DisplayName
                    '//�ѹ�֡������
                    MsgBox("�觢�������§ҹ ���º��������", MsgBoxStyle.Information)
                    Me.Save()
                Else
                    MsgBox("�Դ��ͼԴ��Ҵ �������ö����§ҹ�� ��سҵԴ��ͼ������к�", MsgBoxStyle.Critical)
                End If

            Catch ex As Exception
                MsgBox("�Դ��ͼԴ��Ҵ �������ö����§ҹ�� ��سҵԴ��ͼ������к�", MsgBoxStyle.Critical)
            End Try

        End If
    End Sub

    <Action(Caption:="�֧������", ConfirmationMessage:="��ҹ��ͧ��ô֧������ ���������?", ImageName:="BO_Note", AutoCommit:=True, TargetObjectsCriteria:="Status='Draft'")> _
    Public Sub ImportData()
        Session.Delete(ResultMonthIncomeDatas)
        Session.Delete(ResultMonthExpensesDatas)
        Session.Delete(AccountGFMISDetailRVDatas)
        Session.Delete(AccountGFMISDetailPVDatas)

        Dim _ResultMonthIncomeDatas = New XPCollection(Of TrialBalanceIncome)(Session, CriteriaOperator.Parse("TrialBalanceIncomes.YearID=? AND TrialBalanceIncomes.MonthTrialBalance=?", ResultYear, ResultMonth))
        If _ResultMonthIncomeDatas.Count > 0 Then
            For Each TblEventResultMonthDetailIncome As TrialBalanceIncome In _ResultMonthIncomeDatas
                Dim InsertResultMonthDataIncome As New ResultMonthIncomeData(Session)
                With InsertResultMonthDataIncome
                    .SubmitResultMonthReport = Me
                    .ResultIncomeOid = TblEventResultMonthDetailIncome.listID.Oid.ToString
                    .listID = TblEventResultMonthDetailIncome.listID.listID
                    .listIncome = TblEventResultMonthDetailIncome.listIncome
                    .IncomeMonth = TblEventResultMonthDetailIncome.IncomeMonth
                    .ExpendMonth = TblEventResultMonthDetailIncome.ExpendMonth
                    .Note = TblEventResultMonthDetailIncome.Note
                End With
            Next
        End If
        OnChanged("ResultMonthIncomeData")
        'Add data to PlanBudgetMonthData01

        Dim _ResultMonthExpensesDatas = New XPCollection(Of TrialBalanceExpenses)(Session, CriteriaOperator.Parse("TrialBalanceExpenses.YearID=? AND TrialBalanceExpenses.MonthTrialBalance=?", ResultYear, ResultMonth))
        If _ResultMonthExpensesDatas.Count > 0 Then
            For Each TblEventPlanTransferDetailExpenses As TrialBalanceExpenses In _ResultMonthExpensesDatas
                Dim InsertResultMonthDataExpenses As New ResultMonthExpensesData(Session)
                With InsertResultMonthDataExpenses
                    .SubmitResultMonthReport = Me
                    .ResultExpensesOid = TblEventPlanTransferDetailExpenses.listID.Oid.ToString
                    .listID = TblEventPlanTransferDetailExpenses.listID.listID
                    .listExpenses = TblEventPlanTransferDetailExpenses.listExpenses
                    .IncomeMonth = TblEventPlanTransferDetailExpenses.IncomeMonth
                    .ExpendMonth = TblEventPlanTransferDetailExpenses.ExpendMonth
                    .Note = TblEventPlanTransferDetailExpenses.Note
                End With
            Next
            'Add data to ResultBudgetData01
        End If
        OnChanged("ResultMonthExpensesData")

        Dim _AccountGFMISDetailRVDatas = New XPCollection(Of AccountGFMISDetailRV)(Session, CriteriaOperator.Parse("GFMIS.YearID=? AND GFMIS.MonthTrialBalance=?", ResultYear, ResultMonth))
        If _AccountGFMISDetailRVDatas.Count > 0 Then
            For Each TblEventAccountGFMISDetailRV As AccountGFMISDetailRV In _AccountGFMISDetailRVDatas
                Dim InsertAccountGFMISDetailRV As New AccountGFMISDetailRVData(Session)
                With InsertAccountGFMISDetailRV
                    .GFMIS = Me
                    .listGF = TblEventAccountGFMISDetailRV.listGF
                    .CodeGF = TblEventAccountGFMISDetailRV.CodeGF
                    .AmountIncome = TblEventAccountGFMISDetailRV.AmountIncome
                End With
            Next
            'Add data to ResultBudgetData01
        End If
        OnChanged("AccountGFMISDetailRVDatas")

        Dim _AccountGFMISDetailPVDatas = New XPCollection(Of AccountGFMISDetailPV)(Session, CriteriaOperator.Parse("GFMIS.YearID=? AND GFMIS.MonthTrialBalance=?", ResultYear, ResultMonth))
        If _AccountGFMISDetailPVDatas.Count > 0 Then
            For Each TblEventAccountGFMISDetailPV As AccountGFMISDetailPV In _AccountGFMISDetailPVDatas
                Dim InsertAccountGFMISDetailPV As New AccountGFMISDetailPVData(Session)
                With InsertAccountGFMISDetailPV
                    .GFMIS = Me
                    .listGF = TblEventAccountGFMISDetailPV.listGF
                    .CodeGF = TblEventAccountGFMISDetailPV.CodeGF
                    .AmountExpend = TblEventAccountGFMISDetailPV.AmountExpend
                End With
            Next
            'Add data to ResultBudgetData01
        End If
        OnChanged("AccountGFMISDetailPVDatas")



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
