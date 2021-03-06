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
<XafDisplayName("Ἱ�Ѵ����")> _
Public Class BuyPlan
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
        Session.Delete(BuyPlanDetails)
    End Sub

    Protected Overrides Sub OnLoaded()
        MyBase.OnLoaded()
        BuyPlanDetails.Sorting.Add(New SortProperty("TimeStamp", DB.SortingDirection.Ascending))
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

    Dim fMainPlan As MainPlan
    <XafDisplayName("��ҡ�ü�Ե")> _
    <RuleUniqueValue()> _
    Public Property MainPlan() As MainPlan
        Get
            Return fMainPlan
        End Get
        Set(ByVal value As MainPlan)
            SetPropertyValue(Of MainPlan)("MainPlan", fMainPlan, value)
        End Set
    End Property

    Dim fGrowStartDate As DateTime
    <XafDisplayName("�ѹ�����������Թ���")> _
    Public Property StartDate() As DateTime
        Get
            Return fGrowStartDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("GrowStartDate", fGrowStartDate, value)
        End Set
    End Property

    Dim fGrowEndDate As DateTime
    <XafDisplayName("�ѹ���Ҵ��Ҩ��������")> _
    Public Property EndDate() As DateTime
        Get
            Return fGrowEndDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("GrowEndDate", fGrowEndDate, value)
        End Set
    End Property

    Dim fRemark As String
    <XafDisplayName("�����˵�")> _
    <Size(500)> _
    Public Property Remark() As String
        Get
            Return fRemark
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Remark", fRemark, value)
        End Set
    End Property

    <XafDisplayName("������������������� (��.)")> _
    Public ReadOnly Property SumQuantity As Double
        Get
            Dim ret As Integer = 0
            For Each item As BuyPlanDetail In BuyPlanDetails
                ret += item.Quantity
            Next
            Return ret
        End Get
    End Property

    <XafDisplayName("�� ������������������� (��.)")> _
    Public ReadOnly Property SumActualQuantity As Double
        Get
            Dim ret As Integer = 0
            For Each item As BuyPlanDetail In BuyPlanDetails
                ret += item.ActualQuantity
            Next
            Return ret
        End Get
    End Property

    <Association("BuyPlanDetailReferencesBuyPlan", GetType(BuyPlanDetail))> _
   <XafDisplayName("����ҳ��Ъ�ǧ����㹡�èѴ���ŧ")> _
    Public ReadOnly Property BuyPlanDetails() As XPCollection(Of BuyPlanDetail)
        Get
            Return GetCollection(Of BuyPlanDetail)("BuyPlanDetails")
        End Get
    End Property

    Private _auditTrail As XPCollection(Of AuditDataItemPersistent)
    <XafDisplayName("����ѵԡ����䢢�����")> _
    Public ReadOnly Property AuditTrail() As XPCollection(Of AuditDataItemPersistent)
        Get
            If _auditTrail Is Nothing Then
                _auditTrail = AuditedObjectWeakReference.GetAuditTrail(Session, Me)
            End If
            Return _auditTrail
        End Get
    End Property

    Public Sub GenerateDetail(_session As Session, header As BuyPlan)
        Try
            Dim sMonth As Integer = 10
            Dim sYear As Integer = CInt(header.MainPlan.SeedYear) - 1

            For i As Integer = 0 To 11
                If sMonth > 12 Then
                    sMonth = 1
                    sYear += 1
                End If
                For j As Integer = 0 To 1
                    Dim sWeek As Integer = j + 1
                    Dim objPlanDetail As BuyPlanDetail = _session.FindObject(Of BuyPlanDetail)(CriteriaOperator.Parse("BuyPlan=? and PlanYear=? and MonthNo=? and WeekNo=?", header, sYear, sMonth, sWeek))
                    If objPlanDetail Is Nothing Then
                        objPlanDetail = New BuyPlanDetail(_session)
                        objPlanDetail.BuyPlan = header
                        objPlanDetail.PlanYear = sYear
                        objPlanDetail.MonthNo = sMonth
                        objPlanDetail.WeekNo = sWeek
                        objPlanDetail.TimeStamp = Date.Now
                        objPlanDetail.Save()
                        System.Threading.Thread.Sleep(100)
                    End If
                Next
                sMonth += 1

            Next
        Catch ex As Exception

        End Try
    End Sub

    <Action(Caption:="�ѹ�֡��¡���ѵ��ѵ�", ConfirmationMessage:="��ҹ��ͧ��úѹ�֡������ ���������?", ImageName:="Action_WindowList", AutoCommit:=True)> _
    Public Sub AutoGenerateItems()
        Try
            GenerateDetail(Session, Me)
            Session.CommitTransaction()
        Catch ex As Exception
            Session.RollbackTransaction()
        End Try

    End Sub

End Class
