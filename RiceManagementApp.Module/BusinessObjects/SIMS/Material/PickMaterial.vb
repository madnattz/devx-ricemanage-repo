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
<RuleCriteria("PickMaterial.NotDelete", DefaultContexts.Delete, "Status='Pending'", CustomMessageTemplate:="ไม่สามารถลบข้อมูลนี้ได้")> _
<ConditionalAppearance.Appearance("PickMaterialDisableAllItems", criteria:="Status!='Pending'", Enabled:=False, TargetItems:="*", Context:="DetailView")> _
Public Class PickMaterial
    Inherits BaseObject
    Implements IApproveAble

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        PickDate = Today
        PickType = PublicEnum.PickMaterialType.ProcessSeed
        FactoryNo = PublicEnum.EnumFactoryNo.Factory1
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
    Dim fPickType As PublicEnum.PickMaterialType
    <ImmediatePostData()> _
    Public Property PickType() As PublicEnum.PickMaterialType
        Get
            Return fPickType
        End Get
        Set(ByVal value As PublicEnum.PickMaterialType)
            SetPropertyValue(Of PublicEnum.PickMaterialType)("PickType", fPickType, value)
            If Not value = PublicEnum.PickMaterialType.ProcessSeed Then
                FactoryNo = Nothing
            End If
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
                    FactoryNo = Nothing
                Else
                    OtherPickType = Nothing
                    PickType = PublicEnum.PickMaterialType.ProcessSeed
                    FactoryNo = PublicEnum.EnumFactoryNo.Factory1
                End If
            End If
        End Set
    End Property
    Dim fOtherPickType As String
    <RuleRequiredField(DefaultContexts.Save, TargetCriteria:="IsOtherPickType=True")> _
    Public Property OtherPickType() As String
        Get
            Return fOtherPickType
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("OtherPickType", fOtherPickType, value)
        End Set
    End Property

    Public ReadOnly Property PickTypeAll As Object
        Get
            Dim ret_value As New Object
            Try
                If IsOtherPickType = False Then
                    ret_value = fPickType
                Else
                    ret_value = fOtherPickType
                End If
            Catch ex As Exception
            End Try
            Return ret_value
        End Get
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

    Dim fUseWithPlant As Plant
    Public Property UseWithPlant() As Plant
        Get
            Return fUseWithPlant
        End Get
        Set(ByVal value As Plant)
            SetPropertyValue(Of Plant)("UseWithPlant", fUseWithPlant, value)
        End Set
    End Property
    Dim fUseWithSeedType As SeedType
    <DataSourceProperty("UseWithPlant.SeedTypes")> _
    Public Property UseWithSeedType() As SeedType
        Get
            Return fUseWithSeedType
        End Get
        Set(ByVal value As SeedType)
            SetPropertyValue(Of SeedType)("UseWithSeedType", fUseWithSeedType, value)
        End Set
    End Property
    Dim fUseWithSeedClass As SeedClass
    Public Property UseWithSeedClass() As SeedClass
        Get
            Return fUseWithSeedClass
        End Get
        Set(ByVal value As SeedClass)
            SetPropertyValue(Of SeedClass)("UseWithSeedClass", fUseWithSeedClass, value)
        End Set
    End Property
    Dim fUseWithSeason As Season
    Public Property UseWithSeason() As Season
        Get
            Return fUseWithSeason
        End Get
        Set(ByVal value As Season)
            SetPropertyValue(Of Season)("UseWithSeason", fUseWithSeason, value)
        End Set
    End Property
    Dim fUseWithSeedYear As String
    <Size(4)> _
    Public Property UseWithSeedYear() As String
        Get
            Return fUseWithSeedYear
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("UseWithSeedYear", fUseWithSeedYear, value)
        End Set
    End Property

    Dim fUseWithSeedLot As String
    <XafDisplayName("รุ่นที่")> _
    Public Property UseWithSeedLot() As String
        Get
            Return fUseWithSeedLot
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("UseWithSeedLot", fUseWithSeedLot, value)
        End Set
    End Property

    <VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    Public ReadOnly Property UseWithSeedForReport As String
        Get
            If UseWithPlant IsNot Nothing And _
               UseWithSeedType IsNot Nothing And _
               UseWithSeedClass IsNot Nothing And _
               UseWithSeason IsNot Nothing And _
               UseWithSeedYear <> "" And _
               UseWithSeedLot <> "" Then
                Return UseWithPlant.PlantName & "/" & _
                        UseWithSeedType.SeedName & "/" & _
                        UseWithSeedClass.ClassName & "/" & _
                        UseWithSeason.SeasonName & "/" & _
                        UseWithSeedYear & "/" & _
                        "รุ่นที่ " & UseWithSeedLot
            Else
                Return "-"
            End If
        End Get
    End Property

    Dim fOrganizationSection As OrganizationSection
    <RuleRequiredField(DefaultContexts.Save, TargetCriteria:="IsOtherOrganization=False")> _
    Public Property OrganizationSection() As OrganizationSection
        Get
            Return fOrganizationSection
        End Get
        Set(ByVal value As OrganizationSection)
            SetPropertyValue(Of OrganizationSection)("OrganizationSection", fOrganizationSection, value)
        End Set
    End Property
    Dim fIsOtherOrganization As Boolean
    <ImmediatePostData()> _
    Public Property IsOtherOrganization() As Boolean
        Get
            Return fIsOtherOrganization
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("IsOtherOrganization", fIsOtherOrganization, value)
            If Not IsLoading AndAlso Not IsSaving Then
                If value = True Then
                    OrganizationSection = Nothing
                Else
                    OtherOrganizationSection = Nothing
                End If
            End If
        End Set
    End Property
    Dim fOtherOrganizationSection As String
    <Size(250)> _
    <RuleRequiredField(DefaultContexts.Save, TargetCriteria:="IsOtherOrganization=True")> _
    Public Property OtherOrganizationSection() As String
        Get
            Return fOtherOrganizationSection
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("OtherOrganizationSection", fOtherOrganizationSection, value)
        End Set
    End Property

    Public ReadOnly Property OrganizationSectionAll As Object
        Get
            Dim ret_value As New Object
            Try
                If IsOtherOrganization = False Then
                    ret_value = OrganizationSection
                Else
                    ret_value = fOtherOrganizationSection
                End If
            Catch ex As Exception
            End Try
            Return ret_value
        End Get
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
    Dim fPickUser As String
    <Size(250)> _
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
    <Size(250)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property PickPosition() As String
        Get
            Return fPickPosition
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PickPosition", fPickPosition, value)
        End Set
    End Property
    Dim fApproverUser As String
    <Size(250)> _
     <RuleRequiredField(TargetContextIDs:="Approval")> _
    Public Property ApproverUser() As String
        Get
            Return fApproverUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ApproverUser", fApproverUser, value)
        End Set
    End Property
    Dim fApproverPosition As String
    <Size(250)> _
     <RuleRequiredField(TargetContextIDs:="Approval")> _
    Public Property ApproverPosition() As String
        Get
            Return fApproverPosition
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ApproverPosition", fApproverPosition, value)
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

    <Association("PickMaterialDetailReferencesPickMaterial", GetType(PickMaterialDetail))> _
    Public ReadOnly Property PickMaterialDetails() As XPCollection(Of PickMaterialDetail)
        Get
            Return GetCollection(Of PickMaterialDetail)("PickMaterialDetails")
        End Get
    End Property

    Dim fFactoryPickMaterial As FactoryPickMaterial
    <Browsable(False)> _
    Public Property FactoryPickMaterial() As FactoryPickMaterial
        Get
            Return fFactoryPickMaterial
        End Get
        Set(ByVal value As FactoryPickMaterial)
            SetPropertyValue(Of FactoryPickMaterial)("FactoryPickMaterial", fFactoryPickMaterial, value)
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

    Public Function MarkAsComplete() As Boolean
        If Not IsDeleted Then
            If Not PickMaterialDetails.Count > 0 Then
                MsgBox("ไม่พบรายการวัสดุการผลิต กรุณาตรวจสอบข้อมูลอีกครั้ง", MsgBoxStyle.Information, "")
                Exit Function
            End If
            Try
                Me.fStatus = PublicEnum.SimsStatus.Approve
                '//กรณีจ่ายแบบไม่เบิกจากโรงงาน fIsDataFromFactory = False
                If fIsDataFromFactory = False Then
                    '//สร้างใบเบิกให้โรงงานอัตโนมัติ เพื่อเก็บประวัติการเบิกในระบบ
                    If fPickType = PublicEnum.PickFor.ProcessSeed Then
                        CreateNewRequestForm(Session, Me)
                    End If
                Else
                    Me.FactoryPickMaterial.InventoryStatus = PublicEnum.SimsStatus.Approve
                End If

                For i As Integer = 0 To PickMaterialDetails.Count - 1
                    If PickMaterialDetails(i).MaterialProduct IsNot Nothing Then
                        '// Update ตัดยอดคงคลัง
                        PickMaterialDetails(i).MaterialProduct.UpdateStockAmount(SeedProduct.UpdateStockType.Outcome, PickMaterialDetails(i).Quantity)
                        '// รายการเคลื่อนไหวคลัง
                        InsertTransaction(PickMaterialDetails(i), ActionType.Approve)

                        If fPickType = PublicEnum.PickMaterialType.ProcessSeed Then
                            '// รายการเคลื่อนไหวโรงงงาน
                            InsertMaterialTransaction(Session, PickMaterialDetails(i), ActionType.Approve)
                            '//Update ยอดคลังของโรงงาน
                            UpdateFactoryMaterialInventory(PickMaterialDetails(i).MaterialProduct, Me.FactoryNo, PickMaterialDetails(i).Quantity, ActionType.Approve)

                        End If
                    End If
                Next

                MyBase.Save()
                Session.CommitTransaction()
                Return True
            Catch ex As Exception
                Session.RollbackTransaction()
                Return False
            End Try
        End If
    End Function

    Public Function MarkAsCancel() As Boolean
        If Not IsDeleted Then
            Try
                Me.Status = PublicEnum.SimsStatus.Cancel
                '//กรณีจ่ายแบบไม่เบิกจากโรงงาน fIsDataFromFactory = False
                If IsDataFromFactory = True Then
                    If PickType = PublicEnum.PickFor.ProcessSeed Then
                        Me.FactoryPickMaterial.InventoryStatus = PublicEnum.SimsStatus.Cancel
                    End If
                End If

                For i As Integer = 0 To PickMaterialDetails.Count - 1
                    If PickMaterialDetails(i).MaterialProduct IsNot Nothing Then
                        '// Update เพิ่มยอดคงคลัง
                        PickMaterialDetails(i).MaterialProduct.UpdateStockAmount(SeedProduct.UpdateStockType.Income, PickMaterialDetails(i).Quantity)
                        '// รายการเคลื่อนไหวคลัง
                        InsertTransaction(PickMaterialDetails(i), ActionType.Cancel)
                        Dim objToCancel As MaterialTransaction = Session.FindObject(Of MaterialTransaction)(CriteriaOperator.Parse("TransactionType='Pick' and RefNo=?", PickNo))
                        If objToCancel IsNot Nothing Then
                            objToCancel.IsDelete = True
                        End If

                        If PickType = PublicEnum.PickFor.ProcessSeed Then
                            '// รายการเคลื่อนไหวโรงงงาน
                            InsertMaterialTransaction(Session, PickMaterialDetails(i), ActionType.Cancel)
                            '//Update ตัดคืนยอดคลังของโรงงาน
                            UpdateFactoryMaterialInventory(PickMaterialDetails(i).MaterialProduct, Me.FactoryNo, PickMaterialDetails(i).Quantity, ActionType.Approve)
                        End If
                    End If
                Next

                Session.CommitTransaction()
                Return True
            Catch ex As Exception
                Session.RollbackTransaction()
                Return True
            End Try
        End If
    End Function

    Private Sub InsertTransaction(objPickMaterialDetail As PickMaterialDetail, actionType As ActionType)
        Dim objTrans As New MaterialTransaction(Session)
        If actionType = PickMaterial.ActionType.Approve Then
            objTrans.TransactionType = PublicEnum.MaterialTransactionType.Pick
            objTrans.Income = 0
            objTrans.Outcome = objPickMaterialDetail.Quantity
        Else
            objTrans.TransactionType = PublicEnum.MaterialTransactionType.CancelPick
            objTrans.Income = objPickMaterialDetail.Quantity
            objTrans.Outcome = 0
            objTrans.IsDelete = True
        End If

        objTrans.RefNo = fPickNo
        objTrans.MaterialProduct = objPickMaterialDetail.MaterialProduct
        objTrans.ProductName = objPickMaterialDetail.MaterialProduct.Material.MaterialName
        objTrans.ProductCode = objPickMaterialDetail.MaterialProduct.ProductCode

        ' objTrans.CollectQuantity = objReceiveDetail.Quantity
        objTrans.TransactionDate = Date.Now
        objTrans.TransactionBy = CType(SecuritySystem.CurrentUser, User).DisplayName
        objTrans.Save()

    End Sub

    Private Sub InsertMaterialTransaction(_session As Session, _objPickDetail As PickMaterialDetail, _actionType As ActionType)
        Dim objFacTrans As New FactoryMaterialTransaction(_session)
        If _actionType = PickMaterial.ActionType.Approve Then
            objFacTrans.TransactionType = PublicEnum.FactoryTransactionType.Pick
            objFacTrans.Income = _objPickDetail.Quantity
            objFacTrans.Outcome = 0
        Else
            objFacTrans.TransactionType = PublicEnum.FactoryTransactionType.CancelPick
            objFacTrans.Income = 0
            objFacTrans.Outcome = _objPickDetail.Quantity
        End If

        objFacTrans.RefNo = _objPickDetail.PickMaterial.PickNo
        objFacTrans.MaterialProduct = _objPickDetail.MaterialProduct
        objFacTrans.ProductName = _objPickDetail.MaterialProduct.Material.MaterialName
        objFacTrans.ProductCode = _objPickDetail.MaterialProduct.ProductCode
        objFacTrans.FactoryNo = _objPickDetail.PickMaterial.FactoryNo
        objFacTrans.TransactionDate = Date.Now
        objFacTrans.TransactionBy = CType(SecuritySystem.CurrentUser, User).DisplayName

        If _objPickDetail.PickMaterial.IsDataFromFactory = False Then
            objFacTrans.Remark = "จ่ายจากคลังแบบ Manual ไม่ผ่านระบบการเบิกของโรงงาน"
        Else
            objFacTrans.Remark = ""
        End If

        objFacTrans.Save()

    End Sub

    Public Sub CreateNewRequestForm(_session As Session, _pickForm As PickMaterial)
        Dim objNewRequestForm As New FactoryPickMaterial(_session)
        objNewRequestForm.PickDate = Today
        objNewRequestForm.PickType = _pickForm.fPickType
        objNewRequestForm.FactoryNo = _pickForm.fFactoryNo
        objNewRequestForm.RequestUser = _pickForm.fRequestUser
        objNewRequestForm.RequestPosition = _pickForm.fRequestPosition
        objNewRequestForm.Status = PublicEnum.SimsStatus.Approve
        objNewRequestForm.InventoryStatus = PublicEnum.SimsStatus.Approve

        objNewRequestForm.Remark = "จ่ายจากคลังโดยไม่ผ่านระบบเบิกวัสดุการผลิตของโรงงาน จากเลขที่จ่าย " & _pickForm.fPickNo & " วันที่จ่าย " & _pickForm.fPickDate.ToString("d MMMM yy") & " เอกสารเลขที่ " & _pickForm.fRefNo

        For Each item As PickMaterialDetail In _pickForm.PickMaterialDetails
            Dim objNewRequestItem As New FactoryPickMaterialDetail(_session)
            objNewRequestItem.FactoryPickMaterial = objNewRequestForm
            objNewRequestItem.ProductCode = item.ProductCode
            objNewRequestItem.MaterialProduct = item.MaterialProduct
            objNewRequestItem.Quantity = item.Quantity
            objNewRequestItem.Cost = item.Cost
        Next

        objNewRequestForm.Save()
        '//บันทึกข้อมูลการเบิกใน PickMaterial
        Me.fFactoryPickMaterial = objNewRequestForm

    End Sub

    Public Sub UpdateFactoryMaterialInventory(objProduct As MaterialProduct, facNo As PublicEnum.EnumFactoryNo, quantity As Double, actionType As ActionType)
        Dim objFacInventory As FactoryMaterialInventory = Session.FindObject(Of FactoryMaterialInventory)(CriteriaOperator.Parse("MaterialProduct=? and FactoryNo=?", objProduct, facNo))
        If objFacInventory Is Nothing Then
            Dim objNewFac As New FactoryMaterialInventory(Session)
            objNewFac.FactoryNo = facNo
            objNewFac.MaterialProduct = objProduct
            objNewFac.ReceiveAmount = quantity
        Else
            If actionType = PickMaterial.ActionType.Approve Then
                objFacInventory.UpdateStockAmount(FactoryInventory.UpdateStockType.income, quantity)
            Else
                objFacInventory.UpdateStockAmount(FactoryInventory.UpdateStockType.outcome, quantity)
            End If
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
