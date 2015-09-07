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

'<ImageName("BO_Contact")> _
'<DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")> _
'<DefaultListViewOptions(MasterDetailMode.ListViewOnly, False, NewItemRowPosition.None)> _
'<Persistent("DatabaseTableName")> _
<DefaultClassOptions()> _
<RuleCombinationOfPropertiesIsUnique("PickSeed,SeedProduct", CustomMessageTemplate:="มีรายการเมล็ดพันธุ์นี้แล้ว กรุณาเลือกเมล็ดพันธุ์ใหม่ หรือแก้ไขรายการที่มีอยู่แล้ว")> _
<RuleCriteria("FactoryPickSeedCheckValue", DefaultContexts.Save, "[Quantity] <= [AvailableProductQuantity]", "จำนวนการจ่าย ต้องน้อยกว่าหรือเท่ากับจำนวนคงคลัง")> _
Public Class FactoryPickSeedDetail ' Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        DataOwner = GetCurrentSite()
    End Sub

    Protected Overrides Sub OnSaving()
        MyBase.OnSaving()
        If Me.DataOwner Is Nothing Then
            DataOwner = GetCurrentSite()
        End If
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

    Dim fPickSeed As FactoryPickSeed
    <Association("FactoryPickSeed-FactoryPickSeedDetails")> _
    Public Property PickSeed() As FactoryPickSeed
        Get
            Return fPickSeed
        End Get
        Set(ByVal value As FactoryPickSeed)
            SetPropertyValue(Of FactoryPickSeed)("PickSeed", fPickSeed, value)
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
    <RuleRequiredField(DefaultContexts.Save)> _
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

    'Dim fPlant As Plant
    'Public Property Plant() As Plant
    '    Get
    '        Return fPlant
    '    End Get
    '    Set(ByVal value As Plant)
    '        SetPropertyValue(Of Plant)("Plant", fPlant, value)
    '    End Set
    'End Property
    'Dim fSeedStatus As SeedStatus
    'Public Property SeedStatus() As SeedStatus
    '    Get
    '        Return fSeedStatus
    '    End Get
    '    Set(ByVal value As SeedStatus)
    '        SetPropertyValue(Of SeedStatus)("SeedStatus", fSeedStatus, value)
    '    End Set
    'End Property
    'Dim fSeedType As SeedType
    'Public Property SeedType() As SeedType
    '    Get
    '        Return fSeedType
    '    End Get
    '    Set(ByVal value As SeedType)
    '        SetPropertyValue(Of SeedType)("SeedType", fSeedType, value)
    '    End Set
    'End Property
    'Dim fSeedClass As SeedClass
    'Public Property SeedClass() As SeedClass
    '    Get
    '        Return fSeedClass
    '    End Get
    '    Set(ByVal value As SeedClass)
    '        SetPropertyValue(Of SeedClass)("SeedClass", fSeedClass, value)
    '    End Set
    'End Property
    'Dim fSeason As Season
    'Public Property Season() As Season
    '    Get
    '        Return fSeason
    '    End Get
    '    Set(ByVal value As Season)
    '        SetPropertyValue(Of Season)("Season", fSeason, value)
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
    'Dim fMoneyType As MoneyType
    'Public Property MoneyType() As MoneyType
    '    Get
    '        Return fMoneyType
    '    End Get
    '    Set(ByVal value As MoneyType)
    '        SetPropertyValue(Of MoneyType)("MoneyType", fMoneyType, value)
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
    '<RuleValueComparison("PickSeedDetail.Quantity", DefaultContexts.Save, ValueComparisonType.LessThanOrEqual, "AvailableProductQuantity")> _
    <ImmediatePostData()> _
    <RuleValueComparison(DefaultContexts.Save, ValueComparisonType.GreaterThan, 0)> _
    Public Property Quantity() As Double
        Get
            Return fQuantity
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("Quantity", fQuantity, value)
            'If value > 0 Then
            '    fBags = Quantity / 100
            '    OnChanged("Bags")
            'End If
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
    <Browsable(False)> _
    Public ReadOnly Property Cost() As Double
        Get
            Return fCost
        End Get
        'Set(ByVal value As Double)
        '    SetPropertyValue(Of Double)("Cost", fCost, value)
        'End Set
    End Property
End Class
