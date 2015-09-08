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

<XafDisplayName("����§ҹ �鹷ع���紾ѹ���")> _
<ConditionalAppearance.Appearance("SubmitAccCostReportDisableAllItems", criteria:="Status!='Draft'", Enabled:=False, TargetItems:="*", Context:="DetailView")> _
<DefaultClassOptions()> _
Public Class SubmitAccCostReport
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

    Private _SeedFiscalYear As String
    <XafDisplayName("�է�����ҳ")> _
    <ImmediatePostData()> _
    Public Property SeedFiscalYear() As String
        Get
            Return _SeedFiscalYear
        End Get
        Set(ByVal value As String)
            SetPropertyValue("SeedFiscalYear", _SeedFiscalYear, value)
            OnChanged("PayDateDetails")
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
    <Appearance("SubmitDateAcc", Enabled:=False, Context:="DetailView")> _
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

    <Association("SubmitAccCostReport-AccCostDatas", GetType(AccCostData))> _
<DevExpress.Xpo.Aggregated> _
    <XafDisplayName("�����ŵ鹷ع���紾ѹ���")> _
    Public ReadOnly Property AccCostDatas() As XPCollection(Of AccCostData)
        Get
            Return GetCollection(Of AccCostData)("AccCostDatas")
        End Get
    End Property


    'Dim fEventAccCostID As EventAccCostID
    'Public ReadOnly Property EventAccCostID() As EventAccCostID
    '    Get
    '        If fEventAccCostID Is Nothing Then
    '            'fAccCostDetails = New XPCollection(Of EventAccCostID)(Session, CriteriaOperator.Parse("Plant=? and SeedID=? and SeedTypeID=? and SeasonID=? and YearID=?", Plant, SeedID, SeedTypeID, Season, SeedYear))
    '            fEventAccCostID = Session.FindObject(Of EventAccCostID)(CriteriaOperator.Parse("Plant=? and SeedID=? and SeedTypeID=? and SeasonID=? and YearID=?", Plant, SeedID, SeedTypeID, Season, SeedYear))
    '        End If
    '        Return fEventAccCostID
    '    End Get
    'End Property

    '<Action(Caption:="����§ҹ", ConfirmationMessage:="��ҹ��ͧ����觢�������§ҹ��� ���������?", ImageName:="Action_Grant", AutoCommit:=True, TargetObjectsCriteria:="Status='Draft'")> _
    Public Function MarkAsComplete() As Boolean
        If Not IsDeleted Then
            If AccCostDatas Is Nothing Then
                MsgBox("��辺��¡�õ鹷ع���紾ѹ��� ��سҵ�Ǩ�ͺ�������ա����", MsgBoxStyle.Information, "")
                Exit Function
            End If

            '//��С�ȵ���� webservice
            Dim objService As New RiceService.RiceService

            '//��С�� Object �ͧ��§ҹ ��.2 (Header)
            Dim objData As New RiceService.AccCostHeader
            Try
                '//�����ŵ�駤�Ңͧ�ٹ��
                Dim objSiteInfo As SiteSetting = GetSiteInfo()

                '//��������Ѻ object ��§ҹ ��.2 (��������ǹ Header)
                objData.SiteNo = objSiteInfo.SiteNo
                objData.SiteName = objSiteInfo.SiteName
                objData.SentDate = Date.Now
                objData.SeasonName = Season.SeasonName
                objData.SeedMonth = Month
                objData.SeedYear = SeedYear
                objData.SeedFiscalYear = SeedFiscalYear
                objData.SentBy = CType(SecuritySystem.CurrentUser, User).DisplayName


                'Catch ex As Exception

                'End Try


                'BalanceDate, SentDate, [Year], SentBy
                'objData.SubmitDate = Date.Now
                Dim listOfDetail As New List(Of RiceService.AccCostDetail)
                '//����Ţͧ ��.2 (Detail) (��ª��� ��з������ �ɵá�) ������ List
                For Each item As AccCostData In AccCostDatas
                    Dim objDetail As New RiceService.AccCostDetail
                    objDetail.Plant = item.Plant
                    objDetail.Seed = item.SeedID
                    objDetail.SeedType = item.SeedTypeID
                    objDetail.SeasonName = item.Season
                    objDetail.SeedYear = item.SeedYear
                    objDetail.AmountSeed = item.AmountSeed
                    objDetail.AmountSeedGood = item.AmountSeedGood
                    objDetail.AmountSeedOut = item.AmountSeedOut
                    objDetail.TotalSeed = item.TotalSeed
                    objDetail.TotalMaterials = item.TotalMaterials
                    objDetail.TotalOil = item.TotalOil
                    objDetail.TotalChemical = item.TotalChemical
                    objDetail.PriceSeedGood = item.PriceSeedGood
                    objDetail.PriceSeedOutUsage = item.PriceSeedOutUsage
                    objDetail.CostPriceSeedGood = item.CostPriceSeedGood
                    objDetail.CostPriceSeedOutUsage = item.CostPriceSeedOutUsage

                    listOfDetail.Add(objDetail)

                Next

                '//�����ª����ɵá� (Detail) ���ǹ�ͧ Header
                '//��ͧ�ŧ List ����� Array �����觢����ż�ҹ���������� ��ͧ�� Array 
                objData.AccCostDetails = listOfDetail.ToArray


                '//�觢����ż�ҹ Webservice
                Dim ret As String = objService.SubmitDataAccCost("NTiSecureCode", objData)

                '//��ʶҹ��� �觢��������� (Sent)
                If ret = "Success" Then
                    Me.Status = PublicEnum.SubmitReportStatus.Sent
                    Me.SubmitDate = Date.Now
                    Me.SubmitBy = CType(SecuritySystem.CurrentUser, User).DisplayName
                    '//�ѹ�֡������
                    Me.Save()
                    Return True
                Else
                    Return False
                End If

            Catch ex As Exception
                Return False
            End Try

        End If
    End Function

    '<Action(Caption:="�֧������", ConfirmationMessage:="��ҹ��ͧ��ô֧������ ���������?", ImageName:="BO_Note", AutoCommit:=True, TargetObjectsCriteria:="Status='Draft'")> _
    Public Sub ImportData()
        Session.Delete(AccCostDatas)
        Dim _AccCostDatas = New XPCollection(Of EventAccCostID)(Session, CriteriaOperator.Parse("SeasonID=? and YearID=?", Season, SeedYear))
        If _AccCostDatas.Count > 0 Then
            For Each TblEventAccCostID As EventAccCostID In _AccCostDatas
                Dim InsertAccCostData As New AccCostData(Session)
                With InsertAccCostData
                    .SubmitAccCostReport = Me
                    .Plant = TblEventAccCostID.Plant.PlantName
                    .SeedID = TblEventAccCostID.SeedID.SeedName
                    .SeedTypeID = TblEventAccCostID.SeedTypeID.ClassName
                    .Season = TblEventAccCostID.SeasonID.SeasonName
                    .SeedYear = TblEventAccCostID.YearID
                    .AmountSeed = TblEventAccCostID.AmountSeed
                    .AmountSeedGood = TblEventAccCostID.AmountSeedGood
                    .AmountSeedOut = TblEventAccCostID.AmountSeedOut
                    .TotalSeed = TblEventAccCostID.TotalSeed
                    .TotalMaterials = TblEventAccCostID.TotalMaterials
                    .TotalOil = TblEventAccCostID.TotalOil
                    .TotalChemical = TblEventAccCostID.TotalChemical
                    .PriceSeedGood = TblEventAccCostID.PriceSS
                    .PriceSeedOutUsage = TblEventAccCostID.PriceSU
                    .CostPriceSeedGood = TblEventAccCostID.CostPriceSS
                    .CostPriceSeedOutUsage = TblEventAccCostID.CostPriceSU
                    .Save()
                End With

            Next
        End If
        OnChanged("AccCostDatas")
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


    Public Sub DoLoadData() Implements ISubmitReportAble.DoLoadData
        ImportData()
    End Sub

    Public Function DoSubmitReport() As Boolean Implements ISubmitReportAble.DoSubmitReport
        MarkAsComplete()
    End Function
End Class
