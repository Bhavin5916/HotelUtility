Imports System.Data.SqlClient
Public Class connection

    Public Function connction() As SqlConnection
        Dim con As SqlConnection
        con = New SqlConnection("Data Source=.\SQLEXPRESS;AttachDbFilename=|DataDirectory|\data\db_restorent.mdf;Integrated Security=True;User Instance=True")
        Return con

    End Function
End Class
