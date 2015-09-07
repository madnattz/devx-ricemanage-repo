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

<XafDisplayName("รายการรับโอนวัสดุการผลิต")> _
<DefaultClassOptions()> _
Public Class ReceiveTransferMaterialDetail
    Inherits BaseObject
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        DataOwner = GetCurrentSite()
    End Sub

    Dim fReceiveTransferMaterial As ReceiveTransferMaterial
    <Association("ReceiveTransferMaterial-ReceiveTransferMaterialDetails")> _
    Public Property ReceiveTransferMaterial() As ReceiveTransferMaterial
        Get
            Return fReceiveTransferMaterial
        End Get
        Set(ByVal value As ReceiveTransferMaterial)
            SetPropertyValue(Of ReceiveTransferMaterial)("ReceiveTransferMaterial", fReceiveTransferMaterial, value)
        End Set
    End Property

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

    Dim fMaterial As Material
    '<VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    <XafDisplayName("วัสดุการผลิต")> _
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
    <RuleRequiredField(DefaultContexts.Save)> _
    <XafDisplayName("ปี")> _
    <ModelDefault("PropertyEditorType", "RiceManagementApp.Module.Win.CustomYearDropdown")> _
    Public Property MaterialYear() As String
        Get
            Return fMaterialYear
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("MaterialYear", fMaterialYear, value)
        End Set
    End Property
    Dim fMoneyType As MoneyType
    <RuleRequiredField(DefaultContexts.Save)> _
    <XafDisplayName("ประเภทเงิน")> _
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
    <RuleValueComparison(DefaultContexts.Save, ValueComparisonType.GreaterThan, 0)> _
    <XafDisplayName("ล็อต")> _
    Public Property Lot() As Integer
        Get
            Return fLot
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("Lot", fLot, value)
        End Set
    End Property


    Dim fMaterialProduct As MaterialProduct
    <VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    Public Property MaterialProduct() As MaterialProduct
        Get
            Return fMaterialProduct
        End Get
        Set(ByVal value As MaterialProduct)
            SetPropertyValue(Of MaterialProduct)("MaterialProduct", fMaterialProduct, value)
        End Set
    End Property

    Dim fQuantity As Double
    <ImmediatePostData()> _
    <RuleValueComparison(DefaultContexts.Save, ValueComparisonType.GreaterThan, 0)> _
    <XafDisplayName("จำนวน (หน่วย)")> _
    Public Property Quantity() As Double
        Get
            Return fQuantity
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("Quantity", fQuantity, value)
        End Set
    End Property

    Dim fCost As Double
    <XafDisplayName("ราคาต้นทุน (บาท/หน่วย)")> _
    Public Property Cost() As Double
        Get
            Return fCost
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("Cost", fCost, value)
        End Set
    End Property

End Class
