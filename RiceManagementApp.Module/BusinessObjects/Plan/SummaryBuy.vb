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
<XafDisplayName("เบิกจ่ายเงินค่าจัดซื้อเมล็ดพันธุ์")> _
<RuleCriteria("SummaryBuy.NotDelete", DefaultContexts.Delete, "ApproveStatus='NotApprove'", CustomMessageTemplate:="ไม่สามารถลบข้อมูลนี้ได้")> _
<ConditionalAppearance.Appearance("SummaryBuy.DisableAllItems", criteria:="ApproveStatus='Approve'", Enabled:=False, TargetItems:="*", Context:="DetailView")> _
Public Class SummaryBuy
    Inherits BaseObject
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        DataOwner = GetCurrentSite()
    End Sub

    Protected Overrides Sub OnSaving()
        If (Me.fRefNo Is Nothing) Then
            Dim prefix As String = ""
            Dim _year As String = (Date.Now.Year + 543).ToString
            prefix = _year

            Me.fRefNo = String.Format("{0}/{1:D4}", prefix, DistributedIdGeneratorHelper.Generate(Me.Session.DataLayer, Me.GetType().FullName, prefix))

        End If

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

    <Persistent("RefNo")> _
    Private fRefNo As String
    <PersistentAlias("fRefNo")> _
    <XafDisplayName("เลขที่อ้างอิง")> _
    Public ReadOnly Property RefNo() As String
        Get
            Return fRefNo
        End Get
    End Property

    Dim fMainPlan As MainPlan
    <ImmediatePostData()> _
    <RuleRequiredField(DefaultContexts.Save)> _
    <XafDisplayName("เป้าการผลิต")> _
    <VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    Public Property MainPlan() As MainPlan
        Get
            Return fMainPlan
        End Get
        Set(ByVal value As MainPlan)
            SetPropertyValue(Of MainPlan)("MainPlan", fMainPlan, value)
        End Set
    End Property

    Dim fApproveBuy As ApproveBuy
    <RuleRequiredField(DefaultContexts.Save)> _
    <XafDisplayName("อนุมัติจัดซื้อ")> _
    <ImmediatePostData()> _
    <DataSourceCriteria("[ApproveStatus]='Approve'")> _
    Public Property ApproveBuy() As ApproveBuy
        Get
            Return fApproveBuy
        End Get
        Set(ByVal value As ApproveBuy)
            SetPropertyValue(Of ApproveBuy)("ApproveBuy", fApproveBuy, value)
            If Not IsSaving AndAlso Not IsLoading Then
                MainPlan = value.MainPlan
                AssignDocNo = value.RefDocNo
                OnChanged("AssignDocNo")
                OnChanged("MainPlan")
            End If
        End Set
    End Property

    Dim fApproveCount As Integer
    <RuleRequiredField(DefaultContexts.Save)> _
    <XafDisplayName("ครั้งที่เบิกจ่าย")> _
    Public Property ApproveCount() As Integer
        Get
            Return fApproveCount
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("fApproveCount", fApproveCount, value)
        End Set
    End Property

    <VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    Public ReadOnly Property MainPlanSeedName As String
        Get
            If ApproveBuy IsNot Nothing Then
                Return ApproveBuy.MainPlanSeedName & " / " & ApproveCount.ToString
            Else
                Return ""
            End If
        End Get
    End Property

    <VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    Public ReadOnly Property MinPriceValue As Double
        Get
            Dim ret As Double = 9999
            For Each item As SummaryBuyFarmer In SummaryBuyFarmers
                For Each row As BuySeedWeight In item.BuySeedFarmer.BuySeedWeights
                    If row.PricePerUnit < ret Then
                        ret = row.PricePerUnit
                    End If
                Next
            Next
            Return ret
        End Get
    End Property

    <VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    Public ReadOnly Property MaxPriceValue As Double
        Get
            Dim ret As Double = 0
            For Each item As SummaryBuyFarmer In SummaryBuyFarmers
                For Each row As BuySeedWeight In item.BuySeedFarmer.BuySeedWeights
                    If row.PricePerUnit > ret Then
                        ret = row.PricePerUnit
                    End If
                Next
            Next
            Return ret
        End Get
    End Property

    Dim fAssignDocNo As PlanAssignBuyTeam
    <Size(50)> _
    <ImmediatePostData()> _
    <XafDisplayName("เลขที่คำสั่งแต่งตั้งคณะกรรมการจัดซื้อ")> _
    Public Property AssignDocNo() As PlanAssignBuyTeam
        Get
            Return fAssignDocNo
        End Get
        Set(ByVal value As PlanAssignBuyTeam)
            SetPropertyValue(Of PlanAssignBuyTeam)("AssignDocNo", fAssignDocNo, value)
        End Set
    End Property

    <XafDisplayName("วันที่คำสั่ง")> _
    Public ReadOnly Property AssignDocDate() As DateTime
        Get
            If AssignDocNo IsNot Nothing Then
                Return AssignDocNo.AssignDtae
            Else
                Return Nothing
            End If

        End Get
    End Property

    Dim fBuyStartDate As DateTime
    <RuleRequiredField(DefaultContexts.Save)> _
    <XafDisplayName("วันที่จัดซื้อ")> _
    <ImmediatePostData()> _
    Public Property BuyStartDate() As DateTime
        Get
            Return fBuyStartDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("BuyStartDate", fBuyStartDate, value)
        End Set
    End Property

    Dim fBuyEndDate As DateTime
    <RuleRequiredField(DefaultContexts.Save)> _
    <XafDisplayName("ถึงวันที่")> _
    <ImmediatePostData()> _
    Public Property BuyEndDate() As DateTime
        Get
            Return fBuyEndDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("BuyEndDate", fBuyEndDate, value)
        End Set
    End Property

    <XafDisplayName("จำนวนเกษตรกร (ราย)")> _
    Public ReadOnly Property FarmerCount As Double
        Get
            Return SummaryBuyFarmers.Count
        End Get
    End Property

    <XafDisplayName("น้ำหนัก รวม (กก.)")> _
    Public ReadOnly Property TotalQuantity As Double
        Get
            Dim ret As Double = 0
            For Each item As SummaryBuyFarmer In SummaryBuyFarmers
                ret += item.SumWeight
            Next
            Return ret
        End Get
    End Property

    <XafDisplayName("จำนวนเงิน รวม (กก.)")> _
    Public ReadOnly Property TotalAmount As Double
        Get
            Dim ret As Double = 0
            For Each item As SummaryBuyFarmer In SummaryBuyFarmers
                ret += item.TotalAmount
            Next
            Return ret
        End Get
    End Property


    'Dim fBuySeedFarmer As XPCollection(Of BuySeed)
    '<XafDisplayName("รายชื่อเกษตรกรที่จัดซื้อ")> _
    'Public ReadOnly Property BuySeedFarmer As XPCollection(Of BuySeed)
    '    Get
    '        fBuySeedFarmer = GetBuyFarmer()
    '        Return fBuySeedFarmer
    '    End Get
    'End Property

    Public Function GetBuyFarmer() As XPCollection(Of BuySeed)
        If MainPlan IsNot Nothing And Not BuyStartDate.Equals(Nothing) And Not BuyEndDate.Equals(Nothing) Then
            Dim collFarmer As New XPCollection(Of BuySeed)(Session, CriteriaOperator.Parse("MainPlan=? and BuyDate >= ? and BuyDate <= ? and ApproveBuy=? and IsApproveCash=False and BuyStatus='Approve' ", Me.MainPlan, Me.BuyStartDate, Me.BuyEndDate.AddDays(1).AddMinutes(-1), Me.ApproveBuy))
            Return collFarmer
        Else
            Return Nothing
        End If
    End Function

    <Action(Caption:="แสดงรายชื่อเกษตรกรที่จัดซื้อแล้ว", ImageName:="BO_Department", AutoCommit:=True)> _
    Public Sub LoadFarmer()
        Try
            Dim collFarmer As XPCollection(Of BuySeed) = GetBuyFarmer()
            If collFarmer.Count > 0 Then
                For Each item As BuySeed In collFarmer
                    Dim objNew As SummaryBuyFarmer = Session.FindObject(Of SummaryBuyFarmer)(CriteriaOperator.Parse("BuySeedFarmer=?", item))
                    If objNew Is Nothing Then
                        objNew = New SummaryBuyFarmer(Session)
                        objNew.SummaryBuy = Me
                        objNew.BuySeedFarmer = item
                        objNew.Save()

                    End If
                Next
                Session.CommitTransaction()
                OnChanged("SummaryBuyFarmers")
            Else
                MsgBox("ไม่พบข้อมูลการจัดซื้อ ตามช่วงเวลาดังกล่าว", MsgBoxStyle.Exclamation)
            End If
        Catch ex As Exception
            Session.RollbackTransaction()
        End Try

    End Sub

    Dim fApproveStatus As PublicEnum.PublicApprove
    <XafDisplayName("สถานะ")> _
    Public Property ApproveStatus() As PublicEnum.PublicApprove
        Get
            Return fApproveStatus
        End Get
        Set(ByVal value As PublicEnum.PublicApprove)
            SetPropertyValue(Of Integer)("ApproveStatus", fApproveStatus, value)
        End Set
    End Property

    <Association("SummaryBuy-SummaryBuyFarmers", GetType(SummaryBuyFarmer))> _
    <XafDisplayName("รายชื่อเกษตรกรที่จัดซื้อ")> _
    <DevExpress.Xpo.Aggregated> _
    Public ReadOnly Property SummaryBuyFarmers() As XPCollection(Of SummaryBuyFarmer)
        Get
            Return GetCollection(Of SummaryBuyFarmer)("SummaryBuyFarmers")
        End Get
    End Property

    '<Action(Caption:="อนุมัติ", ConfirmationMessage:="ท่านต้องการบันทึกข้อมูล ใช่หรือไม่?", ImageName:="Action_Grant", AutoCommit:=True, TargetObjectsCriteria:="ApproveStatus='NotApprove'")> _
    Public Function MarkAsComplete() As Boolean
        If Not IsDeleted Then
            If Not SummaryBuyFarmers.Count > 0 Then
                MsgBox("ไม่พบข้อมูลรายชื่อเกษตรกร กรุณาตรวจสอบข้อมูล", MsgBoxStyle.Information)
                Exit Function
            End If
            Try
                Me.fApproveStatus = PublicEnum.PublicApprove.Approve
                For Each item As SummaryBuyFarmer In SummaryBuyFarmers
                    item.BuySeedFarmer.IsApproveCash = True
                    item.Save()
                Next

                MyBase.Save()
                'Session.CommitTransaction()
                Return True
            Catch ex As Exception
                Return False
            End Try

        End If
    End Function
    '<Action(Caption:="ย้อนกลับ", ConfirmationMessage:="ท่านต้องการบันทึกข้อมูล ใช่หรือไม่?", ImageName:="Action_Deny", AutoCommit:=True, TargetObjectsCriteria:="ApproveStatus='Approve'")> _
    Public Function MarkCancle() As Boolean
        Try
            Me.fApproveStatus = PublicEnum.PublicApprove.NotApprove
            For Each item As SummaryBuyFarmer In SummaryBuyFarmers
                item.BuySeedFarmer.IsApproveCash = False
                item.Save()
            Next
            MyBase.Save()
            'Session.CommitTransaction()
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function

End Class
