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

<DefaultProperty("AssociationPositionName")> _
Public Class AssociationPosition
    Inherits BaseObject
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

    Dim fAssociationPositionID As String
    Public Property AssociationPositionID() As String
        Get
            Return fAssociationPositionID
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AssociationPositionID", fAssociationPositionID, value)
        End Set
    End Property

    Dim fAssociationPositionName As String
    Public Property AssociationPositionName() As String
        Get
            Return fAssociationPositionName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AssociationPositionName", fAssociationPositionName, value)
        End Set
    End Property

End Class
