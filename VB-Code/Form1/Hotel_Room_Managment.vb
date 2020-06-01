Imports System.Data.SqlClient
Imports System.Threading

Public Class Hotel_Room_Managment
    Dim con_obj As New connection()
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader
    Dim con As New SqlConnection
    Dim Design_app_obj As New Design_app()
    Dim validation_obj As New Validation()
    Dim ndtp As Date = Date.Now
    Private _varification As Integer

    Private Sub Hotel_Room_Managment_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        con = con_obj.connction
        Me.GroupBox1.Width = Me.Width - 30
        fillcombo_package()
    End Sub

    Private Sub btn_reg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_reg.Click
        Dim tmp As Integer = varification(cmb_prooftype.SelectedItem, txt_id.Text)
        dtp.Format = DateTimePickerFormat.Short
        If tmp = 1 Then
            MsgBox("Regisration ID Exist")
        Else
            con = con_obj.connction
            con.Open()
            cmd = New SqlCommand("insert into tbl_RoomRegistration(Customer_name,Customer_contactNo,Customer_IDproof_type,Customer_IdProof,Customer_regDate)values('" & txt_name.Text & "','" & txt_cntno.Text & "','" & cmb_prooftype.SelectedItem & "','" & txt_id.Text & "','" & dtp.Value & "')", con)
            tmp = cmd.ExecuteScalar()
            con.Close()
        End If

    End Sub
    '---------------------------------------------------------------------------------------------------------------------------
    'This Form Closing

    Public Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        form_Onclosing()
    End Sub
    Private Sub Label6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label6.Click
        Dim i = 0
        Me.AutoSize = True
        For i = 0 To 173 Step 1
            Me.GroupBox1.Height = i
            Thread.Sleep(2)
        Next
    End Sub
    Private Sub fillcombo_package()
        ComboBox2.Items.Clear()
        con.Open()
        cmd = New SqlCommand("select RoomPackage from tbl_room_package", con)
        dr = cmd.ExecuteReader()
        While dr.Read()
            ComboBox2.Items.Add(dr(0))
        End While
        dr.Close()
        con.Close()
    End Sub
    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged
        con = con_obj.connction
        con.Open()
        cmd = New SqlCommand("select * from tbl_room_package where RoomPackage='" + ComboBox2.SelectedItem + "'", con)
        dr = cmd.ExecuteReader()
        Dim i = 1
        While dr.Read()
            If dr.IsDBNull(i) = True Then
                Label10.Text = "null"
            Else
                Label10.Text = dr(i)
            End If
            i = i + 1
            If dr.IsDBNull(i) = True Then
                Label16.Text = "null"
            Else
                Label16.Text = dr(i)
            End If
            i = i + 1
            If dr.IsDBNull(i) = True Then
                Label17.Text = ""
            Else
                Label17.Text = dr(i)
            End If
            i = i + 1
            If dr.IsDBNull(i) = True Then
                Label18.Text = ""
            Else
                Label18.Text = dr(i)
            End If
            i = i + 1
            If dr.IsDBNull(i) = True Then
                Label19.Text = ""
            Else
                Label19.Text = dr(i)
            End If
            i = i + 1
            If dr.IsDBNull(i) = True Then
                Label20.Text = ""
            Else
                Label20.Text = dr(i)
            End If
            i = i + 1
        End While
        dr.Close()
        Dim cmd1 As SqlCommand
        Dim dr1 As SqlDataReader
        cmd1 = New SqlCommand("Select Rom_No From tbl_room where Room_status='OFF'and  Room_Package='" + ComboBox2.SelectedItem + "'", con)
        dr1 = cmd1.ExecuteReader()
        ComboBox3.Items.Clear()
        Dim count = 0
        While dr1.Read()
            ComboBox3.Items.Add(dr1(0))
            count = count + 1
        End While
        If count = 0 Then
            MsgBox("ROOMS FOR THIS SERVICE NOT AVAILABLE")
        End If
        dr1.Close()
        con.Close()
    End Sub
    Private Sub btn_checkin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_checkin.Click
        Dim flag As Integer = varification(ComboBox1.SelectedItem, TextBox1.Text)
        If flag = 1 Then
            _room_status()
            _change_roomstatus()
            form_Onclosing()
        Else
            MsgBox("Id is not Registered")
            TextBox1.Text = ""
            ComboBox1.Text = ""
        End If

    End Sub

    Private Sub _room_status()
        Dim cmd1 As SqlCommand
        Dim dr3 As SqlDataReader
        Dim _id As String = ""
        Dim _roomno As String = ComboBox3.SelectedItem
        Dim _customerentrydate As String = dtp.Value
        Dim _customerleavingdate As String = ""
        Dim _roomstatus As String = "ON"
        Dim _roompackage As String = ComboBox2.SelectedItem
        con.Open()
        cmd1 = New SqlCommand("Select Customer_Id From tbl_RoomRegistration where  Customer_IDproof_type='" + ComboBox1.SelectedItem + "' and Customer_IdProof='" + TextBox1.Text + "'", con)
        dr3 = cmd1.ExecuteReader()
        While dr3.Read()
            _id = dr3(0).ToString
        End While
        dr3.Close()
        Try
            cmd = New SqlCommand("insert into tbl_room_status(Room_No,Customer_Id,Customer_E_date,Customer_L_date,Room_Package) values('" + _roomno + "','" + _id + "' ,'" + ndtp.Date + "',NULL,'" + _roompackage + "')", con)
            cmd.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        con.Close()
    End Sub

    Private Sub _change_roomstatus()
        ComboBox2.Items.Clear()
        Dim cmd2 As SqlCommand
        con.Open()
        cmd2 = New SqlCommand("update tbl_room set Room_status='ON' where Rom_No='" + ComboBox3.SelectedItem + "'", con)
        cmd2.ExecuteNonQuery()
        con.Close()
    End Sub

    Public Sub form_Onclosing()
        Room_mgmt.Room_load()
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub btn_cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_cancel.Click
        Dim i = 0
        Me.AutoSize = True
        For i = 173 To 0 Step -1
            Me.GroupBox1.Height = i
            Thread.Sleep(2)
        Next
    End Sub


    Private Function varification(ByVal p1 As String, ByVal p2 As String) As Integer
        Dim tmp As Integer = 0
        con.Open()
        cmd = New SqlCommand("select * from tbl_RoomRegistration where Customer_IDproof_type='" & p1.ToString & "' and Customer_IdProof='" & p2.ToString & "'", con)
        dr = cmd.ExecuteReader()
        While dr.Read()
            tmp = 1
        End While
        con.Close()
        Return tmp
    End Function



End Class