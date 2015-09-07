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
<RuleCriteria("Sale.NotDelete", DefaultContexts.Delete, "Status='Pending'", CustomMessageTemplate:="ไม่สามารถลบข้อมูลนี้ได้")> _
<ConditionalAppearance.Appearance("SaleSeedDisableAllItems", criteria:="Status!='Pending'", Enabled:=False, TargetItems:="*", Context:="DetailView")> _
Public Class Sale
    Inherits BaseObject
    Implements IApproveAble

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        Me.SaleDate = Date.Today
        Me.SaleUser = CType(SecuritySystem.CurrentUser, User).DisplayName
        Me.IsSubmitToCenter = False
        DataOwner = GetCurrentSite()
    End Sub
    Protected Overrides Sub OnSaving()
        Me.fLastUodateDate = Date.Now
        Me.fLastUpdateBy = CType(SecuritySystem.CurrentUser, User).DisplayName
        Me.fSaleUser = CType(SecuritySystem.CurrentUser, User).DisplayName

        If (Me.fSaleNo Is Nothing) Then
            Dim prefix As String = ""
            Dim _year As String = (Date.Now.Year + 543).ToString
            prefix = _year

            Me.fSaleNo = String.Format("{0}/{1:D4}", prefix, DistributedIdGeneratorHelper.Generate(Me.Session.DataLayer, Me.GetType().FullName, prefix))

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

    <Persistent("SaleNo")> _
    Private fSaleNo As String
    <Size(20)> _
    <PersistentAlias("fSaleNo")> _
    Public ReadOnly Property SaleNo() As String
        Get
            Return fSaleNo
        End Get
    End Property

    <Persistent("DOINo")> _
    Private fDOINo As String
    <Size(20)> _
    <XafDisplayName("เลขที่ DOI")> _
    <PersistentAlias("fDOINo")> _
    Public ReadOnly Property DOINo() As String
        Get
            Return fDOINo
        End Get
    End Property

    Dim fReserveSeed As ReserveSeed
    <ImmediatePostData()> _
    Public Property ReserveSeed() As ReserveSeed
        Get
            Return fReserveSeed
        End Get
        Set(ByVal value As ReserveSeed)
            SetPropertyValue(Of ReserveSeed)("ReserveSeed", fReserveSeed, value)
            Try
                If value IsNot Nothing Then
                    fMember = value.Member
                    fProjectName = value.ProjectName
                    fProjectDetail = value.ProjectDetail

                    OnChanged("Member")
                    OnChanged("ProjectName")
                    OnChanged("ProjectDetail")

                    If Not IsLoading AndAlso Not IsSaving Then
                        If value.ReserveSeedDetails.Count > 0 Then
                            Session.Delete(SaleDetails)
                            For Each item As ReserveSeedDetail In value.ReserveSeedDetails
                                Dim objSaleDetail As New SaleDetail(Session)
                                objSaleDetail.Sale = Me
                                objSaleDetail.ReserveSeedDetail = item
                                If item.SeedProduct IsNot Nothing Then
                                    objSaleDetail.SeedProduct = item.SeedProduct
                                End If
                                objSaleDetail.Quantity = item.Quantity
                                objSaleDetail.Bags = item.Bags
                                objSaleDetail.Save()

                            Next
                            Session.CommitTransaction()
                        End If
                    End If
                Else
                    fMember = Nothing
                    fProjectName = ""
                    fProjectDetail = ""

                    OnChanged("Member")
                    OnChanged("ProjectName")
                    OnChanged("ProjectDetail")

                    If Not IsLoading AndAlso Not IsSaving Then
                        For Each item As SaleDetail In SaleDetails
                            If item.ReserveSeedDetail IsNot Nothing Then
                                item.Delete()
                            End If
                        Next
                        Session.CommitTransaction()
                    End If

                End If
            Catch ex As Exception
                Session.RollbackTransaction()
            End Try
        End Set
    End Property
    Dim fMember As Member
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property Member() As Member
        Get
            Return fMember
        End Get
        Set(ByVal value As Member)
            SetPropertyValue(Of Member)("Member", fMember, value)
        End Set
    End Property
    Dim fPaidType As PublicEnum.PaidType
    Public Property PaidType() As PublicEnum.PaidType
        Get
            Return fPaidType
        End Get
        Set(ByVal value As PublicEnum.PaidType)
            SetPropertyValue(Of PublicEnum.PaidType)("PaidType", fPaidType, value)
        End Set
    End Property
    Dim fPaidReferenceNo As String
    <Size(40)> _
    Public Property PaidReferenceNo() As String
        Get
            Return fPaidReferenceNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("PaidReferenceNo", fPaidReferenceNo, value)
        End Set
    End Property
    Dim fSaleUser As String
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property SaleUser() As String
        Get
            Return fSaleUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("SaleUser", fSaleUser, value)
        End Set
    End Property
    Dim fSaleDate As DateTime
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property SaleDate() As DateTime
        Get
            Return fSaleDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("SaleDate", fSaleDate, value)
        End Set
    End Property
    Dim fCentralSaleType As PublicEnum.CentralSaleSeedType
    Public Property CentralSaleType() As PublicEnum.CentralSaleSeedType
        Get
            Return fCentralSaleType
        End Get
        Set(ByVal value As PublicEnum.CentralSaleSeedType)
            SetPropertyValue(Of PublicEnum.CentralSaleSeedType)("CentralSaleType", fCentralSaleType, value)
        End Set
    End Property
    Dim fProjectName As String
    <Size(200)> _
    Public Property ProjectName() As String
        Get
            Return fProjectName
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ProjectName", fProjectName, value)
        End Set
    End Property
    Dim fProjectDetail As String
    <Size(400)> _
    Public Property ProjectDetail() As String
        Get
            Return fProjectDetail
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ProjectDetail", fProjectDetail, value)
        End Set
    End Property
    Dim fIsPaid As Boolean
    Public Property IsPaid() As Boolean
        Get
            Return fIsPaid
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("IsPaid", fIsPaid, value)
        End Set
    End Property
    Dim fStatus As PublicEnum.SimsStatus
    Public Property Status() As PublicEnum.SimsStatus
        Get
            Return fStatus
        End Get
        Set(ByVal value As PublicEnum.SimsStatus)
            SetPropertyValue(Of PublicEnum.SimsStatus)("Status", fStatus, value)
        End Set
    End Property

    <Index(14), VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)> _
    Public ReadOnly Property TotalAmount() As Double
        Get
            Dim total As Double = 0
            For Each item As SaleDetail In SaleDetails
                total += item.SaleAmount
            Next
            Return total
        End Get

    End Property

    <Index(14), VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)> _
    Public ReadOnly Property TotalWithDiscount() As Double
        Get
            Dim total As Double = 0
            For Each item As SaleDetail In SaleDetails
                total += item.TotalAmount
            Next
            Return total
        End Get

    End Property

    '<Index(14), VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)> _
    '<ImmediatePostData()> _
    <Index(14), VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)> _
    Public ReadOnly Property TotalThai() As String
        Get
            Try
                Return ThaiBaht(TotalWithDiscount)
            Catch ex As Exception
                Return ""
            End Try

        End Get
    End Property

    Public Shared Function ThaiBaht(ByVal pAmount As Double) As String
        If pAmount = 0 Then
            Return "ศูนย์บาทถ้วน"
        End If
        Dim _integerValue As String ' จำนวนเต็ม    
        Dim _decimalValue As String ' ทศนิยม     
        Dim _integerTranslatedText As String ' จำนวนเต็ม ภาษาไทย     
        'Dim _integerTranslatedText2 As String
        Dim _decimalTranslatedText As String ' ทศนิยมภาษาไทย    
        _integerValue = Format(pAmount, "####.00") ' จัด Format ค่าเงินเป็นตัวเลข 2 หลัก   
        _decimalValue = Mid(_integerValue, Len(_integerValue) - 1, 2) ' ทศนิยม    
        _integerValue = Mid(_integerValue, 1, Len(_integerValue) - 3) ' จำนวนเต็ม    
        ' แปลง จำนวนเต็ม เป็น ภาษาไทย    
        _integerTranslatedText = NumberToText(CDbl(_integerValue))
        ' แปลง ทศนิยม เป็น ภาษาไทย     
        If CDbl(_decimalValue) <> 0 Then
            _decimalTranslatedText = NumberToText(CDbl(_decimalValue))
        Else
            _decimalTranslatedText = ""
        End If
        ' ถ้าไม่มีทศนิม    
        If _decimalTranslatedText.Trim = "" Then
            _integerTranslatedText += "บาทถ้วน"
        Else
            _integerTranslatedText += "บาท" & _decimalTranslatedText & "สตางค์"
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

    Dim fIsSubmitToCenter As Boolean
    <VisibleInDetailView(False), VisibleInListView(False), VisibleInLookupListView(False)> _
    <XafDisplayName("ส่งข้อมูลเข้าส่วนกลาง")> _
    Public Property IsSubmitToCenter() As Boolean
        Get
            Return fIsSubmitToCenter
        End Get
        Set(ByVal value As Boolean)
            SetPropertyValue(Of Boolean)("IsSubmitToCenter", fIsSubmitToCenter, value)
        End Set
    End Property

    <Association("SaleDetailReferencesSale", GetType(SaleDetail))> _
    Public ReadOnly Property SaleDetails() As XPCollection(Of SaleDetail)
        Get
            Return GetCollection(Of SaleDetail)("SaleDetails")
        End Get
    End Property

    Public Function MarkAsComplete() As Boolean
        If Not IsDeleted Then
            If Not SaleDetails.Count > 0 Then
                MsgBox("ไม่พบรายการเมล็ดพันธุ์ กรุณาตรวจสอบข้อมูลอีกครั้ง", MsgBoxStyle.Information, "")
                Exit Function
            End If

            Dim _session As Session = Me.Session

            Try
                Me.fStatus = PublicEnum.SimsStatus.Approve

                If Me.fReserveSeed IsNot Nothing Then
                    Me.fReserveSeed.Status = PublicEnum.SimsStatus.Closed
                End If

                For i As Integer = 0 To SaleDetails.Count - 1
                    If SaleDetails(i).SeedProduct IsNot Nothing Then


                        If SaleDetails(i).ReserveSeedDetail IsNot Nothing Then
                            '// ตัดยอดจำหน่ายจากการจอง
                            SaleDetails(i).ReserveSeedDetail.SoldQuantity = SaleDetails(i).Quantity
                            SaleDetails(i).ReserveSeedDetail.SoldBags = SaleDetails(i).Bags

                            '// Update ตัดยอดคงคลัง
                            SaleDetails(i).SeedProduct.UpdateStockAmount(SeedProduct.UpdateStockType.SaleFromReserve, SaleDetails(i).Quantity, SaleDetails(i).Bags)

                        Else
                            '// Update ตัดยอดคงคลัง
                            SaleDetails(i).SeedProduct.UpdateStockAmount(SeedProduct.UpdateStockType.Outcome, SaleDetails(i).Quantity, SaleDetails(i).Bags)

                        End If
                        '// รายการเคลื่อนไหวคลัง
                        Dim objTransactionLog As New SeedTransaction(_session, PublicEnum.SeedTransactionType.Sale, fSaleNo, SaleDetails(i).SeedProduct, SaleDetails(i).Quantity, SaleDetails(i).Bags)

                    End If
                Next

                If (Me.fDOINo Is Nothing) Then
                    Dim prefix As String = ""
                    Dim _year As String = (Date.Now.Year + 543).ToString
                    prefix = _year

                    Me.fDOINo = String.Format("{0}/{1:D4}", prefix, DistributedIdGeneratorHelper.Generate(Me.Session.DataLayer, "DOI", prefix))

                End If

                Me.Save()

                Return True
            Catch ex As Exception
                Return False
            End Try
        End If
    End Function

    Public Function MarkAsCancel() As Boolean
        If Not IsDeleted Then
            Dim _session As Session = Me.Session
            Try
                Me.Status = PublicEnum.SimsStatus.Cancel
                Me.IsSubmitToCenter = False
                If Me.fReserveSeed IsNot Nothing Then
                    Me.fReserveSeed.Status = PublicEnum.SimsStatus.Approve
                End If

                For i As Integer = 0 To SaleDetails.Count - 1
                    If SaleDetails(i).SeedProduct IsNot Nothing Then
                        If SaleDetails(i).ReserveSeedDetail IsNot Nothing Then
                            '// คืนยอดจำหน่ายจากการจอง
                            SaleDetails(i).ReserveSeedDetail.SoldQuantity = 0
                            SaleDetails(i).ReserveSeedDetail.SoldBags = 0

                            '//Update เพิ่มยอดคงคลัง
                            SaleDetails(i).SeedProduct.UpdateStockAmount(SeedProduct.UpdateStockType.CancelSaleFromReserve, SaleDetails(i).Quantity, SaleDetails(i).Bags)

                        Else
                            '//Update เพิ่มยอดคงคลัง
                            SaleDetails(i).SeedProduct.UpdateStockAmount(SeedProduct.UpdateStockType.Income, SaleDetails(i).Quantity, SaleDetails(i).Bags)
                            '//รายการเคลื่อนไหวคลัง
                        End If
                        '//รายการเคลื่อนไหวคลัง
                        Dim objTransactionLog As New SeedTransaction(_session, PublicEnum.SeedTransactionType.CancelSale, SaleNo, SaleDetails(i).SeedProduct, SaleDetails(i).Quantity, SaleDetails(i).Bags)
                        Dim objToCancel As SeedTransaction = Session.FindObject(Of SeedTransaction)(CriteriaOperator.Parse("TransactionType='Sale' and RefNo=?", SaleNo))
                        If objToCancel IsNot Nothing Then
                            objToCancel.IsDelete = True
                        End If
                    End If
                Next
                Me.Save()
                Return True
            Catch ex As Exception
                Return False
            End Try
        End If
    End Function

    Public Function DoApprove() As Boolean Implements IApproveAble.DoApprove
        Return MarkAsComplete()
    End Function

    Public Function DoCancel() As Boolean Implements IApproveAble.DoCancel
        Return MarkAsCancel()
    End Function
End Class
