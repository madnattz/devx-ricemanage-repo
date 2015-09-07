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
<XafDisplayName("ตั้งค่าข้อมูลศูนย์")> _
<RuleCriteria("CannotDeleteSiteSetting", DefaultContexts.Delete, "False", CustomMessageTemplate:="ไม่สามารถลบข้อมูลนี้ได้")> _
Public Class SiteSetting
    Inherits BaseObject
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        Me.Address = New CustomAddress(Session)
    End Sub

    Protected Overrides Sub OnSaving()
        Me.LastUodateDate = Date.Now
        If SecuritySystem.CurrentUser IsNot Nothing Then
            Me.LastUpdateBy = CType(SecuritySystem.CurrentUser, User).DisplayName
        End If
        MyBase.OnSaving()
    End Sub

    Dim fSite As Site
    <XafDisplayName("เลือกศูนย์")> _
    <DataSourceCriteria("SiteType='Factory'")> _
    <ImmediatePostData()> _
    Public Property Site() As Site
        Get
            Return fSite
        End Get
        Set(ByVal value As Site)
            SetPropertyValue(Of Site)("Site", fSite, value)
            If Not IsLoading AndAlso Not IsSaving Then
                SiteNo = value.SiteNo
                SiteName = value.SiteName
                Address = value.Address
                OnChanged("SiteNo")
                OnChanged("SiteName")
                OnChanged("Address")
            End If
        End Set
    End Property

    Dim fSiteNo As String
    <Size(10)> _
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
    Public Property SiteName() As String
        Get
            Return fSiteName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SiteName", fSiteName, value)
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
