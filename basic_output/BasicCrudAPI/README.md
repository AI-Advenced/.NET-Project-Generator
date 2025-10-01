# BasicCrudAPI

A .NET Framework 4.8 Web API project generated automatically.

## Features

- ASP.NET Web API 2
- Entity Framework 6 (if database enabled)
- RESTful API endpoints
- Swagger documentation (if enabled)
- CORS support (if enabled)
- Unit tests with MSTest

## Getting Started

### Prerequisites

- Visual Studio 2015 or later
- .NET Framework 4.8
- SQL Server (if using database)

### Setup

1. Open `BasicCrudAPI.sln` in Visual Studio
2. Restore NuGet packages
3. Update connection strings in `Web.config` (if using database)
4. Build and run the project

### API Endpoints

The following endpoints are available:

#### User
- `GET /api/user` - Get all users
- `GET /api/user/{id}` - Get specific user
- `POST /api/user` - Create new user
- `PUT /api/user/{id}` - Update user
- `DELETE /api/user/{id}` - Delete user

### Documentation

Swagger documentation is available at: `/swagger`

## Project Structure

```
BasicCrudAPI/
├── src/BasicCrudAPI/
│   ├── App_Start/          # Application configuration
│   ├── Controllers/        # Web API controllers
│   ├── Data/              # Entity Framework context and configurations
│   ├── Models/            # Data models
│   ├── Services/          # Business logic services
│   └── Properties/        # Assembly information
├── tests/BasicCrudAPI.Tests/
│   ├── Controllers/       # Controller tests
│   └── Services/         # Service tests
└── BasicCrudAPI.sln
```

## License

MIT License
