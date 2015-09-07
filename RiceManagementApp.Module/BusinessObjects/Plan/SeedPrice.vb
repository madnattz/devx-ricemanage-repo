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

<DefaultClassOptions()> _
Public Class SeedPrice
    Inherits BaseObject
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        CreateEstimateByGrowTypes()
        DataOwner = GetCurrentSite()
    End Sub

    Protected Overrides Sub OnSaving()
        Me.LastUodateDate = Date.Now
        Me.LastUpdateBy = CType(SecuritySystem.CurrentUser, User).DisplayName
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

    Dim fSeedType As SeedType
    <DataSourceProperty("Plant.SeedTypes")> _
    <RuleRequiredField("SeedPrice.SeedType", DefaultContexts.Save)> _
    Public Property SeedType() As SeedType
        Get
            Return fSeedType
        End Get
        Set(ByVal value As SeedType)
            SetPropertyValue(Of SeedType)("SeedType", fSeedType, value)
        End Set
    End Property
    Dim fSeedClass As SeedClass
    <RuleRequiredField("SeedPrice.SeedClass", DefaultContexts.Save)> _
    Public Property SeedClass() As SeedClass
        Get
            Return fSeedClass
        End Get
        Set(ByVal value As SeedClass)
            SetPropertyValue(Of SeedClass)("SeedClass", fSeedClass, value)
        End Set
    End Property
    Dim fSeason As Season
    <RuleRequiredField("SeedPrice.FarmerGroup", DefaultContexts.Save)> _
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
    <RuleRequiredField("SeedPrice.SeedYear", DefaultContexts.Save)> _
    Public Property SeedYear() As String
        Get
            Return fSeedYear
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SeedYear", fSeedYear, value)
        End Set
    End Property
    Dim fPrice As Double
    <ModelDefault("DisPlayFormat", ("{0:#,#0.00}"))> _
    <RuleValueComparison("SeedPrice.Price", DefaultContexts.Save, ValueComparisonType.GreaterThan, 0)> _
    Public Property Price() As Double
        Get
            Return fPrice
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("Price", fPrice, value)
        End Set
    End Property
    Dim fStatus As PublicEnum.PublicStatus
    Public Property Status() As PublicEnum.PublicStatus
        Get
            Return fStatus
        End Get
        Set(ByVal value As PublicEnum.PublicStatus)
            SetPropertyValue(Of Integer)("Status", fStatus, value)
        End Set
    End Property
    Dim fLastUpdateBy As String
    Public Property LastUpdateBy() As String
        Get
            Return fLastUpdateBy
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("LastUpdateBy", fLastUpdateBy, value)
        End Set
    End Property
    Dim fLastUodateDate As DateTime
    Public Property LastUodateDate() As DateTime
        Get
            Return fLastUodateDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("LastUodateDate", fLastUodateDate, value)
        End Set
    End Property
    Dim fPlant As Plant
    <RuleRequiredField("SeedPrice.Plant", DefaultContexts.Save)> _
    Public Property Plant() As Plant
        Get
            Return fPlant
        End Get
        Set(ByVal value As Plant)
            SetPropertyValue(Of Plant)("Plant", fPlant, value)
        End Set
    End Property

    Dim fSeedAge As String
    <RuleValueComparison("SeedPrice.SeedAge", DefaultContexts.Save, ValueComparisonType.GreaterThan, 0)> _
    Public Property SeedAge() As Integer
        Get
            Return fSeedAge
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("SeedAge", fSeedAge, value)
        End Set
    End Property

    Dim fSeedResultPerSeedSource As String
    <RuleValueComparison(DefaultContexts.Save, ValueComparisonType.GreaterThan, 0)> _
    <XafDisplayName("อัตราการขยายพันธุ์ (1:กก.)")> _
    Public Property SeedResultPerSeedSource() As Integer
        Get
            Return fSeedResultPerSeedSource
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("SeedResultPerSeedSource", fSeedResultPerSeedSource, value)
        End Set
    End Property

#Region "Association"
    <Association("SeedPrice-EstimateByGrowTypes", GetType(EstimateByGrowType))> _
    <DevExpress.Xpo.Aggregated>
    Public ReadOnly Property EstimateByGrowTypes() As XPCollection
        Get
            Return GetCollection("EstimateByGrowTypes")
        End Get
    End Property
#End Region

    Public Sub CreateEstimateByGrowTypes()
        Dim colGrowType As New XPCollection(Of GrowType)(Session, CriteriaOperator.Parse("Status='Active'"))
        colGrowType.Sorting.Add(New SortProperty("GrowNo", SortingDirection.Ascending))
        For Each item As GrowType In colGrowType
            Dim objNewItem As New EstimateByGrowType(Session)
            objNewItem.SeedPrice = Me
            objNewItem.GrowType = item
            objNewItem.SeedUsePerArea = 0
            objNewItem.QuantityPerArea = 0
            objNewItem.Save()
        Next
    End Sub

End Class
