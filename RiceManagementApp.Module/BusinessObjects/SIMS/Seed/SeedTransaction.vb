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

<ImageName("Action_Report_Object_Inplace_Preview")> _
<XafDisplayName("รายการเคลื่อนไหวเมล็ดพันธุ์")> _
<DefaultClassOptions()> _
Public Class SeedTransaction
    Inherits BaseObject
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub

    Public Sub New(ByVal _session As Session, _transactionType As PublicEnum.SeedTransactionType, _refNo As String, _seedProduct As SeedProduct, _quantity As Double, _bags As Integer)
        MyBase.New(_session)
        Me.fTransactionType = _transactionType
        Me.fRefNo = _refNo
        Me.fSeedProduct = _seedProduct

        Select Case _transactionType
            Case PublicEnum.SeedTransactionType.Recieve
                Me.Income = _quantity
                Me.Outcome = 0
                Me.BagsIn = _bags
                Me.BagsOut = 0
                Me.IsDelete = False
            Case PublicEnum.SeedTransactionType.CancelRecieve
                Me.Income = 0
                Me.Outcome = _quantity
                Me.BagsIn = 0
                Me.BagsOut = _bags
                Me.IsDelete = True
            Case PublicEnum.SeedTransactionType.Pick
                Me.Income = 0
                Me.Outcome = _quantity
                Me.BagsIn = 0
                Me.BagsOut = _bags
                Me.IsDelete = False
            Case PublicEnum.SeedTransactionType.CancelPick
                Me.Income = 0
                Me.Outcome = _quantity
                Me.BagsIn = 0
                Me.BagsOut = _bags
                Me.IsDelete = True
            Case PublicEnum.SeedTransactionType.TransferOut
                Me.Income = 0
                Me.Outcome = _quantity
                Me.BagsIn = 0
                Me.BagsOut = _bags
                Me.IsDelete = False
            Case PublicEnum.SeedTransactionType.CancelTransferOut
                Me.Income = _quantity
                Me.Outcome = 0
                Me.BagsIn = _bags
                Me.BagsOut = 0
                Me.IsDelete = True
            Case PublicEnum.SeedTransactionType.TransferIn
                Me.Income = _quantity
                Me.Outcome = 0
                Me.BagsIn = _bags
                Me.BagsOut = 0
                Me.IsDelete = False
            Case PublicEnum.SeedTransactionType.CancelTransferIn
                Me.Income = 0
                Me.Outcome = _quantity
                Me.BagsIn = 0
                Me.BagsOut = _bags
                Me.IsDelete = True
            Case PublicEnum.SeedTransactionType.Sale
                Me.Income = 0
                Me.Outcome = _quantity
                Me.BagsIn = 0
                Me.BagsOut = _bags
                Me.IsDelete = False
            Case PublicEnum.SeedTransactionType.CancelSale
                Me.Income = _quantity
                Me.Outcome = 0
                Me.BagsIn = _bags
                Me.BagsOut = 0
                Me.IsDelete = True
            Case PublicEnum.SeedTransactionType.Reserve
                Me.Income = 0
                Me.Outcome = _quantity
                Me.BagsIn = 0
                Me.BagsOut = _bags
                Me.IsDelete = False
            Case PublicEnum.SeedTransactionType.CancelReserve
                Me.Income = _quantity
                Me.Outcome = 0
                Me.BagsIn = _bags
                Me.BagsOut = 0
                Me.IsDelete = True
        End Select

        Me.TransactionDate = Date.Now
        Me.TransactionBy = CType(SecuritySystem.CurrentUser, User).DisplayName

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

    Dim fTransactionType As PublicEnum.SeedTransactionType
    <Size(50)> _
    <XafDisplayName("ประเภท")> _
    Public Property TransactionType() As PublicEnum.SeedTransactionType
        Get
            Return fTransactionType
        End Get
        Set(ByVal value As PublicEnum.SeedTransactionType)
            SetPropertyValue(Of PublicEnum.SeedTransactionType)("TransactionType", fTransactionType, value)
        End Set
    End Property
    Dim fRefNo As String
    <Size(50)> _
    <XafDisplayName("เลขที่อ้างอิง")> _
    Public Property RefNo() As String
        Get
            Return fRefNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("RefNo", fRefNo, value)
        End Set
    End Property
    Dim fSeedProduct As SeedProduct
    <XafDisplayName("เมล็ดพันธุ์")> _
    Public Property SeedProduct() As SeedProduct
        Get
            Return fSeedProduct
        End Get
        Set(ByVal value As SeedProduct)
            SetPropertyValue(Of SeedProduct)("SeedProduct", fSeedProduct, value)
        End Set
    End Property
    Dim fProductCode As String
    <Size(50)> _
    <XafDisplayName("รหัสเมล็ดพันธุ์")> _
    Public Property ProductCode() As String
        Get
            Return fProductCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ProductCode", fProductCode, value)
        End Set
    End Property
    Dim fProductName As String
    <XafDisplayName("ชื่อเมล็ดพันธุ์")> _
    Public Property ProductName() As String
        Get
            Return fProductName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ProductName", fProductName, value)
        End Set
    End Property
    Dim fIncome As Double
    <XafDisplayName("รับ(กก.)")> _
    Public Property Income() As Double
        Get
            Return fIncome
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("Income", fIncome, value)
        End Set
    End Property
    Dim fOutcome As Double
    <XafDisplayName("จ่าย(กก.)")> _
    Public Property Outcome() As Double
        Get
            Return fOutcome
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("Outcome", fOutcome, value)
        End Set
    End Property

    Dim fBagsIn As Integer
    <XafDisplayName("รับ(กระสอบ)")> _
    Public Property BagsIn() As Integer
        Get
            Return fBagsIn
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("BagsIn", fBagsIn, value)
        End Set
    End Property

    Dim fBagsOut As Integer
    <XafDisplayName("จ่าย(กระสอบ)")> _
    Public Property BagsOut() As Integer
        Get
            Return fBagsOut
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("BagsOut", fBagsOut, value)
        End Set
    End Property

    'Dim fCollectQuantity As Double
    'Public Property CollectQuantity() As Double
    '    Get
    '        Return fCollectQuantity
    '    End Get
    '    Set(ByVal value As Double)
    '        SetPropertyValue(Of Double)("CollectQuantity", fCollectQuantity, value)
    '    End Set
    'End Property
    Dim fTransactionDate As DateTime
    <XafDisplayName("วันที่")> _
    Public Property TransactionDate() As DateTime
        Get
            Return fTransactionDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("TransactionDate", fTransactionDate, value)
        End Set
    End Property
    Dim fTransactionBy As String
    <Size(50)> _
    <XafDisplayName("เคลื่อนไหวโดย")> _
    Public Property TransactionBy() As String
        Get
            Return fTransactionBy
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("TransactionBy", fTransactionBy, value)
        End Set
    End Property
    Dim fIsDelete As Boolean
    <Browsable(False)> _
    Public Property IsDelete() As Boolean
        Get
            Return fIsDelete
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("IsDelete", fIsDelete, value)
        End Set
    End Property

    Enum StockType
        Income
        Outcome
    End Enum

End Class
