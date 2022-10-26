Public Class clsDefine

    Public Shared cKey As String = "1=1"
    Public Shared isDelete As Boolean = False
    Public Shared Ws_Id As Char = "L"
    Public Shared sMField As String = "ma_loai,ten_loai,so_ct,ngay_ct,ma_kh,ten_kh,ma_nv,ten_nv,dien_giai"
    Public Shared sMHeader As String = "Loại hàng,Tên hàng,Số đơn hàng,Ngày đơn hàng,Mã khách hàng,Tên khách hàng,Mã điều phối,Tên điều phối,Ghi chú"
    Public Shared sMWidth As String = "100,120,100,100,100,200,100,200,300"
    Public Shared sMFormat As String = ",,,,,,,,"


    Public Shared sDField As String = "ma_cong,ten_cong,sl_n,sl_x,sl_t,dien_giai"
    Public Shared sDHeader As String = "Loại Container,Tên Container,Số lượng, SL phân bổ,SL tồn,Ghi chú"
    Public Shared sDWidth As String = "100,200,80,80,80,300"
    Public Shared sDFormat As String = ",,### ### ### ##0,### ### ### ##0,### ### ### ##0,"

    Public Shared sDField2 As String = "ma_phi,ten_phi,ma_dvt,ten_dvt,gia,dien_giai"
    Public Shared sDHeader2 As String = "Mã phí,Tên phí,Đơn vị,Tên đơn vị,Đơn giá,Diễn giải"
    Public Shared sDWidth2 As String = "100,150,100,0,100,250"
    Public Shared sDFormat2 As String = ",,,,### ### ### ##0,"

    Public Shared cMTable As String = "T1"
    Public Shared cDTable As String = "T1ct"
    Public Shared cDTable2 As String = "T1ct3"
    Public Shared cDTable3 As String = "T1ct4"
End Class
