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
<DefaultClassOptions(), ImageName("BO_Task")> _
Public Class ResultBudget02 ' Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        DataOwner = GetCurrentSite()
        MyBase.AfterConstruction()
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
    Private _October As Double
    <XafDisplayName("ต.ค.")> _
    <ImmediatePostData> _
    <VisibleInListView(True), VisibleInDetailView(False)> _
    Public Property October() As Double
        Get
            Return _October
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("October", _October, value)
        End Set
    End Property

    Private _November As Double
    <XafDisplayName("พ.ย.")> _
    <ImmediatePostData> _
  <VisibleInListView(True), VisibleInDetailView(False)> _
    Public Property November() As Double
        Get
            Return _November
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("November", _November, value)
        End Set
    End Property
    Private _December As Double
    <XafDisplayName("ธ.ค.")> _
    <ImmediatePostData> _
  <VisibleInListView(True), VisibleInDetailView(False)> _
    Public Property December() As Double
        Get
            Return _December
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("December", _December, value)
        End Set
    End Property

    '======================Quarter 2 =========================

    Private _January As Double
    <XafDisplayName("ม.ค.")> _
    <ImmediatePostData> _
  <VisibleInListView(True), VisibleInDetailView(False)> _
    Public Property January() As Double
        Get
            Return _January
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("January", _January, value)
        End Set
    End Property

    Private _February As Double
    <XafDisplayName("ก.พ.")> _
    <ImmediatePostData> _
  <VisibleInListView(True), VisibleInDetailView(False)> _
    Public Property February() As Double
        Get
            Return _February
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("February", _February, value)
        End Set
    End Property

    Private _March As Double
    <XafDisplayName("มี.ค.")> _
    <ImmediatePostData> _
  <VisibleInListView(True), VisibleInDetailView(False)> _
    Public Property March() As Double
        Get
            Return _March
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("March", _March, value)
        End Set
    End Property

    '======================Quarter 3 =========================

    Private _April As Double
    <XafDisplayName("เม.ย.")> _
    <ImmediatePostData> _
  <VisibleInListView(True), VisibleInDetailView(False)> _
    Public Property April() As Double
        Get
            Return _April
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("April", _April, value)
        End Set
    End Property

    Private _May As Double
    <XafDisplayName("พ.ค.")> _
    <ImmediatePostData> _
  <VisibleInListView(True), VisibleInDetailView(False)> _
    Public Property May() As Double
        Get
            Return _May
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("May", _May, value)
        End Set
    End Property

    Private _June As Double
    <XafDisplayName("มิ.ย.")> _
    <ImmediatePostData> _
  <VisibleInListView(True), VisibleInDetailView(False)> _
    Public Property June() As Double
        Get
            Return _June
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("June", _June, value)
        End Set
    End Property

    '======================Quarter 4 =========================

    Private _July As Double
    <XafDisplayName("ก.ค.")> _
    <ImmediatePostData> _
  <VisibleInListView(True), VisibleInDetailView(False)> _
    Public Property July() As Double
        Get
            Return _July
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("July", _July, value)
        End Set
    End Property

    Private _August As Double
    <XafDisplayName("ส.ค.")> _
    <ImmediatePostData> _
  <VisibleInListView(True), VisibleInDetailView(False)> _
    Public Property August() As Double
        Get
            Return _August
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("August", _August, value)
        End Set
    End Property

    Private _September As Double
    <XafDisplayName("ก.ย.")> _
    <ImmediatePostData> _
  <VisibleInListView(True), VisibleInDetailView(False)> _
    Public Property September() As Double
        Get
            Return _September
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("September", _September, value)
        End Set
    End Property

#Region "==========Association============"
    Dim _PlanBudget02 As PlanBudgetDetail02
    <Association("PlanBudgetDetail02-ResultBudget02")> _
    <Index(0)> _
    <VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    Public Property PlanBudget02() As PlanBudgetDetail02
        Get
            Return _PlanBudget02
        End Get
        Set(ByVal value As PlanBudgetDetail02)
            SetPropertyValue(Of PlanBudgetDetail02)("PlanBudget02", _PlanBudget02, value)
        End Set
    End Property
#End Region

    <XafDisplayName("รวมทั้งสิ้น / ผล"), VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)> _
    Public ReadOnly Property SumResult As Double
        Get
            Return (October + November + December + January + February + March + April + May + June + July + August + September)
        End Get
    End Property

End Class
