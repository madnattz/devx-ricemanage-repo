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

'<ImageName("BO_Contact")> _
'<DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")> _
'<DefaultListViewOptions(MasterDetailMode.ListViewOnly, False, NewItemRowPosition.None)> _
'<Persistent("DatabaseTableName")> _
<DefaultClassOptions(), XafDisplayName("จัดสรรล็อตเมล็ดพันธุ์")> _
<ConditionalAppearance.Appearance("ArrangeSeedLotDisableAllItems", criteria:="Status='Approve'", Enabled:=False, TargetItems:="*", Context:="DetailView")> _
Public Class ArrangeSeedLot ' Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        'SeedYear = Date.Now.Year + 543
        '//LotNo = "001"
        ' Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).
    End Sub
    Protected Overrides Sub OnSaving()
        MyBase.OnSaving()
        If (Me.RefNO Is Nothing) Then
            '(x-xx-x-xx-25xx-xxx)
            Dim prefix As String = ""
            Dim _year As String = (Date.Now.Year + 543).ToString
            prefix = _year
            Me.fRefNO = String.Format("{0}/{1:D4}", prefix, DistributedIdGeneratorHelper.Generate(Me.Session.DataLayer, Me.GetType().FullName, prefix))
        End If
        MyBase.OnSaving()

    End Sub

    Protected Overrides Sub OnLoaded()
        MyBase.OnLoaded()
        fTotalQuantity = Nothing
        fAvgCostPerUnit = Nothing
    End Sub

    <Persistent("RefNO")> _
    Private fRefNO As String
    <PersistentAlias("fRefNO")> _
    <XafDisplayName("เลขที่อ้างอิง")> _
    Public ReadOnly Property RefNO() As String
        Get
            Return fRefNO
        End Get
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
    Dim fSeedStatus As SeedStatus
    <XafDisplayName("สถานะเมล็ดพันธุ์")> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property SeedStatus() As SeedStatus
        Get
            Return fSeedStatus
        End Get
        Set(ByVal value As SeedStatus)
            SetPropertyValue(Of SeedStatus)("SeedStatus", fSeedStatus, value)
        End Set
    End Property
    Dim fSeedType As SeedType
    <XafDisplayName("พันธุ์")> _
    <RuleRequiredField(DefaultContexts.Save)> _
    <DataSourceProperty("Plant.SeedTypes")> _
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
    <Size(4)> _
    <XafDisplayName("ปี")> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property SeedYear() As String
        Get
            Return fSeedYear
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SeedYear", fSeedYear, value)
        End Set
    End Property
    Dim fMoneyType As MoneyType
    <XafDisplayName("ปรเะเภทเงิน")> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property MoneyType() As MoneyType
        Get
            Return fMoneyType
        End Get
        Set(ByVal value As MoneyType)
            SetPropertyValue(Of MoneyType)("MoneyType", fMoneyType, value)
        End Set
    End Property
    Dim fLotNo As Integer
    <XafDisplayName("ล็อตที่")> _
    <RuleRequiredField(DefaultContexts.Save)> _
    <ModelDefault("DisPlayFormat", ("###"))> _
    <RuleValueComparison(DefaultContexts.Save, ValueComparisonType.GreaterThan, 0)> _
    Public Property LotNo() As Integer
        Get
            Return fLotNo
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("LotNo", fLotNo, value)
        End Set
    End Property

    <XafDisplayName("รหัสเมล็ดพันธุ์")> _
    Public ReadOnly Property ProductCode As String
        Get
            Try
                '1-4-01-2-1-2558-1-999
                Dim ret As String = ""
                ret = Plant.PlantID & "-" & _
                      SeedStatus.SeedStatusID & "-" & _
                      String.Format("{0:D2}", Convert.ToInt32(SeedType.SeedID)) & "-" & _
                      SeedClass.ClassID & "-" & _
                      Season.SeasonID & "-" & _
                      SeedYear & "-" & _
                      MoneyType.MoneyTypeId & "-" & _
                      LotNo.ToString("D3")
                Return ret
            Catch ex As Exception
                Return ""
            End Try
        End Get
    End Property

    '<XafDisplayName("น้ำหนักรวม(กก.)")> _
    '<PersistentAlias("ArrangeSeedLotDetails.Sum(Quantity)")> _
    '<ModelDefault("DisplayFormat", "{N2}")> _
    '<VisibleInListView(True)> _
    '<ImmediatePostData()> _
    'Public ReadOnly Property TotalQuantity() As Double
    '    Get
    '        Try
    '            Dim tempObject As Object
    '            tempObject = EvaluateAlias("TotalQuantity")
    '            Return CDbl(tempObject)
    '        Catch ex As Exception
    '            Return 0
    '        End Try
    '    End Get
    'End Property

    <Persistent("TotalQuantity")> _
    Private fTotalQuantity As Nullable(Of Double) = Nothing
    <XafDisplayName("น้ำหนักรวม(กก.)")> _
    <ModelDefault("DisplayFormat", "{N2}")> _
    <PersistentAlias("fTotalQuantity")> _
    Public ReadOnly Property TotalQuantity() As Nullable(Of Double)
        Get
            If (Not IsLoading) AndAlso (Not IsSaving) AndAlso Not fTotalQuantity.HasValue Then
                UpdateTotalQuantity(False)
            End If
            Return fTotalQuantity
        End Get
    End Property

    Public Sub UpdateTotalQuantity(ByVal forceChangeEvents As Boolean)
        Dim fOldfTotalQuantity As Nullable(Of Double) = fTotalQuantity
        fTotalQuantity = Convert.ToInt32(Evaluate(CriteriaOperator.Parse("ArrangeSeedLotDetails.Sum(Quantity)")))

        If forceChangeEvents Then
            OnChanged("TotalQuantity", fOldfTotalQuantity, fTotalQuantity)
        End If
    End Sub

    ''<PersistentAlias("ArrangeSeedLotDetails.Average(PricePerUnit)")> _
    '<XafDisplayName("ราคาซื้อคืนเฉลี่ย(บาท/กก.)")> _
    '<PersistentAlias("ArrangeSeedLotDetails.Avg(PricePerUnit)")> _
    '<ModelDefault("DisplayFormat", "{N2}")> _
    '<VisibleInListView(True)> _
    '<ImmediatePostData()> _
    'Public ReadOnly Property AvgCostPerUnit() As Double
    '    Get
    '        Try
    '            Dim tempObject As Object
    '            tempObject = EvaluateAlias("AvgCostPerUnit")
    '            Return CDbl(tempObject)
    '        Catch ex As Exception
    '            Return 0
    '        End Try
    '    End Get
    'End Property

    <Persistent("AvgCostPerUnit")> _
    Private fAvgCostPerUnit As Nullable(Of Double) = Nothing
    <XafDisplayName("น้ำหนักรวม(กก.)")> _
    <ModelDefault("DisplayFormat", "{N2}")> _
    <PersistentAlias("fAvgCostPerUnit")> _
    Public ReadOnly Property AvgCostPerUnit() As Nullable(Of Double)
        Get
            If (Not IsLoading) AndAlso (Not IsSaving) AndAlso Not fAvgCostPerUnit.HasValue Then
                UpdatefAvgCostPerUnit(False)
            End If
            Return fAvgCostPerUnit
        End Get
    End Property

    Public Sub UpdatefAvgCostPerUnit(ByVal forceChangeEvents As Boolean)
        Dim fOldAvgCostPerUnit As Nullable(Of Double) = fAvgCostPerUnit
        fAvgCostPerUnit = Convert.ToInt32(Evaluate(CriteriaOperator.Parse("ArrangeSeedLotDetails.Avg(PricePerUnit)")))

        If forceChangeEvents Then
            OnChanged("AvgCostPerUnit", fOldAvgCostPerUnit, fAvgCostPerUnit)
        End If
    End Sub

    Dim _Status As Integer
    <VisibleInDetailView(False), VisibleInListView(False)> _
    <XafDisplayName("สถานะ")> _
    Public Property Status() As PublicEnum.PublicApprove
        Get
            Return _Status
        End Get
        Set(ByVal value As PublicEnum.PublicApprove)
            SetPropertyValue(Of Integer)("Status", _Status, value)
        End Set
    End Property


    <PersistentAlias("ArrangeSeedLotDetails.Sum(PricePerUnit)")> _
    <ModelDefault("DisplayFormat", "{N}")> _
    <VisibleInListView(False), VisibleInDetailView(False)> _
    <ImmediatePostData()> _
    Public ReadOnly Property TotalPerUnit() As Double
        Get
            Try
                Dim tempObject As Object
                tempObject = EvaluateAlias("TotalPerUnit")
                Return CDbl(tempObject)
            Catch ex As Exception
                Return 0
            End Try

        End Get
    End Property

    <XafDisplayName("จำนวน(ราย)")> _
    <PersistentAlias("ArrangeSeedLotDetails.count")> _
    <ModelDefault("DisplayFormat", "{N}")> _
    <VisibleInListView(False), VisibleInDetailView(False)> _
    Public ReadOnly Property Counts() As Integer
        Get
            Try
                Dim CountObject As Object
                CountObject = EvaluateAlias("Counts")
                Return CDbl(CountObject)
            Catch ex As Exception
                Return 0
            End Try

        End Get
    End Property

    <Action(Caption:="อนุมัติ", ConfirmationMessage:="ท่านต้องการบันทึกข้อมูล ใช่หรือไม่?", ImageName:="Action_Grant", AutoCommit:=True, TargetObjectsCriteria:="Status='NotApprove'")> _
    Public Sub MarkAsApprove()
        Try
            If Not ArrangeSeedLotDetails.Count > 0 Then
                MsgBox("ไม่พบข้อมูลรายการย่อย กรุณาตรวจสอบข้อมูลอีกครั้ง", MsgBoxStyle.Information, "")
                Exit Sub
            End If
            Me._Status = PublicEnum.PublicApprove.Approve
            For i As Integer = 0 To ArrangeSeedLotDetails.Count - 1
                ArrangeSeedLotDetails(i).BuySeedWeight.BuySeed.IsSetLot = True
            Next
            MyBase.Save()
            Session.CommitTransaction()
        Catch ex As Exception

        End Try

    End Sub

    <Action(Caption:="ย้อนกลับ", ConfirmationMessage:="ท่านต้องการบันทึกข้อมูล ใช่หรือไม่?", ImageName:="Action_Deny", AutoCommit:=True, TargetObjectsCriteria:="Status='Approve'")> _
    Public Sub MarkAsCancel()
        Try
            Me._Status = PublicEnum.PublicApprove.NotApprove
            For i As Integer = 0 To ArrangeSeedLotDetails.Count - 1
                ArrangeSeedLotDetails(i).BuySeedWeight.BuySeed.IsSetLot = False
            Next
            MyBase.Save()
            Session.CommitTransaction()
        Catch ex As Exception

        End Try

    End Sub

#Region "==========Association================"
    <Association("ArrangeSeedLotDetailReferencesArrangeSeedLot", GetType(ArrangeSeedLotDetail))> _
    <XafDisplayName("รายการซื้อคืนเมล็ดพันธู์")> _
    <DevExpress.Xpo.Aggregated>
    <RuleUniqueValue(DefaultContexts.Save, TargetPropertyName:="BuySeedWeight", CustomMessageTemplate:="ไม่สามารถเลือกข้อมูลการซื้อคืนซ้ำกันได้")> _
    Public ReadOnly Property ArrangeSeedLotDetails() As XPCollection(Of ArrangeSeedLotDetail)
        Get
            Return GetCollection(Of ArrangeSeedLotDetail)("ArrangeSeedLotDetails")
        End Get
    End Property
#End Region

End Class