# Library Management System

## Tech Stack
- .NET 8
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