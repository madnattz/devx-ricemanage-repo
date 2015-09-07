Imports DevExpress.Xpo
Module GetSiteSetting
    Public Function GetCurrentSite(_session As Session) As Site
        Dim siteSetting As SiteSetting = _session.FindObject(Of SiteSetting)(Nothing)
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
End Module
