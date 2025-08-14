// Seleccionar base de datos
db = db.getSiblingDB('realestate_db');

// Crear colección y datos de ejemplo
db.properties.insertMany([
    {
        Name: "Casa en Bogotá",
        Address: "Calle 123 #45-67",
        Price: 350000000,
        OwnerId: "owner1",
        ImageUrl: "https://via.placeholder.com/300"
    },
    {
        Name: "Apartamento en Medellín",
        Address: "Carrera 50 #10-20",
        Price: 250000000,
        OwnerId: "owner2",
        ImageUrl: "https://via.placeholder.com/300"
    },
    {
        Name: "Finca en Boyacá",
        Address: "Vereda El Paraíso",
        Price: 450000000,
        OwnerId: "owner3",
        ImageUrl: "https://via.placeholder.com/300"
    }
]);

// Crear índices para búsquedas y filtros
db.properties.createIndex({ Name: "text", Address: "text" });
db.properties.createIndex({ Price: 1 });
