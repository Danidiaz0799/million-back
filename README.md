# Real Estate Management API

## Descripci�n del Proyecto

Este es un sistema de gesti�n de propiedades inmobiliarias desarrollado en .NET 8 con MongoDB como base de datos. El sistema permite gestionar propiedades, propietarios, im�genes de propiedades y trazabilidad de transacciones.

## Arquitectura

El proyecto sigue una arquitectura limpia con separaci�n de responsabilidades:

- **RealEstate.Api**: Capa de presentaci�n con controladores API
- **RealEstate.Application**: Capa de aplicaci�n con interfaces y servicios
- **RealEstate.Domain**: Capa de dominio con entidades
- **RealEstate.Infrastructure**: Capa de infraestructura con repositorios
- **RealEstate.Tests**: Proyecto de pruebas unitarias

## Entidades del Dominio

### 1. Owner (Propietario)
- IdOwner: Identificador �nico
- Name: Nombre del propietario
- Address: Direcci�n
- Photo: URL de la foto
- Birthday: Fecha de nacimiento

### 2. Property (Propiedad)
- IdProperty: Identificador �nico
- Name: Nombre de la propiedad
- Address: Direcci�n de la propiedad
- Price: Precio
- CodeInternal: C�digo interno
- Year: A�o de construcci�n
- IdOwner: Referencia al propietario

### 3. PropertyImage (Imagen de Propiedad)
- IdPropertyImage: Identificador �nico
- IdProperty: Referencia a la propiedad
- File: URL del archivo de imagen
- Enabled: Estado activo/inactivo

### 4. PropertyTrace (Trazabilidad de Propiedad)
- IdPropertyTrace: Identificador �nico
- DateSale: Fecha de venta
- Name: Nombre de la transacci�n
- Value: Valor de la transacci�n
- Tax: Impuesto
- IdProperty: Referencia a la propiedad

## Endpoints API

### Properties
- `GET /api/properties` - Lista propiedades con filtros y paginaci�n
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
- `GET /api/propertyimages/property/{propertyId}` - Obtiene im�genes por propiedad
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

## Configuraci�n

### MongoDB
El proyecto utiliza MongoDB como base de datos. Configurar la cadena de conexi�n en `appsettings.json`:

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

### Inicializaci�n de Base de Datos
Ejecutar el script `mongo-setup.js` en MongoDB para crear las colecciones y datos de ejemplo.

## C�mo Ejecutar

1. **Instalar MongoDB**: Aseg�rate de tener MongoDB corriendo en localhost:27017
2. **Ejecutar script de inicializaci�n**: `mongo < mongo-setup.js`
3. **Restaurar paquetes**: `dotnet restore`
4. **Ejecutar el proyecto**: `dotnet run --project RealEstate.Api`
5. **Acceder a Swagger**: `https://localhost:5001/swagger`

## Tecnolog�as Utilizadas

- **.NET 8**: Framework principal
- **ASP.NET Core**: Para la API REST
- **MongoDB Driver**: Para la conexi�n con MongoDB
- **Swagger/OpenAPI**: Para documentaci�n de la API
- **xUnit**: Para pruebas unitarias
- **Moq**: Para mocking en pruebas

## Caracter�sticas Implementadas

? **Arquitectura Limpia**: Separaci�n clara de responsabilidades
? **Patr�n Repository**: Para acceso a datos
? **Inyecci�n de Dependencias**: Configurada correctamente
? **Validaci�n de Modelos**: Con Data Annotations
? **Manejo de Errores**: Middleware personalizado
? **Paginaci�n**: En listado de propiedades
? **Filtros de B�squeda**: Por nombre, direcci�n y rango de precios
? **Documentaci�n API**: Con Swagger
? **Pruebas Unitarias**: Proyecto de pruebas configurado

## Pr�ximas Mejoras

- Implementar autenticaci�n y autorizaci�n
- Agregar m�s validaciones de negocio
- Implementar cach�
- Agregar logs estructurados
- Implementar paginaci�n en todos los endpoints
- Agregar m�s pruebas unitarias e integraci�n