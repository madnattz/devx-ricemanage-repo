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
Public Class Farm ' Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        Me.Address = New CustomAddress(Session)
        Me.Status = PublicEnum.PublicStatus.Active
        DataOwner = GetCurrentSite()
        ' Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).
    End Sub

    Protected Overrides Sub OnSaving()
        Me.LastUodateDate = Date.Now
        Me.LastUpdateBy = CType(SecuritySystem.CurrentUser, User).DisplayName

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

    Public ReadOnly Property FullName As String
        Get
            If Address IsNot Nothing Then
                Return "แปลงที่ " & ItemNo & " บ้าน " & Address.Village & " พื้นที่ " & Area & " ไร่"
            Else
                Return "แปลงที่ " & ItemNo & " พื้นที่ " & Area & " ไร่"
            End If

        End Get
    End Property

    Dim fItemNo As Integer
    <RuleRequiredField("Farm.ItemNo", DefaultContexts.Save)> _
    Public Property ItemNo() As Integer
        Get
            Return fItemNo
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ItemNo", fItemNo, value)
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
    Dim fArea As Double
    <RuleRequiredField("Farm.Area", DefaultContexts.Save)> _
    Public Property Area() As Double
        Get
            Return fArea
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("Area", fArea, value)
        End Set
    End Property
    Dim fFarmer As Farmer
    <Association("FarmReferencesFarmer")> _
    Public Property Farmer() As Farmer
        Get
            Return fFarmer
        End Get
        Set(ByVal value As Farmer)
            SetPropertyValue(Of Farmer)("Farmer", fFarmer, value)
        End Set
    End Property
    Dim fStatus As PublicEnum.PublicStatus
    Public Property Status() As PublicEnum.PublicStatus
        Get
            Return fStatus
        End Get
        Set(ByVal value As PublicEnum.PublicStatus)
            SetPropertyValue(Of PublicEnum.PublicStatus)("Status", fStatus, value)
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
    Dim fDistanceBetweenSite As Double
    <XafDisplayName("ระยะห่างจากศูนย์ฯ (กม.)")> _
    Public Property DistanceBetweenSite() As Double
        Get
            Return fDistanceBetweenSite
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("DistanceBetweenSite", fDistanceBetweenSite, value)
        End Set
    End Property
    Dim fLatitude As String
    Public Property Latitude() As String
        Get
            Return fLatitude
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Latitude", fLatitude, value)
        End Set
    End Property
    Dim fLongitude As String
    Public Property Longitude() As String
        Get
            Return fLongitude
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Longitude", fLongitude, value)
        End Set
    End Property

End Class
