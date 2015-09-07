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

<ImageName("BO_Product")> _
<XafDisplayName("ส่งคืนเมล็ดพันธุ์ดี")> _
<DefaultClassOptions()> _
<RuleCriteria("FactoryReturnSeed.NotDelete", DefaultContexts.Delete, "Status='Pending'", CustomMessageTemplate:="ไม่สามารถลบข้อมูลนี้ได้")> _
<ConditionalAppearance.Appearance("FactoryReturnSeedDisableAllItems", criteria:="Status!='Pending'", Enabled:=False, TargetItems:="*", Context:="DetailView")> _
Public Class FactoryReturnSeed
    Inherits BaseObject
    Implements IApproveAble

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        Me.PickSeedDate = Date.Now
        DataOwner = GetCurrentSite()
        Me.Status = PublicEnum.SimsStatus.Pending
    End Sub

    Protected Overrides Sub OnSaving()
        If (Me.fReturnNo Is Nothing) Then
            Dim prefix As String = ""
            Dim _year As String = (Date.Now.Year + 543).ToString
            prefix = _year

            Me.fReturnNo = String.Format("{0}/{1:D4}", prefix, DistributedIdGeneratorHelper.Generate(Me.Session.DataLayer, Me.GetType().FullName, prefix))

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

    <XafDisplayName("ข้อมูลเมล็ดพันธุ์")> _
    <Association("FactoryReturnSeedDetails-FactoryReturnSeed", GetType(FactoryReturnSeedDetail))> _
    <DevExpress.Xpo.Aggregated> _
    Public ReadOnly Property FactoryReturnSeedDetails() As XPCollection(Of FactoryReturnSeedDetail)
        Get
            Return GetCollection(Of FactoryReturnSeedDetail)("FactoryReturnSeedDetails")
        End Get
    End Property

    <Persistent("ReturnNo")> _
    Private fReturnNo As String
    <PersistentAlias("fReturnNo")> _
    <XafDisplayName("เลขที่")> _
    Public ReadOnly Property ReturnNo() As String
        Get
            Return fReturnNo
        End Get
    End Property

    Dim fPickSeedDate As Date = Now
    <XafDisplayName("วันที่ส่งคืนเมล็ดพันธุ์")> _
    <VisibleInDetailView(True), VisibleInListView(False), VisibleInLookupListView(False)>
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property PickSeedDate() As Date
        Get
            Return fPickSeedDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue(Of Date)("PickSeedDate", fPickSeedDate, value)
        End Set
    End Property

    Dim fPlant As Plant
    <XafDisplayName("พืช")> _
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

    'Dim fLotFactory As String
    '<Index(7)>
    '<XafDisplayName("ล็อตโรงงาน")> _
    '<RuleRequiredField(DefaultContexts.Save)> _
    'Public Property LotFactory() As String
    '    Get
    '        Return fLotFactory
    '    End Get
    '    Set(ByVal value As String)
    '        SetPropertyValue(Of String)("LotFactory ", fLotFactory, value)
    '    End Set
    'End Property

    'Private fBag As Integer
    '<ModelDefault("DisPlayFormat", ("{0:#,#0}"))> _
    '<XafDisplayName("กระสอบ (ใบ)")> _
    '<ImmediatePostData> _
    'Public Property Bag As Integer
    '    Get
    '        Return fBag
    '    End Get
    '    Set(value As Integer)
    '        SetPropertyValue(Of Integer)("Bag", fBag, value)
    '    End Set
    'End Property

    <Persistent("Bag")> _
    Dim fBag As Nullable(Of Integer) = Nothing
    <ModelDefault("DisPlayFormat", ("{0:#,#0}"))> _
    <PersistentAlias("fBag")> _
    <ModelDefault("DisPlayFormat", ("{0:#,#0}"))> _
    <XafDisplayName("กระสอบ (ใบ)")> _
    Public ReadOnly Property Bag() As Integer
        Get
            Try
                If (Not IsLoading) AndAlso (Not IsSaving) AndAlso Not fBag.HasValue Then
                    UpdateBags(False)
                End If
                Return fBag
            Catch ex As Exception
                Return 0
            End Try
        End Get
    End Property

    Public Sub UpdateBags(ByVal forceChangeEvents As Boolean)
        Try
            Dim fOldBag As Nullable(Of Integer) = fBag
            fBag = Convert.ToInt32(Evaluate(CriteriaOperator.Parse("FactoryReturnSeedDetails.Sum(Bag)")))
            If forceChangeEvents Then
                OnChanged("Bag", fOldBag, fBag)
            End If
        Catch ex As Exception
        End Try

    End Sub

    <Persistent("SeedReturn")> _
    Dim fSeedReturn As Nullable(Of Integer) = Nothing
    <ModelDefault("DisPlayFormat", ("{0:#,#0}"))> _
    <PersistentAlias("fSeedTotal")> _
    <ModelDefault("DisPlayFormat", ("{0:#,#0}"))> _
    <XafDisplayName("น้ำหนักเมล็ดพันธุ์ดี รวม (กก.)")> _
    Public ReadOnly Property SeedReturn() As Integer
        Get
            Try
                If (Not IsLoading) AndAlso (Not IsSaving) AndAlso Not fSeedReturn.HasValue Then
                    UpdateSeedComplete(False)
                End If
                Return fSeedReturn
            Catch ex As Exception
                Return 0
            End Try
        End Get
    End Property

    Public Sub UpdateSeedComplete(ByVal forceChangeEvents As Boolean)
        Try
            Dim fOldfSeedComplete As Nullable(Of Integer) = fSeedReturn
            fSeedReturn = Convert.ToInt32(Evaluate(CriteriaOperator.Parse("FactoryReturnSeedDetails.Sum(SeedReturn)")))
            If forceChangeEvents Then
                OnChanged("SeedReturn", fOldfSeedComplete, fSeedReturn)
            End If
        Catch ex As Exception
        End Try

    End Sub

    Dim fUserReturn As String
    <XafDisplayName("ผู้ส่งคืน")> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property UserReturn() As String
        Get
            Return fUserReturn
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("UserReturn  ", fUserReturn, value)
        End Set
    End Property

    Dim fUserReturnPosition As String
    <XafDisplayName("ตำแหน่งผู้ส่งคืน")> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property UserReturnPosition() As String
        Get
            Return fUserReturnPosition
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("UserReturnPosition", fUserReturnPosition, value)
        End Set
    End Property

    Dim fStatus As PublicEnum.SimsStatus
    <XafDisplayName("สถานะ")> _
    Public Property Status() As PublicEnum.SimsStatus
        Get
            Return fStatus
        End Get
        Set(ByVal value As PublicEnum.SimsStatus)
            SetPropertyValue(Of PublicEnum.SimsStatus)("Status", fStatus, value)
        End Set
    End Property

    '<Action(Caption:="ยืนยัน", ConfirmationMessage:="ท่านต้องการบันทึกข้อมูล ใช่หรือไม่?", ImageName:="Action_Grant", AutoCommit:=True, TargetObjectsCriteria:="Status='Pending'")> _
    Public Function MarkAsComplete() As Boolean
        If Not IsDeleted Then
            If Not FactoryReturnSeedDetails.Count > 0 Then
                MsgBox("ไม่พบรายการเมล็ดพันธุ์ กรุณาตรวจสอบข้อมูลอีกครั้ง", MsgBoxStyle.Information, "")
                Exit Function
            End If
            Try
                Me.Status = PublicEnum.SimsStatus.Approve
                For Each item As FactoryReturnSeedDetail In FactoryReturnSeedDetails
                    item.PlantFullName.Status = PublicEnum.FactoryPickProcess.Process
                Next
                MyBase.Save()
                'Session.CommitTransaction()
                'Me.Reload()
                Return True
            Catch ex As Exception
                'Session.RollbackTransaction()
                Return False
            End Try
        End If
    End Function

    '<Action(Caption:="ยกเลิก", ConfirmationMessage:="ท่านต้องการบันทึกข้อมูล ใช่หรือไม่?", ImageName:="Action_Deny", AutoCommit:=True, TargetObjectsCriteria:="Status='Approve'")> _
    Public Function MarkAsCancel() As Boolean
        If Not IsDeleted Then

            Dim _session As Session = Me.Session
            Try
                Me.Status = PublicEnum.SimsStatus.Cancel

                For Each item As FactoryReturnSeedDetail In FactoryReturnSeedDetails
                    item.PlantFullName.Status = PublicEnum.FactoryPickProcess.Pending
                Next

                MyBase.Save()
                '_session.CommitTransaction()
                'Me.Reload()
                Return True
            Catch ex As Exception
                '_session.RollbackTransaction()
                Return False
            End Try
        End If
    End Function

    Public Function DoApprove() As Boolean Implements IApproveAble.DoApprove
        Return MarkAsComplete()
    End Function

    Public Function DoCancel() As Boolean Implements IApproveAble.DoCancel
        Return MarkAsCancel()
    End Function
End Class
