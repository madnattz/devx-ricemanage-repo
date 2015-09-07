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
<RuleCriteria("TransferMaterial.NotDelete", DefaultContexts.Delete, "SendStatus='Pending'", CustomMessageTemplate:="ไม่สามารถลบข้อมูลนี้ได้")> _
<ConditionalAppearance.Appearance("TransferMaterialDisableAllItems", criteria:="SendStatus!='Pending'", Enabled:=False, TargetItems:="*", Context:="DetailView")> _
Public Class TransferMaterial
    Inherits BaseObject
    Implements IApproveAble

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        Me.TransferDate = Date.Today
        Me.TransferType = PublicEnum.TransferType.Send
        Me.SendStatus = PublicEnum.SimsStatus.Pending
        DataOwner = GetCurrentSite()

        Dim collSiteSetting As New XPCollection(Of SiteSetting)(Session)
        If collSiteSetting.Count > 0 Then
            Dim objSite As Site = Session.FindObject(Of Site)(CriteriaOperator.Parse("SiteNo=?", collSiteSetting(0).SiteNo))
            If objSite IsNot Nothing Then
                Me.TransferFrom = objSite
            End If
        End If

    End Sub
    Protected Overrides Sub OnSaving()
        If (Me.fTransferNo Is Nothing) Then
            Dim prefix As String = ""
            Dim _year As String = (Date.Now.Year + 543).ToString
            prefix = _year

            Me.fTransferNo = String.Format("{0}/{1:D4}", prefix, DistributedIdGeneratorHelper.Generate(Me.Session.DataLayer, Me.GetType().FullName, prefix))

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

    <Persistent("TransferNo")> _
    Private fTransferNo As String
    <PersistentAlias("fTransferNo")> _
    Public ReadOnly Property TransferNo() As String
        Get
            Return fTransferNo
        End Get
    End Property

    'Dim fTransferNo As Guid
    'Public Property TransferNo() As Guid
    '    Get
    '        Return fTransferNo
    '    End Get
    '    Set(ByVal value As Guid)
    '        SetPropertyValue(Of Guid)("TransferNo", fTransferNo, value)
    '    End Set
    'End Property
    Dim fTransferType As PublicEnum.TransferType
    Public Property TransferType() As PublicEnum.TransferType
        Get
            Return fTransferType
        End Get
        Set(ByVal value As PublicEnum.TransferType)
            SetPropertyValue(Of PublicEnum.TransferType)("TransferType", fTransferType, value)
        End Set
    End Property
    Dim fTransferFrom As Site
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property TransferFrom() As Site
        Get
            Return fTransferFrom
        End Get
        Set(ByVal value As Site)
            SetPropertyValue(Of Site)("TransferFrom", fTransferFrom, value)
        End Set
    End Property
    Dim fTransferTo As Site
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property TransferTo() As Site
        Get
            Return fTransferTo
        End Get
        Set(ByVal value As Site)
            SetPropertyValue(Of Site)("TransferTo", fTransferTo, value)
        End Set
    End Property
    'Dim fTransferReason As PublicEnum.TransferFor
    'Public Property TransferReason() As PublicEnum.TransferFor
    '    Get
    '        Return fTransferReason
    '    End Get
    '    Set(ByVal value As PublicEnum.TransferFor)
    '        SetPropertyValue(Of PublicEnum.TransferFor)("TransferReason", fTransferReason, value)
    '    End Set
    'End Property
    Dim fTransferDate As DateTime
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property TransferDate() As DateTime
        Get
            Return fTransferDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("TransferDate", fTransferDate, value)
        End Set
    End Property
    'Dim fReceiveDate As DateTime
    'Public Property ReceiveDate() As DateTime
    '    Get
    '        Return fReceiveDate
    '    End Get
    '    Set(ByVal value As DateTime)
    '        SetPropertyValue(Of DateTime)("ReceiveDate", fReceiveDate, value)
    '    End Set
    'End Property
    Dim fTransferUser As String
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property TransferUser() As String
        Get
            Return fTransferUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("TransferUser", fTransferUser, value)
        End Set
    End Property
    Dim fTransferPosition As String
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property TransferPosition() As String
        Get
            Return fTransferPosition
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("TransferPosition", fTransferPosition, value)
        End Set
    End Property
    'Dim fReceiveUser As String
    'Public Property ReceiveUser() As String
    '    Get
    '        Return fReceiveUser
    '    End Get
    '    Set(ByVal value As String)
    '        SetPropertyValue(Of String)("ReceiveUser", fReceiveUser, value)
    '    End Set
    'End Property
    'Dim fReceivePostion As String
    'Public Property ReceivePostion() As String
    '    Get
    '        Return fReceivePostion
    '    End Get
    '    Set(ByVal value As String)
    '        SetPropertyValue(Of String)("ReceivePostion", fReceivePostion, value)
    '    End Set
    'End Property
    Dim fApproveUser As String
    <RuleRequiredField(TargetContextIDs:="Approval")> _
    Public Property ApproveUser() As String
        Get
            Return fApproveUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ApproveUser", fApproveUser, value)
        End Set
    End Property
    Dim fApprovePosition As String
    <RuleRequiredField(TargetContextIDs:="Approval")> _
    Public Property ApprovePosition() As String
        Get
            Return fApprovePosition
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ApprovePosition", fApprovePosition, value)
        End Set
    End Property
    Dim fSendStatus As PublicEnum.SimsStatus
    Public Property SendStatus() As PublicEnum.SimsStatus
        Get
            Return fSendStatus
        End Get
        Set(ByVal value As PublicEnum.SimsStatus)
            SetPropertyValue(Of PublicEnum.SimsStatus)("SendStatus", fSendStatus, value)
        End Set
    End Property

    <Browsable(False)> _
    Public ReadOnly Property Status As PublicEnum.SimsStatus
        Get
            Return SendStatus
        End Get
    End Property
    'Dim fReceiveStatus As PublicEnum.SimsStatus
    'Public Property ReceiveStatus() As PublicEnum.SimsStatus
    '    Get
    '        Return fReceiveStatus
    '    End Get
    '    Set(ByVal value As PublicEnum.SimsStatus)
    '        SetPropertyValue(Of PublicEnum.SimsStatus)("ReceiveStatus", fReceiveStatus, value)
    '    End Set
    'End Property
    Dim fRemark As String
    <Size(400)> _
    Public Property Remark() As String
        Get
            Return fRemark
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Remark", fRemark, value)
        End Set
    End Property
    <Association("TransferMaterialDetailReferencesTransferMaterial")> _
    Public ReadOnly Property TransferMaterialDetails() As XPCollection(Of TransferMaterialDetail)
        Get
            Return GetCollection(Of TransferMaterialDetail)("TransferMaterialDetails")
        End Get
    End Property

    Public Function MarkAsComplete() As Boolean
        If Not IsDeleted Then
            If Not TransferMaterialDetails.Count > 0 Then
                MsgBox("ไม่พบรายการวัสดุการผลิต กรุณาตรวจสอบข้อมูลอีกครั้ง", MsgBoxStyle.Information, "")
                Exit Function
            End If
            Dim _session As Session = Me.Session
            Try
                Me.fSendStatus = PublicEnum.SimsStatus.Approve

                For i As Integer = 0 To TransferMaterialDetails.Count - 1
                    If TransferMaterialDetails(i).MaterialProduct IsNot Nothing Then
                        '// Update ตัดยอดคงคลัง
                        TransferMaterialDetails(i).MaterialProduct.UpdateStockAmount(SeedProduct.UpdateStockType.Outcome, TransferMaterialDetails(i).Quantity)
                        '// รายการเคลื่อนไหวคลัง
                        InsertTransaction(_session, TransferMaterialDetails(i), ActionType.Approve)
                    End If
                Next
                MyBase.Save()
                '_session.CommitTransaction()
                Return True
            Catch ex As Exception
                '_session.RollbackTransaction()
                Return False
            End Try
        End If
    End Function

    Public Function MarkAsCancel() As Boolean
        If Not IsDeleted Then
            Dim _session As Session = Me.Session
            Try
                Me.fSendStatus = PublicEnum.SimsStatus.Cancel
                'Me.fReceiveStatus = PublicEnum.SimsStatus.Cancel
                MyBase.Save()

                For i As Integer = 0 To TransferMaterialDetails.Count - 1
                    If TransferMaterialDetails(i).MaterialProduct IsNot Nothing Then
                        '// Update เพิ่มยอดคงคลัง
                        TransferMaterialDetails(i).MaterialProduct.UpdateStockAmount(SeedProduct.UpdateStockType.Income, TransferMaterialDetails(i).Quantity)
                        '// รายการเคลื่อนไหวคลัง
                        InsertTransaction(_session, TransferMaterialDetails(i), ActionType.Cancel)
                        Dim objToCancel As MaterialTransaction = Session.FindObject(Of MaterialTransaction)(CriteriaOperator.Parse("TransactionType='TransferOut' and RefNo=?", TransferNo))
                        If objToCancel IsNot Nothing Then
                            objToCancel.IsDelete = True
                        End If
                    End If
                Next

                '_session.CommitTransaction()
                Return True
            Catch ex As Exception
                '_session.RollbackTransaction()
                Return False
            End Try
        End If
    End Function

    Private Sub InsertTransaction(_session As Session, objItem As TransferMaterialDetail, actionType As ActionType)
        Dim objTrans As New MaterialTransaction(_session)
        If actionType = TransferMaterial.ActionType.Approve Then
            objTrans.TransactionType = PublicEnum.MaterialTransactionType.TransferOut
            objTrans.Income = 0
            objTrans.Outcome = objItem.Quantity
        Else
            objTrans.TransactionType = PublicEnum.MaterialTransactionType.CancelTransferOut
            objTrans.Income = objItem.Quantity
            objTrans.Outcome = 0
            objTrans.IsDelete = True
        End If

        objTrans.RefNo = fTransferNo
        objTrans.MaterialProduct = objItem.MaterialProduct
        objTrans.ProductName = objItem.MaterialProduct.Material.MaterialName
        objTrans.ProductCode = objItem.MaterialProduct.ProductCode

        ' objTrans.CollectQuantity = objReceiveDetail.Quantity
        objTrans.TransactionDate = Date.Now
        objTrans.TransactionBy = CType(SecuritySystem.CurrentUser, User).DisplayName
        objTrans.Save()

    End Sub

    Enum ActionType
        Approve
        Cancel
    End Enum

    Public Function DoApprove() As Boolean Implements IApproveAble.DoApprove
        Return MarkAsComplete()
    End Function

    Public Function DoCancel() As Boolean Implements IApproveAble.DoCancel
        Return MarkAsCancel()
    End Function
End Class
