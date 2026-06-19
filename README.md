# TFG - Aplicación .NET MVC con arquitectura por capas y CI/CD

Proyecto desarrollado como Trabajo de Fin de Grado, centrado en el diseño, construcción y mantenimiento de una aplicación web ASP.NET Core MVC con arquitectura por capas, pruebas automatizadas, integración continua y despliegue continuo.

El objetivo principal del proyecto no es construir una aplicación funcionalmente compleja, sino aplicar un proceso completo de ingeniería del software: planificación, arquitectura, control de versiones, persistencia, testing, automatización, despliegue y mantenimiento.

## Estado actual

Estado del proyecto: versión `v0.4.0`.

Actualmente el proyecto incluye:

- Aplicación ASP.NET Core MVC en .NET 8.
- Arquitectura por capas.
- Persistencia con Entity Framework Core Code First.
- PostgreSQL como proveedor de base de datos.
- Base de datos de producción en Neon.
- CRUD funcional de la entidad `Item`.
- CRUD funcional de la entidad `Category`.
- Relación opcional entre `Item` y `Category`.
- Separación entre entidades de dominio, DTOs de aplicación y ViewModels de presentación.
- Mapeo entre capas mediante AutoMapper.
- Validaciones en ViewModels mediante DataAnnotations.
- Tests unitarios, tests de casos de uso y tests de integración.
- Pipeline de CI con GitHub Actions.
- Despliegue continuo en Render mediante Docker.
- Aplicación de migraciones al arrancar la aplicación en entornos relacionales.
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

- Controllers.
- Views.
- ViewModels.
- Perfiles de mapeo entre DTOs y ViewModels.
- Configuración de servicios.
- Configuración del pipeline HTTP.
- Configuración de arranque de la aplicación.

Actualmente incluye ViewModels específicos para las entidades `Item` y `Category`.

ViewModels de `Item`:

- `ItemListViewModel`
- `ItemDetailsViewModel`
- `CreateItemViewModel`
- `EditItemViewModel`

ViewModels de `Category`:

- `CategoryListViewModel`
- `CategoryDetailsViewModel`
- `CreateCategoryViewModel`
- `EditCategoryViewModel`

El perfil `ViewModelMappingProfile` centraliza los mapeos entre los DTOs de la capa de aplicación y los ViewModels utilizados por las vistas MVC.

### TfgNetMvc.Application

Contiene la lógica de aplicación:

- Casos de uso.
- DTOs.
- Interfaces de repositorios.
- Perfiles de mapeo entre entidades de dominio y DTOs.

Actualmente incluye casos de uso para la entidad `Item`:

- `CreateItem`
- `GetItems`
- `GetItemById`
- `UpdateItem`
- `DeleteItem`

También incluye casos de uso para la entidad `Category`:

- `CreateCategory`
- `GetCategories`
- `GetCategoryById`
- `UpdateCategory`
- `DeleteCategory`

DTOs de `Item`:

- `ItemDto`
- `CreateItemDto`
- `UpdateItemDto`

DTOs de `Category`:

- `CategoryDto`
- `CreateCategoryDto`
- `UpdateCategoryDto`

El perfil `DtoMappingProfile` centraliza los mapeos entre entidades de dominio y DTOs de aplicación.

### TfgNetMvc.Domain

Contiene las entidades y reglas de negocio independientes de infraestructura.

Actualmente incluye:

- Entidad `Item`.
- Entidad `Category`.
- Validaciones de nombre.
- Validaciones de stock en `Item`.
- Operaciones de dominio como añadir o retirar stock.
- Relación opcional entre `Item` y `Category`.

La entidad `Item` puede tener una categoría asociada mediante `CategoryId`, pero dicha relación es opcional. Esto permite que existan items sin categoría asignada.

### TfgNetMvc.Infrastructure

Contiene detalles técnicos de infraestructura:

- `AppDbContext`.
- Configuración de EF Core.
- Repositorios concretos.
- Migraciones.

Actualmente incluye:

- `ItemRepository`.
- `CategoryRepository`.
- Configuración de la tabla `Items`.
- Configuración de la tabla `Categories`.
- Configuración de la relación opcional `Item` -> `Category`.
- Migración inicial para PostgreSQL.

El proveedor de base de datos utilizado es PostgreSQL mediante `Npgsql.EntityFrameworkCore.PostgreSQL`.

### TfgNetMvc.Tests

Contiene pruebas automatizadas:

- Tests unitarios de dominio.
- Tests de casos de uso.
- Tests de integración con `WebApplicationFactory`.

Los tests de integración utilizan una configuración específica mediante `CustomWebApplicationFactory`, sustituyendo la base de datos relacional real por EF Core InMemory.

## Patrón aplicado al CRUD

Las entidades `Item` y `Category` se implementan siguiendo una separación explícita entre capas:

```text
Views
  -> ViewModels

Controller
  -> AutoMapper
  -> DTOs

Application
  -> Use Cases
  -> DTOs

Domain
  -> Entities
  -> Reglas de negocio

Infrastructure
  -> EF Core
  -> Repositories
```

Las vistas MVC no trabajan directamente con entidades de dominio ni con DTOs de aplicación, sino con ViewModels definidos en la capa Web.

Los casos de uso reciben y devuelven DTOs, manteniendo la capa Application desacoplada de los modelos de presentación.

Los repositorios se definen mediante interfaces en Application y se implementan en Infrastructure. De esta forma, la lógica de aplicación no depende directamente de EF Core ni del DbContext.

## Patrón aplicado al CRUD de Items

La vista `Index` de Items utiliza específicamente:

```csharp
@model List<TfgNetMvc.Web.ViewModels.Items.ItemListViewModel>
```

El listado de Items muestra los campos:

- `Name`
- `Description`
- `Stock`
- `Category`

Las vistas de creación y edición utilizan ViewModels con validaciones mediante DataAnnotations. Los formularios están enlazados mediante Tag Helpers (`asp-for`) para mantener una relación explícita entre la vista y el ViewModel correspondiente.

La creación y edición de Items permite seleccionar una categoría mediante un desplegable. La relación con Category es opcional.

## Patrón aplicado al CRUD de Categories

La vista `Index` de Categories utiliza específicamente:

```csharp
@model List<TfgNetMvc.Web.ViewModels.Categories.CategoryListViewModel>
```

El listado de Categories muestra los campos:

- `Name`
- `Description`

Las vistas de creación y edición utilizan ViewModels con validaciones mediante DataAnnotations. Los formularios están enlazados mediante Tag Helpers (`asp-for`) siguiendo el mismo patrón aplicado a Items.

## Relación entre Item y Category

La relación entre `Item` y `Category` es opcional:

```text
Category 1 -> N Items
Item 0..1 -> Category
```

A nivel de modelo, `Item` contiene:

```csharp
public int? CategoryId { get; private set; }
public Category? Category { get; private set; }
```

La clave foránea `CategoryId` es nullable. Esto permite que un Item exista sin categoría asociada.

En EF Core, la relación se configura de forma que, al eliminar una Category, los Items asociados no se eliminan. En su lugar, su `CategoryId` pasa a `null`. Esta decisión evita pérdidas accidentales de datos y mantiene la independencia entre la gestión de Items y Categories.

## Requisitos técnicos

- .NET 8 SDK.
- Git.
- PostgreSQL para ejecución local con base de datos real.
- Docker opcional para validación local del contenedor.
- Cuenta o servicio PostgreSQL externo para producción. Actualmente se utiliza Neon.

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

El proyecto utiliza EF Core Code First con PostgreSQL.

La cadena de conexión base se encuentra en `TfgNetMvc.Web/appsettings.json` con un valor de referencia sin credenciales reales:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Database=TfgNetMvcDb;Username=postgres;Password=yourpassword"
}
```

Para desarrollo local, se utiliza `TfgNetMvc.Web/appsettings.Development.json` con la password real del PostgreSQL local. Este archivo está excluido del repositorio mediante `.gitignore`.

Ejemplo de estructura local:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=TfgNetMvcDb;Username=postgres;Password=your-local-password"
  }
}
```

La aplicación aplica automáticamente las migraciones pendientes al arrancar cuando el proveedor de base de datos es relacional.

También se pueden aplicar migraciones manualmente con:

```bash
dotnet ef database update --project TfgNetMvc.Infrastructure --startup-project TfgNetMvc.Web
```

## Base de datos de producción

La base de datos de producción está configurada en Neon, utilizando PostgreSQL.

En Render, la cadena de conexión se configura mediante variable de entorno:

```text
ConnectionStrings__DefaultConnection
```

ASP.NET Core interpreta el doble guión bajo (`__`) como jerarquía de configuración, por lo que esta variable equivale a:

```text
ConnectionStrings:DefaultConnection
```

La connection string real de Neon no se almacena en el repositorio. Se gestiona exclusivamente desde las variables de entorno del servicio en Render.

Con esta configuración, el despliegue en Render utiliza Neon como base de datos de producción y aplica las migraciones automáticamente al arrancar la aplicación.

## Gestión de credenciales

El proyecto utiliza un patrón de configuración por entornos:

```text
appsettings.json
  -> valores base y placeholders sin credenciales reales

appsettings.Development.json
  -> configuración local con password real
  -> excluido del repositorio mediante .gitignore

Variables de entorno en Render
  -> connection string real de Neon
  -> no se almacenan en el repositorio
```

Este patrón permite mantener el repositorio libre de secretos y separar la configuración local de la configuración de producción.

## Testing

El proyecto incluye distintos niveles de pruebas.

### Tests unitarios de dominio

Validan reglas de negocio de las entidades `Item` y `Category`, como:

- Creación con datos válidos.
- Rechazo de nombre vacío.
- Rechazo de stock negativo en `Item`.
- Incremento y decremento de stock.
- Actualización de entidades.
- Validación de nombres con espacios.

### Tests de Application

Validan los casos de uso utilizando repositorios fake en memoria.

Esto permite probar la lógica de aplicación sin depender de PostgreSQL ni de EF Core.

Los casos de uso trabajan con DTOs de entrada y salida, lo que permite validar la lógica de aplicación sin acoplarla a los ViewModels de la capa Web.

### Tests de integración

Se utiliza `WebApplicationFactory<Program>` para levantar la aplicación en memoria y validar endpoints MVC.

Para permitir estos tests, `Program.cs` incluye una declaración parcial pública de la clase `Program`:

```csharp
public partial class Program { }
```

En el entorno de integración continua, estos tests utilizan una configuración específica mediante `CustomWebApplicationFactory`, sustituyendo la conexión real a PostgreSQL por una base de datos en memoria con EF Core InMemory. Esto permite ejecutar los tests de integración en GitHub Actions sin depender de infraestructura externa.

La aplicación incorpora startup migration automática, pero esta solo se ejecuta con proveedores relacionales gracias a la comprobación `Database.IsRelational()`. Por ello, los tests de integración con EF Core InMemory siguen funcionando correctamente.

Actualmente el proyecto cuenta con 36 tests correctos.

## CI/CD

### Integración continua

El proyecto utiliza GitHub Actions para ejecutar automáticamente:

- Restore.
- Build.
- Tests.

El workflow se ejecuta sobre las ramas principales del proyecto.

### Despliegue continuo

Inicialmente se intentó desplegar en Azure App Service, pero la suscripción académica Azure for Students aplicaba restricciones de directivas que impedían crear los recursos necesarios.

Como alternativa, se implementó despliegue continuo mediante:

- Render.
- Docker.
- GitHub.
- Neon.

Render construye la imagen Docker a partir del repositorio y despliega automáticamente desde la rama `develop`.

Durante el arranque, la aplicación aplica las migraciones pendientes contra la base de datos PostgreSQL configurada mediante variable de entorno.

URL actual:

```text
https://tfg-net-mvc.onrender.com
```

## Limitaciones actuales

- PostgreSQL local está preparado a nivel de configuración, pero requiere tener PostgreSQL instalado en la máquina local y configurar la password real en `appsettings.Development.json`.
- La base de datos de producción utiliza Neon en modalidad gratuita, por lo que puede tener limitaciones propias del plan gratuito.
- Docker no se pudo validar localmente por problemas de Docker Desktop/WSL en Windows, pero el build Docker fue validado correctamente en Render.
- La memoria final en LaTeX está pendiente de elaboración.

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

Las versiones estables se publican en `main` mediante etiquetas semánticas:

```text
v0.1.0
v0.2.0
v0.3.0
v0.4.0
```

## Versiones

### v0.1.0

Primera versión estable del proyecto.

Incluye:

- Arquitectura por capas.
- CRUD funcional de la entidad `Item` en entorno local.
- EF Core Code First con SQL Server LocalDB.
- Tests unitarios y de integración.
- Integración continua con GitHub Actions.
- Despliegue continuo en Render mediante Docker.

### v0.2.0

Versión centrada en mejorar la separación de responsabilidades del CRUD de `Item`.

Incluye:

- DTOs en la capa Application.
- ViewModels en la capa Web.
- AutoMapper 16.1.1.
- `DtoMappingProfile`.
- `ViewModelMappingProfile`.
- Adaptación del `ItemsController` para trabajar con ViewModels y mapear hacia DTOs.
- Adaptación de vistas para trabajar con ViewModels tipados.
- Validaciones mediante DataAnnotations en ViewModels.
- Formularios enlazados mediante Tag Helpers (`asp-for`).
- Inclusión de `Description` en `ItemListViewModel` y en el listado de Items.

### v0.3.0

Versión centrada en ampliar el dominio con una segunda entidad y validar la extensibilidad del patrón.

Incluye:

- Entidad `Category`.
- CRUD completo de `Category`.
- DTOs, ViewModels, repositorio, casos de uso, controlador y vistas para `Category`.
- Relación opcional entre `Item` y `Category`.
- Selector de categorías en creación y edición de Items.
- Actualización de `ItemDto` y ViewModels de Item para mostrar la categoría.
- Nueva migración EF Core para Categories y la relación con Items.
- Ampliación de tests hasta 36 tests correctos.

### v0.4.0

Versión centrada en completar la infraestructura de persistencia en producción.

Incluye:

- Migración de SQL Server a PostgreSQL.
- Uso de `Npgsql.EntityFrameworkCore.PostgreSQL`.
- Configuración de Neon como base de datos de producción.
- Configuración de connection string mediante variable de entorno en Render.
- Exclusión de `appsettings.Development.json` del repositorio.
- Regeneración de migraciones EF Core para PostgreSQL.
- Startup migration automática con comprobación `Database.IsRelational()`.
- `/Items` y `/Categories` funcionales en producción con persistencia real.

## Próximos pasos

Tras `v0.4.0`, el proyecto dispone de una base técnica completa:

- Arquitectura por capas.
- Dos entidades con CRUD completo.
- Relación entre entidades.
- DTOs, ViewModels y AutoMapper.
- EF Core con PostgreSQL.
- Base de datos de producción en Neon.
- CI/CD con GitHub Actions, Docker y Render.
- Persistencia real en producción.
- 36 tests automatizados.

Los siguientes pasos se centran en:

- Revisar documentación final del repositorio.
- Elaborar la memoria final del TFG en LaTeX.
- Consolidar los documentos técnicos incrementales generados durante el desarrollo.
- Preparar la defensa y explicación de decisiones técnicas.
