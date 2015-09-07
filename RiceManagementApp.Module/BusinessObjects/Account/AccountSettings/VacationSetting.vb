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
<NavigationItem("รายการเบิกจ่ายค่าเงินค่าเมล็ดพันธุ์")> _
<XafDisplayName("วันหยุดราชการทั่วไป")> _
<DefaultClassOptions()> _
Public Class VacationSetting ' Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        ' Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).

    End Sub

    Private _Day As PublicDay
    <XafDisplayName("วันที่")> _
    Public Property Day() As PublicDay
        Get
            Return _Day
        End Get
        Set(ByVal value As PublicDay)
            SetPropertyValue("Day", _Day, value)
        End Set
    End Property

    Private _PublicMonth As PublicEnum.EnumMonth
    <XafDisplayName("เดือน")> _
    Public Property PublicMonth() As PublicEnum.EnumMonth
        Get
            Return _PublicMonth
        End Get
        Set(ByVal value As PublicEnum.EnumMonth)
            SetPropertyValue("PublicMonth", _PublicMonth, value)
        End Set
    End Property

    Private _Description As String
    <XafDisplayName("หมายเหตุ")> _
    Public Property Description() As String
        Get
            Return _Description
        End Get
        Set(ByVal value As String)
            SetPropertyValue("Description", _Description, value)
        End Set
    End Property

    Private _PublicStatus As PublicStatus
    <XafDisplayName("สถานะ")> _
    <Index(6), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    Public Property PublicStatus() As PublicStatus
        Get
            Return _PublicStatus
        End Get
        Set(ByVal value As PublicStatus)
            SetPropertyValue("PublicStatus", _PublicStatus, value)
        End Set
    End Property
    Public Enum PublicDay
        <XafDisplayName("1")> _
        one = 1
        <XafDisplayName("2")> _
        two = 2
        <XafDisplayName("3")> _
        three = 3
        <XafDisplayName("4")> _
        four = 4
        <XafDisplayName("5")> _
        five = 5
        <XafDisplayName("6")> _
        six = 6
        <XafDisplayName("7")> _
        seven = 7
        <XafDisplayName("8")> _
        eight = 8
        <XafDisplayName("9")> _
        nine = 9
        <XafDisplayName("10")> _
        ten = 10
        <XafDisplayName("11")> _
        eleven = 11
        <XafDisplayName("12")> _
        twelve = 12
        <XafDisplayName("13")> _
        thirteen = 13
        <XafDisplayName("14")> _
        fourteen = 14
        <XafDisplayName("15")> _
        fifteen = 15
        <XafDisplayName("16")> _
        sixteen = 16
        <XafDisplayName("17")> _
        seventeen = 17
        <XafDisplayName("18")> _
        eighteen = 18
        <XafDisplayName("19")> _
        nineteen = 19
        <XafDisplayName("20")> _
        twenty = 20
        <XafDisplayName("21")> _
        twentyone = 21
        <XafDisplayName("22")> _
        twentytwo = 22
        <XafDisplayName("23")> _
        twentythree = 23
        <XafDisplayName("24")> _
        twentyfour = 24
        <XafDisplayName("25")> _
        twentyfive = 25
        <XafDisplayName("26")> _
        twentysix = 26
        <XafDisplayName("27")> _
        twentyseven = 27
        <XafDisplayName("28")> _
        twentyeight = 28
        <XafDisplayName("29")> _
        twentynine = 29
        <XafDisplayName("30")> _
        thirty = 30
        <XafDisplayName("31")> _
        thirtyone = 31
    End Enum
End Class
