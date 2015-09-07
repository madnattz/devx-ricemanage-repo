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
<XafDisplayName("�����͹���紾ѹ���")> _
<RuleCriteria("TransferSeed.NotDelete", DefaultContexts.Delete, "SendStatus='Pending'", CustomMessageTemplate:="�������öź�����Ź����")> _
<ConditionalAppearance.Appearance("PickSeedDisableAllItems", criteria:="SendStatus!='Pending'", Enabled:=False, TargetItems:="*", Context:="DetailView")> _
Public Class TransferSeed
    Inherits BaseObject
    Implements IApproveAble

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        Me.TransferDate = Date.Today
        Me.TransferType = PublicEnum.TransferType.Send
        Me.SendStatus = PublicEnum.SimsStatus.Pending
        Me.IsSubmitToCenter = False
        DataOwner = GetCurrentSite()

        Dim collSiteSetting As New XPCollection(Of SiteSetting)(Session)
        If collSiteSetting.Count > 0 Then
            Dim objSite As Site = Session.FindObject(Of Site)(CriteriaOperator.Parse("SiteNo=?", collSiteSetting(0).SiteNo))
            If objSite IsNot Nothing Then
                Me.TransferFrom = objSite
            End If
        End If
    End Sub
    Protected Overrides Sub OnSaving()
        If (Me.fTransferNo Is Nothing) Then
            Dim prefix As String = ""
            Dim _year As String = (Date.Now.Year + 543).ToString
            prefix = _year

            Me.fTransferNo = String.Format("{0}/{1:D4}", prefix, DistributedIdGeneratorHelper.Generate(Me.Session.DataLayer, Me.GetType().FullName, prefix))

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

    <Persistent("TransferNo")> _
    Private fTransferNo As String
    <PersistentAlias("fTransferNo")> _
    Public ReadOnly Property TransferNo() As String
        Get
            Return fTransferNo
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

    Dim fTransferType As PublicEnum.TransferType
    Public Property TransferType() As PublicEnum.TransferType
        Get
            Return fTransferType
        End Get
        Set(ByVal value As PublicEnum.TransferType)
            SetPropertyValue(Of PublicEnum.TransferType)("TransferType", fTransferType, value)
        End Set
    End Property
    Dim fTransferFrom As Site
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property TransferFrom() As Site
        Get
            Return fTransferFrom
        End Get
        Set(ByVal value As Site)
            SetPropertyValue(Of Site)("TransferFrom", fTransferFrom, value)
        End Set
    End Property
    Dim fTransferTo As Site
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property TransferTo() As Site
        Get
            Return fTransferTo
        End Get
        Set(ByVal value As Site)
            SetPropertyValue(Of Site)("TransferTo", fTransferTo, value)
        End Set
    End Property
    Dim fTransferReason As PublicEnum.TransferFor
    Public Property TransferReason() As PublicEnum.TransferFor
        Get
            Return fTransferReason
        End Get
        Set(ByVal value As PublicEnum.TransferFor)
            SetPropertyValue(Of PublicEnum.TransferFor)("TransferReason", fTransferReason, value)
        End Set
    End Property
    Dim fTransferDate As DateTime
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property TransferDate() As DateTime
        Get
            Return fTransferDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("TransferDate", fTransferDate, value)
        End Set
    End Property
    'Dim fReceiveDate As DateTime
    'Public Property ReceiveDate() As DateTime
    '    Get
    '        Return fReceiveDate
    '    End Get
    '    Set(ByVal value As DateTime)
    '        SetPropertyValue(Of DateTime)("ReceiveDate", fReceiveDate, value)
    '    End Set
    'End Property
    Dim fTransferUser As String
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property TransferUser() As String
        Get
            Return fTransferUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("TransferUser", fTransferUser, value)
        End Set
    End Property
    Dim fTransferPosition As String
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property TransferPosition() As String
        Get
            Return fTransferPosition
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("TransferPosition", fTransferPosition, value)
        End Set
    End Property
    'Dim fReceiveUser As String
    'Public Property ReceiveUser() As String
    '    Get
    '        Return fReceiveUser
    '    End Get
    '    Set(ByVal value As String)
    '        SetPropertyValue(Of String)("ReceiveUser", fReceiveUser, value)
    '    End Set
    'End Property
    'Dim fReceivePostion As String
    'Public Property ReceivePostion() As String
    '    Get
    '        Return fReceivePostion
    '    End Get
    '    Set(ByVal value As String)
    '        SetPropertyValue(Of String)("ReceivePostion", fReceivePostion, value)
    '    End Set
    'End Property
    Dim fApproveUser As String
    <RuleRequiredField(TargetContextIDs:="Approval")> _
    Public Property ApproveUser() As String
        Get
            Return fApproveUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ApproveUser", fApproveUser, value)
        End Set
    End Property
    Dim fApprovePosition As String
    <RuleRequiredField(TargetContextIDs:="Approval")> _
    Public Property ApprovePosition() As String
        Get
            Return fApprovePosition
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ApprovePosition", fApprovePosition, value)
        End Set
    End Property
    Dim fSendStatus As PublicEnum.SimsStatus
    Public Property SendStatus() As PublicEnum.SimsStatus
        Get
            Return fSendStatus
        End Get
        Set(ByVal value As PublicEnum.SimsStatus)
            SetPropertyValue(Of PublicEnum.SimsStatus)("SendStatus", fSendStatus, value)
        End Set
    End Property

    <Browsable(False)> _
    Public ReadOnly Property Status As PublicEnum.SimsStatus
        Get
            Return SendStatus
        End Get
    End Property
    'Dim fReceiveStatus As PublicEnum.SimsStatus
    'Public Property ReceiveStatus() As PublicEnum.SimsStatus
    '    Get
    '        Return fReceiveStatus
    '    End Get
    '    Set(ByVal value As PublicEnum.SimsStatus)
    '        SetPropertyValue(Of PublicEnum.SimsStatus)("ReceiveStatus", fReceiveStatus, value)
    '    End Set
    'End Property
    Dim fRemark As String
    <Size(200)> _
    Public Property Remark() As String
        Get
            Return fRemark
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Remark", fRemark, value)
        End Set
    End Property
    <Association("TransferSeedDetailReferencesTransferSeed", GetType(TransferSeedDetail))> _
    Public ReadOnly Property TransferSeedDetails() As XPCollection(Of TransferSeedDetail)
        Get
            Return GetCollection(Of TransferSeedDetail)("TransferSeedDetails")
        End Get
    End Property

    <Index(14), VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)> _
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

    <Index(14), VisibleInListView(False), VisibleInDetailView(False), VisibleInLookupListView(False)> _
    Public ReadOnly Property TotalAmount() As Double
        Get
            Dim total As Double = 0
            For Each item In TransferSeedDetails
                total += item.Cost * item.Quantity
            Next
            Return total
        End Get

    End Property

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

    Public Function MarkAsComplete() As Boolean
        If Not IsDeleted Then
            If Not TransferSeedDetails.Count > 0 Then
                MsgBox("��辺��¡�����紾ѹ��� ��سҵ�Ǩ�ͺ�������ա����", MsgBoxStyle.Information, "")
                Exit Function
            End If
            Dim _session As Session = Me.Session
            Try
                Me.fSendStatus = PublicEnum.SimsStatus.Approve
                Me.IsSubmitToCenter = False

                For i As Integer = 0 To TransferSeedDetails.Count - 1
                    If TransferSeedDetails(i).SeedProduct IsNot Nothing Then
                        '// Update �ʹ����ѧ(�ǡ����) 
                        TransferSeedDetails(i).SeedProduct.UpdateStockAmount(SeedProduct.UpdateStockType.Outcome, TransferSeedDetails(i).Quantity, TransferSeedDetails(i).Bags)
                        '// Insert Transaction Data
                        Dim objTransactionLog As New SeedTransaction(_session, PublicEnum.SeedTransactionType.TransferOut, fTransferNo, TransferSeedDetails(i).SeedProduct, TransferSeedDetails(i).Quantity, TransferSeedDetails(i).Bags)
                        '//InsertTransaction(TransferSeedDetails(i), ActionType.Approve)
                    End If
                Next

                If (Me.fDOINo Is Nothing) Then
                    Dim prefix As String = ""
                    Dim _year As String = (Date.Now.Year + 543).ToString
                    prefix = _year

                    Me.fDOINo = String.Format("{0}/{1:D4}", prefix, DistributedIdGeneratorHelper.Generate(Me.Session.DataLayer, "DOI", prefix))

                End If

                MyBase.Save()
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
                Me.fSendStatus = PublicEnum.SimsStatus.Cancel
                'Me.fReceiveStatus = PublicEnum.SimsStatus.Cancel
                MyBase.Save()

                For i As Integer = 0 To TransferSeedDetails.Count - 1
                    If TransferSeedDetails(i).SeedProduct IsNot Nothing Then
                        '// Update �ʹ����ѧ(�ǡ����) 
                        TransferSeedDetails(i).SeedProduct.UpdateStockAmount(SeedProduct.UpdateStockType.Income, TransferSeedDetails(i).Quantity, TransferSeedDetails(i).Bags)
                        '// Insert Transaction Data
                        Dim objTransactionLog As New SeedTransaction(_session, PublicEnum.SeedTransactionType.CancelTransferOut, TransferNo, TransferSeedDetails(i).SeedProduct, TransferSeedDetails(i).Quantity, TransferSeedDetails(i).Bags)
                        Dim objToCancel As SeedTransaction = Session.FindObject(Of SeedTransaction)(CriteriaOperator.Parse("TransactionType='TransferOut' and RefNo=?", TransferNo))
                        If objToCancel IsNot Nothing Then
                            objToCancel.IsDelete = True
                        End If
                    End If
                Next
                Return True
            Catch ex As Exception
                Return False
            End Try
        End If
    End Function

    Enum ActionType
        Approve
        Cancel
    End Enum

    Public Function DoApprove() As Boolean Implements IApproveAble.DoApprove
        Return MarkAsComplete()
    End Function

    Public Function DoCancel() As Boolean Implements IApproveAble.DoCancel
        Return MarkAsCancel()
    End Function
End Class
