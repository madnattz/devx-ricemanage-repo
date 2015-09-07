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
Public Class SubmitQualityHistoryDetail
    Inherits BaseObject
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

    Dim fSubmitQualityHistoryReport As SubmitQualityHistory
    <Association("SubmitQualityHistoryReport-SubmitQualityHistoryReportDetails")> _
    Public Property SubmitQualityHistoryReport() As SubmitQualityHistory
        Get
            Return fSubmitQualityHistoryReport
        End Get
        Set(ByVal value As SubmitQualityHistory)
            SetPropertyValue(Of SubmitQualityHistory)("SubmitQualityHistoryReport", fSubmitQualityHistoryReport, value)
        End Set
    End Property

    Dim fProductName As String
    <Index(1)> _
    <XafDisplayName("เมล็ดพันธุ์")> _
    <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property ProductName() As String
        Get
            Return fProductName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ProductName   ", fProductName, value)
        End Set
    End Property

    Dim fSeedClass As String
    <Index(2)> _
    <XafDisplayName("ชั้นพันธุ์")> _
    <VisibleInListView(False), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property SeedClass() As String
        Get
            Return fSeedClass
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SeedClass   ", fSeedClass, value)
        End Set
    End Property

    Dim fLotNo As String
    <Index(3)> _
    <XafDisplayName("ล็อตที่")> _
    <VisibleInListView(False), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property LotNo() As String
        Get
            Return fLotNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("LotNo   ", fLotNo, value)
        End Set
    End Property

    Dim fForBuyWetSeed As Double
    <Index(4)> _
    <XafDisplayName("จัดซื้อ-ความชื้น(%)")> _
    <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property ForBuyWetSeed() As Double
        Get
            Return fForBuyWetSeed
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("ForBuyWetSeed   ", fForBuyWetSeed, value)
        End Set
    End Property

    Dim fForBuyGrowSeed As Double
    <Index(5)> _
    <XafDisplayName("จัดซื้อ-ความงอก(%)")> _
    <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property ForBuyGrowSeed() As Double
        Get
            Return fForBuyGrowSeed
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("ForBuyGrowSeed   ", fForBuyGrowSeed, value)
        End Set
    End Property

    Dim fForBuyRedSeed As Double
    <Index(6)> _
    <XafDisplayName("จัดซื้อ-ข้าวแดง(%)")> _
    <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property ForBuyRedSeed() As Double
        Get
            Return fForBuyRedSeed
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("ForBuyRedSeed", fForBuyRedSeed, value)
        End Set
    End Property

    Dim fForBuyOtherSeed As Double
    <Index(7)> _
    <XafDisplayName("เมล็ดอื่นๆ")> _
    <VisibleInListView(False), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property ForBuyOtherSeed() As Double
        Get
            Return fForBuyOtherSeed
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("ForBuyOtherSeed", fForBuyOtherSeed, value)
        End Set
    End Property

    Dim fForBuyCheckDate As String
    <Index(8)> _
    <XafDisplayName("วันที่")> _
    <VisibleInListView(False), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property ForBuyCheckDate() As String
        Get
            Return fForBuyCheckDate
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ForBuyCheckDate", fForBuyCheckDate, value)
        End Set
    End Property

    Dim fBeforeWetSeed As Double
    <Index(9)> _
    <XafDisplayName("ก่อนปรับปรุง-ความชื้น(%)")> _
    <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property BeforeWetSeed() As Double
        Get
            Return fBeforeWetSeed
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("BeforeWetSeed", fBeforeWetSeed, value)
        End Set
    End Property

    Dim fBeforeGrowSeed As Double
    <Index(10)> _
    <XafDisplayName("ก่อนปรับปรุง-ความงอก(%)")> _
    <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property BeforeGrowSeed() As Double
        Get
            Return fBeforeWetSeed
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("BeforeGrowSeed", fBeforeGrowSeed, value)
        End Set
    End Property

    Dim fBeforeCompound As Double
    <Index(11)> _
    <XafDisplayName("ก่อนปรับปรุง-สิ่งเจือปน(%)")> _
    <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property BeforeCompound() As Double
        Get
            Return fBeforeWetSeed
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("BeforeWetSeed", fBeforeWetSeed, value)
        End Set
    End Property

    Dim fBeforeDate As String
    <Index(12)> _
    <XafDisplayName("ก่อนปรับปรุง-วันที่")> _
    <VisibleInListView(False), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property BeforeDate() As String
        Get
            Return fBeforeDate
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("BeforeDate", fBeforeDate, value)
        End Set
    End Property

    Dim fAfterWetSeed As Double
    <Index(13)> _
    <XafDisplayName("หลังปรับปรุง-ความชื้น(%)")> _
    <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property AfterWetSeed() As Double
        Get
            Return fAfterWetSeed
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("AfterWetSeed", fAfterWetSeed, value)
        End Set
    End Property

    Dim fAfterGrowSeed As Double
    <Index(14)> _
    <XafDisplayName("หลังปรับปรุง-ความงอก(%)")> _
    <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property AfterGrowSeed() As Double
        Get
            Return fAfterGrowSeed
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("AfterGrowSeed", fAfterGrowSeed, value)
        End Set
    End Property

    Dim fAfterStrongSeed As Double
    <Index(15)> _
    <XafDisplayName("ความแข็งแรง")> _
    <VisibleInListView(False), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property AfterStrongSeed() As Double
        Get
            Return fAfterStrongSeed
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("AfterStrongSeed", fAfterStrongSeed, value)
        End Set
    End Property

    Dim fAfterPureSeed As Double
    <Index(16)> _
    <XafDisplayName("เมล็ดพันธุ์สุทธิ์")> _
    <VisibleInListView(False), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property AfterPureSeed() As Double
        Get
            Return fAfterPureSeed
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("AfterPureSeed", fAfterPureSeed, value)
        End Set
    End Property

    Dim fAfterCompound As Double
    <Index(17)> _
    <XafDisplayName("หลังปรับปรุง-สิ่งเจือปน(%)")> _
    <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property AfterCompound() As Double
        Get
            Return fAfterCompound
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("AfterCompound", fAfterCompound, value)
        End Set
    End Property

    Dim fAfterRedSeed As Double
    <Index(18)> _
    <XafDisplayName("ข้าวแดง")> _
    <VisibleInListView(False), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property AfterRedSeed() As Double
        Get
            Return fAfterRedSeed
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("AfterRedSeed", fAfterRedSeed, value)
        End Set
    End Property

    Dim fAfterOtherSeed As Double
    <Index(19)> _
    <XafDisplayName("เมล็ดอื่นๆ")> _
    <VisibleInListView(False), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property AfterOtherSeed() As Double
        Get
            Return fAfterOtherSeed
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("AfterOtherSeed", fAfterOtherSeed, value)
        End Set
    End Property

    Dim fAfterWeight As Double
    <Index(20)> _
    <XafDisplayName("หลังปรับปรุง-น้ำหนัก(กก.)")> _
    <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property AfterWeight() As Double
        Get
            Return fAfterWeight
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("AfterWeight", fAfterWeight, value)
        End Set
    End Property

    Dim fAfterDate As String
    <Index(21)> _
    <XafDisplayName("วันที่ตรวจสอบ")> _
    <VisibleInListView(False), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property AfterDate() As String
        Get
            Return fAfterDate
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AfterDate", fAfterDate, value)
        End Set
    End Property

End Class
