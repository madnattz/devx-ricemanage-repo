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
Imports DevExpress.ExpressApp.ConditionalAppearance


<ImageName("BO_Department")> _
Public Class SubmitPlantKP3Detail
    Inherits BaseObject
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

    Dim fSubmitPlantKP3 As SubmitPlantKP3
    <Association("SubmitPlantKP3-SubmitPlantKP3Details")> _
    Public Property SubmitPlantKP3() As SubmitPlantKP3
        Get
            Return fSubmitPlantKP3
        End Get
        Set(ByVal value As SubmitPlantKP3)
            SetPropertyValue(Of SubmitPlantKP3)("SubmitPlantKP3", fSubmitPlantKP3, value)
        End Set
    End Property

    Dim fFarmerNo As String
    <Index(1)> _
    <XafDisplayName("รหัสเกษตกร")> _
    <Appearance("FarmerNo", Enabled:=False)> _
    <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property FarmerNo() As String
        Get
            Return fFarmerNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FarmerNo", fFarmerNo, value)
        End Set
    End Property

    Dim fFullName As String
    <Index(2)> _
    <XafDisplayName("ชื่อ-นามสกุล")> _
    <Appearance("FullName", Enabled:=False)> _
    <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property FullName() As String
        Get
            Return fFullName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FullName", fFullName, value)
        End Set
    End Property

    Dim fGroupCode As String
    <Index(3)> _
    <XafDisplayName("รหัสกลุ่ม")> _
    <Appearance("GroupCode", Enabled:=False)> _
    <VisibleInListView(False), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property GroupCode() As String
        Get
            Return fGroupCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("GroupCode", fGroupCode, value)
        End Set
    End Property

    Dim fGroupName As String
    <Index(4)> _
    <XafDisplayName("ชื่อกลุ่ม")> _
    <Appearance("GroupName", Enabled:=False)> _
    <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property GroupName() As String
        Get
            Return fGroupName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("GroupName", fGroupName, value)
        End Set
    End Property

    Dim fAddressNo As String
    <Index(5)> _
    <XafDisplayName("เลขที่")> _
    <Appearance("AddressNo", Enabled:=False)> _
    <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property AddressNo() As String
        Get
            Return fAddressNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AddressNo", fAddressNo, value)
        End Set
    End Property

    Dim fMoo As String
    <Index(6)> _
    <XafDisplayName("หมู่ที่")> _
    <Appearance("Moo", Enabled:=False)> _
    <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property Moo() As String
        Get
            Return fMoo
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Moo", fMoo, value)
        End Set
    End Property

    Dim fSubDistrictName As String
    <Index(7)> _
    <XafDisplayName("ตำบล")> _
    <Appearance("SubDistrictName", Enabled:=False)> _
    <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property SubDistrictName() As String
        Get
            Return fSubDistrictName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SubDistrictName", fSubDistrictName, value)
        End Set
    End Property

    Dim fDistrictName As String
    <Index(8)> _
    <XafDisplayName("อำเภอ")> _
    <Appearance("DistrictName", Enabled:=False)> _
    <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property DistrictName() As String
        Get
            Return fDistrictName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("DistrictName", fDistrictName, value)
        End Set
    End Property

    Dim fProvinceName As String
    <Index(9)> _
    <XafDisplayName("จังหวัด")> _
    <Appearance("ProvinceName", Enabled:=False)> _
    <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property ProvinceName() As String
        Get
            Return fProvinceName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ProvinceName", fProvinceName, value)
        End Set
    End Property

    Dim fGroupAddressNo As String
    <Index(10)> _
    <XafDisplayName("(กลุ่ม)เลขที่")> _
    <Appearance("GroupAddressNo", Enabled:=False)> _
    <VisibleInListView(False), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property GroupAddressNo() As String
        Get
            Return fGroupAddressNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("GroupAddressNo ", fGroupAddressNo, value)
        End Set
    End Property

    Dim fGroupMoo As String
    <Index(11)> _
    <XafDisplayName("(กลุ่ม)หมู่ที่")> _
    <Appearance("GroupMoo", Enabled:=False)> _
    <VisibleInListView(False), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property GroupMoo() As String
        Get
            Return fGroupMoo
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("GroupMoo", fGroupMoo, value)
        End Set
    End Property

    Dim fGroupSubDistrictName As String
    <Index(12)> _
    <XafDisplayName("(กลุ่ม)ตำบล")> _
    <Appearance("GroupSubDistrictName", Enabled:=False)> _
    <VisibleInListView(False), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property GroupSubDistrictName() As String
        Get
            Return fGroupSubDistrictName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("GroupSubDistrictName", fGroupSubDistrictName, value)
        End Set
    End Property

    Dim fGroupDistrictName As String
    <Index(13)> _
    <XafDisplayName("(กลุ่ม)อำเภอ")> _
    <Appearance("GroupDistrictName", Enabled:=False)> _
    <VisibleInListView(False), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property GroupDistrictName() As String
        Get
            Return fGroupDistrictName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("GroupDistrictName", fGroupDistrictName, value)
        End Set
    End Property

    Dim fGroupProvinceName As String
    <Index(14)> _
    <XafDisplayName("(กลุ่ม)จังหวัด")> _
    <Appearance("GroupProvinceName", Enabled:=False)> _
    <VisibleInListView(False), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property GroupProvinceName() As String
        Get
            Return fGroupProvinceName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("GroupProvinceName", fGroupProvinceName, value)
        End Set
    End Property

    Dim fTotalGrowArea As Integer
    <Index(15)> _
    <XafDisplayName("พื้นที่ปลูกรวม(ไร่)")> _
    <Appearance("TotalGrowArea", Enabled:=False)> _
    <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property TotalGrowArea() As Integer
        Get
            Return fTotalGrowArea
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("TotalGrowArea", fTotalGrowArea, value)
        End Set
    End Property

    Dim fTotalSeedReceive As Integer
    <Index(16)> _
    <XafDisplayName("รวมใช้เมล็ดพันธุ์(กก.)")> _
    <Appearance("TotalSeedReceive", Enabled:=False)> _
    <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property TotalSeedReceive() As Integer
        Get
            Return fTotalSeedReceive
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("TotalSeedReceive", fTotalSeedReceive, value)
        End Set
    End Property

    Dim fTotalSeedUse As Integer
    <Index(17)> _
    <XafDisplayName("รวมรับเมล็ดพันธุ์(กก.)")> _
    <Appearance("TotalSeedUse", Enabled:=False)> _
    <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property TotalSeedUse() As Integer
        Get
            Return fTotalSeedUse
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("TotalSeedUse", fTotalSeedUse, value)
        End Set
    End Property

    Dim fMaxBuyQuantity As Integer
    <Index(18)> _
    <XafDisplayName("เป้าซื้อคืน(กก.)")> _
    <Appearance("MaxBuyQuantity", Enabled:=False)> _
    <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property MaxBuyQuantity() As Integer
        Get
            Return fMaxBuyQuantity
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("MaxBuyQuantity", fMaxBuyQuantity, value)
        End Set
    End Property

    Dim fGrowStartDate As Date
    <Index(19)> _
    <XafDisplayName("วันที่เริ่มปลูก")> _
    <Appearance("GrowStartDate", Enabled:=False)> _
    <VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)> _
    Public Property GrowStartDate() As Date
        Get
            Return fGrowStartDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue(Of Date)("GrowStartDate", fGrowStartDate, value)
        End Set
    End Property

    Dim fGrowEndDate As Date
    <Index(20)> _
    <XafDisplayName("วันที่สิ้นสุดการปลูก")> _
    <Appearance("GrowEndDate", Enabled:=False)> _
    <VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)> _
    Public Property GrowEndDate() As Date
        Get
            Return fGrowEndDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue(Of Date)("GrowEndDate", fGrowEndDate, value)
        End Set
    End Property

    Dim fCheckFarmStartDate As Date
    <Index(21)> _
    <XafDisplayName("วันที่เริ่มตรวจแปลง")> _
    <Appearance("CheckFarmStartDate", Enabled:=False)> _
    <VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)> _
    Public Property CheckFarmStartDate() As Date
        Get
            Return fCheckFarmStartDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue(Of Date)("CheckFarmStartDate", fCheckFarmStartDate, value)
        End Set
    End Property

    Dim fCheckFarmEndDate As Date
    <Index(22)> _
    <XafDisplayName("วันที่สิ้นสุดการตรวจแปลง")> _
    <Appearance("CheckFarmEndDate", Enabled:=False)> _
    <VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)> _
    Public Property CheckFarmEndDate() As Date
        Get
            Return fCheckFarmEndDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue(Of Date)("CheckFarmEndDate", fCheckFarmEndDate, value)
        End Set
    End Property

    Dim fHarvestStartDate As Date
    <Index(23)> _
    <XafDisplayName("วันที่เริ่มเก็บเกี่ยว")> _
    <Appearance("HarvestStartDate", Enabled:=False)> _
    <VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)> _
    Public Property HarvestStartDate() As Date
        Get
            Return fHarvestStartDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue(Of Date)("HarvestStartDate", fHarvestStartDate, value)
        End Set
    End Property

    Dim fHarvestEndDate As Date
    <Index(24)> _
    <XafDisplayName("วันที่สิ้นสุดการเก็บเกี่ยว")> _
    <Appearance("HarvestEndDate", Enabled:=False)> _
    <VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)> _
    Public Property HarvestEndDate() As Date
        Get
            Return fHarvestEndDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue(Of Date)("HarvestEndDate", fHarvestEndDate, value)
        End Set
    End Property

    Dim fBuyStartDate As Date
    <Index(25)> _
    <XafDisplayName("วันที่เริ่มจัดซื้อ")> _
    <Appearance("BuyStartDate", Enabled:=False)> _
    <VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)> _
    Public Property BuyStartDate() As Date
        Get
            Return fBuyStartDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue(Of Date)("BuyStartDate", fBuyStartDate, value)
        End Set
    End Property

    Dim fBuyEndDate As Date
    <Index(26)> _
    <XafDisplayName("วันที่สิ้นสุดการจัดซื้อ")> _
    <Appearance("BuyEndDate", Enabled:=False)> _
    <VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)> _
    Public Property BuyEndDate() As Date
        Get
            Return fBuyEndDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue(Of Date)("BuyEndDate", fBuyEndDate, value)
        End Set
    End Property

    Dim fProcessStartDate As Date
    <Index(27)> _
    <XafDisplayName("วันที่เริ่มปรับปรุง")> _
    <Appearance("ProcessStartDate", Enabled:=False)> _
    <VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)> _
    Public Property ProcessStartDate() As Date
        Get
            Return fProcessStartDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue(Of Date)("ProcessStartDate", fProcessStartDate, value)
        End Set
    End Property

    Dim fProcessEndDate As Date
    <Index(28)> _
    <XafDisplayName("วันที่สิ้นสุดการปรับปรุง")> _
    <Appearance("ProcessEndDate", Enabled:=False)> _
    <VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)> _
    Public Property ProcessEndDate() As Date
        Get
            Return fProcessEndDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue(Of Date)("ProcessEndDate", fProcessEndDate, value)
        End Set
    End Property

    Dim fMarketStartDate As Date
    <Index(29)> _
    <XafDisplayName("วันที่เริ่มส่งเสริมการตลาด")> _
    <Appearance("MarketStartDate", Enabled:=False)> _
    <VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)> _
    Public Property MarketStartDate() As Date
        Get
            Return fMarketStartDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue(Of Date)("MarketStartDate", fMarketStartDate, value)
        End Set
    End Property

    Dim fMarketEndDate As Date
    <Index(30)> _
    <XafDisplayName("วันที่สิ้นสุดส่งเสริมการตลาด")> _
    <Appearance("MarketEndDate", Enabled:=False)> _
    <VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)> _
    Public Property MarketEndDate() As Date
        Get
            Return fMarketEndDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue(Of Date)("MarketEndDate", fMarketEndDate, value)
        End Set
    End Property

    Dim fIsMaximumFarmer As Boolean
    <Index(31)> _
    <XafDisplayName("รายสูงสุด")> _
    <Appearance("IsMaximumFarmer", Enabled:=False)> _
    <VisibleInListView(True), VisibleInDetailView(False), VisibleInLookupListView(False)> _
    Public Property IsMaximumFarmer() As Boolean
        Get
            Return fIsMaximumFarmer
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("IsMaximumFarmer", fIsMaximumFarmer, value)
        End Set
    End Property
End Class
