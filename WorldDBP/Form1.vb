Public Class Form1
    Dim lang As New System.Globalization.CultureInfo("en-US")
    Dim Time As String
    Dim dm As New DateManage
    Dim dir As New Dir
    Dim fm As New FileManage

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim dt As Date = Date.Now
        If DateTime.Compare(My.Settings.nextBackup.ToString, dt.ToString) = 0 Then
            'Debug.WriteLine("Backup")
            If Not My.Settings.donotBackup Then
                dir.Backup(Time)
            End If
        End If

        Label7.Text = dt.ToString("MM/dd(ddd)" & vbCrLf & "HH:mm:ss")
        Time = dt.ToString("yy_MMdd_HHmm_ddd")
        dm.UpdateNextBuDay()
        Label2.Text = My.Settings.nextBackup.ToString("MM/dd(ddd)" & vbCrLf & "HH:mm:ss")
        fm.FileRm(My.Settings.numofFilecnt, My.Settings.SaveDir, My.Settings.WorldName)
        'Debug.WriteLine(My.Settings.nextBackup + "," + dt)
    End Sub

    Private Sub Form1_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        dm.UpdateNextBuDay()
        Label2.Text = My.Settings.nextBackup.ToString("MM/dd(ddd)" & vbCrLf & "HH:mm:ss")

        Try
            If My.Settings.doCompress Then
                Dim zips As String() = System.IO.Directory.GetFiles(My.Settings.SaveDir, My.Settings.WorldName + "_*.zip")
                For i = 0 To zips.Length - 1
                    zips(i) = Replace(zips(i), My.Settings.SaveDir + "\", "")
                Next
                ListBox1.Items.Clear()
                ListBox1.Items.AddRange(zips)
            Else
                Dim files As String() = System.IO.Directory.GetDirectories(My.Settings.SaveDir, My.Settings.WorldName + "_*")
                For i = 0 To files.Length - 1
                    files(i) = Replace(files(i), My.Settings.SaveDir + "\", "")
                Next
                ListBox1.Items.Clear()
                ListBox1.Items.AddRange(files)
            End If
        Catch ex As Exception
        End Try

        If My.Settings.donotBackup Then
            Label4.Visible = True
        Else
            Label4.Visible = False
        End If

        If My.Settings.showNotification Then
            NotifyIcon1.Visible = True
        Else
            NotifyIcon1.Visible = False
        End If

        If My.Settings.debug Then
            Button7.Visible = True
            Button8.Visible = True
        Else
            Button7.Visible = False
            Button8.Visible = False
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Form2.ShowDialog()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim result As DialogResult = MessageBox.Show("終了しますか？", "終了", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2)
        If result = DialogResult.Yes Then
            Close()
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        dir.Backup(Time)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            Process.Start(My.Settings.SaveDir)
        Catch ex As Exception
            MessageBox.Show("パスが正しく指定されていません", "エラーが発生しました", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Try
            Process.Start(My.Settings.CurDir)
        Catch ex As Exception
            MessageBox.Show("パスが正しく指定されていません", "エラーが発生しました", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        My.Settings.firstExecution = True
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Debug.WriteLine(My.Settings.nextBackup)
    End Sub

    Private Sub ListBox1_MouseEnter(sender As Object, e As EventArgs) Handles ListBox1.MouseEnter
        For i As Integer = 16000 To 32200
            ListBox1.Width = i / 100
        Next
    End Sub

    Private Sub ListBox1_MouseLeave(sender As Object, e As EventArgs) Handles ListBox1.MouseLeave
        For i As Integer = 32200 To 16000 Step -1
            ListBox1.Width = i / 100
        Next
    End Sub

    Private Sub BackuNowToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BackuNowToolStripMenuItem.Click
        Button5.PerformClick()
    End Sub

    Private Sub OpenSaveDirToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenSaveDirToolStripMenuItem.Click
        Button3.PerformClick()
    End Sub

    Private Sub OpenCurDirToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenCurDirToolStripMenuItem.Click
        Button6.PerformClick()
    End Sub

    Private Sub ExitToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem1.Click
        Button2.PerformClick()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim thisProcess As String = System.Diagnostics.Process.GetCurrentProcess().ProcessName
        If System.Diagnostics.Process.GetProcessesByName(thisProcess).Length > 1 Then
            MessageBox.Show("既にプログラムが起動されています。", "多重起動を検知", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End
        End If

        If My.Settings.firstExecution Then
            Form3.ShowDialog()
        End If
    End Sub
End Class