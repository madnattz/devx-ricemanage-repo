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
Public Class FactoryMaterialInventory
    Inherits BaseObject
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
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

    Dim fMaterialProduct As MaterialProduct
    Public Property MaterialProduct() As MaterialProduct
        Get
            Return fMaterialProduct
        End Get
        Set(ByVal value As MaterialProduct)
            SetPropertyValue(Of MaterialProduct)("MaterialProduct", fMaterialProduct, value)
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


#Region "Referance Table"
    Private fRefTransaction As XPCollection(Of FactoryMaterialTransaction)
    <XafDisplayName("รายการเคลื่อนไหววัสดุการผลิต")> _
    Public ReadOnly Property RefTransaction() As XPCollection(Of FactoryMaterialTransaction)
        Get
            Try
                If fRefTransaction Is Nothing Then
                    fRefTransaction = New XPCollection(Of FactoryMaterialTransaction)(Session, CriteriaOperator.Parse("MaterialProduct.Oid=? And FactoryNo=? ", Me.fMaterialProduct.Oid, Me.fFactoryNo))
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
#End Region

    '<Action(Caption:="My UI Action", ConfirmationMessage:="Are you sure?", ImageName:="Attention", AutoCommit:=True)> _
    'Public Sub ActionMethod()
    '    ' Trigger a custom business logic for the current record in the UI (http://documentation.devexpress.com/#Xaf/CustomDocument2619).
    '    Me.PersistentProperty = "Paid"
    'End Sub
End Class
