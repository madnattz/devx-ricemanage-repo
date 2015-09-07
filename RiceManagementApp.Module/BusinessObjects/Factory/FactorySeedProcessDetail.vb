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
<ImageName("BO_List")> _
<XafDisplayName("ข้อมูลเมล็ดพันธุ์ดิบ")> _
<RuleCombinationOfPropertiesIsUnique("FactorySeedProcess,SeedProduct", CustomMessageTemplate:="มีรายการเมล็ดพันธุ์นี้แล้ว กรุณาเลือกเมล็ดพันธุ์ใหม่ หรือแก้ไขรายการที่มีอยู่แล้ว")> _
Public Class FactorySeedProcessDetail
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
        If fFactorySeedProcess IsNot Nothing Then
            fFactorySeedProcess.UpdateSeedAmount(True)
        End If
        If Me.DataOwner Is Nothing Then
            DataOwner = GetCurrentSite()
        End If
    End Sub

    Protected Overrides Sub OnDeleted()
        SeedProduct.Status = PublicEnum.FactorySeedProcess.Pending
        SeedProduct.UpdateDate = Nothing
        MyBase.OnDeleted()

        If fFactorySeedProcess IsNot Nothing Then
            fFactorySeedProcess.UpdateSeedAmount(True)
        End If

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

    Dim fFactorySeedProcess As FactorySeedProcess
    <VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)> _
    <Association("FactorySeedProcessDetails-FactorySeedProcess")> _
    Public Property FactorySeedProcess() As FactorySeedProcess
        Get
            Return fFactorySeedProcess
        End Get
        Set(ByVal value As FactorySeedProcess)
            Dim oldFactorySeedProcess As FactorySeedProcess = fFactorySeedProcess
            SetPropertyValue(Of FactorySeedProcess)("FactorySeedProcess", fFactorySeedProcess, value)
            If (Not IsLoading) AndAlso (Not IsSaving) AndAlso oldFactorySeedProcess IsNot fFactorySeedProcess Then
                If (oldFactorySeedProcess IsNot Nothing) Then
                    oldFactorySeedProcess = oldFactorySeedProcess
                Else
                    oldFactorySeedProcess = fFactorySeedProcess
                End If
                oldFactorySeedProcess.UpdateSeedAmount(True)
            End If
        End Set
    End Property

    Dim fSeedProduct As FactoryInventory
    <XafDisplayName("เมล็ดพันธุ์ดิบ")> _
    <ImmediatePostData> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property SeedProduct() As FactoryInventory
        Get
            Return fSeedProduct
        End Get
        Set(ByVal value As FactoryInventory)
            SetPropertyValue(Of FactoryInventory)("SeedProduct", fSeedProduct, value)
            SeedProduct.Status = PublicEnum.FactorySeedProcess.Process
            SeedProduct.UpdateDate = Today
        End Set
    End Property

    Dim fSeedAmount As Double
    <XafDisplayName("น้ำหนัก (กก.)")> _
    <ImmediatePostData> _
    Public ReadOnly Property SeedAmount() As Double
        Get
            If SeedProduct IsNot Nothing Then
                Return SeedProduct.ReceiveAmount
            End If
        End Get
    End Property

End Class
