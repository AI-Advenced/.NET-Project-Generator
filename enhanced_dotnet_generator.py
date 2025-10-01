#!/usr/bin/env python3
"""
Enhanced .NET Project Generator with Complete Implementation
Compatible with Visual Studio 2015 and modern versions
"""

import os
import uuid
from pathlib import Path
from typing import List, Dict, Any
from dataclasses import dataclass
from enum import Enum


class ProjectType(str, Enum):
    """Supported project types"""
    WEBAPI = "webapi"
    MVC = "mvc"
    CONSOLE = "console"


class DatabaseProvider(str, Enum):
    """Supported database providers"""
    SQLSERVER = "sqlserver"
    SQLITE = "sqlite"
    MYSQL = "mysql"
    POSTGRESQL = "postgresql"


@dataclass
class PropertyConfig:
    """Property configuration for entities"""
    name: str
    type: str
    is_required: bool = False
    is_key: bool = False
    max_length: int = None
    foreign_table: str = None


@dataclass
class EntityConfig:
    """Entity configuration"""
    name: str
    properties: List[PropertyConfig]
    table_name: str = None


@dataclass
class ProjectConfig:
    """Project configuration"""
    project_name: str
    project_type: ProjectType = ProjectType.WEBAPI
    target_framework: str = "net48"  # Compatible with VS 2015
    output_path: str = "."
    
    # Database settings
    include_database: bool = False
    database_provider: DatabaseProvider = DatabaseProvider.SQLSERVER
    connection_string: str = ""
    entities: List[EntityConfig] = None
    
    # Feature flags
    include_swagger: bool = True
    include_cors: bool = True
    include_authentication: bool = False
    include_tests: bool = True
    
    def __post_init__(self):
        if self.entities is None:
            self.entities = []


class EnhancedDotNetGenerator:
    """Enhanced .NET project generator with complete implementation"""
    
    def __init__(self):
        self.created_files = []
        
    def generate_project(self, config: ProjectConfig) -> dict:
        """Generate a complete .NET project"""
        try:
            project_path = Path(config.output_path) / config.project_name
            project_path.mkdir(parents=True, exist_ok=True)
            
            result = {
                'success': True,
                'project_path': str(project_path),
                'files_created': [],
                'message': ''
            }
            
            # Generate project structure
            self._create_project_structure(project_path, config)
            
            # Generate solution file
            self._generate_solution_file(project_path, config)
            
            # Generate main project files
            self._generate_main_project(project_path, config)
            
            # Generate test project if needed
            if config.include_tests:
                self._generate_test_project(project_path, config)
            
            # Generate additional files
            self._generate_additional_files(project_path, config)
            
            result['files_created'] = self.created_files
            result['message'] = f'Project {config.project_name} generated successfully!'
            
            return result
            
        except Exception as e:
            return {
                'success': False,
                'error': str(e),
                'files_created': self.created_files
            }
    
    def _create_project_structure(self, project_path: Path, config: ProjectConfig):
        """Create the project directory structure"""
        # Main project directories
        main_project = project_path / f"src/{config.project_name}"
        directories = [
            main_project / "Controllers",
            main_project / "Models",
            main_project / "Services",
            main_project / "Properties",
        ]
        
        if config.include_database:
            directories.extend([
                main_project / "Data",
                main_project / "Data/Configurations",
                main_project / "Migrations"
            ])
        
        # Test project directories
        if config.include_tests:
            test_project = project_path / f"tests/{config.project_name}.Tests"
            directories.extend([
                test_project,
                test_project / "Controllers",
                test_project / "Services",
                test_project / "Properties"
            ])
        
        # Create all directories
        for directory in directories:
            directory.mkdir(parents=True, exist_ok=True)
    
    def _generate_solution_file(self, project_path: Path, config: ProjectConfig):
        """Generate Visual Studio solution file"""
        main_project_guid = str(uuid.uuid4()).upper()
        test_project_guid = str(uuid.uuid4()).upper() if config.include_tests else None
        solution_guid = str(uuid.uuid4()).upper()
        
        # Solution content for VS 2015 compatibility
        solution_content = f'''Microsoft Visual Studio Solution File, Format Version 12.00
# Visual Studio 14
VisualStudioVersion = 14.0.25420.1
MinimumVisualStudioVersion = 10.0.40219.1
Project("{{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}}") = "{config.project_name}", "src\\{config.project_name}\\{config.project_name}.csproj", "{{{main_project_guid}}}"
EndProject'''

        if config.include_tests:
            solution_content += f'''
Project("{{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}}") = "{config.project_name}.Tests", "tests\\{config.project_name}.Tests\\{config.project_name}.Tests.csproj", "{{{test_project_guid}}}"
EndProject'''

        solution_content += f'''
Global
	GlobalSection(SolutionConfigurationPlatforms) = preSolution
		Debug|Any CPU = Debug|Any CPU
		Release|Any CPU = Release|Any CPU
	EndGlobalSection
	GlobalSection(ProjectConfigurationPlatforms) = postSolution
		{{{main_project_guid}}}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{{{main_project_guid}}}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{{{main_project_guid}}}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{{{main_project_guid}}}.Release|Any CPU.Build.0 = Release|Any CPU'''

        if config.include_tests:
            solution_content += f'''
		{{{test_project_guid}}}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{{{test_project_guid}}}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{{{test_project_guid}}}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{{{test_project_guid}}}.Release|Any CPU.Build.0 = Release|Any CPU'''

        solution_content += f'''
	EndGlobalSection
	GlobalSection(SolutionProperties) = preSolution
		HideSolutionNode = FALSE
	EndGlobalSection
	GlobalSection(ExtensibilityGlobals) = postSolution
		SolutionGuid = {{{solution_guid}}}
	EndGlobalSection
EndGlobal
'''
        
        solution_file = project_path / f"{config.project_name}.sln"
        self._write_file(solution_file, solution_content)
    
    def _generate_main_project(self, project_path: Path, config: ProjectConfig):
        """Generate the main project files"""
        main_project_path = project_path / f"src/{config.project_name}"
        
        # Generate project file (.csproj)
        self._generate_main_csproj(main_project_path, config)
        
        # Generate web.config for VS 2015 compatibility
        self._generate_web_config(main_project_path, config)
        
        # Generate Global.asax
        self._generate_global_asax(main_project_path, config)
        
        # Generate Startup.cs
        self._generate_startup(main_project_path, config)
        
        # Generate Models
        self._generate_models(main_project_path, config)
        
        # Generate Data Context
        if config.include_database:
            self._generate_data_context(main_project_path, config)
        
        # Generate Services
        self._generate_services(main_project_path, config)
        
        # Generate Controllers
        self._generate_controllers(main_project_path, config)
        
        # Generate AssemblyInfo
        self._generate_assembly_info(main_project_path, config)
        
        # Generate packages.config
        self._generate_packages_config(main_project_path, config)
    
    def _generate_main_csproj(self, project_path: Path, config: ProjectConfig):
        """Generate main project .csproj file for VS 2015"""
        project_guid = str(uuid.uuid4()).upper()
        
        csproj_content = f'''<?xml version="1.0" encoding="utf-8"?>
<Project ToolsVersion="14.0" DefaultTargets="Build" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
  <Import Project="$(MSBuildExtensionsPath)\\$(MSBuildToolsVersion)\\Microsoft.Common.props" Condition="Exists('$(MSBuildExtensionsPath)\\$(MSBuildToolsVersion)\\Microsoft.Common.props')" />
  <PropertyGroup>
    <Configuration Condition=" '$(Configuration)' == '' ">Debug</Configuration>
    <Platform Condition=" '$(Platform)' == '' ">AnyCPU</Platform>
    <ProductVersion>
    </ProductVersion>
    <SchemaVersion>2.0</SchemaVersion>
    <ProjectGuid>{{{project_guid}}}</ProjectGuid>
    <ProjectTypeGuids>{{349c5851-65df-11da-9384-00065b846f21}};{{fae04ec0-301f-11d3-bf4b-00c04f79efbc}}</ProjectTypeGuids>
    <OutputType>Library</OutputType>
    <AppDesignerFolder>Properties</AppDesignerFolder>
    <RootNamespace>{config.project_name}</RootNamespace>
    <AssemblyName>{config.project_name}</AssemblyName>
    <TargetFrameworkVersion>v4.8</TargetFrameworkVersion>
    <MvcBuildViews>false</MvcBuildViews>
    <UseIISExpress>true</UseIISExpress>
    <IISExpressSSLPort />
    <IISExpressAnonymousAuthentication />
    <IISExpressWindowsAuthentication />
    <IISExpressUseClassicPipelineMode />
    <UseGlobalApplicationHostFile />
  </PropertyGroup>
  <PropertyGroup Condition=" '$(Configuration)|$(Platform)' == 'Debug|AnyCPU' ">
    <DebugSymbols>true</DebugSymbols>
    <DebugType>full</DebugType>
    <Optimize>false</Optimize>
    <OutputPath>bin\\</OutputPath>
    <DefineConstants>DEBUG;TRACE</DefineConstants>
    <ErrorReport>prompt</ErrorReport>
    <WarningLevel>4</WarningLevel>
  </PropertyGroup>
  <PropertyGroup Condition=" '$(Configuration)|$(Platform)' == 'Release|AnyCPU' ">
    <DebugType>pdbonly</DebugType>
    <Optimize>true</Optimize>
    <OutputPath>bin\\</OutputPath>
    <DefineConstants>TRACE</DefineConstants>
    <ErrorReport>prompt</ErrorReport>
    <WarningLevel>4</WarningLevel>
  </PropertyGroup>
  <ItemGroup>
    <Reference Include="Microsoft.CSharp" />
    <Reference Include="System" />
    <Reference Include="System.Data" />
    <Reference Include="System.Drawing" />
    <Reference Include="System.Web.DynamicData" />
    <Reference Include="System.Web.Entity" />
    <Reference Include="System.Web.ApplicationServices" />
    <Reference Include="System.ComponentModel.DataAnnotations" />
    <Reference Include="System.Core" />
    <Reference Include="System.Data.DataSetExtensions" />
    <Reference Include="System.Xml.Linq" />
    <Reference Include="System.Web" />
    <Reference Include="System.Web.Extensions" />
    <Reference Include="System.Web.Abstractions" />
    <Reference Include="System.Web.Routing" />
    <Reference Include="System.Xml" />
    <Reference Include="System.Configuration" />
    <Reference Include="System.Web.Services" />
    <Reference Include="System.EnterpriseServices" />
    <Reference Include="Microsoft.Web.Infrastructure, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31BF3856AD364E35, processorArchitecture=MSIL">
      <Private>True</Private>
      <HintPath>packages\\Microsoft.Web.Infrastructure.1.0.0.0\\lib\\net40\\Microsoft.Web.Infrastructure.dll</HintPath>
    </Reference>
    <Reference Include="System.Net.Http">
    </Reference>
    <Reference Include="System.Net.Http.WebRequest">
    </Reference>
    <Reference Include="Newtonsoft.Json">
      <HintPath>packages\\Newtonsoft.Json.12.0.2\\lib\\net45\\Newtonsoft.Json.dll</HintPath>
    </Reference>
    <Reference Include="System.Web.Mvc, Version=5.2.7.0, Culture=neutral, PublicKeyToken=31BF3856AD364E35, processorArchitecture=MSIL">
      <Private>True</Private>
      <HintPath>packages\\Microsoft.AspNet.Mvc.5.2.7\\lib\\net45\\System.Web.Mvc.dll</HintPath>
    </Reference>
    <Reference Include="System.Web.Optimization">
      <HintPath>packages\\Microsoft.AspNet.Web.Optimization.1.1.3\\lib\\net40\\System.Web.Optimization.dll</HintPath>
    </Reference>
    <Reference Include="System.Web.Razor, Version=3.2.7.0, Culture=neutral, PublicKeyToken=31BF3856AD364E35, processorArchitecture=MSIL">
      <Private>True</Private>
      <HintPath>packages\\Microsoft.AspNet.Razor.3.2.7\\lib\\net45\\System.Web.Razor.dll</HintPath>
    </Reference>
    <Reference Include="System.Web.WebPages, Version=3.2.7.0, Culture=neutral, PublicKeyToken=31BF3856AD364E35, processorArchitecture=MSIL">
      <Private>True</Private>
      <HintPath>packages\\Microsoft.AspNet.WebPages.3.2.7\\lib\\net45\\System.Web.WebPages.dll</HintPath>
    </Reference>
    <Reference Include="System.Web.WebPages.Deployment, Version=3.2.7.0, Culture=neutral, PublicKeyToken=31BF3856AD364E35, processorArchitecture=MSIL">
      <Private>True</Private>
      <HintPath>packages\\Microsoft.AspNet.WebPages.3.2.7\\lib\\net45\\System.Web.WebPages.Deployment.dll</HintPath>
    </Reference>
    <Reference Include="System.Web.WebPages.Razor, Version=3.2.7.0, Culture=neutral, PublicKeyToken=31BF3856AD364E35, processorArchitecture=MSIL">
      <Private>True</Private>
      <HintPath>packages\\Microsoft.AspNet.WebPages.3.2.7\\lib\\net45\\System.Web.WebPages.Razor.dll</HintPath>
    </Reference>
    <Reference Include="System.Web.Http">
      <HintPath>packages\\Microsoft.AspNet.WebApi.Core.5.2.7\\lib\\net45\\System.Web.Http.dll</HintPath>
    </Reference>
    <Reference Include="System.Web.Http.WebHost">
      <HintPath>packages\\Microsoft.AspNet.WebApi.WebHost.5.2.7\\lib\\net45\\System.Web.Http.WebHost.dll</HintPath>
    </Reference>'''

        if config.include_database:
            csproj_content += f'''
    <Reference Include="EntityFramework">
      <HintPath>packages\\EntityFramework.6.4.4\\lib\\net45\\EntityFramework.dll</HintPath>
    </Reference>
    <Reference Include="EntityFramework.SqlServer">
      <HintPath>packages\\EntityFramework.6.4.4\\lib\\net45\\EntityFramework.SqlServer.dll</HintPath>
    </Reference>'''

        if config.include_swagger:
            csproj_content += f'''
    <Reference Include="Swashbuckle.Core">
      <HintPath>packages\\Swashbuckle.Core.5.6.0\\lib\\net40\\Swashbuckle.Core.dll</HintPath>
    </Reference>
    <Reference Include="WebActivatorEx">
      <HintPath>packages\\WebActivatorEx.2.2.0\\lib\\net40\\WebActivatorEx.dll</HintPath>
    </Reference>'''

        csproj_content += '''
  </ItemGroup>
  <ItemGroup>
    <Compile Include="Global.asax.cs">
      <DependentUpon>Global.asax</DependentUpon>
    </Compile>
    <Compile Include="Properties\\AssemblyInfo.cs" />
    <Compile Include="App_Start\\WebApiConfig.cs" />
    <Compile Include="App_Start\\FilterConfig.cs" />
    <Compile Include="App_Start\\RouteConfig.cs" />'''

        if config.include_swagger:
            csproj_content += '''
    <Compile Include="App_Start\\SwaggerConfig.cs" />'''

        if config.include_database:
            csproj_content += f'''
    <Compile Include="Data\\{config.project_name}Context.cs" />'''
            
            for entity in config.entities:
                csproj_content += f'''
    <Compile Include="Models\\{entity.name}.cs" />
    <Compile Include="Data\\Configurations\\{entity.name}Configuration.cs" />
    <Compile Include="Services\\{entity.name}Service.cs" />
    <Compile Include="Services\\I{entity.name}Service.cs" />
    <Compile Include="Controllers\\{entity.name}Controller.cs" />'''

        csproj_content += '''
  </ItemGroup>
  <ItemGroup>
    <Content Include="Global.asax" />
    <Content Include="Web.config" />
    <Content Include="Web.Debug.config">
      <DependentUpon>Web.config</DependentUpon>
    </Content>
    <Content Include="Web.Release.config">
      <DependentUpon>Web.config</DependentUpon>
    </Content>
  </ItemGroup>
  <ItemGroup>
    <Folder Include="App_Data\\" />
  </ItemGroup>
  <ItemGroup>
    <None Include="packages.config" />
  </ItemGroup>
  <PropertyGroup>
    <VisualStudioVersion Condition="'$(VisualStudioVersion)' == ''">10.0</VisualStudioVersion>
    <VSToolsPath Condition="'$(VSToolsPath)' == ''">$(MSBuildExtensionsPath32)\\Microsoft\\VisualStudio\\v$(VisualStudioVersion)</VSToolsPath>
  </PropertyGroup>
  <Import Project="$(MSBuildBinPath)\\Microsoft.CSharp.targets" />
  <Import Project="$(VSToolsPath)\\WebApplications\\Microsoft.WebApplication.targets" Condition="'$(VSToolsPath)' != ''" />
  <Import Project="$(MSBuildExtensionsPath32)\\Microsoft\\VisualStudio\\v10.0\\WebApplications\\Microsoft.WebApplication.targets" Condition="false" />
  <Target Name="MvcBuildViews" AfterTargets="AfterBuild" Condition="'$(MvcBuildViews)'=='true'">
    <AspNetCompiler VirtualPath="temp" PhysicalPath="$(WebProjectOutputDir)" />
  </Target>
  <ProjectExtensions>
    <VisualStudio>
      <FlavorProperties GUID="{349c5851-65df-11da-9384-00065b846f21}">
        <WebProjectProperties>
          <UseIIS>True</UseIIS>
          <AutoAssignPort>True</AutoAssignPort>
          <DevelopmentServerPort>0</DevelopmentServerPort>
          <DevelopmentServerVPath>/</DevelopmentServerVPath>
          <IISUrl>http://localhost:''' + str(hash(config.project_name) % 10000 + 50000) + f'''</IISUrl>
          <NTLMAuthentication>False</NTLMAuthentication>
          <UseCustomServer>False</UseCustomServer>
          <CustomServerUrl>
          </CustomServerUrl>
          <SaveServerSettingsInUserFile>False</SaveServerSettingsInUserFile>
        </WebProjectProperties>
      </FlavorProperties>
    </VisualStudio>
  </ProjectExtensions>
</Project>'''

        self._write_file(project_path / f"{config.project_name}.csproj", csproj_content)
    
    def _generate_web_config(self, project_path: Path, config: ProjectConfig):
        """Generate web.config file"""
        web_config_content = '''<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <configSections>
    <section name="entityFramework" type="System.Data.Entity.Internal.ConfigFile.EntityFrameworkSection, EntityFramework, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" requirePermission="false" />
  </configSections>
  <connectionStrings>'''

        if config.include_database:
            if config.database_provider == DatabaseProvider.SQLSERVER:
                conn_str = config.connection_string or f"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog={config.project_name}Db;Integrated Security=True"
            elif config.database_provider == DatabaseProvider.SQLITE:
                conn_str = config.connection_string or f"Data Source={config.project_name}.db"
            else:
                conn_str = config.connection_string or f"Server=localhost;Database={config.project_name}Db;Trusted_Connection=true;"
            
            web_config_content += f'''
    <add name="DefaultConnection" connectionString="{conn_str}" providerName="System.Data.SqlClient" />'''

        web_config_content += '''
  </connectionStrings>
  <appSettings>
    <add key="webpages:Version" value="3.0.0.0" />
    <add key="webpages:Enabled" value="false" />
    <add key="ClientValidationEnabled" value="true" />
    <add key="UnobtrusiveJavaScriptEnabled" value="true" />
  </appSettings>
  <system.web>
    <authentication mode="None" />
    <compilation debug="true" targetFramework="4.8" />
    <httpRuntime targetFramework="4.8" maxRequestLength="4096" />
  </system.web>
  <system.webServer>
    <handlers>
      <remove name="ExtensionlessUrlHandler-Integrated-4.0" />
      <remove name="OPTIONSVerbHandler" />
      <remove name="TRACEVerbHandler" />
      <add name="ExtensionlessUrlHandler-Integrated-4.0" path="*." verb="*" type="System.Web.Handlers.TransferRequestHandler" preCondition="integratedMode,runtimeVersionv4.0" />
    </handlers>
  </system.webServer>
  <runtime>
    <assemblyBinding xmlns="urn:schemas-microsoft-com:asm.v1">
      <dependentAssembly>
        <assemblyIdentity name="Newtonsoft.Json" culture="neutral" publicKeyToken="30ad4fe6b2a6aeed" />
        <bindingRedirect oldVersion="0.0.0.0-12.0.0.0" newVersion="12.0.0.0" />
      </dependentAssembly>
      <dependentAssembly>
        <assemblyIdentity name="System.Web.Optimization" publicKeyToken="31bf3856ad364e35" />
        <bindingRedirect oldVersion="1.0.0.0-1.1.0.0" newVersion="1.1.0.0" />
      </dependentAssembly>
      <dependentAssembly>
        <assemblyIdentity name="WebGrease" publicKeyToken="31bf3856ad364e35" />
        <bindingRedirect oldVersion="0.0.0.0-1.5.2.14234" newVersion="1.5.2.14234" />
      </dependentAssembly>
      <dependentAssembly>
        <assemblyIdentity name="System.Web.Helpers" publicKeyToken="31bf3856ad364e35" />
        <bindingRedirect oldVersion="1.0.0.0-3.0.0.0" newVersion="3.0.0.0" />
      </dependentAssembly>
      <dependentAssembly>
        <assemblyIdentity name="System.Web.WebPages" publicKeyToken="31bf3856ad364e35" />
        <bindingRedirect oldVersion="1.0.0.0-3.0.0.0" newVersion="3.0.0.0" />
      </dependentAssembly>
      <dependentAssembly>
        <assemblyIdentity name="System.Web.Mvc" publicKeyToken="31bf3856ad364e35" />
        <bindingRedirect oldVersion="1.0.0.0-5.2.7.0" newVersion="5.2.7.0" />
      </dependentAssembly>
    </assemblyBinding>
  </runtime>'''

        if config.include_database:
            web_config_content += '''
  <entityFramework>
    <defaultConnectionFactory type="System.Data.Entity.Infrastructure.LocalDbConnectionFactory, EntityFramework">
      <parameters>
        <parameter value="mssqllocaldb" />
      </parameters>
    </defaultConnectionFactory>
    <providers>
      <provider invariantName="System.Data.SqlClient" type="System.Data.Entity.SqlServer.SqlProviderServices, EntityFramework.SqlServer" />
    </providers>
  </entityFramework>'''

        web_config_content += '''
</configuration>'''

        self._write_file(project_path / "Web.config", web_config_content)
        
        # Generate Web.Debug.config
        debug_config = '''<?xml version="1.0" encoding="utf-8"?>
<configuration xmlns:xdt="http://schemas.microsoft.com/XML-Document-Transform">
  <system.web>
    <compilation xdt:Transform="RemoveAttributes(debug)" />
  </system.web>
</configuration>'''
        self._write_file(project_path / "Web.Debug.config", debug_config)
        
        # Generate Web.Release.config
        release_config = '''<?xml version="1.0" encoding="utf-8"?>
<configuration xmlns:xdt="http://schemas.microsoft.com/XML-Document-Transform">
  <system.web>
    <compilation xdt:Transform="RemoveAttributes(debug)" />
  </system.web>
</configuration>'''
        self._write_file(project_path / "Web.Release.config", release_config)
    
    def _generate_global_asax(self, project_path: Path, config: ProjectConfig):
        """Generate Global.asax file"""
        global_asax_content = f'''<%@ Application Codebehind="Global.asax.cs" Inherits="{config.project_name}.MvcApplication" Language="C#" %>'''
        self._write_file(project_path / "Global.asax", global_asax_content)
        
        # Generate Global.asax.cs
        global_asax_cs_content = f'''using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace {config.project_name}
{{
    public class MvcApplication : System.Web.HttpApplication
    {{
        protected void Application_Start()
        {{
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }}
    }}
}}'''
        
        self._write_file(project_path / "Global.asax.cs", global_asax_cs_content)
    
    def _generate_startup(self, project_path: Path, config: ProjectConfig):
        """Generate App_Start configuration files"""
        app_start_path = project_path / "App_Start"
        app_start_path.mkdir(exist_ok=True)
        
        # WebApiConfig.cs
        webapi_config = f'''using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace {config.project_name}
{{
    public static class WebApiConfig
    {{
        public static void Register(HttpConfiguration config)
        {{
            // Web API configuration and services
            
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{{controller}}/{{id}}",
                defaults: new {{ id = RouteParameter.Optional }}
            );
            
            // Configure JSON serialization
            var settings = config.Formatters.JsonFormatter.SerializerSettings;
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            settings.Formatting = Formatting.Indented;'''

        if config.include_cors:
            webapi_config += '''
            
            // Enable CORS
            config.EnableCors();'''

        webapi_config += '''
        }
    }
}'''
        self._write_file(app_start_path / "WebApiConfig.cs", webapi_config)
        
        # FilterConfig.cs
        filter_config = f'''using System.Web.Mvc;

namespace {config.project_name}
{{
    public class FilterConfig
    {{
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {{
            filters.Add(new HandleErrorAttribute());
        }}
    }}
}}'''
        self._write_file(app_start_path / "FilterConfig.cs", filter_config)
        
        # RouteConfig.cs
        route_config = f'''using System.Web.Mvc;
using System.Web.Routing;

namespace {config.project_name}
{{
    public class RouteConfig
    {{
        public static void RegisterRoutes(RouteCollection routes)
        {{
            routes.IgnoreRoute("{{resource}}.axd/{{*pathInfo}}");

            routes.MapRoute(
                name: "Default",
                url: "{{controller}}/{{action}}/{{id}}",
                defaults: new {{ controller = "Home", action = "Index", id = UrlParameter.Optional }}
            );
        }}
    }}
}}'''
        self._write_file(app_start_path / "RouteConfig.cs", route_config)
        
        # SwaggerConfig.cs (if Swagger is enabled)
        if config.include_swagger:
            swagger_config = f'''using System.Web.Http;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof({config.project_name}.SwaggerConfig), "Register")]

namespace {config.project_name}
{{
    public class SwaggerConfig
    {{
        public static void Register()
        {{
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration 
                .EnableSwagger(c =>
                {{
                    c.SingleApiVersion("v1", "{config.project_name}");
                    c.DescribeAllEnumsAsStrings();
                }})
                .EnableSwaggerUi(c =>
                {{
                    c.DocumentTitle("API Documentation");
                }});
        }}
    }}
}}'''
            self._write_file(app_start_path / "SwaggerConfig.cs", swagger_config)
    
    def _generate_models(self, project_path: Path, config: ProjectConfig):
        """Generate model classes"""
        models_path = project_path / "Models"
        
        for entity in config.entities:
            model_content = f'''using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace {config.project_name}.Models
{{
    public class {entity.name}
    {{'''
            
            for prop in entity.properties:
                # Add data annotations
                if prop.is_key:
                    model_content += f'''
        [Key]'''
                
                if prop.is_required:
                    model_content += f'''
        [Required]'''
                
                if prop.max_length:
                    model_content += f'''
        [MaxLength({prop.max_length})]'''
                
                if prop.foreign_table:
                    model_content += f'''
        [ForeignKey("{prop.foreign_table}")]'''
                
                # Add property
                nullable = "?" if not prop.is_required and prop.type in ["int", "DateTime", "bool", "decimal"] else ""
                model_content += f'''
        public {prop.type}{nullable} {prop.name} {{ get; set; }}'''
            
            model_content += '''
    }
}'''
            
            self._write_file(models_path / f"{entity.name}.cs", model_content)
    
    def _generate_data_context(self, project_path: Path, config: ProjectConfig):
        """Generate Entity Framework data context"""
        data_path = project_path / "Data"
        
        # DbContext
        context_content = f'''using System.Data.Entity;
using {config.project_name}.Models;

namespace {config.project_name}.Data
{{
    public class {config.project_name}Context : DbContext
    {{
        public {config.project_name}Context() : base("DefaultConnection")
        {{
        }}'''
        
        for entity in config.entities:
            context_content += f'''
        
        public DbSet<{entity.name}> {entity.name}s {{ get; set; }}'''
        
        context_content += f'''
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {{
            base.OnModelCreating(modelBuilder);'''
        
        for entity in config.entities:
            context_content += f'''
            
            modelBuilder.Configurations.Add(new Configurations.{entity.name}Configuration());'''
        
        context_content += '''
        }
    }
}'''
        
        self._write_file(data_path / f"{config.project_name}Context.cs", context_content)
        
        # Generate Entity Configurations
        configurations_path = data_path / "Configurations"
        
        for entity in config.entities:
            config_content = f'''using System.Data.Entity.ModelConfiguration;
using {config.project_name}.Models;

namespace {config.project_name}.Data.Configurations
{{
    public class {entity.name}Configuration : EntityTypeConfiguration<{entity.name}>
    {{
        public {entity.name}Configuration()
        {{
            // Table name
            ToTable("{entity.table_name or entity.name + 's'}");
            
            // Primary key
            HasKey(x => x.{next((p.name for p in entity.properties if p.is_key), "Id")});'''
            
            for prop in entity.properties:
                if prop.max_length:
                    config_content += f'''
            
            // {prop.name} configuration
            Property(x => x.{prop.name}).HasMaxLength({prop.max_length});'''
                
                if prop.is_required:
                    config_content += f'''
            Property(x => x.{prop.name}).IsRequired();'''
            
            config_content += '''
        }
    }
}'''
            
            self._write_file(configurations_path / f"{entity.name}Configuration.cs", config_content)
    
    def _generate_services(self, project_path: Path, config: ProjectConfig):
        """Generate service layer"""
        services_path = project_path / "Services"
        
        for entity in config.entities:
            # Interface
            interface_content = f'''using System;
using System.Collections.Generic;
using {config.project_name}.Models;

namespace {config.project_name}.Services
{{
    public interface I{entity.name}Service
    {{
        IEnumerable<{entity.name}> GetAll();
        {entity.name} GetById(int id);
        {entity.name} Create({entity.name} {entity.name.lower()});
        {entity.name} Update({entity.name} {entity.name.lower()});
        bool Delete(int id);
    }}
}}'''
            
            self._write_file(services_path / f"I{entity.name}Service.cs", interface_content)
            
            # Implementation
            service_content = f'''using System;
using System.Collections.Generic;
using System.Linq;
using {config.project_name}.Data;
using {config.project_name}.Models;

namespace {config.project_name}.Services
{{
    public class {entity.name}Service : I{entity.name}Service
    {{
        private readonly {config.project_name}Context _context;

        public {entity.name}Service({config.project_name}Context context)
        {{
            _context = context;
        }}

        public IEnumerable<{entity.name}> GetAll()
        {{
            return _context.{entity.name}s.ToList();
        }}

        public {entity.name} GetById(int id)
        {{
            return _context.{entity.name}s.Find(id);
        }}

        public {entity.name} Create({entity.name} {entity.name.lower()})
        {{
            _context.{entity.name}s.Add({entity.name.lower()});
            _context.SaveChanges();
            return {entity.name.lower()};
        }}

        public {entity.name} Update({entity.name} {entity.name.lower()})
        {{
            _context.Entry({entity.name.lower()}).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
            return {entity.name.lower()};
        }}

        public bool Delete(int id)
        {{
            var {entity.name.lower()} = _context.{entity.name}s.Find(id);
            if ({entity.name.lower()} == null)
                return false;

            _context.{entity.name}s.Remove({entity.name.lower()});
            _context.SaveChanges();
            return true;
        }}
    }}
}}'''
            
            self._write_file(services_path / f"{entity.name}Service.cs", service_content)
    
    def _generate_controllers(self, project_path: Path, config: ProjectConfig):
        """Generate Web API controllers"""
        controllers_path = project_path / "Controllers"
        
        for entity in config.entities:
            controller_content = f'''using System;
using System.Collections.Generic;
using System.Web.Http;'''

            if config.include_cors:
                controller_content += f'''
using System.Web.Http.Cors;'''

            controller_content += f'''
using {config.project_name}.Models;
using {config.project_name}.Services;

namespace {config.project_name}.Controllers
{{'''

            if config.include_cors:
                controller_content += f'''
    [EnableCors(origins: "*", headers: "*", methods: "*")]'''

            controller_content += f'''
    [RoutePrefix("api/{entity.name.lower()}")]
    public class {entity.name}Controller : ApiController
    {{
        private readonly I{entity.name}Service _{entity.name.lower()}Service;

        public {entity.name}Controller(I{entity.name}Service {entity.name.lower()}Service)
        {{
            _{entity.name.lower()}Service = {entity.name.lower()}Service;
        }}

        // GET api/{entity.name.lower()}
        [HttpGet]
        [Route("")]
        public IHttpActionResult Get()
        {{
            try
            {{
                var {entity.name.lower()}s = _{entity.name.lower()}Service.GetAll();
                return Ok({entity.name.lower()}s);
            }}
            catch (Exception ex)
            {{
                return InternalServerError(ex);
            }}
        }}

        // GET api/{entity.name.lower()}/5
        [HttpGet]
        [Route("{{id:int}}")]
        public IHttpActionResult Get(int id)
        {{
            try
            {{
                var {entity.name.lower()} = _{entity.name.lower()}Service.GetById(id);
                if ({entity.name.lower()} == null)
                    return NotFound();

                return Ok({entity.name.lower()});
            }}
            catch (Exception ex)
            {{
                return InternalServerError(ex);
            }}
        }}

        // POST api/{entity.name.lower()}
        [HttpPost]
        [Route("")]
        public IHttpActionResult Post([FromBody]{entity.name} {entity.name.lower()})
        {{
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {{
                var created{entity.name} = _{entity.name.lower()}Service.Create({entity.name.lower()});
                return Created($"api/{entity.name.lower()}/{{created{entity.name}.{next((p.name for p in entity.properties if p.is_key), "Id")}}}", created{entity.name});
            }}
            catch (Exception ex)
            {{
                return InternalServerError(ex);
            }}
        }}

        // PUT api/{entity.name.lower()}/5
        [HttpPut]
        [Route("{{id:int}}")]
        public IHttpActionResult Put(int id, [FromBody]{entity.name} {entity.name.lower()})
        {{
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {{
                var existing{entity.name} = _{entity.name.lower()}Service.GetById(id);
                if (existing{entity.name} == null)
                    return NotFound();

                var updated{entity.name} = _{entity.name.lower()}Service.Update({entity.name.lower()});
                return Ok(updated{entity.name});
            }}
            catch (Exception ex)
            {{
                return InternalServerError(ex);
            }}
        }}

        // DELETE api/{entity.name.lower()}/5
        [HttpDelete]
        [Route("{{id:int}}")]
        public IHttpActionResult Delete(int id)
        {{
            try
            {{
                var result = _{entity.name.lower()}Service.Delete(id);
                if (!result)
                    return NotFound();

                return Ok();
            }}
            catch (Exception ex)
            {{
                return InternalServerError(ex);
            }}
        }}
    }}
}}'''
            
            self._write_file(controllers_path / f"{entity.name}Controller.cs", controller_content)
    
    def _generate_assembly_info(self, project_path: Path, config: ProjectConfig):
        """Generate AssemblyInfo.cs"""
        properties_path = project_path / "Properties"
        
        assembly_info = f'''using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("{config.project_name}")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("{config.project_name}")]
[assembly: AssemblyCopyright("Copyright ©  2023")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("{str(uuid.uuid4()).lower()}")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Revision and Build Numbers 
// by using the '*' as shown below:
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]'''
        
        self._write_file(properties_path / "AssemblyInfo.cs", assembly_info)
    
    def _generate_packages_config(self, project_path: Path, config: ProjectConfig):
        """Generate packages.config for NuGet packages"""
        packages_content = '''<?xml version="1.0" encoding="utf-8"?>
<packages>
  <package id="Microsoft.AspNet.Mvc" version="5.2.7" targetFramework="net48" />
  <package id="Microsoft.AspNet.Razor" version="3.2.7" targetFramework="net48" />
  <package id="Microsoft.AspNet.Web.Optimization" version="1.1.3" targetFramework="net48" />
  <package id="Microsoft.AspNet.WebApi" version="5.2.7" targetFramework="net48" />
  <package id="Microsoft.AspNet.WebApi.Client" version="5.2.7" targetFramework="net48" />
  <package id="Microsoft.AspNet.WebApi.Core" version="5.2.7" targetFramework="net48" />
  <package id="Microsoft.AspNet.WebApi.WebHost" version="5.2.7" targetFramework="net48" />
  <package id="Microsoft.AspNet.WebPages" version="3.2.7" targetFramework="net48" />
  <package id="Microsoft.Web.Infrastructure" version="1.0.0.0" targetFramework="net48" />
  <package id="Newtonsoft.Json" version="12.0.2" targetFramework="net48" />
  <package id="WebGrease" version="1.5.2" targetFramework="net48" />'''

        if config.include_database:
            packages_content += '''
  <package id="EntityFramework" version="6.4.4" targetFramework="net48" />'''

        if config.include_swagger:
            packages_content += '''
  <package id="Swashbuckle" version="5.6.0" targetFramework="net48" />
  <package id="Swashbuckle.Core" version="5.6.0" targetFramework="net48" />
  <package id="WebActivatorEx" version="2.2.0" targetFramework="net48" />'''

        if config.include_cors:
            packages_content += '''
  <package id="Microsoft.AspNet.WebApi.Cors" version="5.2.7" targetFramework="net48" />'''

        packages_content += '''
</packages>'''
        
        self._write_file(project_path / "packages.config", packages_content)
    
    def _generate_test_project(self, project_path: Path, config: ProjectConfig):
        """Generate test project files"""
        test_project_path = project_path / f"tests/{config.project_name}.Tests"
        
        # Generate test project .csproj
        test_guid = str(uuid.uuid4()).upper()
        
        test_csproj = f'''<?xml version="1.0" encoding="utf-8"?>
<Project ToolsVersion="14.0" DefaultTargets="Build" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
  <PropertyGroup>
    <Configuration Condition=" '$(Configuration)' == '' ">Debug</Configuration>
    <Platform Condition=" '$(Platform)' == '' ">AnyCPU</Platform>
    <ProjectGuid>{{{test_guid}}}</ProjectGuid>
    <OutputType>Library</OutputType>
    <AppDesignerFolder>Properties</AppDesignerFolder>
    <RootNamespace>{config.project_name}.Tests</RootNamespace>
    <AssemblyName>{config.project_name}.Tests</AssemblyName>
    <TargetFrameworkVersion>v4.8</TargetFrameworkVersion>
    <FileAlignment>512</FileAlignment>
    <ProjectTypeGuids>{{3AC096D0-A1C2-E12C-1390-A8335801FDAB}};{{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}}</ProjectTypeGuids>
  </PropertyGroup>
  <PropertyGroup Condition=" '$(Configuration)|$(Platform)' == 'Debug|AnyCPU' ">
    <DebugSymbols>true</DebugSymbols>
    <DebugType>full</DebugType>
    <Optimize>false</Optimize>
    <OutputPath>bin\\Debug\\</OutputPath>
    <DefineConstants>DEBUG;TRACE</DefineConstants>
    <ErrorReport>prompt</ErrorReport>
    <WarningLevel>4</WarningLevel>
  </PropertyGroup>
  <PropertyGroup Condition=" '$(Configuration)|$(Platform)' == 'Release|AnyCPU' ">
    <DebugType>pdbonly</DebugType>
    <Optimize>true</Optimize>
    <OutputPath>bin\\Release\\</OutputPath>
    <DefineConstants>TRACE</DefineConstants>
    <ErrorReport>prompt</ErrorReport>
    <WarningLevel>4</WarningLevel>
  </PropertyGroup>
  <ItemGroup>
    <Reference Include="System" />
    <Reference Include="Microsoft.VisualStudio.TestPlatform.TestFramework, Version=14.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a, processorArchitecture=MSIL">
      <HintPath>..\\..\\packages\\MSTest.TestFramework.2.1.2\\lib\\net45\\Microsoft.VisualStudio.TestPlatform.TestFramework.dll</HintPath>
    </Reference>
    <Reference Include="Microsoft.VisualStudio.TestPlatform.TestFramework.Extensions, Version=14.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a, processorArchitecture=MSIL">
      <HintPath>..\\..\\packages\\MSTest.TestFramework.2.1.2\\lib\\net45\\Microsoft.VisualStudio.TestPlatform.TestFramework.Extensions.dll</HintPath>
    </Reference>
    <Reference Include="System.Core" />
  </ItemGroup>
  <ItemGroup>
    <Compile Include="Properties\\AssemblyInfo.cs" />'''

        # Add test files for each entity
        for entity in config.entities:
            test_csproj += f'''
    <Compile Include="Services\\{entity.name}ServiceTests.cs" />
    <Compile Include="Controllers\\{entity.name}ControllerTests.cs" />'''

        test_csproj += f'''
  </ItemGroup>
  <ItemGroup>
    <ProjectReference Include="..\\..\\src\\{config.project_name}\\{config.project_name}.csproj">
      <Project>{{{str(uuid.uuid4()).upper()}}}</Project>
      <Name>{config.project_name}</Name>
    </ProjectReference>
  </ItemGroup>
  <ItemGroup>
    <None Include="packages.config" />
  </ItemGroup>
  <Import Project="$(VSToolsPath)\\TeamTest\\Microsoft.TestTools.targets" Condition="Exists('$(VSToolsPath)\\TeamTest\\Microsoft.TestTools.targets')" />
  <Import Project="$(MSBuildToolsPath)\\Microsoft.CSharp.targets" />
</Project>'''
        
        self._write_file(test_project_path / f"{config.project_name}.Tests.csproj", test_csproj)
        
        # Generate test AssemblyInfo
        test_assembly_info = f'''using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

[assembly: AssemblyTitle("{config.project_name}.Tests")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("{config.project_name}.Tests")]
[assembly: AssemblyCopyright("Copyright ©  2023")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

[assembly: ComVisible(false)]

[assembly: Guid("{str(uuid.uuid4()).lower()}")]

[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]'''
        
        self._write_file(test_project_path / "Properties/AssemblyInfo.cs", test_assembly_info)
        
        # Generate test packages.config
        test_packages = '''<?xml version="1.0" encoding="utf-8"?>
<packages>
  <package id="MSTest.TestAdapter" version="2.1.2" targetFramework="net48" />
  <package id="MSTest.TestFramework" version="2.1.2" targetFramework="net48" />
</packages>'''
        
        self._write_file(test_project_path / "packages.config", test_packages)
        
        # Generate test files for services and controllers
        for entity in config.entities:
            self._generate_service_tests(test_project_path, config, entity)
            self._generate_controller_tests(test_project_path, config, entity)
    
    def _generate_service_tests(self, test_project_path: Path, config: ProjectConfig, entity: EntityConfig):
        """Generate service test files"""
        test_content = f'''using Microsoft.VisualStudio.TestTools.UnitTesting;
using {config.project_name}.Data;
using {config.project_name}.Models;
using {config.project_name}.Services;
using System.Linq;

namespace {config.project_name}.Tests.Services
{{
    [TestClass]
    public class {entity.name}ServiceTests
    {{
        private {config.project_name}Context _context;
        private {entity.name}Service _service;

        [TestInitialize]
        public void Setup()
        {{
            // Use in-memory database for testing
            _context = new {config.project_name}Context();
            _service = new {entity.name}Service(_context);
        }}

        [TestCleanup]
        public void Cleanup()
        {{
            _context?.Dispose();
        }}

        [TestMethod]
        public void GetAll_ReturnsAll{entity.name}s()
        {{
            // Arrange
            var {entity.name.lower()}1 = new {entity.name} {{ /* Set properties */ }};
            var {entity.name.lower()}2 = new {entity.name} {{ /* Set properties */ }};
            
            _context.{entity.name}s.Add({entity.name.lower()}1);
            _context.{entity.name}s.Add({entity.name.lower()}2);
            _context.SaveChanges();

            // Act
            var result = _service.GetAll();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }}

        [TestMethod]
        public void GetById_ReturnsCorrect{entity.name}()
        {{
            // Arrange
            var {entity.name.lower()} = new {entity.name} {{ /* Set properties */ }};
            _context.{entity.name}s.Add({entity.name.lower()});
            _context.SaveChanges();

            // Act
            var result = _service.GetById({entity.name.lower()}.{next((p.name for p in entity.properties if p.is_key), "Id")});

            // Assert
            Assert.IsNotNull(result);
        }}

        [TestMethod]
        public void Create_Adds{entity.name}Successfully()
        {{
            // Arrange
            var {entity.name.lower()} = new {entity.name} {{ /* Set properties */ }};

            // Act
            var result = _service.Create({entity.name.lower()});

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, _context.{entity.name}s.Count());
        }}

        [TestMethod]
        public void Delete_Removes{entity.name}Successfully()
        {{
            // Arrange
            var {entity.name.lower()} = new {entity.name} {{ /* Set properties */ }};
            _context.{entity.name}s.Add({entity.name.lower()});
            _context.SaveChanges();

            // Act
            var result = _service.Delete({entity.name.lower()}.{next((p.name for p in entity.properties if p.is_key), "Id")});

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(0, _context.{entity.name}s.Count());
        }}
    }}
}}'''
        
        services_path = test_project_path / "Services"
        services_path.mkdir(exist_ok=True)
        self._write_file(services_path / f"{entity.name}ServiceTests.cs", test_content)
    
    def _generate_controller_tests(self, test_project_path: Path, config: ProjectConfig, entity: EntityConfig):
        """Generate controller test files"""
        test_content = f'''using Microsoft.VisualStudio.TestTools.UnitTesting;
using {config.project_name}.Controllers;
using {config.project_name}.Models;
using {config.project_name}.Services;
using System.Web.Http;
using System.Web.Http.Results;

namespace {config.project_name}.Tests.Controllers
{{
    [TestClass]
    public class {entity.name}ControllerTests
    {{
        private {entity.name}Controller _controller;
        private Mock{entity.name}Service _mockService;

        [TestInitialize]
        public void Setup()
        {{
            _mockService = new Mock{entity.name}Service();
            _controller = new {entity.name}Controller(_mockService);
        }}

        [TestMethod]
        public void Get_ReturnsOkResult()
        {{
            // Act
            var result = _controller.Get();

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<System.Collections.Generic.IEnumerable<{entity.name}>>));
        }}

        [TestMethod]
        public void GetById_WithValidId_ReturnsOkResult()
        {{
            // Arrange
            var {entity.name.lower()} = new {entity.name} {{ /* Set properties */ }};
            _mockService.SetupGetById({entity.name.lower()});

            // Act
            var result = _controller.Get(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<{entity.name}>));
        }}

        [TestMethod]
        public void GetById_WithInvalidId_ReturnsNotFound()
        {{
            // Arrange
            _mockService.SetupGetById(null);

            // Act
            var result = _controller.Get(999);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }}

        [TestMethod]
        public void Post_WithValidModel_ReturnsCreatedResult()
        {{
            // Arrange
            var {entity.name.lower()} = new {entity.name} {{ /* Set properties */ }};
            _mockService.SetupCreate({entity.name.lower()});

            // Act
            var result = _controller.Post({entity.name.lower()});

            // Assert
            Assert.IsInstanceOfType(result, typeof(CreatedNegotiatedContentResult<{entity.name}>));
        }}

        [TestMethod]
        public void Delete_WithValidId_ReturnsOkResult()
        {{
            // Arrange
            _mockService.SetupDelete(true);

            // Act
            var result = _controller.Delete(1);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }}

        [TestMethod]
        public void Delete_WithInvalidId_ReturnsNotFound()
        {{
            // Arrange
            _mockService.SetupDelete(false);

            // Act
            var result = _controller.Delete(999);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }}
    }}

    // Mock service for testing
    public class Mock{entity.name}Service : I{entity.name}Service
    {{
        private {entity.name} _returnValue;
        private bool _deleteResult = true;

        public void SetupGetById({entity.name} returnValue)
        {{
            _returnValue = returnValue;
        }}

        public void SetupCreate({entity.name} returnValue)
        {{
            _returnValue = returnValue;
        }}

        public void SetupDelete(bool result)
        {{
            _deleteResult = result;
        }}

        public System.Collections.Generic.IEnumerable<{entity.name}> GetAll()
        {{
            return new[] {{ _returnValue ?? new {entity.name}() }};
        }}

        public {entity.name} GetById(int id)
        {{
            return _returnValue;
        }}

        public {entity.name} Create({entity.name} {entity.name.lower()})
        {{
            return _returnValue ?? {entity.name.lower()};
        }}

        public {entity.name} Update({entity.name} {entity.name.lower()})
        {{
            return _returnValue ?? {entity.name.lower()};
        }}

        public bool Delete(int id)
        {{
            return _deleteResult;
        }}
    }}
}}'''
        
        controllers_path = test_project_path / "Controllers"
        controllers_path.mkdir(exist_ok=True)
        self._write_file(controllers_path / f"{entity.name}ControllerTests.cs", test_content)
    
    def _generate_additional_files(self, project_path: Path, config: ProjectConfig):
        """Generate additional project files"""
        # Generate README.md
        readme_content = f'''# {config.project_name}

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

1. Open `{config.project_name}.sln` in Visual Studio
2. Restore NuGet packages
3. Update connection strings in `Web.config` (if using database)
4. Build and run the project

### API Endpoints

The following endpoints are available:'''

        if config.entities:
            for entity in config.entities:
                readme_content += f'''

#### {entity.name}
- `GET /api/{entity.name.lower()}` - Get all {entity.name.lower()}s
- `GET /api/{entity.name.lower()}/{{id}}` - Get specific {entity.name.lower()}
- `POST /api/{entity.name.lower()}` - Create new {entity.name.lower()}
- `PUT /api/{entity.name.lower()}/{{id}}` - Update {entity.name.lower()}
- `DELETE /api/{entity.name.lower()}/{{id}}` - Delete {entity.name.lower()}'''

        if config.include_swagger:
            readme_content += f'''

### Documentation

Swagger documentation is available at: `/swagger`'''

        readme_content += f'''

## Project Structure

```
{config.project_name}/
├── src/{config.project_name}/
│   ├── App_Start/          # Application configuration
│   ├── Controllers/        # Web API controllers
│   ├── Data/              # Entity Framework context and configurations
│   ├── Models/            # Data models
│   ├── Services/          # Business logic services
│   └── Properties/        # Assembly information
├── tests/{config.project_name}.Tests/
│   ├── Controllers/       # Controller tests
│   └── Services/         # Service tests
└── {config.project_name}.sln
```

## License

MIT License
'''
        
        self._write_file(project_path / "README.md", readme_content)
        
        # Generate .gitignore
        gitignore_content = '''# Build results
[Dd]ebug/
[Dd]ebugPublic/
[Rr]elease/
[Rr]eleases/
x64/
x86/
[Bb]in/
[Oo]bj/
[Ll]og/

# Visual Studio
.vs/
*.suo
*.user
*.userosscache
*.sln.docstates
*.userprefs

# NuGet
*.nupkg
**/packages/*
!**/packages/build/

# Entity Framework
*.mdf
*.ldf

# SQL Server files
*.mdf
*.ldf
*.ndf

# Business Intelligence projects
*.rdl.data
*.bim.layout
*.bim_*.settings
*.rptproj.rsuser

# Microsoft Fakes
FakesAssemblies/

# Node.js (if applicable)
node_modules/
npm-debug.log*

# Others
*.cache
*.log
*.vspscc
*.vssscc
.builds
*.pidb
*.svclog
*.scc
'''
        
        self._write_file(project_path / ".gitignore", gitignore_content)
    
    def _write_file(self, file_path: Path, content: str):
        """Write content to file and track created files"""
        file_path.parent.mkdir(parents=True, exist_ok=True)
        with open(file_path, 'w', encoding='utf-8') as f:
            f.write(content)
        self.created_files.append(str(file_path))


# Usage example function
def create_sample_project():
    """Create a sample project with User and Product entities"""
    
    # Define entities
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
    
    product_entity = EntityConfig(
        name="Product",
        properties=[
            PropertyConfig("Id", "int", is_required=True, is_key=True),
            PropertyConfig("Name", "string", is_required=True, max_length=100),
            PropertyConfig("Description", "string", max_length=500),
            PropertyConfig("Price", "decimal", is_required=True),
            PropertyConfig("Stock", "int", is_required=True),
            PropertyConfig("UserId", "int", is_required=True, foreign_table="User"),
            PropertyConfig("CreatedDate", "DateTime", is_required=True)
        ]
    )
    
    # Configure project
    config = ProjectConfig(
        project_name="SampleWebApi",
        project_type=ProjectType.WEBAPI,
        output_path="./output",
        include_database=True,
        database_provider=DatabaseProvider.SQLSERVER,
        connection_string="Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SampleWebApiDb;Integrated Security=True",
        entities=[user_entity, product_entity],
        include_swagger=True,
        include_cors=True,
        include_authentication=False,
        include_tests=True
    )
    
    # Generate project
    generator = EnhancedDotNetGenerator()
    result = generator.generate_project(config)
    
    return result


if __name__ == "__main__":
    print("🚀 Enhanced .NET Project Generator")
    print("=" * 50)
    
    # Create sample project
    result = create_sample_project()
    
    if result['success']:
        print(f"✅ Project generated successfully!")
        print(f"📁 Location: {result['project_path']}")
        print(f"📄 Files created: {len(result['files_created'])}")
        print(f"💬 {result['message']}")
        
        print("\n📋 Generated files:")
        for file_path in result['files_created'][:20]:  # Show first 20 files
            print(f"   ✓ {file_path}")
        
        if len(result['files_created']) > 20:
            print(f"   ... and {len(result['files_created']) - 20} more files")
            
        print(f"\n🎯 Next steps:")
        print(f"1. Open {result['project_path']}/SampleWebApi.sln in Visual Studio")
        print(f"2. Restore NuGet packages")
        print(f"3. Update connection string in Web.config")
        print(f"4. Build and run the project")
        print(f"5. Access Swagger documentation at: /swagger")
        
    else:
        print(f"❌ Failed to generate project: {result['error']}")