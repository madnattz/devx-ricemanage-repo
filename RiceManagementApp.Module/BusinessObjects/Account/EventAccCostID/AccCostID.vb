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
Imports DevExpress.ExpressApp.Editors

'<ImageName("BO_Contact")> _
'<DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")> _
'<DefaultListViewOptions(MasterDetailMode.ListViewOnly, False, NewItemRowPosition.None)> _
'<Persistent("DatabaseTableName")> _
'<NavigationItem("การบันทึกรายการต้นทุนการผลิต")> _
'<XafDisplayName("")> _
'<DefaultClassOptions()> _
Public Class AccCostID ' Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        DataOwner = GetCurrentSite()
        MyBase.AfterConstruction()
        ' Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).
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

    Private _EventIDNo As EventAccCostID
    <Association("EventAccCostID-AccCostID")> _
    <VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)> _
    <DC.Aggregated()> _
    Public Property EventIDNo() As EventAccCostID
        Get
            Return _EventIDNo
        End Get
        Set(ByVal value As EventAccCostID)
            SetPropertyValue("EventIDNo", _EventIDNo, value)
        End Set
    End Property

    Private _CostDate As Date = Today
    <XafDisplayName("วันที่เบิก")> _
    <Index(0), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    <ImmediatePostData()> _
    <RuleRequiredField()> _
    Public Property CostDate() As Date
        Get
            Return _CostDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue("CostDate", _CostDate, value)
        End Set
    End Property

    Private _RefNo As String
    <XafDisplayName("เลขที่ใบเบิก")> _
    <Index(0), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    <ImmediatePostData()> _
    <RuleRequiredField()> _
    Public Property RefNo() As String
        Get
            Return _RefNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue("RefNo", _RefNo, value)
        End Set
    End Property

    Private _CostTypeID As CostTypeID
    <XafDisplayName("ประเภท")> _
    <Index(1), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    <ImmediatePostData()> _
    <RuleRequiredField()> _
    Public Property CostTypeID() As CostTypeID
        Get
            Return _CostTypeID
        End Get
        Set(ByVal value As CostTypeID)
            SetPropertyValue("CostTypeID", _CostTypeID, value)
            Try
                If CostTypeID.CostTypeCode = 1 Then
                    Detail = "เมล็ดพันธุ์ซื้อคืนข้าว"
                    UnitID = "กก."
                Else
                    Detail = Nothing
                    UnitID = Nothing
                    MaterialType = Nothing
                    MaterialName = Nothing
                    Amount = Nothing
                    UnitPrice = Nothing
                    TotalAmount = Nothing
                End If

                '    'If CostTypeID.CostTypeCode <> 2 Then
                '    '    AmountMP = Nothing
                '    '    UnitPrice = Nothing
                '    '    TotalAmountMP = Nothing
                '    'End If
                '    'If CostTypeID.CostTypeCode <> 3 Then
                '    '    AmountCP = Nothing
                '    '    UnitPrice = Nothing
                '    '    TotalAmountMP = Nothing
                '    'End If
            Catch ex As Exception

            End Try
        End Set
    End Property

    Private _MaterialType As PublicEnum.MaterialType
    <XafDisplayName("ชนิดวัสดุการผลิต")> _
    <Appearance("HideMaterial", Visibility:=ViewItemVisibility.Hide, Criteria:="[CostTypeID].[CostTypeCode] <> 2", Context:="DetailView")> _
    <Index(2), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    <ImmediatePostData()> _
    Public Property MaterialType() As PublicEnum.MaterialType
        Get
            Return _MaterialType
        End Get
        Set(ByVal value As PublicEnum.MaterialType)
            SetPropertyValue("MaterialType", _MaterialType, value)
            MaterialName = Nothing
            Detail = Nothing
            UnitID = Nothing
            Amount = Nothing
            UnitPrice = Nothing
            TotalAmount = Nothing
        End Set
    End Property

    Private _MaterialName As Material
    <XafDisplayName("วัสดุการผลิต")> _
    <Appearance("HideMaterialName", Visibility:=ViewItemVisibility.Hide, Criteria:="[CostTypeID].[CostTypeCode] <> 2", Context:="DetailView")> _
    <Index(2), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    <ImmediatePostData()> _
    Public Property MaterialName() As Material
        Get
            Return _MaterialName
        End Get
        Set(ByVal value As Material)
            SetPropertyValue("MaterialName", _MaterialName, value)
            If value IsNot Nothing Then
                Detail = MaterialName.MaterialName
                UnitID = MaterialName.Unit
            Else
                Detail = Nothing
                UnitID = Nothing
                Amount = Nothing
                UnitPrice = Nothing
                TotalAmount = Nothing
            End If
        End Set
    End Property

    Private _Detail As String
    <XafDisplayName("รายการ")> _
    <Index(3), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    <ImmediatePostData()> _
    <RuleRequiredField()> _
    Public Property Detail() As String
        Get
            Return _Detail
        End Get
        Set(ByVal value As String)
            SetPropertyValue("Detail", _Detail, value)
        End Set
    End Property

    Private _UnitID As String
    <XafDisplayName("หน่วย")> _
    <Index(4), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    <ImmediatePostData()> _
    Public Property UnitID() As String
        Get
            Return _UnitID
        End Get
        Set(ByVal value As String)
            SetPropertyValue("UnitID", _UnitID, value)
        End Set
    End Property

    Private _Amount As Double
    <XafDisplayName("จำนวน")> _
    <ModelDefault("DisplayFormat", "{0:N2}")> _
    <ModelDefault("EditMask", "N")> _
    <Index(5), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    <ImmediatePostData()> _
    <RuleRequiredField()> _
    Public Property Amount() As Double
        Get
            Return _Amount
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("Amount", _Amount, value)
        End Set
    End Property

    Private _UnitPrice As Double
    <XafDisplayName("ราคาต่อหน่วย")> _
    <ModelDefault("DisplayFormat", "{0:N4}")> _
    <ModelDefault("EditMask", "N")> _
    <Index(6), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    <ImmediatePostData()> _
    <RuleRequiredField()> _
    Public Property UnitPrice() As Double
        Get
            Return _UnitPrice
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("UnitPrice", _UnitPrice, value)
        End Set
    End Property

    Private _TotalAmount As Double
    <XafDisplayName("รวมเงิน")> _
    <ModelDefault("DisplayFormat", "{0:N4}")> _
    <ModelDefault("EditMask", "N")> _
    <Index(7), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    <ImmediatePostData()> _
    Public Property TotalAmount() As Double
        Get
                If Amount And UnitPrice <> 0 Then
                    TotalAmount = Amount * UnitPrice
                End If
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("TotalAmount", _TotalAmount, value)
        End Set
    End Property

    <ModelDefault("DisplayFormat", "{0:N2}")> _
    <ModelDefault("EditMask", "N")> _
    <Index(8), VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)> _
    <ImmediatePostData()> _
    Public ReadOnly Property AmountSeed() As Double
        Get
            Try
                If CostTypeID.CostTypeCode = 1 Then
                    If Amount <> 0 Then
                        AmountSeed = Amount
                    End If
                End If
            Catch ex As Exception

            End Try

        End Get
    End Property

    <ModelDefault("DisplayFormat", "{0:N2}")> _
<ModelDefault("EditMask", "N")> _
<Index(8), VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)> _
<ImmediatePostData()> _
    Public ReadOnly Property TotalSeed() As Double
        Get
            Try
                If CostTypeID.CostTypeCode = 1 Then
                    If Amount And UnitPrice <> 0 Then
                        TotalSeed = Amount * UnitPrice
                    End If
                End If
            Catch ex As Exception

            End Try

        End Get
    End Property

    <ModelDefault("DisplayFormat", "{0:N4}")> _
    <ModelDefault("EditMask", "N")> _
    <Index(8), VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)> _
    <ImmediatePostData()> _
    Public ReadOnly Property TotalOil() As Double
        Get
            Try
                If CostTypeID.CostTypeCode = 2 Then
                    If MaterialName.MaterialType = PublicEnum.MaterialType.DieselFuel Then
                        If Amount And UnitPrice <> 0 Then
                            TotalOil = Amount * UnitPrice
                        End If
                    End If
                End If
            Catch ex As Exception

            End Try

        End Get
    End Property

    <ModelDefault("DisplayFormat", "{0:N4}")> _
    <ModelDefault("EditMask", "N")> _
    <Index(9), VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)> _
    <ImmediatePostData()> _
    Public ReadOnly Property AmountMaterials() As Double
        Get
            Try
                If CostTypeID.CostTypeCode = 2 Then
                    If MaterialName.MaterialType = PublicEnum.MaterialType.ProcessMaterail Or MaterialName.MaterialType = PublicEnum.MaterialType.DieselFuel Then
                        If Amount <> 0 Then
                            AmountMaterials = Amount
                        End If
                    End If
                End If
            Catch ex As Exception

            End Try
        End Get
    End Property

    <ModelDefault("DisplayFormat", "{0:N4}")> _
    <ModelDefault("EditMask", "N")> _
    <Index(9), VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)> _
    <ImmediatePostData()> _
    Public ReadOnly Property TotalMaterials() As Double
        Get
            Try
                If CostTypeID.CostTypeCode = 2 Then
                    If MaterialName.MaterialType = PublicEnum.MaterialType.ProcessMaterail Or MaterialName.MaterialType = PublicEnum.MaterialType.DieselFuel Then
                        If Amount And UnitPrice <> 0 Then
                            TotalMaterials = Amount * UnitPrice
                        End If
                    End If
                End If
            Catch ex As Exception

            End Try
        End Get
    End Property

    <ModelDefault("DisplayFormat", "{0:N4}")> _
    <ModelDefault("EditMask", "N")> _
    <Index(9), VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)> _
    <ImmediatePostData()> _
    Public ReadOnly Property AmountChemical() As Double
        Get
            Try
                If CostTypeID.CostTypeCode = 2 Then
                    If MaterialName.MaterialType = PublicEnum.MaterialType.FumigationChemical Or MaterialName.MaterialType = PublicEnum.MaterialType.SprayChemical Then
                        If Amount <> 0 Then
                            AmountChemical = Amount
                        End If
                    End If
                End If


            Catch ex As Exception

            End Try
        End Get
    End Property

    <ModelDefault("DisplayFormat", "{0:N4}")> _
    <ModelDefault("EditMask", "N")> _
    <Index(9), VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)> _
    <ImmediatePostData()> _
    Public ReadOnly Property TotalChemical() As Double
        Get
            Try
                If CostTypeID.CostTypeCode = 2 Then
                    If MaterialName.MaterialType = PublicEnum.MaterialType.FumigationChemical Or MaterialName.MaterialType = PublicEnum.MaterialType.SprayChemical Then
                        If Amount And UnitPrice <> 0 Then
                            TotalChemical = Amount * UnitPrice
                        End If
                    End If
                End If
            Catch ex As Exception

            End Try
        End Get
    End Property

    '<Action(Caption:="My UI Action", ConfirmationMessage:="Are you sure?", ImageName:="Attention", AutoCommit:=True)> _
    'Public Sub ActionMethod()
    '    ' Trigger a custom business logic for the current record in the UI (http://documentation.devexpress.com/#Xaf/CustomDocument2619).
    '    Me.PersistentProperty = "Paid"
    'End Sub
End Class
