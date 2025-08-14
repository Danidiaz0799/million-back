// Seleccionar base de datos
db = db.getSiblingDB('realestate_db');

// Crear colección de owners y datos de ejemplo
db.owners.insertMany([
    {
        IdOwner: 1,
        Name: "Juan Pérez",
        Address: "Calle 10 #20-30, Bogotá",
        Photo: "https://via.placeholder.com/150",
        Birthday: new Date("1980-05-15")
    },
    {
        IdOwner: 2,
        Name: "María González",
        Address: "Carrera 15 #30-45, Medellín",
        Photo: "https://via.placeholder.com/150",
        Birthday: new Date("1975-08-22")
    },
    {
        IdOwner: 3,
        Name: "Carlos Rodríguez",
        Address: "Avenida 20 #50-60, Cali",
        Photo: "https://via.placeholder.com/150",
        Birthday: new Date("1985-12-10")
    }
]);

// Crear colección de properties y datos de ejemplo
db.properties.insertMany([
    {
        IdProperty: 1,
        Name: "Casa en Bogotá",
        Address: "Calle 123 #45-67",
        Price: 350000000,
        CodeInternal: "PROP-001",
        Year: 2020,
        IdOwner: 1
    },
    {
        IdProperty: 2,
        Name: "Apartamento en Medellín",
        Address: "Carrera 50 #10-20",
        Price: 250000000,
        CodeInternal: "PROP-002",
        Year: 2018,
        IdOwner: 2
    },
    {
        IdProperty: 3,
        Name: "Finca en Boyacá",
        Address: "Vereda El Paraíso",
        Price: 450000000,
        CodeInternal: "PROP-003",
        Year: 2015,
        IdOwner: 3
    }
]);

// Crear colección de property images
db.propertyimages.insertMany([
    {
        IdPropertyImage: 1,
        IdProperty: 1,
        File: "https://via.placeholder.com/800x600/1.jpg",
        Enabled: true
    },
    {
        IdPropertyImage: 2,
        IdProperty: 1,
        File: "https://via.placeholder.com/800x600/2.jpg",
        Enabled: true
    },
    {
        IdPropertyImage: 3,
        IdProperty: 2,
        File: "https://via.placeholder.com/800x600/3.jpg",
        Enabled: true
    },
    {
        IdPropertyImage: 4,
        IdProperty: 3,
        File: "https://via.placeholder.com/800x600/4.jpg",
        Enabled: true
    }
]);

// Crear colección de property traces
db.propertytraces.insertMany([
    {
        IdPropertyTrace: 1,
        DateSale: new Date("2023-01-15"),
        Name: "Venta inicial",
        Value: 350000000,
        Tax: 3500000,
        IdProperty: 1
    },
    {
        IdPropertyTrace: 2,
        DateSale: new Date("2023-06-20"),
        Name: "Valorización",
        Value: 360000000,
        Tax: 3600000,
        IdProperty: 1
    },
    {
        IdPropertyTrace: 3,
        DateSale: new Date("2023-03-10"),
        Name: "Venta inicial",
        Value: 250000000,
        Tax: 2500000,
        IdProperty: 2
    },
    {
        IdPropertyTrace: 4,
        DateSale: new Date("2023-02-05"),
        Name: "Venta inicial",
        Value: 450000000,
        Tax: 4500000,
        IdProperty: 3
    }
]);

// Crear índices para búsquedas y filtros
db.properties.createIndex({ Name: "text", Address: "text" });
db.properties.createIndex({ Price: 1 });
db.properties.createIndex({ IdOwner: 1 });
db.owners.createIndex({ Name: 1 });
db.propertyimages.createIndex({ IdProperty: 1 });
db.propertytraces.createIndex({ IdProperty: 1 });
// Crear colección de contadores para IDs autoincrementales
db.counters.insertMany([
    { _id: "propertyid", seq: 3 },
    { _id: "ownerid", seq: 3 },
    { _id: "propertyimageid", seq: 4 },
    { _id: "propertytraceid", seq: 4 }
]);

console.log("Base de datos inicializada correctamente con todas las colecciones y datos de ejemplo");
