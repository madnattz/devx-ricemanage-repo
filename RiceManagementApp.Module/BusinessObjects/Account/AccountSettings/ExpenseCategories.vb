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
Imports DevExpress.Persistent.Base.General


<NavigationItem("งบประมาณ สงป302")> _
<DefaultClassOptions(), XafDisplayName("ตั้งค่ารายจ่าย"), DefaultProperty("ExpenseName")> _
Public Class ExpenseCategories ' Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
    Implements ITreeNode
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        ' Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).
    End Sub


    Private _ExpenseID As String
    <XafDisplayName("รหัสรายจ่าย")> _
    <Size(15)> _
    <Index(1), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    Public Property ExpenseID() As String
        Get
            Return _ExpenseID
        End Get
        Set(ByVal value As String)
            SetPropertyValue("ExpenseID", _ExpenseID, value)
        End Set
    End Property

    Private _ExpenseName As String
    <XafDisplayName("ชื่อรายจ่าย")> _
    <Index(2), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    Public Property ExpenseName() As String
        Get
            Return _ExpenseName
        End Get
        Set(ByVal value As String)
            SetPropertyValue("ExpenseName", _ExpenseName, value)
        End Set
    End Property

    Private _ExpenseType As TypeGF
    <XafDisplayName("Group")> _
    <ImmediatePostData> _
    Public Property ExpenseType() As TypeGF
        Get
            Return _ExpenseType
        End Get
        Set(ByVal value As TypeGF)
            SetPropertyValue("ExpenseType", _ExpenseType, value)
        End Set
    End Property

    Public ReadOnly Property LevelItem() As Integer
        Get
            Try
                Dim SQL As String = "  with Emp as (select oid,ExpenseID, ExpenseName,1 as EmpLevel from ExpenseCategories as Parent where Parent.ParentExpenses is null " & _
                                    "  union all " & _
                                    "  select Child.oid, Child.ExpenseID , Child.ExpenseName ,EL.EmpLevel+1  from ExpenseCategories as Child  " & _
                                    "  inner join emp as EL " & _
                                    "  on Child.ParentExpenses  =el.oid " & _
                                    "  where Child.ParentExpenses is not null) " & _
                                    "  select EmpLevel from EMP where ExpenseID  ='" & ExpenseID & "' "

                Return Session.ExecuteScalar(SQL)

            Catch ex As Exception

            End Try

        End Get
    End Property

    Private _Remark As String
    <XafDisplayName("หมายเหตุ")> _
    <Index(5), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    Public Property Remark() As String
        Get
            Return _Remark
        End Get
        Set(ByVal value As String)
            SetPropertyValue("Remark", _Remark, value)
        End Set
    End Property

    Private _PublicStatus As PublicStatus
    <XafDisplayName("สถานะ")> _
    <Index(6), VisibleInListView(True), VisibleInDetailView(True), VisibleInLookupListView(True)> _
    Public Property PublicStatus() As PublicStatus
        Get
            Return _PublicStatus
        End Get
        Set(ByVal value As PublicStatus)
            SetPropertyValue("PublicStatus", _PublicStatus, value)
        End Set
    End Property

    Private _parentExpenses As ExpenseCategories
    <Browsable(False), Association("ExpenseCategories-ExpenseCategories")> _
    Public Property ParentExpenses() As ExpenseCategories
        Get
            Return _parentExpenses
        End Get
        Set(ByVal value As ExpenseCategories)
            SetPropertyValue(Of ExpenseCategories)("ParentExpenses", _parentExpenses, value)
        End Set
    End Property
    <Association("ExpenseCategories-ExpenseCategories"), DevExpress.Xpo.Aggregated()> _
    Public ReadOnly Property NestedExpenses() As XPCollection(Of ExpenseCategories)
        Get
            Return GetCollection(Of ExpenseCategories)("NestedExpenses")
        End Get
    End Property

#Region "ITreeNode Members"
    Private ReadOnly Property Children() As IBindingList Implements ITreeNode.Children
        Get
            Return NestedExpenses
        End Get
    End Property
    Private ReadOnly Property Name() As String Implements ITreeNode.Name
        Get
            Return ExpenseName
        End Get
    End Property
    Private ReadOnly Property Parent() As ITreeNode Implements ITreeNode.Parent
        Get
            Return ParentExpenses
        End Get
    End Property
#End Region
End Class
Public Enum TypeGF
    <XafDisplayName("งบบุคลากร")> _
    a = 1
    <XafDisplayName("งบดำเนินงาน")> _
    b = 2
    <XafDisplayName("งบลงทุน")> _
    c = 3
    <XafDisplayName("งบเงินอุดหนุน")> _
    d = 4
    <XafDisplayName("งบรายจ่ายอื่นๆ")> _
    f = 5
End Enum