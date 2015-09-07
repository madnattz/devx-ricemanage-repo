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

<XafDisplayName("ส่งรายงาน แผนและผลการจัดทำแปลงขยายพันธุ์")> _
<RuleCriteria("SubmitActivityPlanReport.NotDelete", DefaultContexts.Delete, "Status='Draft'", CustomMessageTemplate:="ไม่สามารถลบข้อมูลนี้ได้")> _
<ConditionalAppearance.Appearance("SubmitActivityPlanReportDisableAllItems", criteria:="Status!='Draft'", Enabled:=False, TargetItems:="*", Context:="DetailView")> _
Public Class SubmitActivityPlanReport
    Inherits BaseObject
    Implements ISubmitReportAble

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        'Me.SubmitDate = Date.Today
        'Me.SubmitBy = CType(SecuritySystem.CurrentUser, User).DisplayName
        Me.Status = PublicEnum.SubmitReportStatus.Draft
    End Sub

    'Dim fMainPlan As MainPlan
    '<XafDisplayName("เป้าการผลิต")> _
    '<ImmediatePostData()> _
    'Public Property MainPlan() As MainPlan
    '    Get
    '        Return fMainPlan
    '    End Get
    '    Set(ByVal value As MainPlan)
    '        SetPropertyValue(Of MainPlan)("MainPlan", fMainPlan, value)
    '        If Not IsLoading AndAlso Not IsSaving Then
    '            fPlanfarmers = New XPCollection(Of PlanFarmer)(Session, CriteriaOperator.Parse("PlanFarmerGroup.MainPlan=?", value))
    '        End If
    '    End Set
    'End Property

    Dim fSeason As Season
    <XafDisplayName("ฤดู")> _
    <ImmediatePostData()> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property Season() As Season
        Get
            Return fSeason
        End Get
        Set(ByVal value As Season)
            SetPropertyValue(Of Season)("Season", fSeason, value)
            OnChanged("ActivityPlans")
        End Set
    End Property

    Dim fSeedYear As String
    <XafDisplayName("ปี")> _
    <ImmediatePostData()> _
    <RuleRequiredField(DefaultContexts.Save)> _
    <ModelDefault("PropertyEditorType", "RiceManagementApp.Module.Win.CustomYearDropdown")> _
    Public Property SeedYear() As String
        Get
            Return fSeedYear
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SeedYear", fSeedYear, value)
            OnChanged("ActivityPlans")
        End Set
    End Property

    Dim fSeedLot As String
    <XafDisplayName("รุ่นที่")> _
    <RuleRequiredField(DefaultContexts.Save)> _
    <ImmediatePostData()> _
    <ModelDefault("PropertyEditorType", "RiceManagementApp.Module.Win.CustomLotDropdown")> _
    Public Property SeedLot() As String
        Get
            Return fSeedLot
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SeedLot", fSeedLot, value)
            OnChanged("ActivityPlans")
        End Set
    End Property

    Dim fSubmitDate As Date
    <XafDisplayName("วันที่ส่งรายงาน")> _
    Public Property SubmitDate() As Date
        Get
            Return fSubmitDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue(Of Date)("SubmitDate", fSubmitDate, value)
        End Set
    End Property
    Dim fSubmitBy As String
    <XafDisplayName("ส่งรายงานโดย")> _
    Public Property SubmitBy() As String
        Get
            Return fSubmitBy
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SubmitBy", fSubmitBy, value)
        End Set
    End Property

    Dim fStatus As PublicEnum.SubmitReportStatus
    <XafDisplayName("สถานะ")> _
    Public Property Status() As PublicEnum.SubmitReportStatus
        Get
            Return fStatus
        End Get
        Set(ByVal value As PublicEnum.SubmitReportStatus)
            SetPropertyValue(Of PublicEnum.SubmitReportStatus)("Status", fStatus, value)
        End Set
    End Property

    <XafDisplayName("แผนกิจกรรม")> _
    Public ReadOnly Property ActivityPlans() As BindingList(Of ActivityPlan)
        Get
            Dim fActivityPlans As New BindingList(Of ActivityPlan)
            If Season IsNot Nothing And SeedLot <> "" And SeedYear <> "" Then
                Dim collMainPlans As New XPCollection(Of MainPlan)(Session, CriteriaOperator.Parse("Season=? and SeedYear=? and Lot=? and PlanStatus='Pending'", Me.Season, Me.SeedYear, Me.SeedLot))
                If collMainPlans.Count > 0 Then
                    For Each item As MainPlan In collMainPlans
                        For Each actItem As ActivityPlan In item.ActivityPlans
                            fActivityPlans.Add(actItem)
                        Next
                    Next
                End If
            End If
            Return fActivityPlans
        End Get
    End Property

    '<Action(Caption:="ส่งรายงาน", ConfirmationMessage:="ท่านต้องการส่งข้อมูลรายงานนี้ ใช่หรือไม่?", ImageName:="sent2", AutoCommit:=True, TargetObjectsCriteria:="Status='Draft'")> _
    Public Function SubmitReport() As Boolean
        If Not IsDeleted Then
            If Not ActivityPlans.Count > 0 Then
                MsgBox("ไม่พบข้อมูลแผนการจัดทำแปลงขยายพันธุ์ กรุณาตรวจสอบข้อมูลอีกครั้ง", MsgBoxStyle.Information, "")
                Exit Function
            End If
            Try
                '//ประกาศตัวแปล webservice
                Dim objService As New RiceService.RiceService

                '//ข้อมูลตั้งค่าของศูนย์
                Dim objSiteInfo As SiteSetting = GetSiteInfo()

                Dim objData As New List(Of RiceService.ActivityPlanReportData)

                '//ใส่ค่าให้กับ object รายงาน ขพ.2 (ข้อมูลส่วน Header)
                For Each item As ActivityPlan In ActivityPlans
                    '//ประกาศ Object ของรายงาน ขพ.2 (Header)
                    Dim objHeader As New RiceService.ActivityPlanReportData
                    objHeader.SiteNo = objSiteInfo.SiteNo
                    objHeader.SiteName = objSiteInfo.SiteName
                    objHeader.PlantName = item.MainPlan.Plant.PlantName
                    objHeader.SeedTypeName = item.MainPlan.SeedType.SeedName
                    objHeader.SeedClassName = item.MainPlan.SeedClass.ClassName
                    objHeader.SeasonName = item.MainPlan.Season.SeasonName
                    objHeader.SeedYear = item.MainPlan.SeedYear
                    objHeader.SeedLot = item.MainPlan.Lot
                    objHeader.GoalQuantity = item.MainPlan.TotalGoal / 1000
                    objHeader.BuyQuantity = item.MainPlan.SumBuyQuantity / 1000
                    objHeader.TotalAreaSize = item.MainPlan.AreaCount
                    objHeader.TotalFarmerGroup = item.MainPlan.GroupCount
                    objHeader.TotalFarmer = item.MainPlan.FarmerCount
                    objHeader.SubmitBy = CType(SecuritySystem.CurrentUser, User).DisplayName
                    objHeader.SubmitDate = Date.Now


                    Dim listOfGrowPlanDetail As New List(Of RiceService.GrowPlanDetail)
                    Dim listOfHarvestPlanDetail As New List(Of RiceService.HarvestPlanDetail)
                    Dim listOfBuyPlanDetail As New List(Of RiceService.BuyPlanDetail)

                    'For Each detail As ActivityPlan In ActivityPlans
                    Dim growPlan As New RiceService.GrowPlanDetail
                    growPlan.StartDate = item.GrowStartDate
                    growPlan.EndDate = item.GrowEndDate
                    growPlan.Remark = item.GrowRemark

                    growPlan.Jan1_2Plan = item.JAN.GrowSize1
                    growPlan.Jan1_2Result = item.JAN.GrowActual1
                    growPlan.Jan3_4Plan = item.JAN.GrowSize2
                    growPlan.Jan3_4Result = item.JAN.GrowActual2

                    growPlan.Feb1_2Plan = item.FEB.GrowSize1
                    growPlan.Feb1_2Result = item.FEB.GrowActual1
                    growPlan.Feb3_4Plan = item.FEB.GrowSize2
                    growPlan.Feb3_4Result = item.FEB.GrowActual2

                    growPlan.Mar1_2Plan = item.MAR.GrowSize1
                    growPlan.Mar1_2Result = item.MAR.GrowActual1
                    growPlan.Mar3_4Plan = item.MAR.GrowSize2
                    growPlan.Mar3_4Result = item.MAR.GrowActual2

                    growPlan.Apr1_2Plan = item.APR.GrowSize1
                    growPlan.Apr1_2Result = item.APR.GrowActual1
                    growPlan.Apr3_4Plan = item.APR.GrowSize2
                    growPlan.Apr3_4Result = item.APR.GrowActual2

                    growPlan.May1_2Plan = item.MAY.GrowSize1
                    growPlan.May1_2Result = item.MAY.GrowActual1
                    growPlan.May3_4Plan = item.MAY.GrowSize2
                    growPlan.May3_4Result = item.MAY.GrowActual2

                    growPlan.Jan1_2Plan = item.JAN.GrowSize1
                    growPlan.Jan1_2Result = item.JAN.GrowActual1
                    growPlan.Jan3_4Plan = item.JAN.GrowSize2
                    growPlan.Jan3_4Result = item.JAN.GrowActual2

                    growPlan.Jul1_2Plan = item.JUL.GrowSize1
                    growPlan.Jul1_2Result = item.JUL.GrowActual1
                    growPlan.Jul3_4Plan = item.JUL.GrowSize2
                    growPlan.Jul3_4Result = item.JUL.GrowActual2

                    growPlan.Aug1_2Plan = item.AUG.GrowSize1
                    growPlan.Aug1_2Result = item.AUG.GrowActual1
                    growPlan.Aug3_4Plan = item.AUG.GrowSize2
                    growPlan.Aug3_4Result = item.AUG.GrowActual2

                    growPlan.Sep1_2Plan = item.SEP.GrowSize1
                    growPlan.Sep1_2Result = item.SEP.GrowActual1
                    growPlan.Sep3_4Plan = item.SEP.GrowSize2
                    growPlan.Sep3_4Result = item.SEP.GrowActual2

                    growPlan.Oct1_2Plan = item.OCT.GrowSize1
                    growPlan.Oct1_2Result = item.OCT.GrowActual1
                    growPlan.Oct3_4Plan = item.OCT.GrowSize2
                    growPlan.Oct3_4Result = item.OCT.GrowActual2

                    growPlan.Nov1_2Plan = item.NOV.GrowSize1
                    growPlan.Nov1_2Result = item.NOV.GrowActual1
                    growPlan.Nov3_4Plan = item.NOV.GrowSize2
                    growPlan.Nov3_4Result = item.NOV.GrowActual2

                    growPlan.Dec1_2Plan = item.DEC.GrowSize1
                    growPlan.Dec1_2Result = item.DEC.GrowActual1
                    growPlan.Dec3_4Plan = item.DEC.GrowSize2
                    growPlan.Dec3_4Result = item.DEC.GrowActual2

                    listOfGrowPlanDetail.Add(growPlan)

                    Dim harvestPlan As New RiceService.HarvestPlanDetail
                    harvestPlan.StartDate = item.HarvestStartDate
                    harvestPlan.EndDate = item.HarvestEndDate
                    harvestPlan.Remark = item.HarvestRemark

                    harvestPlan.Jan1_2Plan = item.JAN.HarvestSize1
                    harvestPlan.Jan1_2Result = item.JAN.HarvestActual1
                    harvestPlan.Jan3_4Plan = item.JAN.HarvestSize2
                    harvestPlan.Jan3_4Result = item.JAN.HarvestActual2

                    harvestPlan.Feb1_2Plan = item.FEB.HarvestSize1
                    harvestPlan.Feb1_2Result = item.FEB.HarvestActual1
                    harvestPlan.Feb3_4Plan = item.FEB.HarvestSize2
                    harvestPlan.Feb3_4Result = item.FEB.HarvestActual2

                    harvestPlan.Mar1_2Plan = item.MAR.HarvestSize1
                    harvestPlan.Mar1_2Result = item.MAR.HarvestActual1
                    harvestPlan.Mar3_4Plan = item.MAR.HarvestSize2
                    harvestPlan.Mar3_4Result = item.MAR.HarvestActual2

                    harvestPlan.Apr1_2Plan = item.APR.HarvestSize1
                    harvestPlan.Apr1_2Result = item.APR.HarvestActual1
                    harvestPlan.Apr3_4Plan = item.APR.HarvestSize2
                    harvestPlan.Apr3_4Result = item.APR.HarvestActual2

                    harvestPlan.May1_2Plan = item.MAY.HarvestSize1
                    harvestPlan.May1_2Result = item.MAY.HarvestActual1
                    harvestPlan.May3_4Plan = item.MAY.HarvestSize2
                    harvestPlan.May3_4Result = item.MAY.HarvestActual2

                    harvestPlan.Jan1_2Plan = item.JAN.HarvestSize1
                    harvestPlan.Jan1_2Result = item.JAN.HarvestActual1
                    harvestPlan.Jan3_4Plan = item.JAN.HarvestSize2
                    harvestPlan.Jan3_4Result = item.JAN.HarvestActual2

                    harvestPlan.Jul1_2Plan = item.JUL.HarvestSize1
                    harvestPlan.Jul1_2Result = item.JUL.HarvestActual1
                    harvestPlan.Jul3_4Plan = item.JUL.HarvestSize2
                    harvestPlan.Jul3_4Result = item.JUL.HarvestActual2

                    harvestPlan.Aug1_2Plan = item.AUG.HarvestSize1
                    harvestPlan.Aug1_2Result = item.AUG.HarvestActual1
                    harvestPlan.Aug3_4Plan = item.AUG.HarvestSize2
                    harvestPlan.Aug3_4Result = item.AUG.HarvestActual2

                    harvestPlan.Sep1_2Plan = item.SEP.HarvestSize1
                    harvestPlan.Sep1_2Result = item.SEP.HarvestActual1
                    harvestPlan.Sep3_4Plan = item.SEP.HarvestSize2
                    harvestPlan.Sep3_4Result = item.SEP.HarvestActual2

                    harvestPlan.Oct1_2Plan = item.OCT.HarvestSize1
                    harvestPlan.Oct1_2Result = item.OCT.HarvestActual1
                    harvestPlan.Oct3_4Plan = item.OCT.HarvestSize2
                    harvestPlan.Oct3_4Result = item.OCT.HarvestActual2

                    harvestPlan.Nov1_2Plan = item.NOV.HarvestSize1
                    harvestPlan.Nov1_2Result = item.NOV.HarvestActual1
                    harvestPlan.Nov3_4Plan = item.NOV.HarvestSize2
                    harvestPlan.Nov3_4Result = item.NOV.HarvestActual2

                    harvestPlan.Dec1_2Plan = item.DEC.HarvestSize1
                    harvestPlan.Dec1_2Result = item.DEC.HarvestActual1
                    harvestPlan.Dec3_4Plan = item.DEC.HarvestSize2
                    harvestPlan.Dec3_4Result = item.DEC.HarvestActual2
                    listOfHarvestPlanDetail.Add(harvestPlan)

                    Dim buyPlan As New RiceService.BuyPlanDetail
                    buyPlan.StartDate = item.BuyStartDate
                    buyPlan.EndDate = item.BuyEndDate
                    buyPlan.Remark = item.BuyRemark

                    buyPlan.Jan1_2Plan = item.JAN.BuyQuantity1
                    buyPlan.Jan1_2Result = item.JAN.BuyActual1
                    buyPlan.Jan3_4Plan = item.JAN.BuyQuantity2
                    buyPlan.Jan3_4Result = item.JAN.BuyActual2

                    buyPlan.Feb1_2Plan = item.FEB.BuyQuantity1
                    buyPlan.Feb1_2Result = item.FEB.BuyActual1
                    buyPlan.Feb3_4Plan = item.FEB.BuyQuantity2
                    buyPlan.Feb3_4Result = item.FEB.BuyActual2

                    buyPlan.Mar1_2Plan = item.MAR.BuyQuantity1
                    buyPlan.Mar1_2Result = item.MAR.BuyActual1
                    buyPlan.Mar3_4Plan = item.MAR.BuyQuantity2
                    buyPlan.Mar3_4Result = item.MAR.BuyActual2

                    buyPlan.Apr1_2Plan = item.APR.BuyQuantity1
                    buyPlan.Apr1_2Result = item.APR.BuyActual1
                    buyPlan.Apr3_4Plan = item.APR.BuyQuantity2
                    buyPlan.Apr3_4Result = item.APR.BuyActual2

                    buyPlan.May1_2Plan = item.MAY.BuyQuantity1
                    buyPlan.May1_2Result = item.MAY.BuyActual1
                    buyPlan.May3_4Plan = item.MAY.BuyQuantity2
                    buyPlan.May3_4Result = item.MAY.BuyActual2

                    buyPlan.Jan1_2Plan = item.JAN.BuyQuantity1
                    buyPlan.Jan1_2Result = item.JAN.BuyActual1
                    buyPlan.Jan3_4Plan = item.JAN.BuyQuantity2
                    buyPlan.Jan3_4Result = item.JAN.BuyActual2

                    buyPlan.Jul1_2Plan = item.JUL.BuyQuantity1
                    buyPlan.Jul1_2Result = item.JUL.BuyActual1
                    buyPlan.Jul3_4Plan = item.JUL.BuyQuantity2
                    buyPlan.Jul3_4Result = item.JUL.BuyActual2

                    buyPlan.Aug1_2Plan = item.AUG.BuyQuantity1
                    buyPlan.Aug1_2Result = item.AUG.BuyActual1
                    buyPlan.Aug3_4Plan = item.AUG.BuyQuantity2
                    buyPlan.Aug3_4Result = item.AUG.BuyActual2

                    buyPlan.Sep1_2Plan = item.SEP.BuyQuantity1
                    buyPlan.Sep1_2Result = item.SEP.BuyActual1
                    buyPlan.Sep3_4Plan = item.SEP.BuyQuantity2
                    buyPlan.Sep3_4Result = item.SEP.BuyActual2

                    buyPlan.Oct1_2Plan = item.OCT.BuyQuantity1
                    buyPlan.Oct1_2Result = item.OCT.BuyActual1
                    buyPlan.Oct3_4Plan = item.OCT.BuyQuantity2
                    buyPlan.Oct3_4Result = item.OCT.BuyActual2

                    buyPlan.Nov1_2Plan = item.NOV.BuyQuantity1
                    buyPlan.Nov1_2Result = item.NOV.BuyActual1
                    buyPlan.Nov3_4Plan = item.NOV.BuyQuantity2
                    buyPlan.Nov3_4Result = item.NOV.BuyActual2

                    buyPlan.Dec1_2Plan = item.DEC.BuyQuantity1
                    buyPlan.Dec1_2Result = item.DEC.BuyActual1
                    buyPlan.Dec3_4Plan = item.DEC.BuyQuantity2
                    buyPlan.Dec3_4Result = item.DEC.BuyActual2

                    listOfBuyPlanDetail.Add(buyPlan)

                    'Next

                    '//ใส่ Detail ในส่วนของ Header
                    '//ต้องแปลง List ให้เป็น Array เพราะส่งข้อมูลผ่านเว็บเซอร์วิส ต้องเป็น Array 
                    objHeader.GrowPlanDetails = listOfGrowPlanDetail.ToArray
                    objHeader.HarvestPlanDetails = listOfHarvestPlanDetail.ToArray
                    objHeader.BuyPlanDetails = listOfBuyPlanDetail.ToArray

                    objData.Add(objHeader)

                Next

                '//ส่งข้อมูลผ่าน Webservice
                Dim ret As String = objService.SubmitDataPlanAndResultReport("NTiSecureCode", objData.ToArray)

                '//เป็นสถานะเป็น ส่งข้อมูลแล้ว (Sent)
                If ret = "Success" Then
                    Me.Status = PublicEnum.SubmitReportStatus.Sent
                    Me.SubmitDate = Date.Now
                    Me.SubmitBy = CType(SecuritySystem.CurrentUser, User).DisplayName
                    '//บันทึกข้อมูล
                    'MsgBox("ส่งข้อมูลรายงาน เรียบร้อยแล้ว", MsgBoxStyle.Information)
                    Me.Save()
                    'Session.CommitTransaction()
                    Return True
                Else
                    Return False
                    'MsgBox("เกิดข้อผิดพลาด ไม่สามารถส่งรายงานได้ กรุณาติดต่อผู้ดูแลระบบ", MsgBoxStyle.Critical)
                End If

            Catch ex As Exception
                Return False
                'MsgBox("เกิดข้อผิดพลาด ไม่สามารถส่งรายงานได้ กรุณาติดต่อผู้ดูแลระบบ", MsgBoxStyle.Critical)
            End Try

        End If
    End Function

    Public Function ConvertToInteger(obj As Object) As Integer
        Try
            Return Convert.ToInt32(obj)
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Protected Function GetSiteInfo() As SiteSetting
        Dim objSite As New XPCollection(Of SiteSetting)(Session)
        Return objSite(0)
    End Function

    Public Sub DoLoadData() Implements ISubmitReportAble.DoLoadData

    End Sub

    Public Function DoSubmitReport() As Boolean Implements ISubmitReportAble.DoSubmitReport
        Return SubmitReport()
    End Function
End Class
