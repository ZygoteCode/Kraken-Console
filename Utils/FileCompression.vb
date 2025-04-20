Imports System.IO
Imports System.IO.Compression
Public Class FileCompression
    Public Shared Sub CompressFile(ByVal InputPath As String, ByVal OutputPath As String)
        If System.IO.File.Exists(InputPath) Then
            If System.IO.File.Exists(OutputPath) Then
                System.IO.File.Delete(OutputPath)
            End If
            Dim fileBytes As Byte() = System.IO.File.ReadAllBytes(InputPath)
            Dim gZipBuffer As Byte() = New Byte() {}
            Dim bufferh As Byte() = fileBytes
            Dim rounds As Integer = 5
            For i = 0 To rounds
                Dim memoryStream = New MemoryStream()
                Using gZipStream = New GZipStream(memoryStream, CompressionMode.Compress, True)
                    gZipStream.Write(bufferh, 0, bufferh.Length)
                End Using
                memoryStream.Position = 0
                Dim compressedData = New Byte(memoryStream.Length - 1) {}
                memoryStream.Read(compressedData, 0, compressedData.Length)
                gZipBuffer = New Byte(compressedData.Length + 4 - 1) {}
                Buffer.BlockCopy(compressedData, 0, gZipBuffer, 4, compressedData.Length)
                Buffer.BlockCopy(BitConverter.GetBytes(bufferh.Length), 0, gZipBuffer, 0, 4)
                bufferh = gZipBuffer
            Next
            Array.Reverse(gZipBuffer)
            System.IO.File.WriteAllBytes(OutputPath, gZipBuffer)
            Dim pathT As String = OutputPath.Replace(Path.GetFileName(OutputPath), "")
            System.IO.Directory.CreateDirectory(pathT + "\cya")
            System.IO.File.Move(OutputPath, pathT + "\cya\" + Path.GetFileName(OutputPath))
            ZipFile.CreateFromDirectory(pathT + "\cya", pathT + "\ciao.zip")
            System.IO.File.Delete(OutputPath)
            System.IO.File.Move(pathT + "\ciao.zip", OutputPath)
            System.IO.Directory.Delete(pathT + "\cya", True)
            DataAdder.AddData("Succesfully compressed. Bytes saved from compression: " + ((FileLen(InputPath)) - (FileLen(OutputPath))).ToString() + " bytes")
        End If
    End Sub
    Public Shared Sub UncompressFile(ByVal InputPath As String, ByVal OutputPath As String)
        If System.IO.File.Exists(InputPath) Then
            If System.IO.File.Exists(OutputPath) Then
                System.IO.File.Delete(OutputPath)
            End If
            System.IO.File.Move(InputPath, InputPath.Replace(Path.GetExtension(InputPath), "") + ".zip")
            Dim pathT As String = InputPath.Replace(Path.GetFileName(InputPath), "")
            ZipFile.ExtractToDirectory(InputPath.Replace(Path.GetExtension(InputPath), "") + ".zip", pathT)
            Dim fileBytes As Byte() = System.IO.File.ReadAllBytes(InputPath)
            System.IO.File.Delete(InputPath.Replace(Path.GetExtension(InputPath), "") + ".zip")
            Array.Reverse(fileBytes)
            Dim outputBytes As Byte() = New Byte() {}
            Dim gZipBuffer As Byte() = fileBytes
            Dim rounds As Integer = 5
            For i = 0 To rounds
                Using memoryStream = New MemoryStream()
                    Dim dataLength As Integer = BitConverter.ToInt32(gZipBuffer, 0)
                    memoryStream.Write(gZipBuffer, 4, gZipBuffer.Length - 4)
                    Dim buffer = New Byte(dataLength - 1) {}
                    memoryStream.Position = 0
                    Using gZipStream = New GZipStream(memoryStream, CompressionMode.Decompress)
                        gZipStream.Read(buffer, 0, buffer.Length)
                    End Using
                    outputBytes = buffer
                    gZipBuffer = outputBytes
                End Using
            Next
            System.IO.File.WriteAllBytes(OutputPath, outputBytes)
            DataAdder.AddData("Succesfully uncompressed.")
        End If
    End Sub
End Class