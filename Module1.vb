Imports System
Module Module1
    Public AppPath As String = "", URL As String = "", TempPath As String = "", ZIP As String = ""
    Sub Main()
        ReadConfigs()
        HttpDownload.Download(URL, TempPath + ZIP, False)
        Unzip.Unzip(TempPath + ZIP, AppPath)
    End Sub
    Private Sub ReadConfigs()
        ' Read configs from configs.txt

        Try
            Dim reader As New System.IO.StreamReader(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + "\Configs.txt")
            Dim allLines As List(Of String) = New List(Of String)

            Do While Not reader.EndOfStream
                allLines.Add(reader.ReadLine())
            Loop
            reader.Close()

            AppPath = ReadLine(1, allLines)
            URL = ReadLine(2, allLines)
            TempPath = ReadLine(3, allLines)
            ZIP = ReadLine(4, allLines)

        Catch ex As Exception
            MsgBox("Check Configs")
        End Try
    End Sub
    Private Function ReadLine(ByVal lineNumber As Integer, ByVal lines As List(Of String)) As String
        Return lines(lineNumber - 1)
    End Function
End Module
