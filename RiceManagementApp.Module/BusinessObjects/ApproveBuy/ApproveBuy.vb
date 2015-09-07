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

<RuleCriteria("ApproveBuy.NotDelete", DefaultContexts.Delete, "ApproveStatus='NotApprove'", CustomMessageTemplate:="ไม่สามารถลบข้อมูลนี้ได้")> _
<RuleCriteria("ApproveBuyCheckValue", DefaultContexts.Save, "[TotalQuantity] <= [AvailableBuyQuantity]", "จำนวนรวมขออนุมัติ ต้องน้อยกว่าหรือเท่ากับ คงเหลือซื้อคืนได้")> _
<ConditionalAppearance.Appearance("ApproveBuy.DisableAllItems", criteria:="ApproveStatus='Approve' Or ApproveStatus='Finish'", Enabled:=False, TargetItems:="*", Context:="DetailView")> _
<DefaultClassOptions()> _
Public Class ApproveBuy
    Inherits BaseObject
    Implements IApproveBuy

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        ApproveBuyDate = Date.Today
        DataOwner = GetCurrentSite()
    End Sub

    Protected Overrides Sub OnLoaded()
        ResetData()
        MyBase.OnLoaded()
    End Sub

    Protected Overrides Sub OnSaving()
        If (Me.fApproveBuyNo Is Nothing) Then
            Dim prefix As String = ""
            Dim _year As String = (Date.Now.Year + 543).ToString
            prefix = _year

            Me.fApproveBuyNo = String.Format("{0}/{1:D4}", prefix, DistributedIdGeneratorHelper.Generate(Me.Session.DataLayer, Me.GetType().FullName, prefix))

        End If
        If Me.DataOwner Is Nothing Then
            DataOwner = GetCurrentSite()
        End If
        MyBase.OnSaving()
    End Sub

    Private Sub ResetData()
        fFarmerCounts = Nothing
        fTotalQuantity = Nothing
        'fMaximumOrder = Nothing
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

    <Persistent("ApproveBuyNo")> _
    Private fApproveBuyNo As String
    <PersistentAlias("fApproveBuyNo")> _
    Public ReadOnly Property ApproveBuyNo() As String
        Get
            Return fApproveBuyNo
        End Get
    End Property

    Dim fApproveBuyDate As DateTime
    <RuleRequiredField(DefaultContexts.Save)> _
    <XafDisplayName("วันที่")> _
    Public Property ApproveBuyDate() As DateTime
        Get
            Return fApproveBuyDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ApproveBuyDate", fApproveBuyDate, value)
        End Set
    End Property

    Dim fRefDocNo As PlanAssignBuyTeam
    <Size(50)> _
    <ImmediatePostData()> _
    Public Property RefDocNo() As PlanAssignBuyTeam
        Get
            Return fRefDocNo
        End Get
        Set(ByVal value As PlanAssignBuyTeam)
            SetPropertyValue(Of PlanAssignBuyTeam)("RefDocNo", fRefDocNo, value)
        End Set
    End Property
    'Dim fRefDocDate As DateTime
    '<RuleRequiredField(DefaultContexts.Save)> _
    Public ReadOnly Property RefDocDate() As DateTime
        Get
            If RefDocNo IsNot Nothing Then
                Return RefDocNo.AssignDtae
            Else
                Return Nothing
            End If

        End Get
        'Set(ByVal value As DateTime)
        '    SetPropertyValue(Of DateTime)("RefDocDate", fRefDocDate, value)
        'End Set
    End Property

    Dim fCenterDocNo As String
    <Size(50)> _
    <XafDisplayName("เลขที่คำสั่งกรมกรมฯ")> _
    Public Property CenterDocNo() As String
        Get
            Return fCenterDocNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CenterDocNo", fCenterDocNo, value)
        End Set
    End Property
    Dim fCenterDocDate As DateTime
    <XafDisplayName("วันที่คำสั่งกรมกรมฯ")> _
    Public Property CenterDocDate() As DateTime
        Get
            Return fCenterDocDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("CenterDocDate", fCenterDocDate, value)
        End Set
    End Property

    Dim fMainPlan As MainPlan
    <ImmediatePostData()> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property MainPlan() As MainPlan
        Get
            Return fMainPlan
        End Get
        Set(ByVal value As MainPlan)
            SetPropertyValue(Of MainPlan)("MainPlan", fMainPlan, value)
            'Try
            '    Dim criteria As String = "SeedType= ? and SeedClass=? and Season=? and SeedYear=?"
            '    Dim seedname As SeedType = MainPlan.SeedType
            '    Dim seedClass As SeedClass = MainPlan.SeedClass
            '    Dim season As Season = MainPlan.Season
            '    Dim seedYear As String = MainPlan.SeedYear

            '    'Dim ObjSeed As SeedPrice = Session.FindObject(Of SeedPrice)(CriteriaOperator.Parse(criteria, seedname, seedClass, season, seedYear))

            '    'If Not ObjSeed Is Nothing Then
            '    '    PricePerUnit = ObjSeed.Price
            '    'End If
            'Catch ex As Exception

            'End Try

        End Set
    End Property

    Dim fApproveCount As Integer
    <RuleRequiredField(DefaultContexts.Save)> _
    <XafDisplayName("ครั้งที่")> _
    Public Property ApproveCount() As Integer
        Get
            Return fApproveCount
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("fApproveCount", fApproveCount, value)
        End Set
    End Property

    <VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    Public ReadOnly Property MainPlanSeedName As String
        Get
            If MainPlan IsNot Nothing Then
                Return MainPlan.Plant.PlantName & "  " & MainPlan.SeedType.SeedName & "  ฤดู" & MainPlan.Season.SeasonName & "   ปี " & MainPlan.SeedYear & "   ชั้นพันธุ์ " & MainPlan.SeedClass.ClassName & "   รุ่นที่ " & MainPlan.Lot & "  ครั้งที่ " & ApproveCount.ToString
            Else
                Return ""
            End If
        End Get
    End Property

    <VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    Public ReadOnly Property MaxBuyFarmer As PlanFarmer
        Get
            Dim objFarmer As PlanFarmer = Session.FindObject(Of PlanFarmer)(CriteriaOperator.Parse("PlanFarmerGroup.MainPlan = ? and IsMaximumFarmer=True", Me.MainPlan))
            'Dim m As PlanFarmer
            'm.IsMaximumFarmer = True

            Return objFarmer
        End Get
    End Property

    Dim fPricePerUnit As Double
    <ImmediatePostData()> _
    <ModelDefault("DisPlayFormat", ("{0:#,#0.00}"))> _
    <RuleValueComparison(DefaultContexts.Save, ValueComparisonType.GreaterThan, 0)> _
    Public Property PricePerUnit() As Double
        Get
            Return fPricePerUnit
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("PricePerUnit", fPricePerUnit, value)
        End Set
    End Property

    Dim fRefDocAmount As Double
    <ModelDefault("DisPlayFormat", ("{0:#,#0.00}"))> _
    Public Property RefDocAmount() As Double
        Get
            Return fRefDocAmount
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("RefDocAmount", fRefDocAmount, value)
        End Set
    End Property

    <XafDisplayName("คงเหลือซื้อคืนได้ (กก.)")> _
    Public ReadOnly Property AvailableBuyQuantity As Double
        Get
            Try
                Return MainPlan.AvailableBuyQuantity
            Catch ex As Exception
                Return 0
            End Try
        End Get
    End Property

    'Private fTotalQuantity As Nullable(Of Decimal)
    '<PersistentAlias("BuyFarmers.Sum(WillBuyQuantity)")> _
    '<ModelDefault("DisPlayFormat", ("{0:#,#0.00}"))> _
    'Public ReadOnly Property TotalQuantity() As Double
    '    Get
    '        Try
    '            Dim tempObject As Object
    '            tempObject = EvaluateAlias("TotalQuantity")
    '            Return CDbl(tempObject)
    '        Catch ex As Exception
    '            Return 0
    '        End Try

    '    End Get

    'End Property

    <Persistent("TotalQuantity")> _
    Private fTotalQuantity As Nullable(Of Integer) = Nothing
    <ModelDefault("DisPlayFormat", ("{0:#,##}"))> _
    <ImmediatePostData()> _
    <PersistentAlias("fTotalQuantity")> _
    <XafDisplayName("จำนวนรวมขออนุมัติ(กก.)")> _
    Public ReadOnly Property TotalQuantity() As Nullable(Of Integer)
        Get
            If (Not IsLoading) AndAlso (Not IsSaving) AndAlso Not fTotalQuantity.HasValue Then
                UpdateTotalQuantity(False)
            End If
            Return fTotalQuantity
        End Get
    End Property

    Public Sub UpdateTotalQuantity(ByVal forceChangeEvents As Boolean)
        Dim fOldTotalQuantity As Nullable(Of Integer) = fTotalQuantity
        fTotalQuantity = Convert.ToInt32(Evaluate(CriteriaOperator.Parse("BuyFarmers.Sum(WillBuyQuantity)")))

        If forceChangeEvents Then
            OnChanged("TotalQuantity", fOldTotalQuantity, fTotalQuantity)
        End If
    End Sub

    '<PersistentAlias("BuyFarmers.Sum(MaxBuyBath)")> _
    <ModelDefault("DisPlayFormat", ("{0:#,#0.00}"))> _
    <ImmediatePostData()> _
    Public ReadOnly Property TotalAmount() As Double
        Get
            Return TotalQuantity * PricePerUnit
        End Get

    End Property

    Private fFarmerCounts As Nullable(Of Integer) = Nothing
    Public ReadOnly Property FarmerCounts() As Nullable(Of Integer)
        Get
            If (Not IsLoading) AndAlso (Not IsSaving) AndAlso Not fFarmerCounts.HasValue Then
                UpdateFarmerCount(False)
            End If
            Return fFarmerCounts
        End Get
    End Property

    Public Sub UpdateFarmerCount(ByVal forceChangeEvents As Boolean)
        Dim fOldFarmerCounts As Nullable(Of Integer) = fFarmerCounts
        fFarmerCounts = Convert.ToInt32(Evaluate(CriteriaOperator.Parse("BuyFarmers.Count")))

        If forceChangeEvents Then
            OnChanged("FarmerCounts", fOldFarmerCounts, fFarmerCounts)
        End If
    End Sub

    Dim fApproveAmount As Double
    <ModelDefault("DisPlayFormat", ("{0:#,#0.00}"))> _
    Public Property ApproveAmount() As Double
        Get
            Return fApproveAmount
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("ApproveAmount", fApproveAmount, value)
        End Set
    End Property
    Dim fTakeAmount As Double
    <ModelDefault("DisPlayFormat", ("{0:#,#0.00}"))> _
    Public Property TakeAmount() As Double
        Get
            Return fTakeAmount
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("TakeAmount", fTakeAmount, value)
        End Set
    End Property


    Dim fBuyStartDate As DateTime
    <RuleRequiredField(DefaultContexts.Save)> _
    <XafDisplayName("วันที่จัดซื้อคืน")> _
    Public Property BuyStartDate() As DateTime
        Get
            Return fBuyStartDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("BuyStartDate", fBuyStartDate, value)
        End Set
    End Property

    Dim fBuyEndDate As DateTime
    <RuleRequiredField(DefaultContexts.Save)> _
    <XafDisplayName("ถึงวันที่")> _
    Public Property BuyEndDate() As DateTime
        Get
            Return fBuyEndDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("BuyEndDate", fBuyEndDate, value)
        End Set
    End Property

    '<Association("AuditTeamReferencesApproveBuy", GetType(AuditTeam))> _
    '<DevExpress.Xpo.Aggregated> _
    'Public ReadOnly Property AuditTeams() As XPCollection(Of AuditTeam)
    '    Get
    '        Return GetCollection(Of AuditTeam)("AuditTeams")
    '    End Get
    'End Property
    <Association("BuyFarmerReferencesApproveBuy", GetType(BuyFarmer))> _
    <DevExpress.Xpo.Aggregated> _
    <ImmediatePostData()> _
    <RuleUniqueValue(DefaultContexts.Save, TargetPropertyName:="PlanFarmer", CustomMessageTemplate:="ไม่สามารถเลือกเกษตรกรซ้ำกันได้")> _
    Public ReadOnly Property BuyFarmers() As XPCollection(Of BuyFarmer)
        Get
            Return GetCollection(Of BuyFarmer)("BuyFarmers")
        End Get
    End Property
    '<Association("BuyTeamReferencesApproveBuy", GetType(BuyTeam))> _
    '<DevExpress.Xpo.Aggregated> _
    'Public ReadOnly Property BuyTeams() As XPCollection(Of BuyTeam)
    '    Get
    '        Return GetCollection(Of BuyTeam)("BuyTeams")
    '    End Get
    'End Property
    '<Association("ReceiveTeamReferencesApproveBuy", GetType(ReceiveTeam))> _
    '<DevExpress.Xpo.Aggregated> _
    'Public ReadOnly Property ReceiveTeams() As XPCollection(Of ReceiveTeam)
    '    Get
    '        Return GetCollection(Of ReceiveTeam)("ReceiveTeams")
    '    End Get
    'End Property

    Dim fApproveStatus As PublicEnum.ApproveBuyStatus
    Public Property ApproveStatus() As PublicEnum.ApproveBuyStatus
        Get
            Return fApproveStatus
        End Get
        Set(ByVal value As PublicEnum.ApproveBuyStatus)
            SetPropertyValue(Of PublicEnum.ApproveBuyStatus)("ApproveStatus", fApproveStatus, value)
        End Set
    End Property

    <Browsable(False)> _
    Public ReadOnly Property Status As PublicEnum.ApproveBuyStatus
        Get
            Return ApproveStatus
        End Get
    End Property

    '<Action(Caption:="อนุมัติ", ConfirmationMessage:="ท่านต้องการบันทึกข้อมูล ใช่หรือไม่?", ImageName:="Action_Grant", AutoCommit:=True, TargetObjectsCriteria:="ApproveStatus='NotApprove'")> _
    Public Function MarkAsComplete() As Boolean
        If Not IsDeleted Then
            Try
                If Not BuyFarmers.Count > 0 Then
                    MsgBox("ไม่พบข้อมูลรายชื่อเกษตรกร กรุณาตรวจสอบข้อมูล", MsgBoxStyle.Information)
                    Exit Function
                End If
                Me.ApproveStatus = PublicEnum.PublicApprove.Approve

                MyBase.Save()
                Session.CommitTransaction()
                Return True
            Catch ex As Exception
                Session.RollbackTransaction()
                Return False
            End Try
        Else
            Return False
        End If
    End Function
    '<Action(Caption:="ย้อนกลับ", ConfirmationMessage:="ท่านต้องการบันทึกข้อมูล ใช่หรือไม่?", ImageName:="Action_Deny", AutoCommit:=True, TargetObjectsCriteria:="ApproveStatus='Approve'")> _
    Public Function MarkCancle() As Boolean
        If Not IsDeleted Then
            Try
                Me.ApproveStatus = PublicEnum.ApproveBuyStatus.NotApprove

                MyBase.Save()
                Session.CommitTransaction()
                Return True
            Catch ex As Exception
                Session.RollbackTransaction()
                Return False
            End Try
        Else
            Return False
            'Session.CommitTransaction()
        End If
    End Function

    '<Action(Caption:="เสร็จสิ้น", ConfirmationMessage:="ท่านต้องการบันทึกข้อมูล ใช่หรือไม่?", ImageName:="Action_Grant_Set", AutoCommit:=True, TargetObjectsCriteria:="ApproveStatus='Approve'")> _
    Public Function MarkFinish() As Boolean
        If Not IsDeleted Then
            Try
                Me.ApproveStatus = PublicEnum.ApproveBuyStatus.Finish
                MyBase.Save()
                Session.CommitTransaction()
                Return True
            Catch ex As Exception
                Session.RollbackTransaction()
                Return False
            End Try
        Else
            Return False
        End If
    End Function

    Public Function DoApprove() As Boolean Implements IApproveBuy.DoApprove
        Return MarkAsComplete()
    End Function

    Public Function DoCancel() As Boolean Implements IApproveBuy.DoCancel
        Return MarkCancle()
    End Function

    Public Function DoFinish() As Boolean Implements IApproveBuy.DoFinish
        Return MarkFinish()
    End Function
End Class
