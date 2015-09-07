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
Public Class ReserveSeedDetail ' Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
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

    Dim fReserveSeed As ReserveSeed
    <Association("ReserveSeedDetailReferencesReserveSeed")> _
    Public Property ReserveSeed() As ReserveSeed
        Get
            Return fReserveSeed
        End Get
        Set(ByVal value As ReserveSeed)
            SetPropertyValue(Of ReserveSeed)("ReserveSeed", fReserveSeed, value)
        End Set
    End Property
    Dim fSeedStatus As SeedStatus
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property SeedStatus() As SeedStatus
        Get
            Return fSeedStatus
        End Get
        Set(ByVal value As SeedStatus)
            SetPropertyValue(Of SeedStatus)("SeedStatus", fSeedStatus, value)
        End Set
    End Property
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

    Dim fSeedProduct As SeedProduct
    Public Property SeedProduct() As SeedProduct
        Get
            Return fSeedProduct
        End Get
        Set(ByVal value As SeedProduct)
            SetPropertyValue(Of SeedProduct)("SeedProduct", fSeedProduct, value)
        End Set
    End Property

    Dim fReceiveDateTime As DateTime
    Public Property ReceiveDateTime() As DateTime
        Get
            Return fReceiveDateTime
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ReceiveDateTime", fReceiveDateTime, value)
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
            If Not IsLoading AndAlso Not IsSaving Then
                fBags = fQuantity / 25
                OnChanged("Bags")
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
    Dim fSoldQuantity As Double
    Public Property SoldQuantity() As Double
        Get
            Return fSoldQuantity
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("SoldQuantity", fSoldQuantity, value)
        End Set
    End Property
    Dim fSoldBags As Integer
    Public Property SoldBags() As Integer
        Get
            Return fSoldBags
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("SoldBags", fSoldBags, value)
        End Set
    End Property
End Class
