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

'<RuleCriteria("BuyFarmerCheckValue1", DefaultContexts.Save, "[IsBuyOverMaximum]=False And ([WillBuyQuantity] > [AvailablePlanBuyQuantity] And [WillBuyQuantity] > [AvailableBuyQuantity]) ", "น้ำหนักที่ขออนุมัติซื้อคืน ต้องน้อยกว่าหรือเท่ากับจำนวน คงเหลือซื้อคืนได้ (เป้ารวม และ เป้ารายเกษตรกร) ")> _
'<RuleCriteria("BuyFarmerCheckValue2", DefaultContexts.Save, "[IsBuyOverMaximum]=True  And ([WillBuyQuantity] > [AvailablePlanBuyQuantity] And [WillBuyQuantity] > [MaximumFarmerQuantity]) ", "น้ำหนักที่ขออนุมัติซื้อคืน ต้องน้อยกว่าหรือเท่ากับจำนวน คงเหลือซื้อคืนได้ (เป้ารวม และ เป้ารายสูงสุด)")> _
<DefaultClassOptions()> _
Public Class BuyFarmer
    Inherits BaseObject

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        CanSave = False
        DataOwner = GetCurrentSite()
    End Sub

    Protected Overrides Sub OnSaving()
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

    Dim fApproveBuy As ApproveBuy
    <Association("BuyFarmerReferencesApproveBuy")> _
    Public Property ApproveBuy() As ApproveBuy
        Get
            Return fApproveBuy
        End Get
        Set(ByVal value As ApproveBuy)
            Dim oldApproveBuy As ApproveBuy = fApproveBuy
            SetPropertyValue(Of ApproveBuy)("ApproveBuy", fApproveBuy, value)
            If (Not IsLoading) AndAlso (Not IsSaving) AndAlso oldApproveBuy IsNot fApproveBuy Then
                If (oldApproveBuy IsNot Nothing) Then
                    oldApproveBuy = oldApproveBuy
                Else
                    oldApproveBuy = fApproveBuy
                End If
                oldApproveBuy.UpdateFarmerCount(True)
                oldApproveBuy.UpdateTotalQuantity(True)
            End If
        End Set
    End Property
    Dim fPlanFarmerGroup As PlanFarmerGroup
    <ImmediatePostData()> _
    Public Property PlanFarmerGroup() As PlanFarmerGroup
        Get
            Return fPlanFarmerGroup
        End Get
        Set(ByVal value As PlanFarmerGroup)
            Try
                If Not value.Equals(fPlanFarmerGroup) Then
                    PlanFarmer = Nothing
                    SumBuyQuantity = 0
                    AvailablePlanBuyQuantity = 0
                    CanSave = False
                    OnChanged("PlanFarmer")
                    OnChanged("AvailablePlanBuyQuantity")
                    OnChanged("CanSave")
                    OnChanged("SumBuyQuantity")
                End If
            Catch ex As Exception

            End Try

            SetPropertyValue(Of PlanFarmerGroup)("PlanFarmerGroup", fPlanFarmerGroup, value)
        End Set
    End Property

    Dim fPlanFarmer As PlanFarmer
    <ImmediatePostData()> _
    <DataSourceProperty("AvailablePlanFarmer")> _
    Public Property PlanFarmer() As PlanFarmer
        Get
            Return fPlanFarmer
        End Get
        Set(ByVal value As PlanFarmer)
            Try
                If Not value.Equals(fPlanFarmer) Then
                    WillBuyQuantity = 0
                    SumBuyQuantity = 0
                    AvailablePlanBuyQuantity = 0
                    CanSave = False
                End If
            Catch ex As Exception

            End Try

            SetPropertyValue(Of PlanFarmer)("PlanFarmer", fPlanFarmer, value)
            If Not value Is Nothing And Not IsLoading AndAlso Not IsSaving Then
                SumBuyQuantity = GetSumApproveQuantity() 'value.SumApproveQuantity ' value.SumBuyAmount
                AvailablePlanBuyQuantity = value.PlanFarmerGroup.MainPlan.AvailableBuyQuantity
            End If
        End Set
    End Property

    Private _AvailablePlanFarmer As XPCollection(Of PlanFarmer)
    <Browsable(False)> _
    Public ReadOnly Property AvailablePlanFarmer() As XPCollection(Of PlanFarmer)
        Get
            Try
                Dim _UsedItem As New List(Of String)

                Dim _UsedPlanFarmer As New XPCollection(Of BuyFarmer)(Session, CriteriaOperator.Parse("ApproveBuy=?", Me.ApproveBuy))

                For Each item As BuyFarmer In _UsedPlanFarmer
                    If item.PlanFarmer IsNot Nothing Then
                        _UsedItem.Add(item.PlanFarmer.PlanFarmerNo)
                    End If
                Next

                For Each item As BuyFarmer In Me.ApproveBuy.BuyFarmers
                    If item.PlanFarmer IsNot Nothing Then
                        _UsedItem.Add(item.PlanFarmer.PlanFarmerNo)
                    End If
                Next

                _UsedItem = _UsedItem.Distinct.ToList

                Dim crt As String = ""
                If _UsedItem.Count > 0 Then
                    For i As Integer = 0 To _UsedItem.Count - 1
                        If i <> _UsedItem.Count - 1 Then
                            crt &= "'" & _UsedItem(i) & "',"
                        Else
                            crt &= "'" & _UsedItem(i) & "'"
                        End If
                    Next
                End If

                _AvailablePlanFarmer = New XPCollection(Of PlanFarmer)(Session, CriteriaOperator.Parse("PlanFarmerGroup=? and Not PlanFarmerNo in(" & crt & ")", Me.PlanFarmerGroup, crt))

            Catch ex As Exception

            End Try
            Return _AvailablePlanFarmer
        End Get
    End Property

    Private Sub RefreshAvailableAvailablePlanFarmer()
        If _AvailablePlanFarmer Is Nothing Then
            _AvailablePlanFarmer = Nothing
        End If
    End Sub

    Dim fIsBuyOverMaximum As Boolean
    <XafDisplayName("ขออนุมัติเกินเป้าซื้อคืน")> _
    <ImmediatePostData()> _
    Public Property IsBuyOverMaximum As Boolean
        Get
            Return fIsBuyOverMaximum
        End Get
        Set(value As Boolean)
            SetPropertyValue(Of Boolean)("IsBuyOverMaximum", fIsBuyOverMaximum, value)
            fCanSave = False
            If IsBuyOverMaximum = False Then
                If WillBuyQuantity <= AvailablePlanBuyQuantity And WillBuyQuantity <= AvailableBuyQuantity Then
                    CanSave = True
                Else
                    CanSave = False
                End If
            Else
                If WillBuyQuantity <= AvailablePlanBuyQuantity AndAlso WillBuyQuantity <= MaximumFarmerQuantity Then
                    CanSave = True
                Else
                    CanSave = False
                End If
            End If
            OnChanged("CanSave")
        End Set
    End Property

    <ModelDefault("DisPlayFormat", ("{0:#,#0.00}"))> _
    Public ReadOnly Property MaxBuyQuantity As Double
        Get
            Try
                If PlanFarmer IsNot Nothing Then
                    Return PlanFarmer.MaxBuyQuantity
                End If
            Catch ex As Exception

            End Try
            
        End Get
    End Property

    '<ModelDefault("DisPlayFormat", ("{0:#,#0.00}"))> _
    'Public ReadOnly Property MaxBuyBath As Double
    '    Get
    '        If fPlanFarmer IsNot Nothing Then
    '            Return fPlanFarmer.MaxBuyBath
    '        End If
    '    End Get
    'End Property

    Public Function GetSumApproveQuantity() As Double
        Dim ret As Double = 0
        Dim CollDetail As New XPCollection(Of BuyFarmer)(Session, CriteriaOperator.Parse("PlanFarmer.Oid=? and Oid<>? and ApproveBuy.ApproveStatus <> 'NotApprove'", Me.PlanFarmer.Oid, Me.Oid))
        If CollDetail.Count > 0 Then
            For i As Integer = 0 To CollDetail.Count - 1
                ret += (CollDetail(i).WillBuyQuantity)
            Next
        End If
        Return ret
    End Function

    Dim fSumBuyQuantity As Double
    <ModelDefault("DisPlayFormat", ("{0:#,#0.00}"))> _
    <XafDisplayName("จำนวนที่อนุมัติแล้ว (กก.)")> _
    Public Property SumBuyQuantity As Double
        Get
            Return fSumBuyQuantity
        End Get
        Set(value As Double)
            SetPropertyValue(Of Double)("SumBuyQuantity", fSumBuyQuantity, value)
        End Set
    End Property

    Dim fAvailablePlanBuyQuantity As Double
    <ModelDefault("DisPlayFormat", ("{0:#,#0.00}"))> _
    <XafDisplayName("คงเหลือซื้อคืนได้ (เป้ารวม) (กก.)")> _
    Public Property AvailablePlanBuyQuantity As Double
        Get
            Return fAvailablePlanBuyQuantity
        End Get
        Set(value As Double)
            SetPropertyValue(Of Double)("AvailablePlanBuyQuantity", fAvailablePlanBuyQuantity, value)
        End Set
    End Property

    <ModelDefault("DisPlayFormat", ("{0:#,#0.00}"))> _
    <XafDisplayName("คงเหลืออนุมัติได้ (เป้ารายเกษตรกร) (กก.)")> _
    Public ReadOnly Property AvailableBuyQuantity As Double
        Get
            Dim oldValue As Double = 0
            'Try
            '    Dim listOldApprove As New XPCollection(Of BuyFarmer)(Session, CriteriaOperator.Parse("(ApproveBuy.ApproveStatus='Approve' Or ApproveBuy.ApproveStatus='Finish') and (ApproveBuy.MainPlan=? and ApproveBuy.Oid <> ?)", Me.ApproveBuy.MainPlan, Me.ApproveBuy.Oid))
            '    'listOldApprove(0).ApproveBuy
            '    If listOldApprove.Count > 0 Then
            '        For Each item As BuyFarmer In listOldApprove
            '            If item.PlanFarmer.Oid = Me.PlanFarmer.Oid Then
            '                oldValue += item.WillBuyQuantity
            '            End If
            '        Next
            '    End If
            'Catch ex As Exception

            'End Try

            Return MaxBuyQuantity - SumBuyQuantity
            'If fPlanFarmer IsNot Nothing Then
            '    Return fPlanFarmer.MaxBuyQuantity - SumBuyQuantity
            'End If
        End Get
    End Property

    <ModelDefault("DisPlayFormat", ("{0:#,#0.00}"))> _
   <XafDisplayName("เป้าซื้อคืนรายสูงสุด (กก.)")> _
    Public ReadOnly Property MaximumFarmerQuantity As Double
        Get
            Try
                If Not PlanFarmer Is Nothing Then
                    Dim objFarmer As PlanFarmer = Session.FindObject(Of PlanFarmer)(CriteriaOperator.Parse("[IsMaximumFarmer]= True and PlanFarmerGroup.MainPlan = ?", Me.PlanFarmer.PlanFarmerGroup.MainPlan))
                    If Not objFarmer Is Nothing Then
                        Return objFarmer.MaxBuyQuantity
                    Else
                        Return 0
                    End If
                Else
                    Return 0
                End If
            Catch ex As Exception
                Return 0
            End Try
        End Get
    End Property

    Dim fWillBuyQuantity As Double
    <ImmediatePostData()> _
    <ModelDefault("DisPlayFormat", ("{0:#,#0.00}"))> _
    <RuleValueComparison(DefaultContexts.Save, ValueComparisonType.GreaterThan, 0)> _
    Public Property WillBuyQuantity As Double
        Get
            Return fWillBuyQuantity
        End Get
        Set(ByVal value As Double)
            fCanSave = False
            If IsBuyOverMaximum = False Then
                If value <= AvailablePlanBuyQuantity And value <= AvailableBuyQuantity Then
                    CanSave = True
                Else
                    CanSave = False
                End If
            Else
                If value <= AvailablePlanBuyQuantity AndAlso value <= MaximumFarmerQuantity Then
                    CanSave = True
                Else
                    CanSave = False
                End If
            End If
            SetPropertyValue(Of Double)("WillBuyQuantity", fWillBuyQuantity, value)
            If (Not IsLoading) AndAlso (Not IsSaving) AndAlso fApproveBuy IsNot Nothing Then
                fApproveBuy.UpdateTotalQuantity(True)
            End If
            OnChanged("CanSave")
        End Set
    End Property

    <ModelDefault("DisPlayFormat", ("{0:#,#0.00}"))> _
    Public ReadOnly Property WillBuyBath As Double
        Get
            Try
                Return fWillBuyQuantity * ApproveBuy.PricePerUnit
            Catch ex As Exception
                Return 0
            End Try
        End Get
    End Property

    Dim fIsBuy As Boolean
    Public Property IsBuy As Boolean
        Get
            Return fIsBuy
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("IsBuy", fIsBuy, value)
        End Set
    End Property

    Dim fCanSave As Boolean
    <RuleFromBoolProperty("BuyFarmer.CanSave", DefaultContexts.Save, "จัดซื้อปกติ : น้ำหนักที่ขออนุมัติซื้อคืน ต้องน้อยกว่าหรือเท่ากับจำนวน คงเหลือซื้อคืนได้ (เป้ารวม และ เป้ารายเกษตรกร)" & vbCrLf & "จัดซื้อเกินเป้า : น้ำหนักที่ขออนุมัติซื้อคืน ต้องน้อยกว่าหรือเท่ากับจำนวน คงเหลือซื้อคืนได้ (เป้ารวม และ เป้ารายสูงสุด)")> _
    <XafDisplayName("สามารถบันทึกข้อมูลได้")> _
    Public Property CanSave As Boolean
        Get
            Return fCanSave
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("CanSave", fCanSave, value)
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
End Class
