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

'<ImageName("BO_Contact")> _
'<DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")> _
'<DefaultListViewOptions(MasterDetailMode.ListViewOnly, False, NewItemRowPosition.None)> _
'<Persistent("DatabaseTableName")> _
'<RuleCriteria("CheckAccountPeriodPV", DefaultContexts.Save, "IsConvertUnit = True", "ยังไม่มีการกำหนดงวด หรือ งวดที่ทำการบันทึกได้ถูกปิดไปแล้ว กรุณาตรวจสอบ")> _
<RuleCriteria("CheckTotalDebitPV", DefaultContexts.Save, "[TotalDebitPV] = [AmountPV]", "จำนวนเงินไม่ตรงกับยอดรวม")> _
<RuleCriteria("CheckTotalCreditPV", DefaultContexts.Save, "[TotalCreditPV] = [AmountPV]", "จำนวนเงินไม่ตรงกับยอดรวม")> _
<NavigationItem("สมุดรายวัน")> _
<XafDisplayName("สมุดรายวันจ่าย")> _
<DefaultClassOptions()> _
Public Class AccountBookPV ' Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        DataOwner = GetCurrentSite()
        MyBase.AfterConstruction()
        _DocumentNoPV = Date.Now.ToString("yyMMdd")
        ' Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).

        AccountBookPV = Session.FindObject(Of AccountBookID)(CriteriaOperator.Parse("AccountBookNo=?", CurrentListView))

        'Dim tmpAccountBook As AccountBookID = Session.FindObject(Of AccountBookID)(New BinaryOperator("AccountBookNo", AccountBookNo))
        'If tmpAccountBook IsNot Nothing Then
        '    AccountBookPV = tmpAccountBook
        'End If
    End Sub
    Public Function CheckAccountPeriod(itemDate As Date) As Boolean
        Dim ret As Boolean = True
        Try

            'WHERE        (dbo.AccountPeriodDetail.StartDate <= '2015-03-24 00:00:00.000') AND (dbo.AccountPeriodDetail.EndDate >= '2015-03-24 00:00:00.000')
            Dim criteria As String = "StartDate <= ? and EndDate >= ? and AccountPeriod.Status = 0"

            'Dim Status As AccountPeriodDetail
            'Status.AccountPeriod.Status = 0
            Dim objAccPeriodDetail As AccountPeriodDetail = Session.FindObject(Of AccountPeriodDetail) _
                                                (CriteriaOperator.Parse(criteria, itemDate, itemDate))
            'WHERE        (dbo.AccountPeriodDetail.StartDate <= '2015-03-24 00:00:00.000') AND (dbo.AccountPeriodDetail.EndDate >= '2015-03-24 00:00:00.000')
            If objAccPeriodDetail Is Nothing Then
                ret = False
            Else
                ret = True
            End If
        Catch ex As Exception
            ret = False
        End Try
        Return ret
    End Function

    Protected Overrides Sub OnSaving()
        'MsgBox("dfd")

        If (Me.fBookRefNo Is Nothing) Then
            Dim prefix As String = "PV" & Date.Now.ToString("yyyyMMdd")

            Me.fBookRefNo = String.Format("{0}{1:D5}", prefix, DistributedIdGeneratorHelper.Generate(Me.Session.DataLayer, Me.GetType().FullName, prefix))
        End If
        MyBase.OnSaving()

        If Me.DataOwner Is Nothing Then
            DataOwner = GetCurrentSite()
        End If

    End Sub

    Protected Overrides Sub OnSaved()
        If Not (IsLoading) AndAlso Not (IsSaving) AndAlso Not (IsDeleted) Then
            PostToGL()
        End If
        MyBase.OnSaved()
    End Sub
    Protected Overrides Sub OnDeleted()
        MyBase.OnDeleted()
        PostToGLDelete()
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

    <VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)> _
    <ImmediatePostData()> _
    <RuleFromBoolProperty("AccountBookPV_IsConvertUnit", DefaultContexts.Save, "ยังไม่มีการกำหนดงวด หรือ งวดที่ทำการบันทึกได้ถูกปิดไปแล้ว กรุณาตรวจสอบ")> _
    Public ReadOnly Property IsConvertUnit() As Boolean
        Get
            Return CheckAccountPeriod(DateListPV)
        End Get


    End Property

    <Persistent("BookRefNo")> _
    Private fBookRefNo As String
    <PersistentAlias("fBookRefNo")> _
    <Index(0), VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)> _
    Public ReadOnly Property BookRefNo() As String
        Get
            Return fBookRefNo
        End Get
    End Property
    '    <Appearance("DateListPV", Criteria:="IsConvertUnit = False", Enabled:=False, Context:="DetailView")> _
    Private _DateListPV As Date = Today
    <XafDisplayName("วันที่")> _
    <Index(0), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    <ImmediatePostData()> _
    Public Property DateListPV() As DateTime
        Get
            Return _DateListPV
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue("DateListPV", _DateListPV, value)
            _DocumentNoPV = value.ToString("yyMMdd")
            OnChanged("DocumentNoPV")
            'If (Not IsLoading) AndAlso (Not IsSaving) Then
            '    If CheckAccountPeriod(_DateListPV) = False Then
            '        MsgBox("ยังไม่มีการกำหนดงวด หรือ งวดที่ทำการบันทึกได้ถูกปิดไปแล้ว กรุณาตรวจสอบ", MsgBoxStyle.Critical)
            '    End If
            'End If
        End Set
    End Property

    Private _AccountBookPV As AccountBookID
    <Appearance("AccountBookPV", Criteria:="IsConvertUnit = False", Enabled:=False, Context:="DetailView")> _
    <XafDisplayName("สมุดรายวัน")> _
    <Appearance("NotEditAccountBookPV", Enabled:=False, Context:="DetailView")> _
    <Index(1), VisibleInListView(False), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    <ImmediatePostData()> _
    Public Property AccountBookPV() As AccountBookID
        Get
            Return _AccountBookPV
        End Get
        Set(ByVal value As AccountBookID)
            SetPropertyValue("AccountBookPV", _AccountBookPV, value)
        End Set
    End Property

    Private _DocumentNoPV As String
    <Appearance("DocumentNoPV", Criteria:="IsConvertUnit = False", Enabled:=False, Context:="DetailView")> _
    <XafDisplayName("เลขที่ใบสำคัญ")> _
    <Index(2), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    <ImmediatePostData()> _
    Public Property DocumentNoPV() As String
        Get
            Return _DocumentNoPV
        End Get
        Set(ByVal value As String)
            SetPropertyValue("DocumentNoPV", _DocumentNoPV, value)
        End Set
    End Property

    Private _ListNoPV As String
    <Appearance("ListNoPV", Criteria:="IsConvertUnit = False", Enabled:=False, Context:="DetailView")> _
    <XafDisplayName("รายการที่")> _
    <Index(3), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    <ImmediatePostData()> _
    Public Property ListNoPV() As String
        Get
            Return _ListNoPV
        End Get
        Set(ByVal value As String)
            SetPropertyValue("ListNoPV", _ListNoPV, value)
        End Set
    End Property

    Private _TxtIDPV As String
    <Appearance("TxtIDPV", Criteria:="IsConvertUnit = False", Enabled:=False, Context:="DetailView")> _
    <XafDisplayName("เช็ค/ใบนำส่ง")> _
    <Index(5), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    <RuleUniqueValue("TxtIDPV", DefaultContexts.Save, "เช็ค/ใบนำส่ง ซ้ำ")> _
    <ImmediatePostData()> _
    Public Property TxtIDPV() As String
        Get
            Return _TxtIDPV
        End Get
        Set(ByVal value As String)
            SetPropertyValue("TxtIDPV", _TxtIDPV, value)
        End Set
    End Property

    Private _ReceiptNoPV As String
    <Appearance("ReceiptNoPV", Criteria:="IsConvertUnit = False", Enabled:=False, Context:="DetailView")> _
    <XafDisplayName("ใบกำกับภาษี/ใบเสร็จรับเงิน")> _
    <Index(6), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    <RuleUniqueValue("ReceiptNoPV", DefaultContexts.Save, "ใบกำกับภาษี/ใบเสร็จรับเงิน ซ้ำ")> _
    <ImmediatePostData()> _
    Public Property ReceiptNoPV() As String
        Get
            Return _ReceiptNoPV
        End Get
        Set(ByVal value As String)
            SetPropertyValue("ReceiptNoPV", _ReceiptNoPV, value)
        End Set
    End Property

    Private _AmountPV As Double
    <Appearance("AmountPV", Criteria:="IsConvertUnit = False", Enabled:=False, Context:="DetailView")> _
    <XafDisplayName("จำนวนเงิน")> _
    <ModelDefault("DisplayFormat", "{0:N2}")> _
    <ModelDefault("EditMask", "N")> _
    <Index(7), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    <RuleValueComparison("AmountPV", DefaultContexts.Save, ValueComparisonType.GreaterThan, 0)> _
    <ImmediatePostData()> _
    Public Property AmountPV() As Double
        Get
            Return _AmountPV
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("AmountPV", _AmountPV, value)
        End Set
    End Property

    Private _ReferenceNoPV As String
    <Appearance("ReferenceNoPV", Criteria:="IsConvertUnit = False", Enabled:=False, Context:="DetailView")> _
    <XafDisplayName("เลขที่ขจ.05")> _
    <Index(8), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    <ImmediatePostData()> _
    Public Property ReferenceNoPV() As String
        Get
            Return _ReferenceNoPV
        End Get
        Set(ByVal value As String)
            SetPropertyValue("ReferenceNoPV", _ReferenceNoPV, value)
        End Set
    End Property

    Private _MoneyPV As Double
    <Appearance("MoneyPV", Criteria:="IsConvertUnit = False", Enabled:=False, Context:="DetailView")> _
    <XafDisplayName("จำนวนเงิน")> _
    <ModelDefault("DisplayFormat", "{0:N2}")> _
    <ModelDefault("EditMask", "N")> _
    <Index(9), VisibleInListView(False), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    <ImmediatePostData()> _
    Public Property MoneyPV() As Double
        Get
            Return _MoneyPV
        End Get
        Set(ByVal value As Double)
            SetPropertyValue("MoneyPV", _MoneyPV, value)
        End Set
    End Property

    Private _DateDatePV As Date
    <Appearance("DateDatePV", Criteria:="IsConvertUnit = False", Enabled:=False, Context:="DetailView")> _
    <XafDisplayName("วันที่ ขจ.05")> _
    <Index(10), VisibleInListView(False), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    <ImmediatePostData()> _
    Public Property DateDatePV() As Date
        Get
            Return _DateDatePV
        End Get
        Set(ByVal value As Date)
            SetPropertyValue("DateDatePV", _DateDatePV, value)
        End Set
    End Property

    Private _DetailPV As String
    <Appearance("DetailPV", Criteria:="IsConvertUnit = False", Enabled:=False, Context:="DetailView")> _
    <XafDisplayName("รายละเอียด")> _
    <Index(11), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    <RuleRequiredField(DefaultContexts.Save)> _
    <Size(100)> _
    <ImmediatePostData()> _
    Public Property DetailPV() As String
        Get
            Return _DetailPV
        End Get
        Set(ByVal value As String)
            SetPropertyValue("DetailPV", _DetailPV, value)
        End Set
    End Property

    Protected Overrides Sub OnLoaded()
        'When using "lazy" calculations it's necessary to reset cached values.
        Reset()
        MyBase.OnLoaded()
        AccountID.Sorting.Add(New SortProperty("TimeStamp", DB.SortingDirection.Ascending))
    End Sub


    Private Sub Reset()

        fDebitTotal = Nothing
        fCreditTotal = Nothing
        fSavedTotal = Nothing

    End Sub

    <Persistent("TotalDebitPV")> _
    Private fDebitTotal As Nullable(Of Double) = Nothing
    <PersistentAlias("fDetailTotal")> _
    <VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)> _
    Public ReadOnly Property DebitTotal() As Nullable(Of Double)
        Get
            If (Not IsLoading) AndAlso (Not IsSaving) AndAlso Not fDebitTotal.HasValue Then
                UpdateDebitTotal(False)
            End If
            Return fDebitTotal
        End Get
    End Property


    Public Sub UpdateDebitTotal(ByVal forceChangeEvents As Boolean)
        'Put your complex business logic here. Just for demo purposes, we calculate a sum here.
        Dim oldDebitTotal As Nullable(Of Double) = fDebitTotal
        Dim tempTotal As Double = 0D
        'Manually iterate through the Orders collection if your calculated property requires a complex business logic which cannot be expressed via criteria language.
        For Each detail As AccountID In AccountID
            tempTotal += detail.Debit
        Next detail
        fDebitTotal = tempTotal
        If forceChangeEvents Then
            OnChanged("TotalDebitPV", oldDebitTotal, fDebitTotal)
        End If
    End Sub

    <Persistent("TotalCreditPV")> _
    Private fCreditTotal As Nullable(Of Double) = Nothing
    <PersistentAlias("fCreditTotal")> _
    <VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)> _
    Public ReadOnly Property CreditTotal() As Nullable(Of Double)
        Get
            If (Not IsLoading) AndAlso (Not IsSaving) AndAlso Not fCreditTotal.HasValue Then
                UpdateCreditTotal(False)
            End If
            Return fCreditTotal
        End Get
    End Property


    Public Sub UpdateCreditTotal(ByVal forceChangeEvents As Boolean)
        'Put your complex business logic here. Just for demo purposes, we calculate a sum here.
        Dim oldCreditTotal As Nullable(Of Double) = fCreditTotal
        Dim tempTotal As Double = 0D
        'Manually iterate through the Orders collection if your calculated property requires a complex business logic which cannot be expressed via criteria language.
        For Each detail As AccountID In AccountID
            tempTotal += detail.Credit
        Next detail
        fCreditTotal = tempTotal
        If forceChangeEvents Then
            OnChanged("TotalCreditPV", oldCreditTotal, fCreditTotal)
        End If
    End Sub

    Private _TotalDebitPV As Double
    <XafDisplayName("ยอดรวม")> _
    <PersistentAlias("AccountID.Sum(Debit)")> _
    <ModelDefault("DisplayFormat", "{0:N2}")> _
    <ModelDefault("EditMask", "N")> _
    <Index(12), VisibleInListView(False), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    <ImmediatePostData()> _
    Public ReadOnly Property TotalDebitPV() As Double
        Get
            Try
                Dim tempObject As Object
                tempObject = EvaluateAlias("TotalDebitPV")
                Return CDbl(tempObject)
            Catch ex As Exception
                Return 0
            End Try
        End Get
    End Property

    Private _TotalCreditPV As Double
    <XafDisplayName("")> _
    <PersistentAlias("AccountID.Sum(Credit)")> _
    <ModelDefault("DisplayFormat", "{0:N2}")> _
    <ModelDefault("EditMask", "N")> _
    <Index(13), VisibleInListView(False), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    <ImmediatePostData()> _
    Public ReadOnly Property TotalCreditPV() As Double
        Get
            Try
                Dim tempObject As Object
                tempObject = EvaluateAlias("TotalCreditPV")
                Return CDbl(tempObject)
            Catch ex As Exception
                Return 0
            End Try
        End Get
    End Property

    <Persistent("TotalOnSaved")> _
    Private fSavedTotal As Nullable(Of Double) = Nothing
    <PersistentAlias("fSavedTotal")> _
    <VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)> _
    Public ReadOnly Property SavedTotal() As Nullable(Of Double)
        Get
            If (Not IsLoading) AndAlso (Not IsSaving) AndAlso Not fSavedTotal.HasValue Then
                UpdateSavedTotal(False)
            End If
            Return fSavedTotal
        End Get
    End Property


    Public Sub UpdateSavedTotal(ByVal forceChangeEvents As Boolean)
        'Put your complex business logic here. Just for demo purposes, we calculate a sum here.
        Dim oldSavedTotal As Nullable(Of Double) = fSavedTotal
        Dim tempTotal As Double = 0D
        'Manually iterate through the Orders collection if your calculated property requires a complex business logic which cannot be expressed via criteria language.
        For Each detail As AccountID In AccountID
            tempTotal += detail.Saved
        Next detail
        fSavedTotal = tempTotal
        If forceChangeEvents Then
            OnChanged("TotalOnSaved", oldSavedTotal, fSavedTotal)
        End If
    End Sub

    Private _TotalOnSaved As Double
    <XafDisplayName("")> _
    <PersistentAlias("AccountID.Sum(Saved)")> _
    <ModelDefault("DisplayFormat", "{0:N2}")> _
    <ModelDefault("EditMask", "N")> _
    <Index(13), VisibleInListView(False), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    <ImmediatePostData()> _
    Public ReadOnly Property TotalOnSaved() As Double
        Get
            Try
                Dim tempObject As Object
                tempObject = EvaluateAlias("TotalOnSaved")
                Return CDbl(tempObject)
            Catch ex As Exception
                Return 0
            End Try
        End Get
        'Set(ByVal value As String)
        '    SetPropertyValue("TotalOnSaved", _TotalOnSaved, value)
        'End Set
    End Property


    'Private _TotalThai As String
    <XafDisplayName("จำนวนเงินรวม(ตัวอักษร)")> _
    <Index(14), VisibleInListView(False), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    <ImmediatePostData()> _
    Public ReadOnly Property TotalThai() As String
        Get
            Try
                Return ThaiBaht(_AmountPV)
            Catch ex As Exception
                Return ""
            End Try

        End Get
    End Property

    'Private _Debit As Double
    '<VisibleInListView(False), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    '<ImmediatePostData()> _
    'Public ReadOnly Property Debit() As Double
    '    Get
    '        Dim tempTotal As Double = 0D
    '        For Each detail As AccountID In AccountID
    '            tempTotal = detail.Debit
    '        Next detail

    '        Return tempTotal
    '    End Get
    'End Property

    'Private _Credit As String
    '<VisibleInListView(False), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    '<ImmediatePostData()> _
    'Public ReadOnly Property Credit() As Double
    '    Get
    '        Dim tempTotal As Double = 0D
    '        For Each detail As AccountID In AccountID
    '            tempTotal = detail.Credit
    '        Next detail

    '        Return tempTotal
    '    End Get
    'End Property

    <XafDisplayName("บัญชี")> _
    <Association("AccountBookPV-AccountID", GetType(AccountID))> _
    <DevExpress.Xpo.Aggregated()> _
    Public ReadOnly Property AccountID() As XPCollection(Of AccountID)
        Get
            Return GetCollection(Of AccountID)("AccountID")
        End Get
    End Property

    Public Shared Function ThaiBaht(ByVal pAmount As Double) As String
        If pAmount = 0 Then
            Return "ศูนย์บาทถ้วน"
        End If
        Dim _integerValue As String ' จำนวนเต็ม    
        Dim _DoubleValue As String ' ทศนิยม     
        Dim _integerTranslatedText As String ' จำนวนเต็ม ภาษาไทย     
        Dim _integerTranslatedText2 As String
        Dim _DoubleTranslatedText As String ' ทศนิยมภาษาไทย    
        _integerValue = Format(pAmount, "####.00") ' จัด Format ค่าเงินเป็นตัวเลข 2 หลัก   
        _DoubleValue = Mid(_integerValue, Len(_integerValue) - 1, 2) ' ทศนิยม    
        _integerValue = Mid(_integerValue, 1, Len(_integerValue) - 3) ' จำนวนเต็ม    
        ' แปลง จำนวนเต็ม เป็น ภาษาไทย    
        _integerTranslatedText = NumberToText(CDbl(_integerValue))
        ' แปลง ทศนิยม เป็น ภาษาไทย     
        If CDbl(_DoubleValue) <> 0 Then
            _DoubleTranslatedText = NumberToText(CDbl(_DoubleValue))
        Else
            _DoubleTranslatedText = ""
        End If
        ' ถ้าไม่มีทศนิม    
        If _DoubleTranslatedText.Trim = "" Then
            _integerTranslatedText += "บาทถ้วน"
        Else
            _integerTranslatedText += "บาท" & _DoubleTranslatedText & "สตางค์"
        End If
        'ใส่เพิ่มเพื่อมีวงเล็บ
        '_integerTranslatedText2 = "(" & _integerTranslatedText & ")"
        Return _integerTranslatedText
    End Function

    Private Shared Function NumberToText(ByVal pAmount As Double) As String
        ' ตัวอักษร   
        Dim _numberText() As String = {"", "หนึ่ง", "สอง", "สาม", "สี่", "ห้า", "หก", "เจ็ด", "แปด", "เก้า", "สิบ"}
        ' หลัก หน่วย สิบ ร้อย พัน ...   
        Dim _digit() As String = {"", "สิบ", "ร้อย", "พัน", "หมื่น", "แสน", "ล้าน"}
        Dim _value As String, _aWord As String, _text As String
        Dim _numberTranslatedText As String = ""
        Dim _length, _digitPosition As Integer
        _value = pAmount.ToString
        _length = Len(_value)
        ' ขนาดของ ข้อมูลที่ต้องการแปลง เช่น 122200 มีขนาด เท่ากับ 6   
        For i As Integer = 0 To _length - 1
            ' วนลูป เริ่มจาก 0 จนถึง (ขนาด - 1)       
            ' ตำแหน่งของ หลัก (digit) ของตัวเลข      
            ' เช่น       ' ตำแหน่งหลักที่0 (หลักหน่วย)      
            ' ตำแหน่งหลักที่1 (หลักสิบ)       
            ' ตำแหน่งหลักที่2 (หลักร้อย)      
            ' ถ้าเป็นข้อมูล i = 7 ตำแหน่งหลักจะเท่ากับ 1 (หลักสิบ)      
            ' ถ้าเป็นข้อมูล i = 9 ตำแหน่งหลักจะเท่ากับ 3 (หลักพัน)       
            ' ถ้าเป็นข้อมูล i = 13 ตำแหน่งหลักจะเท่ากับ 1 (หลักสิบ)      
            _digitPosition = i - (6 * ((i - 1) \ 6))
            _aWord = Mid(_value, Len(_value) - i, 1)
            _text = ""
            Select Case _digitPosition
                Case 0 ' หลักหน่วย               
                    If _aWord = "1" And _length > 1 Then
                        ' ถ้าเป็นเลข 1 และมีขนาดมากกว่า 1 ให้มีค่าเท่ากับ "เอ็ด"                  
                        _text = "เอ็ด"
                    ElseIf _aWord <> "0" Then
                        ' ถ้าไม่ใช่เลข 0 ให้หา ตัวอักษร ใน _numberText()                   
                        _text = _numberText(CInt(_aWord))
                    End If
                Case 1 ' หลักสิบ               
                    If _aWord = "1" Then
                        ' ถ้าเป็นเลข 1 ไม่ต้องมี ตัวอักษร อื่นอีก นอกจากคำว่า "สิบ"                  
                        '_numberTranslatedText = "สิบ" & _numberTranslatedText                  
                        _text = _digit(_digitPosition)
                    ElseIf _aWord = "2" Then
                        ' ถ้าเป็นเลข 2 ให้ตัวอักษรคือ "ยี่สิบ"                  
                        _text = "ยี่" & _digit(_digitPosition)
                    ElseIf _aWord <> "0" Then
                        ' ถ้าไม่ใช่เลข 0 ให้หา ตัวอักษร ใน _numberText() และหาหลัก(digit) ใน _digit()                 
                        _text = _numberText(CInt(_aWord)) & _digit(_digitPosition)
                    End If
                Case 2, 3, 4, 5 ' หลักร้อย ถึง แสน               
                    If _aWord <> "0" Then
                        _text = _numberText(CInt(_aWord)) & _digit(_digitPosition)
                    End If
                Case 6 ' หลักล้าน              
                    If _aWord = "0" Then
                        _text = "ล้าน"
                    ElseIf _aWord = "1" And _length - 1 > i Then
                        _text = "เอ็ดล้าน"
                    Else
                        _text = _numberText(CInt(_aWord)) & _digit(_digitPosition)
                    End If
            End Select
            _numberTranslatedText = _text & _numberTranslatedText
        Next
        Return _numberTranslatedText
    End Function

    '<Action(Caption:="PostGL", ConfirmationMessage:="Are you sure?", ImageName:="Attention", AutoCommit:=True)> _
    Public Sub PostToGL()
        Try
            'If Not AccountID.Count > 0 Then
            '    Exit Sub
            'End If
            Dim colToDelete As New XPCollection(Of GL)(Session, CriteriaOperator.Parse("BookRefNo=?", Me.fBookRefNo))
            If colToDelete.Count > 0 Then
                Session.Delete(colToDelete)
                Session.Save(colToDelete)
            End If
            For i As Integer = 0 To AccountID.Count - 1
                Dim ADDGL As GL = New GL(Session)
                ADDGL.BookRefNo = Me.fBookRefNo
                ADDGL.DocuNo = DocumentNoPV
                ADDGL.DocuDate = DateListPV.AddMinutes(5)
                ADDGL.AccountBook = AccountBookPV.AccountBookNameVGA
                ADDGL.IVRefNo = TxtIDPV
                ADDGL.ReceiptNo = ReceiptNoPV
                ADDGL.RefNo = ReferenceNoPV
                ADDGL.RefDate = DateDatePV
                ADDGL.RefAmnt = AmountPV
                ADDGL.ListNo = ListNoPV
                ADDGL.RVDesc1 = DetailPV
                ADDGL.ToptotalAmnt = MoneyPV

                ADDGL.AccID = AccountID(i).Account.AccountID
                ADDGL.AccountName = AccountID(i).Account.AccountName

                ADDGL.AccDetail = DetailPV
                ADDGL.DrAmnt = AccountID(i).Debit
                ADDGL.CrAmnt = AccountID(i).Credit
                ADDGL.Save()
            Next
            'MyBase.Save()
            Session.CommitTransaction()
        Catch ex As Exception
            Session.RollbackTransaction()
        End Try

    End Sub

    Public Sub PostToGLDelete()
        Try
            'If Not AccountID.Count > 0 Then
            '    Exit Sub
            'End If
            Dim colToDelete As New XPCollection(Of GL)(Session, CriteriaOperator.Parse("BookRefNo=?", Me.fBookRefNo))
            If colToDelete.Count > 0 Then
                Session.Delete(colToDelete)
                Session.Save(colToDelete)
            End If

        Catch ex As Exception
        End Try

    End Sub

    '<Action(Caption:="My UI Action", ConfirmationMessage:="Are you sure?", ImageName:="Attention", AutoCommit:=True)> _
    'Public Sub ActionMethod()
    '    Dim CouterServiceInfo As CouterServiceLists = ObjectSpace.FindObject(Of CouterServiceLists)(CriteriaOperator.Parse("ComputerName =?", System.Net.Dns.GetHostName))

    '    Dim reportData As ReportData = ObjectSpace.FindObject(Of ReportData)(New BinaryOperator("Name", "ใบเล็ก(C)"))
    '    If reportData IsNot Nothing Then
    '        'Dim newObject As ListView = TryCast(DirectCast(sender, BaseObjectSpace).Owner, ListView)
    '        'If Not newObject Is Nothing Then
    '        Dim os As IObjectSpace = Application.CreateObjectSpace()
    '        For Each selectedObject As CounterService_C In e.SelectedObjects
    '            Dim CounterService_CInfo As CounterService_C = ObjectSpace.FindObject(Of CounterService_C)(CriteriaOperator.Parse("Oid == '" & selectedObject.Oid.ToString & "'"))


    '            'Using os As IObjectSpace = Application.CreateObjectSpace()
    '            Dim report As XafReport = reportData.LoadReport(os)
    '            report.Filtering.Filter = "[Oid]=='" & selectedObject.Oid.ToString & "'"
    '            Dim printTool As New ReportPrintToolWpf(report)
    '            printTool.Print()
    '            ' End Using
    '            Dim listViewId As String = Application.FindListViewId(GetType(CounterService_C))
    '            Dim cs As CollectionSourceBase = Application.CreateCollectionSource(os, GetType(CounterService_C), listViewId)
    '            e.ShowViewParameters.CreatedView = Application.CreateListView(listViewId, cs, True)
    '            e.ShowViewParameters.Context = TemplateContext.View
    '            e.ShowViewParameters.TargetWindow = TargetWindow.Current
    '        Next
    '    End If
    'End Sub
End Class
