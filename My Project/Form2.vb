Public Class Form2
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Debug.WriteLine("ph1")
        Dim Dir As String = OpenDirSlDia()
        Debug.WriteLine("ph5")
        If Not Dir = "Null" Then
            Debug.WriteLine("ph6")
            TextBox1.Text = Dir
        End If
        Debug.WriteLine("ph7")
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim Dir As String = OpenDirSlDia()
        If Not Dir = "Null" Then
            TextBox2.Text = Dir
        End If
    End Sub

    Private Function OpenDirSlDia()
        Debug.WriteLine("ph2")
        Dim odsd As New FolderBrowserDialog

        odsd.Description = "フォルダを指定してください。"
        odsd.RootFolder = Environment.SpecialFolder.Desktop
        odsd.SelectedPath = "C:\Windows"
        odsd.ShowNewFolderButton = True

        Debug.WriteLine("ph3")

        If odsd.ShowDialog(Me) = DialogResult.OK Then
            Debug.WriteLine("ph4-1")
            Return odsd.SelectedPath
        Else
            Debug.WriteLine("ph4-2")
            Return "Null"
        End If
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim result As DialogResult = MessageBox.Show("変更がある場合は保存されません。よろしいですか？", "注意", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2)
        If result = DialogResult.OK Then
            Close()
        End If
    End Sub

    Private Sub Form2_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        UpdateChBox()
    End Sub

    Private Sub GroupBox4_MouseEnter(sender As Object, e As EventArgs) Handles GroupBox4.MouseEnter
        UpdateChBox()
    End Sub

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TabControl1.SelectedIndex = 0
        My.Settings.Reload()
        TextBox1.Text = My.Settings.CurDir
        TextBox2.Text = My.Settings.SaveDir
        TextBox3.Text = My.Settings.WorldName
        CheckBox1.Checked = My.Settings.LogOutput
        CheckBox2.Checked = My.Settings.doCompress
        DateTimePicker1.Value = My.Settings.SaveTime
        CheckMon.Checked = My.Settings.doMon
        CheckTue.Checked = My.Settings.doTue
        CheckWed.Checked = My.Settings.doWed
        CheckThu.Checked = My.Settings.doThu
        CheckFri.Checked = My.Settings.doFri
        CheckSat.Checked = My.Settings.doSat
        CheckSun.Checked = My.Settings.doSun
        CheckBox3.Checked = My.Settings.showNotification
        CheckBox4.Checked = My.Settings.donotBackup
        CheckBox5.Checked = My.Settings.debug
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        My.Settings.CurDir = TextBox1.Text
        My.Settings.SaveDir = TextBox2.Text
        My.Settings.WorldName = TextBox3.Text
        My.Settings.LogOutput = CheckBox1.Checked
        My.Settings.doCompress = CheckBox2.Checked
        My.Settings.SaveTime = DateTimePicker1.Value
        My.Settings.doMon = CheckMon.Checked
        My.Settings.doTue = CheckTue.Checked
        My.Settings.doWed = CheckWed.Checked
        My.Settings.doThu = CheckThu.Checked
        My.Settings.doFri = CheckFri.Checked
        My.Settings.doSat = CheckSat.Checked
        My.Settings.doSun = CheckSun.Checked
        My.Settings.showNotification = CheckBox3.Checked
        My.Settings.donotBackup = CheckBox4.Checked
        My.Settings.debug = CheckBox5.Checked
        My.Settings.Save()
        Close()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim result As DialogResult = MessageBox.Show("全ての設定を既定値に戻しますか？", "リセットの確認", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2)
        If result = DialogResult.OK Then
            My.Settings.Reset()
            My.Settings.firstExecution = False
            Close()
        End If
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Form3.ShowDialog()
    End Sub

    Sub UpdateChBox()
        Dim obj As Object
        Dim chofN As Integer = 0
        Dim mode As Integer = 0
        For i As Integer = 0 To 1
            For Each obj In GroupBox4.Controls
                If TypeName(obj) = "CheckBox" Then
                    Select Case mode
                        Case 0
                            If obj.Checked Then
                                chofN += 1
                            End If
                        Case 1
                            If obj.Checked Then
                                obj.Enabled = False
                            End If
                        Case 2
                            If Not obj.Enabled Then
                                obj.Enabled = True
                            End If
                    End Select
                End If
            Next
            If chofN = 1 Then
                mode = 1
            Else
                mode = 2
            End If
        Next
        mode = 0
    End Sub
End Class