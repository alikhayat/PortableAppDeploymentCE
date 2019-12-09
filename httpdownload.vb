Imports System
Imports System.Net
Imports System.IO

Class HttpDownload

    Private Const NumericFormat As String = "###,###,###,###,###,###,###"

    'more than enough for uint64:
    '18,446,744,073,709,551,615 max
    Private Shared Function CreateFormat(ByVal preFormat As String, ByVal placeholder As String) As String
        Return preFormat.Replace(placeholder, NumericFormat)
    End Function

    'CreateFormat
    'Private Shared Function ConvertUrlToFileName(ByVal url As String) As String
    '    'Dim terms() As String = url.Split(New String() {":", "//"}, StringSplitOptions.RemoveEmptyEntries)
    '    'Dim fname As String = terms((terms.Length - 1))
    '    'fname = fname.Replace(Microsoft.VisualBasic.ChrW(47), Microsoft.VisualBasic.ChrW(46))
    '    Return url
    'End Function

    'ConvertUrlToFileName
    Private Shared Function GetExistingFileLength(ByVal filename As String) As Long
        If Not File.Exists(filename) Then
            Return 0
        End If

        Dim info As FileInfo = New FileInfo(filename)
        Return info.Length
    End Function

    'GetExistingFileLength
    Public Shared Sub Download(ByVal url As String, ByVal existingFilename As String, ByVal quiet As Boolean)
        Dim webRequest As HttpWebRequest
        Dim webResponse As HttpWebResponse

        Dim fmt As String = HttpDownload.CreateFormat("{0}: {1:#} of {2:#} ({3:g3}%)", "#")
        Dim fs As FileStream = Nothing
        Try
            Dim fname As String = existingFilename
            If (fname Is Nothing) Then
                fname = url
            End If

            webRequest = CType(webRequest.Create(url), HttpWebRequest)
            Dim preloadedLength As Long = HttpDownload.GetExistingFileLength(fname)
            If (preloadedLength > 0) Then
                webRequest.AddRange(CType(preloadedLength, Integer))
            End If

            webResponse = CType(webRequest.GetResponse, HttpWebResponse)
            fs = New FileStream(fname, FileMode.Append, FileAccess.Write)
            Dim fileLength As Long = webResponse.ContentLength
            Dim todoFormat As String = HttpDownload.CreateFormat("Downloading {0}: {1:#} bytes...", "#")
            Console.WriteLine()
            Console.WriteLine(todoFormat, url, fileLength)
            Console.WriteLine("Pre-loaded: preloaded length {0}", preloadedLength)
            Console.WriteLine("Remaining length: {0}", fileLength)
            Dim strm As Stream = webResponse.GetResponseStream
            Dim arrSize As Integer = (10 * (1024 * 1024))

            Dim barr() As Byte = New Byte((arrSize) - 1) {}
            Dim bytesCounter As Long = preloadedLength
            Dim fmtPercent As String = String.Empty

            While True
                Dim actualBytes As Integer = strm.Read(barr, 0, arrSize)
                If (actualBytes <= 0) Then
                    Exit While
                End If

                fs.Write(barr, 0, actualBytes)
                bytesCounter = (bytesCounter + actualBytes)
                Dim percent As Double
                If (fileLength > 0) Then
                    percent = 100D * bytesCounter / (preloadedLength + fileLength)
                End If
                'fmt, fname, bytesCounter, (preloadedLength + fileLength),

                If Not quiet Then
                    Console.WriteLine(percent)
                End If
            End While

            'loop
            Console.WriteLine("{0}: complete!", url)
        Catch e As Exception
            Console.WriteLine("{0}: {1} '{2}'", url, e.GetType.FullName, e.Message)
        Finally
            If (Not (fs) Is Nothing) Then
                fs.Flush()
                fs.Close()
            End If
        End Try
    End Sub
End Class
