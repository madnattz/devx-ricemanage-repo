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

''' <summary>
''' ¾Ñ¹¸Øì àªè¹ ¡¢10 ÁÐÅÔ105
''' </summary>
''' <remarks></remarks>
<DefaultClassOptions()> _
Public Class SeedType ' Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        ' Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).
    End Sub

    Protected Overrides Sub OnSaving()
        Me.LastUodateDate = Date.Now
        Me.LastUpdateBy = CType(SecuritySystem.CurrentUser, User).DisplayName
        MyBase.OnSaving()
    End Sub

    <Size(40)> _
    Dim fPlant As Plant
    <Size(4)> _
    <Association("SeedTypeReferencesPlant")> _
    Public Property Plant() As Plant
        Get
            Return fPlant
        End Get
        Set(ByVal value As Plant)
            SetPropertyValue(Of Plant)("Plant", fPlant, value)
        End Set
    End Property

    Dim fSeedID As Integer
    Public Property SeedID() As Integer
        Get
            Return fSeedID
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("SeedID", fSeedID, value)
        End Set
    End Property
    Dim fSeedName As String
    Public Property SeedName() As String
        Get
            Return fSeedName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SeedName", fSeedName, value)
        End Set
    End Property

    Dim fSeedGroup As PublicEnum.SeedGroup
    Public Property SeedGroup() As PublicEnum.SeedGroup
        Get
            Return fSeedGroup
        End Get
        Set(ByVal value As PublicEnum.SeedGroup)
            SetPropertyValue(Of PublicEnum.SeedGroup)("SeedGroup", fSeedGroup, value)
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
