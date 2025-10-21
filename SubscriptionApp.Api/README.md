# SubscriptionApp

### Overview
A simple and lightweight subscription management API built with **ASP.NET Core 8** following the principles of **Clean Architecture**.  
The project demonstrates clean separation of concerns, scalability, and testability in a modern .NET backend design.

### Features
- List available subscription plans  
- Activate a plan for a user  
- Deactivate a user's subscription  
- View all subscriptions for a specific user  

### Architecture
This project follows the **Clean Architecture** pattern:
- **Domain:** Core entities and business rules  
- **Application:** Interfaces and business logic contracts  
- **Infrastructure:** Data access layer using Entity Framework Core  
- **API:** Presentation layer exposing RESTful endpoints  

### Tech Stack
- ASP.NET Core 8 (Web API)
- Entity Framework Core (InMemory)
- Serilog for structured logging
- Swagger (OpenAPI) for documentation

### How to Run
```bash
dotnet run --project src/SubscriptionApp.Api
