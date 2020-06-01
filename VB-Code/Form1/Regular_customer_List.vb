Imports System.Data.SqlClient
Public Class Regular_customer_List
    Dim con As New SqlConnection
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader
    Dim con_obj As New connection
    Private Sub Regular_customer_List_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        con = con_obj.connction()
        con.Open()
        cmd = New SqlCommand("select * from Customer_detail ", con)
        dr = cmd.ExecuteReader()
        While dr.Read()
            ListBox1.Items.Add(dr(1))
        End While
        con.Close()
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox1.SelectedIndexChanged
        con = con_obj.connction()
        con.Open()
        cmd = New SqlCommand("select * from Customer_detail where customer_Name='" + ListBox1.SelectedItem.ToString + "' ", con)
        dr = cmd.ExecuteReader()
        While dr.Read()
            txt_name.Text = dr(1).ToString
            txt_cntNo.Text = dr(2).ToString
            txt_address.Text = dr(3).ToString
            txt_customerId.Text = dr(4).ToString
        End While
        con.Close()
    End Sub

    Private Sub Regular_customer_List_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Me.Dispose()
    End Sub
End Class