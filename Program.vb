Imports System.IO

Public Class Medicine
    Public Property Name As String
    Public Property ExpirationDate As Integer
    Public Property ProductionDate As Date
    Public Property Count As Integer
    Public Property Price As Double

End Class

Module Program
    Sub isAllFieldEmpty(medicine As Medicine)
        If (medicine.Name = "") Then
            Throw New System.Exception("The field 'Name' is not filled in")
        End If
        If (medicine.ExpirationDate <= 0) Then
            Throw New System.Exception("The field 'Expiration date' is not filled in")
        End If
        If (medicine.Price <= 0) Then
            Throw New System.Exception("The field 'Price' is not filled in")
        End If
    End Sub

    Sub isEmpty(medicines As List(Of Medicine))
        If (medicines.Count = 0) Then
            Throw New System.Exception("List is empty")
        End If
    End Sub

    Sub isEmpty(medicines As IEnumerable(Of Medicine))
        If (medicines.Count = 0) Then
            Throw New System.Exception("List is empty")
        End If
    End Sub

    Sub ReadFile(path As String, medicines As List(Of Medicine))
        Try
            Using reader As New StreamReader(path, System.Text.Encoding.UTF8)
                Dim line As String = reader.ReadLine()

                While (line IsNot Nothing)
                    If medicines.Count <> 0 Then
                        line = reader.ReadLine()
                    End If

                    If line = "" Then
                        Exit While
                    End If
                    Dim item As New Medicine
                    item.Name = line

                    line = reader.ReadLine()
                    item.ExpirationDate = CInt(line)
                    line = reader.ReadLine()
                    item.ProductionDate = CDate(line)
                    line = reader.ReadLine()
                    item.Count = Int(line)
                    line = reader.ReadLine()
                    item.Price = CDbl(line)

                    isAllFieldEmpty(item)
                    medicines.Add(item)
                End While
            End Using
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Sub PrintList(medicines As List(Of Medicine))
        Try
            isEmpty(medicines)
            Dim str = String.Format("| {0, -10} | {1, -20} | {2, -21} | {3, -10} | {4, -10} |",
                               "Name",
                               "Expiration Date",
                               "Production Date",
                               "Count",
                               "Price")

            Console.WriteLine(str)
            For i = 0 To medicines.Count - 1 Step 1
                str = String.Format("| {0, -10} | {1, -20} | {2, -21} | {3, -10} | {4, -10} |",
                                    medicines.Item(i).Name,
                                    medicines.Item(i).ExpirationDate & " year",
                                    medicines.Item(i).ProductionDate,
                                    medicines.Item(i).Count,
                                    medicines.Item(i).Price)
                Console.WriteLine(str)
            Next i
            Console.WriteLine()
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Sub PrintList(medicines As IEnumerable(Of Medicine))
        Try
            isEmpty(medicines)
            Dim str = String.Format("| {0, -10} | {1, -20} | {2, -21} | {3, -10} | {4, -10} |",
                               "Name",
                               "Expiration Date",
                               "Production Date",
                               "Count",
                               "Price")

            Console.WriteLine(str)
            For Each item In medicines
                str = String.Format("| {0, -10} | {1, -20} | {2, -21} | {3, -10} | {4, -10} |",
                                    item.Name,
                                    item.ExpirationDate & " year",
                                    item.ProductionDate,
                                    item.Count,
                                    item.Price)
                Console.WriteLine(str)
            Next
            Console.WriteLine()
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Sub AddNewMedicine(medicines As List(Of Medicine))
        Try
            Dim item As New Medicine
            Console.Write("Enter name: ")
            item.Name = Console.ReadLine()
            Console.Write("Enter expiration date: ")
            item.ExpirationDate = CInt(Console.ReadLine())
            Console.Write("Enter production date: ")
            item.ProductionDate = CDate(Console.ReadLine())
            Console.Write("Enter count: ")
            item.Count = CInt(Console.ReadLine())
            Console.Write("Enter price: ")
            item.Price = CDbl(Console.ReadLine())
            isAllFieldEmpty(item)
            medicines.Add(item)
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Function StartsWith(str As String, medicines As List(Of Medicine)) As IEnumerable(Of Medicine)
        Try
            isEmpty(medicines)
            StartsWith = From item In medicines
                         Where item.Name.StartsWith(str)
                         Select item

            Dim path = "D:\c#\vb\lab4\ConsoleApp1\ConsoleApp1\MyQuery.txt"

            Using writer As New StreamWriter(path, False, System.Text.Encoding.UTF8)
                For Each item In StartsWith
                    writer.WriteLine(item.Name)
                Next
            End Using
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Function

    Function ExpiredMedication(medicines As List(Of Medicine)) As IEnumerable(Of Medicine)
        Try
            isEmpty(medicines)
            ExpiredMedication = From item In medicines
                                Order By item.Name
                                Where DateDiff(DateInterval.Day, item.ProductionDate, Today) > item.ExpirationDate * 365
                                Select item
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Function


    Sub Main(args As String())
        Dim medicines As New List(Of Medicine)
        Dim path As String = "D:\c#\vb\lab4\ConsoleApp1\ConsoleApp1\medicines.txt"

        ReadFile(path, medicines)
        PrintList(medicines)

        AddNewMedicine(medicines)
        PrintList(medicines)

        Dim forStart = "A"

        Console.WriteLine("Starts with '" & forStart & "'")
        PrintList(StartsWith(forStart, medicines))

        Console.WriteLine("Expired medications")
        PrintList(ExpiredMedication(medicines))


    End Sub
End Module
