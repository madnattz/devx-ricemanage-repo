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

'<RuleCombinationOfPropertiesIsUnique("PlanFarmerGroup,Farm", CustomMessageTemplate:="มีข้อมูลแปลงปลูกนี้แล้ว กรุณเลือกแปลงอื่นๆ")> _
<DefaultClassOptions()> _
<RuleCriteria("PlanFarmerCheckQuantity", DefaultContexts.Save, "[MaxBuyQuantity] <= [EstimateResultQuantity]", "จำนวนเป้าซื้อคืน ต้องน้อยกว่าหรือเท่ากับ ประมาณาการผลผลิตรวม")> _
Public Class PlanFarmer
    Inherits BaseObject
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        DataOwner = GetCurrentSite()
    End Sub

    Protected Overrides Sub OnSaving()
        Me.LastUodateDate = Date.Now
        Me.LastUpdateBy = CType(SecuritySystem.CurrentUser, User).DisplayName

        If (Not IsDeleted AndAlso Me.PlanFarmerNo = "") Then
            GenerateFarmerNo()
        End If
        If _PlanFarmerGroup IsNot Nothing Then
            Me._PlanFarmerGroup.UpdateSumArea(True)
            Me._PlanFarmerGroup.UpdateFarmerCount(True)
            Me._PlanFarmerGroup.UpdateSumQuantity(True)
        End If
        'Me.CheckFarm.PlanFarmer = Me

        If Me.DataOwner Is Nothing Then
            DataOwner = GetCurrentSite()
        End If

        MyBase.OnSaving()
    End Sub

    Protected Overrides Sub OnSaved()
        If _PlanFarmerGroup IsNot Nothing Then
            Me._PlanFarmerGroup.UpdateSumArea(True)
            Me._PlanFarmerGroup.UpdateFarmerCount(True)
            Me._PlanFarmerGroup.UpdateSumQuantity(True)
        End If
        MyBase.OnSaved()
    End Sub

    Protected Overrides Sub OnDeleted()
        Session.Delete(Me.fAddress)
        'Session.Delete(Me.fCheckFarm)
        Session.Delete(Me.PlanFarmerFarms)
        Session.Delete(Me.PlanFarmerSeedSouces)
        If _PlanFarmerGroup IsNot Nothing Then
            Me._PlanFarmerGroup.UpdateSumArea(True)
            Me._PlanFarmerGroup.UpdateFarmerCount(True)
            Me._PlanFarmerGroup.UpdateSumQuantity(True)
        End If
        MyBase.OnDeleted()
    End Sub

    Protected Overrides Sub OnDeleting()
        If _PlanFarmerGroup IsNot Nothing Then
            Me._PlanFarmerGroup.UpdateSumArea(True)
            Me._PlanFarmerGroup.UpdateFarmerCount(True)
            Me._PlanFarmerGroup.UpdateSumQuantity(True)
        End If
        MyBase.OnDeleting()
    End Sub

    Protected Overrides Sub OnLoaded()
        MyBase.OnLoaded()
        Me.PlanFarmerSeedSouces.Sorting.Add(New SortProperty("TimeStamp", DevExpress.Xpo.DB.SortingDirection.Ascending))
        fTotalSeedReceive = Nothing
        fTotalSeedUse = Nothing
        fEstimateResultPerArea = Nothing
        fEstimateResultQuantity = Nothing
        fTotalGrowArea = Nothing
    End Sub

    Public Sub GenerateFarmerNo()
        Dim prefix As String = ""
        Dim _year As String = Right(Me.PlanFarmerGroup.MainPlan.SeedYear, 2)
        Dim _groupId As String = String.Format("{0:D2}", Me.PlanFarmerGroup.FarmerGroup.GroupID)
        Dim _seasonId As String = String.Format("{0:D2}", Me.PlanFarmerGroup.MainPlan.Season.SeasonID)
        Dim _seedClass As String = String.Format("{0:D2}", Me.PlanFarmerGroup.MainPlan.SeedClass.ClassID)
        Dim _seedType As String = String.Format("{0:D2}", Me.PlanFarmerGroup.MainPlan.SeedType.SeedID)

        prefix = _year & _seasonId & _seedType & _seedClass & _groupId

        '//รูปแบบ ปี(2)-ฤดู(2)-พันธุ์(2)-ชั้นพันธุ์(2)-กลุ่ม(2)-ลำดับ(3)
        '//58-01-15-02-01-001
        Me.fPlanFarmerNo = String.Format("{0}{1:D3}", prefix, DistributedIdGeneratorHelper.Generate(Me.Session.DataLayer, Me.GetType().FullName, prefix))

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

    Private _PlanFarmerGroup As PlanFarmerGroup
    <XafDisplayName("กลุ่มเกษตรกร")> _
    <ImmediatePostData()> _
    <Association("PlanFarmerGroup-PlanFarmers")> _
    <RuleRequiredField("PlanFarmer.PlanFarmerGroup", DefaultContexts.Save)> _
    Public Property PlanFarmerGroup() As PlanFarmerGroup
        Get
            Return _PlanFarmerGroup
        End Get
        Set(ByVal value As PlanFarmerGroup)
            If value IsNot Nothing Then
                SetPropertyValue("PlanFarmerGroup", _PlanFarmerGroup, value)
                Me.fFarmerGroup = value.FarmerGroup
                OnChanged("FarmerGroup")
            End If
            Dim oldPlanFarmerGroup As PlanFarmerGroup = _PlanFarmerGroup
            If (Not IsLoading) AndAlso (Not IsSaving) AndAlso oldPlanFarmerGroup IsNot _PlanFarmerGroup Then
                If (oldPlanFarmerGroup IsNot Nothing) Then
                    oldPlanFarmerGroup = oldPlanFarmerGroup
                Else
                    oldPlanFarmerGroup = _PlanFarmerGroup
                End If
                oldPlanFarmerGroup.UpdateSumArea(True)
                oldPlanFarmerGroup.UpdateFarmerCount(True)
                oldPlanFarmerGroup.UpdateSumQuantity(True)
            End If
        End Set
    End Property

    <Browsable(False)> _
    Dim fFarmerGroup As FarmerGroup
    Public Property FarmerGroup() As FarmerGroup
        Get
            Return fFarmerGroup
        End Get
        Set(ByVal value As FarmerGroup)
            SetPropertyValue(Of FarmerGroup)("FarmerGroup", fFarmerGroup, value)
        End Set
    End Property

    Dim fFarmer As Farmer
    <DataSourceProperty("FarmerGroup.Farmers")> _
    <RuleRequiredField("PlanFarmer.Farmer", DefaultContexts.Save)> _
    Public Property Farmer() As Farmer
        Get
            Return fFarmer
        End Get
        Set(ByVal value As Farmer)
            SetPropertyValue(Of Farmer)("Farmer", fFarmer, value)
            Me.PrefixName = value.PrefixName
            Me.Firstname = value.Firstname
            Me.Lastname = value.Lastname
            Me.PersonCardID = value.PersonCardID
            Me.Address = value.Address
            Me.TelNo = value.TelNo
        End Set
    End Property

    <Persistent("PlanFarmerNo")> _
    Private fPlanFarmerNo As String
    <PersistentAlias("fPlanFarmerNo")> _
    Public ReadOnly Property PlanFarmerNo() As String
        Get
            Return fPlanFarmerNo
        End Get
    End Property

    Public ReadOnly Property FullName As String
        Get
            If PrefixName IsNot Nothing Then
                Return PrefixName.FrefixName & Firstname & "  " & Lastname
            Else
                Return Firstname & "  " & Lastname
            End If

        End Get
    End Property
    Dim fPersonCardID As String
    <Size(13)> _
    <ImmediatePostData()> _
    Public Property PersonCardID() As String
        Get
            Return fPersonCardID
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PersonCardID", fPersonCardID, value)
        End Set
    End Property
    Dim fPrefixName As PrefixName
    <ImmediatePostData()> _
    Public Property PrefixName() As PrefixName
        Get
            Return fPrefixName
        End Get
        Set(ByVal value As PrefixName)
            SetPropertyValue(Of PrefixName)("PrefixName", fPrefixName, value)
        End Set
    End Property
    Dim fFirstname As String
    <ImmediatePostData()> _
    Public Property Firstname() As String
        Get
            Return fFirstname
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Firstname", fFirstname, value)
        End Set
    End Property
    Dim fLastname As String
    <ImmediatePostData()> _
    Public Property Lastname() As String
        Get
            Return fLastname
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Lastname", fLastname, value)
        End Set
    End Property
    Dim fAddress As CustomAddress
    <ImmediatePostData()> _
    <DC.Aggregated, ExpandObjectMembers(ExpandObjectMembers.Always)> _
    Public Property Address() As CustomAddress
        Get
            Return fAddress
        End Get
        Set(ByVal value As CustomAddress)
            SetPropertyValue(Of CustomAddress)("Address", fAddress, value)
        End Set
    End Property
    Dim fTelNo As String
    <Size(20)> _
    Public Property TelNo() As String
        Get
            Return fTelNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("TelNo", fTelNo, value)
        End Set
    End Property
    'Dim fFarm As Farm
    ''<DataSourceProperty("Farmer.Farms")> _
    '<DataSourceProperty("AvailableFarms")> _
    '<ImmediatePostData()> _
    'Public Property Farm() As Farm
    '    Get
    '        Return fFarm
    '    End Get
    '    Set(ByVal value As Farm)
    '        SetPropertyValue(Of Farm)("Farm", fFarm, value)
    '    End Set
    'End Property

    'Private _AvailableFarms As XPCollection(Of Farm)
    '<Browsable(False)> _
    'Public ReadOnly Property AvailableFarms() As XPCollection(Of Farm)
    '    Get
    '        Try
    '            If _AvailableFarms Is Nothing Then

    '                Dim _UsedFarm As New List(Of String)
    '                _AvailableFarms = New XPCollection(Of Farm)(Session, CriteriaOperator.Parse("Farmer=?", Me.Farmer))
    '                Dim collPlanFarmer As New XPCollection(Of PlanFarmer)(Session, CriteriaOperator.Parse("MainPlan=? and Farmer=? and Farm!=?", Me.PlanFarmerGroup.MainPlan, Me.Farmer, Nothing))
    '                For Each item As PlanFarmer In collPlanFarmer
    '                    If item.Farm IsNot Nothing Then
    '                        _UsedFarm.Add(item.Farm.Oid.ToString)
    '                    End If
    '                Next

    '                If _UsedFarm.Count > 0 Then
    '                    For i As Integer = _AvailableFarms.Count - 1 To 0 Step -1
    '                        For j As Integer = 0 To _UsedFarm.Count - 1
    '                            If _AvailableFarms(i).Oid.ToString = _UsedFarm(j) Then
    '                                _AvailableFarms.Remove(_AvailableFarms(i))
    '                            End If
    '                        Next
    '                    Next
    '                End If

    '            End If
    '        Catch ex As Exception

    '        End Try
    '        Return _AvailableFarms
    '    End Get
    'End Property

    ''<ImmediatePostData()> _
    '<XafDisplayName("พื้นที่แปลง (ไร่)")> _
    'Public ReadOnly Property AreaSize() As Double
    '    Get
    '        If Farm IsNot Nothing Then
    '            Return Farm.Area
    '        Else
    '            Return 0
    '        End If
    '    End Get

    'End Property

    <Persistent("TotalGrowArea")> _
    Private fTotalGrowArea As Nullable(Of Double) = Nothing
    <ModelDefault("DisplayFormat", "{N0}")> _
    <PersistentAlias("fTotalGrowArea")> _
    <XafDisplayName("พื้นที่ปลูกรวม (ไร่)")> _
    Public ReadOnly Property TotalGrowArea() As Nullable(Of Double)
        Get
            If (Not IsLoading) AndAlso (Not IsSaving) AndAlso Not fTotalGrowArea.HasValue Then
                UpdateTotalGrowArea(False)
            End If
            Return fTotalGrowArea
        End Get
    End Property

    Public Sub UpdateTotalGrowArea(ByVal forceChangeEvents As Boolean)
        Dim fOldTotalGrowArea As Nullable(Of Double) = fTotalGrowArea
        fTotalGrowArea = Convert.ToInt32(Evaluate(CriteriaOperator.Parse("PlanFarmerFarms.Sum(TotalGrowArea)")))

        If forceChangeEvents Then
            OnChanged("TotalGrowArea", fOldTotalGrowArea, fTotalGrowArea)
        End If
    End Sub

    Public ReadOnly Property EstimateBuyQuantity As Double
        Get
            Dim ret_val As Double = 0
            'Try
            '    ret_val = AreaSize * 1 'SeedUsePerArea
            'Catch ex As Exception

            'End Try
            Return ret_val
        End Get
    End Property


    <Persistent("TotalSeedUse")> _
    Private fTotalSeedUse As Nullable(Of Double) = Nothing
    <ModelDefault("DisplayFormat", "{N2}")> _
    <PersistentAlias("fTotalSeedUse")> _
    Public ReadOnly Property TotalSeedUse() As Nullable(Of Double)
        Get
            If (Not IsLoading) AndAlso (Not IsSaving) AndAlso Not fTotalSeedUse.HasValue Then
                UpdateTotalUse(False)
            End If
            Return fTotalSeedUse
        End Get
    End Property

    Public Sub UpdateTotalUse(ByVal forceChangeEvents As Boolean)
        Dim fOldTotalSeedUse As Nullable(Of Double) = fTotalSeedUse
        fTotalSeedUse = Convert.ToInt32(Evaluate(CriteriaOperator.Parse("PlanFarmerSeedSouces.Sum(SeedUse)")))

        If forceChangeEvents Then
            OnChanged("TotalSeedUse", fOldTotalSeedUse, fTotalSeedUse)
        End If
    End Sub

    <Persistent("TotalSeedReceive")> _
    Private fTotalSeedReceive As Nullable(Of Double) = Nothing
    <ModelDefault("DisplayFormat", "{N2}")> _
    <PersistentAlias("fTotalSeedReceive")> _
    Public ReadOnly Property TotalSeedReceive() As Nullable(Of Double)
        Get
            If (Not IsLoading) AndAlso (Not IsSaving) AndAlso Not fTotalSeedReceive.HasValue Then
                UpdateTotalReceive(False)
            End If
            Return fTotalSeedReceive
        End Get
    End Property

    Public Sub UpdateTotalReceive(ByVal forceChangeEvents As Boolean)
        Dim fOldTotalSeedReceive As Nullable(Of Double) = fTotalSeedReceive
        fTotalSeedReceive = Convert.ToInt32(Evaluate(CriteriaOperator.Parse("PlanFarmerSeedSouces.Sum(SeedReceive)")))

        If forceChangeEvents Then
            OnChanged("TotalSeedReceive", fOldTotalSeedReceive, fTotalSeedReceive)
        End If
    End Sub

    Public ReadOnly Property FullSeedLotName As String
        Get
            Dim ret_value As String = ""

            Dim listSeedSource As New XPCollection(Of PlanFarmerSeedSouce)(Session, CriteriaOperator.Parse("PlanFarmer.PersonCardID=? and PlanFarmer.PlanFarmerGroup=?", Me.PersonCardID, Me.PlanFarmerGroup))

            '//แก้ไข ให้ดึงข้อมูลในกรณีปลูกพันธุ์เดียวกันหลายแปลง
            For i As Integer = 0 To listSeedSource.Count - 1
                Dim item As PlanFarmerSeedSouce = listSeedSource(i)
                If i <> PlanFarmerSeedSouces.Count - 1 Then
                    ret_value &= item.Site.AppName & " ล็อตที่ " & item.SeedLot & vbCrLf
                Else
                    ret_value &= item.Site.AppName & " ล็อตที่ " & item.SeedLot
                End If
            Next

            'For i As Integer = 0 To PlanFarmerSeedSouces.Count - 1
            '    Dim item As PlanFarmerSeedSouce = PlanFarmerSeedSouces(i)
            '    If i <> PlanFarmerSeedSouces.Count - 1 Then
            '        ret_value &= item.Site.SiteName & " ล็อตที่ " & item.SeedLot & vbCrLf
            '    Else
            '        ret_value &= item.Site.SiteName & " ล็อตที่ " & item.SeedLot
            '    End If
            'Next
            Return ret_value
        End Get
    End Property

    'Dim fGrowDate As DateTime
    '<ImmediatePostData> _
    'Public Property GrowDate() As DateTime
    '    Get
    '        Return fGrowDate
    '    End Get
    '    Set(ByVal value As DateTime)
    '        SetPropertyValue(Of DateTime)("GrowDate", fGrowDate, value)
    '    End Set
    'End Property

    Public Sub UpdateProperty()
        OnChanged("HarvestDate")
        OnChanged("CheckFarmDate")
        OnChanged("BuyDate")
        OnChanged("ProcessDate")

    End Sub

    'Dim fHarvestDate As DateTime
    '<XafDisplayName("วันเก็บเกี่ยว")> _
    'Public Property HarvestDate() As DateTime
    '    Get
    '        Return fHarvestDate
    '    End Get
    '    Set(ByVal value As DateTime)
    '        SetPropertyValue(Of DateTime)("HarvestDate", fHarvestDate, value)
    '    End Set
    'End Property

    Dim fSeedPrice As Double
    <ModelDefault("DisPlayFormat", ("{0:#,#0.00}"))> _
    Public ReadOnly Property SeedPrice() As Double
        Get
            Try
                Dim mPlan As MainPlan = Me.PlanFarmerGroup.MainPlan
                Dim crt As String = "SeedType=? and SeedClass=? and Season=? and SeedYear=?"
                Dim objSeedPrice As SeedPrice = Session.FindObject(Of SeedPrice)(CriteriaOperator.Parse(crt, mPlan.SeedType, mPlan.SeedClass, mPlan.Season, mPlan.SeedYear))
                If Not objSeedPrice Is Nothing Then
                    Return objSeedPrice.Price
                End If
            Catch ex As Exception
                Return 0
            End Try

        End Get
    End Property

    Dim fSeedAge As Integer
    Public ReadOnly Property SeedAge() As Integer
        Get
            Try
                Dim mPlan As MainPlan = Me.PlanFarmerGroup.MainPlan
                Dim crt As String = "SeedType=? and SeedClass=? and Season=? and SeedYear=?"
                Dim objSeedPrice As SeedPrice = Session.FindObject(Of SeedPrice)(CriteriaOperator.Parse(crt, mPlan.SeedType, mPlan.SeedClass, mPlan.Season, mPlan.SeedYear))
                If Not objSeedPrice Is Nothing Then
                    Return objSeedPrice.SeedAge
                End If
            Catch ex As Exception
                Return 0
            End Try
        End Get
    End Property

    <Persistent("EstimateResultPerArea")> _
    Private fEstimateResultPerArea As Nullable(Of Double) = Nothing
    <ModelDefault("DisPlayFormat", ("{0:#,#0}"))> _
    <PersistentAlias("fEstimateResultPerArea")> _
    Public ReadOnly Property EstimateResultPerArea As Double
        Get
            Try
                If (Not IsLoading) AndAlso (Not IsSaving) AndAlso Not fEstimateResultPerArea.HasValue Then
                    UpdateEstimateResultPerArea(False)
                End If
                Return fEstimateResultPerArea
            Catch ex As Exception
                Return 0
            End Try

        End Get
    End Property

    Public Sub UpdateEstimateResultPerArea(ByVal forceChangeEvents As Boolean)
        Dim fOldEstimateResultPerArea As Nullable(Of Double) = fEstimateResultPerArea
        fEstimateResultPerArea = Convert.ToInt32(Evaluate(CriteriaOperator.Parse("PlanFarmerFarms.AVG(EstimateResultPerArea)")))

        If forceChangeEvents Then
            OnChanged("EstimateResultPerArea", fOldEstimateResultPerArea, fEstimateResultPerArea)
        End If
    End Sub

    <Persistent("EstimateResultQuantity")> _
    Private fEstimateResultQuantity As Nullable(Of Double) = Nothing
    <ModelDefault("DisPlayFormat", ("{0:#,#0}"))> _
    <PersistentAlias("fEstimateResultQuantity")> _
    <ModelDefault("DisPlayFormat", ("{0:#,#0}"))> _
    Public ReadOnly Property EstimateResultQuantity As Double
        Get
            Try
                If (Not IsLoading) AndAlso (Not IsSaving) AndAlso Not fEstimateResultQuantity.HasValue Then
                    UpdateEstimateResultQuantity(False)
                End If
                Return fEstimateResultQuantity
            Catch ex As Exception
                Return 0
            End Try
        End Get
    End Property

    Public Sub UpdateEstimateResultQuantity(ByVal forceChangeEvents As Boolean)
        Dim fOldEstimateResultQuantity As Nullable(Of Double) = fEstimateResultQuantity
        fEstimateResultQuantity = Convert.ToInt32(Evaluate(CriteriaOperator.Parse("PlanFarmerFarms.Sum(EstimateResultQuantity)")))

        If forceChangeEvents Then
            OnChanged("EstimateResultQuantity", fOldEstimateResultQuantity, fEstimateResultQuantity)
        End If
    End Sub

    'Dim fMaxBuyQuantity As Double
    '<ModelDefault("DisPlayFormat", ("{0:#,#0}"))> _
    '<ImmediatePostData()> _
    'Public Property MaxBuyQuantity() As Double
    '    Get
    '        Return fMaxBuyQuantity
    '    End Get
    '    Set(ByVal value As Double)
    '        SetPropertyValue(Of Decimal)("MaxBuyQuantity", fMaxBuyQuantity, value)
    '    End Set
    'End Property

    <Persistent("MaxBuyQuantity")> _
    Private fMaxBuyQuantity As Nullable(Of Double) = Nothing
    <ModelDefault("DisPlayFormat", ("{0:#,#0}"))> _
    <PersistentAlias("fMaxBuyQuantity")> _
    <XafDisplayName("เป้าซื้อคืน (กก.)")> _
    Public ReadOnly Property MaxBuyQuantity() As Nullable(Of Double)
        Get
            If (Not IsLoading) AndAlso (Not IsSaving) AndAlso Not fMaxBuyQuantity.HasValue Then
                UpdateMaxBuyQuantity(False)
            End If
            Return fMaxBuyQuantity
        End Get
    End Property

    Public Sub UpdateMaxBuyQuantity(ByVal forceChangeEvents As Boolean)
        Dim fOldMaxBuyQuantity As Nullable(Of Double) = fMaxBuyQuantity
        fMaxBuyQuantity = Convert.ToInt32(Evaluate(CriteriaOperator.Parse("PlanFarmerFarms.Sum(MaxBuyQuantity)")))

        If forceChangeEvents Then
            OnChanged("MaxBuyQuantity", fOldMaxBuyQuantity, fMaxBuyQuantity)
        End If
    End Sub

    '<XafDisplayName("เป้าซื้อคืนรวมรายพันธุ์ (ไร่)")> _
    '<ModelDefault("DisPlayFormat", ("{0:#,#0}"))> _
    'Public ReadOnly Property SumMaxBuyQuantity() As Nullable(Of Double)
    '    Get
    '        Try
    '            Dim listPlanFarmer As New XPCollection(Of PlanFarmer)(Session, CriteriaOperator.Parse("PlanFarmerGroup.MainPlan=? and PersonCardID=?", Me.PlanFarmerGroup.MainPlan, Me.PersonCardID))

    '            Dim sumMaxBuy As Double = 0
    '            For Each item As PlanFarmer In listPlanFarmer
    '                sumMaxBuy += item.MaxBuyQuantity
    '            Next
    '            Return sumMaxBuy
    '        Catch ex As Exception
    '            Return 0
    '        End Try

    '    End Get
    'End Property

    <XafDisplayName("รายสูงสุด")> _
    Public ReadOnly Property IsMaximumFarmer As Boolean
        Get
            If MaxBuyQuantity = 0 Then
                Return False
            End If
            Dim objCollFarmer = New XPCollection(Of PlanFarmer)(Session, CriteriaOperator.Parse("MaxBuyQuantity > ? And PlanFarmerGroup.MainPlan = ?  ", Me.MaxBuyQuantity, Me.PlanFarmerGroup.MainPlan))
            'PlanFarmerGroup.MainPlan = Me.PlanFarmerGroup.MainPlan
            If Not objCollFarmer.Count > 0 Then
                Return True
            Else
                Return False
            End If
        End Get
    End Property

    Dim fMaxBuyBath As Double
    '//วงเงินซื้อคืนไม่เกิน
    <ModelDefault("DisPlayFormat", ("{0:#,#0.00}"))> _
    Public ReadOnly Property MaxBuyBath() As Double
        Get
            Return MaxBuyQuantity * SeedPrice
        End Get

    End Property

    Dim fAdmin As String
    Public Property Admin() As String
        Get
            Return fAdmin
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Admin", fAdmin, value)
        End Set
    End Property

    'Dim fCheckFarm As CheckFarm
    '<DC.Aggregated, ExpandObjectMembers(ExpandObjectMembers.Always)> _
    'Public Property CheckFarm() As CheckFarm
    '    Get
    '        Return fCheckFarm
    '    End Get
    '    Set(ByVal value As CheckFarm)
    '        SetPropertyValue(Of CheckFarm)("CheckFarm", fCheckFarm, value)
    '    End Set
    'End Property
    Dim fBuySeed As Guid
    Public Property BuySeed() As Guid
        Get
            Return fBuySeed
        End Get
        Set(ByVal value As Guid)
            SetPropertyValue(Of Guid)("BuySeed", fBuySeed, value)
        End Set
    End Property

    Dim fSumBuyAmount As Double
    <ModelDefault("DisPlayFormat", ("{0:#,#0.00}"))> _
    Public ReadOnly Property SumBuyAmount() As Double
        Get
            fSumBuyAmount = 0
            Dim CollectionBuyDetail As XPCollection(Of BuySeed) = New XPCollection(Of BuySeed)(Session, CriteriaOperator.Parse("BuyFarmer.PlanFarmer.Oid=? and BuyStatus=?", Me.Oid, PublicEnum.BuyStatus.Approve))

            If CollectionBuyDetail.Count > 0 Then
                For i As Integer = 0 To CollectionBuyDetail.Count - 1
                    fSumBuyAmount += ConvertToDouble(CollectionBuyDetail(i).SumWeight)
                Next
            End If
            Return fSumBuyAmount
        End Get

    End Property

    <VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    Public ReadOnly Property SumApproveQuantity As Double
        Get
            Dim ret As Double = 0
            Dim CollectionBuyDetail As XPCollection(Of BuySeed) = New XPCollection(Of BuySeed)(Session, CriteriaOperator.Parse("BuyFarmer.PlanFarmer.Oid=? and BuyStatus=?", Me.Oid, PublicEnum.BuyStatus.Approve))

            If CollectionBuyDetail.Count > 0 Then
                For i As Integer = 0 To CollectionBuyDetail.Count - 1
                    ret += ConvertToDouble(CollectionBuyDetail(i).ApproveQuantity)
                Next
            End If
            Return ret
        End Get
    End Property

    Public Function ConvertToDouble(obj As Object) As Double
        Try
            Return Convert.ToDouble(obj)
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Dim fSumBuyBaht As Double = 0
    <ModelDefault("DisPlayFormat", ("{0:#,#0.00}"))> _
    <VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    Public ReadOnly Property SumBuyBaht() As Double
        Get
            fSumBuyBaht = 0
            Dim CollectionBuyDetail As XPCollection(Of BuySeed) = New XPCollection(Of BuySeed)(Session, CriteriaOperator.Parse("BuyFarmer.PlanFarmer.Oid=? and BuyStatus=?", Me.Oid, PublicEnum.BuyStatus.Approve))

            If CollectionBuyDetail.Count > 0 Then
                For i As Integer = 0 To CollectionBuyDetail.Count - 1
                    fSumBuyBaht += CollectionBuyDetail(i).TotalAmount
                Next
            End If
            Return fSumBuyBaht
        End Get

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

    Dim fPlanFarmerStatus As PublicEnum.PlanFarmerStatus
    Public Property PlanFarmerStatus() As PublicEnum.PlanFarmerStatus
        Get
            Return fPlanFarmerStatus
        End Get
        Set(ByVal value As PublicEnum.PlanFarmerStatus)
            SetPropertyValue(Of Integer)("PlanFarmerStatus", fPlanFarmerStatus, value)
        End Set
    End Property

    <Association("PlanFarmerSeedSouceReferencesPlanFarmer", GetType(PlanFarmerSeedSouce))> _
    <DevExpress.Xpo.Aggregated> _
    Public ReadOnly Property PlanFarmerSeedSouces() As XPCollection(Of PlanFarmerSeedSouce)
        Get
            Return GetCollection(Of PlanFarmerSeedSouce)("PlanFarmerSeedSouces")
        End Get
    End Property

    '<Association("PlanFarmerGrowReferencesPlanFarmer", GetType(PlanFarmerGrow))> _
    '<DevExpress.Xpo.Aggregated> _
    'Public ReadOnly Property PlanFarmerGrows() As XPCollection(Of PlanFarmerGrow)
    '    Get
    '        Return GetCollection(Of PlanFarmerGrow)("PlanFarmerGrows")
    '    End Get
    'End Property

    <VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    Public ReadOnly Property GrowDate As String
        Get
            Dim listOfGrows As New XPCollection(Of PlanFarmerGrow)(Session, CriteriaOperator.Parse("PlanFarmerFarm.PlanFarmer.PersonCardID=? and PlanFarmerFarm.PlanFarmer.PlanFarmerGroup.MainPlan=?", Me.PersonCardID, PlanFarmerGroup.MainPlan))

            If listOfGrows.Count > 0 Then
                If listOfGrows.Count = 1 Then
                    Return listOfGrows(0).GrowDate.ToString("d MMM yy", New System.Globalization.CultureInfo("th-TH"))
                Else
                    Dim minDate As Date = listOfGrows(0).GrowDate
                    Dim maxDate As Date = listOfGrows(0).GrowDate

                    For Each item As PlanFarmerGrow In listOfGrows
                        If item.GrowDate < minDate Then
                            minDate = item.GrowDate
                        End If
                        If item.GrowDate > maxDate Then
                            maxDate = item.GrowDate
                        End If
                    Next

                    If minDate.ToString("ddMMyy") <> maxDate.ToString("ddMMyy") Then
                        Return minDate.ToString("d MMM yy", New System.Globalization.CultureInfo("th-TH")) & " - " & maxDate.ToString("d MMM yy", New System.Globalization.CultureInfo("th-TH"))
                    Else
                        Return minDate.ToString("d MMM yy", New System.Globalization.CultureInfo("th-TH"))
                    End If

                End If
            Else
                Return ""
            End If
        End Get
    End Property

    <VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    Public ReadOnly Property HarvestDate As String
        'Get
        '    If PlanFarmerGrows.Count > 0 Then
        '        If PlanFarmerGrows.Count = 1 Then
        '            Return PlanFarmerGrows(0).HarvestDate.ToString("d MMM yy")
        '        Else
        '            Dim minDate As Date = PlanFarmerGrows(0).HarvestDate
        '            Dim maxDate As Date = PlanFarmerGrows(0).HarvestDate

        '            For Each item As PlanFarmerGrow In PlanFarmerGrows
        '                If item.HarvestDate < minDate Then
        '                    minDate = item.HarvestDate
        '                End If
        '                If item.HarvestDate > maxDate Then
        '                    maxDate = item.HarvestDate
        '                End If
        '            Next
        '            'If maxDate.Equals(minDate) Then
        '            '    Return minDate.ToString("d MMM yy")
        '            'Else
        '            '    Return minDate.ToString("d MMM yy") & " - " & maxDate.ToString("d MMM yy")
        '            'End If
        '            Return minDate
        '        End If
        '    Else
        '        Return ""
        '    End If
        'End Get

        Get
            Dim listOfGrows As New XPCollection(Of PlanFarmerGrow)(Session, CriteriaOperator.Parse("PlanFarmerFarm.PlanFarmer.PersonCardID=? and PlanFarmerFarm.PlanFarmer.PlanFarmerGroup.MainPlan=?", Me.PersonCardID, PlanFarmerGroup.MainPlan))
            If listOfGrows.Count > 0 Then
                If listOfGrows.Count = 1 Then
                    Return listOfGrows(0).HarvestDate.ToString("d MMM yy", New System.Globalization.CultureInfo("th-TH"))
                Else
                    Dim minDate As Date = listOfGrows(0).HarvestDate
                    Dim maxDate As Date = listOfGrows(0).HarvestDate

                    For Each item As PlanFarmerGrow In listOfGrows
                        If item.HarvestDate < minDate Then
                            minDate = item.HarvestDate
                        End If
                        If item.HarvestDate > maxDate Then
                            maxDate = item.HarvestDate
                        End If
                    Next

                    If minDate.ToString("ddMMyy") <> maxDate.ToString("ddMMyy") Then
                        Return minDate.ToString("d MMM yy", New System.Globalization.CultureInfo("th-TH")) & " - " & maxDate.ToString("d MMM yy", New System.Globalization.CultureInfo("th-TH"))
                    Else
                        Return minDate.ToString("d MMM yy", New System.Globalization.CultureInfo("th-TH"))
                    End If
                    'Return minDate.ToString("d MMM yy", New System.Globalization.CultureInfo("th-TH")) & " - " & maxDate.ToString("d MMM yy", New System.Globalization.CultureInfo("th-TH"))
                End If
            Else
                Return ""
            End If
        End Get
    End Property

    <VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    Public ReadOnly Property CheckFarmDate As String
        Get
            Dim listOfCheckFarm As New XPCollection(Of CheckFarm)(Session, CriteriaOperator.Parse("PlanFarmer.PersonCardID=? and PlanFarmer.PlanFarmerGroup.MainPlan=?", Me.PersonCardID, PlanFarmerGroup.MainPlan))
            If listOfCheckFarm.Count > 0 Then
                If listOfCheckFarm.Count = 1 Then
                    Return listOfCheckFarm(0).CheckDate.ToString("d MMM yy", New System.Globalization.CultureInfo("th-TH"))
                Else
                    Dim minDate As Date = listOfCheckFarm(0).CheckDate
                    Dim maxDate As Date = listOfCheckFarm(0).CheckDate

                    For Each item As CheckFarm In listOfCheckFarm
                        If item.CheckDate < minDate Then
                            minDate = item.CheckDate
                        End If
                        If item.CheckDate > maxDate Then
                            maxDate = item.CheckDate
                        End If
                    Next

                    If minDate.ToString("ddMMyy") <> maxDate.ToString("ddMMyy") Then
                        Return minDate.ToString("d MMM yy", New System.Globalization.CultureInfo("th-TH")) & " - " & maxDate.ToString("d MMM yy", New System.Globalization.CultureInfo("th-TH"))
                    Else
                        Return minDate.ToString("d MMM yy", New System.Globalization.CultureInfo("th-TH"))
                    End If

                    'Return minDate.ToString("d MMM yy", New System.Globalization.CultureInfo("th-TH")) & " - " & maxDate.ToString("d MMM yy", New System.Globalization.CultureInfo("th-TH"))
                End If
            Else
                Return ""
            End If
        End Get
    End Property


    '<Action(Caption:="เสร็จสิ้น", ConfirmationMessage:="ท่านต้องการบันทึกข้อมูล ใช่หรือไม่?", ImageName:="Action_Grant", AutoCommit:=True)> _
    'Public Sub MarkAsComplete()
    '    If Not IsDeleted Then
    '        Me.fPlanFarmerStatus = PublicEnum.PlanFarmerStatus.Approve
    '        MyBase.Save()
    '    End If
    'End Sub


    <Association("PlanFarmer-Farms", GetType(PlanFarmerFarm))> _
    <DevExpress.Xpo.Aggregated> _
    Public ReadOnly Property PlanFarmerFarms() As XPCollection(Of PlanFarmerFarm)
        Get
            Return GetCollection(Of PlanFarmerFarm)("PlanFarmerFarms")
        End Get
    End Property

End Class
