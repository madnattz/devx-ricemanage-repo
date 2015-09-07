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
<XafDisplayName("����§ҹ �����ͧ�����͹")> _
<ConditionalAppearance.Appearance("SubmitTrialBalacneMonthReportDisableAllItems", criteria:="Status!='Draft'", Enabled:=False, TargetItems:="*", Context:="DetailView")> _
<DefaultClassOptions()> _
Public Class SubmitTrialBalacneMonthReport ' Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        ' Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).
        Me.Status = PublicEnum.SubmitReportStatus.Draft
    End Sub

    'Dim fStartDate As Date
    '<XafDisplayName("�ҡ�ѹ���")> _
    'Public Property StartDate As Date
    '    Get
    '        Return fStartDate
    '    End Get
    '    Set(ByVal value As Date)
    '        SetPropertyValue(Of Date)("StartDate", fStartDate, value)
    '    End Set
    'End Property

    'Dim fEndDate As Date
    '<XafDisplayName("�֧�ѹ���")> _
    'Public Property EndDate As Date
    '    Get
    '        Return fEndDate
    '    End Get
    '    Set(ByVal value As Date)
    '        SetPropertyValue(Of Date)("EndDate", fEndDate, value)
    '    End Set
    'End Property

    Dim fBalanceMonth As PublicEnum.EnumMonth
    <XafDisplayName("��͹")> _
    Public Property BalanceMonth As PublicEnum.EnumMonth
        Get
            Return fBalanceMonth
        End Get
        Set(ByVal value As PublicEnum.EnumMonth)
            SetPropertyValue(Of PublicEnum.EnumMonth)("BalanceMonth", fBalanceMonth, value)
        End Set
    End Property

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
    <Appearance("SubmitDateMonth", Enabled:=False, Context:="DetailView")> _
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

    <Association("SubmitTrialBalacneMonthReport-TrialBalanceMonthData", GetType(TrialBalanceMonthData))> _
   <DevExpress.Xpo.Aggregated> _
   <XafDisplayName("�����ͧ�����͹")> _
    Public ReadOnly Property TrialBalanceMonthData() As XPCollection(Of TrialBalanceMonthData)
        Get
            Return GetCollection(Of TrialBalanceMonthData)("TrialBalanceMonthData")
        End Get
    End Property

    <Action(Caption:="����§ҹ", ConfirmationMessage:="��ҹ��ͧ����觢�������§ҹ��� ���������?", ImageName:="Action_Grant", AutoCommit:=True, TargetObjectsCriteria:="Status='Draft'")> _
    Public Sub MarkAsComplete()
        If Not IsDeleted Then
            If Not TrialBalanceMonthData.Count > 0 Then
                MsgBox("��辺�����ŧ����ͧ ��سҵ�Ǩ�ͺ�������ա����", MsgBoxStyle.Information, "")
                Exit Sub
            End If
            Try
                '//��С�ȵ���� webservice
                Dim objService As New RiceService.RiceService

                '//��С�� Object �ͧ��§ҹ ��.2 (Header)
                Dim objData As New RiceService.TrialBalanceMonthHeader

                '//�����ŵ�駤�Ңͧ�ٹ��
                Dim objSiteInfo As SiteSetting = GetSiteInfo()

                '//��������Ѻ object ��§ҹ ��.2 (��������ǹ Header)
                objData.SiteNo = objSiteInfo.SiteNo
                objData.SiteName = objSiteInfo.SiteName
                objData.BalanceDate = SubmitDate
                objData.SentDate = Date.Now
                objData.BalanceMonth = BalanceMonth
                '///---f-dsfdsfdsfds 
                objData.BalanceYear = BalanceYear
                objData.SentBy = CType(SecuritySystem.CurrentUser, User).DisplayName

                'BalanceDate, SentDate, [Year], SentBy
                'objData.SubmitDate = Date.Now

                '//����Ţͧ ��.2 (Detail) (��ª��� ��з������ �ɵá�) ������ List
                Dim listOfDetail As New List(Of RiceService.TrialBalanceMonthDetail)

                For Each row As TrialBalanceMonthData In Me.TrialBalanceMonthData
                    Dim objDetail As New RiceService.TrialBalanceMonthDetail
                    objDetail.AccountID = row.AccountID
                    objDetail.AccountName = row.AccountName
                    objDetail.ForwardDrCr = row.ForwardDrCr
                    objDetail.CurrentDrCr = row.CurrentDrCr
                    objDetail.TotalDrCr = row.TotalDrCr

                    listOfDetail.Add(objDetail)
                Next

                'Dim data As SelectedData = Session.ExecuteSproc("SP_GetTrialBalanceV7", New OperandValue("00000000.00000"), New OperandValue("60000000.00000"), New OperandValue(StartDate), New OperandValue(EndDate))
                'For Each row As SelectStatementResultRow In data.ResultSet(0).Rows
                '    If row IsNot Nothing Then
                '        Dim tBalance As New TrialBalanceMonthData(Session)
                '        tBalance.AccountID = row.Values(0)
                '        tBalance.AccountName = row.Values(1)
                '        If row.Values(2) <> 0 Then
                '            tBalance.TotalDr = row.Values(2)
                '        Else
                '            tBalance.TotalDr = row.Values(3)
                '            'End If
                '        End If
                '        If row.Values(6) <> 0 Then
                '            tBalance.TotalCr = row.Values(6)
                '        Else
                '            tBalance.TotalCr = row.Values(7)
                '        End If
                '        '        TrialBalanceMonthData.Add(tBalance)
                '        '    End If
                '        OnChanged("TrialBalanceMonthData")
                '        'Next

                'Dim objDetail As New RiceService.TrialBalanceMonthDetail
                'objDetail.AccountID = row.Values(0)
                'objDetail.AccountName = row.Values(1)

                '        If row.Values(2) <> 0 Then
                '            tBalance.TotalDr = row.Values(2)
                '        Else
                '            tBalance.TotalDr = row.Values(3)
                '            'End If
                '        End If
                '        If row.Values(6) <> 0 Then
                '            tBalance.TotalCr = row.Values(6)
                '        Else
                '            tBalance.TotalCr = row.Values(7)
                '        End If
                '        '        TrialBalanceMonthData.Add(tBalance)
                '        '    End If
                '        OnChanged("TrialBalanceMonthDetail")
                '        'Next

                '        listOfDetail.Add(objDetail)
                '    End If
                'Next

                '//�����ª����ɵá� (Detail) ���ǹ�ͧ Header
                '//��ͧ�ŧ List ����� Array �����觢����ż�ҹ���������� ��ͧ�� Array 
                objData.TrialBalanceMonthDetail = listOfDetail.ToArray

                '//�觢����ż�ҹ Webservice
                Dim ret As String = objService.SubmitDataTrialBalanceMonth("NTiSecureCode", objData)

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
        Session.Delete(TrialBalanceMonthData)
        ' Dim fAccountBalances As New BindingList(Of TrialBalanceMonthData)
        ''fPlanfarmers = New XPCollection(Of PlanFarmer)(Session, CriteriaOperator.Parse("PlanFarmerGroup.MainPlan=?", Me.MainPlan))
        Dim daysOfMonth As Integer = DateTime.DaysInMonth(CInt(BalanceYear) - 543, (BalanceMonth))

        Dim sDate As Date = New Date(CInt(BalanceYear) - 543, CUInt(BalanceMonth()), 1)
        Dim eDate As Date = New Date(CInt(BalanceYear) - 543, CInt(BalanceMonth()), daysOfMonth)

        Dim data As SelectedData = Session.ExecuteSproc("SP_GetTrialBalanceV7", New OperandValue("00000000.00000"), New OperandValue("60000000.00000"), New OperandValue(sDate), New OperandValue(eDate))

        For Each row As SelectStatementResultRow In data.ResultSet(0).Rows
            If row IsNot Nothing Then
                Dim tBalance As New TrialBalanceMonthData(Session)
                tBalance.AccountID = row.Values(0)
                tBalance.AccountName = row.Values(1)
                If tBalance.AccountID.StartsWith("1") Or tBalance.AccountID.StartsWith("5") Then
                    If CDbl(row.Values(2)) > CDbl(row.Values(3)) Then
                        tBalance.ForwardDrCr = CDbl(row.Values(2)) - CDbl(row.Values(3))
                    ElseIf CDbl(row.Values(3)) > CDbl(row.Values(2)) Then
                        tBalance.ForwardDrCr = CDbl(row.Values(3)) - CDbl(row.Values(2))
                    ElseIf CDbl(row.Values(2)) = CDbl(row.Values(3)) Or CDbl(row.Values(3)) = CDbl(row.Values(2)) Then
                        tBalance.ForwardDrCr = 0
                    Else
                        If CDbl(row.Values(2)) > CDbl(row.Values(3)) Then
                            tBalance.ForwardDrCr = CDbl(row.Values(2)) - CDbl(row.Values(3))
                        ElseIf CDbl(row.Values(3)) > CDbl(row.Values(2)) Then
                            tBalance.ForwardDrCr = CDbl(row.Values(3)) - CDbl(row.Values(2))
                        ElseIf CDbl(row.Values(2)) = CDbl(row.Values(3)) Or CDbl(row.Values(3)) = CDbl(row.Values(2)) Then
                            tBalance.ForwardDrCr = 0
                        End If
                    End If
                End If
                '====================================================================
                If tBalance.AccountID.StartsWith("1") Or tBalance.AccountID.StartsWith("5") Then
                    If CDbl(row.Values(4)) > CDbl(row.Values(5)) Then
                        tBalance.CurrentDrCr = CDbl(row.Values(4)) - CDbl(row.Values(5))
                    ElseIf CDbl(row.Values(5)) > CDbl(row.Values(4)) Then
                        tBalance.CurrentDrCr = CDbl(row.Values(5)) - CDbl(row.Values(4))
                    ElseIf CDbl(row.Values(4)) = CDbl(row.Values(5)) Or CDbl(row.Values(5)) = CDbl(row.Values(4)) Then
                        tBalance.CurrentDrCr = 0
                    Else
                        If CDbl(row.Values(4)) > CDbl(row.Values(5)) Then
                            tBalance.CurrentDrCr = CDbl(row.Values(4)) - CDbl(row.Values(5))
                        ElseIf CDbl(row.Values(5)) > CDbl(row.Values(4)) Then
                            tBalance.CurrentDrCr = CDbl(row.Values(5)) - CDbl(row.Values(4))
                        ElseIf CDbl(row.Values(4)) = CDbl(row.Values(5)) Or CDbl(row.Values(5)) = CDbl(row.Values(4)) Then
                            tBalance.CurrentDrCr = 0
                        End If
                    End If
                End If
                '=================================================================
                If tBalance.AccountID.StartsWith("1") Or tBalance.AccountID.StartsWith("5") Then
                    If CDbl(row.Values(6)) > CDbl(row.Values(7)) Then
                        tBalance.TotalDrCr = CDbl(row.Values(6)) - CDbl(row.Values(7))
                    ElseIf CDbl(row.Values(7)) > CDbl(row.Values(6)) Then
                        tBalance.TotalDrCr = CDbl(row.Values(7)) - CDbl(row.Values(6))
                    ElseIf CDbl(row.Values(6)) = CDbl(row.Values(7)) Or CDbl(row.Values(7)) = CDbl(row.Values(6)) Then
                        tBalance.TotalDrCr = 0
                    Else
                        If CDbl(row.Values(6)) > CDbl(row.Values(7)) Then
                            tBalance.TotalDrCr = CDbl(row.Values(6)) - CDbl(row.Values(7))
                        ElseIf CDbl(row.Values(7)) > CDbl(row.Values(6)) Then
                            tBalance.TotalDrCr = CDbl(row.Values(7)) - CDbl(row.Values(6))
                        ElseIf CDbl(row.Values(6)) = CDbl(row.Values(7)) Or CDbl(row.Values(7)) = CDbl(row.Values(6)) Then
                            tBalance.TotalDrCr = 0
                        End If
                    End If
                End If
                TrialBalanceMonthData.Add(tBalance)
            End If
            OnChanged("TrialBalanceMonthData")
        Next
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

    '<Action(Caption:="My UI Action", ConfirmationMessage:="Are you sure?", ImageName:="Attention", AutoCommit:=True)> _
    'Public Sub ActionMethod()
    '    ' Trigger a custom business logic for the current record in the UI (http://documentation.devexpress.com/#Xaf/CustomDocument2619).
    '    Me.PersistentProperty = "Paid"
    'End Sub
End Class
