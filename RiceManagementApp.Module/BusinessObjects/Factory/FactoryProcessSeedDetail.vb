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
Public Class FactoryProcessSeedDetail ' Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
        Me.ProcessDate = Date.Today
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        ' Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).
    End Sub

    Dim fFactoryInventory As FactoryInventory
    <Association("FactoryInventory-FactoryProcessSeedDetails")> _
    Public Property FactoryInventory() As FactoryInventory
        Get
            Return fFactoryInventory
        End Get
        Set(ByVal value As FactoryInventory)
            SetPropertyValue(Of FactoryInventory)("FactoryInventory", fFactoryInventory, value)
        End Set
    End Property

    Dim fProcessDate As DateTime
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property ProcessDate() As DateTime
        Get
            Return fProcessDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ProcessDate", fProcessDate, value)
        End Set
    End Property
    Dim fOutputQuantity As Double
    <RuleValueComparison(DefaultContexts.Save, ValueComparisonType.GreaterThan, 0)> _
    Public Property OutputQuantity() As Double
        Get
            Return fOutputQuantity
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("OutputQuantity", fOutputQuantity, value)
        End Set
    End Property
    Dim fNormalTime As Boolean
    Public Property NormalTime() As Boolean
        Get
            Return fNormalTime
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("NormalTime", fNormalTime, value)
        End Set
    End Property
    Dim fOverTime As Boolean
    Public Property OverTime() As Boolean
        Get
            Return fOverTime
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("OverTime", fOverTime, value)
        End Set
    End Property
    Dim fShiftTime As Boolean
    Public Property ShiftTime() As Boolean
        Get
            Return fShiftTime
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("ShiftTime", fShiftTime, value)
        End Set
    End Property
    Dim fStartTime As String
    <Size(10)> _
    Public Property StartTime() As String
        Get
            Return fStartTime
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("StartTime", fStartTime, value)
        End Set
    End Property
    Dim fEndTime As String
    <Size(10)> _
    Public Property EndTime() As String
        Get
            Return fEndTime
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("EndTime", fEndTime, value)
        End Set
    End Property

    Dim fImproveComplete As Boolean
    <XafDisplayName("เสร็จสิ้นการปรับปรุงสภาพ")> _
    Public Property ImproveComplete() As Boolean
        Get
            Return fImproveComplete
        End Get
        Set(value As Boolean)
            SetPropertyValue(Of Boolean)("ImproveComplete", fImproveComplete, value)
        End Set
    End Property

    'Dim fStatus As Integer
    'Public Property Status() As PublicEnum.SimsStatus
    '    Get
    '        Return fStatus
    '    End Get
    '    Set(ByVal value As PublicEnum.SimsStatus)
    '        SetPropertyValue(Of Integer)("Status", fStatus, value)
    '    End Set
    'End Property

    '<Action(Caption:="อนุมัติ", ConfirmationMessage:="ท่านต้องการบันทึกข้อมูล ใช่หรือไม่?", ImageName:="Action_Grant", AutoCommit:=True, TargetObjectsCriteria:="Status='Pending'")> _
    'Public Sub MarkAsComplete()
    '    If Not IsDeleted Then
    '        Try
    '            UpdateFactoryInventory(Me.fInputQuantity, ActionType.Approve)
    '            InsertTransaction(Me.FactoryInventory.SeedProduct, Me.fInputQuantity, ActionType.Approve)
    '            Me.fStatus = PublicEnum.SimsStatus.Approve
    '            MyBase.Save()
    '            Session.CommitTransaction()
    '        Catch ex As Exception
    '            Session.RollbackTransaction()
    '        End Try
    '    End If
    'End Sub
    '<Action(Caption:="ยกเลิก", ConfirmationMessage:="ท่านต้องการบันทึกข้อมูล ใช่หรือไม่?", ImageName:="Action_Deny", AutoCommit:=True, TargetObjectsCriteria:="Status='Approve'")> _
    'Public Sub MarkAsCancel()
    '    If Not IsDeleted Then
    '        Try
    '            UpdateFactoryInventory(Me.fInputQuantity, ActionType.Cancel)
    '            InsertTransaction(Me.FactoryInventory.SeedProduct, Me.fInputQuantity, ActionType.Cancel)
    '            Me.fStatus = PublicEnum.SimsStatus.Cancel
    '            MyBase.Save()
    '            Session.CommitTransaction()
    '        Catch ex As Exception
    '            Session.RollbackTransaction()
    '        End Try
    '    End If
    'End Sub

    'Private Sub InsertTransaction(SeedProduct As SeedProduct, quantity As Double, actionType As ActionType)
    '    Dim objFacTrans As New FactoryTransaction(Session)
    '    If actionType = ReceiveSeed.ActionType.Approve Then
    '        objFacTrans.TransactionType = PublicEnum.FactoryTransactionType.Process
    '        objFacTrans.Income = 0
    '        objFacTrans.Outcome = quantity
    '    Else
    '        objFacTrans.TransactionType = PublicEnum.FactoryTransactionType.CancelProcess
    '        objFacTrans.Income = quantity
    '        objFacTrans.Outcome = 0
    '    End If

    '    objFacTrans.RefNo = Me.fProcessNo 'Me.fFactoryPickSeed.PickNo
    '    'objFacTrans.PickRefNo = Me.PickNo
    '    objFacTrans.SeedProduct = SeedProduct
    '    objFacTrans.ProductName = SeedProduct.ProductName
    '    objFacTrans.ProductCode = SeedProduct.ProductCode
    '    'objTrans.CollectQuantity = 0
    '    objFacTrans.FactoryNo = Me.fFactoryNo
    '    objFacTrans.TransactionDate = Date.Now
    '    objFacTrans.TransactionBy = CType(SecuritySystem.CurrentUser, User).DisplayName
    '    objFacTrans.Save()
    'End Sub

    'Public Sub UpdateFactoryInventory(quantity As Double, actionType As ActionType)
    '    Try
    '        If actionType = FactoryProcessSeed.ActionType.Approve Then
    '            Me.FactoryInventory.ProcessAmount += quantity
    '        Else
    '            Me.FactoryInventory.ProcessAmount -= quantity
    '        End If
    '        Me.FactoryInventory.UpdateDate = Date.Now

    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Enum ActionType
    '    Approve
    '    Cancel
    'End Enum

End Class
