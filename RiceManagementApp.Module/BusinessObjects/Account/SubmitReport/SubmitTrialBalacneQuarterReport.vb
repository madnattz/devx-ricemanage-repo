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
<XafDisplayName("����§ҹ �����ͧ��������")> _
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
    <XafDisplayName("�����")> _
    Public Property BalanceQuarter As PublicEnum.EnumQuarter
        Get
            Return fBalanceQuarter
        End Get
        Set(ByVal value As PublicEnum.EnumQuarter)
            SetPropertyValue(Of PublicEnum.EnumQuarter)("BalanceQuarter", fBalanceQuarter, value)
        End Set
    End Property

    'Dim fBalanceMonth As Integer
    '<XafDisplayName("��͹")> _
    'Public ReadOnly Property BalanceMonth As Integer
    '    Get
    '        Return fBalanceMonth
    '    End Get
    'End Property

    'Dim fBalanceMonth As PublicEnum.EnumMonth
    '<XafDisplayName("��͹")> _
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
    <XafDisplayName("��")> _
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

    <Association("SubmitTrialBalacneQuarterReport-TrialBalanceQuarterData", GetType(TrialBalanceQuarterData))> _
<DevExpress.Xpo.Aggregated> _
<XafDisplayName("�����ͧ�������")> _
    Public ReadOnly Property TrialBalanceQuarterDatas() As XPCollection(Of TrialBalanceQuarterData)
        Get
            Return GetCollection(Of TrialBalanceQuarterData)("TrialBalanceQuarterDatas")
        End Get
    End Property

    <Action(Caption:="����§ҹ", ConfirmationMessage:="��ҹ��ͧ����觢�������§ҹ��� ���������?", ImageName:="Action_Grant", AutoCommit:=True, TargetObjectsCriteria:="Status='Draft'")> _
    Public Sub MarkAsComplete()
        If Not IsDeleted Then
            If Not TrialBalanceQuarterDatas.Count > 0 Then
                MsgBox("��辺�����ŧ����ͧ ��سҵ�Ǩ�ͺ�������ա����", MsgBoxStyle.Information, "")
                Exit Sub
            End If
            Try
                '//��С�ȵ���� webservice
                Dim objService As New RiceService.RiceService

                '//��С�� Object �ͧ��§ҹ ��.2 (Header)
                Dim objData As New RiceService.TrialBalanceQuarterHeader

                '//�����ŵ�駤�Ңͧ�ٹ��
                Dim objSiteInfo As SiteSetting = GetSiteInfo()

                If objService.CheckCanSubmit(objSiteInfo.SiteNo, "��§ҹ �������� (����� ��������������)", BalanceQuarter, BalanceYear, 1) = False Then
                    MsgBox("��й�����ա�ûԴ����Ѻ�觢�����")
                    Exit Sub
                End If

                '//��������Ѻ object ��§ҹ ��.2 (��������ǹ Header)
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

                '//����Ţͧ ��.2 (Detail) (��ª��� ��з������ �ɵá�) ������ List
                Dim listOfDetail As New List(Of RiceService.TrialBalanceQuarterDetail)

                For Each row As TrialBalanceQuarterData In Me.TrialBalanceQuarterDatas
                    Dim objDetail As New RiceService.TrialBalanceQuarterDetail
                    objDetail.AccountID = row.AccountID
                    objDetail.AccountName = row.AccountName
                    objDetail.TotalDr = row.TotalDr
                    objDetail.TotalCr = row.TotalCr

                    listOfDetail.Add(objDetail)
                Next

                '//�����ª����ɵá� (Detail) ���ǹ�ͧ Header
                '//��ͧ�ŧ List ����� Array �����觢����ż�ҹ���������� ��ͧ�� Array 
                objData.TrialBalanceQuarterDetail = listOfDetail.ToArray

                '//�觢����ż�ҹ Webservice
                Dim ret As String = objService.SubmitDataTrialBalanceQuarter("NTiSecureCode", objData)

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
