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
Public Class CheckFarm ' Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
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

    Dim fPlanFarmerFarm As PlanFarmerFarm
    <VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    Public Property PlanFarmerFarm() As PlanFarmerFarm
        Get
            Return fPlanFarmerFarm
        End Get
        Set(ByVal value As PlanFarmerFarm)
            SetPropertyValue(Of PlanFarmerFarm)("PlanFarmerFarm", fPlanFarmerFarm, value)

        End Set
    End Property

    'Dim fPlanFarmer As PlanFarmer
    <XafDisplayName("เป้าการผลิต")> _
    Public ReadOnly Property MainPlan() As MainPlan
        Get
            If PlanFarmerFarm IsNot Nothing Then
                Return PlanFarmerFarm.PlanFarmer.PlanFarmerGroup.MainPlan
            End If
        End Get
    End Property

    <XafDisplayName("เกษตรกร")> _
    Public ReadOnly Property PlanFarmer() As PlanFarmer
        Get
            If PlanFarmerFarm IsNot Nothing Then
                Return PlanFarmerFarm.PlanFarmer
            End If
        End Get
    End Property

    Dim fCheckDate As DateTime
    Public Property CheckDate() As DateTime
        Get
            Return fCheckDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("CheckDate", fCheckDate, value)
        End Set
    End Property
    Dim fCheckPlant As Integer
    Public Property CheckPlant() As Integer
        Get
            Return fCheckPlant
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CheckPlant", fCheckPlant, value)
        End Set
    End Property
    Dim fCheckPeriod As PublicEnum.CheckPeriod
    Public Property CheckPeriod() As PublicEnum.CheckPeriod
        Get
            Return fCheckPeriod
        End Get
        Set(ByVal value As PublicEnum.CheckPeriod)
            SetPropertyValue(Of Integer)("CheckPeriod", fCheckPeriod, value)
        End Set
    End Property
    'Dim fHowGrow As GrowType
    'Public Property HowGrow() As GrowType
    '    Get
    '        Return fHowGrow
    '    End Get
    '    Set(ByVal value As GrowType)
    '        SetPropertyValue(Of GrowType)("HowGrow", fHowGrow, value)
    '    End Set
    'End Property

    <VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    Public ReadOnly Property GrowTypeString As String
        Get
            Dim ret As String = ""
            Dim arrGrowType As New List(Of String)

            For i As Integer = 0 To PlanFarmerFarm.PlanFarmerGrows.Count - 1
                arrGrowType.Add(PlanFarmerFarm.PlanFarmerGrows(i).GrowType.GrowType.GrowName)
            Next
            arrGrowType = arrGrowType.Distinct.ToList()
            For i As Integer = 0 To arrGrowType.Count - 1
                If i <> arrGrowType.Count - 1 Then
                    ret &= arrGrowType(i) & ","
                Else
                    ret &= arrGrowType(i)
                End If
            Next
            Return ret
        End Get
    End Property

    'Dim fGrowAreaSize As Integer
    <XafDisplayName("พื้นที่ปลูก (ไร่)")> _
    Public ReadOnly Property GrowAreaSize() As Integer
        Get
            Try
                If Not PlanFarmerFarm Is Nothing Then
                    Return PlanFarmerFarm.TotalGrowArea
                Else
                    Return 0
                End If
            Catch ex As Exception
                Return 0
            End Try
            
        End Get
        'Set(value As Integer)
        '    SetPropertyValue(Of Integer)("GrowAreaSize", fGrowAreaSize, value)
        'End Set
    End Property

    Dim fCheckArea As Integer
    <XafDisplayName("พื้นที่ตรวจจริง (ไร่)")> _
    <ImmediatePostData()> _
    Public Property CheckArea() As Integer
        Get
            'If fCheckArea > GrowAreaSize - DamageArea Then
            '    fCheckArea = GrowAreaSize - DamageArea
            '    OnChanged("CheckArea")
            'End If
            Return fCheckArea
        End Get
        Set(ByVal value As Integer)
            
            SetPropertyValue(Of Integer)("CheckArea", fCheckArea, value)
        End Set
    End Property

    Dim fDamageArea As Integer
    <XafDisplayName("พื้นที่เสียหายก่อนตรวจ (ไร่)")> _
    <ImmediatePostData()> _
    Public Property DamageArea() As Integer
        Get
            Return fDamageArea
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("DamageArea", fDamageArea, value)
            If Not IsLoading AndAlso Not IsSaving Then
                CheckArea = GrowAreaSize - value
                OnChanged("CheckArea")
            End If
        End Set
    End Property

    Dim fPassAreaSize As Integer
    <XafDisplayName("ผ่านมาตรฐาน (ไร่)")> _
    Public Property PassAreaSize As Integer
        Get
            Return fPassAreaSize
        End Get
        Set(value As Integer)
            SetPropertyValue(Of Integer)("PassAreaSize", fPassAreaSize, value)
        End Set
    End Property

    <XafDisplayName("ไม่ผ่านมาตรฐาน (ไร่)")> _
    Public ReadOnly Property FailAreaSize As Integer
        Get
            Return CheckArea - PassAreaSize
        End Get
    End Property

    Dim fRandom1 As Integer
    <Browsable(False)> _
    Public Property Random1() As Integer
        Get
            Return fRandom1
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Random1", fRandom1, value)
        End Set
    End Property
    Dim fRandom2 As Integer
    <Browsable(False)> _
    Public Property Random2() As Integer
        Get
            Return fRandom2
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Random2", fRandom2, value)
        End Set
    End Property
    Dim fRandom3 As Integer
    <Browsable(False)> _
    Public Property Random3() As Integer
        Get
            Return fRandom3
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Random3", fRandom3, value)
        End Set
    End Property

    Dim fRandom4 As Integer
    <XafDisplayName("จุดที่ 4")> _
    <Browsable(False)> _
    Public Property Random4() As Integer
        Get
            Return fRandom4
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Random4", fRandom4, value)
        End Set
    End Property

    Dim fRandom5 As Integer
    <XafDisplayName("จุดที่ 5")> _
    <Browsable(False)> _
    Public Property Random5() As Integer
        Get
            Return fRandom5
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Random5", fRandom5, value)
        End Set
    End Property

    Dim fOtherSeedValue As Double
    <XafDisplayName("จำนวนพันธุ์ปนที่พบ (ต้น/จุดตรวจทั้งหมด)")> _
    Public Property OtherSeedValue() As Double
        Get
            Return fOtherSeedValue
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("OtherSeedValue", fOtherSeedValue, value)
        End Set
    End Property

    Dim fRedSeedValue As Double
    <XafDisplayName("จำนวนข้าวแดงที่พบ (ต้น/จุดตรวจทั้งหมด)")> _
    Public Property RedSeedValue() As Double
        Get
            Return fRedSeedValue
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("RedSeedValue", fRedSeedValue, value)
        End Set
    End Property

    Dim fDisease As Integer
    Public Property Disease() As PublicEnum.Disease
        Get
            Return fDisease
        End Get
        Set(ByVal value As PublicEnum.Disease)
            SetPropertyValue(Of Integer)("Disease", fDisease, value)
        End Set
    End Property
    Dim fBug As Integer
    Public Property Bug() As PublicEnum.Bug
        Get
            Return fBug
        End Get
        Set(ByVal value As PublicEnum.Bug)
            SetPropertyValue(Of Integer)("Bug", fBug, value)
        End Set
    End Property
    Dim fWeed As Integer
    Public Property Weed() As PublicEnum.Weed
        Get
            Return fWeed
        End Get
        Set(ByVal value As PublicEnum.Weed)
            SetPropertyValue(Of Integer)("Weed", fWeed, value)
        End Set
    End Property
    Dim fBroken As Integer
    Public Property Broken() As PublicEnum.Broken
        Get
            Return fBroken
        End Get
        Set(ByVal value As PublicEnum.Broken)
            SetPropertyValue(Of Integer)("Broken", fBroken, value)
        End Set
    End Property
    Dim fRemark As String
    <XafDisplayName("หมายเหตุ")> _
    Public Property Remark() As String
        Get
            Return fRemark
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Remark", fRemark, value)
        End Set
    End Property
    Dim fDistance As Integer
    <XafDisplayName("ระยะห่างระหว่างแปลง")> _
    Public Property Distance() As PublicEnum.ChooseResults
        Get
            Return fDistance
        End Get
        Set(ByVal value As PublicEnum.ChooseResults)
            SetPropertyValue(Of PublicEnum.ChooseResults)("Distance", fDistance, value)
        End Set
    End Property
    Dim fCheckResults As Integer
    <ImmediatePostData()> _
    Public Property CheckResults() As PublicEnum.CheckResults
        Get
            Return fCheckResults
        End Get
        Set(ByVal value As PublicEnum.CheckResults)
            SetPropertyValue(Of Integer)("CheckResults", fCheckResults, value)
            If Not IsLoading AndAlso Not IsSaving Then
                If value = PublicEnum.CheckResults.Fail2 Then
                    DamageArea = GrowAreaSize
                    'CheckPeriod = 0
                    Bug = 0
                    Disease = 0
                    Weed = 0
                    Broken = 0
                    OnChanged("DamageArea")
                End If
            End If
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

#Region "สาเหตุที่ไม่ผ่าน"
    Dim fIsOtherSeed As Boolean
    <XafDisplayName("พันธุ์ปน")> _
    Public Property IsOtherSeed() As Boolean
        Get
            Return fIsOtherSeed
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("IsOtherSeed", fIsOtherSeed, value)
        End Set
    End Property
    Dim fIsRedSeed As Boolean
    <XafDisplayName("ข้าวแดง")> _
    Public Property IsRedSeed() As Boolean
        Get
            Return fIsRedSeed
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("IsRedSeed", fIsRedSeed, value)
        End Set
    End Property
    Dim fIsDown As Boolean
    <XafDisplayName("ข้าวล้ม")> _
    Public Property IsDown() As Boolean
        Get
            Return fIsDown
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("IsDown", fIsDown, value)
        End Set
    End Property
    Dim fIsWeed As Boolean
    <XafDisplayName("วัชพืช")> _
    Public Property IsWeed() As Boolean
        Get
            Return fIsWeed
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("IsWeed", fIsWeed, value)
        End Set
    End Property
    Dim fIsDisease As Boolean
    <XafDisplayName("โรค")> _
    Public Property IsDisease() As Boolean
        Get
            Return fIsDisease
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("IsDisease", fIsDisease, value)
        End Set
    End Property
    Dim fIsBug As Boolean
    <XafDisplayName("แมลง")> _
    Public Property IsBug() As Boolean
        Get
            Return fIsBug
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("IsBug", fIsBug, value)
        End Set
    End Property
    Dim fIsFlood As Boolean
    <XafDisplayName("น้ำท่วม")> _
    Public Property IsFlood() As Boolean
        Get
            Return fIsFlood
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("IsFlood", fIsFlood, value)
        End Set
    End Property
    Dim fIsDrought As Boolean
    <XafDisplayName("แล้ง/ฝนทิ้งช่วง")> _
    Public Property IsDrought() As Boolean
        Get
            Return fIsDrought
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("IsDrought", fIsDrought, value)
        End Set
    End Property

    <VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    Public ReadOnly Property FailReason As String
        Get
            Dim ret As String = ""
            If IsOtherSeed Then
                ret &= ",1"
            End If
            If IsRedSeed Then
                ret &= ",2"
            End If
            If IsWeed Then
                ret &= ",3"
            End If
            If IsDown Then
                ret &= ",4"
            End If
            If IsDisease Then
                ret &= ",5"
            End If
            If IsBug Then
                ret &= ",6"
            End If
            If IsFlood Then
                ret &= ",7"
            End If
            If IsDrought Then
                ret &= ",8"
            End If

            If ret.Length > 0 Then
                ret = ret.Remove(0, 1)
            End If

            Return ret
        End Get
    End Property

#End Region

End Class
