Imports System.IO
Imports System.Security.Cryptography
Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports PPMDU

<TestClass()> Public Class UtilityManagerTests

    'Files for all tests
    Dim romFilename As String = "eos-u.nds"
    Dim romDir As String = "extractedROM"

    'Files for some tests
    Dim destDec = "nlogo-dec.bin"


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

        If File.Exists(destDec) Then
            File.Delete(destDec)
        End If
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

    <TestMethod> Public Sub RunUnPx()
        Dim source = Path.Combine(romDir, "data", "BACK", "n_logo.bgp")

        'Test UnPX
        Using manager As New UtilityManager
            manager.UnPX(source, destDec).Wait()
        End Using

        'Check UnPX
        Assert.IsTrue(File.Exists(destDec), "Failed to create decompressed file.")

        Using md5 As New MD5CryptoServiceProvider
            Dim hash = md5.ComputeHash(File.ReadAllBytes(destDec))
            Assert.IsTrue(hash.SequenceEqual(My.Resources.n_logo_MD5), "Uncompressed file hash mismatch")
        End Using
    End Sub

    <TestMethod> Public Sub RunDoPxAT4PX()
        Try
            RunUnPx()
        Catch ex As Exception
            Assert.Inconclusive("Dependant test RunUnPx failed.  Message: " & ex.ToString)
        End Try

        Dim destCompAT4PX = "nlogo-at4px-comp.bin"
        Dim destDecAT4PX = "nlogo-at4px-dec.bin"

        'Test DoPX AT4PX
        Using manager As New UtilityManager
            manager.DoPX(destDec, destCompAT4PX, PXFormat.AT4PX).Wait()

            'First DoPX AT4PX Check
            Assert.IsTrue(File.Exists(destCompAT4PX), "Failed to create compressed AT4PX file.")
            Dim rawData = File.ReadAllBytes(destCompAT4PX)
            Assert.AreEqual("AT4PX", Encoding.ASCII.GetString(rawData, 0, 5))

            'Prep for second check
            manager.UnPX(destCompAT4PX, destDecAT4PX).Wait()
        End Using

        'Second DoPX AT4PX Check
        Assert.IsTrue(File.Exists(destDecAT4PX), "Failed to create decompressed AT4PX file.")

        Using md5 As New MD5CryptoServiceProvider
            Dim hash = md5.ComputeHash(File.ReadAllBytes(destDecAT4PX))
            Assert.IsTrue(hash.SequenceEqual(My.Resources.n_logo_MD5), "AT4PX re-uncompressed file hash mismatch")
        End Using

        'Cleanup
        File.Delete(destCompAT4PX)
        File.Delete(destDecAT4PX)
    End Sub

    <TestMethod()> Public Sub RunDoPxPKDPX()
        Try
            RunUnPx()
        Catch ex As Exception
            Assert.Inconclusive("Dependant test RunUnPx failed.  Message: " & ex.ToString)
        End Try

        Dim destCompPKDPX = "nlogo-pkdpx-comp.bin"
        Dim destDecPKDPX = "nlogo-pkdpx-dec.bin"

        'Test DoPX PKDPX
        Using manager As New UtilityManager
            manager.DoPX(destDec, destCompPKDPX, PXFormat.PKDPX).Wait()

            'First DoPX AT4PX Check
            Assert.IsTrue(File.Exists(destCompPKDPX), "Failed to create compressed PKDPX file.")
            Dim rawData = File.ReadAllBytes(destCompPKDPX)
            Assert.AreEqual("PKDPX", Encoding.ASCII.GetString(rawData, 0, 5))

            'Prep for second check
            manager.UnPX(destCompPKDPX, destDecPKDPX).Wait()
        End Using

        'Second DoPX AT4PX Check
        Assert.IsTrue(File.Exists(destDecPKDPX), "Failed to create decompressed PKDPX file.")

        Using md5 As New MD5CryptoServiceProvider
            Dim hash = md5.ComputeHash(File.ReadAllBytes(destDecPKDPX))
            Assert.IsTrue(hash.SequenceEqual(My.Resources.n_logo_MD5), "PKDPX re-uncompressed file hash mismatch")
        End Using

        'Cleanup
        File.Delete(destCompPKDPX)
        File.Delete(destDecPKDPX)
    End Sub

End Class