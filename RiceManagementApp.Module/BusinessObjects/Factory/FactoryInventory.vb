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
<ConditionalAppearance.Appearance("FactoryInventoryDisabledAllItems", criteria:="Status='Done'", Enabled:=False, TargetItems:="*", Context:="DetailView")> _
Public Class FactoryInventory
    Inherits BaseObject
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        Status = PublicEnum.FactoryProcessStatus.Doing
        DataOwner = GetCurrentSite()
    End Sub

    Protected Overrides Sub OnSaving()
        MyBase.OnSaving()
        If Me.DataOwner Is Nothing Then
            DataOwner = GetCurrentSite()
        End If
        Me.fUpdateDate = Date.Now
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

    Dim fSeedProduct As SeedProduct
    Public Property SeedProduct() As SeedProduct
        Get
            Return fSeedProduct
        End Get
        Set(ByVal value As SeedProduct)
            SetPropertyValue(Of SeedProduct)("SeedProduct", fSeedProduct, value)
        End Set
    End Property
    Dim fFactoryNo As PublicEnum.EnumFactoryNo
    Public Property FactoryNo() As PublicEnum.EnumFactoryNo
        Get
            Return fFactoryNo
        End Get
        Set(ByVal value As PublicEnum.EnumFactoryNo)
            SetPropertyValue(Of PublicEnum.EnumFactoryNo)("FactoryNo", fFactoryNo, value)
        End Set
    End Property
    Dim fReceiveAmount As Double
    <ModelDefault("DisPlayFormat", ("{0:#,#0.00}"))> _
    Public Property ReceiveAmount() As Double
        Get
            Return fReceiveAmount
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("ReceiveAmount", fReceiveAmount, value)
        End Set
    End Property

    <ModelDefault("DisPlayFormat", ("{0:#,#0.00}"))> _
    <PersistentAlias("FactoryProcessSeedDetails.Sum(OutputQuantity)")> _
    <ImmediatePostData()> _
    Public ReadOnly Property ProcessAmount() As Double
        Get
            Try
                Dim tempObject As Object
                tempObject = EvaluateAlias("ProcessAmount")
                Return CDbl(tempObject)
            Catch ex As Exception
                Return 0
            End Try

        End Get

    End Property

    Dim fUpdateDate As DateTime
    Public Property UpdateDate() As DateTime
        Get
            Return fUpdateDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("UpdateDate", fUpdateDate, value)
        End Set
    End Property

    Enum UpdateStockType
        '//รับ
        income
        '//จ่าย
        outcome
        '//ปรับปรุง
        'process
    End Enum

    Public Sub UpdateStockAmount(_type As UpdateStockType, _quantity As Double)
        Select Case _type
            Case UpdateStockType.income
                Me.ReceiveAmount += _quantity
            Case UpdateStockType.outcome
                Me.ReceiveAmount -= _quantity
                'Case UpdateStockType.process
                '    Me.ProcessAmount += _quantity
        End Select
    End Sub

    Dim fStatus As PublicEnum.FactorySeedProcess
    <XafDisplayName("สถานะ")> _
    Public Property Status() As PublicEnum.FactorySeedProcess
        Get
            Return fStatus
        End Get
        Set(ByVal value As PublicEnum.FactorySeedProcess)
            SetPropertyValue(Of PublicEnum.FactorySeedProcess)("Status", fStatus, value)
        End Set
    End Property

#Region "Referance Table"
    Private fRefTransaction As XPCollection(Of FactoryTransaction)
    <XafDisplayName("รายการเคลื่อนไหวเมล็ดพันธุ์")> _
    Public ReadOnly Property RefTransaction() As XPCollection(Of FactoryTransaction)
        Get
            Try
                If fRefTransaction Is Nothing Then
                    fRefTransaction = New XPCollection(Of FactoryTransaction)(Session, CriteriaOperator.Parse("SeedProduct.Oid=? And FactoryNo=? ", Me.fSeedProduct.Oid, Me.fFactoryNo))
                End If
                Return fRefTransaction
            Catch ex As Exception
                Return Nothing
            End Try

        End Get
    End Property

    '<Association("FactoryInventory-FactoryProcessSeedDetails", GetType(FactoryProcessSeedDetail))> _
    '<ImmediatePostData()> _
    '<XafDisplayName("ข้อมูลการปรับปรุงสภาพรายวัน")> _
    'Public ReadOnly Property FactoryProcessSeedDetails() As XPCollection(Of FactoryProcessSeedDetail)
    '    Get
    '        Return GetCollection(Of FactoryProcessSeedDetail)("FactoryProcessSeedDetails")
    '    End Get
    'End Property

    Private _auditTrail As XPCollection(Of AuditDataItemPersistent)
    <XafDisplayName("ประวัติการแก้ไขข้อมูล")> _
    Public ReadOnly Property AuditTrail() As XPCollection(Of AuditDataItemPersistent)
        Get
            If _auditTrail Is Nothing Then
                _auditTrail = AuditedObjectWeakReference.GetAuditTrail(Session, Me)
            End If
            Return _auditTrail
        End Get
    End Property

#End Region

    '<Action(Caption:="เสร็จสิ้นการปรับปรุง", ConfirmationMessage:="ท่านต้องการบันทึกข้อมูล ใช่หรือไม่?", ImageName:="Action_Grant", AutoCommit:=True, TargetObjectsCriteria:="Status='Doing'")> _
    'Public Sub MarkAsComplete()
    '    If Not IsDeleted Then
    '        If Not FactoryProcessSeedDetails.Count > 0 Then
    '            MsgBox("ไม่พบรายการปรับปรุงสภาพเมล็ดพันธุ์ กรุณาตรวจสอบข้อมูลอีกครั้ง", MsgBoxStyle.Information, "")
    '            Exit Sub
    '        End If
    '        Try
    '            Me.Status = PublicEnum.FactoryProcessStatus.Done
    '            MyBase.Save()
    '            Session.CommitTransaction()
    '        Catch ex As Exception
    '            Session.RollbackTransaction()
    '        End Try
    '    End If
    'End Sub

    '<Action(Caption:="คืนสถานะ", ConfirmationMessage:="ท่านต้องการบันทึกข้อมูล ใช่หรือไม่?", ImageName:="Action_Deny", AutoCommit:=True, TargetObjectsCriteria:="Status='Done'")> _
    'Public Sub RetoreDate()
    '    If Not IsDeleted Then
    '        If Not FactoryProcessSeedDetails.Count > 0 Then
    '            MsgBox("ไม่พบรายการปรับปรุงสภาพเมล็ดพันธุ์ กรุณาตรวจสอบข้อมูลอีกครั้ง", MsgBoxStyle.Information, "")
    '            Exit Sub
    '        End If
    '        Try
    '            Me.Status = PublicEnum.FactoryProcessStatus.Doing
    '            MyBase.Save()
    '            Session.CommitTransaction()
    '        Catch ex As Exception
    '            Session.RollbackTransaction()
    '        End Try
    '    End If
    'End Sub
End Class
