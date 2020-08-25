Imports System.Text.RegularExpressions
Imports System.Globalization
Public Class Form1
    Dim destination1 As String
    Dim destination2 As String
    Private Sub Label1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label1_Click_1(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ofd_StudentList.FileName = Label1.Text
        If ofd_StudentList.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Label1.Text = ofd_StudentList.FileName
            destination1 = ofd_StudentList.FileName
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ofd_QuizTakerList.FileName = Label2.Text
        If ofd_QuizTakerList.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Label2.Text = ofd_QuizTakerList.FileName
            destination2 = ofd_QuizTakerList.FileName
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Using MyReader1 As New Microsoft.VisualBasic.FileIO.TextFieldParser(destination1)

            MyReader1.TextFieldType = Microsoft.VisualBasic.FileIO.FieldType.Delimited
            MyReader1.Delimiters = New String() {","}
            Dim currentRow1 As String
                Dim currentRow2 As String()
                Dim stringPattern_Name As New Regex("\s*[a-zA-Z'-]+,\s*[a-zA-Z'-]+\s*", RegexOptions.IgnoreCase)
            Dim stringPattern_SID As New Regex("\s*(?<SIDresult>\w+([.-]?\w+)*)@", RegexOptions.IgnoreCase)
            MyReader1.ReadLine()
                While Not MyReader1.EndOfData
                Try
                    currentRow1 = MyReader1.ReadLine()
                    Console.WriteLine(currentRow1)
                    Dim matchesName As Match = stringPattern_Name.Match(currentRow1, RegexOptions.IgnoreCase)
                    If matchesName.Success Then
                        Console.WriteLine(matchesName.Value)
                    End If
                    Dim matchesSID As Match = stringPattern_SID.Match(currentRow1, RegexOptions.IgnoreCase)
                    If matchesSID.Success Then
                        Console.WriteLine(matchesSID.Groups("SIDresult"))
                    End If
                    Using MyReader2 As New Microsoft.VisualBasic.FileIO.TextFieldParser(destination2)
                        MyReader2.TextFieldType = Microsoft.VisualBasic.FileIO.FieldType.Delimited
                        MyReader2.Delimiters = New String() {","}
                        MyReader2.ReadLine()
                        Dim output_Name As New Regex(matchesName.Value)
                        Dim output_SID As New Regex(matchesSID.Groups("SIDresult").ToString)
                        Dim matchedID As Boolean = False
                        While Not MyReader2.EndOfData
                            currentRow2 = MyReader2.ReadFields()

                            For Each currentField As String In currentRow2

                                If Regex.IsMatch(currentField, output_SID.ToString, RegexOptions.IgnoreCase) Then
                                    matchedID = True
                                    If currentRow2(5) <> 100 Then
                                        ListBox2.Items.Add(matchesName.Value & " - " & matchesSID.Groups("SIDresult").ToString)


                                        Exit For
                                    End If
                                End If
                            Next
                            If ((MyReader2.EndOfData = True) And (matchedID = False)) Then
                                ListBox1.Items.Add(matchesName.Value & " - " & matchesSID.Groups("SIDresult").ToString)
                            End If
                        End While
                    End Using
                Catch ex As Microsoft.VisualBasic.FileIO.MalformedLineException
                    MsgBox("Error in reading something?")
                    End Try
                End While

        End Using
    End Sub

    Private Sub ofd_QuizTakerList_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ofd_QuizTakerList.FileOk

    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        MsgBox("Student List Button allows you to select the list of students in the class.
               Quiz Taker's Button allows you to select the list of people who have been recorded
                taking the quiz and their corresponding scores. Hit the Compare button to compare the 
                two files. Please Contact Dan Gibbons (631) 456-7733 
                or dgibbo03@nyit.edu for further information or help.")
    End Sub
End Class
