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
Imports DevExpress.ExpressApp.ConditionalAppearance
Imports DevExpress.Xpo.Metadata

<DefaultClassOptions(), XafDisplayName("������ɵáü��Ѵ���ŧ")> _
Public Class PlanFarmerGroup
    Inherits BaseObject

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        FormStatus = 0
        DataOwner = GetCurrentSite()
    End Sub

    Protected Overrides Sub OnSaving()
        MyBase.OnSaving()
        If fFormStatus = 0 Then
            fFormStatus = 1
        End If
        If Me.DataOwner Is Nothing Then
            DataOwner = GetCurrentSite()
        End If
    End Sub

    Protected Overrides Sub OnDeleted()
        MyBase.OnDeleted()
        Session.Delete(PlanFarmers)
    End Sub

    Protected Overrides Sub OnLoaded()
        fSumArea = Nothing
        fFarmerCounts = Nothing
        fSumQuantity = Nothing
        MyBase.OnLoaded()
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

    Private _FarmerGroup As FarmerGroup
    <DataSourceProperty("AvailableGroup")> _
    <XafDisplayName("������ɵá�")> _
    <ImmediatePostData()> _
    <RuleRequiredField("PlanFarmerGroup.FarmerGroup", DefaultContexts.Save)> _
    Public Property FarmerGroup As FarmerGroup
        Get
            Return _FarmerGroup
        End Get
        Set(ByVal value As FarmerGroup)
            SetPropertyValue("FarmerGroup", _FarmerGroup, value)
            'If Not value Is Nothing Then
            '    'LoadMember(value)
            'End If
        End Set
    End Property

    <VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    Public ReadOnly Property GroupID As String
        Get
            Try
                Return FarmerGroup.GroupID
            Catch ex As Exception
                Return ""
            End Try

        End Get
    End Property

    Private _AvailableGroup As XPCollection(Of FarmerGroup)
    <Browsable(False)> _
    Public ReadOnly Property AvailableGroup() As XPCollection(Of FarmerGroup)
        Get
            Try
                If _AvailableGroup Is Nothing Then
                    _AvailableGroup = New XPCollection(Of FarmerGroup)(Session, CriteriaOperator.Parse("[Status]='Active'"))
                    Dim _UsedGruop = New XPCollection(Of PlanFarmerGroup)(Session, CriteriaOperator.Parse("MainPlan = ? ", MainPlan))

                    If _UsedGruop.Count > 0 Then
                        For i As Integer = 0 To _AvailableGroup.Count - 1
                            For j As Integer = 0 To _UsedGruop.Count - 1
                                If _AvailableGroup(i).Oid = _UsedGruop(j).FarmerGroup.Oid Then
                                    _AvailableGroup.Remove(_AvailableGroup(i))
                                End If
                            Next
                        Next
                    End If
                End If
            Catch ex As Exception

            End Try
            Return _AvailableGroup
        End Get
    End Property

    '<XafDisplayName("�ӹǹ��Ҫԡ(���)")> _
    '<PersistentAlias("PlanFarmers.count")> _
    '<ModelDefault("DisplayFormat", "{N}")> _
    '<VisibleInListView(True)> _
    'Public ReadOnly Property SumMember() As Integer
    '    Get
    '        Try
    '            Dim CountObject As Object
    '            CountObject = EvaluateAlias("SumMember")
    '            Return CDbl(CountObject)
    '        Catch ex As Exception
    '            Return 0
    '        End Try

    '    End Get
    'End Property

    Private fFarmerCounts As Nullable(Of Integer) = Nothing
    <XafDisplayName("�ӹǹ��Ҫԡ(���)")> _
    <ModelDefault("DisplayFormat", "{N}")> _
    Public ReadOnly Property SumMember() As Nullable(Of Integer)
        Get
            If (Not IsLoading) AndAlso (Not IsSaving) AndAlso Not fFarmerCounts.HasValue Then
                UpdateFarmerCount(False)
            End If
            Return fFarmerCounts
        End Get
    End Property

    Public Sub UpdateFarmerCount(ByVal forceChangeEvents As Boolean)
        Dim fOldFarmerCounts As Nullable(Of Integer) = fFarmerCounts
        'fFarmerCounts = Convert.ToInt32(Evaluate(CriteriaOperator.Parse("PlanFarmers.count")))

        Dim listOfFarmer As New List(Of String)
        For Each item As PlanFarmer In PlanFarmers
            listOfFarmer.Add(item.PersonCardID)
        Next

        listOfFarmer = listOfFarmer.Distinct.ToList

        fFarmerCounts = listOfFarmer.Count

        If forceChangeEvents Then
            OnChanged("SumMember", fOldFarmerCounts, fFarmerCounts)
        End If
    End Sub

    '<XafDisplayName("��鹷���١���(���)")> _
    '<PersistentAlias("PlanFarmers.Sum(AreaSize)")> _
    '<ModelDefault("DisplayFormat", "{N}")> _
    '<VisibleInListView(True)> _
    'Public ReadOnly Property SumArea() As Integer
    '    Get
    '        Try
    '            Dim CountObject As Object
    '            CountObject = EvaluateAlias("SumArea")
    '            Return CDbl(CountObject)
    '        Catch ex As Exception
    '            Return 0
    '        End Try

    '    End Get
    'End Property

    Private fSumArea As Nullable(Of Integer) = Nothing
    <XafDisplayName("��鹷���١���(���)")> _
    <ModelDefault("DisplayFormat", "{N}")> _
    Public ReadOnly Property SumArea() As Nullable(Of Integer)
        Get
            If (Not IsLoading) AndAlso (Not IsSaving) AndAlso Not fSumArea.HasValue Then
                UpdateSumArea(False)
            End If
            Return fSumArea
        End Get
    End Property

    Public Sub UpdateSumArea(ByVal forceChangeEvents As Boolean)
        Dim fOldSumArea As Nullable(Of Integer) = fSumArea
        fSumArea = Convert.ToInt32(Evaluate(CriteriaOperator.Parse("PlanFarmers.Sum(TotalGrowArea)")))

        If forceChangeEvents Then
            OnChanged("SumArea", fOldSumArea, fSumArea)
        End If
    End Sub

    Private fSumQuantity As Nullable(Of Double) = Nothing
    <XafDisplayName("��ҫ��ͤ׹���(��.)")> _
    <ModelDefault("DisPlayFormat", ("{0:#,#0}"))> _
    Public ReadOnly Property SumQuantity() As Nullable(Of Double)
        Get
            If (Not IsLoading) AndAlso (Not IsSaving) AndAlso Not fSumQuantity.HasValue Then
                UpdateSumQuantity(False)
            End If
            Return fSumQuantity
        End Get
    End Property

    Public Sub UpdateSumQuantity(ByVal forceChangeEvents As Boolean)
        Dim fOldSumQuantity As Nullable(Of Integer) = fSumQuantity
        fSumQuantity = Convert.ToInt32(Evaluate(CriteriaOperator.Parse("PlanFarmers.Sum(MaxBuyQuantity)")))

        If forceChangeEvents Then
            OnChanged("SumQuantity", fOldSumQuantity, fSumQuantity)
        End If
    End Sub

    <VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    Public ReadOnly Property SumGroupSeedUse As Integer
        Get
            Dim ret As Integer = 0
            If PlanFarmers.Count > 0 Then
                For Each item As PlanFarmer In PlanFarmers
                    ret += item.TotalSeedUse
                Next
            End If

            Return ret
        End Get
    End Property

    <VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    Public ReadOnly Property SumGroupSeedReceive As Integer
        Get
            Dim ret As Integer = 0
            If PlanFarmers.Count > 0 Then
                For Each item As PlanFarmer In PlanFarmers
                    ret += item.TotalSeedReceive
                Next
            End If

            Return ret
        End Get
    End Property


#Region "Association"
    Private _MainPlan As MainPlan
    <XafDisplayName("������¡�ü�Ե")> _
    <Index(0)> _
    <Association("MainPlan-PlanFarmerGroups")> _
    Public Property MainPlan() As MainPlan
        Get
            Return _MainPlan
        End Get
        Set(ByVal value As MainPlan)
            SetPropertyValue("MainPlan", _MainPlan, value)
        End Set
    End Property

#End Region

#Region "Association"
    <XafDisplayName("��Ҫԡ�ɵá�")> _
    <ImmediatePostData> _
    <Association("PlanFarmerGroup-PlanFarmers")>
    <DevExpress.Xpo.Aggregated> _
    Public ReadOnly Property PlanFarmers() As XPCollection(Of PlanFarmer)
        Get
            Return GetCollection(Of PlanFarmer)("PlanFarmers")
        End Get
    End Property

#End Region

#Region "Ἱ��Ժѵԧҹ"

    Private fGrowStartDate As DateTime
    <XafDisplayName("�ѹ��١")> _
    <ImmediatePostData()> _
    Public Property GrowStartDate As DateTime
        Get
            Return fGrowStartDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue("GrowStartDate", fGrowStartDate, value)
            If Not IsLoading AndAlso Not IsSaving Then
                Dim criteria As String = "SeedType= ? and SeedClass=? and Season=? and SeedYear=?"
                Dim seedname As SeedType = Me.MainPlan.SeedType
                Dim seedClass As SeedClass = Me.MainPlan.SeedClass
                Dim season As Season = Me.MainPlan.Season
                Dim seedYear As String = Me.MainPlan.SeedYear

                Dim ObjSeed As SeedPrice = Session.FindObject(Of SeedPrice)(CriteriaOperator.Parse(criteria, seedname, seedClass, season, seedYear))
                If ObjSeed IsNot Nothing Then
                    fCheckFarmStartDate = value.AddDays(90)
                    fHarvestStartDate = value.AddDays(ObjSeed.SeedAge)
                    fBuyStartDate = fHarvestStartDate.AddDays(1)
                    fProcessStartDate = fBuyStartDate.AddDays(1)

                    OnChanged("CheckFarmStartDate")
                    OnChanged("HarvestStartDate")
                    OnChanged("BuyStartDate")
                    OnChanged("ProcessStartDate")

                End If
            End If
        End Set
    End Property

    Private fGrowEndDate As DateTime
    <XafDisplayName("�֧�ѹ���")> _
    Public Property GrowEndDate As DateTime
        Get
            Return fGrowEndDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue("GrowEndDate", fGrowEndDate, value)
        End Set
    End Property

    Private fCheckFarmStartDate As DateTime
    <XafDisplayName("�ѹ��Ǩ�ŧ")> _
    Public Property CheckFarmStartDate As DateTime
        Get
            Return fCheckFarmStartDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue("CheckFarmStartDate", fCheckFarmStartDate, value)
        End Set
    End Property

    Private fCheckFarmEndDate As DateTime
    <XafDisplayName("�֧�ѹ���")> _
    Public Property CheckFarmEndDate As DateTime
        Get
            Return fCheckFarmEndDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue("CheckFarmEndDate", fCheckFarmEndDate, value)
        End Set
    End Property

    Private fHarvestStartDate As DateTime
    <XafDisplayName("�ѹ�������")> _
    Public Property HarvestStartDate As DateTime
        Get
            Return fHarvestStartDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue("HarvestStartDate", fHarvestStartDate, value)
        End Set
    End Property

    Private fHarvestEndDate As DateTime
    <XafDisplayName("�֧�ѹ���")> _
    Public Property HarvestEndDate As DateTime
        Get
            Return fHarvestEndDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue("HarvestEndDate", fHarvestEndDate, value)
        End Set
    End Property

    Private fBuyStartDate As DateTime
    <XafDisplayName("�ѹ�Ѵ���ͤ׹")> _
    Public Property BuyStartDate As DateTime
        Get
            Return fBuyStartDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue("BuyStartDate", fBuyStartDate, value)
        End Set
    End Property

    Private fBuyEndDate As DateTime
    <XafDisplayName("�֧�ѹ���")> _
    Public Property BuyEndDate As DateTime
        Get
            Return fBuyEndDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue("BuyEndDate", fBuyEndDate, value)
        End Set
    End Property

    Private fProcessStartDate As DateTime
    <XafDisplayName("�ѹ��Ѻ��ا")> _
    Public Property ProcessStartDate As DateTime
        Get
            Return fProcessStartDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue("ProcessStartDate", fProcessStartDate, value)
        End Set
    End Property

    Private fProcessEndDate As DateTime
    <XafDisplayName("�֧�ѹ���")> _
    Public Property ProcessEndDate As DateTime
        Get
            Return fProcessEndDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue("ProcessEndDate", fProcessEndDate, value)
        End Set
    End Property

    Private fMarketStartDate As DateTime
    <XafDisplayName("Ἱ���������õ�Ҵ")> _
    Public Property MarketStartDate As DateTime
        Get
            Return fMarketStartDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue("MarketStartDate", fMarketStartDate, value)
        End Set
    End Property

    Private fMarketEndDate As DateTime
    <XafDisplayName("�֧�ѹ���")> _
    Public Property MarketEndDate As DateTime
        Get
            Return fMarketEndDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue("MarketEndDate", fMarketEndDate, value)
        End Set
    End Property

    Private fFormStatus As Integer
    <VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    Public Property FormStatus As Integer
        Get
            Return fFormStatus
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue("FormStatus", fFormStatus, value)
        End Set
    End Property

    <Action(Caption:="�ʴ���ª�����Ҫԡ", ImageName:="BO_Department", AutoCommit:=True)> _
    Public Sub LoadFarmers()
        MyBase.Save()
        'TargetObjectsCriteria:="Status='Pending'"
        Dim _session As Session = Me.Session
        Try
            Dim ctr As String = "FarmerGroup.GroupID=? and Status='Active'"
            Dim CollectionFarmers As New XPCollection(Of Farmer)(_session, CriteriaOperator.Parse(ctr, Me.FarmerGroup.GroupID))
            If CollectionFarmers.Count <= 0 Then
                MsgBox("��辺�������ɵá�", MsgBoxStyle.Information)
                Exit Sub
            End If
            For Each farmer As Farmer In CollectionFarmers

                Dim subCriteria As String = "PlanFarmerGroup.MainPlan=? and PersonCardID=?"

                Dim objExistPlanfarmer As PlanFarmer = _session.FindObject(Of PlanFarmer)(CriteriaOperator.Parse(subCriteria, Me.MainPlan, farmer.PersonCardID))
                If objExistPlanfarmer Is Nothing Then
                    Dim objPlanFarmer As New PlanFarmer(_session)
                    objPlanFarmer.PlanFarmerGroup = Me
                    objPlanFarmer.FarmerGroup = Me.FarmerGroup
                    objPlanFarmer.Farmer = farmer
                    objPlanFarmer.PersonCardID = farmer.PersonCardID
                    objPlanFarmer.PrefixName = farmer.PrefixName
                    objPlanFarmer.Firstname = farmer.Firstname
                    objPlanFarmer.Lastname = farmer.Lastname
                    objPlanFarmer.Address = farmer.Address
                    objPlanFarmer.TelNo = farmer.TelNo
                    objPlanFarmer.PlanFarmerStatus = PublicEnum.PlanFarmerStatus.Pending

                    objPlanFarmer.Save()
                End If
            Next
            _session.CommitTransaction()
        Catch ex As Exception
            _session.RollbackTransaction()
        End Try
    End Sub

#End Region


End Class
