#!/usr/bin/env python3
"""
Inspecteur de projets .NET générés
Analyse et affiche des informations sur les projets générés
"""

import os
from pathlib import Path
import xml.etree.ElementTree as ET
from typing import List, Dict, Any
from rich.console import Console
from rich.table import Table
from rich.panel import Panel
from rich.tree import Tree
from rich.syntax import Syntax


console = Console()


class ProjectInspector:
    """Inspecteur pour analyser les projets .NET générés"""
    
    def __init__(self, project_path: str):
        self.project_path = Path(project_path)
        self.project_name = self.project_path.name
        
    def inspect_project(self):
        """Analyse complète du projet"""
        if not self.project_path.exists():
            console.print(f"[red]❌ Projet non trouvé : {self.project_path}[/red]")
            return
        
        console.print(Panel.fit(
            f"[bold blue]🔍 Inspection du projet '{self.project_name}'[/bold blue]\n"
            f"[dim]Chemin : {self.project_path}[/dim]",
            title="[bold green]Projet Inspector[/bold green]"
        ))
        
        # Analyse de la structure
        self.show_project_structure()
        
        # Analyse des fichiers de configuration
        self.analyze_configuration_files()
        
        # Analyse des entités
        self.analyze_entities()
        
        # Analyse des contrôleurs
        self.analyze_controllers()
        
        # Analyse des services
        self.analyze_services()
        
        # Analyse des tests
        self.analyze_tests()
        
        # Résumé
        self.show_summary()
    
    def show_project_structure(self):
        """Affiche la structure du projet sous forme d'arbre"""
        console.print("\n[bold yellow]📁 Structure du projet[/bold yellow]")
        
        tree = Tree(f"[bold]{self.project_name}[/bold]")
        
        def add_directory_to_tree(path: Path, tree_node):
            try:
                for item in sorted(path.iterdir()):
                    if item.is_dir():
                        if item.name in ['.vs', 'bin', 'obj', 'packages']:
                            continue
                        dir_node = tree_node.add(f"[blue]📁 {item.name}/[/blue]")
                        add_directory_to_tree(item, dir_node)
                    elif item.is_file():
                        icon = self.get_file_icon(item.suffix)
                        tree_node.add(f"{icon} {item.name}")
            except PermissionError:
                tree_node.add("[red]❌ Accès refusé[/red]")
        
        add_directory_to_tree(self.project_path, tree)
        console.print(tree)
    
    def get_file_icon(self, extension: str) -> str:
        """Retourne une icône basée sur l'extension de fichier"""
        icons = {
            '.sln': '🔧',
            '.csproj': '📦',
            '.cs': '📄',
            '.config': '⚙️',
            '.json': '📋',
            '.xml': '📜',
            '.md': '📚',
            '.gitignore': '🚫',
            '.asax': '🌐'
        }
        return icons.get(extension.lower(), '📄')
    
    def analyze_configuration_files(self):
        """Analyse les fichiers de configuration"""
        console.print("\n[bold yellow]⚙️ Fichiers de configuration[/bold yellow]")
        
        # Chercher les fichiers de configuration
        config_files = []
        
        # Solution file
        sln_files = list(self.project_path.glob("*.sln"))
        if sln_files:
            config_files.append(("Solution", sln_files[0]))
        
        # Project file
        csproj_files = list(self.project_path.glob("**/*.csproj"))
        if csproj_files:
            config_files.append(("Projet principal", csproj_files[0]))
        
        # Web.config
        web_config = self.project_path / "src" / self.project_name / "Web.config"
        if web_config.exists():
            config_files.append(("Configuration Web", web_config))
        
        # packages.config
        packages_config = self.project_path / "src" / self.project_name / "packages.config"
        if packages_config.exists():
            config_files.append(("Packages NuGet", packages_config))
        
        table = Table(show_header=True, header_style="bold blue")
        table.add_column("Type", style="cyan")
        table.add_column("Fichier", style="green")
        table.add_column("Taille", style="yellow")
        table.add_column("Status", style="magenta")
        
        for config_type, config_file in config_files:
            if config_file.exists():
                size = f"{config_file.stat().st_size} bytes"
                status = "✅ Trouvé"
            else:
                size = "-"
                status = "❌ Manquant"
            
            table.add_row(config_type, str(config_file.name), size, status)
        
        console.print(table)
    
    def analyze_entities(self):
        """Analyse les entités du modèle"""
        console.print("\n[bold yellow]🗂️ Entités du modèle[/bold yellow]")
        
        models_path = self.project_path / "src" / self.project_name / "Models"
        if not models_path.exists():
            console.print("[red]❌ Dossier Models non trouvé[/red]")
            return
        
        entity_files = list(models_path.glob("*.cs"))
        
        if not entity_files:
            console.print("[red]❌ Aucune entité trouvée[/red]")
            return
        
        table = Table(show_header=True, header_style="bold blue")
        table.add_column("Entité", style="cyan")
        table.add_column("Propriétés", style="green")
        table.add_column("Taille", style="yellow")
        
        for entity_file in entity_files:
            entity_name = entity_file.stem
            properties = self.count_properties_in_entity(entity_file)
            size = f"{entity_file.stat().st_size} bytes"
            
            table.add_row(entity_name, str(properties), size)
        
        console.print(table)
    
    def count_properties_in_entity(self, entity_file: Path) -> int:
        """Compte le nombre de propriétés dans une entité"""
        try:
            content = entity_file.read_text(encoding='utf-8')
            # Compte les lignes contenant "{ get; set; }"
            return content.count("{ get; set; }")
        except:
            return 0
    
    def analyze_controllers(self):
        """Analyse les contrôleurs"""
        console.print("\n[bold yellow]🎮 Contrôleurs API[/bold yellow]")
        
        controllers_path = self.project_path / "src" / self.project_name / "Controllers"
        if not controllers_path.exists():
            console.print("[red]❌ Dossier Controllers non trouvé[/red]")
            return
        
        controller_files = list(controllers_path.glob("*Controller.cs"))
        
        if not controller_files:
            console.print("[red]❌ Aucun contrôleur trouvé[/red]")
            return
        
        table = Table(show_header=True, header_style="bold blue")
        table.add_column("Contrôleur", style="cyan")
        table.add_column("Actions", style="green")
        table.add_column("Route", style="yellow")
        
        for controller_file in controller_files:
            controller_name = controller_file.stem
            actions = self.count_actions_in_controller(controller_file)
            route = self.extract_route_from_controller(controller_file)
            
            table.add_row(controller_name, str(actions), route)
        
        console.print(table)
    
    def count_actions_in_controller(self, controller_file: Path) -> int:
        """Compte le nombre d'actions dans un contrôleur"""
        try:
            content = controller_file.read_text(encoding='utf-8')
            # Compte les méthodes HTTP
            http_methods = ['[HttpGet]', '[HttpPost]', '[HttpPut]', '[HttpDelete]']
            return sum(content.count(method) for method in http_methods)
        except:
            return 0
    
    def extract_route_from_controller(self, controller_file: Path) -> str:
        """Extrait le préfixe de route d'un contrôleur"""
        try:
            content = controller_file.read_text(encoding='utf-8')
            import re
            match = re.search(r'\\[RoutePrefix\\("([^"]+)"\\)\\]', content)
            return match.group(1) if match else "api/?"
        except:
            return "?"
    
    def analyze_services(self):
        """Analyse les services"""
        console.print("\n[bold yellow]⚙️ Services métier[/bold yellow]")
        
        services_path = self.project_path / "src" / self.project_name / "Services"
        if not services_path.exists():
            console.print("[red]❌ Dossier Services non trouvé[/red]")
            return
        
        service_files = list(services_path.glob("*Service.cs"))
        interface_files = list(services_path.glob("I*Service.cs"))
        
        table = Table(show_header=True, header_style="bold blue")
        table.add_column("Service", style="cyan")
        table.add_column("Interface", style="green")
        table.add_column("Méthodes", style="yellow")
        
        for service_file in service_files:
            service_name = service_file.stem
            interface_name = f"I{service_name}"
            
            interface_exists = any(f.stem == interface_name for f in interface_files)
            methods = self.count_methods_in_service(service_file)
            
            table.add_row(
                service_name,
                "✅" if interface_exists else "❌",
                str(methods)
            )
        
        console.print(table)
    
    def count_methods_in_service(self, service_file: Path) -> int:
        """Compte le nombre de méthodes dans un service"""
        try:
            content = service_file.read_text(encoding='utf-8')
            import re
            # Compte les méthodes publiques
            matches = re.findall(r'public .+\\(.*\\)', content)
            return len(matches)
        except:
            return 0
    
    def analyze_tests(self):
        """Analyse les tests unitaires"""
        console.print("\n[bold yellow]🧪 Tests unitaires[/bold yellow]")
        
        tests_path = self.project_path / "tests"
        if not tests_path.exists():
            console.print("[red]❌ Dossier tests non trouvé[/red]")
            return
        
        test_files = list(tests_path.glob("**/*Tests.cs"))
        
        if not test_files:
            console.print("[red]❌ Aucun fichier de test trouvé[/red]")
            return
        
        table = Table(show_header=True, header_style="bold blue")
        table.add_column("Fichier de test", style="cyan")
        table.add_column("Tests", style="green")
        table.add_column("Type", style="yellow")
        
        for test_file in test_files:
            test_name = test_file.stem
            test_count = self.count_test_methods(test_file)
            test_type = "Service" if "Service" in test_name else "Controller" if "Controller" in test_name else "Autre"
            
            table.add_row(test_name, str(test_count), test_type)
        
        console.print(table)
    
    def count_test_methods(self, test_file: Path) -> int:
        """Compte le nombre de méthodes de test"""
        try:
            content = test_file.read_text(encoding='utf-8')
            return content.count("[TestMethod]")
        except:
            return 0
    
    def show_summary(self):
        """Affiche un résumé du projet"""
        console.print("\n[bold yellow]📊 Résumé du projet[/bold yellow]")
        
        # Compter les différents éléments
        models_count = len(list((self.project_path / "src" / self.project_name / "Models").glob("*.cs"))) if (self.project_path / "src" / self.project_name / "Models").exists() else 0
        controllers_count = len(list((self.project_path / "src" / self.project_name / "Controllers").glob("*Controller.cs"))) if (self.project_path / "src" / self.project_name / "Controllers").exists() else 0
        services_count = len(list((self.project_path / "src" / self.project_name / "Services").glob("*Service.cs"))) if (self.project_path / "src" / self.project_name / "Services").exists() else 0
        tests_count = len(list((self.project_path / "tests").glob("**/*Tests.cs"))) if (self.project_path / "tests").exists() else 0
        
        # Calculer la taille totale
        total_size = sum(f.stat().st_size for f in self.project_path.rglob("*") if f.is_file())
        size_mb = total_size / (1024 * 1024)
        
        summary_table = Table(show_header=True, header_style="bold blue")
        summary_table.add_column("Composant", style="cyan")
        summary_table.add_column("Nombre", style="green")
        
        summary_table.add_row("Modèles/Entités", str(models_count))
        summary_table.add_row("Contrôleurs API", str(controllers_count))
        summary_table.add_row("Services métier", str(services_count))
        summary_table.add_row("Fichiers de tests", str(tests_count))
        summary_table.add_row("Taille totale", f"{size_mb:.2f} MB")
        
        console.print(summary_table)
        
        # Instructions pour utiliser le projet
        console.print(Panel.fit(
            "[bold green]🚀 Instructions d'utilisation :[/bold green]\n\n"
            f"1. Ouvrir Visual Studio 2015 ou version ultérieure\n"
            f"2. Fichier → Ouvrir → Projet/Solution\n"
            f"3. Sélectionner '{self.project_name}.sln'\n"
            f"4. Clic droit sur la solution → 'Restore NuGet Packages'\n"
            f"5. Build → Build Solution (Ctrl+Shift+B)\n"
            f"6. Debug → Start Debugging (F5)\n\n"
            f"[yellow]💡 Une fois démarré :[/yellow]\n"
            f"• API accessible à : http://localhost:[port]/api/\n"
            f"• Documentation Swagger : http://localhost:[port]/swagger\n"
            f"• Tests : Test → Run All Tests",
            title="[bold blue]Guide de démarrage[/bold blue]"
        ))


def main():
    """Fonction principale"""
    import sys
    
    if len(sys.argv) != 2:
        console.print("[red]❌ Usage : python project_inspector.py <chemin_du_projet>[/red]")
        console.print("[yellow]💡 Exemple : python project_inspector.py ./generated_projects/ECommerceAPI[/yellow]")
        return 1
    
    project_path = sys.argv[1]
    inspector = ProjectInspector(project_path)
    inspector.inspect_project()
    
    return 0


if __name__ == "__main__":
    try:
        from rich.console import Console
        from rich.table import Table
        from rich.panel import Panel
        from rich.tree import Tree
        
    except ImportError:
        print("❌ Module 'rich' requis. Installation : pip install rich")
        exit(1)
    
    exit(main())