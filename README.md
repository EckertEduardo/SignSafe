
# SignSafe

![SignSafe Banner](https://github.com/user-attachments/assets/4b63c9cb-ef14-412a-9e16-354d0a10e3b9)

[![License: MIT](https://img.shields.io/badge/license-MIT-green)](https://github.com/EduardoEckert/SignSafe/blob/develop/LICENSE)

> **Status**: 🛠️ In Development

**SignSafe** is a secure web application for user management. It allows users to register, log in, update profiles, and delete accounts while ensuring security with features like email verification and OTP-based password reset.

---

## ✨ Features

- **Secure Login / Signup**: Users can create accounts and securely log in using industry-standard practices.
- **Email Verification via OTP**: Ensures that users verify their email addresses before proceeding with account setup.
- **Password Reset via OTP**: Forgot your password? Securely reset it with an OTP sent to your email.

### 🛠 Coming Soon

-  Role-based access control
-  User listing with search/filter
-  User deletion
---
## UI Preview
### Home Page
![image](https://github.com/user-attachments/assets/a6bc16cb-3205-40d4-8451-f71687dfba8a)

### Create your account
![image](https://github.com/user-attachments/assets/17960e17-7134-453d-903d-f2351631635c)

### Validate your email with an OTP code
![image](https://github.com/user-attachments/assets/63a0e2bc-1632-4ab3-9b4c-22949f68a2ba)

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
<details>
<summary>Running With HTTP (Recommended)</summary>

#### Prerequisites

1. Install [Docker Desktop](https://www.docker.com/get-started)
    ```bash
    docker --version
    ```

#### Running the Application
1. [Download the DockerCompose file](https://github.com/EckertEduardo/SignSafe-Backend/releases/download/runsetup/RunSetup.rar)
##
</details>

<details>
<summary>Running With HTTPS (Optional)</summary>

#### Prerequisites
1. Install [Docker Desktop](https://www.docker.com/get-started)
    ```bash
    docker --version
    ```
    
2. Install [.NET SDK](https://dotnet.microsoft.com/en-us/download)

    ```bash
    dotnet --version
    ```

3. [Download the RunSetup folder](https://github.com/EckertEduardo/SignSafe-Backend/releases/download/runsetup/RunSetup.rar)

#### Running the Application
1. Execute the setup script:

<details>
<summary><strong>Windows</strong></summary>

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

> ❌ This script may not work correctly on Linux or Windows, depending on the distribution. If you encounter any certificate-related issues, it's recommended to run the application without HTTPS by following the earlier instructions.

</details>

---

### 🔗 Access the Application
<details>
  <summary>Backend API (HTTPS - Swagger UI): (OPTIONAL)</summary>

  [https://localhost:8081/swagger](https://localhost:8081/swagger/index.html)
</details>
  
- **Backend API (HTTP - Swagger UI):**  
  [http://localhost:8080/swagger](http://localhost:8080/swagger/index.html)

- **Frontend Application:**  
  [http://localhost:9090](http://localhost:9090)

- **SEQ Logging Dashboard:**  
  [http://localhost:8082](http://localhost:8082)

> 💡 *If you change any service port in the docker-compose file, update the URLs accordingly.*
