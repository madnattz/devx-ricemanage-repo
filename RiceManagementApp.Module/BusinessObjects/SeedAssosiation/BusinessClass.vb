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
<NavigationItem("ชมรมและสมาคม")>
<DefaultClassOptions(), XafDisplayName("ประเภทธุรกิจ")> _
Public Class BusinessClass ' Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        ' Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).
    End Sub

    Dim _BusinessClassID As String
    <XafDisplayName("รหัสประเภทธุรกิจ")> _
    Public Property BusinessClassID() As String
        Get
            Return _BusinessClassID
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("BusinessClassID", _BusinessClassID, value)
        End Set
    End Property

    Dim _BusinessClassName As String
    <XafDisplayName("ชื่อประเภทธุรกิจ")> _
    Public Property BusinessClassName() As String
        Get
            Return _BusinessClassName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("BusinessClassName", _BusinessClassName, value)
        End Set
    End Property

    Dim _Status As PublicEnum.PublicStatus
    Public Property Status() As PublicEnum.PublicStatus
        Get
            Return _Status
        End Get
        Set(ByVal value As PublicEnum.PublicStatus)
            SetPropertyValue(Of Integer)("Status", _Status, value)
        End Set
    End Property

End Class
