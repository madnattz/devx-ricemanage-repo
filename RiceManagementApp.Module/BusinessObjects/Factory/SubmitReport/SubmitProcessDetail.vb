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

<ImageName("BO_List")> _
<DefaultClassOptions()> _
Public Class SubmitProcessDetail
    Inherits BaseObject
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

    Dim fSubmitProcess As SubmitProcess
    <Association("SubmitProcess-SubmitProcessDetails")> _
    Public Property SubmitProcess() As SubmitProcess
        Get
            Return fSubmitProcess
        End Get
        Set(ByVal value As SubmitProcess)
            SetPropertyValue(Of SubmitProcess)("SubmitProcess", fSubmitProcess, value)
        End Set
    End Property

    Dim fProcessDate As Date
    <Index(1)> _
    <XafDisplayName("วันที่ปรับปรุงสภาพ")> _
    <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property ProcessDate() As Date
        Get
            Return fProcessDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue(Of Date)("ProcessDate", fProcessDate, value)
        End Set
    End Property

    Dim fPlantName As String
    <Index(2)> _
    <XafDisplayName("พืช")> _
    <VisibleInListView(False), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property PlantName() As String
        Get
            Return fPlantName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PlantName", fPlantName, value)
        End Set
    End Property

    Dim fSeedName As String
    <Index(3)> _
    <XafDisplayName("พันธุ์")> _
    <VisibleInListView(False), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property SeedName() As String
        Get
            Return fSeedName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SeedName", fSeedName, value)
        End Set
    End Property

    Dim fClassName As String
    <Index(4)> _
    <XafDisplayName("ชั้นพันธุ์")> _
    <VisibleInListView(False), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property ClassName() As String
        Get
            Return fClassName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ClassName", fClassName, value)
        End Set
    End Property

    Dim fSeasonName As String
    <Index(5)> _
    <XafDisplayName("ฤดู")> _
    <VisibleInListView(False), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property SeasonName() As String
        Get
            Return fSeasonName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SeasonName", fSeasonName, value)
        End Set
    End Property

    Dim fSeedYear As String
    <Index(6)> _
    <XafDisplayName("ปี")> _
    <ModelDefault("PropertyEditorType", "RiceManagementApp.Module.Win.CustomYearDropdown")> _
    <VisibleInListView(False), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property SeedYear() As String
        Get
            Return fSeedYear
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SeedYear", fSeedYear, value)
        End Set
    End Property

    Dim fLot As String
    <XafDisplayName("รุ่นที่")> _
    <RuleRequiredField(DefaultContexts.Save)> _
    <ModelDefault("PropertyEditorType", "RiceManagementApp.Module.Win.CustomLotDropdown")> _
    Public Property Lot() As String
        Get
            Return fLot
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Lot", fLot, value)
        End Set
    End Property

    <XafDisplayName("เมล็ดพันธุ์")> _
    <VisibleInListView(True), VisibleInDetailView(False), VisibleInLookupListView(False)> _
    Public ReadOnly Property SeedProcessFullName As String
        Get
            Dim ret As String = ""
            Try
                ret &= PlantName & " " & SeedName & " ชั้นพันธู์ " & ClassName & " ฤดู " & SeasonName & " ปี " & SeedYear & " รุ่น " & Lot
            Catch ex As Exception

            End Try

            Return ret
        End Get
    End Property

    Dim fOutputQuantity As Integer
    <Index(7)> _
    <XafDisplayName("น้ำหนักเมล็ดพันธุ์ดีที่ผลิตได้(กก.)")> _
    <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    <ModelDefault("DisPlayFormat", ("{0:#,##}"))> _
    Public Property OutputQuantity() As Integer
        Get
            Return fOutputQuantity
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("OutputQuantity", fOutputQuantity, value)
        End Set
    End Property

    Dim fNormalTime As Boolean
    <Index(8)> _
    <XafDisplayName("เวลาปกติ")> _
    <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property NormalTime() As Boolean
        Get
            Return fNormalTime
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("NormalTime", fNormalTime, value)
        End Set
    End Property

    Dim fOverTime As Boolean
    <Index(9)> _
    <XafDisplayName("ล่วงเวลา")> _
    <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property OverTime() As Boolean
        Get
            Return fOverTime
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("OverTime", fOverTime, value)
        End Set
    End Property

    Dim fShiftTime As Boolean
    <Index(10)> _
    <XafDisplayName("2 ผลัด")> _
    <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property ShiftTime() As Boolean
        Get
            Return fShiftTime
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("ShiftTime", fShiftTime, value)
        End Set
    End Property

    Dim fStartTime As String
    <Index(11)> _
    <XafDisplayName("เวลาเริ่ม")> _
    <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property StartTime() As String
        Get
            Return fStartTime
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("StartTime", fStartTime, value)
        End Set
    End Property

    Dim fCompleteTime As String
    <Index(12)> _
    <XafDisplayName("เวลาเสร็จ")> _
    <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property CompleteTime() As String
        Get
            Return fCompleteTime
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CompleteTime ", fCompleteTime, value)
        End Set
    End Property

    Dim fImproveComplete As Boolean
    <Index(13)> _
    <XafDisplayName("เสร็จสิ้นการปรับปรุงสภาพ")> _
    <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property ImproveComplete() As Boolean
        Get
            Return fImproveComplete
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("ImproveComplete", fImproveComplete, value)
        End Set
    End Property

    Dim fFactoryName As String
    <Index(14)> _
    <XafDisplayName("โรงงาน")> _
    <VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property FactoryName() As String
        Get
            Return fFactoryName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FactoryName", fFactoryName, value)
        End Set
    End Property

End Class
