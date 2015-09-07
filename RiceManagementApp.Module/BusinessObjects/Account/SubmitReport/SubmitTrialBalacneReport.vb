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
Imports DevExpress.ExpressApp.ConditionalAppearance

<XafDisplayName("ส่งรายงาน งบทดลอง")> _
<ConditionalAppearance.Appearance("SubmitTrialBalacneReportDisableAllItems", criteria:="Status!='Draft'", Enabled:=False, TargetItems:="*", Context:="DetailView")> _
<DefaultClassOptions()> _
Public Class SubmitTrialBalacneReport
    Inherits BaseObject
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        'Me.SubmitDate = Date.Today
        'Me.SubmitBy = CType(SecuritySystem.CurrentUser, User).DisplayName
        Me.Status = PublicEnum.SubmitReportStatus.Draft
    End Sub


    Dim fStartDate As Date
    <XafDisplayName("จากวันที่")> _
    Public Property StartDate As Date
        Get
            Return fStartDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue(Of Date)("StartDate", fStartDate, value)
        End Set
    End Property

    Dim fEndDate As Date
    <XafDisplayName("ถึงวันที่")> _
    Public Property EndDate As Date
        Get
            Return fEndDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue(Of Date)("EndDate", fEndDate, value)
        End Set
    End Property

    Dim fMonth As PublicEnum.EnumMonth
    <XafDisplayName("เดือน")> _
    Public Property Month As PublicEnum.EnumMonth
        Get
            Return fMonth
        End Get
        Set(ByVal value As PublicEnum.EnumMonth)
            SetPropertyValue(Of PublicEnum.EnumMonth)("Month", fMonth, value)
        End Set
    End Property

    Dim fSubmitDate As Date = Today
    <Appearance("SubmitDateBalacne", Enabled:=False, Context:="DetailView")> _
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

    <Association("SubmitTrialBalacneReport-TrialBalanceData", GetType(TrialBalanceData))> _
<DevExpress.Xpo.Aggregated> _
<XafDisplayName("งบทดลอง")> _
    Public ReadOnly Property TrialBalanceDatas() As XPCollection(Of TrialBalanceData)
        Get
            Return GetCollection(Of TrialBalanceData)("TrialBalanceDatas")
        End Get
    End Property

    '<XafDisplayName("ข้อมูลงบทดลอง")> _
    'Public ReadOnly Property AccountBalances() As BindingList(Of TrialBalanceData)
    '    Get
    '        Dim fAccountBalances As New BindingList(Of TrialBalanceData)
    '        ''fPlanfarmers = New XPCollection(Of PlanFarmer)(Session, CriteriaOperator.Parse("PlanFarmerGroup.MainPlan=?", Me.MainPlan))
    '        Dim data As SelectedData = Session.ExecuteSproc("SP_GetTrialBalanceV7", New OperandValue("00000000.00000"), New OperandValue("60000000.00000"), New OperandValue(StartDate), New OperandValue(EndDate))

    '        For Each row As SelectStatementResultRow In data.ResultSet(0).Rows
    '            If row IsNot Nothing Then
    '                Dim tBalance As New TrialBalanceData(Session)
    '                tBalance.AccountID = row.Values(0)
    '                tBalance.AccountName = row.Values(1)
    '                If tBalance.AccountID.StartsWith("1") Or tBalance.AccountID.StartsWith("5") Then
    '                    If CDbl(row.Values(6)) > CDbl(row.Values(7)) Then
    '                        tBalance.TotalBalance = CDbl(row.Values(6)) - CDbl(row.Values(7))
    '                    ElseIf CDbl(row.Values(7)) > CDbl(row.Values(6)) Then
    '                        tBalance.TotalBalance = CDbl(row.Values(7)) - CDbl(row.Values(6))
    '                    ElseIf CDbl(row.Values(6)) = CDbl(row.Values(7)) Or CDbl(row.Values(7)) = CDbl(row.Values(6)) Then
    '                        tBalance.TotalBalance = 0
    '                    Else

    '                        If CDbl(row.Values(7)) > CDbl(row.Values(6)) Then
    '                            tBalance.TotalBalance = CDbl(row.Values(7)) - CDbl(row.Values(6))
    '                        ElseIf CDbl(row.Values(6)) > CDbl(row.Values(7)) Then
    '                            tBalance.TotalBalance = CDbl(row.Values(6)) - CDbl(row.Values(7))
    '                        ElseIf CDbl(row.Values(6)) = CDbl(row.Values(7)) Or CDbl(row.Values(7)) = CDbl(row.Values(6)) Then
    '                            tBalance.TotalBalance = 0
    '                        End If
    '                    End If
    '                End If
    '                fAccountBalances.Add(tBalance)
    '            End If
    '        Next

    '        Return fAccountBalances

    '    End Get
    'End Property

    <Action(Caption:="ส่งรายงาน", ConfirmationMessage:="ท่านต้องการส่งข้อมูลรายงานนี้ ใช่หรือไม่?", ImageName:="Action_Grant", AutoCommit:=True, TargetObjectsCriteria:="Status='Draft'")> _
    Public Sub MarkAsComplete()
        If Not IsDeleted Then
            If Not TrialBalanceDatas.Count > 0 Then
                MsgBox("ไม่พบรายชื่อเกษตรกรผู้จัดทำแปลง กรุณาตรวจสอบข้อมูลอีกครั้ง", MsgBoxStyle.Information, "")
                Exit Sub
            End If
            Try
                '//ประกาศตัวแปล webservice
                Dim objService As New RiceService.RiceService

                '//ประกาศ Object ของรายงาน ขพ.2 (Header)
                Dim objData As New RiceService.TrialBalanceHeader

                '//ข้อมูลตั้งค่าของศูนย์
                Dim objSiteInfo As SiteSetting = GetSiteInfo()

                '//ใส่ค่าให้กับ object รายงาน ขพ.2 (ข้อมูลส่วน Header)
                objData.SiteNo = objSiteInfo.SiteNo
                objData.SiteName = objSiteInfo.SiteName
                objData.BalanceDate = EndDate
                objData.SentDate = Date.Now
                objData.Month = Month
                '///---f-dsfdsfdsfds 
                objData.Year = Date.Now.Year + 543
                objData.SentBy = CType(SecuritySystem.CurrentUser, User).DisplayName

                'BalanceDate, SentDate, [Year], SentBy
                'objData.SubmitDate = Date.Now

                '//ตัวแปลของ ขพ.2 (Detail) (รายชื่อ และที่อยู่ เกษตรกร) ประเภท List
                Dim listOfDetail As New List(Of RiceService.TrialBalanceDetail)

                Dim data As SelectedData = Session.ExecuteSproc("SP_GetTrialBalanceV7", New OperandValue("00000000.00000"), New OperandValue("60000000.00000"), New OperandValue(StartDate), New OperandValue(EndDate))
                For Each row As SelectStatementResultRow In data.ResultSet(0).Rows
                    If row IsNot Nothing Then
                        Dim tBalance As New TrialBalanceData(Session)
                        tBalance.AccountID = row.Values(0)
                        tBalance.AccountName = row.Values(1)
                        If tBalance.AccountID.StartsWith("1") Or tBalance.AccountID.StartsWith("5") Then
                            tBalance.TotalBalance = CDbl(row.Values(6)) - CDbl(row.Values(7))
                        Else
                            tBalance.TotalBalance = CDbl(row.Values(7)) - CDbl(row.Values(6))
                        End If

                        Dim objDetail As New RiceService.TrialBalanceDetail
                        objDetail.AccountID = row.Values(0)
                        objDetail.AccountName = row.Values(1)

                        If tBalance.AccountID.StartsWith("1") Or tBalance.AccountID.StartsWith("5") Then
                            objDetail.TotalBalance = CDbl(row.Values(6)) - CDbl(row.Values(7))
                        Else
                            objDetail.TotalBalance = CDbl(row.Values(7)) - CDbl(row.Values(6))
                        End If

                        listOfDetail.Add(objDetail)
                    End If
                Next

                '//ใส่รายชื่อเกษตรกร (Detail) ในส่วนของ Header
                '//ต้องแปลง List ให้เป็น Array เพราะส่งข้อมูลผ่านเว็บเซอร์วิส ต้องเป็น Array 
                objData.TrialBalanceDetails = listOfDetail.ToArray

                '//ส่งข้อมูลผ่าน Webservice
                Dim ret As String = objService.SubmitDataTrialBalance("NTiSecureCode", objData)

                '//เป็นสถานะเป็น ส่งข้อมูลแล้ว (Sent)
                If ret = "Success" Then
                    Me.Status = PublicEnum.SubmitReportStatus.Sent
                    Me.SubmitDate = Date.Now
                    Me.SubmitBy = CType(SecuritySystem.CurrentUser, User).DisplayName
                    '//บันทึกข้อมูล
                    MsgBox("ส่งข้อมูลรายงาน เรียบร้อยแล้ว", MsgBoxStyle.Information)
                    Me.Save()
                Else
                    MsgBox("เกิดข้อผิดพลาด ไม่สามารถส่งรายงานได้ กรุณาติดต่อผู้ดูแลระบบ", MsgBoxStyle.Critical)
                End If

            Catch ex As Exception
                MsgBox("เกิดข้อผิดพลาด ไม่สามารถส่งรายงานได้ กรุณาติดต่อผู้ดูแลระบบ", MsgBoxStyle.Critical)
            End Try

        End If
    End Sub
    <Action(Caption:="ดึงข้อมูล", ConfirmationMessage:="ท่านต้องการดึงข้อมูล ใช่หรือไม่?", ImageName:="BO_Note", AutoCommit:=True, TargetObjectsCriteria:="Status='Draft'")> _
    Public Sub ImportData()
        Session.Delete(TrialBalanceDatas)

        Dim fAccountBalances As New BindingList(Of TrialBalanceData)
        ''fPlanfarmers = New XPCollection(Of PlanFarmer)(Session, CriteriaOperator.Parse("PlanFarmerGroup.MainPlan=?", Me.MainPlan))
        Dim data As SelectedData = Session.ExecuteSproc("SP_GetTrialBalanceV7", New OperandValue("00000000.00000"), New OperandValue("60000000.00000"), New OperandValue(StartDate), New OperandValue(EndDate))

        For Each row As SelectStatementResultRow In data.ResultSet(0).Rows
            If row IsNot Nothing Then
                Dim tBalance As New TrialBalanceData(Session)
                tBalance.AccountID = row.Values(0)
                tBalance.AccountName = row.Values(1)
                If tBalance.AccountID.StartsWith("1") Or tBalance.AccountID.StartsWith("5") Then
                    If CDbl(row.Values(6)) > CDbl(row.Values(7)) Then
                        tBalance.TotalBalance = CDbl(row.Values(6)) - CDbl(row.Values(7))
                    ElseIf CDbl(row.Values(7)) > CDbl(row.Values(6)) Then
                        tBalance.TotalBalance = CDbl(row.Values(7)) - CDbl(row.Values(6))
                    ElseIf CDbl(row.Values(6)) = CDbl(row.Values(7)) Or CDbl(row.Values(7)) = CDbl(row.Values(6)) Then
                        tBalance.TotalBalance = 0
                    End If

                    If CDbl(row.Values(7)) > CDbl(row.Values(6)) Then
                        tBalance.TotalBalance = CDbl(row.Values(7)) - CDbl(row.Values(6))
                    ElseIf CDbl(row.Values(6)) > CDbl(row.Values(7)) Then
                        tBalance.TotalBalance = CDbl(row.Values(6)) - CDbl(row.Values(7))
                    ElseIf CDbl(row.Values(6)) = CDbl(row.Values(7)) Or CDbl(row.Values(7)) = CDbl(row.Values(6)) Then
                        tBalance.TotalBalance = 0
                    End If
                End If
                TrialBalanceDatas.Add(tBalance)
            End If
            OnChanged("TrialBalanceData")
        Next
     
    End Sub

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

End Class
