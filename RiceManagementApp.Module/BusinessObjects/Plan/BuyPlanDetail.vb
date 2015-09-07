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

'<ImageName("BO_Contact")> _
'<DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")> _
'<DefaultListViewOptions(MasterDetailMode.ListViewOnly, False, NewItemRowPosition.None)> _
'<Persistent("DatabaseTableName")> _
<DeferredDeletion(False)> _
<DefaultClassOptions()> _
<XafDisplayName("ปริมาณและช่วงเวลาในการจัดทำแปลง")> _
Public Class BuyPlanDetail ' Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        DataOwner = GetCurrentSite()
        TimeStamp = Date.Now
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

    Dim fBuyPlan As BuyPlan
    <Association("BuyPlanDetailReferencesBuyPlan")> _
    Public Property BuyPlan() As BuyPlan
        Get
            Return fBuyPlan
        End Get
        Set(ByVal value As BuyPlan)
            SetPropertyValue(Of BuyPlan)("BuyPlan", fBuyPlan, value)
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

    Dim fWeekNo As PublicEnum.WeekNo
    <XafDisplayName("สัปดาห์ที่")> _
    Public Property WeekNo() As PublicEnum.WeekNo
        Get
            Return fWeekNo
        End Get
        Set(ByVal value As PublicEnum.WeekNo)
            SetPropertyValue(Of PublicEnum.WeekNo)("WeekNo", fWeekNo, value)
        End Set
    End Property
    Dim fQuantity As Integer
    <XafDisplayName("จัดซื้อ (กก.)")> _
    Public Property Quantity() As Double
        Get
            Return fQuantity
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("Quantity", fQuantity, value)
        End Set
    End Property

    <XafDisplayName("จัดซื้อจริง (กก.)")> _
    Public ReadOnly Property ActualQuantity() As Double
        Get
            Dim ret As Double = 0
            Try
                Dim sDay As Integer = 0
                Dim eDay As Integer = 0
                If WeekNo = PublicEnum.WeekNo.OneAndTwo Then
                    sDay = 1
                    eDay = 15
                Else
                    sDay = 16
                    eDay = Date.DaysInMonth(Convert.ToInt32(fPlanYear), CInt(MonthNo))
                End If

                Dim sDateString As String = Convert.ToInt32(fPlanYear).ToString & "-" & CInt(MonthNo).ToString & "-" & sDay.ToString
                Dim eDateString As String = Convert.ToInt32(fPlanYear).ToString & "-" & CInt(MonthNo).ToString & "-" & eDay.ToString
                Dim sDate As Date = Convert.ToDateTime(sDateString)
                Dim eDate As Date = Convert.ToDateTime(eDateString)
                Dim collBuy As New XPCollection(Of BuySeed)(Session, CriteriaOperator.Parse("BuyStatus='Approve' and BuyDate >= ? and BuyDate <= ? and MainPlan=?", sDate, eDate, Me.BuyPlan.MainPlan))
                If collBuy.Count > 0 Then
                    For Each item As BuySeed In collBuy
                        ret += item.SumWeight
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
