Imports System.Windows.Forms

Public Class MainForm
    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Set up the form
        Me.Text = "Partha's Forms"
        Me.KeyPreview = True
        Me.ClientSize = New Size(400, 400) ' Adjust the form size as needed
        Me.BackColor = Color.LightSteelBlue

        ' Add heading label
        Dim lblHeading As New Label()
        lblHeading.Text = "Partha's Forms"
        lblHeading.Font = New Font("Arial", 16, FontStyle.Bold)
        lblHeading.Location = New Point(100, 20)
        lblHeading.Size = New Size(200, 30)
        lblHeading.TextAlign = ContentAlignment.MiddleCenter
        Me.Controls.Add(lblHeading)

        ' Add buttons
        Dim btnViewSubmissions As New Button()
        btnViewSubmissions.Name = "btnViewSubmissions"
        btnViewSubmissions.Text = "View Submissions (CTRL+V)"
        btnViewSubmissions.Location = New Point(50, 100)
        btnViewSubmissions.Size = New Size(280, 50)
        btnViewSubmissions.BackColor = Color.LightGreen
        btnViewSubmissions.Font = New Font("Arial", 12, FontStyle.Bold)
        AddHandler btnViewSubmissions.Click, AddressOf btnViewSubmissions_Click

        Dim btnCreateSubmission As New Button()
        btnCreateSubmission.Name = "btnCreateSubmission"
        btnCreateSubmission.Text = "Create New Submission (CTRL+N)"
        btnCreateSubmission.Location = New Point(50, 160)
        btnCreateSubmission.Size = New Size(280, 50)
        btnCreateSubmission.BackColor = Color.LightCoral
        btnCreateSubmission.Font = New Font("Arial", 12, FontStyle.Bold)
        AddHandler btnCreateSubmission.Click, AddressOf btnCreateSubmission_Click

        Dim btnSearch As New Button()
        btnSearch.Name = "btnSearch"
        btnSearch.Text = "Search by Email"
        btnSearch.Location = New Point(50, 220)
        btnSearch.Size = New Size(280, 50)
        btnSearch.BackColor = Color.LightBlue
        btnSearch.Font = New Font("Arial", 12, FontStyle.Bold)
        AddHandler btnSearch.Click, AddressOf btnSearch_Click

        ' Add buttons to the form
        Me.Controls.Add(btnViewSubmissions)
        Me.Controls.Add(btnCreateSubmission)
        Me.Controls.Add(btnSearch)
    End Sub

    Private Sub btnViewSubmissions_Click(sender As Object, e As EventArgs)
        Dim viewForm As New ViewSubmissionsForm()
        viewForm.Show()
    End Sub

    Private Sub btnCreateSubmission_Click(sender As Object, e As EventArgs)
        Dim createForm As New CreateSubmissionForm()
        createForm.Show()
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs)
        Dim searchForm As New SearchForm()
        searchForm.Show()
    End Sub

    Private Sub MainForm_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.V Then
            btnViewSubmissions_Click(sender, e)
        ElseIf e.Control AndAlso e.KeyCode = Keys.N Then
            btnCreateSubmission_Click(sender, e)
        ElseIf e.Control AndAlso e.KeyCode = Keys.S Then
            btnSearch_Click(sender, e)
        End If
    End Sub
End Class
