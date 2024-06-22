# Submissions Management Application
This application allows users to manage submissions through various functionalities: viewing, creating, searching by email, modifying, and deleting submissions.

## Features
### Main Form

![image](https://github.com/Parthasarathi-D/form-management/assets/141064484/57f4fcdc-23b3-49f3-8946-5675b32a43b8)
### 1. View Submissions
Displays a list of all submissions.
Allows navigation through submissions.
Provides options to view, delete, and modify each submission.

![image](https://github.com/Parthasarathi-D/form-management/issues/2#issue-2366924091)
### 2. New Submission
Opens a form to enter details for a new submission.
Validates and saves the new submission to the backend server.

![image](https://github.com/Parthasarathi-D/form-management/assets/141064484/30a27567-b2ac-4931-ba54-9b7d947c6039)
### 3. Search by Email
Enables searching for submissions based on the user's email address.
Retrieves and displays submission details associated with the entered email.

![image](https://github.com/Parthasarathi-D/form-management/assets/141064484/24356224-61e2-4096-a7ff-3524bc87f949)

## View Submissions Functionality
### View: 
Allows detailed viewing of submission information, including name, email, phone, GitHub link, and stopwatch time.
### Modify: 
Opens a form to edit existing submission details.

![image](https://github.com/Parthasarathi-D/form-management/assets/141064484/62c7178c-1e83-4ee9-8cca-d8032c241cf8)
### Delete: 
Permanently removes the selected submission from the database after confirmation.

![image](https://github.com/Parthasarathi-D/form-management/assets/141064484/c2b5f54b-7de9-4461-a566-dda9ba068453)

## How to Use
### Main Form

### View Submissions:

Launch the application and navigate to the "View Submissions" tab.
Use navigation controls (e.g., buttons or pagination) to browse through all recorded submissions.
Click on a submission to view its details.
Use "Delete" button to delete a submission after confirmation.
Click "Modify" button to edit the details of a submission.

### New Submission:

Navigate to the "New Submission" tab from the main menu.
Enter all required details (name, email, phone, GitHub link, stopwatch time) for the new submission.
Click "Save" to validate and save the new submission to the server.
Optionally, click "Cancel" to discard the entered details and close the form.

![image](https://github.com/Parthasarathi-D/form-management/assets/141064484/ffe97b74-50b1-40f0-8879-957a1fbb9d0d)

### Search by Email:

Go to the "Search by Email" tab.
Enter an email address in the search field.
Click "Search" to retrieve and display submission details associated with the entered email.
If found, details such as name, email, phone, GitHub link, and stopwatch time will be shown.

![image](https://github.com/Parthasarathi-D/form-management/assets/141064484/134f9cf3-1ccf-4dbe-8b7a-79939fa2e540)

### View Submissions Detail

## View:
Upon selecting a submission from the list, detailed information will be displayed.
Information includes name, email, phone, GitHub link, and stopwatch time.

## Modify:
Clicking "Modify" from the view page opens a form to edit the current submission's details.
Update the fields as needed and click "Save" to apply changes to the server.
Upon successful update, the main form automatically refreshes to reflect changes.

![image](https://github.com/Parthasarathi-D/form-management/assets/141064484/2c1a9500-5274-400a-b9c0-efacb1bd84be)

## Delete:
Confirm deletion of a submission by clicking "Delete".
Once confirmed, the submission will be permanently removed from the database.

![image](https://github.com/Parthasarathi-D/form-management/assets/141064484/d33a4020-6382-4ae5-8b86-db50e24f28ec)


# Requirements
### Environment: Windows OS
### Dependencies: .NET Framework, HTTP server (e.g., http://localhost:3000).
### External APIs: Uses HTTP requests to interact with the backend server for CRUD operations.

## Troubleshooting
Ensure the backend server (http://localhost:3000) is running and accessible.
Check network connectivity and server response for any errors during CRUD operations.
If errors occur during submission creation, modification, or deletion, appropriate error messages will be displayed for guidance.

![image](https://github.com/Parthasarathi-D/form-management/assets/141064484/133c0acc-c15d-4c42-a92a-4ee9d199e6b8)

## Notes
This application provides a comprehensive interface for managing submissions with CRUD operations.
Use keyboard shortcuts for quick navigation within forms (if implemented).
Customize and expand the application based on specific requirements or additional features.
