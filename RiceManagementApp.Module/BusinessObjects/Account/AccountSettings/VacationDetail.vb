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
Public Class VacationDetail ' Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        ' Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).
    End Sub

    Private _Vacation As Vacation
    <Association("Vacation-VacationDetails")> _
    <XafDisplayName("ปี")> _
    Public Property Vacation() As Vacation
        Get
            Return _Vacation
        End Get
        Set(ByVal value As Vacation)
            SetPropertyValue("Vacation", _Vacation, value)
        End Set
    End Property

    Private _VacationDate As Date
    <XafDisplayName("วันที่หยุดราชการ")> _
    Public Property VacationDate() As Date
        Get
            Return _VacationDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue("VacationDate", _VacationDate, value)
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
End Class
