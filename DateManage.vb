Friend Class DateManage
    '指定された日から何日後にバックアップが設定されているかを返す
    Public Function GetNextBackupDay(ByVal day As Date, ByVal cor As Integer) As String
        Dim ddowDi As Integer = -1
        If cor = 1 Then
            ddowDi += 1
        End If
        Dim ddow As Integer = day.DayOfWeek
        Dim deDay As Boolean = False
        While True
            ddowDi += 1
            If ddow + ddowDi > 6 Then
                ddowDi -= 7
            End If
            Select Case (ddow + ddowDi)
                Case 0
                    If My.Settings.doSun Then
                        deDay = True
                    End If
                Case 1
                    If My.Settings.doMon Then
                        deDay = True
                    End If
                Case 2
                    If My.Settings.doTue Then
                        deDay = True
                    End If
                Case 3
                    If My.Settings.doWed Then
                        deDay = True
                    End If
                Case 4
                    If My.Settings.doThu Then
                        deDay = True
                    End If
                Case 5
                    If My.Settings.doFri Then
                        deDay = True
                    End If
                Case 6
                    If My.Settings.doSat Then
                        deDay = True
                    End If
            End Select
            If deDay Then
                Exit While
            End If
        End While
        If ddowDi < 0 Then
            ddowDi += 7
        End If
        Return ddowDi
    End Function

    Sub UpdateNextBuDay()
        Dim nowT As Date = Date.Now
        Dim fin As Integer = 0
        If TimeSpan.Compare(nowT.TimeOfDay, My.Settings.nextBackup.TimeOfDay) = 1 Or 0 Then
            fin = 1
        End If
        Dim dif As Integer = GetNextBackupDay(nowT, fin)
        Dim cord As Date = DateAdd("d", dif, nowT)
        Dim NextBuDay As Date = cord.ToShortDateString + " " + My.Settings.SaveTime.ToShortTimeString
        My.Settings.nextBackup = NextBuDay
    End Sub
End Class

'直近の日曜日を取得
'Dim LatestSun As Date
'If day.DayOfWeek = 0 Then
'LatestSun = day
'Else
'LatestSun = DateAdd("d", -day.DayOfWeek, day)
'End If