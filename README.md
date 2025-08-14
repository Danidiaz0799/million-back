# Real Estate API

[![.NET 8](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet)](https://dotnet.microsoft.com/)
[![MongoDB](https://img.shields.io/badge/MongoDB-6.0-47A248?style=for-the-badge&logo=mongodb)](https://www.mongodb.com/)
[![Swagger](https://img.shields.io/badge/Swagger-OpenAPI-85EA2D?style=for-the-badge&logo=swagger)](https://swagger.io/)
[![Clean Architecture](https://img.shields.io/badge/Architecture-Clean-blue?style=for-the-badge)](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)

## Descripci�n

**Real Estate API** es una API RESTful robusta para la gesti�n completa de propiedades inmobiliarias, desarrollada con .NET 8 y MongoDB. Implementa Clean Architecture para garantizar mantenibilidad, testabilidad y escalabilidad.

### Caracter�sticas Principales

- **Gesti�n Completa de Propiedades**: CRUD con filtros avanzados y paginaci�n
- **Sistema de Propietarios**: Gesti�n completa de owners con validaciones
- **Trazabilidad de Transacciones**: Hist�rico completo de property traces
- **Gesti�n de Im�genes**: Sistema de galer�a multimedia por propiedad
- **B�squeda Avanzada**: Filtros por nombre, direcci�n y rango de precios
- **Paginaci�n Inteligente**: Sistema optimizado de paginaci�n
- **Clean Architecture**: Arquitectura escalable y mantenible
- **Documentaci�n Swagger**: API completamente documentada
- **Testing Completo**: Unit tests e integration tests

## Arquitectura de la Aplicaci�n

### Clean Architecture - 4 Capas

```
???????????????????????????????????????????????????????????????
?                    PRESENTATION LAYER                      ?
?             Controllers ? Middleware ? Swagger             ?
???????????????????????????????????????????????????????????????
?                   APPLICATION LAYER                        ?
?              DTOs ? Interfaces ? Services                   ?
???????????????????????????????????????????????????????????????
?                      DOMAIN LAYER                          ?
?          Entities ? Business Logic ? Core Models           ?
???????????????????????????????????????????????????????????????
?                  INFRASTRUCTURE LAYER                      ?
?          Repositories ? MongoDB ? External Services        ?
???????????????????????????????????????????????????????????????
                               ?
                               ?
???????????????????????????????????????????????????????????????
?                    MONGODB DATABASE                        ?
?    Properties ? Owners ? Images ? Traces ? Counters        ?
???????????????????????????????????????????????????????????????
```

### �Por qu� Clean Architecture?

#### **Separaci�n de Responsabilidades**
- **Domain**: L�gica de negocio pura, sin dependencias externas
- **Application**: Casos de uso y reglas de aplicaci�n
- **Infrastructure**: Detalles t�cnicos (BD, APIs externas)
- **Presentation**: Controladores y presentaci�n de datos

#### **Dependency Inversion**
- Las capas internas no conocen las externas
- Interfaces definen contratos, no implementaciones
- F�cil testing y intercambio de componentes

## Stack Tecnol�gico

- **.NET 8** - Framework principal con �ltimas caracter�sticas
- **MongoDB** - Base de datos NoSQL flexible y escalable
- **Swagger/OpenAPI** - Documentaci�n interactiva de API
- **xUnit** - Framework de testing robusto
- **Clean Architecture** - Patr�n arquitect�nico enterprise
- **Dependency Injection** - Inversi�n de control nativa de .NET

## Modelo de Datos

### Entidades Principales

```
Owner (Propietario)
?? IdOwner: int
?? Name: string
?? Address: string
?? Photo: string (URL)
?? Birthday: DateTime

Property (Propiedad)
?? IdProperty: int
?? Name: string
?? Address: string
?? Price: decimal
?? CodeInternal: string
?? Year: int
?? IdOwner: int (FK)

PropertyImage (Imagen)
?? IdPropertyImage: int
?? IdProperty: int (FK)
?? File: string (URL)
?? Enabled: bool

PropertyTrace (Transacci�n)
?? IdPropertyTrace: int
?? DateSale: DateTime
?? Name: string
?? Value: decimal
?? Tax: decimal
?? IdProperty: int (FK)
```

### Relaciones

```
Owner ???
        ? 1:N
        ???> Property ?????> PropertyImage (1:N)
                        ?
                        ???> PropertyTrace (1:N)
```

## Inicio R�pido

### 1. Prerrequisitos

```bash
# .NET 8 SDK
winget install Microsoft.DotNet.SDK.8

# MongoDB Community Server
winget install MongoDB.Server
```

### 2. Configurar Base de Datos

```bash
# Inicializar MongoDB con datos de prueba
mongo < reset-database.js
```

### 3. Ejecutar API

```bash
# Restaurar dependencias y ejecutar
dotnet restore
dotnet run --project RealEstate.Api
```

### 4. Acceder a Swagger

```
http://localhost:5065/swagger
```

## API Reference

### Endpoints Principales

| Recurso | Endpoint | M�todos | Descripci�n |
|---------|----------|---------|-------------|
| **Properties** | `/api/properties` | `GET, POST` | Gesti�n de propiedades |
| **Properties** | `/api/properties/{id}` | `GET, PUT, DELETE` | CRUD espec�fico |
| **Owners** | `/api/owners` | `GET, POST` | Gesti�n de propietarios |
| **Owners** | `/api/owners/{id}` | `GET, PUT, DELETE` | CRUD espec�fico |
| **Images** | `/api/propertyimages` | `GET, POST, DELETE` | Galer�a de im�genes |
| **Traces** | `/api/propertytraces` | `GET, POST, DELETE` | Historial de transacciones |
| **Health** | `/api/health` | `GET` | Estado de la API |

### Filtros Avanzados

**GET /api/properties** soporta:

```http
GET /api/properties?name=Casa&address=Bogot�&priceMin=100000&priceMax=500000&page=1&pageSize=10&sortField=Price&sortDescending=false
```

| Par�metro | Tipo | Descripci�n | Ejemplo |
|-----------|------|-------------|---------|
| `name` | `string?` | Filtro por nombre | `Casa`, `Apartamento` |
| `address` | `string?` | Filtro por direcci�n | `Bogot�`, `Medell�n` |
| `priceMin` | `decimal?` | Precio m�nimo | `100000` |
| `priceMax` | `decimal?` | Precio m�ximo | `500000` |
| `page` | `int` | P�gina (default: 1) | `1`, `2`, `3` |
| `pageSize` | `int` | Elementos por p�gina | `10`, `20`, `50` |
| `sortField` | `string?` | Campo de ordenamiento | `Name`, `Price`, `Year` |
| `sortDescending` | `bool` | Orden descendente | `true`, `false` |

### Respuesta T�pica

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

## Conexi�n MongoDB

### Configuraci�n

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

### Implementaci�n

```csharp
// Dependency Injection en Program.cs
builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDbSettings"));
builder.Services.AddScoped<IPropertyRepository, PropertyRepository>();

// Repository Pattern con MongoDB Driver
public class PropertyRepository : IPropertyRepository
{
    private readonly IMongoCollection<Property> _collection;
    
    public PropertyRepository(IOptions<MongoDbSettings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        var database = client.GetDatabase(settings.Value.DatabaseName);
        _collection = database.GetCollection<Property>("properties");
    }
    
    // Filtros avanzados con MongoDB Builders
    public async Task<(IEnumerable<Property>, long)> GetPropertiesAsync(...)
    {
        var builder = Builders<Property>.Filter;
        var filters = new List<FilterDefinition<Property>>();
        
        if (!string.IsNullOrWhiteSpace(name))
            filters.Add(builder.Regex(x => x.Name, new BsonRegularExpression(name, "i")));
            
        // ... m�s filtros
    }
}
```

## Caracter�sticas T�cnicas

### Funcionalidades Implementadas

- **Validaciones Robustas** - Data Annotations + ModelState
- **Auto-incremento** - IDs secuenciales con MongoDB counters
- **CORS Configurado** - Listo para frontend React/Next.js
- **Exception Handling** - Middleware centralizado de errores
- **Paginaci�n Optimizada** - Sistema eficiente de paginaci�n
- **�ndices de BD** - MongoDB optimizado para b�squedas
- **Repository Pattern** - Abstracci�n de acceso a datos
- **Dependency Injection** - Inversi�n de control nativa
- **Swagger UI** - Documentaci�n interactiva completa

### Testing

```bash
# Ejecutar todos los tests
dotnet test RealEstate.Tests

# Tests disponibles:
? Unit Tests: PropertyRepository, Controllers, Services
? Integration Tests: API Endpoints completos
? Coverage: 13/13 tests passing
```

## Scripts de Utilidad

| Script | Descripci�n | Comando |
|--------|-------------|---------|
| `reset-database.js` | Reset completo con datos | `mongo < reset-database.js` |
| `clean-all.js` | Limpiar BD completamente | `mongo < clean-all.js` |
| `clean-db.bat` | Script interactivo Windows | `./clean-db.bat` |

## Pr�ximas Mejoras

- [ ] Autenticaci�n JWT
- [ ] Logging estructurado (Serilog)
- [ ] Redis Caching
- [ ] Docker containerization
- [ ] Application Insights
- [ ] CQRS Pattern

---

## Contacto

**Desarrollado para integraci�n con Frontend React/Next.js** 

**API Endpoint**: `http://localhost:5065`  
**Swagger**: `http://localhost:5065/swagger`  
**Estado**: Listo para producci�n

---

[![Built with ??](https://img.shields.io/badge/Built%20with-??-red?style=for-the-badge)]() [![Clean Architecture](https://img.shields.io/badge/Clean-Architecture-blue?style=for-the-badge)]() [![.NET 8](https://img.shields.io/badge/.NET-8.0-purple?style=for-the-badge)]()
