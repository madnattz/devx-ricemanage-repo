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
Imports DevExpress.Xpo.DB

<DefaultClassOptions()> _
Public Class SeedProduct ' Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        DataOwner = GetCurrentSite()
    End Sub

    Protected Overrides Sub OnLoaded()
        MyBase.OnLoaded()
        RefTransaction.Sorting.Add(New SortProperty("TransactionDate", SortingDirection.Ascending))

    End Sub

    Protected Overrides Sub OnSaving()
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

    Dim fProductCode As String
    <Size(50)> _
    Public Property ProductCode() As String
        Get
            Return fProductCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ProductCode", fProductCode, value)
        End Set
    End Property
    Dim fProductName As String
    Public Property ProductName() As String
        Get
            Return fProductName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ProductName", fProductName, value)
        End Set
    End Property

    <VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    Public ReadOnly Property ProductNameForReport() As String
        Get
            Try
                Dim ret As String = ""
                ret = Plant.PlantName & " /" & _
                      SeedStatus.SeedStatusName & "/" & _
                      SeedType.SeedName & "/" & _
                      SeedClass.ClassName & "/" & _
                      Season.SeasonName & "/" & _
                      SeedYear
                Return ret
            Catch ex As Exception
                Return ""
            End Try
        End Get
    End Property
    Dim fPlant As Plant
    Public Property Plant() As Plant
        Get
            Return fPlant
        End Get
        Set(ByVal value As Plant)
            SetPropertyValue(Of Plant)("Plant", fPlant, value)
        End Set
    End Property
    Dim fSeedStatus As SeedStatus
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
    Public Property SeedType() As SeedType
        Get
            Return fSeedType
        End Get
        Set(ByVal value As SeedType)
            SetPropertyValue(Of SeedType)("SeedType", fSeedType, value)
        End Set
    End Property
    Dim fSeedClass As SeedClass
    Public Property SeedClass() As SeedClass
        Get
            Return fSeedClass
        End Get
        Set(ByVal value As SeedClass)
            SetPropertyValue(Of SeedClass)("SeedClass", fSeedClass, value)
        End Set
    End Property
    Dim fSeason As Season
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
    Public Property SeedYear() As String
        Get
            Return fSeedYear
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SeedYear", fSeedYear, value)
        End Set
    End Property
    Dim fMoneyType As MoneyType
    Public Property MoneyType() As MoneyType
        Get
            Return fMoneyType
        End Get
        Set(ByVal value As MoneyType)
            SetPropertyValue(Of MoneyType)("MoneyType", fMoneyType, value)
        End Set
    End Property
    Dim fLotNo As Integer
    Public Property LotNo() As Integer
        Get
            Return fLotNo
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("LotNo", fLotNo, value)
        End Set
    End Property
    Dim fIsMix As Boolean
    Public Property IsMix() As Boolean
        Get
            Return fIsMix
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("IsMix", fIsMix, value)
        End Set
    End Property
    Dim fIsFinish As Boolean
    Public Property IsFinish() As Boolean
        Get
            Return fIsFinish
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("IsFinish", fIsFinish, value)
        End Set
    End Property
    Dim fIsDelete As Boolean
    Public Property IsDelete() As Boolean
        Get
            Return fIsDelete
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("IsDelete", fIsDelete, value)
        End Set
    End Property
    Dim fTotalStockAmount As Double
    Public Property TotalStockAmount() As Double
        Get
            Return fTotalStockAmount
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("TotalStockAmount", fTotalStockAmount, value)
        End Set
    End Property
    Dim fAvailableAmount As Double
    Public Property AvailableAmount() As Double
        Get
            Return fAvailableAmount
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("AvailableAmount", fAvailableAmount, value)
        End Set
    End Property
    Dim fCollectAmount As Double
    Public Property CollectAmount() As Double
        Get
            Return fCollectAmount
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("CollectAmount", fCollectAmount, value)
        End Set
    End Property

    Dim fTotalBags As Integer
    <XafDisplayName("จำนวนคงคลัง(กระสอบ)")> _
    Public Property TotalBags() As Integer
        Get
            Return fTotalBags
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("TotalBags", fTotalBags, value)
        End Set
    End Property
    Dim fAvailableBags As Integer
    <XafDisplayName("จำนวนที่ใช้ได้(กระสอบ)")> _
    Public Property AvailableBags() As Integer
        Get
            Return fAvailableBags
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("AvailableBags", fAvailableBags, value)
        End Set
    End Property
    Dim fCollectBags As Integer
    <XafDisplayName("จำนวนสะสม(กระสอบ)")> _
    Public Property CollectBags() As Integer
        Get
            Return fCollectBags
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("CollectBags", fCollectBags, value)
        End Set
    End Property

    Enum UpdateStockType
        '//รับ
        Income
        '//จ่าย
        Outcome
        '//สำรอง
        Reserve
        '//คืนของ
        CancelReserve
        '//ขายจากการจอง
        SaleFromReserve
        '//ยกเลิกขายจากการจอง
        CancelSaleFromReserve
    End Enum

    Public Sub UpdateStockAmount(_type As UpdateStockType, _quantity As Double, _bags As Integer)
        Select Case _type
            Case UpdateStockType.Income
                Me.TotalStockAmount += _quantity
                Me.AvailableAmount += _quantity

                Me.TotalBags += _bags
                Me.AvailableBags += _bags
            Case UpdateStockType.Outcome
                Me.TotalStockAmount -= _quantity
                Me.AvailableAmount -= _quantity

                Me.TotalBags -= _bags
                Me.AvailableBags -= _bags
            Case UpdateStockType.Reserve
                Me.AvailableAmount -= _quantity
                Me.AvailableBags -= _bags
            Case UpdateStockType.CancelReserve
                Me.AvailableAmount += _quantity
                Me.AvailableBags += _bags
            Case UpdateStockType.SaleFromReserve
                Me.TotalStockAmount -= _quantity
                Me.TotalBags -= _bags
            Case UpdateStockType.CancelSaleFromReserve
                Me.TotalStockAmount += _quantity
                Me.TotalBags += _bags
        End Select
    End Sub

#Region "Referance Table"
    Private fRefTransaction As XPCollection(Of SeedTransaction)
    <XafDisplayName("รายการเคลื่อนไหวเมล็ดพันธุ์คงคลัง")> _
    Public ReadOnly Property RefTransaction() As XPCollection(Of SeedTransaction)
        Get
            Try
                If fRefTransaction Is Nothing Then
                    fRefTransaction = New XPCollection(Of SeedTransaction)(Session, CriteriaOperator.Parse("SeedProduct.Oid= ? ", Me.Oid.ToString))
                End If
                Return fRefTransaction
            Catch ex As Exception
                Return Nothing
            End Try

        End Get
    End Property

    Private fRefFarmer As BindingList(Of FactoryPickProcessDetail)
    <XafDisplayName("แหล่งที่มา (เกษตรกร)")> _
    Public ReadOnly Property RefFarmer() As BindingList(Of FactoryPickProcessDetail)
        Get
            Try

                Dim listReceiveDetail As New XPCollection(Of ReceiveSeedDetail)(Session, CriteriaOperator.Parse("SeedProduct=? and ReceiveSeed.Status='Approve'", Me))
                fRefFarmer = New BindingList(Of FactoryPickProcessDetail)
                For Each receive As ReceiveSeedDetail In listReceiveDetail
                    If receive.FactoryLot IsNot Nothing Then
                        For Each item As FactoryPickProcessDetail In receive.FactoryLot.PlantFullName.FactoryPickProcessDetails
                            Dim _fRefFarmer = New FactoryPickProcessDetail(Session)
                            _fRefFarmer = item
                            fRefFarmer.Add(_fRefFarmer)
                        Next
                    End If
                Next

                Return fRefFarmer

            Catch ex As Exception
                Return Nothing
            End Try

        End Get
    End Property

    Private fRefQualityHistotyForBuySeed As XPCollection(Of BuySeedWeight)
    <VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    <XafDisplayName("จัดซื้อ")> _
    Public ReadOnly Property RefQualityHistotyForBuySeed() As XPCollection(Of BuySeedWeight)
        Get
            Try
                Dim farmersOid As String = ""
                If RefFarmer.Count > 0 Then
                    For i As Integer = 0 To RefFarmer.Count - 1
                        If i <> RefFarmer.Count - 1 Then
                            farmersOid &= "'" & RefFarmer(i).PlanFarmer.Oid.ToString & "',"
                        Else
                            farmersOid &= "'" & RefFarmer(i).PlanFarmer.Oid.ToString & "'"
                        End If
                    Next
                End If
                Dim criteria As String = "BuySeed.BuyFarmer.PlanFarmer.Oid in (" & farmersOid & ") and BuySeed.BuyStatus='Approve' "
                fRefQualityHistotyForBuySeed = New XPCollection(Of BuySeedWeight)(Session, CriteriaOperator.Parse(criteria))

                Return fRefQualityHistotyForBuySeed

            Catch ex As Exception
                Return Nothing
            End Try

        End Get
    End Property

    'Private fRefQualityHistotyForBuySeed As XPCollection(Of ArrangeSeedLotDetail)
    '<XafDisplayName("ประวัติคุณภาพเพื่อจัดซื้อ")> _
    'Public ReadOnly Property RefQualityHistotyForBuySeed() As XPCollection(Of ArrangeSeedLotDetail)
    '    Get
    '        Try
    '            If fRefQualityHistotyForBuySeed Is Nothing Then

    '                Dim crt As String = "ArrangeSeedLot.Status='Approve' And ArrangeSeedLot.SeedType=? and ArrangeSeedLot.SeedClass=? and ArrangeSeedLot.Season=? and ArrangeSeedLot.SeedYear=? and ArrangeSeedLot.LotNo=? "
    '                ArrangeSeedLot.Status='Approve'

    '                If Me.SeedStatus.SeedStatusName.Contains("ซื้อ") Then
    '                    fRefQualityHistotyForBuySeed = New XPCollection(Of ArrangeSeedLotDetail) _
    '                                                      (Session, CriteriaOperator.Parse(crt, Me.SeedType, Me.SeedClass, Me.Season, Me.SeedYear, Me.LotNo))
    '                ElseIf Me.SeedStatus.SeedStatusName.Contains("ดี") Then
    '                    Dim objRefLot As ReferenceLotNo = Session.FindObject(Of ReferenceLotNo)(CriteriaOperator.Parse("Receive.SeedProduct=?", Me))
    '                    fRefQualityHistotyForBuySeed = New XPCollection(Of ArrangeSeedLotDetail) _
    '                                                      (Session, CriteriaOperator.Parse(crt, objRefLot.Pick.SeedProduct.SeedType, objRefLot.Pick.SeedProduct.SeedClass, objRefLot.Pick.SeedProduct.Season, objRefLot.Pick.SeedProduct.SeedYear, objRefLot.Pick.SeedProduct.LotNo))
    '                End If

    '            End If

    '            Return fRefQualityHistotyForBuySeed
    '        Catch ex As Exception
    '            Return Nothing
    '        End Try

    '    End Get
    'End Property

    Private fRefQualityHistotyBeforeProcess As XPCollection(Of QualityAudit)
    <VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    <XafDisplayName("ประวัติคุณภาพก่อนการปรับปรุงสภาพ")> _
    Public ReadOnly Property RefQualityHistotyBeforeProcess() As XPCollection(Of QualityAudit)
        Get
            Try
                If fRefQualityHistotyBeforeProcess Is Nothing Then
                    Dim crt As String = "SeedType=? and SeedClass=? and Season=? and SeedYear=? and LotNo=? and QualityAuditStep.StepName like 'ก่อน%'"
                    If Me.SeedStatus.SeedStatusName.Contains("ซื้อ") Then
                        fRefQualityHistotyBeforeProcess = New XPCollection(Of QualityAudit) _
                                                                (Session, CriteriaOperator.Parse(crt, Me.SeedType, Me.SeedClass, Me.Season, Me.SeedYear, Me.LotNo))
                    ElseIf Me.SeedStatus.SeedStatusName.Contains("ดี") Then
                        Dim objRefLot As ReferenceLotNo = Session.FindObject(Of ReferenceLotNo)(CriteriaOperator.Parse("Receive.SeedProduct=?", Me))
                        fRefQualityHistotyBeforeProcess = New XPCollection(Of QualityAudit) _
                                            (Session, CriteriaOperator.Parse(crt, objRefLot.Pick.SeedProduct.SeedType, objRefLot.Pick.SeedProduct.SeedClass, objRefLot.Pick.SeedProduct.Season, objRefLot.Pick.SeedProduct.SeedYear, objRefLot.Pick.SeedProduct.LotNo))

                    End If
                End If
                Return fRefQualityHistotyBeforeProcess

            Catch ex As Exception
                Return Nothing
            End Try

        End Get
    End Property

    Private fRefQualityHistotyAfterProcess As XPCollection(Of QualityAudit)
    <VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    <XafDisplayName("ประวัติคุณภาพหลังการปรับปรุงสภาพ")> _
    Public ReadOnly Property RefQualityHistotyAfterProcess() As XPCollection(Of QualityAudit)
        Get
            Try
                If fRefQualityHistotyAfterProcess Is Nothing Then
                    Dim crt As String = "SeedStatus=? and SeedType=? and SeedClass=? and Season=? and SeedYear=? and LotNo=? and QualityAuditStep.StepName like 'หลัง%' "
                    If Me.SeedStatus.SeedStatusName.Contains("ซื้อ") Then
                        Dim objRefLot As ReferenceLotNo = Session.FindObject(Of ReferenceLotNo)(CriteriaOperator.Parse("Pick.SeedProduct=?", Me))
                        fRefQualityHistotyAfterProcess = New XPCollection(Of QualityAudit) _
                                        (Session, CriteriaOperator.Parse(crt, objRefLot.Receive.SeedStatus, objRefLot.Receive.SeedType, objRefLot.Receive.SeedClass, objRefLot.Receive.Season, objRefLot.Receive.SeedYear, objRefLot.Receive.LotNo))

                    ElseIf Me.SeedStatus.SeedStatusName.Contains("ดี") Then
                        fRefQualityHistotyAfterProcess = New XPCollection(Of QualityAudit) _
                                        (Session, CriteriaOperator.Parse(crt, Me.SeedStatus, Me.SeedType, Me.SeedClass, Me.Season, Me.SeedYear, Me.LotNo))

                    End If
                End If
                Return fRefQualityHistotyAfterProcess

            Catch ex As Exception
                Return Nothing
            End Try

        End Get
    End Property

    Private fRefQualityKeepHistoty As XPCollection(Of QualityAudit)
    <VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    <XafDisplayName("ประวัติคุณภาพระหว่างการเก็บรักษา")> _
    Public ReadOnly Property RefQualityKeepHistoty() As XPCollection(Of QualityAudit)
        Get
            Try
                If fRefQualityKeepHistoty Is Nothing Then
                    Dim crt As String = "SeedStatus=? and SeedType=? and SeedClass=? and Season=? and SeedYear=? and LotNo=? and QualityAuditStep.StepName like '%รักษา' "
                    fRefQualityKeepHistoty = New XPCollection(Of QualityAudit) _
                                        (Session, CriteriaOperator.Parse(crt, Me.SeedStatus, Me.SeedType, Me.SeedClass, Me.Season, Me.SeedYear, Me.LotNo))
                End If
                Return fRefQualityKeepHistoty

            Catch ex As Exception
                Return Nothing
            End Try

        End Get
    End Property

#End Region
    <VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    Public ReadOnly Property QaulityHistory As SeedQualityHistory
        Get
            Dim obj As New SeedQualityHistory(Session)

            '============= เพื่อจัดซื้อ =================================================
            Dim avgBuyWet As Double = 0
            Dim avgBuyGrow As Double = 0
            Dim avgBuyRedSeed As Double = 0
            Dim avgBuyOtherSeed As Double = 0
            Dim avgBuyDate As New List(Of DateTime)

            'For Each item As ArrangeSeedLotDetail In RefQualityHistotyForBuySeed
            '    avgBuyWet += StringToDouble(item.BuySeedWeight.QualityAudit.Wet)
            '    avgBuyGrow += StringToDouble(item.BuySeedWeight.QualityAudit.Grow)
            '    avgBuyRedSeed += StringToDouble(item.BuySeedWeight.QualityAudit.RedSeed)
            '    avgBuyOtherSeed += StringToDouble(item.BuySeedWeight.QualityAudit.OtherSeed)
            '    avgBuyDate.Add(item.BuySeedWeight.QualityAudit.LabDate)

            'Next
            Dim BuyItemCount As Integer = 0
            If RefQualityHistotyForBuySeed IsNot Nothing AndAlso RefQualityHistotyForBuySeed.Count > 0 Then
                For Each item As BuySeedWeight In RefQualityHistotyForBuySeed
                    If item.QualityAudit IsNot Nothing Then
                        BuyItemCount += 1
                        avgBuyWet += StringToDouble(item.QualityAudit.Wet)
                        avgBuyGrow += StringToDouble(item.QualityAudit.Grow)
                        avgBuyRedSeed += StringToDouble(item.QualityAudit.RedSeed)
                        avgBuyOtherSeed += StringToDouble(item.QualityAudit.OtherSeed)
                        avgBuyDate.Add(item.QualityAudit.LabDate)
                    End If
                Next
            End If
            'If RefQualityHistotyForBuySeed.Count > 0 Then
            '    avgBuyWet = avgBuyWet / RefQualityHistotyForBuySeed.Count
            '    avgBuyGrow = avgBuyGrow / RefQualityHistotyForBuySeed.Count
            '    avgBuyRedSeed = avgBuyRedSeed / RefQualityHistotyForBuySeed.Count
            '    avgBuyOtherSeed = avgBuyOtherSeed / RefQualityHistotyForBuySeed.Count
            'End If

            If BuyItemCount > 0 Then
                avgBuyWet = avgBuyWet / BuyItemCount
                avgBuyGrow = avgBuyGrow / BuyItemCount
                avgBuyRedSeed = avgBuyRedSeed / BuyItemCount
                avgBuyOtherSeed = avgBuyOtherSeed / BuyItemCount
            End If

            '======================ก่อนการปรับปรุง======================================
            Dim avgBeforeWet As Double = 0
            Dim avgBeforeGrow As Double = 0
            Dim avgBeforeCompound As Double = 0
            Dim avgBeforeLabDate As New List(Of DateTime)
            For Each item As QualityAudit In RefQualityHistotyBeforeProcess
                avgBeforeWet += StringToDouble(item.Wet)
                avgBeforeGrow += StringToDouble(item.Grow)
                avgBeforeCompound += StringToDouble(item.Compound)
                avgBeforeLabDate.Add(item.LabDate)
            Next
            If RefQualityHistotyBeforeProcess.Count > 0 Then
                avgBeforeWet = avgBeforeWet / RefQualityHistotyBeforeProcess.Count
                avgBeforeGrow = avgBeforeGrow / RefQualityHistotyBeforeProcess.Count
                avgBeforeCompound = avgBeforeCompound / RefQualityHistotyBeforeProcess.Count
            End If

            '=========================หลังการปรับปรุง===================================
            Dim avgAfterWet As Double = 0
            Dim avgAfterGrow As Double = 0
            Dim avgAfterStrong As Double = 0
            Dim avgAfterPureSeed As Double = 0
            Dim avgAfterCompound As Double = 0
            Dim avgAfterOtherSeed As Double = 0
            Dim avgAfterRedSeed As Double = 0
            Dim avgAfterSeedWeight As Double = 0
            Dim avgAfterLabDate As New List(Of DateTime)
            For Each item As QualityAudit In RefQualityHistotyAfterProcess
                avgAfterWet += item.Wet
                avgAfterGrow += item.Grow
                avgAfterStrong += item.Strong
                avgAfterPureSeed += item.PureSeed
                avgAfterCompound += item.Compound
                avgAfterOtherSeed += item.OtherSeed
                avgAfterRedSeed += item.RedSeed
                avgAfterSeedWeight += item.SeedWeight
                avgAfterLabDate.Add(item.LabDate)
            Next
            If RefQualityHistotyAfterProcess.Count > 0 Then
                avgAfterWet = avgAfterWet / RefQualityHistotyAfterProcess.Count
                avgAfterGrow = avgAfterGrow / RefQualityHistotyAfterProcess.Count
                avgAfterStrong = avgAfterStrong / RefQualityHistotyAfterProcess.Count
                avgAfterPureSeed = avgAfterPureSeed / RefQualityHistotyAfterProcess.Count
                avgAfterCompound = avgAfterCompound / RefQualityHistotyAfterProcess.Count
                avgAfterOtherSeed = avgAfterOtherSeed / RefQualityHistotyAfterProcess.Count
                avgAfterRedSeed = avgAfterRedSeed / RefQualityHistotyAfterProcess.Count
                avgAfterSeedWeight = avgAfterSeedWeight / RefQualityHistotyAfterProcess.Count
            End If

            '-------------------------------populate object -------------------------
            obj.BuyWet = Math.Round(avgBuyWet, 2)
            obj.BuyGrow = avgBuyGrow
            obj.BuyOtherSeed = avgBuyOtherSeed
            obj.BuyRedSeed = avgBuyRedSeed
            If avgBuyDate.Count > 0 Then
                If Not avgBuyDate.Min.Equals(avgBuyDate.Max) Then
                    obj.BuyLabDate = avgBuyDate.Min.ToString("dd/MM/yy", New System.Globalization.CultureInfo("th-TH")) & "-" & avgBuyDate.Max.ToString("dd/MM/yy", New System.Globalization.CultureInfo("th-TH"))
                Else
                    obj.BuyLabDate = avgBuyDate.Min.ToString("dd/MM/yy", New System.Globalization.CultureInfo("th-TH"))
                End If

            End If

            '-------------------------------------------------------------------------
            obj.BeforeWet = avgBeforeWet
            obj.BeforeGrow = avgBeforeGrow
            obj.BeforeCompound = avgBeforeCompound
            If avgBeforeLabDate.Count > 0 Then
                obj.BeforeLabDate = avgBeforeLabDate.Min.ToString("dd/MM/yy", New System.Globalization.CultureInfo("th-TH"))
            End If

            '-----------------------------------------------------------------------
            obj.AfterWet = avgAfterWet
            obj.AfterGrow = avgAfterGrow
            obj.AfterStrong = avgAfterStrong
            obj.AfterPureSeed = avgAfterPureSeed
            obj.AfterCompound = avgAfterCompound
            obj.AfterOtherSeed = avgAfterOtherSeed
            obj.AfterRedSeed = avgAfterRedSeed
            obj.AfterSeedWeight = avgAfterSeedWeight
            If avgAfterLabDate.Count > 0 Then
                obj.AfterLabDate = avgAfterLabDate.Max.ToString("dd/MM/yy", New System.Globalization.CultureInfo("th-TH"))
            End If

            Return obj

        End Get
    End Property

    Public Function StringToDouble(obj As Object) As Double
        Try
            Return Math.Round(Convert.ToDouble(obj), 2)

        Catch ex As Exception
            Return 0
        End Try
    End Function

End Class
