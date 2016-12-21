Imports System.IO
Imports System.Security.Cryptography
Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports PPMDU

<TestClass()> Public Class UtilityManagerTests

    Dim romFilename As String = "eos-u.nds"
    Dim romDir As String = "extractedROM"


    <TestInitialize()> Public Sub TestInit()
        'Set up
        Try
            Using md5 As New MD5CryptoServiceProvider
                Dim hash = md5.ComputeHash(My.Resources.EoS_U)
                If Not hash.SequenceEqual(My.Resources.EoS_U_MD5) Then
                    Assert.Inconclusive("Incorrect test ROM specified.  Should be a trimmed North America PMD: Explorers of Sky ROM.")
                End If
            End Using

            File.WriteAllBytes(romFilename, My.Resources.EoS_U)
            Using extractor As New DotNet3dsToolkit.Converter
                extractor.ExtractNDS(romFilename, romDir).Wait()
            End Using
        Catch ex As Exception
            Assert.Inconclusive("Failed to set up.  Exception message: " & ex.Message)
        End Try
    End Sub

    <TestCleanup> Public Sub Cleanup()
        File.Delete(romFilename)
        Directory.Delete(romDir, True)
    End Sub

    <TestMethod()> Public Sub RunStatsUtil_Extract()
        Dim xmlPath As String = "xmlFiles"

        'Test
        Using manager As New UtilityManager
            manager.RunStatsUtil(romDir, xmlPath, New StatsUtilOptions).Wait()
        End Using

        'Check (not that in-depth tbh)
        Assert.IsTrue(Directory.Exists(xmlPath))

        'Cleanup
        Directory.Delete(xmlPath, True)

    End Sub

    <TestMethod()> Public Sub RunUnPX()
        Dim source = Path.Combine(romDir, "data", "BACK", "n_logo.bgp")
        Dim dest = "nlogo-dec.bin"

        'Test
        Using manager As New UtilityManager
            manager.UnPX(source, dest).Wait()
        End Using

        'Check
        Assert.IsTrue(File.Exists(dest), "Failed to create destination file.")

        Using md5 As New MD5CryptoServiceProvider
            Dim hash = md5.ComputeHash(File.ReadAllBytes(dest))
            Assert.IsTrue(hash.SequenceEqual(My.Resources.n_logo_MD5))
        End Using

        'Cleanup
        File.Delete(dest)
    End Sub

End Class