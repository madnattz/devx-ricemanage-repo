Imports DevExpress.Xpo
Public Class GlobalSiteSetting
    Public Shared Function GetCurrentSite(session As Session) As Site
        Dim siteSetting As SiteSetting = session.FindObject(Of SiteSetting)(Nothing)
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
End Class
