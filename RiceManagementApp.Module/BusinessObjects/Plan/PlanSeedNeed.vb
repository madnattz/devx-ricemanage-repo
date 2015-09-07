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

<NavigationItem("ความต้องการใช้เมล็ดพันธุ์")> _
<XafDisplayName("ความต้องการใช้เมล็ดพันธุ์จัดทำแปลง")> _
<ImageName("BO_Task")> _
<DefaultClassOptions()> _
Public Class PlanSeedNeed
    Inherits BaseObject
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

    Private _PlantName As Plant
    <Index(0)> _
    <XafDisplayName("พืช")> _
    <RuleRequiredField(DefaultContexts.Save)> _
    <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)>
    <DataSourceCriteria("Status='Active'")> _
    Public Property PlantName() As Plant
        Get
            Return _PlantName
        End Get
        Set(ByVal value As Plant)
            SetPropertyValue("PlantName", _PlantName, value)
        End Set
    End Property

    Private _SeedName As SeedType
    <Index(1)> _
    <XafDisplayName("พันธุ์")> _
    <RuleRequiredField(DefaultContexts.Save)> _
    <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)>
    <DataSourceProperty("PlantName.SeedTypes")> _
    Public Property SeedName() As SeedType
        Get
            Return _SeedName
        End Get
        Set(ByVal value As SeedType)
            SetPropertyValue("SeedName", _SeedName, value)
        End Set
    End Property

    Private _SeasonName As Season
    <Index(2)> _
    <XafDisplayName("ฤดู")> _
    <RuleRequiredField(DefaultContexts.Save)> _
    <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)>
    Public Property SeasonName() As Season
        Get
            Return _SeasonName
        End Get
        Set(ByVal value As Season)
            SetPropertyValue("SeasonName", _SeasonName, value)
        End Set
    End Property

    Private _SeedYear As String
    <Index(3)> _
    <XafDisplayName("ปี")> _
    <RuleRequiredField(DefaultContexts.Save)> _
    <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)>
    <ModelDefault("PropertyEditorType", "RiceManagementApp.Module.Win.CustomYearDropdown")> _
    Public Property SeedYear() As String
        Get
            Return _SeedYear
        End Get
        Set(ByVal value As String)
            SetPropertyValue("SeedYear", _SeedYear, value)
        End Set
    End Property

    Private _TotalGoal As Integer
    <Index(4)> _
    <XafDisplayName("เป้าหมายการผลิต(ตัน)")> _
   <RuleRequiredField(DefaultContexts.Save)> _
    <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)>
    Public Property TotalGoal() As Integer
        Get
            Return _TotalGoal
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue("TotalGoal", _TotalGoal, value)
        End Set
    End Property

    Private _AreaSize As Integer
    <Index(5)> _
    <XafDisplayName("พื้นที่ปลูก(ไร่)")> _
   <RuleRequiredField(DefaultContexts.Save)> _
    <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)>
    Public Property AreaSize() As Integer
        Get
            Return _AreaSize
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue("AreaSize", _AreaSize, value)
        End Set
    End Property

    Private _ScaleGrow As Integer
    <Index(6)> _
    <XafDisplayName("อัตราการปลูก(กก./ไร่)")> _
    <RuleRequiredField(DefaultContexts.Save)> _
    <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)>
    Public Property ScaleGrow() As Integer
        Get
            Return _ScaleGrow
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue("ScaleGrow", _ScaleGrow, value)
        End Set
    End Property

    Private _MainSeed As Integer
    <Index(7)> _
    <XafDisplayName("เมล็ดพันธุ์หลัก")> _
    <RuleRequiredField(DefaultContexts.Save)> _
    <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)>
    Public Property MainSeed() As Integer
        Get
            Return _MainSeed
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue("MainSeed", _MainSeed, value)
        End Set
    End Property

    Private _SpreadSeed As Integer
    <Index(8)> _
    <XafDisplayName("เมล็ดพันธุ์ขยาย")> _
    <RuleRequiredField(DefaultContexts.Save)> _
    <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)>
    Public Property SpreadSeed() As Integer
        Get
            Return _SpreadSeed
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue("SpreadSeed", _SpreadSeed, value)
        End Set
    End Property

    Private _NeedDateStart As Date
    <Index(9)> _
    <XafDisplayName("ช่วงเวลาที่ต้องการ")> _
    <RuleRequiredField(DefaultContexts.Save)> _
    <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)>
    Public Property NeedDateStart() As Date
        Get
            Return _NeedDateStart
        End Get
        Set(ByVal value As Date)
            SetPropertyValue("NeedDateStart", _NeedDateStart, value)
        End Set
    End Property

    Private _NeedDateEnd As Date
    <Index(10)> _
    <XafDisplayName("ถึงวันที่")> _
    <RuleRequiredField(DefaultContexts.Save)> _
    <VisibleInListView(False), VisibleInDetailView(True), VisibleInLookupListView(False)>
    Public Property NeedDateEnd() As Date
        Get
            Return _NeedDateEnd
        End Get
        Set(ByVal value As Date)
            SetPropertyValue("NeedDateEnd", _NeedDateEnd, value)
        End Set
    End Property

    Private _GrowDateStart As Date
    <Index(11)> _
    <XafDisplayName("วันปลูก")> _
    <RuleRequiredField(DefaultContexts.Save)> _
    <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)>
    Public Property GrowDateStart() As Date
        Get
            Return _GrowDateStart
        End Get
        Set(ByVal value As Date)
            SetPropertyValue("GrowDateStart", _GrowDateStart, value)
        End Set
    End Property

    Private _GrowDateEnd As Date
    <Index(12)> _
    <XafDisplayName("ถึงวันที่")> _
    <RuleRequiredField(DefaultContexts.Save)> _
    <VisibleInListView(False), VisibleInDetailView(True), VisibleInLookupListView(False)>
    Public Property GrowDateEnd() As Date
        Get
            Return _GrowDateEnd
        End Get
        Set(ByVal value As Date)
            SetPropertyValue("GrowDateEnd", _GrowDateEnd, value)
        End Set
    End Property

    Private _HarvestDateStart As Date
    <Index(13)> _
    <XafDisplayName("วันเก็บเกี่ยว")> _
    <RuleRequiredField(DefaultContexts.Save)> _
    <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)>
    Public Property HarvestDateStart() As Date
        Get
            Return _HarvestDateStart
        End Get
        Set(ByVal value As Date)
            SetPropertyValue("HarvestDateStart", _HarvestDateStart, value)
        End Set
    End Property

    Private _HarvestDateEnd As Date
    <Index(14)> _
    <XafDisplayName("ถึงวันที่")> _
    <RuleRequiredField(DefaultContexts.Save)> _
    <VisibleInListView(False), VisibleInDetailView(True), VisibleInLookupListView(False)>
    Public Property HarvestDateEnd() As Date
        Get
            Return _HarvestDateEnd
        End Get
        Set(ByVal value As Date)
            SetPropertyValue("HarvestDateEnd", _HarvestDateEnd, value)
        End Set
    End Property

    Private _Remark As String
    <Index(15)> _
    <XafDisplayName("หมายเหตุ")> _
    <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)>
    Public Property Remark() As String
        Get
            Return _Remark
        End Get
        Set(ByVal value As String)
            SetPropertyValue("Remark", _Remark, value)
        End Set
    End Property

    Private _SumGrowDate As String
    <Index(16)> _
    <XafDisplayName("รวมวันปลูก")> _
    <VisibleInListView(False), VisibleInDetailView(True), VisibleInLookupListView(False)>
    Public ReadOnly Property SumGrowDate() As String
        Get
            Return GrowDateStart + " - " + GrowDateEnd
        End Get
    End Property

    Private _SumNeedDate As String
    <Index(17)> _
    <XafDisplayName("รวมช่วงเวลาที่ต้องการ")> _
    <VisibleInListView(False), VisibleInDetailView(True), VisibleInLookupListView(False)>
    Public ReadOnly Property SumNeedDate() As String
        Get
            Return NeedDateStart + " - " + NeedDateEnd
        End Get
    End Property

    Private _SumHarvestDate As String
    <Index(18)> _
    <XafDisplayName("รวมเก็บเกี่ยว")> _
    <VisibleInListView(False), VisibleInDetailView(True), VisibleInLookupListView(False)>
    Public ReadOnly Property SumHarvestDate() As String
        Get
            Return HarvestDateStart + " - " + HarvestDateEnd
        End Get
    End Property

End Class
