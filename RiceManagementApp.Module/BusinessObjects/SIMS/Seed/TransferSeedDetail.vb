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

<DefaultClassOptions()> _
<RuleCombinationOfPropertiesIsUnique("TransferSeed,SeedProduct", CustomMessageTemplate:="มีรายการเมล็ดพันธุ์นี้แล้ว กรุณาเลือกเมล็ดพันธุ์ใหม่ หรือแก้ไขรายการที่มีอยู่แล้ว")> _
<RuleCriteria("TransferSeedDetail.CheckValue", DefaultContexts.Save, "[Quantity] <= [AvailableProductQuantity]", "จำนวนการจ่าย ต้องน้อยกว่าหรือเท่าจำนวนคงคลัง")> _
Public Class TransferSeedDetail
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

    Dim fTransferSeed As TransferSeed
    <Association("TransferSeedDetailReferencesTransferSeed")> _
    Public Property TransferSeed() As TransferSeed
        Get
            Return fTransferSeed
        End Get
        Set(ByVal value As TransferSeed)
            SetPropertyValue(Of TransferSeed)("TransferSeed", fTransferSeed, value)
        End Set
    End Property

    Dim fSeedProductCode As String
    <ImmediatePostData()> _
    Public Property SeedProductCode As String
        Get
            If fSeedProduct IsNot Nothing Then
                Return fSeedProduct.ProductCode
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            Try
                Dim objSeedProduct As SeedProduct = Session.FindObject(Of SeedProduct)(CriteriaOperator.Parse("ProductCode=?", value))
                If objSeedProduct IsNot Nothing Then
                    SetPropertyValue(Of String)("SeedProductCode", fSeedProductCode, value)
                    fSeedProduct = objSeedProduct
                    'fCost = objSeedProduct
                    OnChanged("SeedProduct")
                Else
                    fSeedProduct = Nothing
                End If
            Catch ex As Exception

            End Try

        End Set
    End Property

    Dim fSeedProduct As SeedProduct
    '<Association("ReceiveSeedDetailReferencesPlant")> _
    <ImmediatePostData()> _
    <DataSourceCriteria("[SeedStatus.SeedStatusName] like '%ดี%' and [AvailableAmount] > 0")> _
    <RuleRequiredField(DefaultContexts.Save)> _
    <XafDisplayName("เมล็ดพันธุ์")> _
    Public Property SeedProduct() As SeedProduct
        Get
            Return fSeedProduct
        End Get
        Set(ByVal value As SeedProduct)
            SetPropertyValue(Of SeedProduct)("SeedProduct", fSeedProduct, value)
            If value IsNot Nothing Then
                fSeedProductCode = value.ProductCode
                OnChanged("SeedProductCode")
            Else
                fSeedProductCode = ""
            End If
        End Set
    End Property

    Public ReadOnly Property AvailableProductQuantity As Double
        Get
            If fSeedProduct IsNot Nothing Then
                Return fSeedProduct.AvailableAmount
            End If
        End Get
    End Property

    'Dim fPlant As Guid
    'Public Property Plant() As Guid
    '    Get
    '        Return fPlant
    '    End Get
    '    Set(ByVal value As Guid)
    '        SetPropertyValue(Of Guid)("Plant", fPlant, value)
    '    End Set
    'End Property
    'Dim fSeedStatus As Guid
    'Public Property SeedStatus() As Guid
    '    Get
    '        Return fSeedStatus
    '    End Get
    '    Set(ByVal value As Guid)
    '        SetPropertyValue(Of Guid)("SeedStatus", fSeedStatus, value)
    '    End Set
    'End Property
    'Dim fSeedType As Guid
    'Public Property SeedType() As Guid
    '    Get
    '        Return fSeedType
    '    End Get
    '    Set(ByVal value As Guid)
    '        SetPropertyValue(Of Guid)("SeedType", fSeedType, value)
    '    End Set
    'End Property
    'Dim fSeedClass As Guid
    'Public Property SeedClass() As Guid
    '    Get
    '        Return fSeedClass
    '    End Get
    '    Set(ByVal value As Guid)
    '        SetPropertyValue(Of Guid)("SeedClass", fSeedClass, value)
    '    End Set
    'End Property
    'Dim fSeason As Guid
    'Public Property Season() As Guid
    '    Get
    '        Return fSeason
    '    End Get
    '    Set(ByVal value As Guid)
    '        SetPropertyValue(Of Guid)("Season", fSeason, value)
    '    End Set
    'End Property
    'Dim fSeedYear As String
    '<Size(4)> _
    'Public Property SeedYear() As String
    '    Get
    '        Return fSeedYear
    '    End Get
    '    Set(ByVal value As String)
    '        SetPropertyValue(Of String)("SeedYear", fSeedYear, value)
    '    End Set
    'End Property
    'Dim fMoneyType As Guid
    'Public Property MoneyType() As Guid
    '    Get
    '        Return fMoneyType
    '    End Get
    '    Set(ByVal value As Guid)
    '        SetPropertyValue(Of Guid)("MoneyType", fMoneyType, value)
    '    End Set
    'End Property
    'Dim fLotNo As Integer
    'Public Property LotNo() As Integer
    '    Get
    '        Return fLotNo
    '    End Get
    '    Set(ByVal value As Integer)
    '        SetPropertyValue(Of Integer)("LotNo", fLotNo, value)
    '    End Set
    'End Property
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
                If fSeedProduct.SeedStatus.SeedStatusName.ToString.Contains("ดี") Then
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
            SetPropertyValue(Of Integer)("Bags", fBags, value)
        End Set
    End Property
    Dim fCost As Double
    Public Property Cost() As Double
        Get
            Return fCost
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("Cost", fCost, value)
        End Set
    End Property
End Class
