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

<DeferredDeletion(True)> _
<DefaultClassOptions()> _
<XafDisplayName("แผนและผลการดำเนินงานตามกิจกรรม")> _
Public Class AuditActivityPlan
    Inherits BaseObject
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        DataOwner = GetCurrentSite()
    End Sub

    Protected Overrides Sub OnDeleted()
        MyBase.OnDeleted()
        Session.Delete(AuditGrowPlanDetails)
        Session.Delete(AuditCheckFarmPlanDetails)
        Session.Delete(AuditBuyPlanDetails)
        Session.Delete(AuditProcessPlanDetails)
        Session.Delete(AuditKeepPlanDetails)
        Session.Delete(AuditSalePlanDetails)
        Session.Delete(AuditTrail)
    End Sub

    Protected Overrides Sub OnLoaded()
        MyBase.OnLoaded()
        AuditGrowPlanDetails.Sorting.Add(New SortProperty("TimeStamp", DB.SortingDirection.Ascending))
        AuditCheckFarmPlanDetails.Sorting.Add(New SortProperty("TimeStamp", DB.SortingDirection.Ascending))
        AuditBuyPlanDetails.Sorting.Add(New SortProperty("TimeStamp", DB.SortingDirection.Ascending))
        AuditProcessPlanDetails.Sorting.Add(New SortProperty("TimeStamp", DB.SortingDirection.Ascending))
        AuditKeepPlanDetails.Sorting.Add(New SortProperty("TimeStamp", DB.SortingDirection.Ascending))
        AuditSalePlanDetails.Sorting.Add(New SortProperty("TimeStamp", DB.SortingDirection.Ascending))
    End Sub

    'Protected Overrides Sub OnSaved()
    '    MyBase.OnSaved()
    '    GenerateAuditGrowPlanDetail(Session, Me)
    '    GenerateAuditCheckFarmPlanDetail(Session, Me)
    '    GenerateAuditBuyPlanDetail(Session, Me)
    '    GenerateAuditProcessPlanDetail(Session, Me)
    '    GenerateAuditKeepPlanDetail(Session, Me)
    '    GenerateAuditSalePlanDetail(Session, Me)
    'End Sub

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

    Dim fMainPlan As MainPlan
    <XafDisplayName("เป้าการผลิต")> _
    <RuleUniqueValue()> _
    Public Property MainPlan() As MainPlan
        Get
            Return fMainPlan
        End Get
        Set(ByVal value As MainPlan)
            SetPropertyValue(Of MainPlan)("MainPlan", fMainPlan, value)
        End Set
    End Property

    ' Dim fGrowStartDate As DateTime
    ' <XafDisplayName("วันที่เริ่มดำเนินการ")> _
    ' Public Property StartDate() As DateTime
    '     Get
    '         Return fGrowStartDate
    '     End Get
    '     Set(ByVal value As DateTime)
    '         SetPropertyValue(Of DateTime)("GrowStartDate", fGrowStartDate, value)
    '     End Set
    ' End Property

    ' Dim fGrowEndDate As DateTime
    ' <XafDisplayName("วันที่คาดว่าจะเสร็จสิ้น")> _
    ' Public Property EndDate() As DateTime
    '     Get
    '         Return fGrowEndDate
    '     End Get
    '     Set(ByVal value As DateTime)
    '         SetPropertyValue(Of DateTime)("GrowEndDate", fGrowEndDate, value)
    '     End Set
    ' End Property

    ' Dim fRemark As String
    ' <XafDisplayName("หมายเหตุ")> _
    ' <Size(500)> _
    ' Public Property Remark() As String
    '     Get
    '         Return fRemark
    '     End Get
    '     Set(ByVal value As String)
    '         SetPropertyValue(Of String)("Remark", fRemark, value)
    '     End Set
    ' End Property

    ' <XafDisplayName("รวมสะสมตั้งแต่เริ่มต้น (ไร่)")> _
    ' Public ReadOnly Property SumQuantity As Integer
    '     Get
    '         Dim ret As Integer = 0
    '         For Each item As GrowPlanDetail In GrowPlanDetails
    '             ret += item.Quantity
    '         Next
    '         Return ret
    '     End Get
    ' End Property

    <Association("AuditGrowPlanDetailReferencesAuditActivityPlan", GetType(AuditGrowPlanDetail))> _
    <XafDisplayName("แผนปลูก")> _
    Public ReadOnly Property AuditGrowPlanDetails() As XPCollection(Of AuditGrowPlanDetail)
        Get
            Return GetCollection(Of AuditGrowPlanDetail)("AuditGrowPlanDetails")
        End Get
    End Property

    <Association("AuditCheckFarmPlanDetailReferencesAuditActivityPlan", GetType(AuditCheckFarmPlanDetail))> _
    <XafDisplayName("แผนตรวจแปลง")> _
    Public ReadOnly Property AuditCheckFarmPlanDetails() As XPCollection(Of AuditCheckFarmPlanDetail)
        Get
            Return GetCollection(Of AuditCheckFarmPlanDetail)("AuditCheckFarmPlanDetails")
        End Get
    End Property

    <Association("AuditBuyPlanDetailReferencesAuditActivityPlan", GetType(AuditBuyPlanDetail))> _
    <XafDisplayName("แผนจัดซื้อ")> _
    Public ReadOnly Property AuditBuyPlanDetails() As XPCollection(Of AuditBuyPlanDetail)
        Get
            Return GetCollection(Of AuditBuyPlanDetail)("AuditBuyPlanDetails")
        End Get
    End Property

    <Association("AuditProcessPlanDetailReferencesAuditActivityPlan", GetType(AuditProcessPlanDetail))> _
    <XafDisplayName("แผนปรับปรุง")> _
    Public ReadOnly Property AuditProcessPlanDetails() As XPCollection(Of AuditProcessPlanDetail)
        Get
            Return GetCollection(Of AuditProcessPlanDetail)("AuditProcessPlanDetails")
        End Get
    End Property

    <Association("AuditKeepPlanDetailReferencesAuditActivityPlan", GetType(AuditKeepPlanDetail))> _
    <XafDisplayName("แผนจัดเก็บ")> _
    Public ReadOnly Property AuditKeepPlanDetails() As XPCollection(Of AuditKeepPlanDetail)
        Get
            Return GetCollection(Of AuditKeepPlanDetail)("AuditKeepPlanDetails")
        End Get

    End Property

    <Association("AuditSalePlanDetailReferencesAuditActivityPlan", GetType(AuditSalePlanDetail))> _
    <XafDisplayName("แผนจำหน่าย")> _
    Public ReadOnly Property AuditSalePlanDetails() As XPCollection(Of AuditSalePlanDetail)
        Get
            Return GetCollection(Of AuditSalePlanDetail)("AuditSalePlanDetails")
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

    Public Sub GenerateAuditGrowPlanDetail(_trans As Session, header As AuditActivityPlan)
        Try
            Dim sMonth As Integer = 10
            Dim sYear As Integer = CInt(header.MainPlan.SeedYear) - 1
            For i As Integer = 0 To 11
                If sMonth > 12 Then
                    sMonth = 1
                    sYear += 1
                End If
                Dim objAuditGrowPlanDetail As AuditGrowPlanDetail = _trans.FindObject(Of AuditGrowPlanDetail)(CriteriaOperator.Parse("AuditActivityPlan=? and PlanYear=? and MonthNo=?", header, sYear, sMonth))
                If objAuditGrowPlanDetail Is Nothing Then
                    objAuditGrowPlanDetail = New AuditGrowPlanDetail(_trans)
                    objAuditGrowPlanDetail.AuditActivityPlan = header
                    objAuditGrowPlanDetail.PlanYear = sYear
                    objAuditGrowPlanDetail.MonthNo = sMonth
                    objAuditGrowPlanDetail.TimeStamp = Date.Now
                    objAuditGrowPlanDetail.Save()
                    System.Threading.Thread.Sleep(100)
                End If
                sMonth += 1
            Next
            '_trans.CommitTransaction()
        Catch ex As Exception
            '_trans.RollbackTransaction()
        End Try
    End Sub

    Public Sub GenerateAuditCheckFarmPlanDetail(_trans As Session, header As AuditActivityPlan)
        Try
            Dim sMonth As Integer = 10
            Dim sYear As Integer = CInt(header.MainPlan.SeedYear) - 1
            For i As Integer = 0 To 11
                If sMonth > 12 Then
                    sMonth = 1
                    sYear += 1
                End If
                Dim objAuditGrowPlanDetail As AuditCheckFarmPlanDetail = _trans.FindObject(Of AuditCheckFarmPlanDetail)(CriteriaOperator.Parse("AuditActivityPlan=? and PlanYear=? and MonthNo=?", header, sYear, sMonth))
                If objAuditGrowPlanDetail Is Nothing Then
                    objAuditGrowPlanDetail = New AuditCheckFarmPlanDetail(_trans)
                    objAuditGrowPlanDetail.AuditActivityPlan = header
                    objAuditGrowPlanDetail.PlanYear = sYear
                    objAuditGrowPlanDetail.MonthNo = sMonth
                    objAuditGrowPlanDetail.TimeStamp = Date.Now
                    objAuditGrowPlanDetail.Save()
                    System.Threading.Thread.Sleep(100)
                End If
                sMonth += 1
            Next
            '_trans.CommitTransaction()
        Catch ex As Exception
            '_trans.RollbackTransaction()
        End Try
    End Sub

    Public Sub GenerateAuditBuyPlanDetail(_trans As Session, header As AuditActivityPlan)
        Try
            Dim sMonth As Integer = 10
            Dim sYear As Integer = CInt(header.MainPlan.SeedYear) - 1
            For i As Integer = 0 To 11
                If sMonth > 12 Then
                    sMonth = 1
                    sYear += 1
                End If
                Dim objAuditGrowPlanDetail As AuditBuyPlanDetail = _trans.FindObject(Of AuditBuyPlanDetail)(CriteriaOperator.Parse("AuditActivityPlan=? and PlanYear=? and MonthNo=?", header, sYear, sMonth))
                If objAuditGrowPlanDetail Is Nothing Then
                    objAuditGrowPlanDetail = New AuditBuyPlanDetail(_trans)
                    objAuditGrowPlanDetail.AuditActivityPlan = header
                    objAuditGrowPlanDetail.PlanYear = sYear
                    objAuditGrowPlanDetail.MonthNo = sMonth
                    objAuditGrowPlanDetail.TimeStamp = Date.Now
                    objAuditGrowPlanDetail.Save()
                    System.Threading.Thread.Sleep(100)
                End If
                sMonth += 1
            Next
            '_trans.CommitTransaction()
        Catch ex As Exception
            '_trans.RollbackTransaction()
        End Try
    End Sub

    Public Sub GenerateAuditProcessPlanDetail(_trans As Session, header As AuditActivityPlan)
        Try
            Dim sMonth As Integer = 10
            Dim sYear As Integer = CInt(header.MainPlan.SeedYear) - 1
            For i As Integer = 0 To 11
                If sMonth > 12 Then
                    sMonth = 1
                    sYear += 1
                End If
                Dim objAuditGrowPlanDetail As AuditProcessPlanDetail = _trans.FindObject(Of AuditProcessPlanDetail)(CriteriaOperator.Parse("AuditActivityPlan=? and PlanYear=? and MonthNo=?", header, sYear, sMonth))
                If objAuditGrowPlanDetail Is Nothing Then
                    objAuditGrowPlanDetail = New AuditProcessPlanDetail(_trans)
                    objAuditGrowPlanDetail.AuditActivityPlan = header
                    objAuditGrowPlanDetail.PlanYear = sYear
                    objAuditGrowPlanDetail.MonthNo = sMonth
                    objAuditGrowPlanDetail.TimeStamp = Date.Now
                    objAuditGrowPlanDetail.Save()
                    System.Threading.Thread.Sleep(100)
                End If
                sMonth += 1
            Next
            '_trans.CommitTransaction()
        Catch ex As Exception
            '_trans.RollbackTransaction()
        End Try
    End Sub

    Public Sub GenerateAuditKeepPlanDetail(_trans As Session, header As AuditActivityPlan)
        Try
            Dim sMonth As Integer = 10
            Dim sYear As Integer = CInt(header.MainPlan.SeedYear) - 1
            For i As Integer = 0 To 11
                If sMonth > 12 Then
                    sMonth = 1
                    sYear += 1
                End If
                Dim objAuditGrowPlanDetail As AuditKeepPlanDetail = _trans.FindObject(Of AuditKeepPlanDetail)(CriteriaOperator.Parse("AuditActivityPlan=? and PlanYear=? and MonthNo=?", header, sYear, sMonth))
                If objAuditGrowPlanDetail Is Nothing Then
                    objAuditGrowPlanDetail = New AuditKeepPlanDetail(_trans)
                    objAuditGrowPlanDetail.AuditActivityPlan = header
                    objAuditGrowPlanDetail.PlanYear = sYear
                    objAuditGrowPlanDetail.MonthNo = sMonth
                    objAuditGrowPlanDetail.TimeStamp = Date.Now
                    objAuditGrowPlanDetail.Save()
                    System.Threading.Thread.Sleep(100)
                End If
                sMonth += 1
            Next
            '_trans.CommitTransaction()
        Catch ex As Exception
            '_trans.RollbackTransaction()
        End Try
    End Sub

    Public Sub GenerateAuditSalePlanDetail(_trans As Session, header As AuditActivityPlan)
        Try
            Dim sMonth As Integer = 10
            Dim sYear As Integer = CInt(header.MainPlan.SeedYear) - 1
            For i As Integer = 0 To 11
                If sMonth > 12 Then
                    sMonth = 1
                    sYear += 1
                End If
                Dim objAuditGrowPlanDetail As AuditSalePlanDetail = _trans.FindObject(Of AuditSalePlanDetail)(CriteriaOperator.Parse("AuditActivityPlan=? and PlanYear=? and MonthNo=?", header, sYear, sMonth))
                If objAuditGrowPlanDetail Is Nothing Then
                    objAuditGrowPlanDetail = New AuditSalePlanDetail(_trans)
                    objAuditGrowPlanDetail.AuditActivityPlan = header
                    objAuditGrowPlanDetail.PlanYear = sYear
                    objAuditGrowPlanDetail.MonthNo = sMonth
                    objAuditGrowPlanDetail.TimeStamp = Date.Now
                    objAuditGrowPlanDetail.Save()
                    System.Threading.Thread.Sleep(100)
                End If
                sMonth += 1
            Next
            '_trans.CommitTransaction()
        Catch ex As Exception
            '_trans.RollbackTransaction()
        End Try
    End Sub

    <Action(Caption:="บันทึกรายการอัตโนมัติ", ConfirmationMessage:="ท่านต้องการบันทึกข้อมูล ใช่หรือไม่?", ImageName:="Action_WindowList", AutoCommit:=True)> _
    Public Sub AutoGenerateItems()
        Try
            GenerateAuditGrowPlanDetail(Session, Me)
            GenerateAuditCheckFarmPlanDetail(Session, Me)
            GenerateAuditBuyPlanDetail(Session, Me)
            GenerateAuditProcessPlanDetail(Session, Me)
            GenerateAuditKeepPlanDetail(Session, Me)
            GenerateAuditSalePlanDetail(Session, Me)
            Session.CommitTransaction()
        Catch ex As Exception
            Session.RollbackTransaction()
        End Try

    End Sub

    <XafDisplayName("สรุปแผนและผลการดำเนินงานตามกิจกรรม")> _
    Public ReadOnly Property ActivityPlanReports() As BindingList(Of ActivityPlanReport)
        Get
            Dim collReport As New BindingList(Of ActivityPlanReport)
            InsertGrowPlanList(collReport)
            InsertChekFarmPlanList(collReport)
            InsertBuyPlanList(collReport)
            InsertProcessPlanList(collReport)
            InsertKeepPlanList(collReport)
            InsertSalePlanList(collReport)

            Return collReport
        End Get
    End Property

    Public Sub InsertGrowPlanList(objList As BindingList(Of ActivityPlanReport))

        Dim objPlan As New ActivityPlanReport(Session)
        objPlan.ItemNo = 1
        objPlan.ActivityName = "แผนปลูก"
        objPlan.ActivityUnit = "ไร่"

        Dim objResult As New ActivityPlanReport(Session)
        objResult.ItemNo = 2
        objResult.ActivityName = "ผลการปลูก"
        objResult.ActivityUnit = "ไร่"

        For Each item As AuditGrowPlanDetail In AuditGrowPlanDetails
            'objPlan.ActivityYear = item.PlanYear
            'objResult.ActivityYear = item.PlanYear
            objPlan.Total += item.Quantity
            objResult.Total += item.ActualQuantity
            'objPlan.Differrence = 0
            objResult.Differrence += (item.ActualQuantity - item.Quantity)

            Select Case item.MonthNo
                Case PublicEnum.EnumMonth.APR
                    objPlan.APR = item.Quantity
                    objResult.APR = item.ActualQuantity
                Case PublicEnum.EnumMonth.AUG
                    objPlan.AUG = item.Quantity
                    objResult.AUG = item.ActualQuantity
                Case PublicEnum.EnumMonth.DEC
                    objPlan.DEC = item.Quantity
                    objResult.DEC = item.ActualQuantity
                Case PublicEnum.EnumMonth.FEB
                    objPlan.FEB = item.Quantity
                    objResult.FEB = item.ActualQuantity
                Case PublicEnum.EnumMonth.JAN
                    objPlan.JAN = item.Quantity
                    objResult.JAN = item.ActualQuantity
                Case PublicEnum.EnumMonth.JUL
                    objPlan.JUL = item.Quantity
                    objResult.JUL = item.ActualQuantity
                Case PublicEnum.EnumMonth.JUN
                    objPlan.JUN = item.Quantity
                    objResult.JUN = item.ActualQuantity
                Case PublicEnum.EnumMonth.MAR
                    objPlan.MAR = item.Quantity
                    objResult.MAR = item.ActualQuantity
                Case PublicEnum.EnumMonth.MAY
                    objPlan.MAY = item.Quantity
                    objResult.MAY = item.ActualQuantity
                Case PublicEnum.EnumMonth.NOV
                    objPlan.NOV = item.Quantity
                    objResult.NOV = item.ActualQuantity
                Case PublicEnum.EnumMonth.OCT
                    objPlan.OCT = item.Quantity
                    objResult.OCT = item.ActualQuantity
                Case PublicEnum.EnumMonth.SEP
                    objPlan.SEP = item.Quantity
                    objResult.SEP = item.ActualQuantity
            End Select
        Next

        objList.Add(objPlan)
        objList.Add(objResult)

    End Sub

    Public Sub InsertChekFarmPlanList(objList As BindingList(Of ActivityPlanReport))

        Dim objPlan As New ActivityPlanReport(Session)
        objPlan.ItemNo = 3
        objPlan.ActivityName = "แผนตรวจแปลง"
        objPlan.ActivityUnit = "ไร่"

        Dim objResult As New ActivityPlanReport(Session)
        objResult.ItemNo = 4
        objResult.ActivityName = "ผล พื้นที่ตรวจแปลง"
        objResult.ActivityUnit = "ไร่"

        Dim objResult2 As New ActivityPlanReport(Session)
        objResult2.ItemNo = 5
        objResult2.ActivityName = "ผล แปลงผ่านมาตรฐาน"
        objResult2.ActivityUnit = "ไร่"

        For Each item As AuditCheckFarmPlanDetail In AuditCheckFarmPlanDetails
            'objPlan.ActivityYear = item.PlanYear
            'objResult.ActivityYear = item.PlanYear
            'objResult2.ActivityYear = item.PlanYear
            objPlan.Total += item.AreaSize
            objResult.Total += item.ActualAreaSize
            objResult2.Total += item.ActualPassArea

            objResult.Differrence += (item.ActualAreaSize - item.AreaSize)
            objResult2.Differrence += (item.ActualPassArea - item.ActualAreaSize)

            Select Case item.MonthNo
                Case PublicEnum.EnumMonth.APR
                    objPlan.APR = item.AreaSize
                    objResult.APR = item.ActualAreaSize
                    objResult2.APR = item.ActualPassArea
                Case PublicEnum.EnumMonth.AUG
                    objPlan.AUG = item.AreaSize
                    objResult.AUG = item.ActualAreaSize
                    objResult2.AUG = item.ActualPassArea
                Case PublicEnum.EnumMonth.DEC
                    objPlan.DEC = item.AreaSize
                    objResult.DEC = item.ActualAreaSize
                    objResult2.DEC = item.ActualPassArea
                Case PublicEnum.EnumMonth.FEB
                    objPlan.FEB = item.AreaSize
                    objResult.FEB = item.ActualAreaSize
                    objResult2.FEB = item.ActualPassArea
                Case PublicEnum.EnumMonth.JAN
                    objPlan.JAN = item.AreaSize
                    objResult.JAN = item.ActualAreaSize
                    objResult2.JAN = item.ActualPassArea
                Case PublicEnum.EnumMonth.JUL
                    objPlan.JUL = item.AreaSize
                    objResult.JUL = item.ActualAreaSize
                    objResult2.JUL = item.ActualPassArea
                Case PublicEnum.EnumMonth.JUN
                    objPlan.JUN = item.AreaSize
                    objResult.JUN = item.ActualAreaSize
                    objResult2.JUN = item.ActualPassArea
                Case PublicEnum.EnumMonth.MAR
                    objPlan.MAR = item.AreaSize
                    objResult.MAR = item.ActualAreaSize
                    objResult2.MAR = item.ActualPassArea
                Case PublicEnum.EnumMonth.MAY
                    objPlan.MAY = item.AreaSize
                    objResult.MAY = item.ActualAreaSize
                    objResult2.MAY = item.ActualPassArea
                Case PublicEnum.EnumMonth.NOV
                    objPlan.NOV = item.AreaSize
                    objResult.NOV = item.ActualAreaSize
                    objResult2.NOV = item.ActualPassArea
                Case PublicEnum.EnumMonth.OCT
                    objPlan.OCT = item.AreaSize
                    objResult.OCT = item.ActualAreaSize
                    objResult2.OCT = item.ActualPassArea
                Case PublicEnum.EnumMonth.SEP
                    objPlan.SEP = item.AreaSize
                    objResult.SEP = item.ActualAreaSize
                    objResult2.SEP = item.ActualPassArea
            End Select
        Next

        objList.Add(objPlan)
        objList.Add(objResult)
        objList.Add(objResult2)
    End Sub

    Public Sub InsertBuyPlanList(objList As BindingList(Of ActivityPlanReport))

        Dim objPlan As New ActivityPlanReport(Session)
        objPlan.ItemNo = 6
        objPlan.ActivityName = "แผนจัดซื้อ"
        objPlan.ActivityUnit = "ตัน*"

        Dim objResult As New ActivityPlanReport(Session)
        objResult.ItemNo = 7
        objResult.ActivityName = "ผลจัดซื้อ"
        objResult.ActivityUnit = "ตัน*"

        For Each item As AuditBuyPlanDetail In AuditBuyPlanDetails
            'objPlan.ActivityYear = item.PlanYear
            'objResult.ActivityYear = item.PlanYear
            objPlan.Total += item.Quantity
            objResult.Total += item.ActualQuantity
            objResult.Differrence += (item.ActualQuantity - item.Quantity)
            Select Case item.MonthNo
                Case PublicEnum.EnumMonth.APR
                    objPlan.APR = item.Quantity
                    objResult.APR = item.ActualQuantity
                Case PublicEnum.EnumMonth.AUG
                    objPlan.AUG = item.Quantity
                    objResult.AUG = item.ActualQuantity
                Case PublicEnum.EnumMonth.DEC
                    objPlan.DEC = item.Quantity
                    objResult.DEC = item.ActualQuantity
                Case PublicEnum.EnumMonth.FEB
                    objPlan.FEB = item.Quantity
                    objResult.FEB = item.ActualQuantity
                Case PublicEnum.EnumMonth.JAN
                    objPlan.JAN = item.Quantity
                    objResult.JAN = item.ActualQuantity
                Case PublicEnum.EnumMonth.JUL
                    objPlan.JUL = item.Quantity
                    objResult.JUL = item.ActualQuantity
                Case PublicEnum.EnumMonth.JUN
                    objPlan.JUN = item.Quantity
                    objResult.JUN = item.ActualQuantity
                Case PublicEnum.EnumMonth.MAR
                    objPlan.MAR = item.Quantity
                    objResult.MAR = item.ActualQuantity
                Case PublicEnum.EnumMonth.MAY
                    objPlan.MAY = item.Quantity
                    objResult.MAY = item.ActualQuantity
                Case PublicEnum.EnumMonth.NOV
                    objPlan.NOV = item.Quantity
                    objResult.NOV = item.ActualQuantity
                Case PublicEnum.EnumMonth.OCT
                    objPlan.OCT = item.Quantity
                    objResult.OCT = item.ActualQuantity
                Case PublicEnum.EnumMonth.SEP
                    objPlan.SEP = item.Quantity
                    objResult.SEP = item.ActualQuantity
            End Select
        Next

        objList.Add(objPlan)
        objList.Add(objResult)

    End Sub

    Public Sub InsertProcessPlanList(objList As BindingList(Of ActivityPlanReport))

        Dim objPlan As New ActivityPlanReport(Session)
        objPlan.ItemNo = 8
        objPlan.ActivityName = "แผนปรับปรุง"
        objPlan.ActivityUnit = "ตัน*"

        Dim objResult As New ActivityPlanReport(Session)
        objResult.ItemNo = 9
        objResult.ActivityName = "ผลปรับปรุง"
        objResult.ActivityUnit = "ตัน*"

        For Each item As AuditProcessPlanDetail In AuditProcessPlanDetails
            'objPlan.ActivityYear = item.PlanYear
            'objResult.ActivityYear = item.PlanYear
            objPlan.Total += item.Quantity
            objResult.Total += item.ActualQuantity
            objResult.Differrence += (item.ActualQuantity - item.Quantity)
            Select Case item.MonthNo
                Case PublicEnum.EnumMonth.APR
                    objPlan.APR = item.Quantity
                    objResult.APR = item.ActualQuantity
                Case PublicEnum.EnumMonth.AUG
                    objPlan.AUG = item.Quantity
                    objResult.AUG = item.ActualQuantity
                Case PublicEnum.EnumMonth.DEC
                    objPlan.DEC = item.Quantity
                    objResult.DEC = item.ActualQuantity
                Case PublicEnum.EnumMonth.FEB
                    objPlan.FEB = item.Quantity
                    objResult.FEB = item.ActualQuantity
                Case PublicEnum.EnumMonth.JAN
                    objPlan.JAN = item.Quantity
                    objResult.JAN = item.ActualQuantity
                Case PublicEnum.EnumMonth.JUL
                    objPlan.JUL = item.Quantity
                    objResult.JUL = item.ActualQuantity
                Case PublicEnum.EnumMonth.JUN
                    objPlan.JUN = item.Quantity
                    objResult.JUN = item.ActualQuantity
                Case PublicEnum.EnumMonth.MAR
                    objPlan.MAR = item.Quantity
                    objResult.MAR = item.ActualQuantity
                Case PublicEnum.EnumMonth.MAY
                    objPlan.MAY = item.Quantity
                    objResult.MAY = item.ActualQuantity
                Case PublicEnum.EnumMonth.NOV
                    objPlan.NOV = item.Quantity
                    objResult.NOV = item.ActualQuantity
                Case PublicEnum.EnumMonth.OCT
                    objPlan.OCT = item.Quantity
                    objResult.OCT = item.ActualQuantity
                Case PublicEnum.EnumMonth.SEP
                    objPlan.SEP = item.Quantity
                    objResult.SEP = item.ActualQuantity
            End Select
        Next

        objList.Add(objPlan)
        objList.Add(objResult)

    End Sub

    Public Sub InsertKeepPlanList(objList As BindingList(Of ActivityPlanReport))

        Dim objPlan As New ActivityPlanReport(Session)
        objPlan.ItemNo = 10
        objPlan.ActivityName = "แผนจัดเก็บ"
        objPlan.ActivityUnit = "ตัน"

        Dim objResult As New ActivityPlanReport(Session)
        objResult.ItemNo = 11
        objResult.ActivityName = "ผลการจัดเก็บ"
        objResult.ActivityUnit = "ตัน"

        For Each item As AuditKeepPlanDetail In AuditKeepPlanDetails
            'objPlan.ActivityYear = item.PlanYear
            'objResult.ActivityYear = item.PlanYear
            objPlan.Total += item.Quantity
            objResult.Total += item.ActualQuantity
            objResult.Differrence += (item.ActualQuantity - item.Quantity)
            Select Case item.MonthNo
                Case PublicEnum.EnumMonth.APR
                    objPlan.APR = item.Quantity
                    objResult.APR = item.ActualQuantity
                Case PublicEnum.EnumMonth.AUG
                    objPlan.AUG = item.Quantity
                    objResult.AUG = item.ActualQuantity
                Case PublicEnum.EnumMonth.DEC
                    objPlan.DEC = item.Quantity
                    objResult.DEC = item.ActualQuantity
                Case PublicEnum.EnumMonth.FEB
                    objPlan.FEB = item.Quantity
                    objResult.FEB = item.ActualQuantity
                Case PublicEnum.EnumMonth.JAN
                    objPlan.JAN = item.Quantity
                    objResult.JAN = item.ActualQuantity
                Case PublicEnum.EnumMonth.JUL
                    objPlan.JUL = item.Quantity
                    objResult.JUL = item.ActualQuantity
                Case PublicEnum.EnumMonth.JUN
                    objPlan.JUN = item.Quantity
                    objResult.JUN = item.ActualQuantity
                Case PublicEnum.EnumMonth.MAR
                    objPlan.MAR = item.Quantity
                    objResult.MAR = item.ActualQuantity
                Case PublicEnum.EnumMonth.MAY
                    objPlan.MAY = item.Quantity
                    objResult.MAY = item.ActualQuantity
                Case PublicEnum.EnumMonth.NOV
                    objPlan.NOV = item.Quantity
                    objResult.NOV = item.ActualQuantity
                Case PublicEnum.EnumMonth.OCT
                    objPlan.OCT = item.Quantity
                    objResult.OCT = item.ActualQuantity
                Case PublicEnum.EnumMonth.SEP
                    objPlan.SEP = item.Quantity
                    objResult.SEP = item.ActualQuantity
            End Select
        Next

        objList.Add(objPlan)
        objList.Add(objResult)

    End Sub

    Public Sub InsertSalePlanList(objList As BindingList(Of ActivityPlanReport))

        Dim objPlan As New ActivityPlanReport(Session)
        objPlan.ItemNo = 12
        objPlan.ActivityName = "แผนจำหน่าย"
        objPlan.ActivityUnit = "ตัน"

        Dim objResult As New ActivityPlanReport(Session)
        objResult.ItemNo = 13
        objResult.ActivityName = "ผลการจำหน่าย"
        objResult.ActivityUnit = "ตัน"

        For Each item As AuditSalePlanDetail In AuditSalePlanDetails
            'objPlan.ActivityYear = item.PlanYear
            'objResult.ActivityYear = item.PlanYear
            objPlan.Total += item.Quantity
            objResult.Total += item.ActualQuantity
            objResult.Differrence += (item.ActualQuantity - item.Quantity)
            Select Case item.MonthNo
                Case PublicEnum.EnumMonth.APR
                    objPlan.APR = item.Quantity
                    objResult.APR = item.ActualQuantity
                Case PublicEnum.EnumMonth.AUG
                    objPlan.AUG = item.Quantity
                    objResult.AUG = item.ActualQuantity
                Case PublicEnum.EnumMonth.DEC
                    objPlan.DEC = item.Quantity
                    objResult.DEC = item.ActualQuantity
                Case PublicEnum.EnumMonth.FEB
                    objPlan.FEB = item.Quantity
                    objResult.FEB = item.ActualQuantity
                Case PublicEnum.EnumMonth.JAN
                    objPlan.JAN = item.Quantity
                    objResult.JAN = item.ActualQuantity
                Case PublicEnum.EnumMonth.JUL
                    objPlan.JUL = item.Quantity
                    objResult.JUL = item.ActualQuantity
                Case PublicEnum.EnumMonth.JUN
                    objPlan.JUN = item.Quantity
                    objResult.JUN = item.ActualQuantity
                Case PublicEnum.EnumMonth.MAR
                    objPlan.MAR = item.Quantity
                    objResult.MAR = item.ActualQuantity
                Case PublicEnum.EnumMonth.MAY
                    objPlan.MAY = item.Quantity
                    objResult.MAY = item.ActualQuantity
                Case PublicEnum.EnumMonth.NOV
                    objPlan.NOV = item.Quantity
                    objResult.NOV = item.ActualQuantity
                Case PublicEnum.EnumMonth.OCT
                    objPlan.OCT = item.Quantity
                    objResult.OCT = item.ActualQuantity
                Case PublicEnum.EnumMonth.SEP
                    objPlan.SEP = item.Quantity
                    objResult.SEP = item.ActualQuantity
            End Select
        Next

        objList.Add(objPlan)
        objList.Add(objResult)

    End Sub

    <XafDisplayName("รายงานผลการตรวจตัดสินคุณภาพแปลงขยายพันธุ์")> _
    Public ReadOnly Property CheckFarmSummarys() As BindingList(Of CheckFarmSummary)
        Get
            Dim collReport As New BindingList(Of CheckFarmSummary)
            Dim objData As New CheckFarmSummary(Session)
            For Each item As AuditCheckFarmPlanDetail In AuditCheckFarmPlanDetails
                Dim objItem As New CheckFarmObject
                objItem.PlanSize = item.AreaSize
                objItem.ActualSize = item.ActualAreaSize
                objItem.PassSize = item.ActualPassArea
                objItem.FailSize = item.ActualAreaSize - item.ActualPassArea
                objItem.FullDamageSize = item.FullDamageArea

                'If item.FullDamageArea <> 0 Then
                '    objItem.FailSize = 0
                'Else
                '    objItem.FailSize = item.ActualAreaSize - item.ActualPassArea
                'End If

                objItem.FullDamageSize = item.FullDamageArea
                objItem.FailRemark = item.FailReason

                Select Case item.MonthNo
                    Case PublicEnum.EnumMonth.APR
                        objData.APR = objItem
                    Case PublicEnum.EnumMonth.AUG
                        objData.AUG = objItem
                    Case PublicEnum.EnumMonth.DEC
                        objData.DEC = objItem
                    Case PublicEnum.EnumMonth.FEB
                        objData.FEB = objItem
                    Case PublicEnum.EnumMonth.JAN
                        objData.JAN = objItem
                    Case PublicEnum.EnumMonth.JUL
                        objData.JUL = objItem
                    Case PublicEnum.EnumMonth.JUN
                        objData.JUN = objItem
                    Case PublicEnum.EnumMonth.MAR
                        objData.MAR = objItem
                    Case PublicEnum.EnumMonth.MAY
                        objData.MAY = objItem
                    Case PublicEnum.EnumMonth.NOV
                        objData.NOV = objItem
                    Case PublicEnum.EnumMonth.OCT
                        objData.OCT = objItem
                    Case PublicEnum.EnumMonth.SEP
                        objData.SEP = objItem
                End Select

            Next
            collReport.Add(objData)
            Return collReport

        End Get
    End Property

End Class
