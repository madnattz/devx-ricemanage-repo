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
Public Class QualityAudit
    Inherits BaseObject
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        CheckResults = PublicEnum.CheckResults.Pending
        Me.CheckResults = PublicEnum.AuditCheckResults.Pending
        Me.RegDate = Date.Today
        DataOwner = GetCurrentSite()
    End Sub

    Protected Overrides Sub OnSaving()
        Me.LastUodateDate = Date.Now
        Me.LastUpdateBy = CType(SecuritySystem.CurrentUser, User).DisplayName
        If Me.fSampleNo = "" Then
            GenerateRunningNo()
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

    Public Sub GenerateRunningNo()
        Dim prefix As String = ""
        Dim _year As String = Right(Date.Now.Year + 543, 2)
        Dim _stepNo As String = String.Format("{0:D2}", Me.QualityAuditStep.StepNo)

        prefix = _year & "-" & _stepNo

        Me.fSampleNo = String.Format("{0}-{1:D4}", prefix, DistributedIdGeneratorHelper.Generate(Me.Session.DataLayer, Me.GetType().FullName, prefix))

    End Sub

    '<Persistent("SampleNo")> _
    'Dim fSampleNo As String
    '<PersistentAlias("fSampleNo")> _
    '<Size(50)> _
    'Public ReadOnly Property SampleNo() As String
    '    Get
    '        Return fSampleNo
    '    End Get
    'End Property

    Dim fSampleNo As String
    <XafDisplayName("ทะเบียนตรวจสอบ")> _
    Public Property SampleNo() As String
        Get
            Return fSampleNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SampleNo", fSampleNo, value)
        End Set
    End Property

    Dim fSampleSource As String
    <RuleRequiredField(DefaultContexts.Save, TargetCriteria:="NOT QualityAuditStep.StepName LIKE '%จัดซื้อ%'")> _
    Public Property SampleSource() As String
        Get
            Return fSampleSource
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SampleSource", fSampleSource, value)
        End Set
    End Property
    Dim fPlanFarmer As PlanFarmer
    <ImmediatePostData()> _
    <RuleRequiredField(DefaultContexts.Save, TargetCriteria:="QualityAuditStep.StepName LIKE '%จัดซื้อ%'")> _
    Public Property PlanFarmer() As PlanFarmer
        Get
            Return fPlanFarmer
        End Get
        Set(ByVal value As PlanFarmer)
            SetPropertyValue(Of PlanFarmer)("PlanFarmer", fPlanFarmer, value)
            Try

                fPlant = value.PlanFarmerGroup.MainPlan.Plant
                fSeedStatus = Session.FindObject(Of SeedStatus)(CriteriaOperator.Parse("[SeedStatusName] Like '%ซื้อ%'"))
                fSeedType = value.PlanFarmerGroup.MainPlan.SeedType
                fSeedClass = value.PlanFarmerGroup.MainPlan.SeedClass
                fSeason = value.PlanFarmerGroup.MainPlan.Season
                fSeedYear = value.PlanFarmerGroup.MainPlan.SeedYear
                'HarvestDate = value.

                OnChanged("Plant")
                OnChanged("SeedStatus")
                OnChanged("SeedType")
                OnChanged("SeedClass")
                OnChanged("Season")
                OnChanged("SeedYear")

            Catch ex As Exception

            End Try
        End Set
    End Property

    Dim fCarNo As String
    <XafDisplayName("ทะเบียนรถ")> _
    <RuleRequiredField(DefaultContexts.Save, TargetCriteria:="QualityAuditStep.StepName LIKE '%จัดซื้อ%'")> _
    Public Property CarNo() As String
        Get
            Return fCarNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("CarNo", fCarNo, value)
        End Set
    End Property

    Public ReadOnly Property SampleSourceAll As Object
        Get
            Dim ret As Object = Nothing
            Try
                If fQualityAuditStep.StepName.Contains("จัดซื้อ") Then
                    ret = fPlanFarmer
                Else
                    ret = fSampleSource
                End If
            Catch ex As Exception

            End Try
            Return ret
        End Get
    End Property

    Public ReadOnly Property SampleSourceLookUp As String
        Get
            Return fSampleNo & " ตัวอย่างที่ " & ItemNo
        End Get
    End Property

    Dim fItemNo As Integer
    <RuleValueComparison(DefaultContexts.Save, ValueComparisonType.GreaterThan, 0)> _
    Public Property ItemNo() As Integer
        Get
            Return fItemNo
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ItemNo", fItemNo, value)
        End Set
    End Property

    Dim fSeedWeight As Double
    <RuleValueComparison(DefaultContexts.Save, ValueComparisonType.GreaterThan, 0)> _
    Public Property SeedWeight() As Double
        Get
            Return fSeedWeight
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("SeedWeight", fSeedWeight, value)
        End Set
    End Property

    <VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    Public ReadOnly Property PassSeedWeight() As Double
        Get
            If CheckResults = PublicEnum.CheckResults.Pass Then
                Return fSeedWeight
            Else
                Return 0
            End If

        End Get
    End Property

    Dim fBags As Integer
    Public Property Bags() As Integer
        Get
            Return fBags
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Bags", fBags, value)
        End Set
    End Property
    Dim fRegDate As DateTime
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property RegDate() As DateTime
        Get
            Return fRegDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("RegDate", fRegDate, value)
        End Set
    End Property

    Dim fGetSampleDate As DateTime
    <RuleRequiredField(DefaultContexts.Save)> _
    <XafDisplayName("วันที่เก็บตัวอย่าง")> _
    Public Property GetSampleDate() As DateTime
        Get
            Return fGetSampleDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("GetSampleDate", fGetSampleDate, value)
        End Set
    End Property

    Dim fPackageType As PublicEnum.PackageType
    <RuleRequiredField(DefaultContexts.Save)> _
    <XafDisplayName("ลักษณะการบรรจุ")> _
    Public Property PackageType() As PublicEnum.PackageType
        Get
            Return fPackageType
        End Get
        Set(ByVal value As PublicEnum.PackageType)
            SetPropertyValue(Of PublicEnum.PackageType)("PackageType", fPackageType, value)
        End Set
    End Property

    Dim fHarvestDate As DateTime
    '<RuleRequiredField(DefaultContexts.Save)> _
    <XafDisplayName("วันที่เก็บเกี่ยว")> _
    Public Property HarvestDate() As DateTime
        Get
            Return fHarvestDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("HarvestDate", fHarvestDate, value)
        End Set
    End Property
    Dim fPlant As Plant
    <RuleRequiredField(DefaultContexts.Save)> _
    <ImmediatePostData()> _
    Public Property Plant() As Plant
        Get
            Return fPlant
        End Get
        Set(ByVal value As Plant)
            SetPropertyValue(Of Plant)("Plant", fPlant, value)
            If Not IsLoading AndAlso Not IsSaving AndAlso QualityAuditStep.StepName.Contains("รักษา") Then
                SeedWeight = GetSeedWeight()
                OnChanged("SeedWeight")
            End If
        End Set
    End Property
    Dim fSeedStatus As SeedStatus
    <RuleRequiredField(DefaultContexts.Save)> _
    <ImmediatePostData()> _
    Public Property SeedStatus() As SeedStatus
        Get
            Return fSeedStatus
        End Get
        Set(ByVal value As SeedStatus)
            SetPropertyValue(Of SeedStatus)("SeedStatus", fSeedStatus, value)
            If Not IsLoading AndAlso Not IsSaving AndAlso QualityAuditStep.StepName.Contains("รักษา") Then
                SeedWeight = GetSeedWeight()
                OnChanged("SeedWeight")
            End If
        End Set
    End Property
    Dim fSeedType As SeedType
    <RuleRequiredField(DefaultContexts.Save)> _
    <DataSourceProperty("Plant.SeedTypes")> _
    <ImmediatePostData()> _
    Public Property SeedType() As SeedType
        Get
            Return fSeedType
        End Get
        Set(ByVal value As SeedType)
            SetPropertyValue(Of SeedType)("SeedType", fSeedType, value)
            If Not IsLoading AndAlso Not IsSaving AndAlso QualityAuditStep.StepName.Contains("รักษา") Then
                SeedWeight = GetSeedWeight()
                OnChanged("SeedWeight")
            End If
        End Set
    End Property
    Dim fSeedClass As SeedClass
    <RuleRequiredField(DefaultContexts.Save)> _
    <ImmediatePostData()> _
    Public Property SeedClass() As SeedClass
        Get
            Return fSeedClass
        End Get
        Set(ByVal value As SeedClass)
            SetPropertyValue(Of SeedClass)("SeedClass", fSeedClass, value)
            If Not IsLoading AndAlso Not IsSaving AndAlso QualityAuditStep.StepName.Contains("รักษา") Then
                SeedWeight = GetSeedWeight()
                OnChanged("SeedWeight")
            End If
        End Set
    End Property
    Dim fSeason As Season
    <ImmediatePostData()> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property Season() As Season
        Get
            Return fSeason
        End Get
        Set(ByVal value As Season)
            SetPropertyValue(Of Season)("Season", fSeason, value)
            If Not IsLoading AndAlso Not IsSaving AndAlso QualityAuditStep.StepName.Contains("รักษา") Then
                SeedWeight = GetSeedWeight()
                OnChanged("SeedWeight")
            End If
        End Set
    End Property
    Dim fSeedYear As String
    <ImmediatePostData()> _
    <RuleRequiredField(DefaultContexts.Save)> _
    <Size(4)> _
    Public Property SeedYear() As String
        Get
            Return fSeedYear
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SeedYear", fSeedYear, value)
            If Not IsLoading AndAlso Not IsSaving AndAlso QualityAuditStep.StepName.Contains("รักษา") Then
                SeedWeight = GetSeedWeight()
                OnChanged("SeedWeight")
            End If
        End Set
    End Property

    Dim fLotNo As String
    <ImmediatePostData()> _
    Public Property LotNo() As String
        Get
            Return fLotNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("LotNo", fLotNo, value)
            If Not IsLoading AndAlso Not IsSaving AndAlso QualityAuditStep.StepName.Contains("รักษา") Then
                SeedWeight = GetSeedWeight()
                OnChanged("SeedWeight")
            End If
        End Set
    End Property

    Public Function GetSeedWeight() As Double
        Try
            If QualityAuditStep.StepName.Contains("รักษา") Then
                Dim crt As String = "Plant=? and SeedStatus=? and SeedType=? and SeedClass=? and Season=? and SeedYear=? and LotNo=?"
                Dim objSeedProduct As SeedProduct = Session.FindObject(Of SeedProduct)(CriteriaOperator.Parse(crt, Plant, SeedStatus, SeedType, SeedClass, Season, SeedYear, LotNo))
                If Not objSeedProduct Is Nothing Then
                    Return objSeedProduct.TotalStockAmount
                Else
                    Return 0
                End If
            Else
                Return 0
            End If
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Dim fQualityAuditStep As QualityAuditStep
    <RuleRequiredField(DefaultContexts.Save)> _
    <ImmediatePostData()> _
    Public Property QualityAuditStep() As QualityAuditStep
        Get
            Return fQualityAuditStep
        End Get
        Set(ByVal value As QualityAuditStep)
            SetPropertyValue(Of QualityAuditStep)("QualityAuditStep", fQualityAuditStep, value)
            Try
                If value.StepName.Contains("ซื้อ") Then
                    SampleSource = Nothing
                    OnChanged("SampleSource")
                Else
                    PlanFarmer = Nothing
                    OnChanged("PlanFarmer")
                End If
            Catch ex As Exception

            End Try
        End Set
    End Property
    Dim fLabDate As DateTime
    Public Property LabDate() As DateTime
        Get
            Return fLabDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("LabDate", fLabDate, value)
        End Set
    End Property

    Dim fLabDateMonth As String
    <VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    Public ReadOnly Property LabDateMonth() As String
        Get
            Return fLabDate.Month
        End Get
    End Property

    Dim fLabDateYear As String
    <VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    Public ReadOnly Property LabDateYear() As String
        Get
            Return (fLabDate.Year + 543).ToString
        End Get
    End Property

    Dim fWet As String
    Public Property Wet() As String
        Get
            Return fWet
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Wet", fWet, value)
        End Set
    End Property
    Dim fPureSeed As String
    Public Property PureSeed() As String
        Get
            Return fPureSeed
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PureSeed", fPureSeed, value)
        End Set
    End Property
    Dim fCompound As String
    Public Property Compound() As String
        Get
            Return fCompound
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Compound", fCompound, value)
        End Set
    End Property
    Dim fOtherRiceSeed As String
    Public Property OtherRiceSeed() As String
        Get
            Return fOtherRiceSeed
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("OtherRiceSeed", fOtherRiceSeed, value)
        End Set
    End Property
    Dim fStickySeed As String
    Public Property StickySeed() As String
        Get
            Return fStickySeed
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("StickySeed", fStickySeed, value)
        End Set
    End Property
    Dim fGrow As String
    Public Property Grow() As String
        Get
            Return fGrow
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Grow", fGrow, value)
        End Set
    End Property
    Dim fStrong As String
    Public Property Strong() As String
        Get
            Return fStrong
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Strong", fStrong, value)
        End Set
    End Property
    Dim fKOHTest As String
    Public Property KOHTest() As String
        Get
            Return fKOHTest
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("KOHTest", fKOHTest, value)
        End Set
    End Property
    Dim fIodineTest As String
    Public Property IodineTest() As String
        Get
            Return fIodineTest
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("IodineTest", fIodineTest, value)
        End Set
    End Property
    Dim fAATest As String
    <XafDisplayName("เร่งอายุ(AA)(%)")> _
    Public Property AATest() As String
        Get
            Return fAATest
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AATest", fAATest, value)
        End Set
    End Property
    Dim fSGRTest As String
    <XafDisplayName("อัตรการเจริญเติบโตของต้นอ่อน(SGR)(%)")> _
    Public Property SGRTest() As String
        Get
            Return fSGRTest
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SGRTest", fSGRTest, value)
        End Set
    End Property
    Dim fGITest As String
    <XafDisplayName("ดัชนีความงอกของเมล็ดพันธุ์(GI)(%)")> _
    Public Property GITest() As String
        Get
            Return fGITest
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("GITest", fGITest, value)
        End Set
    End Property
    Dim fInsect As String
    Public Property Insect() As String
        Get
            Return fInsect
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Insect", fInsect, value)
        End Set
    End Property
    Dim fSampleBy As String
    Public Property SampleBy() As String
        Get
            Return fSampleBy
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SampleBy", fSampleBy, value)
        End Set
    End Property
    Dim fTestBy As String
    Public Property TestBy() As String
        Get
            Return fTestBy
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("TestBy", fTestBy, value)
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
    Dim fOtherSeed As String
    Public Property OtherSeed() As String
        Get
            Return fOtherSeed
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("OtherSeed", fOtherSeed, value)
        End Set
    End Property
    Dim fGreenSeed As String
    Public Property GreenSeed() As String
        Get
            Return fGreenSeed
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("GreenSeed", fGreenSeed, value)
        End Set
    End Property
    Dim fDiseaseSeed As String
    Public Property DiseaseSeed() As String
        Get
            Return fDiseaseSeed
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("DiseaseSeed", fDiseaseSeed, value)
        End Set
    End Property
    Dim fFloatSeed As String
    Public Property FloatSeed() As String
        Get
            Return fFloatSeed
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FloatSeed", fFloatSeed, value)
        End Set
    End Property
    Dim fRedSeed As String
    Public Property RedSeed() As String
        Get
            Return fRedSeed
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("RedSeed", fRedSeed, value)
        End Set
    End Property

    Dim fWeightPerVolume As String
    <XafDisplayName("น้ำหนักต่อปริมาตร (กรัม/ลิตร)")> _
    Public Property WeightPerVolume() As String
        Get
            Return fWeightPerVolume
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("WeightPerVolume", fWeightPerVolume, value)
        End Set
    End Property

    Dim fCheckResults As PublicEnum.AuditCheckResults
    Public Property CheckResults() As PublicEnum.AuditCheckResults
        Get
            Return fCheckResults
        End Get
        Set(ByVal value As PublicEnum.AuditCheckResults)
            SetPropertyValue(Of PublicEnum.AuditCheckResults)("CheckResults", fCheckResults, value)
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

    Private _auditTrail As XPCollection(Of AuditDataItemPersistent)
    <XafDisplayName("ประวัติการแก้ไขข้อมูล")> _
    Public ReadOnly Property AuditTrail() As XPCollection(Of AuditDataItemPersistent)
        Get
            If _auditTrail Is Nothing Then
                _auditTrail = AuditedObjectWeakReference.GetAuditTrail(Session, Me)
            End If
            Return _auditTrail
        End Get
    End Property

End Class
