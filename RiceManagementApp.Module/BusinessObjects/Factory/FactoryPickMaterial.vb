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
<RuleCriteria("FactoryPickMaterial.NotDelete", DefaultContexts.Delete, "Status='Pending'", CustomMessageTemplate:="ไม่สามารถลบข้อมูลนี้ได้")> _
<ConditionalAppearance.Appearance("FactoryPickMaterialDisableAllItems", criteria:="Status!='Pending'", Enabled:=False, TargetItems:="*", Context:="DetailView")> _
Public Class FactoryPickMaterial ' Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        fPickDate = Date.Today
        MyBase.AfterConstruction()
        Me.FactoryNo = PublicEnum.EnumFactoryNo.Factory1
        Me.PickType = PublicEnum.PickMaterialType.ProcessSeed
        DataOwner = GetCurrentSite()
    End Sub

    Protected Overrides Sub OnSaving()
        If (Me.fPickNo Is Nothing) Then
            Dim prefix As String = ""
            Dim _year As String = (Date.Now.Year + 543).ToString
            prefix = _year

            Me.fPickNo = String.Format("{0}/{1:D4}", prefix, DistributedIdGeneratorHelper.Generate(Me.Session.DataLayer, Me.GetType().FullName, prefix))

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

    <Persistent("PickNo")> _
    Private fPickNo As String
    <PersistentAlias("fPickNo")> _
    Public ReadOnly Property PickNo() As String
        Get
            Return fPickNo
        End Get
    End Property
    Dim fPickDate As DateTime
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property PickDate() As DateTime
        Get
            Return fPickDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("PickDateTime", fPickDate, value)
        End Set
    End Property
    Dim fFactoryNo As PublicEnum.EnumFactoryNo
    Public Property FactoryNo() As PublicEnum.EnumFactoryNo
        Get
            Return fFactoryNo
        End Get
        Set(ByVal value As PublicEnum.EnumFactoryNo)
            SetPropertyValue(Of PublicEnum.EnumFactoryNo)("FactoryNo", fFactoryNo, value)
        End Set
    End Property
    Dim fPickType As PublicEnum.PickMaterialType
    Public Property PickType() As PublicEnum.PickMaterialType
        Get
            Return fPickType
        End Get
        Set(ByVal value As PublicEnum.PickMaterialType)
            SetPropertyValue(Of PublicEnum.PickMaterialType)("PickType", fPickType, value)
        End Set
    End Property
    Dim fIsOtherPickType As Boolean
    <ImmediatePostData()> _
    Public Property IsOtherPickType() As Boolean
        Get
            Return fIsOtherPickType
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("IsOtherPickType", fIsOtherPickType, value)
            If Not IsLoading AndAlso IsSaving Then
                If value = True Then
                    PickType = Nothing
                Else
                    OtherPickType = ""
                End If
            End If
        End Set
    End Property
    Dim fOtherPickType As String
    <RuleRequiredField(DefaultContexts.Save, TargetCriteria:="IsOtherPickType=True")> _
    Public Property OtherPickType() As String
        Get
            Return fOtherPickType
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("OtherPickType", fOtherPickType, value)
        End Set
    End Property
    Dim fOrganizationSection As OrganizationSection
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property OrganizationSection() As OrganizationSection
        Get
            Return fOrganizationSection
        End Get
        Set(ByVal value As OrganizationSection)
            SetPropertyValue(Of OrganizationSection)("OrganizationSection", fOrganizationSection, value)
        End Set
    End Property
    Dim fRemark As String
    <Size(200)> _
    Public Property Remark() As String
        Get
            Return fRemark
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Remark", fRemark, value)
        End Set
    End Property
    Dim fUseWithPlant As Plant
    Public Property UseWithPlant() As Plant
        Get
            Return fUseWithPlant
        End Get
        Set(ByVal value As Plant)
            SetPropertyValue(Of Plant)("UseWithPlant", fUseWithPlant, value)
        End Set
    End Property
    Dim fUseWithSeedType As SeedType
    <DataSourceProperty("UseWithPlant.SeedTypes")> _
    Public Property UseWithSeedType() As SeedType
        Get
            Return fUseWithSeedType
        End Get
        Set(ByVal value As SeedType)
            SetPropertyValue(Of SeedType)("UseWithSeedType", fUseWithSeedType, value)
        End Set
    End Property
    Dim fUseWithSeedClass As SeedClass
    Public Property UseWithSeedClass() As SeedClass
        Get
            Return fUseWithSeedClass
        End Get
        Set(ByVal value As SeedClass)
            SetPropertyValue(Of SeedClass)("UseWithSeedClass", fUseWithSeedClass, value)
        End Set
    End Property
    Dim fUseWithSeason As Season
    Public Property UseWithSeason() As Season
        Get
            Return fUseWithSeason
        End Get
        Set(ByVal value As Season)
            SetPropertyValue(Of Season)("UseWithSeason", fUseWithSeason, value)
        End Set
    End Property
    Dim fUseWithSeedYear As String
    <Size(4)> _
    Public Property UseWithSeedYear() As String
        Get
            Return fUseWithSeedYear
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("UseWithSeedYear", fUseWithSeedYear, value)
        End Set
    End Property

    Dim fUseWithSeedLot As String
    <XafDisplayName("รุ่นที่")> _
    Public Property UseWithSeedLot() As String
        Get
            Return fUseWithSeedLot
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("UseWithSeedLot", fUseWithSeedLot, value)
        End Set
    End Property

    Dim fRequestUser As String
    <RuleRequiredField(DefaultContexts.Save)> _
    <Size(50)> _
    Public Property RequestUser() As String
        Get
            Return fRequestUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("RequestUser", fRequestUser, value)
        End Set
    End Property
    Dim fRequestPosition As String
    <RuleRequiredField(DefaultContexts.Save)> _
    <Size(50)> _
    Public Property RequestPosition() As String
        Get
            Return fRequestPosition
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("RequestPosition", fRequestPosition, value)
        End Set
    End Property
    Dim fStatus As PublicEnum.SimsStatus
    Public Property Status() As PublicEnum.SimsStatus
        Get
            Return fStatus
        End Get
        Set(ByVal value As PublicEnum.SimsStatus)
            SetPropertyValue(Of PublicEnum.SimsStatus)("Status", fStatus, value)
        End Set
    End Property

    Dim fInventoryStatus As PublicEnum.SimsStatus
    Public Property InventoryStatus() As PublicEnum.SimsStatus
        Get
            Return fInventoryStatus
        End Get
        Set(ByVal value As PublicEnum.SimsStatus)
            SetPropertyValue(Of PublicEnum.SimsStatus)("InventoryStatus", fInventoryStatus, value)
        End Set
    End Property

    <Association("FactoryPickMaterialDetailReferencesFactoryPickMaterial", GetType(FactoryPickMaterialDetail))> _
    Public ReadOnly Property FactoryPickMaterialDetails() As XPCollection(Of FactoryPickMaterialDetail)
        Get
            Return GetCollection(Of FactoryPickMaterialDetail)("FactoryPickMaterialDetails")
        End Get
    End Property

    <Action(Caption:="ยืนยันการเบิก", ConfirmationMessage:="ท่านต้องการบันทึกข้อมูล ใช่หรือไม่?", ImageName:="Action_Grant", AutoCommit:=True, TargetObjectsCriteria:="Status='Pending'")> _
    Public Sub MarkAsComplete()
        If Not IsDeleted Then
            If Not FactoryPickMaterialDetails.Count > 0 Then
                MsgBox("ไม่พบรายการวัสดุการผลิต กรุณาตรวจสอบข้อมูลอีกครั้ง", MsgBoxStyle.Information, "")
                Exit Sub
            End If
            Try
                '//สร้างข้อมูลใบเบิก
                CreateInventoryPickForm(Session, Me)

                Me.fStatus = PublicEnum.SimsStatus.Approve

                MyBase.Save()

                Session.CommitTransaction()
            Catch ex As Exception
                Session.RollbackTransaction()
            End Try
        End If
    End Sub

    Public Sub CreateInventoryPickForm(_session As Session, _requestForm As FactoryPickMaterial)
        '//สร้างข้อมูลใบเบิก
        Dim objNewPick As New PickMaterial(_session)
        objNewPick.PickDate = _requestForm.fPickDate
        objNewPick.PickType = _requestForm.fPickType
        objNewPick.FactoryNo = _requestForm.fFactoryNo
        objNewPick.RefNo = _requestForm.fPickNo
        objNewPick.RequestUser = _requestForm.fRequestUser
        objNewPick.RequestPosition = _requestForm.fRequestPosition
        objNewPick.Status = PublicEnum.SimsStatus.Pending
        objNewPick.Remark = "โรงงานขอเบิกวัสดุการผลิตผ่านระบบการเบิกเมล็ดพันธุ์ของโรงงาน"

        For Each item As FactoryPickMaterialDetail In _requestForm.FactoryPickMaterialDetails
            Dim objPickDetail As New PickMaterialDetail(_session)
            objPickDetail.PickMaterial = objNewPick
            objPickDetail.MaterialProduct = item.MaterialProduct
            objPickDetail.ProductCode = item.ProductCode
            objPickDetail.Quantity = item.Quantity
            objPickDetail.Cost = item.Cost

            objPickDetail.Save()
        Next

        objNewPick.FactoryPickMaterial = _requestForm
        objNewPick.IsDataFromFactory = True

        objNewPick.Save()

    End Sub

End Class
