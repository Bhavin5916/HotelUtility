Imports System.Data.SqlClient
Public Class Validation
    Dim con_obj As New connection()
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader
    Dim con As New SqlConnection
    Dim dt As DataTable
    Public Property id As String
    Dim tmp As Integer = 0
    Sub New()
        con = con_obj.connction()
    End Sub
    Function varification() As Integer
        con = con_obj.connction()
        Dim tmp As Integer = 0
        con.Open()
        cmd = New SqlCommand("select * from tbl_RoomRegistration where Customer_IdProof='" & id & "'", con)
        dr = cmd.ExecuteReader()
        While dr.Read()
            tmp = 1
        End While
        con.Close()
        Return tmp
    End Function
    Sub Global_insert(ByVal amount As String, ByVal id As String, ByVal p3 As String, ByVal tmp_time As String, ByVal bill_no As Integer)
        con = con_obj.connction
        con.Open()
        cmd = New SqlCommand("insert into customer_billdetail(Customer_accountNo,Customer_customerBill,Customer_Billdate,bill_time,bill_No)values('" & id & "','" & amount & "','" & p3 & "','" & tmp_time & "','" & bill_no & "')", con)
        cmd.ExecuteNonQuery()
        con.Close()
    End Sub
    Sub detailedbill_insert(ByVal table As DataTable)
        Dim dt As New DataTable
        Dim i As Integer = 0
        dt = table
        con = con_obj.connction
        con.Open()
        For Each dr As DataRow In dt.Rows
            cmd = New SqlCommand("insert into tbl_detailBill(detail_billNo,detail_itemCatagory,detail_itemName,detail_itemPrice,detail_itemQuntity,detail_itemTotalprice)values('" + dr.Item("Bill No").ToString + "','" + dr.Item("Item_cat").ToString + "','" + dr.Item("Item_name").ToString + "','" + dr.Item("Item_price").ToString + "','" + dr.Item("Item_quntity").ToString + "','" + dr.Item("Item_total").ToString + "')", con)
            cmd.ExecuteNonQuery()
        Next
        con.Close()
    End Sub

    Sub updatestock(ByVal table As DataTable, ByVal dataGridView1 As DataGridView)
        Dim cmd1 As SqlCommand
        Dim u_stock As Integer
        Dim item_namegrd As String = ""
        Dim item_nametable As String = ""

        con.Open()
        For dr1 = 0 To dataGridView1.Rows.Count - 2 Step 1
            Try
                item_namegrd = dataGridView1.Rows(dr1).Cells(1).Value.ToString
            Catch ex As Exception
                MsgBox("object reference")
            End Try
            For dt_row = 0 To table.Rows.Count - 1
                item_nametable = table(dt_row).Item("Item_name").ToString
                If item_namegrd = item_nametable Then
                    If IsNumeric(dataGridView1.Rows(dr1).Cells(4).Value) = True Then
                        MsgBox("yes")
                        u_stock = CInt(dataGridView1.Rows(dr1).Cells(4).Value) - CInt(table(dt_row).Item("Item_quntity").ToString)
                        cmd1 = New SqlCommand("update tbl_Item set Item_stock='" & u_stock & "' where Item_name='" & item_namegrd & "'", con)
                        cmd1.ExecuteNonQuery()
                        dataGridView1.Rows(dr1).Cells(4).Value = u_stock
                        MsgBox("update success")
                    Else
                        MsgBox("no")
                    End If
                End If
            Next


        Next

        con.Close()
    End Sub
End Class
