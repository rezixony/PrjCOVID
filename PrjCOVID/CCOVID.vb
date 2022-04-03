Imports System.Net
Imports System.IO
Imports Newtonsoft.Json
Public Class CCOVID
    Implements ICOVID
    Dim request As HttpWebRequest
    Dim response As HttpWebResponse = Nothing
    Dim reader As StreamReader
    Dim jsonString As String
    Private TotalCasesValue As Integer
    Private DailyCasesValue As Integer
    Private AntudDate As String
    Public Property TotalCases As Integer Implements ICOVID.TotalCases
        Get
            Return TotalCasesValue 'atribuudi kusimusel tagasta see
        End Get
        Set(value As Integer)
            TotalCasesValue = value 'atribuudi vaartuse seadistamine
        End Set
    End Property

    Public Property LastStatisticsDate As String Implements ICOVID.LastStatisticsDate
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As String)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property DailyCases As Integer Implements ICOVID.DailyCases
        Get
            Return DailyCasesValue
        End Get
        Set(value As Integer)
            DailyCasesValue = value
        End Set
    End Property

    Public Property TotalCasesLast14D As Integer Implements ICOVID.TotalCasesLast14D
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As Integer)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property PerPopulation As Double Implements ICOVID.PerPopulation
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As Double)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property StatisticsDate As String Implements ICOVID.StatisticsDate
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As String)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property Country As String Implements ICOVID.Country
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As String)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property County As String Implements ICOVID.County
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As String)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Property ResultValue As String Implements ICOVID.ResultValue
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As String)
            Throw New NotImplementedException()
        End Set
    End Property

    Public Sub leiaArvS(ByRef strSisendDate As String) Implements ICOVID.leiaArvS
        request = DirectCast(WebRequest.Create("https://opendata.digilugu.ee/opendata_covid19_tests_total.json"), HttpWebRequest)
        response = DirectCast(request.GetResponse(), HttpWebResponse)
        reader = New StreamReader(response.GetResponseStream())
        jsonString = reader.ReadToEnd()
        AntudDate = strSisendDate
        Dim objectList = JsonConvert.DeserializeObject(Of List(Of Data))(jsonString)
        Dim foundItem = objectList.Where(Function(__) __.StatisticsDate = AntudDate).FirstOrDefault()
        TotalCasesValue = foundItem.TotalCases
        DailyCasesValue = foundItem.DailyCases
    End Sub

    Public Function leiaArvF() As Integer Implements ICOVID.leiaArvF
        Throw New NotImplementedException()
    End Function
End Class
