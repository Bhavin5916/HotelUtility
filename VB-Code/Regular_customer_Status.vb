Imports System.Data.SqlClient

Public Class Regular_customer_Status
    Dim design_obj As New Design_app()
    Dim con As New SqlConnection
    Dim cmd As New SqlCommand
    Dim dr As SqlDataReader
    Dim adp As SqlDataAdapter
    Dim con_obj As New connection
    Private Sub Regular_customer_Status_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        con = con_obj.connction()
        TextBox2.Text = ""
        TextBox2.Enabled = False
        TextBox3.Text = ""
        TextBox3.Enabled = False
        TextBox4.Text = ""
        TextBox4.Enabled = False
        design_obj._set_customer_status()
        fillcombobox1()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim cmd1 As SqlCommand
        Dim dr1 As SqlDataReader
        Dim dt_billdetail As New DataTable
        Dim dt_1 As New DataTable
        Dim dt_2 As New DataTable
        ' Dim adp2 As SqlDataAdapter
        If TextBox1.Text = "" Then
            MsgBox("User Id Required")
        Else
            con.Open()
            Try
                cmd.CommandText = "select * From tbl_RoomRegistration where Customer_IdProof='" + TextBox1.Text + "' "
                cmd.CommandType = CommandType.Text
                cmd.Connection = con
                adp = New SqlDataAdapter(cmd)
                adp.Fill(dt_billdetail)
                If dt_billdetail.Rows.Count > 0 Then
                    TextBox2.Text = dt_billdetail.Rows(0).Item(1).ToString
                    TextBox3.Text = dt_billdetail.Rows(0).Item(2).ToString
                    TextBox4.Text = dt_billdetail.Rows(0).Item(6).ToString
                    Label8.Text = dt_billdetail.Rows(0).Item(7).ToString
                    Label9.Text = dt_billdetail.Rows(0).Item(8).ToString
                    cmd1 = New SqlCommand("select * from customer_billdetail where Customer_accountNo='" + TextBox1.Text + "'", con)
                    dr1 = cmd1.ExecuteReader()
                    DataGridView1.Rows.Clear()
                    While dr1.Read()
                        DataGridView1.Rows.Add(dr1(1), dr1(2))
                    End While
                    dr1.Close()
                    Dim cmd3 As SqlCommand = New SqlCommand("select * from tbl_paymentdetail where acount_no='" & TextBox1.Text & "'", con)
                    adp = New SqlDataAdapter(cmd3)
                    adp.Fill(dt_2)
                    DataGridView2.DataSource = dt_2
                Else
                    MsgBox("User Id Incorect")
                End If
            Catch ex As Exception
                MsgBox(ex.ToString)
            Finally
                dt_billdetail.Dispose()
            End Try
            con.Close()
        End If
        '    cmd1 = New SqlCommand("select * from customer_billdetail where Customer_accountNo='" + TextBox1.Text + "'", con)
        '    dr1 = cmd1.ExecuteReader()
        '    DataGridView1.Rows.Clear()
        '    While dr1.Read()
        '        DataGridView1.Rows.Add(dr1(1), dr1(2))
        '    End While
        ' Cal_total()
    End Sub
    'Private Sub Cal_total()
    '    Dim dr As DataGridViewRow
    '    Dim total_credit As Double = 0
    '    Dim total_payment As Double = 0
    '    For Each dr In DataGridView1.Rows
    '        total_credit = total_credit + Convert.ToDouble(dr.Cells(0).Value)
    '    Next
    '    For Each dr In DataGridView2.Rows
    '        total_payment = total_payment
    '    Next
    '    'lbl_total.Text = total_credit
    'End Sub

    Private Sub Regular_customer_Status_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing

    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Me.Close()
        Me.Dispose()
        design_obj._set_home()
    End Sub
    Private Sub fillcombobox1()
        Dim dt_account As DataTable = New DataTable
        Try
            con.Open()
            cmd.CommandText = "select Customer_IdProof,Customer_name From tbl_RoomRegistration"
            cmd.CommandType = CommandType.Text
            cmd.Connection = con
            adp = New SqlDataAdapter(cmd)
            adp.Fill(dt_account)
            con.Close()
        Catch ex As SqlException
            MsgBox(ex.ToString)
        End Try
        ComboBox1.DisplayMember = "Customer_name"
        ComboBox1.ValueMember = "Customer_IdProof"
        ComboBox1.DataSource = dt_account
        ComboBox1.SelectedIndex = -1
    End Sub
    Private Sub ComboBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.TextChanged
        TextBox1.Text = ComboBox1.SelectedValue
    End Sub
End Class