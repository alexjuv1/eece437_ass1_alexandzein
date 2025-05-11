# 📘 UniversityApp — Clean Architecture (.NET)

A modular .NET application built using **Clean Architecture principles**, implementing layered separation of concerns with CQRS, MediatR, AutoMapper, EF Core, FluentValidation, and JWT authentication.

---

## 📂 Solution Structure

```
UniversityApp.sln
│
├── UniversityApp.Core           // Domain entities and interfaces
├── UniversityApp.Application    // Use cases, DTOs, handlers, validators
├── UniversityApp.Infrastructure // EF Core, persistence, dependency injection
└── UniversityApp.Web            // API controllers, views, program startup
```

---

## ✅ Features Implemented

### Domain Entities
- `Student`, `Professor`, `Course`, `Department`

### CRUD Operations
- Fully implemented using **CQRS (MediatR)**:
  - Commands: Create, Update, Delete
  - Queries: GetById, GetAll (with pagination)

### DTOs & Validation
- Clean DTO separation for input/output
- Synchronous and async **FluentValidation** rules

### Pagination
- `?pageNumber=1&pageSize=10` on all GET endpoints

### Authentication & Roles
- JWT Bearer Authentication
- Roles: `Admin`, `Student`
- Seeding: default `admin` and `stud` users

### AutoMapper
- Profiles defined for all entities

### Swagger Docs
- Swagger UI configured with JWT bearer support

### Error Handling
- Global JSON exception middleware

---

## 🛠 How to Run Locally

1. **Clone the Repo**
   ```bash
   git clone https://github.com/your-username/universityapp.git
   cd universityapp
   ```

2. **Update DB (if needed)**
   ```bash
   dotnet ef database update --project UniversityApp.Infrastructure
   ```

3. **Run the App**
   ```bash
   dotnet run --project UniversityApp.Web
   ```

4. **Open in Browser**
   ```
   https://localhost:5001/swagger
   ```

---

## 🔑 Test Users

| Role    | Username | Password |
|---------|----------|----------|
| Admin   | admin    | Admin1!  |
| Student | stud     | Stud1!   |

Use these in Swagger under **Authorize 🔐**.

---

## 💬 Technologies Used

- .NET 7 / 8
- Entity Framework Core
- MediatR (CQRS)
- AutoMapper
- FluentValidation
- ASP.NET Core Identity
- JWT Authentication
- SQLite (or replace with SQL Server)
- Swagger / Swashbuckle
- Serilog (optional)

---

## ✍️ Authors

- Zein & Alex — AUB EECE 437 – Assignment 4
