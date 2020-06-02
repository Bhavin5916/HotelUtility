Public Class Pertablebill
    ' Dim tableNo As Integer
    Dim issaved As Boolean = False
    Dim totalwithouttax As Decimal
    Dim tax As Decimal = CDec(0)
    Dim tatalwithtax As Decimal = CDec(0)
    Dim discount As String = ""

    Private _bill As List(Of Aclass) = New List(Of Aclass)
    Public Property Bill() As List(Of Aclass)
        Get
            Return _bill
        End Get
        Set(ByVal value As List(Of Aclass))
            _bill = value
        End Set
    End Property



    Private _tableNo As Integer
    Public Property tableNo As Integer
        Get
            Return _tableNo
        End Get
        Set(ByVal value As Integer)
            _tableNo = value
        End Set
    End Property


End Class
