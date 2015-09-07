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

'<RuleCriteria("ReceiveTransferSeedDetail.CheckValue", DefaultContexts.Save, "[Quantity] <= [AvailableProductQuantity]", "�ӹǹ��è��� ��ͧ���¡���������Ҩӹǹ����ѧ")> _
<XafDisplayName("��¡���Ѻ�͹���紾ѹ���")> _
<DefaultClassOptions()> _
<RuleCombinationOfPropertiesIsUnique("ReceiveTransferSeed,SeedProduct", CustomMessageTemplate:="����¡�����紾ѹ��������� ��س����͡���紾ѹ������� ���������¡�÷������������")> _
Public Class ReceiveTransferSeedDetail
    Inherits BaseObject
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        DataOwner = GetCurrentSite()
    End Sub

    Protected Overrides Sub OnSaving()
        If Me.DataOwner Is Nothing Then
            DataOwner = GetCurrentSite()
        End If
        MyBase.OnSaving()
    End Sub

    Dim fDataOwner As Site
    <Browsable(False)> _
    Public Property DataOwner() As Site
        Get
            Return fDataOwner
        End Get
        Set(ByVal value As Site)
            SetPropertyValue(Of Site)("DataOwner", fDataOwner, value)
        End Set
    End Property

    Public Function GetCurrentSite() As Site
        Dim siteSetting As SiteSetting = Session.FindObject(Of SiteSetting)(Nothing)
        If siteSetting IsNot Nothing Then
            If siteSetting.Site IsNot Nothing Then
                Return siteSetting.Site
            Else
                Return Nothing
            End If
            Return siteSetting.Site
        Else
            Return Nothing
        End If
    End Function

    Dim fReceiveTransferSeed As ReceiveTransferSeed
    <Association("ReceiveTransferSeed-ReceiveTransferSeedDetails")> _
    Public Property ReceiveTransferSeed() As ReceiveTransferSeed
        Get
            Return fReceiveTransferSeed
        End Get
        Set(ByVal value As ReceiveTransferSeed)
            SetPropertyValue(Of ReceiveTransferSeed)("ReceiveTransferSeed", fReceiveTransferSeed, value)
            If Not IsSaving And value IsNot Nothing Then
                GetTempItem(value)
            End If
        End Set
    End Property

    Public Sub GetTempItem(ReceiveHeader As ReceiveTransferSeed)
        Try
            If ReceiveHeader.ReceiveTransferSeedDetails.Count > 0 Then
                Dim objTempItem As ReceiveTransferSeedDetail = ReceiveHeader.ReceiveTransferSeedDetails(0)
                Me.Plant = objTempItem.Plant
                Me.SeedStatus = objTempItem.SeedStatus
                Me.SeedType = objTempItem.SeedType
                Me.SeedClass = objTempItem.SeedClass
                Me.Season = objTempItem.Season
                Me.SeedYear = objTempItem.SeedYear
                Me.MoneyType = objTempItem.MoneyType
            End If
        Catch ex As Exception

        End Try
    End Sub

    'Dim fSeedProductCode As String
    '<ImmediatePostData()> _
    'Public Property SeedProductCode As String
    '    Get
    '        If fSeedProduct IsNot Nothing Then
    '            Return fSeedProduct.ProductCode
    '        Else
    '            Return ""
    '        End If
    '    End Get
    '    Set(ByVal value As String)
    '        Try
    '            Dim objSeedProduct As SeedProduct = Session.FindObject(Of SeedProduct)(CriteriaOperator.Parse("ProductCode=?", value))
    '            If objSeedProduct IsNot Nothing Then
    '                SetPropertyValue(Of String)("SeedProductCode", fSeedProductCode, value)
    '                fSeedProduct = objSeedProduct
    '                'fCost = objSeedProduct
    '                OnChanged("SeedProduct")
    '            Else
    '                fSeedProduct = Nothing
    '            End If
    '        Catch ex As Exception

    '        End Try

    '    End Set
    'End Property

    Dim fPlant As Plant
    <RuleRequiredField(DefaultContexts.Save)> _
    <XafDisplayName("�ת")> _
    <DataSourceCriteria("Status='Active'")> _
    Public Property Plant() As Plant
        Get
            Return fPlant
        End Get
        Set(ByVal value As Plant)
            SetPropertyValue(Of Plant)("Plant", fPlant, value)
        End Set
    End Property

    Dim fSeedStatus As SeedStatus

    <XafDisplayName("ʶҹ����紾ѹ���")> _
    <ImmediatePostData()> _
    <RuleRequiredField(DefaultContexts.Save)> _
    <DataSourceCriteria("[Status]='Active'")> _
    Public Property SeedStatus() As SeedStatus
        Get
            Return fSeedStatus
        End Get
        Set(ByVal value As SeedStatus)
            SetPropertyValue(Of SeedStatus)("SeedStatus", fSeedStatus, value)

        End Set
    End Property

    Dim fSeedType As SeedType
    <XafDisplayName("�ѹ���")> _
    <DataSourceCriteria("[Status]='Active'")> _
    <DataSourceProperty("Plant.SeedTypes")> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property SeedType() As SeedType
        Get
            Return fSeedType
        End Get
        Set(ByVal value As SeedType)
            SetPropertyValue(Of SeedType)("SeedType", fSeedType, value)
        End Set
    End Property

    Dim fSeedClass As SeedClass
    <XafDisplayName("��鹾ѹ���")> _
    <DataSourceCriteria("[Status]='Active'")> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property SeedClass() As SeedClass
        Get
            Return fSeedClass
        End Get
        Set(ByVal value As SeedClass)
            SetPropertyValue(Of SeedClass)("SeedClass", fSeedClass, value)
        End Set
    End Property

    Dim fSeason As Season
    <XafDisplayName("Ĵ�")> _
    <DataSourceCriteria("[Status]='Active'")> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property Season() As Season
        Get
            Return fSeason
        End Get
        Set(ByVal value As Season)
            SetPropertyValue(Of Season)("Season", fSeason, value)
        End Set
    End Property
    Dim fSeedYear As String

    <XafDisplayName("��")> _
    <Size(4)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    <ModelDefault("PropertyEditorType", "RiceManagementApp.Module.Win.CustomYearDropdown")> _
    Public Property SeedYear() As String
        Get
            Return fSeedYear
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SeedYear", fSeedYear, value)
        End Set
    End Property

    Dim fMoneyType As MoneyType
    <XafDisplayName("�������Թ")> _
    <DataSourceCriteria("[Status]='Active'")> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property MoneyType() As MoneyType
        Get
            Return fMoneyType
        End Get
        Set(ByVal value As MoneyType)
            SetPropertyValue(Of MoneyType)("MoneyType", fMoneyType, value)
        End Set
    End Property

    Dim fLotNo As Integer
    <XafDisplayName("��͵")> _
    <RuleValueComparison(DefaultContexts.Save, ValueComparisonType.GreaterThan, 0)> _
    Public Property LotNo() As Integer
        Get
            Return fLotNo
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("LotNo", fLotNo, value)
        End Set
    End Property
    Dim fIsMixChemical As Boolean
    <XafDisplayName("��ء������")> _
    Public Property IsMixChemical() As Boolean
        Get
            Return fIsMixChemical
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("IsMixChemical", fIsMixChemical, value)
        End Set
    End Property

    Dim fSeedProduct As SeedProduct
    <VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    Public Property SeedProduct() As SeedProduct
        Get
            Return fSeedProduct
        End Get
        Set(ByVal value As SeedProduct)
            SetPropertyValue(Of SeedProduct)("SeedProduct", fSeedProduct, value)
        End Set
    End Property

    Dim fQuantity As Double
    <ImmediatePostData()> _
    <XafDisplayName("���˹ѡ (��.)")> _
    <RuleValueComparison(DefaultContexts.Save, ValueComparisonType.GreaterThan, 0)> _
    Public Property Quantity() As Double
        Get
            Return fQuantity
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("Quantity", fQuantity, value)
            Try
                If SeedStatus.SeedStatusName.ToString.Contains("��") Then
                    If value > 0 Then
                        fBags = Quantity / 25
                        OnChanged("Bags")
                    End If
                End If
            Catch ex As Exception

            End Try

        End Set
    End Property

    Dim fBags As Integer
    <XafDisplayName("�ӹǹ (����ͺ)")> _
    Public Property Bags() As Integer
        Get
            Return fBags
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Bags", fBags, value)
        End Set
    End Property

    Dim fCost As Double
    <XafDisplayName("�Ҥҵ鹷ع (�ҷ/��.)")> _
    Public Property Cost() As Double
        Get
            Return fCost
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("Cost", fCost, value)
        End Set
    End Property
End Class
