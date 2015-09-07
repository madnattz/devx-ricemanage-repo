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

<RuleCriteria("ReceiveMaterial.NotDelete", DefaultContexts.Delete, "Status='Pending'", CustomMessageTemplate:="ไม่สามารถลบข้อมูลนี้ได้")> _
<ConditionalAppearance.Appearance("ReceiveMaterialDisableAllItems", criteria:="Status!='Pending'", Enabled:=False, TargetItems:="*", Context:="DetailView")> _
<DefaultClassOptions()> _
Public Class ReceiveMaterial
    Inherits BaseObject
    Implements IApproveAble

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        Me.ReceiveDate = Date.Now
        Me.BuyerType = PublicEnum.BuyerType.Local
        DataOwner = GetCurrentSite()
    End Sub

    Protected Overrides Sub OnSaving()
        If (Me.ReceiveNo Is Nothing) Then
            Dim prefix As String = ""
            Dim _year As String = (Date.Now.Year + 543).ToString
            prefix = _year

            Me.fReceiveNo = String.Format("{0}/{1:D4}", prefix, DistributedIdGeneratorHelper.Generate(Me.Session.DataLayer, Me.GetType().FullName, prefix))

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

    <Persistent("ReceiveNo")> _
    Private fReceiveNo As String
    <PersistentAlias("fReceiveNo")> _
    Public ReadOnly Property ReceiveNo() As String
        Get
            Return fReceiveNo
        End Get
    End Property

    'Dim fReceiveNo As String
    '<Size(20)> _
    'Public Property ReceiveNo() As String
    '    Get
    '        Return fReceiveNo
    '    End Get
    '    Set(ByVal value As String)
    '        SetPropertyValue(Of String)("ReceiveNo", fReceiveNo, value)
    '    End Set
    'End Property
    Dim fReceiveDate As DateTime
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property ReceiveDate() As DateTime
        Get
            Return fReceiveDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ReceiveDate", fReceiveDate, value)
        End Set
    End Property

    Dim fBuyerType As PublicEnum.BuyerType ', Context:="DetailView"
    Public Property BuyerType() As PublicEnum.BuyerType
        Get
            Return fBuyerType
        End Get
        Set(ByVal value As PublicEnum.BuyerType)
            SetPropertyValue(Of PublicEnum.BuyerType)("BuyerType", fBuyerType, value)
        End Set
    End Property

    Dim fRefNo As String
    <Size(10)> _
    Public Property RefNo() As String
        Get
            Return fRefNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("RefNo", fRefNo, value)
        End Set
    End Property
    Dim fSendUser As String
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property SendUser() As String
        Get
            Return fSendUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SendUser", fSendUser, value)
        End Set
    End Property
    Dim fSendUserPosition As String
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property SendUserPosition() As String
        Get
            Return fSendUserPosition
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SendUserPosition", fSendUserPosition, value)
        End Set
    End Property
    Dim fReceiveUser As String
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property ReceiveUser() As String
        Get
            Return fReceiveUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ReceiveUser", fReceiveUser, value)
        End Set
    End Property
    Dim fReceiveUserPosition As String
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property ReceiveUserPosition() As String
        Get
            Return fReceiveUserPosition
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ReceiveUserPosition", fReceiveUserPosition, value)
        End Set
    End Property
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
    Dim fApproveUserPosition As String
    <RuleRequiredField(TargetContextIDs:="Approval")> _
    Public Property ApproveUserPosition() As String
        Get
            Return fApproveUserPosition
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ApproveUserPosition", fApproveUserPosition, value)
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
    <Association("ReceiveMaterial-ReceiveMaterialDetails", GetType(ReceiveMaterialDetail))> _
    Public ReadOnly Property ReceiveMaterialDetails() As XPCollection(Of ReceiveMaterialDetail)
        Get
            Return GetCollection(Of ReceiveMaterialDetail)("ReceiveMaterialDetails")
        End Get
    End Property

    Public Function MarkAsComplete() As Boolean
        If Not IsDeleted Then
            If Not ReceiveMaterialDetails.Count > 0 Then
                MsgBox("ไม่พบรายการ กรุณาตรวจสอบข้อมูลอีกครั้ง", MsgBoxStyle.Information, "")
                Exit Function
            End If
            Try
                Me.fStatus = PublicEnum.SimsStatus.Approve
                '//สร้าง Product
                MyBase.Save()

                If ReceiveMaterialDetails.Count > 0 Then
                    Dim criteria As String = "Material=? and MaterialYear=? and MoneyType=? and Lot=?"
                    For i As Integer = 0 To ReceiveMaterialDetails.Count - 1
                        Dim _Material As Material = ReceiveMaterialDetails(i).Material
                        Dim _MaterialYear As String = ReceiveMaterialDetails(i).MaterialYear
                        Dim _MoneyType As MoneyType = ReceiveMaterialDetails(i).MoneyType
                        Dim _Lot As String = ReceiveMaterialDetails(i).Lot

                        Dim objProduct As MaterialProduct = Session.FindObject(Of MaterialProduct) _
                                                        (CriteriaOperator.Parse(criteria, _Material,
                                                                                            _MaterialYear, _
                                                                                            _MoneyType, _
                                                                                            _Lot))
                        If objProduct Is Nothing Then
                            Dim objNewProduct As New MaterialProduct(Session)
                            Dim _MaterialId As String = ReceiveMaterialDetails(i).Material.MaterialCode
                            Dim _Year As String = ReceiveMaterialDetails(i).MaterialYear
                            Dim _MoneyTypeId As String = ReceiveMaterialDetails(i).MoneyType.MoneyTypeId
                            Dim _lotNo As String = ReceiveMaterialDetails(i).Lot
                            ' 5-01-2557-1
                            objNewProduct.ProductCode = String.Format("5-{0}-{1}-{2}-{3}", _
                                                                      _MaterialId.ToString.PadLeft(2, "0"), _
                                                                      _Year, _
                                                                      _MoneyTypeId,
                                                                      _lotNo.ToString.PadLeft(3, "0"))
                            objNewProduct.MaterialYear = ReceiveMaterialDetails(i).MaterialYear
                            objNewProduct.MoneyType = ReceiveMaterialDetails(i).MoneyType
                            objNewProduct.Material = ReceiveMaterialDetails(i).Material
                            objNewProduct.Lot = ReceiveMaterialDetails(i).Lot
                            objNewProduct.Cost = ReceiveMaterialDetails(i).Cost

                            '// จำนวนคงคลังครั้งแรก
                            objNewProduct.TotalStockAmount = ReceiveMaterialDetails(i).Quantity
                            objNewProduct.AvailableAmount = ReceiveMaterialDetails(i).Quantity
                            objNewProduct.CollectAmount = ReceiveMaterialDetails(i).Quantity

                            '//ใส่สินค้าให้กับรายการรับ (ตอนแรกยังไม่มีสินค้า)
                            ReceiveMaterialDetails(i).MaterialProduct = objNewProduct
                            '// Insert Transaction Data
                            InsertTransaction(ReceiveMaterialDetails(i), ActionType.Approve)

                            objNewProduct.Save()
                        Else
                            '//ใส่สินค้าให้กับรายการรับ (ตอนแรกยังไม่มีสินค้า)
                            ReceiveMaterialDetails(i).MaterialProduct = objProduct

                            objProduct.Cost = ReceiveMaterialDetails(i).Cost

                            '// Update ยอดคงคลัง(บวกเพิ่ม) 
                            objProduct.TotalStockAmount += ReceiveMaterialDetails(i).Quantity
                            objProduct.AvailableAmount += ReceiveMaterialDetails(i).Quantity
                            objProduct.CollectAmount += ReceiveMaterialDetails(i).Quantity
                            '// Insert Transaction Data
                            InsertTransaction(ReceiveMaterialDetails(i), ActionType.Approve)
                        End If

                    Next
                    '//Session.CommitTransaction()
                End If

                Return True

            Catch ex As Exception
                Return False
            End Try
        End If
    End Function

    Public Function MarkAsCancel() As Boolean
        If Not IsDeleted Then
            Try
                Me.fStatus = PublicEnum.SimsStatus.Cancel
                '//สร้าง Product
                MyBase.Save()

                If ReceiveMaterialDetails.Count > 0 Then
                    Dim criteria As String = "Material=? and MaterialYear=? and MoneyType=? and Lot=?"
                    For i As Integer = 0 To ReceiveMaterialDetails.Count - 1

                        Dim _Material As Material = ReceiveMaterialDetails(i).Material
                        Dim _MaterialYear As String = ReceiveMaterialDetails(i).MaterialYear
                        Dim _MoneyType As MoneyType = ReceiveMaterialDetails(i).MoneyType
                        Dim _Lot As Integer = ReceiveMaterialDetails(i).Lot

                        Dim objProduct As MaterialProduct = Session.FindObject(Of MaterialProduct) _
                                                        (CriteriaOperator.Parse(criteria, _Material,
                                                                                            _MaterialYear, _
                                                                                            _MoneyType, _
                                                                                            _Lot))
                        If Not objProduct Is Nothing Then
                            objProduct.TotalStockAmount -= ReceiveMaterialDetails(i).Quantity
                            objProduct.AvailableAmount -= ReceiveMaterialDetails(i).Quantity
                            objProduct.CollectAmount -= ReceiveMaterialDetails(i).Quantity
                            'objProduct.Cost = ReceiveMaterialDetails(i).Cost
                            objProduct.Save()
                            '// ลบข้อมูล Transaction ของ Product
                            InsertTransaction(ReceiveMaterialDetails(i), ActionType.Cancel)
                            Dim objToCancel As MaterialTransaction = Session.FindObject(Of MaterialTransaction)(CriteriaOperator.Parse("TransactionType='Recieve' and RefNo=?", ReceiveNo))
                            If objToCancel IsNot Nothing Then
                                objToCancel.IsDelete = True
                            End If
                        End If
                    Next
                    '//Session.CommitTransaction()
                End If

                Return True

            Catch ex As Exception
                Return False
            End Try
        End If
    End Function

    Private Sub InsertTransaction(objReceiveMaterialDetail As ReceiveMaterialDetail, actionType As ActionType)
        Dim objTrans As New MaterialTransaction(Session)
        If actionType = ReceiveSeed.ActionType.Approve Then
            objTrans.TransactionType = PublicEnum.MaterialTransactionType.Recieve
            objTrans.Income = objReceiveMaterialDetail.Quantity
            objTrans.Outcome = 0
        Else
            objTrans.TransactionType = PublicEnum.MaterialTransactionType.CancelRecieve
            objTrans.Income = 0
            objTrans.Outcome = objReceiveMaterialDetail.Quantity
            objTrans.IsDelete = True
        End If

        objTrans.RefNo = fReceiveNo
        objTrans.MaterialProduct = objReceiveMaterialDetail.MaterialProduct
        objTrans.ProductName = objReceiveMaterialDetail.Material.MaterialName
        objTrans.ProductCode = objReceiveMaterialDetail.MaterialProduct.ProductCode

        ' objTrans.CollectQuantity = objReceiveDetail.Quantity
        objTrans.TransactionDate = Date.Now
        objTrans.TransactionBy = CType(SecuritySystem.CurrentUser, User).DisplayName
        objTrans.Save()
    End Sub

    Private Sub DeleteTransaction(refNo As String)
        Dim criteria As String = "TransactionType=? and RefNo=?"
        Dim objTrans As SeedTransaction = Session.FindObject(Of SeedTransaction)(CriteriaOperator.Parse(criteria, PublicEnum.SeedTransactionType.Recieve, refNo))
        If objTrans IsNot Nothing Then
            objTrans.Delete()
        End If
    End Sub

    Public Sub SaveToReferenceLot(objPick As PickSeedDetail, objReceive As ReceiveSeedDetail)
        Dim objRefLot As New ReferenceLotNo(Session)
        objRefLot.Pick = objPick
        objRefLot.Receive = objReceive
        objRefLot.Save()
    End Sub

    Public Sub DeleteReferenceLot(objPick As PickSeedDetail, objReceive As ReceiveSeedDetail)
        Dim objRefLot As ReferenceLotNo = Session.FindObject(Of ReferenceLotNo)(CriteriaOperator.Parse("Pick=? and Receive=?", objPick, objReceive))
        If objRefLot IsNot Nothing Then
            objRefLot.Delete()
        End If
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
