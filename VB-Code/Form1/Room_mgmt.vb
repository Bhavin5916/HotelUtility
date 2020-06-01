Imports System.Data.SqlClient
Public Class Room_mgmt
    Dim con As New SqlConnection
    Dim con_obj As New connection()
    Dim cmd As SqlCommand
    Dim cmd1 As SqlCommand
    Dim dt As DataTable
    Dim dt1 As DataTable
    Dim adp As SqlDataAdapter
    Dim adp1 As SqlDataAdapter
    Dim dr As SqlDataReader
    Dim Design_app_obj As New Design_app()

    Private Sub Room_mgmt_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Design_app_obj._set_room_management()
        Room_load()
        _set_customerdetailnull()

    End Sub

    Private Sub lbl_click(ByVal sender As Object, ByVal e As EventArgs)
        Dim lbl As New Button
        lbl = sender
        Dim room_text As String
        Dim room_no As Integer
        room_text = lbl.Text.Trim()
        room_text = room_text.Substring(4)
        room_no = Convert.ToInt32(room_text)
        dt = New DataTable()
        con.Open()
        cmd = New SqlCommand("select * from tbl_room inner join tbl_room_status on tbl_room.Rom_No  = tbl_room_status.Room_No  where  tbl_room.Rom_No='" + room_no.ToString + "' and tbl_room.Room_status='ON'", con)
        adp = New SqlDataAdapter(cmd)
        adp.Fill(dt)
        con.Close()
        If dt.Rows.Count = 0 Then
            Design_app_obj._set_room_reigister()
            _set_customerdetailnull()
        Else
            con.Open()
            cmd = New SqlCommand("select * from tbl_RoomRegistration where tbl_RoomRegistration.Customer_Id='" + dt.Rows(0).Item(7).ToString + "'", con)
            dr = cmd.ExecuteReader()
            While dr.Read()
                Button1.Enabled = True
                Button2.Enabled = True
                Label4.Text = room_text
                'Label5.Text = dt.Rows(0).Item(4).ToString
                txt_name.Text = dr(1)
                txt_cntno.Text = dr(2)
            End While
            con.Close()
        End If

    End Sub

    Public Sub Room_load()
        con = con_obj.connction()
        dt1 = New DataTable()
        con.Open()
        cmd1 = New SqlCommand("select * from tbl_room", con)
        adp1 = New SqlDataAdapter(cmd1)
        adp1.Fill(dt1)
        con.Close()

        Dim i = 50
        FlowLayoutPanel1.Controls.Clear()
        Dim count = 0
        Dim label_add(i) As Button
        For count = count To dt1.Rows.Count - 1 Step 1
            Dim lbl_counter = 0
            label_add(count) = New Button
            label_add(count).Text = "Room" + dt1.Rows(count).Item(1)
            If dt1.Rows(count).Item(2) = "ON" Then
                lbl_counter = lbl_counter + 1
                label_add(count).BackgroundImage = Form1.My.Resources.booked
                label_add(count).Text = label_add(count).Text.ToString
                label_add(count).TextAlign = ContentAlignment.TopCenter

            Else
                label_add(count).BackgroundImage = Form1.My.Resources.blue
                label_add(count).TextAlign = ContentAlignment.MiddleCenter
            End If
            label_add(count).BackgroundImageLayout = ImageLayout.Stretch
            label_add(count).FlatStyle = FlatStyle.Flat
            label_add(count).Margin = New Padding(15)

            label_add(count).Width = 130
            label_add(count).Height = 100

            label_add(count).CausesValidation = False
            FlowLayoutPanel1.Controls.Add(label_add(count))
            AddHandler label_add(count).MouseDown, AddressOf lblMousedown
            AddHandler label_add(count).MouseHover, AddressOf lblMoushover
            AddHandler label_add(count).Click, AddressOf lbl_click
        Next
    End Sub

    'closing room management

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Me.Close()
        Me.Dispose()
        Design_app_obj._set_home()
    End Sub


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Design_app_obj._set_check_out(dt.Rows(0).Item(4).ToString)
    End Sub


    Public Sub _set_customerdetailnull()
        Label4.Text = ""
        txt_name.Text = ""
        txt_cntno.Text = ""
        Label4.Enabled = False
        txt_name.Enabled = False
        txt_cntno.Enabled = False
        Button1.Enabled = False
        Button2.Enabled = False
    End Sub
    Private Sub lblMousedown(ByVal sender As Object, ByVal e As MouseEventArgs)
        If e.Button = Windows.Forms.MouseButtons.Right Then
            Dim btn As Button = sender
            con.Open()
            cmd = New SqlCommand("select * from tbl_room as t1 inner join tbl_room_package as t2 on t1.Room_Package=t2.RoomPackage where Rom_No='" + btn.Text.ToString.Substring(4) + "'", con)
            dr = cmd.ExecuteReader
            While dr.Read
                Label10.Text = dr(1)
                Label14.Text = dr(4)
            End While
            con.Close()
        End If
    End Sub
    Private Sub lblMoushover(ByVal sender As Object, ByVal e As EventArgs)
        Dim lbl As Button = sender
        lbl.FlatStyle = FlatStyle.Popup
    End Sub
End Class