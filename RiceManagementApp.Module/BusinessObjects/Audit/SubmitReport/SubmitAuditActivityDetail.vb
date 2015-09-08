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
<XafDisplayName("ข้อมูล แผนและผลการดำเนินงาน")> _
Public Class SubmitAuditActivityDetail
    Inherits BaseObject
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

    Dim fSubmitAuditActivity As SubmitAuditActivity
    <Association("SubmitAuditActivity-SubmitAuditActivityDetails")> _
    Public Property SubmitAuditActivity() As SubmitAuditActivity
        Get
            Return fSubmitAuditActivity
        End Get
        Set(ByVal value As SubmitAuditActivity)
            SetPropertyValue(Of SubmitAuditActivity)("SubmitAuditActivity", fSubmitAuditActivity, value)
        End Set
    End Property

    Dim fActivityNo As Integer
    <Index(1)>
    <VisibleInDetailView(True), VisibleInListView(True), VisibleInLookupListView(False)>
<Appearance("ActivityNo", Enabled:=False)> _
    <XafDisplayName("ลำดับ")> _
    Public Property ActivityNo() As Integer
        Get
            Return fActivityNo
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ActivityNo", fActivityNo, value)
        End Set
    End Property

    Dim fActivityName As String
    <Index(2)>
    <VisibleInDetailView(True), VisibleInListView(True), VisibleInLookupListView(False)>
    <Appearance("ActivityName", Enabled:=False)> _
    <XafDisplayName("กิจกรรม")> _
    Public Property ActivityName() As String
        Get
            Return fActivityName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ActivityName", fActivityName, value)
        End Set
    End Property

    Dim fActivityUnit As String
    <Index(3)>
    <VisibleInDetailView(True), VisibleInListView(True), VisibleInLookupListView(False)>
    <XafDisplayName("หน่วย")> _
    <Appearance("ActivityUnit", Enabled:=False)> _
    Public Property ActivityUnit() As String
        Get
            Return fActivityUnit
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ActivityUnit", fActivityUnit, value)
        End Set
    End Property

    Dim fOctValue As Integer
    <Index(4)>
    <VisibleInDetailView(True), VisibleInListView(True), VisibleInLookupListView(False)>
    <XafDisplayName("ต.ค.")> _
    <Appearance("OctValue", Enabled:=False)> _
    Public Property OctValue() As Integer
        Get
            Return fOctValue
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("OctValue", fOctValue, value)
        End Set
    End Property

    Dim fNovValue As Integer
    <Index(5)>
    <VisibleInDetailView(True), VisibleInListView(True), VisibleInLookupListView(False)>
    <XafDisplayName("พ.ย.")> _
    <Appearance("NovValue", Enabled:=False)> _
    Public Property NovValue() As Integer
        Get
            Return fNovValue
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("NovValue", fNovValue, value)
        End Set
    End Property

    Dim fDecValue As Integer
    <Index(6)>
    <VisibleInDetailView(True), VisibleInListView(True), VisibleInLookupListView(False)>
    <XafDisplayName("ธ.ค.")> _
    <Appearance("DecValue", Enabled:=False)> _
    Public Property DecValue() As Integer
        Get
            Return fDecValue
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("DecValue", fDecValue, value)
        End Set
    End Property

    Dim fJanValue As Integer
    <Index(7)>
    <VisibleInDetailView(True), VisibleInListView(True), VisibleInLookupListView(False)>
    <XafDisplayName("ม.ค.")> _
    <Appearance("JanValue", Enabled:=False)> _
    Public Property JanValue() As Integer
        Get
            Return fJanValue
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("JanValue", fJanValue, value)
        End Set
    End Property

    Dim fFebValue As Integer
    <Index(8)>
    <VisibleInDetailView(True), VisibleInListView(True), VisibleInLookupListView(False)>
    <XafDisplayName("ก.พ.")> _
    <Appearance("FebValue", Enabled:=False)> _
    Public Property FebValue() As Integer
        Get
            Return fFebValue
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("FebValue", fFebValue, value)
        End Set
    End Property

    Dim fMarValue As Integer
    <Index(9)>
    <VisibleInDetailView(True), VisibleInListView(True), VisibleInLookupListView(False)>
    <XafDisplayName("มี.ค.")> _
    <Appearance("MarValue", Enabled:=False)> _
    Public Property MarValue() As Integer
        Get
            Return fMarValue
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("MarValue", fMarValue, value)
        End Set
    End Property

    Dim fAprValue As Integer
    <Index(10)>
    <VisibleInDetailView(True), VisibleInListView(True), VisibleInLookupListView(False)>
    <XafDisplayName("เม.ย.")> _
    <Appearance("AprValue", Enabled:=False)> _
    Public Property AprValue() As Integer
        Get
            Return fAprValue
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("AprValue", fAprValue, value)
        End Set
    End Property

    Dim fMayValue As Integer
    <Index(11)>
    <VisibleInDetailView(True), VisibleInListView(True), VisibleInLookupListView(False)>
    <XafDisplayName("พ.ค.")> _
    <Appearance("MayValue", Enabled:=False)> _
    Public Property MayValue() As Integer
        Get
            Return fMayValue
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("MayValue", fMayValue, value)
        End Set
    End Property

    Dim fJunValue As Integer
    <Index(12)>
    <VisibleInDetailView(True), VisibleInListView(True), VisibleInLookupListView(False)>
    <XafDisplayName("มิ.ย.")> _
    <Appearance("JunValue", Enabled:=False)> _
    Public Property JunValue() As Integer
        Get
            Return fJunValue
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("JunValue", fJunValue, value)
        End Set
    End Property

    Dim fJulValue As Integer
    <Index(13)>
    <VisibleInDetailView(True), VisibleInListView(True), VisibleInLookupListView(False)>
    <XafDisplayName("ก.ค.")> _
    <Appearance("JulValue", Enabled:=False)> _
    Public Property JulValue() As Integer
        Get
            Return fJulValue
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("JulValue", fJulValue, value)
        End Set
    End Property

    Dim fAugValue As Integer
    <Index(14)>
    <VisibleInDetailView(True), VisibleInListView(True), VisibleInLookupListView(False)>
    <XafDisplayName("ส.ค.")> _
    <Appearance("AugValue", Enabled:=False)> _
    Public Property AugValue() As Integer
        Get
            Return fAugValue
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("AugValue", fAugValue, value)
        End Set
    End Property

    Dim fSepValue As Integer
    <Index(15)>
    <VisibleInDetailView(True), VisibleInListView(True), VisibleInLookupListView(False)>
    <XafDisplayName("ก.ย.")> _
    <Appearance("SepValue", Enabled:=False)> _
    Public Property SepValue() As Integer
        Get
            Return fSepValue
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("SepValue", fSepValue, value)
        End Set
    End Property

    Dim fTotal As Integer
    <Index(16)>
    <VisibleInDetailView(True), VisibleInListView(True), VisibleInLookupListView(False)>
    <XafDisplayName("รวม")> _
    <Appearance("Total", Enabled:=False)> _
    Public Property Total() As Integer
        Get
            Return fTotal
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Total", fTotal, value)
        End Set
    End Property

    Dim fDifferrence As Integer
    <Index(17)>
    <VisibleInDetailView(True), VisibleInListView(True), VisibleInLookupListView(False)>
    <XafDisplayName("ผลต่าง")> _
    <Appearance("Differrence", Enabled:=False)> _
    Public Property Differrence() As Integer
        Get
            Return fDifferrence
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Differrence", fDifferrence, value)
        End Set
    End Property

End Class
