Imports System.Xml
Imports System.Data
Imports System.IO
Imports System.Data.SqlClient
Public Class Table_billing
    Dim con As SqlConnection
    Dim con_obj As New connection
    Dim dr As SqlDataReader
    Dim dt As DataTable
    Dim ds As DataSet
    Dim cmd As SqlCommand
    Dim adp As SqlDataAdapter
    Dim design_obj As New Design_app()
    Dim bill_no As Integer

    '================================================================Flow layout=================================================================

    Public Sub btn_add_dynamic()
        Dim config As New Confighandler
        Dim i = config.getconfig
        FlowLayoutPanel1.Controls.Clear()
        Dim count = 0
        Dim btn_add(i) As Button
        For count = count + 1 To i
            Dim xml_path As String
            btn_add(count) = New Button
            btn_add(count).Text = count
            xml_path = "table'" & count.ToString & "'.xml"
            If File.Exists(xml_path) = True Then
                btn_add(count).ForeColor = Color.White
                btn_add(count).BackgroundImage = Form1.My.Resources.activebutton
            Else
                btn_add(count).BackgroundImage = Form1.My.Resources.blue
                btn_add(count).ForeColor = Color.Black
            End If
            btn_add(count).Width = 60
            btn_add(count).Height = 40
            btn_add(count).BackgroundImageLayout = ImageLayout.Zoom
            btn_add(count).CausesValidation = False
            FlowLayoutPanel1.Controls.Add(btn_add(count))
            AddHandler btn_add(count).Click, AddressOf btn_click
            If count = 50 Then
                Button1.Visible = True
            Else
                Button1.Visible = False
            End If
        Next
    End Sub

    Public Sub btn_click(ByVal sender As Object, ByVal e As EventArgs)
        Dim btn As Button = sender
        btn.BackgroundImageLayout = ImageLayout.Stretch
        btn.BackgroundImage = Form1.My.Resources.activebutton
        Me.Label_table.Text = btn.Text
        varification_xml(btn)
    End Sub

    '=================================================================Panel 1======================================================================

    Private Sub Table_billing_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        con = con_obj.connction
        btn_add_dynamic()
        'load1()
        load_menu()
        ComboBox1_fill()
        billOfdays()
    End Sub

    Private Sub varification_xml(ByVal btn As Button)
        Dim _addtodgv As Xml_Handler = New Xml_Handler
        Dim xml_path As String
        xml_path = "table'" & btn.Text & "'.xml"
        If File.Exists(xml_path) = True Then
            _addtodgv.bind_gridview_from_xml(btn.Text)
        Else
            _addtodgv.Create_xml_firsttime(btn.Text)
            _addtodgv.bind_gridview_from_xml(btn.Text)
        End If
    End Sub
    'ADD new Table
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim i As Integer
        i = FlowLayoutPanel1.Controls.Count
        Dim btn_add As New Button
        btn_add.BackgroundImage = Form1.My.Resources.blue
        btn_add.ForeColor = Color.White
        btn_add.Width = 60
        btn_add.Height = 40
        btn_add.Text = i + 1
        FlowLayoutPanel1.Controls.Add(btn_add)
        AddHandler btn_add.Click, AddressOf btn_click
    End Sub

    ' Billing Events 

    'Add item button click event


    Private Sub btn_remove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_remove.Click
        Dim row_no As Int32
        row_no = DataGridView1.Rows.Count
        If row_no = 0 Then
            MsgBox("No Item For Delete")
        Else
            Dim i As Integer = DataGridView1.SelectedRows.Item(0).Index
            Dim selectedrow As DataGridViewRow = DataGridView1.Rows.Item(i)
            Dim remove_row As Xml_Handler = New Xml_Handler()
            DataGridView1.Rows.Remove(selectedrow)
            remove_row.addnew_row(Me.Label_table.Text)
            cal_final_total()
        End If
    End Sub

    ' table cancel button
    Private Sub btn_cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_cancel.Click
        If Label_table.Text = "" Then
            MsgBox("No Chance")
        Else
            Me.DataGridView1.Rows.Clear()
            File.Delete("table'" & Label_table.Text & "'.xml")
            Label_table.Text = ""
            Me.btn_add_dynamic()
        End If
    End Sub

    'close button
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Me.Close()
        ' Me.Dispose()
        design_obj._set_home()
    End Sub

    Private Sub lbl_total_final_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbl_total_final.TextChanged
        cal_duebill()
    End Sub
    'billing public methods

    Public Sub cal_final_total()
        Dim _totalAmount As Double = 0
        Dim i As Integer = DataGridView1.Rows.Count
        For j As Integer = 0 To i - 1
            _totalAmount = _totalAmount + DataGridView1.Rows(j).Cells(3).Value
        Next
        lbl_total_final.Text = _totalAmount.ToString
    End Sub

    Private Sub Label_table_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label_table.TextChanged
        'load1()
    End Sub
    Public Sub load1()
        If Label_table.Text = "" Then
            Panel1.Enabled = False
            Panel2.Enabled = False
        Else
            Panel1.Enabled = True
            Panel2.Enabled = True
        End If
    End Sub
    '=================================================================Grid View events======================================================================
    '=================================================================Billing Detail======================================================================
    '=================================================================Panel 2======================================================================

    Private Sub rdb_cash_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdb_cash.CheckedChanged
        Panel5.Enabled = True
        Panel4.Enabled = False
        TextBox1.Text = ""
        TextBox2.Text = ""

    End Sub

    Private Sub rdb_Creadit_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdb_Creadit.CheckedChanged
        Panel4.Enabled = True
        Panel5.Enabled = False
        TextBox1.Text = ""
        TextBox2.Text = ""
    End Sub
    Private Sub btn_add_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim con As New SqlConnection
        Dim cmd As New SqlCommand
        Dim dr As SqlDataReader
        Dim con_obj As New connection
        con = con_obj.connction()
        con.Open()
        cmd.CommandText = "select * From tbl_RoomRegistration where Customer_IdProof='" + TextBox2.Text + "' "
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
    'button print click

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click

        If RadioButton1.Checked = True Then
            Button5_Click(sender, e)
        Else

        End If


        If Label_table.Text = "" Then
            MsgBox("Invalid Action")
        Else
            If TextBox3.Text = "" Then
                MsgBox("Received Amount")
            Else
                Dim dt As DataTable = New DataTable
                bill_no = get_billno()
                With dt
                    .Columns.Add("Item_cat")
                    .Columns.Add("Item_name")
                    .Columns.Add("Item_price")
                    .Columns.Add("Item_quntity")
                    .Columns.Add("Item_total")
                    .Columns.Add("Item_totalbil")
                    .Columns.Add("Date")
                    .Columns.Add("Bill No")
                End With
                For Each rows As DataGridViewRow In Me.DataGridView1.Rows
                    dt.Rows.Add(rows.Cells(0).Value, rows.Cells(1).Value, rows.Cells(2).Value, rows.Cells(3).Value, rows.Cells(4).Value, lbl_total_final.Text, DateTimePicker1.Value, bill_no)
                Next

                ''-----------------------add to global datatable----------
                Dim _id As String = "Anomus"
                Dim _amount As String
                Dim _date As String
                Dim tmp_date As Date
                Dim tmp_time As String
                Dim val_obj As New Validation()
                _amount = lbl_total_final.Text
                tmp_date = Convert.ToDateTime(DateTimePicker1.Value)
                tmp_time = Convert.ToDateTime(DateTimePicker1.Value).TimeOfDay.ToString.Substring(0, 5)
                _date = tmp_date.Date
                If rdb_cash.Checked = True Then
                    If TextBox1.Text = "" Then
                        _id = "Anomus"
                    Else
                        _id = TextBox1.Text
                    End If
                    val_obj.Global_insert(_amount, _id, _date.ToString, tmp_time, bill_no)
                    Global_insert(dt)
                    DataGridView3.Rows.Add(_id, _amount, tmp_time, bill_no)
                End If
                If rdb_Creadit.Checked = True Then
                    _id = TextBox2.Text
                    If TextBox2.Text = "" Then
                        MsgBox("Enter User id Please")
                    Else
                        val_obj.id = TextBox2.Text
                        Dim _result_bool As Integer = val_obj.varification()
                        If _result_bool = 1 Then
                            val_obj.Global_insert(_amount, _id, _date.ToString, tmp_time, bill_no)
                            Global_insert(dt)
                            fetch_update(TextBox2.Text)
                            DataGridView3.Rows.Add(TextBox2.Text, _amount, tmp_time, bill_no)
                        Else
                            MsgBox("User Id is Wrong")
                        End If
                    End If
                End If
                ' load_menu()
            End If
        End If
    End Sub
    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Label_table.Text = "" Then
            MsgBox("Invalid Action")
        Else
            If DataGridView1.Rows.Count = 0 Then
                MsgBox("No Item For Bill")
            Else
                Printbill.Focus()
                Printbill.Show()
                Printbill.StartPosition = FormStartPosition.CenterScreen
                Printbill.WindowState = FormWindowState.Normal
                Printbill.MdiParent = MDIPare
            End If
        End If
    End Sub

    Public Function get_billno() As Integer
        Dim con As New SqlConnection
        Dim cmd As New SqlCommand
        Dim dr As SqlDataReader
        Dim con_obj As New connection
        Dim _billNo = 0
        con = con_obj.connction()
        con.Open()
        cmd.CommandText = "select * From customer_billdetail "
        cmd.CommandType = CommandType.Text
        cmd.Connection = con
        dr = cmd.ExecuteReader
        While dr.Read()
            _billNo = _billNo + 1
        End While
        _billNo = _billNo + 1
        dr.Close()
        con.Close()
        Return _billNo
    End Function

    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged
        cal_duebill()
    End Sub

    Public Sub cal_duebill()
        Dim rec_amount As Double = 0
        If TextBox3.Text = "" Then
            rec_amount = 0
        Else
            rec_amount = Convert.ToDouble(TextBox3.Text)
        End If
        TextBox4.Text = rec_amount - Convert.ToDouble(lbl_total_final.Text)
    End Sub

    Private Sub Global_insert(ByVal table As DataTable)
        'insert into detail of bill in detailedlist
        Dim val_obj As New Validation()
        val_obj.detailedbill_insert(table)
        val_obj.updatestock(table, DataGridView2)
        DataGridView1.Rows.Clear()
        Try
            My.Computer.FileSystem.DeleteFile("table'" & Label_table.Text & "'.xml")
        Catch ex As Exception
            MsgBox("Invalid Action")
        End Try

        btn_add_dynamic()
    End Sub

    Private Sub fetch_update(ByVal p1 As String)
        ' Dim con As New SqlConnection
        Dim cmd1 As New SqlCommand
        Dim cmd2 As SqlCommand
        Dim credit As Double = 0
        Dim debit As Double = 0
        Dim Ucredit As Double = 0
        Dim Udebit As Double = 0
        Dim dr1 As SqlDataReader
        ' Dim con_obj As New connection
        ' con = con_obj.connction()
        con.Open()
        cmd1.CommandText = "select * From tbl_RoomRegistration where Customer_IdProof='" & p1 & "'  "
        cmd1.CommandType = CommandType.Text
        cmd1.Connection = con
        dr1 = cmd1.ExecuteReader
        While dr1.Read()
            Try
                credit = CDbl(dr1(7))
                debit = CDbl(dr1(8))
            Catch ex As Exception
                'MsgBox(ex.ToString)
                credit = 0
                debit = 0
            End Try
        End While
        dr1.Close()

        Try
            If debit > 0 Then
                Udebit = debit - CDbl(lbl_total_final.Text)
                Ucredit = 0
                If Udebit < 0 Then
                    Ucredit = Math.Abs(Udebit)
                    Udebit = 0
                End If
            Else
                Ucredit = credit + CInt(lbl_total_final.Text)
                Udebit = 0
            End If
            MsgBox(Ucredit.ToString + "    " + Udebit.ToString)

            cmd2 = New SqlCommand("update tbl_RoomRegistration set Customer_credit='" & Ucredit & "' ,Customer_debit='" & Udebit & "' where Customer_IdProof='" & p1 & "'", con)
            cmd2.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        con.Close()

    End Sub
    '=====================================Key Events    ======================================================================
    Private Sub TextBox2_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox2.KeyPress
        If Asc(e.KeyChar) = 13 Then
            btn_add_Click(sender, e)
        End If
    End Sub

    Private Sub DataGridView1_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyDown
        If e.KeyCode = Keys.Delete Then
            btn_remove_Click(sender, e)
        End If
    End Sub

    Private Sub Button1_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.MouseEnter
        Button1.BackgroundImage = My.Resources.green1
        Button1.ForeColor = Color.White
    End Sub

    Private Sub Button1_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.MouseLeave
        Button1.BackgroundImage = My.Resources.green
        Button1.ForeColor = Color.Black
    End Sub

    Private Sub load_menu()
        Dim dr_this As SqlDataReader
        Dim dt_cat As DataTable = New DataTable
        Dim item_stock As String
        con.Open()
        Dim str As String = "select * from tbl_Item"
        cmd = New SqlCommand(str, con)
        dr_this = cmd.ExecuteReader
        DataGridView2.Rows.Clear()
        While dr_this.Read
            If IsDBNull(dr_this(4)) = True Then
                item_stock = "---"
            Else
                item_stock = CInt(dr_this(4))
            End If
            DataGridView2.Rows.Add(dr_this(0), dr_this(1), dr_this(2), dr_this(3), item_stock)
        End While
        con.Close()
        For i As Integer = 0 To DataGridView2.Rows.Count - 1 Step 1
            If IsNumeric(DataGridView2.Rows(i).Cells(4).Value) Then
                If CInt(DataGridView2.Rows(i).Cells(4).Value) < 20 Then
                    'MsgBox("stock under")
                    DataGridView2.Rows(i).DefaultCellStyle.BackColor = Color.Red
                End If
            End If
        Next
    End Sub
    'by entering item code and quantity
    'item added to  table
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            For Each row As DataGridViewRow In DataGridView2.Rows
                If row.Cells(0).Value = TextBox6.Text Then
                    DataGridView1.Rows.Add(row.Cells(3).Value, row.Cells(1).Value, row.Cells(2).Value, Convert.ToInt32(TextBox7.Text), CDec(row.Cells(2).Value) * Convert.ToInt32(TextBox7.Text))
                End If
            Next
            cal_final_total()
            Dim add_data As Xml_Handler = New Xml_Handler
            add_data.addnew_row(Me.Label_table.Text)
        Catch ex As Exception
            MsgBox("Error")
        End Try
        TextBox6.Text = ""
        TextBox7.Text = ""
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub TextBox7_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox7.KeyDown
        If e.KeyCode = Keys.Enter Then
            Button2_Click(sender, e)
        End If

    End Sub

    Private Sub TextBox6_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox6.KeyDown

        If e.KeyCode = Keys.Enter Then
            Button2_Click(sender, e)
        End If
    End Sub

    Private Sub ComboBox1_fill()
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
        TextBox2.Text = ComboBox1.SelectedValue
    End Sub

    Private Sub billOfdays()
        con.Open()
        Try
            cmd = New SqlCommand("select * from customer_billdetail where Customer_BillDate='" & System.DateTime.Today.Date & "'", con)
            dr = cmd.ExecuteReader()
            While dr.Read()
                DataGridView3.Rows.Add(dr(0), dr(1), dr(3), dr(4))
            End While
            dr.Close()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

        con.Close()
    End Sub

    Private Sub TextBox3_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox3.KeyPress
        If Not IsNumeric(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub
    Dim rwinde As Integer
    Private Sub DataGridView3_RowHeaderMouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView3.RowHeaderMouseClick
        rwinde = e.RowIndex
        If e.Button = Windows.Forms.MouseButtons.Right Then
            Dim cntsrip As New ContextMenuStrip
            cntsrip.Items.Add("Show Detail Bill", Nothing, onClick:=AddressOf item1)
            cntsrip.Items.Add("Remove Bill", Nothing, onClick:=AddressOf item2)
            cntsrip.Items.Add("Edit Bill", Nothing, onClick:=AddressOf item3)
            'cntsrip.Show()
            DataGridView3.ContextMenuStrip = cntsrip
        End If
    End Sub
    Private Function item1() As Object
        MsgBox(rwinde.ToString)
        Return Nothing
    End Function
    Private Function item2() As Object
        MsgBox(rwinde.ToString)
        Return Nothing
    End Function
    Private Function item3() As Object
        MsgBox(rwinde.ToString)
        Return Nothing
    End Function


End Class