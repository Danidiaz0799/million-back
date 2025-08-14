using MongoDB.Driver;
using MongoDB.Bson;
using RealEstate.Application.Interfaces;
using RealEstate.Domain.Entities;
using RealEstate.Infrastructure.Configurations;
using Microsoft.Extensions.Options;

namespace RealEstate.Infrastructure.Repositories
{
    public class PropertyTraceRepository : IPropertyTraceRepository
    {
        private readonly IMongoCollection<PropertyTrace> _collection;
        private readonly IMongoDatabase _database;

        public PropertyTraceRepository(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.DatabaseName);
            _collection = _database.GetCollection<PropertyTrace>(settings.Value.PropertyTracesCollectionName);
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

        public async Task<IEnumerable<PropertyTrace>> GetByPropertyIdAsync(int propertyId)
        {
            return await _collection.Find(pt => pt.IdProperty == propertyId)
                                   .SortByDescending(pt => pt.DateSale)
                                   .ToListAsync();
        }

        public async Task<PropertyTrace?> GetByIdAsync(int id)
        {
            return await _collection.Find(pt => pt.IdPropertyTrace == id).FirstOrDefaultAsync();
        }

        public async Task<PropertyTrace> CreateAsync(PropertyTrace propertyTrace)
        {
            propertyTrace.IdPropertyTrace = GetNextSequenceValue("propertytraceid");
            await _collection.InsertOneAsync(propertyTrace);
            return propertyTrace;
        }

        public async Task<bool> UpdateAsync(int id, PropertyTrace propertyTrace)
        {
            propertyTrace.IdPropertyTrace = id;
            var result = await _collection.ReplaceOneAsync(pt => pt.IdPropertyTrace == id, propertyTrace);
            return result.ModifiedCount > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var result = await _collection.DeleteOneAsync(pt => pt.IdPropertyTrace == id);
            return result.DeletedCount > 0;
        }
    }
}