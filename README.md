# SignSafe
![SignSafe](https://github.com/EduardoEckert/SignSafe/assets/89213922/6633e139-4cf6-41ff-ba04-4bc983607b3f)

[![NPM](https://img.shields.io/badge/license-MIT-green)](https://github.com/EduardoEckert/SignSafe/blob/develop/LICENSE)

> Status : Developing

## Web application, responsible for create, update, visualize and delete users. You be able to log in securely, management users, and attribute roles for each one.

### Architetural pattern
* [Onion Architecture](https://codewithmukesh.com/blog/onion-architecture-in-aspnet-core/)
* [CQRS](https://learn.microsoft.com/pt-br/azure/architecture/patterns/cqrs)

### Project pattern
* [Repository](https://learn.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application) and [Unit of Work](https://learn.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application)

### Principles
* [SOLID](https://medium.com/@lucas.and227/the-solid-principles-in-c-319755838805#:~:text=The%20SOLID%20principles%20%E2%80%94%20Single%20Responsibility%20Principle%2C%20Open%2FClosed%20Principle,maintainable%2C%20and%20extensible%20C%23%20code.) 
* DDD
* [IoC, DI e DIP](https://balta.io/blog/inversion-of-control)

### Features
* Authentication/Authorization with JWT Bearer Token
* Pasword hashing with AspNet Identity
* Logging with Serilog
* Obserbaility with SEQ
* Containerization with Docker 
---
### Unit tests
Framework: Xunit
* Create large amounts of data in the quick and simple way for testing with **Bogus Faker**
* Improve the test legibility with **Fluent Assertions**

* [FIRST principle](https://medium.com/@tasdikrahman/f-i-r-s-t-principles-of-testing-1a497acda8d6) ->
**F**.ast
**I**.solated
**R**.epeatable
**S**.elf-validation
**T**-horough

* [AAA Pattern](https://medium.com/@pjbgf/title-testing-code-ocd-and-the-aaa-pattern-df453975ab80)

### Communication
* Api Rest

### Containerization
* Docker
  - Images : 
     - mcr.microsoft.com/mssql/server:2019-latest
     - mcr.microsoft.com/dotnet/sdk:8.0
     - datalust/seq:latest
     - eduardoeckert/signsafe.backend:latest
     - eduardoeckert/signsafe.frontend:latest
       

### Logs
Effective logging using [Serilog](https://serilog.net/) helps with debugging, performance monitoring, and security.

### Observability
[SEQ](https://datalust.co/seq) is an observability tool, specifically focused on log aggregation and analysis. It helps developers and operations teams collect, search, and visualize structured logs from applications, making it easier to detect and diagnose issues.
After you 'up' the docker compose file, the Seq will be available by following this link -> [http://localhost:8082](http://localhost:8082)

### Technologies Used 
<table> 
<tr>
 
 <td>.Net</td>
 <td>EfCore</td>
 <td>Microsoft Sql Server 2019</td>
 
</tr>
<tr>
 
 <td>8.0</td>
 <td>8.0</td>
 <td>15.0.2000.5</td>
 
</tr>
</table>

---
# How to run this application?

### Prerequisites:
  1. You need to have the [DockerDesktop](https://www.docker.com/get-started) installed.
     To check, run these commands in your CLI:
     ```
     docker compose version
     ```
     The output should look like: `Docker Compose version vX.Y.Z`
     ```
     docker --version
     ```
     The output should look like: `Docker version X.Y.Z, build abcdefg`
     
     
      
### Running:
This is a containerized application, so you just need to follow these simple steps:
1. Download the run setup folder [RunSetup](https://github.com/EckertEduardo/SignSafe/tree/master/RunSetup)
* **Windows** - Open the folder and execute the file **setup.ps1** with your cli or open your CLI, go to the directory where the folder was downloaded and execute:
     ```
     powershell -ExecutionPolicy Bypass -File setup.ps1
     ```
* **MacOs/Linux** - Open the folder and execute the file **setup.sh** with your cli or open your CLI, go to the directory where the folder was downloaded and execute:
     ```
     chmod +x setup.sh
     ```
* Obs: This setup will create a certificate for ssl connection and execute the docker-compose file
   
2. Access the **backend** api by following this link -> [https://localhost/8081/swagger](https://localhost:8081/swagger/index.html)
3. Access the **frontend** by following this link -> [http://localhost/9090](http://localhost:9090)
4. Access the **Seq logs/dashboards** by following this link -> [http://localhost/8082](http://localhost/8082)

* Obs: If you have modified the output port of any service inside the docker-compose, you will need to update these URL's
  
     That's it!
