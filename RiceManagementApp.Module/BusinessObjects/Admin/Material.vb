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

<DefaultClassOptions()> _
Public Class Material
    Inherits BaseObject
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

    Protected Overrides Sub OnSaving()
        Me.LastUodateDate = Date.Now
        Me.LastUpdateBy = CType(SecuritySystem.CurrentUser, User).DisplayName
        MyBase.OnSaving()
    End Sub

    Dim fMaterialType As PublicEnum.MaterialType
    Public Property MaterialType() As PublicEnum.MaterialType
        Get
            Return fMaterialType
        End Get
        Set(ByVal value As PublicEnum.MaterialType)
            SetPropertyValue(Of PublicEnum.MaterialType)("MaterialType", fMaterialType, value)
        End Set
    End Property

    'Dim fMaterailId As Integer
    'Public Property MaterailId() As Integer
    '    Get
    '        Return fMaterailId
    '    End Get
    '    Set(ByVal value As Integer)
    '        SetPropertyValue(Of Integer)("MaterailId", fMaterailId, value)
    '    End Set
    'End Property

    Dim fMaterialCode As String
    Public Property MaterialCode() As String
        Get
            Return fMaterialCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("MaterialCode", fMaterialCode, value)
        End Set
    End Property

    Dim fMaterialName As String
    Public Property MaterialName() As String
        Get
            Return fMaterialName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("MaterialName", fMaterialName, value)
        End Set
    End Property

    Dim fUnit As String
    Public Property Unit() As String
        Get
            Return fUnit
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Unit", fUnit, value)
        End Set
    End Property

    Dim fStatus As PublicEnum.PublicStatus
    Public Property Status() As PublicEnum.PublicStatus
        Get
            Return fStatus
        End Get
        Set(ByVal value As PublicEnum.PublicStatus)
            SetPropertyValue(Of Integer)("Status", fStatus, value)
        End Set
    End Property
    Dim fLastUpdateBy As String
    Public Property LastUpdateBy() As String
        Get
            Return fLastUpdateBy
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("LastUpdateBy", fLastUpdateBy, value)
        End Set
    End Property
    Dim fLastUodateDate As DateTime
    Public Property LastUodateDate() As DateTime
        Get
            Return fLastUodateDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("LastUodateDate", fLastUodateDate, value)
        End Set
    End Property

End Class
