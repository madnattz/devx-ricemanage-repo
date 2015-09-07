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

<RuleCombinationOfPropertiesIsUnique("ReceiveSeed,Plant,SeedStatus,SeedType,SeedClass,Season,SeedYear,MoneyType,LotNo", CustomMessageTemplate:="มีรายการเมล็ดพันธุ์นี้แล้ว กรุณาเลือกเมล็ดพันธุ์ใหม่ หรือแก้ไขรายการที่มีอยู่แล้ว")> _
Public Class ReceiveSeedDetail
    Inherits BaseObject

    Public Sub New(ByVal session As Session)
        MyBase.New(session)

    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        Me.fFarmSource = PublicEnum.SeedSource.Farmer
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

    Dim fReceiveSeed As ReceiveSeed
    <Association("ReceiveSeedDetailReferencesReceiveSeed")> _
    Public Property ReceiveSeed() As ReceiveSeed
        Get
            Return fReceiveSeed
        End Get
        Set(ByVal value As ReceiveSeed)
            SetPropertyValue(Of ReceiveSeed)("ReceiveSeed", fReceiveSeed, value)
            If Not IsSaving And value IsNot Nothing Then
                GetTempItem(value)
            End If
        End Set
    End Property

    Public Sub GetTempItem(ReceiveHeader As ReceiveSeed)
        Try
            If ReceiveHeader.ReceiveSeedDetails.Count > 0 Then
                Dim objTempItem As ReceiveSeedDetail = ReceiveHeader.ReceiveSeedDetails(0)
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
    ' Dim fPlant As Plant
    '<Association("ReceiveSeedDetailReferencesPlant")> _
    'Public ReadOnly Property SeedProductCode As String
    '    Get
    '        Try
    '            If SeedStatus IsNot Nothing And Plant IsNot Nothing And SeedType IsNot Nothing _
    '                And SeedClass IsNot Nothing And Season IsNot Nothing And SeedYear <> "" _
    '                And LotNo <> 0 And MoneyType IsNot Nothing Then

    '            End If
    '        Catch ex As Exception
    '            Return ""
    '        End Try

    '    End Get

    'End Property
    Dim fPlant As Plant
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property Plant() As Plant
        Get
            Return fPlant
        End Get
        Set(ByVal value As Plant)
            SetPropertyValue(Of Plant)("Plant", fPlant, value)
        End Set
    End Property

    Dim fSeedStatus As SeedStatus
    <ImmediatePostData()> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property SeedStatus() As SeedStatus
        Get
            Return fSeedStatus
        End Get
        Set(ByVal value As SeedStatus)
            SetPropertyValue(Of SeedStatus)("SeedStatus", fSeedStatus, value)
            Try
                If Not value.SeedStatusName.Contains("ดี") Then
                    fRefLot = Nothing
                    fFactoryLot = Nothing
                    OnChanged("RefLot")
                    OnChanged("FactoryLot")
                End If
            Catch ex As Exception

            End Try

        End Set
    End Property
    Dim fSeedType As SeedType
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
    <Size(4)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property SeedYear() As String
        Get
            Return fSeedYear
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SeedYear", fSeedYear, value)
        End Set
    End Property
    Dim fMoneyType As MoneyType
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
    <XafDisplayName("คลุกสารเคมี")> _
    Public Property IsMixChemical() As Boolean
        Get
            Return fIsMixChemical
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("IsMixChemical", fIsMixChemical, value)
        End Set
    End Property

    Dim fIsFinishProcess As Boolean
    <XafDisplayName("เสร็จสิ้นการปรับปรุงสภาพ")> _
    Public Property IsFinishProcess() As Boolean
        Get
            Return fIsFinishProcess
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("IsFinishProcess", fIsFinishProcess, value)
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

    Dim fSeedSource As Site
    <RuleRequiredField(DefaultContexts.Save, TargetCriteria:="SeedClass.ClassName like '%หลัก%'")> _
    Public Property SeedSource() As Site
        Get
            Return fSeedSource
        End Get
        Set(ByVal value As Site)
            SetPropertyValue(Of Site)("SeedSource", fSeedSource, value)
        End Set
    End Property
    Dim fRefLot As PickSeedDetail
    <DataSourceCriteria("PickSeed.Status = 'Approve' and StartsWith([SeedProduct.SeedStatus.SeedStatusName], 'ซื้อคืน') and SeedProduct.SeedType.Oid='@this.SeedType.Oid' and SeedProduct.SeedClass.Oid='@this.SeedClass.Oid' and SeedProduct.Season.Oid='@this.Season.Oid' and SeedProduct.MoneyType.Oid='@this.MoneyType.Oid' and SeedProduct.SeedYear='@this.SeedYear'")> _
    <RuleRequiredField(DefaultContexts.Save, TargetCriteria:="SeedStatus.SeedStatusName like '%ดี%'")> _
    Public Property RefLot() As PickSeedDetail
        Get
            Return fRefLot
        End Get
        Set(ByVal value As PickSeedDetail)
            SetPropertyValue(Of PickSeedDetail)("RefLot", fRefLot, value)
        End Set
    End Property

    Dim fFactoryLot As FactoryReturnSeedDetail
    '<RuleRequiredField(DefaultContexts.Save, TargetCriteria:="SeedStatus.SeedStatusName like '%ดี%' and ReceiveSeed.ReceiveReason='ProcessSeed'")> _
    <RuleUniqueValue(DefaultContexts.Save)> _
    <XafDisplayName("ล็อตโรงงาน")> _
    <ImmediatePostData()> _
    Public Property FactoryLot() As FactoryReturnSeedDetail
        Get
            Return fFactoryLot
        End Get
        Set(ByVal value As FactoryReturnSeedDetail)
            SetPropertyValue(Of FactoryReturnSeedDetail)("FactoryLot", fFactoryLot, value)
            If Not IsLoading AndAlso Not IsSaving Then
                If value IsNot Nothing Then
                    Quantity = value.SeedReturn
                    Bags = value.Bag
                    IsMixChemical = value.IsMixChemical
                    IsFinishProcess = value.IsFinishProcess
                Else
                    Quantity = 0
                    Bags = 0
                    IsMixChemical = False
                    IsFinishProcess = False
                End If

                OnChanged("Quantity")
                OnChanged("Bags")
                OnChanged("IsMixChemical")
                OnChanged("IsFinishProcess")

            End If
        End Set
    End Property

    Dim fFarmSource As PublicEnum.SeedSource
    Public Property FarmSource() As PublicEnum.SeedSource
        Get
            Return fFarmSource
        End Get
        Set(ByVal value As PublicEnum.SeedSource)
            SetPropertyValue(Of PublicEnum.SeedSource)("FarmSource", fFarmSource, value)
        End Set
    End Property
    Dim fQuantity As Double
    <ImmediatePostData()> _
    <RuleValueComparison(DefaultContexts.Save, ValueComparisonType.GreaterThan, 0)> _
    Public Property Quantity() As Double
        Get
            Return fQuantity
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("Quantity", fQuantity, value)
            Try
                If fSeedStatus.SeedStatusName.ToString.Contains("ดี") Then
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
    Public Property Bags() As Integer
        Get
            Return fBags
        End Get
        Set(ByVal value As Integer)
            If fQuantity <> 0 Then

            End If
            SetPropertyValue(Of Integer)("Bags", fBags, value)
        End Set
    End Property
    'Dim fCost As Double
    'Public Property Cost() As Double
    '    Get
    '        Return fCost
    '    End Get
    '    Set(ByVal value As Double)
    '        SetPropertyValue(Of Double)("Cost", fCost, value)
    '    End Set
    'End Property

End Class
