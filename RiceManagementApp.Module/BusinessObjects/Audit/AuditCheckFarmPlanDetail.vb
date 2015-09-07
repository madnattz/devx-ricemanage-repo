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

<DefaultClassOptions()> _
<XafDisplayName("แผนตรวจแปลง")> _
Public Class AuditCheckFarmPlanDetail
    Inherits BaseObject
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
    <Association("AuditCheckFarmPlanDetailReferencesAuditActivityPlan")> _
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

    Dim fQuantity As Integer
    <XafDisplayName("แผนตรวแปลง (ไร่)")> _
    Public Property AreaSize() As Integer
        Get
            Return fQuantity
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Quantity", fQuantity, value)
        End Set
    End Property

    <XafDisplayName("ผล พื้นที่ตรวจแปลง (ไร่)")> _
    Public ReadOnly Property ActualAreaSize() As Integer
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
                Dim coll As New XPCollection(Of CheckFarm)(Session, CriteriaOperator.Parse("CheckResults <> 'Pending' and CheckDate >= ? and CheckDate <= ? and PlanFarmer.PlanFarmerGroup.MainPlan=?", sDate, eDate, Me.AuditActivityPlan.MainPlan))

                If coll.Count > 0 Then
                    For Each item As CheckFarm In coll
                        ret += item.CheckArea
                    Next
                End If

            Catch ex As Exception

            End Try

            Return ret
        End Get

    End Property

    <XafDisplayName("ผล แปลงที่ผ่านมาตรฐาน (ไร่)")> _
    Public ReadOnly Property ActualPassArea() As Integer
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
                'Dim coll As New XPCollection(Of CheckFarm)(Session, CriteriaOperator.Parse("CheckResults='Pass' and CheckDate >= ? and CheckDate <= ? and PlanFarmer.PlanFarmerGroup.MainPlan=?", sDate, eDate, Me.AuditActivityPlan.MainPlan))
                Dim coll As New XPCollection(Of CheckFarm)(Session, CriteriaOperator.Parse("CheckResults <> 'Pending' and CheckDate >= ? and CheckDate <= ? and PlanFarmer.PlanFarmerGroup.MainPlan=?", sDate, eDate, Me.AuditActivityPlan.MainPlan))
                If coll.Count > 0 Then
                    For Each item As CheckFarm In coll
                        ret += item.PassAreaSize
                    Next
                End If

            Catch ex As Exception

            End Try

            Return ret
        End Get

    End Property

    <XafDisplayName("ผล เสียหายก่อนตรวจ (ไร่)")> _
    Public ReadOnly Property FullDamageArea() As Integer
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
                'Dim coll As New XPCollection(Of CheckFarm)(Session, CriteriaOperator.Parse("CheckResults='Fail2' and CheckDate >= ? and CheckDate <= ? and PlanFarmer.PlanFarmerGroup.MainPlan=?", sDate, eDate, Me.AuditActivityPlan.MainPlan))
                Dim coll As New XPCollection(Of CheckFarm)(Session, CriteriaOperator.Parse("CheckResults <> 'Pending' and CheckDate >= ? and CheckDate <= ? and PlanFarmer.PlanFarmerGroup.MainPlan=?", sDate, eDate, Me.AuditActivityPlan.MainPlan))

                If coll.Count > 0 Then
                    For Each item As CheckFarm In coll
                        ret += item.DamageArea
                    Next
                End If

            Catch ex As Exception

            End Try

            Return ret
        End Get

    End Property

    <XafDisplayName("สาเหตุที่ไม่ผ่าน")> _
    Public ReadOnly Property FailReason() As String
        Get
            Dim ret As String = ""
            Try
                Dim sDay As Integer = 0
                Dim eDay As Integer = 0

                sDay = 1
                eDay = Date.DaysInMonth(Convert.ToInt32(fPlanYear), CInt(MonthNo))

                Dim sDateString As String = Convert.ToInt32(fPlanYear).ToString & "-" & CInt(MonthNo).ToString & "-" & sDay.ToString
                Dim eDateString As String = Convert.ToInt32(fPlanYear).ToString & "-" & CInt(MonthNo).ToString & "-" & eDay.ToString
                Dim sDate As Date = Convert.ToDateTime(sDateString)
                Dim eDate As Date = Convert.ToDateTime(eDateString)
                Dim coll As New XPCollection(Of CheckFarm)(Session, CriteriaOperator.Parse("(CheckResults='Fail' or CheckResults='Fail2') and CheckDate >= ? and CheckDate <= ? and PlanFarmer.PlanFarmerGroup.MainPlan=?", sDate, eDate, Me.AuditActivityPlan.MainPlan))

                If coll.Count > 0 Then
                    For Each item As CheckFarm In coll
                        ret += item.FailReason & ","
                    Next
                End If

                Dim FailList As New List(Of String)
                Dim separator() As String = {","}
                Dim str() As String = ret.Split(separator, StringSplitOptions.RemoveEmptyEntries)
                FailList = str.ToList
                FailList = FailList.Distinct.ToList

                ret = ""
                For i As Integer = 0 To FailList.Count - 1
                    If i <> FailList.Count - 1 Then
                        ret &= SwapString(FailList(i)) & ","
                    Else
                        ret &= SwapString(FailList(i))
                    End If
                Next

            Catch ex As Exception

            End Try

            Return ret
        End Get

    End Property

    Public Function SwapString(_data As String) As String
        Select Case _data
            Case "1"
                Return "ป"
            Case "2"
                Return "ด"
            Case "3"
                Return "ว"
            Case "4"
                Return "ข"
            Case "5"
                Return "ร"
            Case "6"
                Return "ม"
            Case "7"
                Return "น"
            Case "8"
                Return "ล"
            Case Else
                Return ""
        End Select
    End Function


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
