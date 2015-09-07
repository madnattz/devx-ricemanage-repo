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

<NonPersistent()> _
Public Class SeedQualityHistory
    Inherits BaseObject
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()

    End Sub

#Region "เพื่อจัดซื้อ"
    Dim fBuyWet As String
    Public Property BuyWet() As String
        Get
            Return fBuyWet
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("BuyWet", fBuyWet, value)
        End Set
    End Property

    Dim fBuyGrow As String
    Public Property BuyGrow() As String
        Get
            Return fBuyGrow
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("BuyGrow", fBuyGrow, value)
        End Set
    End Property

    Dim fBuyRedSeed As String
    Public Property BuyRedSeed() As String
        Get
            Return fBuyRedSeed
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("BuyRedSeed", fBuyRedSeed, value)
        End Set
    End Property

    Dim fBuyOtherSeed As String
    Public Property BuyOtherSeed() As String
        Get
            Return fBuyOtherSeed
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("BuyOtherSeed", fBuyOtherSeed, value)
        End Set
    End Property

    Dim fBuyLabDate As String
    Public Property BuyLabDate() As String
        Get
            Return fBuyLabDate
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("BuyLabDate", fBuyLabDate, value)
        End Set
    End Property

#End Region

#Region "ก่อนการปรับปรุงสภาพ"
    Dim fBeforeWet As String
    Public Property BeforeWet() As String
        Get
            Return fBeforeWet
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("BeforeWet", fBeforeWet, value)
        End Set
    End Property

    Dim fBeforeGrow As String
    Public Property BeforeGrow() As String
        Get
            Return fBeforeGrow
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("BeforeGrow", fBeforeGrow, value)
        End Set
    End Property

    Dim fBeforeCompound As String
    Public Property BeforeCompound() As String
        Get
            Return fBeforeCompound
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("BeforeCompound", fBeforeCompound, value)
        End Set
    End Property

    Dim fBeforeLabDate As String
    Public Property BeforeLabDate() As String
        Get
            Return fBeforeLabDate
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("BeforeLabDate", fBeforeLabDate, value)
        End Set
    End Property

#End Region

#Region "หลังการปรับปรุงสภาพ"
    Dim fAfterWet As String
    Public Property AfterWet() As String
        Get
            Return fAfterWet
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AfterWet", fAfterWet, value)
        End Set
    End Property

    Dim fAfterGrow As String
    Public Property AfterGrow() As String
        Get
            Return fAfterGrow
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AfterGrow", fAfterGrow, value)
        End Set
    End Property

    Dim fAfterStrong As String
    Public Property AfterStrong() As String
        Get
            Return fAfterStrong
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AfterStrong", fAfterStrong, value)
        End Set
    End Property

    Dim fAfterPureSeed As String
    Public Property AfterPureSeed() As String
        Get
            Return fAfterPureSeed
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AfterPureSeed", fAfterPureSeed, value)
        End Set
    End Property

    Dim fAfterCompound As String
    Public Property AfterCompound() As String
        Get
            Return fAfterCompound
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AfterCompound", fAfterCompound, value)
        End Set
    End Property

    Dim fAfterRedSeed As String
    Public Property AfterRedSeed() As String
        Get
            Return fAfterRedSeed
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AfterRedSeed", fAfterRedSeed, value)
        End Set
    End Property

    Dim fAfterOtherSeed As String
    Public Property AfterOtherSeed() As String
        Get
            Return fAfterOtherSeed
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AfterOtherSeed", fAfterOtherSeed, value)
        End Set
    End Property

    Dim fAfterSeedWeight As Double
    Public Property AfterSeedWeight() As Double
        Get
            Return fAfterSeedWeight
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("AfterSeedWeight", fAfterSeedWeight, value)
        End Set
    End Property

    Dim fAfterLabDate As String
    Public Property AfterLabDate() As String
        Get
            Return fAfterLabDate
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AfterLabDate", fAfterLabDate, value)
        End Set
    End Property

#End Region

End Class
