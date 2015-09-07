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

<RuleCombinationOfPropertiesIsUnique("Plant,SeedType,SeedClass,Season,SeedYear,Lot", _
    CustomMessageTemplate:="มีข้อมูลเป้าการผลิตนี้แล้ว กรุณเลือกข้อมูลเป้าการผลิตอื่น")> _
<DefaultClassOptions()> _
Public Class MainPlan
    Inherits BaseObject
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        DataOwner = GetCurrentSite()
    End Sub

    Protected Overrides Sub OnSaving()
        If (Me.fPlanRefNo Is Nothing) Then
            Dim prefix As String = ""
            Dim _year As String = (Date.Now.Year + 543).ToString
            prefix = _year

            Me.fPlanRefNo = String.Format("{0}/{1:D4}", prefix, DistributedIdGeneratorHelper.Generate( _
                                          Me.Session.DataLayer, Me.GetType().FullName, prefix))

        End If

        If Me.DataOwner Is Nothing Then
            DataOwner = GetCurrentSite()
        End If

        Me.LastUodateDate = Date.Now
        Me.LastUpdateBy = CType(SecuritySystem.CurrentUser, User).DisplayName

        MyBase.OnSaving()
    End Sub

    Protected Overrides Sub OnSaved()
        MyBase.OnSaved()

        Try
            If Not IsDeleted Then

                Dim objGrowPlan As GrowPlan = Session.FindObject(Of GrowPlan)(CriteriaOperator.Parse("MainPlan=?", Me))
                If objGrowPlan Is Nothing Then
                    objGrowPlan = New GrowPlan(Session)
                    objGrowPlan.MainPlan = Me
                    objGrowPlan.Save()
                End If

                Dim objHarvestPlan As HarvestPlan = Session.FindObject(Of HarvestPlan)(CriteriaOperator.Parse("MainPlan=?", Me))
                If objHarvestPlan Is Nothing Then
                    objHarvestPlan = New HarvestPlan(Session)
                    objHarvestPlan.MainPlan = Me
                    objHarvestPlan.Save()
                End If

                Dim objBuyPlan As BuyPlan = Session.FindObject(Of BuyPlan)(CriteriaOperator.Parse("MainPlan=?", Me))
                If objBuyPlan Is Nothing Then
                    objBuyPlan = New BuyPlan(Session)
                    objBuyPlan.MainPlan = Me
                    objBuyPlan.Save()
                End If

                Dim objAuditPlan As AuditActivityPlan = Session.FindObject(Of AuditActivityPlan)(CriteriaOperator.Parse("MainPlan=?", Me))
                If objAuditPlan Is Nothing Then
                    objAuditPlan = New AuditActivityPlan(Session)
                    objAuditPlan.MainPlan = Me
                    objAuditPlan.Save()
                End If
            Else

                DeleteBuyPlan(Session)
                DeleteGrowPlan(Session)
                DeleteHarvestPlan(Session)
                DeleteAuditPlan(Session)

            End If

            Session.CommitTransaction()
        Catch ex As Exception
            Session.RollbackTransaction()
        End Try
    End Sub

    Protected Overrides Sub OnDeleted()
        MyBase.OnDeleted()
        Session.Delete(PlanFarmerGroups)
        'Session.Delete(AuditTrail)
        'DeleteBuyPlan(Session)
        'DeleteGrowPlan(Session)
        'DeleteHarvestPlan(Session)
        'DeleteAuditPlan(Session)

    End Sub

    'Protected Overrides Sub OnDeleting()
    '    MyBase.OnDeleting()
    '    DeleteBuyPlan(Session)
    '    DeleteGrowPlan(Session)
    '    DeleteHarvestPlan(Session)
    '    DeleteAuditPlan(Session)

    'End Sub

    Public Sub DeleteBuyPlan(_session As Session)
        Dim objPlan As BuyPlan = _session.FindObject(Of BuyPlan)(CriteriaOperator.Parse("MainPlan=?", Me))
        If objPlan IsNot Nothing Then
            _session.Delete(objPlan.BuyPlanDetails)
            _session.Delete(objPlan.AuditTrail)
            objPlan.Delete()
        End If
    End Sub

    Public Sub DeleteGrowPlan(_session As Session)
        Dim objPlan As GrowPlan = _session.FindObject(Of GrowPlan)(CriteriaOperator.Parse("MainPlan=?", Me))
        If objPlan IsNot Nothing Then
            _session.Delete(objPlan.GrowPlanDetails)
            _session.Delete(objPlan.AuditTrail)
            objPlan.Delete()
        End If
    End Sub

    Public Sub DeleteHarvestPlan(_session As Session)
        Dim objPlan As HarvestPlan = _session.FindObject(Of HarvestPlan)(CriteriaOperator.Parse("MainPlan=?", Me))
        If objPlan IsNot Nothing Then
            _session.Delete(objPlan.HarvestPlanDetails)
            _session.Delete(objPlan.AuditTrail)
            objPlan.Delete()
        End If
    End Sub

    Public Sub DeleteAuditPlan(_session As Session)
        Dim objPlan As AuditActivityPlan = _session.FindObject(Of AuditActivityPlan)(CriteriaOperator.Parse("MainPlan=?", Me))
        If objPlan IsNot Nothing Then
            _session.Delete(objPlan.AuditGrowPlanDetails)
            _session.Delete(objPlan.AuditCheckFarmPlanDetails)
            _session.Delete(objPlan.AuditBuyPlanDetails)
            _session.Delete(objPlan.AuditKeepPlanDetails)
            _session.Delete(objPlan.AuditProcessPlanDetails)
            _session.Delete(objPlan.AuditSalePlanDetails)
            objPlan.Delete()
        End If
    End Sub

    Protected Overrides Sub OnLoaded()
        MyBase.OnLoaded()
        'GrowPlans.Sorting.Add(New SortProperty("TimeStamp", DB.SortingDirection.Ascending))
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

    <Persistent("PlanRefNo")> _
    Private fPlanRefNo As String
    <PersistentAlias("fPlanRefNo")> _
    Public ReadOnly Property PlanRefNo() As String
        Get
            Return fPlanRefNo
        End Get
    End Property

    Dim fPlant As Plant
    <RuleRequiredField("MainPlan.Plant", DefaultContexts.Save)> _
    Public Property Plant() As Plant
        Get
            Return fPlant
        End Get
        Set(ByVal value As Plant)
            SetPropertyValue(Of Plant)("Plant", fPlant, value)
        End Set
    End Property
    Dim fSeedType As SeedType
    <RuleRequiredField("MainPlan.SeedType", DefaultContexts.Save)> _
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
    <RuleRequiredField("MainPlan.SeedClass", DefaultContexts.Save)> _
    Public Property SeedClass() As SeedClass
        Get
            Return fSeedClass
        End Get
        Set(ByVal value As SeedClass)
            SetPropertyValue(Of SeedClass)("SeedClass", fSeedClass, value)
        End Set
    End Property
    Dim fSeason As Season
    <RuleRequiredField("MainPlan.Season", DefaultContexts.Save)> _
    Public Property Season() As Season
        Get
            Return fSeason
        End Get
        Set(ByVal value As Season)
            SetPropertyValue(Of Season)("Season", fSeason, value)
        End Set
    End Property
    Dim fSeedYear As String
    <RuleRequiredField("MainPlan.SeedYear", DefaultContexts.Save)> _
    Public Property SeedYear() As String
        Get
            Return fSeedYear
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SeedYear", fSeedYear, value)
        End Set
    End Property

    Dim fLot As String
    <RuleRequiredField("MainPlan.Lot", DefaultContexts.Save)> _
    Public Property Lot() As String
        Get
            Return fLot
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Lot", fLot, value)
        End Set
    End Property

    Public ReadOnly Property FullName As String
        Get
            Dim ret_val As String = ""
            Try
                ret_val = Plant.PlantName & " / " & SeedType.SeedName & " / " & SeedClass.ClassName & " / " & Season.SeasonName & " / " & SeedYear & " / รุ่นที่ " & Lot
            Catch ex As Exception

            End Try
            Return ret_val
        End Get
    End Property

    Dim fTotalGoal As Integer
    <RuleValueComparison("MainPlan.TotalGoal", DefaultContexts.Save, ValueComparisonType.GreaterThan, 0)> _
    <ModelDefault("DisPlayFormat", ("{0:#,##}"))> _
    Public Property TotalGoal() As Integer
        Get
            Return fTotalGoal
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("TotalGoal", fTotalGoal, value)
        End Set
    End Property

    Dim fBuySeedType As PublicEnum.BuySeedType
    '<RuleValueComparison("MainPlan.TotalGoal", DefaultContexts.Save, ValueComparisonType.GreaterThan, 0)> _
    <XafDisplayName("ลักษณะเมล็ดพันธุ์ที่จัดซื้อคืน")> _
    Public Property BuySeedType() As PublicEnum.BuySeedType
        Get
            Return fBuySeedType
        End Get
        Set(ByVal value As PublicEnum.BuySeedType)
            SetPropertyValue(Of PublicEnum.BuySeedType)("BuySeedType", fBuySeedType, value)
        End Set
    End Property

    Dim fLastUpdateBy As String
    Public Property LastUpdateBy() As String
        Get
            Return fLastUpdateBy
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("LastUpdateBy", fLastUpdateBy, value)
        End Set
    End Property
    Dim fLastUodateDate As DateTime
    Public Property LastUodateDate() As DateTime
        Get
            Return fLastUodateDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("LastUodateDate", fLastUodateDate, value)
        End Set
    End Property

    Private fPlanStatus As PublicEnum.PlanStatus
    Public Property PlanStatus() As PublicEnum.PlanStatus
        Get
            Return fPlanStatus
        End Get
        Set(ByVal value As PublicEnum.PlanStatus)
            SetPropertyValue("PlanStatus", fPlanStatus, value)
        End Set
    End Property

    '<Association("PlanFarmerReferencesMainPlan", GetType(PlanFarmer))> _
    'Public ReadOnly Property PlanFarmers() As XPCollection(Of PlanFarmer)
    '    Get
    '        Return GetCollection(Of PlanFarmer)("PlanFarmers")
    '    End Get
    'End Property

    <XafDisplayName("เป้าซื้อคืน รวม (กก.)")> _
    Public ReadOnly Property SumMaxBuyQuantity As Double
        Get
            Dim ret As Double = 0
            For Each item As PlanFarmerGroup In PlanFarmerGroups
                ret += item.SumQuantity
            Next
            Return ret
        End Get
    End Property

    <XafDisplayName("ซื้อคืนแล้ว รวม (กก.)")> _
    Public ReadOnly Property SumBuyQuantity As Double
        Get
            Dim ret As Double = 0
            For Each item As PlanFarmerGroup In PlanFarmerGroups
                For Each _farmer As PlanFarmer In item.PlanFarmers
                    ret += _farmer.SumBuyAmount
                Next
            Next
            Return ret
        End Get
    End Property

    <XafDisplayName("คงเหลือซื้อคืนได้ (กก.)")> _
    Public ReadOnly Property AvailableBuyQuantity As Double
        Get
            Return SumMaxBuyQuantity - SumBuyQuantity
        End Get
    End Property

    <VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    Public ReadOnly Property GroupCount As Integer
        Get
            Return PlanFarmerGroups.Count
        End Get
    End Property

    <VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    Public ReadOnly Property FarmerCount As Integer
        Get
            Dim ret As Integer = 0
            For Each item As PlanFarmerGroup In PlanFarmerGroups
                ret += item.SumMember
            Next
            Return ret
        End Get
    End Property

    '------------------------ข้อมูลสำหรับออก ขพ.5------------------------------------------
    'Dim fSeedSourceTotalQuantity As Double
    <XafDisplayName("จำนวนเมล็ดพันธุ์สำหรับจัดทำแปลง รวม (กก.)")> _
    Public ReadOnly Property SeedSourceTotalQuantity() As Integer
        Get
            Dim ret As Integer = 0
            Try

                For Each fGroup As PlanFarmerGroup In PlanFarmerGroups
                    For Each fFarmar As PlanFarmer In fGroup.PlanFarmers
                        For Each seedReceive As PlanFarmerSeedSouce In fFarmar.PlanFarmerSeedSouces
                            ret += seedReceive.SeedReceive
                        Next
                    Next
                Next
            Catch ex As Exception

            End Try

            Return ret
        End Get
    End Property

    <VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    Public ReadOnly Property SeedSourcesText As String
        Get
            Dim listOfSeedSource As New List(Of String)

            Dim queryResult As SelectedData = Session.ExecuteSproc("SP_Plan_GetSeedSource", New OperandValue(Me.Oid.ToString))

            For Each row As SelectStatementResultRow In queryResult.ResultSet(0).Rows
                Console.WriteLine(row.Values(0))
                listOfSeedSource.Add("ฤดู   " & row.Values(1) & "      ปี   " & row.Values(2) & "     จาก   " & row.Values(0) & "    จำนวน  " & ConvertToNumberWithFormat(row.Values(3)) & "  กิโลกรัม")
            Next row

            listOfSeedSource = listOfSeedSource.Distinct.ToList

            Dim ret As String = ""
            For i As Integer = 0 To listOfSeedSource.Count - 1
                If i <> listOfSeedSource.Count - 1 Then
                    ret &= listOfSeedSource(i) & vbCrLf
                Else
                    ret &= listOfSeedSource(i)
                End If
            Next
            Return ret
        End Get
    End Property

    <VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    Public ReadOnly Property SeedSources As String
        Get
            Dim listOfSeedSource As New List(Of String)

            Dim queryResult As SelectedData = Session.ExecuteSproc("SP_Plan_GetSeedSource", New OperandValue(Me.Oid.ToString))

            For Each row As SelectStatementResultRow In queryResult.ResultSet(0).Rows
                Console.WriteLine(row.Values(0))
                listOfSeedSource.Add(row.Values(0))
            Next row

            listOfSeedSource = listOfSeedSource.Distinct.ToList

            Dim ret As String = ""
            For i As Integer = 0 To listOfSeedSource.Count - 1
                If i <> listOfSeedSource.Count - 1 Then
                    ret &= listOfSeedSource(i) & vbCrLf
                Else
                    ret &= listOfSeedSource(i)
                End If
            Next
            Return ret
        End Get
    End Property

    Dim fSeedResultPerSeedSource As String
    <VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    Public ReadOnly Property SeedPrice() As SeedPrice
        Get
            Try
                Dim mPlan As MainPlan = Me
                Dim crt As String = "SeedType=? and SeedClass=? and Season=? and SeedYear=?"
                Dim objSeedPrice As SeedPrice = Session.FindObject(Of SeedPrice)(CriteriaOperator.Parse(crt, mPlan.SeedType, mPlan.SeedClass, mPlan.Season, mPlan.SeedYear))
                If Not objSeedPrice Is Nothing Then
                    Return objSeedPrice
                Else
                    Return Nothing
                End If
            Catch ex As Exception
                Return Nothing
            End Try
        End Get
    End Property

    Public Function ConvertToNumberWithFormat(obj As Object) As String
        Try
            Dim ret As String = ""

            Return Convert.ToInt32(obj).ToString("#,#")

        Catch ex As Exception

        End Try
    End Function

    Dim fSeedSourceReceiveValue As Double
    <XafDisplayName("มูลค่าเมล็ดพันธุ์ที่ได้รับจัดสรร (บาท)")> _
    Public Property SeedSourceReceiveValue() As Double
        Get
            Return fSeedSourceReceiveValue
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("SeedSourceReceiveValue", fSeedSourceReceiveValue, value)
        End Set
    End Property

    Dim fSeedSourceTotalValue As Double
    <XafDisplayName("มูลค่าเมล็ดพันธุ์ที่ใช้ไป(บาท)")> _
    Public Property SeedSourceTotalValue() As Double
        Get
            Return fSeedSourceTotalValue
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("SeedSourceTotalValue", fSeedSourceTotalValue, value)
        End Set
    End Property

    <XafDisplayName("มูลค่าเมล็ดพันธุ์ที่คงเหลือ(บาท)")> _
    Public ReadOnly Property SeedSourceBeyondValue() As Double
        Get
            Return fSeedSourceReceiveValue - SeedSourceTotalValue
        End Get
    End Property

    Dim fPlanAdminUser As String
    <XafDisplayName("พนักงานควบคุมแปลง")> _
    Public Property PlanAdminUser() As String
        Get
            Return fPlanAdminUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PlanAdminUser", fPlanAdminUser, value)
        End Set
    End Property

    Dim fPlanAdminPosition As String
    <XafDisplayName("ตำแหน่ง")> _
    Public Property PlanAdminPosition() As String
        Get
            Return fPlanAdminPosition
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PlanAdminPosition", fPlanAdminPosition, value)
        End Set
    End Property

    Dim fPlanApproveUser As String
    <XafDisplayName("หัวหน้าพนักงานควบคุมแปลง")> _
    Public Property PlanApproveUser() As String
        Get
            Return fPlanApproveUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PlanApproveUser", fPlanApproveUser, value)
        End Set
    End Property

    Dim fPlanApprovePosition As String
    <XafDisplayName("ตำแหน่ง")> _
    Public Property PlanApprovePosition() As String
        Get
            Return fPlanApprovePosition
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PlanApprovePosition", fPlanApprovePosition, value)
        End Set
    End Property

    '-----------------------------------------------------------------------------------
    <VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    Public ReadOnly Property AreaCount As Integer
        Get
            Dim ret As Integer = 0
            For Each item As PlanFarmerGroup In PlanFarmerGroups
                ret += item.SumArea
            Next
            Return ret
        End Get
    End Property

    <VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    Public ReadOnly Property PlanAreaCount As Integer
        Get
            Dim ret As Integer = 0
            Dim collPlan As New XPCollection(Of AuditGrowPlanDetail)(Session, CriteriaOperator.Parse("AuditActivityPlan.MainPlan=?", Me))
            For Each item As AuditGrowPlanDetail In collPlan
                ret += item.Quantity
            Next

            Return ret
        End Get
    End Property

    <XafDisplayName("กลุ่มเกษตรกร")> _
    <ImmediatePostData> _
    <Association("MainPlan-PlanFarmerGroups")> _
    <RuleUniqueValue(DefaultContexts.Save, TargetPropertyName:="FarmerGroup", CustomMessageTemplate:="ไม่สามารถเลือกกลุ่มเกษตรกรซ้ำกันได้")> _
    Public ReadOnly Property PlanFarmerGroups() As XPCollection(Of PlanFarmerGroup)
        Get
            Return GetCollection(Of PlanFarmerGroup)("PlanFarmerGroups")
        End Get
    End Property

    Private _auditTrail As XPCollection(Of AuditDataItemPersistent)
    <XafDisplayName("ประวัติการแก้ไขข้อมูล")> _
    Public ReadOnly Property AuditTrail() As XPCollection(Of AuditDataItemPersistent)
        Get
            If _auditTrail Is Nothing Then
                _auditTrail = AuditedObjectWeakReference.GetAuditTrail(Session, Me)
            End If
            Return _auditTrail
        End Get
    End Property

    <Action(Caption:="เสร็จสิ้น", ConfirmationMessage:="ท่านต้องการบันทึกข้อมูล ใช่หรือไม่?", ImageName:="Action_Grant", AutoCommit:=True, TargetObjectsCriteria:="PlanStatus='Pending'")> _
    Public Sub MarkAsComplete()
        If Not IsDeleted Then
            Me.PlanStatus = PublicEnum.PlanStatus.Done
            Session.CommitTransaction()
        End If
    End Sub

    <Action(Caption:="คืนค่า", ConfirmationMessage:="ท่านต้องการบันทึกข้อมูล ใช่หรือไม่?", ImageName:="Action_Deny", AutoCommit:=True, TargetObjectsCriteria:="PlanStatus='Done'")> _
    Public Sub MarkAsCancel()
        If Not IsDeleted Then
            If Not IsDeleted Then
                Me.PlanStatus = PublicEnum.PlanStatus.Pending
                Session.CommitTransaction()
            End If
        End If
    End Sub

    <XafDisplayName("แผนและผลการจัดทำแปลง")> _
    <VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    Public ReadOnly Property ActivityPlans() As BindingList(Of ActivityPlan)
        Get
            Dim collReport As New BindingList(Of ActivityPlan)
            InsertPlanList(collReport)

            Return collReport
        End Get
    End Property

    Public Sub InsertPlanList(objList As BindingList(Of ActivityPlan))

        'Dim objResult As New ActivityPlan(Session)

        Dim objGrowPlan As GrowPlan = Session.FindObject(Of GrowPlan)(CriteriaOperator.Parse("MainPlan=?", Me))
        Dim objHarvestPlan As HarvestPlan = Session.FindObject(Of HarvestPlan)(CriteriaOperator.Parse("MainPlan=?", Me))
        Dim objBuyPlan As BuyPlan = Session.FindObject(Of BuyPlan)(CriteriaOperator.Parse("MainPlan=?", Me))

        Dim objPlan As New ActivityPlan(Session)

        Dim objAPR As New ActivePlanDetailObject
        Dim objAUG As New ActivePlanDetailObject
        Dim objDEC As New ActivePlanDetailObject
        Dim objFEB As New ActivePlanDetailObject
        Dim objJAN As New ActivePlanDetailObject
        Dim objJUL As New ActivePlanDetailObject
        Dim objJUN As New ActivePlanDetailObject
        Dim objMAR As New ActivePlanDetailObject
        Dim objMAY As New ActivePlanDetailObject
        Dim objNOV As New ActivePlanDetailObject
        Dim objOCT As New ActivePlanDetailObject
        Dim objSEP As New ActivePlanDetailObject

        For Each item As GrowPlanDetail In objGrowPlan.GrowPlanDetails
            Select Case item.MonthNo
                Case PublicEnum.EnumMonth.APR
                    Select Case item.WeekNo
                        Case PublicEnum.WeekNo.OneAndTwo
                            objAPR.GrowSize1 = item.Quantity
                            objAPR.GrowActual1 = item.ActualQuantity
                        Case PublicEnum.WeekNo.ThreeAndFour
                            objAPR.GrowSize2 = item.Quantity
                            objAPR.GrowActual2 = item.ActualQuantity
                    End Select
                Case PublicEnum.EnumMonth.AUG
                    Select Case item.WeekNo
                        Case PublicEnum.WeekNo.OneAndTwo
                            objAUG.GrowSize1 = item.Quantity
                            objAUG.GrowActual1 = item.ActualQuantity
                        Case PublicEnum.WeekNo.ThreeAndFour
                            objAUG.GrowSize2 = item.Quantity
                            objAUG.GrowActual2 = item.ActualQuantity
                    End Select
                Case PublicEnum.EnumMonth.DEC
                    Select Case item.WeekNo
                        Case PublicEnum.WeekNo.OneAndTwo
                            objDEC.GrowSize1 = item.Quantity
                            objDEC.GrowActual1 = item.ActualQuantity
                        Case PublicEnum.WeekNo.ThreeAndFour
                            objDEC.GrowSize2 = item.Quantity
                            objDEC.GrowActual2 = item.ActualQuantity
                    End Select
                Case PublicEnum.EnumMonth.FEB
                    Select Case item.WeekNo
                        Case PublicEnum.WeekNo.OneAndTwo
                            objFEB.GrowSize1 = item.Quantity
                            objFEB.GrowActual1 = item.ActualQuantity
                        Case PublicEnum.WeekNo.ThreeAndFour
                            objFEB.GrowSize2 = item.Quantity
                            objFEB.GrowActual2 = item.ActualQuantity
                    End Select
                Case PublicEnum.EnumMonth.JAN
                    Select Case item.WeekNo
                        Case PublicEnum.WeekNo.OneAndTwo
                            objJAN.GrowSize1 = item.Quantity
                            objJAN.GrowActual1 = item.ActualQuantity
                        Case PublicEnum.WeekNo.ThreeAndFour
                            objJAN.GrowSize2 = item.Quantity
                            objJAN.GrowActual2 = item.ActualQuantity
                    End Select
                Case PublicEnum.EnumMonth.JUL
                    Select Case item.WeekNo
                        Case PublicEnum.WeekNo.OneAndTwo
                            objJUL.GrowSize1 = item.Quantity
                            objJUL.GrowActual1 = item.ActualQuantity
                        Case PublicEnum.WeekNo.ThreeAndFour
                            objJUL.GrowSize2 = item.Quantity
                            objJUL.GrowActual2 = item.ActualQuantity
                    End Select
                Case PublicEnum.EnumMonth.JUN
                    Select Case item.WeekNo
                        Case PublicEnum.WeekNo.OneAndTwo
                            objJUN.GrowSize1 = item.Quantity
                            objJUN.GrowActual1 = item.ActualQuantity
                        Case PublicEnum.WeekNo.ThreeAndFour
                            objJUN.GrowSize2 = item.Quantity
                            objJUN.GrowActual2 = item.ActualQuantity
                    End Select
                Case PublicEnum.EnumMonth.MAR
                    Select Case item.WeekNo
                        Case PublicEnum.WeekNo.OneAndTwo
                            objMAR.GrowSize1 = item.Quantity
                            objMAR.GrowActual1 = item.ActualQuantity
                        Case PublicEnum.WeekNo.ThreeAndFour
                            objMAR.GrowSize2 = item.Quantity
                            objMAR.GrowActual2 = item.ActualQuantity
                    End Select
                Case PublicEnum.EnumMonth.MAY
                    Select Case item.WeekNo
                        Case PublicEnum.WeekNo.OneAndTwo
                            objMAY.GrowSize1 = item.Quantity
                            objMAY.GrowActual1 = item.ActualQuantity
                        Case PublicEnum.WeekNo.ThreeAndFour
                            objMAY.GrowSize2 = item.Quantity
                            objMAY.GrowActual2 = item.ActualQuantity
                    End Select
                Case PublicEnum.EnumMonth.NOV
                    Select Case item.WeekNo
                        Case PublicEnum.WeekNo.OneAndTwo
                            objNOV.GrowSize1 = item.Quantity
                            objNOV.GrowActual1 = item.ActualQuantity
                        Case PublicEnum.WeekNo.ThreeAndFour
                            objNOV.GrowSize2 = item.Quantity
                            objNOV.GrowActual2 = item.ActualQuantity
                    End Select
                Case PublicEnum.EnumMonth.OCT
                    Select Case item.WeekNo
                        Case PublicEnum.WeekNo.OneAndTwo
                            objOCT.GrowSize1 = item.Quantity
                            objOCT.GrowActual1 = item.ActualQuantity
                        Case PublicEnum.WeekNo.ThreeAndFour
                            objOCT.GrowSize2 = item.Quantity
                            objOCT.GrowActual2 = item.ActualQuantity
                    End Select
                Case PublicEnum.EnumMonth.SEP
                    Select Case item.WeekNo
                        Case PublicEnum.WeekNo.OneAndTwo
                            objSEP.GrowSize1 = item.Quantity
                            objSEP.GrowActual1 = item.ActualQuantity
                        Case PublicEnum.WeekNo.ThreeAndFour
                            objSEP.GrowSize2 = item.Quantity
                            objSEP.GrowActual2 = item.ActualQuantity
                    End Select
            End Select
        Next

        '==============================================================
        For Each item As HarvestPlanDetail In objHarvestPlan.HarvestPlanDetails
            Select Case item.MonthNo
                Case PublicEnum.EnumMonth.APR
                    Select Case item.WeekNo
                        Case PublicEnum.WeekNo.OneAndTwo
                            objAPR.HarvestSize1 = item.Quantity
                            objAPR.HarvestActual1 = item.ActualQuantity
                        Case PublicEnum.WeekNo.ThreeAndFour
                            objAPR.HarvestSize2 = item.Quantity
                            objAPR.HarvestActual2 = item.ActualQuantity
                    End Select
                Case PublicEnum.EnumMonth.AUG
                    Select Case item.WeekNo
                        Case PublicEnum.WeekNo.OneAndTwo
                            objAUG.HarvestSize1 = item.Quantity
                            objAUG.HarvestActual1 = item.ActualQuantity
                        Case PublicEnum.WeekNo.ThreeAndFour
                            objAUG.HarvestSize2 = item.Quantity
                            objAUG.HarvestActual2 = item.ActualQuantity
                    End Select
                Case PublicEnum.EnumMonth.DEC
                    Select Case item.WeekNo
                        Case PublicEnum.WeekNo.OneAndTwo
                            objDEC.HarvestSize1 = item.Quantity
                            objDEC.HarvestActual1 = item.ActualQuantity
                        Case PublicEnum.WeekNo.ThreeAndFour
                            objDEC.HarvestSize2 = item.Quantity
                            objDEC.HarvestActual2 = item.ActualQuantity
                    End Select
                Case PublicEnum.EnumMonth.FEB
                    Select Case item.WeekNo
                        Case PublicEnum.WeekNo.OneAndTwo
                            objFEB.HarvestSize1 = item.Quantity
                            objFEB.HarvestActual1 = item.ActualQuantity
                        Case PublicEnum.WeekNo.ThreeAndFour
                            objFEB.HarvestSize2 = item.Quantity
                            objFEB.HarvestActual2 = item.ActualQuantity
                    End Select
                Case PublicEnum.EnumMonth.JAN
                    Select Case item.WeekNo
                        Case PublicEnum.WeekNo.OneAndTwo
                            objJAN.HarvestSize1 = item.Quantity
                            objJAN.HarvestActual1 = item.ActualQuantity
                        Case PublicEnum.WeekNo.ThreeAndFour
                            objJAN.HarvestSize2 = item.Quantity
                            objJAN.HarvestActual2 = item.ActualQuantity
                    End Select
                Case PublicEnum.EnumMonth.JUL
                    Select Case item.WeekNo
                        Case PublicEnum.WeekNo.OneAndTwo
                            objJUL.HarvestSize1 = item.Quantity
                            objJUL.HarvestActual1 = item.ActualQuantity
                        Case PublicEnum.WeekNo.ThreeAndFour
                            objJUL.HarvestSize2 = item.Quantity
                            objJUL.HarvestActual2 = item.ActualQuantity
                    End Select
                Case PublicEnum.EnumMonth.JUN
                    Select Case item.WeekNo
                        Case PublicEnum.WeekNo.OneAndTwo
                            objJUN.HarvestSize1 = item.Quantity
                            objJUN.HarvestActual1 = item.ActualQuantity
                        Case PublicEnum.WeekNo.ThreeAndFour
                            objJUN.HarvestSize2 = item.Quantity
                            objJUN.HarvestActual2 = item.ActualQuantity
                    End Select
                Case PublicEnum.EnumMonth.MAR
                    Select Case item.WeekNo
                        Case PublicEnum.WeekNo.OneAndTwo
                            objMAR.HarvestSize1 = item.Quantity
                            objMAR.HarvestActual1 = item.ActualQuantity
                        Case PublicEnum.WeekNo.ThreeAndFour
                            objMAR.HarvestSize2 = item.Quantity
                            objMAR.HarvestActual2 = item.ActualQuantity
                    End Select
                Case PublicEnum.EnumMonth.MAY
                    Select Case item.WeekNo
                        Case PublicEnum.WeekNo.OneAndTwo
                            objMAY.HarvestSize1 = item.Quantity
                            objMAY.HarvestActual1 = item.ActualQuantity
                        Case PublicEnum.WeekNo.ThreeAndFour
                            objMAY.HarvestSize2 = item.Quantity
                            objMAY.HarvestActual2 = item.ActualQuantity
                    End Select
                Case PublicEnum.EnumMonth.NOV
                    Select Case item.WeekNo
                        Case PublicEnum.WeekNo.OneAndTwo
                            objNOV.HarvestSize1 = item.Quantity
                            objNOV.HarvestActual1 = item.ActualQuantity
                        Case PublicEnum.WeekNo.ThreeAndFour
                            objNOV.HarvestSize2 = item.Quantity
                            objNOV.HarvestActual2 = item.ActualQuantity
                    End Select
                Case PublicEnum.EnumMonth.OCT
                    Select Case item.WeekNo
                        Case PublicEnum.WeekNo.OneAndTwo
                            objOCT.HarvestSize1 = item.Quantity
                            objOCT.HarvestActual1 = item.ActualQuantity
                        Case PublicEnum.WeekNo.ThreeAndFour
                            objOCT.HarvestSize2 = item.Quantity
                            objOCT.HarvestActual2 = item.ActualQuantity
                    End Select
                Case PublicEnum.EnumMonth.SEP
                    Select Case item.WeekNo
                        Case PublicEnum.WeekNo.OneAndTwo
                            objSEP.HarvestSize1 = item.Quantity
                            objSEP.HarvestActual1 = item.ActualQuantity
                        Case PublicEnum.WeekNo.ThreeAndFour
                            objSEP.HarvestSize2 = item.Quantity
                            objSEP.HarvestActual2 = item.ActualQuantity
                    End Select
            End Select
        Next

        '==============================================================
        For Each item As BuyPlanDetail In objBuyPlan.BuyPlanDetails
            Select Case item.MonthNo
                Case PublicEnum.EnumMonth.APR
                    Select Case item.WeekNo
                        Case PublicEnum.WeekNo.OneAndTwo
                            objAPR.BuyQuantity1 = Math.Round(item.Quantity / 1000, 2)
                            objAPR.BuyActual1 = Math.Round(item.ActualQuantity / 1000, 2)
                        Case PublicEnum.WeekNo.ThreeAndFour
                            objAPR.BuyQuantity2 = Math.Round(item.Quantity / 1000, 2)
                            objAPR.BuyActual2 = Math.Round(item.ActualQuantity / 1000, 2)
                    End Select
                Case PublicEnum.EnumMonth.AUG
                    Select Case item.WeekNo
                        Case PublicEnum.WeekNo.OneAndTwo
                            objAUG.BuyQuantity1 = Math.Round(item.Quantity / 1000, 2)
                            objAUG.BuyActual1 = Math.Round(item.ActualQuantity / 1000, 2)
                        Case PublicEnum.WeekNo.ThreeAndFour
                            objAUG.BuyQuantity2 = Math.Round(item.Quantity / 1000, 2)
                            objAUG.BuyActual2 = Math.Round(item.ActualQuantity / 1000, 2)
                    End Select
                Case PublicEnum.EnumMonth.DEC
                    Select Case item.WeekNo
                        Case PublicEnum.WeekNo.OneAndTwo
                            objDEC.BuyQuantity1 = Math.Round(item.Quantity / 1000, 2)
                            objDEC.BuyActual1 = Math.Round(item.ActualQuantity / 1000, 2)
                        Case PublicEnum.WeekNo.ThreeAndFour
                            objDEC.BuyQuantity2 = Math.Round(item.Quantity / 1000, 2)
                            objDEC.BuyActual2 = Math.Round(item.ActualQuantity / 1000, 2)
                    End Select
                Case PublicEnum.EnumMonth.FEB
                    Select Case item.WeekNo
                        Case PublicEnum.WeekNo.OneAndTwo
                            objFEB.BuyQuantity1 = Math.Round(item.Quantity / 1000, 2)
                            objFEB.BuyActual1 = Math.Round(item.ActualQuantity / 1000, 2)
                        Case PublicEnum.WeekNo.ThreeAndFour
                            objFEB.BuyQuantity2 = Math.Round(item.Quantity / 1000, 2)
                            objFEB.BuyActual2 = Math.Round(item.ActualQuantity / 1000, 2)
                    End Select
                Case PublicEnum.EnumMonth.JAN
                    Select Case item.WeekNo
                        Case PublicEnum.WeekNo.OneAndTwo
                            objJAN.BuyQuantity1 = Math.Round(item.Quantity / 1000, 2)
                            objJAN.BuyActual1 = Math.Round(item.ActualQuantity / 1000, 2)
                        Case PublicEnum.WeekNo.ThreeAndFour
                            objJAN.BuyQuantity2 = Math.Round(item.Quantity / 1000, 2)
                            objJAN.BuyActual2 = Math.Round(item.ActualQuantity / 1000, 2)
                    End Select
                Case PublicEnum.EnumMonth.JUL
                    Select Case item.WeekNo
                        Case PublicEnum.WeekNo.OneAndTwo
                            objJUL.BuyQuantity1 = Math.Round(item.Quantity / 1000, 2)
                            objJUL.BuyActual1 = Math.Round(item.ActualQuantity / 1000, 2)
                        Case PublicEnum.WeekNo.ThreeAndFour
                            objJUL.BuyQuantity2 = Math.Round(item.Quantity / 1000, 2)
                            objJUL.BuyActual2 = Math.Round(item.ActualQuantity / 1000, 2)
                    End Select
                Case PublicEnum.EnumMonth.JUN
                    Select Case item.WeekNo
                        Case PublicEnum.WeekNo.OneAndTwo
                            objJUN.BuyQuantity1 = Math.Round(item.Quantity / 1000, 2)
                            objJUN.BuyActual1 = Math.Round(item.ActualQuantity / 1000, 2)
                        Case PublicEnum.WeekNo.ThreeAndFour
                            objJUN.BuyQuantity2 = Math.Round(item.Quantity / 1000, 2)
                            objJUN.BuyActual2 = Math.Round(item.ActualQuantity / 1000, 2)
                    End Select
                Case PublicEnum.EnumMonth.MAR
                    Select Case item.WeekNo
                        Case PublicEnum.WeekNo.OneAndTwo
                            objMAR.BuyQuantity1 = Math.Round(item.Quantity / 1000, 2)
                            objMAR.BuyActual1 = Math.Round(item.ActualQuantity / 1000, 2)
                        Case PublicEnum.WeekNo.ThreeAndFour
                            objMAR.BuyQuantity2 = Math.Round(item.Quantity / 1000, 2)
                            objMAR.BuyActual2 = Math.Round(item.ActualQuantity / 1000, 2)
                    End Select
                Case PublicEnum.EnumMonth.MAY
                    Select Case item.WeekNo
                        Case PublicEnum.WeekNo.OneAndTwo
                            objMAY.BuyQuantity1 = Math.Round(item.Quantity / 1000, 2)
                            objMAY.BuyActual1 = Math.Round(item.ActualQuantity / 1000, 2)
                        Case PublicEnum.WeekNo.ThreeAndFour
                            objMAY.BuyQuantity2 = Math.Round(item.Quantity / 1000, 2)
                            objMAY.BuyActual2 = Math.Round(item.ActualQuantity / 1000, 2)
                    End Select
                Case PublicEnum.EnumMonth.NOV
                    Select Case item.WeekNo
                        Case PublicEnum.WeekNo.OneAndTwo
                            objNOV.BuyQuantity1 = Math.Round(item.Quantity / 1000, 2)
                            objNOV.BuyActual1 = Math.Round(item.ActualQuantity, 2)
                        Case PublicEnum.WeekNo.ThreeAndFour
                            objNOV.BuyQuantity2 = Math.Round(item.Quantity / 1000, 2)
                            objNOV.BuyActual2 = Math.Round(item.ActualQuantity / 1000, 2)
                    End Select
                Case PublicEnum.EnumMonth.OCT
                    Select Case item.WeekNo
                        Case PublicEnum.WeekNo.OneAndTwo
                            objOCT.BuyQuantity1 = Math.Round(item.Quantity / 1000, 2)
                            objOCT.BuyActual1 = Math.Round(item.ActualQuantity / 1000, 2)
                        Case PublicEnum.WeekNo.ThreeAndFour
                            objOCT.BuyQuantity2 = Math.Round(item.Quantity / 1000, 2)
                            objOCT.BuyActual2 = Math.Round(item.ActualQuantity / 1000, 2)
                    End Select
                Case PublicEnum.EnumMonth.SEP
                    Select Case item.WeekNo
                        Case PublicEnum.WeekNo.OneAndTwo
                            objSEP.BuyQuantity1 = Math.Round(item.Quantity / 1000, 2)
                            objSEP.BuyActual1 = Math.Round(item.ActualQuantity / 1000, 2)
                        Case PublicEnum.WeekNo.ThreeAndFour
                            objSEP.BuyQuantity2 = Math.Round(item.Quantity / 1000, 2)
                            objSEP.BuyActual2 = Math.Round(item.ActualQuantity / 1000, 2)
                    End Select
            End Select
        Next

        objPlan.MainPlan = Me

        objPlan.APR = objAPR
        objPlan.AUG = objAUG
        objPlan.DEC = objDEC
        objPlan.FEB = objFEB
        objPlan.JAN = objJAN
        objPlan.JUL = objJUL
        objPlan.JUN = objJUN
        objPlan.MAR = objMAR
        objPlan.MAY = objMAY
        objPlan.NOV = objNOV
        objPlan.OCT = objOCT
        objPlan.SEP = objSEP

        objPlan.GrowStartDate = objGrowPlan.StartDate
        objPlan.GrowEndDate = objGrowPlan.EndDate
        objPlan.GrowRemark = objGrowPlan.Remark

        objPlan.HarvestStartDate = objHarvestPlan.StartDate
        objPlan.HarvestEndDate = objHarvestPlan.EndDate
        objPlan.HarvestRemark = objHarvestPlan.Remark

        objPlan.BuyStartDate = objBuyPlan.StartDate
        objPlan.BuyEndDate = objBuyPlan.EndDate
        objPlan.BuyRemark = objBuyPlan.Remark

        objPlan.TotalGrowSize = objGrowPlan.SumQuantity
        objPlan.TotalGrowActual = objGrowPlan.SumActualQuantity

        objPlan.TotalHarvestSize = objHarvestPlan.SumQuantity
        objPlan.TotalHarvestActual = objHarvestPlan.SumActualQuantity

        objPlan.TotalBuyQuantity = Math.Round(objBuyPlan.SumQuantity / 1000, 2)
        objPlan.TotalBuyActual = Math.Round(objBuyPlan.SumActualQuantity / 1000, 2)

        objList.Add(objPlan)
        'objList.Add(objResult)

    End Sub

End Class
