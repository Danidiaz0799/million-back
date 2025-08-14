# Real Estate Management API

## Descripción del Proyecto

Este es un sistema de gestión de propiedades inmobiliarias desarrollado en .NET 8 con MongoDB como base de datos. El sistema permite gestionar propiedades, propietarios, imágenes de propiedades y trazabilidad de transacciones.

## Arquitectura

El proyecto sigue una arquitectura limpia con separación de responsabilidades:

- **RealEstate.Api**: Capa de presentación con controladores API
- **RealEstate.Application**: Capa de aplicación con interfaces y servicios
- **RealEstate.Domain**: Capa de dominio con entidades
- **RealEstate.Infrastructure**: Capa de infraestructura con repositorios
- **RealEstate.Tests**: Proyecto de pruebas unitarias

## Entidades del Dominio

### 1. Owner (Propietario)
- IdOwner: Identificador único
- Name: Nombre del propietario
- Address: Dirección
- Photo: URL de la foto
- Birthday: Fecha de nacimiento

### 2. Property (Propiedad)
- IdProperty: Identificador único
- Name: Nombre de la propiedad
- Address: Dirección de la propiedad
- Price: Precio
- CodeInternal: Código interno
- Year: Año de construcción
- IdOwner: Referencia al propietario

### 3. PropertyImage (Imagen de Propiedad)
- IdPropertyImage: Identificador único
- IdProperty: Referencia a la propiedad
- File: URL del archivo de imagen
- Enabled: Estado activo/inactivo

### 4. PropertyTrace (Trazabilidad de Propiedad)
- IdPropertyTrace: Identificador único
- DateSale: Fecha de venta
- Name: Nombre de la transacción
- Value: Valor de la transacción
- Tax: Impuesto
- IdProperty: Referencia a la propiedad

## Endpoints API

### Properties
- `GET /api/properties` - Lista propiedades con filtros y paginación
- `GET /api/properties/{id}` - Obtiene una propiedad por ID
- `POST /api/properties` - Crea una nueva propiedad
- `PUT /api/properties/{id}` - Actualiza una propiedad
- `DELETE /api/properties/{id}` - Elimina una propiedad
- `GET /api/properties/owner/{ownerId}` - Obtiene propiedades por propietario

### Owners
- `GET /api/owners` - Lista todos los propietarios
- `GET /api/owners/{id}` - Obtiene un propietario por ID
- `POST /api/owners` - Crea un nuevo propietario
- `PUT /api/owners/{id}` - Actualiza un propietario
- `DELETE /api/owners/{id}` - Elimina un propietario

### PropertyImages
- `GET /api/propertyimages/property/{propertyId}` - Obtiene imágenes por propiedad
- `GET /api/propertyimages/{id}` - Obtiene una imagen por ID
- `POST /api/propertyimages` - Agrega una nueva imagen
- `PUT /api/propertyimages/{id}` - Actualiza una imagen
- `DELETE /api/propertyimages/{id}` - Elimina una imagen

### PropertyTraces
- `GET /api/propertytraces/property/{propertyId}` - Obtiene trazas por propiedad
- `GET /api/propertytraces/{id}` - Obtiene una traza por ID
- `POST /api/propertytraces` - Crea una nueva traza
- `PUT /api/propertytraces/{id}` - Actualiza una traza
- `DELETE /api/propertytraces/{id}` - Elimina una traza

## Configuración

### MongoDB
El proyecto utiliza MongoDB como base de datos. Configurar la cadena de conexión en `appsettings.json`:

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

### Inicialización de Base de Datos
Ejecutar el script `mongo-setup.js` en MongoDB para crear las colecciones y datos de ejemplo.

## Cómo Ejecutar

1. **Instalar MongoDB**: Asegúrate de tener MongoDB corriendo en localhost:27017
2. **Ejecutar script de inicialización**: `mongo < mongo-setup.js`
3. **Restaurar paquetes**: `dotnet restore`
4. **Ejecutar el proyecto**: `dotnet run --project RealEstate.Api`
5. **Acceder a Swagger**: `https://localhost:5001/swagger`

## Tecnologías Utilizadas

- **.NET 8**: Framework principal
- **ASP.NET Core**: Para la API REST
- **MongoDB Driver**: Para la conexión con MongoDB
- **Swagger/OpenAPI**: Para documentación de la API
- **xUnit**: Para pruebas unitarias
- **Moq**: Para mocking en pruebas

## Características Implementadas

? **Arquitectura Limpia**: Separación clara de responsabilidades
? **Patrón Repository**: Para acceso a datos
? **Inyección de Dependencias**: Configurada correctamente
? **Validación de Modelos**: Con Data Annotations
? **Manejo de Errores**: Middleware personalizado
? **Paginación**: En listado de propiedades
? **Filtros de Búsqueda**: Por nombre, dirección y rango de precios
? **Documentación API**: Con Swagger
? **Pruebas Unitarias**: Proyecto de pruebas configurado

## Próximas Mejoras

- Implementar autenticación y autorización
- Agregar más validaciones de negocio
- Implementar caché
- Agregar logs estructurados
- Implementar paginación en todos los endpoints
- Agregar más pruebas unitarias e integración