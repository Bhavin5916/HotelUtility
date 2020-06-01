Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.IO
Imports System.Runtime.Serialization
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.Data.SqlClient


Public Class Confighandler
    Dim conobj As New connection
    Dim cmd As SqlCommand
    Dim dt As DataTable
    Dim adp As SqlDataAdapter
    Dim dr As SqlDataReader
    Dim con As SqlConnection = conobj.connction

    Property table_No As Integer
    Property Adress As String
    Property Phone_no As String
    Property taxRate As Decimal
    Property Taxadditionthresold As Decimal
    Property billcolumnsize As Decimal

    Public Sub New()
        dt = New DataTable
        con.Open()
        Dim str As String
        str = "select * from tbl_configration"
        cmd = New SqlCommand(str, con)
        adp = New SqlDataAdapter(cmd)
        adp.Fill(dt)
        con.Close()
        table_No = dt.Rows(0).Item(7)
        Adress = dt.Rows(0).Item(1).ToString
        Phone_no = dt.Rows(0).Item(2)
        taxRate = CDec(dt.Rows(0).Item(3))
        Taxadditionthresold = CDec(dt.Rows(0).Item(8))
        billcolumnsize = CDec(dt.Rows(0).Item(9))
    End Sub


    Public Function getconfig() As Integer
        Return table_No
    End Function


End Class


