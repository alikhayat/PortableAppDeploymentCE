Imports System
Module Module1
    Public AppPath As String = "", URL As String = "", TempPath As String = "", FileNameApp As String = "", FileNameDB As String = ""
    Sub Main()
        Try
            EnableWifi()
            InializeTimer()
            ReadConfigs()
            If HttpDownload.Download(URL + FileNameDB, TempPath + FileNameDB, False) And HttpDownload.Download(URL + FileNameApp, TempPath + FileNameApp, False) Then
                Unzip.Unzip(TempPath + FileNameDB, TempPath)
                Unzip.Unzip(TempPath + FileNameApp, AppPath)
                Unzip.Unzip(TempPath + FileNameApp, "\Program Files\Dc")

                Process.Start(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + "\Shortcut.bat", "")
            Else
                Console.WriteLine("Deploy Failed, Check network and restart")
            End If

        Catch ex As Exception
        Finally
        End Try
        
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
            FileNameApp = ReadLine(4, allLines)
            FileNameDB = ReadLine(5, allLines)

        Catch ex As Exception
            MsgBox("Check Configs")
        End Try
    End Sub
    Private Function ReadLine(ByVal lineNumber As Integer, ByVal lines As List(Of String)) As String
        Return lines(lineNumber - 1)
    End Function
End Module
