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

<RuleCombinationOfPropertiesIsUnique("FarmerGroup-PersonCardID", DefaultContexts.Save, "FarmerGroup,PersonCardID", CustomMessageTemplate:="มีข้อมูลเกษตรกรรายนี้แล้ว ไม่สามารถบันทึกข้อมูลได้")> _
<RuleCombinationOfPropertiesIsUnique("PersonCardID-Status", DefaultContexts.Save, "PersonCardID, Status", CustomMessageTemplate:="มีข้อมูลเกษตรกรรายนี้แล้วในกลุ่มเกษตรกรอื่นแล้ว")> _
<DefaultClassOptions()> _
Public Class Farmer
    Inherits BaseObject
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        Me.Address = New CustomAddress(Session)
        Me.Status = PublicEnum.PublicStatus.Active
        DataOwner = GetCurrentSite()
    End Sub

    Protected Overrides Sub OnLoaded()
        MyBase.OnLoaded()
    End Sub

    Protected Overrides Sub OnSaving()
        Me.LastUodateDate = Date.Now
        Me.LastUpdateBy = CType(SecuritySystem.CurrentUser, User).DisplayName

        If (Me.MemberID Is Nothing) Then
            Dim prefix As String = ""
            Dim _year As String = Date.Now.ToString("yy", New Globalization.CultureInfo("th-TH"))
            Dim _group As String = String.Format("{0:D2}", FarmerGroup.GroupID)
            prefix = _year & "-" & _group

            Me.fMemberID = String.Format("{0}-{1:D3}", prefix, DistributedIdGeneratorHelper.Generate(Me.Session.DataLayer, Me.GetType().FullName, prefix))

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

    'Dim fMemberID As String
    '<Size(50)> _
    'Public Property MemberID() As String
    '    Get
    '        Return fMemberID
    '    End Get
    '    Set(ByVal value As String)
    '        SetPropertyValue(Of String)("MemberID", fMemberID, value)
    '    End Set
    'End Property

    <Persistent("MemberID")> _
    Private fMemberID As String
    <PersistentAlias("fMemberID")> _
    Public ReadOnly Property MemberID() As String
        Get
            Return fMemberID
        End Get
    End Property

    Dim fPersonCardID As String
    '<RuleUniqueValue("Farmer.PersonCardID_Unique", DefaultContexts.Save)> _
    <Size(13)> _
    <RuleRequiredField("Farmer.PersonCardID", DefaultContexts.Save)> _
    Public Property PersonCardID() As String
        Get
            Return fPersonCardID
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PersonCardID", fPersonCardID, value)
        End Set
    End Property

    Public ReadOnly Property FullName As String
        Get
            If PrefixName IsNot Nothing Then
                Return PrefixName.FrefixName & Firstname & "  " & Lastname
            Else
                Return Firstname & "  " & Lastname
            End If

        End Get
    End Property

    Public ReadOnly Property IsLeader As Boolean
        Get
            Dim ret_val As Boolean = False
            Try
                If FarmerGroup IsNot Nothing Then
                    If FarmerGroup.Leader.Oid = Me.Oid Then
                        ret_val = True
                    End If
                End If
            Catch ex As Exception

            End Try

            Return ret_val
        End Get
    End Property

    Public ReadOnly Property IsAssLeader As Boolean
        Get
            Dim ret_val As Boolean = False
            Try
                If FarmerGroup IsNot Nothing Then
                    If FarmerGroup.AssLeader.Oid = Me.Oid Then
                        ret_val = True
                    End If
                End If
            Catch ex As Exception

            End Try

            Return ret_val
        End Get
    End Property

    Dim fPrefixName As PrefixName
    <RuleRequiredField("Farmer.PrefixName", DefaultContexts.Save)> _
    Public Property PrefixName() As PrefixName
        Get
            Return fPrefixName
        End Get
        Set(ByVal value As PrefixName)
            SetPropertyValue(Of PrefixName)("PrefixName", fPrefixName, value)
        End Set
    End Property
    Dim fFirstname As String
    <RuleRequiredField("Farmer.Firstname", DefaultContexts.Save)> _
    Public Property Firstname() As String
        Get
            Return fFirstname
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Firstname", fFirstname, value)
        End Set
    End Property
    Dim fLastname As String
    <RuleRequiredField("Farmer.Lastname", DefaultContexts.Save)> _
    Public Property Lastname() As String
        Get
            Return fLastname
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Lastname", fLastname, value)
        End Set
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
    Dim fBankAcountNo As String
    <Size(20)> _
    <RuleRequiredField("Farmer.BankAcountNo", DefaultContexts.Save)> _
    Public Property BankAcountNo() As String
        Get
            Return fBankAcountNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("BankAcountNo", fBankAcountNo, value)
        End Set
    End Property
    Dim fBankName As String
    <RuleRequiredField("Farmer.BankName", DefaultContexts.Save)> _
    Public Property BankName() As String
        Get
            Return fBankName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("BankName", fBankName, value)
        End Set
    End Property
    Dim fBankBranch As String
    <RuleRequiredField("Farmer.BankBranch", DefaultContexts.Save)> _
    Public Property BankBranch() As String
        Get
            Return fBankBranch
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("BankBranch", fBankBranch, value)
        End Set
    End Property
    Dim fBankProvince As Province
    Public Property BankProvince() As Province
        Get
            Return fBankProvince
        End Get
        Set(ByVal value As Province)
            SetPropertyValue(Of Province)("BankProvince", fBankProvince, value)
        End Set
    End Property

    Dim fTelNo As String
    <Size(20)> _
    Public Property TelNo() As String
        Get
            Return fTelNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("TelNo", fTelNo, value)
        End Set
    End Property

    Dim fStatus As PublicEnum.PublicStatus
    Public Property Status() As PublicEnum.PublicStatus
        Get
            Return fStatus
        End Get
        Set(ByVal value As PublicEnum.PublicStatus)
            SetPropertyValue(Of PublicEnum.PublicStatus)("Status", fStatus, value)
        End Set
    End Property
    Dim fFarmerGroup As FarmerGroup
    <Association("FarmerReferencesFarmerGroup")> _
    <RuleRequiredField("Farmer.FarmerGroup", DefaultContexts.Save)> _
    Public Property FarmerGroup() As FarmerGroup
        Get
            Return fFarmerGroup
        End Get
        Set(ByVal value As FarmerGroup)
            SetPropertyValue(Of FarmerGroup)("FarmerGroup", fFarmerGroup, value)
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

    <XafDisplayName("จำนวนแปลง(แปลง)"), VisibleInDetailView(False), VisibleInListView(True), VisibleInLookupListView(False)> _
    Public ReadOnly Property FarmCount As Integer
        Get
            Return Farms.Count
        End Get
    End Property

    <Association("FarmReferencesFarmer", GetType(Farm))> _
    Public ReadOnly Property Farms() As XPCollection(Of Farm)
        Get
            Return GetCollection(Of Farm)("Farms")
        End Get
    End Property

End Class
