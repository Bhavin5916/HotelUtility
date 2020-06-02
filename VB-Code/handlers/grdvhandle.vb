Imports System.Data.SqlClient

Public Class grdvhandle
    Dim con As SqlConnection
    Dim conObj As New connection

    Private _before_edit As String
    Private _after_edit As String
    Private _sender As DataGridView
    Private _sender1 As Object
    Private _p4 As Integer
    Sub New()
        con = conObj.connction
    End Sub
    'Sub New(ByVal before_edit As String, ByVal after_edit As String, ByVal sender As Object)
    '    con = conObj.connction
    '    ' TODO: Complete member initialization 
    '    _before_edit = before_edit
    '    _after_edit = after_edit
    '    _sender = sender
    'End Sub

    Sub New(ByVal before_edit As String, ByVal after_edit As String, ByVal sender As Object, ByVal p4 As Integer)
        ' TODO: Complete member initialization 
        con = conObj.connction
        ' TODO: Complete member initialization 
        _before_edit = before_edit
        _after_edit = after_edit
        _sender = sender
        _p4 = p4
    End Sub
    Sub update(ByVal p1 As String)
        Dim cmd1 As SqlCommand
        If _after_edit.Length = 0 Then
            MsgBox("Null Vales Not Allowed")
            _sender.CurrentCell.Value = _before_edit
        Else
            If _before_edit.Equals(_after_edit) Then
                MsgBox("Change Not Allowed")
            Else
                Try
                    con.Open()
                    cmd1 = New SqlCommand(p1, con)
                    cmd1.ExecuteNonQuery()
                    con.Close()
                Catch ex As Exception
                    MsgBox(ex.Message)
                Finally
                    MsgBox("Updated...")
                End Try
            End If
        End If
    End Sub
    Sub update_grd2()
        Dim cmd As SqlCommand
        Dim query As String = ""
        If _after_edit.Length = 0 Then
            MsgBox("Null Vales Not Allowed")
            _sender.CurrentCell.Value = _before_edit
        Else
            If _before_edit.Equals(_after_edit) Then
                MsgBox("No Change")
            Else
                Try
                    If _p4 = 0 Then
                        query = "update tbl_Item set Item_name='" & _after_edit & "' where Item_name='" & _before_edit & "'"
                    End If
                    If _p4 = 1 Then
                        query = "update tbl_Item set Item_rate='" & _after_edit & "' where Item_rate='" & _before_edit & "'"
                    End If
                    If _p4 = 2 Then
                        MsgBox("change Not Allow")
                    End If
                    If query.Length = 0 Then
                        MsgBox("Invalid Operarion")
                    Else
                        con.Open()
                        cmd = New SqlCommand(query, con)
                        cmd.ExecuteNonQuery()
                        con.Close()
                    End If
                Catch ex As Exception
                    MsgBox(ex.Message)
                Finally
                    MsgBox("Updated...")
                End Try
            End If
        End If
    End Sub
End Class
