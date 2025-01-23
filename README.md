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
