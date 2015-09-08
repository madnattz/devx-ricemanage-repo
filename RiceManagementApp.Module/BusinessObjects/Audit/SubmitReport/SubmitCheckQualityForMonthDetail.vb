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
Imports DevExpress.Xpo.DB
Imports DevExpress.ExpressApp.ConditionalAppearance

<ImageName("BO_Product")> _
<XafDisplayName("ปริมาณการตรวจสอบคุณภาพเมล็ดพันธุ์ข้าวประจำเดือน")> _
Public Class SubmitCheckQualityForMonthDetail
    Inherits BaseObject
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

    Dim fSubmitCheckQualityForMonth As SubmitCheckQualityForMonth
    <Association("SubmitCheckQualityForMonth -SubmitCheckQualityForMonthDetails")> _
    Public Property SubmitCheckQualityForMonth() As SubmitCheckQualityForMonth
        Get
            Return fSubmitCheckQualityForMonth
        End Get
        Set(ByVal value As SubmitCheckQualityForMonth)
            SetPropertyValue(Of SubmitCheckQualityForMonth)("SubmitCheckQualityForMonth", fSubmitCheckQualityForMonth, value)
        End Set
    End Property

    Dim fSeedTypeName As String
    <Index(1)>
    <XafDisplayName("พันธุ์")> _
    <Appearance("SeedTypeName", Enabled:=False)> _
    Public Property SeedTypeName() As String
        Get
            Return fSeedTypeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SeedTypeName", fSeedTypeName, value)
        End Set
    End Property

    Dim fSeedClassName As String
    <Index(2)>
    <XafDisplayName("ชั้นพันธุ์")> _
    <Appearance("SeedClassName", Enabled:=False)> _
    Public Property SeedClassName() As String
        Get
            Return fSeedClassName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SeedClassName", fSeedClassName, value)
        End Set
    End Property

    Dim fSeasonName As String
    <Index(3)>
    <XafDisplayName("ฤดู")> _
    <Appearance("SeasonName", Enabled:=False)> _
    Public Property SeasonName() As String
        Get
            Return fSeasonName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SeasonName", fSeasonName, value)
        End Set
    End Property

    Dim fSeedYear As String
    <Index(4)>
    <XafDisplayName("ปี")> _
    <Appearance("SeedYear", Enabled:=False)> _
    Public Property SeedYear() As String
        Get
            Return fSeedYear
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SeedYear", fSeedYear, value)
        End Set
    End Property

    Dim fStepName As String
    <Index(5)>
    <XafDisplayName("ระยะที่ตรวจสอบ")> _
    <Appearance("StepName", Enabled:=False)> _
    Public Property StepName() As String
        Get
            Return fStepName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("StepName", fStepName, value)
        End Set
    End Property

    Dim fItemCount As Integer
    <Index(6)>
    <XafDisplayName("จำนวนตัวอย่าง")> _
    <Appearance("ItemCount", Enabled:=False)> _
    Public Property ItemCount() As Integer
        Get
            Return fItemCount
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ItemCount", fItemCount, value)
        End Set
    End Property

    Dim fCheckMonthNo As Integer
    <Index(7)>
    <XafDisplayName("เดือนที่")> _
    <Appearance("CheckMonthNo", Enabled:=False)> _
    <VisibleInDetailView(True), VisibleInListView(False), VisibleInLookupListView(False)> _
    Public Property CheckMonthNo() As Integer
        Get
            Return fCheckMonthNo
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CheckMonthNo", fCheckMonthNo, value)
        End Set
    End Property

    Dim fCheckMonthName As String
    <Index(8)>
    <XafDisplayName("ชื่อเดือน")> _
    <Appearance("CheckMonthName", Enabled:=False)> _
    <VisibleInDetailView(True), VisibleInListView(False), VisibleInLookupListView(False)> _
    Public Property CheckMonthName() As String
        Get
            Return fCheckMonthName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CheckMonthName", fCheckMonthName, value)
        End Set
    End Property

    Dim fCheckYear As String
    <Index(9)>
    <XafDisplayName("ปี")> _
    <Appearance("CheckYear", Enabled:=False)> _
    <VisibleInDetailView(True), VisibleInListView(False), VisibleInLookupListView(False)> _
    Public Property CheckYear() As String
        Get
            Return fCheckYear
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CheckYear", fCheckYear, value)
        End Set
    End Property

    Dim fPassQuantity As Integer
    <Index(10)>
    <XafDisplayName("น้ำหนักที่ผ่านการตรวจสอบ")> _
    <Appearance("PassQuantity", Enabled:=False)> _
    Public Property PassQuantity() As Integer
        Get
            Return fPassQuantity
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("PassQuantity", fPassQuantity, value)
        End Set
    End Property

    Dim fWet As Double
    <Index(11)>
    <VisibleInDetailView(True), VisibleInListView(False), VisibleInLookupListView(False)> _
    <XafDisplayName("ความชื้น")> _
    <Appearance("Wet", Enabled:=False)> _
    Public Property Wet() As Double
        Get
            Return fWet
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("Wet", fWet, value)
        End Set
    End Property

    Dim fCompound As Double
    <Index(12)>
    <VisibleInDetailView(True), VisibleInListView(False), VisibleInLookupListView(False)> _
    <XafDisplayName("สิ่่งเจือปน")> _
    <Appearance("Compound", Enabled:=False)> _
    Public Property Compound() As Double
        Get
            Return fCompound
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("Compound ", fCompound, value)
        End Set
    End Property

    Dim fGrow As Double
    <Index(13)>
    <VisibleInDetailView(True), VisibleInListView(False), VisibleInLookupListView(False)> _
    <XafDisplayName("ความงอก")> _
    <Appearance("Grow", Enabled:=False)> _
    Public Property Grow() As Double
        Get
            Return fGrow
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("Grow", fGrow, value)
        End Set
    End Property

    Dim fStrong As Double
    <Index(14)>
    <VisibleInDetailView(True), VisibleInListView(False), VisibleInLookupListView(False)> _
    <XafDisplayName("ความแข็งแรง")> _
    <Appearance("Strong", Enabled:=False)> _
    Public Property Strong() As Double
        Get
            Return fStrong
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("Strong", fStrong, value)
        End Set
    End Property

    Dim fPureSeed As Double
    <Index(15)>
    <VisibleInDetailView(True), VisibleInListView(False), VisibleInLookupListView(False)> _
    <XafDisplayName("ความบริสุทธิ์")> _
    <Appearance("PureSeed", Enabled:=False)> _
    Public Property PureSeed() As Double
        Get
            Return fPureSeed
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("PureSeed", fPureSeed, value)
        End Set
    End Property

    Dim fOtherSeed As Double
    <Index(16)>
    <VisibleInDetailView(True), VisibleInListView(False), VisibleInLookupListView(False)> _
    <XafDisplayName("เมล็ดพันธุ์อื่น")> _
    <Appearance("OtherSeed", Enabled:=False)> _
    Public Property OtherSeed() As Double
        Get
            Return fOtherSeed
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("OtherSeed", fOtherSeed, value)
        End Set
    End Property

    Dim fOtherRice As Double
    <Index(17)>
    <VisibleInDetailView(True), VisibleInListView(False), VisibleInLookupListView(False)> _
    <XafDisplayName("เมล็ดข้าวอื่นๆ")> _
    <Appearance("OtherRice", Enabled:=False)> _
    Public Property OtherRice() As Double
        Get
            Return fOtherRice
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("OtherRice", fOtherRice, value)
        End Set
    End Property

    Dim fRedSeed As Double
    <Index(18)>
    <VisibleInDetailView(True), VisibleInListView(False), VisibleInLookupListView(False)> _
    <XafDisplayName("เมล็ดข้าวแดง")> _
    <Appearance("RedSeed", Enabled:=False)> _
    Public Property RedSeed() As Double
        Get
            Return fRedSeed
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("RedSeed", fRedSeed, value)
        End Set
    End Property

    Dim fGreenSeed As Double
    <Index(19)>
    <VisibleInDetailView(True), VisibleInListView(False), VisibleInLookupListView(False)> _
    <XafDisplayName("เมล็ดข้าวเขียว")> _
    <Appearance("GreenSeed", Enabled:=False)> _
    Public Property GreenSeed() As Double
        Get
            Return fGreenSeed
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("GreenSeed", fGreenSeed, value)
        End Set
    End Property

    Dim fFloatSeed As Double
    <Index(20)>
    <VisibleInDetailView(True), VisibleInListView(False), VisibleInLookupListView(False)> _
    <XafDisplayName("เมล็ดข้าวลอยน้ำ")> _
    <Appearance("FloatSeed", Enabled:=False)> _
    Public Property FloatSeed() As Double
        Get
            Return fFloatSeed
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("FloatSeed", fFloatSeed, value)
        End Set
    End Property

    Dim fInsect As Double
    <Index(21)>
    <VisibleInDetailView(True), VisibleInListView(False), VisibleInLookupListView(False)> _
    <XafDisplayName("แมลง")> _
    <Appearance("Insect", Enabled:=False)> _
    Public Property Insect() As Double
        Get
            Return fInsect
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("Insect", fInsect, value)
        End Set
    End Property

    Dim fDiseaseSeed As Double
    <Index(22)>
    <VisibleInDetailView(True), VisibleInListView(False), VisibleInLookupListView(False)> _
    <XafDisplayName("เมล็ดเป็นโรค")> _
    <Appearance("DiseaseSeed", Enabled:=False)> _
    Public Property DiseaseSeed() As Double
        Get
            Return fDiseaseSeed
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("DiseaseSeed", fDiseaseSeed, value)
        End Set
    End Property

    Dim fKOH As Double
    <Index(23)>
    <VisibleInDetailView(True), VisibleInListView(False), VisibleInLookupListView(False)> _
    <XafDisplayName("KOH")> _
    <Appearance("KOH", Enabled:=False)> _
    Public Property KOH() As Double
        Get
            Return fKOH
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("KOH", fKOH, value)
        End Set
    End Property

    Dim fIodine As Double
    <Index(24)>
    <VisibleInDetailView(True), VisibleInListView(False), VisibleInLookupListView(False)> _
    <XafDisplayName("ไอโอดีน")> _
    <Appearance("Iodine", Enabled:=False)> _
    Public Property Iodine() As Double
        Get
            Return fIodine
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("Iodine", fIodine, value)
        End Set
    End Property

    Dim fStickySeed As Double
    <Index(25)>
    <VisibleInDetailView(True), VisibleInListView(False), VisibleInLookupListView(False)> _
    <XafDisplayName("เมล็ดข้าวเหนียว")> _
    <Appearance("StickySeed", Enabled:=False)> _
    Public Property StickySeed() As Double
        Get
            Return fStickySeed
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("StickySeed", fStickySeed, value)
        End Set
    End Property

    Dim fAATest As Double
    <Index(26)>
    <VisibleInDetailView(True), VisibleInListView(False), VisibleInLookupListView(False)> _
    <XafDisplayName("AATest")> _
    <Appearance("AATest", Enabled:=False)> _
    Public Property AATest() As Double
        Get
            Return fAATest
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("AATest", fAATest, value)
        End Set
    End Property

    Dim fAllTestCount As Double
    <Index(27)>
    <VisibleInDetailView(True), VisibleInListView(False), VisibleInLookupListView(False)> _
    <XafDisplayName("รวม")> _
    <Appearance("AllTestCount", Enabled:=False)> _
    Public Property AllTestCount() As Double
        Get
            Return fAllTestCount
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("AllTestCount", fAllTestCount, value)
        End Set
    End Property

    Dim fRemark As String
    <Index(28)>
    <VisibleInDetailView(True), VisibleInListView(False), VisibleInLookupListView(False)> _
    <XafDisplayName("หมายเหตุ")> _
    <Appearance("Remark", Enabled:=False)> _
    Public Property Remark() As String
        Get
            Return fRemark
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Remark", fRemark, value)
        End Set
    End Property





End Class
