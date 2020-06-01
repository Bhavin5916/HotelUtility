Imports System.Data.SqlClient
Public Class AddNewCustomer
    Dim con As SqlConnection
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader
    Dim con_obj As New connection
    Dim design_obj As New Design_app()

    Private Sub AddNewCustomer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    End Sub



    Private Sub btn_add_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_add.Click
        If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox4.Text = "" Then
            MsgBox("Insufficiant Data")
        Else
            Dim regdate As Date = Convert.ToDateTime(DateTimePicker1.Value).Date
            con = New SqlConnection()
            con = con_obj.connction()
            con.Open()
            Try
                cmd = New SqlCommand("insert into tbl_Roomregistration(Customer_name,Customer_contactNo,Customer_IDproof_type,Customer_IdProof,Customer_regDate,Customer_adress) values ('" + TextBox1.Text + "','" + TextBox2.Text + "','" + ComboBox1.SelectedItem + "','" + TextBox4.Text + "','" + regdate + "','" + TextBox3.Text + "') ", con)
                cmd.ExecuteNonQuery()
                con.Close()
                MsgBox("Success!!!!!")
            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try

        End If

    End Sub
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Me.Close()
        Me.Dispose()
        design_obj._set_home()
    End Sub


End Class