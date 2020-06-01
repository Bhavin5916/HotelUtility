Imports System.Data.SqlClient
Public Class Roo_check_Out
    Dim Design_app_obj As New Design_app()
    Dim con As New SqlConnection
    Dim dr As SqlDataReader
    Dim cmd As SqlCommand
    Dim con_obj As New connection()
    Dim id As String = Room_mgmt.Label5.Text
    Dim total_bill As Double = 0
    Private Sub Roo_check_Out_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        this_load()
        get_caterinbill()
        cl_bill()
    End Sub
    'This form close button
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        _thisformclosing()
    End Sub
    'Name edit
    Private Sub Label5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label5.Click
        TextBox1.Enabled = True
    End Sub

    Private Sub btn_ckeckout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_ckeckout.Click
        Dim cmd1 As SqlCommand
        con.Open()
        cmd = New SqlCommand("Update tbl_room set Room_status='OFF' where Rom_No =@roomno", con)
        cmd.Parameters.AddWithValue("@roomno", Label10.Text)
        cmd.ExecuteNonQuery()
        cmd1 = New SqlCommand("delete  tbl_room_status where Room_No='" + Label10.Text + "'", con)
        cmd1.ExecuteNonQuery()
        con.Close()
        Insert_record()
        Room_mgmt.Room_load()
        Room_mgmt._set_customerdetailnull()
        _thisformclosing()
    End Sub

    Public Sub this_load()
        con = con_obj.connction
        Dim _days As Integer
        Dim _roomno As String = Room_mgmt.Label4.Text
        If _roomno = "Label " Then
            MsgBox(Handle)
        Else
            _roomno = Room_mgmt.Label4.Text
            con.Open()
            cmd = New SqlCommand("Select * from tbl_room_status inner join tbl_RoomRegistration on tbl_room_status.Customer_Id=tbl_RoomRegistration.Customer_Id  where tbl_room_status.Room_No='" + _roomno + "'", con)
            dr = cmd.ExecuteReader()
            While dr.Read()
                Label6.Text = DateAndTime.DateValue(dr(2).ToString)
                Label8.Text = DateAndTime.DateValue(DateAndTime.Now)
                Label10.Text = dr(0)
                Label12.Text = dr(4)
                TextBox1.Text = dr(6)
                Label4.Text = dr(1)
                _days = DateAndTime.DateDiff(DateInterval.Day, Convert.ToDateTime(Label6.Text), Convert.ToDateTime(Label8.Text))
                If _days = 0 Then
                    _days = 1
                End If
                Label20.Text = _days
            End While
            con.Close()
        End If
    End Sub

    Public Sub _thisformclosing()
        Me.Close()
        Me.Dispose()
        Design_app_obj._set_room_management()
    End Sub
    Public Sub cl_bill()
        Dim room_charge As Double
        con = con_obj.connction
        con.Open()
        cmd = New SqlCommand("Select * from tbl_room inner join tbl_room_package on tbl_room.Room_Package=tbl_room_package.RoomPackage where Rom_No='" + Label10.Text + "'", con)
        dr = cmd.ExecuteReader()
        While dr.Read()
            Label18.Text = dr(5).ToString
            room_charge = Convert.ToDouble(dr(7))
        End While
        con.Close()
        room_charge = room_charge + ((room_charge * Convert.ToDouble(Label14.Text)) / 100) + total_bill
        Label16.Text = room_charge.ToString
    End Sub

    Private Sub get_caterinbill()
        con.Open()
        Dim total_bill As Double = 0
        cmd = New SqlCommand("Select * from customer_billdetail where Customer_accountNo='" + Label4.Text + "' and Customer_BillDate>='" + Convert.ToDateTime(Label6.Text).Date + "' and Customer_BillDate<='" + Convert.ToDateTime(Label8.Text).Date + "' ", con)
        dr = cmd.ExecuteReader()
        Bill.Items.Insert(0, "Date                    Time                       Bill")
        While dr.Read()
            Bill.Items.Add(dr(2) + "            " + dr(3).ToString.Substring(0, 4) + "             " + dr(1))
            total_bill = total_bill + Convert.ToDouble(dr(1))
        End While
        con.Close()
        Label22.Text = total_bill
    End Sub
    Private Sub Insert_record()
        con.Open()
        cmd = New SqlCommand("insert into tbl_records(customer_id,Room_No,Checkin_date,checkout_date,Room_package,Rastaurat_bill,Room_bill,Room_tax) values('" + Label4.Text + "','" + Label10.Text + "','" + Label6.Text + "','" + Label8.Text + "','" + Label12.Text + "','" + Label22.Text + "','" + Label16.Text + "','" + Label14.Text + "')", con)
        cmd.Parameters.AddWithValue("@roomno", Label10.Text)
        cmd.ExecuteNonQuery()
        con.Close()
    End Sub
End Class