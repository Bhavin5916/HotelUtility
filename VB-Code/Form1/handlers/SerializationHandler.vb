Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.IO
Imports System.Runtime.Serialization
Imports System.Runtime.Serialization.Formatters.Binary
Public Class SerializationHandler
    Private SerializationPath As String
    Private Sub New()
    End Sub
    Public Sub New(ByVal path As String, ByVal folder As String, ByVal name As String)
        SerializationPath = path & "\" & folder & "\" & name
        If Not Directory.Exists(path & "\" & folder) Then
            Directory.CreateDirectory(path & "\" & folder)
        End If
    End Sub
    Public Sub New(ByVal path As String, ByVal name As String)
        SerializationPath = path & "\" & name
    End Sub
    Public Sub Serialize(Of T)(ByVal objectRoot As T)
        Dim fs As FileStream = File.Open(SerializationPath, FileMode.Create)
        Dim bf As New BinaryFormatter()
        bf.Serialize(fs, objectRoot)
        fs.Close()
    End Sub
    Public Function DeSerialize(Of T As Class)() As T
        Dim fs As FileStream = File.Open(SerializationPath, FileMode.Open)
        Dim bf As New BinaryFormatter()
        Dim dishes As T = TryCast(bf.Deserialize(fs), T)
        fs.Close()
        Return dishes
    End Function
End Class
