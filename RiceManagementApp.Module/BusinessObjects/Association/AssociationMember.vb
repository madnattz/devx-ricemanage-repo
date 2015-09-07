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

Public Class AssociationMember
    Inherits BaseObject
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        Me.Address = New CustomAddress(Session)
        DataOwner = GetCurrentSite()
        AssociationPositionName = Session.FindObject(Of AssociationPosition)(CriteriaOperator.Parse("AssociationPositionID=?", 1))
    End Sub

    Protected Overrides Sub OnSaving()
        Me.LastUodateDate = Date.Now
        Me.LastUpdateBy = CType(SecuritySystem.CurrentUser, User).DisplayName

        If (Me.AssociationMemberID Is Nothing) Then
            Dim prefix As String = ""
            Dim _year As String = Date.Now.ToString("yy", New Globalization.CultureInfo("th-TH"))
            prefix = _year

            Me.fAssociationMemberID = String.Format("{0}-{1:D4}", prefix, DistributedIdGeneratorHelper.Generate(Me.Session.DataLayer, Me.GetType().FullName, prefix))
        End If

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

    <Persistent("AssociationMemberID")> _
    Private fAssociationMemberID As String
    <PersistentAlias("fAssociationMemberID")> _
    Public ReadOnly Property AssociationMemberID() As String
        Get
            Return fAssociationMemberID
        End Get
    End Property

    Dim fPersonCardID As String
    <Size(13)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property PersonCardID() As String
        Get
            Return fPersonCardID
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PersonCardID", fPersonCardID, value)
        End Set
    End Property

    Public ReadOnly Property FullName As String
        Get
            If PrefixName IsNot Nothing Then
                Return PrefixName.FrefixName & Firstname & "  " & Lastname
            Else
                Return Firstname & "  " & Lastname
            End If
        End Get
    End Property


    Dim fPrefixName As PrefixName
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property PrefixName() As PrefixName
        Get
            Return fPrefixName
        End Get
        Set(ByVal value As PrefixName)
            SetPropertyValue(Of PrefixName)("PrefixName", fPrefixName, value)
        End Set
    End Property

    Dim fFirstname As String
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property Firstname() As String
        Get
            Return fFirstname
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Firstname", fFirstname, value)
        End Set
    End Property

    Dim fLastname As String
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property Lastname() As String
        Get
            Return fLastname
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Lastname", fLastname, value)
        End Set
    End Property

    Dim fAssociationPositionName As AssociationPosition
    Public Property AssociationPositionName() As AssociationPosition
        Get
            Return fAssociationPositionName
        End Get
        Set(ByVal value As AssociationPosition)
            SetPropertyValue(Of AssociationPosition)("AssociationPositionName", fAssociationPositionName, value)
        End Set
    End Property

    Dim fAssociationTypeBusinessName As AssociationBusiness
    Public Property AssociationTypeBusinessName() As AssociationBusiness
        Get
            Return fAssociationTypeBusinessName
        End Get
        Set(ByVal value As AssociationBusiness)
            SetPropertyValue(Of AssociationBusiness)("AssociationTypeBusinessName", fAssociationTypeBusinessName, value)
        End Set
    End Property

    Dim fShopName As String
    <XafDisplayName("ชื่อร้าน/หน่วยงาน")> _
    Public Property ShopName() As String
        Get
            Return fShopName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ShopName", fShopName, value)
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

    Dim fCheckAsso As Boolean
    Public Property CheckAsso() As Boolean
        Get
            Return fCheckAsso
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("CheckAsso ", fCheckAsso, value)
        End Set
    End Property

    Dim fCheckMember As Boolean
    Public Property CheckMember() As Boolean
        Get
            Return fCheckMember
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("CheckMember", fCheckMember, value)
        End Set
    End Property

    Dim fCheckAgentSeed As Boolean
    Public Property CheckAgentSeed() As Boolean
        Get
            Return fCheckAgentSeed
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("CheckAgentSeed ", fCheckAgentSeed, value)
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

    Dim fAssociationGroup As AssociationGroup
    <Association("AssociationMemberReferencesAssociationGroup")> _
    Public Property AssociationGroup() As AssociationGroup
        Get
            Return fAssociationGroup
        End Get
        Set(ByVal value As AssociationGroup)
            SetPropertyValue(Of AssociationGroup)("AssociationGroup", fAssociationGroup, value)
        End Set
    End Property

    <Association("AssociationSeedReferencesAssociationMember", GetType(AssociationSeed))> _
    Public ReadOnly Property AssociationSeeds() As XPCollection(Of AssociationSeed)
        Get
            Return GetCollection(Of AssociationSeed)("AssociationSeeds")
        End Get
    End Property

    <Association("AssociationDealReferencesAssociationMember", GetType(AssociationDeal))> _
    Public ReadOnly Property AssociationDeals() As XPCollection(Of AssociationDeal)
        Get
            Return GetCollection(Of AssociationDeal)("AssociationDeals")
        End Get
    End Property


End Class
