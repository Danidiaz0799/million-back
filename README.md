# Real Estate API

[![.NET 8](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet)](https://dotnet.microsoft.com/)
[![MongoDB](https://img.shields.io/badge/MongoDB-6.0-47A248?style=for-the-badge&logo=mongodb)](https://www.mongodb.com/)
[![Swagger](https://img.shields.io/badge/Swagger-OpenAPI-85EA2D?style=for-the-badge&logo=swagger)](https://swagger.io/)
[![Clean Architecture](https://img.shields.io/badge/Architecture-Clean-blue?style=for-the-badge)](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)

API RESTful para gestionar propiedades inmobiliarias. Construida con .NET 8, MongoDB y principios de Clean Architecture.

## Características

- CRUD de propiedades con filtros y paginación
- Gestión de propietarios (owners)
- Imágenes por propiedad (galería)
- Trazas/historial de transacciones
- Búsqueda por nombre, dirección y rango de precios
- Swagger/OpenAPI y validaciones
- Tests unitarios e integrados con NUnit

## Inicio Rápido

1) Prerrequisitos

```sh
# .NET 8 SDK
winget install Microsoft.DotNet.SDK.8

# MongoDB Community Server
winget install MongoDB.Server
```

2) Ejecutar API

```sh
# Restaurar dependencias y ejecutar
dotnet restore
dotnet run --project RealEstate.Api
```

3) Inicializar la base de datos con datos de ejemplo

Después de ejecutar el proyecto por primera vez, puedes cargar datos de ejemplo en la base de datos ejecutando el siguiente script:

### Requisitos previos
- **MongoDB Community Server** instalado y corriendo (el servicio debe estar iniciado)
- **MongoDB Shell (mongosh)** instalado ([descargar aquí](https://www.mongodb.com/try/download/shell))
- El ejecutable `mongosh` debe estar en el PATH, o usa la ruta completa al ejecutarlo

### ¿Cuándo ejecutarlo?
- Solo la primera vez después de instalar MongoDB y correr la API
- O cuando quieras restaurar los datos de ejemplo

### ¿Cómo ejecutarlo?

1. Abre una terminal **cmd.exe** (no PowerShell)
2. Navega a la raíz del proyecto (donde está la carpeta `scripts`)
3. Ejecuta:

```cmd
mongosh < scripts\mongo\mongo-init.js
```

- Si el comando `mongosh` no se reconoce, agrega la carpeta de binarios de MongoDB Shell al PATH (ejemplo: `C:\Program Files\MongoDB\mongosh\`), o usa la ruta completa al ejecutable.
- El script conecta a `localhost:27017` y crea la base de datos `realestate_db` con datos de ejemplo.

¡Listo! Ahora la API tendrá datos de prueba disponibles.

4) Swagger

```
http://localhost:5064/swagger
```

## Arquitectura (Clean Architecture)

Patrón utilizado:
- Clean Architecture (inspirada en Onion/Hexagonal). Separa reglas de negocio del detalle técnico y define límites claros entre capas.

¿Por qué esta arquitectura?
- Independencia de frameworks: el dominio no depende de ASP.NET Core ni de MongoDB.
- Testabilidad: el núcleo es fácilmente unit testeable; la infraestructura se puede mockear.
- Mantenibilidad y escalabilidad: cambios en persistencia o exposición (REST/GraphQL) no rompen el dominio.
- Sustituibilidad: reemplazar MongoDB por otro storage afecta solo a Infrastructure.
- Enfoque en negocio: el modelo de dominio permanece estable pese a cambios técnicos.

Mapeo a proyectos del repositorio:
- Presentation ? RealEstate.Api (Controllers, Middleware, Swagger, CORS)
- Application ? RealEstate.Application (DTOs, interfaces, servicios de aplicación)
- Domain ? RealEstate.Domain (entidades y reglas de negocio puras)
- Infrastructure ? RealEstate.Infrastructure (repositorios, MongoDB Driver, implementaciones de interfaces)

Reglas de dependencia (de adentro hacia afuera):
- Domain no depende de ninguna otra capa.
- Application depende solo de Domain (contratos/DTOs, casos de uso).
- Infrastructure depende de Application y Domain (implementa interfaces, acceso a datos).
- Presentation depende de Application (usa casos de uso/servicios) y modela la API.

Flujo típico de una petición:
1. Controller (Presentation) recibe la request y valida parámetros.
2. Invoca un servicio/caso de uso (Application) trabajando con DTOs e interfaces de repositorio.
3. Infrastructure implementa las interfaces y consulta MongoDB.
4. Application mapea entidades ? DTOs y retorna el resultado al Controller.
5. Controller responde con el contrato HTTP (códigos, paginación, etc.).

Patrones y decisiones clave:
- Dependency Injection: servicios y repositorios registrados en Program.cs.
- Repository Pattern con MongoDB Driver y filtros/paginación eficientes.
- DTOs + servicio de mapeo para aislar el dominio del contrato HTTP.
- Middleware centralizado de excepciones y validaciones por atributos.
- IDs secuenciales con colección de counters en MongoDB.
- Swagger/OpenAPI con generación y UI para documentación.
- CORS habilitado con políticas por entorno (DevelopmentCORS, AllowReactApp).

Trade-offs:
- Más archivos/abstracciones y curva de entrada mayor.
- Beneficia proyectos con evolución de negocio, múltiples integraciones y necesidades de testeo/escala.

## Modelo de Datos

Entidades principales:

- Owner
  - IdOwner: int
  - Name: string
  - Address: string
  - Photo: string (URL)
  - Birthday: DateTime

- Property
  - IdProperty: int
  - Name: string
  - Address: string
  - Price: decimal
  - CodeInternal: string
  - Year: int
  - IdOwner: int (FK)

- PropertyImage
  - IdPropertyImage: int
  - IdProperty: int (FK)
  - File: string (URL)
  - Enabled: bool

- PropertyTrace
  - IdPropertyTrace: int
  - DateSale: DateTime
  - Name: string
  - Value: decimal
  - Tax: decimal
  - IdProperty: int (FK)

Relaciones:
- Owner 1:N Property
- Property 1:N PropertyImage
- Property 1:N PropertyTrace

## CORS

CORS ya está configurado:
- Entorno Development: política "DevelopmentCORS" (AllowAnyOrigin, AllowAnyMethod, AllowAnyHeader)
- Otros entornos: política "AllowReactApp" con orígenes permitidos
  - http://localhost:3000
  - https://localhost:5064
  - Permite credenciales

Puedes ajustar los orígenes en Program.cs.

## Configuración (appsettings.json)

```json
{
  "MongoDbSettings": {
    "ConnectionString": "mongodb://localhost:27017",
    "DatabaseName": "realestate_db",
    "PropertiesCollectionName": "properties",
    "OwnersCollectionName": "owners",
    "PropertyImagesCollectionName": "propertyimages",
    "PropertyTracesCollectionName": "propertytraces"
  }
}
```

## API Reference

### Lista completa de endpoints

- Health
  - GET /api/health
  - GET /api/health/ping

- Properties
  - GET /api/properties
  - GET /api/properties/{id}
  - POST /api/properties
  - PUT /api/properties/{id}
  - DELETE /api/properties/{id}
  - GET /api/properties/owner/{ownerId}

- Owners
  - GET /api/owners
  - GET /api/owners/{id}
  - POST /api/owners
  - PUT /api/owners/{id}
  - DELETE /api/owners/{id}

- Property Images
  - GET /api/propertyimages
  - GET /api/propertyimages/{id}
  - GET /api/propertyimages/property/{propertyId}
  - POST /api/propertyimages
  - PUT /api/propertyimages/{id}
  - DELETE /api/propertyimages/{id}

- Property Traces
  - GET /api/propertytraces
  - GET /api/propertytraces/{id}
  - GET /api/propertytraces/property/{propertyId}
  - POST /api/propertytraces
  - PUT /api/propertytraces/{id}
  - DELETE /api/propertytraces/{id}

### Filtros avanzados (GET /api/properties)

```http
GET /api/properties?name=Casa&address=Bogotá&priceMin=100000&priceMax=500000&page=1&pageSize=10&sortField=Price&sortDescending=false
```

| Parámetro | Tipo | Descripción |
|-----------|------|-------------|
| name | string? | Filtro por nombre |
| address | string? | Filtro por dirección |
| priceMin | decimal? | Precio mínimo |
| priceMax | decimal? | Precio máximo |
| page | int | Página (default: 1) |
| pageSize | int | Elementos por página |
| sortField | string? | Campo de ordenamiento |
| sortDescending | bool | Orden descendente |

Respuesta típica:

```json
{
  "data": [
    {
      "id": 1,
      "name": "Casa en Bogotá",
      "address": "Calle 123 #45-67",
      "price": 350000000,
      "codeInternal": "PROP-001",
      "year": 2020,
      "idOwner": 1
    }
  ],
  "totalCount": 1,
  "page": 1,
  "pageSize": 10
}
```

## Testing

Herramientas:
- NUnit: framework de testing.
- Moq: mocks y stubs para aislar dependencias en unit tests.
- Microsoft.AspNetCore.Mvc.Testing: WebApplicationFactory para tests de integración con servidor en memoria.

Tipos de tests y cómo funcionan:
- Unit tests (carpeta RealEstate.Tests/Unit)
  - OwnersControllerTests: mockea IOwnerRepository e IMapperService, invoca acciones del controller y verifica códigos (Ok, NotFound) y payloads.
  - PropertyRepositoryTests: valida parámetros y configuración usando IOptions<MongoDbSettings> mockeado; no toca la base de datos.

- Integration tests (carpeta RealEstate.Tests/Integration)
  - PropertiesIntegrationTests: usa WebApplicationFactory<Program> para levantar la API en memoria y HttpClient para llamar endpoints reales (/api/properties, /api/owners, etc.).
  - Verifica status 200 y, en algunos casos, estructura básica del JSON (campos data y totalCount).
  - No requiere ejecutar la API por separado; el servidor de pruebas se hospeda en memoria.

Ejecución:
```sh
# Ejecutar todo
dotnet test

# Solo integración
dotnet test --filter "FullyQualifiedName~RealEstate.Tests.Integration"

# Solo unitarios
dotnet test --filter "FullyQualifiedName~RealEstate.Tests.Unit"
```

Notas:
- Los tests de integración usan la configuración de la aplicación; si necesitas aislar MongoDB para pruebas, crea una WebApplicationFactory personalizada que reemplace la configuración/servicios.
- Los tests de controllers no dependen de MongoDB gracias a los mocks.
