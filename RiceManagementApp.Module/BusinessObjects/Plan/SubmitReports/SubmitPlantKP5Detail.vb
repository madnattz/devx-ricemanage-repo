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
Public Class SubmitPlantKP5Detail
    Inherits BaseObject
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

    Dim fSubmitPlantKP5 As SubmitPlantKP5
    <Association("SubmitPlantKP5-SubmitPlantKP5Details")> _
    Public Property SubmitPlantKP5() As SubmitPlantKP5
        Get
            Return fSubmitPlantKP5
        End Get
        Set(ByVal value As SubmitPlantKP5)
            SetPropertyValue(Of SubmitPlantKP5)("SubmitPlantKP5", fSubmitPlantKP5, value)
        End Set
    End Property

    Dim fFarmerNo As String
    <XafDisplayName("รหัสเกษตรกร")> _
    <Index(1)>
    <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property FarmerNo() As String
        Get
            Return fFarmerNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FarmerNo", fFarmerNo, value)
        End Set
    End Property

    Dim fFarmerName As String
    <XafDisplayName("ชื่อเกษตรกร")> _
    <Index(2)>
    <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property FarmerName() As String
        Get
            Return fFarmerName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FarmerName", fFarmerName, value)
        End Set
    End Property


    Dim fFarmerGroupNo As String
    <XafDisplayName("รหัสกลุ่ม")> _
    <Index(3)>
    <VisibleInListView(False), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property FarmerGroupNo() As String
        Get
            Return fFarmerGroupNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FarmerGroupNo", fFarmerGroupNo, value)
        End Set
    End Property

    Dim fFarmerGroupName As String
    <XafDisplayName("ชื่อกลุ่ม")> _
    <Index(4)>
    <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property FarmerGroupName() As String
        Get
            Return fFarmerGroupName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FarmerGroupName", fFarmerGroupName, value)
        End Set
    End Property

    Dim fAddressNo As String
    <XafDisplayName("เลขที่")> _
    <Index(5)>
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
    <XafDisplayName("หมู่")> _
    <Index(6)>
    <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property Moo() As String
        Get
            Return fMoo
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Moo", fMoo, value)
        End Set
    End Property

    Dim fTambol As String
    <XafDisplayName("ตำบล")> _
    <Index(7)>
    <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property Tambol() As String
        Get
            Return fTambol
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Tambol", fTambol, value)
        End Set
    End Property

    Dim fAmphur As String
    <XafDisplayName("อำเภอ")> _
    <Index(8)>
    <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property Amphur() As String
        Get
            Return fAmphur
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Amphur", fAmphur, value)
        End Set
    End Property

    Dim fProvince As String
    <XafDisplayName("จังหวัด")> _
    <Index(9)>
    <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property Province() As String
        Get
            Return fProvince
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Province", fProvince, value)
        End Set
    End Property

    Dim fAreaSize As String
    <XafDisplayName("พื้นที่ปลูกรวม(ไร่)")> _
    <Index(10)>
    <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property AreaSize() As String
        Get
            Return fAreaSize
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AreaSize", fAreaSize, value)
        End Set
    End Property

    Dim fBuyQuantity As String
    <XafDisplayName("เป้าซื้อคืน(กก.)")> _
    <Index(11)>
    <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property BuyQuantity() As String
        Get
            Return fBuyQuantity
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("BuyQuantity", fBuyQuantity, value)
        End Set
    End Property

    Dim fQuantityPerUnit As String
    <XafDisplayName("ผลผลิต(กก./ไร่)")> _
    <Index(12)>
    <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property QuantityPerUnit() As String
        Get
            Return fQuantityPerUnit
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("QuantityPerUnit", fQuantityPerUnit, value)
        End Set
    End Property

    Dim fSeedPrice As Double
    <XafDisplayName("ราคาซื้อคืนไม่เกิน(บาท/กก.)")> _
    <Index(13)>
    <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property SeedPrice() As Double
        Get
            Return fSeedPrice
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("SeedPrice", fSeedPrice, value)
        End Set
    End Property

    Dim fIsMaxBuyFarmer As Boolean
    <XafDisplayName("รายสูงสุด")> _
    <Index(14)>
    <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property IsMaxBuyFarmer() As Boolean
        Get
            Return fIsMaxBuyFarmer
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("IsMaxBuyFarmer", fIsMaxBuyFarmer, value)
        End Set
    End Property

End Class
