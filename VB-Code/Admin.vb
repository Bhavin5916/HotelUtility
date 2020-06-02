Imports System.Data.SqlClient
Imports System.Xml
Public Class Admin
    Dim con As SqlConnection
    Dim con_obj As New connection
    Dim dr As SqlDataReader
    Dim dt As DataTable
    Dim ds As DataSet
    Dim cmd As SqlCommand
    Dim adp As SqlDataAdapter
    Dim design_obj As New Design_app()
    Dim enter_key As Boolean = False



    Dim rowindex As Integer
    Dim colindex As Integer

    '************************************ Tab1 **********************************************
    Private Sub Admin_panel_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.MdiParent = MDIPare
        Me.Width = MDIPare.Width
        Me.Dock = DockStyle.Fill
        Me.Location = New Point(0, MDIPare.ToolStrip1.Height)
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.Focus()
        Me.Show()
        fill_grid1()

        'tab 5
        fillcombo2()
        ToolTip1.ToolTipIcon = ToolTipIcon.Info
        ToolTip1.SetToolTip(Me.TextBox1, "New Item Category  ")
        ToolTip1.SetToolTip(Me.TextBox4, "New Item Name")
        ToolTip1.SetToolTip(Me.TextBox3, "Item Prize")


    End Sub
    Private Sub DataGridView1_RowHeaderMouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.RowHeaderMouseDoubleClick, DataGridView1.CellMouseClick
        rowindex = e.RowIndex
        colindex = e.ColumnIndex
        Label1.Text = DataGridView1.Rows(e.RowIndex).Cells(0).Value.ToString
        fill_grid2()
    End Sub
    Private Sub Add_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Add.Click
        If TextBox1.Text = "Add New Catagory" Or TextBox1.Text = "" Then
            MsgBox("write new catagory")
        Else

            Dim query As String
            con = New SqlConnection()
            con = con_obj.connction()
            con.Open()
            query = "insert into tbl_foodcatagory(Food_catagary) values('" & TextBox1.Text & "')"
            cmd = New SqlCommand(query, con)
            cmd.ExecuteNonQuery()
            con.Close()
        End If
        fill_grid1()
    End Sub
    Public Sub fill_grid1()
        Dim dt As New DataTable
        With dt
            dt.Columns.Add("CAT", GetType(System.String))
            dt.Columns.Add("Rate")
        End With

        Dim query As String
        con = New SqlConnection()
        con = con_obj.connction()
        con.Open()
        ds = New DataSet()
        query = "Select * from tbl_foodcatagory"
        cmd = New SqlCommand(query, con)
        dr = cmd.ExecuteReader
        Dim rw As DataRow
        While dr.Read()
            rw = dt.NewRow()
            rw("CAT") = dr(0).ToString
            dt.Rows.Add(rw)
        End While
        con.Close()
        DataGridView1.DataSource = dt
    End Sub
    Public Sub fill_grid2()
        Dim dt As New DataTable
        With dt
            dt.Columns.Add("Item", GetType(System.String))
            dt.Columns.Add("Rate")
        End With

        Dim query As String
        con = New SqlConnection()
        con = con_obj.connction()
        con.Open()
        'ds = New DataSet()
        query = "Select * from tbl_Item where Item_catagory='" & Label1.Text & "'"
        cmd = New SqlCommand(query, con)
        dr = cmd.ExecuteReader
        Dim rw As DataRow
        While dr.Read()
            rw = dt.NewRow()
            rw("Item") = dr(1).ToString

            If IsDBNull(dr(2)) = True Or Not IsNumeric(dr(2)) Then
                rw("Rate") = "Null"
            Else
                rw("Rate") = Convert.ToDouble(dr(2))
            End If
            dt.Rows.Add(rw)
        End While
        con.Close()
        DataGridView2.DataSource = dt
    End Sub
    'Private Sub m_enter(ByVal sender As Object, ByVal e As EventArgs)
    '    Dim txt As New TextBox
    '    txt = sender
    '    If txt.Text = "Add New" Or txt.Text = "" Or txt.Text = "Prize" Then
    '        txt.Text = ""
    '    End If
    'End Sub
    'Private Sub m_leave(ByVal sender As Object, ByVal e As EventArgs)
    '    Dim txt As New TextBox
    '    txt = sender
    '    If txt.Text = "" Then
    '        If txt.Name = "TextBox1" Or txt.Name = "TextBox4" Then
    '            txt.Text = "Add New"
    '        Else
    '            txt.Text = "Prize"
    '        End If
    '    End If
    'End Sub
    '******************************NEW ITEM ADD*************************************************************
    Private Sub Button2_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If TextBox1.Text = "Add New Catagory" Or TextBox1.Text = "" Then
            MsgBox("write new catagory")
        Else

            Dim query As String
            con = New SqlConnection()
            con = con_obj.connction()
            con.Open()
            query = "insert into tbl_Item(Item_name,Item_rate,Item_catagory) values('" & TextBox4.Text & "','" & TextBox3.Text & "','" & Label1.Text & "')"
            cmd = New SqlCommand(query, con)
            cmd.ExecuteNonQuery()
            con.Close()
        End If
        fill_grid2()
    End Sub
    Private Sub Panel1_MouseEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Panel1.MouseEnter
        'AddHandler TextBox1.MouseEnter, AddressOf m_enter
        'AddHandler TextBox3.MouseEnter, AddressOf m_enter
        'AddHandler TextBox4.MouseEnter, AddressOf m_enter

        'AddHandler TextBox1.MouseLeave, AddressOf m_leave
        'AddHandler TextBox3.MouseLeave, AddressOf m_leave
        'AddHandler TextBox4.MouseLeave, AddressOf m_leave
    End Sub

    Dim after_edit As String
    Dim before_edit As String
    Private Sub DataGridView1_CellBeginEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellCancelEventArgs) Handles DataGridView1.CellBeginEdit
        before_edit = DataGridView1.CurrentCell.Value.ToString
    End Sub
    Private Sub DataGridView1_CellEndEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellEndEdit
        Dim dtgrd As DataGridView = sender
        after_edit = DataGridView1.CurrentCell.Value.ToString
        Dim grdv As grdvhandle = New grdvhandle(before_edit, after_edit, sender, dtgrd.CurrentCell.ColumnIndex)
        grdv.update("update tbl_foodcatagory set Food_catagary='" & after_edit & "' where Food_catagary='" & before_edit & "'")
        grdv.update("update tbl_Item set Item_catagory='" & after_edit & "' where Item_catagory='" & before_edit & "'")
    End Sub
    Private Sub DataGridView2_CellBeginEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellCancelEventArgs) Handles DataGridView2.CellBeginEdit
        before_edit = DataGridView2.CurrentCell.Value.ToString
    End Sub
    Private Sub DataGridView2_CellEndEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView2.CellEndEdit
        after_edit = DataGridView2.CurrentCell.Value.ToString
        Dim dtgrd As DataGridView = sender
        Dim grdv As grdvhandle = New grdvhandle(before_edit, after_edit, sender, dtgrd.CurrentCell.ColumnIndex)
        grdv.update_grd2()
    End Sub


    '************************************ Tab2 **********************************************
    Private Sub TabControl1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabControl1.Enter
        If TabControl1.TabIndex = 0 Then
        End If
        If TabControl1.TabIndex = 1 Then
            fill_listpackages()
            fill_listrooms()
        End If
    End Sub
    '*************************************Packeges************************************************************
    Public Sub fill_listpackages()
        Dim query As String
        Dim index As Integer = 0
        con = New SqlConnection()
        con = con_obj.connction()
        con.Open()
        ds = New DataSet()
        query = "Select * from tbl_room_package"
        cmd = New SqlCommand(query, con)
        dr = cmd.ExecuteReader
        While dr.Read()
            Packages.ItemHeight = 6
            index = index + 1
            Packages.Items.Add(dr(0))
        End While
        Packages.Items.Insert(index, "Add new Package")
        con.Close()

    End Sub

    Private Sub Packages_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Packages.SelectedIndexChanged
        If Packages.SelectedItem = "Add new Package" Then
            Label10.Text = "Package Name"
            TextBox12.Visible = True
        Else
            Label10.Text = ""
            TextBox12.Visible = False
            Packages.ForeColor = Color.ForestGreen
            get_dataroompackage()
        End If

    End Sub

    Private Sub btn_save_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_save.Click
        If Packages.SelectedItem = "Add new Package" Then
            insert_newpacakge()
            fill_listpackages()
            get_dataroompackage()
        Else
            Label10.Text = ""
            TextBox12.Visible = False
            update_roompackage()
        End If

    End Sub

    Public Sub insert_newpacakge()
        Dim query As String
        con = New SqlConnection()
        con = con_obj.connction()
        con.Open()
        query = "insert into tbl_room_package(RoomPackage,RoomCharge_day,Service1,Service2,Service3,Service4,service5,Tax) values('" & TextBox12.Text & "','" & TextBox5.Text & "','" & TextBox6.Text & "','" & TextBox7.Text & "','" & TextBox8.Text & "','" & TextBox9.Text & "','" & TextBox10.Text & "','" & TextBox11.Text & "',)"
        cmd = New SqlCommand(query, con)
        cmd.ExecuteNonQuery()
        con.Close()
    End Sub

    Public Sub get_dataroompackage()
        Label10.Text = ""
        TextBox12.Visible = False
        Dim query As String
        con = New SqlConnection()
        con = con_obj.connction()
        con.Open()
        ds = New DataSet()
        query = "Select * from tbl_room_package where RoomPackage='" & Packages.SelectedItem & "'"
        cmd = New SqlCommand(query, con)
        dr = cmd.ExecuteReader
        While dr.Read()
            Label10.Text = dr(0).ToString
            TextBox5.Text = dr(1).ToString
            TextBox6.Text = dr(2).ToString
            TextBox7.Text = dr(3).ToString
            TextBox8.Text = dr(4).ToString
            TextBox9.Text = dr(5).ToString
            TextBox10.Text = dr(6).ToString
            TextBox11.Text = dr(7).ToString
        End While
        con.Close()
    End Sub

    Public Sub update_roompackage()
        Dim query As String
        con = New SqlConnection()
        con = con_obj.connction()
        con.Open()
        ds = New DataSet()
        query = "update tbl_room_package set RoomCharge_day='" & TextBox5.Text & "',Service1='" & TextBox6.Text & "',Service2='" & TextBox7.Text & "',Service3='" & TextBox7.Text & "',Service4='" & TextBox8.Text & "',Service5='" & TextBox10.Text & "',Tax='" & TextBox11.Text & "' where RoomPackage='" & Packages.SelectedItem & "'"
        cmd = New SqlCommand(query, con)
        cmd.ExecuteNonQuery()
        con.Close()
    End Sub

    Private Sub btn_cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_cancel.Click
        get_dataroompackage()
    End Sub

    Private Sub fill_listrooms()
        Dim query As String
        Dim index As Integer = 0
        con = New SqlConnection()
        con = con_obj.connction()
        con.Open()
        ds = New DataSet()
        query = "Select Rom_No from tbl_room"
        cmd = New SqlCommand(query, con)
        dr = cmd.ExecuteReader
        While dr.Read()
            Rooms.ItemHeight = 6
            index = index + 1
            Rooms.Items.Add(dr(0))
        End While
        'Packages.Items.Insert(index, "Add new Package")
        con.Close()
    End Sub

    Private Sub Rooms_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Rooms.SelectedIndexChanged
        Dim query As String
        Dim index As Integer = 0
        con = New SqlConnection()
        con = con_obj.connction()
        con.Open()
        ds = New DataSet()
        query = "Select * from tbl_room where Rom_No='" & Rooms.SelectedItem.ToString & " '"
        cmd = New SqlCommand(query, con)
        dr = cmd.ExecuteReader
        While dr.Read()
            Label15.Text = dr(1).ToString
            Label16.Text = dr(3).ToString
            Label17.Text = dr(2).ToString
        End While
        'Packages.Items.Insert(index, "Add new Package")
        con.Close()
    End Sub

    Private Sub Label16_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label16.Click
        Label10.Text = ""
        TextBox12.Visible = False
        Dim query As String
        con = New SqlConnection()
        con = con_obj.connction()
        con.Open()
        ds = New DataSet()
        query = "Select * from tbl_room_package where RoomPackage='" & Label16.Text & "'"
        cmd = New SqlCommand(query, con)
        dr = cmd.ExecuteReader
        While dr.Read()
            Label10.Text =   dr(0).ToString
            TextBox5.Text =  dr(1).ToString
            TextBox6.Text =  dr(2).ToString
            TextBox7.Text =  dr(3).ToString
            TextBox8.Text  =  dr(4).ToString
            TextBox9.Text  =  dr(5).ToString
            TextBox10.Text = dr(6).ToString
            TextBox11.Text = dr(7).ToString
        End While
        Packages.SelectedItem = Label16.Text
        con.Close()
    End Sub

    '*******************************************See All Items**********************************
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim dt As New DataTable
        With dt
            dt.Columns.Add("Item", GetType(System.String))
            dt.Columns.Add("Rate")
            dt.Columns.Add("Cat")
        End With
        Dim query As String
        con = New SqlConnection()
        con = con_obj.connction()
        con.Open()
        ds = New DataSet()
        query = "Select * from tbl_Item"
        cmd = New SqlCommand(query, con)
        dr = cmd.ExecuteReader
        Dim rw As DataRow
        While dr.Read()
            rw = dt.NewRow()
            rw("Item") = dr(1).ToString
            If IsDBNull(dr(2)) = True Then
                rw("Rate") = "Null"
            Else
                rw("Rate") = Convert.ToDouble(dr(2))
            End If
            rw("Cat") = dr(3).ToString
            dt.Rows.Add(rw)
        End While
        con.Close()
        DataGridView2.DataSource = dt
    End Sub

    Private Sub add_newcat()
        GroupBox1.Focus()

    End Sub
    Private Sub delete_cat()
        Throw New NotImplementedException
    End Sub
    Private Sub edit_cat()
        Dim category As String = DataGridView1.Rows(rowindex).Cells(0).Value
        TextBox2.Text = DataGridView1.Rows(rowindex).Cells(0).Value
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If TextBox2.Text = "" Or TextBox2.Text = "Add New" Then
        Else
            con.Open()
            Dim query As String = "update tbl_foodcatagory set Food_catagary='" & TextBox2.Text & "' where Food_catagary='" & DataGridView1.Rows(rowindex).Cells(0).Value & "'"
            Dim query1 As String = "update tbl_Item set Item_catagory='" & TextBox2.Text & "' where  Item_catagory='" & DataGridView1.Rows(rowindex).Cells(0).Value & "'"
            cmd = New SqlCommand(query, con)
            cmd.ExecuteNonQuery()
            cmd = New SqlCommand(query1, con)
            cmd.ExecuteNonQuery()
            con.Close()
            fill_grid1()
            fill_grid2()
        End If
    End Sub

    Private Sub DataGridView2_CellMouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView2.CellMouseClick
        rowindex = e.RowIndex
        colindex = e.ColumnIndex
        If e.Button = Windows.Forms.MouseButtons.Right Then
            Dim cntextmenu As ContextMenuStrip = New ContextMenuStrip
            cntextmenu.Items.Add("Add New Item", image:=My.Resources.add, onClick:=AddressOf add_newitem)
            cntextmenu.Items.Add("Delete", image:=My.Resources.delete, onClick:=AddressOf delete_item).Enabled = False
            cntextmenu.Items.Add("Edit", image:=My.Resources.edit, onClick:=AddressOf edit_item)
            cntextmenu.Show()
            cntextmenu.Location = New Point(DataGridView2.Location.X, DataGridView2.Location.Y + 200)
        End If
    End Sub
    Private Sub add_newitem()
        GroupBox3.Focus()
    End Sub
    Private Sub delete_item()
        Throw New NotImplementedException
    End Sub
    Private Sub showitem()
        Label1.Text = DataGridView1.Rows(rowindex).Cells(0).Value.ToString
        fill_grid2()
    End Sub
    Private Sub edit_item()
        Dim Item As String = DataGridView1.Rows(rowindex).Cells(0).Value
        Dim prize As String = DataGridView2.Rows(rowindex).Cells(1).Value
        TextBox13.Text = DataGridView2.Rows(rowindex).Cells(0).Value
        TextBox14.Text = DataGridView2.Rows(rowindex).Cells(1).Value
    End Sub
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        If TextBox13.Text = "" Or TextBox13.Text = "Add New" Or TextBox14.Text = "" Or TextBox14.Text = "Prize" Then

        Else
            con.Open()
            Dim query As String = "update tbl_Item set Item_Name='" & TextBox13.Text & "',Item_rate='" + TextBox14.Text + "' where Item_Name='" & DataGridView2.Rows(rowindex).Cells(0).Value & "'"
            cmd = New SqlCommand(query, con)
            cmd.ExecuteNonQuery()
            con.Close()
            fill_grid1()
        End If
        fill_grid2()
    End Sub
    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        Dim dtdate As DataTable = New DataTable
        Dim flag = 0
        If RadioButton3.Checked = True And Not ComboBox1.SelectedIndex = -1 Then
            con.Open()
            Dim str As String = "select * from customer_billdetail "
            cmd = New SqlCommand(str, con)
            adp = New SqlDataAdapter(cmd)
            adp.Fill(dtdate)
            con.Close()
            flag = 1
        Else
            flag = 0
            MsgBox("error")
        End If
        If flag = 1 Then
            MsgBox(dtdate.Rows.Count)
            Dim data As DataTable = New DataTable
            With data
                .Columns.Add("Customer_Account")
                .Columns.Add("Bill")
                .Columns.Add("Date")
            End With
            For Each dr As DataRow In dtdate.Rows
                Dim month As Integer = Convert.ToDateTime(dr(2)).Month
                If month = ComboBox1.SelectedIndex + 1 Then
                    data.Rows.Add(dr(0), dr(1), dr(2))
                Else

                End If
                DataGridView3.DataSource = data
                BindingSource1.DataSource = data
            Next
            'Dim rptdoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
            'rptdoc = New report1
            'rptdoc.SetDataSource(data)
            'Crystel_reportviewer.CrystalReportViewer1.ReportSource = rptdoc
            'Crystel_reportviewer.CrystalReportViewer1.Refresh()
            'Crystel_reportviewer.ShowDialog()
            'MsgBox(data.Rows.Count)
        End If

    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Dim dtdate As DataTable = New DataTable
        Dim flag = 0
        If RadioButton1.Checked = True Then
            con.Open()
            Dim str As String = "select * from customer_billdetail "
            cmd = New SqlCommand(str, con)
            adp = New SqlDataAdapter(cmd)
            adp.Fill(dtdate)
            con.Close()
            flag = 1
        Else
            flag = 0
            MsgBox("error")
        End If
        If flag = 1 Then
            MsgBox(dtdate.Rows.Count)
            Dim data As DataTable = New DataTable
            With data
                .Columns.Add("Customer_Account")
                .Columns.Add("Bill")
                .Columns.Add("Date")
            End With
            For Each dr As DataRow In dtdate.Rows
                Dim date1 As Date = Convert.ToDateTime(dr(2)).Date
                If date1 = Convert.ToDateTime(DateTimePicker1.Value).Date Then
                    data.Rows.Add(dr(0), dr(1), dr(2))
                    DataGridView3.DataSource = data
                    BindingSource1.DataSource = data
                Else
                End If
            Next
            'Dim rptdoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
            'rptdoc = New report1
            'rptdoc.SetDataSource(data)
            'Crystel_reportviewer.CrystalReportViewer1.ReportSource = rptdoc
            'Crystel_reportviewer.CrystalReportViewer1.Refresh()
            'Crystel_reportviewer.ShowDialog()
            'MsgBox(data.Rows.Count)
        End If
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Dim dtdate As DataTable = New DataTable
        Dim flag = 0
        If RadioButton2.Checked = True Then
            con.Open()
            Dim str As String = "select * from customer_billdetail "
            cmd = New SqlCommand(str, con)
            adp = New SqlDataAdapter(cmd)
            adp.Fill(dtdate)
            con.Close()
            flag = 1
        Else
            flag = 0
            MsgBox("error")
        End If
        If flag = 1 Then
            Dim data As DataTable = New DataTable
            With data
                .Columns.Add("Customer_Account")
                .Columns.Add("Bill")
                .Columns.Add("Date")
            End With
            For Each dr As DataRow In dtdate.Rows
                Dim date1 As Date = Convert.ToDateTime(dr(2)).Date
                If Not date1 < Convert.ToDateTime(DateTimePicker2.Value).Date And Not date1 > Convert.ToDateTime(DateTimePicker3.Value).Date Then
                    data.Rows.Add(dr(0), dr(1), dr(2))
                Else
                End If
                DataGridView3.DataSource = data
                BindingSource1.DataSource = data
            Next
            'Dim rptdoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
            'rptdoc = New report1
            'rptdoc.SetDataSource(data)
            'Crystel_reportviewer.CrystalReportViewer1.ReportSource = rptdoc
            'Crystel_reportviewer.CrystalReportViewer1.Refresh()
            'Crystel_reportviewer.ShowDialog()
        End If
    End Sub

    'Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
    '    Dim rptdoc As CrystalDecisions.CrystalReports.Engine.ReportDocument
    '    rptdoc = New report1
    '    rptdoc.SetDataSource(BindingSource1.DataSource)
    '    Crystel_reportviewer.CrystalReportViewer1.ReportSource = rptdoc
    '    Crystel_reportviewer.CrystalReportViewer1.Refresh()
    '    'Crystel_reportviewer.ShowDialog()
    '    Crystel_reportviewer.MdiParent = MDIPare
    '    Crystel_reportviewer.Show()
    '    Crystel_reportviewer.Focus()
    'End Sub




    'eMPLOYER tAB''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    Dim isabsent As String
    'Private Sub RadioButton4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton4.CheckedChanged, RadioButton6.CheckedChanged
    '    Dim dt1 As New DataTable
    '    isabsent = "No"
    '    con.Open()
    '    cmd = New SqlCommand("select * from tbl_attendence where employer_id='" & CInt(TextBox15.Text) & "' and employer_date='" & System.DateTime.Today.Date & "'", con)
    '    adp = New SqlDataAdapter(cmd)
    '    adp.Fill(dt1)
    '    If dt1.Rows.Count = 0 Then
    '        TextBox16.Text = MDIPare.Label1.Text
    '    Else
    '        TextBox16.Text = dt.Rows(0).Item(2).ToString
    '        ' TextBox17.Text =
    '    End If
    '    'If dr1.Read = False Then
    '    '    TextBox16.Text = MDIPare.Label1.Text
    '    'End If
    '    'While dr1.Read
    '    '    If IsDBNull(dr(2)) = True Or dr.Read = False Then
    '    '        TextBox16.Text = MDIPare.Label1.Text
    '    '        con.Open()
    '    '        cmd = New SqlCommand("insert into tbl_attendence(employer_id,employer_date,employer_entrytime,employer_leavingtime,employer_absent) values('" & TextBox15.Text & ",'" & System.DateTime.Today.Date & ",'" & TextBox16.Text & "','" & TextBox17.Text & "','" & isabsent & "') ", con)
    '    '        cmd.ExecuteNonQuery()
    '    '        con.Close()
    '    '    End If
    '    'End While
    '    con.Close()
    'End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        Dim debit As Integer = 0
        Dim credit As Integer = 0
        Dim pymenttype As String
        Dim query As String
        Dim ckeckno As String = ""
        If Not TextBox15.Text = "" Then
            If RadioButton6.Checked = True Then
                pymenttype = RadioButton6.Text
                MsgBox("insert into tbl_employersalary (employer_id,employer_onhandamount,employer_salarydate,employer_debit,employer_credit,employer_pyamenttype,employer_checkno) values('" & TextBox15.Text & "','" & TextBox18.Text & "','" & Today.Date.ToString & "','" & debit.ToString & "','" & credit.ToString & "','" & pymenttype & "','" & ckeckno & "' )")
                query = "insert into tbl_employersalary (employer_id,employer_onhandamount,employer_salarydate,employer_debit,employer_credit,employer_pyamenttype,employer_checkno) values('" & CDec(TextBox15.Text) & "','" & CDec(TextBox18.Text) & "','" & System.DateTime.Today.Date.ToString & "','" & debit & "','" & credit & "','" & pymenttype & "','" & ckeckno & "' )"

            Else
                pymenttype = RadioButton7.Text
                ckeckno = TextBox18.Text
                query = "insert into tbl_employersalary (employer_id,employer_onhandamount,employer_salarydate,employer_debit,employer_credit,employer_pyamenttype,employer_checkno) values('" & CInt(TextBox15.Text) & "','" & CInt(TextBox18.Text) & "','" & System.DateTime.Today.Date.ToString & "','" & debit.ToString & "','" & credit.ToString & "','" & pymenttype & "','" & ckeckno & "' )"
            End If
            con.Open()
            cmd = New SqlCommand(query, con)
            cmd.ExecuteNonQuery()
            con.Close()
            MsgBox("salary paid")
        Else
            MsgBox("Employer Id ???")
        End If
    End Sub

    Private Sub TextBox18_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox18.KeyPress
        If Not (Char.IsDigit(e.KeyChar)) Or Char.IsControl(e.KeyChar) Or e.KeyChar = "." And TextBox18.Text.IndexOf(".") < 0 Then
            e.Handled = True
        End If
    End Sub


    Private Sub fillcombo2()
        Dim dt_cmb As New DataTable
        With dt_cmb
            dt_cmb.Columns.Add("Item")
            dt_cmb.Columns.Add("Stock")
        End With
        Dim query As String
        con = New SqlConnection()
        con = con_obj.connction()
        con.Open()
        ds = New DataSet()
        query = "Select * from tbl_Item"
        cmd = New SqlCommand(query, con)
        dr = cmd.ExecuteReader
        Dim rw As DataRow
        While dr.Read()
            rw = dt_cmb.NewRow()
            rw("Item") = dr(1).ToString
            If IsDBNull(dr(4)) = True Then
                rw("Stock") = "Null"
                '  Label30.Text = "0"
            Else
                rw("Stock") = Convert.ToDouble(dr(4))
                'Label30.Text = dr(4).ToString
            End If
            dt_cmb.Rows.Add(rw)
        End While
        con.Close()
        ComboBox2.DisplayMember = "Item"
        ComboBox2.ValueMember = "stock"
        ComboBox2.DataSource = dt_cmb
        ComboBox2.SelectedIndex = -1
    End Sub


    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        Dim cmd1 As SqlCommand
        Dim cmd2 As SqlCommand
        Dim stock As Integer = 0
        If ComboBox2.SelectedItem Is Nothing Or TextBox19.Text = "" Or TextBox20.Text = "" Or DateTimePicker4.Text = "" Then
            MsgBox("Data insufficent")
        Else
            con.Open()
            MsgBox(ComboBox2.Text + "  " + CStr(TextBox19.Text) + "   " + CStr(TextBox20.Text) + "   " + DateTimePicker4.Value.Date)
            cmd = New SqlCommand("insert into tbl_purchase(purchase_Itemname,purchase_ItemQuntity,purchase_Itemqntunit,purchase_amount,purchase_date) values('" + ComboBox2.Text + "','" + TextBox19.Text + "',0,'" + TextBox20.Text + "','" + DateTimePicker4.Value.Date + "')", con)
            cmd.ExecuteNonQuery()

            MsgBox("Purchase  Recorded")
            cmd1 = New SqlCommand("select * from  tbl_Item where Item_name='" + ComboBox2.Text + "'", con)
            dr = cmd1.ExecuteReader
            While dr.Read
                If IsDBNull(dr(4)) Then
                    stock = 0
                    MsgBox(stock.ToString)
                Else
                    stock = CInt(dr(4))
                    MsgBox(stock.ToString)
                End If
            End While
            dr.Close()
            stock = stock + CInt(TextBox20.Text)
            cmd2 = New SqlCommand("update tbl_Item set Item_stock='" & stock & "' where Item_name='" & ComboBox2.Text & "'", con)
            cmd2.ExecuteNonQuery()
            Label30.Text = stock.ToString
            MsgBox("stock  updd")
            con.Close()
        End If
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged
        If Not ComboBox2.SelectedIndex = -1 Then
            Label30.Text = ComboBox2.SelectedValue.ToString
        End If
    End Sub



    Private Sub TextBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.Click
        ToolTip1.Tag = "Enter catagory here!"
        ToolTip1.Active = True
    End Sub

End Class