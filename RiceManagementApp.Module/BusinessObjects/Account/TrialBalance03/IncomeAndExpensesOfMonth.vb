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
<DefaultClassOptions(), XafDisplayName("ผลการรับ - จ่าย เงินประจำเดือน")> _
Public Class IncomeAndExpensesOfMonth ' Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        DataOwner = GetCurrentSite()
        MyBase.AfterConstruction()
        ' Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).
        CreateTrialBalanceIncome()
        CreateTrialBalanceExpenses()
    End Sub
    Protected Overrides Sub OnSaved()
        PostToExpend()
        MyBase.OnSaved()
    End Sub

    Public Sub CreateTrialBalanceIncome()
        Dim coTrialBalanceIncome As XPCollection(Of SettingIncome) = New XPCollection(Of SettingIncome)(Session)
        coTrialBalanceIncome.Sorting.Add(New SortProperty("listID", DB.SortingDirection.Ascending))
        For Each item As SettingIncome In coTrialBalanceIncome
            Dim objDetail As New TrialBalanceIncome(Session)
            objDetail.listID = item
            objDetail.CodeGF_Oid = item.CodeGF_ID
            objDetail.CodeGF_Name = item.CodeGF_Name
            objDetail.TrialBalanceIncomes = Me
            objDetail.Save()
        Next

        'fRefTransaction = New XPCollection(Of FactoryTransaction)(Session, CriteriaOperator.Parse("SeedProduct.Oid=? And FactoryNo=? ", Me.fSeedProduct.Oid, Me.fFactoryNo))
    End Sub

    Public Sub CreateTrialBalanceExpenses()
        Dim coTrialBalanceExpenses As XPCollection(Of SettingExpenses) = New XPCollection(Of SettingExpenses)(Session)
        coTrialBalanceExpenses.Sorting.Add(New SortProperty("listID", DB.SortingDirection.Ascending))
        For Each item As SettingExpenses In coTrialBalanceExpenses
            Dim objDetail As New TrialBalanceExpenses(Session)
            objDetail.listID = item
            objDetail.CodeGF_Oid = item.CodeGF_ID
            objDetail.CodeGF_Name = item.CodeGF_Name
            objDetail.TrialBalanceExpenses = Me
            objDetail.Save()
        Next
        'fRefTransaction = New XPCollection(Of FactoryTransaction)(Session, CriteriaOperator.Parse("SeedProduct.Oid=? And FactoryNo=? ", Me.fSeedProduct.Oid, Me.fFactoryNo))
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

    Private _YearID As String
    <XafDisplayName("ปี")> _
    <Index(0), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    Public Property YearID() As String
        Get
            Return _YearID
        End Get
        Set(ByVal value As String)
            SetPropertyValue("YearID", _YearID, value)
        End Set
    End Property

    Private _MonthTrialBalance As PublicEnum.EnumMonth
    <XafDisplayName("เดือน")> _
    <Index(1), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property MonthTrialBalance() As PublicEnum.EnumMonth
        Get
            Return _MonthTrialBalance
        End Get
        Set(ByVal value As PublicEnum.EnumMonth)
            SetPropertyValue("MonthTrialBalance", _MonthTrialBalance, value)
        End Set
    End Property

    <XafDisplayName("รายงาน ได้รับโอนเงิน")> _
<Association("IncomeAndExpensesOfMonth-TrialBalanceIncomes", GetType(TrialBalanceIncome))> _
<DC.Aggregated()> _
    Public ReadOnly Property TrialBalanceIncomes() As XPCollection(Of TrialBalanceIncome)
        Get
            Return GetCollection(Of TrialBalanceIncome)("TrialBalanceIncomes")
        End Get
    End Property

    <XafDisplayName("รายงาน ใช้จ่าย")> _
<Association("IncomeAndExpensesOfMonth-TrialBalanceExpenses", GetType(TrialBalanceExpenses))> _
<DC.Aggregated()> _
    Public ReadOnly Property TrialBalanceExpenses() As XPCollection(Of TrialBalanceExpenses)
        Get
            Return GetCollection(Of TrialBalanceExpenses)("TrialBalanceExpenses")
        End Get
    End Property

    Public Sub PostToExpend()
        Try
            Dim colExpendType As XPCollection(Of TrialBalanceExpenses) = New XPCollection(Of TrialBalanceExpenses)(Session, CriteriaOperator.Parse("TrialBalanceExpenses.MonthTrialBalance=? and TrialBalanceExpenses.YearID=?", MonthTrialBalance, YearID))
            Dim colIncomeType As XPCollection(Of TrialBalanceIncome) = New XPCollection(Of TrialBalanceIncome)(Session, CriteriaOperator.Parse("TrialBalanceIncomes.MonthTrialBalance=? and TrialBalanceIncomes.YearID=?", MonthTrialBalance, YearID))
            Dim objDate As New AccountGFMIS(Session)
            For Each itempv As TrialBalanceExpenses In colExpendType
                If itempv.ExpendMonth <> 0 Then
                    Dim objDetailPV As New AccountGFMISDetailPV(Session)
                    If itempv.CodeGF_Oid IsNot Nothing Then
                        objDate.MonthTrialBalance = MonthTrialBalance
                        objDate.YearID = YearID
                        objDetailPV.GFMIS = objDate
                        objDetailPV.listGF = itempv.CodeGF_Name
                        objDetailPV.CodeGF = itempv.CodeGF_Oid
                        objDetailPV.AmountExpend = itempv.ExpendMonth
                        objDetailPV.Save()
                    End If
                End If
            Next
            For Each itemrv As TrialBalanceIncome In colIncomeType
                Dim objDetailRV As New AccountGFMISDetailRV(Session)
                If itemrv.ExpendMonth <> 0 Then
                    If itemrv.CodeGF_Oid IsNot Nothing Then
                        objDate.MonthTrialBalance = MonthTrialBalance
                        objDate.YearID = YearID
                        objDetailRV.GFMIS = objDate
                        objDetailRV.listGF = itemrv.CodeGF_Name
                        objDetailRV.CodeGF = itemrv.CodeGF_Oid
                        objDetailRV.AmountIncome = itemrv.ExpendMonth
                        objDetailRV.Save()
                    End If
                End If
            Next
            'MyBase.Save()
            objDate.Save()
            Session.CommitTransaction()
        Catch ex As Exception
            Session.RollbackTransaction()
        End Try

    End Sub

    Public Sub PostToIncome()
        'Try
        '    Dim collncomeType As XPCollection(Of TrialBalanceIncome) = New XPCollection(Of TrialBalanceIncome)(Session)
        '    Dim objDate As New AccountGFMIS(Session)
        '    objDate.MonthTrialBalance = MonthTrialBalance
        '    objDate.YearID = YearID
        '    objDate.Save()
        '    For Each item As TrialBalanceIncome In collncomeType
        '        Dim objDetail As New AccountGFMISDetailRV(Session)
        '        If item.CodeGF_Oid IsNot Nothing Then
        '            objDetail.GFMIS = objDate
        '            objDetail.listGF = item.CodeGF_Name
        '            objDetail.CodeGF = item.CodeGF_Oid
        '            objDetail.AmountIncome = item.IncomeMonth
        '            objDetail.Save()
        '        End If
        '    Next
        '    'MyBase.Save()
        '    Session.CommitTransaction()
        'Catch ex As Exception
        '    Session.RollbackTransaction()
        'End Try

    End Sub

    'Private _BringforwardExpend As Double
    '<XafDisplayName("สะสมยกมา")> _
    '<PersistentAlias("TrialBalanceExpenses.Sum(BringforwardExpend)")> _
    '<ModelDefault("DisplayFormat", "{0:N2}")> _
    '<ModelDefault("EditMask", "N")> _
    '<Index(2), VisibleInListView(False), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    '<RuleRequiredField(DefaultContexts.Save)> _
    '<ImmediatePostData()> _
    'Public ReadOnly Property BringforwardExpend() As Double
    '    Get
    '        Try
    '            Dim tempObject As Object
    '            tempObject = EvaluateAlias("BringforwardExpend")
    '            Return CDbl(tempObject)
    '        Catch ex As Exception
    '            Return 0
    '        End Try
    '    End Get
    'End Property

    'Private _AmountExpend As Double
    '<XafDisplayName("เดือนนี้")> _
    '<PersistentAlias("TrialBalanceExpenses.Sum(AmountExpend)")> _
    '<ModelDefault("DisplayFormat", "{0:N2}")> _
    '<ModelDefault("EditMask", "N")> _
    '<Index(3), VisibleInListView(False), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    '<RuleRequiredField(DefaultContexts.Save)> _
    '<ImmediatePostData()> _
    'Public ReadOnly Property AmountExpend() As Double
    '    Get
    '        Try
    '            Dim tempObject As Object
    '            tempObject = EvaluateAlias("AmountExpend")
    '            Return CDbl(tempObject)
    '        Catch ex As Exception
    '            Return 0
    '        End Try
    '    End Get
    'End Property

    'Private _TotalExpend As Double
    '<XafDisplayName("รวม")> _
    '<PersistentAlias("TrialBalanceExpenses.Sum(TotalExpend)")> _
    '<ModelDefault("DisplayFormat", "{0:N2}")> _
    '<ModelDefault("EditMask", "N")> _
    '<Index(4), VisibleInListView(False), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    '<RuleRequiredField(DefaultContexts.Save)> _
    '<ImmediatePostData()> _
    'Public ReadOnly Property TotalExpend() As Double
    '    Get
    '        Try
    '            Dim tempObject As Object
    '            tempObject = EvaluateAlias("TotalExpend")
    '            Return CDbl(tempObject)
    '        Catch ex As Exception
    '            Return 0
    '        End Try
    '    End Get
    'End Property

    'Private _TotalBalance As Double
    '<PersistentAlias("TrialBalanceExpenses.Sum(TotalBalance)")> _
    '<XafDisplayName("คงเหลือ")> _
    '<ModelDefault("DisplayFormat", "{0:N2}")> _
    '<ModelDefault("EditMask", "N")> _
    '<Index(5), VisibleInListView(False), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    '<RuleRequiredField(DefaultContexts.Save)> _
    '<ImmediatePostData()> _
    'Public ReadOnly Property TotalBalance() As Double
    '    Get
    '        Try
    '            Dim tempObject As Object
    '            tempObject = EvaluateAlias("TotalBalance")
    '            Return CDbl(tempObject)
    '        Catch ex As Exception
    '            Return 0
    '        End Try
    '    End Get
    'End Property

    ''<Action(Caption:="My UI Action", ConfirmationMessage:="Are you sure?", ImageName:="Attention", AutoCommit:=True)> _
    'Public Sub ActionMethod()
    '    ' Trigger a custom business logic for the current record in the UI (http://documentation.devexpress.com/#Xaf/CustomDocument2619).
    '    For Each TblTrialBalanceExpenses As TrialBalanceExpenses In TrialBalanceExpenses
    '        Dim objDetail As New InvestmentDetail(Session)
    '        If TblTrialBalanceExpenses.TotalExpend <> 0 Then
    '            If TblTrialBalanceExpenses.GLID IsNot Nothing Then
    '                objDetail.MonthTrialBalance = MonthTrialBalance
    '                objDetail.YearID = YearID
    '                objDetail.listID = TblTrialBalanceExpenses.listID
    '                objDetail.list = TblTrialBalanceExpenses.listTypeExpenses
    '                objDetail.listExpenses = TblTrialBalanceExpenses.listExpenses
    '                objDetail.GLID = TblTrialBalanceExpenses.GLID
    '                objDetail.AmountExpend = TblTrialBalanceExpenses.AmountExpend

    '            End If
    '        End If
    '        objDetail.Save()
    '    Next
    '    Session.CommitTransaction()
    'End Sub
End Class
