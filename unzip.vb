Imports System
Imports System.IO
Imports Ionic.Zip
Class Unzip
    Public Shared Sub Unzip(ByVal ZipToUnpack As String, ByVal Destination As String)
        Dim complete As Boolean = False
        Try
            Dim TargetDir As String = Destination

            Console.WriteLine("Extracting file {0} to {1}", ZipToUnpack, TargetDir)
            Using zip1 As ZipFile = ZipFile.Read(ZipToUnpack)
                Dim e As ZipEntry
                For Each e In zip1
                    e.Extract(TargetDir, ExtractExistingFileAction.OverwriteSilently)
                Next
                complete = True
            End Using
        Catch ex As Exception
        Finally
            If complete = True Then
                System.IO.File.Delete(ZipToUnpack)
            End If
        End Try
    End Sub
End Class