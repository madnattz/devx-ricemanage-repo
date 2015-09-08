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

<ImageName("BO_List")> _
<DefaultClassOptions()> _
Public Class SubmitCheckQualityDetail
    Inherits BaseObject
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

    Dim fSubmitCheckQuality As SubmitCheckQuality
    <Association("SubmitCheckQuality-SubmitCheckQualityDetails")> _
    Public Property SubmitCheckQuality() As SubmitCheckQuality
        Get
            Return fSubmitCheckQuality
        End Get
        Set(ByVal value As SubmitCheckQuality)
            SetPropertyValue(Of SubmitCheckQuality)("SubmitCheckQuality", fSubmitCheckQuality, value)
        End Set
    End Property

    Dim fSampleNo As String
    <Index(1)>
    <XafDisplayName("ทะเบียนตรวจสอบ")> _
    <Appearance("SampleNo", Enabled:=False)> _
    Public Property SampleNo() As String
        Get
            Return fSampleNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SampleNo", fSampleNo, value)
        End Set
    End Property

    Dim fSeedTypeName As String
    <Index(2)>
    <XafDisplayName("พันธุ์")> _
    <Appearance("SeedTypeName", Enabled:=False)> _
    Public Property SeedTypeName() As String
        Get
            Return fSeedTypeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SeedTypeName ", fSeedTypeName, value)
        End Set
    End Property

    Dim fSeedLot As String
    <Index(3)>
    <XafDisplayName("ล็อต")> _
    <Appearance("SeedLot", Enabled:=False)> _
    Public Property SeedLot() As String
        Get
            Return fSeedLot
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SeedLot ", fSeedLot, value)
        End Set
    End Property

    Dim fHarvestDate As DateTime
    <Index(4)>
    <XafDisplayName("วันที่เก็บเกี่ยว")> _
    <Appearance("HarvestDate", Enabled:=False)> _
    Public Property HarvestDate() As DateTime
        Get
            Return fHarvestDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("HarvestDate", fHarvestDate, value)
        End Set
    End Property

    Dim fSeedSource As String
    <Index(5)>
    <XafDisplayName("แหล่งรับเมล็ดพันธุ์")> _
    <Appearance("SeedSource", Enabled:=False)> _
    Public Property SeedSource() As String
        Get
            Return fSeedSource
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SeedSource ", fSeedSource, value)
        End Set
    End Property

    Dim fBags As String
    <Index(6)>
    <XafDisplayName("กระสอบ")> _
    <Appearance("Bags", Enabled:=False)> _
    Public Property Bags() As String
        Get
            Return fBags
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Bags ", fBags, value)
        End Set
    End Property

    Dim fQuantity As String
    <Index(7)>
    <XafDisplayName("น้ำหนัก(กก.)")> _
    <Appearance("Quantity", Enabled:=False)> _
    Public Property Quantity() As String
        Get
            Return fQuantity
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Quantity ", fQuantity, value)
        End Set
    End Property

    Dim fWet As String
    <Index(8)>
    <XafDisplayName("ความชื้น")> _
    <Appearance("Wet", Enabled:=False)> _
    Public Property Wet() As String
        Get
            Return fWet
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Wet ", fWet, value)
        End Set
    End Property

    Dim fGrow As String
    <Index(9)>
    <XafDisplayName("ความงอก")> _
    <Appearance("Grow", Enabled:=False)> _
    Public Property Grow() As String
        Get
            Return fGrow
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Grow ", fGrow, value)
        End Set
    End Property

    Dim fPureSeed As String
    <Index(10)>
    <XafDisplayName("เมล็ดพันธุ์บริสุทธิ์")> _
    <Appearance("PureSeed", Enabled:=False)> _
    Public Property PureSeed() As String
        Get
            Return fPureSeed
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PureSeed ", fPureSeed, value)
        End Set
    End Property

    Dim fOtherSeed As String
    <Index(11)>
    <XafDisplayName("เมล็ดพันธุ์อื่น")> _
    <Appearance("OtherSeed", Enabled:=False)> _
    Public Property OtherSeed() As String
        Get
            Return fOtherSeed
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("OtherSeed ", fOtherSeed, value)
        End Set
    End Property

    Dim fCompound As String
    <Index(12)>
    <XafDisplayName("สิ่งเจือปน")> _
    <Appearance("Compound", Enabled:=False)> _
    Public Property Compound() As String
        Get
            Return fCompound
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Compound ", fCompound, value)
        End Set
    End Property

    Dim fRedSeed As String
    <Index(13)>
    <XafDisplayName("ข้าวแดง")> _
    <Appearance("RedSeed", Enabled:=False)> _
    Public Property RedSeed() As String
        Get
            Return fRedSeed
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("RedSeed ", fRedSeed, value)
        End Set
    End Property

    Dim fStickySeed As String
    <Index(14)>
    <XafDisplayName("ข้าวเหนียว")> _
    <Appearance("StickySeed", Enabled:=False)> _
    Public Property StickySeed() As String
        Get
            Return fStickySeed
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("StickySeed ", fStickySeed, value)
        End Set
    End Property

    Dim fStrong As String
    <Index(15)>
    <XafDisplayName("ความแข็งแรง")> _
    <Appearance("Strong", Enabled:=False)> _
    Public Property Strong() As String
        Get
            Return fStrong
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Strong ", fStrong, value)
        End Set
    End Property

    Dim fCheckDate As DateTime
    <Index(16)>
    <XafDisplayName("วันที่ทดสอบ")> _
    <Appearance("CheckDate", Enabled:=False)> _
    Public Property CheckDate() As DateTime
        Get
            Return fCheckDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("CheckDate", fCheckDate, value)
        End Set
    End Property

    Dim fCheckResult As PublicEnum.AuditCheckResults
    <Index(17)>
    <XafDisplayName("สถานะ")> _
    <Appearance("CheckResult", Enabled:=False)> _
    Public Property CheckResult() As PublicEnum.AuditCheckResults
        Get
            Return fCheckResult
        End Get
        Set(ByVal value As PublicEnum.AuditCheckResults)
            SetPropertyValue(Of PublicEnum.AuditCheckResults)("CheckResult ", fCheckResult, value)
        End Set
    End Property

    Dim fRemark As String
    <Index(18)>
    <XafDisplayName("หมายเหตุ")> _
    <Appearance("Remark", Enabled:=False)> _
    Public Property Remark() As String
        Get
            Return fRemark
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Remark ", fRemark, value)
        End Set
    End Property




End Class
