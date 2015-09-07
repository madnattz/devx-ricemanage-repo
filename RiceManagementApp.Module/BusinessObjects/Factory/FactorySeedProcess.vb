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

'<RuleCriteria("CheckAmount", DefaultContexts.Save, "[SeedProcessAmount] <= [SeedAmount]", "���紾ѹ���շ���Ѻ��ا�� ��ͧ���¡���������ҡѺ ���˹ѡ���紾ѹ���Ժ�������")> _
<DefaultClassOptions()> _
<ImageName("BO_Task")> _
<XafDisplayName("��Ѻ��ا��Ҿ���紾ѹ���")> _
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

    <XafDisplayName("��������紾ѹ���Ժ")> _
    <Association("FactorySeedProcessDetails-FactorySeedProcess", GetType(FactorySeedProcessDetail))> _
     <ImmediatePostData> _
    <DevExpress.Xpo.Aggregated> _
    Public ReadOnly Property FactorySeedProcessDetails() As XPCollection(Of FactorySeedProcessDetail)
        Get
            Return GetCollection(Of FactorySeedProcessDetail)("FactorySeedProcessDetails")
        End Get
    End Property

    <XafDisplayName("�����š�û�Ѻ��ا��Ҿ����ѹ")> _
    <Association("FactorySeedProcessTodays-FactorySeedProcess", GetType(FactorySeedProcessToday))> _
    <ImmediatePostData> _
    <DevExpress.Xpo.Aggregated> _
    Public ReadOnly Property FactorySeedProcessTodays() As XPCollection(Of FactorySeedProcessToday)
        Get
            Return GetCollection(Of FactorySeedProcessToday)("FactorySeedProcessTodays")
        End Get
    End Property

    <XafDisplayName("�Ѵ��͵���紾ѹ����")> _
    <Association("FactoryPickProcess-FactorySeedProcess", GetType(FactoryPickProcess))> _
    <ImmediatePostData> _
    <DevExpress.Xpo.Aggregated> _
    Public ReadOnly Property FactoryPickProcessLots() As XPCollection(Of FactoryPickProcess)
        Get
            Return GetCollection(Of FactoryPickProcess)("FactoryPickProcessLots")
        End Get
    End Property

    Dim fPlant As Plant
    <XafDisplayName("�ת")> _
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
    <XafDisplayName("�ѹ���")> _
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
    <XafDisplayName("��鹾ѹ���")> _
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
    <XafDisplayName("Ĵ�")> _
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
    <XafDisplayName("��")> _
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
    <XafDisplayName("��蹷��")> _
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
    <XafDisplayName("�ç�ҹ���")> _
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
    <XafDisplayName("�ѹ����������Ѻ��ا")> _
    Public Property StartProcessDate() As Date
        Get
            Return fStartProcessDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue(Of Date)("StartProcessDate", fStartProcessDate, value)
        End Set
    End Property

    Dim fEndProcessDate As Date
    <XafDisplayName("�ѹ����������")> _
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
    <XafDisplayName("���˹ѡ���紾ѹ���Ժ������� ���� (��.)")> _
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
    <XafDisplayName("���紾ѹ���շ���Ѻ��ا�� ���� (��.)")> _
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
    <XafDisplayName("���紾ѹ���շ��Ѵ��͵ ���� (��.)")> _
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
    <XafDisplayName("���紾ѹ���շ���ѧ���Ѵ��͵ (��.)")> _
    Public ReadOnly Property AvailableSetLotAmount As Integer
        Get
            Return SeedProcessAmount - SeedSetLotAmount
        End Get
    End Property

    Dim fStatus As PublicEnum.FactoryProcessStatus
    <XafDisplayName("ʶҹ�")> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property Status() As PublicEnum.FactoryProcessStatus
        Get
            Return fStatus
        End Get
        Set(ByVal value As PublicEnum.FactoryProcessStatus)
            SetPropertyValue(Of PublicEnum.FactoryProcessStatus)("Status ", fStatus, value)
        End Set
    End Property

    <Action(Caption:="������鹡�û�Ѻ��ا��Ҿ", ConfirmationMessage:="��ҹ��ͧ��úѹ�֡������ ���������?", ImageName:="Action_Grant", AutoCommit:=True, TargetObjectsCriteria:="Status='Doing'")> _
    Public Sub MarkAsComplete()
        If Not IsDeleted Then
            If Not FactorySeedProcessDetails.Count > 0 Then
                MsgBox("��辺�����š�û�Ѻ��ا��Ҿ���紾ѹ�������ѹ ��سҵ�Ǩ�ͺ������", MsgBoxStyle.Information)
                Exit Sub
            End If
            Me.Status = PublicEnum.FactoryProcessStatus.Done

            MyBase.Save()
            Session.CommitTransaction()
        End If
    End Sub

    <Action(Caption:="��͹��Ѻ", ConfirmationMessage:="��ҹ��ͧ�����䢢����� ���������?", ImageName:="Action_Deny", AutoCommit:=True, TargetObjectsCriteria:="Status='Done'")> _
    Public Sub MarkCancle()
        If Not IsDeleted Then
            Me.Status = PublicEnum.FactoryProcessStatus.Doing
            MyBase.Save()
            Session.CommitTransaction()
        End If
    End Sub


End Class
