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
<NavigationItem("พยากรณ์รายได้")> _
<DefaultClassOptions(), XafDisplayName("พยากรณ์รายได้")> _
Public Class Forecast ' Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        DataOwner = GetCurrentSite()
        MyBase.AfterConstruction()
        Docudate = Today
        ' Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).
    End Sub

  Protected Overrides Sub OnSaving()
        MyBase.OnSaving()
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

    Private _DocuNo As String
    <XafDisplayName("เลขที่เอกสาร")> _
    <Index(1), VisibleInListView(True)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property DocuNo() As String
        Get
            Return _DocuNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue("DocuNo", _DocuNo, value)
        End Set
    End Property

    Private _Docudate As Date
    <XafDisplayName("วันที่เอกสาร")> _
    <Index(2), VisibleInListView(True)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property Docudate() As Date
        Get
            Return _Docudate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue("Docudate", _Docudate, value)
        End Set
    End Property

    Private _OtherCostID As Double
    <XafDisplayName("ค่าใช้จ่ายอื่นๆ : บาท/กก.")> _
    <ModelDefault("DisplayFormat", "{0:N2}")> _
   <ModelDefault("EditMask", "N")> _
    <Index(5), VisibleInListView(True)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property OtherCostID() As Double
        Get
            Return _OtherCostID
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("OtherCostID", _OtherCostID, value)
        End Set
    End Property

    Private _BudgetID As Double
    <XafDisplayName("งบตวป : บาท")> _
    <ModelDefault("DisplayFormat", "{0:N2}")> _
   <ModelDefault("EditMask", "N")> _
    <Index(6), VisibleInListView(True)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property BudgetID() As Double
        Get
            Return _BudgetID
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("BudgetID", _BudgetID, value)
        End Set
    End Property

#Region "==========Association============"
    <XafDisplayName("ข้อมูลพยาการรายได้")> _
    <Association("Forecast-ForecastDetail", GetType(ForecastDetail))> _
    <DevExpress.Xpo.Aggregated>
    Public ReadOnly Property ForecastDetails() As XPCollection(Of ForecastDetail)
        Get
            Return GetCollection(Of ForecastDetail)("ForecastDetails")
        End Get
    End Property
#End Region


End Class
