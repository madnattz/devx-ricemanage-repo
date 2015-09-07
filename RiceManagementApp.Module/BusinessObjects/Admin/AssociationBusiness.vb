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

<DefaultProperty("AssociationTypeBusinessName")> _
<XafDisplayName("ประเภทธุรกิจ")> _
<DefaultClassOptions()> _
Public Class AssociationBusiness
    Inherits BaseObject
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

    Dim fAssociationTypeBusinessID As String
    Public Property AssociationTypeBusinessID() As String
        Get
            Return fAssociationTypeBusinessID
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AssociationTypeBusinessID", fAssociationTypeBusinessID, value)
        End Set
    End Property

    Dim fAssociationTypeBusinessName As String
    Public Property AssociationTypeBusinessName() As String
        Get
            Return fAssociationTypeBusinessName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AssociationTypeBusinessName", fAssociationTypeBusinessName, value)
        End Set
    End Property
End Class
