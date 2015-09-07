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
Imports DevExpress.ExpressApp.ConditionalAppearance

'<ImageName("BO_Contact")> _
'<DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")> _
'<DefaultListViewOptions(MasterDetailMode.ListViewOnly, False, NewItemRowPosition.None)> _
'<Persistent("DatabaseTableName")> _
<NavigationItem("ชมรมและสมาคม")>
<DefaultClassOptions(), XafDisplayName("รายชื่อสมาชิก"), DefaultProperty("FullName")> _
Public Class SeedMembers ' Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        Address = New CustomAddress(Session)
        ' Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).
    End Sub


    Dim _PrefixName As PrefixName
    <XafDisplayName("คำนำหน้า")> _
    <VisibleInDetailView(True), VisibleInListView(False)> _
    Public Property PrefixName() As PrefixName
        Get
            Return _PrefixName
        End Get
        Set(ByVal value As PrefixName)
            SetPropertyValue(Of PrefixName)("PrefixName", _PrefixName, value)
        End Set
    End Property

    Dim _Firstname As String
    <XafDisplayName("ชื่อ")> _
    <VisibleInDetailView(True), VisibleInListView(False)> _
    Public Property Firstname() As String
        Get
            Return _Firstname
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Firstname", _Firstname, value)
        End Set
    End Property

    Dim _Lastname As String
    <VisibleInDetailView(True), VisibleInListView(False)> _
    <XafDisplayName("นามสกุล")> _
    Public Property Lastname() As String
        Get
            Return _Lastname
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Lastname", _Lastname, value)
        End Set
    End Property

    <XafDisplayName("ชื่อ - นามสกุล")> _
    <VisibleInDetailView(False), VisibleInListView(True)> _
    Public ReadOnly Property FullName As String
        Get
            If PrefixName IsNot Nothing Then
                Return PrefixName.FrefixName & Firstname & "  " & Lastname
            Else
                Return Firstname & "  " & Lastname
            End If

        End Get
    End Property

    Dim _PersonCardID As String
    <Size(13)> _
    <VisibleInListView(False)> _
    <XafDisplayName("เลขบัตรประชาชน")> _
    Public Property PersonCardID() As String
        Get
            Return _PersonCardID
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PersonCardID", _PersonCardID, value)
        End Set
    End Property


    Dim _Address As CustomAddress
    <DC.Aggregated, ExpandObjectMembers(ExpandObjectMembers.Always)> _
    <VisibleInListView(False)> _
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

    Dim _BusinessClass As BusinessClass
    <XafDisplayName("ประเภทธุรกิจ")> _
    <ImmediatePostData> _
    <VisibleInDetailView(True), VisibleInListView(False)> _
    Public Property BusinessClass() As BusinessClass
        Get
            Return _BusinessClass
        End Get
        Set(ByVal value As BusinessClass)
            SetPropertyValue(Of BusinessClass)("BusinessClass", _BusinessClass, value)
            OnChanged("BusinessName")
        End Set
    End Property

    Dim _BusinessName As String
    <XafDisplayName("ชื่อธุรกิจ")> _
    Public Property BusinessName() As String
        Get
            Return _BusinessName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("BusinessName", _BusinessName, value)
        End Set
    End Property

    

    Dim _Status As PublicEnum.PublicStatus
    <VisibleInDetailView(False), VisibleInListView(False)> _
    Public Property Status() As PublicEnum.PublicStatus
        Get
            Return _Status
        End Get
        Set(ByVal value As PublicEnum.PublicStatus)
            SetPropertyValue(Of Integer)("Status", _Status, value)
        End Set
    End Property

    Dim _LastUpdateBy As String
    <VisibleInDetailView(False), VisibleInListView(False)> _
    Public Property LastUpdateBy() As String
        Get
            Return _LastUpdateBy
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("LastUpdateBy", _LastUpdateBy, value)
        End Set
    End Property

    Dim _LastUodateDate As DateTime
    <VisibleInDetailView(False), VisibleInListView(False)> _
    Public Property LastUodateDate() As DateTime
        Get
            Return _LastUodateDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("LastUodateDate", _LastUodateDate, value)
        End Set
    End Property

    Protected Overrides Sub OnDeleting()
        MyBase.OnDeleting()
        Status = PublicEnum.PublicStatus.DeActive
    End Sub

    Dim _SeedClub As SeedClubs
    <Association("SeedClub-SeedMembers")> _
    <XafDisplayName("ชมรม")> _
    <Appearance("DISSeedClub", Enabled:=False, Context:="DetailView")> _
    Public Property SeedClub() As SeedClubs
        Get
            Return _SeedClub
        End Get
        Set(ByVal value As SeedClubs)
            SetPropertyValue(Of SeedClubs)("SeedClub", _SeedClub, value)
        End Set
    End Property

    Dim _Position As Positions
    <XafDisplayName("ตำแหน่งชมรม")> _
    Public Property Position() As Positions
        Get
            Return _Position
        End Get
        Set(ByVal value As Positions)
            SetPropertyValue(Of Positions)("Position", _Position, value)
        End Set
    End Property


    Dim _AssPosition As Positions
    <XafDisplayName("ตำแหน่งสมาคม")> _
    Public Property AssPosition() As Positions
        Get
            Return _AssPosition
        End Get
        Set(ByVal value As Positions)
            SetPropertyValue(Of Positions)("AssPosition", _AssPosition, value)
        End Set
    End Property
End Class
