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
Public Class SummaryBuyFarmer
    Inherits BaseObject
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
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

    Dim fSummaryBuy As SummaryBuy
    <Association("SummaryBuy-SummaryBuyFarmers")> _
    Public Property SummaryBuy() As SummaryBuy
        Get
            Return fSummaryBuy
        End Get
        Set(ByVal value As SummaryBuy)
            SetPropertyValue(Of SummaryBuy)("SummaryBuy", fSummaryBuy, value)
        End Set
    End Property

    Dim fBuySeedFarmer As BuySeed
    <XafDisplayName("เกษตรกรที่จัดซื้อ")> _
    <ImmediatePostData()> _
    <DataSourceProperty("AvailableFarmer")> _
    Public Property BuySeedFarmer() As BuySeed
        Get
            Return fBuySeedFarmer
        End Get
        Set(ByVal value As BuySeed)
            SetPropertyValue(Of BuySeed)("BuySeedFarmer", fBuySeedFarmer, value)
        End Set
    End Property

    Private _AvailableFarmer As XPCollection(Of BuySeed)
    <Browsable(False)> _
    Public ReadOnly Property AvailableFarmer() As XPCollection(Of BuySeed)
        Get
            Try
                Dim _UsedItem As New List(Of String)

                Dim _UsedFarmer As New XPCollection(Of SummaryBuyFarmer)(Session, CriteriaOperator.Parse("SummaryBuy=?", Me.SummaryBuy))

                For Each item As SummaryBuyFarmer In _UsedFarmer
                    If item.BuySeedFarmer IsNot Nothing Then
                        _UsedItem.Add(item.BuySeedFarmer.BuySeedNo)
                    End If
                Next

                For Each item As SummaryBuyFarmer In Me.SummaryBuy.SummaryBuyFarmers
                    If item.BuySeedFarmer IsNot Nothing Then
                        _UsedItem.Add(item.BuySeedFarmer.BuySeedNo)
                    End If
                Next

                _UsedItem = _UsedItem.Distinct.ToList

                Dim crt As String = ""
                If _UsedItem.Count > 0 Then
                    For i As Integer = 0 To _UsedItem.Count - 1
                        If i <> _UsedItem.Count - 1 Then
                            crt &= "'" & _UsedItem(i) & "',"
                        Else
                            crt &= "'" & _UsedItem(i) & "'"
                        End If
                    Next
                End If

                _AvailableFarmer = New XPCollection(Of BuySeed)(Session, CriteriaOperator.Parse("MainPlan=? and BuyDate >= ? and BuyDate <= ? and IsApproveCash=False and BuyStatus='Approve' and Not BuySeedNo in(" & crt & ")", _
                                                                                                Me.SummaryBuy.MainPlan, Me.SummaryBuy.BuyStartDate, Me.SummaryBuy.BuyEndDate))

            Catch ex As Exception

            End Try
            Return _AvailableFarmer
        End Get
    End Property

    <XafDisplayName("รหัสเกษตรกร")> _
    Public ReadOnly Property PlanFarmerNo As String
        Get
            If BuySeedFarmer IsNot Nothing Then
                Return BuySeedFarmer.BuyFarmer.PlanFarmer.Farmer.MemberID
            Else
                Return 0
            End If
        End Get

    End Property

    <XafDisplayName("น้ำหนักสุทธิ (กก.)")> _
    Public ReadOnly Property SumWeight As Double
        Get
            If BuySeedFarmer IsNot Nothing Then
                Return BuySeedFarmer.SumWeight
            Else
                Return 0
            End If
        End Get

    End Property

    <XafDisplayName("จำนวนเงิน (บาท)")> _
    Public ReadOnly Property SumAmount As Double
        Get
            If BuySeedFarmer IsNot Nothing Then
                Return BuySeedFarmer.SumAmount
            Else
                Return 0
            End If

        End Get

    End Property

    <XafDisplayName("จำนวนกระสอบ (ใบ)")> _
    Public ReadOnly Property TotalBags As Double
        Get
            Dim ret As Double = 0
            If BuySeedFarmer IsNot Nothing Then

                For Each item As BuySeedWeight In BuySeedFarmer.BuySeedWeights
                    ret += item.Bags
                Next
            End If
            Return ret
        End Get
    End Property

    <XafDisplayName("ค่าพันธุ์ (บาท)")> _
    Public ReadOnly Property SeedLoanAmount As Double
        Get
            If BuySeedFarmer IsNot Nothing Then
                Return BuySeedFarmer.SeedLoanAmount
            Else
                Return 0
            End If
        End Get

    End Property

    <XafDisplayName("ค่าโอน (บาท)")> _
    Public ReadOnly Property TransferAmount As Double
        Get
            If BuySeedFarmer IsNot Nothing Then
                Return BuySeedFarmer.TransferAmount
            Else
                Return 0
            End If
        End Get

    End Property

    <XafDisplayName("รับจริง (บาท)")> _
    Public ReadOnly Property TotalAmount As Double
        Get
            If BuySeedFarmer IsNot Nothing Then
                Return SumAmount - SeedLoanAmount - TransferAmount
            Else
                Return 0
            End If
        End Get

    End Property

End Class
