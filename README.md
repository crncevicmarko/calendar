# Calendar Project

## About This Project

### Employee Calendar and Absence Tracker

This project is a desktop application developed for internal company use. It helps employees schedule meetings and track their absences. The application is built using WPF, .NET, and MSSQL, providing a user-friendly interface for managing calendars, scheduling meetings, and requesting absences. Administrators can manage all employees' data, approve or reject absence requests, and view all scheduled meetings.

### Features

- **User Authentication**:  
  Users can register and log in securely. Once logged in, they access their personal dashboard.
  
- **Personal Calendar Management**:  
  Each user has access to their personal calendar, where they can see all meetings and absences.

- **Absence Requests**:  
  Users can submit absence requests (e.g., vacation, sick leave) which admins can approve or reject.

- **Meeting Scheduling**:  
  Users can schedule meetings, either for online sessions or in physical rooms.

- **Admin Privileges**:  
  Administrators can view, approve, and manage all absence requests and meetings for employees.

### Usage

To use this project:
1. **Download Visual Studio**:  
   If you don't have Visual Studio installed, download it from the [Visual Studio Download Page](https://visualstudio.microsoft.com/downloads/).

2. **Clone the repository**:  
   Open a terminal or command prompt and run the following command:
   ```bash
   git clone https://github.com/crncevicmarko/calendar.git
3. **Open the solution in Visual Studio**:
    Navigate to the cloned directory and open the `.sln` file in Visual Studio.
4. **Set Up the MSSQL Database**:
   - Open SQL Server Management Studio (SSMS).
   - Connect to your SQL Server instance.
   - Create a new database (e.g., `calendar`) if one does not already exist.
   - Configure the connection string in the project file `Config.cs` to connect to your SQL Server and the newly created database.

5. **Build and run the project**:
   - Click on Build in the top menu and select Build Solution.
   - Start debugging by pressing F5 or clicking the Start button in Visual Studio.
   - Log in as a user or admin to start managing calendars and meetings.

### Technologies Used

- WPF (Windows Presentation Foundation)
- .NET Framework (C#)
- MSSQL (Microsoft SQL Server)
