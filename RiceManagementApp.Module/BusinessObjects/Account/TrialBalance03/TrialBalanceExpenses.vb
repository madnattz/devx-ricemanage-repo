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
<Appearance("EnabledTrialBalanceTransferDetailExpenses03", AppearanceItemType:="ViewItem", TargetItems:="*", Context:="ListView", Criteria:="CodeGF_Oid = NULL", BackColor:="224, 224, 224", Enabled:=False)> _
<DefaultClassOptions()> _
Public Class TrialBalanceExpenses ' Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        DataOwner = GetCurrentSite()
        MyBase.AfterConstruction()
        ' Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).
    End Sub

    Protected Overrides Sub OnSaved()
        MyBase.OnSaved()
    End Sub

    Private _TrialBalanceExpenses As IncomeAndExpensesOfMonth
    <Association("IncomeAndExpensesOfMonth-TrialBalanceExpenses")> _
    <Index(0), VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)> _
    <ImmediatePostData()> _
    Public Property TrialBalanceExpenses() As IncomeAndExpensesOfMonth
        Get
            Return _TrialBalanceExpenses
        End Get
        Set(ByVal value As IncomeAndExpensesOfMonth)
            SetPropertyValue("TrialBalanceExpenses", _TrialBalanceExpenses, value)
        End Set
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

    'Private _MonthTrialBalance As PublicEnum.EnumMonth
    '<XafDisplayName("เดือน")> _
    '<Index(0), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    '<RuleRequiredField(DefaultContexts.Save)> _
    'Public Property MonthTrialBalance() As PublicEnum.EnumMonth
    '    Get
    '        Return _MonthTrialBalance
    '    End Get
    '    Set(ByVal value As PublicEnum.EnumMonth)
    '        SetPropertyValue("MonthTrialBalance", _MonthTrialBalance, value)
    '    End Set
    'End Property

    Private _listID As SettingExpenses
    <XafDisplayName("รหัส")> _
    <Index(0), VisibleInListView(True), VisibleInDetailView(False), VisibleInLookupListView(True)> _
    Public Property listID() As SettingExpenses
        Get
            Return _listID
        End Get
        Set(ByVal value As SettingExpenses)
            SetPropertyValue("listID", _listID, value)
        End Set
    End Property

    Private _listExpenses As String
    <XafDisplayName("รายการ")> _
    <Index(1), VisibleInListView(True), VisibleInDetailView(False), VisibleInLookupListView(True)> _
    Public ReadOnly Property listExpenses() As String
        Get
            If listExpenses Is Nothing Then
                listExpenses = listID.listExpenses
            End If
        End Get
    End Property

    Private _CodeGF_Oid As String
    <XafDisplayName("รหัสGF")> _
    <Index(2), VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)> _
    Public Property CodeGF_Oid() As String
        Get
            Return _CodeGF_Oid
        End Get
        Set(ByVal value As String)
            SetPropertyValue("CodeGF_Oid", _CodeGF_Oid, value)
        End Set
    End Property

    Private _CodeGF_Name As String
    <XafDisplayName("ชื่อGF")> _
    <Index(2), VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)> _
    Public Property CodeGF_Name() As String
        Get
            Return _CodeGF_Name
        End Get
        Set(ByVal value As String)
            SetPropertyValue("CodeGF_Name", _CodeGF_Name, value)
        End Set
    End Property

    Private _IncomeMonth As Double
    <XafDisplayName("ได้รับโอนเงิน เดือนนี้")> _
    <Index(3), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    <ImmediatePostData()> _
    Public Property IncomeMonth() As Double
        Get
            Return _IncomeMonth
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("IncomeMonth", _IncomeMonth, value)
        End Set
    End Property

    Private _ExpendMonth As Double
    <XafDisplayName("ใช้จ่าย เดือนนี้")> _
    <Index(4), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    <ImmediatePostData()> _
    Public Property ExpendMonth() As Double
        Get
            Return _ExpendMonth
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("ExpendMonth", _ExpendMonth, value)
        End Set
    End Property

    Private _Note As String
    <XafDisplayName("หมายเหตุ")> _
    <Index(5), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    Public Property Note() As String
        Get
            Return _Note
        End Get
        Set(ByVal value As String)
            SetPropertyValue("Note", _Note, value)
        End Set
    End Property

    '<Action(Caption:="My UI Action", ConfirmationMessage:="Are you sure?", ImageName:="Attention", AutoCommit:=True)> _
    Public Sub ActionSend()
        ' Trigger a custom business logic for the current record in the UI (http://documentation.devexpress.com/#Xaf/CustomDocument2619).
        'Dim objDetail As New InvestmentDetail(Session)
        'Select Case GLID
        '    Case "5104040104"
        '        objDetail.listID = listID
        '        objDetail.list = "1. วัสดุใช้ไป"
        '        objDetail.listExpenses = listExpenses
        '        objDetail.GLID = GLID
        '    Case "5104010112"
        '        objDetail.listID = listID
        '        objDetail.list = "2. ค่าใช้จ่ายบริการ"
        '        objDetail.listExpenses = listExpenses
        '        objDetail.GLID = GLID
        '    Case "5104010110"
        '        objDetail.listID = listID
        '        objDetail.list = "3. ค่าปิโตรเลี่ยมใช้งาน"
        '        objDetail.listExpenses = listExpenses
        '        objDetail.GLID = GLID
        '    Case "5104020101"
        '        objDetail.listID = listID
        '        objDetail.list = "4. ค่าไฟฟ้าใช้งาน"
        '        objDetail.listExpenses = listExpenses
        '        objDetail.GLID = GLID
        '    Case "5104010107"
        '        objDetail.listID = listID
        '        objDetail.list = "5. ค่าซ่อมแซมบำรุงรักษา"
        '        objDetail.listExpenses = listExpenses
        '        objDetail.GLID = GLID
        'End Select
    End Sub
End Class
