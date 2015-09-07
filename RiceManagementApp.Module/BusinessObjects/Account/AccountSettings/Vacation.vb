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

'<ImageName("BO_Contact")> _
'<DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")> _
'<DefaultListViewOptions(MasterDetailMode.ListViewOnly, False, NewItemRowPosition.None)> _
'<Persistent("DatabaseTableName")> _
<NavigationItem("รายการเบิกจ่ายค่าเงินค่าเมล็ดพันธุ์")> _
<XafDisplayName("การตั้งค่าวันหยุดราชการ")> _
<DefaultClassOptions()> _
Public Class Vacation ' Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    Inherits BaseObject ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
        ' Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).
        Year = Now.Year + 543
    End Sub

    Private _Year As String
    <XafDisplayName("ปี")> _
    Public Property Year() As String
        Get
            Return _Year
        End Get
        Set(ByVal value As String)
            SetPropertyValue("Year", _Year, value)
        End Set
    End Property

    <Action(Caption:="ตั้งค่าวันหยุดราชการ", ConfirmationMessage:="ท่านต้องการตั้งค่าวันหยุดราชการใช่หรือไม่?", ImageName:="BO_Appointment", AutoCommit:=True)> _
    Public Sub Vacation()
        ' Trigger a custom business logic for the current record in the UI (http://documentation.devexpress.com/#Xaf/CustomDocument2619).
        Dim result As New XPCollection(Of VacationSetting)(Session, CriteriaOperator.Parse("[PublicStatus]=0"))
        If result.Count > 0 Then
            For Each item As VacationSetting In result
                Dim InsertVacationDetail As New VacationDetail(Session)
                InsertVacationDetail.Vacation = Me
                InsertVacationDetail.Description = item.Description
                Dim _dateString As String = Year & "/" & item.PublicMonth & "/" & item.Day
                InsertVacationDetail.VacationDate = Convert.ToDateTime(_dateString)
                InsertVacationDetail.Save()
                'With InsertVacationDetail
                '    .VacationDate = String.Format(TblVacation.Day) + String.Format("/") + String.Format(TblVacation.PublicMonth) + String.Format("/") + String.Format(_Year)
                '    .Description = TblVacation.Description
                '    .Year = Me
                '    .Save()
                'End With
            Next
        End If
    End Sub

    <XafDisplayName("ข้อมูลวันหยุดราชการ")> _
<Association("Vacation-VacationDetails", GetType(VacationDetail))> _
<DevExpress.Xpo.Aggregated()> _
    Public ReadOnly Property VacationDetails() As XPCollection(Of VacationDetail)
        Get
            Return GetCollection(Of VacationDetail)("VacationDetails")
        End Get
    End Property
End Class
