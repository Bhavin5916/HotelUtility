Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports System.Data.SqlClient
Public Class configoption

    Dim conobj As New connection
    Dim cmd As SqlCommand
    Dim dt As DataTable
    Dim adp As SqlDataAdapter
    Dim dr As SqlDataReader
    Dim con As SqlConnection = conobj.connction




    Private TaxRate As Decimal
    Private Address As String
    Private PhoneNumber As String
    Private LastRunDate As DateTime
    Private RegistrationKey As String
    Private FirstRun As Boolean
    Private NoOfTables As Integer
    Private TaxAdditionThreshhold As Decimal
    Private BillItemColumnSize As Integer
    Private Sub configoption_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        GetConfig()
        textBox1.Text = TaxRate.ToString()
        textBox2.Text = PhoneNumber
        textBox3.Text = Address.ToString
        textBox4.Text = NoOfTables.ToString()
        textBox5.Text = TaxAdditionThreshhold.ToString()
        textBox6.Text = BillItemColumnSize.ToString()
        AddHandler textBox1.Leave, AddressOf txtleave
        AddHandler textBox2.Leave, AddressOf txtleave
        AddHandler textBox3.Leave, AddressOf txtleave
        AddHandler textBox4.Leave, AddressOf txtleave
        AddHandler textBox5.Leave, AddressOf txtleave
        AddHandler textBox6.Leave, AddressOf txtleave

    End Sub
    Private Sub txtleave(ByVal sender As Object, ByVal e As EventArgs)

    End Sub

    'textBox1.Leave += New EventHandler(Function(source, args)
    '                                       If textBox1.Text = "" Then
    '                                           textBox1.Text = (0).ToString()
    '                                       End If
    '                                   End Function)
    'textBox5.Leave += New EventHandler(Function(source, args)
    '                                       If textBox5.Text = "" Then
    '                                           textBox5.Text = (0).ToString()
    '                                       End If
    '                                   End Function)
    'textBox4.Leave += New EventHandler(Function(source, args)
    '                                       If textBox4.Text = "" Then
    '                                           textBox4.Text = (24).ToString()
    '                                       End If
    '                                   End Function)
    'textBox6.Leave += New EventHandler(Function(source, args) 
    'If textBox6.Text = "" Then
    '	textBox6.Text = (200).ToString()
    'End If


    Private Sub button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles button1.Click
        Try
            Dim TaxRate1 As Decimal = [Decimal].Parse(textBox1.Text)
            If TaxRate1 < CDec(0) Then
                Throw New Exception("Tax rate cannot be negative.")
            End If
            TaxRate = TaxRate1
            PhoneNumber = textBox2.Text
            Address = textBox3.Text
            Dim NoOfTables1 As Integer = Int32.Parse(textBox4.Text)
            If NoOfTables1 < 1 Then
                Throw New Exception("There has to be at least one table.")
            End If
            NoOfTables = Convert.ToInt32(NoOfTables1)
            Dim TaxAddnThreshhold1 As Decimal = [Decimal].Parse(textBox5.Text)
            If TaxAddnThreshhold1 < CDec(0) Then
                Throw New Exception("Tax addition threshold cannot be negative.")
            End If
            TaxAdditionThreshhold = TaxAddnThreshhold1
            Dim billItemColWidth1 As Integer = Integer.Parse(textBox6.Text)
            If billItemColWidth1 < 50 Then
                Throw New Exception("Column width cannot be less than 50 px.")
            End If
            BillItemColumnSize = billItemColWidth1
            'MsgBox(config.TaxRate.ToString + config.PhoneNumber.ToString + config.TaxAdditionThreshhold.ToString + config.NoOfTables.ToString)
            Save()
        Catch x As Exception
            MessageBox.Show(x.Message)
            Return
        End Try
        Me.Close()
    End Sub

    Private Sub button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles button2.Click
        Me.Close()
    End Sub


    Public Sub GetConfig()
        Try
            dt = New DataTable
            con.Open()
            cmd = New SqlCommand("select * from tbl_configration", con)
            adp = New SqlDataAdapter(cmd)
            adp.Fill(dt)
            con.Close()
            Address = dt.Rows(0).Item(1)
            PhoneNumber = dt.Rows(0).Item(2).ToString
            TaxRate = Convert.ToDecimal(dt.Rows(0).Item(3))
            NoOfTables = Convert.ToInt32(dt.Rows(0).Item(7))
            TaxAdditionThreshhold = Convert.ToDecimal(dt.Rows(0).Item(8))
            BillItemColumnSize = Convert.ToInt32(dt.Rows(0).Item(9).ToString)
            'LastRunDate = dt.Rows(0).Item(3)
            'RegistrationKey = dt.Rows(0).Item(4)
            'FirstRun = dt.Rows(0).Item(5)
        Catch x As Exception
            MsgBox(x.ToString)

            Address = "Shop No. 1, Shreyashrayam , Sargasan Road, Behind DA-IICT , Gandhinagar, Gujarat"
            PhoneNumber = "8866573252"
            FirstRun = True
            LastRunDate = DateTime.Now
            RegistrationKey = ""
            NoOfTables = 21
            TaxAdditionThreshhold = CDec(0)
        End Try
    End Sub
    Private Sub Save()
        MsgBox("->" + Address.ToString + "->" + PhoneNumber.ToString + "->" + TaxRate.ToString + "->" + NoOfTables.ToString + "->" + TaxAdditionThreshhold.ToString + "->" + BillItemColumnSize.ToString)
        con.Open()
        Dim str As String = "update tbl_configration set Adress='" & Address.ToString & "',Phonenumber='" & PhoneNumber.ToString & "',Taxrate='" & TaxRate.ToString & "',NoOfTables='" & NoOfTables.ToString & "',TaxAdditionThreshhold='" & TaxAdditionThreshhold.ToString & "',BillItemColumnSize='" & BillItemColumnSize.ToString & "' where id=1"
        MsgBox(str)
        cmd = New SqlCommand(str, con)
        cmd.ExecuteNonQuery()
        con.Close()
    End Sub
End Class