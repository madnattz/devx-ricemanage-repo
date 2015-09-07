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
Public Class BuyQualitySettings ' Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        DataOwner = GetCurrentSite()
        ' Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).
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

    Dim fPlant As Plant
    Public Property Plant() As Plant
        Get
            Return fPlant
        End Get
        Set(ByVal value As Plant)
            SetPropertyValue(Of Plant)("Plant", fPlant, value)
        End Set
    End Property
    'Dim fSeedType As SeedType
    'Public Property SeedType() As SeedType
    '    Get
    '        Return fSeedType
    '    End Get
    '    Set(ByVal value As SeedType)
    '        SetPropertyValue(Of SeedType)("SeedType", fSeedType, value)
    '    End Set
    'End Property
    'Dim fSeedClass As SeedClass
    'Public Property SeedClass() As SeedClass
    '    Get
    '        Return fSeedClass
    '    End Get
    '    Set(ByVal value As SeedClass)
    '        SetPropertyValue(Of SeedClass)("SeedClass", fSeedClass, value)
    '    End Set
    'End Property
    Dim fWet As Double
    Public Property Wet() As Double
        Get
            Return fWet
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("Wet", fWet, value)
        End Set
    End Property
    Dim fCompound As Double
    Public Property Compound() As Double
        Get
            Return fCompound
        End Get
        Set(ByVal value As Double)
            SetPropertyValue(Of Double)("Compound", fCompound, value)
        End Set
    End Property
End Class
