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


<NavigationItem("สมาคมฯ")> _
<ImageName("BO_Department")> _
<DefaultClassOptions()> _
Public Class AssociationGroup
    Inherits BaseObject
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        DataOwner = GetCurrentSite()
        Me.Address = New CustomAddress(Session)
    End Sub

    Protected Overrides Sub OnSaving()
        If Me.DataOwner Is Nothing Then
            DataOwner = GetCurrentSite()
        End If
        MyBase.OnSaving()
    End Sub

    Dim fDataOwner As Site
    <Browsable(False)> _
    Public Property DataOwner() As Site
        Get
            Return fDataOwner
        End Get
        Set(ByVal value As Site)
            SetPropertyValue(Of Site)("DataOwner", fDataOwner, value)
        End Set
    End Property

    Public Function GetCurrentSite() As Site
        Dim siteSetting As SiteSetting = Session.FindObject(Of SiteSetting)(Nothing)
        If siteSetting IsNot Nothing Then
            If siteSetting.Site IsNot Nothing Then
                Return siteSetting.Site
            Else
                Return Nothing
            End If
            Return siteSetting.Site
        Else
            Return Nothing
        End If
    End Function

    Dim fAssociationGroupID As String
    <RuleRequiredField(DefaultContexts.Save)> _
     Public Property AssociationGroupID() As String
        Get
            Return fAssociationGroupID
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AssociationGroupID", fAssociationGroupID, value)
        End Set
    End Property

    Dim fAssociationGroupName As String
    <RuleRequiredField(DefaultContexts.Save)> _
     Public Property AssociationGroupName() As String
        Get
            Return fAssociationGroupName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AssociationGroupName", fAssociationGroupName, value)
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
    <VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)>
    Public Property LastUpdateBy() As String
        Get
            Return fLastUpdateBy
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("LastUpdateBy", fLastUpdateBy, value)
        End Set
    End Property
    Dim fLastUodateDate As DateTime
    <VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)>
    Public Property LastUodateDate() As DateTime
        Get
            Return fLastUodateDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("LastUodateDate", fLastUodateDate, value)
        End Set
    End Property

    Public ReadOnly Property AssociationMemberCount() As Integer
        Get
            Return AssociationMembers.Count
        End Get
    End Property

    <Association("AssociationMemberReferencesAssociationGroup", GetType(AssociationMember))> _
    Public ReadOnly Property AssociationMembers() As XPCollection(Of AssociationMember)
        Get
            Return GetCollection(Of AssociationMember)("AssociationMembers")
        End Get
    End Property







End Class
