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
Public Class Employee ' Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        ' Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).
    End Sub

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
    Public Property PrefixName() As PrefixName
        Get
            Return fPrefixName
        End Get
        Set(ByVal value As PrefixName)
            SetPropertyValue(Of PrefixName)("PrefixName", fPrefixName, value)
        End Set
    End Property
    Dim fFirstname As String
    Public Property Firstname() As String
        Get
            Return fFirstname
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Firstname", fFirstname, value)
        End Set
    End Property
    Dim fLastname As String
    Public Property Lastname() As String
        Get
            Return fLastname
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Lastname", fLastname, value)
        End Set
    End Property

    Dim fPosition As String
    Public Property Position() As String
        Get
            Return fPosition
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Position", fPosition, value)
        End Set
    End Property
   
    Dim fTelNo As String
    <Size(20)> _
    Public Property TelNo() As String
        Get
            Return fTelNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("TelNo", fTelNo, value)
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
