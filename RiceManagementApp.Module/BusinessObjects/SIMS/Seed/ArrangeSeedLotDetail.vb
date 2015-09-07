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

'<ImageName("BO_Contact")> _
'<DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")> _
'<DefaultListViewOptions(MasterDetailMode.ListViewOnly, False, NewItemRowPosition.None)> _
'<Persistent("DatabaseTableName")> _
<DefaultClassOptions(), XafDisplayName("ข้อมูลซื้อคืนเมล็ดพันธู์")> _
<RuleCombinationOfPropertiesIsUnique("ArrangeSeedLot,BuySeedWeight", CustomMessageTemplate:="มีรายการจัดซื้อคืนนี้แล้ว กรุณาเลือกรายการอื่น")> _
Public Class ArrangeSeedLotDetail ' Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()

    End Sub

    Protected Overrides Sub OnSaving()
        MyBase.OnSaving()
        If fArrangeSeedLot IsNot Nothing Then
            fArrangeSeedLot.UpdateTotalQuantity(True)
            fArrangeSeedLot.UpdatefAvgCostPerUnit(True)
        End If
    End Sub

    Protected Overrides Sub OnDeleting()
        MyBase.OnDeleting()
        If fArrangeSeedLot IsNot Nothing Then
            fArrangeSeedLot.UpdateTotalQuantity(True)
            fArrangeSeedLot.UpdatefAvgCostPerUnit(True)
        End If
    End Sub

    Protected Overrides Sub OnDeleted()
        MyBase.OnDeleted()
        If fArrangeSeedLot IsNot Nothing Then
            fArrangeSeedLot.UpdateTotalQuantity(True)
            fArrangeSeedLot.UpdatefAvgCostPerUnit(True)
        End If
    End Sub

    Private fAvailableBuySeed As XPCollection(Of BuySeed)
    <Browsable(False)> _
    Public ReadOnly Property AvailableBuySeed() As XPCollection(Of BuySeed)
        Get
            If fAvailableBuySeed Is Nothing Then
                ' Retrieve all Accessory objects 
                fAvailableBuySeed = New XPCollection(Of BuySeed)(Session)
                ' Filter the retrieved collection according to current conditions 
                RefreshAvailable()
            End If
            ' Return the filtered collection of Accessory objects 
            Return fAvailableBuySeed
        End Get
    End Property
    Private Sub RefreshAvailable()
        If fAvailableBuySeed Is Nothing Then
            Return
        End If
        fAvailableBuySeed.Criteria = CriteriaOperator.Parse("IsSetLot = false")
        'BuySeed.BuyFarmer.PlanFarmer.FullName
    End Sub

    'Dim fBuySeed As BuySeed
    ''==== เลือก รายชื่อเกษตร อ้างอิงจาก ระบบการซื้อคืน =====
    '<XafDisplayName("ชื่อ-นามสกุล")> _
    '<ImmediatePostData> _
    '<Index(0)> _
    '<RuleRequiredField(DefaultContexts.Save)> _
    '  Public Property BuySeed() As BuySeed
    '    Get
    '        Return fBuySeed

    '    End Get
    '    Set(ByVal value As BuySeed)
    '        SetPropertyValue(Of BuySeed)("BuySeed", fBuySeed, value)
    '    End Set
    'End Property

    Dim fBuySeedWeight As BuySeedWeight
    <XafDisplayName("ชื่อ-นามสกุล")> _
    <ImmediatePostData> _
    <Index(0)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property BuySeedWeight() As BuySeedWeight
        Get
            Return fBuySeedWeight
            'BuySeedWeight.BuySeed.BuyFarmer.PlanFarmer.FullName
        End Get
        Set(ByVal value As BuySeedWeight)
            SetPropertyValue(Of BuySeedWeight)("BuySeedWeight", fBuySeedWeight, value)
        End Set
    End Property

    Dim fQuantity As Double
    <XafDisplayName("น้ำหนัก(กก.)")> _
    <ImmediatePostData> _
    <Index(1)> _
    <Appearance("DisQTY", Enabled:=False, context:="DetailView")> _
    Public Property Quantity() As Double
        Get
            If BuySeedWeight IsNot Nothing Then
                Return BuySeedWeight.TotalWeight
            Else
                Return 0
            End If
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("Quantity", fQuantity, value)
        End Set
    End Property

    Dim fPricePerUnit As Double
    <XafDisplayName("ราคา(บาท/กก.)")> _
    <ImmediatePostData> _
    <Index(2)> _
    <Appearance("DisPerUnit", Enabled:=False, context:="DetailView")> _
    Public Property PricePerUnit() As Double
        Get
            'If BuySeedWeight IsNot Nothing Then
            '    'Return 0 'BuySeed.PricePerUnit
            '    Dim avgPricePerUnit As Double = 0
            '    For Each item As BuySeedWeight In BuySeed.BuySeedWeights
            '        avgPricePerUnit += item.PricePerUnit
            '    Next
            '    avgPricePerUnit = avgPricePerUnit / BuySeed.BuySeedWeights.Count
            '    Return avgPricePerUnit
            'Else
            '    Return 0
            'End If
            If BuySeedWeight IsNot Nothing Then
                Return BuySeedWeight.PricePerUnit
            Else
                Return 0
            End If
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("PricePerUnit", fPricePerUnit, value)
        End Set
    End Property

#Region "========Association========="
    Dim fArrangeSeedLot As ArrangeSeedLot
    <Association("ArrangeSeedLotDetailReferencesArrangeSeedLot")> _
    <VisibleInDetailView(False)> _
    Public Property ArrangeSeedLot() As ArrangeSeedLot
        Get
            Return fArrangeSeedLot
        End Get
        Set(ByVal value As ArrangeSeedLot)
            Dim oldArrangeSeedLot As ArrangeSeedLot = fArrangeSeedLot
            SetPropertyValue(Of ArrangeSeedLot)("ArrangeSeedLot", fArrangeSeedLot, value)
            If (Not IsLoading) AndAlso (Not IsSaving) AndAlso oldArrangeSeedLot IsNot fBuySeedWeight Then
                If (oldArrangeSeedLot IsNot Nothing) Then
                    oldArrangeSeedLot = oldArrangeSeedLot
                Else
                    oldArrangeSeedLot = fArrangeSeedLot
                End If
                oldArrangeSeedLot.UpdateTotalQuantity(True)
                oldArrangeSeedLot.UpdatefAvgCostPerUnit(True)
            End If
        End Set
    End Property
#End Region
End Class
