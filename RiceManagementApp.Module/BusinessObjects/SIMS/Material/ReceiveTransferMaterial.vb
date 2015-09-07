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
<XafDisplayName("รับโอน วัสดุการผลิต")> _
<RuleCriteria("ReceiveTransferMaterial.NotDelete", DefaultContexts.Delete, "ReceiveStatus='Pending'", CustomMessageTemplate:="ไม่สามารถลบข้อมูลนี้ได้")> _
<ConditionalAppearance.Appearance("ReceiveTransferMaterialDisableAllItems", criteria:="ReceiveStatus!='Pending'", Enabled:=False, TargetItems:="*", Context:="DetailView")> _
Public Class ReceiveTransferMaterial
    Inherits BaseObject
    Implements IApproveAble

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        Me.TransferDate = Date.Today
        Me.TransferType = PublicEnum.TransferType.Recieve
        Me.ReceiveStatus = PublicEnum.SimsStatus.Pending
        DataOwner = GetCurrentSite()

        Dim collSiteSetting As New XPCollection(Of SiteSetting)(Session)
        If collSiteSetting.Count > 0 Then
            Dim objSite As Site = Session.FindObject(Of Site)(CriteriaOperator.Parse("SiteNo=?", collSiteSetting(0).SiteNo))
            If objSite IsNot Nothing Then
                Me.TransferTo = objSite
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
    <XafDisplayName("เลขที่รับโอน")> _
    <PersistentAlias("fTransferNo")> _
    Public ReadOnly Property TransferNo() As String
        Get
            Return fTransferNo
        End Get
    End Property

    Dim fSendNo As String
    <XafDisplayName("เลขที่จ่ายโอน")> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property SendNo() As String
        Get
            Return fSendNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue("fSendNo", fSendNo, value)
        End Set
    End Property

    Dim fTransferType As PublicEnum.TransferType
    <XafDisplayName("ประเภท")> _
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
    <XafDisplayName("โอนจาก")> _
    <DataSourceCriteria("[SiteType]='Factory'")> _
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
    <XafDisplayName("โอนไปยัง")> _
    <DataSourceCriteria("[SiteType]='Factory'")> _
    Public Property TransferTo() As Site
        Get
            Return fTransferTo
        End Get
        Set(ByVal value As Site)
            SetPropertyValue(Of Site)("TransferTo", fTransferTo, value)
        End Set
    End Property
    Dim fTransferDate As DateTime
    <RuleRequiredField(DefaultContexts.Save)> _
     <XafDisplayName("วันที่จ่ายโอน")> _
    Public Property TransferDate() As DateTime
        Get
            Return fTransferDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("TransferDate", fTransferDate, value)
        End Set
    End Property
    Dim fReceiveDate As DateTime
    <RuleRequiredField(DefaultContexts.Save)> _
     <XafDisplayName("วันที่รับโอน")> _
    Public Property ReceiveDate() As DateTime
        Get
            Return fReceiveDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ReceiveDate", fReceiveDate, value)
        End Set
    End Property
    Dim fTransferUser As String
    <XafDisplayName("ผู้จ่ายโอน")> _
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
    <XafDisplayName("ตำแหน่งผู้จ่ายโอน")> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property TransferPosition() As String
        Get
            Return fTransferPosition
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("TransferPosition", fTransferPosition, value)
        End Set
    End Property
    Dim fReceiveUser As String
    <XafDisplayName("ผู้รับโอน")> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property ReceiveUser() As String
        Get
            Return fReceiveUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ReceiveUser", fReceiveUser, value)
        End Set
    End Property
    Dim fReceivePostion As String
    <XafDisplayName("ตำแหน่งผู้รับโอน")> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property ReceivePostion() As String
        Get
            Return fReceivePostion
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ReceivePostion", fReceivePostion, value)
        End Set
    End Property
    Dim fApproveUser As String
    <RuleRequiredField(TargetContextIDs:="Approval")> _
    <XafDisplayName("ผู้อนุมัติ")> _
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
    <XafDisplayName("ตำแหน่งผู้อนุมัติ")> _
    Public Property ApprovePosition() As String
        Get
            Return fApprovePosition
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ApprovePosition", fApprovePosition, value)
        End Set
    End Property

    Dim fReceiveStatus As PublicEnum.SimsStatus
    <XafDisplayName("สถานะการรับโอน")> _
    Public Property ReceiveStatus() As PublicEnum.SimsStatus
        Get
            Return fReceiveStatus
        End Get
        Set(ByVal value As PublicEnum.SimsStatus)
            SetPropertyValue(Of PublicEnum.SimsStatus)("ReceiveStatus", fReceiveStatus, value)
        End Set
    End Property

    <Browsable(False)> _
    Public ReadOnly Property Status As PublicEnum.SimsStatus
        Get
            Return ReceiveStatus
        End Get
    End Property

    Dim fRemark As String
    <XafDisplayName("หมายเหตุ")> _
    <Size(200)> _
    Public Property Remark() As String
        Get
            Return fRemark
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Remark", fRemark, value)
        End Set
    End Property
    <XafDisplayName("รายการรับโอนวัสดุการผลิต")> _
    <Association("ReceiveTransferMaterial-ReceiveTransferMaterialDetails")> _
    Public ReadOnly Property ReceiveTransferMaterialDetails() As XPCollection(Of ReceiveTransferMaterialDetail)
        Get
            Return GetCollection(Of ReceiveTransferMaterialDetail)("ReceiveTransferMaterialDetails")
        End Get
    End Property

    Public Function MarkAsComplete() As Boolean
        If Not IsDeleted Then
            If Not ReceiveTransferMaterialDetails.Count > 0 Then
                MsgBox("ไม่พบรายการวัสดุการผลิต กรุณาตรวจสอบข้อมูลอีกครั้ง", MsgBoxStyle.Information, "")
                Exit Function
            End If
            Dim _session As Session = Me.Session
            Try
                Me.ReceiveStatus = PublicEnum.SimsStatus.Approve
                '//สร้าง Product
                MyBase.Save()

                If ReceiveTransferMaterialDetails.Count > 0 Then
                    Dim criteria As String = "Material=? and MaterialYear=? and MoneyType=? and Lot=?"
                    For i As Integer = 0 To ReceiveTransferMaterialDetails.Count - 1
                        Dim _Material As Material = ReceiveTransferMaterialDetails(i).Material
                        Dim _MaterialYear As String = ReceiveTransferMaterialDetails(i).MaterialYear
                        Dim _MoneyType As MoneyType = ReceiveTransferMaterialDetails(i).MoneyType
                        Dim _Lot As String = ReceiveTransferMaterialDetails(i).Lot

                        Dim objProduct As MaterialProduct = Session.FindObject(Of MaterialProduct) _
                                                        (CriteriaOperator.Parse(criteria, _Material,
                                                                                            _MaterialYear, _
                                                                                            _MoneyType, _
                                                                                            _Lot))
                        If objProduct Is Nothing Then
                            Dim objNewProduct As New MaterialProduct(Session)
                            Dim _MaterialId As String = ReceiveTransferMaterialDetails(i).Material.MaterialCode
                            Dim _Year As String = ReceiveTransferMaterialDetails(i).MaterialYear
                            Dim _MoneyTypeId As String = ReceiveTransferMaterialDetails(i).MoneyType.MoneyTypeId
                            Dim _lotNo As String = ReceiveTransferMaterialDetails(i).Lot
                            ' 5-01-2557-1
                            objNewProduct.ProductCode = String.Format("5-{0}-{1}-{2}-{3}", _
                                                                      _MaterialId.ToString.PadLeft(2, "0"), _
                                                                      _Year, _
                                                                      _MoneyTypeId,
                                                                      _lotNo.ToString.PadLeft(3, "0"))
                            objNewProduct.MaterialYear = ReceiveTransferMaterialDetails(i).MaterialYear
                            objNewProduct.MoneyType = ReceiveTransferMaterialDetails(i).MoneyType
                            objNewProduct.Material = ReceiveTransferMaterialDetails(i).Material
                            objNewProduct.Lot = ReceiveTransferMaterialDetails(i).Lot
                            objNewProduct.Cost = ReceiveTransferMaterialDetails(i).Cost

                            '// จำนวนคงคลังครั้งแรก
                            objNewProduct.TotalStockAmount = ReceiveTransferMaterialDetails(i).Quantity
                            objNewProduct.AvailableAmount = ReceiveTransferMaterialDetails(i).Quantity
                            objNewProduct.CollectAmount = ReceiveTransferMaterialDetails(i).Quantity

                            '//ใส่สินค้าให้กับรายการรับ (ตอนแรกยังไม่มีสินค้า)
                            ReceiveTransferMaterialDetails(i).MaterialProduct = objNewProduct
                            '// Insert Transaction Data
                            InsertTransaction(_session, ReceiveTransferMaterialDetails(i), ActionType.Approve)

                            objNewProduct.Save()
                        Else
                            '//ใส่สินค้าให้กับรายการรับ (ตอนแรกยังไม่มีสินค้า)
                            ReceiveTransferMaterialDetails(i).MaterialProduct = objProduct

                            objProduct.Cost = ReceiveTransferMaterialDetails(i).Cost

                            '// Update ยอดคงคลัง(บวกเพิ่ม) 
                            objProduct.TotalStockAmount += ReceiveTransferMaterialDetails(i).Quantity
                            objProduct.AvailableAmount += ReceiveTransferMaterialDetails(i).Quantity
                            objProduct.CollectAmount += ReceiveTransferMaterialDetails(i).Quantity
                            '// Insert Transaction Data
                            InsertTransaction(_session, ReceiveTransferMaterialDetails(i), ActionType.Approve)
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
            Dim _session As Session = Me.Session
            Try
                Me.ReceiveStatus = PublicEnum.SimsStatus.Cancel
                '//สร้าง Product
                MyBase.Save()

                If ReceiveTransferMaterialDetails.Count > 0 Then
                    Dim criteria As String = "Material=? and MaterialYear=? and MoneyType=? and Lot=?"
                    For i As Integer = 0 To ReceiveTransferMaterialDetails.Count - 1

                        Dim _Material As Material = ReceiveTransferMaterialDetails(i).Material
                        Dim _MaterialYear As String = ReceiveTransferMaterialDetails(i).MaterialYear
                        Dim _MoneyType As MoneyType = ReceiveTransferMaterialDetails(i).MoneyType
                        Dim _Lot As Integer = ReceiveTransferMaterialDetails(i).Lot

                        Dim objProduct As MaterialProduct = Session.FindObject(Of MaterialProduct) _
                                                        (CriteriaOperator.Parse(criteria, _Material,
                                                                                            _MaterialYear, _
                                                                                            _MoneyType, _
                                                                                            _Lot))
                        If Not objProduct Is Nothing Then
                            objProduct.TotalStockAmount -= ReceiveTransferMaterialDetails(i).Quantity
                            objProduct.AvailableAmount -= ReceiveTransferMaterialDetails(i).Quantity
                            objProduct.CollectAmount -= ReceiveTransferMaterialDetails(i).Quantity
                            'objProduct.Cost = ReceiveMaterialDetails(i).Cost
                            objProduct.Save()
                            '// ลบข้อมูล Transaction ของ Product
                            InsertTransaction(_session, ReceiveTransferMaterialDetails(i), ActionType.Cancel)
                            Dim objToCancel As MaterialTransaction = Session.FindObject(Of MaterialTransaction)(CriteriaOperator.Parse("TransactionType='TransferIn' and RefNo=?", TransferNo))
                            If objToCancel IsNot Nothing Then
                                objToCancel.IsDelete = True
                            End If
                        End If
                    Next
                    '//Session.CommitTransaction()
                End If

                Return False

            Catch ex As Exception
                Return True
            End Try
        End If
    End Function

    Private Sub InsertTransaction(_session As Session, objItem As ReceiveTransferMaterialDetail, actionType As ActionType)
        Dim objTrans As New MaterialTransaction(_session)
        If actionType = TransferMaterial.ActionType.Approve Then
            objTrans.TransactionType = PublicEnum.MaterialTransactionType.TransferIn
            objTrans.Income = objItem.Quantity
            objTrans.Outcome = 0
        Else
            objTrans.TransactionType = PublicEnum.MaterialTransactionType.CancelTransferIn
            objTrans.Income = 0
            objTrans.Outcome = objItem.Quantity
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
