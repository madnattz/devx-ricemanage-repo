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

<XafDisplayName("����§ҹ ��.3")> _
<ConditionalAppearance.Appearance("SubmitPlanKP3ReportDisableAllItems", criteria:="Status!='Draft'", Enabled:=False, TargetItems:="*", Context:="DetailView")> _
Public Class SubmitPlanKP3Report
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

    Dim fMainPlan As MainPlan
    <RuleRequiredField(DefaultContexts.Save)> _
    <XafDisplayName("��ҡ�ü�Ե")> _
    <ImmediatePostData()> _
    Public Property MainPlan() As MainPlan
        Get
            Return fMainPlan
        End Get
        Set(ByVal value As MainPlan)
            SetPropertyValue(Of MainPlan)("MainPlan", fMainPlan, value)
            If Not IsLoading AndAlso Not IsSaving Then
                fPlanfarmers = New XPCollection(Of PlanFarmer)(Session, CriteriaOperator.Parse("PlanFarmerGroup.MainPlan=?", value))
                OnChanged("PlanFarmers")
            End If
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

    Dim fPlanfarmers As XPCollection(Of PlanFarmer)
    <XafDisplayName("�ɵáü��Ѵ���ŧ")> _
    Public ReadOnly Property PlanFarmers() As XPCollection(Of PlanFarmer)
        Get
            If fPlanfarmers Is Nothing Then
                fPlanfarmers = New XPCollection(Of PlanFarmer)(Session, CriteriaOperator.Parse("PlanFarmerGroup.MainPlan=?", Me.MainPlan))
            End If
            Return fPlanfarmers

        End Get
    End Property

    <Action(Caption:="����§ҹ", ConfirmationMessage:="��ҹ��ͧ����觢�������§ҹ��� ���������?", ImageName:="sent2", AutoCommit:=True, TargetObjectsCriteria:="Status='Draft'")> _
    Public Sub MarkAsComplete()
        If Not IsDeleted Then
            If Not PlanFarmers.Count > 0 Then
                MsgBox("��辺��ª����ɵáü��Ѵ���ŧ ��سҵ�Ǩ�ͺ�������ա����", MsgBoxStyle.Information, "")
                Exit Sub
            End If
            Try
                '//��С�ȵ���� webservice
                Dim objService As New RiceService.RiceService

                '//��С�� Object �ͧ��§ҹ ��.3 (Header)
                Dim objData As New RiceService.KP3ReportData

                '//�����ŵ�駤�Ңͧ�ٹ��
                Dim objSiteInfo As SiteSetting = GetSiteInfo()

                '//��������Ѻ object ��§ҹ ��.3 (��������ǹ Header)
                objData.SiteNo = objSiteInfo.SiteNo
                objData.SiteName = objSiteInfo.SiteName
                objData.PlantName = MainPlan.Plant.PlantName
                objData.SeedTypeName = MainPlan.SeedType.SeedName
                objData.SeedClassName = MainPlan.SeedClass.ClassName
                objData.SeasonName = MainPlan.Season.SeasonName
                objData.SeedYear = MainPlan.SeedYear
                objData.SeedLot = MainPlan.Lot
                objData.SubmitBy = CType(SecuritySystem.CurrentUser, User).DisplayName
                objData.SubmitDate = Date.Now

                '//����Ţͧ ��.3 (Detail) (��ª��� ��з������ �ɵá�) ������ List
                Dim listOfDetail As New List(Of RiceService.KP3ReportDetail)
                For Each item As PlanFarmer In PlanFarmers
                    '//��С�ȵ���� Detail �ͧ��§ҹ ��.3
                    Dim objDetail As New RiceService.KP3ReportDetail
                    objDetail.FarmerNo = item.PlanFarmerNo
                    objDetail.FarmerName = item.FullName
                    objDetail.FarmerGroupNo = item.Farmer.FarmerGroup.GroupCode
                    objDetail.FarmerGroupName = item.Farmer.FarmerGroup.GroupName
                    objDetail.AddressNo = item.Farmer.Address.AddressNo
                    objDetail.Moo = item.Farmer.Address.Moo
                    objDetail.Tambol = item.Farmer.Address.SubDistrict.SubDistrictName
                    objDetail.Amphur = item.Farmer.Address.District.DistrictName
                    objDetail.Province = item.Farmer.Address.Province.ProvinceName

                    objDetail.GroupAddressNo = item.Farmer.FarmerGroup.Address.AddressNo
                    objDetail.GroupMoo = item.Farmer.FarmerGroup.Address.Moo
                    objDetail.GroupTambol = item.Farmer.FarmerGroup.Address.SubDistrict.SubDistrictName
                    objDetail.GroupAmphur = item.Farmer.FarmerGroup.Address.District.DistrictName
                    objDetail.GroupProvince = item.Farmer.FarmerGroup.Address.Province.ProvinceName

                    objDetail.AreaSize = ConvertToInteger(item.TotalGrowArea)
                    objDetail.SeedRecieveQuantity = ConvertToInteger(item.TotalSeedReceive)
                    objDetail.SeedUseQuantity = ConvertToInteger(item.TotalSeedUse)
                    objDetail.SeedGoalQuantity = ConvertToInteger(item.MaxBuyQuantity)

                    objDetail.GrowStartDate = item.PlanFarmerGroup.GrowStartDate
                    objDetail.GrowEndDate = item.PlanFarmerGroup.GrowEndDate

                    objDetail.CheckFarmStartDate = item.PlanFarmerGroup.CheckFarmStartDate
                    objDetail.CheckFarmEndDate = item.PlanFarmerGroup.CheckFarmEndDate

                    objDetail.HarvestStartDate = item.PlanFarmerGroup.HarvestStartDate
                    objDetail.HarvestEndDate = item.PlanFarmerGroup.HarvestEndDate

                    objDetail.BuyStartDate = item.PlanFarmerGroup.BuyStartDate
                    objDetail.BuyEndDate = item.PlanFarmerGroup.BuyEndDate

                    objDetail.ProcessStartDate = item.PlanFarmerGroup.ProcessStartDate
                    objDetail.ProcessEndDate = item.PlanFarmerGroup.ProcessEndDate

                    objDetail.MarketStartDate = item.PlanFarmerGroup.MarketStartDate
                    objDetail.MarketEndDate = item.PlanFarmerGroup.MarketEndDate

                    objDetail.IsMaxBuyFarmer = item.IsMaximumFarmer

                    'objDetail.SeedSource = item.FullSeedLotName
                    listOfDetail.Add(objDetail)
                Next

                '//�����ª����ɵá� (Detail) ���ǹ�ͧ Header
                '//��ͧ�ŧ List ����� Array �����觢����ż�ҹ���������� ��ͧ�� Array 
                objData.KP3ReportDetails = listOfDetail.ToArray

                '//�觢����ż�ҹ Webservice
                Dim ret As String = objService.SubmitDataKp3Report("NTiSecureCode", objData)

                '//��ʶҹ��� �觢��������� (Sent)
                If ret = "Success" Then
                    Me.Status = PublicEnum.SubmitReportStatus.Sent
                    Me.SubmitDate = Date.Now
                    Me.SubmitBy = CType(SecuritySystem.CurrentUser, User).DisplayName
                    '//�ѹ�֡������
                    MsgBox("�觢�������§ҹ ���º��������", MsgBoxStyle.Information)
                    Me.Save()
                    Session.CommitTransaction()
                Else
                    MsgBox("�Դ��ͼԴ��Ҵ �������ö����§ҹ�� ��سҵԴ��ͼ������к�", MsgBoxStyle.Critical)
                End If

            Catch ex As Exception
                MsgBox("�Դ��ͼԴ��Ҵ �������ö����§ҹ�� ��سҵԴ��ͼ������к�", MsgBoxStyle.Critical)
            End Try

        End If
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
