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

<XafDisplayName("����§ҹ �ԡ�����ɵá�")> _
<ConditionalAppearance.Appearance("SubmitPayDateReportDisableAllItems", criteria:="Status!='Draft'", Enabled:=False, TargetItems:="*", Context:="DetailView")> _
<DefaultClassOptions()> _
Public Class SubmitPayDateReport
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

    Private _SeasonID As Season
    <XafDisplayName("Ĵ�")> _
    <ImmediatePostData()> _
    Public Property Season() As Season
        Get
            Return _SeasonID
        End Get
        Set(ByVal value As Season)
            SetPropertyValue("SeasonID", _SeasonID, value)
            OnChanged("PayDateDetails")
        End Set
    End Property

    Private _SeedYear As String
    <XafDisplayName("��")> _
    <ImmediatePostData()> _
    Public Property SeedYear() As String
        Get
            Return _SeedYear
        End Get
        Set(ByVal value As String)
            SetPropertyValue("SeedYear", _SeedYear, value)
            OnChanged("PayDateDetails")
        End Set
    End Property

    Dim fSeedMonth As PublicEnum.EnumMonth
    <XafDisplayName("��͹")> _
    Public Property SeedMonth As PublicEnum.EnumMonth
        Get
            Return fSeedMonth
        End Get
        Set(ByVal value As PublicEnum.EnumMonth)
            SetPropertyValue(Of PublicEnum.EnumMonth)("SeedMonth", fSeedMonth, value)
        End Set
    End Property

    Private _FiscalYear As String
    <XafDisplayName("�է�����ҳ")> _
    <ImmediatePostData()> _
    Public Property FiscalYear() As String
        Get
            Return _FiscalYear
        End Get
        Set(ByVal value As String)
            SetPropertyValue("FiscalYear", _FiscalYear, value)
        End Set
    End Property

    Dim fSubmitDate As Date = Today
    <Appearance("SubmitDatePay", Enabled:=False, Context:="DetailView")> _
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

    <Association("SubmitPayDateReport-PayDateData", GetType(PayDateData))> _
<DevExpress.Xpo.Aggregated> _
<XafDisplayName("�������ԡ�����ɵá�")> _
    Public ReadOnly Property PayDateDatas() As XPCollection(Of PayDateData)
        Get
            Return GetCollection(Of PayDateData)("PayDateDatas")
        End Get
    End Property


    'Dim fPayDateDetails As XPCollection(Of PayDateDetail)
    'Public ReadOnly Property PayDateDetails() As XPCollection(Of PayDateDetail)
    '    Get
    '        If fPayDateDetails Is Nothing Then
    '            fPayDateDetails = New XPCollection(Of PayDateDetail)(Session, CriteriaOperator.Parse("No.SeasonID=? and No.YearID=?", Season, SeedYear))
    '        End If
    '        Return fPayDateDetails
    '    End Get
    'End Property

    <Action(Caption:="����§ҹ", ConfirmationMessage:="��ҹ��ͧ����觢�������§ҹ��� ���������?", ImageName:="Action_Grant", AutoCommit:=True, TargetObjectsCriteria:="Status='Draft'")> _
    Public Sub MarkAsComplete()
        If Not IsDeleted Then
            If Not PayDateDatas.Count > 0 Then
                MsgBox("��辺��¡���ԡ�����ɵá� ��سҵ�Ǩ�ͺ�������ա����", MsgBoxStyle.Information, "")
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
            Dim objData As New RiceService.PayDateHeader
            Try
                '//�����ŵ�駤�Ңͧ�ٹ��
                Dim objSiteInfo As SiteSetting = GetSiteInfo()

                '//��������Ѻ object ��§ҹ ��.2 (��������ǹ Header)
                objData.SiteNo = objSiteInfo.SiteNo
                objData.SiteName = objSiteInfo.SiteName
                objData.SentDate = Date.Now
                objData.SeasonName = Season.SeasonName
                objData.SeedMonth = SeedMonth
                objData.SeedYear = SeedYear
                objData.FiscalYear = FiscalYear
                objData.SentBy = CType(SecuritySystem.CurrentUser, User).DisplayName

                'Catch ex As Exception

                'End Try


                'BalanceDate, SentDate, [Year], SentBy
                'objData.SubmitDate = Date.Now

                '//����Ţͧ ��.2 (Detail) (��ª��� ��з������ �ɵá�) ������ List
                Dim listOfDetail As New List(Of RiceService.PayDateDetail)
                For Each item As PayDateData In PayDateDatas
                    Dim objDetail As New RiceService.PayDateDetail
                    objDetail.SeedName = item.SeedName.SeedName
                    objDetail.SeedClass = item.SeedClass.ClassName
                    objDetail.BatchNo = item.BatchNo
                    objDetail.RefNo = item.RefNo
                    objDetail.WeightDate = item.WeightDate
                    objDetail.RefDate = item.RefDate
                    objDetail.CenterPayDate = item.CenterPayDate
                    objDetail.PayDate = item.FamerPayDate
                    objDetail.TotalDays = item.DateCount
                    objDetail.TotalFamer = item.TotalFamer
                    objDetail.TotalWeight = item.TotalWeight
                    objDetail.TotalAmount = item.TotalAmount

                    listOfDetail.Add(objDetail)
                Next

                '//�����ª����ɵá� (Detail) ���ǹ�ͧ Header
                '//��ͧ�ŧ List ����� Array �����觢����ż�ҹ���������� ��ͧ�� Array 
                objData.PayDateDetails = listOfDetail.ToArray


                '//�觢����ż�ҹ Webservice
                Dim ret As String = objService.SubmitDataPayDate("NTiSecureCode", objData)

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
        Session.Delete(PayDateDatas)
        Dim _PayDateDatas = New XPCollection(Of PayDateDetail)(Session, CriteriaOperator.Parse("No.SeasonID=? and No.YearID=?", Season, SeedYear))
        If _PayDateDatas.Count > 0 Then
            For Each TblPayDateDetail As PayDateDetail In _PayDateDatas
                Dim InsertPayDateData As New PayDateData(Session)
                With InsertPayDateData
                    .SubmitPayDateReport = Me
                    .SeedName = TblPayDateDetail.SeedName
                    .SeedClass = TblPayDateDetail.SeedClass
                    .BatchNo = TblPayDateDetail.BatchNo
                    .RefNo = TblPayDateDetail.RefNo
                    .WeightDate = TblPayDateDetail.StartWeightDate
                    .RefDate = TblPayDateDetail.RefDate
                    .CenterPayDate = TblPayDateDetail.CenterPayDate
                    .FamerPayDate = TblPayDateDetail.FamerPayDate
                    .DateCount = TblPayDateDetail.DateCount
                    .TotalFamer = TblPayDateDetail.TotalFamer
                    .TotalWeight = TblPayDateDetail.TotalWeight
                    .TotalAmount = TblPayDateDetail.Amount
                    .Save()
                End With

            Next
        End If
        OnChanged("PayDateDatas")
        'Else
        'End If
        'OnChanged("PayDateDatas")

    End Sub

    'Dim result As New XPCollection(Of Account)(Session, CriteriaOperator.Parse("[LevelItem]>=3 AND PublicStatus = 0 AND LocalStatus = 0"))
    '    If result.Count > 0 Then
    '        For Each TblAccount As Account In result
    'Dim InsertBringforward As New Bringforward(Session)
    '            With InsertBringforward
    '                .AccountID = TblAccount.AccountID
    '                .AccountName = TblAccount.AccountName
    '                .Account = Me
    '                .Save()
    '            End With
    '        Next
    '    End If

    'Private Sub GetEventPayDateID(listPayDateDetail As XPCollection(Of PayDateDetail))
    '    For Each item As PayDateDetail In listPayDateDetail
    '        Dim objItem As PayDateData = Session.FindObject(Of PayDateData)(CriteriaOperator.Parse("SubmitPayDateReport=?", Me))
    '        If objItem Is Nothing Then
    '            objItem = New PayDateData(Session)
    '            objItem.SubmitPayDateReport = Me
    '            objItem.TotalWeight = item.TotalWeight
    '            objItem.TotalAmount = item.Amount
    '            objItem.TotalFamer = item.TotalFamer
    '            objItem.WeightDate = item.EndWeightDate
    '            objItem.FamerPayDate = item.FamerPayDate
    '            objItem.DateCount = item.DateCount
    '            'objItem.Save()
    '        End If
    '    Next
    'End Sub

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
