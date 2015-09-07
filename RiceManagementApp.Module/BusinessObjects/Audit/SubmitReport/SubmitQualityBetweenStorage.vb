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


<ConditionalAppearance.Appearance("SubmitQualityBetweenStorageDisableAllItems", criteria:="Status!='Draft'", Enabled:=False, TargetItems:="*", Context:="DetailView")> _
<DefaultClassOptions> _
<XafDisplayName("����§ҹ�š�õ�Ǩ�ͺ�س�Ҿ���紾ѹ��������ҧ������ѡ��")> _
Public Class SubmitQualityBetweenStorage
    Inherits BaseObject
    Implements ISubmitReportAble

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        'Me.SubmitDate = Date.Today
        'Me.SubmitBy = CType(SecuritySystem.CurrentUser, User).DisplayName
        Me.Status = PublicEnum.SubmitReportStatus.Draft
    End Sub

    Dim _Month As PublicEnum.EnumMonth
    <XafDisplayName("��͹")> _
    <ImmediatePostData()> _
    <RuleValueComparison(DefaultContexts.Save, ValueComparisonType.NotEquals, "None")> _
    Public Property Month() As PublicEnum.EnumMonth
        Get

            Return _Month
        End Get
        Set(ByVal value As PublicEnum.EnumMonth)
            SetPropertyValue(Of PublicEnum.EnumMonth)("Month", _Month, value)
            OnChanged("QualityBetweenStorageDetails")
        End Set
    End Property

    Dim _SeedYear As String
    <XafDisplayName("��")> _
    <ImmediatePostData()> _
    <RuleRequiredField(DefaultContexts.Save)> _
    <ModelDefault("PropertyEditorType", "RiceManagementApp.Module.Win.CustomYearDropdown")> _
    Public Property SeedYear() As String
        Get
            Return _SeedYear
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SeedYear", _SeedYear, value)
            OnChanged("QualityBetweenStorageDetails")
        End Set
    End Property


    Dim fSubmitDate As Date
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

    <Association("SubmitQualityBetweenStorage-SubmitQualityBetweenStorageDetails", GetType(SubmitQualityBetweenStorageDetail))> _
    <DevExpress.Xpo.Aggregated> _
    <XafDisplayName("�����żš�õ�Ǩ�ͺ�س�Ҿ���紾ѹ��������ҧ������ѡ��")> _
    Public ReadOnly Property SubmitQualityBetweenStorageDetails() As XPCollection(Of SubmitQualityBetweenStorageDetail)
        Get
            Return GetCollection(Of SubmitQualityBetweenStorageDetail)("SubmitQualityBetweenStorageDetails")
        End Get
    End Property

    '<XafDisplayName("�����żš�õ�Ǩ�ͺ�س�Ҿ���紾ѹ��������ҧ������ѡ��")> _
    'Public ReadOnly Property QualityBetweenStorageDetails() As BindingList(Of SubmitQualityBetweenStorageDetail)
    '    Get
    '        Dim fAccountBalances As New BindingList(Of SubmitQualityBetweenStorageDetail)
    '        Dim data As SelectedData = Session.ExecuteSproc("SP_GetCheckQualityByMonthReport", New OperandValue((Int(Month))), New OperandValue(SeedYear))

    '        For Each row As SelectStatementResultRow In data.ResultSet(0).Rows
    '            If row IsNot Nothing Then
    '                Dim TmpData As New SubmitQualityBetweenStorageDetail(Session)
    '                TmpData.PlantName = row.Values(1)
    '                TmpData.SeedTypeName = row.Values(2)
    '                TmpData.SeedClassName = row.Values(4)
    '                TmpData.SeasonName = row.Values(5)
    '                TmpData.SeedYear = row.Values(11)
    '                TmpData.SeedLot = row.Values(12)
    '                TmpData.BuyDate = row.Values(18)
    '                TmpData.DiffMonthBuy = row.Values(20)
    '                TmpData.ProcessDate = row.Values(19)
    '                TmpData.DiffProcess = row.Values(21)
    '                TmpData.Wet = row.Values(13)
    '                TmpData.Grow = row.Values(14)
    '                TmpData.Strong = row.Values(15)
    '                TmpData.SeedWeight = row.Values(16)
    '                TmpData.LabDate = row.Values(17)
    '                TmpData.Remark = row.Values(15)
    '                fAccountBalances.Add(TmpData)
    '            End If
    '        Next

    '        Return fAccountBalances

    '    End Get
    'End Property

    '<Action(Caption:="����§ҹ", ConfirmationMessage:="��ҹ��ͧ����觢�������§ҹ��� ���������?", ImageName:="sent2", AutoCommit:=True, TargetObjectsCriteria:="Status='Draft'")> _
    Public Function MarkAsComplete() As Boolean
        If Not IsDeleted Then
            If Not SubmitQualityBetweenStorageDetails.Count > 0 Then
                MsgBox("��辺��¡�� ��سҵ�Ǩ�ͺ�������ա����", MsgBoxStyle.Information, "")
                Exit Function
            End If

            '//��С�ȵ���� webservice
            Dim objService As New RiceService.RiceService

            '//��С�� Object �ͧ��§ҹ ��.2 (Header)
            Dim objData As New RiceService.QualityBetweenStorageHeader
            Try

                '//�����ŵ�駤�Ңͧ�ٹ��
                Dim objSiteInfo As SiteSetting = GetSiteInfo()

                '//��������Ѻ object ��§ҹ ��.2 (��������ǹ Header)
                objData.SiteNo = objSiteInfo.SiteNo
                objData.SiteName = objSiteInfo.SiteName
                objData.SubmitDate = Now
                objData.SeasonName = Nothing
                objData.SeedYear = SeedYear
                objData.SubmitBy = CType(SecuritySystem.CurrentUser, User).DisplayName

                '//ǹ�Ң������ Collection
                Dim listOfDetail As New List(Of RiceService.QualityBetweenStorageDetail)
                For Each row As SubmitQualityBetweenStorageDetail In SubmitQualityBetweenStorageDetails
                    If row IsNot Nothing Then
                        Dim objDetail As New RiceService.QualityBetweenStorageDetail
                        objDetail.PlantName = row.PlantName
                        objDetail.SeedTypeName = row.SeedTypeName
                        objDetail.SeedClassName = row.SeedClassName
                        objDetail.SeasonName = row.SeasonName
                        objDetail.SeedYear = row.SeedYear
                        objDetail.SeedLot = row.SeedLot
                        objDetail.BuyDate = row.BuyDate
                        objDetail.DiffMonthBuy = row.DiffMonthBuy
                        objDetail.ProcessDate = row.ProcessDate
                        objDetail.DiffProcess = row.DiffProcess
                        objDetail.DiffProcessDate = row.ProcessDate
                        objDetail.Wet = row.Wet
                        objDetail.Grow = row.Grow
                        objDetail.Strong = row.Strong
                        objDetail.SeedWeight = row.SeedWeight
                        objDetail.LabDate = row.LabDate
                        'objDetail.Remark = row.Values(15)
                        listOfDetail.Add(objDetail)
                    End If
                Next

                '//�����ª����ɵá� (Detail) ���ǹ�ͧ Header
                '//��ͧ�ŧ List ����� Array �����觢����ż�ҹ���������� ��ͧ�� Array 
                objData.QualityBetweenStorageDetails = listOfDetail.ToArray


                '//�觢����ż�ҹ Webservice
                Dim ret As String = objService.SubmitDataQBS06Report("NTiSecureCode", objData)

                '//��ʶҹ��� �觢��������� (Sent)
                If ret = "Success" Then
                    Me.Status = PublicEnum.SubmitReportStatus.Sent
                    Me.SubmitDate = Date.Now
                    Me.SubmitBy = CType(SecuritySystem.CurrentUser, User).DisplayName
                    '//�ѹ�֡������
                    'MsgBox("�觢�������§ҹ ���º��������", MsgBoxStyle.Information)
                    Me.Save()
                    Session.CommitTransaction()
                    Return True
                Else
                    'MsgBox("�Դ��ͼԴ��Ҵ �������ö����§ҹ�� ��سҵԴ��ͼ������к�", MsgBoxStyle.Critical)
                    Return False
                End If

            Catch ex As Exception
                'MsgBox("�Դ��ͼԴ��Ҵ �������ö����§ҹ�� ��سҵԴ��ͼ������к�", MsgBoxStyle.Critical)
                Return False
            End Try

        End If
    End Function

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

    Public Sub DoLoadData() Implements ISubmitReportAble.DoLoadData
        Try
            Dim data As SelectedData = Session.ExecuteSproc("SP_GetCheckQualityByMonthReport", New OperandValue((Int(Month))), New OperandValue(SeedYear))
            For Each row As SelectStatementResultRow In data.ResultSet(0).Rows
                If row IsNot Nothing Then
                    Dim tmpData As SubmitQualityBetweenStorageDetail = Session.FindObject(Of SubmitQualityBetweenStorageDetail)(CriteriaOperator.Parse("PlantName=? and SubmitQualityBetweenStorage=?", row.Values(1), Me))
                    If tmpData Is Nothing Then
                        tmpData = New SubmitQualityBetweenStorageDetail(Session)
                        tmpData.SubmitQualityBetweenStorage = Me
                        tmpData.PlantName = row.Values(1)
                        tmpData.SeedTypeName = row.Values(2)
                        tmpData.SeedClassName = row.Values(4)
                        tmpData.SeasonName = row.Values(5)
                        tmpData.SeedYear = row.Values(11)
                        tmpData.SeedLot = row.Values(12)
                        tmpData.BuyDate = row.Values(18)
                        tmpData.DiffMonthBuy = row.Values(20)
                        tmpData.ProcessDate = row.Values(19)
                        tmpData.DiffProcess = row.Values(21)
                        tmpData.Wet = row.Values(13)
                        tmpData.Grow = row.Values(14)
                        tmpData.Strong = row.Values(15)
                        tmpData.SeedWeight = row.Values(16)
                        tmpData.LabDate = row.Values(17)
                        tmpData.Remark = row.Values(15)
                        tmpData.Save()
                    End If
                End If
            Next
            Session.CommitTransaction()
        Catch ex As Exception
            Session.RollbackTransaction()
        End Try
    End Sub

    Public Function DoSubmitReport() As Boolean Implements ISubmitReportAble.DoSubmitReport
        Return MarkAsComplete()
    End Function
End Class
