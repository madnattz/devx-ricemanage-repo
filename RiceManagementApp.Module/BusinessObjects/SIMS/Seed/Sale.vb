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
<RuleCriteria("Sale.NotDelete", DefaultContexts.Delete, "Status='Pending'", CustomMessageTemplate:="�������öź�����Ź����")> _
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
    <XafDisplayName("�Ţ��� DOI")> _
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
            Return "�ٹ��ҷ��ǹ"
        End If
        Dim _integerValue As String ' �ӹǹ���    
        Dim _decimalValue As String ' �ȹ���     
        Dim _integerTranslatedText As String ' �ӹǹ��� ������     
        'Dim _integerTranslatedText2 As String
        Dim _decimalTranslatedText As String ' �ȹ���������    
        _integerValue = Format(pAmount, "####.00") ' �Ѵ Format ����Թ�繵���Ţ 2 ��ѡ   
        _decimalValue = Mid(_integerValue, Len(_integerValue) - 1, 2) ' �ȹ���    
        _integerValue = Mid(_integerValue, 1, Len(_integerValue) - 3) ' �ӹǹ���    
        ' �ŧ �ӹǹ��� �� ������    
        _integerTranslatedText = NumberToText(CDbl(_integerValue))
        ' �ŧ �ȹ��� �� ������     
        If CDbl(_decimalValue) <> 0 Then
            _decimalTranslatedText = NumberToText(CDbl(_decimalValue))
        Else
            _decimalTranslatedText = ""
        End If
        ' �������շȹ��    
        If _decimalTranslatedText.Trim = "" Then
            _integerTranslatedText += "�ҷ��ǹ"
        Else
            _integerTranslatedText += "�ҷ" & _decimalTranslatedText & "ʵҧ��"
        End If
        '�������������ǧ���
        '_integerTranslatedText2 = "(" & _integerTranslatedText & ")"
        Return _integerTranslatedText
    End Function

    Private Shared Function NumberToText(ByVal pAmount As Double) As String
        ' ����ѡ��   
        Dim _numberText() As String = {"", "˹��", "�ͧ", "���", "���", "���", "ˡ", "��", "Ỵ", "���", "�Ժ"}
        ' ��ѡ ˹��� �Ժ ���� �ѹ ...   
        Dim _digit() As String = {"", "�Ժ", "����", "�ѹ", "����", "�ʹ", "��ҹ"}
        Dim _value As String, _aWord As String, _text As String
        Dim _numberTranslatedText As String = ""
        Dim _length, _digitPosition As Integer
        _value = pAmount.ToString
        _length = Len(_value)
        ' ��Ҵ�ͧ �����ŷ���ͧ����ŧ �� 122200 �բ�Ҵ ��ҡѺ 6   
        For i As Integer = 0 To _length - 1
            ' ǹ�ٻ ������ҡ 0 ���֧ (��Ҵ - 1)       
            ' ���˹觢ͧ ��ѡ (digit) �ͧ����Ţ      
            ' ��       ' ���˹���ѡ���0 (��ѡ˹���)      
            ' ���˹���ѡ���1 (��ѡ�Ժ)       
            ' ���˹���ѡ���2 (��ѡ����)      
            ' ����繢����� i = 7 ���˹���ѡ����ҡѺ 1 (��ѡ�Ժ)      
            ' ����繢����� i = 9 ���˹���ѡ����ҡѺ 3 (��ѡ�ѹ)       
            ' ����繢����� i = 13 ���˹���ѡ����ҡѺ 1 (��ѡ�Ժ)      
            _digitPosition = i - (6 * ((i - 1) \ 6))
            _aWord = Mid(_value, Len(_value) - i, 1)
            _text = ""
            Select Case _digitPosition
                Case 0 ' ��ѡ˹���               
                    If _aWord = "1" And _length > 1 Then
                        ' ������Ţ 1 ����բ�Ҵ�ҡ���� 1 ����դ����ҡѺ "���"                  
                        _text = "���"
                    ElseIf _aWord <> "0" Then
                        ' ���������Ţ 0 ����� ����ѡ�� � _numberText()                   
                        _text = _numberText(CInt(_aWord))
                    End If
                Case 1 ' ��ѡ�Ժ               
                    If _aWord = "1" Then
                        ' ������Ţ 1 ����ͧ�� ����ѡ�� ����ա �͡�ҡ����� "�Ժ"                  
                        '_numberTranslatedText = "�Ժ" & _numberTranslatedText                  
                        _text = _digit(_digitPosition)
                    ElseIf _aWord = "2" Then
                        ' ������Ţ 2 ������ѡ�ä�� "����Ժ"                  
                        _text = "���" & _digit(_digitPosition)
                    ElseIf _aWord <> "0" Then
                        ' ���������Ţ 0 ����� ����ѡ�� � _numberText() �������ѡ(digit) � _digit()                 
                        _text = _numberText(CInt(_aWord)) & _digit(_digitPosition)
                    End If
                Case 2, 3, 4, 5 ' ��ѡ���� �֧ �ʹ               
                    If _aWord <> "0" Then
                        _text = _numberText(CInt(_aWord)) & _digit(_digitPosition)
                    End If
                Case 6 ' ��ѡ��ҹ              
                    If _aWord = "0" Then
                        _text = "��ҹ"
                    ElseIf _aWord = "1" And _length - 1 > i Then
                        _text = "�����ҹ"
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
    <XafDisplayName("�觢����������ǹ��ҧ")> _
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
                MsgBox("��辺��¡�����紾ѹ��� ��سҵ�Ǩ�ͺ�������ա����", MsgBoxStyle.Information, "")
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
                            '// �Ѵ�ʹ��˹��¨ҡ��èͧ
                            SaleDetails(i).ReserveSeedDetail.SoldQuantity = SaleDetails(i).Quantity
                            SaleDetails(i).ReserveSeedDetail.SoldBags = SaleDetails(i).Bags

                            '// Update �Ѵ�ʹ����ѧ
                            SaleDetails(i).SeedProduct.UpdateStockAmount(SeedProduct.UpdateStockType.SaleFromReserve, SaleDetails(i).Quantity, SaleDetails(i).Bags)

                        Else
                            '// Update �Ѵ�ʹ����ѧ
                            SaleDetails(i).SeedProduct.UpdateStockAmount(SeedProduct.UpdateStockType.Outcome, SaleDetails(i).Quantity, SaleDetails(i).Bags)

                        End If
                        '// ��¡������͹��Ǥ�ѧ
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
                            '// �׹�ʹ��˹��¨ҡ��èͧ
                            SaleDetails(i).ReserveSeedDetail.SoldQuantity = 0
                            SaleDetails(i).ReserveSeedDetail.SoldBags = 0

                            '//Update �����ʹ����ѧ
                            SaleDetails(i).SeedProduct.UpdateStockAmount(SeedProduct.UpdateStockType.CancelSaleFromReserve, SaleDetails(i).Quantity, SaleDetails(i).Bags)

                        Else
                            '//Update �����ʹ����ѧ
                            SaleDetails(i).SeedProduct.UpdateStockAmount(SeedProduct.UpdateStockType.Income, SaleDetails(i).Quantity, SaleDetails(i).Bags)
                            '//��¡������͹��Ǥ�ѧ
                        End If
                        '//��¡������͹��Ǥ�ѧ
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
