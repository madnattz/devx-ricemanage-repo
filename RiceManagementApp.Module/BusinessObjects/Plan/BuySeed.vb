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

<RuleCriteria("BuySeedCheckValue", DefaultContexts.Save, "[SumWeight] <= [CanBuyQuantity]", "น้ำหนักที่จัดซื้อคืน ต้องน้อยกว่าหรือเท่าจำนวนคงเหลือซื้อคืนได้")> _
<DefaultClassOptions()> _
<RuleCriteria("BuySeed.NotDelete", DefaultContexts.Delete, "BuyStatus='Pending'", CustomMessageTemplate:="ไม่สามารถลบข้อมูลนี้ได้")> _
<ConditionalAppearance.Appearance("BuyDisableAllItems", criteria:="BuyStatus!='Pending'", Enabled:=False, TargetItems:="*", Context:="DetailView")> _
Public Class BuySeed
    Inherits BaseObject
    Implements IBuySeed

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        fBuyDate = Date.Now
        IsApproveCash = False
        DataOwner = GetCurrentSite()
        ' Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).
    End Sub

    Protected Overrides Sub OnSaving()
        MyBase.OnSaving()
        If (Me.fBuySeedNo Is Nothing) Then
            Dim prefix As String = ""
            Dim _year As String = Date.Now.Year + 543
            prefix = _year

            Me.fBuySeedNo = String.Format("{0}/{1:D4}", prefix, DistributedIdGeneratorHelper.Generate(Me.Session.DataLayer, Me.GetType().FullName, prefix))

        End If

        If Me.DataOwner Is Nothing Then
            DataOwner = GetCurrentSite()
        End If

    End Sub

    Protected Overrides Sub OnLoaded()
        MyBase.OnLoaded()
        fSumWeight = Nothing
        fSumAmount = Nothing

        BuySeedWeights.Sorting.Add(New SortProperty("LastUpdateDate", DB.SortingDirection.Ascending))

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

    <Persistent("BuySeedNo")> _
    Private fBuySeedNo As String
    <PersistentAlias("fBuySeedNo")> _
    Public ReadOnly Property BuySeedNo() As String
        Get
            Return fBuySeedNo
        End Get
    End Property

    Dim fMainPlan As MainPlan
    <RuleRequiredField(DefaultContexts.Save)> _
    <VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    Public Property MainPlan() As MainPlan
        Get
            Return fMainPlan
        End Get
        Set(ByVal value As MainPlan)
            SetPropertyValue(Of MainPlan)("MainPlan", fMainPlan, value)
        End Set
    End Property

    Dim fApproveBuy As ApproveBuy
    <RuleRequiredField(DefaultContexts.Save)> _
    <XafDisplayName("อนุมัติจัดซื้อ")> _
    <ImmediatePostData()> _
    <DataSourceCriteria("[ApproveStatus]='Approve'")> _
    Public Property ApproveBuy() As ApproveBuy
        Get
            Return fApproveBuy
        End Get
        Set(ByVal value As ApproveBuy)
            SetPropertyValue(Of ApproveBuy)("ApproveBuy", fApproveBuy, value)
            If Not IsSaving AndAlso Not IsLoading Then
                MainPlan = value.MainPlan
                OnChanged("MainPlan")
            End If
        End Set
    End Property

    '<VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    'Public ReadOnly Property MainPlanSeedName As String
    '    Get
    '        If MainPlan IsNot Nothing Then
    '            Return MainPlan.Plant.PlantName & "  " & MainPlan.SeedType.SeedName & "  ฤดู" & MainPlan.Season.SeasonName & "   ปี " & MainPlan.SeedYear & "   ชั้นพันธุ์ " & MainPlan.SeedClass.ClassName & "   รุ่นที่ " & MainPlan.Lot
    '        Else
    '            Return ""
    '        End If
    '    End Get
    'End Property

    <VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    Public ReadOnly Property MainPlanSeedName As String
        Get
            If ApproveBuy IsNot Nothing Then
                Return ApproveBuy.MainPlan.Plant.PlantName & "  " & ApproveBuy.MainPlan.SeedType.SeedName & "  ฤดู" & ApproveBuy.MainPlan.Season.SeasonName & "   ปี " & MainPlan.SeedYear & "   ชั้นพันธุ์ " & MainPlan.SeedClass.ClassName & "   รุ่นที่ " & MainPlan.Lot
            Else
                Return ""
            End If
        End Get
    End Property

    Dim fBuyDate As DateTime
    <RuleRequiredField(DefaultContexts.Save)> _
    <ImmediatePostData()> _
    Public Property BuyDate() As DateTime
        Get
            Return fBuyDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("BuyDate", fBuyDate, value)
            If Not IsLoading AndAlso Not IsSaving Then
                HarvestDate = value
                OnChanged("HarvestDate")
            End If
        End Set
    End Property

    Dim fHarvestDate As DateTime
    <RuleRequiredField(DefaultContexts.Save)> _
    <XafDisplayName("วันที่เก็บเกี่ยว")> _
    Public Property HarvestDate() As DateTime
        Get
            Return fHarvestDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("HarvestDate", fHarvestDate, value)
        End Set
    End Property

    Dim fBuyFarmer As BuyFarmer
    '<DataSourceCriteria("[PlanFarmerGroup.MainPlan] = '@this.MainPlan' And [ApproveBuy.ApproveStatus] = 'Approve'")> _
    <ImmediatePostData()> _
    <RuleRequiredField(DefaultContexts.Save)> _
    <DataSourceProperty("ApproveBuy.BuyFarmers")> _
    Public Property BuyFarmer() As BuyFarmer
        Get
            Return fBuyFarmer
        End Get
        Set(ByVal value As BuyFarmer)
            SetPropertyValue(Of BuyFarmer)("BuyFarmer", fBuyFarmer, value)
        End Set
    End Property

    '//สำหรับเป็นยอดของการอนุมัติครั้งต่อไป
    <VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    Public ReadOnly Property ApproveQuantity As Double
        Get
            Try
                Return BuyFarmer.WillBuyQuantity
            Catch ex As Exception

            End Try
        End Get
    End Property

    Dim fSeedLoanAmount As Double
    <ModelDefault("DisPlayFormat", ("{0:#,#0.00}"))> _
    Public Property SeedLoanAmount() As Double
        Get
            Return fSeedLoanAmount
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("SeedLoanAmount", fSeedLoanAmount, value)
        End Set
    End Property

    Dim fLastUpdateBy As String
    Public Property LastUpdateBy() As String
        Get
            Return fLastUpdateBy
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("LastUpdateBy", fLastUpdateBy, value)
        End Set
    End Property
    Dim fLastUodateDate As DateTime
    Public Property LastUodateDate() As DateTime
        Get
            Return fLastUodateDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("LastUodateDate", fLastUodateDate, value)
        End Set
    End Property

    Dim fIsSetLot As Boolean
    Public Property IsSetLot() As Boolean
        Get
            Return fIsSetLot
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("IsSetLot", fIsSetLot, value)
        End Set
    End Property

    Dim fIsApproveCash As Boolean
    <VisibleInDetailView(False), VisibleInListView(True), VisibleInLookupListView(False)> _
    <XafDisplayName("เบิกจ่ายแล้ว")> _
    Public Property IsApproveCash() As Boolean
        Get
            Return fIsApproveCash
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("IsApproveCash", fIsApproveCash, value)
        End Set
    End Property

    <Association("BuySeedWeightReferencesBuySeed", GetType(BuySeedWeight))> _
    <DevExpress.Xpo.Aggregated> _
    Public ReadOnly Property BuySeedWeights() As XPCollection(Of BuySeedWeight)
        Get
            Return GetCollection(Of BuySeedWeight)("BuySeedWeights")
        End Get
    End Property

#Region "ReadOnly Property"

    <XafDisplayName("จำนวนที่ซื้อคืนแล้ว (กก.)")> _
    Public ReadOnly Property BuyedQuantity As Double
        Get
            Dim ret As Double = 0
            Try
                Dim listBuyed As New XPCollection(Of BuySeed)(Session, CriteriaOperator.Parse("ApproveBuy=? and BuyFarmer.PlanFarmer=? and Oid <> ? and BuyStatus='Approve'", Me.ApproveBuy, Me.BuyFarmer.PlanFarmer, Me))
                If listBuyed.Count > 0 Then
                    For Each item As BuySeed In listBuyed
                        ret += item.SumWeight
                    Next
                End If
            Catch ex As Exception

            End Try
            Return ret
        End Get
    End Property

    <Persistent("SumWeight")> _
    Private fSumWeight As Nullable(Of Integer) = Nothing
    <ModelDefault("DisPlayFormat", ("{0:#,##}"))> _
    <ImmediatePostData()> _
    <PersistentAlias("fSumWeight")> _
    <XafDisplayName("น้ำหนักสุทธิ รวม (กก.)")> _
    Public ReadOnly Property SumWeight() As Nullable(Of Integer)
        Get
            If (Not IsLoading) AndAlso (Not IsSaving) AndAlso Not fSumWeight.HasValue Then
                UpdateSumWeight(False)
            End If
            Return fSumWeight
        End Get
    End Property

    Public Sub UpdateSumWeight(ByVal forceChangeEvents As Boolean)
        Dim fOldSumWeight As Nullable(Of Integer) = fSumWeight
        fSumWeight = Convert.ToInt32(Evaluate(CriteriaOperator.Parse("BuySeedWeights.Sum(TotalWeight)")))

        If forceChangeEvents Then
            OnChanged("SumWeight", fOldSumWeight, fSumWeight)
        End If
    End Sub

    <ModelDefault("DisPlayFormat", ("{0:#,##}"))> _
    <XafDisplayName("คงเหลือซื้อคืนได้ (กก.)")> _
    Public ReadOnly Property CanBuyQuantity As Double
        Get
            Try
                Return BuyFarmer.WillBuyQuantity - BuyedQuantity
            Catch ex As Exception
                Return 0
            End Try
        End Get
    End Property

    <Persistent("SumAmount")> _
    Private fSumAmount As Nullable(Of Integer) = Nothing
    <ModelDefault("DisPlayFormat", ("{0:#,##}"))> _
    <ImmediatePostData()> _
    <PersistentAlias("fSumAmount")> _
    <XafDisplayName("ราคาสุทธิ รวม (กก.)")> _
    Public ReadOnly Property SumAmount() As Nullable(Of Integer)
        Get
            If (Not IsLoading) AndAlso (Not IsSaving) AndAlso Not fSumAmount.HasValue Then
                UpdateSumAmount(False)
            End If
            Return fSumAmount
        End Get
    End Property

    Public Sub UpdateSumAmount(ByVal forceChangeEvents As Boolean)
        Dim fOldSumAmount As Nullable(Of Integer) = fSumAmount
        fSumAmount = Convert.ToInt32(Evaluate(CriteriaOperator.Parse("BuySeedWeights.Sum(TotalAmount)")))

        If forceChangeEvents Then
            OnChanged("SumAmount", fOldSumAmount, fSumAmount)
        End If
    End Sub

    Dim fTransferAmount As Double
    <ModelDefault("DisPlayFormat", ("{0:#,#0.00}"))> _
    <XafDisplayName("ค่าขนส่ง (บาท)")> _
    Public Property TransferAmount() As Double
        Get
            Return fTransferAmount
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("TransferAmount", fTransferAmount, value)
        End Set
    End Property

    <ImmediatePostData()> _
    <ModelDefault("DisPlayFormat", ("{0:#,#0.00}"))> _
    Public ReadOnly Property TotalAmount() As Double
        Get
            Try
                Return SumAmount - TransferAmount - SeedLoanAmount
            Catch ex As Exception
                Return 0
            End Try

        End Get

    End Property

    Dim fBuyStatus As PublicEnum.BuyStatus
    Public Property BuyStatus() As PublicEnum.BuyStatus
        Get
            Return fBuyStatus
        End Get
        Set(ByVal value As PublicEnum.BuyStatus)
            SetPropertyValue(Of PublicEnum.BuyStatus)("BuyStatus", fBuyStatus, value)
        End Set
    End Property

#End Region

    '<Action(Caption:="ยืนยันการซื้อคืน", ConfirmationMessage:="ท่านต้องการบันทึกข้อมูล ใช่หรือไม่?", ImageName:="Action_Grant", AutoCommit:=True, TargetObjectsCriteria:="BuyStatus='Pending'")> _
    Public Function MarkAsComplete() As Boolean
        If Not BuySeedWeights.Count > 0 Then
            MsgBox("ไม่พบข้อมูลการชั่งน้ำหนัก กรุณาตรวจสอบข้อมูล", MsgBoxStyle.Information)
            Exit Function
        End If

        Try
            Me.BuyFarmer.IsBuy = True
            BuyStatus = PublicEnum.BuyStatus.Approve
            MyBase.Save()
            Session.CommitTransaction()
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function

    '<Action(Caption:="ยกเลิกการซื้อคืน", ConfirmationMessage:="ท่านต้องการบันทึกข้อมูล ใช่หรือไม่?", ImageName:="Action_Deny", AutoCommit:=True, TargetObjectsCriteria:="BuyStatus='Approve'")> _
    Public Function MarkAsCancel() As Boolean
        Try
            Me.BuyFarmer.IsBuy = False
            BuyStatus = PublicEnum.BuyStatus.Cancel
            MyBase.Save()
            Session.CommitTransaction()
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function DoApprove() As Boolean Implements IBuySeed.DoApprove
        Return MarkAsComplete()
    End Function

    Public Function DoCancel() As Boolean Implements IBuySeed.DoCancel
        Return MarkAsCancel()
    End Function
End Class
