# TFG - Aplicación .NET MVC con arquitectura por capas y CI/CD

Proyecto desarrollado como Trabajo de Fin de Grado, centrado en el diseño, construcción y mantenimiento de una aplicación web ASP.NET Core MVC con arquitectura por capas, pruebas automatizadas, integración continua y despliegue continuo.

El objetivo principal del proyecto no es construir una aplicación funcionalmente compleja, sino aplicar un proceso completo de ingeniería del software: planificación, arquitectura, control de versiones, persistencia, testing, automatización y despliegue.

## Estado actual

Estado del proyecto: versión inicial en desarrollo.

Actualmente el proyecto incluye:

- Aplicación ASP.NET Core MVC en .NET 8.
- Arquitectura por capas.
- Persistencia con Entity Framework Core Code First.
- CRUD funcional de la entidad `Item` en entorno local.
- Tests unitarios y de integración.
- Pipeline de CI con GitHub Actions.
- Despliegue continuo en Render mediante Docker.
- URL pública de despliegue: `https://tfg-net-mvc.onrender.com`

## Arquitectura

La solución está dividida en varios proyectos:

```text
TfgNetMvc.Web
TfgNetMvc.Application
TfgNetMvc.Domain
TfgNetMvc.Infrastructure
TfgNetMvc.Tests
```

### TfgNetMvc.Web

Contiene la capa de presentación MVC:

- Controllers
- Views
- Configuración de servicios
- Configuración del pipeline HTTP

### TfgNetMvc.Application

Contiene la lógica de aplicación:

- Casos de uso
- DTOs cuando sean necesarios
- Interfaces de repositorios

Actualmente incluye casos de uso para la entidad `Item`:

- `CreateItem`
- `GetItems`
- `GetItemById`
- `UpdateItem`
- `DeleteItem`

### TfgNetMvc.Domain

Contiene las entidades y reglas de negocio independientes de infraestructura.

Actualmente incluye:

- Entidad `Item`
- Validaciones de nombre
- Validaciones de stock
- Operaciones de dominio como añadir o retirar stock

### TfgNetMvc.Infrastructure

Contiene detalles técnicos de infraestructura:

- `AppDbContext`
- Configuración de EF Core
- Repositorios concretos
- Migraciones

Actualmente incluye:

- `ItemRepository`
- Migración inicial
- Configuración de la tabla `Items`

### TfgNetMvc.Tests

Contiene pruebas automatizadas:

- Tests unitarios de dominio
- Tests de casos de uso
- Tests de integración con `WebApplicationFactory`

## Requisitos técnicos

- .NET 8 SDK
- SQL Server LocalDB para desarrollo local
- Git
- Docker opcional para validación local del contenedor

## Ejecución local

Restaurar dependencias:

```bash
dotnet restore
```

Compilar:

```bash
dotnet build
```

Ejecutar tests:

```bash
dotnet test
```

Ejecutar la aplicación:

```bash
dotnet run --project TfgNetMvc.Web
```

## Base de datos local

El proyecto utiliza EF Core Code First con SQL Server LocalDB en desarrollo.

La cadena de conexión local se encuentra en `TfgNetMvc.Web/appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=TfgNetMvcDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
}
```

Aplicar migraciones:

```bash
dotnet ef database update --project TfgNetMvc.Infrastructure --startup-project TfgNetMvc.Web
```

## Testing

El proyecto incluye distintos niveles de pruebas.

### Tests unitarios de dominio

Validan reglas de negocio de la entidad `Item`, como:

- Creación con datos válidos
- Rechazo de nombre vacío
- Rechazo de stock negativo
- Incremento y decremento de stock

### Tests de Application

Validan los casos de uso utilizando un repositorio fake en memoria.

Esto permite probar la lógica de aplicación sin depender de SQL Server ni de EF Core.

### Tests de integración

Se utiliza `WebApplicationFactory<Program>` para levantar la aplicación en memoria y validar endpoints MVC.

Para permitir estos tests, `Program.cs` incluye una declaración parcial pública de la clase `Program`:

```csharp
public partial class Program { }
```

En el entorno de integración continua, estos tests utilizan una configuración específica mediante `CustomWebApplicationFactory`, sustituyendo la conexión real a SQL Server LocalDB por una base de datos en memoria con EF Core InMemory. Esto permite ejecutar los tests de integración en GitHub Actions sin depender de infraestructura local de Windows.


## CI/CD

### Integración continua

El proyecto utiliza GitHub Actions para ejecutar automáticamente:

- Restore
- Build
- Tests

El workflow se ejecuta sobre las ramas principales del proyecto.

### Despliegue continuo

Inicialmente se intentó desplegar en Azure App Service, pero la suscripción académica Azure for Students aplicaba restricciones de directivas que impedían crear los recursos necesarios.

Como alternativa, se implementó despliegue continuo mediante:

- Render
- Docker
- GitHub

Render construye la imagen Docker a partir del repositorio y despliega automáticamente desde la rama `develop`.

URL actual:

```text
https://tfg-net-mvc.onrender.com
```

## Limitaciones actuales

- La aplicación desplegada en Render carga correctamente la Home.
- La sección `/Items` no funciona actualmente en Render porque la aplicación usa SQL Server LocalDB en desarrollo y todavía no se ha configurado una base de datos de producción.
- La persistencia en producción queda planteada como mejora futura.
- Docker no se pudo validar localmente por problemas de Docker Desktop/WSL en Windows, pero el build Docker fue validado correctamente en Render.

## Flujo de ramas

El proyecto utiliza el siguiente flujo:

```text
main       -> versión estable
develop    -> integración de funcionalidades
feature/*  -> desarrollo de funcionalidades concretas
```

Flujo habitual:

```text
feature/* -> develop -> main
```

Actualmente Render despliega desde `develop`.

## Próximos pasos

Posibles siguientes tareas:

- Preparar versión `v0.1.0`
- Configurar base de datos de producción
- Añadir una segunda entidad
- Mejorar validaciones mediante DTOs/ViewModels
- Ampliar documentación final del TFG
- Estructurar memoria en LaTeX
