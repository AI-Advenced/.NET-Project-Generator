# ECommerceAPI

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

1. Open `ECommerceAPI.sln` in Visual Studio
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

#### Category
- `GET /api/category` - Get all categorys
- `GET /api/category/{id}` - Get specific category
- `POST /api/category` - Create new category
- `PUT /api/category/{id}` - Update category
- `DELETE /api/category/{id}` - Delete category

#### Product
- `GET /api/product` - Get all products
- `GET /api/product/{id}` - Get specific product
- `POST /api/product` - Create new product
- `PUT /api/product/{id}` - Update product
- `DELETE /api/product/{id}` - Delete product

#### Order
- `GET /api/order` - Get all orders
- `GET /api/order/{id}` - Get specific order
- `POST /api/order` - Create new order
- `PUT /api/order/{id}` - Update order
- `DELETE /api/order/{id}` - Delete order

#### OrderItem
- `GET /api/orderitem` - Get all orderitems
- `GET /api/orderitem/{id}` - Get specific orderitem
- `POST /api/orderitem` - Create new orderitem
- `PUT /api/orderitem/{id}` - Update orderitem
- `DELETE /api/orderitem/{id}` - Delete orderitem

#### Review
- `GET /api/review` - Get all reviews
- `GET /api/review/{id}` - Get specific review
- `POST /api/review` - Create new review
- `PUT /api/review/{id}` - Update review
- `DELETE /api/review/{id}` - Delete review

### Documentation

Swagger documentation is available at: `/swagger`

## Project Structure

```
ECommerceAPI/
├── src/ECommerceAPI/
│   ├── App_Start/          # Application configuration
│   ├── Controllers/        # Web API controllers
│   ├── Data/              # Entity Framework context and configurations
│   ├── Models/            # Data models
│   ├── Services/          # Business logic services
│   └── Properties/        # Assembly information
├── tests/ECommerceAPI.Tests/
│   ├── Controllers/       # Controller tests
│   └── Services/         # Service tests
└── ECommerceAPI.sln
```

## License

MIT License
