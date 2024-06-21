Imports System.Net.Http
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports System.Drawing
Imports Newtonsoft.Json.Linq

Public Class SearchForm
    Inherits Form

    Private WithEvents txtEmail As New TextBox()
    Private WithEvents btnSearch As New Button()
    Private WithEvents lblResults As New Label()

    Public Sub New()
        InitializeControls()
        Me.Text = "Search by Email"
        Me.ClientSize = New Size(400, 300) ' Increased form size
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.BackColor = Color.LightGray ' Set background color
    End Sub

    Private Sub InitializeControls()
        ' Initialize label and text box
        txtEmail.Location = New Point(50, 50)
        txtEmail.Size = New Size(300, 30) ' Increased text box size
        txtEmail.Font = New Font("Arial", 12, FontStyle.Regular) ' Adjust font size and style
        Me.Controls.Add(txtEmail)

        ' Initialize search button
        btnSearch.Location = New Point(50, 100)
        btnSearch.Size = New Size(150, 40) ' Increased button size
        btnSearch.Text = "Search"
        btnSearch.BackColor = Color.LightBlue ' Set button color
        btnSearch.Font = New Font("Arial", 12, FontStyle.Bold) ' Adjust font size and style
        Me.Controls.Add(btnSearch)

        ' Initialize label for displaying results
        lblResults.Location = New Point(50, 160)
        lblResults.Size = New Size(300, 120) ' Increased label size
        lblResults.TextAlign = ContentAlignment.TopLeft
        lblResults.BorderStyle = BorderStyle.FixedSingle
        lblResults.Font = New Font("Arial", 12, FontStyle.Regular) ' Adjust font size and style
        Me.Controls.Add(lblResults)

        ' Attach event handlers
        AddHandler btnSearch.Click, AddressOf btnSearch_Click
    End Sub

    Private Async Sub btnSearch_Click(sender As Object, e As EventArgs)
        Dim email As String = txtEmail.Text.Trim()

        If String.IsNullOrWhiteSpace(email) Then
            MessageBox.Show("Please enter an email address.")
            Return
        End If

        Try
            Using client As New HttpClient()
                Dim response = Await client.GetAsync($"http://localhost:3000/search?email={email}")

                If response.IsSuccessStatusCode Then
                    Dim responseString = Await response.Content.ReadAsStringAsync()
                    Dim userDetails = JObject.Parse(responseString)

                    ' Display details
                    lblResults.Text = $"Name: {userDetails("name")}" & vbCrLf &
                                      $"Email: {userDetails("email")}" & vbCrLf &
                                      $"Phone: {userDetails("phone")}" & vbCrLf &
                                      $"GitHub: {userDetails("github_link")}" & vbCrLf &
                                      $"Time: {userDetails("stopwatch_time")}"
                Else
                    lblResults.Text = "User not found."
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show($"Error: {ex.Message}")
        End Try
    End Sub
End Class
