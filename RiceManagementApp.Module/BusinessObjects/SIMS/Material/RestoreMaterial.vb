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
<RuleCriteria("RestoreMaterial.NotDelete", DefaultContexts.Delete, "Status='Pending'", CustomMessageTemplate:="ไม่สามารถลบข้อมูลนี้ได้")> _
<ConditionalAppearance.Appearance("RestoreMaterialDisableAllItems", criteria:="Status!='Pending'", Enabled:=False, TargetItems:="*", Context:="DetailView")> _
Public Class RestoreMaterial
    Inherits BaseObject
    Implements IApproveAble

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        Me.fRestoreDate = Date.Today
        Me.fStatus = PublicEnum.SimsStatus.Pending
        DataOwner = GetCurrentSite()
    End Sub

    Protected Overrides Sub OnSaving()
        If (Me.fRestoreNo Is Nothing) Then
            Dim prefix As String = ""
            Dim _year As String = (Date.Now.Year + 543).ToString
            prefix = _year

            Me.fRestoreNo = String.Format("{0}/{1:D4}", prefix, DistributedIdGeneratorHelper.Generate(Me.Session.DataLayer, Me.GetType().FullName, prefix))

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

    <Persistent("RestoreNo")> _
    Private fRestoreNo As String
    <PersistentAlias("fRestoreNo")> _
    Public ReadOnly Property RestoreNo() As String
        Get
            Return fRestoreNo
        End Get
    End Property

    Dim fRestoreDate As DateTime
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property RestoreDate() As DateTime
        Get
            Return fRestoreDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("RestoreDate", fRestoreDate, value)
        End Set
    End Property
    Dim fOrganizationSection As OrganizationSection
    <RuleRequiredField(DefaultContexts.Save, TargetCriteria:="IsOtherOrganizationSection=False")> _
    Public Property OrganizationSection() As OrganizationSection
        Get
            Return fOrganizationSection
        End Get
        Set(ByVal value As OrganizationSection)
            SetPropertyValue(Of OrganizationSection)("OrganizationSection", fOrganizationSection, value)
        End Set
    End Property
    Dim fIsOtherOrganizationSection As Boolean
    <ImmediatePostData()> _
    Public Property IsOtherOrganizationSection() As Boolean
        Get
            Return fIsOtherOrganizationSection
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("IsOtherOrganizationSection", fIsOtherOrganizationSection, value)
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
    <RuleRequiredField(DefaultContexts.Save, TargetCriteria:="IsOtherOrganizationSection=True")> _
    Public Property OtherOrganizationSection() As String
        Get
            Return fOtherOrganizationSection
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("OtherOrganizationSection", fOtherOrganizationSection, value)
        End Set
    End Property
    Dim fRestoreUser As String
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property RestoreUser() As String
        Get
            Return fRestoreUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("RestoreUser", fRestoreUser, value)
        End Set
    End Property
    Dim fRestoreUserPosition As String
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property RestoreUserPosition() As String
        Get
            Return fRestoreUserPosition
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("RestoreUserPosition", fRestoreUserPosition, value)
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
    Dim fReceivePosition As String
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property ReceivePosition() As String
        Get
            Return fReceivePosition
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ReceivePosition", fReceivePosition, value)
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
    Dim fStatus As PublicEnum.SimsStatus
    Public Property Status() As PublicEnum.SimsStatus
        Get
            Return fStatus
        End Get
        Set(ByVal value As PublicEnum.SimsStatus)
            SetPropertyValue(Of PublicEnum.SimsStatus)("Status", fStatus, value)
        End Set
    End Property
    <Association("RestoreMaterialDetailReferencesRestoreMaterial", GetType(RestoreMaterialDetail))> _
    Public ReadOnly Property RestoreMaterialDetails() As XPCollection(Of RestoreMaterialDetail)
        Get
            Return GetCollection(Of RestoreMaterialDetail)("RestoreMaterialDetails")
        End Get
    End Property

    Public Function MarkAsComplete() As Boolean
        If Not IsDeleted Then
            If Not RestoreMaterialDetails.Count > 0 Then
                MsgBox("ไม่พบรายการวัสดุการผลิต กรุณาตรวจสอบข้อมูลอีกครั้ง", MsgBoxStyle.Information, "")
                Exit Function
            End If
            Try
                Me.fStatus = PublicEnum.SimsStatus.Approve
                For i As Integer = 0 To RestoreMaterialDetails.Count - 1
                    If RestoreMaterialDetails(i).MaterialProduct IsNot Nothing Then
                        '// Update ตัดยอดคงคลัง
                        RestoreMaterialDetails(i).MaterialProduct.UpdateStockAmount(SeedProduct.UpdateStockType.Income, RestoreMaterialDetails(i).Quantity)
                        '// รายการเคลื่อนไหวคลัง
                        InsertTransaction(RestoreMaterialDetails(i), ActionType.Approve)
                    End If
                Next

                MyBase.Save()
                'Session.CommitTransaction()
                Return True
            Catch ex As Exception
                'Session.RollbackTransaction()
                Return False
            End Try
        End If
    End Function

    Public Function MarkAsCancel() As Boolean
        If Not IsDeleted Then
            Try
                Me.fStatus = PublicEnum.SimsStatus.Cancel

                For i As Integer = 0 To RestoreMaterialDetails.Count - 1
                    If RestoreMaterialDetails(i).MaterialProduct IsNot Nothing Then
                        '// Update เพิ่มยอดคงคลัง
                        RestoreMaterialDetails(i).MaterialProduct.UpdateStockAmount(SeedProduct.UpdateStockType.Outcome, RestoreMaterialDetails(i).Quantity)
                        '// รายการเคลื่อนไหวคลัง
                        InsertTransaction(RestoreMaterialDetails(i), ActionType.Cancel)
                        Dim objToCancel As MaterialTransaction = Session.FindObject(Of MaterialTransaction)(CriteriaOperator.Parse("TransactionType='Restore' and RefNo=?", RestoreNo))
                        If objToCancel IsNot Nothing Then
                            objToCancel.IsDelete = True
                        End If
                    End If
                Next

                MyBase.Save()
                'Session.CommitTransaction()
                Return True
            Catch ex As Exception
                'Session.RollbackTransaction()
                Return False
            End Try
        End If
    End Function

    Private Sub InsertTransaction(objItem As RestoreMaterialDetail, actionType As ActionType)
        Dim objTrans As New MaterialTransaction(Session)
        If actionType = RestoreMaterial.ActionType.Approve Then
            objTrans.TransactionType = PublicEnum.MaterialTransactionType.Restore
            objTrans.Income = objItem.Quantity
            objTrans.Outcome = 0
        Else
            objTrans.TransactionType = PublicEnum.MaterialTransactionType.CancelRestore
            objTrans.Income = 0
            objTrans.Outcome = objItem.Quantity
            objTrans.IsDelete = True
        End If

        objTrans.RefNo = fRestoreNo
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
