Imports System.IO

Class MoveDir
    Public Shared Sub DirectoryCopy(ByVal sourceDirName As String, ByVal destDirName As String, ByVal copySubDirs As Boolean)
        Dim dir As DirectoryInfo = New DirectoryInfo(sourceDirName)
        Dim dirs As DirectoryInfo() = dir.GetDirectories()

        If Not dir.Exists Then
            Throw New DirectoryNotFoundException("Source directory does not exist or could not be found: " & sourceDirName)
        End If

        If Not Directory.Exists(destDirName) Then
            Directory.CreateDirectory(destDirName)
        End If

        Dim files As FileInfo() = dir.GetFiles()

        For Each file As FileInfo In files
            Dim temppath As String = Path.Combine(destDirName, file.Name)
            file.CopyTo(temppath, False)
        Next

        If copySubDirs Then

            For Each subdir As DirectoryInfo In dirs
                Dim temppath As String = Path.Combine(destDirName, subdir.Name)
                DirectoryCopy(subdir.FullName, temppath, copySubDirs)
            Next
        End If
    End Sub
    Public Shared Sub MoveShortcut(ByVal SourceDirName As String, ByVal Des As String)
        System.IO.File.Copy(SourceDirName, Des)
    End Sub
    Public Sub Dispose()
        Me.Dispose()
    End Sub
End Class

