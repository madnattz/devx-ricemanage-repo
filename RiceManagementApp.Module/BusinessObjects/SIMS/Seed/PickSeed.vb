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
<RuleCriteria("PickSeed.NotDelete", DefaultContexts.Delete, "Status='Pending'", CustomMessageTemplate:="ไม่สามารถลบข้อมูลนี้ได้")> _
<ConditionalAppearance.Appearance("PickSeedDisableAllItems", criteria:="Status!='Pending'", Enabled:=False, TargetItems:="*", Context:="DetailView")> _
Public Class PickSeed
    Inherits BaseObject
    Implements IApproveAble

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        Me.PickDate = Date.Now
        Me.fIsDataFromFactory = False
        Me.fStatus = PublicEnum.SimsStatus.Pending
        Me.IsSubmitToCenter = False
        DataOwner = GetCurrentSite()
    End Sub
    Protected Overrides Sub OnSaving()
        If (Me.fPickNo Is Nothing) Then
            Dim prefix As String = ""
            Dim _year As String = (Date.Now.Year + 543).ToString
            prefix = _year

            Me.fPickNo = String.Format("{0}/{1:D4}", prefix, DistributedIdGeneratorHelper.Generate(Me.Session.DataLayer, Me.GetType().FullName, prefix))

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

    <Persistent("PickNo")> _
    Private fPickNo As String
    <PersistentAlias("fPickNo")> _
    Public ReadOnly Property PickNo() As String
        Get
            Return fPickNo
        End Get
    End Property
    Dim fPickDate As DateTime
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property PickDate() As DateTime
        Get
            Return fPickDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("PickDate", fPickDate, value)
        End Set
    End Property
    Dim fPickType As PublicEnum.PickFor
    '<ConditionalAppearance.Appearance("PickTypeNormal", criteria:="IsOtherPickType=False", Enabled:=True, Context:="DetailView")> _
    Public Property PickType() As PublicEnum.PickFor
        Get
            Return fPickType
        End Get
        Set(ByVal value As PublicEnum.PickFor)
            SetPropertyValue(Of PublicEnum.PickFor)("PickType", fPickType, value)
            If Not PickType = PublicEnum.PickFor.ProcessSeed Then
                FactoryNo = Nothing
            Else
                FactoryNo = PublicEnum.EnumFactoryNo.Factory1
            End If
        End Set
    End Property
    Dim fFactoryNo As PublicEnum.EnumFactoryNo
    Public Property FactoryNo() As PublicEnum.EnumFactoryNo
        Get
            Return fFactoryNo
        End Get
        Set(ByVal value As PublicEnum.EnumFactoryNo)
            SetPropertyValue(Of PublicEnum.EnumFactoryNo)("FactoryNo", fFactoryNo, value)
        End Set
    End Property
    Dim fIsOtherPickType As Boolean
    <ImmediatePostData()> _
    Public Property IsOtherPickType() As Boolean
        Get
            Return fIsOtherPickType
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("IsOtherPickType", fIsOtherPickType, value)
            If Not IsLoading AndAlso Not IsSaving Then
                If value = True Then
                    PickType = Nothing
                Else
                    PickTypeOther = Nothing
                End If
            End If
        End Set
    End Property
    Dim fPickTypeOther As String
    <Size(50)> _
    Public Property PickTypeOther() As String
        Get
            Return fPickTypeOther
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PickTypeOther", fPickTypeOther, value)
        End Set
    End Property

    Public ReadOnly Property PickTypeText() As Object
        Get
            Dim ret_value As New Object
            Try
                If IsOtherPickType = False Then
                    ret_value = fPickType
                Else
                    ret_value = fPickTypeOther
                End If
            Catch ex As Exception
            End Try
            Return ret_value
        End Get
    End Property

    Dim fRefNo As String
    <Size(20)> _
    Public Property RefNo() As String
        Get
            Return fRefNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("RefNo", fRefNo, value)
        End Set
    End Property

    Dim fPickUser As String
    <Size(50)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property PickUser() As String
        Get
            Return fPickUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PickUser", fPickUser, value)
        End Set
    End Property
    Dim fPickPosition As String
    <Size(50)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property PickPosition() As String
        Get
            Return fPickPosition
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PickPosition", fPickPosition, value)
        End Set
    End Property
    Dim fApproveUser As String
    <Size(50)> _
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
    <Size(50)> _
    <RuleRequiredField(TargetContextIDs:="Approval")> _
    Public Property ApprovePosition() As String
        Get
            Return fApprovePosition
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ApprovePosition", fApprovePosition, value)
        End Set
    End Property

    Dim fRequestUser As String
    <Size(50)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property RequestUser() As String
        Get
            Return fRequestUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("RequestUser", fRequestUser, value)
        End Set
    End Property
    Dim fRequestPosition As String
    <Size(50)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property RequestPosition() As String
        Get
            Return fRequestPosition
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("RequestPosition", fRequestPosition, value)
        End Set
    End Property

    Dim fStatus As Integer
    Public Property Status() As PublicEnum.SimsStatus
        Get
            Return fStatus
        End Get
        Set(ByVal value As PublicEnum.SimsStatus)
            SetPropertyValue(Of Integer)("Status", fStatus, value)
        End Set
    End Property
    Dim fRemark As String
    <Size(200)> _
    Public Property Remark() As String
        Get
            Return fRemark
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Remark", fRemark, value)
        End Set
    End Property
    <Association("PickSeedDetailReferencesPickSeed", GetType(PickSeedDetail))> _
    Public ReadOnly Property PickSeedDetails() As XPCollection(Of PickSeedDetail)
        Get
            Return GetCollection(Of PickSeedDetail)("PickSeedDetails")
        End Get
    End Property

    Dim fFactoryPickSeed As FactoryPickSeed
    <Browsable(False)> _
    Public Property FactoryPickSeed() As FactoryPickSeed
        Get
            Return fFactoryPickSeed
        End Get
        Set(ByVal value As FactoryPickSeed)
            SetPropertyValue(Of FactoryPickSeed)("FactoryPickSeed", fFactoryPickSeed, value)
        End Set
    End Property

    Dim fIsDataFromFactory As Boolean
    '<Browsable(False)> _
    Public Property IsDataFromFactory() As Boolean
        Get
            Return fIsDataFromFactory
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("IsDataFromFactory", fIsDataFromFactory, value)
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


    Public Function MarkAsComplete() As Boolean
        If Not IsDeleted Then
            If Not PickSeedDetails.Count > 0 Then
                MsgBox("ไม่พบรายการเมล็ดพันธุ์ กรุณาตรวจสอบข้อมูลอีกครั้ง", MsgBoxStyle.Information, "")
                Exit Function
            End If

            Dim _session As Session = Me.Session

            Try
                Me.Status = PublicEnum.SimsStatus.Approve

                For i As Integer = 0 To PickSeedDetails.Count - 1
                    If PickSeedDetails(i).SeedProduct IsNot Nothing Then
                        '// Update ตัดยอดคงคลัง
                        PickSeedDetails(i).SeedProduct.UpdateStockAmount(SeedProduct.UpdateStockType.Outcome, PickSeedDetails(i).Quantity, PickSeedDetails(i).Bags)
                        '// รายการเคลื่อนไหวคลัง
                        Dim objTransactionLog As New SeedTransaction(_session, PublicEnum.SeedTransactionType.Pick, fPickNo, PickSeedDetails(i).SeedProduct, PickSeedDetails(i).Quantity, PickSeedDetails(i).Bags)

                        If fPickType = PublicEnum.PickFor.ProcessSeed Then
                            '// รายการเคลื่อนไหวโรงงงาน
                            InsertFactoryTransaction(_session, PickSeedDetails(i), ActionType.Approve)
                            '//Update ยอดคลังของโรงงาน
                            UpdateFactoryInventory(PickSeedDetails(i).SeedProduct, Me.FactoryNo, PickSeedDetails(i).Quantity, ActionType.Approve)

                        End If
                    End If
                Next

                '//กรณีจ่ายแบบไม่เบิกจากโรงงาน fIsDataFromFactory = False
                If fIsDataFromFactory = False Then
                    '//สร้างใบเบิกให้โรงงานอัตโนมัติ เพื่อเก็บประวัติการเบิกในระบบ
                    If fPickType = PublicEnum.PickFor.ProcessSeed Then
                        CreateNewRequestForm(_session, Me)
                    End If
                Else
                    Me.FactoryPickSeed.InventoryStatus = PublicEnum.SimsStatus.Approve
                End If

                ' MyBase.Save()
                _session.CommitTransaction()
                Return True
            Catch ex As Exception
                _session.RollbackTransaction()
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
                '//กรณีจ่ายแบบไม่เบิกจากโรงงาน fIsDataFromFactory = False
                If IsDataFromFactory = True Then
                    If PickType = PublicEnum.PickFor.ProcessSeed Then
                        Me.FactoryPickSeed.InventoryStatus = PublicEnum.SimsStatus.Cancel
                    End If
                End If

                For i As Integer = 0 To PickSeedDetails.Count - 1
                    If PickSeedDetails(i).SeedProduct IsNot Nothing Then
                        '//Update เพิ่มยอดคงคลัง
                        PickSeedDetails(i).SeedProduct.UpdateStockAmount(SeedProduct.UpdateStockType.Income, PickSeedDetails(i).Quantity, PickSeedDetails(i).Bags)
                        '//รายการเคลื่อนไหวคลัง
                        '//InsertTransaction(Session, PickSeedDetails(i), ActionType.Cancel)
                        Dim objTransactionLog As New SeedTransaction(_session, PublicEnum.SeedTransactionType.CancelPick, PickNo, PickSeedDetails(i).SeedProduct, PickSeedDetails(i).Quantity, PickSeedDetails(i).Bags)
                        Dim objToCancel As SeedTransaction = Session.FindObject(Of SeedTransaction)(CriteriaOperator.Parse("TransactionType='Pick' and RefNo=?", PickNo))
                        If objToCancel IsNot Nothing Then
                            objToCancel.IsDelete = True
                        End If

                        If PickType = PublicEnum.PickFor.ProcessSeed Then
                            '// รายการเคลื่อนไหวโรงงงาน
                            InsertFactoryTransaction(_session, PickSeedDetails(i), ActionType.Cancel)
                            '//Update ตัดคืนยอดคลังของโรงงาน
                            UpdateFactoryInventory(PickSeedDetails(i).SeedProduct, Me.FactoryNo, PickSeedDetails(i).Quantity, ActionType.Cancel)

                        End If
                    End If
                Next

                'MyBase.Save()
                Session.CommitTransaction()
                Return True

            Catch ex As Exception
                Session.RollbackTransaction()
                Return False
            End Try
        End If
    End Function

    Private Sub InsertFactoryTransaction(_session As Session, _objPickSeedDetail As PickSeedDetail, _actionType As ActionType)
        Dim objFacTrans As New FactoryTransaction(_session)
        If _actionType = ReceiveSeed.ActionType.Approve Then
            objFacTrans.TransactionType = PublicEnum.FactoryTransactionType.Pick
            objFacTrans.Income = _objPickSeedDetail.Quantity
            objFacTrans.Outcome = 0
        Else
            objFacTrans.TransactionType = PublicEnum.FactoryTransactionType.CancelPick
            objFacTrans.Income = 0
            objFacTrans.Outcome = _objPickSeedDetail.Quantity
        End If

        objFacTrans.RefNo = _objPickSeedDetail.PickSeed.PickNo
        objFacTrans.SeedProduct = _objPickSeedDetail.SeedProduct
        objFacTrans.ProductName = _objPickSeedDetail.SeedProduct.ProductName
        objFacTrans.ProductCode = _objPickSeedDetail.SeedProduct.ProductCode
        objFacTrans.FactoryNo = _objPickSeedDetail.PickSeed.FactoryNo
        objFacTrans.TransactionDate = Date.Now
        objFacTrans.TransactionBy = CType(SecuritySystem.CurrentUser, User).DisplayName

        If _objPickSeedDetail.PickSeed.IsDataFromFactory = False Then
            objFacTrans.Remark = "จ่ายจากคลังแบบ Manual ไม่ผ่านระบบการเบิกของโรงงาน"
        Else
            objFacTrans.Remark = ""
        End If

        objFacTrans.Save()

    End Sub

    Public Sub UpdateFactoryInventory(objProduct As SeedProduct, facNo As PublicEnum.EnumFactoryNo, quantity As Double, actionType As ActionType)
        Dim objFacInventory As FactoryInventory = Session.FindObject(Of FactoryInventory)(CriteriaOperator.Parse("SeedProduct=? and FactoryNo=?", objProduct, facNo))
        If objFacInventory Is Nothing Then
            Dim objNewFac As New FactoryInventory(Session)
            objNewFac.FactoryNo = facNo
            objNewFac.SeedProduct = objProduct
            objNewFac.ReceiveAmount = quantity
        Else
            If actionType = PickSeed.ActionType.Approve Then
                objFacInventory.UpdateStockAmount(FactoryInventory.UpdateStockType.income, quantity)
            Else
                objFacInventory.UpdateStockAmount(FactoryInventory.UpdateStockType.outcome, quantity)
            End If
        End If
    End Sub

    Public Sub CreateNewRequestForm(_session As Session, _pickSeedForm As PickSeed)
        Dim objNewRequestForm As New FactoryPickSeed(_session)
        objNewRequestForm.PickDate = Today
        objNewRequestForm.PickType = _pickSeedForm.fPickType
        objNewRequestForm.FactoryNo = _pickSeedForm.fFactoryNo
        objNewRequestForm.PickUser = _pickSeedForm.fRequestUser
        objNewRequestForm.PickPosition = _pickSeedForm.fRequestPosition
        objNewRequestForm.Status = PublicEnum.SimsStatus.Approve
        objNewRequestForm.InventoryStatus = PublicEnum.SimsStatus.Approve

        objNewRequestForm.Remark = "จ่ายจากคลังโดยไม่ผ่านระบบเบิกเมล็ดพันธุ์ของโรงงาน จากเลขที่จ่าย " & _pickSeedForm.fPickNo & " วันที่จ่าย " & _pickSeedForm.fPickDate.ToString("d MMMM yy") & " เอกสารเลขที่ " & _pickSeedForm.fRefNo

        For Each item As PickSeedDetail In _pickSeedForm.PickSeedDetails
            Dim objNewRequestItem As New FactoryPickSeedDetail(_session)
            objNewRequestItem.PickSeed = objNewRequestForm
            objNewRequestItem.SeedProductCode = item.SeedProductCode
            objNewRequestItem.SeedProduct = item.SeedProduct
            objNewRequestItem.Quantity = item.Quantity
            objNewRequestItem.Bags = item.Bags
        Next

        objNewRequestForm.Save()
        '//บันทึกข้อมูลการเบิกใน PickSeed

        Me.fFactoryPickSeed = objNewRequestForm

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
