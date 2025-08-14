# Real Estate API

API RESTful para gestión de propiedades inmobiliarias desarrollada con .NET 8, MongoDB y Clean Architecture.

## ??? Arquitectura

**Clean Architecture** con separación de responsabilidades en 4 capas:

```
???????????????????
?   API Layer     ?  ? Controllers, Middleware, Swagger
???????????????????
? Application     ?  ? DTOs, Interfaces, Services
???????????????????
?    Domain       ?  ? Entities (Property, Owner, etc.)
???????????????????
? Infrastructure  ?  ? Repositories, MongoDB, Config
???????????????????
```

## ??? Stack Tecnológico

- **.NET 8** - Framework principal
- **MongoDB** - Base de datos NoSQL
- **Swagger** - Documentación de API
- **xUnit** - Testing framework
- **Clean Architecture** - Patrón arquitectónico

## ?? Modelo de Datos

```
Owner ???
        ?
        ???> Property ?????> PropertyImage
                        ?
                        ???> PropertyTrace
```

- **Owner**: Propietarios
- **Property**: Propiedades inmobiliarias
- **PropertyImage**: Galería de imágenes
- **PropertyTrace**: Historial de transacciones

## ?? Instalación y Uso

### 1. Prerrequisitos
```bash
# .NET 8 SDK
# MongoDB Server (localhost:27017)
```

### 2. Configurar Base de Datos
```bash
# Ejecutar desde el directorio raíz
mongo < reset-database.js
```

### 3. Ejecutar API
```bash
dotnet run --project RealEstate.Api
```

### 4. Acceder a Swagger
```
http://localhost:5065/swagger
```

## ?? Endpoints Principales

| Endpoint | Método | Descripción |
|----------|--------|-------------|
| `/api/properties` | GET | Listar propiedades con filtros |
| `/api/properties/{id}` | GET/PUT/DELETE | CRUD de propiedades |
| `/api/owners` | GET/POST | Gestión de propietarios |
| `/api/propertyimages` | GET/POST/DELETE | Galería de imágenes |
| `/api/propertytraces` | GET/POST/DELETE | Historial de transacciones |

## ?? Filtros Avanzados

**GET /api/properties** soporta:
- `name` - Búsqueda por nombre
- `address` - Filtro por dirección  
- `priceMin/priceMax` - Rango de precios
- `page/pageSize` - Paginación
- `sortField/sortDescending` - Ordenamiento

**Ejemplo:**
```
GET /api/properties?name=Casa&priceMin=100000&page=1&pageSize=10
```

## ??? Conexión MongoDB

### Configuración (appsettings.json)
```json
{
  "MongoDbSettings": {
    "ConnectionString": "mongodb://localhost:27017",
    "DatabaseName": "realestate_db"
  }
}
```

### Implementación
```csharp
// Dependency Injection
services.Configure<MongoDbSettings>(configuration.GetSection("MongoDbSettings"));
services.AddScoped<IPropertyRepository, PropertyRepository>();

// Repository Pattern
public class PropertyRepository : IPropertyRepository
{
    private readonly IMongoCollection<Property> _collection;
    
    public PropertyRepository(IOptions<MongoDbSettings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        _database = client.GetDatabase(settings.Value.DatabaseName);
        _collection = _database.GetCollection<Property>("properties");
    }
}
```

## ?? Características

- **? CRUD Completo** - Todas las entidades
- **? Filtros Avanzados** - Búsqueda y paginación
- **? Validaciones** - Data annotations y ModelState
- **? Auto-incremento** - IDs secuenciales en MongoDB  
- **? CORS** - Configurado para frontend
- **? Exception Handling** - Middleware centralizado
- **? Testing** - Unit e Integration tests
- **? Swagger** - Documentación interactiva

## ?? Testing

```bash
# Ejecutar todos los tests
dotnet test RealEstate.Tests

# Resultado esperado: 13/13 tests passing
```

## ??? Scripts de Utilidad

- `reset-database.js` - Resetear BD con datos de prueba
- `clean-all.js` - Limpiar BD completamente  
- `clean-db.bat` - Script interactivo de limpieza

## ?? Respuesta Típica

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

---

**Proyecto listo para integración con frontend React/Next.js** ??
