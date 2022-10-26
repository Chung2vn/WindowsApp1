Imports libs

Public Class frmSearch

    Private Sub frmSearch_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtdFrom.AddCalenderControl()
        txtdTo.AddCalenderControl()
        butOk.Image = Functions.Image("libs.mark.png")
        butCancel.Image = Functions.Image("libs.cancel.png")

        Functions.ApplyForm(Me)

        Dim s1 As New clsAutocl.DirLibObj.DirLib(txtma_kh, lblten_kh, frmMain.conn, "dmkh", "ma_kh", "ten_kh", "Customer", "1=1", True, butCancel)
        Dim s1s As New clsAutocl.DirLibObj.DirLib(txtMa_nv, lblTen_nv, frmMain.conn, "dmkh", "ma_kh", "ten_kh", "Customer", "1=1", True, butCancel)
        Dim s14 As New clsAutocl.DirLibObj.DirLib(txtma_loai, lblten_loai, frmMain.conn, "dmloailog", "ma_loai", "ten_loai", "LogType", "1=1", True, butCancel)
        txtdFrom.Value = Reg.GetRegistryKey("DFDFrom", frmMain.appName)
        txtdTo.Value = Reg.GetRegistryKey("DFDTo", frmMain.appName)
    End Sub

    Private Sub butOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butOk.Click
        Dim strSQL As String = ""
        Dim usersales As String = ""
        usersales = frmMain.Usersales
        strSQL = "exec st_Search_t1 " + Sql.Convert2SQL(txtdFrom.Value, "") + ", " + Sql.Convert2SQL(txtdTo.Value, "") + ", '" + txtma_kh.Text.Trim + "'" + ", '" + txtSo_ct.Text.Trim + "'" + ","
        strSQL += "'" + txtma_loai.Text.Trim + "'" + ",'" + txtMa_nv.Text.Trim + "'" + ",'" + txtBook_no.Text.Trim + "'" + ",'" + txtSo_tk.Text.Trim + "'" + ","
        strSQL += "'" + usersales + "'"
        Dim ds As New DataSet
        Sql.SQLRetrieve(frmMain.conn, strSQL, "tmp", ds)

        If ds.Tables(0).Rows.Count > 0 Then
            frmMain.grdDetail.SuspendLayout()

            Reg.SetRegistryKey("DFDFrom", txtdFrom.Value, frmMain.appName)
            Reg.SetRegistryKey("DFDTo", txtdTo.Value, frmMain.appName)
            Me.Close()
            frmMain.tblMaster.Table.Clear()
            frmMain.tblMaster.Table.AcceptChanges()

            frmMain.tblDetail.Table.Clear()
            frmMain.tblDetail.Table.AcceptChanges()
            frmMain.tblMaster.Table = ds.Tables(0)
            frmMain.tblDetail.Table = ds.Tables(1)

            frmMain.grdDetail.TableStyles(0).MappingName = frmMain.tblDetail.Table.ToString

            frmMain.iMasterRow = 0
            Dim strFilter = "stt_rec = '" + frmMain.tblMaster(frmMain.iMasterRow).Item("stt_rec") + "'"
            frmMain.tblDetail.RowFilter = strFilter

            frmMain.oVoucher.Action = "View"
            frmMain.oVoucher.RefreshButton(frmMain.oVoucher.Action)

            frmMain.grdDetail.ResumeLayout()

            If frmMain.tblMaster.Count = 1 Then
                frmMain.RefrehForm()
            Else
                frmMain.View()
            End If

            If frmMain.tblMaster.Count = 1 Then
                frmMain.cmdEdit.Focus()
            End If
            ds = Nothing
        Else
            Msg.Alert(AllText.VoucherSearchEmpty, 2, Me.Text)
            ds = Nothing
        End If
    End Sub

    Private Sub butCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butCancel.Click
        Me.Dispose()
        Me.Close()
    End Sub

End Class