# TaskManager-SanmolAssignment


Task & Customer Manager – ASP.NET Core MVC Application
This is a lightweight ASP.NET Core MVC application developed as part of an API Developer Assignment. It helps small businesses manage customers, tasks, and basic operations through a user-friendly admin panel.
The application includes customer management, task tracking, filtering, dashboard analytics, and reminder features with a clean UI built using SB Admin 2 Bootstrap template.


🚀 Technologies Used

•	ASP.NET Core MVC (.NET 8.0)

•	Entity Framework Core (Code First)

•	SQL Server

•	Bootstrap 4.6 (SB Admin 2 Template)

•	jQuery & AJAX

•	EPPlus (Excel Export)

•	Hangfire (Background Email Scheduler)



🔐 Authentication

•	Simple login screen with hardcoded admin credentials

•	No database-based user/role management

•	Used only to restrict access to internal admin features
Default Admin Login:

📧 UserId: admin

🔒 Password: 1234



📁 Features Overview



👥 Customer Management

•	View all customers in a searchable table

•	Columns: Name, Phone, Email, Total Tasks

•	Add/Edit customer with:

•	- Validations for required fields (Name, Phone, Email)

•	- Unique Email and Phone validation on the server side

•	Soft Delete with SweetAlert confirmation dialog

•	Redirect to the last visited page after Add/Edit (pagination-aware)



✅ Task Management

•	View and manage all tasks from a centralized task list

•	Filter tasks by: Pending (default), Completed, or All

•	Columns: Description, Customer (link), Due Date, Status (color-coded), Priority

•	Sort by Due Date – ascending or descending toggle

•	Select page size (e.g., 5, 10, 20 per page) dynamically

•	Display total number of tasks matching the current filter and search

•	Mark as Complete: AJAX-based button to update task status without page reload

•	Send Email button: Sends overdue task reminders via background service

•	Add/Edit Task Form: Fields: Description (required), Due Date, Status (Pending/Completed), Priority (High/Medium/Low), Customer (dropdown)



📊 Dashboard

•	Visual overview of:

•	- Total Customers

•	- Total Tasks

•	- Completed Tasks

•	- Pending Tasks

•	- Overdue Tasks

•	Recent Activity Table – shows latest 5 task updates

•	Cards auto-update using database queries



✉️ Email Reminder (Overdue Tasks)

•	Sends automated email reminders to customers for their overdue tasks

•	Implemented using Hangfire for background scheduling and background job execution

•	Manual Email Send: A "Send Email" button is available on the Task Index page to trigger reminders on demand

•	Scheduled Execution: Configured to run periodically in the background via the Hangfire Dashboard

•	Email Content: Email body includes task details such as description, due date, and customer name

•	Custom HTML email template is used for a clean and professional appearance



📄 Excel Export

•	Export Tasks and Customers as Excel files

•	EPPlus used for Excel formatting

•	Export includes filters and formatting



🧩 Bonus Features

•	Responsive mobile/tablet friendly layout

•	Fallback delete confirmation view (no-JS compatibility)



🏗️ Project Structure

•	SanmolTaskManager_Models → Contains all model classes shared across the application

•	SanmolTaskManager_DAL → Contains DbContext and Generic Repository pattern implementation using Entity Framework Core

•	SanmolTaskManager_BLL → Business Logic Layer with services like CustomerService, TaskService, and EmailService

•	SanmolTaskManager_Web → ASP.NET Core MVC web project containing Controllers, Views, UI, and front-end logic, custom app-utils.js , templates



⚙️ Prerequisites

•	.NET 8.0 SDK

•	Visual Studio 2022 or later

•	SQL Server Management Studio (SSMS) 18+



🛠️ Setup Instructions

1. Clone or Download the Repository
   git clone https://github.com/sagar7898/TaskManager-SanmolAssignment.git
   
2. Open the Solution in Visual Studio 2022 or newer
   
3. Update the connection string in appsettings.json:
   "ConnectionStrings": { "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=SanmolTaskManagerDb;Trusted_Connection=True;TrustServerCertificate=True" }
   If using SQL Authentication:
   "Server=YOUR_SERVER_NAME;Database=SanmolTaskManagerDb;User Id=your_Id;Password=your_password;"

4. Press F5 or Ctrl+F5 to run the project

   
   
🧪 Database Setup

•	Create a new database named SanmolTaskManagerDb in SQL Server

•	Ensure SQL Server is running

•	Execute the provided SQL Script to set up the database schema and data



🙋‍♂️ Contact

For questions or improvements, contact:

Sagar Nachankar – sagarnachankar77@gmail.com

