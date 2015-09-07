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

<XafDisplayName("ข้อมูลการปรับปรุงสภาพรายวัน")> _
<ImageName("BO_List")> _
<DefaultClassOptions()> _
Public Class FactorySeedProcessToday
    Inherits BaseObject

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        DataOwner = GetCurrentSite()
    End Sub

    Protected Overrides Sub OnSaving()
        MyBase.OnSaving()
        If fFactorySeedProcess IsNot Nothing Then
            fFactorySeedProcess.UpdateSeedProcessAmount(True)
        End If
        If Me.DataOwner Is Nothing Then
            DataOwner = GetCurrentSite()
        End If
    End Sub

    Protected Overrides Sub OnDeleted()
        MyBase.OnDeleted()
        If fFactorySeedProcess IsNot Nothing Then
            fFactorySeedProcess.UpdateSeedProcessAmount(True)
        End If
    End Sub

    Dim fDataOwner As Site
    <Browsable(False)> _
    Public Property DataOwner() As Site
        Get
            Return fDataOwner
        End Get
        Set(ByVal value As Site)
            SetPropertyValue(Of Site)("DataOwner", fDataOwner, value)
        End Set
    End Property

    Public Function GetCurrentSite() As Site
        Dim siteSetting As SiteSetting = Session.FindObject(Of SiteSetting)(Nothing)
        If siteSetting IsNot Nothing Then
            If siteSetting.Site IsNot Nothing Then
                Return siteSetting.Site
            Else
                Return Nothing
            End If
            Return siteSetting.Site
        Else
            Return Nothing
        End If
    End Function

    Dim fFactorySeedProcess As FactorySeedProcess
    <VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)> _
    <Association("FactorySeedProcessTodays-FactorySeedProcess")> _
    <ImmediatePostData> _
    Public Property FactorySeedProcess() As FactorySeedProcess
        Get
            Return fFactorySeedProcess
        End Get
        Set(ByVal value As FactorySeedProcess)
            Dim oldFactorySeedProcess As FactorySeedProcess = fFactorySeedProcess
            SetPropertyValue(Of FactorySeedProcess)("FactorySeedProcess", fFactorySeedProcess, value)
            If (IsLoading) AndAlso (IsSaving) AndAlso oldFactorySeedProcess IsNot fFactorySeedProcess Then
                If (oldFactorySeedProcess IsNot Nothing) Then
                    oldFactorySeedProcess = oldFactorySeedProcess
                Else
                    oldFactorySeedProcess = fFactorySeedProcess
                End If
                oldFactorySeedProcess.UpdateSeedProcessAmount(True)
            End If
        End Set
    End Property

    Dim fProcessDate As DateTime
    <Index(1)> _
    <XafDisplayName("วันที่ปรับปรุงสภาพ")> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property ProcessDate() As DateTime
        Get
            Return fProcessDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ProcessDate", fProcessDate, value)
        End Set
    End Property

    Dim fSeedProcessAmount As Integer
    <Index(2)> _
    <XafDisplayName("น้ำหนักเมล็ดพันธุ์ดีที่ผลิตได้ (กก.)")> _
    <ImmediatePostData> _
    <RuleValueComparison(DefaultContexts.Save, ValueComparisonType.GreaterThan, 0)> _
    Public Property SeedProcessAmount() As Integer
        Get
            Return fSeedProcessAmount
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("SeedProcessAmount", fSeedProcessAmount, value)
            FactorySeedProcess.UpdateSeedProcessAmount(True)
            Try
                If (Not IsLoading) AndAlso (Not IsSaving) AndAlso SeedProcessAmount Then
                    FactorySeedProcess.UpdateSeedProcessAmount(True)
                End If
            Catch ex As Exception
            End Try
        End Set
    End Property

    Private Sub Reset()
        fSeedProcessAmount = Nothing
    End Sub

    Dim fNormalTime As Boolean
    <Index(3)> _
    <XafDisplayName("เวลาปกติ")> _
    Public Property NormalTime() As Boolean
        Get
            Return fNormalTime
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("NormalTime", fNormalTime, value)
        End Set
    End Property

    Dim fOverTime As Boolean
    <Index(4)> _
    <XafDisplayName("ล่วงเวลา")> _
    Public Property OverTime() As Boolean
        Get
            Return fOverTime
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("OverTime", fOverTime, value)
        End Set
    End Property

    Dim fShiftTime As Boolean
    <Index(5)> _
    <XafDisplayName("2 ผลัด")> _
    Public Property ShiftTime() As Boolean
        Get
            Return fShiftTime
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("ShiftTime", fShiftTime, value)
        End Set
    End Property

    Dim fStartTime As String
    <Index(6)> _
    <XafDisplayName("เวลาเริ่ม")> _
    <Size(10)> _
    Public Property StartTime() As String
        Get
            Return fStartTime
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("StartTime", fStartTime, value)
        End Set
    End Property

    Dim fEndTime As String
    <Index(7)> _
    <XafDisplayName("เวลาเสร็จ")> _
    <Size(10)> _
    Public Property EndTime() As String
        Get
            Return fEndTime
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("EndTime", fEndTime, value)
        End Set
    End Property

    Dim fImproveComplete As Boolean
    <Index(8)> _
    <XafDisplayName("เสร็จสิ้นการปรับปรุงสภาพ")> _
    Public Property ImproveComplete() As Boolean
        Get
            Return fImproveComplete
        End Get
        Set(value As Boolean)
            SetPropertyValue(Of Boolean)("ImproveComplete", fImproveComplete, value)
        End Set
    End Property

    <VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    Public ReadOnly Property FactoryNo() As Integer
        Get
            Return CInt(FactorySeedProcess.FactoryNo)
        End Get
    End Property


End Class
