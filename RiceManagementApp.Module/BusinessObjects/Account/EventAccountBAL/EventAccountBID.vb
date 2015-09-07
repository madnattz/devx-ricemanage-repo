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
Imports System.Threading

'<ImageName("BO_Contact")> _
'<DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")> _
'<DefaultListViewOptions(MasterDetailMode.ListViewOnly, False, NewItemRowPosition.None)> _
'<Persistent("DatabaseTableName")> _
<NavigationItem("รายการยอดเงินคงเหลือประจำวัน")> _
<XafDisplayName("รายการยอดเงินคงเหลือ")> _
<DefaultClassOptions()> _
Public Class EventAccountBID ' Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and บdeletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
    Public Sub New(ByVal session As Session)
        MyBase.New(session)

    End Sub
    Public Overrides Sub AfterConstruction()
        DataOwner = GetCurrentSite()
        CreateType()
        _DocumentNo = Date.Now.ToString("yyMMdd")
        MyBase.AfterConstruction()
        ' Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).
    End Sub


    Public Sub CreateType()
        Dim colAccountBalanceType As XPCollection(Of AccountBalanceType) = New XPCollection(Of AccountBalanceType)(Session)
        colAccountBalanceType.Sorting.Add(New SortProperty("TypeID", DB.SortingDirection.Ascending))
        For Each item As AccountBalanceType In colAccountBalanceType
            Dim objDetail As New AccountBID(Session)
            objDetail.Detail = item.TypeName
            objDetail.EventID = Me
            objDetail.Save()
            Thread.Sleep(10)
        Next
        'Session.CommitTransaction()
        'fRefTransaction = New XPCollection(Of FactoryTransaction)(Session, CriteriaOperator.Parse("SeedProduct.Oid=? And FactoryNo=? ", Me.fSeedProduct.Oid, Me.fFactoryNo))
    End Sub

    Protected Overrides Sub OnSaving()
        MyBase.OnSaving()
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

    Private _DocumentNo As String
    <XafDisplayName("เลขที่เอกสาร")> _
    <Index(2), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    <ImmediatePostData()> _
    Public Property DocumentNo() As String
        Get
            Return _DocumentNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue("DocumentNo", _DocumentNo, value)
        End Set
    End Property

    Private _DocumentDate As Date = Today
    <XafDisplayName("วันที่")> _
    <Index(3), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    <ImmediatePostData()> _
    Public Property DocumentDate() As Date
        Get
            Return _DocumentDate
        End Get
        Set(ByVal value As Date)
            SetPropertyValue("DocumentDate", _DocumentDate, value)
            _DocumentNo = value.ToString("yyMMdd")
        End Set
    End Property

    Private Sub Reset()

        fDebitTotal = Nothing

    End Sub

    <Persistent("TotalAmount")> _
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
        For Each detail As AccountBID In AccountBID
            tempTotal += detail.Amount
        Next detail
        fDebitTotal = tempTotal
        If forceChangeEvents Then
            OnChanged("TotalAmount", oldDebitTotal, fDebitTotal)
        End If
    End Sub

    Private _TotalAmount As Double
    <XafDisplayName("จำนวนเงินรวม (บาท)")> _
    <PersistentAlias("AccountBID.Sum(Amount)")> _
    <ModelDefault("DisplayFormat", "{0:N2}")> _
    <ModelDefault("EditMask", "N")> _
    <Index(5), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    <ImmediatePostData()> _
    Public ReadOnly Property TotalAmount() As Double
        Get
            Try
                Dim tempObject As Object
                tempObject = EvaluateAlias("TotalAmount")
                Return CDbl(tempObject)
            Catch ex As Exception
                Return 0
            End Try
        End Get
    End Property

    <XafDisplayName("จำนวนเงินรวม (ตัวอักษร)")> _
    <Index(14), VisibleInListView(False), VisibleInDetailView(True), VisibleInLookupListView(False)> _
    <ImmediatePostData()> _
    Public ReadOnly Property TotalThai() As String
        Get
            Try
                Return ThaiBaht(TotalAmount)
            Catch ex As Exception
                Return ""
            End Try

        End Get
    End Property

    'Private _TotalAmount As Double
    '<XafDisplayName("จำนวนเงินรวมทั้งสิ้น")> _
    '<Index(2), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    'Public Property TotalAmount() As Double
    '    Get
    '        Return _TotalAmount
    '    End Get
    '    Set(ByVal value As Double)
    '        SetPropertyValue("TotalAmount", _TotalAmount, value)
    '    End Set
    'End Property

    Protected Overrides Sub OnLoaded()
        Reset()
        MyBase.OnLoaded()
        AccountBID.Sorting.Add(New SortProperty("TimeStamp", DB.SortingDirection.Ascending))
    End Sub

    <XafDisplayName("บันทึกรายการยอดเงินคงเหลือ")> _
<Association("EventAccountBID-AccountBID", GetType(AccountBID))> _
<DC.Aggregated()> _
    Public ReadOnly Property AccountBID() As XPCollection(Of AccountBID)
        Get
            Return GetCollection(Of AccountBID)("AccountBID")
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

    '<Action(Caption:="My UI Action", ConfirmationMessage:="Are you sure?", ImageName:="Attention", AutoCommit:=True)> _
    'Public Sub ActionMethod()
    '    ' Trigger a custom business logic for the current record in the UI (http://documentation.devexpress.com/#Xaf/CustomDocument2619).
    '    Me.PersistentProperty = "Paid"
    'End Sub
End Class
