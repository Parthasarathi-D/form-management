Imports System.Net.Http
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports System.Drawing
Imports Newtonsoft.Json.Linq

Public Class ModifySubmissionForm
    Inherits Form

    Public Event SubmissionUpdated As EventHandler

    Private ReadOnly index As Integer
    Private ReadOnly submissionsForm As ViewSubmissionsForm

    Private WithEvents btnSave As New Button()
    Private WithEvents btnCancel As New Button()
    Private WithEvents txtName As New TextBox()
    Private WithEvents txtEmail As New TextBox()
    Private WithEvents txtPhone As New TextBox()
    Private WithEvents txtGitHub As New TextBox()
    Private WithEvents txtStopwatchTime As New TextBox()

    Public Sub New(index As Integer, submissionsForm As ViewSubmissionsForm)
        Me.index = index
        Me.submissionsForm = submissionsForm
        InitializeControls()
        LoadSubmissionDetails()
        Me.Text = "Modify Submission"
        Me.ClientSize = New Size(400, 300)
        Me.StartPosition = FormStartPosition.CenterScreen
    End Sub

    Private Sub InitializeControls()
        ' Initialize txtName TextBox
        txtName.Location = New Point(50, 30)
        txtName.Size = New Size(300, 20)
        Me.Controls.Add(txtName)

        ' Initialize txtEmail TextBox
        txtEmail.Location = New Point(50, 60)
        txtEmail.Size = New Size(300, 20)
        Me.Controls.Add(txtEmail)

        ' Initialize txtPhone TextBox
        txtPhone.Location = New Point(50, 90)
        txtPhone.Size = New Size(300, 20)
        Me.Controls.Add(txtPhone)

        ' Initialize txtGitHub TextBox
        txtGitHub.Location = New Point(50, 120)
        txtGitHub.Size = New Size(300, 20)
        Me.Controls.Add(txtGitHub)

        ' Initialize txtStopwatchTime TextBox
        txtStopwatchTime.Location = New Point(50, 150)
        txtStopwatchTime.Size = New Size(300, 20)
        Me.Controls.Add(txtStopwatchTime)

        ' Initialize btnSave Button
        btnSave.Location = New Point(100, 200)
        btnSave.Size = New Size(80, 30)
        btnSave.Text = "Save"
        btnSave.BackColor = Color.LightGreen
        btnSave.Font = New Font("Arial", 10, FontStyle.Bold)
        Me.Controls.Add(btnSave)

        ' Initialize btnCancel Button
        btnCancel.Location = New Point(200, 200)
        btnCancel.Size = New Size(80, 30)
        btnCancel.Text = "Cancel"
        btnCancel.BackColor = Color.LightCoral
        btnCancel.Font = New Font("Arial", 10, FontStyle.Bold)
        Me.Controls.Add(btnCancel)

        ' Attach event handlers
        AddHandler btnSave.Click, AddressOf btnSave_Click
        AddHandler btnCancel.Click, AddressOf btnCancel_Click
    End Sub

    Private Async Sub LoadSubmissionDetails()
        Try
            Using client As New HttpClient()
                Dim response = Await client.GetAsync($"http://localhost:3000/read?index={index}")

                If response.IsSuccessStatusCode Then
                    Dim responseString = Await response.Content.ReadAsStringAsync()
                    Dim submission = JObject.Parse(responseString)

                    ' Load existing submission details into text boxes
                    txtName.Text = submission("name").ToString()
                    txtEmail.Text = submission("email").ToString()
                    txtPhone.Text = submission("phone").ToString()
                    txtGitHub.Text = submission("github_link").ToString()
                    txtStopwatchTime.Text = submission("stopwatch_time").ToString()
                Else
                    MessageBox.Show("Failed to retrieve submission details.")
                    Me.Close()
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show($"An error occurred: {ex.Message}")
        End Try
    End Sub

    Private Async Sub btnSave_Click(sender As Object, e As EventArgs)
        Dim name As String = txtName.Text.Trim()
        Dim email As String = txtEmail.Text.Trim()
        Dim phone As String = txtPhone.Text.Trim()
        Dim githubLink As String = txtGitHub.Text.Trim()
        Dim stopwatchTime As String = txtStopwatchTime.Text.Trim()

        If String.IsNullOrWhiteSpace(name) OrElse
           String.IsNullOrWhiteSpace(email) OrElse
           String.IsNullOrWhiteSpace(phone) OrElse
           String.IsNullOrWhiteSpace(githubLink) OrElse
           String.IsNullOrWhiteSpace(stopwatchTime) Then
            MessageBox.Show("Please fill in all fields.")
            Return
        End If

        Try
            Using client As New HttpClient()
                Dim values As New Dictionary(Of String, String) From {
                    {"index", index.ToString()},
                    {"name", name},
                    {"email", email},
                    {"phone", phone},
                    {"github_link", githubLink},
                    {"stopwatch_time", stopwatchTime}
                }
                Dim content As New FormUrlEncodedContent(values)
                Dim response = Await client.PostAsync("http://localhost:3000/update", content)

                If response.IsSuccessStatusCode Then
                    MessageBox.Show("Submission updated successfully.")
                    ' Fire event to notify the parent form (ViewSubmissionsForm) that a submission was updated
                    RaiseEvent SubmissionUpdated(Me, EventArgs.Empty)
                    Me.Close()
                Else
                    MessageBox.Show($"Error: {response.StatusCode} - {response.ReasonPhrase}")
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show($"An error occurred: {ex.Message}")
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub
End Class
