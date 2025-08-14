// Seleccionar base de datos
db = db.getSiblingDB('realestate_db');

// Crear colecci�n y datos de ejemplo
db.properties.insertMany([
    {
        Name: "Casa en Bogot�",
        Address: "Calle 123 #45-67",
        Price: 350000000,
        OwnerId: "owner1",
        ImageUrl: "https://via.placeholder.com/300"
    },
    {
        Name: "Apartamento en Medell�n",
        Address: "Carrera 50 #10-20",
        Price: 250000000,
        OwnerId: "owner2",
        ImageUrl: "https://via.placeholder.com/300"
    },
    {
        Name: "Finca en Boyac�",
        Address: "Vereda El Para�so",
        Price: 450000000,
        OwnerId: "owner3",
        ImageUrl: "https://via.placeholder.com/300"
    }
]);

// Crear �ndices para b�squedas y filtros
db.properties.createIndex({ Name: "text", Address: "text" });
db.properties.createIndex({ Price: 1 });
