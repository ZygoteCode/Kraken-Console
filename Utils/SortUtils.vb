Public Class SortUtils
    Public Shared Sub QuickSort(ByVal arr As Integer(), ByVal left As Integer, ByVal right As Integer)
        If left < right Then
            Dim pivot As Integer = Partition(arr, left, right)
            If pivot > 1 Then
                QuickSort(arr, left, pivot - 1)
            End If
            If pivot + 1 < right Then
                QuickSort(arr, pivot + 1, right)
            End If
        End If
    End Sub
    Private Shared Function Partition(ByVal arr As Integer(), ByVal left As Integer, ByVal right As Integer) As Integer
        Dim pivot As Integer = arr(left)
        While True
            While arr(left) < pivot
                left += 1
            End While
            While arr(right) > pivot
                right -= 1
            End While
            If left < right Then
                If arr(left) = arr(right) Then Return right
                Dim temp As Integer = arr(left)
                arr(left) = arr(right)
                arr(right) = temp
            Else
                Return right
            End If
        End While
    End Function
End Class