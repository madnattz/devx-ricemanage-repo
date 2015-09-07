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
Imports DevExpress.ExpressApp.Win.Editors
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Repository
Imports System.Globalization
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.ExpressApp.Editors

<PropertyEditor(GetType(String), False)> _
Public Class CustomTexboxWithXEditor ' Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    Inherits StringPropertyEditor ' Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
    Public Sub New(ByVal objectType As System.Type, ByVal info As IModelMemberViewItem)
        MyBase.New(objectType, info)
    End Sub
    Protected Overrides Function CreateControlCore() As Object
        Return New TextEdit()
    End Function
    Protected Overrides Function CreateRepositoryItem() As RepositoryItem
        Return New RepositoryItemTextEdit()
    End Function
    Protected Overrides Sub SetupRepositoryItem(ByVal item As DevExpress.XtraEditors.Repository.RepositoryItem)
        'Dim _year As Integer = Date.Now.Year + 543
        'For i As Integer = -1 To 4
        '    CType(item, RepositoryItemComboBox).Items.Add(_year - i)
        'Next
    End Sub
End Class
