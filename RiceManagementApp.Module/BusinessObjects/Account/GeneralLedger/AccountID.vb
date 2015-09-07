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
'<Appearance("EnabledAccountID", Criteria:="IsConvertUnit = False", Enabled:=False, Context:="ListView")> _
'<Appearance("EnabledAccountID", AppearanceItemType:="ViewItem", TargetItems:="*", Context:="ListView", Criteria:="IsConvertUnit = False", FontColor:="Red")> _
Public Class AccountID ' Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        DataOwner = GetCurrentSite()
        MyBase.AfterConstruction()
        ' Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).
        Me._TimeStamp = Date.Now
        'BaseObject.OidInitializationMode = OidInitializationMode.AfterConstruction
    End Sub

    Protected Overrides Sub OnSaved()
        'If Not (IsLoading) AndAlso Not (IsSaving) AndAlso Not (IsDeleted) Then
        '    If NumbersRV IsNot Nothing Then
        '        NumbersRV.PostToGL()
        '    End If
        '    If NumbersPV IsNot Nothing Then
        '        NumbersPV.PostToGL()
        '    End If
        '    If NumbersJV IsNot Nothing Then
        '        NumbersJV.PostToGL()
        '    End If
        'End If
        MyBase.OnSaved()
    End Sub

    Public Function CheckAccountPeriod(itemDate As Date) As Boolean
        Dim ret As Boolean = True
        Try

            'WHERE        (dbo.AccountPeriodDetail.StartDate <= '2015-03-24 00:00:00.000') AND (dbo.AccountPeriodDetail.EndDate >= '2015-03-24 00:00:00.000')
            Dim criteria As String = "StartDate <= ? and EndDate >= ? and AccountPeriod.Status = 0"

            'Dim Status As AccountPeriodDetail
            'Status.AccountPeriod.Status = 0
            Dim objAccPeriodDetail As AccountPeriodDetail = Session.FindObject(Of AccountPeriodDetail) _
                                                (CriteriaOperator.Parse(criteria, itemDate, itemDate))
            'WHERE        (dbo.AccountPeriodDetail.StartDate <= '2015-03-24 00:00:00.000') AND (dbo.AccountPeriodDetail.EndDate >= '2015-03-24 00:00:00.000')
            If objAccPeriodDetail Is Nothing Then
                ret = False
            Else
                ret = True
            End If
        Catch ex As Exception
            ret = False
        End Try
        Return ret
    End Function

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

    Private _NumbersRV As AccountBookRV
    <Association("AccountBookRV-AccountID")> _
    <Index(0), VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)> _
    <ImmediatePostData()> _
    Public Property NumbersRV() As AccountBookRV
        Get
            Return _NumbersRV
        End Get
        Set(ByVal value As AccountBookRV)
            Dim oldDebit As AccountBookRV = NumbersRV
            Dim oldCredit As AccountBookRV = NumbersRV
            Dim oldSaved As AccountBookRV = NumbersRV
            SetPropertyValue("NumbersRV", _NumbersRV, value)
            Try
                If (Not IsLoading) AndAlso (Not IsSaving) AndAlso oldDebit IsNot NumbersRV Then
                    If (oldDebit IsNot Nothing) Then
                        oldDebit = oldDebit
                    Else
                        oldDebit = NumbersRV
                    End If
                    oldDebit.UpdateDebitTotal(True)
                End If

                If (Not IsLoading) AndAlso (Not IsSaving) AndAlso oldCredit IsNot NumbersRV Then
                    If (oldCredit IsNot Nothing) Then
                        oldCredit = oldCredit
                    Else
                        oldCredit = NumbersRV
                    End If
                    oldCredit.UpdateCreditTotal(True)
                End If

                If (Not IsLoading) AndAlso (Not IsSaving) AndAlso oldSaved IsNot NumbersRV Then
                    If (oldSaved IsNot Nothing) Then
                        oldSaved = oldSaved
                    Else
                        oldSaved = NumbersRV
                    End If
                    oldSaved.UpdateSavedTotal(True)
                End If

                If (Not IsLoading) Then
                    DateList = NumbersRV.DateListRV
                End If
            Catch ex As Exception

            End Try
        End Set
    End Property

    Private _NumbersPV As AccountBookPV
    <Association("AccountBookPV-AccountID")> _
    <Index(0), VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)> _
    <ImmediatePostData()> _
    Public Property NumbersPV() As AccountBookPV
        Get
            Return _NumbersPV
        End Get
        Set(ByVal value As AccountBookPV)
            Dim oldDebit As AccountBookPV = NumbersPV
            Dim oldCredit As AccountBookPV = NumbersPV
            Dim oldSaved As AccountBookPV = NumbersPV
            SetPropertyValue("NumbersPV", _NumbersPV, value)
            Try
                If (Not IsLoading) AndAlso (Not IsSaving) AndAlso oldDebit IsNot NumbersPV Then
                    If (oldDebit IsNot Nothing) Then
                        oldDebit = oldDebit
                    Else
                        oldDebit = NumbersPV
                    End If
                    oldDebit.UpdateDebitTotal(True)
                End If

                If (Not IsLoading) AndAlso (Not IsSaving) AndAlso oldCredit IsNot NumbersPV Then
                    If (oldCredit IsNot Nothing) Then
                        oldCredit = oldCredit
                    Else
                        oldCredit = NumbersPV
                    End If
                    oldCredit.UpdateCreditTotal(True)
                End If

                If (Not IsLoading) AndAlso (Not IsSaving) AndAlso oldSaved IsNot NumbersRV Then
                    If (oldSaved IsNot Nothing) Then
                        oldSaved = oldSaved
                    Else
                        oldSaved = NumbersPV
                    End If
                    oldSaved.UpdateSavedTotal(True)
                End If

                If (Not IsLoading) Then
                    DateList = NumbersPV.DateListPV
                End If
            Catch ex As Exception

            End Try
        End Set
    End Property

    Private _NumbersJV As AccountBookJV
    <Association("AccountBookJV-AccountID")> _
    <Index(0), VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)> _
    <ImmediatePostData()> _
    Public Property NumbersJV() As AccountBookJV
        Get
            Return _NumbersJV
        End Get
        Set(ByVal value As AccountBookJV)
            Dim oldDebit As AccountBookJV = NumbersJV
            Dim oldCredit As AccountBookJV = NumbersJV
            Dim oldSaved As AccountBookJV = NumbersJV
            SetPropertyValue("NumbersJV", _NumbersJV, value)
            Try
                If (Not IsLoading) AndAlso (Not IsSaving) AndAlso oldDebit IsNot NumbersPV Then
                    If (oldDebit IsNot Nothing) Then
                        oldDebit = oldDebit
                    Else
                        oldDebit = NumbersJV
                    End If
                    oldDebit.UpdateDebitTotal(True)
                End If

                If (Not IsLoading) AndAlso (Not IsSaving) AndAlso oldCredit IsNot NumbersPV Then
                    If (oldCredit IsNot Nothing) Then
                        oldCredit = oldCredit
                    Else
                        oldCredit = NumbersJV
                    End If
                    oldCredit.UpdateCreditTotal(True)
                End If

                If (Not IsLoading) AndAlso (Not IsSaving) AndAlso oldSaved IsNot NumbersRV Then
                    If (oldSaved IsNot Nothing) Then
                        oldSaved = oldSaved
                    Else
                        oldSaved = NumbersJV
                    End If
                    oldSaved.UpdateSavedTotal(True)
                End If

                If (Not IsLoading) Then
                    DateList = NumbersJV.DateListJV
                End If
            Catch ex As Exception

            End Try
        End Set
    End Property

    <VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)> _
    <ImmediatePostData()> _
    <RuleFromBoolProperty("AccountID_IsConvertUnit", DefaultContexts.Save, "ยังไม่มีการกำหนดงวด หรือ งวดที่ทำการบันทึกได้ถูกปิดไปแล้ว กรุณาตรวจสอบ")> _
    Public ReadOnly Property IsConvertUnit() As Boolean
        Get
            Return CheckAccountPeriod(DateList)
        End Get
    End Property

    Private _DateList As Date
    <XafDisplayName("วันที่")> _
    <Index(0), VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    <ImmediatePostData()> _
    Public Property DateList() As DateTime
        Get
            Return _DateList
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue("DateList", _DateList, value)
        End Set
    End Property

    Private _TimeStamp As DateTime

    <XafDisplayName("วันที่บันทึก")> _
    <ModelDefault("DisplayFormat", "{0:dd/MM/yyyy hh:mm:ss}"), ModelDefault("EditMask", "dd/MM/yyyy hh:mm:ss")> _
    <VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)> _
    Public Property TimeStamp() As DateTime
        Get
            Return _TimeStamp
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue("TimeStamp", _TimeStamp, value)
        End Set
    End Property
    '<Appearance("EnabledAccount", Criteria:="IsConvertUnit = False", Enabled:=False, Context:="ListView")> _
    Private _Account As Account
    <XafDisplayName("รหัสบัญชี")> _
    <Appearance("AccountID", Criteria:="IsConvertUnit = False", Enabled:=False, Context:="ListView")> _
    <Index(0), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    <ImmediatePostData()> _
    Public Property Account() As Account
        Get
            Return _Account
        End Get
        Set(ByVal value As Account)
            SetPropertyValue("Account", _Account, value)
            If Account IsNot Nothing Then
                AccountName = Account.AccountName
            End If
        End Set
    End Property

    Private _AccountName As String
    <XafDisplayName("ชื่อบัญชี")> _
    <Appearance("EnableAccountName", Enabled:=False, Context:="ListView")> _
    <Index(1), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    <ImmediatePostData()> _
    Public Property AccountName() As String
        Get
            Return _AccountName
        End Get
        Set(ByVal value As String)
            SetPropertyValue("AccountName", _AccountName, value)
            If AccountName IsNot Nothing Then
                Saved = 1
            End If
        End Set
    End Property

    Private _Debit As Double
    <XafDisplayName("เดบิต")> _
    <ModelDefault("DisplayFormat", "{0:N2}")> _
    <ModelDefault("EditMask", "N")> _
    <Appearance("DebitEnable", Criteria:="IsConvertUnit = False", Enabled:=False, Context:="ListView")> _
    <Appearance("EnableDebit", Enabled:=False, Criteria:="[Credit] > [Debit]", Context:="ListView")> _
    <Index(2), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    <ImmediatePostData()> _
    Public Property Debit() As Double
        Get
            Return _Debit
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("Debit", _Debit, value)
            Try
                If (Not IsLoading) AndAlso (Not IsSaving) AndAlso NumbersRV IsNot Nothing Then
                    NumbersRV.UpdateDebitTotal(True)
                End If
                If (Not IsLoading) AndAlso (Not IsSaving) AndAlso NumbersPV IsNot Nothing Then
                    NumbersPV.UpdateDebitTotal(True)
                End If
                If (Not IsLoading) AndAlso (Not IsSaving) AndAlso NumbersJV IsNot Nothing Then
                    NumbersJV.UpdateDebitTotal(True)
                End If
            Catch ex As Exception

            End Try
        End Set
    End Property

    Private _Credit As Double
    <ModelDefault("DisplayFormat", "{0:N2}")> _
    <ModelDefault("EditMask", "N")> _
    <XafDisplayName("เครดิต")> _
    <Appearance("CreditEnable", Criteria:="IsConvertUnit = False", Enabled:=False, Context:="ListView")> _
    <Appearance("EnableCredit", Enabled:=False, Criteria:="[Debit] > [Credit]", Context:="ListView")> _
    <Index(3), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    <ImmediatePostData()> _
    Public Property Credit() As Double
        Get
            Return _Credit
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("Credit", _Credit, value)
            Try
                If (Not IsLoading) AndAlso (Not IsSaving) AndAlso NumbersRV IsNot Nothing Then
                    NumbersRV.UpdateCreditTotal(True)
                End If
                If (Not IsLoading) AndAlso (Not IsSaving) AndAlso NumbersPV IsNot Nothing Then
                    NumbersPV.UpdateCreditTotal(True)
                End If
                If (Not IsLoading) AndAlso (Not IsSaving) AndAlso NumbersJV IsNot Nothing Then
                    NumbersJV.UpdateCreditTotal(True)
                End If
            Catch ex As Exception

            End Try
        End Set
    End Property

    Private _Saved As Double
    <XafDisplayName("")> _
    <ModelDefault("DisplayFormat", "{0:N2}")> _
    <ModelDefault("EditMask", "N")> _
    <Appearance("EnableSaved", Enabled:=False, Context:="DetailView")> _
    <Index(2), VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)> _
    <ImmediatePostData()> _
    Public Property Saved() As Double
        Get
            Return _Saved
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("Saved", _Saved, value)
            Try
                If (Not IsLoading) AndAlso (Not IsSaving) AndAlso NumbersRV IsNot Nothing Then
                    NumbersRV.UpdateSavedTotal(True)
                End If
                If (Not IsLoading) AndAlso (Not IsSaving) AndAlso NumbersPV IsNot Nothing Then
                    NumbersPV.UpdateDebitTotal(True)
                End If
                If (Not IsLoading) AndAlso (Not IsSaving) AndAlso NumbersJV IsNot Nothing Then
                    NumbersJV.UpdateDebitTotal(True)
                End If
            Catch ex As Exception

            End Try
        End Set
    End Property
End Class
