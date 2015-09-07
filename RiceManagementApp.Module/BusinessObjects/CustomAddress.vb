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
Public Class CustomAddress ' Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        DataOwner = GetCurrentSite()
    End Sub

    Protected Overrides Sub OnSaving()
        Me.LastUodateDate = Date.Now
        If SecuritySystem.CurrentUser IsNot Nothing Then
            Me.LastUpdateBy = CType(SecuritySystem.CurrentUser, User).DisplayName
        End If
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

    Dim fAddressNo As String
    <Size(10)> _
    Public Property AddressNo() As String
        Get
            Return fAddressNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AddressNo", fAddressNo, value)
        End Set
    End Property
    Dim fVillage As String
    Public Property Village() As String
        Get
            Return fVillage
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Village", fVillage, value)
        End Set
    End Property
    Dim fMoo As String
    <Size(10)> _
    Public Property Moo() As String
        Get
            Return fMoo
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Moo", fMoo, value)
        End Set
    End Property

    Dim fStreet As String
    Public Property Street() As String
        Get
            Return fStreet
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Street", fStreet, value)
        End Set
    End Property

    Dim fSubStreet As String
    Public Property SubStreet() As String
        Get
            Return fSubStreet
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SubStreet", fSubStreet, value)
        End Set
    End Property

    Dim fSubDistrict As SubDistrict
    <DataSourceProperty("District.SubDistricts")> _
    Public Property SubDistrict() As SubDistrict
        Get
            Return fSubDistrict
        End Get
        Set(ByVal value As SubDistrict)
            SetPropertyValue(Of SubDistrict)("SubDistrict", fSubDistrict, value)
        End Set
    End Property
    Dim fDistrict As District
    <DataSourceProperty("Province.Districts")> _
    <ImmediatePostData()> _
    Public Property District() As District
        Get
            Return fDistrict
        End Get
        Set(ByVal value As District)
            SetPropertyValue(Of District)("District", fDistrict, value)
            Try
                Me.fZipCode = value.Zipcode
                OnChanged("ZipCode")
            Catch ex As Exception

            End Try
           
        End Set
    End Property
    Dim fProvince As Province
    Public Property Province() As Province
        Get
            Return fProvince
        End Get
        Set(ByVal value As Province)
            SetPropertyValue(Of Province)("Province", fProvince, value)
        End Set
    End Property
    Dim fZipCode As String
    <Size(10)> _
    <ImmediatePostData()> _
    Public Property ZipCode() As String
        Get
            Return fZipCode
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ZipCode", fZipCode, value)
        End Set
    End Property

    Dim fTel As String
    Public Property Tel() As String
        Get
            Return fTel
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Tel", fTel, value)
        End Set
    End Property

    Dim fFax As String
    Public Property Fax() As String
        Get
            Return fFax
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Fax", fFax, value)
        End Set
    End Property

    Dim fEmail As String
    Public Property Email() As String
        Get
            Return fEmail
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Email", fEmail, value)
        End Set
    End Property

    Dim fLastUpdateBy As String
    Public Property LastUpdateBy() As String
        Get
            Return fLastUpdateBy
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("LastUpdateBy", fLastUpdateBy, value)
        End Set
    End Property
    Dim fLastUodateDate As DateTime
    Public Property LastUodateDate() As DateTime
        Get
            Return fLastUodateDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("LastUodateDate", fLastUodateDate, value)
        End Set
    End Property
    'Dim fStatus As PublicEnum.PublicStatus
    'Public Property Status() As PublicEnum.PublicStatus
    '    Get
    '        Return fStatus
    '    End Get
    '    Set(ByVal value As PublicEnum.PublicStatus)
    '        SetPropertyValue(Of Integer)("Status", fStatus, value)
    '    End Set
    'End Property

    <VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    Public ReadOnly Property FullAddress As String
        Get
            Dim Full As String = AddressNo & " "

            If Village <> "" And Village <> "-" Then
                Full &= "หมู่บ้าน  " & Village & " "
            End If
            If Moo <> "" And Moo <> "-" Then
                Full &= "หมู่  " & Moo & " "
            End If
            If Street <> "" And Street <> "-" Then
                Full &= "ถนน  " & Street & " "
            End If
            If SubStreet <> "" And SubStreet <> "-" Then
                Full &= "ซอย  " & SubStreet & " "
            End If
            If SubDistrict IsNot Nothing Then
                Full &= "ตำบล  " & SubDistrict.SubDistrictName & " "
            End If
            If District IsNot Nothing Then
                Full &= "อำเภอ  " & District.DistrictName & " "
            End If
            If Province IsNot Nothing Then
                Full &= "จังหวัด  " & Province.ProvinceName & " " & ZipCode
            End If

            Return Full
        End Get
    End Property

    <VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    Public ReadOnly Property FullAddressForReport As String
        Get
            Dim ret As String = ""
            If AddressNo <> "" Then
                ret &= "เลขที่ " & AddressNo
            End If
            If Moo <> "" AndAlso Moo.Replace("-", "") <> "" Then
                ret &= "  ม. " & Moo
            End If
            If Street <> "" AndAlso Street.Replace("-", "") <> "" Then
                ret &= "   ถ. " & Street
            End If
            If SubStreet <> "" AndAlso SubStreet.Replace("-", "") <> "" Then
                ret &= "  ซ. " & SubStreet
            End If
            If SubDistrict IsNot Nothing Then
                ret &= "  ต. " & SubDistrict.SubDistrictName
            End If
            If District IsNot Nothing Then
                ret &= "  อ. " & District.DistrictName
            End If
            If Province IsNot Nothing Then
                ret &= "  จ. " & Province.ProvinceName
            End If
            ret &= "  รหัสไปรษณีย์ " & ZipCode

            Return ret
        End Get
    End Property

End Class
