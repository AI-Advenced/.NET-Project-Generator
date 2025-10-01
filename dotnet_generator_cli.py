#!/usr/bin/env python3
"""
Enhanced .NET Project Generator CLI
Un générateur CLI interactif pour créer des projets .NET complets compatibles avec Visual Studio 2015+
"""

import os
import sys
from pathlib import Path
from typing import List, Tuple
import inquirer
from rich.console import Console
from rich.table import Table
from rich.panel import Panel
from rich.progress import Progress, SpinnerColumn, TextColumn
from rich.prompt import Prompt, IntPrompt, Confirm
from rich.syntax import Syntax

# Import du générateur
from enhanced_dotnet_generator import (
    EnhancedDotNetGenerator, 
    ProjectConfig, 
    EntityConfig, 
    PropertyConfig, 
    ProjectType, 
    DatabaseProvider
)

console = Console()

class DotNetGeneratorCLI:
    """CLI interactif pour le générateur .NET amélioré"""
    
    def __init__(self):
        self.generator = EnhancedDotNetGenerator()
        self.config = None
        
    def show_welcome(self):
        """Affiche l'écran d'accueil"""
        console.print(Panel.fit(
            "[bold blue]🚀 Enhanced .NET Project Generator[/bold blue]\n"
            "[dim]Générateur de projets .NET complets compatibles Visual Studio 2015+[/dim]\n\n"
            "[green]Caractéristiques :[/green]\n"
            "• Projets Web API complets avec CRUD\n"
            "• Entity Framework 6 intégré\n"
            "• Swagger/OpenAPI documentation\n"
            "• Support CORS\n"
            "• Tests unitaires (MSTest)\n"
            "• Compatible Visual Studio 2015 et versions ultérieures",
            title="[bold green]Bienvenue[/bold green]"
        ))
    
    def get_project_config(self) -> ProjectConfig:
        """Configure le projet de manière interactive"""
        console.print("\n[bold yellow]📋 Configuration du projet[/bold yellow]")
        
        # Nom du projet
        project_name = Prompt.ask(
            "[cyan]Nom du projet[/cyan]", 
            default="MyWebAPI"
        )
        
        # Type de projet
        project_types = [
            ("Web API", ProjectType.WEBAPI),
            ("MVC", ProjectType.MVC),
            ("Console", ProjectType.CONSOLE)
        ]
        
        type_choices = [name for name, _ in project_types]
        selected_type = inquirer.list_input(
            "Type de projet",
            choices=type_choices,
            default=type_choices[0]
        )
        
        project_type = next(ptype for name, ptype in project_types if name == selected_type)
        
        # Dossier de sortie
        output_path = Prompt.ask(
            "[cyan]Dossier de sortie[/cyan]", 
            default="./generated_projects"
        )
        
        # Configuration de base de données
        include_database = Confirm.ask("[cyan]Inclure une base de données ?[/cyan]", default=True)
        
        database_provider = DatabaseProvider.SQLSERVER
        connection_string = ""
        
        if include_database:
            db_providers = [
                ("SQL Server", DatabaseProvider.SQLSERVER),
                ("SQLite", DatabaseProvider.SQLITE),
                ("MySQL", DatabaseProvider.MYSQL),
                ("PostgreSQL", DatabaseProvider.POSTGRESQL)
            ]
            
            db_choices = [name for name, _ in db_providers]
            selected_db = inquirer.list_input(
                "Fournisseur de base de données",
                choices=db_choices,
                default=db_choices[0]
            )
            
            database_provider = next(db for name, db in db_providers if name == selected_db)
            
            # Chaîne de connexion par défaut
            if database_provider == DatabaseProvider.SQLSERVER:
                connection_string = f"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog={project_name}Db;Integrated Security=True"
            elif database_provider == DatabaseProvider.SQLITE:
                connection_string = f"Data Source={project_name}.db"
            
            custom_connection = Confirm.ask("[cyan]Personnaliser la chaîne de connexion ?[/cyan]", default=False)
            if custom_connection:
                connection_string = Prompt.ask("[cyan]Chaîne de connexion[/cyan]", default=connection_string)
        
        # Options avancées
        include_swagger = Confirm.ask("[cyan]Inclure Swagger/OpenAPI ?[/cyan]", default=True)
        include_cors = Confirm.ask("[cyan]Inclure support CORS ?[/cyan]", default=True)
        include_authentication = Confirm.ask("[cyan]Inclure authentification ?[/cyan]", default=False)
        include_tests = Confirm.ask("[cyan]Inclure tests unitaires ?[/cyan]", default=True)
        
        return ProjectConfig(
            project_name=project_name,
            project_type=project_type,
            output_path=output_path,
            include_database=include_database,
            database_provider=database_provider,
            connection_string=connection_string,
            entities=[],
            include_swagger=include_swagger,
            include_cors=include_cors,
            include_authentication=include_authentication,
            include_tests=include_tests
        )
    
    def add_entities(self, config: ProjectConfig):
        """Ajoute des entités au projet de manière interactive"""
        console.print("\n[bold yellow]🗂️ Configuration des entités[/bold yellow]")
        
        if not Confirm.ask("[cyan]Voulez-vous ajouter des entités personnalisées ?[/cyan]", default=True):
            return
        
        while True:
            entity = self.create_entity()
            if entity:
                config.entities.append(entity)
                console.print(f"[green]✓[/green] Entité '{entity.name}' ajoutée avec {len(entity.properties)} propriétés")
            
            if not Confirm.ask("[cyan]Ajouter une autre entité ?[/cyan]", default=False):
                break
    
    def create_entity(self) -> EntityConfig:
        """Crée une entité de manière interactive"""
        console.print("\n[dim]--- Nouvelle entité ---[/dim]")
        
        name = Prompt.ask("[cyan]Nom de l'entité[/cyan]")
        if not name:
            return None
        
        properties = []
        
        # Ajouter automatiquement une clé primaire ID
        properties.append(PropertyConfig("Id", "int", is_required=True, is_key=True))
        console.print("[dim]• Propriété 'Id' (clé primaire) ajoutée automatiquement[/dim]")
        
        # Ajouter des propriétés personnalisées
        while True:
            prop = self.create_property()
            if prop:
                properties.append(prop)
                console.print(f"[green]✓[/green] Propriété '{prop.name}' ajoutée")
            
            if not Confirm.ask("[cyan]Ajouter une autre propriété ?[/cyan]", default=True):
                break
        
        table_name = Prompt.ask(
            f"[cyan]Nom de la table (optionnel)[/cyan]", 
            default=f"{name}s"
        )
        
        return EntityConfig(
            name=name,
            properties=properties,
            table_name=table_name if table_name != f"{name}s" else None
        )
    
    def create_property(self) -> PropertyConfig:
        """Crée une propriété de manière interactive"""
        name = Prompt.ask("[cyan]  Nom de la propriété[/cyan]")
        if not name:
            return None
        
        # Types courants
        type_choices = [
            "string", "int", "decimal", "DateTime", "bool", 
            "double", "float", "long", "byte[]"
        ]
        
        prop_type = inquirer.list_input(
            "  Type de propriété",
            choices=type_choices,
            default="string"
        )
        
        is_required = Confirm.ask("[cyan]  Propriété obligatoire ?[/cyan]", default=False)
        
        max_length = None
        if prop_type == "string":
            has_max_length = Confirm.ask("[cyan]  Limiter la longueur ?[/cyan]", default=False)
            if has_max_length:
                max_length = IntPrompt.ask("[cyan]  Longueur maximum[/cyan]", default=100)
        
        is_foreign_key = Confirm.ask("[cyan]  Clé étrangère ?[/cyan]", default=False)
        foreign_table = None
        if is_foreign_key:
            foreign_table = Prompt.ask("[cyan]  Table référencée[/cyan]")
        
        return PropertyConfig(
            name=name,
            type=prop_type,
            is_required=is_required,
            is_key=False,
            max_length=max_length,
            foreign_table=foreign_table
        )
    
    def show_project_summary(self, config: ProjectConfig):
        """Affiche un résumé du projet configuré"""
        console.print("\n[bold yellow]📊 Résumé du projet[/bold yellow]")
        
        table = Table(show_header=True, header_style="bold blue")
        table.add_column("Paramètre", style="cyan")
        table.add_column("Valeur", style="green")
        
        table.add_row("Nom du projet", config.project_name)
        table.add_row("Type", config.project_type.value.upper())
        table.add_row("Framework", ".NET Framework 4.8")
        table.add_row("Dossier de sortie", config.output_path)
        table.add_row("Base de données", "✓" if config.include_database else "✗")
        
        if config.include_database:
            table.add_row("Fournisseur DB", config.database_provider.value.upper())
            table.add_row("Entités", str(len(config.entities)))
        
        table.add_row("Swagger", "✓" if config.include_swagger else "✗")
        table.add_row("CORS", "✓" if config.include_cors else "✗")
        table.add_row("Tests unitaires", "✓" if config.include_tests else "✗")
        
        console.print(table)
        
        if config.entities:
            console.print(f"\n[bold cyan]Entités configurées:[/bold cyan]")
            for entity in config.entities:
                console.print(f"  • {entity.name} ({len(entity.properties)} propriétés)")
    
    def generate_project(self, config: ProjectConfig):
        """Génère le projet avec une barre de progression"""
        console.print(f"\n[bold green]🚀 Génération du projet '{config.project_name}'...[/bold green]")
        
        with Progress(
            SpinnerColumn(),
            TextColumn("[progress.description]{task.description}"),
            console=console
        ) as progress:
            task = progress.add_task("Génération en cours...", total=None)
            
            result = self.generator.generate_project(config)
            
            progress.update(task, completed=True)
        
        return result
    
    def show_result(self, result: dict, config: ProjectConfig):
        """Affiche le résultat de la génération"""
        if result['success']:
            console.print(Panel.fit(
                f"[bold green]✅ Projet généré avec succès ![/bold green]\n\n"
                f"[cyan]📁 Emplacement :[/cyan] {result['project_path']}\n"
                f"[cyan]📄 Fichiers créés :[/cyan] {len(result['files_created'])}\n\n"
                f"[yellow]🚀 Prochaines étapes :[/yellow]\n"
                f"1. Ouvrir Visual Studio 2015 ou version ultérieure\n"
                f"2. Ouvrir le fichier {config.project_name}.sln\n"
                f"3. Restaurer les packages NuGet\n"
                f"4. Mettre à jour la chaîne de connexion si nécessaire\n"
                f"5. Compiler et exécuter le projet (F5)",
                title="[bold green]Succès[/bold green]"
            ))
            
            if config.include_swagger:
                console.print(f"[dim]💡 Documentation Swagger disponible à : http://localhost:[port]/swagger[/dim]")
        else:
            console.print(Panel.fit(
                f"[bold red]❌ Erreur lors de la génération[/bold red]\n\n"
                f"{result['error']}",
                title="[bold red]Erreur[/bold red]"
            ))
    
    def show_templates_menu(self):
        """Affiche le menu des templates prédéfinis"""
        console.print("\n[bold yellow]📋 Templates prédéfinis[/bold yellow]")
        
        templates = [
            ("Projet E-commerce complet", self.create_ecommerce_template),
            ("API de gestion de blog", self.create_blog_template),
            ("Système de gestion d'inventaire", self.create_inventory_template),
            ("Retour au menu principal", None)
        ]
        
        choices = [name for name, _ in templates]
        selected = inquirer.list_input(
            "Choisir un template",
            choices=choices
        )
        
        template_func = next(func for name, func in templates if name == selected)
        
        if template_func:
            return template_func()
        return None
    
    def create_ecommerce_template(self) -> ProjectConfig:
        """Crée un template e-commerce complet"""
        project_name = Prompt.ask("[cyan]Nom du projet e-commerce[/cyan]", default="ECommerceAPI")
        
        # Entités e-commerce
        entities = [
            EntityConfig("User", [
                PropertyConfig("Id", "int", is_required=True, is_key=True),
                PropertyConfig("Email", "string", is_required=True, max_length=100),
                PropertyConfig("FirstName", "string", is_required=True, max_length=50),
                PropertyConfig("LastName", "string", is_required=True, max_length=50),
                PropertyConfig("PasswordHash", "string", is_required=True, max_length=255),
                PropertyConfig("PhoneNumber", "string", max_length=20),
                PropertyConfig("DateOfBirth", "DateTime"),
                PropertyConfig("CreatedDate", "DateTime", is_required=True),
                PropertyConfig("IsActive", "bool", is_required=True)
            ]),
            EntityConfig("Category", [
                PropertyConfig("Id", "int", is_required=True, is_key=True),
                PropertyConfig("Name", "string", is_required=True, max_length=100),
                PropertyConfig("Description", "string", max_length=500),
                PropertyConfig("IsActive", "bool", is_required=True),
                PropertyConfig("CreatedDate", "DateTime", is_required=True)
            ]),
            EntityConfig("Product", [
                PropertyConfig("Id", "int", is_required=True, is_key=True),
                PropertyConfig("Name", "string", is_required=True, max_length=200),
                PropertyConfig("Description", "string", max_length=1000),
                PropertyConfig("Price", "decimal", is_required=True),
                PropertyConfig("SKU", "string", is_required=True, max_length=50),
                PropertyConfig("Stock", "int", is_required=True),
                PropertyConfig("CategoryId", "int", is_required=True, foreign_table="Category"),
                PropertyConfig("IsActive", "bool", is_required=True),
                PropertyConfig("CreatedDate", "DateTime", is_required=True)
            ]),
            EntityConfig("Order", [
                PropertyConfig("Id", "int", is_required=True, is_key=True),
                PropertyConfig("OrderNumber", "string", is_required=True, max_length=20),
                PropertyConfig("UserId", "int", is_required=True, foreign_table="User"),
                PropertyConfig("Status", "string", is_required=True, max_length=20),
                PropertyConfig("TotalAmount", "decimal", is_required=True),
                PropertyConfig("CreatedDate", "DateTime", is_required=True)
            ])
        ]
        
        return ProjectConfig(
            project_name=project_name,
            project_type=ProjectType.WEBAPI,
            output_path="./generated_projects",
            include_database=True,
            database_provider=DatabaseProvider.SQLSERVER,
            connection_string=f"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog={project_name}Db;Integrated Security=True",
            entities=entities,
            include_swagger=True,
            include_cors=True,
            include_authentication=False,
            include_tests=True
        )
    
    def create_blog_template(self) -> ProjectConfig:
        """Crée un template de blog"""
        project_name = Prompt.ask("[cyan]Nom du projet blog[/cyan]", default="BlogAPI")
        
        entities = [
            EntityConfig("Author", [
                PropertyConfig("Id", "int", is_required=True, is_key=True),
                PropertyConfig("Email", "string", is_required=True, max_length=100),
                PropertyConfig("Username", "string", is_required=True, max_length=50),
                PropertyConfig("FirstName", "string", is_required=True, max_length=50),
                PropertyConfig("LastName", "string", is_required=True, max_length=50),
                PropertyConfig("Bio", "string", max_length=1000),
                PropertyConfig("IsActive", "bool", is_required=True),
                PropertyConfig("CreatedDate", "DateTime", is_required=True)
            ]),
            EntityConfig("BlogPost", [
                PropertyConfig("Id", "int", is_required=True, is_key=True),
                PropertyConfig("Title", "string", is_required=True, max_length=200),
                PropertyConfig("Content", "string", is_required=True),
                PropertyConfig("AuthorId", "int", is_required=True, foreign_table="Author"),
                PropertyConfig("Status", "string", is_required=True, max_length=20),
                PropertyConfig("ViewCount", "int"),
                PropertyConfig("PublishedDate", "DateTime"),
                PropertyConfig("CreatedDate", "DateTime", is_required=True)
            ]),
            EntityConfig("Comment", [
                PropertyConfig("Id", "int", is_required=True, is_key=True),
                PropertyConfig("BlogPostId", "int", is_required=True, foreign_table="BlogPost"),
                PropertyConfig("AuthorName", "string", is_required=True, max_length=100),
                PropertyConfig("Content", "string", is_required=True, max_length=2000),
                PropertyConfig("IsApproved", "bool", is_required=True),
                PropertyConfig("CreatedDate", "DateTime", is_required=True)
            ])
        ]
        
        return ProjectConfig(
            project_name=project_name,
            project_type=ProjectType.WEBAPI,
            output_path="./generated_projects",
            include_database=True,
            database_provider=DatabaseProvider.SQLSERVER,
            connection_string=f"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog={project_name}Db;Integrated Security=True",
            entities=entities,
            include_swagger=True,
            include_cors=True,
            include_authentication=True,
            include_tests=True
        )
    
    def create_inventory_template(self) -> ProjectConfig:
        """Crée un template de gestion d'inventaire"""
        project_name = Prompt.ask("[cyan]Nom du projet inventaire[/cyan]", default="InventoryAPI")
        
        entities = [
            EntityConfig("Supplier", [
                PropertyConfig("Id", "int", is_required=True, is_key=True),
                PropertyConfig("CompanyName", "string", is_required=True, max_length=200),
                PropertyConfig("ContactName", "string", is_required=True, max_length=100),
                PropertyConfig("ContactEmail", "string", is_required=True, max_length=100),
                PropertyConfig("Address", "string", max_length=500),
                PropertyConfig("IsActive", "bool", is_required=True),
                PropertyConfig("CreatedDate", "DateTime", is_required=True)
            ]),
            EntityConfig("Item", [
                PropertyConfig("Id", "int", is_required=True, is_key=True),
                PropertyConfig("Name", "string", is_required=True, max_length=200),
                PropertyConfig("SKU", "string", is_required=True, max_length=50),
                PropertyConfig("UnitCost", "decimal", is_required=True),
                PropertyConfig("SalePrice", "decimal", is_required=True),
                PropertyConfig("SupplierId", "int", is_required=True, foreign_table="Supplier"),
                PropertyConfig("MinStockLevel", "int", is_required=True),
                PropertyConfig("IsActive", "bool", is_required=True),
                PropertyConfig("CreatedDate", "DateTime", is_required=True)
            ])
        ]
        
        return ProjectConfig(
            project_name=project_name,
            project_type=ProjectType.WEBAPI,
            output_path="./generated_projects",
            include_database=True,
            database_provider=DatabaseProvider.SQLSERVER,
            connection_string=f"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog={project_name}Db;Integrated Security=True",
            entities=entities,
            include_swagger=True,
            include_cors=True,
            include_authentication=False,
            include_tests=True
        )
    
    def main_menu(self):
        """Menu principal du CLI"""
        while True:
            console.clear()
            self.show_welcome()
            
            choices = [
                "Créer un projet personnalisé",
                "Utiliser un template prédéfini", 
                "Quitter"
            ]
            
            choice = inquirer.list_input(
                "Que souhaitez-vous faire ?",
                choices=choices
            )
            
            if choice == "Créer un projet personnalisé":
                config = self.get_project_config()
                self.add_entities(config)
                self.show_project_summary(config)
                
                if Confirm.ask("\n[cyan]Continuer avec cette configuration ?[/cyan]", default=True):
                    result = self.generate_project(config)
                    self.show_result(result, config)
                    
                    if Confirm.ask("\n[cyan]Générer un autre projet ?[/cyan]", default=False):
                        continue
                    else:
                        break
                        
            elif choice == "Utiliser un template prédéfini":
                config = self.show_templates_menu()
                if config:
                    self.show_project_summary(config)
                    
                    if Confirm.ask("\n[cyan]Générer ce projet ?[/cyan]", default=True):
                        result = self.generate_project(config)
                        self.show_result(result, config)
                        
                        if Confirm.ask("\n[cyan]Générer un autre projet ?[/cyan]", default=False):
                            continue
                        else:
                            break
            else:
                console.print("\n[green]👋 Merci d'avoir utilisé le générateur .NET ![/green]")
                break
    
    def run(self):
        """Point d'entrée principal du CLI"""
        try:
            # Vérifier les dépendances
            try:
                import inquirer
                from rich.console import Console
            except ImportError as e:
                console.print(f"[red]Erreur :[/red] Dépendance manquante : {e}")
                console.print("[yellow]Installation requise :[/yellow] pip install inquirer rich")
                return 1
            
            self.main_menu()
            return 0
            
        except KeyboardInterrupt:
            console.print("\n\n[yellow]⚠️ Opération annulée par l'utilisateur[/yellow]")
            return 1
        except Exception as e:
            console.print(f"\n[red]❌ Erreur inattendue :[/red] {e}")
            return 1


if __name__ == "__main__":
    # Installation automatique des dépendances si nécessaire
    try:
        import inquirer
        from rich.console import Console
        from rich.table import Table
        from rich.panel import Panel
        from rich.progress import Progress, SpinnerColumn, TextColumn
        from rich.prompt import Prompt, IntPrompt, Confirm
        
    except ImportError as e:
        print(f"🚨 Dépendance manquante : {e}")
        print("📦 Installation des dépendances requises...")
        
        import subprocess
        import sys
        
        try:
            subprocess.check_call([sys.executable, "-m", "pip", "install", "inquirer", "rich"])
            print("✅ Dépendances installées avec succès !")
            print("🔄 Veuillez relancer le script...")
        except subprocess.CalledProcessError:
            print("❌ Erreur lors de l'installation des dépendances")
            print("🔧 Veuillez installer manuellement : pip install inquirer rich")
        
        sys.exit(1)
    
    cli = DotNetGeneratorCLI()
    sys.exit(cli.run())