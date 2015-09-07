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

'//µÓºÅ
Public Class District ' Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        ' Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).
    End Sub
    Private _DistrictName As String
    <XafDisplayName("ª×èÍÍÓàÀÍ")> _
    <Index(0), VisibleInListView(True)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property DistrictName() As String
        Get
            Return _DistrictName
        End Get
        Set(ByVal value As String)
            SetPropertyValue("DistrictName", _DistrictName, value)
        End Set
    End Property

    Private _Zipcode As String
    <XafDisplayName("ÃËÑÊä»ÃÉ³ÕÂì")> _
    <Index(0), VisibleInListView(True)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property Zipcode() As String
        Get
            Return _Zipcode
        End Get
        Set(ByVal value As String)
            SetPropertyValue("Zipcode", _Zipcode, value)
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
    Private _Province As Province
    <XafDisplayName("¨Ñ§ËÇÑ´")> _
    <Index(5)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    <Association("Province-Districts", GetType(Province))> _
    Public Property Province() As Province
        Get
            Return _Province
        End Get
        Set(ByVal value As Province)
            SetPropertyValue("Province", _Province, value)
        End Set
    End Property
#End Region

#Region "Association"
    <XafDisplayName("µÓÅº/á¢Ç§")> _
    <Association("District-SubDistricts", GetType(SubDistrict))> _
    <DevExpress.Xpo.Aggregated>
    Public ReadOnly Property SubDistricts() As XPCollection
        Get
            Return GetCollection("SubDistricts")
        End Get
    End Property
#End Region
End Class
