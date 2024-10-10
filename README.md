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
5. **Run the Database Script**:
   - Navigate to the `DataBaseSQL` folder in your cloned repository.
   - Open the `Calendar.sql` file.
   - Execute the SQL script by clicking on the "Execute" button in SSMS. This will create all the necessary tables and relationships needed for the application.
6. **Build and Run the Project**:
   - In Visual Studio, click on `Build` in the top menu and select `Build Solution`.
   - Start debugging by pressing `F5` or clicking the `Start` button in Visual Studio.
   - The application should launch, and you will see the **login screen**.

### Logging In

To log in as an administrator, use the following credentials:

- **Username**: `admin`
- **Password**: `admin123`

Once logged in, you can start managing calendars and meetings.

### User Roles

- **Admin**: Can manage all user accounts, approve or reject absence requests, and view all meetings.
- **Employee**: Can manage their own calendar, submit absence requests, and schedule meetings.

## Technologies Used

- **WPF (Windows Presentation Foundation)**: For building the user interface.
- **.NET Framework (C#)**: The programming framework used to develop the application.
- **MSSQL (Microsoft SQL Server)**: The relational database used for data storage and management.
- **SQL Server Management Studio (SSMS)**: Tool used for managing the database.
