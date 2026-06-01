# Employee Management System

A full-stack **Employee Management System** built with **.NET 9**, combining and **MVC Web Application** and a **REST API** under a single project. Built with clean separation of concerns using the **Repository Pattern**, **Service Layer**, and **Entity Framework Core**.

> **Status:** In active development - Controllers and Views in progress.

---

## Project Structure

'''
EmployeeManagement/
├── Controllers/                    # MVC + API Controllers (in progress)
├── Data/
│   └── AppDbContext.cs             # EF Core DbContext with Fluent API
├── DTOs/                           # Data Transfer Objects
│   ├── CreateEmployeeDto.cs
│   └── UpdateEmployeeDto.cs
├── Interfaces/                     # Service + Repository contracts
│   ├── IEmployeeRepository.cs
│   ├── IDepartmentRepository.cs
│   ├── IEmployeeService.cs
│   └── IDepartmentService.cs
├── Migrations/                     # EF Core database migrations
├── Models/                         # Domain entities
│   ├── Employee.cs
│   └── Department.cs
├── Repositories/                   # Data access layer
│   ├── EmployeeRepository.cs
│   └── DepartmentRepository.cs
├── Services/                       # Business logic layer
│   ├── EmployeeService.cs
│   └── DepartmentService.cs
├── ViewModels/                     # MVC ViewModels (in progress)
├── Views/                          # Razor Views (in progress)
├── wwwroot/                        # Static files
└── Program.cs                      # Entry point + DI registration
```

---
 
## Features

- ✔ Full **CRUD** for Employees and Departments
- ✔ **MVC Web UI** with Razor Views
- ✔ **REST API** endpoints returning JSON
- ✔ **Repository Pattern** — clean data access layer
- ✔ **Service Layer** — all business logic and validation
- ✔ **Department → Employee** one-to-many relationship
- ✔ **Self-referencing Manager** relationship (Employee → Employee)
- ✔ Auto-generated `EmployeeID` format (e.g. `EMP0001`)
- ✔ Email uniqueness validation across employees
- ✔ EF Core **Fluent API** — column types, indexes, constraints
- ✔ **Async/await** throughout all layers
- ✔ Dependency Injection registered in `Program.cs`
---
 
## 🗄️ Data Models
 
### Employee
 
| Field        | Type           | Notes                              |
|--------------|----------------|------------------------------------|
| Id           | int            | Primary key, auto-increment        |
| EmployeeID   | varchar(8)     | Auto-generated e.g. `EMP0001`      |
| Name         | nvarchar(200)  | Required                           |
| Email        | nvarchar(256)  | Required, Unique                   |
| Dob          | DateTime       | Required                           |
| Role         | nvarchar       | Required                           |
| Salary       | decimal(18,2)  | Required                           |
| DepartmentID | int            | Foreign key → Departments          |
| ManagerID    | int?           | Self-ref FK → Employees (nullable) |
| CreatedAt    | DateTime       | Auto-filled by database            |
| UpdatedAt    | DateTime?      | Nullable, set on update            |
 
### Department
 
| Field          | Type          | Notes                   |
|----------------|---------------|-------------------------|
| Id             | int           | Primary key, auto-increment |
| DepartmentId   | varchar(8)    | Unique                  |
| DepartmentName | nvarchar(200) | Required, Unique        |
| CreatedAt      | DateTime      | Auto-filled by database |
| UpdatedAt      | DateTime?     | Nullable                |
 
---
 
##  Relationships
 
```
Department  ──<  Employee    One department has many employees
Employee    ──<  Employee    Self-referencing — a manager has many subordinates
```
 
---
 
##  Architecture
 
```
Browser / API Client
         ↓
Controllers  (MVC + API)
         ↓
IEmployeeService / IDepartmentService   (interfaces)
         ↓
EmployeeService / DepartmentService     (business logic + validation)
         ↓
IEmployeeRepository / IDepartmentRepository   (interfaces)
         ↓
EmployeeRepository / DepartmentRepository     (EF Core queries)
         ↓
AppDbContext
         ↓
SQL Server
```
 
---
 
##  Business Rules
 
- Employee **email must be unique** — validated before create and update
- `EmployeeID` is **auto-generated** after insert (`EMP0001`, `EMP0002` ...)
- Department must **exist** before being assigned to an employee
- Deleting a department that has employees is **blocked** (`OnDelete: Restrict`)
- `ManagerID` is **optional** — top-level employees have no manager
- `CreatedAt` is **auto-filled** by the database on insert
- `UpdatedAt` is **set in code** on every update
---
 
##  Database Configuration (Fluent API highlights)
 
```csharp
// Auto-generated EmployeeID after insert
saved.EmployeeID = $"EMP{saved.Id:D4}";
 
// Unique indexes
entity.HasIndex(e => e.EmployeeID).IsUnique();
entity.HasIndex(e => e.Email).IsUnique();
 
// Department relationship
entity.HasOne(e => e.Department)
      .WithMany(d => d.Employees)
      .HasForeignKey(e => e.DepartmentID)
      .OnDelete(DeleteBehavior.Restrict);
 
// Self-referencing Manager relationship
entity.HasOne(e => e.Manager)
      .WithMany(e => e.Subordinates)
      .HasForeignKey(e => e.ManagerID)
      .OnDelete(DeleteBehavior.Restrict)
      .IsRequired(false);
```
 
---
 
##  Getting Started
 
### Prerequisites
 
- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- SQL Server (local or Express)
- Visual Studio 2022 or VS Code
  
### 1. Clone the Repository
 
```bash
git clone https://github.com/yathaviR/EmployeeManagement.git
cd EmployeeManagement
```
 
### 2. Configure Connection String
 
Create `appsettings.json` in the project root (not tracked by Git):
 
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=EmployeeDB;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```
 
### 3. Apply Migrations
 
```bash
dotnet ef database update
```
 
### 4. Run the Project
 
```bash
dotnet run
```
 
Open browser → `https://localhost:5001`
 
---
 
##  API Endpoints
 
| Method | Endpoint               | Description            |
|--------|------------------------|------------------------|
| GET    | `/api/employees`       | Get all employees      |
| GET    | `/api/employees/{id}`  | Get employee by ID     |
| POST   | `/api/employees`       | Create new employee    |
| PUT    | `/api/employees/{id}`  | Update employee        |
| DELETE | `/api/employees/{id}`  | Delete employee        |
| GET    | `/api/departments`     | Get all departments    |
| GET    | `/api/departments/{id}`| Get department by ID   |
 
---
 
## 🌐 MVC Pages
 
| URL                        | Description              |
|----------------------------|--------------------------|
| `/Employee`                | List all employees       |
| `/Employee/Details/{id}`   | View employee details    |
| `/Employee/Create`         | Create new employee      |
| `/Employee/Edit/{id}`      | Edit employee            |
| `/Department`              | List all departments     |
| `/Department/Create`       | Create new department    |
| `/Department/Edit/{id}`    | Edit department          |
 
---
 
## 🛠️ Tech Stack
 
| Layer              | Technology                    |
|--------------------|-------------------------------|
| Framework          | .NET 9 / ASP.NET Core         |
| Web UI             | Razor Views (MVC)             |
| API                | ASP.NET Core Web API          |
| ORM                | Entity Framework Core 9       |
| Database           | SQL Server                    |
| Architecture       | Repository + Service Pattern  |
| Dependency Injection | Built-in .NET DI            |
| Language           | C# 13                         |
 
---
 
##  Key Design Decisions
 
**Single project structure** — all layers (Interfaces, Services, Repositories) organised as folders within one `.csproj`. Chosen for simplicity as a solo developer, with a clear path to split into multiple projects if the team or codebase grows.
 
**Interface-driven design** — every service and repository has a corresponding interface, enabling easy unit testing and future swapping of implementations.
 
**No direct DbContext in controllers** — controllers only depend on service interfaces, keeping them thin and testable.
 
---
 
##  Roadmap
 
- [x] Domain models (Employee, Department)
- [x] EF Core DbContext with Fluent API
- [x] Migrations and database setup
- [x] Repository interfaces and implementations
- [x] Service interfaces and implementations
- [x] DI registration in Program.cs
- [ ] MVC Controllers and Views
- [ ] API Controllers
- [ ] Form validation
- [ ] Authentication and authorisation
- [ ] Unit tests
---
 
*Built as a portfolio project demonstrating .NET 9 full-stack development with clean architecture principles.*
