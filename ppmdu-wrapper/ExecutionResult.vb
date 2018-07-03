﻿Public Class ExecutionResult
    Public Property ProgramName As String
    Public Property ExitCode As Integer

    Public Function IsExitCodeSuccessful() As Boolean
        Return ExitCode = 0
    End Function

    Public Sub EnsureExitCodeSuccessful()
        If Not IsExitCodeSuccessful() Then
            Throw New UnsuccessfulExitCodeException(ExitCode, ProgramName)
        End If
    End Sub
End Class
