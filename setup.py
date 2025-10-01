#!/usr/bin/env python3
"""
Script d'installation pour le générateur .NET amélioré
"""

import subprocess
import sys
import os
from pathlib import Path


def install_dependencies():
    """Installe les dépendances Python requises"""
    print("Installation des dependances Python...")

    requirements_file = Path(__file__).parent / "requirements.txt"

    try:
        subprocess.check_call([
            sys.executable, "-m", "pip", "install", "-r", str(requirements_file)
        ])
        print("Dependances installees avec succes !")
        return True

    except subprocess.CalledProcessError as e:
        print(f"Erreur lors de l'installation : {e}")
        print("Essayez d'installer manuellement :")
        print("   pip install rich inquirer")
        return False


def check_python_version():
    """Verifie que Python 3.7+ est installe"""
    version = sys.version_info

    if version.major < 3 or (version.major == 3 and version.minor < 7):
        print(f"Python 3.7+ requis. Version actuelle : {version.major}.{version.minor}")
        return False

    print(f"Python {version.major}.{version.minor}.{version.micro} detecte")
    return True


def make_scripts_executable():
    """Rend les scripts Python executables sur Unix/Linux"""
    if os.name != 'nt':  # Pas Windows
        scripts = [
            "enhanced_dotnet_generator.py",
            "dotnet_generator_cli.py",
            "generate_example.py",
            "demo_simple.py"
        ]

        for script in scripts:
            script_path = Path(__file__).parent / script
            if script_path.exists():
                os.chmod(script_path, 0o755)
                print(f"{script} rendu executable")


def show_usage_info():
    """Affiche les informations d'utilisation"""
    print("\nInstallation terminee !")
    print("=" * 50)

    print("\nScripts disponibles :")
    print("   • python dotnet_generator_cli.py    - CLI interactif")
    print("   • python generate_example.py        - Templates predefinis")
    print("   • python demo_simple.py             - Demonstration simple")

    print("\nUtilisation rapide :")
    print("   1. Lancer le CLI interactif :")
    print("      python dotnet_generator_cli.py")
    print("")
    print("   2. Generer des exemples complets :")
    print("      python generate_example.py")
    print("")
    print("   3. Test rapide avec demonstration :")
    print("      python demo_simple.py")

    print("\nDocumentation complete dans README.md")


def main():
    """Fonction principale d'installation"""
    print("Configuration du generateur .NET ameliore")
    print("=" * 60)

    # Verifier Python
    if not check_python_version():
        return 1

    # Installer les dependances
    if not install_dependencies():
        return 1

    # Rendre les scripts executables
    make_scripts_executable()

    # Afficher les informations d'utilisation
    show_usage_info()

    return 0


if __name__ == "__main__":
    sys.exit(main())
