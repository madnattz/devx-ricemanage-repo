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
''' ชื่อศูนย์ทั้ง 23 ศูนย์ และศูนย์วิจัยข้าว
''' </summary>
''' <remarks></remarks>
<DefaultClassOptions()> _
Public Class Site ' Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        Me.Address = New CustomAddress(Session)
        ' Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).
    End Sub

    Protected Overrides Sub OnSaving()
        Me.LastUodateDate = Date.Now
        Me.LastUpdateBy = CType(SecuritySystem.CurrentUser, User).DisplayName
        MyBase.OnSaving()
    End Sub

    Dim fSiteNo As String
    <Size(10)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property SiteNo() As String
        Get
            Return fSiteNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SiteNo", fSiteNo, value)
        End Set
    End Property

    Dim fSiteName As String
    <Size(200)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property SiteName() As String
        Get
            Return fSiteName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SiteName", fSiteName, value)
        End Set
    End Property

    Dim fAppName As String
    <Size(20)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    <XafDisplayName("ชื่อย่อ")> _
    Public Property AppName() As String
        Get
            Return fAppName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AppName", fAppName, value)
        End Set
    End Property
    Dim fSiteType As PublicEnum.SiteType
    Public Property SiteType() As PublicEnum.SiteType
        Get
            Return fSiteType
        End Get
        Set(ByVal value As PublicEnum.SiteType)
            SetPropertyValue(Of Integer)("SiteType", fSiteType, value)
        End Set
    End Property

    Dim fAddress As CustomAddress
    <DC.Aggregated, ExpandObjectMembers(ExpandObjectMembers.Always)> _
    Public Property Address() As CustomAddress
        Get
            Return fAddress
        End Get
        Set(ByVal value As CustomAddress)
            SetPropertyValue(Of CustomAddress)("Address", fAddress, value)
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
