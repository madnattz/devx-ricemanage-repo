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
Public Class SubmitCheckFarmSummaryDetail
    Inherits BaseObject
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

    Dim fSubmitCheckFarmSummary As SubmitCheckFarmSummary
    <Association("SubmitCheckFarmSummary-SubmitCheckFarmSummaryDetails")> _
    Public Property SubmitCheckFarmSummary() As SubmitCheckFarmSummary
        Get
            Return fSubmitCheckFarmSummary
        End Get
        Set(ByVal value As SubmitCheckFarmSummary)
            SetPropertyValue(Of SubmitCheckFarmSummary)("SubmitCheckFarmSummary", fSubmitCheckFarmSummary, value)
        End Set
    End Property

    Dim fMainPlan As MainPlan
    <Index(1)>
    <XafDisplayName("เป้าการผลิต")> _
    <Appearance("MainPlan", Enabled:=False)> _
    <VisibleInDetailView(True), VisibleInListView(True), VisibleInLookupListView(False)> _
    Public Property MainPlan() As MainPlan
        Get
            Return fMainPlan
        End Get
        Set(ByVal value As MainPlan)
            SetPropertyValue(Of MainPlan)("MainPlan ", fMainPlan, value)
        End Set
    End Property

    Dim fMonthNo As PublicEnum.EnumMonth
    <Index(2)>
    <XafDisplayName("เดือน")> _
    <Appearance("MonthNo", Enabled:=False)> _
    <VisibleInDetailView(True), VisibleInListView(True), VisibleInLookupListView(False)> _
    Public Property MonthNo() As PublicEnum.EnumMonth
        Get
            Return fMonthNo
        End Get
        Set(ByVal value As PublicEnum.EnumMonth)
            SetPropertyValue(Of PublicEnum.EnumMonth)("MonthNo", fMonthNo, value)
        End Set
    End Property

    Dim fPlanYear As String
    <Index(3)>
    <XafDisplayName("ปี")> _
    <Appearance("PlanYear", Enabled:=False)> _
    <VisibleInDetailView(True), VisibleInListView(True), VisibleInLookupListView(False)> _
    Public Property PlanYear() As String
        Get
            Return fPlanYear
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PlanYear", fPlanYear, value)
        End Set
    End Property

    Dim fAreaSize As Integer
    <Index(4)>
    <XafDisplayName("แผนตรวแปลง (ไร่)")> _
    <Appearance("AreaSize", Enabled:=False)> _
    <VisibleInDetailView(True), VisibleInListView(True), VisibleInLookupListView(False)> _
    Public Property AreaSize() As Integer
        Get
            Return fAreaSize
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("AreaSize", fAreaSize, value)
        End Set
    End Property

    Dim fActualAreaSize As Integer
    <Index(5)>
    <XafDisplayName("ผล พื้นที่ตรวจแปลง(ไร่)")> _
    <Appearance("ActualAreaSize", Enabled:=False)> _
    <VisibleInDetailView(True), VisibleInListView(True), VisibleInLookupListView(False)> _
    Public Property ActualAreaSize() As Integer
        Get
            Return fActualAreaSize
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ActualAreaSize", fActualAreaSize, value)
        End Set
    End Property

    Dim fActualPassArea As Integer
    <Index(6)>
    <XafDisplayName("ผล แปลงที่ผ่านมาตรฐาน(ไร่)")> _
    <Appearance("ActualPassArea", Enabled:=False)> _
    <VisibleInDetailView(True), VisibleInListView(True), VisibleInLookupListView(False)> _
    Public Property ActualPassArea() As Integer
        Get
            Return fActualPassArea
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ActualPassArea", fActualPassArea, value)
        End Set
    End Property

    Dim fFullDamageArea As Integer
    <Index(7)>
    <XafDisplayName("ผล เสียหายก่อนตรวจ(ไร่)")> _
    <Appearance("FullDamageArea", Enabled:=False)> _
    <VisibleInDetailView(True), VisibleInListView(True), VisibleInLookupListView(False)> _
    Public Property FullDamageArea() As Integer
        Get
            Return fFullDamageArea
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("FullDamageArea", fFullDamageArea, value)
        End Set
    End Property

    Dim fFailReason As String
    <Index(8)>
    <XafDisplayName("สาเหตุที่ไม่ผ่าน")> _
    <Appearance("FailReason", Enabled:=False)> _
    <VisibleInDetailView(True), VisibleInListView(True), VisibleInLookupListView(False)> _
    Public Property FailReason() As String
        Get
            Return fFailReason
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FailReason", fFailReason, value)
        End Set
    End Property

End Class