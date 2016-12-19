Imports System.IO
Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports PPMDU

<TestClass()> Public Class UtilityManagerTests

    <TestMethod()> Public Sub RunStatsUtil_Extract()
        Dim romFilename As String = "eos-u.nds"
        Dim romDir As String = "extractedROM"
        Dim xmlPath As String = "xmlFiles"

        'Set up
        Try
            File.WriteAllBytes(romFilename, My.Resources.EoS_U)
            Using extractor As New DotNet3dsToolkit.Converter
                extractor.ExtractNDS(romFilename, romDir).Wait()
            End Using
        Catch ex As Exception
            Assert.Inconclusive("Failed to set up.  Exception message: " & ex.Message)
        End Try

        'Test
        Using manager As New UtilityManager
            manager.RunStatsUtil(romDir, xmlPath, New StatsUtilOptions).Wait()
        End Using

        'Check (not that in-depth tbh)
        Assert.IsTrue(Directory.Exists(xmlPath))

        'Cleanup
        File.Delete(romFilename)
        Directory.Delete(romDir, True)
        'Directory.Delete(xmlPath, True)

    End Sub

End Class