Imports System.Net.Http
Imports System.Threading.Tasks
Imports Newtonsoft.Json.Linq

Public Class ViewSubmissionsForm
    Inherits Form

    Private currentIndex As Integer = 0
    Private submissions As List(Of JObject)

    Private WithEvents btnPrevious As New Button()
    Private WithEvents btnNext As New Button()
    Private WithEvents btnDelete As New Button()
    Private WithEvents btnModify As New Button()
    Private lblSubmission As New Label()

    Private Async Sub ViewSubmissionsForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "View Submissions"
        Me.ClientSize = New Size(450, 400) ' Adjust form size as needed
        Me.StartPosition = FormStartPosition.CenterScreen ' Center the form on startup
        Me.KeyPreview = True
        Me.BackColor = Color.LightSteelBlue

        ' Initialize controls
        InitializeControls()

        ' Load submissions
        Await LoadSubmissions()
        DisplaySubmission()
    End Sub

    Private Sub InitializeControls()
        ' Initialize btnPrevious Button
        btnPrevious.Location = New Point(50, 300)
        btnPrevious.Size = New Size(120, 40)
        btnPrevious.Text = "Previous (CTRL+P)"
        btnPrevious.BackColor = Color.LightSkyBlue
        btnPrevious.Font = New Font("Arial", 10, FontStyle.Bold)
        Me.Controls.Add(btnPrevious)

        ' Initialize btnNext Button
        btnNext.Location = New Point(200, 300)
        btnNext.Size = New Size(120, 40)
        btnNext.Text = "Next (CTRL+N)"
        btnNext.BackColor = Color.LightSkyBlue
        btnNext.Font = New Font("Arial", 10, FontStyle.Bold)
        Me.Controls.Add(btnNext)

        ' Initialize btnDelete Button
        btnDelete.Location = New Point(350, 300)
        btnDelete.Size = New Size(100, 40)
        btnDelete.Text = "Delete (CTRL+D)"
        btnDelete.BackColor = Color.LightCoral
        btnDelete.Font = New Font("Arial", 10, FontStyle.Bold)
        Me.Controls.Add(btnDelete)

        ' Initialize btnModify Button
        btnModify.Location = New Point(50, 350)
        btnModify.Size = New Size(120, 40)
        btnModify.Text = "Modify"
        btnModify.BackColor = Color.LightGreen
        btnModify.Font = New Font("Arial", 10, FontStyle.Bold)
        Me.Controls.Add(btnModify)

        ' Initialize lblSubmission Label
        lblSubmission.Location = New Point(50, 50)
        lblSubmission.Size = New Size(380, 230) ' Adjust size as needed
        lblSubmission.Text = "Submission Details"
        lblSubmission.TextAlign = ContentAlignment.TopLeft
        lblSubmission.BorderStyle = BorderStyle.FixedSingle ' Optional border style
        lblSubmission.Font = New Font("Arial", 10, FontStyle.Regular)
        Me.Controls.Add(lblSubmission)

        ' Attach event handlers
        AddHandler btnPrevious.Click, AddressOf btnPrevious_Click
        AddHandler btnNext.Click, AddressOf btnNext_Click
        AddHandler btnDelete.Click, AddressOf btnDelete_Click
        AddHandler btnModify.Click, AddressOf btnModify_Click
    End Sub

    Private Async Function LoadSubmissions() As Task
        submissions = New List(Of JObject)()
        Dim index As Integer = 0
        While True
            Using client As New HttpClient()
                Dim response = Await client.GetAsync($"http://localhost:3000/read?index={index}")
                If response.IsSuccessStatusCode Then
                    Dim responseString = Await response.Content.ReadAsStringAsync()
                    submissions.Add(JObject.Parse(responseString))
                    index += 1
                Else
                    Exit While
                End If
            End Using
        End While
    End Function

    Private Sub btnPrevious_Click(sender As Object, e As EventArgs)
        If currentIndex > 0 Then
            currentIndex -= 1
            DisplaySubmission()
        End If
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs)
        If currentIndex < submissions.Count - 1 Then
            currentIndex += 1
            DisplaySubmission()
        End If
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs)
        If currentIndex >= 0 AndAlso currentIndex < submissions.Count Then
            Dim confirmResult = MessageBox.Show("Are you sure you want to delete this submission?", "Confirm Delete", MessageBoxButtons.YesNo)
            If confirmResult = DialogResult.Yes Then
                DeleteSubmission(currentIndex)
            End If
        End If
    End Sub

    Private Async Sub DeleteSubmission(index As Integer)
        Try
            Using client As New HttpClient()
                Dim values As New Dictionary(Of String, String) From {
                    {"index", index.ToString()}
                }
                Dim content As New FormUrlEncodedContent(values)
                Dim response = Await client.PostAsync("http://localhost:3000/delete", content)

                If response.IsSuccessStatusCode Then
                    Dim responseString = Await response.Content.ReadAsStringAsync()
                    MessageBox.Show(responseString)
                    Await LoadSubmissions()

                    ' Adjust currentIndex if needed
                    If currentIndex >= submissions.Count Then
                        currentIndex = submissions.Count - 1
                    End If

                    DisplaySubmission()
                Else
                    MessageBox.Show($"Error: {response.StatusCode} - {response.ReasonPhrase}")
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show($"An error occurred: {ex.Message}")
        End Try
    End Sub

    Private Sub btnModify_Click(sender As Object, e As EventArgs)
        If currentIndex >= 0 AndAlso currentIndex < submissions.Count Then
            Dim modifyForm As New ModifySubmissionForm(currentIndex, Me)
            AddHandler modifyForm.FormClosed, AddressOf ModifyFormClosed
            modifyForm.ShowDialog()
        End If
    End Sub

    Private Sub ModifyFormClosed(sender As Object, e As EventArgs)
        ' Handle ModifySubmissionForm closed event to refresh submissions
        LoadSubmissions().Wait()
        DisplaySubmission()
    End Sub

    Private Sub DisplaySubmission()
        If submissions.Count = 0 Then
            lblSubmission.Text = "No submissions found."
            Return
        End If
        Dim submission = submissions(currentIndex)
        lblSubmission.Text = $"Name: {submission("name")}" & vbCrLf &
                             $"Email: {submission("email")}" & vbCrLf &
                             $"Phone: {submission("phone")}" & vbCrLf &
                             $"GitHub: {submission("github_link")}" & vbCrLf &
                             $"Time: {submission("stopwatch_time")}"
    End Sub

    Private Sub ViewSubmissionsForm_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.P Then
            btnPrevious_Click(sender, e)
        ElseIf e.Control AndAlso e.KeyCode = Keys.N Then
            btnNext_Click(sender, e)
        ElseIf e.Control AndAlso e.KeyCode = Keys.D Then
            btnDelete_Click(sender, e)
        End If
    End Sub
End Class
