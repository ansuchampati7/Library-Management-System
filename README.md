# Library Management System

## Tech Stack
- .NET
- Entity Framework Core (Code First)
- SQL Server
- xUnit

## How to Run
1. Clone repository
2. Update connection string
3. Run:
   dotnet ef migrations add InitialCreate
   dotnet ef database update
4. Run project

## Run Tests
dotnet test

## code
please note for many places we used console.writeline insted of argument exception for better User experience
