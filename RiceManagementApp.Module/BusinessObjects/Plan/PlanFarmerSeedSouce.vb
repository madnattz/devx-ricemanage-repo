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
Public Class PlanFarmerSeedSouce
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
        fTimeStamp = Date.Now

        If fPlanFarmer IsNot Nothing Then
            fPlanFarmer.UpdateTotalReceive(True)
            fPlanFarmer.UpdateTotalUse(True)
        End If

        If Me.DataOwner Is Nothing Then
            DataOwner = GetCurrentSite()
        End If

    End Sub

    Protected Overrides Sub OnDeleted()
        MyBase.OnDeleted()
        If fPlanFarmer IsNot Nothing Then
            fPlanFarmer.UpdateTotalReceive(True)
            fPlanFarmer.UpdateTotalUse(True)
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

    Dim fPlanFarmer As PlanFarmer
    <Association("PlanFarmerSeedSouceReferencesPlanFarmer")> _
    Public Property PlanFarmer() As PlanFarmer
        Get
            Return fPlanFarmer
        End Get
        Set(ByVal value As PlanFarmer)
            Dim oldPlanFarmer As PlanFarmer = fPlanFarmer
            SetPropertyValue(Of PlanFarmer)("PlanFarmer", fPlanFarmer, value)
            If (Not IsLoading) AndAlso (Not IsSaving) AndAlso oldPlanFarmer IsNot fPlanFarmer Then
                If (oldPlanFarmer IsNot Nothing) Then
                    oldPlanFarmer = oldPlanFarmer
                Else
                    oldPlanFarmer = fPlanFarmer
                End If
                oldPlanFarmer.UpdateTotalReceive(True)
                oldPlanFarmer.UpdateTotalUse(True)
            End If
        End Set
    End Property
    Dim fSite As Site
    <DataSourceProperty("AvailableSite")> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property Site() As Site
        Get
            Return fSite
        End Get
        Set(ByVal value As Site)
            SetPropertyValue(Of Site)("Site", fSite, value)
        End Set
    End Property

    Private fAvailableSite As XPCollection(Of Site)
    <Browsable(False)> _
    Public ReadOnly Property AvailableSite() As XPCollection(Of Site)
        Get
            If fPlanFarmer.PlanFarmerGroup.MainPlan.SeedClass.ClassName.Contains("ขยาย") Then
                fAvailableSite = New XPCollection(Of Site)(Session, CriteriaOperator.Parse("SiteType=?", PublicEnum.SiteType.Research))
            ElseIf fPlanFarmer.PlanFarmerGroup.MainPlan.SeedClass.ClassName.Contains("จำหน่าย") Then
                fAvailableSite = New XPCollection(Of Site)(Session, CriteriaOperator.Parse("SiteType=?", PublicEnum.SiteType.Factory))
            End If
            Return fAvailableSite
        End Get
    End Property

    Dim fSeason As Season
    <RuleRequiredField(DefaultContexts.Save)> _
    <XafDisplayName("ฤดู")> _
    Public Property Season() As Season
        Get
            Return fSeason
        End Get
        Set(ByVal value As Season)
            SetPropertyValue(Of Season)("Season", fSeason, value)
        End Set
    End Property
    Dim fSeedYear As String
    <RuleRequiredField(DefaultContexts.Save)> _
    <XafDisplayName("ปี")> _
    Public Property SeedYear() As String
        Get
            Return fSeedYear
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SeedYear", fSeedYear, value)
        End Set
    End Property

    Dim fSeedLot As String
    <RuleRequiredField(DefaultContexts.Save)> _
    <Size(50)> _
    Public Property SeedLot() As String
        Get
            Return fSeedLot
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SeedLot", fSeedLot, value)
        End Set
    End Property
    Dim fSeedReceive As Double
    <ModelDefault("DisPlayFormat", ("{N2}"))> _
    <RuleValueComparison(DefaultContexts.Save, ValueComparisonType.GreaterThan, 0)> _
    Public Property SeedReceive() As Double
        Get
            Return fSeedReceive
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("SeedReceive", fSeedReceive, value)
        End Set
    End Property
    Dim fSeedUse As Double
    <RuleValueComparison(DefaultContexts.Save, ValueComparisonType.GreaterThan, 0)> _
    <ModelDefault("DisPlayFormat", ("{N2}"))> _
    Public Property SeedUse() As Double
        Get
            Return fSeedUse
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("SeedUse", fSeedUse, value)
        End Set
    End Property

    Dim fTimeStamp As DateTime
    <VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    Public Property TimeStamp() As DateTime
        Get
            Return fTimeStamp
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("TimeStamp", fTimeStamp, value)
        End Set
    End Property

End Class
