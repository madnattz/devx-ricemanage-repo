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
Public Class PlanBudgetDetailMonth01 ' Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        DataOwner = GetCurrentSite()
        MyBase.AfterConstruction()
        ' Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).
    End Sub

    <VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    Public ReadOnly Property ChkQuarter As Integer
        Get
            Dim Quarter As Integer '= Today.Month
            Select Case Quarter
                Case 10, 11, 12
                    Return 1
                Case 1, 2, 3
                    Return 2
                Case 4, 5, 6
                    Return 3
                Case 7, 8, 9
                    Return 4
            End Select
        End Get
    End Property

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
    <ModelDefault("DisplayFormat", "{0:N2}")> _
    <ModelDefault("EditMask", "N")> _
    <ImmediatePostData> _
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
    <ModelDefault("DisplayFormat", "{0:N2}")> _
    <ModelDefault("EditMask", "N")> _
    <ImmediatePostData> _
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
    <ModelDefault("DisplayFormat", "{0:N2}")> _
    <ModelDefault("EditMask", "N")> _
    <ImmediatePostData> _
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
    <ModelDefault("DisplayFormat", "{0:N2}")> _
    <ModelDefault("EditMask", "N")> _
    <ImmediatePostData> _
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
    <ModelDefault("DisplayFormat", "{0:N2}")> _
    <ModelDefault("EditMask", "N")> _
    <ImmediatePostData> _
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
    <ModelDefault("DisplayFormat", "{0:N2}")> _
    <ModelDefault("EditMask", "N")> _
    <ImmediatePostData> _
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
    <ModelDefault("DisplayFormat", "{0:N2}")> _
    <ModelDefault("EditMask", "N")> _
    <ImmediatePostData> _
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
    <ModelDefault("DisplayFormat", "{0:N2}")> _
    <ModelDefault("EditMask", "N")> _
    <ImmediatePostData> _
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
    <ModelDefault("DisplayFormat", "{0:N2}")> _
    <ModelDefault("EditMask", "N")> _
    <ImmediatePostData> _
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
    <ModelDefault("DisplayFormat", "{0:N2}")> _
    <ModelDefault("EditMask", "N")> _
    <ImmediatePostData> _
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
    <ModelDefault("DisplayFormat", "{0:N2}")> _
    <ModelDefault("EditMask", "N")> _
    <ImmediatePostData> _
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
    <ModelDefault("DisplayFormat", "{0:N2}")> _
    <ModelDefault("EditMask", "N")> _
    <ImmediatePostData> _
    Public Property September() As Double
        Get
            Return _September
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("September", _September, value)
        End Set
    End Property
    <XafDisplayName("ผลรวม")> _
    <VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    <ModelDefault("DisplayFormat", "{0:N2}")> _
    <ModelDefault("EditMask", "N")> _
    <ImmediatePostData()> _
    Public ReadOnly Property SUM As Double
        Get
            Return (October + November + December + January + February + March + April + May + June + July + August + September)
        End Get
    End Property

    Private _PlanQuarter1 As Double
    <XafDisplayName("แผนไตรมาส 1 (ต.ค. - ธ.ค.)")> _
    <VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    <ImmediatePostData> _
    Public ReadOnly Property PlanQuarter1() As Double
        Get
            Return (October + November + December)
        End Get
    End Property

    Private _BudgetQuarter1 As Double
    <XafDisplayName("งบประมาณจัดสรร (ต.ค. - ธ.ค.)")> _
    <VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    <ImmediatePostData> _
    Public ReadOnly Property BudgetQuarter1() As Double
        Get
            Return (October + November + December)
        End Get
    End Property

    Private _PlanQuarter2 As Double
    <XafDisplayName("แผนไตรมาส 2 (ม.ค. - มี.ค.)")> _
    <VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    <ImmediatePostData> _
    Public ReadOnly Property PlanQuarter2() As Double
        Get
            Return (January + February + March)
        End Get
    End Property

    Private _BudgetQuarter2 As Double
    <XafDisplayName("งบประมาณจัดสรร (ม.ค. - มี.ค.)")> _
    <VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    <ImmediatePostData> _
    Public ReadOnly Property BudgetQuarter2() As Double
        Get
            Return (January + February + March)
        End Get
    End Property

    Private _PlanQuarter3 As Double
    <XafDisplayName("แผนไตรมาส 3 (เม.ย. - มิ.ย.)")> _
    <VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    <ImmediatePostData> _
    Public ReadOnly Property PlanQuarter3() As Double
        Get
            Return (April + May + June)
        End Get
    End Property

    Private _BudgetQuarter3 As Double
    <XafDisplayName("งบประมาณจัดสรร (เม.ย. - มิ.ย.)")> _
    <VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    <ImmediatePostData> _
    Public ReadOnly Property BudgetQuarter3() As Double
        Get
            Return (April + May + June)
        End Get
    End Property

    Private _PlanQuarter4 As Double
    <XafDisplayName("แผนไตรมาส 4 (ก.ค. - ก.ย.)")> _
    <VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    <ImmediatePostData> _
    Public ReadOnly Property PlanQuarter4() As Double
        Get
            Return (July + August + September)
        End Get
    End Property

    Private _BudgetQuarter4 As Double
    <XafDisplayName("งบประมาณจัดสรร (ก.ค. - ก.ย.)")> _
    <VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    <ImmediatePostData> _
    Public ReadOnly Property BudgetQuarter4() As Double
        Get
            Return (July + August + September)
        End Get
    End Property

#Region "==========Association============"
    Dim _PlanBudget01 As PlanBudgetDetail01
    <Association("PlanBudgetDetail01-PlanBudgetDetailMonth01")> _
    <Index(0)> _
    <VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    Public Property PlanBudget01() As PlanBudgetDetail01
        Get
            Return _PlanBudget01
        End Get
        Set(ByVal value As PlanBudgetDetail01)
            SetPropertyValue(Of PlanBudgetDetail01)("PlanBudget01", _PlanBudget01, value)
        End Set
    End Property
#End Region

    Private _PlaningStatus As StausAppove
    <XafDisplayName("สถานะ")> _
    <Index(10)> _
    <RuleRequiredField(DefaultContexts.Save), VisibleInDetailView(False), VisibleInListView(False)> _
    Public Property PlaningStatus() As StausAppove
        Get
            Return _PlaningStatus
        End Get
        Set(ByVal value As StausAppove)
            SetPropertyValue("PlaningStatus", _PlaningStatus, value)
        End Set
    End Property


    '<Action(Caption:="My UI Action", ConfirmationMessage:="Are you sure?", ImageName:="Attention", AutoCommit:=True)> _
    'Public Sub ActionMethod()
    '    ' Trigger a custom business logic for the current record in the UI (http://documentation.devexpress.com/#Xaf/CustomDocument2619).
    '    Me.PersistentProperty = "Paid"
    'End Sub
End Class
