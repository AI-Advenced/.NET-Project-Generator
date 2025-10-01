#!/usr/bin/env python3
"""
Example script to generate a complete .NET project with multiple entities
"""

from enhanced_dotnet_generator import (
    EnhancedDotNetGenerator, 
    ProjectConfig, 
    EntityConfig, 
    PropertyConfig, 
    ProjectType, 
    DatabaseProvider
)


def create_ecommerce_project():
    """Create a complete e-commerce project with multiple entities"""
    
    print("🛒 Creating E-commerce Web API Project...")
    
    # Define User entity
    user_entity = EntityConfig(
        name="User",
        properties=[
            PropertyConfig("Id", "int", is_required=True, is_key=True),
            PropertyConfig("Email", "string", is_required=True, max_length=100),
            PropertyConfig("FirstName", "string", is_required=True, max_length=50),
            PropertyConfig("LastName", "string", is_required=True, max_length=50),
            PropertyConfig("PasswordHash", "string", is_required=True, max_length=255),
            PropertyConfig("PhoneNumber", "string", max_length=20),
            PropertyConfig("DateOfBirth", "DateTime"),
            PropertyConfig("CreatedDate", "DateTime", is_required=True),
            PropertyConfig("LastLoginDate", "DateTime"),
            PropertyConfig("IsActive", "bool", is_required=True),
            PropertyConfig("IsEmailConfirmed", "bool", is_required=True)
        ]
    )
    
    # Define Category entity
    category_entity = EntityConfig(
        name="Category",
        properties=[
            PropertyConfig("Id", "int", is_required=True, is_key=True),
            PropertyConfig("Name", "string", is_required=True, max_length=100),
            PropertyConfig("Description", "string", max_length=500),
            PropertyConfig("ImageUrl", "string", max_length=255),
            PropertyConfig("IsActive", "bool", is_required=True),
            PropertyConfig("CreatedDate", "DateTime", is_required=True),
            PropertyConfig("UpdatedDate", "DateTime")
        ]
    )
    
    # Define Product entity
    product_entity = EntityConfig(
        name="Product",
        properties=[
            PropertyConfig("Id", "int", is_required=True, is_key=True),
            PropertyConfig("Name", "string", is_required=True, max_length=200),
            PropertyConfig("Description", "string", max_length=1000),
            PropertyConfig("Price", "decimal", is_required=True),
            PropertyConfig("ComparePrice", "decimal"),
            PropertyConfig("SKU", "string", is_required=True, max_length=50),
            PropertyConfig("Barcode", "string", max_length=50),
            PropertyConfig("Stock", "int", is_required=True),
            PropertyConfig("MinStock", "int", is_required=True),
            PropertyConfig("Weight", "decimal"),
            PropertyConfig("ImageUrl", "string", max_length=255),
            PropertyConfig("CategoryId", "int", is_required=True, foreign_table="Category"),
            PropertyConfig("IsActive", "bool", is_required=True),
            PropertyConfig("IsFeatured", "bool", is_required=True),
            PropertyConfig("CreatedDate", "DateTime", is_required=True),
            PropertyConfig("UpdatedDate", "DateTime")
        ]
    )
    
    # Define Order entity
    order_entity = EntityConfig(
        name="Order",
        properties=[
            PropertyConfig("Id", "int", is_required=True, is_key=True),
            PropertyConfig("OrderNumber", "string", is_required=True, max_length=20),
            PropertyConfig("UserId", "int", is_required=True, foreign_table="User"),
            PropertyConfig("Status", "string", is_required=True, max_length=20),
            PropertyConfig("SubTotal", "decimal", is_required=True),
            PropertyConfig("TaxAmount", "decimal", is_required=True),
            PropertyConfig("ShippingAmount", "decimal", is_required=True),
            PropertyConfig("DiscountAmount", "decimal"),
            PropertyConfig("TotalAmount", "decimal", is_required=True),
            PropertyConfig("ShippingAddress", "string", is_required=True, max_length=500),
            PropertyConfig("BillingAddress", "string", is_required=True, max_length=500),
            PropertyConfig("PaymentMethod", "string", is_required=True, max_length=50),
            PropertyConfig("PaymentStatus", "string", is_required=True, max_length=20),
            PropertyConfig("Notes", "string", max_length=1000),
            PropertyConfig("CreatedDate", "DateTime", is_required=True),
            PropertyConfig("UpdatedDate", "DateTime"),
            PropertyConfig("ShippedDate", "DateTime"),
            PropertyConfig("DeliveredDate", "DateTime")
        ]
    )
    
    # Define OrderItem entity
    order_item_entity = EntityConfig(
        name="OrderItem",
        properties=[
            PropertyConfig("Id", "int", is_required=True, is_key=True),
            PropertyConfig("OrderId", "int", is_required=True, foreign_table="Order"),
            PropertyConfig("ProductId", "int", is_required=True, foreign_table="Product"),
            PropertyConfig("ProductName", "string", is_required=True, max_length=200),
            PropertyConfig("ProductSKU", "string", is_required=True, max_length=50),
            PropertyConfig("UnitPrice", "decimal", is_required=True),
            PropertyConfig("Quantity", "int", is_required=True),
            PropertyConfig("TotalPrice", "decimal", is_required=True),
            PropertyConfig("CreatedDate", "DateTime", is_required=True)
        ]
    )
    
    # Define Customer Review entity
    review_entity = EntityConfig(
        name="Review",
        properties=[
            PropertyConfig("Id", "int", is_required=True, is_key=True),
            PropertyConfig("ProductId", "int", is_required=True, foreign_table="Product"),
            PropertyConfig("UserId", "int", is_required=True, foreign_table="User"),
            PropertyConfig("Rating", "int", is_required=True),
            PropertyConfig("Title", "string", is_required=True, max_length=200),
            PropertyConfig("Comment", "string", max_length=2000),
            PropertyConfig("IsApproved", "bool", is_required=True),
            PropertyConfig("IsHelpful", "int"),
            PropertyConfig("CreatedDate", "DateTime", is_required=True),
            PropertyConfig("UpdatedDate", "DateTime")
        ]
    )
    
    # Configure project
    config = ProjectConfig(
        project_name="ECommerceAPI",
        project_type=ProjectType.WEBAPI,
        output_path="./generated_projects",
        include_database=True,
        database_provider=DatabaseProvider.SQLSERVER,
        connection_string="Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ECommerceDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False",
        entities=[
            user_entity, 
            category_entity, 
            product_entity, 
            order_entity, 
            order_item_entity, 
            review_entity
        ],
        include_swagger=True,
        include_cors=True,
        include_authentication=False,
        include_tests=True
    )
    
    # Generate project
    generator = EnhancedDotNetGenerator()
    result = generator.generate_project(config)
    
    return result, config


def create_blog_project():
    """Create a blog management API project"""
    
    print("📝 Creating Blog Management Web API Project...")
    
    # Define Author entity
    author_entity = EntityConfig(
        name="Author",
        properties=[
            PropertyConfig("Id", "int", is_required=True, is_key=True),
            PropertyConfig("Email", "string", is_required=True, max_length=100),
            PropertyConfig("Username", "string", is_required=True, max_length=50),
            PropertyConfig("FirstName", "string", is_required=True, max_length=50),
            PropertyConfig("LastName", "string", is_required=True, max_length=50),
            PropertyConfig("Bio", "string", max_length=1000),
            PropertyConfig("AvatarUrl", "string", max_length=255),
            PropertyConfig("Website", "string", max_length=255),
            PropertyConfig("TwitterHandle", "string", max_length=50),
            PropertyConfig("IsActive", "bool", is_required=True),
            PropertyConfig("CreatedDate", "DateTime", is_required=True),
            PropertyConfig("LastLoginDate", "DateTime")
        ]
    )
    
    # Define BlogPost entity
    blog_post_entity = EntityConfig(
        name="BlogPost",
        properties=[
            PropertyConfig("Id", "int", is_required=True, is_key=True),
            PropertyConfig("Title", "string", is_required=True, max_length=200),
            PropertyConfig("Slug", "string", is_required=True, max_length=250),
            PropertyConfig("Content", "string", is_required=True),
            PropertyConfig("Excerpt", "string", max_length=500),
            PropertyConfig("FeaturedImageUrl", "string", max_length=255),
            PropertyConfig("AuthorId", "int", is_required=True, foreign_table="Author"),
            PropertyConfig("Status", "string", is_required=True, max_length=20),
            PropertyConfig("ViewCount", "int"),
            PropertyConfig("LikeCount", "int"),
            PropertyConfig("IsCommentEnabled", "bool", is_required=True),
            PropertyConfig("IsFeatured", "bool", is_required=True),
            PropertyConfig("MetaTitle", "string", max_length=200),
            PropertyConfig("MetaDescription", "string", max_length=300),
            PropertyConfig("PublishedDate", "DateTime"),
            PropertyConfig("CreatedDate", "DateTime", is_required=True),
            PropertyConfig("UpdatedDate", "DateTime")
        ]
    )
    
    # Define Comment entity
    comment_entity = EntityConfig(
        name="Comment",
        properties=[
            PropertyConfig("Id", "int", is_required=True, is_key=True),
            PropertyConfig("BlogPostId", "int", is_required=True, foreign_table="BlogPost"),
            PropertyConfig("ParentCommentId", "int", foreign_table="Comment"),
            PropertyConfig("AuthorName", "string", is_required=True, max_length=100),
            PropertyConfig("AuthorEmail", "string", is_required=True, max_length=100),
            PropertyConfig("AuthorWebsite", "string", max_length=255),
            PropertyConfig("Content", "string", is_required=True, max_length=2000),
            PropertyConfig("IsApproved", "bool", is_required=True),
            PropertyConfig("IpAddress", "string", max_length=45),
            PropertyConfig("UserAgent", "string", max_length=500),
            PropertyConfig("CreatedDate", "DateTime", is_required=True)
        ]
    )
    
    # Define Tag entity
    tag_entity = EntityConfig(
        name="Tag",
        properties=[
            PropertyConfig("Id", "int", is_required=True, is_key=True),
            PropertyConfig("Name", "string", is_required=True, max_length=50),
            PropertyConfig("Slug", "string", is_required=True, max_length=60),
            PropertyConfig("Description", "string", max_length=500),
            PropertyConfig("Color", "string", max_length=7),
            PropertyConfig("PostCount", "int"),
            PropertyConfig("CreatedDate", "DateTime", is_required=True)
        ]
    )
    
    # Configure project
    config = ProjectConfig(
        project_name="BlogAPI",
        project_type=ProjectType.WEBAPI,
        output_path="./generated_projects",
        include_database=True,
        database_provider=DatabaseProvider.SQLSERVER,
        connection_string="Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BlogDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False",
        entities=[
            author_entity, 
            blog_post_entity, 
            comment_entity, 
            tag_entity
        ],
        include_swagger=True,
        include_cors=True,
        include_authentication=True,
        include_tests=True
    )
    
    # Generate project
    generator = EnhancedDotNetGenerator()
    result = generator.generate_project(config)
    
    return result, config


def create_inventory_project():
    """Create an inventory management API project"""
    
    print("📦 Creating Inventory Management Web API Project...")
    
    # Define Supplier entity
    supplier_entity = EntityConfig(
        name="Supplier",
        properties=[
            PropertyConfig("Id", "int", is_required=True, is_key=True),
            PropertyConfig("CompanyName", "string", is_required=True, max_length=200),
            PropertyConfig("ContactName", "string", is_required=True, max_length=100),
            PropertyConfig("ContactEmail", "string", is_required=True, max_length=100),
            PropertyConfig("ContactPhone", "string", max_length=20),
            PropertyConfig("Address", "string", max_length=500),
            PropertyConfig("City", "string", max_length=100),
            PropertyConfig("State", "string", max_length=50),
            PropertyConfig("ZipCode", "string", max_length=10),
            PropertyConfig("Country", "string", max_length=50),
            PropertyConfig("Website", "string", max_length=255),
            PropertyConfig("TaxId", "string", max_length=50),
            PropertyConfig("IsActive", "bool", is_required=True),
            PropertyConfig("CreatedDate", "DateTime", is_required=True),
            PropertyConfig("UpdatedDate", "DateTime")
        ]
    )
    
    # Define Warehouse entity
    warehouse_entity = EntityConfig(
        name="Warehouse",
        properties=[
            PropertyConfig("Id", "int", is_required=True, is_key=True),
            PropertyConfig("Name", "string", is_required=True, max_length=100),
            PropertyConfig("Code", "string", is_required=True, max_length=10),
            PropertyConfig("Address", "string", is_required=True, max_length=500),
            PropertyConfig("City", "string", is_required=True, max_length=100),
            PropertyConfig("State", "string", is_required=True, max_length=50),
            PropertyConfig("ZipCode", "string", is_required=True, max_length=10),
            PropertyConfig("Country", "string", is_required=True, max_length=50),
            PropertyConfig("ManagerName", "string", max_length=100),
            PropertyConfig("ManagerEmail", "string", max_length=100),
            PropertyConfig("ManagerPhone", "string", max_length=20),
            PropertyConfig("IsActive", "bool", is_required=True),
            PropertyConfig("CreatedDate", "DateTime", is_required=True)
        ]
    )
    
    # Define Item entity
    item_entity = EntityConfig(
        name="Item",
        properties=[
            PropertyConfig("Id", "int", is_required=True, is_key=True),
            PropertyConfig("Name", "string", is_required=True, max_length=200),
            PropertyConfig("Description", "string", max_length=1000),
            PropertyConfig("SKU", "string", is_required=True, max_length=50),
            PropertyConfig("Barcode", "string", max_length=50),
            PropertyConfig("Category", "string", max_length=100),
            PropertyConfig("Brand", "string", max_length=100),
            PropertyConfig("UnitOfMeasure", "string", is_required=True, max_length=20),
            PropertyConfig("UnitCost", "decimal", is_required=True),
            PropertyConfig("SalePrice", "decimal", is_required=True),
            PropertyConfig("Weight", "decimal"),
            PropertyConfig("Dimensions", "string", max_length=100),
            PropertyConfig("SupplierId", "int", is_required=True, foreign_table="Supplier"),
            PropertyConfig("MinStockLevel", "int", is_required=True),
            PropertyConfig("MaxStockLevel", "int", is_required=True),
            PropertyConfig("ReorderPoint", "int", is_required=True),
            PropertyConfig("IsActive", "bool", is_required=True),
            PropertyConfig("CreatedDate", "DateTime", is_required=True),
            PropertyConfig("UpdatedDate", "DateTime")
        ]
    )
    
    # Define Stock entity
    stock_entity = EntityConfig(
        name="Stock",
        properties=[
            PropertyConfig("Id", "int", is_required=True, is_key=True),
            PropertyConfig("ItemId", "int", is_required=True, foreign_table="Item"),
            PropertyConfig("WarehouseId", "int", is_required=True, foreign_table="Warehouse"),
            PropertyConfig("QuantityOnHand", "int", is_required=True),
            PropertyConfig("QuantityReserved", "int", is_required=True),
            PropertyConfig("QuantityAvailable", "int", is_required=True),
            PropertyConfig("Location", "string", max_length=50),
            PropertyConfig("LastCountDate", "DateTime"),
            PropertyConfig("LastMovementDate", "DateTime"),
            PropertyConfig("CreatedDate", "DateTime", is_required=True),
            PropertyConfig("UpdatedDate", "DateTime")
        ]
    )
    
    # Configure project
    config = ProjectConfig(
        project_name="InventoryAPI",
        project_type=ProjectType.WEBAPI,
        output_path="./generated_projects",
        include_database=True,
        database_provider=DatabaseProvider.SQLSERVER,
        connection_string="Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=InventoryDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False",
        entities=[
            supplier_entity, 
            warehouse_entity, 
            item_entity, 
            stock_entity
        ],
        include_swagger=True,
        include_cors=True,
        include_authentication=False,
        include_tests=True
    )
    
    # Generate project
    generator = EnhancedDotNetGenerator()
    result = generator.generate_project(config)
    
    return result, config


def print_project_summary(result, config):
    """Print a summary of the generated project"""
    if result['success']:
        print(f"\n✅ {config.project_name} generated successfully!")
        print(f"📁 Location: {result['project_path']}")
        print(f"📄 Files created: {len(result['files_created'])}")
        
        print(f"\n📊 Project Details:")
        print(f"   • Framework: .NET Framework 4.8")
        print(f"   • Project Type: {config.project_type.value.upper()}")
        print(f"   • Database: {config.database_provider.value.upper()}")
        print(f"   • Entities: {len(config.entities)}")
        print(f"   • Swagger: {'✓' if config.include_swagger else '✗'}")
        print(f"   • CORS: {'✓' if config.include_cors else '✗'}")
        print(f"   • Unit Tests: {'✓' if config.include_tests else '✗'}")
        
        print(f"\n🎯 Generated Entities:")
        for entity in config.entities:
            print(f"   • {entity.name} ({len(entity.properties)} properties)")
        
        print(f"\n🚀 Next Steps:")
        print(f"   1. Open Visual Studio 2015 or later")
        print(f"   2. Open {result['project_path']}/{config.project_name}.sln")
        print(f"   3. Restore NuGet packages (right-click solution → Restore NuGet Packages)")
        print(f"   4. Update connection string in Web.config if needed")
        print(f"   5. Build solution (Ctrl+Shift+B)")
        print(f"   6. Run project (F5)")
        
        if config.include_swagger:
            print(f"   7. Access Swagger UI at: http://localhost:[port]/swagger")
        
        print(f"\n🌐 Available API Endpoints:")
        for entity in config.entities:
            name = entity.name.lower()
            print(f"   • GET    /api/{name}")
            print(f"   • GET    /api/{name}/{{id}}")
            print(f"   • POST   /api/{name}")
            print(f"   • PUT    /api/{name}/{{id}}")
            print(f"   • DELETE /api/{name}/{{id}}")
        
    else:
        print(f"\n❌ Failed to generate {config.project_name}: {result['error']}")


def main():
    """Main function to generate multiple projects"""
    print("🎯 Enhanced .NET Project Generator")
    print("=" * 60)
    print("Generating multiple complete .NET Web API projects...")
    print("Compatible with Visual Studio 2015 and later versions")
    print("=" * 60)
    
    projects = [
        ("E-commerce API", create_ecommerce_project),
        ("Blog Management API", create_blog_project),
        ("Inventory Management API", create_inventory_project)
    ]
    
    for project_name, create_func in projects:
        print(f"\n{project_name}")
        print("-" * 60)
        
        try:
            result, config = create_func()
            print_project_summary(result, config)
            
        except Exception as e:
            print(f"❌ Error generating {project_name}: {str(e)}")
            import traceback
            traceback.print_exc()
        
        print("-" * 60)
    
    print(f"\n🎉 Project generation completed!")
    print(f"📁 Check './generated_projects' folder for all generated projects")
    print(f"💡 Each project is a complete, working .NET Web API with:")
    print(f"   • Full CRUD operations")
    print(f"   • Entity Framework 6 integration")
    print(f"   • RESTful API endpoints")
    print(f"   • Swagger documentation")
    print(f"   • Unit tests")
    print(f"   • Visual Studio 2015+ compatibility")


if __name__ == "__main__":
    main()