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

<XafDisplayName("����§ҹ �����ͧ")> _
<ConditionalAppearance.Appearance("SubmitTrialBalacneReportDisableAllItems", criteria:="Status!='Draft'", Enabled:=False, TargetItems:="*", Context:="DetailView")> _
<DefaultClassOptions()> _
Public Class SubmitTrialBalacneReport
    Inherits BaseObject
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        'Me.SubmitDate = Date.Today
        'Me.SubmitBy = CType(SecuritySystem.CurrentUser, User).DisplayName
        Me.Status = PublicEnum.SubmitReportStatus.Draft
    End Sub


    Dim fStartDate As Date
    <XafDisplayName("�ҡ�ѹ���")> _
    Public Property StartDate As Date
        Get
            Return fStartDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue(Of Date)("StartDate", fStartDate, value)
        End Set
    End Property

    Dim fEndDate As Date
    <XafDisplayName("�֧�ѹ���")> _
    Public Property EndDate As Date
        Get
            Return fEndDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue(Of Date)("EndDate", fEndDate, value)
        End Set
    End Property

    Dim fMonth As PublicEnum.EnumMonth
    <XafDisplayName("��͹")> _
    Public Property Month As PublicEnum.EnumMonth
        Get
            Return fMonth
        End Get
        Set(ByVal value As PublicEnum.EnumMonth)
            SetPropertyValue(Of PublicEnum.EnumMonth)("Month", fMonth, value)
        End Set
    End Property

    Dim fSubmitDate As Date = Today
    <Appearance("SubmitDateBalacne", Enabled:=False, Context:="DetailView")> _
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

    <Association("SubmitTrialBalacneReport-TrialBalanceData", GetType(TrialBalanceData))> _
<DevExpress.Xpo.Aggregated> _
<XafDisplayName("�����ͧ")> _
    Public ReadOnly Property TrialBalanceDatas() As XPCollection(Of TrialBalanceData)
        Get
            Return GetCollection(Of TrialBalanceData)("TrialBalanceDatas")
        End Get
    End Property

    '<XafDisplayName("�����ŧ����ͧ")> _
    'Public ReadOnly Property AccountBalances() As BindingList(Of TrialBalanceData)
    '    Get
    '        Dim fAccountBalances As New BindingList(Of TrialBalanceData)
    '        ''fPlanfarmers = New XPCollection(Of PlanFarmer)(Session, CriteriaOperator.Parse("PlanFarmerGroup.MainPlan=?", Me.MainPlan))
    '        Dim data As SelectedData = Session.ExecuteSproc("SP_GetTrialBalanceV7", New OperandValue("00000000.00000"), New OperandValue("60000000.00000"), New OperandValue(StartDate), New OperandValue(EndDate))

    '        For Each row As SelectStatementResultRow In data.ResultSet(0).Rows
    '            If row IsNot Nothing Then
    '                Dim tBalance As New TrialBalanceData(Session)
    '                tBalance.AccountID = row.Values(0)
    '                tBalance.AccountName = row.Values(1)
    '                If tBalance.AccountID.StartsWith("1") Or tBalance.AccountID.StartsWith("5") Then
    '                    If CDbl(row.Values(6)) > CDbl(row.Values(7)) Then
    '                        tBalance.TotalBalance = CDbl(row.Values(6)) - CDbl(row.Values(7))
    '                    ElseIf CDbl(row.Values(7)) > CDbl(row.Values(6)) Then
    '                        tBalance.TotalBalance = CDbl(row.Values(7)) - CDbl(row.Values(6))
    '                    ElseIf CDbl(row.Values(6)) = CDbl(row.Values(7)) Or CDbl(row.Values(7)) = CDbl(row.Values(6)) Then
    '                        tBalance.TotalBalance = 0
    '                    Else

    '                        If CDbl(row.Values(7)) > CDbl(row.Values(6)) Then
    '                            tBalance.TotalBalance = CDbl(row.Values(7)) - CDbl(row.Values(6))
    '                        ElseIf CDbl(row.Values(6)) > CDbl(row.Values(7)) Then
    '                            tBalance.TotalBalance = CDbl(row.Values(6)) - CDbl(row.Values(7))
    '                        ElseIf CDbl(row.Values(6)) = CDbl(row.Values(7)) Or CDbl(row.Values(7)) = CDbl(row.Values(6)) Then
    '                            tBalance.TotalBalance = 0
    '                        End If
    '                    End If
    '                End If
    '                fAccountBalances.Add(tBalance)
    '            End If
    '        Next

    '        Return fAccountBalances

    '    End Get
    'End Property

    <Action(Caption:="����§ҹ", ConfirmationMessage:="��ҹ��ͧ����觢�������§ҹ��� ���������?", ImageName:="Action_Grant", AutoCommit:=True, TargetObjectsCriteria:="Status='Draft'")> _
    Public Sub MarkAsComplete()
        If Not IsDeleted Then
            If Not TrialBalanceDatas.Count > 0 Then
                MsgBox("��辺��ª����ɵáü��Ѵ���ŧ ��سҵ�Ǩ�ͺ�������ա����", MsgBoxStyle.Information, "")
                Exit Sub
            End If
            Try
                '//��С�ȵ���� webservice
                Dim objService As New RiceService.RiceService

                '//��С�� Object �ͧ��§ҹ ��.2 (Header)
                Dim objData As New RiceService.TrialBalanceHeader

                '//�����ŵ�駤�Ңͧ�ٹ��
                Dim objSiteInfo As SiteSetting = GetSiteInfo()

                '//��������Ѻ object ��§ҹ ��.2 (��������ǹ Header)
                objData.SiteNo = objSiteInfo.SiteNo
                objData.SiteName = objSiteInfo.SiteName
                objData.BalanceDate = EndDate
                objData.SentDate = Date.Now
                objData.Month = Month
                '///---f-dsfdsfdsfds 
                objData.Year = Date.Now.Year + 543
                objData.SentBy = CType(SecuritySystem.CurrentUser, User).DisplayName

                'BalanceDate, SentDate, [Year], SentBy
                'objData.SubmitDate = Date.Now

                '//����Ţͧ ��.2 (Detail) (��ª��� ��з������ �ɵá�) ������ List
                Dim listOfDetail As New List(Of RiceService.TrialBalanceDetail)

                Dim data As SelectedData = Session.ExecuteSproc("SP_GetTrialBalanceV7", New OperandValue("00000000.00000"), New OperandValue("60000000.00000"), New OperandValue(StartDate), New OperandValue(EndDate))
                For Each row As SelectStatementResultRow In data.ResultSet(0).Rows
                    If row IsNot Nothing Then
                        Dim tBalance As New TrialBalanceData(Session)
                        tBalance.AccountID = row.Values(0)
                        tBalance.AccountName = row.Values(1)
                        If tBalance.AccountID.StartsWith("1") Or tBalance.AccountID.StartsWith("5") Then
                            tBalance.TotalBalance = CDbl(row.Values(6)) - CDbl(row.Values(7))
                        Else
                            tBalance.TotalBalance = CDbl(row.Values(7)) - CDbl(row.Values(6))
                        End If

                        Dim objDetail As New RiceService.TrialBalanceDetail
                        objDetail.AccountID = row.Values(0)
                        objDetail.AccountName = row.Values(1)

                        If tBalance.AccountID.StartsWith("1") Or tBalance.AccountID.StartsWith("5") Then
                            objDetail.TotalBalance = CDbl(row.Values(6)) - CDbl(row.Values(7))
                        Else
                            objDetail.TotalBalance = CDbl(row.Values(7)) - CDbl(row.Values(6))
                        End If

                        listOfDetail.Add(objDetail)
                    End If
                Next

                '//�����ª����ɵá� (Detail) ���ǹ�ͧ Header
                '//��ͧ�ŧ List ����� Array �����觢����ż�ҹ���������� ��ͧ�� Array 
                objData.TrialBalanceDetails = listOfDetail.ToArray

                '//�觢����ż�ҹ Webservice
                Dim ret As String = objService.SubmitDataTrialBalance("NTiSecureCode", objData)

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
        Session.Delete(TrialBalanceDatas)

        Dim fAccountBalances As New BindingList(Of TrialBalanceData)
        ''fPlanfarmers = New XPCollection(Of PlanFarmer)(Session, CriteriaOperator.Parse("PlanFarmerGroup.MainPlan=?", Me.MainPlan))
        Dim data As SelectedData = Session.ExecuteSproc("SP_GetTrialBalanceV7", New OperandValue("00000000.00000"), New OperandValue("60000000.00000"), New OperandValue(StartDate), New OperandValue(EndDate))

        For Each row As SelectStatementResultRow In data.ResultSet(0).Rows
            If row IsNot Nothing Then
                Dim tBalance As New TrialBalanceData(Session)
                tBalance.AccountID = row.Values(0)
                tBalance.AccountName = row.Values(1)
                If tBalance.AccountID.StartsWith("1") Or tBalance.AccountID.StartsWith("5") Then
                    If CDbl(row.Values(6)) > CDbl(row.Values(7)) Then
                        tBalance.TotalBalance = CDbl(row.Values(6)) - CDbl(row.Values(7))
                    ElseIf CDbl(row.Values(7)) > CDbl(row.Values(6)) Then
                        tBalance.TotalBalance = CDbl(row.Values(7)) - CDbl(row.Values(6))
                    ElseIf CDbl(row.Values(6)) = CDbl(row.Values(7)) Or CDbl(row.Values(7)) = CDbl(row.Values(6)) Then
                        tBalance.TotalBalance = 0
                    End If

                    If CDbl(row.Values(7)) > CDbl(row.Values(6)) Then
                        tBalance.TotalBalance = CDbl(row.Values(7)) - CDbl(row.Values(6))
                    ElseIf CDbl(row.Values(6)) > CDbl(row.Values(7)) Then
                        tBalance.TotalBalance = CDbl(row.Values(6)) - CDbl(row.Values(7))
                    ElseIf CDbl(row.Values(6)) = CDbl(row.Values(7)) Or CDbl(row.Values(7)) = CDbl(row.Values(6)) Then
                        tBalance.TotalBalance = 0
                    End If
                End If
                TrialBalanceDatas.Add(tBalance)
            End If
            OnChanged("TrialBalanceData")
        Next
     
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
