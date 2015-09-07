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

<XafDisplayName("����")> _
<ImageName("SearchIcon")> _
<NonPersistent> _
Public Class SearchSeedDetail ' Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        Status = PublicEnum.SimsStatusSearch.None
    End Sub

    Dim fPlant As Plant
    <XafDisplayName("�ת")> _
    <DataSourceCriteria("Status='Active'")> _
    Public Property Plant() As Plant
        Get
            Return fPlant
        End Get
        Set(ByVal value As Plant)
            SetPropertyValue(Of Plant)("Plant", fPlant, value)
        End Set
    End Property

    Dim fSeedStatus As SeedStatus
    <ImmediatePostData()> _
    <XafDisplayName("ʶҹ����紾ѹ���")> _
    Public Property SeedStatus() As SeedStatus
        Get
            Return fSeedStatus
        End Get
        Set(ByVal value As SeedStatus)
            SetPropertyValue(Of SeedStatus)("SeedStatus", fSeedStatus, value)
        End Set
    End Property
    Dim fSeedType As SeedType
    <DataSourceProperty("Plant.SeedTypes")> _
    <XafDisplayName("�ѹ���")> _
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
    Public Property Season() As Season
        Get
            Return fSeason
        End Get
        Set(ByVal value As Season)
            SetPropertyValue(Of Season)("Season", fSeason, value)
        End Set
    End Property
    Dim fSeedYear As String
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
    Dim fMoneyType As MoneyType
    <XafDisplayName("�������Թ�ع")> _
    Public Property MoneyType() As MoneyType
        Get
            Return fMoneyType
        End Get
        Set(ByVal value As MoneyType)
            SetPropertyValue(Of MoneyType)("MoneyType", fMoneyType, value)
        End Set
    End Property
    Dim fLotNo As Integer
    <XafDisplayName("��͵")> _
    Public Property LotNo() As Integer
        Get
            Return fLotNo
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("LotNo", fLotNo, value)
        End Set
    End Property

    Dim fReceiveNo As String
    <XafDisplayName("�Ţ����Ѻ")> _
    Public Property ReceiveNo() As String
        Get
            Return fReceiveNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ReceiveNo", fReceiveNo, value)
        End Set
    End Property

    Dim fStartDate As Date
    <XafDisplayName("�ѹ����Ѻ")> _
    Public Property StartDate() As Date
        Get
            Return fStartDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue(Of Date)("StartDate", fStartDate, value)
        End Set
    End Property

    Dim fEndDate As Date
    <XafDisplayName("�֧�ѹ���")> _
    Public Property EndDate() As Date
        Get
            Return fEndDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue(Of Date)("EndDate", fEndDate, value)
        End Set
    End Property

    Dim fStatus As PublicEnum.SimsStatusSearch
    <XafDisplayName("ʶҹС���Ѻ")> _
    Public Property Status() As PublicEnum.SimsStatusSearch
        Get
            Return fStatus
        End Get
        Set(ByVal value As PublicEnum.SimsStatusSearch)
            SetPropertyValue(Of PublicEnum.SimsStatusSearch)("Status", fStatus, value)
        End Set
    End Property

End Class
