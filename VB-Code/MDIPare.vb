Imports System.Windows.Forms

Public Class MDIPare
    Dim design_obj As New Design_app()

    Private Sub MDIPare_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Visible = False
        Login.Focus()
        Login.StartPosition = FormStartPosition.CenterScreen
        Login.Show()
        Timer1.Start()
        'Label1.Location.X = New Point(Me.Width - Label1.Width)
        Label1.Location = New Point(Me.Width - Label1.Width, 0)
    End Sub

    Private Sub ToolStripLabel1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripLabel1.Click
        Dim design_obj1 As New Design_app()
        ActiveMdiChild.Close()
        design_obj._set_home()
    End Sub
    Private Sub ToolStripLabel2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripLabel2.Click
        ActiveMdiChild.Close()
        Dim design_obj1 As New Design_app()
        design_obj1._set_biiling_table()
    End Sub
    Private Sub ToolStripLabel3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripLabel3.Click
        ActiveMdiChild.Close()
        design_obj._set_room_management()
    End Sub
    Private Sub ToolStripLabel4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripLabel4.Click
        ActiveMdiChild.Close()
        design_obj._set_customer_status()
    End Sub
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer1.Interval = 1000
        Label1.Text = System.DateTime.Now.TimeOfDay.ToString.Substring(0, 8)
    End Sub

    Private Sub ConfigOptionToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ConfigOptionToolStripMenuItem.Click
        configoption.Show()
        configoption.Focus()
    End Sub

    Private Sub ToolStripLabel5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripLabel5.Click
        MsgBox("Dou Yo Want Too LogOut :-)", MsgBoxStyle.OkCancel)
        If MsgBoxResult.Ok = 1 Then
            Me.Close()
        End If
    End Sub
End Class
