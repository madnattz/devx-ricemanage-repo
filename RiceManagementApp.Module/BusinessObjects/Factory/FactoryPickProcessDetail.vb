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

<ImageName("BO_Product_Group")> _
<XafDisplayName("Êè§¤×¹àÁÅç´¾Ñ¹¸Øì´Õ")> _
Public Class FactoryPickProcessDetail
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
        If fFactoryPickProcess IsNot Nothing Then
            fFactoryPickProcess.UpdatePickSeedAmount(True)
        End If
        If Me.DataOwner Is Nothing Then
            DataOwner = GetCurrentSite()
        End If
    End Sub

    Protected Overrides Sub OnDeleted()
        MyBase.OnDeleted()
        If fFactoryPickProcess IsNot Nothing Then
            fFactoryPickProcess.UpdatePickSeedAmount(True)
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

    Dim fFactoryPickProcess As FactoryPickProcess
    <VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)> _
    <Association("FactoryPickProcessDetails-FactoryPickProcess")> _
    Public Property FactoryPickProcess() As FactoryPickProcess
        Get
            Return fFactoryPickProcess
        End Get
        Set(ByVal value As FactoryPickProcess)
            Dim oldFactoryPickProcess As FactoryPickProcess = fFactoryPickProcess
            SetPropertyValue(Of FactoryPickProcess)("FactorySeedProcess", fFactoryPickProcess, value)
            If (Not IsLoading) AndAlso (Not IsSaving) AndAlso oldFactoryPickProcess IsNot fFactoryPickProcess Then
                If (oldFactoryPickProcess IsNot Nothing) Then
                    oldFactoryPickProcess = oldFactoryPickProcess
                Else
                    oldFactoryPickProcess = fFactoryPickProcess
                End If
                oldFactoryPickProcess.UpdatePickSeedAmount(True)
            End If
        End Set
    End Property

    Dim fPlanFarmer As PlanFarmer
    <Index(1)> _
    <XafDisplayName("à¡ÉµÃ¡Ã")> _
    <RuleRequiredField(DefaultContexts.Save)> _
    <DataSourceProperty("AvailablePlanFarmer")> _
    Public Property PlanFarmer() As PlanFarmer
        Get
            Return fPlanFarmer
        End Get
        Set(ByVal value As PlanFarmer)
            SetPropertyValue(Of PlanFarmer)("PlanFarmer", fPlanFarmer, value)
        End Set
    End Property

    Private _AvailablePlanFarmer As XPCollection(Of PlanFarmer)
    <Browsable(False)> _
    Public ReadOnly Property AvailablePlanFarmer() As XPCollection(Of PlanFarmer)
        Get
            Try
                Dim buyCriteria As String = "MainPlan.SeedType=? and MainPlan.SeedClass=? and MainPlan.Season=? and MainPlan.SeedYear=? and MainPlan.Lot=? and BuyStatus='Approve'"

                Dim BuyedFarmers As New XPCollection(Of BuySeed)(Session, CriteriaOperator.Parse(buyCriteria, _
                                                                                                Me.FactoryPickProcess.FactorySeedProcess.SeedType, _
                                                                                                Me.FactoryPickProcess.FactorySeedProcess.SeedClass, _
                                                                                                Me.FactoryPickProcess.FactorySeedProcess.Season, _
                                                                                                Me.FactoryPickProcess.FactorySeedProcess.SeedYear, _
                                                                                                Me.FactoryPickProcess.FactorySeedProcess.Lot))

                Dim stringOid As String = ""

                If BuyedFarmers.Count > 0 Then
                    For i As Integer = 0 To BuyedFarmers.Count - 1
                        If i <> BuyedFarmers.Count - 1 Then
                            stringOid &= "'" & BuyedFarmers(i).BuyFarmer.PlanFarmer.Oid.ToString & "',"
                        Else
                            stringOid &= "'" & BuyedFarmers(i).BuyFarmer.PlanFarmer.Oid.ToString & "'"
                        End If
                    Next
                End If

                _AvailablePlanFarmer = New XPCollection(Of PlanFarmer)(Session, CriteriaOperator.Parse("Oid in (" & stringOid & ")"))

            Catch ex As Exception

            End Try
            Return _AvailablePlanFarmer
        End Get
    End Property

    Dim fPickSeedAmount As Integer
    <Index(2)> _
    <XafDisplayName("¹éÓË¹Ñ¡àÁÅç´¾Ñ¹¸Øì´Ôº (¡¡.)")> _
    <RuleValueComparison(DefaultContexts.Save, ValueComparisonType.GreaterThan, 0)> _
    Public Property PickSeedAmount() As Integer
        Get
            Return fPickSeedAmount
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("PickSeedAmount", fPickSeedAmount, value)
            Try
                If (Not IsLoading) AndAlso (Not IsSaving) AndAlso fPickSeedAmount Then
                    FactoryPickProcess.UpdatePickSeedAmount(True)
                End If
            Catch ex As Exception
            End Try
        End Set
    End Property
End Class
