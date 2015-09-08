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
Imports DevExpress.Xpo.DB
Imports DevExpress.ExpressApp.ConditionalAppearance

<ImageName("BO_List")> _
<XafDisplayName("ข้อมูล ปริมาณการตรวจสอบข้าวแดง")> _
Public Class SubmitCheckRedSeedDetail
    Inherits BaseObject
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub

    Dim fSubmitCheckRedSeed As SubmitCheckRedSeed
    <Association("SubmitCheckRedSeed-SubmitCheckRedSeedDetails")> _
    Public Property SubmitCheckRedSeed() As SubmitCheckRedSeed
        Get
            Return fSubmitCheckRedSeed
        End Get
        Set(ByVal value As SubmitCheckRedSeed)
            SetPropertyValue(Of SubmitCheckRedSeed)("SubmitCheckRedSeed", fSubmitCheckRedSeed, value)
        End Set
    End Property

    Dim fItemNo As Integer
    <VisibleInDetailView(True), VisibleInListView(False), VisibleInLookupListView(False)> _
    <Index(1)>
    <XafDisplayName("รายการที่")> _
    <Appearance("ItemNo", Enabled:=False)> _
    Public Property ItemNo() As Integer
        Get
            Return fItemNo
        End Get
        Set(ByVal value As Integer)
            SetPropertyValue(Of Integer)("ItemNo", fItemNo, value)
        End Set
    End Property

    Dim fItemCaption As String
    <VisibleInDetailView(True), VisibleInListView(True), VisibleInLookupListView(False)> _
    <Index(2)>
    <XafDisplayName("การตรวจสอบข้าวแดง")> _
    <Appearance("ItemCaption", Enabled:=False)> _
    Public Property ItemCaption() As String
        Get
            Return fItemCaption
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ItemCaption", fItemCaption, value)
        End Set
    End Property

    Dim fMainValue As String
    <VisibleInDetailView(True), VisibleInListView(True), VisibleInLookupListView(False)> _
    <Index(3)>
    <XafDisplayName("เมล็ดพันธุ์หลัก")> _
    <Appearance("MainValue", Enabled:=False)> _
    Public Property MainValue() As String
        Get
            Return fMainValue
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("MainValue", fMainValue, value)
        End Set
    End Property

    Dim fBuyValue As String
    <VisibleInDetailView(True), VisibleInListView(True), VisibleInLookupListView(False)> _
    <Index(4)>
    <XafDisplayName("ตรวจเพื่อการจัดซื้อคืน")> _
    <Appearance("BuyValue", Enabled:=False)> _
    Public Property BuyValue() As String
        Get
            Return fBuyValue
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("BuyValue", fBuyValue, value)
        End Set
    End Property

    Dim fsiteNoField As String
    <VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    Public Property siteNoField() As String
        Get
            Return fsiteNoField
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("siteNoField", fsiteNoField, value)
        End Set
    End Property

    Dim fsiteNameField As String
    <VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    Public Property siteNameField() As String
        Get
            Return fsiteNameField
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("siteNameField", fsiteNameField, value)
        End Set
    End Property


    'Dim fItemNo As Integer
    '<VisibleInDetailView(False), VisibleInListView(True), VisibleInLookupListView(False)> _
    '<Index(1)>
    '<XafDisplayName("รายการที่")> _
    ' Public Property ItemNo() As Integer
    '    Get
    '        Return fItemNo
    '    End Get
    '    Set(ByVal value As Integer)
    '        SetPropertyValue(Of Integer)("ItemNo", fItemNo, value)
    '    End Set
    'End Property

    'Dim fItemCaption As String
    '<VisibleInDetailView(False), VisibleInListView(True), VisibleInLookupListView(False)> _
    '<Index(2)>
    '<XafDisplayName("การตรวจสอบข้าวแดง")> _
    ' Public Property ItemCaption() As String
    '    Get
    '        Return fItemCaption
    '    End Get
    '    Set(ByVal value As String)
    '        SetPropertyValue(Of String)("ItemCaption", fItemCaption, value)
    '    End Set
    'End Property

    'Dim fMainValue As Integer
    '<VisibleInDetailView(False), VisibleInListView(True), VisibleInLookupListView(False)> _
    '<Index(3)>
    '<XafDisplayName("เมล็ดพันธุ์หลัก")> _
    ' Public Property MainValue() As Integer
    '    Get
    '        Return fMainValue
    '    End Get
    '    Set(ByVal value As Integer)
    '        SetPropertyValue(Of Integer)("MainValue", fMainValue, value)
    '    End Set
    'End Property

    'Dim fBuyValue As Integer
    '<VisibleInDetailView(False), VisibleInListView(True), VisibleInLookupListView(False)> _
    '<Index(4)>
    '<XafDisplayName("ตรวจเพื่อการจัดซื้อคืน")> _
    ' Public Property BuyValue() As Integer
    '    Get
    '        Return fBuyValue
    '    End Get
    '    Set(ByVal value As Integer)
    '        SetPropertyValue(Of Integer)("BuyValue", fBuyValue, value)
    '    End Set
    'End Property

    'Dim fsiteNoField As String
    '<VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    'Public Property siteNoField() As String
    '    Get
    '        Return fsiteNoField
    '    End Get
    '    Set(ByVal value As String)
    '        SetPropertyValue(Of String)("siteNoField", fsiteNoField, value)
    '    End Set
    'End Property

    'Dim fsiteNameField As String
    '<VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    'Public Property siteNameField() As String
    '    Get
    '        Return fsiteNameField
    '    End Get
    '    Set(ByVal value As String)
    '        SetPropertyValue(Of String)("siteNameField", fsiteNameField, value)
    '    End Set
    'End Property

    'Dim fseasonNameField As String
    '<VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    'Public Property seasonNameField() As String
    '    Get
    '        Return fseasonNameField
    '    End Get
    '    Set(ByVal value As String)
    '        SetPropertyValue(Of String)("seasonNameField", fseasonNameField, value)
    '    End Set
    'End Property

    'Dim fseedYearField As String
    '<VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    'Public Property seedYearField() As String
    '    Get
    '        Return fseedYearField
    '    End Get
    '    Set(ByVal value As String)
    '        SetPropertyValue(Of String)("seedYearField", fseedYearField, value)
    '    End Set
    'End Property

    'Dim fmainSeedAllField As Integer
    '<VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    'Public Property mainSeedAllField() As Integer
    '    Get
    '        Return fmainSeedAllField
    '    End Get
    '    Set(ByVal value As Integer)
    '        SetPropertyValue(Of Integer)("mainSeedAllField", fmainSeedAllField, value)
    '    End Set
    'End Property

    'Dim fmainSeedNoRedSeedField As Integer
    '<VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    'Public Property mainSeedNoRedSeedField() As Integer
    '    Get
    '        Return fmainSeedNoRedSeedField
    '    End Get
    '    Set(ByVal value As Integer)
    '        SetPropertyValue(Of Integer)("mainSeedNoRedSeedField", fmainSeedNoRedSeedField, value)
    '    End Set
    'End Property

    'Dim fmainSeedRedSeedField As Integer
    '<VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    'Public Property mainSeedRedSeedField() As Integer
    '    Get
    '        Return fmainSeedRedSeedField
    '    End Get
    '    Set(ByVal value As Integer)
    '        SetPropertyValue(Of Integer)("mainSeedRedSeedField", fmainSeedRedSeedField, value)
    '    End Set
    'End Property

    'Dim fmain1Field As Integer
    '<VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    'Public Property main1Field() As Integer
    '    Get
    '        Return fmain1Field
    '    End Get
    '    Set(ByVal value As Integer)
    '        SetPropertyValue(Of Integer)("main1Field", fmain1Field, value)
    '    End Set
    'End Property

    'Dim fmain2Field As Integer
    '<VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    'Public Property main2Field() As Integer
    '    Get
    '        Return fmain2Field
    '    End Get
    '    Set(ByVal value As Integer)
    '        SetPropertyValue(Of Integer)("main2Field", fmain2Field, value)
    '    End Set
    'End Property

    'Dim fmain3Field As Integer
    '<VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    'Public Property main3Field() As Integer
    '    Get
    '        Return fmain3Field
    '    End Get
    '    Set(ByVal value As Integer)
    '        SetPropertyValue(Of Integer)("main3Field", fmain3Field, value)
    '    End Set
    'End Property

    'Dim fmain4Field As Integer
    '<VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    'Public Property main4Field() As Integer
    '    Get
    '        Return fmain4Field
    '    End Get
    '    Set(ByVal value As Integer)
    '        SetPropertyValue(Of Integer)("main4Field", fmain4Field, value)
    '    End Set
    'End Property

    'Dim fmain5Field As Integer
    '<VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    'Public Property main5Field() As Integer
    '    Get
    '        Return fmain5Field
    '    End Get
    '    Set(ByVal value As Integer)
    '        SetPropertyValue(Of Integer)("main5Field", fmain5Field, value)
    '    End Set
    'End Property

    'Dim fmain6_10Field As Integer
    '<VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    'Public Property main6_10Field() As Integer
    '    Get
    '        Return fmain6_10Field
    '    End Get
    '    Set(ByVal value As Integer)
    '        SetPropertyValue(Of Integer)("main6_10Field", fmain6_10Field, value)
    '    End Set
    'End Property

    'Dim fmain11_15Field As Integer
    '<VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    'Public Property main11_15Field() As Integer
    '    Get
    '        Return fmain11_15Field
    '    End Get
    '    Set(ByVal value As Integer)
    '        SetPropertyValue(Of Integer)("main11_15Field", fmain11_15Field, value)
    '    End Set
    'End Property

    'Dim fmain16_20Field As Integer
    '<VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    'Public Property main16_20Field() As Integer
    '    Get
    '        Return fmain16_20Field
    '    End Get
    '    Set(ByVal value As Integer)
    '        SetPropertyValue(Of Integer)("main16_20Field", fmain16_20Field, value)
    '    End Set
    'End Property

    'Dim fmain21_25Field As Integer
    '<VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    'Public Property main21_25Field() As Integer
    '    Get
    '        Return fmain21_25Field
    '    End Get
    '    Set(ByVal value As Integer)
    '        SetPropertyValue(Of Integer)("main21_25Field", fmain21_25Field, value)
    '    End Set
    'End Property

    'Dim fmain26_30Field As Integer
    '<VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    'Public Property main26_30Field() As Integer
    '    Get
    '        Return fmain26_30Field
    '    End Get
    '    Set(ByVal value As Integer)
    '        SetPropertyValue(Of Integer)("main26_30Field", fmain26_30Field, value)
    '    End Set
    'End Property

    'Dim fmain31_40Field As Integer
    '<VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    'Public Property main31_40Field() As Integer
    '    Get
    '        Return fmain31_40Field
    '    End Get
    '    Set(ByVal value As Integer)
    '        SetPropertyValue(Of Integer)("main31_40Field ", fmain31_40Field, value)
    '    End Set
    'End Property

    'Dim fmain41_50Field As Integer
    '<VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    'Public Property main41_50Field() As Integer
    '    Get
    '        Return fmain41_50Field
    '    End Get
    '    Set(ByVal value As Integer)
    '        SetPropertyValue(Of Integer)("main41_50Field", fmain41_50Field, value)
    '    End Set
    'End Property

    'Dim fmain51_100Field As Integer
    '<VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    'Public Property main51_100Field() As Integer
    '    Get
    '        Return fmain51_100Field
    '    End Get
    '    Set(ByVal value As Integer)
    '        SetPropertyValue(Of Integer)("main51_100Field", fmain51_100Field, value)
    '    End Set
    'End Property

    'Dim fmain101_150Field As Integer
    '<VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    'Public Property main101_150Field() As Integer
    '    Get
    '        Return fmain101_150Field
    '    End Get
    '    Set(ByVal value As Integer)
    '        SetPropertyValue(Of Integer)("main101_150Field", fmain101_150Field, value)
    '    End Set
    'End Property

    'Dim fmain151_200Field As Integer
    '<VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    'Public Property main151_200Field() As Integer
    '    Get
    '        Return fmain151_200Field
    '    End Get
    '    Set(ByVal value As Integer)
    '        SetPropertyValue(Of Integer)("main151_200Field", fmain151_200Field, value)
    '    End Set
    'End Property

    'Dim fmainMore200Field As Integer
    '<VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    'Public Property mainMore200Field() As Integer
    '    Get
    '        Return fmainMore200Field
    '    End Get
    '    Set(ByVal value As Integer)
    '        SetPropertyValue(Of Integer)("mainMore200Field", fmainMore200Field, value)
    '    End Set
    'End Property

    'Dim fbuySeedAllField As Integer
    '<VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    'Public Property buySeedAllField() As Integer
    '    Get
    '        Return fbuySeedAllField
    '    End Get
    '    Set(ByVal value As Integer)
    '        SetPropertyValue(Of Integer)("buySeedAllField", fbuySeedAllField, value)
    '    End Set
    'End Property

    'Dim fbuySeedNoRedSeedField As Integer
    '<VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    'Public Property buySeedNoRedSeedField() As Integer
    '    Get
    '        Return fbuySeedNoRedSeedField
    '    End Get
    '    Set(ByVal value As Integer)
    '        SetPropertyValue(Of Integer)("buySeedNoRedSeedField", fbuySeedNoRedSeedField, value)
    '    End Set
    'End Property

    'Dim fbuySeedRedSeedField As Integer
    '<VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    'Public Property buySeedRedSeedField() As Integer
    '    Get
    '        Return fbuySeedRedSeedField
    '    End Get
    '    Set(ByVal value As Integer)
    '        SetPropertyValue(Of Integer)("buySeedRedSeedField", fbuySeedRedSeedField, value)
    '    End Set
    'End Property

    'Dim fbuy1Field As Integer
    '<VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    'Public Property buy1Field() As Integer
    '    Get
    '        Return fbuy1Field
    '    End Get
    '    Set(ByVal value As Integer)
    '        SetPropertyValue(Of Integer)("buy1Field", fbuy1Field, value)
    '    End Set
    'End Property

    'Dim fbuy2Field As Integer
    '<VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    'Public Property buy2Field() As Integer
    '    Get
    '        Return fbuy2Field
    '    End Get
    '    Set(ByVal value As Integer)
    '        SetPropertyValue(Of Integer)("buy2Field", fbuy2Field, value)
    '    End Set
    'End Property

    'Private fbuy3Field As Integer
    '<VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    'Public Property buy3Field() As Integer
    '    Get
    '        Return fbuy3Field
    '    End Get
    '    Set(ByVal value As Integer)
    '        SetPropertyValue(Of Integer)("buy3Field", fbuy3Field, value)
    '    End Set
    'End Property

    'Dim fbuy4Field As Integer
    '<VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    'Public Property buy4Field() As Integer
    '    Get
    '        Return fbuy4Field
    '    End Get
    '    Set(ByVal value As Integer)
    '        SetPropertyValue(Of Integer)("buy4Field", fbuy4Field, value)
    '    End Set
    'End Property

    'Dim fbuy5Field As Integer
    '<VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    'Public Property buy5Field() As Integer
    '    Get
    '        Return fbuy5Field
    '    End Get
    '    Set(ByVal value As Integer)
    '        SetPropertyValue(Of Integer)("buy5Field", fbuy5Field, value)
    '    End Set
    'End Property

    'Dim fbuy6_10Field As Integer
    '<VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    'Public Property buy6_10Field() As Integer
    '    Get
    '        Return fbuy6_10Field
    '    End Get
    '    Set(ByVal value As Integer)
    '        SetPropertyValue(Of Integer)("buy6_10Field", fbuy6_10Field, value)
    '    End Set
    'End Property

    'Dim fbuy11_15Field As Integer
    '<VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    'Public Property buy11_15Field() As Integer
    '    Get
    '        Return fbuy11_15Field
    '    End Get
    '    Set(ByVal value As Integer)
    '        SetPropertyValue(Of Integer)("buy11_15Field", fbuy11_15Field, value)
    '    End Set
    'End Property

    'Dim fbuy16_20Field As Integer
    '<VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    'Public Property buy16_20Field() As Integer
    '    Get
    '        Return fbuy16_20Field
    '    End Get
    '    Set(ByVal value As Integer)
    '        SetPropertyValue(Of Integer)("buy16_20Field", fbuy16_20Field, value)
    '    End Set
    'End Property

    'Dim fbuy21_25Field As Integer
    '<VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    'Public Property buy21_25Field() As Integer
    '    Get
    '        Return fbuy21_25Field
    '    End Get
    '    Set(ByVal value As Integer)
    '        SetPropertyValue(Of Integer)("buy21_25Field", fbuy21_25Field, value)
    '    End Set
    'End Property

    'Dim fbuy26_30Field As Integer
    '<VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    'Public Property buy26_30Field() As Integer
    '    Get
    '        Return fbuy26_30Field
    '    End Get
    '    Set(ByVal value As Integer)
    '        SetPropertyValue(Of Integer)("buy26_30Field", fbuy26_30Field, value)
    '    End Set
    'End Property

    'Dim fbuy31_40Field As Integer
    '<VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    'Public Property buy31_40Field() As Integer
    '    Get
    '        Return fbuy31_40Field
    '    End Get
    '    Set(ByVal value As Integer)
    '        SetPropertyValue(Of Integer)("buy31_40Field", fbuy31_40Field, value)
    '    End Set
    'End Property

    'Dim fbuy41_50Field As Integer
    '<VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    'Public Property buy41_50Field() As Integer
    '    Get
    '        Return fbuy41_50Field
    '    End Get
    '    Set(ByVal value As Integer)
    '        SetPropertyValue(Of Integer)("buy41_50Field", fbuy41_50Field, value)
    '    End Set
    'End Property

    'Dim fbuy51_100Field As Integer
    '<VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    'Public Property buy51_100Field() As Integer
    '    Get
    '        Return fbuy51_100Field
    '    End Get
    '    Set(ByVal value As Integer)
    '        SetPropertyValue(Of Integer)("buy51_100Field", fbuy51_100Field, value)
    '    End Set
    'End Property

    'Dim fbuy101_150Field As Integer
    '<VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    'Public Property buy101_150Field() As Integer
    '    Get
    '        Return fbuy101_150Field
    '    End Get
    '    Set(ByVal value As Integer)
    '        SetPropertyValue(Of Integer)("buy101_150Field", fbuy101_150Field, value)
    '    End Set
    'End Property

    'Dim fbuy151_200Field As Integer
    '<VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    'Public Property buy151_200Field() As Integer
    '    Get
    '        Return fbuy151_200Field
    '    End Get
    '    Set(ByVal value As Integer)
    '        SetPropertyValue(Of Integer)("buy151_200Field", fbuy151_200Field, value)
    '    End Set
    'End Property

    'Dim fbuyMore200Field As Integer
    '<VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    'Public Property buyMore200Field() As Integer
    '    Get
    '        Return fbuyMore200Field
    '    End Get
    '    Set(ByVal value As Integer)
    '        SetPropertyValue(Of Integer)("buyMore200Field", fbuyMore200Field, value)
    '    End Set
    'End Property

    'Dim fsubmitDateField As Date
    '<VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    'Public Property submitDateField() As Date
    '    Get
    '        Return fsubmitDateField
    '    End Get
    '    Set(ByVal value As Date)
    '        SetPropertyValue(Of Date)("submitDateField", fsubmitDateField, value)
    '    End Set
    'End Property

    'Dim fsubmitByField As String
    '<VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    'Public Property submitByField() As String
    '    Get
    '        Return fsubmitByField
    '    End Get
    '    Set(ByVal value As String)
    '        SetPropertyValue(Of String)("submitByField", fsubmitByField, value)
    '    End Set
    'End Property

End Class
