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
'<ConditionalAppearance.Appearance("FactoryProcessSeedDisableAllItems", criteria:="Status!='Pending'", Enabled:=False, TargetItems:="*", Context:="DetailView")> _

<DefaultClassOptions()> _
Public Class FactoryProcessSeed ' Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        Me.FactoryNo = PublicEnum.EnumFactoryNo.Factory1
        ' Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).
    End Sub
    Protected Overrides Sub OnSaving()
        If (Me.fProcessNo Is Nothing) Then
            Dim prefix As String = ""
            Dim _year As String = (Date.Now.Year + 543).ToString
            prefix = _year

            Me.fProcessNo = String.Format("{0}/{1:D4}", prefix, DistributedIdGeneratorHelper.Generate(Me.Session.DataLayer, Me.GetType().FullName, prefix))

        End If
        MyBase.OnSaving()
    End Sub
    <Persistent("ProcessNo")> _
    Private fProcessNo As String
    <PersistentAlias("fProcessNo")> _
    Public ReadOnly Property ProcessNo() As String
        Get
            Return fProcessNo
        End Get
    End Property
    'Dim fProcessDate As DateTime
    'Public Property ProcessDate() As DateTime
    '    Get
    '        Return fProcessDate
    '    End Get
    '    Set(ByVal value As DateTime)
    '        SetPropertyValue(Of DateTime)("ProcessDate", fProcessDate, value)
    '    End Set
    'End Property
    Dim fFactoryNo As PublicEnum.EnumFactoryNo
    Public Property FactoryNo() As PublicEnum.EnumFactoryNo
        Get
            Return fFactoryNo
        End Get
        Set(ByVal value As PublicEnum.EnumFactoryNo)
            SetPropertyValue(Of PublicEnum.EnumFactoryNo)("FactoryNo", fFactoryNo, value)
        End Set
    End Property
    Dim fFactoryInventory As FactoryInventory
    <ImmediatePostData()> _
    Public Property FactoryInventory() As FactoryInventory
        Get
            Return fFactoryInventory
        End Get
        Set(ByVal value As FactoryInventory)
            SetPropertyValue(Of FactoryInventory)("FactoryInventory", fFactoryInventory, value)
        End Set
    End Property
    Dim fProcessLot As Integer
    Public Property ProcessLot() As Integer
        Get
            Return fProcessLot
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ProcessLot", fProcessLot, value)
        End Set
    End Property
    'Dim fInputQuantity As Double
    'Public Property InputQuantity() As Double
    '    Get
    '        Return fInputQuantity
    '    End Get
    '    Set(ByVal value As Double)
    '        SetPropertyValue(Of Double)("InputQuantity", fInputQuantity, value)
    '    End Set
    'End Property
    'Dim fOutputQuantity As Double
    'Public Property OutputQuantity() As Double
    '    Get
    '        Return fOutputQuantity
    '    End Get
    '    Set(ByVal value As Double)
    '        SetPropertyValue(Of Double)("OutputQuantity", fOutputQuantity, value)
    '    End Set
    'End Property
    'Dim fNormalTime As Boolean
    'Public Property NormalTime() As Boolean
    '    Get
    '        Return fNormalTime
    '    End Get
    '    Set(ByVal value As Boolean)
    '        SetPropertyValue(Of Boolean)("NormalTime", fNormalTime, value)
    '    End Set
    'End Property
    'Dim fOverTime As Boolean
    'Public Property OverTime() As Boolean
    '    Get
    '        Return fOverTime
    '    End Get
    '    Set(ByVal value As Boolean)
    '        SetPropertyValue(Of Boolean)("OverTime", fOverTime, value)
    '    End Set
    'End Property
    'Dim fShiftTime As Boolean
    'Public Property ShiftTime() As Boolean
    '    Get
    '        Return fShiftTime
    '    End Get
    '    Set(ByVal value As Boolean)
    '        SetPropertyValue(Of Boolean)("ShiftTime", fShiftTime, value)
    '    End Set
    'End Property
    'Dim fStartTime As String
    '<Size(10)> _
    'Public Property StartTime() As String
    '    Get
    '        Return fStartTime
    '    End Get
    '    Set(ByVal value As String)
    '        SetPropertyValue(Of String)("StartTime", fStartTime, value)
    '    End Set
    'End Property
    'Dim fEndTime As String
    '<Size(10)> _
    'Public Property EndTime() As String
    '    Get
    '        Return fEndTime
    '    End Get
    '    Set(ByVal value As String)
    '        SetPropertyValue(Of String)("EndTime", fEndTime, value)
    '    End Set
    'End Property

    Dim fStatus As PublicEnum.FactoryProcessStatus
    Public Property Status() As PublicEnum.FactoryProcessStatus
        Get
            Return fStatus
        End Get
        Set(ByVal value As PublicEnum.FactoryProcessStatus)
            SetPropertyValue(Of PublicEnum.FactoryProcessStatus)("Status", fStatus, value)
        End Set
    End Property

    <ModelDefault("DisPlayFormat", ("{0:#,#0.00}"))> _
    <PersistentAlias("FactoryProcessSeedDetails.Sum(OutputQuantity)")> _
    <ImmediatePostData()> _
    Public ReadOnly Property SumOutputSeed() As Double
        Get
            Try
                Dim tempObject As Object
                tempObject = EvaluateAlias("SumOutputSeed")
                Return CDbl(tempObject)
            Catch ex As Exception
                Return 0
            End Try

        End Get

    End Property

    '<Association("FactoryProcessSeed-FactoryProcessSeedDetails", GetType(FactoryProcessSeedDetail))> _
    '<ImmediatePostData()> _
    'Public ReadOnly Property FactoryProcessSeedDetails() As XPCollection(Of FactoryProcessSeedDetail)
    '    Get
    '        Return GetCollection(Of FactoryProcessSeedDetail)("FactoryProcessSeedDetails")
    '    End Get
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

    Private Sub InsertTransaction(SeedProduct As SeedProduct, quantity As Double, actionType As ActionType)
        Dim objFacTrans As New FactoryTransaction(Session)
        If actionType = ReceiveSeed.ActionType.Approve Then
            objFacTrans.TransactionType = PublicEnum.FactoryTransactionType.Process
            objFacTrans.Income = 0
            objFacTrans.Outcome = quantity
        Else
            objFacTrans.TransactionType = PublicEnum.FactoryTransactionType.CancelProcess
            objFacTrans.Income = quantity
            objFacTrans.Outcome = 0
        End If

        objFacTrans.RefNo = Me.fProcessNo 'Me.fFactoryPickSeed.PickNo
        'objFacTrans.PickRefNo = Me.PickNo
        objFacTrans.SeedProduct = SeedProduct
        objFacTrans.ProductName = SeedProduct.ProductName
        objFacTrans.ProductCode = SeedProduct.ProductCode
        'objTrans.CollectQuantity = 0
        objFacTrans.FactoryNo = Me.fFactoryNo
        objFacTrans.TransactionDate = Date.Now
        objFacTrans.TransactionBy = CType(SecuritySystem.CurrentUser, User).DisplayName
        objFacTrans.Save()
    End Sub

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



    Enum ActionType
        Approve
        Cancel
    End Enum

End Class
