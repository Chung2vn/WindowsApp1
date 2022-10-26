Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Threading
Imports libs

Public Class frmMain

#Region " Declare "

    Public Const MaxColumns = 50
    Public Shared conn As New SqlConnection, UserID As Integer = 0, MenuID As String = "", Admin As Boolean, Username As String = "", dtOptions As New DataTable(), isContinue As Boolean = True, Usersales As String = ""
    Public Shared dsMain As New DataSet, tblMaster As New DataView, tblDetail, tblDetail2, tblDetail3 As New DataView, ctrlButtons(11) As Button
    Public oColumns As New Collection(), tbsDetail, tbsDetail2, tbsDetail3 As New DataGridTableStyle
    Public iDetailRow, iDetailRow2, iDetailRow3 As Integer, iOldMasterRow As Integer, cIDNumber As String, cOldIDNumber As String, cOldMa_ct As String = "", cOldSo_ct As String = "", cOldchk_nhapkhau As String = ""
    Public Shared iMasterRow As Integer, tbcDetail(MaxColumns), tbcDetail2(MaxColumns), tbcDetail3(MaxColumns) As DataGridTextBoxColumn
    Public colma_cong, colten_cong, colseal_no, colma_kien, colsl_x, colsl_t, colso_cong, colsl_n, colhs_code, coldien_giai As DataGridTextBoxColumn

    Public colma_phi2, colten_phi2, colma_dvt2, colten_dvt2, colgia2, coldien_giai2 As DataGridTextBoxColumn
    Public colma_phi3, colten_phi3, colma_dvt3, colten_dvt3, colgia3, coldien_giai3 As DataGridTextBoxColumn

    Private oCong, oPhi2, oPhi3, oDvt2, oDvt3 As GridLib, oldtblDetail, oldtblDetail2, oldtblDetail3 As DataTable, isActive As Boolean = False, cOldItem As String = "", cOldPhi2 As String = "", cOldPhi3 As String = "", strSQL As String = ""
    Public Shared _assembly As System.Reflection.Assembly, controller As String = ""

    Private Sub grdDetail_Navigate(sender As Object, ne As NavigateEventArgs) Handles grdDetail.Navigate

    End Sub

    Private Sub txtngay_dk_TextChanged(sender As Object, e As EventArgs) Handles txtngay_dk.TextChanged

    End Sub

    Private Sub txtngay_hh_TextChanged(sender As Object, e As EventArgs) Handles txtngay_hh.TextChanged

    End Sub

    Private Sub grdDetail3_Navigate(sender As Object, ne As NavigateEventArgs) Handles grdDetail3.Navigate

    End Sub

    Private Sub cmdView_Click(sender As Object, e As EventArgs) Handles cmdView.Click

    End Sub

    Private Sub lblten_noi_dr_Click(sender As Object, e As EventArgs) Handles lblten_noi_dr.Click

    End Sub

    Private oMa_code As clsAutocl.DirLibObj.DirLib, oMa_dr As clsAutocl.DirLibObj.DirLib

    Public aButtons(11) As Button
    Public oVoucher As libs.Voucher
    Private datetime2 As String = ""
    Public Shared appName As String = ""
    Private isLoaded As Boolean = False
    Public Shared appLog As String = ""
    Private cIDVoucher As String = ""
#End Region

#Region " Page Events "

    Public Sub New()

        Try

            Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")
            Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")
            System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy"
            System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.LongDatePattern = "dd/MM/yyyy"
            System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortTimePattern = "HH:mm:ss"
            System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.LongTimePattern = "HH:mm:ss"
        Catch ex As Exception
            MsgBox("Kiểm tra ngày giờ hệ thống")
        End Try
        InitializeComponent()
    End Sub

    Private Sub frmMain_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.D1 Then
            oTab.SelectedIndex = 0
        ElseIf e.Control AndAlso e.KeyCode = Keys.D2 Then
            oTab.SelectedIndex = 1
        ElseIf e.Control AndAlso e.KeyCode = Keys.D3 Then
            oTab.SelectedIndex = 0
        ElseIf e.Control AndAlso e.KeyCode = Keys.D4 Then
            oTab.SelectedIndex = 1
        ElseIf e.Control AndAlso e.KeyCode = Keys.D5 Then
            oTab.SelectedIndex = 2
        ElseIf e.Control AndAlso e.KeyCode = Keys.D6 Then
            oTab.SelectedIndex = 3
        End If
    End Sub

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim debug As Boolean = True
        'If My.Computer.Name.ToLower = "tronghv" Then
        '    debug = True
        'End If


        If Not Authentication.StartUp(My.Application.CommandLineArgs, appName, UserID, conn, controller, MenuID, Admin, debug) Then
            Application.Exit()
        End If

        If debug Then
            conn = New SqlConnection("Server=GF75-THIN-9RCX\SQLEXPRESS;database=Log40_NguyenLong;user=sa;pwd=123456")
            UserID = 1
        End If
        cIDVoucher = ""
        Dim argLength As Integer = My.Application.CommandLineArgs.Count
        If argLength > 0 Then
            If InStr(My.Application.CommandLineArgs(argLength - 1), "#") > 0 Then
                cIDVoucher = My.Application.CommandLineArgs(argLength - 1).Trim.Replace("#", "")
            End If
        End If
        txtso_ct.MaxLength = 7
        txtNgay_ct.AddCalenderControl()
        txtngay_dk.AddCalenderControl()
        txtngay_hh.AddCalenderControl()

        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

        oVoucher = New Voucher(conn, Me, MenuID, UserID, Admin, aButtons, oTab, txtNgay_ct, Nothing)
        oVoucher.Table = clsDefine.cMTable

        Username = Sql.GetValue(conn, String.Format("select rtrim(username) + ': ' + ltrim(fullname) from userinfo where id = {0}", UserID))
        'Usersales = "NV04"
        Usersales = Sql.GetValue(conn, String.Format("select ISNULL(ma_nv,'') ma_nv from userinfo where id = {0}", UserID))
        Try
            strSQL = "select name, prec as Maxlength from syscolumns where (id in (select id from sysobjects where name = '" + clsDefine.cMTable + "'))"

            Dim dtColumns As DataTable = Sql.GetRecord(conn, strSQL)

            With dtColumns
                For iColumn = 0 To .Rows.Count - 1
                    oColumns.Add(.Rows(iColumn).Item("Maxlength"), Trim(.Rows(iColumn).Item("Name")))
                Next
            End With
            Dim iControl As Control
            For Each iControl In Me.Controls
                If Microsoft.VisualBasic.Left(iControl.Tag, 1) = "F" Then
                    Dim iText As TextBox
                    iText = iControl
                    Dim cFieldName = Microsoft.VisualBasic.Right(iControl.Name, iControl.Name.Length - 3)
                    If Mid(iControl.Tag, 2, 1) = "C" Then
                        iText = iControl
                        iText.MaxLength = oColumns(cFieldName)
                    End If
                End If
            Next
            dtColumns = Nothing
        Catch ex As Exception
            MsgBox("Lỗi kết nối")
        End Try


        txtT_sl_t.Format = Functions.GetOptions(conn, dtOptions, "m_ip_tien")
        txtT_sl_x.Format = Functions.GetOptions(conn, dtOptions, "m_ip_tien")
        txtT_sl_n.Format = Functions.GetOptions(conn, dtOptions, "m_ip_tien")

        Dim i As Integer
        For i = 0 To MaxColumns - 1
            tbcDetail(i) = New DataGridTextBoxColumn
            tbcDetail2(i) = New DataGridTextBoxColumn
            tbcDetail3(i) = New DataGridTextBoxColumn
        Next

        iMasterRow = -1
        iOldMasterRow = -1
        iDetailRow = -1
        cIDNumber = ""
        cOldIDNumber = ""
        strSQL = "select top 0 *,'' ten_kh,'' ten_loai,'' ten_nv, '' ten_noi_di,'' ten_noi_dr,'' ten_noi_den,'' ten_hang from " + clsDefine.cMTable
        Sql.SQLRetrieve(conn, strSQL, "Master", dsMain)
        strSQL = "select top 0 *,'' ten_cong from " + clsDefine.cDTable
        Sql.SQLRetrieve(conn, strSQL, "Detail", dsMain)

        strSQL = "select top 0 *,'' ten_phi, '' ten_dvt from " + clsDefine.cDTable2
        Sql.SQLRetrieve(conn, strSQL, "Detail2", dsMain)
        strSQL = "select top 0 *,'' ten_phi,'' ten_dvt from " + clsDefine.cDTable3
        Sql.SQLRetrieve(conn, strSQL, "Detail3", dsMain)

        tblMaster.Table = dsMain.Tables("Master")
        tblDetail.Table = dsMain.Tables("Detail")
        tblDetail2.Table = dsMain.Tables("Detail2")
        tblDetail3.Table = dsMain.Tables("Detail3")


        Grid.Fill(grdDetail, tblDetail, tbsDetail, tbcDetail, clsDefine.sDField, clsDefine.sDHeader, clsDefine.sDFormat, clsDefine.sDWidth)
        grdDetail.dvGrid = tblDetail
        grdDetail.cFieldKey = "ma_cong"
        grdDetail.AllowSorting = False
        grdDetail.TableStyles(0).AllowSorting = False
        colma_cong = Grid.GetColumn(grdDetail, "ma_cong")
        colten_cong = Grid.GetColumn(grdDetail, "ten_cong")
        colsl_n = Grid.GetColumn(grdDetail, "sl_n")
        colsl_x = Grid.GetColumn(grdDetail, "sl_x")
        colsl_t = Grid.GetColumn(grdDetail, "sl_t")
        coldien_giai = Grid.GetColumn(grdDetail, "dien_giai")
        colma_cong.DataGridTableStyle.BackColor = Color.FloralWhite


        Grid.Fill(grdDetail2, tblDetail2, tbsDetail2, tbcDetail2, clsDefine.sDField2, clsDefine.sDHeader2, clsDefine.sDFormat2, clsDefine.sDWidth2)
        grdDetail2.dvGrid = tblDetail2
        grdDetail2.cFieldKey = "ma_phi"
        colma_phi2 = Grid.GetColumn(grdDetail2, "ma_phi")
        colten_phi2 = Grid.GetColumn(grdDetail2, "ten_phi")
        colma_dvt2 = Grid.GetColumn(grdDetail2, "ma_dvt")
        colten_dvt2 = Grid.GetColumn(grdDetail2, "ten_dvt")
        colgia2 = Grid.GetColumn(grdDetail2, "gia")
        coldien_giai2 = Grid.GetColumn(grdDetail2, "dien_giai")
        Grid.Fill(grdDetail3, tblDetail3, tbsDetail3, tbcDetail3, clsDefine.sDField2, clsDefine.sDHeader2, clsDefine.sDFormat2, clsDefine.sDWidth2)
        grdDetail3.dvGrid = tblDetail3
        grdDetail3.cFieldKey = "ma_phi"
        colma_phi3 = Grid.GetColumn(grdDetail3, "ma_phi")
        colten_phi3 = Grid.GetColumn(grdDetail3, "ten_phi")
        colma_dvt3 = Grid.GetColumn(grdDetail3, "ma_dvt")
        colten_dvt3 = Grid.GetColumn(grdDetail3, "ten_dvt")
        colgia3 = Grid.GetColumn(grdDetail3, "gia")
        coldien_giai3 = Grid.GetColumn(grdDetail3, "dien_giai")


        Dim s17 As New clsAutocl.DirLibObj.DirLib(txtma_loai, lblten_loai, conn, "dmloailog", "ma_loai", "ten_loai", "LogType", "1=1", False, cmdEdit)
        Dim s1 As New clsAutocl.DirLibObj.DirLib(txtma_kh, lblten_kh, conn, "dmkh", "ma_kh", "ten_kh", "Customer", "1=1", True, cmdEdit)
        Dim s9 As New clsAutocl.DirLibObj.DirLib(txtma_nv, lblten_nv, conn, "dmkh", "ma_kh", "ten_kh", "Customer", "1=1 and used_nv=1", True, cmdEdit)
        Dim s3 As New clsAutocl.DirLibObj.DirLib(txtma_noi_di, lblten_noi_di, conn, "dmkho", "ma_kho", "ten_kho", "Warehouse", "1=1", True, cmdEdit)
        Dim s5 As New clsAutocl.DirLibObj.DirLib(txtma_noi_den, lblten_noi_den, conn, "dmkho", "ma_kho", "ten_kho", "Warehouse", "1=1", True, cmdEdit)
        oMa_dr = New clsAutocl.DirLibObj.DirLib(txtma_noi_dr, lblten_noi_dr, conn, "dmkho", "ma_kho", "ten_kho", "Warehouse", "1=1", True, cmdEdit)
        Dim s10 As New clsAutocl.DirLibObj.DirLib(txtMa_hang, lblTen_hang, conn, "dmhangtau", "ma_hang", "ten_hang", "Carrier", "1=1", True, cmdEdit)


        oCong = New GridLib(colma_cong, "ten_cong", conn, conn, "dmcong", "ma_cong", "ten_cong", "Container", "1=1", tblDetail, New StatusBarPanel, True, cmdEdit)

        oPhi2 = New GridLib(colma_phi2, "ten_phi", conn, conn, "dmphi", "ma_phi", "ten_phi", "Cost", "1=1", tblDetail2, New StatusBarPanel, True, cmdEdit)
        oPhi3 = New GridLib(colma_phi3, "ten_phi", conn, conn, "dmphi", "ma_phi", "ten_phi", "Cost", "1=1", tblDetail3, New StatusBarPanel, True, cmdEdit)
        oDvt2 = New GridLib(colma_dvt2, "ten_dvt", conn, conn, "dmdvt", "ma_dvt", "ten_dvt", "Unit", "1=1", tblDetail2, New StatusBarPanel, False, cmdEdit)
        oDvt3 = New GridLib(colma_dvt3, "ten_dvt", conn, conn, "dmdvt", "ma_dvt", "ten_dvt", "Unit", "1=1", tblDetail3, New StatusBarPanel, False, cmdEdit)

        '' Import and export for n detail
        Dim mncItems As New ContextMenu
        Dim iAddItem As New MenuItem("Thêm chi tiết", AddressOf NewItem, Shortcut.F4)
        Dim iDeleteItem As New MenuItem("Xóa chi tiết", AddressOf DeleteItem, Shortcut.F8)

        ' BEGIN IMPORT/EXPORT
        grdDetail.CaptionText = "F4 - Thêm, F8 - Xóa, F10 - Tải tệp mẫu, F11 - Lấy dữ liệu, Shift + F5 - Copy Phiếu"
        Dim iExportTemplate As New MenuItem("Tệp mẫu", AddressOf ExportExcel, Shortcut.F10)
        Dim iImportTemplate As New MenuItem("Lấy dữ liệu", AddressOf ImportExcel, Shortcut.F11)

        mncItems.MenuItems.Add(iExportTemplate)
        mncItems.MenuItems.Add(iImportTemplate)
        ' END IMPORT/EXPORT


        mncItems.MenuItems.Add(iAddItem)
        mncItems.MenuItems.Add(iDeleteItem)

        grdDetail.ContextMenu = mncItems
        grdDetail2.ContextMenu = mncItems
        grdDetail3.ContextMenu = mncItems
        ''end im and ex n detal

        Voucher.ScatterMemvarBlank(Me)
        oVoucher.Action = "Start"
        isActive = False
        EDTBColumns()
        Dim alMaster As String = ""
        Tabs.ScatterMemvarBlankTabControl(oTab)
        Tabs.ReadOnlyTabControls(True, oTab)
        Tabs.SetMaxlength(oTab, clsDefine.cMTable, conn)

        Tabs.ScatterMemvarBlankTabControl(oTab1)
        Tabs.ReadOnlyTabControls(True, oTab1)
        Tabs.SetMaxlength(oTab1, clsDefine.cMTable, conn)
        Tabs.ScatterMemvarBlankTabControl(oTab2)
        Tabs.ReadOnlyTabControls(True, oTab2)
        Tabs.SetMaxlength(oTab2, clsDefine.cMTable, conn)

        Dim strFieldNumeric As String = "sl_n,sl_x,sl_t"
        For i = 0 To MaxColumns - 1
            If InStr(LCase(strFieldNumeric), tbcDetail(i).MappingName.ToLower) > 0 Then
                tbcDetail(i).NullText = "0"
            Else
                tbcDetail(i).NullText = ""
            End If
            If i <> 0 Then
                AddHandler tbcDetail(i).TextBox.Enter, AddressOf txt_Enter
            End If
        Next


        strFieldNumeric = "gia"
        For i = 0 To MaxColumns - 1
            If InStr(LCase(strFieldNumeric), tbcDetail2(i).MappingName.ToLower) > 0 Then
                tbcDetail2(i).NullText = "0"
            Else
                tbcDetail2(i).NullText = ""
            End If
            If i <> 0 Then
                AddHandler tbcDetail2(i).TextBox.Enter, AddressOf txt_Enter2
            End If
        Next

        For i = 0 To MaxColumns - 1
            If InStr(LCase(strFieldNumeric), tbcDetail3(i).MappingName.ToLower) > 0 Then
                tbcDetail3(i).NullText = "0"
            Else
                tbcDetail3(i).NullText = ""
            End If
            If i <> 0 Then
                AddHandler tbcDetail3(i).TextBox.Enter, AddressOf txt_Enter3
            End If
        Next

        AddHandler colma_cong.TextBox.Enter, AddressOf textbox_enter
        AddHandler colma_phi2.TextBox.Enter, AddressOf textbox_enter2
        AddHandler colma_phi3.TextBox.Enter, AddressOf textbox_enter3

        AddHandler colsl_n.TextBox.Validated, AddressOf txtsl_n_valid
        AddHandler colma_cong.TextBox.Validated, AddressOf txtma_cong_valid
        AddHandler colma_phi2.TextBox.Validated, AddressOf txtma_phi2_valid
        AddHandler colma_phi3.TextBox.Validated, AddressOf txtma_phi3_valid


    End Sub

    Private Sub frmMain_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        If Not isActive Then
            isActive = True
            InitRecords()
        End If
    End Sub


#End Region

#Region " Control Events "

    Private Sub tbDetail_Enter(ByVal sender As Object, ByVal e As System.EventArgs)
        grdDetail.Focus()
    End Sub

    Private Sub txt_Enter(ByVal sender As Object, ByVal e As System.EventArgs)
        If IsDBNull(tblDetail(grdDetail.CurrentRowIndex).Item("ma_cong")) Then
            sender.ReadOnly = True
        Else
            Dim cAccount As String
            cAccount = Trim(tblDetail(grdDetail.CurrentRowIndex).Item("ma_cong"))
            sender.ReadOnly = (cAccount = "")
        End If
    End Sub

    Private Sub txt_Enter2(ByVal sender As Object, ByVal e As System.EventArgs)
        If IsDBNull(tblDetail2(grdDetail2.CurrentRowIndex).Item("ma_phi")) Then
            sender.ReadOnly = True
        Else
            Dim cAccount As String
            cAccount = Trim(tblDetail2(grdDetail2.CurrentRowIndex).Item("ma_phi"))
            sender.ReadOnly = (cAccount = "")
        End If
    End Sub
    Private Sub txt_Enter3(ByVal sender As Object, ByVal e As System.EventArgs)
        If IsDBNull(tblDetail3(grdDetail3.CurrentRowIndex).Item("ma_phi")) Then
            sender.ReadOnly = True
        Else
            Dim cAccount As String
            cAccount = Trim(tblDetail3(grdDetail3.CurrentRowIndex).Item("ma_phi"))
            sender.ReadOnly = (cAccount = "")
        End If
    End Sub

    Private Sub grdMVCurrentCellChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim iRow As Integer
        iRow = sender.CurrentCell.RowNumber
        Dim strFilter = "stt_rec = '" + tblMaster(iRow).Item("stt_rec") + "'"
        tblDetail.RowFilter = strFilter
        tblDetail2.RowFilter = strFilter
        tblDetail3.RowFilter = strFilter
    End Sub

#End Region

#Region " Detail "

    Private Sub NewItem(ByVal sender As Object, ByVal e As System.EventArgs)
        If oVoucher.Action = "New" Or oVoucher.Action = "Edit" Then
            Dim nCount As Integer, iCurrIndex As Integer
            Select Case oTab.SelectedIndex
                Case 0
                    iCurrIndex = grdDetail.CurrentRowIndex
                    If iCurrIndex < 0 Then
                        tblDetail.AddNew()
                        grdDetail.CurrentCell = New DataGridCell(0, 0)
                        Return
                    End If
                    If Not IsDBNull(tblDetail(iCurrIndex).Item("stt_rec")) Then
                        If Not IsDBNull(tblDetail(iCurrIndex).Item("ma_cong")) Then
                            If Trim(tblDetail(iCurrIndex).Item("ma_cong")) <> "" Then
                                nCount = tblDetail.Count
                                grdDetail.CurrentCell = New DataGridCell(nCount, 0)
                            End If
                        End If
                    End If
                Case 1
                    iCurrIndex = grdDetail2.CurrentRowIndex
                    If iCurrIndex < 0 Then
                        tblDetail2.AddNew()
                        grdDetail2.CurrentCell = New DataGridCell(0, 0)
                        Return
                    End If
                    If Not IsDBNull(tblDetail2(iCurrIndex).Item("stt_rec")) Then
                        If Not IsDBNull(tblDetail2(iCurrIndex).Item("ma_phi")) Then
                            If Trim(tblDetail2(iCurrIndex).Item("ma_phi")) <> "" Then
                                nCount = tblDetail2.Count
                                grdDetail2.CurrentCell = New DataGridCell(nCount, 0)
                            End If
                        End If
                    End If
                Case 2
                    iCurrIndex = grdDetail2.CurrentRowIndex
                    If iCurrIndex < 0 Then
                        tblDetail3.AddNew()
                        grdDetail3.CurrentCell = New DataGridCell(0, 0)
                        Return
                    End If
                    If Not IsDBNull(tblDetail3(iCurrIndex).Item("stt_rec")) Then
                        If Not IsDBNull(tblDetail3(iCurrIndex).Item("ma_phi")) Then
                            If Trim(tblDetail3(iCurrIndex).Item("ma_phi")) <> "" Then
                                nCount = tblDetail3.Count
                                grdDetail3.CurrentCell = New DataGridCell(nCount, 0)
                            End If
                        End If
                    End If
            End Select
        End If
    End Sub

    Private Sub DeleteItem(ByVal sender As Object, ByVal e As System.EventArgs)
        If oVoucher.Action = "New" Or oVoucher.Action = "Edit" Then
            Dim iCurrIndex As Integer
            Select Case oTab.SelectedIndex
                Case 0
                    iCurrIndex = grdDetail.CurrentRowIndex
                    If iCurrIndex >= 0 And iCurrIndex < tblDetail.Count Then
                        If grdDetail.EndEdit(grdDetail.TableStyles(0).GridColumnStyles(grdDetail.CurrentCell.ColumnNumber), iCurrIndex, False) Then
                            Return
                        End If
                        If Msg.Question(AllText.VoucherDelete, 1, Me.Text) Then
                            grdDetail.Select(iCurrIndex)
                            Dim oRow As DataRowView
                            oRow = tblDetail(iCurrIndex)
                            oRow.Delete()
                        End If
                    End If
                Case 1
                    iCurrIndex = grdDetail2.CurrentRowIndex
                    If iCurrIndex >= 0 And iCurrIndex < tblDetail2.Count Then
                        If grdDetail2.EndEdit(grdDetail2.TableStyles(0).GridColumnStyles(grdDetail2.CurrentCell.ColumnNumber), iCurrIndex, False) Then
                            Return
                        End If
                        If Msg.Question(AllText.VoucherDelete, 1, Me.Text) Then
                            grdDetail2.Select(iCurrIndex)
                            Dim oRow As DataRowView
                            oRow = tblDetail2(iCurrIndex)
                            oRow.Delete()
                        End If
                    End If
                Case 2
                    iCurrIndex = grdDetail3.CurrentRowIndex
                    If iCurrIndex >= 0 And iCurrIndex < tblDetail3.Count Then
                        If grdDetail3.EndEdit(grdDetail3.TableStyles(0).GridColumnStyles(grdDetail3.CurrentCell.ColumnNumber), iCurrIndex, False) Then
                            Return
                        End If
                        If Msg.Question(AllText.VoucherDelete, 1, Me.Text) Then
                            grdDetail3.Select(iCurrIndex)
                            Dim oRow As DataRowView
                            oRow = tblDetail3(iCurrIndex)
                            oRow.Delete()
                        End If
                    End If
            End Select
        End If
    End Sub

#End Region

#Region " Functions "

    Public Sub InitRecords()

        Dim strSQL As String
        strSQL = String.Format("exec st_InitRecords_t1 '{0}','{1}','{2}'", cIDVoucher, Usersales.Trim, appLog)

        Dim ds As DataSet = Sql.GetDataSet(conn, strSQL)

        Voucher.AppendFrom(tblMaster, ds.Tables(0))
        Voucher.AppendFrom(tblDetail, ds.Tables(1))
        Voucher.AppendFrom(tblDetail2, ds.Tables(2))
        Voucher.AppendFrom(tblDetail3, ds.Tables(3))

        If tblMaster.Count > 0 Then
            iMasterRow = 0
            Dim strFilter = "stt_rec = '" + tblMaster(iMasterRow).Item("stt_rec") + "'"
            tblDetail.RowFilter = strFilter
            tblDetail2.RowFilter = strFilter
            tblDetail3.RowFilter = strFilter
            oVoucher.Action = "View"

            oVoucher.RefreshButton(oVoucher.Action)
            If tblMaster.Count <> 1 Then
                View()
            Else
                RefrehForm()
                EDTBColumns()
            End If

            If tblMaster.Count = 1 Then
                cmdEdit.Focus()
            End If
        Else
            iMasterRow = -1
            cmdNew.Focus()
        End If
        ds = Nothing
    End Sub

    Private Sub txtsl_n_valid(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oldValue As Decimal, newValue As Decimal, nsl_x As Decimal
        oldValue = -1
        newValue = Val(Replace(sender.Text, " ", ""))
        If newValue <> oldValue Then
            If Not IsDBNull(tblDetail(grdDetail.CurrentRowIndex).Item("sl_x")) Then
                nsl_x = tblDetail(grdDetail.CurrentRowIndex).Item("sl_x")
            Else
                nsl_x = 0
            End If

            With tblDetail(grdDetail.CurrentRowIndex)
                .Item("sl_n") = newValue
                .Item("sl_t") = .Item("sl_n") - nsl_x
            End With
        End If
        UpdateList()
    End Sub
    Private Sub txtma_cong_valid(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim newValue As String = Trim(sender.Text)
            If String.IsNullOrEmpty(newValue) Then
                Return
            End If
            If IsDuplicateStation(newValue, grdDetail.CurrentRowIndex) Then
                Msg.Alert("Mã Container " & RTrim(newValue) & " đã được nhập. Vui lòng kiểm tra lại.", 2, Me.Text)
                Return
            End If
            Dim iCurrIndex As Integer = grdDetail.CurrentRowIndex
            If IsDBNull(tblDetail(iCurrIndex).Item("sl_n")) Then
                tblDetail(iCurrIndex).Item("sl_n") = 0
            End If
            tblDetail(iCurrIndex).EndEdit()
            grdDetail.Refresh()
        Catch ex As Exception
            Msg.Alert("Lỗi....., txtma_phi2_valid:" + ex.Message)
        End Try
    End Sub

    Private Sub txtma_phi2_valid(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim newValue As String = Trim(sender.Text)
            If String.IsNullOrEmpty(newValue) Then
                Return
            End If
            If IsDuplicateCost(newValue, grdDetail2.CurrentRowIndex, tblDetail2) Then
                Msg.Alert("Mã phí " & RTrim(newValue) & " đã được nhập. Vui lòng kiểm tra lại.", 2, Me.Text)
                Return
            End If
            Dim iCurrIndex As Integer = grdDetail2.CurrentRowIndex
            If IsDBNull(tblDetail2(iCurrIndex).Item("ma_dvt")) Then
                tblDetail2(iCurrIndex).Item("ma_dvt") = "CONT"
            End If

            tblDetail2(iCurrIndex).EndEdit()
            grdDetail2.Refresh()
        Catch ex As Exception
            Msg.Alert("Lỗi....., txtma_phi2_valid:" + ex.Message)
        End Try
    End Sub

    Private Sub txtma_phi3_valid(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim newValue As String = Trim(sender.Text)
            If String.IsNullOrEmpty(newValue) Then
                Return
            End If
            If IsDuplicateCost(newValue, grdDetail3.CurrentRowIndex, tblDetail3) Then
                Msg.Alert("Mã phí " & RTrim(newValue) & " đã được nhập. Vui lòng kiểm tra lại.", 3, Me.Text)
                Return
            End If
            Dim iCurrIndex As Integer = grdDetail3.CurrentRowIndex
            If IsDBNull(tblDetail3(iCurrIndex).Item("ma_dvt")) Then
                tblDetail3(iCurrIndex).Item("ma_dvt") = "CONT"
            End If
            tblDetail3(iCurrIndex).EndEdit()
            grdDetail3.Refresh()
        Catch ex As Exception
            Msg.Alert("Lỗi....., txtma_phi3_valid:" + ex.Message)
        End Try
    End Sub
    Private Function IsDuplicateStation(ByVal ma_cong As String, ByVal currentIndex As Integer) As Boolean
        ' Kiem tra nhap ma co trung hay khong
        Dim ma_cong_i As String
        Dim rowI As Integer

        For rowI = 0 To tblDetail.Count - 2
            If rowI <> currentIndex Then
                ma_cong_i = RTrim(tblDetail(rowI).Item("ma_cong"))
                If RTrim(ma_cong_i) = RTrim(ma_cong) Then
                    Return True
                End If
            End If
        Next
        Return False
    End Function

    Private Function IsDuplicateCost(ByVal ma_phi As String, ByVal currentIndex As Integer, tbl As DataView) As Boolean
        ' Kiem tra nhap ma co trung hay khong
        Dim ma_phi_i As String
        Dim rowI As Integer

        For rowI = 0 To tbl.Count - 2
            If rowI <> currentIndex Then
                ma_phi_i = RTrim(tbl(rowI).Item("ma_phi"))
                If RTrim(ma_phi_i) = RTrim(ma_phi) Then
                    Return True
                End If
            End If
        Next
        Return False
    End Function
    Private Sub textbox_enter(ByVal sender As Object, ByVal e As System.EventArgs)
        SetEmptyColKey(sender, e)
        sender.CharacterCasing = CharacterCasing.Upper
    End Sub
    Private Sub SetEmptyColKey(ByVal sender As Object, ByVal e As System.EventArgs)
        If Not oCong.Cancel Then
            cOldItem = Trim(sender.Text)
            Dim iCurrIndex2 As Integer
            iCurrIndex2 = grdDetail.CurrentRowIndex
            If oVoucher.Action = "New" And IsDBNull(tblDetail(iCurrIndex2).Item("stt_rec")) Then
                tblDetail(grdDetail.CurrentRowIndex).Item("stt_rec") = ""
                WhenAddNewItem()
            End If
            If oVoucher.Action = "Edit" And IsDBNull(tblDetail(iCurrIndex2).Item("stt_rec")) Then
                tblDetail(grdDetail.CurrentRowIndex).Item("stt_rec") = tblMaster(iMasterRow).Item("stt_rec")
                WhenAddNewItem()
            End If
        End If
    End Sub
    Private Sub WhenAddNewItem()
        tblDetail(grdDetail.CurrentRowIndex).Item("stt_rec0") = Voucher.GetIDItem(tblDetail, "0")
    End Sub


    Private Sub textbox_enter2(ByVal sender As Object, ByVal e As System.EventArgs)
        SetEmptyColKey2(sender, e)
        sender.CharacterCasing = CharacterCasing.Upper
    End Sub
    Private Sub SetEmptyColKey2(ByVal sender As Object, ByVal e As System.EventArgs)
        If Not oPhi2.Cancel Then
            cOldPhi2 = Trim(sender.Text)
            Dim iCurrIndex2 As Integer
            iCurrIndex2 = grdDetail2.CurrentRowIndex
            If oVoucher.Action = "New" And IsDBNull(tblDetail2(iCurrIndex2).Item("stt_rec")) Then
                tblDetail2(grdDetail2.CurrentRowIndex).Item("stt_rec") = ""
                WhenAddNewItem2()
            End If
            If oVoucher.Action = "Edit" And IsDBNull(tblDetail2(iCurrIndex2).Item("stt_rec")) Then
                tblDetail2(grdDetail2.CurrentRowIndex).Item("stt_rec") = tblMaster(iMasterRow).Item("stt_rec")
                WhenAddNewItem2()
            End If
        End If
    End Sub
    Private Sub WhenAddNewItem2()
        tblDetail2(grdDetail2.CurrentRowIndex).Item("stt_rec0") = Voucher.GetIDItem(tblDetail2, "0")
    End Sub

    Private Sub textbox_enter3(ByVal sender As Object, ByVal e As System.EventArgs)
        SetEmptyColKey3(sender, e)
        sender.CharacterCasing = CharacterCasing.Upper
    End Sub
    Private Sub SetEmptyColKey3(ByVal sender As Object, ByVal e As System.EventArgs)
        If Not oPhi3.Cancel Then
            cOldPhi3 = Trim(sender.Text)
            Dim iCurrIndex3 As Integer
            iCurrIndex3 = grdDetail3.CurrentRowIndex
            If oVoucher.Action = "New" And IsDBNull(tblDetail3(iCurrIndex3).Item("stt_rec")) Then
                tblDetail3(grdDetail3.CurrentRowIndex).Item("stt_rec") = ""
                WhenAddNewItem3()
            End If
            If oVoucher.Action = "Edit" And IsDBNull(tblDetail3(iCurrIndex3).Item("stt_rec")) Then
                tblDetail3(grdDetail3.CurrentRowIndex).Item("stt_rec") = tblMaster(iMasterRow).Item("stt_rec")
                WhenAddNewItem3()
            End If
        End If
    End Sub
    Private Sub WhenAddNewItem3()
        tblDetail3(grdDetail3.CurrentRowIndex).Item("stt_rec0") = Voucher.GetIDItem(tblDetail3, "0")
    End Sub
    Public Sub UpdateList()
        Try
            If oVoucher Is Nothing Then Return
            If oVoucher.Action Is Nothing Then Return

            Dim nT_sl_n, nT_sl_x, nT_sl_t As Decimal, i As Integer
            nT_sl_n = 0 : nT_sl_x = 0 : nT_sl_t = 0
            If oVoucher.Action = "New" Or oVoucher.Action = "Edit" Or oVoucher.Action = "View" Then
                For i = 0 To tblDetail.Count - 1
                    If Not IsDBNull(tblDetail(i).Item("sl_n")) Then
                        nT_sl_n = nT_sl_n + tblDetail(i).Item("sl_n")
                    End If

                    If Not IsDBNull(tblDetail(i).Item("sl_x")) Then
                        nT_sl_x = nT_sl_x + tblDetail(i).Item("sl_x")
                    End If
                    If Not IsDBNull(tblDetail(i).Item("sl_t")) Then
                        nT_sl_t = nT_sl_t + tblDetail(i).Item("sl_t")
                    End If
                Next

            End If
            txtT_sl_n.Value = nT_sl_n
            txtT_sl_x.Value = nT_sl_x
            txtT_sl_t.Value = nT_sl_t

        Catch ex As Exception
            Msg.Alert("Lỗi...., UpdateList: " + ex.Message)
        End Try
    End Sub

    Private Sub EDTBColumns()
        Dim i As Integer
        Try
            For i = 0 To MaxColumns - 1
                tbcDetail(i).TextBox.Enabled = (oVoucher.Action = "New" Or oVoucher.Action = "Edit")

            Next
        Catch ex As Exception
            Msg.Alert("Lỗi....EDTBColumns1:" + ex.Message)
        End Try

        Try
            colten_cong.TextBox.Enabled = False
            colten_phi2.TextBox.Enabled = False
            colten_phi3.TextBox.Enabled = False
            colsl_x.TextBox.Enabled = False
            colsl_t.TextBox.Enabled = False
        Catch ex2 As Exception
            Msg.Alert("Lỗi....EDTBColumns2:" + ex2.Message)
        End Try
    End Sub

    Public Sub RefrehForm()
        Voucher.ScatterMemvar(tblMaster(iMasterRow), Me)
        Dim strFilter = "stt_rec = '" + tblMaster(iMasterRow).Item("stt_rec") + "'"
        tblDetail.RowFilter = strFilter
        tblDetail2.RowFilter = strFilter
        tblDetail3.RowFilter = strFilter
        cmdNew.Focus()
        EnabledButtonsOnLoad()
        Tabs.ScatterTabControl(tblMaster(iMasterRow), oTab)
        Tabs.ScatterTabControl(tblMaster(iMasterRow), oTab1)
        Tabs.ScatterTabControl(tblMaster(iMasterRow), oTab2)
    End Sub

#Region " Process "

    Public Sub AddNew()
        Dim strFilter = "stt_rec is null or stt_rec = ''"
        tblMaster.AllowNew = True
        tblDetail.AllowNew = True
        tblDetail.AllowDelete = True
        tblDetail.AddNew()

        tblDetail2.AllowNew = True
        tblDetail2.AllowDelete = True
        tblDetail2.AddNew()

        tblDetail3.AllowNew = True
        tblDetail3.AllowDelete = True
        tblDetail3.AddNew()

        tblDetail.RowFilter = strFilter
        tblDetail2.RowFilter = strFilter
        tblDetail3.RowFilter = strFilter


        Voucher.ScatterMemvarBlankWithDefault(Me)

        Tabs.ReadOnlyTabControls(False, oTab)
        Tabs.ScatterMemvarBlankTabControl(oTab)
        Tabs.ReadOnlyTabControls(False, oTab1)
        Tabs.ScatterMemvarBlankTabControl(oTab1)
        Tabs.ReadOnlyTabControls(False, oTab2)
        Tabs.ScatterMemvarBlankTabControl(oTab2)

        txtNgay_ct.Value = Now.Date
        txtngay_dk.Value = Now.Date
        ' txtgio_dk.Text = Microsoft.VisualBasic.Right(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), 8)
        'txtso_ct.Text = "WIN-F-" + Date.Now.ToString("yyMMdd")
        'txtma_loai.Text = "WINL"

        txtso_ct.Text = Sql.GetValue(conn, String.Format("SELECT dbo.getVoucherNumber_t1('{0}','{1}')", txtma_loai.Text, txtNgay_ct.Value.ToString("yyyyMMdd")))

        cOldIDNumber = cIDNumber
        iOldMasterRow = iMasterRow
        cOldSo_ct = ""
        EDTBColumns()
        insertdetail()
        txtma_kh.Text = ""
        txtDien_giai.Text = ""
        txtma_kh.Focus()
    End Sub
    Public Sub Edit() ' Sua
        oldtblDetail = Voucher.Copy2Table(tblDetail)
        oldtblDetail2 = Voucher.Copy2Table(tblDetail2)
        oldtblDetail3 = Voucher.Copy2Table(tblDetail3)

        tblMaster.AllowNew = True
        tblDetail.AllowNew = True
        tblDetail.AllowDelete = True
        tblDetail2.AllowNew = True
        tblDetail2.AllowDelete = True
        tblDetail3.AllowNew = True
        tblDetail3.AllowDelete = True

        If tblMaster.Table.Rows.Count = 0 Then
            iMasterRow = -1
        Else
            'iMasterRow = 0
            datetime2 = CType(tblMaster(iMasterRow).Item("datetime2"), DateTime).ToString("yyyyMMddHH:mm:ss")
        End If

        Tabs.ReadOnlyTabControls(False, oTab)
        Tabs.ReadOnlyTabControls(False, oTab1)
        Tabs.ReadOnlyTabControls(False, oTab2)
        iOldMasterRow = iMasterRow
        cOldSo_ct = txtso_ct.Text.Trim

        EDTBColumns()

        txtma_kh.Focus()
    End Sub

    Public Sub Save()  ' Luu
        Dim command As String = "", val As String = ""

        If Not Functions.CheckDate(txtNgay_ct) Then
            Msg.Alert(AllText.VoucherDateValid, 3)
            Voucher.isContinue = False
            Return
        End If

        If oVoucher.Action = "Edit" Then
            command = "select 1 from " + clsDefine.cMTable + " where stt_rec = '" + tblMaster(iMasterRow).Item("stt_rec") + "' and convert(varchar(10), datetime2, 112) + convert(varchar(10), datetime2, 108) = '" + datetime2 + "'"
            If String.IsNullOrEmpty(Sql.GetValue(conn, command)) Then
                Msg.Alert("Phiếu đã được sửa hoặc xóa bởi người khác.", 3)
                Voucher.isContinue = False
                Return
            End If
        End If

        Try
            grdDetail.CurrentCell = New DataGridCell(0, 0)
            grdDetail2.CurrentCell = New DataGridCell(0, 0)
            grdDetail3.CurrentCell = New DataGridCell(0, 0)
        Catch
        End Try

        Dim i As Integer = 0, nCount As Integer = 0, strSQL As String = "", rowJ, rowI As Integer

        nCount = tblDetail.Count - 1
        For i = nCount To 0 Step -1
            If Not IsDBNull(tblDetail(i).Item("ma_cong")) Then
                If Trim(tblDetail(i).Item("ma_cong")) = "" Then
                    tblDetail(i).Delete()
                End If
            Else
                tblDetail(i).Delete()
            End If
        Next

        Try
            nCount = tblDetail.Count - 1
            For i = nCount To 0 Step -1
                If tblDetail(i).Item("sl_n") = 0 Then
                    tblDetail(i).Delete()
                End If
            Next
        Catch ex As Exception
        End Try

        nCount = tblDetail2.Count - 1
        For i = nCount To 0 Step -1
            If Not IsDBNull(tblDetail2(i).Item("ma_phi")) Then
                If Trim(tblDetail2(i).Item("ma_phi")) = "" Then
                    tblDetail2(i).Delete()
                End If
            Else
                tblDetail2(i).Delete()
            End If
        Next

        nCount = tblDetail3.Count - 1
        For i = nCount To 0 Step -1
            If Not IsDBNull(tblDetail3(i).Item("ma_phi")) Then
                If Trim(tblDetail3(i).Item("ma_phi")) = "" Then
                    tblDetail3(i).Delete()
                End If
            Else
                tblDetail3(i).Delete()
            End If
        Next

        ' Kiem tra da nhap chi tiet hay chua
        nCount = 0
        For i = 0 To tblDetail.Count - 1
            If Not IsDBNull(tblDetail(i).Item("ma_cong")) Then
                If Trim(tblDetail(i).Item("ma_cong")) <> "" Then
                    nCount = 1
                    Exit For
                End If
            End If
        Next
        If nCount = 0 Then
            Msg.Alert(AllText.VoucherNotDetail, 2, Me.Text)
            Voucher.isContinue = False
            Return
        End If

        Dim ma_cong_i As String = ""
        Dim ma_cong_j As String = ""
        nCount = 0
        For rowI = 0 To tblDetail.Count - 2
            For rowJ = rowI + 1 To tblDetail.Count - 1
                ma_cong_i = Trim(tblDetail(rowI).Item("ma_cong"))
                ma_cong_j = Trim(tblDetail(rowJ).Item("ma_cong"))
                If ma_cong_i = ma_cong_j Then
                    nCount = 1
                    Exit For
                End If
            Next
            If nCount = 1 Then
                Exit For
            End If
        Next

        If nCount = 1 Then
            ' Kiem tra san pham nhap trung nhau
            Msg.Alert("Mã container " & ma_cong_i & " tại dòng " & rowJ + 1 & " đã được nhập tại dòng " & rowI + 1, 3, Me.Text)
            Voucher.isContinue = False
            Return
        End If



        'Kiểm tra phí trung
        Dim ma_phi_i As String = ""
        Dim ma_phi_j As String = ""
        nCount = 0
        For rowI = 0 To tblDetail2.Count - 2
            For rowJ = rowI + 1 To tblDetail2.Count - 1
                ma_phi_i = Trim(tblDetail2(rowI).Item("ma_phi"))
                ma_phi_j = Trim(tblDetail2(rowJ).Item("ma_phi"))
                If ma_phi_i = ma_phi_j Then
                    nCount = 1
                    Exit For
                End If
            Next
            If nCount = 1 Then
                Exit For
            End If
        Next

        If nCount = 1 Then
            ' Kiem tra san pham nhap trung nhau
            Msg.Alert("Mã Phí " & ma_phi_i & " tại dòng " & rowJ + 1 & " đã được nhập tại dòng " & rowI + 1, 3, Me.Text)
            Voucher.isContinue = False
            Return
        End If


        nCount = 0
        For rowI = 0 To tblDetail3.Count - 3
            For rowJ = rowI + 1 To tblDetail3.Count - 1
                ma_phi_i = Trim(tblDetail3(rowI).Item("ma_phi"))
                ma_phi_j = Trim(tblDetail3(rowJ).Item("ma_phi"))
                If ma_phi_i = ma_phi_j Then
                    nCount = 1
                    Exit For
                End If
            Next
            If nCount = 1 Then
                Exit For
            End If
        Next

        If nCount = 1 Then
            ' Kiem tra san pham nhap trung nhau
            Msg.Alert("Mã Phí " & ma_phi_i & " tại dòng " & rowJ + 1 & " đã được nhập tại dòng " & rowI + 1, 3, Me.Text)
            Voucher.isContinue = False
            Return
        End If


        ' Kiem tra trường hợp thay đổi số lương
        If oVoucher.Action = "Edit" Then
            nCount = 0

            For i = 0 To tblDetail.Count - 1
                ma_cong_i = Trim(tblDetail(i).Item("ma_cong"))
                If Not IsDBNull(tblDetail(i).Item("ma_cong")) Then
                    If tblDetail(i).Item("sl_n") < tblDetail(i).Item("sl_x") Then
                        nCount = 1
                        Exit For
                    End If
                End If
            Next
            If nCount = 1 Then
                Msg.Alert("Mã container " & ma_cong_i & " tại dòng " & i + 1 & " số lượng container không đúng. ", 3, Me.Text)
                Voucher.isContinue = False
                Return
            End If
        End If
        grdDetail.Update()
        grdDetail2.Update()
        grdDetail3.Update()

        ' Gan stt_rec, stt_rec0 bang trong neu la Null
        Dim strFields As String = "stt_rec,stt_rec0", cF As String, j As Integer
        For i = 0 To tblDetail.Count - 1
            For j = 1 To Str.GetWordCount(strFields, ",")
                cF = Trim(Str.GetWordNum(strFields, j, ","))
                If IsDBNull(tblDetail(i).Item(cF)) Then
                    tblDetail(i).Item(cF) = ""
                End If
            Next
        Next

        For i = 0 To tblDetail2.Count - 1
            For j = 1 To Str.GetWordCount(strFields, ",")
                cF = Trim(Str.GetWordNum(strFields, j, ","))
                If IsDBNull(tblDetail2(i).Item(cF)) Then
                    tblDetail2(i).Item(cF) = ""
                End If
            Next
        Next

        For i = 0 To tblDetail3.Count - 1
            For j = 1 To Str.GetWordCount(strFields, ",")
                cF = Trim(Str.GetWordNum(strFields, j, ","))
                If IsDBNull(tblDetail3(i).Item(cF)) Then
                    tblDetail3(i).Item(cF) = ""
                End If
            Next
        Next

        If oVoucher.Action = "New" Then
            cIDNumber = ""
        Else
            cIDNumber = tblMaster(iMasterRow).Item("stt_rec")
        End If

        command = String.Format("if exists(select 1 from " + clsDefine.cMTable + " where rtrim(ma_ct) + rtrim(so_ct) = '{0}' + '{1}' " + IIf(oVoucher.Action = "New", "", "and stt_rec <> '" + cIDNumber.Trim + "'") + ") select 1 else select 0", txtma_loai.Text.Trim, txtso_ct.Text.Trim)
        val = Sql.GetValue(conn, command)
        If val = "1" Then
            If Msg.Question("Mã chứng từ " & txtso_ct.Text.Trim & " đã có sẵn trên hệ thống hoặc đã được xóa. Chương trình sẽ tự động tạo mã mới ?", 1, Me.Text) Then
                txtso_ct.Text = Sql.GetValue(conn, String.Format("SELECT dbo.getVoucherNumber_t1('{0}','{1}')", txtma_loai.Text, txtNgay_ct.Value.ToString("yyyyMMdd")))
            Else
                Voucher.isContinue = False
                Return
            End If
        End If

        UpdateList()

        If oVoucher.Action = "New" Then
            cIDNumber = Voucher.GetIdentityNumber(conn, clsDefine.cMTable, clsDefine.Ws_Id)
            tblMaster.AddNew()
            iMasterRow = tblMaster.Count - 1
            tblMaster(iMasterRow).Item("stt_rec") = cIDNumber
        Else
            cIDNumber = tblMaster(iMasterRow).Item("stt_rec")
        End If
        ' Set Datetime, User
        Try
            Functions.UserAndDatetime(conn, tblMaster(iMasterRow), oVoucher.Action, UserID)
            Voucher.GatherMemvar(tblMaster(iMasterRow), Me)
        Catch ex As Exception
            Msg.Alert("Không kết nối được dữ liệu, vui lòng thử lại.", 3)
            Voucher.isContinue = False
            Return
        End Try

        Tabs.GatherMemvarTabControl(tblMaster(iMasterRow), oTab)
        Tabs.GatherMemvarTabControl(tblMaster(iMasterRow), oTab1)
        Tabs.GatherMemvarTabControl(tblMaster(iMasterRow), oTab2)

        If oVoucher.Action = "New" Then
            strSQL = Voucher.GenSQLInsert(conn, Trim(clsDefine.cMTable), tblMaster(iMasterRow).Row)
        Else
            Dim strKey As String
            strKey = "stt_rec = '" + tblMaster(iMasterRow).Item("stt_rec") + "'"
            strSQL = Voucher.GenSQLUpdate(conn, Trim(clsDefine.cMTable), tblMaster(iMasterRow).Row, strKey)
            strSQL = strSQL + ChrW(13) + Voucher.GenSQLDelete(Trim(clsDefine.cDTable), strKey)
            strSQL = strSQL + ChrW(13) + Voucher.GenSQLDelete(Trim(clsDefine.cDTable2), strKey)
            strSQL = strSQL + ChrW(13) + Voucher.GenSQLDelete(Trim(clsDefine.cDTable3), strKey)
        End If

        '  tblMaster(iMasterRow).Item("so_ct") = Str.PadL(Trim(tblMaster(iMasterRow).Item("so_ct")), txtso_ct.MaxLength)
        tblMaster(iMasterRow).EndEdit()

        ' Gan cac gia tri cua cac truong trong ct nhu ph
        strFields = "ngay_ct, so_ct, stt_rec"
        Dim strFilter As String
        strFilter = "stt_rec = '" + cIDNumber + "' or stt_rec = ''"

        tblDetail.RowFilter = strFilter
        nCount = tblDetail.Count - 1
        Dim nE As Integer
        nE = 0
        For i = 0 To nCount
            If tblDetail(i).Item("stt_rec") = IIf(oVoucher.Action = "New", "", tblMaster(iMasterRow).Item("stt_rec")) Then
                For j = 1 To Str.GetWordCount(strFields, ",")
                    cF = Trim(Str.GetWordNum(strFields, j, ","))
                    tblDetail(i).Item(cF) = tblMaster(iMasterRow).Item(cF)
                Next j
                nE = nE + 1
                tblDetail(i).Item("stt_rec0") = Format(nE, "000").ToString
                tblDetail(i).Item("line_nbr") = nE
                tblDetail(i).EndEdit()
                grdDetail.Update()
                strSQL = strSQL + ChrW(13) + Voucher.GenSQLInsert(conn, clsDefine.cDTable, tblDetail(i).Row)
            End If
            tblDetail(i).Item("stt_rec") = tblMaster(iMasterRow).Item("stt_rec").ToString.Trim
        Next i


        tblDetail2.RowFilter = strFilter
        nCount = tblDetail2.Count - 1
        nE = 0
        For i = 0 To nCount
            If tblDetail2(i).Item("stt_rec") = IIf(oVoucher.Action = "New", "", tblMaster(iMasterRow).Item("stt_rec")) Then
                For j = 1 To Str.GetWordCount(strFields, ",")
                    cF = Trim(Str.GetWordNum(strFields, j, ","))
                    tblDetail2(i).Item(cF) = tblMaster(iMasterRow).Item(cF)
                Next j
                nE = nE + 1
                tblDetail2(i).Item("stt_rec0") = Format(nE, "000").ToString
                tblDetail2(i).Item("line_nbr") = nE
                tblDetail2(i).EndEdit()
                grdDetail2.Update()
                strSQL = strSQL + ChrW(13) + Voucher.GenSQLInsert(conn, clsDefine.cDTable2, tblDetail2(i).Row)
            End If
            tblDetail2(i).Item("stt_rec") = tblMaster(iMasterRow).Item("stt_rec").ToString.Trim
        Next i

        tblDetail3.RowFilter = strFilter
        nCount = tblDetail3.Count - 1
        nE = 0
        For i = 0 To nCount
            If tblDetail3(i).Item("stt_rec") = IIf(oVoucher.Action = "New", "", tblMaster(iMasterRow).Item("stt_rec")) Then
                For j = 1 To Str.GetWordCount(strFields, ",")
                    cF = Trim(Str.GetWordNum(strFields, j, ","))
                    tblDetail3(i).Item(cF) = tblMaster(iMasterRow).Item(cF)
                Next j
                nE = nE + 1
                tblDetail3(i).Item("stt_rec0") = Format(nE, "000").ToString
                tblDetail3(i).Item("line_nbr") = nE
                tblDetail3(i).EndEdit()
                grdDetail3.Update()
                strSQL = strSQL + ChrW(13) + Voucher.GenSQLInsert(conn, clsDefine.cDTable3, tblDetail3(i).Row)
            End If
            tblDetail3(i).Item("stt_rec") = tblMaster(iMasterRow).Item("stt_rec").ToString.Trim
        Next i


        If oVoucher.Action <> "New" Then
            strSQL = strSQL + String.Format(" exec [dbo].st_TinhTonCong '{0}'", tblMaster(iMasterRow).Item("stt_rec"))
        End If
        Try
            Sql.Execute(conn, strSQL, True)
        Catch ex As Exception
            Msg.Alert("Có lỗi xảy ra, không lưu được, vui lòng kiểm tra và thử lại.", 3)
            If oVoucher.Action = "New" Then
                Dim strKey As String
                strKey = "stt_rec = '" + tblMaster(iMasterRow).Item("stt_rec") + "'"
                cIDNumber = tblMaster(iMasterRow).Item("stt_rec")
                ' Xoa trong grid
                nCount = tblDetail.Count - 1
                For i = nCount To 0 Step -1
                    If Not IsDBNull(tblDetail(i).Item("stt_rec")) Then
                        If Trim(tblDetail(i).Item("stt_rec")) = tblMaster(iMasterRow).Item("stt_rec") Then
                            tblDetail(i).Item("stt_rec") = ""
                        End If
                    Else
                        tblDetail(i).Delete()
                    End If
                Next

                nCount = tblDetail2.Count - 1
                For i = nCount To 0 Step -1
                    If Not IsDBNull(tblDetail2(i).Item("stt_rec")) Then
                        If Trim(tblDetail2(i).Item("stt_rec")) = tblMaster(iMasterRow).Item("stt_rec") Then
                            tblDetail2(i).Item("stt_rec") = ""
                        End If
                    Else
                        tblDetail2(i).Delete()
                    End If
                Next

                nCount = tblDetail3.Count - 1
                For i = nCount To 0 Step -1
                    If Not IsDBNull(tblDetail3(i).Item("stt_rec")) Then
                        If Trim(tblDetail3(i).Item("stt_rec")) = tblMaster(iMasterRow).Item("stt_rec") Then
                            tblDetail3(i).Item("stt_rec") = ""
                        End If
                    Else
                        tblDetail3(i).Delete()
                    End If
                Next


            End If
            Voucher.isContinue = False
            Return
        End Try

        datetime2 = CType(tblMaster(iMasterRow).Item("datetime2"), DateTime).ToString("yyyyMMddHH:mm:ss")

        Tabs.ReadOnlyTabControls(True, oTab)
        Tabs.ReadOnlyTabControls(True, oTab1)
        Tabs.ReadOnlyTabControls(True, oTab2)
        EDTBColumns()

    End Sub

    Public Sub Delete() ' Xoa
        Dim strSQL As String = "", i As Integer, nCount As Integer, strKey As String
        strKey = "stt_rec = '" + tblMaster(iMasterRow).Item("stt_rec") + "'"
        cIDNumber = tblMaster(iMasterRow).Item("stt_rec")
        ' Xoa trong grid
        nCount = tblDetail.Count - 1
        For i = nCount To 0 Step -1
            If Not IsDBNull(tblDetail(i).Item("stt_rec")) Then
                If Trim(tblDetail(i).Item("stt_rec")) = tblMaster(iMasterRow).Item("stt_rec") Then
                    tblDetail(i).Delete()
                End If
            Else
                tblDetail(i).Delete()
            End If
        Next

        nCount = tblDetail2.Count - 1
        For i = nCount To 0 Step -1
            If Not IsDBNull(tblDetail2(i).Item("stt_rec")) Then
                If Trim(tblDetail2(i).Item("stt_rec")) = tblMaster(iMasterRow).Item("stt_rec") Then
                    tblDetail2(i).Delete()
                End If
            Else
                tblDetail2(i).Delete()
            End If
        Next

        nCount = tblDetail3.Count - 1
        For i = nCount To 0 Step -1
            If Not IsDBNull(tblDetail3(i).Item("stt_rec")) Then
                If Trim(tblDetail3(i).Item("stt_rec")) = tblMaster(iMasterRow).Item("stt_rec") Then
                    tblDetail3(i).Delete()
                End If
            Else
                tblDetail3(i).Delete()
            End If
        Next

        strSQL = "update " + clsDefine.cMTable + " set status = '*', datetime2 = getdate(), user_id2 = " + UserID.ToString + " where " + strKey

        Try
            Sql.Execute(conn, strSQL, True)
        Catch ex As Exception
            Msg.Alert("Có lỗi xảy ra, không xóa được, vui lòng thử lại.", 3)
            Voucher.isContinue = False
            Return
        End Try

        ' Xoa trong Master
        tblMaster(iMasterRow).Delete()
        If iMasterRow > 0 Then
            iMasterRow = iMasterRow - 1
        Else
            If tblMaster.Count = 0 Then
                iMasterRow = -1
            End If
        End If
        If iMasterRow = -1 Then
            Voucher.ScatterMemvarBlank(Me)
            oVoucher.Action = "Start"
            Dim strFilter = "stt_rec = ''"
            tblDetail.RowFilter = strFilter
            tblDetail2.RowFilter = strFilter
            tblDetail3.RowFilter = strFilter
        Else
            oVoucher.Action = "View"
            RefrehForm()
        End If
        'Sql.Execute(conn, strSQL, True)
    End Sub

    Public Sub View() ' Xem
        Dim frmView As New Form, pn As StatusBarPanel, grdMV As New clsGrid.clsGrid, grdDV As New clsGrid.clsGrid
        Dim tbsmv As New DataGridTableStyle, tbsdv As New DataGridTableStyle
        Dim tbclv(MaxColumns) As DataGridTextBoxColumn, i As Integer
        For i = 0 To MaxColumns - 1
            tbclv(i) = New DataGridTextBoxColumn
            If InStr(tbcDetail(i).Format, "0") > 0 Then
                tbclv(i).NullText = 0
            Else
                tbclv(i).NullText = ""
            End If
        Next
        With frmView
            .Top = 0
            .Left = 0
            .Width = Me.Width
            .Height = Me.Height
            .Text = Me.Text
            .StartPosition = FormStartPosition.CenterParent
            pn = Voucher.AddStb(frmView, Username)
        End With
        With grdMV
            .CaptionVisible = False
            .ReadOnly = True
            .Top = 0
            .Left = 0
            .Height = (Me.Height - SystemInformation.CaptionHeight) / 2
            .Width = Me.Width - 15
            .Anchor = AnchorStyles.Top + AnchorStyles.Left + AnchorStyles.Right + AnchorStyles.Bottom
            .BackgroundColor = Color.White
        End With

        With grdDV
            .CaptionVisible = False
            .ReadOnly = True
            .Top = (Me.Height - SystemInformation.CaptionHeight) / 2
            .Left = 0
            .Height = (Me.Height - SystemInformation.CaptionHeight) / 2 - 36
            .Width = Me.Width - 15
            .Anchor = AnchorStyles.Bottom + AnchorStyles.Left + AnchorStyles.Right
            .BackgroundColor = Color.White
        End With

        Dim cmdClose As New Button
        cmdClose.Visible = True
        cmdClose.Anchor = AnchorStyles.Left + AnchorStyles.Top
        cmdClose.Left = -100 - cmdClose.Width
        frmView.Controls.Add(cmdClose)
        frmView.CancelButton = cmdClose
        frmView.Controls.Add(grdMV)
        frmView.Controls.Add(grdDV)

        Grid.Fill(grdMV, tblMaster, tbsmv, tbclv, clsDefine.sMField, clsDefine.sMHeader, clsDefine.sMFormat, clsDefine.sMWidth)
        tbclv(2).Alignment = HorizontalAlignment.Center
        Grid.Fill(grdDV, tblDetail, tbsdv, tbclv, clsDefine.sDField, clsDefine.sDHeader, clsDefine.sDFormat, clsDefine.sDWidth)

        Dim msgText As String, nCount As Integer = tblMaster.Count
        msgText = "Số phiếu: " + nCount.ToString.Trim
        pn.Text = msgText
        AddHandler grdMV.CurrentCellChanged, AddressOf grdMVCurrentCellChanged
        grdMV.CurrentRowIndex = iMasterRow
        frmView.Text = Me.Text
        frmView.Icon = Me.Icon
        frmView.ShowDialog()
        frmView.Dispose()
        iMasterRow = grdMV.CurrentRowIndex
        RefrehForm()
        EDTBColumns()
    End Sub

    Public Sub Search()
        Dim fSearch As New frmSearch
        fSearch.ShowDialog()
    End Sub

    Public Sub Print() ' In
        If oVoucher.Action = "New" Or oVoucher.Action = "Edit" Then
            Msg.Alert("Vui lòng lưu phiếu trước khi in.")
            Return
        End If
        cmsExport.Show(PointToScreen(cmdPrint.Location))
    End Sub


    Public Sub Cancel() ' Huy
        Dim i As Integer, nCount As Integer
        i = grdDetail.CurrentRowIndex
        If i >= 0 Then
            grdDetail.Select(i)
        End If

        If oVoucher.Action = "New" Then
            ' Xoa trong grid
            nCount = tblDetail.Count - 1
            For i = nCount To 0 Step -1
                If Not IsDBNull(tblDetail(i).Item("stt_rec")) Then
                    If Trim(tblDetail(i).Item("stt_rec")) = "" Then
                        tblDetail(i).Delete()
                    End If
                Else
                    tblDetail(i).Delete()
                End If
            Next

            nCount = tblDetail2.Count - 1
            For i = nCount To 0 Step -1
                If Not IsDBNull(tblDetail2(i).Item("stt_rec")) Then
                    If Trim(tblDetail2(i).Item("stt_rec")) = "" Then
                        tblDetail2(i).Delete()
                    End If
                Else
                    tblDetail2(i).Delete()
                End If
            Next

            nCount = tblDetail3.Count - 1
            For i = nCount To 0 Step -1
                If Not IsDBNull(tblDetail3(i).Item("stt_rec")) Then
                    If Trim(tblDetail3(i).Item("stt_rec")) = "" Then
                        tblDetail3(i).Delete()
                    End If
                Else
                    tblDetail3(i).Delete()
                End If
            Next


            ' Scatter memvar
            If iOldMasterRow = -1 Then
                oVoucher.Action = "Start"
                Voucher.ScatterMemvarBlank(Me)
                Dim strFilter = "stt_rec = ''"
                tblDetail.RowFilter = strFilter
                tblDetail2.RowFilter = strFilter
                tblDetail3.RowFilter = strFilter
                cmdNew.Focus()
            Else
                oVoucher.Action = "View"
                Voucher.ScatterMemvar(tblMaster(iOldMasterRow), Me)
                Dim strFilter = "stt_rec = '" + tblMaster(iOldMasterRow).Item("stt_rec") + "'"
                tblDetail.RowFilter = strFilter
                tblDetail2.RowFilter = strFilter
                tblDetail3.RowFilter = strFilter

                cmdEdit.Focus()

                Tabs.ScatterTabControl(tblMaster(iOldMasterRow), oTab)
                Tabs.ScatterTabControl(tblMaster(iOldMasterRow), oTab1)
                Tabs.ScatterTabControl(tblMaster(iOldMasterRow), oTab2)
                'grdHeader.DataRow = tblMaster(iOldMasterRow).Row
                'grdHeader.Scatter()
            End If
        Else ' Truong hop sua
            ' Xoa trong grid
            oVoucher.Action = "View"
            nCount = tblDetail.Count - 1
            For i = nCount To 0 Step -1
                If Not IsDBNull(tblDetail(i).Item("stt_rec")) Then
                    If Trim(tblDetail(i).Item("stt_rec")) = "" Then
                        tblDetail(i).Delete()
                    End If
                    If Trim(tblDetail(i).Item("stt_rec")) = tblMaster(iMasterRow).Item("stt_rec") Then
                        tblDetail(i).Delete()
                    End If
                Else
                    tblDetail(i).Delete()
                End If
            Next
            Voucher.AppendFrom(tblDetail, oldtblDetail)


            nCount = tblDetail2.Count - 1
            For i = nCount To 0 Step -1
                If Not IsDBNull(tblDetail2(i).Item("stt_rec")) Then
                    If Trim(tblDetail2(i).Item("stt_rec")) = "" Then
                        tblDetail2(i).Delete()
                    End If
                    If Trim(tblDetail2(i).Item("stt_rec")) = tblMaster(iMasterRow).Item("stt_rec") Then
                        tblDetail2(i).Delete()
                    End If
                Else
                    tblDetail2(i).Delete()
                End If
            Next
            Voucher.AppendFrom(tblDetail2, oldtblDetail2)

            nCount = tblDetail3.Count - 1
            For i = nCount To 0 Step -1
                If Not IsDBNull(tblDetail3(i).Item("stt_rec")) Then
                    If Trim(tblDetail3(i).Item("stt_rec")) = "" Then
                        tblDetail3(i).Delete()
                    End If
                    If Trim(tblDetail3(i).Item("stt_rec")) = tblMaster(iMasterRow).Item("stt_rec") Then
                        tblDetail3(i).Delete()
                    End If
                Else
                    tblDetail3(i).Delete()
                End If
            Next
            Voucher.AppendFrom(tblDetail3, oldtblDetail3)
            RefrehForm()
            cmdEdit.Focus()
        End If
        Tabs.ReadOnlyTabControls(True, oTab)
        Tabs.ReadOnlyTabControls(True, oTab1)
        Tabs.ReadOnlyTabControls(True, oTab2)
        UpdateList()
        EDTBColumns()
    End Sub

    Public Sub GoRecno(ByVal cRecno)
        If Not oVoucher.Action = "View" Then
            Return
        End If
        Select Case cRecno
            Case "Top"
                If iMasterRow > 0 Then
                    iMasterRow = 0
                    EnabledButtons("0011")
                    RefrehForm()
                End If
            Case "Prev"
                If iMasterRow > 0 Then
                    iMasterRow = iMasterRow - 1
                    If iMasterRow = 0 Then
                        EnabledButtons("0011")
                    Else
                        EnabledButtons("1111")
                    End If
                    RefrehForm()
                End If
            Case "Next"
                If iMasterRow < tblMaster.Count - 1 And tblMaster.Count > 0 Then
                    iMasterRow = iMasterRow + 1
                    If iMasterRow = tblMaster.Count - 1 Then
                        EnabledButtons("1100")
                    Else
                        EnabledButtons("1111")
                    End If
                    RefrehForm()
                End If
            Case "Bottom"
                If iMasterRow < tblMaster.Count - 1 And tblMaster.Count > 0 Then
                    iMasterRow = tblMaster.Count - 1
                    EnabledButtons("1100")
                    RefrehForm()
                End If
        End Select
    End Sub

    Public Sub EnabledButtonsOnLoad()
        If iMasterRow = 0 And tblMaster.Count > 0 Then
            EnabledButtons("0011")
        ElseIf iMasterRow = tblMaster.Count - 1 And tblMaster.Count > 0 Then
            EnabledButtons("1100")
        Else
            EnabledButtons("1111")
        End If
    End Sub

    Private Sub EnabledButtons(s As String)
        cmdFirst.Enabled = IIf(s.Substring(0, 1) = "1", True, False)
        cmdPrevious.Enabled = IIf(s.Substring(1, 1) = "1", True, False)
        cmdNext.Enabled = IIf(s.Substring(2, 1) = "1", True, False)
        cmdLast.Enabled = IIf(s.Substring(3, 1) = "1", True, False)
    End Sub

#End Region

#End Region

#Region " Others "


    Private Sub txtma_ct_TextChanged(sender As Object, e As EventArgs)
        CreateVoucherNumber()
    End Sub

    Private Sub CreateVoucherNumber()
        If (oVoucher.Action = "New" Or oVoucher.Action = "Edit") Then
            If oVoucher.Action = "New" Then
                txtso_ct.Text = Sql.GetValue(conn, String.Format("SELECT dbo.getVoucherNumber_t1('{0}','{1}')", txtma_loai.Text, txtNgay_ct.Value.ToString("yyyyMMdd")))
            Else
                Dim command As String = "", val As String = "", nDem As Integer = 0
                command = String.Format("if exists(select 1 from  " + clsDefine.cMTable + " where stt_rec = '{0}' and ma_ct = '{1}' and left(CONVERT(char(8),ngay_ct,112),6)='{2}') select 1 else select 0", tblMaster(iMasterRow).Item("stt_rec"), txtma_loai.Text.Trim, Microsoft.VisualBasic.Left(txtNgay_ct.Value.ToString("yyyyMMdd"), 6))
                val = Sql.GetValue(conn, command)
                If val = "1" Then
                    nDem = 1
                    txtso_ct.Text = cOldSo_ct
                Else
                    txtso_ct.Text = Sql.GetValue(conn, String.Format("SELECT dbo.getVoucherNumber_t1('{0}','{1}')", txtma_loai.Text, txtNgay_ct.Value.ToString("yyyyMMdd")))
                End If
            End If
        End If
    End Sub

#End Region

#Region "Insert Bill"

    Private Sub txtNgay_ct_TextChanged(sender As Object, e As EventArgs) Handles txtNgay_ct.TextChanged, txtNgay_ct.Validated
        If oVoucher.Action = "New" Or oVoucher.Action = "Edit" Then
            CreateVoucherNumber()
            ' insertbill(txtNgay_ct.Value.ToString("yyyyMMdd"), txtma_loai.Text, txtMa_nt.Text, txtMa_cang_xephang.Text, txtMa_cang_dohang.Text, txtMa_hang.Text)
        End If
    End Sub

#End Region



    ' BEGIN IMPORT/EXPORT

    Private Sub ExportExcel(ByVal sender As Object, ByVal e As System.EventArgs)
        libs.Export.ExportExcel(Me.Text, "Detail", "v", clsDefine.sDField, clsDefine.sDHeader, clsDefine.sDFormat, clsDefine.sDWidth)
    End Sub

    Private Sub ImportExcel(ByVal sender As Object, ByVal e As System.EventArgs)
        If oVoucher.Action = "New" Or oVoucher.Action = "Edit" Then
            Dim ofd As New OpenFileDialog, fileName As String = ""
            ofd.Filter = "Excel Files (*.xlsx*)|*.xlsx"
            ofd.Title = "Chọn đường dẫn lưu tập tin mẫu"

            Dim dt As DataTable = Nothing, c As Integer = 0
            If ofd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                fileName = ofd.FileName

                dt = Export.ImportExcel(fileName, clsDefine.sDField, clsDefine.sDHeader, clsDefine.sDFormat, clsDefine.sDWidth)
                dt.Columns.Add(New DataColumn With {.ColumnName = "stt_rec"})
                c = dt.Rows.Count

                Dim stt_rec As String = ""
                Try
                    stt_rec = IIf(oVoucher.Action = "New", "", tblMaster(iMasterRow).Item("stt_rec").ToString.Trim)
                Catch ex As Exception
                    stt_rec = ""
                End Try

                For i As Integer = 0 To c - 1
                    dt.Rows(i)("stt_rec") = stt_rec
                Next

                Voucher.AppendFrom(tblDetail, dt)
            End If
            If c = 0 Then
                Msg.Alert("Không lấy được dữ liệu", 3)
                Return
            End If
        End If
    End Sub

    ' END IMPORT/EXPORT
    Private Sub insertdetail()
        Dim xSql As String = "", dt As DataTable, i As Integer = 0
        xSql = "select * from dmcong where status =1"
        dt = Sql.GetRecord(conn, xSql)
        If dt Is Nothing Then Return
        If dt.Rows.Count = 0 Then Return
        For i = 0 To dt.Rows.Count - 1
            tblDetail(i).Item("ma_cong") = dt.Rows(i).Item("ma_cong")
            tblDetail(i).Item("ten_cong") = dt.Rows(i).Item("ten_cong")
            tblDetail(i).Item("stt_rec") = ""
            tblDetail(i).Item("sl_n") = 0
            tblDetail(i).Item("sl_x") = 0
            tblDetail(i).Item("sl_t") = 0
            tblDetail.AddNew()
        Next
    End Sub

    Private Sub txtma_loai_TextChanged(sender As Object, e As EventArgs) Handles txtma_loai.TextChanged, txtma_loai.Validated
        If oVoucher.Action = "New" Or oVoucher.Action = "Edit" Then
            CreateVoucherNumber()
        End If
    End Sub

    Private Sub lblten_kh_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblten_kh.LinkClicked
        Try
            Authentication.CallAppWithArguments(conn, "", UserID, Admin, "dmkhokh.exe", "#" + txtma_kh.Text.Trim)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtma_kh_TextChanged(sender As Object, e As EventArgs) Handles txtma_kh.TextChanged
        oMa_dr.Key = String.Format(" status = 1 and ma_kho in (select ma_kho from dmkhct where ma_kh = '{0}')", txtma_kh.Text.Trim)
    End Sub
End Class
