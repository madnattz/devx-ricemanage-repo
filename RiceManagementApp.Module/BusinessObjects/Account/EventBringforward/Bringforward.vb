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
Public Class Bringforward ' Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
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

    Private _Account As EventBringforward
    <Association("EventBringforward-Bringforward")> _
    <Index(0), VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)> _
    <DC.Aggregated()> _
    <ImmediatePostData()> _
    Public Property Account() As EventBringforward
        Get
            Return _Account
        End Get
        Set(ByVal value As EventBringforward)
            Dim oldDebit As EventBringforward = Account
            Dim oldCredit As EventBringforward = Account
            SetPropertyValue("Account", _Account, value)
            If (Not IsLoading) AndAlso (Not IsSaving) AndAlso oldDebit IsNot Account Then
                If (oldDebit IsNot Nothing) Then
                    oldDebit = oldDebit
                Else
                    oldDebit = Account
                End If
                oldDebit.UpdateDebitTotal(True)
            End If

            If (Not IsLoading) AndAlso (Not IsSaving) AndAlso oldCredit IsNot Account Then
                If (oldCredit IsNot Nothing) Then
                    oldCredit = oldCredit
                Else
                    oldCredit = Account
                End If
                oldCredit.UpdateCreditTotal(True)
            End If
        End Set
    End Property

    Private _AccountID As String
    <XafDisplayName("รหัสบัญชี")> _
    <Size(15)> _
    <Index(1), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    Public Property AccountID() As String
        Get
            Return _AccountID
        End Get
        Set(ByVal value As String)
            SetPropertyValue("AccountID", _AccountID, value)
        End Set
    End Property

    Private _AccountName As String
    <XafDisplayName("ชื่อบัญชี")> _
    <Index(2), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    Public Property AccountName() As String
        Get
            Return _AccountName
        End Get
        Set(ByVal value As String)
            SetPropertyValue("AccountName", _AccountName, value)
        End Set
    End Property

    Private _Debit As Double
    <XafDisplayName("เดบิต")> _
    <Appearance("EnableDebitBf", Enabled:=False, Criteria:="[Crebit] > [Debit]", Context:="ListView")> _
    <ModelDefault("DisplayFormat", "{0:N2}")> _
    <ModelDefault("EditMask", "N")> _
    <Index(3), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    Public Property Debit() As Double
        Get
            Return _Debit
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("Debit", _Debit, value)
            If (Not IsLoading) AndAlso (Not IsSaving) AndAlso Account IsNot Nothing Then
                Account.UpdateDebitTotal(True)
            End If
        End Set
    End Property

    Private _Crebit As Double
    <XafDisplayName("เครดิต")> _
    <Appearance("EnableCreditBf", Enabled:=False, Criteria:="[Debit] > [Crebit]", Context:="ListView")> _
    <ModelDefault("DisplayFormat", "{0:N2}")> _
    <ModelDefault("EditMask", "N")> _
    <Index(4), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    Public Property Crebit() As Double
        Get
            Return _Crebit
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("Crebit", _Crebit, value)
            If (Not IsLoading) AndAlso (Not IsSaving) AndAlso Account IsNot Nothing Then
                Account.UpdateDebitTotal(True)
            End If
        End Set
    End Property

End Class
