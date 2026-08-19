# CRN Product API

A RESTful backend API for managing Products, built using ASP.NET Core Web API and .NET 8.

## Overview

The CRN Product API provides CRUD operations for Products with JWT authentication, refresh tokens, validation, pagination, database persistence, exception handling, security headers, and automated unit tests.

The solution follows a layered architecture with separation of concerns.

## Technology Stack

- .NET 8
- C#
- ASP.NET Core Web API
- SQL Server
- Entity Framework Core
- JWT Authentication
- Refresh Tokens
- Swagger / OpenAPI
- FluentValidation
- xUnit
- Moq
- API Versioning
- Response Compression

## Architecture

The project follows a layered architecture:

```text
CRNProductAPI/
├── src/
│   ├── API/
│   │   ├── Controllers/
│   │   ├── Middleware/
│   │   ├── Properties/
│   │   ├── Program.cs
│   │   └── appsettings.json
│   │
│   ├── Application/
│   │   ├── DTOs/
│   │   ├── Interfaces/
│   │   ├── Services/
│   │   └── Validators/
│   │
│   ├── Domain/
│   │   └── Entities/
│   │
│   └── Infrastructure/
│       └── Data/
│           ├── Configurations/
│           ├── Migrations/
│           └── Repositories/
│
├── tests/
│   └── Application.Tests/
│
├── .gitignore
├── README.md
└── CRNProductAPI.slnx

Features
Product CRUD

The API supports:

Create Product
Get all Products
Get Product by ID
Update Product
Delete Product
Pagination

The Product collection endpoint supports pagination using:

pageNumber
pageSize

Example:

GET /api/v1/Product?pageNumber=1&pageSize=10
Authentication

The API uses JWT Bearer authentication.

Authentication includes:

Login
Access token generation
Refresh token generation
Refresh token persistence
Refresh token expiration
Validation

FluentValidation is used to validate incoming Product requests.

Exception Handling

A custom exception handling middleware catches unhandled exceptions and returns a consistent JSON response.

Example:

{
  "statusCode": 500,
  "message": "An unexpected error occurred."
}
Security Headers

Custom security middleware adds:

X-Content-Type-Options
X-Frame-Options
Referrer-Policy
Content-Security-Policy
Permissions-Policy
CORS

CORS is configured to allow requests from:

http://localhost:4200
Response Compression

Response compression is enabled to reduce response payload size and improve network performance.

Database Optimization

Entity Framework Core uses:

AsNoTracking() for read-only queries
Pagination
Database indexes
Async database operations

An index is configured for the Product CreatedOn column:

IX_Product_CreatedOn
API Endpoints
Authentication
Method	Endpoint	Description
POST	/api/v1/Auth/login	Authenticate user and generate tokens
POST	/api/v1/Auth/refresh	Generate a new access token using a refresh token
Products
Method	Endpoint	Description
GET	/api/v1/Product	Get paginated Products
GET	/api/v1/Product/{id}	Get Product by ID
POST	/api/v1/Product	Create a Product
PUT	/api/v1/Product/{id}	Update a Product
DELETE	/api/v1/Product/{id}	Delete a Product
Database

The application uses SQL Server with Entity Framework Core.

Main entities include:

Product
Item
RefreshToken

Entity Framework Core migrations are located in:

src/Infrastructure/Data/Migrations/
Configuration

The API uses appsettings.json for application configuration.

The application requires a SQL Server connection string and JWT configuration.

For production environments, sensitive configuration values should be supplied through secure environment-specific configuration or secret management.

Running the Application
Prerequisites

Install:

.NET 8 SDK
SQL Server / SQL Server Express
Git
Clone the Repository
git clone https://github.com/Ajay6029/CRNProductAPI.git
cd CRNProductAPI
Restore Dependencies
dotnet restore
Build
dotnet build
Run Tests
dotnet test
Run the API
dotnet run --project ./src/API/API.csproj
Swagger / OpenAPI

Swagger is enabled in the Development environment.

Once the API is running, open:

http://localhost:5000/swagger/index.html

Swagger provides interactive documentation and allows the API endpoints to be tested directly from the browser.

Testing

The project uses:

xUnit
Moq

The current application test suite contains 8 unit tests for Product service functionality.

All tests are currently passing.

Test summary: total: 8, failed: 0, succeeded: 8
Error Handling

Unhandled exceptions are processed by:

ExceptionHandlingMiddleware

This prevents internal exception details from being exposed to API consumers and provides a consistent error response.

Project Quality

The implementation focuses on:

Separation of concerns
Dependency Injection
Repository Pattern
Service Layer
DTO-based API contracts
Async/await
Database query optimization
Input validation
Authentication and authorization
Centralized exception handling
Security headers
API versioning
Automated testing
Swagger/OpenAPI documentation
Author

Ajay Teknur