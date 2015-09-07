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

Public Class SubmitQualityBetweenStorageDetail ' Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

    Dim fSubmitQualityBetweenStorage As SubmitQualityBetweenStorage
    <Association("SubmitQualityBetweenStorage-SubmitQualityBetweenStorageDetails")> _
    Public Property SubmitQualityBetweenStorage() As SubmitQualityBetweenStorage
        Get
            Return fSubmitQualityBetweenStorage
        End Get
        Set(ByVal value As SubmitQualityBetweenStorage)
            SetPropertyValue(Of SubmitQualityBetweenStorage)("SubmitQualityBetweenStorage", fSubmitQualityBetweenStorage, value)
        End Set
    End Property

    Dim _PlantName As String
    <XafDisplayName("พืช")> _
    <ImmediatePostData()> _
    Public Property PlantName() As String
        Get
            Return _PlantName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PlantName", _PlantName, value)

        End Set
    End Property


    Dim _SeedTypeName As String
    <XafDisplayName("พันธุ์")> _
    <ImmediatePostData()> _
    Public Property SeedTypeName() As String
        Get
            Return _SeedTypeName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SeedTypeName", _SeedTypeName, value)

        End Set
    End Property


    Dim _SeedClassName As String
    <XafDisplayName("ชั้นพันธุ์")> _
    <ImmediatePostData()> _
    Public Property SeedClassName() As String
        Get
            Return _SeedClassName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SeedClassName", _SeedClassName, value)

        End Set

    End Property

    Dim _SeasonName As String
    <XafDisplayName("ฤดู")> _
    <ImmediatePostData()> _
    Public Property SeasonName() As String
        Get
            Return _SeasonName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SeasonName", _SeasonName, value)
        End Set

    End Property

    Dim _SeedYear As String
    <XafDisplayName("ปี")> _
    <ImmediatePostData()> _
    Public Property SeedYear() As String
        Get
            Return _SeedYear
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SeedYear", _SeedYear, value)
        End Set

    End Property

    Dim _SeedLot As String
    <XafDisplayName("ล็อตที่")> _
    <ImmediatePostData()> _
    Public Property SeedLot() As String
        Get
            Return _SeedLot
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SeedLot", _SeedLot, value)
        End Set

    End Property

    Dim _BuyDate As Date
    <XafDisplayName(" เดือน/ปี ที่ซื้อคืน")> _
    <ImmediatePostData()> _
    Public Property BuyDate() As Date
        Get
            Return _BuyDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue(Of Date)("BuyDate", _BuyDate, value)
        End Set

    End Property

    Dim _DiffMonthBuy As String
    <XafDisplayName("จำนวนเดือน ที่เก็บรักษา")> _
    <ImmediatePostData()> _
    Public Property DiffMonthBuy() As String
        Get
            Return _DiffMonthBuy
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("DiffMonthBuy", _DiffMonthBuy, value)
        End Set

    End Property

    Dim _ProcessDate As Date
    <XafDisplayName("เดือน/ปี ที่ปรับปรุงสภาพ")> _
    <ImmediatePostData()> _
    Public Property ProcessDate() As Date
        Get
            Return _ProcessDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue(Of Date)("ProcessDate", _ProcessDate, value)
        End Set

    End Property

    Dim _DiffProcess As String
    <XafDisplayName("จำนวนเดือนที่เก็บรักษา")> _
    <ImmediatePostData()> _
    Public Property DiffProcess() As String
        Get
            Return _DiffProcess
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("DiffProcess", _DiffProcess, value)
        End Set

    End Property

    Dim _Wet As String
    <XafDisplayName("ความชื้น (%)")> _
    <ImmediatePostData()> _
    Public Property Wet() As String
        Get
            Return _Wet
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Wet", _Wet, value)
        End Set

    End Property

    Dim _Grow As String
    <XafDisplayName("ความงอก(%)")> _
    <ImmediatePostData()> _
    Public Property Grow() As String
        Get
            Return _Grow
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Grow", _Grow, value)
        End Set

    End Property

    Dim _Strong As String
    <XafDisplayName("ความแข็งแรง(%)")> _
    <ImmediatePostData()> _
    Public Property Strong() As String
        Get
            Return _Strong
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Strong", _Strong, value)
        End Set

    End Property

    Dim _SeedWeight As String
    <XafDisplayName("น้ำหนักเมล็ดพันธุ์(กก.)")> _
    <ImmediatePostData()> _
    Public Property SeedWeight() As String
        Get
            Return _SeedWeight
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SeedWeight", _SeedWeight, value)
        End Set

    End Property

    Dim _LabDate As Date
    <XafDisplayName("วันที่ตรวจสอบ")> _
    <ImmediatePostData()> _
    Public Property LabDate() As Date
        Get
            Return _LabDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue(Of Date)("LabDate", _LabDate, value)
        End Set

    End Property

    Dim _Remark As String
    <XafDisplayName("หมายเหตุ")> _
    <ImmediatePostData()> _
    Public Property Remark() As String
        Get
            Return _Remark
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Remark", _Remark, value)
        End Set

    End Property

End Class
