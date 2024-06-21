Imports System.Net.Http
Imports System.Threading.Tasks
Imports System.Windows.Forms
Imports System.Drawing
Imports System.Diagnostics

Public Class CreateSubmissionForm
    Inherits Form

    Private WithEvents txtName As New TextBox()
    Private WithEvents txtEmail As New TextBox()
    Private WithEvents txtPhone As New TextBox()
    Private WithEvents txtGithub As New TextBox()
    Private WithEvents btnStopwatch As New Button()
    Private WithEvents btnSubmit As New Button()
    Private WithEvents lblName As New Label()
    Private WithEvents lblEmail As New Label()
    Private WithEvents lblPhone As New Label()
    Private WithEvents lblGithub As New Label()
    Private WithEvents lblTimer As New Label()

    Private stopwatch As New Stopwatch()
    Private timerUpdate As New Timer()

    Public Sub New()
        InitializeControls()
        Me.Text = "Create New Submission"
        Me.ClientSize = New Size(320, 320)
        Me.StartPosition = FormStartPosition.CenterScreen

        ' Configure timer update
        timerUpdate.Interval = 1000 ' 1 second interval
        AddHandler timerUpdate.Tick, AddressOf TimerUpdate_Tick
    End Sub

    Private Sub InitializeControls()
        ' Initialize labels
        lblName.Text = "Name:"
        lblName.Location = New Point(10, 50)
        lblName.Size = New Size(40, 20)
        Me.Controls.Add(lblName)

        lblEmail.Text = "Email:"
        lblEmail.Location = New Point(10, 80)
        lblEmail.Size = New Size(40, 20)
        Me.Controls.Add(lblEmail)

        lblPhone.Text = "Phone:"
        lblPhone.Location = New Point(10, 110)
        lblPhone.Size = New Size(40, 20)
        Me.Controls.Add(lblPhone)

        lblGithub.Text = "GitHub:"
        lblGithub.Location = New Point(10, 140)
        lblGithub.Size = New Size(50, 20)
        Me.Controls.Add(lblGithub)

        ' Initialize text boxes
        txtName.Location = New Point(60, 50)
        txtName.Size = New Size(200, 20)
        Me.Controls.Add(txtName)

        txtEmail.Location = New Point(60, 80)
        txtEmail.Size = New Size(200, 20)
        Me.Controls.Add(txtEmail)

        txtPhone.Location = New Point(60, 110)
        txtPhone.Size = New Size(200, 20)
        Me.Controls.Add(txtPhone)

        txtGithub.Location = New Point(60, 140)
        txtGithub.Size = New Size(200, 20)
        Me.Controls.Add(txtGithub)

        ' Initialize stopwatch button
        btnStopwatch.Location = New Point(60, 180)
        btnStopwatch.Size = New Size(120, 40)
        btnStopwatch.Text = "Start"
        Me.Controls.Add(btnStopwatch)

        ' Initialize submit button
        btnSubmit.Location = New Point(10, 240)
        btnSubmit.Size = New Size(120, 40)
        btnSubmit.Text = "Submit (Ctrl+S)"
        btnSubmit.BackColor = Color.FromArgb(46, 204, 113) ' Green color for emphasis
        btnSubmit.ForeColor = Color.White
        btnSubmit.Font = New Font("Arial", 12, FontStyle.Bold)
        Me.Controls.Add(btnSubmit)

        ' Initialize timer label
        lblTimer.Text = "Timer: 00:00:00"
        lblTimer.Location = New Point(200, 180)
        lblTimer.Size = New Size(100, 20)
        Me.Controls.Add(lblTimer)
    End Sub

    Private Sub btnStopwatch_Click(sender As Object, e As EventArgs) Handles btnStopwatch.Click
        If stopwatch.IsRunning Then
            stopwatch.Stop()
            btnStopwatch.Text = "Start"
        Else
            stopwatch.Start()
            btnStopwatch.Text = "Stop"
            UpdateTimerDisplay()
            timerUpdate.Start() ' Start timer update
        End If
    End Sub

    Private Sub TimerUpdate_Tick(sender As Object, e As EventArgs)
        UpdateTimerDisplay()
    End Sub

    Private Sub UpdateTimerDisplay()
        Dim elapsed As TimeSpan = stopwatch.Elapsed
        lblTimer.Text = String.Format("Timer: {0:00}:{1:00}:{2:00}", elapsed.Hours, elapsed.Minutes, elapsed.Seconds)
    End Sub

    Private Async Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Dim name As String = txtName.Text
        Dim email As String = txtEmail.Text
        Dim phone As String = txtPhone.Text
        Dim github As String = txtGithub.Text
        Dim stopwatchTime As String = stopwatch.Elapsed.ToString()

        Using client As New HttpClient()
            Dim values As New Dictionary(Of String, String) From {
                {"name", name},
                {"email", email},
                {"phone", phone},
                {"github_link", github},
                {"stopwatch_time", stopwatchTime}
            }
            Dim content As New FormUrlEncodedContent(values)
            Dim response = Await client.PostAsync("http://localhost:3000/submit", content)
            Dim responseString = Await response.Content.ReadAsStringAsync()
            MessageBox.Show(responseString)

            txtName.Text = ""
            txtEmail.Text = ""
            txtPhone.Text = ""
            txtGithub.Text = ""
            stopwatch.Reset()
            btnStopwatch.Text = "Start"
            lblTimer.Text = "Timer: 00:00:00"


        End Using
    End Sub

    Private Sub CreateSubmissionForm_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.S Then
            btnSubmit_Click(sender, e)
        End If
    End Sub
End Class
