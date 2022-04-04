Public Interface ICOVID

    Property TotalCases As Integer
    Property DailyCases As Integer
    Property TotalCasesLast14D As Integer
    Property PerPopulation As Double
    Property NextDay As Integer

    Sub leiaArvS(ByRef strSisendDate As String, ByRef strSisendMaakond As String)

End Interface
