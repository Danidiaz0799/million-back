using MongoDB.Driver;
using MongoDB.Bson;
using RealEstate.Application.Interfaces;
using RealEstate.Domain.Entities;
using RealEstate.Infrastructure.Configurations;
using Microsoft.Extensions.Options;

namespace RealEstate.Infrastructure.Repositories
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly IMongoCollection<Property> _collection;
        private readonly IMongoDatabase _database;

        public PropertyRepository(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.DatabaseName);
            _collection = _database.GetCollection<Property>(settings.Value.PropertiesCollectionName);
        }

        private int GetNextSequenceValue(string sequenceName)
        {
            var counterCollection = _database.GetCollection<BsonDocument>("counters");
            var filter = Builders<BsonDocument>.Filter.Eq("_id", sequenceName);
            var update = Builders<BsonDocument>.Update.Inc("seq", 1);
            var options = new FindOneAndUpdateOptions<BsonDocument>
            {
                IsUpsert = true,
                ReturnDocument = ReturnDocument.After
            };

            var result = counterCollection.FindOneAndUpdate(filter, update, options);
            return result["seq"].AsInt32;
        }

        public async Task<(IEnumerable<Property> Items, long Total)> GetPropertiesAsync(
            string? name,
            string? address,
            decimal? priceMin,
            decimal? priceMax,
            int page,
            int pageSize,
            string? sortField,
            bool sortDescending)
        {
            var builder = Builders<Property>.Filter;
            var filters = new List<FilterDefinition<Property>>();

            if (!string.IsNullOrWhiteSpace(name))
                filters.Add(builder.Regex(x => x.Name, new BsonRegularExpression(name, "i")));

            if (!string.IsNullOrWhiteSpace(address))
                filters.Add(builder.Regex(x => x.Address, new BsonRegularExpression(address, "i")));

            if (priceMin.HasValue)
                filters.Add(builder.Gte(x => x.Price, priceMin.Value));

            if (priceMax.HasValue)
                filters.Add(builder.Lte(x => x.Price, priceMax.Value));

            var finalFilter = filters.Count > 0 ? builder.And(filters) : builder.Empty;

            var sort = Builders<Property>.Sort.Ascending("Id");
            if (!string.IsNullOrWhiteSpace(sortField))
            {
                sort = sortDescending
                    ? Builders<Property>.Sort.Descending(sortField)
                    : Builders<Property>.Sort.Ascending(sortField);
            }

            // Total de registros (para paginación)
            var total = await _collection.CountDocumentsAsync(finalFilter);

            // Paginación
            var items = await _collection
                .Find(finalFilter)
                .Sort(sort)
                .Skip((page - 1) * pageSize)
                .Limit(pageSize)
                .ToListAsync();

            return (items, total);
        }

        public async Task<Property?> GetByIdAsync(int id)
        {
            return await _collection.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Property> CreateAsync(Property property)
        {
            property.Id = GetNextSequenceValue("propertyid");
            await _collection.InsertOneAsync(property);
            return property;
        }

        public async Task<bool> UpdateAsync(int id, Property property)
        {
            property.Id = id;
            var result = await _collection.ReplaceOneAsync(p => p.Id == id, property);
            return result.ModifiedCount > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var result = await _collection.DeleteOneAsync(p => p.Id == id);
            return result.DeletedCount > 0;
        }
    }
}
