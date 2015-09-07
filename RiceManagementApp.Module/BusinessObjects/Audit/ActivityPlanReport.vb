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
Public Class ActivityPlanReport
    Inherits BaseObject
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()

    End Sub

    Dim fItemNo As Integer
    <XafDisplayName("ลำดับ")> _
    Public Property ItemNo As Integer
        Get
            Return fItemNo
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ItemNo", fItemNo, value)
        End Set
    End Property

    Dim fActivityName As String
    <XafDisplayName("กิจกรรม")> _
    Public Property ActivityName As String
        Get
            Return fActivityName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ActivityName", fActivityName, value)
        End Set
    End Property

    Dim fActivityUnit As String
    <XafDisplayName("หน่วย")> _
    Public Property ActivityUnit As String
        Get
            Return fActivityUnit
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ActivityUnit", fActivityUnit, value)
        End Set
    End Property

    Dim fOCT As Double
    <XafDisplayName("ต.ค.")> _
    Public Property OCT As Double
        Get
            Return fOCT
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("OCT", fOCT, value)
        End Set
    End Property

    Dim fNOV As Double
    <XafDisplayName("พ.ย.")> _
    Public Property NOV As Double
        Get
            Return fNOV
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("NOV", fNOV, value)
        End Set
    End Property

    Dim fDEC As Double
    <XafDisplayName("ธ.ค.")> _
    Public Property DEC As Double
        Get
            Return fDEC
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("DEC", fDEC, value)
        End Set
    End Property

    Dim fJAN As Double
    <XafDisplayName("ม.ค.")> _
    Public Property JAN As Double
        Get
            Return fJAN
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("JAN", fJAN, value)
        End Set
    End Property

    Dim fFEB As Double
    <XafDisplayName("ก.พ.")> _
    Public Property FEB As Double
        Get
            Return fFEB
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("FEB", fFEB, value)
        End Set
    End Property

    Dim fMAR As Double
    <XafDisplayName("มี.ค.")> _
    Public Property MAR As Double
        Get
            Return fMAR
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("MAR", fMAR, value)
        End Set
    End Property

    Dim fAPR As Double
    <XafDisplayName("เม.ย.")> _
    Public Property APR As Double
        Get
            Return fAPR
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("APR", fAPR, value)
        End Set
    End Property

    Dim fMAY As Double
    <XafDisplayName("พ.ค.")> _
    Public Property MAY As Double
        Get
            Return fMAY
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("MAY", fMAY, value)
        End Set
    End Property

    Dim fJUN As Double
    <XafDisplayName("มิ.ย.")> _
    Public Property JUN As Double
        Get
            Return fJUN
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("JUN", fJUN, value)
        End Set
    End Property

    Dim fJUL As Double
    <XafDisplayName("ก.ค.")> _
    Public Property JUL As Double
        Get
            Return fJUL
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("JUL", fJUL, value)
        End Set
    End Property

    Dim fAUG As Double
    <XafDisplayName("ส.ค.")> _
    Public Property AUG As Double
        Get
            Return fAUG
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("AUG", fAUG, value)
        End Set
    End Property

    Dim fSEP As Double
    <XafDisplayName("ก.ย.")> _
    Public Property SEP As Double
        Get
            Return fSEP
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("SEP", fSEP, value)
        End Set
    End Property

    Dim fTotal As Double
    <XafDisplayName("รวม")> _
    Public Property Total As Double
        Get
            Return fTotal
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("Total", fTotal, value)
        End Set
    End Property

    Dim fDifferrence As Double
    <XafDisplayName("ผลต่าง (ลด/เพิ่ม)")> _
    Public Property Differrence As Double
        Get
            Return fDifferrence
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("Differrence", fDifferrence, value)
        End Set
    End Property

End Class
