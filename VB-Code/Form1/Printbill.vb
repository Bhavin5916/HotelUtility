Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports System.Drawing.Printing
Imports System.Threading
Public Class Printbill
    Dim config As Confighandler = New Confighandler
    Dim bill As Pertablebill = New Pertablebill
    Dim pd As PrintDocument = New PrintDocument
    Dim dataformateforprint As List(Of String) = New List(Of String)
    Dim nocharperline As Integer = 50
    Dim Account As String
    Dim billNo As Integer = Table_billing.get_billno 
    Dim amount As Decimal = 0
    Dim total As Decimal = 0
    Dim data As IEnumerable(Of Aclass)
    Dim tax_amount As Decimal = 0
    Dim TaxRate As Decimal = config.taxRate
    Dim taxtreshold As Decimal = config.Taxadditionthresold

    Private Sub Printbill_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        dataformateforprint.Clear()
        new1()
        FormatForPrint()
        fill_table()
    End Sub

    Private Sub FormatForPrint()
        If dataformateforprint.Count = 0 Then
            dataformateforprint.Add(CenterLine("SALT N' PEPPER", 35))
            ' For Each lin As e In config.Adress
            dataformateforprint.Add(CenterLine(config.Adress.ToString, 35))
            'Next
            dataformateforprint.Add(CenterLine("Phone: " + config.Phone_no, 35))
            dataformateforprint.Add(Repeat("-", 35))

            dataformateforprint.Add(LeftLine("Bill No: " & billNo.ToString(), 10) & RightLine(DateTime.Now.ToString(), 25))
            If Not [String].IsNullOrEmpty(Me.Account) Then
                If Me.Account.Length <= 20 Then
                    dataformateforprint.Add(LeftLine("Customer:", 15) & LeftLine(Me.Account, 20))
                Else
                    dataformateforprint.Add(LeftLine([String].Format("Customer: {0}", Me.Account), 35))
                End If
            End If
            dataformateforprint.Add(Repeat("-", 35))

            Dim strItem = LeftLine("Item Name", 20)
            Dim strPrice = CenterLine("Rate", 5)
            Dim strQuantity = CenterLine("Qty", 5)
            Dim strAmount = CenterLine("Price", 5)

            dataformateforprint.Add(strItem & strPrice & strQuantity & strAmount)
            dataformateforprint.Add(Repeat(" ", 35))

            If amount > taxtreshold Then
                tax_amount = Math.Ceiling((amount * config.taxRate) / 100)
            Else
                tax_amount = 0
            End If


            'add items from customer bill
            For Each dr As DataGridViewRow In grid.Rows
                Dim name = LeftLine(dr.Cells(0).Value, 20)
                Dim qty = CenterLine(dr.Cells(1).Value, 5)
                Dim amt = CenterLine(dr.Cells(2).Value, 5)
                Dim prc = CenterLine(dr.Cells(3).Value, 5)
                amount += CDec(dr.Cells(3).Value)
                dataformateforprint.Add(name & qty & amt & prc)
            Next

            dataformateforprint.Add(Repeat("-", 35))

            dataformateforprint.Add(RightLine([String].Format("Price: Rs {0}", amount), 35))
            'taxes = (amount * TaxRate) / 100
            dataformateforprint.Add(RightLine([String].Format("Tax ({0}%): Rs {1}", TaxRate, Math.Ceiling(tax_amount)), 35))
            total = amount + Math.Ceiling(tax_amount)
            dataformateforprint.Add(RightLine([String].Format("Total: Rs {0}", total), 35))

            dataformateforprint.Add(Repeat("-", 35))

            For Each line As String In dataformateforprint
                Console.WriteLine(line)
            Next
        End If
    End Sub
    Public Function RightLine(ByVal p As String, ByVal lineLength As Integer) As String
        If [String].IsNullOrEmpty(p) Then
            Return Repeat(" ", lineLength)
        End If
        If p.Length = lineLength Then
            Return p
        ElseIf p.Length > lineLength Then
            Return p.Substring(0, lineLength)
        Else
            Return Repeat(" ", lineLength - p.Length) & p
        End If
    End Function
    Public Function CenterLine(ByVal p As String, ByVal lineLength As Integer) As String
        If [String].IsNullOrEmpty(p) Then
            Return Repeat(" ", lineLength)
        End If
        If p.Length = lineLength Then
            Return p
        ElseIf p.Length > lineLength Then
            Return p.Substring(0, lineLength)
        Else
            Dim l = p.Length
            Dim requiredBlanks = lineLength - l
            Dim blanksOnLeft = CInt(Math.Truncate(Math.Ceiling(requiredBlanks / CDbl(2))))
            Dim blanksOnRight = CInt(Math.Truncate(Math.Floor(requiredBlanks / CDbl(2))))
            Return GetBlanks(blanksOnLeft) & p & GetBlanks(blanksOnRight)
        End If
    End Function
    Public Function LeftLine(ByVal p As String, ByVal lineLength As Integer) As String
        If [String].IsNullOrEmpty(p) Then
            Return Repeat(" ", lineLength)
        End If
        Dim len As Integer = p.Length
        If len < lineLength Then
            Return p & Repeat(" ", lineLength - len)
        ElseIf len = lineLength Then
            Return p
        Else
            Return p.Substring(0, lineLength)
        End If
    End Function
    Public Function GetBlanks(ByVal length As Integer) As String
        Return Repeat(" ", length)
    End Function
    Public Function Repeat(ByVal chr As String, ByVal length As Integer) As String
        Dim s As String = ""
        For i As Integer = 0 To length - 1
            s += chr
        Next
        Return s
    End Function


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Print()
    End Sub
    Private Sub Print()
        Dim t As System.Threading.Thread = New System.Threading.Thread(New System.Threading.ThreadStart(AddressOf startprint))
        t.Start()
    End Sub
    Private Function startprint() As Threading.ThreadStart
        Dim psd As PageSetupDialog = New PageSetupDialog
        psd.Document = pd
        Dim multiplier As Integer = 1
        'multiplier =(Int )Math .Ceiling ((Double )dataformateforprint.count/(Double )26)


        multiplier = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(dataformateforprint.Count) / 26))
        psd.PageSettings.PaperSize = New PaperSize("Bill", 400, (400 * multiplier) + 150)
        psd.PageSettings.PaperSize.RawKind = Convert.ToInt32(PaperKind.Custom)
        psd.PageSettings.PaperSize.Width = 400
        psd.PageSettings.PaperSize.Height = (400 * multiplier) + 150

        pd.DefaultPageSettings.Margins.Left = 35
        pd.DefaultPageSettings.Margins.Top = 10
        pd.DefaultPageSettings.Margins.Right = 10
        pd.DefaultPageSettings.Margins.Bottom = 10
        pd.DocumentName = String.Format("Restaurateur: Bill No: {0}", 1)

        AddHandler pd.PrintPage, AddressOf print_Page1

        pd.Print()

        Return Nothing
    End Function
    Private Sub print_Page1(ByVal sender As Object, ByVal e As PrintPageEventArgs)
        Dim leftMargin As Single = e.MarginBounds.Left
        Dim topMargin As Single = e.MarginBounds.Top
        Dim printingFont As FontFamily = FontFamily.GenericMonospace
        Dim f As New Font(printingFont, 10)
        Dim lineHeight As Single = f.GetHeight(e.Graphics)
        Dim lineNo As Integer = 0
        For Each line As String In dataformateforprint
            e.Graphics.DrawString(line, f, Brushes.Black, leftMargin, topMargin + lineHeight * System.Math.Max(System.Threading.Interlocked.Increment(lineNo), lineNo - 1))
        Next
        e.HasMorePages = False
    End Sub


    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click

        PrintPreviewControl()
    End Sub
    Private Sub PrintPreviewControl()
        Dim ppd As New PrintPreviewDialog()
        Dim psd As New PageSetupDialog()
        psd.Document = pd
        Dim multiplier As Integer = 1
        multiplier = CInt(Math.Truncate(Math.Ceiling(CDbl(dataformateforprint.Count) / CDbl(26))))
        psd.PageSettings.PaperSize = New PaperSize("Bill", 400, 400 * multiplier)
        psd.PageSettings.PaperSize.RawKind = CInt(PaperKind.[Custom])
        psd.PageSettings.PaperSize.Width = 400
        psd.PageSettings.PaperSize.Height = 400 * multiplier
        pd.DefaultPageSettings.Margins.Left = 35
        pd.DefaultPageSettings.Margins.Top = 10
        pd.DefaultPageSettings.Margins.Right = 10
        pd.DefaultPageSettings.Margins.Bottom = 10
        pd.DocumentName = "Bill"
        AddHandler pd.PrintPage, AddressOf print_Page1
        ppd.Document = pd
        Try
            ppd.ShowDialog()
        Catch x As InvalidPrinterException
            MessageBox.Show(x.Message)
        End Try
    End Sub
    Private Sub new1()

        bill.tableNo = Convert.ToInt32(Table_billing.Label_table.Text)
        For Each dr As DataGridViewRow In Table_billing.DataGridView1.Rows
            Dim ab As Aclass = New Aclass
            ab.item = dr.Cells(1).Value
            ab.Quantity = dr.Cells(2).Value
            ab.price = dr.Cells(3).Value
            ab.amount = dr.Cells(4).Value
            'amount1 += CDec(dr.Cells(4).Value)
            bill.Bill.Add(ab)
        Next
        grid.DataSource = bill.Bill
        grid.Columns(0).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

    End Sub
    Public Sub fill_table()
        table.Controls.Add(New Label With {.Text = String.Format("Total:")}, 0, 0)
        table.Controls.Add(New Label With {.Text = String.Format("Rs {0}", amount)}, 0, 1)
        table.Controls.Add(New Label With {.Text = String.Format("Taxes ({0}%):", config.taxRate)}, 0, 2)
        table.Controls.Add(New Label With {.Text = String.Format("Rs {0}", tax_amount)}, 0, 3)
        table.Controls.Add(New Label With {.Text = "Total:"}, 0, 4)
        table.Controls.Add(New Label With {.Text = String.Format("Rs {0}", Math.Ceiling(amount + tax_amount))}, 0, 5)
    End Sub

End Class