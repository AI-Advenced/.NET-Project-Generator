# 🚀 Enhanced .NET Project Generator

A complete and advanced generator for **.NET Web API projects** compatible with **Visual Studio 2015 and later**.
This generator creates fully functional projects with a complete architecture, including **Models, Services, Data Layer, and Controllers with CRUD operations**.

---

## ✨ Key Features

### 🏗️ Full Architecture

* **Models**: Entity classes with Data Annotations
* **Services**: Business logic layer with interfaces and implementations
* **Data Layer**: Entity Framework 6 context with entity configurations
* **Controllers**: Web API controllers with full CRUD operations
* **Unit Tests**: Test projects using MSTest

### 🔧 Supported Technologies

* **.NET Framework 4.8** – Compatible with Visual Studio 2015+
* **ASP.NET Web API 2** – Modern REST framework
* **Entity Framework 6** – ORM with Code First
* **Swagger/OpenAPI** – Automatic API documentation
* **CORS** – Multi-domain support
* **MSTest** – Built-in unit testing

### 🗄️ Supported Databases

* **SQL Server** (LocalDB, SQL Express, SQL Server)
* **SQLite** – Embedded database
* **MySQL** – Open-source database
* **PostgreSQL** – Advanced database

---

## 🚀 Quick Start

### Install Dependencies

```bash
pip install inquirer rich
```

### Run Interactive CLI

```bash
python dotnet_generator_cli.py
```

### Programmatic Usage

```python
from enhanced_dotnet_generator import *

# Project configuration
config = ProjectConfig(
    project_name="MyAPI",
    project_type=ProjectType.WEBAPI,
    output_path="./output",
    include_database=True,
    database_provider=DatabaseProvider.SQLSERVER,
    entities=[
        EntityConfig("User", [
            PropertyConfig("Id", "int", is_required=True, is_key=True),
            PropertyConfig("Email", "string", is_required=True, max_length=100),
            PropertyConfig("Name", "string", is_required=True, max_length=50)
        ])
    ],
    include_swagger=True,
    include_cors=True,
    include_tests=True
)

# Generate the project
generator = EnhancedDotNetGenerator()
result = generator.generate_project(config)

if result['success']:
    print(f"✅ Project generated at: {result['project_path']}")
else:
    print(f"❌ Error: {result['error']}")
```

### Generate with Predefined Templates

```bash
python generate_example.py
```

Generates 3 complete sample projects:

* **E-commerce API** (User, Category, Product, Order, OrderItem, Review)
* **Blog API** (Author, BlogPost, Comment, Tag)
* **Inventory API** (Supplier, Warehouse, Item, Stock)

---

## 📂 Project Structure

```
MyProject/
├── src/MyProject/
│   ├── App_Start/
│   │   ├── WebApiConfig.cs
│   │   ├── FilterConfig.cs
│   │   ├── RouteConfig.cs
│   │   └── SwaggerConfig.cs
│   ├── Controllers/
│   │   ├── UserController.cs
│   │   └── ProductController.cs
│   ├── Data/
│   │   ├── MyProjectContext.cs
│   │   └── Configurations/
│   │       ├── UserConfiguration.cs
│   │       └── ProductConfiguration.cs
│   ├── Models/
│   │   ├── User.cs
│   │   └── Product.cs
│   ├── Services/
│   │   ├── IUserService.cs
│   │   ├── UserService.cs
│   │   ├── IProductService.cs
│   │   └── ProductService.cs
│   ├── Properties/
│   │   └── AssemblyInfo.cs
│   ├── MyProject.csproj
│   ├── Global.asax
│   ├── Global.asax.cs
│   ├── Web.config
│   └── packages.config
├── tests/MyProject.Tests/
│   ├── Controllers/
│   ├── Services/
│   ├── Properties/
│   ├── MyProject.Tests.csproj
│   └── packages.config
├── MyProject.sln
├── .gitignore
└── README.md
```

---

## 🎯 Generated Project Features

### 🌐 Full REST API

For each entity:

* `GET /api/entity` – List all
* `GET /api/entity/{id}` – Get by ID
* `POST /api/entity` – Create new
* `PUT /api/entity/{id}` – Update existing
* `DELETE /api/entity/{id}` – Delete

### 📊 Automatic Swagger Documentation

* Interactive web UI for testing APIs
* Auto-generated model & parameter documentation
* Available at `/swagger`

### 🔐 Built-in CORS Support

* Preconfigured for cross-origin requests
* Flexible config for dev & production

### 🧪 Integrated Unit Tests

* Tests for all services & controllers
* Ready-to-use MSTest project
* Auto-generated mocks

### 🗄️ Entity Framework 6 Setup

* Code First with migrations
* Separate entity configurations
* Relationship support
* Performance optimizations

---

## ⚙️ Advanced Configuration

### Supported Property Types

```python
PropertyConfig(
    name="MyProperty",
    type="string",        # string, int, decimal, DateTime, bool, etc.
    is_required=True,
    is_key=False,
    max_length=100,
    foreign_table="User"  # Foreign key
)
```

### Database Providers

```python
# SQL Server
DatabaseProvider.SQLSERVER
"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MyDb;Integrated Security=True"

# SQLite
DatabaseProvider.SQLITE
"Data Source=MyApp.db"

# MySQL
DatabaseProvider.MYSQL
"Server=localhost;Database=MyDb;Uid=user;Pwd=password;"

# PostgreSQL
DatabaseProvider.POSTGRESQL
"Host=localhost;Database=MyDb;Username=user;Password=password"
```

### Project Options

```python
ProjectConfig(
    project_name="MyAPI",
    project_type=ProjectType.WEBAPI,   # WEBAPI, MVC, CONSOLE
    target_framework="net48",
    output_path="./output",
    include_database=True,
    include_swagger=True,
    include_cors=True,
    include_authentication=False,      # JWT support
    include_tests=True
)
```

---

## 🔧 Using with Visual Studio

1. **Open the Project**

   * VS 2015+ → Open → Project/Solution → select `.sln`

2. **Restore NuGet Packages**

   * Right-click solution → *Restore NuGet Packages*
   * Or run `Update-Package -reinstall`

3. **Configure Database** (if enabled)

   * Update `Web.config` connectionStrings
   * Run:

     ```
     Enable-Migrations
     Update-Database
     ```

4. **Build & Run**

   ```
   Build → Build Solution (Ctrl+Shift+B)
   Debug → Start Debugging (F5)
   ```

5. **Access API**

   * App: `http://localhost:[port]/`
   * Swagger: `http://localhost:[port]/swagger`
   * API: `http://localhost:[port]/api/[entity]`

---

## 📋 Available Templates

* **🛒 E-commerce API** – Users, Categories, Products, Orders, OrderItems, Reviews
* **📝 Blog API** – Authors, BlogPosts, Comments, Tags
* **📦 Inventory API** – Suppliers, Warehouses, Items, Stock

---

## 🧪 Tests & Validation

### Run Tests

```bash
# Visual Studio
Test → Run All Tests

# Command line
MSTest.exe /testcontainer:MyProject.Tests.dll

# .NET Core (if installed)
dotnet test
```

### Example Test

```csharp
[TestClass]
public class UserServiceTests
{
    [TestMethod]
    public void GetAll_ReturnsAllUsers() { }
    
    [TestMethod]  
    public void GetById_ReturnsCorrectUser() { }
    
    [TestMethod]
    public void Create_AddsUserSuccessfully() { }
    
    [TestMethod]
    public void Delete_RemovesUserSuccessfully() { }
}
```

---

## 🔍 Troubleshooting

**1. Package not found**
→ Restore NuGet packages (`Update-Package -reinstall`)

**2. Database connection error**
→ Check `Web.config` connectionStrings & ensure SQL LocalDB is installed

**3. Swagger load error**
→ Verify Swashbuckle references, clean & rebuild solution

**4. CORS issues in production**
→ Update allowed origins in `WebApiConfig.cs`

---

## 🖥️ System Requirements

* Visual Studio 2015+
* .NET Framework 4.8
* SQL Server / SQLite / MySQL / PostgreSQL
* Python 3.7+ (for generator)

---

## 🤝 Contribution

1. Fork the repo
2. Create a feature branch
3. Commit changes
4. Push and open PR

---

## 📄 License

MIT License – see `LICENSE`

---

## 🚀 Roadmap

**Future (v2.0)**

* .NET Core/.NET 5+ support
* Extra templates (CRM, ERP)
* UI generation (Angular, React)
* Docker support
* CI/CD pipelines
* Advanced JWT Auth
* GraphQL support
* Microservices templates

**Current Improvements**

* ✅ VS 2015+ support
* ✅ Full Models/Services/Data/Controllers
* ✅ Predefined templates (E-commerce/Blog/Inventory)
* ✅ Interactive CLI
* ✅ Multi-database support
* ✅ Auto unit tests
* ✅ Swagger integration

---

💡 *Built with ❤️ to simplify .NET development*

---
