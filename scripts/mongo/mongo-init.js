// Script para inicializar la base de datos con datos de ejemplo personalizados

const db = connect("localhost:27017/realestate_db");

db.owners.drop();
db.properties.drop();
db.propertyimages.drop();
db.propertytraces.drop();
db.counters.drop();

// Owners
// id = IdOwner
// birthday en formato ISO
// photo es URL
// address es string
// name es string
db.owners.insertMany([
  {
    IdOwner: 1,
    Name: "Steven Diaz",
    Address: "Calle 40",
    Photo: "https://img.freepik.com/vector-gratis/joven-capucha-naranja_1308-173533.jpg?semt=ais_hybrid&w=740&q=80",
    Birthday: new Date("1999-08-14T18:08:18.463Z")
  },
  {
    IdOwner: 2,
    Name: "Maria Antonieta",
    Address: "Calle 120",
    Photo: "https://png.pngtree.com/png-clipart/20231007/original/pngtree-person-in-graduation-cap-illustration-vector-png-image_12978054.png",
    Birthday: new Date("1997-08-14T18:08:18.463Z")
  },
  {
    IdOwner: 3,
    Name: "Vegeta",
    Address: "Tierra",
    Photo: "https://preview.redd.it/tf96t3nppi171.jpg?width=1080&crop=smart&auto=webp&s=7e1500aa10f2386e5a18f98e968de2164c6d330d",
    Birthday: new Date("1920-08-14T18:08:18.463Z")
  }
]);

// Properties
// id = IdProperty
// idOwner = IdOwner
// price = Price
// codeInternal = CodeInternal
// year = Year
db.properties.insertMany([
  {
    IdProperty: 1,
    Name: "Apartamento Medellin",
    Address: "Medellin calle 120",
    Price: 8500000,
    CodeInternal: "Med1",
    Year: 2020,
    IdOwner: 2
  },
  {
    IdProperty: 2,
    Name: "Casa grande Bogotá",
    Address: "Calle 40",
    Price: 13000000,
    CodeInternal: "Bog1",
    Year: 2025,
    IdOwner: 1
  },
  {
    IdProperty: 3,
    Name: "Apartamento en Playa",
    Address: "California",
    Price: 21000000,
    CodeInternal: "Cal1",
    Year: 2025,
    IdOwner: 1
  },
  {
    IdProperty: 4,
    Name: "Casa grande moderna",
    Address: "California",
    Price: 19000000,
    CodeInternal: "mod1",
    Year: 2021,
    IdOwner: 3
  }
]);

// Property Images
// id = IdPropertyImage
// idProperty = IdProperty
db.propertyimages.insertMany([
  {
    IdPropertyImage: 1,
    IdProperty: 1,
    File: "https://images.ctfassets.net/8lc7xdlkm4kt/5SAM0z4fBdLCQQZTDD2sfe/fd8486a3b1c63e1cc102345755967d0e/mint-apartamentos-barranquilla-balcon.jpg?w=1366&h=768&fl=progressive&q=60&fm=jpg",
    Enabled: true
  },
  {
    IdPropertyImage: 2,
    IdProperty: 2,
    File: "https://images.adsttc.com/media/images/5d67/e6f4/284d/d1be/6000/0254/newsletter/FEATURED_IMAGE.jpg?1567090404",
    Enabled: true
  },
  {
    IdPropertyImage: 3,
    IdProperty: 3,
    File: "https://heymondo.es/blog/wp-content/uploads/2023/08/Oceanside-Pier-en-California.jpg",
    Enabled: true
  },
  {
    IdPropertyImage: 4,
    IdProperty: 4,
    File: "https://soyarquitectura.mx/wp-content/uploads/2019/05/Fachada-Casa-moderna-dos-pisos-Nogal-blogc7.jpg",
    Enabled: true
  }
]);

// Property Traces
// id = IdPropertyTrace
// idProperty = IdProperty
db.propertytraces.insertMany([
  {
    IdPropertyTrace: 1,
    DateSale: new Date("2025-08-14T05:00:00Z"),
    Name: "Venta ",
    Value: 15000000,
    Tax: 12222,
    IdProperty: 1
  },
  {
    IdPropertyTrace: 2,
    DateSale: new Date("2025-08-14T05:00:00Z"),
    Name: "Venta de casa grande por pisos",
    Value: 31000000,
    Tax: 210000,
    IdProperty: 2
  }
]);

// Counters para IDs autoincrementales
// (ajustados al máximo id usado en cada colección)
db.counters.insertMany([
  { _id: "propertyid", seq: 4 },
  { _id: "ownerid", seq: 3 },
  { _id: "propertyimageid", seq: 4 },
  { _id: "propertytraceid", seq: 2 }
]);

print("Base de datos inicializada correctamente con datos personalizados.");
