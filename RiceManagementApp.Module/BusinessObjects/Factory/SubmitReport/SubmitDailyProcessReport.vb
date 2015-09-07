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

<XafDisplayName("ẺἹ��ػ�š�û�Ѻ��ا��Ҿ���紾ѹ�������ѹ")> _
<DefaultClassOptions()> _
<ConditionalAppearance.Appearance("SubmitDailyProcessReportDisableAllItems", criteria:="Status!='Draft'", Enabled:=False, TargetItems:="*", Context:="DetailView")> _
Public Class SubmitDailyProcessReport
    Inherits BaseObject
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        Me.Status = PublicEnum.SubmitReportStatus.Draft
    End Sub

    Dim fBiweekly As PublicEnum.EnumBiweekly
    <XafDisplayName("�ѡ��")> _
    <ImmediatePostData()> _
    <RuleValueComparison(DefaultContexts.Save, ValueComparisonType.NotEquals, "None")> _
    Public Property Biweekly() As PublicEnum.EnumBiweekly
        Get
            Return fBiweekly
        End Get
        Set(ByVal value As PublicEnum.EnumBiweekly)
            SetPropertyValue(Of PublicEnum.EnumBiweekly)("Biweekly", fBiweekly, value)
            If Not IsLoading AndAlso Not IsSaving Then
                Try
                    Dim sDay As Integer = 1
                    Dim eDay As Integer = 30

                    If Biweekly = PublicEnum.EnumBiweekly.WeekOne Then
                        sDay = 1
                        eDay = 15
                    ElseIf Biweekly = PublicEnum.EnumBiweekly.WeekTwo Then
                        sDay = 16
                        eDay = Date.DaysInMonth(CInt(ProcessYear) - 543, ProcessMonth)
                    End If

                    Dim sDate As New Date(CInt(ProcessYear) - 543, ProcessMonth, sDay)
                    Dim eDate As New Date(CInt(ProcessYear) - 543, ProcessMonth, eDay)

                    fFactoryProcessSeedDetails = New XPCollection(Of FactorySeedProcessToday)(Session, CriteriaOperator.Parse("ProcessDate >= ? and ProcessDate <= ?", sDate, eDate))
                    OnChanged("FactoryProcessSeedDetails")
                Catch ex As Exception

                End Try

            End If

        End Set
    End Property

    Dim fProcessMonth As PublicEnum.EnumMonth
    <XafDisplayName("��͹")> _
    <ImmediatePostData()> _
    <RuleValueComparison(DefaultContexts.Save, ValueComparisonType.NotEquals, "None")> _
    Public Property ProcessMonth() As PublicEnum.EnumMonth
        Get
            Return fProcessMonth
        End Get
        Set(ByVal value As PublicEnum.EnumMonth)
            SetPropertyValue(Of PublicEnum.EnumMonth)("ProcessMonth", fProcessMonth, value)
            If Not IsLoading AndAlso Not IsSaving Then
                Try
                    Dim sDay As Integer = 1
                    Dim eDay As Integer = 30

                    If Biweekly = PublicEnum.EnumBiweekly.WeekOne Then
                        sDay = 1
                        eDay = 15
                    ElseIf Biweekly = PublicEnum.EnumBiweekly.WeekTwo Then
                        sDay = 16
                        eDay = Date.DaysInMonth(CInt(ProcessYear) - 543, ProcessMonth)
                    End If

                    Dim sDate As New Date(CInt(ProcessYear) - 543, ProcessMonth, sDay)
                    Dim eDate As New Date(CInt(ProcessYear) - 543, ProcessMonth, eDay)

                    fFactoryProcessSeedDetails = New XPCollection(Of FactorySeedProcessToday)(Session, CriteriaOperator.Parse("ProcessDate >= ? and ProcessDate <= ?", sDate, eDate))
                    OnChanged("FactoryProcessSeedDetails")
                Catch ex As Exception

                End Try
               
            End If
        End Set
    End Property

    Dim fProcessYear As String
    <XafDisplayName("��")> _
    <RuleRequiredField(DefaultContexts.Save)> _
    <ModelDefault("PropertyEditorType", "RiceManagementApp.Module.Win.CustomYearDropdown")> _
    Public Property ProcessYear() As String
        Get
            Return fProcessYear
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ProcessYear", fProcessYear, value)
            If Not IsLoading AndAlso Not IsSaving Then
                Try
                    Dim sDay As Integer = 1
                    Dim eDay As Integer = 30

                    If Biweekly = PublicEnum.EnumBiweekly.WeekOne Then
                        sDay = 1
                        eDay = 15
                    ElseIf Biweekly = PublicEnum.EnumBiweekly.WeekTwo Then
                        sDay = 16
                        eDay = Date.DaysInMonth(CInt(ProcessYear) - 543, ProcessMonth)
                    End If

                    Dim sDate As New Date(CInt(ProcessYear) - 543, ProcessMonth, sDay)
                    Dim eDate As New Date(CInt(ProcessYear) - 543, ProcessMonth, eDay)

                    fFactoryProcessSeedDetails = New XPCollection(Of FactorySeedProcessToday)(Session, CriteriaOperator.Parse("ProcessDate >= ? and ProcessDate <= ?", sDate, eDate))
                    OnChanged("FactoryProcessSeedDetails")
                Catch ex As Exception

                End Try
               
            End If
        End Set
    End Property

    Dim fSubmitDate As Date
    <XafDisplayName("�ѹ�������§ҹ")> _
    Public Property SubmitDate() As Date
        Get
            Return fSubmitDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue(Of Date)("SubmitDate", fSubmitDate, value)
        End Set
    End Property
    Dim fSubmitBy As String
    <XafDisplayName("����§ҹ��")> _
    Public Property SubmitBy() As String
        Get
            Return fSubmitBy
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SubmitBy", fSubmitBy, value)
        End Set
    End Property

    Dim fStatus As PublicEnum.SubmitReportStatus
    <XafDisplayName("ʶҹ�")> _
    Public Property Status() As PublicEnum.SubmitReportStatus
        Get
            Return fStatus
        End Get
        Set(ByVal value As PublicEnum.SubmitReportStatus)
            SetPropertyValue(Of PublicEnum.SubmitReportStatus)("Status", fStatus, value)
        End Set
    End Property

    Dim fFactoryProcessSeedDetails As XPCollection(Of FactorySeedProcessToday)
    <XafDisplayName("���紾ѹ��줧��ѧ�ç�ҹ")> _
    Public ReadOnly Property FactoryProcessSeedDetails() As XPCollection(Of FactorySeedProcessToday)
        Get
            Try
                If fFactoryProcessSeedDetails Is Nothing Then

                    Dim sDay As Integer = 1
                    Dim eDay As Integer = 30

                    If Biweekly = PublicEnum.EnumBiweekly.WeekOne Then
                        sDay = 1
                        eDay = 15
                    ElseIf Biweekly = PublicEnum.EnumBiweekly.WeekTwo Then
                        sDay = 16
                        eDay = Date.DaysInMonth(CInt(ProcessYear) - 543, ProcessMonth)
                    End If

                    Dim sDate As New Date(CInt(ProcessYear) - 543, ProcessMonth, sDay)
                    Dim eDate As New Date(CInt(ProcessYear) - 543, ProcessMonth, eDay)

                    fFactoryProcessSeedDetails = New XPCollection(Of FactorySeedProcessToday)(Session, CriteriaOperator.Parse("ProcessDate >= ? and ProcessDate <= ?", sDate, eDate))

                End If
            Catch ex As Exception

            End Try

            Return fFactoryProcessSeedDetails
        End Get
    End Property

    <Action(Caption:="����§ҹ", ConfirmationMessage:="��ҹ��ͧ����觢�������§ҹ��� ���������?", ImageName:="sent2", AutoCommit:=True, TargetObjectsCriteria:="Status='Draft'")> _
    Public Sub MarkAsComplete()
        If Not IsDeleted Then
            If Not FactoryProcessSeedDetails.Count > 0 Then
                MsgBox("��辺��ª������紾ѹ����Ѻ��ا��Ҿ����ѹ ��سҵ�Ǩ�ͺ�������ա����", MsgBoxStyle.Information, "")
                Exit Sub
            End If
            Try
                '//��С�ȵ���� webservice
                Dim objService As New RiceService.RiceService

                '//��С�� Object �ͧ��§ҹ ẺἹ��ػ�š�û�Ѻ��ا��Ҿ���紾ѹ����������ѹ (Header)
                Dim objData As New RiceService.DailyProcessHeader

                '//�����ŵ�駤�Ңͧ�ٹ��
                Dim objSiteInfo As SiteSetting = GetSiteInfo()

                '//��������Ѻ object ��§ҹ ẺἹ��ػ�š�û�Ѻ��ا��Ҿ���紾ѹ����������ѹ(��������ǹ Header)
                objData.SiteNo = objSiteInfo.SiteNo
                objData.SiteName = objSiteInfo.SiteName
                objData.ProcessWeek = Biweekly
                objData.ProcessMonth = ProcessMonth
                objData.ProcessYear = ProcessYear
                objData.SubmitBy = CType(SecuritySystem.CurrentUser, User).DisplayName
                objData.SubmitDate = Date.Now

                Dim sDay As Integer = 1
                Dim eDay As Integer = 30

                If Biweekly = PublicEnum.EnumBiweekly.WeekOne Then
                    sDay = 1
                    eDay = 15
                ElseIf Biweekly = PublicEnum.EnumBiweekly.WeekTwo Then
                    sDay = 16
                    eDay = Date.DaysInMonth(CInt(ProcessYear) - 543, ProcessMonth)
                End If

                Dim sDate As New Date(CInt(ProcessYear) - 543, ProcessMonth, sDay)
                Dim eDate As New Date(CInt(ProcessYear) - 543, ProcessMonth, eDay)

                objData.ProcessDateFullName = "�ѹ��� " & sDate.ToString("d MMMM yyyy", New System.Globalization.CultureInfo("th-TH")) & _
                                                               "   �֧�ѹ��� " & eDate.ToString("d MMMM yyyy", New System.Globalization.CultureInfo("th-TH"))

                '//����Ţͧ ẺἹ��ػ�š�û�Ѻ��ا��Ҿ���紾ѹ����������ѹ(Detail) (�ç�ҹ��Т����š�û�Ѻ��ا) ������ List
                Dim listOfDetail As New List(Of RiceService.DailyProcessDetail)
                For Each item As FactorySeedProcessToday In FactoryProcessSeedDetails
                    Dim objDetail As New RiceService.DailyProcessDetail
                    objDetail.ProcessDate = item.ProcessDate
                    objDetail.PlantName = item.FactorySeedProcess.Plant.PlantName
                    objDetail.SeedName = item.FactorySeedProcess.SeedType.SeedName
                    objDetail.ClassName = item.FactorySeedProcess.SeedClass.ClassName
                    objDetail.SeasonName = item.FactorySeedProcess.Season.SeasonName
                    objDetail.SeedYear = item.FactorySeedProcess.SeedYear
                    objDetail.OutputQuantity = item.SeedProcessAmount
                    objDetail.NormalTime = item.NormalTime
                    objDetail.OverTime = item.OverTime
                    objDetail.ShiftTime = item.ShiftTime
                    objDetail.ImproveComplete = item.ImproveComplete
                    objDetail.FactoryName = item.FactorySeedProcess.FactoryNo
                    listOfDetail.Add(objDetail)
                Next

                '  //�����ª����ɵá� (Detail) ���ǹ�ͧ Header
                '  //��ͧ�ŧ List ����� Array �����觢����ż�ҹ���������� ��ͧ�� Array 
                objData.DailyProcessDetails = listOfDetail.ToArray

                ' //�觢����ż�ҹ Webservice
                Dim ret As String = objService.SubmitDataDailyProcessReport("NTiSecureCode", objData)

                '//��ʶҹ��� �觢��������� (Sent)
                If ret = "Success" Then
                    Me.Status = PublicEnum.SubmitReportStatus.Sent

                    ' //�ѹ�֡������
                    MsgBox("�觢�������§ҹ ���º��������", MsgBoxStyle.Information)
                    Me.Save()
                    Session.CommitTransaction()
                Else
                    MsgBox("�Դ��ͼԴ��Ҵ �������ö����§ҹ�� ��سҵԴ��ͼ������к�", MsgBoxStyle.Critical)
                End If

            Catch ex As Exception
                MsgBox("�Դ��ͼԴ��Ҵ �������ö����§ҹ�� ��سҵԴ��ͼ������к�", MsgBoxStyle.Critical)
            End Try

        End If
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
