# PATBMS - Patient Admission, Triage and Bed Management System

## Overview
The PATBMS is a web-based system designed to improve the efficiency 
of patient admission, triage, and bed management at Waitakere Hospital. 
Built with ASP.NET Core MVC, it supports five user roles with role-based 
access control, real-time bed availability tracking, and automated 
capacity notifications.

## Organisation
Waitakere Hospital, operating under Te Whatu Ora - Health New Zealand 
Waitemata.

## Prerequisites
- .NET 9 SDK
- Visual Studio Code or any C# compatible IDE

## Phase 1 - Console Application
The Phase 1 console application is located in the `PATBMS` folder.

### How to Run Phase 1
1. Clone the repository:
   git clone https://github.com/ExplosiveSalad/Data-Process-and-Software-Modelling.git
2. Navigate to the Phase 1 folder:
   cd PATBMS
3. Run the program:
   dotnet run

## Phase 2 - Web Application
The Phase 2 web application is located in the `PATBMS_Web` folder.

### How to Run Phase 2
1. Clone the repository:
   git clone https://github.com/ExplosiveSalad/Data-Process-and-Software-Modelling.git
2. Navigate to the Phase 2 folder:
   cd PATBMS_Web
3. Create and seed the database:
   dotnet ef database update
4. Run the application:
   dotnet run
5. Open your browser and navigate to:
   http://localhost:5114

### Demo Login Credentials
| Role           | Email                    | Password    |
|----------------|--------------------------|-------------|
| Doctor         | smith@waitakere.nz       | password123 |
| Doctor         | patel@waitakere.nz       | password123 |
| Nurse          | jane@waitakere.nz        | password123 |
| Nurse          | mark@waitakere.nz        | password123 |
| Admin          | bob@waitakere.nz         | password123 |
| Allied Health  | sara@waitakere.nz        | password123 |
| Management     | baker@waitakere.nz       | password123 |

## System Features

### Phase 1 - Console Application
- User login for five roles
- Patient registration and NHI record retrieval
- Patient triage with ATS category assignment (1-5)
- Bed allocation with occupancy tracking
- Patient discharge with automatic bed status update
- Inter-department handover
- Notification system
- Allied Health and Management demonstrations

### Phase 2 - Web Application
- Role-based login with session management
- Role-specific dashboards with quick actions
- Patient registration and NHI search
- Triage with ATS category validation (1-5)
- Bed dashboard with colour-coded ward grids
- Ward-based bed allocation using Strategy pattern
  - ATS 1-2: Titirangi Emergency Ward
  - ATS 3-4: Piha Surgical Ward
  - ATS 5: Te Henga General Medicine Ward
- Discharge with automatic bed status update
- Inter-department handover with structured form
- Per-user notification acknowledgement
- Diagnosis and treatment plan entry
- Capacity simulation for demo purposes

## Design Patterns Applied (Phase 2)
- **Factory** - UserFactory creates correct User subclass based on role
- **Singleton** - BedManager single instance manages all bed state
- **Observer** - Automatic capacity alerts to Nurse and Management
- **Strategy** - Ward-based bed allocation by ATS category

## Tech Stack (Phase 2)
- ASP.NET Core MVC (.NET 9)
- Entity Framework Core
- SQLite (local development)
- Razor Views
- Session-based authentication

## Author
Mohammed Yaacoub Abou Chlih
Student ID: 21138726
ENSE706 - Data Process and Software Modelling
Auckland University of Technology
31 May 2026