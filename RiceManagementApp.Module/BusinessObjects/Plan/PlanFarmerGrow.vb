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
Public Class PlanFarmerGrow ' Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        'Me.PlanFarmerHarvest = New PlanFarmerHarvest(Session)
        DataOwner = GetCurrentSite()
    End Sub

    Protected Overrides Sub OnSaving()
        MyBase.OnSaving()
        If fPlanFarmerFarm IsNot Nothing Then
            fPlanFarmerFarm.UpdateEstimateResultPerArea(True)
            fPlanFarmerFarm.UpdateEstimateResultQuantity(True)
            fPlanFarmerFarm.UpdateTotalGrowArea(True)
        End If

        If Me.DataOwner Is Nothing Then
            DataOwner = GetCurrentSite()
        End If
       
    End Sub

    Protected Overrides Sub OnDeleted()
        MyBase.OnDeleted()
        If fPlanFarmerFarm IsNot Nothing Then
            fPlanFarmerFarm.UpdateEstimateResultPerArea(True)
            fPlanFarmerFarm.UpdateEstimateResultQuantity(True)
            fPlanFarmerFarm.UpdateTotalGrowArea(True)
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

    Dim fPlanFarmerFarm As PlanFarmerFarm
    <Association("PlanFarmerFarm-PlanFarmerGrows")> _
    Public Property PlanFarmerFarm() As PlanFarmerFarm
        Get
            Return fPlanFarmerFarm
        End Get
        Set(ByVal value As PlanFarmerFarm)
            Dim oldPlanFarmerFarm As PlanFarmerFarm = fPlanFarmerFarm
            SetPropertyValue(Of PlanFarmerFarm)("PlanFarmer", fPlanFarmerFarm, value)
            If (Not IsLoading) AndAlso (Not IsSaving) AndAlso oldPlanFarmerFarm IsNot fPlanFarmerFarm Then
                If (oldPlanFarmerFarm IsNot Nothing) Then
                    oldPlanFarmerFarm = oldPlanFarmerFarm
                Else
                    oldPlanFarmerFarm = fPlanFarmerFarm
                End If
                oldPlanFarmerFarm.UpdateEstimateResultPerArea(True)
                oldPlanFarmerFarm.UpdateEstimateResultQuantity(True)
                oldPlanFarmerFarm.UpdateTotalGrowArea(True)
            End If
        End Set
    End Property

    'Dim fPlanFarmerHarvest As PlanFarmerHarvest
    '<VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    'Public Property PlanFarmerHarvest() As PlanFarmerHarvest
    '    Get
    '        Return fPlanFarmerHarvest
    '    End Get
    '    Set(ByVal value As PlanFarmerHarvest)
    '        SetPropertyValue(Of PlanFarmerHarvest)("fPlanFarmerHarvest", fPlanFarmerHarvest, value)
    '    End Set
    'End Property

    Dim fGrowDate As DateTime
    <XafDisplayName("วันปลูก")> _
    <ImmediatePostData()> _
    Public Property GrowDate() As DateTime
        Get
            Return fGrowDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("GrowDate", fGrowDate, value)
            'If Not IsLoading AndAlso Not IsSaving Then
            '    HarvestDate = value.AddDays(PlanFarmerFarm.PlanFarmer.SeedAge)
            '    OnChanged("HarvestDate")
            'End If
        End Set
    End Property
    Dim fHarvestDate As DateTime
    <XafDisplayName("วันเก็บเกี่ยว")> _
    Public Property HarvestDate() As DateTime
        Get
            Return fHarvestDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("HarvestDate", fHarvestDate, value)

        End Set
    End Property

    Dim fHarvestArea As Double
    <XafDisplayName("พื้นที่เก็บเกี่ยว (ไร่)")> _
    <VisibleInDetailView(True), VisibleInListView(False), VisibleInLookupListView(False)> _
    Public Property HarvestArea() As Double
        Get
            Return fHarvestArea
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("HarvestArea", fHarvestArea, value)
        End Set
    End Property

    Dim fGrowType As EstimateByGrowType
    <XafDisplayName("วิธีปลูก")> _
    <ImmediatePostData()> _
    <DataSourceCriteria("[SeedPrice.SeedType] = '@this.PlanFarmerFarm.PlanFarmer.PlanFarmerGroup.MainPlan.SeedType'" & _
        " And [SeedPrice.SeedClass] = '@this.PlanFarmerFarm.PlanFarmer.PlanFarmerGroup.MainPlan.SeedClass' " & _
        " And [SeedPrice.Season] = '@this.PlanFarmerFarm.PlanFarmer.PlanFarmerGroup.MainPlan.Season' " & _
        " And [SeedPrice.SeedYear] = '@this.PlanFarmerFarm.PlanFarmer.PlanFarmerGroup.MainPlan.SeedYear'")> _
    Public Property GrowType() As EstimateByGrowType
        Get
            Return fGrowType
        End Get
        Set(ByVal value As EstimateByGrowType)
            SetPropertyValue(Of EstimateByGrowType)("GrowType", fGrowType, value)
            If Not IsLoading AndAlso Not IsSaving Then
                Me.SeedUsePerArea = value.SeedUsePerArea
                Me.EstimateResultPerArea = value.QuantityPerArea
                OnChanged("SeedUsePerArea")
                OnChanged("EstimateResultPerArea")
            End If
        End Set
    End Property
    Dim fAreaSize As Integer
    <XafDisplayName("พื้นที่ปลูก (ไร่)")> _
    Public Property AreaSize() As Integer
        Get
            Return fAreaSize
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("AreaSize", fAreaSize, value)
        End Set
    End Property

    'Dim fSeedUsePerArea As Double
    '<ModelDefault("DisPlayFormat", ("{N2}"))> _
    '<XafDisplayName("ประมาณการการใช้เมล็ดพันธุ์ (กก./ไร่)")> _
    'Public ReadOnly Property SeedUsePerArea() As Double
    '    Get
    '        If Not GrowType Is Nothing Then
    '            Return GrowType.SeedUsePerArea
    '        Else
    '            Return 0
    '        End If

    '    End Get

    'End Property

    Dim fSeedUsePerArea As Double
    <ModelDefault("DisPlayFormat", ("{N2}"))> _
    <XafDisplayName("ประมาณการการใช้เมล็ดพันธุ์ (กก./ไร่)")> _
    Public Property SeedUsePerArea() As Double
        Get
            Return fSeedUsePerArea
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("SeedUsePerArea", fSeedUsePerArea, value)
        End Set

    End Property

    Dim fSeedAge As Integer
    <VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    Public ReadOnly Property SeedAge() As Integer
        Get
            If Not GrowType Is Nothing Then
                Return GrowType.SeedPrice.SeedAge
            Else
                Return 0
            End If
        End Get
    End Property

    '<ModelDefault("DisPlayFormat", ("{0:#,#0}"))> _
    '<XafDisplayName("ผลผลิต (กก./ไร่)")> _
    'Public ReadOnly Property EstimateResultPerArea As Double
    '    Get
    '        If Not GrowType Is Nothing Then
    '            Return GrowType.QuantityPerArea
    '        Else
    '            Return 0
    '        End If
    '    End Get
    'End Property

    Dim fEstimateResultPerArea As Double
    <ModelDefault("DisPlayFormat", ("{0:#,#0}"))> _
    <XafDisplayName("ผลผลิต (กก./ไร่)")> _
    Public Property EstimateResultPerArea As Double
        Get
            Return fEstimateResultPerArea
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("EstimateResultPerArea", fEstimateResultPerArea, value)
        End Set
    End Property

    Dim fEstimateResultQuantity As Double
    <ModelDefault("DisPlayFormat", ("{0:#,#0}"))> _
    <XafDisplayName("ประมาณการผลผลิตรวม (กก.)")> _
    Public ReadOnly Property EstimateResultQuantity As Double
        Get
            Return AreaSize * EstimateResultPerArea
        End Get
    End Property

End Class
