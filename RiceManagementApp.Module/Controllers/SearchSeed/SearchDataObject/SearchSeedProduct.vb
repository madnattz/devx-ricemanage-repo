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

<XafDisplayName("ค้นหา")> _
<ImageName("SearchIcon")> _
<NonPersistent> _
Public Class SearchSeedProduct
    Inherits BaseObject
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        SearchCriteria = SearchSeedProductCriteria.MoreThan
        Quantity = 0
        IsSearchBy = False
    End Sub

    Dim fPlant As Plant
    <XafDisplayName("พืช")> _
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
    <ImmediatePostData()> _
    <XafDisplayName("สถานะเมล็ดพันธุ์")> _
    Public Property SeedStatus() As SeedStatus
        Get
            Return fSeedStatus
        End Get
        Set(ByVal value As SeedStatus)
            SetPropertyValue(Of SeedStatus)("SeedStatus", fSeedStatus, value)
        End Set
    End Property
    Dim fSeedType As SeedType
    <DataSourceProperty("Plant.SeedTypes")> _
    <XafDisplayName("พันธุ์")> _
    Public Property SeedType() As SeedType
        Get
            Return fSeedType
        End Get
        Set(ByVal value As SeedType)
            SetPropertyValue(Of SeedType)("SeedType", fSeedType, value)
        End Set
    End Property
    Dim fSeedClass As SeedClass
    <XafDisplayName("ชั้นพันธุ์")> _
    Public Property SeedClass() As SeedClass
        Get
            Return fSeedClass
        End Get
        Set(ByVal value As SeedClass)
            SetPropertyValue(Of SeedClass)("SeedClass", fSeedClass, value)
        End Set
    End Property
    Dim fSeason As Season
    <XafDisplayName("ฤดู")> _
    Public Property Season() As Season
        Get
            Return fSeason
        End Get
        Set(ByVal value As Season)
            SetPropertyValue(Of Season)("Season", fSeason, value)
        End Set
    End Property
    Dim fSeedYear As String
    <XafDisplayName("ปี")> _
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
    <XafDisplayName("ประเภทเงินทุน")> _
    Public Property MoneyType() As MoneyType
        Get
            Return fMoneyType
        End Get
        Set(ByVal value As MoneyType)
            SetPropertyValue(Of MoneyType)("MoneyType", fMoneyType, value)
        End Set
    End Property
    Dim fLotNo As Integer
    <XafDisplayName("ล็อต")> _
    Public Property LotNo() As Integer
        Get
            Return fLotNo
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("LotNo", fLotNo, value)
        End Set
    End Property

    Dim fSearchCriteria As SearchSeedProductCriteria
    <XafDisplayName("เงือนไข")> _
    Public Property SearchCriteria() As SearchSeedProductCriteria
        Get
            Return fSearchCriteria
        End Get
        Set(ByVal value As SearchSeedProductCriteria)
            SetPropertyValue(Of SearchSeedProductCriteria)("SearchCriteria", fSearchCriteria, value)
        End Set
    End Property

    Dim fIsSearchBy As Boolean
    <XafDisplayName("ตามจำนวนที่มีอยู่")> _
    <ImmediatePostData()> _
    Public Property IsSearchBy() As Boolean
        Get
            Return fIsSearchBy
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("IsSearchBy", fIsSearchBy, value)
        End Set
    End Property

    Dim fQuantity As Integer
    <XafDisplayName("จำนวน (กก.)")> _
    Public Property Quantity() As Integer
        Get
            Return fQuantity
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Quantity", fQuantity, value)
        End Set
    End Property

    Enum SearchSeedProductCriteria
        <XafDisplayName(">")>
        MoreThan = 0
        <XafDisplayName("<")>
        LessThan = 1
        <XafDisplayName("=")>
        Equals = 2
    End Enum

End Class


