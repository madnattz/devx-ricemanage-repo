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

<XafDisplayName("แปลงขยายพันธุ์ที่จัดทำแปลง")> _
<DefaultClassOptions()> _
<RuleCombinationOfPropertiesIsUnique("PlanFarmer,Farm", CustomMessageTemplate:="มีข้อมูลแปลงปลูกนี้แล้ว กรุณเลือกแปลงอื่นๆ")> _
<RuleCriteria("PlanFarmerFarmCheckValue", DefaultContexts.Save, "[TotalGrowArea] <= [AreaSize]", "พื้นที่ปลูกรวม ต้องน้อยกว่าหรือเท่ากับ พื้นที่แปลง")> _
<RuleCriteria("PlanFarmerFarmCheckQuantity", DefaultContexts.Save, "[MaxBuyQuantity] <= [EstimateResultQuantity]", "จำนวนเป้าซื้อคืน ต้องน้อยกว่าหรือเท่ากับ ประมาณาการผลผลิตรวม")> _
Public Class PlanFarmerFarm
    Inherits BaseObject
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        Me.CheckFarm = New CheckFarm(Session)
        DataOwner = GetCurrentSite()
    End Sub

    Protected Overrides Sub OnSaving()
        MyBase.OnSaving()
        If Not IsDeleted Then
            Me.CheckFarm.PlanFarmerFarm = Me
            If fPlanFarmer IsNot Nothing Then
                fPlanFarmer.UpdateEstimateResultPerArea(True)
                fPlanFarmer.UpdateEstimateResultQuantity(True)
                fPlanFarmer.UpdateTotalGrowArea(True)
                fPlanFarmer.UpdateMaxBuyQuantity(True)
            End If
        End If
        If Me.DataOwner Is Nothing Then
            DataOwner = GetCurrentSite()
        End If
    End Sub

    Protected Overrides Sub OnDeleted()
        MyBase.OnDeleted()
        Session.Delete(Me.CheckFarm)
        Session.Delete(Me.PlanFarmerGrows)
        If fPlanFarmer IsNot Nothing Then
            fPlanFarmer.UpdateEstimateResultPerArea(True)
            fPlanFarmer.UpdateEstimateResultQuantity(True)
            fPlanFarmer.UpdateTotalGrowArea(True)
            fPlanFarmer.UpdateMaxBuyQuantity(True)
        End If
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

    Dim fPlanFarmer As PlanFarmer
    <Association("PlanFarmer-Farms")> _
    Public Property PlanFarmer() As PlanFarmer
        Get
            Return fPlanFarmer
        End Get
        Set(ByVal value As PlanFarmer)

            Dim oldPlanFarmer As PlanFarmer = fPlanFarmer
            SetPropertyValue(Of PlanFarmer)("PlanFarmer", fPlanFarmer, value)
            If (Not IsLoading) AndAlso (Not IsSaving) AndAlso oldPlanFarmer IsNot oldPlanFarmer Then
                If (oldPlanFarmer IsNot Nothing) Then
                    oldPlanFarmer = oldPlanFarmer
                Else
                    oldPlanFarmer = fPlanFarmer
                End If
                oldPlanFarmer.UpdateEstimateResultPerArea(True)
                oldPlanFarmer.UpdateEstimateResultQuantity(True)
                oldPlanFarmer.UpdateTotalGrowArea(True)
                oldPlanFarmer.UpdateMaxBuyQuantity(True)

            End If
        End Set
    End Property
    Dim fGrowDate As DateTime

    Dim fFarm As Farm
    <DataSourceProperty("AvailableFarms")> _
    <ImmediatePostData()> _
    <XafDisplayName("แปลง")> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property Farm() As Farm
        Get
            Return fFarm
        End Get
        Set(ByVal value As Farm)
            SetPropertyValue(Of Farm)("Farm", fFarm, value)
        End Set
    End Property

    Private _AvailableFarms As XPCollection(Of Farm)
    <Browsable(False)> _
    Public ReadOnly Property AvailableFarms() As XPCollection(Of Farm)
        Get
            Try
                If _AvailableFarms Is Nothing Then

                    Dim _UsedFarm As New List(Of String)
                    _AvailableFarms = New XPCollection(Of Farm)(Session, CriteriaOperator.Parse("Farmer=? and Status='Active'", Me.PlanFarmer.Farmer))
                    Dim collPlanFarmer As New XPCollection(Of PlanFarmer)(Session, CriteriaOperator.Parse("MainPlan=? and Farmer=? and Farm!=? ", Me.PlanFarmer.PlanFarmerGroup.MainPlan, Me.PlanFarmer.Farmer, Nothing))
                    For Each item As PlanFarmer In collPlanFarmer
                        If item.PlanFarmerFarms.Count > 0 Then
                            For Each row As PlanFarmerFarm In item.PlanFarmerFarms
                                _UsedFarm.Add(row.Farm.Oid.ToString)
                            Next

                        End If
                    Next

                    If _UsedFarm.Count > 0 Then
                        For i As Integer = _AvailableFarms.Count - 1 To 0 Step -1
                            For j As Integer = 0 To _UsedFarm.Count - 1
                                If _AvailableFarms(i).Oid.ToString = _UsedFarm(j) Then
                                    _AvailableFarms.Remove(_AvailableFarms(i))
                                End If
                            Next
                        Next
                    End If

                End If
            Catch ex As Exception

            End Try
            Return _AvailableFarms
        End Get
    End Property

    '<ImmediatePostData()> _
    <XafDisplayName("พื้นที่แปลง (ไร่)")> _
    Public ReadOnly Property AreaSize() As Double
        Get
            If Farm IsNot Nothing Then
                Return Farm.Area
            Else
                Return 0
            End If
        End Get

    End Property

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
        fTotalGrowArea = Convert.ToInt32(Evaluate(CriteriaOperator.Parse("PlanFarmerGrows.Sum(AreaSize)")))

        If forceChangeEvents Then
            OnChanged("TotalGrowArea", fOldTotalGrowArea, fTotalGrowArea)
        End If
    End Sub

    <Persistent("EstimateResultPerArea")> _
    Private fEstimateResultPerArea As Nullable(Of Double) = Nothing
    <ModelDefault("DisPlayFormat", ("{0:#,#0}"))> _
    <PersistentAlias("fEstimateResultPerArea")> _
    <XafDisplayName("ประมาณการผลผลิต (กก./ไร่)")> _
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
        fEstimateResultPerArea = Convert.ToInt32(Evaluate(CriteriaOperator.Parse("PlanFarmerGrows.AVG(EstimateResultPerArea)")))

        If forceChangeEvents Then
            OnChanged("EstimateResultPerArea", fOldEstimateResultPerArea, fEstimateResultPerArea)
        End If
    End Sub

    <Persistent("EstimateResultQuantity")> _
    Private fEstimateResultQuantity As Nullable(Of Double) = Nothing
    <ModelDefault("DisPlayFormat", ("{0:#,#0}"))> _
    <PersistentAlias("fEstimateResultQuantity")> _
    <ModelDefault("DisPlayFormat", ("{0:#,#0}"))> _
    <XafDisplayName("ประมาณการผลผลิตทั้งหมด (กก.)")> _
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
        fEstimateResultQuantity = Convert.ToInt32(Evaluate(CriteriaOperator.Parse("PlanFarmerGrows.Sum(EstimateResultQuantity)")))

        If forceChangeEvents Then
            OnChanged("EstimateResultQuantity", fOldEstimateResultQuantity, fEstimateResultQuantity)
        End If
    End Sub

    Dim fMaxBuyQuantity As Double
    <ModelDefault("DisPlayFormat", ("{0:#,#0}"))> _
    <XafDisplayName("เป้าซื้อคืน (กก.)")> _
    Public Property MaxBuyQuantity() As Double
        Get
            Return fMaxBuyQuantity
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Decimal)("MaxBuyQuantity", fMaxBuyQuantity, value)
        End Set
    End Property

    Dim fCheckFarm As CheckFarm
    '<DC.Aggregated, ExpandObjectMembers(ExpandObjectMembers.Always)> _
    <VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    Public Property CheckFarm() As CheckFarm
        Get
            Return fCheckFarm
        End Get
        Set(ByVal value As CheckFarm)
            SetPropertyValue(Of CheckFarm)("CheckFarm", fCheckFarm, value)
        End Set
    End Property

    <Association("PlanFarmerFarm-PlanFarmerGrows", GetType(PlanFarmerGrow))> _
   <DevExpress.Xpo.Aggregated> _
    Public ReadOnly Property PlanFarmerGrows() As XPCollection(Of PlanFarmerGrow)
        Get
            Return GetCollection(Of PlanFarmerGrow)("PlanFarmerGrows")
        End Get
    End Property

    <VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    Public ReadOnly Property GrowDate As String
        Get
            Dim listOfGrows As New XPCollection(Of PlanFarmerGrow)(Session, CriteriaOperator.Parse("PlanFarmerFarm=? ", Me))

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

End Class
