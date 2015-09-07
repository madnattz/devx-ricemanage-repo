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

<ConditionalAppearance.Appearance("DisableAllItems", criteria:="Status!='Pending'", Enabled:=False, TargetItems:="*", Context:="DetailView")> _
<DefaultClassOptions()> _
<RuleCriteria("ReceiveSeed.NotDelete", DefaultContexts.Delete, "Status='Pending'", CustomMessageTemplate:="ไม่สามารถลบข้อมูลนี้ได้")> _
Public Class ReceiveSeed
    Inherits BaseObject
    Implements IApproveAble

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        Me.ReceiveDate = Date.Now
        Me.fReceiveReason = PublicEnum.ReceiveForm.ProcessSeed
        Me.IsSubmitToCenter = False
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
    Dim fReceiveReason As PublicEnum.ReceiveForm ', Context:="DetailView"
    <ConditionalAppearance.Appearance("ReceiveReasonCrt", criteria:="IsOtherReceiveType=True", Enabled:=False)> _
    <RuleRequiredField(DefaultContexts.Save, TargetCriteria:="IsOtherReceiveType=False")> _
    Public Property ReceiveReason() As PublicEnum.ReceiveForm
        Get
            Return fReceiveReason
        End Get
        Set(ByVal value As PublicEnum.ReceiveForm)
            SetPropertyValue(Of PublicEnum.ReceiveForm)("ReceiveReason", fReceiveReason, value)
        End Set
    End Property
    Dim fIsOtherReceiveType As Boolean
    <ImmediatePostData()> _
    Public Property IsOtherReceiveType() As Boolean
        Get
            Return fIsOtherReceiveType
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("IsOtherReceiveType", fIsOtherReceiveType, value)
            If Not IsLoading AndAlso Not IsSaving Then
                If value = True Then
                    ReceiveReason = Nothing
                Else
                    ReceiveTypeOther = ""
                    ReceiveReason = PublicEnum.ReceiveForm.ProcessSeed
                End If
            End If
        End Set
    End Property
    Dim fReceiveTypeOther As String ', Context:="DetailView"
    <ConditionalAppearance.Appearance("otherSourceCrt", criteria:="IsOtherReceiveType=False", Enabled:=False)> _
    <RuleRequiredField(DefaultContexts.Save, TargetCriteria:="IsOtherReceiveType=True")> _
    Public Property ReceiveTypeOther() As String
        Get
            Return fReceiveTypeOther
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ReceiveTypeOther", fReceiveTypeOther, value)
        End Set
    End Property

    Public ReadOnly Property ReceiveTypeText() As Object
        Get
            Dim ret_value As New Object
            Try
                If IsOtherReceiveType = False Then
                    ret_value = fReceiveReason
                Else
                    ret_value = fReceiveTypeOther
                End If
            Catch ex As Exception
            End Try
            Return ret_value
        End Get
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
    Dim fStatus As PublicEnum.SimsStatus
    Public Property Status() As PublicEnum.SimsStatus
        Get
            Return fStatus
        End Get
        Set(ByVal value As PublicEnum.SimsStatus)
            SetPropertyValue(Of PublicEnum.SimsStatus)("Status", fStatus, value)
        End Set
    End Property
    Dim fRemark As String
    <Size(10)> _
    Public Property Remark() As String
        Get
            Return fRemark
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Remark", fRemark, value)
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

    <Association("ReceiveSeedDetailReferencesReceiveSeed", GetType(ReceiveSeedDetail))> _
    Public ReadOnly Property ReceiveSeedDetails() As XPCollection(Of ReceiveSeedDetail)
        Get
            Return GetCollection(Of ReceiveSeedDetail)("ReceiveSeedDetails")
        End Get
    End Property

    '<Action(Caption:="อนุมัติ", ConfirmationMessage:="ท่านต้องการบันทึกข้อมูล ใช่หรือไม่?", ImageName:="Action_Grant", AutoCommit:=True, TargetObjectsCriteria:="Status='Pending'")> _
    Public Function MarkAsComplete() As Boolean
        If Not IsDeleted Then
            If Not ReceiveSeedDetails.Count > 0 Then
                MsgBox("ไม่พบรายการเมล็ดพันธุ์ กรุณาตรวจสอบข้อมูลอีกครั้ง", MsgBoxStyle.Information, "")
                Exit Function
            End If

            'Dim _session As Session = Me.Session
            Try
                Me.Status = PublicEnum.SimsStatus.Approve
                '//สร้าง Product
                '//MyBase.Save()
                If ReceiveSeedDetails.Count > 0 Then
                    Dim criteria As String = "SeedStatus=? and SeedType= ? and SeedClass=? and Season=? and SeedYear=? and LotNo=? and MoneyType=?"
                    For i As Integer = 0 To ReceiveSeedDetails.Count - 1
                        Dim _SeedType As SeedType = ReceiveSeedDetails(i).SeedType
                        Dim _SeedStatus As SeedStatus = ReceiveSeedDetails(i).SeedStatus
                        Dim _SeedClass As SeedClass = ReceiveSeedDetails(i).SeedClass
                        Dim _Season As Season = ReceiveSeedDetails(i).Season
                        Dim _SeedYear As String = ReceiveSeedDetails(i).SeedYear
                        Dim _SeedLot As Integer = ReceiveSeedDetails(i).LotNo
                        Dim _MoneyType As MoneyType = ReceiveSeedDetails(i).MoneyType

                        Dim objProduct As SeedProduct = Session.FindObject(Of SeedProduct) _
                                                        (CriteriaOperator.Parse(criteria, _SeedStatus,
                                                                                            _SeedType, _
                                                                                            _SeedClass, _
                                                                                            _Season, _
                                                                                            _SeedYear, _
                                                                                            _SeedLot, _
                                                                                            _MoneyType))
                        If objProduct Is Nothing Then
                            Dim objNewProduct As New SeedProduct(Session)

                            Dim _PlantID As String = ReceiveSeedDetails(i).Plant.PlantID
                            Dim _SeedStatusID As String = ReceiveSeedDetails(i).SeedStatus.SeedStatusID.ToString
                            Dim _SeedTypeID As String = String.Format("{0:D2}", Convert.ToInt32(ReceiveSeedDetails(i).SeedType.SeedID))
                            Dim _ClassID As String = ReceiveSeedDetails(i).SeedClass.ClassID
                            Dim _SeasonID As String = ReceiveSeedDetails(i).Season.SeasonID
                            Dim _Year As String = ReceiveSeedDetails(i).SeedYear
                            Dim _MoneyTypeId As String = ReceiveSeedDetails(i).MoneyType.MoneyTypeId
                            Dim _LotNo As String = ReceiveSeedDetails(i).LotNo.ToString("D3")

                            '1-62-2-1-2556-1-001
                            objNewProduct.ProductCode = String.Format("{0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", _
                                                                      _PlantID, _
                                                                      _SeedStatusID, _
                                                                      _SeedTypeID, _
                                                                      _ClassID, _
                                                                      _SeasonID, _
                                                                      _Year, _
                                                                      _MoneyTypeId, _
                                                                      _LotNo)
                            objNewProduct.ProductName = ReceiveSeedDetails(i).Plant.PlantName & "-" & _
                                                        ReceiveSeedDetails(i).SeedStatus.SeedStatusName & "-" & _
                                                        ReceiveSeedDetails(i).SeedType.SeedName & "-" & _
                                                        ReceiveSeedDetails(i).SeedClass.ClassName & "-" & _
                                                        ReceiveSeedDetails(i).Season.SeasonName & "-" & _
                                                        ReceiveSeedDetails(i).SeedYear & "-" & _
                                                        ReceiveSeedDetails(i).MoneyType.MoneyTypeName & "-" & _
                                                        ReceiveSeedDetails(i).LotNo.ToString("D3")
                            objNewProduct.Plant = ReceiveSeedDetails(i).Plant
                            objNewProduct.SeedStatus = ReceiveSeedDetails(i).SeedStatus
                            objNewProduct.SeedType = ReceiveSeedDetails(i).SeedType
                            objNewProduct.SeedClass = ReceiveSeedDetails(i).SeedClass
                            objNewProduct.Season = ReceiveSeedDetails(i).Season
                            objNewProduct.SeedYear = ReceiveSeedDetails(i).SeedYear
                            objNewProduct.MoneyType = ReceiveSeedDetails(i).MoneyType
                            objNewProduct.LotNo = ReceiveSeedDetails(i).LotNo

                            objNewProduct.IsMix = ReceiveSeedDetails(i).IsMixChemical
                            objNewProduct.IsFinish = ReceiveSeedDetails(i).IsFinishProcess

                            '// จำนวนคงคลังครั้งแรก
                            objNewProduct.TotalStockAmount = ReceiveSeedDetails(i).Quantity
                            objNewProduct.TotalBags = ReceiveSeedDetails(i).Bags

                            objNewProduct.AvailableAmount = ReceiveSeedDetails(i).Quantity
                            objNewProduct.AvailableBags = ReceiveSeedDetails(i).Bags

                            objNewProduct.CollectAmount = ReceiveSeedDetails(i).Quantity
                            objNewProduct.CollectBags = ReceiveSeedDetails(i).Bags

                            '//ใส่สินค้าให้กับรายการรับ (ตอนแรกยังไม่มีสินค้า)
                            ReceiveSeedDetails(i).SeedProduct = objNewProduct

                            '// Insert Transaction Data
                            '//InsertTransaction(ReceiveSeedDetails(i), ActionType.Approve)
                            '//TransactionControl.InsertSeedTransaction(_session, PublicEnum.SeedTransactionType.Recieve, fRefNo, ReceiveSeedDetails(i).SeedProduct, ReceiveSeedDetails(i).Quantity, TransactionControl.StockType.Income)
                            Dim objTransactionLog As New SeedTransaction(Session, PublicEnum.SeedTransactionType.Recieve, ReceiveNo, ReceiveSeedDetails(i).SeedProduct, ReceiveSeedDetails(i).Quantity, ReceiveSeedDetails(i).Bags)

                            objNewProduct.Save()
                        Else
                            '//ใส่สินค้าให้กับรายการรับ (ตอนแรกยังไม่มีสินค้า)
                            ReceiveSeedDetails(i).SeedProduct = objProduct

                            '// Update ยอดคงคลัง(บวกเพิ่ม) 
                            objProduct.TotalStockAmount += ReceiveSeedDetails(i).Quantity
                            objProduct.TotalBags += ReceiveSeedDetails(i).Bags

                            objProduct.AvailableAmount += ReceiveSeedDetails(i).Quantity
                            objProduct.AvailableBags += ReceiveSeedDetails(i).Bags

                            objProduct.CollectAmount += ReceiveSeedDetails(i).Quantity
                            objProduct.CollectBags += ReceiveSeedDetails(i).Bags
                            '// Insert Transaction Data
                            '//InsertTransaction(ReceiveSeedDetails(i), ActionType.Approve)
                            Dim objTransactionLog As New SeedTransaction(Session, PublicEnum.SeedTransactionType.Recieve, ReceiveNo, ReceiveSeedDetails(i).SeedProduct, ReceiveSeedDetails(i).Quantity, ReceiveSeedDetails(i).Bags)

                        End If

                        '//เพิ่มข้อมูลสำหรับการอ้างอิง Lot
                        If ReceiveSeedDetails(i).SeedStatus.SeedStatusName.Contains("ดี") Then
                            SaveToReferenceLot(ReceiveSeedDetails(i).RefLot, ReceiveSeedDetails(i))
                        End If

                        '//เปลี่ยนสถานะการรับเข้าคลังของการจัดล็อตเมล็ดพันธุ์ของโรงงาน
                        If ReceiveSeedDetails(i).FactoryLot IsNot Nothing Then
                            ReceiveSeedDetails(i).FactoryLot.ChangeIsReceiveToInventory(True)
                        End If

                    Next
                    '//Session.CommitTransaction()
                End If

                MyBase.Save()
                Return True
                'Session.CommitTransaction()

            Catch ex As Exception
                Me.fStatus = PublicEnum.SimsStatus.Pending
                Return False
                'Session.RollbackTransaction()
            End Try
        End If
    End Function

    '<Action(Caption:="ยกเลิก", ConfirmationMessage:="ท่านต้องการบันทึกข้อมูล ใช่หรือไม่?", ImageName:="Action_Deny", AutoCommit:=True, TargetObjectsCriteria:="Status='Approve'")> _
    Public Function MarkAsCancel() As Boolean
        If Not IsDeleted Then

            Dim _session As Session = Me.Session

            Try
                Me.Status = PublicEnum.SimsStatus.Cancel
                Me.IsSubmitToCenter = False
                '//สร้าง Product
                '//MyBase.Save()

                If ReceiveSeedDetails.Count > 0 Then
                    Dim criteria As String = "SeedStatus=? and SeedType= ? and SeedClass=? and Season=? and SeedYear=? and LotNo=? and MoneyType=?"
                    For i As Integer = 0 To ReceiveSeedDetails.Count - 1
                        Dim _SeedStatus As SeedStatus = ReceiveSeedDetails(i).SeedStatus
                        Dim _SeedType As SeedType = ReceiveSeedDetails(i).SeedType
                        Dim _SeedClass As SeedClass = ReceiveSeedDetails(i).SeedClass
                        Dim _Season As Season = ReceiveSeedDetails(i).Season
                        Dim _SeedYear As String = ReceiveSeedDetails(i).SeedYear
                        Dim _SeedLot As Integer = ReceiveSeedDetails(i).LotNo
                        Dim _MoneyType As MoneyType = ReceiveSeedDetails(i).MoneyType

                        Dim objProduct As SeedProduct = _session.FindObject(Of SeedProduct) _
                                                        (CriteriaOperator.Parse(criteria, _SeedStatus,
                                                                                            _SeedType, _
                                                                                            _SeedClass, _
                                                                                            _Season, _
                                                                                            _SeedYear, _
                                                                                            _SeedLot, _
                                                                                            _MoneyType))
                        If Not objProduct Is Nothing Then
                            objProduct.IsDelete = True
                            objProduct.TotalStockAmount -= ReceiveSeedDetails(i).Quantity
                            objProduct.AvailableAmount -= ReceiveSeedDetails(i).Quantity
                            objProduct.CollectAmount -= ReceiveSeedDetails(i).Quantity
                            objProduct.Save()
                            '//ลบข้อมูล Transaction ของ Product
                            '//InsertTransaction(ReceiveSeedDetails(i), ActionType.Cancel)
                            Dim objTransactionLog As New SeedTransaction(_session, PublicEnum.SeedTransactionType.CancelRecieve, ReceiveNo, ReceiveSeedDetails(i).SeedProduct, ReceiveSeedDetails(i).Quantity, ReceiveSeedDetails(i).Bags)
                            Dim objToCancel As SeedTransaction = Session.FindObject(Of SeedTransaction)(CriteriaOperator.Parse("TransactionType='Recieve' and RefNo=?", ReceiveNo))
                            If objToCancel IsNot Nothing Then
                                objToCancel.IsDelete = True
                            End If
                        End If
                        'DeleteTransaction(fReceiveNo)

                        '//ลบข้อมูล Reference Lot ที่ใช้อ้างอิงกัน
                        If ReceiveSeedDetails(i).SeedStatus.SeedStatusName.Contains("ดี") Then
                            DeleteReferenceLot(ReceiveSeedDetails(i).RefLot, ReceiveSeedDetails(i))
                        End If

                        '//เปลี่ยนสถานะการรับเข้าคลังของการจัดล็อตเมล็ดพันธุ์ของโรงงาน
                        If ReceiveSeedDetails(i).FactoryLot IsNot Nothing Then
                            ReceiveSeedDetails(i).FactoryLot.ChangeIsReceiveToInventory(False)
                            ReceiveSeedDetails(i).FactoryLot = Nothing
                        End If
                    Next
                    '//Session.CommitTransaction()
                End If

                MyBase.Save()
                '_session.CommitTransaction()
                Return True
            Catch ex As Exception
                Return False
                '_session.RollbackTransaction()
            End Try
        End If
    End Function

    'Private Sub InsertTransaction(objReceiveDetail As ReceiveSeedDetail, actionType As ActionType)
    '    Dim objTrans As New SeedTransaction(Session)
    '    If actionType = ReceiveSeed.ActionType.Approve Then
    '        objTrans.TransactionType = PublicEnum.SeedTransactionType.Recieve
    '        objTrans.Income = objReceiveDetail.Quantity
    '        objTrans.Outcome = 0
    '    Else
    '        objTrans.TransactionType = PublicEnum.SeedTransactionType.CancelRecieve
    '        objTrans.Income = 0
    '        objTrans.Outcome = objReceiveDetail.Quantity
    '    End If

    '    objTrans.RefNo = fReceiveNo
    '    objTrans.SeedProduct = objReceiveDetail.SeedProduct
    '    objTrans.ProductName = objReceiveDetail.SeedProduct.ProductName
    '    objTrans.ProductCode = objReceiveDetail.SeedProduct.ProductCode

    '    ' objTrans.CollectQuantity = objReceiveDetail.Quantity
    '    objTrans.TransactionDate = Date.Now
    '    objTrans.TransactionBy = CType(SecuritySystem.CurrentUser, User).DisplayName
    '    objTrans.Save()
    'End Sub

    'Private Sub DeleteTransaction(refNo As String)
    '    Dim criteria As String = "TransactionType=? and RefNo=?"
    '    Dim objTrans As SeedTransaction = Session.FindObject(Of SeedTransaction)(CriteriaOperator.Parse(criteria, PublicEnum.SeedTransactionType.Recieve, refNo))
    '    If objTrans IsNot Nothing Then
    '        objTrans.Delete()
    '    End If
    'End Sub

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
