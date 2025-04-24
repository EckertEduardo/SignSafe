
# SignSafe

![SignSafe Banner](https://github.com/user-attachments/assets/4b63c9cb-ef14-412a-9e16-354d0a10e3b9)

[![License: MIT](https://img.shields.io/badge/license-MIT-green)](https://github.com/EduardoEckert/SignSafe/blob/develop/LICENSE)

> **Status**: 🛠️ In Development

**SignSafe** is a secure web application designed for managing users — enabling registration, login, user updates, and deletion. It provides secure authentication mechanisms, user role attribution, and detailed logging via SEQ.

---

## ✨ Features

-  Secure Login / Signup
-  Email Verification via OTP
-  Password Reset via OTP

### 🛠 Coming Soon

-  Role-based access control
-  View all users
-  Delete users

---

## Architecture

- [Onion Architecture](https://codewithmukesh.com/blog/onion-architecture-in-aspnet-core/)
- [CQRS (Command Query Responsibility Segregation)](https://learn.microsoft.com/en-us/azure/architecture/patterns/cqrs)

## Project Patterns

- [Repository & Unit of Work](https://learn.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application)

## Principles

- [SOLID](https://medium.com/@lucas.and227/the-solid-principles-in-c-319755838805)
- Domain-Driven Design (DDD)
- [IoC, DI, and DIP](https://balta.io/blog/inversion-of-control)

## Tooling

-  JWT Bearer Authentication
-  Password hashing with ASP.NET Identity
- Logging with [Serilog](https://serilog.net/)
- Observability with [SEQ](https://datalust.co/seq)
- Containerized with Docker

---

## 🧪 Testing

- Framework: xUnit
- Data Generation: [Bogus Faker](https://github.com/bchavez/Bogus)
- Assertions: [FluentAssertions](https://fluentassertions.com)

### Testing Principles

- [**FIRST**](https://medium.com/@tasdikrahman/f-i-r-s-t-principles-of-testing-1a497acda8d6):
  - **F**ast
  - **I**solated
  - **R**epeatable
  - **S**elf-validating
  - **T**horough

- [**AAA Pattern**](https://medium.com/@pjbgf/title-testing-code-ocd-and-the-aaa-pattern-df453975ab80) (Arrange–Act–Assert)

---

## Communication

- RESTful APIs

---

## 🐳 Dockerized Environment

Images used:

- `mcr.microsoft.com/mssql/server:2019-latest`
- `mcr.microsoft.com/dotnet/sdk:8.0`
- `datalust/seq:latest`
- `eduardoeckert/sign-safe-backend:latest`
- `eduardoeckert/sign-safe-frontend:latest`

---

## 📊 Observability

SEQ helps collect, search, and visualize structured logs:

> **Access SEQ after running Docker Compose:**  
> 🔗 [http://localhost:8082](http://localhost:8082)

---

## 💻 Technologies Used

| Framework         | ORM      | Database                |
|-------------------|----------|--------------------------|
| .NET 8.0          | EF Core  | Microsoft SQL Server 2019 (v15.0.2000.5) |

---

# 🚀 Getting Started

## Prerequisites

1. Install [Docker Desktop](https://www.docker.com/get-started)
    ```bash
    docker --version
    ```
2. Install [.NET SDK](https://dotnet.microsoft.com/en-us/download)
    ```bash
    dotnet --version
    ```

---

## Running the Application

1. [Download the RunSetup folder](https://github.com/EckertEduardo/SignSafe/releases/download/runsetup/RunSetup.zip)

2. Execute the setup script:

<details>
<summary>Windows</summary>

```bash
powershell -ExecutionPolicy Bypass -File setup.ps1
```
</details>

<details>
<summary>MacOS / Linux</summary>

```bash
chmod +x setup.sh
./setup.sh
```
</details>

> 📝 This script will generate SSL certificates and start the Docker containers.

---

### 🔗 Access the Application

- **Backend API (Swagger)**: [https://localhost:8081/swagger](https://localhost:8081/swagger/index.html)  
- **Frontend**: [http://localhost:9090](http://localhost:9090)  
- **SEQ Logs**: [http://localhost:8082](http://localhost:8082)

> 💡 *If you change any service port in the docker-compose file, update the URLs accordingly.*
