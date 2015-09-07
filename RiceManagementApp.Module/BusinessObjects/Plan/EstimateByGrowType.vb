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
Public Class EstimateByGrowType
    Inherits BaseObject
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

    Private fGrowType As GrowType
    <ImmediatePostData> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property GrowType() As GrowType
        Get
            Return fGrowType
        End Get
        Set(ByVal value As GrowType)
            SetPropertyValue("GrowType", fGrowType, value)
        End Set
    End Property

    Dim fSeedUsePerArea As Double
    <ImmediatePostData> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property SeedUsePerArea() As Double
        Get
            Return fSeedUsePerArea
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("SeedUsePerArea", fSeedUsePerArea, value)
        End Set
    End Property

    Dim fQuantityPerArea As Double
    <ImmediatePostData> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property QuantityPerArea() As Double
        Get
            Return fQuantityPerArea
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("QuantityPerArea", fQuantityPerArea, value)
        End Set
    End Property

    Private fSeedPrice As SeedPrice
    <ImmediatePostData> _
    <Association("SeedPrice-EstimateByGrowTypes", GetType(SeedPrice))> _
    Public Property SeedPrice() As SeedPrice
        Get
            Return fSeedPrice
        End Get
        Set(ByVal value As SeedPrice)
            SetPropertyValue("SeedPrice", fSeedPrice, value)
        End Set
    End Property


End Class
