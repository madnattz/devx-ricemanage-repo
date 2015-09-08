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
Public Class SubmitQualityForBuyDetail
    Inherits BaseObject
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

    Dim fSubmitQualityForBuyReport As SubmitQualityForBuy
    <Association("SubmitQualityForBuyReport -SubmitQualityForBuyReportDetails")> _
    Public Property SubmitQualityForBuyReport() As SubmitQualityForBuy
        Get
            Return fSubmitQualityForBuyReport
        End Get
        Set(ByVal value As SubmitQualityForBuy)
            SetPropertyValue(Of SubmitQualityForBuy)("SubmitQualityForBuyReport", fSubmitQualityForBuyReport, value)
        End Set
    End Property

    Dim fSampleNo As String
    <Index(1)> _
    <XafDisplayName("ทะเบียนตรวจสอบ")> _
    <Appearance("SampleNo", Enabled:=False)> _
    <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property SampleNo() As String
        Get
            Return fSampleNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SampleNo", fSampleNo, value)
        End Set
    End Property

    Dim fPlanFarmerNo As String
    <Index(2)> _
    <XafDisplayName("รหัสเกษตรกร")> _
    <Appearance("PlanFarmerNo", Enabled:=False)> _
    <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property PlanFarmerNo() As String
        Get
            Return fPlanFarmerNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PlanFarmerNo", fPlanFarmerNo, value)
        End Set
    End Property

    Dim fItemNo As String
    <Index(3)> _
    <XafDisplayName("ตัวอย่างที่")> _
    <Appearance("ItemNo", Enabled:=False)> _
    <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property ItemNo() As String
        Get
            Return fItemNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ItemNo", fItemNo, value)
        End Set
    End Property

    Dim fFullName As String
    <Index(4)> _
    <XafDisplayName("ชื่อ - สกุล")> _
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

    Dim fPlant As String
    <Index(5)> _
    <XafDisplayName("พืช")> _
    <Appearance("Plant", Enabled:=False)> _
    <VisibleInListView(False), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property Plant() As String
        Get
            Return fPlant
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Plant", fPlant, value)
        End Set
    End Property

    Dim fSeedType As String
    <Index(6)> _
    <XafDisplayName("พันธุ์")> _
    <Appearance("SeedType", Enabled:=False)> _
    <VisibleInListView(False), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property SeedType() As String
        Get
            Return fSeedType
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SeedType", fSeedType, value)
        End Set
    End Property

    Dim fSeedClass As String
    <Index(7)> _
    <XafDisplayName("ชั้นพันธุ์")> _
    <Appearance("SeedClass", Enabled:=False)> _
    <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property SeedClass() As String
        Get
            Return fSeedClass
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SeedClass  ", fSeedClass, value)
        End Set
    End Property

    Dim fSeedSeason As String
    <Index(8)> _
    <XafDisplayName("ฤดู")> _
    <Appearance("SeedSeason", Enabled:=False)> _
    <VisibleInListView(False), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property SeedSeason() As String
        Get
            Return fSeedSeason
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SeedSeason  ", fSeedSeason, value)
        End Set
    End Property

    Dim fSeedYear As String
    <Index(9)> _
    <XafDisplayName("ปี")> _
    <Appearance("SeedYear", Enabled:=False)> _
    <VisibleInListView(False), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property SeedYear() As String
        Get
            Return fSeedYear
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SeedYear", fSeedYear, value)
        End Set
    End Property

    Dim fSeedWeight As Integer
    <Index(10)> _
    <XafDisplayName("จำนวน(กก.)")> _
    <Appearance("SeedWeight", Enabled:=False)> _
    <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property SeedWeight() As Integer
        Get
            Return fSeedWeight
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("SeedWeight ", fSeedWeight, value)
        End Set
    End Property

    Dim fWet As String
    <Index(11)> _
   <XafDisplayName("ความชื้น")> _
   <Appearance("Wet", Enabled:=False)> _
   <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property Wet() As String
        Get
            Return fWet
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Wet", fWet, value)
        End Set
    End Property

    Dim fPureSeed As String
    <Index(12)> _
    <XafDisplayName("ความบริสุทธิ์(%)")> _
    <Appearance("PureSeed", Enabled:=False)> _
    <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property PureSeed() As String
        Get
            Return fPureSeed
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PureSeed", fPureSeed, value)
        End Set
    End Property

    Dim fCompound As String
    <Index(13)> _
    <XafDisplayName("สิ่งเจือปน")> _
    <Appearance("Compound", Enabled:=False)> _
    <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property Compound() As String
        Get
            Return fCompound
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Compound", fCompound, value)
        End Set
    End Property

    Dim fOtherRiceSeed As String
    <Index(14)> _
    <XafDisplayName("พันธุ์ปน(เมล็ด/70กรัม)")> _
    <Appearance("OtherRiceSeed", Enabled:=False)> _
    <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property OtherRiceSeed() As String
        Get
            Return fOtherRiceSeed
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("OtherRiceSeed ", fOtherRiceSeed, value)
        End Set
    End Property

    Dim fRedSeed As String
    <Index(15)> _
    <XafDisplayName("ข้าวแดง(เมล็ด/500กรัม)")> _
    <Appearance("RedSeed", Enabled:=False)> _
    <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property RedSeed() As String
        Get
            Return fRedSeed
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("RedSeed ", fRedSeed, value)
        End Set
    End Property

    Dim fGrow As String
    <Index(16)> _
   <XafDisplayName("ความงอก(%)")> _
   <Appearance("Grow", Enabled:=False)> _
   <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property Grow() As String
        Get
            Return fGrow
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Grow", fGrow, value)
        End Set
    End Property

    Dim fOtherSeed As String
    <Index(17)> _
   <XafDisplayName("เมล็ดข้าวอื่นๆ(เมล็ด)")> _
   <Appearance("OtherSeed", Enabled:=False)> _
   <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property OtherSeed() As String
        Get
            Return fOtherSeed
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("OtherSeed ", fOtherSeed, value)
        End Set
    End Property

    Dim fLabDate As String
    <Index(18)> _
   <XafDisplayName("วันที่ตรวจสอบ")> _
   <Appearance("LabDate", Enabled:=False)> _
   <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property LabDate() As String
        Get
            Return fLabDate
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("LabDate", fLabDate, value)
        End Set
    End Property

    Dim fCheckResults As PublicEnum.CheckResults
    <Index(19)> _
    <XafDisplayName("ผลการตรวจ")> _
    <Appearance("CheckResults", Enabled:=False)> _
    <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property CheckResults() As PublicEnum.CheckResults
        Get
            Return fCheckResults
        End Get
        Set(ByVal value As PublicEnum.CheckResults)
            SetPropertyValue(Of PublicEnum.CheckResults)("CheckResults", fCheckResults, value)
        End Set
    End Property

End Class
