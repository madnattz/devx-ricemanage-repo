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
'<DefaultClassOptions()> _
Public Class AccountBID ' Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        DataOwner = GetCurrentSite()
        MyBase.AfterConstruction()
        Me._TimeStamp = Date.Now
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

    Private _EventID As EventAccountBID
    <Association("EventAccountBID-AccountBID")> _
    <Index(0), VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)> _
    <ImmediatePostData()> _
    Public Property EventID() As EventAccountBID
        Get
            Return _EventID
        End Get
        Set(ByVal value As EventAccountBID)
            Dim oldDebit As EventAccountBID = EventID
            SetPropertyValue("EventID", _EventID, value)
            If (Not IsLoading) AndAlso (Not IsSaving) AndAlso oldDebit IsNot EventID Then
                If (oldDebit IsNot Nothing) Then
                    oldDebit = oldDebit
                Else
                    oldDebit = EventID
                End If
                oldDebit.UpdateDebitTotal(True)
            End If
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

    Private _Detail As String
    <XafDisplayName("รายการ")> _
    <Index(0), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    <ImmediatePostData()> _
    Public Property Detail() As String
        Get
            Return _Detail
        End Get
        Set(ByVal value As String)
            SetPropertyValue("Detail", _Detail, value)
        End Set
    End Property

    Private _Amount As Double
    <XafDisplayName("จำนวนเงิน")> _
    <ModelDefault("DisplayFormat", "{0:N2}")> _
    <ModelDefault("EditMask", "N")> _
    <Index(1), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    <ImmediatePostData()> _
    Public Property Amount() As Double
        Get
            Return _Amount
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("Amount", _Amount, value)
            If (Not IsLoading) AndAlso (Not IsSaving) AndAlso EventID IsNot Nothing Then
                EventID.UpdateDebitTotal(True)
            End If
        End Set
    End Property

    Private _Remark As String
    <XafDisplayName("หมายเหตุ")> _
    <Index(2), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    <ImmediatePostData()> _
    Public Property Remark() As String
        Get
            Return _Remark
        End Get
        Set(ByVal value As String)
            SetPropertyValue("Remark", _Remark, value)
        End Set
    End Property

    '<Action(Caption:="My UI Action", ConfirmationMessage:="Are you sure?", ImageName:="Attention", AutoCommit:=True)> _
    'Public Sub ActionMethod()
    '    ' Trigger a custom business logic for the current record in the UI (http://documentation.devexpress.com/#Xaf/CustomDocument2619).
    '    Me.PersistentProperty = "Paid"
    'End Sub
End Class
