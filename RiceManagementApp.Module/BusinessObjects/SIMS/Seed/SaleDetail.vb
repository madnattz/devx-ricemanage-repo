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

'<ImageName("BO_Contact")> _
'<DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")> _
'<DefaultListViewOptions(MasterDetailMode.ListViewOnly, False, NewItemRowPosition.None)> _
'<Persistent("DatabaseTableName")> _
<DefaultClassOptions()> _
Public Class SaleDetail ' Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        Me.fSaleType = PublicEnum.SaleType.NormalPrice
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

    Dim fSale As Sale
    <Association("SaleDetailReferencesSale")> _
    Public Property Sale() As Sale
        Get
            Return fSale
        End Get
        Set(ByVal value As Sale)
            SetPropertyValue(Of Sale)("Sale", fSale, value)
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
                    Me.fSeedProduct = objSeedProduct
                    'fCost = objSeedProduct
                    OnChanged("SeedProduct")
                Else
                    Me.fSeedProduct = Nothing
                End If
            Catch ex As Exception

            End Try

        End Set
    End Property

    Dim fSeedProduct As SeedProduct
    '<Association("ReceiveSeedDetailReferencesPlant")> _
    <ImmediatePostData()> _
    Public Property SeedProduct() As SeedProduct
        Get
            Return fSeedProduct
        End Get
        Set(ByVal value As SeedProduct)
            SetPropertyValue(Of SeedProduct)("SeedProduct", fSeedProduct, value)
            Try
                If value IsNot Nothing Then
                    fSeedProductCode = value.ProductCode
                    fSaleType = PublicEnum.SaleType.NormalPrice
                    fDiscountPercentage = 0
                    fDiscountPrice = 0
                    fDeliveryCost = 0

                    Dim objSalePrice As SalePrice = GetProductPrice(value, Me.fSale.SaleDate)

                    If Not objSalePrice Is Nothing Then
                        fProductPrice = objSalePrice.Price
                        fSalePrice = objSalePrice.Price
                        fPriceEffectDate = objSalePrice.EffectiveDate
                    Else
                        fProductPrice = 0
                        fSalePrice = 0
                        fPriceEffectDate = Nothing
                    End If

                    OnChanged("AvailableAmount")
                    OnChanged("ProductPrice")
                    OnChanged("SalePrice")
                    OnChanged("SeedProductCode")

                Else
                    fSeedProductCode = ""
                End If
            Catch ex As Exception

            End Try
            
        End Set
    End Property

    Private fReserveSeedDetail As ReserveSeedDetail
    <Browsable(False)> _
    Public Property ReserveSeedDetail As ReserveSeedDetail
        Get
            Return fReserveSeedDetail
        End Get
        Set(value As ReserveSeedDetail)
            SetPropertyValue(Of ReserveSeedDetail)("ReserveSeedDetail", fReserveSeedDetail, value)
        End Set
    End Property

    Public ReadOnly Property AvailableAmount As Double
        Get
            If fSeedProduct IsNot Nothing Then
                Return fSeedProduct.AvailableAmount
            End If
        End Get
    End Property

    Private fPriceEffectDate As DateTime
    Public ReadOnly Property PriceEffectDate() As DateTime
        Get
            Return fPriceEffectDate
        End Get

    End Property

    Dim fReceiveDate As DateTime
    Public Property ReceiveDate() As DateTime
        Get
            Return fReceiveDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ReceiveDate", fReceiveDate, value)
        End Set
    End Property
    Dim fSaleType As PublicEnum.SaleType
    Public Property SaleType() As PublicEnum.SaleType
        Get
            Return fSaleType
        End Get
        Set(ByVal value As PublicEnum.SaleType)
            SetPropertyValue(Of PublicEnum.SaleType)("SaleType", fSaleType, value)
            If Not IsLoading AndAlso Not IsSaving Then
                Select Case value
                    Case PublicEnum.SaleType.NormalPrice
                        fDiscountPercentage = 0
                        fDiscountPrice = 0
                        fDeliveryCost = 0
                        SalePrice = ProductPrice
                    Case PublicEnum.SaleType.DiscountPrice
                        fDeliveryCost = 0
                        SalePrice = ProductPrice
                    Case PublicEnum.SaleType.ExtraPrice
                        fDiscountPercentage = 0
                        fDiscountPrice = 0
                        fDeliveryCost = 0
                    Case PublicEnum.SaleType.AuctionPrice
                        fDiscountPercentage = 0
                        fDiscountPrice = 0
                        fDeliveryCost = 0
                    Case PublicEnum.SaleType.AddDeliveryPrice
                        fDiscountPercentage = 0
                        fDiscountPrice = 0
                End Select

                'SalePrice = parseFloat(SeedPrice) - (parseFloat(SeedPrice) * (parseFloat(DiscountPercentage) / 100));
                OnChanged("DiscountPercentage")
                OnChanged("DiscountPrice")
                OnChanged("DeliveryCost")
            End If
        End Set
    End Property
    Dim fProductPrice As Double
    Public Property ProductPrice() As Double
        Get
            Return fProductPrice
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("ProductPrice", fProductPrice, value)

        End Set
    End Property
    Dim fDiscountPrice As Double
    <Browsable(False)> _
    Public Property DiscountPrice() As Double
        Get
            Return fDiscountPrice
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("DiscountPrice", fDiscountPrice, value)
        End Set
    End Property
    Dim fSalePrice As Double
    <RuleValueComparison(DefaultContexts.Save, ValueComparisonType.GreaterThan, 0)> _
    Public Property SalePrice() As Double
        Get
            Return fSalePrice
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("SalePrice", fSalePrice, value)
            If Not IsLoading AndAlso Not IsSaving Then
            End If
        End Set
    End Property
    Dim fQuantity As Double
    <RuleValueComparison(DefaultContexts.Save, ValueComparisonType.GreaterThan, 0)> _
    <ImmediatePostData()> _
    Public Property Quantity() As Double
        Get
            Return fQuantity
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("Quantity", fQuantity, value)
            If Not IsLoading AndAlso Not IsSaving Then
                fBags = fQuantity / 25
                OnChanged("Bags")
            End If
        End Set
    End Property

    '<XafDisplayName("ราคาก่อนหักส่วนลด")> _
    <VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    Public ReadOnly Property NormalAmount() As Double
        Get
            Return fQuantity * fProductPrice
        End Get
    End Property

    <XafDisplayName("ราคาก่อนหักส่วนลด")> _
    Public ReadOnly Property SaleAmount() As Double
        Get
            Return fQuantity * fSalePrice
        End Get
    End Property

    '<VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    <XafDisplayName("ส่วนลด (บาท)")> _
    Public ReadOnly Property DiscountAmount() As Double
        Get
            If SaleType = PublicEnum.SaleType.DiscountPrice Then
                Dim val As Double = 0
                val = (SaleAmount * DiscountPercentage) / 100

                Return Math.Floor(val * 100) / 100

            Else
                Return 0
            End If

        End Get
    End Property

    <XafDisplayName("ราคาหลังหักส่วนลด")> _
    Public ReadOnly Property TotalAmount() As Double
        Get
            Return SaleAmount - DiscountAmount
        End Get
    End Property

    Dim fDeliveryCost As Double
    <ImmediatePostData()> _
    Public Property DeliveryCost() As Double
        Get
            Return fDeliveryCost
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("DeliveryCost", fDeliveryCost, value)
            If Not IsLoading AndAlso Not IsSaving Then
                Dim _salePrice As Double = fProductPrice + value
                fSalePrice = _salePrice
                OnChanged("SalePrice")
            End If
        End Set
    End Property
    Dim fDiscountPercentage As Double
    <ImmediatePostData()> _
    Public Property DiscountPercentage() As Double
        Get
            Return fDiscountPercentage
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("DiscountPercentage", fDiscountPercentage, value)
            If Not IsLoading AndAlso Not IsSaving Then
                'Dim _salePrice As Double = fProductPrice - ((fProductPrice) * fDiscountPercentage / 100)
                'fSalePrice = _salePrice
                'OnChanged("SalePrice")
            End If
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
    Public ReadOnly Property IsMixed() As Boolean
        Get
            If fSeedProduct IsNot Nothing Then
                Return fSeedProduct.IsMix
            End If
        End Get
    End Property

    Public Function GetProductPrice(_seedProduct As SeedProduct, _saleDate As DateTime) As SalePrice
        Dim ret As SalePrice = Nothing
        Dim crt As String = "SeedType=? and SeedClass=? and EffectiveDate<=?"

        Dim objColSalePrice = New XPCollection(Of SalePrice)(Session, CriteriaOperator.Parse(crt, _seedProduct.SeedType, _seedProduct.SeedClass, _saleDate))
        'objSalePrice.Sorting =
        If objColSalePrice.Count > 0 Then
            objColSalePrice.Sorting.Add(New SortProperty("EffectiveDate", SortingDirection.Descending))
            ret = objColSalePrice(0)
        End If

        Return ret

    End Function

End Class
