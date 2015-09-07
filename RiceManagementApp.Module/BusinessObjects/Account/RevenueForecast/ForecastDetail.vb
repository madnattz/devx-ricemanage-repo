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
Public Class ForecastDetail ' Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        DataOwner = GetCurrentSite()
        MyBase.AfterConstruction()
        ' Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).
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

    Dim _PlantID As Plant
    <XafDisplayName("พืช")> _
    <Index(0), VisibleInListView(True)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property PlantID() As Plant
        Get
            Return _PlantID
        End Get
        Set(ByVal value As Plant)
            SetPropertyValue(Of Plant)("PlantID", _PlantID, value)
            SeedTypeID = Nothing
            SeedClassID = Nothing
        End Set
    End Property

    Private _SeedTypeID As SeedType
    <XafDisplayName("พันธุ์")> _
    <Index(1), VisibleInListView(True)> _
    <DataSourceProperty("PlantID.SeedTypes")> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property SeedTypeID() As SeedType
        Get
            Return _SeedTypeID
        End Get
        Set(ByVal value As SeedType)
            SetPropertyValue("SeedTypeID", _SeedTypeID, value)
            SeedClassID = Nothing
        End Set
    End Property

    Private _SeedClassID As SeedClass
    <XafDisplayName("ชั้นพันธุ์")> _
    <Index(2), VisibleInListView(True)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property SeedClassID() As SeedClass
        Get
            Return _SeedClassID
        End Get
        Set(ByVal value As SeedClass)
            SetPropertyValue("SeedClassID", _SeedClassID, value)
        End Set
    End Property

    Private _SeasonID As Season
    <XafDisplayName("ฤดู")> _
    <Index(3), VisibleInListView(True)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property SeasonID() As Season
        Get
            Return _SeasonID
        End Get
        Set(ByVal value As Season)
            SetPropertyValue("SeasonID", _SeasonID, value)
        End Set
    End Property

    Private _YearID As String
    <XafDisplayName("ปี")> _
    <Index(4), VisibleInListView(True)> _
 <ModelDefault("PropertyEditorType", "RiceManagementApp.Module.CustomYearDropdown")> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property YearID() As String
        Get
            Return _YearID
        End Get
        Set(ByVal value As String)
            SetPropertyValue("YearID", _YearID, value)
        End Set
    End Property

    Private _GoalID As Double
    <XafDisplayName("เป้าหมายการผลิต : (ตัน)")> _
    <Index(5), VisibleInListView(True)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property GoalID() As Double
        Get
            Return _GoalID
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("GoalID", _GoalID, value)
        End Set
    End Property

    Private _BuyseedsID As Double
    <XafDisplayName("เมล็ดพันซื้อคืน : (ตัน)")> _
    <Index(6), VisibleInListView(True)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property BuyseedsID() As Double
        Get
            Return _BuyseedsID
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("BuyseedsID", _BuyseedsID, value)
        End Set
    End Property

    Private _ImprovementID As Double
    <XafDisplayName("อัตราปรับปรุง : (%)")> _
    <ModelDefault("DisplayFormat", "{0:N2}")> _
    <ModelDefault("EditMask", "N")> _
    <Index(7), VisibleInListView(True)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property ImprovementID() As Double
        Get
            Return _ImprovementID
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("ImprovementID", _ImprovementID, value)
        End Set
    End Property

    Private _CostTreasuryID As Double
    <XafDisplayName("ราคาซื้อคืนเฉลี่ย : (บาท/กก.)")> _
    <ModelDefault("DisplayFormat", "{0:N2}")> _
    <ModelDefault("EditMask", "N")> _
    <Index(8), VisibleInListView(True)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property CostTreasuryID() As Double
        Get
            Return _CostTreasuryID
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("CostTreasuryID", _CostTreasuryID, value)
        End Set
    End Property

    Private _CostSeedGood As Double
    <ModelDefault("DisplayFormat", "{0:N2}")> _
    <ModelDefault("EditMask", "N")> _
    <XafDisplayName("ต้นทุนคัดออก(เมล็ดพันธุ์ดี) : (บาท/กก.)")> _
    <Index(9), VisibleInListView(True)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public ReadOnly Property CostSeedGood() As Double
        Get
            Return GetCostPriceSS(CostTreasuryID, ImprovementID)
        End Get
    End Property

    Private Function GetCostPriceSS(_CostTreasuryID As Double, _ImprovementID As Double)
        Dim ret_val As Double = 0
        If _ImprovementID <> 0 And _CostTreasuryID <> 0 Then
            ret_val = (_CostTreasuryID * 100) / (100 - _ImprovementID)
        End If
        Return ret_val
    End Function

    Private _CostOther As Double
    <XafDisplayName("ต้นทุนอื่นๆ : (บาท/กก.)")> _
    <ModelDefault("DisplayFormat", "{0:N2}")> _
    <ModelDefault("EditMask", "N")> _
    <Index(10), VisibleInListView(True)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property CostOther() As Double
        Get
            Return _CostOther
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("CostOther", _CostOther, value)
        End Set
    End Property

    Private _CostTotal As Double
    <XafDisplayName("รวมต้นทุน : (บาท/กก.)")> _
    <ModelDefault("DisplayFormat", "{0:N2}")> _
    <ModelDefault("EditMask", "N")> _
    <Index(11), VisibleInListView(True)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property CostTotal() As Double
        Get
            If CostSeedGood <> 0 And CostOther <> 0 Then
                CostTotal = CostSeedGood + CostOther
            End If
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("CostTotal", _CostTotal, value)
        End Set
    End Property

    Private _AmntID As Double
    <XafDisplayName("ราคาจำหน่าย : (บาท/กก.)")> _
    <ModelDefault("DisplayFormat", "{0:N2}")> _
    <ModelDefault("EditMask", "N")> _
    <Index(12), VisibleInListView(True)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property AmntID() As Double
        Get
            Return _AmntID
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("AmntID", _AmntID, value)
        End Set
    End Property

    Private _TotalAmntID As Double
    <XafDisplayName("ขายราคาเต็ม : (%)")> _
    <Index(13), VisibleInListView(True)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property TotalAmntID() As Double
        Get
            Return _TotalAmntID
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("TotalAmntID", _TotalAmntID, value)
        End Set
    End Property

    Private _DiscountID As Double
    <XafDisplayName("ขายส่วนลด 5 %")> _
    <Index(14), VisibleInListView(True)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property DiscountID() As Double
        Get
            Return _DiscountID
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("DiscountID", _DiscountID, value)
        End Set
    End Property

    Private _Discount1ID As Double
    <XafDisplayName("ขายส่วนลด 7 %")> _
    <Index(15), VisibleInListView(True)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property Discount1ID() As Double
        Get
            Return _Discount1ID
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("Discount1ID", _Discount1ID, value)
        End Set
    End Property

    Private _Discount2ID As Double
    <XafDisplayName("ขายส่วนลด 10 %")> _
    <Index(16), VisibleInListView(True)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property Discount2ID() As Double
        Get
            Return _Discount2ID
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("Discount2ID", _Discount2ID, value)
        End Set
    End Property

    Private _BalanceID As Double
    <XafDisplayName("เหลือคงคลัง (%)")> _
    <Index(17), VisibleInListView(True)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property BalanceID() As Double
        Get
            Return _BalanceID
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("BalanceID", _BalanceID, value)
        End Set
    End Property

    Private _SumTotalAmntID As Double
    <XafDisplayName("ราคาคงเหลือ : (บาท/กก.)")> _
    <ModelDefault("DisplayFormat", "{0:N2}")> _
    <ModelDefault("EditMask", "N")> _
    <Index(18), VisibleInListView(True)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public ReadOnly Property SumTotalAmntID() As Double
        Get
            Return GetCostPriceSU(AmntID, TotalAmntID, DiscountID, Discount1ID, Discount2ID)
        End Get
    End Property

    Private Function GetCostPriceSU(_AmntID As Double, _TotalAmntID As Double, _DiscountID As Double, _Discount1ID As Double, _Discount2ID As Double)
        Dim ret_val As Double = 0
        If _AmntID <> 0 Then
            ret_val = ((_AmntID * _TotalAmntID) / (100)) + ((_AmntID * _DiscountID) / (100)) + ((_AmntID * _Discount1ID) / (100)) + ((_AmntID * _Discount2ID) / (100))
        End If
        Return ret_val
    End Function

    Private _ProfitAndLoss As Double
    <XafDisplayName("กำไรขาดทุน : (บาท/กก.)")> _
    <ModelDefault("DisplayFormat", "{0:N2}")> _
    <ModelDefault("EditMask", "N")> _
    <Index(19), VisibleInListView(True)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property ProfitAndLoss() As Double
        Get
            If SumTotalAmntID <> 0 And CostTotal <> 0 Then
                ProfitAndLoss = SumTotalAmntID - CostTotal
                If ProfitAndLoss > 0 Then
                    ProfitAndLoss = (Math.Ceiling(ProfitAndLoss * 100)) / 100
                Else
                    ProfitAndLoss = (Math.Ceiling(ProfitAndLoss * 1000)) / 1000
                End If
            End If
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("ProfitAndLoss", _ProfitAndLoss, value)
        End Set
    End Property

#Region "==========Association============"
    Dim _ForecastID As Forecast
    <Association("Forecast-ForecastDetail")> _
    <Index(13)> _
    <VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    Public Property ForecastID() As Forecast
        Get
            Return _ForecastID
        End Get
        Set(ByVal value As Forecast)
            SetPropertyValue(Of Forecast)("ForecastID", _ForecastID, value)
        End Set
    End Property
#End Region

End Class
