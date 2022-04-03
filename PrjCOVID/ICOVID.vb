Public Interface ICOVID

    Property TotalCases As Integer
    Property LastStatisticsDate As String
    Property DailyCases As Integer
    Property TotalCasesLast14D As Integer
    Property PerPopulation As Double
    '------------------------------------------------------[ Maakond ]------------------------------------------------------
    Property StatisticsDate As String
    Property Country As String
    Property County As String
    Property ResultValue As String

    Function leiaArvF() As Integer
    Sub leiaArvS(ByRef strSisendDate As String)

End Interface
