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
<RuleCriteria("ReserveSeed.NotDelete", DefaultContexts.Delete, "Status='Pending'", CustomMessageTemplate:="ไม่สามารถลบข้อมูลนี้ได้")> _
<ConditionalAppearance.Appearance("ReserveSeedDisableAllItems", criteria:="Status!='Pending'", Enabled:=False, TargetItems:="*", Context:="DetailView")> _
Public Class ReserveSeed
    Inherits BaseObject
    Implements IApproveAble

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        Me.fReserveDateTime = Date.Today
        Me.fStatus = PublicEnum.SimsStatus.Pending
        Me.fReserveType = PublicEnum.ReserveType.Reserve
        Me.ReserveBy = PublicEnum.ReserveBy.Local
        Me.CentralSaleType = PublicEnum.CentralSaleType.Farming
        Me.IsSubmitToCenter = False
        DataOwner = GetCurrentSite()
    End Sub

    Protected Overrides Sub OnSaving()
        If (Me.fReserveNo Is Nothing) Then
            Dim prefix As String = ""
            Dim _year As String = (Date.Now.Year + 543).ToString
            prefix = _year

            Me.fReserveNo = String.Format("{0}/{1:D4}", prefix, DistributedIdGeneratorHelper.Generate(Me.Session.DataLayer, Me.GetType().FullName, prefix))

        End If
        If Me.DataOwner Is Nothing Then
            DataOwner = GetCurrentSite()
        End If
        MyBase.OnSaving()
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

    <Persistent("ReserveNo")> _
    Private fReserveNo As String
    <PersistentAlias("fReserveNo")> _
    Public ReadOnly Property ReserveNo() As String
        Get
            Return fReserveNo
        End Get
    End Property

    <VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(True)> _
    Public ReadOnly Property ReserveDetailForLookUp As String
        Get
            Try
                Return "ผู้จอง " & fMember.MemberLookupName & " เลขที่จอง " & Me.fReserveNo
            Catch ex As Exception
                Return ""
            End Try
        End Get
    End Property

    Private fMember As Member
    <ImmediatePostData> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property Member() As Member
        Get
            Return fMember
        End Get
        Set(ByVal value As Member)
            SetPropertyValue(Of Member)("Member", fMember, value)
        End Set
    End Property

    Private fDeposit As Double
    Public Property Deposit() As Double
        Get
            Return fDeposit
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("Deposit", fDeposit, value)
        End Set
    End Property
    Private fReserveBy As PublicEnum.ReserveBy
    Public Property ReserveBy() As PublicEnum.ReserveBy
        Get
            Return fReserveBy
        End Get
        Set(ByVal value As PublicEnum.ReserveBy)
            SetPropertyValue(Of PublicEnum.ReserveBy)("ReserveBy", fReserveBy, value)
        End Set
    End Property
    Private fReserveType As PublicEnum.ReserveType
    Public Property ReserveType() As PublicEnum.ReserveType
        Get
            Return fReserveType
        End Get
        Set(ByVal value As PublicEnum.ReserveType)
            SetPropertyValue(Of PublicEnum.ReserveType)("ReserveType", fReserveType, value)
        End Set
    End Property
    'Dim fReservePurpose As PublicEnum.ReservePurpose
    'Public Property ReservePurpose() As PublicEnum.ReservePurpose
    '    Get
    '        Return fReservePurpose
    '    End Get
    '    Set(ByVal value As PublicEnum.ReservePurpose)
    '        SetPropertyValue(Of PublicEnum.ReservePurpose)("ReservePurpose", fReservePurpose, value)
    '    End Set
    'End Property
    Private fReserveDateTime As DateTime
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property ReserveDateTime() As DateTime
        Get
            Return fReserveDateTime
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ReserveDateTime", fReserveDateTime, value)
        End Set
    End Property
    Private fCentralSaleType As PublicEnum.CentralSaleType
    Public Property CentralSaleType() As PublicEnum.CentralSaleType
        Get
            Return fCentralSaleType
        End Get
        Set(ByVal value As PublicEnum.CentralSaleType)
            SetPropertyValue(Of PublicEnum.CentralSaleType)("CentralSaleType", fCentralSaleType, value)
        End Set
    End Property
    Private fProjectName As String
    <Size(50)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property ProjectName() As String
        Get
            Return fProjectName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ProjectName", fProjectName, value)
        End Set
    End Property
    Private fProjectDetail As String
    <Size(200)> _
    Public Property ProjectDetail() As String
        Get
            Return fProjectDetail
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ProjectDetail", fProjectDetail, value)
        End Set
    End Property
    'Private fReserveReason As String
    '<Size(50)> _
    'Public Property ReserveReason() As String
    '    Get
    '        Return fReserveReason
    '    End Get
    '    Set(ByVal value As String)
    '        SetPropertyValue(Of String)("ReserveReason", fReserveReason, value)
    '    End Set
    'End Property
    Private fStatus As PublicEnum.SimsStatus
    Public Property Status() As PublicEnum.SimsStatus
        Get
            Return fStatus
        End Get
        Set(ByVal value As PublicEnum.SimsStatus)
            SetPropertyValue(Of PublicEnum.SimsStatus)("Status", fStatus, value)
        End Set
    End Property

    Dim fIsSubmitToCenter As Boolean
    <VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    <XafDisplayName("ส่งข้อมูลเข้าส่วนกลาง")> _
    Public Property IsSubmitToCenter() As Boolean
        Get
            Return fIsSubmitToCenter
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("IsSubmitToCenter", fIsSubmitToCenter, value)
        End Set
    End Property

    <Association("ReserveSeedDetailReferencesReserveSeed", GetType(ReserveSeedDetail))> _
    Public ReadOnly Property ReserveSeedDetails() As XPCollection(Of ReserveSeedDetail)
        Get
            Return GetCollection(Of ReserveSeedDetail)("ReserveSeedDetails")
        End Get
    End Property

    Public Function MarkAsComplete() As Boolean
        If Not IsDeleted Then
            If Not ReserveSeedDetails.Count > 0 Then
                MsgBox("ไม่พบรายการเมล็ดพันธุ์ กรุณาตรวจสอบข้อมูลอีกครั้ง", MsgBoxStyle.Information, "")
                Exit Function
            End If
            Dim _session As Session = Me.Session
            Try
                Me.fStatus = PublicEnum.SimsStatus.Approve
                For Each item As ReserveSeedDetail In ReserveSeedDetails
                    If Not item.SeedProduct Is Nothing Then
                        item.SeedProduct.UpdateStockAmount(SeedProduct.UpdateStockType.Reserve, item.Quantity, item.Bags)
                        '//InsertTransaction(Session, item, ActionType.Approve)
                        Dim objTransactionLog As New SeedTransaction(_session, PublicEnum.SeedTransactionType.Reserve, fReserveNo, item.SeedProduct, item.Quantity, item.Bags)

                    End If
                Next
                Me.Save()
                Return True
            Catch ex As Exception
                Return False
            End Try
        End If
    End Function

    Public Function MarkAsCancel() As Boolean
        If Not IsDeleted Then
            Dim _session As Session = Me.Session
            Try
                Me.Status = PublicEnum.SimsStatus.Cancel
                Me.IsSubmitToCenter = False

                For Each item As ReserveSeedDetail In ReserveSeedDetails
                    If Not item.SeedProduct Is Nothing Then
                        item.SeedProduct.UpdateStockAmount(SeedProduct.UpdateStockType.CancelReserve, item.Quantity, item.Bags)
                        Dim objTransactionLog As New SeedTransaction(_session, PublicEnum.SeedTransactionType.CancelReserve, ReserveNo, item.SeedProduct, item.Quantity, item.Bags)
                        Dim objToCancel As SeedTransaction = Session.FindObject(Of SeedTransaction)(CriteriaOperator.Parse("TransactionType='Reserve' and RefNo=?", ReserveNo))
                        If objToCancel IsNot Nothing Then
                            objToCancel.IsDelete = True
                        End If
                        '//InsertTransaction(Session, item, ActionType.Cancel)
                    End If
                Next

                Me.Save()
                Return True
            Catch ex As Exception
                Return False
            End Try
        End If
    End Function

    Public Function DoApprove() As Boolean Implements IApproveAble.DoApprove
        Return MarkAsComplete()
    End Function

    Public Function DoCancel() As Boolean Implements IApproveAble.DoCancel
        Return MarkAsCancel()
    End Function
End Class
