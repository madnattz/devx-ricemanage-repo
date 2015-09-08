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
Public Class SubmitCheckFarmDetail
    Inherits BaseObject
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

    Dim fSubmitCheckFarm As SubmitCheckFarm
    <Association("SubmitCheckFarm-SubmitCheckFarmDetails")> _
    Public Property SubmitCheckFarm() As SubmitCheckFarm
        Get
            Return fSubmitCheckFarm
        End Get
        Set(ByVal value As SubmitCheckFarm)
            SetPropertyValue(Of SubmitCheckFarm)("SubmitCheckFarm", fSubmitCheckFarm, value)
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

    Dim fFarmerName As String
    <Index(2)> _
    <XafDisplayName("ชื่อ - นามสกุล")> _
    <Appearance("FarmerName", Enabled:=False)> _
    <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property FarmerName() As String
        Get
            Return fFarmerName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FarmerName", fFarmerName, value)
        End Set
    End Property

    Dim fGrowAreaSize As String
    <Index(3)> _
    <XafDisplayName("พื้นที่ปลูก (ไร่)")> _
    <Appearance("GrowAreaSize", Enabled:=False)> _
    <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property GrowAreaSize() As String
        Get
            Return fGrowAreaSize
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("GrowAreaSize", fGrowAreaSize, value)
        End Set
    End Property

    Dim fEstimateResultPerArea As String
    <Index(4)> _
    <XafDisplayName("ประเมินผลผลิต (กก./ไร่)")> _
    <Appearance("EstimateResultPerArea", Enabled:=False)> _
    <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property EstimateResultPerArea() As String
        Get
            Return fEstimateResultPerArea
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("EstimateResultPerArea ", fEstimateResultPerArea, value)
        End Set
    End Property

    Dim fCheckPeriod As String
    <Index(5)> _
    <XafDisplayName("ระยะที่ตรวจ")> _
    <Appearance("CheckPeriod", Enabled:=False)> _
    <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property CheckPeriod() As String
        Get
            Return fCheckPeriod
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CheckPeriod ", fCheckPeriod, value)
        End Set
    End Property

    Dim fGrowTypeString As String
    <Index(6)> _
    <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    <XafDisplayName("วิธีปลูก")> _
    <Appearance("GrowTypeString", Enabled:=False)> _
    Public Property GrowTypeString() As String
        Get
            Return fGrowTypeString
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("GrowTypeString ", fGrowTypeString, value)
        End Set
    End Property

    Dim fRandom1 As String
    <Index(7)> _
    <XafDisplayName("จุดที่ 1")> _
    <Appearance("Random1", Enabled:=False)> _
    <VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)> _
    Public Property Random1() As String
        Get
            Return fRandom1
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Random1 ", fRandom1, value)
        End Set
    End Property

    Dim fRandom2 As String
    <Index(8)> _
    <XafDisplayName("จุดที่ 2")> _
    <Appearance("Random2", Enabled:=False)> _
    <VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)> _
    Public Property Random2() As String
        Get
            Return fRandom2
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Random2", fRandom2, value)
        End Set
    End Property

    Dim fRandom3 As String
    <Index(9)> _
    <XafDisplayName("จุดที่ 3")> _
    <Appearance("Random3", Enabled:=False)> _
    <VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)> _
    Public Property Random3() As String
        Get
            Return fRandom3
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Random3 ", fRandom3, value)
        End Set
    End Property

    Dim fRandom4 As String
    <Index(10)> _
    <XafDisplayName("จุดที่ 4")> _
    <Appearance("Random4", Enabled:=False)> _
    <VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)> _
    Public Property Random4() As String
        Get
            Return fRandom4
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Random4 ", fRandom4, value)
        End Set
    End Property

    Dim fRandom5 As String
    <Index(11)> _
    <XafDisplayName("จุดที่ 5")> _
    <Appearance("Random5", Enabled:=False)> _
    <VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)> _
    Public Property Random5() As String
        Get
            Return fRandom5
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Random5 ", fRandom5, value)
        End Set
    End Property

    Dim fOtherSeedValue As Double
    <Index(12)> _
    <XafDisplayName("พันธุ์ปน")> _
    <Appearance("OtherSeedValue", Enabled:=False)> _
    <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property OtherSeedValue() As Double
        Get
            Return fOtherSeedValue
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("OtherSeedValue ", fOtherSeedValue, value)
        End Set
    End Property

    Dim fRedSeedValue As Double
    <Index(13)> _
    <XafDisplayName("ข้าวแดง")> _
    <Appearance("RedSeedValue", Enabled:=False)> _
    <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property RedSeedValue() As Double
        Get
            Return fRedSeedValue
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("RedSeedValue ", fRedSeedValue, value)
        End Set
    End Property

    Dim fDisease As String
    <Index(14)> _
    <XafDisplayName("โรค")> _
    <Appearance("Disease", Enabled:=False)> _
    <VisibleInListView(False), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property Disease() As String
        Get
            Return fDisease
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Disease ", fDisease, value)
        End Set
    End Property

    Dim fBug As String
    <Index(15)> _
    <XafDisplayName("แมลง")> _
    <Appearance("Bug", Enabled:=False)> _
    <VisibleInListView(False), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property Bug() As String
        Get
            Return fBug
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Bug ", fBug, value)
        End Set
    End Property

    Dim fWeed As String
    <Index(16)> _
    <XafDisplayName("วัชพืช")> _
    <Appearance("Weed", Enabled:=False)> _
    <VisibleInListView(False), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property Weed() As String
        Get
            Return fWeed
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Weed  ", fWeed, value)
        End Set
    End Property

    Dim fBroken As String
    <Index(17)> _
    <XafDisplayName("หักล้ม")> _
    <Appearance("Broken", Enabled:=False)> _
    <VisibleInListView(False), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property Broken() As String
        Get
            Return fBroken
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Broken  ", fBroken, value)
        End Set
    End Property

    Dim fDistance As String
    <Index(18)> _
    <XafDisplayName("ระยะ")> _
    <Appearance("Distance", Enabled:=False)> _
    <VisibleInListView(False), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property Distance() As String
        Get
            Return fDistance
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Distance  ", fDistance, value)
        End Set
    End Property

    Dim fDamageArea As String
    <Index(19)> _
    <XafDisplayName("เสียหาย (ไร่)")> _
    <Appearance("DamageArea", Enabled:=False)> _
     <VisibleInListView(False), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property DamageArea() As String
        Get
            Return fDamageArea
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("DamageArea  ", fDamageArea, value)
        End Set
    End Property

    Dim fCheckDate As String
    <Index(20)> _
    <XafDisplayName("วันที่ตรวจ")> _
    <Appearance("CheckDate", Enabled:=False)> _
    <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property CheckDate() As String
        Get
            Return fCheckDate
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CheckDate   ", fCheckDate, value)
        End Set
    End Property

    Dim fCheckResults As PublicEnum.CheckResults
    <Index(21)> _
    <XafDisplayName("ผลการตรวจ")> _
    <Appearance("CheckResults", Enabled:=False)> _
    <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property CheckResults() As PublicEnum.CheckResults
        Get
            Return fCheckResults
        End Get
        Set(ByVal value As PublicEnum.CheckResults)
            SetPropertyValue(Of PublicEnum.CheckResults)("CheckResults   ", fCheckResults, value)
        End Set
    End Property

    Dim fRemark As String
    <Index(22)> _
    <XafDisplayName("หมายเหตุ")> _
    <Appearance("Remark", Enabled:=False)> _
    <VisibleInListView(False), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property Remark() As String
        Get
            Return fRemark
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Remark   ", fRemark, value)
        End Set
    End Property

End Class
