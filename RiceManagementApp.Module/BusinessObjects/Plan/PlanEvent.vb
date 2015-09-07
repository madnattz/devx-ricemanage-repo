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

'<ImageName("BO_Contact")> _
'<DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")> _
'<DefaultListViewOptions(MasterDetailMode.ListViewOnly, False, NewItemRowPosition.None)> _
'<Persistent("DatabaseTableName")> _
<DefaultClassOptions()> _
Public Class PlanEvent ' Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        ' Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).
    End Sub

    Dim fMainPlan As MainPlan
    <Association("PlanEventReferencesMainPlan")> _
    Public Property MainPlan() As MainPlan
        Get
            Return fMainPlan
        End Get
        Set(ByVal value As MainPlan)
            SetPropertyValue(Of MainPlan)("MainPlan", fMainPlan, value)
        End Set
    End Property
    Dim fFarmerGroup As FarmerGroup
    Public Property FarmerGroup() As FarmerGroup
        Get
            Return fFarmerGroup
        End Get
        Set(ByVal value As FarmerGroup)
            SetPropertyValue(Of FarmerGroup)("FarmerGroup", fFarmerGroup, value)
        End Set
    End Property
    Dim fEventPlan As Integer
    Public Property EventPlan() As Integer
        Get
            Return fEventPlan
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("EventPlan", fEventPlan, value)
        End Set
    End Property
    Dim fPlanStartDate As DateTime
    Public Property PlanStartDate() As DateTime
        Get
            Return fPlanStartDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("PlanStartDate", fPlanStartDate, value)
        End Set
    End Property
    Dim fPlanEndDate As DateTime
    Public Property PlanEndDate() As DateTime
        Get
            Return fPlanEndDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("PlanEndDate", fPlanEndDate, value)
        End Set
    End Property
    Dim fResultStartDate As DateTime
    Public Property ResultStartDate() As DateTime
        Get
            Return fResultStartDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ResultStartDate", fResultStartDate, value)
        End Set
    End Property
    Dim fResultEndDate As DateTime
    Public Property ResultEndDate() As DateTime
        Get
            Return fResultEndDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ResultEndDate", fResultEndDate, value)
        End Set
    End Property

End Class
