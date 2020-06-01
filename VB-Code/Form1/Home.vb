Imports System.Threading
Public Class Home
    Dim design_obj As New Design_app()
    Private Sub Home_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        _set_box_property()
    End Sub
    Private Sub _set_box_property()
        Dim h As Integer = 160
        Dim w As Integer = 2 * h
        Label1.Height = h
        Label1.Width = w
        Label2.Height = h
        Label2.Width = w
        Label3.Height = h
        Label3.Width = w
        Label4.Height = h
        Label4.Width = w
        Label5.Height = h
        Label5.Width = w
        Dim _margin = 30
        Dim x2 = (MDIPare.Width / 2) - (Label2.Width / 2)
        Dim y2 = (MDIPare.Height / 2) - (Label2.Height / 2) - 80
        Label2.Location = New Point(x2, y2)
        Label1.Location = New Point(x2 - Label1.Width - _margin, y2 - Label1.Height - _margin)
        Label3.Location = New Point(x2 + Label2.Width + _margin, y2 - Label2.Height - _margin)
        Label4.Location = New Point(x2 - Label4.Width - _margin, y2 + Label4.Height + _margin)
        Label5.Location = New Point(x2 + Label5.Width + _margin, y2 + Label5.Height + _margin)

    End Sub
    '---------------------------------------------------------------------------------------------------------------------
    'Mouse Enter
    '---------------------------------------------------------------------------------------------------------------------
    Private Sub Label1_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label1.MouseEnter
        enter_event(sender)
    End Sub
    Private Sub Label2_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label2.MouseEnter
        enter_event(sender)
    End Sub
    Private Sub Label3_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label3.MouseEnter
        'Label3.Height = Label3.Height + 20
        'Label3.Width = Label3.Width + 20
        enter_event(sender)
    End Sub
    Private Sub Label4_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label4.MouseEnter
        enter_event(sender)
    End Sub
    Private Sub Label5_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label5.MouseEnter

        enter_event(sender)

    End Sub
    Private Sub enter_event(ByVal sender As Object)
        Dim lbl As New Label
        lbl = sender
        lbl.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        lbl.Height = lbl.Height + 20
        lbl.Width = lbl.Width + 20
        lbl.Location = New Point(lbl.Location.X - 10, lbl.Location.Y - 10)
    End Sub

    'Mouse Leave
    '---------------------------------------------------------------------------------------------------------------------

    Private Sub Label1_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label1.MouseLeave
        leave_event(sender)
    End Sub
    Private Sub Label2_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label2.MouseLeave
        leave_event(sender)
    End Sub
    Private Sub Label3_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label3.MouseLeave

        leave_event(sender)
    End Sub
    Private Sub Label4_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label4.MouseLeave

        leave_event(sender)
    End Sub
    Private Sub Label5_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label5.MouseLeave

        leave_event(sender)
    End Sub
    Private Sub leave_event(ByVal sender As Object)
        Dim lbl As New Label
        lbl = sender
        lbl.BackColor = System.Drawing.SystemColors.ActiveCaption
        lbl.Height = lbl.Height - 20
        lbl.Width = lbl.Width - 20
        lbl.Location = New Point(lbl.Location.X + 10, lbl.Location.Y + 10)
        lbl.BackColor = System.Drawing.SystemColors.ButtonHighlight

    End Sub







    '----------------------------------------------------------------------------------------------------------------

    'click event 

    '--------------------------------------------------------------------------------------------------------------
    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label1.Click
        design_obj._set_customer_register()
        Me.Close()
    End Sub
    Private Sub Label2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label2.Click
        design_obj._set_biiling_table()
        Me.Close()
    End Sub
    Private Sub Label3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label3.Click
        design_obj._set_customer_status()
        Me.Close()
    End Sub
    Private Sub Label4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label4.Click
        design_obj._setadmin_panel()
        Me.Close()
    End Sub
    Private Sub Label5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label5.Click
        design_obj._set_room_management()
        Me.Close()
    End Sub





End Class