Public Class CommandTranslator
    Public Function TranslateCommand(ByVal Command As String) As String
        Dim firstTranslatedCommand As String = ""
        Dim continueWithSpace As Boolean = True
        For Each c As Char In Command.ToCharArray()
            Dim s As String = c.ToString()
            If continueWithSpace Then
                If Not s = " " Then
                    continueWithSpace = False
                    firstTranslatedCommand += s
                End If
            Else
                firstTranslatedCommand += s
            End If
        Next
        Dim finalCommand As String = ""
        continueWithSpace = True
        Dim stepper As Boolean = False
        If firstTranslatedCommand.StartsWith(".") Then
            For Each c As Char In firstTranslatedCommand.ToCharArray()
                If stepper Then
                    Dim s As String = c.ToString()
                    If continueWithSpace Then
                        If Not s = " " Then
                            continueWithSpace = False
                            finalCommand += s
                        End If
                    Else
                        finalCommand += s
                    End If
                Else
                    stepper = True
                End If
            Next
            finalCommand = "." + finalCommand
        Else
            finalCommand = firstTranslatedCommand
        End If
        Return finalCommand
    End Function
End Class