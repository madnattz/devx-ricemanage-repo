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

<ImageName("sent3")> _
<XafDisplayName("����§ҹ ����ѵԤس�Ҿ���紾ѹ��� 2")> _
<DefaultClassOptions()> _
<ConditionalAppearance.Appearance("SubmitQualityHistoryReportDisableAllItems", criteria:="Status!='Draft'", Enabled:=False, TargetItems:="*", Context:="DetailView")> _
Public Class SubmitQualityHistory
    Inherits BaseObject
    Implements ISubmitReportAble

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        Me.Status = PublicEnum.SubmitReportStatus.Draft
    End Sub

    <Association("SubmitQualityHistoryReport-SubmitQualityHistoryReportDetails", GetType(SubmitQualityHistoryDetail))> _
    <DevExpress.Xpo.Aggregated> _
    <XafDisplayName("�ɵáü��Ѵ���ŧ")> _
    Public ReadOnly Property SubmitQualityHistoryReportDetails() As XPCollection(Of SubmitQualityHistoryDetail)
        Get
            Return GetCollection(Of SubmitQualityHistoryDetail)("SubmitQualityHistoryReportDetails")
        End Get
    End Property

    Dim fSeason As Season
    <RuleRequiredField(DefaultContexts.Save)> _
    <XafDisplayName("Ĵ�")> _
    <ImmediatePostData()> _
    Public Property Season() As Season
        Get
            Return fSeason
        End Get
        Set(ByVal value As Season)
            SetPropertyValue(Of Season)("Season", fSeason, value)
        End Set
    End Property

    Dim fSeedYear As String
    <RuleRequiredField(DefaultContexts.Save)> _
    <XafDisplayName("��")> _
    <ImmediatePostData()> _
    <ModelDefault("PropertyEditorType", "RiceManagementApp.Module.Win.CustomYearDropdown")> _
    Public Property SeedYear() As String
        Get
            Return fSeedYear
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SeedYear", fSeedYear, value)
        End Set
    End Property

    Dim fSubmitDate As Date
    <XafDisplayName("�ѹ�������§ҹ")> _
    <Appearance("SubmitDate", Enabled:=False)> _
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
    <Appearance("SubmitBy", Enabled:=False)> _
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

    Private Sub GetSeedProduct(listSeedProduct As XPCollection(Of SeedProduct))
        Try
            For Each item As SeedProduct In listSeedProduct
                Dim objItem As SubmitQualityHistoryDetail = Session.FindObject(Of SubmitQualityHistoryDetail)(CriteriaOperator.Parse("ProductName = ? and SubmitQualityHistoryReport=?", item.ProductName, Me))
                If objItem Is Nothing Then
                    objItem = New SubmitQualityHistoryDetail(Session)
                    objItem.SubmitQualityHistoryReport = Me
                    objItem.ProductName = item.ProductName
                    objItem.SeedClass = item.SeedClass.ClassName
                    objItem.LotNo = item.LotNo
                    objItem.ForBuyWetSeed = item.QaulityHistory.BuyWet
                    objItem.ForBuyGrowSeed = item.QaulityHistory.BuyGrow
                    objItem.ForBuyRedSeed = item.QaulityHistory.BuyRedSeed
                    objItem.ForBuyOtherSeed = item.QaulityHistory.BuyOtherSeed
                    objItem.ForBuyCheckDate = item.QaulityHistory.BuyLabDate
                    objItem.BeforeWetSeed = item.QaulityHistory.BeforeWet
                    objItem.BeforeGrowSeed = item.QaulityHistory.BeforeGrow
                    objItem.BeforeCompound = item.QaulityHistory.BeforeCompound
                    objItem.BeforeDate = item.QaulityHistory.BeforeLabDate
                    objItem.AfterWetSeed = item.QaulityHistory.AfterWet
                    objItem.AfterGrowSeed = item.QaulityHistory.AfterGrow
                    objItem.AfterStrongSeed = item.QaulityHistory.AfterStrong
                    objItem.AfterPureSeed = item.QaulityHistory.AfterStrong
                    objItem.AfterPureSeed = item.QaulityHistory.AfterPureSeed
                    objItem.AfterCompound = item.QaulityHistory.AfterCompound
                    objItem.AfterRedSeed = item.QaulityHistory.AfterRedSeed
                    objItem.AfterOtherSeed = item.QaulityHistory.AfterOtherSeed
                    objItem.AfterWeight = item.QaulityHistory.AfterSeedWeight
                    objItem.AfterWeight = item.QaulityHistory.AfterSeedWeight
                End If
            Next
            Session.CommitTransaction()
        Catch ex As Exception
            Session.RollbackTransaction()
        End Try
    End Sub

    '<Action(Caption:="�֧������", ConfirmationMessage:="��ҹ��ͧ��ô֧������ ���������?", ImageName:="BO_Note", AutoCommit:=True, TargetObjectsCriteria:="Status='Draft'")> _
    Public Sub ImportData()
        'Session.Delete(SubmitQualityHistoryReportDetails)
        Dim _SeedProducts = New XPCollection(Of SeedProduct)(Session, CriteriaOperator.Parse("Season = ? and SeedYear = ? and SeedStatus.SeedStatusName like '%��%' ", Me.Season, Me.SeedYear))
        If _SeedProducts.Count > 0 Then
            GetSeedProduct(_SeedProducts)
            OnChanged("SubmitQualityHistoryReportDetails")
        End If
    End Sub


    '  <Action(Caption:="����§ҹ", ConfirmationMessage:="��ҹ��ͧ����觢�������§ҹ��� ���������?", ImageName:="sent2", AutoCommit:=True, TargetObjectsCriteria:="Status='Draft'")> _
    Public Function MarkAsComplete() As Boolean
        If Not IsDeleted Then
            If Not SubmitQualityHistoryReportDetails.Count > 0 Then
                MsgBox("��辺�����Ż���ѵԤس�Ҿ ��سҵ�Ǩ�ͺ�������ա����", MsgBoxStyle.Information, "")
                Exit Function
            End If
            Try
                '//��С�ȵ���� webservice
                Dim objService As New RiceService.RiceService

                '//��С�� Object �ͧ��§ҹ ����ѵԤس�Ҿ���紾ѹ��� (Header)
                Dim objData As New List(Of RiceService.QualityHistoryHeader)

                '//�����ŵ�駤�Ңͧ�ٹ��
                Dim objSiteInfo As SiteSetting = GetSiteInfo()
                '//��С�ȵ���� Detail �ͧ��§ҹ ����ѵԤس�Ҿ���紾ѹ���
                Dim objHeader As New RiceService.QualityHistoryHeader
                '//��������Ѻ object ��§ҹ ����ѵԤس�Ҿ���紾ѹ���(��������ǹ Header)
                objHeader.SiteNo = objSiteInfo.SiteNo
                objHeader.SiteName = objSiteInfo.SiteName
                objHeader.SubmitBy = CType(SecuritySystem.CurrentUser, User).DisplayName
                objHeader.SubmitDate = Date.Now

                '//����Ţͧ ����ѵԤس�Ҿ���紾ѹ��� (Detail) (�������, �����͡) ������ List

                Dim listOfDetail As New List(Of RiceService.QualityHistoryDetail)
                For Each item As SubmitQualityHistoryDetail In SubmitQualityHistoryReportDetails
                    Dim objDetail As New RiceService.QualityHistoryDetail

                    objDetail.ForBuyWetSeed = item.ForBuyWetSeed
                    objDetail.ForBuyGrowSeed = item.ForBuyGrowSeed
                    objDetail.ForBuyRedSeed = item.ForBuyRedSeed
                    objDetail.ForBuyOtherSeed = item.ForBuyOtherSeed
                    objDetail.ForBuyCheckDate = item.ForBuyCheckDate
                    objDetail.BeforeWetSeed = item.BeforeWetSeed
                    objDetail.BeforeGrowSeed = item.BeforeGrowSeed
                    objDetail.BeforeCompound = item.BeforeCompound
                    objDetail.BeforeDate = item.BeforeDate
                    objDetail.AfterWetSeed = item.AfterWetSeed
                    objDetail.AfterGrowSeed = item.AfterGrowSeed
                    objDetail.AfterStrongSeed = item.AfterStrongSeed
                    objDetail.AfterPureSeed = item.AfterPureSeed
                    objDetail.AfterCompound = item.AfterCompound
                    objDetail.AfterRedSeed = item.AfterRedSeed
                    objDetail.AfterOtherSeed = item.AfterOtherSeed
                    objDetail.AfterWeight = item.AfterWeight
                    objDetail.AfterDate = item.AfterDate

                    listOfDetail.Add(objDetail)
                    objHeader.QualityHistoryDetails = listOfDetail.ToArray

                    objData.Add(objHeader)
                Next

                '//�觢����ż�ҹ Webservice
                Dim ret As String = objService.SubmitDataQualityHistoryReport("NTiSecureCode", objData.ToArray)

                '//��ʶҹ��� �觢��������� (Sent)
                If ret = "Success" Then
                    Me.Status = PublicEnum.SubmitReportStatus.Sent
                    Me.SubmitDate = Date.Now
                    Me.SubmitBy = CType(SecuritySystem.CurrentUser, User).DisplayName
                    Return True
                    Me.Save()
                Else
                    Return False
                End If

            Catch ex As Exception
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
        ImportData()
    End Sub

    Public Function DoSubmitReport() As Boolean Implements ISubmitReportAble.DoSubmitReport
        Return MarkAsComplete()
    End Function
End Class