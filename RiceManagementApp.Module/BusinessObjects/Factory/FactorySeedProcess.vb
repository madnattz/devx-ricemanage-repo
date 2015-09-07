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

'<RuleCriteria("CheckAmount", DefaultContexts.Save, "[SeedProcessAmount] <= [SeedAmount]", "เมล็ดพันธุ์ดีที่ปรับปรุงได้ ต้องน้อยกว่าหรือเท่ากับ น้ำหนักเมล็ดพันธุ์ดิบที่นำเข้า")> _
<DefaultClassOptions()> _
<ImageName("BO_Task")> _
<XafDisplayName("ปรับปรุงสภาพเมล็ดพันธุ์")> _
<ConditionalAppearance.Appearance("DisableAllItems", criteria:="Status='Done'", Enabled:=False, TargetItems:="*", Context:="DetailView")> _
Public Class FactorySeedProcess
    Inherits BaseObject

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        DataOwner = GetCurrentSite()
    End Sub

    Protected Overrides Sub OnSaving()
        MyBase.OnSaving()
        If Me.DataOwner Is Nothing Then
            DataOwner = GetCurrentSite()
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

    <XafDisplayName("นำเข้าเมล็ดพันธุ์ดิบ")> _
    <Association("FactorySeedProcessDetails-FactorySeedProcess", GetType(FactorySeedProcessDetail))> _
     <ImmediatePostData> _
    <DevExpress.Xpo.Aggregated> _
    Public ReadOnly Property FactorySeedProcessDetails() As XPCollection(Of FactorySeedProcessDetail)
        Get
            Return GetCollection(Of FactorySeedProcessDetail)("FactorySeedProcessDetails")
        End Get
    End Property

    <XafDisplayName("ข้อมูลการปรับปรุงสภาพรายวัน")> _
    <Association("FactorySeedProcessTodays-FactorySeedProcess", GetType(FactorySeedProcessToday))> _
    <ImmediatePostData> _
    <DevExpress.Xpo.Aggregated> _
    Public ReadOnly Property FactorySeedProcessTodays() As XPCollection(Of FactorySeedProcessToday)
        Get
            Return GetCollection(Of FactorySeedProcessToday)("FactorySeedProcessTodays")
        End Get
    End Property

    <XafDisplayName("จัดล็อตเมล็ดพันธุ์ดี")> _
    <Association("FactoryPickProcess-FactorySeedProcess", GetType(FactoryPickProcess))> _
    <ImmediatePostData> _
    <DevExpress.Xpo.Aggregated> _
    Public ReadOnly Property FactoryPickProcessLots() As XPCollection(Of FactoryPickProcess)
        Get
            Return GetCollection(Of FactoryPickProcess)("FactoryPickProcessLots")
        End Get
    End Property

    Dim fPlant As Plant
    <XafDisplayName("พืช")> _
    <DataSourceCriteria("Status = 'Active'")> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property Plant() As Plant
        Get
            Return fPlant
        End Get
        Set(ByVal value As Plant)
            SetPropertyValue(Of Plant)("Plant", fPlant, value)
        End Set
    End Property

    Dim fSeedType As SeedType
    <XafDisplayName("พันธุ์")> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property SeedType() As SeedType
        Get
            Return fSeedType
        End Get
        Set(ByVal value As SeedType)
            SetPropertyValue(Of SeedType)("SeedType", fSeedType, value)
        End Set
    End Property

    Dim fSeedClass As SeedClass
    <XafDisplayName("ชั้นพันธุ์")> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property SeedClass() As SeedClass
        Get
            Return fSeedClass
        End Get
        Set(ByVal value As SeedClass)
            SetPropertyValue(Of SeedClass)("SeedClass", fSeedClass, value)
        End Set
    End Property

    Dim fSeason As Season
    <XafDisplayName("ฤดู")> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property Season() As Season
        Get
            Return fSeason
        End Get
        Set(ByVal value As Season)
            SetPropertyValue(Of Season)("Season", fSeason, value)
        End Set
    End Property

    Dim fSeedYear As String
    <RuleRequiredField(DefaultContexts.Save)> _
    <ImmediatePostData()> _
    <XafDisplayName("ปี")> _
    <ModelDefault("PropertyEditorType", "RiceManagementApp.Module.Win.CustomYearDropdown")> _
    Public Property SeedYear() As String
        Get
            Return fSeedYear
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SeedYear", fSeedYear, value)
        End Set
    End Property

    Dim fLot As String
    <XafDisplayName("รุ่นที่")> _
    <RuleRequiredField(DefaultContexts.Save)> _
    <ModelDefault("PropertyEditorType", "RiceManagementApp.Module.Win.CustomLotDropdown")> _
    Public Property Lot() As String
        Get
            Return fLot
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Lot", fLot, value)
        End Set
    End Property

    Dim fFactoryNo As PublicEnum.EnumFactoryNo
    <XafDisplayName("โรงงานที่")> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property FactoryNo() As PublicEnum.EnumFactoryNo
        Get
            Return fFactoryNo
        End Get
        Set(ByVal value As PublicEnum.EnumFactoryNo)
            SetPropertyValue(Of PublicEnum.EnumFactoryNo)("FactoryNo", fFactoryNo, value)
        End Set
    End Property

    Dim fStartProcessDate As Date
    <XafDisplayName("วันที่เริ่มปรับปรุง")> _
    Public Property StartProcessDate() As Date
        Get
            Return fStartProcessDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue(Of Date)("StartProcessDate", fStartProcessDate, value)
        End Set
    End Property

    Dim fEndProcessDate As Date
    <XafDisplayName("วันที่เสร็จสิ้น")> _
    Public Property EndProcessDate() As Date
        Get
            Return fEndProcessDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue(Of Date)("EndProcessDate", fEndProcessDate, value)
        End Set
    End Property

    <Persistent("SeedAmount")> _
    Private fSeedAmount As Nullable(Of Integer) = Nothing
    <ModelDefault("DisPlayFormat", ("{0:#,#0}"))> _
    <PersistentAlias("fSeedAmount")> _
    <ModelDefault("DisPlayFormat", ("{0:#,#0}"))> _
    <XafDisplayName("น้ำหนักเมล็ดพันธุ์ดิบที่นำเข้า สะสม (กก.)")> _
    Public ReadOnly Property SeedAmount As Integer
        Get
            Try
                If (Not IsLoading) AndAlso (Not IsSaving) AndAlso Not fSeedAmount.HasValue Then
                    UpdateSeedAmount(False)
                End If
                Return fSeedAmount
            Catch ex As Exception
                Return 0
            End Try
        End Get
    End Property

    Public Sub UpdateSeedAmount(ByVal forceChangeEvents As Boolean)
        Dim fOldSeedAmount As Nullable(Of Integer) = fSeedAmount
        fSeedAmount = Convert.ToInt32(Evaluate(CriteriaOperator.Parse("FactorySeedProcessDetails.Sum(SeedAmount)")))

        If forceChangeEvents Then
            OnChanged("SeedAmount", fOldSeedAmount, fSeedAmount)
        End If
    End Sub

    <Persistent("SeedProcessAmount")> _
    Private fSeedProcessAmount As Nullable(Of Integer) = Nothing
    <ModelDefault("DisPlayFormat", ("{0:#,#0}"))> _
    <PersistentAlias("fSeedProcessAmount")> _
    <ModelDefault("DisPlayFormat", ("{0:#,#0}"))> _
    <XafDisplayName("เมล็ดพันธุ์ดีที่ปรับปรุงได้ สะสม (กก.)")> _
    Public ReadOnly Property SeedProcessAmount() As Nullable(Of Integer)
        Get
            Try
                If (Not IsLoading) AndAlso (Not IsSaving) AndAlso Not fSeedProcessAmount.HasValue Then
                    UpdateSeedProcessAmount(False)
                End If
                Return fSeedProcessAmount
            Catch ex As Exception
                Return 0
            End Try
        End Get
    End Property

    Public Sub UpdateSeedProcessAmount(ByVal forceChangeEvents As Boolean)
        Try
            Dim fOldSeedProcessAmount As Nullable(Of Integer) = fSeedProcessAmount
            fSeedProcessAmount = Convert.ToInt32(Evaluate(CriteriaOperator.Parse("FactorySeedProcessTodays.Sum(SeedProcessAmount)")))
            If forceChangeEvents Then
                OnChanged("SeedProcessAmount", fOldSeedProcessAmount, fSeedProcessAmount)
            End If
        Catch ex As Exception

        End Try

    End Sub

    <Persistent("SeedSetLotAmount")> _
    Private fSeedSetLotAmount As Nullable(Of Integer) = Nothing
    <PersistentAlias("fSeedSetLotAmount")> _
    <ModelDefault("DisPlayFormat", ("{0:#,#0}"))> _
    <XafDisplayName("เมล็ดพันธุ์ดีที่จัดล็อต สะสม (กก.)")> _
    Public ReadOnly Property SeedSetLotAmount() As Nullable(Of Integer)
        Get
            Try
                If (Not IsLoading) AndAlso (Not IsSaving) AndAlso Not fSeedSetLotAmount.HasValue Then
                    UpdateSetLotAmount(False)
                End If
                Return fSeedSetLotAmount
            Catch ex As Exception
                Return 0
            End Try
        End Get
    End Property

    Public Sub UpdateSetLotAmount(ByVal forceChangeEvents As Boolean)
        Try
            Dim fOldSeedSetLotAmount As Nullable(Of Integer) = fSeedSetLotAmount
            fSeedSetLotAmount = Convert.ToInt32(Evaluate(CriteriaOperator.Parse("FactoryPickProcessLots.Sum(Quantity)")))
            If forceChangeEvents Then
                OnChanged("SeedSetLotAmount", fOldSeedSetLotAmount, fSeedSetLotAmount)
            End If
        Catch ex As Exception

        End Try

    End Sub

    <ModelDefault("DisPlayFormat", ("{0:#,#0}"))> _
    <XafDisplayName("เมล็ดพันธุ์ดีที่ยังไม่จัดล็อต (กก.)")> _
    Public ReadOnly Property AvailableSetLotAmount As Integer
        Get
            Return SeedProcessAmount - SeedSetLotAmount
        End Get
    End Property

    Dim fStatus As PublicEnum.FactoryProcessStatus
    <XafDisplayName("สถานะ")> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property Status() As PublicEnum.FactoryProcessStatus
        Get
            Return fStatus
        End Get
        Set(ByVal value As PublicEnum.FactoryProcessStatus)
            SetPropertyValue(Of PublicEnum.FactoryProcessStatus)("Status ", fStatus, value)
        End Set
    End Property

    <Action(Caption:="เสร็จสิ้นการปรับปรุงสภาพ", ConfirmationMessage:="ท่านต้องการบันทึกข้อมูล ใช่หรือไม่?", ImageName:="Action_Grant", AutoCommit:=True, TargetObjectsCriteria:="Status='Doing'")> _
    Public Sub MarkAsComplete()
        If Not IsDeleted Then
            If Not FactorySeedProcessDetails.Count > 0 Then
                MsgBox("ไม่พบข้อมูลการปรับปรุงสภาพเมล็ดพันธุ์รายวัน กรุณาตรวจสอบข้อมูล", MsgBoxStyle.Information)
                Exit Sub
            End If
            Me.Status = PublicEnum.FactoryProcessStatus.Done

            MyBase.Save()
            Session.CommitTransaction()
        End If
    End Sub

    <Action(Caption:="ย้อนกลับ", ConfirmationMessage:="ท่านต้องการแก้ไขข้อมูล ใช่หรือไม่?", ImageName:="Action_Deny", AutoCommit:=True, TargetObjectsCriteria:="Status='Done'")> _
    Public Sub MarkCancle()
        If Not IsDeleted Then
            Me.Status = PublicEnum.FactoryProcessStatus.Doing
            MyBase.Save()
            Session.CommitTransaction()
        End If
    End Sub


End Class
