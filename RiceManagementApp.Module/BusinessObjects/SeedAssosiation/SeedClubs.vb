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
''' === ชมรมผู้ผลิตและจำหน่ายเมล็ดพันธุ์ข้าว ===
''' </summary>
''' <remarks></remarks>
''' 
<NavigationItem("ชมรมและสมาคม")>
<DefaultClassOptions(), XafDisplayName("ชมรมผู้ผลิตและจำหน่ายเมล็ดพันธุ์ข้าว")> _
Public Class SeedClubs ' Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        Address = New CustomAddress(Session)
        ' Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).
    End Sub

    Dim _SeedClubName As String
    <XafDisplayName("ชื่อชมรม")> _
    Public Property SeedClubName() As String
        Get
            Return _SeedClubName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SeedClubName", _SeedClubName, value)
        End Set
    End Property

    Dim _Address As CustomAddress
    <DC.Aggregated, ExpandObjectMembers(ExpandObjectMembers.Always)> _
    <VisibleInListView(False)> _
    <XafDisplayName("ที่อยู่")> _
    Public Property Address() As CustomAddress
        Get
            Return _Address
        End Get
        Set(ByVal value As CustomAddress)
            SetPropertyValue(Of CustomAddress)("Address", _Address, value)
        End Set
    End Property

    <XafDisplayName("ที่อยู่")> _
    Public ReadOnly Property FullAddress As String
        Get
            Return Address.FullAddress
        End Get
    End Property

    Private _CountMembers As Integer
    <XafDisplayName("สมาชิกจำนวน")> _
    <PersistentAlias("SeedMembers.Count")> _
    <ModelDefault("DisplayFormat", "{0} ราย")> _
    <ImmediatePostData()> _
    Public ReadOnly Property CountMembers() As Integer
        Get
            Try
                Dim tempObject As Object
                tempObject = EvaluateAlias("CountMembers")
                Return CDbl(tempObject)
            Catch ex As Exception
                Return 0
            End Try
        End Get
    End Property

    <XafDisplayName("รายชื่อสมาชิก")> _
    <Association("SeedClub-SeedMembers", GetType(SeedMembers))> _
    Public ReadOnly Property SeedMembers() As XPCollection(Of SeedMembers)
        Get
            Return GetCollection(Of SeedMembers)("SeedMembers")
        End Get
    End Property
End Class
