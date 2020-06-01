Imports System.Data.SqlClient

Public Class Login


    Dim con As SqlConnection
    Dim con_obj As New connection()
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader
    Dim design_obj As New Design_app()
    Private Sub btnLogIn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        con = con_obj.connction()
        con.Open()
        Dim query As String
        query = "select * from tbl_admin where Admin_password='" + txtPassword.Text + "'"
        cmd = New SqlCommand(query, con)
        dr = cmd.ExecuteReader()
        If dr.Read = False Then
            MsgBox("Password Incorrect")
        Else
            MDIPare.Visible = True
            Me.Close()
            design_obj._set_home()
        End If
        con.Close()



    End Sub
    Private Sub txtPassword_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPassword.KeyPress
        If Asc(e.KeyChar) = 13 Then
            btnLogIn_Click(sender, e)
        End If
    End Sub

    Private Sub Label3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label3.Click
        btnLogIn_Click(sender, e)
    End Sub

    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label1.Click

    End Sub
End Class