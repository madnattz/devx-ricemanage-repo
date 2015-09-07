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

''' <summary>
''' µÓºÅ
''' </summary>
''' <remarks></remarks>
Public Class SubDistrict ' Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        ' Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).
    End Sub

    Private _SubDistrictName As String
    <XafDisplayName("ª×èÍµÓºÅ")> _
    <Index(0), VisibleInListView(True)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property SubDistrictName() As String
        Get
            Return _SubDistrictName
        End Get
        Set(ByVal value As String)
            SetPropertyValue("SubDistrictName", _SubDistrictName, value)
        End Set
    End Property


    Private _Status As PublicEnum.PublicStatus
    <XafDisplayName("Ê¶Ò¹Ð")> _
    <Index(1), VisibleInListView(True)> _
    Public Property Status() As PublicEnum.PublicStatus
        Get
            Return _Status
        End Get
        Set(ByVal value As PublicEnum.PublicStatus)
            SetPropertyValue("Status", _Status, value)
        End Set
    End Property

#Region "Association"
    Private _District As District
    <XafDisplayName("ÍÓàÀÍ/à¢µ")> _
    <Index(5)> _
    <VisibleInListView(True), VisibleInDetailView(False), VisibleInLookupListView(False)> _
    <Association("District-SubDistricts", GetType(District))> _
    Public Property District() As District
        Get
            Return _District
        End Get
        Set(ByVal value As District)
            SetPropertyValue("District", _District, value)
        End Set
    End Property
#End Region
End Class
