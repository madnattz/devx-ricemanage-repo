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
Imports DevExpress.ExpressApp.ConditionalAppearance

<ImageName("BO_Product")> _
<DefaultClassOptions()> _
<XafDisplayName("รายการเมล็ดพันธุ์ที่ส่งคืน")> _
Public Class FactoryReturnSeedDetail
    Inherits BaseObject
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        IsReceiveToInventory = False
        DataOwner = GetCurrentSite()
    End Sub

    Protected Overrides Sub OnSaving()
        MyBase.OnSaving()
        If fFactoryReturnSeed IsNot Nothing Then
            fFactoryReturnSeed.UpdateSeedComplete(True)
            fFactoryReturnSeed.UpdateBags(True)
        End If
        If Me.DataOwner Is Nothing Then
            DataOwner = GetCurrentSite()
        End If
    End Sub

    Protected Overrides Sub OnDeleted()
        MyBase.OnDeleted()
        If fFactoryReturnSeed IsNot Nothing Then
            fFactoryReturnSeed.UpdateSeedComplete(True)
            fFactoryReturnSeed.UpdateBags(True)
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

    Dim fFactoryReturnSeed As FactoryReturnSeed
    <VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)> _
    <Association("FactoryReturnSeedDetails-FactoryReturnSeed")> _
    Public Property FactoryReturnSeed() As FactoryReturnSeed
        Get
            Return fFactoryReturnSeed
        End Get
        Set(ByVal value As FactoryReturnSeed)
            Dim oldFactoryReturnSeed As FactoryReturnSeed = fFactoryReturnSeed
            SetPropertyValue(Of FactoryReturnSeed)("FactorySeedProcess", fFactoryReturnSeed, value)
            If (Not IsLoading) AndAlso (Not IsSaving) AndAlso oldFactoryReturnSeed IsNot fFactoryReturnSeed Then
                If (oldFactoryReturnSeed IsNot Nothing) Then
                    oldFactoryReturnSeed = oldFactoryReturnSeed
                Else
                    oldFactoryReturnSeed = fFactoryReturnSeed
                End If
                oldFactoryReturnSeed.UpdateSeedComplete(True)
                oldFactoryReturnSeed.UpdateBags(True)
            End If
        End Set
    End Property

    Dim fPlantFullName As FactoryPickProcess
    <Index(1)>
    <XafDisplayName("เมล็ดพันธุ์ดี")> _
    <ImmediatePostData> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property PlantFullName() As FactoryPickProcess
        Get
            Return fPlantFullName
            fPlantFullName.FactorySeedProcess.SeedType = FactoryReturnSeed.SeedType
        End Get
        Set(ByVal value As FactoryPickProcess)
            SetPropertyValue(Of FactoryPickProcess)("PlantFullName  ", fPlantFullName, value)
            PlantFullName.Status = PublicEnum.FactoryPickProcess.Process
            OnChanged("SeedReturn")
        End Set
    End Property

    Dim fSeedReturn As Integer
    <Appearance("SeedReturn", Enabled:=False)>
    <Index(2)>
    <XafDisplayName("น้ำหนัก (กก.)")> _
    <ImmediatePostData> _
    <RuleValueComparison(DefaultContexts.Save, ValueComparisonType.GreaterThan, 0)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property SeedReturn() As Integer
        Get
            Try
                Return PlantFullName.Quantity
            Catch ex As Exception
                Return 0
            End Try
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("SeedReturn", fSeedReturn, value)
            If (Not IsLoading) AndAlso (Not IsSaving) AndAlso fSeedReturn Then
                FactoryReturnSeed.UpdateSeedComplete(True)
            End If
        End Set
    End Property

    Private fBag As Integer
    <XafDisplayName("กระสอบ (ใบ)")> _
    <ImmediatePostData> _
    <Index(3)>
    Public ReadOnly Property Bag() As Integer
        Get
            fBag = SeedReturn / 25
            Try
                Return fBag
            Catch ex As Exception
                Return 0
            End Try
        End Get
    End Property

    Dim fIsMixChemical As Boolean
    <XafDisplayName("คลุกสารเคมี")> _
    Public Property IsMixChemical() As Boolean
        Get
            Return fIsMixChemical
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("IsMixChemical", fIsMixChemical, value)
        End Set
    End Property

    Dim fIsFinishProcess As Boolean
    <XafDisplayName("เสร็จสิ้นการปรับปรุงสภาพ")> _
    Public Property IsFinishProcess() As Boolean
        Get
            Return fIsFinishProcess
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("IsFinishProcess", fIsFinishProcess, value)
        End Set
    End Property

    Dim fIsReceiveToInventory As Boolean
    <VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    <XafDisplayName("รับเข้าคลังแล้ว")> _
    Public Property IsReceiveToInventory As Boolean
        Get
            Return fIsReceiveToInventory
        End Get
        Set(value As Boolean)
            fIsReceiveToInventory = value
        End Set
    End Property

    Public Sub ChangeIsReceiveToInventory(val As Boolean)
        Me.IsReceiveToInventory = val
        OnChanged("IsReceiveToInventory")
    End Sub

End Class
