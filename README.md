## Todo application with ASP.NET Core Web API

This is a Todo application that follows Clean Architecture and features:

- [**Todo.Core**](ToDo.Core) - A C# class library that contains all entities and interfaces.
- [**Todo.Infra**](ToDo.Infra) - A C# class library that contains implementation repositiory classes and database logic.
- [**Todo.WebAPI**](ToDo.WebAPI) - An ASP.NET Core REST API backend

## Prerequisites

### .NET

1. [Install .NET 7](https://dotnet.microsoft.com/en-us/download)
2. [Install SQL Server Express Edition](https://www.microsoft.com/en-in/sql-server/sql-server-downloads)

### Database

1. Run the script "[db_mytodoapp.sql](db_mytodoapp.sql)" in SQL Server to create the DB and related tables.
2. Change the ConnectionString in "ToDo.WebAPI → appsetting.json" with your database.

![image](https://user-images.githubusercontent.com/3891454/219577199-8d64fdc4-cafc-49b2-a3c3-7afde6205449.png)

### Running the application

To run the application, run [Todo.WebAPI](ToDo.WebAPI) ASP.NET Core Web API application.
