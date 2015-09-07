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

<XafDisplayName("�����š���������")> _
<DefaultClassOptions()> _
Public Class PlanFarmerHarvest
    Inherits BaseObject
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

    Dim fPlanFarmerGrow As PlanFarmerGrow
    <XafDisplayName("�ɵáü��Ѵ���ŧ")> _
    Public Property PlanFarmerGrow() As PlanFarmerGrow
        Get
            Return fPlanFarmerGrow
        End Get
        Set(ByVal value As PlanFarmerGrow)
            SetPropertyValue(Of PlanFarmerGrow)("PlanFarmerGrow", fPlanFarmerGrow, value)
        End Set
    End Property

    Dim fHarvestDate As DateTime
    <XafDisplayName("�ѹ����������")> _
    Public Property HarvestDate() As DateTime
        Get
            Return fHarvestDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("HarvestDate", fHarvestDate, value)
        End Set
    End Property

    Dim fHarvestArea As Integer
    <XafDisplayName("��鹷��������� (���)")> _
    Public Property HarvestArea() As Integer
        Get
            Return fHarvestArea
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("HarvestArea", fHarvestArea, value)
        End Set
    End Property

End Class
