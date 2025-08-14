# Real Estate API

[![.NET 8](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet)](https://dotnet.microsoft.com/)
[![MongoDB](https://img.shields.io/badge/MongoDB-6.0-47A248?style=for-the-badge&logo=mongodb)](https://www.mongodb.com/)
[![Swagger](https://img.shields.io/badge/Swagger-OpenAPI-85EA2D?style=for-the-badge&logo=swagger)](https://swagger.io/)
[![Clean Architecture](https://img.shields.io/badge/Architecture-Clean-blue?style=for-the-badge)](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)

API RESTful para gestionar propiedades inmobiliarias. Construida con .NET 8, MongoDB y principios de Clean Architecture.

## Caracter�sticas

- CRUD de propiedades con filtros y paginaci�n
- Gesti�n de propietarios (owners)
- Im�genes por propiedad (galer�a)
- Trazas/historial de transacciones
- B�squeda por nombre, direcci�n y rango de precios
- Swagger/OpenAPI y validaciones
- Tests unitarios e integrados

## Arquitectura (Clean Architecture)

- Presentation: Controllers, middleware, Swagger
- Application: DTOs, interfaces, servicios de aplicaci�n
- Domain: Entidades y reglas de negocio
- Infrastructure: Repositorios, MongoDB, servicios externos

Las capas internas no dependen de las externas (Dependency Inversion) y se inyectan a trav�s de interfaces.

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

## Inicio R�pido

1) Prerrequisitos

```sh
# .NET 8 SDK
winget install Microsoft.DotNet.SDK.8

# MongoDB Community Server
winget install MongoDB.Server
```

2) Configurar base de datos

```sh
# Inicializar MongoDB con datos de prueba
mongo < reset-database.js
```

3) Ejecutar API

```sh
# Restaurar dependencias y ejecutar
dotnet restore
dotnet run --project RealEstate.Api
```

4) Swagger

```
http://localhost:5065/swagger
```

## Configuraci�n (appsettings.json)

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
GET /api/properties?name=Casa&address=Bogot�&priceMin=100000&priceMax=500000&page=1&pageSize=10&sortField=Price&sortDescending=false
```

| Par�metro | Tipo | Descripci�n |
|-----------|------|-------------|
| name | string? | Filtro por nombre |
| address | string? | Filtro por direcci�n |
| priceMin | decimal? | Precio m�nimo |
| priceMax | decimal? | Precio m�ximo |
| page | int | P�gina (default: 1) |
| pageSize | int | Elementos por p�gina |
| sortField | string? | Campo de ordenamiento |
| sortDescending | bool | Orden descendente |

Respuesta t�pica:

```json
{
  "data": [
    {
      "id": 1,
      "name": "Casa en Bogot�",
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

```sh
dotnet test RealEstate.Tests
```

## Scripts de utilidad

| Script | Descripci�n | Comando |
|--------|-------------|---------|
| reset-database.js | Reset con datos | `mongo < reset-database.js` |
| clean-all.js | Limpiar BD | `mongo < clean-all.js` |
| clean-db.bat | Script Windows | `./clean-db.bat` |

## Roadmap

- Autenticaci�n JWT
- Logging estructurado (Serilog)
- Redis caching
- Docker
- Application Insights
- CQRS

## Contacto

- API: http://localhost:5065
- Swagger: http://localhost:5065/swagger
