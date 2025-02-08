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
     - eduardoeckert/signsafepresentation:v1
       

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
  1. You need to have the [DockerComposePlugin](https://docs.docker.com/compose/install) installed (Docker Desktop already includes Docker Compose).
     To check, run this command in your CLI:
     ```
     docker compose version
     ```
     The output should look like: `Docker Compose version vX.Y.Z`
    
  2. You need create a **valid certificate** to be able to access the https port.
     To create a valid certificate, run this command in your CLI:
<br><br/>
   (Windows)
     ```
     dotnet dev-certs https -ep "C:\Users\<your-user-name>\AppData\Roaming\ASP.NET\Https\signsafe.pfx"  -p signSafePassword
     ```
     * In the preceding commands, replace `<your-user-name>` with the user local host machine.
     
     * The path maybe will be different depending on the OS. For more info, click [here](https://learn.microsoft.com/en-us/aspnet/core/security/docker-compose-https?view=aspnetcore-9.0)
     
     * The password(after -p) must be `"signSafePassword"`

     After this, you will need trust the certificate
     Run this command in your CLI:
     ```
      dotnet dev-certs https --trust
     ```
      > ***!* Whithout this valid certificate, the command "docker compose up" will fail**

      
### Running:
This is a containerized application, so you just need to follow these simple steps:
1. Download the `docker-compose.yml` application file
   * Obs: If needed, you can modify the output port inside the file. Default output ports -> `http -> 8080` | `https -> 8081`
     
2. Open your CLI, go to the directory where the docker-compose.yml was downloaded and execute:
   ```
   docker compose up
   ```
   
3. Access the api by following this link -> [https://localhost/8081/swagger](https://localhost:8081/swagger/index.html)
   * Obs: If you have modified the output port on step 2, you will need to update the URL -> [https://localhost/[output-port]/swagger]
  
     That's it!
