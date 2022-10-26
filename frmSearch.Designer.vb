<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSearch
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
        Me.txtBook_no = New System.Windows.Forms.TextBox()
        Me.txtSo_tk = New System.Windows.Forms.TextBox()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblten_loai = New System.Windows.Forms.Label()
        Me.lblTen_nv = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtMa_nv = New System.Windows.Forms.TextBox()
        Me.txtma_loai = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtSo_ct = New System.Windows.Forms.TextBox()
        Me.lblten_kh = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtdFrom = New libs.txtDate()
        Me.txtma_kh = New System.Windows.Forms.TextBox()
        Me.txtdTo = New libs.txtDate()
        Me.butCancel = New System.Windows.Forms.Button()
        Me.butOk = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'txtBook_no
        '
        Me.txtBook_no.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtBook_no.Font = New System.Drawing.Font("Arial", 9.5!)
        Me.txtBook_no.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtBook_no.Location = New System.Drawing.Point(147, 128)
        Me.txtBook_no.MaxLength = 20
        Me.txtBook_no.Name = "txtBook_no"
        Me.txtBook_no.Size = New System.Drawing.Size(251, 22)
        Me.txtBook_no.TabIndex = 51
        Me.txtBook_no.Tag = "FCCF"
        '
        'txtSo_tk
        '
        Me.txtSo_tk.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSo_tk.Font = New System.Drawing.Font("Arial", 9.5!)
        Me.txtSo_tk.Location = New System.Drawing.Point(147, 153)
        Me.txtSo_tk.Name = "txtSo_tk"
        Me.txtSo_tk.Size = New System.Drawing.Size(251, 22)
        Me.txtSo_tk.TabIndex = 53
        Me.txtSo_tk.Tag = "FCCF"
        '
        'Label36
        '
        Me.Label36.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label36.AutoSize = True
        Me.Label36.Font = New System.Drawing.Font("Arial", 9.5!)
        Me.Label36.ForeColor = System.Drawing.Color.Black
        Me.Label36.Location = New System.Drawing.Point(12, 133)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(78, 16)
        Me.Label36.TabIndex = 50
        Me.Label36.Text = "Số Booking."
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.5!)
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(12, 157)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(68, 16)
        Me.Label2.TabIndex = 52
        Me.Label2.Text = "Số tờ khai"
        '
        'lblten_loai
        '
        Me.lblten_loai.Font = New System.Drawing.Font("Arial", 9.5!)
        Me.lblten_loai.ForeColor = System.Drawing.Color.Black
        Me.lblten_loai.Location = New System.Drawing.Point(276, 37)
        Me.lblten_loai.Name = "lblten_loai"
        Me.lblten_loai.Size = New System.Drawing.Size(323, 16)
        Me.lblten_loai.TabIndex = 41
        Me.lblten_loai.Tag = "RF"
        Me.lblten_loai.Text = "Label5"
        Me.lblten_loai.Visible = False
        '
        'lblTen_nv
        '
        Me.lblTen_nv.Font = New System.Drawing.Font("Arial", 9.5!)
        Me.lblTen_nv.ForeColor = System.Drawing.Color.Black
        Me.lblTen_nv.Location = New System.Drawing.Point(276, 106)
        Me.lblTen_nv.Name = "lblTen_nv"
        Me.lblTen_nv.Size = New System.Drawing.Size(297, 15)
        Me.lblTen_nv.TabIndex = 49
        Me.lblTen_nv.Tag = "RF"
        Me.lblTen_nv.Text = "Label5"
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Font = New System.Drawing.Font("Arial", 9.5!)
        Me.Label32.ForeColor = System.Drawing.Color.Teal
        Me.Label32.Location = New System.Drawing.Point(12, 58)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(82, 16)
        Me.Label32.TabIndex = 42
        Me.Label32.Text = "Số đơn hàng"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Arial", 9.5!)
        Me.Label10.ForeColor = System.Drawing.Color.Black
        Me.Label10.Location = New System.Drawing.Point(12, 83)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(76, 16)
        Me.Label10.TabIndex = 44
        Me.Label10.Text = "Khách hàng"
        '
        'txtMa_nv
        '
        Me.txtMa_nv.Font = New System.Drawing.Font("Arial", 9.5!)
        Me.txtMa_nv.Location = New System.Drawing.Point(147, 103)
        Me.txtMa_nv.Name = "txtMa_nv"
        Me.txtMa_nv.Size = New System.Drawing.Size(122, 22)
        Me.txtMa_nv.TabIndex = 48
        Me.txtMa_nv.Tag = "FCCF"
        '
        'txtma_loai
        '
        Me.txtma_loai.Font = New System.Drawing.Font("Arial", 9.5!)
        Me.txtma_loai.ForeColor = System.Drawing.Color.Black
        Me.txtma_loai.Location = New System.Drawing.Point(147, 30)
        Me.txtma_loai.MaxLength = 13
        Me.txtma_loai.Name = "txtma_loai"
        Me.txtma_loai.Size = New System.Drawing.Size(122, 22)
        Me.txtma_loai.TabIndex = 40
        Me.txtma_loai.Tag = ""
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Arial", 9.5!)
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(12, 107)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(61, 16)
        Me.Label8.TabIndex = 47
        Me.Label8.Text = "Điều phối"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.5!)
        Me.Label3.ForeColor = System.Drawing.Color.Teal
        Me.Label3.Location = New System.Drawing.Point(12, 33)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(63, 16)
        Me.Label3.TabIndex = 39
        Me.Label3.Text = "Loại hàng"
        '
        'txtSo_ct
        '
        Me.txtSo_ct.Font = New System.Drawing.Font("Arial", 9.5!)
        Me.txtSo_ct.Location = New System.Drawing.Point(147, 54)
        Me.txtSo_ct.Name = "txtSo_ct"
        Me.txtSo_ct.Size = New System.Drawing.Size(122, 22)
        Me.txtSo_ct.TabIndex = 43
        Me.txtSo_ct.Tag = "FCCF"
        '
        'lblten_kh
        '
        Me.lblten_kh.Font = New System.Drawing.Font("Arial", 9.5!)
        Me.lblten_kh.ForeColor = System.Drawing.Color.Black
        Me.lblten_kh.Location = New System.Drawing.Point(276, 82)
        Me.lblten_kh.Name = "lblten_kh"
        Me.lblten_kh.Size = New System.Drawing.Size(297, 15)
        Me.lblten_kh.TabIndex = 46
        Me.lblten_kh.Tag = "RF"
        Me.lblten_kh.Text = "ten khach hang"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.5!)
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(82, 16)
        Me.Label1.TabIndex = 36
        Me.Label1.Text = "Từ/Đến ngày"
        '
        'txtdFrom
        '
        Me.txtdFrom.Font = New System.Drawing.Font("Arial", 9.5!)
        Me.txtdFrom.Location = New System.Drawing.Point(147, 6)
        Me.txtdFrom.MaxLength = 10
        Me.txtdFrom.Name = "txtdFrom"
        Me.txtdFrom.Size = New System.Drawing.Size(122, 22)
        Me.txtdFrom.TabIndex = 37
        Me.txtdFrom.Tag = "FCCF"
        Me.txtdFrom.Text = "09-May-10"
        Me.txtdFrom.Value = New Date(2010, 5, 9, 0, 0, 0, 0)
        '
        'txtma_kh
        '
        Me.txtma_kh.Font = New System.Drawing.Font("Arial", 9.5!)
        Me.txtma_kh.Location = New System.Drawing.Point(147, 79)
        Me.txtma_kh.Name = "txtma_kh"
        Me.txtma_kh.Size = New System.Drawing.Size(122, 22)
        Me.txtma_kh.TabIndex = 45
        Me.txtma_kh.Tag = "FCCF"
        '
        'txtdTo
        '
        Me.txtdTo.Font = New System.Drawing.Font("Arial", 9.5!)
        Me.txtdTo.Location = New System.Drawing.Point(276, 6)
        Me.txtdTo.MaxLength = 10
        Me.txtdTo.Name = "txtdTo"
        Me.txtdTo.Size = New System.Drawing.Size(122, 22)
        Me.txtdTo.TabIndex = 38
        Me.txtdTo.Tag = "FCCF"
        Me.txtdTo.Text = "09-May-10"
        Me.txtdTo.Value = New Date(2010, 5, 9, 0, 0, 0, 0)
        '
        'butCancel
        '
        Me.butCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.butCancel.BackColor = System.Drawing.Color.White
        Me.butCancel.Font = New System.Drawing.Font("Arial", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.butCancel.ForeColor = System.Drawing.Color.Red
        Me.butCancel.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.butCancel.Location = New System.Drawing.Point(119, 217)
        Me.butCancel.Name = "butCancel"
        Me.butCancel.Size = New System.Drawing.Size(98, 26)
        Me.butCancel.TabIndex = 35
        Me.butCancel.Tag = "CB01"
        Me.butCancel.Text = "&Hủy"
        Me.butCancel.UseVisualStyleBackColor = False
        '
        'butOk
        '
        Me.butOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.butOk.BackColor = System.Drawing.Color.WhiteSmoke
        Me.butOk.Font = New System.Drawing.Font("Arial", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.butOk.ForeColor = System.Drawing.Color.Teal
        Me.butOk.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.butOk.Location = New System.Drawing.Point(12, 217)
        Me.butOk.Name = "butOk"
        Me.butOk.Size = New System.Drawing.Size(98, 26)
        Me.butOk.TabIndex = 34
        Me.butOk.Tag = ""
        Me.butOk.Text = "&Lọc"
        Me.butOk.UseVisualStyleBackColor = False
        '
        'frmSearch
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(582, 255)
        Me.Controls.Add(Me.txtBook_no)
        Me.Controls.Add(Me.txtSo_tk)
        Me.Controls.Add(Me.Label36)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lblten_loai)
        Me.Controls.Add(Me.lblTen_nv)
        Me.Controls.Add(Me.Label32)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.txtMa_nv)
        Me.Controls.Add(Me.txtma_loai)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtSo_ct)
        Me.Controls.Add(Me.lblten_kh)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtdFrom)
        Me.Controls.Add(Me.txtma_kh)
        Me.Controls.Add(Me.txtdTo)
        Me.Controls.Add(Me.butCancel)
        Me.Controls.Add(Me.butOk)
        Me.Name = "frmSearch"
        Me.Text = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txtBook_no As TextBox
    Friend WithEvents txtSo_tk As TextBox
    Friend WithEvents Label36 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents lblten_loai As Label
    Friend WithEvents lblTen_nv As Label
    Friend WithEvents Label32 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents txtMa_nv As TextBox
    Friend WithEvents txtma_loai As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents txtSo_ct As TextBox
    Friend WithEvents lblten_kh As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents txtdFrom As libs.txtDate
    Friend WithEvents txtma_kh As TextBox
    Friend WithEvents txtdTo As libs.txtDate
    Friend WithEvents butCancel As Button
    Friend WithEvents butOk As Button
End Class
