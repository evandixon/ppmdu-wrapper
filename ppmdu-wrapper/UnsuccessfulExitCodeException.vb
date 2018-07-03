Public Class UnsuccessfulExitCodeException
    Inherits Exception

    Public Sub New(exitCode As Integer, programName As String)
        MyBase.New(String.Format(My.Resources.Language.UnsuccessfulExitCodeExceptionMessage, programName, exitCode))

        Me.ExitCode = exitCode
        Me.ProgramName = programName
    End Sub

    Public Property ExitCode As Integer
    Public Property ProgramName As String
End Class
