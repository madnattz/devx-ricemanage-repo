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

<ImageName("sent3")> _
<XafDisplayName("ส่งรายงาน ปริมาณข้าวแดง 2")> _
<DefaultClassOptions()> _
<ConditionalAppearance.Appearance("SubmitCheckRedSeedtDisableAllItems", criteria:="Status!='Draft'", Enabled:=False, TargetItems:="*", Context:="DetailView")> _
Public Class SubmitCheckRedSeed
    Inherits BaseObject
    Implements ISubmitReportAble

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()

        Me.Status = PublicEnum.SubmitReportStatus.Draft
    End Sub

    Protected Overrides Sub OnLoaded()
        MyBase.OnLoaded()
        SubmitCheckRedSeedDetails.Sorting.Add(New SortProperty("ItemNo", DevExpress.Xpo.DB.SortingDirection.Ascending))
    End Sub

    <Association("SubmitCheckRedSeed-SubmitCheckRedSeedDetails", GetType(SubmitCheckRedSeedDetail))> _
    <DevExpress.Xpo.Aggregated> _
    <XafDisplayName("ข้อมูลปริมาณการตรวจสอบข้าวแดง")> _
    Public ReadOnly Property SubmitCheckRedSeedDetails() As XPCollection(Of SubmitCheckRedSeedDetail)
        Get
            Return GetCollection(Of SubmitCheckRedSeedDetail)("SubmitCheckRedSeedDetails")
        End Get
    End Property

    Dim fSeason As Season
    <Index(1)>
    <RuleRequiredField(DefaultContexts.Save)> _
    <XafDisplayName("ฤดู")> _
    <ImmediatePostData()> _
    Public Property Season() As Season
        Get
            Return fSeason
        End Get
        Set(ByVal value As Season)
            SetPropertyValue(Of Season)("Season", fSeason, value)
        End Set
    End Property

    Dim fSeedYear As String
    <Index(2)>
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

    Dim fSubmitDate As Date
    <Index(3)>
    <Appearance("SubmitDate", Enabled:=False)> _
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
    <Index(4)>
    <Appearance("SubmitBy", Enabled:=False)> _
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
    <Index(5)>
    <Appearance("Status", Enabled:=False)> _
    <XafDisplayName("สถานะ")> _
    Public Property Status() As PublicEnum.SubmitReportStatus
        Get
            Return fStatus
        End Get
        Set(ByVal value As PublicEnum.SubmitReportStatus)
            SetPropertyValue(Of PublicEnum.SubmitReportStatus)("Status", fStatus, value)
        End Set
    End Property

    '<Action(Caption:="ดึงข้อมูล", ConfirmationMessage:="ท่านต้องการดึงข้อมูล ใช่หรือไม่?", ImageName:="BO_Note", AutoCommit:=True, TargetObjectsCriteria:="Status='Draft'")> _
    Public Sub ImportData()

        Session.Delete(SubmitCheckRedSeedDetails)
        Dim data As SelectedData = Session.ExecuteSproc("SP_GetRedSeedReport", New OperandValue(Season.SeasonName), New OperandValue(SeedYear))

        Dim obj1 As New SubmitCheckRedSeedDetail(Session)
        Dim obj2 As New SubmitCheckRedSeedDetail(Session)
        Dim obj3 As New SubmitCheckRedSeedDetail(Session)
        Dim obj4 As New SubmitCheckRedSeedDetail(Session)
        Dim obj5 As New SubmitCheckRedSeedDetail(Session)
        Dim obj6 As New SubmitCheckRedSeedDetail(Session)
        Dim obj7 As New SubmitCheckRedSeedDetail(Session)
        Dim obj8 As New SubmitCheckRedSeedDetail(Session)
        Dim obj9 As New SubmitCheckRedSeedDetail(Session)
        Dim obj10 As New SubmitCheckRedSeedDetail(Session)
        Dim obj11 As New SubmitCheckRedSeedDetail(Session)
        Dim obj12 As New SubmitCheckRedSeedDetail(Session)
        Dim obj13 As New SubmitCheckRedSeedDetail(Session)
        Dim obj14 As New SubmitCheckRedSeedDetail(Session)
        Dim obj15 As New SubmitCheckRedSeedDetail(Session)
        Dim obj16 As New SubmitCheckRedSeedDetail(Session)
        Dim obj17 As New SubmitCheckRedSeedDetail(Session)
        Dim obj18 As New SubmitCheckRedSeedDetail(Session)
        Dim obj19 As New SubmitCheckRedSeedDetail(Session)
        Dim obj20 As New SubmitCheckRedSeedDetail(Session)
        Dim obj21 As New SubmitCheckRedSeedDetail(Session)

        For Each row As SelectStatementResultRow In data.ResultSet(0).Rows
            If row IsNot Nothing Then


                obj1.SubmitCheckRedSeed = Me
                obj1.ItemNo = 1
                obj1.ItemCaption = "1. จำนวนตัวอย่างที่ตรวจทั้งหมด"
                obj1.MainValue = row.Values(0)
                obj1.BuyValue = row.Values(19)
                obj1.Save()

                obj2.SubmitCheckRedSeed = Me
                obj2.ItemNo = 2
                obj2.ItemCaption = "2. จำนวนตัวอย่างที่ตรวจไม่พบข้าวแดง"
                obj2.MainValue = row.Values(1)
                obj2.BuyValue = row.Values(20)
                obj2.Save()

                obj3.SubmitCheckRedSeed = Me
                obj3.ItemNo = 3
                obj3.ItemCaption = "3. จำนวนตัวอย่างที่ตรวจพบข้าวแดง"
                obj3.MainValue = row.Values(2)
                obj3.BuyValue = row.Values(21)
                obj3.Save()

                obj4.SubmitCheckRedSeed = Me
                obj4.ItemNo = 4
                obj4.ItemCaption = "4. แจกแจงความถี่ ตามข้อ 3"
                obj4.MainValue = " - "
                obj4.BuyValue = " - "
                obj4.Save()

                obj5.SubmitCheckRedSeed = Me
                obj5.ItemNo = 5
                obj5.ItemCaption = "    จำนวนเมล็ดข้าวแดง / 500 กรัม"
                obj5.MainValue = "จำนวนตัวอย่าง"
                obj5.BuyValue = "จำนวนตัวอย่าง"
                obj5.Save()

                obj6.SubmitCheckRedSeed = Me
                obj6.ItemNo = 6
                obj6.ItemCaption = "    1 เมล็ด"
                obj6.MainValue = row.Values(3)
                obj6.BuyValue = row.Values(22)
                obj6.Save()

                obj7.SubmitCheckRedSeed = Me
                obj7.ItemNo = 7
                obj7.ItemCaption = "    2 เมล็ด"
                obj7.MainValue = row.Values(4)
                obj7.BuyValue = row.Values(23)
                obj7.Save()

                obj8.SubmitCheckRedSeed = Me
                obj8.ItemNo = 8
                obj8.ItemCaption = "    3 เมล็ด"
                obj8.MainValue = row.Values(5)
                obj8.BuyValue = row.Values(24)
                obj8.Save()

                obj9.SubmitCheckRedSeed = Me
                obj9.ItemNo = 9
                obj9.ItemCaption = "    4 เมล็ด"
                obj9.MainValue = row.Values(6)
                obj9.BuyValue = row.Values(25)
                obj9.Save()

                obj10.SubmitCheckRedSeed = Me
                obj10.ItemNo = 10
                obj10.ItemCaption = "   5 เมล็ด"
                obj10.MainValue = row.Values(7)
                obj10.BuyValue = row.Values(26)
                obj10.Save()

                obj11.SubmitCheckRedSeed = Me
                obj11.ItemNo = 11
                obj11.ItemCaption = "   6 - 10 เมล็ด"
                obj11.MainValue = row.Values(8)
                obj11.BuyValue = row.Values(27)
                obj11.Save()

                obj12.SubmitCheckRedSeed = Me
                obj12.ItemNo = 12
                obj12.ItemCaption = "   11 - 15 เมล็ด"
                obj12.MainValue = row.Values(9)
                obj12.BuyValue = row.Values(28)
                obj12.Save()

                obj13.SubmitCheckRedSeed = Me
                obj13.ItemNo = 13
                obj13.ItemCaption = "   16 - 20 เมล็ด"
                obj13.MainValue = row.Values(10)
                obj13.BuyValue = row.Values(29)
                obj13.Save()

                obj14.SubmitCheckRedSeed = Me
                obj14.ItemNo = 14
                obj14.ItemCaption = "   21 - 25 เมล็ด"
                obj14.MainValue = row.Values(11)
                obj14.BuyValue = row.Values(30)
                obj14.Save()

                obj15.SubmitCheckRedSeed = Me
                obj15.ItemNo = 15
                obj15.ItemCaption = "   26 - 30 เมล็ด"
                obj15.MainValue = row.Values(12)
                obj15.BuyValue = row.Values(31)
                obj15.Save()

                obj16.SubmitCheckRedSeed = Me
                obj16.ItemNo = 16
                obj16.ItemCaption = "   31 - 40 เมล็ด"
                obj16.MainValue = row.Values(13)
                obj16.BuyValue = row.Values(32)
                obj16.Save()

                obj17.SubmitCheckRedSeed = Me
                obj17.ItemNo = 17
                obj17.ItemCaption = "   41 - 50 เมล็ด"
                obj17.MainValue = row.Values(14)
                obj17.BuyValue = row.Values(33)
                obj17.Save()

                obj18.SubmitCheckRedSeed = Me
                obj18.ItemNo = 18
                obj18.ItemCaption = "   51 - 100 เมล็ด"
                obj18.MainValue = row.Values(15)
                obj18.BuyValue = row.Values(34)
                obj18.Save()

                obj19.SubmitCheckRedSeed = Me
                obj19.ItemNo = 19
                obj19.ItemCaption = "   101-150"
                obj19.MainValue = row.Values(16)
                obj19.BuyValue = row.Values(35)
                obj19.Save()

                obj20.SubmitCheckRedSeed = Me
                obj20.ItemNo = 20
                obj20.ItemCaption = "   151 - 200 เมล็ด"
                obj20.MainValue = row.Values(17)
                obj20.BuyValue = row.Values(36)
                obj20.Save()

                obj21.SubmitCheckRedSeed = Me
                obj21.ItemNo = 21
                obj21.ItemCaption = "   มากกว่า 200 เมล็ด"
                obj21.MainValue = row.Values(18)
                obj21.BuyValue = row.Values(37)
                obj21.Save()


            End If
        Next

    End Sub

    '<Action(Caption:="ส่งรายงาน", ConfirmationMessage:="ท่านต้องการส่งข้อมูลรายงานนี้ ใช่หรือไม่?", ImageName:="sent2", AutoCommit:=True, TargetObjectsCriteria:="Status='Draft'")> _
    Public Function MarkAsComplete() As Boolean
        If Not IsDeleted Then
            If Not SubmitCheckRedSeedDetails.Count > 0 Then
                MsgBox("ไม่พบข้อมูลการตรวจสอบคุณภาพ กรุณาตรวจสอบข้อมูลอีกครั้ง", MsgBoxStyle.Information, "")
                Exit Function
            End If
            Try
                Dim objService As New RiceService.RiceService

                Dim objData As New RiceService.CheckRedSeedData
                For Each item As SubmitCheckRedSeedDetail In SubmitCheckRedSeedDetails

                    objData.SiteNo = item.siteNoField
                    objData.SiteName = item.siteNameField
                    objData.SeasonName = Season.SeasonName
                    objData.SeedYear = SeedYear

                    If item.ItemNo = 1 Then
                        objData.MainSeedAll = ConvertToInteger(item.MainValue)
                        objData.BuySeedAll = ConvertToInteger(item.BuyValue)
                    ElseIf item.ItemNo = 2 Then
                        objData.MainSeedNoRedSeed = ConvertToInteger(item.MainValue)
                        objData.BuySeedNoRedSeed = ConvertToInteger(item.BuyValue)
                    ElseIf item.ItemNo = 3 Then
                        objData.MainSeedRedSeed = ConvertToInteger(item.MainValue)
                        objData.BuySeedRedSeed = ConvertToInteger(item.BuyValue)
                    ElseIf item.ItemNo = 6 Then
                        objData.Main1 = ConvertToInteger(item.MainValue)
                        objData.Buy1 = ConvertToInteger(item.BuyValue)
                    ElseIf item.ItemNo = 7 Then
                        objData.Main2 = ConvertToInteger(item.MainValue)
                        objData.Buy2 = ConvertToInteger(item.BuyValue)
                    ElseIf item.ItemNo = 8 Then
                        objData.Main3 = ConvertToInteger(item.MainValue)
                        objData.Buy3 = ConvertToInteger(item.BuyValue)
                    ElseIf item.ItemNo = 9 Then
                        objData.Main4 = ConvertToInteger(item.MainValue)
                        objData.Buy4 = ConvertToInteger(item.BuyValue)
                    ElseIf item.ItemNo = 10 Then
                        objData.Main5 = ConvertToInteger(item.MainValue)
                        objData.Buy5 = ConvertToInteger(item.BuyValue)
                    ElseIf item.ItemNo = 11 Then
                        objData.Main6_10 = ConvertToInteger(item.MainValue)
                        objData.Buy6_10 = ConvertToInteger(item.BuyValue)
                    ElseIf item.ItemNo = 12 Then
                        objData.Main11_15 = ConvertToInteger(item.MainValue)
                        objData.Buy11_15 = ConvertToInteger(item.BuyValue)
                    ElseIf item.ItemNo = 13 Then
                        objData.Main16_20 = ConvertToInteger(item.MainValue)
                        objData.Buy16_20 = ConvertToInteger(item.BuyValue)
                    ElseIf item.ItemNo = 14 Then
                        objData.Main21_25 = ConvertToInteger(item.MainValue)
                        objData.Buy21_25 = ConvertToInteger(item.BuyValue)
                    ElseIf item.ItemNo = 15 Then
                        objData.Main26_30 = ConvertToInteger(item.MainValue)
                        objData.Buy26_30 = ConvertToInteger(item.BuyValue)
                    ElseIf item.ItemNo = 16 Then
                        objData.Main31_40 = ConvertToInteger(item.MainValue)
                        objData.Buy31_40 = ConvertToInteger(item.BuyValue)
                    ElseIf item.ItemNo = 17 Then
                        objData.Main41_50 = ConvertToInteger(item.MainValue)
                        objData.Buy41_50 = ConvertToInteger(item.BuyValue)
                    ElseIf item.ItemNo = 18 Then
                        objData.Main51_100 = ConvertToInteger(item.MainValue)
                        objData.Buy51_100 = ConvertToInteger(item.BuyValue)
                    ElseIf item.ItemNo = 19 Then
                        objData.Main101_150 = ConvertToInteger(item.MainValue)
                        objData.Buy101_150 = ConvertToInteger(item.BuyValue)
                    ElseIf item.ItemNo = 20 Then
                        objData.Main151_200 = ConvertToInteger(item.MainValue)
                        objData.Buy151_200 = ConvertToInteger(item.BuyValue)
                    ElseIf item.ItemNo = 21 Then
                        objData.MainMore200 = ConvertToInteger(item.MainValue)
                        objData.BuyMore200 = ConvertToInteger(item.BuyValue)
                    End If

                    objData.SubmitBy = CType(SecuritySystem.CurrentUser, User).DisplayName
                    objData.SubmitDate = Date.Now


                Next
                Dim ret As String = objService.SubmitCheckRedSeedReport("NTiSecureCode", objData)

                If ret = "Success" Then
                    Me.Status = PublicEnum.SubmitReportStatus.Sent
                    Me.SubmitDate = Date.Now
                    Me.SubmitBy = CType(SecuritySystem.CurrentUser, User).DisplayName
                    'MsgBox("ส่งข้อมูลรายงาน เรียบร้อยแล้ว", MsgBoxStyle.Information)
                    Me.Save()
                    Session.CommitTransaction()
                    Return True
                Else
                    'MsgBox("เกิดข้อผิดพลาด ไม่สามารถส่งรายงานได้ กรุณาติดต่อผู้ดูแลระบบ", MsgBoxStyle.Critical)
                    Return False
                End If

            Catch ex As Exception
                'MsgBox("เกิดข้อผิดพลาด ไม่สามารถส่งรายงานได้ กรุณาติดต่อผู้ดูแลระบบ", MsgBoxStyle.Critical)
                Return False
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
        If Me.Season IsNot Nothing AndAlso Me.SeedYear <> "" Then
            ImportData()
            SubmitCheckRedSeedDetails.Sorting.Add(New SortProperty("ItemNo", DevExpress.Xpo.DB.SortingDirection.Ascending))
        End If
       
    End Sub

    Public Function DoSubmitReport() As Boolean Implements ISubmitReportAble.DoSubmitReport
        Return MarkAsComplete()
    End Function
End Class

