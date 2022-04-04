Imports System.Net
Imports System.IO
Imports Newtonsoft.Json
Public Class CCOVID
    Implements ICOVID

    Dim request As HttpWebRequest
    Dim response As HttpWebResponse = Nothing
    Dim reader As StreamReader
    Dim jsonString As String
    Private AntudDate As String 'mis kuupaeval andmeid kuvada
    Private AntudMaakond As String 'mis maakonnast andmeid kuvada

    Private TotalCasesValue As Integer 'anname tegeliku vaartuse
    Private DailyCasesValue As Integer
    Private TotalCasesLast14DValue As Integer
    Private PerPopulationValue As Integer
    Private NextDayValue As Integer

    Public Property TotalCases As Integer Implements ICOVID.TotalCases
        Get
            Return TotalCasesValue 'atribuudi kusimusel tagasta see
        End Get
        Set(value As Integer)
            TotalCasesValue = value 'atribuudi vaartuse seadistamine
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
            Return TotalCasesLast14DValue
        End Get
        Set(value As Integer)
            TotalCasesLast14DValue = value
        End Set
    End Property

    Public Property PerPopulation As Double Implements ICOVID.PerPopulation
        Get
            Return PerPopulationValue
        End Get
        Set(value As Double)
            PerPopulationValue = value

        End Set
    End Property

    Public Property NextDay As Integer Implements ICOVID.NextDay
        Get
            Return NextDayValue
        End Get
        Set(value As Integer)
            NextDayValue = value
        End Set
    End Property

    Public Sub leiaArvS(ByRef strSisendDate As String, ByRef strSisendMaakond As String) Implements ICOVID.leiaArvS
        AntudDate = strSisendDate
        AntudMaakond = strSisendMaakond

        If strSisendMaakond = "Eesti" Then
            request = DirectCast(WebRequest.Create("https://opendata.digilugu.ee/opendata_covid19_tests_total.json"), HttpWebRequest)
            response = DirectCast(request.GetResponse(), HttpWebResponse)
            reader = New StreamReader(response.GetResponseStream())
            jsonString = reader.ReadToEnd()

            Dim objectList = JsonConvert.DeserializeObject(Of List(Of Data))(jsonString)
            Dim foundItem = objectList.Where(Function(__) __.StatisticsDate = AntudDate).FirstOrDefault()
            TotalCasesValue = foundItem.TotalCases
            DailyCasesValue = foundItem.DailyCases
            TotalCasesLast14DValue = foundItem.TotalCasesLast14D
            PerPopulationValue = foundItem.PerPopulation

            NextDayValue = foundItem.TotalCasesLast14D / 14


        ElseIf strSisendMaakond IsNot "Eesti" Then
            request = DirectCast(WebRequest.Create("https://opendata.digilugu.ee/opendata_covid19_test_county_all.json"), HttpWebRequest)
            response = DirectCast(request.GetResponse(), HttpWebResponse)
            reader = New StreamReader(response.GetResponseStream())
            Dim jsonString As String
            jsonString = reader.ReadToEnd()

            Dim objectList = JsonConvert.DeserializeObject(Of List(Of Data))(jsonString)
            Dim foundItem = objectList.Where(Function(__) __.StatisticsDate = AntudDate And __.County = AntudMaakond And __.ResultValue = "P").FirstOrDefault
            TotalCasesValue = foundItem.TotalCases
            DailyCasesValue = foundItem.DailyCases
            TotalCasesLast14DValue = vbNull
            PerPopulation = vbNull
        End If

    End Sub

End Class
