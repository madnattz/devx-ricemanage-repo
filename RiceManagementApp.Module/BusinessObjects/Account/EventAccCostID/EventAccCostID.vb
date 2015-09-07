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
'<RuleCriteria("CheckTotalAmount", DefaultContexts.Save, "[CompareValue] = True", "จำนวนเงินไม่ตรงกับยอดรวม")> _
'<Persistent("DatabaseTableName")> _
<NavigationItem("รายการต้นทุนการผลิต")> _
<XafDisplayName("บันทึกต้นทุนการผลิต")> _
<DefaultClassOptions()> _
Public Class EventAccCostID ' Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        DataOwner = GetCurrentSite()
        MyBase.AfterConstruction()
        ' Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).
    End Sub

    Protected Overrides Sub OnLoaded()
        MyBase.OnLoaded()
        'AccCostID.Sorting.Add(New SortProperty("CostDate", DB.SortingDirection.Ascending))
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

    Private _Plant As Plant
    <XafDisplayName("พืช")> _
    <Index(1), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    Public Property Plant() As Plant
        Get
            Return _Plant
        End Get
        Set(ByVal value As Plant)
            SetPropertyValue("Plant", _Plant, value)
        End Set
    End Property

    Private _SeedID As SeedType
    <XafDisplayName("พันธุ์")> _
    <DataSourceProperty("Plant.SeedTypes")> _
    <Index(2), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    Public Property SeedID() As SeedType
        Get
            Return _SeedID
        End Get
        Set(ByVal value As SeedType)
            SetPropertyValue("SeedID", _SeedID, value)
        End Set
    End Property

    Private _SeedTypeID As SeedClass
    <XafDisplayName("ชั้นพันธุ์")> _
    <Index(3), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    Public Property SeedTypeID() As SeedClass
        Get
            Return _SeedTypeID
        End Get
        Set(ByVal value As SeedClass)
            SetPropertyValue("SeedTypeID", _SeedTypeID, value)
        End Set
    End Property

    Private _No As String
    <XafDisplayName("รุ่นที่")> _
    <Index(4), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    Public Property No() As String
        Get
            Return _No
        End Get
        Set(ByVal value As String)
            SetPropertyValue("No", _No, value)
        End Set
    End Property

    Private _SeasonID As Season
    <XafDisplayName("ฤดู")> _
    <Index(5), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
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
    <Index(6), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    Public Property YearID() As String
        Get
            Return _YearID
        End Get
        Set(ByVal value As String)
            SetPropertyValue("YearID", _YearID, value)
        End Set
    End Property

    Private _FundingTypeID As MoneyType
    <XafDisplayName("ประเภทบัญชี")> _
    <Index(7), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    Public Property FundingTypeID() As MoneyType
        Get
            Return _FundingTypeID
        End Get
        Set(ByVal value As MoneyType)
            SetPropertyValue("_FundingTypeID", _FundingTypeID, value)
        End Set
    End Property

    <XafDisplayName("รายการต้นทุนการผลิต")> _
<Association("EventAccCostID-AccCostID", GetType(AccCostID))> _
<DC.Aggregated()> _
    Public ReadOnly Property AccCostID() As XPCollection(Of AccCostID)
        Get
            Return GetCollection(Of AccCostID)("AccCostID")
        End Get
    End Property

    <XafDisplayName("เมล็ดพันธุ์ซื้อคืน (กก.)")> _
   <PersistentAlias("AccCostID.Sum(AmountSeed)")> _
   <ModelDefault("DisplayFormat", "{0:N4}")> _
   <ModelDefault("EditMask", "N")> _
   <Index(8), VisibleInListView(False), VisibleInDetailView(True), VisibleInLookupListView(False)> _
   <ImmediatePostData()> _
    Public ReadOnly Property AmountSeed() As Double
        Get
            Try
                Dim tempObject As Object
                tempObject = EvaluateAlias("AmountSeed")
                Return CDbl(tempObject)
            Catch ex As Exception
                Return 0
            End Try
        End Get
    End Property

    <XafDisplayName("ราคาเมล็ดพันธุ์ (บาท)")> _
    <PersistentAlias("AccCostID.Sum(TotalSeed)")> _
    <ModelDefault("DisplayFormat", "{0:N4}")> _
    <ModelDefault("EditMask", "N")> _
    <Index(9), VisibleInListView(False), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    <ImmediatePostData()> _
    Public ReadOnly Property TotalSeed() As Double
        Get
            Try
                Dim tempObject As Object
                tempObject = EvaluateAlias("TotalSeed")
                Return CDbl(tempObject)
            Catch ex As Exception
                Return 0
            End Try
        End Get
    End Property

    <XafDisplayName("วัสดุการผลิต(หน่วย)")> _
    <PersistentAlias("AccCostID.Sum(AmountMaterials)")> _
<ModelDefault("DisplayFormat", "{0:N4}")> _
<ModelDefault("EditMask", "N")> _
<Index(10), VisibleInListView(False), VisibleInDetailView(True), VisibleInLookupListView(False)> _
<ImmediatePostData()> _
    Public ReadOnly Property AmountMaterials() As Double
        Get
            Try
                Dim tempObject As Object
                tempObject = EvaluateAlias("AmountMaterials")
                Return CDbl(tempObject)
            Catch ex As Exception
                Return 0
            End Try
        End Get
    End Property

    <XafDisplayName("ราคาวัสดุการผลิต (บาท)")> _
    <PersistentAlias("AccCostID.Sum(TotalMaterials)")> _
    <ModelDefault("DisplayFormat", "{0:N4}")> _
    <ModelDefault("EditMask", "N")> _
    <Index(11), VisibleInListView(False), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    <ImmediatePostData()> _
    Public ReadOnly Property TotalMaterials() As Double
        Get
            Try
                Dim tempObject As Object
                tempObject = EvaluateAlias("TotalMaterials")
                Return CDbl(tempObject)
            Catch ex As Exception
                Return 0
            End Try
        End Get
    End Property

    <XafDisplayName("เคมีภัณฑ์ (หน่วย)")> _
    <PersistentAlias("AccCostID.Sum(AmountChemical)")> _
<ModelDefault("DisplayFormat", "{0:N4}")> _
<ModelDefault("EditMask", "N")> _
<Index(12), VisibleInListView(False), VisibleInDetailView(True), VisibleInLookupListView(False)> _
<ImmediatePostData()> _
    Public ReadOnly Property AmountChemical() As Double
        Get
            Try
                Dim tempObject As Object
                tempObject = EvaluateAlias("AmountChemical")
                Return CDbl(tempObject)
            Catch ex As Exception
                Return 0
            End Try
        End Get
    End Property

    <XafDisplayName("ราคาเคมีภัณฑ์ (บาท)")> _
    <PersistentAlias("AccCostID.Sum(TotalChemical)")> _
<ModelDefault("DisplayFormat", "{0:N4}")> _
<ModelDefault("EditMask", "N")> _
<Index(13), VisibleInListView(False), VisibleInDetailView(True), VisibleInLookupListView(False)> _
<ImmediatePostData()> _
    Public ReadOnly Property TotalChemical() As Double
        Get
            Try
                Dim tempObject As Object
                tempObject = EvaluateAlias("TotalChemical")
                Return CDbl(tempObject)
            Catch ex As Exception
                Return 0
            End Try
        End Get
    End Property

    <XafDisplayName("ราคารวม (บาท)")> _
<ModelDefault("DisplayFormat", "{0:N4}")> _
<ModelDefault("EditMask", "N")> _
<Index(14), VisibleInListView(False), VisibleInDetailView(True), VisibleInLookupListView(False)> _
<ImmediatePostData()> _
    Public ReadOnly Property TotalAmount() As Double
        Get
            Try
                If TotalSeed Or TotalMaterials Or TotalChemical <> 0 Then
                    TotalAmount = (TotalSeed + TotalMaterials + TotalChemical)
                End If
            Catch ex As Exception

            End Try
        End Get
    End Property

    Private _TotalOil As Double
    <XafDisplayName("น้ำมันอบ : มูลค่า")> _
    <PersistentAlias("AccCostID.Sum(TotalOil)")> _
    <ModelDefault("DisplayFormat", "{0:N4}")> _
    <ModelDefault("EditMask", "N")> _
    <Index(15), VisibleInListView(False), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    <ImmediatePostData()> _
    Public Property TotalOil() As Double
        Get
            Try
                Dim tempObject As Object
                tempObject = EvaluateAlias("TotalOil")
                Return CDbl(tempObject)
            Catch ex As Exception
                Return 0
            End Try
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("TotalOil", _TotalOil, value)
        End Set
    End Property

    Private _AmountSeedGood As Double
    <XafDisplayName("จำนวนเมล็ดพันธุ์ดี : กก.")> _
    <ModelDefault("DisplayFormat", "{0:N4}")> _
    <ModelDefault("EditMask", "N")> _
    <Index(16), VisibleInListView(False), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    <ImmediatePostData()> _
    Public Property AmountSeedGood() As Double
        Get
            Return _AmountSeedGood
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("AmountSeedGood", _AmountSeedGood, value)
        End Set
    End Property

    Private _Material As Double
    <XafDisplayName("วัสดุการผลิต : มูลค่า")> _
    <ModelDefault("DisplayFormat", "{0:N4}")> _
    <ModelDefault("EditMask", "N")> _
    <Index(17), VisibleInListView(False), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    <ImmediatePostData()> _
    Public Property Material() As Double
        Get
            If TotalMaterials Or TotalOil <> 0 Then
                Material = (TotalMaterials - TotalOil)
            End If
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("Material", _Material, value)
        End Set
    End Property

    Private _AmountSeedOut As Double
    <XafDisplayName("จำนวนเมล็ดพันธุ์คัดออก : กก.")> _
    <ModelDefault("DisplayFormat", "{0:N4}")> _
    <ModelDefault("EditMask", "N")> _
    <Index(18), VisibleInListView(False), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    <ImmediatePostData()> _
    Public Property AmountSeedOut() As Double
        Get
            Return _AmountSeedOut
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("AmountSeedOut", _AmountSeedOut, value)
        End Set
    End Property

    Private _SeedSell As Double
    <XafDisplayName("เมล็ดพันธุ์จำหน่าย (กก.)")> _
    <ModelDefault("DisplayFormat", "{0:N4}")> _
    <ModelDefault("EditMask", "N")> _
    <Index(19), VisibleInListView(False), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    <ImmediatePostData()> _
    Public ReadOnly Property SeedSell() As Double
        Get
            If AmountSeedGood <> 0 Then
                SeedSell = AmountSeedGood
            End If
        End Get
    End Property

    Private _SeedOutUsage As Double
    <XafDisplayName("เมล็ดพันธุ์คัดออกใช้ประโยชน์ได้ (กก.)")> _
    <ModelDefault("DisplayFormat", "{0:N4}")> _
    <ModelDefault("EditMask", "N")> _
    <Index(20), VisibleInListView(False), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    <ImmediatePostData()> _
    Public ReadOnly Property SeedOutUsage() As Double
        Get
            If AmountSeedOut <> 0 Then
                SeedOutUsage = AmountSeedOut
            End If
        End Get
    End Property

    Private _CostPriceSS As Double
    <XafDisplayName("ราคาต้นทุน (บาท/กก.)")> _
    <ModelDefault("DisplayFormat", "{0:N8}")> _
    <ModelDefault("EditMask", "N")> _
    <Index(21), VisibleInListView(False), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    <ImmediatePostData()> _
    Public ReadOnly Property CostPriceSS() As Double
        Get
            Return GetCostPriceSS(TotalSeed, TotalOil, AmountSeedGood, AmountSeedOut, Material, TotalChemical)
        End Get
    End Property

    Private Function GetCostPriceSS(_UnitPriceBS As Double, _Oil As Double, _AmountSeedGood As Double, _AmountSeedOut As Double, _Material As Double, _UnitPriceCP As Double)
        Dim ret_val As Double = 0
        If _AmountSeedGood <> 0 And _AmountSeedOut <> 0 Then
            ret_val = (_UnitPriceBS + _Oil) / (_AmountSeedGood + _AmountSeedOut) + (_Material + _UnitPriceCP) / (_AmountSeedGood)
        End If
        Return ret_val
    End Function

    Private Function GetCostPriceSU(_UnitPriceBS As Double, _Oil As Double, _AmountSeedGood As Double, _AmountSeedOut As Double)
        Dim ret_val As Double = 0
        If _AmountSeedGood <> 0 And _AmountSeedOut <> 0 Then
            ret_val = (_UnitPriceBS + _Oil) / (_AmountSeedGood + _AmountSeedOut)
        End If
        Return ret_val
    End Function

    Private _CostPriceSU As Double
    <XafDisplayName("ราคาต้นทุน (บาท/กก.)")> _
    <ModelDefault("DisplayFormat", "{0:N8}")> _
    <ModelDefault("EditMask", "N")> _
    <Index(22), VisibleInListView(False), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    <ImmediatePostData()> _
    Public ReadOnly Property CostPriceSU() As Double
        Get
            Return GetCostPriceSU(TotalSeed, TotalOil, AmountSeedGood, AmountSeedOut)
        End Get
    End Property

    Private _PriceSS As Double
    <XafDisplayName("เป็นเงิน (บาท)")> _
    <ModelDefault("DisplayFormat", "{0:N4}")> _
    <ModelDefault("EditMask", "N")> _
    <Index(23), VisibleInListView(False), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    <ImmediatePostData()> _
    Public ReadOnly Property PriceSS() As Double
        Get
            If AmountSeedGood And CostPriceSS <> 0 Then
                _PriceSS = AmountSeedGood * CostPriceSS
            End If
            Return _PriceSS
        End Get
    End Property

    Private _PriceSU As Double
    <XafDisplayName("เป็นเงิน (บาท)")> _
    <ModelDefault("DisplayFormat", "{0:N4}")> _
    <ModelDefault("EditMask", "N")> _
    <Index(24), VisibleInListView(False), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    <ImmediatePostData()> _
    Public ReadOnly Property PriceSU() As Double
        Get
            If SeedOutUsage And CostPriceSU <> 0 Then
                _PriceSU = SeedOutUsage * CostPriceSU
            End If
            Return _PriceSU
        End Get
    End Property

    Private _AmountCost As Double
    <XafDisplayName("รวมต้นทุนทั้งสิ้น (บาท)")> _
    <ModelDefault("DisplayFormat", "{0:N4}")> _
    <ModelDefault("EditMask", "N")> _
    <Index(25), VisibleInListView(False), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    <ImmediatePostData()> _
    Public ReadOnly Property AmountCost() As Double
        Get
            If PriceSS And PriceSU <> 0 Then
                AmountCost = PriceSS + PriceSU
            End If
        End Get
    End Property

    'Private xxx = CompareDouble(AmountCost, TotalAmount)
    <VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)> _
    Public ReadOnly Property CompareValue As Boolean
        Get
            Return Math.Abs(AmountCost - TotalAmount) < 0.00001
        End Get
    End Property

    '<Action(Caption:="My UI Action", ConfirmationMessage:="Are you sure?", ImageName:="Attention", AutoCommit:=True)> _
    'Public Sub ActionMethod()
    '    ' Trigger a custom business logic for the current record in the UI (http://documentation.devexpress.com/#Xaf/CustomDocument2619).
    '    Me.PersistentProperty = "Paid"
    'End Sub
End Class
