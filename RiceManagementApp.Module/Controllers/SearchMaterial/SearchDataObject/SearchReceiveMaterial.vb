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
Public Class SearchReceiveMaterial
    Inherits BaseObject
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        Status = PublicEnum.SimsStatusSearch.None
    End Sub

    Dim fMaterial As Material
    <XafDisplayName("��ʴء�ü�Ե")> _
    <DataSourceCriteria("Status='Active'")> _
    Public Property Material() As Material
        Get
            Return fMaterial
        End Get
        Set(ByVal value As Material)
            SetPropertyValue(Of Material)("Material", fMaterial, value)
        End Set
    End Property

    Dim fSeedYear As String
    <XafDisplayName("��")> _
    <ModelDefault("PropertyEditorType", "RiceManagementApp.Module.Win.CustomYearDropdown")> _
    Public Property MaterialYear() As String
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
