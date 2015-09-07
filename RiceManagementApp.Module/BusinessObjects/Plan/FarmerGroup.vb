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

'<ImageName("BO_Contact")> _
'<DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")> _
'<DefaultListViewOptions(MasterDetailMode.ListViewOnly, False, NewItemRowPosition.None)> _
'<Persistent("DatabaseTableName")> _
<DefaultClassOptions()> _
Public Class FarmerGroup ' Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        Me.Address = New CustomAddress(Session)
        Me.GroupType = PublicEnum.GroupType.RegisterGroup
        Me.Status = PublicEnum.PublicStatus.Active
        DataOwner = GetCurrentSite()
    End Sub

    Protected Overrides Sub OnSaving()
        MyBase.OnSaving()
        Me.LastUodateDate = Date.Now
        Me.LastUpdateBy = CType(SecuritySystem.CurrentUser, User).DisplayName

        If Me.DataOwner Is Nothing Then
            DataOwner = GetCurrentSite()
        End If
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

    Dim fGroupID As String
    <RuleRequiredField("FarmerGroup.GroupID", DefaultContexts.Save)> _
    Public Property GroupID() As String
        Get
            Return fGroupID
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("GroupID", fGroupID, value)
        End Set
    End Property
    Dim fGroupCode As String
    <RuleRequiredField("FarmerGroup.GroupCode", DefaultContexts.Save)> _
    <XafDisplayName("·ÐàºÕÂ¹¡ÅØèÁ¼Ùé¼ÅÔµàÁÅç´¾Ñ¹¸Øì")> _
    Public Property GroupCode() As String
        Get
            Return fGroupCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("GroupCode", fGroupCode, value)
        End Set
    End Property
    Dim fGroupName As String
    <RuleRequiredField("FarmerGroup.GroupName", DefaultContexts.Save)> _
    Public Property GroupName() As String
        Get
            Return fGroupName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("GroupName", fGroupName, value)
        End Set
    End Property
    Dim fGroupType As PublicEnum.GroupType
    Public Property GroupType() As PublicEnum.GroupType
        Get
            Return fGroupType
        End Get
        Set(ByVal value As PublicEnum.GroupType)
            SetPropertyValue(Of PublicEnum.GroupType)("GroupType", fGroupType, value)
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
    Dim fLeader As Farmer
    <DataSourceCriteria("FarmerGroup.GroupName='@this.GroupName' and Not FullName In('@this.Leader.FullName')")> _
    Public Property Leader() As Farmer
        Get
            Return fLeader
        End Get
        Set(ByVal value As Farmer)
            SetPropertyValue(Of Farmer)("Leader", fLeader, value)
        End Set
    End Property
    Dim fAssLeader As Farmer
    <DataSourceCriteria("FarmerGroup.GroupName='@this.GroupName' and Not FullName In('@this.Leader.FullName')")> _
    Public Property AssLeader() As Farmer
        Get
            Return fAssLeader
        End Get
        Set(ByVal value As Farmer)
            SetPropertyValue(Of Farmer)("AssLeader", fAssLeader, value)
        End Set
    End Property

    Dim fDistanceBetweenSite As Double
    <XafDisplayName("ÃÐÂÐËèÒ§¨Ò¡ÈÙ¹ÂìÏ (¡Á.)")> _
    Public Property DistanceBetweenSite() As Double
        Get
            Return fDistanceBetweenSite
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("DistanceBetweenSite", fDistanceBetweenSite, value)
        End Set
    End Property

    Public ReadOnly Property MembersCount() As Integer
        Get
            Return Farmers.Count
        End Get
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

    Dim fStatus As PublicEnum.PublicStatus
    <XafDisplayName("Ê¶Ò¹Ð")> _
    Public Property Status() As PublicEnum.PublicStatus
        Get
            Return fStatus
        End Get
        Set(ByVal value As PublicEnum.PublicStatus)
            SetPropertyValue(Of PublicEnum.PublicStatus)("Status", fStatus, value)
        End Set
    End Property
    <Association("FarmerReferencesFarmerGroup", GetType(Farmer))> _
    Public ReadOnly Property Farmers() As XPCollection(Of Farmer)
        Get
            Return GetCollection(Of Farmer)("Farmers")
        End Get
    End Property
End Class
