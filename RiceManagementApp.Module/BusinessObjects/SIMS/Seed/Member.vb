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
Imports DevExpress.ExpressApp.ConditionalAppearance

<DefaultClassOptions()> _
Public Class Member ' Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        Me.Address = New CustomAddress(Session)
        DataOwner = GetCurrentSite()
    End Sub

    Protected Overrides Sub OnSaving()
        Me.LastUodateDate = Date.Now
        Me.LastUpdateBy = CType(SecuritySystem.CurrentUser, User).DisplayName

        If (Me.MemberNo Is Nothing) Then
            Dim prefix As String = ""
            Dim _year As String = Date.Now.ToString("yy", New Globalization.CultureInfo("th-TH"))
            prefix = _year

            Me.fMemberNo = String.Format("{0}-{1:D4}", prefix, DistributedIdGeneratorHelper.Generate(Me.Session.DataLayer, Me.GetType().FullName, prefix))

        End If

        MyBase.OnSaving()

        Select Case MemberType
            Case PublicEnum.MemberType.Official
                AgentNo = Nothing
                AgentStartDate = Nothing
                AgentExpireDate = Nothing
                PersonID = Nothing
                FistName = Nothing
                LastName = Nothing
            Case PublicEnum.MemberType.GroupFarmer, PublicEnum.MemberType.MemberFarmer
                AgentNo = Nothing
                AgentStartDate = Nothing
                AgentExpireDate = Nothing
            Case PublicEnum.MemberType.Farmers
                OrgName = Nothing
                AgentNo = Nothing
                AgentStartDate = Nothing
                AgentExpireDate = Nothing
        End Select

        If Me.DataOwner Is Nothing Then
            DataOwner = GetCurrentSite()
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

    <XafDisplayName("ชื่อหน่วยงาน,ร้าน,กลุ่ม,ชื่อ-สกุล")> _
    Public ReadOnly Property MemberLookupName As String
        Get
            Try
                Select Case fMemberType
                    Case PublicEnum.MemberType.Farmers
                        Return fPrefixName.FrefixName & fFistName & "   " & fLastName
                    Case Else
                        Return fOrgName
                End Select
            Catch ex As Exception
                Return ""
            End Try

        End Get
    End Property


    <Persistent("MemberNo")> _
    Private fMemberNo As String
    <XafDisplayName("รหัสลูกค้า"), Index(0)> _
    <Size(20)> _
    Public ReadOnly Property MemberNo() As String
        Get
            Return fMemberNo
        End Get
    End Property

    Dim fMemberType As PublicEnum.MemberType
    <XafDisplayName("ประเภทลูกค้า"), Index(1)> _
    <ImmediatePostData> _
    Public Property MemberType() As PublicEnum.MemberType
        Get
            Return fMemberType
        End Get
        Set(ByVal value As PublicEnum.MemberType)
            SetPropertyValue(Of PublicEnum.MemberType)("MemberType", fMemberType, value)
        End Set
    End Property

    Dim fOrgName As String
    <XafDisplayName("ชื่อหน่วยงาน,ชื่อร้าน,ชื่อกลุ่ม"), Index(2)> _
    <Appearance("Visibility_OrgName", Visibility:=Editors.ViewItemVisibility.Hide, Criteria:="MemberType = 'Farmers'", Context:="DetailView")> _
    <RuleRequiredField(DefaultContexts.Save, TargetCriteria:="MemberType <> 'Farmers'")> _
    Public Property OrgName() As String
        Get
            Return fOrgName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("OrgName", fOrgName, value)
        End Set
    End Property

    Dim fAgentNo As String
    <XafDisplayName("ทะเบียนตัวแทน"), Index(3)> _
    <Size(20)> _
    <Appearance("Visibility_AgentNo", Visibility:=Editors.ViewItemVisibility.Hide, Criteria:="MemberType <> 'Agent'", Context:="DetailView")> _
    <RuleRequiredField(DefaultContexts.Save, TargetCriteria:="MemberType = 'Agent'")> _
    <RuleUniqueValue("AgentNo", DefaultContexts.Save, "ไม่สามารถเพิ่มข้อมูลนี้ได้ เนื่องจากข้อมูลนี้มีอยู่ในระบบแล้ว", TargetCriteria:="MemberType = 'Agent'")> _
    Public Property AgentNo() As String
        Get
            Return fAgentNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("AgentNo", fAgentNo, value)
        End Set
    End Property

    Dim fAgentStartDate As DateTime
    <XafDisplayName("วันที่ได้รับการขึ้นทะเบียน"), Index(4)> _
    <Appearance("Visibility_AgentStartDate", Visibility:=Editors.ViewItemVisibility.Hide, Criteria:="MemberType <> 'Agent'", Context:="DetailView")> _
    <RuleRequiredField(DefaultContexts.Save, TargetCriteria:="MemberType = 'Agent'")> _
    Public Property AgentStartDate() As DateTime
        Get
            Return fAgentStartDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("AgentStartDate", fAgentStartDate, value)
        End Set
    End Property

    Dim fAgentExpireDate As DateTime
    <XafDisplayName("ถึง"), Index(5)> _
    <Appearance("Visibility_AgentExpireDate", Visibility:=Editors.ViewItemVisibility.Hide, Criteria:="MemberType <> 'Agent'", Context:="DetailView")> _
    <RuleRequiredField(DefaultContexts.Save, TargetCriteria:="MemberType = 'Agent'")> _
    Public Property AgentExpireDate() As DateTime
        Get
            Return fAgentExpireDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("AgentExpireDate", fAgentExpireDate, value)
        End Set
    End Property

    Dim fPersonID As String
    <XafDisplayName("เลขที่บัตรประชาชน"), Index(6)> _
    <Size(20)> _
    <Appearance("Visibility_PersonID", Visibility:=Editors.ViewItemVisibility.Hide, Criteria:="MemberType = 'Official'", Context:="DetailView")> _
    Public Property PersonID() As String
        Get
            Return fPersonID
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PersonID", fPersonID, value)
        End Set
    End Property

    Dim fPrefixName As PrefixName
    <XafDisplayName("คำนำหน้า"), Index(7)> _
    <Appearance("Visibility_PrefixName", Visibility:=Editors.ViewItemVisibility.Hide, Criteria:="MemberType = 'Official'", Context:="DetailView")> _
    Public Property PrefixName() As PrefixName
        Get
            Return fPrefixName
        End Get
        Set(ByVal value As PrefixName)
            SetPropertyValue(Of PrefixName)("PrefixName", fPrefixName, value)
        End Set
    End Property

    Dim fFistName As String
    <XafDisplayName("ชื่อ"), Index(8)> _
    <Size(50)> _
    <Appearance("Visibility_FistName", Visibility:=Editors.ViewItemVisibility.Hide, Criteria:="MemberType = 'Official'", Context:="DetailView")> _
    <RuleRequiredField(DefaultContexts.Save, TargetCriteria:="MemberType <> 'Official'")> _
    <ImmediatePostData> _
    Public Property FistName() As String
        Get
            Return fFistName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FistName", fFistName, value)
        End Set
    End Property

    Dim fLastName As String
    <XafDisplayName("นามสกุล"), Index(9)> _
    <Size(50)> _
    <Appearance("Visibility_LastName", Visibility:=Editors.ViewItemVisibility.Hide, Criteria:="MemberType = 'Official'", Context:="DetailView")> _
    <RuleRequiredField(DefaultContexts.Save, TargetCriteria:="MemberType <> 'Official'")> _
    <ImmediatePostData> _
    Public Property LastName() As String
        Get
            Return fLastName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("LastName", fLastName, value)
        End Set
    End Property

    <Appearance("Visibility_Fullname", Visibility:=Editors.ViewItemVisibility.Hide)> _
    <RuleUniqueValue("Fullname", DefaultContexts.Save, "ไม่สามารถเพิ่มข้อมูลนี้ได้ เนื่องจากข้อมูลนี้มีอยู่ในระบบแล้ว", TargetCriteria:="MemberType <> 'Official'")> _
    Public ReadOnly Property Fullname As String
        Get
            Return FistName & " " & LastName
        End Get
    End Property

    Dim fAddress As CustomAddress
    <DC.Aggregated, ExpandObjectMembers(ExpandObjectMembers.Always)> _
    Public Property Address() As CustomAddress
        Get
            Return fAddress
        End Get
        Set(ByVal value As CustomAddress)
            SetPropertyValue(Of CustomAddress)("Address", fAddress, value)
        End Set
    End Property

    Dim fTelNo As String
    <XafDisplayName("โทรศัพท์")> _
    <Size(20)> _
    Public Property TelNo() As String
        Get
            Return fTelNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("TelNo", fTelNo, value)
        End Set
    End Property

    Dim fFaxNo As String
    <XafDisplayName("โทรสาร")> _
    <Size(20)> _
    Public Property FaxNo() As String
        Get
            Return fFaxNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("FaxNo", fFaxNo, value)
        End Set
    End Property

    Dim fEmail As String
    <XafDisplayName("E-mail")> _
    <Size(50)> _
    Public Property Email() As String
        Get
            Return fEmail
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Email", fEmail, value)
        End Set
    End Property

    Dim fStatus As PublicEnum.PublicStatus
    <XafDisplayName("สถานะ")> _
    Public Property Status() As PublicEnum.PublicStatus
        Get
            Return fStatus
        End Get
        Set(ByVal value As PublicEnum.PublicStatus)
            SetPropertyValue(Of PublicEnum.PublicStatus)("Status", fStatus, value)
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
End Class

