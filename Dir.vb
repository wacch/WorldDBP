Friend Class Dir
    Public Sub Backup(ByVal Time As String)
        Dim Cur As String = My.Settings.CurDir
        Dim Save As String = My.Settings.SaveDir + "\" + My.Settings.WorldName + "_" + Time
        Try
            If My.Settings.doCompress Then
                IO.Compression.ZipFile.CreateFromDirectory(Cur, Save + ".zip", IO.Compression.CompressionLevel.Optimal, False)
            Else
                My.Computer.FileSystem.CopyDirectory(Cur, Save, FileIO.UIOption.AllDialogs, FileIO.UICancelOption.DoNothing)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "エラーが発生しました", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class
