# BlogAPI

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

1. Open `BlogAPI.sln` in Visual Studio
2. Restore NuGet packages
3. Update connection strings in `Web.config` (if using database)
4. Build and run the project

### API Endpoints

The following endpoints are available:

#### Author
- `GET /api/author` - Get all authors
- `GET /api/author/{id}` - Get specific author
- `POST /api/author` - Create new author
- `PUT /api/author/{id}` - Update author
- `DELETE /api/author/{id}` - Delete author

#### BlogPost
- `GET /api/blogpost` - Get all blogposts
- `GET /api/blogpost/{id}` - Get specific blogpost
- `POST /api/blogpost` - Create new blogpost
- `PUT /api/blogpost/{id}` - Update blogpost
- `DELETE /api/blogpost/{id}` - Delete blogpost

#### Comment
- `GET /api/comment` - Get all comments
- `GET /api/comment/{id}` - Get specific comment
- `POST /api/comment` - Create new comment
- `PUT /api/comment/{id}` - Update comment
- `DELETE /api/comment/{id}` - Delete comment

#### Tag
- `GET /api/tag` - Get all tags
- `GET /api/tag/{id}` - Get specific tag
- `POST /api/tag` - Create new tag
- `PUT /api/tag/{id}` - Update tag
- `DELETE /api/tag/{id}` - Delete tag

### Documentation

Swagger documentation is available at: `/swagger`

## Project Structure

```
BlogAPI/
├── src/BlogAPI/
│   ├── App_Start/          # Application configuration
│   ├── Controllers/        # Web API controllers
│   ├── Data/              # Entity Framework context and configurations
│   ├── Models/            # Data models
│   ├── Services/          # Business logic services
│   └── Properties/        # Assembly information
├── tests/BlogAPI.Tests/
│   ├── Controllers/       # Controller tests
│   └── Services/         # Service tests
└── BlogAPI.sln
```

## License

MIT License
