#!/usr/bin/env python3
"""
Exemples de configurations pour le générateur .NET amélioré
Montre différentes façons de configurer des projets
"""

from enhanced_dotnet_generator import *


def get_minimal_config():
    """Configuration minimale - juste un projet Web API de base"""
    return ProjectConfig(
        project_name="MinimalAPI",
        project_type=ProjectType.WEBAPI,
        output_path="./minimal_output",
        include_database=False,
        include_swagger=True,
        include_cors=True,
        include_tests=False
    )


def get_basic_crud_config():
    """Configuration basique avec CRUD pour une entité simple"""
    
    # Entité User basique
    user_entity = EntityConfig(
        name="User",
        properties=[
            PropertyConfig("Id", "int", is_required=True, is_key=True),
            PropertyConfig("Email", "string", is_required=True, max_length=100),
            PropertyConfig("Name", "string", is_required=True, max_length=50),
            PropertyConfig("CreatedDate", "DateTime", is_required=True)
        ]
    )
    
    return ProjectConfig(
        project_name="BasicCrudAPI",
        project_type=ProjectType.WEBAPI,
        output_path="./basic_output",
        include_database=True,
        database_provider=DatabaseProvider.SQLITE,  # SQLite pour simplicité
        connection_string="Data Source=BasicCrud.db",
        entities=[user_entity],
        include_swagger=True,
        include_cors=True,
        include_tests=True
    )


def get_full_featured_config():
    """Configuration complète avec multiples entités et toutes les fonctionnalités"""
    
    # Entités complètes avec relations
    customer_entity = EntityConfig(
        name="Customer",
        properties=[
            PropertyConfig("Id", "int", is_required=True, is_key=True),
            PropertyConfig("Email", "string", is_required=True, max_length=100),
            PropertyConfig("FirstName", "string", is_required=True, max_length=50),
            PropertyConfig("LastName", "string", is_required=True, max_length=50),
            PropertyConfig("Phone", "string", max_length=20),
            PropertyConfig("Address", "string", max_length=500),
            PropertyConfig("City", "string", max_length=100),
            PropertyConfig("PostalCode", "string", max_length=10),
            PropertyConfig("Country", "string", max_length=50),
            PropertyConfig("DateOfBirth", "DateTime"),
            PropertyConfig("IsActive", "bool", is_required=True),
            PropertyConfig("CreatedDate", "DateTime", is_required=True),
            PropertyConfig("LastModifiedDate", "DateTime")
        ]
    )
    
    order_entity = EntityConfig(
        name="Order",
        properties=[
            PropertyConfig("Id", "int", is_required=True, is_key=True),
            PropertyConfig("OrderNumber", "string", is_required=True, max_length=20),
            PropertyConfig("CustomerId", "int", is_required=True, foreign_table="Customer"),
            PropertyConfig("OrderDate", "DateTime", is_required=True),
            PropertyConfig("RequiredDate", "DateTime"),
            PropertyConfig("ShippedDate", "DateTime"),
            PropertyConfig("Status", "string", is_required=True, max_length=20),
            PropertyConfig("SubTotal", "decimal", is_required=True),
            PropertyConfig("TaxAmount", "decimal", is_required=True),
            PropertyConfig("TotalAmount", "decimal", is_required=True),
            PropertyConfig("ShippingAddress", "string", is_required=True, max_length=500),
            PropertyConfig("Notes", "string", max_length=1000),
            PropertyConfig("CreatedDate", "DateTime", is_required=True)
        ]
    )
    
    product_entity = EntityConfig(
        name="Product",
        properties=[
            PropertyConfig("Id", "int", is_required=True, is_key=True),
            PropertyConfig("Name", "string", is_required=True, max_length=200),
            PropertyConfig("Description", "string", max_length=2000),
            PropertyConfig("SKU", "string", is_required=True, max_length=50),
            PropertyConfig("Price", "decimal", is_required=True),
            PropertyConfig("Cost", "decimal", is_required=True),
            PropertyConfig("Stock", "int", is_required=True),
            PropertyConfig("MinStock", "int", is_required=True),
            PropertyConfig("MaxStock", "int"),
            PropertyConfig("Weight", "decimal"),
            PropertyConfig("Dimensions", "string", max_length=100),
            PropertyConfig("Category", "string", max_length=100),
            PropertyConfig("Brand", "string", max_length=100),
            PropertyConfig("IsActive", "bool", is_required=True),
            PropertyConfig("IsDiscontinued", "bool", is_required=True),
            PropertyConfig("CreatedDate", "DateTime", is_required=True),
            PropertyConfig("LastModifiedDate", "DateTime")
        ]
    )
    
    order_item_entity = EntityConfig(
        name="OrderItem",
        properties=[
            PropertyConfig("Id", "int", is_required=True, is_key=True),
            PropertyConfig("OrderId", "int", is_required=True, foreign_table="Order"),
            PropertyConfig("ProductId", "int", is_required=True, foreign_table="Product"),
            PropertyConfig("Quantity", "int", is_required=True),
            PropertyConfig("UnitPrice", "decimal", is_required=True),
            PropertyConfig("Discount", "decimal"),
            PropertyConfig("TotalPrice", "decimal", is_required=True),
            PropertyConfig("CreatedDate", "DateTime", is_required=True)
        ]
    )
    
    return ProjectConfig(
        project_name="FullFeaturedAPI",
        project_type=ProjectType.WEBAPI,
        output_path="./full_output",
        include_database=True,
        database_provider=DatabaseProvider.SQLSERVER,
        connection_string="Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=FullFeaturedDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False",
        entities=[customer_entity, order_entity, product_entity, order_item_entity],
        include_swagger=True,
        include_cors=True,
        include_authentication=True,
        include_tests=True
    )


def get_mysql_config():
    """Configuration avec base de données MySQL"""
    
    article_entity = EntityConfig(
        name="Article",
        properties=[
            PropertyConfig("Id", "int", is_required=True, is_key=True),
            PropertyConfig("Title", "string", is_required=True, max_length=200),
            PropertyConfig("Content", "string", is_required=True),
            PropertyConfig("AuthorEmail", "string", is_required=True, max_length=100),
            PropertyConfig("PublishedDate", "DateTime"),
            PropertyConfig("IsPublished", "bool", is_required=True),
            PropertyConfig("ViewCount", "int"),
            PropertyConfig("CreatedDate", "DateTime", is_required=True)
        ]
    )
    
    return ProjectConfig(
        project_name="MySQLBlogAPI",
        project_type=ProjectType.WEBAPI,
        output_path="./mysql_output",
        include_database=True,
        database_provider=DatabaseProvider.MYSQL,
        connection_string="Server=localhost;Database=BlogDb;Uid=bloguser;Pwd=blogpassword;",
        entities=[article_entity],
        include_swagger=True,
        include_cors=True,
        include_tests=True
    )


def get_postgresql_config():
    """Configuration avec base de données PostgreSQL"""
    
    employee_entity = EntityConfig(
        name="Employee",
        properties=[
            PropertyConfig("Id", "int", is_required=True, is_key=True),
            PropertyConfig("EmployeeNumber", "string", is_required=True, max_length=20),
            PropertyConfig("FirstName", "string", is_required=True, max_length=50),
            PropertyConfig("LastName", "string", is_required=True, max_length=50),
            PropertyConfig("Email", "string", is_required=True, max_length=100),
            PropertyConfig("Department", "string", is_required=True, max_length=100),
            PropertyConfig("Position", "string", is_required=True, max_length=100),
            PropertyConfig("Salary", "decimal", is_required=True),
            PropertyConfig("HireDate", "DateTime", is_required=True),
            PropertyConfig("IsActive", "bool", is_required=True)
        ]
    )
    
    return ProjectConfig(
        project_name="PostgreSQLHRAPI",
        project_type=ProjectType.WEBAPI,
        output_path="./postgresql_output",
        include_database=True,
        database_provider=DatabaseProvider.POSTGRESQL,
        connection_string="Host=localhost;Database=HRDb;Username=hruser;Password=hrpassword",
        entities=[employee_entity],
        include_swagger=True,
        include_cors=True,
        include_tests=True
    )


# Dictionnaire des configurations disponibles
CONFIGURATIONS = {
    "minimal": ("Projet minimal (sans base de données)", get_minimal_config),
    "basic": ("CRUD basique avec SQLite", get_basic_crud_config),
    "full": ("Projet complet avec multiples entités", get_full_featured_config),
    "mysql": ("Exemple avec MySQL", get_mysql_config),
    "postgresql": ("Exemple avec PostgreSQL", get_postgresql_config)
}


def generate_config_example(config_name: str):
    """Génère un projet à partir d'une configuration d'exemple"""
    
    if config_name not in CONFIGURATIONS:
        print(f"❌ Configuration '{config_name}' inconnue")
        print(f"📋 Configurations disponibles : {list(CONFIGURATIONS.keys())}")
        return False
    
    description, config_func = CONFIGURATIONS[config_name]
    print(f"🚀 Génération du projet : {description}")
    
    try:
        config = config_func()
        generator = EnhancedDotNetGenerator()
        result = generator.generate_project(config)
        
        if result['success']:
            print(f"✅ Projet '{config.project_name}' généré avec succès !")
            print(f"📁 Emplacement : {result['project_path']}")
            return True
        else:
            print(f"❌ Erreur : {result['error']}")
            return False
            
    except Exception as e:
        print(f"💥 Erreur inattendue : {e}")
        return False


if __name__ == "__main__":
    print("📋 Générateur d'exemples de configuration")
    print("=" * 50)
    
    print("\nConfigurations disponibles :")
    for key, (description, _) in CONFIGURATIONS.items():
        print(f"  • {key}: {description}")
    
    print(f"\nUsage :")
    print(f"  python config_example.py [config_name]")
    print(f"  Exemple : python config_example.py basic")
    
    # Si un argument est fourni, générer cette configuration
    import sys
    if len(sys.argv) > 1:
        config_name = sys.argv[1]
        success = generate_config_example(config_name)
        sys.exit(0 if success else 1)