using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Demo.Hotel
{
    class grdvhandle
    {
        SqlConnection con;
        connection conobject;

        private string _before_edit;
        private string _after_edit;
        private string _sender;
        private string _sender1;
        private string _p4;

        public grdvhandle()
        { 
        }

        public grdvhandle(string before_edit, string after_edit, object sender, Int16 p4)
        {
        //           ' TODO: Complete member initialization 
        //con = conObj.connction
        //' TODO: Complete member initialization 
        //_before_edit = before_edit
        //_after_edit = after_edit
        //_sender = sender
        //_p4 = p4
        }

        public void update()
        {
        //    Dim cmd1 As SqlCommand
        //If _after_edit.Length = 0 Then
        //    MsgBox("Null Vales Not Allowed")
        //    _sender.CurrentCell.Value = _before_edit
        //Else
        //    If _before_edit.Equals(_after_edit) Then
        //        MsgBox("Change Not Allowed")
        //    Else
        //        Try
        //            con.Open()
        //            cmd1 = New SqlCommand(p1, con)
        //            cmd1.ExecuteNonQuery()
        //            con.Close()
        //        Catch ex As Exception
        //            MsgBox(ex.Message)
        //        Finally
        //            MsgBox("Updated...")
        //        End Try
        //    End If
        //End If
        }
        public void update_grd2()
        { 
        //     Dim cmd As SqlCommand
        //Dim query As String = ""
        //If _after_edit.Length = 0 Then
        //    MsgBox("Null Vales Not Allowed")
        //    _sender.CurrentCell.Value = _before_edit
        //Else
        //    If _before_edit.Equals(_after_edit) Then
        //        MsgBox("No Change")
        //    Else
        //        Try
        //            If _p4 = 0 Then
        //                query = "update tbl_Item set Item_name='" & _after_edit & "' where Item_name='" & _before_edit & "'"
        //            End If
        //            If _p4 = 1 Then
        //                query = "update tbl_Item set Item_rate='" & _after_edit & "' where Item_rate='" & _before_edit & "'"
        //            End If
        //            If _p4 = 2 Then
        //                MsgBox("change Not Allow")
        //            End If
        //            If query.Length = 0 Then
        //                MsgBox("Invalid Operarion")
        //            Else
        //                con.Open()
        //                cmd = New SqlCommand(query, con)
        //                cmd.ExecuteNonQuery()
        //                con.Close()
        //            End If
        //        Catch ex As Exception
        //            MsgBox(ex.Message)
        //        Finally
        //            MsgBox("Updated...")
        //        End Try
        //    End
        }
    }
}
