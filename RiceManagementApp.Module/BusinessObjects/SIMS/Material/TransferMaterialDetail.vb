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
<RuleCriteria("TransferMaterialDetailCheckValue", DefaultContexts.Save, "[Quantity] <= [MaterialProduct.AvailableAmount]", "จำนวนการจ่าย ต้องน้อยกว่าหรือเท่ากับจำนวนคงคลัง")> _
Public Class TransferMaterialDetail ' Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
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

    Dim fTransferMaterial As TransferMaterial
    <Association("TransferMaterialDetailReferencesTransferMaterial")> _
    Public Property TransferMaterial() As TransferMaterial
        Get
            Return fTransferMaterial
        End Get
        Set(ByVal value As TransferMaterial)
            SetPropertyValue(Of TransferMaterial)("TransferMaterial", fTransferMaterial, value)
        End Set
    End Property
    Dim fProductCode As String
    <Size(50)> _
    <ImmediatePostData()> _
    Public Property ProductCode() As String
        Get
            Return fProductCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ProductCode", fProductCode, value)
            If (Not IsLoading) AndAlso (Not IsSaving) Then
                Dim crt As String = "ProductCode=?"
                Dim objProduct As MaterialProduct = Session.FindObject(Of MaterialProduct)(CriteriaOperator.Parse(crt, value))
                If Not objProduct Is Nothing Then
                    Me.fMaterialProduct = objProduct
                    Me.fCost = objProduct.Cost
                    OnChanged("MaterialProduct")
                    OnChanged("Cost")
                End If
            End If
        End Set
    End Property
    Dim fMaterialProduct As MaterialProduct
    <ImmediatePostData()> _
    Public Property MaterialProduct() As MaterialProduct
        Get
            Return fMaterialProduct
        End Get
        Set(ByVal value As MaterialProduct)
            SetPropertyValue(Of MaterialProduct)("MaterialProduct", fMaterialProduct, value)
            If (Not IsLoading) AndAlso (Not IsSaving) Then
                Me.fProductCode = value.ProductCode
                Me.fCost = value.Cost
                OnChanged("ProductCode")
                OnChanged("Cost")
            End If
        End Set
    End Property
    Public ReadOnly Property AvailableProductQuantity As Double
        Get
            If fMaterialProduct IsNot Nothing Then
                Return fMaterialProduct.AvailableAmount
            End If
        End Get
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
    Dim fQuantity As Double
    Public Property Quantity() As Double
        Get
            Return fQuantity
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("Quantity", fQuantity, value)
        End Set
    End Property

    '<Action(Caption:="My UI Action", ConfirmationMessage:="Are you sure?", ImageName:="Attention", AutoCommit:=True)> _
    'Public Sub ActionMethod()
    '    ' Trigger a custom business logic for the current record in the UI (http://documentation.devexpress.com/#Xaf/CustomDocument2619).
    '    Me.PersistentProperty = "Paid"
    'End Sub
End Class
