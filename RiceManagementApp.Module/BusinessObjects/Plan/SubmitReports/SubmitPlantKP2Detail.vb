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


<ImageName("BO_Department")> _
Public Class SubmitPlantKP2Detail
    Inherits BaseObject
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

    Dim fSubmitPlantKP2 As SubmitPlantKP2
    <Association("SubmitPlantKP2-SubmitPlantKP2Details")> _
    Public Property SubmitPlantKP2() As SubmitPlantKP2
        Get
            Return fSubmitPlantKP2
        End Get
        Set(ByVal value As SubmitPlantKP2)
            SetPropertyValue(Of SubmitPlantKP2)("SubmitPlantKP2", fSubmitPlantKP2, value)
        End Set
    End Property

    Dim fPlanFarmerNo As String
    <Index(1)> _
    <XafDisplayName("รหัสเกษตกร")> _
    <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property PlanFarmerNo() As String
        Get
            Return fPlanFarmerNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PlanFarmerNo", fPlanFarmerNo, value)
        End Set
    End Property

    Dim fFullName As String
    <Index(2)> _
    <XafDisplayName("ชื่อ-นามสกุล")> _
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
    <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property ProvinceName() As String
        Get
            Return fProvinceName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ProvinceName", fProvinceName, value)
        End Set
    End Property

    Dim fTotalGrowArea As Integer
    <Index(10)> _
    <XafDisplayName("พื้นที่ปลูกรวม(ไร่)")> _
    <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property TotalGrowArea() As Integer
        Get
            Return fTotalGrowArea
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("TotalGrowArea", fTotalGrowArea, value)
        End Set
    End Property

    Dim fTotalSeedUse As Integer
    <Index(11)> _
    <XafDisplayName("รวมใช้เมล็ดพันธุ์(กก.)")> _
    <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property TotalSeedUse() As Integer
        Get
            Return fTotalSeedUse
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("TotalSeedUse", fTotalSeedUse, value)
        End Set
    End Property

    Dim fTotalSeedReceive As Integer
    <Index(12)> _
    <XafDisplayName("รวมรับเมล็ดพันธุ์(กก.)")> _
    <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property TotalSeedReceive() As Integer
        Get
            Return fTotalSeedReceive
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("TotalSeedRecive", fTotalSeedReceive, value)
        End Set
    End Property

    Dim fFullSeedLotName As String
    <Index(13)> _
    <XafDisplayName("แหล่งที่มาเมล็ดพันธุ์")> _
    <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property FullSeedLotName() As String
        Get
            Return fFullSeedLotName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FullSeedLotName", fFullSeedLotName, value)
        End Set
    End Property

    Dim fMaxBuyQuantity As Integer
    <Index(12)> _
    <XafDisplayName("เป้าซื้อคืน(กก.)")> _
    <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property MaxBuyQuantity() As Integer
        Get
            Return fMaxBuyQuantity
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("MaxBuyQuantity", fMaxBuyQuantity, value)
        End Set
    End Property

    Dim fIsMaximumFarmer As Boolean
    <Index(14)> _
    <XafDisplayName("รายสูงสุด")> _
    <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property IsMaximumFarmer() As Boolean
        Get
            Return fIsMaximumFarmer
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("IsMaximumFarmer", fIsMaximumFarmer, value)
        End Set
    End Property

End Class
