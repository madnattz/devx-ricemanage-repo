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

<ImageName("BO_Product_Group")> _
<XafDisplayName("จัดล็อตเมล็ดพันธุ์")> _
<DefaultProperty("FullPlant")> _
<DefaultClassOptions()> _
Public Class FactoryPickProcess
    Inherits BaseObject
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        DataOwner = GetCurrentSite()
        Status = PublicEnum.FactoryPickProcess.Pending
    End Sub

    Protected Overrides Sub OnSaving()
        MyBase.OnSaving()
        If fFactorySeedProcess IsNot Nothing Then
            fFactorySeedProcess.UpdateSetLotAmount(True)
        End If
        If Me.DataOwner Is Nothing Then
            DataOwner = GetCurrentSite()
        End If
    End Sub

    Protected Overrides Sub OnDeleted()
        MyBase.OnDeleted()
        If fFactorySeedProcess IsNot Nothing Then
            fFactorySeedProcess.UpdateSetLotAmount(True)
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

    Dim fFactorySeedProcess As FactorySeedProcess
    <VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)> _
    <Association("FactoryPickProcess-FactorySeedProcess")> _
    Public Property FactorySeedProcess() As FactorySeedProcess
        Get
            Return fFactorySeedProcess
        End Get
        Set(ByVal value As FactorySeedProcess)

            Dim fOldFactorySeedProcess As FactorySeedProcess = fFactorySeedProcess
            SetPropertyValue(Of FactorySeedProcess)("FactorySeedProcess", fFactorySeedProcess, value)
            If (Not IsLoading) AndAlso (Not IsSaving) AndAlso fOldFactorySeedProcess IsNot fFactorySeedProcess Then
                If (fOldFactorySeedProcess IsNot Nothing) Then
                    fOldFactorySeedProcess = fOldFactorySeedProcess
                Else
                    fOldFactorySeedProcess = fFactorySeedProcess
                End If
                fOldFactorySeedProcess.UpdateSetLotAmount(True)
            End If
        End Set
    End Property

    <XafDisplayName("แหล่งที่มาเมล็ดพันธุ์ดิบ (เกษตรกร)")> _
   <Association("FactoryPickProcessDetails-FactoryPickProcess", GetType(FactoryPickProcessDetail))> _
   <DevExpress.Xpo.Aggregated> _
    Public ReadOnly Property FactoryPickProcessDetails() As XPCollection(Of FactoryPickProcessDetail)
        Get
            Return GetCollection(Of FactoryPickProcessDetail)("FactoryPickProcessDetails")
        End Get
    End Property

    'Dim fPickSeedDate As Date = Now
    '<Index(1)>
    '<XafDisplayName("วันที่จัดล็อต")> _
    '<VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)>
    '<RuleRequiredField(DefaultContexts.Save)> _
    'Public Property PickSeedDate() As Date
    '    Get
    '        Return fPickSeedDate
    '    End Get
    '    Set(ByVal value As Date)
    '        SetPropertyValue(Of Date)("PickSeedDate", fPickSeedDate, value)
    '    End Set
    'End Property

    'Dim fPlant As Plant
    '<Index(2)>
    '<XafDisplayName("พืช")> _
    '<RuleRequiredField(DefaultContexts.Save)> _
    'Public Property Plant() As Plant
    '    Get
    '        Return fPlant
    '    End Get
    '    Set(ByVal value As Plant)
    '        SetPropertyValue(Of Plant)("Plant", fPlant, value)
    '    End Set
    'End Property

    'Dim fSeedType As SeedType
    '<Index(3)>
    '<XafDisplayName("พันธุ์")> _
    '<RuleRequiredField(DefaultContexts.Save)> _
    'Public Property SeedType() As SeedType
    '    Get
    '        Return fSeedType
    '    End Get
    '    Set(ByVal value As SeedType)
    '        SetPropertyValue(Of SeedType)("SeedType", fSeedType, value)
    '    End Set
    'End Property

    'Dim fSeedClass As SeedClass
    '<Index(4)>
    '<XafDisplayName("ชั้นพันธุ์")> _
    '<RuleRequiredField(DefaultContexts.Save)> _
    'Public Property SeedClass() As SeedClass
    '    Get
    '        Return fSeedClass
    '    End Get
    '    Set(ByVal value As SeedClass)
    '        SetPropertyValue(Of SeedClass)("SeedClass", fSeedClass, value)
    '    End Set
    'End Property

    'Dim fSeason As Season
    '<Index(5)>
    '<XafDisplayName("ฤดู")> _
    '<RuleRequiredField(DefaultContexts.Save)> _
    'Public Property Season() As Season
    '    Get
    '        Return fSeason
    '    End Get
    '    Set(ByVal value As Season)
    '        SetPropertyValue(Of Season)("Season", fSeason, value)
    '    End Set
    'End Property

    'Dim fSeedYear As String
    '<Index(6)>
    '<RuleRequiredField(DefaultContexts.Save)> _
    '<ImmediatePostData()> _
    '<XafDisplayName("ปี")> _
    '<ModelDefault("PropertyEditorType", "RiceManagementApp.Module.Win.CustomYearDropdown")> _
    'Public Property SeedYear() As String
    '    Get
    '        Return fSeedYear
    '    End Get
    '    Set(ByVal value As String)
    '        SetPropertyValue(Of String)("SeedYear", fSeedYear, value)
    '    End Set
    'End Property

    'Dim fLot As String
    '<Index(7)>
    '<XafDisplayName("รุ่นที่")> _
    '<RuleRequiredField(DefaultContexts.Save)> _
    'Public Property Lot() As String
    '    Get
    '        Return fLot
    '    End Get
    '    Set(ByVal value As String)
    '        SetPropertyValue(Of String)("Lot", fLot, value)
    '    End Set
    'End Property

    Dim fLotFactory As String
    <Index(8)>
    <XafDisplayName("ล็อต(โรงงาน)")> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property LotFactory() As String
        Get
            Return fLotFactory
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("LotFactory ", fLotFactory, value)
        End Set
    End Property


    Dim fQuantity As Integer
    <Index(9)>
    <XafDisplayName("น้ำหนักเมล็ดพันธุ์ดี (กก.)")> _
    <RuleValueComparison(DefaultContexts.Save, ValueComparisonType.GreaterThan, 0)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    <ImmediatePostData()> _
    Public Property Quantity() As Integer
        Get
            Return fQuantity
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Quantity", fQuantity, value)
            If Not IsLoading AndAlso Not IsSaving Then
                Bag = value / 25
                OnChanged("Bag")
            End If
        End Set
    End Property


    Private fBag As Integer
    <Index(10)>
    <ModelDefault("DisPlayFormat", ("{0:#,#0}"))> _
    <XafDisplayName("กระสอบ (ใบ)")> _
    <ImmediatePostData> _
    Public Property Bag As Integer
        Get
            Return fBag
        End Get
        Set(value As Integer)
            SetPropertyValue(Of Integer)("Bag", fBag, value)
        End Set
    End Property

    <Persistent("PickSeedAmount")> _
    Private fPickSeedAmount As Nullable(Of Integer) = Nothing
    <Index(11)>
    <ModelDefault("DisPlayFormat", ("{0:#,#0}"))> _
    <PersistentAlias("fSeedAmount")> _
    <ModelDefault("DisPlayFormat", ("{0:#,#0}"))> _
    <XafDisplayName("น้ำหนักเมล็ดพันธุ์ดิบ รวม (กก.)")> _
    Public ReadOnly Property PickSeedAmount As Integer
        Get
            Try
                If (Not IsLoading) AndAlso (Not IsSaving) AndAlso Not fPickSeedAmount.HasValue Then
                    UpdatePickSeedAmount(False)

                End If
                Return fPickSeedAmount
            Catch ex As Exception
                Return 0
            End Try
        End Get
    End Property

    Public Sub UpdatePickSeedAmount(ByVal forceChangeEvents As Boolean)
        Try
            Dim fOldPickSeedAmount As Nullable(Of Integer) = fPickSeedAmount
            fPickSeedAmount = Convert.ToInt32(Evaluate(CriteriaOperator.Parse("FactoryPickProcessDetails.Sum(fPickSeedAmount)")))
            If forceChangeEvents Then
                OnChanged("PickSeedAmount", fOldPickSeedAmount, fPickSeedAmount)
            End If
        Catch ex As Exception
        End Try

    End Sub

    Dim fStatus As PublicEnum.FactoryPickProcess
    <Index(12)>
    <XafDisplayName("สถานะ")> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property Status() As PublicEnum.FactoryPickProcess
        Get
            Return fStatus
        End Get
        Set(ByVal value As PublicEnum.FactoryPickProcess)
            SetPropertyValue(Of PublicEnum.FactoryPickProcess)("Status", fStatus, value)
        End Set
    End Property

    Dim fFullPlant As String
    <VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    <XafDisplayName("ล็อตเมล็ดพันธุ์ดี")> _
    Public ReadOnly Property FullPlant As String
        Get
            Try
                Dim srt As String = Me.FactorySeedProcess.Plant.PlantName & " " & Me.FactorySeedProcess.SeedType.SeedName & " ชั้นพันธุ์ " & _
                                       Me.FactorySeedProcess.SeedClass.ClassName & " ฤดู " & Me.FactorySeedProcess.Season.SeasonName & " ปี " & Me.FactorySeedProcess.SeedYear & _
                                       " รุ่น " & Me.FactorySeedProcess.Lot & " ล็อต " & LotFactory
                Return srt
            Catch ex As Exception
                Return ""
            End Try

        End Get
    End Property

    

End Class
