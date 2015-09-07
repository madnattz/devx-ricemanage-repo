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

'<ImageName("BO_Contact")> _
'<DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")> _
'<DefaultListViewOptions(MasterDetailMode.ListViewOnly, False, NewItemRowPosition.None)> _
'<Persistent("DatabaseTableName")> _
<DefaultClassOptions()> _
Public Class BuySeedWeight ' Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        DataOwner = GetCurrentSite()
        ' Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).
    End Sub

    Protected Overrides Sub OnSaving()
        Me.LastUpdateDate = Date.Now
        Me.LastUpdateBy = CType(SecuritySystem.CurrentUser, User).DisplayName

        If fBuySeed IsNot Nothing Then
            'fBuySeed.UpdateSumAllWeight(True)
            fBuySeed.UpdateSumAmount(True)
            fBuySeed.UpdateSumWeight(True)
        End If

        If Me.DataOwner Is Nothing Then
            DataOwner = GetCurrentSite()
        End If

        MyBase.OnSaving()
    End Sub

    Protected Overrides Sub OnDeleting()
        MyBase.OnDeleting()
        If fBuySeed IsNot Nothing Then
            'fBuySeed.UpdateSumAllWeight(True)
            fBuySeed.UpdateSumAmount(True)
            fBuySeed.UpdateSumWeight(True)
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

    Dim fBuySeed As BuySeed
    <Association("BuySeedWeightReferencesBuySeed")> _
    Public Property BuySeed() As BuySeed
        Get
            Return fBuySeed
        End Get
        Set(ByVal value As BuySeed)
            Dim oldBuySeed As BuySeed = fBuySeed
            SetPropertyValue(Of BuySeed)("BuySeed", fBuySeed, value)
            If (Not IsLoading) AndAlso (Not IsSaving) AndAlso oldBuySeed IsNot fBuySeed Then
                If (oldBuySeed IsNot Nothing) Then
                    oldBuySeed = oldBuySeed
                Else
                    oldBuySeed = fBuySeed
                End If
                'oldBuySeed.UpdateSumAllWeight(True)
                'oldBuySeed.UpdateSumCarWeight(True)
                'oldBuySeed.UpdatefSumBags(True)

                oldBuySeed.UpdateSumWeight(True)
                oldBuySeed.UpdateSumAmount(True)
                'oldBuySeed.UpdateWetAndCompound(True)
            End If
        End Set
    End Property

    Dim fRiceAndCarWeight As Double
    '<ModelDefault("DisPlayFormat", ("{0:#,##}"))> _
    <XafDisplayName("น้ำหนักรวม (กก.)")> _
    <RuleValueComparison(DefaultContexts.Save, ValueComparisonType.GreaterThan, 0)> _
    Public Property RiceAndCarWeight() As Double
        Get
            Return fRiceAndCarWeight
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("RiceAndCarWeight", fRiceAndCarWeight, value)
            If (Not IsLoading) AndAlso (Not IsSaving) AndAlso fBuySeed IsNot Nothing Then
                fBuySeed.UpdateSumAmount(True)
                fBuySeed.UpdateSumWeight(True)
            End If
        End Set
    End Property

    Dim fCarWeight As Double
    '<ModelDefault("DisPlayFormat", ("{0:#,##}"))> _
    <XafDisplayName("น้ำหนักรถ (กก.)")> _
    <RuleValueComparison(DefaultContexts.Save, ValueComparisonType.GreaterThan, 0)> _
    Public Property CarWeight() As Double
        Get
            Return fCarWeight
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("CarWeight", fCarWeight, value)
            If (Not IsLoading) AndAlso (Not IsSaving) AndAlso fBuySeed IsNot Nothing Then
                fBuySeed.UpdateSumAmount(True)
                fBuySeed.UpdateSumWeight(True)
            End If
        End Set
    End Property

    Dim fBags As Integer
    '<ModelDefault("DisPlayFormat", ("{0:#,##}"))> _
    <XafDisplayName("จำนวนกระสอบ(ใบ)")> _
    Public Property Bags() As Integer
        Get
            Return fBags
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Bags", fBags, value)
            If (Not IsLoading) AndAlso (Not IsSaving) AndAlso fBuySeed IsNot Nothing Then
                'fBuySeed.UpdatefSumBags(True)
            End If
        End Set
    End Property

    Dim fWeightCardNo As String
    <XafDisplayName("เลขที่ใบชั่งน้ำหนัก")> _
    Public Property WeightCardNo() As String
        Get
            Return fWeightCardNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("WeightCardNo", fWeightCardNo, value)
        End Set
    End Property

    Dim fAuditSampleNo As String
    <XafDisplayName("เลขที่ผลคุณภาพ")> _
    <RuleRequiredField()> _
    <ImmediatePostData()> _
    Public Property AuditSampleNo() As String
        Get
            Return fAuditSampleNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AuditSampleNo", fAuditSampleNo, value)
            If (Not IsLoading) AndAlso (Not IsSaving) AndAlso fBuySeed IsNot Nothing Then
                'fBuySeed.UpdatefSumBags(True)
                Dim _QualityAudit As QualityAudit = Session.FindObject(Of QualityAudit)(CriteriaOperator.Parse("SampleNo=?", value))

                If _QualityAudit IsNot Nothing Then
                    Me.QualityAudit = _QualityAudit
                Else
                    Me.QualityAudit = Nothing
                    QAWet = ConvertToDouble(0)
                    QACompound = ConvertToDouble(0)
                    OnChanged("QAWet")
                    OnChanged("QACompound")
                End If


            End If
        End Set
    End Property

    '-------------------------------ย้ายมาจาก Header ---------------------------------------------
    Dim fQualityAudit As QualityAudit
    '<Association("BuySeedReferencesMainPlan")> _
    <ImmediatePostData()> _
    <XafDisplayName("ผลคุณภาพ")> _
    <DataSourceCriteria("PlanFarmer = '@this.BuySeed.BuyFarmer.PlanFarmer' and CheckResults='Pass'")> _
    Public Property QualityAudit() As QualityAudit
        Get
            Return fQualityAudit
        End Get
        Set(ByVal value As QualityAudit)
            SetPropertyValue(Of QualityAudit)("QualityAudit", fQualityAudit, value)
            'If Not IsLoading AndAlso Not IsSaving Then
            Try
                If value IsNot Nothing Then
                    Dim _plant As Plant = BuySeed.MainPlan.Plant
                    Dim _objSetting As BuyQualitySettings = Session.FindObject(Of BuyQualitySettings)(CriteriaOperator.Parse("Plant=?", _plant))
                    QAWet = ConvertToDouble(value.Wet)
                    QACompound = ConvertToDouble(value.Compound)
                    OnChanged("QAWet")
                    OnChanged("QACompound")
                    If _objSetting IsNot Nothing Then
                        Wet = CalculateWet(WeightWithoutBags, _objSetting.Wet, fQAWet)
                        Compound = CalculateCompound(WeightWithoutBags, _objSetting.Compound, fQACompound)
                        OnChanged("Wet")
                        OnChanged("Compound")

                    End If
                End If
            Catch ex As Exception

            End Try
            'End If
        End Set
    End Property

    Public Function ConvertToDouble(obj As Object) As Double
        Try
            Return Convert.ToDouble(obj)
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Dim fCompound As Double
    '<RuleValueComparison(DefaultContexts.Save, ValueComparisonType.GreaterThan, 0)> _
    '<ModelDefault("DisPlayFormat", ("{0:#,##}"))> _
    <XafDisplayName("หักสิ่งเจือปน (กก.)")> _
    Public Property Compound() As Double
        Get
            Return fCompound
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("Compound", fCompound, value)
        End Set
    End Property

    Dim fWet As Double
    '<RuleValueComparison(DefaultContexts.Save, ValueComparisonType.GreaterThan, 0)> _
    '<ModelDefault("DisPlayFormat", ("{0:#,##}"))> _
    <XafDisplayName("หักความชื้น (กก.)")> _
    Public Property Wet() As Double
        Get
            Return fWet
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("Wet", fWet, value)
        End Set
    End Property

    Dim fPricePerUnit As Double
    <ModelDefault("DisPlayFormat", ("{0:#,#0.00}"))> _
    <XafDisplayName("ราคาซื้อคืน (บาท/กก.)")> _
    <RuleValueComparison(DefaultContexts.Save, ValueComparisonType.GreaterThan, 0)> _
    Public Property PricePerUnit() As Double
        Get
            Return fPricePerUnit
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("PricePerUnit", fPricePerUnit, value)
        End Set
    End Property

    Dim fBagWeight As Double
    '<ModelDefault("DisPlayFormat", ("{0:#,##}"))> _
    <ImmediatePostData()> _
    <XafDisplayName("น้ำหนักกระสอบ (กก.)")> _
    Public Property BagWeight() As Double
        Get
            Return fBagWeight
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("BagWeight", fBagWeight, value)
            'UpdateWetAndCompound(True)
        End Set
    End Property

    Dim fQAWet As Double
    '<ModelDefault("DisPlayFormat", ("{0:#,##.##}"))> _
    <ImmediatePostData()> _
    <XafDisplayName("ความชื้น (%)")> _
    Public Property QAWet() As Double
        Get
            Return fQAWet
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("QAWet", fQAWet, value)
            If Not IsLoading AndAlso Not IsSaving Then
                Try

                    Dim _plant As Plant = BuySeed.MainPlan.Plant
                    Dim _objSetting As BuyQualitySettings = Session.FindObject(Of BuyQualitySettings)(CriteriaOperator.Parse("Plant=?", _plant))
                    If _objSetting IsNot Nothing Then
                        Wet = CalculateWet(WeightWithoutBags, _objSetting.Wet, value)
                        OnChanged("Wet")
                    End If

                Catch ex As Exception

                End Try
            End If
        End Set
    End Property

    Dim fQACompound As Double
    '<ModelDefault("DisPlayFormat", ("{0:#,##.##}"))> _
    <ImmediatePostData()> _
    <XafDisplayName("สิ่งเจือปน (%)")> _
    Public Property QACompound() As Double
        Get
            Return fQACompound
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("QACompound", fQACompound, value)
            If Not IsLoading AndAlso Not IsSaving Then
                Try

                    Dim _plant As Plant = BuySeed.MainPlan.Plant
                    Dim _objSetting As BuyQualitySettings = Session.FindObject(Of BuyQualitySettings)(CriteriaOperator.Parse("Plant=?", _plant))
                    If _objSetting IsNot Nothing Then
                        Compound = CalculateCompound(WeightWithoutBags, _objSetting.Compound, fQACompound)
                        OnChanged("Compound")
                    End If

                Catch ex As Exception

                End Try
            End If
        End Set
    End Property

    Private Function CalculateWet(totalWeight As Double, standardWet As Double, currentWet As Double) As Double
        Dim ret As Double = 0
        If currentWet < standardWet Then
            currentWet = standardWet
        End If

        ret = (currentWet - standardWet) * (15 / 1000) * totalWeight

        Return Math.Floor(ret)
    End Function

    Private Function CalculateCompound(totalWeight As Double, standardCompound As Double, currentCompound As Double) As Double
        Dim ret As Double = 0
        If currentCompound < standardCompound Then
            currentCompound = standardCompound
        End If

        ret = (currentCompound - standardCompound) * (totalWeight / 100)

        Return Math.Floor(ret)
    End Function

    '<ModelDefault("DisPlayFormat", ("{0:#,##}"))> _
    <ImmediatePostData()> _
    <XafDisplayName("น้ำหนักข้าว (กก.)")> _
    Public ReadOnly Property SumWeight() As Double
        Get
            Try
                Return RiceAndCarWeight - CarWeight

            Catch ex As Exception
                Return 0
            End Try

        End Get

    End Property

    '<ModelDefault("DisPlayFormat", ("{0:#,##}"))> _
    <ImmediatePostData()> _
    <XafDisplayName("น้ำหนักหลังหักกระสอบ (กก.)")> _
    Public ReadOnly Property WeightWithoutBags() As Double
        Get
            Try
                Return SumWeight - BagWeight
            Catch ex As Exception
                Return 0
            End Try

        End Get

    End Property

    <ModelDefault("DisPlayFormat", ("{0:#,##}"))> _
    <XafDisplayName("น้ำหนักสุทธิ (กก.)")> _
    Public ReadOnly Property TotalWeight() As Double
        Get
            Try
                Return Math.Ceiling(WeightWithoutBags - Wet - Compound)
            Catch ex As Exception
                Return 0
            End Try

        End Get
    End Property

    <ImmediatePostData()> _
    <ModelDefault("DisPlayFormat", ("{0:#,#0.00}"))> _
    <XafDisplayName("จำนวนเงิน (บาท)")> _
    Public ReadOnly Property TotalAmount() As Double
        Get
            Try
                Return TotalWeight * PricePerUnit
            Catch ex As Exception
                Return 0
            End Try

        End Get
    End Property

    '-------------------------------------------------------------------------------------------
    Dim fCarNo As String
    <XafDisplayName("ทะเบียนรถ")> _
    Public Property CarNo() As String
        Get
            Return fCarNo
        End Get
        Set(value As String)
            SetPropertyValue(Of String)("CarNo", fCarNo, value)
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
    Dim fLastUpdateDate As DateTime
    Public Property LastUpdateDate() As DateTime
        Get
            Return fLastUpdateDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("LastUpdateDate", fLastUpdateDate, value)
        End Set
    End Property

    Public ReadOnly Property GroupUntiPrice As String
        Get
            If BuySeed.BuyFarmer.PlanFarmer.Farmer.MemberID IsNot Nothing Then
                Return BuySeed.BuyFarmer.PlanFarmer.Farmer.MemberID & "-" & PricePerUnit
            Else
                Return ""
            End If
        End Get
    End Property

End Class
