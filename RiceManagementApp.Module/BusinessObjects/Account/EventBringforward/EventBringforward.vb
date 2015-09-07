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
<NavigationItem("รายการยอดยกมา")> _
<XafDisplayName("ข้อมูลยอดยกมา")> _
<RuleCriteria("Total", DefaultContexts.Save, "[TotalDebit] = [TotalCredit]", "จำนวนเงินไม่ตรงกับยอดรวม")> _
<DefaultClassOptions()> _
Public Class EventBringforward ' Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        DataOwner = GetCurrentSite()
        MyBase.AfterConstruction()
        _DocumentNo = Date.Now.ToString("yyMMdd")
        ' Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).
        AccountBring = Session.FindObject(Of AccountBookID)(CriteriaOperator.Parse("AccountBookNo=?", CurrentListView))
    End Sub

    Protected Overrides Sub OnSaving()
        'MsgBox("dfd")

        If (Me.fBookRefNo Is Nothing) Then
            Dim prefix As String = AccountBring.AccountBookNameVGA & Date.Now.ToString("yyyyMMdd")

            Me.fBookRefNo = String.Format("{0}{1:D5}", prefix, DistributedIdGeneratorHelper.Generate(Me.Session.DataLayer, Me.GetType().FullName, prefix))

        End If

        If Me.DataOwner Is Nothing Then
            DataOwner = GetCurrentSite()
        End If
        MyBase.OnSaving()

    End Sub

    Protected Overrides Sub OnSaved()
        PostToGL()
        MyBase.OnSaved()
    End Sub
    Protected Overrides Sub OnDeleted()
        MyBase.OnDeleted()
        'PostToGL()
        DeleteOldGL()
        Session.Delete(Bringforward)
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

    <Persistent("BookRefNo")> _
    Private fBookRefNo As String
    <PersistentAlias("fBookRefNo")> _
    <Index(0), VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)> _
    Public ReadOnly Property BookRefNo() As String
        Get
            Return fBookRefNo
        End Get
    End Property

    'Private _DateLise As Date = Today
    '<XafDisplayName("วันที่")> _
    '<Index(0), VisibleInListView(True)> _
    '<RuleRequiredField(DefaultContexts.Save)> _
    'Public Property DateLise() As Date
    '    Get
    '        Return _DateLise
    '    End Get
    '    Set(ByVal value As Date)
    '        SetPropertyValue("DateLise", _DateLise, value)
    '        _DocumentNo = value.ToString("yyMMdd")
    '    End Set
    'End Property

    Private _AccountPeriod As AccountPeriod
    <XafDisplayName("ปีบัญชี")> _
    <Index(0), VisibleInListView(True)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property AccountPeriod() As AccountPeriod
        Get
            Return _AccountPeriod
        End Get
        Set(ByVal value As AccountPeriod)
            SetPropertyValue("_AccountPeriod", _AccountPeriod, value)
            '_DocumentNo = value.ToString("yyMMdd")
        End Set
    End Property

    Private _AccountBring As AccountBookID
    <XafDisplayName("สมุด")> _
    <Appearance("NotEditAccountBring", Enabled:=False, Context:="DetailView")> _
    <Index(1), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property AccountBring() As AccountBookID
        Get
            Return _AccountBring
        End Get
        Set(ByVal value As AccountBookID)
            SetPropertyValue("AccountBring", _AccountBring, value)
        End Set
    End Property

    Private _DocumentNo As String
    <XafDisplayName("เลขที่ใบสำคัญ")> _
    <Index(2), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    <ImmediatePostData()> _
    Public Property DocumentNo() As String
        Get
            Return _DocumentNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue("DocumentNo", _DocumentNo, value)
        End Set
    End Property

    Private _ListNo As String
    <XafDisplayName("รายการที่")> _
    <Index(3), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    <ImmediatePostData()> _
    Public Property ListNo() As String
        Get
            Return _ListNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue("ListNo", _ListNo, value)
        End Set
    End Property

    Protected Overrides Sub OnLoaded()
        'When using "lazy" calculations it's necessary to reset cached values.
        Reset()
        MyBase.OnLoaded()
    End Sub


    Private Sub Reset()

        fDebitTotal = Nothing
        fCreditTotal = Nothing

    End Sub

    <Persistent("TotalDebit")> _
    Private fDebitTotal As Nullable(Of Double) = Nothing
    <PersistentAlias("fDetailTotal")> _
    <VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)> _
    Public ReadOnly Property DebitTotal() As Nullable(Of Double)
        Get
            If (Not IsLoading) AndAlso (Not IsSaving) AndAlso Not fDebitTotal.HasValue Then
                UpdateDebitTotal(False)
            End If
            Return fDebitTotal
        End Get
    End Property


    Public Sub UpdateDebitTotal(ByVal forceChangeEvents As Boolean)
        'Put your complex business logic here. Just for demo purposes, we calculate a sum here.
        Dim oldDebitTotal As Nullable(Of Double) = fDebitTotal
        Dim tempTotal As Double = 0D
        'Manually iterate through the Orders collection if your calculated property requires a complex business logic which cannot be expressed via criteria language.
        For Each detail As Bringforward In Bringforward
            tempTotal += detail.Debit
        Next detail
        fDebitTotal = tempTotal
        If forceChangeEvents Then
            OnChanged("TotalDebit", oldDebitTotal, fDebitTotal)
        End If
    End Sub

    <Persistent("TotalCredit")> _
    Private fCreditTotal As Nullable(Of Double) = Nothing
    <PersistentAlias("fCreditTotal")> _
    <VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)> _
    Public ReadOnly Property CreditTotal() As Nullable(Of Double)
        Get
            If (Not IsLoading) AndAlso (Not IsSaving) AndAlso Not fCreditTotal.HasValue Then
                UpdateCreditTotal(False)
            End If
            Return fCreditTotal
        End Get
    End Property


    Public Sub UpdateCreditTotal(ByVal forceChangeEvents As Boolean)
        'Put your complex business logic here. Just for demo purposes, we calculate a sum here.
        Dim oldCreditTotal As Nullable(Of Double) = fCreditTotal
        Dim tempTotal As Double = 0D
        'Manually iterate through the Orders collection if your calculated property requires a complex business logic which cannot be expressed via criteria language.
        For Each detail As Bringforward In Bringforward
            tempTotal += detail.Crebit
        Next detail
        fCreditTotal = tempTotal
        If forceChangeEvents Then
            OnChanged("TotalCredit", oldCreditTotal, fCreditTotal)
        End If
    End Sub

    Private _TotalDebit As Double
    <XafDisplayName("ยอดรวมทั้งสิ้นเดบิต")> _
    <PersistentAlias("Bringforward.Sum(Debit)")> _
    <ModelDefault("DisplayFormat", "{0:N2}")> _
    <ModelDefault("EditMask", "N")> _
    <Index(1), VisibleInListView(False), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    <ImmediatePostData()> _
    Public ReadOnly Property TotalDebit() As Double
        Get
            Try
                Dim tempObject As Object
                tempObject = EvaluateAlias("TotalDebit")
                Return CDbl(tempObject)
            Catch ex As Exception
                Return 0
            End Try
        End Get
    End Property

    Private _TotalCredit As Double
    <XafDisplayName("ยอดรวมทั้งสิ้นเครดิต")> _
    <PersistentAlias("Bringforward.Sum(Crebit)")> _
    <ModelDefault("DisplayFormat", "{0:N2}")> _
    <ModelDefault("EditMask", "N")> _
    <Index(2), VisibleInListView(False), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    <ImmediatePostData()> _
    Public ReadOnly Property TotalCredit() As Double
        Get
            Try
                Dim tempObject As Object
                tempObject = EvaluateAlias("TotalCredit")
                Return CDbl(tempObject)
            Catch ex As Exception
                Return 0
            End Try
        End Get
    End Property

    <XafDisplayName("รายการบัญชียกมา")> _
<Association("EventBringforward-Bringforward", GetType(Bringforward))> _
<DC.Aggregated()> _
<ImmediatePostData()> _
    Public ReadOnly Property Bringforward() As XPCollection(Of Bringforward)
        Get
            Return GetCollection(Of Bringforward)("Bringforward")
        End Get
    End Property

    <Action(Caption:="ตั้งค่ายอดยกมา", ConfirmationMessage:="ท่านต้องการดึงข้อมูลจากตารางรหัสบัญชีใช่หรือไม่?", ImageName:="Attention", AutoCommit:=True)> _
    Public Sub LoadBlanceAccountBegin()
        ' Trigger a custom business logic for the current record in the UI (http://documentation.devexpress.com/#Xaf/CustomDocument2619).
        Dim colDelete As New XPCollection(Of Bringforward)(Session, CriteriaOperator.Parse("Account=?", Me))
        If colDelete.Count > 0 Then
            Session.Delete(colDelete)
            Session.Save(colDelete)
        End If
        Dim result As New XPCollection(Of Account)(Session, CriteriaOperator.Parse("[LevelItem]>=3 AND PublicStatus = 0"))
        If result.Count > 0 Then
            For Each TblAccount As Account In result
                Dim InsertBringforward As New Bringforward(Session)
                With InsertBringforward
                    .AccountID = TblAccount.AccountID
                    .AccountName = TblAccount.AccountName
                    .Account = Me
                    .Save()
                End With
            Next
        End If
    End Sub

    Public Sub PostToGL()
        Try
            DeleteOldGL()

            Dim accPeriodStartDate As Date = Date.Now

            If AccountPeriod IsNot Nothing Then
                For Each item As AccountPeriodDetail In AccountPeriod.AccountPeriodDetails
                    If item.ItemNo = 1 Then
                        accPeriodStartDate = item.StartDate
                    End If
                Next
            End If

            For i As Integer = 0 To Bringforward.Count - 1
                If Not (Bringforward(i).Debit = 0 And Bringforward(i).Crebit = 0) Then
                    Dim ADDGL As GL = New GL(Session)
                    ADDGL.BookRefNo = Me.fBookRefNo
                    ADDGL.DocuNo = DocumentNo
                    ADDGL.DocuDate = accPeriodStartDate
                    ADDGL.AccountBook = AccountBring.AccountBookNameVGA
                    ADDGL.ListNo = "0000" 'ListNo

                    ADDGL.AccID = Bringforward(i).AccountID
                    ADDGL.AccountName = Bringforward(i).AccountName
                    ADDGL.AccDetail = "ยอดยกมาต้นงวด"

                    ADDGL.DrAmnt = Bringforward(i).Debit
                    ADDGL.CrAmnt = Bringforward(i).Crebit
                    ADDGL.Save()
                End If
                
            Next
            'MyBase.Save()
            Session.CommitTransaction()
        Catch ex As Exception
            Session.RollbackTransaction()
        End Try

    End Sub

    Public Sub DeleteOldGL()
        Dim colToDelete As New XPCollection(Of GL)(Session, CriteriaOperator.Parse("BookRefNo=?", Me.fBookRefNo))
        If colToDelete.Count > 0 Then
            Session.Delete(colToDelete)
            Session.Save(colToDelete)
        End If
    End Sub
End Class
