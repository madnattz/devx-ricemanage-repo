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
<RuleCriteria("FactoryPickSeed.NotDelete", DefaultContexts.Delete, "Status='Pending'", CustomMessageTemplate:="ไม่สามารถลบข้อมูลนี้ได้")> _
<ConditionalAppearance.Appearance("FactoryPickSeedDisableAllItems", criteria:="Status!='Pending'", Enabled:=False, TargetItems:="*", Context:="DetailView")> _
Public Class FactoryPickSeed
    Inherits BaseObject
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        Me.PickDate = Date.Now
        Me.PickType = PublicEnum.PickFor.ProcessSeed
        Me.FactoryNo = PublicEnum.EnumFactoryNo.Factory1
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
    'Dim fPickNo As String
    '<Size(20)> _
    'Public Property PickNo() As String
    '    Get
    '        Return fPickNo
    '    End Get
    '    Set(ByVal value As String)
    '        SetPropertyValue(Of String)("PickNo", fPickNo, value)
    '    End Set
    'End Property
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
            If value = True Then
                PickType = Nothing
            Else
                PickTypeOther = Nothing
            End If
        End Set
    End Property
    Dim fPickTypeOther As String
    <Size(50)> _
    <RuleRequiredField(DefaultContexts.Save, TargetCriteria:="IsOtherPickType=True")> _
    Public Property PickTypeOther() As String
        Get
            Return fPickTypeOther
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PickTypeOther", fPickTypeOther, value)
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
    Dim fStatus As Integer
    Public Property Status() As PublicEnum.SimsStatus
        Get
            Return fStatus
        End Get
        Set(ByVal value As PublicEnum.SimsStatus)
            SetPropertyValue(Of Integer)("Status", fStatus, value)
        End Set
    End Property
    Dim fInventoryStatus As PublicEnum.SimsStatus
    Public Property InventoryStatus() As PublicEnum.SimsStatus
        Get
            Return fInventoryStatus
        End Get
        Set(ByVal value As PublicEnum.SimsStatus)
            SetPropertyValue(Of PublicEnum.SimsStatus)("InventoryStatus", fInventoryStatus, value)
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
    <Association("FactoryPickSeed-FactoryPickSeedDetails", GetType(FactoryPickSeedDetail))> _
    Public ReadOnly Property FactoryPickSeedDetails() As XPCollection(Of FactoryPickSeedDetail)
        Get
            Return GetCollection(Of FactoryPickSeedDetail)("FactoryPickSeedDetails")
        End Get
    End Property

    <Action(Caption:="ยืนยันการเบิก", ConfirmationMessage:="ท่านต้องการบันทึกข้อมูล ใช่หรือไม่?", ImageName:="Action_Grant", AutoCommit:=True, TargetObjectsCriteria:="Status='Pending'")> _
    Public Sub MarkAsComplete()
        If Not IsDeleted Then
            If Not FactoryPickSeedDetails.Count > 0 Then
                MsgBox("ไม่พบรายการเมล็ดพันธุ์ กรุณาตรวจสอบข้อมูลอีกครั้ง", MsgBoxStyle.Information, "")
                Exit Sub
            End If
            Try
                '//สร้างข้อมูลใบเบิก
                CreateInventoryPickForm(Session, Me)

                Me.fStatus = PublicEnum.SimsStatus.Approve

                MyBase.Save()

                Session.CommitTransaction()
            Catch ex As Exception
                Session.RollbackTransaction()
            End Try
        End If
    End Sub

    '<Action(Caption:="ยกเลิก", ConfirmationMessage:="ท่านต้องการบันทึกข้อมูล ใช่หรือไม่?", ImageName:="Action_Deny", AutoCommit:=True, TargetObjectsCriteria:="Status='Approve' AND InventoryStatus='Pending'")> _
    'Public Sub MarkAsCancel()
    '    If Not IsDeleted Then
    '        Try
    '            Me.fStatus = PublicEnum.SimsStatus.Cancel
    '            MyBase.Save()

    '            Session.CommitTransaction()
    '        Catch ex As Exception
    '            Session.RollbackTransaction()
    '        End Try
    '    End If
    'End Sub

    Public Sub CreateInventoryPickForm(_session As Session, _requestForm As FactoryPickSeed)
        '//สร้างข้อมูลใบเบิก
        Dim objNewPickSeed As New PickSeed(_session)
        objNewPickSeed.PickDate = _requestForm.fPickDate
        objNewPickSeed.PickType = _requestForm.fPickType
        objNewPickSeed.FactoryNo = _requestForm.fFactoryNo
        objNewPickSeed.RefNo = _requestForm.fPickNo
        objNewPickSeed.RequestUser = _requestForm.fPickUser
        objNewPickSeed.RequestPosition = _requestForm.fPickPosition
        objNewPickSeed.Status = PublicEnum.SimsStatus.Pending
        objNewPickSeed.Remark = "โรงงานขอเบิกเมล็ดพันธู์เพื่อปรับปรุงสภาพผ่านระบบการเบิกเมล็ดพันธุ์ของโรงงาน"

        For Each item As FactoryPickSeedDetail In _requestForm.FactoryPickSeedDetails
            Dim objPickDetail As New PickSeedDetail(_session)
            objPickDetail.PickSeed = objNewPickSeed
            objPickDetail.SeedProduct = item.SeedProduct
            objPickDetail.SeedProductCode = item.SeedProductCode
            objPickDetail.Quantity = item.Quantity
            objPickDetail.Bags = 0

            objPickDetail.Save()
        Next

        objNewPickSeed.FactoryPickSeed = _requestForm
        objNewPickSeed.IsDataFromFactory = True

        objNewPickSeed.Save()

    End Sub

    '<Action(Caption:="ใบเบิก-จ่าย", ImageName:="Action_Printing_Print", AutoCommit:=True, TargetObjectsCriteria:="Status='Approve'")> _
    'Public Sub ShowReport()
    '    If Not IsDeleted Then
    '        Try
    '            'Dim report As XtraReport2 = ReportData.LoadReport(os)
    '            'report.Filtering.Filter = "[Oid]=='" & Me.Oid.ToString & "'"
    '            'Dim printTool As New ReportPrintToolWpf(report)
    '            'printTool.Print()
    '            '' End Using
    '            'Dim listViewId As String = Application.FindListViewId(GetType(CounterService_C))
    '            'Dim cs As CollectionSourceBase = Application.CreateCollectionSource(os, GetType(CounterService_C), listViewId)
    '            'e.ShowViewParameters.CreatedView = Application.CreateListView(listViewId, cs, True)
    '            'e.ShowViewParameters.Context = TemplateContext.View
    '            'e.ShowViewParameters.TargetWindow = TargetWindow.Current
    '            'Session.CommitTransaction()
    '        Catch ex As Exception
    '            'Session.RollbackTransaction()
    '        End Try
    '    End If
    'End Sub

    Private Sub InsertTransaction(objPickDetail As PickSeedDetail, actionType As ActionType)
        Dim objTrans As New SeedTransaction(Session)
        If actionType = ReceiveSeed.ActionType.Approve Then
            objTrans.TransactionType = PublicEnum.SeedTransactionType.Pick
            objTrans.Income = 0
            objTrans.Outcome = objPickDetail.Quantity
        Else
            objTrans.TransactionType = PublicEnum.SeedTransactionType.CancelPick
            objTrans.Income = objPickDetail.Quantity
            objTrans.Outcome = 0
        End If

        objTrans.RefNo = fPickNo
        objTrans.SeedProduct = objPickDetail.SeedProduct
        objTrans.ProductName = objPickDetail.SeedProduct.ProductName
        objTrans.ProductCode = objPickDetail.SeedProduct.ProductCode
        'objTrans.CollectQuantity = 0
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

    Enum ActionType
        Approve
        Cancel
    End Enum

End Class
