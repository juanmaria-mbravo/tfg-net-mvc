# TFG - Aplicación .NET MVC con arquitectura por capas y CI/CD

Proyecto desarrollado como Trabajo de Fin de Grado, centrado en el diseño, construcción y mantenimiento de una aplicación web ASP.NET Core MVC con arquitectura por capas, pruebas automatizadas, integración continua y despliegue continuo.

El objetivo principal del proyecto no es construir una aplicación funcionalmente compleja, sino aplicar un proceso completo de ingeniería del software: planificación, arquitectura, control de versiones, persistencia, testing, automatización, despliegue y mantenimiento.

## Estado actual

Estado del proyecto: versión `v0.7.3`.

Actualmente el proyecto incluye:

- Aplicación ASP.NET Core MVC en .NET 8.
- Arquitectura por capas.
- Persistencia con Entity Framework Core Code First.
- PostgreSQL como proveedor de base de datos.
- Base de datos de producción en Neon.
- CRUD funcional de las entidades `Item`, `Category`, `Supplier` y `WarehouseLocation`.
- Registro inmutable de movimientos de stock (`StockMovement`): entradas y salidas.
- Relación opcional entre `Item` y `Category`.
- Relación opcional entre `Item` y `WarehouseLocation`.
- Relaciones opcionales entre `StockMovement` y `Supplier`/`WarehouseLocation`.
- El stock de un artículo solo puede modificarse mediante `RegisterStockEntry` y `RegisterStockExit`, garantizando trazabilidad completa en `StockMovement`.
- Separación entre entidades de dominio, DTOs de aplicación y ViewModels de presentación.
- Mapeo entre capas mediante AutoMapper.
- Validaciones en ViewModels mediante DataAnnotations.
- Autenticación y autorización basada en roles con ASP.NET Core Identity.
- Roles: Admin, Operator, Viewer.
- Política de autorización global: todas las rutas requieren autenticación por defecto.
- Seed automático de roles y usuario admin desde variables de entorno.
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

ViewModels incluidos:

- `Item`: `ItemListViewModel`, `ItemDetailsViewModel`, `CreateItemViewModel`, `EditItemViewModel`
- `Category`: `CategoryListViewModel`, `CategoryDetailsViewModel`, `CreateCategoryViewModel`, `EditCategoryViewModel`
- `Supplier`: `SupplierListViewModel`, `SupplierDetailsViewModel`, `CreateSupplierViewModel`, `EditSupplierViewModel`
- `WarehouseLocation`: `WarehouseLocationListViewModel`, `WarehouseLocationDetailsViewModel`, `CreateWarehouseLocationViewModel`, `EditWarehouseLocationViewModel`
- `StockMovement`: `StockMovementViewModel`, `RegisterStockEntryViewModel`, `RegisterStockExitViewModel`

El perfil `ViewModelMappingProfile` centraliza los mapeos entre los DTOs de la capa de aplicación y los ViewModels utilizados por las vistas MVC.

### TfgNetMvc.Application

Contiene la lógica de aplicación:

- Casos de uso.
- DTOs.
- Interfaces de repositorios.
- Perfiles de mapeo entre entidades de dominio y DTOs.

Casos de uso por entidad:

- `Item`: `CreateItem`, `GetItems`, `GetItemById`, `UpdateItem`, `DeleteItem`
- `Category`: `CreateCategory`, `GetCategories`, `GetCategoryById`, `UpdateCategory`, `DeleteCategory`
- `Supplier`: `CreateSupplier`, `GetSuppliers`, `GetSupplierById`, `UpdateSupplier`, `DeleteSupplier`
- `WarehouseLocation`: `CreateWarehouseLocation`, `GetWarehouseLocations`, `GetWarehouseLocationById`, `UpdateWarehouseLocation`, `DeleteWarehouseLocation`
- `StockMovement`: `RegisterStockEntry`, `RegisterStockExit`, `GetStockMovements`, `GetMovementsByItem`

El perfil `DtoMappingProfile` centraliza los mapeos entre entidades de dominio y DTOs de aplicación.

### TfgNetMvc.Domain

Contiene las entidades y reglas de negocio independientes de infraestructura.

Entidades incluidas:

- `Item`: nombre, descripción, stock, categoría opcional y ubicación de almacén opcional. El stock solo puede modificarse mediante `AddStock` y `RemoveStock`; `Update()` no acepta parámetro de stock.
- `Category`: nombre, descripción.
- `Supplier`: nombre, email de contacto, teléfono, notas.
- `WarehouseLocation`: nombre, descripción.
- `StockMovement`: entidad inmutable (solo creación, sin modificación ni eliminación). Registra tipo de movimiento (`Entry`/`Exit`), cantidad, stock anterior, stock nuevo, referencia opcional a `Supplier` y `WarehouseLocation`.

### TfgNetMvc.Infrastructure

Contiene detalles técnicos de infraestructura:

- `AppDbContext`, que hereda de `IdentityDbContext<ApplicationUser>` para integrar las tablas de Identity.
- Configuración de EF Core.
- Repositorios concretos.
- Migraciones.
- `ApplicationUser` (usuario de Identity).
- `DataSeeder`: crea los roles y el usuario admin al arrancar desde variables de entorno.

Repositorios incluidos: `ItemRepository`, `CategoryRepository`, `SupplierRepository`, `WarehouseLocationRepository`, `StockMovementRepository`.

El proveedor de base de datos utilizado es PostgreSQL mediante `Npgsql.EntityFrameworkCore.PostgreSQL`.

### TfgNetMvc.Tests

Contiene pruebas automatizadas:

- Tests unitarios de dominio.
- Tests de casos de uso.
- Tests de integración con `WebApplicationFactory`.

Los tests de integración utilizan dos fábricas:

- `CustomWebApplicationFactory`: reemplaza la BD por EF Core InMemory y configura un `TestAuthHandler` que autentica las peticiones como Admin. Usado por tests de Items, Categories, etc.
- `AnonymousWebApplicationFactory`: reemplaza la BD por EF Core InMemory sin override de autenticación. Usado para verificar que rutas protegidas redirigen a Login cuando el usuario es anónimo.

Actualmente el proyecto cuenta con **84 tests correctos**.

## Autorización basada en roles

El proyecto implementa ASP.NET Core Identity con tres roles:

| Rol | Permisos |
|-----|----------|
| Admin | CRUD completo de todas las entidades + registro de movimientos |
| Operator | Lectura de todas las entidades + registro de movimientos de stock |
| Viewer | Lectura de todas las entidades |

La política de autorización es **global**: todas las rutas requieren autenticación por defecto. Solo la página de inicio (`/`) tiene `[AllowAnonymous]`.

Las vistas ocultan los botones de creación, edición y eliminación a los usuarios sin el rol Admin.

## Patrón aplicado al CRUD

Las entidades se implementan siguiendo una separación explícita entre capas:

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

Los repositorios se definen mediante interfaces en Application y se implementan en Infrastructure.

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

Para desarrollo local, se utiliza `TfgNetMvc.Web/appsettings.Development.json` con la password real del PostgreSQL local y las credenciales del usuario admin de seed. Este archivo está excluido del repositorio mediante `.gitignore`.

Ejemplo de estructura local:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=TfgNetMvcDb;Username=postgres;Password=your-local-password"
  },
  "SeedAdmin": {
    "Email": "admin@example.com",
    "Password": "your-admin-password"
  }
}
```

La aplicación aplica automáticamente las migraciones pendientes y ejecuta el seed de roles y usuario admin al arrancar cuando el proveedor de base de datos es relacional.

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

ASP.NET Core interpreta el doble guión bajo (`__`) como jerarquía de configuración.

La connection string real de Neon no se almacena en el repositorio. Se gestiona exclusivamente desde las variables de entorno del servicio en Render.

## Gestión de credenciales

El proyecto utiliza un patrón de configuración por entornos:

```text
appsettings.json
  -> valores base y placeholders sin credenciales reales

appsettings.Development.json
  -> configuración local con password real de PostgreSQL y credenciales de seed
  -> excluido del repositorio mediante .gitignore

Variables de entorno en Render
  -> ConnectionStrings__DefaultConnection  (connection string de Neon)
  -> SeedAdmin__Email                      (email del usuario admin inicial)
  -> SeedAdmin__Password                   (password del usuario admin inicial)
  -> no se almacenan en el repositorio
```

Este patrón permite mantener el repositorio libre de secretos y separar la configuración local de la configuración de producción.

## Testing

El proyecto incluye distintos niveles de pruebas.

### Tests unitarios de dominio

Validan reglas de negocio de las entidades `Item`, `Category`, `Supplier`, `WarehouseLocation` y `StockMovement`.

### Tests de Application

Validan los casos de uso utilizando repositorios fake en memoria. Esto permite probar la lógica de aplicación sin depender de PostgreSQL ni de EF Core.

### Tests de integración

Se utiliza `WebApplicationFactory<Program>` para levantar la aplicación en memoria y validar endpoints MVC.

Para permitir estos tests, `Program.cs` incluye una declaración parcial pública de la clase `Program`:

```csharp
public partial class Program { }
```

Los tests de integración utilizan EF Core InMemory en lugar de PostgreSQL real, y `TestAuthHandler` para simular un usuario autenticado con rol Admin. Adicionalmente, se valida que las rutas protegidas redirigen a Login cuando el usuario es anónimo.

La startup migration automática solo se ejecuta con proveedores relacionales gracias a la comprobación `Database.IsRelational()`, por lo que los tests con EF Core InMemory siguen funcionando correctamente.

## CI/CD

### Integración continua

El proyecto utiliza GitHub Actions para ejecutar automáticamente:

- Restore.
- Build.
- Tests.

El workflow se ejecuta sobre las ramas principales del proyecto.

### Despliegue continuo

El despliegue continuo se realiza mediante:

- Render.
- Docker.
- GitHub.
- Neon.

Render construye la imagen Docker a partir del repositorio y despliega automáticamente desde la rama `develop`.

Durante el arranque, la aplicación aplica las migraciones pendientes y ejecuta el seed de roles y usuario admin.

URL actual:

```text
https://tfg-net-mvc.onrender.com
```

## Limitaciones actuales

- PostgreSQL local requiere tener PostgreSQL instalado y configurar las credenciales en `appsettings.Development.json`.
- La base de datos de producción utiliza Neon en modalidad gratuita, por lo que puede tener limitaciones propias del plan gratuito.
- Docker no se pudo validar localmente por problemas de Docker Desktop/WSL en Windows, pero el build Docker fue validado correctamente en Render.
- La memoria final en LaTeX está en elaboración.

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
v0.1.0  v0.2.0  v0.3.0  v0.4.0  v0.5.0  v0.6.0  v0.7.0  v0.7.1  v0.7.2  v0.7.3
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
- AutoMapper.
- `DtoMappingProfile` y `ViewModelMappingProfile`.
- Adaptación del `ItemsController` para trabajar con ViewModels y mapear hacia DTOs.
- Validaciones mediante DataAnnotations en ViewModels.
- Formularios enlazados mediante Tag Helpers (`asp-for`).

### v0.3.0

Versión centrada en ampliar el dominio con una segunda entidad y validar la extensibilidad del patrón.

Incluye:

- Entidad `Category`.
- CRUD completo de `Category`.
- Relación opcional entre `Item` y `Category`.
- Selector de categorías en creación y edición de Items.
- Nueva migración EF Core para Categories y la relación con Items.
- 36 tests correctos.

### v0.4.0

Versión centrada en completar la infraestructura de persistencia en producción.

Incluye:

- Migración de SQL Server a PostgreSQL.
- Uso de `Npgsql.EntityFrameworkCore.PostgreSQL`.
- Configuración de Neon como base de datos de producción.
- Configuración de connection string mediante variable de entorno en Render.
- Startup migration automática con comprobación `Database.IsRelational()`.
- `/Items` y `/Categories` funcionales en producción con persistencia real.

### v0.5.0

Versión centrada en documentación y configuración del entorno.

Incluye:

- README actualizado al estado real del proyecto.
- `appsettings.Development.json` excluido del repositorio mediante `.gitignore`.

### v0.6.0

Versión centrada en la expansión del dominio de inventario.

Incluye:

- Entidad `Supplier` con CRUD completo.
- Entidad `WarehouseLocation` con CRUD completo.
- Entidad `StockMovement` inmutable (solo creación, sin edición ni eliminación).
- Casos de uso `RegisterStockEntry` y `RegisterStockExit` que coordinan `IItemRepository` e `IStockMovementRepository`.
- Relaciones opcionales entre `StockMovement` y `Supplier`/`WarehouseLocation`.
- Vista de movimientos filtrada por item (`/StockMovements/ByItem?itemId=`).
- 76 tests correctos.

### v0.7.0

Versión centrada en autenticación y autorización basada en roles.

Incluye:

- ASP.NET Core Identity con `IdentityDbContext<ApplicationUser>`.
- Tres roles: Admin, Operator, Viewer.
- Política de autorización global (`FallbackPolicy`) que requiere autenticación en todas las rutas.
- `[AllowAnonymous]` exclusivamente en la página de inicio.
- Autorización granular por rol en cada controlador: Admin para CRUD, Admin+Operator para registro de movimientos.
- `DataSeeder` idempotente: crea roles y usuario admin al arrancar desde variables de entorno `SeedAdmin__Email` y `SeedAdmin__Password`.
- Migración `AddIdentityAuthentication` con las tablas de Identity.
- Navbar adaptado: muestra los enlaces solo cuando el usuario está autenticado, con opción de Login/Logout.
- Vistas adaptadas: botones de creación, edición y eliminación visibles solo para Admin.
- `TestAuthHandler` y `AnonymousWebApplicationFactory` para mantener los tests de integración funcionando.
- 82 tests correctos.

### v0.7.1

Versión correctiva del formulario de inicio de sesión.

Incluye:

- Corrección de la página de login: la partial de Identity no se renderizaba correctamente en la vista de inicio de sesión.
- Sin cambios funcionales en el dominio ni en los casos de uso.

### v0.7.2

Versión centrada en correcciones de coherencia entre el dominio y la capa de presentación.

Incluye:

- Cableado completo de `WarehouseLocationId` en `Item` a través de todas las capas: DTOs, casos de uso, perfil de mapeo, ViewModels, controlador, vistas y repositorio.
- Selector de ubicación de almacén en los formularios de creación y edición de artículos.
- Columna `Location` en el listado de artículos y campo `Location` en la vista de detalle.
- Eliminación del campo `Stock` del formulario de edición de artículos: el stock solo puede modificarse mediante `RegisterStockEntry` y `RegisterStockExit`.
- 84 tests correctos.

### v0.7.3

Versión correctiva del invariante de stock en el dominio.

Incluye:

- Eliminación del parámetro `stock` de `Item.Update()`: el contrato del dominio ya no permite modificar el stock desde la operación de actualización.
- El stock de un artículo solo puede cambiar mediante `AddStock()` y `RemoveStock()`, que son invocados exclusivamente por `RegisterStockEntry` y `RegisterStockExit`.
- Adaptación del caso de uso `UpdateItem` y de los tests de dominio.
- Test nuevo `Update_StockRemainsUnchanged` que documenta el invariante explícitamente.
- Sin cambios en el esquema de base de datos.
- 84 tests correctos.
