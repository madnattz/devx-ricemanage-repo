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
<DefaultClassOptions()> _
<Appearance("EnabledTrialBalanceTransferDetailExpenses03", AppearanceItemType:="ViewItem", TargetItems:="*", Context:="ListView", Criteria:="CodeGF_Oid = NULL", BackColor:="224, 224, 224", Enabled:=False)> _
Public Class TrialBalanceTransferDetailExpenses03 ' Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        DataOwner = GetCurrentSite()
        MyBase.AfterConstruction()
        ' Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).
    End Sub

#Region "==========Association============"
    Dim _TrialBalanceTransfer03 As TrialBalanceTransfer03
    <Association("TrialBalanceTransfer03-TrialBalanceTransferDetailExpenses03s")> _
    <VisibleInDetailView(False), VisibleInListView(False)> _
    <Index(0)> _
    Public Property TrialBalanceTransfer03() As TrialBalanceTransfer03
        Get
            Return _TrialBalanceTransfer03
        End Get
        Set(ByVal value As TrialBalanceTransfer03)
            SetPropertyValue(Of TrialBalanceTransfer03)("TrialBalanceTransfer03", _TrialBalanceTransfer03, value)
        End Set
    End Property
#End Region

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

    Private _CatalogueNo As SettingExpenses
    <XafDisplayName("รหัส")> _
    <VisibleInDetailView(False), VisibleInListView(True)> _
    Public Property CatalogueNo() As SettingExpenses
        Get
            Return _CatalogueNo
        End Get
        Set(ByVal value As SettingExpenses)
            SetPropertyValue("CatalogueNo", _CatalogueNo, value)
        End Set
    End Property

    Private _listExpenses As String
    <XafDisplayName("รายการ")> _
    <VisibleInDetailView(False), VisibleInListView(True)> _
    Public ReadOnly Property listExpenses() As String
        Get
            If listExpenses Is Nothing Then
                listExpenses = CatalogueNo.listExpenses
            End If
        End Get
    End Property

    Private _CodeGF_Oid As CodeGFPV
    <XafDisplayName("รหัสGF")> _
    <Index(2), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    Public Property CodeGF_Oid() As CodeGFPV
        Get
            Return _CodeGF_Oid
        End Get
        Set(ByVal value As CodeGFPV)
            SetPropertyValue("CodeGF_Oid", _CodeGF_Oid, value)
        End Set
    End Property

    Private _October As Double
    <XafDisplayName("ต.ค.")> _
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
    <ImmediatePostData> _
    Public Property September() As Double
        Get
            Return _September
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("September", _September, value)
        End Set
    End Property

    Private _PlanQuarter1 As Double
    <XafDisplayName("รวมไตรมาส 1 (ต.ค. - ธ.ค.)")> _
    <VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    <ImmediatePostData> _
    Public ReadOnly Property PlanQuarter1() As Double
        Get
            Return (October + November + December)
        End Get
    End Property

    Private _PlanQuarter2 As Double
    <XafDisplayName("รวมไตรมาส 2 (ม.ค. - มี.ค.)")> _
    <VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    <ImmediatePostData> _
    Public ReadOnly Property PlanQuarter2() As Double
        Get
            Return (January + February + March)
        End Get
    End Property

    Private _PlanQuarter3 As Double
    <XafDisplayName("รวมไตรมาส 3 (เม.ย. - มิ.ย.)")> _
    <VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    <ImmediatePostData> _
    Public ReadOnly Property PlanQuarter3() As Double
        Get
            Return (April + May + June)
        End Get
    End Property

    Private _PlanQuarter4 As Double
    <XafDisplayName("รวมไตรมาส 4 (ก.ค. - ก.ย.)")> _
    <VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    <ImmediatePostData> _
    Public ReadOnly Property PlanQuarter4() As Double
        Get
            Return (July + August + September)
        End Get
    End Property

    <XafDisplayName("รวมทั้งสิ้น")> _
<VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
<ImmediatePostData()> _
    Public ReadOnly Property SUM As Double
        Get
            Return (October + November + December + January + February + March + April + May + June + July + August + September)
        End Get
    End Property

    '<Action(Caption:="My UI Action", ConfirmationMessage:="Are you sure?", ImageName:="Attention", AutoCommit:=True)> _
    'Public Sub ActionMethod()
    '    ' Trigger a custom business logic for the current record in the UI (http://documentation.devexpress.com/#Xaf/CustomDocument2619).
    '    Me.PersistentProperty = "Paid"
    'End Sub
End Class
