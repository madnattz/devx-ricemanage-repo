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
Public Class ReceiveTeam ' Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        ' Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).
    End Sub

    Dim fApproveBuy As ApproveBuy
    '<Association("ReceiveTeamReferencesApproveBuy")> _
    Public Property ApproveBuy() As ApproveBuy
        Get
            Return fApproveBuy
        End Get
        Set(ByVal value As ApproveBuy)
            SetPropertyValue(Of ApproveBuy)("ApproveBuy", fApproveBuy, value)
        End Set
    End Property
    Dim fEmployee As Employee
    Public Property Employee() As Employee
        Get
            Return fEmployee
        End Get
        Set(ByVal value As Employee)
            SetPropertyValue(Of Employee)("Employee", fEmployee, value)
        End Set
    End Property
    Dim fBuyPosition As BuyPosition
    Public Property BuyPosition() As BuyPosition
        Get
            Return fBuyPosition
        End Get
        Set(ByVal value As BuyPosition)
            SetPropertyValue(Of BuyPosition)("BuyPosition", fBuyPosition, value)
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
