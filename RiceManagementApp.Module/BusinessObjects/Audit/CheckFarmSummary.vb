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
Public Class CheckFarmSummary
    Inherits BaseObject
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()

    End Sub

    Dim fOCT As CheckFarmObject
    <XafDisplayName("ต.ค.")> _
    Public Property OCT As CheckFarmObject
        Get
            Return fOCT
        End Get
        Set(ByVal value As CheckFarmObject)
            SetPropertyValue(Of CheckFarmObject)("OCT", fOCT, value)
        End Set
    End Property

    Dim fNOV As CheckFarmObject
    <XafDisplayName("พ.ย.")> _
    Public Property NOV As CheckFarmObject
        Get
            Return fNOV
        End Get
        Set(ByVal value As CheckFarmObject)
            SetPropertyValue(Of CheckFarmObject)("NOV", fNOV, value)
        End Set
    End Property

    Dim fDEC As CheckFarmObject
    <XafDisplayName("ธ.ค.")> _
    Public Property DEC As CheckFarmObject
        Get
            Return fDEC
        End Get
        Set(ByVal value As CheckFarmObject)
            SetPropertyValue(Of CheckFarmObject)("DEC", fDEC, value)
        End Set
    End Property

    Dim fJAN As CheckFarmObject
    <XafDisplayName("ม.ค.")> _
    Public Property JAN As CheckFarmObject
        Get
            Return fJAN
        End Get
        Set(ByVal value As CheckFarmObject)
            SetPropertyValue(Of CheckFarmObject)("JAN", fJAN, value)
        End Set
    End Property

    Dim fFEB As CheckFarmObject
    <XafDisplayName("ก.พ.")> _
    Public Property FEB As CheckFarmObject
        Get
            Return fFEB
        End Get
        Set(ByVal value As CheckFarmObject)
            SetPropertyValue(Of CheckFarmObject)("FEB", fFEB, value)
        End Set
    End Property

    Dim fMAR As CheckFarmObject
    <XafDisplayName("มี.ค.")> _
    Public Property MAR As CheckFarmObject
        Get
            Return fMAR
        End Get
        Set(ByVal value As CheckFarmObject)
            SetPropertyValue(Of CheckFarmObject)("MAR", fMAR, value)
        End Set
    End Property

    Dim fAPR As CheckFarmObject
    <XafDisplayName("เม.ย.")> _
    Public Property APR As CheckFarmObject
        Get
            Return fAPR
        End Get
        Set(ByVal value As CheckFarmObject)
            SetPropertyValue(Of CheckFarmObject)("APR", fAPR, value)
        End Set
    End Property

    Dim fMAY As CheckFarmObject
    <XafDisplayName("พ.ค.")> _
    Public Property MAY As CheckFarmObject
        Get
            Return fMAY
        End Get
        Set(ByVal value As CheckFarmObject)
            SetPropertyValue(Of CheckFarmObject)("MAY", fMAY, value)
        End Set
    End Property

    Dim fJUN As CheckFarmObject
    <XafDisplayName("มิ.ย.")> _
    Public Property JUN As CheckFarmObject
        Get
            Return fJUN
        End Get
        Set(ByVal value As CheckFarmObject)
            SetPropertyValue(Of CheckFarmObject)("JUN", fJUN, value)
        End Set
    End Property

    Dim fJUL As CheckFarmObject
    <XafDisplayName("ก.ค.")> _
    Public Property JUL As CheckFarmObject
        Get
            Return fJUL
        End Get
        Set(ByVal value As CheckFarmObject)
            SetPropertyValue(Of CheckFarmObject)("JUL", fJUL, value)
        End Set
    End Property

    Dim fAUG As CheckFarmObject
    <XafDisplayName("ส.ค.")> _
    Public Property AUG As CheckFarmObject
        Get
            Return fAUG
        End Get
        Set(ByVal value As CheckFarmObject)
            SetPropertyValue(Of CheckFarmObject)("AUG", fAUG, value)
        End Set
    End Property

    Dim fSEP As CheckFarmObject
    <XafDisplayName("ก.ย.")> _
    Public Property SEP As CheckFarmObject
        Get
            Return fSEP
        End Get
        Set(ByVal value As CheckFarmObject)
            SetPropertyValue(Of CheckFarmObject)("SEP", fSEP, value)
        End Set
    End Property

    Dim fTotalPlanSize As Double
    <XafDisplayName("รวมพื้นที่ตามแผน (ไร่)")> _
    Public ReadOnly Property TotalPlanSize As Double
        Get
            Try
                Return JAN.PlanSize + _
              FEB.PlanSize + _
              MAR.PlanSize + _
              APR.PlanSize + _
              MAY.PlanSize + _
              JUN.PlanSize + _
              JUL.PlanSize + _
              AUG.PlanSize + _
              SEP.PlanSize + _
              OCT.PlanSize + _
              NOV.PlanSize + _
              DEC.PlanSize
            Catch ex As Exception
                Return 0
            End Try

           
        End Get
    End Property

    Dim fTotalActualSize As Double
    <XafDisplayName("รวมพื้นที่ตรวจจริง (ไร่)")> _
    Public ReadOnly Property TotalActualSize As Double
        Get
            Try
                Return JAN.ActualSize + _
                   FEB.ActualSize + _
                   MAR.ActualSize + _
                   APR.ActualSize + _
                   MAY.ActualSize + _
                   JUN.ActualSize + _
                   JUL.ActualSize + _
                   AUG.ActualSize + _
                   SEP.ActualSize + _
                   OCT.ActualSize + _
                   NOV.ActualSize + _
                   DEC.ActualSize
            Catch ex As Exception
                Return 0
            End Try
        End Get
    End Property

    Dim fTotalPassSize As Double
    <XafDisplayName("รวมพื้นที่ผ่าน (ไร่)")> _
    Public ReadOnly Property TotalPassSize As Double
        Get
            Try
                Return JAN.PassSize + _
              FEB.PassSize + _
              MAR.PassSize + _
              APR.PassSize + _
              MAY.PassSize + _
              JUN.PassSize + _
              JUL.PassSize + _
              AUG.PassSize + _
              SEP.PassSize + _
              OCT.PassSize + _
              NOV.PassSize + _
              DEC.PassSize
            Catch ex As Exception
                Return 0
            End Try
           
        End Get
    End Property

    Dim fTotalFailSize As Double
    <XafDisplayName("รวมพื้นที่ไม่ผ่าน (ไร่)")> _
    Public ReadOnly Property TotalFailSize As Double
        Get
            Try
                Return JAN.FailSize + _
                                FEB.FailSize + _
                                MAR.FailSize + _
                                APR.FailSize + _
                                MAY.FailSize + _
                                JUN.FailSize + _
                                JUL.FailSize + _
                                AUG.FailSize + _
                                SEP.FailSize + _
                                OCT.FailSize + _
                                NOV.FailSize + _
                                DEC.FailSize

            Catch ex As Exception
                Return 0
            End Try

        End Get
    End Property

    Dim fTotalDamageSize As Double
    <XafDisplayName("รวมเสียหายก่อนตรวจ (ไร่)")> _
    Public ReadOnly Property TotalDamageSize As Double
        Get
            Try
                Return JAN.FullDamageSize + _
                              FEB.FullDamageSize + _
                              MAR.FullDamageSize + _
                              APR.FullDamageSize + _
                              MAY.FullDamageSize + _
                              JUN.FullDamageSize + _
                              JUL.FullDamageSize + _
                              AUG.FullDamageSize + _
                              SEP.FullDamageSize + _
                              OCT.FullDamageSize + _
                              NOV.FullDamageSize + _
                              DEC.FullDamageSize
            Catch ex As Exception
                Return 0
            End Try
          
        End Get
    End Property

End Class

<NonPersistent()> _
Public Class CheckFarmObject
    Private _PlanSize As Integer
    Private _ActualSize As Integer
    Private _PassSize As Integer
    Private _FailSize As Integer
    Private _FullDamageSize As Integer
    Private _FailRemark As String

    Public Sub New()

    End Sub

    Public Property PlanSize As Integer
        Get
            Return _PlanSize
        End Get
        Set(value As Integer)
            _PlanSize = value
        End Set
    End Property

    Public Property ActualSize As Integer
        Get
            Return _ActualSize
        End Get
        Set(value As Integer)
            _ActualSize = value
        End Set
    End Property
    Public Property PassSize As Integer
        Get
            Return _PassSize
        End Get
        Set(value As Integer)
            _PassSize = value
        End Set
    End Property
    Public Property FailSize As Integer
        Get
            Return _FailSize
        End Get
        Set(value As Integer)
            _FailSize = value
        End Set
    End Property
    Public Property FullDamageSize As Integer
        Get
            Return _FullDamageSize
        End Get
        Set(value As Integer)
            _FullDamageSize = value
        End Set
    End Property
    Public Property FailRemark As String
        Get
            Return _FailRemark
        End Get
        Set(value As String)
            _FailRemark = value
        End Set
    End Property
End Class
