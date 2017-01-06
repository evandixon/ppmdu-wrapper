Imports System.IO
Imports System.Text
Imports System.Threading

Public Class UtilityManager
    Implements IDisposable

    Public Sub New()
        ResetToolDirectory()
    End Sub

    Private Function AbsolutizePath(path As String) As String
        If IO.Path.IsPathRooted(path) Then
            Return path
        Else
            Return IO.Path.Combine(Environment.CurrentDirectory, path)
        End If
    End Function

#Region "Process Running"

    Public Event ConsoleOutputReceived(sender As Object, e As DataReceivedEventArgs)

    ''' <summary>
    ''' Whether or not to forward console output of child processes to the current process.
    ''' </summary>
    ''' <returns></returns>
    Public Property OutputConsoleOutput As Boolean = True

    Private Async Function RunProgram(program As String, arguments As String) As Task
        Dim handlersRegistered As Boolean = False

        Dim p As New Process
        p.StartInfo.FileName = program
        p.StartInfo.WorkingDirectory = Path.GetDirectoryName(program)
        p.StartInfo.Arguments = arguments
        p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
        p.StartInfo.CreateNoWindow = True
        p.StartInfo.RedirectStandardOutput = OutputConsoleOutput
        p.StartInfo.RedirectStandardError = p.StartInfo.RedirectStandardOutput
        p.StartInfo.UseShellExecute = False

        If p.StartInfo.RedirectStandardOutput Then
            AddHandler p.OutputDataReceived, AddressOf OnInputRecieved
            AddHandler p.ErrorDataReceived, AddressOf OnInputRecieved
            handlersRegistered = True
        End If

        p.Start()

        p.BeginOutputReadLine()
        p.BeginErrorReadLine()

        Await Task.Run(Sub() p.WaitForExit())

        If handlersRegistered Then
            RemoveHandler p.OutputDataReceived, AddressOf OnInputRecieved
            RemoveHandler p.ErrorDataReceived, AddressOf OnInputRecieved
        End If
    End Function

    Private Sub OnInputRecieved(sender As Object, e As DataReceivedEventArgs)
        If TypeOf sender Is Process AndAlso Not String.IsNullOrEmpty(e.Data) Then
            Console.Write($"[{Path.GetFileNameWithoutExtension(DirectCast(sender, Process).StartInfo.FileName)}] ")
            Console.WriteLine(e.Data)
            RaiseEvent ConsoleOutputReceived(Me, e)
        End If
    End Sub
#End Region

#Region "Tool Management"
    Dim _toolDirectoryResetLock As New Object

    Private ReadOnly Property ToolDirectory As String
        Get
            If _toolDirectory Is Nothing Then
                ResetToolDirectory()
            End If
            Return _toolDirectory
        End Get
    End Property
    Dim _toolDirectory As String

    Private Sub ResetToolDirectory()
        SyncLock _toolDirectoryResetLock
            If _toolDirectory IsNot Nothing AndAlso Directory.Exists(_toolDirectory) Then
                Exit Sub
            End If

            _toolDirectory = Path.Combine(Path.GetTempPath, "PPMDU Wrapper-" & Guid.NewGuid.ToString)
            If Directory.Exists(_toolDirectory) Then
                ResetToolDirectory()
            Else
                Directory.CreateDirectory(_toolDirectory)

                'Copy the tools
                File.WriteAllBytes(Path_AudioUtil, My.Resources.ppmd_audioutil)
                File.WriteAllBytes(Path_DoPX, My.Resources.ppmd_dopx)
                File.WriteAllBytes(Path_KaoUtil, My.Resources.ppmd_kaoutil)
                File.WriteAllBytes(Path_PackFileUtil, My.Resources.ppmd_packfileutil)
                File.WriteAllBytes(Path_PaletteTool, My.Resources.ppmd_palettetool)
                File.WriteAllBytes(Path_StatsUtil, My.Resources.ppmd_statsutil)
                File.WriteAllBytes(Path_UnPX, My.Resources.ppmd_unpx)
                File.WriteAllText(Path_Facenames, My.Resources.facenames)
                File.WriteAllText(Path_Pmd2Data, My.Resources.pmd2data)
                File.WriteAllText(Path_Pmd2Eos_CvInfo, My.Resources.pmd2eos_cvinfo)
                File.WriteAllText(Path_Pmd2ScriptData, My.Resources.pmd2scriptdata)
                File.WriteAllText(Path_Pokenames, My.Resources.pokenames)
            End If
        End SyncLock
    End Sub

    Private ReadOnly Property Path_AudioUtil As String
        Get
            Return Path.Combine(ToolDirectory, "ppmd_audioutil.exe")
        End Get
    End Property

    Private ReadOnly Property Path_DoPX As String
        Get
            Return Path.Combine(ToolDirectory, "ppmd_dopx.exe")
        End Get
    End Property

    Private ReadOnly Property Path_KaoUtil As String
        Get
            Return Path.Combine(ToolDirectory, "ppmd_kaoutil.exe")
        End Get
    End Property

    Private ReadOnly Property Path_PackFileUtil As String
        Get
            Return Path.Combine(ToolDirectory, "ppmd_packfileutil.exe")
        End Get
    End Property

    Private ReadOnly Property Path_PaletteTool As String
        Get
            Return Path.Combine(ToolDirectory, "ppmd_palettetool.exe")
        End Get
    End Property

    Private ReadOnly Property Path_StatsUtil As String
        Get
            Return Path.Combine(ToolDirectory, "ppmd_statsutil.exe")
        End Get
    End Property

    Private ReadOnly Property Path_UnPX As String
        Get
            Return Path.Combine(ToolDirectory, "ppmd_unpx.exe")
        End Get
    End Property

    Private ReadOnly Property Path_Facenames As String
        Get
            Return Path.Combine(ToolDirectory, "facenames.txt")
        End Get
    End Property

    Private ReadOnly Property Path_Pmd2Data As String
        Get
            Return Path.Combine(ToolDirectory, "pmd2data.xml")
        End Get
    End Property

    Private ReadOnly Property Path_Pmd2Eos_CvInfo As String
        Get
            Return Path.Combine(ToolDirectory, "pmd2eos_cvinfo.xml")
        End Get
    End Property

    Private ReadOnly Property Path_Pmd2ScriptData As String
        Get
            Return Path.Combine(ToolDirectory, "pmd2scriptdata.xml")
        End Get
    End Property

    Private ReadOnly Property Path_Pokenames As String
        Get
            Return Path.Combine(ToolDirectory, "pokenames.txt")
        End Get
    End Property

#End Region

#Region "Tool Execution"
    Public Async Function RunStatsUtil(extractedRomPath As String, xmlPath As String, options As StatsUtilOptions) As Task
        Dim args As New StringBuilder

        If options.IsImport Then
            args.Append("-i ")
        Else
            args.Append("-e ")
        End If

        If options.EnablePokemon Then
            args.Append("-pokemon ")
        End If

        If options.EnableMoves Then
            args.Append("-moves ")
        End If

        If options.EnableItems Then
            args.Append("-items ")
        End If

        If options.EnableScripts Then
            args.Append("-scripts ")

            If options.EnableScriptDebug Then
                args.Append("-scriptdebug ")
            End If
        End If

        args.Append("-romroot """)
        args.Append(AbsolutizePath(extractedRomPath))
        args.Append(""" """)
        args.Append(AbsolutizePath(xmlPath))
        args.Append("""")

        Await RunProgram(Path_StatsUtil, args.ToString.Trim)
    End Function

    Public Async Function RunUnPX(compressedFilename As String, outputFilename As String) As Task
        Await RunProgram(Path_UnPX, $"-fext ""{Path.GetExtension(outputFilename).TrimStart(".")}"" ""{AbsolutizePath(compressedFilename)}"" ""{AbsolutizePath(outputFilename)}""")
    End Function

    Dim _doPxTempDirectoryCreateLock As New Object
    Private Function GetDoPXTempDirectory() As String
        Dim tempDirectory As String = Path.Combine(ToolDirectory, "Compressed")
        If Not Directory.Exists(tempDirectory) Then 'Check to see if the directory is missing
            SyncLock _doPxTempDirectoryCreateLock 'Only one thread can create it
                If Not Directory.Exists(tempDirectory) Then 'Check again in case another thread already did it
                    Directory.CreateDirectory(tempDirectory)
                End If
            End SyncLock
        End If
        Return tempDirectory
    End Function

    Private Function GetDoPXOutputTempFilename(format As PXFormat) As String
        Dim tempOutput As String = Path.Combine(GetDoPXTempDirectory, Path.GetFileName("DoPX-Temp-" & Guid.NewGuid.ToString))

        Select Case format
            Case PXFormat.NotSpecified
                tempOutput &= ".bin"
            Case PXFormat.AT4PX
                tempOutput &= ".at4px"
            Case PXFormat.PKDPX
                tempOutput &= ".pkdpx"
            Case Else
                Throw New NotSupportedException("Only AT4PX and PKDPX compression formats are supported.")
        End Select
        Return tempOutput
    End Function

    Public Async Function RunDoPX(uncompressedFilename As String, outputFilename As String, format As PXFormat) As Task
        'Set up the output file
        Dim tempOutput = GetDoPXOutputTempFilename(format)

        'Read the data
        Await RunProgram(Path_DoPX, $"""{AbsolutizePath(uncompressedFilename)}"" ""{tempOutput}""")

        'Copy the file to the requested destination
        File.Copy(tempOutput, outputFilename, True)

        'Cleanup
        File.Delete(tempOutput)
    End Function

    Public Async Function RunDoPX(uncompressedData As Byte(), format As PXFormat) As Task(Of Byte())
        'Create a temporary file
        Dim tempUncompressedFilename = Path.GetTempFileName
        File.WriteAllBytes(tempUncompressedFilename, uncompressedData)

        'Set up the output file
        Dim tempOutput = GetDoPXOutputTempFilename(format)

        'Compress the data
        Await RunProgram(Path_DoPX, $"""{tempUncompressedFilename}"" ""{tempOutput}""")

        'Read the data
        Dim data = File.ReadAllBytes(tempOutput)

        'Cleanup
        File.Delete(tempUncompressedFilename)
        File.Delete(tempOutput)

        Return data
    End Function

    ''' <summary>
    ''' Runs Kaoutil
    ''' </summary>
    ''' <param name="sourcePath">The source path for the operation.  The kao file for extracts, the directory for packs.</param>
    ''' <param name="destinationPath">The destination path for the operation.  The directory for extracks, the kao file for packs.</param>
    ''' <param name="bitmaps">True to use indexed bitmap files.  False to use indexed PNG files.</param>
    ''' <param name="facenamesFilename">Optional file containing the names of the faces.  Pass in null for default.</param>
    ''' <param name="pokemonNamesFilename">Optional file containing the names of the Pokemon.  Pass in null for default.</param>
    Public Async Function RunKaoUtil(sourcePath As String, destinationPath As String, bitmaps As Boolean, Optional facenamesFilename As String = Nothing, Optional pokemonNamesFilename As String = Nothing) As Task
        If facenamesFilename Is Nothing Then
            facenamesFilename = Path_Facenames
        End If

        If pokemonNamesFilename Is Nothing Then
            pokemonNamesFilename = Path_Pokenames
        End If

        Dim bmpArg As String
        If bitmaps Then
            bmpArg = " -bmp"
        Else
            bmpArg = ""
        End If

        Await RunProgram(Path_KaoUtil, $"-fn ""{AbsolutizePath(facenamesFilename)}"" -pn ""{AbsolutizePath(pokemonNamesFilename)}""{bmpArg} ""{AbsolutizePath(sourcePath)}"" ""{AbsolutizePath(destinationPath)}""")
    End Function
#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).

                If _toolDirectory IsNot Nothing AndAlso Directory.Exists(_toolDirectory) Then
                    'Try to delete the tool directory
                    Try
                        Directory.Delete(_toolDirectory, True)
                    Catch ex As Exception
                        'If it fails, wait and try again
                        Thread.Sleep(500)
                        Try
                            Directory.Delete(_toolDirectory, True)
                        Catch ex2 As Exception
                            'If it fails again, leave the tool directory where it is; it's in the temp directory and may be cleaned up later
                        End Try
                    End Try
                End If
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        ' TODO: uncomment the following line if Finalize() is overridden above.
        ' GC.SuppressFinalize(Me)
    End Sub
#End Region
End Class
