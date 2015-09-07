Imports System.Configuration
Imports System.Data.SqlClient
Imports System.Xml

''' <summary>
''' ฟอร์มสำหรับกำหนดค่าการเชื่อมต่อฐานข้อมูล Microsoft SQL Server
''' </summary>
''' <remarks></remarks>
Public Class ConfigDB

    Private Sub btnRestart_Click(sender As Object, e As EventArgs)
        Application.Restart()
    End Sub

    Private Sub ConfigDB_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        'Application.Exit()
    End Sub

    Private Sub ConfigDB_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadAppConfig()
    End Sub

    ''' <summary>
    ''' สำหรับเริ่มต้นอ่านข้อมูลจาก App Config
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadAppConfig()
        txtDatabaseServer.Text = ConfigurationManager.AppSettings("DatabaseServer")
        txtUserId.Text = ConfigurationManager.AppSettings("UserId")
        txtPassword.Text = ConfigurationManager.AppSettings("Password")
        txtDatabaseName.Text = ConfigurationManager.AppSettings("DatabaseName")

        If ConfigurationManager.AppSettings("AuthenType").ToUpper = "USER" Then
            ddlAuthenType.SelectedIndex = 1
        Else
            ddlAuthenType.SelectedIndex = 0
        End If

        If txtDatabaseServer.Text = "" Then
            PanelStartup.Visible = True
        Else
            PanelStartup.Visible = False
        End If

    End Sub

    ''' <summary>
    ''' สำหรับบันทึกข้อมูลการกำหนดค่าใน App Config
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SaveAppConfig()

    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        'Application.ExitThread()
        Me.Close()
    End Sub

    ''' <summary>
    ''' สำหรับบันทึกการกำหนดค่า App config
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        ' Save App configuration.
        Dim AuthenType As String
        Dim config As Configuration
        config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
        config.AppSettings.Settings("DatabaseServer").Value = txtDatabaseServer.Text
        config.AppSettings.Settings("DatabaseName").Value = txtDatabaseName.Text
        config.AppSettings.Settings("UserId").Value = txtUserId.Text
        config.AppSettings.Settings("Password").Value = txtPassword.Text

        If ddlAuthenType.SelectedIndex = 0 Then
            AuthenType = "WINDOWS"
        Else
            AuthenType = "USER"
        End If
        config.AppSettings.Settings("AuthenType").Value = AuthenType

        config.Save(System.Configuration.ConfigurationSaveMode.Modified)

        updateConfigFile(GetConnectionString())

        MsgBox("บันทึกข้อมูลเสร็จเรียบร้อยแล้ว", MsgBoxStyle.Information)

        ' Restart Application again.
        Application.Restart()
    End Sub


    Public Sub updateConfigFile(ByVal con As String)
        'updating config file
        Dim XmlDoc As New XmlDocument()
        'Loading the Config file
        XmlDoc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile)
        For Each xElement As XmlElement In XmlDoc.DocumentElement
            If xElement.Name = "connectionStrings" Then
                'setting the coonection string
                xElement.FirstChild.Attributes(1).Value = con
            End If
        Next
        'writing the connection string in config file
        XmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile)

        '===================== Save Temp Config Files =================
        Dim filePath As String
        filePath = System.IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.MyDocuments, "\RiceManagementApp\LocalSettings\ConfigFiles\")
        System.IO.Directory.CreateDirectory(filePath)
        Dim filename As String = filePath & "RiceManagementApp.Win.exe.config.txt"
        XmlDoc.Save(filename)
        '-------------------------------------------------------------

    End Sub

    '=======================================================
    'Service provided by Telerik (www.telerik.com)
    'Conversion powered by NRefactory.
    'Twitter: @telerik
    'Facebook: facebook.com/telerik
    '=======================================================


    Private Function GetConnectionString() As String
        Dim strConn As String = String.Empty
        If ddlAuthenType.SelectedIndex = 0 Then
            'Windows Authen.
            strConn = "Data Source=" & txtDatabaseServer.Text.Trim() & ";Initial Catalog=" & txtDatabaseName.Text.Trim() & ";Persist Security Info=False;Integrated Security=SSPI;"
        Else
            'MS SQL Server Authen.
            strConn = "Data Source=" & txtDatabaseServer.Text.Trim() & ";Initial Catalog=" & txtDatabaseName.Text.Trim() & ";Persist Security Info=True;User ID=" & txtUserId.Text.Trim() & ";Password=" & txtPassword.Text.Trim()
        End If
        Return strConn
    End Function

    Private Sub btnTest_Click(sender As Object, e As EventArgs) Handles btnTest.Click
        Dim objConn As SqlConnection = Nothing
        Try
            objConn = New SqlConnection(GetConnectionString())
            objConn.Open()
            objConn.Close()
            MsgBox("สามารถเชื่อมต่อฐานข้อมูลได้ตามปกติ" & vbCrLf & "================================" & vbCrLf & "Database Server: " & txtDatabaseServer.Text & vbCrLf & "Database name: " & txtDatabaseName.Text & vbCrLf & "User: " & txtUserId.Text, MsgBoxStyle.OkOnly + MsgBoxStyle.Information, "ทดสอบการเชื่อมต่อฐานข้อมูล")
        Catch
            MsgBox("ไม่สามารถเชื่อมต่อฐานข้อมูลได้ กรุณาตรวจสอบความถูกต้องอีกครั้ง" & vbCrLf & "================================" & vbCrLf & "Database Server: " & txtDatabaseServer.Text & vbCrLf & "Database name: " & txtDatabaseName.Text & vbCrLf & "User: " & txtUserId.Text, MsgBoxStyle.OkOnly + MsgBoxStyle.Critical, "ทดสอบการเชื่อมต่อฐานข้อมูล")
        End Try
    End Sub

    Private Sub ddlAuthenType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAuthenType.SelectedIndexChanged
        If ddlAuthenType.SelectedIndex = 0 Then
            txtUserId.Enabled = False
            txtPassword.Enabled = False
        Else
            txtUserId.Enabled = True
            txtPassword.Enabled = True
        End If
    End Sub

    Private Sub btnStart_Click(sender As Object, e As EventArgs) Handles btnStart.Click
        PanelStartup.Visible = False
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class