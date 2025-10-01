# InventoryAPI

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

1. Open `InventoryAPI.sln` in Visual Studio
2. Restore NuGet packages
3. Update connection strings in `Web.config` (if using database)
4. Build and run the project

### API Endpoints

The following endpoints are available:

#### Supplier
- `GET /api/supplier` - Get all suppliers
- `GET /api/supplier/{id}` - Get specific supplier
- `POST /api/supplier` - Create new supplier
- `PUT /api/supplier/{id}` - Update supplier
- `DELETE /api/supplier/{id}` - Delete supplier

#### Warehouse
- `GET /api/warehouse` - Get all warehouses
- `GET /api/warehouse/{id}` - Get specific warehouse
- `POST /api/warehouse` - Create new warehouse
- `PUT /api/warehouse/{id}` - Update warehouse
- `DELETE /api/warehouse/{id}` - Delete warehouse

#### Item
- `GET /api/item` - Get all items
- `GET /api/item/{id}` - Get specific item
- `POST /api/item` - Create new item
- `PUT /api/item/{id}` - Update item
- `DELETE /api/item/{id}` - Delete item

#### Stock
- `GET /api/stock` - Get all stocks
- `GET /api/stock/{id}` - Get specific stock
- `POST /api/stock` - Create new stock
- `PUT /api/stock/{id}` - Update stock
- `DELETE /api/stock/{id}` - Delete stock

### Documentation

Swagger documentation is available at: `/swagger`

## Project Structure

```
InventoryAPI/
├── src/InventoryAPI/
│   ├── App_Start/          # Application configuration
│   ├── Controllers/        # Web API controllers
│   ├── Data/              # Entity Framework context and configurations
│   ├── Models/            # Data models
│   ├── Services/          # Business logic services
│   └── Properties/        # Assembly information
├── tests/InventoryAPI.Tests/
│   ├── Controllers/       # Controller tests
│   └── Services/         # Service tests
└── InventoryAPI.sln
```

## License

MIT License
