Imports System.Data.SqlClient
Public Class Create_bill
    Private Sub Create_bill_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Panel1.Enabled = True

    End Sub

    Private Sub rdb_Creadit_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdb_Creadit.CheckedChanged
        Panel1.Enabled = False
        Panel2.Enabled = True
        TextBox1.Text = ""
        TextBox2.Text = ""
        btn_forgetId.Visible = True
    End Sub

    Private Sub rdb_cash_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdb_cash.CheckedChanged
        Panel1.Enabled = True
        Panel2.Enabled = False
        TextBox1.Text = ""
        TextBox2.Text = ""
        btn_forgetId.Visible = False
    End Sub


    Private Sub btn_add_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_add.Click
        Dim con As New SqlConnection
        Dim cmd As New SqlCommand
        Dim dr As SqlDataReader
        Dim con_obj As New connection
        con = con_obj.connction()
        con.Open()
        cmd.CommandText = "select * From Customer_detail where Customer_AccountNo='" + TextBox2.Text + "' "
        cmd.CommandType = CommandType.Text
        cmd.Connection = con
        dr = cmd.ExecuteReader
        If dr.Read = True Then
            TextBox1.Enabled = True
            TextBox1.Text = dr(1).ToString
        Else
            MsgBox("User Id Incorect")
        End If
        con.Close()
    End Sub

    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged

    End Sub

    Private Sub btn_print_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_print.Click
        If rdb_Creadit.Checked = True Then
            Dim con As New SqlConnection
            Dim cmd As New SqlCommand
            Dim dr As SqlDataReader
            Dim con_obj As New connection
            con = con_obj.connction()
            con.Open()
            cmd.CommandText = "insert into  customer_billdetail(Customer_accountNo,Customer_customerBill,Customer_BillDate) values('" + TextBox2.Text + "','" + label_total_bill.Text + "','" + Label7.Text + "')"
            cmd.CommandType = CommandType.Text
            cmd.Connection = con
            dr = cmd.ExecuteReader
            If dr.Read = True Then
                TextBox1.Enabled = True
                TextBox1.Text = dr(1).ToString

            Else
                MsgBox("User Id Incorect")
            End If

            con.Close()
        End If
        print_Bill()
    End Sub
    Private Sub print_Bill()
        Dim dt As DataTable = New DataTable
        With dt
            .Columns.Add("Item_name")
            .Columns.Add("Item_price")
            .Columns.Add("Item_quntity")
            .Columns.Add("Item_total")
            .Columns.Add("Item_totalbil")
            .Columns.Add("Date")
        End With
        For Each rows As DataGridViewRow In Table_billing.DataGridView1.Rows

            dt.Rows.Add(rows.Cells(0).Value, rows.Cells(1).Value, rows.Cells(2).Value, rows.Cells(3).Value, Table_billing.lbl_FinalTotal.Text, Table_billing.DateTimePicker1.Value)
        Next
        If dt.Rows.Count = 0 Then
            MsgBox("No Data Found")
        Else
            Dim rptdoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
            rptdoc = New Bill_CrystalReport
            rptdoc.SetDataSource(dt)
            Crystel_reportviewer.CrystalReportViewer1.ReportSource = rptdoc
            Crystel_reportviewer.CrystalReportViewer1.Refresh()
            Crystel_reportviewer.ShowDialog()

        End If

    End Sub

    Private Sub btn_forgetId_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_forgetId.Click
        ' Regular_customer_List.MdiParent = MDIPare
        Regular_customer_List.Focus()
        Regular_customer_List.Show()


    End Sub
End Class