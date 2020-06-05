Friend Class FileManage
    Public Sub FileRm(ByVal NoF, ByVal Path, ByVal Name)
        Dim p As New ProcessStartInfo()
        p.FileName = System.IO.Directory.GetCurrentDirectory() + "\Resources\FileRemoveTool.bat"
        p.Arguments = String.Format("{0} ""{1}"" ""{2}""", NoF, Path, Name)
        p.CreateNoWindow = True
        p.UseShellExecute = False
        Process.Start(p)
    End Sub
End Class
