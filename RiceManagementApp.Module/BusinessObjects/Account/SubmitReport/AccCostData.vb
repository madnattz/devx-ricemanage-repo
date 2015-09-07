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
Public Class AccCostData ' Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        ' Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).
    End Sub

    Dim fSubmitAccCostReport As SubmitAccCostReport
    <Association("SubmitAccCostReport-AccCostDatas")> _
    Public Property SubmitAccCostReport() As SubmitAccCostReport
        Get
            Return fSubmitAccCostReport
        End Get
        Set(ByVal value As SubmitAccCostReport)
            SetPropertyValue(Of SubmitAccCostReport)("SubmitAccCostReport", fSubmitAccCostReport, value)
        End Set
    End Property


    Private _Plant As String
    <XafDisplayName("พืช")> _
    Public Property Plant() As String
        Get
            Return _Plant
        End Get
        Set(ByVal value As String)
            SetPropertyValue("Plant", _Plant, value)
        End Set
    End Property

    Private _SeedID As String
    <XafDisplayName("พันธุ์")> _
    <DataSourceProperty("Plant.SeedTypes")> _
    Public Property SeedID() As String
        Get
            Return _SeedID
        End Get
        Set(ByVal value As String)
            SetPropertyValue("SeedID", _SeedID, value)
        End Set
    End Property

    Private _SeedTypeID As String
    <XafDisplayName("ชั้นพันธุ์")> _
    Public Property SeedTypeID() As String
        Get
            Return _SeedTypeID
        End Get
        Set(ByVal value As String)
            SetPropertyValue("SeedTypeID", _SeedTypeID, value)
        End Set
    End Property

    Private _SeasonID As String
    <XafDisplayName("ฤดู")> _
    <ImmediatePostData()> _
    Public Property Season() As String
        Get
            Return _SeasonID
        End Get
        Set(ByVal value As String)
            SetPropertyValue("SeasonID", _SeasonID, value)
            OnChanged("PayDateDetails")
        End Set
    End Property

    Private _SeedYear As String
    <XafDisplayName("ปี")> _
    <ImmediatePostData()> _
    Public Property SeedYear() As String
        Get
            Return _SeedYear
        End Get
        Set(ByVal value As String)
            SetPropertyValue("SeedYear", _SeedYear, value)
            OnChanged("PayDateDetails")
        End Set
    End Property

    Private _AmountSeed As Double
    <XafDisplayName("น้ำหนักเมล็ดพันธุ์   ซื้อคืน (กก.)")> _
<ModelDefault("DisplayFormat", "{0:N4}")> _
<ModelDefault("EditMask", "N")> _
<ImmediatePostData()> _
    Public Property AmountSeed() As Double
        Get
            Return _AmountSeed
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("AmountSeed", _AmountSeed, value)
        End Set
    End Property

    Private _AmountSeedGood As Double
    <XafDisplayName("น้ำหนักเมล็ดพันธุ์ดี (กก.)")> _
    <ModelDefault("DisplayFormat", "{0:N4}")> _
    <ModelDefault("EditMask", "N")> _
    <ImmediatePostData()> _
    Public Property AmountSeedGood() As Double
        Get
            Return _AmountSeedGood
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("AmountSeedGood", _AmountSeedGood, value)
        End Set
    End Property

    Private _AmountSeedOut As Double
    <XafDisplayName("น้ำหนักเมล็ดพันธุ์คัดออก (กก.)")> _
    <ModelDefault("DisplayFormat", "{0:N4}")> _
    <ModelDefault("EditMask", "N")> _
    <ImmediatePostData()> _
    Public Property AmountSeedOut() As Double
        Get
            Return _AmountSeedOut
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("AmountSeedOut", _AmountSeedOut, value)
        End Set
    End Property

    Private _TotalSeed As Double
    <XafDisplayName("วงเงินจัดซื้อเมล็ดพันธุ์ซื้อคืนทั้งสิ้น (บาท)")> _
<ModelDefault("DisplayFormat", "{0:N4}")> _
<ModelDefault("EditMask", "N")> _
<ImmediatePostData()> _
    Public Property TotalSeed() As Double
        Get
            Return _TotalSeed
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("TotalSeed", _TotalSeed, value)
        End Set
    End Property

    Private _TotalMaterials As Double
    <XafDisplayName("วัสดุที่ใช้ในการผลิต (บาท)")> _
<ModelDefault("DisplayFormat", "{0:N4}")> _
<ModelDefault("EditMask", "N")> _
<ImmediatePostData()> _
    Public Property TotalMaterials() As Double
        Get
            Return _TotalMaterials
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("TotalMaterials", _TotalMaterials, value)
        End Set
    End Property

    Private _TotalOil As Double
    <XafDisplayName("ค่าน้ำมัน")> _
<ModelDefault("DisplayFormat", "{0:N4}")> _
<ModelDefault("EditMask", "N")> _
<ImmediatePostData()> _
    Public Property TotalOil() As Double
        Get
            Return _TotalOil
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("TotalOil", _TotalOil, value)
        End Set
    End Property

    Private _TotalChemical As Double
    <XafDisplayName("เคมีภัณฑ์ที่ใช้ในการผลิต (บาท)")> _
<ModelDefault("DisplayFormat", "{0:N4}")> _
<ModelDefault("EditMask", "N")> _
<ImmediatePostData()> _
    Public Property TotalChemical() As Double
        Get
            Return _TotalChemical
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("TotalChemical", _TotalChemical, value)
        End Set
    End Property

    Private _PriceSeedGood As Double
    <XafDisplayName("ราคาต้นทุนเมล็ดพันธุ์ดีรวม (บาท)")> _
<ModelDefault("DisplayFormat", "{0:N4}")> _
<ModelDefault("EditMask", "N")> _
<ImmediatePostData()> _
    Public Property PriceSeedGood() As Double
        Get
            Return _PriceSeedGood
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("PriceSeedGood", _PriceSeedGood, value)
        End Set
    End Property

    Private _PriceSeedOutUsage As Double
    <XafDisplayName("ราคาต้นทุนเมล็ดพันธุ์คัดออก (บาท)")> _
<ModelDefault("DisplayFormat", "{0:N4}")> _
<ModelDefault("EditMask", "N")> _
<ImmediatePostData()> _
    Public Property PriceSeedOutUsage() As Double
        Get
            Return _PriceSeedOutUsage
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("PriceSeedOutUsage", _PriceSeedOutUsage, value)
        End Set
    End Property

    Private _CostPriceSeedGood As Double
    <XafDisplayName("ราคาต้นทุนเมล็ดพันธุ์ดี/กก. (บาท)")> _
<ModelDefault("DisplayFormat", "{0:N4}")> _
<ModelDefault("EditMask", "N")> _
<ImmediatePostData()> _
    Public Property CostPriceSeedGood() As Double
        Get
            Return _CostPriceSeedGood
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("CostPriceSeedGood", _CostPriceSeedGood, value)
        End Set
    End Property

    Private _CostPriceSeedOutUsage As Double
    <XafDisplayName("ราคาต้นทุนเมล็ดพันธุ์คัดออก/กก. (บาท)")> _
<ModelDefault("DisplayFormat", "{0:N4}")> _
<ModelDefault("EditMask", "N")> _
<ImmediatePostData()> _
    Public Property CostPriceSeedOutUsage() As Double
        Get
            Return _CostPriceSeedOutUsage
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("CostPriceSeedOutUsage", _CostPriceSeedOutUsage, value)
        End Set
    End Property

    '<Action(Caption:="My UI Action", ConfirmationMessage:="Are you sure?", ImageName:="Attention", AutoCommit:=True)> _
    'Public Sub ActionMethod()
    '    ' Trigger a custom business logic for the current record in the UI (http://documentation.devexpress.com/#Xaf/CustomDocument2619).
    '    Me.PersistentProperty = "Paid"
    'End Sub
End Class
