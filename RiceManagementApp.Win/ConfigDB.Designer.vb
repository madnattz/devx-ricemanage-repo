<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ConfigDB
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtDatabaseServer = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtUserId = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtDatabaseName = New System.Windows.Forms.TextBox()
        Me.btnTest = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.ddlAuthenType = New System.Windows.Forms.ComboBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.PanelStartup = New System.Windows.Forms.Panel()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnStart = New System.Windows.Forms.Button()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.PanelStartup.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 96)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(99, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "ชื่อเครื่องฐานข้อมูล:"
        '
        'txtDatabaseServer
        '
        Me.txtDatabaseServer.ForeColor = System.Drawing.Color.Blue
        Me.txtDatabaseServer.Location = New System.Drawing.Point(121, 93)
        Me.txtDatabaseServer.Name = "txtDatabaseServer"
        Me.txtDatabaseServer.Size = New System.Drawing.Size(219, 21)
        Me.txtDatabaseServer.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(55, 185)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(60, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "ชื่อล็อกอิน:"
        '
        'txtUserId
        '
        Me.txtUserId.ForeColor = System.Drawing.Color.Blue
        Me.txtUserId.Location = New System.Drawing.Point(121, 182)
        Me.txtUserId.Name = "txtUserId"
        Me.txtUserId.Size = New System.Drawing.Size(219, 21)
        Me.txtUserId.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(66, 215)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(49, 13)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "รหัสผ่าน:"
        '
        'txtPassword
        '
        Me.txtPassword.ForeColor = System.Drawing.Color.Blue
        Me.txtPassword.Location = New System.Drawing.Point(121, 212)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.Size = New System.Drawing.Size(219, 21)
        Me.txtPassword.TabIndex = 2
        Me.txtPassword.UseSystemPasswordChar = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(44, 247)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(71, 13)
        Me.Label4.TabIndex = 1
        Me.Label4.Text = "ชื่อฐานข้อมูล:"
        '
        'txtDatabaseName
        '
        Me.txtDatabaseName.ForeColor = System.Drawing.Color.Blue
        Me.txtDatabaseName.Location = New System.Drawing.Point(121, 244)
        Me.txtDatabaseName.Name = "txtDatabaseName"
        Me.txtDatabaseName.Size = New System.Drawing.Size(219, 21)
        Me.txtDatabaseName.TabIndex = 2
        '
        'btnTest
        '
        Me.btnTest.BackColor = System.Drawing.Color.SandyBrown
        Me.btnTest.Location = New System.Drawing.Point(121, 277)
        Me.btnTest.Name = "btnTest"
        Me.btnTest.Size = New System.Drawing.Size(60, 29)
        Me.btnTest.TabIndex = 0
        Me.btnTest.Text = "ทดสอบ"
        Me.btnTest.UseVisualStyleBackColor = False
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(121, 326)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(80, 29)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "บันทึก"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(207, 326)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 29)
        Me.btnCancel.TabIndex = 0
        Me.btnCancel.Text = "ยกเลิก"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(120, 316)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(220, 1)
        Me.Label5.TabIndex = 3
        '
        'Label6
        '
        Me.Label6.ForeColor = System.Drawing.Color.Gray
        Me.Label6.Location = New System.Drawing.Point(18, 381)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(353, 32)
        Me.Label6.TabIndex = 1
        Me.Label6.Text = "หลังจากทำการแก้ไขค่าและบันทึกข้อมูลเรียบร้อยแล้ว จำเป็นต้องทำการปิดโปรแกรมและทำกา" & _
    "รเปิดโปรแกรมใหม่อีกครั้ง"
        '
        'Label7
        '
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label7.Location = New System.Drawing.Point(76, 34)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(310, 25)
        Me.Label7.TabIndex = 1
        Me.Label7.Text = "กำหนดค่าสำหรับการเชื่อมต่อฐานข้อมูลของระบบงาน"
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label8.Location = New System.Drawing.Point(77, 51)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(259, 21)
        Me.Label8.TabIndex = 1
        Me.Label8.Text = "Microsoft SQL Server Database Setup"
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.DimGray
        Me.Label9.Location = New System.Drawing.Point(119, 118)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(222, 32)
        Me.Label9.TabIndex = 1
        Me.Label9.Text = "ชื่อเครื่องคอมพิวเตอร์หรือ IP Address ของเครื่องคอมพิวเตอร์ที่ติดตั้งฐานข้อมูลไว้" & _
    ""
        '
        'ddlAuthenType
        '
        Me.ddlAuthenType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ddlAuthenType.FormattingEnabled = True
        Me.ddlAuthenType.Items.AddRange(New Object() {"Windows Authenticate", "SQL Server Authentication"})
        Me.ddlAuthenType.Location = New System.Drawing.Point(121, 153)
        Me.ddlAuthenType.Name = "ddlAuthenType"
        Me.ddlAuthenType.Size = New System.Drawing.Size(219, 21)
        Me.ddlAuthenType.TabIndex = 5
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(10, 156)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(105, 13)
        Me.Label10.TabIndex = 1
        Me.Label10.Text = "รูปแบบการตรวจสอบ:"
        '
        'PanelStartup
        '
        Me.PanelStartup.BackColor = System.Drawing.Color.White
        Me.PanelStartup.Controls.Add(Me.Label15)
        Me.PanelStartup.Controls.Add(Me.btnClose)
        Me.PanelStartup.Controls.Add(Me.btnStart)
        Me.PanelStartup.Controls.Add(Me.PictureBox2)
        Me.PanelStartup.Controls.Add(Me.Label12)
        Me.PanelStartup.Controls.Add(Me.Label14)
        Me.PanelStartup.Controls.Add(Me.Label13)
        Me.PanelStartup.Controls.Add(Me.Label11)
        Me.PanelStartup.Controls.Add(Me.PictureBox3)
        Me.PanelStartup.Location = New System.Drawing.Point(0, 0)
        Me.PanelStartup.Name = "PanelStartup"
        Me.PanelStartup.Size = New System.Drawing.Size(398, 432)
        Me.PanelStartup.TabIndex = 6
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Label15.Location = New System.Drawing.Point(51, 223)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(300, 1)
        Me.Label15.TabIndex = 4
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.White
        Me.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Location = New System.Drawing.Point(363, 398)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(34, 31)
        Me.btnClose.TabIndex = 3
        Me.btnClose.Text = "ปิด"
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'btnStart
        '
        Me.btnStart.Location = New System.Drawing.Point(110, 285)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(178, 49)
        Me.btnStart.TabIndex = 2
        Me.btnStart.Text = "กำหนดค่าการเชื่อมต่อฐานข้อมูล >"
        Me.btnStart.UseVisualStyleBackColor = True
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = Global.RiceManagementApp.Win.My.Resources.Resources.kasert_logo09
        Me.PictureBox2.Location = New System.Drawing.Point(136, 49)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(126, 97)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox2.TabIndex = 1
        Me.PictureBox2.TabStop = False
        '
        'Label12
        '
        Me.Label12.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label12.Location = New System.Drawing.Point(71, 151)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(257, 34)
        Me.Label12.TabIndex = 0
        Me.Label12.Text = "โครงการการพัฒนาระบบบริหารจัดการการผลิตและกระจายเมล็ดพันธุ์ข้าว"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label14
        '
        Me.Label14.ForeColor = System.Drawing.Color.DarkGray
        Me.Label14.Location = New System.Drawing.Point(24, 404)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(350, 19)
        Me.Label14.TabIndex = 0
        Me.Label14.Text = "พัฒนาโดย บริษัทนิวเทคโนโลยี อินฟอร์เมชั่น จำกัด"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label13
        '
        Me.Label13.Location = New System.Drawing.Point(24, 185)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(350, 19)
        Me.Label13.TabIndex = 0
        Me.Label13.Text = "กรมการข้าว   กระทรวงเกษตรและสหกรณ์"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label11
        '
        Me.Label11.Location = New System.Drawing.Point(47, 231)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(305, 47)
        Me.Label11.TabIndex = 0
        Me.Label11.Text = "สำหรับการเริ่มต้นใช้งานครั้งแรกจำเป็นต้องทำการกำหนดค่าการเชื่อมต่อกับฐานข้อมูลระบ" & _
    "บงานของศูนย์ให้เรียบร้อยก่อน"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'PictureBox3
        '
        Me.PictureBox3.Image = Global.RiceManagementApp.Win.My.Resources.Resources.title_bg
        Me.PictureBox3.Location = New System.Drawing.Point(0, 0)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(398, 171)
        Me.PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox3.TabIndex = 5
        Me.PictureBox3.TabStop = False
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.RiceManagementApp.Win.My.Resources.Resources.new_database
        Me.PictureBox1.Location = New System.Drawing.Point(17, 14)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(54, 69)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 4
        Me.PictureBox1.TabStop = False
        '
        'ConfigDB
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(398, 430)
        Me.Controls.Add(Me.PanelStartup)
        Me.Controls.Add(Me.ddlAuthenType)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtDatabaseName)
        Me.Controls.Add(Me.txtPassword)
        Me.Controls.Add(Me.txtUserId)
        Me.Controls.Add(Me.txtDatabaseServer)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnTest)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.PictureBox1)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.MaximizeBox = False
        Me.Name = "ConfigDB"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "กำหนดค่าการเชื่อมต่อฐานข้อมูล"
        Me.PanelStartup.ResumeLayout(False)
        Me.PanelStartup.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtDatabaseServer As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtUserId As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtDatabaseName As System.Windows.Forms.TextBox
    Friend WithEvents btnTest As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents ddlAuthenType As System.Windows.Forms.ComboBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents PanelStartup As System.Windows.Forms.Panel
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents btnStart As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents PictureBox3 As System.Windows.Forms.PictureBox
End Class
