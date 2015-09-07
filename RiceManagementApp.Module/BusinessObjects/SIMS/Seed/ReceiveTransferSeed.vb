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
<XafDisplayName("�Ѻ�͹���紾ѹ���")> _
<RuleCriteria("ReceiveTransferSeed.NotDelete", DefaultContexts.Delete, "ReceiveStatus='Pending'", CustomMessageTemplate:="�������öź�����Ź����")> _
<ConditionalAppearance.Appearance("ReceiveTransferSeedDisableAllItems", criteria:="ReceiveStatus!='Pending'", Enabled:=False, TargetItems:="*", Context:="DetailView")> _
Public Class ReceiveTransferSeed
    Inherits BaseObject
    Implements IApproveAble

    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        'Me.TransferDate = Date.Today
        Me.ReceiveDate = Date.Today
        Me.TransferType = PublicEnum.TransferType.Recieve
        Me.ReceiveStatus = PublicEnum.SimsStatus.Pending
        Me.IsSubmitToCenter = False
        DataOwner = GetCurrentSite()

        Dim collSiteSetting As New XPCollection(Of SiteSetting)(Session)
        If collSiteSetting.Count > 0 Then
            Dim objSite As Site = Session.FindObject(Of Site)(CriteriaOperator.Parse("SiteNo=?", collSiteSetting(0).SiteNo))
            If objSite IsNot Nothing Then
                Me.TransferTo = objSite
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
    <XafDisplayName("�Ţ����Ѻ�͹")> _
    Public ReadOnly Property TransferNo() As String
        Get
            Return fTransferNo
        End Get
    End Property

    Dim fSendNo As String
    <XafDisplayName("�Ţ�������͹")> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property SendNo() As String
        Get
            Return fSendNo
        End Get
        Set(ByVal value As String)
            SetPropertyValue("fSendNo", fSendNo, value)
        End Set
    End Property

    Dim fTransferType As PublicEnum.TransferType
    <XafDisplayName("������")> _
    Public Property TransferType() As PublicEnum.TransferType
        Get
            Return fTransferType
        End Get
        Set(ByVal value As PublicEnum.TransferType)
            SetPropertyValue(Of PublicEnum.TransferType)("TransferType", fTransferType, value)
        End Set
    End Property

    Dim fTransferFrom As Site
    <XafDisplayName("�Ѻ�͹�ҡ")> _
    <RuleRequiredField(DefaultContexts.Save)> _
    <DataSourceCriteria("[SiteType]='Factory'")> _
    Public Property TransferFrom() As Site
        Get
            Return fTransferFrom
        End Get
        Set(ByVal value As Site)
            SetPropertyValue(Of Site)("TransferFrom", fTransferFrom, value)
        End Set
    End Property

    Dim fTransferTo As Site
    <XafDisplayName("�͹��ѧ")> _
    <RuleRequiredField(DefaultContexts.Save)> _
    <DataSourceCriteria("[SiteType]='Factory'")> _
    Public Property TransferTo() As Site
        Get
            Return fTransferTo
        End Get
        Set(ByVal value As Site)
            SetPropertyValue(Of Site)("TransferTo", fTransferTo, value)
        End Set
    End Property

    Dim fTransferReason As PublicEnum.TransferFor
    <XafDisplayName("�͹����")> _
    Public Property TransferReason() As PublicEnum.TransferFor
        Get
            Return fTransferReason
        End Get
        Set(ByVal value As PublicEnum.TransferFor)
            SetPropertyValue(Of PublicEnum.TransferFor)("TransferReason", fTransferReason, value)
        End Set
    End Property

    Dim fTransferDate As DateTime
    <XafDisplayName("�ѹ�������͹")> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property TransferDate() As DateTime
        Get
            Return fTransferDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("TransferDate", fTransferDate, value)
        End Set
    End Property

    Dim fReceiveDate As DateTime
    <XafDisplayName("�ѹ����Ѻ�͹")> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property ReceiveDate() As DateTime
        Get
            Return fReceiveDate
        End Get
        Set(ByVal value As DateTime)
            SetPropertyValue(Of DateTime)("ReceiveDate", fReceiveDate, value)
        End Set
    End Property

    Dim fTransferUser As String
    <XafDisplayName("�������͹")> _
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
    <XafDisplayName("���˹觼������͹")> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property TransferPosition() As String
        Get
            Return fTransferPosition
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("TransferPosition", fTransferPosition, value)
        End Set
    End Property

    Dim fReceiveUser As String
    <XafDisplayName("����Ѻ�͹")> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property ReceiveUser() As String
        Get
            Return fReceiveUser
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ReceiveUser", fReceiveUser, value)
        End Set
    End Property

    Dim fReceivePostion As String
    <XafDisplayName("���˹觼���Ѻ�͹")> _
    <RuleRequiredField(DefaultContexts.Save)> _
    Public Property ReceivePostion() As String
        Get
            Return fReceivePostion
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ReceivePostion", fReceivePostion, value)
        End Set
    End Property

    Dim fApproveUser As String
    <XafDisplayName("���͹��ѵ�")> _
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
    <XafDisplayName("���˹觼��͹��ѵ�")> _
    <RuleRequiredField(TargetContextIDs:="Approval")> _
    Public Property ApprovePosition() As String
        Get
            Return fApprovePosition
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("ApprovePosition", fApprovePosition, value)
        End Set
    End Property
    'Dim fSendStatus As PublicEnum.SimsStatus
    'Public Property SendStatus() As PublicEnum.SimsStatus
    '    Get
    '        Return fSendStatus
    '    End Get
    '    Set(ByVal value As PublicEnum.SimsStatus)
    '        SetPropertyValue(Of PublicEnum.SimsStatus)("SendStatus", fSendStatus, value)
    '    End Set
    'End Property

    Dim fReceiveStatus As PublicEnum.SimsStatus
    <XafDisplayName("ʶҹС���Ѻ�͹")> _
    Public Property ReceiveStatus() As PublicEnum.SimsStatus
        Get
            Return fReceiveStatus
        End Get
        Set(ByVal value As PublicEnum.SimsStatus)
            SetPropertyValue(Of PublicEnum.SimsStatus)("ReceiveStatus", fReceiveStatus, value)
        End Set
    End Property

    <Browsable(False)> _
    Public ReadOnly Property Status As PublicEnum.SimsStatus
        Get
            Return ReceiveStatus
        End Get
    End Property

    Dim fRemark As String
    <XafDisplayName("�����˵�")> _
    <Size(200)> _
    Public Property Remark() As String
        Get
            Return fRemark
        End Get
        Set(ByVal value As String)
            SetPropertyValue(Of String)("Remark", fRemark, value)
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

    <XafDisplayName("��¡�����紾ѹ������Ѻ�͹")> _
    <Association("ReceiveTransferSeed-ReceiveTransferSeedDetails", GetType(ReceiveTransferSeedDetail))> _
    Public ReadOnly Property ReceiveTransferSeedDetails() As XPCollection(Of ReceiveTransferSeedDetail)
        Get
            Return GetCollection(Of ReceiveTransferSeedDetail)("ReceiveTransferSeedDetails")
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
            For Each item In ReceiveTransferSeedDetails
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

    Public Function MarkAsComplete() As Boolean
        If Not IsDeleted Then
            If Not ReceiveTransferSeedDetails.Count > 0 Then
                MsgBox("��辺��¡�����紾ѹ��� ��سҵ�Ǩ�ͺ�������ա����", MsgBoxStyle.Information, "")
                Exit Function
            End If
            Dim _session As Session = Me.Session

            Try
                Me.ReceiveStatus = PublicEnum.SimsStatus.Approve

                If ReceiveTransferSeedDetails.Count > 0 Then
                    Dim criteria As String = "SeedStatus=? and SeedType= ? and SeedClass=? and Season=? and SeedYear=? and LotNo=? and MoneyType=?"
                    For i As Integer = 0 To ReceiveTransferSeedDetails.Count - 1
                        Dim _SeedType As SeedType = ReceiveTransferSeedDetails(i).SeedType
                        Dim _SeedStatus As SeedStatus = ReceiveTransferSeedDetails(i).SeedStatus
                        Dim _SeedClass As SeedClass = ReceiveTransferSeedDetails(i).SeedClass
                        Dim _Season As Season = ReceiveTransferSeedDetails(i).Season
                        Dim _SeedYear As String = ReceiveTransferSeedDetails(i).SeedYear
                        Dim _SeedLot As Integer = ReceiveTransferSeedDetails(i).LotNo
                        Dim _MoneyType As MoneyType = ReceiveTransferSeedDetails(i).MoneyType

                        Dim objProduct As SeedProduct = Session.FindObject(Of SeedProduct) _
                                                        (CriteriaOperator.Parse(criteria, _SeedStatus,
                                                                                            _SeedType, _
                                                                                            _SeedClass, _
                                                                                            _Season, _
                                                                                            _SeedYear, _
                                                                                            _SeedLot, _
                                                                                            _MoneyType))
                        If objProduct Is Nothing Then
                            Dim objNewProduct As New SeedProduct(Session)

                            Dim _PlantID As String = ReceiveTransferSeedDetails(i).Plant.PlantID
                            Dim _SeedStatusID As String = ReceiveTransferSeedDetails(i).SeedStatus.SeedStatusID.ToString
                            Dim _SeedTypeID As String = String.Format("{0:D2}", Convert.ToInt32(ReceiveTransferSeedDetails(i).SeedType.SeedID))
                            Dim _ClassID As String = ReceiveTransferSeedDetails(i).SeedClass.ClassID
                            Dim _SeasonID As String = ReceiveTransferSeedDetails(i).Season.SeasonID
                            Dim _Year As String = ReceiveTransferSeedDetails(i).SeedYear
                            Dim _MoneyTypeId As String = ReceiveTransferSeedDetails(i).MoneyType.MoneyTypeId
                            Dim _LotNo As String = ReceiveTransferSeedDetails(i).LotNo.ToString("D3")

                            '1-62-2-1-2556-1-001
                            objNewProduct.ProductCode = String.Format("{0}-{1}-{2}-{3}-{4}-{5}-{6}-{7}", _
                                                                      _PlantID, _
                                                                      _SeedStatusID, _
                                                                      _SeedTypeID, _
                                                                      _ClassID, _
                                                                      _SeasonID, _
                                                                      _Year, _
                                                                      _MoneyTypeId, _
                                                                      _LotNo)
                            objNewProduct.ProductName = ReceiveTransferSeedDetails(i).Plant.PlantName & "-" & _
                                                        ReceiveTransferSeedDetails(i).SeedStatus.SeedStatusName & "-" & _
                                                        ReceiveTransferSeedDetails(i).SeedType.SeedName & "-" & _
                                                        ReceiveTransferSeedDetails(i).SeedClass.ClassName & "-" & _
                                                        ReceiveTransferSeedDetails(i).Season.SeasonName & "-" & _
                                                        ReceiveTransferSeedDetails(i).SeedYear & "-" & _
                                                        ReceiveTransferSeedDetails(i).MoneyType.MoneyTypeName & "-" & _
                                                        ReceiveTransferSeedDetails(i).LotNo.ToString("D3")
                            objNewProduct.Plant = ReceiveTransferSeedDetails(i).Plant
                            objNewProduct.SeedStatus = ReceiveTransferSeedDetails(i).SeedStatus
                            objNewProduct.SeedType = ReceiveTransferSeedDetails(i).SeedType
                            objNewProduct.SeedClass = ReceiveTransferSeedDetails(i).SeedClass
                            objNewProduct.Season = ReceiveTransferSeedDetails(i).Season
                            objNewProduct.SeedYear = ReceiveTransferSeedDetails(i).SeedYear
                            objNewProduct.MoneyType = ReceiveTransferSeedDetails(i).MoneyType
                            objNewProduct.LotNo = ReceiveTransferSeedDetails(i).LotNo

                            objNewProduct.IsMix = ReceiveTransferSeedDetails(i).IsMixChemical

                            '// �ӹǹ����ѧ�����á
                            objNewProduct.TotalStockAmount = ReceiveTransferSeedDetails(i).Quantity
                            objNewProduct.AvailableAmount = ReceiveTransferSeedDetails(i).Quantity
                            objNewProduct.CollectAmount = ReceiveTransferSeedDetails(i).Quantity

                            '//����Թ������Ѻ��¡���Ѻ (�͹�á�ѧ������Թ���)
                            ReceiveTransferSeedDetails(i).SeedProduct = objNewProduct

                            '// Insert Transaction Data
                            Dim objTransactionLog As New SeedTransaction(Session, PublicEnum.SeedTransactionType.TransferIn, TransferNo, ReceiveTransferSeedDetails(i).SeedProduct, ReceiveTransferSeedDetails(i).Quantity, ReceiveTransferSeedDetails(i).Bags)
                            '            
                            objNewProduct.Save()

                        Else
                            '//����Թ������Ѻ��¡���Ѻ (�͹�á�ѧ������Թ���)
                            ReceiveTransferSeedDetails(i).SeedProduct = objProduct

                            '// Update �ʹ����ѧ(�ǡ����) 
                            objProduct.TotalStockAmount += ReceiveTransferSeedDetails(i).Quantity
                            objProduct.AvailableAmount += ReceiveTransferSeedDetails(i).Quantity
                            objProduct.CollectAmount += ReceiveTransferSeedDetails(i).Quantity
                            '// Insert Transaction Data
                            Dim objTransactionLog As New SeedTransaction(Session, PublicEnum.SeedTransactionType.TransferIn, TransferNo, ReceiveTransferSeedDetails(i).SeedProduct, ReceiveTransferSeedDetails(i).Quantity, ReceiveTransferSeedDetails(i).Bags)
                            '            
                        End If
                    Next
                    '//Session.CommitTransaction()
                End If

                MyBase.Save()

                Return True
                'Session.CommitTransaction()

            Catch ex As Exception
                Me.ReceiveStatus = PublicEnum.SimsStatus.Pending
                'Session.RollbackTransaction()
                Return False
            End Try
        End If
    End Function

    Public Function MarkAsCancel() As Boolean
        If Not IsDeleted Then
            Dim _session As Session = Me.Session

            Try
                Me.ReceiveStatus = PublicEnum.SimsStatus.Cancel
                Me.IsSubmitToCenter = False
                '//���ҧ Product
                '//MyBase.Save()

                If ReceiveTransferSeedDetails.Count > 0 Then
                    Dim criteria As String = "SeedStatus=? and SeedType= ? and SeedClass=? and Season=? and SeedYear=? and LotNo=? and MoneyType=?"
                    For i As Integer = 0 To ReceiveTransferSeedDetails.Count - 1
                        Dim _SeedStatus As SeedStatus = ReceiveTransferSeedDetails(i).SeedStatus
                        Dim _SeedType As SeedType = ReceiveTransferSeedDetails(i).SeedType
                        Dim _SeedClass As SeedClass = ReceiveTransferSeedDetails(i).SeedClass
                        Dim _Season As Season = ReceiveTransferSeedDetails(i).Season
                        Dim _SeedYear As String = ReceiveTransferSeedDetails(i).SeedYear
                        Dim _SeedLot As Integer = ReceiveTransferSeedDetails(i).LotNo
                        Dim _MoneyType As MoneyType = ReceiveTransferSeedDetails(i).MoneyType

                        Dim objProduct As SeedProduct = _session.FindObject(Of SeedProduct) _
                                                        (CriteriaOperator.Parse(criteria, _SeedStatus,
                                                                                            _SeedType, _
                                                                                            _SeedClass, _
                                                                                            _Season, _
                                                                                            _SeedYear, _
                                                                                            _SeedLot, _
                                                                                            _MoneyType))
                        If Not objProduct Is Nothing Then
                            objProduct.IsDelete = True
                            objProduct.TotalStockAmount -= ReceiveTransferSeedDetails(i).Quantity
                            objProduct.AvailableAmount -= ReceiveTransferSeedDetails(i).Quantity
                            objProduct.CollectAmount -= ReceiveTransferSeedDetails(i).Quantity
                            objProduct.Save()
                            '//ź������ Transaction �ͧ Product
                            Dim objTransactionLog As New SeedTransaction(_session, PublicEnum.SeedTransactionType.CancelTransferIn, TransferNo, ReceiveTransferSeedDetails(i).SeedProduct, ReceiveTransferSeedDetails(i).Quantity, ReceiveTransferSeedDetails(i).Bags)
                            Dim objToCancel As SeedTransaction = Session.FindObject(Of SeedTransaction)(CriteriaOperator.Parse("TransactionType='TransferIn' and RefNo=?", TransferNo))
                            If objToCancel IsNot Nothing Then
                                objToCancel.IsDelete = True
                            End If
                        End If

                    Next
                End If

                MyBase.Save()
                '_session.CommitTransaction()
                Return True
            Catch ex As Exception
                '_session.RollbackTransaction()
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
