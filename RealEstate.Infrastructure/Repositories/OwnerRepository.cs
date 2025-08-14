using MongoDB.Driver;
using MongoDB.Bson;
using RealEstate.Application.Interfaces;
using RealEstate.Domain.Entities;
using RealEstate.Infrastructure.Configurations;
using Microsoft.Extensions.Options;

namespace RealEstate.Infrastructure.Repositories
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly IMongoCollection<Owner> _collection;
        private readonly IMongoDatabase _database;

        public OwnerRepository(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.DatabaseName);
            _collection = _database.GetCollection<Owner>(settings.Value.OwnersCollectionName);
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

        public async Task<IEnumerable<Owner>> GetOwnersAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<Owner?> GetByIdAsync(int id)
        {
            return await _collection.Find(o => o.IdOwner == id).FirstOrDefaultAsync();
        }

        public async Task<Owner> CreateAsync(Owner owner)
        {
            owner.IdOwner = GetNextSequenceValue("ownerid");
            await _collection.InsertOneAsync(owner);
            return owner;
        }

        public async Task<bool> UpdateAsync(int id, Owner owner)
        {
            owner.IdOwner = id;
            var result = await _collection.ReplaceOneAsync(o => o.IdOwner == id, owner);
            return result.ModifiedCount > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var result = await _collection.DeleteOneAsync(o => o.IdOwner == id);
            return result.DeletedCount > 0;
        }
    }
}