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

<DefaultClassOptions()> _
<XafDisplayName("แต่งตั้งคณะกรรมการจัดซื้อ")> _
Public Class PlanAssignBuyTeam
    Inherits BaseObject
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

    Protected Overrides Sub OnSaving()
        MyBase.OnSaving()
        If (Me.fRefNo Is Nothing) Then
            Dim prefix As String = ""
            Dim _year As String = Date.Now.Year + 543
            prefix = _year

            Me.fRefNo = String.Format("{0}/{1:D4}", prefix, DistributedIdGeneratorHelper.Generate(Me.Session.DataLayer, Me.GetType().FullName, prefix))

        End If
    End Sub

    <Persistent("RefNo")> _
    Private fRefNo As String
    <PersistentAlias("fRefNo")> _
    <XafDisplayName("เลขที่อ้างอิง")> _
    Public ReadOnly Property RefNo() As String
        Get
            Return fRefNo
        End Get
    End Property

    Dim fAssignNo As String
    <Size(50)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    <XafDisplayName("เลขที่คำสั่ง")> _
    Public Property AssignNo() As String
        Get
            Return fAssignNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AssignNo", fAssignNo, value)
        End Set
    End Property

    Dim fAssignDtae As DateTime
    <RuleRequiredField(DefaultContexts.Save)> _
    <XafDisplayName("ลงวันที่")> _
    Public Property AssignDtae() As DateTime
        Get
            Return fAssignDtae
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("AssignDtae", fAssignDtae, value)
        End Set
    End Property

    Dim fMainPlan As MainPlan
    <RuleRequiredField(DefaultContexts.Save)> _
    <RuleUniqueValue()> _
    <XafDisplayName("เป้าการผลิต")> _
    Public Property MainPlan() As MainPlan
        Get
            Return fMainPlan
        End Get
        Set(ByVal value As MainPlan)
            SetPropertyValue(Of MainPlan)("MainPlan", fMainPlan, value)
        End Set
    End Property

    Dim fBuyLead As String
    <Size(100)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    <XafDisplayName("ประธานกรรมการจัดซื้อ")> _
    Public Property BuyLead() As String
        Get
            Return fBuyLead
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("BuyLead", fBuyLead, value)
        End Set
    End Property

    Dim fBuyLeadPosition As String
    <Size(100)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    <XafDisplayName("ตำแหน่งประธานกรรมการจัดซื้อ")> _
    Public Property BuyLeadPosition() As String
        Get
            Return fBuyLeadPosition
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("fBuyLeadPosition", fBuyLeadPosition, value)
        End Set
    End Property

    Dim fBuyTeam1 As String
    <Size(100)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    <XafDisplayName("กรรมการจัดซื้อ (1)")> _
    Public Property BuyTeam1() As String
        Get
            Return fBuyTeam1
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("BuyTeam1", fBuyTeam1, value)
        End Set
    End Property

    Dim fBuyTeam1Position As String
    <Size(100)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    <XafDisplayName("ตำแหน่งกรรมการจัดซื้อ (1)")> _
    <VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    Public Property BuyTeam1Position() As String
        Get
            Return fBuyTeam1Position
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("BuyTeam1Position", fBuyTeam1Position, value)
        End Set
    End Property

    Dim fBuyTeam2 As String
    <Size(100)> _
    <XafDisplayName("กรรมการจัดซื้อ (2)")> _
    Public Property BuyTeam2() As String
        Get
            Return fBuyTeam2
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("BuyTeam2", fBuyTeam2, value)
        End Set
    End Property

    Dim fBuyTeam2Position As String
    <Size(100)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    <XafDisplayName("ตำแหน่งกรรมการจัดซื้อ (2)")> _
    <VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    Public Property BuyTeam2Position() As String
        Get
            Return fBuyTeam1Position
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("BuyTeam2Position", fBuyTeam2Position, value)
        End Set
    End Property

    Dim fBuyTeam3 As String
    <Size(100)> _
    <XafDisplayName("กรรมการจัดซื้อ (3)")> _
    Public Property BuyTeam3() As String
        Get
            Return fBuyTeam3
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("BuyTeam3", fBuyTeam3, value)
        End Set
    End Property

    Dim fBuyTeam3Position As String
    <Size(100)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    <XafDisplayName("ตำแหน่งกรรมการจัดซื้อ (3)")> _
    <VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    Public Property BuyTeam3Position() As String
        Get
            Return fBuyTeam3Position
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("BuyTeam3Position", fBuyTeam3Position, value)
        End Set
    End Property

    Dim fBuyTeam4 As String
    <Size(100)> _
    <XafDisplayName("กรรมการจัดซื้อ (4)")> _
    Public Property BuyTeam4() As String
        Get
            Return fBuyTeam4
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("BuyTeam4", fBuyTeam4, value)
        End Set
    End Property

    Dim fBuyTeam4Position As String
    <Size(100)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    <VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    <XafDisplayName("ตำแหน่งกรรมการจัดซื้อ (4)")> _
    Public Property BuyTeam4Position() As String
        Get
            Return fBuyTeam4Position
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("BuyTeam4Position", fBuyTeam4Position, value)
        End Set
    End Property

    Dim fCheckLead As String
    <Size(100)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    <XafDisplayName("ประธานกรรมการตรวจรับ")> _
    Public Property CheckLead() As String
        Get
            Return fCheckLead
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CheckLead", fCheckLead, value)
        End Set
    End Property

    Dim fCheckLeadPosition As String
    <Size(100)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    <VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    <XafDisplayName("ตำแหน่งประธานกรรมการตรวจรับ")> _
    Public Property CheckLeadPosition() As String
        Get
            Return fCheckLeadPosition
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CheckLeadPosition", fCheckLeadPosition, value)
        End Set
    End Property

    Dim fCheckTeam1 As String
    <Size(100)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    <XafDisplayName("กรรมการตรวจรับ (1)")> _
    Public Property CheckTeam1() As String
        Get
            Return fCheckTeam1
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CheckTeam1", fCheckTeam1, value)
        End Set
    End Property

    Dim fCheckTeam1Position As String
    <Size(100)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    <VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    <XafDisplayName("ตำแหน่งกรรมการตรวจรับ (1)")> _
    Public Property CheckTeam1Position() As String
        Get
            Return fCheckTeam1Position
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CheckTeam1Position", fCheckTeam1Position, value)
        End Set
    End Property

    Dim fCheckTeam2 As String
    <Size(100)> _
    <XafDisplayName("กรรมการตรวจรับ (2)")> _
    Public Property CheckTeam2() As String
        Get
            Return fCheckTeam2
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CheckTeam2", fCheckTeam2, value)
        End Set
    End Property

    Dim fCheckTeam2Position As String
    <Size(100)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    <VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    <XafDisplayName("ตำแหน่งกรรมการตรวจรับ (2)")> _
    Public Property CheckTeam2Position() As String
        Get
            Return fCheckTeam2Position
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CheckTeam2Position", fCheckTeam2Position, value)
        End Set
    End Property

    Dim fCheckTeam3 As String
    <Size(100)> _
    <XafDisplayName("กรรมการตรวจรับ (3)")> _
    Public Property CheckTeam3() As String
        Get
            Return fCheckTeam3
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CheckTeam3", fCheckTeam3, value)
        End Set
    End Property

    Dim fCheckTeam3Position As String
    <Size(100)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    <VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    <XafDisplayName("ตำแหน่งกรรมการตรวจรับ (3)")> _
    Public Property CheckTeam3Position() As String
        Get
            Return fCheckTeam3Position
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CheckTeam3Position", fCheckTeam3Position, value)
        End Set
    End Property

    Dim fCheckTeam4 As String
    <Size(100)> _
    <XafDisplayName("กรรมการตรวจรับ (4)")> _
    Public Property CheckTeam4() As String
        Get
            Return fCheckTeam4
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CheckTeam4", fCheckTeam4, value)
        End Set
    End Property

    Dim fCheckTeam4Position As String
    <Size(100)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    <VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    <XafDisplayName("ตำแหน่งกรรมการตรวจรับ (4)")> _
    Public Property CheckTeam4Position() As String
        Get
            Return fCheckTeam4Position
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CheckTeam4Position", fCheckTeam4Position, value)
        End Set
    End Property

End Class
