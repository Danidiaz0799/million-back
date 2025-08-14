# Real Estate API

[![.NET 8](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet)](https://dotnet.microsoft.com/)
[![MongoDB](https://img.shields.io/badge/MongoDB-6.0-47A248?style=for-the-badge&logo=mongodb)](https://www.mongodb.com/)
[![Swagger](https://img.shields.io/badge/Swagger-OpenAPI-85EA2D?style=for-the-badge&logo=swagger)](https://swagger.io/)
[![Clean Architecture](https://img.shields.io/badge/Architecture-Clean-blue?style=for-the-badge)](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)

## Descripción

**Real Estate API** es una API RESTful robusta para la gestión completa de propiedades inmobiliarias, desarrollada con .NET 8 y MongoDB. Implementa Clean Architecture para garantizar mantenibilidad, testabilidad y escalabilidad.

### Características Principales

- **Gestión Completa de Propiedades**: CRUD con filtros avanzados y paginación
- **Sistema de Propietarios**: Gestión completa de owners con validaciones
- **Trazabilidad de Transacciones**: Histórico completo de property traces
- **Gestión de Imágenes**: Sistema de galería multimedia por propiedad
- **Búsqueda Avanzada**: Filtros por nombre, dirección y rango de precios
- **Paginación Inteligente**: Sistema optimizado de paginación
- **Clean Architecture**: Arquitectura escalable y mantenible
- **Documentación Swagger**: API completamente documentada
- **Testing Completo**: Unit tests e integration tests

## Arquitectura de la Aplicación

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

### ¿Por qué Clean Architecture?

#### **Separación de Responsabilidades**
- **Domain**: Lógica de negocio pura, sin dependencias externas
- **Application**: Casos de uso y reglas de aplicación
- **Infrastructure**: Detalles técnicos (BD, APIs externas)
- **Presentation**: Controladores y presentación de datos

#### **Dependency Inversion**
- Las capas internas no conocen las externas
- Interfaces definen contratos, no implementaciones
- Fácil testing y intercambio de componentes

## Stack Tecnológico

- **.NET 8** - Framework principal con últimas características
- **MongoDB** - Base de datos NoSQL flexible y escalable
- **Swagger/OpenAPI** - Documentación interactiva de API
- **xUnit** - Framework de testing robusto
- **Clean Architecture** - Patrón arquitectónico enterprise
- **Dependency Injection** - Inversión de control nativa de .NET

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

PropertyTrace (Transacción)
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

## Inicio Rápido

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

| Recurso | Endpoint | Métodos | Descripción |
|---------|----------|---------|-------------|
| **Properties** | `/api/properties` | `GET, POST` | Gestión de propiedades |
| **Properties** | `/api/properties/{id}` | `GET, PUT, DELETE` | CRUD específico |
| **Owners** | `/api/owners` | `GET, POST` | Gestión de propietarios |
| **Owners** | `/api/owners/{id}` | `GET, PUT, DELETE` | CRUD específico |
| **Images** | `/api/propertyimages` | `GET, POST, DELETE` | Galería de imágenes |
| **Traces** | `/api/propertytraces` | `GET, POST, DELETE` | Historial de transacciones |
| **Health** | `/api/health` | `GET` | Estado de la API |

### Filtros Avanzados

**GET /api/properties** soporta:

```http
GET /api/properties?name=Casa&address=Bogotá&priceMin=100000&priceMax=500000&page=1&pageSize=10&sortField=Price&sortDescending=false
```

| Parámetro | Tipo | Descripción | Ejemplo |
|-----------|------|-------------|---------|
| `name` | `string?` | Filtro por nombre | `Casa`, `Apartamento` |
| `address` | `string?` | Filtro por dirección | `Bogotá`, `Medellín` |
| `priceMin` | `decimal?` | Precio mínimo | `100000` |
| `priceMax` | `decimal?` | Precio máximo | `500000` |
| `page` | `int` | Página (default: 1) | `1`, `2`, `3` |
| `pageSize` | `int` | Elementos por página | `10`, `20`, `50` |
| `sortField` | `string?` | Campo de ordenamiento | `Name`, `Price`, `Year` |
| `sortDescending` | `bool` | Orden descendente | `true`, `false` |

### Respuesta Típica

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

## Conexión MongoDB

### Configuración

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

### Implementación

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
            
        // ... más filtros
    }
}
```

## Características Técnicas

### Funcionalidades Implementadas

- **Validaciones Robustas** - Data Annotations + ModelState
- **Auto-incremento** - IDs secuenciales con MongoDB counters
- **CORS Configurado** - Listo para frontend React/Next.js
- **Exception Handling** - Middleware centralizado de errores
- **Paginación Optimizada** - Sistema eficiente de paginación
- **Índices de BD** - MongoDB optimizado para búsquedas
- **Repository Pattern** - Abstracción de acceso a datos
- **Dependency Injection** - Inversión de control nativa
- **Swagger UI** - Documentación interactiva completa

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

| Script | Descripción | Comando |
|--------|-------------|---------|
| `reset-database.js` | Reset completo con datos | `mongo < reset-database.js` |
| `clean-all.js` | Limpiar BD completamente | `mongo < clean-all.js` |
| `clean-db.bat` | Script interactivo Windows | `./clean-db.bat` |

## Próximas Mejoras

- [ ] Autenticación JWT
- [ ] Logging estructurado (Serilog)
- [ ] Redis Caching
- [ ] Docker containerization
- [ ] Application Insights
- [ ] CQRS Pattern

---

## Contacto

**Desarrollado para integración con Frontend React/Next.js** 

**API Endpoint**: `http://localhost:5065`  
**Swagger**: `http://localhost:5065/swagger`  
**Estado**: Listo para producción

---

[![Built with ??](https://img.shields.io/badge/Built%20with-??-red?style=for-the-badge)]() [![Clean Architecture](https://img.shields.io/badge/Clean-Architecture-blue?style=for-the-badge)]() [![.NET 8](https://img.shields.io/badge/.NET-8.0-purple?style=for-the-badge)]()
