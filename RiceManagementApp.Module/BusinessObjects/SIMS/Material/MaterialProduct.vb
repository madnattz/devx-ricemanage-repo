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
Public Class MaterialProduct ' Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
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

    Dim fProductCode As String
    <Size(50)> _
    Public Property ProductCode() As String
        Get
            Return fProductCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ProductCode", fProductCode, value)
        End Set
    End Property
    'Dim fProductName As String
    'Public Property ProductName() As String
    '    Get
    '        Return fProductName
    '    End Get
    '    Set(ByVal value As String)
    '        SetPropertyValue(Of String)("ProductName", fProductName, value)
    '    End Set
    'End Property

    '<Browsable(False)> _
    'Public ReadOnly Property ProductNameForReport() As String
    '    Get
    '        Try
    '            Dim ret As String = ""
    '            ret = SeedStatus.SeedStatusName & "/" & _
    '                  SeedType.SeedName & "/" & _
    '                  SeedClass.ClassName & "/" & _
    '                  Season.SeasonName & "/" & _
    '                  SeedYear
    '            Return ret
    '        Catch ex As Exception
    '            Return ""
    '        End Try
    '    End Get
    'End Property
    Dim fMaterial As Material
    Public Property Material() As Material
        Get
            Return fMaterial
        End Get
        Set(ByVal value As Material)
            SetPropertyValue(Of Material)("Material", fMaterial, value)
        End Set
    End Property

    Dim fMaterialYear As String
    <Size(4)> _
    Public Property MaterialYear() As String
        Get
            Return fMaterialYear
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("MaterialYear", fMaterialYear, value)
        End Set
    End Property
    Dim fMoneyType As MoneyType
    Public Property MoneyType() As MoneyType
        Get
            Return fMoneyType
        End Get
        Set(ByVal value As MoneyType)
            SetPropertyValue(Of MoneyType)("MoneyType", fMoneyType, value)
        End Set
    End Property

    Dim fLot As Integer
    <RuleRequiredField(DefaultContexts.Save)> _
    <XafDisplayName("ล็อต")> _
    Public Property Lot() As Integer
        Get
            Return fLot
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Lot", fLot, value)
        End Set
    End Property

    Dim fTotalStockAmount As Double
    Public Property TotalStockAmount() As Double
        Get
            Return fTotalStockAmount
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("TotalStockAmount", fTotalStockAmount, value)
        End Set
    End Property
    Dim fAvailableAmount As Double
    Public Property AvailableAmount() As Double
        Get
            Return fAvailableAmount
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("AvailableAmount", fAvailableAmount, value)
        End Set
    End Property
    Dim fCollectAmount As Double
    Public Property CollectAmount() As Double
        Get
            Return fCollectAmount
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("CollectAmount", fCollectAmount, value)
        End Set
    End Property

    Dim fCost As Double
    Public Property Cost() As Double
        Get
            Return fCost
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("Cost", fCost, value)
        End Set
    End Property

    Enum UpdateStockType
        '//รับ
        income
        '//จ่าย
        outcome
        '//สำรอง
        reserve
        '//คืนของ
        back
    End Enum

    Public Sub UpdateStockAmount(_type As UpdateStockType, _quantity As Double)
        Select Case _type
            Case UpdateStockType.income
                Me.TotalStockAmount += _quantity
                Me.AvailableAmount += _quantity
            Case UpdateStockType.outcome
                Me.TotalStockAmount -= _quantity
                Me.AvailableAmount -= _quantity
            Case UpdateStockType.reserve
                Me.AvailableAmount -= _quantity
            Case UpdateStockType.back
                Me.AvailableAmount += _quantity
        End Select
    End Sub

#Region "Referance Table"
    Private fRefTransaction As XPCollection(Of MaterialTransaction)
    <XafDisplayName("รายการเคลื่อนไหววัสดุการผลิตคงคลัง")> _
    Public ReadOnly Property RefTransaction() As XPCollection(Of MaterialTransaction)
        Get
            Try
                If fRefTransaction Is Nothing Then
                    fRefTransaction = New XPCollection(Of MaterialTransaction)(Session, CriteriaOperator.Parse("MaterialProduct.Oid= ? ", Me.Oid.ToString))
                End If
                Return fRefTransaction
            Catch ex As Exception
                Return Nothing
            End Try

        End Get
    End Property
#End Region

End Class
