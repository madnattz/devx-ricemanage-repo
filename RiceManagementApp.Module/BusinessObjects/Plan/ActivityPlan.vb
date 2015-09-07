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
<DefaultClassOptions()> _
Public Class ActivityPlan
    Inherits BaseObject
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

    Dim fMainPlan As MainPlan
    <XafDisplayName("เป้าการผลิต")> _
    Public Property MainPlan As MainPlan
        Get
            Return fMainPlan
        End Get
        Set(ByVal value As MainPlan)
            SetPropertyValue(Of MainPlan)("MainPlan", fMainPlan, value)
        End Set
    End Property

    Dim fOCT As ActivePlanDetailObject
    <XafDisplayName("ต.ค.")> _
    Public Property OCT As ActivePlanDetailObject
        Get
            Return fOCT
        End Get
        Set(ByVal value As ActivePlanDetailObject)
            SetPropertyValue(Of ActivePlanDetailObject)("OCT", fOCT, value)
        End Set
    End Property

    Dim fNOV As ActivePlanDetailObject
    <XafDisplayName("พ.ย.")> _
    Public Property NOV As ActivePlanDetailObject
        Get
            Return fNOV
        End Get
        Set(ByVal value As ActivePlanDetailObject)
            SetPropertyValue(Of ActivePlanDetailObject)("NOV", fNOV, value)
        End Set
    End Property

    Dim fDEC As ActivePlanDetailObject
    <XafDisplayName("ธ.ค.")> _
    Public Property DEC As ActivePlanDetailObject
        Get
            Return fDEC
        End Get
        Set(ByVal value As ActivePlanDetailObject)
            SetPropertyValue(Of ActivePlanDetailObject)("DEC", fDEC, value)
        End Set
    End Property

    Dim fJAN As ActivePlanDetailObject
    <XafDisplayName("ม.ค.")> _
    Public Property JAN As ActivePlanDetailObject
        Get
            Return fJAN
        End Get
        Set(ByVal value As ActivePlanDetailObject)
            SetPropertyValue(Of ActivePlanDetailObject)("JAN", fJAN, value)
        End Set
    End Property

    Dim fFEB As ActivePlanDetailObject
    <XafDisplayName("ก.พ.")> _
    Public Property FEB As ActivePlanDetailObject
        Get
            Return fFEB
        End Get
        Set(ByVal value As ActivePlanDetailObject)
            SetPropertyValue(Of ActivePlanDetailObject)("FEB", fFEB, value)
        End Set
    End Property

    Dim fMAR As ActivePlanDetailObject
    <XafDisplayName("มี.ค.")> _
    Public Property MAR As ActivePlanDetailObject
        Get
            Return fMAR
        End Get
        Set(ByVal value As ActivePlanDetailObject)
            SetPropertyValue(Of ActivePlanDetailObject)("MAR", fMAR, value)
        End Set
    End Property

    Dim fAPR As ActivePlanDetailObject
    <XafDisplayName("เม.ย.")> _
    Public Property APR As ActivePlanDetailObject
        Get
            Return fAPR
        End Get
        Set(ByVal value As ActivePlanDetailObject)
            SetPropertyValue(Of ActivePlanDetailObject)("APR", fAPR, value)
        End Set
    End Property

    Dim fMAY As ActivePlanDetailObject
    <XafDisplayName("พ.ค.")> _
    Public Property MAY As ActivePlanDetailObject
        Get
            Return fMAY
        End Get
        Set(ByVal value As ActivePlanDetailObject)
            SetPropertyValue(Of ActivePlanDetailObject)("MAY", fMAY, value)
        End Set
    End Property

    Dim fJUN As ActivePlanDetailObject
    <XafDisplayName("มิ.ย.")> _
    Public Property JUN As ActivePlanDetailObject
        Get
            Return fJUN
        End Get
        Set(ByVal value As ActivePlanDetailObject)
            SetPropertyValue(Of ActivePlanDetailObject)("JUN", fJUN, value)
        End Set
    End Property

    Dim fJUL As ActivePlanDetailObject
    <XafDisplayName("ก.ค.")> _
    Public Property JUL As ActivePlanDetailObject
        Get
            Return fJUL
        End Get
        Set(ByVal value As ActivePlanDetailObject)
            SetPropertyValue(Of ActivePlanDetailObject)("JUL", fJUL, value)
        End Set
    End Property

    Dim fAUG As ActivePlanDetailObject
    <XafDisplayName("ส.ค.")> _
    Public Property AUG As ActivePlanDetailObject
        Get
            Return fAUG
        End Get
        Set(ByVal value As ActivePlanDetailObject)
            SetPropertyValue(Of ActivePlanDetailObject)("AUG", fAUG, value)
        End Set
    End Property

    Dim fSEP As ActivePlanDetailObject
    <XafDisplayName("ก.ย.")> _
    Public Property SEP As ActivePlanDetailObject
        Get
            Return fSEP
        End Get
        Set(ByVal value As ActivePlanDetailObject)
            SetPropertyValue(Of ActivePlanDetailObject)("SEP", fSEP, value)
        End Set
    End Property

    Dim fGrowStartDate As DateTime
    '<XafDisplayName("ก.ย.")> _
    Public Property GrowStartDate As DateTime
        Get
            Return fGrowStartDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("GrowStartDate", fGrowStartDate, value)
        End Set
    End Property

    Dim fGrowEndDate As DateTime
    '<XafDisplayName("ก.ย.")> _
    Public Property GrowEndDate As DateTime
        Get
            Return fGrowEndDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("GrowEndDate", fGrowEndDate, value)
        End Set
    End Property

    Dim fGrowRemark As String
    Public Property GrowRemark As String
        Get
            Return fGrowRemark
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("GrowRemark", fGrowRemark, value)
        End Set
    End Property

    Dim fHarvestStartDate As DateTime
    '<XafDisplayName("ก.ย.")> _
    Public Property HarvestStartDate As DateTime
        Get
            Return fHarvestStartDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("HarvestStartDate", fHarvestStartDate, value)
        End Set
    End Property

    Dim fHarvestEndDate As DateTime
    '<XafDisplayName("ก.ย.")> _
    Public Property HarvestEndDate As DateTime
        Get
            Return fHarvestEndDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("HarvestEndDate", fHarvestEndDate, value)
        End Set
    End Property

    Dim fHarvestRemark As String
    Public Property HarvestRemark As String
        Get
            Return fHarvestRemark
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("HarvestRemark", fHarvestRemark, value)
        End Set
    End Property

    Dim fBuyStartDate As DateTime
    '<XafDisplayName("ก.ย.")> _
    Public Property BuyStartDate As DateTime
        Get
            Return fBuyStartDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("BuyStartDate", fBuyStartDate, value)
        End Set
    End Property

    Dim fBuyEndDate As DateTime
    '<XafDisplayName("ก.ย.")> _
    Public Property BuyEndDate As DateTime
        Get
            Return fBuyEndDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("BuyEndDate", fBuyEndDate, value)
        End Set
    End Property

    Dim fBuyRemark As String
    Public Property BuyRemark As String
        Get
            Return fBuyRemark
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("BuyRemark", fBuyRemark, value)
        End Set
    End Property

    '<XafDisplayName("รวมพื้นที่ตามแผน (ไร่)")> _
    Dim fTotalGrowSize As Double
    Public Property TotalGrowSize As Double
        Get
            'Try
            '    'Return JAN.GrowSize1 + JAN.GrowSize2 + _
            '    '      FEB.GrowSize1 + FEB.GrowSize2 + _
            '    '      MAR.GrowSize1 + MAR.GrowSize2 + _
            '    '      APR.GrowSize1 + APR.GrowSize2 + _
            '    '      MAY.GrowSize1 + MAY.GrowSize2 + _
            '    '      JUN.GrowSize1 + JUN.GrowSize2 + _
            '    '      JUL.GrowSize1 + JUL.GrowSize2 + _
            '    '      AUG.GrowSize1 + AUG.GrowSize2 + _
            '    '      SEP.GrowSize1 + SEP.GrowSize2 + _
            '    '      OCT.GrowSize1 + OCT.GrowSize2 + _
            '    '      NOV.GrowSize1 + NOV.GrowSize2 + _
            '    '      DEC.GrowSize1 + DEC.GrowSize2
            'Catch ex As Exception
            '    Return 0
            'End Try
            Return fTotalGrowSize

        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("TotalGrowSize", fTotalGrowSize, value)
        End Set
    End Property

    '<XafDisplayName("รวมพื้นที่ตามแผน (ไร่)")> _
    Dim fTotalGrowActual As Double
    Public Property TotalGrowActual As Double
        Get
            'Try
            '    Return JAN.GrowActual1 + JAN.GrowActual2 + _
            '           FEB.GrowActual1 + FEB.GrowActual2 + _
            '           MAR.GrowActual1 + MAR.GrowActual2 + _
            '           APR.GrowActual1 + APR.GrowActual2 + _
            '           MAY.GrowActual1 + MAY.GrowActual2 + _
            '           JUN.GrowActual1 + JUN.GrowActual2 + _
            '           JUL.GrowActual1 + JUL.GrowActual2 + _
            '           AUG.GrowActual1 + AUG.GrowActual2 + _
            '           SEP.GrowActual1 + SEP.GrowActual2 + _
            '           OCT.GrowActual1 + OCT.GrowActual2 + _
            '           NOV.GrowActual1 + NOV.GrowActual2 + _
            '           DEC.GrowActual1 + DEC.GrowActual2
            'Catch ex As Exception
            '    Return 0
            'End Try
            Return fTotalGrowActual

        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("TotalGrowActual", fTotalGrowActual, value)
        End Set
    End Property

    Dim fTotalHarvestSize As Double
    Public Property TotalHarvestSize As Double
        Get
            'Try
            '    Return JAN.HarvestSize1 + JAN.HarvestSize2 + _
            '           FEB.HarvestSize1 + FEB.HarvestSize2 + _
            '           MAR.HarvestSize1 + MAR.HarvestSize2 + _
            '           APR.HarvestSize1 + APR.HarvestSize2 + _
            '           MAY.HarvestSize1 + MAY.HarvestSize2 + _
            '           JUN.HarvestSize1 + JUN.HarvestSize2 + _
            '           JUL.HarvestSize1 + JUL.HarvestSize2 + _
            '           AUG.HarvestSize1 + AUG.HarvestSize2 + _
            '           SEP.HarvestSize1 + SEP.HarvestSize2 + _
            '           OCT.HarvestSize1 + OCT.HarvestSize2 + _
            '           NOV.HarvestSize1 + NOV.HarvestSize2 + _
            '           DEC.HarvestSize1 + DEC.HarvestSize2
            'Catch ex As Exception
            '    Return 0
            'End Try
            Return fTotalHarvestSize

        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("TotalHarvestSize", fTotalHarvestSize, value)
        End Set
    End Property

    Dim fTotalHarvestActual As Double
    Public Property TotalHarvestActual As Double
        Get
            'Try
            '    Return JAN.HarvestActual1 + JAN.HarvestActual2 + _
            '           FEB.HarvestActual1 + FEB.HarvestActual2 + _
            '           MAR.HarvestActual1 + MAR.HarvestActual2 + _
            '           APR.HarvestActual1 + APR.HarvestActual2 + _
            '           MAY.HarvestActual1 + MAY.HarvestActual2 + _
            '           JUN.HarvestActual1 + JUN.HarvestActual2 + _
            '           JUL.HarvestActual1 + JUL.HarvestActual2 + _
            '           AUG.HarvestActual1 + AUG.HarvestActual2 + _
            '           SEP.HarvestActual1 + SEP.HarvestActual2 + _
            '           OCT.HarvestActual1 + OCT.HarvestActual2 + _
            '           NOV.HarvestActual1 + NOV.HarvestActual2 + _
            '           DEC.HarvestActual1 + DEC.HarvestActual2
            'Catch ex As Exception
            '    Return 0
            'End Try
            Return fTotalHarvestActual

        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("TotalHarvestActual", fTotalHarvestActual, value)
        End Set
    End Property

    Dim fTotalBuyQuantity As Double
    Public Property TotalBuyQuantity As Double
        Get
            'Try
            '    Return JAN.BuyQuantity1 + JAN.BuyQuantity2 + _
            '           FEB.BuyQuantity1 + FEB.BuyQuantity2 + _
            '           MAR.BuyQuantity1 + MAR.BuyQuantity2 + _
            '           APR.BuyQuantity1 + APR.BuyQuantity2 + _
            '           MAY.BuyQuantity1 + MAY.BuyQuantity2 + _
            '           JUN.BuyQuantity1 + JUN.BuyQuantity2 + _
            '           JUL.BuyQuantity1 + JUL.BuyQuantity2 + _
            '           AUG.BuyQuantity1 + AUG.BuyQuantity2 + _
            '           SEP.BuyQuantity1 + SEP.BuyQuantity2 + _
            '           OCT.BuyQuantity1 + OCT.BuyQuantity2 + _
            '           NOV.BuyQuantity1 + NOV.BuyQuantity2 + _
            '           DEC.BuyQuantity1 + DEC.BuyQuantity2
            'Catch ex As Exception
            '    Return 0
            'End Try
            Return fTotalBuyQuantity

        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("TotalBuyQuantity", fTotalBuyQuantity, value)
        End Set
    End Property

    Dim fTotalBuyActual As Double
    Public Property TotalBuyActual As Double
        Get
            'Try
            '    Return JAN.BuyActual1 + JAN.BuyActual2 + _
            '           FEB.BuyActual1 + FEB.BuyActual2 + _
            '           MAR.BuyActual1 + MAR.BuyActual2 + _
            '           APR.BuyActual1 + APR.BuyActual2 + _
            '           MAY.BuyActual1 + MAY.BuyActual2 + _
            '           JUN.BuyActual1 + JUN.BuyActual2 + _
            '           JUL.BuyActual1 + JUL.BuyActual2 + _
            '           AUG.BuyActual1 + AUG.BuyActual2 + _
            '           SEP.BuyActual1 + SEP.BuyActual2 + _
            '           OCT.BuyActual1 + OCT.BuyActual2 + _
            '           NOV.BuyActual1 + NOV.BuyActual2 + _
            '           DEC.BuyActual1 + DEC.BuyActual2
            'Catch ex As Exception
            '    Return 0
            'End Try
            Return fTotalBuyActual

        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("TotalBuyActual", fTotalBuyActual, value)
        End Set
    End Property

End Class

<NonPersistent()> _
Public Class ActivePlanDetailObject
    Private _GrowSize1 As Integer
    Private _GrowActual1 As Integer
    Private _HarvestSize1 As Integer
    Private _HarvestActual1 As Integer
    Private _BuyQuantity1 As Double
    Private _BuyActual1 As Double

    Private _GrowSize2 As Integer
    Private _GrowActual2 As Integer
    Private _HarvestSize2 As Integer
    Private _HarvestActual2 As Integer
    Private _BuyQuantity2 As Double
    Private _BuyActual2 As Double

    Public Sub New()

    End Sub

    Public Property GrowSize1 As Integer
        Get
            Return _GrowSize1
        End Get
        Set(value As Integer)
            _GrowSize1 = value
        End Set
    End Property
    Public Property GrowActual1 As Integer
        Get
            Return _GrowActual1
        End Get
        Set(value As Integer)
            _GrowActual1 = value
        End Set
    End Property
    Public Property HarvestSize1 As Integer
        Get
            Return _HarvestSize1
        End Get
        Set(value As Integer)
            _HarvestSize1 = value
        End Set
    End Property

    Public Property HarvestActual1 As Integer
        Get
            Return _HarvestActual1
        End Get
        Set(value As Integer)
            _HarvestActual1 = value
        End Set
    End Property
    Public Property BuyQuantity1 As Double
        Get
            Return _BuyQuantity1
        End Get
        Set(value As Double)
            _BuyQuantity1 = value
        End Set
    End Property
    Public Property BuyActual1 As Double
        Get
            Return _BuyActual1
        End Get
        Set(value As Double)
            _BuyActual1 = value
        End Set
    End Property

    '=======================================================
    Public Property GrowSize2 As Integer
        Get
            Return _GrowSize2
        End Get
        Set(value As Integer)
            _GrowSize2 = value
        End Set
    End Property
    Public Property GrowActual2 As Integer
        Get
            Return _GrowActual2
        End Get
        Set(value As Integer)
            _GrowActual2 = value
        End Set
    End Property
    Public Property HarvestSize2 As Integer
        Get
            Return _HarvestSize2
        End Get
        Set(value As Integer)
            _HarvestSize2 = value
        End Set
    End Property

    Public Property HarvestActual2 As Integer
        Get
            Return _HarvestActual2
        End Get
        Set(value As Integer)
            _HarvestActual2 = value
        End Set
    End Property
    Public Property BuyQuantity2 As Double
        Get
            Return _BuyQuantity2
        End Get
        Set(value As Double)
            _BuyQuantity2 = value
        End Set
    End Property
    Public Property BuyActual2 As Double
        Get
            Return _BuyActual2
        End Get
        Set(value As Double)
            _BuyActual2 = value
        End Set
    End Property
End Class
