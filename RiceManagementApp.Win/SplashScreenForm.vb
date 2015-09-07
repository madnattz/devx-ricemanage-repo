Public Class SplashScreenForm
    Inherits Form

    Public Sub New()
        InitializeComponent()
    End Sub
    Friend Sub UpdateInfo(ByVal info As String)
        msgText.Text = info
    End Sub

End Class