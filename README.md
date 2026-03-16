# Student Management API

A secure and scalable **ASP.NET Core Web API** for managing student data.
This project demonstrates modern backend development practices including **JWT authentication, refresh tokens, role-based authorization, rate limiting, and layered architecture**.

The API allows administrators to manage students while enabling authenticated users to securely access their own data.

---

# Features

• JWT Authentication
• Secure Refresh Token mechanism
• Role-based Authorization (Admin / Student)
• Custom Authorization Policy (StudentOwnerOrAdmin)
• Rate Limiting for authentication endpoints
• Password hashing using BCrypt
• Entity Framework Core with SQL Server
• Repository Pattern + Service Layer
• DTO validation
• Swagger UI with JWT authentication support
• CORS configuration

---

# Architecture

The project follows 3 tier Architecture Designe

Controller Layer
Handles HTTP requests and responses.

Service Layer
Contains business logic.

Repository Layer
Handles database operations.

Data Layer
Contains Entities, DbContext, and EF Core configurations.

```
StudentApi
│
├── Authorization
│
├── Controllers
│   ├── AuthController
│   └── StudentApiController
│
├── DTOs
│
├── Business
│   ├── Interfaces
│   └── Services
│
├── Data
│   ├── Entities
│   ├── DBContext
│   ├── Configurations
│   ├── Interfaces
│   └── Repositories
│
└── Program.cs
```

---

# Technologies Used

• ASP.NET Core Web API
• C#
• Entity Framework Core
• SQL Server
• JWT Authentication
• BCrypt Password Hashing
• Swagger
• Rate Limiting Middleware

---

# Authentication Flow

1. User logs in using email and password.
2. The API validates credentials.
3. A **JWT Access Token** and **Refresh Token** are generated.
4. The access token is used to access protected endpoints.
5. When the access token expires, the refresh token can generate a new one.

---

# Authorization

The API supports:

Role-based authorization

Admin users can:

* Create students
* Update students
* Delete students
* View all students

Students can:

* Access their own profile

Custom Authorization Policy:

`StudentOwnerOrAdmin`

This ensures a student can only access their own data unless they are an admin.

---

# Security

Security measures implemented in this API include:

• BCrypt password hashing
• JWT token validation
• Refresh token hashing
• Rate limiting for login attempts
• Secure environment variable usage for secret keys

---

# Environment Configuration

The application requires a **JWT secret key** to be configured before running.

Set the following environment variable:

JWT_SECRET_KEY


Windows:

```
setx JWT_SECRET_KEY "your_secret_key_here"
```

Linux / Mac:

```
export JWT_SECRET_KEY="your_secret_key_here"
```

Restart your terminal or IDE after setting the variable.

---

# Running the Project

1. Clone the repository

```
git clone https://github.com/yourusername/student-api.git
```

2. Navigate to the project directory

```
cd student-api
```

3. Restore dependencies

```
dotnet restore
```

4. Run the API

```
dotnet run
```

---

# API Documentation

Swagger UI is enabled for testing the API.

After running the application, navigate to:

```
https://localhost:{port}/swagger
```

You can authenticate using JWT directly from Swagger.

---

# Example Endpoints

Authentication

POST /api/Auth/Login
POST /api/Auth/Refresh
POST /api/Auth/Logout

Students

GET /api/StudentApi/GetStudents
GET /api/StudentApi/{id}
POST /api/StudentApi/AddNewStudent
PUT /api/StudentApi/UpdateStudent/{id}
DELETE /api/StudentApi/DeleteStudent/{id}

---

# Learning Goals

This project was created to practice:

• Building secure REST APIs
• Implementing JWT authentication
• Designing layered architecture in ASP.NET Core
• Applying role-based authorization
• Working with Entity Framework Core

---


