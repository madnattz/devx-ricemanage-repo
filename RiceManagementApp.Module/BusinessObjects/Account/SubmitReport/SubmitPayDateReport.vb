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

<XafDisplayName("ส่งรายงาน เบิกจ่ายเกษตรกร")> _
<ConditionalAppearance.Appearance("SubmitPayDateReportDisableAllItems", criteria:="Status!='Draft'", Enabled:=False, TargetItems:="*", Context:="DetailView")> _
<DefaultClassOptions()> _
Public Class SubmitPayDateReport
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


    'Dim fStartDate As Date
    '<XafDisplayName("จากวันที่")> _
    'Public Property StartDate As Date
    '    Get
    '        Return fStartDate
    '    End Get
    '    Set(ByVal value As Date)
    '        SetPropertyValue(Of Date)("StartDate", fStartDate, value)
    '    End Set
    'End Property

    'Dim fEndDate As Date
    '<XafDisplayName("ถึงวันที่")> _
    'Public Property EndDate As Date
    '    Get
    '        Return fEndDate
    '    End Get
    '    Set(ByVal value As Date)
    '        SetPropertyValue(Of Date)("EndDate", fEndDate, value)
    '    End Set
    'End Property

    Private _SeasonID As Season
    <XafDisplayName("ฤดู")> _
    <ImmediatePostData()> _
    Public Property Season() As Season
        Get
            Return _SeasonID
        End Get
        Set(ByVal value As Season)
            SetPropertyValue("SeasonID", _SeasonID, value)
            OnChanged("PayDateDetails")
        End Set
    End Property

    Private _SeedYear As String
    <XafDisplayName("ปี")> _
    <ImmediatePostData()> _
    Public Property SeedYear() As String
        Get
            Return _SeedYear
        End Get
        Set(ByVal value As String)
            SetPropertyValue("SeedYear", _SeedYear, value)
            OnChanged("PayDateDetails")
        End Set
    End Property

    Dim fSeedMonth As PublicEnum.EnumMonth
    <XafDisplayName("เดือน")> _
    Public Property SeedMonth As PublicEnum.EnumMonth
        Get
            Return fSeedMonth
        End Get
        Set(ByVal value As PublicEnum.EnumMonth)
            SetPropertyValue(Of PublicEnum.EnumMonth)("SeedMonth", fSeedMonth, value)
        End Set
    End Property

    Private _FiscalYear As String
    <XafDisplayName("ปีงบประมาณ")> _
    <ImmediatePostData()> _
    Public Property FiscalYear() As String
        Get
            Return _FiscalYear
        End Get
        Set(ByVal value As String)
            SetPropertyValue("FiscalYear", _FiscalYear, value)
        End Set
    End Property

    Dim fSubmitDate As Date = Today
    <Appearance("SubmitDatePay", Enabled:=False, Context:="DetailView")> _
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

    <Association("SubmitPayDateReport-PayDateData", GetType(PayDateData))> _
<DevExpress.Xpo.Aggregated> _
<XafDisplayName("ข้อมูลเบิกจ่ายเกษตรกร")> _
    Public ReadOnly Property PayDateDatas() As XPCollection(Of PayDateData)
        Get
            Return GetCollection(Of PayDateData)("PayDateDatas")
        End Get
    End Property


    'Dim fPayDateDetails As XPCollection(Of PayDateDetail)
    'Public ReadOnly Property PayDateDetails() As XPCollection(Of PayDateDetail)
    '    Get
    '        If fPayDateDetails Is Nothing Then
    '            fPayDateDetails = New XPCollection(Of PayDateDetail)(Session, CriteriaOperator.Parse("No.SeasonID=? and No.YearID=?", Season, SeedYear))
    '        End If
    '        Return fPayDateDetails
    '    End Get
    'End Property

    <Action(Caption:="ส่งรายงาน", ConfirmationMessage:="ท่านต้องการส่งข้อมูลรายงานนี้ ใช่หรือไม่?", ImageName:="Action_Grant", AutoCommit:=True, TargetObjectsCriteria:="Status='Draft'")> _
    Public Sub MarkAsComplete()
        If Not IsDeleted Then
            If Not PayDateDatas.Count > 0 Then
                MsgBox("ไม่พบรายการเบิกจ่ายเกษตรกร กรุณาตรวจสอบข้อมูลอีกครั้ง", MsgBoxStyle.Information, "")
                Exit Sub
            End If

            'Dim accStartDate As Date = Date.MinValue
            'Dim accEndDate As Date = Date.MinValue
            'For Each item As AccountPeriodDetail In AccountPeriodDetails
            '    If item.ItemNo = 1 Then
            '        accStartDate = item.StartDate
            '        'Exit For
            '    End If
            'Next
            '//ประกาศตัวแปล webservice
            Dim objService As New RiceService.RiceService

            '//ประกาศ Object ของรายงาน ขพ.2 (Header)
            Dim objData As New RiceService.PayDateHeader
            Try
                '//ข้อมูลตั้งค่าของศูนย์
                Dim objSiteInfo As SiteSetting = GetSiteInfo()

                '//ใส่ค่าให้กับ object รายงาน ขพ.2 (ข้อมูลส่วน Header)
                objData.SiteNo = objSiteInfo.SiteNo
                objData.SiteName = objSiteInfo.SiteName
                objData.SentDate = Date.Now
                objData.SeasonName = Season.SeasonName
                objData.SeedMonth = SeedMonth
                objData.SeedYear = SeedYear
                objData.FiscalYear = FiscalYear
                objData.SentBy = CType(SecuritySystem.CurrentUser, User).DisplayName

                'Catch ex As Exception

                'End Try


                'BalanceDate, SentDate, [Year], SentBy
                'objData.SubmitDate = Date.Now

                '//ตัวแปลของ ขพ.2 (Detail) (รายชื่อ และที่อยู่ เกษตรกร) ประเภท List
                Dim listOfDetail As New List(Of RiceService.PayDateDetail)
                For Each item As PayDateData In PayDateDatas
                    Dim objDetail As New RiceService.PayDateDetail
                    objDetail.SeedName = item.SeedName.SeedName
                    objDetail.SeedClass = item.SeedClass.ClassName
                    objDetail.BatchNo = item.BatchNo
                    objDetail.RefNo = item.RefNo
                    objDetail.WeightDate = item.WeightDate
                    objDetail.RefDate = item.RefDate
                    objDetail.CenterPayDate = item.CenterPayDate
                    objDetail.PayDate = item.FamerPayDate
                    objDetail.TotalDays = item.DateCount
                    objDetail.TotalFamer = item.TotalFamer
                    objDetail.TotalWeight = item.TotalWeight
                    objDetail.TotalAmount = item.TotalAmount

                    listOfDetail.Add(objDetail)
                Next

                '//ใส่รายชื่อเกษตรกร (Detail) ในส่วนของ Header
                '//ต้องแปลง List ให้เป็น Array เพราะส่งข้อมูลผ่านเว็บเซอร์วิส ต้องเป็น Array 
                objData.PayDateDetails = listOfDetail.ToArray


                '//ส่งข้อมูลผ่าน Webservice
                Dim ret As String = objService.SubmitDataPayDate("NTiSecureCode", objData)

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
        Session.Delete(PayDateDatas)
        Dim _PayDateDatas = New XPCollection(Of PayDateDetail)(Session, CriteriaOperator.Parse("No.SeasonID=? and No.YearID=?", Season, SeedYear))
        If _PayDateDatas.Count > 0 Then
            For Each TblPayDateDetail As PayDateDetail In _PayDateDatas
                Dim InsertPayDateData As New PayDateData(Session)
                With InsertPayDateData
                    .SubmitPayDateReport = Me
                    .SeedName = TblPayDateDetail.SeedName
                    .SeedClass = TblPayDateDetail.SeedClass
                    .BatchNo = TblPayDateDetail.BatchNo
                    .RefNo = TblPayDateDetail.RefNo
                    .WeightDate = TblPayDateDetail.StartWeightDate
                    .RefDate = TblPayDateDetail.RefDate
                    .CenterPayDate = TblPayDateDetail.CenterPayDate
                    .FamerPayDate = TblPayDateDetail.FamerPayDate
                    .DateCount = TblPayDateDetail.DateCount
                    .TotalFamer = TblPayDateDetail.TotalFamer
                    .TotalWeight = TblPayDateDetail.TotalWeight
                    .TotalAmount = TblPayDateDetail.Amount
                    .Save()
                End With

            Next
        End If
        OnChanged("PayDateDatas")
        'Else
        'End If
        'OnChanged("PayDateDatas")

    End Sub

    'Dim result As New XPCollection(Of Account)(Session, CriteriaOperator.Parse("[LevelItem]>=3 AND PublicStatus = 0 AND LocalStatus = 0"))
    '    If result.Count > 0 Then
    '        For Each TblAccount As Account In result
    'Dim InsertBringforward As New Bringforward(Session)
    '            With InsertBringforward
    '                .AccountID = TblAccount.AccountID
    '                .AccountName = TblAccount.AccountName
    '                .Account = Me
    '                .Save()
    '            End With
    '        Next
    '    End If

    'Private Sub GetEventPayDateID(listPayDateDetail As XPCollection(Of PayDateDetail))
    '    For Each item As PayDateDetail In listPayDateDetail
    '        Dim objItem As PayDateData = Session.FindObject(Of PayDateData)(CriteriaOperator.Parse("SubmitPayDateReport=?", Me))
    '        If objItem Is Nothing Then
    '            objItem = New PayDateData(Session)
    '            objItem.SubmitPayDateReport = Me
    '            objItem.TotalWeight = item.TotalWeight
    '            objItem.TotalAmount = item.Amount
    '            objItem.TotalFamer = item.TotalFamer
    '            objItem.WeightDate = item.EndWeightDate
    '            objItem.FamerPayDate = item.FamerPayDate
    '            objItem.DateCount = item.DateCount
    '            'objItem.Save()
    '        End If
    '    Next
    'End Sub

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
