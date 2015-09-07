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

<DeferredDeletion(False)> _
<DefaultClassOptions()> _
<XafDisplayName("แผนปลูก")> _
Public Class AuditGrowPlanDetail ' Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        TimeStamp = Date.Now
        DataOwner = GetCurrentSite()
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

    Dim fAuditActivityPlan As AuditActivityPlan
    <Association("AuditGrowPlanDetailReferencesAuditActivityPlan")> _
    Public Property AuditActivityPlan() As AuditActivityPlan
        Get
            Return fAuditActivityPlan
        End Get
        Set(ByVal value As AuditActivityPlan)
            SetPropertyValue(Of AuditActivityPlan)("AuditActivityPlan", fAuditActivityPlan, value)
        End Set
    End Property

    Dim fMonthNo As PublicEnum.EnumMonth
    <XafDisplayName("เดือน")> _
    Public Property MonthNo() As PublicEnum.EnumMonth
        Get
            Return fMonthNo
        End Get
        Set(ByVal value As PublicEnum.EnumMonth)
            SetPropertyValue(Of PublicEnum.EnumMonth)("MonthNo", fMonthNo, value)
        End Set
    End Property

    Dim fPlanYear As String
    <XafDisplayName("ปี")> _
    Public Property PlanYear() As String
        Get
            Return fPlanYear
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PlanYear", fPlanYear, value)
        End Set
    End Property

    'Dim fQuantity As Integer
    <XafDisplayName("แผนปลูก (ไร่)")> _
    Public ReadOnly Property Quantity() As Integer
        Get
            Dim ret As Integer = 0
            Dim collGrowPlanDetail As New XPCollection(Of GrowPlanDetail)(Session, CriteriaOperator.Parse("MonthNo=? and PlanYear=? and GrowPlan.MainPlan=?", Me.MonthNo, Me.PlanYear, Me.AuditActivityPlan.MainPlan))
            For Each item As GrowPlanDetail In collGrowPlanDetail
                ret += item.Quantity
            Next
            Return ret
        End Get
        'Set(ByVal value As Integer)
        '    SetPropertyValue(Of Integer)("Quantity", fQuantity, value)
        'End Set
    End Property

    <XafDisplayName("ผลการปลูก (ไร่)")> _
    Public ReadOnly Property ActualQuantity() As Integer
        Get
            Dim ret As Integer = 0
            Try
                Dim sDay As Integer = 0
                Dim eDay As Integer = 0

                sDay = 1
                eDay = Date.DaysInMonth(Convert.ToInt32(fPlanYear), CInt(MonthNo))

                Dim sDateString As String = Convert.ToInt32(fPlanYear).ToString & "-" & CInt(MonthNo).ToString & "-" & sDay.ToString
                Dim eDateString As String = Convert.ToInt32(fPlanYear).ToString & "-" & CInt(MonthNo).ToString & "-" & eDay.ToString
                Dim sDate As Date = Convert.ToDateTime(sDateString)
                Dim eDate As Date = Convert.ToDateTime(eDateString)
                Dim collGrow As New XPCollection(Of PlanFarmerGrow)(Session, CriteriaOperator.Parse("GrowDate >= ? and GrowDate <= ? and PlanFarmerFarm.PlanFarmer.PlanFarmerGroup.MainPlan=?", sDate, eDate, Me.AuditActivityPlan.MainPlan))

                If collGrow.Count > 0 Then
                    For Each item As PlanFarmerGrow In collGrow
                        ret += item.AreaSize
                    Next
                End If

            Catch ex As Exception

            End Try

            Return ret
        End Get

    End Property

    Dim fTimeStamp As DateTime
    <Browsable(False)> _
    Public Property TimeStamp() As DateTime
        Get
            Return fTimeStamp
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("TimeStamp", fTimeStamp, value)
        End Set
    End Property

End Class
