using MongoDB.Driver;
using MongoDB.Bson;
using RealEstate.Application.Interfaces;
using RealEstate.Domain.Entities;
using RealEstate.Infrastructure.Configurations;
using Microsoft.Extensions.Options;

namespace RealEstate.Infrastructure.Repositories
{
    public class PropertyImageRepository : IPropertyImageRepository
    {
        private readonly IMongoCollection<PropertyImage> _collection;
        private readonly IMongoDatabase _database;

        public PropertyImageRepository(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.DatabaseName);
            _collection = _database.GetCollection<PropertyImage>(settings.Value.PropertyImagesCollectionName);
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

        public async Task<IEnumerable<PropertyImage>> GetByPropertyIdAsync(int propertyId)
        {
            return await _collection.Find(pi => pi.IdProperty == propertyId && pi.Enabled).ToListAsync();
        }

        public async Task<PropertyImage?> GetByIdAsync(int id)
        {
            return await _collection.Find(pi => pi.IdPropertyImage == id).FirstOrDefaultAsync();
        }

        public async Task<PropertyImage> CreateAsync(PropertyImage propertyImage)
        {
            propertyImage.IdPropertyImage = GetNextSequenceValue("propertyimageid");
            await _collection.InsertOneAsync(propertyImage);
            return propertyImage;
        }

        public async Task<bool> UpdateAsync(int id, PropertyImage propertyImage)
        {
            propertyImage.IdPropertyImage = id;
            var result = await _collection.ReplaceOneAsync(pi => pi.IdPropertyImage == id, propertyImage);
            return result.ModifiedCount > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var result = await _collection.DeleteOneAsync(pi => pi.IdPropertyImage == id);
            return result.DeletedCount > 0;
        }
    }
}