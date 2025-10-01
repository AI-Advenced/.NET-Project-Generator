#!/usr/bin/env python3
"""
Démonstration simple du générateur .NET amélioré
Génère un projet Web API basique avec quelques entités
"""

from enhanced_dotnet_generator import *


def create_simple_demo():
    """Crée un projet de démonstration simple"""
    print("🚀 Génération d'un projet .NET Web API de démonstration")
    print("=" * 60)
    
    # Configuration d'entités simples
    user_entity = EntityConfig(
        name="User",
        properties=[
            PropertyConfig("Id", "int", is_required=True, is_key=True),
            PropertyConfig("Email", "string", is_required=True, max_length=100),
            PropertyConfig("FirstName", "string", is_required=True, max_length=50),
            PropertyConfig("LastName", "string", is_required=True, max_length=50),
            PropertyConfig("CreatedDate", "DateTime", is_required=True),
            PropertyConfig("IsActive", "bool", is_required=True)
        ]
    )
    
    task_entity = EntityConfig(
        name="Task",
        properties=[
            PropertyConfig("Id", "int", is_required=True, is_key=True),
            PropertyConfig("Title", "string", is_required=True, max_length=200),
            PropertyConfig("Description", "string", max_length=1000),
            PropertyConfig("DueDate", "DateTime"),
            PropertyConfig("IsCompleted", "bool", is_required=True),
            PropertyConfig("Priority", "string", is_required=True, max_length=20),
            PropertyConfig("UserId", "int", is_required=True, foreign_table="User"),
            PropertyConfig("CreatedDate", "DateTime", is_required=True)
        ]
    )
    
    # Configuration du projet
    config = ProjectConfig(
        project_name="TaskManagerAPI",
        project_type=ProjectType.WEBAPI,
        output_path="./demo_output",
        include_database=True,
        database_provider=DatabaseProvider.SQLSERVER,
        connection_string="Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=TaskManagerDb;Integrated Security=True",
        entities=[user_entity, task_entity],
        include_swagger=True,
        include_cors=True,
        include_authentication=False,
        include_tests=True
    )
    
    # Génération du projet
    generator = EnhancedDotNetGenerator()
    result = generator.generate_project(config)
    
    # Affichage du résultat
    if result['success']:
        print(f"\n✅ Projet '{config.project_name}' généré avec succès !")
        print(f"📁 Emplacement : {result['project_path']}")
        print(f"📄 Fichiers créés : {len(result['files_created'])}")
        
        print(f"\n📊 Détails du projet :")
        print(f"   • Framework : .NET Framework 4.8")
        print(f"   • Type : Web API")
        print(f"   • Base de données : SQL Server")
        print(f"   • Entités : User, Task")
        print(f"   • Swagger : Activé")
        print(f"   • CORS : Activé")
        print(f"   • Tests unitaires : Inclus")
        
        print(f"\n🌐 Endpoints API générés :")
        print(f"   User API :")
        print(f"     • GET    /api/user")
        print(f"     • GET    /api/user/{{id}}")
        print(f"     • POST   /api/user")
        print(f"     • PUT    /api/user/{{id}}")
        print(f"     • DELETE /api/user/{{id}}")
        print(f"   ")
        print(f"   Task API :")
        print(f"     • GET    /api/task")
        print(f"     • GET    /api/task/{{id}}")
        print(f"     • POST   /api/task")
        print(f"     • PUT    /api/task/{{id}}")
        print(f"     • DELETE /api/task/{{id}}")
        
        print(f"\n🚀 Prochaines étapes :")
        print(f"   1. Ouvrir Visual Studio 2015+")
        print(f"   2. Ouvrir {result['project_path']}/TaskManagerAPI.sln")
        print(f"   3. Restaurer les packages NuGet")
        print(f"   4. Compiler et exécuter (F5)")
        print(f"   5. Accéder à Swagger : http://localhost:[port]/swagger")
        
        print(f"\n💡 Exemple d'utilisation de l'API :")
        print(f"   # Créer un utilisateur")
        print(f"   POST /api/user")
        print(f"   {{")
        print(f"     \"Email\": \"john.doe@example.com\",")
        print(f"     \"FirstName\": \"John\",")
        print(f"     \"LastName\": \"Doe\",")
        print(f"     \"CreatedDate\": \"2023-12-01T10:00:00\",")
        print(f"     \"IsActive\": true")
        print(f"   }}")
        print(f"   ")
        print(f"   # Créer une tâche")
        print(f"   POST /api/task")
        print(f"   {{")
        print(f"     \"Title\": \"Terminer le projet\",")
        print(f"     \"Description\": \"Finaliser tous les composants\",")
        print(f"     \"DueDate\": \"2023-12-15T17:00:00\",")
        print(f"     \"IsCompleted\": false,")
        print(f"     \"Priority\": \"High\",")
        print(f"     \"UserId\": 1,")
        print(f"     \"CreatedDate\": \"2023-12-01T10:30:00\"")
        print(f"   }}")
        
        print(f"\n📋 Fichiers importants générés :")
        important_files = [
            "TaskManagerAPI.sln",
            "src/TaskManagerAPI/TaskManagerAPI.csproj", 
            "src/TaskManagerAPI/Web.config",
            "src/TaskManagerAPI/Global.asax.cs",
            "src/TaskManagerAPI/Models/User.cs",
            "src/TaskManagerAPI/Models/Task.cs",
            "src/TaskManagerAPI/Controllers/UserController.cs",
            "src/TaskManagerAPI/Controllers/TaskController.cs",
            "src/TaskManagerAPI/Services/UserService.cs",
            "src/TaskManagerAPI/Services/TaskService.cs",
            "src/TaskManagerAPI/Data/TaskManagerAPIContext.cs"
        ]
        
        for file in important_files:
            print(f"   ✓ {file}")
        
    else:
        print(f"\n❌ Erreur lors de la génération : {result['error']}")
        return False
    
    return True


if __name__ == "__main__":
    try:
        success = create_simple_demo()
        
        if success:
            print(f"\n🎉 Démonstration terminée avec succès !")
            print(f"📂 Consultez le dossier './demo_output/TaskManagerAPI' pour voir le projet généré")
        else:
            print(f"\n❌ Échec de la démonstration")
            
    except Exception as e:
        print(f"\n💥 Erreur inattendue : {e}")
        import traceback
        traceback.print_exc()