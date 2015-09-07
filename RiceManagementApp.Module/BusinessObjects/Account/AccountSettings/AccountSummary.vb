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
Imports DevExpress.Xpo.DB

'<ImageName("BO_Contact")> _
'<DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")> _
'<DefaultListViewOptions(MasterDetailMode.ListViewOnly, False, NewItemRowPosition.None)> _
'<Persistent("DatabaseTableName")> _
<DeferredDeletion(False)> _
<XafDisplayName("รายการงวดบัญชีxxxx")> _
<NonPersistent()> _
Public Class AccountSummary ' Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        ' Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).
    End Sub


    Private _BringForword As Double
    <XafDisplayName("รายได้สูงกว่ารายจ่ายสะสมปีก่อน (บาท)")> _
    Public Property BringForword() As Double
        Get
            Return _BringForword
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("BringForword", _BringForword, value)
        End Set

    End Property

    Private _CurrentSummary As Double
    <XafDisplayName("รายได้สูงกว่า(ต่ำกว่า)รายจ่ายสะสมปีปัจจุบัน (บาท)")> _
    Public Property CurrentSummary() As Double
        Get
            Return _CurrentSummary
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("CurrentSummary", _CurrentSummary, value)
        End Set
    End Property

    Private _CumulativeSummary As Double
    <XafDisplayName("รายได้สูงกว่ารายจ่ายสะสม")> _
    Public Property CumulativeSummary() As Double
        Get
            Return _CumulativeSummary
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("CumulativeSummary", _CumulativeSummary, value)
        End Set
    End Property

    '<Action(Caption:="My UI Action", ConfirmationMessage:="Are you sure?", ImageName:="Attention", AutoCommit:=True)> _
    'Public Sub ActionMethod()
    '    ' Trigger a custom business logic for the current record in the UI (http://documentation.devexpress.com/#Xaf/CustomDocument2619).
    '    Me.PersistentProperty = "Paid"
    'End Sub

End Class
