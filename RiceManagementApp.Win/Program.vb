Imports System
Imports System.Configuration
Imports System.Windows.Forms

Imports DevExpress.Persistent.Base
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Security
Imports DevExpress.ExpressApp.Win
Imports DevExpress.Persistent.BaseImpl
Imports RiceManagementApp.Win
Imports System.Data.SqlClient

Public Class Program

    <STAThread()> _
    Public Shared Sub Main(ByVal arguments() As String)
#If EASYTEST Then
              DevExpress.ExpressApp.Win.EasyTest.EasyTestRemotingRegistration.Register()
#End If

        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)
        EditModelPermission.AlwaysGranted = System.Diagnostics.Debugger.IsAttached
        Dim _application As RiceManagementAppWindowsFormsApplication = New RiceManagementAppWindowsFormsApplication()

        ' สำหรับทำชุดติดตั้ง ========================
        ' ตรวจสอบการกำหนดค่าการเชื่อมต่อฐานข้อมูล ต้องกำหนดค่าให้เรียบร้อยก่อน.
        Dim filePath As String = System.IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.MyDocuments, "\RiceManagementApp\LocalSettings\ConfigFiles\")
        Dim filename As String = filePath & "RiceManagementApp.Win.exe.config.txt"

        If System.IO.File.Exists(filename) Then
            If System.IO.File.Exists(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile) Then
                System.IO.File.Delete(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile)
            End If
            System.IO.File.Copy(filename, AppDomain.CurrentDomain.SetupInformation.ConfigurationFile)
            ConfigurationManager.RefreshSection("appSettings")
            ConfigurationManager.RefreshSection("ConnectionString")
        End If

        If ConfigurationManager.AppSettings("DatabaseServer") = "" Then
            Dim frmConfigDB As ConfigDB = New ConfigDB()
            frmConfigDB.ShowDialog()
            Exit Sub
        End If

        ' ======================================
        _application.SplashScreen = New SplashScreen 'New DevExpress.ExpressApp.Win.Utils.DXSplashScreen("YourSplashImage.png")
        If (Not ConfigurationManager.ConnectionStrings.Item("ConnectionString") Is Nothing) Then
            _application.ConnectionString = ConfigurationManager.ConnectionStrings.Item("ConnectionString").ConnectionString
            Dim objConn As SqlConnection = Nothing
            Try
                objConn = New SqlConnection(_application.ConnectionString)
                objConn.Open()
                objConn.Close()
                'MsgBox("สามารถเชื่อมต่อฐานข้อมูลได้ตามปกติ" & vbCrLf & "================================" & vbCrLf & "Database Server: " & txtDatabaseServer.Text & vbCrLf & "Database name: " & txtDatabaseName.Text & vbCrLf & "User: " & txtUserId.Text, MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "ทดสอบการเชื่อมต่อฐานข้อมูล")
            Catch
                MsgBox("ไม่สามารถเชื่อมต่อฐานข้อมูลได้ กรุณาตรวจสอบความถูกต้องอีกครั้ง" & _
                       vbCrLf & "================================" & vbCrLf _
                       & _application.ConnectionString, _
                       MsgBoxStyle.OkOnly + MsgBoxStyle.Critical, "ทดสอบการเชื่อมต่อฐานข้อมูล")
                Dim frmConfigDB As ConfigDB = New ConfigDB()
                frmConfigDB.ShowDialog()
                Exit Sub
            End Try
        End If
#If EASYTEST Then
        If (Not ConfigurationManager.ConnectionStrings.Item("EasyTestConnectionString") Is Nothing) Then
            _application.ConnectionString = ConfigurationManager.ConnectionStrings.Item("EasyTestConnectionString").ConnectionString
        End If
#End If
        If System.Diagnostics.Debugger.IsAttached Then
            _application.DatabaseUpdateMode = DatabaseUpdateMode.UpdateDatabaseAlways
        End If
        Try
            _application.Setup()
            _application.Start()
        Catch e As Exception
            _application.HandleException(e)
        End Try

    End Sub
End Class