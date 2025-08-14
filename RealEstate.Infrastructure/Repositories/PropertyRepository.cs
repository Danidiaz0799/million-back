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

        public PropertyRepository(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _collection = database.GetCollection<Property>(settings.Value.PropertiesCollectionName);
        }

        public async Task<IEnumerable<Property>> GetPropertiesAsync(
            string? name,
            string? address,
            decimal? priceMin,
            decimal? priceMax,
            int page,
            int pageSize,
            string? sortField,
            bool sortDescending)
        {
            var filters = new List<FilterDefinition<Property>>();
            var builder = Builders<Property>.Filter;

            if (!string.IsNullOrWhiteSpace(name))
                filters.Add(builder.Regex(x => x.Name, new MongoDB.Bson.BsonRegularExpression(name, "i")));

            if (!string.IsNullOrWhiteSpace(address))
                filters.Add(builder.Regex(x => x.Address, new MongoDB.Bson.BsonRegularExpression(address, "i")));

            if (priceMin.HasValue)
                filters.Add(builder.Gte(x => x.Price, priceMin.Value));

            if (priceMax.HasValue)
                filters.Add(builder.Lte(x => x.Price, priceMax.Value));

            var finalFilter = filters.Count > 0 ? builder.And(filters) : builder.Empty;

            var sort = Builders<Property>.Sort.Ascending("_id");
            if (!string.IsNullOrWhiteSpace(sortField))
            {
                sort = sortDescending
                    ? Builders<Property>.Sort.Descending(sortField)
                    : Builders<Property>.Sort.Ascending(sortField);
            }

            return await _collection
                .Find(finalFilter)
                .Sort(sort)
                .Skip((page - 1) * pageSize)
                .Limit(pageSize)
                .ToListAsync();
        }
    }
}
