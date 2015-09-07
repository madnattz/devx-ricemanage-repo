Imports System.Linq
Imports System.Text
Imports DevExpress.ExpressApp.Win
Public Class SplashScreen
    Implements ISplash, ISupportUpdateSplash

    Private Shared form As SplashScreenForm
    Private Shared _isStarted As Boolean = False
    Public Sub Start() Implements ISplash.Start
        _isStarted = True
        form = New SplashScreenForm()
        form.Show()
        System.Windows.Forms.Application.DoEvents()
    End Sub
    Public Sub [Stop]() Implements ISplash.Stop
        If form IsNot Nothing Then
            form.Hide()
            form.Close()
            form = Nothing
        End If
        _isStarted = False
    End Sub
    Public Sub SetDisplayText(ByVal displayText As String) Implements ISplash.SetDisplayText
    End Sub
    Public ReadOnly Property IsStarted() As Boolean Implements ISplash.IsStarted
        Get
            Return _isStarted
        End Get
    End Property
    Public Sub UpdateSplash(ByVal caption As String, ByVal description As String, ByVal ParamArray additionalParams() As Object) Implements ISupportUpdateSplash.UpdateSplash
        Try
            form.UpdateInfo(description)
        Catch ex As Exception

        End Try

    End Sub

End Class